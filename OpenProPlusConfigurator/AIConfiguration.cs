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
using System.Reflection;
using System.ComponentModel;
using System.Net;
using System.Text.RegularExpressions;
using System.Collections;
using System.Drawing.Imaging;
using System.IO.Compression;
using OpenProPlusConfigurator.Properties;
using System.Globalization;
using System.Security.AccessControl;
using System.Security.Principal;

namespace OpenProPlusConfigurator
{
    public class AIConfiguration
    {
        #region Declarations
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        DataSet dsdummy = null; //Ajay: 17/01/2019
        protected string iName;
        protected string iID;
        protected MasterTypes mt;
        private string rnName = "";
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        private Mode mapMode = Mode.NONE;
        private int mapEditIndex = -1;
        private bool isNodeComment = false;
        private string comment = "";
        private string currentSlave = "";
        Dictionary<string, List<AIMap>> slavesAIMapList = new Dictionary<string, List<AIMap>>();
        private MasterTypes masterType = MasterTypes.UNKNOWN;
        private int masterNo = -1;
        private int IEDNo = -1;
        List<AI> aiList = new List<AI>();
        ucAIlist ucai = new ucAIlist();
        private const int COL_CMD_TYPE_WIDTH = 130;
        Configuration con = new Configuration();
        //Namrata: 11/09/2017
        public DataGridView dataGridViewDataSet = new DataGridView();
        public DataTable dtdataset = new DataTable();
        DataRow datasetRow;
        private string Response = "";
        private string ied = "";
        public DataGridView DataGridViewFetchData = new DataGridView();//Namrata: 11/09/2017
        public DataTable StoreData = new DataTable();
        DataSet ds = new DataSet();
        private string aimapDatatype = "";
        private string AICDatatype = "";
        List<AIMap> slaveAIMapList;
        public string AIDescription = "";
        public DataTable DtIEC61850Index = new DataTable();
        DataTable dt = new DataTable();
        List<string> MergeList = null;//Namrata:25/03/2019
        List<string> ObjectRef = null;//Namrata:25/03/2019
        //Namrata: 12/08/2019
        //Get CellNo from Dataset
        List<string> CellNo = null;

        //Get Widget from Dataset
        List<string> Widget = null;
        List<string> ObjectRefForMap = null;//Namrata:12/04/2019
        List<string> NodeForMap = null;//Namrata:12/04/2019
        List<string> MergeListMap = null;//Namrata:25/03/2019
        int intMapCheckItems = 0;
        List<string> FC = null;//Namrata:25/03/2019
        private RCBConfiguration RCBNode = null;
        List<string> IEC61850CheckedList = null;
        List<string> IEC61850MapCheckedList = null;
        private bool IsAddEdit = false;
        int intCheckItems = 0;
        #endregion Declarations
        public AIConfiguration(MasterTypes mType, int mNo, int iNo)
        {
            string strRoutineName = "AIConfiguration";
            try
            {
                ucai.splitContainer2.Panel2Collapsed = true;
                masterType = mType;
                masterNo = mNo;
                IEDNo = iNo;

                #region ListView Events
                ucai.ucAIlistLoad += new System.EventHandler(this.ucAIlist_Load);
                ucai.lvAIlistDoubleClick += new System.EventHandler(this.lvAIlist_DoubleClick);
                ucai.lvAIlistItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvAIlist_ItemCheck);
                ucai.lvAIMapItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvAIMap_ItemSelectionChanged);
                ucai.lvAIlistItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvAIlist_ItemSelectionChanged);
                ucai.lvAIMapDoubleClick += new System.EventHandler(this.lvAIMap_DoubleClick);
                #endregion ListView Events

                #region Btn Events
                ucai.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                ucai.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                ucai.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucai.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucai.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                ucai.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                ucai.btnNextClick += new System.EventHandler(this.btnNext_Click);
                ucai.btnLastClick += new System.EventHandler(this.btnLast_Click);
                ucai.btnAIMDeleteClick += new System.EventHandler(this.btnAIMDelete_Click);
                ucai.btnAIMDoneClick += new System.EventHandler(this.btnAIMDone_Click);
                ucai.btnAIMCancelClick += new System.EventHandler(this.btnAIMCancel_Click);
                ucai.btnVerifyClick += new EventHandler(this.btnVerify_Click); //Ajay: 31/07/2018
                #endregion Btn Events

                #region LinkBtn Events
                ucai.lnkbtnDeleteAllMap.Click += new System.EventHandler(this.linkLabel1_Click);
                ucai.lnkbtnDeleteAll.Click += new System.EventHandler(this.lnkbtnDeleteAll_Click);
                #endregion LinkBtn Events

                #region Combobox Events
                ucai.cmbIEDName.SelectedIndexChanged += new System.EventHandler(this.cmbIEDName_SelectedIndexChanged);
                ucai.cmb61850ResponseType.SelectedIndexChanged += new System.EventHandler(this.cmb61850ResponseType_SelectedIndexChanged);
                ucai.cmb61850Index.SelectedIndexChanged += new System.EventHandler(this.cmb61850Index_SelectedIndexChanged);
                ucai.cmbAIMDataTypeSelectedIndexChanged += new System.EventHandler(this.cmbAIMDataType_SelectedIndexChanged);
                ucai.cmbDataTypeSelectedIndexChanged += new System.EventHandler(this.cmbDataType_SelectedIndexChanged);
                ucai.cmbFCSelectedIndexChanged += new System.EventHandler(this.cmbFC_SelectedIndexChanged); //Ajay: 17/01/2019
                ucai.CmbCellNoSelectedIndexChanged += new System.EventHandler(this.CmbCellNo_SelectedIndexChanged); //Ajay: 17/01/2019
                #endregion Combobox Events

                #region CheckCombobox Events
                ucai.ChkIEC61850Index.SelectedIndexChanged += new System.EventHandler(this.ChkIEC61850Index_SelectedIndexChanged);//Namrata:22/03/2019
                #endregion CheckCombobox Events
                ucai.TblLayoutCSLD.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TblLayoutCSLD_MouseMove);
                ucai.TblLayoutCSLD.CellPaint += new System.Windows.Forms.TableLayoutCellPaintEventHandler(this.TblLayoutCSLD_CellPaint);
                ucai.txtBxSearch.TextChanged += new System.EventHandler(this.txtBxSearch_TextChanged);

                #region Enable/Disable Events
                if (mType == MasterTypes.Virtual)
                {
                    Virtual_MasterOnLoadEvents();
                }
                else if (mType == MasterTypes.IEC61850Client)
                {
                    IEC61850Client_MasterOnLoadEvents();
                }
                else if (mType == MasterTypes.IEC103 || mType == MasterTypes.ADR || mType == MasterTypes.MODBUS) //Ajay: 30/08/2018
                {
                    IEC103_ADR_MODBUS_MasterOnLoadEvents();
                }
                else if (masterType == MasterTypes.IEC101)//Ajay: 12/11/2018
                {
                    IEC101_MasterOnLoadEvents();
                }
                else if (masterType == MasterTypes.IEC104)//Ajay: 30/08/2018
                {
                    IEC104_MasterOnLoadEvents();
                }
                else if (masterType == MasterTypes.SPORT)
                {
                    SPORT_MasterOnLoadEvents();
                }
                else if (masterType == MasterTypes.LoadProfile)//Ajay: 31/07/2018
                {
                    LoadProfile_MasterOnLoad();
                }
                #endregion Enable/Disable Events

                addListHeaders();
                fillOptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        Color RemBackColor =new Color();
       
        private void txtBxSearch_TextChanged(object sender, EventArgs e)
       {
            string strRoutineName = "AIConfiguration:txtBxSearch_TextChanged";
            try
            {
                Color GreenColour = Color.FromArgb(34, 217, 0);
                int ListIndex = 0;// ucai.lvAIlist.FocusedItem.Index;
                int txtSearch = 0;
                if (ucai.txtBxSearch.Text == "")
                {
                    return;
                }
                if (ucai.txtBxSearch.Text != "")
                {
                    txtSearch = Convert.ToInt32(ucai.txtBxSearch.Text);
                }
                if ((ucai.lvAIlist.Items.Count == 0))
                {
                    MessageBox.Show("Please Add AI", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    foreach (ListViewItem item in ucai.lvAIlist.Items)
                    {
                        ListViewItem item1 = ucai.lvAIlist.FindItemWithText(ucai.txtBxSearch.Text);
                        if (item1== null)
                        {
                            item.Selected = false;
                            item.Focused = false;
                            ucai.lvAIlist.SelectedItems.Clear();
                            ucai.lvAIlist.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                            ucai.lvAIlist.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                            
                            //item.BackColor = RemBackColor;
                            item.ForeColor = Color.Black;
                            MessageBox.Show("AINo Does Not Exist.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            int ListItemtext = Convert.ToInt32(item.Text);
                            if (ListItemtext == txtSearch)
                            {
                                RemBackColor = item.BackColor;
                                ListIndex = item.Index;
                                item.Selected = true;
                                item.Focused = true;
                                item.BackColor = GreenColour;
                                item.ForeColor = Color.Black;
                                ucai.lvAIlist.EnsureVisible(ListIndex);  // Works fine
                            }
                            else
                            {
                                item.Selected = false;
                                //item.BackColor = Color.White;
                                item.ForeColor = Color.Black;
                            }
                        }
                    }
                    //When the selection is narrowed to one the user can stop typing
                    if (ucai.lvAIlist.SelectedItems.Count == 1)
                    {
                        ucai.lvAIlist.Focus();
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int index_search = 0;

        private void TblLayoutCSLD_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {


        }
        private void TblLayoutCSLD_MouseMove(object sender, MouseEventArgs e)
        {



        }
        //Ajay: 07/12/2018
        private void ucAIlist_Load(object sender, EventArgs e)
        {
            string strRoutineName = "AIConfiguration:ucAIlist_Load";
            try
            {
                ucai.button1.Visible = false;
                ucai.splitContainer2.Panel2Collapsed = true;
                //SetDoubleBuffered(ucai.TblLayoutCSLD);
                foreach (var L in Utils.VirtualPLU)
                {
                    if (L.Run == "YES")
                    {
                        ucai.btnAdd.Enabled = true;
                        ucai.btnDelete.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//Namrata: 26/10/2017
        public void btnAdd_Click(object sender, EventArgs e)
        {
            string strRoutineName = "AIConfiguration:btnAdd_Click";
            try
            {
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted) //Ajay:07/12/2018
                {
                    if (AllowMasterOptionsOnClick(masterType)) { return; }
                    else { }
                }
                if (aiList.Count >= getMaxAIs())
                {
                    MessageBox.Show("Maximum " + getMaxAIs() + " AI's are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                mode = Mode.ADD;
                editIndex = -1;
                #region EnableDisable Events as per Masters
                if (masterType == MasterTypes.IEC61850Client)
                {
                    IEC61850Client_MasterOnLoadEvents();
                    FetchComboboxData();
                }
                else if ((masterType == MasterTypes.IEC103) || (masterType == MasterTypes.ADR) || (masterType == MasterTypes.MODBUS))
                {
                    IEC103_ADR_MODBUS_MasterOnLoadEvents();
                    //Namrata: 01/08/2019
                    ucai.txtIndex.Enabled = true;
                    ucai.txtSubIndex.Enabled = true;
                }
                else if (masterType == MasterTypes.Virtual)
                {
                    Virtual_MasterOnLoadEvents();
                }
                else if ((masterType == MasterTypes.IEC101))
                {
                    IEC101_MasterOnLoadEvents();
                }
                else if (masterType == MasterTypes.IEC104)
                {
                    IEC104_MasterOnLoadEvents();
                }
                else if (masterType == MasterTypes.SPORT)
                {
                    SPORT_MasterOnLoadEvents();
                }
                else if (masterType == MasterTypes.LoadProfile)//Ajay: 31/07/2018
                {
                    LoadProfile_MasterOnLoad(); //Ajay: 31/07/2018
                }
                #endregion EnableDisable Events as per Master

                Utils.resetValues(ucai.grpAI);
                Utils.showNavigation(ucai.grpAI, false);
                fillOptions(); //Ajay: 22/09/2018
                loadDefaults();
                ucai.grpAI.Visible = true;
                ucai.cmbResponseType.Focus();
                //Namrata: 22/02/2019
                IsAddEdit = false;
                //Namrata: 04/04/2018
                if (masterType == MasterTypes.IEC61850Client)
                {
                    //Namrata: 26/04/2018
                    if (ucai.cmbIEDName.SelectedIndex != -1)
                    {
                        //Namrata: 25/03/2019
                        ucai.ChkIEC61850Index.Text = "";
                        if (IEC61850CheckedList != null)
                        {
                            ucai.ChkIEC61850Index.CheckBoxItems.ForEach(x =>
                            {
                                x.Checked = false; x.CheckState = CheckState.Unchecked;
                            });
                        }
                        ucai.cmbIEDName.SelectedIndex = ucai.cmbIEDName.FindStringExact(Utils.Iec61850IEDname);
                    }
                    else
                    {
                        ucai.grpAI.Visible = false;
                        MessageBox.Show("ICD file missing !!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            string strRoutineName = "AIConfiguration:btnDone_Click";
            try
            {
                int iIncrement = 1;
                Utils.DummyslaveAIMapList1.Clear();
                List<KeyValuePair<string, string>> aiData = Utils.getKeyValueAttributes(ucai.grpAI);

                #region LoadProfile
                if (masterType == MasterTypes.LoadProfile)//Ajay: 31/07/2018
                {
                    string[] str = new string[2];
                    if (!IsVerified(ucai.cmbAI1, "AI1", out str))
                    {
                        string dlgMsg = str[0];
                        if (!string.IsNullOrEmpty(str[1])) { dlgMsg += "=" + str[1]; }
                        dlgMsg += " is not valid." + Environment.NewLine + "Do you want to continue?";
                        DialogResult rslt = MessageBox.Show(dlgMsg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (rslt.ToString().ToLower() == "no") { return; }
                    }
                    if (!IsVerified(ucai.cmbAI2, "AI2", out str))
                    {
                        string dlgMsg = str[0];
                        if (!string.IsNullOrEmpty(str[1])) { dlgMsg += "=" + str[1]; }
                        dlgMsg += " is not valid." + Environment.NewLine + "Do you want to continue?";
                        DialogResult rslt = MessageBox.Show(dlgMsg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (rslt.ToString().ToLower() == "no") { return; }
                    }
                    if (!IsVerified(ucai.cmbAI3, "AI3", out str))
                    {
                        string dlgMsg = str[0];
                        if (!string.IsNullOrEmpty(str[1])) { dlgMsg += "=" + str[1]; }
                        dlgMsg += " is not valid." + Environment.NewLine + "Do you want to continue?";
                        DialogResult rslt = MessageBox.Show(dlgMsg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (rslt.ToString().ToLower() == "no") { return; }
                    }
                }
                #endregion LoadProfile

                #region Fill Address to Datatable for RCBConfiguration
                if (masterType == MasterTypes.IEC61850Client)//Namrata: 27/09/2017
                {
                    IEC61850CheckedList = ucai.ChkIEC61850Index.CheckBoxItems.Where(x => x.Checked == true).Select(x => x.Text).ToList();//Namrata:25/03/2019
                    Response = ucai.cmb61850ResponseType.Text;
                    ied = IEDNo.ToString(); DataColumn dcAddressColumn;
                    if (!dtdataset.Columns.Contains("Address"))//Namrata: 15/03/2018
                    { dcAddressColumn = dtdataset.Columns.Add("Address", typeof(string)); }
                    if (!dtdataset.Columns.Contains("IED"))
                    { dtdataset.Columns.Add("IED", typeof(string)); }
                    datasetRow = dtdataset.NewRow();
                    datasetRow["Address"] = Response.ToString();
                    datasetRow["IED"] = IEDNo.ToString();
                    dtdataset.Rows.Add(datasetRow);
                    Utils.dtDataSet = dtdataset;
                    dataGridViewDataSet.DataSource = Utils.dtDataSet;//Namrata: 15/03/2018
                    Utils.dtDataSet.TableName = Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname;
                    string Index112 = "";
                    DataRow row112;
                    if (Utils.dsRCBAI.Tables.Contains(Utils.dtDataSet.TableName))
                    {
                        Utils.dsRCBAI.Tables[Utils.dtDataSet.TableName].Clear();
                    }
                    else
                    {
                        Utils.dsRCBAI.Tables.Add(Utils.dtDataSet.TableName);
                        Utils.dsRCBAI.Tables[Utils.dtDataSet.TableName].Columns.Add("ObjectReferrence");
                        Utils.dsRCBAI.Tables[Utils.dtDataSet.TableName].Columns.Add("Node");
                    }
                    for (int i = 0; i < Utils.dtDataSet.Rows.Count; i++)
                    {
                        row112 = Utils.dsRCBAI.Tables[Utils.dtDataSet.TableName].NewRow();
                        Utils.dsRCBAI.Tables[Utils.dtDataSet.TableName].NewRow();
                        for (int j = 0; j < Utils.dtDataSet.Columns.Count; j++)
                        {
                            Index112 = Utils.dtDataSet.Rows[i][j].ToString();
                            row112[j] = Index112.ToString();
                        }
                        Utils.dsRCBAI.Tables[Utils.dtDataSet.TableName].Rows.Add(row112);
                    }
                    Utils.dsRCBData = Utils.dsRCBAI;
                    Utils.dsRCBData.Merge(Utils.dsRCBAO, false, MissingSchemaAction.Add);
                    Utils.dsRCBData.Merge(Utils.dsRCBDI, false, MissingSchemaAction.Add);
                    Utils.dsRCBData.Merge(Utils.dsRCBDO, false, MissingSchemaAction.Add);
                    Utils.dsRCBData.Merge(Utils.dsRCBEN, false, MissingSchemaAction.Add);
                }
                #endregion Fill Address to Datatable for RCBConfiguration

                #region Add
                if (mode == Mode.ADD)
                {
                    //Ajay: 31/07/2018
                    int AINumber = 0, AIINdex = 0, AIINdex1 = 0;
                    int intAIIndex = 0, intAIIndex1 = 0;
                    //Ajay: 31/07/2018
                    int intRange = 0;
                    //Namrata:29/01/2019
                    int intStart = Convert.ToInt32(aiData[21].Value);
                    //Namrata: 01/08/2019
                    //Get SubIndex value from List
                    int SubIndex = Convert.ToInt32(aiData[22].Value);

                    if (IEC61850CheckedList != null)
                    {
                        intCheckItems = IEC61850CheckedList.Count();
                    }

                    #region LoadProfile
                    if (masterType == MasterTypes.LoadProfile) { intRange = 1; }
                    else { intRange = Convert.ToInt32(aiData[15].Value); } //AutoMap Range
                    if (masterType != MasterTypes.LoadProfile)
                    {
                        intAIIndex = Convert.ToInt32(aiData[23].Value);//(aiData[18].Value); // Index
                        intAIIndex1 = Convert.ToInt32(aiData[23].Value); //Index
                    }
                    #endregion LoadProfilej3

                    #region MaxAI
                    //Namrata: 23/11/2017
                    if (intRange > getMaxAIs())
                    {
                        MessageBox.Show("Maximum " + getMaxAIs() + " AI's are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    #endregion MaxAI

                    #region IEC61850Client
                    if (masterType == MasterTypes.IEC61850Client)
                    {
                        int iListCount = 0;
                        for (int i = intStart; i <= intStart + intCheckItems - 1; i++)
                        {
                            AIDescription = "";
                            if (i == Globals.AINo)
                            {
                                AINumber = Globals.AINo;
                                AIDescription = ucai.txtDescription.Text;
                            }
                            else
                            {
                                AINumber = i;
                                AIDescription = ucai.txtDescription.Text;
                            }
                            AI NewAI = new AI("AI", aiData, null, masterType, masterNo, IEDNo);
                            NewAI.AINo = AINumber.ToString();
                            NewAI.Description = AIDescription;
                            NewAI.IEDName = ucai.cmbIEDName.Text.ToString();
                            NewAI.IEC61850Index = IEC61850CheckedList[iListCount].ToString();
                            NewAI.IEC61850ResponseType = ucai.cmb61850ResponseType.Text.ToString();

                            //string FindFC = MergeList.Where(x => x.Contains(IEC61850CheckedList[iListCount].ToString() + ",")).Select(x => x).FirstOrDefault();
                            //string[] GetFC = FindFC.Split(',');
                            //ucai.cmbFC.SelectedIndex = ucai.cmbFC.FindStringExact(GetFC[1].ToString());
                            NewAI.FC = ucai.cmbFC.Text.ToString();
                            aiList.Add(NewAI);
                            iListCount++;
                            Utils.DummyslaveAIMapList1.Add(NewAI);
                            if (iIncrement == 1) { iIncrement = 2; }
                        }
                    }
                    #endregion IEC61850Client

                    //Namrata: 01/08/2019
                    #region IEC103Master
                    else if (masterType == MasterTypes.IEC103)
                    {
                        if (ucai.cmbResponseType.SelectedItem.ToString() == "ParametersForMeasurementRevision")
                        {
                            int iReset = 0;  //Ajay: 01/08/2019
                            for (int i = intStart; i <= intStart + intRange * 3 - 1; i++)
                            {
                                AIDescription = "";
                                if (i == Globals.AINo)
                                {
                                    AINumber = Globals.AINo;
                                    AIINdex = Convert.ToInt32(aiData[23].Value); //All Configuration Same Index
                                    SubIndex = Convert.ToInt32(aiData[23].Value);
                                    AIDescription = ucai.txtDescription.Text;
                                }
                                else
                                {
                                    AINumber = i;
                                    AIINdex = Convert.ToInt32(aiData[23].Value);//All Configuration Same Index
                                    SubIndex = SubIndex++;
                                    AIDescription = ucai.txtDescription.Text;
                                }
                                AI NewAI = new AI("AI", aiData, null, masterType, masterNo, IEDNo);
                                NewAI.AINo = AINumber.ToString();
                                NewAI.Index = AIINdex.ToString();
                                NewAI.Description = AIDescription;
                                if (intRange > 1)
                                {
                                    if (iReset == 3)
                                    {
                                        SubIndex++;
                                        iReset = 0;
                                    }
                                }
                                NewAI.SubIndex = SubIndex.ToString();
                                aiList.Add(NewAI);
                                iReset++;
                                Utils.DummyslaveAIMapList1.Add(NewAI);
                                if (iIncrement == 1) { iIncrement = 2; }
                            }
                        }
                        else
                        {
                            for (int i = intStart; i <= intStart + intRange - 1; i++)
                            {
                                AIDescription = "";
                                if ((i == Globals.AINo) && (i == Globals.AIIndex))
                                {
                                    AINumber = Globals.AINo;
                                    AIINdex = intAIIndex++;
                                    AIDescription = ucai.txtDescription.Text;
                                }
                                else
                                {
                                    AINumber = i;
                                    AIINdex = intAIIndex++;
                                    AIINdex1 = intAIIndex1++;
                                    AIDescription = ucai.txtDescription.Text;
                                }
                                AI NewAI = new AI("AI", aiData, null, masterType, masterNo, IEDNo);
                                NewAI.AINo = AINumber.ToString();
                                if (masterType == MasterTypes.MODBUS)
                                {
                                    NewAI.Index = AIINdex.ToString();
                                    if ((ucai.cmbDataType.Text == "UnsignedInt32_LSB_MSB") || (ucai.cmbDataType.Text == "SignedInt32_LSB_MSB") || (ucai.cmbDataType.Text == "UnsignedInt32_MSB_LSB") || (ucai.cmbDataType.Text == "SignedInt32_MSB_LSB") || (ucai.cmbDataType.Text == "Float_LSB_MSB") || (ucai.cmbDataType.Text == "Float_MSB_LSB") || (ucai.cmbDataType.Text == "UnsignedLong32_Bit_MSWord_LSWord") || (ucai.cmbDataType.Text == "UnsignedLong32_Bit_LSWord_MSWord") || (ucai.cmbDataType.Text == "SignedLong32_Bit_MSWord_LSWord") || (ucai.cmbDataType.Text == "SignedLong32_Bit_LSWord_MSWord") || (ucai.cmbDataType.Text == "Float_MSWord_LSWord") || (ucai.cmbDataType.Text == "Float_LSWord_MSWord") || (ucai.cmbDataType.Text == "UnsignedInt24_LSB_MSB") || (ucai.cmbDataType.Text == "SignedInt24_LSB_MSB") || (ucai.cmbDataType.Text == "UnsignedInt24_MSB_LSB") || (ucai.cmbDataType.Text == "SignedInt24_MSB_LSB"))
                                    {
                                        intAIIndex++; intAIIndex1++;
                                    }
                                }
                                else
                                {
                                    NewAI.Index = AIINdex.ToString();
                                }
                                NewAI.Description = AIDescription;
                                NewAI.IEDName = ucai.cmbIEDName.Text.ToString();
                                NewAI.IEC61850ResponseType = ucai.cmb61850ResponseType.Text.ToString();
                                aiList.Add(NewAI);
                                Utils.DummyslaveAIMapList1.Add(NewAI);
                                if (iIncrement == 1) { iIncrement = 2; }
                            }
                        }
                    }

                    #endregion IEC103Master

                    #region OtherMasters
                    else if ((masterType == MasterTypes.ADR) || (masterType == MasterTypes.IEC101) || (masterType == MasterTypes.IEC104) || (masterType == MasterTypes.MODBUS) || (masterType == MasterTypes.Virtual) || (masterType == MasterTypes.SPORT) || (masterType == MasterTypes.LoadProfile))
                    {
                        for (int i = intStart; i <= intStart + intRange - 1; i++)
                        {
                            AIDescription = "";
                            if ((i == Globals.AINo) && (i == Globals.AIIndex))
                            {
                                AINumber = Globals.AINo;
                                AIINdex = intAIIndex++;
                                if (masterType == MasterTypes.ADR)
                                {
                                    AIDescription = ucai.txtDescription.Text;
                                }
                                else if (masterType == MasterTypes.IEC101)
                                {
                                    AIDescription = ucai.txtDescription.Text;
                                }
                                else if (masterType == MasterTypes.IEC104)
                                {
                                    AIDescription = ucai.txtDescription.Text;
                                }
                                else if (masterType == MasterTypes.IEC103)
                                {
                                    AIDescription = ucai.txtDescription.Text;
                                }
                                else if (masterType == MasterTypes.MODBUS)
                                {
                                    AIDescription = ucai.txtDescription.Text;
                                }
                                else if (masterType == MasterTypes.Virtual)
                                {
                                    AIDescription = ucai.txtDescription.Text;
                                }
                                else if (masterType == MasterTypes.SPORT)
                                {
                                    AIDescription = ucai.txtDescription.Text;
                                }
                                //Ajay: 31/07/2018
                                else if (masterType == MasterTypes.LoadProfile)
                                {
                                    AIDescription = ucai.txtDescription.Text;
                                }
                            }
                            else
                            {
                                AINumber = i;
                                AIINdex = intAIIndex++;
                                AIINdex1 = intAIIndex1++;
                                if (masterType == MasterTypes.ADR)
                                {
                                    AIDescription = ucai.txtDescription.Text;
                                }
                                else if (masterType == MasterTypes.IEC101)
                                {
                                    AIDescription = ucai.txtDescription.Text;
                                }
                                else if (masterType == MasterTypes.IEC103)
                                {
                                    AIDescription = ucai.txtDescription.Text;
                                }
                                else if (masterType == MasterTypes.IEC104)
                                {
                                    AIDescription = ucai.txtDescription.Text;
                                }
                                else if (masterType == MasterTypes.MODBUS)
                                {
                                    AIDescription = ucai.txtDescription.Text;
                                }
                                else if (masterType == MasterTypes.Virtual)
                                {
                                    AIDescription = ucai.txtDescription.Text;
                                }
                                else if (masterType == MasterTypes.SPORT)
                                {
                                    AIDescription = ucai.txtDescription.Text;
                                }
                                //Ajay: 31/07/2018
                                else if (masterType == MasterTypes.LoadProfile)
                                {
                                    AIDescription = ucai.txtDescription.Text;
                                }
                            }

                            AI NewAI = new AI("AI", aiData, null, masterType, masterNo, IEDNo);
                            NewAI.AINo = AINumber.ToString();
                            if (masterType == MasterTypes.MODBUS)
                            {
                                NewAI.Index = AIINdex.ToString();
                                if ((ucai.cmbDataType.Text == "UnsignedInt32_LSB_MSB") || (ucai.cmbDataType.Text == "SignedInt32_LSB_MSB") || (ucai.cmbDataType.Text == "UnsignedInt32_MSB_LSB") || (ucai.cmbDataType.Text == "SignedInt32_MSB_LSB") || (ucai.cmbDataType.Text == "Float_LSB_MSB") || (ucai.cmbDataType.Text == "Float_MSB_LSB") || (ucai.cmbDataType.Text == "UnsignedLong32_Bit_MSWord_LSWord") || (ucai.cmbDataType.Text == "UnsignedLong32_Bit_LSWord_MSWord") || (ucai.cmbDataType.Text == "SignedLong32_Bit_MSWord_LSWord") || (ucai.cmbDataType.Text == "SignedLong32_Bit_LSWord_MSWord") || (ucai.cmbDataType.Text == "Float_MSWord_LSWord") || (ucai.cmbDataType.Text == "Float_LSWord_MSWord") || (ucai.cmbDataType.Text == "UnsignedInt24_LSB_MSB") || (ucai.cmbDataType.Text == "SignedInt24_LSB_MSB") || (ucai.cmbDataType.Text == "UnsignedInt24_MSB_LSB") || (ucai.cmbDataType.Text == "SignedInt24_MSB_LSB"))
                                {
                                    intAIIndex++; intAIIndex1++;
                                }
                            }
                            else
                            {
                                NewAI.Index = AIINdex.ToString();
                            }
                            NewAI.Description = AIDescription;
                            NewAI.IEDName = ucai.cmbIEDName.Text.ToString();
                            NewAI.IEC61850ResponseType = ucai.cmb61850ResponseType.Text.ToString();
                            aiList.Add(NewAI);
                            Utils.DummyslaveAIMapList1.Add(NewAI);
                            if (iIncrement == 1) { iIncrement = 2; }
                        }
                    }
                    #endregion OtherMasters
                }
                #endregion Add

                #region Edit
                else if (mode == Mode.EDIT)
                {
                    if (ucai.ChkIEC61850Index.CheckBoxItems.Count > 0)
                    {
                        if (ucai.ChkIEC61850Index.CheckBoxItems.Where(x => x.Checked == true).Count() > 1)
                        {
                            MessageBox.Show("Please select only 1 index.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            aiList[editIndex].updateAttributes(aiData);
                        }
                    }
                    else
                    {
                        aiList[editIndex].updateAttributes(aiData);
                    }

                }
                #endregion Edit

                refreshList();
                //Namrata: 15/03/2018
                if (masterType == MasterTypes.IEC61850Client)
                {
                    RCBNode = new RCBConfiguration(MasterTypes.IEC61850Client, Convert.ToInt32(Utils.MasterNumForIEC61850Client), Convert.ToInt32(Utils.UnitIDForIEC61850Client));
                    RCBNode.FillRCBList();
                }
                //Namrata: 27/7/2017 
                if (sender != null && e != null)//To Change Multiple Records at a Time.
                {
                    ucai.grpAI.Visible = false;
                    mode = Mode.NONE;
                    editIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            string strRoutineName = "AIConfiguration:btnCancel_Click";
            try
            {
                ucai.grpAI.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                Utils.resetValues(ucai.grpAI);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            string strRoutineName = "AIConfiguration:btnFirst_Click";
            try
            {
                //Namrata:27/7/2017
                if (ucai.lvAIlist.Items.Count <= 0) return;
                if (aiList.ElementAt(0).IsNodeComment) return;
                editIndex = 0;
                loadValues();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            string strRoutineName = "AIConfiguration:btnPrev_Click";
            try
            {
                //Namrata:27/7/2017
                btnDone_Click(null, null);
                if (editIndex - 1 < 0) return;
                if (aiList.ElementAt(editIndex - 1).IsNodeComment) return;
                editIndex--;
                loadValues();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            string strRoutineName = "AIConfiguration:btnNext_Click";
            try
            {
                //Namrata:27/7/2017
                btnDone_Click(null, null);
                if (editIndex + 1 >= ucai.lvAIlist.Items.Count) return;
                if (aiList.ElementAt(editIndex + 1).IsNodeComment) return;
                editIndex++;
                loadValues();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            string strRoutineName = "AIConfiguration:btnLast_Click";
            try
            {
                if (ucai.lvAIlist.Items.Count <= 0) return;
                if (aiList.ElementAt(aiList.Count - 1).IsNodeComment) return;
                editIndex = aiList.Count - 1;
                loadValues();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strRoutineName = "AIConfiguration:btnDelete_Click";
            try
            {
                // Ajay: 07/12/2018
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    if (AllowMasterOptionsOnClick(masterType)) { return; }
                    else { }
                }
                DialogResult result = MessageBox.Show(Globals.PROMPT_DELETE_ENTRY, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                {
                    return;
                }
                for (int i = ucai.lvAIlist.Items.Count - 1; i >= 0; i--)
                {
                    if (ucai.lvAIlist.Items[i].Checked)
                    {
                        if (Utils.IsExistAIinPLC(aiList.ElementAt(i).AINo))
                        {
                            DialogResult ask = MessageBox.Show("AI" + aiList.ElementAt(i).AINo + " is referred in ParameterLoadConfiguration and all the references will also be deleted." + Environment.NewLine + "Do you want to continue?", "Delete AI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (ask == DialogResult.No)
                            {
                                continue;
                            }
                            Utils.DeleteAIFromPLC(aiList.ElementAt(i).AINo);
                        }
                        deleteAIFromMaps(aiList.ElementAt(i).AINo);
                        aiList.RemoveAt(i);
                        ucai.lvAIlist.Items[i].Remove();
                    }
                }
                refreshList();
                refreshCurrentMapList();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnVerify_Click(object sender, EventArgs e)
        {
            string strRoutineName = "AIConfiguration:btnVerify_Click";
            try
            {
                string[] str = new string[2];
                if (!IsVerified(ucai.cmbAI1, "AI1", out str) || !IsVerified(ucai.cmbAI2, "AI2", out str) || !IsVerified(ucai.cmbAI3, "AI3", out str))
                {
                    string dlgMsg = str[0];
                    if (!string.IsNullOrEmpty(str[1])) { dlgMsg += "=" + str[1]; }
                    dlgMsg += " is not valid.";
                    MessageBox.Show(dlgMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("AI1, AI2 & AI3 all are valid.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex) { MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void lnkbtnDeleteAll_Click(object sender, EventArgs e)
        {
            string strRoutineName = "AIConfiguration:lnkbtnDeleteAll_Click";
            try
            {
                //Ajay: 07/12/2018
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    if (AllowMasterOptionsOnClick(masterType)) { return; }
                    else { }
                }

                foreach (ListViewItem listItem in ucai.lvAIlist.Items)
                {
                    listItem.Checked = true;
                }
                DialogResult result = MessageBox.Show("Do You Want To Delete All Records ? ", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                {
                    foreach (ListViewItem listItem in ucai.lvAIlist.Items)
                    {
                        listItem.Checked = false;
                    }
                    return;
                }
                for (int i = ucai.lvAIlist.Items.Count - 1; i >= 0; i--)
                {
                    if (Utils.IsExistAIinPLC(aiList.ElementAt(i).AINo))
                    {
                        DialogResult ask = MessageBox.Show("AI " + aiList.ElementAt(i).AINo + " is referred in ParameterLoadConfiguration and all the references will also be deleted." + Environment.NewLine + "Do you want to continue?", "Delete AI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (ask == DialogResult.No)
                        {
                            continue;
                        }
                        Utils.DeleteAIFromPLC(aiList.ElementAt(i).AINo);
                    }
                    deleteAIFromMaps(aiList.ElementAt(i).AINo);
                    aiList.RemoveAt(i);
                    ucai.lvAIlist.Items.Clear();
                }
                refreshList();
                refreshCurrentMapList();//Refresh map listview...
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbAIMDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "AIConfiguration:cmbAIMDataType_SelectedIndexChanged";
            try
            {
                aimapDatatype = ucai.cmbAIMDataType.Text.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "AIConfiguration:cmbAIMDataType_SelectedIndexChanged";
            try
            {
                AICDatatype = ucai.cmbDataType.Text.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Namrata: 04/04/2018
        private void cmbIEDName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "AIConfiguration:cmbIEDName_SelectedIndexChanged";
            try
            {
                //Namrata: 04/04/2018
                if (ucai.cmbIEDName.Focused == false)
                { }
                else
                {
                    Utils.Iec61850IEDname = ucai.cmbIEDName.Text;
                    List<DataTable> dtList = Utils.dsResponseType.Tables.OfType<DataTable>().Where(tbl => tbl.TableName.StartsWith(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname)).ToList();
                    if (dtList.Count == 0)
                    {
                        ucai.cmb61850ResponseType.DataSource = null;
                        ucai.cmb61850Index.DataSource = null;
                        ucai.cmb61850ResponseType.Enabled = false;
                        //Namrata:22/03/2019
                        ucai.ChkIEC61850Index.Enabled = false;
                        ucai.cmbFC.DataSource = null;
                    }
                    else
                    {
                        ucai.cmb61850ResponseType.Enabled = true;
                        ucai.ChkIEC61850Index.Enabled = true;
                        ucai.cmb61850ResponseType.DataSource = Utils.dsResponseType.Tables[Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname];//[Utils.strFrmOpenproplusTreeNode + "/" + "Undefined" + "/" + Utils.Iec61850IEDname];
                        ucai.cmb61850ResponseType.DisplayMember = "Address";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void cmb61850ResponseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "AIConfiguration:cmb61850ResponseType_SelectedIndexChanged";
            try
            {
                if (ucai.cmb61850ResponseType.Items.Count > 1)
                {
                    if ((ucai.cmb61850ResponseType.SelectedIndex != -1))
                    {
                        //Namrata: 04/04/2018
                        Utils.Iec61850IEDname = ucai.cmbIEDName.Text;
                        List<DataTable> dtList = Utils.DsAllConfigurationData.Tables.OfType<DataTable>().Where(tbl => tbl.TableName.StartsWith(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname)).ToList();
                        dsdummy = new DataSet();
                        dtList.ForEach(tbl => { DataTable dt = tbl.Copy(); dsdummy.Tables.Add(dt); });
                        ucai.cmbFC.DataSource = dsdummy.Tables[ucai.cmb61850ResponseType.SelectedIndex].AsEnumerable().Select(x => x.Field<string>("FC")).Distinct().ToList();//Ajay: 17/01/2019
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Namrata:22/03/2019
        private void ChkIEC61850Index_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "AIConfiguration:ChkIEC61850Index_SelectedIndexChanged";
            try
            {
                if (ucai.ChkIEC61850Index.Items.Count > 0)
                {
                    if (ucai.ChkIEC61850Index.SelectedIndex != -1)
                    {
                        string a = ucai.ChkIEC61850Index.Text;
                        //string FindObjRef = MergeList.Where(x => x.Contains(a.ToString() + ",")).Select(x => x).FirstOrDefault();
                        //string[] GetFC = FindObjRef.Split(',');
                        //ucai.cmbFC.SelectedIndex = ucai.cmbFC.FindStringExact(GetFC[1].ToString());
                        //Utils.IEC61850Index = GetFC[0].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void cmb61850Index_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        //Ajay: 17/01/2019
        private void cmbFC_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "AIConfiguration:cmbFC_SelectedIndexChanged";
            try
            {
                string FC = ucai.cmbFC.Text;
                DataTable DT = FilteredIndexDT(FC);
                if (DT != null)
                {
                    List<string> FC1 = DT.AsEnumerable().Select(x => x[0].ToString()).ToList();//Namrata:25/03/2019
                    ucai.ChkIEC61850Index.Items.Clear();
                    //Namrata:25/03/2019
                    ucai.ChkIEC61850Index.Text = "";//Namrata:25/03/2019
                    foreach (var kv in FC1)
                    {
                        ucai.ChkIEC61850Index.Items.Add(kv.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvAIlist_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string strRoutineName = "AIConfiguration:lvAIlist_ItemCheck";
            try
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void linkLabel1_Click(object sender, EventArgs e)
        {
            string strRoutineName = "AIConfiguration:linkLabel1_Click";
            try
            {
                // Ajay: 07/12/2018
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    if (AllowSlaveOptionsOnClick(Utils.getSlaveTypes(currentSlave))) { return; }
                    else { }
                }
                List<AIMap> slaveAIMapList;
                if (!slavesAIMapList.TryGetValue(currentSlave, out slaveAIMapList))
                {
                    MessageBox.Show("Error Deleting AI map!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                foreach (ListViewItem listItem in ucai.lvAIMap.Items)
                {
                    listItem.Checked = true;
                }
                DialogResult result = MessageBox.Show("Do You Want To Delete All Records ? ", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                {
                    foreach (ListViewItem listItem in ucai.lvAIMap.Items)
                    {
                        listItem.Checked = false;
                    }
                    return;
                }
                for (int i = ucai.lvAIMap.Items.Count - 1; i >= 0; i--)
                {
                    if (GDSlave.DsExcelData.Tables.Count > 0)
                    {
                        //Namrata: 04/02/2019
                        var results = from row in GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable()
                                      where row.Field<string>("CellNo") == slaveAIMapList[i].CellNo && row.Field<string>("Widget") == slaveAIMapList[i].Widget && row.Field<string>("Type") == "AI"
                                      select row;

                        foreach (DataRow dataRow in results)
                        {
                            dataRow.BeginEdit();
                            dataRow["Type"] = "";
                            dataRow["Name"] = "";
                            dataRow["DBIndex"] = "";
                            dataRow["Unit"] = "";
                            dataRow["Configuration"] = dataRow[0] + "," + dataRow[1] + "," + dataRow[2] + "," + dataRow[3] + "," + dataRow[4] + "," + dataRow[5];
                            dataRow.EndEdit();
                            // dataRow.Delete();
                        }
                        GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                    }
                    slaveAIMapList.RemoveAt(i);
                    ucai.lvAIMap.Items.Clear();
                }
                refreshMapList(slaveAIMapList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Namrata:25/03/2019
        public void FetchCheckboxData()
        {
            string strRoutineName = "AIConfiguration:FetchCheckboxData";
            try
            {
                List<string> tblNameList = Utils.dsIED.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                string tblName = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
                List<string> List = new List<string>();
                if (Utils.DsAllConfigurationData.Tables.Count > 0)
                {
                    List = (from r in Utils.DsAllConfigurationData.Tables[tblName + "_On Request"].AsEnumerable() select r.Field<string>("ObjectReferrence")).ToList();
                    ObjectRef = ((DataTable)Utils.DsAllConfigurationData.Tables[tblName + "_On Request"]).AsEnumerable().Select(x => x[0].ToString()).ToList();
                    FC = ((DataTable)Utils.DsAllConfigurationData.Tables[tblName + "_On Request"]).AsEnumerable().Select(x => x[2].ToString()).ToList();
                    var MergeFCObjeRefList = ObjectRef.Zip(FC, (leftTooth, rightTooth) => leftTooth + "," + rightTooth).ToList();//Merge List
                    MergeList = MergeFCObjeRefList;
                    ucai.ChkIEC61850Index.Items.Clear();
                    foreach (var kv in ObjectRef)
                    {
                        ucai.ChkIEC61850Index.Items.Add(kv.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FetchComboboxData()
        {
            string strRoutineName = "AIConfiguration:FetchComboboxData";
            try
            {
                //Namrata: 18/03/2018
                ucai.cmbIEDName.DataSource = null;
                List<string> tblNameList = Utils.dsIED.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                string tblName = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
                //Namrata: 26/04/2018
                if (tblName != null)
                {
                    ucai.cmbIEDName.DataSource = Utils.dsIED.Tables[tblName];
                    ucai.cmbIEDName.DisplayMember = "IEDName";
                    //Namrata:21/03/2018
                    ucai.cmb61850ResponseType.DataSource = Utils.dsResponseType.Tables[tblName];
                    //Namrata:21/03/2018
                    ucai.cmb61850ResponseType.DisplayMember = "Address";
                    ucai.cmb61850Index.DataSource = Utils.DsAllConfigurationData.Tables[tblName + "_On Request"]; //Namrata:29/03/2018
                    ucai.cmb61850Index.DisplayMember = "ObjectReferrence";
                    ucai.cmb61850Index.ValueMember = "Node";
                    //Namrata:26/03/2019
                    FetchCheckboxData();
                }
                else { }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool IsVerified(ComboBox cmb, string param, out string[] str)
        {
            str = new string[2];
            string strRoutineName = "AIConfiguration:IsVerified";
            try
            {
                int iParam = 0;
                int.TryParse(cmb.Text, out iParam);
                if (!Utils.GetAllAIList().Contains(iParam))
                {
                    str[0] = param; str[1] = iParam.ToString(); return false;
                }
                else { return true; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        //Ajay: 17/01/2019
        private DataTable FilteredIndexDT(string FC)
        {
            string strRoutineName = "AIConfiguration:FilteredIndexDT";
            try
            {
                DataTable DT = new DataTable();
                DT.Columns.Add("ObjectReferrence");
                DT.Columns.Add("Node");
                DT.Columns.Add("FC");
                DataRow[] drRwArry = dsdummy.Tables[ucai.cmb61850ResponseType.SelectedIndex].AsEnumerable().Where(x => x.Field<string>("FC") == FC).ToArray();
                foreach (DataRow Rw in drRwArry)
                {
                    DT.ImportRow(Rw);
                }
                return DT;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        private void loadValues()
        {
            string strRoutineName = "AIConfiguration:loadValues";
            try
            {
                AI ai = aiList.ElementAt(editIndex);
                if (ai != null)
                {
                    ucai.txtAINo.Text = ai.AINo;
                    ucai.cmbResponseType.SelectedIndex = ucai.cmbResponseType.FindStringExact(ai.ResponseType);
                    ucai.txtIndex.Text = ai.Index;
                    ucai.txtSubIndex.Text = ai.SubIndex;
                    ucai.cmbDataType.SelectedIndex = ucai.cmbDataType.FindStringExact(ai.DataType);
                    ucai.txtMultiplier.Text = ai.Multiplier;
                    ucai.txtConstant.Text = ai.Constant;
                    ucai.txtDescription.Text = ai.Description;
                    ucai.cmbIEDName.SelectedIndex = ucai.cmbIEDName.FindStringExact(Utils.Iec61850IEDname);
                    ucai.cmb61850ResponseType.SelectedIndex = ucai.cmb61850ResponseType.FindStringExact(ai.IEC61850ResponseType);
                    ucai.cmbFC.SelectedIndex = ucai.cmbFC.FindStringExact(ai.FC); //Ajay: 17/01/2019
                    //Namrata:27/03/2019
                    ucai.ChkIEC61850Index.SelectedIndex = ucai.ChkIEC61850Index.FindStringExact(ai.IEC61850Index);
                    if (ai.EventEnable.ToLower() == "yes")
                    { ucai.chkbxEventEnable.Checked = true; }
                    else { ucai.chkbxEventEnable.Checked = false; }
                    //Ajay: 31/07/2018
                    ucai.cmbName.SelectedIndex = ucai.cmbName.FindStringExact(ai.Name);
                    ucai.cmbAI1.SelectedIndex = ucai.cmbAI1.FindStringExact(ai.AI1);
                    ucai.cmbAI2.SelectedIndex = ucai.cmbAI2.FindStringExact(ai.AI2);
                    ucai.cmbAI3.SelectedIndex = ucai.cmbAI3.FindStringExact(ai.AI3);
                    if (ai.LogEnable.ToLower() == "yes")
                    { ucai.chkbxLogEnable.Checked = true; }
                    else { ucai.chkbxLogEnable.Checked = false; }
                    //Namrata: 29/01/2019
                    ucai.txtDeadband.Text = ai.Deadband;
                    ucai.txtHighLimit.Text = ai.HighLimit;
                    ucai.txtLowLimit.Text = ai.LowLimit;
                    ucai.CmbDONo.SelectedIndex = ucai.CmbDONo.FindStringExact(ai.DONo);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvAIlist_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "AIConfiguration:lvAIlist_DoubleClick";
            try
            {
                // Ajay: 07/12/2018
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    if (AllowMasterOptionsOnClick(masterType)) { return; }
                    else { }
                }
                int ListIndex = ucai.lvAIlist.FocusedItem.Index;
                #region EnableDisable Events as per Masters
                if ((masterType == MasterTypes.ADR) || (masterType == MasterTypes.MODBUS))
                {
                    IEC103_ADR_MODBUS_MasterOnDoubleClickEvents();
                }
                //Namrata:01/08/2019
                if (masterType == MasterTypes.IEC103)
                {

                    IEC103_ADR_MODBUS_MasterOnDoubleClickEvents();
                    if (ucai.lvAIlist.FocusedItem.SubItems[1].Text == "ParametersForMeasurementRevision")
                    {
                        ucai.txtIndex.Enabled = false;
                        ucai.txtSubIndex.Enabled = true;
                    }
                    else
                    {
                        ucai.txtIndex.Enabled = true;
                        ucai.txtSubIndex.Enabled = true;
                    }
                }

                if ((masterType == MasterTypes.IEC101))
                {
                    IEC101_MasterOnDoubleClickEvents();
                }
                if (masterType == MasterTypes.IEC104)
                {
                    IEC104_MasterOnDoubleClickEvents();
                }
                //Namrata:29/01/2019
                if (masterType == MasterTypes.SPORT)
                {
                    SPORT_MasterOnDoubleClickEvents();
                }
                else if (masterType == MasterTypes.LoadProfile) //Ajay: 31/07/2018
                {
                    LoadProfile_OnDoubleClick();
                }
                else if (masterType == MasterTypes.IEC61850Client)
                {
                    IEC61850_MasterOnDoubleClickEvents();
                    FetchComboboxData();
                    ucai.cmbIEDName.SelectedIndex = ucai.cmbIEDName.FindStringExact(Utils.Iec61850IEDname); //Namrata: 04/04/2018
                }
                #endregion EnableDisable Events as per Masters
                ListViewItem lvi = ucai.lvAIlist.Items[ListIndex];//ucai.lvAIlist.SelectedItems[0];
                Utils.UncheckOthers(ucai.lvAIlist, lvi.Index);
                if (aiList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ucai.grpAI.Visible = true;
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                //Namrata:25/03/2019
                ucai.ChkIEC61850Index.Text = "";
                if (IEC61850CheckedList != null)
                {
                    ucai.ChkIEC61850Index.CheckBoxItems.ForEach(x =>
                    {
                        x.Checked = false; x.CheckState = CheckState.Unchecked;
                    });
                }
                if (ucai.ChkIEC61850Index.CheckBoxItems.Count > 0)
                {
                    ucai.ChkIEC61850Index.Text = aiList[lvi.Index].IEC61850Index.ToString();
                }
                Utils.showNavigation(ucai.grpAI, true);
                loadValues();
                if (ucai.ChkIEC61850Index.CheckBoxItems.Count > 0)
                {
                    //Namrata:26/03/2019
                    ucai.ChkIEC61850Index.CheckBoxItems.Where(x => x.Text == aiList[lvi.Index].IEC61850Index.ToString()).Take(1).ToList().ForEach(x =>
                    {
                        x.Checked = true; x.CheckState = CheckState.Checked;
                    });
                }
                ucai.cmbResponseType.Focus();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvAIlist_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string strRoutineName = "AIConfiguration:lvAIlist_ItemSelectionChanged";
            try
            {
                if (e.IsSelected)
                {
                    Color GreenColour = Color.FromArgb(34, 217, 0);
                    string diIndex = e.Item.Text;
                    //Namrata: 27/7/2017
                    ucai.lvAIMapItemSelectionChanged -= new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvAIMap_ItemSelectionChanged);
                    ucai.lvAIMap.SelectedItems.Clear();   //Remove selection from DIMap...
                    ucai.lvAIMap.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window); //Namrata: 07/04/2018
                    ucai.lvAIMap.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour)); //Namrata: 07/04/2018
                    Utils.highlightListviewItem(diIndex, ucai.lvAIMap);
                    //Namrata: 27/7/2017
                    ucai.lvAIMap.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);//Namrata: 07/04/2018
                    ucai.lvAIlist.SelectedItems.Clear();
                    ucai.lvAIlist.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                    ucai.lvAIlist.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                    ucai.lvAIlist.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);
                    ucai.lvAIMapItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvAIMap_ItemSelectionChanged);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvAIMap_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string strRoutineName = "AIConfiguration:lvAIMap_ItemSelectionChanged";
            try
            {
                if (e.IsSelected)
                {
                    Color GreenColour = Color.FromArgb(34, 217, 0);
                    string diIndex = e.Item.Text;
                    //Namrata: 27/7/2017
                    ucai.lvAIlistItemSelectionChanged -= new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvAIlist_ItemSelectionChanged);
                    ucai.lvAIlist.SelectedItems.Clear();   //Remove selection from DIMap...
                    ucai.lvAIlist.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window); //Namrata: 07/04/2018
                    ucai.lvAIlist.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour)); //Namrata: 07/04/2018
                    Utils.highlightListviewItem(diIndex, ucai.lvAIlist);
                    //Namrata:lvAIlist 27/7/2017
                    ucai.lvAIlist.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);//Namrata: 07/04/2018
                    ucai.lvAIMap.SelectedItems.Clear();
                    ucai.lvAIMap.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                    ucai.lvAIMap.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                    ucai.lvAIMap.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);
                    ucai.lvAIlistItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvAIlist_ItemSelectionChanged);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadDefaults()
        {
            string strRoutineName = "AIConfiguration:loadDefaults";
            try
            {
                ucai.txtIndex.Clear();
                ucai.txtAINo.Text = (Globals.AINo + 1).ToString();
                ucai.txtAIAutoMapRange.Text = "1";
                ucai.txtMultiplier.Text = "1";
                ucai.txtConstant.Text = "0";
                ucai.txtIndex.Text = "1";
                //Namrata:29/01/2019
                ucai.txtDeadband.Text = "1";
                ucai.txtHighLimit.Text = "0";
                ucai.txtLowLimit.Text = "0";
                ucai.CmbDONo.Text = "0";
                //Namrata:02/02/2019
                if (masterType == MasterTypes.SPORT)
                {
                    Utils.GetDONoForSPORTAI(masterType, masterNo.ToString());
                    if ((Utils.DONoList != null) && (Utils.DONoList.Count > 0))
                    {
                        ucai.CmbDONo.DataSource = null;
                        ucai.CmbDONo.DataSource = Utils.GetDONoForSPORTAI(masterType, masterNo.ToString());
                    }
                    else
                    {
                        ucai.CmbDONo.Items.Add(0);
                        ucai.CmbDONo.SelectedIndex = 0;
                    }
                }

                if (masterType == MasterTypes.ADR)
                {
                    if (ucai.lvAIlist.Items.Count - 1 >= 0)
                    {
                        //ucai.txtAINo.Text = Convert.ToString(Convert.ToInt32(aiList[aiList.Count - 1].AINo) + 1); //For Reindexing increment
                        ucai.txtIndex.Text = Convert.ToString(Convert.ToInt32(aiList[aiList.Count - 1].Index) + 1);
                    }
                    ucai.txtSubIndex.Text = "1";
                    ucai.cmbResponseType.SelectedIndex = ucai.cmbResponseType.FindStringExact("ADR_AI");
                    ucai.txtDescription.Text = "ADR_AI";
                }

                else if (masterType == MasterTypes.IEC101)
                {
                    if (ucai.lvAIlist.Items.Count - 1 >= 0)
                    {
                        ucai.txtIndex.Text = Convert.ToString(Convert.ToInt32(aiList[aiList.Count - 1].Index) + 1);
                    }
                    ucai.txtSubIndex.Text = "0";
                    ucai.cmbResponseType.SelectedIndex = ucai.cmbResponseType.FindStringExact("ShortFloatingPoint");
                    ucai.txtDescription.Text = "IEC101_AI";
                }
                else if (masterType == MasterTypes.SPORT)
                {
                    if (ucai.lvAIlist.Items.Count - 1 >= 0)
                    {
                        ucai.txtIndex.Text = Convert.ToString(Convert.ToInt32(aiList[aiList.Count - 1].Index) + 1);
                    }
                    ucai.txtSubIndex.Text = "0";
                    //Ajay: 12/11/2018
                    if (ucai.cmbResponseType != null && ucai.cmbResponseType.Items.Count > 0)
                    {
                        ucai.cmbResponseType.SelectedIndex = 0; // ucai.cmbResponseType.FindStringExact("ShortFloatingPoint");
                    }
                    ucai.txtDescription.Text = "SPORT_AI";
                }
                else if (masterType == MasterTypes.IEC104)
                {
                    if (ucai.lvAIlist.Items.Count - 1 >= 0)
                    {
                        ucai.txtIndex.Text = Convert.ToString(Convert.ToInt32(aiList[aiList.Count - 1].Index) + 1);
                    }
                    ucai.txtSubIndex.Text = "0";
                    ucai.cmbResponseType.SelectedIndex = ucai.cmbResponseType.FindStringExact("ShortFloatingPoint");
                    ucai.txtDescription.Text = "IEC104_AI";
                }
                else if (masterType == MasterTypes.IEC103)
                {
                    if (ucai.lvAIlist.Items.Count - 1 >= 0)
                    {
                        ucai.txtIndex.Text = Convert.ToString(Convert.ToInt32(aiList[aiList.Count - 1].Index) + 1);
                    }
                    ucai.txtSubIndex.Text = "1";
                    ucai.cmbResponseType.SelectedIndex = ucai.cmbResponseType.FindStringExact("Measurand_II");
                    ucai.txtDescription.Text = "IEC103_AI";// + (Globals.AINo + 1).ToString();
                }
                else if (masterType == MasterTypes.MODBUS)
                {
                    if (ucai.lvAIlist.Items.Count - 1 >= 0)
                    {
                        //Ajay: 11/06/2018
                        switch (aiList[aiList.Count - 1].DataType)
                        {
                            case "UnsignedInt32_LSB_MSB":
                            case "SignedInt32_LSB_MSB":
                            case "UnsignedInt32_MSB_LSB":
                            case "SignedInt32_MSB_LSB":
                            case "Float_LSB_MSB":
                            case "Float_MSB_LSB":
                            case "UnsignedLong32_Bit_MSWord_LSWord":
                            case "UnsignedLong32_Bit_LSWord_MSWord":
                            case "SignedLong32_Bit_MSWord_LSWord":
                            case "SignedLong32_Bit_LSWord_MSWord":
                            case "Float_MSWord_LSWord":
                            case "Float_LSWord_MSWord":
                            case "UnsignedInt24_LSB_MSB":
                            case "SignedInt24_LSB_MSB":
                            case "UnsignedInt24_MSB_LSB":
                            case "SignedInt24_MSB_LSB":
                                ucai.txtIndex.Text = Convert.ToString(Convert.ToInt32(aiList[aiList.Count - 1].Index) + 2);
                                break;
                            default:
                                ucai.txtIndex.Text = Convert.ToString(Convert.ToInt32(aiList[aiList.Count - 1].Index) + 1);
                                break;
                        }
                    }
                    ucai.txtSubIndex.Text = "0";
                    ucai.cmbResponseType.SelectedIndex = ucai.cmbResponseType.FindStringExact("ReadInputRegister");
                    ucai.txtDescription.Text = "MODBUS_AI";// + (Globals.AINo + 1).ToString();
                }
                else if (masterType == MasterTypes.IEC61850Client)
                {
                    if (ucai.lvAIlist.Items.Count - 1 >= 0)
                    {
                        ucai.txtIndex.Text = Convert.ToString(Convert.ToInt32(aiList[aiList.Count - 1].Index) + 1);
                    }
                    ucai.txtSubIndex.Text = "0";
                    ucai.txtDescription.Text = "IEC61850_AI";// + (Globals.AINo + 1).ToString();
                }
                else if (masterType == MasterTypes.Virtual)
                {
                    if (ucai.lvAIlist.Items.Count - 1 >= 0)
                    {
                        ucai.txtIndex.Text = Convert.ToString(Convert.ToInt32(aiList[aiList.Count - 1].Index) + 1);
                    }
                    ucai.txtSubIndex.Text = "1";
                    ucai.cmbResponseType.SelectedIndex = ucai.cmbResponseType.FindStringExact("PLU_AI");
                    ucai.txtDescription.Text = "Virtual_AI";// + (Globals.AINo + 1).ToString();
                }
                //Ajay: 31/07/2018
                else if (masterType == MasterTypes.LoadProfile)
                {
                    if (ucai.lvAIlist.Items.Count - 1 >= 0)
                    {
                        ucai.txtIndex.Text = Convert.ToString(Convert.ToInt32(aiList[aiList.Count - 1].Index) + 1);
                    }
                    if (ucai.cmbName.Items.Count > 0) { ucai.cmbName.SelectedIndex = 0; }
                    if (ucai.cmbAI1.Items.Count > 0) { ucai.cmbAI1.SelectedIndex = 0; }
                    if (ucai.cmbAI2.Items.Count > 0) { ucai.cmbAI2.SelectedIndex = 0; }
                    if (ucai.cmbAI3.Items.Count > 0) { ucai.cmbAI3.SelectedIndex = 0; }
                    ucai.txtDescription.Text = "LoadProfile_AI";
                    ucai.chkbxLogEnable.Checked = false;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void refreshList()
        {
            string strRoutineName = "AIConfiguration:refreshList";
            try
            {
                int cnt = 0;
                Utils.AIlistforDescription.Clear();
                ucai.lvAIlist.Items.Clear();

                if (ucai.lvAIlist.InvokeRequired)
                {
                    ucai.lvAIlist.Invoke(new MethodInvoker(delegate
                    {
                        #region MasterTypes.SPORT
                        if (masterType == MasterTypes.SPORT)
                        {
                            foreach (AI ai in aiList)
                            {
                                string[] row = new string[12]; //string[] row = new string[8];
                                if (ai.IsNodeComment)
                                {
                                    row[0] = "Comment...";
                                }
                                else
                                {
                                    row[0] = ai.AINo;
                                    row[1] = ai.ResponseType;
                                    row[2] = ai.Index;
                                    row[3] = ai.SubIndex;
                                    row[4] = ai.Deadband;
                                    row[5] = ai.Multiplier;
                                    row[6] = ai.Constant;
                                    row[7] = ai.EventEnable; //Ajay: 30/08/2018 EventEnable added
                                    row[8] = ai.HighLimit;
                                    row[9] = ai.LowLimit;
                                    row[10] = ai.DONo;
                                    row[11] = ai.Description;
                                }
                                ListViewItem lvItem = new ListViewItem(row);
                                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                                ucai.lvAIlist.Items.Add(lvItem);
                            }
                        }
                        #endregion MasterTypes.SPORT
                        #region IEC103,MODBUS,ADR
                        if ((masterType == MasterTypes.IEC103) || (masterType == MasterTypes.MODBUS) || (masterType == MasterTypes.ADR))  // || (masterType == MasterTypes.Virtual))
                        {
                            foreach (AI ai in aiList)
                            {
                                string[] row = new string[9]; //string[] row = new string[8];
                                if (ai.IsNodeComment)
                                {
                                    row[0] = "Comment...";
                                }
                                else
                                {
                                    row[0] = ai.AINo;
                                    row[1] = ai.ResponseType;
                                    row[2] = ai.Index;
                                    row[3] = ai.SubIndex;
                                    row[4] = ai.DataType;
                                    row[5] = ai.Multiplier;
                                    row[6] = ai.Constant;
                                    row[7] = ai.EventEnable; //Ajay: 30/08/2018 EventEnable added
                                    row[8] = ai.Description;
                                }
                                ListViewItem lvItem = new ListViewItem(row);
                                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                                ucai.lvAIlist.Items.Add(lvItem);
                            }
                        }
                        #endregion IEC103,MODBUS,ADR
                        #region Virtual
                        if (masterType == MasterTypes.Virtual)
                        {
                            foreach (AI ai in aiList)
                            {
                                string[] row = new string[8]; //string[] row = new string[8];
                                if (ai.IsNodeComment)
                                {
                                    row[0] = "Comment...";
                                }
                                else
                                {
                                    row[0] = ai.AINo;
                                    row[1] = ai.ResponseType;
                                    row[2] = ai.Index;
                                    row[3] = ai.SubIndex;
                                    row[4] = ai.DataType;
                                    row[5] = ai.Multiplier;
                                    row[6] = ai.Constant;
                                    row[7] = ai.Description;
                                }
                                ListViewItem lvItem = new ListViewItem(row);
                                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                                ucai.lvAIlist.Items.Add(lvItem);
                            }
                        }
                        #endregion Virtual
                        #region IEC101
                        if ((masterType == MasterTypes.IEC101))
                        {
                            foreach (AI ai in aiList)
                            {
                                string[] row = new string[9]; //string[] row = new string[8];
                                if (ai.IsNodeComment)
                                {
                                    row[0] = "Comment...";
                                }
                                else
                                {
                                    row[0] = ai.AINo;
                                    row[1] = ai.ResponseType;
                                    row[2] = ai.Index;
                                    row[3] = ai.SubIndex;
                                    row[4] = ai.DataType;
                                    row[5] = ai.Multiplier;
                                    row[6] = ai.Constant;
                                    row[7] = ai.EventEnable; //Ajay: 30/08/2018 EventEnable added
                                    row[8] = ai.Description;
                                }
                                ListViewItem lvItem = new ListViewItem(row);
                                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                                ucai.lvAIlist.Items.Add(lvItem);
                            }
                        }
                        #endregion IEC101
                        #region IEC104
                        if (masterType == MasterTypes.IEC104)
                        {
                            foreach (AI ai in aiList)
                            {
                                string[] row = new string[8]; //string[] row = new string[8];
                                if (ai.IsNodeComment)
                                {
                                    row[0] = "Comment...";
                                }
                                else
                                {
                                    row[0] = ai.AINo;
                                    row[1] = ai.ResponseType;
                                    row[2] = ai.Index;
                                    row[3] = ai.SubIndex;
                                    row[4] = ai.Multiplier;
                                    row[5] = ai.Constant;
                                    row[6] = ai.EventEnable; //Ajay: 30/08/2018 EventEnable added
                                    row[7] = ai.Description;
                                }
                                ListViewItem lvItem = new ListViewItem(row);
                                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                                ucai.lvAIlist.Items.Add(lvItem);
                            }
                        }
                        #endregion IEC104
                        #region IEC61850Client
                        if (masterType == MasterTypes.IEC61850Client)
                        {
                            foreach (AI ai in aiList)
                            {
                                //System.Data.DataRowView vw = ai.IEC61850Index;
                                string[] row = new string[10]; //string[] row = new string[10];
                                if (ai.IsNodeComment)
                                {
                                    row[0] = "Comment...";
                                }
                                else
                                {
                                    row[0] = ai.AINo;
                                    row[1] = ai.IEDName; //Ajay: 01/11/2018
                                    row[2] = ai.IEC61850ResponseType;
                                    row[3] = ai.IEC61850Index;
                                    row[4] = ai.FC;
                                    row[5] = ai.SubIndex;
                                    row[6] = ai.Multiplier;
                                    row[7] = ai.Constant;
                                    row[8] = ai.EventEnable; //Ajay: 30/08/2018 EventEnable added
                                                             //Namrata: 11/9/2017
                                    row[9] = ai.Description;
                                }
                                ListViewItem lvItem = new ListViewItem(row);
                                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                                ucai.lvAIlist.Items.Add(lvItem);
                            }

                        }
                        #endregion IEC61850Client
                        #region LoadProfile
                        //Ajay: 31/07/2018
                        if ((masterType == MasterTypes.LoadProfile))
                        {
                            foreach (AI ai in aiList)
                            {
                                string[] row = new string[7];
                                if (ai.IsNodeComment)
                                {
                                    row[0] = "Comment...";
                                }
                                else
                                {
                                    row[0] = ai.AINo;
                                    row[1] = ai.Name;
                                    row[2] = ai.AI1;
                                    row[3] = ai.AI2;
                                    row[4] = ai.AI3;
                                    row[5] = ai.EventEnable;
                                    //row[6] = ai.LogEnable; //Ajay: 19/09/2018 commented
                                    row[6] = ai.Description;
                                }
                                ListViewItem lvItem = new ListViewItem(row);
                                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                                ucai.lvAIlist.Items.Add(lvItem);
                            }
                        }
                        #endregion LoadProfile
                    }));
                }
                else
                {
                    #region MasterTypes.SPORT
                    if (masterType == MasterTypes.SPORT)
                    {
                        foreach (AI ai in aiList)
                        {
                            string[] row = new string[12]; //string[] row = new string[8];
                            if (ai.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = ai.AINo;
                                row[1] = ai.ResponseType;
                                row[2] = ai.Index;
                                row[3] = ai.SubIndex;
                                row[4] = ai.Deadband;
                                row[5] = ai.Multiplier;
                                row[6] = ai.Constant;
                                row[7] = ai.EventEnable; //Ajay: 30/08/2018 EventEnable added
                                row[8] = ai.HighLimit;
                                row[9] = ai.LowLimit;
                                row[10] = ai.DONo;
                                row[11] = ai.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucai.lvAIlist.Items.Add(lvItem);
                        }
                    }
                    #endregion MasterTypes.SPORT
                    #region IEC103,MODBUS,ADR
                    if ((masterType == MasterTypes.IEC103) || (masterType == MasterTypes.MODBUS) || (masterType == MasterTypes.ADR))  // || (masterType == MasterTypes.Virtual))
                    {
                        foreach (AI ai in aiList)
                        {
                            string[] row = new string[9]; //string[] row = new string[8];
                            if (ai.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = ai.AINo;
                                row[1] = ai.ResponseType;
                                row[2] = ai.Index;
                                row[3] = ai.SubIndex;
                                row[4] = ai.DataType;
                                row[5] = ai.Multiplier;
                                row[6] = ai.Constant;
                                row[7] = ai.EventEnable; //Ajay: 30/08/2018 EventEnable added
                                row[8] = ai.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucai.lvAIlist.Items.Add(lvItem);
                        }
                    }
                    #endregion IEC103,MODBUS,ADR
                    #region Virtual
                    if (masterType == MasterTypes.Virtual)
                    {
                        foreach (AI ai in aiList)
                        {
                            string[] row = new string[8]; //string[] row = new string[8];
                            if (ai.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = ai.AINo;
                                row[1] = ai.ResponseType;
                                row[2] = ai.Index;
                                row[3] = ai.SubIndex;
                                row[4] = ai.DataType;
                                row[5] = ai.Multiplier;
                                row[6] = ai.Constant;
                                row[7] = ai.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucai.lvAIlist.Items.Add(lvItem);
                        }
                    }
                    #endregion Virtual
                    #region IEC101
                    if ((masterType == MasterTypes.IEC101))
                    {
                        foreach (AI ai in aiList)
                        {
                            string[] row = new string[9]; //string[] row = new string[8];
                            if (ai.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = ai.AINo;
                                row[1] = ai.ResponseType;
                                row[2] = ai.Index;
                                row[3] = ai.SubIndex;
                                row[4] = ai.DataType;
                                row[5] = ai.Multiplier;
                                row[6] = ai.Constant;
                                row[7] = ai.EventEnable; //Ajay: 30/08/2018 EventEnable added
                                row[8] = ai.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucai.lvAIlist.Items.Add(lvItem);
                        }
                    }
                    #endregion IEC101
                    #region IEC104
                    if (masterType == MasterTypes.IEC104)
                    {
                        foreach (AI ai in aiList)
                        {
                            string[] row = new string[8]; //string[] row = new string[8];
                            if (ai.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = ai.AINo;
                                row[1] = ai.ResponseType;
                                row[2] = ai.Index;
                                row[3] = ai.SubIndex;
                                row[4] = ai.Multiplier;
                                row[5] = ai.Constant;
                                row[6] = ai.EventEnable; //Ajay: 30/08/2018 EventEnable added
                                row[7] = ai.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucai.lvAIlist.Items.Add(lvItem);
                        }
                    }
                    #endregion IEC104
                    #region IEC61850Client
                    if (masterType == MasterTypes.IEC61850Client)
                    {
                        foreach (AI ai in aiList)
                        {
                            //System.Data.DataRowView vw = ai.IEC61850Index;
                            string[] row = new string[10]; //string[] row = new string[10];
                            if (ai.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = ai.AINo;
                                row[1] = ai.IEDName; //Ajay: 01/11/2018
                                row[2] = ai.IEC61850ResponseType;
                                row[3] = ai.IEC61850Index;
                                row[4] = ai.FC;
                                row[5] = ai.SubIndex;
                                row[6] = ai.Multiplier;
                                row[7] = ai.Constant;
                                row[8] = ai.EventEnable; //Ajay: 30/08/2018 EventEnable added
                                                         //Namrata: 11/9/2017
                                row[9] = ai.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucai.lvAIlist.Items.Add(lvItem);
                        }

                    }
                    #endregion IEC61850Client
                    #region LoadProfile
                    //Ajay: 31/07/2018
                    if ((masterType == MasterTypes.LoadProfile))
                    {
                        foreach (AI ai in aiList)
                        {
                            string[] row = new string[7];
                            if (ai.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = ai.AINo;
                                row[1] = ai.Name;
                                row[2] = ai.AI1;
                                row[3] = ai.AI2;
                                row[4] = ai.AI3;
                                row[5] = ai.EventEnable;
                                //row[6] = ai.LogEnable; //Ajay: 19/09/2018 commented
                                row[6] = ai.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucai.lvAIlist.Items.Add(lvItem);
                        }
                    }
                    #endregion LoadProfile
                }

                ucai.lblAIRecords.Text = aiList.Count.ToString();
                Utils.AilistRegenerateIndex.AddRange(aiList);
                Utils.AIlistforDescription.AddRange(aiList);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool Validate()
        {
            string strRoutineName = "AIConfiguration: Validate";
            try
            {
                bool status = true;
                if (Utils.IsEmptyFields(ucai.grpAI))//Check empty field's
                {
                    MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                return status;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private int getMaxAIs()
        {
            if (masterType == MasterTypes.IEC103) return Globals.MaxIEC103AI;
            else if (masterType == MasterTypes.ADR) return Globals.MaxADRAI;
            else if (masterType == MasterTypes.IEC101) return Globals.MaxIEC101AI;
            else if (masterType == MasterTypes.MODBUS) return Globals.MaxMODBUSAI;
            else if (masterType == MasterTypes.IEC61850Client) return Globals.MaxMODBUSAI1;
            else if (masterType == MasterTypes.Virtual) return Globals.MaxPLUAI;
            else if (masterType == MasterTypes.IEC104) return Globals.MaxIEC104MasterAI;
            else if (masterType == MasterTypes.SPORT) return Globals.MaxSPORTMasterAI;
            else if (masterType == MasterTypes.LoadProfile) return Globals.MaxLoadProfileAI; //Ajay: 31/07/2018
            else return 0;
        }
        private bool AllowMasterOptionsOnClick(MasterTypes mstrType)
        {
            try
            {
                switch (mstrType)
                {
                    case MasterTypes.ADR:
                        if (ProtocolGateway.OppADRGroup_ReadOnly) { return true; }
                        else { return false; }
                    case MasterTypes.IEC101:
                        if (ProtocolGateway.OppIEC101Group_ReadOnly) { return true; }
                        else { return false; }
                    case MasterTypes.IEC103:
                        if (ProtocolGateway.OppIEC103Group_ReadOnly) { return true; }
                        else { return false; }
                    case MasterTypes.MODBUS:
                        if (ProtocolGateway.OppMODBUSGroup_ReadOnly) { return true; }
                        else { return false; }
                    case MasterTypes.IEC61850Client:
                        if (ProtocolGateway.OppIEC61850Group_ReadOnly) { return true; }
                        else { return false; }
                    case MasterTypes.IEC104:
                        if (ProtocolGateway.OppIEC104Group_ReadOnly) { return true; }
                        else { return false; }
                    case MasterTypes.SPORT:
                        if (ProtocolGateway.OppSPORTGroup_ReadOnly) { return true; }
                        else { return false; }
                    case MasterTypes.Virtual:
                        if (ProtocolGateway.OppVirtualGroup_ReadOnly) { return true; }
                        else { return false; }
                    case MasterTypes.LoadProfile:
                        if (ProtocolGateway.OppLoadProfileGroup_ReadOnly) { return true; }
                        else { return false; }
                    default:
                        return false;
                }
            }
            catch { return false; }
        }
        private bool AllowSlaveOptionsOnClick(SlaveTypes slvType)
        {
            try
            {
                switch (slvType)
                {
                    case SlaveTypes.IEC101SLAVE:
                        if (ProtocolGateway.OppIEC101SlaveGroup_ReadOnly) { return true; }
                        else { return false; }
                    case SlaveTypes.IEC104:
                        if (ProtocolGateway.OppIEC104SlaveGroup_ReadOnly) { return true; }
                        else { return false; }
                    case SlaveTypes.IEC61850Server:
                        if (ProtocolGateway.OppIEC61850SlaveGroup_ReadOnly) { return true; }
                        else { return false; }
                    case SlaveTypes.MODBUSSLAVE:
                        if (ProtocolGateway.OppMODBUSSlaveGroup_ReadOnly) { return true; }
                        else { return false; }
                    case SlaveTypes.SPORTSLAVE:
                        if (ProtocolGateway.OppSPORTSlaveGroup_ReadOnly) { return true; }
                        else { return false; }
                    //Namrata:02/04/2019
                    case SlaveTypes.MQTTSLAVE:
                        if (ProtocolGateway.OppMQTTSlaveGroup_ReadOnly) { return true; }
                        else { return false; }
                    //Namrata: 29/05/2019
                    case SlaveTypes.SMSSLAVE:
                        if (ProtocolGateway.OppSMSSlaveGroup_ReadOnly) { return true; }
                        else { return false; }
                    case SlaveTypes.GRAPHICALDISPLAYSLAVE:
                        if (ProtocolGateway.OppGraphicalDisplaySlaveGroup_ReadOnly) { return true; }
                        else { return false; }
                    case SlaveTypes.DNP3SLAVE:
                        if (ProtocolGateway.OppDNP3SlaveGroup_ReadOnly) { return true; }
                        else { return false; }
                    default:
                        return false;
                }
            }
            catch { return false; }
        } //Ajay: 07/12/2018
        /* ============================================= Below this, AI Map logic... ============================================= */
        private void DeleteSlave(string slaveID)
        {
            string strRoutineName = "AIConfiguration:DeleteSlave";
            try
            {
                Console.WriteLine("*** Deleting slave {0}", slaveID);
                slavesAIMapList.Remove(slaveID);
                RadioButton rb = null;
                foreach (Control ctrl in ucai.flpMap2Slave.Controls)
                {
                    if (ctrl.Tag.ToString() == slaveID)
                    {
                        rb = (RadioButton)ctrl;
                        break;
                    }
                }
                if (rb != null) ucai.flpMap2Slave.Controls.Remove(rb);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CreateNewSlave(string slaveNum, string slaveID, XmlNode aimNode)
        {
            string strRoutineName = "AIConfiguration:CreateNewSlave";
            try
            {
                List<AIMap> saimList = new List<AIMap>();
                slavesAIMapList.Add(slaveID, saimList);
                if (aimNode != null)
                {
                    foreach (XmlNode node in aimNode)
                    {
                        if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                        saimList.Add(new AIMap(node, Utils.getSlaveTypes(slaveID)));
                    }
                }
                AddMap2SlaveButton(Int32.Parse(slaveNum), slaveID);
                //Namrata: 24/02/2018
                #region If Description attribute not exist in XML 
                saimList.AsEnumerable().ToList().ForEach(item =>
                {
                    string strAINo = item.AINo;
                    if (string.IsNullOrEmpty(item.Description)) //Ajay: 05/01/2018
                    {
                        item.Description = Utils.AIlistforDescription.AsEnumerable().Where(x => x.AINo == strAINo).Select(x => x.Description).FirstOrDefault();
                    }
                });
                #endregion If Description attribute not exist in XML 
                GDSlave.CurrentSlave = "GraphicalDisplaySlave_" + slaveNum + "_" + GDSlave.XLSFileName; //GraphicalDisplaySlave_1_5x8 SingleBUS_1.txt
                currentSlave = slaveID;
                refreshMapList(saimList);
                
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckDNPSlaveStatusChanges()
        {
            string strRoutineName = "AIConfiguration:CheckDNPSlaveStatusChanges";
            try
            {
                //Check for slave addition...
                foreach (DNP3Settings dnp3 in Utils.getOpenProPlusHandle().getSlaveConfiguration().getDNPSlaveGroup().getDNPSlaves())//Loop thru slaves...
                {
                    string slaveID = "DNP3Slave_" + dnp3.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<AIMap>> sn in slavesAIMapList)
                    {
                        if (sn.Key == slaveID)
                        {
                            slaveAdded = false;
                            break;
                        }
                    }
                    if (slaveAdded)
                    {
                        CreateNewSlave(dnp3.SlaveNum, slaveID, null);
                    }
                }
                //Check for slave deletion...
                List<string> delSlaves = new List<string>();
                foreach (KeyValuePair<string, List<AIMap>> sain in slavesAIMapList)//Loop thru slaves...
                {
                    string slaveID = sain.Key;
                    bool slaveDeleted = true;
                    if (Utils.getSlaveTypes(slaveID) != SlaveTypes.DNP3SLAVE) continue;
                    foreach (DNP3Settings dnp3 in Utils.getOpenProPlusHandle().getSlaveConfiguration().getDNPSlaveGroup().getDNPSlaves())//Loop thru slaves...
                    {
                        if (slaveID == "DNP3Slave_" + dnp3.SlaveNum)
                        {
                            slaveDeleted = false;
                            break;
                        }
                    }
                    if (slaveDeleted)
                    {
                        delSlaves.Add(slaveID);//We cannot delete from collection now as we r looping...
                    }
                }
                foreach (string delslave in delSlaves)
                {
                    DeleteSlave(delslave);
                }
                if (delSlaves.Count > 0)//Select some new slave button n refresh list...
                {
                    if (ucai.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucai.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucai.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucai.lvAIMap.Items.Clear();
                        currentSlave = "";
                    }
                }
                fillMapOptions(Utils.getSlaveTypes(currentSlave));
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckIEC104SlaveStatusChanges()
        {
            string strRoutineName = "AIConfiguration:CheckIEC104SlaveStatusChanges";
            try
            {
                //Check for slave addition...
                foreach (IEC104Slave slv104 in Utils.getOpenProPlusHandle().getSlaveConfiguration().getIEC104Group().getIEC104Slaves())//Loop thru slaves...
                {
                    string slaveID = "IEC104_" + slv104.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<AIMap>> sn in slavesAIMapList)
                    {
                        if (sn.Key == slaveID)
                        {
                            slaveAdded = false;
                            break;
                        }
                    }
                    if (slaveAdded)
                    {
                        CreateNewSlave(slv104.SlaveNum, slaveID, null);
                    }
                }
                //Check for slave deletion...
                List<string> delSlaves = new List<string>();
                foreach (KeyValuePair<string, List<AIMap>> sain in slavesAIMapList)//Loop thru slaves...
                {
                    string slaveID = sain.Key;
                    bool slaveDeleted = true;
                    if (Utils.getSlaveTypes(slaveID) != SlaveTypes.IEC104) continue;
                    foreach (IEC104Slave slv104 in Utils.getOpenProPlusHandle().getSlaveConfiguration().getIEC104Group().getIEC104Slaves())
                    {
                        if (slaveID == "IEC104_" + slv104.SlaveNum)
                        {
                            slaveDeleted = false;
                            break;
                        }
                    }
                    if (slaveDeleted)
                    {
                        delSlaves.Add(slaveID);//We cannot delete from collection now as we r looping...
                    }
                }
                foreach (string delslave in delSlaves)
                {
                    DeleteSlave(delslave);
                }
                if (delSlaves.Count > 0)//Select some new slave button n refresh list...
                {
                    if (ucai.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucai.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucai.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucai.lvAIMap.Items.Clear();
                        currentSlave = "";
                    }
                }
                fillMapOptions(Utils.getSlaveTypes(currentSlave));
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CheckMODBUSSlaveStatusChanges()
        {
            string strRoutineName = "AIConfiguration:CheckMODBUSSlaveStatusChanges";
            try
            {
                //Check for slave addition...
                foreach (MODBUSSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().getMODBUSSlaveGroup().getMODBUSSlaves())//Loop thru slaves...
                {
                    string slaveID = "MODBUSSlave_" + slvMB.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<AIMap>> sn in slavesAIMapList)
                    {
                        if (sn.Key == slaveID)
                        {
                            slaveAdded = false;
                            break;
                        }
                    }
                    if (slaveAdded)
                    {
                        CreateNewSlave(slvMB.SlaveNum, slaveID, null);
                    }
                }
                //Check for slave deletion...
                List<string> delSlaves = new List<string>();
                foreach (KeyValuePair<string, List<AIMap>> sain in slavesAIMapList)//Loop thru slaves...
                {
                    string slaveID = sain.Key;
                    bool slaveDeleted = true;
                    if (Utils.getSlaveTypes(slaveID) != SlaveTypes.MODBUSSLAVE) continue;
                    foreach (MODBUSSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().getMODBUSSlaveGroup().getMODBUSSlaves())
                    {
                        if (slaveID == "MODBUSSlave_" + slvMB.SlaveNum)
                        {
                            slaveDeleted = false;
                            break;
                        }
                    }
                    if (slaveDeleted)
                    {
                        delSlaves.Add(slaveID);//We cannot delete from collection now as we r looping...
                    }
                }
                foreach (string delslave in delSlaves)
                {
                    DeleteSlave(delslave);
                }
                if (delSlaves.Count > 0)//Select some new slave button n refresh list...
                {
                    if (ucai.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucai.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucai.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucai.lvAIMap.Items.Clear();
                        currentSlave = "";
                    }
                }
                fillMapOptions(Utils.getSlaveTypes(currentSlave));
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CheckIEC61850laveStatusChanges()
        {
            string strRoutineName = "AIConfiguration:CheckIEC61850laveStatusChanges";
            try
            {
                //Check for slave addition...
                foreach (IEC61850ServerSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().get61850SlaveGroup().getMODBUSSlaves())//Loop thru slaves...
                {
                    string slaveID = "IEC61850Server_" + slvMB.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<AIMap>> sn in slavesAIMapList)
                    {
                        if (sn.Key == slaveID)
                        {
                            slaveAdded = false;
                            break;
                        }
                    }
                    if (slaveAdded)
                    {
                        CreateNewSlave(slvMB.SlaveNum, slaveID, null);
                    }
                }
                //Check for slave deletion...
                List<string> delSlaves = new List<string>();
                foreach (KeyValuePair<string, List<AIMap>> sain in slavesAIMapList)//Loop thru slaves...
                {
                    string slaveID = sain.Key;
                    bool slaveDeleted = true;
                    if (Utils.getSlaveTypes(slaveID) != SlaveTypes.IEC61850Server) continue;
                    foreach (IEC61850ServerSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().get61850SlaveGroup().getMODBUSSlaves())
                    {
                        if (slaveID == "IEC61850Server_" + slvMB.SlaveNum)
                        {
                            slaveDeleted = false;
                            break;
                        }
                    }
                    if (slaveDeleted)
                    {
                        delSlaves.Add(slaveID); //We cannot delete from collection now as we r looping...
                    }
                }
                foreach (string delslave in delSlaves)
                {
                    DeleteSlave(delslave);
                }
                if (delSlaves.Count > 0) //Select some new slave button n refresh list...
                {
                    if (ucai.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucai.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucai.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucai.lvAIMap.Items.Clear();
                        currentSlave = "";
                    }
                }
                fillMapOptions(Utils.getSlaveTypes(currentSlave));
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CheckIEC101SlaveStatusChanges()
        {
            string strRoutineName = "AIConfiguration:CheckIEC101SlaveStatusChanges";
            try
            {
                //Check for slave addition...
                foreach (IEC101Slave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().getIEC101SlaveGroup().getIEC101Slaves())
                {
                    string slaveID = "IEC101Slave_" + slvMB.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<AIMap>> sn in slavesAIMapList)
                    {
                        if (sn.Key == slaveID)
                        {
                            slaveAdded = false;
                            break;
                        }
                    }
                    if (slaveAdded)
                    {
                        CreateNewSlave(slvMB.SlaveNum, slaveID, null);
                    }
                }
                //Check for slave deletion...
                List<string> delSlaves = new List<string>();
                foreach (KeyValuePair<string, List<AIMap>> sain in slavesAIMapList)//Loop thru slaves...
                {
                    string slaveID = sain.Key;
                    bool slaveDeleted = true;
                    if (Utils.getSlaveTypes(slaveID) != SlaveTypes.IEC101SLAVE) continue;
                    foreach (IEC101Slave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().getIEC101SlaveGroup().getIEC101Slaves())
                    {
                        if (slaveID == "IEC101Slave_" + slvMB.SlaveNum)
                        {
                            slaveDeleted = false;
                            break;
                        }
                    }
                    if (slaveDeleted)
                    {
                        delSlaves.Add(slaveID);//We cannot delete from collection now as we r looping...
                    }
                }
                foreach (string delslave in delSlaves)
                {
                    DeleteSlave(delslave);
                }
                if (delSlaves.Count > 0)//Select some new slave button n refresh list...
                {
                    if (ucai.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucai.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucai.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucai.lvAIMap.Items.Clear();
                        currentSlave = "";
                    }
                }
                fillMapOptions(Utils.getSlaveTypes(currentSlave));
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 05/07/2018
        private void CheckSPORTSlaveStatusChanges()
        {
            string strRoutineName = "AIConfiguration:CheckSPORTSlaveStatusChanges";
            try
            {
                //Check for slave addition...
                foreach (SPORTSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().getSPORTSlaveGroup().getSPORTSlaves())
                {
                    string slaveID = "SPORTSlave_" + slvMB.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<AIMap>> sn in slavesAIMapList)
                    {
                        if (sn.Key == slaveID)
                        {
                            slaveAdded = false;
                            break;
                        }
                    }
                    if (slaveAdded)
                    {
                        CreateNewSlave(slvMB.SlaveNum, slaveID, null);
                    }
                }
                //Check for slave deletion...
                List<string> delSlaves = new List<string>();
                foreach (KeyValuePair<string, List<AIMap>> sain in slavesAIMapList)//Loop thru slaves...
                {
                    string slaveID = sain.Key;
                    bool slaveDeleted = true;
                    if (Utils.getSlaveTypes(slaveID) != SlaveTypes.SPORTSLAVE) continue;
                    foreach (SPORTSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().getSPORTSlaveGroup().getSPORTSlaves())
                    {
                        if (slaveID == "SPORTSlave_" + slvMB.SlaveNum)
                        {
                            slaveDeleted = false;
                            break;
                        }
                    }
                    if (slaveDeleted)
                    {
                        delSlaves.Add(slaveID);//We cannot delete from collection now as we r looping...
                    }
                }
                foreach (string delslave in delSlaves)
                {
                    DeleteSlave(delslave);
                }
                if (delSlaves.Count > 0)//Select some new slave button n refresh list...
                {
                    if (ucai.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucai.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucai.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucai.lvAIMap.Items.Clear();
                        currentSlave = "";
                    }
                }
                fillMapOptions(Utils.getSlaveTypes(currentSlave));
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Namrata: 10/08/2019
        private void CheckGDisplaySlaveStatusChanges()
        {
            string strRoutineName = "AIConfiguration: CheckGDisplaySlaveStatusChanges";
            try
            {
                //Check for slave addition...
                foreach (GraphicalDisplaySlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().getGraphicalDisplaySlaveGroup().getGDisplaySlaves())
                {
                    string slaveID = "GraphicalDisplaySlave_" + slvMB.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<AIMap>> sn in slavesAIMapList)
                    {
                        if (sn.Key == slaveID)
                        {
                            slaveAdded = false;
                            break;
                        }
                    }
                    if (slaveAdded)
                    {
                        CreateNewSlave(slvMB.SlaveNum, slaveID, null);
                    }
                }
                //Check for slave deletion...
                List<string> delSlaves = new List<string>();
                foreach (KeyValuePair<string, List<AIMap>> sain in slavesAIMapList)//Loop thru slaves...
                {
                    string slaveID = sain.Key;
                    bool slaveDeleted = true;
                    if (Utils.getSlaveTypes(slaveID) != SlaveTypes.GRAPHICALDISPLAYSLAVE) continue;
                    foreach (GraphicalDisplaySlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().getGraphicalDisplaySlaveGroup().getGDisplaySlaves())
                    {
                        if (slaveID == "GraphicalDisplaySlave_" + slvMB.SlaveNum)
                        {
                            slaveDeleted = false;
                            break;
                        }
                    }
                    if (slaveDeleted)
                    {
                        delSlaves.Add(slaveID);//We cannot delete from collection now as we r looping...
                    }
                }
                foreach (string delslave in delSlaves)
                {
                    DeleteSlave(delslave);
                }
                if (delSlaves.Count > 0)//Select some new slave button n refresh list...
                {
                    if (ucai.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucai.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucai.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucai.lvAIMap.Items.Clear();
                        currentSlave = "";
                    }
                }
                fillMapOptions(Utils.getSlaveTypes(currentSlave));
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Namrata:01/04/2019
        private void CheckMQTTSlaveStatusChanges()
        {
            string strRoutineName = "AIConfiguration: CheckMQTTSlaveStatusChanges";
            try
            {

                //Check for slave addition...
                foreach (MQTTSlave slvMQTT in Utils.getOpenProPlusHandle().getSlaveConfiguration().getMQTTSlaveGroup().getMQTTSlaves())
                {
                    string slaveID = "MQTTSlave_" + slvMQTT.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<AIMap>> sn in slavesAIMapList)
                    {
                        if (sn.Key == slaveID)
                        {
                            slaveAdded = false;
                            break;
                        }
                    }
                    if (slaveAdded)
                    {
                        CreateNewSlave(slvMQTT.SlaveNum, slaveID, null);
                    }
                }
                //Check for slave deletion...
                List<string> delSlaves = new List<string>();
                foreach (KeyValuePair<string, List<AIMap>> sain in slavesAIMapList)//Loop thru slaves...
                {
                    string slaveID = sain.Key;
                    bool slaveDeleted = true;
                    if (Utils.getSlaveTypes(slaveID) != SlaveTypes.MQTTSLAVE) continue;
                    foreach (MQTTSlave slvMQTT in Utils.getOpenProPlusHandle().getSlaveConfiguration().getMQTTSlaveGroup().getMQTTSlaves())
                    {
                        if (slaveID == "MQTTSlave_" + slvMQTT.SlaveNum)
                        {
                            slaveDeleted = false;
                            break;
                        }
                    }
                    if (slaveDeleted)
                    {
                        delSlaves.Add(slaveID);//We cannot delete from collection now as we r looping...
                    }
                }
                foreach (string delslave in delSlaves)
                {
                    DeleteSlave(delslave);
                }
                if (delSlaves.Count > 0)//Select some new slave button n refresh list...
                {
                    if (ucai.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucai.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucai.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucai.lvAIMap.Items.Clear();
                        currentSlave = "";
                    }
                }
                fillMapOptions(Utils.getSlaveTypes(currentSlave));
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Namrata: 30/05/2019
        private void CheckSMSSlaveStatusChanges()
        {
            string strRoutineName = "AIConfiguration: CheckSMSSlaveStatusChanges";
            try
            {

                //Check for slave addition...
                foreach (SMSSlave slvSMS in Utils.getOpenProPlusHandle().getSlaveConfiguration().getSMSSlaveGroup().getSMSSlaves())
                {
                    string slaveID = "SMSSlave_" + slvSMS.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<AIMap>> sn in slavesAIMapList)
                    {
                        if (sn.Key == slaveID)
                        {
                            slaveAdded = false;
                            break;
                        }
                    }
                    if (slaveAdded)
                    {
                        CreateNewSlave(slvSMS.SlaveNum, slaveID, null);
                    }
                }
                //Check for slave deletion...
                List<string> delSlaves = new List<string>();
                foreach (KeyValuePair<string, List<AIMap>> sain in slavesAIMapList)//Loop thru slaves...
                {
                    string slaveID = sain.Key;
                    bool slaveDeleted = true;
                    if (Utils.getSlaveTypes(slaveID) != SlaveTypes.SMSSLAVE) continue;
                    foreach (SMSSlave slvSMS in Utils.getOpenProPlusHandle().getSlaveConfiguration().getSMSSlaveGroup().getSMSSlaves())
                    {
                        if (slaveID == "SMSSlave_" + slvSMS.SlaveNum)
                        {
                            slaveDeleted = false;
                            break;
                        }
                    }
                    if (slaveDeleted)
                    {
                        delSlaves.Add(slaveID);//We cannot delete from collection now as we r looping...
                    }
                }
                foreach (string delslave in delSlaves)
                {
                    DeleteSlave(delslave);
                }
                if (delSlaves.Count > 0)//Select some new slave button n refresh list...
                {
                    if (ucai.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucai.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucai.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucai.lvAIMap.Items.Clear();
                        currentSlave = "";
                    }
                }
                fillMapOptions(Utils.getSlaveTypes(currentSlave));
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void deleteAIFromMaps(string aiNo)
        {
            string strRoutineName = "AIConfiguration:deleteAIFromMaps";
            try
            {
                foreach (KeyValuePair<string, List<AIMap>> sain in slavesAIMapList)//Loop thru slaves...
                {
                    List<AIMap> delEntry = sain.Value;
                    foreach (AIMap aimn in delEntry)
                    {
                        if (aimn.AINo == aiNo)
                        {
                            delEntry.Remove(aimn);
                            break;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AddMap2SlaveButton(int slaveNum, string slaveID)
        {
            string strRoutineName = "AIConfiguration:AddMap2SlaveButton";
            try
            {
                RadioButton rb = new RadioButton();
                rb.Name = slaveID;
                rb.Tag = slaveID;
                if (Utils.getSlaveTypes(slaveID) == SlaveTypes.IEC104)
                    rb.Text = "IEC104 " + slaveNum;
                else if (Utils.getSlaveTypes(slaveID) == SlaveTypes.MODBUSSLAVE)
                    rb.Text = "MODBUS " + slaveNum;
                //Namrata:7/7/17
                else if (Utils.getSlaveTypes(slaveID) == SlaveTypes.IEC101SLAVE)
                    rb.Text = "IEC101 " + slaveNum;
                else if (Utils.getSlaveTypes(slaveID) == SlaveTypes.IEC61850Server)
                    rb.Text = "IEC61850 " + slaveNum;
                //Ajay: 05/07/2018
                else if (Utils.getSlaveTypes(slaveID) == SlaveTypes.SPORTSLAVE)
                    rb.Text = "SPORT " + slaveNum;
                else if (Utils.getSlaveTypes(slaveID) == SlaveTypes.MQTTSLAVE)
                    rb.Text = "MQTT " + slaveNum;
                //Namrata: 30/05/2019
                else if (Utils.getSlaveTypes(slaveID) == SlaveTypes.SMSSLAVE)
                    rb.Text = "SMS " + slaveNum;
                //Namrata: 10/08/2019
                else if (Utils.getSlaveTypes(slaveID) == SlaveTypes.GRAPHICALDISPLAYSLAVE)
                    rb.Text = "GRAPHICAL DISPLAY " + slaveNum;
                //Namrata: 12/11/2019
                else if (Utils.getSlaveTypes(slaveID) == SlaveTypes.DNP3SLAVE)
                    rb.Text = "DNP3Slave " + slaveNum;

                else
                    rb.Text = "Unknown " + slaveNum;
                if (Utils.getSlaveTypes(slaveID) == SlaveTypes.IEC104)
                    rb.Text = "IEC104 " + slaveNum;
                if (Utils.getSlaveTypes(slaveID) == SlaveTypes.MODBUSSLAVE)
                    rb.Text = "MODBUS " + slaveNum;
                if (Utils.getSlaveTypes(slaveID) == SlaveTypes.IEC101SLAVE)
                    rb.Text = "IEC101 " + slaveNum;
                if (Utils.getSlaveTypes(slaveID) == SlaveTypes.IEC61850Server)
                    rb.Text = "IEC61850 " + slaveNum;
                if (Utils.getSlaveTypes(slaveID) == SlaveTypes.SPORTSLAVE)
                    rb.Text = "SPORT " + slaveNum;
                //Namrata:01/04/2019
                if (Utils.getSlaveTypes(slaveID) == SlaveTypes.MQTTSLAVE)
                    rb.Text = "MQTT " + slaveNum;
                //Namrata: 30/05/2019
                else if (Utils.getSlaveTypes(slaveID) == SlaveTypes.SMSSLAVE)
                    rb.Text = "SMS " + slaveNum;
                //Namrata: 10/08/2019
                if (Utils.getSlaveTypes(slaveID) == SlaveTypes.GRAPHICALDISPLAYSLAVE)
                    rb.Text = "GRAPHICAL DISPLAY " + slaveNum;
                //Namrata: 12/11/2019
                else if (Utils.getSlaveTypes(slaveID) == SlaveTypes.DNP3SLAVE)
                    rb.Text = "DNP3Slave " + slaveNum;

                rb.TextAlign = ContentAlignment.TopCenter;
                rb.BackColor = ColorTranslator.FromHtml("#f2f2f2");
                rb.Appearance = Appearance.Button;
                rb.AutoSize = true;
                rb.Image = Properties.Resources.SlaveRadioButton;
                rb.Click += rbGrpMap2Slave_Click;
                ucai.flpMap2Slave.Controls.Add(rb);
                rb.Checked = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void rbGrpMap2Slave_Click(object sender, EventArgs e)
        {
            string strRoutineName = "AIConfiguration:rbGrpMap2Slave_Click";
            try
            {
                ucai.grpAIMap.Visible = false; //Ajay: 07/12/2018
                bool rbChanged = false;
                RadioButton currRB = ((RadioButton)sender);
                if (currentSlave != currRB.Tag.ToString())
                {
                    currentSlave = currRB.Tag.ToString();
                    rbChanged = true;
                    refreshCurrentMapList();
                    if (ucai.lvAIlist.SelectedItems.Count > 0)
                    {
                        ucai.lvAIMap.SelectedItems.Clear();
                        Utils.highlightListviewItem(ucai.lvAIlist.SelectedItems[0].Text, ucai.lvAIMap);
                    }
                }
                ShowHideSlaveColumns();
                // ShowHideSlaveColumnsSPORT();
                // Ajay: 07/12/2018
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    if (AllowSlaveOptionsOnClick(Utils.getSlaveTypes(currentSlave))) { return; }
                    else { }
                }
                if (rbChanged && ucai.lvAIlist.CheckedItems.Count <= 0) return; //We supported map listing only
                AI mapAI = null;
                if (ucai.lvAIlist.CheckedItems.Count != 1)
                {
                    MessageBox.Show("Select Single AI Element To Map!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                for (int i = 0; i < ucai.lvAIlist.Items.Count; i++)
                {
                    if (ucai.lvAIlist.Items[i].Checked)
                    {
                        mapAI = aiList.ElementAt(i);
                        ucai.lvAIlist.Items[i].Checked = false;//Now we can uncheck in listview...
                        break;
                    }
                }
                if (mapAI == null) return;
                //Search if already mapped for current slave...
                bool alreadyMapped = false;
                List<AIMap> slaveAIMapList;
                if (!slavesAIMapList.TryGetValue(currentSlave, out slaveAIMapList))
                {
                    MessageBox.Show("Slave Entry Doesnot Exist!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else { }
                if (!alreadyMapped)
                {
                    mapMode = Mode.ADD;
                    mapEditIndex = -1;
                    Utils.resetValues(ucai.grpAIMap);
                    ucai.txtAIMNo.Text = mapAI.AINo;
                    //Namrata: 16/11/2017
                    string str = getDescription(ucai.lvAIlist, mapAI.AINo);
                    ucai.txtMapDescription.Text = str;
                    loadMapDefaults();

                    #region Graphical Display
                    if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.GRAPHICALDISPLAYSLAVE)
                    {
                        ucai.button1.Visible = true;
                        //ucai.splitContainer2.SplitterDistance = 750;
                        ucai.splitContainer2.Panel2Collapsed = false;
                        //ucai.TblLayoutCSLD.Visible = true;
                    }
                    else
                    {
                        ucai.button1.Visible = false;
                        ucai.splitContainer2.Panel2Collapsed = true;
                        ucai.TblLayoutCSLD.Visible = false;
                    }

                    #endregion Graphical Display

                    #region MQTT
                    //Namrata:01/04/2019
                    if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MQTTSLAVE)
                    {
                        MQTTSlave_OnMapLoad();
                        ucai.txtKey.Text = getMQTTSlaveKey(ucai.lvAIlist, mapAI.AINo);
                        ucai.lblEV.Visible = false;
                        ucai.CmbEV.Visible = false;
                        ucai.lblEventClass.Visible = false;
                        ucai.cmbEventC.Visible = false;
                        ucai.lblVariation.Visible = false;
                        ucai.CmbVari.Visible = false;
                        ucai.txtAutoMapM.Text = "1";
                    }
                    #endregion MQTT

                    #region SMS
                    //Namrata: 30/05/2019
                    if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.SMSSLAVE)
                    {
                        SMSSlave_OnMapLoad(); ucai.txtAutoMapM.Text = "1"; ucai.ChkIEC61850Index.Visible = false;
                    }
                    #endregion SMS

                    #region IEC61850Server
                    //Namrata: 04/11/2017
                    if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server)
                    {
                        GetIndexForIEC61850Server();
                        ucai.ChkIEC61850MapIndex.Text = "";//Namrata:25/03/2019
                        if (IEC61850MapCheckedList != null)
                        {
                            ucai.ChkIEC61850MapIndex.CheckBoxItems.ForEach(x =>
                            {
                                x.Checked = false; x.CheckState = CheckState.Unchecked;
                            });
                        }
                        IEC61850Server_OnLoad();
                    }
                    #endregion IEC61850Server

                    #region GRAPHICALDISPLAYSLAVE
                    //Namrata: 12/08/2019
                    if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.GRAPHICALDISPLAYSLAVE)
                    {
                        ucai.TblLayoutCSLD.SuspendLayout();
                        //Namrata: 05/11/2019
                        List<string> li = new List<string>();
                        for(int i=0;i<GDSlave.DsExcelData.Tables.Count;i++)
                        {
                            li.Add(GDSlave.DsExcelData.Tables[i].TableName);
                        }
                        bool matchFound = li.Any(s => s.Contains(currentSlave));
                        if (!matchFound)
                        //if (!GDSlave.DsExcelData.Tables.Contains(currentSlave + "_diagram.txt"))
                        {
                            MessageBox.Show("Please Save SLD for " + currentSlave, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            //Namrata: 16/10/2019
                            if (ucai.TblLayoutCSLD.Controls.Count != 0)
                            {
                                ucai.TblLayoutCSLD.Controls.Clear();
                            }
                            ucai.TblLayoutCSLD.Visible = false;
                            #region DisplayImage;
                            List<string> tblNameList1 = GDSlave.DsExcelData.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                            string CurrentSlave1 = tblNameList1.Where(x => x.Contains(Utils.CurrentSlaveFinal)).Select(x => x).FirstOrDefault();
                            List<DataRow> ListCurrentSlave = GDSlave.DsExcelData.Tables[CurrentSlave1].AsEnumerable().ToList();

                            DataView view = new DataView(GDSlave.DsExcelData.Tables[CurrentSlave1]);
                            DataTable distinctValues = new DataTable();
                            distinctValues = view.ToTable(true, "CellNo", "Widget");


                            int iWidget = 0; int iCellNo = 1; int iColumn = 0;
                            for (int iRow = 0; iRow < distinctValues.Rows.Count; iRow++)
                            {
                                string Widget = distinctValues.Rows[iWidget]["Widget"].ToString();
                                PictureBox pb = new PictureBox();
                                ImageConverter converter = new ImageConverter();
                                pb.Margin = new Padding(0, 0, 0, 0);
                                pb.Size = new Size(70, 70);
                                string ImagePath = Globals.Widgets_path + Widget + ".png";
                                pb.Image = Image.FromFile(ImagePath);
                                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                                pb.Name = iCellNo.ToString();
                                ucai.TblLayoutCSLD.Controls.Add(pb);
                                ucai.TblLayoutCSLD.SetColumn(pb, iColumn);
                                ucai.TblLayoutCSLD.SetColumn(pb, iRow);
                                //if (iCellNo < distinctValues.Rows.Count - 1) { }
                                //else { iCellNo++; }
                                if (iCellNo < distinctValues.Rows.Count - 1)
                                { iCellNo++; }
                                else { }
                                if (iWidget < distinctValues.Rows.Count - 1)
                                { iWidget++; }
                                else { }
                                if (iColumn == 5)
                                {
                                    iColumn = 0;
                                }
                            }

                            ucai.TblLayoutCSLD.Visible = true;
                            ucai.TblLayoutCSLD.Size = new Size(350, 560);
                            #endregion DisplayImage
                            ucai.txtUnitID.Text = "Unity";
                            GetDataForGDisplaySlave();
                            if (GDSlave.DsExcelData.Tables.Count > 0)
                            {
                                var rowColl = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable();
                                if (ucai.CmbCellNo.SelectedItem != null)
                                {
                                    string name = (from r in rowColl
                                                   where r.Field<string>("CellNo") == ucai.CmbCellNo.SelectedItem.ToString()
                                                   select r.Field<string>("Widget")).First<string>();
                                    ucai.CmbWidget.Enabled = false;
                                    ucai.CmbWidget.SelectedIndex = ucai.CmbWidget.FindStringExact(name);
                                    ucai.CmbWidget.SelectedItem = name;
                                }
                                DisplaySlave_OnLoad();
                                //Namrata: 13/12/2019
                                //Actual splitContainer2.Width-(70*7)+Tolerance
                                ucai.splitContainer2.SplitterDistance = ucai.splitContainer2.Width-490+35; // 35 - Tolerance
                                ucai.TblLayoutCSLD.ResumeLayout();
                               
                            }
                            //Namrata: 26/11/2019
                            UpdateWidgetsList(slaveAIMapList);
                            refreshMapList(slaveAIMapList);
                        }
                    }

                    #endregion GRAPHICALDISPLAYSLAVE

                    #region MODBUSSLAVE
                    if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE)
                    {
                        MODBUSSlave_OnLoad();
                        ucai.ChkIEC61850MapIndex.Visible = false;//Namrata:16/04/2019
                        ucai.txtAutoMapM.Text = "1";
                    }
                    #endregion MODBUSSLAVE

                    #region IEC101SLAVE,IEC104
                    if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC101SLAVE || Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC104)
                    {
                        IEC101Slave_IEC104Slave_OnMapLoad(); ucai.ChkIEC61850MapIndex.Visible = false;//Namrata:16/04/2019
                    }
                    #endregion IEC101SLAVE,IEC104

                    #region SPORTSLAVE
                    //Ajay:06/07/2018
                    else if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.SPORTSLAVE) //Ajay: 05/07/2018)
                    {
                        SPORTSlave_OnLoadEvents(); ucai.ChkIEC61850MapIndex.Visible = false;//Namrata:16/04/2019
                        ucai.txtAutoMapM.Text = "1";
                    }
                    #endregion SPORTSLAVE

                    #region DNP3Slave
                    if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE)
                    {
                        DNPSlave_OnLoad();
                        ucai.ChkIEC61850MapIndex.Visible = false;//Namrata:16/04/2019
                        ucai.txtAutoMapM.Text = "1";
                    }
                    #endregion DNP3Slave

                    if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server)) ucai.cmbAIMCommandType.Enabled = true;
                    else ucai.cmbAIMCommandType.Enabled = false;
                    ucai.grpAIMap.Visible = true;
                    ucai.txtAIMReportingIndex.Focus(); ucai.txtAutoMapM.Text = "1";
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Namrata: 26/11/2019
        private void UpdateWidgetsList(List<AIMap> list)
        {
            if (list != null)
            {
                list.ToList().ForEach(item =>
                {
                    DataRow Rw = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == item.CellNo).FirstOrDefault();
                    if (Rw != null)
                    {
                        item.Widget = Rw["Widget"].ToString();
                    }
                });
            }
        }
        //Namrata:12/04/2019
        private void GetIndexForIEC61850Server()
        {

            string strRoutineName = "AIConfiguration:GetIndexForIEC61850Server";
            try
            {
                Utils.CurrentSlaveFinal = currentSlave;
                ucai.ChkIEC61850MapIndex.DataSource = null;
                List<string> List = new List<string>();
                if (Utils.dsAISlave.Tables.Count > 0)
                {
                    List = (from r in Utils.dsAISlave.Tables[Utils.CurrentSlaveFinal].AsEnumerable() select r.Field<string>("ObjectReferrence")).ToList();
                    ObjectRefForMap = ((DataTable)Utils.dsAISlave.Tables[Utils.CurrentSlaveFinal]).AsEnumerable().Select(x => x[0].ToString()).ToList();
                    NodeForMap = ((DataTable)Utils.dsAISlave.Tables[Utils.CurrentSlaveFinal]).AsEnumerable().Select(x => x[1].ToString()).ToList();
                    var MergeNodeObjeRefList = ObjectRefForMap.Zip(NodeForMap, (leftTooth, rightTooth) => leftTooth + "," + rightTooth).ToList();//Merge List
                    MergeListMap = MergeNodeObjeRefList;
                    ucai.ChkIEC61850MapIndex.Items.Clear();
                    foreach (var kv in ObjectRefForMap)
                    {
                        ucai.ChkIEC61850MapIndex.Items.Add(kv.ToString());
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        Graphics graphics;
        private void FocusPictureBox(string id, bool IsFocused)
        {
            
            for (int i = 1; i <= 60; i++)
            {
                PictureBox pb = (PictureBox)ucai.TblLayoutCSLD.Controls[i.ToString()];
                Color GreenColour = Color.FromArgb(144, 202, 249);
                if (pb != null)
                {
                    if (i.ToString() == id)
                    {
                        if (IsFocused == true)
                        {
                           
                            graphics = pb.CreateGraphics();
                            Color color = Color.DarkBlue;
                            ControlPaint.DrawBorder(graphics, pb.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                            pb.BackColor = GreenColour;
                        }
                        else
                        {
                            graphics = pb.CreateGraphics();
                            Color color = Color.White; pb.BackColor = Color.White;
                            ControlPaint.DrawBorder(graphics, pb.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                        }

                    }
                    else
                    {
                        graphics = pb.CreateGraphics();
                        Color color = Color.White; pb.BackColor = Color.White;
                        ControlPaint.DrawBorder(graphics, pb.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
                    }
                }
            }
        }
        private void CmbCellNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "AIConfiguration:CmbCellNo_SelectedIndexChanged";
            try
            {
                if (ucai.CmbCellNo.Items.Count > 1)
                {
                    if ((ucai.CmbCellNo.SelectedIndex != -1))
                    {
                        var rowColl = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable();
                        if (rowColl.Where(x => x["CellNo"].ToString() == ucai.CmbCellNo.SelectedItem.ToString()).Any())
                        {
                            string Widget = rowColl.Where(x => x["CellNo"].ToString() == ucai.CmbCellNo.SelectedItem.ToString()).Select(x => x["Widget"].ToString()).FirstOrDefault();
                            //string Widget = (from r in rowColl
                            //                 where r.Field<string>("CellNo") == ucdo.CmbCellNo.SelectedItem.ToString()
                            //                 select r.Field<string>("Widget")).First<string>();
                            ucai.CmbWidget.Enabled = false;
                            ucai.CmbWidget.SelectedIndex = ucai.CmbWidget.FindStringExact(Widget);
                            ucai.CmbWidget.SelectedItem = Widget;
                            //Namrata: 15/10/2019
                            FocusPictureBox(ucai.CmbCellNo.Text, true);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CmbCellNo_SelectedIndexChanged_Old(object sender, EventArgs e)
        {
            string strRoutineName = "AIConfiguration:refreshMapList";
            try
            {
                if (ucai.CmbCellNo.Items.Count > 1)
                {
                    if ((ucai.CmbCellNo.SelectedIndex != -1))
                    {
                        var rowColl = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable();
                        string name = (from r in rowColl
                                       where r.Field<string>("CellNo") == ucai.CmbCellNo.SelectedItem.ToString()
                                       select r.Field<string>("Widget")).First<string>();
                        ucai.CmbWidget.Enabled = false;
                        ucai.CmbWidget.SelectedIndex = ucai.CmbWidget.FindStringExact(name);
                        ucai.CmbWidget.SelectedItem = name;
                        ucai.CmbWidget.SelectedText = name;
                        //Namrata: 15/10/2019
                        FocusPictureBox(ucai.CmbCellNo.Text, true);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GetDataForGDisplaySlave()
        {
            string strRoutineName = "AIConfiguration:GetDataForGDisplaySlave";
            try
            {
                Utils.CurrentSlaveFinal = currentSlave;
                ucai.CmbCellNo.DataSource = null;
                if (GDSlave.DsExcelData.Tables.Count > 0)
                {
                    GDSlave.ListImageAI();
                    ListToDataTable(GDSlave.ListAI);
                    List<string> tblNameList = GDSlave.DsExcelData.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                    string CurrentSlave = tblNameList.Where(x => x.Contains(Utils.CurrentSlaveFinal)).Select(x => x).FirstOrDefault();
                    GDSlave.CurrentSlave = CurrentSlave;

                    //Namrata: 24/09/2019
                    List<string> DistinctDataSetCellNo = GDSlave.DsExcelData.Tables[CurrentSlave].Rows.OfType<DataRow>()
                                                   .Where(x => x.Field<string>("Widget").Contains("Blank") || x.Field<string>("Widget").Contains("Text_")).Select(x => x.Field<string>("CellNo")).Distinct().ToList();
                    
                    List<string> DistinctDataSetWidgets = GDSlave.DsExcelData.Tables[CurrentSlave].Rows.OfType<DataRow>()
                                                   .Where(x => x.Field<string>("Widget").Contains("Blank") || x.Field<string>("Widget").Contains("Text_")).Select(x => x.Field<string>("Widget")).Distinct().ToList();
                    if (DistinctDataSetCellNo.Count > 0)
                    {
                        ucai.CmbCellNo.DataSource = null;
                        ucai.CmbCellNo.Items.Clear();
                        ucai.CmbCellNo.DataSource = DistinctDataSetCellNo;
                        ucai.CmbCellNo.SelectedIndex = 0;
                    }
                    if (DistinctDataSetWidgets.Count > 0)
                    {
                        ucai.CmbWidget.DataSource = null;
                        ucai.CmbWidget.Items.Clear();
                        ucai.CmbWidget.DataSource = DistinctDataSetWidgets;
                        ucai.CmbWidget.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Namrata: 16/08/2019
        private void GetDataForGDisplaySlave11()
        {
            string strRoutineName = "AIConfiguration:GetDataForGDisplaySlave";
            try
            {
                Utils.CurrentSlaveFinal = currentSlave;
                ucai.CmbCellNo.DataSource = null;
                if (GDSlave.DsExcelData.Tables.Count > 0)
                {
                    GDSlave.ListImageAI();
                    ListToDataTable(GDSlave.ListAI);

                    List<string> tblNameList = GDSlave.DsExcelData.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                    string CurrentSlave = tblNameList.Where(x => x.Contains(Utils.CurrentSlaveFinal)).Select(x => x).FirstOrDefault();
                    GDSlave.CurrentSlave = CurrentSlave;
                    var CellNo = Utils.GetListFromDT_StringCol(GDSlave.DsExcelData.Tables[CurrentSlave], "CellNo", false);

                    CellNo = ((DataTable)GDSlave.DsExcelData.Tables[CurrentSlave]).AsEnumerable().Select(x => x[0].ToString()).Distinct().ToList(); //Get CellNo from DataTable

                    //dTable.AsEnumerable().Where(r => r.Field<string>("col1") == "ali").ToList().ForEach(row => row.Delete());

                    #region Fill CellNo
                    ucai.CmbCellNo.Items.Clear();
                    foreach (var cellNo in CellNo)
                    {
                        ucai.CmbCellNo.Items.Add(cellNo.ToString());
                    }
                    ucai.CmbCellNo.SelectedIndex = ucai.CmbCellNo.FindStringExact("1");
                    #endregion Fill CellNo

                    #region Fill Widget
                    var Widget = (from r in GDSlave.DTAIImage.AsEnumerable() select r.Field<string>("Image")).Distinct();
                    ucai.CmbWidget.Items.Clear();
                    foreach (var widget in Widget)
                    {
                        ucai.CmbWidget.Items.Add(widget.ToString());
                    }
                    ucai.CmbWidget.SelectedIndex = ucai.CmbWidget.FindStringExact("Symbol_CT_H_U_1");
                    #endregion Fill Widget
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Namrata: 06/08/2019
        private void btnAIMDone_Click(object sender, EventArgs e)
        {
            string strRoutineName = "AIConfiguration:btnAIMDone_Click";
            try
            {
                #region Declaration
                int iIncrement = 2; int AINumber = 0, AIINdex = 0; int AIINdex1 = 0;
                int intAIIndex = 0, intAIIndex1 = 0;
                string AIDescription = ""; int intRange = 0;
                #endregion Declaration

                #region List KeyVauePair
                List<KeyValuePair<string, string>> aimData = Utils.getKeyValueAttributes(ucai.grpAIMap);
                 int intStart = Convert.ToInt32(aimData[19].Value); //AINo
                if (aimData[13].Value != "")
                {
                    intRange = Convert.ToInt32(aimData[13].Value); //AutoMapRange
                }
                intAIIndex = Convert.ToInt32(aimData[20].Value); //Index
                intAIIndex1 = Convert.ToInt32(aimData[20].Value); //Index

                #endregion List KeyVauePair

                #region ListViewItem
                ListViewItem item = ucai.lvAIlist.Items.Cast<ListViewItem>().FirstOrDefault(x => x.Text == intStart.ToString());//Namrata:8/7/2017 //Find Index Of ListView
                string ind = ucai.lvAIlist.Items.IndexOf(item).ToString();
                ListViewItem ExistAIMap = ucai.lvAIMap.FindItemWithText(ucai.txtAIMNo.Text);//Namrata:31/7/2017 //Eliminate Duplicate Record From lvAIMAp List
                #endregion ListViewItem

                #region Error adding AI map for slave
                if (!slavesAIMapList.TryGetValue(currentSlave, out slaveAIMapList))
                {
                    MessageBox.Show("Error adding AI map for slave!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion Error adding AI map for slave

                #region Add
                if (mapMode == Mode.ADD)
                {
                    #region IEC61850Server
                    if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server)
                    {
                        IEC61850MapCheckedList = ucai.ChkIEC61850MapIndex.CheckBoxItems.Where(x => x.Checked == true).Select(x => x.Text).ToList();//Namrata:25/03/2019
                        if (IEC61850MapCheckedList != null)
                        {
                            intMapCheckItems = IEC61850MapCheckedList.Count();
                        }
                        int iListCount = 0;
                        if (aiList.Count >= 0)
                        {
                            if ((intMapCheckItems + Convert.ToInt16(ind)) > ucai.lvAIlist.Items.Count)
                            {
                                MessageBox.Show("Slave Entry Does Not Exist", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            else
                            {
                                for (int i = intStart; i <= intStart + intMapCheckItems - 1; i++)
                                {
                                    if (aiList.Select(x => x.AINo).ToList().Contains(i.ToString()))
                                    {
                                        AINumber = i;
                                        AIINdex = intAIIndex++;
                                        AIDescription = getDescription(ucai.lvAIlist, AINumber.ToString());//Namrata:27/06/2019
                                        AIMap NewAIMap = new AIMap("AI", aimData, Utils.getSlaveTypes(currentSlave));
                                        NewAIMap.AINo = AINumber.ToString();
                                        NewAIMap.Description = AIDescription;
                                        NewAIMap.IEC61850ReportingIndex = IEC61850MapCheckedList[iListCount].ToString();
                                        if ((ucai.cmbAIMDataType.SelectedItem.ToString() == "Float_MSB_LSB") || (ucai.cmbAIMDataType.Text == "Float_LSB_MSB") || (ucai.cmbAIMDataType.Text == "Float_MSWord_LSWord") || (ucai.cmbAIMDataType.Text == "Float_LSWord_MSWord") || (ucai.cmbAIMDataType.Text == "UnsignedInt32_LSB_MSB") || (ucai.cmbAIMDataType.Text == "SignedInt32_LSB_MSB") || (ucai.cmbAIMDataType.Text == "UnsignedInt32_MSB_LSB") || (ucai.cmbAIMDataType.Text == "SignedInt32_MSB_LSB") || (ucai.cmbAIMDataType.Text == "UnsignedLong32_Bit_MSWord_LSWord") || (ucai.cmbAIMDataType.Text == "UnsignedLong32_Bit_LSWord_MSWord") || (ucai.cmbAIMDataType.Text == "SignedLong32_Bit_MSWord_LSWord") || (ucai.cmbAIMDataType.Text == "SignedLong32_Bit_LSWord_MSWord"))
                                        {
                                            if ((slaveAIMapList.Count == 0) || (intRange == 1))
                                            {
                                                AIINdex = Convert.ToInt32(ucai.txtAIMReportingIndex.Text);
                                            }
                                            else { }
                                        }
                                        else
                                        {
                                            NewAIMap.ReportingIndex = (AIINdex).ToString();
                                        }
                                        if (masterType == MasterTypes.IEC61850Client)
                                        {
                                            if (ucai.ChkIEC61850MapIndex.Text.ToString() == "")
                                            {
                                                MessageBox.Show("Fields cannot empty", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                return;
                                            }
                                        }
                                        slaveAIMapList.Add(NewAIMap);
                                        iListCount++;
                                        if (iIncrement == 1) { iIncrement = 2; } //Namrata: 28/11/2017
                                    }
                                }
                            }
                        }
                    }
                    #endregion IEC61850Server

                    #region OtherSlaves
                    else
                    {
                        if (aiList.Count >= 0)
                        {
                            if ((intRange + Convert.ToInt16(ind)) > ucai.lvAIlist.Items.Count)
                            {
                                MessageBox.Show("Slave Entry Does Not Exist", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            else
                            {
                                for (int i = intStart; i <= intStart + intRange - 1; i++)
                                {
                                    AIDescription = "";
                                    if (aiList.Select(x => x.AINo).ToList().Contains(i.ToString()))
                                    {
                                        AINumber = i;
                                        AIINdex = intAIIndex++;
                                        AIDescription = aimData[12].Value.ToString(); //Namrata: 27/06/2019
                                    }
                                    else
                                    {
                                        AINumber = i;
                                        AIINdex = intAIIndex++;
                                        AIINdex1 = intAIIndex1++;
                                        AIDescription = aimData[12].Value.ToString(); //Namrata: 27/06/2019
                                    }
                                    AIMap NewAIMap = new AIMap("AI", aimData, Utils.getSlaveTypes(currentSlave));
                                    NewAIMap.AINo = AINumber.ToString();
                                    NewAIMap.ReportingIndex = AIINdex.ToString();
                                    if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server))
                                    {
                                        if ((ucai.cmbDataType.Text == "UnsignedInt32_LSB_MSB") || (ucai.cmbDataType.Text == "SignedInt32_LSB_MSB") || (ucai.cmbDataType.Text == "UnsignedInt32_MSB_LSB") || (ucai.cmbDataType.Text == "SignedInt32_MSB_LSB") || (ucai.cmbDataType.Text == "Float_LSB_MSB") || (ucai.cmbDataType.Text == "Float_MSB_LSB") || (ucai.cmbDataType.Text == "UnsignedLong32_Bit_MSWord_LSWord") || (ucai.cmbDataType.Text == "UnsignedLong32_Bit_LSWord_MSWord") || (ucai.cmbDataType.Text == "SignedLong32_Bit_MSWord_LSWord") || (ucai.cmbDataType.Text == "SignedLong32_Bit_LSWord_MSWord") || (ucai.cmbDataType.Text == "Float_MSWord_LSWord") || (ucai.cmbDataType.Text == "Float_LSWord_MSWord") || (ucai.cmbDataType.Text == "UnsignedInt24_LSB_MSB") || (ucai.cmbDataType.Text == "SignedInt24_LSB_MSB") || (ucai.cmbDataType.Text == "UnsignedInt24_MSB_LSB") || (ucai.cmbDataType.Text == "SignedInt24_MSB_LSB"))
                                        {
                                            if ((slaveAIMapList.Count == 0) || (intRange == 1))
                                            {
                                                AIINdex = Convert.ToInt32(ucai.txtAIMReportingIndex.Text);
                                            }
                                            else { }
                                            intAIIndex++; intAIIndex1++;
                                        }
                                    }

                                    else
                                    {
                                        NewAIMap.ReportingIndex = AIINdex.ToString();
                                    }
                                    NewAIMap.Description = AIDescription;
                                    slaveAIMapList.Add(NewAIMap);
                                    if (iIncrement == 1) { iIncrement = 2; }
                                }
                            }
                        }
                    }
                    #endregion OtherSlaves
                }
                #endregion Add

                else if (mapMode == Mode.EDIT)
                {
                    if (ucai.ChkIEC61850MapIndex.CheckBoxItems.Count > 0)
                    {
                        if (ucai.ChkIEC61850MapIndex.CheckBoxItems.Where(x => x.Checked == true).Count() > 1)
                        {
                            MessageBox.Show("Please select only 1 index.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            slaveAIMapList[mapEditIndex].updateAttributes(aimData);
                        }
                    }
                    else
                    {
                        slaveAIMapList[mapEditIndex].updateAttributes(aimData);
                    }
                }
                refreshMapList(slaveAIMapList);
                ucai.grpAIMap.Visible = false;
                mapMode = Mode.NONE;
                mapEditIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAIMDelete_Click(object sender, EventArgs e)
        {
            string strRoutineName = "AIConfiguration:btnAIMDelete_Click";
            try
            {
                // Ajay: 07/12/2018
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    if (AllowSlaveOptionsOnClick(Utils.getSlaveTypes(currentSlave))) { return; }
                    else { }
                }
                DialogResult result = MessageBox.Show(Globals.PROMPT_DELETE_ENTRY, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.No)
                {
                    return;
                }
                List<AIMap> slaveAIMapList;
                if (!slavesAIMapList.TryGetValue(currentSlave, out slaveAIMapList))
                {
                    MessageBox.Show("Error deleting AI map!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int i = ucai.lvAIMap.Items.Count;
                for (i = ucai.lvAIMap.Items.Count - 1; i >= 0; i--)
                {
                    if (ucai.lvAIMap.Items[i].Checked)
                    {
                        if (GDSlave.DsExcelData.Tables.Count > 0)
                        {
                            //Namrata: 04/12/2019
                            var results = from row in GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable()
                                          where row.Field<string>("CellNo") == slaveAIMapList[i].CellNo && row.Field<string>("Widget") == slaveAIMapList[i].Widget && row.Field<string>("Type") == "AI"
                                          select row;
                            foreach (DataRow dataRow in results)
                            {
                                dataRow.BeginEdit();
                                dataRow["Type"] = "";
                                dataRow["Name"] = "";
                                dataRow["DBIndex"] = "";
                                dataRow["Unit"] = "";
                                dataRow["Configuration"] = dataRow[0] + "," + dataRow[1] + "," + dataRow[2] + "," + dataRow[3] + "," + dataRow[4] + "," + dataRow[5];
                                dataRow.EndEdit();
                                //dataRow.Delete();
                            }
                            GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                        }
                        slaveAIMapList.RemoveAt(i);
                        ucai.lvAIMap.Items[i].Remove();
                    }
                    //GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == slaveAIMapList[i].CellNo && x["Widget"].ToString() == slaveAIMapList[i].Widget && x["DBindex"].ToString() == slaveAIMapList[i].AINo).ToList().ForEach(Rw => Rw.Delete());
                    //GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                    
                    //}
                }
                refreshMapList(slaveAIMapList);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAIMCancel_Click(object sender, EventArgs e)
        {
            string strRoutineName = "AIConfiguration:btnAIMCancel_Click";
            try
            {
                ucai.grpAIMap.Visible = false;
                mapMode = Mode.NONE;
                mapEditIndex = -1;
                Utils.resetValues(ucai.grpAIMap);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvAIMap_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "AIConfiguration:lvAIMap_DoubleClick";
            try
            {
                // Ajay: 07/12/2018
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    if (AllowSlaveOptionsOnClick(Utils.getSlaveTypes(currentSlave))) { return; }
                    else { }
                }
                List<AIMap> slaveAIMapList;
                if (!slavesAIMapList.TryGetValue(currentSlave, out slaveAIMapList))
                {
                    MessageBox.Show("Error editing AI map for slave!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int ListIndex = ucai.lvAIMap.FocusedItem.Index;
                ListViewItem lvi = ucai.lvAIMap.Items[ListIndex];//ucai.lvAIlist.SelectedItems[0];
                Utils.UncheckOthers(ucai.lvAIMap, lvi.Index);
                if (slaveAIMapList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC101SLAVE || Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC104))
                {
                    IEC101Slave_IEC104Slave_OnMapDoubleClickEvents();
                    ucai.ChkIEC61850MapIndex.Visible = false;//Namrata:16/04/2019
                    ucai.txtAutoMapM.Text = "1";
                }
                else if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE)
                {
                    MODBUSSlave_OnDoubleClick();
                    ucai.ChkIEC61850MapIndex.Visible = false;//Namrata:16/04/2019
                    ucai.txtAutoMapM.Text = "1";
                }
                else if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.SPORTSLAVE) //Ajay: 05/07/2018)
                {
                    SPORTSlave_OnDoubleClickEvents();
                    ucai.CmbReportingIndex.Visible = false;
                    ucai.ChkIEC61850MapIndex.Visible = false;//Namrata:16/04/2019
                    ucai.txtAutoMapM.Text = "1";
                }
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server)
                {
                    GetIndexForIEC61850Server();
                    IEC61850_OnMapDoubleClick(); ucai.txtAutoMapM.Text = "";
                }
                //Namrata: 20/08/2019
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.GRAPHICALDISPLAYSLAVE)
                {
                    GetDataForGDisplaySlave();
                    DisplaySlave_OnDoubleClick(); ucai.txtAutoMapM.Text = "";
                }
                else if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MQTTSLAVE)//Namrata:02/04/2019
                {
                    MQTTSlave_OnDoubleClick();
                }
                else if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.SMSSLAVE) //Namrata: 30/05/2019
                {
                    SMSSlave_OnDoubleClick();
                }
                else if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE)
                {
                    DNPSlave_OnDoubleClick();
                    ucai.ChkIEC61850MapIndex.Visible = false;//Namrata:16/04/2019
                    ucai.txtAutoMapM.Text = "1";
                }
                    ucai.grpAIMap.Visible = true;
                mapMode = Mode.EDIT;
                mapEditIndex = lvi.Index;
                ucai.ChkIEC61850MapIndex.Text = ""; //Namrata:25/03/2019
                if ((IEC61850MapCheckedList != null) || (IEC61850MapCheckedList == null))
                {
                    ucai.ChkIEC61850MapIndex.CheckBoxItems.ForEach(x =>
                    {
                        x.Checked = false; x.CheckState = CheckState.Unchecked;
                    });
                }
                if (ucai.ChkIEC61850MapIndex.CheckBoxItems.Count > 0)
                {
                    ucai.ChkIEC61850MapIndex.Text = slaveAIMapList[lvi.Index].IEC61850ReportingIndex.ToString();
                }
                loadMapValues();
                if (ucai.ChkIEC61850MapIndex.CheckBoxItems.Count > 0)
                {
                    //Namrata:26/03/2019
                    ucai.ChkIEC61850MapIndex.CheckBoxItems.Where(x => x.Text == slaveAIMapList[lvi.Index].IEC61850ReportingIndex.ToString()).Take(1).ToList().ForEach(x =>
                    {
                        x.Checked = true; x.CheckState = CheckState.Checked;
                    });
                }
                ucai.txtAIMReportingIndex.Focus();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string getDescription(ListView lstview, string ainno)
        {
            try
            {
                int iColIndex = ucai.lvAIlist.Columns["Description"].Index;
                var query = lstview.Items.Cast<ListViewItem>().Where(item => item.SubItems[0].Text == ainno).Select(s => s.SubItems[iColIndex].Text).Single();
                return query.ToString();
            }
            catch
            {
                return null;
            }
        }
        private void loadMapDefaults()
        {
            string strRoutineName = "AIConfiguration:loadMapDefaults";
            try
            {
                ucai.txtAIMReportingIndex.Text = (Globals.AIReportingIndex + 1).ToString();
                ucai.txtAIMDeadBand.Text = "1";
                ucai.txtAIMMultiplier.Text = "1";
                ucai.txtAIMConstant.Text = "0";
                ucai.txtUnitID.Text = "1"; //Ajay:06/07/2018
                ucai.txtUnit.Text = " ";

            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadMapValues()
        {
            string strRoutineName = "AIConfiguration:loadMapValues";
            try
            {
                List<AIMap> slaveAIMapList;
                if (!slavesAIMapList.TryGetValue(currentSlave, out slaveAIMapList))
                {
                    MessageBox.Show("Error loading AI map data for slave!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                AIMap aimn = slaveAIMapList.ElementAt(mapEditIndex);
                if (aimn != null)
                {
                    ucai.txtAIMNo.Text = aimn.AINo;
                    ucai.txtAIMReportingIndex.Text = aimn.ReportingIndex;
                    ucai.cmbAIMDataType.SelectedIndex = ucai.cmbAIMDataType.FindStringExact(aimn.DataType);
                    if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE))
                    {
                        ucai.cmbAIMCommandType.SelectedIndex = ucai.cmbAIMCommandType.FindStringExact(aimn.CommandType);
                        ucai.cmbAIMCommandType.Enabled = true;
                    }
                    else
                    {
                        ucai.cmbAIMCommandType.Enabled = false;
                    }
                    if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.SPORTSLAVE)
                    {
                        ucai.txtUnitID.Text = aimn.UnitID;
                        ucai.txtUnitID.Visible = true;
                        ucai.chkUsed.Visible = true;
                    }
                    else
                    {
                        ucai.txtUnitID.Enabled = false;
                        ucai.txtUnitID.Visible = false;
                        ucai.chkUsed.Enabled = false;
                        ucai.chkUsed.Visible = false;

                    }
                    //Ajay: 12/07/2018 check-unchecked Used checkbox according to mapped Used value
                    if (aimn.Used.ToLower() == "yes")
                    { ucai.chkUsed.Checked = true; }
                    else { ucai.chkUsed.Checked = false; }
                    //Namrata:09/04/2019
                    ucai.txtUnit.Text = aimn.Unit;
                    ucai.txtKey.Text = aimn.Key;

                    ucai.txtAIMDeadBand.Text = aimn.Deadband;
                    ucai.txtAIMMultiplier.Text = aimn.Multiplier;
                    ucai.txtAIMConstant.Text = aimn.Constant;
                    ucai.txtMapDescription.Text = aimn.Description;//Namrata: 18/11/2017
                    ucai.ChkIEC61850Index.SelectedIndex = ucai.ChkIEC61850Index.FindStringExact(aimn.IEC61850ReportingIndex);//Namrata:27/03/2019
                    //Ajay: 05/09/2018
                    if (aimn.Event.ToLower() == "yes")
                    { ucai.chkbxEvent.Checked = true; }
                    else { ucai.chkbxEvent.Checked = false; }

                    if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE)
                    {
                        ucai.cmbEventC.SelectedIndex = ucai.cmbEventC.FindStringExact(aimn.EventClass);//Namrata:27/03/2019
                        ucai.CmbVari.SelectedIndex = ucai.CmbVari.FindStringExact(aimn.Variation);//Namrata:27/03/2019
                        ucai.CmbEV.SelectedIndex = ucai.CmbEV.FindStringExact(aimn.EventVariation);//Namrata:27/03/2019
                    }
                    else
                    {

                    }

                        //Namrata: 20/08/2019
                        if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.GRAPHICALDISPLAYSLAVE)
                    {
                        ucai.CmbCellNo.SelectedIndex = ucai.CmbCellNo.FindStringExact(aimn.CellNo);
                        ucai.CmbWidget.SelectedIndex = ucai.CmbWidget.FindStringExact(aimn.Widget);
                        ucai.txtUnitID.Text = aimn.UnitID;
                        ucai.txtUnitID.Enabled = true;
                        ucai.txtUnitID.Visible = true;
                        if (GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == aimn.CellNo && (x["Type"].ToString() == "AI" && ((x["DBIndex"].ToString() == aimn.AINo) || string.IsNullOrEmpty(x["DBIndex"].ToString())))).Any())
                        {
                            DataRow datarow;
                            datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == aimn.CellNo && x["Type"].ToString() == "AI" && ((x["DBIndex"].ToString() == aimn.AINo) || string.IsNullOrEmpty(x["DBIndex"].ToString()))).ToList().First();
                            //datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == aimp.CellNo && x["Type"].ToString() == "AI").ToList().First();
                            datarow["CellNo"] = aimn.CellNo;
                            datarow["Widget"] = aimn.Widget;
                            datarow["Type"] = "";
                            datarow["Name"] = "";
                            datarow["DBIndex"] = "";
                            datarow["Unit"] = "";
                            string strConfiguration = datarow[0].ToString() + "," + datarow[1].ToString() + "," + datarow[2].ToString() + "," + datarow[3].ToString() + "," + datarow[4].ToString() + "," + datarow[5].ToString();
                            datarow["Configuration"] = strConfiguration;
                            GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                        }
                    }
                    else
                    {
                        ucai.CmbCellNo.Visible = false;
                        ucai.CmbWidget.Visible = false;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CmbCellNo_SelectedIndexChanged1(object sender, EventArgs e)
        {
            string strRoutineName = "AIConfiguration:refreshMapList";
            try
            {
                if (ucai.CmbCellNo.Items.Count > 1)
                {
                    if ((ucai.CmbCellNo.SelectedIndex != -1))
                    {
                        var rowColl = GDSlave.DtAIWidgets.AsEnumerable();
                        string name = (from r in rowColl
                                       where r.Field<string>("CellNo") == ucai.CmbCellNo.SelectedItem.ToString()
                                       select r.Field<string>("Widget")).First<string>();
                        ucai.CmbWidget.Enabled = false;
                        ucai.CmbWidget.SelectedIndex = ucai.CmbWidget.FindStringExact(name); ;
                        ucai.CmbWidget.SelectedItem = ucai.CmbWidget.FindStringExact(name);//name;
                        ucai.CmbWidget.SelectedText = name;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void refreshMapList(List<AIMap> tmpList)
        {
            string strRoutineName = "AIConfiguration:refreshMapList";
            try
            {
                int cnt = 0;
                ucai.lvAIMap.Items.Clear();
                if (tmpList == null) return;

               
                    #region MQTTSlave
                    //Namrata:01/04/2019
                    if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.MQTTSLAVE))
                    {
                        foreach (AIMap aimp in tmpList)
                        {
                            string[] row = new string[18];
                            if (aimp.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = aimp.AINo;
                                row[1] = " ";
                                row[2] = aimp.ReportingIndex;
                                row[3] = aimp.DataType;
                                row[4] = " ";
                                row[5] = aimp.Deadband;
                                row[6] = aimp.Multiplier;
                                row[7] = aimp.Constant;
                                row[8] = " ";
                                row[9] = " ";
                                row[10] = " ";
                                row[11] = " ";
                                row[12] = aimp.Unit;
                                row[13] = aimp.Key;
                                row[14] = "";
                                row[15] = "";
                                row[16] = "";
                                row[17] = aimp.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucai.lvAIMap.Items.Add(lvItem);
                        }
                    }
                    #endregion MQTTSlave

                    #region SMSSlave
                    //Namrata: 30/05/2019
                    if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.SMSSLAVE))
                    {
                        foreach (AIMap aimp in tmpList)
                        {
                            string[] row = new string[18];
                            if (aimp.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = aimp.AINo;
                                row[1] = " ";
                                row[2] = aimp.ReportingIndex;
                                row[3] = aimp.DataType;
                                row[4] = " ";
                                row[5] = aimp.Deadband;
                                row[6] = aimp.Multiplier;
                                row[7] = aimp.Constant;
                                row[8] = " ";
                                row[9] = " ";
                                row[10] = " ";
                                row[11] = " ";
                                row[12] = aimp.Unit;
                                row[13] = "";
                                row[14] = "";
                                row[15] = "";
                                row[16] = "";
                                row[17] = aimp.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucai.lvAIMap.Items.Add(lvItem);
                        }
                    }
                    #endregion SMSSlave

                    #region MODBUS Slave
                    if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE))
                    {
                        foreach (AIMap aimp in tmpList)
                        {
                            string[] row = new string[18];
                            if (aimp.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = aimp.AINo;
                                row[1] = ""; //Ajay: 05/09/2018
                                row[2] = aimp.ReportingIndex;
                                row[3] = aimp.DataType;
                                row[4] = aimp.CommandType;
                                row[5] = aimp.Deadband;
                                row[6] = aimp.Multiplier;
                                row[7] = aimp.Constant;
                                row[8] = ""; //Ajay: 05/09/2018
                                row[9] = ""; //Ajay: 05/09/2018
                                row[10] = ""; //Ajay: 05/09/2018
                                row[11] = ""; //Ajay: 05/09/2018
                                row[12] = ""; //Ajay: 05/09/2018
                                row[13] = "";
                                row[13] = aimp.Key;
                                row[14] = "";
                                row[15] = "";
                                row[16] = "";
                                row[17] = aimp.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucai.lvAIMap.Items.Add(lvItem);
                        }
                    }
                    #endregion MODBUS Slave

                    #region OtherSlaves
                    //Ajay: 05/09/2018
                    if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC104) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC101SLAVE))
                    {
                        foreach (AIMap aimp in tmpList)
                        {
                            string[] row = new string[18];
                            if (aimp.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = aimp.AINo;
                                row[1] = ""; //Ajay: 05/09/2018
                                row[2] = aimp.ReportingIndex;
                                row[3] = aimp.DataType;
                                row[4] = aimp.CommandType;
                                row[5] = aimp.Deadband;
                                row[6] = aimp.Multiplier;
                                row[7] = aimp.Constant;
                                row[8] = ""; //Ajay: 05/09/2018
                                row[9] = aimp.Event; //Ajay: 05/09/2018
                                row[10] = ""; //Ajay: 05/09/2018
                                row[11] = ""; //Ajay: 05/09/2018
                                row[12] = ""; //Ajay: 05/09/2018
                                row[13] = "";
                                row[13] = aimp.Key;
                                row[14] = "";
                                row[15] = "";
                                row[16] = "";
                                row[17] = aimp.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucai.lvAIMap.Items.Add(lvItem);
                        }
                    }
                    #endregion OtherSlaves

                    #region SPORT Slave
                    if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.SPORTSLAVE))
                    {
                        foreach (AIMap aimp in tmpList)
                        {
                            string[] row = new string[18];
                            if (aimp.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = aimp.AINo;
                                row[1] = aimp.UnitID;
                                row[2] = aimp.ReportingIndex;
                                row[3] = aimp.DataType;
                                row[4] = aimp.CommandType;
                                row[5] = aimp.Deadband;
                                row[6] = aimp.Multiplier;
                                row[7] = aimp.Constant;
                                row[8] = aimp.Used;
                                row[9] = aimp.Event; //Ajay: 05/09/2018
                                row[10] = ""; //Ajay: 05/09/2018
                                row[11] = ""; //Ajay: 05/09/2018
                                row[12] = ""; //Ajay: 05/09/2018
                                row[13] = "";
                                row[13] = aimp.Key;
                                row[14] = "";
                                row[15] = "";
                                row[16] = "";
                                row[17] = aimp.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucai.lvAIMap.Items.Add(lvItem);
                        }
                    }
                    #endregion SPORT Slave

                    #region IEC61850Server
                    else if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server)
                    {
                        foreach (AIMap aimp in tmpList)
                        {
                            string[] row = new string[18];
                            if (aimp.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = aimp.AINo;
                                row[1] = ""; //Ajay: 05/09/2018
                                row[2] = aimp.IEC61850ReportingIndex;
                                row[3] = aimp.DataType;
                                row[4] = aimp.CommandType;
                                row[5] = aimp.Deadband;
                                row[6] = aimp.Multiplier;
                                row[7] = aimp.Constant;
                                row[8] = ""; //Ajay: 05/09/2018
                                row[9] = ""; //Ajay: 05/09/2018
                                row[10] = ""; //Ajay: 05/09/2018
                                row[11] = ""; //Ajay: 05/09/2018
                                row[12] = ""; //Ajay: 05/09/2018
                                row[13] = "";
                                row[13] = aimp.Key;
                                row[14] = "";
                                row[15] = "";
                                row[16] = "";
                                row[17] = aimp.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucai.lvAIMap.Items.Add(lvItem);
                        }
                    }
                    #endregion IEC61850Server

                    #region Graphical Display
                    if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.GRAPHICALDISPLAYSLAVE))
                {
                    foreach (AIMap aimp in tmpList)
                        {
                            string[] row = new string[18];
                            if (aimp.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = aimp.AINo;
                                row[1] = aimp.UnitID;
                                row[2] = "";
                                row[3] = "";
                                row[4] = "";
                                row[5] = "";
                                row[6] = "";
                                row[7] = "";
                                row[8] = "";
                                row[9] = "";
                                row[10] = aimp.CellNo;
                                row[11] = aimp.Widget;
                                row[12] = "";// aimp.Unit; //Ajay: 05/09/2018
                                row[13] = "";
                                row[13] = aimp.Key;
                                row[14] = "";
                                row[15] = "";
                                row[16] = "";
                                row[17] = aimp.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucai.lvAIMap.Items.Add(lvItem);
                            //Namrata: 16/09/2019
                            #region Update Dataset Tables as per Slave
                            if (aimp.CellNo != "")
                            {
                                string strConfiguration = aimp.CellNo + "," + aimp.Widget + "," + "AI" + "," + aimp.Description + "," + aimp.AINo + "," + aimp.UnitID;
                                if (GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == aimp.CellNo && (x["Type"].ToString() == "AI" && ((x["DBIndex"].ToString() == aimp.AINo) || string.IsNullOrEmpty(x["DBIndex"].ToString())))).Any())
                                //if (GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == aimp.CellNo && (x["Type"].ToString() == "AI")).Any())
                                {
                                    DataRow datarow;
                                    datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == aimp.CellNo && x["Type"].ToString() == "AI" && ((x["DBIndex"].ToString() == aimp.AINo) || string.IsNullOrEmpty(x["DBIndex"].ToString()))).ToList().First();
                                    //datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == aimp.CellNo && x["Type"].ToString() == "AI").ToList().First();
                                    datarow["CellNo"] = aimp.CellNo;
                                    datarow["Widget"] = aimp.Widget;
                                    datarow["Type"] = "AI";
                                    datarow["Name"] = aimp.Description;
                                    datarow["DBIndex"] = aimp.AINo;
                                    datarow["Unit"] = aimp.UnitID;
                                    datarow["Configuration"] = strConfiguration;
                                    GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                                }
                                else if (GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == aimp.CellNo && string.IsNullOrEmpty(x["Type"].ToString())).ToList().Any())
                                {
                                    DataRow datarow;
                                    datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == aimp.CellNo && string.IsNullOrEmpty(x["Type"].ToString())).ToList().First();
                                    datarow["CellNo"] = aimp.CellNo;
                                    datarow["Widget"] = aimp.Widget;
                                    datarow["Type"] = "AI";
                                    datarow["Name"] = aimp.Description;
                                    datarow["DBIndex"] = aimp.AINo;
                                    datarow["Unit"] = aimp.UnitID;
                                    datarow["Configuration"] = strConfiguration;
                                    GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                                }
                            }
                            #endregion Update Dataset Tables as per Slave
                        }
                    }
                    #endregion Graphical Display

                    #region DNP3 Slave
                    if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE))
                    {
                        foreach (AIMap aimp in tmpList)
                        {
                            string[] row = new string[18];
                            if (aimp.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {

                                row[0] = aimp.AINo;
                                row[1] = "";
                                row[2] = aimp.ReportingIndex;
                                row[3] = "";
                                row[4] = aimp.CommandType;
                                row[5] = aimp.Deadband;
                                row[6] = aimp.Multiplier;
                                row[7] = aimp.Constant;
                                row[8] = "";
                                row[9] = "";
                                row[10] = "";
                                row[11] = "";
                                row[12] = "";
                                row[13] = "";
                                row[14] = aimp.EventClass;
                                row[15] = aimp.Variation;
                                row[16] = aimp.EventVariation;
                                row[17] = aimp.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucai.lvAIMap.Items.Add(lvItem);
                        }
                    }
                    #endregion DNP3 Slave
                
                ucai.lblAIMRecords.Text = tmpList.Count.ToString();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void refreshCurrentMapList()
        {
            string strRoutineName = "AIConfiguration:refreshCurrentMapList";
            try
            {
                fillMapOptions(Utils.getSlaveTypes(currentSlave));
                List<AIMap> saimList;
                if (!slavesAIMapList.TryGetValue(currentSlave, out saimList))
                {
                    refreshMapList(null);
                }
                else
                {
                    refreshMapList(saimList);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /* ============================================= Above this, AI Map logic... ============================================= */

        private void fillOptions()
        {
            string strRoutineName = "AIConfiguration:fillOptions";
            try
            {
                if (!dtdataset.Columns.Contains("Address")) //Ajay: 22/09/2018 Condition checked to handle exception
                { DataColumn dcAddressColumn = dtdataset.Columns.Add("Address", typeof(string)); }
                if (!dtdataset.Columns.Contains("IED")) //Ajay: 22/09/2018 Condition checked to handle exception
                { dtdataset.Columns.Add("IED", typeof(string)); }
                List<string> tblNameList = Utils.dsIED.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                string tblName = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
                ucai.cmbIEDName.DataSource = Utils.dsIED.Tables[tblName];
                ucai.cmbIEDName.DisplayMember = "IEDName";
               
                //Namrata: 04/04/2018
                if (Utils.Iec61850IEDname != "")
                {
                    ucai.cmbIEDName.Text = Utils.Iec61850IEDname;
                }
                //Namrata: 15/9/2017
                //Fill ResponseType For IEC61850Client
                //Namrata: 31/10/2017
                DataSet ds11 = Utils.dsResponseType;
                ucai.cmb61850ResponseType.DataSource = Utils.dsResponseType.Tables[Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname];//[Utils.strFrmOpenproplusTreeNode + "/" + "Undefined" + "/" + Utils.Iec61850IEDname];
                ucai.cmb61850ResponseType.DisplayMember = "Address";
                //Fill Response Type...
                if (masterType == MasterTypes.IEC61850Client)
                {
                    ucai.cmbResponseType.Items.Clear();
                }
                else
                {
                    ucai.cmbResponseType.Items.Clear();
                    if (masterType != MasterTypes.LoadProfile) //Ajay: 31/07/2018
                    {
                        foreach (String rt in AI.getResponseTypes(masterType))
                        {
                            ucai.cmbResponseType.Items.Add(rt.ToString());
                        }
                        ucai.cmbResponseType.SelectedIndex = 0;
                    }
                }
                //Fill Data Type...
                if ((masterType == MasterTypes.IEC61850Client) || (masterType == MasterTypes.SPORT))
                {
                    ucai.cmbDataType.Items.Clear();
                }
                else
                {
                    ucai.cmbDataType.Items.Clear();
                    if (masterType != MasterTypes.LoadProfile) //Ajay: 31/07/2018
                    {
                        foreach (String dt in AI.getDataTypes(masterType))
                        {
                            ucai.cmbDataType.Items.Add(dt.ToString());
                        }
                        ucai.cmbDataType.SelectedIndex = 0;
                    }
                }
                //Fill AI1, AI2, AI3
                if (masterType != MasterTypes.LoadProfile)
                {
                    ucai.cmbAI1.Items.Clear();
                    ucai.cmbAI2.Items.Clear();
                    ucai.cmbAI3.Items.Clear();
                }
                else
                {
                    ucai.cmbName.DataSource = Utils.getOpenProPlusHandle().getDataTypeValues("LoadProfile_AI_Name");
                    ucai.cmbAI1.DataSource = Utils.GetAllAIList();
                    ucai.cmbAI2.DataSource = Utils.GetAllAIList();
                    ucai.cmbAI3.DataSource = Utils.GetAllAIList();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void fillMapOptions(SlaveTypes sType)
        {
            string strRoutineName = "AIConfiguration:fillMapOptions";
            try
            {
                try
                {
                    //Fill Data Type...
                    ucai.cmbAIMDataType.Items.Clear();
                    foreach (String dt in AIMap.getDataTypes(sType))
                    {
                        ucai.cmbAIMDataType.Items.Add(dt.ToString());
                    }
                    if (ucai.cmbAIMDataType.Items.Count > 0) ucai.cmbAIMDataType.SelectedIndex = 0;

                    #region Fill Variations
                    ucai.CmbVari.Items.Clear();
                    foreach (String dt in AIMap.getVariartions(sType))
                    {
                        ucai.CmbVari.Items.Add(dt.ToString());
                    }
                    if (ucai.CmbVari.Items.Count > 0) ucai.CmbVari.SelectedIndex = 0;
                    #endregion Fill Variations

                    #region Fill EVariations
                    ucai.CmbEV.Items.Clear();
                    foreach (String dt in AIMap.getEventsVariations(sType))
                    {
                        ucai.CmbEV.Items.Add(dt.ToString());
                    }
                    if (ucai.CmbEV.Items.Count > 0) ucai.CmbEV.SelectedIndex = 0;
                    #endregion Fill EVariations

                    #region Fill EClass
                    ucai.cmbEventC.Items.Clear();
                    foreach (String dt in AIMap.getEventsClasses(sType))
                    {
                        ucai.cmbEventC.Items.Add(dt.ToString());
                    }
                    if (ucai.cmbEventC.Items.Count > 0) ucai.cmbEventC.SelectedIndex = 0;
                    #endregion Fill EClass
                }
                catch (System.NullReferenceException)
                {
                    Utils.WriteLine(VerboseLevel.ERROR, "AI Map DataType does not exist for Slave Type: {0}", sType.ToString());
                }
                try
                {
                    //Fill Command Type...
                    ucai.cmbAIMCommandType.Items.Clear();
                    foreach (String ct in AIMap.getCommandTypes(sType))
                    {
                        ucai.cmbAIMCommandType.Items.Add(ct.ToString());
                    }
                    if (ucai.cmbAIMCommandType.Items.Count > 0) ucai.cmbAIMCommandType.SelectedIndex = 0;
                }
                catch (System.NullReferenceException)
                {
                    Utils.WriteLine(VerboseLevel.ERROR, "AI Map CommandType does not exist for Slave Type: {0}", sType.ToString());
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void addListHeaders()
        {
            string strRoutineName = "AIConfiguration:addListHeaders";
            try
            {
                #region Masters
                if ((masterType == MasterTypes.IEC103) || (masterType == MasterTypes.ADR) || (masterType == MasterTypes.MODBUS))
                {
                    ucai.lvAIlist.Columns.Add("AI No.", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Sub Index", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Data Type", 200, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Multiplier", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Constant", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Event Enable", 80, HorizontalAlignment.Left); //Ajay: 30/08/2018
                    ucai.lvAIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                }
                else if (masterType == MasterTypes.IEC104)
                {
                    ucai.lvAIlist.Columns.Add("AI No.", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Sub Index", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Multiplier", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Constant", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Event Enable", 80, HorizontalAlignment.Left); //Ajay: 30/08/2018
                    ucai.lvAIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                }
                //Namrata:29/01/2019
                else if (masterType == MasterTypes.SPORT)
                {
                    ucai.lvAIlist.Columns.Add("AI No.", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Sub Index", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Deadband", 200, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Multiplier", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Constant", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Event Enable", 80, HorizontalAlignment.Left); //Ajay: 30/08/2018
                    ucai.lvAIlist.Columns.Add("HighLimit", 80, HorizontalAlignment.Left); //Namrata:29/01/2019
                    ucai.lvAIlist.Columns.Add("LowLimit", 80, HorizontalAlignment.Left); //Namrata:29/01/2019
                    ucai.lvAIlist.Columns.Add("DONo.", 80, HorizontalAlignment.Left); //Namrata:29/01/2019
                    ucai.lvAIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                }
                else if ((masterType == MasterTypes.IEC101))
                {
                    ucai.lvAIlist.Columns.Add("AI No.", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Sub Index", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Data Type", 200, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Multiplier", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Constant", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Event Enable", 80, HorizontalAlignment.Left); //Ajay: 30/08/2018
                    ucai.lvAIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                }
                else if (masterType == MasterTypes.Virtual)
                {
                    ucai.lvAIlist.Columns.Add("AI No.", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Sub Index", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Data Type", 200, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Multiplier", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Constant", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                }
                else if (masterType == MasterTypes.IEC61850Client)
                {
                    ucai.lvAIlist.Columns.Add("AI No.", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("IEDName", 50, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Response Type", 270, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Index", 350, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("FC", 100, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Sub Index", 65, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Multiplier", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Constant", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Event Enable", 80, HorizontalAlignment.Left); //Ajay: 30/08/2018
                    ucai.lvAIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                    //Namrata: 15/9/2017
                    ucai.lvAIlist.Columns[1].Width = 0;//Hide IED Name
                }
                //Ajay: 31/07/2018
                else if (masterType == MasterTypes.LoadProfile)
                {
                    ucai.lvAIlist.Columns.Add("AI No.", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Name", 130, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("AI1", 60, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("AI2", 60, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("AI3", 60, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Event Enable", 80, HorizontalAlignment.Left); //Ajay: 30/08/2018
                    ucai.lvAIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                }
                #endregion Masters

                #region Slave
                //Add AI map headers..
                ucai.lvAIMap.Columns.Add("AI No.", 70, HorizontalAlignment.Left);
                ucai.lvAIMap.Columns.Add("Unit ID", 0, HorizontalAlignment.Left);
                ucai.lvAIMap.Columns.Add("Reporting Index", 200, HorizontalAlignment.Left);
                ucai.lvAIMap.Columns.Add("Data Type", 205, HorizontalAlignment.Left);
                ucai.lvAIMap.Columns.Add("Command Type", 0, HorizontalAlignment.Left);
                ucai.lvAIMap.Columns.Add("Deadband", 70, HorizontalAlignment.Left);
                ucai.lvAIMap.Columns.Add("Multiplier", 70, HorizontalAlignment.Left);
                ucai.lvAIMap.Columns.Add("Constant", -2, HorizontalAlignment.Left);
                ucai.lvAIMap.Columns.Add("Used", 0, HorizontalAlignment.Left);
                ucai.lvAIMap.Columns.Add("Event", 0, HorizontalAlignment.Left); //Ajay: 05/09/2018
                //Namrata: 16/08/2019
                ucai.lvAIMap.Columns.Add("CellNo", 0, HorizontalAlignment.Left);
                ucai.lvAIMap.Columns.Add("Widget", 0, HorizontalAlignment.Left);
                ucai.lvAIMap.Columns.Add("Unit", 0, HorizontalAlignment.Left); //Ajay: 05/09/2018
                ucai.lvAIMap.Columns.Add("Key", 0, HorizontalAlignment.Left); //Ajay: 05/09/2018

                //Namrata: 12/11/2019
                ucai.lvAIMap.Columns.Add("Event Class", 0, HorizontalAlignment.Left); 
                ucai.lvAIMap.Columns.Add("Variation", 0, HorizontalAlignment.Left); 
                ucai.lvAIMap.Columns.Add("Event Variation", 0, HorizontalAlignment.Left);

                //Namrata: 16/11/2017
                ucai.lvAIMap.Columns.Add("Description", 100, HorizontalAlignment.Left);

                #endregion Slave
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ShowHideSlaveColumns()
        {
            string strRoutineName = "AIConfiguration:ShowHideSlaveColumns";
            try
            {
                switch (Utils.getSlaveTypes(currentSlave))
                {
                    case SlaveTypes.IEC104:
                        ucai.lvAIMap.Columns[1].Text = "Unit ID";
                        Utils.getColumnHeader(ucai.lvAIMap, "AI No.").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Unit ID").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Reporting Index").Width = 100;
                        Utils.getColumnHeader(ucai.lvAIMap, "Data Type").Width = 150;
                        Utils.getColumnHeader(ucai.lvAIMap, "Command Type").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Deadband").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Multiplier").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Constant").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Used").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Event").Width = 100;
                        Utils.getColumnHeader(ucai.lvAIMap, "Description").Width = 150;
                        Utils.getColumnHeader(ucai.lvAIMap, "CellNo").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Widget").Width = 0;
                        //Namrata: 21/10/2019
                        Utils.getColumnHeader(ucai.lvAIMap, "Unit").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Key").Width = 0;
                        //Namrata: 12/11/2019
                        Utils.getColumnHeader(ucai.lvAIMap, "Event Class").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Variation").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Event Variation").Width = 0;
                        break;

                    case SlaveTypes.MODBUSSLAVE:
                        ucai.lvAIMap.Columns[1].Text = "Unit ID";
                        Utils.getColumnHeader(ucai.lvAIMap, "AI No.").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Unit ID").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Reporting Index").Width = 100;
                        Utils.getColumnHeader(ucai.lvAIMap, "Data Type").Width = 150;
                        Utils.getColumnHeader(ucai.lvAIMap, "Command Type").Width = 100;
                        Utils.getColumnHeader(ucai.lvAIMap, "Deadband").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Multiplier").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Constant").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Used").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Event").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Description").Width = 150;
                        Utils.getColumnHeader(ucai.lvAIMap, "CellNo").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Widget").Width = 0;
                        //Namrata: 21/10/2019
                        Utils.getColumnHeader(ucai.lvAIMap, "Unit").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Key").Width = 0;
                        //Namrata: 12/11/2019
                        Utils.getColumnHeader(ucai.lvAIMap, "Event Class").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Variation").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Event Variation").Width = 0;
                        break;

                    case SlaveTypes.IEC101SLAVE:
                        ucai.lvAIMap.Columns[1].Text = "Unit ID";
                        Utils.getColumnHeader(ucai.lvAIMap, "AI No.").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Unit ID").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Reporting Index").Width = 100;
                        Utils.getColumnHeader(ucai.lvAIMap, "Data Type").Width = 150;
                        Utils.getColumnHeader(ucai.lvAIMap, "Command Type").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Deadband").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Multiplier").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Constant").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Used").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Event").Width = 100;
                        Utils.getColumnHeader(ucai.lvAIMap, "Description").Width = 150;
                        Utils.getColumnHeader(ucai.lvAIMap, "CellNo").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Widget").Width = 0;
                        //Namrata: 21/10/2019
                        Utils.getColumnHeader(ucai.lvAIMap, "Unit").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Key").Width = 0;
                        //Namrata: 12/11/2019
                        Utils.getColumnHeader(ucai.lvAIMap, "Event Class").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Variation").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Event Variation").Width = 0;
                        break;

                    case SlaveTypes.IEC61850Server:
                        ucai.lvAIMap.Columns[1].Text = "Unit ID";
                        Utils.getColumnHeader(ucai.lvAIMap, "AI No.").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Unit ID").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Reporting Index").Width = 200;
                        Utils.getColumnHeader(ucai.lvAIMap, "Data Type").Width = 150;
                        Utils.getColumnHeader(ucai.lvAIMap, "Command Type").Width = 100;
                        Utils.getColumnHeader(ucai.lvAIMap, "Deadband").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Multiplier").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Constant").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Used").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Event").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Description").Width = 150;
                        Utils.getColumnHeader(ucai.lvAIMap, "CellNo").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Widget").Width = 0;
                        //Namrata: 21/10/2019
                        Utils.getColumnHeader(ucai.lvAIMap, "Unit").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Key").Width = 0;
                        //Namrata: 12/11/2019
                        Utils.getColumnHeader(ucai.lvAIMap, "Event Class").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Variation").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Event Variation").Width = 0;
                        break;

                    case SlaveTypes.SPORTSLAVE:
                        ucai.lvAIMap.Columns[1].Text = "Unit ID";
                        Utils.getColumnHeader(ucai.lvAIMap, "AI No.").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Unit ID").Width = 120;
                        Utils.getColumnHeader(ucai.lvAIMap, "Reporting Index").Width = 100;
                        Utils.getColumnHeader(ucai.lvAIMap, "Data Type").Width = 150;
                        Utils.getColumnHeader(ucai.lvAIMap, "Command Type").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Deadband").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Multiplier").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Constant").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Used").Width = 100;
                        Utils.getColumnHeader(ucai.lvAIMap, "Event").Width = 100;
                        Utils.getColumnHeader(ucai.lvAIMap, "Description").Width = 150;
                        Utils.getColumnHeader(ucai.lvAIMap, "CellNo").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Widget").Width = 0;
                        //Namrata: 21/10/2019
                        Utils.getColumnHeader(ucai.lvAIMap, "Unit").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Key").Width = 0;
                        //Namrata: 12/11/2019
                        Utils.getColumnHeader(ucai.lvAIMap, "Event Class").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Variation").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Event Variation").Width = 0;
                        break;

                    //Namrata: 16/08/2019
                    case SlaveTypes.GRAPHICALDISPLAYSLAVE:
                        ucai.lvAIMap.Columns[1].Text = "Unit";
                        Utils.getColumnHeader(ucai.lvAIMap, "AI No.").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Unit").Width = 120;
                        Utils.getColumnHeader(ucai.lvAIMap, "Reporting Index").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Data Type").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Command Type").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Deadband").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Multiplier").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Constant").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Used").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Event").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Description").Width = 150;
                        Utils.getColumnHeader(ucai.lvAIMap, "CellNo").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Widget").Width = 150;
                        //Namrata: 21/10/2019
                        Utils.getColumnHeader(ucai.lvAIMap, "Unit").Width = 100;
                        Utils.getColumnHeader(ucai.lvAIMap, "Key").Width = 0;
                        //Namrata: 12/11/2019
                        Utils.getColumnHeader(ucai.lvAIMap, "Event Class").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Variation").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Event Variation").Width = 0;
                        break;
                    //Namrata: 21/10/2019
                    case SlaveTypes.MQTTSLAVE:
                        ucai.lvAIMap.Columns[1].Text = "Unit ID";
                        //ucai.lvAIMap.Columns[1].Text = "Unit";
                        Utils.getColumnHeader(ucai.lvAIMap, "AI No.").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Unit ID").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Reporting Index").Width = 100;
                        Utils.getColumnHeader(ucai.lvAIMap, "Data Type").Width = 100;
                        Utils.getColumnHeader(ucai.lvAIMap, "Command Type").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Deadband").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Multiplier").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Constant").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Used").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Event").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Description").Width = 150;
                        Utils.getColumnHeader(ucai.lvAIMap, "CellNo").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Widget").Width = 0;
                        //Namrata: 21/10/2019
                        Utils.getColumnHeader(ucai.lvAIMap, "Unit").Width = 100;
                        Utils.getColumnHeader(ucai.lvAIMap, "Key").Width = 120;
                        break;
                    //Namrata: 21/10/2019
                    case SlaveTypes.SMSSLAVE:
                        ucai.lvAIMap.Columns[1].Text = "Unit ID";
                        Utils.getColumnHeader(ucai.lvAIMap, "AI No.").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Unit ID").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Reporting Index").Width = 100;
                        Utils.getColumnHeader(ucai.lvAIMap, "Data Type").Width = 100;
                        Utils.getColumnHeader(ucai.lvAIMap, "Command Type").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Deadband").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Multiplier").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Constant").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Used").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Event").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Description").Width = 150;
                        Utils.getColumnHeader(ucai.lvAIMap, "CellNo").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Widget").Width = 0;
                        //Namrata: 21/10/2019
                        Utils.getColumnHeader(ucai.lvAIMap, "Unit").Width = 100;
                        Utils.getColumnHeader(ucai.lvAIMap, "Key").Width = 0;
                        //Namrata: 12/11/2019
                        Utils.getColumnHeader(ucai.lvAIMap, "Event Class").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Variation").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Event Variation").Width = 0;
                        break;

                    case SlaveTypes.DNP3SLAVE:
                        ucai.lvAIMap.Columns[1].Text = "Unit ID";
                        Utils.getColumnHeader(ucai.lvAIMap, "AI No.").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Unit ID").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Reporting Index").Width = 100;
                        Utils.getColumnHeader(ucai.lvAIMap, "Data Type").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Command Type").Width = 100;
                        Utils.getColumnHeader(ucai.lvAIMap, "Deadband").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Multiplier").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Constant").Width = 80;
                        Utils.getColumnHeader(ucai.lvAIMap, "Used").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Event").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Description").Width = 150;
                        Utils.getColumnHeader(ucai.lvAIMap, "CellNo").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Widget").Width = 0;
                        //Namrata: 21/10/2019
                        Utils.getColumnHeader(ucai.lvAIMap, "Unit").Width = 0;
                        Utils.getColumnHeader(ucai.lvAIMap, "Key").Width = 0;
                        //Namrata: 12/11/2019
                        Utils.getColumnHeader(ucai.lvAIMap, "Event Class").Width = 100;
                        Utils.getColumnHeader(ucai.lvAIMap, "Variation").Width = 100;
                        Utils.getColumnHeader(ucai.lvAIMap, "Event Variation").Width = 100;
                        break;

                    default:
                        Utils.getColumnHeader(ucai.lvAIMap, "Event").Width = 0;
                        break;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        public XmlNode exportXMLnode()
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
            rootNode = xmlDoc.CreateElement("AIConfiguration");//rootNode = xmlDoc.CreateElement(rnName);
            xmlDoc.AppendChild(rootNode);
            foreach (AI ai in aiList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(ai.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }
            return rootNode;
        }
        public XmlNode exportMapXMLnode(String slaveID)
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
            rootNode = xmlDoc.CreateElement("AIMap");
            xmlDoc.AppendChild(rootNode);
            List<AIMap> slaveAIMapList;
            if (!slavesAIMapList.TryGetValue(slaveID, out slaveAIMapList))
            {
                Console.WriteLine("##### Slave entries for {0} does not exists", slaveID);
                return rootNode;
            }
            foreach (AIMap aimn in slaveAIMapList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(aimn.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }
            return rootNode;
        }
        public string exportINI(string slaveNum, string slaveID, string element, ref int ctr)
        {
            string iniData = "";
            List<AIMap> slaveAIMapList;
            if (!slavesAIMapList.TryGetValue(slaveID, out slaveAIMapList))
            {
                Console.WriteLine("AI INI: ##### Slave entries for {0} does not exists", slaveID);
                return iniData;
            }
            if (element == "DeadBandAI")
            {
                foreach (AIMap aimn in slaveAIMapList)
                {
                    iniData += "DeadBand_" + ctr++ + "=" + Utils.GetDataTypeShortNotation(aimn.DataType) + "," + aimn.ReportingIndex + "," + aimn.Deadband + Environment.NewLine;
                }
            }
            else if (element == "AI")
            {
                foreach (AIMap aimn in slaveAIMapList)
                {
                    int ri;
                    try
                    {
                        ri = Int32.Parse(aimn.ReportingIndex);
                    }
                    catch (System.FormatException)
                    {
                        ri = 0;
                    }
                    if (slaveAIMapList.Where(x => x.ReportingIndex == ri.ToString()).Select(x => x).Count() > 1) //Ajay: 10/01/2019
                    {
                        MessageBox.Show("Duplicate Reporting Index (" + aimn.ReportingIndex + ") found of AI No " + aimn.AINo, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        iniData += "AI_" + ctr++ + "=" + Utils.GenerateIndex("AI", Utils.GetDataTypeIndex(aimn.DataType), ri).ToString() + Environment.NewLine;
                    }
                }
            }
            return iniData;
        }
        public void changeMDSequence(int oMDNo, int nMDNo)
        {
            string strRoutineName = "AIConfiguration:changeMDSequence";
            try
            {
                if (oMDNo == nMDNo) return;
                foreach (AI ain in aiList)
                {
                    if (ain.ResponseType == "MDSlindingWindow" && ain.Index == oMDNo.ToString() && ain.SubIndex == "0")
                    {
                        ain.Index = nMDNo.ToString();
                        if (ain.Description == "MDSlindingWindow_" + oMDNo) ain.Description = "MDSlindingWindow_" + nMDNo;
                        break;
                    }
                }
                foreach (AI ain in aiList)
                {
                    if (ain.ResponseType == "MDWindow" && ain.Index == oMDNo.ToString() && ain.SubIndex == "0")
                    {
                        ain.Index = nMDNo.ToString();
                        if (ain.Description == "MDWindow_" + oMDNo) ain.Description = "MDWindow_" + nMDNo;
                        break;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void changeDPSequence(int oDPNo, int nDPNo)
        {
            string strRoutineName = "AIConfiguration:changeDPSequence";
            try
            {
                if (oDPNo == nDPNo) return;
                foreach (AI ain in aiList)
                {
                    if (ain.ResponseType == "DerivedParam" && ain.Index == oDPNo.ToString() && ain.SubIndex == "0")
                    {
                        ain.Index = nDPNo.ToString();
                        if (ain.Description == "DerivedParam_" + oDPNo) ain.Description = "DerivedParam_" + nDPNo;
                        break;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void regenerateAISequence()
        {
            string strRoutineName = "AIConfiguration:regenerateAISequence";
            try
            {
                foreach (AI ain in aiList)
                {
                    int oAINo = Int32.Parse(ain.AINo);
                    //Namrata: 30/10/2017
                    int nAINo = Globals.AINo++;
                    ain.AINo = nAINo.ToString();
                    //Now Change in Map...
                    foreach (KeyValuePair<string, List<AIMap>> maps in slavesAIMapList)
                    {
                        //Ajay: 10/01/2019 Commented
                        //List<AIMap> saimList = maps.Value;
                        //foreach (AIMap aim in saimList)
                        //{
                        //    if (aim.AINo == oAINo.ToString() && !aim.IsReindexed)
                        //    {
                        //        //Ajay: 30/07/2018 if same AI mapped again it should take same AI no on reindex. 
                        //        //aim.AINo = nAINo.ToString();
                        //        //aim.IsReindexed = true;
                        //        saimList.Where(x => x.AINo == oAINo.ToString()).ToList().ForEach(x => { x.AINo = nAINo.ToString(); x.IsReindexed = true; });
                        //        break;
                        //    }
                        //}
                        //Ajay: 10/01/2019 Reindexing issues reported by Aditya K. mail dtd. 27-12-2018
                        maps.Value.OfType<AIMap>().Where(x => x.AINo == oAINo.ToString() && !x.IsReindexed).ToList().ForEach(x => { x.AINo = nAINo.ToString(); x.IsReindexed = true; });
                    }
                    Utils.getOpenProPlusHandle().getParameterLoadConfiguration().ChangeAISequence(oAINo, nAINo);  //Now change in Parameter Load nodes...
                }
                foreach (KeyValuePair<string, List<AIMap>> maps in slavesAIMapList) //Reset reindex status, for next use...
                {
                    List<AIMap> saimList = maps.Value;
                    foreach (AIMap aim in saimList)
                    {
                        aim.IsReindexed = false;
                    }
                }
                refreshList();
                refreshCurrentMapList();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public int GetReportingIndex(string slaveNum, string slaveID, int value)
        {
            int ret = 0;
            List<AIMap> slaveAIMapList;
            if (!slavesAIMapList.TryGetValue(slaveID, out slaveAIMapList))
            {
                return ret;
            }
            foreach (AIMap aim in slaveAIMapList)
            {
                if (aim.AINo == value.ToString()) return Int32.Parse(aim.ReportingIndex);
            }
            return ret;
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("AI_"))
            {
                CheckIEC104SlaveStatusChanges();  //If a IEC104 slave added/deleted, reflect in UI as well as objects.
                CheckMODBUSSlaveStatusChanges();  //If a MODBUS slave added/deleted, reflect in UI as well as objects.
                CheckIEC61850laveStatusChanges(); //If a IEC61850 slave added/deleted, reflect in UI as well as objects.
                CheckIEC101SlaveStatusChanges(); //If a IEC101 slave added/deleted, reflect in UI as well as objects.
                //Ajay: 05/07/2018
                CheckSPORTSlaveStatusChanges(); //If a SPORT slave added/deleted, reflect in UI as well as objects.
                CheckMQTTSlaveStatusChanges();
                CheckSMSSlaveStatusChanges();
                CheckGDisplaySlaveStatusChanges();
                CheckDNPSlaveStatusChanges();
                ShowHideSlaveColumns();
                //ShowHideSlaveColumnsSPORT();
                return ucai;
            }
            return null;
        }
        public void parseAICNode(XmlNode aicNode, bool imported)
        {
            string strRoutineName = "AIConfiguration:parseAICNode";
            try
            {
                if (aicNode == null)
                {
                    rnName = "AIConfiguration";
                    return;
                }
                rnName = aicNode.Name;  //First set root node name...
                if (aicNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = aicNode.Value;
                }
                foreach (XmlNode node in aicNode)
                {
                    if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                    aiList.Add(new AI(node, masterType, masterNo, IEDNo, imported));
                }
                refreshList();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void parseAIMNode(string slaveNum, string slaveID, XmlNode aimNode)
        {
            string strRoutineName = "AIConfiguration:parseAIMNode";
            try
            {
                //Task thDetails = new Task(() => CreateNewSlave(slaveNum, slaveID, aimNode));
                //thDetails.Start();
                CreateNewSlave(slaveNum, slaveID, aimNode);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Namrata:27/7/2017
        public int getAIMapCount()
        {
            int ctr = 0;
            List<AIMap> saimList;
            if (!slavesAIMapList.TryGetValue(currentSlave, out saimList))
            {
                refreshMapList(null);
            }
            else
            {
                refreshMapList(saimList);
            }
            if (saimList == null)
            {
                return 0;
            }
            else
            {
                foreach (AIMap asaa in saimList)
                {
                    if (asaa.IsNodeComment) continue;
                    ctr++;
                }
            }
            return ctr;
        }
        public int getCount()
        {
            int ctr = 0;
            foreach (AI aiNode in aiList)
            {
                if (aiNode.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }
        public List<AI> getAIs()
        {
            return aiList;
        }
        public bool removeAI(string responseType, int Idx, int subIdx)
        {
            bool removed = false;
            for (int i = 0; i < aiList.Count; i++)
            {
                if (aiList[i].IsNodeComment) continue;
                AI tmp = aiList[i];
                if (tmp.Index == Idx.ToString() && tmp.SubIndex == subIdx.ToString() && tmp.ResponseType == responseType)
                {
                    aiList.RemoveAt(i);
                    removed = true;
                    break;
                }
            }
            return removed;
        }
        public List<AIMap> getSlaveAIMaps(string slaveID)
        {
            List<AIMap> slaveAIMapList;
            slavesAIMapList.TryGetValue(slaveID, out slaveAIMapList);
            return slaveAIMapList;
        }

        #region All Masters OnLoad Events
        //Ajay: 30/08/2018
        public void IEC61850Client_MasterOnLoadEvents()
        {
            string strRoutineName = "AIConfiguration:IEC61850Client_MasterOnLoadEvents";
            try
            {
                foreach (Control c in ucai.grpAI.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucai.lblAINo.Visible = ucai.txtAINo.Visible = true;
                ucai.txtAINo.Enabled = false;
                ucai.lblAINo.Location = new Point(15, 35);
                ucai.txtAINo.Location = new Point(102, 30);
                ucai.txtAINo.Size = new Size(300, 21);

                ucai.lblIEDName.Visible = ucai.cmbIEDName.Visible = true;
                ucai.lblIEDName.Location = new Point(15, 60);
                ucai.cmbIEDName.Location = new Point(102, 55);
                ucai.cmbIEDName.Size = new Size(300, 21);

                ucai.lbl61850ResponseType.Visible = ucai.cmb61850ResponseType.Visible = true;
                ucai.lbl61850ResponseType.Location = new Point(15, 85);
                ucai.cmb61850ResponseType.Location = new Point(102, 80);
                ucai.cmb61850ResponseType.Size = new Size(300, 21);

                ucai.lblFC.Visible = ucai.cmbFC.Visible = true;
                //ucai.cmbFC.Enabled = false;
                ucai.lblFC.Location = new Point(15, 110);
                ucai.cmbFC.Location = new Point(102, 105);
                ucai.cmbFC.Size = new Size(300, 21);

                ucai.lbl61850Index.Visible = ucai.cmb61850Index.Visible = false;
                ucai.lbl61850Index.Location = new Point(15, 135);
                ucai.cmb61850Index.Location = new Point(102, 130);
                ucai.cmb61850Index.Size = new Size(300, 21);

                ucai.lbl61850Index.Visible = ucai.ChkIEC61850Index.Visible = true;
                ucai.lbl61850Index.Location = new Point(15, 135);
                ucai.ChkIEC61850Index.Location = new Point(102, 130);
                ucai.ChkIEC61850Index.Size = new Size(300, 21);

                ucai.lblSubIndex.Visible = ucai.txtSubIndex.Visible = true;
                ucai.lblSubIndex.Location = new Point(15, 160);
                ucai.txtSubIndex.Location = new Point(102, 155);
                ucai.txtSubIndex.Size = new Size(300, 21);

                ucai.lblMultiplier.Visible = ucai.txtMultiplier.Visible = true;
                ucai.lblMultiplier.Location = new Point(15, 185);
                ucai.txtMultiplier.Location = new Point(102, 180);
                ucai.txtMultiplier.Size = new Size(300, 21);

                ucai.lblConstant.Visible = ucai.txtConstant.Visible = true;
                ucai.lblConstant.Location = new Point(15, 210);
                ucai.txtConstant.Location = new Point(102, 205);
                ucai.txtConstant.Size = new Size(300, 21);

                ucai.lblDesc.Visible = ucai.txtDescription.Visible = true;
                ucai.lblDesc.Location = new Point(15, 235);
                ucai.txtDescription.Location = new Point(102, 230);
                ucai.txtDescription.Size = new Size(300, 21);

                ucai.lblAutoMap.Visible = ucai.txtAIAutoMapRange.Visible = false;
                ucai.lblAutoMap.Location = new Point(15, 260);
                ucai.txtAIAutoMapRange.Location = new Point(102, 255);
                ucai.txtAIAutoMapRange.Size = new Size(300, 21);

                ucai.chkbxEventEnable.Visible = true;
                ucai.chkbxEventEnable.Checked = false;
                ucai.chkbxEventEnable.Location = new Point(18, 263);

                ucai.btnDone.Visible = ucai.btnCancel.Visible = true;
                ucai.btnDone.Location = new Point(135, 270);
                ucai.btnCancel.Location = new Point(235, 270);

                ucai.grpAI.Height = 310;// 355;
                ucai.grpAI.Width = 420;
                ucai.grpAI.Location = new Point(172, 52);
                ucai.pbHdr.Width = 510;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 30/08/2018
        public void IEC103_ADR_MODBUS_MasterOnLoadEvents()
        {
            //Ajay: 30/08/2018
            string strRoutineName = "AIConfiguration: IEC103_ADR_MODBUS_MasterOnLoadEvents";
            try
            {
                foreach (Control c in ucai.grpAI.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }
                ucai.txtAINo.Size = new Size(220, 20);
                ucai.cmbResponseType.Size = new Size(220, 20);
                ucai.txtIndex.Size = new Size(220, 20);
                ucai.txtSubIndex.Size = new Size(220, 20);
                ucai.txtMultiplier.Size = new Size(220, 20);
                ucai.txtConstant.Size = new Size(220, 20);
                ucai.txtDescription.Size = new Size(220, 20);
                ucai.txtAIAutoMapRange.Size = new Size(220, 20);

                //lblAINo & txtAINo
                ucai.lblAINo.Visible = ucai.txtAINo.Visible = true;
                ucai.txtAINo.Enabled = false;
                ucai.lblAINo.Location = new Point(15, 35);
                ucai.txtAINo.Location = new Point(102, 30);
                ucai.cmbDataType.Size = new Size(220, 20);

                //lblResponseType & cmbResponseType
                ucai.lblResponseType.Visible = ucai.cmbResponseType.Visible = true;
                ucai.lblResponseType.Location = new Point(15, 60);
                ucai.cmbResponseType.Location = new Point(102, 55);

                //lblIndex & lblIndex
                ucai.lblIndex.Visible = ucai.txtIndex.Visible = true;
                ucai.lblIndex.Location = new Point(15, 85);
                ucai.txtIndex.Location = new Point(102, 80);

                //lblSubIndex & txtSubIndex
                ucai.lblSubIndex.Visible = ucai.txtSubIndex.Visible = true;
                ucai.lblSubIndex.Location = new Point(15, 110);
                ucai.txtSubIndex.Location = new Point(102, 105);


                ucai.lblDataType.Visible = ucai.cmbDataType.Visible = true;
                ucai.lblDataType.Location = new Point(15, 135);
                ucai.cmbDataType.Location = new Point(102, 130);


                ucai.lblMultiplier.Visible = ucai.txtMultiplier.Visible = true;
                ucai.lblMultiplier.Location = new Point(15, 160);
                ucai.txtMultiplier.Location = new Point(102, 155);


                ucai.lblConstant.Visible = ucai.txtConstant.Visible = true;
                ucai.lblConstant.Location = new Point(15, 185);
                ucai.txtConstant.Location = new Point(102, 180);


                ucai.lblDesc.Visible = ucai.txtDescription.Visible = true;
                ucai.lblDesc.Location = new Point(15, 210);
                ucai.txtDescription.Location = new Point(102, 205);


                ucai.lblAutoMap.Visible = ucai.txtAIAutoMapRange.Visible = true;
                ucai.lblAutoMap.Location = new Point(15, 235);
                ucai.txtAIAutoMapRange.Location = new Point(102, 230);


                ucai.chkbxEventEnable.Visible = true;
                ucai.chkbxEventEnable.Checked = false;
                ucai.chkbxEventEnable.Location = new Point(102, 255);

                ucai.btnDone.Visible = ucai.btnCancel.Visible = true;
                ucai.btnDone.Location = new Point(105, 275);
                ucai.btnCancel.Location = new Point(220, 275);

                ucai.grpAI.Size = new Size(350, 310);
                ucai.pbHdr.Size = new Size(350, 22);
                ucai.grpAI.Location = new Point(172, 52);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void IEC101_MasterOnLoadEvents()
        {
            //Ajay: 12/11/2018
            string strRoutineName = "AIConfiguration: IEC101_MasterOnLoadEvents";
            try
            {
                foreach (Control c in ucai.grpAI.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucai.txtAINo.Size = new Size(220, 20);
                ucai.cmbResponseType.Size = new Size(220, 20);
                ucai.txtIndex.Size = new Size(220, 20);
                ucai.txtSubIndex.Size = new Size(220, 20);
                ucai.txtMultiplier.Size = new Size(220, 20);
                ucai.txtConstant.Size = new Size(220, 20);
                ucai.txtDescription.Size = new Size(220, 20);
                ucai.txtAIAutoMapRange.Size = new Size(220, 20);

                //lblAINo & txtAINo
                ucai.lblAINo.Visible = ucai.txtAINo.Visible = true;
                ucai.txtAINo.Enabled = false;
                ucai.lblAINo.Location = new Point(15, 35);
                ucai.txtAINo.Location = new Point(102, 30);
                ucai.txtAINo.Size = new Size(220, 20);

                ucai.lblResponseType.Visible = ucai.cmbResponseType.Visible = true;
                ucai.lblResponseType.Location = new Point(15, 60);
                ucai.cmbResponseType.Location = new Point(102, 55);
                ucai.cmbResponseType.Size = new Size(220, 20);

                ucai.lblIndex.Visible = ucai.txtIndex.Visible = true;
                ucai.lblIndex.Location = new Point(15, 85);
                ucai.txtIndex.Location = new Point(102, 80);
                ucai.txtIndex.Size = new Size(220, 20);

                ucai.lblSubIndex.Visible = ucai.txtSubIndex.Visible = true;
                ucai.lblSubIndex.Location = new Point(15, 110);
                ucai.txtSubIndex.Location = new Point(102, 105);
                ucai.txtSubIndex.Size = new Size(220, 20);

                ucai.lblDataType.Visible = ucai.cmbDataType.Visible = true;
                ucai.lblDataType.Location = new Point(15, 135);
                ucai.cmbDataType.Location = new Point(102, 130);
                ucai.cmbDataType.Size = new Size(220, 20);

                ucai.lblMultiplier.Visible = ucai.txtMultiplier.Visible = true;
                ucai.lblMultiplier.Location = new Point(15, 160);
                ucai.txtMultiplier.Location = new Point(102, 155);
                ucai.txtMultiplier.Size = new Size(220, 20);

                ucai.lblConstant.Visible = ucai.txtConstant.Visible = true;
                ucai.lblConstant.Location = new Point(15, 185);
                ucai.txtConstant.Location = new Point(102, 180);
                ucai.txtConstant.Size = new Size(220, 20);

                ucai.lblDesc.Visible = ucai.txtDescription.Visible = true;
                ucai.lblDesc.Location = new Point(15, 210);
                ucai.txtDescription.Location = new Point(102, 205);
                ucai.txtDescription.Size = new Size(220, 20);

                ucai.lblAutoMap.Visible = ucai.txtAIAutoMapRange.Visible = true;
                ucai.lblAutoMap.Location = new Point(15, 235);
                ucai.txtAIAutoMapRange.Location = new Point(102, 230);
                ucai.txtAIAutoMapRange.Size = new Size(220, 20);

                ucai.chkbxEventEnable.Visible = true;
                ucai.chkbxEventEnable.Checked = false;
                ucai.chkbxEventEnable.Location = new Point(102, 255);

                ucai.btnDone.Visible = ucai.btnCancel.Visible = true;
                ucai.btnDone.Location = new Point(105, 275);
                ucai.btnCancel.Location = new Point(220, 275);

                ucai.grpAI.Size = new Size(350, 310);
                ucai.pbHdr.Size = new Size(350, 22);
                ucai.grpAI.Location = new Point(172, 52);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 30/08/2018
        public void IEC104_MasterOnLoadEvents()
        {
            //Ajay: 30/08/2018
            string strRoutineName = "AIConfiguration: IEC104_MasterOnLoadEvents";
            try
            {
                foreach (Control c in ucai.grpAI.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }
                ucai.txtAINo.Size = new Size(220, 20);
                ucai.cmbResponseType.Size = new Size(220, 20);
                ucai.txtIndex.Size = new Size(220, 20);
                ucai.txtSubIndex.Size = new Size(220, 20);
                ucai.txtMultiplier.Size = new Size(220, 20);
                ucai.txtConstant.Size = new Size(220, 20);
                ucai.txtDescription.Size = new Size(220, 20);
                ucai.txtAIAutoMapRange.Size = new Size(220, 20);

                ucai.lblAINo.Visible = ucai.txtAINo.Visible = true;
                ucai.txtAINo.Enabled = false;
                ucai.lblAINo.Location = new Point(15, 35);
                ucai.txtAINo.Location = new Point(102, 30);

                ucai.lblResponseType.Visible = ucai.cmbResponseType.Visible = true;
                ucai.lblResponseType.Location = new Point(15, 60);
                ucai.cmbResponseType.Location = new Point(102, 55);

                ucai.lblIndex.Visible = ucai.txtIndex.Visible = true;
                ucai.lblIndex.Location = new Point(15, 85);
                ucai.txtIndex.Location = new Point(102, 80);

                ucai.lblSubIndex.Visible = ucai.txtSubIndex.Visible = true;
                ucai.lblSubIndex.Location = new Point(15, 110);
                ucai.txtSubIndex.Location = new Point(102, 105);

                ucai.lblMultiplier.Visible = ucai.txtMultiplier.Visible = true;
                ucai.lblMultiplier.Location = new Point(15, 135);
                ucai.txtMultiplier.Location = new Point(102, 130);

                ucai.lblConstant.Visible = ucai.txtConstant.Visible = true;
                ucai.lblConstant.Location = new Point(15, 160);
                ucai.txtConstant.Location = new Point(102, 155);

                ucai.lblDesc.Visible = ucai.txtDescription.Visible = true;
                ucai.lblDesc.Location = new Point(15, 185);
                ucai.txtDescription.Location = new Point(102, 180);

                ucai.lblAutoMap.Visible = ucai.txtAIAutoMapRange.Visible = true;
                ucai.lblAutoMap.Location = new Point(15, 210);
                ucai.txtAIAutoMapRange.Location = new Point(102, 205);

                ucai.chkbxEventEnable.Visible = true;
                ucai.chkbxEventEnable.Checked = false;
                //ucai.chkbxEventEnable.Location = new Point(15, 235);

                //ucai.btnDone.Visible = ucai.btnCancel.Visible = true;
                //ucai.btnDone.Location = new Point(105, 250);
                //ucai.btnCancel.Location = new Point(220, 250);

                //ucai.grpAI.Height = 290;
                //ucai.grpAI.Width = 320;
                //ucai.grpAI.Location = new Point(172, 52);


                ucai.chkbxEventEnable.Location = new Point(102, 233);

                ucai.btnDone.Visible = ucai.btnCancel.Visible = true;
                ucai.btnDone.Location = new Point(105, 252);
                ucai.btnCancel.Location = new Point(220, 252);

                ucai.grpAI.Size = new Size(350, 290);
                ucai.pbHdr.Size = new Size(350, 22);
                ucai.grpAI.Location = new Point(172, 52);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Namrata:29/01/2019
        public void SPORT_MasterOnLoadEvents()
        {
            //Ajay: 30/08/2018
            string strRoutineName = "AIConfiguration: SPORT_OnLoad";
            try
            {
                foreach (Control c in ucai.grpAI.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }
                ucai.txtAINo.Size = new Size(220, 20);
                ucai.cmbResponseType.Size = new Size(220, 20);
                ucai.txtIndex.Size = new Size(220, 20);
                ucai.txtSubIndex.Size = new Size(220, 20);
                ucai.txtMultiplier.Size = new Size(220, 20);
                ucai.txtConstant.Size = new Size(220, 20);
                ucai.txtDescription.Size = new Size(220, 20);
                ucai.txtAIAutoMapRange.Size = new Size(220, 20);

                //Label AINo. And Text AINo.
                ucai.lblAINo.Visible = ucai.txtAINo.Visible = true;
                ucai.txtAINo.Enabled = false;
                ucai.lblAINo.Location = new Point(15, 35);
                ucai.txtAINo.Location = new Point(102, 30);

                //Label ResponseType and Combobox Response Type
                ucai.lblResponseType.Visible = ucai.cmbResponseType.Visible = true;
                ucai.lblResponseType.Location = new Point(15, 60);
                ucai.cmbResponseType.Location = new Point(102, 55);

                //Label Index and Text Index
                ucai.lblIndex.Visible = ucai.txtIndex.Visible = true;
                ucai.lblIndex.Location = new Point(15, 85);
                ucai.txtIndex.Location = new Point(102, 80);

                //Label Subindex and Text Subindex
                ucai.lblSubIndex.Visible = ucai.txtSubIndex.Visible = true;
                ucai.lblSubIndex.Location = new Point(15, 110);
                ucai.txtSubIndex.Location = new Point(102, 105);


                //label Deadband And Text Deadband
                ucai.lblDeadband.Visible = ucai.txtDeadband.Visible = true;
                ucai.lblDeadband.Location = new Point(15, 135);
                ucai.txtDeadband.Location = new Point(102, 130);
                ucai.txtDeadband.Size = new Size(220, 20);

                //Label Multiplier and Text Multiplier
                ucai.lblMultiplier.Visible = ucai.txtMultiplier.Visible = true;
                ucai.lblMultiplier.Location = new Point(15, 160);
                ucai.txtMultiplier.Location = new Point(102, 155);

                //Label Constant and Text Constant
                ucai.lblConstant.Visible = ucai.txtConstant.Visible = true;
                ucai.lblConstant.Location = new Point(15, 185);
                ucai.txtConstant.Location = new Point(102, 180);

                //Label HighLimit And Text High Limit
                ucai.lblHighLimit.Visible = ucai.txtHighLimit.Visible = true;
                ucai.lblHighLimit.Location = new Point(15, 210);
                ucai.txtHighLimit.Location = new Point(102, 205);
                ucai.txtHighLimit.Size = new Size(220, 20);

                //Label LowLimit And Text Low Limit
                ucai.lblLowLimit.Visible = ucai.txtLowLimit.Visible = true;
                ucai.lblLowLimit.Location = new Point(15, 235);
                ucai.txtLowLimit.Location = new Point(102, 230);
                ucai.txtLowLimit.Size = new Size(220, 20);

                //Label DONo. And Text DONo.
                ucai.lblDONo.Visible = ucai.CmbDONo.Visible = true;
                ucai.lblDONo.Location = new Point(15, 260);
                ucai.CmbDONo.Location = new Point(102, 255);
                ucai.CmbDONo.Size = new Size(220, 20);

                //Label Description and Text Description
                ucai.lblDesc.Visible = ucai.txtDescription.Visible = true;
                ucai.lblDesc.Location = new Point(15, 285);
                ucai.txtDescription.Location = new Point(102, 280);

                //Label Automap and text Automap
                ucai.lblAutoMap.Visible = ucai.txtAIAutoMapRange.Visible = true;
                ucai.lblAutoMap.Location = new Point(15, 310);
                ucai.txtAIAutoMapRange.Location = new Point(102, 305);

                //Checkbox Event Enable
                ucai.chkbxEventEnable.Visible = true;
                ucai.chkbxEventEnable.Checked = false;
                ucai.chkbxEventEnable.Location = new Point(102, 335);

                //Button Done and Button Cancel
                ucai.btnDone.Visible = ucai.btnCancel.Visible = true;
                ucai.btnDone.Location = new Point(105, 355);
                ucai.btnCancel.Location = new Point(220, 355);

                //GroupBox AI
                ucai.grpAI.Size = new Size(350, 400);
                ucai.pbHdr.Size = new Size(350, 22);
                ucai.grpAI.Location = new Point(172, 52);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 31/07/2018 
        public void LoadProfile_MasterOnLoad()
        {
            string strRoutineName = "AIConfiguration: LoadProfile_MasterOnLoad";
            try
            {
                foreach (Control c in ucai.grpAI.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }
                ucai.txtAINo.Size = new Size(220, 20);
                ucai.cmbResponseType.Size = new Size(220, 20);
                ucai.txtIndex.Size = new Size(220, 20);
                ucai.txtSubIndex.Size = new Size(220, 20);
                ucai.txtMultiplier.Size = new Size(220, 20);
                ucai.txtConstant.Size = new Size(220, 20);
                ucai.txtDescription.Size = new Size(220, 20);
                ucai.txtAIAutoMapRange.Size = new Size(220, 20);

                ucai.lblAINo.Visible = ucai.txtAINo.Visible = true;
                ucai.lblAINo.Location = new Point(15, 35);
                ucai.txtAINo.Location = new Point(102, 30);

                ucai.lblName.Visible = ucai.cmbName.Visible = true;
                ucai.lblName.Location = new Point(15, 60);
                ucai.cmbName.Location = new Point(102, 55);
                ucai.cmbName.Size = new Size(220, 20);

                ucai.lblAI1.Visible = ucai.cmbAI1.Visible = true;
                ucai.lblAI1.Location = new Point(15, 85);
                ucai.cmbAI1.Location = new Point(102, 80);
                ucai.cmbAI1.Size = new Size(220, 20);

                ucai.lblAI2.Visible = ucai.cmbAI2.Visible = true;
                ucai.lblAI2.Location = new Point(15, 110);
                ucai.cmbAI2.Location = new Point(102, 105);
                ucai.cmbAI2.Size = new Size(220, 20);

                ucai.lblAI3.Visible = ucai.cmbAI3.Visible = true;
                ucai.lblAI3.Location = new Point(15, 135);
                ucai.cmbAI3.Location = new Point(102, 130);
                ucai.cmbAI3.Size = new Size(220, 20);

                ucai.lblDesc.Visible = ucai.txtDescription.Visible = true;
                ucai.lblDesc.Location = new Point(15, 160);
                ucai.txtDescription.Location = new Point(102, 155);

                ucai.chkbxEventEnable.Visible = true;
                ucai.chkbxEventEnable.Checked = false;
                ucai.chkbxEventEnable.Location = new Point(102, 185);

                //Ajay: 19/09/2018 LogEnable remove
                //ucai.chkbxLogEnable.Visible = true;
                //ucai.chkbxLogEnable.Checked = false;
                //ucai.chkbxLogEnable.Location = new Point(120, 190);
                ucai.chkbxLogEnable.Visible = false;

                ucai.btnDone.Visible = ucai.btnCancel.Visible = ucai.btnVerify.Visible = true;
                ucai.btnVerify.Location = new Point(15, 210);
                ucai.btnDone.Location = new Point(120, 210);
                ucai.btnCancel.Location = new Point(223, 210);

                //GroupBox AI
                ucai.grpAI.Size = new Size(350, 250);
                ucai.pbHdr.Size = new Size(350, 22);
                ucai.grpAI.Location = new Point(172, 52);
                ucai.lvAIlistDoubleClick += new System.EventHandler(this.lvAIlist_DoubleClick);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Virtual_MasterOnLoadEvents()
        {
            string strRoutineName = "AIConfiguration: Virtual_MasterOnLoadEvents";
            try
            {
                ucai.btnAdd.Enabled = false;
                ucai.btnDelete.Enabled = false;
                IEC103_ADR_MODBUS_MasterOnLoadEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion All Masters OnLoad Events

        #region All Masters OnDoubleClick Events
        public void IEC101_MasterOnDoubleClickEvents()
        {
            string strRoutineName = "AIConfiguration: IEC101_MasterOnDoubleClickEvents";
            try
            {
                foreach (Control c in ucai.grpAI.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }
                ucai.txtAINo.Size = new Size(220, 20);

                //lblAINo & txtAINo
                ucai.lblAINo.Visible = ucai.txtAINo.Visible = true;
                ucai.txtAINo.Enabled = false;
                ucai.lblAINo.Location = new Point(15, 35);
                ucai.txtAINo.Location = new Point(102, 30);

                //lblResponseType & cmbResponseType
                ucai.lblResponseType.Visible = ucai.cmbResponseType.Visible = true;
                ucai.lblResponseType.Location = new Point(15, 60);
                ucai.cmbResponseType.Location = new Point(102, 55);
                ucai.cmbResponseType.Size = new Size(220, 20);

                //lblIndex & txtIndex
                ucai.lblIndex.Visible = ucai.txtIndex.Visible = true;
                ucai.lblIndex.Location = new Point(15, 85);
                ucai.txtIndex.Location = new Point(102, 80);
                ucai.txtIndex.Size = new Size(220, 20);

                //lblSubIndex & txtSubIndex
                ucai.lblSubIndex.Visible = ucai.txtSubIndex.Visible = true;
                ucai.lblSubIndex.Location = new Point(15, 110);
                ucai.txtSubIndex.Location = new Point(102, 105);
                ucai.txtSubIndex.Size = new Size(220, 20);

                //lblDataType & cmbDataType
                ucai.lblDataType.Visible = ucai.cmbDataType.Visible = true;
                ucai.lblDataType.Location = new Point(15, 135);
                ucai.cmbDataType.Location = new Point(102, 130);
                ucai.cmbDataType.Size = new Size(220, 20);

                //lblMultiplier & txtMultiplier
                ucai.lblMultiplier.Visible = ucai.txtMultiplier.Visible = true;
                ucai.lblMultiplier.Location = new Point(15, 160);
                ucai.txtMultiplier.Location = new Point(102, 155);
                ucai.txtMultiplier.Size = new Size(220, 20);

                //lblConstant & txtConstant
                ucai.lblConstant.Visible = ucai.txtConstant.Visible = true;
                ucai.lblConstant.Location = new Point(15, 185);
                ucai.txtConstant.Location = new Point(102, 180);
                ucai.txtConstant.Size = new Size(220, 20);

                //lblDesc & txtDescription
                ucai.lblDesc.Visible = ucai.txtDescription.Visible = true;
                ucai.lblDesc.Location = new Point(15, 210);
                ucai.txtDescription.Location = new Point(102, 205);
                ucai.txtDescription.Size = new Size(220, 20);

                //chkbxEventEnable
                ucai.chkbxEventEnable.Visible = true;
                ucai.chkbxEventEnable.Checked = false;
                ucai.chkbxEventEnable.Location = new Point(102, 230);

                //btnDone & btnCancel
                ucai.btnDone.Visible = ucai.btnCancel.Visible = true;
                ucai.btnDone.Location = new Point(102, 253);
                ucai.btnCancel.Location = new Point(205, 253);

                //btnFirst & btnPrev & btnNext
                ucai.btnFirst.Visible = ucai.btnPrev.Visible = ucai.btnNext.Visible = ucai.btnLast.Visible = true;
                ucai.btnFirst.Location = new Point(13, 285);
                ucai.btnPrev.Location = new Point(88, 285);
                ucai.btnNext.Location = new Point(163, 285);
                ucai.btnLast.Location = new Point(238, 285);

                //grpAI
                ucai.grpAI.Size = new Size(350, 310);
                ucai.grpAI.Location = new Point(172, 52);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void IEC103_ADR_MODBUS_MasterOnDoubleClickEvents()
        {
            string strRoutineName = "AIConfiguration: IEC103_ADR_MODBUS_MasterOnDoubleClickEvents";
            try
            {
                foreach (Control c in ucai.grpAI.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }
                ucai.txtAIAutoMapRange.Text = "0"; //Namrata: 10/09/2017
                ucai.txtAIAutoMapRange.Text = "0"; //Namrata: 10/09/2017
                ucai.txtAINo.Size = new Size(220, 20);
                ucai.cmbResponseType.Size = new Size(220, 20);
                ucai.txtIndex.Size = new Size(220, 20);
                ucai.txtSubIndex.Size = new Size(220, 20);
                ucai.txtMultiplier.Size = new Size(220, 20);
                ucai.txtConstant.Size = new Size(220, 20);
                ucai.txtDescription.Size = new Size(220, 20);
                ucai.txtAIAutoMapRange.Size = new Size(220, 20);


                ucai.lblAINo.Visible = ucai.txtAINo.Visible = true;
                ucai.txtAINo.Enabled = false;
                ucai.lblAINo.Location = new Point(15, 35);
                ucai.txtAINo.Location = new Point(102, 30);

                ucai.lblResponseType.Visible = ucai.cmbResponseType.Visible = true;
                ucai.lblResponseType.Location = new Point(15, 60);
                ucai.cmbResponseType.Location = new Point(102, 55);

                ucai.lblIndex.Visible = ucai.txtIndex.Visible = true;
                ucai.lblIndex.Location = new Point(15, 85);
                ucai.txtIndex.Location = new Point(102, 80);

                ucai.lblSubIndex.Visible = ucai.txtSubIndex.Visible = true;
                ucai.lblSubIndex.Location = new Point(15, 110);
                ucai.txtSubIndex.Location = new Point(102, 105);

                ucai.lblDataType.Visible = ucai.cmbDataType.Visible = true;
                ucai.lblDataType.Location = new Point(15, 135);
                ucai.cmbDataType.Location = new Point(102, 130);

                ucai.lblMultiplier.Visible = ucai.txtMultiplier.Visible = true;
                ucai.lblMultiplier.Location = new Point(15, 160);
                ucai.txtMultiplier.Location = new Point(102, 155);

                ucai.lblConstant.Visible = ucai.txtConstant.Visible = true;
                ucai.lblConstant.Location = new Point(15, 185);
                ucai.txtConstant.Location = new Point(102, 180);

                ucai.lblDesc.Visible = ucai.txtDescription.Visible = true;
                ucai.lblDesc.Location = new Point(15, 210);
                ucai.txtDescription.Location = new Point(102, 205);

                ucai.chkbxEventEnable.Visible = true;
                ucai.chkbxEventEnable.Checked = false;
                ucai.chkbxEventEnable.Location = new Point(102, 235);

                ucai.btnDone.Visible = ucai.btnCancel.Visible = true;
                ucai.btnDone.Location = new Point(102, 260);
                ucai.btnCancel.Location = new Point(205, 260);

                ucai.btnFirst.Visible = ucai.btnPrev.Visible = ucai.btnNext.Visible = ucai.btnLast.Visible = true;
                ucai.btnFirst.Location = new Point(13, 295);
                ucai.btnPrev.Location = new Point(88, 295);
                ucai.btnNext.Location = new Point(163, 295);
                ucai.btnLast.Location = new Point(238, 295);

                ucai.grpAI.Height = 330;
                ucai.grpAI.Width = 350;
                ucai.grpAI.Location = new Point(172, 52);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void IEC104_MasterOnDoubleClickEvents()
        {
            string strRoutineName = "AIConfiguration: IEC104_MasterOnDoubleClickEvents";
            try
            {
                foreach (Control c in ucai.grpAI.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }
                ucai.txtAIAutoMapRange.Text = "0"; //Namrata: 10/09/2017
                ucai.txtAINo.Size = new Size(220, 20);
                ucai.cmbResponseType.Size = new Size(220, 20);
                ucai.txtIndex.Size = new Size(220, 20);
                ucai.txtSubIndex.Size = new Size(220, 20);
                ucai.txtMultiplier.Size = new Size(220, 20);
                ucai.txtConstant.Size = new Size(220, 20);
                ucai.txtDescription.Size = new Size(220, 20);
                ucai.txtAIAutoMapRange.Size = new Size(220, 20);

                ucai.lblAINo.Visible = ucai.txtAINo.Visible = true;
                ucai.txtAINo.Enabled = false;
                ucai.lblAINo.Location = new Point(15, 35);
                ucai.txtAINo.Location = new Point(102, 30);

                ucai.lblResponseType.Visible = ucai.cmbResponseType.Visible = true;
                ucai.lblResponseType.Location = new Point(15, 60);
                ucai.cmbResponseType.Location = new Point(102, 55);

                ucai.lblIndex.Visible = ucai.txtIndex.Visible = true;
                ucai.lblIndex.Location = new Point(15, 85);
                ucai.txtIndex.Location = new Point(102, 80);

                ucai.lblSubIndex.Visible = ucai.txtSubIndex.Visible = true;
                ucai.lblSubIndex.Location = new Point(15, 110);
                ucai.txtSubIndex.Location = new Point(102, 105);

                ucai.lblMultiplier.Visible = ucai.txtMultiplier.Visible = true;
                ucai.lblMultiplier.Location = new Point(15, 135);
                ucai.txtMultiplier.Location = new Point(102, 130);

                ucai.lblConstant.Visible = ucai.txtConstant.Visible = true;
                ucai.lblConstant.Location = new Point(15, 160);
                ucai.txtConstant.Location = new Point(102, 155);

                ucai.lblDesc.Visible = ucai.txtDescription.Visible = true;
                ucai.lblDesc.Location = new Point(15, 185);
                ucai.txtDescription.Location = new Point(102, 180);

                ucai.lblAutoMap.Visible = ucai.txtAIAutoMapRange.Visible = true;
                ucai.lblAutoMap.Location = new Point(15, 450); //Set the location out of box to hide
                ucai.txtAIAutoMapRange.Location = new Point(102, 450); //Set the location out of box to hide

                ucai.chkbxEventEnable.Visible = true;
                ucai.chkbxEventEnable.Checked = false;
                ucai.chkbxEventEnable.Location = new Point(102, 210);

                ucai.btnDone.Visible = ucai.btnCancel.Visible = true;
                ucai.btnDone.Location = new Point(102, 230);
                ucai.btnCancel.Location = new Point(205, 230);

                ucai.btnFirst.Visible = ucai.btnPrev.Visible = ucai.btnNext.Visible = ucai.btnLast.Visible = true;
                ucai.btnFirst.Location = new Point(13, 260);
                ucai.btnPrev.Location = new Point(88, 260);
                ucai.btnNext.Location = new Point(163, 260);
                ucai.btnLast.Location = new Point(238, 260);


                ucai.grpAI.Size = new Size(350, 290);
                ucai.grpAI.Location = new Point(172, 52);


            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Namrata:29/01/2019
        public void SPORT_MasterOnDoubleClickEvents()
        {
            string strRoutineName = "AIConfiguration: SPORT_MasterOnDoubleClickEvents";
            try
            {
                foreach (Control c in ucai.grpAI.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }
                ucai.txtAIAutoMapRange.Text = "0"; //Namrata: 10/09/2017
                ucai.txtAINo.Size = new Size(220, 20);
                ucai.cmbResponseType.Size = new Size(220, 20);
                ucai.txtIndex.Size = new Size(220, 20);
                ucai.txtSubIndex.Size = new Size(220, 20);
                ucai.txtMultiplier.Size = new Size(220, 20);
                ucai.txtConstant.Size = new Size(220, 20);
                ucai.txtDescription.Size = new Size(220, 20);
                ucai.txtAIAutoMapRange.Size = new Size(220, 20);

                ucai.lblAINo.Visible = ucai.txtAINo.Visible = true;
                ucai.txtAINo.Enabled = false;
                ucai.lblAINo.Location = new Point(15, 35);
                ucai.txtAINo.Location = new Point(102, 30);

                ucai.lblResponseType.Visible = ucai.cmbResponseType.Visible = true;
                ucai.lblResponseType.Location = new Point(15, 60);
                ucai.cmbResponseType.Location = new Point(102, 55);

                ucai.lblIndex.Visible = ucai.txtIndex.Visible = true;
                ucai.lblIndex.Location = new Point(15, 85);
                ucai.txtIndex.Location = new Point(102, 80);

                ucai.lblSubIndex.Visible = ucai.txtSubIndex.Visible = true;
                ucai.lblSubIndex.Location = new Point(15, 110);
                ucai.txtSubIndex.Location = new Point(102, 105);

                //label Deadband And Text Deadband
                ucai.lblDeadband.Visible = ucai.txtDeadband.Visible = true;
                ucai.lblDeadband.Location = new Point(15, 135);
                ucai.txtDeadband.Location = new Point(102, 130);
                ucai.txtDeadband.Size = new Size(220, 20);

                //Label Multiplier and Text Multiplier
                ucai.lblMultiplier.Visible = ucai.txtMultiplier.Visible = true;
                ucai.lblMultiplier.Location = new Point(15, 160);
                ucai.txtMultiplier.Location = new Point(102, 155);

                //Label Constant and Text Constant
                ucai.lblConstant.Visible = ucai.txtConstant.Visible = true;
                ucai.lblConstant.Location = new Point(15, 185);
                ucai.txtConstant.Location = new Point(102, 180);

                //Label HighLimit And Text High Limit
                ucai.lblHighLimit.Visible = ucai.txtHighLimit.Visible = true;
                ucai.lblHighLimit.Location = new Point(15, 210);
                ucai.txtHighLimit.Location = new Point(102, 205);
                ucai.txtHighLimit.Size = new Size(220, 20);

                //Label LowLimit And Text Low Limit
                ucai.lblLowLimit.Visible = ucai.txtLowLimit.Visible = true;
                ucai.lblLowLimit.Location = new Point(15, 235);
                ucai.txtLowLimit.Location = new Point(102, 230);
                ucai.txtLowLimit.Size = new Size(220, 20);

                //Label DONo. And Text DONo.
                ucai.lblDONo.Visible = ucai.CmbDONo.Visible = true;
                ucai.lblDONo.Location = new Point(15, 260);
                ucai.CmbDONo.Location = new Point(102, 255);
                ucai.CmbDONo.Size = new Size(220, 20);

                //Label Description and Text Description
                ucai.lblDesc.Visible = ucai.txtDescription.Visible = true;
                ucai.lblDesc.Location = new Point(15, 285);
                ucai.txtDescription.Location = new Point(102, 280);

                ucai.lblAutoMap.Visible = ucai.txtAIAutoMapRange.Visible = true;
                ucai.lblAutoMap.Location = new Point(15, 450); //Set the location out of box to hide
                ucai.txtAIAutoMapRange.Location = new Point(102, 450); //Set the location out of box to hide

                ucai.chkbxEventEnable.Visible = true;
                ucai.chkbxEventEnable.Checked = false;
                ucai.chkbxEventEnable.Location = new Point(102, 310);

                ucai.btnDone.Visible = ucai.btnCancel.Visible = true;
                ucai.btnDone.Location = new Point(102, 330);
                ucai.btnCancel.Location = new Point(205, 330);

                ucai.btnFirst.Visible = ucai.btnPrev.Visible = ucai.btnNext.Visible = ucai.btnLast.Visible = true;
                ucai.btnFirst.Location = new Point(13, 360);
                ucai.btnPrev.Location = new Point(88, 360);
                ucai.btnNext.Location = new Point(163, 360);
                ucai.btnLast.Location = new Point(238, 360);

                ucai.grpAI.Size = new Size(350, 390);
                ucai.grpAI.Location = new Point(172, 52);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void IEC61850_MasterOnDoubleClickEvents()
        {
            string strRoutineName = "AIConfiguration: IEC61850_MasterOnDoubleClickEvents";
            try
            {
                foreach (Control c in ucai.grpAI.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }
                ucai.txtAIAutoMapRange.Text = ""; //Namrata: 10/09/2017
                IsAddEdit = true;//Namrata:22/02/2019
                ucai.lblAINo.Visible = ucai.txtAINo.Visible = true;
                ucai.txtAINo.Enabled = false;
                ucai.lblAINo.Location = new Point(15, 35);
                ucai.txtAINo.Location = new Point(102, 30);
                ucai.txtAINo.Size = new Size(300, 21);

                ucai.lblIEDName.Visible = ucai.cmbIEDName.Visible = true;
                ucai.lblIEDName.Location = new Point(15, 60);
                ucai.cmbIEDName.Location = new Point(102, 55);
                ucai.cmbIEDName.Size = new Size(300, 21);

                ucai.lbl61850ResponseType.Visible = ucai.cmb61850ResponseType.Visible = true;
                ucai.lbl61850ResponseType.Location = new Point(15, 85);
                ucai.cmb61850ResponseType.Location = new Point(102, 80);
                ucai.cmb61850ResponseType.Size = new Size(300, 21);

                ucai.lblFC.Visible = ucai.cmbFC.Visible = true;
                //ucai.cmbFC.Enabled = false;
                ucai.lblFC.Location = new Point(15, 110);
                ucai.cmbFC.Location = new Point(102, 105);
                ucai.cmbFC.Size = new Size(300, 21);

                //Namrata:25/03/2019
                ucai.lbl61850Index.Visible = ucai.cmb61850Index.Visible = false;
                ucai.lbl61850Index.Location = new Point(15, 135);
                ucai.cmb61850Index.Location = new Point(102, 130);
                ucai.cmb61850Index.Size = new Size(300, 21);

                ucai.lbl61850Index.Visible = ucai.ChkIEC61850Index.Visible = true;
                ucai.lbl61850Index.Location = new Point(15, 135);
                ucai.ChkIEC61850Index.Location = new Point(102, 130);
                ucai.ChkIEC61850Index.Size = new Size(300, 21);


                ucai.lblSubIndex.Visible = ucai.txtSubIndex.Visible = true;
                ucai.lblSubIndex.Location = new Point(15, 160);
                ucai.txtSubIndex.Location = new Point(102, 155);
                ucai.txtSubIndex.Size = new Size(300, 21);

                ucai.lblMultiplier.Visible = ucai.txtMultiplier.Visible = true;
                ucai.lblMultiplier.Location = new Point(15, 185);
                ucai.txtMultiplier.Location = new Point(102, 180);
                ucai.txtMultiplier.Size = new Size(300, 21);

                ucai.lblConstant.Visible = ucai.txtConstant.Visible = true;
                ucai.lblConstant.Location = new Point(15, 210);
                ucai.txtConstant.Location = new Point(102, 205);
                ucai.txtConstant.Size = new Size(300, 21);

                ucai.lblDesc.Visible = ucai.txtDescription.Visible = true;
                ucai.lblDesc.Location = new Point(15, 235);
                ucai.txtDescription.Location = new Point(102, 230);
                ucai.txtDescription.Size = new Size(300, 21);

                ucai.lblAutoMap.Visible = ucai.txtAIAutoMapRange.Visible = true;
                ucai.lblAutoMap.Location = new Point(15, 450); //Set the location out of box to hide
                ucai.txtAIAutoMapRange.Location = new Point(102, 450); //Set the location out of box to hide
                ucai.txtAIAutoMapRange.Size = new Size(300, 21);

                ucai.chkbxEventEnable.Visible = true;
                ucai.chkbxEventEnable.Checked = false;
                ucai.chkbxEventEnable.Location = new Point(18, 260);

                ucai.btnDone.Visible = ucai.btnCancel.Visible = true;
                ucai.btnDone.Location = new Point(125, 270);
                ucai.btnCancel.Location = new Point(225, 270);

                ucai.btnFirst.Visible = ucai.btnPrev.Visible = ucai.btnNext.Visible = ucai.btnLast.Visible = true;
                ucai.btnFirst.Location = new Point(80, 300);
                ucai.btnPrev.Location = new Point(150, 300);
                ucai.btnNext.Location = new Point(220, 300);
                ucai.btnLast.Location = new Point(290, 300);

                ucai.grpAI.Height = 330;
                ucai.grpAI.Width = 420;
                ucai.grpAI.Location = new Point(172, 52);
                ucai.pbHdr.Width = 510;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 31/07/2018 
        public void LoadProfile_OnDoubleClick()
        {
            string strRoutineName = "AIConfiguration: LoadProfile_OnDoubleClick";
            try
            {
                foreach (Control c in ucai.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }
                ucai.txtAINo.Size = new Size(220, 20);
                ucai.cmbResponseType.Size = new Size(220, 20);
                ucai.txtIndex.Size = new Size(220, 20);
                ucai.txtSubIndex.Size = new Size(220, 20);
                ucai.txtMultiplier.Size = new Size(220, 20);
                ucai.txtConstant.Size = new Size(220, 20);
                ucai.txtDescription.Size = new Size(220, 20);
                ucai.txtAIAutoMapRange.Size = new Size(220, 20);

                ucai.txtAIAutoMapRange.Text = "0"; //Namrata: 10/09/2017
                ucai.lblAINo.Visible = ucai.txtAINo.Visible = true;
                ucai.lblAINo.Location = new Point(15, 35);
                ucai.txtAINo.Location = new Point(102, 30);

                ucai.lblName.Visible = ucai.cmbName.Visible = true;
                ucai.lblName.Location = new Point(15, 60);
                ucai.cmbName.Location = new Point(102, 55);
                ucai.cmbName.Size = new Size(220, 20);

                ucai.lblAI1.Visible = ucai.cmbAI1.Visible = true;
                ucai.lblAI1.Location = new Point(15, 85);
                ucai.cmbAI1.Location = new Point(102, 80);
                ucai.cmbAI1.Size = new Size(220, 20);

                ucai.lblAI2.Visible = ucai.cmbAI2.Visible = true;
                ucai.lblAI2.Location = new Point(15, 110);
                ucai.cmbResponseType.Size = new Size(220, 20);
                ucai.cmbResponseType.Location = new Point(102, 105);

                ucai.lblAI3.Visible = ucai.cmbAI3.Visible = true;
                ucai.lblAI3.Location = new Point(15, 135);
                ucai.cmbAI3.Size = new Size(220, 20);
                ucai.cmbAI3.Location = new Point(102, 130);

                ucai.lblDesc.Visible = ucai.txtDescription.Visible = true;
                ucai.lblDesc.Location = new Point(15, 160);
                ucai.txtDescription.Location = new Point(102, 155);

                ucai.chkbxEventEnable.Visible = true;
                ucai.chkbxEventEnable.Checked = false;
                ucai.chkbxEventEnable.Location = new Point(102, 183);


                ucai.chkbxLogEnable.Visible = false;

                ucai.btnDone.Visible = ucai.btnCancel.Visible = ucai.btnVerify.Visible = true;
                ucai.btnVerify.Location = new Point(15, 205);
                ucai.btnDone.Location = new Point(115, 205);
                ucai.btnCancel.Location = new Point(223, 205);

                ucai.btnFirst.Visible = ucai.btnPrev.Visible = ucai.btnNext.Visible = ucai.btnLast.Visible = true;
                ucai.btnFirst.Location = new Point(13, 240);
                ucai.btnPrev.Location = new Point(88, 240);
                ucai.btnNext.Location = new Point(163, 240);
                ucai.btnLast.Location = new Point(238, 240);

                ucai.grpAI.Size = new Size(350, 270);
                ucai.grpAI.Location = new Point(172, 52);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion All Masters OnDoubleClick Events

        #region All Slaves Events
        //Namrata:02/04/2019
        private void MQTTSlave_OnDoubleClick()
        {
            string strRoutineName = "AIConfiguration: MQTTSlave_OnMapLoad";
            try
            {
                ucai.ChkIEC61850MapIndex.Visible = false;
                //Ajay: 04/09/2018 

                #region Visible false Events
                #region CellNo
                ucai.CmbCellNo.Visible = false;
                ucai.lblCellNo.Visible = false;
                #endregion CellNo

                #region Widget
                ucai.CmbWidget.Visible = false;
                ucai.lblWidget.Visible = false;
                #endregion Widget
                //UnitID
                ucai.lblUnitID.Visible = false;
                ucai.txtUnitID.Visible = false;

                //Event
                ucai.chkbxEvent.Visible = false;

                //IEC61850Server Index
                ucai.lblMapIndex.Visible = false;
                ucai.CmbReportingIndex.Visible = false;

                //Command Type
                ucai.lblCT.Visible = false;
                ucai.cmbAIMCommandType.Visible = false;

                //AutoMap
                ucai.AutoMapRange.Visible = false;
                ucai.txtAutoMapM.Visible = false;
                #endregion Visible false Events

                #region Visible true Events
                //AIMNo
                ucai.lblANM.Location = new Point(13, 33);
                ucai.txtAIMNo.Location = new Point(119, 30);
                ucai.txtAIMNo.Size = new Size(220, 21);

                //Reporting Index
                ucai.lblAMRI.Visible = true;//Namrata:28/03/2019
                ucai.txtAIMReportingIndex.Visible = true;//Namrata:28/03/2019
                ucai.lblAMRI.Location = new Point(13, 58);
                ucai.txtAIMReportingIndex.Location = new Point(119, 55);
                ucai.txtAIMReportingIndex.Size = new Size(220, 21);

                //DataType
                ucai.lblAMDT.Visible = true; ucai.cmbAIMDataType.Visible = true;
                ucai.lblAMDT.Location = new Point(13, 83);
                ucai.cmbAIMDataType.Location = new Point(119, 80);
                ucai.cmbAIMDataType.Size = new Size(220, 21);

                //Deadband
                ucai.lblAMDB.Location = new Point(13, 109);
                ucai.txtAIMDeadBand.Location = new Point(119, 106);
                ucai.txtAIMDeadBand.Visible = true; ucai.lblAMDB.Visible = true;
                ucai.txtAIMDeadBand.Size = new Size(220, 21);

                //Multiplier
                ucai.lblAMM.Location = new Point(13, 135);
                ucai.txtAIMMultiplier.Location = new Point(119, 132);
                ucai.txtAIMMultiplier.Visible = true; ucai.lblAMM.Visible = true;
                ucai.txtAIMMultiplier.Size = new Size(220, 21);

                //Constant
                ucai.lblAMC.Location = new Point(13, 161);
                ucai.txtAIMConstant.Location = new Point(119, 158); ucai.lblAMC.Visible = true;
                ucai.txtAIMConstant.Size = new Size(220, 21);



                //Unit
                ucai.lblUnit.Location = new Point(13, 186);
                ucai.lblUnit.Visible = true;
                ucai.txtUnit.Visible = true;
                ucai.txtUnit.Location = new Point(119, 183);
                ucai.txtUnit.Visible = true;
                ucai.txtUnit.Size = new Size(220, 21);

                //Key
                ucai.lblKey.Location = new Point(13, 211);
                ucai.txtKey.Location = new Point(119, 208);
                ucai.lblKey.Visible = true;
                ucai.txtKey.Visible = true;
                ucai.txtKey.Size = new Size(220, 21);

                //MapDescription
                ucai.lblMDec.Location = new Point(13, 236);
                ucai.txtMapDescription.Location = new Point(119, 233);
                ucai.txtMapDescription.Visible = true; ucai.lblMDec.Visible = true;
                ucai.txtMapDescription.Size = new Size(220, 21);

                ucai.chkUsed.Visible = false;

                ucai.btnAIMDone.Location = new Point(120, 260);
                ucai.btnAIMCancel.Location = new Point(210, 260);


                ucai.grpAIMap.Size = new Size(355, 300);
                ucai.pbAIMHdr.Size = new Size(355, 22);
                ucai.grpAIMap.Location = new Point(252, 305);
                #endregion Visible true Events


            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SMSSlave_OnDoubleClick()
        {
            string strRoutineName = "AIConfiguration:SMSSlave_OnDoubleClick";
            try
            {
                ucai.ChkIEC61850MapIndex.Visible = false;
                //Ajay: 04/09/2018 

                #region Visible false Events
                #region CellNo
                ucai.CmbCellNo.Visible = false;
                ucai.lblCellNo.Visible = false;
                #endregion CellNo

                #region Widget
                ucai.CmbWidget.Visible = false;
                ucai.lblWidget.Visible = false;
                #endregion Widget
                //UnitID
                ucai.lblUnitID.Visible = false;
                ucai.txtUnitID.Visible = false;

                //Event
                ucai.chkbxEvent.Visible = false;

                //IEC61850Server Index
                ucai.lblMapIndex.Visible = false;
                ucai.CmbReportingIndex.Visible = false;

                //Command Type
                ucai.lblCT.Visible = false;
                ucai.cmbAIMCommandType.Visible = false;

                //AutoMap
                ucai.AutoMapRange.Visible = false;
                ucai.txtAutoMapM.Visible = false;

                //Key
                ucai.lblKey.Visible = false;
                ucai.txtKey.Visible = false;

                ucai.chkUsed.Visible = false;

                #endregion Visible false Events

                #region Visible true Events
                //AIMNo
                ucai.lblANM.Location = new Point(13, 33);
                ucai.txtAIMNo.Location = new Point(119, 30);
                ucai.txtAIMNo.Size = new Size(220, 21);

                //Reporting Index
                ucai.lblAMRI.Visible = true;//Namrata:28/03/2019
                ucai.txtAIMReportingIndex.Visible = true;//Namrata:28/03/2019
                ucai.lblAMRI.Location = new Point(13, 58);
                ucai.txtAIMReportingIndex.Location = new Point(119, 55);
                ucai.txtAIMReportingIndex.Size = new Size(220, 21);

                //DataType
                ucai.lblAMDT.Visible = true;
                ucai.lblAMDT.Location = new Point(13, 83);
                ucai.cmbAIMDataType.Location = new Point(119, 80);
                ucai.cmbAIMDataType.Size = new Size(220, 21);

                //Deadband
                ucai.lblAMDB.Location = new Point(13, 109);
                ucai.txtAIMDeadBand.Location = new Point(119, 106);
                ucai.txtAIMDeadBand.Visible = true;
                ucai.txtAIMDeadBand.Size = new Size(220, 21);

                //Multiplier
                ucai.lblAMM.Location = new Point(13, 135);
                ucai.txtAIMMultiplier.Location = new Point(119, 132);
                ucai.txtAIMMultiplier.Visible = true;
                ucai.txtAIMMultiplier.Size = new Size(220, 21);

                //Constant
                ucai.lblAMC.Location = new Point(13, 161);
                ucai.txtAIMConstant.Location = new Point(119, 158);
                ucai.txtAIMConstant.Size = new Size(220, 21);

                //Unit
                ucai.lblUnit.Location = new Point(13, 186);
                ucai.lblUnit.Visible = true;
                ucai.txtUnit.Visible = true;
                ucai.txtUnit.Location = new Point(119, 183);
                ucai.txtUnit.Visible = true;
                ucai.txtUnit.Size = new Size(220, 21);

                //txtMapDescription
                ucai.lblMDec.Location = new Point(13, 211);
                ucai.txtMapDescription.Location = new Point(119, 208);
                ucai.lblMDec.Visible = true;
                ucai.txtMapDescription.Visible = true;
                ucai.txtMapDescription.Size = new Size(220, 21);

                ////MapDescription
                //ucai.lblMDec.Location = new Point(13, 236);
                //ucai.txtMapDescription.Location = new Point(119, 233);
                //ucai.txtMapDescription.Visible = true;
                //ucai.txtMapDescription.Size = new Size(220, 21);

                ucai.btnAIMDone.Location = new Point(120, 235);
                ucai.btnAIMCancel.Location = new Point(210, 235);

                ucai.grpAIMap.Size = new Size(355, 280);
                ucai.pbAIMHdr.Size = new Size(355, 22);
                ucai.grpAIMap.Location = new Point(257, 304);
                #endregion Visible true Events

                ucai.lblAMC.Visible = true; ucai.txtAIMConstant.Visible = true;
                ucai.lblAMDT.Visible = true; ucai.cmbAIMDataType.Visible = true;
                ucai.txtAIMDeadBand.Visible = true; ucai.lblAMDB.Visible = true;
                ucai.txtAIMMultiplier.Visible = true; ucai.lblAMM.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MQTTSlave_OnMapLoad()
        {
            string strRoutineName = "AIConfiguration:MQTTSlave_OnMapLoad";
            try
            {
                ucai.ChkIEC61850MapIndex.Visible = false;
                //Ajay: 04/09/2018 

                #region Visible false Events
                //Namrata: 20/08/2019
                #region CellNo
                ucai.CmbCellNo.Visible = false;
                ucai.lblCellNo.Visible = false;
                #endregion CellNo

                #region Widget
                ucai.CmbWidget.Visible = false;
                ucai.lblWidget.Visible = false;
                #endregion Widget



                //UnitID
                ucai.lblUnitID.Visible = false;
                ucai.txtUnitID.Visible = false;

                //Event
                ucai.chkbxEvent.Visible = false;

                //IEC61850Server Index
                ucai.lblMapIndex.Visible = false;
                ucai.CmbReportingIndex.Visible = false;

                //Command Type
                ucai.lblCT.Visible = false;
                ucai.cmbAIMCommandType.Visible = false;


                #endregion Visible false Events

                #region Visible true Events
                //AIMNo
                ucai.lblANM.Location = new Point(13, 33);
                ucai.txtAIMNo.Location = new Point(119, 30);
                ucai.txtAIMNo.Size = new Size(220, 21);

                //Reporting Index
                ucai.lblAMRI.Visible = true;//Namrata:28/03/2019
                ucai.txtAIMReportingIndex.Visible = true;//Namrata:28/03/2019
                ucai.lblAMRI.Location = new Point(13, 58);
                ucai.txtAIMReportingIndex.Location = new Point(119, 55);
                ucai.txtAIMReportingIndex.Size = new Size(220, 21);

                //DataType
                ucai.lblAMDT.Visible = true; ucai.cmbAIMDataType.Visible = true;
                ucai.lblAMDT.Location = new Point(13, 83);
                ucai.cmbAIMDataType.Location = new Point(119, 80);
                ucai.cmbAIMDataType.Size = new Size(220, 21);

                //Deadband
                ucai.lblAMDB.Location = new Point(13, 109); ucai.lblAMDB.Visible = true;
                ucai.txtAIMDeadBand.Location = new Point(119, 106);
                ucai.txtAIMDeadBand.Visible = true;
                ucai.txtAIMDeadBand.Size = new Size(220, 21);

                //Multiplier
                ucai.lblAMM.Location = new Point(13, 135); ucai.lblAMM.Visible = true;
                ucai.txtAIMMultiplier.Location = new Point(119, 132);
                ucai.txtAIMMultiplier.Visible = true;
                ucai.txtAIMMultiplier.Size = new Size(220, 21);

                //Constant
                ucai.lblAMC.Location = new Point(13, 161); ucai.lblAMC.Visible = true; ucai.txtAIMConstant.Visible = true;
                ucai.txtAIMConstant.Location = new Point(119, 158);
                ucai.txtAIMConstant.Size = new Size(220, 21);

                //Unit
                ucai.lblUnit.Location = new Point(13, 186);
                ucai.lblUnit.Visible = true;
                ucai.txtUnit.Visible = true;
                ucai.txtUnit.Location = new Point(119, 183);
                ucai.txtUnit.Visible = true;
                ucai.txtUnit.Size = new Size(220, 21);

                //Key
                ucai.lblKey.Location = new Point(13, 211);
                ucai.txtKey.Location = new Point(119, 208);
                ucai.lblKey.Visible = true;
                ucai.txtKey.Visible = true;
                ucai.txtKey.Size = new Size(220, 21);
                //MapDescription
                ucai.lblMDec.Location = new Point(13, 236);
                ucai.txtMapDescription.Location = new Point(119, 233);
                ucai.txtMapDescription.Visible = true;
                ucai.txtMapDescription.Size = new Size(220, 21);

                //AutoMap
                ucai.AutoMapRange.Location = new Point(13, 261);
                ucai.txtAutoMapM.Location = new Point(119, 258);
                ucai.txtAutoMapM.Visible = true;
                ucai.txtAutoMapM.Size = new Size(220, 21);
                ucai.AutoMapRange.Visible = true;
                ucai.txtAutoMapM.Visible = true;


                ucai.chkUsed.Visible = false;

                ucai.btnAIMDone.Location = new Point(120, 285);
                ucai.btnAIMCancel.Location = new Point(210, 285);


                ucai.grpAIMap.Size = new Size(355, 320);
                ucai.pbAIMHdr.Size = new Size(355, 22);
                ucai.grpAIMap.Location = new Point(252, 305);
                #endregion Visible true Events


            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Namrata:10/04/2019
        private string getMQTTSlaveKey(ListView lstview, string ainno)
        {
            try
            {
                int iColIndex = ucai.lvAIlist.Columns["Description"].Index;
                var query = lstview.Items.Cast<ListViewItem>().Where(item => item.SubItems[0].Text == ainno).Select(s => s.SubItems[iColIndex].Text).Single();
                return query.ToString();
            }
            catch
            {
                return null;
            }
        }
        private void SMSSlave_OnMapLoad()
        {
            string strRoutineName = "AIConfiguration:MQTTSlave_OnMapLoad";
            try
            {
                //Ajay: 04/09/2018 
                ucai.ChkIEC61850MapIndex.Visible = false;
                #region Visible false Events
                //Namrata: 20/08/2019
                #region CellNo
                ucai.CmbCellNo.Visible = false;
                ucai.lblCellNo.Visible = false;
                #endregion CellNo

                #region Widget
                ucai.CmbWidget.Visible = false;
                ucai.lblWidget.Visible = false;
                #endregion Widget
                //UnitID
                ucai.lblUnitID.Visible = false;
                ucai.txtUnitID.Visible = false;

                //Event
                ucai.chkbxEvent.Visible = false;

                //IEC61850Server Index
                ucai.lblMapIndex.Visible = false;
                ucai.CmbReportingIndex.Visible = false;

                //Command Type
                ucai.lblCT.Visible = false;
                ucai.cmbAIMCommandType.Visible = false;

                //Key
                ucai.lblKey.Visible = false;
                ucai.txtKey.Visible = false;

                ucai.lblEV.Visible = false;
                ucai.CmbEV.Visible = false;
                ucai.lblEventClass.Visible = false;
                ucai.cmbEventC.Visible = false;
                ucai.lblVariation.Visible = false;
                ucai.CmbVari.Visible = false;


                #endregion Visible false Events

                #region Visible true Events
                //AIMNo
                ucai.lblANM.Location = new Point(13, 33);
                ucai.txtAIMNo.Location = new Point(119, 30);
                ucai.txtAIMNo.Size = new Size(220, 21);

                //Reporting Index
                ucai.lblAMRI.Visible = true;//Namrata:28/03/2019
                ucai.txtAIMReportingIndex.Visible = true;//Namrata:28/03/2019
                ucai.lblAMRI.Location = new Point(13, 58);
                ucai.txtAIMReportingIndex.Location = new Point(119, 55);
                ucai.txtAIMReportingIndex.Size = new Size(220, 21);

                //DataType
               
                ucai.lblAMDT.Location = new Point(13, 83);
                ucai.cmbAIMDataType.Location = new Point(119, 80);
                ucai.cmbAIMDataType.Size = new Size(220, 21);

                //Deadband
                ucai.lblAMDB.Location = new Point(13, 109);
                ucai.txtAIMDeadBand.Location = new Point(119, 106);
                ucai.txtAIMDeadBand.Size = new Size(220, 21);

                //Multiplier
                ucai.lblAMM.Location = new Point(13, 135);
                ucai.txtAIMMultiplier.Location = new Point(119, 132);
                ucai.txtAIMMultiplier.Size = new Size(220, 21);

                //Constant
                ucai.lblAMC.Location = new Point(13, 161);
                ucai.txtAIMConstant.Location = new Point(119, 158);
                ucai.txtAIMConstant.Size = new Size(220, 21);
               
                //Unit
                ucai.lblUnit.Location = new Point(13, 186);
                ucai.lblUnit.Visible = true;
                ucai.txtUnit.Visible = true;
                ucai.txtUnit.Location = new Point(119, 183);
                ucai.txtUnit.Visible = true;
                ucai.txtUnit.Size = new Size(220, 21);

                //txtMapDescription
                ucai.lblMDec.Location = new Point(13, 211);
                ucai.txtMapDescription.Location = new Point(119, 208);
                ucai.lblMDec.Visible = true;
                ucai.txtMapDescription.Visible = true;
                ucai.txtMapDescription.Size = new Size(220, 21);

                //txtAutoMapM
                ucai.AutoMapRange.Location = new Point(13, 236);
                ucai.AutoMapRange.Visible = true;
                ucai.txtAutoMapM.Location = new Point(119, 233);
                ucai.txtAutoMapM.Visible = true;
                ucai.txtAutoMapM.Size = new Size(220, 21);
                ucai.lblAMC.Visible = true; ucai.txtAIMConstant.Visible = true;
                ucai.lblAMDT.Visible = true; ucai.cmbAIMDataType.Visible = true;
                ucai.txtAIMDeadBand.Visible = true; ucai.lblAMDB.Visible = true;
                ucai.txtAIMMultiplier.Visible = true; ucai.lblAMM.Visible = true;
                //Used
                ucai.chkUsed.Visible = false;

                //BtnDone & btnCancel
                ucai.btnAIMDone.Location = new Point(120, 260);
                ucai.btnAIMCancel.Location = new Point(210, 260);

                ucai.grpAIMap.Size = new Size(355, 300);
                ucai.pbAIMHdr.Size = new Size(355, 22);
                ucai.grpAIMap.Location = new Point(257, 304);
                #endregion Visible true Events
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SPORTSlave_OnLoadEvents()
        {
            string strRoutineName = "AIConfiguration: SPORTSlave_OnLoadEvents";
            try
            {
                //Namrata: 21/10/2019
                ucai.lblKey.Visible = false;
                ucai.txtKey.Visible = false;
                ucai.lblUnit.Visible = false;
                ucai.txtUnit.Visible = false;

                //Ajay: 04/09/2018
                ucai.chkbxEvent.Visible = true;
                ucai.chkbxEvent.Location = new Point(120, 261);

                ucai.lblANM.Location = new Point(13, 33);
                ucai.txtAIMNo.Location = new Point(119, 30);
                ucai.txtAIMNo.Size = new Size(220, 21);

                //UnitID
                ucai.lblUnitID.Visible = true;
                ucai.txtUnitID.Visible = true;
                ucai.lblUnitID.Location = new Point(13, 58);
                ucai.txtUnitID.Location = new Point(119, 55);
                ucai.txtUnitID.Size = new Size(220, 21);

                //Reporting Index
                ucai.lblAMRI.Visible = true;
                ucai.txtAIMReportingIndex.Visible = true;
                ucai.lblAMRI.Location = new Point(13, 83);
                ucai.txtAIMReportingIndex.Location = new Point(119, 80);
                ucai.txtAIMReportingIndex.Size = new Size(220, 21);

                //Map Index
                ucai.lblMapIndex.Visible = false;
                ucai.CmbReportingIndex.Visible = false;
                ucai.ChkIEC61850MapIndex.Visible = false;//Namrata: 16/04/2019

                ucai.lblEV.Visible = false;
                ucai.CmbEV.Visible = false;
                ucai.lblEventClass.Visible = false;
                ucai.cmbEventC.Visible = false;
                ucai.lblVariation.Visible = false;
                ucai.CmbVari.Visible = false;

                //DataType
                ucai.lblAMDT.Visible = true;
                ucai.cmbAIMDataType.Visible = true;
                ucai.lblAMDT.Location = new Point(13, 109);
                ucai.cmbAIMDataType.Location = new Point(119, 106);
                ucai.cmbAIMDataType.Size = new Size(220, 21);

                //Command Type
                ucai.lblCT.Visible = false;
                ucai.cmbAIMCommandType.Visible = false;

                //Deadband
                ucai.lblAMDB.Visible = true;
                ucai.txtAIMDeadBand.Visible = true;
                ucai.lblAMDB.Location = new Point(13, 135);
                ucai.txtAIMDeadBand.Location = new Point(119, 132);
                ucai.txtAIMDeadBand.Size = new Size(220, 21);

                //Multiplier
                ucai.lblAMM.Visible = true;
                ucai.txtAIMMultiplier.Visible = true;
                ucai.lblAMM.Location = new Point(13, 161);
                ucai.txtAIMMultiplier.Location = new Point(119, 158);
                ucai.txtAIMMultiplier.Size = new Size(220, 21);

                //Constant
                ucai.lblAMC.Visible = true;
                ucai.txtAIMConstant.Visible = true;
                ucai.lblAMC.Location = new Point(13, 186);
                ucai.txtAIMConstant.Location = new Point(119, 183);
                ucai.txtAIMConstant.Visible = true;
                ucai.txtAIMConstant.Size = new Size(220, 21);

                //Description
                ucai.lblMDec.Location = new Point(13, 211);
                ucai.txtMapDescription.Location = new Point(119, 208);
                ucai.lblMDec.Visible = true;
                ucai.txtMapDescription.Visible = true;
                ucai.txtMapDescription.Size = new Size(220, 21);

                //Automap  Range
                ucai.AutoMapRange.Visible = true;
                ucai.txtAutoMapM.Visible = true;
                ucai.AutoMapRange.Location = new Point(13, 236);
                ucai.txtAutoMapM.Location = new Point(119, 233);
                ucai.txtAutoMapM.Visible = true;
                ucai.txtAutoMapM.Size = new Size(220, 21);
                ucai.txtAutoMapM.Text = "1";

                ucai.chkUsed.Visible = true;
                ucai.chkUsed.Location = new Point(18, 261);

                //Namrata: 20/08/2019
                #region CellNo
                ucai.CmbCellNo.Visible = false;
                ucai.lblCellNo.Visible = false;
                #endregion CellNo

                #region Widget
                ucai.CmbWidget.Visible = false;
                ucai.lblWidget.Visible = false;
                #endregion Widget

                ucai.btnAIMDone.Location = new Point(120, 285);
                ucai.btnAIMCancel.Location = new Point(210, 285);

                ucai.grpAIMap.Size = new Size(350, 325);
                ucai.pbAIMHdr.Size = new Size(350, 20);
                ucai.grpAIMap.Location = new Point(350, 310);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SPORTSlave_OnDoubleClickEvents()
        {
            string strRoutineName = "AIConfiguration: SPORTSlave_OnDoubleClickEvents";
            try
            {
                #region Visible False
                //Namrata: 21/10/2019
                ucai.lblKey.Visible = false;
                ucai.txtKey.Visible = false;
                ucai.lblUnit.Visible = false;
                ucai.txtUnit.Visible = false;

                //IEC61850Reporting Index
                ucai.lblMapIndex.Visible = false;
                ucai.CmbReportingIndex.Visible = false;
                ucai.ChkIEC61850MapIndex.Visible = false;//Namrata: 16/04/2019

                //CommandType
                ucai.lblCT.Visible = false;
                ucai.cmbAIMCommandType.Visible = false;

                //AutoMap
                ucai.AutoMapRange.Visible = false;
                ucai.txtAutoMapM.Visible = false;
                ucai.AutoMapRange.Location = new Point(13, 236);
                ucai.txtAutoMapM.Location = new Point(119, 233);
                ucai.txtAutoMapM.Size = new Size(220, 21);

                ucai.lblEV.Visible = false;
                ucai.CmbEV.Visible = false;
                ucai.lblEventClass.Visible = false;
                ucai.cmbEventC.Visible = false;
                ucai.lblVariation.Visible = false;
                ucai.CmbVari.Visible = false;

                //Namrata: 20/08/2019
                #region CellNo
                ucai.CmbCellNo.Visible = false;
                ucai.lblCellNo.Visible = false;
                #endregion CellNo

                #region Widget
                ucai.CmbWidget.Visible = false;
                ucai.lblWidget.Visible = false;
                #endregion Widget
                #endregion Visible False

                #region Visible True
                //AINo
                ucai.lblANM.Location = new Point(13, 33);
                ucai.txtAIMNo.Location = new Point(119, 30);
                ucai.txtAIMNo.Size = new Size(220, 21);

                //UnitID
                ucai.lblUnitID.Visible = true;
                ucai.txtUnitID.Visible = true;
                ucai.lblUnitID.Location = new Point(13, 58);
                ucai.txtUnitID.Location = new Point(119, 55);
                ucai.txtUnitID.Size = new Size(220, 21);

                //ReportingIndex
                ucai.lblAMRI.Visible = true;
                ucai.txtAIMReportingIndex.Visible = true;
                ucai.lblAMRI.Location = new Point(13, 83);
                ucai.txtAIMReportingIndex.Location = new Point(119, 80);
                ucai.txtAIMReportingIndex.Size = new Size(220, 21);

                //DataType
                ucai.lblAMDT.Visible = true;
                ucai.lblAMDT.Location = new Point(13, 109);
                ucai.cmbAIMDataType.Location = new Point(119, 106);
                ucai.cmbAIMDataType.Visible = true;
                ucai.cmbAIMDataType.Size = new Size(220, 21);

                //DeadBand
                ucai.lblAMDB.Visible = true;
                ucai.lblAMDB.Location = new Point(13, 135);
                ucai.txtAIMDeadBand.Location = new Point(119, 132);
                ucai.txtAIMDeadBand.Visible = true;
                ucai.txtAIMDeadBand.Size = new Size(220, 21);

                //Multiplier
                ucai.lblAMM.Visible = true;
                ucai.txtAIMMultiplier.Visible = true;
                ucai.lblAMM.Location = new Point(13, 161);
                ucai.txtAIMMultiplier.Location = new Point(119, 158);
                ucai.txtAIMMultiplier.Size = new Size(220, 21);

                //Constant
                ucai.lblAMC.Location = new Point(13, 186);
                ucai.txtAIMConstant.Location = new Point(119, 183);
                ucai.lblAMC.Visible = true;
                ucai.txtAIMConstant.Visible = true;
                ucai.txtAIMConstant.Size = new Size(220, 21);

                //Description
                ucai.lblMDec.Location = new Point(13, 211);
                ucai.txtMapDescription.Location = new Point(119, 208);
                ucai.lblMDec.Visible = true;
                ucai.txtMapDescription.Visible = true;
                ucai.txtMapDescription.Size = new Size(220, 21);

                //Used
                ucai.chkUsed.Visible = false;
                ucai.chkUsed.Enabled = true;
                ucai.chkUsed.Location = new Point(18, 240);

                //Event
                ucai.chkbxEvent.Visible = true; //Ajay: 08/09/2018
                ucai.chkbxEvent.Location = new Point(120, 240); //Ajay: 08/09/2018

                //BtnDone & BtnCancel
                ucai.btnAIMDone.Location = new Point(120, 260);
                ucai.btnAIMCancel.Location = new Point(210, 260);

                //GrpAIMap
                ucai.grpAIMap.Size = new Size(350, 295);
                ucai.pbAIMHdr.Size = new Size(350, 20);
                ucai.grpAIMap.Location = new Point(253, 305);
                #endregion Visible True
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void IEC101Slave_IEC104Slave_OnMapDoubleClickEvents()
        {
            string strRoutineName = "AIConfiguration: IEC101Slave_IEC104Slave_OnMapDoubleClickEvents";
            try
            {
                #region Visible (False) Events

                ucai.lblEV.Visible = false;
                ucai.CmbEV.Visible = false;
                ucai.lblEventClass.Visible = false;
                ucai.cmbEventC.Visible = false;
                ucai.lblVariation.Visible = false;
                ucai.CmbVari.Visible = false;
                //Namrata: 21/10/2019
                ucai.lblKey.Visible = false;
                ucai.txtKey.Visible = false;
                ucai.lblUnit.Visible = false;
                ucai.txtUnit.Visible = false;

                #region Unit ID
                ucai.lblUnitID.Visible = false;
                ucai.txtUnitID.Visible = false;
                #endregion Unit ID

                #region IEC61850Index
                ucai.lblMapIndex.Visible = false;
                ucai.CmbReportingIndex.Visible = false;
                ucai.ChkIEC61850MapIndex.Visible = false;//Namrata: 16/04/2019
                #endregion IEC61850Index

                #region CommandType
                ucai.lblCT.Visible = false;
                ucai.cmbAIMCommandType.Visible = false;
                #endregion CommandType

                #region Used
                ucai.chkUsed.Visible = false;
                #endregion Used

                //Namrata: 20/08/2019
                #region CellNo
                ucai.CmbCellNo.Visible = false;
                ucai.lblCellNo.Visible = false;
                #endregion CellNo

                #region Widget
                ucai.CmbWidget.Visible = false;
                ucai.lblWidget.Visible = false;
                #endregion Widget
                #endregion Visible (False) Events

                #region Visible (True) Events

                #region AINo
                ucai.lblANM.Visible = true;
                ucai.txtAIMNo.Visible = true;
                ucai.lblANM.Location = new Point(13, 33);
                ucai.txtAIMNo.Location = new Point(119, 30);
                ucai.txtAIMNo.Size = new Size(220, 21);
                #endregion AINo

                #region Reporting Index
                ucai.lblAMRI.Visible = true;
                ucai.txtAIMReportingIndex.Visible = true;
                ucai.lblAMRI.Location = new Point(13, 58);
                ucai.txtAIMReportingIndex.Location = new Point(119, 55);
                ucai.txtAIMReportingIndex.Size = new Size(220, 21);
                #endregion Reporting Index

                #region DataType
                ucai.lblAMDT.Visible = true;
                ucai.cmbAIMDataType.Visible = true;
                ucai.lblAMDT.Location = new Point(13, 83);
                ucai.cmbAIMDataType.Location = new Point(119, 80);
                ucai.cmbAIMDataType.Size = new Size(220, 21);
                #endregion DataType

                #region Deadband
                ucai.lblAMDB.Visible = true;
                ucai.txtAIMDeadBand.Visible = true;
                ucai.lblAMDB.Location = new Point(13, 109);
                ucai.txtAIMDeadBand.Location = new Point(119, 106);
                ucai.txtAIMDeadBand.Size = new Size(220, 21);
                #endregion Deadband

                #region Multiplier
                ucai.lblAMM.Visible = true;
                ucai.txtAIMMultiplier.Visible = true;
                ucai.lblAMM.Location = new Point(13, 135);
                ucai.txtAIMMultiplier.Location = new Point(119, 132);
                ucai.txtAIMMultiplier.Size = new Size(220, 21);
                #endregion Multiplier

                #region Constant
                ucai.lblAMC.Visible = true;
                ucai.txtAIMConstant.Visible = true;
                ucai.lblAMC.Location = new Point(13, 161);
                ucai.txtAIMConstant.Location = new Point(119, 158);
                ucai.txtAIMConstant.Size = new Size(220, 21);
                #endregion Constant

                #region Description
                ucai.lblMDec.Location = new Point(13, 186);
                ucai.txtMapDescription.Location = new Point(119, 183);
                ucai.txtMapDescription.Visible = true;
                ucai.txtMapDescription.Size = new Size(220, 21);
                #endregion Description

                #region Event
                ucai.chkbxEvent.Visible = true; //Ajay: 08/09/2018
                ucai.chkbxEvent.Location = new Point(15, 215); //Ajay: 08/09/2018
                #endregion Event

                #region Automap
                ucai.AutoMapRange.Visible = false;
                ucai.txtAutoMapM.Visible = false;
                #endregion Automap

                #region BtnDone & BtnCancel
                ucai.btnAIMDone.Location = new Point(120, 215);
                ucai.btnAIMCancel.Location = new Point(210, 215);
                #endregion BtnDone & BtnCancel

                #region GroupBox
                ucai.grpAIMap.Size = new Size(350, 260);
                ucai.pbAIMHdr.Size = new Size(350, 20);
                ucai.grpAIMap.Location = new Point(253, 305);
                #endregion GroupBox
                #endregion Visible (True) Events
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void IEC101Slave_IEC104Slave_OnMapLoad()
        {
            string strRoutineName = "AIConfiguration: IEC101Slave_IEC104Slave_OnMapLoad";
            try
            {
                ucai.lblEV.Visible = false;
                ucai.CmbEV.Visible = false;
                ucai.lblEventClass.Visible = false;
                ucai.cmbEventC.Visible = false;
                ucai.lblVariation.Visible = false;
                ucai.CmbVari.Visible = false;
                //Namrata: 21/10/2019
                ucai.lblKey.Visible = false;
                ucai.txtKey.Visible = false;
                ucai.lblUnit.Visible = false;
                ucai.txtUnit.Visible = false;
                //Namrata: 20/08/2019
                #region CellNo
                ucai.CmbCellNo.Visible = false;
                ucai.lblCellNo.Visible = false;
                #endregion CellNo

                #region Widget
                ucai.CmbWidget.Visible = false;
                ucai.lblWidget.Visible = false;
                #endregion Widget

                //Ajay: 04/09/2018
                ucai.chkbxEvent.Visible = true;
                ucai.chkbxEvent.Location = new Point(15, 240);

                ucai.lblANM.Location = new Point(13, 33);
                ucai.txtAIMNo.Location = new Point(119, 30);
                ucai.txtAIMNo.Size = new Size(220, 21);

                ucai.lblUnitID.Visible = false;
                ucai.txtUnitID.Visible = false;

                ucai.lblAMRI.Visible = true;//Namrata:28/03/2019
                ucai.txtAIMReportingIndex.Visible = true;//Namrata:28/03/2019
                ucai.lblAMRI.Location = new Point(13, 58);
                ucai.txtAIMReportingIndex.Location = new Point(119, 55);
                ucai.txtAIMReportingIndex.Size = new Size(220, 21);

                ucai.lblMapIndex.Visible = false;
                ucai.CmbReportingIndex.Visible = false;
                ucai.ChkIEC61850MapIndex.Visible = false;//Namrata: 16/04/2019

                ucai.lblAMDT.Visible = true;
                ucai.cmbAIMDataType.Visible = true;
                ucai.lblAMDT.Location = new Point(13, 83);
                ucai.cmbAIMDataType.Location = new Point(119, 80);
                ucai.cmbAIMDataType.Size = new Size(220, 21);


                ucai.lblCT.Visible = false;
                ucai.cmbAIMCommandType.Visible = false;

                ucai.lblAMDB.Visible = true;
                ucai.txtAIMDeadBand.Visible = true;
                ucai.lblAMDB.Location = new Point(13, 109);
                ucai.txtAIMDeadBand.Location = new Point(119, 106);
                ucai.txtAIMDeadBand.Size = new Size(220, 21);

                ucai.lblAMM.Visible = true;
                ucai.lblAMM.Location = new Point(13, 135);
                ucai.txtAIMMultiplier.Location = new Point(119, 132);
                ucai.txtAIMMultiplier.Visible = true;
                ucai.txtAIMMultiplier.Size = new Size(220, 21);

                ucai.lblAMC.Visible = true;
                ucai.txtAIMConstant.Visible = true;
                ucai.lblAMC.Location = new Point(13, 161);
                ucai.txtAIMConstant.Location = new Point(119, 158);
                ucai.txtAIMConstant.Size = new Size(220, 21);

                ucai.lblMDec.Visible = true;
                ucai.lblMDec.Location = new Point(13, 186);
                ucai.txtMapDescription.Location = new Point(119, 183);
                ucai.txtMapDescription.Visible = true;
                ucai.txtMapDescription.Size = new Size(220, 21);

                ucai.AutoMapRange.Location = new Point(13, 211);
                ucai.txtAutoMapM.Location = new Point(119, 208);
                ucai.txtAutoMapM.Size = new Size(220, 21);
                ucai.AutoMapRange.Visible = true;
                ucai.txtAutoMapM.Visible = true;

                ucai.chkUsed.Visible = false;

                ucai.btnAIMDone.Location = new Point(120, 236);
                ucai.btnAIMCancel.Location = new Point(210, 236);

                ucai.txtAutoMapM.Text = "1";
                ucai.ChkIEC61850MapIndex.Visible = false;//Namrata:16/04/2019

                ucai.grpAIMap.Size = new Size(350, 275);
                ucai.pbAIMHdr.Size = new Size(350, 20);
                ucai.grpAIMap.Location = new Point(350, 360);

            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Namrata: 12/08/2019
        private void DisplaySlave_OnLoad()
        {
            string strRoutineName = "AIConfiguration: DisplaySlave_OnLoad";
            try
            {
                #region Visible False Events
                ucai.lblEV.Visible = false;
                ucai.CmbEV.Visible = false;
                ucai.lblEventClass.Visible = false;
                ucai.cmbEventC.Visible = false;
                ucai.lblVariation.Visible = false;
                ucai.CmbVari.Visible = false;
                //Namrata: 21/10/2019
                ucai.lblKey.Visible = false;
                ucai.txtKey.Visible = false;
                ucai.lblUnit.Visible = false;
                ucai.txtUnit.Visible = false;
                #region IEC61850ReportingIndex
                ucai.lblMapIndex.Visible = false;
                ucai.CmbReportingIndex.Visible = false;
                ucai.lblMapIndex.Location = new Point(12, 56);
                ucai.ChkIEC61850MapIndex.Visible = true;
                ucai.ChkIEC61850MapIndex.Location = new Point(118, 56);
                ucai.ChkIEC61850MapIndex.Visible = false;
                ucai.ChkIEC61850MapIndex.Size = new Size(300, 21);
                #endregion IEC61850ReportingIndex

                #region DataType
                ucai.lblAMDT.Visible = false;
                ucai.cmbAIMDataType.Visible = false;
                #endregion DataType

                #region CommandType
                ucai.lblCT.Visible = false;//Namrata:28/03/2019
                ucai.cmbAIMCommandType.Enabled = false;
                ucai.cmbAIMCommandType.Visible = false;//Namrata:28/03/2019
                #endregion CommandType

                #region Deadband
                ucai.lblAMDB.Visible = false;
                ucai.txtAIMDeadBand.Visible = false;
                #endregion Deadband

                #region Multiplier
                ucai.lblAMM.Visible = false;
                ucai.txtAIMMultiplier.Visible = false;
                #endregion Multiplier

                #region Constant
                ucai.lblAMC.Visible = false;
                ucai.txtAIMConstant.Visible = false;
                #endregion Constant

                #region CheckBox Events
                ucai.ChkIEC61850MapIndex.Visible = false;
                ucai.chkUsed.Visible = false;
                ucai.chkbxEvent.Visible = false;
                #endregion CheckBox Events

                #endregion Visible False Events

                #region Visible True

                #region AIMapNo
                ucai.lblANM.Visible = true;
                ucai.txtAIMNo.Visible = true;
                ucai.lblANM.Location = new Point(12, 33);
                ucai.txtAIMNo.Location = new Point(80, 30);
                ucai.txtAIMNo.Size = new Size(160, 21);
                #endregion AIMapNo

                #region ReportingIndex
                ucai.lblAMRI.Visible = false;
                ucai.txtAIMReportingIndex.Visible = false;
                #endregion ReportingIndex

                #region CellNo
                ucai.lblCellNo.Visible = true;
                ucai.CmbCellNo.Visible = true;
                ucai.lblCellNo.Location = new Point(12, 58);
                ucai.CmbCellNo.Location = new Point(80, 55);
                ucai.CmbCellNo.Size = new Size(160, 21);
                #endregion CellNo

                #region Widgets
                ucai.lblWidget.Visible = true;
                ucai.CmbWidget.Visible = true;
                ucai.lblWidget.Location = new Point(12, 83);
                ucai.CmbWidget.Location = new Point(80, 80);
                ucai.CmbWidget.Size = new Size(160, 21);
                #endregion Widgets

                #region Unit
                ucai.lblUnitID.Text = "Unit";
                ucai.lblUnitID.Visible = true;
                ucai.txtUnitID.Visible = true;
                ucai.lblUnitID.Enabled = true;
                ucai.txtUnitID.Enabled = true;
                ucai.lblUnitID.Location = new Point(12, 109);
                ucai.txtUnitID.Location = new Point(80, 106);
                ucai.txtUnitID.Size = new Size(160, 21);
                ucai.txtUnitID.Text = "Unity";
                #endregion Unit

                #region Description
                ucai.lblMDec.Visible = true;
                ucai.txtMapDescription.Visible = true;
                ucai.lblMDec.Location = new Point(12, 135);
                ucai.txtMapDescription.Location = new Point(80, 132);
                ucai.txtMapDescription.Size = new Size(160, 21);
                #endregion Description

                #region Automap
                ucai.AutoMapRange.Visible = false;
                ucai.txtAutoMapM.Visible = false;
                ucai.txtAutoMapM.Text = "1";
                ucai.AutoMapRange.Location = new Point(12, 161);
                ucai.txtAutoMapM.Location = new Point(80, 158);
                ucai.txtAutoMapM.Size = new Size(160, 21);
                #endregion Automap

                ucai.grpAIMap.Size = new Size(260, 200);
                ucai.grpAIMap.Location = new Point(178, 79);
                ucai.pbAIMHdr.Size = new Size(450, 22);

                ucai.btnAIMDone.Location = new Point(80, 160);
                ucai.btnAIMCancel.Location = new Point(160, 160);
                #endregion Visible True
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DisplaySlave_OnDoubleClick()
        {
            string strRoutineName = "AIConfiguration: DisplaySlave_OnDoubleClick";
            try
            {
                #region Visible False Events
                ucai.lblEV.Visible = false;
                ucai.CmbEV.Visible = false;
                ucai.lblEventClass.Visible = false;
                ucai.cmbEventC.Visible = false;
                ucai.lblVariation.Visible = false;
                ucai.CmbVari.Visible = false;
                //Namrata: 21/10/2019
                ucai.lblKey.Visible = false;
                ucai.txtKey.Visible = false;
                ucai.lblUnit.Visible = false;
                ucai.txtUnit.Visible = false;
                #region IEC61850ReportingIndex
                ucai.lblMapIndex.Visible = false;
                ucai.CmbReportingIndex.Visible = false;
                ucai.lblMapIndex.Location = new Point(12, 56);
                ucai.ChkIEC61850MapIndex.Visible = true;
                ucai.ChkIEC61850MapIndex.Location = new Point(118, 56);
                ucai.ChkIEC61850MapIndex.Visible = false;
                ucai.ChkIEC61850MapIndex.Size = new Size(300, 21);
                #endregion IEC61850ReportingIndex

                #region DataType
                ucai.lblAMDT.Visible = false;
                ucai.cmbAIMDataType.Visible = false;
                #endregion DataType

                #region CommandType
                ucai.lblCT.Visible = false;//Namrata:28/03/2019
                ucai.cmbAIMCommandType.Enabled = false;
                ucai.cmbAIMCommandType.Visible = false;//Namrata:28/03/2019
                #endregion CommandType

                #region Deadband
                ucai.lblAMDB.Visible = false;
                ucai.txtAIMDeadBand.Visible = false;
                #endregion Deadband

                #region Multiplier
                ucai.lblAMM.Visible = false;
                ucai.txtAIMMultiplier.Visible = false;
                #endregion Multiplier

                #region Constant
                ucai.lblAMC.Visible = false;
                ucai.txtAIMConstant.Visible = false;
                #endregion Constant

                #region CheckBox Events
                ucai.ChkIEC61850MapIndex.Visible = false;
                ucai.chkUsed.Visible = false;
                ucai.chkbxEvent.Visible = false;
                #endregion CheckBox Events

                #endregion Visible False Events

                #region Visible True

                #region AIMapNo
                ucai.lblANM.Visible = true;
                ucai.txtAIMNo.Visible = true;
                ucai.lblANM.Location = new Point(12, 33);
                ucai.txtAIMNo.Location = new Point(80, 30);
                ucai.txtAIMNo.Size = new Size(160, 21);
                #endregion AIMapNo

                #region ReportingIndex
                ucai.lblAMRI.Visible = false;
                ucai.txtAIMReportingIndex.Visible = false;
                #endregion ReportingIndex

                #region CellNo
                ucai.lblCellNo.Visible = true;
                ucai.CmbCellNo.Visible = true;
                ucai.lblCellNo.Location = new Point(12, 58);
                ucai.CmbCellNo.Location = new Point(80, 55);
                ucai.CmbCellNo.Size = new Size(160, 21);
                #endregion CellNo

                #region Widgets
                ucai.lblWidget.Visible = true;
                ucai.CmbWidget.Visible = true;
                ucai.lblWidget.Location = new Point(12, 83);
                ucai.CmbWidget.Location = new Point(80, 80);
                ucai.CmbWidget.Size = new Size(160, 21);
                #endregion Widgets

                #region Unit
                ucai.lblUnitID.Text = "Unit";
                ucai.lblUnitID.Visible = true;
                ucai.txtUnitID.Visible = true;
                ucai.lblUnitID.Enabled = true;
                ucai.txtUnitID.Enabled = true;
                ucai.lblUnitID.Location = new Point(12, 109);
                ucai.txtUnitID.Location = new Point(80, 106);
                ucai.txtUnitID.Size = new Size(160, 21);
                ucai.txtUnitID.Text = "Unity";
                #endregion Unit

                #region Description
                ucai.lblMDec.Visible = true;
                ucai.txtMapDescription.Visible = true;
                ucai.lblMDec.Location = new Point(12, 135);
                ucai.txtMapDescription.Location = new Point(80, 132);
                ucai.txtMapDescription.Size = new Size(160, 21);
                #endregion Description

                #region Automap
                ucai.AutoMapRange.Visible = false;
                ucai.txtAutoMapM.Visible = false;
                ucai.AutoMapRange.Location = new Point(12, 161);
                ucai.txtAutoMapM.Location = new Point(80, 158);
                ucai.txtAutoMapM.Size = new Size(160, 21);
                #endregion Automap

                ucai.btnAIMDone.Location = new Point(80, 160);
                ucai.btnAIMCancel.Location = new Point(160, 160);

                ucai.grpAIMap.Size = new Size(260, 200);
                ucai.grpAIMap.Location = new Point(178, 79);
                ucai.pbAIMHdr.Size = new Size(450, 22);


                #endregion Visible True
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void IEC61850Server_OnLoad()
        {
            string strRoutineName = "AIConfiguration: IEC61850Server_OnLoad";
            try
            {
                ucai.lblEV.Visible = false;
                ucai.CmbEV.Visible = false;
                ucai.lblEventClass.Visible = false;
                ucai.cmbEventC.Visible = false;
                ucai.lblVariation.Visible = false;
                ucai.CmbVari.Visible = false;
                //Namrata: 21/10/2019
                ucai.lblKey.Visible = false;
                ucai.txtKey.Visible = false;
                ucai.lblUnit.Visible = false;
                ucai.txtUnit.Visible = false;
                //Namrata: 20/08/2019
                #region CellNo
                ucai.CmbCellNo.Visible = false;
                ucai.lblCellNo.Visible = false;
                #endregion CellNo

                #region Widget
                ucai.CmbWidget.Visible = false;
                ucai.lblWidget.Visible = false;
                #endregion Widget
                ucai.txtAutoMapM.Text = "";
                ucai.ChkIEC61850MapIndex.Visible = true;//Namrata:16/04/2019
                //Ajay: 04/09/2018 
                ucai.chkbxEvent.Visible = false;
                ucai.grpAIMap.Size = new Size(450, 260);
                ucai.grpAIMap.Location = new Point(467, 353);
                ucai.pbAIMHdr.Size = new Size(450, 22);

                ucai.lblUnitID.Visible = false;
                ucai.txtUnitID.Visible = false;

                ucai.lblANM.Location = new Point(12, 30);
                ucai.txtAIMNo.Location = new Point(118, 30);
                ucai.txtAIMNo.Size = new Size(300, 21);


                ucai.lblAMRI.Visible = false;
                ucai.txtAIMReportingIndex.Visible = false;

                ucai.lblMapIndex.Visible = true;
                ucai.CmbReportingIndex.Visible = false;
                ucai.lblMapIndex.Location = new Point(12, 56);
                ucai.ChkIEC61850MapIndex.Visible = true;
                ucai.ChkIEC61850MapIndex.Location = new Point(118, 56);
                ucai.ChkIEC61850MapIndex.Visible = true;
                ucai.ChkIEC61850MapIndex.Size = new Size(300, 21);

                ucai.lblAMDT.Location = new Point(12, 82);
                ucai.cmbAIMDataType.Location = new Point(118, 82);
                ucai.cmbAIMDataType.Size = new Size(300, 21);

                ucai.lblCT.Location = new Point(12, 109);
                ucai.lblCT.Visible = true;//Namrata:28/03/2019
                ucai.cmbAIMCommandType.Enabled = true;
                ucai.cmbAIMCommandType.Visible = true;//Namrata:28/03/2019
                ucai.cmbAIMCommandType.Location = new Point(118, 109);
                ucai.cmbAIMCommandType.Size = new Size(300, 21);

                ucai.lblAMDB.Visible = true;//Namrata:28/03/2019
                ucai.txtAIMDeadBand.Visible = true;//Namrata:28/03/2019
                ucai.lblAMDB.Location = new Point(12, 109);
                ucai.txtAIMDeadBand.Location = new Point(118, 109);
                ucai.txtAIMDeadBand.Size = new Size(300, 21);

                ucai.lblAMM.Location = new Point(12, 136);
                ucai.txtAIMMultiplier.Location = new Point(118, 136);
                ucai.txtAIMMultiplier.Size = new Size(300, 21);


                ucai.lblAMC.Location = new Point(12, 163);
                ucai.txtAIMConstant.Location = new Point(118, 163);
                ucai.txtAIMConstant.Size = new Size(300, 21);

                ucai.lblMDec.Location = new Point(12, 188);
                ucai.txtMapDescription.Location = new Point(118, 188);
                ucai.txtMapDescription.Size = new Size(300, 21);

                ucai.AutoMapRange.Visible = false;
                ucai.txtAutoMapM.Visible = false;
                ucai.AutoMapRange.Location = new Point(12, 214);
                ucai.txtAutoMapM.Location = new Point(117, 214);
                ucai.txtAutoMapM.Size = new Size(300, 21);

                ucai.chkUsed.Visible = false;//Namrata:28/03/2019
                ucai.chkUsed.Location = new Point(16, 240);
                ucai.btnAIMDone.Location = new Point(170, 217);
                ucai.btnAIMCancel.Location = new Point(260, 217);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void IEC61850_OnMapDoubleClick()
        {
            string strRoutineName = "AIConfiguration: IEC61850_OnMapDoubleClick";
            try
            {
                ucai.lblEV.Visible = false;
                ucai.CmbEV.Visible = false;
                ucai.lblEventClass.Visible = false;
                ucai.cmbEventC.Visible = false;
                ucai.lblVariation.Visible = false;
                ucai.CmbVari.Visible = false;
                //Namrata: 21/10/2019
                ucai.lblKey.Visible = false;
                ucai.txtKey.Visible = false;
                ucai.lblUnit.Visible = false;
                ucai.txtUnit.Visible = false;
                #region Visible False
                //Namrata: 20/08/2019
                #region CellNo
                ucai.CmbCellNo.Visible = false;
                ucai.lblCellNo.Visible = false;
                #endregion CellNo

                #region Widget
                ucai.CmbWidget.Visible = false;
                ucai.lblWidget.Visible = false;
                #endregion Widget
                //Event
                ucai.chkbxEvent.Visible = false; //Ajay: 08/09/2018

                //Automap
                ucai.AutoMapRange.Visible = false;
                ucai.txtAutoMapM.Visible = false;

                //Reporting Index
                ucai.lblAMRI.Visible = false;
                ucai.txtAIMReportingIndex.Visible = false;
                #endregion Visible False

                #region Visible True
                //AINo
                ucai.lblANM.Location = new Point(12, 30);
                ucai.txtAIMNo.Location = new Point(118, 30);
                ucai.txtAIMNo.Size = new Size(300, 21);

                //IEC61850Index
                ucai.lblMapIndex.Visible = true;
                ucai.lblMapIndex.Location = new Point(12, 56);
                ucai.ChkIEC61850MapIndex.Location = new Point(118, 56);
                ucai.CmbReportingIndex.Visible = false;
                ucai.ChkIEC61850MapIndex.Visible = true;//Namrata:16/04/2019
                ucai.ChkIEC61850MapIndex.Size = new Size(300, 21);

                //DataType
                ucai.lblAMDT.Location = new Point(12, 82);
                ucai.cmbAIMDataType.Location = new Point(118, 82);
                ucai.cmbAIMDataType.Size = new Size(300, 21);

                //CommandType
                ucai.lblCT.Visible = true;
                ucai.cmbAIMCommandType.Visible = true;
                ucai.cmbAIMCommandType.Enabled = true;
                ucai.lblCT.Location = new Point(12, 109);
                ucai.cmbAIMCommandType.Location = new Point(118, 109);
                ucai.cmbAIMCommandType.Size = new Size(300, 21);


                //Deadband

                ucai.lblAMDB.Location = new Point(12, 136);
                ucai.txtAIMDeadBand.Location = new Point(118, 136);
                ucai.txtAIMDeadBand.Size = new Size(300, 21);



                //Multiplier
                ucai.lblAMM.Location = new Point(12, 163);
                ucai.txtAIMMultiplier.Location = new Point(118, 163);
                ucai.txtAIMMultiplier.Size = new Size(300, 21);

                //Constant
                ucai.lblAMC.Location = new Point(12, 188);
                ucai.txtAIMConstant.Visible = true;
                ucai.txtAIMConstant.Location = new Point(118, 188);
                ucai.txtAIMConstant.Size = new Size(300, 21);


                //Description
                ucai.lblMDec.Location = new Point(12, 213);
                ucai.txtMapDescription.Visible = true;
                ucai.txtMapDescription.Location = new Point(118, 213);
                ucai.txtMapDescription.Size = new Size(300, 21);

                //BtnDone & BtnCancel
                ucai.btnAIMDone.Location = new Point(170, 240);
                ucai.btnAIMCancel.Location = new Point(265, 240);

                //GrpAI
                ucai.grpAIMap.Size = new Size(450, 280);
                ucai.grpAIMap.Location = new Point(450, 355);
                ucai.pbAIMHdr.Size = new Size(450, 22);
                #endregion Visible True
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MODBUSSlave_OnDoubleClick()
        {
            string strRoutineName = "AIConfiguration: Modbus_OnMapDoubleClick";
            try
            {
                ucai.lblEV.Visible = false;
                ucai.CmbEV.Visible = false;
                ucai.lblEventClass.Visible = false;
                ucai.cmbEventC.Visible = false;
                ucai.lblVariation.Visible = false;
                ucai.CmbVari.Visible = false;
                //Namrata: 21/10/2019
                ucai.lblKey.Visible = false;
                ucai.txtKey.Visible = false;
                ucai.lblUnit.Visible = false;
                ucai.txtUnit.Visible = false;
                #region Visible false Events
                //Namrata: 20/08/2019
                #region CellNo
                ucai.CmbCellNo.Visible = false;
                ucai.lblCellNo.Visible = false;
                #endregion CellNo

                #region Widget
                ucai.CmbWidget.Visible = false;
                ucai.lblWidget.Visible = false;
                #endregion Widget
                //IEC61850Index
                ucai.lblMapIndex.Visible = false;
                ucai.CmbReportingIndex.Visible = false;
                ucai.ChkIEC61850MapIndex.Visible = false;//Namrata: 16/04/2019
                //Event
                ucai.chkbxEvent.Visible = false; //Ajay: 08/09/2018

                //Automap
                ucai.AutoMapRange.Visible = false;
                ucai.txtAutoMapM.Visible = false;
                #endregion Visible false Events

                #region Visible True Events
                ucai.lblANM.Location = new Point(13, 33);
                ucai.txtAIMNo.Location = new Point(119, 30);
                ucai.txtAIMNo.Size = new Size(220, 21);

                ucai.lblUnitID.Visible = false;
                ucai.txtUnitID.Visible = false;

                ucai.lblAMRI.Visible = true;//Namrata:28/03/2019
                ucai.txtAIMReportingIndex.Visible = true;//Namrata:28/03/2019
                ucai.lblAMRI.Location = new Point(13, 58);
                ucai.txtAIMReportingIndex.Location = new Point(119, 55);
                ucai.txtAIMReportingIndex.Size = new Size(220, 21);

                ucai.lblMapIndex.Visible = false;
                ucai.CmbReportingIndex.Visible = false;
                ucai.ChkIEC61850MapIndex.Visible = false;//Namrata:16/04/2019

                ucai.lblAMDT.Visible = true;
                ucai.cmbAIMDataType.Visible = true;
                ucai.lblAMDT.Location = new Point(13, 83);
                ucai.cmbAIMDataType.Location = new Point(119, 80);
                ucai.cmbAIMDataType.Size = new Size(220, 21);


                ucai.lblCT.Visible = true;
                ucai.lblCT.Location = new Point(13, 109);
                ucai.cmbAIMCommandType.Location = new Point(119, 106);
                ucai.cmbAIMCommandType.Visible = true;
                ucai.cmbAIMCommandType.Size = new Size(220, 21);

                ucai.lblAMDB.Location = new Point(13, 135);
                ucai.txtAIMDeadBand.Location = new Point(119, 132);
                ucai.txtAIMDeadBand.Visible = true; ucai.lblAMDB.Visible = true;
                ucai.txtAIMDeadBand.Size = new Size(220, 21);

                ucai.lblAMM.Visible = true; ucai.txtAIMMultiplier.Visible = true;
                ucai.lblAMM.Location = new Point(13, 161);
                ucai.txtAIMMultiplier.Location = new Point(119, 158);
                ucai.txtAIMMultiplier.Size = new Size(220, 21);

                ucai.txtAIMConstant.Visible = true; ucai.lblAMC.Visible = true;
                ucai.lblAMC.Location = new Point(13, 186);
                ucai.txtAIMConstant.Location = new Point(119, 183);
                ucai.txtAIMConstant.Visible = true;
                ucai.txtAIMConstant.Size = new Size(220, 21);

                ucai.lblMDec.Location = new Point(13, 211);
                ucai.txtMapDescription.Location = new Point(119, 208);
                ucai.txtMapDescription.Visible = true; ucai.lblMDec.Visible = true;
                ucai.txtMapDescription.Size = new Size(220, 21);

                //BtnDone & BtnCancel
                ucai.btnAIMDone.Location = new Point(110, 245);
                ucai.btnAIMCancel.Location = new Point(210, 245);

                //GrpAI
                ucai.grpAIMap.Size = new Size(350, 285);
                ucai.pbAIMHdr.Size = new Size(350, 20);
                ucai.grpAIMap.Location = new Point(253, 305);
                #endregion Visible True Events
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MODBUSSlave_OnLoad()
        {
            string strRoutineName = "AIConfiguration: MODBUSSlave_OnLoad";
            try
            {
                ucai.lblEV.Visible = false;
                ucai.CmbEV.Visible = false;
                ucai.lblEventClass.Visible = false;
                ucai.cmbEventC.Visible = false;
                ucai.lblVariation.Visible = false;
                ucai.CmbVari.Visible = false;
                //Namrata: 21/10/2019
                ucai.lblKey.Visible = false;
                ucai.txtKey.Visible = false;
                ucai.lblUnit.Visible = false;
                ucai.txtUnit.Visible = false;
                //Namrata: 20/08/2019
                #region CellNo
                ucai.CmbCellNo.Visible = false;
                ucai.lblCellNo.Visible = false;
                #endregion CellNo

                #region Widget
                ucai.CmbWidget.Visible = false;
                ucai.lblWidget.Visible = false;
                #endregion Widget
                //Ajay: 04/09/2018 
                ucai.chkbxEvent.Visible = false;
                ucai.ChkIEC61850MapIndex.Visible = false;//Namrata: 16/04/2019
                ucai.lblANM.Location = new Point(13, 33);
                ucai.txtAIMNo.Location = new Point(119, 30);
                ucai.txtAIMNo.Size = new Size(220, 21);

                ucai.lblUnitID.Visible = false;
                ucai.txtUnitID.Visible = false;

                ucai.lblAMRI.Visible = true;//Namrata:28/03/2019
                ucai.txtAIMReportingIndex.Visible = true;//Namrata:28/03/2019
                ucai.lblAMRI.Location = new Point(13, 58);
                ucai.txtAIMReportingIndex.Location = new Point(119, 55);
                ucai.txtAIMReportingIndex.Size = new Size(220, 21);

                ucai.lblMapIndex.Visible = false;
                ucai.CmbReportingIndex.Visible = false;
                ucai.ChkIEC61850MapIndex.Visible = false;//Namrata:16/04/2019

                ucai.lblAMDT.Visible = true; ucai.cmbAIMDataType.Visible = true;
                ucai.lblAMDT.Location = new Point(13, 83);
                ucai.cmbAIMDataType.Location = new Point(119, 80);
                ucai.cmbAIMDataType.Size = new Size(220, 21);


                ucai.lblCT.Visible = true;
                ucai.lblCT.Location = new Point(13, 109);
                ucai.cmbAIMCommandType.Location = new Point(119, 106);
                ucai.cmbAIMCommandType.Visible = true;
                ucai.cmbAIMCommandType.Size = new Size(220, 21);

                ucai.lblAMDB.Location = new Point(13, 135);
                ucai.txtAIMDeadBand.Location = new Point(119, 132);
                ucai.txtAIMDeadBand.Visible = true; ucai.lblAMDB.Visible = true;
                ucai.txtAIMDeadBand.Size = new Size(220, 21);

                ucai.lblAMM.Visible = true;
                ucai.txtAIMMultiplier.Visible = true;
                ucai.lblAMM.Location = new Point(13, 161);
                ucai.txtAIMMultiplier.Location = new Point(119, 158);
                ucai.txtAIMMultiplier.Size = new Size(220, 21);

                ucai.lblAMC.Location = new Point(13, 186);
                ucai.txtAIMConstant.Location = new Point(119, 183);
                ucai.txtAIMConstant.Visible = true; ucai.lblAMC.Visible = true;
                ucai.txtAIMConstant.Size = new Size(220, 21);

                ucai.lblMDec.Location = new Point(13, 211);
                ucai.txtMapDescription.Location = new Point(119, 208);
                ucai.txtMapDescription.Visible = true; ucai.lblMDec.Visible = true;
                ucai.txtMapDescription.Size = new Size(220, 21);

                ucai.AutoMapRange.Visible = true;
                ucai.txtAutoMapM.Visible = true;
                ucai.AutoMapRange.Location = new Point(13, 236);
                ucai.txtAutoMapM.Location = new Point(119, 233);
                ucai.txtAutoMapM.Visible = true;
                ucai.txtAutoMapM.Size = new Size(220, 21);

                ucai.chkUsed.Visible = false;

                ucai.btnAIMDone.Location = new Point(120, 265);
                ucai.btnAIMCancel.Location = new Point(210, 265);


                ucai.grpAIMap.Size = new Size(353, 310);
                ucai.grpAIMap.Location = new Point(450, 325);
                ucai.pbAIMHdr.Size = new Size(353, 22);


            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DNPSlave_OnDoubleClick()
        {
            string strRoutineName = "AIConfiguration: DNPSlave_OnLoad";
            try
            {
                //Namrata: 21/10/2019
                ucai.lblKey.Visible = false;
                ucai.txtKey.Visible = false;
                ucai.lblUnit.Visible = false;
                ucai.txtUnit.Visible = false;
                //Namrata: 20/08/2019
                #region CellNo
                ucai.CmbCellNo.Visible = false;
                ucai.lblCellNo.Visible = false;
                #endregion CellNo

                #region Widget
                ucai.CmbWidget.Visible = false;
                ucai.lblWidget.Visible = false;
                #endregion Widget
                //Ajay: 04/09/2018 
                ucai.chkbxEvent.Visible = false;
                ucai.ChkIEC61850MapIndex.Visible = false;//Namrata: 16/04/2019
                ucai.lblANM.Location = new Point(13, 33);
                ucai.txtAIMNo.Location = new Point(119, 30);
                ucai.txtAIMNo.Size = new Size(220, 21);

                ucai.lblUnitID.Visible = false;
                ucai.txtUnitID.Visible = false;


                ucai.lblAMRI.Visible = true;//Namrata:28/03/2019
                ucai.txtAIMReportingIndex.Visible = true;//Namrata:28/03/2019
                ucai.lblAMRI.Location = new Point(13, 58);
                ucai.txtAIMReportingIndex.Location = new Point(119, 55);
                ucai.txtAIMReportingIndex.Size = new Size(220, 21);

                ucai.lblMapIndex.Visible = false;
                ucai.CmbReportingIndex.Visible = false;
                ucai.ChkIEC61850MapIndex.Visible = false;//Namrata:16/04/2019

                ucai.lblAMDT.Visible = false; ucai.cmbAIMDataType.Visible = false;


                ucai.lblCT.Visible = true;
                ucai.cmbAIMCommandType.Visible = true;
                ucai.lblCT.Location = new Point(13, 83);
                ucai.cmbAIMCommandType.Location = new Point(119, 80);
                ucai.cmbAIMCommandType.Size = new Size(220, 21);

                ucai.txtAIMDeadBand.Visible = true; ucai.lblAMDB.Visible = true;
                ucai.lblAMDB.Location = new Point(13, 109);
                ucai.txtAIMDeadBand.Location = new Point(119, 106);
                ucai.txtAIMDeadBand.Size = new Size(220, 21);


                ucai.lblAMM.Visible = true;
                ucai.txtAIMMultiplier.Visible = true;
                ucai.lblAMM.Location = new Point(13, 135);
                ucai.txtAIMMultiplier.Location = new Point(119, 132);
                ucai.txtAIMMultiplier.Size = new Size(220, 21);


                ucai.lblAMC.Location = new Point(13, 161);
                ucai.txtAIMConstant.Location = new Point(119, 158);
                ucai.txtAIMConstant.Visible = true; ucai.lblAMC.Visible = true;
                ucai.txtAIMConstant.Size = new Size(220, 21);




                ucai.lblEventClass.Visible = true;
                ucai.lblEventClass.Location = new Point(13, 186);
                ucai.cmbEventC.Location = new Point(119, 183);
                ucai.cmbEventC.Size = new Size(220, 21);
                ucai.cmbEventC.Visible = true;

                ucai.lblEV.Visible = true;
                ucai.lblEV.Location = new Point(13, 211);
                ucai.CmbEV.Location = new Point(119, 208);
                ucai.CmbEV.Size = new Size(220, 21);
                ucai.CmbEV.Visible = true;


                ucai.lblVariation.Visible = true;
                ucai.lblVariation.Location = new Point(13, 236);
                ucai.CmbVari.Location = new Point(119, 233);
                ucai.CmbVari.Size = new Size(220, 21);
                ucai.CmbVari.Visible = true;

                ucai.lblMDec.Location = new Point(13, 261);
                ucai.txtMapDescription.Location = new Point(119, 258);
                ucai.txtMapDescription.Visible = true; ucai.lblMDec.Visible = true;
                ucai.txtMapDescription.Size = new Size(220, 21);

                ucai.AutoMapRange.Visible = false;
                ucai.txtAutoMapM.Visible = false;
                ucai.AutoMapRange.Location = new Point(13, 286);
                ucai.txtAutoMapM.Location = new Point(119, 283);
                //ucai.txtAutoMapM.Visible = true;
                ucai.txtAutoMapM.Size = new Size(220, 21);

                ucai.chkUsed.Visible = false;

                ucai.btnAIMDone.Location = new Point(120, 290);
                ucai.btnAIMCancel.Location = new Point(210, 290);


                ucai.grpAIMap.Size = new Size(353, 330);
                ucai.grpAIMap.Location = new Point(350, 260);
                ucai.pbAIMHdr.Size = new Size(353, 22);


            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DNPSlave_OnLoad()
        {
            string strRoutineName = "AIConfiguration: DNPSlave_OnLoad";
            try
            {
                //Namrata: 21/10/2019
                ucai.lblKey.Visible = false;
                ucai.txtKey.Visible = false;
                ucai.lblUnit.Visible = false;
                ucai.txtUnit.Visible = false;
                //Namrata: 20/08/2019
                #region CellNo
                ucai.CmbCellNo.Visible = false;
                ucai.lblCellNo.Visible = false;
                #endregion CellNo

                #region Widget
                ucai.CmbWidget.Visible = false;
                ucai.lblWidget.Visible = false;
                #endregion Widget
                //Ajay: 04/09/2018 
                ucai.chkbxEvent.Visible = false;
                ucai.ChkIEC61850MapIndex.Visible = false;//Namrata: 16/04/2019
                ucai.lblANM.Location = new Point(13, 33);
                ucai.txtAIMNo.Location = new Point(119, 30);
                ucai.txtAIMNo.Size = new Size(220, 21);

                ucai.lblUnitID.Visible = false;
                ucai.txtUnitID.Visible = false;
             

                ucai.lblAMRI.Visible = true;//Namrata:28/03/2019
                ucai.txtAIMReportingIndex.Visible = true;//Namrata:28/03/2019
                ucai.lblAMRI.Location = new Point(13, 58);
                ucai.txtAIMReportingIndex.Location = new Point(119, 55);
                ucai.txtAIMReportingIndex.Size = new Size(220, 21);

                ucai.lblMapIndex.Visible = false;
                ucai.CmbReportingIndex.Visible = false;
                ucai.ChkIEC61850MapIndex.Visible = false;//Namrata:16/04/2019

                ucai.lblAMDT.Visible = false; ucai.cmbAIMDataType.Visible = false;


                ucai.lblCT.Visible = true;
                ucai.cmbAIMCommandType.Visible = true;
                ucai.lblCT.Location = new Point(13, 83);
                ucai.cmbAIMCommandType.Location = new Point(119, 80);
                ucai.cmbAIMCommandType.Size = new Size(220, 21);

                ucai.txtAIMDeadBand.Visible = true; ucai.lblAMDB.Visible = true;
                ucai.lblAMDB.Location = new Point(13, 109);
                ucai.txtAIMDeadBand.Location = new Point(119, 106);
                ucai.txtAIMDeadBand.Size = new Size(220, 21);
                

                ucai.lblAMM.Visible = true;
                ucai.txtAIMMultiplier.Visible = true;
                ucai.lblAMM.Location = new Point(13, 135);
                ucai.txtAIMMultiplier.Location = new Point(119, 132);
                ucai.txtAIMMultiplier.Size = new Size(220, 21);


                ucai.lblAMC.Location = new Point(13, 161);
                ucai.txtAIMConstant.Location = new Point(119, 158);
                ucai.txtAIMConstant.Visible = true; ucai.lblAMC.Visible = true;
                ucai.txtAIMConstant.Size = new Size(220, 21);

             


                ucai.lblEventClass.Visible = true;
                ucai.lblEventClass.Location = new Point(13, 186);
                ucai.cmbEventC.Location = new Point(119, 183);
                ucai.cmbEventC.Size = new Size(220, 21);
                ucai.cmbEventC.Visible = true;

                ucai.lblEV.Visible = true;
                ucai.lblEV.Location = new Point(13, 211);
                ucai.CmbEV.Location = new Point(119, 208);
                ucai.CmbEV.Size = new Size(220, 21);
                ucai.CmbEV.Visible = true;


                ucai.lblVariation.Visible = true;
                ucai.lblVariation.Location = new Point(13, 236);
                ucai.CmbVari.Location = new Point(119, 233);
                ucai.CmbVari.Size = new Size(220, 21);
                ucai.CmbVari.Visible = true;

                ucai.lblMDec.Location = new Point(13, 261);
                ucai.txtMapDescription.Location = new Point(119, 258);
                ucai.txtMapDescription.Visible = true; ucai.lblMDec.Visible = true;
                ucai.txtMapDescription.Size = new Size(220, 21);

                ucai.AutoMapRange.Visible = true;
                ucai.txtAutoMapM.Visible = true;
                ucai.AutoMapRange.Location = new Point(13, 286);
                ucai.txtAutoMapM.Location = new Point(119, 283);
                ucai.txtAutoMapM.Visible = true;
                ucai.txtAutoMapM.Size = new Size(220, 21);

                ucai.chkUsed.Visible = false;

                ucai.btnAIMDone.Location = new Point(120, 310);
                ucai.btnAIMCancel.Location = new Point(210, 310);


                ucai.grpAIMap.Size = new Size(353, 350);
                ucai.grpAIMap.Location = new Point(350,260);
                ucai.pbAIMHdr.Size = new Size(353, 22);


            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion All Slaves Events
        public bool IsAIExist(string RT, int iIndex)
        {
            return aiList.Where(x => x.Index == iIndex.ToString() && x.SubIndex == "0" && x.ResponseType == RT).Any();
        }

        public void ListToDataTable<NetworkInterface>(IList<NetworkInterface> varlist)
        {

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            if (typeof(NetworkInterface).IsValueType || typeof(NetworkInterface).Equals(typeof(string)))
            {
                DataColumn dc = new DataColumn("Image");
                dt.Columns.Add(dc);
                foreach (NetworkInterface item in varlist)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item;
                    dt.Rows.Add(dr);
                }
            }
            else
            {
                PropertyInfo[] propT = typeof(NetworkInterface).GetProperties(); //find all the public properties of this Type using reflection;
                foreach (PropertyInfo pi in propT)
                {
                    DataColumn dc = new DataColumn(pi.Name, pi.PropertyType);
                    dt.Columns.Add(dc);
                }
                for (int item = 0; item < varlist.Count(); item++)
                {
                    DataRow dr = dt.NewRow();
                    for (int property = 0; property < propT.Length; property++)
                    {
                        dr[property] = propT[property].GetValue(varlist[item], null);
                    }
                    dt.Rows.Add(dr);
                }
            }
            dt.AcceptChanges();
            ds.Tables.Add(dt);
            GDSlave.DTAIImage = dt;
            //return dt;
        }
    }
}
