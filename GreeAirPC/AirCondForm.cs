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

            foreach (var x in MyGree.CmdToNameDic)
                Ds1.Cmds.Rows.Add(x.Key, x.Value);

            LoadDefault();
        }

        void LoadDefault()
        {
            try
            {
                if (!string.IsNullOrEmpty(Settings.Default.NetMask))
                    NetMaskTb.Text = Settings.Default.NetMask;
            }
            catch
            {
            }

            try
            {
                List<AirCondModel> units = new List<AirCondModel>();
                units.Add(MyGree.GetDefault());
                AddUnits(units);
                _ = GetCurrentCtrl().UpdateDeviceStatus();
            }
            catch
            {

            }
        }

        void AddUnits(List<AirCondModel> foundUnits)
        {
            DgvUnitsBinding.SuspendBinding();
            Ds1.BeginInit();
            Ds1.Devices.Clear();
            Ctrls.Clear();

            foreach (var model in foundUnits)
            {
                var ctrl = new Gree.Controller(model);
                Ctrls.Add(ctrl);
                AirCondDs.DevicesRow devRow = Ds1.Devices.AddDevicesRow(model.Name, model.ID, model.PrivateKey, model.Address);
                ctrl.DeviceStatusChanged += Ctrl_DeviceStatusChanged;
            }

            Ds1.EndInit();
            DgvUnitsBinding.ResumeBinding();
            DgvUnits.Rows[0].Selected = true;
        }

        Gree.Controller GetCurrentCtrl()
        {
            Gree.Controller ctrl = Ctrls[DgvUnits.SelectedRows[DgvUnits.SelectedRows.Count-1].Index];

            return ctrl;
        }

        void Ctrl_DeviceStatusChanged(object sender, Gree.DeviceStatusChangedEventArgs e)
        {
            try
            {
                LogSep();

                List<string> vars = new List<string>();

                Gree.Controller ctrl = (Gree.Controller)sender;
                
                AirCondDs.DevicesRow devRow = Ds1.Devices.ToList().Where(x => x.ID == ctrl.DeviceID).FirstOrDefault();
                var statusRows = devRow.GetStatusRows();
                AirCondDs.StatusRow statusRow;

                if (statusRows.Length == 0)
                    statusRow = Ds1.Status.AddStatusRow(null, null, null, null, null, null, null, null, null, null, null, null, devRow, null, null);
                else
                     statusRow = devRow.GetStatusRows()[0];

                Ds1.Status.BeginInit();

                foreach (var x in e.Parameters)
                {
                    var name = MyGree.CmdToNameDic[x.Key];

                    Log("{0}: {1}, ", x.Key, x.Value);

                    statusRow[name] = x.Value;

                    OptionsClb.SelectedItem = null; // Must be done to avoid re-check

                    if (OptionsClb.Items.Contains(name))
                        OptionsClb.SetItemChecked(OptionsClb.Items.IndexOf(name), (x.Value > 0));
                }

                LogLine("");
                LogSep();

                //StatusDgv.Refresh();
                //StatusDgv.Update();
            }
            catch(Exception exc)
            { 
                LogLine(exc.Message);
            }
            finally
            {
                Ds1.Status.EndInit();
            }
        }

        async Task DiscoverAsync(string netMask)
        {
            try
            {
                List<AirCondModel> units = await MyGree.DiscoverDevices(netMask);

                AddUnits(units);
            }
            catch (Exception exc)
            {
                LogLine(exc.Message);
            }
        }

        void DiscoverButton_Click(object sender, EventArgs e)
        {
            try
            {
                _ = DiscoverAsync(NetMaskTb.Text);
            }
            catch 
            {
                LogLine("NetMask should be 192.168.1.255 or 10.0.0.255");
            }
        }

        void SetParam(string cmd, int value)
        {
            try
            {
                Gree.Controller ctrl = GetCurrentCtrl();

                _ = ctrl.SetDeviceParameter(cmd, value);

                LogLine("Cmd: {0}  Value: {1}", cmd, value);
            }
            catch (Exception exc)
            {
                LogLine(exc.Message);
            }
        }

        void SetParamByName(string name, int value)
        {
            var cmd = MyGree.NameToCmdDic[name];

            SetParam(cmd, value);
        }

        void SetParamByName(string name, bool check)
        {
            try
            {
                var cmd = MyGree.NameToCmdDic[name];
                int value = check ? ((name == "Quiet") ? 2 : 1) : 0;

                SetParam(cmd, value);
            }
            catch (Exception exc)
            {
                LogLine(exc.Message);
            }
        }

        void SetTemp(int value)
        {
            SetParamByName("Temperature", value);
        }

        void SetFan(int value)
        {
            SetParamByName("Fan Speed", value);
        }

        void SetParam(string cmd)
        {
            try
            {
                int value = int.Parse(Tb1.Text);

                SetParam(cmd, value);
            }
            catch (Exception exc)
            {
                LogLine(exc.Message);
            }
        }

        void SendCmd()
        {
            try
            {
                LogLine("Send {0}", CmdTb.Text);

                SetParam(CmdTb.Text);
            }
            catch (Exception exc)
            {
                LogLine(exc.Message);
            }
        }

        private void SendCmdB_Click(object sender, EventArgs e)
        {
            SendCmd();
        }

        void GetStatus()
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

        private void GetstatusB_Click(object sender, EventArgs e)
        {
            GetStatus();
        }

        void SaveDeviceAsDefault(AirCondModel model)
        {
            LogSep();
            LogLine("Saving Device as default: {0} {1} {2} {3}", model.Name, model.ID, model.PrivateKey, model.Address);

            Settings.Default.Name       = model.Name;
            Settings.Default.ID         = model.ID;
            Settings.Default.PrivateKey = model.PrivateKey;
            Settings.Default.IP         = model.Address;
            Settings.Default.Save();
            LogSep();
        }

        private void SetDefaultB_Click(object sender, EventArgs e)
        {
            try
            {
                Gree.Controller ctrl = GetCurrentCtrl();

                SaveDeviceAsDefault(ctrl.Model);
            }
            catch (Exception exc)
            {
                LogLine(exc.Message);
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

        void LogSep()
        {
            LogLine("------------------------------------------------");
        }
        
        void LogLine(string text, params object[] args) => Log(text + "\n", args);

        void Log(string text, params object[] args)
        {
            LogTb.AppendText(string.Format(text, args));
        }

        private void DgvUnits_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                DataGridView dgv = sender as DataGridView;

                foreach (DataGridViewRow row in dgv.Rows)
                    row.HeaderCell.Value = (row.Index + 1).ToString();
            }
            catch
            {

            }
        }

        private void Tb1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
                SendCmd();
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                if (OptionsClb.SelectedItem == null)
                    return;

                string name = OptionsClb.SelectedItem.ToString();
                
                if (string.IsNullOrEmpty(name))
                    return;
                   
                SetParamByName(name, e.NewValue > 0);

                GetStatus();
            }
            catch (Exception exc)
            {
                LogLine(exc.Message);
            }
        }

        private void TempUpDown_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                SetTemp((int)TempUpDown.Value);
                GetStatus();
            }
            catch (Exception exc)
            {
                LogLine(exc.Message);
            }
        }

        private void FanUpDown_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                SetFan((int)FanUpDown.Value);
                GetStatus();
            }
            catch (Exception exc)
            {
                LogLine(exc.Message);
            }
        }

        private void MnuAbout_Click(object sender, EventArgs e)
        {
            try
            {
                AboutBoxGreeAir abg = new AboutBoxGreeAir();

                abg.ShowDialog();
            }
            catch
            {

            }
        }
    }
}
