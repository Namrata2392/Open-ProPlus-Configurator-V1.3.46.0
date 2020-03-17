namespace OpenProPlusConfigurator
{
    partial class ucGroupLoadProfile
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
            this.lvLoadProfilemaster = new System.Windows.Forms.ListView();
            this.lblMMC = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.grpLoadProfile = new System.Windows.Forms.GroupBox();
            this.txtTimeIntrvl = new System.Windows.Forms.TextBox();
            this.chkbxRun = new System.Windows.Forms.CheckBox();
            this.lblTimeInterval = new System.Windows.Forms.Label();
            this.cmbDebug = new System.Windows.Forms.ComboBox();
            this.lblDB = new System.Windows.Forms.Label();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.txtMasterNo = new System.Windows.Forms.TextBox();
            this.lblMN = new System.Windows.Forms.Label();
            this.lblHdrText = new System.Windows.Forms.Label();
            this.pbHdr = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grpLoadProfile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvLoadProfilemaster
            // 
            this.lvLoadProfilemaster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvLoadProfilemaster.CheckBoxes = true;
            this.lvLoadProfilemaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLoadProfilemaster.FullRowSelect = true;
            this.lvLoadProfilemaster.Location = new System.Drawing.Point(0, 30);
            this.lvLoadProfilemaster.MultiSelect = false;
            this.lvLoadProfilemaster.Name = "lvLoadProfilemaster";
            this.lvLoadProfilemaster.Size = new System.Drawing.Size(1050, 620);
            this.lvLoadProfilemaster.TabIndex = 9;
            this.lvLoadProfilemaster.UseCompatibleStateImageBehavior = false;
            this.lvLoadProfilemaster.View = System.Windows.Forms.View.Details;
            this.lvLoadProfilemaster.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvLoadProfilemaster_ItemCheck);
            this.lvLoadProfilemaster.SelectedIndexChanged += new System.EventHandler(this.lvLoadProfilemaster_SelectedIndexChanged);
            this.lvLoadProfilemaster.DoubleClick += new System.EventHandler(this.lvLoadProfilemaster_DoubleClick);
            // 
            // lblMMC
            // 
            this.lblMMC.AutoSize = true;
            this.lblMMC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMMC.Location = new System.Drawing.Point(3, 0);
            this.lblMMC.Name = "lblMMC";
            this.lblMMC.Size = new System.Drawing.Size(227, 15);
            this.lblMMC.TabIndex = 8;
            this.lblMMC.Text = "Load Profile Master Configuration:";
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(80, 9);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(68, 28);
            this.btnDelete.TabIndex = 26;
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
            this.btnAdd.TabIndex = 25;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // grpLoadProfile
            // 
            this.grpLoadProfile.BackColor = System.Drawing.SystemColors.Control;
            this.grpLoadProfile.Controls.Add(this.txtTimeIntrvl);
            this.grpLoadProfile.Controls.Add(this.chkbxRun);
            this.grpLoadProfile.Controls.Add(this.lblTimeInterval);
            this.grpLoadProfile.Controls.Add(this.cmbDebug);
            this.grpLoadProfile.Controls.Add(this.lblDB);
            this.grpLoadProfile.Controls.Add(this.btnLast);
            this.grpLoadProfile.Controls.Add(this.btnNext);
            this.grpLoadProfile.Controls.Add(this.btnPrev);
            this.grpLoadProfile.Controls.Add(this.btnFirst);
            this.grpLoadProfile.Controls.Add(this.txtMasterNo);
            this.grpLoadProfile.Controls.Add(this.lblMN);
            this.grpLoadProfile.Controls.Add(this.lblHdrText);
            this.grpLoadProfile.Controls.Add(this.pbHdr);
            this.grpLoadProfile.Controls.Add(this.btnCancel);
            this.grpLoadProfile.Controls.Add(this.btnDone);
            this.grpLoadProfile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpLoadProfile.Location = new System.Drawing.Point(365, 100);
            this.grpLoadProfile.Name = "grpLoadProfile";
            this.grpLoadProfile.Size = new System.Drawing.Size(281, 168);
            this.grpLoadProfile.TabIndex = 24;
            this.grpLoadProfile.TabStop = false;
            this.grpLoadProfile.Visible = false;
            // 
            // txtTimeIntrvl
            // 
            this.txtTimeIntrvl.Location = new System.Drawing.Point(111, 55);
            this.txtTimeIntrvl.MaxLength = 4;
            this.txtTimeIntrvl.Name = "txtTimeIntrvl";
            this.txtTimeIntrvl.Size = new System.Drawing.Size(156, 20);
            this.txtTimeIntrvl.TabIndex = 113;
            this.txtTimeIntrvl.Tag = "TimeInterval";
            this.txtTimeIntrvl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTimeIntrvl_KeyPress);
            // 
            // chkbxRun
            // 
            this.chkbxRun.AutoSize = true;
            this.chkbxRun.Location = new System.Drawing.Point(18, 111);
            this.chkbxRun.Name = "chkbxRun";
            this.chkbxRun.Size = new System.Drawing.Size(46, 17);
            this.chkbxRun.TabIndex = 111;
            this.chkbxRun.Tag = "Run_YES_NO";
            this.chkbxRun.Text = "Run";
            this.chkbxRun.UseVisualStyleBackColor = true;
            // 
            // lblTimeInterval
            // 
            this.lblTimeInterval.AutoSize = true;
            this.lblTimeInterval.Location = new System.Drawing.Point(12, 58);
            this.lblTimeInterval.Name = "lblTimeInterval";
            this.lblTimeInterval.Size = new System.Drawing.Size(93, 13);
            this.lblTimeInterval.TabIndex = 109;
            this.lblTimeInterval.Text = "Time Interval (min)";
            // 
            // cmbDebug
            // 
            this.cmbDebug.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDebug.FormattingEnabled = true;
            this.cmbDebug.Location = new System.Drawing.Point(111, 82);
            this.cmbDebug.Name = "cmbDebug";
            this.cmbDebug.Size = new System.Drawing.Size(156, 21);
            this.cmbDebug.TabIndex = 108;
            this.cmbDebug.Tag = "DEBUG";
            // 
            // lblDB
            // 
            this.lblDB.AutoSize = true;
            this.lblDB.Location = new System.Drawing.Point(12, 85);
            this.lblDB.Name = "lblDB";
            this.lblDB.Size = new System.Drawing.Size(68, 13);
            this.lblDB.TabIndex = 107;
            this.lblDB.Text = "Debug Level";
            // 
            // btnLast
            // 
            this.btnLast.FlatAppearance.BorderSize = 0;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast.Location = new System.Drawing.Point(217, 139);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(58, 22);
            this.btnLast.TabIndex = 106;
            this.btnLast.Text = "Last>>";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(150, 139);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(58, 22);
            this.btnNext.TabIndex = 105;
            this.btnNext.Text = "Next>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.FlatAppearance.BorderSize = 0;
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrev.Location = new System.Drawing.Point(79, 139);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(58, 22);
            this.btnPrev.TabIndex = 104;
            this.btnPrev.Text = "<Prev";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.FlatAppearance.BorderSize = 0;
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFirst.Location = new System.Drawing.Point(9, 139);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(58, 22);
            this.btnFirst.TabIndex = 103;
            this.btnFirst.Text = "<<First";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // txtMasterNo
            // 
            this.txtMasterNo.Enabled = false;
            this.txtMasterNo.Location = new System.Drawing.Point(111, 30);
            this.txtMasterNo.Name = "txtMasterNo";
            this.txtMasterNo.Size = new System.Drawing.Size(156, 20);
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
            // lblHdrText
            // 
            this.lblHdrText.AutoSize = true;
            this.lblHdrText.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblHdrText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHdrText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHdrText.Location = new System.Drawing.Point(10, 4);
            this.lblHdrText.Name = "lblHdrText";
            this.lblHdrText.Size = new System.Drawing.Size(117, 13);
            this.lblHdrText.TabIndex = 40;
            this.lblHdrText.Text = "Load Profile Master";
            this.lblHdrText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseDown);
            this.lblHdrText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseMove);
            // 
            // pbHdr
            // 
            this.pbHdr.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pbHdr.Location = new System.Drawing.Point(0, 0);
            this.pbHdr.Name = "pbHdr";
            this.pbHdr.Size = new System.Drawing.Size(315, 22);
            this.pbHdr.TabIndex = 39;
            this.pbHdr.TabStop = false;
            this.pbHdr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseDown);
            this.pbHdr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseMove);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(199, 110);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 28);
            this.btnCancel.TabIndex = 102;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDone
            // 
            this.btnDone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDone.Location = new System.Drawing.Point(111, 110);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(68, 28);
            this.btnDone.TabIndex = 101;
            this.btnDone.Text = "&Update";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblMMC);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1050, 30);
            this.panel1.TabIndex = 27;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 650);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1050, 50);
            this.panel2.TabIndex = 28;
            // 
            // ucGroupLoadProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpLoadProfile);
            this.Controls.Add(this.lvLoadProfilemaster);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ucGroupLoadProfile";
            this.Size = new System.Drawing.Size(1050, 700);
            this.grpLoadProfile.ResumeLayout(false);
            this.grpLoadProfile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView lvLoadProfilemaster;
        private System.Windows.Forms.Label lblMMC;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Button btnAdd;
        public System.Windows.Forms.GroupBox grpLoadProfile;
        public System.Windows.Forms.TextBox txtMasterNo;
        private System.Windows.Forms.Label lblMN;
        private System.Windows.Forms.Label lblHdrText;
        private System.Windows.Forms.PictureBox pbHdr;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.ComboBox cmbDebug;
        private System.Windows.Forms.Label lblDB;
        private System.Windows.Forms.Label lblTimeInterval;
        public System.Windows.Forms.CheckBox chkbxRun;
        public System.Windows.Forms.TextBox txtTimeIntrvl;
    }
}
