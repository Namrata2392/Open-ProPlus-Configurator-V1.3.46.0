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
    public partial class ucUFSMS : UserControl
    {
        public event EventHandler btnAddClick;
        public event EventHandler btnDeleteClick;
        public event EventHandler btnDoneClick;
        public event EventHandler btnCancelClick;
        public event EventHandler btnFirstClick;
        public event EventHandler btnPrevClick;
        public event EventHandler btnNextClick;
        public event EventHandler btnLastClick;
        public event EventHandler lvUFSMSListDoubleClick;
        public event ItemCheckEventHandler lvUFSMSListItemCheck; 
        public ucUFSMS()
        {
            InitializeComponent();
        }
        private void chkbxRun_CheckedChanged(object sender, EventArgs e)
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
        private void btnDone_Click(object sender, EventArgs e)
        {
            if (btnDoneClick != null)
                btnDoneClick(sender, e);
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
        private void lvUFSMSList_DoubleClick(object sender, EventArgs e)
        {
            if (lvUFSMSListDoubleClick != null)
                lvUFSMSListDoubleClick(sender, e);
        }
        private void lvUFSMSList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (lvUFSMSListItemCheck != null)
                lvUFSMSListItemCheck(sender, e);
        }
       

        private void pbHdr_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }
        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void pbHdr_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpUFSMS, sender, e);
        }
        private void lblHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }
        private void lblHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpUFSMS, sender, e);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
                 if (btnCancelClick != null)
                btnCancelClick(sender, e);
        }
    }
}
