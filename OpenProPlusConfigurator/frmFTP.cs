using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenProPlusConfigurator
{
    public partial class frmFTP : Form
    {
        public frmFTP()
        {
            InitializeComponent();
        }

        private void frmFTP_Load(object sender, EventArgs e)
        {
            try
            {
                this.Clear();
                this.LoadDefault();
            }
            catch { }
        }

        private void Clear()
        {
            txtbxIPAddr.Text = txtbxFTPUser.Text = txtbxFTPPassword.Text = "";
        }
        private void LoadDefault()
        {
            try
            {
                if (Properties.Settings.Default["OppIPAddress"] != null)
                {
                    txtbxIPAddr.Text = Properties.Settings.Default["OppIPAddress"].ToString();
                }
                if (Properties.Settings.Default["FTPUser"] != null)
                {
                    txtbxFTPUser.Text = Properties.Settings.Default["FTPUser"].ToString();
                }
                if (Properties.Settings.Default["FTPPassword"] != null)
                {
                    txtbxFTPPassword.Text = Properties.Settings.Default["FTPPassword"].ToString();
                }
                if (Properties.Settings.Default["FTProtocol"] != null)
                {
                    if(Properties.Settings.Default["FTProtocol"].ToString().ToUpper() == "FTP")
                    {
                        rdoFTP.Checked = true;
                    }
                    else if (Properties.Settings.Default["FTProtocol"].ToString().ToUpper() == "SFTP")
                    {
                        rdoSFTP.Checked = true;
                    }
                    //Ajay: 08/01/2019
                    else if (Properties.Settings.Default["FTProtocol"].ToString().ToUpper() == "HTTP")
                    {
                        rdoHTTP.Checked = true;
                    }
                }
            }
            catch { }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                ProtocolGateway.OppIpAddress = txtbxIPAddr.Text.TrimEnd();
                Properties.Settings.Default.OppIPAddress = txtbxIPAddr.Text.TrimEnd();

                ProtocolGateway.User = txtbxFTPUser.Text.TrimEnd();
                Properties.Settings.Default.FTPUser = txtbxFTPUser.Text.TrimEnd();

                ProtocolGateway.Password = txtbxFTPPassword.Text.TrimEnd();
                Properties.Settings.Default.FTPPassword = txtbxFTPPassword.Text.TrimEnd();

                if (rdoFTP.Checked) { ProtocolGateway.Protocol = "FTP"; }
                else if (rdoSFTP.Checked) { ProtocolGateway.Protocol = "SFTP"; }
                else if (rdoHTTP.Checked) { ProtocolGateway.Protocol = "HTTP"; } //Ajay: 08/01/2019
                Properties.Settings.Default.FTProtocol = ProtocolGateway.Protocol;

                Properties.Settings.Default.Save();

                this.Close();
            }
            catch { }
        }
    }
}
