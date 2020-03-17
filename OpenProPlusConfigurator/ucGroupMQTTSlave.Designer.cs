namespace OpenProPlusConfigurator
{
    partial class ucGroupMQTTSlave
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMSC = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnExportINI = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lvMQTTSlave = new System.Windows.Forms.ListView();
            this.grpMQTTSlave = new System.Windows.Forms.GroupBox();
            this.CmbBroker = new System.Windows.Forms.ComboBox();
            this.CmbQoS = new System.Windows.Forms.ComboBox();
            this.lblQoS = new System.Windows.Forms.Label();
            this.txtPortNum = new System.Windows.Forms.TextBox();
            this.lblPortNum = new System.Windows.Forms.Label();
            this.txtScanTime = new System.Windows.Forms.TextBox();
            this.lblScanTime = new System.Windows.Forms.Label();
            this.chkRun = new System.Windows.Forms.CheckBox();
            this.CmbOutput = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBroker = new System.Windows.Forms.Label();
            this.txtBorkerAddr = new System.Windows.Forms.TextBox();
            this.lblBrokerAddr = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.txtSlaveNum = new System.Windows.Forms.TextBox();
            this.lblSN = new System.Windows.Forms.Label();
            this.TxtTLSLevel = new System.Windows.Forms.TextBox();
            this.lblTLSLevel = new System.Windows.Forms.Label();
            this.txtCertificate = new System.Windows.Forms.TextBox();
            this.lblCertificate = new System.Windows.Forms.Label();
            this.txtTopic = new System.Windows.Forms.TextBox();
            this.lblTopic = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblHdrText = new System.Windows.Forms.Label();
            this.pbHdr = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.TpBtn = new System.Windows.Forms.ToolTip(this.components);
            this.cmbDebug = new System.Windows.Forms.ComboBox();
            this.lblDebug = new System.Windows.Forms.Label();
            this.txtFirmwareVersion = new System.Windows.Forms.TextBox();
            this.lblAFV = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.grpMQTTSlave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).BeginInit();
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
            this.lblMSC.Size = new System.Drawing.Size(178, 15);
            this.lblMSC.TabIndex = 4;
            this.lblMSC.Text = "MQTT Slave Configuration:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnExportINI);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 589);
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
            // lvMQTTSlave
            // 
            this.lvMQTTSlave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvMQTTSlave.CheckBoxes = true;
            this.lvMQTTSlave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMQTTSlave.FullRowSelect = true;
            this.lvMQTTSlave.Location = new System.Drawing.Point(0, 30);
            this.lvMQTTSlave.MultiSelect = false;
            this.lvMQTTSlave.Name = "lvMQTTSlave";
            this.lvMQTTSlave.Size = new System.Drawing.Size(1026, 559);
            this.lvMQTTSlave.TabIndex = 25;
            this.lvMQTTSlave.UseCompatibleStateImageBehavior = false;
            this.lvMQTTSlave.View = System.Windows.Forms.View.Details;
            this.lvMQTTSlave.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvMQTTSlave_ItemCheck);
            this.lvMQTTSlave.SelectedIndexChanged += new System.EventHandler(this.lvMQTTSlave_SelectedIndexChanged);
            this.lvMQTTSlave.DoubleClick += new System.EventHandler(this.lvMQTTSlave_DoubleClick);
            // 
            // grpMQTTSlave
            // 
            this.grpMQTTSlave.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.grpMQTTSlave.Controls.Add(this.cmbDebug);
            this.grpMQTTSlave.Controls.Add(this.lblDebug);
            this.grpMQTTSlave.Controls.Add(this.txtFirmwareVersion);
            this.grpMQTTSlave.Controls.Add(this.lblAFV);
            this.grpMQTTSlave.Controls.Add(this.CmbBroker);
            this.grpMQTTSlave.Controls.Add(this.CmbQoS);
            this.grpMQTTSlave.Controls.Add(this.lblQoS);
            this.grpMQTTSlave.Controls.Add(this.txtPortNum);
            this.grpMQTTSlave.Controls.Add(this.lblPortNum);
            this.grpMQTTSlave.Controls.Add(this.txtScanTime);
            this.grpMQTTSlave.Controls.Add(this.lblScanTime);
            this.grpMQTTSlave.Controls.Add(this.chkRun);
            this.grpMQTTSlave.Controls.Add(this.CmbOutput);
            this.grpMQTTSlave.Controls.Add(this.label1);
            this.grpMQTTSlave.Controls.Add(this.lblBroker);
            this.grpMQTTSlave.Controls.Add(this.txtBorkerAddr);
            this.grpMQTTSlave.Controls.Add(this.lblBrokerAddr);
            this.grpMQTTSlave.Controls.Add(this.txtPassword);
            this.grpMQTTSlave.Controls.Add(this.lblPassword);
            this.grpMQTTSlave.Controls.Add(this.btnLast);
            this.grpMQTTSlave.Controls.Add(this.btnNext);
            this.grpMQTTSlave.Controls.Add(this.btnPrev);
            this.grpMQTTSlave.Controls.Add(this.btnFirst);
            this.grpMQTTSlave.Controls.Add(this.txtSlaveNum);
            this.grpMQTTSlave.Controls.Add(this.lblSN);
            this.grpMQTTSlave.Controls.Add(this.TxtTLSLevel);
            this.grpMQTTSlave.Controls.Add(this.lblTLSLevel);
            this.grpMQTTSlave.Controls.Add(this.txtCertificate);
            this.grpMQTTSlave.Controls.Add(this.lblCertificate);
            this.grpMQTTSlave.Controls.Add(this.txtTopic);
            this.grpMQTTSlave.Controls.Add(this.lblTopic);
            this.grpMQTTSlave.Controls.Add(this.txtUserName);
            this.grpMQTTSlave.Controls.Add(this.lblUserName);
            this.grpMQTTSlave.Controls.Add(this.lblHdrText);
            this.grpMQTTSlave.Controls.Add(this.pbHdr);
            this.grpMQTTSlave.Controls.Add(this.btnCancel);
            this.grpMQTTSlave.Controls.Add(this.btnDone);
            this.grpMQTTSlave.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpMQTTSlave.Location = new System.Drawing.Point(445, 55);
            this.grpMQTTSlave.Name = "grpMQTTSlave";
            this.grpMQTTSlave.Size = new System.Drawing.Size(318, 461);
            this.grpMQTTSlave.TabIndex = 26;
            this.grpMQTTSlave.TabStop = false;
            this.grpMQTTSlave.Visible = false;
            // 
            // CmbBroker
            // 
            this.CmbBroker.FormattingEnabled = true;
            this.CmbBroker.Location = new System.Drawing.Point(116, 131);
            this.CmbBroker.Name = "CmbBroker";
            this.CmbBroker.Size = new System.Drawing.Size(173, 21);
            this.CmbBroker.TabIndex = 139;
            this.CmbBroker.Tag = "Broker";
            // 
            // CmbQoS
            // 
            this.CmbQoS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbQoS.FormattingEnabled = true;
            this.CmbQoS.Location = new System.Drawing.Point(116, 308);
            this.CmbQoS.Name = "CmbQoS";
            this.CmbQoS.Size = new System.Drawing.Size(173, 21);
            this.CmbQoS.TabIndex = 138;
            this.CmbQoS.Tag = "QoS";
            // 
            // lblQoS
            // 
            this.lblQoS.AutoSize = true;
            this.lblQoS.Location = new System.Drawing.Point(20, 314);
            this.lblQoS.Name = "lblQoS";
            this.lblQoS.Size = new System.Drawing.Size(28, 13);
            this.lblQoS.TabIndex = 137;
            this.lblQoS.Text = "QoS";
            // 
            // txtPortNum
            // 
            this.txtPortNum.Location = new System.Drawing.Point(116, 283);
            this.txtPortNum.Name = "txtPortNum";
            this.txtPortNum.Size = new System.Drawing.Size(173, 20);
            this.txtPortNum.TabIndex = 136;
            this.txtPortNum.Tag = "PortNum";
            this.txtPortNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPortNum_KeyPress);
            // 
            // lblPortNum
            // 
            this.lblPortNum.AutoSize = true;
            this.lblPortNum.Location = new System.Drawing.Point(20, 288);
            this.lblPortNum.Name = "lblPortNum";
            this.lblPortNum.Size = new System.Drawing.Size(46, 13);
            this.lblPortNum.TabIndex = 135;
            this.lblPortNum.Text = "Port No.";
            // 
            // txtScanTime
            // 
            this.txtScanTime.Location = new System.Drawing.Point(116, 182);
            this.txtScanTime.Name = "txtScanTime";
            this.txtScanTime.Size = new System.Drawing.Size(173, 20);
            this.txtScanTime.TabIndex = 134;
            this.txtScanTime.Tag = "ScanTime";
            // 
            // lblScanTime
            // 
            this.lblScanTime.AutoSize = true;
            this.lblScanTime.Location = new System.Drawing.Point(20, 186);
            this.lblScanTime.Name = "lblScanTime";
            this.lblScanTime.Size = new System.Drawing.Size(83, 13);
            this.lblScanTime.TabIndex = 133;
            this.lblScanTime.Text = "ScanTime (Sec)";
            // 
            // chkRun
            // 
            this.chkRun.AutoSize = true;
            this.chkRun.Location = new System.Drawing.Point(23, 399);
            this.chkRun.Name = "chkRun";
            this.chkRun.Size = new System.Drawing.Size(46, 17);
            this.chkRun.TabIndex = 121;
            this.chkRun.Tag = "Run_YES_NO";
            this.chkRun.Text = "Run";
            this.chkRun.UseVisualStyleBackColor = true;
            // 
            // CmbOutput
            // 
            this.CmbOutput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbOutput.FormattingEnabled = true;
            this.CmbOutput.Location = new System.Drawing.Point(116, 257);
            this.CmbOutput.Name = "CmbOutput";
            this.CmbOutput.Size = new System.Drawing.Size(173, 21);
            this.CmbOutput.TabIndex = 128;
            this.CmbOutput.Tag = "Output";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 261);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 127;
            this.label1.Text = "Output";
            // 
            // lblBroker
            // 
            this.lblBroker.AutoSize = true;
            this.lblBroker.Location = new System.Drawing.Point(20, 133);
            this.lblBroker.Name = "lblBroker";
            this.lblBroker.Size = new System.Drawing.Size(38, 13);
            this.lblBroker.TabIndex = 125;
            this.lblBroker.Text = "Broker";
            // 
            // txtBorkerAddr
            // 
            this.txtBorkerAddr.Location = new System.Drawing.Point(116, 157);
            this.txtBorkerAddr.MaxLength = 100;
            this.txtBorkerAddr.Name = "txtBorkerAddr";
            this.txtBorkerAddr.Size = new System.Drawing.Size(173, 20);
            this.txtBorkerAddr.TabIndex = 124;
            this.txtBorkerAddr.Tag = "BrokerAddress";
            // 
            // lblBrokerAddr
            // 
            this.lblBrokerAddr.AutoSize = true;
            this.lblBrokerAddr.Location = new System.Drawing.Point(20, 160);
            this.lblBrokerAddr.Name = "lblBrokerAddr";
            this.lblBrokerAddr.Size = new System.Drawing.Size(79, 13);
            this.lblBrokerAddr.TabIndex = 123;
            this.lblBrokerAddr.Text = "Broker Address";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(116, 81);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(173, 20);
            this.txtPassword.TabIndex = 67;
            this.txtPassword.Tag = "Password";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(20, 82);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 66;
            this.lblPassword.Text = "Password";
            // 
            // btnLast
            // 
            this.btnLast.FlatAppearance.BorderSize = 0;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast.Location = new System.Drawing.Point(230, 433);
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
            this.btnNext.Location = new System.Drawing.Point(161, 433);
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
            this.btnPrev.Location = new System.Drawing.Point(92, 433);
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
            this.btnFirst.Location = new System.Drawing.Point(23, 433);
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
            this.txtSlaveNum.Location = new System.Drawing.Point(116, 31);
            this.txtSlaveNum.Name = "txtSlaveNum";
            this.txtSlaveNum.Size = new System.Drawing.Size(173, 20);
            this.txtSlaveNum.TabIndex = 57;
            this.txtSlaveNum.Tag = "SlaveNum";
            // 
            // lblSN
            // 
            this.lblSN.AutoSize = true;
            this.lblSN.Location = new System.Drawing.Point(20, 34);
            this.lblSN.Name = "lblSN";
            this.lblSN.Size = new System.Drawing.Size(54, 13);
            this.lblSN.TabIndex = 56;
            this.lblSN.Text = "Slave No.";
            // 
            // TxtTLSLevel
            // 
            this.TxtTLSLevel.Location = new System.Drawing.Point(116, 232);
            this.TxtTLSLevel.Name = "TxtTLSLevel";
            this.TxtTLSLevel.Size = new System.Drawing.Size(173, 20);
            this.TxtTLSLevel.TabIndex = 71;
            this.TxtTLSLevel.Tag = "TLSLevel";
            // 
            // lblTLSLevel
            // 
            this.lblTLSLevel.AutoSize = true;
            this.lblTLSLevel.Location = new System.Drawing.Point(20, 236);
            this.lblTLSLevel.Name = "lblTLSLevel";
            this.lblTLSLevel.Size = new System.Drawing.Size(56, 13);
            this.lblTLSLevel.TabIndex = 70;
            this.lblTLSLevel.Text = "TLS Level";
            // 
            // txtCertificate
            // 
            this.txtCertificate.Location = new System.Drawing.Point(116, 207);
            this.txtCertificate.Name = "txtCertificate";
            this.txtCertificate.Size = new System.Drawing.Size(173, 20);
            this.txtCertificate.TabIndex = 69;
            this.txtCertificate.Tag = "Certificate";
            // 
            // lblCertificate
            // 
            this.lblCertificate.AutoSize = true;
            this.lblCertificate.Location = new System.Drawing.Point(20, 210);
            this.lblCertificate.Name = "lblCertificate";
            this.lblCertificate.Size = new System.Drawing.Size(54, 13);
            this.lblCertificate.TabIndex = 68;
            this.lblCertificate.Text = "Certificate";
            // 
            // txtTopic
            // 
            this.txtTopic.Location = new System.Drawing.Point(116, 106);
            this.txtTopic.Name = "txtTopic";
            this.txtTopic.Size = new System.Drawing.Size(173, 20);
            this.txtTopic.TabIndex = 65;
            this.txtTopic.Tag = "Topic";
            // 
            // lblTopic
            // 
            this.lblTopic.AutoSize = true;
            this.lblTopic.Location = new System.Drawing.Point(20, 108);
            this.lblTopic.Name = "lblTopic";
            this.lblTopic.Size = new System.Drawing.Size(34, 13);
            this.lblTopic.TabIndex = 64;
            this.lblTopic.Text = "Topic";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(116, 56);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(173, 20);
            this.txtUserName.TabIndex = 63;
            this.txtUserName.Tag = "UserName";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(20, 57);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(57, 13);
            this.lblUserName.TabIndex = 62;
            this.lblUserName.Text = "UserName";
            // 
            // lblHdrText
            // 
            this.lblHdrText.AutoSize = true;
            this.lblHdrText.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblHdrText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHdrText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHdrText.Location = new System.Drawing.Point(10, 4);
            this.lblHdrText.Name = "lblHdrText";
            this.lblHdrText.Size = new System.Drawing.Size(78, 13);
            this.lblHdrText.TabIndex = 40;
            this.lblHdrText.Text = "MQTT Slave";
            this.lblHdrText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseDown);
            this.lblHdrText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseMove);
            // 
            // pbHdr
            // 
            this.pbHdr.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pbHdr.Location = new System.Drawing.Point(0, 0);
            this.pbHdr.Name = "pbHdr";
            this.pbHdr.Size = new System.Drawing.Size(318, 22);
            this.pbHdr.TabIndex = 39;
            this.pbHdr.TabStop = false;
            this.pbHdr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseDown);
            this.pbHdr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseMove);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(221, 399);
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
            this.btnDone.Location = new System.Drawing.Point(116, 399);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(68, 28);
            this.btnDone.TabIndex = 75;
            this.btnDone.Text = "&Update";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // cmbDebug
            // 
            this.cmbDebug.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDebug.FormattingEnabled = true;
            this.cmbDebug.Location = new System.Drawing.Point(116, 359);
            this.cmbDebug.Name = "cmbDebug";
            this.cmbDebug.Size = new System.Drawing.Size(173, 21);
            this.cmbDebug.TabIndex = 143;
            this.cmbDebug.Tag = "DEBUG";
            // 
            // lblDebug
            // 
            this.lblDebug.AutoSize = true;
            this.lblDebug.Location = new System.Drawing.Point(20, 365);
            this.lblDebug.Name = "lblDebug";
            this.lblDebug.Size = new System.Drawing.Size(68, 13);
            this.lblDebug.TabIndex = 142;
            this.lblDebug.Text = "Debug Level";
            // 
            // txtFirmwareVersion
            // 
            this.txtFirmwareVersion.Enabled = false;
            this.txtFirmwareVersion.Location = new System.Drawing.Point(116, 334);
            this.txtFirmwareVersion.Name = "txtFirmwareVersion";
            this.txtFirmwareVersion.Size = new System.Drawing.Size(173, 20);
            this.txtFirmwareVersion.TabIndex = 141;
            this.txtFirmwareVersion.Tag = "AppFirmwareVersion";
            // 
            // lblAFV
            // 
            this.lblAFV.AutoSize = true;
            this.lblAFV.Location = new System.Drawing.Point(20, 341);
            this.lblAFV.Name = "lblAFV";
            this.lblAFV.Size = new System.Drawing.Size(87, 13);
            this.lblAFV.TabIndex = 140;
            this.lblAFV.Text = "Firmware Version";
            // 
            // ucGroupMQTTSlave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMQTTSlave);
            this.Controls.Add(this.lvMQTTSlave);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ucGroupMQTTSlave";
            this.Size = new System.Drawing.Size(1026, 639);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.grpMQTTSlave.ResumeLayout(false);
            this.grpMQTTSlave.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMSC;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Button btnExportINI;
        public System.Windows.Forms.Button btnAdd;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.ListView lvMQTTSlave;
        public System.Windows.Forms.GroupBox grpMQTTSlave;
        public System.Windows.Forms.TextBox txtBorkerAddr;
        private System.Windows.Forms.Label lblBrokerAddr;
        public System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnFirst;
        public System.Windows.Forms.TextBox txtSlaveNum;
        private System.Windows.Forms.Label lblSN;
        public System.Windows.Forms.TextBox TxtTLSLevel;
        private System.Windows.Forms.Label lblTLSLevel;
        public System.Windows.Forms.TextBox txtCertificate;
        private System.Windows.Forms.Label lblCertificate;
        public System.Windows.Forms.TextBox txtTopic;
        private System.Windows.Forms.Label lblTopic;
        public System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblHdrText;
        private System.Windows.Forms.PictureBox pbHdr;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.ToolTip TpBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblBroker;
        public System.Windows.Forms.ComboBox CmbOutput;
        public System.Windows.Forms.CheckBox chkRun;
        public System.Windows.Forms.TextBox txtScanTime;
        private System.Windows.Forms.Label lblScanTime;
        public System.Windows.Forms.ComboBox CmbQoS;
        private System.Windows.Forms.Label lblQoS;
        public System.Windows.Forms.TextBox txtPortNum;
        private System.Windows.Forms.Label lblPortNum;
        public System.Windows.Forms.ComboBox CmbBroker;
        public System.Windows.Forms.ComboBox cmbDebug;
        private System.Windows.Forms.Label lblDebug;
        public System.Windows.Forms.TextBox txtFirmwareVersion;
        private System.Windows.Forms.Label lblAFV;
    }
}
