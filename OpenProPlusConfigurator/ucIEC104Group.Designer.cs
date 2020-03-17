namespace OpenProPlusConfigurator
{
    partial class ucIEC104Group
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblIMC = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lvIEC101Master = new System.Windows.Forms.ListView();
            this.grpIEC104 = new System.Windows.Forms.GroupBox();
            this.txtK = new System.Windows.Forms.TextBox();
            this.lblK = new System.Windows.Forms.Label();
            this.txtW = new System.Windows.Forms.TextBox();
            this.lblW = new System.Windows.Forms.Label();
            this.txtT3 = new System.Windows.Forms.TextBox();
            this.lblT3 = new System.Windows.Forms.Label();
            this.txtT2 = new System.Windows.Forms.TextBox();
            this.lblT2 = new System.Windows.Forms.Label();
            this.txtT1 = new System.Windows.Forms.TextBox();
            this.lblT1 = new System.Windows.Forms.Label();
            this.txtT0 = new System.Windows.Forms.TextBox();
            this.lblT0 = new System.Windows.Forms.Label();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.txtRefreshInterval = new System.Windows.Forms.TextBox();
            this.lblRI = new System.Windows.Forms.Label();
            this.cmbPortNo = new System.Windows.Forms.ComboBox();
            this.cmbDebug = new System.Windows.Forms.ComboBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.chkRun = new System.Windows.Forms.CheckBox();
            this.txtMasterNo = new System.Windows.Forms.TextBox();
            this.lblMN = new System.Windows.Forms.Label();
            this.txtFirmwareVersion = new System.Windows.Forms.TextBox();
            this.lblAFV = new System.Windows.Forms.Label();
            this.txtClockSyncInterval = new System.Windows.Forms.TextBox();
            this.lblCSI = new System.Windows.Forms.Label();
            this.txtGiTime = new System.Windows.Forms.TextBox();
            this.lblGT = new System.Windows.Forms.Label();
            this.lblDB = new System.Windows.Forms.Label();
            this.lblPN = new System.Windows.Forms.Label();
            this.lblHdrText = new System.Windows.Forms.Label();
            this.pbHdr = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.grpIEC104.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblIMC);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1050, 30);
            this.panel1.TabIndex = 108;
            // 
            // lblIMC
            // 
            this.lblIMC.AutoSize = true;
            this.lblIMC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIMC.Location = new System.Drawing.Point(3, 1);
            this.lblIMC.Name = "lblIMC";
            this.lblIMC.Size = new System.Drawing.Size(195, 15);
            this.lblIMC.TabIndex = 7;
            this.lblIMC.Text = "IEC104 Master Configuration:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 650);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1050, 50);
            this.panel2.TabIndex = 109;
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(80, 9);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(68, 28);
            this.btnDelete.TabIndex = 106;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Location = new System.Drawing.Point(3, 9);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(68, 28);
            this.btnAdd.TabIndex = 105;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // lvIEC101Master
            // 
            this.lvIEC101Master.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvIEC101Master.CheckBoxes = true;
            this.lvIEC101Master.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvIEC101Master.FullRowSelect = true;
            this.lvIEC101Master.Location = new System.Drawing.Point(0, 30);
            this.lvIEC101Master.MultiSelect = false;
            this.lvIEC101Master.Name = "lvIEC101Master";
            this.lvIEC101Master.Size = new System.Drawing.Size(1050, 620);
            this.lvIEC101Master.TabIndex = 110;
            this.lvIEC101Master.UseCompatibleStateImageBehavior = false;
            this.lvIEC101Master.View = System.Windows.Forms.View.Details;
            this.lvIEC101Master.SelectedIndexChanged += new System.EventHandler(this.lvIEC101Master_SelectedIndexChanged);
            // 
            // grpIEC104
            // 
            this.grpIEC104.BackColor = System.Drawing.SystemColors.Control;
            this.grpIEC104.Controls.Add(this.txtK);
            this.grpIEC104.Controls.Add(this.lblK);
            this.grpIEC104.Controls.Add(this.txtW);
            this.grpIEC104.Controls.Add(this.lblW);
            this.grpIEC104.Controls.Add(this.txtT3);
            this.grpIEC104.Controls.Add(this.lblT3);
            this.grpIEC104.Controls.Add(this.txtT2);
            this.grpIEC104.Controls.Add(this.lblT2);
            this.grpIEC104.Controls.Add(this.txtT1);
            this.grpIEC104.Controls.Add(this.lblT1);
            this.grpIEC104.Controls.Add(this.txtT0);
            this.grpIEC104.Controls.Add(this.lblT0);
            this.grpIEC104.Controls.Add(this.btnLast);
            this.grpIEC104.Controls.Add(this.btnNext);
            this.grpIEC104.Controls.Add(this.btnPrev);
            this.grpIEC104.Controls.Add(this.btnFirst);
            this.grpIEC104.Controls.Add(this.txtRefreshInterval);
            this.grpIEC104.Controls.Add(this.lblRI);
            this.grpIEC104.Controls.Add(this.cmbPortNo);
            this.grpIEC104.Controls.Add(this.cmbDebug);
            this.grpIEC104.Controls.Add(this.txtDescription);
            this.grpIEC104.Controls.Add(this.lblDesc);
            this.grpIEC104.Controls.Add(this.chkRun);
            this.grpIEC104.Controls.Add(this.txtMasterNo);
            this.grpIEC104.Controls.Add(this.lblMN);
            this.grpIEC104.Controls.Add(this.txtFirmwareVersion);
            this.grpIEC104.Controls.Add(this.lblAFV);
            this.grpIEC104.Controls.Add(this.txtClockSyncInterval);
            this.grpIEC104.Controls.Add(this.lblCSI);
            this.grpIEC104.Controls.Add(this.txtGiTime);
            this.grpIEC104.Controls.Add(this.lblGT);
            this.grpIEC104.Controls.Add(this.lblDB);
            this.grpIEC104.Controls.Add(this.lblPN);
            this.grpIEC104.Controls.Add(this.lblHdrText);
            this.grpIEC104.Controls.Add(this.pbHdr);
            this.grpIEC104.Controls.Add(this.btnCancel);
            this.grpIEC104.Controls.Add(this.btnDone);
            this.grpIEC104.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpIEC104.Location = new System.Drawing.Point(386, 108);
            this.grpIEC104.Name = "grpIEC104";
            this.grpIEC104.Size = new System.Drawing.Size(310, 397);
            this.grpIEC104.TabIndex = 111;
            this.grpIEC104.TabStop = false;
            this.grpIEC104.Visible = false;
            this.grpIEC104.Move += new System.EventHandler(this.grpIEC101_Move);
            // 
            // txtK
            // 
            this.txtK.Location = new System.Drawing.Point(145, 229);
            this.txtK.Name = "txtK";
            this.txtK.Size = new System.Drawing.Size(146, 20);
            this.txtK.TabIndex = 122;
            this.txtK.Tag = "K";
            // 
            // lblK
            // 
            this.lblK.AutoSize = true;
            this.lblK.Location = new System.Drawing.Point(12, 232);
            this.lblK.Name = "lblK";
            this.lblK.Size = new System.Drawing.Size(14, 13);
            this.lblK.TabIndex = 121;
            this.lblK.Text = "K";
            // 
            // txtW
            // 
            this.txtW.Location = new System.Drawing.Point(145, 205);
            this.txtW.Name = "txtW";
            this.txtW.Size = new System.Drawing.Size(146, 20);
            this.txtW.TabIndex = 120;
            this.txtW.Tag = "W";
            // 
            // lblW
            // 
            this.lblW.AutoSize = true;
            this.lblW.Location = new System.Drawing.Point(12, 208);
            this.lblW.Name = "lblW";
            this.lblW.Size = new System.Drawing.Size(18, 13);
            this.lblW.TabIndex = 119;
            this.lblW.Text = "W";
            // 
            // txtT3
            // 
            this.txtT3.Location = new System.Drawing.Point(246, 178);
            this.txtT3.Name = "txtT3";
            this.txtT3.Size = new System.Drawing.Size(45, 20);
            this.txtT3.TabIndex = 118;
            this.txtT3.Tag = "T3";
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.Location = new System.Drawing.Point(243, 159);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(46, 13);
            this.lblT3.TabIndex = 117;
            this.lblT3.Text = "T3 (sec)";
            // 
            // txtT2
            // 
            this.txtT2.Location = new System.Drawing.Point(168, 178);
            this.txtT2.Name = "txtT2";
            this.txtT2.Size = new System.Drawing.Size(45, 20);
            this.txtT2.TabIndex = 116;
            this.txtT2.Tag = "T2";
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.Location = new System.Drawing.Point(166, 159);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(46, 13);
            this.lblT2.TabIndex = 115;
            this.lblT2.Text = "T2 (sec)";
            // 
            // txtT1
            // 
            this.txtT1.Location = new System.Drawing.Point(90, 178);
            this.txtT1.Name = "txtT1";
            this.txtT1.Size = new System.Drawing.Size(45, 20);
            this.txtT1.TabIndex = 114;
            this.txtT1.Tag = "T1";
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.Location = new System.Drawing.Point(89, 159);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(46, 13);
            this.lblT1.TabIndex = 113;
            this.lblT1.Text = "T1 (sec)";
            // 
            // txtT0
            // 
            this.txtT0.Location = new System.Drawing.Point(12, 178);
            this.txtT0.Name = "txtT0";
            this.txtT0.Size = new System.Drawing.Size(45, 20);
            this.txtT0.TabIndex = 112;
            this.txtT0.Tag = "T0";
            // 
            // lblT0
            // 
            this.lblT0.AutoSize = true;
            this.lblT0.Location = new System.Drawing.Point(12, 159);
            this.lblT0.Name = "lblT0";
            this.lblT0.Size = new System.Drawing.Size(46, 13);
            this.lblT0.TabIndex = 111;
            this.lblT0.Text = "T0 (sec)";
            // 
            // btnLast
            // 
            this.btnLast.FlatAppearance.BorderSize = 0;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast.Location = new System.Drawing.Point(233, 370);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(58, 22);
            this.btnLast.TabIndex = 110;
            this.btnLast.Text = "Last>>";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(158, 370);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(58, 22);
            this.btnNext.TabIndex = 109;
            this.btnNext.Text = "Next>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.FlatAppearance.BorderSize = 0;
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrev.Location = new System.Drawing.Point(85, 370);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(58, 22);
            this.btnPrev.TabIndex = 108;
            this.btnPrev.Text = "<Prev";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.FlatAppearance.BorderSize = 0;
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFirst.Location = new System.Drawing.Point(12, 370);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(58, 22);
            this.btnFirst.TabIndex = 107;
            this.btnFirst.Text = "<<First";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // txtRefreshInterval
            // 
            this.txtRefreshInterval.Location = new System.Drawing.Point(145, 131);
            this.txtRefreshInterval.Name = "txtRefreshInterval";
            this.txtRefreshInterval.Size = new System.Drawing.Size(146, 20);
            this.txtRefreshInterval.TabIndex = 93;
            this.txtRefreshInterval.Tag = "RefreshInterval";
            // 
            // lblRI
            // 
            this.lblRI.AutoSize = true;
            this.lblRI.Location = new System.Drawing.Point(12, 134);
            this.lblRI.Name = "lblRI";
            this.lblRI.Size = new System.Drawing.Size(108, 13);
            this.lblRI.TabIndex = 92;
            this.lblRI.Text = "Refresh Interval (sec)";
            // 
            // cmbPortNo
            // 
            this.cmbPortNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPortNo.FormattingEnabled = true;
            this.cmbPortNo.Location = new System.Drawing.Point(145, 55);
            this.cmbPortNo.Name = "cmbPortNo";
            this.cmbPortNo.Size = new System.Drawing.Size(146, 21);
            this.cmbPortNo.TabIndex = 85;
            this.cmbPortNo.Tag = "PortNum";
            // 
            // cmbDebug
            // 
            this.cmbDebug.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDebug.FormattingEnabled = true;
            this.cmbDebug.Location = new System.Drawing.Point(144, 282);
            this.cmbDebug.Name = "cmbDebug";
            this.cmbDebug.Size = new System.Drawing.Size(146, 21);
            this.cmbDebug.TabIndex = 87;
            this.cmbDebug.Tag = "DEBUG";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(145, 308);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(146, 20);
            this.txtDescription.TabIndex = 97;
            this.txtDescription.Tag = "Description";
            this.txtDescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescription_KeyPress);
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Location = new System.Drawing.Point(12, 311);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(60, 13);
            this.lblDesc.TabIndex = 96;
            this.lblDesc.Text = "Description";
            // 
            // chkRun
            // 
            this.chkRun.AutoSize = true;
            this.chkRun.Location = new System.Drawing.Point(12, 342);
            this.chkRun.Name = "chkRun";
            this.chkRun.Size = new System.Drawing.Size(46, 17);
            this.chkRun.TabIndex = 98;
            this.chkRun.Tag = "Run_YES_NO";
            this.chkRun.Text = "Run";
            this.chkRun.UseVisualStyleBackColor = true;
            // 
            // txtMasterNo
            // 
            this.txtMasterNo.Enabled = false;
            this.txtMasterNo.Location = new System.Drawing.Point(145, 30);
            this.txtMasterNo.Name = "txtMasterNo";
            this.txtMasterNo.Size = new System.Drawing.Size(146, 20);
            this.txtMasterNo.TabIndex = 83;
            this.txtMasterNo.Tag = "MasterNum";
            // 
            // lblMN
            // 
            this.lblMN.AutoSize = true;
            this.lblMN.Location = new System.Drawing.Point(12, 33);
            this.lblMN.Name = "lblMN";
            this.lblMN.Size = new System.Drawing.Size(59, 13);
            this.lblMN.TabIndex = 82;
            this.lblMN.Text = "Master No.";
            // 
            // txtFirmwareVersion
            // 
            this.txtFirmwareVersion.Enabled = false;
            this.txtFirmwareVersion.Location = new System.Drawing.Point(144, 257);
            this.txtFirmwareVersion.Name = "txtFirmwareVersion";
            this.txtFirmwareVersion.Size = new System.Drawing.Size(146, 20);
            this.txtFirmwareVersion.TabIndex = 95;
            this.txtFirmwareVersion.Tag = "AppFirmwareVersion";
            // 
            // lblAFV
            // 
            this.lblAFV.AutoSize = true;
            this.lblAFV.Location = new System.Drawing.Point(12, 260);
            this.lblAFV.Name = "lblAFV";
            this.lblAFV.Size = new System.Drawing.Size(87, 13);
            this.lblAFV.TabIndex = 94;
            this.lblAFV.Text = "Firmware Version";
            // 
            // txtClockSyncInterval
            // 
            this.txtClockSyncInterval.Location = new System.Drawing.Point(145, 106);
            this.txtClockSyncInterval.Name = "txtClockSyncInterval";
            this.txtClockSyncInterval.Size = new System.Drawing.Size(146, 20);
            this.txtClockSyncInterval.TabIndex = 91;
            this.txtClockSyncInterval.Tag = "ClockSyncInterval";
            // 
            // lblCSI
            // 
            this.lblCSI.AutoSize = true;
            this.lblCSI.Location = new System.Drawing.Point(12, 109);
            this.lblCSI.Name = "lblCSI";
            this.lblCSI.Size = new System.Drawing.Size(125, 13);
            this.lblCSI.TabIndex = 90;
            this.lblCSI.Text = "Clock Sync Interval (sec)";
            // 
            // txtGiTime
            // 
            this.txtGiTime.Location = new System.Drawing.Point(145, 81);
            this.txtGiTime.Name = "txtGiTime";
            this.txtGiTime.Size = new System.Drawing.Size(146, 20);
            this.txtGiTime.TabIndex = 89;
            this.txtGiTime.Tag = "GiTime";
            // 
            // lblGT
            // 
            this.lblGT.AutoSize = true;
            this.lblGT.Location = new System.Drawing.Point(12, 84);
            this.lblGT.Name = "lblGT";
            this.lblGT.Size = new System.Drawing.Size(70, 13);
            this.lblGT.TabIndex = 88;
            this.lblGT.Text = "GI Time (sec)";
            // 
            // lblDB
            // 
            this.lblDB.AutoSize = true;
            this.lblDB.Location = new System.Drawing.Point(12, 285);
            this.lblDB.Name = "lblDB";
            this.lblDB.Size = new System.Drawing.Size(68, 13);
            this.lblDB.TabIndex = 86;
            this.lblDB.Text = "Debug Level";
            // 
            // lblPN
            // 
            this.lblPN.AutoSize = true;
            this.lblPN.Location = new System.Drawing.Point(12, 58);
            this.lblPN.Name = "lblPN";
            this.lblPN.Size = new System.Drawing.Size(46, 13);
            this.lblPN.TabIndex = 84;
            this.lblPN.Text = "Port No.";
            // 
            // lblHdrText
            // 
            this.lblHdrText.AutoSize = true;
            this.lblHdrText.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblHdrText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHdrText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHdrText.Location = new System.Drawing.Point(10, 4);
            this.lblHdrText.Name = "lblHdrText";
            this.lblHdrText.Size = new System.Drawing.Size(90, 13);
            this.lblHdrText.TabIndex = 40;
            this.lblHdrText.Text = "IEC104 Master";
            // 
            // pbHdr
            // 
            this.pbHdr.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pbHdr.Location = new System.Drawing.Point(0, 0);
            this.pbHdr.Name = "pbHdr";
            this.pbHdr.Size = new System.Drawing.Size(310, 22);
            this.pbHdr.TabIndex = 39;
            this.pbHdr.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(223, 337);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 28);
            this.btnCancel.TabIndex = 100;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDone
            // 
            this.btnDone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDone.Location = new System.Drawing.Point(145, 337);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(68, 28);
            this.btnDone.TabIndex = 99;
            this.btnDone.Text = "&Update";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // ucIEC104Group
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpIEC104);
            this.Controls.Add(this.lvIEC101Master);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ucIEC104Group";
            this.Size = new System.Drawing.Size(1050, 700);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.grpIEC104.ResumeLayout(false);
            this.grpIEC104.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblIMC;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Button btnAdd;
        public System.Windows.Forms.ListView lvIEC101Master;
        public System.Windows.Forms.GroupBox grpIEC104;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnFirst;
        public System.Windows.Forms.TextBox txtRefreshInterval;
        private System.Windows.Forms.Label lblRI;
        public System.Windows.Forms.ComboBox cmbPortNo;
        public System.Windows.Forms.ComboBox cmbDebug;
        public System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDesc;
        public System.Windows.Forms.CheckBox chkRun;
        public System.Windows.Forms.TextBox txtMasterNo;
        private System.Windows.Forms.Label lblMN;
        public System.Windows.Forms.TextBox txtFirmwareVersion;
        private System.Windows.Forms.Label lblAFV;
        public System.Windows.Forms.TextBox txtClockSyncInterval;
        private System.Windows.Forms.Label lblCSI;
        public System.Windows.Forms.TextBox txtGiTime;
        private System.Windows.Forms.Label lblGT;
        private System.Windows.Forms.Label lblDB;
        private System.Windows.Forms.Label lblPN;
        private System.Windows.Forms.Label lblHdrText;
        private System.Windows.Forms.PictureBox pbHdr;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDone;
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
    }
}
