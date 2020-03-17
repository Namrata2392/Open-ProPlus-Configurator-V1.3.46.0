namespace OpenProPlusConfigurator
{
    partial class ucSPORTGroup
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
            this.lvSPORTMaster = new System.Windows.Forms.ListView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblIMC = new System.Windows.Forms.Label();
            this.grpSPORT = new System.Windows.Forms.GroupBox();
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
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grpSPORT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).BeginInit();
            this.SuspendLayout();
            // 
            // lvSPORTMaster
            // 
            this.lvSPORTMaster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvSPORTMaster.CheckBoxes = true;
            this.lvSPORTMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSPORTMaster.FullRowSelect = true;
            this.lvSPORTMaster.Location = new System.Drawing.Point(0, 30);
            this.lvSPORTMaster.MultiSelect = false;
            this.lvSPORTMaster.Name = "lvSPORTMaster";
            this.lvSPORTMaster.Size = new System.Drawing.Size(895, 422);
            this.lvSPORTMaster.TabIndex = 9;
            this.lvSPORTMaster.UseCompatibleStateImageBehavior = false;
            this.lvSPORTMaster.View = System.Windows.Forms.View.Details;
            this.lvSPORTMaster.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvSPORTMaster_ItemCheck);
            this.lvSPORTMaster.DoubleClick += new System.EventHandler(this.lvSPORTMaster_DoubleClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 452);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(895, 50);
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
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
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
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblIMC);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(895, 30);
            this.panel1.TabIndex = 110;
            // 
            // lblIMC
            // 
            this.lblIMC.AutoSize = true;
            this.lblIMC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIMC.Location = new System.Drawing.Point(3, 1);
            this.lblIMC.Name = "lblIMC";
            this.lblIMC.Size = new System.Drawing.Size(195, 15);
            this.lblIMC.TabIndex = 7;
            this.lblIMC.Text = "SPORT Master Configuration:";
            // 
            // grpSPORT
            // 
            this.grpSPORT.BackColor = System.Drawing.SystemColors.Control;
            this.grpSPORT.Controls.Add(this.btnLast);
            this.grpSPORT.Controls.Add(this.btnNext);
            this.grpSPORT.Controls.Add(this.btnPrev);
            this.grpSPORT.Controls.Add(this.btnFirst);
            this.grpSPORT.Controls.Add(this.txtRefreshInterval);
            this.grpSPORT.Controls.Add(this.lblRI);
            this.grpSPORT.Controls.Add(this.cmbPortNo);
            this.grpSPORT.Controls.Add(this.cmbDebug);
            this.grpSPORT.Controls.Add(this.txtDescription);
            this.grpSPORT.Controls.Add(this.lblDesc);
            this.grpSPORT.Controls.Add(this.chkRun);
            this.grpSPORT.Controls.Add(this.txtMasterNo);
            this.grpSPORT.Controls.Add(this.lblMN);
            this.grpSPORT.Controls.Add(this.txtFirmwareVersion);
            this.grpSPORT.Controls.Add(this.lblAFV);
            this.grpSPORT.Controls.Add(this.txtClockSyncInterval);
            this.grpSPORT.Controls.Add(this.lblCSI);
            this.grpSPORT.Controls.Add(this.txtGiTime);
            this.grpSPORT.Controls.Add(this.lblGT);
            this.grpSPORT.Controls.Add(this.lblDB);
            this.grpSPORT.Controls.Add(this.lblPN);
            this.grpSPORT.Controls.Add(this.lblHdrText);
            this.grpSPORT.Controls.Add(this.pbHdr);
            this.grpSPORT.Controls.Add(this.btnCancel);
            this.grpSPORT.Controls.Add(this.btnDone);
            this.grpSPORT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpSPORT.Location = new System.Drawing.Point(283, 103);
            this.grpSPORT.Name = "grpSPORT";
            this.grpSPORT.Size = new System.Drawing.Size(306, 297);
            this.grpSPORT.TabIndex = 111;
            this.grpSPORT.TabStop = false;
            this.grpSPORT.Visible = false;
            // 
            // btnLast
            // 
            this.btnLast.FlatAppearance.BorderSize = 0;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast.Location = new System.Drawing.Point(234, 269);
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
            this.btnNext.Location = new System.Drawing.Point(160, 269);
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
            this.btnPrev.Location = new System.Drawing.Point(86, 269);
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
            this.btnFirst.Location = new System.Drawing.Point(12, 269);
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
            this.txtRefreshInterval.Size = new System.Drawing.Size(147, 20);
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
            this.cmbPortNo.Size = new System.Drawing.Size(147, 21);
            this.cmbPortNo.TabIndex = 85;
            this.cmbPortNo.Tag = "PortNum";
            // 
            // cmbDebug
            // 
            this.cmbDebug.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDebug.FormattingEnabled = true;
            this.cmbDebug.Location = new System.Drawing.Point(144, 181);
            this.cmbDebug.Name = "cmbDebug";
            this.cmbDebug.Size = new System.Drawing.Size(147, 21);
            this.cmbDebug.TabIndex = 87;
            this.cmbDebug.Tag = "DEBUG";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(145, 207);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(147, 20);
            this.txtDescription.TabIndex = 97;
            this.txtDescription.Tag = "Description";
            this.txtDescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescription_KeyPress);
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Location = new System.Drawing.Point(12, 210);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(60, 13);
            this.lblDesc.TabIndex = 96;
            this.lblDesc.Text = "Description";
            // 
            // chkRun
            // 
            this.chkRun.AutoSize = true;
            this.chkRun.Location = new System.Drawing.Point(12, 241);
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
            this.txtMasterNo.Size = new System.Drawing.Size(147, 20);
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
            this.txtFirmwareVersion.Location = new System.Drawing.Point(144, 156);
            this.txtFirmwareVersion.Name = "txtFirmwareVersion";
            this.txtFirmwareVersion.Size = new System.Drawing.Size(147, 20);
            this.txtFirmwareVersion.TabIndex = 95;
            this.txtFirmwareVersion.Tag = "AppFirmwareVersion";
            // 
            // lblAFV
            // 
            this.lblAFV.AutoSize = true;
            this.lblAFV.Location = new System.Drawing.Point(12, 159);
            this.lblAFV.Name = "lblAFV";
            this.lblAFV.Size = new System.Drawing.Size(87, 13);
            this.lblAFV.TabIndex = 94;
            this.lblAFV.Text = "Firmware Version";
            // 
            // txtClockSyncInterval
            // 
            this.txtClockSyncInterval.Location = new System.Drawing.Point(145, 106);
            this.txtClockSyncInterval.Name = "txtClockSyncInterval";
            this.txtClockSyncInterval.Size = new System.Drawing.Size(147, 20);
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
            this.txtGiTime.Size = new System.Drawing.Size(147, 20);
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
            this.lblDB.Location = new System.Drawing.Point(12, 184);
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
            this.lblHdrText.Size = new System.Drawing.Size(91, 13);
            this.lblHdrText.TabIndex = 40;
            this.lblHdrText.Text = "SPORT Master";
            this.lblHdrText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseDown);
            this.lblHdrText.MouseHover += new System.EventHandler(this.lblHdrText_MouseHover);
            this.lblHdrText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseMove);
            // 
            // pbHdr
            // 
            this.pbHdr.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pbHdr.Location = new System.Drawing.Point(0, 0);
            this.pbHdr.Name = "pbHdr";
            this.pbHdr.Size = new System.Drawing.Size(306, 22);
            this.pbHdr.TabIndex = 39;
            this.pbHdr.TabStop = false;
            this.pbHdr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseDown);
            this.pbHdr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseMove);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(224, 236);
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
            this.btnDone.Location = new System.Drawing.Point(145, 236);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(68, 28);
            this.btnDone.TabIndex = 99;
            this.btnDone.Text = "&Update";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // ucSPORTGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpSPORT);
            this.Controls.Add(this.lvSPORTMaster);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "ucSPORTGroup";
            this.Size = new System.Drawing.Size(895, 502);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grpSPORT.ResumeLayout(false);
            this.grpSPORT.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView lvSPORTMaster;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblIMC;
        public System.Windows.Forms.GroupBox grpSPORT;
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
    }
}
