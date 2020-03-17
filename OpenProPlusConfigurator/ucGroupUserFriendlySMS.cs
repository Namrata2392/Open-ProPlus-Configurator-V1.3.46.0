using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenProPlusConfigurator
{
    public partial class ucGroupUserFriendlySMS : UserControl
    {
        public event EventHandler btnAddClick;
        public event EventHandler btnDeleteClick;
        public event EventHandler btnDoneClick;
        public event EventHandler btnCancelClick;
        public event EventHandler btnFirstClick;
        public event EventHandler btnPrevClick;
        public event EventHandler btnNextClick;
        public event EventHandler btnLastClick;
        public event ItemCheckEventHandler lvUFSMSItemCheck;
        public event EventHandler lvUFSMSDoubleClick;
        public ucGroupUserFriendlySMS()
        {
            InitializeComponent();
            // Ajay: 23/11/2018
            //if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
            //{
            //    if (ProtocolGateway.OppMQTTSlaveGroup_ReadOnly)
            //    {
            //        btnAdd.Enabled = false;
            //        btnDelete.Enabled = false;
                   
            //        return;
            //    }
            //    else { }
            //}
        }
        private void lvUFSMS_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (lvUFSMSItemCheck != null)
                lvUFSMSItemCheck(sender, e);
        }
        private void lvUFSMS_DoubleClick(object sender, EventArgs e)
        {
            if (lvUFSMSDoubleClick != null)
                lvUFSMSDoubleClick(sender, e);
        }
        private void lblHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }
        private void lblHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpUFSMS, sender, e);
        }
        private void pbHdr_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }
        private void pbHdr_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpUFSMS, sender, e);
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

        private void lvUFSMS_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
    }
}
