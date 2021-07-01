namespace GreeAirPC
{
    partial class AirCondForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ScanB = new System.Windows.Forms.Button();
            this.Ds1 = new GreeAirPC.AirCondDs();
            this.Dgv1 = new System.Windows.Forms.DataGridView();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iPDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.privateKeyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvBinding = new System.Windows.Forms.BindingSource(this.components);
            this.Tb1 = new System.Windows.Forms.TextBox();
            this.CmdTb = new System.Windows.Forms.TextBox();
            this.SendCmdB = new System.Windows.Forms.Button();
            this.LogTb = new System.Windows.Forms.RichTextBox();
            this.GetstatusB = new System.Windows.Forms.Button();
            this.DgvCmds = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Ds1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvBinding)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvCmds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmdsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ScanB
            // 
            this.ScanB.Location = new System.Drawing.Point(35, 37);
            this.ScanB.Name = "ScanB";
            this.ScanB.Size = new System.Drawing.Size(75, 23);
            this.ScanB.TabIndex = 0;
            this.ScanB.Text = "Scan";
            this.ScanB.UseVisualStyleBackColor = true;
            this.ScanB.Click += new System.EventHandler(this.button1_ClickAsync);
            // 
            // Ds1
            // 
            this.Ds1.DataSetName = "AirCondDs";
            this.Ds1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Dgv1
            // 
            this.Dgv1.AutoGenerateColumns = false;
            this.Dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.iPDataGridViewTextBoxColumn,
            this.privateKeyDataGridViewTextBoxColumn});
            this.Dgv1.DataSource = this.DgvBinding;
            this.Dgv1.Location = new System.Drawing.Point(126, 37);
            this.Dgv1.Name = "Dgv1";
            this.Dgv1.ReadOnly = true;
            this.Dgv1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv1.ShowCellErrors = false;
            this.Dgv1.ShowCellToolTips = false;
            this.Dgv1.ShowEditingIcon = false;
            this.Dgv1.ShowRowErrors = false;
            this.Dgv1.Size = new System.Drawing.Size(359, 151);
            this.Dgv1.TabIndex = 1;
            this.Dgv1.SelectionChanged += new System.EventHandler(this.Dgv1_SelectionChanged);
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.MinimumWidth = 20;
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.ReadOnly = true;
            this.iDDataGridViewTextBoxColumn.Width = 43;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.MinimumWidth = 60;
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn.Width = 60;
            // 
            // iPDataGridViewTextBoxColumn
            // 
            this.iPDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.iPDataGridViewTextBoxColumn.DataPropertyName = "IP";
            this.iPDataGridViewTextBoxColumn.HeaderText = "IP";
            this.iPDataGridViewTextBoxColumn.MinimumWidth = 60;
            this.iPDataGridViewTextBoxColumn.Name = "iPDataGridViewTextBoxColumn";
            this.iPDataGridViewTextBoxColumn.ReadOnly = true;
            this.iPDataGridViewTextBoxColumn.Width = 60;
            // 
            // privateKeyDataGridViewTextBoxColumn
            // 
            this.privateKeyDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.privateKeyDataGridViewTextBoxColumn.DataPropertyName = "PrivateKey";
            this.privateKeyDataGridViewTextBoxColumn.HeaderText = "PrivateKey";
            this.privateKeyDataGridViewTextBoxColumn.Name = "privateKeyDataGridViewTextBoxColumn";
            this.privateKeyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // DgvBinding
            // 
            this.DgvBinding.DataMember = "Devices";
            this.DgvBinding.DataSource = this.Ds1;
            // 
            // Tb1
            // 
            this.Tb1.Location = new System.Drawing.Point(595, 417);
            this.Tb1.Name = "Tb1";
            this.Tb1.Size = new System.Drawing.Size(100, 20);
            this.Tb1.TabIndex = 4;
            // 
            // CmdTb
            // 
            this.CmdTb.Location = new System.Drawing.Point(504, 417);
            this.CmdTb.Name = "CmdTb";
            this.CmdTb.Size = new System.Drawing.Size(85, 20);
            this.CmdTb.TabIndex = 5;
            // 
            // SendCmdB
            // 
            this.SendCmdB.Location = new System.Drawing.Point(710, 417);
            this.SendCmdB.Name = "SendCmdB";
            this.SendCmdB.Size = new System.Drawing.Size(75, 23);
            this.SendCmdB.TabIndex = 6;
            this.SendCmdB.Text = "Send";
            this.SendCmdB.UseVisualStyleBackColor = true;
            this.SendCmdB.Click += new System.EventHandler(this.SendCmdB_Click);
            // 
            // LogTb
            // 
            this.LogTb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogTb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LogTb.Location = new System.Drawing.Point(12, 450);
            this.LogTb.Name = "LogTb";
            this.LogTb.Size = new System.Drawing.Size(776, 359);
            this.LogTb.TabIndex = 7;
            this.LogTb.Text = "";
            // 
            // GetstatusB
            // 
            this.GetstatusB.Location = new System.Drawing.Point(35, 66);
            this.GetstatusB.Name = "GetstatusB";
            this.GetstatusB.Size = new System.Drawing.Size(75, 23);
            this.GetstatusB.TabIndex = 8;
            this.GetstatusB.Text = "Status";
            this.GetstatusB.UseVisualStyleBackColor = true;
            this.GetstatusB.Click += new System.EventHandler(this.GetstatusB_Click);
            // 
            // DgvCmds
            // 
            this.DgvCmds.AutoGenerateColumns = false;
            this.DgvCmds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvCmds.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn1,
            this.cmdDataGridViewTextBoxColumn});
            this.DgvCmds.DataSource = this.cmdsBindingSource;
            this.DgvCmds.Location = new System.Drawing.Point(507, 37);
            this.DgvCmds.Name = "DgvCmds";
            this.DgvCmds.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvCmds.Size = new System.Drawing.Size(281, 361);
            this.DgvCmds.TabIndex = 9;
            this.DgvCmds.SelectionChanged += new System.EventHandler(this.DgvCmds_SelectionChanged);
            // 
            // nameDataGridViewTextBoxColumn1
            // 
            this.nameDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.nameDataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn1.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn1.Name = "nameDataGridViewTextBoxColumn1";
            this.nameDataGridViewTextBoxColumn1.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn1.Width = 60;
            // 
            // cmdDataGridViewTextBoxColumn
            // 
            this.cmdDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cmdDataGridViewTextBoxColumn.DataPropertyName = "Cmd";
            this.cmdDataGridViewTextBoxColumn.HeaderText = "Cmd";
            this.cmdDataGridViewTextBoxColumn.Name = "cmdDataGridViewTextBoxColumn";
            this.cmdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cmdsBindingSource
            // 
            this.cmdsBindingSource.DataMember = "Cmds";
            this.cmdsBindingSource.DataSource = this.Ds1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(504, 401);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Command";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(595, 401);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Data";
            // 
            // AirCondForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 821);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DgvCmds);
            this.Controls.Add(this.GetstatusB);
            this.Controls.Add(this.LogTb);
            this.Controls.Add(this.SendCmdB);
            this.Controls.Add(this.CmdTb);
            this.Controls.Add(this.Tb1);
            this.Controls.Add(this.Dgv1);
            this.Controls.Add(this.ScanB);
            this.Name = "AirCondForm";
            this.Text = "GreeAirPC";
            this.Load += new System.EventHandler(this.AirCondForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Ds1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvBinding)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvCmds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmdsBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ScanB;
        private AirCondDs Ds1;
        private System.Windows.Forms.DataGridView Dgv1;
        private System.Windows.Forms.BindingSource DgvBinding;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iPDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn privateKeyDataGridViewTextBoxColumn;
        private System.Windows.Forms.TextBox Tb1;
        private System.Windows.Forms.TextBox CmdTb;
        private System.Windows.Forms.Button SendCmdB;
        private System.Windows.Forms.RichTextBox LogTb;
        private System.Windows.Forms.Button GetstatusB;
        private System.Windows.Forms.DataGridView DgvCmds;
        private System.Windows.Forms.BindingSource cmdsBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cmdDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

