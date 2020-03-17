namespace OpenProPlusConfigurator
{
    partial class SchemaValidationLog
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
            this.components = new System.ComponentModel.Container();
            this.listViewLog = new System.Windows.Forms.ListView();
            this.NotVisibleColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStripLog = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSclFileButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.contextMenuStripLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewLog
            // 
            this.listViewLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NotVisibleColumnHeader});
            this.listViewLog.ContextMenuStrip = this.contextMenuStripLog;
            this.listViewLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewLog.Location = new System.Drawing.Point(0, 0);
            this.listViewLog.MultiSelect = false;
            this.listViewLog.Name = "listViewLog";
            this.listViewLog.Size = new System.Drawing.Size(695, 302);
            this.listViewLog.TabIndex = 0;
            this.listViewLog.UseCompatibleStateImageBehavior = false;
            this.listViewLog.View = System.Windows.Forms.View.Details;
            this.listViewLog.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listViewLog_ItemSelectionChanged);
            this.listViewLog.SizeChanged += new System.EventHandler(this.listViewLog_SizeChanged);
            // 
            // NotVisibleColumnHeader
            // 
            this.NotVisibleColumnHeader.Text = "NOT_VISIBLE";
            this.NotVisibleColumnHeader.Width = 25;
            // 
            // contextMenuStripLog
            // 
            this.contextMenuStripLog.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem});
            this.contextMenuStripLog.Name = "contextMenuStrip1";
            this.contextMenuStripLog.Size = new System.Drawing.Size(103, 26);
            this.contextMenuStripLog.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripLog_Opening);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // openSclFileButton
            // 
            this.openSclFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.openSclFileButton.Location = new System.Drawing.Point(506, 308);
            this.openSclFileButton.Name = "openSclFileButton";
            this.openSclFileButton.Size = new System.Drawing.Size(96, 23);
            this.openSclFileButton.TabIndex = 1;
            this.openSclFileButton.Text = "Open SCL file";
            this.openSclFileButton.UseVisualStyleBackColor = true;
            this.openSclFileButton.Click += new System.EventHandler(this.openSclFileButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(608, 308);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // SchemaValidationLog
            // 
            this.AcceptButton = this.openSclFileButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(695, 343);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.openSclFileButton);
            this.Controls.Add(this.listViewLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(400, 250);
            this.Name = "SchemaValidationLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SCL Schema Validation Log";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SchemaValidationLog_FormClosing);
            this.Load += new System.EventHandler(this.SchemaValidationLog_Load);
            this.Shown += new System.EventHandler(this.SchemaValidationLog_Shown);
            this.contextMenuStripLog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewLog;
        private System.Windows.Forms.ColumnHeader NotVisibleColumnHeader;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripLog;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.Button openSclFileButton;
        private System.Windows.Forms.Button cancelButton;
    }
}