namespace OpenProPlusConfigurator
{
    partial class ucMasterLoadProfile
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
            this.lvIEDList = new System.Windows.Forms.ListView();
            this.lblIED = new System.Windows.Forms.Label();
            this.grpLP = new System.Windows.Forms.GroupBox();
            this.txtTimeInrvl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkbxRun = new System.Windows.Forms.CheckBox();
            this.txtDebug = new System.Windows.Forms.TextBox();
            this.lblDebug = new System.Windows.Forms.Label();
            this.txtMasterNo = new System.Windows.Forms.TextBox();
            this.lblMN = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.grpIED = new System.Windows.Forms.GroupBox();
            this.txtUnitID = new System.Windows.Forms.TextBox();
            this.txtTxOffsetCurrent = new System.Windows.Forms.TextBox();
            this.lblTXOffsetCurrent = new System.Windows.Forms.Label();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDS = new System.Windows.Forms.Label();
            this.lblUnitID = new System.Windows.Forms.Label();
            this.txtDevice = new System.Windows.Forms.TextBox();
            this.lblDevice = new System.Windows.Forms.Label();
            this.txtTapRatio = new System.Windows.Forms.TextBox();
            this.lblTapRatio = new System.Windows.Forms.Label();
            this.lblHdrText = new System.Windows.Forms.Label();
            this.pbHdr = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.btnExportIED = new System.Windows.Forms.Button();
            this.btnImportIED = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpLP.SuspendLayout();
            this.grpIED.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.lvIEDList.Size = new System.Drawing.Size(1050, 567);
            this.lvIEDList.TabIndex = 11;
            this.lvIEDList.UseCompatibleStateImageBehavior = false;
            this.lvIEDList.View = System.Windows.Forms.View.Details;
            this.lvIEDList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvIEDList_ItemCheck);
            this.lvIEDList.DoubleClick += new System.EventHandler(this.lvIEDList_DoubleClick);
            // 
            // lblIED
            // 
            this.lblIED.AutoSize = true;
            this.lblIED.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblIED.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIED.Location = new System.Drawing.Point(0, 0);
            this.lblIED.Name = "lblIED";
            this.lblIED.Size = new System.Drawing.Size(52, 13);
            this.lblIED.TabIndex = 10;
            this.lblIED.Text = "IED List";
            // 
            // grpLP
            // 
            this.grpLP.Controls.Add(this.txtTimeInrvl);
            this.grpLP.Controls.Add(this.label1);
            this.grpLP.Controls.Add(this.chkbxRun);
            this.grpLP.Controls.Add(this.txtDebug);
            this.grpLP.Controls.Add(this.lblDebug);
            this.grpLP.Controls.Add(this.txtMasterNo);
            this.grpLP.Controls.Add(this.lblMN);
            this.grpLP.Location = new System.Drawing.Point(3, 0);
            this.grpLP.Name = "grpLP";
            this.grpLP.Size = new System.Drawing.Size(477, 70);
            this.grpLP.TabIndex = 14;
            this.grpLP.TabStop = false;
            this.grpLP.Text = "Load Profile Master";
            // 
            // txtTimeInrvl
            // 
            this.txtTimeInrvl.Enabled = false;
            this.txtTimeInrvl.Location = new System.Drawing.Point(316, 16);
            this.txtTimeInrvl.Name = "txtTimeInrvl";
            this.txtTimeInrvl.Size = new System.Drawing.Size(139, 20);
            this.txtTimeInrvl.TabIndex = 114;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(242, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 113;
            this.label1.Text = "Time Interval";
            // 
            // chkbxRun
            // 
            this.chkbxRun.AutoSize = true;
            this.chkbxRun.Enabled = false;
            this.chkbxRun.Location = new System.Drawing.Point(245, 44);
            this.chkbxRun.Name = "chkbxRun";
            this.chkbxRun.Size = new System.Drawing.Size(46, 17);
            this.chkbxRun.TabIndex = 112;
            this.chkbxRun.Tag = "Run_YES_NO";
            this.chkbxRun.Text = "Run";
            this.chkbxRun.UseVisualStyleBackColor = true;
            // 
            // txtDebug
            // 
            this.txtDebug.Enabled = false;
            this.txtDebug.Location = new System.Drawing.Point(81, 42);
            this.txtDebug.Name = "txtDebug";
            this.txtDebug.Size = new System.Drawing.Size(139, 20);
            this.txtDebug.TabIndex = 30;
            // 
            // lblDebug
            // 
            this.lblDebug.AutoSize = true;
            this.lblDebug.Location = new System.Drawing.Point(15, 45);
            this.lblDebug.Name = "lblDebug";
            this.lblDebug.Size = new System.Drawing.Size(39, 13);
            this.lblDebug.TabIndex = 29;
            this.lblDebug.Text = "Debug";
            // 
            // txtMasterNo
            // 
            this.txtMasterNo.Enabled = false;
            this.txtMasterNo.Location = new System.Drawing.Point(81, 16);
            this.txtMasterNo.Name = "txtMasterNo";
            this.txtMasterNo.Size = new System.Drawing.Size(139, 20);
            this.txtMasterNo.TabIndex = 1;
            // 
            // lblMN
            // 
            this.lblMN.AutoSize = true;
            this.lblMN.Location = new System.Drawing.Point(15, 19);
            this.lblMN.Name = "lblMN";
            this.lblMN.Size = new System.Drawing.Size(59, 13);
            this.lblMN.TabIndex = 0;
            this.lblMN.Text = "Master No.";
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
            // grpIED
            // 
            this.grpIED.BackColor = System.Drawing.SystemColors.Control;
            this.grpIED.Controls.Add(this.txtUnitID);
            this.grpIED.Controls.Add(this.txtTxOffsetCurrent);
            this.grpIED.Controls.Add(this.lblTXOffsetCurrent);
            this.grpIED.Controls.Add(this.btnLast);
            this.grpIED.Controls.Add(this.btnNext);
            this.grpIED.Controls.Add(this.btnPrev);
            this.grpIED.Controls.Add(this.btnFirst);
            this.grpIED.Controls.Add(this.txtDescription);
            this.grpIED.Controls.Add(this.lblDS);
            this.grpIED.Controls.Add(this.lblUnitID);
            this.grpIED.Controls.Add(this.txtDevice);
            this.grpIED.Controls.Add(this.lblDevice);
            this.grpIED.Controls.Add(this.txtTapRatio);
            this.grpIED.Controls.Add(this.lblTapRatio);
            this.grpIED.Controls.Add(this.lblHdrText);
            this.grpIED.Controls.Add(this.pbHdr);
            this.grpIED.Controls.Add(this.btnCancel);
            this.grpIED.Controls.Add(this.btnDone);
            this.grpIED.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpIED.Location = new System.Drawing.Point(272, 39);
            this.grpIED.Name = "grpIED";
            this.grpIED.Size = new System.Drawing.Size(286, 216);
            this.grpIED.TabIndex = 27;
            this.grpIED.TabStop = false;
            this.grpIED.Visible = false;
            // 
            // txtUnitID
            // 
            this.txtUnitID.Location = new System.Drawing.Point(117, 30);
            this.txtUnitID.MaxLength = 3;
            this.txtUnitID.Name = "txtUnitID";
            this.txtUnitID.Size = new System.Drawing.Size(155, 20);
            this.txtUnitID.TabIndex = 106;
            this.txtUnitID.Tag = "UnitID";
            this.txtUnitID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUnitID_KeyPress);
            // 
            // txtTxOffsetCurrent
            // 
            this.txtTxOffsetCurrent.Location = new System.Drawing.Point(118, 109);
            this.txtTxOffsetCurrent.Name = "txtTxOffsetCurrent";
            this.txtTxOffsetCurrent.Size = new System.Drawing.Size(155, 20);
            this.txtTxOffsetCurrent.TabIndex = 89;
            this.txtTxOffsetCurrent.Tag = "TXOffsetCurrent";
            this.txtTxOffsetCurrent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTxOffsetCurrent_KeyPress);
            // 
            // lblTXOffsetCurrent
            // 
            this.lblTXOffsetCurrent.AutoSize = true;
            this.lblTXOffsetCurrent.Location = new System.Drawing.Point(14, 112);
            this.lblTXOffsetCurrent.Name = "lblTXOffsetCurrent";
            this.lblTXOffsetCurrent.Size = new System.Drawing.Size(89, 13);
            this.lblTXOffsetCurrent.TabIndex = 88;
            this.lblTXOffsetCurrent.Text = "TX Offset Current";
            // 
            // btnLast
            // 
            this.btnLast.FlatAppearance.BorderSize = 0;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast.Location = new System.Drawing.Point(214, 192);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(58, 22);
            this.btnLast.TabIndex = 102;
            this.btnLast.Text = "Last>>";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(150, 192);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(58, 22);
            this.btnNext.TabIndex = 101;
            this.btnNext.Text = "Next>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.FlatAppearance.BorderSize = 0;
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrev.Location = new System.Drawing.Point(86, 192);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(58, 22);
            this.btnPrev.TabIndex = 100;
            this.btnPrev.Text = "<Prev";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.FlatAppearance.BorderSize = 0;
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFirst.Location = new System.Drawing.Point(22, 192);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(58, 22);
            this.btnFirst.TabIndex = 99;
            this.btnFirst.Text = "<<First";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(118, 135);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(155, 20);
            this.txtDescription.TabIndex = 95;
            this.txtDescription.Tag = "Description";
            this.txtDescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescription_KeyPress);
            // 
            // lblDS
            // 
            this.lblDS.AutoSize = true;
            this.lblDS.Location = new System.Drawing.Point(13, 138);
            this.lblDS.Name = "lblDS";
            this.lblDS.Size = new System.Drawing.Size(60, 13);
            this.lblDS.TabIndex = 94;
            this.lblDS.Text = "Description";
            // 
            // lblUnitID
            // 
            this.lblUnitID.AutoSize = true;
            this.lblUnitID.Location = new System.Drawing.Point(12, 33);
            this.lblUnitID.Name = "lblUnitID";
            this.lblUnitID.Size = new System.Drawing.Size(40, 13);
            this.lblUnitID.TabIndex = 82;
            this.lblUnitID.Text = "Unit ID";
            // 
            // txtDevice
            // 
            this.txtDevice.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDevice.Location = new System.Drawing.Point(118, 57);
            this.txtDevice.MaxLength = 20;
            this.txtDevice.Name = "txtDevice";
            this.txtDevice.Size = new System.Drawing.Size(155, 20);
            this.txtDevice.TabIndex = 85;
            this.txtDevice.Tag = "Device";
            this.txtDevice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDevice_KeyPress);
            // 
            // lblDevice
            // 
            this.lblDevice.AutoSize = true;
            this.lblDevice.Location = new System.Drawing.Point(13, 60);
            this.lblDevice.Name = "lblDevice";
            this.lblDevice.Size = new System.Drawing.Size(41, 13);
            this.lblDevice.TabIndex = 84;
            this.lblDevice.Text = "Device";
            // 
            // txtTapRatio
            // 
            this.txtTapRatio.Location = new System.Drawing.Point(118, 83);
            this.txtTapRatio.Name = "txtTapRatio";
            this.txtTapRatio.Size = new System.Drawing.Size(155, 20);
            this.txtTapRatio.TabIndex = 87;
            this.txtTapRatio.Tag = "DefaultTapRatio";
            this.txtTapRatio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTapRatio_KeyPress);
            // 
            // lblTapRatio
            // 
            this.lblTapRatio.AutoSize = true;
            this.lblTapRatio.Location = new System.Drawing.Point(13, 86);
            this.lblTapRatio.Name = "lblTapRatio";
            this.lblTapRatio.Size = new System.Drawing.Size(91, 13);
            this.lblTapRatio.TabIndex = 86;
            this.lblTapRatio.Text = "Default Tap Ratio";
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
            // pbHdr
            // 
            this.pbHdr.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pbHdr.Location = new System.Drawing.Point(0, 0);
            this.pbHdr.Name = "pbHdr";
            this.pbHdr.Size = new System.Drawing.Size(287, 22);
            this.pbHdr.TabIndex = 39;
            this.pbHdr.TabStop = false;
            this.pbHdr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseDown);
            this.pbHdr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseMove);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(204, 161);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 28);
            this.btnCancel.TabIndex = 98;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDone
            // 
            this.btnDone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDone.Location = new System.Drawing.Point(118, 161);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(68, 28);
            this.btnDone.TabIndex = 97;
            this.btnDone.Text = "&Update";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnExportIED
            // 
            this.btnExportIED.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExportIED.FlatAppearance.BorderSize = 0;
            this.btnExportIED.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExportIED.Location = new System.Drawing.Point(160, 3);
            this.btnExportIED.Name = "btnExportIED";
            this.btnExportIED.Size = new System.Drawing.Size(68, 28);
            this.btnExportIED.TabIndex = 30;
            this.btnExportIED.Text = "E&xport IED";
            this.btnExportIED.UseVisualStyleBackColor = true;
            this.btnExportIED.Click += new System.EventHandler(this.btnExportIED_Click);
            // 
            // btnImportIED
            // 
            this.btnImportIED.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnImportIED.FlatAppearance.BorderSize = 0;
            this.btnImportIED.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnImportIED.Location = new System.Drawing.Point(240, 3);
            this.btnImportIED.Name = "btnImportIED";
            this.btnImportIED.Size = new System.Drawing.Size(68, 28);
            this.btnImportIED.TabIndex = 31;
            this.btnImportIED.Text = "&Import IED";
            this.btnImportIED.UseVisualStyleBackColor = true;
            this.btnImportIED.Click += new System.EventHandler(this.btnImportIED_Click);
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
            this.splitContainer1.Panel1.Controls.Add(this.grpLP);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grpIED);
            this.splitContainer1.Panel2.Controls.Add(this.lvIEDList);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.lblIED);
            this.splitContainer1.Size = new System.Drawing.Size(1050, 700);
            this.splitContainer1.SplitterDistance = 71;
            this.splitContainer1.TabIndex = 32;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnExportIED);
            this.panel1.Controls.Add(this.btnImportIED);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 580);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1050, 45);
            this.panel1.TabIndex = 28;
            // 
            // ucMasterLoadProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucMasterLoadProfile";
            this.Size = new System.Drawing.Size(1050, 700);
            this.grpLP.ResumeLayout(false);
            this.grpLP.PerformLayout();
            this.grpIED.ResumeLayout(false);
            this.grpIED.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView lvIEDList;
        public System.Windows.Forms.Label lblIED;
        private System.Windows.Forms.GroupBox grpLP;
        public System.Windows.Forms.TextBox txtMasterNo;
        private System.Windows.Forms.Label lblMN;
        public System.Windows.Forms.TextBox txtDebug;
        private System.Windows.Forms.Label lblDebug;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Button btnAdd;
        public System.Windows.Forms.GroupBox grpIED;
        private System.Windows.Forms.Label lblUnitID;
        public System.Windows.Forms.TextBox txtDevice;
        private System.Windows.Forms.Label lblDevice;
        public System.Windows.Forms.TextBox txtTapRatio;
        private System.Windows.Forms.Label lblTapRatio;
        private System.Windows.Forms.Label lblHdrText;
        private System.Windows.Forms.PictureBox pbHdr;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDone;
        public System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDS;
        public System.Windows.Forms.Button btnExportIED;
        public System.Windows.Forms.Button btnImportIED;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnFirst;
        public System.Windows.Forms.TextBox txtTxOffsetCurrent;
        private System.Windows.Forms.Label lblTXOffsetCurrent;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.CheckBox chkbxRun;
        public System.Windows.Forms.TextBox txtTimeInrvl;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtUnitID;
    }
}
