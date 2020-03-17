namespace OpenProPlusConfigurator
{
    partial class ucGroupUnsolicited
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
            this.grpUnsolicited = new System.Windows.Forms.GroupBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.txtORD = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRD = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtC3ME = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtC3MD = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtC2ME = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtC2MD = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtC1ME = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtC1MD = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMR = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDestAdd = new System.Windows.Forms.TextBox();
            this.lblMobileNo = new System.Windows.Forms.Label();
            this.lblHdrText = new System.Windows.Forms.Label();
            this.pbHdr = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.lvUnsolicitedList = new System.Windows.Forms.ListView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblIMC = new System.Windows.Forms.Label();
            this.grpUnsolicited.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpUnsolicited
            // 
            this.grpUnsolicited.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.grpUnsolicited.Controls.Add(this.checkBox3);
            this.grpUnsolicited.Controls.Add(this.txtORD);
            this.grpUnsolicited.Controls.Add(this.label1);
            this.grpUnsolicited.Controls.Add(this.txtRD);
            this.grpUnsolicited.Controls.Add(this.label14);
            this.grpUnsolicited.Controls.Add(this.txtC3ME);
            this.grpUnsolicited.Controls.Add(this.label12);
            this.grpUnsolicited.Controls.Add(this.txtC3MD);
            this.grpUnsolicited.Controls.Add(this.label13);
            this.grpUnsolicited.Controls.Add(this.txtC2ME);
            this.grpUnsolicited.Controls.Add(this.label10);
            this.grpUnsolicited.Controls.Add(this.txtC2MD);
            this.grpUnsolicited.Controls.Add(this.label11);
            this.grpUnsolicited.Controls.Add(this.txtC1ME);
            this.grpUnsolicited.Controls.Add(this.label9);
            this.grpUnsolicited.Controls.Add(this.txtC1MD);
            this.grpUnsolicited.Controls.Add(this.label8);
            this.grpUnsolicited.Controls.Add(this.txtMR);
            this.grpUnsolicited.Controls.Add(this.label7);
            this.grpUnsolicited.Controls.Add(this.txtDestAdd);
            this.grpUnsolicited.Controls.Add(this.lblMobileNo);
            this.grpUnsolicited.Controls.Add(this.lblHdrText);
            this.grpUnsolicited.Controls.Add(this.pbHdr);
            this.grpUnsolicited.Controls.Add(this.btnCancel);
            this.grpUnsolicited.Controls.Add(this.btnDone);
            this.grpUnsolicited.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpUnsolicited.Location = new System.Drawing.Point(262, 174);
            this.grpUnsolicited.Name = "grpUnsolicited";
            this.grpUnsolicited.Size = new System.Drawing.Size(402, 357);
            this.grpUnsolicited.TabIndex = 29;
            this.grpUnsolicited.TabStop = false;
            this.grpUnsolicited.Visible = false;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(201, 295);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(117, 17);
            this.checkBox3.TabIndex = 154;
            this.checkBox3.Tag = "AllowNull";
            this.checkBox3.Text = "AllowNullResponse";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // txtORD
            // 
            this.txtORD.Location = new System.Drawing.Point(201, 268);
            this.txtORD.Name = "txtORD";
            this.txtORD.Size = new System.Drawing.Size(183, 20);
            this.txtORD.TabIndex = 150;
            this.txtORD.Tag = "OfflineRetryDelay";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 271);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 13);
            this.label1.TabIndex = 149;
            this.label1.Text = "Offline Retry Delay ( 0-42949672 msec)";
            // 
            // txtRD
            // 
            this.txtRD.Location = new System.Drawing.Point(201, 242);
            this.txtRD.Name = "txtRD";
            this.txtRD.Size = new System.Drawing.Size(183, 20);
            this.txtRD.TabIndex = 148;
            this.txtRD.Tag = "RetryDelay";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 245);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(159, 13);
            this.label14.TabIndex = 147;
            this.label14.Text = "Retry Delay ( 0-42949672 msec)";
            // 
            // txtC3ME
            // 
            this.txtC3ME.Location = new System.Drawing.Point(201, 164);
            this.txtC3ME.Name = "txtC3ME";
            this.txtC3ME.Size = new System.Drawing.Size(183, 20);
            this.txtC3ME.TabIndex = 146;
            this.txtC3ME.Tag = "C3MaxEvents";
            this.txtC3ME.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtC3ME_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 167);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(97, 13);
            this.label12.TabIndex = 145;
            this.label12.Text = "Class3 Max Events";
            // 
            // txtC3MD
            // 
            this.txtC3MD.Location = new System.Drawing.Point(201, 190);
            this.txtC3MD.Name = "txtC3MD";
            this.txtC3MD.Size = new System.Drawing.Size(183, 20);
            this.txtC3MD.TabIndex = 144;
            this.txtC3MD.Tag = "C3MaxDelay";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 193);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(188, 13);
            this.label13.TabIndex = 143;
            this.label13.Text = "Class3 Max Delay ( 0-42949672 msec)";
            // 
            // txtC2ME
            // 
            this.txtC2ME.Location = new System.Drawing.Point(201, 112);
            this.txtC2ME.Name = "txtC2ME";
            this.txtC2ME.Size = new System.Drawing.Size(183, 20);
            this.txtC2ME.TabIndex = 142;
            this.txtC2ME.Tag = "C2MaxEvents";
            this.txtC2ME.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtC2ME_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 115);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(97, 13);
            this.label10.TabIndex = 141;
            this.label10.Text = "Class2 Max Events";
            // 
            // txtC2MD
            // 
            this.txtC2MD.Location = new System.Drawing.Point(201, 138);
            this.txtC2MD.Name = "txtC2MD";
            this.txtC2MD.Size = new System.Drawing.Size(183, 20);
            this.txtC2MD.TabIndex = 140;
            this.txtC2MD.Tag = "C2MaxDelay";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 141);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(188, 13);
            this.label11.TabIndex = 139;
            this.label11.Text = "Class2 Max Delay ( 0-42949672 msec)";
            // 
            // txtC1ME
            // 
            this.txtC1ME.Location = new System.Drawing.Point(201, 60);
            this.txtC1ME.Name = "txtC1ME";
            this.txtC1ME.Size = new System.Drawing.Size(183, 20);
            this.txtC1ME.TabIndex = 138;
            this.txtC1ME.Tag = "C1MaxEvents";
            this.txtC1ME.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtC1ME_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 63);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 13);
            this.label9.TabIndex = 137;
            this.label9.Text = "Class1 Max Events";
            // 
            // txtC1MD
            // 
            this.txtC1MD.Location = new System.Drawing.Point(201, 86);
            this.txtC1MD.Name = "txtC1MD";
            this.txtC1MD.Size = new System.Drawing.Size(183, 20);
            this.txtC1MD.TabIndex = 136;
            this.txtC1MD.Tag = "C1MaxDelay";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 89);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(188, 13);
            this.label8.TabIndex = 135;
            this.label8.Text = "Class1 Max Delay ( 0-42949672 msec)";
            // 
            // txtMR
            // 
            this.txtMR.Location = new System.Drawing.Point(201, 216);
            this.txtMR.Name = "txtMR";
            this.txtMR.Size = new System.Drawing.Size(183, 20);
            this.txtMR.TabIndex = 134;
            this.txtMR.Tag = "MaxRetry";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 219);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(135, 13);
            this.label7.TabIndex = 133;
            this.label7.Text = "Maximum Retries (0-65535)";
            // 
            // txtDestAdd
            // 
            this.txtDestAdd.Location = new System.Drawing.Point(201, 34);
            this.txtDestAdd.Name = "txtDestAdd";
            this.txtDestAdd.Size = new System.Drawing.Size(183, 20);
            this.txtDestAdd.TabIndex = 57;
            this.txtDestAdd.Tag = "DestAdd";
            // 
            // lblMobileNo
            // 
            this.lblMobileNo.AutoSize = true;
            this.lblMobileNo.Location = new System.Drawing.Point(6, 37);
            this.lblMobileNo.Name = "lblMobileNo";
            this.lblMobileNo.Size = new System.Drawing.Size(101, 13);
            this.lblMobileNo.TabIndex = 56;
            this.lblMobileNo.Text = "Destination Address";
            // 
            // lblHdrText
            // 
            this.lblHdrText.AutoSize = true;
            this.lblHdrText.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblHdrText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHdrText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHdrText.Location = new System.Drawing.Point(10, 4);
            this.lblHdrText.Name = "lblHdrText";
            this.lblHdrText.Size = new System.Drawing.Size(130, 13);
            this.lblHdrText.TabIndex = 40;
            this.lblHdrText.Text = "Unsolicited Response";
            this.lblHdrText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseDown);
            this.lblHdrText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseMove);
            // 
            // pbHdr
            // 
            this.pbHdr.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pbHdr.Location = new System.Drawing.Point(0, 0);
            this.pbHdr.Name = "pbHdr";
            this.pbHdr.Size = new System.Drawing.Size(402, 22);
            this.pbHdr.TabIndex = 39;
            this.pbHdr.TabStop = false;
            this.pbHdr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseDown);
            this.pbHdr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseMove);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(316, 318);
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
            this.btnDone.Location = new System.Drawing.Point(201, 318);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(68, 28);
            this.btnDone.TabIndex = 75;
            this.btnDone.Text = "&Update";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // lvUnsolicitedList
            // 
            this.lvUnsolicitedList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvUnsolicitedList.CheckBoxes = true;
            this.lvUnsolicitedList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvUnsolicitedList.FullRowSelect = true;
            this.lvUnsolicitedList.Location = new System.Drawing.Point(0, 30);
            this.lvUnsolicitedList.MultiSelect = false;
            this.lvUnsolicitedList.Name = "lvUnsolicitedList";
            this.lvUnsolicitedList.Size = new System.Drawing.Size(989, 696);
            this.lvUnsolicitedList.TabIndex = 11;
            this.lvUnsolicitedList.UseCompatibleStateImageBehavior = false;
            this.lvUnsolicitedList.View = System.Windows.Forms.View.Details;
            this.lvUnsolicitedList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvUnsolicitedList_ItemCheck);
            this.lvUnsolicitedList.DoubleClick += new System.EventHandler(this.lvUnsolicitedList_DoubleClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 726);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(989, 50);
            this.panel2.TabIndex = 30;
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
            // panel1
            // 
            this.panel1.Controls.Add(this.lblIMC);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(989, 30);
            this.panel1.TabIndex = 26;
            // 
            // lblIMC
            // 
            this.lblIMC.AutoSize = true;
            this.lblIMC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIMC.Location = new System.Drawing.Point(3, 3);
            this.lblIMC.Name = "lblIMC";
            this.lblIMC.Size = new System.Drawing.Size(241, 15);
            this.lblIMC.TabIndex = 6;
            this.lblIMC.Text = "Unsolicited Response Configuration:";
            // 
            // ucGroupUnsolicited
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpUnsolicited);
            this.Controls.Add(this.lvUnsolicitedList);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "ucGroupUnsolicited";
            this.Size = new System.Drawing.Size(989, 776);
            this.grpUnsolicited.ResumeLayout(false);
            this.grpUnsolicited.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.GroupBox grpUnsolicited;
        public System.Windows.Forms.TextBox txtDestAdd;
        private System.Windows.Forms.Label lblMobileNo;
        private System.Windows.Forms.Label lblHdrText;
        private System.Windows.Forms.PictureBox pbHdr;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDone;
        public System.Windows.Forms.ListView lvUnsolicitedList;
        public System.Windows.Forms.TextBox txtORD;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtRD;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.TextBox txtC3ME;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.TextBox txtC3MD;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.TextBox txtC2ME;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox txtC2MD;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.TextBox txtC1ME;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox txtC1MD;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox txtMR;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblIMC;
    }
}
