//Ajay: 09/01/2019 Form Created
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenProPlusConfigurator
{
    public partial class frmInput : Form
    {
        public string defaultFileName = "openproplus_config.xml";
        public frmInput()
        {
            InitializeComponent();
        }

        private void frmInput_Load(object sender, EventArgs e)
        {
            txtbxInput.Text = Utils.ZipDirName;//defaultFileName;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbxInput.Text))
            {
                MessageBox.Show("File name is empty!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            else if(string.IsNullOrEmpty(Path.GetExtension(txtbxInput.Text)))
            {
                MessageBox.Show("File extension is missing!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmInput_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(txtbxInput.Text.Trim()) && !string.IsNullOrEmpty(Path.GetExtension(txtbxInput.Text)))
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = false;
            }
        }
    }
}
