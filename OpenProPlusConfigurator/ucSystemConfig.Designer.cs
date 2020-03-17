namespace OpenProPlusConfigurator
{
    partial class ucSystemConfig
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblSC = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbRedundancyMode = new System.Windows.Forms.ComboBox();
            this.lblRM = new System.Windows.Forms.Label();
            this.txtMaxDataPoints = new System.Windows.Forms.TextBox();
            this.lblTimeZone = new System.Windows.Forms.Label();
            this.lblDP = new System.Windows.Forms.Label();
            this.cmbTimeSyncSource = new System.Windows.Forms.ComboBox();
            this.lblTSS = new System.Windows.Forms.Label();
            this.txtRedundantSystemIP = new System.Windows.Forms.TextBox();
            this.lblRSIP = new System.Windows.Forms.Label();
            this.grpLog = new System.Windows.Forms.GroupBox();
            this.txtLogServerIP = new System.Windows.Forms.TextBox();
            this.lblLP = new System.Windows.Forms.Label();
            this.txtLogServerPort = new System.Windows.Forms.TextBox();
            this.lblLSP = new System.Windows.Forms.Label();
            this.lblLSIP = new System.Windows.Forms.Label();
            this.chkEdit = new System.Windows.Forms.CheckBox();
            this.cmbLogProtocol = new System.Windows.Forms.ComboBox();
            this.chkLogRemote = new System.Windows.Forms.CheckBox();
            this.chkLogLocal = new System.Windows.Forms.CheckBox();
            this.grpNTP = new System.Windows.Forms.GroupBox();
            this.chkbxNTPServerEnable = new System.Windows.Forms.CheckBox();
            this.chkNTP = new System.Windows.Forms.CheckBox();
            this.lblInterval = new System.Windows.Forms.Label();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.txtNTPServer2 = new System.Windows.Forms.TextBox();
            this.txtNTPServer1 = new System.Windows.Forms.TextBox();
            this.lblNTPS1 = new System.Windows.Forms.Label();
            this.lblNTPS2 = new System.Windows.Forms.Label();
            this.CmbTimeZone = new System.Windows.Forms.ComboBox();
            this.pnlSystemConfig = new System.Windows.Forms.Panel();
            this.ChkSLDSupported = new System.Windows.Forms.CheckBox();
            this.chkDBSync = new System.Windows.Forms.CheckBox();
            this.chkSNMP = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbHSRPRPMode = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.grpLog.SuspendLayout();
            this.grpNTP.SuspendLayout();
            this.pnlSystemConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSC
            // 
            this.lblSC.AutoSize = true;
            this.lblSC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSC.Location = new System.Drawing.Point(2, 3);
            this.lblSC.Name = "lblSC";
            this.lblSC.Size = new System.Drawing.Size(102, 15);
            this.lblSC.TabIndex = 0;
            this.lblSC.Text = "System Config:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblSC);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(792, 30);
            this.panel1.TabIndex = 37;
            // 
            // cmbRedundancyMode
            // 
            this.cmbRedundancyMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRedundancyMode.FormattingEnabled = true;
            this.cmbRedundancyMode.Location = new System.Drawing.Point(178, 9);
            this.cmbRedundancyMode.Name = "cmbRedundancyMode";
            this.cmbRedundancyMode.Size = new System.Drawing.Size(198, 21);
            this.cmbRedundancyMode.TabIndex = 40;
            this.cmbRedundancyMode.SelectedIndexChanged += new System.EventHandler(this.cmbRedundancyMode_SelectedIndexChanged);
            this.cmbRedundancyMode.SelectedValueChanged += new System.EventHandler(this.cmbRedundancyMode_SelectedValueChanged);
            // 
            // lblRM
            // 
            this.lblRM.AutoSize = true;
            this.lblRM.Location = new System.Drawing.Point(9, 12);
            this.lblRM.Name = "lblRM";
            this.lblRM.Size = new System.Drawing.Size(98, 13);
            this.lblRM.TabIndex = 39;
            this.lblRM.Text = "Redundancy Mode";
            // 
            // txtMaxDataPoints
            // 
            this.txtMaxDataPoints.Enabled = false;
            this.txtMaxDataPoints.Location = new System.Drawing.Point(178, 434);
            this.txtMaxDataPoints.Name = "txtMaxDataPoints";
            this.txtMaxDataPoints.Size = new System.Drawing.Size(197, 20);
            this.txtMaxDataPoints.TabIndex = 48;
            // 
            // lblTimeZone
            // 
            this.lblTimeZone.AutoSize = true;
            this.lblTimeZone.Location = new System.Drawing.Point(9, 89);
            this.lblTimeZone.Name = "lblTimeZone";
            this.lblTimeZone.Size = new System.Drawing.Size(58, 13);
            this.lblTimeZone.TabIndex = 49;
            this.lblTimeZone.Text = "Time Zone";
            // 
            // lblDP
            // 
            this.lblDP.AutoSize = true;
            this.lblDP.Location = new System.Drawing.Point(22, 437);
            this.lblDP.Name = "lblDP";
            this.lblDP.Size = new System.Drawing.Size(109, 13);
            this.lblDP.TabIndex = 47;
            this.lblDP.Text = "Maximum Data Points";
            // 
            // cmbTimeSyncSource
            // 
            this.cmbTimeSyncSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTimeSyncSource.FormattingEnabled = true;
            this.cmbTimeSyncSource.Location = new System.Drawing.Point(178, 62);
            this.cmbTimeSyncSource.Name = "cmbTimeSyncSource";
            this.cmbTimeSyncSource.Size = new System.Drawing.Size(198, 21);
            this.cmbTimeSyncSource.TabIndex = 44;
            this.cmbTimeSyncSource.SelectedIndexChanged += new System.EventHandler(this.cmbTimeSyncSource_SelectedIndexChanged);
            // 
            // lblTSS
            // 
            this.lblTSS.AutoSize = true;
            this.lblTSS.Location = new System.Drawing.Point(9, 63);
            this.lblTSS.Name = "lblTSS";
            this.lblTSS.Size = new System.Drawing.Size(94, 13);
            this.lblTSS.TabIndex = 43;
            this.lblTSS.Text = "Time Sync Source";
            // 
            // txtRedundantSystemIP
            // 
            this.txtRedundantSystemIP.Location = new System.Drawing.Point(178, 36);
            this.txtRedundantSystemIP.Name = "txtRedundantSystemIP";
            this.txtRedundantSystemIP.Size = new System.Drawing.Size(198, 20);
            this.txtRedundantSystemIP.TabIndex = 42;
            // 
            // lblRSIP
            // 
            this.lblRSIP.AutoSize = true;
            this.lblRSIP.Location = new System.Drawing.Point(9, 37);
            this.lblRSIP.Name = "lblRSIP";
            this.lblRSIP.Size = new System.Drawing.Size(151, 13);
            this.lblRSIP.TabIndex = 41;
            this.lblRSIP.Text = "Redundant System IP Address";
            // 
            // grpLog
            // 
            this.grpLog.Controls.Add(this.txtLogServerIP);
            this.grpLog.Controls.Add(this.lblLP);
            this.grpLog.Controls.Add(this.txtLogServerPort);
            this.grpLog.Controls.Add(this.lblLSP);
            this.grpLog.Controls.Add(this.lblLSIP);
            this.grpLog.Controls.Add(this.chkEdit);
            this.grpLog.Controls.Add(this.cmbLogProtocol);
            this.grpLog.Controls.Add(this.chkLogRemote);
            this.grpLog.Controls.Add(this.chkLogLocal);
            this.grpLog.Location = new System.Drawing.Point(12, 288);
            this.grpLog.Name = "grpLog";
            this.grpLog.Size = new System.Drawing.Size(363, 140);
            this.grpLog.TabIndex = 46;
            this.grpLog.TabStop = false;
            this.grpLog.Text = "Log Settings";
            // 
            // txtLogServerIP
            // 
            this.txtLogServerIP.Location = new System.Drawing.Point(166, 53);
            this.txtLogServerIP.Name = "txtLogServerIP";
            this.txtLogServerIP.Size = new System.Drawing.Size(190, 20);
            this.txtLogServerIP.TabIndex = 27;
            // 
            // lblLP
            // 
            this.lblLP.AutoSize = true;
            this.lblLP.Location = new System.Drawing.Point(8, 109);
            this.lblLP.Name = "lblLP";
            this.lblLP.Size = new System.Drawing.Size(67, 13);
            this.lblLP.TabIndex = 30;
            this.lblLP.Text = "Log Protocol";
            // 
            // txtLogServerPort
            // 
            this.txtLogServerPort.Enabled = false;
            this.txtLogServerPort.Location = new System.Drawing.Point(166, 79);
            this.txtLogServerPort.Name = "txtLogServerPort";
            this.txtLogServerPort.Size = new System.Drawing.Size(189, 20);
            this.txtLogServerPort.TabIndex = 29;
            // 
            // lblLSP
            // 
            this.lblLSP.AutoSize = true;
            this.lblLSP.Location = new System.Drawing.Point(8, 82);
            this.lblLSP.Name = "lblLSP";
            this.lblLSP.Size = new System.Drawing.Size(81, 13);
            this.lblLSP.TabIndex = 28;
            this.lblLSP.Text = "Log Server Port";
            // 
            // lblLSIP
            // 
            this.lblLSIP.AutoSize = true;
            this.lblLSIP.Location = new System.Drawing.Point(8, 56);
            this.lblLSIP.Name = "lblLSIP";
            this.lblLSIP.Size = new System.Drawing.Size(113, 13);
            this.lblLSIP.TabIndex = 26;
            this.lblLSIP.Text = "Log Server IP Address";
            // 
            // chkEdit
            // 
            this.chkEdit.AutoSize = true;
            this.chkEdit.Location = new System.Drawing.Point(8, 141);
            this.chkEdit.Name = "chkEdit";
            this.chkEdit.Size = new System.Drawing.Size(64, 17);
            this.chkEdit.TabIndex = 32;
            this.chkEdit.Text = "Editable";
            this.chkEdit.UseVisualStyleBackColor = true;
            this.chkEdit.Visible = false;
            // 
            // cmbLogProtocol
            // 
            this.cmbLogProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLogProtocol.Enabled = false;
            this.cmbLogProtocol.FormattingEnabled = true;
            this.cmbLogProtocol.Location = new System.Drawing.Point(166, 106);
            this.cmbLogProtocol.Name = "cmbLogProtocol";
            this.cmbLogProtocol.Size = new System.Drawing.Size(189, 21);
            this.cmbLogProtocol.TabIndex = 31;
            // 
            // chkLogRemote
            // 
            this.chkLogRemote.AutoSize = true;
            this.chkLogRemote.Location = new System.Drawing.Point(166, 30);
            this.chkLogRemote.Name = "chkLogRemote";
            this.chkLogRemote.Size = new System.Drawing.Size(84, 17);
            this.chkLogRemote.TabIndex = 25;
            this.chkLogRemote.Text = "Log Remote";
            this.chkLogRemote.UseVisualStyleBackColor = true;
            // 
            // chkLogLocal
            // 
            this.chkLogLocal.AutoSize = true;
            this.chkLogLocal.Location = new System.Drawing.Point(13, 30);
            this.chkLogLocal.Name = "chkLogLocal";
            this.chkLogLocal.Size = new System.Drawing.Size(73, 17);
            this.chkLogLocal.TabIndex = 24;
            this.chkLogLocal.Text = "Log Local";
            this.chkLogLocal.UseVisualStyleBackColor = true;
            // 
            // grpNTP
            // 
            this.grpNTP.Controls.Add(this.chkbxNTPServerEnable);
            this.grpNTP.Controls.Add(this.chkNTP);
            this.grpNTP.Controls.Add(this.lblInterval);
            this.grpNTP.Controls.Add(this.txtInterval);
            this.grpNTP.Controls.Add(this.txtNTPServer2);
            this.grpNTP.Controls.Add(this.txtNTPServer1);
            this.grpNTP.Controls.Add(this.lblNTPS1);
            this.grpNTP.Controls.Add(this.lblNTPS2);
            this.grpNTP.Location = new System.Drawing.Point(12, 147);
            this.grpNTP.Name = "grpNTP";
            this.grpNTP.Size = new System.Drawing.Size(364, 135);
            this.grpNTP.TabIndex = 45;
            this.grpNTP.TabStop = false;
            this.grpNTP.Text = "NTP Settings";
            // 
            // chkbxNTPServerEnable
            // 
            this.chkbxNTPServerEnable.AutoSize = true;
            this.chkbxNTPServerEnable.Location = new System.Drawing.Point(166, 110);
            this.chkbxNTPServerEnable.Name = "chkbxNTPServerEnable";
            this.chkbxNTPServerEnable.Size = new System.Drawing.Size(118, 17);
            this.chkbxNTPServerEnable.TabIndex = 54;
            this.chkbxNTPServerEnable.Tag = "NTPServerEnable";
            this.chkbxNTPServerEnable.Text = "NTP Server Enable";
            this.chkbxNTPServerEnable.UseVisualStyleBackColor = true;
            // 
            // chkNTP
            // 
            this.chkNTP.AutoSize = true;
            this.chkNTP.Location = new System.Drawing.Point(13, 110);
            this.chkNTP.Name = "chkNTP";
            this.chkNTP.Size = new System.Drawing.Size(70, 17);
            this.chkNTP.TabIndex = 22;
            this.chkNTP.Text = "Use NTP";
            this.chkNTP.UseVisualStyleBackColor = true;
            // 
            // lblInterval
            // 
            this.lblInterval.AutoSize = true;
            this.lblInterval.Location = new System.Drawing.Point(10, 80);
            this.lblInterval.Name = "lblInterval";
            this.lblInterval.Size = new System.Drawing.Size(87, 13);
            this.lblInterval.TabIndex = 52;
            this.lblInterval.Text = "Interval (minutes)";
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(166, 77);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(192, 20);
            this.txtInterval.TabIndex = 53;
            this.txtInterval.Tag = "IntervalInMin";
            this.txtInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInterval_KeyPress);
            // 
            // txtNTPServer2
            // 
            this.txtNTPServer2.Location = new System.Drawing.Point(166, 51);
            this.txtNTPServer2.Name = "txtNTPServer2";
            this.txtNTPServer2.Size = new System.Drawing.Size(191, 20);
            this.txtNTPServer2.TabIndex = 21;
            // 
            // txtNTPServer1
            // 
            this.txtNTPServer1.Location = new System.Drawing.Point(166, 25);
            this.txtNTPServer1.Name = "txtNTPServer1";
            this.txtNTPServer1.Size = new System.Drawing.Size(191, 20);
            this.txtNTPServer1.TabIndex = 19;
            // 
            // lblNTPS1
            // 
            this.lblNTPS1.AutoSize = true;
            this.lblNTPS1.Location = new System.Drawing.Point(8, 28);
            this.lblNTPS1.Name = "lblNTPS1";
            this.lblNTPS1.Size = new System.Drawing.Size(113, 13);
            this.lblNTPS1.TabIndex = 18;
            this.lblNTPS1.Text = "NTP Server 1 Address";
            // 
            // lblNTPS2
            // 
            this.lblNTPS2.AutoSize = true;
            this.lblNTPS2.Location = new System.Drawing.Point(10, 54);
            this.lblNTPS2.Name = "lblNTPS2";
            this.lblNTPS2.Size = new System.Drawing.Size(113, 13);
            this.lblNTPS2.TabIndex = 20;
            this.lblNTPS2.Text = "NTP Server 2 Address";
            // 
            // CmbTimeZone
            // 
            this.CmbTimeZone.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CmbTimeZone.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CmbTimeZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbTimeZone.FormattingEnabled = true;
            this.CmbTimeZone.Location = new System.Drawing.Point(178, 86);
            this.CmbTimeZone.Name = "CmbTimeZone";
            this.CmbTimeZone.Size = new System.Drawing.Size(198, 21);
            this.CmbTimeZone.TabIndex = 50;
            this.CmbTimeZone.TextUpdate += new System.EventHandler(this.CmbTimeZone_TextUpdate);
            // 
            // pnlSystemConfig
            // 
            this.pnlSystemConfig.Controls.Add(this.label3);
            this.pnlSystemConfig.Controls.Add(this.cmbHSRPRPMode);
            this.pnlSystemConfig.Controls.Add(this.chkSNMP);
            this.pnlSystemConfig.Controls.Add(this.ChkSLDSupported);
            this.pnlSystemConfig.Controls.Add(this.chkDBSync);
            this.pnlSystemConfig.Controls.Add(this.cmbRedundancyMode);
            this.pnlSystemConfig.Controls.Add(this.lblRM);
            this.pnlSystemConfig.Controls.Add(this.CmbTimeZone);
            this.pnlSystemConfig.Controls.Add(this.txtMaxDataPoints);
            this.pnlSystemConfig.Controls.Add(this.grpNTP);
            this.pnlSystemConfig.Controls.Add(this.lblTimeZone);
            this.pnlSystemConfig.Controls.Add(this.grpLog);
            this.pnlSystemConfig.Controls.Add(this.lblDP);
            this.pnlSystemConfig.Controls.Add(this.lblRSIP);
            this.pnlSystemConfig.Controls.Add(this.cmbTimeSyncSource);
            this.pnlSystemConfig.Controls.Add(this.txtRedundantSystemIP);
            this.pnlSystemConfig.Controls.Add(this.lblTSS);
            this.pnlSystemConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSystemConfig.Location = new System.Drawing.Point(0, 30);
            this.pnlSystemConfig.Name = "pnlSystemConfig";
            this.pnlSystemConfig.Size = new System.Drawing.Size(792, 574);
            this.pnlSystemConfig.TabIndex = 0;
            // 
            // ChkSLDSupported
            // 
            this.ChkSLDSupported.AutoSize = true;
            this.ChkSLDSupported.Location = new System.Drawing.Point(178, 481);
            this.ChkSLDSupported.Name = "ChkSLDSupported";
            this.ChkSLDSupported.Size = new System.Drawing.Size(97, 17);
            this.ChkSLDSupported.TabIndex = 52;
            this.ChkSLDSupported.Tag = "GUISupported";
            this.ChkSLDSupported.Text = "GUI Supported";
            this.ChkSLDSupported.UseVisualStyleBackColor = true;
            // 
            // chkDBSync
            // 
            this.chkDBSync.AutoSize = true;
            this.chkDBSync.Location = new System.Drawing.Point(178, 460);
            this.chkDBSync.Name = "chkDBSync";
            this.chkDBSync.Size = new System.Drawing.Size(68, 17);
            this.chkDBSync.TabIndex = 51;
            this.chkDBSync.Tag = "DBSync";
            this.chkDBSync.Text = "DB Sync";
            this.chkDBSync.UseVisualStyleBackColor = true;
            // 
            // chkSNMP
            // 
            this.chkSNMP.AutoSize = true;
            this.chkSNMP.Location = new System.Drawing.Point(178, 504);
            this.chkSNMP.Name = "chkSNMP";
            this.chkSNMP.Size = new System.Drawing.Size(57, 17);
            this.chkSNMP.TabIndex = 56;
            this.chkSNMP.Tag = "SNMP";
            this.chkSNMP.Text = "SNMP";
            this.chkSNMP.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 57;
            this.label3.Text = "HSRPRP Mode";
            // 
            // cmbHSRPRPMode
            // 
            this.cmbHSRPRPMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHSRPRPMode.FormattingEnabled = true;
            this.cmbHSRPRPMode.Location = new System.Drawing.Point(178, 113);
            this.cmbHSRPRPMode.Name = "cmbHSRPRPMode";
            this.cmbHSRPRPMode.Size = new System.Drawing.Size(197, 21);
            this.cmbHSRPRPMode.TabIndex = 58;
            // 
            // ucSystemConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlSystemConfig);
            this.Controls.Add(this.panel1);
            this.Name = "ucSystemConfig";
            this.Size = new System.Drawing.Size(792, 604);
            this.Load += new System.EventHandler(this.ucSystemConfig_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grpLog.ResumeLayout(false);
            this.grpLog.PerformLayout();
            this.grpNTP.ResumeLayout(false);
            this.grpNTP.PerformLayout();
            this.pnlSystemConfig.ResumeLayout(false);
            this.pnlSystemConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSC;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.ComboBox cmbRedundancyMode;
        private System.Windows.Forms.Label lblRM;
        public System.Windows.Forms.TextBox txtMaxDataPoints;
        private System.Windows.Forms.Label lblTimeZone;
        private System.Windows.Forms.Label lblDP;
        public System.Windows.Forms.ComboBox cmbTimeSyncSource;
        private System.Windows.Forms.Label lblTSS;
        public System.Windows.Forms.TextBox txtRedundantSystemIP;
        private System.Windows.Forms.Label lblRSIP;
        public System.Windows.Forms.GroupBox grpLog;
        public System.Windows.Forms.TextBox txtLogServerIP;
        private System.Windows.Forms.Label lblLP;
        public System.Windows.Forms.TextBox txtLogServerPort;
        private System.Windows.Forms.Label lblLSP;
        private System.Windows.Forms.Label lblLSIP;
        public System.Windows.Forms.CheckBox chkEdit;
        public System.Windows.Forms.ComboBox cmbLogProtocol;
        public System.Windows.Forms.CheckBox chkLogRemote;
        public System.Windows.Forms.CheckBox chkLogLocal;
        public System.Windows.Forms.GroupBox grpNTP;
        public System.Windows.Forms.CheckBox chkNTP;
        public System.Windows.Forms.TextBox txtNTPServer2;
        public System.Windows.Forms.TextBox txtNTPServer1;
        private System.Windows.Forms.Label lblNTPS1;
        private System.Windows.Forms.Label lblNTPS2;
        public System.Windows.Forms.ComboBox CmbTimeZone;
        public System.Windows.Forms.CheckBox chkDBSync;
        public System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.Label lblInterval;
        public System.Windows.Forms.CheckBox chkbxNTPServerEnable;
        public System.Windows.Forms.Panel pnlSystemConfig;
        public System.Windows.Forms.CheckBox ChkSLDSupported;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox cmbHSRPRPMode;
        public System.Windows.Forms.CheckBox chkSNMP;
    }
}
