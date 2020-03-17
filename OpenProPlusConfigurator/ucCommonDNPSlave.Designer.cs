namespace OpenProPlusConfigurator
{
    partial class ucCommonDNPSlave
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grpIED = new System.Windows.Forms.GroupBox();
            this.txtTCPPort = new System.Windows.Forms.TextBox();
            this.lblTPort = new System.Windows.Forms.Label();
            this.CmbPortName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblPN = new System.Windows.Forms.Label();
            this.cmbProtocolType = new System.Windows.Forms.ComboBox();
            this.lblPT = new System.Windows.Forms.Label();
            this.chkBxDNPSA = new System.Windows.Forms.CheckBox();
            this.ChkbxEncryption = new System.Windows.Forms.CheckBox();
            this.ChkbxUR = new System.Windows.Forms.CheckBox();
            this.ChkBxNeedRIIN = new System.Windows.Forms.CheckBox();
            this.ChkBxNeedTIIN = new System.Windows.Forms.CheckBox();
            this.chkbxMFAllow = new System.Windows.Forms.CheckBox();
            this.txtEvenyQSize = new System.Windows.Forms.TextBox();
            this.lblEventQSize = new System.Windows.Forms.Label();
            this.txtUnitID = new System.Windows.Forms.TextBox();
            this.txtDestAdd = new System.Windows.Forms.TextBox();
            this.lbDestAdd = new System.Windows.Forms.Label();
            this.txtSelectTout = new System.Windows.Forms.TextBox();
            this.lblSelectTOut = new System.Windows.Forms.Label();
            this.txtCVP = new System.Windows.Forms.TextBox();
            this.lblCVP = new System.Windows.Forms.Label();
            this.txtACTout = new System.Windows.Forms.TextBox();
            this.lblAppCTout = new System.Windows.Forms.Label();
            this.lblFS = new System.Windows.Forms.Label();
            this.lblUI = new System.Windows.Forms.Label();
            this.txtSecRemoteIP = new System.Windows.Forms.TextBox();
            this.lblSecRemoteIP = new System.Windows.Forms.Label();
            this.txtRemoteIP = new System.Windows.Forms.TextBox();
            this.lblRIP = new System.Windows.Forms.Label();
            this.chkRun = new System.Windows.Forms.CheckBox();
            this.txtSlaveNum = new System.Windows.Forms.TextBox();
            this.lblSN = new System.Windows.Forms.Label();
            this.lblDebug = new System.Windows.Forms.Label();
            this.txtFirmwareVersion = new System.Windows.Forms.TextBox();
            this.lblAFV = new System.Windows.Forms.Label();
            this.lvSettingsDetails = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblIED = new System.Windows.Forms.Label();
            this.txtPortNo = new System.Windows.Forms.TextBox();
            this.txtFS = new System.Windows.Forms.TextBox();
            this.txtDebug = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grpIED.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.grpIED);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lvSettingsDetails);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(942, 643);
            this.splitContainer1.SplitterDistance = 299;
            this.splitContainer1.TabIndex = 38;
            // 
            // grpIED
            // 
            this.grpIED.Controls.Add(this.txtDebug);
            this.grpIED.Controls.Add(this.txtFS);
            this.grpIED.Controls.Add(this.txtPortNo);
            this.grpIED.Controls.Add(this.txtTCPPort);
            this.grpIED.Controls.Add(this.lblTPort);
            this.grpIED.Controls.Add(this.CmbPortName);
            this.grpIED.Controls.Add(this.label1);
            this.grpIED.Controls.Add(this.lblPN);
            this.grpIED.Controls.Add(this.cmbProtocolType);
            this.grpIED.Controls.Add(this.lblPT);
            this.grpIED.Controls.Add(this.chkBxDNPSA);
            this.grpIED.Controls.Add(this.ChkbxEncryption);
            this.grpIED.Controls.Add(this.ChkbxUR);
            this.grpIED.Controls.Add(this.ChkBxNeedRIIN);
            this.grpIED.Controls.Add(this.ChkBxNeedTIIN);
            this.grpIED.Controls.Add(this.chkbxMFAllow);
            this.grpIED.Controls.Add(this.txtEvenyQSize);
            this.grpIED.Controls.Add(this.lblEventQSize);
            this.grpIED.Controls.Add(this.txtUnitID);
            this.grpIED.Controls.Add(this.txtDestAdd);
            this.grpIED.Controls.Add(this.lbDestAdd);
            this.grpIED.Controls.Add(this.txtSelectTout);
            this.grpIED.Controls.Add(this.lblSelectTOut);
            this.grpIED.Controls.Add(this.txtCVP);
            this.grpIED.Controls.Add(this.lblCVP);
            this.grpIED.Controls.Add(this.txtACTout);
            this.grpIED.Controls.Add(this.lblAppCTout);
            this.grpIED.Controls.Add(this.lblFS);
            this.grpIED.Controls.Add(this.lblUI);
            this.grpIED.Controls.Add(this.txtSecRemoteIP);
            this.grpIED.Controls.Add(this.lblSecRemoteIP);
            this.grpIED.Controls.Add(this.txtRemoteIP);
            this.grpIED.Controls.Add(this.lblRIP);
            this.grpIED.Controls.Add(this.chkRun);
            this.grpIED.Controls.Add(this.txtSlaveNum);
            this.grpIED.Controls.Add(this.lblSN);
            this.grpIED.Controls.Add(this.lblDebug);
            this.grpIED.Controls.Add(this.txtFirmwareVersion);
            this.grpIED.Controls.Add(this.lblAFV);
            this.grpIED.Location = new System.Drawing.Point(6, 3);
            this.grpIED.Name = "grpIED";
            this.grpIED.Size = new System.Drawing.Size(658, 289);
            this.grpIED.TabIndex = 15;
            this.grpIED.TabStop = false;
            this.grpIED.Text = "DNP3Slave Params";
            // 
            // txtTCPPort
            // 
            this.txtTCPPort.Enabled = false;
            this.txtTCPPort.Location = new System.Drawing.Point(166, 70);
            this.txtTCPPort.Name = "txtTCPPort";
            this.txtTCPPort.Size = new System.Drawing.Size(143, 20);
            this.txtTCPPort.TabIndex = 208;
            this.txtTCPPort.Tag = "TcpPort";
            // 
            // lblTPort
            // 
            this.lblTPort.AutoSize = true;
            this.lblTPort.Location = new System.Drawing.Point(6, 73);
            this.lblTPort.Name = "lblTPort";
            this.lblTPort.Size = new System.Drawing.Size(50, 13);
            this.lblTPort.TabIndex = 207;
            this.lblTPort.Text = "TCP Port";
            // 
            // CmbPortName
            // 
            this.CmbPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbPortName.Enabled = false;
            this.CmbPortName.FormattingEnabled = true;
            this.CmbPortName.Location = new System.Drawing.Point(511, 221);
            this.CmbPortName.Name = "CmbPortName";
            this.CmbPortName.Size = new System.Drawing.Size(136, 21);
            this.CmbPortName.TabIndex = 206;
            this.CmbPortName.Tag = "PortName";
            this.CmbPortName.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(334, 224);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 205;
            this.label1.Text = "Port Name";
            this.label1.Visible = false;
            // 
            // lblPN
            // 
            this.lblPN.AutoSize = true;
            this.lblPN.Location = new System.Drawing.Point(334, 48);
            this.lblPN.Name = "lblPN";
            this.lblPN.Size = new System.Drawing.Size(46, 13);
            this.lblPN.TabIndex = 202;
            this.lblPN.Text = "Port No.";
            // 
            // cmbProtocolType
            // 
            this.cmbProtocolType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProtocolType.Enabled = false;
            this.cmbProtocolType.FormattingEnabled = true;
            this.cmbProtocolType.Location = new System.Drawing.Point(511, 20);
            this.cmbProtocolType.Name = "cmbProtocolType";
            this.cmbProtocolType.Size = new System.Drawing.Size(136, 21);
            this.cmbProtocolType.TabIndex = 201;
            this.cmbProtocolType.Tag = "ProtocolType";
            // 
            // lblPT
            // 
            this.lblPT.AutoSize = true;
            this.lblPT.Location = new System.Drawing.Point(334, 23);
            this.lblPT.Name = "lblPT";
            this.lblPT.Size = new System.Drawing.Size(73, 13);
            this.lblPT.TabIndex = 200;
            this.lblPT.Text = "Protocol Type";
            // 
            // chkBxDNPSA
            // 
            this.chkBxDNPSA.AutoSize = true;
            this.chkBxDNPSA.Enabled = false;
            this.chkBxDNPSA.Location = new System.Drawing.Point(11, 241);
            this.chkBxDNPSA.Name = "chkBxDNPSA";
            this.chkBxDNPSA.Size = new System.Drawing.Size(63, 17);
            this.chkBxDNPSA.TabIndex = 199;
            this.chkBxDNPSA.Tag = "DNPSA";
            this.chkBxDNPSA.Text = "DNPSA";
            this.chkBxDNPSA.UseVisualStyleBackColor = true;
            // 
            // ChkbxEncryption
            // 
            this.ChkbxEncryption.AutoSize = true;
            this.ChkbxEncryption.Enabled = false;
            this.ChkbxEncryption.Location = new System.Drawing.Point(11, 219);
            this.ChkbxEncryption.Name = "ChkbxEncryption";
            this.ChkbxEncryption.Size = new System.Drawing.Size(76, 17);
            this.ChkbxEncryption.TabIndex = 198;
            this.ChkbxEncryption.Tag = "Encryption";
            this.ChkbxEncryption.Text = "Encryption";
            this.ChkbxEncryption.UseVisualStyleBackColor = true;
            // 
            // ChkbxUR
            // 
            this.ChkbxUR.AutoSize = true;
            this.ChkbxUR.Checked = true;
            this.ChkbxUR.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkbxUR.Enabled = false;
            this.ChkbxUR.Location = new System.Drawing.Point(168, 241);
            this.ChkbxUR.Name = "ChkbxUR";
            this.ChkbxUR.Size = new System.Drawing.Size(129, 17);
            this.ChkbxUR.TabIndex = 197;
            this.ChkbxUR.Tag = "UnsolicitedResponse";
            this.ChkbxUR.Text = "Unsolicited Response";
            this.ChkbxUR.UseVisualStyleBackColor = true;
            // 
            // ChkBxNeedRIIN
            // 
            this.ChkBxNeedRIIN.AutoSize = true;
            this.ChkBxNeedRIIN.Enabled = false;
            this.ChkBxNeedRIIN.Location = new System.Drawing.Point(168, 218);
            this.ChkBxNeedRIIN.Name = "ChkBxNeedRIIN";
            this.ChkBxNeedRIIN.Size = new System.Drawing.Size(125, 17);
            this.ChkBxNeedRIIN.TabIndex = 196;
            this.ChkBxNeedRIIN.Tag = "SetNeedRestartIIN_YES_NO";
            this.ChkBxNeedRIIN.Text = "Set Need Restart IIN";
            this.ChkBxNeedRIIN.UseVisualStyleBackColor = true;
            // 
            // ChkBxNeedTIIN
            // 
            this.ChkBxNeedTIIN.AutoSize = true;
            this.ChkBxNeedTIIN.Enabled = false;
            this.ChkBxNeedTIIN.Location = new System.Drawing.Point(168, 195);
            this.ChkBxNeedTIIN.Name = "ChkBxNeedTIIN";
            this.ChkBxNeedTIIN.Size = new System.Drawing.Size(114, 17);
            this.ChkBxNeedTIIN.TabIndex = 195;
            this.ChkBxNeedTIIN.Tag = "SetNeedTimeIIN_YES_NO";
            this.ChkBxNeedTIIN.Text = "Set Need Time IIN";
            this.ChkBxNeedTIIN.UseVisualStyleBackColor = true;
            // 
            // chkbxMFAllow
            // 
            this.chkbxMFAllow.AutoSize = true;
            this.chkbxMFAllow.Enabled = false;
            this.chkbxMFAllow.Location = new System.Drawing.Point(11, 196);
            this.chkbxMFAllow.Name = "chkbxMFAllow";
            this.chkbxMFAllow.Size = new System.Drawing.Size(135, 17);
            this.chkbxMFAllow.TabIndex = 194;
            this.chkbxMFAllow.Tag = "MultiFragment_YES_NO";
            this.chkbxMFAllow.Text = "Multi Fragment Allowed";
            this.chkbxMFAllow.UseVisualStyleBackColor = true;
            // 
            // txtEvenyQSize
            // 
            this.txtEvenyQSize.Enabled = false;
            this.txtEvenyQSize.Location = new System.Drawing.Point(511, 71);
            this.txtEvenyQSize.Name = "txtEvenyQSize";
            this.txtEvenyQSize.Size = new System.Drawing.Size(136, 20);
            this.txtEvenyQSize.TabIndex = 193;
            this.txtEvenyQSize.Tag = "EventQSize";
            // 
            // lblEventQSize
            // 
            this.lblEventQSize.AutoSize = true;
            this.lblEventQSize.Location = new System.Drawing.Point(334, 74);
            this.lblEventQSize.Name = "lblEventQSize";
            this.lblEventQSize.Size = new System.Drawing.Size(63, 13);
            this.lblEventQSize.TabIndex = 192;
            this.lblEventQSize.Text = "EventQSize";
            // 
            // txtUnitID
            // 
            this.txtUnitID.Enabled = false;
            this.txtUnitID.Location = new System.Drawing.Point(166, 45);
            this.txtUnitID.Name = "txtUnitID";
            this.txtUnitID.Size = new System.Drawing.Size(143, 20);
            this.txtUnitID.TabIndex = 191;
            this.txtUnitID.Tag = "UnitID";
            // 
            // txtDestAdd
            // 
            this.txtDestAdd.Enabled = false;
            this.txtDestAdd.Location = new System.Drawing.Point(166, 170);
            this.txtDestAdd.Name = "txtDestAdd";
            this.txtDestAdd.Size = new System.Drawing.Size(143, 20);
            this.txtDestAdd.TabIndex = 190;
            this.txtDestAdd.Tag = "DestAdd";
            // 
            // lbDestAdd
            // 
            this.lbDestAdd.AutoSize = true;
            this.lbDestAdd.Location = new System.Drawing.Point(6, 173);
            this.lbDestAdd.Name = "lbDestAdd";
            this.lbDestAdd.Size = new System.Drawing.Size(101, 13);
            this.lbDestAdd.TabIndex = 189;
            this.lbDestAdd.Text = "Destination Address";
            // 
            // txtSelectTout
            // 
            this.txtSelectTout.Enabled = false;
            this.txtSelectTout.Location = new System.Drawing.Point(511, 146);
            this.txtSelectTout.Name = "txtSelectTout";
            this.txtSelectTout.Size = new System.Drawing.Size(136, 20);
            this.txtSelectTout.TabIndex = 188;
            this.txtSelectTout.Tag = "SelectTOut";
            // 
            // lblSelectTOut
            // 
            this.lblSelectTOut.AutoSize = true;
            this.lblSelectTOut.Location = new System.Drawing.Point(334, 149);
            this.lblSelectTOut.Name = "lblSelectTOut";
            this.lblSelectTOut.Size = new System.Drawing.Size(160, 13);
            this.lblSelectTOut.TabIndex = 187;
            this.lblSelectTOut.Text = "Select Timeout (0-4294966 Sec)";
            // 
            // txtCVP
            // 
            this.txtCVP.Enabled = false;
            this.txtCVP.Location = new System.Drawing.Point(511, 121);
            this.txtCVP.Name = "txtCVP";
            this.txtCVP.Size = new System.Drawing.Size(136, 20);
            this.txtCVP.TabIndex = 186;
            this.txtCVP.Tag = "ClockValidatePeriod";
            // 
            // lblCVP
            // 
            this.lblCVP.AutoSize = true;
            this.lblCVP.Location = new System.Drawing.Point(334, 124);
            this.lblCVP.Name = "lblCVP";
            this.lblCVP.Size = new System.Drawing.Size(176, 13);
            this.lblCVP.TabIndex = 185;
            this.lblCVP.Text = "Clock Validate Period (0-70581 Min)";
            // 
            // txtACTout
            // 
            this.txtACTout.Enabled = false;
            this.txtACTout.Location = new System.Drawing.Point(511, 96);
            this.txtACTout.Name = "txtACTout";
            this.txtACTout.Size = new System.Drawing.Size(136, 20);
            this.txtACTout.TabIndex = 184;
            this.txtACTout.Tag = "AppConfirmTimeout";
            // 
            // lblAppCTout
            // 
            this.lblAppCTout.AutoSize = true;
            this.lblAppCTout.Location = new System.Drawing.Point(334, 99);
            this.lblAppCTout.Name = "lblAppCTout";
            this.lblAppCTout.Size = new System.Drawing.Size(169, 13);
            this.lblAppCTout.TabIndex = 183;
            this.lblAppCTout.Text = "Application ConfirmTimeout (msec)";
            // 
            // lblFS
            // 
            this.lblFS.AutoSize = true;
            this.lblFS.Location = new System.Drawing.Point(6, 148);
            this.lblFS.Name = "lblFS";
            this.lblFS.Size = new System.Drawing.Size(74, 13);
            this.lblFS.TabIndex = 181;
            this.lblFS.Text = "Fragment Size";
            // 
            // lblUI
            // 
            this.lblUI.AutoSize = true;
            this.lblUI.Location = new System.Drawing.Point(6, 48);
            this.lblUI.Name = "lblUI";
            this.lblUI.Size = new System.Drawing.Size(40, 13);
            this.lblUI.TabIndex = 180;
            this.lblUI.Text = "Unit ID";
            // 
            // txtSecRemoteIP
            // 
            this.txtSecRemoteIP.Enabled = false;
            this.txtSecRemoteIP.Location = new System.Drawing.Point(166, 120);
            this.txtSecRemoteIP.Name = "txtSecRemoteIP";
            this.txtSecRemoteIP.Size = new System.Drawing.Size(143, 20);
            this.txtSecRemoteIP.TabIndex = 179;
            this.txtSecRemoteIP.Tag = "SecRemoteIP";
            // 
            // lblSecRemoteIP
            // 
            this.lblSecRemoteIP.AutoSize = true;
            this.lblSecRemoteIP.Location = new System.Drawing.Point(6, 123);
            this.lblSecRemoteIP.Name = "lblSecRemoteIP";
            this.lblSecRemoteIP.Size = new System.Drawing.Size(152, 13);
            this.lblSecRemoteIP.TabIndex = 178;
            this.lblSecRemoteIP.Text = "Secondary Remote IP Address";
            // 
            // txtRemoteIP
            // 
            this.txtRemoteIP.Enabled = false;
            this.txtRemoteIP.Location = new System.Drawing.Point(166, 95);
            this.txtRemoteIP.Name = "txtRemoteIP";
            this.txtRemoteIP.Size = new System.Drawing.Size(143, 20);
            this.txtRemoteIP.TabIndex = 177;
            this.txtRemoteIP.Tag = "RemoteIP";
            // 
            // lblRIP
            // 
            this.lblRIP.AutoSize = true;
            this.lblRIP.Location = new System.Drawing.Point(6, 98);
            this.lblRIP.Name = "lblRIP";
            this.lblRIP.Size = new System.Drawing.Size(98, 13);
            this.lblRIP.TabIndex = 176;
            this.lblRIP.Text = "Remote IP Address";
            // 
            // chkRun
            // 
            this.chkRun.AutoSize = true;
            this.chkRun.Enabled = false;
            this.chkRun.Location = new System.Drawing.Point(168, 264);
            this.chkRun.Name = "chkRun";
            this.chkRun.Size = new System.Drawing.Size(46, 17);
            this.chkRun.TabIndex = 175;
            this.chkRun.Tag = "Run_YES_NO";
            this.chkRun.Text = "Run";
            this.chkRun.UseVisualStyleBackColor = true;
            // 
            // txtSlaveNum
            // 
            this.txtSlaveNum.Enabled = false;
            this.txtSlaveNum.Location = new System.Drawing.Point(166, 20);
            this.txtSlaveNum.Name = "txtSlaveNum";
            this.txtSlaveNum.Size = new System.Drawing.Size(143, 20);
            this.txtSlaveNum.TabIndex = 168;
            this.txtSlaveNum.Tag = "SlaveNum";
            // 
            // lblSN
            // 
            this.lblSN.AutoSize = true;
            this.lblSN.Location = new System.Drawing.Point(6, 23);
            this.lblSN.Name = "lblSN";
            this.lblSN.Size = new System.Drawing.Size(54, 13);
            this.lblSN.TabIndex = 167;
            this.lblSN.Text = "Slave No.";
            // 
            // lblDebug
            // 
            this.lblDebug.AutoSize = true;
            this.lblDebug.Location = new System.Drawing.Point(334, 199);
            this.lblDebug.Name = "lblDebug";
            this.lblDebug.Size = new System.Drawing.Size(68, 13);
            this.lblDebug.TabIndex = 171;
            this.lblDebug.Text = "Debug Level";
            // 
            // txtFirmwareVersion
            // 
            this.txtFirmwareVersion.Enabled = false;
            this.txtFirmwareVersion.Location = new System.Drawing.Point(511, 171);
            this.txtFirmwareVersion.Name = "txtFirmwareVersion";
            this.txtFirmwareVersion.Size = new System.Drawing.Size(136, 20);
            this.txtFirmwareVersion.TabIndex = 170;
            this.txtFirmwareVersion.Tag = "AppFirmwareVersion";
            // 
            // lblAFV
            // 
            this.lblAFV.AutoSize = true;
            this.lblAFV.Location = new System.Drawing.Point(334, 174);
            this.lblAFV.Name = "lblAFV";
            this.lblAFV.Size = new System.Drawing.Size(87, 13);
            this.lblAFV.TabIndex = 169;
            this.lblAFV.Text = "Firmware Version";
            // 
            // lvSettingsDetails
            // 
            this.lvSettingsDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvSettingsDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSettingsDetails.FullRowSelect = true;
            this.lvSettingsDetails.Location = new System.Drawing.Point(0, 30);
            this.lvSettingsDetails.MultiSelect = false;
            this.lvSettingsDetails.Name = "lvSettingsDetails";
            this.lvSettingsDetails.Size = new System.Drawing.Size(942, 310);
            this.lvSettingsDetails.TabIndex = 35;
            this.lvSettingsDetails.UseCompatibleStateImageBehavior = false;
            this.lvSettingsDetails.View = System.Windows.Forms.View.Details;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblIED);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(942, 30);
            this.panel1.TabIndex = 36;
            // 
            // lblIED
            // 
            this.lblIED.AutoSize = true;
            this.lblIED.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIED.Location = new System.Drawing.Point(2, 5);
            this.lblIED.Name = "lblIED";
            this.lblIED.Size = new System.Drawing.Size(75, 15);
            this.lblIED.TabIndex = 0;
            this.lblIED.Text = "DNP3 List:";
            // 
            // txtPortNo
            // 
            this.txtPortNo.Enabled = false;
            this.txtPortNo.Location = new System.Drawing.Point(511, 46);
            this.txtPortNo.Name = "txtPortNo";
            this.txtPortNo.Size = new System.Drawing.Size(136, 20);
            this.txtPortNo.TabIndex = 209;
            this.txtPortNo.Tag = "SlaveNum";
            // 
            // txtFS
            // 
            this.txtFS.Enabled = false;
            this.txtFS.Location = new System.Drawing.Point(166, 145);
            this.txtFS.Name = "txtFS";
            this.txtFS.Size = new System.Drawing.Size(143, 20);
            this.txtFS.TabIndex = 210;
            this.txtFS.Tag = "FragmentSize";
            // 
            // txtDebug
            // 
            this.txtDebug.Enabled = false;
            this.txtDebug.Location = new System.Drawing.Point(511, 196);
            this.txtDebug.Name = "txtDebug";
            this.txtDebug.Size = new System.Drawing.Size(136, 20);
            this.txtDebug.TabIndex = 211;
            this.txtDebug.Tag = "DEBUG";
            // 
            // ucCommonDNPSlave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucCommonDNPSlave";
            this.Size = new System.Drawing.Size(942, 643);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.grpIED.ResumeLayout(false);
            this.grpIED.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox grpIED;
        public System.Windows.Forms.TextBox txtTCPPort;
        private System.Windows.Forms.Label lblTPort;
        public System.Windows.Forms.ComboBox CmbPortName;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label lblPN;
        public System.Windows.Forms.ComboBox cmbProtocolType;
        private System.Windows.Forms.Label lblPT;
        public System.Windows.Forms.CheckBox chkBxDNPSA;
        public System.Windows.Forms.CheckBox ChkbxEncryption;
        public System.Windows.Forms.CheckBox ChkbxUR;
        public System.Windows.Forms.CheckBox ChkBxNeedRIIN;
        public System.Windows.Forms.CheckBox ChkBxNeedTIIN;
        public System.Windows.Forms.CheckBox chkbxMFAllow;
        public System.Windows.Forms.TextBox txtEvenyQSize;
        private System.Windows.Forms.Label lblEventQSize;
        public System.Windows.Forms.TextBox txtUnitID;
        public System.Windows.Forms.TextBox txtDestAdd;
        private System.Windows.Forms.Label lbDestAdd;
        public System.Windows.Forms.TextBox txtSelectTout;
        private System.Windows.Forms.Label lblSelectTOut;
        public System.Windows.Forms.TextBox txtCVP;
        private System.Windows.Forms.Label lblCVP;
        public System.Windows.Forms.TextBox txtACTout;
        private System.Windows.Forms.Label lblAppCTout;
        private System.Windows.Forms.Label lblFS;
        private System.Windows.Forms.Label lblUI;
        public System.Windows.Forms.TextBox txtSecRemoteIP;
        private System.Windows.Forms.Label lblSecRemoteIP;
        public System.Windows.Forms.TextBox txtRemoteIP;
        private System.Windows.Forms.Label lblRIP;
        public System.Windows.Forms.CheckBox chkRun;
        public System.Windows.Forms.TextBox txtSlaveNum;
        private System.Windows.Forms.Label lblSN;
        private System.Windows.Forms.Label lblDebug;
        public System.Windows.Forms.TextBox txtFirmwareVersion;
        private System.Windows.Forms.Label lblAFV;
        public System.Windows.Forms.ListView lvSettingsDetails;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label lblIED;
        public System.Windows.Forms.TextBox txtPortNo;
        public System.Windows.Forms.TextBox txtFS;
        public System.Windows.Forms.TextBox txtDebug;
    }
}
