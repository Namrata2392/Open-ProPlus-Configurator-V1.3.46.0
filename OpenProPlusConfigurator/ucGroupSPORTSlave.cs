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
    public partial class ucGroupSPORTSlave : UserControl
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
        public event EventHandler btnexportSPORTINIClick;
        public event EventHandler lvSPORTSlaveDoubleClick;
        public event ItemCheckEventHandler lvSPORTSlaveItemCheck;
        public ucGroupSPORTSlave()
        {
            InitializeComponent();

            //Ajay: 23/11/2018
            if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
            {
                if (ProtocolGateway.OppSPORTSlaveGroup_ReadOnly)
                {
                    btnAdd.Enabled = false;
                    btnDelete.Enabled = false;
                    btnexportSPORTINI.Enabled = false;
                    return;
                }
                else { }
            }
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

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (btnDoneClick != null)
                btnDoneClick(sender, e);
        }

        private void grpIEC104_Enter(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (btnCancelClick != null)
                btnCancelClick(sender, e);
        }
        private void lvSPORTSlave_DoubleClick(object sender, EventArgs e)
        {
            if (lvSPORTSlaveDoubleClick != null)
                lvSPORTSlaveDoubleClick(sender, e);
        }
        private void btnexportSPORTINI_Click(object sender, EventArgs e)
        {
            if (btnexportSPORTINIClick != null)
                btnexportSPORTINIClick(sender, e);
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

        private void lvIEC101Slave_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (lvSPORTSlaveItemCheck != null)
                lvSPORTSlaveItemCheck(sender, e);
        }

        private void lvIEC101Slave_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
