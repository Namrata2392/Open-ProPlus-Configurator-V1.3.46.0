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
    public partial class ucGroupDNPSlave : UserControl
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
        public event EventHandler lvDNPSlaveDoubleClick;
        public event ItemCheckEventHandler lvDNPSlaveItemCheck;
        public ucGroupDNPSlave()
        {
            InitializeComponent();
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

        private void lvGraphicalDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lvDNPSlave_DoubleClick(object sender, EventArgs e)
        {
            if (lvDNPSlaveDoubleClick != null)
                lvDNPSlaveDoubleClick(sender, e);

        }

        private void lvDNPSlave_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (lvDNPSlaveItemCheck != null)
                lvDNPSlaveItemCheck(sender, e);
        }
        private void pbHdr_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void pbHdr_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpDNPSlave, sender, e);
        }

        private void lblHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void lblHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpDNPSlave, sender, e);
        }

        private void txtUnitID_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtRemoteIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowIP(sender, e);
        }

        private void txtSecRemoteIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowIP(sender, e);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowIP(sender, e);
        }

        private void txtT0_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtTCPPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtUnitID_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtACTout_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtCVP_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtSelectTout_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtDestAdd_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }
        public event EventHandler cmbProtocolTypeSelectedIndexChanged;
        public event EventHandler CmbPortNameSelectedIndexChanged;
        public event EventHandler cmbPortNoSelectedIndexChanged; public event EventHandler cmbLocalIPSelectedIndexChanged;
        private void cmbProtocolType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProtocolTypeSelectedIndexChanged != null)
                cmbProtocolTypeSelectedIndexChanged(sender, e);
        }

        private void cmbPortNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPortNoSelectedIndexChanged != null)
                cmbPortNoSelectedIndexChanged(sender, e);

        }
        private void CmbPortName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbPortNameSelectedIndexChanged != null)
                CmbPortNameSelectedIndexChanged(sender, e);
        }

        private void cmbLocalIP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLocalIPSelectedIndexChanged != null)
                cmbLocalIPSelectedIndexChanged(sender, e);
        }
    }
}
    

