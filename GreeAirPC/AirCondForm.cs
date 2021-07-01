using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GreeAirPC.Database;
using GreeAirPC.Properties;

namespace GreeAirPC
{
    public partial class AirCondForm : Form
    {
        List<Gree.Controller> Ctrls = new List<Gree.Controller>();

        public AirCondForm()
        {
            InitializeComponent();

            LogTb.HideSelection = false;//Hide selection so that AppendText will auto scroll to the end

            foreach (var x in MyGree.CmdList)
                Ds1.Cmds.Rows.Add(x.Key, x.Value);
        }

        void AddUnits(List<AirCondModel> foundUnits)
        {
            DgvBinding.SuspendBinding();
            Ds1.BeginInit();
            Ds1.Devices.Clear();

            foreach (var model in foundUnits)
            {
                var ctrl = new Gree.Controller(model);
                Ctrls.Add(ctrl);
                Ds1.Devices.AddDevicesRow(model.Name, model.ID, model.PrivateKey, model.Address);
                ctrl.DeviceStatusChanged += Ctrl_DeviceStatusChanged;
            }

            Ds1.EndInit();
            DgvBinding.ResumeBinding();
            Dgv1.Rows[0].Selected = true;
        }

        Gree.Controller GetCurrentCtrl()
        {
            Gree.Controller ctrl = Ctrls[Dgv1.SelectedRows[0].Index];

            return ctrl;
        }

        void Ctrl_DeviceStatusChanged(object sender, Gree.DeviceStatusChangedEventArgs e)
        {
            try
            {
                List<string> vars = new List<string>();

                LogLine("Device status:");

                foreach (var x in e.Parameters)
                    LogLine("{0}\t{1}", MyGree.CmdList[x.Key], x.Value);
                
                LogLine("------------------------------------------");
            }
            catch
            { 
            }
        }

        async Task DiscoverAsync()
        {
            try
            {
                var units = await MyGree.DiscoverDevices();

                AddUnits(units);
            }
            catch
            {

            }
        }
        void button1_ClickAsync(object sender, EventArgs e)
        {
            
            _ = DiscoverAsync();
        }

        void SetParam(string cmd)
        {
            try
            {
                Gree.Controller ctrl = GetCurrentCtrl();

                int value = int.Parse(Tb1.Text);

                _ = ctrl.SetDeviceParameter(cmd, value);

                LogLine("Cmd: {0}  Value: {1}", cmd, value);
            }
            catch
            {

            }

        }

        private void SendCmdB_Click(object sender, EventArgs e)
        {
            try
            {
                SetParam(CmdTb.Text);
            }
            catch
            {

            }
        }

        private void GetstatusB_Click(object sender, EventArgs e)
        {
            try
            {
                Gree.Controller ctrl = GetCurrentCtrl();

                _ = ctrl.UpdateDeviceStatus();
            }
            catch
            {

            }
        }

        void SaveDeviceAsDefault(AirCondModel model)
        {
            Settings.Default.Name       = model.Name;
            Settings.Default.ID         = model.ID;
            Settings.Default.PrivateKey = model.PrivateKey;
            Settings.Default.IP = model.Address;
            Settings.Default.Save();
            LogLine("Device Saved:");
            LogLine(Settings.Default.Name);
            LogLine(Settings.Default.ID);
            LogLine(Settings.Default.PrivateKey);
            LogLine(Settings.Default.IP);


        }

        private void Dgv1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var ctrl = GetCurrentCtrl();

                 SaveDeviceAsDefault(ctrl.Model);
            }
            catch
            {

            }
        }

        private void DgvCmds_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var row = DgvCmds.SelectedRows[0];

                CmdTb.Text = (string)row.Cells[0].Value;
            }
            catch
            {
            }
        }

        void LogLine(string text, params object[] args)
        {
            LogTb.AppendText(string.Format(text + "\n", args));
        }

        private void AirCondForm_Load(object sender, EventArgs e)
        {
            try
            {
                List<AirCondModel> list = new List<AirCondModel>();
                list.Add(MyGree.GetDefault());
                AddUnits(list);

                _ = GetCurrentCtrl().UpdateDeviceStatus();
            }
            catch
            {

            }
        }
    }
}
