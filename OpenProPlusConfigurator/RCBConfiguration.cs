using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Reflection;

namespace OpenProPlusConfigurator
{
    public class RCBConfiguration
    {
        #region Declarations
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        string DSAddress = "";
        string Address = "";
        string TriggerOption = "";
        string BufferTime = "";
        string IntegrityPeriod = "";
        string configRevision = "";
        //Namrata: 28/09/2017
        string DatasetAddress = "";
        string ied = "";
        protected string iName;
        protected string iID;
        protected MasterTypes mt;
        private string rnName = "";
        private int editIndex = -1;
        private bool isNodeComment = false;
        private string comment = "";
        private Mode mode = Mode.NONE;
        private MasterTypes masterType = MasterTypes.UNKNOWN;
        private int masterNo = -1;
        private int IEDNo = -1;
        List<RCB> rcbList = new List<RCB>();
        ucRCBList ucrcb = new ucRCBList();
        private Report report;
        private Configuration config;
        private string dynamicDS = "";
        private string newDataSet = "";
        private string dynamicDSdots = "";
        private string dataSet = "";
        private string dataSetName = "";
        private Boolean changed = false;
        private Boolean CheckListChanged = false;
        int introw = 0;
        int intcolumns = 0;
        DataTable DTRCBTriggeredItems = new DataTable();
        public string Text { get; private set; }
        #endregion Declarations
        public RCBConfiguration(MasterTypes mType, int mNo, int iNo)
        {
            string strRoutineName = "RCBConfiguration: RCBConfiguration";
            try
            {
                masterType = mType;
                masterNo = mNo;
                IEDNo = iNo;
                report = null;
                config = null;
                ucrcb.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucrcb.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucrcb.lvAIlistDoubleClick += new System.EventHandler(this.lvAIlist_DoubleClick);
                ucrcb.ucRCBListLoad += new System.EventHandler(this.ucRCBList_Load);
                ucrcb.comboBox1KeyUp += new System.Windows.Forms.KeyEventHandler(this.comboBox1_KeyUp);
                ucrcb.ChkListTriOptn.Click += new System.EventHandler(this.checkedListBox1_Click);
                addListHeaders();
                fillOptions();
                DTRCBTriggeredItems.Columns.Add("RCBAddress", typeof(string));
                DTRCBTriggeredItems.Columns.Add("TriggeredItems", typeof(string));
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void comboBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string strRoutineName = "RCBConfiguration: comboBox1_KeyUp";
            try
            {
                var combo = sender as ComboBox;
                if (combo == null)
                {
                    return;
                }
                if (e.KeyCode == Keys.Enter)
                {
                    string text = combo.Text.Trim();
                    combo.Text = text;
                    CkeckComboBox(combo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ucRCBList_Load(object sender, EventArgs e)
        {
            string strRoutineName = "RCBConfiguration: ucRCBList_Load";
            try
            {
                FillRCBList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void FillRCBList()
        {
            string strRoutineName = "RCBConfiguration: FillRCBList";
            try
            {
                List<KeyValuePair<string, string>> RcbData = Utils.getKeyValueAttributes(ucrcb.grpAO);
                List<string> tblNameList = Utils.dsRCBData.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                string RCBTable = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
                DataSet ds = Utils.DsRCB;
                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        Address = dr["Address"].ToString();
                        BufferTime = dr["BufTime"].ToString();
                        configRevision = dr["ConRev"].ToString();
                        DSAddress = dr["DSAddress"].ToString();
                        IntegrityPeriod = dr["IntgPD"].ToString();
                        TriggerOption = dr["trigOptNum"].ToString();

                        #region Set By deafult TriggerOption Values
                        //Namrata:04/04/2019
                        CkeckComboBox(ucrcb.comboBox1);
                        int trigger = 0;//Namrata:04/04/2019
                        if (CheckListChanged == false)
                        {
                            string strxyz = "";
                            for (int i = 0; i < ucrcb.ChkListTriOptn.Items.Count; i++)
                            {
                                if (ucrcb.ChkListTriOptn.GetItemChecked(i))
                                {
                                    trigger = trigger + (1 << (i + 1));
                                    strxyz += ucrcb.ChkListTriOptn.Items[i].ToString() + ",";
                                }
                            }
                            ucrcb.txtTriggerOptionBinary.Text = trigger.ToString(CultureInfo.InvariantCulture);
                            TriggerOption = ucrcb.txtTriggerOptionBinary.Text;

                        }
                        #endregion Set By deafult TriggerOption Values

                        if (Utils.dsRCBData.Tables.Count > 0)
                        {
                            if (RCBTable != null)
                            {
                                for (introw = 0; introw < Utils.dsRCBData.Tables[RCBTable].Rows.Count; introw++)
                                    for (intcolumns = 0; intcolumns < Utils.dsRCBData.Tables[RCBTable].Columns.Count; intcolumns++)
                                    {
                                        DatasetAddress = Utils.dsRCBData.Tables[RCBTable].Rows[introw].ItemArray[0].ToString();
                                        ied = Utils.dsRCBData.Tables[RCBTable].Rows[introw].ItemArray[1].ToString();
                                        if ((DatasetAddress == Address) && (ied == IEDNo.ToString()))
                                        {
                                            RCB NewRCB = new RCB("RCB", RcbData, null, MasterTypes.IEC61850Client, masterNo, IEDNo);
                                            NewRCB.Address = Address.ToString();
                                            NewRCB.BufferTime = BufferTime.ToString();
                                            NewRCB.ConfigRevision = configRevision.ToString();
                                            NewRCB.Dataset = DSAddress.ToString();
                                            NewRCB.IntegrityPeriod = IntegrityPeriod.ToString();
                                            NewRCB.TriggerOptions = TriggerOption.ToString();
                                            NewRCB.FC = "URCB"; //Ajay: 04/08/2018
                                            bool boolPresent = false;
                                            for (int i = 0; i < rcbList.Count; i++)
                                            {
                                                if (rcbList[i].Address == NewRCB.Address.ToString())
                                                {
                                                    boolPresent = true;
                                                }
                                            }
                                            if (!boolPresent)
                                            {
                                                if (NewRCB.Address != "")
                                                {
                                                    rcbList.Add(NewRCB);
                                                    refreshList();
                                                    break;
                                                }
                                            }
                                        }
                                    }
                               
                            }
                        }
                    }
                }      
                ucrcb.grpAO.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                int aa = ucrcb.lvRCBlist.Items.Count;
                ucrcb.lvRCBlist.View = View.Details;
                ucrcb.lvRCBlist.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void fillOptions()
        {
            string strRoutineName = "RCBConfiguration: fillOptions";
            try
            {
                //ucai.lvRCBlist.Items.Clear();
                ucrcb.comboBox1.Items.Clear();
                if (Utils.DsRCBDataset.Tables.Count > 0)
                {
                    ucrcb.comboBox1.DataSource = null;
                    List<string> tblNameList = Utils.DsRCBDataset.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                    string CurrentIED = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
                    List<string> DistinctDataSet = Utils.DsRCBDataset.Tables[CurrentIED].Rows.OfType<DataRow>()
                    .Where(x => !string.IsNullOrEmpty(x.Field<string>("DataSetRCB"))).Select(x => x.Field<string>("DataSetRCB")).Distinct().ToList();
                    ucrcb.comboBox1.DataSource = DistinctDataSet;
                    ucrcb.comboBox1.DisplayMember = "Dataset";
                }
                //Namrata: 10/09/2017
                for (int i = 0; i < ucrcb.ChkListTriOptn.Items.Count - 1; i++)
                    ucrcb.ChkListTriOptn.SetItemChecked(i, true);

                //Ajay: 04/08/2018
                ucrcb.cmbFC.Items.Clear();
                List<string> fcList = Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850_FC");
                if(fcList.Count > 0)
                {
                    fcList.ForEach(x => { ucrcb.cmbFC.Items.Add(x); });
                    ucrcb.cmbFC.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void addListHeaders()
        {
            string strRoutineName = "RCBConfiguration: addListHeaders";
            try
            {
                ucrcb.lvRCBlist.Columns.Add("Address", 240, HorizontalAlignment.Left);
                ucrcb.lvRCBlist.Columns.Add("BufferTime", 90, HorizontalAlignment.Left);
                ucrcb.lvRCBlist.Columns.Add("Config Revision", 90, HorizontalAlignment.Left);
                ucrcb.lvRCBlist.Columns.Add("FC", 100, HorizontalAlignment.Left); //Ajay: 04/08/2018
                ucrcb.lvRCBlist.Columns.Add("Dataset", 240, HorizontalAlignment.Left);
                ucrcb.lvRCBlist.Columns.Add("Integrity Period", 120, HorizontalAlignment.Left);
                ucrcb.lvRCBlist.Columns.Add("Trigger Options", 150, HorizontalAlignment.Left);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CkeckComboBox(ComboBox combo)
        {
            string strRoutineName = "RCBConfiguration: CkeckComboBox";
            try
            {
                newDataSet = "";
                string[] newDynamiDS;
                string[] dataSetArr;
                bool addedNew = false;
                if (!combo.Items.Contains(combo.Text)) //check if item is already in drop down, if not, add it to all
                {
                    if (config != null)
                    {
                        newDataSet = combo.Text;

                        if (newDataSet.Length != 0 && Regex.IsMatch(newDataSet, "^[a-zA-Z0-9_@/.]+$"))
                        {
                            if (newDataSet.StartsWith("@", StringComparison.CurrentCulture))
                            {
                                dynamicDS = report.IedName + report.LDName + "/" + report.LNName + "." + combo.Text;
                                dynamicDSdots = newDataSet + "." + report.IedName + "." + report.LDName + "." + report.LNName;

                                if (!config.ICustomDataSetsInUse.ContainsKey(dynamicDS))
                                {
                                    if (!config.ICustomDataSets.ContainsKey(dynamicDSdots))
                                    {
                                        if (!combo.Items.Contains(combo.Text))
                                        {
                                            combo.Items.Add(combo.Text);
                                            ucrcb.comboBox1.Text = combo.Text;
                                        }
                                        config.ITempDataSets.Add(dynamicDSdots, null);
                                        ucrcb.Text = combo.Text;
                                        changed = true;
                                        addedNew = true;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Specified dataset already exists", "Adding new dataset error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                        dynamicDS = "";
                                        dynamicDSdots = "";
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Specified dataset already exists", "Adding new dataset error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                    dynamicDS = "";
                                    dynamicDSdots = "";
                                }
                            }
                            else if (Regex.IsMatch(newDataSet, "^[a-zA-Z0-9_]+$"))
                            {
                                dynamicDS = report.IedName + report.LDName + "/" + report.LNName + "." + combo.Text;
                                dynamicDSdots = combo.Text + "." + report.IedName + "." + report.LDName + "." + report.LNName;

                                if (!config.ICustomDataSets.ContainsKey(dynamicDSdots))
                                {
                                    if (!combo.Items.Contains(dynamicDS))
                                    {
                                        combo.Items.Add(dynamicDS);
                                    }
                                    config.ITempDataSets.Add(dynamicDSdots, null);
                                    combo.Text = dynamicDS;
                                    addedNew = true;
                                }
                                else
                                {
                                    MessageBox.Show("Specified dataset already exists", "Adding new dataset error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                    dynamicDS = "";
                                    dynamicDSdots = "";
                                }
                            }
                            else if (newDataSet.Contains("/"))
                            {
                                newDynamiDS = newDataSet.Split('/', '.');
                                if (newDynamiDS.Length == 3 && (newDynamiDS[0].Equals(report.IedName + report.LDName) && newDynamiDS[1].Equals(report.LNName)))
                                {
                                    dynamicDSdots = newDynamiDS[2] + "." + report.IedName + "." + report.LDName + "." + report.LNName;
                                    if (!config.ICustomDataSets.ContainsKey(dynamicDSdots))
                                    {
                                        config.ITempDataSets.Add(dynamicDSdots, null);
                                        if (!combo.Items.Contains(newDataSet))
                                        {
                                            combo.Items.Add(newDataSet);
                                        }
                                        ucrcb.comboBox1.Text = newDataSet;
                                        changed = true;
                                        addedNew = true;
                                    }
                                }
                            }
                            if (config.IReports.Values.Contains(report))
                            {
                                dynamicDS = combo.Text;
                            }
                            if (addedNew == true)
                            {
                                ucrcb.btnDone.Enabled = true;
                            }
                            else
                            {
                                ucrcb.comboBox1.Text = "";
                            }
                        }
                        else
                        {
                            ucrcb.comboBox1.Text = "";
                            changed = true;
                        }
                        ucrcb.comboBox1.Refresh();
                    }
                }
                else
                {
                    dataSet = combo.Text;
                    if (dataSet.StartsWith("@", StringComparison.CurrentCulture))
                    {
                        newDataSet = dataSet;
                        dynamicDS = dataSet;
                        foreach (var key in config.ICustomDataSets.Keys)
                        {
                            if (key.Contains(dataSet + ".") && key.Contains(report.IedName))
                            {
                                dynamicDSdots = key;
                            }
                        }
                    }
                    else
                    {
                        dataSetArr = dataSet.Split('/', '.');
                        dataSetName = dataSetArr[2];
                    }
                    ucrcb.btnDone.Enabled = true;
                    changed = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void checkedListBox1_Click(object sender, EventArgs e)
        {
            string strRoutineName = "RCBConfiguration: checkedListBox1_Click";
            try
            {
                changed = true;
                CheckListChanged = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            string strRoutineName = "RCBConfiguration: btnDone_Click";
            try
            {
                Console.WriteLine("*** ucai btnDone_Click clicked in class!!!");
                List<KeyValuePair<string, string>> aiData = Utils.getKeyValueAttributes(ucrcb.grpAO);
                if (mode == Mode.ADD)
                {
                }
                else if (mode == Mode.EDIT)
                {
                    rcbList[editIndex].updateAttributes(aiData);
                    string oldDataSetAddress = DSAddress;
                    CkeckComboBox(ucrcb.comboBox1);
                    int trigger = 0;//Namrata:04/04/2019
                    if (CheckListChanged)
                    {
                        string strxyz = "";
                        for (int i = 0; i < ucrcb.ChkListTriOptn.Items.Count; i++)
                        {
                            if (ucrcb.ChkListTriOptn.GetItemChecked(i))
                            {
                                trigger = trigger + (1 << (i + 1));
                                strxyz += ucrcb.ChkListTriOptn.Items[i].ToString() + ",";
                            }
                        }
                        if (!DTRCBTriggeredItems.Rows.OfType<DataRow>().Where(x => !string.IsNullOrEmpty(x.Field<string>("RCBAddress"))).Select(x => x.Field<string>("RCBAddress")).ToList().Contains(rcbList[editIndex].Address))
                        {
                            DTRCBTriggeredItems.Rows.Add(rcbList[editIndex].Address, strxyz);
                        }
                        else
                        {
                            DataRow Rw = DTRCBTriggeredItems.Rows.OfType<DataRow>().Where(x => x.Field<string>("RCBAddress") == rcbList[editIndex].Address).Select(x => x).Single();
                            Rw["TriggeredItems"] = strxyz;
                        }
                        ucrcb.txtTriggerOptionBinary.Text = trigger.ToString(CultureInfo.InvariantCulture);
                        rcbList[editIndex].TriggerOptions = ucrcb.txtTriggerOptionBinary.Text;
                    }
                    rcbList[editIndex].Dataset = ucrcb.comboBox1.Text;
                    rcbList[editIndex].BufferTime = ucrcb.buffTime.Text;
                    rcbList[editIndex].ConfigRevision = ucrcb.cRev.Text;
                    rcbList[editIndex].IntegrityPeriod = ucrcb.ipd.Text;
                    rcbList[editIndex].FC = ucrcb.cmbFC.Text; //Ajay: 04/08/2018
                }
                refreshList();
                ucrcb.grpAO.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                changed = false;   //Namrata: 25/01/2018
                CheckListChanged = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadValues()
        {
            string strRoutineName = "RCBConfiguration: loadValues";
            try
            {
                RCB rcb = rcbList.ElementAt(editIndex);
                if (rcb != null)
                {
                    ucrcb.comboBox1.SelectedIndex = ucrcb.comboBox1.FindStringExact(rcb.Dataset);
                    ucrcb.ipd.Text = rcb.IntegrityPeriod;
                    ucrcb.cRev.Text = rcb.ConfigRevision;
                    ucrcb.buffTime.Text = rcb.BufferTime;
                    ucrcb.ChkListTriOptn.Text = rcb.TriggerOptions1;
                    ucrcb.txtTriggerOptionBinary.Text = rcb.TriggerOptions;
                    ucrcb.cmbFC.SelectedIndex = ucrcb.cmbFC.FindStringExact(rcb.FC); //Ajay: 04/08/2018
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void refreshList()
        {
            string strRoutineName = "RCBConfiguration: refreshList";
            try
            {
                int cnt = 0;
                //Utils.RCB.Clear();
                ucrcb.lvRCBlist.Items.Clear();
                foreach (RCB rcb in rcbList)
                {
                    string[] row = new string[8]; 
                    if (rcb.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = rcb.Address;
                        row[1] = rcb.BufferTime;
                        row[2] = rcb.ConfigRevision;
                        row[3] = rcb.FC; //Ajay: 04/08/2018
                        row[4] = rcb.Dataset;
                        row[5] = rcb.IntegrityPeriod;
                        row[6] = rcb.TriggerOptions;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucrcb.lvRCBlist.Items.Add(lvItem);

                    //Distinct Items From ListView
                    ListViewItem[] arr = ucrcb.lvRCBlist.Items.OfType<ListViewItem>().Select(x => x).Distinct().ToArray();
                    ucrcb.lvRCBlist.Items.Clear();
                    ucrcb.lvRCBlist.Items.AddRange(arr);
                    ucrcb.lvRCBlist.View = View.Details;
                    ucrcb.lblAORecords.Text = rcbList.Count.ToString();
                    //ucai.lvRCBlist.View = View.Details;
                    Utils.RCB.AddRange(rcbList);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            string strRoutineName = "RCBConfiguration: btnCancel_Click";
            try
            {
                Console.WriteLine("*** ucai btnCancel_Click clicked in class!!!");
                ucrcb.grpAO.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                Utils.resetValues(ucrcb.grpAO);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvAIlist_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "RCBConfiguration: lvAIlist_DoubleClick";
            try
            {
                if (ucrcb.lvRCBlist.SelectedItems.Count <= 0) return;
                ListViewItem lvi = ucrcb.lvRCBlist.SelectedItems[0];
                Utils.UncheckOthers(ucrcb.lvRCBlist, lvi.Index);
                if (rcbList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ucrcb.grpAO.Visible = true;
                //Namrata:13/03/2018
                ucrcb.comboBox1.DataSource = null;
                if (Utils.DsRCBDataset.Tables.Count > 0)
                {
                    ucrcb.comboBox1.DataSource = null;
                    List<string> tblNameList = Utils.DsRCBDataset.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                    string CurrentIED = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
                    List<string> DistinctDataSet = Utils.DsRCBDataset.Tables[CurrentIED].Rows.OfType<DataRow>()
                    .Where(x => !string.IsNullOrEmpty(x.Field<string>("DataSetRCB"))).Select(x => x.Field<string>("DataSetRCB")).Distinct().ToList();
                    ucrcb.comboBox1.DataSource = DistinctDataSet;
                    ucrcb.comboBox1.DisplayMember = "Dataset";
                }
                //Ajay: 04/08/2018
                ucrcb.cmbFC.Items.Clear();
                List<string> fcList = Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850_FC");
                if (fcList.Count > 0)
                {
                    fcList.ForEach(x => { ucrcb.cmbFC.Items.Add(x); });
                    ucrcb.cmbFC.SelectedIndex = 0;
                }
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(ucrcb.grpAO, true);
                loadValues();
                List<String> CheckedItemsList = new List<string>();
                if (DTRCBTriggeredItems.Rows.Count > 0)
                {
                    DTRCBTriggeredItems.Rows.OfType<DataRow>().ToList().ForEach(Rw =>
                    {
                        if (Rw[0].ToString() == rcbList[editIndex].Address)
                        {
                            CheckedItemsList = Rw[1].ToString().Split(',').ToList();
                        }
                    });
                    for (int i = 0; i < ucrcb.ChkListTriOptn.Items.Count; i++)
                    {
                        if (CheckedItemsList.Contains(ucrcb.ChkListTriOptn.Items[i].ToString()))
                        {
                            ucrcb.ChkListTriOptn.SetItemChecked(i, true);
                        }
                        else
                        {
                            ucrcb.ChkListTriOptn.SetItemChecked(i, false);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      
        public XmlNode exportXMLnode()
        {
            FillRCBList();
            string strRoutineName = "RCBConfiguration: exportXMLnode";
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                StringWriter stringWriter = new StringWriter();
                XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
                XmlNode rootNode = null;
                if (isNodeComment)
                {
                    rootNode = xmlDoc.CreateComment(comment);
                    xmlDoc.AppendChild(rootNode);
                    return rootNode;
                }
                rootNode = xmlDoc.CreateElement(rnName);
                xmlDoc.AppendChild(rootNode);
                var myDistinctList = rcbList.GroupBy(i => i.Address).Select(g => g.First()).ToList();
                foreach (RCB ai in myDistinctList)
                {
                    XmlNode importNode = rootNode.OwnerDocument.ImportNode(ai.exportXMLnode(), true);
                    rootNode.AppendChild(importNode);
                }
                return rootNode;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public Control getView(List<string> kpArr)
        {
            string strRoutineName = "RCBConfiguration: getView";
            try
            {
                if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("RCB_"))
                {
                    return ucrcb;
                }
                return null;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public void parseAICNode(XmlNode aicNode, bool imported)
        {
            string strRoutineName = "RCBConfiguration: parseAICNode";
            try
            {
                if (aicNode == null)
                {
                    rnName = "ControlBlock";
                    return;
                }
                //First set root node name...
                rnName = aicNode.Name;
                if (aicNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = aicNode.Value;
                }
                foreach (XmlNode node in aicNode)
                {
                    if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                    rcbList.Add(new RCB(node, masterType, masterNo, IEDNo, imported));
                }
                refreshList();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


