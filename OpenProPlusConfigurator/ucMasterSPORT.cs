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
    public partial class ucMasterSPORT : UserControl
    {
        public event EventHandler btnAddClick;
        public event EventHandler btnDeleteClick;
        public event EventHandler btnExportIEDClick;
        public event EventHandler btnImportIEDClick;
        public event EventHandler btnDoneClick;
        public event EventHandler btnCancelClick;
        public event EventHandler btnFirstClick;
        public event EventHandler btnPrevClick;
        public event EventHandler btnNextClick;
        public event EventHandler btnLastClick;
        public event EventHandler btnExportXlsClick;
        public event EventHandler lvIEDListDoubleClick;
        public event EventHandler BtnDeleteAllClick;
        public event EventHandler txtLastAITextChanged;
        public event EventHandler txtLastAIValidated;
        public event EventHandler txtLastAIKeyPress;
        public event EventHandler txtLastDITextChanged;
        public event EventHandler txtLastDOTextChanged;
        public ucMasterSPORT()
        {
            InitializeComponent();
            //Ajay: 23/11/2018
            if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
            {
                if (ProtocolGateway.OppSPORTGroup_ReadOnly)
                {
                    btnAdd.Enabled = false;
                    btnDelete.Enabled = false;
                    btnExportIED.Enabled = false;
                    btnImportIED.Enabled = false;
                    return;
                }
                else { }
            }
        }
        private void txtLastAI_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }
        private void txtLastAI_Validated(object sender, EventArgs e)
        {
            if (txtLastAIValidated != null)
                txtLastAIValidated(sender, e);
        }
        private void txtLastAI_TextChanged(object sender, EventArgs e)
        {
            if (txtLastAITextChanged != null)
                txtLastAITextChanged(sender, e);
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

        private void btnExportIED_Click(object sender, EventArgs e)
        {
            if (btnExportIEDClick != null)
                btnExportIEDClick(sender, e);
        }

        private void btnImportIED_Click(object sender, EventArgs e)
        {
            if (btnImportIEDClick != null)
                btnImportIEDClick(sender, e);
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

        private void lvIEDList_DoubleClick(object sender, EventArgs e)
        {
            if (lvIEDListDoubleClick != null)
                lvIEDListDoubleClick(sender, e);
        }

        private void chkDR_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void lblIOASize_Click(object sender, EventArgs e)
        {

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (btnNextClick != null)
                btnNextClick(sender, e);
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

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (btnLastClick != null)
                btnLastClick(sender, e);
        }

        private void pbHdr_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void pbHdr_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpIED, sender, e);
        }

        private void lblHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpIED, sender, e);
        }

        private void lblHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }
        public event ItemCheckEventHandler lvIEDListItemCheck;
        private void lvIEDList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (lvIEDListItemCheck != null)
                lvIEDListItemCheck(sender, e);
        }

        private void BtnDeleteAll_Click(object sender, EventArgs e)
        {
            if (BtnDeleteAllClick != null)
                BtnDeleteAllClick(sender, e);
        }

        private void txtDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SpecialCharacter_Validation(e);
        }
        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void txtLastDI_TextChanged(object sender, EventArgs e)
        {
            if (txtLastDITextChanged != null)
                txtLastDITextChanged(sender, e);
        }
        private void txtLastDO_TextChanged(object sender, EventArgs e)
        {
            if (txtLastDOTextChanged != null)
                txtLastDOTextChanged(sender, e);
        }
        private void txtLastDI_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }
        private void txtLastDO_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtDescription_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            Utils.SpecialCharacter_Validation(e);
        }

        private void txtTimestampType_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtTimestampAccuracy_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTimestampAccuracy_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtWindowTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtDebounceTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtPulsewidthTimeout_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtTimestampType_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }
    }
}
