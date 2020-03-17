namespace OpenProPlusConfigurator
{
    partial class frmFTP
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtbxIPAddr = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtbxFTPUser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtbxFTPPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoHTTP = new System.Windows.Forms.RadioButton();
            this.rdoSFTP = new System.Windows.Forms.RadioButton();
            this.rdoFTP = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP Address:";
            // 
            // txtbxIPAddr
            // 
            this.txtbxIPAddr.Location = new System.Drawing.Point(76, 39);
            this.txtbxIPAddr.Name = "txtbxIPAddr";
            this.txtbxIPAddr.Size = new System.Drawing.Size(168, 20);
            this.txtbxIPAddr.TabIndex = 3;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(196, 117);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(48, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtbxFTPUser
            // 
            this.txtbxFTPUser.Location = new System.Drawing.Point(76, 65);
            this.txtbxFTPUser.Name = "txtbxFTPUser";
            this.txtbxFTPUser.Size = new System.Drawing.Size(168, 20);
            this.txtbxFTPUser.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "User:";
            // 
            // txtbxFTPPassword
            // 
            this.txtbxFTPPassword.Location = new System.Drawing.Point(76, 91);
            this.txtbxFTPPassword.Name = "txtbxFTPPassword";
            this.txtbxFTPPassword.PasswordChar = '*';
            this.txtbxFTPPassword.Size = new System.Drawing.Size(168, 20);
            this.txtbxFTPPassword.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Password:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoHTTP);
            this.groupBox1.Controls.Add(this.rdoSFTP);
            this.groupBox1.Controls.Add(this.rdoFTP);
            this.groupBox1.Location = new System.Drawing.Point(76, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(168, 29);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // rdoHTTP
            // 
            this.rdoHTTP.AutoSize = true;
            this.rdoHTTP.Checked = true;
            this.rdoHTTP.Location = new System.Drawing.Point(108, 9);
            this.rdoHTTP.Name = "rdoHTTP";
            this.rdoHTTP.Size = new System.Drawing.Size(54, 17);
            this.rdoHTTP.TabIndex = 2;
            this.rdoHTTP.TabStop = true;
            this.rdoHTTP.Text = "HTTP";
            this.rdoHTTP.UseVisualStyleBackColor = true;
            // 
            // rdoSFTP
            // 
            this.rdoSFTP.AutoSize = true;
            this.rdoSFTP.Location = new System.Drawing.Point(53, 9);
            this.rdoSFTP.Name = "rdoSFTP";
            this.rdoSFTP.Size = new System.Drawing.Size(52, 17);
            this.rdoSFTP.TabIndex = 1;
            this.rdoSFTP.Text = "SFTP";
            this.rdoSFTP.UseVisualStyleBackColor = true;
            // 
            // rdoFTP
            // 
            this.rdoFTP.AutoSize = true;
            this.rdoFTP.Location = new System.Drawing.Point(6, 9);
            this.rdoFTP.Name = "rdoFTP";
            this.rdoFTP.Size = new System.Drawing.Size(45, 17);
            this.rdoFTP.TabIndex = 0;
            this.rdoFTP.Text = "FTP";
            this.rdoFTP.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Protocol:";
            // 
            // frmFTP
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 146);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtbxFTPPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtbxFTPUser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtbxIPAddr);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFTP";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Protocol Configuration";
            this.Load += new System.EventHandler(this.frmFTP_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtbxIPAddr;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtbxFTPUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtbxFTPPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoFTP;
        private System.Windows.Forms.RadioButton rdoSFTP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rdoHTTP;
    }
}