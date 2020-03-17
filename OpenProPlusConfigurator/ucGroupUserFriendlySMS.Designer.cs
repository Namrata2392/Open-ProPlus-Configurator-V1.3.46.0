namespace OpenProPlusConfigurator
{
    partial class ucGroupUserFriendlySMS
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lvUFSMS = new System.Windows.Forms.ListView();
            this.TpBtn = new System.Windows.Forms.ToolTip(this.components);
            this.btnDone = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pbHdr = new System.Windows.Forms.PictureBox();
            this.lblHdrText = new System.Windows.Forms.Label();
            this.lblMobileNo = new System.Windows.Forms.Label();
            this.txtMobileNo = new System.Windows.Forms.TextBox();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.grpUFSMS = new System.Windows.Forms.GroupBox();
            this.chkGrantForControl = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).BeginInit();
            this.grpUFSMS.SuspendLayout();
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
            this.lblMSC.Size = new System.Drawing.Size(149, 15);
            this.lblMSC.TabIndex = 4;
            this.lblMSC.Text = "UFSMS Configuration:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 589);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1026, 50);
            this.panel2.TabIndex = 24;
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
            // lvUFSMS
            // 
            this.lvUFSMS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvUFSMS.CheckBoxes = true;
            this.lvUFSMS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvUFSMS.FullRowSelect = true;
            this.lvUFSMS.Location = new System.Drawing.Point(0, 30);
            this.lvUFSMS.MultiSelect = false;
            this.lvUFSMS.Name = "lvUFSMS";
            this.lvUFSMS.Size = new System.Drawing.Size(1026, 559);
            this.lvUFSMS.TabIndex = 25;
            this.lvUFSMS.UseCompatibleStateImageBehavior = false;
            this.lvUFSMS.View = System.Windows.Forms.View.Details;
            this.lvUFSMS.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvUFSMS_ItemCheck);
            this.lvUFSMS.SelectedIndexChanged += new System.EventHandler(this.lvUFSMS_SelectedIndexChanged);
            this.lvUFSMS.DoubleClick += new System.EventHandler(this.lvUFSMS_DoubleClick);
            // 
            // btnDone
            // 
            this.btnDone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDone.Location = new System.Drawing.Point(74, 79);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(68, 28);
            this.btnDone.TabIndex = 75;
            this.btnDone.Text = "&Update";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(189, 79);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 28);
            this.btnCancel.TabIndex = 76;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pbHdr
            // 
            this.pbHdr.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pbHdr.Location = new System.Drawing.Point(0, 0);
            this.pbHdr.Name = "pbHdr";
            this.pbHdr.Size = new System.Drawing.Size(354, 22);
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
            this.lblHdrText.Size = new System.Drawing.Size(49, 13);
            this.lblHdrText.TabIndex = 40;
            this.lblHdrText.Text = "UFSMS";
            this.lblHdrText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseDown);
            this.lblHdrText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseMove);
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
            // txtMobileNo
            // 
            this.txtMobileNo.Enabled = false;
            this.txtMobileNo.Location = new System.Drawing.Point(74, 31);
            this.txtMobileNo.Name = "txtMobileNo";
            this.txtMobileNo.Size = new System.Drawing.Size(183, 20);
            this.txtMobileNo.TabIndex = 57;
            this.txtMobileNo.Tag = "MobileNo";
            // 
            // btnFirst
            // 
            this.btnFirst.FlatAppearance.BorderSize = 0;
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFirst.Location = new System.Drawing.Point(10, 111);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(59, 22);
            this.btnFirst.TabIndex = 77;
            this.btnFirst.Text = "<<First";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.FlatAppearance.BorderSize = 0;
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrev.Location = new System.Drawing.Point(79, 111);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(59, 22);
            this.btnPrev.TabIndex = 78;
            this.btnPrev.Text = "<Prev";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnNext
            // 
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(148, 111);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(59, 22);
            this.btnNext.TabIndex = 79;
            this.btnNext.Text = "Next>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.FlatAppearance.BorderSize = 0;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast.Location = new System.Drawing.Point(217, 111);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(59, 22);
            this.btnLast.TabIndex = 80;
            this.btnLast.Text = "Last>>";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
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
            this.grpUFSMS.Location = new System.Drawing.Point(307, 110);
            this.grpUFSMS.Name = "grpUFSMS";
            this.grpUFSMS.Size = new System.Drawing.Size(295, 140);
            this.grpUFSMS.TabIndex = 26;
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
            // ucGroupUserFriendlySMS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpUFSMS);
            this.Controls.Add(this.lvUFSMS);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ucGroupUserFriendlySMS";
            this.Size = new System.Drawing.Size(1026, 639);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).EndInit();
            this.grpUFSMS.ResumeLayout(false);
            this.grpUFSMS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMSC;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Button btnAdd;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.ListView lvUFSMS;
        private System.Windows.Forms.ToolTip TpBtn;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.PictureBox pbHdr;
        private System.Windows.Forms.Label lblHdrText;
        private System.Windows.Forms.Label lblMobileNo;
        public System.Windows.Forms.TextBox txtMobileNo;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnLast;
        public System.Windows.Forms.GroupBox grpUFSMS;
        public System.Windows.Forms.CheckBox chkGrantForControl;
    }
}
