using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
namespace OpenProPlusConfigurator
{
    /**
    * \brief     <b>ucDetails</b> is a user interface to display device details
    * \details   This is a user interface to display device details. The user can only modify it's parameters.
    * 
    * 
    */
    public partial class ucDetails : UserControl
    {
        public event EventHandler btnEditClick;
        public event EventHandler btnDoneClick;
        public event EventHandler btnCancelClick;
        public event EventHandler lvDetailsDoubleClick;
        public event EventHandler lvDetailsSizeChanged;
        public event EventHandler ucDetailsLoad;
        public ucDetails()
        {
            InitializeComponent();
            //Utils.createPBTitleBar(pbHdr, lblHdrText, this.PointToScreen(lblHdrText.Location));
            txtDescription.MaxLength = Globals.MAX_DESCRIPTION_LEN;
        }

        private void txtXMLVersion_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEditClick != null)
                btnEditClick(sender, e);
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (btnDoneClick != null)
                btnDoneClick(sender, e);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (btnCancelClick != null)
                btnCancelClick(sender, e);
        }

        private void pbHdr_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void pbHdr_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpDetails, sender, e);
        }

        private void lblHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void lblHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpDetails, sender, e);
        }

        private void lblHwVersion_Click(object sender, EventArgs e)
        {

        }

        private void lvDetails_DoubleClick(object sender, EventArgs e)
        {
            if (lvDetailsDoubleClick != null)
                lvDetailsDoubleClick(sender, e);
        }

        private void lvDetails_SizeChanged(object sender, EventArgs e)
        {
            if (lvDetailsSizeChanged != null)
                lvDetailsSizeChanged(sender, e);
        }
        private void ucDetails_Load(object sender, EventArgs e)
        {
            if (ucDetailsLoad != null)
                ucDetailsLoad(sender, e);
        }

        //Namrata:28/01/2019
        private void txtDeviceType_KeyPress(object sender, KeyPressEventArgs e)
        {
            #region Hostname may not start with a hyphen.
            if (e.KeyChar == '-')
            {
                if (txtDeviceType.TextLength < 1)
                    e.Handled = true;
            }
            else { e.Handled = false; }
            #endregion Hostname may not start with a hyphen.
            Utils.SpecialCharacter_Validation(e);
        }

        private void txtDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SpecialCharacter_Validation(e);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtDeviceType_TextChanged(object sender, EventArgs e)
        {
            #region ASCII letters from a to z, the digits from 0 to 9, and the hyphen (-).
           // Regex phNum = new Regex("^[a-z0-9-]+$");
           // Match getmatch = phNum.Match(txtDeviceType.Text);
           // if (getmatch.Success)
           // {
           //     OPP.IsValidDeviceType = true;
           //     errorProvider1.SetError(txtDeviceType, "");
           //     errorProvider2.SetError(txtDeviceType, "");
           //     errorProvider3.SetError(txtDeviceType, "Correct Format");
           // }
           //else
           // {
           //     OPP.IsValidDeviceType = false;
           //     errorProvider1.SetError(txtDeviceType, "");
           //     errorProvider2.SetError(txtDeviceType, "DeviceType Should 0-9a-z- Format");
           //     errorProvider3.SetError(txtDeviceType, "");
           // }
            #endregion ASCII letters from a to z, the digits from 0 to 9, and the hyphen (-).                                                                                                                                                                                                                                 

            //if (txtDeviceType.MaxLength <= 253)
            //{
            //    errorProvider1.SetError(txtDeviceType, "");
            //    errorProvider2.SetError(txtDeviceType, "");
            //    errorProvider3.SetError(txtDeviceType, "ASCII letters Correct Format");
            //}
            //else if (txtDeviceType.Text == "" || txtDeviceType.MaxLength > 253)
            //{
            //    errorProvider1.SetError(txtDeviceType, "");
            //    errorProvider2.SetError(txtDeviceType, "DeviceType MaxLength 253 Characters");
            //    errorProvider3.SetError(txtDeviceType, "");
            //}

        }

        private void lblMaximize_Click(object sender, EventArgs e)
        {
            if (lblMaximize.Text == ">>")
            {
                lblMaximize.Text = "<<";
                groupBox1.Show();
            }
            else
            {
                lblMaximize.Text = ">>";
                groupBox1.Hide();
              
            }
        }
    }
}
