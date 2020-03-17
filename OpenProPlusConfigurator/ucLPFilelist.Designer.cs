namespace OpenProPlusConfigurator
{
    partial class ucLPFilelist
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
            this.lblLPFilelist = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.grpLPFile = new System.Windows.Forms.GroupBox();
            this.cmbName = new System.Windows.Forms.ComboBox();
            this.btnVerify = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.cmbENNo = new System.Windows.Forms.ComboBox();
            this.lblENNo = new System.Windows.Forms.Label();
            this.cmbAINo = new System.Windows.Forms.ComboBox();
            this.lblAINo = new System.Windows.Forms.Label();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.lblHdrText = new System.Windows.Forms.Label();
            this.pbHdr = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.lblDIRecords = new System.Windows.Forms.Label();
            this.lblDITR = new System.Windows.Forms.Label();
            this.linkLPFile = new System.Windows.Forms.LinkLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lvLPFilelist = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpLPFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblLPFilelist
            // 
            this.lblLPFilelist.AutoSize = true;
            this.lblLPFilelist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLPFilelist.Location = new System.Drawing.Point(3, 5);
            this.lblLPFilelist.Name = "lblLPFilelist";
            this.lblLPFilelist.Size = new System.Drawing.Size(70, 13);
            this.lblLPFilelist.TabIndex = 10;
            this.lblLPFilelist.Text = "LPFile List:";
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(258, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(68, 26);
            this.btnDelete.TabIndex = 29;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Location = new System.Drawing.Point(172, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(68, 26);
            this.btnAdd.TabIndex = 28;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // grpLPFile
            // 
            this.grpLPFile.BackColor = System.Drawing.SystemColors.Control;
            this.grpLPFile.Controls.Add(this.cmbName);
            this.grpLPFile.Controls.Add(this.btnVerify);
            this.grpLPFile.Controls.Add(this.lblName);
            this.grpLPFile.Controls.Add(this.cmbENNo);
            this.grpLPFile.Controls.Add(this.lblENNo);
            this.grpLPFile.Controls.Add(this.cmbAINo);
            this.grpLPFile.Controls.Add(this.lblAINo);
            this.grpLPFile.Controls.Add(this.btnLast);
            this.grpLPFile.Controls.Add(this.btnNext);
            this.grpLPFile.Controls.Add(this.btnPrev);
            this.grpLPFile.Controls.Add(this.btnFirst);
            this.grpLPFile.Controls.Add(this.lblHdrText);
            this.grpLPFile.Controls.Add(this.pbHdr);
            this.grpLPFile.Controls.Add(this.btnCancel);
            this.grpLPFile.Controls.Add(this.btnDone);
            this.grpLPFile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpLPFile.Location = new System.Drawing.Point(193, 145);
            this.grpLPFile.Name = "grpLPFile";
            this.grpLPFile.Size = new System.Drawing.Size(324, 175);
            this.grpLPFile.TabIndex = 27;
            this.grpLPFile.TabStop = false;
            this.grpLPFile.Visible = false;
            // 
            // cmbName
            // 
            this.cmbName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbName.FormattingEnabled = true;
            this.cmbName.Location = new System.Drawing.Point(93, 85);
            this.cmbName.Name = "cmbName";
            this.cmbName.Size = new System.Drawing.Size(215, 21);
            this.cmbName.TabIndex = 170;
            this.cmbName.Tag = "Name";
            // 
            // btnVerify
            // 
            this.btnVerify.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnVerify.Location = new System.Drawing.Point(19, 116);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(80, 28);
            this.btnVerify.TabIndex = 168;
            this.btnVerify.Tag = "Verify";
            this.btnVerify.Text = "&Verify";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(16, 88);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 127;
            this.lblName.Text = "Name";
            // 
            // cmbENNo
            // 
            this.cmbENNo.FormattingEnabled = true;
            this.cmbENNo.Location = new System.Drawing.Point(93, 58);
            this.cmbENNo.Name = "cmbENNo";
            this.cmbENNo.Size = new System.Drawing.Size(215, 21);
            this.cmbENNo.TabIndex = 126;
            this.cmbENNo.Tag = "ENNo";
            this.cmbENNo.TextChanged += new System.EventHandler(this.cmbENNo_TextChanged);
            this.cmbENNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbENNo_KeyPress);
            // 
            // lblENNo
            // 
            this.lblENNo.AutoSize = true;
            this.lblENNo.Location = new System.Drawing.Point(16, 63);
            this.lblENNo.Name = "lblENNo";
            this.lblENNo.Size = new System.Drawing.Size(39, 13);
            this.lblENNo.TabIndex = 125;
            this.lblENNo.Text = "EN No";
            // 
            // cmbAINo
            // 
            this.cmbAINo.FormattingEnabled = true;
            this.cmbAINo.Location = new System.Drawing.Point(93, 32);
            this.cmbAINo.Name = "cmbAINo";
            this.cmbAINo.Size = new System.Drawing.Size(215, 21);
            this.cmbAINo.TabIndex = 124;
            this.cmbAINo.Tag = "AINo";
            this.cmbAINo.TextChanged += new System.EventHandler(this.cmbAINo_TextChanged);
            this.cmbAINo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbAINo_KeyPress);
            // 
            // lblAINo
            // 
            this.lblAINo.AutoSize = true;
            this.lblAINo.Location = new System.Drawing.Point(16, 35);
            this.lblAINo.Name = "lblAINo";
            this.lblAINo.Size = new System.Drawing.Size(34, 13);
            this.lblAINo.TabIndex = 123;
            this.lblAINo.Text = "AI No";
            // 
            // btnLast
            // 
            this.btnLast.FlatAppearance.BorderSize = 0;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast.Location = new System.Drawing.Point(237, 149);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(58, 22);
            this.btnLast.TabIndex = 114;
            this.btnLast.Text = "Last>>";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(165, 149);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(58, 22);
            this.btnNext.TabIndex = 113;
            this.btnNext.Text = "Next>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.FlatAppearance.BorderSize = 0;
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrev.Location = new System.Drawing.Point(93, 149);
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
            this.btnFirst.Location = new System.Drawing.Point(21, 149);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(58, 22);
            this.btnFirst.TabIndex = 111;
            this.btnFirst.Text = "<<First";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // lblHdrText
            // 
            this.lblHdrText.AutoSize = true;
            this.lblHdrText.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblHdrText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHdrText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHdrText.Location = new System.Drawing.Point(10, 4);
            this.lblHdrText.Name = "lblHdrText";
            this.lblHdrText.Size = new System.Drawing.Size(48, 13);
            this.lblHdrText.TabIndex = 40;
            this.lblHdrText.Text = "LPFILE";
            this.lblHdrText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseDown);
            this.lblHdrText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseMove);
            // 
            // pbHdr
            // 
            this.pbHdr.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pbHdr.Location = new System.Drawing.Point(0, 0);
            this.pbHdr.Name = "pbHdr";
            this.pbHdr.Size = new System.Drawing.Size(350, 22);
            this.pbHdr.TabIndex = 39;
            this.pbHdr.TabStop = false;
            this.pbHdr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseDown);
            this.pbHdr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseMove);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(233, 117);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 27);
            this.btnCancel.TabIndex = 106;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDone
            // 
            this.btnDone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDone.Location = new System.Drawing.Point(133, 116);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(75, 27);
            this.btnDone.TabIndex = 105;
            this.btnDone.Text = "&Update";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // lblDIRecords
            // 
            this.lblDIRecords.AutoSize = true;
            this.lblDIRecords.Location = new System.Drawing.Point(145, 5);
            this.lblDIRecords.Name = "lblDIRecords";
            this.lblDIRecords.Size = new System.Drawing.Size(13, 13);
            this.lblDIRecords.TabIndex = 127;
            this.lblDIRecords.Text = "0";
            // 
            // lblDITR
            // 
            this.lblDITR.AutoSize = true;
            this.lblDITR.Location = new System.Drawing.Point(70, 5);
            this.lblDITR.Name = "lblDITR";
            this.lblDITR.Size = new System.Drawing.Size(77, 13);
            this.lblDITR.TabIndex = 126;
            this.lblDITR.Text = "Total Records:";
            // 
            // linkLPFile
            // 
            this.linkLPFile.AutoSize = true;
            this.linkLPFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLPFile.LinkColor = System.Drawing.Color.DarkSlateBlue;
            this.linkLPFile.Location = new System.Drawing.Point(3, 30);
            this.linkLPFile.Name = "linkLPFile";
            this.linkLPFile.Size = new System.Drawing.Size(113, 13);
            this.linkLPFile.TabIndex = 137;
            this.linkLPFile.TabStop = true;
            this.linkLPFile.Text = "Delete All Records";
            this.linkLPFile.Click += new System.EventHandler(this.linkLPFile_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grpLPFile);
            this.splitContainer1.Panel1.Controls.Add(this.lvLPFilelist);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1050, 700);
            this.splitContainer1.SplitterDistance = 670;
            this.splitContainer1.TabIndex = 138;
            // 
            // lvLPFilelist
            // 
            this.lvLPFilelist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvLPFilelist.CheckBoxes = true;
            this.lvLPFilelist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLPFilelist.FullRowSelect = true;
            this.lvLPFilelist.HideSelection = false;
            this.lvLPFilelist.Location = new System.Drawing.Point(0, 51);
            this.lvLPFilelist.MultiSelect = false;
            this.lvLPFilelist.Name = "lvLPFilelist";
            this.lvLPFilelist.Size = new System.Drawing.Size(1050, 619);
            this.lvLPFilelist.TabIndex = 12;
            this.lvLPFilelist.UseCompatibleStateImageBehavior = false;
            this.lvLPFilelist.View = System.Windows.Forms.View.Details;
            this.lvLPFilelist.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDIlist_ItemSelectionChanged);
            this.lvLPFilelist.SelectedIndexChanged += new System.EventHandler(this.lvLPFilelist_SelectedIndexChanged);
            this.lvLPFilelist.DoubleClick += new System.EventHandler(this.lvLPFilelist_DoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblLPFilelist);
            this.panel1.Controls.Add(this.linkLPFile);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.lblDIRecords);
            this.panel1.Controls.Add(this.lblDITR);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1050, 51);
            this.panel1.TabIndex = 13;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // ucLPFilelist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucLPFilelist";
            this.Size = new System.Drawing.Size(1050, 700);
            this.Load += new System.EventHandler(this.ucLPFilelist_Load);
            this.grpLPFile.ResumeLayout(false);
            this.grpLPFile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblLPFilelist;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Button btnAdd;
        public System.Windows.Forms.GroupBox grpLPFile;
        private System.Windows.Forms.Label lblHdrText;
        public System.Windows.Forms.Label lblDIRecords;
        private System.Windows.Forms.Label lblDITR;
        public System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button btnDone;
        public System.Windows.Forms.Button btnLast;
        public System.Windows.Forms.Button btnNext;
        public System.Windows.Forms.Button btnPrev;
        public System.Windows.Forms.Button btnFirst;
        public System.Windows.Forms.LinkLabel linkLPFile;
        public System.Windows.Forms.Label lblName;
        public System.Windows.Forms.ComboBox cmbENNo;
        public System.Windows.Forms.Label lblENNo;
        public System.Windows.Forms.ComboBox cmbAINo;
        public System.Windows.Forms.Label lblAINo;
        public System.Windows.Forms.PictureBox pbHdr;
        private System.Windows.Forms.SplitContainer splitContainer1;
        public System.Windows.Forms.ListView lvLPFilelist;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button btnVerify;
        public System.Windows.Forms.ComboBox cmbName;
    }
}
