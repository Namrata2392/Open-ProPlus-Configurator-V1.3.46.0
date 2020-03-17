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
    public partial class ucGroupSMSSlave : UserControl
    {
        public event EventHandler btnAddClick;
        public event EventHandler btnDeleteClick;
        public event EventHandler btnExportINIClick;
        public event EventHandler btnDoneClick;
        public event EventHandler btnCancelClick;
        public event EventHandler btnFirstClick;
        public event EventHandler btnPrevClick;
        public event EventHandler btnNextClick;
        public event EventHandler btnLastClick;
        public event ItemCheckEventHandler lvSMSSlaveItemCheck;
        public event EventHandler lvSMSSlaveDoubleClick;
        public event EventHandler chkUserFriendlySMSCheckedChanged;
        public event EventHandler chkUserFriendlySMSCheckStateChanged;
        public ucGroupSMSSlave()
        {
            InitializeComponent();
            // Ajay: 23/11/2018
            if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
            {
                if (ProtocolGateway.OppSMSSlaveGroup_ReadOnly)
                {
                    btnAdd.Enabled = false;
                    btnDelete.Enabled = false;
                    btnExportINI.Enabled = false;
                    return;
                }
                else { }
            }
        }
        private void chkUserFriendlySMS_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUserFriendlySMSCheckedChanged != null)
                chkUserFriendlySMSCheckedChanged(sender, e);
        }
        private void chkUserFriendlySMS_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkUserFriendlySMSCheckStateChanged != null)
                chkUserFriendlySMSCheckStateChanged(sender, e);
        }
        private void lblHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }
        private void lblHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpSMSSlave, sender, e);
        }
        private void pbHdr_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }
        private void pbHdr_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpSMSSlave, sender, e);
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
        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (btnFirstClick != null)
                btnFirstClick(sender, e);
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (btnPrevClick != null)
                btnPrevClick(sender, e);
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (btnNextClick != null)
                btnNextClick(sender, e);
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            if (btnLastClick != null)
                btnLastClick(sender, e);
        }
        private void lvMQTTSlave_ItemCheck(object sender, ItemCheckEventArgs e)
        {

        }

        private void lvMQTTSlave_DoubleClick(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAddClick != null)
                btnAddClick(sender, e);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (btnDeleteClick != null)
                btnDeleteClick(sender, e);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtPortNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void lvSMSSlave_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lvSMSSlave_DoubleClick(object sender, EventArgs e)
        {
            if (lvSMSSlaveDoubleClick != null)
                lvSMSSlaveDoubleClick(sender, e);
        }
        private void lvSMSSlave_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (lvSMSSlaveItemCheck != null)
                lvSMSSlaveItemCheck(sender, e);
        }

        private void txtRemoteMobileNo_Validating(object sender, CancelEventArgs e)
        {
            if (txtRemoteMobileNo.Text == string.Empty)
            {
                Utils.IsValidate = false;
                errorProvider1.SetError(txtRemoteMobileNo, "Please enter Remote Mobile No.");
                errorProvider2.SetError(txtRemoteMobileNo, "");
                errorProvider3.SetError(txtRemoteMobileNo, "");
            }
            else
            {
                Regex phNum = new Regex("^\\+[0-9]+$");
                Match getmatch = phNum.Match(txtRemoteMobileNo.Text);
                if (getmatch.Success)
                {
                    Utils.IsValidate = true;
                    errorProvider1.SetError(txtRemoteMobileNo, "");
                    errorProvider2.SetError(txtRemoteMobileNo, "");
                    errorProvider3.SetError(txtRemoteMobileNo, "Correct Format");
                }
                else
                {
                    Utils.IsValidate = false;
                    errorProvider1.SetError(txtRemoteMobileNo, "");
                    errorProvider2.SetError(txtRemoteMobileNo, "Mobile No. Should 10 Digits");
                    errorProvider3.SetError(txtRemoteMobileNo, "");
                }
            }
        }

        private void grpSMSSlave_Enter(object sender, EventArgs e)
        {

        }

        private void txtRemoteMobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

       
        private void pbHdr_Click(object sender, EventArgs e)
        {

        }

        private void lblSN_Click(object sender, EventArgs e)
        {

        }
    }
}
