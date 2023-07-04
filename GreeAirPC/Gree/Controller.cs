namespace GreeAirPC.Gree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;
    using GreeAirPC.Database;
    using GreeAirPC.Gree.Protocol;
    using GreeAirPC.Logging;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using ILogger = Microsoft.Extensions.Logging.ILogger;

    internal class Controller
    {
        private AirCondModel model;
        private ILogger log;

        public Controller(AirCondModel model)
        {
            this.Parameters = new Dictionary<string, int>();
            this.model = model;

            this.log = Logger.CreateLogger($"Controller({this.model.Name}/{this.model.ID})");

            this.log.LogDebug("Controller created");
        }

        public delegate void DeviceStatusChangedEventHandler(object sender, DeviceStatusChangedEventArgs e);

        public event DeviceStatusChangedEventHandler DeviceStatusChanged;

        public string DeviceName { get => this.model.Name; private set { } }

        public string DeviceID { get => this.model.ID; private set { } }

        public AirCondModel Model { get { return model; } }

        public Dictionary<string, int> Parameters { get; private set; }

        void ThrowExc(string msg)
		{
            this.log.LogWarning(msg);
            throw new Exception(msg);
        }

        void ThrowExc(Exception exc, string msg)
        {
            this.log.LogWarning(exc.Message);
            this.log.LogWarning(msg);
            throw new Exception(msg);
        }

        public async Task<bool> UpdateDeviceStatus()
        {
            this.log.LogDebug("Updating device status");

            var columns = DevParam.DescToParam.Select(x => x.Value).ToList();
            var pack = DeviceStatusRequestPack.Create(this.model.ID, columns);
            var json = JsonConvert.SerializeObject(pack);

            var encrypted = Crypto.EncryptData(json, this.model.PrivateKey);

            if (encrypted == null)
            {
                ThrowExc("Failed to encrypt DeviceStatusRequestPack");
                return false;
            }

            var request = Request.Create(this.model.ID, encrypted);

            ResponsePackInfo response;

            try
            {
                response = await this.SendDeviceRequest(request);
            }
            catch (Exception e)
            {
                ThrowExc($"Failed to send DeviceStatusRequestPack. Error: {e.Message}");
                return false;
            }

            json = Crypto.DecryptData(response.Pack, this.model.PrivateKey);

            if (json == null)
            {
                ThrowExc("Failed to decrypt DeviceStatusResponsePack");
                return false;
            }

            var responsePack = JsonConvert.DeserializeObject<DeviceStatusResponsePack>(json);

            if (responsePack == null)
            {
                ThrowExc("Failed to deserialize DeviceStatusReponsePack");
                return false;
            }

            var updatedParameters = responsePack.Columns.Zip(responsePack.Values, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);

            bool parametersChanged = !this.Parameters.OrderBy(pair => pair.Key).SequenceEqual(updatedParameters.OrderBy(pair => pair.Key));

            if (parametersChanged)
            {
                this.log.LogDebug("Device parameters updated");
                this.Parameters = updatedParameters;
                this.DeviceStatusChanged?.Invoke(this, new DeviceStatusChangedEventArgs() { Parameters = updatedParameters });
            }

            return true;
        }

        public async Task SetDeviceParameter(string name, int value)
        {
            try
			{
                name = DevParam.DescToParam[name];  // Parsing of command name from a description
            }
            catch
			{
                // Use the name as the command name
			}

            this.log.LogDebug($"Setting parameter: {name}={value}");

            var pack = CommandRequestPack.Create(this.DeviceID, new List<string>() { name }, new List<int>() { value });
            var json = JsonConvert.SerializeObject(pack);
            var request = Request.Create(this.DeviceID, Crypto.EncryptData(json, this.model.PrivateKey));

            ResponsePackInfo response;

            try
            {
                response = await this.SendDeviceRequest(request);
            }
            catch (System.IO.IOException e)
            {
                string msg = $"Failed to send CommandRequestPack: {e.Message}";
                this.log.LogWarning(msg);
                throw new Exception(msg);
            }

            json = Crypto.DecryptData(response.Pack, this.model.PrivateKey);

            var responsePack = JsonConvert.DeserializeObject<CommandResponsePack>(json);

            if (!responsePack.Columns.Contains(name))
            {
                this.log.LogWarning("Parameter cannot be changed.");
            }
        }

        /// <summary>
        /// Sends a request to the actual device and waits a few seconds for the response.
        /// </summary>
        /// <param name="request">Request object which encapsulates the encrypted pack</param>
        /// <returns>The response object which encapsulates the encrypted response pack</returns>
        /// <exception cref="System.IO.IOException"/>
        private async Task<ResponsePackInfo> SendDeviceRequest(Request request)
        {
            this.log.LogDebug($"Sending device request");

            var datagram = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(request));

            this.log.LogDebug($"{datagram.Length} bytes will be sent");

            using (var udp = new UdpClient())
            {
                var sent = await udp.SendAsync(datagram, datagram.Length, this.model.Address, 7000);

                this.log.LogDebug($"{sent} bytes sent to {this.model.Address}");

                for (int i = 0; i < 20; ++i)
                {
                    if (udp.Available > 0)
                    {
                        var results = await udp.ReceiveAsync();

                        this.log.LogDebug($"Got response, {results.Buffer.Length} bytes");

                        var json = Encoding.ASCII.GetString(results.Buffer);
                        var response = JsonConvert.DeserializeObject<ResponsePackInfo>(json);

                        return response;
                    }

                    await Task.Delay(100);
                }

                this.log.LogWarning("Request timed out");

                throw new System.IO.IOException("Device request timed out");
            }
        }

        public static async Task<List<AirCondModel>> DiscoverDevices(string netMask)
        {
            List<AirCondModel> foundUnits = new List<Database.AirCondModel>();

            var results = await Gree.Scanner.Scan(netMask);

            foundUnits.AddRange(results);

            return foundUnits;
        }

        public async Task SetParamByName(string name, int value)
        {
			await SetDeviceParameter(name, value);
        }

        public async Task SetParamByName(string name, bool check)
        {
            var cmd = DevParam.DescToParam[name];
            int value = check ? ((name == "Quiet") ? 2 : 1) : 0;

            await SetParamByName(name, value);
        }

        public async Task SetParamByName(string name, string value)
        {
            if (name.ToLower() == DevParam.Description.Mode.ToLower())
                await SetMode(value);
            else
                await SetParamByName(name, int.Parse(value));
        }

        public async Task SetMode(string mode)
        {
            await SetParamByName(DevParam.Description.Mode, DevParam.ModeNameToIdx[mode]);
        }

        public async Task SetTemp(int value)
        {
            await SetParamByName(DevParam.Description.SetTemperature, value);
        }

        public async Task SetFan(int value)
        {
            await SetParamByName(DevParam.Description.FanSpeed, value);
        }
    }
}