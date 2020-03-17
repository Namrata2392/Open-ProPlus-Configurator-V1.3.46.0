namespace OpenProPlusConfigurator
{
    partial class ucMasterSPORT
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
            this.btnDone = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pbHdr = new System.Windows.Forms.PictureBox();
            this.lblHdrText = new System.Windows.Forms.Label();
            this.lblDevice = new System.Windows.Forms.Label();
            this.txtDevice = new System.Windows.Forms.TextBox();
            this.lblRetries = new System.Windows.Forms.Label();
            this.txtRetries = new System.Windows.Forms.TextBox();
            this.lblTO = new System.Windows.Forms.Label();
            this.txtTimeOut = new System.Windows.Forms.TextBox();
            this.lblUI = new System.Windows.Forms.Label();
            this.txtUnitID = new System.Windows.Forms.TextBox();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.lblIOASize = new System.Windows.Forms.Label();
            this.CmbIOA = new System.Windows.Forms.ComboBox();
            this.lblSportType = new System.Windows.Forms.Label();
            this.CmbSportType = new System.Windows.Forms.ComboBox();
            this.lblLastAI = new System.Windows.Forms.Label();
            this.txtLastAI = new System.Windows.Forms.TextBox();
            this.lblLastDI = new System.Windows.Forms.Label();
            this.txtLastDI = new System.Windows.Forms.TextBox();
            this.lblLastDO = new System.Windows.Forms.Label();
            this.txtLastDO = new System.Windows.Forms.TextBox();
            this.lblTimestampType = new System.Windows.Forms.Label();
            this.txtTimestampType = new System.Windows.Forms.TextBox();
            this.lblTimeAccuracy = new System.Windows.Forms.Label();
            this.txtTimestampAccuracy = new System.Windows.Forms.TextBox();
            this.grpIED = new System.Windows.Forms.GroupBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtWindowTime = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.lblWindowTime = new System.Windows.Forms.Label();
            this.txtPulsewidthTimeout = new System.Windows.Forms.TextBox();
            this.lblPulsewidthTimeout = new System.Windows.Forms.Label();
            this.txtDebounceTime = new System.Windows.Forms.TextBox();
            this.lblDebounceTime = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grpIEC101.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).BeginInit();
            this.grpIED.SuspendLayout();
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
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
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
            this.grpIEC101.Text = "SPORT Master";
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
            this.lvIEDList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvIEDList_ItemCheck);
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
            this.BtnDeleteAll.Click += new System.EventHandler(this.BtnDeleteAll_Click);
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
            // btnDone
            // 
            this.btnDone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDone.Location = new System.Drawing.Point(164, 409);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(68, 28);
            this.btnDone.TabIndex = 95;
            this.btnDone.Text = "&Update";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(272, 411);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 28);
            this.btnCancel.TabIndex = 96;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pbHdr
            // 
            this.pbHdr.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pbHdr.Location = new System.Drawing.Point(0, 0);
            this.pbHdr.Name = "pbHdr";
            this.pbHdr.Size = new System.Drawing.Size(369, 22);
            this.pbHdr.TabIndex = 39;
            this.pbHdr.TabStop = false;
            this.pbHdr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseDown);
            this.pbHdr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseMove);
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
            // lblDevice
            // 
            this.lblDevice.AutoSize = true;
            this.lblDevice.Location = new System.Drawing.Point(16, 59);
            this.lblDevice.Name = "lblDevice";
            this.lblDevice.Size = new System.Drawing.Size(41, 13);
            this.lblDevice.TabIndex = 86;
            this.lblDevice.Text = "Device";
            // 
            // txtDevice
            // 
            this.txtDevice.Location = new System.Drawing.Point(164, 56);
            this.txtDevice.Name = "txtDevice";
            this.txtDevice.Size = new System.Drawing.Size(175, 20);
            this.txtDevice.TabIndex = 87;
            this.txtDevice.Tag = "Device";
            // 
            // lblRetries
            // 
            this.lblRetries.AutoSize = true;
            this.lblRetries.Location = new System.Drawing.Point(16, 84);
            this.lblRetries.Name = "lblRetries";
            this.lblRetries.Size = new System.Drawing.Size(40, 13);
            this.lblRetries.TabIndex = 88;
            this.lblRetries.Text = "Retries";
            // 
            // txtRetries
            // 
            this.txtRetries.Location = new System.Drawing.Point(164, 81);
            this.txtRetries.Name = "txtRetries";
            this.txtRetries.Size = new System.Drawing.Size(175, 20);
            this.txtRetries.TabIndex = 89;
            this.txtRetries.Tag = "Retries";
            // 
            // lblTO
            // 
            this.lblTO.AutoSize = true;
            this.lblTO.Location = new System.Drawing.Point(16, 109);
            this.lblTO.Name = "lblTO";
            this.lblTO.Size = new System.Drawing.Size(79, 13);
            this.lblTO.TabIndex = 90;
            this.lblTO.Text = "Timeout (msec)";
            // 
            // txtTimeOut
            // 
            this.txtTimeOut.Location = new System.Drawing.Point(164, 106);
            this.txtTimeOut.Name = "txtTimeOut";
            this.txtTimeOut.Size = new System.Drawing.Size(176, 20);
            this.txtTimeOut.TabIndex = 91;
            this.txtTimeOut.Tag = "TimeOutMS";
            // 
            // lblUI
            // 
            this.lblUI.AutoSize = true;
            this.lblUI.Location = new System.Drawing.Point(16, 34);
            this.lblUI.Name = "lblUI";
            this.lblUI.Size = new System.Drawing.Size(40, 13);
            this.lblUI.TabIndex = 82;
            this.lblUI.Text = "Unit ID";
            // 
            // txtUnitID
            // 
            this.txtUnitID.Location = new System.Drawing.Point(164, 31);
            this.txtUnitID.Name = "txtUnitID";
            this.txtUnitID.Size = new System.Drawing.Size(175, 20);
            this.txtUnitID.TabIndex = 83;
            this.txtUnitID.Tag = "UnitID";
            // 
            // btnFirst
            // 
            this.btnFirst.FlatAppearance.BorderSize = 0;
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFirst.Location = new System.Drawing.Point(16, 445);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(58, 22);
            this.btnFirst.TabIndex = 111;
            this.btnFirst.Text = "<<First";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.FlatAppearance.BorderSize = 0;
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrev.Location = new System.Drawing.Point(104, 445);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(58, 22);
            this.btnPrev.TabIndex = 112;
            this.btnPrev.Text = "<Prev";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnNext
            // 
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(192, 445);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(58, 22);
            this.btnNext.TabIndex = 113;
            this.btnNext.Text = "Next>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.FlatAppearance.BorderSize = 0;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast.Location = new System.Drawing.Point(280, 445);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(58, 22);
            this.btnLast.TabIndex = 114;
            this.btnLast.Text = "Last>>";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // lblIOASize
            // 
            this.lblIOASize.AutoSize = true;
            this.lblIOASize.Location = new System.Drawing.Point(16, 160);
            this.lblIOASize.Name = "lblIOASize";
            this.lblIOASize.Size = new System.Drawing.Size(45, 13);
            this.lblIOASize.TabIndex = 117;
            this.lblIOASize.Text = "IOASize";
            // 
            // CmbIOA
            // 
            this.CmbIOA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbIOA.FormattingEnabled = true;
            this.CmbIOA.Location = new System.Drawing.Point(164, 157);
            this.CmbIOA.Name = "CmbIOA";
            this.CmbIOA.Size = new System.Drawing.Size(175, 21);
            this.CmbIOA.TabIndex = 122;
            this.CmbIOA.Tag = "IOASize";
            // 
            // lblSportType
            // 
            this.lblSportType.AutoSize = true;
            this.lblSportType.Location = new System.Drawing.Point(16, 134);
            this.lblSportType.Name = "lblSportType";
            this.lblSportType.Size = new System.Drawing.Size(56, 13);
            this.lblSportType.TabIndex = 123;
            this.lblSportType.Text = "SportType";
            // 
            // CmbSportType
            // 
            this.CmbSportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbSportType.FormattingEnabled = true;
            this.CmbSportType.Location = new System.Drawing.Point(165, 131);
            this.CmbSportType.Name = "CmbSportType";
            this.CmbSportType.Size = new System.Drawing.Size(175, 21);
            this.CmbSportType.TabIndex = 124;
            this.CmbSportType.Tag = "SportType";
            // 
            // lblLastAI
            // 
            this.lblLastAI.AutoSize = true;
            this.lblLastAI.Location = new System.Drawing.Point(16, 186);
            this.lblLastAI.Name = "lblLastAI";
            this.lblLastAI.Size = new System.Drawing.Size(40, 13);
            this.lblLastAI.TabIndex = 125;
            this.lblLastAI.Text = "Last AI";
            // 
            // txtLastAI
            // 
            this.txtLastAI.Location = new System.Drawing.Point(164, 183);
            this.txtLastAI.Name = "txtLastAI";
            this.txtLastAI.Size = new System.Drawing.Size(175, 20);
            this.txtLastAI.TabIndex = 126;
            this.txtLastAI.Tag = "LastAI";
            this.txtLastAI.TextChanged += new System.EventHandler(this.txtLastAI_TextChanged);
            this.txtLastAI.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLastAI_KeyPress);
            this.txtLastAI.Validated += new System.EventHandler(this.txtLastAI_Validated);
            // 
            // lblLastDI
            // 
            this.lblLastDI.AutoSize = true;
            this.lblLastDI.Location = new System.Drawing.Point(16, 211);
            this.lblLastDI.Name = "lblLastDI";
            this.lblLastDI.Size = new System.Drawing.Size(41, 13);
            this.lblLastDI.TabIndex = 127;
            this.lblLastDI.Text = "Last DI";
            // 
            // txtLastDI
            // 
            this.txtLastDI.Location = new System.Drawing.Point(164, 208);
            this.txtLastDI.Name = "txtLastDI";
            this.txtLastDI.Size = new System.Drawing.Size(175, 20);
            this.txtLastDI.TabIndex = 128;
            this.txtLastDI.Tag = "LastDI";
            this.txtLastDI.TextChanged += new System.EventHandler(this.txtLastDI_TextChanged);
            this.txtLastDI.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLastDI_KeyPress);
            // 
            // lblLastDO
            // 
            this.lblLastDO.AutoSize = true;
            this.lblLastDO.Location = new System.Drawing.Point(16, 236);
            this.lblLastDO.Name = "lblLastDO";
            this.lblLastDO.Size = new System.Drawing.Size(46, 13);
            this.lblLastDO.TabIndex = 129;
            this.lblLastDO.Text = "Last DO";
            // 
            // txtLastDO
            // 
            this.txtLastDO.Location = new System.Drawing.Point(164, 233);
            this.txtLastDO.Name = "txtLastDO";
            this.txtLastDO.Size = new System.Drawing.Size(175, 20);
            this.txtLastDO.TabIndex = 130;
            this.txtLastDO.Tag = "LastDO";
            this.txtLastDO.TextChanged += new System.EventHandler(this.txtLastDO_TextChanged);
            this.txtLastDO.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLastDO_KeyPress);
            // 
            // lblTimestampType
            // 
            this.lblTimestampType.AutoSize = true;
            this.lblTimestampType.Location = new System.Drawing.Point(16, 261);
            this.lblTimestampType.Name = "lblTimestampType";
            this.lblTimestampType.Size = new System.Drawing.Size(115, 13);
            this.lblTimestampType.TabIndex = 131;
            this.lblTimestampType.Text = "TimestampType(mSec)";
            // 
            // txtTimestampType
            // 
            this.txtTimestampType.Location = new System.Drawing.Point(164, 258);
            this.txtTimestampType.Name = "txtTimestampType";
            this.txtTimestampType.Size = new System.Drawing.Size(176, 20);
            this.txtTimestampType.TabIndex = 132;
            this.txtTimestampType.Tag = "TimestampType";
            this.txtTimestampType.TextChanged += new System.EventHandler(this.txtTimestampType_TextChanged);
            this.txtTimestampType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTimestampType_KeyPress);
            // 
            // lblTimeAccuracy
            // 
            this.lblTimeAccuracy.AutoSize = true;
            this.lblTimeAccuracy.Location = new System.Drawing.Point(16, 286);
            this.lblTimeAccuracy.Name = "lblTimeAccuracy";
            this.lblTimeAccuracy.Size = new System.Drawing.Size(136, 13);
            this.lblTimeAccuracy.TabIndex = 133;
            this.lblTimeAccuracy.Text = "TimestampAccuracy(mSec)";
            // 
            // txtTimestampAccuracy
            // 
            this.txtTimestampAccuracy.Location = new System.Drawing.Point(164, 283);
            this.txtTimestampAccuracy.Name = "txtTimestampAccuracy";
            this.txtTimestampAccuracy.Size = new System.Drawing.Size(176, 20);
            this.txtTimestampAccuracy.TabIndex = 134;
            this.txtTimestampAccuracy.Tag = "TimestampAccuracy";
            this.txtTimestampAccuracy.TextChanged += new System.EventHandler(this.txtTimestampAccuracy_TextChanged);
            this.txtTimestampAccuracy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTimestampAccuracy_KeyPress);
            // 
            // grpIED
            // 
            this.grpIED.BackColor = System.Drawing.SystemColors.Control;
            this.grpIED.Controls.Add(this.txtDescription);
            this.grpIED.Controls.Add(this.txtWindowTime);
            this.grpIED.Controls.Add(this.label14);
            this.grpIED.Controls.Add(this.lblWindowTime);
            this.grpIED.Controls.Add(this.txtPulsewidthTimeout);
            this.grpIED.Controls.Add(this.lblPulsewidthTimeout);
            this.grpIED.Controls.Add(this.txtDebounceTime);
            this.grpIED.Controls.Add(this.txtTimestampAccuracy);
            this.grpIED.Controls.Add(this.lblDebounceTime);
            this.grpIED.Controls.Add(this.lblTimeAccuracy);
            this.grpIED.Controls.Add(this.txtTimestampType);
            this.grpIED.Controls.Add(this.lblTimestampType);
            this.grpIED.Controls.Add(this.txtLastDO);
            this.grpIED.Controls.Add(this.lblLastDO);
            this.grpIED.Controls.Add(this.txtLastDI);
            this.grpIED.Controls.Add(this.lblLastDI);
            this.grpIED.Controls.Add(this.txtLastAI);
            this.grpIED.Controls.Add(this.lblLastAI);
            this.grpIED.Controls.Add(this.CmbSportType);
            this.grpIED.Controls.Add(this.lblSportType);
            this.grpIED.Controls.Add(this.CmbIOA);
            this.grpIED.Controls.Add(this.lblIOASize);
            this.grpIED.Controls.Add(this.btnLast);
            this.grpIED.Controls.Add(this.btnNext);
            this.grpIED.Controls.Add(this.btnPrev);
            this.grpIED.Controls.Add(this.btnFirst);
            this.grpIED.Controls.Add(this.txtUnitID);
            this.grpIED.Controls.Add(this.lblUI);
            this.grpIED.Controls.Add(this.txtTimeOut);
            this.grpIED.Controls.Add(this.lblTO);
            this.grpIED.Controls.Add(this.txtRetries);
            this.grpIED.Controls.Add(this.lblRetries);
            this.grpIED.Controls.Add(this.txtDevice);
            this.grpIED.Controls.Add(this.lblDevice);
            this.grpIED.Controls.Add(this.lblHdrText);
            this.grpIED.Controls.Add(this.pbHdr);
            this.grpIED.Controls.Add(this.btnCancel);
            this.grpIED.Controls.Add(this.btnDone);
            this.grpIED.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpIED.Location = new System.Drawing.Point(324, 112);
            this.grpIED.Name = "grpIED";
            this.grpIED.Size = new System.Drawing.Size(356, 477);
            this.grpIED.TabIndex = 25;
            this.grpIED.TabStop = false;
            this.grpIED.Visible = false;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(164, 383);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(176, 20);
            this.txtDescription.TabIndex = 147;
            this.txtDescription.Tag = "Description";
            this.txtDescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescription_KeyPress);
            // 
            // txtWindowTime
            // 
            this.txtWindowTime.Location = new System.Drawing.Point(164, 308);
            this.txtWindowTime.Name = "txtWindowTime";
            this.txtWindowTime.Size = new System.Drawing.Size(176, 20);
            this.txtWindowTime.TabIndex = 143;
            this.txtWindowTime.Tag = "WindowTime";
            this.txtWindowTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWindowTime_KeyPress);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(16, 386);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(60, 13);
            this.label14.TabIndex = 146;
            this.label14.Text = "Description";
            // 
            // lblWindowTime
            // 
            this.lblWindowTime.AutoSize = true;
            this.lblWindowTime.Location = new System.Drawing.Point(16, 311);
            this.lblWindowTime.Name = "lblWindowTime";
            this.lblWindowTime.Size = new System.Drawing.Size(102, 13);
            this.lblWindowTime.TabIndex = 142;
            this.lblWindowTime.Text = "WindowTime(mSec)";
            // 
            // txtPulsewidthTimeout
            // 
            this.txtPulsewidthTimeout.Location = new System.Drawing.Point(164, 358);
            this.txtPulsewidthTimeout.Name = "txtPulsewidthTimeout";
            this.txtPulsewidthTimeout.Size = new System.Drawing.Size(176, 20);
            this.txtPulsewidthTimeout.TabIndex = 145;
            this.txtPulsewidthTimeout.Tag = "PulseWidthTimeout";
            this.txtPulsewidthTimeout.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPulsewidthTimeout_KeyPress);
            // 
            // lblPulsewidthTimeout
            // 
            this.lblPulsewidthTimeout.AutoSize = true;
            this.lblPulsewidthTimeout.Location = new System.Drawing.Point(16, 361);
            this.lblPulsewidthTimeout.Name = "lblPulsewidthTimeout";
            this.lblPulsewidthTimeout.Size = new System.Drawing.Size(129, 13);
            this.lblPulsewidthTimeout.TabIndex = 144;
            this.lblPulsewidthTimeout.Text = "PulsewidthTimeout(mSec)";
            // 
            // txtDebounceTime
            // 
            this.txtDebounceTime.Location = new System.Drawing.Point(164, 333);
            this.txtDebounceTime.Name = "txtDebounceTime";
            this.txtDebounceTime.Size = new System.Drawing.Size(176, 20);
            this.txtDebounceTime.TabIndex = 143;
            this.txtDebounceTime.Tag = "DebounceTime";
            this.txtDebounceTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDebounceTime_KeyPress);
            // 
            // lblDebounceTime
            // 
            this.lblDebounceTime.AutoSize = true;
            this.lblDebounceTime.Location = new System.Drawing.Point(16, 336);
            this.lblDebounceTime.Name = "lblDebounceTime";
            this.lblDebounceTime.Size = new System.Drawing.Size(105, 13);
            this.lblDebounceTime.TabIndex = 142;
            this.lblDebounceTime.Text = "DebounceTime(Sec)";
            // 
            // ucMasterSPORT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpIED);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucMasterSPORT";
            this.Size = new System.Drawing.Size(1050, 700);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.grpIEC101.ResumeLayout(false);
            this.grpIEC101.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).EndInit();
            this.grpIED.ResumeLayout(false);
            this.grpIED.PerformLayout();
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
        public System.Windows.Forms.ListView lvIEDList;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button BtnDeleteAll;
        public System.Windows.Forms.Button btnAdd;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Button btnExportIED;
        public System.Windows.Forms.Button btnImportIED;
        public System.Windows.Forms.Label lblIED;
        public System.Windows.Forms.GroupBox grpIED;
        public System.Windows.Forms.TextBox txtDescription;
        public System.Windows.Forms.TextBox txtWindowTime;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblWindowTime;
        public System.Windows.Forms.TextBox txtPulsewidthTimeout;
        private System.Windows.Forms.Label lblPulsewidthTimeout;
        public System.Windows.Forms.TextBox txtDebounceTime;
        public System.Windows.Forms.TextBox txtTimestampAccuracy;
        private System.Windows.Forms.Label lblDebounceTime;
        private System.Windows.Forms.Label lblTimeAccuracy;
        public System.Windows.Forms.TextBox txtTimestampType;
        private System.Windows.Forms.Label lblTimestampType;
        public System.Windows.Forms.TextBox txtLastDO;
        private System.Windows.Forms.Label lblLastDO;
        public System.Windows.Forms.TextBox txtLastDI;
        private System.Windows.Forms.Label lblLastDI;
        public System.Windows.Forms.TextBox txtLastAI;
        private System.Windows.Forms.Label lblLastAI;
        public System.Windows.Forms.ComboBox CmbSportType;
        private System.Windows.Forms.Label lblSportType;
        public System.Windows.Forms.ComboBox CmbIOA;
        private System.Windows.Forms.Label lblIOASize;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnFirst;
        public System.Windows.Forms.TextBox txtUnitID;
        private System.Windows.Forms.Label lblUI;
        public System.Windows.Forms.TextBox txtTimeOut;
        private System.Windows.Forms.Label lblTO;
        public System.Windows.Forms.TextBox txtRetries;
        private System.Windows.Forms.Label lblRetries;
        public System.Windows.Forms.TextBox txtDevice;
        private System.Windows.Forms.Label lblDevice;
        private System.Windows.Forms.Label lblHdrText;
        private System.Windows.Forms.PictureBox pbHdr;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDone;
    }
}
