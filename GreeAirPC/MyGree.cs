using GreeAirPC.Database;
using GreeAirPC.Gree;
using GreeAirPC.Properties;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GreeAirPC
{
    public static class MyGree
    {
        static void SaveDeviceAsDefault(AirCondModel model)
        {
            Settings.Default.Name = model.Name;
            Settings.Default.ID = model.ID;
            Settings.Default.PrivateKey = model.PrivateKey;
            Settings.Default.IP = model.Address;
            Settings.Default.Save();
        }

        static async Task<List<AirCondModel>> DiscoverAsync(string netMask)
        {
            return await Controller.DiscoverDevices(netMask);
        }

        public static async Task<AirCondModel> GetDefault(bool forceDiscover = false)
        {
            var ID = Settings.Default.ID;

            if (!forceDiscover && !string.IsNullOrEmpty(ID) && !string.IsNullOrEmpty(Settings.Default.IP))
                return new AirCondModel(Settings.Default.ID, Settings.Default.Name, Settings.Default.PrivateKey, Settings.Default.IP);

            return await DicoverDefaultAsync();
        }

        public static async Task<AirCondModel> DicoverDefaultAsync()
        {
            var ID = Settings.Default.ID;

            List<AirCondModel> units = await DiscoverAsync(Settings.Default.NetMask);

            if (units.Count <= 0)
                throw new Exception("No units found");

			AirCondModel device = null;

            if (!string.IsNullOrEmpty(ID))
            {
                foreach (AirCondModel x in units)
                {
                    if (x.ID == ID)
                    {
                        device = x;
                        break;
                    }
                }
            }

            if (device == null) //  || (ID == "ID") || string.IsNullOrEmpty(Settings.Default.IP))
            {
                device = units[0];
                SaveDeviceAsDefault(device);
            }

            if (device.Name != Settings.Default.Name)
            {
                Settings.Default.IP = device.Address;
                Settings.Default.Save();
            }

            if (device.Address != Settings.Default.IP)
            {
                Settings.Default.IP = device.Address;
                Settings.Default.Save();
            }

            if (device.PrivateKey != Settings.Default.PrivateKey)
            {
                Settings.Default.PrivateKey = device.PrivateKey;
                Settings.Default.Save();
            }

            return device; //new AirCondModel(ID, Settings.Default.Name, Settings.Default.PrivateKey, Settings.Default.IP);
        }
    }
}
