namespace OpenProPlusConfigurator
{
    partial class ucGroupUser
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
            this.lvUserList = new System.Windows.Forms.ListView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.grpDNPSA = new System.Windows.Forms.GroupBox();
            this.txtHMAC = new System.Windows.Forms.TextBox();
            this.txtAEEVC = new System.Windows.Forms.TextBox();
            this.ChkAEE = new System.Windows.Forms.CheckBox();
            this.chkAggMode = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAEC = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtKIT = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtKIC = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtART = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBI = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.grpUser = new System.Windows.Forms.GroupBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.txtUserNo = new System.Windows.Forms.TextBox();
            this.lblMobileNo = new System.Windows.Forms.Label();
            this.lblHdrText = new System.Windows.Forms.Label();
            this.pbHdr = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.grpDNPSA.SuspendLayout();
            this.grpUser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).BeginInit();
            this.SuspendLayout();
            // 
            // lvUserList
            // 
            this.lvUserList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvUserList.CheckBoxes = true;
            this.lvUserList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvUserList.FullRowSelect = true;
            this.lvUserList.Location = new System.Drawing.Point(0, 13);
            this.lvUserList.MultiSelect = false;
            this.lvUserList.Name = "lvUserList";
            this.lvUserList.Size = new System.Drawing.Size(997, 342);
            this.lvUserList.TabIndex = 11;
            this.lvUserList.UseCompatibleStateImageBehavior = false;
            this.lvUserList.View = System.Windows.Forms.View.Details;
            this.lvUserList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvUserList_ItemCheck);
            this.lvUserList.SelectedIndexChanged += new System.EventHandler(this.lvUserList_SelectedIndexChanged);
            this.lvUserList.Click += new System.EventHandler(this.lvUserList_Click);
            this.lvUserList.DoubleClick += new System.EventHandler(this.lvUserList_DoubleClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 596);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(997, 50);
            this.panel2.TabIndex = 42;
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
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.grpDNPSA);
            this.splitContainer2.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer2_Panel1_Paint);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.grpUser);
            this.splitContainer2.Panel2.Controls.Add(this.lvUserList);
            this.splitContainer2.Panel2.Controls.Add(this.label2);
            this.splitContainer2.Size = new System.Drawing.Size(997, 596);
            this.splitContainer2.SplitterDistance = 237;
            this.splitContainer2.TabIndex = 43;
            // 
            // grpDNPSA
            // 
            this.grpDNPSA.Controls.Add(this.txtHMAC);
            this.grpDNPSA.Controls.Add(this.txtAEEVC);
            this.grpDNPSA.Controls.Add(this.ChkAEE);
            this.grpDNPSA.Controls.Add(this.chkAggMode);
            this.grpDNPSA.Controls.Add(this.label3);
            this.grpDNPSA.Controls.Add(this.label1);
            this.grpDNPSA.Controls.Add(this.txtAEC);
            this.grpDNPSA.Controls.Add(this.label10);
            this.grpDNPSA.Controls.Add(this.txtKIT);
            this.grpDNPSA.Controls.Add(this.label4);
            this.grpDNPSA.Controls.Add(this.txtKIC);
            this.grpDNPSA.Controls.Add(this.label5);
            this.grpDNPSA.Controls.Add(this.txtART);
            this.grpDNPSA.Controls.Add(this.label6);
            this.grpDNPSA.Controls.Add(this.txtBI);
            this.grpDNPSA.Controls.Add(this.label15);
            this.grpDNPSA.Location = new System.Drawing.Point(4, 4);
            this.grpDNPSA.Name = "grpDNPSA";
            this.grpDNPSA.Size = new System.Drawing.Size(415, 224);
            this.grpDNPSA.TabIndex = 15;
            this.grpDNPSA.TabStop = false;
            this.grpDNPSA.Text = "DNPSA";
            // 
            // txtHMAC
            // 
            this.txtHMAC.Enabled = false;
            this.txtHMAC.Location = new System.Drawing.Point(195, 149);
            this.txtHMAC.Name = "txtHMAC";
            this.txtHMAC.Size = new System.Drawing.Size(183, 20);
            this.txtHMAC.TabIndex = 186;
            this.txtHMAC.Tag = "HMACAlgo";
            // 
            // txtAEEVC
            // 
            this.txtAEEVC.Enabled = false;
            this.txtAEEVC.Location = new System.Drawing.Point(195, 124);
            this.txtAEEVC.Name = "txtAEEVC";
            this.txtAEEVC.Size = new System.Drawing.Size(183, 20);
            this.txtAEEVC.TabIndex = 185;
            this.txtAEEVC.Tag = "AuthErrEventClass";
            // 
            // ChkAEE
            // 
            this.ChkAEE.AutoSize = true;
            this.ChkAEE.Enabled = false;
            this.ChkAEE.Location = new System.Drawing.Point(195, 199);
            this.ChkAEE.Name = "ChkAEE";
            this.ChkAEE.Size = new System.Drawing.Size(150, 17);
            this.ChkAEE.TabIndex = 184;
            this.ChkAEE.Tag = "AuthErrEvent_YES_NO";
            this.ChkAEE.Text = "Authentication Error Event";
            this.ChkAEE.UseVisualStyleBackColor = true;
            // 
            // chkAggMode
            // 
            this.chkAggMode.AutoSize = true;
            this.chkAggMode.Enabled = false;
            this.chkAggMode.Location = new System.Drawing.Point(195, 176);
            this.chkAggMode.Name = "chkAggMode";
            this.chkAggMode.Size = new System.Drawing.Size(108, 17);
            this.chkAggMode.TabIndex = 182;
            this.chkAggMode.Tag = "AggressiveM_YES_NO";
            this.chkAggMode.Text = "Aggressive Mode";
            this.chkAggMode.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 181;
            this.label3.Text = "HMAC Algorithm";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 13);
            this.label1.TabIndex = 180;
            this.label1.Text = "Authentication Error Event Class";
            // 
            // txtAEC
            // 
            this.txtAEC.Enabled = false;
            this.txtAEC.Location = new System.Drawing.Point(195, 99);
            this.txtAEC.Name = "txtAEC";
            this.txtAEC.Size = new System.Drawing.Size(183, 20);
            this.txtAEC.TabIndex = 179;
            this.txtAEC.Tag = "MaxAuthErrCount";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 102);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(175, 13);
            this.label10.TabIndex = 178;
            this.label10.Text = "MaximumnAuthetication Error Count";
            // 
            // txtKIT
            // 
            this.txtKIT.Enabled = false;
            this.txtKIT.Location = new System.Drawing.Point(195, 49);
            this.txtKIT.Name = "txtKIT";
            this.txtKIT.Size = new System.Drawing.Size(183, 20);
            this.txtKIT.TabIndex = 177;
            this.txtKIT.Tag = "SSIVTime";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(176, 13);
            this.label4.TabIndex = 176;
            this.label4.Text = "Session Key Invalidation Time (Sec)";
            // 
            // txtKIC
            // 
            this.txtKIC.Enabled = false;
            this.txtKIC.Location = new System.Drawing.Point(195, 74);
            this.txtKIC.Name = "txtKIC";
            this.txtKIC.Size = new System.Drawing.Size(183, 20);
            this.txtKIC.TabIndex = 175;
            this.txtKIC.Tag = "SSIVCount";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(181, 13);
            this.label5.TabIndex = 174;
            this.label5.Text = "Session Key Invalidation Count (Sec)";
            // 
            // txtART
            // 
            this.txtART.Enabled = false;
            this.txtART.Location = new System.Drawing.Point(195, 24);
            this.txtART.Name = "txtART";
            this.txtART.Size = new System.Drawing.Size(183, 20);
            this.txtART.TabIndex = 173;
            this.txtART.Tag = "ReplyTOut";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(174, 13);
            this.label6.TabIndex = 172;
            this.label6.Text = "Authentication Reply Timeout (Sec)";
            // 
            // txtBI
            // 
            this.txtBI.Location = new System.Drawing.Point(130, -30);
            this.txtBI.Name = "txtBI";
            this.txtBI.Size = new System.Drawing.Size(183, 20);
            this.txtBI.TabIndex = 160;
            this.txtBI.Tag = "BI";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(2, -27);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(63, 13);
            this.label15.TabIndex = 159;
            this.label15.Text = "Binary Input";
            // 
            // grpUser
            // 
            this.grpUser.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.grpUser.Controls.Add(this.txtUserName);
            this.grpUser.Controls.Add(this.label9);
            this.grpUser.Controls.Add(this.txtKey);
            this.grpUser.Controls.Add(this.label8);
            this.grpUser.Controls.Add(this.btnLast);
            this.grpUser.Controls.Add(this.btnNext);
            this.grpUser.Controls.Add(this.btnPrev);
            this.grpUser.Controls.Add(this.btnFirst);
            this.grpUser.Controls.Add(this.txtUserNo);
            this.grpUser.Controls.Add(this.lblMobileNo);
            this.grpUser.Controls.Add(this.lblHdrText);
            this.grpUser.Controls.Add(this.pbHdr);
            this.grpUser.Controls.Add(this.btnCancel);
            this.grpUser.Controls.Add(this.btnDone);
            this.grpUser.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpUser.Location = new System.Drawing.Point(215, 47);
            this.grpUser.Name = "grpUser";
            this.grpUser.Size = new System.Drawing.Size(424, 181);
            this.grpUser.TabIndex = 30;
            this.grpUser.TabStop = false;
            this.grpUser.Visible = false;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(77, 65);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(327, 20);
            this.txtUserName.TabIndex = 138;
            this.txtUserName.Tag = "Name";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 65);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 13);
            this.label9.TabIndex = 137;
            this.label9.Text = "User Name";
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(77, 90);
            this.txtKey.MaxLength = 47;
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(327, 20);
            this.txtKey.TabIndex = 136;
            this.txtKey.Tag = "Key";
            this.txtKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtKey_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 13);
            this.label8.TabIndex = 135;
            this.label8.Text = "Key";
            // 
            // btnLast
            // 
            this.btnLast.FlatAppearance.BorderSize = 0;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast.Location = new System.Drawing.Point(345, 149);
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
            this.btnNext.Location = new System.Drawing.Point(232, 149);
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
            this.btnPrev.Location = new System.Drawing.Point(119, 149);
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
            this.btnFirst.Location = new System.Drawing.Point(6, 149);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(59, 22);
            this.btnFirst.TabIndex = 77;
            this.btnFirst.Text = "<<First";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // txtUserNo
            // 
            this.txtUserNo.Location = new System.Drawing.Point(77, 40);
            this.txtUserNo.Name = "txtUserNo";
            this.txtUserNo.Size = new System.Drawing.Size(327, 20);
            this.txtUserNo.TabIndex = 57;
            this.txtUserNo.Tag = "No";
            // 
            // lblMobileNo
            // 
            this.lblMobileNo.AutoSize = true;
            this.lblMobileNo.Location = new System.Drawing.Point(6, 40);
            this.lblMobileNo.Name = "lblMobileNo";
            this.lblMobileNo.Size = new System.Drawing.Size(46, 13);
            this.lblMobileNo.TabIndex = 56;
            this.lblMobileNo.Text = "User No";
            // 
            // lblHdrText
            // 
            this.lblHdrText.AutoSize = true;
            this.lblHdrText.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblHdrText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHdrText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHdrText.Location = new System.Drawing.Point(10, 4);
            this.lblHdrText.Name = "lblHdrText";
            this.lblHdrText.Size = new System.Drawing.Size(33, 13);
            this.lblHdrText.TabIndex = 40;
            this.lblHdrText.Text = "User";
            this.lblHdrText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseDown);
            this.lblHdrText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseMove);
            // 
            // pbHdr
            // 
            this.pbHdr.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pbHdr.Location = new System.Drawing.Point(0, 0);
            this.pbHdr.Name = "pbHdr";
            this.pbHdr.Size = new System.Drawing.Size(424, 22);
            this.pbHdr.TabIndex = 39;
            this.pbHdr.TabStop = false;
            this.pbHdr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseDown);
            this.pbHdr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseMove);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(232, 116);
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
            this.btnDone.Location = new System.Drawing.Point(136, 116);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(68, 28);
            this.btnDone.TabIndex = 75;
            this.btnDone.Text = "&Update";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "User List";
            // 
            // ucGroupUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.panel2);
            this.Name = "ucGroupUser";
            this.Size = new System.Drawing.Size(997, 646);
            this.panel2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.grpDNPSA.ResumeLayout(false);
            this.grpDNPSA.PerformLayout();
            this.grpUser.ResumeLayout(false);
            this.grpUser.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.ListView lvUserList;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox grpDNPSA;
        public System.Windows.Forms.GroupBox grpUser;
        public System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnFirst;
        public System.Windows.Forms.TextBox txtUserNo;
        private System.Windows.Forms.Label lblMobileNo;
        private System.Windows.Forms.Label lblHdrText;
        private System.Windows.Forms.PictureBox pbHdr;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDone;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtBI;
        private System.Windows.Forms.Label label15;
        public System.Windows.Forms.CheckBox ChkAEE;
        public System.Windows.Forms.CheckBox chkAggMode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtAEC;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox txtKIT;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtKIC;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtART;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox txtHMAC;
        public System.Windows.Forms.TextBox txtAEEVC;
    }
}
