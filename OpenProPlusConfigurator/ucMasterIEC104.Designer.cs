namespace OpenProPlusConfigurator
{
    partial class ucMasterIEC104
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
            this.grpIEC101 = new System.Windows.Forms.GroupBox();
            this.txtRefreshInterval = new System.Windows.Forms.TextBox();
            this.lblRI = new System.Windows.Forms.Label();
            this.txtIECDesc = new System.Windows.Forms.TextBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.txtFirmwareVer = new System.Windows.Forms.TextBox();
            this.lblAFV = new System.Windows.Forms.Label();
            this.txtPortNo = new System.Windows.Forms.TextBox();
            this.lblPortNo = new System.Windows.Forms.Label();
            this.txtDebug = new System.Windows.Forms.TextBox();
            this.lblDebug = new System.Windows.Forms.Label();
            this.txtClockSyncInterval = new System.Windows.Forms.TextBox();
            this.txtGiTime = new System.Windows.Forms.TextBox();
            this.lblGT = new System.Windows.Forms.Label();
            this.lblCSI = new System.Windows.Forms.Label();
            this.chkRun = new System.Windows.Forms.CheckBox();
            this.txtMasterNo = new System.Windows.Forms.TextBox();
            this.lblMN = new System.Windows.Forms.Label();
            this.lvIEDList = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnDeleteAll = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnExportIED = new System.Windows.Forms.Button();
            this.btnImportIED = new System.Windows.Forms.Button();
            this.lblIED = new System.Windows.Forms.Label();
            this.grpIED = new System.Windows.Forms.GroupBox();
            this.txtTCPPort = new System.Windows.Forms.TextBox();
            this.lblTCPPort = new System.Windows.Forms.Label();
            this.txtRdeudantIP = new System.Windows.Forms.TextBox();
            this.lblRedIP = new System.Windows.Forms.Label();
            this.txtK = new System.Windows.Forms.TextBox();
            this.lblK = new System.Windows.Forms.Label();
            this.btnLast = new System.Windows.Forms.Button();
            this.txtW = new System.Windows.Forms.TextBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblW = new System.Windows.Forms.Label();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.txtT3 = new System.Windows.Forms.TextBox();
            this.lblT3 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtT2 = new System.Windows.Forms.TextBox();
            this.lblDS = new System.Windows.Forms.Label();
            this.lblT2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtT1 = new System.Windows.Forms.TextBox();
            this.btnDone = new System.Windows.Forms.Button();
            this.lblT1 = new System.Windows.Forms.Label();
            this.txtDevice = new System.Windows.Forms.TextBox();
            this.txtT0 = new System.Windows.Forms.TextBox();
            this.lblDevice = new System.Windows.Forms.Label();
            this.lblT0 = new System.Windows.Forms.Label();
            this.txtRemoteIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CmbCOT = new System.Windows.Forms.ComboBox();
            this.CmbIOA = new System.Windows.Forms.ComboBox();
            this.lblCOTSize = new System.Windows.Forms.Label();
            this.txtUnitID = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbASDU = new System.Windows.Forms.ComboBox();
            this.lblIOASize = new System.Windows.Forms.Label();
            this.lblHdrText = new System.Windows.Forms.Label();
            this.txtTimeOut = new System.Windows.Forms.TextBox();
            this.pbHdr = new System.Windows.Forms.PictureBox();
            this.lblTO = new System.Windows.Forms.Label();
            this.lblAA = new System.Windows.Forms.Label();
            this.txtRetries = new System.Windows.Forms.TextBox();
            this.lblASDUSize = new System.Windows.Forms.Label();
            this.lblRetries = new System.Windows.Forms.Label();
            this.txtASDUaddress = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grpIEC101.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grpIED.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.grpIEC101);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lvIEDList);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.lblIED);
            this.splitContainer1.Size = new System.Drawing.Size(1050, 700);
            this.splitContainer1.SplitterDistance = 239;
            this.splitContainer1.TabIndex = 34;
            // 
            // grpIEC101
            // 
            this.grpIEC101.Controls.Add(this.txtRefreshInterval);
            this.grpIEC101.Controls.Add(this.lblRI);
            this.grpIEC101.Controls.Add(this.txtIECDesc);
            this.grpIEC101.Controls.Add(this.lblDesc);
            this.grpIEC101.Controls.Add(this.txtFirmwareVer);
            this.grpIEC101.Controls.Add(this.lblAFV);
            this.grpIEC101.Controls.Add(this.txtPortNo);
            this.grpIEC101.Controls.Add(this.lblPortNo);
            this.grpIEC101.Controls.Add(this.txtDebug);
            this.grpIEC101.Controls.Add(this.lblDebug);
            this.grpIEC101.Controls.Add(this.txtClockSyncInterval);
            this.grpIEC101.Controls.Add(this.txtGiTime);
            this.grpIEC101.Controls.Add(this.lblGT);
            this.grpIEC101.Controls.Add(this.lblCSI);
            this.grpIEC101.Controls.Add(this.chkRun);
            this.grpIEC101.Controls.Add(this.txtMasterNo);
            this.grpIEC101.Controls.Add(this.lblMN);
            this.grpIEC101.Location = new System.Drawing.Point(4, 3);
            this.grpIEC101.Name = "grpIEC101";
            this.grpIEC101.Size = new System.Drawing.Size(291, 231);
            this.grpIEC101.TabIndex = 16;
            this.grpIEC101.TabStop = false;
            this.grpIEC101.Text = "IEC104 Master";
            this.grpIEC101.Enter += new System.EventHandler(this.grpIEC101_Enter);
            // 
            // txtRefreshInterval
            // 
            this.txtRefreshInterval.Enabled = false;
            this.txtRefreshInterval.Location = new System.Drawing.Point(143, 133);
            this.txtRefreshInterval.Name = "txtRefreshInterval";
            this.txtRefreshInterval.Size = new System.Drawing.Size(130, 20);
            this.txtRefreshInterval.TabIndex = 38;
            // 
            // lblRI
            // 
            this.lblRI.AutoSize = true;
            this.lblRI.Location = new System.Drawing.Point(14, 136);
            this.lblRI.Name = "lblRI";
            this.lblRI.Size = new System.Drawing.Size(108, 13);
            this.lblRI.TabIndex = 37;
            this.lblRI.Text = "Refresh Interval (sec)";
            // 
            // txtIECDesc
            // 
            this.txtIECDesc.Enabled = false;
            this.txtIECDesc.Location = new System.Drawing.Point(143, 182);
            this.txtIECDesc.Name = "txtIECDesc";
            this.txtIECDesc.Size = new System.Drawing.Size(130, 20);
            this.txtIECDesc.TabIndex = 36;
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Location = new System.Drawing.Point(14, 185);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(60, 13);
            this.lblDesc.TabIndex = 35;
            this.lblDesc.Text = "Description";
            // 
            // txtFirmwareVer
            // 
            this.txtFirmwareVer.Enabled = false;
            this.txtFirmwareVer.Location = new System.Drawing.Point(143, 158);
            this.txtFirmwareVer.Name = "txtFirmwareVer";
            this.txtFirmwareVer.Size = new System.Drawing.Size(130, 20);
            this.txtFirmwareVer.TabIndex = 34;
            // 
            // lblAFV
            // 
            this.lblAFV.AutoSize = true;
            this.lblAFV.Location = new System.Drawing.Point(14, 161);
            this.lblAFV.Name = "lblAFV";
            this.lblAFV.Size = new System.Drawing.Size(71, 13);
            this.lblAFV.TabIndex = 33;
            this.lblAFV.Text = "Firmware Ver.";
            // 
            // txtPortNo
            // 
            this.txtPortNo.Enabled = false;
            this.txtPortNo.Location = new System.Drawing.Point(143, 37);
            this.txtPortNo.Name = "txtPortNo";
            this.txtPortNo.Size = new System.Drawing.Size(130, 20);
            this.txtPortNo.TabIndex = 32;
            // 
            // lblPortNo
            // 
            this.lblPortNo.AutoSize = true;
            this.lblPortNo.Location = new System.Drawing.Point(14, 40);
            this.lblPortNo.Name = "lblPortNo";
            this.lblPortNo.Size = new System.Drawing.Size(46, 13);
            this.lblPortNo.TabIndex = 31;
            this.lblPortNo.Text = "Port No.";
            // 
            // txtDebug
            // 
            this.txtDebug.Enabled = false;
            this.txtDebug.Location = new System.Drawing.Point(143, 61);
            this.txtDebug.Name = "txtDebug";
            this.txtDebug.Size = new System.Drawing.Size(130, 20);
            this.txtDebug.TabIndex = 30;
            // 
            // lblDebug
            // 
            this.lblDebug.AutoSize = true;
            this.lblDebug.Location = new System.Drawing.Point(14, 64);
            this.lblDebug.Name = "lblDebug";
            this.lblDebug.Size = new System.Drawing.Size(39, 13);
            this.lblDebug.TabIndex = 29;
            this.lblDebug.Text = "Debug";
            // 
            // txtClockSyncInterval
            // 
            this.txtClockSyncInterval.Enabled = false;
            this.txtClockSyncInterval.Location = new System.Drawing.Point(143, 109);
            this.txtClockSyncInterval.Name = "txtClockSyncInterval";
            this.txtClockSyncInterval.Size = new System.Drawing.Size(130, 20);
            this.txtClockSyncInterval.TabIndex = 28;
            // 
            // txtGiTime
            // 
            this.txtGiTime.Enabled = false;
            this.txtGiTime.Location = new System.Drawing.Point(143, 85);
            this.txtGiTime.Name = "txtGiTime";
            this.txtGiTime.Size = new System.Drawing.Size(130, 20);
            this.txtGiTime.TabIndex = 27;
            // 
            // lblGT
            // 
            this.lblGT.AutoSize = true;
            this.lblGT.Location = new System.Drawing.Point(14, 88);
            this.lblGT.Name = "lblGT";
            this.lblGT.Size = new System.Drawing.Size(43, 13);
            this.lblGT.TabIndex = 26;
            this.lblGT.Text = "Gi Time";
            // 
            // lblCSI
            // 
            this.lblCSI.AutoSize = true;
            this.lblCSI.Location = new System.Drawing.Point(14, 112);
            this.lblCSI.Name = "lblCSI";
            this.lblCSI.Size = new System.Drawing.Size(125, 13);
            this.lblCSI.TabIndex = 25;
            this.lblCSI.Text = "Clock Sync Interval (sec)";
            // 
            // chkRun
            // 
            this.chkRun.AutoSize = true;
            this.chkRun.Enabled = false;
            this.chkRun.Location = new System.Drawing.Point(19, 207);
            this.chkRun.Name = "chkRun";
            this.chkRun.Size = new System.Drawing.Size(46, 17);
            this.chkRun.TabIndex = 18;
            this.chkRun.Text = "Run";
            this.chkRun.UseVisualStyleBackColor = true;
            // 
            // txtMasterNo
            // 
            this.txtMasterNo.Enabled = false;
            this.txtMasterNo.Location = new System.Drawing.Point(143, 13);
            this.txtMasterNo.Name = "txtMasterNo";
            this.txtMasterNo.Size = new System.Drawing.Size(130, 20);
            this.txtMasterNo.TabIndex = 1;
            // 
            // lblMN
            // 
            this.lblMN.AutoSize = true;
            this.lblMN.Location = new System.Drawing.Point(14, 16);
            this.lblMN.Name = "lblMN";
            this.lblMN.Size = new System.Drawing.Size(59, 13);
            this.lblMN.TabIndex = 0;
            this.lblMN.Text = "Master No.";
            // 
            // lvIEDList
            // 
            this.lvIEDList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvIEDList.CheckBoxes = true;
            this.lvIEDList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvIEDList.FullRowSelect = true;
            this.lvIEDList.Location = new System.Drawing.Point(0, 13);
            this.lvIEDList.MultiSelect = false;
            this.lvIEDList.Name = "lvIEDList";
            this.lvIEDList.Size = new System.Drawing.Size(1050, 399);
            this.lvIEDList.TabIndex = 18;
            this.lvIEDList.UseCompatibleStateImageBehavior = false;
            this.lvIEDList.View = System.Windows.Forms.View.Details;
            this.lvIEDList.DoubleClick += new System.EventHandler(this.lvIEDList_DoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnDeleteAll);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnExportIED);
            this.panel1.Controls.Add(this.btnImportIED);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 412);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1050, 45);
            this.panel1.TabIndex = 19;
            // 
            // BtnDeleteAll
            // 
            this.BtnDeleteAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnDeleteAll.FlatAppearance.BorderSize = 0;
            this.BtnDeleteAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnDeleteAll.Location = new System.Drawing.Point(302, 3);
            this.BtnDeleteAll.Name = "BtnDeleteAll";
            this.BtnDeleteAll.Size = new System.Drawing.Size(68, 28);
            this.BtnDeleteAll.TabIndex = 33;
            this.BtnDeleteAll.Text = "Delete All";
            this.BtnDeleteAll.UseVisualStyleBackColor = true;
            this.BtnDeleteAll.Visible = false;
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Location = new System.Drawing.Point(4, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(68, 28);
            this.btnAdd.TabIndex = 29;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(79, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(68, 28);
            this.btnDelete.TabIndex = 30;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnExportIED
            // 
            this.btnExportIED.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExportIED.FlatAppearance.BorderSize = 0;
            this.btnExportIED.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExportIED.Location = new System.Drawing.Point(154, 3);
            this.btnExportIED.Name = "btnExportIED";
            this.btnExportIED.Size = new System.Drawing.Size(68, 28);
            this.btnExportIED.TabIndex = 31;
            this.btnExportIED.Text = "E&xport IED";
            this.btnExportIED.UseVisualStyleBackColor = true;
            this.btnExportIED.Click += new System.EventHandler(this.btnExportIED_Click);
            // 
            // btnImportIED
            // 
            this.btnImportIED.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnImportIED.FlatAppearance.BorderSize = 0;
            this.btnImportIED.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnImportIED.Location = new System.Drawing.Point(228, 3);
            this.btnImportIED.Name = "btnImportIED";
            this.btnImportIED.Size = new System.Drawing.Size(68, 28);
            this.btnImportIED.TabIndex = 32;
            this.btnImportIED.Text = "&Import IED";
            this.btnImportIED.UseVisualStyleBackColor = true;
            this.btnImportIED.Click += new System.EventHandler(this.btnImportIED_Click);
            // 
            // lblIED
            // 
            this.lblIED.AutoSize = true;
            this.lblIED.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblIED.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIED.Location = new System.Drawing.Point(0, 0);
            this.lblIED.Name = "lblIED";
            this.lblIED.Size = new System.Drawing.Size(52, 13);
            this.lblIED.TabIndex = 17;
            this.lblIED.Text = "IED List";
            // 
            // grpIED
            // 
            this.grpIED.BackColor = System.Drawing.SystemColors.Control;
            this.grpIED.Controls.Add(this.txtTCPPort);
            this.grpIED.Controls.Add(this.lblTCPPort);
            this.grpIED.Controls.Add(this.txtRdeudantIP);
            this.grpIED.Controls.Add(this.lblRedIP);
            this.grpIED.Controls.Add(this.txtK);
            this.grpIED.Controls.Add(this.lblK);
            this.grpIED.Controls.Add(this.btnLast);
            this.grpIED.Controls.Add(this.txtW);
            this.grpIED.Controls.Add(this.btnNext);
            this.grpIED.Controls.Add(this.lblW);
            this.grpIED.Controls.Add(this.btnPrev);
            this.grpIED.Controls.Add(this.btnFirst);
            this.grpIED.Controls.Add(this.txtT3);
            this.grpIED.Controls.Add(this.lblT3);
            this.grpIED.Controls.Add(this.txtDescription);
            this.grpIED.Controls.Add(this.txtT2);
            this.grpIED.Controls.Add(this.lblDS);
            this.grpIED.Controls.Add(this.lblT2);
            this.grpIED.Controls.Add(this.btnCancel);
            this.grpIED.Controls.Add(this.txtT1);
            this.grpIED.Controls.Add(this.btnDone);
            this.grpIED.Controls.Add(this.lblT1);
            this.grpIED.Controls.Add(this.txtDevice);
            this.grpIED.Controls.Add(this.txtT0);
            this.grpIED.Controls.Add(this.lblDevice);
            this.grpIED.Controls.Add(this.lblT0);
            this.grpIED.Controls.Add(this.txtRemoteIP);
            this.grpIED.Controls.Add(this.label2);
            this.grpIED.Controls.Add(this.CmbCOT);
            this.grpIED.Controls.Add(this.CmbIOA);
            this.grpIED.Controls.Add(this.lblCOTSize);
            this.grpIED.Controls.Add(this.txtUnitID);
            this.grpIED.Controls.Add(this.label7);
            this.grpIED.Controls.Add(this.cmbASDU);
            this.grpIED.Controls.Add(this.lblIOASize);
            this.grpIED.Controls.Add(this.lblHdrText);
            this.grpIED.Controls.Add(this.txtTimeOut);
            this.grpIED.Controls.Add(this.pbHdr);
            this.grpIED.Controls.Add(this.lblTO);
            this.grpIED.Controls.Add(this.lblAA);
            this.grpIED.Controls.Add(this.txtRetries);
            this.grpIED.Controls.Add(this.lblASDUSize);
            this.grpIED.Controls.Add(this.lblRetries);
            this.grpIED.Controls.Add(this.txtASDUaddress);
            this.grpIED.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpIED.Location = new System.Drawing.Point(387, 226);
            this.grpIED.Name = "grpIED";
            this.grpIED.Size = new System.Drawing.Size(325, 429);
            this.grpIED.TabIndex = 125;
            this.grpIED.TabStop = false;
            this.grpIED.Visible = false;
            // 
            // txtTCPPort
            // 
            this.txtTCPPort.Location = new System.Drawing.Point(172, 88);
            this.txtTCPPort.Name = "txtTCPPort";
            this.txtTCPPort.Size = new System.Drawing.Size(141, 20);
            this.txtTCPPort.TabIndex = 138;
            this.txtTCPPort.Tag = "TCPPort";
            this.txtTCPPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTCPPort_KeyPress);
            // 
            // lblTCPPort
            // 
            this.lblTCPPort.AutoSize = true;
            this.lblTCPPort.Location = new System.Drawing.Point(169, 72);
            this.lblTCPPort.Name = "lblTCPPort";
            this.lblTCPPort.Size = new System.Drawing.Size(70, 13);
            this.lblTCPPort.TabIndex = 137;
            this.lblTCPPort.Text = "TCP Port No.";
            // 
            // txtRdeudantIP
            // 
            this.txtRdeudantIP.Location = new System.Drawing.Point(13, 88);
            this.txtRdeudantIP.Name = "txtRdeudantIP";
            this.txtRdeudantIP.Size = new System.Drawing.Size(135, 20);
            this.txtRdeudantIP.TabIndex = 136;
            this.txtRdeudantIP.Tag = "RedundantIP";
            // 
            // lblRedIP
            // 
            this.lblRedIP.AutoSize = true;
            this.lblRedIP.Location = new System.Drawing.Point(10, 72);
            this.lblRedIP.Name = "lblRedIP";
            this.lblRedIP.Size = new System.Drawing.Size(114, 13);
            this.lblRedIP.TabIndex = 135;
            this.lblRedIP.Text = "Redundant IP Address";
            // 
            // txtK
            // 
            this.txtK.Location = new System.Drawing.Point(171, 293);
            this.txtK.Name = "txtK";
            this.txtK.Size = new System.Drawing.Size(141, 20);
            this.txtK.TabIndex = 134;
            this.txtK.Tag = "K";
            // 
            // lblK
            // 
            this.lblK.AutoSize = true;
            this.lblK.Location = new System.Drawing.Point(168, 279);
            this.lblK.Name = "lblK";
            this.lblK.Size = new System.Drawing.Size(14, 13);
            this.lblK.TabIndex = 133;
            this.lblK.Text = "K";
            // 
            // btnLast
            // 
            this.btnLast.FlatAppearance.BorderSize = 0;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast.Location = new System.Drawing.Point(252, 390);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(58, 22);
            this.btnLast.TabIndex = 114;
            this.btnLast.Text = "Last>>";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // txtW
            // 
            this.txtW.Location = new System.Drawing.Point(12, 293);
            this.txtW.Name = "txtW";
            this.txtW.Size = new System.Drawing.Size(136, 20);
            this.txtW.TabIndex = 132;
            this.txtW.Tag = "W";
            // 
            // btnNext
            // 
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(172, 390);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(58, 22);
            this.btnNext.TabIndex = 113;
            this.btnNext.Text = "Next>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblW
            // 
            this.lblW.AutoSize = true;
            this.lblW.Location = new System.Drawing.Point(9, 279);
            this.lblW.Name = "lblW";
            this.lblW.Size = new System.Drawing.Size(18, 13);
            this.lblW.TabIndex = 131;
            this.lblW.Text = "W";
            // 
            // btnPrev
            // 
            this.btnPrev.FlatAppearance.BorderSize = 0;
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrev.Location = new System.Drawing.Point(92, 390);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(58, 22);
            this.btnPrev.TabIndex = 112;
            this.btnPrev.Text = "<Prev";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.FlatAppearance.BorderSize = 0;
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFirst.Location = new System.Drawing.Point(12, 390);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(58, 22);
            this.btnFirst.TabIndex = 111;
            this.btnFirst.Text = "<<First";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // txtT3
            // 
            this.txtT3.Location = new System.Drawing.Point(267, 254);
            this.txtT3.Name = "txtT3";
            this.txtT3.Size = new System.Drawing.Size(45, 20);
            this.txtT3.TabIndex = 130;
            this.txtT3.Tag = "T3";
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.Location = new System.Drawing.Point(264, 235);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(46, 13);
            this.lblT3.TabIndex = 129;
            this.lblT3.Text = "T3 (sec)";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(171, 334);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(141, 20);
            this.txtDescription.TabIndex = 93;
            this.txtDescription.Tag = "Description";
            this.txtDescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescription_KeyPress);
            // 
            // txtT2
            // 
            this.txtT2.Location = new System.Drawing.Point(182, 254);
            this.txtT2.Name = "txtT2";
            this.txtT2.Size = new System.Drawing.Size(45, 20);
            this.txtT2.TabIndex = 128;
            this.txtT2.Tag = "T2";
            // 
            // lblDS
            // 
            this.lblDS.AutoSize = true;
            this.lblDS.Location = new System.Drawing.Point(168, 318);
            this.lblDS.Name = "lblDS";
            this.lblDS.Size = new System.Drawing.Size(60, 13);
            this.lblDS.TabIndex = 92;
            this.lblDS.Text = "Description";
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.Location = new System.Drawing.Point(179, 235);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(46, 13);
            this.lblT2.TabIndex = 127;
            this.lblT2.Text = "T2 (sec)";
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(172, 360);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 28);
            this.btnCancel.TabIndex = 96;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtT1
            // 
            this.txtT1.Location = new System.Drawing.Point(97, 254);
            this.txtT1.Name = "txtT1";
            this.txtT1.Size = new System.Drawing.Size(45, 20);
            this.txtT1.TabIndex = 126;
            this.txtT1.Tag = "T1";
            // 
            // btnDone
            // 
            this.btnDone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDone.Location = new System.Drawing.Point(80, 360);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(68, 28);
            this.btnDone.TabIndex = 95;
            this.btnDone.Text = "&Update";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.Location = new System.Drawing.Point(94, 235);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(46, 13);
            this.lblT1.TabIndex = 125;
            this.lblT1.Text = "T1 (sec)";
            // 
            // txtDevice
            // 
            this.txtDevice.Location = new System.Drawing.Point(12, 334);
            this.txtDevice.Name = "txtDevice";
            this.txtDevice.Size = new System.Drawing.Size(136, 20);
            this.txtDevice.TabIndex = 87;
            this.txtDevice.Tag = "Device";
            this.txtDevice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDevice_KeyPress);
            // 
            // txtT0
            // 
            this.txtT0.Location = new System.Drawing.Point(12, 254);
            this.txtT0.Name = "txtT0";
            this.txtT0.Size = new System.Drawing.Size(45, 20);
            this.txtT0.TabIndex = 124;
            this.txtT0.Tag = "T0";
            // 
            // lblDevice
            // 
            this.lblDevice.AutoSize = true;
            this.lblDevice.Location = new System.Drawing.Point(9, 318);
            this.lblDevice.Name = "lblDevice";
            this.lblDevice.Size = new System.Drawing.Size(41, 13);
            this.lblDevice.TabIndex = 86;
            this.lblDevice.Text = "Device";
            // 
            // lblT0
            // 
            this.lblT0.AutoSize = true;
            this.lblT0.Location = new System.Drawing.Point(9, 235);
            this.lblT0.Name = "lblT0";
            this.lblT0.Size = new System.Drawing.Size(46, 13);
            this.lblT0.TabIndex = 123;
            this.lblT0.Text = "T0 (sec)";
            // 
            // txtRemoteIP
            // 
            this.txtRemoteIP.Location = new System.Drawing.Point(171, 45);
            this.txtRemoteIP.Name = "txtRemoteIP";
            this.txtRemoteIP.Size = new System.Drawing.Size(142, 20);
            this.txtRemoteIP.TabIndex = 85;
            this.txtRemoteIP.Tag = "RemoteIP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(169, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 84;
            this.label2.Text = "Remote IP Address";
            // 
            // CmbCOT
            // 
            this.CmbCOT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbCOT.FormattingEnabled = true;
            this.CmbCOT.Location = new System.Drawing.Point(171, 166);
            this.CmbCOT.Name = "CmbCOT";
            this.CmbCOT.Size = new System.Drawing.Size(141, 21);
            this.CmbCOT.TabIndex = 121;
            this.CmbCOT.Tag = "COTSize";
            // 
            // CmbIOA
            // 
            this.CmbIOA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbIOA.FormattingEnabled = true;
            this.CmbIOA.Location = new System.Drawing.Point(12, 166);
            this.CmbIOA.Name = "CmbIOA";
            this.CmbIOA.Size = new System.Drawing.Size(136, 21);
            this.CmbIOA.TabIndex = 122;
            this.CmbIOA.Tag = "IOASize";
            // 
            // lblCOTSize
            // 
            this.lblCOTSize.AutoSize = true;
            this.lblCOTSize.Location = new System.Drawing.Point(168, 151);
            this.lblCOTSize.Name = "lblCOTSize";
            this.lblCOTSize.Size = new System.Drawing.Size(49, 13);
            this.lblCOTSize.TabIndex = 119;
            this.lblCOTSize.Text = "COTSize";
            // 
            // txtUnitID
            // 
            this.txtUnitID.Location = new System.Drawing.Point(13, 45);
            this.txtUnitID.Name = "txtUnitID";
            this.txtUnitID.Size = new System.Drawing.Size(135, 20);
            this.txtUnitID.TabIndex = 83;
            this.txtUnitID.Tag = "UnitID";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 82;
            this.label7.Text = "Unit ID";
            // 
            // cmbASDU
            // 
            this.cmbASDU.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbASDU.FormattingEnabled = true;
            this.cmbASDU.Location = new System.Drawing.Point(171, 126);
            this.cmbASDU.Name = "cmbASDU";
            this.cmbASDU.Size = new System.Drawing.Size(141, 21);
            this.cmbASDU.TabIndex = 120;
            this.cmbASDU.Tag = "ASDUSize";
            // 
            // lblIOASize
            // 
            this.lblIOASize.AutoSize = true;
            this.lblIOASize.Location = new System.Drawing.Point(9, 151);
            this.lblIOASize.Name = "lblIOASize";
            this.lblIOASize.Size = new System.Drawing.Size(45, 13);
            this.lblIOASize.TabIndex = 117;
            this.lblIOASize.Text = "IOASize";
            // 
            // lblHdrText
            // 
            this.lblHdrText.AutoSize = true;
            this.lblHdrText.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblHdrText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHdrText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHdrText.Location = new System.Drawing.Point(10, 4);
            this.lblHdrText.Name = "lblHdrText";
            this.lblHdrText.Size = new System.Drawing.Size(28, 13);
            this.lblHdrText.TabIndex = 40;
            this.lblHdrText.Text = "IED";
            this.lblHdrText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseDown);
            this.lblHdrText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseMove);
            // 
            // txtTimeOut
            // 
            this.txtTimeOut.Location = new System.Drawing.Point(171, 209);
            this.txtTimeOut.Name = "txtTimeOut";
            this.txtTimeOut.Size = new System.Drawing.Size(141, 20);
            this.txtTimeOut.TabIndex = 91;
            this.txtTimeOut.Tag = "TimeOutMS";
            // 
            // pbHdr
            // 
            this.pbHdr.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pbHdr.Location = new System.Drawing.Point(0, 0);
            this.pbHdr.Name = "pbHdr";
            this.pbHdr.Size = new System.Drawing.Size(339, 22);
            this.pbHdr.TabIndex = 39;
            this.pbHdr.TabStop = false;
            this.pbHdr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseDown);
            this.pbHdr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseMove);
            // 
            // lblTO
            // 
            this.lblTO.AutoSize = true;
            this.lblTO.Location = new System.Drawing.Point(168, 193);
            this.lblTO.Name = "lblTO";
            this.lblTO.Size = new System.Drawing.Size(79, 13);
            this.lblTO.TabIndex = 90;
            this.lblTO.Text = "Timeout (msec)";
            // 
            // lblAA
            // 
            this.lblAA.AutoSize = true;
            this.lblAA.Location = new System.Drawing.Point(9, 111);
            this.lblAA.Name = "lblAA";
            this.lblAA.Size = new System.Drawing.Size(78, 13);
            this.lblAA.TabIndex = 84;
            this.lblAA.Text = "ASDU Address";
            // 
            // txtRetries
            // 
            this.txtRetries.Location = new System.Drawing.Point(12, 209);
            this.txtRetries.Name = "txtRetries";
            this.txtRetries.Size = new System.Drawing.Size(136, 20);
            this.txtRetries.TabIndex = 89;
            this.txtRetries.Tag = "Retries";
            // 
            // lblASDUSize
            // 
            this.lblASDUSize.AutoSize = true;
            this.lblASDUSize.Location = new System.Drawing.Point(168, 111);
            this.lblASDUSize.Name = "lblASDUSize";
            this.lblASDUSize.Size = new System.Drawing.Size(57, 13);
            this.lblASDUSize.TabIndex = 115;
            this.lblASDUSize.Text = "ASDUSize";
            // 
            // lblRetries
            // 
            this.lblRetries.AutoSize = true;
            this.lblRetries.Location = new System.Drawing.Point(9, 193);
            this.lblRetries.Name = "lblRetries";
            this.lblRetries.Size = new System.Drawing.Size(40, 13);
            this.lblRetries.TabIndex = 88;
            this.lblRetries.Text = "Retries";
            // 
            // txtASDUaddress
            // 
            this.txtASDUaddress.Location = new System.Drawing.Point(12, 126);
            this.txtASDUaddress.Name = "txtASDUaddress";
            this.txtASDUaddress.Size = new System.Drawing.Size(136, 20);
            this.txtASDUaddress.TabIndex = 85;
            this.txtASDUaddress.Tag = "ASDUAddr";
            // 
            // ucMasterIEC104
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpIED);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucMasterIEC104";
            this.Size = new System.Drawing.Size(1050, 700);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.grpIEC101.ResumeLayout(false);
            this.grpIEC101.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.grpIED.ResumeLayout(false);
            this.grpIED.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox grpIEC101;
        public System.Windows.Forms.TextBox txtRefreshInterval;
        private System.Windows.Forms.Label lblRI;
        public System.Windows.Forms.TextBox txtIECDesc;
        private System.Windows.Forms.Label lblDesc;
        public System.Windows.Forms.TextBox txtFirmwareVer;
        private System.Windows.Forms.Label lblAFV;
        public System.Windows.Forms.TextBox txtPortNo;
        private System.Windows.Forms.Label lblPortNo;
        public System.Windows.Forms.TextBox txtDebug;
        private System.Windows.Forms.Label lblDebug;
        public System.Windows.Forms.TextBox txtClockSyncInterval;
        public System.Windows.Forms.TextBox txtGiTime;
        private System.Windows.Forms.Label lblGT;
        private System.Windows.Forms.Label lblCSI;
        public System.Windows.Forms.CheckBox chkRun;
        public System.Windows.Forms.TextBox txtMasterNo;
        private System.Windows.Forms.Label lblMN;
        public System.Windows.Forms.GroupBox grpIED;
        public System.Windows.Forms.TextBox txtRemoteIP;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox CmbCOT;
        public System.Windows.Forms.ComboBox CmbIOA;
        private System.Windows.Forms.Label lblCOTSize;
        public System.Windows.Forms.TextBox txtUnitID;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.ComboBox cmbASDU;
        private System.Windows.Forms.Label lblIOASize;
        private System.Windows.Forms.Label lblHdrText;
        public System.Windows.Forms.TextBox txtTimeOut;
        private System.Windows.Forms.PictureBox pbHdr;
        private System.Windows.Forms.Label lblTO;
        private System.Windows.Forms.Label lblAA;
        public System.Windows.Forms.TextBox txtRetries;
        private System.Windows.Forms.Label lblASDUSize;
        private System.Windows.Forms.Label lblRetries;
        public System.Windows.Forms.TextBox txtASDUaddress;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnFirst;
        public System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDS;
        public System.Windows.Forms.TextBox txtDevice;
        private System.Windows.Forms.Label lblDevice;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDone;
        public System.Windows.Forms.ListView lvIEDList;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button BtnDeleteAll;
        public System.Windows.Forms.Button btnAdd;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Button btnExportIED;
        public System.Windows.Forms.Button btnImportIED;
        public System.Windows.Forms.Label lblIED;
        public System.Windows.Forms.TextBox txtK;
        private System.Windows.Forms.Label lblK;
        public System.Windows.Forms.TextBox txtW;
        private System.Windows.Forms.Label lblW;
        public System.Windows.Forms.TextBox txtT3;
        private System.Windows.Forms.Label lblT3;
        public System.Windows.Forms.TextBox txtT2;
        private System.Windows.Forms.Label lblT2;
        public System.Windows.Forms.TextBox txtT1;
        private System.Windows.Forms.Label lblT1;
        public System.Windows.Forms.TextBox txtT0;
        private System.Windows.Forms.Label lblT0;
        public System.Windows.Forms.TextBox txtRdeudantIP;
        private System.Windows.Forms.Label lblRedIP;
        private System.Windows.Forms.Label lblTCPPort;
        public System.Windows.Forms.TextBox txtTCPPort;
    }
}
