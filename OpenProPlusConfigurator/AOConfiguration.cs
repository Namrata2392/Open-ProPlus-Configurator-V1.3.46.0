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
    public class AOConfiguration
    {
        #region Declarations
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        private RCBConfiguration RCBNode = null;
        List<string> IEC61850CheckedList = null;
        int intCheckItems = 0;
        DataSet dsdummy = null; //Ajay: 17/01/2019
        List<string> MergeList = null;
        List<string> ObjectRef = null;
        List<string> FC = null;
        //Namrata: 11/09/2017
        //Fill RessponseType in All Configuration. 
        public DataGridView dataGridViewDataSet = new DataGridView();
        public DataTable dtdataset = new DataTable();
        DataRow datasetRow;
        private string Response = "";
        private string ied = "";
        List<AOMap> slaveAIMapList;
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
        Dictionary<string, List<AOMap>> slavesAIMapList = new Dictionary<string, List<AOMap>>();
        private MasterTypes masterType = MasterTypes.UNKNOWN;
        private int masterNo = -1;
        private int IEDNo = -1;
        List<AO> aoList = new List<AO>();
        ucAOList ucao = new ucAOList();
        private const int COL_CMD_TYPE_WIDTH = 130;
        Configuration con = new Configuration();
        #endregion Declarations
        public AOConfiguration(MasterTypes mType, int mNo, int iNo)
        {
            string strRoutineName = "AOConfiguration";
            try
            {
                masterType = mType;
                masterNo = mNo;
                IEDNo = iNo;
                ucao.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                ucao.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                ucao.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucao.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucao.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                ucao.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                ucao.btnNextClick += new System.EventHandler(this.btnNext_Click);
                ucao.btnLastClick += new System.EventHandler(this.btnLast_Click);
                ucao.lnkbtnDeleteAllMap.Click += new System.EventHandler(this.linkLabel1_Click);
                ucao.lnkbtnDeleteAll.Click += new System.EventHandler(this.LinkDeleteConfigue_Click);
                //Namrata: 21/04/2018
                ucao.cmbIEDName.SelectedIndexChanged += new System.EventHandler(this.cmbIEDName_SelectedIndexChanged);
                ucao.cmb61850ResponseType.SelectedIndexChanged += new System.EventHandler(this.cmb61850ResponseType_SelectedIndexChanged);
                ucao.cmb61850Index.SelectedIndexChanged += new System.EventHandler(this.cmb61850Index_SelectedIndexChanged);
                ucao.lvAIlistItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvAIlist_ItemSelectionChanged);
                ucao.lvAIMapItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvAIMap_ItemSelectionChanged);

                if (mType == MasterTypes.Virtual)
                {
                    ucao.btnAdd.Enabled = false;
                    ucao.btnDelete.Enabled = false;
                }
                else
                {
                    ucao.lvAIlistDoubleClick += new System.EventHandler(this.lvAIlist_DoubleClick);
                }
                //Ajay: 26/07/2018 Separate condition handled for IEC104
                if ((masterType == MasterTypes.ADR) || (masterType == MasterTypes.IEC101) || (masterType == MasterTypes.IEC103) || (masterType == MasterTypes.MODBUS))
                {
                    ADR_IEC101_IEC103_Modbus_OnLoad();
                }
                //Ajay: 26/07/2018 
                if (masterType == MasterTypes.IEC104)
                {
                    IEC104_OnLoad();
                }
                if (masterType == MasterTypes.SPORT)
                {
                    SPORT_OnLoad();
                }
                //Namrata: 21/04/2018
                if (masterType == MasterTypes.IEC61850Client)
                {
                    IEC61850_OnLoad();
                }
                //Ajay: 31/07/2018 
                if (masterType == MasterTypes.LoadProfile)
                {
                    LoadProfile_OnLoad();
                }
                ucao.btnAIMDeleteClick += new System.EventHandler(this.btnAIMDelete_Click);
                ucao.btnAIMDoneClick += new System.EventHandler(this.btnAIMDone_Click);
                ucao.btnAIMCancelClick += new System.EventHandler(this.btnAIMCancel_Click);
                ucao.lvAIMapDoubleClick += new System.EventHandler(this.lvAIMap_DoubleClick);
                ucao.cmbFCSelectedIndexChanged += new System.EventHandler(this.cmbFC_SelectedIndexChanged); //Ajay: 17/01/2019
                ucao.ChkIEC61850Index.SelectedIndexChanged += new System.EventHandler(this.ChkIEC61850Index_SelectedIndexChanged);//Namrata:22/03/2019
                addListHeaders();
                fillOptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string strRoutineName = "AOConfiguration: btnAdd_Click";
            try
            {
                // Ajay: 07/12/2018
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    if (AllowMasterOptionsOnClick(masterType)) { return; }
                    else { }
                }
                if (aoList.Count >= getMaxAIs())
                {
                    MessageBox.Show("Maximum " + getMaxAIs() + " AI's are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                mode = Mode.ADD;
                editIndex = -1;
                Utils.resetValues(ucao.grpAO);
                Utils.showNavigation(ucao.grpAO, false);
                #region Events
                //Ajay: 26/07/2018 separate condition handled for IEC104
                if ((masterType == MasterTypes.ADR) || (masterType == MasterTypes.IEC101) || (masterType == MasterTypes.IEC103) || (masterType == MasterTypes.MODBUS))
                {
                    ADR_IEC101_IEC103_Modbus_OnLoad();
                }
                //Ajay: 26/07/2018
                if (masterType == MasterTypes.IEC104)
                {
                    IEC104_OnLoad();
                }
                if (masterType == MasterTypes.IEC61850Client)
                {
                    IEC61850_OnLoad();
                    FetchComboboxData();
                }
                if (masterType == MasterTypes.SPORT)
                {
                    SPORT_OnLoad();
                }
                //Ajay: 31/07/2018
                if (masterType == MasterTypes.LoadProfile)
                {
                    LoadProfile_OnLoad();
                }
                #endregion Events
                //Ajay: 12/11/2018
                fillOptions();
                loadDefaults();
                ucao.grpAO.Visible = true;
                ucao.cmbResponseType.Focus();
                //Namrata: 04/04/2018
                if (masterType == MasterTypes.IEC61850Client)
                {
                    if (ucao.cmbIEDName.SelectedIndex != -1)
                    {
                        //Namrata: 25/03/2019
                        ucao.ChkIEC61850Index.Text = "";
                        if (IEC61850CheckedList != null)
                        {
                            ucao.ChkIEC61850Index.CheckBoxItems.ForEach(x =>
                            {
                                x.Checked = false; x.CheckState = CheckState.Unchecked;
                            });
                        }
                        ucao.cmbIEDName.SelectedIndex = ucao.cmbIEDName.FindStringExact(Utils.Iec61850IEDname);
                        //Namrata: 10/04/2018
                        if ((ucao.cmb61850Index.Text.ToString() == "") || (ucao.cmb61850ResponseType.Text.ToString() == ""))
                        {
                            MessageBox.Show("Fields cannot empty", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        ucao.grpAO.Visible = false;
                        MessageBox.Show("ICD File Missing !!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            string strRoutineName = "AOConfiguration:btnDone_Click";
            try
            {
                Utils.DummyslaveAIMapList1.Clear();
                List<KeyValuePair<string, string>> aiData = Utils.getKeyValueAttributes(ucao.grpAO);
                #region fill Address to Datatable for RCBConfiguration
                //Namrata: 27/09/2017
                if (masterType == MasterTypes.IEC61850Client)
                {
                    IEC61850CheckedList = ucao.ChkIEC61850Index.CheckBoxItems.Where(x => x.Checked == true).Select(x => x.Text).ToList();//Namrata:25/03/2019
                    Response = ucao.cmb61850ResponseType.Text;
                    ied = IEDNo.ToString(); DataColumn dcAddressColumn;
                    //Namrata: 15/03/2018
                    if (!dtdataset.Columns.Contains("Address"))
                    { dcAddressColumn = dtdataset.Columns.Add("Address", typeof(string)); }
                    if (!dtdataset.Columns.Contains("IED"))
                    { dtdataset.Columns.Add("IED", typeof(string)); }
                    datasetRow = dtdataset.NewRow();
                    datasetRow["Address"] = Response.ToString();
                    datasetRow["IED"] = IEDNo.ToString();
                    dtdataset.Rows.Add(datasetRow);
                    Utils.dtDataSet = dtdataset;
                    //Namrata: 15/03/2018
                    dataGridViewDataSet.DataSource = Utils.dtDataSet;
                    Utils.dtDataSet.TableName = Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname;
                    string Index112 = "";
                    DataRow row112;
                    if (Utils.dsRCBAO.Tables.Contains(Utils.dtDataSet.TableName))
                    {
                        Utils.dsRCBAO.Tables[Utils.dtDataSet.TableName].Clear();
                    }
                    else
                    {
                        Utils.dsRCBAO.Tables.Add(Utils.dtDataSet.TableName);
                        Utils.dsRCBAO.Tables[Utils.dtDataSet.TableName].Columns.Add("ObjectReferrence");
                        Utils.dsRCBAO.Tables[Utils.dtDataSet.TableName].Columns.Add("Node");
                    }
                    for (int i = 0; i < Utils.dtDataSet.Rows.Count; i++)
                    {
                        row112 = Utils.dsRCBAO.Tables[Utils.dtDataSet.TableName].NewRow();
                        Utils.dsRCBAO.Tables[Utils.dtDataSet.TableName].NewRow();
                        for (int j = 0; j < Utils.dtDataSet.Columns.Count; j++)
                        {
                            Index112 = Utils.dtDataSet.Rows[i][j].ToString();
                            row112[j] = Index112.ToString();
                        }
                        Utils.dsRCBAO.Tables[Utils.dtDataSet.TableName].Rows.Add(row112);
                    }
                    Utils.dsRCBData = Utils.dsRCBAO;
                    Utils.dsRCBData.Merge(Utils.dsRCBAI, false, MissingSchemaAction.Add);
                    Utils.dsRCBData.Merge(Utils.dsRCBDI, false, MissingSchemaAction.Add);
                    Utils.dsRCBData.Merge(Utils.dsRCBDO, false, MissingSchemaAction.Add);
                    Utils.dsRCBData.Merge(Utils.dsRCBEN, false, MissingSchemaAction.Add);
                }
                #endregion fill Address to Datatable for RCBConfiguration
                if (mode == Mode.ADD)
                {
                    int intAONO = Convert.ToInt32(aiData[12].Value); //AINo
                    int AONo = 0, ReportingIndex = 0;
                    string Description = "";
                    int intAutoRange = 0;

                    if (IEC61850CheckedList != null)
                    {
                        intCheckItems = IEC61850CheckedList.Count();
                    }

                    //Ajay: 31/07/2018
                    if (masterType == MasterTypes.LoadProfile) { intAutoRange = 1; }
                    else { intAutoRange = Convert.ToInt32(aiData[6].Value); }

                    //Ajay: 31/07/2018
                    int intReportingIndex = 0;
                    if (masterType != MasterTypes.LoadProfile) { intReportingIndex = Convert.ToInt32(aiData[14].Value); }

                    //Namrata: 23/11/2017
                    if (intAutoRange > getMaxAIs())
                    {
                        MessageBox.Show("Maximum " + getMaxAIs() + " AO's are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        #region IEC61850Client
                        if (masterType == MasterTypes.IEC61850Client)
                        {
                            int iListCount = 0;
                            for (int i = intAONO; i <= intAONO + intCheckItems - 1; i++)
                            {

                                Description = "";
                                if (i == Globals.AONo)
                                {
                                    AONo = Globals.AONo;
                                    Description = ucao.txtDescription.Text;
                                }
                                else
                                {
                                    AONo = i;
                                    Description = ucao.txtDescription.Text;
                                }
                                AO NewAO = new AO("AO", aiData, null, masterType, masterNo, IEDNo);
                                NewAO.AONo = AONo.ToString();
                                NewAO.Description = Description;
                                NewAO.IEDName = ucao.cmbIEDName.Text.ToString();
                                NewAO.IEC61850Index = IEC61850CheckedList[iListCount].ToString();
                                NewAO.IEC61850ResponseType = ucao.cmb61850ResponseType.Text.ToString();
                                string FindFC = MergeList.Where(x => x.Contains(IEC61850CheckedList[iListCount].ToString() + ",")).Select(x => x).FirstOrDefault();
                                string[] GetFC = FindFC.Split(',');
                                ucao.cmbFC.SelectedIndex = ucao.cmbFC.FindStringExact(GetFC[1].ToString());
                                NewAO.FC = ucao.cmbFC.Text.ToString();
                                aoList.Add(NewAO);
                                iListCount++;
                            }
                        }
                        #endregion IEC61850Client

                        #region OtherMasters
                        else
                        {
                            for (int i = intAONO; i <= intAONO + intAutoRange - 1; i++)
                            {
                                Description = "";
                                if ((i == Globals.AONo) && (i == Globals.AIIndex))
                                {
                                    AONo = Globals.AINo;
                                    ReportingIndex = intReportingIndex++;
                                    if (masterType == MasterTypes.ADR)
                                    {
                                        Description = ucao.txtDescription.Text;
                                    }
                                    else if (masterType == MasterTypes.IEC101)
                                    {
                                        Description = ucao.txtDescription.Text;
                                    }
                                    else if (masterType == MasterTypes.IEC104)
                                    {
                                        Description = ucao.txtDescription.Text;
                                    }
                                    else if (masterType == MasterTypes.IEC103)
                                    {
                                        Description = ucao.txtDescription.Text;
                                    }
                                    else if (masterType == MasterTypes.MODBUS)
                                    {
                                        Description = ucao.txtDescription.Text;
                                    }
                                    else if (masterType == MasterTypes.Virtual)
                                    {
                                        Description = ucao.txtDescription.Text;
                                    }
                                    else if (masterType == MasterTypes.SPORT)
                                    {
                                        Description = ucao.txtDescription.Text;
                                    }
                                    //Ajay: 31/07/2018
                                    else if (masterType == MasterTypes.LoadProfile)
                                    {
                                        Description = ucao.txtDescription.Text;
                                    }
                                }
                                else
                                {
                                    AONo = i;
                                    ReportingIndex = intReportingIndex++;
                                    if (masterType == MasterTypes.ADR)
                                    {
                                        Description = ucao.txtDescription.Text;
                                    }
                                    else if (masterType == MasterTypes.IEC101)
                                    {
                                        Description = ucao.txtDescription.Text;
                                    }
                                    else if (masterType == MasterTypes.IEC103)
                                    {
                                        Description = ucao.txtDescription.Text;
                                    }
                                    else if (masterType == MasterTypes.IEC104)
                                    {
                                        Description = ucao.txtDescription.Text;
                                    }
                                    else if (masterType == MasterTypes.MODBUS)
                                    {
                                        Description = ucao.txtDescription.Text;
                                    }
                                    else if (masterType == MasterTypes.Virtual)
                                    {
                                        Description = ucao.txtDescription.Text;
                                    }
                                    else if (masterType == MasterTypes.SPORT)
                                    {
                                        Description = ucao.txtDescription.Text;
                                    }
                                    //Ajay: 31/07/2018
                                    else if (masterType == MasterTypes.LoadProfile)
                                    {
                                        Description = ucao.txtDescription.Text;
                                    }
                                }
                                AO NewAI = new AO("AO", aiData, null, masterType, masterNo, IEDNo);
                                NewAI.AONo = AONo.ToString();
                                NewAI.Index = ReportingIndex.ToString();
                                NewAI.Description = Description;
                                aoList.Add(NewAI);
                            }
                        }
                        #endregion OtherMasters
                    }
                }

                else if (mode == Mode.EDIT)
                {
                    if (ucao.ChkIEC61850Index.CheckBoxItems.Count > 0)
                    {
                        if (ucao.ChkIEC61850Index.CheckBoxItems.Where(x => x.Checked == true).Count() > 1)
                        {
                            MessageBox.Show("Please select only 1 index.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            aoList[editIndex].updateAttributes(aiData);
                        }
                    }
                    else
                    {
                        aoList[editIndex].updateAttributes(aiData);
                    }

                }
                refreshList();
                //Namrata: 15/03/2018
                if (masterType == MasterTypes.IEC61850Client)
                {
                    RCBNode = new RCBConfiguration(MasterTypes.IEC61850Client, Convert.ToInt32(Utils.MasterNumForIEC61850Client), Convert.ToInt32(Utils.UnitIDForIEC61850Client));
                    RCBNode.FillRCBList();
                }
                //Namrata: 27/7/2017 
                //To Change Multiple Records at A Time .
                if (sender != null && e != null)
                {
                    ucao.grpAO.Visible = false;
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
            string strRoutineName = "AOConfiguration: btnCancel_Click";
            try
            {
                ucao.grpAO.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                Utils.resetValues(ucao.grpAO);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            string strRoutineName = "AOConfiguration: btnFirst_Click";
            try
            {
                //Namrata:27/7/2017
                if (ucao.lvAIlist.Items.Count <= 0) return;
                if (aoList.ElementAt(0).IsNodeComment) return;
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
            string strRoutineName = "AOConfiguration: btnPrev_Click";
            try
            {
                //Namrata: 27/7/2017
                btnDone_Click(null, null);
                if (editIndex - 1 < 0) return;
                if (aoList.ElementAt(editIndex - 1).IsNodeComment) return;
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
            string strRoutineName = "AOConfiguration:btnNext_Click";
            try
            {
                //Namrata: 27/7/2017
                btnDone_Click(null, null);
                if (editIndex + 1 >= ucao.lvAIlist.Items.Count) return;
                if (aoList.ElementAt(editIndex + 1).IsNodeComment) return;
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
            string strRoutineName = "AOConfiguration:btnLast_Click";
            try
            {
                if (ucao.lvAIlist.Items.Count <= 0) return;
                if (aoList.ElementAt(aoList.Count - 1).IsNodeComment) return;
                editIndex = aoList.Count - 1;
                loadValues();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbIEDName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "AOConfiguration: cmbIEDName_SelectedIndexChanged";
            try
            {
                //Namrata: 04/04/2018
                if (ucao.cmbIEDName.Focused == false) { }
                else
                {
                    Utils.Iec61850IEDname = ucao.cmbIEDName.Text;
                    List<DataTable> dtList = Utils.dsResponseType.Tables.OfType<DataTable>().Where(tbl => tbl.TableName.StartsWith(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname)).ToList();
                    if (dtList.Count == 0)
                    {
                        ucao.cmb61850ResponseType.DataSource = null;
                        ucao.cmb61850Index.DataSource = null;
                        ucao.cmb61850ResponseType.Enabled = false;
                        ucao.cmb61850Index.Enabled = false;
                        // Ajay: 17/01/2019
                        ucao.cmbFC.DataSource = null;
                    }
                    else
                    {
                        ucao.cmb61850ResponseType.Enabled = true;
                        ucao.cmb61850Index.Enabled = true;
                        ucao.cmb61850ResponseType.DataSource = Utils.dsResponseType.Tables[Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname];//[Utils.strFrmOpenproplusTreeNode + "/" + "Undefined" + "/" + Utils.Iec61850IEDname];
                        ucao.cmb61850ResponseType.DisplayMember = "Address";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmb61850ResponseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "AOConfiguration:cmb61850ResponseType_SelectedIndexChanged";
            try
            {
                if (ucao.cmb61850ResponseType.Items.Count > 1)
                {
                    if ((ucao.cmb61850ResponseType.SelectedIndex != -1))
                    {
                        Utils.Iec61850IEDname = ucao.cmbIEDName.Text;//Namrata: 04/04/2018
                        List<DataTable> dtList = Utils.DsAllConfigurationData.Tables.OfType<DataTable>().Where(tbl => tbl.TableName.StartsWith(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname)).ToList();
                        dsdummy = new DataSet();
                        dtList.ForEach(tbl => { DataTable dt = tbl.Copy(); dsdummy.Tables.Add(dt); });
                        ucao.cmbFC.DataSource = dsdummy.Tables[ucao.cmb61850ResponseType.SelectedIndex].AsEnumerable().Select(x => x.Field<string>("FC")).Distinct().ToList();//Ajay: 17/01/2019
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void FetchCheckboxData()
        {
            string strRoutineName = "AOConfiguration:FetchCheckboxData";
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
                    ucao.ChkIEC61850Index.Items.Clear();
                    foreach (var kv in ObjectRef)
                    {
                        ucao.ChkIEC61850Index.Items.Add(kv.ToString());
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
            string strRoutineName = "AOConfiguration:ChkIEC61850Index_SelectedIndexChanged";
            try
            {
                if (ucao.ChkIEC61850Index.Items.Count > 0)
                {
                    if (ucao.ChkIEC61850Index.SelectedIndex != -1)
                    {
                        string a = ucao.ChkIEC61850Index.Text;
                        string FindObjRef = MergeList.Where(x => x.Contains(a.ToString() + ",")).Select(x => x).FirstOrDefault();
                        string[] GetFC = FindObjRef.Split(',');
                        ucao.cmbFC.SelectedIndex = ucao.cmbFC.FindStringExact(GetFC[1].ToString());
                        Utils.IEC61850Index = GetFC[0].ToString();
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
            string strRoutineName = "AI : cmb61850Index_SelectedIndexChanged";
            try
            {
                if (ucao.cmb61850Index.Items.Count > 0)
                {
                    if (ucao.cmb61850Index.SelectedIndex != -1)
                    {
                        Utils.IEC61850Index = (((DataRowView)ucao.cmb61850Index.SelectedItem).Row[0].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 17/01/2019
        private void cmbFC_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "AOConfiguration: cmbFC_SelectedIndexChanged";
            try
            {
                string FC = ucao.cmbFC.Text;
                DataTable DT = FilteredIndexDT(FC);
                if (DT != null)
                {
                    //Namrata: 25/03/2019
                    List<string> FC1 = DT.AsEnumerable().Select(x => x[0].ToString()).ToList();
                    ucao.ChkIEC61850Index.Items.Clear();
                    //Namrata: 25/03/2019
                    ucao.ChkIEC61850Index.Text = "";//Namrata: 25/03/2019
                    foreach (var kv in FC1)
                    {
                        ucao.ChkIEC61850Index.Items.Add(kv.ToString());
                    }
                    ucao.cmb61850Index.DataSource = DT;
                    ucao.cmb61850Index.DisplayMember = "ObjectReferrence";
                    ucao.cmb61850Index.ValueMember = "Node";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 17/01/2019
        private DataTable FilteredIndexDT(string FC)
        {
            string strRoutineName = "AOConfiguration:FilteredIndexDT";
            try
            {
                DataTable DT = new DataTable();
                DT.Columns.Add("ObjectReferrence");
                DT.Columns.Add("Node");
                DT.Columns.Add("FC");
                DataRow[] drRwArry = dsdummy.Tables[ucao.cmb61850ResponseType.SelectedIndex].AsEnumerable().Where(x => x.Field<string>("FC") == FC).ToArray();
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
        private void linkLabel1_Click(object sender, EventArgs e)
        {
            string strRoutineName = "AOConfiguration:linkLabel1_Click";
            try
            {
                // Ajay: 07/12/2018
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    if (AllowSlaveOptionsOnClick(Utils.getSlaveTypes(currentSlave))) { return; }
                    else { }
                }
                List<AOMap> slaveAIMapList;
                if (!slavesAIMapList.TryGetValue(currentSlave, out slaveAIMapList))
                {
                    MessageBox.Show("Error Deleting AO map!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                foreach (ListViewItem listItem in ucao.lvAIMap.Items)
                {
                    listItem.Checked = true;
                }
                DialogResult result = MessageBox.Show("Do You Want To Delete All Records ? ", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                {
                    foreach (ListViewItem listItem in ucao.lvAIMap.Items)
                    {
                        listItem.Checked = false;
                    }
                    return;
                }
                for (int i = ucao.lvAIMap.Items.Count - 1; i >= 0; i--)
                {
                    Console.WriteLine("*** removing indices: {0}", i);
                    slaveAIMapList.RemoveAt(i);
                    ucao.lvAIMap.Items.Clear();
                }
                refreshMapList(slaveAIMapList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FetchComboboxData()
        {
            string strRoutineName = "AOConfiguration:FetchComboboxData";
            try
            {
                //Namrata: 13/03/2018
                ucao.cmbIEDName.DataSource = null;
                List<string> tblNameList = Utils.dsIED.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                string tblName = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
                //Namrata: 26/04/2018
                if (tblName != null)
                {
                    ucao.cmbIEDName.DataSource = Utils.dsIED.Tables[tblName];
                    ucao.cmbIEDName.DisplayMember = "IEDName";
                    //Namrata: 21/03/2018
                    ucao.cmb61850ResponseType.DataSource = Utils.dsResponseType.Tables[tblName];
                    ucao.cmb61850ResponseType.DisplayMember = "Address";
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
        private void LinkDeleteConfigue_Click(object sender, EventArgs e)
        {
            string strRoutineName = "AOConfiguration:LinkDeleteConfigue_Click";
            try
            {
                // Ajay: 07/12/2018
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    if (AllowMasterOptionsOnClick(masterType)) { return; }
                    else { }
                }

                foreach (ListViewItem listItem in ucao.lvAIlist.Items)
                {
                    listItem.Checked = true;
                }
                DialogResult result = MessageBox.Show("Do You Want To Delete All Records ? ", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                {
                    foreach (ListViewItem listItem in ucao.lvAIlist.Items)
                    {
                        listItem.Checked = false;
                    }
                    return;
                }
                for (int i = ucao.lvAIlist.Items.Count - 1; i >= 0; i--)
                {
                    if (Utils.IsExistAIinPLC(aoList.ElementAt(i).AONo))
                    {
                        DialogResult ask = MessageBox.Show("AO " + aoList.ElementAt(i).AONo + " is referred in ParameterLoadConfiguration and all the references will also be deleted." + Environment.NewLine + "Do you want to continue?", "Delete AI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (ask == DialogResult.No)
                        {
                            continue;
                        }
                        Utils.DeleteAIFromPLC(aoList.ElementAt(i).AONo);
                    }
                    deleteAIFromMaps(aoList.ElementAt(i).AONo);
                    aoList.RemoveAt(i);
                    ucao.lvAIlist.Items.Clear();
                }
                refreshList();
                refreshCurrentMapList();//Refresh map listview...
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strRoutineName = "AOConfiguration: btnDelete_Click";
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
                for (int i = ucao.lvAIlist.Items.Count - 1; i >= 0; i--)
                {
                    if (ucao.lvAIlist.Items[i].Checked)
                    {
                        if (Utils.IsExistAIinPLC(aoList.ElementAt(i).AONo))
                        {
                            DialogResult ask = MessageBox.Show("AO " + aoList.ElementAt(i).AONo + " is referred in ParameterLoadConfiguration and all the references will also be deleted." + Environment.NewLine + "Do you want to continue?", "Delete AO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (ask == DialogResult.No)
                            {
                                continue;
                            }
                            Utils.DeleteAIFromPLC(aoList.ElementAt(i).AONo);
                        }
                        deleteAIFromMaps(aoList.ElementAt(i).AONo);
                        aoList.RemoveAt(i);
                        ucao.lvAIlist.Items[i].Remove();
                    }
                }
                refreshList();
                refreshCurrentMapList(); //Refresh map listview...
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAIMDone_Click(object sender, EventArgs e)
        {
            string strRoutineName = "AOConfiguration: btnAIMDone_Click";
            try
            {
                List<KeyValuePair<string, string>> aimData = Utils.getKeyValueAttributes(ucao.grpAIMap);
                int intAINo = Convert.ToInt32(aimData[11].Value);
                int intAutoMapRange = Convert.ToInt32(aimData[6].Value);
                int intReportingIndex = Convert.ToInt32(aimData[12].Value);

                //Namrata:24/7/2017
                int intModbusSlaveIndex = Convert.ToInt32(aimData[12].Value);
                int AINo = 0, ReportingIndex = 0;

                //Namrata:8/7/2017
                ListViewItem ListViewIndex = ucao.lvAIlist.Items.Cast<ListViewItem>().FirstOrDefault(x => x.Text == intAINo.ToString()); //Find Index Of ListView
                string ind = ucao.lvAIlist.Items.IndexOf(ListViewIndex).ToString();

                //Namrata:31/7/2017
                ListViewItem ExistAIMap = ucao.lvAIMap.FindItemWithText(ucao.txtAIMNo.Text);  //Eliminate Duplicate Record From lvAIMAp List
                if (!ValidateMap()) return;
                if (!slavesAIMapList.TryGetValue(currentSlave, out slaveAIMapList))
                {
                    MessageBox.Show("Error adding AO map for slave!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (mapMode == Mode.ADD)
                {
                    if (aoList.Count >= 0)
                    {
                        if ((intAutoMapRange + Convert.ToInt16(ind)) > ucao.lvAIlist.Items.Count)
                        {
                            MessageBox.Show("Slave Entry Does Not Exist", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            for (int i = intAINo; i <= intAINo + intAutoMapRange - 1; i++)
                            {
                                //Ajay: 21/11/2017
                                if (aoList.Select(x => x.AONo).ToList().Contains(i.ToString()))
                                {
                                    AINo = i;
                                    if ((Utils.getSlaveTypes(currentSlave) != SlaveTypes.MODBUSSLAVE))
                                    {
                                        ReportingIndex = intReportingIndex++;
                                    }
                                    if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE))
                                    {
                                        if ((ucao.cmbDataType.Text == "UnsignedInt32_LSB_MSB") || (ucao.cmbDataType.Text == "SignedInt32_LSB_MSB") || (ucao.cmbDataType.Text == "UnsignedInt32_MSB_LSB") || (ucao.cmbDataType.Text == "SignedInt32_MSB_LSB") || (ucao.cmbDataType.Text == "Float_LSB_MSB") || (ucao.cmbDataType.Text == "Float_MSB_LSB") || (ucao.cmbDataType.Text == "UnsignedLong32_Bit_MSWord_LSWord") || (ucao.cmbDataType.Text == "UnsignedLong32_Bit_LSWord_MSWord") || (ucao.cmbDataType.Text == "SignedLong32_Bit_MSWord_LSWord") || (ucao.cmbDataType.Text == "SignedLong32_Bit_LSWord_MSWord") || (ucao.cmbDataType.Text == "Float_MSWord_LSWord") || (ucao.cmbDataType.Text == "Float_LSWord_MSWord"))
                                        {
                                            if (slaveAIMapList.Count == 0)
                                            {
                                                ReportingIndex = Convert.ToInt32(ucao.txtAIMReportingIndex.Text);
                                            }
                                            else
                                            {
                                                ReportingIndex += 2;
                                            }
                                        }
                                    }
                                    AOMap NewAIMap = new AOMap("AO", aimData, Utils.getSlaveTypes(currentSlave));
                                    NewAIMap.AONo = AINo.ToString();
                                    NewAIMap.Description = aimData[5].Value.ToString();//getDescription(ucao.lvAIlist,AINo.ToString());
                                    NewAIMap.ReportingIndex = ReportingIndex.ToString();
                                    slaveAIMapList.Add(NewAIMap);
                                }
                                else
                                {
                                    intAutoMapRange++;
                                }
                            }
                        }
                    }
                }
                else if (mapMode == Mode.EDIT)
                {
                    slaveAIMapList[mapEditIndex].updateAttributes(aimData);
                }
                refreshMapList(slaveAIMapList);
                ucao.grpAIMap.Visible = false;
                mapMode = Mode.NONE;
                mapEditIndex = -1;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvAIlist_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "AOConfiguration: lvAOlist_DoubleClick";
            try
            {
                // Ajay: 07/12/2018
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    if (AllowMasterOptionsOnClick(masterType)) { return; }
                    else { }
                }

                int ListIndex = ucao.lvAIlist.FocusedItem.Index;
                //Namrata: 10/09/2017
                ucao.txtAIAutoMapRange.Text = "0";
                ListViewItem lvi = ucao.lvAIlist.Items[ListIndex];//ucai.lvAIlist.SelectedItems[0];
                Utils.UncheckOthers(ucao.lvAIlist, lvi.Index);
                if (aoList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #region Enable/Disable Events
                //Ajay: 27/06/2018
                if (masterType == MasterTypes.IEC104)
                {
                    IEC104_OnDoubleClick(); //Ajay: 26/07/2018
                }
                else if ((masterType == MasterTypes.ADR) || (masterType == MasterTypes.IEC101) || (masterType == MasterTypes.IEC103) || (masterType == MasterTypes.MODBUS))
                {
                    ADR_IEC101_IEC103_Modbus_OnDoubleClick();
                }
                else if ((masterType == MasterTypes.SPORT))
                {
                    SPORT_OnDoubleClick();
                }
                //Ajay: 31/07/2018
                else if (masterType == MasterTypes.LoadProfile)
                {
                    LoadProfile_OnDoubleClick();
                }
                else if (masterType == MasterTypes.IEC61850Client)
                {
                    IEC61850_OnDoubleClick();
                    FetchComboboxData();
                    //Namrata: 04/04/2018
                    ucao.cmbIEDName.SelectedIndex = ucao.cmbIEDName.FindStringExact(Utils.Iec61850IEDname);
                }
                #endregion Enable/Disable Events
                ucao.grpAO.Visible = true;
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                //Namrata:25/03/2019
                ucao.ChkIEC61850Index.Text = "";
                if (IEC61850CheckedList != null)
                {
                    ucao.ChkIEC61850Index.CheckBoxItems.ForEach(x =>
                    {
                        x.Checked = false; x.CheckState = CheckState.Unchecked;
                    });
                }
                if (ucao.ChkIEC61850Index.CheckBoxItems.Count > 0)
                {
                    ucao.ChkIEC61850Index.Text = aoList[lvi.Index].IEC61850Index.ToString();
                }
                Utils.showNavigation(ucao.grpAO, true);
                loadValues();
                if (ucao.ChkIEC61850Index.CheckBoxItems.Count > 0)
                {
                    //Namrata:26/03/2019
                    ucao.ChkIEC61850Index.CheckBoxItems.Where(x => x.Text == aoList[lvi.Index].IEC61850Index.ToString()).Take(1).ToList().ForEach(x =>
                {
                    x.Checked = true; x.CheckState = CheckState.Checked;
                });
                }

                ucao.cmbResponseType.Focus();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvAIlist_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string strRoutineName = "AOConfiguration: lvAIlist_ItemSelectionChanged";
            try
            {
                if (e.IsSelected)
                {
                    Color GreenColour = Color.FromArgb(34, 217, 0);
                    string diIndex = e.Item.Text;
                    Console.WriteLine("*** selected DI: {0}", diIndex);
                    //Namrata: 27/7/2017
                    ucao.lvAIMapItemSelectionChanged -= new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvAIMap_ItemSelectionChanged);
                    ucao.lvAIMap.SelectedItems.Clear();   //Remove selection from DIMap...
                    ucao.lvAIMap.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window); //Namrata: 07/04/2018
                    ucao.lvAIMap.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour)); //Namrata: 07/04/2018
                    Utils.highlightListviewItem(diIndex, ucao.lvAIMap);
                    //Namrata: 27/7/2017
                    ucao.lvAIMap.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);//Namrata: 07/04/2018
                    ucao.lvAIlist.SelectedItems.Clear();
                    ucao.lvAIlist.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                    ucao.lvAIlist.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                    ucao.lvAIlist.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);
                    ucao.lvAIMapItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvAIMap_ItemSelectionChanged);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvAIMap_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string strRoutineName = "AOConfiguration: lvAIMap_ItemSelectionChanged";
            try
            {
                if (e.IsSelected)
                {
                    Color GreenColour = Color.FromArgb(34, 217, 0);
                    string diIndex = e.Item.Text;
                    Console.WriteLine("*** selected DI: {0}", diIndex);
                    //Namrata: 27/7/2017
                    ucao.lvAIlistItemSelectionChanged -= new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvAIlist_ItemSelectionChanged);
                    ucao.lvAIlist.SelectedItems.Clear();   //Remove selection from DIMap...
                    ucao.lvAIlist.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window); //Namrata: 07/04/2018
                    ucao.lvAIlist.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour)); //Namrata: 07/04/2018
                    Utils.highlightListviewItem(diIndex, ucao.lvAIlist);
                    //Namrata:lvAIlist 27/7/2017
                    ucao.lvAIlist.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);//Namrata: 07/04/2018
                    ucao.lvAIMap.SelectedItems.Clear();
                    ucao.lvAIMap.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                    ucao.lvAIMap.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                    ucao.lvAIMap.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);
                    ucao.lvAIlistItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvAIlist_ItemSelectionChanged);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadDefaults()
        {
            string strRoutineName = "AOConfiguration: loadDefaults";
            try
            {
                ucao.txtIndex.Clear();
                ucao.txtAONo.Text = (Globals.AONo + 1).ToString();
                ucao.txtAIAutoMapRange.Text = "1";
                ucao.txtSubIndex.Text = "1";
                ucao.txtMultiplier.Text = "1";
                ucao.txtConstant.Text = "0";
                ucao.txtIndex.Text = "1";
                if (masterType == MasterTypes.ADR)
                {
                    if (ucao.lvAIlist.Items.Count - 1 >= 0)
                    {
                        ucao.txtIndex.Text = Convert.ToString(Convert.ToInt32(aoList[aoList.Count - 1].Index) + 1);
                    }
                    ucao.cmbResponseType.SelectedIndex = ucao.cmbResponseType.FindStringExact("ADR_AO");
                    ucao.txtDescription.Text = "ADR_AO";// + (Globals.AINo + 1).ToString();
                }
                else if (masterType == MasterTypes.IEC101)
                {
                    if (ucao.lvAIlist.Items.Count - 1 >= 0)
                    {
                        ucao.txtIndex.Text = Convert.ToString(Convert.ToInt32(aoList[aoList.Count - 1].Index) + 1);
                    }
                    ucao.cmbResponseType.SelectedIndex = ucao.cmbResponseType.FindStringExact("ShortFloatingPoint");
                    ucao.txtDescription.Text = "IEC103_AO";
                }
                else if (masterType == MasterTypes.SPORT)
                {
                    if (ucao.lvAIlist.Items.Count - 1 >= 0)
                    {
                        ucao.txtIndex.Text = Convert.ToString(Convert.ToInt32(aoList[aoList.Count - 1].Index) + 1);
                    }
                    //Ajay: 12/11/2018
                    if (ucao.cmbResponseType != null && ucao.cmbResponseType.Items.Count > 0)
                    {
                        ucao.cmbResponseType.SelectedIndex = 0; // ucao.cmbResponseType.FindStringExact("ShortFloatingPoint");
                    }
                    ucao.txtDescription.Text = "SPORT_AO";
                }
                else if (masterType == MasterTypes.IEC103)
                {
                    if (ucao.lvAIlist.Items.Count - 1 >= 0)
                    {
                        ucao.txtIndex.Text = Convert.ToString(Convert.ToInt32(aoList[aoList.Count - 1].Index) + 1);
                    }
                    ucao.cmbResponseType.SelectedIndex = ucao.cmbResponseType.FindStringExact("Measurand_II");
                    ucao.txtDescription.Text = "IEC103_AO";
                }
                else if (masterType == MasterTypes.IEC104)
                {
                    if (ucao.lvAIlist.Items.Count - 1 >= 0)
                    {
                        ucao.txtIndex.Text = Convert.ToString(Convert.ToInt32(aoList[aoList.Count - 1].Index) + 1);
                    }
                    ucao.cmbResponseType.SelectedIndex = ucao.cmbResponseType.FindStringExact("ShortFloatingPoint");
                    ucao.txtDescription.Text = "IEC104_AO";
                }
                else if (masterType == MasterTypes.MODBUS)
                {
                    if (ucao.lvAIlist.Items.Count - 1 >= 0)
                    {
                        ucao.txtIndex.Text = Convert.ToString(Convert.ToInt32(aoList[aoList.Count - 1].Index) + 1);
                    }
                    //ucai.txtIndex.Text = (Globals.AIIndex + 1).ToString();
                    ucao.cmbResponseType.SelectedIndex = ucao.cmbResponseType.FindStringExact("WriteSingleRegister");
                    ucao.txtDescription.Text = "MODBUS_AO";// + (Globals.AINo + 1).ToString();
                }
                else if (masterType == MasterTypes.IEC61850Client)
                {
                    if (ucao.lvAIlist.Items.Count - 1 >= 0)
                    {
                        ucao.txtIndex.Text = Convert.ToString(Convert.ToInt32(aoList[aoList.Count - 1].Index) + 1);
                    }
                    ucao.txtDescription.Text = "IEC61850_AO";// + (Globals.AINo + 1).ToString();
                }
                else if (masterType == MasterTypes.Virtual)
                {
                    if (ucao.lvAIlist.Items.Count - 1 >= 0)
                    {
                        ucao.txtIndex.Text = Convert.ToString(Convert.ToInt32(aoList[aoList.Count - 1].Index) + 1);
                    }
                    ucao.cmbResponseType.SelectedIndex = ucao.cmbResponseType.FindStringExact("PLU_AI");
                    ucao.txtDescription.Text = "Virtual_AO";
                }
                //Ajay: 31/07/2018
                else if (masterType == MasterTypes.LoadProfile)
                {
                    if (ucao.lvAIlist.Items.Count - 1 >= 0)
                    {
                        ucao.txtIndex.Text = Convert.ToString(Convert.ToInt32(aoList[aoList.Count - 1].Index) + 1);
                    }
                    ucao.txtDescription.Text = "LoadProfile_AO";
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadValues()
        {
            string strRoutineName = "AOConfiguration: loadValues";
            try
            {
                AO ao = aoList.ElementAt(editIndex);
                if (ao != null)
                {
                    ucao.txtAONo.Text = ao.AONo;
                    ucao.cmbResponseType.SelectedIndex = ucao.cmbResponseType.FindStringExact(ao.ResponseType);
                    ucao.txtIndex.Text = ao.Index;
                    ucao.txtSubIndex.Text = ao.SubIndex;
                    ucao.cmbDataType.SelectedIndex = ucao.cmbDataType.FindStringExact(ao.DataType);
                    ucao.txtMultiplier.Text = ao.Multiplier;
                    ucao.txtConstant.Text = ao.Constant;
                    ucao.txtDescription.Text = ao.Description;
                    ucao.cmbIEDName.SelectedIndex = ucao.cmbIEDName.FindStringExact(Utils.Iec61850IEDname);
                    ucao.cmb61850ResponseType.SelectedIndex = ucao.cmb61850ResponseType.FindStringExact(ao.IEC61850ResponseType);
                    ucao.cmb61850Index.SelectedIndex = ucao.cmb61850Index.FindStringExact(ao.IEC61850Index);
                    ucao.cmbFC.SelectedIndex = ucao.cmbFC.FindStringExact(ao.FC); //Ajay: 17/01/2019
                    ucao.ChkIEC61850Index.SelectedIndex = ucao.ChkIEC61850Index.FindStringExact(ao.IEC61850Index);//Namrata:27/03/2019
                    if (ao.Event.ToLower() == "yes") //Ajay: 26/07/2018
                    { ucao.ChkEvent.Checked = true; }
                    else { ucao.ChkEvent.Checked = false; }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool Validate()
        {
            string strRoutineName = "AOConfiguration: Validate";
            try
            {
                bool status = true;
                //Check empty field's
                if (Utils.IsEmptyFields(ucao.grpAO))
                {
                    MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                return status;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public void refreshList()
        {
            string strRoutineName = "AOConfiguration: refreshList";
            try
            {
                int cnt = 0;
                Utils.AOlistforDescription.Clear();
                ucao.lvAIlist.Items.Clear();
                //Ajay: 26/07/2018 Separate condition handled for IEC104
                //if ((masterType == MasterTypes.ADR) || (masterType == MasterTypes.IEC101) || (masterType == MasterTypes.IEC103) || (masterType == MasterTypes.IEC104) || (masterType == MasterTypes.MODBUS) || (masterType == MasterTypes.Virtual))
                if ((masterType == MasterTypes.ADR) || (masterType == MasterTypes.IEC103) || (masterType == MasterTypes.MODBUS) || (masterType == MasterTypes.Virtual))
                {
                    foreach (AO ai in aoList)
                    {
                        string[] row = new string[8];
                        if (ai.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = ai.AONo;
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
                        ucao.lvAIlist.Items.Add(lvItem);
                    }
                }
                //Ajay: 12/11/2018
                if ((masterType == MasterTypes.IEC101))
                {
                    foreach (AO ai in aoList)
                    {
                        string[] row = new string[8];
                        if (ai.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = ai.AONo;
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
                        ucao.lvAIlist.Items.Add(lvItem);
                    }
                }
                //Ajay: 02/11/2018
                if ((masterType == MasterTypes.SPORT))
                {
                    foreach (AO ai in aoList)
                    {
                        string[] row = new string[8];
                        if (ai.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = ai.AONo;
                            row[1] = ai.ResponseType;
                            row[2] = ai.Index;
                            row[3] = ai.SubIndex;
                            row[4] = ai.DataType; //Ajay: 12/11/2018 DataType Added
                            row[5] = ai.Multiplier;
                            row[6] = ai.Constant;
                            row[7] = ai.Description;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucao.lvAIlist.Items.Add(lvItem);
                    }
                }
                //Ajay: 26/07/2018 for IEC2014
                if (masterType == MasterTypes.IEC104)
                {
                    foreach (AO ai in aoList)
                    {
                        string[] row = new string[8];
                        if (ai.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = ai.AONo;
                            row[1] = ai.ResponseType;
                            row[2] = ai.Index;
                            row[3] = ai.SubIndex;
                            row[4] = ai.DataType;
                            row[5] = ai.Multiplier;
                            row[6] = ai.Constant;
                            //Ajay: 12/11/2018 Event Removed
                            //row[7] = ai.Event; //Ajay: 26/07/2018  Event added 
                            row[7] = ai.Description;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucao.lvAIlist.Items.Add(lvItem);
                    }
                }
                //Ajay: 31/07/2018 for LoadProfile
                if (masterType == MasterTypes.LoadProfile)
                {
                    foreach (AO ai in aoList)
                    {
                        string[] row = new string[2];
                        if (ai.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = ai.AONo;
                            row[1] = ai.Description;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucao.lvAIlist.Items.Add(lvItem);
                    }
                }
                //if ((masterType == MasterTypes.IEC101) || (masterType == MasterTypes.IEC104) || (masterType == MasterTypes.SPORT))
                //{
                //    foreach (AO ai in aiList)
                //    {
                //        string[] row = new string[8];
                //        if (ai.IsNodeComment)
                //        {
                //            row[0] = "Comment...";
                //        }
                //        else
                //        {
                //            row[0] = ai.AONo;
                //            row[1] = ai.ResponseType;
                //            row[2] = ai.Index;
                //            row[3] = ai.SubIndex;
                //            row[4] = ai.Multiplier;
                //            row[5] = ai.Constant;
                //            row[6] = ai.Description;
                //        }
                //        ListViewItem lvItem = new ListViewItem(row);
                //        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                //        ucai.lvAIlist.Items.Add(lvItem);
                //    }
                //}
                if (masterType == MasterTypes.IEC61850Client)
                {
                    foreach (AO ai in aoList)
                    {
                        string[] row = new string[9];
                        if (ai.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = ai.AONo;
                            row[1] = ai.IEDName;
                            row[2] = ai.IEC61850ResponseType;
                            row[3] = ai.IEC61850Index;
                            row[4] = ai.FC;
                            row[5] = ai.SubIndex;
                            row[6] = ai.Multiplier;
                            row[7] = ai.Constant;
                            row[8] = ai.Description; //Namrata: 11/9/2017
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucao.lvAIlist.Items.Add(lvItem);
                    }
                }
                ucao.lblAIRecords.Text = aoList.Count.ToString();
                //Namrata: 11/16/2017
                Utils.AolistRegenerateIndex.AddRange(aoList);
                Utils.AOlistforDescription.AddRange(aoList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            else if (masterType == MasterTypes.SPORT) return Globals.MaxSPORTMasterAO;
            else if (masterType == MasterTypes.LoadProfile) return Globals.MaxLoadProfileAO; //Ajay: 31/07/2018
            else return 0;
        }
        /* ============================================= Below this, AI Map logic... ============================================= */
        private void DeleteSlave(string slaveID)
        {
            string strRoutineName = "AOConfiguration: DeleteSlave";
            try
            {
                Console.WriteLine("*** Deleting slave {0}", slaveID);
                slavesAIMapList.Remove(slaveID);
                RadioButton rb = null;
                foreach (Control ctrl in ucao.flpMap2Slave.Controls)
                {
                    if (ctrl.Tag.ToString() == slaveID)
                    {
                        rb = (RadioButton)ctrl;
                        break;
                    }
                }
                if (rb != null) ucao.flpMap2Slave.Controls.Remove(rb);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CreateNewSlave(string slaveNum, string slaveID, XmlNode aimNode)
        {
            string strRoutineName = "AOConfiguration: CreateNewSlave";
            try
            {
                List<AOMap> saimList = new List<AOMap>();
                slavesAIMapList.Add(slaveID, saimList);
                if (aimNode != null)
                {
                    foreach (XmlNode node in aimNode)
                    {
                        if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                        saimList.Add(new AOMap(node, Utils.getSlaveTypes(slaveID)));
                    }
                }
                AddMap2SlaveButton(Int32.Parse(slaveNum), slaveID);
                //Namrata: 24/02/2018
                //If Description attribute not exist in XML 
                saimList.AsEnumerable().ToList().ForEach(item =>
                {
                    string strAONo = item.AONo;
                    if (string.IsNullOrEmpty(item.Description)) //Ajay: 05/01/2018 
                    {
                        item.Description = Utils.AOlistforDescription.AsEnumerable().Where(x => x.AONo == strAONo).Select(x => x.Description).FirstOrDefault();
                    }
                });
                refreshMapList(saimList);
                currentSlave = slaveID;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CheckIEC104SlaveStatusChanges()
        {
            string strRoutineName = "AOConfiguration: CheckIEC104SlaveStatusChanges";
            try
            {
                //Check for slave addition...
                foreach (IEC104Slave slv104 in Utils.getOpenProPlusHandle().getSlaveConfiguration().getIEC104Group().getIEC104Slaves())//Loop thru slaves...
                {
                    string slaveID = "IEC104_" + slv104.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<AOMap>> sn in slavesAIMapList)
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
                foreach (KeyValuePair<string, List<AOMap>> sain in slavesAIMapList)//Loop thru slaves...
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
                    if (ucao.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucao.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucao.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucao.lvAIMap.Items.Clear();
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
            string strRoutineName = "AOConfiguration: CheckMODBUSSlaveStatusChanges";
            try
            {
                //Check for slave addition...
                foreach (MODBUSSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().getMODBUSSlaveGroup().getMODBUSSlaves())//Loop thru slaves...
                {
                    string slaveID = "MODBUSSlave_" + slvMB.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<AOMap>> sn in slavesAIMapList)
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
                foreach (KeyValuePair<string, List<AOMap>> sain in slavesAIMapList)//Loop thru slaves...
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
                    if (ucao.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucao.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucao.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucao.lvAIMap.Items.Clear();
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
            string strRoutineName = "AOConfiguration:CheckSMSSlaveStatusChanges";
            try
            {

                //Check for slave addition...
                foreach (SMSSlave slvSMS in Utils.getOpenProPlusHandle().getSlaveConfiguration().getSMSSlaveGroup().getSMSSlaves())
                {
                    string slaveID = "SMSSlave_" + slvSMS.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<AOMap>> sn in slavesAIMapList)
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
                foreach (KeyValuePair<string, List<AOMap>> sain in slavesAIMapList)//Loop thru slaves...
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
                    if (ucao.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucao.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucao.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucao.lvAIMap.Items.Clear();
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

        private void CheckDNPSlaveStatusChanges()
        {
            string strRoutineName = "AOConfiguration:CheckDNPSlaveStatusChanges";
            try
            {
                //Check for slave addition...
                foreach (DNP3Settings dnp3 in Utils.getOpenProPlusHandle().getSlaveConfiguration().getDNPSlaveGroup().getDNPSlaves())//Loop thru slaves...
                {
                    string slaveID = "DNP3Slave_" + dnp3.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<AOMap>> sn in slavesAIMapList)
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
                foreach (KeyValuePair<string, List<AOMap>> sain in slavesAIMapList)//Loop thru slaves...
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
                    if (ucao.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucao.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucao.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucao.lvAIMap.Items.Clear();
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
            string strRoutineName = "AOConfiguration:CheckMQTTSlaveStatusChanges";
            try
            {

                //Check for slave addition...
                foreach (MQTTSlave slvMQTT in Utils.getOpenProPlusHandle().getSlaveConfiguration().getMQTTSlaveGroup().getMQTTSlaves())
                {
                    string slaveID = "MQTTSlave_" + slvMQTT.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<AOMap>> sn in slavesAIMapList)
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
                foreach (KeyValuePair<string, List<AOMap>> sain in slavesAIMapList)//Loop thru slaves...
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
                    if (ucao.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucao.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucao.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucao.lvAIMap.Items.Clear();
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
            string strRoutineName = "AOConfiguration: CheckIEC61850laveStatusChanges";
            try
            {
                foreach (IEC61850ServerSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().get61850SlaveGroup().getMODBUSSlaves())//Loop thru slaves...
                {
                    //string slaveID = "IEC61850ServerGroup_" + slvMB.SlaveNum;
                    string slaveID = "IEC61850Server_" + slvMB.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<AOMap>> sn in slavesAIMapList)
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
                foreach (KeyValuePair<string, List<AOMap>> sain in slavesAIMapList)//Loop thru slaves...
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
                    if (ucao.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucao.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucao.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucao.lvAIMap.Items.Clear();
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


        private void CheckGDisplaySlaveStatusChanges()
        {
            string strRoutineName = "AOConfiguration: CheckGDisplaySlaveStatusChanges";
            try
            {
                //Check for slave addition...
                foreach (GraphicalDisplaySlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().getGraphicalDisplaySlaveGroup().getGDisplaySlaves())
                {
                    string slaveID = "GraphicalDisplaySlave_" + slvMB.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<AOMap>> sain in slavesAIMapList)//Loop thru slaves...
                    {
                        if (sain.Key == slaveID)
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
                foreach (KeyValuePair<string, List<AOMap>> sain in slavesAIMapList)//Loop thru slaves...
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
                    if (ucao.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucao.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucao.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucao.lvAIMap.Items.Clear();
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
        private void CheckIEC101SlaveStatusChanges()
        {
            string strRoutineName = "CheckIEC101SlaveStatusChanges";
            try
            {
                Console.WriteLine("*** CheckIEC101SlaveStatusChanges");
                //Check for slave addition...
                foreach (IEC101Slave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().getIEC101SlaveGroup().getIEC101Slaves())
                {
                    string slaveID = "IEC101Slave_" + slvMB.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<AOMap>> sn in slavesAIMapList)
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
                foreach (KeyValuePair<string, List<AOMap>> sain in slavesAIMapList)//Loop thru slaves...
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
                    if (ucao.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucao.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucao.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucao.lvAIMap.Items.Clear();
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
            string strRoutineName = "AOConfiguration: CheckSPORTSlaveStatusChanges";
            try
            {
                Console.WriteLine("*** CheckSPORTSlaveStatusChanges");
                //Check for slave addition...
                foreach (SPORTSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().getSPORTSlaveGroup().getSPORTSlaves())
                {
                    string slaveID = "SPORTSlave_" + slvMB.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<AOMap>> sn in slavesAIMapList)
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
                foreach (KeyValuePair<string, List<AOMap>> sain in slavesAIMapList)//Loop thru slaves...
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
                    if (ucao.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucao.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucao.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucao.lvAIMap.Items.Clear();
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
            string strRoutineName = "AOConfiguration: deleteAIFromMaps";
            try
            {
                Console.WriteLine("*** Deleting element no. {0}", aiNo);
                foreach (KeyValuePair<string, List<AOMap>> sain in slavesAIMapList)//Loop thru slaves...
                {
                    List<AOMap> delEntry = sain.Value;
                    foreach (AOMap aimn in delEntry)
                    {
                        if (aimn.AONo == aiNo)
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
            string strRoutineName = "AOConfiguration: AddMap2SlaveButton";
            try
            {
                RadioButton rb = new RadioButton();
                rb.Name = slaveID;
                rb.Tag = slaveID;//Ex. 'IEC104_1'/'MODBUSSlave_1'
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
                //Namrata:01/04/2019
                if (Utils.getSlaveTypes(slaveID) == SlaveTypes.MQTTSLAVE)
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
                //rb.Padding = new Padding(0, 0, 0, 0);
                if (Utils.getSlaveTypes(slaveID) == SlaveTypes.MODBUSSLAVE)
                    rb.Text = "MODBUS " + slaveNum;
                if (Utils.getSlaveTypes(slaveID) == SlaveTypes.IEC104)
                    rb.Text = "IEC104 " + slaveNum;
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

                ucao.flpMap2Slave.Controls.Add(rb);
                rb.Checked = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void rbGrpMap2Slave_Click(object sender, EventArgs e)
        {
            string strRoutineName = "AOConfiguration:rbGrpMap2Slave_Click";
            try
            {
                ucao.grpAIMap.Visible = false; //Ajay: 07/12/2018
                bool rbChanged = false;
                RadioButton currRB = ((RadioButton)sender);
                if (currentSlave != currRB.Tag.ToString())
                {
                    currentSlave = currRB.Tag.ToString();
                    rbChanged = true;
                    refreshCurrentMapList();
                    if (ucao.lvAIlist.SelectedItems.Count > 0)
                    {
                        //If possible highlight the map for new slave selected...
                        //Remove selection from AIMap...
                        ucao.lvAIMap.SelectedItems.Clear();
                        Utils.highlightListviewItem(ucao.lvAIlist.SelectedItems[0].Text, ucao.lvAIMap);
                    }
                }
                //ShowHideSlaveColumns();
                //Ajay:06/07/2018
                ShowHideSlaveColumnsSPORT();
                ShowHideSlaveColumnsMQTT();
                ShowHideSlaveColumnsDNP3();
                // Ajay: 07/12/2018
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    if (AllowSlaveOptionsOnClick(Utils.getSlaveTypes(currentSlave))) { return; }
                    else { }
                }
                if (rbChanged && ucao.lvAIlist.CheckedItems.Count <= 0) return;//We supported map listing only
                AO mapAI = null;
                if (ucao.lvAIlist.CheckedItems.Count != 1)
                {
                    MessageBox.Show("Select Single AO Element To Map!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                for (int i = 0; i < ucao.lvAIlist.Items.Count; i++)
                {
                    if (ucao.lvAIlist.Items[i].Checked)
                    {
                        mapAI = aoList.ElementAt(i);
                        ucao.lvAIlist.Items[i].Checked = false;//Now we can uncheck in listview...
                        break;
                    }
                }

                if (mapAI == null) return;
                bool alreadyMapped = false; //Search if already mapped for current slave...
                List<AOMap> slaveAIMapList;
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
                    Utils.resetValues(ucao.grpAIMap);
                    ucao.txtAIMNo.Text = mapAI.AONo;
                    //Namrata: 16/11/2017
                    string str = getDescription(ucao.lvAIlist, mapAI.AONo);
                    ucao.txtMapDescription.Text = str;
                    ucao.textBox1.Text = "1";
                    loadMapDefaults();

                    #region Mapping Enable/Disable Events on Load
                    if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.SMSSLAVE||Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE || Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC101SLAVE || Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC104)
                    {
                        ucao.lblCT.Visible = false;
                        ucao.CmbCT.Visible = false;
                        MODBUS_IEC101_IEC104Slave_EventsOnLoad();
                    }
                    //Ajay:06/07/2018
                    else if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.SPORTSLAVE) //Ajay: 05/07/2018)
                    {
                        ucao.lblCT.Visible = false;
                        ucao.CmbCT.Visible = false;
                        SPORTSlave_EventsOnLoad();
                    }
                    //Namrata:02/04/2019
                    else if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MQTTSLAVE) //Ajay: 05/07/2018)
                    {
                        ucao.lblCT.Visible = false;
                        ucao.CmbCT.Visible = false;
                        MQTTSlaveEvents(); ucao.txtKey.Text = getMQTTSlaveKey(ucao.lvAIlist, mapAI.AONo);
                    }
                    else if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE) //Ajay: 05/07/2018)
                    {
                        ucao.lblKey.Visible = false;
                        ucao.txtKey.Visible = false;
                        ucao.txtUnit.Visible = false;
                        ucao.lblUnit.Visible = false;

                        ucao.lblCT.Visible = true;
                        ucao.CmbCT.Visible = true;

                        //UnitID
                        ucao.lblUnitID.Visible = false;
                        ucao.txtUnitID.Visible = false;

                        //Used
                        ucao.chkUsed.Visible = false;

                        //AIMNo
                        ucao.lblANM.Location = new Point(13, 33);
                        ucao.txtAIMNo.Location = new Point(119, 30);
                        ucao.txtAIMNo.Size = new Size(165, 21);

                        //AIMReportingIndex
                        ucao.lblAMRI.Location = new Point(13, 58);
                        ucao.txtAIMReportingIndex.Location = new Point(119, 55);
                        ucao.txtAIMReportingIndex.Size = new Size(165, 21);

                        //DataType
                        ucao.lblCT.Visible = true;
                        ucao.lblCT.Location = new Point(13, 83);
                        ucao.CmbCT.Location = new Point(119, 80);
                        ucao.CmbCT.Size = new Size(165, 21);

                        ucao.lblAMDT.Visible = false; ucao.cmbAIMDataType.Visible = false;

                        //Deadband
                        ucao.lblAMDB.Location = new Point(13, 109);
                        ucao.txtAIMDeadBand.Location = new Point(119, 106);
                        ucao.txtAIMDeadBand.Size = new Size(165, 21);

                        //Multiplier
                        ucao.lblAMM.Location = new Point(13, 135);
                        ucao.txtAIMMultiplier.Location = new Point(119, 132);
                        ucao.txtAIMMultiplier.Visible = true;
                        ucao.txtAIMMultiplier.Size = new Size(165, 21);

                        //Constant
                        ucao.lblAMC.Location = new Point(13, 161);
                        ucao.txtAIMConstant.Location = new Point(119, 158);
                        ucao.txtAIMConstant.Size = new Size(165, 21);

                        //Description
                        ucao.label5.Location = new Point(13, 186);
                        ucao.txtMapDescription.Location = new Point(119, 183);
                        ucao.txtMapDescription.Visible = true;
                        ucao.txtMapDescription.Size = new Size(165, 21);

                        //Automap
                        ucao.AutoMapRange.Location = new Point(13, 211);
                        ucao.textBox1.Location = new Point(119, 208);
                        ucao.textBox1.Visible = true;
                        ucao.textBox1.Size = new Size(165, 21);
                        ucao.AutoMapRange.Visible = true;
                        ucao.textBox1.Visible = true;

                        //BtnDone & BtnCancel
                        ucao.btnAIMDone.Location = new Point(120, 236);
                        ucao.btnAIMCancel.Location = new Point(210, 236);

                        //GrpAI
                        ucao.grpAIMap.Size = new Size(307, 278);
                        ucao.pbAIMHdr.Size = new Size(307, 22);
                        ucao.grpAIMap.Location = new Point(450, 325);
                    }
                    #endregion Mapping Enable/Disable Events on Load
                    ucao.grpAIMap.Visible = true;
                    ucao.txtAIMReportingIndex.Focus();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string getDescription(ListView lstview, string ainno)
        {
            int iColIndex = ucao.lvAIlist.Columns["Description"].Index;
            var query = lstview.Items
                    .Cast<ListViewItem>()
                    .Where(item => item.SubItems[0].Text == ainno).Select(s => s.SubItems[iColIndex].Text).Single();
            return query.ToString();
        }
        private void btnAIMDelete_Click(object sender, EventArgs e)
        {
            string strRoutineName = "AOConfiguration:btnAIMDelete_Click";
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
                List<AOMap> slaveAIMapList;
                if (!slavesAIMapList.TryGetValue(currentSlave, out slaveAIMapList))
                {
                    MessageBox.Show("Error deleting AO map!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int i = ucao.lvAIMap.Items.Count;
                for (i = ucao.lvAIMap.Items.Count - 1; i >= 0; i--)
                {
                    if (ucao.lvAIMap.Items[i].Checked)
                    {
                        slaveAIMapList.RemoveAt(i);
                        ucao.lvAIMap.Items[i].Remove();
                    }
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
            string strRoutineName = "AOConfiguration:btnAIMCancel_Click";
            try
            {
                ucao.grpAIMap.Visible = false;
                mapMode = Mode.NONE;
                mapEditIndex = -1;
                Utils.resetValues(ucao.grpAIMap);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvAIMap_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "AOConfiguration: lvAIMap_DoubleClick";
            try
            {
                // Ajay: 07/12/2018
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    if (AllowSlaveOptionsOnClick(Utils.getSlaveTypes(currentSlave))) { return; }
                    else { }
                }

                List<AOMap> slaveAIMapList;
                if (!slavesAIMapList.TryGetValue(currentSlave, out slaveAIMapList))
                {
                    MessageBox.Show("Error editing AO map for slave!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //Namrata: 07/03/2018
                int ListIndex = ucao.lvAIMap.FocusedItem.Index;
                ListViewItem lvi = ucao.lvAIMap.Items[ListIndex];//ucai.lvAIlist.SelectedItems[0];
                Utils.UncheckOthers(ucao.lvAIMap, lvi.Index);

                if (slaveAIMapList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #region Enable/Disable Evets on DoubleClick
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE || Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC101SLAVE || Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC104)
                {
                    MODBUS_IEC101_IEC104Slave_EventsDoubleClick();
                }
                else if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.SPORTSLAVE) //Ajay: 05/07/2018)
                {
                    SPORTSlave_EventsOnDoubleClick();
                }
                else if(Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE)
                {
                    ucao.lblKey.Visible = false;
                    ucao.txtKey.Visible = false;
                    ucao.txtUnit.Visible = false;
                    ucao.lblUnit.Visible = false;

                    ucao.lblCT.Visible = true;
                    ucao.CmbCT.Visible = true;

                    //UnitID
                    ucao.lblUnitID.Visible = false;
                    ucao.txtUnitID.Visible = false;

                    //Used
                    ucao.chkUsed.Visible = false;

                    //AIMNo
                    ucao.lblANM.Location = new Point(13, 33);
                    ucao.txtAIMNo.Location = new Point(119, 30);
                    ucao.txtAIMNo.Size = new Size(165, 21);

                    //AIMReportingIndex
                    ucao.lblAMRI.Location = new Point(13, 58);
                    ucao.txtAIMReportingIndex.Location = new Point(119, 55);
                    ucao.txtAIMReportingIndex.Size = new Size(165, 21);

                    //DataType
                    ucao.lblCT.Visible = true;
                    ucao.lblCT.Location = new Point(13, 83);
                    ucao.CmbCT.Location = new Point(119, 80);
                    ucao.CmbCT.Size = new Size(165, 21);

                    ucao.lblAMDT.Visible = false; ucao.cmbAIMDataType.Visible = false;

                    //Deadband
                    ucao.lblAMDB.Location = new Point(13, 109);
                    ucao.txtAIMDeadBand.Location = new Point(119, 106);
                    ucao.txtAIMDeadBand.Size = new Size(165, 21);

                    //Multiplier
                    ucao.lblAMM.Location = new Point(13, 135);
                    ucao.txtAIMMultiplier.Location = new Point(119, 132);
                    ucao.txtAIMMultiplier.Visible = true;
                    ucao.txtAIMMultiplier.Size = new Size(165, 21);

                    //Constant
                    ucao.lblAMC.Location = new Point(13, 161);
                    ucao.txtAIMConstant.Location = new Point(119, 158);
                    ucao.txtAIMConstant.Size = new Size(165, 21);

                    //Description
                    ucao.label5.Location = new Point(13, 186);
                    ucao.txtMapDescription.Location = new Point(119, 183);
                    ucao.txtMapDescription.Visible = true;
                    ucao.txtMapDescription.Size = new Size(165, 21);

                    //Automap
                    ucao.AutoMapRange.Location = new Point(13, 211);
                    ucao.textBox1.Location = new Point(119, 208);
                    ucao.textBox1.Visible = false;
                    ucao.textBox1.Size = new Size(165, 21);
                    ucao.AutoMapRange.Visible = false;
                    //ucao.textBox1.Visible = true;

                    //BtnDone & BtnCancel
                    ucao.btnAIMDone.Location = new Point(120, 205);
                    ucao.btnAIMCancel.Location = new Point(210, 205);

                    //GrpAI
                    ucao.grpAIMap.Size = new Size(307, 278);
                    ucao.pbAIMHdr.Size = new Size(307, 22);
                    ucao.grpAIMap.Location = new Point(450, 325);
                }
                else if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MQTTSLAVE)
                {
                    ucao.lblANM.Location = new Point(13, 33);
                    ucao.txtAIMNo.Location = new Point(119, 30);
                    ucao.txtAIMNo.Size = new Size(220, 21);

                    ucao.lblUnitID.Visible = false;
                    ucao.txtUnitID.Visible = false;


                    ucao.lblAMRI.Location = new Point(13, 58);
                    ucao.txtAIMReportingIndex.Location = new Point(119, 55);
                    ucao.txtAIMReportingIndex.Size = new Size(220, 21);

                    ucao.lblAMDT.Visible = true;
                    ucao.lblAMDT.Location = new Point(13, 83);
                    ucao.cmbAIMDataType.Location = new Point(119, 80);
                    ucao.cmbAIMDataType.Size = new Size(220, 21);

                    ucao.lblAMDB.Location = new Point(13, 109);
                    ucao.txtAIMDeadBand.Location = new Point(119, 106);
                    ucao.txtAIMDeadBand.Size = new Size(220, 21);

                    ucao.lblAMM.Location = new Point(13, 135);
                    ucao.txtAIMMultiplier.Location = new Point(119, 132);
                    ucao.txtAIMMultiplier.Visible = true;
                    ucao.txtAIMMultiplier.Size = new Size(220, 21);
                    //Constant
                    ucao.lblAMC.Location = new Point(13, 161);
                    ucao.txtAIMConstant.Location = new Point(119, 158);
                    ucao.txtAIMConstant.Size = new Size(220, 21);
                    
                    ucao.lblKey.Visible = true;
                    ucao.lblKey.Location = new Point(13, 186);
                    ucao.txtKey.Location = new Point(119, 183);
                    ucao.txtKey.Size = new Size(220, 21);
                    ucao.txtKey.Visible = true;

                    ucao.txtUnit.Visible = true;
                    ucao.lblUnit.Location = new Point(13, 211);
                    ucao.txtUnit.Location = new Point(119, 208);
                    ucao.txtUnit.Size = new Size(220, 21);
                    ucao.lblUnit.Visible = true;

                    //Description
                    ucao.label5.Location = new Point(13, 236);
                    ucao.txtMapDescription.Location = new Point(119, 233);
                    ucao.txtMapDescription.Visible = true;
                    ucao.txtMapDescription.Size = new Size(220, 21);

                    ucao.AutoMapRange.Visible = false;
                    ucao.textBox1.Visible = false;

                    ucao.chkUsed.Visible = false;

                    ucao.btnAIMDone.Location = new Point(120, 265);
                    ucao.btnAIMCancel.Location = new Point(210, 265);


                    ucao.grpAIMap.Size = new Size(355, 300);
                    ucao.pbAIMHdr.Size = new Size(355, 22);
                    ucao.grpAIMap.Location = new Point(243, 319);

                }
                #endregion Enable/Disable Evets on DoubleClick
                ucao.textBox1.Text = "0";
                ucao.grpAIMap.Visible = true;
                mapMode = Mode.EDIT;
                mapEditIndex = lvi.Index;
                loadMapValues();
                ucao.txtAIMReportingIndex.Focus();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadMapDefaults()
        {
            string strRoutineName = "AOConfiguration: loadMapDefaults";
            try
            {
                ucao.txtAIMReportingIndex.Text = (Globals.AOReportingIndex + 1).ToString();
                ucao.txtAIMDeadBand.Text = "1";
                ucao.txtAIMMultiplier.Text = "1";
                ucao.txtAIMConstant.Text = "0";
                //Ajay:06/07/2018
                ucao.txtUnitID.Text = "1";
                ucao.txtUnit.Text = "kV";
                ucao.txtKey.Text = "FDR1-Volt-VR";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadMapValues()
        {
            string strRoutineName = "AOConfiguration: loadMapValues";
            try
            {
                List<AOMap> slaveAIMapList;
                if (!slavesAIMapList.TryGetValue(currentSlave, out slaveAIMapList))
                {
                    MessageBox.Show("Error loading AO map data for slave!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                AOMap aimn = slaveAIMapList.ElementAt(mapEditIndex);
                if (aimn != null)
                {
                    ucao.txtAIMNo.Text = aimn.AONo;
                    ucao.txtAIMReportingIndex.Text = aimn.ReportingIndex;
                    ucao.cmbAIMDataType.SelectedIndex = ucao.cmbAIMDataType.FindStringExact(aimn.DataType);
                    ucao.txtAIMDeadBand.Text = aimn.Deadband;
                    ucao.txtAIMMultiplier.Text = aimn.Multiplier;
                    ucao.txtAIMConstant.Text = aimn.Constant;
                    ucao.CmbCT.SelectedIndex = ucao.CmbCT.FindStringExact(aimn.CommandType);
                    ucao.txtUnit.Text = aimn.Unit;
                    ucao.txtKey.Text = aimn.Key;
                    //Namrata: 18/11/2017
                    ucao.txtMapDescription.Text = aimn.Description;
                    //Ajay: 12/07/2018 check-unchecked Used checkbox according to mapped Used value
                    if (aimn.Used.ToLower() == "yes")
                    { ucao.chkUsed.Checked = true; }
                    else { ucao.chkUsed.Checked = false; }
                    ucao.txtUnitID.Text = aimn.UnitID;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidateMap()
        {
            string strRoutineName = "AOConfiguration:ValidateMap";
            try
            {
                bool status = true;
                //Check empty field's
                if (Utils.IsEmptyFields(ucao.grpAIMap))
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
        private void refreshMapList(List<AOMap> tmpList)
        {
            string strRoutineName = "AOConfiguration: refreshMapList";
            try
            {
                int cnt = 0;
                ucao.lvAIMap.Items.Clear();
                ucao.lblAIMRecords.Text = "0";
                if (tmpList == null) return;

                //ucao.lvAIMap.Columns.Add("AO No.", 70, HorizontalAlignment.Left);
                //ucao.lvAIMap.Columns.Add("Unit ID", 0, HorizontalAlignment.Left);
                //ucao.lvAIMap.Columns.Add("Reporting Index", 200, HorizontalAlignment.Left);
                //ucao.lvAIMap.Columns.Add("Data Type", 205, HorizontalAlignment.Left);
                //ucao.lvAIMap.Columns.Add("Deadband", 70, HorizontalAlignment.Left);
                //ucao.lvAIMap.Columns.Add("Multiplier", 70, HorizontalAlignment.Left);
                //ucao.lvAIMap.Columns.Add("Constant", -2, HorizontalAlignment.Left);
                //ucao.lvAIMap.Columns.Add("Used", 0, HorizontalAlignment.Left);
                //ucao.lvAIMap.Columns.Add("Unit", 0, HorizontalAlignment.Left); //Ajay: 05/09/2018
                //ucao.lvAIMap.Columns.Add("Key", 0, HorizontalAlignment.Left); //Ajay: 05/09/2018
                //ucao.lvAIMap.Columns.Add("Command Type", 0, HorizontalAlignment.Left); //Ajay: 05/09/2018
                //ucao.lvAIMap.Columns.Add("Description", 100, HorizontalAlignment.Left); //Namrata:16/11/2017



                #region MODBUSSLAVE,IEC104,IEC101SLAVE,IEC61850Server,SMSSLAVE
                //Ajay: 16/11/2018 IEC101Slave and IEC6180Server added
                if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.SMSSLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC104) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC101SLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server))
                {
                    foreach (AOMap aimp in tmpList)
                    {
                        string[] row = new string[12];
                        if (aimp.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = aimp.AONo;
                            row[2] = aimp.ReportingIndex;
                            row[3] = aimp.DataType;
                            row[4] = aimp.Deadband;
                            row[5] = aimp.Multiplier;
                            row[6] = aimp.Constant;
                            row[8] = aimp.Description;
                            row[8] = "";
                            row[9] = "";
                            row[10] = "";
                            row[11] = aimp.Description;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucao.lvAIMap.Items.Add(lvItem);
                    }
                }
                #endregion MODBUSSLAVE,IEC104,IEC101SLAVE,IEC61850Server,SMSSLAVE

                #region SPORTSlave
                else if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.SPORTSLAVE))
                {
                    foreach (AOMap aimp in tmpList)
                    {
                        string[] row = new string[12];
                        if (aimp.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = aimp.AONo;
                            row[1] = aimp.UnitID;
                            row[2] = aimp.ReportingIndex;
                            row[3] = aimp.DataType;
                            row[4] = aimp.Deadband;
                            row[5] = aimp.Multiplier;
                            row[6] = aimp.Constant;
                            row[7] = aimp.Used;
                            row[8] = "";
                            row[9] = "";
                            row[10] = "";
                            row[11] = aimp.Description;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucao.lvAIMap.Items.Add(lvItem);
                    }
                }
                #endregion SPORTSlave
                //Namrata:02/04/2019
                else if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.MQTTSLAVE))
                {
                    foreach (AOMap aimp in tmpList)
                    {
                        string[] row = new string[12];
                        if (aimp.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = aimp.AONo;
                            row[1] = "";
                            row[2] = aimp.ReportingIndex;
                            row[3] = aimp.DataType;
                            row[4] = aimp.Deadband;
                            row[5] = aimp.Multiplier;
                            row[6] = aimp.Constant;
                            row[7] = "";
                            row[8] = aimp.Unit;
                            row[9] = aimp.Key;
                            row[10] = "";
                            row[11] = aimp.Description;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucao.lvAIMap.Items.Add(lvItem);
                    }
                }
                #region DNPSlave
                else if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE))
                {
                    foreach (AOMap aimp in tmpList)
                    {
                        string[] row = new string[12];
                        if (aimp.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = aimp.AONo;
                            row[1] = aimp.UnitID;
                            row[2] = aimp.ReportingIndex;
                            row[3] = aimp.DataType;
                            row[4] = aimp.Deadband;
                            row[5] = aimp.Multiplier;
                            row[6] = aimp.Constant;
                            row[7] = aimp.Used;
                            row[8] = "";
                            row[9] = "";
                            row[10] = aimp.CommandType;
                            row[11] = aimp.Description;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucao.lvAIMap.Items.Add(lvItem);
                    }
                }
                #endregion DNPSlave

                ucao.lblAIMRecords.Text = tmpList.Count.ToString();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void refreshCurrentMapList()
        {
            string strRoutineName = "AOConfiguration: refreshCurrentMapList";
            try
            {
                fillMapOptions(Utils.getSlaveTypes(currentSlave));
                List<AOMap> saimList;
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
            string strRoutineName = "AOConfiguration: fillOptions";
            try
            {
                if (!dtdataset.Columns.Contains("Address")) //Ajay: 12/11/2018 Condition checked to handle exception
                { DataColumn dcAddressColumn = dtdataset.Columns.Add("Address", typeof(string)); }
                if (!dtdataset.Columns.Contains("IED")) //Ajay: 12/11/2018 Condition checked to handle exception
                { dtdataset.Columns.Add("IED", typeof(string)); }
                List<string> tblNameList = Utils.dsIED.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                string tblName = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
                ucao.cmbIEDName.DataSource = Utils.dsIED.Tables[tblName];
                ucao.cmbIEDName.DisplayMember = "IEDName";
                //Namrata: 04/04/2018
                if (Utils.Iec61850IEDname != "")
                {
                    ucao.cmbIEDName.Text = Utils.Iec61850IEDname;
                }
                //Namrata: 31/10/2017
                DataSet ds11 = Utils.dsResponseType;
                ucao.cmb61850ResponseType.DataSource = Utils.dsResponseType.Tables[Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname];//[Utils.strFrmOpenproplusTreeNode + "/" + "Undefined" + "/" + Utils.Iec61850IEDname];
                ucao.cmb61850ResponseType.DisplayMember = "Address";

                //Fill Response Type...
                if (masterType == MasterTypes.IEC61850Client)
                {
                    ucao.cmbResponseType.Items.Clear();
                }
                else
                {
                    ucao.cmbResponseType.Items.Clear();
                    if (masterType != MasterTypes.LoadProfile) //Ajay: 31/07/2018
                    {
                        foreach (String rt in AO.getResponseTypes(masterType))
                        {
                            ucao.cmbResponseType.Items.Add(rt.ToString());
                        }
                        ucao.cmbResponseType.SelectedIndex = 0;
                    }
                }
                //Fill Data Type...
                if ((masterType == MasterTypes.IEC61850Client))
                {
                    ucao.cmbDataType.Items.Clear();
                }
                else
                {
                    ucao.cmbDataType.Items.Clear();
                    if (masterType != MasterTypes.LoadProfile) //Ajay: 31/07/2018
                    {
                        foreach (String dt in AO.getDataTypes(masterType))
                        {
                            ucao.cmbDataType.Items.Add(dt.ToString());
                        }
                        if (ucao.cmbDataType != null && ucao.cmbDataType.Items.Count > 0)
                        {
                            ucao.cmbDataType.SelectedIndex = 0;
                        }
                    }
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fillMapOptions(SlaveTypes sType)
        {
            string strRoutineName = "AOConfiguration:fillMapOptions";
            try
            {
                try
                {
                    //Fill Data Type...
                    ucao.cmbAIMDataType.Items.Clear();
                    foreach (String dt in AOMap.getDataTypes(sType))
                    {
                        ucao.cmbAIMDataType.Items.Add(dt.ToString());
                    }
                    if (ucao.cmbAIMDataType.Items.Count > 0) ucao.cmbAIMDataType.SelectedIndex = 0;

                    ucao.CmbCT.Items.Clear();
                    foreach (String ct in AOMap.getCommandTypes(sType))
                    {
                        ucao.CmbCT.Items.Add(ct.ToString());
                    }
                    if (ucao.CmbCT.Items.Count > 0) ucao.CmbCT.SelectedIndex = 0;

                    #region Fill Variations
                    //ucao.CmbVari.Items.Clear();
                    //foreach (String dt in AIMap.getVariartions(sType))
                    //{
                    //    ucao.CmbVari.Items.Add(dt.ToString());
                    //}
                    //if (ucao.CmbVari.Items.Count > 0) ucao.CmbVari.SelectedIndex = 0;
                    //#endregion Fill Variations

                    //#region Fill EVariations
                    //ucao.CmbEV.Items.Clear();
                    //foreach (String dt in AIMap.getEventsVariations(sType))
                    //{
                    //    ucao.CmbEV.Items.Add(dt.ToString());
                    //}
                    //if (ucao.CmbEV.Items.Count > 0) ucao.CmbEV.SelectedIndex = 0;
                    //#endregion Fill EVariations

                    //#region Fill EClass
                    //ucao.cmbEventC.Items.Clear();
                    //foreach (String dt in AIMap.getEventsClasses(sType))
                    //{
                    //    ucao.cmbEventC.Items.Add(dt.ToString());
                    //}
                    //if (ucao.cmbEventC.Items.Count > 0) ucao.cmbEventC.SelectedIndex = 0;
                    #endregion Fill EClass
                }
                catch (System.NullReferenceException) { }
                
                try
                {
                }
                catch (System.NullReferenceException) { }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void addListHeaders()
        {
            string strRoutineName = "AOConfiguration: addListHeaders";
            try
            {
                if (masterType == MasterTypes.IEC103)
                {
                    ucao.lvAIlist.Columns.Add("AO No.", 70, HorizontalAlignment.Left); //Ajay: 31/07/2018 "AI No." -> "AO No."
                    ucao.lvAIlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Sub Index", 70, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Data Type", 200, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Multiplier", 70, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Constant", 70, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                }
                else if (masterType == MasterTypes.IEC101)
                {
                    ucao.lvAIlist.Columns.Add("AO No.", 70, HorizontalAlignment.Left); //Ajay: 31/07/2018 "AI No." -> "AO No."
                    ucao.lvAIlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Sub Index", 70, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Data Type", 200, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Multiplier", 70, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Constant", 70, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                }
                else if (masterType == MasterTypes.IEC104)
                {
                    ucao.lvAIlist.Columns.Add("AO No.", 70, HorizontalAlignment.Left); //Ajay: 31/07/2018 "AI No." -> "AO No."
                    ucao.lvAIlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Sub Index", 70, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Data Type", 200, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Multiplier", 70, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Constant", 70, HorizontalAlignment.Left);
                    //Ajay: 12/11/2018 Event Removed
                    //ucao.lvAIlist.Columns.Add("Event", "Event", 70, HorizontalAlignment.Left, String.Empty); //Ajay: 26/07/2018 Event added
                    ucao.lvAIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                }
                else if (masterType == MasterTypes.SPORT)
                {
                    ucao.lvAIlist.Columns.Add("AO No.", 70, HorizontalAlignment.Left); //Ajay: 31/07/2018 "AI No." -> "AO No."
                    ucao.lvAIlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Sub Index", 70, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Data Type", 200, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Multiplier", 70, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Constant", 70, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                }
                else if (masterType == MasterTypes.ADR)
                {
                    ucao.lvAIlist.Columns.Add("AO No.", 70, HorizontalAlignment.Left); //Ajay: 31/07/2018 "AI No." -> "AO No."
                    ucao.lvAIlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Sub Index", 70, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Data Type", 200, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Multiplier", 70, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Constant", 70, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                }
                else if (masterType == MasterTypes.MODBUS)
                {
                    ucao.lvAIlist.Columns.Add("AO No.", 70, HorizontalAlignment.Left); //Ajay: 31/07/2018 "AI No." -> "AO No."
                    ucao.lvAIlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Sub Index", 70, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Data Type", 200, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Multiplier", 70, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Constant", 70, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                }
                else if (masterType == MasterTypes.Virtual)
                {
                    ucao.lvAIlist.Columns.Add("AO No.", 70, HorizontalAlignment.Left); //Ajay: 31/07/2018 "AI No." -> "AO No."
                    ucao.lvAIlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Sub Index", 70, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Data Type", 200, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Multiplier", 70, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Constant", 70, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                }
                //Ajay: 31/07/2018
                else if (masterType == MasterTypes.LoadProfile)
                {
                    ucao.lvAIlist.Columns.Add("AO No.", 70, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                }
                else if (masterType == MasterTypes.IEC61850Client)
                {
                    ucao.lvAIlist.Columns.Add("AO No.", 70, HorizontalAlignment.Left); //Ajay: 31/07/2018 "AI No." -> "AO No."
                    ucao.lvAIlist.Columns.Add("IEDName", 50, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Response Type", 270, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Index", 350, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("FC", 100, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Sub Index", 65, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Multiplier", 70, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Constant", 70, HorizontalAlignment.Left);
                    ucao.lvAIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                    //Namrata: 15/9/2017
                    ucao.lvAIlist.Columns[1].Width = 0; //Hide IED Name
                }
                //Add AO map headers..
                ucao.lvAIMap.Columns.Add("AO No.", 70, HorizontalAlignment.Left);
                ucao.lvAIMap.Columns.Add("Unit ID", 0, HorizontalAlignment.Left);
                ucao.lvAIMap.Columns.Add("Reporting Index", 200, HorizontalAlignment.Left);
                ucao.lvAIMap.Columns.Add("Data Type", 205, HorizontalAlignment.Left);
                ucao.lvAIMap.Columns.Add("Deadband", 70, HorizontalAlignment.Left);
                ucao.lvAIMap.Columns.Add("Multiplier", 70, HorizontalAlignment.Left);
                ucao.lvAIMap.Columns.Add("Constant", -2, HorizontalAlignment.Left);
                ucao.lvAIMap.Columns.Add("Used", 0, HorizontalAlignment.Left);
                ucao.lvAIMap.Columns.Add("Unit", 0, HorizontalAlignment.Left); //Ajay: 05/09/2018
                ucao.lvAIMap.Columns.Add("Key", 0, HorizontalAlignment.Left); //Ajay: 05/09/2018
                ucao.lvAIMap.Columns.Add("Command Type", 0, HorizontalAlignment.Left); //Ajay: 05/09/2018
                ucao.lvAIMap.Columns.Add("Description", 100, HorizontalAlignment.Left); //Namrata:16/11/2017
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ShowHideSlaveColumnsMQTT()
        {
            string strRoutineName = "AOConfiguration:ShowHideSlaveColumnsMQTT";
            try
            {
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MQTTSLAVE) Utils.getColumnHeader(ucao.lvAIMap, "Unit").Width = COL_CMD_TYPE_WIDTH;
                else Utils.getColumnHeader(ucao.lvAIMap, "Unit").Width = 0; //Hide...
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MQTTSLAVE) Utils.getColumnHeader(ucao.lvAIMap, "Key").Width = COL_CMD_TYPE_WIDTH;
                else Utils.getColumnHeader(ucao.lvAIMap, "Key").Width = 0; //Hide...
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ShowHideSlaveColumnsDNP3()
        {
            string strRoutineName = "AOConfiguration:ShowHideSlaveColumnsDNP3";
            try
            {
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE) Utils.getColumnHeader(ucao.lvAIMap, "Command Type").Width = COL_CMD_TYPE_WIDTH;
                else Utils.getColumnHeader(ucao.lvAIMap, "Command Type").Width = 0; //Hide...
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE) Utils.getColumnHeader(ucao.lvAIMap, "Data Type").Width = 0;

            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay:06/07/2018
        private void ShowHideSlaveColumnsSPORT()
        {
            string strRoutineName = "AOConfiguration: ShowHideSlaveColumnsSPORT";
            try
            {
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.SPORTSLAVE) Utils.getColumnHeader(ucao.lvAIMap, "Unit ID").Width = COL_CMD_TYPE_WIDTH;
                else Utils.getColumnHeader(ucao.lvAIMap, "Unit ID").Width = 0; //Hide...
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.SPORTSLAVE) Utils.getColumnHeader(ucao.lvAIMap, "Used").Width = COL_CMD_TYPE_WIDTH;
                else Utils.getColumnHeader(ucao.lvAIMap, "Used").Width = 0; //Hide...
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
            rootNode = xmlDoc.CreateElement("AOConfiguration");
            xmlDoc.AppendChild(rootNode);
            foreach (AO ai in aoList)
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
            rootNode = xmlDoc.CreateElement("AOMap");
            xmlDoc.AppendChild(rootNode);
            List<AOMap> slaveAIMapList;
            if (!slavesAIMapList.TryGetValue(slaveID, out slaveAIMapList))
            {
                Console.WriteLine("##### Slave entries for {0} does not exists", slaveID);
                return rootNode;
            }
            foreach (AOMap aimn in slaveAIMapList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(aimn.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }
            return rootNode;
        }
        public string exportINI(string slaveNum, string slaveID, string element, ref int ctr)
        {
            string iniData = "";
            List<AOMap> slaveAOMapList;
            if (!slavesAIMapList.TryGetValue(slaveID, out slaveAOMapList))
            {
                Console.WriteLine("AO INI: ##### Slave entries for {0} does not exists", slaveID);
                return iniData;
            }

            if (element == "AO")
            {
                foreach (AOMap aomn in slaveAOMapList)
                {
                    int ri;
                    try
                    {
                        ri = Int32.Parse(aomn.ReportingIndex);
                    }
                    catch (System.FormatException)
                    {
                        ri = 0;
                    }
                    //Ajay: 10/01/2019
                    if (slaveAOMapList.Where(x => x.ReportingIndex == ri.ToString()).Select(x => x).Count() > 1) //Ajay: 28/12/2018
                    {
                        MessageBox.Show("Duplicate Reporting Index (" + aomn.ReportingIndex + ") found of AO No " + aomn.AONo, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        iniData += "AO_" + ctr++ + "=" + Utils.GenerateIndex("AO", Utils.GetDataTypeIndex(aomn.DataType), ri).ToString() + Environment.NewLine;
                    }
                }

            }
            return iniData;
        }
        public void regenerateAOSequence()
        {
            string strRoutineName = "AOConfiguration: regenerateAISequence";
            try
            {
                foreach (AO ain in aoList)
                {
                    int oAONo = Int32.Parse(ain.AONo);
                    int nAONo = Globals.AONo++;
                    ain.AONo = nAONo.ToString();
                    foreach (KeyValuePair<string, List<AOMap>> maps in slavesAIMapList)  //Now Change in Map...
                    {
                        //Ajay: 10/01/2019 Reindexing issues reported by Aditya K. mail dtd. 27-12-2018
                        maps.Value.OfType<AOMap>().Where(x => x.AONo == oAONo.ToString() && !x.IsReindexed).ToList().ForEach(x => { x.AONo = nAONo.ToString(); x.IsReindexed = true; });
                    }
                    Utils.getOpenProPlusHandle().getParameterLoadConfiguration().ChangeAISequence(oAONo, nAONo);//Now change in Parameter Load nodes...
                }
                foreach (KeyValuePair<string, List<AOMap>> maps in slavesAIMapList) //Reset reindex status, for next use...
                {
                    List<AOMap> saimList = maps.Value;
                    foreach (AOMap aim in saimList)
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
            List<AOMap> slaveAIMapList;
            if (!slavesAIMapList.TryGetValue(slaveID, out slaveAIMapList))
            {
                return ret;
            }
            foreach (AOMap aim in slaveAIMapList)
            {
                if (aim.AONo == value.ToString()) return Int32.Parse(aim.ReportingIndex);
            }
            return ret;
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("AO_"))
            {
                CheckIEC104SlaveStatusChanges();  //If a IEC104 slave added/deleted, reflect in UI as well as objects.
                CheckMODBUSSlaveStatusChanges(); //If a MODBUS slave added/deleted, reflect in UI as well as objects.
                CheckIEC61850laveStatusChanges(); //If a IEC61850 slave added/deleted, reflect in UI as well as objects.
                CheckIEC101SlaveStatusChanges(); //If a IEC101 slave added/deleted, reflect in UI as well as objects.
                CheckSPORTSlaveStatusChanges();
                //ShowHideSlaveColumns();
                //Ajay:06/07/2018
                ShowHideSlaveColumnsSPORT();
                ShowHideSlaveColumnsMQTT();
                ShowHideSlaveColumnsDNP3();
                CheckMQTTSlaveStatusChanges();
                CheckSMSSlaveStatusChanges();
                CheckGDisplaySlaveStatusChanges();
                CheckDNPSlaveStatusChanges();
                return ucao;
            }
            return null;
        }
        public void parseAOCNode(XmlNode aicNode, bool imported)
        {
            string strRoutineName = "AOConfiguration: parseAICNode";
            try
            {
                if (aicNode == null)
                {
                    rnName = "AOConfiguration";
                    return;
                }
                rnName = aicNode.Name; //First set root node name...
                if (aicNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = aicNode.Value;
                }
                foreach (XmlNode node in aicNode)
                {
                    if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                    aoList.Add(new AO(node, masterType, masterNo, IEDNo, imported));
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
            string strRoutineName = "AOConfiguration: parseAIMNode";
            try
            {
                Task thDetails = new Task(() => CreateNewSlave(slaveNum, slaveID, aimNode));
                thDetails.Start();
                /*CreateNewSlave(slaveNum, slaveID, aimNode)*///;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public int getCount()
        {
            int ctr = 0;
            foreach (AO aiNode in aoList)
            {
                if (aiNode.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }
        public List<AO> getAOs()
        {
            return aoList;
        }
        public int getENMapCount()
        {
            int ctr = 0;
            fillMapOptions(Utils.getSlaveTypes(currentSlave));
            List<AOMap> senmList;
            if (!slavesAIMapList.TryGetValue(currentSlave, out senmList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                refreshMapList(null);
            }
            else
            {
                refreshMapList(senmList);
            }
            if (senmList == null)
            {
                return 0;
            }
            else
            {
                foreach (AOMap asaa in senmList)
                {
                    if (asaa.IsNodeComment) continue;
                    ctr++;
                }
            }
            return ctr;
        }
        public bool removeAI(string responseType, int Idx, int subIdx)
        {
            bool removed = false;
            for (int i = 0; i < aoList.Count; i++)
            {
                if (aoList[i].IsNodeComment) continue;
                AO tmp = aoList[i];
                if (tmp.Index == Idx.ToString() && tmp.SubIndex == subIdx.ToString() && tmp.ResponseType == responseType)
                {
                    aoList.RemoveAt(i);
                    removed = true;
                    break;
                }
            }
            return removed;
        }
        public List<AOMap> getSlaveAIMaps(string slaveID)
        {
            List<AOMap> slaveAIMapList;
            slavesAIMapList.TryGetValue(slaveID, out slaveAIMapList);
            return slaveAIMapList;
        }
        private string getMQTTSlaveKey(ListView lstview, string ainno)
        {
            int iColIndex = ucao.lvAIlist.Columns["Description"].Index;
            var query = lstview.Items
                    .Cast<ListViewItem>()
                    .Where(item => item.SubItems[0].Text == ainno).Select(s => s.SubItems[iColIndex].Text).Single();
            return query.ToString();
        }
        private void MQTTSlaveEvents()
        {
            //UnitID
            ucao.lblUnitID.Visible = false;
            ucao.txtUnitID.Visible = false;

            //Used
            ucao.chkUsed.Visible = false;

            //AIMNo
            ucao.lblANM.Location = new Point(13, 33);
            ucao.txtAIMNo.Location = new Point(119, 30);
            ucao.txtAIMNo.Size = new Size(220, 21);

            //AIMReportingIndex
            ucao.lblAMRI.Location = new Point(13, 58);
            ucao.txtAIMReportingIndex.Location = new Point(119, 55);
            ucao.txtAIMReportingIndex.Size = new Size(220, 21);

            //DataType
            ucao.lblAMDT.Visible = true;
            ucao.lblAMDT.Location = new Point(13, 83);
            ucao.cmbAIMDataType.Location = new Point(119, 80);
            ucao.cmbAIMDataType.Size = new Size(220, 21);

            //Deadband
            ucao.lblAMDB.Location = new Point(13, 109);
            ucao.txtAIMDeadBand.Location = new Point(119, 106);
            ucao.txtAIMDeadBand.Size = new Size(220, 21);

            //Multiplier
            ucao.lblAMM.Location = new Point(13, 135);
            ucao.txtAIMMultiplier.Location = new Point(119, 132);
            ucao.txtAIMMultiplier.Visible = true;
            ucao.txtAIMMultiplier.Size = new Size(220, 21);

            //Constant
            ucao.lblAMC.Location = new Point(13, 161);
            ucao.txtAIMConstant.Location = new Point(119, 158);
            ucao.txtAIMConstant.Size = new Size(220, 21);
            ucao.lblKey.Visible = true;
            ucao.lblKey.Location = new Point(13, 186);
            ucao.txtKey.Location = new Point(119, 183);
            ucao.txtKey.Size = new Size(220, 21);
            ucao.txtKey.Visible = true;
            ucao.txtUnit.Visible = true;
            ucao.lblUnit.Location = new Point(13, 211);
            ucao.txtUnit.Location = new Point(119, 208);
            ucao.txtUnit.Size = new Size(220, 21);
            ucao.lblUnit.Visible = true;
            //Description
            ucao.label5.Location = new Point(13, 236);
            ucao.txtMapDescription.Location = new Point(119, 233);
            ucao.txtMapDescription.Visible = true;
            ucao.txtMapDescription.Size = new Size(220, 21);

            //Automap
            ucao.AutoMapRange.Location = new Point(13, 261);
            ucao.textBox1.Location = new Point(119, 258);
            ucao.textBox1.Visible = true;
            ucao.textBox1.Size = new Size(220, 21);
            ucao.AutoMapRange.Visible = true;
            ucao.textBox1.Visible = true;

            //BtnDone & BtnCancel
            ucao.btnAIMDone.Location = new Point(120, 285);
            ucao.btnAIMCancel.Location = new Point(210, 285);

            //GrpAI
            ucao.grpAIMap.Size = new Size(355, 320);
            ucao.pbAIMHdr.Size = new Size(355, 22);
            ucao.grpAIMap.Location = new Point(243, 319);
        }
        private void SPORTSlave_EventsOnLoad()
        {
            string strRoutineName = "AOConfiguration:SPORTSlave_EventsOnLoad";
            try
            {
                //AIMNo
                ucao.lblANM.Location = new Point(13, 33);
                ucao.txtAIMNo.Location = new Point(119, 30);
                ucao.txtAIMNo.Size = new Size(165, 21);

                //UnitID
                ucao.lblUnitID.Visible = true;
                ucao.txtUnitID.Visible = true;
                ucao.lblUnitID.Location = new Point(13, 58);
                ucao.txtUnitID.Location = new Point(119, 55);
                ucao.txtUnitID.Size = new Size(165, 21);

                //ReportingIndex
                ucao.lblAMRI.Visible = true;
                ucao.txtAIMReportingIndex.Visible = true;
                ucao.lblAMRI.Location = new Point(13, 83);
                ucao.txtAIMReportingIndex.Location = new Point(119, 80);
                ucao.txtAIMReportingIndex.Size = new Size(165, 21);

                //DataType
                ucao.lblAMDT.Location = new Point(13, 109);
                ucao.cmbAIMDataType.Location = new Point(119, 106);
                ucao.cmbAIMDataType.Visible = true;
                ucao.cmbAIMDataType.Size = new Size(165, 21);

                //Deadband
                ucao.lblAMDB.Location = new Point(13, 135);
                ucao.txtAIMDeadBand.Location = new Point(119, 132);
                ucao.txtAIMDeadBand.Visible = true;
                ucao.txtAIMDeadBand.Size = new Size(165, 21);

                //Multiplier
                ucao.lblAMM.Location = new Point(13, 161);
                ucao.txtAIMMultiplier.Location = new Point(119, 158);
                ucao.txtAIMMultiplier.Size = new Size(165, 21);

                //Constant
                ucao.lblAMC.Location = new Point(13, 186);
                ucao.txtAIMConstant.Location = new Point(119, 183);
                ucao.txtAIMConstant.Visible = true;
                ucao.txtAIMConstant.Size = new Size(165, 21);

                //Description
                ucao.label5.Location = new Point(13, 211);
                ucao.txtMapDescription.Location = new Point(119, 208);
                ucao.txtMapDescription.Visible = true;
                ucao.txtMapDescription.Size = new Size(165, 21);

                //Automap
                ucao.AutoMapRange.Visible = true;
                ucao.textBox1.Visible = true;
                ucao.AutoMapRange.Location = new Point(13, 236);
                ucao.textBox1.Location = new Point(119, 233);
                ucao.textBox1.Visible = true;
                ucao.textBox1.Size = new Size(165, 21);

                //Used
                ucao.chkUsed.Visible = true;
                ucao.chkUsed.Location = new Point(18, 261);

                //BtnDone & BtnCancel
                ucao.btnAIMDone.Location = new Point(120, 265);
                ucao.btnAIMCancel.Location = new Point(210, 265);

                //GrpAIMap
                ucao.grpAIMap.Size = new Size(300, 305);
                ucao.grpAIMap.Location = new Point(350, 330);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void MODBUS_IEC101_IEC104Slave_EventsOnLoad()
        {
            string strRoutineName = "AOConfiguration: MODBUS_IEC101_IEC104Slave_EventsOnLoad";
            try
            {
                ucao.lblKey.Visible = false;
                ucao.txtKey.Visible = false;
                ucao.lblUnit.Visible = false;
                ucao.txtUnit.Visible = false;

                //UnitID
                ucao.lblUnitID.Visible = false;
                ucao.txtUnitID.Visible = false;

                //Used
                ucao.chkUsed.Visible = false;

                //AIMNo
                ucao.lblANM.Location = new Point(13, 33);
                ucao.txtAIMNo.Location = new Point(119, 30);
                ucao.txtAIMNo.Size = new Size(165, 21);

                //AIMReportingIndex
                ucao.lblAMRI.Location = new Point(13, 58);
                ucao.txtAIMReportingIndex.Location = new Point(119, 55);
                ucao.txtAIMReportingIndex.Size = new Size(165, 21);

                //DataType
                ucao.lblAMDT.Visible = true;
                ucao.lblAMDT.Location = new Point(13, 83);
                ucao.cmbAIMDataType.Location = new Point(119, 80);
                ucao.cmbAIMDataType.Size = new Size(165, 21);

                //Deadband
                ucao.lblAMDB.Location = new Point(13, 109);
                ucao.txtAIMDeadBand.Location = new Point(119, 106);
                ucao.txtAIMDeadBand.Size = new Size(165, 21);

                //Multiplier
                ucao.lblAMM.Location = new Point(13, 135);
                ucao.txtAIMMultiplier.Location = new Point(119, 132);
                ucao.txtAIMMultiplier.Visible = true;
                ucao.txtAIMMultiplier.Size = new Size(165, 21);

                //Constant
                ucao.lblAMC.Location = new Point(13, 161);
                ucao.txtAIMConstant.Location = new Point(119, 158);
                ucao.txtAIMConstant.Size = new Size(165, 21);

                //Description
                ucao.label5.Location = new Point(13, 186);
                ucao.txtMapDescription.Location = new Point(119, 183);
                ucao.txtMapDescription.Visible = true;
                ucao.txtMapDescription.Size = new Size(165, 21);

                //Automap
                ucao.AutoMapRange.Location = new Point(13, 211);
                ucao.textBox1.Location = new Point(119, 208);
                ucao.textBox1.Visible = true;
                ucao.textBox1.Size = new Size(165, 21);
                ucao.AutoMapRange.Visible = true;
                ucao.textBox1.Visible = true;

                //BtnDone & BtnCancel
                ucao.btnAIMDone.Location = new Point(120, 236);
                ucao.btnAIMCancel.Location = new Point(210, 236);

                //GrpAI
                ucao.grpAIMap.Size = new Size(307, 278);
                ucao.pbAIMHdr.Size = new Size(307, 22);
                ucao.grpAIMap.Location = new Point(450, 325);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MODBUS_IEC101_IEC104Slave_EventsDoubleClick()
        {
            string strRoutineName = "AOConfiguration:MODBUS_IEC101_IEC104Slave_EventsDoubleClick";
            try
            {
                ucao.lblKey.Visible = false;
                ucao.txtKey.Visible = false;
                ucao.lblUnit.Visible = false;
                ucao.txtUnit.Visible = false;
                ucao.lblANM.Location = new Point(13, 33);
                ucao.txtAIMNo.Location = new Point(119, 30);
                ucao.txtAIMNo.Size = new Size(165, 21);

                ucao.lblUnitID.Visible = false;
                ucao.txtUnitID.Visible = false;

                ucao.lblAMRI.Location = new Point(13, 58);
                ucao.txtAIMReportingIndex.Location = new Point(119, 55);
                ucao.txtAIMReportingIndex.Size = new Size(165, 21);

                ucao.lblAMDT.Visible = true;
                ucao.lblAMDT.Location = new Point(13, 83);
                ucao.cmbAIMDataType.Location = new Point(119, 80);
                ucao.cmbAIMDataType.Size = new Size(165, 21);

                ucao.lblAMDB.Location = new Point(13, 109);
                ucao.txtAIMDeadBand.Location = new Point(119, 106);
                ucao.txtAIMDeadBand.Size = new Size(165, 21);

                ucao.lblAMM.Location = new Point(13, 135);
                ucao.txtAIMMultiplier.Location = new Point(119, 132);
                ucao.txtAIMMultiplier.Visible = true;
                ucao.txtAIMMultiplier.Size = new Size(165, 21);

                ucao.lblAMC.Location = new Point(13, 161);
                ucao.txtAIMConstant.Location = new Point(119, 158);
                ucao.txtAIMConstant.Size = new Size(165, 21);

                ucao.label5.Location = new Point(13, 186);
                ucao.txtMapDescription.Location = new Point(119, 183);
                ucao.txtMapDescription.Visible = true;
                ucao.txtMapDescription.Size = new Size(165, 21);

                ucao.AutoMapRange.Location = new Point(13, 211);
                ucao.textBox1.Location = new Point(119, 208);
                ucao.textBox1.Visible = true;
                ucao.textBox1.Size = new Size(165, 21);

                ucao.AutoMapRange.Visible = false;
                ucao.textBox1.Visible = false;

                ucao.chkUsed.Visible = false;

                ucao.btnAIMDone.Location = new Point(120, 210);
                ucao.btnAIMCancel.Location = new Point(210, 210);

                ucao.grpAIMap.Size = new Size(307, 250);
                ucao.pbAIMHdr.Size = new Size(307, 22);
                ucao.grpAIMap.Location = new Point(450, 325);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SPORTSlave_EventsOnDoubleClick()
        {
            string strRoutineName = "AOConfiguration:SPORTSlave_EventsOnDoubleClick";
            try
            {
                ucao.lblANM.Location = new Point(13, 33);
                ucao.txtAIMNo.Location = new Point(119, 30);
                ucao.txtAIMNo.Size = new Size(165, 21);

                ucao.lblUnitID.Visible = true;
                ucao.txtUnitID.Visible = true;
                ucao.lblUnitID.Location = new Point(13, 58);
                ucao.txtUnitID.Location = new Point(119, 55);
                ucao.txtUnitID.Size = new Size(165, 21);

                ucao.lblAMRI.Visible = true;
                ucao.txtAIMReportingIndex.Visible = true;
                ucao.lblAMRI.Location = new Point(13, 83);
                ucao.txtAIMReportingIndex.Location = new Point(119, 80);
                ucao.txtAIMReportingIndex.Size = new Size(165, 21);

                ucao.lblAMDT.Location = new Point(13, 109);
                ucao.cmbAIMDataType.Location = new Point(119, 106);
                ucao.cmbAIMDataType.Visible = true;
                ucao.cmbAIMDataType.Size = new Size(165, 21);

                ucao.lblAMDB.Location = new Point(13, 135);
                ucao.txtAIMDeadBand.Location = new Point(119, 132);
                ucao.txtAIMDeadBand.Visible = true;
                ucao.txtAIMDeadBand.Size = new Size(165, 21);

                ucao.lblAMM.Location = new Point(13, 161);
                ucao.txtAIMMultiplier.Location = new Point(119, 158);
                ucao.txtAIMMultiplier.Size = new Size(165, 21);

                ucao.lblAMC.Location = new Point(13, 186);
                ucao.txtAIMConstant.Location = new Point(119, 183);
                ucao.txtAIMConstant.Visible = true;
                ucao.txtAIMConstant.Size = new Size(165, 21);

                ucao.label5.Location = new Point(13, 211);
                ucao.txtMapDescription.Location = new Point(119, 208);
                ucao.txtMapDescription.Visible = true;
                ucao.txtMapDescription.Size = new Size(165, 21);

                ucao.AutoMapRange.Visible = false;
                ucao.textBox1.Visible = false;
                ucao.AutoMapRange.Location = new Point(13, 236);
                ucao.textBox1.Location = new Point(119, 233);
                ucao.textBox1.Size = new Size(165, 21);

                ucao.chkUsed.Visible = true;
                ucao.chkUsed.Location = new Point(18, 240);

                ucao.btnAIMDone.Location = new Point(120, 240);
                ucao.btnAIMCancel.Location = new Point(210, 240);

                ucao.grpAIMap.Size = new Size(300, 280);
                ucao.grpAIMap.Location = new Point(350, 330);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool AllowMasterOptionsOnClick(MasterTypes mstrType)
        {
            string strRoutineName = "AOConfiguration: AllowMasterOptionsOnClick";
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
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //catch { return false; }
        }
        //Ajay: 07/12/2018
        private bool AllowSlaveOptionsOnClick(SlaveTypes slvType)
        {
            string strRoutineName = "AOConfiguration: AllowSlaveOptionsOnClick";
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
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        //Ajay: 02/11/2018
        public void IEC61850_OnLoad()
        {
            //Ajay: 02/11/2018
            string strRoutineName = "AOConfiguration: IEC61850_OnLoad";
            try
            {
                foreach (Control c in ucao.grpAO.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucao.lblAONo.Visible = ucao.txtAONo.Visible = true;
                ucao.txtAONo.Enabled = false;
                ucao.lblAONo.Location = new Point(15, 35);
                ucao.txtAONo.Location = new Point(102, 30);
                ucao.txtAONo.Size = new Size(300, 21);

                ucao.lblIEDName.Visible = ucao.cmbIEDName.Visible = true;
                ucao.lblIEDName.Location = new Point(15, 60);
                ucao.cmbIEDName.Location = new Point(102, 55);
                ucao.cmbIEDName.Size = new Size(300, 21);

                ucao.lbl61850ResponseType.Visible = ucao.cmb61850ResponseType.Visible = true;
                ucao.lbl61850ResponseType.Location = new Point(15, 85);
                ucao.cmb61850ResponseType.Location = new Point(102, 80);
                ucao.cmb61850ResponseType.Size = new Size(300, 21);

                ucao.lblFC.Visible = ucao.cmbFC.Visible = true;
                //ucao.cmbFC.Enabled = false;
                ucao.lblFC.Location = new Point(15, 110);
                ucao.cmbFC.Location = new Point(102, 105);
                ucao.cmbFC.Size = new Size(300, 21);

                ucao.lbl61850Index.Visible = ucao.cmb61850Index.Visible = false;
                ucao.lbl61850Index.Location = new Point(15, 135);
                ucao.cmb61850Index.Location = new Point(102, 130);
                ucao.cmb61850Index.Size = new Size(300, 21);

                //Namrata:27/03/2019
                ucao.lbl61850Index.Visible = ucao.ChkIEC61850Index.Visible = true;
                ucao.lbl61850Index.Location = new Point(15, 135);
                ucao.ChkIEC61850Index.Location = new Point(102, 130);
                ucao.ChkIEC61850Index.Size = new Size(300, 21);

                ucao.lblSubIndex.Visible = ucao.txtSubIndex.Visible = true;
                ucao.lblSubIndex.Location = new Point(15, 160);
                ucao.txtSubIndex.Location = new Point(102, 155);
                ucao.txtSubIndex.Size = new Size(300, 21);

                ucao.lblMultiplier.Visible = ucao.txtMultiplier.Visible = true;
                ucao.lblMultiplier.Location = new Point(15, 185);
                ucao.txtMultiplier.Location = new Point(102, 180);
                ucao.txtMultiplier.Size = new Size(300, 21);

                ucao.lblConstant.Visible = ucao.txtConstant.Visible = true;
                ucao.lblConstant.Location = new Point(15, 210);
                ucao.txtConstant.Location = new Point(102, 205);
                ucao.txtConstant.Size = new Size(300, 21);

                ucao.lblDescription.Visible = ucao.txtDescription.Visible = true;
                ucao.lblDescription.Location = new Point(15, 235);
                ucao.txtDescription.Location = new Point(102, 230);
                ucao.txtDescription.Size = new Size(300, 21);

                ucao.lblAutoMap.Visible = ucao.txtAIAutoMapRange.Visible = false;
                ucao.lblAutoMap.Location = new Point(15, 260);
                ucao.txtAIAutoMapRange.Location = new Point(102, 255);
                ucao.txtAIAutoMapRange.Size = new Size(300, 21);

                ucao.btnDone.Visible = ucao.btnCancel.Visible = true;
                ucao.btnDone.Location = new Point(135, 265);
                ucao.btnCancel.Location = new Point(235, 265);

                ucao.grpAO.Height = 310;
                ucao.grpAO.Width = 420;
                ucao.grpAO.Location = new Point(172, 52);
                ucao.pbHdr.Width = 510;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 02/11/2018
        public void SPORT_OnLoad()
        {
            //Ajay: 02/11/2018
            string strRoutineName = "AOConfiguration: SPORT_OnLoad";
            try
            {
                foreach (Control c in ucao.grpAO.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucao.lblAONo.Visible = ucao.txtAONo.Visible = true;
                ucao.txtAONo.Enabled = false;
                ucao.lblAONo.Location = new Point(15, 35);
                ucao.txtAONo.Location = new Point(102, 30);

                ucao.lblResponseType.Visible = ucao.cmbResponseType.Visible = true;
                ucao.lblResponseType.Location = new Point(15, 60);
                ucao.cmbResponseType.Location = new Point(102, 55);

                ucao.lblIndex.Visible = ucao.txtIndex.Visible = true;
                ucao.lblIndex.Location = new Point(15, 85);
                ucao.txtIndex.Location = new Point(102, 80);

                ucao.lblSubIndex.Visible = ucao.txtSubIndex.Visible = true;
                ucao.lblSubIndex.Location = new Point(15, 110);
                ucao.txtSubIndex.Location = new Point(102, 105);

                ucao.lblDataType.Visible = ucao.cmbDataType.Visible = true;
                ucao.lblDataType.Location = new Point(15, 135);
                ucao.cmbDataType.Location = new Point(102, 130);

                ucao.lblMultiplier.Visible = ucao.txtMultiplier.Visible = true;
                ucao.lblMultiplier.Location = new Point(15, 160);
                ucao.txtMultiplier.Location = new Point(102, 155);

                ucao.lblConstant.Visible = ucao.txtConstant.Visible = true;
                ucao.lblConstant.Location = new Point(15, 185);
                ucao.txtConstant.Location = new Point(102, 180);

                ucao.lblDescription.Visible = ucao.txtDescription.Visible = true;
                ucao.lblDescription.Location = new Point(15, 210);
                ucao.txtDescription.Location = new Point(102, 205);

                ucao.lblAutoMap.Visible = ucao.txtAIAutoMapRange.Visible = true;
                ucao.lblAutoMap.Location = new Point(15, 235);
                ucao.txtAIAutoMapRange.Location = new Point(102, 230);

                ucao.btnDone.Visible = ucao.btnCancel.Visible = true;
                ucao.btnDone.Location = new Point(75, 260);
                ucao.btnCancel.Location = new Point(175, 260);

                ucao.grpAO.Height = 300;
                ucao.grpAO.Width = 325;
                ucao.grpAO.Location = new Point(172, 52);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 02/11/2018
        public void ADR_IEC101_IEC103_Modbus_OnLoad()
        {
            //Ajay: 02/11/2018
            string strRoutineName = "AOConfiguration: ADR_IEC101_IEC103_Modbus_OnLoad";
            try
            {
                foreach (Control c in ucao.grpAO.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucao.lblAONo.Visible = ucao.txtAONo.Visible = true;
                ucao.txtAONo.Enabled = false;
                ucao.lblAONo.Location = new Point(15, 35);
                ucao.txtAONo.Location = new Point(102, 30);

                ucao.lblResponseType.Visible = ucao.cmbResponseType.Visible = true;
                ucao.lblResponseType.Location = new Point(15, 60);
                ucao.cmbResponseType.Location = new Point(102, 55);

                ucao.lblIndex.Visible = ucao.txtIndex.Visible = true;
                ucao.lblIndex.Location = new Point(15, 85);
                ucao.txtIndex.Location = new Point(102, 80);

                ucao.lblSubIndex.Visible = ucao.txtSubIndex.Visible = true;
                ucao.lblSubIndex.Location = new Point(15, 110);
                ucao.txtSubIndex.Location = new Point(102, 105);

                ucao.lblDataType.Visible = ucao.cmbDataType.Visible = true;
                ucao.lblDataType.Location = new Point(15, 135);
                ucao.cmbDataType.Location = new Point(102, 130);

                ucao.lblMultiplier.Visible = ucao.txtMultiplier.Visible = true;
                ucao.lblMultiplier.Location = new Point(15, 160);
                ucao.txtMultiplier.Location = new Point(102, 155);

                ucao.lblConstant.Visible = ucao.txtConstant.Visible = true;
                ucao.lblConstant.Location = new Point(15, 185);
                ucao.txtConstant.Location = new Point(102, 180);

                ucao.lblDescription.Visible = ucao.txtDescription.Visible = true;
                ucao.lblDescription.Location = new Point(15, 210);
                ucao.txtDescription.Location = new Point(102, 205);

                ucao.lblAutoMap.Visible = ucao.txtAIAutoMapRange.Visible = true;
                ucao.lblAutoMap.Location = new Point(15, 235);
                ucao.txtAIAutoMapRange.Location = new Point(102, 230);

                ucao.btnDone.Visible = ucao.btnCancel.Visible = true;
                ucao.btnDone.Location = new Point(75, 260);
                ucao.btnCancel.Location = new Point(175, 260);

                ucao.grpAO.Height = 300;
                ucao.grpAO.Width = 325;
                ucao.grpAO.Location = new Point(172, 52);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 02/11/2018
        public void IEC104_OnLoad()
        {
            //Ajay: 02/11/2018
            string strRoutineName = "AOConfiguration: IEC104_OnLoad";
            try
            {
                foreach (Control c in ucao.grpAO.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucao.lblAONo.Visible = ucao.txtAONo.Visible = true;
                ucao.txtAONo.Enabled = false;
                ucao.lblAONo.Location = new Point(15, 35);
                ucao.txtAONo.Location = new Point(102, 30);

                ucao.lblResponseType.Visible = ucao.cmbResponseType.Visible = true;
                ucao.lblResponseType.Location = new Point(15, 60);
                ucao.cmbResponseType.Location = new Point(102, 55);

                ucao.lblIndex.Visible = ucao.txtIndex.Visible = true;
                ucao.lblIndex.Location = new Point(15, 85);
                ucao.txtIndex.Location = new Point(102, 80);

                ucao.lblSubIndex.Visible = ucao.txtSubIndex.Visible = true;
                ucao.lblSubIndex.Location = new Point(15, 110);
                ucao.txtSubIndex.Location = new Point(102, 105);

                ucao.lblDataType.Visible = ucao.cmbDataType.Visible = true;
                ucao.lblDataType.Location = new Point(15, 135);
                ucao.cmbDataType.Location = new Point(102, 130);

                ucao.lblMultiplier.Visible = ucao.txtMultiplier.Visible = true;
                ucao.lblMultiplier.Location = new Point(15, 160);
                ucao.txtMultiplier.Location = new Point(102, 155);

                ucao.lblConstant.Visible = ucao.txtConstant.Visible = true;
                ucao.lblConstant.Location = new Point(15, 185);
                ucao.txtConstant.Location = new Point(102, 180);

                ucao.lblDescription.Visible = ucao.txtDescription.Visible = true;
                ucao.lblDescription.Location = new Point(15, 210);
                ucao.txtDescription.Location = new Point(102, 205);

                ucao.lblAutoMap.Visible = ucao.txtAIAutoMapRange.Visible = true;
                ucao.lblAutoMap.Location = new Point(15, 235);
                ucao.txtAIAutoMapRange.Location = new Point(102, 230);

                ucao.btnDone.Visible = ucao.btnCancel.Visible = true;
                ucao.btnDone.Location = new Point(75, 260);
                ucao.btnCancel.Location = new Point(175, 260);

                ucao.grpAO.Height = 300;
                ucao.grpAO.Width = 325;
                ucao.grpAO.Location = new Point(172, 52);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 31/07/2018
        public void LoadProfile_OnLoad()
        {
            string strRoutineName = "AOConfiguration: EventsOnLoad";
            try
            {
                foreach (Control c in ucao.grpAO.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucao.lblAONo.Visible = ucao.txtAONo.Visible = true;
                ucao.lblAONo.Location = new Point(15, 35);
                ucao.txtAONo.Location = new Point(102, 30);

                ucao.lblDescription.Visible = ucao.txtDescription.Visible = true;
                ucao.lblDescription.Location = new Point(15, 60);
                ucao.txtDescription.Location = new Point(102, 55);

                ucao.btnDone.Visible = ucao.btnCancel.Visible = true;
                ucao.btnDone.Location = new Point(102, 90);
                ucao.btnCancel.Location = new Point(223, 90);

                ucao.grpAO.Height = 130;
                ucao.grpAO.Location = new Point(172, 52);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 02/11/2018
        public void IEC61850_OnDoubleClick()
        {
            string strRoutineName = "AOConfiguration:IEC61850_OnDoubleClick";
            try
            {
                //Ajay: 02/11/2018
                foreach (Control c in ucao.grpAO.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucao.lblAONo.Visible = ucao.txtAONo.Visible = true;
                ucao.txtAONo.Enabled = false;
                ucao.lblAONo.Location = new Point(15, 35);
                ucao.txtAONo.Location = new Point(102, 30);
                ucao.txtAONo.Size = new Size(300, 21);

                ucao.lblIEDName.Visible = ucao.cmbIEDName.Visible = true;
                ucao.lblIEDName.Location = new Point(15, 60);
                ucao.cmbIEDName.Location = new Point(102, 55);
                ucao.cmbIEDName.Size = new Size(300, 21);

                ucao.lbl61850ResponseType.Visible = ucao.cmb61850ResponseType.Visible = true;
                ucao.lbl61850ResponseType.Location = new Point(15, 85);
                ucao.cmb61850ResponseType.Location = new Point(102, 80);
                ucao.cmb61850ResponseType.Size = new Size(300, 21);

                ucao.lblFC.Visible = ucao.cmbFC.Visible = true;
                //ucao.cmbFC.Enabled = false;
                ucao.lblFC.Location = new Point(15, 110);
                ucao.cmbFC.Location = new Point(102, 105);
                ucao.cmbFC.Size = new Size(300, 21);

                ucao.lbl61850Index.Visible = ucao.cmb61850Index.Visible = false;
                ucao.lbl61850Index.Location = new Point(15, 135);
                ucao.cmb61850Index.Location = new Point(102, 130);
                ucao.cmb61850Index.Size = new Size(300, 21);

                //Namrata:27/03/2019
                ucao.lbl61850Index.Visible = ucao.ChkIEC61850Index.Visible = true;
                ucao.lbl61850Index.Location = new Point(15, 135);
                ucao.ChkIEC61850Index.Location = new Point(102, 130);
                ucao.ChkIEC61850Index.Size = new Size(300, 21);

                ucao.lblSubIndex.Visible = ucao.txtSubIndex.Visible = true;
                ucao.lblSubIndex.Location = new Point(15, 160);
                ucao.txtSubIndex.Location = new Point(102, 155);
                ucao.txtSubIndex.Size = new Size(300, 21);

                ucao.lblMultiplier.Visible = ucao.txtMultiplier.Visible = true;
                ucao.lblMultiplier.Location = new Point(15, 185);
                ucao.txtMultiplier.Location = new Point(102, 180);
                ucao.txtMultiplier.Size = new Size(300, 21);

                ucao.lblConstant.Visible = ucao.txtConstant.Visible = true;
                ucao.lblConstant.Location = new Point(15, 210);
                ucao.txtConstant.Location = new Point(102, 205);
                ucao.txtConstant.Size = new Size(300, 21);

                ucao.lblDescription.Visible = ucao.txtDescription.Visible = true;
                ucao.lblDescription.Location = new Point(15, 235);
                ucao.txtDescription.Location = new Point(102, 230);
                ucao.txtDescription.Size = new Size(300, 21);

                //ucao.lblAutoMap.Visible = ucao.txtAIAutoMapRange.Visible = true;
                //ucao.lblAutoMap.Location = new Point(15, 260);
                //ucao.txtAIAutoMapRange.Location = new Point(102, 255);
                //ucao.txtAIAutoMapRange.Size = new Size(300, 21);

                ucao.btnDone.Visible = ucao.btnCancel.Visible = true;
                ucao.btnDone.Location = new Point(135, 260);
                ucao.btnCancel.Location = new Point(235, 260);

                ucao.btnFirst.Visible = ucao.btnPrev.Visible = ucao.btnNext.Visible = ucao.btnLast.Visible = true;
                ucao.btnFirst.Location = new Point(80, 295);
                ucao.btnPrev.Location = new Point(150, 295);
                ucao.btnNext.Location = new Point(220, 295);
                ucao.btnLast.Location = new Point(290, 295);

                ucao.grpAO.Height = 320;
                ucao.grpAO.Width = 420;
                ucao.grpAO.Location = new Point(172, 52);
                ucao.pbHdr.Width = 510;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 02/11/2018
        public void SPORT_OnDoubleClick()
        {
            //Ajay: 02/11/2018
            string strRoutineName = "AOConfiguration: SPORT_OnDoubleClick";
            try
            {
                foreach (Control c in ucao.grpAO.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucao.lblAONo.Visible = ucao.txtAONo.Visible = true;
                ucao.txtAONo.Enabled = false;
                ucao.lblAONo.Location = new Point(15, 35);
                ucao.txtAONo.Location = new Point(102, 30);

                ucao.lblResponseType.Visible = ucao.cmbResponseType.Visible = true;
                ucao.lblResponseType.Location = new Point(15, 60);
                ucao.cmbResponseType.Location = new Point(102, 55);

                ucao.lblIndex.Visible = ucao.txtIndex.Visible = true;
                ucao.lblIndex.Location = new Point(15, 85);
                ucao.txtIndex.Location = new Point(102, 80);

                ucao.lblSubIndex.Visible = ucao.txtSubIndex.Visible = true;
                ucao.lblSubIndex.Location = new Point(15, 110);
                ucao.txtSubIndex.Location = new Point(102, 105);

                ucao.lblDataType.Visible = ucao.cmbDataType.Visible = true;
                ucao.lblDataType.Location = new Point(15, 135);
                ucao.cmbDataType.Location = new Point(102, 130);

                ucao.lblMultiplier.Visible = ucao.txtMultiplier.Visible = true;
                ucao.lblMultiplier.Location = new Point(15, 160);
                ucao.txtMultiplier.Location = new Point(102, 155);

                ucao.lblConstant.Visible = ucao.txtConstant.Visible = true;
                ucao.lblConstant.Location = new Point(15, 185);
                ucao.txtConstant.Location = new Point(102, 180);

                ucao.lblDescription.Visible = ucao.txtDescription.Visible = true;
                ucao.lblDescription.Location = new Point(15, 210);
                ucao.txtDescription.Location = new Point(102, 205);

                //ucao.lblAutoMap.Visible = ucao.txtAIAutoMapRange.Visible = true;
                //ucao.lblAutoMap.Location = new Point(15, 210);
                //ucao.txtAIAutoMapRange.Location = new Point(102, 205);

                ucao.btnDone.Visible = ucao.btnCancel.Visible = true;
                ucao.btnDone.Location = new Point(75, 235);
                ucao.btnCancel.Location = new Point(175, 235);

                ucao.btnFirst.Visible = ucao.btnPrev.Visible = ucao.btnNext.Visible = ucao.btnLast.Visible = true;
                ucao.btnFirst.Location = new Point(13, 270);
                ucao.btnPrev.Location = new Point(88, 270);
                ucao.btnNext.Location = new Point(163, 270);
                ucao.btnLast.Location = new Point(238, 270);

                ucao.grpAO.Height = 305;
                ucao.grpAO.Width = 325;
                ucao.grpAO.Location = new Point(172, 52);

            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 02/11/2018
        public void IEC104_OnDoubleClick()
        {
            string strRoutineName = "AOConfiguration: IEC104_OnDoubleClick";
            try
            {
                foreach (Control c in ucao.grpAO.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                //AONo
                ucao.lblAONo.Visible = ucao.txtAONo.Visible = true;
                ucao.txtAONo.Enabled = false;
                ucao.lblAONo.Location = new Point(15, 35);
                ucao.txtAONo.Location = new Point(102, 30);

                //Index
                ucao.lblIndex.Visible = ucao.txtIndex.Visible = true;
                ucao.lblIndex.Location = new Point(15, 85);
                ucao.txtIndex.Location = new Point(102, 80);

                //DataType
                ucao.lblDataType.Visible = ucao.cmbDataType.Visible = true;
                ucao.lblDataType.Location = new Point(15, 135);
                ucao.cmbDataType.Location = new Point(102, 130);

                //ResponseType
                ucao.lblResponseType.Visible = ucao.cmbResponseType.Visible = true;
                ucao.lblResponseType.Location = new Point(15, 60);
                ucao.cmbResponseType.Location = new Point(102, 55);



                ucao.lblSubIndex.Visible = ucao.txtSubIndex.Visible = true;
                ucao.lblSubIndex.Location = new Point(15, 110);
                ucao.txtSubIndex.Location = new Point(102, 105);


                ucao.lblMultiplier.Visible = ucao.txtMultiplier.Visible = true;
                ucao.lblMultiplier.Location = new Point(15, 160);
                ucao.txtMultiplier.Location = new Point(102, 155);

                ucao.lblConstant.Visible = ucao.txtConstant.Visible = true;
                ucao.lblConstant.Location = new Point(15, 185);
                ucao.txtConstant.Location = new Point(102, 180);

                ucao.lblDescription.Visible = ucao.txtDescription.Visible = true;
                ucao.lblDescription.Location = new Point(15, 210);
                ucao.txtDescription.Location = new Point(102, 205);

                //ucao.lblAutoMap.Visible = ucao.txtAIAutoMapRange.Visible = true;
                //ucao.lblAutoMap.Location = new Point(15, 235);
                //ucao.txtAIAutoMapRange.Location = new Point(102, 230);

                //ucao.ChkEvent.Visible = true;
                //ucao.ChkEvent.Checked = false;
                //ucao.ChkEvent.Location = new Point(15, 235);

                ucao.btnDone.Visible = ucao.btnCancel.Visible = true;
                ucao.btnDone.Location = new Point(75, 235);
                ucao.btnCancel.Location = new Point(175, 235);

                ucao.btnFirst.Visible = ucao.btnPrev.Visible = ucao.btnNext.Visible = ucao.btnLast.Visible = true;
                ucao.btnFirst.Location = new Point(13, 265);
                ucao.btnPrev.Location = new Point(88, 265);
                ucao.btnNext.Location = new Point(163, 265);
                ucao.btnLast.Location = new Point(238, 265);

                ucao.grpAO.Height = 300;
                ucao.grpAO.Width = 325;
                ucao.grpAO.Location = new Point(172, 52);

                //Ajay: 02/11/2018 Commented
                //ucao.ChkEvent.Visible = true; //Ajay: 26/07/2018
                //ucao.ChkEvent.Location = new Point(15, 210); //Ajay: 26/07/2018

                //ucao.lblIEDName.Visible = false;
                //ucao.cmbIEDName.Visible = false; ;
                //ucao.lbl61850ResponseType.Visible = false;
                //ucao.cmb61850ResponseType.Visible = false;
                //ucao.lbl61850Index.Visible = false;
                //ucao.cmb61850Index.Visible = false;
                //ucao.lblIEDName.Enabled = false;
                //ucao.cmbIEDName.Enabled = false;
                //ucao.lbl61850ResponseType.Enabled = false;
                //ucao.cmb61850ResponseType.Enabled = false;
                //ucao.lbl61850Index.Enabled = false;
                //ucao.cmb61850Index.Enabled = false;
                //ucao.lblDescription.Location = new Point(15, 185);
                //ucao.txtDescription.Location = new Point(102, 182);
                //ucao.lblAutoMap.Visible = false;
                //ucao.txtAIAutoMapRange.Visible = false;
                //ucao.btnDone.Location = new Point(102, 210);
                //ucao.btnCancel.Location = new Point(223, 210);
                //ucao.btnFirst.Location = new Point(13, 245);
                //ucao.btnPrev.Location = new Point(88, 245);
                //ucao.btnNext.Location = new Point(163, 245);
                //ucao.btnLast.Location = new Point(238, 245);
                //ucao.grpAO.Location = new Point(172, 52);
                //ucao.grpAO.Height = 270;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 31/07/2018
        public void LoadProfile_OnDoubleClick()
        {
            string strRoutineName = "AOConfiguration: LoadProfile_OnDoubleClick";
            try
            {
                foreach (Control c in ucao.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                }

                ucao.lblAONo.Visible = ucao.txtAONo.Visible = true;
                ucao.lblAONo.Location = new Point(15, 35);
                ucao.txtAONo.Location = new Point(102, 30);

                ucao.lblDescription.Visible = ucao.txtDescription.Visible = true;
                ucao.lblDescription.Location = new Point(15, 60);
                ucao.txtDescription.Location = new Point(102, 55);

                ucao.btnDone.Visible = ucao.btnCancel.Visible = true;
                ucao.btnDone.Location = new Point(102, 90);
                ucao.btnCancel.Location = new Point(223, 90);

                ucao.btnFirst.Visible = ucao.btnPrev.Visible = ucao.btnNext.Visible = ucao.btnLast.Visible = true;
                ucao.btnFirst.Location = new Point(13, 125);
                ucao.btnPrev.Location = new Point(88, 125);
                ucao.btnNext.Location = new Point(163, 125);
                ucao.btnLast.Location = new Point(238, 125);

                ucao.grpAO.Height = 155;
                ucao.grpAO.Location = new Point(172, 52);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ADR_IEC101_IEC103_Modbus_OnDoubleClick()
        {
            //Ajay: 02/11/2018
            string strRoutineName = "AOConfiguration: ADR_IEC101_IEC103_Modbus_OnDoubleClick";
            try
            {
                foreach (Control c in ucao.grpAO.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucao.lblAONo.Visible = ucao.txtAONo.Visible = true;
                ucao.txtAONo.Enabled = false;
                ucao.lblAONo.Location = new Point(15, 35);
                ucao.txtAONo.Location = new Point(102, 30);

                ucao.lblResponseType.Visible = ucao.cmbResponseType.Visible = true;
                ucao.lblResponseType.Location = new Point(15, 60);
                ucao.cmbResponseType.Location = new Point(102, 55);

                ucao.lblIndex.Visible = ucao.txtIndex.Visible = true;
                ucao.lblIndex.Location = new Point(15, 85);
                ucao.txtIndex.Location = new Point(102, 80);

                ucao.lblSubIndex.Visible = ucao.txtSubIndex.Visible = true;
                ucao.lblSubIndex.Location = new Point(15, 110);
                ucao.txtSubIndex.Location = new Point(102, 105);

                ucao.lblDataType.Visible = ucao.cmbDataType.Visible = true;
                ucao.lblDataType.Location = new Point(15, 135);
                ucao.cmbDataType.Location = new Point(102, 130);

                ucao.lblMultiplier.Visible = ucao.txtMultiplier.Visible = true;
                ucao.lblMultiplier.Location = new Point(15, 160);
                ucao.txtMultiplier.Location = new Point(102, 155);

                ucao.lblConstant.Visible = ucao.txtConstant.Visible = true;
                ucao.lblConstant.Location = new Point(15, 185);
                ucao.txtConstant.Location = new Point(102, 180);

                ucao.lblDescription.Visible = ucao.txtDescription.Visible = true;
                ucao.lblDescription.Location = new Point(15, 210);
                ucao.txtDescription.Location = new Point(102, 205);

                //ucao.lblAutoMap.Visible = ucao.txtAIAutoMapRange.Visible = true;
                //ucao.lblAutoMap.Location = new Point(15, 235);
                //ucao.txtAIAutoMapRange.Location = new Point(102, 230);

                ucao.btnDone.Visible = ucao.btnCancel.Visible = true;
                ucao.btnDone.Location = new Point(75, 235);
                ucao.btnCancel.Location = new Point(175, 235);

                ucao.btnFirst.Visible = ucao.btnPrev.Visible = ucao.btnNext.Visible = ucao.btnLast.Visible = true;
                ucao.btnFirst.Location = new Point(13, 270);
                ucao.btnPrev.Location = new Point(88, 270);
                ucao.btnNext.Location = new Point(163, 270);
                ucao.btnLast.Location = new Point(238, 270);

                ucao.grpAO.Height = 305;
                ucao.grpAO.Width = 325;
                ucao.grpAO.Location = new Point(172, 52);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
