namespace OpenProPlusConfigurator
{
    partial class ucGroupSMSSlave
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucGroupSMSSlave));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMSC = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnExportINI = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lvSMSSlave = new System.Windows.Forms.ListView();
            this.grpSMSSlave = new System.Windows.Forms.GroupBox();
            this.txtEventQSize = new System.Windows.Forms.TextBox();
            this.lblEQS = new System.Windows.Forms.Label();
            this.txtUnitID = new System.Windows.Forms.TextBox();
            this.lblUnitID = new System.Windows.Forms.Label();
            this.cmbPortNo = new System.Windows.Forms.ComboBox();
            this.chkUserFriendlySMS = new System.Windows.Forms.CheckBox();
            this.chkEnableEncryption = new System.Windows.Forms.CheckBox();
            this.txtRemoteMobileNo = new System.Windows.Forms.TextBox();
            this.lblRemoteMobileNo = new System.Windows.Forms.Label();
            this.txtTolerancePeriodInMin = new System.Windows.Forms.TextBox();
            this.lblTolerancePeriodInMin = new System.Windows.Forms.Label();
            this.cmbDebug = new System.Windows.Forms.ComboBox();
            this.lblDebug = new System.Windows.Forms.Label();
            this.txtFirmwareVersion = new System.Windows.Forms.TextBox();
            this.lblAFV = new System.Windows.Forms.Label();
            this.CmbModem = new System.Windows.Forms.ComboBox();
            this.lblModem = new System.Windows.Forms.Label();
            this.lblPortNum = new System.Windows.Forms.Label();
            this.chkRun = new System.Windows.Forms.CheckBox();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.txtSlaveNum = new System.Windows.Forms.TextBox();
            this.lblSN = new System.Windows.Forms.Label();
            this.lblHdrText = new System.Windows.Forms.Label();
            this.pbHdr = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.TpBtn = new System.Windows.Forms.ToolTip(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.grpSMSSlave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblMSC);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1026, 30);
            this.panel1.TabIndex = 23;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // lblMSC
            // 
            this.lblMSC.AutoSize = true;
            this.lblMSC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMSC.Location = new System.Drawing.Point(3, 3);
            this.lblMSC.Name = "lblMSC";
            this.lblMSC.Size = new System.Drawing.Size(170, 15);
            this.lblMSC.TabIndex = 4;
            this.lblMSC.Text = "SMS Slave Configuration:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnExportINI);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 775);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1026, 50);
            this.panel2.TabIndex = 24;
            // 
            // btnExportINI
            // 
            this.btnExportINI.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExportINI.FlatAppearance.BorderSize = 0;
            this.btnExportINI.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExportINI.Location = new System.Drawing.Point(160, 9);
            this.btnExportINI.Name = "btnExportINI";
            this.btnExportINI.Size = new System.Drawing.Size(68, 28);
            this.btnExportINI.TabIndex = 21;
            this.btnExportINI.Text = "E&xport INI";
            this.btnExportINI.UseVisualStyleBackColor = true;
            this.btnExportINI.Visible = false;
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Location = new System.Drawing.Point(3, 9);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(68, 28);
            this.btnAdd.TabIndex = 19;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(80, 9);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(68, 28);
            this.btnDelete.TabIndex = 20;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lvSMSSlave
            // 
            this.lvSMSSlave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvSMSSlave.CheckBoxes = true;
            this.lvSMSSlave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSMSSlave.FullRowSelect = true;
            this.lvSMSSlave.Location = new System.Drawing.Point(0, 30);
            this.lvSMSSlave.MultiSelect = false;
            this.lvSMSSlave.Name = "lvSMSSlave";
            this.lvSMSSlave.Size = new System.Drawing.Size(1026, 745);
            this.lvSMSSlave.TabIndex = 25;
            this.lvSMSSlave.UseCompatibleStateImageBehavior = false;
            this.lvSMSSlave.View = System.Windows.Forms.View.Details;
            this.lvSMSSlave.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvSMSSlave_ItemCheck);
            this.lvSMSSlave.SelectedIndexChanged += new System.EventHandler(this.lvSMSSlave_SelectedIndexChanged);
            this.lvSMSSlave.DoubleClick += new System.EventHandler(this.lvSMSSlave_DoubleClick);
            // 
            // grpSMSSlave
            // 
            this.grpSMSSlave.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.grpSMSSlave.Controls.Add(this.txtEventQSize);
            this.grpSMSSlave.Controls.Add(this.lblEQS);
            this.grpSMSSlave.Controls.Add(this.txtUnitID);
            this.grpSMSSlave.Controls.Add(this.lblUnitID);
            this.grpSMSSlave.Controls.Add(this.cmbPortNo);
            this.grpSMSSlave.Controls.Add(this.chkUserFriendlySMS);
            this.grpSMSSlave.Controls.Add(this.chkEnableEncryption);
            this.grpSMSSlave.Controls.Add(this.txtRemoteMobileNo);
            this.grpSMSSlave.Controls.Add(this.lblRemoteMobileNo);
            this.grpSMSSlave.Controls.Add(this.txtTolerancePeriodInMin);
            this.grpSMSSlave.Controls.Add(this.lblTolerancePeriodInMin);
            this.grpSMSSlave.Controls.Add(this.cmbDebug);
            this.grpSMSSlave.Controls.Add(this.lblDebug);
            this.grpSMSSlave.Controls.Add(this.txtFirmwareVersion);
            this.grpSMSSlave.Controls.Add(this.lblAFV);
            this.grpSMSSlave.Controls.Add(this.CmbModem);
            this.grpSMSSlave.Controls.Add(this.lblModem);
            this.grpSMSSlave.Controls.Add(this.lblPortNum);
            this.grpSMSSlave.Controls.Add(this.chkRun);
            this.grpSMSSlave.Controls.Add(this.btnLast);
            this.grpSMSSlave.Controls.Add(this.btnNext);
            this.grpSMSSlave.Controls.Add(this.btnPrev);
            this.grpSMSSlave.Controls.Add(this.btnFirst);
            this.grpSMSSlave.Controls.Add(this.txtSlaveNum);
            this.grpSMSSlave.Controls.Add(this.lblSN);
            this.grpSMSSlave.Controls.Add(this.lblHdrText);
            this.grpSMSSlave.Controls.Add(this.pbHdr);
            this.grpSMSSlave.Controls.Add(this.btnCancel);
            this.grpSMSSlave.Controls.Add(this.btnDone);
            this.grpSMSSlave.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpSMSSlave.Location = new System.Drawing.Point(333, 73);
            this.grpSMSSlave.Name = "grpSMSSlave";
            this.grpSMSSlave.Size = new System.Drawing.Size(351, 350);
            this.grpSMSSlave.TabIndex = 26;
            this.grpSMSSlave.TabStop = false;
            this.grpSMSSlave.Visible = false;
            this.grpSMSSlave.Enter += new System.EventHandler(this.grpSMSSlave_Enter);
            // 
            // txtEventQSize
            // 
            this.txtEventQSize.Location = new System.Drawing.Point(122, 107);
            this.txtEventQSize.Name = "txtEventQSize";
            this.txtEventQSize.Size = new System.Drawing.Size(211, 20);
            this.txtEventQSize.TabIndex = 171;
            this.txtEventQSize.Tag = "EventQSize";
            // 
            // lblEQS
            // 
            this.lblEQS.AutoSize = true;
            this.lblEQS.Location = new System.Drawing.Point(12, 110);
            this.lblEQS.Name = "lblEQS";
            this.lblEQS.Size = new System.Drawing.Size(93, 13);
            this.lblEQS.TabIndex = 170;
            this.lblEQS.Text = "Event Queue Size";
            // 
            // txtUnitID
            // 
            this.txtUnitID.Location = new System.Drawing.Point(122, 56);
            this.txtUnitID.Name = "txtUnitID";
            this.txtUnitID.Size = new System.Drawing.Size(211, 20);
            this.txtUnitID.TabIndex = 169;
            this.txtUnitID.Tag = "UnitID";
            // 
            // lblUnitID
            // 
            this.lblUnitID.AutoSize = true;
            this.lblUnitID.Location = new System.Drawing.Point(12, 59);
            this.lblUnitID.Name = "lblUnitID";
            this.lblUnitID.Size = new System.Drawing.Size(37, 13);
            this.lblUnitID.TabIndex = 168;
            this.lblUnitID.Text = "UnitID";
            // 
            // cmbPortNo
            // 
            this.cmbPortNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPortNo.FormattingEnabled = true;
            this.cmbPortNo.Location = new System.Drawing.Point(122, 81);
            this.cmbPortNo.Name = "cmbPortNo";
            this.cmbPortNo.Size = new System.Drawing.Size(211, 21);
            this.cmbPortNo.TabIndex = 152;
            this.cmbPortNo.Tag = "PortNum";
            // 
            // chkUserFriendlySMS
            // 
            this.chkUserFriendlySMS.AutoSize = true;
            this.chkUserFriendlySMS.Location = new System.Drawing.Point(237, 262);
            this.chkUserFriendlySMS.Name = "chkUserFriendlySMS";
            this.chkUserFriendlySMS.Size = new System.Drawing.Size(107, 17);
            this.chkUserFriendlySMS.TabIndex = 151;
            this.chkUserFriendlySMS.Tag = "UserFriendlySMS_YES_NO";
            this.chkUserFriendlySMS.Text = "UserFriendlySMS";
            this.chkUserFriendlySMS.UseVisualStyleBackColor = true;
            this.chkUserFriendlySMS.CheckedChanged += new System.EventHandler(this.chkUserFriendlySMS_CheckedChanged);
            this.chkUserFriendlySMS.CheckStateChanged += new System.EventHandler(this.chkUserFriendlySMS_CheckStateChanged);
            // 
            // chkEnableEncryption
            // 
            this.chkEnableEncryption.AutoSize = true;
            this.chkEnableEncryption.Location = new System.Drawing.Point(122, 264);
            this.chkEnableEncryption.Name = "chkEnableEncryption";
            this.chkEnableEncryption.Size = new System.Drawing.Size(109, 17);
            this.chkEnableEncryption.TabIndex = 150;
            this.chkEnableEncryption.Tag = "EnableEncryption_YES_NO";
            this.chkEnableEncryption.Text = "EnableEncryption";
            this.chkEnableEncryption.UseVisualStyleBackColor = true;
            // 
            // txtRemoteMobileNo
            // 
            this.txtRemoteMobileNo.Location = new System.Drawing.Point(122, 183);
            this.txtRemoteMobileNo.Name = "txtRemoteMobileNo";
            this.txtRemoteMobileNo.Size = new System.Drawing.Size(211, 20);
            this.txtRemoteMobileNo.TabIndex = 149;
            this.txtRemoteMobileNo.Tag = "RemoteMobileNo";
            this.txtRemoteMobileNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRemoteMobileNo_KeyPress);
            this.txtRemoteMobileNo.Validating += new System.ComponentModel.CancelEventHandler(this.txtRemoteMobileNo_Validating);
            // 
            // lblRemoteMobileNo
            // 
            this.lblRemoteMobileNo.AutoSize = true;
            this.lblRemoteMobileNo.Location = new System.Drawing.Point(12, 186);
            this.lblRemoteMobileNo.Name = "lblRemoteMobileNo";
            this.lblRemoteMobileNo.Size = new System.Drawing.Size(89, 13);
            this.lblRemoteMobileNo.TabIndex = 148;
            this.lblRemoteMobileNo.Text = "RemoteMobileNo";
            // 
            // txtTolerancePeriodInMin
            // 
            this.txtTolerancePeriodInMin.Location = new System.Drawing.Point(122, 158);
            this.txtTolerancePeriodInMin.Name = "txtTolerancePeriodInMin";
            this.txtTolerancePeriodInMin.Size = new System.Drawing.Size(211, 20);
            this.txtTolerancePeriodInMin.TabIndex = 145;
            this.txtTolerancePeriodInMin.Tag = "TolerancePeriodInMin";
            // 
            // lblTolerancePeriodInMin
            // 
            this.lblTolerancePeriodInMin.AutoSize = true;
            this.lblTolerancePeriodInMin.Location = new System.Drawing.Point(12, 161);
            this.lblTolerancePeriodInMin.Name = "lblTolerancePeriodInMin";
            this.lblTolerancePeriodInMin.Size = new System.Drawing.Size(111, 13);
            this.lblTolerancePeriodInMin.TabIndex = 144;
            this.lblTolerancePeriodInMin.Text = "TolerancePeriodInMin";
            // 
            // cmbDebug
            // 
            this.cmbDebug.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDebug.FormattingEnabled = true;
            this.cmbDebug.Location = new System.Drawing.Point(122, 233);
            this.cmbDebug.Name = "cmbDebug";
            this.cmbDebug.Size = new System.Drawing.Size(211, 21);
            this.cmbDebug.TabIndex = 143;
            this.cmbDebug.Tag = "DEBUG";
            // 
            // lblDebug
            // 
            this.lblDebug.AutoSize = true;
            this.lblDebug.Location = new System.Drawing.Point(12, 236);
            this.lblDebug.Name = "lblDebug";
            this.lblDebug.Size = new System.Drawing.Size(68, 13);
            this.lblDebug.TabIndex = 142;
            this.lblDebug.Text = "Debug Level";
            // 
            // txtFirmwareVersion
            // 
            this.txtFirmwareVersion.Enabled = false;
            this.txtFirmwareVersion.Location = new System.Drawing.Point(122, 208);
            this.txtFirmwareVersion.Name = "txtFirmwareVersion";
            this.txtFirmwareVersion.Size = new System.Drawing.Size(211, 20);
            this.txtFirmwareVersion.TabIndex = 141;
            this.txtFirmwareVersion.Tag = "AppFirmwareVersion";
            // 
            // lblAFV
            // 
            this.lblAFV.AutoSize = true;
            this.lblAFV.Location = new System.Drawing.Point(12, 211);
            this.lblAFV.Name = "lblAFV";
            this.lblAFV.Size = new System.Drawing.Size(87, 13);
            this.lblAFV.TabIndex = 140;
            this.lblAFV.Text = "Firmware Version";
            // 
            // CmbModem
            // 
            this.CmbModem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbModem.FormattingEnabled = true;
            this.CmbModem.Location = new System.Drawing.Point(122, 132);
            this.CmbModem.Name = "CmbModem";
            this.CmbModem.Size = new System.Drawing.Size(211, 21);
            this.CmbModem.TabIndex = 138;
            this.CmbModem.Tag = "Modem";
            // 
            // lblModem
            // 
            this.lblModem.AutoSize = true;
            this.lblModem.Location = new System.Drawing.Point(12, 135);
            this.lblModem.Name = "lblModem";
            this.lblModem.Size = new System.Drawing.Size(42, 13);
            this.lblModem.TabIndex = 137;
            this.lblModem.Text = "Modem";
            // 
            // lblPortNum
            // 
            this.lblPortNum.AutoSize = true;
            this.lblPortNum.Location = new System.Drawing.Point(12, 84);
            this.lblPortNum.Name = "lblPortNum";
            this.lblPortNum.Size = new System.Drawing.Size(46, 13);
            this.lblPortNum.TabIndex = 135;
            this.lblPortNum.Text = "Port No.";
            // 
            // chkRun
            // 
            this.chkRun.AutoSize = true;
            this.chkRun.Location = new System.Drawing.Point(15, 264);
            this.chkRun.Name = "chkRun";
            this.chkRun.Size = new System.Drawing.Size(46, 17);
            this.chkRun.TabIndex = 121;
            this.chkRun.Tag = "Run_YES_NO";
            this.chkRun.Text = "Run";
            this.chkRun.UseVisualStyleBackColor = true;
            // 
            // btnLast
            // 
            this.btnLast.FlatAppearance.BorderSize = 0;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast.Location = new System.Drawing.Point(255, 318);
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
            this.btnNext.Location = new System.Drawing.Point(175, 318);
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
            this.btnPrev.Location = new System.Drawing.Point(95, 318);
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
            this.btnFirst.Location = new System.Drawing.Point(15, 318);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(59, 22);
            this.btnFirst.TabIndex = 77;
            this.btnFirst.Text = "<<First";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // txtSlaveNum
            // 
            this.txtSlaveNum.Enabled = false;
            this.txtSlaveNum.Location = new System.Drawing.Point(122, 31);
            this.txtSlaveNum.Name = "txtSlaveNum";
            this.txtSlaveNum.Size = new System.Drawing.Size(211, 20);
            this.txtSlaveNum.TabIndex = 57;
            this.txtSlaveNum.Tag = "SlaveNum";
            // 
            // lblSN
            // 
            this.lblSN.AutoSize = true;
            this.lblSN.Location = new System.Drawing.Point(12, 34);
            this.lblSN.Name = "lblSN";
            this.lblSN.Size = new System.Drawing.Size(54, 13);
            this.lblSN.TabIndex = 56;
            this.lblSN.Text = "Slave No.";
            this.lblSN.Click += new System.EventHandler(this.lblSN_Click);
            // 
            // lblHdrText
            // 
            this.lblHdrText.AutoSize = true;
            this.lblHdrText.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblHdrText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHdrText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHdrText.Location = new System.Drawing.Point(10, 4);
            this.lblHdrText.Name = "lblHdrText";
            this.lblHdrText.Size = new System.Drawing.Size(69, 13);
            this.lblHdrText.TabIndex = 40;
            this.lblHdrText.Text = "SMS Slave";
            this.lblHdrText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseDown);
            this.lblHdrText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseMove);
            // 
            // pbHdr
            // 
            this.pbHdr.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pbHdr.Location = new System.Drawing.Point(0, 0);
            this.pbHdr.Name = "pbHdr";
            this.pbHdr.Size = new System.Drawing.Size(444, 22);
            this.pbHdr.TabIndex = 39;
            this.pbHdr.TabStop = false;
            this.pbHdr.Click += new System.EventHandler(this.pbHdr_Click);
            this.pbHdr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseDown);
            this.pbHdr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseMove);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(238, 284);
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
            this.btnDone.Location = new System.Drawing.Point(122, 284);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(68, 28);
            this.btnDone.TabIndex = 75;
            this.btnDone.Text = "&Update";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.Icon = ((System.Drawing.Icon)(resources.GetObject("errorProvider1.Icon")));
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            this.errorProvider2.Icon = ((System.Drawing.Icon)(resources.GetObject("errorProvider2.Icon")));
            // 
            // errorProvider3
            // 
            this.errorProvider3.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider3.ContainerControl = this;
            this.errorProvider3.Icon = ((System.Drawing.Icon)(resources.GetObject("errorProvider3.Icon")));
            // 
            // ucGroupSMSSlave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpSMSSlave);
            this.Controls.Add(this.lvSMSSlave);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ucGroupSMSSlave";
            this.Size = new System.Drawing.Size(1026, 825);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.grpSMSSlave.ResumeLayout(false);
            this.grpSMSSlave.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMSC;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Button btnExportINI;
        public System.Windows.Forms.Button btnAdd;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.ListView lvSMSSlave;
        public System.Windows.Forms.GroupBox grpSMSSlave;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnFirst;
        public System.Windows.Forms.TextBox txtSlaveNum;
        private System.Windows.Forms.Label lblHdrText;
        private System.Windows.Forms.PictureBox pbHdr;
        private System.Windows.Forms.ToolTip TpBtn;
        public System.Windows.Forms.CheckBox chkRun;
        public System.Windows.Forms.ComboBox CmbModem;
        public System.Windows.Forms.ComboBox cmbDebug;
        public System.Windows.Forms.TextBox txtFirmwareVersion;
        public System.Windows.Forms.CheckBox chkUserFriendlySMS;
        public System.Windows.Forms.CheckBox chkEnableEncryption;
        public System.Windows.Forms.TextBox txtRemoteMobileNo;
        public System.Windows.Forms.TextBox txtTolerancePeriodInMin;
        public System.Windows.Forms.ComboBox cmbPortNo;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        public System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button btnDone;
        public System.Windows.Forms.Label lblDebug;
        public System.Windows.Forms.Label lblAFV;
        public System.Windows.Forms.Label lblSN;
        public System.Windows.Forms.Label lblModem;
        public System.Windows.Forms.Label lblPortNum;
        public System.Windows.Forms.Label lblRemoteMobileNo;
        public System.Windows.Forms.Label lblTolerancePeriodInMin;
        public System.Windows.Forms.TextBox txtEventQSize;
        public System.Windows.Forms.Label lblEQS;
        public System.Windows.Forms.TextBox txtUnitID;
        public System.Windows.Forms.Label lblUnitID;
    }
}
