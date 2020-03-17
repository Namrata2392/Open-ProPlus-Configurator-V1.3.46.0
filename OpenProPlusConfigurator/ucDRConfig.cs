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
using System.Xml;
using System.IO;
using System.Reflection;

namespace OpenProPlusConfigurator
{
    public partial class ucDRConfig : UserControl
    {
        List<DRConfig> drTagList = new List<DRConfig>();
        List<DRConfig> drChannelList = new List<DRConfig>();
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        private string filePath = "";
        private bool FileSaved = true;
        frmOpenProPlus frmopp = null;
        OpenProPlus_Config csoppconfig = null;
        private bool VersionMatch = false; //Ajay: 15/01/2019
        //Ajay: 16/01/2019
        private int MinTag = 1;
        private int MaxTag = 1000;
        private int MinChannel = 1;
        private int MaxChannel = 200;
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }

        public ucDRConfig(frmOpenProPlus frm, OpenProPlus_Config csOpp)
        {
            InitializeComponent();
            addListHeaders();
            //Ajay: 11/01/2019
            frmopp = frm;
            csoppconfig = csOpp;
        }

        private void btnAddTag_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnAddTag_Click";
            try
            {
                if (drTagList != null && drTagList.Count < MaxTag)
                {
                    mode = Mode.ADD;
                    editIndex = -1;
                    Utils.resetValues(grpTag);
                    Utils.showNavigation(grpTag, false);
                    //FillOptions();
                    LoadTagDefaults();
                    grpTag.Visible = true;
                    txtbxFun.Focus();
                    HideNavigationButtons("tag", false);
                }
                else
                {
                    MessageBox.Show("Maximum " + MaxTag + " tag entries allowed!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteTag_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnDeleteTag_Click";
            try
            {
                if (lvTagList.CheckedItems.Count <= 0)
                {
                    MessageBox.Show("Please select tag entry to delete!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }
                DialogResult result = MessageBox.Show(Globals.PROMPT_DELETE_ENTRY, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                {
                    return;
                }
                List<DRConfig> lst = drTagList;
                for (int i = lvTagList.Items.Count - 1; i >= 0; i--)
                {
                    if (lvTagList.Items[i].Checked)
                    {
                        lst.Remove(lst.ElementAt(i));
                    }
                }
                drTagList = lst;
                refreshlvTagList();
                grpTag.Visible = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddChannel_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnAddChannel_Click";
            try
            {
                if (drChannelList != null && drChannelList.Count < MaxTag)
                {
                    mode = Mode.ADD;
                    editIndex = -1;
                    Utils.resetValues(grpChannel);
                    Utils.showNavigation(grpChannel, false);
                    FillOptions();
                    LoadChannelDefaults();
                    grpChannel.Visible = true;
                    txtbxAcc.Focus();
                    HideNavigationButtons("channel", false);
                }
                else
                {
                    MessageBox.Show("Maximum " + MaxChannel + " channel entries allowed!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteChannel_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnDeleteChannel_Click";
            try
            {
                if (lvChannelList.CheckedItems.Count <= 0)
                {
                    MessageBox.Show("Please select channel entry to delete!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }
                DialogResult result = MessageBox.Show(Globals.PROMPT_DELETE_ENTRY, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                {
                    return;
                }
                List<DRConfig> lst = drChannelList;
                for (int i = lvChannelList.Items.Count - 1; i >= 0; i--)
                {
                    if (lvChannelList.Items[i].Checked)
                    {
                        lst.Remove(lst.ElementAt(i));
                    }
                }
                drChannelList = lst;
                refreshlvChannelList();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDoneTag_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnDoneTag_Click";
            try
            {
                if (!Validate(grpTag)) return;

                if (mode == Mode.ADD)
                {
                    if (drTagList.Where(x => x.fun == txtbxFun.Text.Trim() && x.inf == txtbxInf.Text.Trim()).Any())
                    {
                        MessageBox.Show("fun=" + txtbxFun.Text.Trim() + " and inf=" + txtbxInf.Text.Trim() + " is already exists!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    else
                    {
                        DRConfig dr = new DRConfig();
                        dr.fun = txtbxFun.Text;
                        dr.inf = txtbxInf.Text;
                        dr.tagName = txtbxTagName.Text;
                        drTagList.Add(dr);
                    }
                }
                else if (mode == Mode.EDIT)
                {
                    foreach (DRConfig tg in drTagList)
                    {
                        int iIndex = drTagList.IndexOf(drTagList.Where(x => x.fun == tg.fun && x.inf == tg.inf).FirstOrDefault());
                        if (iIndex != editIndex && tg.fun == txtbxFun.Text.Trim() && tg.inf == txtbxInf.Text.Trim())
                        {
                            MessageBox.Show("fun=" + txtbxFun.Text.Trim() + " and inf=" + txtbxInf.Text.Trim() + " is already exists!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            return;
                        }
                    }
                    drTagList[editIndex].updateAttributes(Utils.getKeyValueAttributes(grpTag));
                }
                FileSaved = false;
                refreshlvTagList();
                grpTag.Visible = false;
                mode = Mode.NONE;
                //editIndex = -1;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelTag_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnCancelTag_Click";
            try
            {
                grpTag.Visible = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDoneChannel_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnDoneChannel_Click";
            try
            {
                if (!Validate(grpChannel)) return;

                if (mode == Mode.ADD)
                {
                    if (drChannelList.Where(x => x.acc == txtbxAcc.Text.Trim()).Any())
                    {
                        MessageBox.Show("acc=" + txtbxAcc.Text.Trim() + " is already exists!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    DRConfig dr = new DRConfig();
                    dr.acc = txtbxAcc.Text;
                    dr.channelName = txtbxChnlName.Text;
                    dr.unit = txtbxUnit.Text;
                    dr.vori = cmbVori.Text;
                    dr.phase = cmbPhase.Text;
                    dr.pors = cmbPors.Text;
                    dr.min = txtbxMin.Text;
                    dr.max = txtbxMax.Text;
                    drChannelList.Add(dr);
                }
                else if (mode == Mode.EDIT)
                {
                    foreach (DRConfig dr in drChannelList)
                    {
                        int iIndex = drChannelList.IndexOf(drChannelList.Where(x => x.acc == dr.acc).FirstOrDefault());
                        if (iIndex != editIndex && dr.acc == txtbxAcc.Text.Trim())
                        {
                            MessageBox.Show("acc=" + txtbxAcc.Text.Trim() + " is already exists!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            return;
                        }
                    }
                    drChannelList[editIndex].updateAttributes(Utils.getKeyValueAttributes(grpChannel));
                }
                FileSaved = false;
                refreshlvChannelList();
                grpChannel.Visible = false;
                mode = Mode.NONE;
                //editIndex = -1;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelChannel_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnCancelChannel_Click";
            try
            {
                grpChannel.Visible = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnFirst_Click";
            try
            {
                if (lvTagList.Items.Count <= 0) return;
                editIndex = 0;
                LoadTagValues();
                refreshlvTagList();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnPrev_Click";
            try
            {
                btnDoneTag_Click(null, null);
                if (lvTagList.Items.Count <= 0) return;
                editIndex--;
                LoadTagValues();
                refreshlvTagList();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnNext_Click";
            try
            {
                btnDoneTag_Click(null, null);
                if (lvTagList.Items.Count <= 0) return;
                editIndex++;
                LoadTagValues();
                refreshlvTagList();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnLast_Click";
            try
            {
                btnDoneTag_Click(null, null);
                if (lvTagList.Items.Count <= 0) return;
                editIndex = drTagList.Count - 1;
                LoadTagValues();
                refreshlvTagList();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnChnlFirst_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnChnlFirst_Click";
            try
            {
                if (lvChannelList.Items.Count <= 0) return;
                editIndex = 0;
                LoadChannelValues();
                refreshlvChannelList();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnChnlPrev_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnChnlPrev_Click";
            try
            {
                if (lvChannelList.Items.Count <= 0) return;
                editIndex--;
                btnDoneChannel_Click(null, null);
                LoadChannelValues();
                refreshlvChannelList();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnChnlNext_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnChnlNext_Click";
            try
            {
                btnDoneChannel_Click(null, null);
                if (lvChannelList.Items.Count <= 0) return;
                editIndex++;
                LoadChannelValues();
                refreshlvChannelList();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnChnlLast_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnChnlLast_Click";
            try
            {
                if (lvChannelList.Items.Count <= 0) return;
                editIndex = drChannelList.Count - 1;
                LoadChannelValues();
                refreshlvChannelList();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvTagListDouble_Click(object sender, EventArgs e)
        {
            string strRoutineName = "lvTagListDouble_Click";
            try
            {
                mode = Mode.EDIT;
                editIndex = lvTagList.FocusedItem.Index;
                LoadTagDefaults();
                LoadTagValues();
                HideNavigationButtons("tag", true);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvChannelListDouble_Click(object sender, EventArgs e)
        {
            string strRoutineName = "lvChannelListDouble_Click";
            try
            {
                mode = Mode.EDIT;
                editIndex = lvChannelList.FocusedItem.Index;
                FillOptions();
                LoadChannelDefaults();
                LoadChannelValues();
                HideNavigationButtons("channel", true);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //private void lvTagListItem_SelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        //{
        //    string strRoutineName = "lvTagListItem_SelectionChanged";
        //    try
        //    {

        //    }
        //    catch (Exception Ex)
        //    {
        //        MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        private void lvChannelListItem_SelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string strRoutineName = "lvChannelListItem_SelectionChanged";
            try
            {
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void pbHdr_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "pbHdr_MouseDown";
            try
            {
                Utils.handleMouseDown(sender, e);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void pbHdr_MouseMove(object sender, MouseEventArgs e)
        {
            string strRoutineName = "pbHdr_MouseMove";
            try
            {
                Utils.handleMouseMove(grpTag, sender, e);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lblHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "lblHdrText_MouseDown";
            try
            {
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lblHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            string strRoutineName = "lblHdrText_MouseMove";
            try
            {
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lblAIMHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "lblAIMHdrText_MouseDown";
            try
            {
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lblAIMHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            string strRoutineName = "lblAIMHdrText_MouseMove";
            try
            {
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void pbAIMHdr_MouseDown(object sender, MouseEventArgs e)
        {
            string strRoutineName = "pbAIMHdr_MouseDown";
            try
            {
                Utils.handleMouseDown(sender, e);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void pbAIMHdr_MouseMove(object sender, MouseEventArgs e)
        {
            string strRoutineName = "pbAIMHdr_MouseMove";
            try
            {
                Utils.handleMouseMove(grpChannel, sender, e);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvTagList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "lvTagList_SelectedIndexChanged";
            try
            {
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvChannelList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "lvChannelList_SelectedIndexChanged";
            try
            {
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
                //Tag List
                lvTagList.Columns.Add("SN", 50, HorizontalAlignment.Left);
                lvTagList.Columns.Add("Fun", 100, HorizontalAlignment.Left);
                lvTagList.Columns.Add("Inf", 100, HorizontalAlignment.Left);
                lvTagList.Columns.Add("Name", "Name", -2, HorizontalAlignment.Left, string.Empty);

                //Channel List
                lvChannelList.Columns.Add("SN", 50, HorizontalAlignment.Left);
                lvChannelList.Columns.Add("Acc", 70, HorizontalAlignment.Left);
                lvChannelList.Columns.Add("Name", 100, HorizontalAlignment.Left);
                lvChannelList.Columns.Add("Unit", 70, HorizontalAlignment.Left);
                lvChannelList.Columns.Add("Vori", 70, HorizontalAlignment.Left);
                lvChannelList.Columns.Add("Phase", 70, HorizontalAlignment.Left);
                lvChannelList.Columns.Add("Pors", 70, HorizontalAlignment.Left);
                lvChannelList.Columns.Add("Min", 70, HorizontalAlignment.Left);
                lvChannelList.Columns.Add("Max", "Max", -2, HorizontalAlignment.Left, String.Empty);

            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Clear()
        {
            txtbxDeviceType.Text = "";
            txtbxDeviceDesc.Text = "";
            txtbxDrXsdVer.Text = "";
            drTagList.Clear();
            drChannelList.Clear();
            refreshlvTagList();
            refreshlvChannelList();
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

                cmbPhase.DataSource = Utils.getOpenProPlusHandle().getDRSimpleTypeValues("channel_phase");
                cmbVori.DataSource = Utils.getOpenProPlusHandle().getDRSimpleTypeValues("channel_vori");
                cmbPors.DataSource = Utils.getOpenProPlusHandle().getDRSimpleTypeValues("channel_pors");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 16/01/2019
        private bool Validate(GroupBox grp)
        {
            try
            {
                foreach(Control c in grp.Controls)
                {
                    if(c is TextBox || c is ComboBox)
                    {
                        if(string.IsNullOrEmpty(c.Text))
                        {
                            MessageBox.Show("All fields are mandatory!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(this.Name + ": " + MethodBase.GetCurrentMethod().Name + ": Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public void refreshlvTagList()
        {
            string strRoutineName = "refreshlvTagList";
            try
            {
                int cnt = 0;
                lvTagList.Items.Clear();

                int i = 1;
                foreach (DRConfig dr in drTagList)
                {
                    string[] row = new string[4];
                    row[0] = i.ToString();
                    row[1] = dr.fun.ToString();
                    row[2] = dr.inf.ToString();
                    row[3] = dr.tagName;

                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    lvTagList.Items.Add(lvItem);
                    i++;
                }
                lblTagRecords.Text = drTagList.Count.ToString();
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
                lvChannelList.Items.Clear();
                int i = 1;
                foreach (DRConfig dr in drChannelList)
                {
                    string[] row = new string[9];
                    row[0] = i.ToString();
                    row[1] = dr.acc.ToString();
                    row[2] = dr.channelName.ToString();
                    row[3] = dr.unit;
                    row[4] = dr.vori;
                    row[5] = dr.phase;
                    row[6] = dr.pors;
                    row[7] = dr.min.ToString();
                    row[8] = dr.max.ToString();

                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    lvChannelList.Items.Add(lvItem);
                    i++;
                }
                lblChannelRecords.Text = drChannelList.Count.ToString();
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
                txtbxFun.Text = "";
                txtbxInf.Text = "";
                txtbxTagName.Text = "";
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
                txtbxAcc.Text = "";
                txtbxChnlName.Text = "";
                txtbxUnit.Text = "";
                cmbVori.SelectedIndex = 0;
                cmbPhase.SelectedIndex = 0;
                cmbPors.SelectedIndex = 0;
                txtbxMin.Text = "";
                txtbxMax.Text = "";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadTagValues()
        {
            string strRoutineName = "LoadTagValues";
            try
            {
                if (editIndex >= 0 && editIndex <= drTagList.Count - 1)
                {
                    DRConfig drObj = drTagList.ElementAt(editIndex);
                    //drList.Where(x => x.drType == DRConfig.DRType.Tag && x.fun == fun && x.inf == inf).Select(x => x).SingleOrDefault();
                    if (drObj != null)
                    {
                        if (drObj.fun != null) txtbxFun.Text = drObj.fun.ToString();
                        if (drObj.inf != null) txtbxInf.Text = drObj.inf.ToString();
                        if (drObj.tagName != null) txtbxTagName.Text = drObj.tagName.ToString();
                    }
                }
                grpTag.Visible = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadChannelValues()
        {
            string strRoutineName = "LoadChannelValues";
            try
            {
                if (editIndex >= 0 && editIndex <= drChannelList.Count-1)
                {
                    DRConfig drObj = drChannelList.ElementAt(editIndex);
                    //drList.Where(x => x.drType == DRConfig.DRType.Channel && x.acc == acc).Select(x => x).SingleOrDefault();
                    if (drObj != null)
                    {
                        txtbxAcc.Text = drObj.acc.ToString();
                        txtbxChnlName.Text = drObj.channelName.ToString();
                        txtbxMin.Text = drObj.min.ToString();
                        txtbxMax.Text = drObj.max.ToString();
                        txtbxUnit.Text = drObj.unit.ToString();
                        if (drObj.phase != null) cmbPhase.SelectedIndex = cmbPhase.Items.IndexOf(drObj.phase);
                        else cmbPhase.SelectedIndex = -1;
                        if (drObj.vori != null) cmbVori.SelectedIndex = cmbVori.Items.IndexOf(drObj.vori);
                        else cmbVori.SelectedIndex = -1;
                        if (drObj.pors != null) cmbPors.SelectedIndex = cmbPors.Items.IndexOf(drObj.pors);
                        else cmbPors.SelectedIndex = -1;
                    }
                }
                grpChannel.Visible = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void HideNavigationButtons(string drType, bool Visible)
        {
            string strRoutineName = "HideNavigationButtons";
            try
            {
                if (drType.ToLower() == "tag")
                {
                    btnFirst.Visible = btnPrev.Visible = btnNext.Visible = btnLast.Visible = Visible;
                }
                else if (drType.ToLower() == "channel")
                {
                    btnChnlFirst.Visible = btnChnlPrev.Visible = btnChnlNext.Visible = btnChnlLast.Visible = Visible;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lnkbtnDeleteAllTags_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string strRoutineName = "lnkbtnDeleteAllTags_LinkClicked";
            try
            {
                DialogResult result = MessageBox.Show(Globals.PROMPT_DELETE_ENTRY, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                {
                    return;
                }

                drTagList.Clear();
                refreshlvTagList();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lnkbtnDeleteAllChannels_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string strRoutineName = "lnkbtnDeleteAllChannels_LinkClicked";
            try
            {
                DialogResult result = MessageBox.Show(Globals.PROMPT_DELETE_ENTRY, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                {
                    return;
                }
                drChannelList.Clear();
                refreshlvChannelList();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtbxFun_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtbxInf_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtbxAcc_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtbxMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtbxMax_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnBrowse_Click";
            try
            {
                if (!FileSaved)
                {
                    if (MessageBox.Show("Do you want to save the file?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        SaveFileDialog svfiledlg = new SaveFileDialog();
                        svfiledlg.Title = "Save DR Configuration File";
                        svfiledlg.InitialDirectory = Path.GetFullPath(Environment.SpecialFolder.MyDocuments.ToString());
                        svfiledlg.DefaultExt = "xml";
                        svfiledlg.FileName = "DRConfig";
                        svfiledlg.Filter = "xml files (*.xml)|*.xml";
                        svfiledlg.CheckFileExists = false;
                        svfiledlg.CheckPathExists = true;
                        svfiledlg.SupportMultiDottedExtensions = false;
                        if (svfiledlg.ShowDialog() == DialogResult.OK)
                        {
                            lblFilePath.Text = filePath = svfiledlg.FileName;
                            SaveXML(svfiledlg.FileName);
                            FileSaved = true;
                        }
                        else { }
                    }
                    else { }
                }

                OpenFileDialog opnfiledlg = new OpenFileDialog();
                opnfiledlg.Title = "Select DR Configuration File";
                opnfiledlg.DefaultExt = "xml";
                opnfiledlg.Filter = "xml files (*.xml)|*.xml";
                opnfiledlg.CheckFileExists = true;
                opnfiledlg.CheckPathExists = true;
                opnfiledlg.SupportMultiDottedExtensions = false;
                if (opnfiledlg.ShowDialog() == DialogResult.OK)
                {
                    Clear();
                    lblFilePath.Text = "";
                    lblFilePath.Text = filePath = opnfiledlg.FileName;

                    if (!CheckVersions(filePath))
                    {
                        VersionMatch = false;
                        DialogResult rslt = MessageBox.Show("Do you still want to continue with mismatched versions of OpenPro+Configurator, xml and dr_config.xsd file?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (rslt == DialogResult.No)
                        {
                            return;
                        }
                        else { }
                    }
                    else { VersionMatch = true; }

                    frmopp.lvValidationMessages.Items.Clear();
                    ListViewItem lvi = new ListViewItem("Validating file: " + filePath);
                    frmopp.lvValidationMessages.Items.Add(lvi);
                    lvi = new ListViewItem("");
                    frmopp.lvValidationMessages.Items.Add(lvi);

                    bool IsXmlValid = false;
                    string errMsg = "";
                    int result = csoppconfig.loadDRxml(filePath, out IsXmlValid);
                    if (IsXmlValid)
                    {
                        frmopp.lvValidationMessages.Items.Clear();
                        frmopp.pnlValidationMessages.Visible = false;
                        frmopp.pnlValidationMessages.SendToBack();
                    }
                    else
                    {
                        frmopp.pnlValidationMessages.Visible = true;
                        frmopp.pnlValidationMessages.BringToFront();
                    }
                    if (result == -1) errMsg = "File doesnot exist!!!";
                    else if (result == -2) errMsg = "File is not a well-formed XML!!!";
                    else if (result == -3) errMsg = "XSD file is not valid!!!";
                    else if (result == -4) errMsg = "File is not a valid XML, as per the schema!!!";
                    else if (result == -5) errMsg = "MODBUS Slave Port is Not Valid as per the schema";

                    lvi = new ListViewItem("");
                    frmopp.lvValidationMessages.Items.Add(lvi);
                    lvi = new ListViewItem(errMsg);
                    frmopp.lvValidationMessages.Items.Add(lvi);
                    frmopp.lvValidationMessages.EnsureVisible(frmopp.lvValidationMessages.Items.Count - 1);
                    if (result < 0)
                    {
                        MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                    LoadXML(filePath);
                    refreshlvTagList();
                    refreshlvChannelList();
                    FileSaved = true;
                }
                else { return; }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadXML(string FilePath)
        {
            string strRoutineName = "LoadXML";
            try
            {
                if (File.Exists(FilePath))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(FilePath);
                    if (doc != null)
                    {
                        Clear();
                        XmlElement root = doc.DocumentElement;
                        XmlNodeList xnl = root.ChildNodes;
                        DRConfig dr = null;
                        foreach (XmlNode xn in xnl)
                        {
                            if (xn.Name == "details") //Ajay"; 15/01/2019
                            {
                                string xaDeviceType = xn.Attributes["DeviceType"].Value;
                                if (!string.IsNullOrEmpty(xaDeviceType)) txtbxDeviceType.Text = xaDeviceType;
                                string xaDeviceDesc = xn.Attributes["DeviceDescription"].Value;
                                if (!string.IsNullOrEmpty(xaDeviceDesc)) txtbxDeviceDesc.Text = xaDeviceDesc;
                                string xaDrXsdVer = xn.Attributes["DrXsdVer"].Value;
                                if (!string.IsNullOrEmpty(xaDrXsdVer)) txtbxDrXsdVer.Text = xaDrXsdVer;
                                txtbxDrXsdVer.Enabled = false;
                            }
                            if (xn.Name == "tag")
                            {
                                dr = new DRConfig();
                                string xaFun = xn.Attributes["fun"].Value;
                                if (!string.IsNullOrEmpty(xaFun)) dr.fun = xaFun;
                                string xaInf = xn.Attributes["inf"].Value;
                                if (!string.IsNullOrEmpty(xaInf)) dr.inf = xaInf;
                                string xaName = xn.Attributes["name"].Value;
                                if (!string.IsNullOrEmpty(xaName)) dr.tagName = xaName;
                                if (drTagList.Where(x => x.fun == dr.fun && x.inf == dr.inf).Any())
                                {
                                    MessageBox.Show("Duplicate tag entry found with fun=" + dr.fun + " and inf=" + dr.inf, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                }
                                drTagList.Add(dr);
                            }
                            else if (xn.Name == "channel")
                            {
                                dr = new DRConfig();
                                string xaAcc = xn.Attributes["acc"].Value;
                                if (!string.IsNullOrEmpty(xaAcc)) dr.acc = xaAcc;
                                string xaName = xn.Attributes["name"].Value;
                                if (!string.IsNullOrEmpty(xaName)) dr.channelName = xaName;
                                string xaUnit = xn.Attributes["unit"].Value;
                                if (!string.IsNullOrEmpty(xaUnit)) dr.unit = xaUnit;
                                string xaVori = xn.Attributes["vori"].Value;
                                if (!string.IsNullOrEmpty(xaVori)) dr.vori = xaVori;
                                string xaPhase = xn.Attributes["phase"].Value;
                                if (!string.IsNullOrEmpty(xaPhase)) dr.phase = xaPhase;
                                string xaPors = xn.Attributes["pors"].Value;
                                if (!string.IsNullOrEmpty(xaPors)) dr.pors = xaPors;
                                string xaMin = xn.Attributes["min"].Value;
                                if (!string.IsNullOrEmpty(xaMin)) dr.min = xaMin;
                                string xaMax = xn.Attributes["max"].Value;
                                if (!string.IsNullOrEmpty(xaMax)) dr.max = xaMax;
                                if (drChannelList.Where(x => x.acc == dr.acc).Any())
                                {
                                    MessageBox.Show("Duplicate channel entry found with acc=" + dr.acc, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                }
                                drChannelList.Add(dr);
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnSave_Click";
            try
            {
                if (!RestrictNoOFEntries()) return;
                if (string.IsNullOrEmpty(filePath))
                {
                    SaveFileDialog svfiledlg = new SaveFileDialog();
                    svfiledlg.Title = "Save DR Configuration File";
                    svfiledlg.InitialDirectory = Path.GetFullPath(Environment.SpecialFolder.MyDocuments.ToString());
                    svfiledlg.DefaultExt = "xml";
                    svfiledlg.FileName = "DRConfig";
                    svfiledlg.Filter = "xml files (*.xml)|*.xml";
                    svfiledlg.CheckFileExists = false;
                    svfiledlg.CheckPathExists = true;
                    svfiledlg.SupportMultiDottedExtensions = false;
                    if (svfiledlg.ShowDialog() == DialogResult.OK)
                    {
                        lblFilePath.Text = filePath = svfiledlg.FileName;
                    }
                    else { return; }
                }
                if (SaveXML(filePath))
                {
                    FileSaved = true;
                    MessageBox.Show("'" + filePath + "' saved successfully!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    MessageBox.Show("'" + filePath + "' saving failed!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    FileSaved = false;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool SaveXML(string filePath)
        {
            string strRoutineName = "btnSave_Click";
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlNode xmlDocNode = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmlDoc.AppendChild(xmlDocNode);

                XmlComment newComment;
                newComment = xmlDoc.CreateComment("ASHIDA OpenPro+ Configuration");

                //Add the new node to the document.
                XmlElement root = xmlDoc.DocumentElement;
                xmlDoc.InsertBefore(newComment, root);

                XmlNode xnIEDInfo = xmlDoc.CreateElement("ied_info");
                xmlDoc.AppendChild(xnIEDInfo);

                XmlElement xeDetails = xmlDoc.CreateElement("details");
                xeDetails.SetAttribute("DeviceType", txtbxDeviceType.Text);
                xeDetails.SetAttribute("DeviceDescription", txtbxDeviceDesc.Text);
                xeDetails.SetAttribute("OppConfiguratorVer", Utils.AssemblyVersion);
                string[] Vers = Utils.GetVersionsFromdrXSD();
                xeDetails.SetAttribute("DrXsdVer", Utils.GetVersionSubstring(Vers[1]));
                xnIEDInfo.AppendChild(xeDetails);

                foreach (DRConfig drtag in drTagList)
                {
                    XmlElement xeTag = xmlDoc.CreateElement("tag");
                    xeTag.SetAttribute("fun", drtag.fun);
                    xeTag.SetAttribute("inf", drtag.inf);
                    xeTag.SetAttribute("name", drtag.tagName);
                    xnIEDInfo.AppendChild(xeTag);
                }

                foreach (DRConfig drChannel in drChannelList)
                {
                    XmlElement xeChannel = xmlDoc.CreateElement("channel");
                    xeChannel.SetAttribute("acc", drChannel.acc);
                    xeChannel.SetAttribute("name", drChannel.channelName);
                    xeChannel.SetAttribute("unit", drChannel.unit);
                    xeChannel.SetAttribute("vori", drChannel.vori);
                    xeChannel.SetAttribute("phase", drChannel.phase);
                    xeChannel.SetAttribute("pors", drChannel.pors);
                    xeChannel.SetAttribute("min", drChannel.min);
                    xeChannel.SetAttribute("max", drChannel.max);
                    xnIEDInfo.AppendChild(xeChannel);
                }
       
                xmlDoc.Save(filePath);
                return true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnSave_Click";
            try
            {
                if (!RestrictNoOFEntries()) return;

                SaveFileDialog svfiledlg = new SaveFileDialog();
                svfiledlg.Title = "Save DR Configuration File";
                svfiledlg.InitialDirectory = Path.GetFullPath(Environment.SpecialFolder.MyDocuments.ToString());
                svfiledlg.DefaultExt = "xml";
                svfiledlg.FileName = "DRConfig";
                svfiledlg.Filter = "xml files (*.xml)|*.xml";
                svfiledlg.CheckFileExists = false;
                svfiledlg.CheckPathExists = true;
                svfiledlg.SupportMultiDottedExtensions = false;
                if (svfiledlg.ShowDialog() == DialogResult.OK)
                {
                    lblFilePath.Text = filePath = svfiledlg.FileName;
                }
                else { return; }

                if (SaveXML(filePath))
                {
                    FileSaved = true;
                    MessageBox.Show("'" + filePath + "' saved successfully!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    FileSaved = false;
                    MessageBox.Show("'" + filePath + "' saving failed!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckVersions(string xmlFilePath)
        {
            try
            {
                string[] drXsdVers = Utils.GetVersionsFromdrXSD();
                string[] xmlfileVers = Utils.GetVersionsFromDRXML(xmlFilePath);
                if (drXsdVers != null && xmlfileVers != null)
                {
                    if (drXsdVers.Count() > 0 && xmlfileVers.Count() > 0)
                    {
                        if (!string.IsNullOrEmpty(xmlfileVers[0]))
                        {
                            if (xmlfileVers[0] != "OppConfiguratorVer=" + Utils.AssemblyVersion)
                            {
                                MessageBox.Show("'" + Path.GetFileName(xmlFilePath) + "' file was created using OpenPro+Configurator version " + Utils.GetVersionSubstring(xmlfileVers[0]) + ", and the current OpenPro+Configurator version is " + Utils.AssemblyVersion, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                return false;
                            }
                        }
                        if (!string.IsNullOrEmpty(drXsdVers[0]))
                        {
                            if (drXsdVers[0] != "OppConfiguratorVer=" + Utils.AssemblyVersion)
                            {
                                MessageBox.Show("OpenPro+Configurator version (" + Utils.AssemblyVersion + ") is mismatched in dr_config.xsd", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                return false;
                            }
                        }
                        if (!string.IsNullOrEmpty(xmlfileVers[0]) && !string.IsNullOrEmpty(drXsdVers[0]))
                        {
                            if (xmlfileVers[0] != drXsdVers[0])
                            {
                                MessageBox.Show("OpenPro+Configurator version is mismatched in dr_config.xsd and '" + Path.GetFileName(xmlFilePath) + "'", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                return false;
                            }
                        }
                    }
                    if (drXsdVers.Count() > 1 && xmlfileVers.Count() > 1)
                    {
                        if (!string.IsNullOrEmpty(xmlfileVers[1]) && !string.IsNullOrEmpty(drXsdVers[1]))
                        {
                            if (xmlfileVers[1] != drXsdVers[1])
                            {
                                MessageBox.Show("dr_config.xsd version is mismatched in dr_config.xsd and '" + Path.GetFileName(xmlFilePath) + "'", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.Name + ": " + MethodBase.GetCurrentMethod() + ": " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }
        }
        //Ajay: 16/01/2019
        private bool RestrictNoOFEntries()
        {
            try
            {
                if (drTagList != null && drTagList.Count < MinTag)
                {
                    MessageBox.Show("Minimum " + MinTag + " tag entry is mandatory!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return false;
                }
                if (drTagList != null && drTagList.Count > MaxTag)
                {
                    MessageBox.Show("Maximum " + MaxTag + " tag entry is allowed!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return false;
                }
                if (drChannelList != null && drChannelList.Count < MinChannel)
                {
                    MessageBox.Show("Minimum " + MinChannel + " channel entry is mandatory!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return false;
                }
                if (drChannelList != null && drChannelList.Count > MaxChannel)
                {
                    MessageBox.Show("Maximum " + MaxChannel + " channel entry is allowed!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.Name + ": " + MethodBase.GetCurrentMethod().Name + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        //Ajay: 15/01/2019
        private void btnNew_Click(object sender, EventArgs e)
        {
            string strRoutineName = this.Name + ": btnNew_Click";
            try
            {
                if (!FileSaved)
                {
                    if (MessageBox.Show("Do you want to save the file?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        SaveFileDialog svfiledlg = new SaveFileDialog();
                        svfiledlg.Title = "Save DR Configuration File";
                        svfiledlg.InitialDirectory = Path.GetFullPath(Environment.SpecialFolder.MyDocuments.ToString());
                        svfiledlg.DefaultExt = "xml";
                        svfiledlg.FileName = "DRConfig";
                        svfiledlg.Filter = "xml files (*.xml)|*.xml";
                        svfiledlg.CheckFileExists = false;
                        svfiledlg.CheckPathExists = true;
                        svfiledlg.SupportMultiDottedExtensions = false;
                        if (svfiledlg.ShowDialog() == DialogResult.OK)
                        {
                            lblFilePath.Text = filePath = svfiledlg.FileName;
                            SaveXML(svfiledlg.FileName);
                        }
                        else { }
                    }
                    else { }
                }
                lblFilePath.Text = "";
                Clear();
                FileSaved = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
