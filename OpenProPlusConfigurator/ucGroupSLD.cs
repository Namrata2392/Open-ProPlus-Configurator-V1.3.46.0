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
    public partial class ucGroupSLD : UserControl
    {
        public event EventHandler btnAddClick;
        public event EventHandler btnDeleteClick;
        public event EventHandler btnDoneClick;
        public event EventHandler btnCancelClick;
        public event EventHandler lvSLDListDoubleClick;
        public event EventHandler PCDFileClick;
        public event ItemCheckEventHandler lvSLDListItemCheck;
        public event EventHandler ChkGridLinesCheckedChanged;
        public event EventHandler ChkSHGridLinesCheckedChanged;
        public event EventHandler ucGroupSLDLoad;
        public event MouseEventHandler PbSourceMouseDown;
        public event DragEventHandler PbTargetDragEnter;
        public event DragEventHandler PbTargetDragDrop;
        public event EventHandler btnImportSLDClick;
        public event EventHandler BtnCreateSLDClick;
        public event DragEventHandler Pb1DragEnter;
        public event DragEventHandler Pb1DragDrop;
        public event DragEventHandler Pb2DragEnter;
        public event DragEventHandler Pb2DragDrop;
        public event DragEventHandler Pb3DragEnter;
        public event DragEventHandler Pb3DragDrop;
        public event DragEventHandler Pb4DragEnter;
        public event DragEventHandler Pb4DragDrop;
        public event DragEventHandler Pb5DragEnter;
        public event DragEventHandler Pb5DragDrop;
        public event DragEventHandler Pb6DragEnter;
        public event DragEventHandler Pb6DragDrop;
        public event DragEventHandler Pb7DragEnter;
        public event DragEventHandler Pb7DragDrop;
        public event DragEventHandler Pb8DragEnter;
        public event DragEventHandler Pb8DragDrop;
        public event DragEventHandler Pb9DragEnter;
        public event DragEventHandler Pb9DragDrop;
        public event DragEventHandler Pb10DragEnter;
        public event DragEventHandler Pb10DragDrop;
        public event DragEventHandler Pb11DragEnter;
        public event DragEventHandler Pb11DragDrop;
        public event DragEventHandler Pb12DragEnter;
        public event DragEventHandler Pb12DragDrop;
        public event DragEventHandler Pb13DragEnter;
        public event DragEventHandler Pb13DragDrop;
        public event DragEventHandler Pb14DragEnter;
        public event DragEventHandler Pb14DragDrop;
        public event DragEventHandler Pb15DragEnter;
        public event DragEventHandler Pb15DragDrop;
        public event DragEventHandler Pb16DragEnter;
        public event DragEventHandler Pb16DragDrop;
        public event DragEventHandler Pb17DragEnter;
        public event DragEventHandler Pb17DragDrop;
        public event DragEventHandler Pb18DragEnter;
        public event DragEventHandler Pb18DragDrop;
        public event DragEventHandler Pb19DragDrop;
        public event DragEventHandler Pb19DragEnter;
        public event DragEventHandler Pb20DragDrop;
        public event DragEventHandler Pb20DragEnter;
        public event DragEventHandler Pb21DragDrop;
        public event DragEventHandler Pb21DragEnter;
        public event DragEventHandler Pb22DragDrop;
        public event DragEventHandler Pb22DragEnter;
        public event DragEventHandler Pb23DragDrop;
        public event DragEventHandler Pb23DragEnter;
        public event DragEventHandler Pb24DragDrop;
        public event DragEventHandler Pb24DragEnter;
        public event DragEventHandler Pb25DragDrop;
        public event DragEventHandler Pb25DragEnter;
        public event DragEventHandler Pb26DragDrop;
        public event DragEventHandler Pb26DragEnter;
        public event DragEventHandler Pb27DragDrop;
        public event DragEventHandler Pb27DragEnter;
        public event DragEventHandler Pb28DragDrop;
        public event DragEventHandler Pb28DragEnter;
        public event DragEventHandler Pb29DragDrop;
        public event DragEventHandler Pb29DragEnter;
        public event DragEventHandler Pb30DragDrop;
        public event DragEventHandler Pb30DragEnter;
        public event DragEventHandler Pb31DragDrop;
        public event DragEventHandler Pb31DragEnter;
        public event DragEventHandler Pb32DragDrop;
        public event DragEventHandler Pb32DragEnter;
        public event DragEventHandler Pb33DragDrop;
        public event DragEventHandler Pb33DragEnter;
        public event DragEventHandler Pb34DragDrop;
        public event DragEventHandler Pb34DragEnter;
        public event DragEventHandler Pb35DragDrop;
        public event DragEventHandler Pb35DragEnter;
        public event DragEventHandler Pb36DragDrop;
        public event DragEventHandler Pb36DragEnter;
        public event DragEventHandler Pb37DragDrop;
        public event DragEventHandler Pb37DragEnter;
        public event DragEventHandler Pb38DragDrop;
        public event DragEventHandler Pb38DragEnter;
        public event DragEventHandler Pb39DragDrop;
        public event DragEventHandler Pb39DragEnter;
        public event DragEventHandler Pb40DragDrop;
        public event DragEventHandler Pb40DragEnter;
        public event DragEventHandler Pb41DragDrop;
        public event DragEventHandler Pb42DragDrop;
        public event DragEventHandler Pb43DragDrop;
        public event DragEventHandler Pb44DragDrop;
        public event DragEventHandler Pb45DragDrop;
        public event DragEventHandler Pb46DragDrop;
        public event DragEventHandler Pb47DragDrop;
        public event DragEventHandler Pb48DragDrop;
        public event DragEventHandler Pb49DragDrop;
        public event DragEventHandler Pb50DragDrop;
        public event DragEventHandler Pb51DragDrop;
        public event DragEventHandler Pb52DragDrop;
        public event DragEventHandler Pb53DragDrop;
        public event DragEventHandler Pb54DragDrop;
        public event DragEventHandler Pb55DragDrop;
        public event DragEventHandler Pb56DragDrop;
        public event DragEventHandler Pb57DragDrop;
        public event DragEventHandler Pb58DragDrop;
        public event DragEventHandler Pb59DragDrop;
        public event DragEventHandler Pb60DragDrop;




        public event DragEventHandler Pb41DragEnter;
        public event DragEventHandler Pb42DragEnter;
        public event DragEventHandler Pb43DragEnter;
        public event DragEventHandler Pb44DragEnter;
        public event DragEventHandler Pb45DragEnter;
        public event DragEventHandler Pb46DragEnter;
        public event DragEventHandler Pb47DragEnter;
        public event DragEventHandler Pb48DragEnter;
        public event DragEventHandler Pb49DragEnter;


        public event DragEventHandler Pb50DragEnter;
        public event DragEventHandler Pb51DragEnter;
        public event DragEventHandler Pb52DragEnter;
        public event DragEventHandler Pb53DragEnter;
        public event DragEventHandler Pb54DragEnter;
        public event DragEventHandler Pb55DragEnter;
        public event DragEventHandler Pb56DragEnter;
        public event DragEventHandler Pb57DragEnter;
        public event DragEventHandler Pb58DragEnter;
        public event DragEventHandler Pb59DragEnter;
        public event DragEventHandler Pb60DragEnter;
        public ucGroupSLD()
        {
            InitializeComponent();
        }
        //private void BtnSaveImage_Click(object sender, EventArgs e)
        //{
        //    if (BtnSaveImageClick != null)
        //        BtnSaveImageClick(sender, e);
        //}
        private void btnImportSLD_Click(object sender, EventArgs e)
        {
            if (btnImportSLDClick != null)
                btnImportSLDClick(sender, e);
        }

        private void BtnCreateSLD_Click(object sender, EventArgs e)
        {
            if (BtnCreateSLDClick != null)
                BtnCreateSLDClick(sender, e);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAddClick != null)
                btnAddClick(sender, e);
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
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (btnDeleteClick != null)
                btnDeleteClick(sender, e);
        }
        private void lvSLDList_DoubleClick(object sender, EventArgs e)
        {
            if (lvSLDListDoubleClick != null)
                lvSLDListDoubleClick(sender, e);
        }
        private void lvSLDList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (lvSLDListItemCheck != null)
                lvSLDListItemCheck(sender, e);
        }
        private void pbHdr_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpGD, sender, e);
        }
        private void lblHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }
        private void lblHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpGD, sender, e);
        }
        private void pbHdr_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }
        private void PCDFile_Click(object sender, EventArgs e)
        {
            if (PCDFileClick != null)
                PCDFileClick(sender, e);
        }
        private void ucGroupSLD_Load(object sender, EventArgs e)
        {
            SetDoubleBuffered(tableLayoutPanel1);
            SetDoubleBuffered(TblLayoutCSLD);
            if (ucGroupSLDLoad != null)
                ucGroupSLDLoad(sender, e);
            //splitContainer2.Panel2.Hide();
        }
        private void ChkGridLines_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkGridLinesCheckedChanged != null)
                ChkGridLinesCheckedChanged(sender, e);
        }
        private void ChkSHGridLines_CheckedChanged(object sender, EventArgs e)
        {
            //if (ChkSHGridLinesCheckedChanged != null)
            //    ChkSHGridLinesCheckedChanged(sender, e);
        }
        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
           
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void lvSLDList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
        private void PbSource_MouseDown(object sender, MouseEventArgs e)
        {
            if (PbSourceMouseDown != null)
                PbSourceMouseDown(sender, e);
        }
        private void PbTarget_DragEnter(object sender, DragEventArgs e)
        {
            if (PbTargetDragEnter != null)
                PbTargetDragEnter(sender, e);
        }
        private void PbTarget_DragDrop(object sender, DragEventArgs e)
        {
            if (PbTargetDragDrop != null)
                PbTargetDragDrop(sender, e);
        }

        private void TvSLD_MouseHover(object sender, EventArgs e)
        {
            //TvSLD.HotTracking = false;
           // TvSLD.SelectedNode.BackColor = Color.White;
            //TvSLD_MouseDown(sender, null);
        }

        private void TvSLD_MouseLeave(object sender, EventArgs e)
        {

        }

        private void TvSLD_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            PictureBox pb = new PictureBox();
            pb.Size = new Size(70, 70);
            //pb.Size = new Size(50, 50);

        }

        private void TvSLD_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if ((e.State & TreeNodeStates.Selected) == TreeNodeStates.Selected)
            {
                // get node font and node fore color
                Font nodeFont = GetTreeNodeFont(e.Node);
                Color nodeForeColor = GetTreeNodeForeColor(e.Node, e.State);

                // fill node background
                using (SolidBrush brush = new SolidBrush(Color.SkyBlue))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }

                // draw node text
                TextRenderer.DrawText(e.Graphics, e.Node.Text, nodeFont, e.Bounds, nodeForeColor, TextFormatFlags.Left | TextFormatFlags.Top);
                
                // draw selected node border
                if ((e.State & TreeNodeStates.Selected) == TreeNodeStates.Selected)
                {
                    using (Pen pen = new Pen(nodeForeColor))
                    {
                        pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                        Rectangle penBounds = e.Bounds;
                        penBounds.Width -= 1;
                        penBounds.Height -= 1;
                        e.Graphics.DrawRectangle(pen, penBounds);
                    }
                }
            }
            else
            {
                e.DrawDefault = true;
            }
        }
        
   
        private Font GetTreeNodeFont(TreeNode node)
        {
            Font nodeFont = node.NodeFont;
            if (nodeFont == null)
            {
                nodeFont = this.Font;
            }
            return nodeFont;
        }
        private Color GetTreeNodeForeColor(TreeNode node, TreeNodeStates nodeState)
        {
            Color nodeForeColor = Color.Empty;

            if ((nodeState & TreeNodeStates.Selected) == TreeNodeStates.Selected)
            {
                nodeForeColor = Color.FromKnownColor(KnownColor.Black);
            }
            else
            {
                nodeForeColor = node.ForeColor;
                if (nodeForeColor == Color.Empty)
                {
                    nodeForeColor = this.ForeColor;
                }
            }
            return nodeForeColor;
        }
        #region PictureBox Drag Drop
        
        private void Pb1_DragEnter(object sender, DragEventArgs e)
        {

            if (Pb1DragEnter != null)
                Pb1DragEnter(sender, e);
        }
        private void Pb1_DragDrop(object sender, DragEventArgs e)
        {

            if (Pb1DragDrop != null)
                Pb1DragDrop(sender, e);
        }
        private void Pb2_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb2_DragEnter";
            try
            {
                if (Pb2DragEnter != null)
                    Pb2DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb2_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb2_DragDrop";
            try
            {
                if (Pb2DragDrop != null)
                    Pb2DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb3_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb3_DragEnter";
            try
            {
                if (Pb3DragEnter != null)
                    Pb3DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb3_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb3_DragDrop";
            try
            {
                if (Pb3DragDrop != null)
                    Pb3DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb4_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb4_DragEnter";
            try
            {
                if (Pb4DragEnter != null)
                    Pb4DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb4_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb4_DragDrop";
            try
            {
                if (Pb4DragDrop != null)
                    Pb4DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb5_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb5_DragEnter";
            try
            {
                if (Pb5DragEnter != null)
                    Pb5DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb5_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb5_DragDrop";
            try
            {
                if (Pb5DragDrop != null)
                    Pb5DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb6_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb6_DragDrop";
            try
            {
                if (Pb6DragDrop != null)
                    Pb6DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb6_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb6_DragEnter";
            try
            {
                if (Pb6DragEnter != null)
                    Pb6DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void Pb7_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb7_DragDrop";
            try
            {
                if (Pb7DragDrop != null)
                    Pb7DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb7_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb7_DragEnter";
            try
            {
                if (Pb7DragEnter != null)
                    Pb7DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb8_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:v";
            try
            {
                if (Pb8DragDrop != null)
                    Pb8DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb8_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb8_DragEnter";
            try
            {
                if (Pb8DragEnter != null)
                    Pb8DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb9_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb9_DragDrop";
            try
            {
                if (Pb9DragDrop != null)
                    Pb9DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void Pb9_DragEnter(object sender, DragEventArgs e)
        {

        }
        private void Pb10_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb10_DragDrop";
            try
            {
                if (Pb10DragDrop != null)
                    Pb10DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void Pb10_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb10_DragEnter";
            try
            {
                if (Pb10DragEnter != null)
                    Pb10DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb11_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb11_DragDrop";
            try
            {
                if (Pb11DragDrop != null)
                    Pb11DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb11_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb11_DragEnter";
            try
            {
                if (Pb11DragEnter != null)
                    Pb11DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb12_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb12_DragDrop";
            try
            {
                if (Pb12DragDrop != null)
                    Pb12DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb12_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb12_DragEnter";
            try
            {
                if (Pb12DragEnter != null)
                    Pb12DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb13_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb13_DragDrop";
            try
            {
                if (Pb13DragDrop != null)
                    Pb13DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb13_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb13_DragEnter";
            try
            {
                if (Pb13DragEnter != null)
                    Pb13DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb14_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb14_DragDrop";
            try
            {
                if (Pb14DragDrop != null)
                    Pb14DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb14_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb14_DragEnter";
            try
            {
                if (Pb14DragEnter != null)
                    Pb14DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb15_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb15_DragDrop";
            try
            {
                if (Pb15DragDrop != null)
                    Pb15DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb15_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb15_DragEnter";
            try
            {
                if (Pb15DragEnter != null)
                    Pb15DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb16_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb16_DragDrop";
            try
            {
                if (Pb16DragDrop != null)
                    Pb16DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Pb16_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb16_DragEnter";
            try
            {
                if (Pb16DragEnter != null)
                    Pb16DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb17_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb17_DragDrop";
            try
            {
                if (Pb17DragDrop != null)
                    Pb17DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Pb17_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb17_DragEnter";
            try
            {
                if (Pb17DragEnter != null)
                    Pb17DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb18_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb18_DragDrop";
            try
            {
                if (Pb18DragDrop != null)
                    Pb18DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb18_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb18_DragEnter";
            try
            {
                if (Pb18DragEnter != null)
                    Pb18DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb19_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb19_DragDrop";
            try
            {
                if (Pb19DragDrop != null)
                    Pb19DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb19_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb19_DragEnter";
            try
            {
                if (Pb19DragEnter != null)
                    Pb19DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb20_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb20_DragDrop";
            try
            {
                if (Pb20DragDrop != null)
                    Pb20DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb20_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb20_DragEnter";
            try
            {
                if (Pb20DragEnter != null)
                    Pb20DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb21_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb21_DragDrop";
            try
            {
                if (Pb21DragDrop != null)
                    Pb21DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb21_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb21_DragEnter";
            try
            {
                if (Pb21DragEnter != null)
                    Pb21DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb22_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb22_DragDrop";
            try
            {
                if (Pb22DragDrop != null)
                    Pb22DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb22_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb22_DragEnter";
            try
            {
                if (Pb22DragEnter != null)
                    Pb22DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb23_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb23_DragDrop";
            try
            {
                if (Pb23DragDrop != null)
                    Pb23DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb23_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb23_DragEnter";
            try
            {
                if (Pb23DragEnter != null)
                    Pb23DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb24_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb24_DragDrop";
            try
            {
                if (Pb24DragDrop != null)
                    Pb24DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb24_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb24_DragEnter";
            try
            {
                if (Pb24DragEnter != null)
                    Pb24DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb25_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb25_DragDrop";
            try
            {
                if (Pb25DragDrop != null)
                    Pb25DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb25_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb25_DragEnter";
            try
            {
                if (Pb25DragEnter != null)
                    Pb25DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb26_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb26_DragDrop";
            try
            {
                if (Pb26DragDrop != null)
                    Pb26DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Pb26_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb26_DragEnter";
            try
            {
                if (Pb26DragEnter != null)
                    Pb26DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb27_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb27_DragDrop";
            try
            {
                if (Pb27DragDrop != null)
                    Pb27DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb27_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb27_DragEnter";
            try
            {
                if (Pb27DragEnter != null)
                    Pb27DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb28_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:v";
            try
            {
                if (Pb28DragDrop != null)
                    Pb28DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb28_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void Pb29_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb29_DragDrop";
            try
            {
                if (Pb29DragDrop != null)
                    Pb29DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Pb29_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb29_DragEnter";
            try
            {
                if (Pb29DragEnter != null)
                    Pb29DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb30_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb30_DragDrop";
            try
            {
                if (Pb30DragDrop != null)
                    Pb30DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb30_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb30_DragEnter";
            try
            {
                if (Pb30DragEnter != null)
                    Pb30DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb31_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb31_DragDrop";
            try
            {
                if (Pb31DragDrop != null)
                    Pb31DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb31_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb31_DragEnter";
            try
            {
                if (Pb31DragEnter != null)
                    Pb31DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb32_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb32_DragDrop";
            try
            {
                if (Pb32DragDrop != null)
                    Pb32DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb32_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb32_DragEnter";
            try
            {
                if (Pb32DragEnter != null)
                    Pb32DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb33_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb33_DragDrop";
            try
            {
                if (Pb33DragDrop != null)
                    Pb33DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb33_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb33_DragEnter";
            try
            {
                if (Pb33DragEnter != null)
                    Pb33DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Pb34_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb34_DragDrop";
            try
            {
                if (Pb34DragDrop != null)
                    Pb34DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb34_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb34_DragEnter";
            try
            {
                if (Pb34DragEnter != null)
                    Pb34DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb35_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void Pb35_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb35_DragDrop";
            try
            {
                if (Pb35DragDrop != null)
                    Pb35DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb36_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb36_DragDrop";
            try
            {
                if (Pb36DragDrop != null)
                    Pb36DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Pb36_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb36_DragEnter";
            try
            {
                if (Pb36DragEnter != null)
                    Pb36DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb37_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb37_DragDrop";
            try
            {
                if (Pb37DragDrop != null)
                    Pb37DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb37_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb37_DragEnter";
            try
            {
                if (Pb37DragEnter != null)
                    Pb37DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb38_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb38_DragDrop";
            try
            {
                if (Pb38DragDrop != null)
                    Pb38DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb38_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb38_DragEnter";
            try
            {
                if (Pb38DragEnter != null)
                    Pb38DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Pb39_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:v";
            try
            {
                if (Pb39DragDrop != null)
                    Pb39DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb39_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb39_DragEnter";
            try
            {
                if (Pb39DragEnter != null)
                    Pb39DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Pb40_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb40_DragDrop";
            try
            {
                if (Pb40DragDrop != null)
                    Pb40DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb41_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb41_DragDrop";
            try
            {
                if (Pb41DragDrop != null)
                    Pb41DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb42_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb42_DragDrop";
            try
            {
                if (Pb42DragDrop != null)
                    Pb42DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Pb43_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb43_DragDrop";
            try
            {
                if (Pb43DragDrop != null)
                    Pb43DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb44_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb44_DragDrop";
            try
            {
                if (Pb44DragDrop != null)
                    Pb44DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Pb45_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb45_DragDrop";
            try
            {
                if (Pb45DragDrop != null)
                    Pb45DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb46_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb46_DragDrop";
            try
            {
                if (Pb46DragDrop != null)
                    Pb46DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Pb47_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb47_DragDrop";
            try
            {
                if (Pb47DragDrop != null)
                    Pb47DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb48_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb48_DragDrop";
            try
            {
                if (Pb48DragDrop != null)
                    Pb48DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb49_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb49_DragDrop";
            try
            {
                if (Pb49DragDrop != null)
                    Pb49DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Pb50_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb50_DragDrop";
            try
            {
                if (Pb50DragDrop != null)
                    Pb50DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb51_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb51_DragDrop";
            try
            {
                if (Pb51DragDrop != null)
                    Pb51DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb52_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb52_DragDrop";
            try
            {
                if (Pb52DragDrop != null)
                    Pb52DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb53_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb53_DragDrop";
            try
            {
                if (Pb53DragDrop != null)
                    Pb53DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb54_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:P54_DragDrop";
            try
            {
                if (Pb54DragDrop != null)
                    Pb54DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb55_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb55_DragDrop";
            try
            {
                if (Pb55DragDrop != null)
                    Pb55DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb56_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb56_DragDrop";
            try
            {
                if (Pb56DragDrop != null)
                    Pb56DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb57_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb57_DragDrop";
            try
            {
                if (Pb57DragDrop != null)
                    Pb57DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb58_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb58_DragDrop";
            try
            {
                if (Pb58DragDrop != null)
                    Pb58DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb59_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb59_DragDrop";
            try
            {
                if (Pb59DragDrop != null)
                    Pb59DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb60_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb60_DragDrop";
            try
            {
                if (Pb60DragDrop != null)
                    Pb60DragDrop(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void Pb40_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb40_DragEnter";
            try
            {
                if (Pb40DragEnter != null)
                    Pb40DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion PictureBox Drag Drop

        #region For DrwaBorder For Highlight PictureBox
        Graphics graphics;
        private void Pb1_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb1_MouseEnter";
            try
            {
                graphics = Pb1.CreateGraphics();
                Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb1.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb2_MouseHover(object sender, EventArgs e)
        {

        }

        private void Pb2_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb1_MouseEnter";
            try
            {
                graphics = Pb2.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb2.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb3_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb3_MouseEnter";
            try
            {
                graphics = Pb3.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb3.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb4_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb4_MouseEnter";
            try
            {
                graphics = Pb4.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb4.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb5_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb5_MouseEnter";
            try
            {
                graphics = Pb5.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb5.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb6_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb6_MouseEnter";
            try
            {
                graphics = Pb6.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb6.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb7_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb7_MouseEnter";
            try
            {
                graphics = Pb7.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb7.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb8_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb8_MouseEnter";
            try
            {
                graphics = Pb8.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb8.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb9_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb9_MouseEnter";
            try
            {
                graphics = Pb9.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb9.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb10_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb10_MouseEnter";
            try
            {
                graphics = Pb10.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb10.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb11_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb11_MouseEnter";
            try
            {
                graphics = Pb11.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb11.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb12_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb12_MouseEnter";
            try
            {
                graphics = Pb12.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb12.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb13_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb13_MouseEnter";
            try
            {
                graphics = Pb13.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb13.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb14_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb14_MouseEnter";
            try
            {
                graphics = Pb14.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb14.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb15_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb15_MouseEnter";
            try
            {
                graphics = Pb15.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb15.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb16_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb16_MouseEnter";
            try
            {
                graphics = Pb16.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb16.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb17_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb17_MouseEnter";
            try
            {
                graphics = Pb17.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb17.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb18_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb18_MouseEnter";
            try
            {
                graphics = Pb18.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb18.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Pb19_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb19_MouseEnter";
            try
            {
                graphics = Pb19.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb19.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb20_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb20_MouseEnter";
            try
            {
                graphics = Pb20.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb20.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb21_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb21_MouseEnter";
            try
            {
                graphics = Pb21.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb21.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb22_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb22_MouseEnter";
            try
            {
                graphics = Pb22.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb22.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb23_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb23_MouseEnter";
            try
            {
                graphics = Pb23.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb23.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb24_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb24_MouseEnter";
            try
            {
                graphics = Pb24.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb24.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb25_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb25_MouseEnter";
            try
            {
                graphics = Pb25.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb25.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb26_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb26_MouseEnter";
            try
            {
                graphics = Pb26.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb26.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb27_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb27_MouseEnter";
            try
            {
                graphics = Pb27.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb27.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb28_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb28_MouseEnter";
            try
            {
                graphics = Pb28.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb28.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb29_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb29_MouseEnter";
            try
            {
                graphics = Pb29.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb29.ClientRectangle, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb30_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb30_MouseEnter";
            try
            {
                graphics = Pb30.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb30.ClientRectangle, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb31_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb31_MouseEnter";
            try
            {
                graphics = Pb31.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb31.ClientRectangle, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb32_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb32_MouseEnter";
            try
            {
                graphics = Pb32.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb32.ClientRectangle, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb33_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb33_MouseEnter";
            try
            {
                graphics = Pb33.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb33.ClientRectangle, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb34_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb34_MouseEnter";
            try
            {
                graphics = Pb34.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb34.ClientRectangle, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb35_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb35_MouseEnter";
            try
            {
                graphics = Pb35.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb35.ClientRectangle, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb36_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb36_MouseEnter";
            try
            {
                graphics = Pb36.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb36.ClientRectangle, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb37_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb37_MouseEnter";
            try
            {
                graphics = Pb37.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb37.ClientRectangle, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Pb38_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb38_MouseEnter";
            try
            {
                graphics = Pb38.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb38.ClientRectangle, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb39_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb39_MouseEnter";
            try
            {
                graphics = Pb39.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb39.ClientRectangle, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Pb40_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb40_MouseEnter";
            try
            {
                graphics = Pb40.CreateGraphics();
                 Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb40.ClientRectangle, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid, color,2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb41_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb41_MouseEnter";
            try
            {
                graphics = Pb41.CreateGraphics();
                Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb41.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb42_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb42_MouseEnter";
            try
            {
                graphics = Pb42.CreateGraphics();
                Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb42.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb43_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb43_MouseEnter";
            try
            {
                graphics = Pb43.CreateGraphics();
                Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb43.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb44_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb40_MouseEnter";
            try
            {
                graphics = Pb44.CreateGraphics();
                Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb44.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Pb45_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb45_MouseEnter";
            try
            {
                graphics = Pb45.CreateGraphics();
                Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb45.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb46_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb46_MouseEnter";
            try
            {
                graphics = Pb46.CreateGraphics();
                Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb46.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb47_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb47_MouseEnter";
            try
            {
                graphics = Pb47.CreateGraphics();
                Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb47.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb48_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb48_MouseEnter";
            try
            {
                graphics = Pb48.CreateGraphics();
                Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb48.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb49_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb49_MouseEnter";
            try
            {
                graphics = Pb49.CreateGraphics();
                Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb49.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb50_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb50_MouseEnter";
            try
            {
                graphics = Pb50.CreateGraphics();
                Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb50.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Pb51_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb51_MouseEnter";
            try
            {
                graphics = Pb51.CreateGraphics();
                Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb51.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb52_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb52_MouseEnter";
            try
            {
                graphics = Pb52.CreateGraphics();
                Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb52.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb53_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb53_MouseEnter";
            try
            {
                graphics = Pb53.CreateGraphics();
                Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb53.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb54_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb54_MouseEnter";
            try
            {
                graphics = Pb54.CreateGraphics();
                Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb54.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb55_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb55_MouseEnter";
            try
            {
                graphics = Pb55.CreateGraphics();
                Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb55.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Pb56_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb56_MouseEnter";
            try
            {
                graphics = Pb56.CreateGraphics();
                Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb56.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb57_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb57_MouseEnter";
            try
            {
                graphics = Pb57.CreateGraphics();
                Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb57.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb58_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb58_MouseEnter";
            try
            {
                graphics = Pb58.CreateGraphics();
                Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb58.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Pb59_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb59_MouseEnter";
            try
            {
                graphics = Pb59.CreateGraphics();
                Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb59.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Pb60_MouseEnter(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb60_MouseEnter";
            try
            {
                graphics = Pb60.CreateGraphics();
                Color color = Color.DarkBlue;
                ControlPaint.DrawBorder(graphics, Pb60.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                //ControlPaint.DrawBorder(g, Pb1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Outset, Color.Red, 5, ButtonBorderStyle.Inset, Color.Red, 5, ButtonBorderStyle.Inset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion For DrwaBorder For Highlight PictureBox

        #region PictureBox MouseLeave
        private void Pb1_MouseLeave(object sender, EventArgs e)
        {
            Pb1.Invalidate();
        }
        private void Pb2_MouseLeave(object sender, EventArgs e)
        {
            Pb2.Invalidate();
        }
        private void Pb3_MouseLeave(object sender, EventArgs e)
        {
            Pb3.Invalidate();
        }
        private void Pb4_MouseLeave(object sender, EventArgs e)
        {
            Pb4.Invalidate();
        }
        private void Pb5_MouseLeave(object sender, EventArgs e)
        {
            Pb5.Invalidate();
        }
        private void Pb6_MouseLeave(object sender, EventArgs e)
        {
            Pb6.Invalidate();
        }
        private void Pb7_MouseLeave(object sender, EventArgs e)
        {
            Pb7.Invalidate();
        }
        private void Pb8_MouseLeave(object sender, EventArgs e)
        {
            Pb8.Invalidate();
        }
        private void Pb9_MouseLeave(object sender, EventArgs e)
        {
            Pb9.Invalidate();
        }

        private void Pb10_MouseLeave(object sender, EventArgs e)
        {
            Pb10.Invalidate();
        }
        private void Pb11_MouseLeave(object sender, EventArgs e)
        {
            Pb11.Invalidate();
        }
        private void Pb12_MouseLeave(object sender, EventArgs e)
        {
            Pb12.Invalidate();
        }
        private void Pb13_MouseLeave(object sender, EventArgs e)
        {
            Pb13.Invalidate();
        }
        private void Pb14_MouseLeave(object sender, EventArgs e)
        {
            Pb14.Invalidate();
        }
        private void Pb15_DragLeave(object sender, EventArgs e)
        {
            //Pb15.Invalidate();
        }
        private void Pb15_MouseLeave(object sender, EventArgs e)
        {
            Pb15.Invalidate();
        }
        private void Pb16_MouseLeave(object sender, EventArgs e)
        {
            Pb16.Invalidate();
        }
        private void Pb17_MouseLeave(object sender, EventArgs e)
        {
            Pb17.Invalidate();
        }
        private void Pb18_MouseLeave(object sender, EventArgs e)
        {
            Pb18.Invalidate();
        }
        private void Pb19_MouseLeave(object sender, EventArgs e)
        {
            Pb19.Invalidate();
        }
        private void Pb20_MouseLeave(object sender, EventArgs e)
        {
            Pb20.Invalidate();
        }
        private void Pb21_MouseLeave(object sender, EventArgs e)
        {
            Pb21.Invalidate();
        }
        private void Pb22_MouseLeave(object sender, EventArgs e)
        {
            Pb22.Invalidate();
        }
        private void Pb23_MouseLeave(object sender, EventArgs e)
        {
            Pb23.Invalidate();
        }
        private void Pb24_MouseLeave(object sender, EventArgs e)
        {
            Pb24.Invalidate();
        }

        private void Pb25_MouseLeave(object sender, EventArgs e)
        {
            Pb25.Invalidate();
        }

        private void Pb26_MouseLeave(object sender, EventArgs e)
        {
            Pb26.Invalidate();
        }

        private void Pb27_MouseLeave(object sender, EventArgs e)
        {
            Pb27.Invalidate();
        }

        private void Pb28_MouseLeave(object sender, EventArgs e)
        {
            Pb28.Invalidate();
        }

        private void Pb29_MouseLeave(object sender, EventArgs e)
        {
            Pb29.Invalidate();
        }

        private void Pb30_MouseLeave(object sender, EventArgs e)
        {
            Pb30.Invalidate();
        }
        private void Pb31_MouseLeave(object sender, EventArgs e)
        {
            Pb31.Invalidate();
        }
        private void Pb32_MouseLeave(object sender, EventArgs e)
        {
            Pb32.Invalidate();
        }
        private void Pb33_MouseLeave(object sender, EventArgs e)
        {
            Pb33.Invalidate();
        }
        private void Pb34_MouseLeave(object sender, EventArgs e)
        {
            Pb34.Invalidate();
        }
        private void Pb35_MouseLeave(object sender, EventArgs e)
        {
            Pb35.Invalidate();
        }
        private void Pb36_MouseLeave(object sender, EventArgs e)
        {
            Pb36.Invalidate();
        }
        private void Pb37_MouseLeave(object sender, EventArgs e)
        {
            Pb37.Invalidate();
        }
        private void Pb38_MouseLeave(object sender, EventArgs e)
        {
            Pb38.Invalidate();
        }
        private void Pb39_MouseLeave(object sender, EventArgs e)
        {
            Pb39.Invalidate();
        }
        private void Pb40_MouseLeave(object sender, EventArgs e)
        {
            Pb40.Invalidate();
        }
        private void Pb41_MouseLeave(object sender, EventArgs e)
        {
            Pb41.Invalidate();
        }

        private void Pb42_MouseLeave(object sender, EventArgs e)
        {
            Pb42.Invalidate();
        }

        private void Pb43_MouseLeave(object sender, EventArgs e)
        {
            Pb43.Invalidate();
        }

        private void Pb44_MouseLeave(object sender, EventArgs e)
        {
            Pb44.Invalidate();
        }

        private void Pb45_MouseLeave(object sender, EventArgs e)
        {
            Pb45.Invalidate();
        }

        private void Pb46_MouseLeave(object sender, EventArgs e)
        {
            Pb46.Invalidate();
        }

        private void Pb47_MouseLeave(object sender, EventArgs e)
        {
            Pb47.Invalidate();
        }

        private void Pb48_MouseLeave(object sender, EventArgs e)
        {
            Pb48.Invalidate();
        }

        private void Pb49_MouseLeave(object sender, EventArgs e)
        {
            Pb49.Invalidate();
        }

        private void Pb50_MouseLeave(object sender, EventArgs e)
        {
            Pb50.Invalidate();
        }

        private void Pb51_MouseLeave(object sender, EventArgs e)
        {
            Pb51.Invalidate();
        }

        private void Pb52_MouseLeave(object sender, EventArgs e)
        {
            Pb52.Invalidate();
        }

        private void Pb53_MouseLeave(object sender, EventArgs e)
        {
            Pb53.Invalidate();
        }

        private void Pb54_MouseLeave(object sender, EventArgs e)
        {
            Pb54.Invalidate();
        }

        private void Pb55_MouseLeave(object sender, EventArgs e)
        {
            Pb55.Invalidate();
        }

        private void Pb56_MouseLeave(object sender, EventArgs e)
        {
            Pb56.Invalidate();
        }

        private void Pb57_MouseLeave(object sender, EventArgs e)
        {
            Pb57.Invalidate();
        }

        private void Pb58_MouseLeave(object sender, EventArgs e)
        {
            Pb58.Invalidate();
        }

        private void Pb59_MouseLeave(object sender, EventArgs e)
        {
            Pb59.Invalidate();
        }

        private void Pb60_MouseLeave(object sender, EventArgs e)
        {
            Pb60.Invalidate();
        }
        #endregion PictureBox MouseLeave

        #region MouseDown
        private void Pb1_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb1_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    Graphics g;
                    GDSlave.PBox = Pb1;
                    g = Pb1.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(g, Pb1.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb1, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb2_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb2_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb2;
                    graphics = Pb2.CreateGraphics();
                    Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb2.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb2, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb3_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb3_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb3;
                    graphics = Pb3.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb3.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb3, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb4_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb4_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb4;
                    graphics = Pb4.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb4.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb4, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb5_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb5_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb5;
                    graphics = Pb5.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb5.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb5, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb6_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb6_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb6;
                    graphics = Pb6.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb6.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb6, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb7_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb7_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb7;
                    graphics = Pb7.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb7.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb7, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb8_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb8_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb8;
                    graphics = Pb8.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb8.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb8, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb9_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb9_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb9;
                    graphics = Pb9.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb9.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb9, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb10_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb10_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb10;
                    graphics = Pb10.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb10.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb10, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb11_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb1_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb11;
                    graphics = Pb11.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb11.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb11, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb12_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb12_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb12;
                    graphics = Pb12.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb12.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb12, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb13_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb13_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb13;
                    graphics = Pb13.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb13.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb13, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb14_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb14_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb14;
                    graphics = Pb14.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb14.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb14, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb15_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb15_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb15;
                    graphics = Pb15.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb15.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb15, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb16_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb16_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb16;
                    graphics = Pb16.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb16.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb16, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb17_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb17_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb17;
                    graphics = Pb17.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb17.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb17, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb18_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb18_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb18;
                    graphics = Pb18.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb18.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb18, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb19_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb19_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb19;
                    graphics = Pb19.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb19.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb19, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb20_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb20_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb20;
                    graphics = Pb20.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb20.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb20, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb21_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb21_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb21;
                    graphics = Pb21.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb21.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb21, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb22_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb22_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb22;
                    graphics = Pb22.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb22.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb22, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb23_MouseDown(object sender, MouseEventArgs e)
        {

        }
        private void Pb24_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb24_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb24;
                    graphics = Pb24.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb24.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb24, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb25_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb25_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb25;
                    graphics = Pb25.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb25.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb25, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb26_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb26_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb26;
                    graphics = Pb26.CreateGraphics();
                     Color color = Color.DarkBlue;
                    // ControlPaint.DrawBorder(graphics, Pb26.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ControlPaint.DrawBorder(graphics, Pb26.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb26, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb27_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb27_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb27;
                    graphics = Pb27.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb27.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb27.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb27, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb28_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb28_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb28;
                    graphics = Pb28.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb28.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb28.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb28, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb29_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb29_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb29;
                    graphics = Pb29.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb29.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb29.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb29, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb30_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb30_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb30;
                    graphics = Pb30.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb30.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb30.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb30, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb31_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb31_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb31;
                    graphics = Pb31.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb31.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb31.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb31, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb32_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb32_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb32;
                    graphics = Pb32.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb32.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb32.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb32, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb33_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb33_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb33;
                    graphics = Pb33.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb33.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb33.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb33, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb34_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb34_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb34;
                    graphics = Pb34.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb34.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb34.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb34, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb35_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb35_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb35;
                    graphics = Pb35.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb35.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb35.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb35, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb36_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb36_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb36;
                    graphics = Pb36.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb36.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb36.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb36, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb37_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb37_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb37;
                    graphics = Pb37.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb37.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb37.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb37, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb38_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb38_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb38;
                    graphics = Pb38.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb38.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb38.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb38, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb39_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb39_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb39;
                    graphics = Pb39.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb39.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb39.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb39, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb40_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb40_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb40;
                    graphics = Pb40.CreateGraphics();
                     Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb40.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb40.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb40, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb41_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb41_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb41;
                    graphics = Pb41.CreateGraphics();
                    Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb41.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb40.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb41, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb42_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb42_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb42;
                    graphics = Pb42.CreateGraphics();
                    Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb42.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb40.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb42, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb43_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb43_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb43;
                    graphics = Pb43.CreateGraphics();
                    Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb43.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb40.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb43, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Pb44_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb44_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb44;
                    graphics = Pb44.CreateGraphics();
                    Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb44.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb40.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb44, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb45_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb45_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb45;
                    graphics = Pb45.CreateGraphics();
                    Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb45.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb40.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb45, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb46_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb46_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb46;
                    graphics = Pb46.CreateGraphics();
                    Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb46.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb40.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb46, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb47_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb47_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb47;
                    graphics = Pb47.CreateGraphics();
                    Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb47.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb40.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb47, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Pb48_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb48_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb48;
                    graphics = Pb48.CreateGraphics();
                    Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb48.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb40.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb48, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb49_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb49_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb49;
                    graphics = Pb49.CreateGraphics();
                    Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb49.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb40.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb49, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb50_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb50_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb50;
                    graphics = Pb50.CreateGraphics();
                    Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb50.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb40.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb50, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb51_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb51_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb51;
                    graphics = Pb51.CreateGraphics();
                    Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb51.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb40.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb51, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb52_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb52_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb52;
                    graphics = Pb52.CreateGraphics();
                    Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb52.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb40.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb52, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Pb53_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb53_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb53;
                    graphics = Pb53.CreateGraphics();
                    Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb53.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb40.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb53, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb54_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb54_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb54;
                    graphics = Pb54.CreateGraphics();
                    Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb54.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb40.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb54, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb55_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb55_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb55;
                    graphics = Pb55.CreateGraphics();
                    Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb55.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb40.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb55, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb56_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb56_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb56;
                    graphics = Pb56.CreateGraphics();
                    Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb56.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb40.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb56, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb57_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb57_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb57;
                    graphics = Pb57.CreateGraphics();
                    Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb57.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb40.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb57, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb58_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb58_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb58;
                    graphics = Pb58.CreateGraphics();
                    Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb58.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb40.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb58, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb59_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb59_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb59;
                    graphics = Pb59.CreateGraphics();
                    Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb59.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb40.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb59, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb60_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb60_MouseDown";
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GDSlave.PBox = Pb60;
                    graphics = Pb60.CreateGraphics();
                    Color color = Color.DarkBlue;
                    ControlPaint.DrawBorder(graphics, Pb60.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder(graphics, Pb40.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
                    ContextMenuStrip cm = getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(Pb60, pt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion MouseDown
        public ContextMenuStrip getContextMenu()
        {
            //GDSlave.PBox = Pb1;
            //graphics = Pb1.CreateGraphics();
            //Color color = Color.Yellow;
            //ControlPaint.DrawBorder(graphics, Pb1.ClientRectangle, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid, color, 1, ButtonBorderStyle.Solid);
            ContextMenuStrip tcms = new ContextMenuStrip();
            ImageList ilTvItems = new ImageList();
            ilTvItems.ImageSize = new Size(10, 10);
            ilTvItems.ColorDepth = ColorDepth.Depth32Bit;
            tcms.ImageList = ilTvItems;
            //ToolStripMenuItem itmAdd = new ToolStripMenuItem("Clear", null, new System.EventHandler(this.mnuClear_Click));
            ToolStripMenuItem item = new ToolStripMenuItem("Clear Image", OpenProPlusConfigurator.Properties.Resources.Clear, new System.EventHandler(this.mnuClear_Click));
            item.TextAlign = ContentAlignment.TopLeft;
            item.AutoSize = true;
            tcms.Items.Add(item);
            return tcms;
        }
        public event ItemDragEventHandler TvSLDItemDrag;
        public event DragEventHandler TvSLDDragDrop;
        public event DragEventHandler TvSLDDragEnter;
        private void mnuClear_Click(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroupSLD:mnuClear_Click";
            try
            {
                if (GDSlave.PBox.Image != null)
                {
                    Graphics graphic = Graphics.FromImage(GDSlave.PBox.Image);
                    graphic.Clear(Color.White);
                    if (GDSlave.PBox.Image != null)
                    {
                        GDSlave.PBox.Image.Dispose();
                        GDSlave.PBox.Image = null;
                        GDSlave.PBox.Tag = "Blank";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void TvSLD_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (TvSLDItemDrag != null)
                TvSLDItemDrag(sender, e);
        }

        private void TvSLD_DragDrop(object sender, DragEventArgs e)
        {
            if (TvSLDDragDrop != null)
                TvSLDDragDrop(sender, e);
        }

        private void TvSLD_DragEnter(object sender, DragEventArgs e)
        {
            if (TvSLDDragEnter != null)
                TvSLDDragEnter(sender, e);
        }
        public event EventHandler btnSaveSLDClick;
        private void BtnSaveSLD_Click(object sender, EventArgs e)
        {
            if (btnSaveSLDClick != null)
                btnSaveSLDClick(sender, e);
        }

        private void splitContainer8_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TvSLD_NodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            //TvSLD.Nodes.OfType<TreeNode>().ToList().ForEach(x => x.BackColor = Color.White);
            //TvSLD.SelectedNode = e.Node;
            ////TvSLD_MouseDown(sender, null);
            //TvSLD.Focus();
            
        }

        bool isDown = false;
        int initialX;
        int initialY; System.Drawing.Graphics formGraphics;
        private void TvSLD_MouseMove(object sender, MouseEventArgs e)
        {
            //if (isDown == true)
            //{
            //    this.Refresh();
            //    Pen drwaPen = new Pen(Color.Navy, 1);
            //    int width = e.X - initialX, height = e.Y - initialY;
            //    Rectangle rect = new Rectangle(Math.Min(e.X, initialX),
            //                    Math.Min(e.Y, initialY),
            //                    Math.Abs(e.X - initialX),
            //                    Math.Abs(e.Y - initialY));

            //    Graphics g = TvSLD.CreateGraphics();
            //    g.DrawRectangle(drwaPen, rect);
            //}
        }

        private void TvSLD_MouseUp(object sender, MouseEventArgs e)
        {
            //isDown = false;
        }
        private void TvSLD_MouseDown(object sender, MouseEventArgs e)
        {
            //isDown = true;
            //initialX = e.X;
            //initialY = e.Y;
            TreeNode clickedNode =TvSLD.GetNodeAt(e.X, e.Y);
            
            if (NodeBounds(clickedNode).Contains(e.X, e.Y))
            {
                TvSLD.SelectedNode = clickedNode;
            }
        }
        // Returns the bounds of the specified node, including the region 
        // occupied by the node label and any node tag displayed.
        private Rectangle NodeBounds(TreeNode node)
        {
            //// Set the return value to the normal node bounds.
            //Rectangle bounds = node.Bounds;
            //TreeView tv = TvSLD;
            //if (tv != null)
            //{
            //    TreeNode selected = node;
            //    if (selected != null)
            //    {
            //    }
            //}  
            Rectangle bounds = new Rectangle();
            if (node != null)
            {
                // Set the return value to the normal node bounds.
                //Rectangle bounds = node.Bounds;
                bounds = node.Bounds;
                Font nodeFont = GetTreeNodeFont(node);
                if (node.Tag != null)
                {
                    // Retrieve a Graphics object from the TreeView handle
                    // and use it to calculate the display width of the tag.
                    Graphics g = TvSLD.CreateGraphics();
                    int tagWidth = (int)g.MeasureString
                        (node.Tag.ToString(), nodeFont).Width + 6;

                    // Adjust the node bounds using the calculated value.
                    bounds.Offset(tagWidth / 2, 0);
                    bounds = Rectangle.Inflate(bounds, tagWidth / 2, 0);
                    g.Dispose();
                }
                
            }
            return bounds;
            //}
            //else
            //{
            //    return false;
            //}

        }
        private void TvSLD_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void splitContainer6_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TblLayoutCSLD_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            //if (ChkSHGridLines.Checked == true)
            //{
            //    TblLayoutCSLD.Size = new Size(256, 409);
            //    TblLayoutCSLD.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            //}
            //else
            //{
            //    TblLayoutCSLD.Size = new Size(250, 400);
            //    TblLayoutCSLD.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
            //}

        }

        private void splitContainer7_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TvSLD_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void SCDisplayImage_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TblLayoutImagePanel_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private void TblLayoutImagePanel_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void TblLayoutImagePanel_MouseMove(object sender, MouseEventArgs e)
        {
            //int row = 0;
            //int verticalOffset = 0;
            //foreach (int h in TblLayoutImagePanel.GetRowHeights())
            //{
            //    int column = 0;
            //    int horizontalOffset = 0;
            //    foreach (int w in TblLayoutImagePanel.GetColumnWidths())
            //    {
            //        Rectangle rectangle = new Rectangle(horizontalOffset, verticalOffset, w, h);
                   
            //        if (rectangle.Contains(e.Location))
            //        {
            //            Control c = TblLayoutImagePanel.GetControlFromPosition(column, row);
            //            //MessageBox.Show(String.Format("row {0}, column {1} was clicked", row, column));
            //            return;
            //        }
                   
            //        horizontalOffset += w;
            //        column++;
            //    }
            //    verticalOffset += h;
            //    row++;
            //}
        }

        private void TblLayoutImagePanel_MouseHover(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void Pb8_Click(object sender, EventArgs e)
        {

        }

        private void Pb41_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb41_DragEnter";
            try
            {
                if (Pb41DragEnter != null)
                    Pb41DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb42_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb42_DragEnter";
            try
            {
                if (Pb42DragEnter != null)
                    Pb42DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb43_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb43_DragEnter";
            try
            {
                if (Pb43DragEnter != null)
                    Pb43DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb44_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb44_DragEnter";
            try
            {
                if (Pb44DragEnter != null)
                    Pb44DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Pb45_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb45_DragEnter";
            try
            {
                if (Pb45DragEnter != null)
                    Pb45DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb46_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb46_DragEnter";
            try
            {
                if (Pb46DragEnter != null)
                    Pb46DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb47_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb47_DragEnter";
            try
            {
                if (Pb47DragEnter != null)
                    Pb47DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb48_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb48_DragEnter";
            try
            {
                if (Pb48DragEnter != null)
                    Pb48DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb49_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb49_DragEnter";
            try
            {
                if (Pb49DragEnter != null)
                    Pb49DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb50_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb50_DragEnter";
            try
            {
                if (Pb50DragEnter != null)
                    Pb50DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Pb51_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb51_DragEnter";
            try
            {
                if (Pb51DragEnter != null)
                    Pb51DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb52_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb51_DragEnter";
            try
            {
                if (Pb51DragEnter != null)
                    Pb51DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb53_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb53_DragEnter";
            try
            {
                if (Pb53DragEnter != null)
                    Pb53DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb54_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb54_DragEnter";
            try
            {
                if (Pb54DragEnter != null)
                    Pb54DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb55_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb55_DragEnter";
            try
            {
                if (Pb55DragEnter != null)
                    Pb55DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb56_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb56_DragEnter";
            try
            {
                if (Pb56DragEnter != null)
                    Pb56DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb57_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb57_DragEnter";
            try
            {
                if (Pb57DragEnter != null)
                    Pb57DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb58_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb58_DragEnter";
            try
            {
                if (Pb58DragEnter != null)
                    Pb58DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Pb59_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb59_DragEnter";
            try
            {
                if (Pb59DragEnter != null)
                    Pb59DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb60_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "ucGroupSLD:Pb60_DragEnter";
            try
            {
                if (Pb60DragEnter != null)
                    Pb60DragEnter(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #region .. Double Buffered function ..
        public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;
            System.Reflection.PropertyInfo aProp = typeof(System.Windows.Forms.Control).GetProperty("DoubleBuffered",
            System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.Instance);
            aProp.SetValue(c, true, null);
        }

        #endregion
        #region .. code for Flucuring ..
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        #endregion
    }
}
