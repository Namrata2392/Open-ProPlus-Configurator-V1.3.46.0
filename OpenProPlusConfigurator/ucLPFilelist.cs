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
    /**
    * \brief     <b>ucDIlist</b> is a user interface to display all DI's and there corresponding mapping infos.
    * \details   This is a user interface to display all DI's and there corresponding mapping's for various slaves. It provides interface
    * to add multiple DI's. The user can map this DI's to various slaves. The user can also modify mapping parameters.
    * 
    * 
    */
    public partial class ucLPFilelist : UserControl
    {
        public event EventHandler btnAddClick;
        public event EventHandler btnDeleteClick;
        public event EventHandler btnVerifyClick; 
        public event EventHandler btnDoneClick;
        public event EventHandler btnCancelClick;
        public event EventHandler btnFirstClick;
        public event EventHandler btnPrevClick;
        public event EventHandler btnNextClick;
        public event EventHandler btnLastClick;
        public event EventHandler lvLPFilelistDoubleClick;
        public event ListViewItemSelectionChangedEventHandler lvLPFilelistItemSelectionChanged;
        public event EventHandler button1Click; 
        public event EventHandler button2Click;
        public event EventHandler linkLPFileClick; 
        public event EventHandler linkLabel1Click;
        public event EventHandler ucLPFilelistLoad;
        public event EventHandler lvLPFilelistSelectedIndexChanged;
        //Ajay: 10/10/2018
        public event EventHandler cmbAINoTextChanged;
        public event EventHandler cmbENNoTextChanged;
        public ucLPFilelist()
        {
            InitializeComponent();
            //Ajay: 10/10/2018 Commented
            //txtLPFileNo.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
        }

        private void lvLPFilelist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvLPFilelistSelectedIndexChanged != null)
                lvLPFilelistSelectedIndexChanged(sender, e);
        }
    
        public void ucLPFilelist_Load(object sender, EventArgs e)
        {
            if (ucLPFilelistLoad != null)
                ucLPFilelistLoad(sender, e);
        }
        private void linkLPFile_Click(object sender, EventArgs e)
        {
            if (linkLPFileClick != null)
                linkLPFileClick(sender, e);
        }
        private void linkLabel1_Click(object sender, EventArgs e)
        {
            if (linkLabel1Click != null)
                linkLabel1Click(sender, e);
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (btnCancelClick != null)
                btnCancelClick(sender, e);
        }
        
        private void lvLPFilelist_DoubleClick(object sender, EventArgs e)
        {
            if (lvLPFilelistDoubleClick != null)
                lvLPFilelistDoubleClick(sender, e);
        }

        private void lvDIlist_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (lvLPFilelistItemSelectionChanged != null)
                lvLPFilelistItemSelectionChanged(sender, e);
            Console.WriteLine("***** lvDIlist ItemSelectionChanged!!! index: {0}", e.ItemIndex);
        }

        private void pbHdr_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void pbHdr_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpLPFile, sender, e);
        }

        private void lblHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void lblHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpLPFile, sender, e);
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
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1Click != null)
                button1Click(sender, e);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (button2Click != null)
                button2Click(sender, e);
        }
        private void txtIndex_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtSubIndex_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtDIMReportingIndex_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtDIMBitPos_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            if (btnVerifyClick != null)
                btnVerifyClick(sender, e);
        }
        //Ajay: 10/09/2018
        private void cmbAINo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnlyForComboBox(sender, e, false, false);
        }
        //Ajay: 10/09/2018
        private void cmbENNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnlyForComboBox(sender, e, false, false);
        }

        private void cmbAINo_TextChanged(object sender, EventArgs e)
        {
            if (cmbAINoTextChanged != null)
                cmbAINoTextChanged(sender, e);
        }

        private void cmbENNo_TextChanged(object sender, EventArgs e)
        {
            if (cmbENNoTextChanged != null)
                cmbENNoTextChanged(sender, e);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
