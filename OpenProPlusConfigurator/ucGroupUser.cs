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
    public partial class ucGroupUser : UserControl
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
        public event EventHandler lvUserListDoubleClick;
        public event ItemCheckEventHandler lvUserListItemCheck;
        public ucGroupUser()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void lvUserList_DoubleClick(object sender, EventArgs e)
        {
            if (lvUserListDoubleClick != null)
                lvUserListDoubleClick(sender, e);
        }
        private void lvUserList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (lvUserListItemCheck != null)
                lvUserListItemCheck(sender, e);
        }
        private void lvUserList_Click(object sender, EventArgs e)
        {

        }
        private void lvUFSMSList_DoubleClick(object sender, EventArgs e)
        {

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




        private void pbHdr_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void pbHdr_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpUser, sender, e);
        }

        private void lblHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void lblHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpUser, sender, e);
        }

        private void lvUserList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtKey_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode!=Keys.Back)
            {
                string text = this.txtKey.Text;
                if(text.Replace(" ","").Length % 2 ==0 && text.Length!=0 && text.Substring(text.Length-1)!=" ")
                {
                    txtKey.Text = txtKey.Text + " ";
                    txtKey.Select(txtKey.Text.Length, 1);
                }
            }
        }
    }
}
