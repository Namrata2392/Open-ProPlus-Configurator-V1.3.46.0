using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

namespace OpenProPlusConfigurator
{
    public class DRConfiguration
    {
        ucDRConfig ucdr = null;
        List<DRConfig> drList = new List<DRConfig>();
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        
        public DRConfiguration(ucDRConfig ucDRconfig)
        {
            try
            {
                addListHeaders();
                FillOptions();
                //Ajay: 11/01/2019
                ucdr = ucDRconfig;
            }
            catch { }
        }

        private void Ucdr_lvTagListSelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "Ucdr_lvTagListSelectedIndexChanged";
            try
            {

            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ucdr_lvChannelListSelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "Ucdr_lvChannelListSelectedIndexChanged";
            try
            {

            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ucdr_lvTagListItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string strRoutineName = "Ucdr_lvTagListItemSelectionChanged";
            try
            {

            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ucdr_lvChannelListItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string strRoutineName = "Ucdr_lvChannelListItemSelectionChanged";
            try
            {

            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ucdr_lvTagListDoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "Ucdr_lvTagListDoubleClick";
            try
            {

            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ucdr_lvChannelListDoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "Ucdr_lvChannelListDoubleClick";
            try
            {

            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ucdr_btnChnlLastClick(object sender, EventArgs e)
        {
            string strRoutineName = "Ucdr_btnChnlLastClick";
            try
            {

            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ucdr_btnChnlNextClick(object sender, EventArgs e)
        {
            string strRoutineName = "Ucdr_btnChnlNextClick";
            try
            {

            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ucdr_btnChnlPrevClick(object sender, EventArgs e)
        {
            string strRoutineName = "Ucdr_btnChnlPrevClick";
            try
            {

            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ucdr_btnLastClick(object sender, EventArgs e)
        {
            string strRoutineName = "Ucdr_btnLastClick";
            try
            {

            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ucdr_btnNextClick(object sender, EventArgs e)
        {
            string strRoutineName = "Ucdr_btnNextClick";
            try
            {

            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ucdr_btnPrevClick(object sender, EventArgs e)
        {
            string strRoutineName = "Ucdr_btnPrevClick";
            try
            {

            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ucdr_btnFirstClick(object sender, EventArgs e)
        {
            string strRoutineName = "Ucdr_btnFirstClick";
            try
            {

            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ucdr_btnChnlFirstClick(object sender, EventArgs e)
        {
            string strRoutineName = "Ucdr_btnChnlFirstClick";
            try
            {

            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ucdr_btnCancelTagClick(object sender, EventArgs e)
        {
            string strRoutineName = "Ucdr_btnCancelTagClick";
            try
            {

            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ucdr_btnCancelChannelClick(object sender, EventArgs e)
        {
            string strRoutineName = "Ucdr_btnCancelChannelClick";
            try
            {

            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ucdr_btnDoneTagClick(object sender, EventArgs e)
        {
            string strRoutineName = "Ucdr_btnDoneTagClick";
            try
            {

            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ucdr_btnDoneChannelClick(object sender, EventArgs e)
        {
            string strRoutineName = "Ucdr_btnDoneChannelClick";
            try
            {

            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ucdr_btnDeleteTagClick(object sender, EventArgs e)
        {
            string strRoutineName = "Ucdr_btnDeleteTagClick";
            try
            {

            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ucdr_btnDeleteChannelClick(object sender, EventArgs e)
        {
            string strRoutineName = "Ucdr_btnDeleteChannelClick";
            try
            {

            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ucdr_btnAddTagClick(object sender, EventArgs e)
        {
            string strRoutineName = "Ucdr_btnAddTagClick";
            try
            {
                mode = Mode.ADD;
                editIndex = -1;
                Utils.resetValues(ucdr.grpTag);
                Utils.showNavigation(ucdr.grpTag, false);
                //FillOptions();
                LoadTagDefaults();
                ucdr.grpTag.Visible = true;
                ucdr.txtbxFun.Focus();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ucdr_btnAddChannelClick(object sender, EventArgs e)
        {
            string strRoutineName = "Ucdr_btnAddChannelClick";
            try
            {
                mode = Mode.ADD;
                editIndex = -1;
                Utils.resetValues(ucdr.grpChannel);
                Utils.showNavigation(ucdr.grpChannel, false);
                FillOptions();
                LoadChannelDefaults();
                ucdr.grpChannel.Visible = true;
                ucdr.txtbxAcc.Focus();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addListHeaders()
        {
            string strRoutineName = "addListHeaders";
            try
            {
                if (ucdr != null)
                {
                    ucdr.lvTagList.Columns.Add("SN", 0, HorizontalAlignment.Left);
                    ucdr.lvTagList.Columns.Add("Fun", 100, HorizontalAlignment.Left);
                    ucdr.lvTagList.Columns.Add("Inf", 100, HorizontalAlignment.Left);
                    ucdr.lvTagList.Columns.Add("Name", -2, HorizontalAlignment.Left);

                    ucdr.lvChannelList.Columns.Add("SN", 0, HorizontalAlignment.Left);
                    ucdr.lvChannelList.Columns.Add("Acc", 70, HorizontalAlignment.Left);
                    ucdr.lvChannelList.Columns.Add("Name", 100, HorizontalAlignment.Left);
                    ucdr.lvChannelList.Columns.Add("Unit", 70, HorizontalAlignment.Left);
                    ucdr.lvChannelList.Columns.Add("Vori", 70, HorizontalAlignment.Left);
                    ucdr.lvChannelList.Columns.Add("Phase", 70, HorizontalAlignment.Left);
                    ucdr.lvChannelList.Columns.Add("Pors", 70, HorizontalAlignment.Left);
                    ucdr.lvChannelList.Columns.Add("Min", 70, HorizontalAlignment.Left);
                    ucdr.lvChannelList.Columns.Add("Max", "Max", -2, HorizontalAlignment.Left, String.Empty);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillOptions()
        {
            string strRoutineName = "DR : fillOptions";
            try
            {
                //ucdr.cmbUnit.Items.Clear();
                //ucdr.cmbPhase.Items.Clear();
                //ucdr.cmbVori.Items.Clear();
                //ucdr.cmbPors.Items.Clear();
                //ucdr.cmbPhase.DataSource = Utils.getOpenProPlusHandle().getDRSimpleTypeValues("channel_phase");
                //ucdr.cmbVori.DataSource = Utils.getOpenProPlusHandle().getDRSimpleTypeValues("channel_vori");
                //ucdr.cmbPors.DataSource = Utils.getOpenProPlusHandle().getDRSimpleTypeValues("channel_pors");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void refreshlvTagList()
        {
            string strRoutineName = "refreshlvTagList";
            try
            {
                int cnt = 0;
                ucdr.lvTagList.Items.Clear();

                foreach (DRConfig dr in drList)
                {
                    string[] row = new string[3];
                    row[0] = dr.fun.ToString();
                    row[1] = dr.inf.ToString();
                    row[2] = dr.tagName;

                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucdr.lvTagList.Items.Add(lvItem);
                }
                ucdr.lblTagRecords.Text = drList.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void refreshlvChannelList()
        {
            string strRoutineName = "refreshlvChannelList";
            try
            {
                int cnt = 0;
                ucdr.lvChannelList.Items.Clear();

                foreach (DRConfig dr in drList)
                {
                    string[] row = new string[8];
                    row[0] = dr.acc.ToString();
                    row[1] = dr.channelName.ToString();
                    row[2] = dr.unit;
                    row[3] = dr.vori;
                    row[4] = dr.phase;
                    row[5] = dr.pors;
                    row[6] = dr.min.ToString();
                    row[7] = dr.max.ToString();

                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucdr.lvChannelList.Items.Add(lvItem);
                }
                ucdr.lblChannelRecords.Text = drList.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadTagDefaults()
        {
            string strRoutineName = "LoadTagDefaults";
            try
            {
                ucdr.txtbxFun.Text = "";
                ucdr.txtbxInf.Text = "";
                ucdr.txtbxTagName.Text = "";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadChannelDefaults()
        {
            string strRoutineName = "LoadChannelDefaults";
            try
            {
                ucdr.txtbxAcc.Text = "";
                ucdr.txtbxChnlName.Text = "";
                ucdr.txtbxUnit.Text = "";
                ucdr.cmbVori.SelectedIndex = 0;
                ucdr.cmbPhase.SelectedIndex = 0;
                ucdr.cmbPors.SelectedIndex = 0;
                ucdr.txtbxMin.Text = "";
                ucdr.txtbxMax.Text = "";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
