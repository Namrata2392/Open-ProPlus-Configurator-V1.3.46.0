namespace OpenProPlusConfigurator
{
    partial class ucGroupEncryption
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
            this.grpEncryption = new System.Windows.Forms.GroupBox();
            this.txtCA = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDHP = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPK = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCer = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.txtCS = new System.Windows.Forms.TextBox();
            this.lblMobileNo = new System.Windows.Forms.Label();
            this.lblHdrText = new System.Windows.Forms.Label();
            this.pbHdr = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.lvEncryptionList = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblIMC = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.grpEncryption.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpEncryption
            // 
            this.grpEncryption.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.grpEncryption.Controls.Add(this.txtCA);
            this.grpEncryption.Controls.Add(this.label10);
            this.grpEncryption.Controls.Add(this.txtDHP);
            this.grpEncryption.Controls.Add(this.label11);
            this.grpEncryption.Controls.Add(this.txtPK);
            this.grpEncryption.Controls.Add(this.label9);
            this.grpEncryption.Controls.Add(this.txtCer);
            this.grpEncryption.Controls.Add(this.label8);
            this.grpEncryption.Controls.Add(this.btnLast);
            this.grpEncryption.Controls.Add(this.btnNext);
            this.grpEncryption.Controls.Add(this.btnPrev);
            this.grpEncryption.Controls.Add(this.btnFirst);
            this.grpEncryption.Controls.Add(this.txtCS);
            this.grpEncryption.Controls.Add(this.lblMobileNo);
            this.grpEncryption.Controls.Add(this.lblHdrText);
            this.grpEncryption.Controls.Add(this.pbHdr);
            this.grpEncryption.Controls.Add(this.btnCancel);
            this.grpEncryption.Controls.Add(this.btnDone);
            this.grpEncryption.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpEncryption.Location = new System.Drawing.Point(249, 75);
            this.grpEncryption.Name = "grpEncryption";
            this.grpEncryption.Size = new System.Drawing.Size(475, 230);
            this.grpEncryption.TabIndex = 29;
            this.grpEncryption.TabStop = false;
            this.grpEncryption.Visible = false;
            // 
            // txtCA
            // 
            this.txtCA.Location = new System.Drawing.Point(94, 115);
            this.txtCA.Name = "txtCA";
            this.txtCA.Size = new System.Drawing.Size(361, 20);
            this.txtCA.TabIndex = 142;
            this.txtCA.Tag = "CA";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 115);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(21, 13);
            this.label10.TabIndex = 141;
            this.label10.Text = "CA";
            // 
            // txtDHP
            // 
            this.txtDHP.Location = new System.Drawing.Point(94, 141);
            this.txtDHP.Name = "txtDHP";
            this.txtDHP.Size = new System.Drawing.Size(361, 20);
            this.txtDHP.TabIndex = 140;
            this.txtDHP.Tag = "DHParameter";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 141);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 13);
            this.label11.TabIndex = 139;
            this.label11.Text = "DH Parameter";
            // 
            // txtPK
            // 
            this.txtPK.Location = new System.Drawing.Point(94, 63);
            this.txtPK.MaxLength = 47;
            this.txtPK.Name = "txtPK";
            this.txtPK.Size = new System.Drawing.Size(361, 20);
            this.txtPK.TabIndex = 138;
            this.txtPK.Tag = "PrivateKey";
            this.txtPK.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPK_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 63);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 137;
            this.label9.Text = "Private Key";
            // 
            // txtCer
            // 
            this.txtCer.Location = new System.Drawing.Point(94, 89);
            this.txtCer.Name = "txtCer";
            this.txtCer.Size = new System.Drawing.Size(361, 20);
            this.txtCer.TabIndex = 136;
            this.txtCer.Tag = "Certificate";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 89);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 135;
            this.label8.Text = "Certificate";
            // 
            // btnLast
            // 
            this.btnLast.FlatAppearance.BorderSize = 0;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast.Location = new System.Drawing.Point(396, 198);
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
            this.btnNext.Location = new System.Drawing.Point(266, 201);
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
            this.btnPrev.Location = new System.Drawing.Point(136, 201);
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
            this.btnFirst.Location = new System.Drawing.Point(6, 198);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(59, 22);
            this.btnFirst.TabIndex = 77;
            this.btnFirst.Text = "<<First";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // txtCS
            // 
            this.txtCS.Location = new System.Drawing.Point(94, 37);
            this.txtCS.Name = "txtCS";
            this.txtCS.Size = new System.Drawing.Size(361, 20);
            this.txtCS.TabIndex = 57;
            this.txtCS.Tag = "CipherSuite";
            // 
            // lblMobileNo
            // 
            this.lblMobileNo.AutoSize = true;
            this.lblMobileNo.Location = new System.Drawing.Point(6, 37);
            this.lblMobileNo.Name = "lblMobileNo";
            this.lblMobileNo.Size = new System.Drawing.Size(64, 13);
            this.lblMobileNo.TabIndex = 56;
            this.lblMobileNo.Text = "Cipher Suite";
            // 
            // lblHdrText
            // 
            this.lblHdrText.AutoSize = true;
            this.lblHdrText.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblHdrText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHdrText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHdrText.Location = new System.Drawing.Point(10, 4);
            this.lblHdrText.Name = "lblHdrText";
            this.lblHdrText.Size = new System.Drawing.Size(67, 13);
            this.lblHdrText.TabIndex = 40;
            this.lblHdrText.Text = "Encryption";
            this.lblHdrText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseDown);
            this.lblHdrText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseMove);
            // 
            // pbHdr
            // 
            this.pbHdr.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pbHdr.Location = new System.Drawing.Point(0, 0);
            this.pbHdr.Name = "pbHdr";
            this.pbHdr.Size = new System.Drawing.Size(475, 22);
            this.pbHdr.TabIndex = 39;
            this.pbHdr.TabStop = false;
            this.pbHdr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseDown);
            this.pbHdr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseMove);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(273, 167);
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
            this.btnDone.Location = new System.Drawing.Point(158, 167);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(68, 28);
            this.btnDone.TabIndex = 75;
            this.btnDone.Text = "&Update";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // lvEncryptionList
            // 
            this.lvEncryptionList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvEncryptionList.CheckBoxes = true;
            this.lvEncryptionList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvEncryptionList.FullRowSelect = true;
            this.lvEncryptionList.Location = new System.Drawing.Point(0, 33);
            this.lvEncryptionList.MultiSelect = false;
            this.lvEncryptionList.Name = "lvEncryptionList";
            this.lvEncryptionList.Size = new System.Drawing.Size(1089, 571);
            this.lvEncryptionList.TabIndex = 11;
            this.lvEncryptionList.UseCompatibleStateImageBehavior = false;
            this.lvEncryptionList.View = System.Windows.Forms.View.Details;
            this.lvEncryptionList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvEncryptionList_ItemCheck);
            this.lvEncryptionList.DoubleClick += new System.EventHandler(this.lvEncryptionList_DoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblIMC);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1089, 33);
            this.panel1.TabIndex = 37;
            // 
            // lblIMC
            // 
            this.lblIMC.AutoSize = true;
            this.lblIMC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIMC.Location = new System.Drawing.Point(3, 2);
            this.lblIMC.Name = "lblIMC";
            this.lblIMC.Size = new System.Drawing.Size(82, 15);
            this.lblIMC.TabIndex = 7;
            this.lblIMC.Text = "Encryption :";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 604);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1089, 50);
            this.panel2.TabIndex = 38;
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(80, 9);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(68, 28);
            this.btnDelete.TabIndex = 23;
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
            this.btnAdd.TabIndex = 22;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // ucGroupEncryption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpEncryption);
            this.Controls.Add(this.lvEncryptionList);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ucGroupEncryption";
            this.Size = new System.Drawing.Size(1089, 654);
            this.grpEncryption.ResumeLayout(false);
            this.grpEncryption.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.GroupBox grpEncryption;
        public System.Windows.Forms.TextBox txtCA;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox txtDHP;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.TextBox txtPK;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox txtCer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnFirst;
        public System.Windows.Forms.TextBox txtCS;
        private System.Windows.Forms.Label lblMobileNo;
        private System.Windows.Forms.Label lblHdrText;
        private System.Windows.Forms.PictureBox pbHdr;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDone;
        public System.Windows.Forms.ListView lvEncryptionList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblIMC;
    }
}
