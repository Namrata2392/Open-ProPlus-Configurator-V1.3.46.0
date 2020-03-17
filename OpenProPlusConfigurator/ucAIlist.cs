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
using System.Reflection;

namespace OpenProPlusConfigurator
{
    public partial class ucAIlist : UserControl
    {
        public event EventHandler btnAddClick;
        public event EventHandler btnDeleteClick;
        public event EventHandler btnDoneClick;
        public event EventHandler btnVerifyClick; //Ajay: 31/07/2018
        public event EventHandler btnCancelClick;
        public event EventHandler btnFirstClick;
        public event EventHandler btnPrevClick;
        public event EventHandler btnNextClick;
        public event EventHandler btnLastClick;
        public event EventHandler lvAIlistDoubleClick;
        public event ListViewItemSelectionChangedEventHandler lvAIlistItemSelectionChanged;
        public event ListViewItemSelectionChangedEventHandler lvAIMapItemSelectionChanged;
        public event EventHandler cmbIEDnoSelectedIndexChanged;
        public event EventHandler btnAIMDeleteClick;
        public event EventHandler btnAIMDoneClick;
        public event EventHandler btnAIMCancelClick;
        public event EventHandler lvAIMapDoubleClick;
        public event EventHandler button1Click; 
        public event EventHandler button3Click; 
        public event EventHandler lnkbtnDeleteAllClick;
        public event EventHandler LinkDeleteConfigueClick;
        public event EventHandler cmb61850IndexSelectedIndexChanged;
        public event EventHandler cmb61850ResponseTypeSelectedIndexChanged;
        public event EventHandler cmbIEDNameSelectedIndexChanged;
        public event EventHandler ucAIlistLoad;
        public event EventHandler cmbDataTypeSelectedIndexChanged;
        public event EventHandler cmbAIMDataTypeSelectedIndexChanged;
        public event DrawListViewItemEventHandler lvAIlistDrawItem;
        public event EventHandler lvAIMapSelectedIndexChanged;
        public event EventHandler lvAIlistSelectedIndexChanged;
        public event EventHandler cmbFCSelectedIndexChanged; //Ajay: 17/01/2019
        public event EventHandler ChkIEC61850IndexSelectedIndexChanged;//Namrata:22/03/2019
        public event EventHandler ChkIEC61850MapIndexSelectedIndexChanged;//Namrata:12/04/2019
        public event EventHandler CmbCellNoSelectedIndexChanged;//Namrata:12/04/2019
        public event TableLayoutCellPaintEventHandler TblLayoutCSLDCellPaint;
        public event MouseEventHandler TblLayoutCSLDMouseMove;
        public event EventHandler txtBxSearchTextChanged; //Ajay: 17/01/2019
        public event EventHandler linkLabel1Click;
        public ucAIlist()
        {
            InitializeComponent();
          
            //Utils.createPBTitleBar(pbHdr, lblHdrText, this.PointToScreen(lblHdrText.Location));
            //Utils.createPBTitleBar(pbAIMHdr, lblAIMHdrText, this.PointToScreen(lblAIMHdrText.Location));
            txtAINo.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtAIMNo.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            cmbAIMCommandType.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtDescription.MaxLength = Globals.MAX_DESCRIPTION_LEN;

          
            
        }
        #region .. Double Buffered function ..
        //public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        //{
        //    if (System.Windows.Forms.SystemInformation.TerminalServerSession)
        //        return;
        //    System.Reflection.PropertyInfo aProp = typeof(System.Windows.Forms.Control).GetProperty("DoubleBuffered",
        //    System.Reflection.BindingFlags.NonPublic |
        //    System.Reflection.BindingFlags.Instance);
        //    aProp.SetValue(c, true, null);
        //}

        #endregion


        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }


        //public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        //{
        //    if (System.Windows.Forms.SystemInformation.TerminalServerSession)
        //        return;
        //    System.Reflection.PropertyInfo aProp = typeof(System.Windows.Forms.Control).GetProperty("DoubleBuffered",
        //    System.Reflection.BindingFlags.NonPublic |
        //    System.Reflection.BindingFlags.Instance);
        //    aProp.SetValue(c, true, null);
        //}
        private void txtBxSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtBxSearchTextChanged != null)
                txtBxSearchTextChanged(sender, e);
        }
        private void cmbDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDataTypeSelectedIndexChanged != null)
                cmbDataTypeSelectedIndexChanged(sender, e);
        }
        public void btnAdd_Click(object sender, EventArgs e)
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
        private void lvAIlist_DoubleClick(object sender, EventArgs e)
        {

            if (lvAIlistDoubleClick != null)
                lvAIlistDoubleClick(sender, e);
        }
        private void lvAIlist_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (lvAIlistItemSelectionChanged != null)
                lvAIlistItemSelectionChanged(sender, e);
            Console.WriteLine("***** lvAIlist ItemSelectionChanged!!! index: {0}", e.ItemIndex);
        }
        private void lvAIMap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvAIMapSelectedIndexChanged != null)
                lvAIMapSelectedIndexChanged(sender, e);
        }
        private void lvAIlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvAIlistSelectedIndexChanged != null)
                lvAIlistSelectedIndexChanged(sender, e);
            //Console.WriteLine("***** lvAIMap ItemSelectionChanged!!! index: {0}", e.ItemIndex);
        }
        //Namrata:10/6/2017
        private void lvAIMap_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (lvAIMapItemSelectionChanged != null)
                lvAIMapItemSelectionChanged(sender, e);
            Console.WriteLine("***** lvAIMap ItemSelectionChanged!!! index: {0}", e.ItemIndex);

        }
        private void cmbAIMDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAIMDataTypeSelectedIndexChanged != null)
                cmbAIMDataTypeSelectedIndexChanged(sender, e);
        }
        private void cmb61850Index_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb61850IndexSelectedIndexChanged != null)
                cmb61850IndexSelectedIndexChanged(sender, e);
        }
        private void cmbIEDName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIEDNameSelectedIndexChanged != null)
                cmbIEDNameSelectedIndexChanged(sender, e);
        }
        private void cmb61850ResponseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb61850ResponseTypeSelectedIndexChanged != null)
                cmb61850ResponseTypeSelectedIndexChanged(sender, e);

        }
        private void linkLabel1_Click(object sender, EventArgs e)
        {
            if (linkLabel1Click != null)
                linkLabel1Click(sender, e);
        }
        private void lnkbtnDeleteAll_Click(object sender, EventArgs e)
        {
            if (lnkbtnDeleteAllClick != null)
                lnkbtnDeleteAllClick(sender, e);
        }
        private void LinkDeleteConfigue_Click(object sender, EventArgs e)
        {

        }
        private void lvAIMap_DoubleClick(object sender, EventArgs e)
        {
            if (lvAIMapDoubleClick != null)
                lvAIMapDoubleClick(sender, e);
        }
        private void btnAIMDone_Click(object sender, EventArgs e)
        {
            if (btnAIMDoneClick != null)
                btnAIMDoneClick(sender, e);
        }
        private void btnAIMCancel_Click(object sender, EventArgs e)
        {
            if (btnAIMCancelClick != null)
                btnAIMCancelClick(sender, e);
        }
        private void btnAIMDelete_Click(object sender, EventArgs e)
        {
            if (btnAIMDeleteClick != null)
                btnAIMDeleteClick(sender, e);
        }
        private void pbHdr_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }
        private void pbHdr_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpAI, sender, e);
        }
        private void lblHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }
        private void lblHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpAI, sender, e);
        }
        private void txtIndex_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }
        private void txtSubIndex_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }
        private void txtMultiplier_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, true);
        }
        private void txtConstant_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, true);
        }
        private void txtAIMReportingIndex_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }
        private void txtAIMMultiplier_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, true);
        }
        private void txtAIMConstant_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, true);
        }
        private void txtAIMDeadBand_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, true);
        }
        private void pbAIMHdr_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }
        private void pbAIMHdr_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpAIMap, sender, e);
        }
        private void lblAIMHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }
        private void lblAIMHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpAIMap, sender, e);
        }
        public void ucAIlist_Load(object sender, EventArgs e)
        {
            //SetDoubleBuffered(TblLayoutCSLD);
            //SetDoubleBufferinga(lvAIlist, true);
            if (ucAIlistLoad != null)
                ucAIlistLoad(sender, e);
        }
        public static void SetDoubleBufferinga(System.Windows.Forms.Control control, bool value)
        {
            System.Reflection.PropertyInfo controlProperty = typeof(System.Windows.Forms.Control)
                .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            controlProperty.SetValue(control, value, null);
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (btnPrevClick != null)
                btnPrevClick(sender, e);
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (btnFirstClick != null)
                btnFirstClick(sender, e);
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            if (btnFirstClick != null)
                btnFirstClick(sender, e);
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (btnNextClick != null)
                btnNextClick(sender, e);

        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
        private void LinkDeleteConfigue_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
        private void txtAIAutoMapRange_TextChanged(object sender, EventArgs e)
        {

        }
        private void lblHdrText_Click(object sender, EventArgs e)
        {

        }
        private void txtAIAutoMapRange_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }
        public event ItemCheckEventHandler lvAIlistItemCheck;
        private void lvAIlist_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (lvAIlistItemCheck != null)
                lvAIlistItemCheck(sender, e);
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void lvAIlist_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
           
            if (lvAIlistDrawItem != null)
                lvAIlistDrawItem(sender, e);
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //Ajay: 31/07/2018
        private void btnVerify_Click(object sender, EventArgs e)
        {
            if (btnVerifyClick != null)
                btnVerifyClick(sender, e);
        }
        //Ajay: 10/09/2018
        private void cmbAI1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnlyForComboBox(sender, e, false, false);
        }
        //Ajay: 10/09/2018
        private void cmbAI2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnlyForComboBox(sender, e, false, false);
        }
        //Ajay: 10/09/2018
        private void cmbAI3_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnlyForComboBox(sender, e, false, false);
        }
        //Ajay: 17/01/2019
        private void cmbFC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFCSelectedIndexChanged != null)
                cmbFCSelectedIndexChanged(sender, e);
        }

        private void txtDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SpecialCharacter_Validation(e);
        }

        private void txtMapDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SpecialCharacter_Validation(e);
        }
        //Namrata:22/03/2019
        private void ChkIEC61850Index_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ChkIEC61850IndexSelectedIndexChanged != null)
                ChkIEC61850IndexSelectedIndexChanged(sender, e);
        }
        private void ChkIEC61850Index_CheckBoxCheckedChanged(object sender, EventArgs e)
        {

        }
        //Namrata:12/04/2019
        private void ChkIEC61850MapIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ChkIEC61850MapIndexSelectedIndexChanged != null)
                ChkIEC61850MapIndexSelectedIndexChanged(sender, e);
        }

        private void CmbCellNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbCellNoSelectedIndexChanged != null)
                CmbCellNoSelectedIndexChanged(sender, e);
        }

        private void TblLayoutCSLD_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (TblLayoutCSLDCellPaint != null)
                TblLayoutCSLDCellPaint(sender, e);
            
        }
        private void TblLayoutCSLD_MouseMove(object sender, MouseEventArgs e)
        {
            if (TblLayoutCSLDMouseMove != null)
                TblLayoutCSLDMouseMove(sender, e);

            //MessageBox.Show("X:" + e.X.ToString() + " Y:" + e.Y.ToString());
            //ToolTip tooltip = new ToolTip();
            //Point pt = TblLayoutCSLD.PointToClient(Control.MousePosition);
            //TableLayoutPanelCellPosition pos = GetCellPosition(TblLayoutCSLD, pt);
            //Control c = TblLayoutCSLD.GetControlFromPosition(pos.Column, pos.Row);
            //if (c != null)
            //{
            //    tooltip.Show(c.Text, TblLayoutCSLD, pt, 500);
            //}
        }
        private TableLayoutPanelCellPosition GetCellPosition(TableLayoutPanel panel, Point p)
        {
            //Cell position
            TableLayoutPanelCellPosition pos = new TableLayoutPanelCellPosition(0, 0);
            //Panel size.
            Size size = panel.Size;
            //average cell size.
            SizeF cellAutoSize = new SizeF(size.Width / panel.ColumnCount, size.Height / panel.RowCount);

            //Get the cell row.
            //y coordinate
            float y = 0;
            for (int i = 0; i < panel.RowCount; i++)
            {
                //Calculate the summary of the row heights.
                SizeType type = panel.RowStyles[i].SizeType;
                float height = panel.RowStyles[i].Height;
                switch (type)
                {
                    case SizeType.Absolute:
                        y += height;
                        break;
                    case SizeType.Percent:
                        y += height / 100 * size.Height;
                        break;
                    case SizeType.AutoSize:
                        y += cellAutoSize.Height;
                        break;
                }
                //Check the mouse position to decide if the cell is in current row.
                if ((int)y > p.Y)
                {
                    pos.Row = i;
                    break;
                }
            }

            //Get the cell column.
            //x coordinate
            float x = 0;
            for (int i = 0; i < panel.ColumnCount; i++)
            {
                //Calculate the summary of the row widths.
                SizeType type = panel.ColumnStyles[i].SizeType;
                float width = panel.ColumnStyles[i].Width;
                switch (type)
                {
                    case SizeType.Absolute:
                        x += width;
                        break;
                    case SizeType.Percent:
                        x += width / 100 * size.Width;
                        break;
                    case SizeType.AutoSize:
                        x += cellAutoSize.Width;
                        break;
                }
                //Check the mouse position to decide if the cell is in current column.
                if ((int)x > p.X)
                {
                    pos.Column = i;
                    break;
                }
            }

            //return the mouse position.
            return pos;
        }
        private void TblLayoutCSLD_MouseHover(object sender, EventArgs e)
        {
        }

        private void TblLayoutCSLD_MouseClick(object sender, MouseEventArgs e)
        {
          
        }

        private void TblLayoutCSLD_Click(object sender, EventArgs e)
        {
            
        }

       

        private void TblLayoutCSLD_MouseUp(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("X:" + e.X.ToString()+ " Y:"+e.Y.ToString());
            
        }

        private void TblLayoutCSLD_MouseCaptureChanged(object sender, EventArgs e)
        {
            
        }

        private void TblLayoutCSLD_ChangeUICues(object sender, UICuesEventArgs e)
        {
            
        }

        private void TblLayoutCSLD_Move(object sender, EventArgs e)
        {
           
        }

        private void TblLayoutCSLD_RegionChanged(object sender, EventArgs e)
        {
            
        }
      
        Graphics graphics;
        private void TblLayoutCSLD_MouseEnter(object sender, EventArgs e)
        {
        }

        private void txtBxSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar);
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            if (button1.Text == ">>")
            {
                button1.Text = "<<";
                splitContainer2.SplitterDistance = splitContainer2.Width - 490 + 35; // 35 - Tolerance
                //splitContainer2.SplitterDistance = 650;
                splitContainer2.Panel2Collapsed = false;
                TblLayoutCSLD.Size = new Size(430, 700);
                //lvAIlist.Borde
                
            }
            else
            {
                button1.Text = ">>";
                splitContainer2.SplitterDistance = 1050;
                splitContainer2.Panel2Collapsed = true;
                
            }
        }
     
        private void TblLayoutCSLD_Click_1(object sender, EventArgs e)
        {

        }

        private void TblLayoutCSLD_Leave(object sender, EventArgs e)
        {

        }
    }
}
