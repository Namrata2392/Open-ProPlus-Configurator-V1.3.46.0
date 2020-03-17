namespace OpenProPlusConfigurator
{
    partial class ucUFSMS
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
            this.grpSMS = new System.Windows.Forms.GroupBox();
            this.txtEventQSize = new System.Windows.Forms.TextBox();
            this.lblEQS = new System.Windows.Forms.Label();
            this.txtUnitID = new System.Windows.Forms.TextBox();
            this.lblUnitID = new System.Windows.Forms.Label();
            this.txtDebug = new System.Windows.Forms.TextBox();
            this.txtModem = new System.Windows.Forms.TextBox();
            this.txtPortNo = new System.Windows.Forms.TextBox();
            this.chkUserFriendlySMS = new System.Windows.Forms.CheckBox();
            this.chkEnableEncryption = new System.Windows.Forms.CheckBox();
            this.txtRemoteMobileNo = new System.Windows.Forms.TextBox();
            this.lblRemoteMobileNo = new System.Windows.Forms.Label();
            this.txtTolerancePeriodInMin = new System.Windows.Forms.TextBox();
            this.lblTolerancePeriodInMin = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFirmwareVersion = new System.Windows.Forms.TextBox();
            this.lblAFV = new System.Windows.Forms.Label();
            this.lblModem = new System.Windows.Forms.Label();
            this.lblPortNum = new System.Windows.Forms.Label();
            this.chkRun = new System.Windows.Forms.CheckBox();
            this.txtSlaveNum = new System.Windows.Forms.TextBox();
            this.lblSN = new System.Windows.Forms.Label();
            this.grpUFSMS = new System.Windows.Forms.GroupBox();
            this.chkGrantForControl = new System.Windows.Forms.CheckBox();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.txtMobileNo = new System.Windows.Forms.TextBox();
            this.lblMobileNo = new System.Windows.Forms.Label();
            this.lblHdrText = new System.Windows.Forms.Label();
            this.pbHdr = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.lvUFSMSList = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblIED = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grpSMS.SuspendLayout();
            this.grpUFSMS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.grpSMS);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grpUFSMS);
            this.splitContainer1.Panel2.Controls.Add(this.lvUFSMSList);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.lblIED);
            this.splitContainer1.Size = new System.Drawing.Size(975, 524);
            this.splitContainer1.SplitterDistance = 176;
            this.splitContainer1.TabIndex = 33;
            // 
            // grpSMS
            // 
            this.grpSMS.Controls.Add(this.txtEventQSize);
            this.grpSMS.Controls.Add(this.lblEQS);
            this.grpSMS.Controls.Add(this.txtUnitID);
            this.grpSMS.Controls.Add(this.lblUnitID);
            this.grpSMS.Controls.Add(this.txtDebug);
            this.grpSMS.Controls.Add(this.txtModem);
            this.grpSMS.Controls.Add(this.txtPortNo);
            this.grpSMS.Controls.Add(this.chkUserFriendlySMS);
            this.grpSMS.Controls.Add(this.chkEnableEncryption);
            this.grpSMS.Controls.Add(this.txtRemoteMobileNo);
            this.grpSMS.Controls.Add(this.lblRemoteMobileNo);
            this.grpSMS.Controls.Add(this.txtTolerancePeriodInMin);
            this.grpSMS.Controls.Add(this.lblTolerancePeriodInMin);
            this.grpSMS.Controls.Add(this.label2);
            this.grpSMS.Controls.Add(this.txtFirmwareVersion);
            this.grpSMS.Controls.Add(this.lblAFV);
            this.grpSMS.Controls.Add(this.lblModem);
            this.grpSMS.Controls.Add(this.lblPortNum);
            this.grpSMS.Controls.Add(this.chkRun);
            this.grpSMS.Controls.Add(this.txtSlaveNum);
            this.grpSMS.Controls.Add(this.lblSN);
            this.grpSMS.Location = new System.Drawing.Point(3, 0);
            this.grpSMS.Name = "grpSMS";
            this.grpSMS.Size = new System.Drawing.Size(503, 168);
            this.grpSMS.TabIndex = 14;
            this.grpSMS.TabStop = false;
            this.grpSMS.Text = "SMS Slave";
            // 
            // txtEventQSize
            // 
            this.txtEventQSize.Enabled = false;
            this.txtEventQSize.Location = new System.Drawing.Point(355, 47);
            this.txtEventQSize.Name = "txtEventQSize";
            this.txtEventQSize.Size = new System.Drawing.Size(131, 20);
            this.txtEventQSize.TabIndex = 173;
            this.txtEventQSize.Tag = "EventQSize";
            // 
            // lblEQS
            // 
            this.lblEQS.AutoSize = true;
            this.lblEQS.Location = new System.Drawing.Point(243, 52);
            this.lblEQS.Name = "lblEQS";
            this.lblEQS.Size = new System.Drawing.Size(93, 13);
            this.lblEQS.TabIndex = 172;
            this.lblEQS.Text = "Event Queue Size";
            // 
            // txtUnitID
            // 
            this.txtUnitID.Enabled = false;
            this.txtUnitID.Location = new System.Drawing.Point(355, 23);
            this.txtUnitID.Name = "txtUnitID";
            this.txtUnitID.Size = new System.Drawing.Size(131, 20);
            this.txtUnitID.TabIndex = 171;
            this.txtUnitID.Tag = "UnitID";
            // 
            // lblUnitID
            // 
            this.lblUnitID.AutoSize = true;
            this.lblUnitID.Location = new System.Drawing.Point(243, 28);
            this.lblUnitID.Name = "lblUnitID";
            this.lblUnitID.Size = new System.Drawing.Size(37, 13);
            this.lblUnitID.TabIndex = 170;
            this.lblUnitID.Text = "UnitID";
            // 
            // txtDebug
            // 
            this.txtDebug.Enabled = false;
            this.txtDebug.Location = new System.Drawing.Point(97, 121);
            this.txtDebug.Name = "txtDebug";
            this.txtDebug.Size = new System.Drawing.Size(132, 20);
            this.txtDebug.TabIndex = 155;
            this.txtDebug.Tag = "SlaveNum";
            // 
            // txtModem
            // 
            this.txtModem.Enabled = false;
            this.txtModem.Location = new System.Drawing.Point(97, 73);
            this.txtModem.Name = "txtModem";
            this.txtModem.Size = new System.Drawing.Size(132, 20);
            this.txtModem.TabIndex = 155;
            this.txtModem.Tag = "SlaveNum";
            // 
            // txtPortNo
            // 
            this.txtPortNo.Enabled = false;
            this.txtPortNo.Location = new System.Drawing.Point(97, 49);
            this.txtPortNo.Name = "txtPortNo";
            this.txtPortNo.Size = new System.Drawing.Size(131, 20);
            this.txtPortNo.TabIndex = 155;
            this.txtPortNo.Tag = "SlaveNum";
            // 
            // chkUserFriendlySMS
            // 
            this.chkUserFriendlySMS.AutoSize = true;
            this.chkUserFriendlySMS.Enabled = false;
            this.chkUserFriendlySMS.Location = new System.Drawing.Point(243, 123);
            this.chkUserFriendlySMS.Name = "chkUserFriendlySMS";
            this.chkUserFriendlySMS.Size = new System.Drawing.Size(107, 17);
            this.chkUserFriendlySMS.TabIndex = 168;
            this.chkUserFriendlySMS.Tag = "UserFriendlySMS_YES_NO";
            this.chkUserFriendlySMS.Text = "UserFriendlySMS";
            this.chkUserFriendlySMS.UseVisualStyleBackColor = true;
            // 
            // chkEnableEncryption
            // 
            this.chkEnableEncryption.AutoSize = true;
            this.chkEnableEncryption.Enabled = false;
            this.chkEnableEncryption.Location = new System.Drawing.Point(377, 123);
            this.chkEnableEncryption.Name = "chkEnableEncryption";
            this.chkEnableEncryption.Size = new System.Drawing.Size(109, 17);
            this.chkEnableEncryption.TabIndex = 167;
            this.chkEnableEncryption.Tag = "EnableEncryption_YES_NO";
            this.chkEnableEncryption.Text = "EnableEncryption";
            this.chkEnableEncryption.UseVisualStyleBackColor = true;
            // 
            // txtRemoteMobileNo
            // 
            this.txtRemoteMobileNo.Enabled = false;
            this.txtRemoteMobileNo.Location = new System.Drawing.Point(97, 97);
            this.txtRemoteMobileNo.Name = "txtRemoteMobileNo";
            this.txtRemoteMobileNo.Size = new System.Drawing.Size(132, 20);
            this.txtRemoteMobileNo.TabIndex = 166;
            this.txtRemoteMobileNo.Tag = "RemoteMobileNo";
            // 
            // lblRemoteMobileNo
            // 
            this.lblRemoteMobileNo.AutoSize = true;
            this.lblRemoteMobileNo.Location = new System.Drawing.Point(6, 100);
            this.lblRemoteMobileNo.Name = "lblRemoteMobileNo";
            this.lblRemoteMobileNo.Size = new System.Drawing.Size(89, 13);
            this.lblRemoteMobileNo.TabIndex = 165;
            this.lblRemoteMobileNo.Text = "RemoteMobileNo";
            // 
            // txtTolerancePeriodInMin
            // 
            this.txtTolerancePeriodInMin.Enabled = false;
            this.txtTolerancePeriodInMin.Location = new System.Drawing.Point(355, 71);
            this.txtTolerancePeriodInMin.Name = "txtTolerancePeriodInMin";
            this.txtTolerancePeriodInMin.Size = new System.Drawing.Size(131, 20);
            this.txtTolerancePeriodInMin.TabIndex = 164;
            this.txtTolerancePeriodInMin.Tag = "TolerancePeriodInMin";
            // 
            // lblTolerancePeriodInMin
            // 
            this.lblTolerancePeriodInMin.AutoSize = true;
            this.lblTolerancePeriodInMin.Location = new System.Drawing.Point(243, 76);
            this.lblTolerancePeriodInMin.Name = "lblTolerancePeriodInMin";
            this.lblTolerancePeriodInMin.Size = new System.Drawing.Size(111, 13);
            this.lblTolerancePeriodInMin.TabIndex = 163;
            this.lblTolerancePeriodInMin.Text = "TolerancePeriodInMin";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 161;
            this.label2.Text = "Debug Level";
            // 
            // txtFirmwareVersion
            // 
            this.txtFirmwareVersion.Enabled = false;
            this.txtFirmwareVersion.Location = new System.Drawing.Point(355, 95);
            this.txtFirmwareVersion.Name = "txtFirmwareVersion";
            this.txtFirmwareVersion.Size = new System.Drawing.Size(131, 20);
            this.txtFirmwareVersion.TabIndex = 160;
            this.txtFirmwareVersion.Tag = "AppFirmwareVersion";
            // 
            // lblAFV
            // 
            this.lblAFV.AutoSize = true;
            this.lblAFV.Location = new System.Drawing.Point(243, 100);
            this.lblAFV.Name = "lblAFV";
            this.lblAFV.Size = new System.Drawing.Size(87, 13);
            this.lblAFV.TabIndex = 159;
            this.lblAFV.Text = "Firmware Version";
            // 
            // lblModem
            // 
            this.lblModem.AutoSize = true;
            this.lblModem.Location = new System.Drawing.Point(6, 76);
            this.lblModem.Name = "lblModem";
            this.lblModem.Size = new System.Drawing.Size(42, 13);
            this.lblModem.TabIndex = 157;
            this.lblModem.Text = "Modem";
            // 
            // lblPortNum
            // 
            this.lblPortNum.AutoSize = true;
            this.lblPortNum.Location = new System.Drawing.Point(6, 52);
            this.lblPortNum.Name = "lblPortNum";
            this.lblPortNum.Size = new System.Drawing.Size(46, 13);
            this.lblPortNum.TabIndex = 156;
            this.lblPortNum.Text = "Port No.";
            // 
            // chkRun
            // 
            this.chkRun.AutoSize = true;
            this.chkRun.Enabled = false;
            this.chkRun.Location = new System.Drawing.Point(9, 144);
            this.chkRun.Name = "chkRun";
            this.chkRun.Size = new System.Drawing.Size(46, 17);
            this.chkRun.TabIndex = 155;
            this.chkRun.Tag = "Run_YES_NO";
            this.chkRun.Text = "Run";
            this.chkRun.UseVisualStyleBackColor = true;
            // 
            // txtSlaveNum
            // 
            this.txtSlaveNum.Enabled = false;
            this.txtSlaveNum.Location = new System.Drawing.Point(97, 25);
            this.txtSlaveNum.Name = "txtSlaveNum";
            this.txtSlaveNum.Size = new System.Drawing.Size(131, 20);
            this.txtSlaveNum.TabIndex = 154;
            this.txtSlaveNum.Tag = "SlaveNum";
            // 
            // lblSN
            // 
            this.lblSN.AutoSize = true;
            this.lblSN.Location = new System.Drawing.Point(6, 28);
            this.lblSN.Name = "lblSN";
            this.lblSN.Size = new System.Drawing.Size(54, 13);
            this.lblSN.TabIndex = 153;
            this.lblSN.Text = "Slave No.";
            // 
            // grpUFSMS
            // 
            this.grpUFSMS.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.grpUFSMS.Controls.Add(this.chkGrantForControl);
            this.grpUFSMS.Controls.Add(this.btnLast);
            this.grpUFSMS.Controls.Add(this.btnNext);
            this.grpUFSMS.Controls.Add(this.btnPrev);
            this.grpUFSMS.Controls.Add(this.btnFirst);
            this.grpUFSMS.Controls.Add(this.txtMobileNo);
            this.grpUFSMS.Controls.Add(this.lblMobileNo);
            this.grpUFSMS.Controls.Add(this.lblHdrText);
            this.grpUFSMS.Controls.Add(this.pbHdr);
            this.grpUFSMS.Controls.Add(this.btnCancel);
            this.grpUFSMS.Controls.Add(this.btnDone);
            this.grpUFSMS.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpUFSMS.Location = new System.Drawing.Point(320, 35);
            this.grpUFSMS.Name = "grpUFSMS";
            this.grpUFSMS.Size = new System.Drawing.Size(272, 146);
            this.grpUFSMS.TabIndex = 29;
            this.grpUFSMS.TabStop = false;
            this.grpUFSMS.Visible = false;
            // 
            // chkGrantForControl
            // 
            this.chkGrantForControl.AutoSize = true;
            this.chkGrantForControl.Location = new System.Drawing.Point(74, 60);
            this.chkGrantForControl.Name = "chkGrantForControl";
            this.chkGrantForControl.Size = new System.Drawing.Size(100, 17);
            this.chkGrantForControl.TabIndex = 122;
            this.chkGrantForControl.Tag = "GrantForControl_YES_NO";
            this.chkGrantForControl.Text = "GrantForControl";
            this.chkGrantForControl.UseVisualStyleBackColor = true;
            // 
            // btnLast
            // 
            this.btnLast.FlatAppearance.BorderSize = 0;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast.Location = new System.Drawing.Point(217, 116);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(59, 22);
            this.btnLast.TabIndex = 80;
            this.btnLast.Text = "Last>>";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(148, 116);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(59, 22);
            this.btnNext.TabIndex = 79;
            this.btnNext.Text = "Next>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.FlatAppearance.BorderSize = 0;
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrev.Location = new System.Drawing.Point(79, 116);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(59, 22);
            this.btnPrev.TabIndex = 78;
            this.btnPrev.Text = "<Prev";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.FlatAppearance.BorderSize = 0;
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFirst.Location = new System.Drawing.Point(10, 116);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(59, 22);
            this.btnFirst.TabIndex = 77;
            this.btnFirst.Text = "<<First";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // txtMobileNo
            // 
            this.txtMobileNo.Location = new System.Drawing.Point(74, 31);
            this.txtMobileNo.Name = "txtMobileNo";
            this.txtMobileNo.Size = new System.Drawing.Size(183, 20);
            this.txtMobileNo.TabIndex = 57;
            this.txtMobileNo.Tag = "MobileNo";
            // 
            // lblMobileNo
            // 
            this.lblMobileNo.AutoSize = true;
            this.lblMobileNo.Location = new System.Drawing.Point(10, 34);
            this.lblMobileNo.Name = "lblMobileNo";
            this.lblMobileNo.Size = new System.Drawing.Size(58, 13);
            this.lblMobileNo.TabIndex = 56;
            this.lblMobileNo.Text = "Mobile No.";
            // 
            // lblHdrText
            // 
            this.lblHdrText.AutoSize = true;
            this.lblHdrText.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblHdrText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHdrText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHdrText.Location = new System.Drawing.Point(10, 4);
            this.lblHdrText.Name = "lblHdrText";
            this.lblHdrText.Size = new System.Drawing.Size(49, 13);
            this.lblHdrText.TabIndex = 40;
            this.lblHdrText.Text = "UFSMS";
            this.lblHdrText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseDown);
            this.lblHdrText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseMove);
            // 
            // pbHdr
            // 
            this.pbHdr.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pbHdr.Location = new System.Drawing.Point(0, 0);
            this.pbHdr.Name = "pbHdr";
            this.pbHdr.Size = new System.Drawing.Size(272, 22);
            this.pbHdr.TabIndex = 39;
            this.pbHdr.TabStop = false;
            this.pbHdr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseDown);
            this.pbHdr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseMove);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(189, 80);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 28);
            this.btnCancel.TabIndex = 76;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDone
            // 
            this.btnDone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDone.Location = new System.Drawing.Point(74, 80);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(68, 28);
            this.btnDone.TabIndex = 75;
            this.btnDone.Text = "&Update";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // lvUFSMSList
            // 
            this.lvUFSMSList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvUFSMSList.CheckBoxes = true;
            this.lvUFSMSList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvUFSMSList.FullRowSelect = true;
            this.lvUFSMSList.Location = new System.Drawing.Point(0, 13);
            this.lvUFSMSList.MultiSelect = false;
            this.lvUFSMSList.Name = "lvUFSMSList";
            this.lvUFSMSList.Size = new System.Drawing.Size(975, 286);
            this.lvUFSMSList.TabIndex = 11;
            this.lvUFSMSList.UseCompatibleStateImageBehavior = false;
            this.lvUFSMSList.View = System.Windows.Forms.View.Details;
            this.lvUFSMSList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvUFSMSList_ItemCheck);
            this.lvUFSMSList.DoubleClick += new System.EventHandler(this.lvUFSMSList_DoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 299);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(975, 45);
            this.panel1.TabIndex = 28;
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Location = new System.Drawing.Point(4, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(68, 28);
            this.btnAdd.TabIndex = 28;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(84, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(68, 28);
            this.btnDelete.TabIndex = 29;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblIED
            // 
            this.lblIED.AutoSize = true;
            this.lblIED.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblIED.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIED.Location = new System.Drawing.Point(0, 0);
            this.lblIED.Name = "lblIED";
            this.lblIED.Size = new System.Drawing.Size(73, 13);
            this.lblIED.TabIndex = 10;
            this.lblIED.Text = "UFSMS List";
            // 
            // ucUFSMS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucUFSMS";
            this.Size = new System.Drawing.Size(975, 524);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.grpSMS.ResumeLayout(false);
            this.grpSMS.PerformLayout();
            this.grpUFSMS.ResumeLayout(false);
            this.grpUFSMS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox grpSMS;
        public System.Windows.Forms.ListView lvUFSMSList;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button btnAdd;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Label lblIED;
        public System.Windows.Forms.CheckBox chkUserFriendlySMS;
        public System.Windows.Forms.CheckBox chkEnableEncryption;
        public System.Windows.Forms.TextBox txtRemoteMobileNo;
        private System.Windows.Forms.Label lblRemoteMobileNo;
        public System.Windows.Forms.TextBox txtTolerancePeriodInMin;
        private System.Windows.Forms.Label lblTolerancePeriodInMin;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtFirmwareVersion;
        private System.Windows.Forms.Label lblAFV;
        private System.Windows.Forms.Label lblModem;
        private System.Windows.Forms.Label lblPortNum;
        public System.Windows.Forms.CheckBox chkRun;
        public System.Windows.Forms.TextBox txtSlaveNum;
        private System.Windows.Forms.Label lblSN;
        public System.Windows.Forms.TextBox txtDebug;
        public System.Windows.Forms.TextBox txtModem;
        public System.Windows.Forms.TextBox txtPortNo;
        public System.Windows.Forms.GroupBox grpUFSMS;
        public System.Windows.Forms.CheckBox chkGrantForControl;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnFirst;
        public System.Windows.Forms.TextBox txtMobileNo;
        private System.Windows.Forms.Label lblMobileNo;
        private System.Windows.Forms.Label lblHdrText;
        private System.Windows.Forms.PictureBox pbHdr;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDone;
        public System.Windows.Forms.TextBox txtUnitID;
        public System.Windows.Forms.Label lblUnitID;
        public System.Windows.Forms.TextBox txtEventQSize;
        public System.Windows.Forms.Label lblEQS;
    }
}
