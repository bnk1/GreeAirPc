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
using GreeAirPC.Gree;

namespace GreeAirPC
{
    public partial class AirCondForm : Form
    {
        List<Gree.Controller> Ctrls = new List<Gree.Controller>();

        Gree.Controller CurrentCtrl
        {
            get
            {
                Gree.Controller ctrl = Ctrls[DgvUnits.SelectedRows[DgvUnits.SelectedRows.Count - 1].Index];

                return ctrl;
            }
        }

        public AirCondForm()
        {
            InitializeComponent();

            LogTb.HideSelection = false;//Hide selection so that AppendText will auto scroll to the end

            foreach (var x in DevParam.ParamToDesc)
                Ds1.Cmds.Rows.Add(x.Key, x.Value);

            foreach (var x in DevParam.ModeIdxToName)
                Ds1.Modes.Rows.Add(x.Key, x.Value);

            LoadDefault();
        }

        async void LoadDefault()
        {
            try
            {
                if (string.IsNullOrEmpty(Settings.Default.NetMask))
				{
                    Settings.Default.NetMask = "10.0.0.255";
                    Settings.Default.Save();
                }

                NetMaskTb.Text = Settings.Default.NetMask;
            }
            catch
            {
            }

            try
            {
                await ScanAsync(NetMaskTb.Text);
                await CurrentCtrl.UpdateDeviceStatus();
            }
            catch
            {

            }
        }

        void AddUnits(List<AirCondModel> foundUnits)
        {
            if (foundUnits.Count == 0)
            {
                AppendLine("No units found");
                return;
            }
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

                foreach (var item in e.Parameters)
                {
                    var name = DevParam.ParamToDesc[item.Key];

                    AppendText("{0}: {1}, ", item.Key, item.Value);

                    statusRow[name] = item.Value;

                    OptionsClb.SelectedItem = null; // Must be done to avoid re-check

                    if (OptionsClb.Items.Contains(name))
                        OptionsClb.SetItemChecked(OptionsClb.Items.IndexOf(name), (item.Value > 0));

                    if (name == "Mode")
                        ModeClb.SetItemChecked(item.Value, true);

                    if (name == "Temperature")
						SetTemperatureGui(item.Value);
				}

                AppendLine();
                LogSep();
            }
            catch(Exception exc)
            {
                Log(exc);
            }
            finally
            {
                Ds1.Status.EndInit();
            }
        }

		private void SetTemperatureGui(Int32 value)
		{
			TempUpDown.ValueChanged -= TempUpDown_ValueChanged;
			TempUpDown.Value = value;
			TempUpDown.ValueChanged += TempUpDown_ValueChanged;
		}

		async Task DiscoverAsync(string netMask)
        {
            try
            {
                List<AirCondModel> units = await Controller.DiscoverDevices(netMask);

                AddUnits(units);
            }
            catch (Exception exc)
            {
                Log(exc);
            }
        }

        async Task ScanAsync(string netMask)
		{
            try
            {
                await DiscoverAsync(netMask);
            }
            catch
            {
                AppendLine("NetMask should be 192.168.1.255 or 10.0.0.255");
            }
        }

        void DiscoverButton_ClickAsync(object sender, EventArgs e)
        {
            _ = ScanAsync(NetMaskTb.Text);
        }


        async Task SendCmd()
        {
            try
            {
                AppendLine("Send Cmd {0}={1}", CmdTb.Text, ValueTb.Text);

                int value = int.Parse(ValueTb.Text);

                await CurrentCtrl.SetParamByName(CmdTb.Text, value);
            }
            catch (Exception exc)
            {
                Log(exc);
            }
        }

        private void SendCmdB_Click(object sender, EventArgs e)
        {
			_ = SendCmd();
        }

        async Task<bool> GetStatus()
        {
            return await CurrentCtrl.UpdateDeviceStatus();
        }

        private void GetstatusB_Click(object sender, EventArgs e)
        {
            _ = GetStatus();
        }

        void SaveDeviceAsDefault(AirCondModel model)
        {
            LogSep();
            AppendLine("Saving Device as default: {0} {1} {2} {3}", model.Name, model.ID, model.PrivateKey, model.Address);

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
                Gree.Controller ctrl = CurrentCtrl;

                SaveDeviceAsDefault(ctrl.Model);
            }
            catch (Exception exc)
            {
                Log(exc);
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
            AppendLine("------------------------------------------------");
        }
        
        void AppendLine(string text = "", params object[] args) => AppendText(text + "\n", args);

        void AppendText(string text, params object[] args)
        {
            LogTb.AppendText(string.Format(text, args));
        }

        void Log(Exception exc)
        {
            AppendLine(exc.Message);
            AppendLine(exc.Source);
            AppendLine(exc.StackTrace);

            if (exc.InnerException != null)
                Log(exc.InnerException);
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

        private void ValueTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
                _ = SendCmd();
        }

        private async Task SetCheckAsync(string name, CheckState value)
		{
            await CurrentCtrl.SetParamByName(name, value > 0);
            await GetStatus();
        }

        private void OptionsClb_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                CheckedListBox cb = sender as CheckedListBox;

                if (cb.SelectedItem == null)
                    return;

                string name = cb.SelectedItem.ToString();
                
                if (string.IsNullOrEmpty(name))
                    return;

                _ = SetCheckAsync(name, e.NewValue);
            }
            catch (Exception exc)
            {
                Log(exc);
            }
        }

        private async Task SetTemp(int value)
		{
            await CurrentCtrl.SetTemp(value);
            await GetStatus();
        }

        private void TempUpDown_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                _ = SetTemp((int)TempUpDown.Value);
            }
            catch (Exception exc)
            {
                Log(exc);
            }
        }

        private async Task SetFan(int value)
        {
            await CurrentCtrl.SetFan(value);
            await GetStatus();
        }

        private void FanUpDown_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                _ = SetFan((int)FanUpDown.Value);
            }
            catch (Exception exc)
            {
                Log(exc);
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

		private void ModeCb_ItemCheck(Object sender, ItemCheckEventArgs e)
		{
            CheckedListBox cb = sender as CheckedListBox;

            if (e.NewValue == CheckState.Checked && cb.CheckedItems.Count > 0)
            {
                cb.ItemCheck -= ModeCb_ItemCheck;
                cb.SetItemChecked(cb.CheckedIndices[0], false);
                cb.ItemCheck += ModeCb_ItemCheck;
            }
        }

        private async Task SetMode(string value)
        {
            await CurrentCtrl.SetMode(value);
            await GetStatus();
        }

        private void ModeClb_SelectedIndexChanged(Object sender, EventArgs e)
		{
            try
            {
                CheckedListBox cb = sender as CheckedListBox;
                _ = SetMode(cb.SelectedItem.ToString());
            }
            catch
			{

			}
        }

		private void SetNetmask_Click(Object sender, EventArgs e)
		{
            NetMaskTb.Text = sender.ToString();
		}
	}
}
