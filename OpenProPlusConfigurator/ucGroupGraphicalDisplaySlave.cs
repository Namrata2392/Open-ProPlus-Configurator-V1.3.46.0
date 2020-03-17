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
    public partial class ucGroupGraphicalDisplaySlave : UserControl
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
        public event EventHandler btnexportIEC101INIClick;
        public event EventHandler lvGraphicalDisplayDoubleClick;
        public event ItemCheckEventHandler lvGraphicalDisplayItemCheck;
        public ucGroupGraphicalDisplaySlave()
        {
            InitializeComponent();
            //Namrata: 09/08/2019
            if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
            {
                if (ProtocolGateway.OppGraphicalDisplaySlaveGroup_ReadOnly)
                {
                    btnAdd.Enabled = false;
                    btnDelete.Enabled = false;
                    return;
                }
                else { }
            }
        }
        private void lblHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpGraphicalDisplay, sender, e);
        }
        private void lblHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupGraphicalDisplaySlave: btnAdd_Click";
            try
            {
                if (btnAddClick != null)
                    btnAddClick(sender, e);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupGraphicalDisplaySlave: btnDelete_Click";
            try
            {
                if (btnDeleteClick != null)
                    btnDeleteClick(sender, e);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupGraphicalDisplaySlave: btnDone_Click";
            try
            {
                if (btnDoneClick != null)
                    btnDoneClick(sender, e);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupGraphicalDisplaySlave: btnCancel_Click";
            try
            {
                if (btnCancelClick != null)
                    btnCancelClick(sender, e);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupGraphicalDisplaySlave: btnFirst_Click";
            try
            {
                if (btnFirstClick != null)
                    btnFirstClick(sender, e);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupGraphicalDisplaySlave: btnPrev_Click";
            try
            {
                if (btnPrevClick != null)
                    btnPrevClick(sender, e);

            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupGraphicalDisplaySlave: btnNext_Click";
            try
            {
                if (btnNextClick != null)
                    btnNextClick(sender, e);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupGraphicalDisplaySlave: btnLast_Click";
            try
            {
                if (btnLastClick != null)
                    btnLastClick(sender, e);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvGraphicalDisplay_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (lvGraphicalDisplayItemCheck != null)
                lvGraphicalDisplayItemCheck(sender, e);
        }
        private void lvGraphicalDisplay_DoubleClick(object sender, EventArgs e)
        {
            if (lvGraphicalDisplayDoubleClick != null)
                lvGraphicalDisplayDoubleClick(sender, e);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
