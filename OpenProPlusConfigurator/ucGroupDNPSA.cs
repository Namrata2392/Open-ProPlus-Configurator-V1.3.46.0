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
    public partial class ucGroupDNPSA : UserControl
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
        public event EventHandler lvDNPSAListDoubleClick;
        public event ItemCheckEventHandler lvDNPSAListItemCheck;
        public ucGroupDNPSA()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void pbHdr_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void pbHdr_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpDNPSA, sender, e);
        }

        private void lblHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void lblHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpDNPSA, sender, e);
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

        private void lvDNPSAList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (lvDNPSAListItemCheck != null)
                lvDNPSAListItemCheck(sender, e);
        }

        private void lvDNPSAList_DoubleClick(object sender, EventArgs e)
        {
            if (lvDNPSAListDoubleClick != null)
                lvDNPSAListDoubleClick(sender, e);
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void lvDNPSAList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
