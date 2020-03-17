using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace OpenProPlusConfigurator
{
    public partial class ucSPORTGroup : UserControl
    {
        public event EventHandler btnAddClick;
        public event EventHandler btnDeleteClick;
        public event EventHandler btnDoneClick;
        public event EventHandler btnCancelClick;
        public event EventHandler btnFirstClick;
        public event EventHandler btnPrevClick;
        public event EventHandler btnNextClick;
        public event EventHandler btnLastClick;
        public event EventHandler lvSPORTMasterDoubleClick;
        public ucSPORTGroup()
        {
            InitializeComponent();
            //Ajay: 23/11/2018
            if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
            {
                if (ProtocolGateway.OppSPORTGroup_ReadOnly)
                {
                    btnAdd.Enabled = false;
                    btnDelete.Enabled = false;
                    return;
                }
                else { }
            }
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
        private void lblHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpSPORT, sender, e);
        }

        private void lblHdrText_MouseHover(object sender, EventArgs e)
        {

        }

        private void lvSPORTMaster_DoubleClick(object sender, EventArgs e)
        {
            if (lvSPORTMasterDoubleClick != null)
                lvSPORTMasterDoubleClick(sender, e);
        }

        private void lblHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void pbHdr_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpSPORT, sender, e);
        }

        private void pbHdr_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }
        public event ItemCheckEventHandler lvSPORTMasterItemCheck;
        private void lvSPORTMaster_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (lvSPORTMasterItemCheck != null)
                lvSPORTMasterItemCheck(sender, e);
        }

        private void txtDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SpecialCharacter_Validation(e);
        }
    }
}
