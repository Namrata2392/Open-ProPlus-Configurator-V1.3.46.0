namespace OpenProPlusConfigurator
{
    partial class ucGroupGraphicalDisplaySlave
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
            this.grpGraphicalDisplay = new System.Windows.Forms.GroupBox();
            this.txtType = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.chkRun = new System.Windows.Forms.CheckBox();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.cmbDebug = new System.Windows.Forms.ComboBox();
            this.txtSlaveNum = new System.Windows.Forms.TextBox();
            this.lblSN = new System.Windows.Forms.Label();
            this.lblDebug = new System.Windows.Forms.Label();
            this.txtFirmwareVersion = new System.Windows.Forms.TextBox();
            this.lblAFV = new System.Windows.Forms.Label();
            this.txtGridColumns = new System.Windows.Forms.TextBox();
            this.lblGridColumns = new System.Windows.Forms.Label();
            this.txtGridRows = new System.Windows.Forms.TextBox();
            this.lblGridRows = new System.Windows.Forms.Label();
            this.lblHdrText = new System.Windows.Forms.Label();
            this.pbHdr = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.lvGraphicalDisplay = new System.Windows.Forms.ListView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblISC = new System.Windows.Forms.Label();
            this.txtEQS = new System.Windows.Forms.TextBox();
            this.lblEQS = new System.Windows.Forms.Label();
            this.grpGraphicalDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpGraphicalDisplay
            // 
            this.grpGraphicalDisplay.BackColor = System.Drawing.SystemColors.Control;
            this.grpGraphicalDisplay.Controls.Add(this.lblEQS);
            this.grpGraphicalDisplay.Controls.Add(this.txtEQS);
            this.grpGraphicalDisplay.Controls.Add(this.txtType);
            this.grpGraphicalDisplay.Controls.Add(this.lblType);
            this.grpGraphicalDisplay.Controls.Add(this.chkRun);
            this.grpGraphicalDisplay.Controls.Add(this.btnLast);
            this.grpGraphicalDisplay.Controls.Add(this.btnNext);
            this.grpGraphicalDisplay.Controls.Add(this.btnPrev);
            this.grpGraphicalDisplay.Controls.Add(this.btnFirst);
            this.grpGraphicalDisplay.Controls.Add(this.cmbDebug);
            this.grpGraphicalDisplay.Controls.Add(this.txtSlaveNum);
            this.grpGraphicalDisplay.Controls.Add(this.lblSN);
            this.grpGraphicalDisplay.Controls.Add(this.lblDebug);
            this.grpGraphicalDisplay.Controls.Add(this.txtFirmwareVersion);
            this.grpGraphicalDisplay.Controls.Add(this.lblAFV);
            this.grpGraphicalDisplay.Controls.Add(this.txtGridColumns);
            this.grpGraphicalDisplay.Controls.Add(this.lblGridColumns);
            this.grpGraphicalDisplay.Controls.Add(this.txtGridRows);
            this.grpGraphicalDisplay.Controls.Add(this.lblGridRows);
            this.grpGraphicalDisplay.Controls.Add(this.lblHdrText);
            this.grpGraphicalDisplay.Controls.Add(this.pbHdr);
            this.grpGraphicalDisplay.Controls.Add(this.btnCancel);
            this.grpGraphicalDisplay.Controls.Add(this.btnDone);
            this.grpGraphicalDisplay.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpGraphicalDisplay.Location = new System.Drawing.Point(148, 84);
            this.grpGraphicalDisplay.Name = "grpGraphicalDisplay";
            this.grpGraphicalDisplay.Size = new System.Drawing.Size(324, 268);
            this.grpGraphicalDisplay.TabIndex = 19;
            this.grpGraphicalDisplay.TabStop = false;
            this.grpGraphicalDisplay.Visible = false;
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(121, 54);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(181, 20);
            this.txtType.TabIndex = 122;
            this.txtType.Tag = "Type";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(10, 57);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(31, 13);
            this.lblType.TabIndex = 121;
            this.lblType.Text = "Type";
            // 
            // chkRun
            // 
            this.chkRun.AutoSize = true;
            this.chkRun.Location = new System.Drawing.Point(12, 207);
            this.chkRun.Name = "chkRun";
            this.chkRun.Size = new System.Drawing.Size(46, 17);
            this.chkRun.TabIndex = 120;
            this.chkRun.Tag = "Run_YES_NO";
            this.chkRun.Text = "Run";
            this.chkRun.UseVisualStyleBackColor = true;
            // 
            // btnLast
            // 
            this.btnLast.FlatAppearance.BorderSize = 0;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast.Location = new System.Drawing.Point(243, 237);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(59, 22);
            this.btnLast.TabIndex = 119;
            this.btnLast.Text = "Last>>";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(166, 237);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(59, 22);
            this.btnNext.TabIndex = 118;
            this.btnNext.Text = "Next>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.FlatAppearance.BorderSize = 0;
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrev.Location = new System.Drawing.Point(89, 237);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(59, 22);
            this.btnPrev.TabIndex = 117;
            this.btnPrev.Text = "<Prev";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.FlatAppearance.BorderSize = 0;
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFirst.Location = new System.Drawing.Point(12, 237);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(59, 22);
            this.btnFirst.TabIndex = 116;
            this.btnFirst.Text = "<<First";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // cmbDebug
            // 
            this.cmbDebug.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDebug.FormattingEnabled = true;
            this.cmbDebug.Location = new System.Drawing.Point(121, 174);
            this.cmbDebug.Name = "cmbDebug";
            this.cmbDebug.Size = new System.Drawing.Size(181, 21);
            this.cmbDebug.TabIndex = 91;
            this.cmbDebug.Tag = "DEBUG";
            // 
            // txtSlaveNum
            // 
            this.txtSlaveNum.Enabled = false;
            this.txtSlaveNum.Location = new System.Drawing.Point(121, 30);
            this.txtSlaveNum.Name = "txtSlaveNum";
            this.txtSlaveNum.Size = new System.Drawing.Size(181, 20);
            this.txtSlaveNum.TabIndex = 57;
            this.txtSlaveNum.Tag = "SlaveNum";
            // 
            // lblSN
            // 
            this.lblSN.AutoSize = true;
            this.lblSN.Location = new System.Drawing.Point(10, 33);
            this.lblSN.Name = "lblSN";
            this.lblSN.Size = new System.Drawing.Size(54, 13);
            this.lblSN.TabIndex = 56;
            this.lblSN.Text = "Slave No.";
            // 
            // lblDebug
            // 
            this.lblDebug.AutoSize = true;
            this.lblDebug.Location = new System.Drawing.Point(10, 177);
            this.lblDebug.Name = "lblDebug";
            this.lblDebug.Size = new System.Drawing.Size(68, 13);
            this.lblDebug.TabIndex = 90;
            this.lblDebug.Text = "Debug Level";
            // 
            // txtFirmwareVersion
            // 
            this.txtFirmwareVersion.Enabled = false;
            this.txtFirmwareVersion.Location = new System.Drawing.Point(121, 150);
            this.txtFirmwareVersion.Name = "txtFirmwareVersion";
            this.txtFirmwareVersion.Size = new System.Drawing.Size(181, 20);
            this.txtFirmwareVersion.TabIndex = 89;
            this.txtFirmwareVersion.Tag = "AppFirmwareVersion";
            // 
            // lblAFV
            // 
            this.lblAFV.AutoSize = true;
            this.lblAFV.Location = new System.Drawing.Point(10, 153);
            this.lblAFV.Name = "lblAFV";
            this.lblAFV.Size = new System.Drawing.Size(87, 13);
            this.lblAFV.TabIndex = 88;
            this.lblAFV.Text = "Firmware Version";
            // 
            // txtGridColumns
            // 
            this.txtGridColumns.Enabled = false;
            this.txtGridColumns.Location = new System.Drawing.Point(121, 102);
            this.txtGridColumns.Name = "txtGridColumns";
            this.txtGridColumns.Size = new System.Drawing.Size(181, 20);
            this.txtGridColumns.TabIndex = 87;
            this.txtGridColumns.Tag = "GridColumns";
            // 
            // lblGridColumns
            // 
            this.lblGridColumns.AutoSize = true;
            this.lblGridColumns.Location = new System.Drawing.Point(10, 105);
            this.lblGridColumns.Name = "lblGridColumns";
            this.lblGridColumns.Size = new System.Drawing.Size(66, 13);
            this.lblGridColumns.TabIndex = 86;
            this.lblGridColumns.Text = "GridColumns";
            // 
            // txtGridRows
            // 
            this.txtGridRows.Enabled = false;
            this.txtGridRows.Location = new System.Drawing.Point(121, 78);
            this.txtGridRows.Name = "txtGridRows";
            this.txtGridRows.Size = new System.Drawing.Size(181, 20);
            this.txtGridRows.TabIndex = 65;
            this.txtGridRows.Tag = "GridRows";
            // 
            // lblGridRows
            // 
            this.lblGridRows.AutoSize = true;
            this.lblGridRows.Location = new System.Drawing.Point(10, 81);
            this.lblGridRows.Name = "lblGridRows";
            this.lblGridRows.Size = new System.Drawing.Size(56, 13);
            this.lblGridRows.TabIndex = 64;
            this.lblGridRows.Text = "Grid Rows";
            // 
            // lblHdrText
            // 
            this.lblHdrText.AutoSize = true;
            this.lblHdrText.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblHdrText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHdrText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHdrText.Location = new System.Drawing.Point(10, 4);
            this.lblHdrText.Name = "lblHdrText";
            this.lblHdrText.Size = new System.Drawing.Size(142, 13);
            this.lblHdrText.TabIndex = 40;
            this.lblHdrText.Text = "Graphical Display Slave";
            this.lblHdrText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseDown);
            this.lblHdrText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseMove);
            // 
            // pbHdr
            // 
            this.pbHdr.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pbHdr.Location = new System.Drawing.Point(0, 0);
            this.pbHdr.Name = "pbHdr";
            this.pbHdr.Size = new System.Drawing.Size(324, 22);
            this.pbHdr.TabIndex = 39;
            this.pbHdr.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(234, 204);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 28);
            this.btnCancel.TabIndex = 94;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDone
            // 
            this.btnDone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDone.Location = new System.Drawing.Point(121, 204);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(68, 28);
            this.btnDone.TabIndex = 93;
            this.btnDone.Text = "&Update";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // lvGraphicalDisplay
            // 
            this.lvGraphicalDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvGraphicalDisplay.CheckBoxes = true;
            this.lvGraphicalDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvGraphicalDisplay.FullRowSelect = true;
            this.lvGraphicalDisplay.Location = new System.Drawing.Point(0, 30);
            this.lvGraphicalDisplay.MultiSelect = false;
            this.lvGraphicalDisplay.Name = "lvGraphicalDisplay";
            this.lvGraphicalDisplay.Size = new System.Drawing.Size(1045, 465);
            this.lvGraphicalDisplay.TabIndex = 20;
            this.lvGraphicalDisplay.UseCompatibleStateImageBehavior = false;
            this.lvGraphicalDisplay.View = System.Windows.Forms.View.Details;
            this.lvGraphicalDisplay.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvGraphicalDisplay_ItemCheck);
            this.lvGraphicalDisplay.DoubleClick += new System.EventHandler(this.lvGraphicalDisplay_DoubleClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 495);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1045, 46);
            this.panel2.TabIndex = 28;
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Location = new System.Drawing.Point(3, 9);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(68, 28);
            this.btnAdd.TabIndex = 23;
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
            this.btnDelete.TabIndex = 24;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblISC);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1045, 30);
            this.panel1.TabIndex = 29;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // lblISC
            // 
            this.lblISC.AutoSize = true;
            this.lblISC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblISC.Location = new System.Drawing.Point(2, 2);
            this.lblISC.Name = "lblISC";
            this.lblISC.Size = new System.Drawing.Size(253, 15);
            this.lblISC.TabIndex = 5;
            this.lblISC.Text = "Graphical Display Slave Configuration:";
            // 
            // txtEQS
            // 
            this.txtEQS.Location = new System.Drawing.Point(121, 126);
            this.txtEQS.Name = "txtEQS";
            this.txtEQS.Size = new System.Drawing.Size(181, 20);
            this.txtEQS.TabIndex = 124;
            this.txtEQS.Tag = "EventQSize";
            // 
            // lblEQS
            // 
            this.lblEQS.AutoSize = true;
            this.lblEQS.Location = new System.Drawing.Point(10, 129);
            this.lblEQS.Name = "lblEQS";
            this.lblEQS.Size = new System.Drawing.Size(93, 13);
            this.lblEQS.TabIndex = 125;
            this.lblEQS.Text = "Event Queue Size";
            // 
            // ucGroupGraphicalDisplaySlave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpGraphicalDisplay);
            this.Controls.Add(this.lvGraphicalDisplay);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "ucGroupGraphicalDisplaySlave";
            this.Size = new System.Drawing.Size(1045, 541);
            this.grpGraphicalDisplay.ResumeLayout(false);
            this.grpGraphicalDisplay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.GroupBox grpGraphicalDisplay;
        private System.Windows.Forms.Label lblSN;
        private System.Windows.Forms.Label lblDebug;
        public System.Windows.Forms.TextBox txtFirmwareVersion;
        private System.Windows.Forms.Label lblAFV;
        public System.Windows.Forms.TextBox txtGridColumns;
        private System.Windows.Forms.Label lblGridColumns;
        public System.Windows.Forms.TextBox txtGridRows;
        private System.Windows.Forms.Label lblGridRows;
        private System.Windows.Forms.Label lblHdrText;
        private System.Windows.Forms.PictureBox pbHdr;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDone;
        public System.Windows.Forms.ListView lvGraphicalDisplay;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Button btnAdd;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.Label lblType;
        public System.Windows.Forms.CheckBox chkRun;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnFirst;
        public System.Windows.Forms.ComboBox cmbDebug;
        public System.Windows.Forms.TextBox txtSlaveNum;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblISC;
        public System.Windows.Forms.TextBox txtEQS;
        private System.Windows.Forms.Label lblEQS;
    }
}
