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
namespace OpenProPlusConfigurator
{
    public class DIConfiguration
    {
        #region Declaration
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        //Namrata:12/04/2019
        List<string> ObjectRefForMap = null;//Namrata:12/04/2019
        List<string> NodeForMap = null;//Namrata:12/04/2019
        List<string> MergeListMap = null;//Namrata:25/03/2019
        int intMapCheckItems = 0;
        int intCheckItems = 0;
        DataSet dsdummy = null; //Ajay: 17/01/2019
        List<string> IEC61850CheckedList = null;
        private int SelectedIndex;
        List<string> MergeList = null;
        List<string> ObjectRef = null;
        List<string> FC = null;
        private string rnName = "";
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        private Mode mapMode = Mode.NONE;
        private int mapEditIndex = -1;
        private bool isNodeComment = false;
        private string comment = "";
        private string currentSlave = "";
        Dictionary<string, List<DIMap>> slavesDIMapList = new Dictionary<string, List<DIMap>>();
        private MasterTypes masterType = MasterTypes.UNKNOWN;
        private int masterNo = -1;
        private int IEDNo = -1;
        private const int COL_CMD_TYPE_WIDTH = 130;
        //Namrata: 11/09/2017
        //Fill RessponseType in All Configuration . 
        public DataGridView dataGridViewDataSet = new DataGridView();
        public DataTable dtdataset = new DataTable();
        DataRow datasetRow;
        private string Response = "";
        List<DI> diList = new List<DI>();
        ucDIlist ucdi = new ucDIlist();
        DataSet DsRCB = new DataSet();
        List<DIMap> slaveDIMapList;
        private RCBConfiguration RCBNode = null;
        List<string> IEC61850MapCheckedList = null;
        #endregion Declaration

        public DIConfiguration(MasterTypes mType, int mNo, int iNo)
        {
            string strRoutineName = "DIConfiguration";
            try
            {
                ucdi.splitContainer3.Panel2Collapsed = true;
                masterType = mType;
                masterNo = mNo;
                IEDNo = iNo;
                ucdi.ucDIlistLoad += new System.EventHandler(this.ucDIlist_Load);
                #region Btn Events
                ucdi.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                ucdi.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                ucdi.btnVerifyClick += new EventHandler(this.btnVerify_Click); //Ajay: 31/07/2018
                ucdi.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucdi.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucdi.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                ucdi.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                ucdi.btnNextClick += new System.EventHandler(this.btnNext_Click);
                ucdi.btnLastClick += new System.EventHandler(this.btnLast_Click);
                ucdi.btnDIMDeleteClick += new System.EventHandler(this.btnDIMDelete_Click);
                ucdi.btnDIMDoneClick += new System.EventHandler(this.btnDIMDone_Click);
                ucdi.btnDIMCancelClick += new System.EventHandler(this.btnDIMCancel_Click);
                #endregion Btn Events

                #region LinkButton Events
                ucdi.linkDOClick += new System.EventHandler(this.linkDO_Click);
                ucdi.linkLabel1Click += new System.EventHandler(this.linkLabel1_Click);
                #endregion LinkButton Events

                #region Combobox Events
                ucdi.cmbIEDName.SelectedIndexChanged += new System.EventHandler(this.cmbIEDName_SelectedIndexChanged);
                ucdi.cmb61850DIResponseType.SelectedIndexChanged += new System.EventHandler(this.cmb61850DIResponseType_SelectedIndexChanged);
                ucdi.cmb61850DIIndex.SelectedIndexChanged += new System.EventHandler(this.cmb61850DIIndex_SelectedIndexChanged);
                ucdi.cmbFCSelectedIndexChanged += new System.EventHandler(this.cmbFC_SelectedIndexChanged); //Ajay: 17/01/2019
                ucdi.ChkIEC61850Index.SelectedIndexChanged += new System.EventHandler(this.ChkIEC61850Index_SelectedIndexChanged);//Namrata:22/03/2019
                ucdi.CmbCellNoSelectedIndexChanged += new System.EventHandler(this.CmbCellNo_SelectedIndexChanged); //Ajay: 17/01/2019
                #endregion Combobox Events

                #region Listview Events
                ucdi.lvDIMapDoubleClick += new System.EventHandler(this.lvDIMap_DoubleClick);
                ucdi.lvDIlistDoubleClick += new System.EventHandler(this.lvDIlist_DoubleClick); //Ajay: 12/11/2018
                ucdi.lvDIlistItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDIlist_ItemSelectionChanged);
                ucdi.lvDIMapItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDIMap_ItemSelectionChanged);//Namrata: 27/07/2017
                #endregion Listview Events

                #region All Masters
                if (mType == MasterTypes.Virtual)//Disable add/edit/delete/dblclick n remove checkboxes...
                {
                    Virtual_OnLoad();
                }
                if (mType == MasterTypes.IEC101)
                {
                    ucdi.ChkEvent.Visible = false;//Ajay: 14/07/2018
                    IEC101_IEC103_SPORT_OnLoad();
                }
                if (mType == MasterTypes.SPORT)
                {
                    ucdi.ChkEvent.Visible = false; //Ajay: 14/07/2018
                    IEC101_IEC103_SPORT_OnLoad();
                }
                if (mType == MasterTypes.IEC104)
                {
                    IEC104_OnLoad();//Ajay: 18/07/2018
                }
                if (mType == MasterTypes.IEC103)
                {
                    ucdi.ChkEvent.Visible = false; //Ajay: 14/07/2018
                    IEC101_IEC103_SPORT_OnLoad();
                }
                if (mType == MasterTypes.ADR)
                {
                    ucdi.ChkEvent.Visible = false; //Ajay: 14/07/2018
                    ADR_OnLoad();
                }
                if (mType == MasterTypes.IEC61850Client)
                {
                    IEC61850Client_OnLoad();
                }
                if (mType == MasterTypes.MODBUS)
                {
                    ucdi.ChkEvent.Visible = false;//Ajay: 14/07/2018
                    MODBUS_OnLoad();
                }
                //Ajay: 31/07/2018
                if (mType == MasterTypes.LoadProfile)
                {
                    LoadProfile_OnLoad();
                }
                #endregion All Masters
                addListHeaders();
                fillOptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GetDataForGDisplaySlave()
        {
            string strRoutineName = "DOConfiguration:GetDataForGDisplaySlave";
            try
            {
                Utils.CurrentSlaveFinal = currentSlave;
                ucdi.CmbCellNo.DataSource = null;
                if (GDSlave.DsExcelData.Tables.Count > 0)
                {
                    GDSlave.ListImageDIDO();
                    ListToDataTable(GDSlave.ListDIDO);
                    List<string> tblNameList = GDSlave.DsExcelData.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                    string CurrentSlave = tblNameList.Where(x => x.Contains(Utils.CurrentSlaveFinal)).Select(x => x).FirstOrDefault();
                    GDSlave.CurrentSlave = CurrentSlave;
                    var CellNo = Utils.GetListFromDT_StringCol(GDSlave.DsExcelData.Tables[CurrentSlave], "CellNo", false);

                    List<string> DistinctDataSetCellNo = GDSlave.DsExcelData.Tables[CurrentSlave].Rows.OfType<DataRow>()
                                                   .Where(x => x.Field<string>("Widget").Contains("Blank") || x.Field<string>("Widget").Contains("SW_")).Select(x => x.Field<string>("CellNo")).Distinct().ToList();

                    List<string> DistinctDataSetWidgets = GDSlave.DsExcelData.Tables[CurrentSlave].Rows.OfType<DataRow>()
                                                   .Where(x => x.Field<string>("Widget").Contains("Blank") || x.Field<string>("Widget").Contains("SW_")).Select(x => x.Field<string>("Widget")).Distinct().ToList();
                    if (DistinctDataSetCellNo.Count > 0)
                    {
                        ucdi.CmbCellNo.DataSource = null;
                        ucdi.CmbCellNo.Items.Clear();
                        ucdi.CmbCellNo.DataSource = DistinctDataSetCellNo;
                        ucdi.CmbCellNo.SelectedIndex = 0;
                    }
                    if (DistinctDataSetWidgets.Count > 0)
                    {
                        ucdi.CmbWidget.DataSource = null;
                        ucdi.CmbWidget.Items.Clear();
                        ucdi.CmbWidget.DataSource = DistinctDataSetWidgets;
                        ucdi.CmbWidget.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Namrata: 16/10/2019
        Graphics graphics;
        private void FocusPictureBox(string id, bool IsFocused)
        {
            for (int i = 1; i <= 60; i++)
            {
                Color GreenColour = Color.FromArgb(144, 202, 249);
                PictureBox pb = (PictureBox)ucdi.TblLayoutCSLD.Controls[i.ToString()];
                if (pb != null)
                {
                    if (i.ToString() == id)
                    {
                        if (IsFocused == true)
                        {
                            //pb.BorderStyle = BorderStyle.FixedSingle;
                            graphics = pb.CreateGraphics();
                            Color color = Color.DarkBlue; pb.BackColor = GreenColour;
                            ControlPaint.DrawBorder(graphics, pb.ClientRectangle, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid, color, 2, ButtonBorderStyle.Solid);
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
            string strRoutineName = "DIConfiguration:CmbCellNo_SelectedIndexChanged";
            try
            {
                if (ucdi.CmbCellNo.Items.Count > 1)
                {
                    if ((ucdi.CmbCellNo.SelectedIndex != -1))
                    {
                        var rowColl = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable();

                        if (rowColl.Where(x => x["CellNo"].ToString() == ucdi.CmbCellNo.SelectedItem.ToString()).Any())
                        {
                            string Widget = rowColl.Where(x => x["CellNo"].ToString() == ucdi.CmbCellNo.SelectedItem.ToString()).Select(x => x["Widget"].ToString()).FirstOrDefault();
                            //string Widget = (from r in rowColl
                            //                 where r.Field<string>("CellNo") == ucdo.CmbCellNo.SelectedItem.ToString()
                            //                 select r.Field<string>("Widget")).First<string>();
                            ucdi.CmbWidget.Enabled = false;
                            ucdi.CmbWidget.SelectedIndex = ucdi.CmbWidget.FindStringExact(Widget);
                            ucdi.CmbWidget.SelectedItem = Widget;
                            //Namrata: 15/10/2019
                            FocusPictureBox(ucdi.CmbCellNo.Text, true);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CmbCellNo_SelectedIndexChangedOld(object sender, EventArgs e)
        {
            string strRoutineName = "DIConfiguration:CmbCellNo_SelectedIndexChanged";
            try
            {
                if (ucdi.CmbCellNo.Items.Count > 1)
                {
                    if ((ucdi.CmbCellNo.SelectedIndex != -1))
                    {
                        var rowColl = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable();
                        string name = (from r in rowColl
                                       where r.Field<string>("CellNo") == ucdi.CmbCellNo.SelectedItem.ToString()
                                       select r.Field<string>("Widget")).First<string>();
                        ucdi.CmbWidget.Enabled = false;
                        ucdi.CmbWidget.SelectedIndex = ucdi.CmbWidget.FindStringExact(name);
                        ucdi.CmbWidget.SelectedItem = name;
                        //Namrata: 15/10/2019
                        FocusPictureBox(ucdi.CmbCellNo.Text, true);
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
            string strRoutineName = "DIConfiguration:CmbCellNo_SelectedIndexChanged";
            try
            {
                if (ucdi.CmbCellNo.Items.Count > 1)
                {
                    if ((ucdi.CmbCellNo.SelectedIndex != -1))
                    {
                        var rowColl = GDSlave.DtDIDOWidgets.AsEnumerable();//GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable();
                        string name = (from r in rowColl
                                       where r.Field<string>("CellNo") == ucdi.CmbCellNo.SelectedItem.ToString()
                                       select r.Field<string>("Widget")).First<string>();
                        ucdi.CmbWidget.Enabled = false;
                        ucdi.CmbWidget.SelectedIndex = ucdi.CmbWidget.FindStringExact(name);
                        ucdi.CmbWidget.SelectedItem = name;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ucDIlist_Load(object sender, EventArgs e)
        {
            string strRoutineName = "DIConfiguration: ucDIlist_Load";
            try
            {
                ucdi.button1.Visible = false;
                foreach (var L in Utils.VirtualPLU)
                {
                    if (L.Run == "YES")
                    {
                        ucdi.btnAdd.Enabled = true;
                        ucdi.btnDelete.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string strRoutinename = "DIConfiguration: btnAdd_Click";
            try
            {
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)// Ajay: 07/12/2018
                {
                    if (AllowMasterOptionsOnClick(masterType)) { return; }
                    else { }
                }
                if (diList.Count >= getMaxDIs())
                {
                    MessageBox.Show("Maximum " + getMaxDIs() + " DI's are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                mode = Mode.ADD;
                editIndex = -1;
                if (masterType == MasterTypes.ADR)
                {
                    ucdi.txtAutoMap.Text = "1";
                    ADR_OnLoad();
                }
                //Ajay: 18/07/2018
                else if ((masterType == MasterTypes.IEC101) || (masterType == MasterTypes.IEC103) || (masterType == MasterTypes.SPORT))
                {
                    ucdi.txtAutoMap.Text = "1";
                    IEC101_IEC103_SPORT_OnLoad();
                }
                else if (masterType == MasterTypes.IEC104)  //Ajay: 18/07/2018 Separate condition handled for IEC104
                {
                    ucdi.txtAutoMap.Text = "1";
                    IEC104_OnLoad();
                }
                else if (masterType == MasterTypes.MODBUS)
                {
                    ucdi.txtAutoMap.Text = "1";
                    MODBUS_OnLoad();
                }
                else if (masterType == MasterTypes.IEC61850Client)
                {
                    ucdi.txtAutoMap.Text = "";
                    IEC61850Client_OnLoad();
                    FetchComboboxData();
                }
                else if (masterType == MasterTypes.Virtual)
                {
                    ucdi.txtAutoMap.Text = "1";
                    Virtual_OnLoad();
                }
                else if (masterType == MasterTypes.LoadProfile)//Ajay: 31/07/2018
                {
                    ucdi.txtAutoMap.Text = "1";
                    LoadProfile_OnLoad();
                }
                Utils.resetValues(ucdi.grpDI);
                Utils.showNavigation(ucdi.grpDI, false);
                fillOptions(); //Ajay: 22/09/2018
                loadDefaults();
                ucdi.grpDI.Visible = true;
                ucdi.cmbResponseType.Focus();
                if (masterType == MasterTypes.IEC61850Client)//Namrata: 04/04/2018
                {
                    if (ucdi.cmbIEDName.SelectedIndex != -1)
                    {
                        ucdi.ChkIEC61850Index.Text = "";//Namrata:25/03/2019
                        if (IEC61850CheckedList != null)
                        {
                            ucdi.ChkIEC61850Index.CheckBoxItems.ForEach(x =>
                            {
                                x.Checked = false; x.CheckState = CheckState.Unchecked;
                            });
                        }
                        ucdi.cmbIEDName.SelectedIndex = ucdi.cmbIEDName.FindStringExact(Utils.Iec61850IEDname);
                    }
                    else
                    {
                        ucdi.cmbIEDName.Visible = false;
                        MessageBox.Show("ICD File Missing !!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutinename + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            string strRoutineName = "DIConfiguration: btnDone_Click";
            try
            {
                #region MasterTypes.LoadProfile
                //Ajay: 31/07/2018
                if (masterType == MasterTypes.LoadProfile)
                {
                    string[] str = new string[2];
                    if (!IsVerified(ucdi.cmbDI1, "DI1", out str))
                    {
                        string dlgMsg = str[0];
                        if (!string.IsNullOrEmpty(str[1])) { dlgMsg += "=" + str[1]; }
                        dlgMsg += " is not valid." + Environment.NewLine + "Do you want to continue?";
                        DialogResult rslt = MessageBox.Show(dlgMsg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (rslt.ToString().ToLower() == "no") { return; }
                    }
                }
                #endregion MasterTypes.LoadProfile

                Utils.diList1.Clear();
                List<KeyValuePair<string, string>> diData = Utils.getKeyValueAttributes(ucdi.grpDI);
                #region Fill Address to Datatable for RCBConfiguration 
                //Namrata: 27/09/2017
                if (masterType == MasterTypes.IEC61850Client)
                {
                    IEC61850CheckedList = ucdi.ChkIEC61850Index.CheckBoxItems.Where(x => x.Checked == true).Select(x => x.Text).ToList();//Namrata:25/03/2019
                    Response = ucdi.cmb61850DIResponseType.Text;
                    DataColumn dcAddressColumn;
                    if (!dtdataset.Columns.Contains("Address"))
                    { dcAddressColumn = dtdataset.Columns.Add("Address", typeof(string)); }
                    if (!dtdataset.Columns.Contains("IED"))
                    { dtdataset.Columns.Add("IED", typeof(string)); }
                    datasetRow = dtdataset.NewRow();
                    datasetRow["Address"] = Response.ToString();
                    datasetRow["IED"] = IEDNo.ToString();
                    dtdataset.Rows.Add(datasetRow);
                    dataGridViewDataSet.DataSource = dtdataset;//Namrata: 15/03/2018
                    dtdataset.TableName = Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname;
                    string Index112 = "";
                    string[] arr112 = new string[dtdataset.Rows.Count];
                    string[] arrCo1l12 = new string[dtdataset.Rows.Count];
                    DataRow row112;
                    if (Utils.dsRCBDI.Tables.Contains(dtdataset.TableName))
                    {
                        Utils.dsRCBDI.Tables[dtdataset.TableName].Clear();
                    }
                    else
                    {
                        Utils.dsRCBDI.Tables.Add(dtdataset.TableName);
                        Utils.dsRCBDI.Tables[dtdataset.TableName].Columns.Add("ObjectReferrence");
                        Utils.dsRCBDI.Tables[dtdataset.TableName].Columns.Add("Node");
                    }
                    for (int i = 0; i < dtdataset.Rows.Count; i++)
                    {
                        row112 = Utils.dsRCBDI.Tables[dtdataset.TableName].NewRow();
                        Utils.dsRCBDI.Tables[dtdataset.TableName].NewRow();
                        for (int j = 0; j < dtdataset.Columns.Count; j++)
                        {
                            Index112 = dtdataset.Rows[i][j].ToString();
                            row112[j] = Index112.ToString();
                        }
                        Utils.dsRCBDI.Tables[dtdataset.TableName].Rows.Add(row112);
                    }
                    Utils.dsRCBData = Utils.dsRCBDI;
                    Utils.dsRCBData.Merge(Utils.dsRCBAI, false, MissingSchemaAction.Add);
                    Utils.dsRCBData.Merge(Utils.dsRCBAO, false, MissingSchemaAction.Add);
                    Utils.dsRCBData.Merge(Utils.dsRCBDO, false, MissingSchemaAction.Add);
                    Utils.dsRCBData.Merge(Utils.dsRCBEN, false, MissingSchemaAction.Add);
                }
                #endregion Fill Address to Datatable for RCBConfiguration 

                #region ADD
                if (mode == Mode.ADD)
                {
                    //Ajay: 14/07/2018 [#] changes as per diData
                    int intDINO = Convert.ToInt32(diData[16].Value);//DINo
                    int intAutoMapRange = 0; //Ajay: 31/07/2018
                    int intReportingIndex = 0; //Ajay: 31/07/2018
                    int DINo = 0, ReportingIndex = 0;
                    string Description = "";

                    if (IEC61850CheckedList != null)//Namrata:27/03/2019
                    {
                        intCheckItems = IEC61850CheckedList.Count();
                    }

                    #region LoadProfile
                    if (masterType == MasterTypes.LoadProfile) { intAutoMapRange = 1; }
                    else
                    {
                        if (diData[13].Value != "")
                        {
                            intAutoMapRange = Convert.ToInt32(diData[13].Value);//AutoMapRange
                        }

                    }

                    if (masterType != MasterTypes.LoadProfile)
                    {
                        intReportingIndex = Convert.ToInt32(diData[18].Value);//AIIndex 
                    }
                    #endregion LoadProfile

                    #region MaxDI
                    if (intAutoMapRange > getMaxDIs())//Namrata:23/11/2017
                    {
                        MessageBox.Show("Maximum " + getMaxDIs() + " DI's are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    #endregion MaxDI

                    else
                    {
                        #region IEC61850Client
                        if (masterType == MasterTypes.IEC61850Client)
                        {
                            int iListCount = 0;
                            for (int i = intDINO; i <= intDINO + intCheckItems - 1; i++)
                            {
                                DINo = Globals.DINo;
                                DINo += 1;
                                ReportingIndex = intReportingIndex++;
                                Description = ucdi.txtDescription.Text;
                                DI NewDI = new DI("DI", diData, null, masterType, masterNo, IEDNo);
                                NewDI.DINo = DINo.ToString();
                                NewDI.Index = ReportingIndex.ToString();
                                NewDI.Description = Description;
                                NewDI.IEDName = ucdi.cmbIEDName.Text.ToString();
                                NewDI.IEC61850Index = IEC61850CheckedList[iListCount].ToString();
                                NewDI.IEC61850ResponseType = ucdi.cmb61850DIResponseType.Text.ToString();
                                //string FindFC = MergeList.Where(x => x.Contains(IEC61850CheckedList[iListCount].ToString() + ",")).Select(x => x).FirstOrDefault();
                                //string[] GetFC = FindFC.Split(',');
                                //ucdi.cmbFC.SelectedIndex = ucdi.cmbFC.FindStringExact(GetFC[1].ToString());
                                NewDI.FC = ucdi.cmbFC.Text.ToString();
                                diList.Add(NewDI);
                                iListCount++;
                            }
                            Utils.diList1.AddRange(diList);
                        }
                        #endregion IEC61850Client

                        #region OtherMasters
                        else
                        {
                            for (int i = intDINO; i <= intDINO + intAutoMapRange - 1; i++)
                            {
                                DINo = Globals.DINo;
                                DINo += 1;
                                ReportingIndex = intReportingIndex++;
                                #region Description For All Masters
                                if (masterType == MasterTypes.ADR)
                                {
                                    Description = ucdi.txtDescription.Text;
                                }
                                else if (masterType == MasterTypes.IEC101)
                                {
                                    Description = ucdi.txtDescription.Text;
                                }
                                else if (masterType == MasterTypes.SPORT)
                                {
                                    Description = ucdi.txtDescription.Text;
                                }
                                if (masterType == MasterTypes.IEC103)
                                {
                                    Description = ucdi.txtDescription.Text;
                                }
                                if (masterType == MasterTypes.IEC104)
                                {
                                    Description = ucdi.txtDescription.Text;
                                }
                                else if (masterType == MasterTypes.MODBUS)
                                {
                                    Description = ucdi.txtDescription.Text;
                                }
                                else if (masterType == MasterTypes.Virtual)//Namrata: 31/10/2017
                                {
                                    Description = ucdi.txtDescription.Text;
                                }

                                else if (masterType == MasterTypes.LoadProfile)//Ajay: 31/07/2018
                                {
                                    Description = ucdi.txtDescription.Text;
                                }
                                #endregion Description For All Masters
                                DI NewDI = new DI("DI", diData, null, masterType, masterNo, IEDNo);
                                NewDI.DINo = DINo.ToString();
                                NewDI.Index = ReportingIndex.ToString();
                                NewDI.Description = Description;
                                diList.Add(NewDI);
                            }
                        }
                        Utils.diList1.AddRange(diList);
                    }

                    #endregion OtherMasters
                }

                #endregion ADD

                #region EDIT
                else if (mode == Mode.EDIT)
                {
                    if (ucdi.ChkIEC61850Index.CheckBoxItems.Count > 0)
                    {
                        if (ucdi.ChkIEC61850Index.CheckBoxItems.Where(x => x.Checked == true).Count() > 1)
                        {
                            MessageBox.Show("Please select only 1 index.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            diList[editIndex].updateAttributes(diData);
                        }
                    }
                    else
                    {
                        diList[editIndex].updateAttributes(diData);
                    }
                }
                #endregion EDIT

                refreshList();
                if (masterType == MasterTypes.IEC61850Client)//Namrata: 15/03/2018
                {
                    RCBNode = new RCBConfiguration(MasterTypes.IEC61850Client, Convert.ToInt32(Utils.MasterNumForIEC61850Client), Convert.ToInt32(Utils.UnitIDForIEC61850Client));
                    RCBNode.FillRCBList();
                }
                if (sender != null && e != null)
                {
                    ucdi.grpDI.Visible = false;
                    mode = Mode.NONE;
                    editIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            string strRoutineName = "DIConfiguration: btnCancel_Click";
            try
            {
                ucdi.grpDI.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                Utils.resetValues(ucdi.grpDI);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            string strRoutineName = "DIConfiguration: btnFirst_Click";
            try
            {
                if (ucdi.lvDIlist.Items.Count <= 0) return;
                if (diList.ElementAt(0).IsNodeComment) return;
                editIndex = 0;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            string strRoutineName = "DIConfiguration: btnPrev_Click";
            try
            {
                //Namrata: 27/7/2017
                btnDone_Click(null, null);
                if (editIndex - 1 < 0) return;
                if (diList.ElementAt(editIndex - 1).IsNodeComment) return;
                editIndex--;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            string strRoutineName = "DIConfiguration: btnNext_Click";
            try
            {
                //Namrata: 27/7/2017
                btnDone_Click(null, null);
                if (editIndex + 1 >= ucdi.lvDIlist.Items.Count) return;
                if (diList.ElementAt(editIndex + 1).IsNodeComment) return;
                editIndex++;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            string strRoutineName = "DIConfiguration: btnLast_Click";
            try
            {
                if (ucdi.lvDIlist.Items.Count <= 0) return;
                if (diList.ElementAt(diList.Count - 1).IsNodeComment) return;
                editIndex = diList.Count - 1;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strRoutineName = "DIConfiguration: btnDelete_Click";
            try
            {
                // Ajay: 07/12/2018
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    if (AllowMasterOptionsOnClick(masterType)) { return; }
                    else { }
                }

                var LitsItemsChecked = new List<KeyValuePair<int, int>>();
                DialogResult result = MessageBox.Show(Globals.PROMPT_DELETE_ENTRY, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                {
                    return;
                }
                for (int i = ucdi.lvDIlist.Items.Count - 1; i >= 0; i--)
                {
                    if (ucdi.lvDIlist.Items[i].Checked)
                    {
                        if (Utils.IsExistDIinPLC(diList.ElementAt(i).DINo))
                        {
                            DialogResult ask = MessageBox.Show("DI " + diList.ElementAt(i).DINo + " is referred in ParameterLoadConfiguration and all the references will also be deleted." + Environment.NewLine + "Do you want to continue?", "Delete DI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (ask == DialogResult.No)
                            {
                                continue;
                            }
                            Utils.DeleteDIFromPLC(diList.ElementAt(i).DINo);
                        }
                        deleteDIFromMaps(diList.ElementAt(i).DINo);
                        diList.RemoveAt(i);
                        ucdi.lvDIlist.Items[i].Remove();
                    }
                }
                refreshList();
                refreshCurrentMapList();//Refresh map listview...
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void linkDO_Click(object sender, EventArgs e)
        {
            string strRoutineName = "DIConfiguration:linkDO_Click";
            try
            {
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted) // Ajay: 07/12/2018
                {
                    if (AllowMasterOptionsOnClick(masterType)) { return; }
                    else { }
                }
                foreach (ListViewItem listItem in ucdi.lvDIlist.Items)
                {
                    listItem.Checked = true;
                }
                DialogResult result = MessageBox.Show("Do you want to delete all records ? ", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                {
                    foreach (ListViewItem listItem in ucdi.lvDIlist.Items)
                    {
                        listItem.Checked = false;
                    }
                    return;
                }
                for (int i = ucdi.lvDIlist.Items.Count - 1; i >= 0; i--)
                {
                    if (Utils.IsExistDIinPLC(diList.ElementAt(i).DINo))
                    {
                        DialogResult ask = MessageBox.Show("DI " + diList.ElementAt(i).DINo + " is referred in ParameterLoadConfiguration and all the references will also be deleted." + Environment.NewLine + "Do you want to continue?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (ask == DialogResult.No)
                        {
                            continue;
                        }
                        Utils.DeleteDIFromPLC(diList.ElementAt(i).DINo);
                    }
                    deleteDIFromMaps(diList.ElementAt(i).DINo);
                    diList.RemoveAt(i);
                    ucdi.lvDIlist.Items.Clear();
                }
                refreshList();
                refreshCurrentMapList();//Refresh map listview...
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void linkLabel1_Click(object sender, EventArgs e)
        {
            string strRoutineName = "DIConfiguration:linkLabel1_Click";
            try
            {
                List<DIMap> slaveDIMapList;
                if (!slavesDIMapList.TryGetValue(currentSlave, out slaveDIMapList))
                {
                    MessageBox.Show("Error deleting DI map!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                foreach (ListViewItem listItem in ucdi.lvDIMap.Items)
                {
                    listItem.Checked = true;
                }
                DialogResult result = MessageBox.Show("Do You Want To Delete All Records ? ", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                {
                    foreach (ListViewItem listItem in ucdi.lvDIMap.Items)
                    {
                        listItem.Checked = false;
                    }
                    return;
                }
                for (int i = ucdi.lvDIMap.Items.Count - 1; i >= 0; i--)
                {
                    if (GDSlave.DsExcelData.Tables.Count > 0)
                    {
                        //Namrata: 04/02/2019
                        var results = from row in GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable()
                                      where row.Field<string>("CellNo") == slaveDIMapList[i].CellNo && row.Field<string>("Widget") == slaveDIMapList[i].Widget && row.Field<string>("Type") == "DO"
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
                    slaveDIMapList.RemoveAt(i);
                    ucdi.lvDIMap.Items.Clear();
                }
                refreshMapList(slaveDIMapList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvDIlist_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "DIConfiguration: lvDIlist_DoubleClick";
            try
            {
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)// Ajay: 07/12/2018
                {
                    if (AllowMasterOptionsOnClick(masterType)) { return; }
                    else { }
                }

                int ListIndex = ucdi.lvDIlist.FocusedItem.Index;
                ucdi.txtAutoMap.Text = "0";//Namrata: 10/09/2017
                ListViewItem lvi = ucdi.lvDIlist.Items[ListIndex];//Namrata:07/03/2018;

                Utils.UncheckOthers(ucdi.lvDIlist, lvi.Index);
                if (diList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ucdi.txtEvent_F.Text = "0";
                ucdi.txtEvent_T.Text = "0";
                ucdi.grpDI.Visible = true;
                #region Enable/Disable Controls Masterwise
                if (masterType == MasterTypes.IEC101 || masterType == MasterTypes.IEC103 || masterType == MasterTypes.SPORT)
                {
                    IEC101_IEC103_SPORT_OnDoubleClick();
                }
                else if (masterType == MasterTypes.MODBUS)
                {
                    MODBUS_OnDoubleClick();
                }
                else if (masterType == MasterTypes.ADR)
                {
                    //ucdi.ChkEvent.Visible = false; //Ajay: 14/07/2018
                    ADR_OnDoubleClick();
                }
                //Ajay: 14/07/2018
                else if (masterType == MasterTypes.IEC104)
                {
                    IEC104_OnDoubleClick();
                }
                //Ajay: 14/07/2018
                else if (masterType == MasterTypes.IEC61850Client)
                {
                    IEC61850Client_OnDoubleClick();
                    FetchComboboxData();
                    //Namrata: 04/04/2018
                    ucdi.cmbIEDName.SelectedIndex = ucdi.cmbIEDName.FindStringExact(Utils.Iec61850IEDname);
                }
                else if (masterType == MasterTypes.Virtual)
                {
                    ucdi.ChkEvent.Visible = false; //Ajay: 14/07/2018
                    Virtual_OnDoubleClick();
                }
                //Ajay: 31/07/2018
                else if (masterType == MasterTypes.LoadProfile)
                {
                    LoadProfile_OnDoubleClick();
                }
                #endregion Enable/Disable Controls Masterwise
                mode = Mode.EDIT;
                editIndex = lvi.Index;

                //Namrata:25/03/2019
                ucdi.ChkIEC61850Index.Text = "";
                if (IEC61850CheckedList != null)
                {
                    ucdi.ChkIEC61850Index.CheckBoxItems.ForEach(x =>
                    {
                        x.Checked = false; x.CheckState = CheckState.Unchecked;
                    });
                }
                if (ucdi.ChkIEC61850Index.CheckBoxItems.Count > 0)
                {
                    ucdi.ChkIEC61850Index.Text = diList[lvi.Index].IEC61850Index.ToString();
                }
                Utils.showNavigation(ucdi.grpDI, true);
                loadValues();
                if (ucdi.ChkIEC61850Index.CheckBoxItems.Count > 0)
                {
                    //Namrata:26/03/2019
                    ucdi.ChkIEC61850Index.CheckBoxItems.Where(x => x.Text == diList[lvi.Index].IEC61850Index.ToString()).Take(1).ToList().ForEach(x =>
                    {
                        x.Checked = true; x.CheckState = CheckState.Checked;
                    });
                }
                if (ucdi.ChkIEC61850Index.CheckBoxItems.Where(x => x.Checked == true).Count() > 1)
                {
                    MessageBox.Show("Please select only 1 index.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ucdi.cmbResponseType.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvDIlist_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string strRoutineName = "DIConfiguration: lvDIlist_ItemSelectionChanged";
            try
            {
                if (e.IsSelected)
                {
                    Color GreenColour = Color.FromArgb(34, 217, 0);
                    string diIndex = e.Item.Text;

                    //Namrata: 27/7/2017
                    ucdi.lvDIMapItemSelectionChanged -= new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDIMap_ItemSelectionChanged);
                    ucdi.lvDIMap.SelectedItems.Clear();   //Remove selection from DIMap...
                    ucdi.lvDIMap.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window); //Namrata: 07/04/2018
                    ucdi.lvDIMap.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour)); //Namrata: 07/04/2018
                    Utils.highlightListviewItem(diIndex, ucdi.lvDIMap);
                    //Namrata: 27/7/2017
                    ucdi.lvDIMap.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);//Namrata: 07/04/2018
                    ucdi.lvDIlist.SelectedItems.Clear();
                    ucdi.lvDIlist.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                    ucdi.lvDIlist.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                    ucdi.lvDIlist.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);
                    ucdi.lvDIMapItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDIMap_ItemSelectionChanged);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Namrata: 27/7/2017
        private void lvDIMap_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string strRoutineName = "DIConfiguration: lvDIMap_ItemSelectionChanged";
            try
            {
                if (e.IsSelected)
                {
                    Color GreenColour = Color.FromArgb(34, 217, 0);
                    string diIndex = e.Item.Text;
                    Console.WriteLine("*** selected DI: {0}", diIndex);
                    //Namrata: 27/7/2017
                    ucdi.lvDIlistItemSelectionChanged -= new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDIlist_ItemSelectionChanged);
                    //Remove selection from DIMap...
                    ucdi.lvDIlist.SelectedItems.Clear();
                    ucdi.lvDIlist.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window); //Namrata: 07/04/2018
                    ucdi.lvDIlist.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour)); //Namrata: 07/04/2018
                    Utils.highlightListviewMapItem(diIndex, ucdi.lvDIlist);
                    //Namrata: 27/7/2017
                    ucdi.lvDIlist.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);//Namrata: 07/04/2018
                    ucdi.lvDIMap.SelectedItems.Clear();
                    ucdi.lvDIMap.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                    ucdi.lvDIMap.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                    ucdi.lvDIMap.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);
                    ucdi.lvDIlistItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDIlist_ItemSelectionChanged);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbIEDName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "DIConfiguration: cmbIEDName_SelectedIndexChanged";
            try
            {
                //Namrata: 04/04/2018
                if (ucdi.cmbIEDName.Focused == false)
                {

                }
                else
                {
                    Utils.Iec61850IEDname = ucdi.cmbIEDName.Text;
                    List<DataTable> dtList = Utils.dsResponseType.Tables.OfType<DataTable>().Where(tbl => tbl.TableName.StartsWith(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname)).ToList();
                    if (dtList.Count == 0)
                    {
                        ucdi.cmb61850DIResponseType.DataSource = null;
                        ucdi.cmb61850DIIndex.DataSource = null;
                        ucdi.cmb61850DIResponseType.Enabled = false;
                        ucdi.cmb61850DIIndex.Enabled = false;
                        //Ajay: 17/01/2019
                        ucdi.cmbFC.DataSource = null;
                    }
                    else
                    {
                        ucdi.cmb61850DIResponseType.Enabled = true;
                        ucdi.cmb61850DIIndex.Enabled = true;
                        ucdi.cmb61850DIResponseType.DataSource = Utils.dsResponseType.Tables[Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname];//[Utils.strFrmOpenproplusTreeNode + "/" + "Undefined" + "/" + Utils.Iec61850IEDname];
                        ucdi.cmb61850DIResponseType.DisplayMember = "Address";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmb61850DIIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string strRoutineName = "DI : cmb61850DIIndex_SelectedIndexChanged";
            //try
            //{
            //    if (ucdi.cmb61850DIIndex.Items.Count > 0)
            //    {
            //        if (ucdi.cmb61850DIIndex.SelectedIndex != -1)
            //        {
            //            //Ajay: 17/01/2019
            //            //ucdi.txtFC.Text = ((DataRowView)ucdi.cmb61850DIIndex.SelectedItem).Row[2].ToString();

            //            //ucdi.cmbFC.SelectedIndex = ucdi.cmbFC.FindStringExact(((DataRowView)ucdi.cmb61850DIIndex.SelectedItem).Row[2].ToString());
            //            ucdi.cmbFC.SelectedIndex = ucdi.cmbFC.Items.IndexOf(((DataRowView)ucdi.cmb61850DIIndex.SelectedItem).Row[2].ToString());
            //            cmbFC_SelectedIndexChanged(ucdi.cmbFC, null);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            string strRoutineName = "AI : cmb61850Index_SelectedIndexChanged";
            try
            {
                if (ucdi.cmb61850DIIndex.Items.Count > 0)
                {
                    if (ucdi.cmb61850DIIndex.SelectedIndex != -1)
                    {
                        //Ajay: 17/01/2019
                        //ucai.txtFC.Text = ((DataRowView)ucai.cmb61850Index.SelectedItem).Row[2].ToString(); // ucai.cmb61850Index.Items.OfType<DataRowView>().Select(x => x.Row[2].ToString()).FirstOrDefault().ToString();
                        ucdi.cmbFC.SelectedIndex = ucdi.cmbFC.Items.IndexOf(((DataRowView)ucdi.cmb61850DIIndex.SelectedItem).Row[2].ToString());
                        //ucai.cmbFC.Text = ((DataRowView)ucai.cmb61850Index.SelectedItem).Row[2].ToString();
                        Utils.IEC61850Index = (((DataRowView)ucdi.cmb61850DIIndex.SelectedItem).Row[0].ToString());
                        //cmbFC_SelectedIndexChanged(ucai.cmbFC, null);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmb61850DIResponseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "DIConfiguration: cmb61850ResponseType_SelectedIndexChanged";
            try
            {
                if (ucdi.cmb61850DIResponseType.Items.Count > 1)
                {
                    if ((ucdi.cmb61850DIResponseType.SelectedIndex != -1))
                    {
                        Utils.Iec61850IEDname = ucdi.cmbIEDName.Text;//Namrata: 04/04/2018
                        List<DataTable> dtList = Utils.DsAllConfigurationData.Tables.OfType<DataTable>().Where(tbl => tbl.TableName.StartsWith(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname)).ToList();
                        dsdummy = new DataSet();
                        dtList.ForEach(tbl => { DataTable dt = tbl.Copy(); dsdummy.Tables.Add(dt); });
                        ucdi.cmbFC.DataSource = dsdummy.Tables[ucdi.cmb61850DIResponseType.SelectedIndex].AsEnumerable().Select(x => x.Field<string>("FC")).Distinct().ToList();//Ajay: 17/01/2019
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
            string strRoutineName = "DIConfiguration: ChkIEC61850Index_SelectedIndexChanged";
            try
            {
                if (ucdi.ChkIEC61850Index.Items.Count > 0)
                {
                    if (ucdi.ChkIEC61850Index.SelectedIndex != -1)
                    {
                        string a = ucdi.ChkIEC61850Index.Text;
                        //string FindObjRef = MergeList.Where(x => x.Contains(a.ToString() + ",")).Select(x => x).FirstOrDefault();
                        //string[] GetFC = FindObjRef.Split(',');
                        //ucdi.cmbFC.SelectedIndex = ucdi.cmbFC.FindStringExact(GetFC[1].ToString());
                        //Utils.IEC61850Index = GetFC[0].ToString();
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
            string strRoutineName = "DIConfiguration: cmbFC_SelectedIndexChanged";
            try
            {

                string FC = ucdi.cmbFC.Text;
                DataTable DT = FilteredIndexDT(FC);
                if (DT != null)
                {
                    //Namrata:25/03/2019
                    List<string> FC1 = DT.AsEnumerable().Select(x => x[0].ToString()).ToList();
                    ucdi.ChkIEC61850Index.Items.Clear();
                    ucdi.ChkIEC61850Index.Text = ""; //Namrata:25/03/2019
                    foreach (var kv in FC1)
                    {
                        ucdi.ChkIEC61850Index.Items.Add(kv.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 31/07/2018
        private void btnVerify_Click(object sender, EventArgs e)
        {
            string strRoutineName = "DIConfiguration: btnVerify_Click";
            try
            {
                string[] str = new string[2];
                if (!IsVerified(ucdi.cmbDI1, "DI1", out str))
                {
                    string dlgMsg = str[0];
                    if (!string.IsNullOrEmpty(str[1])) { dlgMsg += "=" + str[1]; }
                    dlgMsg += " is not valid.";
                    MessageBox.Show(dlgMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("DI1 is valid.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex) { MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        //Ajay: 31/07/2018
        //private bool IsVerified(out string[] str)
        //{
        //    str = new string[2];
        //    string strRoutineName = "IsVerified()";
        //    bool IsValid = false;
        //    try
        //    {
        //        int iDI1 = 0;
        //        int.TryParse(ucdi.cmbDI1.Text, out iDI1);
        //        if (iDI1 > 0)
        //        {
        //            if (!Utils.GetAllDIList().Contains(iDI1)) { str[0] = "DI1"; str[1] = iDI1.ToString(); return false; }
        //            else { IsValid = true; }
        //        }
        //        else
        //        {
        //            str[0] = "DI1"; str[1] = "";
        //            return false;
        //        }
        //        return IsValid;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }
        //}
        private bool IsVerified(ComboBox cmb, string param, out string[] str)
        {
            str = new string[2];
            string strRoutineName = "IsVerified()";
            try
            {
                int iParam = 0;
                int.TryParse(cmb.Text, out iParam);
                if (!Utils.GetAllDIList().Contains(iParam))
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
        //Namrata:22/02/2019
        private DataTable FilteredIndexDT(string FC)
        {
            string strRoutineName = "DIConfiguration: FilteredIndexDT";
            try
            {
                DataTable DT = new DataTable();
                DT.Columns.Add("ObjectReferrence");
                DT.Columns.Add("Node");
                DT.Columns.Add("FC");
                DataRow[] drRwArry = dsdummy.Tables[ucdi.cmb61850DIResponseType.SelectedIndex].AsEnumerable().Where(x => x.Field<string>("FC") == FC).ToArray();
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
        private string getDescription(ListView lstview, string ainno)
        {
            int iColIndex = ucdi.lvDIlist.Columns["Description"].Index;
            var query = lstview.Items
                    .Cast<ListViewItem>()
                    .Where(item => item.SubItems[0].Text == ainno).Select(s => s.SubItems[iColIndex].Text).Single();
            return query.ToString();
        }
        public void FetchCheckboxData()
        {
            string strRoutineName = "DIConfiguration: FetchCheckboxData";
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
                    ucdi.ChkIEC61850Index.Items.Clear();
                    foreach (var kv in ObjectRef)
                    {
                        ucdi.ChkIEC61850Index.Items.Add(kv.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void FetchComboboxData()
        {
            string strRoutineName = "DIConfiguration: FetchComboboxData";
            try
            {
                ucdi.cmbIEDName.DataSource = null;  //Namrata: 13/03/2018
                List<string> tblNameList = Utils.dsIED.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                string tblName = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
                if (tblName != null)//Namrata: 26/04/2018
                {
                    ucdi.cmbIEDName.DataSource = Utils.dsIED.Tables[tblName];
                    ucdi.cmbIEDName.DisplayMember = "IEDName";
                    ucdi.cmb61850DIResponseType.DataSource = Utils.dsResponseType.Tables[tblName];//Namrata: 21/03/2018
                    ucdi.cmb61850DIResponseType.DisplayMember = "Address";
                    FetchCheckboxData(); //Namrata: 27/03/2019
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void loadDefaults()
        {
            string strRoutineName = "DIConfiguration: loadDefaults";
            try
            {
                ucdi.txtDINo.Text = (Globals.DINo + 1).ToString();
                ucdi.txtIndex.Text = "5";
                ucdi.txtSubIndex.Text = "1";
                ucdi.txtEvent_T.Text = "257";
                ucdi.txtEvent_F.Text = "258";
                if (masterType == MasterTypes.ADR)
                {
                    if (ucdi.lvDIlist.Items.Count - 1 >= 0)
                    {
                        ucdi.txtIndex.Text = Convert.ToString(Convert.ToInt32(diList[diList.Count - 1].Index) + 1);
                    }
                    ucdi.txtAutoMap.Text = "1";
                    ucdi.cmbResponseType.SelectedIndex = ucdi.cmbResponseType.FindStringExact("ADR_DI");
                    ucdi.txtDescription.Text = "ADR_DI";// + (Globals.DINo + 1).ToString();
                }
                else if (masterType == MasterTypes.IEC101)
                {
                    if (ucdi.lvDIlist.Items.Count - 1 >= 0)
                    {
                        ucdi.txtIndex.Text = Convert.ToString(Convert.ToInt32(diList[diList.Count - 1].Index) + 1);
                    }
                    ucdi.txtAutoMap.Text = "1";
                    ucdi.cmbResponseType.SelectedIndex = ucdi.cmbResponseType.FindStringExact("DoublePoint");
                    ucdi.txtDescription.Text = "IEC101_DI";// + (Globals.DINo + 1).ToString();
                }
                else if (masterType == MasterTypes.IEC104)
                {
                    if (ucdi.lvDIlist.Items.Count - 1 >= 0)
                    {
                        ucdi.txtIndex.Text = Convert.ToString(Convert.ToInt32(diList[diList.Count - 1].Index) + 1);
                    }
                    ucdi.txtAutoMap.Text = "1";
                    ucdi.cmbResponseType.SelectedIndex = ucdi.cmbResponseType.FindStringExact("DoublePoint");
                    ucdi.txtDescription.Text = "IEC104_DI";// + (Globals.DINo + 1).ToString();
                }
                else if (masterType == MasterTypes.SPORT)
                {
                    if (ucdi.lvDIlist.Items.Count - 1 >= 0)
                    {
                        ucdi.txtIndex.Text = Convert.ToString(Convert.ToInt32(diList[diList.Count - 1].Index) + 1);
                    }
                    //Ajay: 12/11/2018
                    if (ucdi.cmbResponseType != null && ucdi.cmbResponseType.Items.Count > 0)
                    {
                        ucdi.cmbResponseType.SelectedIndex = 0; // ucdi.cmbResponseType.FindStringExact("DoublePoint");
                    }
                    ucdi.txtDescription.Text = "SPORT_DI";// + (Globals.DINo + 1).ToString();
                    ucdi.txtAutoMap.Text = "1";
                }
                if (masterType == MasterTypes.IEC103)
                {
                    if (ucdi.lvDIlist.Items.Count - 1 >= 0)
                    {
                        ucdi.txtIndex.Text = Convert.ToString(Convert.ToInt32(diList[diList.Count - 1].Index) + 1);
                    }
                    ucdi.cmbResponseType.SelectedIndex = ucdi.cmbResponseType.FindStringExact("TimeTaggedMessage");
                    ucdi.txtDescription.Text = "IEC103_DI";// + (Globals.DINo + 1).ToString();
                    ucdi.txtAutoMap.Text = "1";
                }
                else if (masterType == MasterTypes.MODBUS)
                {
                    if (ucdi.lvDIlist.Items.Count - 1 >= 0)
                    {
                        ucdi.txtIndex.Text = Convert.ToString(Convert.ToInt32(diList[diList.Count - 1].Index) + 1);
                    }
                    ucdi.cmbResponseType.SelectedIndex = ucdi.cmbResponseType.FindStringExact("ReadCoil");
                    ucdi.txtDescription.Text = "MODBUS_DI";// + (Globals.DINo + 1).ToString();
                    //Ajay: 12/11/2018
                    if (ucdi.CmbDataType != null && ucdi.CmbDataType.Items.Count > 0)
                    {
                        ucdi.CmbDataType.SelectedIndex = 0; // ucdi.cmbResponseType.FindStringExact("DoublePoint");
                    }
                    ucdi.txtAutoMap.Text = "1";
                }
                else if (masterType == MasterTypes.IEC61850Client)
                {
                    if (ucdi.lvDIlist.Items.Count - 1 >= 0)
                    {
                        ucdi.txtIndex.Text = Convert.ToString(Convert.ToInt32(diList[diList.Count - 1].Index) + 1);
                    }
                    ucdi.cmbResponseType.SelectedIndex = ucdi.cmbResponseType.FindStringExact("ReadCoil");
                    ucdi.txtDescription.Text = "IEC61850_DI";// + (Globals.DINo + 1).ToString();
                    ucdi.txtAutoMap.Text = "";
                }
                else if (masterType == MasterTypes.Virtual)
                {
                    if (ucdi.lvDIlist.Items.Count - 1 >= 0)
                    {
                        ucdi.txtIndex.Text = Convert.ToString(Convert.ToInt32(diList[diList.Count - 1].Index) + 1);
                    }
                    ucdi.cmbResponseType.SelectedIndex = ucdi.cmbResponseType.FindStringExact("DeviceMode");
                    ucdi.txtDescription.Text = "Virtual_DI";// + (Globals.DINo + 1).ToString();
                    ucdi.txtAutoMap.Text = "1";
                }
                //Ajay: 31/07/2018
                else if (masterType == MasterTypes.LoadProfile)
                {
                    if (ucdi.lvDIlist.Items.Count - 1 >= 0)
                    {
                        ucdi.txtIndex.Text = Convert.ToString(Convert.ToInt32(diList[diList.Count - 1].Index) + 1);
                    }
                    if (ucdi.cmbName.Items.Count > 0) { ucdi.cmbName.SelectedIndex = 0; }
                    if (ucdi.cmbDI1.Items.Count > 0) { ucdi.cmbDI1.SelectedIndex = 0; }
                    ucdi.txtConstant.Text = "4.8";
                    ucdi.txtDescription.Text = "LoadProfile_DI";
                    ucdi.chkbxLogEnable.Checked = false;
                    ucdi.txtAutoMap.Text = "1";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Namrata:15/6/2017
        public bool UpdateDI(string responseType, int Idx, int SubOldIdx, int SubNewIdx, List<KeyValuePair<string, string>> diData)
        {
            bool Updated = false;
            for (int i = 0; i < diList.Count; i++)
            {
                if (diList[i].IsNodeComment) continue;
                DI tmp = diList[i];
                if (tmp.Index == Idx.ToString() && tmp.SubIndex == SubOldIdx.ToString() && tmp.ResponseType == responseType.ToString())
                {
                    diList[i].updateAttributes(diData);
                    Updated = true;
                    break;
                }
            }
            return Updated;
        }

        //Namrata: 27/09/2017
        //Remove Entry From Virtual DI when Active is "NO".
        public bool removeDINetwork(string responseType, int Idx, int SubOldIdx, int SubNewIdx, List<KeyValuePair<string, string>> diData)
        {
            bool removed = false;
            for (int i = 0; i < diList.Count; i++)
            {
                if (diList[i].IsNodeComment) continue;
                DI tmp = diList[i];
                if (tmp.Index == Idx.ToString() && tmp.SubIndex == SubOldIdx.ToString() && tmp.ResponseType == responseType)
                {
                    diList.RemoveAt(i);
                    removed = true;
                    break;
                }
            }
            return removed;
        }
        private void loadValues()
        {
            string strRoutineName = "lDIConfiguration: oadValues";
            try
            {
                DI di = diList.ElementAt(editIndex);
                if (di != null)
                {
                    ucdi.txtDINo.Text = di.DINo;
                    ucdi.cmbResponseType.SelectedIndex = ucdi.cmbResponseType.FindStringExact(di.ResponseType);
                    ucdi.txtIndex.Text = di.Index;
                    ucdi.txtSubIndex.Text = di.SubIndex;
                    ucdi.txtDescription.Text = di.Description;
                    ucdi.txtEvent_T.Text = di.Event_T;
                    ucdi.txtEvent_F.Text = di.Event_F;
                    ucdi.cmbIEDName.SelectedIndex = ucdi.cmbIEDName.FindStringExact(Utils.Iec61850IEDname);// (di.IEDName);
                    ucdi.cmb61850DIResponseType.SelectedIndex = ucdi.cmb61850DIResponseType.FindStringExact(di.IEC61850ResponseType);
                    ucdi.cmb61850DIIndex.SelectedIndex = ucdi.cmb61850DIIndex.FindStringExact(di.IEC61850Index);
                    ucdi.CmbDataType.SelectedIndex = ucdi.CmbDataType.FindStringExact(di.DataType);//Namrata: 17/10/2017
                    ucdi.cmbFC.SelectedIndex = ucdi.cmbFC.FindStringExact(di.FC); //Ajay: 17/01/2019
                    ucdi.ChkIEC61850Index.SelectedIndex = ucdi.ChkIEC61850Index.FindStringExact(di.IEC61850Index);//Namrata:27/03/2019
                    if (di.Event.ToLower() == "yes")//Ajay: 18/07/2018
                    { ucdi.ChkEvent.Checked = true; }
                    else { ucdi.ChkEvent.Checked = false; }
                    ucdi.cmbName.SelectedIndex = ucdi.cmbName.FindStringExact(di.Name);//Ajay: 31/07/2018;
                    ucdi.cmbDI1.SelectedIndex = ucdi.cmbDI1.FindStringExact(di.DI1);
                    ucdi.txtConstant.Text = di.Constant;
                    ucdi.cmbFC.SelectedIndex = ucdi.cmbFC.FindStringExact(di.FC); //Ajay: 17/01/2019
                    ucdi.ChkIEC61850Index.SelectedIndex = ucdi.ChkIEC61850Index.FindStringExact(di.IEC61850Index);//Namrata:27/03/2019
                    if (di.LogEnable.ToLower() == "yes")
                    { ucdi.chkbxLogEnable.Checked = true; }
                    else { ucdi.chkbxLogEnable.Checked = false; }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool Validate()
        {
            bool status = true;
            //Check empty field's
            if (Utils.IsEmptyFields(ucdi.grpDI))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return status;
        }
        private int getMaxDIs()
        {
            if (masterType == MasterTypes.IEC103) return Globals.MaxIEC103DI;
            else if (masterType == MasterTypes.MODBUS) return Globals.MaxMODBUSDI;
            //Namrata:13/7/2017
            else if (masterType == MasterTypes.ADR) return Globals.MaxADRDI;
            else if (masterType == MasterTypes.IEC101) return Globals.MaxIEC101DI;
            else if (masterType == MasterTypes.IEC61850Client) return Globals.MaxIEC61850DI;
            //Namrata: 26/10/2017
            else if (masterType == MasterTypes.Virtual) return Globals.MaxPLUDI;
            else if (masterType == MasterTypes.IEC104) return Globals.MaxIEC104MasterDI;
            else if (masterType == MasterTypes.SPORT) return Globals.MaxSPORTMasterDI;
            else if (masterType == MasterTypes.LoadProfile) return Globals.MaxLoadProfileDI; //Ajay: 31/07/2018
            else return 0;
        }
        public void refreshList()
        {
            string strRoutineName = "DIConfiguration: refreshList";
            try
            {
                int cnt = 0;
                ucdi.lvDIlist.Items.Clear();
                Utils.DIlistforDescription.Clear();

                if (ucdi.lvDIlist.InvokeRequired)
                {
                    ucdi.lvDIlist.Invoke(new MethodInvoker(delegate
                    {
                        #region VirtualMaster
                        //Namrata: 25/10/2017
                        if (masterType == MasterTypes.Virtual)
                        {
                            Utils.DummyDI.Clear();
                            foreach (DI di in diList)
                            {
                                string[] row = new string[5];
                                if (di.IsNodeComment)
                                {
                                    row[0] = "Comment...";
                                }
                                else
                                {
                                    row[0] = di.DINo;
                                    row[1] = di.ResponseType;
                                    row[2] = di.Index;
                                    row[3] = di.SubIndex;
                                    row[4] = di.Description;
                                }
                                ListViewItem lvItem = new ListViewItem(row);
                                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                                ucdi.lvDIlist.Items.Add(lvItem);
                            }
                        }
                        #endregion VirtualMaster

                        #region MODBUS
                        if (masterType == MasterTypes.MODBUS)
                        {
                            foreach (DI di in diList)
                            {
                                string[] row = new string[9];
                                if (di.IsNodeComment)
                                {
                                    row[0] = "Comment...";
                                }
                                else
                                {
                                    row[0] = di.DINo;
                                    row[1] = di.ResponseType;
                                    row[2] = di.Index;
                                    row[3] = di.SubIndex;
                                    row[4] = di.DataType;
                                    row[5] = di.Description;
                                }
                                ListViewItem lvItem = new ListViewItem(row);
                                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                                ucdi.lvDIlist.Items.Add(lvItem);
                            }
                        }
                        #endregion MODBUS

                        #region ADRMaster
                        if (masterType == MasterTypes.ADR)
                        {
                            foreach (DI di in diList)
                            {
                                string[] row = new string[8];
                                if (di.IsNodeComment)
                                {
                                    row[0] = "Comment...";
                                }
                                else
                                {
                                    row[0] = di.DINo;
                                    row[1] = di.ResponseType;
                                    row[2] = di.Index;
                                    row[3] = di.SubIndex;
                                    row[4] = di.Event_T;
                                    row[5] = di.Event_F;
                                    row[6] = di.Description;
                                }
                                ListViewItem lvItem = new ListViewItem(row);
                                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                                ucdi.lvDIlist.Items.Add(lvItem);
                            }
                        }
                        #endregion ADRMaster

                        //Ajay: 14/07/2018
                        #region IEC104Master
                        if ((masterType == MasterTypes.IEC104))
                        {
                            foreach (DI di in diList)
                            {
                                string[] row = new string[5];
                                if (di.IsNodeComment)
                                {
                                    row[0] = "Comment...";
                                }
                                else
                                {
                                    row[0] = di.DINo;
                                    row[1] = di.ResponseType;
                                    row[2] = di.Index;
                                    row[3] = di.SubIndex;
                                    //row[4] = di.Event; //Ajay: 12/11/2018 Event Removed
                                    row[4] = di.Description;
                                }
                                ListViewItem lvItem = new ListViewItem(row);
                                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                                ucdi.lvDIlist.Items.Add(lvItem);
                            }
                        }
                        #endregion
                        //Ajay: 31/07/2018
                        #region Load Profile
                        if ((masterType == MasterTypes.LoadProfile))
                        {
                            foreach (DI di in diList)
                            {
                                string[] row = new string[5];
                                if (di.IsNodeComment)
                                {
                                    row[0] = "Comment...";
                                }
                                else
                                {
                                    row[0] = di.DINo;
                                    row[1] = di.Name;
                                    row[2] = di.DI1;
                                    row[3] = di.Constant;
                                    //row[4] = di.LogEnable; //Ajay: "LogEnable" removed
                                    row[4] = di.Description;
                                }
                                ListViewItem lvItem = new ListViewItem(row);
                                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                                ucdi.lvDIlist.Items.Add(lvItem);
                            }
                        }
                        #endregion

                        #region IEC103master,IEC101master,SPORTMaster
                        //Ajay: 18/07/2018 Condition handeled separetely for IEC104Master
                        //if ((masterType == MasterTypes.IEC103) || (masterType == MasterTypes.IEC101) || (masterType == MasterTypes.IEC104) || (masterType == MasterTypes.SPORT))
                        if ((masterType == MasterTypes.IEC103) || (masterType == MasterTypes.IEC101) || (masterType == MasterTypes.SPORT))
                        {
                            foreach (DI di in diList)
                            {
                                string[] row = new string[6];
                                if (di.IsNodeComment)
                                {
                                    row[0] = "Comment...";
                                }
                                else
                                {
                                    row[0] = di.DINo;
                                    row[1] = di.ResponseType;
                                    row[2] = di.Index;
                                    row[3] = di.SubIndex;
                                    row[4] = di.Description;
                                }
                                ListViewItem lvItem = new ListViewItem(row);
                                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                                ucdi.lvDIlist.Items.Add(lvItem);
                            }
                        }
                        #endregion IEC103master,IEC101master,ADRMaster

                        #region IEC61850Client
                        if (masterType == MasterTypes.IEC61850Client)
                        {
                            foreach (DI di in diList)
                            {
                                string[] row = new string[8];
                                if (di.IsNodeComment)
                                {
                                    row[0] = "Comment...";
                                }
                                else
                                {
                                    row[0] = di.DINo;
                                    row[1] = di.IEDName;
                                    row[2] = di.IEC61850ResponseType;
                                    row[3] = di.IEC61850Index;
                                    row[4] = di.FC;
                                    row[5] = di.SubIndex;
                                    row[6] = di.Description;
                                }
                                ListViewItem lvItem = new ListViewItem(row);
                                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                                ucdi.lvDIlist.Items.Add(lvItem);
                            }
                        }
                        #endregion IEC61850Client

                    }));
                }
                else
                {
                    #region VirtualMaster
                    //Namrata: 25/10/2017
                    if (masterType == MasterTypes.Virtual)
                    {
                        Utils.DummyDI.Clear();
                        foreach (DI di in diList)
                        {
                            string[] row = new string[5];
                            if (di.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = di.DINo;
                                row[1] = di.ResponseType;
                                row[2] = di.Index;
                                row[3] = di.SubIndex;
                                row[4] = di.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucdi.lvDIlist.Items.Add(lvItem);
                        }
                    }
                    #endregion VirtualMaster

                    #region MODBUS
                    if (masterType == MasterTypes.MODBUS)
                    {
                        foreach (DI di in diList)
                        {
                            string[] row = new string[9];
                            if (di.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = di.DINo;
                                row[1] = di.ResponseType;
                                row[2] = di.Index;
                                row[3] = di.SubIndex;
                                row[4] = di.DataType;
                                row[5] = di.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucdi.lvDIlist.Items.Add(lvItem);
                        }
                    }
                    #endregion MODBUS

                    #region ADRMaster
                    if (masterType == MasterTypes.ADR)
                    {
                        foreach (DI di in diList)
                        {
                            string[] row = new string[8];
                            if (di.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = di.DINo;
                                row[1] = di.ResponseType;
                                row[2] = di.Index;
                                row[3] = di.SubIndex;
                                row[4] = di.Event_T;
                                row[5] = di.Event_F;
                                row[6] = di.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucdi.lvDIlist.Items.Add(lvItem);
                        }
                    }
                    #endregion ADRMaster

                    //Ajay: 14/07/2018
                    #region IEC104Master
                    if ((masterType == MasterTypes.IEC104))
                    {
                        foreach (DI di in diList)
                        {
                            string[] row = new string[5];
                            if (di.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = di.DINo;
                                row[1] = di.ResponseType;
                                row[2] = di.Index;
                                row[3] = di.SubIndex;
                                //row[4] = di.Event; //Ajay: 12/11/2018 Event Removed
                                row[4] = di.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucdi.lvDIlist.Items.Add(lvItem);
                        }
                    }
                    #endregion
                    //Ajay: 31/07/2018
                    #region Load Profile
                    if ((masterType == MasterTypes.LoadProfile))
                    {
                        foreach (DI di in diList)
                        {
                            string[] row = new string[5];
                            if (di.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = di.DINo;
                                row[1] = di.Name;
                                row[2] = di.DI1;
                                row[3] = di.Constant;
                                //row[4] = di.LogEnable; //Ajay: "LogEnable" removed
                                row[4] = di.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucdi.lvDIlist.Items.Add(lvItem);
                        }
                    }
                    #endregion

                    #region IEC103master,IEC101master,SPORTMaster
                    //Ajay: 18/07/2018 Condition handeled separetely for IEC104Master
                    //if ((masterType == MasterTypes.IEC103) || (masterType == MasterTypes.IEC101) || (masterType == MasterTypes.IEC104) || (masterType == MasterTypes.SPORT))
                    if ((masterType == MasterTypes.IEC103) || (masterType == MasterTypes.IEC101) || (masterType == MasterTypes.SPORT))
                    {
                        foreach (DI di in diList)
                        {
                            string[] row = new string[6];
                            if (di.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = di.DINo;
                                row[1] = di.ResponseType;
                                row[2] = di.Index;
                                row[3] = di.SubIndex;
                                row[4] = di.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucdi.lvDIlist.Items.Add(lvItem);
                        }
                    }
                    #endregion IEC103master,IEC101master,ADRMaster

                    #region IEC61850Client
                    if (masterType == MasterTypes.IEC61850Client)
                    {
                        foreach (DI di in diList)
                        {
                            string[] row = new string[8];
                            if (di.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = di.DINo;
                                row[1] = di.IEDName;
                                row[2] = di.IEC61850ResponseType;
                                row[3] = di.IEC61850Index;
                                row[4] = di.FC;
                                row[5] = di.SubIndex;
                                row[6] = di.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucdi.lvDIlist.Items.Add(lvItem);
                        }
                    }
                    #endregion IEC61850Client

                }
                if (ucdi.lblDIRecords.InvokeRequired)
                {
                    ucdi.lblDIRecords.Invoke(new MethodInvoker(delegate
                    {
                        ucdi.lblDIRecords.Text = diList.Count.ToString();
                    }));
                }
                else
                {
                    ucdi.lblDIRecords.Text = diList.Count.ToString();
                }


                  
                Utils.DilistRegenerateIndex.AddRange(diList); //For DI Reindex
                Utils.DummyDI.AddRange(diList); //For VirtualDI AutomaticEnteries
                Utils.EnableDI.AddRange(diList); //Validate DI exist in DOConfiguration
                Utils.DIlistforDescription.AddRange(diList); //For Description not present in XML 
                Utils.DiListForVirualDIImport.AddRange(diList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /* ============================================= Below this, DI Map logic... ============================================= */
        private void CreateNewSlave(string slaveNum, string slaveID, XmlNode dimNode)
        {
            string strRoutineName = "DIConfiguration: CreateNewSlave";
            try
            {
                List<DIMap> sdimList = new List<DIMap>();
                slavesDIMapList.Add(slaveID, sdimList);
                if (dimNode != null)
                {
                    foreach (XmlNode node in dimNode)
                    {
                        if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                        sdimList.Add(new DIMap(node, Utils.getSlaveTypes(slaveID)));
                    }
                }
                AddMap2SlaveButton(Int32.Parse(slaveNum), slaveID);

                //Namrata: 24/02/2018
                //If Description attribute not exist in XML 
                sdimList.AsEnumerable().ToList().ForEach(item =>
                {
                    string strDONo = item.DINo;
                    if (string.IsNullOrEmpty(item.Description)) //Ajay: 05/01/2018 
                    {
                        item.Description = Utils.DIlistforDescription.AsEnumerable().Where(x => x.DINo == strDONo).Select(x => x.Description).FirstOrDefault();
                    }
                });
                GDSlave.CurrentSlave = "GraphicalDisplaySlave_" + slaveNum + "_" + GDSlave.XLSFileName; //GraphicalDisplaySlave_1_5x8 SingleBUS_1.txt
                currentSlave = slaveID;
                refreshMapList(sdimList); 
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DeleteSlave(string slaveID)
        {
            string strRoutineName = "DIConfiguration: DeleteSlave";
            try
            {
                slavesDIMapList.Remove(slaveID);
                RadioButton rb = null;
                foreach (Control ctrl in ucdi.flpMap2Slave.Controls)
                {
                    if (ctrl.Tag.ToString() == slaveID)
                    {
                        rb = (RadioButton)ctrl;
                        break;
                    }
                }
                if (rb != null) ucdi.flpMap2Slave.Controls.Remove(rb);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CheckIEC104SlaveStatusChanges()
        {
            string strRoutineName = "DIConfiguration: CheckIEC104SlaveStatusChanges";
            try
            {
                //Check for slave addition...
                foreach (IEC104Slave slv104 in Utils.getOpenProPlusHandle().getSlaveConfiguration().getIEC104Group().getIEC104Slaves())//Loop thru slaves...
                {
                    string slaveID = "IEC104_" + slv104.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<DIMap>> sn in slavesDIMapList)
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
                foreach (KeyValuePair<string, List<DIMap>> sdin in slavesDIMapList)//Loop thru slaves...
                {
                    string slaveID = sdin.Key;
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
                    if (ucdi.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucdi.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucdi.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucdi.lvDIMap.Items.Clear();
                        currentSlave = "";
                    }
                }
                fillMapOptions(Utils.getSlaveTypes(currentSlave));
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckDNPSlaveStatusChanges()
        {
            string strRoutineName = "DIConfiguration:CheckDNPSlaveStatusChanges";
            try
            {
                //Check for slave addition...
                foreach (DNP3Settings dnp3 in Utils.getOpenProPlusHandle().getSlaveConfiguration().getDNPSlaveGroup().getDNPSlaves())//Loop thru slaves...
                {
                    string slaveID = "DNP3Slave_" + dnp3.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<DIMap>> sn in slavesDIMapList)
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
                foreach (KeyValuePair<string, List<DIMap>> sn in slavesDIMapList)
                {
                    string slaveID = sn.Key;
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
                    if (ucdi.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucdi.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucdi.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucdi.lvDIMap.Items.Clear();
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
            string strRoutineName = "DIConfiguration: CheckGDisplaySlaveStatusChanges";
            try
            {
                //Check for slave addition...
                foreach (GraphicalDisplaySlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().getGraphicalDisplaySlaveGroup().getGDisplaySlaves())
                {
                    string slaveID = "GraphicalDisplaySlave_" + slvMB.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<DIMap>> sdin in slavesDIMapList)
                    {
                        if (sdin.Key == slaveID)
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
                foreach (KeyValuePair<string, List<DIMap>> sdin in slavesDIMapList)
                {
                    string slaveID = sdin.Key;
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
                    if (ucdi.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucdi.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucdi.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucdi.lvDIMap.Items.Clear();
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
        //Namrata:13/7/2017
        private void CheckIEC101SlaveStatusChanges()
        {
            string strRoutineName = "DIConfiguration: CheckIEC101SlaveStatusChanges";
            try
            {
                //Check for slave addition...
                foreach (IEC101Slave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().getIEC101SlaveGroup().getIEC101Slaves())
                {
                    string slaveID = "IEC101Slave_" + slvMB.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<DIMap>> sn in slavesDIMapList)
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
                foreach (KeyValuePair<string, List<DIMap>> sain in slavesDIMapList)//Loop thru slaves...
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
                    if (ucdi.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucdi.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucdi.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucdi.lvDIMap.Items.Clear();
                        currentSlave = "";
                    }
                }
                fillMapOptions(Utils.getSlaveTypes(currentSlave));
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CheckMODBUSSlaveStatusChanges()
        {
            string strRoutineName = "DIConfiguration: CheckMODBUSSlaveStatusChanges";
            try
            {
                //Check for slave addition...
                foreach (MODBUSSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().getMODBUSSlaveGroup().getMODBUSSlaves())//Loop thru slaves...
                {
                    string slaveID = "MODBUSSlave_" + slvMB.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<DIMap>> sn in slavesDIMapList)
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
                foreach (KeyValuePair<string, List<DIMap>> sdin in slavesDIMapList)//Loop thru slaves...
                {
                    string slaveID = sdin.Key;
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
                    if (ucdi.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucdi.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucdi.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucdi.lvDIMap.Items.Clear();
                        currentSlave = "";
                    }
                }
                fillMapOptions(Utils.getSlaveTypes(currentSlave));
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Namrata: 30/05/2019
        private void CheckSMSSlaveStatusChanges()
        {
            string strRoutineName = "DIConfiguration: CheckSMSSlaveStatusChanges";
            try
            {

                //Check for slave addition...
                foreach (SMSSlave slvSMS in Utils.getOpenProPlusHandle().getSlaveConfiguration().getSMSSlaveGroup().getSMSSlaves())
                {
                    string slaveID = "SMSSlave_" + slvSMS.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<DIMap>> sn in slavesDIMapList)
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
                foreach (KeyValuePair<string, List<DIMap>> sain in slavesDIMapList)//Loop thru slaves...
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
                    if (ucdi.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucdi.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucdi.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucdi.lvDIMap.Items.Clear();
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

        //Namrata:02/04/2019
        private void CheckMQTTSlaveStatusChanges()
        {
            string strRoutineName = "DIConfiguration:CheckMQTTSlaveStatusChanges";
            try
            {

                //Check for slave addition...
                foreach (MQTTSlave slvMQTT in Utils.getOpenProPlusHandle().getSlaveConfiguration().getMQTTSlaveGroup().getMQTTSlaves())
                {
                    string slaveID = "MQTTSlave_" + slvMQTT.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<DIMap>> sn in slavesDIMapList)
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
                foreach (KeyValuePair<string, List<DIMap>> sain in slavesDIMapList)//Loop thru slaves...
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
                    if (ucdi.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucdi.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucdi.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucdi.lvDIMap.Items.Clear();
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
        private void CheckIEc61850SlaveStatusChanges()
        {
            string strRoutineName = "DIConfiguration: CheckIEc61850SlaveStatusChanges";
            try
            {
                //Check for slave addition...
                foreach (IEC61850ServerSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().get61850SlaveGroup().getMODBUSSlaves())//Loop thru slaves...
                {
                    string slaveID = "IEC61850Server_" + slvMB.SlaveNum; //"61850ServerSlaveGroup_"
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<DIMap>> sn in slavesDIMapList)
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
                foreach (KeyValuePair<string, List<DIMap>> sdin in slavesDIMapList)//Loop thru slaves...
                {
                    string slaveID = sdin.Key;
                    bool slaveDeleted = true;
                    if (Utils.getSlaveTypes(slaveID) != SlaveTypes.IEC61850Server) continue;
                    foreach (IEC61850ServerSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().get61850SlaveGroup().getMODBUSSlaves())
                    {
                        if (slaveID == "IEC61850Server_" + slvMB.SlaveNum) //61850ServerSlaveGroup_
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
                    if (ucdi.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucdi.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucdi.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucdi.lvDIMap.Items.Clear();
                        currentSlave = "";
                    }
                }
                fillMapOptions(Utils.getSlaveTypes(currentSlave));
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CheckSPORTSlaveStatusChanges()
        {
            string strRoutineName = "DIConfiguration: CheckSPORTSlaveStatusChanges";
            try
            {
                Console.WriteLine("*** CheckSPORTSlaveStatusChanges");
                //Check for slave addition...
                foreach (SPORTSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().getSPORTSlaveGroup().getSPORTSlaves())
                {
                    string slaveID = "SPORTSlave_" + slvMB.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<DIMap>> sn in slavesDIMapList)
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
                foreach (KeyValuePair<string, List<DIMap>> sain in slavesDIMapList)//Loop thru slaves...
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
                    if (ucdi.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucdi.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucdi.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucdi.lvDIMap.Items.Clear();
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
        private void deleteDIFromMaps(string diNo)
        {
            string strRoutineName = "DIConfiguration: deleteDIFromMaps";
            try
            {
                foreach (KeyValuePair<string, List<DIMap>> sdin in slavesDIMapList)//Loop thru slaves...
                {
                    List<DIMap> delEntry = sdin.Value;
                    foreach (DIMap dimn in delEntry)
                    {
                        if (dimn.DINo == diNo)
                        {
                            delEntry.Remove(dimn);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AddMap2SlaveButton(int slaveNum, string slaveID)
        {
            string strRoutineName = "DIConfiguration: AddMap2SlaveButton";
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
                //--------------------------//
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
                rb.Padding = new Padding(0, 0, 0, 0);
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
                //End Namrata Code
                rb.TextAlign = ContentAlignment.TopCenter;
                rb.BackColor = ColorTranslator.FromHtml("#f2f2f2");
                rb.Appearance = Appearance.Button;
                rb.AutoSize = true;
                rb.Image = Properties.Resources.SlaveRadioButton;
                rb.Click += rbGrpMap2Slave_Click;
                ucdi.flpMap2Slave.Controls.Add(rb);
                rb.Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void rbGrpMap2Slave_Click(object sender, EventArgs e)
        {
            ucdi.grpDIMap.Visible = false; //Ajay: 07/12/2018
            bool rbChanged = false;
            RadioButton currRB = ((RadioButton)sender);
            if (currentSlave != currRB.Tag.ToString())
            {
                currentSlave = currRB.Tag.ToString();
                rbChanged = true;
                refreshCurrentMapList();
                if (ucdi.lvDIlist.SelectedItems.Count > 0)
                {
                    //If possible highlight the map for new slave selected...
                    //Remove selection from DIMap...
                    ucdi.lvDIMap.SelectedItems.Clear();
                    Utils.highlightListviewItem(ucdi.lvDIlist.SelectedItems[0].Text, ucdi.lvDIMap);
                }
            }
            ShowHideSlaveColumns();
            //ShowHideSlaveColumnsSPORT();

            // Ajay: 07/12/2018
            if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
            {
                if (AllowSlaveOptionsOnClick(Utils.getSlaveTypes(currentSlave))) { return; }
                else { }
            }

            if (rbChanged && ucdi.lvDIlist.CheckedItems.Count <= 0) return;//We supported map listing only
            DI mapDI = null;
            if (ucdi.lvDIlist.CheckedItems.Count != 1)
            {
                MessageBox.Show("Select single DI element to map!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            for (int i = 0; i < ucdi.lvDIlist.Items.Count; i++)
            {
                if (ucdi.lvDIlist.Items[i].Checked)
                {
                    mapDI = diList.ElementAt(i);
                    ucdi.lvDIlist.Items[i].Checked = false;//Now we can uncheck in listview...
                    break;
                }
            }
            if (mapDI == null) return;
            bool alreadyMapped = false;  //Search if already mapped for current slave...
            List<DIMap> slaveDIMapList;
            if (!slavesDIMapList.TryGetValue(currentSlave, out slaveDIMapList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                MessageBox.Show("Slave entry doesnot exist!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                Console.WriteLine("##### Slave entries exists");
            }

            if (!alreadyMapped)
            {
                mapMode = Mode.ADD;
                mapEditIndex = -1;
                Utils.resetValues(ucdi.grpDIMap);
                ucdi.txtDIMNo.Text = mapDI.DINo;
                ucdi.txtMapDescription.Text = getDescription(ucdi.lvDIlist, mapDI.DINo.ToString());
                loadMapDefaults();
                //Namrata: 16/10/2019
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.GRAPHICALDISPLAYSLAVE)
                {
                    ucdi.button1.Visible = true;
                    ucdi.splitContainer3.Panel2Collapsed = false;
                    //ucai.TblLayoutCSLD.Visible = true;
                }
                else
                {
                    ucdi.button1.Visible = false;
                    ucdi.splitContainer3.Panel2Collapsed = true;
                    ucdi.TblLayoutCSLD.Visible = false;
                }
                #region IEC61850Server
                //Namrata: 4/11/2017
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server)
                {
                    Utils.CurrentSlaveFinal = currentSlave;//Namrata: 4/11/2017
                    IEC61850Server_OnLoad();
                    GetIndexForIEC61850Server();
                    ucdi.ChkIEC61850MapIndex.Text = "";//Namrata:25/03/2019
                    if (IEC61850MapCheckedList != null)
                    {
                        ucdi.ChkIEC61850MapIndex.CheckBoxItems.ForEach(x =>
                        {
                            x.Checked = false; x.CheckState = CheckState.Unchecked;
                        });
                    }
                }
                #endregion IEC61850Server
                if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE))
                {
                    MODBUSSlave_OnLoad();
                }
                else if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC101SLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC104))
                {
                    IEC101Slave_IEC104Slave_OnLoad();
                }
                //Ajay:06/07/2018
                if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.SPORTSLAVE))
                {
                    SPORTSlave_OnLoad();
                }
                if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.MQTTSLAVE))
                {
                    MQTTSlaveMappingEvents(); ucdi.cmbDIMCommandType.Enabled = false;
                    //ucdi.lblUnit.Visible = true;
                    //ucdi.txtUnit.Visible = true;
                    ucdi.lblKey.Visible = true;
                    ucdi.txtKey.Visible = true;
                    ucdi.txtKey.Text = getMQTTSlaveKey(ucdi.lvDIlist, mapDI.DINo);
                }
                if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.SMSSLAVE))
                {
                    SMSSlaveMapEvent_OnLoad(); ucdi.cmbDIMCommandType.Enabled = false;
                    ucdi.lblKey.Visible = false;
                    ucdi.txtKey.Visible = false;

                }
                if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE))
                {
                    DNPSlave_OnLoad();
                }
                //Namrata: 12/08/2019
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.GRAPHICALDISPLAYSLAVE)
                {
                    //Namrata: 05/11/2019
                    List<string> li = new List<string>();
                    for (int i = 0; i < GDSlave.DsExcelData.Tables.Count; i++)
                    {
                        li.Add(GDSlave.DsExcelData.Tables[i].TableName);
                    }
                    bool matchFound = li.Any(s => s.Contains(currentSlave));
                    if (!matchFound)
                    {
                        MessageBox.Show("Please Save SLD for " + currentSlave, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        //Namrata: 16/10/2019
                        if (ucdi.TblLayoutCSLD.Controls.Count != 0)
                        {
                            ucdi.TblLayoutCSLD.Controls.Clear();
                        }
                        ucdi.TblLayoutCSLD.Visible = false;
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
                            ucdi.TblLayoutCSLD.Controls.Add(pb);
                            ucdi.TblLayoutCSLD.SetColumn(pb, iColumn);
                            ucdi.TblLayoutCSLD.SetColumn(pb, iRow);
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

                        ucdi.TblLayoutCSLD.Visible = true;
                        ucdi.TblLayoutCSLD.Size = new Size(350, 560);
                        #endregion DisplayImage
                        ucdi.txtUnitID.Text = "Unity";
                        GetDataForGDisplaySlave();
                        //var rowColl = GDSlave.DtDIDOWidgets.AsEnumerable();
                        var rowColl = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable();
                        if (ucdi.CmbCellNo.SelectedItem != null)
                        {
                            string name = (from r in rowColl
                                           where r.Field<string>("CellNo") == ucdi.CmbCellNo.SelectedItem.ToString()
                                           select r.Field<string>("Widget")).First<string>();
                            ucdi.CmbWidget.Enabled = false;
                            ucdi.CmbWidget.SelectedIndex = ucdi.CmbWidget.FindStringExact(name);
                        }
                        //Namrata: 26/11/2019
                        UpdateWidgetsList(slaveDIMapList);
                        refreshMapList(slaveDIMapList);
                        DisplaySlave_OnLoad();ucdi.button1.Visible = true;
                        ucdi.splitContainer3.SplitterDistance = ucdi.splitContainer3.Width - 490 + 35; // 35 - Tolerance
                    }
                }
                ucdi.grpDIMap.Visible = true;
                ucdi.txtDIMReportingIndex.Focus();
            }
        }
        //Namrata: 26/11/2019
        private void UpdateWidgetsList(List<DIMap> list)
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
        private void btnDIMDelete_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucdi btnDIMDelete_Click clicked in class!!!");
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
            List<DIMap> slaveDIMapList;
            if (!slavesDIMapList.TryGetValue(currentSlave, out slaveDIMapList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                MessageBox.Show("Error deleting DI map!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            for (int i = ucdi.lvDIMap.Items.Count - 1; i >= 0; i--)
            {
                if (ucdi.lvDIMap.Items[i].Checked)
                {
                    Console.WriteLine("*** removing indices: {0}", i);
                    if (slaveDIMapList != null && slaveDIMapList.Count > 0) //Ajay: 02/07/2018
                    {
                        if (GDSlave.DsExcelData.Tables.Count > 0)
                        {
                            //Namrata: 04/12/2019
                            var results = from row in GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable()
                                          where row.Field<string>("CellNo") == slaveDIMapList[i].CellNo && row.Field<string>("Widget") == slaveDIMapList[i].Widget && row.Field<string>("Type") == "DI"
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
                        slaveDIMapList.RemoveAt(i);
                    }
                    ucdi.lvDIMap.Items[i].Remove();
                    //GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == slaveDIMapList[i].CellNo && x["Widget"].ToString() == slaveDIMapList[i].Widget && x["DBindex"].ToString() == slaveDIMapList[i].DINo).ToList().ForEach(Rw => Rw.Delete());
                    //GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                }
            }
            Console.WriteLine("*** slaveDIMapList count: {0} lv count: {1}", slaveDIMapList.Count, ucdi.lvDIMap.Items.Count);
            refreshMapList(slaveDIMapList);
        }

        DataTable DtCmb = new DataTable();

        //Namrata: 16/08/2019
        private void GetDataForGDisplaySlave1()
        {
            string strRoutineName = "DOConfiguration:GetDataForGDisplaySlave";
            try
            {
                Utils.CurrentSlaveFinal = currentSlave;
                ucdi.CmbCellNo.DataSource = null;
                if (GDSlave.DsExcelData.Tables.Count > 0)
                {
                    GDSlave.ListImageDIDO();
                    ListToDataTable(GDSlave.ListDIDO);

                    List<string> tblNameList = GDSlave.DsExcelData.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                    string CurrentSlave = tblNameList.Where(x => x.Contains(Utils.CurrentSlaveFinal)).Select(x => x).FirstOrDefault();
                    GDSlave.CurrentSlave = CurrentSlave;

                    //Namrata: 24/09/2019
                    List<string> ListCell = GDSlave.DsExcelData.Tables[CurrentSlave].Rows.OfType<DataRow>()
                                                   .Where(x => x.Field<string>("Widget").Contains("SW_") || x.Field<string>("Widget").Contains("Blank") || x.Field<string>("Widget").Contains("Line_") || x.Field<string>("Widget").Contains("Symbol_Arrow")).Select(x => x.Field<string>("CellNo")).Distinct().ToList();
                    ucdi.CmbCellNo.DataSource = null;
                    ucdi.CmbCellNo.Items.Clear();
                    ucdi.CmbCellNo.DataSource = ListCell;
                    ucdi.CmbCellNo.SelectedIndex = 0;

                    List<string> ListWidget = GDSlave.DsExcelData.Tables[CurrentSlave].Rows.OfType<DataRow>()
                                                    .Where(x => x.Field<string>("Widget").Contains("SW_") || x.Field<string>("Widget").Contains("Blank") || x.Field<string>("Widget").Contains("Line_") || x.Field<string>("Widget").Contains("Symbol_Arrow")).Select(x => x.Field<string>("Widget")).Distinct().ToList();
                    ucdi.CmbWidget.DataSource = null;
                    ucdi.CmbWidget.Items.Clear();
                    ucdi.CmbWidget.DataSource = ListWidget;
                    ucdi.CmbWidget.SelectedIndex = 0;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Namrata:12/04/2019
        private void GetIndexForIEC61850Server()
        {

            string strRoutineName = "AIConfiguration: GetIndexForIEC61850Server";
            try
            {
                Utils.CurrentSlaveFinal = currentSlave;
                ucdi.ChkIEC61850MapIndex.DataSource = null;
                List<string> List = new List<string>();
                if (Utils.dsAISlave.Tables.Count > 0)
                {
                    List = (from r in Utils.DSDISlave.Tables[Utils.CurrentSlaveFinal].AsEnumerable() select r.Field<string>("ObjectReferrence")).ToList();
                    ObjectRefForMap = ((DataTable)Utils.DSDISlave.Tables[Utils.CurrentSlaveFinal]).AsEnumerable().Select(x => x[0].ToString()).ToList();
                    NodeForMap = ((DataTable)Utils.DSDISlave.Tables[Utils.CurrentSlaveFinal]).AsEnumerable().Select(x => x[1].ToString()).ToList();
                    var MergeNodeObjeRefList = ObjectRefForMap.Zip(NodeForMap, (leftTooth, rightTooth) => leftTooth + "," + rightTooth).ToList();//Merge List
                    MergeListMap = MergeNodeObjeRefList;
                    ucdi.ChkIEC61850MapIndex.Items.Clear();
                    foreach (var kv in ObjectRefForMap)
                    {
                        ucdi.ChkIEC61850MapIndex.Items.Add(kv.ToString());
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDIMDone_Click(object sender, EventArgs e)
        {
            int intAutoMapRange = 0;
            //if (!ValidateMap()) return;
            if (!slavesDIMapList.TryGetValue(currentSlave, out slaveDIMapList))
            {
                MessageBox.Show("Error adding DI map for slave!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<KeyValuePair<string, string>> dimData = Utils.getKeyValueAttributes(ucdi.grpDIMap);
            if (mapMode == Mode.ADD)
            {
                #region Declaration
                int intDINO = Convert.ToInt32(dimData[15].Value);
                if (dimData[11].Value != "")
                {
                    intAutoMapRange = Convert.ToInt32(dimData[11].Value);
                }
                int intRepotingIndex = Convert.ToInt32(dimData[17].Value);
                int DINo = 0, ReportingIndex = 0;
                string Description = "";
                #endregion Declaration

                #region ListViewItem
                ListViewItem item = ucdi.lvDIlist.Items.Cast<ListViewItem>().FirstOrDefault(x => x.Text == intDINO.ToString()); //Namrata:8/7/2017 //Find Index Of ListView
                ListViewItem ExistDIMap = ucdi.lvDIMap.FindItemWithText(ucdi.txtDIMNo.Text);//Namrata:31/7/2017//Eliminate Duplicate Record From lvAIMAp List
                string ind = ucdi.lvDIlist.Items.IndexOf(item).ToString();
                #endregion ListViewItem

                #region IEC61850Server
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server)
                {
                    IEC61850MapCheckedList = ucdi.ChkIEC61850MapIndex.CheckBoxItems.Where(x => x.Checked == true).Select(x => x.Text).ToList();//Namrata:25/03/2019
                    if (IEC61850MapCheckedList != null)
                    {
                        intMapCheckItems = IEC61850MapCheckedList.Count();
                    }
                    int iListCount = 0;
                    if (diList.Count >= 0)
                    {
                        if ((intMapCheckItems + Convert.ToInt16(ind)) > ucdi.lvDIlist.Items.Count)
                        {
                            MessageBox.Show("Slave Entry Does Not Exist", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            for (int i = intDINO; i <= intDINO + intMapCheckItems - 1; i++)
                            {
                                if (diList.Select(x => x.DINo).ToList().Contains(i.ToString()))
                                {
                                    DINo = i;
                                    ReportingIndex = intRepotingIndex++;
                                    Description = getDescription(ucdi.lvDIlist, DINo.ToString());//Namrata:27/06/2019
                                    DIMap NewDIMap = new DIMap("DI", dimData, Utils.getSlaveTypes(currentSlave));
                                    NewDIMap.DINo = DINo.ToString();
                                    NewDIMap.ReportingIndex = ReportingIndex.ToString();
                                    NewDIMap.Description = Description;
                                    NewDIMap.IEC61850ReportingIndex = IEC61850MapCheckedList[iListCount].ToString();
                                    slaveDIMapList.Add(NewDIMap);
                                    iListCount++;
                                }
                                else
                                {
                                    intAutoMapRange++;
                                }
                            }
                        }
                    }
                }
                #endregion IEC61850Server

                #region Other Slaves
                else
                {
                    if (diList.Count >= 0)
                    {
                        if ((intAutoMapRange + Convert.ToInt16(ind)) > ucdi.lvDIlist.Items.Count)
                        {
                            MessageBox.Show("Slave Entry Does Not Exist", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            for (int i = intDINO; i <= intDINO + intAutoMapRange - 1; i++)//Ajay: 21/11/2017
                            {
                                if (diList.Select(x => x.DINo).ToList().Contains(i.ToString()))//Ajay: 21/11/2017
                                {
                                    DINo = i;
                                    ReportingIndex = intRepotingIndex++;
                                    Description = dimData[10].Value.ToString();//getDescription(ucdi.lvDIlist, DINo.ToString());//Namrata:27/06/2019
                                    DIMap NewDIMap = new DIMap("DI", dimData, Utils.getSlaveTypes(currentSlave));
                                    NewDIMap.DINo = DINo.ToString();
                                    NewDIMap.ReportingIndex = ReportingIndex.ToString();
                                    NewDIMap.Description = Description;
                                    slaveDIMapList.Add(NewDIMap);
                                }
                                else
                                {
                                    intAutoMapRange++;
                                }
                            }
                        }
                    }
                }
                #endregion Other Slaves
            }
            else if (mapMode == Mode.EDIT)
            {
                if (ucdi.ChkIEC61850MapIndex.CheckBoxItems.Count > 0)
                {
                    if (ucdi.ChkIEC61850MapIndex.CheckBoxItems.Where(x => x.Checked == true).Count() > 1)
                    {
                        MessageBox.Show("Please select only 1 ReportingIndex.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        slaveDIMapList[mapEditIndex].updateAttributes(dimData);
                    }
                }
                else
                {
                    slaveDIMapList[mapEditIndex].updateAttributes(dimData);
                }
            }
            refreshMapList(slaveDIMapList);
            ucdi.grpDIMap.Visible = false;
            mapMode = Mode.NONE;
            mapEditIndex = -1;
        }
        private void btnDIMCancel_Click(object sender, EventArgs e)
        {
            ucdi.grpDIMap.Visible = false;
            mapMode = Mode.NONE;
            mapEditIndex = -1;
            Utils.resetValues(ucdi.grpDIMap);
        }
        private void lvDIMap_DoubleClick(object sender, EventArgs e)
        {
            if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)// Ajay: 07/12/2018
            {
                if (AllowSlaveOptionsOnClick(Utils.getSlaveTypes(currentSlave))) { return; }
                else { }
            }

            List<DIMap> slaveDIMapList;
            if (!slavesDIMapList.TryGetValue(currentSlave, out slaveDIMapList))
            {
                MessageBox.Show("Error editing DI map for slave!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int ListIndex = ucdi.lvDIMap.FocusedItem.Index;//Namrata: 07/03/2018
            ListViewItem lvi = ucdi.lvDIMap.Items[ListIndex];
            Utils.UncheckOthers(ucdi.lvDIMap, lvi.Index);
            if (slaveDIMapList.ElementAt(lvi.Index).IsNodeComment)
            {
                MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE))
            {
                MODBUSSlave_OnDoubleClick();
            }
            else if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.SMSSLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC101SLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC104))
            {
                IEC101Slave_IEC104Slave_OnDoubleClick();
                ucdi.lblDMDT.Visible = true;
                ucdi.cmbDIMDataType.Visible = true;
                ucdi.lblDMBP.Visible = true;
                ucdi.txtDIMBitPos.Visible = true;
                ucdi.txtDIAutoMap.Text = "0";
                ucdi.cmbDIMCommandType.Enabled = false;
            }
            if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.SPORTSLAVE))//Ajay:06/07/2018
            {
                SPORTSlave_OnDoubleClick();
            }
            if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server))
            {
                IEC61850Server_OnDoubleClick();
                ucdi.txtDIAutoMap.Text = "";
                GetIndexForIEC61850Server();//Namrata:12/04/2019
            }
            if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.GRAPHICALDISPLAYSLAVE))
            {
                DisplaySlave_OnLoad();
                ucdi.txtDIAutoMap.Text = "";
                GetDataForGDisplaySlave();//Namrata:12/04/2019
            }
            if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.MQTTSLAVE))
            {
                MQTTSlave_OnDoubleClick(); ucdi.cmbDIMCommandType.Enabled = true;
            }
            if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE))
            {
                DNPSlave_OnDoubleClick();
            }
            ucdi.grpDIMap.Visible = true;
            mapMode = Mode.EDIT;
            mapEditIndex = lvi.Index;
            //Namrata:12/04/2019
            ucdi.ChkIEC61850MapIndex.Text = "";
            if ((IEC61850MapCheckedList != null) || (IEC61850MapCheckedList == null))
            {
                ucdi.ChkIEC61850MapIndex.CheckBoxItems.ForEach(x =>
                {
                    x.Checked = false; x.CheckState = CheckState.Unchecked;
                });
            }
            if (ucdi.ChkIEC61850MapIndex.CheckBoxItems.Count > 0)
            {
                ucdi.ChkIEC61850MapIndex.Text = slaveDIMapList[lvi.Index].IEC61850ReportingIndex.ToString();
            }
            loadMapValues();
            if (ucdi.ChkIEC61850MapIndex.CheckBoxItems.Count > 0)
            {
                //Namrata:26/03/2019
                ucdi.ChkIEC61850MapIndex.CheckBoxItems.Where(x => x.Text == slaveDIMapList[lvi.Index].IEC61850ReportingIndex.ToString()).Take(1).ToList().ForEach(x =>
                {
                    x.Checked = true; x.CheckState = CheckState.Checked;
                });
            }
            ucdi.txtDIMReportingIndex.Focus();
        }
        private void loadMapDefaults()
        {
            ucdi.txtDIMReportingIndex.Text = (Globals.DIReportingIndex + 1).ToString();
            ucdi.txtDIMBitPos.Text = "0";
            ucdi.txtUnitID.Text = "1"; ucdi.txtKey.Text = "FDR1-Volt-VR";
        }
        private void loadMapValues()
        {
            List<DIMap> slaveDIMapList;
            if (!slavesDIMapList.TryGetValue(currentSlave, out slaveDIMapList))
            {
                MessageBox.Show("Error loading DI map data for slave!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DIMap dimn = slaveDIMapList.ElementAt(mapEditIndex);
            if (dimn != null)
            {
                ucdi.txtDIMNo.Text = dimn.DINo;
                ucdi.txtDIMReportingIndex.Text = dimn.ReportingIndex;
                ucdi.txtKey.Text = dimn.Key;
                ucdi.cmbDIMDataType.SelectedIndex = ucdi.cmbDIMDataType.FindStringExact(dimn.DataType);
                if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE))
                {
                    ucdi.cmbDIMCommandType.SelectedIndex = ucdi.cmbDIMCommandType.FindStringExact(dimn.CommandType);
                    ucdi.cmbDIMCommandType.Enabled = true;
                }
                else
                {
                    ucdi.cmbDIMCommandType.Enabled = false;
                }
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.SPORTSLAVE)
                {
                    ucdi.txtUnitID.Text = dimn.UnitID;
                    ucdi.txtUnitID.Visible = true;
                    ucdi.chkUsed.Visible = true;
                }
                else
                {
                    ucdi.txtUnitID.Enabled = false;
                    ucdi.txtUnitID.Visible = false;
                    ucdi.chkUsed.Enabled = false;
                    ucdi.chkUsed.Visible = false;
                }
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE)
                {
                    ucdi.cmbEventC.SelectedIndex = ucdi.cmbEventC.FindStringExact(dimn.EventClass);//Namrata:27/03/2019
                    ucdi.CmbVari.SelectedIndex = ucdi.CmbVari.FindStringExact(dimn.Variation);//Namrata:27/03/2019
                    ucdi.CmbEV.SelectedIndex = ucdi.CmbEV.FindStringExact(dimn.EventVariation);//Namrata:27/03/2019
                }
                //Namrata: 20/08/2019
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.GRAPHICALDISPLAYSLAVE)
                {
                    ucdi.CmbCellNo.SelectedIndex = ucdi.CmbCellNo.FindStringExact(dimn.CellNo);
                    ucdi.CmbWidget.SelectedIndex = ucdi.CmbWidget.FindStringExact(dimn.Widget);
                    ucdi.txtUnitID.Enabled = false;
                    ucdi.txtUnitID.Visible = false;
                    if (GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == dimn.CellNo && (x["Type"].ToString() == "DI" && ((x["DBIndex"].ToString() == dimn.DINo) || string.IsNullOrEmpty(x["DBIndex"].ToString())))).Any())
                    {
                        DataRow datarow;
                        datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == dimn.CellNo && x["Type"].ToString() == "DI" && ((x["DBIndex"].ToString() == dimn.DINo) || string.IsNullOrEmpty(x["DBIndex"].ToString()))).ToList().First();
                        //datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == aimp.CellNo && x["Type"].ToString() == "AI").ToList().First();
                        datarow["CellNo"] = dimn.CellNo;
                        datarow["Widget"] = dimn.Widget;
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
                    ucdi.CmbCellNo.Visible = false;
                    ucdi.CmbWidget.Visible = false;
                }
                //Ajay: 12/07/2018 check-unchecked Used checkbox according to mapped Used value
                if (dimn.Used.ToLower() == "yes")
                { ucdi.chkUsed.Checked = true; }
                else { ucdi.chkUsed.Checked = false; }
                ucdi.txtDIMBitPos.Text = dimn.BitPos;
                //Namrata: 18/11/2017
                ucdi.txtMapDescription.Text = dimn.Description;
                if (dimn.Complement.ToLower() == "yes") ucdi.chkComplement.Checked = true;
                else ucdi.chkComplement.Checked = false;
            }
        }
       
        private void refreshMapList(List<DIMap> tmpList)
        {
            string strRoutineName = "DIConfiguration:refreshMapList";
            try
            {
                int cnt = 0;
                ucdi.lvDIMap.Items.Clear();

                ucdi.lblDIMRecords.Text = "0";
                if (tmpList == null) return;

                if (ucdi.lblDIMRecords.InvokeRequired)
                {
                    ucdi.lblDIMRecords.Invoke(new MethodInvoker(delegate
                    {
                        #region ModbusSlave
                        if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE)
                        {
                            foreach (DIMap dimp in tmpList)
                            {
                                string[] row = new string[15];
                                if (dimp.IsNodeComment)
                                {
                                    row[0] = "Comment...";
                                }
                                else
                                {
                                    row[0] = dimp.DINo;
                                    row[1] = "";
                                    row[2] = dimp.ReportingIndex;
                                    row[3] = dimp.DataType;
                                    row[4] = dimp.CommandType;
                                    row[5] = dimp.BitPos;
                                    row[6] = dimp.Complement;
                                    row[7] = "";
                                    row[8] = "";
                                    row[9] = "";
                                    row[10] = "";
                                    row[11] = "";
                                    row[12] = "";
                                    row[13] = "";
                                    row[14] = dimp.Description;
                                }
                                ListViewItem lvItem = new ListViewItem(row);
                                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                                ucdi.lvDIMap.Items.Add(lvItem);
                            }
                        }
                        #endregion ModbusSlave

                        #region DNPSlave
                        if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE)
                        {
                            foreach (DIMap dimp in tmpList)
                            {
                                string[] row = new string[15];
                                if (dimp.IsNodeComment)
                                {
                                    row[0] = "Comment...";
                                }
                                else
                                {
                                    row[0] = dimp.DINo;
                                    row[1] = "";
                                    row[2] = dimp.ReportingIndex;
                                    row[3] = "";
                                    row[4] = dimp.CommandType;
                                    row[5] = dimp.BitPos;
                                    row[6] = dimp.Complement;
                                    row[7] = "";
                                    row[8] = "";
                                    row[9] = "";
                                    row[10] = "";
                                    row[11] = dimp.EventClass;
                                    row[12] = dimp.Variation;
                                    row[13] = dimp.EventVariation;
                                    row[14] = dimp.Description;
                                }
                                ListViewItem lvItem = new ListViewItem(row);
                                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                                ucdi.lvDIMap.Items.Add(lvItem);
                            }
                        }
                        #endregion DNPSlave

                        #region IEC101SLAVE,IEC104,SMSSLAVE
                        else if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.SMSSLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC101SLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC104))
                        {
                            foreach (DIMap dimp in tmpList)
                            {
                                string[] row = new string[15];
                                if (dimp.IsNodeComment)
                                {
                                    row[0] = "Comment...";
                                }
                                else
                                {
                                    row[0] = dimp.DINo;
                                    row[2] = dimp.ReportingIndex;
                                    row[3] = dimp.DataType;
                                    row[5] = dimp.BitPos;
                                    row[6] = dimp.Complement;
                                    row[8] = "";
                                    row[9] = "";
                                    row[10] = "";
                                    row[11] = "";
                                    row[12] = "";
                                    row[13] = "";
                                    row[14] = dimp.Description;
                                }
                                ListViewItem lvItem = new ListViewItem(row);
                                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                                ucdi.lvDIMap.Items.Add(lvItem);
                            }
                        }
                        #endregion IEC101SLAVE,IEC104,SMSSLAVE

                        #region SPORTSLAVE
                        else if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.SPORTSLAVE))
                        {
                            foreach (DIMap dimp in tmpList)
                            {
                                string[] row = new string[15];
                                if (dimp.IsNodeComment)
                                {
                                    row[0] = "Comment...";
                                }
                                else
                                {
                                    row[0] = dimp.DINo;
                                    row[1] = dimp.UnitID;
                                    row[2] = dimp.ReportingIndex;
                                    row[3] = dimp.DataType;
                                    row[5] = dimp.BitPos;
                                    row[6] = dimp.Complement;
                                    row[7] = dimp.Used;
                                    row[8] = "";
                                    row[9] = "";
                                    row[10] = "";
                                    row[11] = "";
                                    row[12] = "";
                                    row[13] = "";
                                    row[14] = dimp.Description;
                                }
                                ListViewItem lvItem = new ListViewItem(row);
                                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                                ucdi.lvDIMap.Items.Add(lvItem);
                            }
                        }
                        #endregion SPORTSLAVE

                        #region IEC61850Server
                        if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server)
                        {
                            foreach (DIMap dimp in tmpList)
                            {
                                string[] row = new string[15];
                                if (dimp.IsNodeComment)
                                {
                                    row[0] = "Comment...";
                                }
                                else
                                {
                                    row[0] = dimp.DINo;
                                    row[1] = "";
                                    row[2] = dimp.IEC61850ReportingIndex;
                                    row[3] = dimp.DataType;
                                    row[4] = dimp.CommandType;
                                    row[5] = dimp.BitPos;
                                    row[6] = dimp.Complement;
                                    row[7] = "";
                                    row[8] = "";
                                    row[9] = "";
                                    row[10] = "";
                                    row[11] = "";
                                    row[12] = "";
                                    row[13] = "";
                                    row[14] = dimp.Description;
                                }
                                ListViewItem lvItem = new ListViewItem(row);
                                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                                ucdi.lvDIMap.Items.Add(lvItem);
                            }
                        }
                        #endregion IEC61850Server

                        #region Graphical Display
                        if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.GRAPHICALDISPLAYSLAVE))
                        {
                            foreach (DIMap dimp in tmpList)
                            {
                                string[] row = new string[15];
                                if (dimp.IsNodeComment)
                                {
                                    row[0] = "Comment...";
                                }
                                else
                                {
                                    row[0] = dimp.DINo;
                                    row[1] = "";
                                    row[2] = "";
                                    row[3] = "";
                                    row[4] = "";
                                    row[5] = "";
                                    row[6] = dimp.Complement;
                                    row[7] = dimp.CellNo;
                                    row[8] = dimp.Widget;
                                    row[9] = "";
                                    row[10] = "";
                                    row[11] = "";
                                    row[12] = "";
                                    row[13] = "";
                                    row[14] = dimp.Description;
                                }
                                //ListViewItem lvItem = new ListViewItem(row);
                                //if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                                //ucdi.lvDIMap.Items.Add(lvItem);


                                string name = (from r in GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable()
                                               where r.Field<string>("CellNo") == dimp.CellNo
                                               select r.Field<string>("Widget")).FirstOrDefault();
                                //Namrata: 16/09/2019
                                #region Update Dataset Tables as per Slave
                                if (dimp.CellNo != "")
                                {
                                    string strConfiguration = dimp.CellNo + "," + dimp.Widget + "," + "DI" + "," + dimp.Description + "," + dimp.DINo + "," + dimp.UnitID;
                                    if (GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == dimp.CellNo && (x["Type"].ToString() == "DI")).Any())
                                    {
                                        DataRow datarow;
                                        datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == dimp.CellNo && x["Type"].ToString() == "DI").ToList().First();
                                        datarow["CellNo"] = dimp.CellNo;
                                        datarow["Widget"] = dimp.Widget;
                                        datarow["Type"] = "DI";
                                        datarow["Name"] = dimp.Description;
                                        datarow["DBIndex"] = dimp.DINo;
                                        datarow["Unit"] = dimp.UnitID;
                                        datarow["Configuration"] = strConfiguration;
                                        GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                                    }
                                    else if (GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == dimp.CellNo && x["Widget"].ToString() == dimp.Widget && string.IsNullOrEmpty(x["Type"].ToString())).ToList().Any())
                                    {
                                        DataRow datarow;
                                        datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == dimp.CellNo && string.IsNullOrEmpty(x["Type"].ToString())).ToList().First();
                                        datarow["CellNo"] = dimp.CellNo;
                                        datarow["Widget"] = dimp.Widget;
                                        datarow["Type"] = "DI";
                                        datarow["Name"] = dimp.Description;
                                        datarow["DBIndex"] = dimp.DINo;
                                        datarow["Unit"] = dimp.UnitID;
                                        datarow["Configuration"] = strConfiguration;
                                        GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                                    }
                                    else if (GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == dimp.CellNo && x["Widget"].ToString() != dimp.Widget && string.IsNullOrEmpty(x["Type"].ToString())).ToList().Any())
                                    {
                                        DataRow datarow;
                                        datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == dimp.CellNo && x["Widget"].ToString() != dimp.Widget && string.IsNullOrEmpty(x["Type"].ToString())).ToList().First();
                                        dimp.Widget = name;
                                        datarow["CellNo"] = dimp.CellNo;
                                        datarow["Widget"] = name;//dimp.Widget;
                                        datarow["Type"] = "DI";
                                        datarow["Name"] = dimp.Description;
                                        datarow["DBIndex"] = dimp.DINo;
                                        datarow["Unit"] = dimp.UnitID;
                                        datarow["Configuration"] = strConfiguration;
                                        GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                                    }

                                    //DataRow dr = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == dimp.CellNo && x["Type"].ToString() == "DI").ToList().First();
                                    //dr["CellNo"] = dimp.CellNo;
                                    //dr["Widget"] = dimp.Widget;
                                    //dr["Type"] = "DI";
                                    //dr["Name"] = dimp.Description;
                                    //dr["DBIndex"] = dimp.DINo;
                                    //dr["Unit"] = dimp.UnitID;
                                    //dr["Configuration"] = strConfiguration;
                                    //GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                                }
                                ListViewItem lvItem = new ListViewItem(row);
                                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                                ucdi.lvDIMap.Items.Add(lvItem);

                                #endregion Update Dataset Tables as per Slave
                            }
                        }
                        #endregion Graphical Display

                        #region MQTTSlave
                        //Namrata:02/04/2019
                        else if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.MQTTSLAVE))
                        {
                            foreach (DIMap dimp in tmpList)
                            {
                                string[] row = new string[15];
                                if (dimp.IsNodeComment)
                                {
                                    row[0] = "Comment...";
                                }
                                else
                                {
                                    row[0] = dimp.DINo;
                                    row[1] = "";
                                    row[2] = dimp.ReportingIndex;
                                    row[3] = dimp.DataType;
                                    row[4] = "";
                                    row[5] = dimp.BitPos;
                                    row[6] = dimp.Complement;
                                    row[7] = "";
                                    row[8] = "";
                                    row[9] = "";
                                    row[10] = dimp.Key;
                                    row[11] = "";
                                    row[12] = "";
                                    row[13] = "";
                                    row[14] = dimp.Description;
                                }
                                ListViewItem lvItem = new ListViewItem(row);
                                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                                ucdi.lvDIMap.Items.Add(lvItem);
                            }
                        }
                        #endregion MQTTSlave

                    }));
                }
                else
                {
                    #region ModbusSlave
                    if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE)
                    {
                        foreach (DIMap dimp in tmpList)
                        {
                            string[] row = new string[15];
                            if (dimp.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = dimp.DINo;
                                row[1] = "";
                                row[2] = dimp.ReportingIndex;
                                row[3] = dimp.DataType;
                                row[4] = dimp.CommandType;
                                row[5] = dimp.BitPos;
                                row[6] = dimp.Complement;
                                row[7] = "";
                                row[8] = "";
                                row[9] = "";
                                row[10] = "";
                                row[11] = "";
                                row[12] = "";
                                row[13] = "";
                                row[14] = dimp.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucdi.lvDIMap.Items.Add(lvItem);
                        }
                    }
                    #endregion ModbusSlave

                    #region DNPSlave
                    if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE)
                    {
                        foreach (DIMap dimp in tmpList)
                        {
                            string[] row = new string[15];
                            if (dimp.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = dimp.DINo;
                                row[1] = "";
                                row[2] = dimp.ReportingIndex;
                                row[3] = "";
                                row[4] = dimp.CommandType;
                                row[5] = dimp.BitPos;
                                row[6] = dimp.Complement;
                                row[7] = "";
                                row[8] = "";
                                row[9] = "";
                                row[10] = "";
                                row[11] = dimp.EventClass;
                                row[12] = dimp.Variation;
                                row[13] = dimp.EventVariation;
                                row[14] = dimp.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucdi.lvDIMap.Items.Add(lvItem);
                        }
                    }
                    #endregion DNPSlave

                    #region IEC101SLAVE,IEC104,SMSSLAVE
                    else if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.SMSSLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC101SLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC104))
                    {
                        foreach (DIMap dimp in tmpList)
                        {
                            string[] row = new string[15];
                            if (dimp.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = dimp.DINo;
                                row[2] = dimp.ReportingIndex;
                                row[3] = dimp.DataType;
                                row[5] = dimp.BitPos;
                                row[6] = dimp.Complement;
                                row[8] = "";
                                row[9] = "";
                                row[10] = "";
                                row[11] = "";
                                row[12] = "";
                                row[13] = "";
                                row[14] = dimp.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucdi.lvDIMap.Items.Add(lvItem);
                        }
                    }
                    #endregion IEC101SLAVE,IEC104,SMSSLAVE

                    #region SPORTSLAVE
                    else if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.SPORTSLAVE))
                    {
                        foreach (DIMap dimp in tmpList)
                        {
                            string[] row = new string[15];
                            if (dimp.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = dimp.DINo;
                                row[1] = dimp.UnitID;
                                row[2] = dimp.ReportingIndex;
                                row[3] = dimp.DataType;
                                row[5] = dimp.BitPos;
                                row[6] = dimp.Complement;
                                row[7] = dimp.Used;
                                row[8] = "";
                                row[9] = "";
                                row[10] = "";
                                row[11] = "";
                                row[12] = "";
                                row[13] = "";
                                row[14] = dimp.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucdi.lvDIMap.Items.Add(lvItem);
                        }
                    }
                    #endregion SPORTSLAVE

                    #region IEC61850Server
                    if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server)
                    {
                        foreach (DIMap dimp in tmpList)
                        {
                            string[] row = new string[15];
                            if (dimp.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = dimp.DINo;
                                row[1] = "";
                                row[2] = dimp.IEC61850ReportingIndex;
                                row[3] = dimp.DataType;
                                row[4] = dimp.CommandType;
                                row[5] = dimp.BitPos;
                                row[6] = dimp.Complement;
                                row[7] = "";
                                row[8] = "";
                                row[9] = "";
                                row[10] = "";
                                row[11] = "";
                                row[12] = "";
                                row[13] = "";
                                row[14] = dimp.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucdi.lvDIMap.Items.Add(lvItem);
                        }
                    }
                    #endregion IEC61850Server

                    #region Graphical Display
                    if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.GRAPHICALDISPLAYSLAVE))
                    {
                        foreach (DIMap dimp in tmpList)
                        {
                            string[] row = new string[15];
                            if (dimp.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = dimp.DINo;
                                row[1] = "";
                                row[2] = "";
                                row[3] = "";
                                row[4] = "";
                                row[5] = "";
                                row[6] = dimp.Complement;
                                row[7] = dimp.CellNo;
                                row[8] = dimp.Widget;
                                row[9] = "";
                                row[10] = "";
                                row[11] = "";
                                row[12] = "";
                                row[13] = "";
                                row[14] = dimp.Description;
                            }
                            //ListViewItem lvItem = new ListViewItem(row);
                            //if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            //ucdi.lvDIMap.Items.Add(lvItem);

                            string name = (from r in GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable()
                                           where r.Field<string>("CellNo") == dimp.CellNo
                                           select r.Field<string>("Widget")).First<string>();
                            //Namrata: 16/09/2019
                            #region Update Dataset Tables as per Slave
                            if (dimp.CellNo != "")
                            {
                                string strConfiguration = dimp.CellNo + "," + dimp.Widget + "," + "DI" + "," + dimp.Description + "," + dimp.DINo + "," + dimp.UnitID;
                                if (GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == dimp.CellNo && (x["Type"].ToString() == "DI")).Any())
                                {
                                    DataRow datarow;
                                    datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == dimp.CellNo && x["Type"].ToString() == "DI").ToList().First();
                                    datarow["CellNo"] = dimp.CellNo;
                                    datarow["Widget"] = dimp.Widget;
                                    datarow["Type"] = "DI";
                                    datarow["Name"] = dimp.Description;
                                    datarow["DBIndex"] = dimp.DINo;
                                    datarow["Unit"] = dimp.UnitID;
                                    datarow["Configuration"] = strConfiguration;
                                    GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                                }
                                else if (GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == dimp.CellNo && x["Widget"].ToString() == dimp.Widget && string.IsNullOrEmpty(x["Type"].ToString())).ToList().Any())
                                {
                                    DataRow datarow;
                                    datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == dimp.CellNo && string.IsNullOrEmpty(x["Type"].ToString())).ToList().First();
                                    datarow["CellNo"] = dimp.CellNo;
                                    datarow["Widget"] = dimp.Widget;
                                    datarow["Type"] = "DI";
                                    datarow["Name"] = dimp.Description;
                                    datarow["DBIndex"] = dimp.DINo;
                                    datarow["Unit"] = dimp.UnitID;
                                    datarow["Configuration"] = strConfiguration;
                                    GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                                }
                                else if (GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == dimp.CellNo && x["Widget"].ToString() != dimp.Widget && string.IsNullOrEmpty(x["Type"].ToString())).ToList().Any())
                                {
                                    DataRow datarow;
                                    datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == dimp.CellNo && x["Widget"].ToString() != dimp.Widget && string.IsNullOrEmpty(x["Type"].ToString())).ToList().First();
                                    dimp.Widget = name;
                                    datarow["CellNo"] = dimp.CellNo;
                                    datarow["Widget"] = name;//dimp.Widget;
                                    datarow["Type"] = "DI";
                                    datarow["Name"] = dimp.Description;
                                    datarow["DBIndex"] = dimp.DINo;
                                    datarow["Unit"] = dimp.UnitID;
                                    datarow["Configuration"] = strConfiguration;
                                    GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                                }

                                //DataRow dr = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == dimp.CellNo && x["Type"].ToString() == "DI").ToList().First();
                                //dr["CellNo"] = dimp.CellNo;
                                //dr["Widget"] = dimp.Widget;
                                //dr["Type"] = "DI";
                                //dr["Name"] = dimp.Description;
                                //dr["DBIndex"] = dimp.DINo;
                                //dr["Unit"] = dimp.UnitID;
                                //dr["Configuration"] = strConfiguration;
                                //GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucdi.lvDIMap.Items.Add(lvItem);

                            #endregion Update Dataset Tables as per Slave
                        }
                    }
                    #endregion Graphical Display

                    #region MQTTSlave
                    //Namrata:02/04/2019
                    else if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.MQTTSLAVE))
                    {
                        foreach (DIMap dimp in tmpList)
                        {
                            string[] row = new string[15];
                            if (dimp.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = dimp.DINo;
                                row[1] = "";
                                row[2] = dimp.ReportingIndex;
                                row[3] = dimp.DataType;
                                row[4] = "";
                                row[5] = dimp.BitPos;
                                row[6] = dimp.Complement;
                                row[7] = "";
                                row[8] = "";
                                row[9] = "";
                                row[10] = dimp.Key;
                                row[11] = "";
                                row[12] = "";
                                row[13] = "";
                                row[14] = dimp.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucdi.lvDIMap.Items.Add(lvItem);
                        }
                    }
                    #endregion MQTTSlave

                }



                //#region ModbusSlave
                //    if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE)
                //{
                //    foreach (DIMap dimp in tmpList)
                //    {
                //        string[] row = new string[15];
                //        if (dimp.IsNodeComment)
                //        {
                //            row[0] = "Comment...";
                //        }
                //        else
                //        {
                //            row[0] = dimp.DINo;
                //            row[1] = "";
                //            row[2] = dimp.ReportingIndex;
                //            row[3] = dimp.DataType;
                //            row[4] = dimp.CommandType;
                //            row[5] = dimp.BitPos;
                //            row[6] = dimp.Complement;
                //            row[7] = "";
                //            row[8] = "";
                //            row[9] = "";
                //            row[10] = "";
                //            row[11] = "";
                //            row[12] = "";
                //            row[13] = "";
                //            row[14] = dimp.Description;
                //        }
                //        ListViewItem lvItem = new ListViewItem(row);
                //        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                //        ucdi.lvDIMap.Items.Add(lvItem);
                //    }
                //}
                //#endregion ModbusSlave

                //#region DNPSlave
                //if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE)
                //{
                //    foreach (DIMap dimp in tmpList)
                //    {
                //        string[] row = new string[15];
                //        if (dimp.IsNodeComment)
                //        {
                //            row[0] = "Comment...";
                //        }
                //        else
                //        {
                //            row[0] = dimp.DINo;
                //            row[1] = "";
                //            row[2] = dimp.ReportingIndex;
                //            row[3] = "";
                //            row[4] = dimp.CommandType;
                //            row[5] = dimp.BitPos;
                //            row[6] = dimp.Complement;
                //            row[7] = "";
                //            row[8] = "";
                //            row[9] = "";
                //            row[10] = "";
                //            row[11] = dimp.EventClass;
                //            row[12] = dimp.Variation;
                //            row[13] = dimp.EventVariation;
                //            row[14] = dimp.Description;
                //        }
                //        ListViewItem lvItem = new ListViewItem(row);
                //        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                //        ucdi.lvDIMap.Items.Add(lvItem);
                //    }
                //}
                //#endregion DNPSlave

                //#region IEC101SLAVE,IEC104,SMSSLAVE
                //else if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.SMSSLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC101SLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC104))
                //{
                //    foreach (DIMap dimp in tmpList)
                //    {
                //        string[] row = new string[15];
                //        if (dimp.IsNodeComment)
                //        {
                //            row[0] = "Comment...";
                //        }
                //        else
                //        {
                //            row[0] = dimp.DINo;
                //            row[2] = dimp.ReportingIndex;
                //            row[3] = dimp.DataType;
                //            row[5] = dimp.BitPos;
                //            row[6] = dimp.Complement;
                //            row[8] = "";
                //            row[9] = "";
                //            row[10] = "";
                //            row[11] = "";
                //            row[12] = "";
                //            row[13] = "";
                //            row[14] = dimp.Description;
                //        }
                //        ListViewItem lvItem = new ListViewItem(row);
                //        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                //        ucdi.lvDIMap.Items.Add(lvItem);
                //    }
                //}
                //#endregion IEC101SLAVE,IEC104,SMSSLAVE

                //#region SPORTSLAVE
                //else if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.SPORTSLAVE))
                //{
                //    foreach (DIMap dimp in tmpList)
                //    {
                //        string[] row = new string[15];
                //        if (dimp.IsNodeComment)
                //        {
                //            row[0] = "Comment...";
                //        }
                //        else
                //        {
                //            row[0] = dimp.DINo;
                //            row[1] = dimp.UnitID;
                //            row[2] = dimp.ReportingIndex;
                //            row[3] = dimp.DataType;
                //            row[5] = dimp.BitPos;
                //            row[6] = dimp.Complement;
                //            row[7] = dimp.Used;
                //            row[8] = "";
                //            row[9] = "";
                //            row[10] = "";
                //            row[11] = "";
                //            row[12] = "";
                //            row[13] = "";
                //            row[14] = dimp.Description;
                //        }
                //        ListViewItem lvItem = new ListViewItem(row);
                //        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                //        ucdi.lvDIMap.Items.Add(lvItem);
                //    }
                //}
                //#endregion SPORTSLAVE

                //#region IEC61850Server
                //if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server)
                //{
                //    foreach (DIMap dimp in tmpList)
                //    {
                //        string[] row = new string[15];
                //        if (dimp.IsNodeComment)
                //        {
                //            row[0] = "Comment...";
                //        }
                //        else
                //        {
                //            row[0] = dimp.DINo;
                //            row[1] = "";
                //            row[2] = dimp.IEC61850ReportingIndex;
                //            row[3] = dimp.DataType;
                //            row[4] = dimp.CommandType;
                //            row[5] = dimp.BitPos;
                //            row[6] = dimp.Complement;
                //            row[7] = "";
                //            row[8] = "";
                //            row[9] = "";
                //            row[10] = "";
                //            row[11] = "";
                //            row[12] = "";
                //            row[13] = "";
                //            row[14] = dimp.Description;
                //        }
                //        ListViewItem lvItem = new ListViewItem(row);
                //        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                //        ucdi.lvDIMap.Items.Add(lvItem);
                //    }
                //}
                //#endregion IEC61850Server

                //#region Graphical Display
                //if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.GRAPHICALDISPLAYSLAVE))
                //{
                //    foreach (DIMap dimp in tmpList)
                //    {
                //        string[] row = new string[15];
                //        if (dimp.IsNodeComment)
                //        {
                //            row[0] = "Comment...";
                //        }
                //        else
                //        {
                //            row[0] = dimp.DINo;
                //            row[1] = "";
                //            row[2] = "";
                //            row[3] = "";
                //            row[4] = "";
                //            row[5] = "";
                //            row[6] = dimp.Complement;
                //            row[7] = dimp.CellNo;
                //            row[8] = dimp.Widget;
                //            row[9] = "";
                //            row[10] = "";
                //            row[11] = "";
                //            row[12] = "";
                //            row[13] = "";
                //            row[14] = dimp.Description;
                //        }
                //        //ListViewItem lvItem = new ListViewItem(row);
                //        //if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                //        //ucdi.lvDIMap.Items.Add(lvItem);

                //        string name = (from r in GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable()
                //                       where r.Field<string>("CellNo") == dimp.CellNo
                //                       select r.Field<string>("Widget")).First<string>();
                //        //Namrata: 16/09/2019
                //        #region Update Dataset Tables as per Slave
                //        if (dimp.CellNo != "")
                //        {
                //            string strConfiguration = dimp.CellNo + "," + dimp.Widget + "," + "DI" + "," + dimp.Description + "," + dimp.DINo + "," + dimp.UnitID;
                //            if (GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == dimp.CellNo && (x["Type"].ToString() == "DI")).Any())
                //            {
                //                DataRow datarow;
                //                datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == dimp.CellNo && x["Type"].ToString() == "DI").ToList().First();
                //                datarow["CellNo"] = dimp.CellNo;
                //                datarow["Widget"] = dimp.Widget;
                //                datarow["Type"] = "DI";
                //                datarow["Name"] = dimp.Description;
                //                datarow["DBIndex"] = dimp.DINo;
                //                datarow["Unit"] = dimp.UnitID;
                //                datarow["Configuration"] = strConfiguration;
                //                GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                //            }
                //            else if (GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == dimp.CellNo && x["Widget"].ToString() == dimp.Widget && string.IsNullOrEmpty(x["Type"].ToString())).ToList().Any())
                //            {
                //                DataRow datarow;
                //                datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == dimp.CellNo && string.IsNullOrEmpty(x["Type"].ToString())).ToList().First();
                //                datarow["CellNo"] = dimp.CellNo;
                //                datarow["Widget"] = dimp.Widget;
                //                datarow["Type"] = "DI";
                //                datarow["Name"] = dimp.Description;
                //                datarow["DBIndex"] = dimp.DINo;
                //                datarow["Unit"] = dimp.UnitID;
                //                datarow["Configuration"] = strConfiguration;
                //                GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                //            }
                //            else if (GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == dimp.CellNo && x["Widget"].ToString() != dimp.Widget && string.IsNullOrEmpty(x["Type"].ToString())).ToList().Any())
                //            {
                //                DataRow datarow;
                //                datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == dimp.CellNo && x["Widget"].ToString() != dimp.Widget && string.IsNullOrEmpty(x["Type"].ToString())).ToList().First();
                //                dimp.Widget = name;
                //                datarow["CellNo"] = dimp.CellNo;
                //                datarow["Widget"] = name;//dimp.Widget;
                //                datarow["Type"] = "DI";
                //                datarow["Name"] = dimp.Description;
                //                datarow["DBIndex"] = dimp.DINo;
                //                datarow["Unit"] = dimp.UnitID;
                //                datarow["Configuration"] = strConfiguration;
                //                GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                //            }

                //            //DataRow dr = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == dimp.CellNo && x["Type"].ToString() == "DI").ToList().First();
                //            //dr["CellNo"] = dimp.CellNo;
                //            //dr["Widget"] = dimp.Widget;
                //            //dr["Type"] = "DI";
                //            //dr["Name"] = dimp.Description;
                //            //dr["DBIndex"] = dimp.DINo;
                //            //dr["Unit"] = dimp.UnitID;
                //            //dr["Configuration"] = strConfiguration;
                //            //GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                //        }
                //        ListViewItem lvItem = new ListViewItem(row);
                //        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                //        ucdi.lvDIMap.Items.Add(lvItem);

                //        #endregion Update Dataset Tables as per Slave
                //    }
                //}
                //#endregion Graphical Display

                //#region MQTTSlave
                ////Namrata:02/04/2019
                //else if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.MQTTSLAVE))
                //{
                //    foreach (DIMap dimp in tmpList)
                //    {
                //        string[] row = new string[15];
                //        if (dimp.IsNodeComment)
                //        {
                //            row[0] = "Comment...";
                //        }
                //        else
                //        {
                //            row[0] = dimp.DINo;
                //            row[1] = "";
                //            row[2] = dimp.ReportingIndex;
                //            row[3] = dimp.DataType;
                //            row[4] = "";
                //            row[5] = dimp.BitPos;
                //            row[6] = dimp.Complement;
                //            row[7] = "";
                //            row[8] = "";
                //            row[9] = "";
                //            row[10] = dimp.Key;
                //            row[11] = "";
                //            row[12] = "";
                //            row[13] = "";
                //            row[14] = dimp.Description;
                //        }
                //        ListViewItem lvItem = new ListViewItem(row);
                //        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                //        ucdi.lvDIMap.Items.Add(lvItem);
                //    }
                //}
                //#endregion MQTTSlave

                ucdi.lblDIMRecords.Text = tmpList.Count.ToString();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void refreshCurrentMapList()
        {
            fillMapOptions(Utils.getSlaveTypes(currentSlave));
            List<DIMap> sdimList;
            if (!slavesDIMapList.TryGetValue(currentSlave, out sdimList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                refreshMapList(null);
            }
            else
            {
                refreshMapList(sdimList);
            }
        }
        /* ============================================= Above this, DI Map logic... ============================================= */
        private void fillOptions()
        {
            string strRoutineName = "DI : fillOptions";
            try
            {
                if (!dtdataset.Columns.Contains("Address")) //Ajay: 22/09/2018 Condition checked to handle exception
                { DataColumn dcAddressColumn = dtdataset.Columns.Add("Address", typeof(string)); }
                if (!dtdataset.Columns.Contains("IED")) //Ajay: 22/09/2018 Condition checked to handle exception
                { dtdataset.Columns.Add("IED", typeof(string)); }

                //Fill IED Name
                //ucdi.cmbIEDName.Items.Clear(); //Ajay: 12/01/2018 commented
                //Namrata: 31/10/2017
                ucdi.cmbIEDName.DataSource = Utils.dsIED.Tables[Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname];//Utils.DtIEDName;
                ucdi.cmbIEDName.DisplayMember = "IEDName";
                //Namrata: 04/04/2018
                if (Utils.Iec61850IEDname != "")
                {
                    ucdi.cmbIEDName.Text = Utils.Iec61850IEDname;
                }
                //Namrata: 15/9/2017
                //Fill ResponseType For IEC61850Client
                //ucdi.cmb61850DIResponseType.Items.Clear();  //Ajay: 12/01/2018 commented
                //Namrata: 31/10/2017
                ucdi.cmb61850DIResponseType.DataSource = Utils.dsResponseType.Tables[Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname];//Utils.DtAddress;
                ucdi.cmb61850DIResponseType.DisplayMember = "Address";

                //Fill Response Type...
                if (masterType == MasterTypes.IEC61850Client)
                {
                    ucdi.cmbResponseType.Items.Clear();
                }
                else
                {
                    ucdi.cmbResponseType.Items.Clear();
                    if (masterType != MasterTypes.LoadProfile) //Ajay: 31/07/2018
                    {
                        foreach (String rt in DI.getResponseTypes(masterType))
                        {
                            ucdi.cmbResponseType.Items.Add(rt.ToString());
                        }
                        ucdi.cmbResponseType.SelectedIndex = 0;
                    }
                }
                //Fill Data Type...
                if ((masterType == MasterTypes.IEC61850Client) || (masterType == MasterTypes.SPORT))
                {
                    ucdi.CmbDataType.Items.Clear();
                }
                else
                {
                    ucdi.CmbDataType.Items.Clear();
                    if (masterType != MasterTypes.LoadProfile) //Ajay: 31/07/2018
                    {
                        foreach (String dt in DI.getDataTypes(masterType))
                        {
                            ucdi.CmbDataType.Items.Add(dt.ToString());
                        }
                    }
                }
                //Ajay: 31/07/2018
                if (masterType != MasterTypes.LoadProfile)
                {
                    ucdi.cmbName.Items.Clear();
                    ucdi.cmbDI1.Items.Clear();
                }
                else
                {
                    //ucdi.cmbName.Items.Clear();
                    //ucdi.cmbDI1.Items.Clear();
                    ucdi.cmbName.DataSource = Utils.getOpenProPlusHandle().getDataTypeValues("LoadProfile_DI_Name");
                    ucdi.cmbDI1.DataSource = Utils.GetAllDIList();
                }
            }

            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //ucdi.CmbDataType.SelectedIndex = 0;
        }
        private void fillMapOptions(SlaveTypes sType)
        {
            /***** Fill Map details related combobox ******/
            try
            {
                //Fill Data Type...
                ucdi.cmbDIMDataType.Items.Clear();
                foreach (String dt in DIMap.getDataTypes(sType))
                {
                    ucdi.cmbDIMDataType.Items.Add(dt.ToString());
                }
                if (ucdi.cmbDIMDataType.Items.Count > 0) ucdi.cmbDIMDataType.SelectedIndex = 0;
                #region Fill Variations
                ucdi.CmbVari.Items.Clear();
                foreach (String dt in DIMap.getVariartions(sType))
                {
                    ucdi.CmbVari.Items.Add(dt.ToString());
                }
                if (ucdi.CmbVari.Items.Count > 0) ucdi.CmbVari.SelectedIndex = 0;
                #endregion Fill Variations

                #region Fill EVariations
                ucdi.CmbEV.Items.Clear();
                foreach (String dt in DIMap.getEventsVariations(sType))
                {
                    ucdi.CmbEV.Items.Add(dt.ToString());
                }
                if (ucdi.CmbEV.Items.Count > 0) ucdi.CmbEV.SelectedIndex = 0;
                #endregion Fill EVariations

                #region Fill EClass
                ucdi.cmbEventC.Items.Clear();
                foreach (String dt in DIMap.getEventsClasses(sType))
                {
                    ucdi.cmbEventC.Items.Add(dt.ToString());
                }
                if (ucdi.cmbEventC.Items.Count > 0) ucdi.cmbEventC.SelectedIndex = 0;
                #endregion Fill EClass
            }
            catch (System.NullReferenceException)
            {
                Utils.WriteLine(VerboseLevel.ERROR, "DI Map DataType does not exist for Slave Type: {0}", sType.ToString());
            }
            try
            {
                //Fill Command Type...
                ucdi.cmbDIMCommandType.Items.Clear();
                foreach (String ct in DIMap.getCommandTypes(sType))
                {
                    ucdi.cmbDIMCommandType.Items.Add(ct.ToString());
                }
                if (ucdi.cmbDIMCommandType.Items.Count > 0) ucdi.cmbDIMCommandType.SelectedIndex = 0;
            }
            catch (System.NullReferenceException)
            {
                Utils.WriteLine(VerboseLevel.ERROR, "DI Map CommandType does not exist for Slave Type: {0}", sType.ToString());
            }
        }
        private void addListHeaders()
        {
            #region Master
            //Namrata: 12/09/2017
            if (masterType == MasterTypes.IEC61850Client)
            {
                ucdi.lvDIlist.Columns.Add("DI No.", 70, HorizontalAlignment.Left);

                ucdi.lvDIlist.Columns.Add("IEDName", 100, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Response Type", 210, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Index", 320, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("FC", 60, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Sub Index", 100, HorizontalAlignment.Left);

                ucdi.lvDIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                //Namrata: 15/9/2017
                //Hide IED Name
                ucdi.lvDIlist.Columns[1].Width = 0;
            }
            else if (masterType == MasterTypes.MODBUS)
            {
                ucdi.lvDIlist.Columns.Add("DI No.", 70, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Sub Index", 120, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Data Type", 100, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
            }
            else if (masterType == MasterTypes.ADR)
            {
                ucdi.lvDIlist.Columns.Add("DI No.", 70, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Sub Index", 120, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Event_T", 100, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Event_F", 100, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
            }
            else if (masterType == MasterTypes.IEC101)
            {
                ucdi.lvDIlist.Columns.Add("DI No.", 70, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Sub Index", 120, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
            }
            else if (masterType == MasterTypes.SPORT)
            {
                ucdi.lvDIlist.Columns.Add("DI No.", 70, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Sub Index", 120, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
            }
            else if (masterType == MasterTypes.IEC104)
            {
                ucdi.lvDIlist.Columns.Add("DI No.", 70, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Sub Index", 120, HorizontalAlignment.Left);
                //Ajay: 12/11/2018 Event Removed
                //ucdi.lvDIlist.Columns.Add("Event", "Event", 90, HorizontalAlignment.Left, String.Empty); //Ajay:18/07/2018 Event Added
                ucdi.lvDIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
            }
            else if (masterType == MasterTypes.IEC103)
            {
                ucdi.lvDIlist.Columns.Add("DI No.", 70, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Sub Index", 120, HorizontalAlignment.Left);
                //ucdi.lvDIlist.Columns.Add("Event", 90, HorizontalAlignment.Left); //Ajay:14/07/2018
                ucdi.lvDIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
            }
            else if (masterType == MasterTypes.Virtual)
            {
                ucdi.lvDIlist.Columns.Add("DI No.", 70, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Sub Index", 120, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
            }
            //Ajay: 31/07/2018
            else if (masterType == MasterTypes.LoadProfile)
            {
                ucdi.lvDIlist.Columns.Add("DI No.", 70, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Name", 130, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("DI1", 60, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Constant", 60, HorizontalAlignment.Left);
                //ucdi.lvDIlist.Columns.Add("Log Enable", 80, HorizontalAlignment.Left); //Ajay: "LogEnable" removed
                ucdi.lvDIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
            }
            #endregion Master

            #region Map
            //Add DI map headers...
            ucdi.lvDIMap.Columns.Add("DI No.", "DI No.", 70, HorizontalAlignment.Left, null);
            ucdi.lvDIMap.Columns.Add("Unit ID", 0, HorizontalAlignment.Left);
            ucdi.lvDIMap.Columns.Add("Reporting Index", "Reporting Index", 130, HorizontalAlignment.Left, null);
            ucdi.lvDIMap.Columns.Add("Data Type", "Data Type", 130, HorizontalAlignment.Left, null);
            ucdi.lvDIMap.Columns.Add("Command Type", "Command Type", 0, HorizontalAlignment.Left, null);
            ucdi.lvDIMap.Columns.Add("Bit Pos.", "Bit Pos.", 80, HorizontalAlignment.Left, null);
            ucdi.lvDIMap.Columns.Add("Complement", "Complement", -2, HorizontalAlignment.Left, null);
            //Namrata: 16/08/2019
            ucdi.lvDIMap.Columns.Add("CellNo", 0, HorizontalAlignment.Left);
            ucdi.lvDIMap.Columns.Add("Widget", 0, HorizontalAlignment.Left);
            ucdi.lvDIMap.Columns.Add("Used", 0, HorizontalAlignment.Left);
            ucdi.lvDIMap.Columns.Add("Key", "Key", 90, HorizontalAlignment.Left, null);

            //Namrata: 12/11/2019
            ucdi.lvDIMap.Columns.Add("Event Class", 0, HorizontalAlignment.Left);
            ucdi.lvDIMap.Columns.Add("Variation", 0, HorizontalAlignment.Left);
            ucdi.lvDIMap.Columns.Add("Event Variation", 0, HorizontalAlignment.Left);

            ucdi.lvDIMap.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, null);
            #endregion Map
        }

        private void ShowHideSlaveColumns()
        {
            string strRoutineName = "AIConfiguration:ShowHideSlaveColumns";
            try
            {
                switch (Utils.getSlaveTypes(currentSlave))
                {
                    case SlaveTypes.IEC104:
                        ucdi.lvDIMap.Columns[1].Text = "Unit ID";
                        Utils.getColumnHeader(ucdi.lvDIMap, "DI No.").Width = 80;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Unit ID").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Reporting Index").Width = 150;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Data Type").Width = 150;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Command Type").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Bit Pos.").Width = 80;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Complement").Width = 80;
                        Utils.getColumnHeader(ucdi.lvDIMap, "CellNo").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Widget").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Used").Width = 0;
                        //Namrata: 21/10/2019
                        Utils.getColumnHeader(ucdi.lvDIMap, "Key").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Description").Width = 80;
                        //Namrata: 12/11/2019
                        Utils.getColumnHeader(ucdi.lvDIMap, "Event Class").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Variation").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Event Variation").Width = 0;
                        break;

                    case SlaveTypes.MODBUSSLAVE:
                        ucdi.lvDIMap.Columns[1].Text = "Unit ID";
                        Utils.getColumnHeader(ucdi.lvDIMap, "DI No.").Width = 80;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Unit ID").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Reporting Index").Width = 150;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Data Type").Width = 150;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Command Type").Width = 100;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Bit Pos.").Width = 80;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Complement").Width = 80;
                        Utils.getColumnHeader(ucdi.lvDIMap, "CellNo").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Widget").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Used").Width = 0;
                        //Namrata: 21/10/2019
                        Utils.getColumnHeader(ucdi.lvDIMap, "Key").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Description").Width = 80;
                        //Namrata: 12/11/2019
                        Utils.getColumnHeader(ucdi.lvDIMap, "Event Class").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Variation").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Event Variation").Width = 0;
                        break;
                    case SlaveTypes.DNP3SLAVE:
                        ucdi.lvDIMap.Columns[1].Text = "Unit ID";
                        Utils.getColumnHeader(ucdi.lvDIMap, "DI No.").Width = 80;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Unit ID").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Reporting Index").Width = 150;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Data Type").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Command Type").Width = 100;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Bit Pos.").Width = 80;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Complement").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "CellNo").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Widget").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Used").Width = 0;
                        //Namrata: 21/10/2019
                        Utils.getColumnHeader(ucdi.lvDIMap, "Key").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Description").Width = 80;
                        //Namrata: 12/11/2019
                        Utils.getColumnHeader(ucdi.lvDIMap, "Event Class").Width = 100;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Variation").Width = 100;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Event Variation").Width = 100;
                        break;
                    case SlaveTypes.IEC101SLAVE:
                        ucdi.lvDIMap.Columns[1].Text = "Unit ID";
                        Utils.getColumnHeader(ucdi.lvDIMap, "DI No.").Width = 80;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Unit ID").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Reporting Index").Width = 150;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Data Type").Width = 150;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Command Type").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Bit Pos.").Width = 80;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Complement").Width = 80;
                        Utils.getColumnHeader(ucdi.lvDIMap, "CellNo").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Widget").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Used").Width = 0;
                        //Namrata: 21/10/2019
                        Utils.getColumnHeader(ucdi.lvDIMap, "Key").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Description").Width = 80;
                        //Namrata: 12/11/2019
                        Utils.getColumnHeader(ucdi.lvDIMap, "Event Class").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Variation").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Event Variation").Width = 0;
                        break;

                    case SlaveTypes.IEC61850Server:
                        ucdi.lvDIMap.Columns[1].Text = "Unit ID";
                        Utils.getColumnHeader(ucdi.lvDIMap, "DI No.").Width = 80;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Unit ID").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Reporting Index").Width = 150;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Data Type").Width = 150;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Command Type").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Bit Pos.").Width = 80;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Complement").Width = 80;
                        Utils.getColumnHeader(ucdi.lvDIMap, "CellNo").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Widget").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Used").Width = 0;
                        //Namrata: 21/10/2019
                        Utils.getColumnHeader(ucdi.lvDIMap, "Key").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Description").Width = 80;
                        //Namrata: 12/11/2019
                        Utils.getColumnHeader(ucdi.lvDIMap, "Event Class").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Variation").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Event Variation").Width = 0;
                        break;

                    case SlaveTypes.SPORTSLAVE:
                        ucdi.lvDIMap.Columns[1].Text = "Unit ID";
                        Utils.getColumnHeader(ucdi.lvDIMap, "DI No.").Width = 80;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Unit ID").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Reporting Index").Width = 150;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Data Type").Width = 150;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Command Type").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Bit Pos.").Width = 80;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Complement").Width = 80;
                        Utils.getColumnHeader(ucdi.lvDIMap, "CellNo").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Widget").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Used").Width = 0;
                        //Namrata: 21/10/2019
                        Utils.getColumnHeader(ucdi.lvDIMap, "Key").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Description").Width = 80;
                        //Namrata: 12/11/2019
                        Utils.getColumnHeader(ucdi.lvDIMap, "Event Class").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Variation").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Event Variation").Width = 0;
                        break;

                    //Namrata: 16/08/2019
                    case SlaveTypes.GRAPHICALDISPLAYSLAVE:
                        ucdi.lvDIMap.Columns[1].Text = "Unit";
                        Utils.getColumnHeader(ucdi.lvDIMap, "DI No.").Width = 80;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Unit").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Reporting Index").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Data Type").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Command Type").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Bit Pos.").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Complement").Width = 80;
                        Utils.getColumnHeader(ucdi.lvDIMap, "CellNo").Width = 90;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Widget").Width = 150;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Used").Width = 0;
                        //Namrata: 21/10/2019
                        Utils.getColumnHeader(ucdi.lvDIMap, "Key").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Description").Width = 100;
                        //Namrata: 12/11/2019
                        Utils.getColumnHeader(ucdi.lvDIMap, "Event Class").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Variation").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Event Variation").Width = 0;
                        break;
                    //Namrata: 16/08/2019
                    case SlaveTypes.MQTTSLAVE:
                        ucdi.lvDIMap.Columns[1].Text = "Unit";
                        Utils.getColumnHeader(ucdi.lvDIMap, "DI No.").Width = 80;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Unit").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Reporting Index").Width = 100;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Data Type").Width = 100;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Command Type").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Bit Pos.").Width = 80;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Complement").Width = 80;
                        Utils.getColumnHeader(ucdi.lvDIMap, "CellNo").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Widget").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Used").Width = 0;
                        //Namrata: 21/10/2019
                        Utils.getColumnHeader(ucdi.lvDIMap, "Key").Width = 100;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Description").Width = 100;
                        //Namrata: 12/11/2019
                        Utils.getColumnHeader(ucdi.lvDIMap, "Event Class").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Variation").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Event Variation").Width = 0;
                        break;
                    //Namrata: 21/10/2019
                    case SlaveTypes.SMSSLAVE:
                        ucdi.lvDIMap.Columns[1].Text = "Unit";
                        Utils.getColumnHeader(ucdi.lvDIMap, "DI No.").Width = 80;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Unit").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Reporting Index").Width = 100;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Data Type").Width = 100;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Command Type").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Bit Pos.").Width = 80;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Complement").Width = 80;
                        Utils.getColumnHeader(ucdi.lvDIMap, "CellNo").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Widget").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Used").Width = 0;
                        //Namrata: 21/10/2019
                        Utils.getColumnHeader(ucdi.lvDIMap, "Key").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Description").Width = 100;
                        //Namrata: 12/11/2019
                        Utils.getColumnHeader(ucdi.lvDIMap, "Event Class").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Variation").Width = 0;
                        Utils.getColumnHeader(ucdi.lvDIMap, "Event Variation").Width = 0;
                        break;
                    default:
                        //Utils.getColumnHeader(ucai.lvAIMap, "Event").Width = 0;
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
            rootNode = xmlDoc.CreateElement("DIConfiguration");//rootNode = xmlDoc.CreateElement(rnName);
            xmlDoc.AppendChild(rootNode);
            //Namrata: 17/11/2017
            if (masterType == MasterTypes.Virtual)
            {
                //var myDistinctList = ((diList.GroupBy(i => new { i.Index, i.SubIndex, i.ResponseType }).Select(g => g.First()).ToList())&&); //Distinct items in list
                var myDistinctList = diList.GroupBy(i => new { i.Index, i.SubIndex, i.ResponseType }).Select(g => g.First()).ToList(); //Distinct items in list
                foreach (DI ai in myDistinctList)
                {
                    XmlNode importNode = rootNode.OwnerDocument.ImportNode(ai.exportXMLnode(), true);
                    rootNode.AppendChild(importNode);
                }
            }
            else
            {
                foreach (DI di in diList)
                {
                    XmlNode importNode = rootNode.OwnerDocument.ImportNode(di.exportXMLnode(), true);
                    rootNode.AppendChild(importNode);
                }
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
            rootNode = xmlDoc.CreateElement("DIMap");
            xmlDoc.AppendChild(rootNode);
            List<DIMap> slaveDIMapList;
            if (!slavesDIMapList.TryGetValue(slaveID, out slaveDIMapList))
            {
                Console.WriteLine("##### Slave entries for {0} does not exists", slaveID);
                return rootNode;
            }
            foreach (DIMap dimn in slaveDIMapList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(dimn.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }
            return rootNode;
        }
        public string exportINI(string slaveNum, string slaveID, string element, ref int ctr)
        {
            string iniData = "";
            List<DIMap> slaveDIMapList;
            if (!slavesDIMapList.TryGetValue(slaveID, out slaveDIMapList))
            {
                Console.WriteLine("DI INI: ##### Slave entries for {0} does not exists", slaveID);
                return iniData;
            }
            //IMP: If "DoublePoint", create only single IOA in .INI file...
            Dictionary<string, string> riList = new Dictionary<string, string>();
            foreach (DIMap dimn in slaveDIMapList)
            {
                int ri;
                try
                {
                    ri = Int32.Parse(dimn.ReportingIndex);
                }
                catch (System.FormatException)
                {
                    ri = 0;
                }

                if (dimn.DataType == "DoublePoint" && IsRIinINI(riList, dimn.ReportingIndex)) continue;
                if (!riList.ContainsKey(dimn.ReportingIndex)) //Ajay: 10/01/2019
                {
                    iniData += "DI_" + ctr++ + "=" + Utils.GenerateIndex("DI", Utils.GetDataTypeIndex(dimn.DataType), ri).ToString() + Environment.NewLine;
                    riList.Add(dimn.ReportingIndex, dimn.DataType);
                }
                //Ajay: 10/01/2019
                else
                {
                    MessageBox.Show("Duplicate Reporting Index (" + dimn.ReportingIndex + ") found of DI No " + dimn.DINo, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                //iniData += "DI_" + ctr++ + "=" + Utils.GenerateIndex("DI", Utils.GetDataTypeIndex(dimn.DataType), ri).ToString() + Environment.NewLine;
            }

            return iniData;
        }
        private bool IsRIinINI(Dictionary<string, string> ril, string ri)
        {
            string tmp;
            if (!ril.TryGetValue(ri, out tmp)) return false;
            else if (tmp != "DoublePoint")
            {//Got ri, check if DoublePoint
                return false;
            }
            else return true;
        }
        public void changeCLASequence(int oCLANo, int nCLANo)
        {
            if (oCLANo == nCLANo) return;
            foreach (DI din in diList)
            {
                if (din.ResponseType == "CLA" && din.Index == oCLANo.ToString() && din.SubIndex == "0")
                {
                    din.Index = nCLANo.ToString();
                    if (din.Description == "CLA_" + oCLANo) din.Description = "CLA_" + nCLANo;
                    break;
                }
            }
        }
        public void changeProfileSequence(int oProfileNo, int nProfileNo)
        {
            if (oProfileNo == nProfileNo) return;
            foreach (DI din in diList)
            {
                if (din.ResponseType == "Profile" && din.Index == oProfileNo.ToString() && din.SubIndex == "0")
                {
                    din.Index = nProfileNo.ToString();
                    if (din.Description == "Profile_" + oProfileNo) din.Description = "Profile_" + nProfileNo;
                    break;
                }
            }
        }
        public void changeMDSequence(int oMDNo, int nMDNo)
        {
            if (oMDNo == nMDNo) return;
            foreach (DI din in diList)
            {
                if (din.ResponseType == "MDAlarm" && din.Index == oMDNo.ToString() && din.SubIndex == "0")
                {
                    din.Index = nMDNo.ToString();
                    if (din.Description == "MDAlarm_" + oMDNo) din.Description = "MDAlarm_" + nMDNo;
                    break;
                }
            }
        }
        public void changeDDSequence(int oDDNo, int nDDNo)
        {
            if (oDDNo == nDDNo) return;
            foreach (DI din in diList)
            {
                if (din.ResponseType == "DerivedDI" && din.Index == oDDNo.ToString() && din.SubIndex == "0")
                {
                    din.Index = nDDNo.ToString();
                    if (din.Description == "DerivedDI_" + oDDNo) din.Description = "DerivedDI_" + nDDNo;
                    break;
                }
            }
        }
        public void regenerateDISequence()
        {
            foreach (DI din in diList)
            {
                int oDINo = Int32.Parse(din.DINo);
                int nDINo = Globals.DINo++;
                din.DINo = nDINo.ToString();

                //Now change in map...
                foreach (KeyValuePair<string, List<DIMap>> maps in slavesDIMapList)
                {
                    //Ajay: 10/01/2019 Commented
                    //List<DIMap> sdimList = maps.Value;
                    //foreach (DIMap dim in sdimList)
                    //{
                    //    if (dim.DINo == oDINo.ToString() && !dim.IsReindexed)
                    //    {
                    //        //Ajay: 30/07/2018 if same DI mapped again it should take same DI no on reindex. 
                    //        //dim.DINo = nDINo.ToString();
                    //        //dim.IsReindexed = true;
                    //        sdimList.Where(x => x.DINo == oDINo.ToString()).ToList().ForEach(x => { x.DINo = nDINo.ToString(); x.IsReindexed = true; });
                    //        break;
                    //    }
                    //    if (dim.DINo != oDINo.ToString() && !dim.IsReindexed)
                    //    {
                    //        dim.DINo = nDINo.ToString();
                    //        dim.IsReindexed = true;
                    //        break;
                    //    }
                    //}

                    //Ajay: 10/01/2019 Reindexing issues reported by Aditya K. mail dtd. 27-12-2018
                    maps.Value.OfType<DIMap>().Where(x => x.DINo == oDINo.ToString() && !x.IsReindexed).ToList().ForEach(x => { x.DINo = nDINo.ToString(); x.IsReindexed = true; });
                }
                //Now change in Parameter Load nodes...
                Utils.getOpenProPlusHandle().getParameterLoadConfiguration().ChangeDISequence(oDINo, nDINo);
            }
            //Reset reindexing status, for next use...
            foreach (KeyValuePair<string, List<DIMap>> maps in slavesDIMapList)
            {
                List<DIMap> sdimList = maps.Value;
                foreach (DIMap dim in sdimList)
                {
                    dim.IsReindexed = false;
                }
            }
            refreshList();
            refreshCurrentMapList();
        }
        public void regenerateDISequencne()
        {
            Utils.IsAI = true;
            List<DIMap> sdimList = new List<DIMap>();
            List<DIMap> OrignalList = new List<DIMap>();
            List<DIMap> ReIndexedList = new List<DIMap>();
            foreach (DI din in diList)
            {
                int oDINo = Int32.Parse(din.DINo);
                int nDINo = Globals.DINo++;
                din.DINo = nDINo.ToString();

                //Now change in map...
                foreach (KeyValuePair<string, List<DIMap>> maps in slavesDIMapList)
                {
                    if (OrignalList != null && OrignalList.Count <= 0)
                    {
                        OrignalList = maps.Value;
                    }
                    sdimList = maps.Value;
                    foreach (DIMap dim in sdimList)
                    {
                        if (dim.DINo == oDINo.ToString() && !dim.IsReindexed)
                        {
                            //Namrata:02/05/2018
                            List<DIMap> tmpaimap = OrignalList.Where(x => x.DINo == oDINo.ToString()).Select(x => x).ToList();
                            if (tmpaimap != null && tmpaimap.Count > 0)
                            {
                                tmpaimap.ForEach(itm =>
                                {
                                    itm.DINo = nDINo.ToString();
                                    itm.IsReindexed = true;
                                    ReIndexedList.Add(itm);
                                });
                            }
                            //dim.AINo = nDINo.ToString();
                            //dim.IsReindexed = true;
                            break;
                        }
                        if (dim.DINo != oDINo.ToString() && !dim.IsReindexed)
                        {
                            //Namrata:02/05/2018
                            List<DIMap> tmpaimap = OrignalList.Where(x => x.DINo == oDINo.ToString()).Select(x => x).ToList();
                            if (tmpaimap != null && tmpaimap.Count > 0)
                            {
                                tmpaimap.ForEach(itm =>
                                {
                                    itm.DINo = nDINo.ToString();
                                    itm.IsReindexed = true;
                                    ReIndexedList.Add(itm);
                                });
                            }
                            //dim.AINo = oDINo.ToString(); //nDINo.ToString();
                            //dim.IsReindexed = true;
                            break;
                        }
                    }
                }
                //Now change in Parameter Load nodes...
                Utils.getOpenProPlusHandle().getParameterLoadConfiguration().ChangeDISequence(oDINo, nDINo);
            }
            //Reset reindexing status, for next use...
            foreach (KeyValuePair<string, List<DIMap>> maps in slavesDIMapList)
            {
                sdimList = maps.Value; //List<AIMap> sdimList = maps.Value;
                foreach (DIMap dim in sdimList)
                {
                    dim.IsReindexed = false;
                }
            }
            //Namrata:02/05/2018
            sdimList = ReIndexedList;
            refreshList();
            refreshCurrentMapList();
        }
        public void regenerateDISequence12()
        {
            foreach (DI din in diList)
            {
                int oDINo = Int32.Parse(din.DINo);
                int nDINo = Globals.DINo++;
                din.DINo = nDINo.ToString();

                //Now change in map...
                foreach (KeyValuePair<string, List<DIMap>> maps in slavesDIMapList)
                {
                    List<DIMap> sdimList = maps.Value;
                    foreach (DIMap dim in sdimList)
                    {
                        if (dim.DINo == oDINo.ToString() && !dim.IsReindexed)
                        {
                            dim.DINo = nDINo.ToString();
                            dim.IsReindexed = true;
                            break;
                        }
                    }
                }

                //Now change in Parameter Load nodes...
                Utils.getOpenProPlusHandle().getParameterLoadConfiguration().ChangeDISequence(oDINo, nDINo);
            }
            //Reset reindexing status, for next use...
            foreach (KeyValuePair<string, List<DIMap>> maps in slavesDIMapList)
            {
                List<DIMap> sdimList = maps.Value;
                foreach (DIMap dim in sdimList)
                {
                    dim.IsReindexed = false;
                }
            }
            refreshList();
            refreshCurrentMapList();
        }
        public void regenerateDISequenceoLD()
        {
            foreach (DI din in diList)
            {
                int oDINo = Int32.Parse(din.DINo);
                int nDINo = Globals.DINo++;
                din.DINo = nDINo.ToString();

                //Now change in map...
                foreach (KeyValuePair<string, List<DIMap>> maps in slavesDIMapList)
                {
                    List<DIMap> sdimList = maps.Value;
                    foreach (DIMap dim in sdimList)
                    {
                        if (dim.DINo == oDINo.ToString() && !dim.IsReindexed)
                        {
                            dim.DINo = nDINo.ToString();
                            dim.IsReindexed = true;
                            break;
                        }
                        //if (dim.DINo != oDINo.ToString() && !dim.IsReindexed)
                        //{
                        //    dim.DINo = nDINo.ToString();
                        //    dim.IsReindexed = true;
                        //    break;
                        //}
                    }
                }
                //Now change in Parameter Load nodes...
                Utils.getOpenProPlusHandle().getParameterLoadConfiguration().ChangeDISequence(oDINo, nDINo);
            }
            //Reset reindexing status, for next use...
            foreach (KeyValuePair<string, List<DIMap>> maps in slavesDIMapList)
            {
                List<DIMap> sdimList = maps.Value;
                foreach (DIMap dim in sdimList)
                {
                    dim.IsReindexed = false;
                }
            }
            refreshList();
            refreshCurrentMapList();
        }
        public int GetReportingIndex(string slaveNum, string slaveID, int value)
        {
            int ret = 0;

            List<DIMap> slaveDIMapList;
            if (!slavesDIMapList.TryGetValue(slaveID, out slaveDIMapList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                return ret;
            }

            foreach (DIMap dim in slaveDIMapList)
            {
                if (dim.DINo == value.ToString()) return Int32.Parse(dim.ReportingIndex);
            }

            return ret;
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("DI_"))
            {
                //If a IEC104 slave added/deleted, reflect in UI as well as objects.
                CheckIEC104SlaveStatusChanges();
                //If a MODBUS slave added/deleted, reflect in UI as well as objects.
                CheckMODBUSSlaveStatusChanges();
                CheckIEc61850SlaveStatusChanges();
                //Namrata:13/7/2017
                //If a IEC101 slave added/deleted, reflect in UI as well as objects.
                CheckIEC101SlaveStatusChanges();
                //Ajay: 05/07/2018
                CheckSPORTSlaveStatusChanges();
                CheckMQTTSlaveStatusChanges();
                CheckSMSSlaveStatusChanges();
                ShowHideSlaveColumns();
                //ShowHideSlaveColumnsSPORT();
                CheckGDisplaySlaveStatusChanges();
                CheckDNPSlaveStatusChanges();
                return ucdi;
            }
            return null;
        }
        public void parseDICNode(XmlNode dicNode, bool imported)
        {
            if (dicNode == null)
            {
                rnName = "DIConfiguration";
                return;
            }
            //First set root node name...
            rnName = dicNode.Name;
            if (dicNode.NodeType == XmlNodeType.Comment)
            {
                isNodeComment = true;
                comment = dicNode.Value;
            }
            foreach (XmlNode node in dicNode)
            {
                if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                diList.Add(new DI(node, masterType, masterNo, IEDNo, imported));
                //string message = "DINo"+ node.Attributes[0].Value.ToString()+" "+ "Index"+node.Attributes[2].Value.ToString() + " " + "Description" +node.Attributes[4].Value.ToString();
                //string aaa= node.Attributes[0].Value+node.Attributes[1].Value,node.Attributes[2].Value,node.Attributes[3].Value;
                //csLog.LogError(message + Environment.NewLine);
                //refreshList();
            }
           refreshList();
            
            //Task thDI = new Task(() => refreshList());
            //thDI.Start();
            
        }
        public void parseDIMNode(string slaveNum, string slaveID, XmlNode dimNode)
        {
            CreateNewSlave(slaveNum, slaveID, dimNode);
        }
        public int getCount()
        {
            int ctr = 0;
            foreach (DI diNode in diList)
            {
                if (diNode.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }
        public List<DI> getDIs()
        {
            return diList;
        }
        //Namrata:27/7/2017
        public int getDIMapCount()
        {
            int ctr = 0;
            fillMapOptions(Utils.getSlaveTypes(currentSlave));
            List<DIMap> sdimList;
            if (!slavesDIMapList.TryGetValue(currentSlave, out sdimList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                refreshMapList(null);
            }
            else
            {
                refreshMapList(sdimList);
            }
            if (sdimList == null)
            {
                return 0;
            }
            else
            {
                foreach (DIMap asaa in sdimList)
                {
                    if (asaa.IsNodeComment) continue;
                    ctr++;
                }
            }

            return ctr;
        }
        public bool removeDI(string responseType, int Idx, int subIdx)
        {
            bool removed = false;
            for (int i = 0; i < diList.Count; i++)
            {
                if (diList[i].IsNodeComment) continue;
                DI tmp = diList[i];
                if (tmp.Index == Idx.ToString() && tmp.SubIndex == subIdx.ToString() && tmp.ResponseType == responseType)
                {
                    diList.RemoveAt(i);
                    removed = true;
                    break;
                }
            }
            return removed;
        }
        public List<DIMap> getSlaveDIMaps(string slaveID)
        {
            List<DIMap> slaveDIMapList;
            slavesDIMapList.TryGetValue(slaveID, out slaveDIMapList);
            return slaveDIMapList;
        }
        //Ajay: 06/10/2018
        public bool IsDIExist(string RT, int iIndex)
        {
            return diList.Where(x => x.Index == iIndex.ToString() && x.SubIndex == "0" && x.ResponseType == RT).Any();
        }
        //Ajay: 07/12/2018
        private bool AllowMasterOptionsOnClick(MasterTypes mstrType)
        {
            string strRoutineName = "DIConfiguration: AllowMasterOptionsOnClick";
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
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        //Ajay: 07/12/2018
        private bool AllowSlaveOptionsOnClick(SlaveTypes slvType)
        {
            string strRoutineName = "DIConfiguration: AllowSlaveOptionsOnClick";
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
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        //Namrata:10/04/2019
        private string getMQTTSlaveKey(ListView lstview, string ainno)
        {
            int iColIndex = ucdi.lvDIlist.Columns["Description"].Index;
            var query = lstview.Items
                    .Cast<ListViewItem>()
                    .Where(item => item.SubItems[0].Text == ainno).Select(s => s.SubItems[iColIndex].Text).Single();
            return query.ToString();
        }
        #region Events For All Masters
        public void ADR_OnLoad()
        {
            ucdi.txtDINo.Size = new Size(220, 20);
            ucdi.cmbResponseType.Size = new Size(220, 20);
            ucdi.txtIndex.Size = new Size(220, 20);
            ucdi.txtSubIndex.Size = new Size(220, 20);
            ucdi.CmbDataType.Size = new Size(220, 20);
            ucdi.txtDescription.Size = new Size(220, 20);
            ucdi.txtEvent_T.Size = new Size(220, 20);
            ucdi.txtEvent_F.Size = new Size(220, 20);
            ucdi.cmbIEDName.Size = new Size(220, 20);
            ucdi.cmbResponseType.Size = new Size(220, 20);
            ucdi.ChkIEC61850Index.Size = new Size(220, 20);
            ucdi.txtAutoMap.Size = new Size(220, 20);
            ucdi.cmbFC.Size = new Size(220, 20);

            //Ajay: 02/11/2018
            string strRoutineName = "ADR_OnLoad";
            try
            {
                foreach (Control c in ucdi.grpDI.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucdi.lblDINo.Visible = ucdi.txtDINo.Visible = true;
                ucdi.txtDINo.Enabled = false;
                ucdi.lblDINo.Location = new Point(15, 35);
                ucdi.txtDINo.Location = new Point(102, 30);

                ucdi.lblResponseType.Visible = ucdi.cmbResponseType.Visible = true;
                ucdi.lblResponseType.Location = new Point(15, 60);
                ucdi.cmbResponseType.Location = new Point(102, 55);

                ucdi.lblIndex.Visible = ucdi.txtIndex.Visible = true;
                ucdi.lblIndex.Location = new Point(15, 85);
                ucdi.txtIndex.Location = new Point(102, 80);

                ucdi.lblSubIndex.Visible = ucdi.txtSubIndex.Visible = true;
                ucdi.lblSubIndex.Location = new Point(15, 110);
                ucdi.txtSubIndex.Location = new Point(102, 105);

                ucdi.lblEventTrue.Visible = ucdi.txtEvent_T.Visible = true;
                ucdi.lblEventTrue.Location = new Point(15, 135);
                ucdi.txtEvent_T.Location = new Point(102, 130);

                ucdi.lblEventFalse.Visible = ucdi.txtEvent_F.Visible = true;
                ucdi.lblEventFalse.Location = new Point(15, 160);
                ucdi.txtEvent_F.Location = new Point(102, 155);

                ucdi.lblDescription.Visible = ucdi.txtDescription.Visible = true;
                ucdi.lblDescription.Location = new Point(15, 185);
                ucdi.txtDescription.Location = new Point(102, 180);

                ucdi.lblAutoMap.Visible = ucdi.txtAutoMap.Visible = true;
                ucdi.lblAutoMap.Location = new Point(15, 210);
                ucdi.txtAutoMap.Location = new Point(102, 205);

                ucdi.btnDone.Visible = ucdi.btnCancel.Visible = true;
                ucdi.btnDone.Location = new Point(102, 235);
                ucdi.btnCancel.Location = new Point(205, 235);

                ucdi.grpDI.Height = 275;
                ucdi.grpDI.Width = 330;
                ucdi.grpDI.Location = new Point(172, 52);

            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 03/11/2018
        public void ADR_OnDoubleClick()
        {
            //Ajay: 03/11/2018
            string strRoutineName = "ADR_OnDoubleClick";
            try
            {
                foreach (Control c in ucdi.grpDI.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }
                ucdi.txtDINo.Size = new Size(220, 20);
                ucdi.cmbResponseType.Size = new Size(220, 20);
                ucdi.txtIndex.Size = new Size(220, 20);
                ucdi.txtSubIndex.Size = new Size(220, 20);
                ucdi.CmbDataType.Size = new Size(220, 20);
                ucdi.txtDescription.Size = new Size(220, 20);
                ucdi.txtEvent_T.Size = new Size(220, 20);
                ucdi.txtEvent_F.Size = new Size(220, 20);
                ucdi.cmbIEDName.Size = new Size(220, 20);
                ucdi.cmbResponseType.Size = new Size(220, 20);
                ucdi.ChkIEC61850Index.Size = new Size(220, 20);
                ucdi.txtAutoMap.Size = new Size(220, 20);
                ucdi.cmbFC.Size = new Size(220, 20);

                ucdi.lblDINo.Visible = ucdi.txtDINo.Visible = true;
                ucdi.txtDINo.Enabled = false;
                ucdi.lblDINo.Location = new Point(15, 35);
                ucdi.txtDINo.Location = new Point(102, 30);

                ucdi.lblResponseType.Visible = ucdi.cmbResponseType.Visible = true;
                ucdi.lblResponseType.Location = new Point(15, 60);
                ucdi.cmbResponseType.Location = new Point(102, 55);

                ucdi.lblIndex.Visible = ucdi.txtIndex.Visible = true;
                ucdi.lblIndex.Location = new Point(15, 85);
                ucdi.txtIndex.Location = new Point(102, 80);

                ucdi.lblSubIndex.Visible = ucdi.txtSubIndex.Visible = true;
                ucdi.lblSubIndex.Location = new Point(15, 110);
                ucdi.txtSubIndex.Location = new Point(102, 105);

                ucdi.lblEventTrue.Visible = ucdi.txtEvent_T.Visible = true;
                ucdi.lblEventTrue.Location = new Point(15, 135);
                ucdi.txtEvent_T.Location = new Point(102, 130);

                ucdi.lblEventFalse.Visible = ucdi.txtEvent_F.Visible = true;
                ucdi.lblEventFalse.Location = new Point(15, 160);
                ucdi.txtEvent_F.Location = new Point(102, 155);

                ucdi.lblDescription.Visible = ucdi.txtDescription.Visible = true;
                ucdi.lblDescription.Location = new Point(15, 185);
                ucdi.txtDescription.Location = new Point(102, 180);

                ucdi.btnDone.Visible = ucdi.btnCancel.Visible = true;
                ucdi.btnDone.Location = new Point(102, 210);
                ucdi.btnCancel.Location = new Point(205, 210);

                ucdi.btnFirst.Visible = ucdi.btnPrev.Visible = ucdi.btnNext.Visible = ucdi.btnLast.Visible = true;
                ucdi.btnFirst.Location = new Point(13, 245);
                ucdi.btnPrev.Location = new Point(88, 245);
                ucdi.btnNext.Location = new Point(163, 245);
                ucdi.btnLast.Location = new Point(238, 245);

                ucdi.grpDI.Height = 280;
                ucdi.grpDI.Width = 340;
                ucdi.grpDI.Location = new Point(172, 52);

            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Virtual_OnLoad()
        {
            ucdi.grpDI.Location = new Point(172, 52);
            ucdi.txtAutoMap.Enabled = true;
            ucdi.lblAutoMap.Enabled = true;
            ucdi.txtAutoMap.Visible = true;
            ucdi.lblAutoMap.Visible = true;
            //Namrata: 12/09/2017
            ucdi.txtDescription.Location = new Point(110, 131);
            ucdi.lblDescription.Location = new Point(15, 134);
            ucdi.lblDataType.Visible = false;
            ucdi.CmbDataType.Visible = false;
            ucdi.lblIEDName.Visible = false;
            ucdi.lbl61850DIResponseType.Visible = false;
            ucdi.lbl61850DIIndex.Visible = false;
            ucdi.txtEvent_F.Visible = false;
            ucdi.cmbIEDName.Visible = false;
            ucdi.cmb61850DIIndex.Visible = false;
            ucdi.cmb61850DIResponseType.Visible = false;
            ucdi.lblEventTrue.Visible = false;
            ucdi.lblEventFalse.Visible = false;
            ucdi.txtEvent_T.Visible = false;
            ucdi.txtEvent_F.Visible = false;
            ucdi.txtAutoMap.Location = new Point(110, 157);
            ucdi.lblAutoMap.Location = new Point(15, 160);
            ucdi.btnDone.Location = new Point(110, 185);
            ucdi.btnCancel.Location = new Point(214, 185);
            ucdi.btnFirst.Location = new Point(15, 220);
            ucdi.btnPrev.Location = new Point(87, 220);
            ucdi.btnNext.Location = new Point(189, 220);
            ucdi.btnLast.Location = new Point(231, 220);
            ucdi.grpDI.Location = new Point(250, 53);
            ucdi.grpDI.Height = 225;
            ucdi.lvDIlistDoubleClick += new System.EventHandler(this.lvDIlist_DoubleClick);
        }
        public void Virtual_OnDoubleClick()
        {
            ucdi.ChkEvent.Visible = false; //Ajay: 18/07/2018
            ucdi.lblDescription.Location = new Point(15, 134);
            ucdi.txtDescription.Location = new Point(110, 131);
            ucdi.lblDataType.Visible = false;
            ucdi.CmbDataType.Visible = false;
            ucdi.lblIEDName.Visible = false;
            ucdi.lbl61850DIResponseType.Visible = false;
            ucdi.lbl61850DIIndex.Visible = false;
            ucdi.txtEvent_F.Visible = false;
            ucdi.cmbIEDName.Visible = false;
            ucdi.cmb61850DIIndex.Visible = false;
            ucdi.cmb61850DIResponseType.Visible = false;
            ucdi.lblAutoMap.Visible = false;
            ucdi.txtAutoMap.Visible = false;
            ucdi.lblEventTrue.Visible = false;
            ucdi.lblEventFalse.Visible = false;
            ucdi.txtEvent_T.Visible = false;
            ucdi.txtEvent_F.Visible = false;
            ucdi.grpDI.Location = new Point(250, 53);
            ucdi.btnDone.Location = new Point(110, 160);
            ucdi.btnCancel.Location = new Point(214, 160);
            ucdi.btnFirst.Location = new Point(15, 195);
            ucdi.btnPrev.Location = new Point(87, 195);
            ucdi.btnNext.Location = new Point(159, 195);
            ucdi.btnLast.Location = new Point(231, 195);
            ucdi.grpDI.Height = 220;
        }
        //Ajay: 02/11/2018
        public void MODBUS_OnLoad()
        {
            //Ajay: 02/11/2018
            string strRoutineName = "MODBUS_OnLoad";
            try
            {
                foreach (Control c in ucdi.grpDI.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }
                ucdi.txtDINo.Size = new Size(220, 20);
                ucdi.cmbResponseType.Size = new Size(220, 20);
                ucdi.txtIndex.Size = new Size(220, 20);
                ucdi.txtSubIndex.Size = new Size(220, 20);
                ucdi.CmbDataType.Size = new Size(220, 20);
                ucdi.txtDescription.Size = new Size(220, 20);
                ucdi.txtEvent_T.Size = new Size(220, 20);
                ucdi.txtEvent_F.Size = new Size(220, 20);
                ucdi.cmbIEDName.Size = new Size(220, 20);
                ucdi.cmbResponseType.Size = new Size(220, 20);
                ucdi.ChkIEC61850Index.Size = new Size(220, 20);
                ucdi.txtAutoMap.Size = new Size(220, 20);
                ucdi.cmbFC.Size = new Size(220, 20);

                ucdi.lblDINo.Visible = ucdi.txtDINo.Visible = true;
                ucdi.txtDINo.Enabled = false;
                ucdi.lblDINo.Location = new Point(15, 35);
                ucdi.txtDINo.Location = new Point(102, 30);

                ucdi.lblResponseType.Visible = ucdi.cmbResponseType.Visible = true;
                ucdi.lblResponseType.Location = new Point(15, 60);
                ucdi.cmbResponseType.Location = new Point(102, 55);

                ucdi.lblIndex.Visible = ucdi.txtIndex.Visible = true;
                ucdi.lblIndex.Location = new Point(15, 85);
                ucdi.txtIndex.Location = new Point(102, 80);

                ucdi.lblSubIndex.Visible = ucdi.txtSubIndex.Visible = true;
                ucdi.lblSubIndex.Location = new Point(15, 110);
                ucdi.txtSubIndex.Location = new Point(102, 105);

                ucdi.lblDataType.Visible = ucdi.CmbDataType.Visible = true;
                ucdi.lblDataType.Location = new Point(15, 135);
                ucdi.CmbDataType.Location = new Point(102, 130);

                ucdi.lblDescription.Visible = ucdi.txtDescription.Visible = true;
                ucdi.lblDescription.Location = new Point(15, 160);
                ucdi.txtDescription.Location = new Point(102, 155);

                ucdi.lblAutoMap.Visible = ucdi.txtAutoMap.Visible = true;
                ucdi.lblAutoMap.Location = new Point(15, 185);
                ucdi.txtAutoMap.Location = new Point(102, 180);

                ucdi.btnDone.Visible = ucdi.btnCancel.Visible = true;
                ucdi.btnDone.Location = new Point(102, 210);
                ucdi.btnCancel.Location = new Point(205, 210);

                ucdi.grpDI.Height = 250;
                ucdi.grpDI.Width = 340;
                ucdi.grpDI.Location = new Point(172, 52);

            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 03/11/2018
        public void MODBUS_OnDoubleClick()
        {
            //Ajay: 03/11/2018
            string strRoutineName = "MODBUS_OnDoubleClick";
            try
            {
                foreach (Control c in ucdi.grpDI.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }
                ucdi.txtDINo.Size = new Size(220, 20);
                ucdi.cmbResponseType.Size = new Size(220, 20);
                ucdi.txtIndex.Size = new Size(220, 20);
                ucdi.txtSubIndex.Size = new Size(220, 20);
                ucdi.CmbDataType.Size = new Size(220, 20);
                ucdi.txtDescription.Size = new Size(220, 20);
                ucdi.txtEvent_T.Size = new Size(220, 20);
                ucdi.txtEvent_F.Size = new Size(220, 20);
                ucdi.cmbIEDName.Size = new Size(220, 20);
                ucdi.cmbResponseType.Size = new Size(220, 20);
                ucdi.ChkIEC61850Index.Size = new Size(220, 20);
                ucdi.txtAutoMap.Size = new Size(220, 20);
                ucdi.cmbFC.Size = new Size(220, 20);

                ucdi.lblDINo.Visible = ucdi.txtDINo.Visible = true;
                ucdi.txtDINo.Enabled = false;
                ucdi.lblDINo.Location = new Point(15, 35);
                ucdi.txtDINo.Location = new Point(102, 30);

                ucdi.lblResponseType.Visible = ucdi.cmbResponseType.Visible = true;
                ucdi.lblResponseType.Location = new Point(15, 60);
                ucdi.cmbResponseType.Location = new Point(102, 55);

                ucdi.lblIndex.Visible = ucdi.txtIndex.Visible = true;
                ucdi.lblIndex.Location = new Point(15, 85);
                ucdi.txtIndex.Location = new Point(102, 80);

                ucdi.lblSubIndex.Visible = ucdi.txtSubIndex.Visible = true;
                ucdi.lblSubIndex.Location = new Point(15, 110);
                ucdi.txtSubIndex.Location = new Point(102, 105);

                ucdi.lblDataType.Visible = ucdi.CmbDataType.Visible = true;
                ucdi.lblDataType.Location = new Point(15, 135);
                ucdi.CmbDataType.Location = new Point(102, 130);

                ucdi.lblDescription.Visible = ucdi.txtDescription.Visible = true;
                ucdi.lblDescription.Location = new Point(15, 160);
                ucdi.txtDescription.Location = new Point(102, 155);

                ucdi.btnDone.Visible = ucdi.btnCancel.Visible = true;
                ucdi.btnDone.Location = new Point(102, 185);
                ucdi.btnCancel.Location = new Point(205, 185);

                ucdi.btnFirst.Visible = ucdi.btnPrev.Visible = ucdi.btnNext.Visible = ucdi.btnLast.Visible = true;
                ucdi.btnFirst.Location = new Point(13, 220);
                ucdi.btnPrev.Location = new Point(88, 220);
                ucdi.btnNext.Location = new Point(163, 220);
                ucdi.btnLast.Location = new Point(238, 220);

                ucdi.grpDI.Height = 255;
                ucdi.grpDI.Width = 340;
                ucdi.grpDI.Location = new Point(172, 52);

            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 31/07/2018
        public void LoadProfile_OnLoad()
        {
            string strRoutineName = "LoadProfileOnLoad";
            try
            {
                foreach (Control c in ucdi.grpDI.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                    ucdi.txtDINo.Size = new Size(220, 20);
                    ucdi.cmbResponseType.Size = new Size(220, 20);
                    ucdi.txtIndex.Size = new Size(220, 20);
                    ucdi.txtSubIndex.Size = new Size(220, 20);
                    ucdi.CmbDataType.Size = new Size(220, 20);
                    ucdi.txtDescription.Size = new Size(220, 20);
                    ucdi.txtEvent_T.Size = new Size(220, 20);
                    ucdi.txtEvent_F.Size = new Size(220, 20);
                    ucdi.cmbIEDName.Size = new Size(220, 20);
                    ucdi.cmbResponseType.Size = new Size(220, 20);
                    ucdi.ChkIEC61850Index.Size = new Size(220, 20);
                    ucdi.txtAutoMap.Size = new Size(220, 20);
                    ucdi.cmbFC.Size = new Size(220, 20);

                    ucdi.lblDINo.Visible = ucdi.txtDINo.Visible = true;
                    ucdi.lblDINo.Location = new Point(15, 35);
                    ucdi.txtDINo.Location = new Point(102, 30);

                    ucdi.lblName.Visible = ucdi.cmbName.Visible = true;
                    ucdi.lblName.Location = new Point(15, 60);
                    ucdi.cmbName.Location = new Point(102, 55);
                    ucdi.cmbName.Size = new Size(220, 20);
                    ucdi.lblDI1.Visible = ucdi.cmbDI1.Visible = true;
                    ucdi.lblDI1.Location = new Point(15, 85);
                    ucdi.cmbDI1.Location = new Point(102, 80);
                    ucdi.cmbDI1.Size = new Size(220, 20);

                    ucdi.lblConstant.Visible = ucdi.txtConstant.Visible = true;
                    ucdi.lblConstant.Location = new Point(15, 110);
                    ucdi.txtConstant.Location = new Point(102, 105); ucdi.txtConstant.Size = new Size(220, 20);

                    ucdi.lblDescription.Visible = ucdi.txtDescription.Visible = true;
                    ucdi.lblDescription.Location = new Point(15, 135);
                    ucdi.txtDescription.Location = new Point(102, 130);

                    //ucdi.chkbxLogEnable.Visible = true;
                    ucdi.chkbxLogEnable.Visible = false; //Ajay: 22/09/2018 LogEnbale not required
                    ucdi.chkbxLogEnable.Checked = false;
                    ucdi.chkbxLogEnable.Location = new Point(102, 165);

                    ucdi.btnDone.Visible = ucdi.btnCancel.Visible = ucdi.btnVerify.Visible = true;
                    ucdi.btnVerify.Location = new Point(15, 165);
                    ucdi.btnDone.Location = new Point(130, 165);
                    ucdi.btnCancel.Location = new Point(240, 165);

                    ucdi.grpDI.Height = 205;
                    ucdi.grpDI.Width = 335;
                    ucdi.grpDI.Location = new Point(172, 52);

                    ucdi.lvDIlistDoubleClick += new System.EventHandler(this.lvDIlist_DoubleClick);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 31/07/2018
        public void LoadProfile_OnDoubleClick()
        {
            string strRoutineName = "LoadProfileOnDoubleClick";
            try
            {
                foreach (Control c in ucdi.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }
                ucdi.txtDINo.Size = new Size(220, 20);
                ucdi.cmbResponseType.Size = new Size(220, 20);
                ucdi.txtIndex.Size = new Size(220, 20);
                ucdi.txtSubIndex.Size = new Size(220, 20);
                ucdi.CmbDataType.Size = new Size(220, 20);
                ucdi.txtDescription.Size = new Size(220, 20);
                ucdi.txtEvent_T.Size = new Size(220, 20);
                ucdi.txtEvent_F.Size = new Size(220, 20);
                ucdi.cmbIEDName.Size = new Size(220, 20);
                ucdi.cmbResponseType.Size = new Size(220, 20);
                ucdi.ChkIEC61850Index.Size = new Size(220, 20);
                ucdi.txtAutoMap.Size = new Size(220, 20);
                ucdi.cmbFC.Size = new Size(220, 20);

                ucdi.lblDINo.Visible = ucdi.txtDINo.Visible = true;
                ucdi.lblDINo.Location = new Point(15, 35);
                ucdi.txtDINo.Location = new Point(102, 30);

                ucdi.lblName.Visible = ucdi.cmbName.Visible = true;
                ucdi.lblName.Location = new Point(15, 60);
                ucdi.cmbName.Location = new Point(102, 55);

                ucdi.lblDI1.Visible = ucdi.cmbDI1.Visible = true;
                ucdi.lblDI1.Location = new Point(15, 85);
                ucdi.cmbDI1.Location = new Point(102, 80);

                ucdi.lblConstant.Visible = ucdi.txtConstant.Visible = true;
                ucdi.lblConstant.Location = new Point(15, 110);
                ucdi.txtConstant.Location = new Point(102, 105);

                ucdi.lblDescription.Visible = ucdi.txtDescription.Visible = true;
                ucdi.lblDescription.Location = new Point(15, 135);
                ucdi.txtDescription.Location = new Point(102, 130);

                ucdi.chkbxLogEnable.Visible = true;
                ucdi.chkbxLogEnable.Checked = false;
                ucdi.chkbxLogEnable.Location = new Point(102, 165);

                ucdi.btnDone.Visible = ucdi.btnCancel.Visible = ucdi.btnVerify.Visible = true;
                ucdi.btnVerify.Location = new Point(15, 195);
                ucdi.btnDone.Location = new Point(130, 195);
                ucdi.btnCancel.Location = new Point(240, 195);

                ucdi.btnFirst.Visible = ucdi.btnPrev.Visible = ucdi.btnNext.Visible = ucdi.btnLast.Visible = true;
                ucdi.btnFirst.Location = new Point(13, 230);
                ucdi.btnPrev.Location = new Point(88, 230);
                ucdi.btnNext.Location = new Point(163, 230);
                ucdi.btnLast.Location = new Point(238, 230);

                ucdi.grpDI.Height = 260;
                ucdi.grpDI.Location = new Point(172, 52);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void IEC101_IEC103_SPORT_OnLoad()
        {
            //Ajay: 02/11/2018
            string strRoutineName = "IEC101_IEC103_SPORT_OnLoad";
            try
            {
                foreach (Control c in ucdi.grpDI.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucdi.txtDINo.Size = new Size(220, 20);
                ucdi.cmbResponseType.Size = new Size(220, 20);
                ucdi.txtIndex.Size = new Size(220, 20);
                ucdi.txtSubIndex.Size = new Size(220, 20);
                ucdi.CmbDataType.Size = new Size(220, 20);
                ucdi.txtDescription.Size = new Size(220, 20);
                ucdi.txtEvent_T.Size = new Size(220, 20);
                ucdi.txtEvent_F.Size = new Size(220, 20);
                ucdi.cmbIEDName.Size = new Size(220, 20);
                ucdi.cmbResponseType.Size = new Size(220, 20);
                ucdi.ChkIEC61850Index.Size = new Size(220, 20);
                ucdi.txtAutoMap.Size = new Size(220, 20);
                ucdi.cmbFC.Size = new Size(220, 20);

                ucdi.lblDINo.Visible = ucdi.txtDINo.Visible = true;
                ucdi.txtDINo.Enabled = false;
                ucdi.lblDINo.Location = new Point(15, 35);
                ucdi.txtDINo.Location = new Point(102, 30);


                ucdi.lblResponseType.Visible = ucdi.cmbResponseType.Visible = true;
                ucdi.lblResponseType.Location = new Point(15, 60);
                ucdi.cmbResponseType.Location = new Point(102, 55);

                ucdi.lblIndex.Visible = ucdi.txtIndex.Visible = true;
                ucdi.lblIndex.Location = new Point(15, 85);
                ucdi.txtIndex.Location = new Point(102, 80);

                ucdi.lblSubIndex.Visible = ucdi.txtSubIndex.Visible = true;
                ucdi.lblSubIndex.Location = new Point(15, 110);
                ucdi.txtSubIndex.Location = new Point(102, 105);

                ucdi.lblDescription.Visible = ucdi.txtDescription.Visible = true;
                ucdi.lblDescription.Location = new Point(15, 135);
                ucdi.txtDescription.Location = new Point(102, 130);

                ucdi.lblAutoMap.Visible = ucdi.txtAutoMap.Visible = true;
                ucdi.lblAutoMap.Location = new Point(15, 160);
                ucdi.txtAutoMap.Location = new Point(102, 155);

                ucdi.btnDone.Visible = ucdi.btnCancel.Visible = true;
                ucdi.btnDone.Location = new Point(102, 185);
                ucdi.btnCancel.Location = new Point(205, 185);

                ucdi.grpDI.Height = 225;
                ucdi.grpDI.Width = 340;
                ucdi.grpDI.Location = new Point(172, 52);
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
            string strRoutineName = "IEC104_OnLoad";
            try
            {

                foreach (Control c in ucdi.grpDI.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }
                ucdi.txtDINo.Size = new Size(220, 20);
                ucdi.cmbResponseType.Size = new Size(220, 20);
                ucdi.txtIndex.Size = new Size(220, 20);
                ucdi.txtSubIndex.Size = new Size(220, 20);
                ucdi.CmbDataType.Size = new Size(220, 20);
                ucdi.txtDescription.Size = new Size(220, 20);
                ucdi.txtEvent_T.Size = new Size(220, 20);
                ucdi.txtEvent_F.Size = new Size(220, 20);
                ucdi.cmbIEDName.Size = new Size(220, 20);
                ucdi.cmbResponseType.Size = new Size(220, 20);
                ucdi.ChkIEC61850Index.Size = new Size(220, 20);
                ucdi.txtAutoMap.Size = new Size(220, 20);
                ucdi.cmbFC.Size = new Size(220, 20);

                ucdi.lblDINo.Visible = ucdi.txtDINo.Visible = true;
                ucdi.txtDINo.Enabled = false;
                ucdi.lblDINo.Location = new Point(15, 35);
                ucdi.txtDINo.Location = new Point(102, 30);

                ucdi.lblResponseType.Visible = ucdi.cmbResponseType.Visible = true;
                ucdi.lblResponseType.Location = new Point(15, 60);
                ucdi.cmbResponseType.Location = new Point(102, 55);

                ucdi.lblIndex.Visible = ucdi.txtIndex.Visible = true;
                ucdi.lblIndex.Location = new Point(15, 85);
                ucdi.txtIndex.Location = new Point(102, 80);

                ucdi.lblSubIndex.Visible = ucdi.txtSubIndex.Visible = true;
                ucdi.lblSubIndex.Location = new Point(15, 110);
                ucdi.txtSubIndex.Location = new Point(102, 105);

                ucdi.lblDescription.Visible = ucdi.txtDescription.Visible = true;
                ucdi.lblDescription.Location = new Point(15, 135);
                ucdi.txtDescription.Location = new Point(102, 130);

                ucdi.lblAutoMap.Visible = ucdi.txtAutoMap.Visible = true;
                ucdi.lblAutoMap.Location = new Point(15, 160);
                ucdi.txtAutoMap.Location = new Point(102, 155);

                ucdi.btnDone.Visible = ucdi.btnCancel.Visible = true;
                ucdi.btnDone.Location = new Point(102, 185);
                ucdi.btnCancel.Location = new Point(205, 185);

                ucdi.grpDI.Height = 225;
                ucdi.grpDI.Width = 340;
                ucdi.grpDI.Location = new Point(172, 52);

            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 03/11/2018
        public void IEC104_OnDoubleClick()
        {
            //Ajay: 03/11/2018
            string strRoutineName = "IEC104_OnDoubleClick";
            try
            {
                foreach (Control c in ucdi.grpDI.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }
                ucdi.txtDINo.Size = new Size(220, 20);
                ucdi.cmbResponseType.Size = new Size(220, 20);
                ucdi.txtIndex.Size = new Size(220, 20);
                ucdi.txtSubIndex.Size = new Size(220, 20);
                ucdi.CmbDataType.Size = new Size(220, 20);
                ucdi.txtDescription.Size = new Size(220, 20);
                ucdi.txtEvent_T.Size = new Size(220, 20);
                ucdi.txtEvent_F.Size = new Size(220, 20);
                ucdi.cmbIEDName.Size = new Size(220, 20);
                ucdi.cmbResponseType.Size = new Size(220, 20);
                ucdi.ChkIEC61850Index.Size = new Size(220, 20);
                ucdi.txtAutoMap.Size = new Size(220, 20);
                ucdi.cmbFC.Size = new Size(220, 20);

                ucdi.lblDINo.Visible = ucdi.txtDINo.Visible = true;
                ucdi.txtDINo.Enabled = false;
                ucdi.lblDINo.Location = new Point(15, 35);
                ucdi.txtDINo.Location = new Point(102, 30);

                ucdi.lblResponseType.Visible = ucdi.cmbResponseType.Visible = true;
                ucdi.lblResponseType.Location = new Point(15, 60);
                ucdi.cmbResponseType.Location = new Point(102, 55);

                ucdi.lblIndex.Visible = ucdi.txtIndex.Visible = true;
                ucdi.lblIndex.Location = new Point(15, 85);
                ucdi.txtIndex.Location = new Point(102, 80);

                ucdi.lblSubIndex.Visible = ucdi.txtSubIndex.Visible = true;
                ucdi.lblSubIndex.Location = new Point(15, 110);
                ucdi.txtSubIndex.Location = new Point(102, 105);

                ucdi.lblDescription.Visible = ucdi.txtDescription.Visible = true;
                ucdi.lblDescription.Location = new Point(15, 135);
                ucdi.txtDescription.Location = new Point(102, 130);

                ucdi.btnDone.Visible = ucdi.btnCancel.Visible = true;
                ucdi.btnDone.Location = new Point(102, 160);
                ucdi.btnCancel.Location = new Point(205, 160);

                ucdi.btnFirst.Visible = ucdi.btnPrev.Visible = ucdi.btnNext.Visible = ucdi.btnLast.Visible = true;
                ucdi.btnFirst.Location = new Point(13, 195);
                ucdi.btnPrev.Location = new Point(88, 195);
                ucdi.btnNext.Location = new Point(163, 195);
                ucdi.btnLast.Location = new Point(238, 195);

                ucdi.grpDI.Height = 230;
                ucdi.grpDI.Width = 340;
                ucdi.grpDI.Location = new Point(172, 52);

            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 03/11/2018
        public void IEC101_IEC103_SPORT_OnDoubleClick()
        {
            //Ajay: 03/11/2018
            string strRoutineName = "IEC101_IEC103_SPORT_OnDoubleClick";
            try
            {
                foreach (Control c in ucdi.grpDI.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }
                ucdi.txtDINo.Size = new Size(220, 20);
                ucdi.cmbResponseType.Size = new Size(220, 20);
                ucdi.txtIndex.Size = new Size(220, 20);
                ucdi.txtSubIndex.Size = new Size(220, 20);
                ucdi.CmbDataType.Size = new Size(220, 20);
                ucdi.txtDescription.Size = new Size(220, 20);
                ucdi.txtEvent_T.Size = new Size(220, 20);
                ucdi.txtEvent_F.Size = new Size(220, 20);
                ucdi.cmbIEDName.Size = new Size(220, 20);
                ucdi.cmbResponseType.Size = new Size(220, 20);
                ucdi.ChkIEC61850Index.Size = new Size(220, 20);
                ucdi.txtAutoMap.Size = new Size(220, 20);
                ucdi.cmbFC.Size = new Size(220, 20);

                ucdi.lblDINo.Visible = ucdi.txtDINo.Visible = true;
                ucdi.txtDINo.Enabled = false;
                ucdi.lblDINo.Location = new Point(15, 35);
                ucdi.txtDINo.Location = new Point(102, 30);

                ucdi.lblResponseType.Visible = ucdi.cmbResponseType.Visible = true;
                ucdi.lblResponseType.Location = new Point(15, 60);
                ucdi.cmbResponseType.Location = new Point(102, 55);

                ucdi.lblIndex.Visible = ucdi.txtIndex.Visible = true;
                ucdi.lblIndex.Location = new Point(15, 85);
                ucdi.txtIndex.Location = new Point(102, 80);

                ucdi.lblSubIndex.Visible = ucdi.txtSubIndex.Visible = true;
                ucdi.lblSubIndex.Location = new Point(15, 110);
                ucdi.txtSubIndex.Location = new Point(102, 105);
                ucdi.lblDescription.Visible = ucdi.txtDescription.Visible = true;
                ucdi.lblDescription.Location = new Point(15, 135);
                ucdi.txtDescription.Location = new Point(102, 130);

                ucdi.btnDone.Visible = ucdi.btnCancel.Visible = true;
                ucdi.btnDone.Location = new Point(102, 160);
                ucdi.btnCancel.Location = new Point(205, 160);

                ucdi.btnFirst.Visible = ucdi.btnPrev.Visible = ucdi.btnNext.Visible = ucdi.btnLast.Visible = true;
                ucdi.btnFirst.Location = new Point(13, 195);
                ucdi.btnPrev.Location = new Point(88, 195);
                ucdi.btnNext.Location = new Point(163, 195);
                ucdi.btnLast.Location = new Point(238, 195);

                ucdi.grpDI.Height = 230;
                ucdi.grpDI.Width = 340;
                ucdi.grpDI.Location = new Point(172, 52);

            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void IEC61850Client_OnLoad()
        {
            string strRoutineName = "DIConfiguration: IEC61850Client_OnLoad";
            try
            {
                foreach (Control c in ucdi.grpDI.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucdi.lblDINo.Visible = ucdi.txtDINo.Visible = true;
                ucdi.txtDINo.Enabled = false;
                ucdi.lblDINo.Location = new Point(15, 35);
                ucdi.txtDINo.Location = new Point(102, 30);
                ucdi.txtDINo.Size = new Size(300, 21);

                ucdi.lblIEDName.Visible = ucdi.cmbIEDName.Visible = true;
                ucdi.lblIEDName.Location = new Point(15, 60);
                ucdi.cmbIEDName.Location = new Point(102, 55);
                ucdi.cmbIEDName.Size = new Size(300, 21);

                ucdi.lbl61850DIResponseType.Visible = ucdi.cmb61850DIResponseType.Visible = true;
                ucdi.lbl61850DIResponseType.Location = new Point(15, 85);
                ucdi.cmb61850DIResponseType.Location = new Point(102, 80);
                ucdi.cmb61850DIResponseType.Size = new Size(300, 21);

                ucdi.lblFC.Visible = ucdi.cmbFC.Visible = true;
                //ucdi.txtFC.Enabled = false;
                ucdi.lblFC.Location = new Point(15, 110);
                ucdi.cmbFC.Location = new Point(102, 105);
                ucdi.cmbFC.Size = new Size(300, 21);

                ucdi.lbl61850DIIndex.Visible = ucdi.cmb61850DIIndex.Visible = false;
                ucdi.lbl61850DIIndex.Location = new Point(15, 135);
                ucdi.cmb61850DIIndex.Location = new Point(102, 130);
                ucdi.cmb61850DIIndex.Size = new Size(300, 21);

                //Namrata:27/03/2019
                ucdi.lbl61850DIIndex.Visible = ucdi.ChkIEC61850Index.Visible = true;
                ucdi.lbl61850DIIndex.Location = new Point(15, 135);
                ucdi.ChkIEC61850Index.Location = new Point(102, 130);
                ucdi.ChkIEC61850Index.Size = new Size(300, 21);

                ucdi.lblSubIndex.Visible = ucdi.txtSubIndex.Visible = true;
                ucdi.lblSubIndex.Location = new Point(15, 160);
                ucdi.txtSubIndex.Location = new Point(102, 155);
                ucdi.txtSubIndex.Size = new Size(300, 21);

                ucdi.lblDescription.Visible = ucdi.txtDescription.Visible = true;
                ucdi.lblDescription.Location = new Point(15, 185);
                ucdi.txtDescription.Location = new Point(102, 180);
                ucdi.txtDescription.Size = new Size(300, 21);

                ucdi.lblAutoMap.Visible = ucdi.txtAutoMap.Visible = false;
                ucdi.lblAutoMap.Location = new Point(15, 210);
                ucdi.txtAutoMap.Location = new Point(102, 205);
                ucdi.txtAutoMap.Size = new Size(300, 21);

                ucdi.btnDone.Visible = ucdi.btnCancel.Visible = true;
                ucdi.btnDone.Location = new Point(135, 210);
                ucdi.btnCancel.Location = new Point(235, 210);

                ucdi.grpDI.Height = 250;
                ucdi.grpDI.Width = 420;
                ucdi.grpDI.Location = new Point(172, 52);
                ucdi.pbHdr.Width = 510;

            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void IEC61850Client_OnDoubleClick()
        {
            string strRoutineName = "IEC61850_OnDoubleClick";
            try
            {
                foreach (Control c in ucdi.grpDI.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucdi.lblDINo.Visible = ucdi.txtDINo.Visible = true;
                ucdi.txtDINo.Enabled = false;
                ucdi.lblDINo.Location = new Point(15, 35);
                ucdi.txtDINo.Location = new Point(102, 30);
                ucdi.txtDINo.Size = new Size(300, 21);

                ucdi.lblIEDName.Visible = ucdi.cmbIEDName.Visible = true;
                ucdi.lblIEDName.Location = new Point(15, 60);
                ucdi.cmbIEDName.Location = new Point(102, 55);
                ucdi.cmbIEDName.Size = new Size(300, 21);

                ucdi.lbl61850DIResponseType.Visible = ucdi.cmb61850DIResponseType.Visible = true;
                ucdi.lbl61850DIResponseType.Location = new Point(15, 85);
                ucdi.cmb61850DIResponseType.Location = new Point(102, 80);
                ucdi.cmb61850DIResponseType.Size = new Size(300, 21);

                ucdi.lblFC.Visible = ucdi.cmbFC.Visible = true;
                ucdi.cmbFC.Enabled = true;// = false;
                ucdi.lblFC.Location = new Point(15, 110);
                ucdi.cmbFC.Location = new Point(102, 105);
                ucdi.cmbFC.Size = new Size(300, 21);

                ucdi.lbl61850DIIndex.Visible = ucdi.cmb61850DIIndex.Visible = false;
                ucdi.lbl61850DIIndex.Location = new Point(15, 135);
                ucdi.cmb61850DIIndex.Location = new Point(102, 130);
                ucdi.cmb61850DIIndex.Size = new Size(300, 21);

                //Namrata:27/03/2019
                ucdi.lbl61850DIIndex.Visible = ucdi.ChkIEC61850Index.Visible = true;
                ucdi.lbl61850DIIndex.Location = new Point(15, 135);
                ucdi.ChkIEC61850Index.Location = new Point(102, 130);
                ucdi.ChkIEC61850Index.Size = new Size(300, 21);


                ucdi.lblSubIndex.Visible = ucdi.txtSubIndex.Visible = true;
                ucdi.lblSubIndex.Location = new Point(15, 160);
                ucdi.txtSubIndex.Location = new Point(102, 155);
                ucdi.txtSubIndex.Size = new Size(300, 21);

                ucdi.lblDescription.Visible = ucdi.txtDescription.Visible = true;
                ucdi.lblDescription.Location = new Point(15, 185);
                ucdi.txtDescription.Location = new Point(102, 180);
                ucdi.txtDescription.Size = new Size(300, 21);

                ucdi.btnDone.Visible = ucdi.btnCancel.Visible = true;
                ucdi.btnDone.Location = new Point(135, 210);
                ucdi.btnCancel.Location = new Point(235, 210);

                ucdi.btnFirst.Visible = ucdi.btnPrev.Visible = ucdi.btnNext.Visible = ucdi.btnLast.Visible = true;
                ucdi.btnFirst.Location = new Point(90, 240);
                ucdi.btnPrev.Location = new Point(160, 240);
                ucdi.btnNext.Location = new Point(230, 240);
                ucdi.btnLast.Location = new Point(290, 240);

                ucdi.grpDI.Height = 270;
                ucdi.grpDI.Width = 450;
                ucdi.grpDI.Location = new Point(172, 52);
                ucdi.pbHdr.Width = 510;

            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion Events For All Masters

        #region Events For All Slaves
        public void MQTTSlave_OnDoubleClick()
        {
            string strRoutineName = "EnableMappingEvents";
            try
            {
                ucdi.lblEV.Visible = false;
                ucdi.CmbEV.Visible = false;
                ucdi.lblEventClass.Visible = false;
                ucdi.cmbEventC.Visible = false;
                ucdi.lblVariation.Visible = false;
                ucdi.CmbVari.Visible = false;

                //Namrata: 20/08/2019
                #region CellNo
                ucdi.CmbCellNo.Visible = false;
                ucdi.lblCellNo.Visible = false;
                #endregion CellNo

                #region Widget
                ucdi.CmbWidget.Visible = false;
                ucdi.lblWidget.Visible = false;
                #endregion Widget
                ucdi.grpDIMap.Location = new Point(234, 315);//new Point(450, 325);
                ucdi.grpDIMap.Size = new Size(360, 250);
                ucdi.pbDIMHdr.Size = new Size(360, 22);

                ucdi.lblDNM.Location = new Point(12, 30);
                ucdi.txtDIMNo.Location = new Point(122, 30);
                ucdi.txtDIMNo.Size = new Size(220, 20);

                ucdi.lblDMRI.Visible = true;
                ucdi.txtDIMReportingIndex.Visible = true;
                ucdi.lblDMRI.Location = new Point(12, 55);
                ucdi.txtDIMReportingIndex.Location = new Point(121, 55);
                ucdi.txtDIMReportingIndex.Size = new Size(220, 20);

                ucdi.lbl61850reporting.Visible = false;
                ucdi.CmbReportingIndex.Visible = false;

                ucdi.lblDMDT.Location = new Point(12, 81);
                ucdi.cmbDIMDataType.Location = new Point(121, 81);
                ucdi.cmbDIMDataType.Size = new Size(220, 20);

                ucdi.lblDMCT.Visible = false;
                ucdi.cmbDIMCommandType.Visible = false;
                ucdi.lblDMCT.Location = new Point(12, 108);
                ucdi.cmbDIMCommandType.Location = new Point(121, 108);
                ucdi.cmbDIMCommandType.Size = new Size(220, 20);


                ucdi.lblDMBP.Visible = true;
                ucdi.txtDIMBitPos.Visible = true;
                ucdi.lblDMBP.Location = new Point(12, 108);
                ucdi.txtDIMBitPos.Location = new Point(121, 108);
                ucdi.txtDIMBitPos.Size = new Size(220, 20);


                //Key
                ucdi.lblKey.Location = new Point(12, 134);
                ucdi.txtKey.Location = new Point(121, 134);
                ucdi.txtKey.Size = new Size(220, 20);

                //MapDescription
                ucdi.lblMapdesc.Location = new Point(12, 161);
                ucdi.txtMapDescription.Location = new Point(121, 161);
                ucdi.txtMapDescription.Size = new Size(220, 20);

                ucdi.lblMapAutoMapRange.Visible = false;
                ucdi.txtDIAutoMap.Visible = false;
                ucdi.lblMapAutoMapRange.Location = new Point(12, 187);
                ucdi.txtDIAutoMap.Location = new Point(121, 187);
                ucdi.txtDIAutoMap.Size = new Size(150, 20);
                ucdi.lblDMDT.Visible = true;
                ucdi.cmbDIMDataType.Visible = true;
                ucdi.lblDMBP.Visible = true;
                ucdi.txtDIMBitPos.Visible = true;
                ucdi.lblKey.Visible = true;
                ucdi.txtKey.Visible = true;
                //Complement
                ucdi.chkComplement.Location = new Point(13, 190);

                ucdi.btnDIMDone.Location = new Point(120, 200);
                ucdi.btnDIMCancel.Location = new Point(200, 200);

                ucdi.lblUnitID.Visible = false;
                ucdi.txtUnitID.Visible = false;
                ucdi.chkUsed.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void MQTTSlaveMappingEvents()
        {
            string strRoutineName = "DIConfiguration:MQTTSlaveMappingEvents";
            try
            {
                #region Visible false
                ucdi.lblEV.Visible = false;
                ucdi.CmbEV.Visible = false;
                ucdi.lblEventClass.Visible = false;
                ucdi.cmbEventC.Visible = false;
                ucdi.lblVariation.Visible = false;
                ucdi.CmbVari.Visible = false;

                //Namrata: 20/08/2019
                #region CellNo
                ucdi.CmbCellNo.Visible = false;
                ucdi.lblCellNo.Visible = false;
                #endregion CellNo

                #region Widget
                ucdi.CmbWidget.Visible = false;
                ucdi.lblWidget.Visible = false;
                #endregion Widget
                //UnitID
                ucdi.lblUnitID.Visible = false;
                ucdi.txtUnitID.Visible = false;

                //Used
                ucdi.chkUsed.Visible = false;

                //61850reportingIndex
                ucdi.lbl61850reporting.Visible = false;
                ucdi.CmbReportingIndex.Visible = false;

                //CommandType
                ucdi.lblDMCT.Visible = false;
                ucdi.cmbDIMCommandType.Visible = false;
                #endregion Visible false

                #region Visible True
                //DIMNo
                ucdi.lblDNM.Location = new Point(12, 30);
                ucdi.txtDIMNo.Location = new Point(122, 30);
                ucdi.txtDIMNo.Size = new Size(220, 20);

                //ReportingIndex
                ucdi.lblDMRI.Visible = true;
                ucdi.txtDIMReportingIndex.Visible = true;
                ucdi.lblDMRI.Location = new Point(12, 55);
                ucdi.txtDIMReportingIndex.Location = new Point(121, 55);
                ucdi.txtDIMReportingIndex.Size = new Size(220, 20);

                //DataType
              
                ucdi.lblDMDT.Location = new Point(12, 81);
                ucdi.cmbDIMDataType.Location = new Point(121, 81);
                ucdi.cmbDIMDataType.Size = new Size(220, 20);

                //BitPos
                
                ucdi.lblDMBP.Location = new Point(12, 108);
                ucdi.txtDIMBitPos.Location = new Point(121, 108);
                ucdi.txtDIMBitPos.Size = new Size(220, 20);

                //Key
                ucdi.lblDMDT.Visible = true;
                ucdi.cmbDIMDataType.Visible = true;
                ucdi.lblDMBP.Visible = true;
                ucdi.txtDIMBitPos.Visible = true;
                ucdi.lblKey.Visible = true;
                ucdi.txtKey.Visible = true;
                ucdi.lblKey.Location = new Point(12, 134);
                ucdi.txtKey.Location = new Point(121, 134);
                ucdi.txtKey.Size = new Size(220, 20);

                //MapDescription
                ucdi.lblMapdesc.Location = new Point(12, 161);
                ucdi.txtMapDescription.Location = new Point(121, 161);
                ucdi.txtMapDescription.Size = new Size(220, 20);

                //AutoMap
                ucdi.lblMapAutoMapRange.Location = new Point(12, 188);
                ucdi.txtDIAutoMap.Location = new Point(121, 188);
                ucdi.txtDIAutoMap.Size = new Size(220, 20);
                ucdi.lblMapAutoMapRange.Visible = true;
                ucdi.txtDIAutoMap.Visible = true;
                ucdi.txtDIAutoMap.Text = "1";
                //Complement
                ucdi.chkComplement.Location = new Point(13, 215);

                //BtnDone & BtnCancel
                ucdi.btnDIMDone.Location = new Point(120, 230);
                ucdi.btnDIMCancel.Location = new Point(200, 230);

                //GrpDIMap
                ucdi.grpDIMap.Location = new Point(234, 315);//new Point(450, 325);
                ucdi.grpDIMap.Size = new Size(360, 270);
                ucdi.pbDIMHdr.Size = new Size(360, 22);

                #endregion Visible True

            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void SMSSlaveMapEvent_OnLoad()
        {
            string strRoutineName = "DIConfiguration: SMSSlaveMapEvent_OnLoad";
            try
            {
                #region Visible false
                ucdi.lblEV.Visible = false;
                ucdi.CmbEV.Visible = false;
                ucdi.lblEventClass.Visible = false;
                ucdi.cmbEventC.Visible = false;
                ucdi.lblVariation.Visible = false;
                ucdi.CmbVari.Visible = false;

                //Namrata: 20/08/2019
                #region CellNo
                ucdi.CmbCellNo.Visible = false;
                ucdi.lblCellNo.Visible = false;
                #endregion CellNo

                #region Widget
                ucdi.CmbWidget.Visible = false;
                ucdi.lblWidget.Visible = false;
                #endregion Widget
                //UnitID
                ucdi.lblUnitID.Visible = false;
                ucdi.txtUnitID.Visible = false;

                //Used
                ucdi.chkUsed.Visible = false;

                //61850reportingIndex
                ucdi.lbl61850reporting.Visible = false;
                ucdi.CmbReportingIndex.Visible = false;

                //CommandType
                ucdi.lblDMCT.Visible = false;
                ucdi.cmbDIMCommandType.Visible = false;
                #endregion Visible false

                #region Visible True
                //DIMNo
                ucdi.lblDNM.Location = new Point(12, 30);
                ucdi.txtDIMNo.Location = new Point(122, 30);
                ucdi.txtDIMNo.Size = new Size(220, 20);

                //ReportingIndex
                ucdi.lblDMRI.Visible = true;
                ucdi.txtDIMReportingIndex.Visible = true;
                ucdi.lblDMRI.Location = new Point(12, 55);
                ucdi.txtDIMReportingIndex.Location = new Point(121, 55);
                ucdi.txtDIMReportingIndex.Size = new Size(220, 20);


                ucdi.lblDMDT.Visible = true;
                ucdi.cmbDIMDataType.Visible = true;
                ucdi.lblDMBP.Visible = true;
                ucdi.txtDIMBitPos.Visible = true;
                //DataType
                ucdi.lblDMDT.Location = new Point(12, 81);
                ucdi.cmbDIMDataType.Location = new Point(121, 81);
                ucdi.cmbDIMDataType.Size = new Size(220, 20);

                //BitPos
                ucdi.lblDMBP.Location = new Point(12, 108);
                ucdi.txtDIMBitPos.Location = new Point(121, 108);
                ucdi.txtDIMBitPos.Size = new Size(220, 20);

                //Complement
                ucdi.chkComplement.Location = new Point(13, 187);

                //Description
                ucdi.lblMapdesc.Location = new Point(12, 134);
                ucdi.txtMapDescription.Location = new Point(121, 134);
                ucdi.txtMapDescription.Size = new Size(220, 20);

                //AutoMap
                ucdi.lblMapAutoMapRange.Location = new Point(12, 161);
                ucdi.txtDIAutoMap.Location = new Point(121, 161);
                ucdi.txtDIAutoMap.Size = new Size(220, 20);
                ucdi.lblMapAutoMapRange.Visible = true;
                ucdi.txtDIAutoMap.Visible = true;
                ucdi.txtDIAutoMap.Text = "1";
                //BtnDone & BtnCancel
                ucdi.btnDIMDone.Location = new Point(120, 200);
                ucdi.btnDIMCancel.Location = new Point(200, 200);
                ucdi.ChkIEC61850MapIndex.Visible = false;
                //GrpDIMap
                ucdi.grpDIMap.Location = new Point(234, 315);//new Point(450, 325);
                ucdi.grpDIMap.Size = new Size(360, 240);
                ucdi.pbDIMHdr.Size = new Size(360, 22);
                //ucdi.pbDIMHdr.Width = 300;
                #endregion Visible True

            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void MODBUSSlave_OnDoubleClick()
        {
            string strRoutineName = "DIConfiguration: MODBUSSlave_OnDoubleClick";
            try
            {
                ucdi.lblEV.Visible = false;
                ucdi.CmbEV.Visible = false;
                ucdi.lblEventClass.Visible = false;
                ucdi.cmbEventC.Visible = false;
                ucdi.lblVariation.Visible = false;
                ucdi.CmbVari.Visible = false;

                //Namrata: 21/10/2019
                ucdi.lblKey.Visible = false;
                ucdi.txtKey.Visible = false;
                //Namrata: 20/08/2019
                #region CellNo
                ucdi.CmbCellNo.Visible = false;
                ucdi.lblCellNo.Visible = false;
                #endregion CellNo

                #region Widget
                ucdi.CmbWidget.Visible = false;
                ucdi.lblWidget.Visible = false;
                #endregion Widget

                ucdi.txtDIAutoMap.Text = "0";
                ucdi.ChkIEC61850MapIndex.Visible = false;//Namrata: 16/04/2019
                ucdi.cmbDIMCommandType.Enabled = true;

                ucdi.grpDIMap.Location = new Point(236, 315);//new Point(450, 325);
                ucdi.grpDIMap.Size = new Size(350, 230);
                ucdi.pbDIMHdr.Width = 350;

                ucdi.lblDNM.Location = new Point(12, 30);
                ucdi.txtDIMNo.Location = new Point(122, 30);
                ucdi.txtDIMNo.Size = new Size(220, 20);

                ucdi.lblDMRI.Visible = true;
                ucdi.txtDIMReportingIndex.Visible = true;
                ucdi.lblDMRI.Location = new Point(12, 55);
                ucdi.txtDIMReportingIndex.Location = new Point(121, 55);
                ucdi.txtDIMReportingIndex.Size = new Size(220, 20);

                ucdi.lbl61850reporting.Visible = false;
                ucdi.CmbReportingIndex.Visible = false;

                ucdi.lblDMDT.Visible = true; ucdi.cmbDIMDataType.Visible = true;
                ucdi.lblDMDT.Location = new Point(12, 81);
                ucdi.cmbDIMDataType.Location = new Point(121, 81);
                ucdi.cmbDIMDataType.Size = new Size(220, 20);

                ucdi.lblDMCT.Visible = true; ucdi.cmbDIMCommandType.Visible = true;
                ucdi.lblDMCT.Location = new Point(12, 108);
                ucdi.cmbDIMCommandType.Location = new Point(121, 108);
                ucdi.cmbDIMCommandType.Size = new Size(220, 20);

                ucdi.lblDMBP.Visible = true; ucdi.txtDIMBitPos.Visible = true;
                ucdi.lblDMBP.Location = new Point(12, 134);
                ucdi.txtDIMBitPos.Location = new Point(121, 134);
                ucdi.txtDIMBitPos.Size = new Size(220, 20);

                ucdi.lblMapdesc.Visible = true; ucdi.txtMapDescription.Visible = true;
                ucdi.lblMapdesc.Location = new Point(12, 161);
                ucdi.txtMapDescription.Location = new Point(121, 161);
                ucdi.txtMapDescription.Size = new Size(220, 20);

                ucdi.lblMapAutoMapRange.Visible = false;
                ucdi.txtDIAutoMap.Visible = false;
                ucdi.lblMapAutoMapRange.Location = new Point(12, 187);
                ucdi.txtDIAutoMap.Location = new Point(121, 187);
                ucdi.txtDIAutoMap.Size = new Size(220, 20);

                ucdi.chkComplement.Location = new Point(13, 190);

                ucdi.btnDIMDone.Location = new Point(120, 190);
                ucdi.btnDIMCancel.Location = new Point(200, 190);

                ucdi.lblUnitID.Visible = false;
                ucdi.txtUnitID.Visible = false;
                ucdi.chkUsed.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void MODBUSSlave_OnLoad()
        {
            string strRoutineName = "DIConfiguration: MODBUSSlave_OnLoad";
            try
            {
                ucdi.lblEV.Visible = false;
                ucdi.CmbEV.Visible = false;
                ucdi.lblEventClass.Visible = false;
                ucdi.cmbEventC.Visible = false;
                ucdi.lblVariation.Visible = false;
                ucdi.CmbVari.Visible = false;

                ucdi.ChkIEC61850MapIndex.Visible = false;//Namrata:16/04/2019
                ucdi.txtDIAutoMap.Text = "1";//Namrata:1/7/2017
                ucdi.cmbDIMCommandType.Enabled = true;


                #region Visibale false
                //Namrata: 21/10/2019
                ucdi.lblKey.Visible = false;
                ucdi.txtKey.Visible = false;

                //Namrata: 20/08/2019
                #region CellNo
                ucdi.CmbCellNo.Visible = false;
                ucdi.lblCellNo.Visible = false;
                #endregion CellNo

                #region Widget
                ucdi.CmbWidget.Visible = false;
                ucdi.lblWidget.Visible = false;
                #endregion Widget
                //61850reporting
                ucdi.lbl61850reporting.Visible = false;
                ucdi.CmbReportingIndex.Visible = false;

                //UnitID
                ucdi.lblUnitID.Visible = false;
                ucdi.txtUnitID.Visible = false;

                //Used
                ucdi.chkUsed.Visible = false;
                #endregion Visibale false

                #region Visibale true
                //DIMNo
                ucdi.lblDNM.Location = new Point(12, 30);
                ucdi.txtDIMNo.Location = new Point(122, 30);
                ucdi.txtDIMNo.Size = new Size(220, 20);

                //DIMReportingIndex
                ucdi.lblDMRI.Visible = true;
                ucdi.txtDIMReportingIndex.Visible = true;
                ucdi.lblDMRI.Location = new Point(12, 55);
                ucdi.txtDIMReportingIndex.Location = new Point(121, 55);
                ucdi.txtDIMReportingIndex.Size = new Size(220, 20);

                //DIMDataType
                ucdi.lblDMDT.Visible = true; ucdi.cmbDIMDataType.Visible = true;
                ucdi.lblDMDT.Location = new Point(12, 81);
                ucdi.cmbDIMDataType.Location = new Point(121, 81);
                ucdi.cmbDIMDataType.Size = new Size(220, 20);

                //CommandType
                ucdi.lblDMCT.Visible = true;
                ucdi.cmbDIMCommandType.Visible = true;
                ucdi.lblDMCT.Location = new Point(12, 108);
                ucdi.cmbDIMCommandType.Location = new Point(121, 108);
                ucdi.cmbDIMCommandType.Size = new Size(220, 20);

                //BitPos
                ucdi.lblDMBP.Visible = true; ucdi.txtDIMBitPos.Visible = true;
                ucdi.lblDMBP.Location = new Point(12, 134);
                ucdi.txtDIMBitPos.Location = new Point(121, 134);
                ucdi.txtDIMBitPos.Size = new Size(220, 20);

                //Complement
                ucdi.chkComplement.Visible = true;
                ucdi.chkComplement.Location = new Point(13, 215);

                //MapDescription
                ucdi.lblMapdesc.Visible = true; ucdi.txtMapDescription.Visible = true;
                ucdi.lblMapdesc.Location = new Point(12, 161);
                ucdi.txtMapDescription.Location = new Point(121, 161);
                ucdi.txtMapDescription.Size = new Size(220, 20);

                //Automap
                ucdi.lblMapAutoMapRange.Visible = true;
                ucdi.txtDIAutoMap.Visible = true;
                ucdi.lblMapAutoMapRange.Location = new Point(12, 187);
                ucdi.txtDIAutoMap.Location = new Point(121, 187);
                ucdi.txtDIAutoMap.Size = new Size(220, 20);

                //BtnDone & BtnCancel
                ucdi.btnDIMDone.Location = new Point(120, 215);
                ucdi.btnDIMCancel.Location = new Point(200, 215);

                //grpDIMap
                ucdi.grpDIMap.Location = new Point(242, 315);//new Point(450, 325);
                ucdi.pbDIMHdr.Width = 350;
                ucdi.grpDIMap.Size = new Size(350, 260);


                #endregion Visibale true
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void IEC101Slave_IEC104Slave_OnDoubleClick()
        {
            string strRoutineName = "DIConfiguration: IEC101Slave_IEC104Slave_OnDoubleClick";
            try
            {
                ucdi.lblEV.Visible = false;
                ucdi.CmbEV.Visible = false;
                ucdi.lblEventClass.Visible = false;
                ucdi.cmbEventC.Visible = false;
                ucdi.lblVariation.Visible = false;
                ucdi.CmbVari.Visible = false;

                ucdi.txtDIAutoMap.Text = "1";//Namrata: 01/07/2017
                ucdi.cmbDIMCommandType.Enabled = false;
                ucdi.ChkIEC61850MapIndex.Visible = false;//Namrata:16/04/2019
                                                         //Namrata: 21/10/2019
                ucdi.lblKey.Visible = false;
                ucdi.txtKey.Visible = false;
                #region Visible false
                //Namrata: 20/08/2019
                #region CellNo
                ucdi.CmbCellNo.Visible = false;
                ucdi.lblCellNo.Visible = false;
                #endregion CellNo

                #region Widget
                ucdi.CmbWidget.Visible = false;
                ucdi.lblWidget.Visible = false;
                #endregion Widget
                //UnitID
                ucdi.lblUnitID.Visible = false;
                ucdi.txtUnitID.Visible = false;

                //Used
                ucdi.chkUsed.Visible = false;

                //61850reportingIndex
                ucdi.lbl61850reporting.Visible = false;
                ucdi.CmbReportingIndex.Visible = false;

                //CommandType
                ucdi.lblDMCT.Visible = false;
                ucdi.cmbDIMCommandType.Visible = false;
                #endregion Visible false

                #region Visible True
                //DIMNo
                ucdi.lblDNM.Location = new Point(12, 30);
                ucdi.txtDIMNo.Location = new Point(122, 30);
                ucdi.txtDIMNo.Size = new Size(220, 20);

                //ReportingIndex
                ucdi.lblDMRI.Visible = true;
                ucdi.txtDIMReportingIndex.Visible = true;
                ucdi.lblDMRI.Location = new Point(12, 55);
                ucdi.txtDIMReportingIndex.Location = new Point(121, 55);
                ucdi.txtDIMReportingIndex.Size = new Size(220, 20);

                //DataType
                ucdi.lblDMDT.Visible = true; ; ucdi.cmbDIMDataType.Visible = true;
                ucdi.lblDMDT.Location = new Point(12, 81);
                ucdi.cmbDIMDataType.Location = new Point(121, 81);
                ucdi.cmbDIMDataType.Size = new Size(220, 20);

                //BitPos
                ucdi.lblDMBP.Visible = true; ; ucdi.txtDIMBitPos.Visible = true;
                ucdi.lblDMBP.Location = new Point(12, 108);
                ucdi.txtDIMBitPos.Location = new Point(121, 108);
                ucdi.txtDIMBitPos.Size = new Size(220, 20);

                //Complement
                ucdi.chkComplement.Visible = true;
                ucdi.chkComplement.Location = new Point(13, 160);

                //Description
                ucdi.lblMapdesc.Visible = true; ; ucdi.txtMapDescription.Visible = true;
                ucdi.lblMapdesc.Location = new Point(12, 134);
                ucdi.txtMapDescription.Location = new Point(121, 134);
                ucdi.txtMapDescription.Size = new Size(220, 20);


                //AutoMap
                ucdi.lblMapAutoMapRange.Location = new Point(12, 161);
                ucdi.txtDIAutoMap.Location = new Point(121, 161);
                ucdi.txtDIAutoMap.Size = new Size(220, 20);
                ucdi.lblMapAutoMapRange.Visible = false;
                ucdi.txtDIAutoMap.Visible = false;

                //BtnDone & BtnCancel
                ucdi.btnDIMDone.Location = new Point(120, 165);
                ucdi.btnDIMCancel.Location = new Point(200, 165);

                //GrpDIMap
                ucdi.grpDIMap.Location = new Point(242, 315);//new Point(450, 325);
                ucdi.pbDIMHdr.Width = 350;
                ucdi.grpDIMap.Size = new Size(350, 205);
                #endregion Visible True

            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void IEC101Slave_IEC104Slave_OnLoad()
        {
            string strRoutineName = "DIConfiguration: IEC101Slave_IEC104Slave_OnLoad";
            try
            {
                ucdi.lblEV.Visible = false;
                ucdi.CmbEV.Visible = false;
                ucdi.lblEventClass.Visible = false;
                ucdi.cmbEventC.Visible = false;
                ucdi.lblVariation.Visible = false;
                ucdi.CmbVari.Visible = false;

                //Namrata: 21/10/2019
                ucdi.lblKey.Visible = false;
                ucdi.txtKey.Visible = false;
                ucdi.txtDIAutoMap.Text = "1";//Namrata: 01/07/2017
                ucdi.cmbDIMCommandType.Enabled = false;
                ucdi.ChkIEC61850MapIndex.Visible = false;//Namrata:16/04/2019

                #region Visible false

                //Namrata: 20/08/2019
                #region CellNo
                ucdi.CmbCellNo.Visible = false;
                ucdi.lblCellNo.Visible = false;
                #endregion CellNo

                #region Widget
                ucdi.CmbWidget.Visible = false;
                ucdi.lblWidget.Visible = false;
                #endregion Widget
                //UnitID
                ucdi.lblUnitID.Visible = false;
                ucdi.txtUnitID.Visible = false;

                //Used
                ucdi.chkUsed.Visible = false;

                //61850reportingIndex
                ucdi.lbl61850reporting.Visible = false;
                ucdi.CmbReportingIndex.Visible = false;

                //CommandType
                ucdi.lblDMCT.Visible = false;
                ucdi.cmbDIMCommandType.Visible = false;
                #endregion Visible false

                #region Visible True
                //DIMNo
                ucdi.lblDNM.Location = new Point(12, 30);
                ucdi.txtDIMNo.Location = new Point(122, 30);
                ucdi.txtDIMNo.Size = new Size(220, 20);

                //ReportingIndex
                ucdi.lblDMRI.Visible = true;
                ucdi.txtDIMReportingIndex.Visible = true;
                ucdi.lblDMRI.Location = new Point(12, 55);
                ucdi.txtDIMReportingIndex.Location = new Point(121, 55);
                ucdi.txtDIMReportingIndex.Size = new Size(220, 20);

                //DataType
                ucdi.lblDMDT.Visible = true; ; ucdi.cmbDIMDataType.Visible = true;
                ucdi.lblDMDT.Location = new Point(12, 81);
                ucdi.cmbDIMDataType.Location = new Point(121, 81);
                ucdi.cmbDIMDataType.Size = new Size(220, 20);

                //BitPos
                ucdi.lblDMBP.Visible = true; ; ucdi.txtDIMBitPos.Visible = true;
                ucdi.lblDMBP.Location = new Point(12, 108);
                ucdi.txtDIMBitPos.Location = new Point(121, 108);
                ucdi.txtDIMBitPos.Size = new Size(220, 20);

                //Complement
                ucdi.chkComplement.Visible = true;
                ucdi.chkComplement.Location = new Point(13, 187);

                //Description
                ucdi.lblMapdesc.Visible = true; ; ucdi.txtMapDescription.Visible = true;
                ucdi.lblMapdesc.Location = new Point(12, 134);
                ucdi.txtMapDescription.Location = new Point(121, 134);
                ucdi.txtMapDescription.Size = new Size(220, 20);


                //AutoMap
                ucdi.lblMapAutoMapRange.Location = new Point(12, 161);
                ucdi.txtDIAutoMap.Location = new Point(121, 161);
                ucdi.txtDIAutoMap.Size = new Size(220, 20);
                ucdi.lblMapAutoMapRange.Visible = true;
                ucdi.txtDIAutoMap.Visible = true;

                //BtnDone & BtnCancel
                ucdi.btnDIMDone.Location = new Point(120, 200);
                ucdi.btnDIMCancel.Location = new Point(200, 200);

                //GrpDIMap
                ucdi.grpDIMap.Location = new Point(242, 315);//new Point(450, 325);
                ucdi.pbDIMHdr.Width = 350;
                ucdi.grpDIMap.Size = new Size(350, 240);
                #endregion Visible True

            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void IEC61850Server_OnLoad()
        {
            string strRoutineName = "DIConfiguration: IEC61850Server_OnLoad";
            try
            {
                ucdi.lblEV.Visible = false;
                ucdi.CmbEV.Visible = false;
                ucdi.lblEventClass.Visible = false;
                ucdi.cmbEventC.Visible = false;
                ucdi.lblVariation.Visible = false;
                ucdi.CmbVari.Visible = false;

                //Namrata: 21/10/2019
                ucdi.lblKey.Visible = false;
                ucdi.txtKey.Visible = false;
                //Namrata: 20/08/2019
                #region CellNo
                ucdi.CmbCellNo.Visible = false;
                ucdi.lblCellNo.Visible = false;
                #endregion CellNo

                #region Widget
                ucdi.CmbWidget.Visible = false;
                ucdi.lblWidget.Visible = false;
                #endregion Widget
                ucdi.ChkIEC61850MapIndex.Visible = true;//Namrata:16/04/2019
                ucdi.txtDIAutoMap.Text = "";//Namrata:1/7/2017
                //ReportingIndex
                ucdi.txtDIMReportingIndex.Visible = false;

                //UnitID
                ucdi.txtUnitID.Visible = false;
                ucdi.lblUnitID.Visible = false;

                //DIMNo
                ucdi.lblDNM.Location = new Point(12, 30);
                ucdi.txtDIMNo.Location = new Point(121, 30);
                ucdi.txtDIMNo.Size = new Size(300, 21);
                ucdi.lblDMRI.Visible = false;

                //IEC61850 ReportingIndex
                //ucdi.lbl61850reporting.Visible = true;
                //ucdi.CmbReportingIndex.Visible = true;
                //ucdi.lbl61850reporting.Location = new Point(12, 55);
                //ucdi.CmbReportingIndex.Location = new Point(121, 55);
                //ucdi.CmbReportingIndex.Size = new Size(300, 21);
                ucdi.lbl61850reporting.Visible = true;
                ucdi.CmbReportingIndex.Visible = false;
                ucdi.ChkIEC61850MapIndex.Visible = true;
                ucdi.lbl61850reporting.Location = new Point(12, 55);
                ucdi.ChkIEC61850MapIndex.Location = new Point(121, 55);
                ucdi.ChkIEC61850MapIndex.Size = new Size(300, 21);

                //DataType
                ucdi.lblDMDT.Location = new Point(12, 81);
                ucdi.cmbDIMDataType.Location = new Point(121, 81);
                ucdi.cmbDIMDataType.Size = new Size(300, 21);

                //CommandType
                ucdi.lblDMCT.Visible = false;
                ucdi.cmbDIMCommandType.Visible = false;

                //BitPos
                ucdi.lblDMBP.Location = new Point(12, 108);
                ucdi.txtDIMBitPos.Location = new Point(121, 108);
                ucdi.txtDIMBitPos.Size = new Size(300, 21);

                //Map Description
                ucdi.lblMapdesc.Location = new Point(12, 134);
                ucdi.txtMapDescription.Location = new Point(121, 134);
                ucdi.txtMapDescription.Size = new Size(300, 21);

                //Complement
                ucdi.chkComplement.Location = new Point(12, 160);

                //Automap
                ucdi.lblMapAutoMapRange.Visible = false;
                ucdi.txtDIAutoMap.Visible = false;

                //BtnDone & BtnCancel
                ucdi.btnDIMDone.Location = new Point(170, 165);
                ucdi.btnDIMCancel.Location = new Point(250, 165);

                //GrpAIMap
                ucdi.pbDIMHdr.Size = new Size(450, 22);
                ucdi.grpDIMap.Size = new Size(450, 210);
                ucdi.grpDIMap.Location = new Point(500, 370);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void IEC61850Server_OnDoubleClick()
        {
            string strRoutineName = "DIConfiguration: IEC61850Server_OnDoubleClick";
            try
            {
                ucdi.lblEV.Visible = false;
                ucdi.CmbEV.Visible = false;
                ucdi.lblEventClass.Visible = false;
                ucdi.cmbEventC.Visible = false;
                ucdi.lblVariation.Visible = false;
                ucdi.CmbVari.Visible = false;

                //Namrata: 21/10/2019
                ucdi.lblKey.Visible = false;
                ucdi.txtKey.Visible = false;
                //Namrata: 20/08/2019
                #region CellNo
                ucdi.CmbCellNo.Visible = false;
                ucdi.lblCellNo.Visible = false;
                #endregion CellNo

                #region Widget
                ucdi.CmbWidget.Visible = false;
                ucdi.lblWidget.Visible = false;
                #endregion Widget
                ucdi.lblUnitID.Visible = false;
                ucdi.txtUnitID.Visible = false;
                ucdi.lblMapAutoMapRange.Visible = false;
                ucdi.txtDIAutoMap.Visible = false;

                ucdi.lblDNM.Location = new Point(12, 30);
                ucdi.txtDIMNo.Location = new Point(121, 30);

                ucdi.lblDMRI.Visible = false;
                ucdi.txtDIMReportingIndex.Visible = false;

                ucdi.lbl61850reporting.Visible = true;
                ucdi.CmbReportingIndex.Visible = false;
                ucdi.ChkIEC61850MapIndex.Visible = true;
                ucdi.lbl61850reporting.Location = new Point(12, 55);
                ucdi.ChkIEC61850MapIndex.Location = new Point(121, 55);
                ucdi.txtDIMNo.Size = new Size(300, 21);
                ucdi.ChkIEC61850MapIndex.Size = new Size(300, 21);

                ucdi.lblDMDT.Location = new Point(12, 81);
                ucdi.cmbDIMDataType.Location = new Point(121, 81);
                ucdi.cmbDIMDataType.Size = new Size(300, 21);

                ucdi.lblDMCT.Location = new Point(12, 108);
                ucdi.lblDMCT.Visible = false;
                ucdi.cmbDIMCommandType.Visible = false;
                ucdi.cmbDIMCommandType.Location = new Point(121, 108);
                ucdi.cmbDIMCommandType.Size = new Size(300, 21);


                ucdi.lblDMBP.Location = new Point(12, 108);
                ucdi.txtDIMBitPos.Location = new Point(121, 108);
                ucdi.txtDIMBitPos.Size = new Size(300, 21);



                ucdi.lblMapdesc.Location = new Point(11, 134);
                ucdi.txtMapDescription.Location = new Point(121, 134);
                ucdi.txtMapDescription.Size = new Size(300, 21);

                ucdi.chkComplement.Location = new Point(12, 165);

                ucdi.btnDIMDone.Location = new Point(170, 170);
                ucdi.btnDIMCancel.Location = new Point(270, 170);
                ucdi.grpDIMap.Size = new Size(450, 210);
                ucdi.grpDIMap.Location = new Point(450, 370);
                ucdi.pbDIMHdr.Size = new Size(450, 22);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void SPORTSlave_OnDoubleClick()
        {
            string strRoutineName = "DIConfiguration: SPORTSlave_OnDoubleClick";
            try
            {
                ucdi.lblEV.Visible = false;
                ucdi.CmbEV.Visible = false;
                ucdi.lblEventClass.Visible = false;
                ucdi.cmbEventC.Visible = false;
                ucdi.lblVariation.Visible = false;
                ucdi.CmbVari.Visible = false;

                //Namrata: 21/10/2019
                ucdi.lblKey.Visible = false;
                ucdi.txtKey.Visible = false;
                //Namrata: 20/08/2019
                #region CellNo
                ucdi.CmbCellNo.Visible = false;
                ucdi.lblCellNo.Visible = false;
                #endregion CellNo

                #region Widget
                ucdi.CmbWidget.Visible = false;
                ucdi.lblWidget.Visible = false;
                #endregion Widget
                ucdi.ChkIEC61850MapIndex.Visible = false;//Namrata: 16/04/2019
                ucdi.grpDIMap.Location = new Point(450, 350);//new Point(450, 325);
                ucdi.grpDIMap.Size = new Size(300, 255);
                ucdi.pbDIMHdr.Width = 300;
                ucdi.cmbDIMCommandType.Visible = false;
                ucdi.lblDMCT.Visible = false;

                ucdi.lblDNM.Location = new Point(13, 33);
                ucdi.txtDIMNo.Location = new Point(122, 30);
                ucdi.txtDIMNo.Size = new Size(150, 20);

                ucdi.lblUnitID.Visible = true;
                ucdi.txtUnitID.Visible = true;
                ucdi.lblUnitID.Enabled = true;
                ucdi.txtUnitID.Enabled = true;
                ucdi.lblUnitID.Location = new Point(13, 58);
                ucdi.txtUnitID.Location = new Point(121, 55);
                ucdi.txtUnitID.Size = new Size(150, 20);


                ucdi.lblDMRI.Visible = true;
                ucdi.txtDIMReportingIndex.Visible = true;
                ucdi.lblDMRI.Location = new Point(13, 83);
                ucdi.txtDIMReportingIndex.Location = new Point(121, 80);
                ucdi.txtDIMReportingIndex.Size = new Size(150, 20);

                ucdi.lbl61850reporting.Visible = false;
                ucdi.CmbReportingIndex.Visible = false;

                ucdi.lblDMDT.Visible = true; ; ucdi.cmbDIMDataType.Visible = true;
                ucdi.lblDMDT.Location = new Point(13, 109);
                ucdi.cmbDIMDataType.Location = new Point(121, 106);
                ucdi.cmbDIMDataType.Size = new Size(150, 20);

                ucdi.lblDMBP.Visible = true; ; ucdi.txtDIMBitPos.Visible = true;
                ucdi.lblDMBP.Location = new Point(13, 135);
                ucdi.txtDIMBitPos.Location = new Point(121, 132);
                ucdi.txtDIMBitPos.Size = new Size(150, 20);

                ucdi.lblMapdesc.Visible = true; ; ucdi.txtMapDescription.Visible = true;
                ucdi.lblMapdesc.Location = new Point(13, 161);
                ucdi.txtMapDescription.Location = new Point(121, 158);
                ucdi.txtMapDescription.Size = new Size(150, 20);

                ucdi.lblMapAutoMapRange.Visible = true; ; ucdi.txtDIAutoMap.Visible = true;
                ucdi.lblMapAutoMapRange.Location = new Point(13, 186);
                ucdi.txtDIAutoMap.Location = new Point(121, 183);
                ucdi.txtDIAutoMap.Size = new Size(150, 20);

                ucdi.lblMapAutoMapRange.Visible = false;
                ucdi.txtDIAutoMap.Visible = false;

                ucdi.chkComplement.Location = new Point(121, 186);
                ucdi.chkUsed.Location = new Point(227, 186);
                ucdi.chkUsed.Visible = true;
                ucdi.chkComplement.Enabled = true;
                ucdi.btnDIMDone.Location = new Point(120, 210);
                ucdi.btnDIMCancel.Location = new Point(200, 210);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SPORTSlave_OnLoad()
        {
            string strRoutineName = "DIConfiguration: SPORTSlave_OnLoad";
            try
            {
                ucdi.lblEV.Visible = false;
                ucdi.CmbEV.Visible = false;
                ucdi.lblEventClass.Visible = false;
                ucdi.cmbEventC.Visible = false;
                ucdi.lblVariation.Visible = false;
                ucdi.CmbVari.Visible = false;

                //Namrata: 21/10/2019
                ucdi.lblKey.Visible = false;
                ucdi.txtKey.Visible = false;
                ucdi.txtDIAutoMap.Text = "1";//Namrata:01/07/2017
                ucdi.ChkIEC61850MapIndex.Visible = false;//Namrata:16/04/2019
                #region Visible False

                //Namrata: 20/08/2019
                #region CellNo
                ucdi.CmbCellNo.Visible = false;
                ucdi.lblCellNo.Visible = false;
                #endregion CellNo

                #region Widget
                ucdi.CmbWidget.Visible = false;
                ucdi.lblWidget.Visible = false;
                #endregion Widget
                //CommandType
                ucdi.cmbDIMCommandType.Visible = false;
                ucdi.lblDMCT.Visible = false;

                //61850reportingIndex
                ucdi.lbl61850reporting.Visible = false;
                ucdi.CmbReportingIndex.Visible = false;
                #endregion Visible False

                #region Visible True
                //DIMNo
                ucdi.lblDNM.Location = new Point(13, 33);
                ucdi.txtDIMNo.Location = new Point(122, 30);
                ucdi.txtDIMNo.Size = new Size(220, 20);

                //UnitID
                ucdi.lblUnitID.Visible = true;
                ucdi.txtUnitID.Visible = true;
                ucdi.lblUnitID.Location = new Point(13, 58);
                ucdi.txtUnitID.Location = new Point(121, 55);
                ucdi.txtUnitID.Size = new Size(220, 20);

                //ReportingIndex
                ucdi.lblDMRI.Visible = true;
                ucdi.txtDIMReportingIndex.Visible = true;
                ucdi.lblDMRI.Location = new Point(13, 83);
                ucdi.txtDIMReportingIndex.Location = new Point(121, 80);
                ucdi.txtDIMReportingIndex.Size = new Size(220, 20);

                //DataType
                ucdi.lblDMDT.Visible = true; ; ucdi.cmbDIMDataType.Visible = true;
                ucdi.lblDMDT.Location = new Point(13, 109);
                ucdi.cmbDIMDataType.Location = new Point(121, 106);
                ucdi.cmbDIMDataType.Size = new Size(220, 20);

                //BitPos
                ucdi.lblDMBP.Visible = true; ; ucdi.txtDIMBitPos.Visible = true;
                ucdi.lblDMBP.Location = new Point(13, 135);
                ucdi.txtDIMBitPos.Location = new Point(121, 132);
                ucdi.txtDIMBitPos.Size = new Size(220, 20);

                //Complement
                ucdi.chkComplement.Visible = true;
                ucdi.chkComplement.Location = new Point(121, 210);

                //Description
                ucdi.lblMapdesc.Visible = true; ; ucdi.txtMapDescription.Visible = true;
                ucdi.lblMapdesc.Location = new Point(13, 161);
                ucdi.txtMapDescription.Location = new Point(121, 158);
                ucdi.txtMapDescription.Size = new Size(220, 20);

                //Used
                ucdi.chkUsed.Location = new Point(227, 210);
                ucdi.chkUsed.Visible = true;

                //Automap
                ucdi.lblMapAutoMapRange.Visible = true;
                ucdi.txtDIAutoMap.Visible = true;
                ucdi.lblMapAutoMapRange.Location = new Point(13, 186);
                ucdi.txtDIAutoMap.Location = new Point(121, 183);
                ucdi.txtDIAutoMap.Size = new Size(220, 20);

                //BtnDone & BtnCancel
                ucdi.btnDIMDone.Location = new Point(120, 236);
                ucdi.btnDIMCancel.Location = new Point(200, 236);

                //GrpDIMap
                ucdi.grpDIMap.Location = new Point(242, 315);//new Point(450, 325);
                ucdi.pbDIMHdr.Width = 350;
                ucdi.grpDIMap.Size = new Size(350, 280);
                #endregion Visible True
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void DisplaySlave_OnLoad()
        {
            string strRoutineName = "DIConfiguration: DisplaySlave_OnLoad";
            try
            {
                ucdi.lblEV.Visible = false;
                ucdi.CmbEV.Visible = false;
                ucdi.lblEventClass.Visible = false;
                ucdi.cmbEventC.Visible = false;
                ucdi.lblVariation.Visible = false;
                ucdi.CmbVari.Visible = false;

                //Namrata: 21/10/2019
                ucdi.lblKey.Visible = false;
                ucdi.txtKey.Visible = false;
                ucdi.txtDIAutoMap.Text = "1";
                ucdi.ChkIEC61850MapIndex.Visible = false;
                #region Visible False
                //CommandType
                ucdi.cmbDIMCommandType.Visible = false;
                ucdi.lblDMCT.Visible = false;

                //61850reportingIndex
                ucdi.lbl61850reporting.Visible = false;
                ucdi.CmbReportingIndex.Visible = false;
                #endregion Visible False

                #region Visible True
                //DIMNo
                ucdi.lblDNM.Visible = true;
                ucdi.txtDIMNo.Visible = true;
                ucdi.lblDNM.Location = new Point(13, 33);
                ucdi.txtDIMNo.Location = new Point(122, 30);
                ucdi.txtDIMNo.Size = new Size(160, 21);

                #region CellNo
                ucdi.lblCellNo.Visible = true;
                ucdi.CmbCellNo.Visible = true;
                ucdi.lblCellNo.Location = new Point(13, 58);
                ucdi.CmbCellNo.Location = new Point(121, 55);
                ucdi.CmbCellNo.Size = new Size(160, 21);
                #endregion CellNo

                #region Widgets
                ucdi.lblWidget.Visible = true;
                ucdi.CmbWidget.Visible = true;
                ucdi.lblWidget.Location = new Point(13, 83);
                ucdi.CmbWidget.Location = new Point(121, 80);
                ucdi.CmbWidget.Size = new Size(160, 21);
                #endregion Widgets

                //UnitID
                ucdi.lblUnitID.Visible = false;
                ucdi.txtUnitID.Visible = false;
                //ucdi.lblUnitID.Location = new Point(13, 109);
                //ucdi.txtUnitID.Location = new Point(121, 106);
                ucdi.txtUnitID.Size = new Size(160, 21);

                //Description
                ucdi.lblMapdesc.Visible = true;
                ucdi.txtMapDescription.Visible = true;
                ucdi.lblMapdesc.Location = new Point(13, 109);
                ucdi.txtMapDescription.Location = new Point(121, 106);
                //ucdi.lblMapdesc.Location = new Point(13, 135);
                //ucdi.txtMapDescription.Location = new Point(121, 132);
                ucdi.txtMapDescription.Size = new Size(160, 21);

                //Automap
                ucdi.lblMapAutoMapRange.Visible = false;
                ucdi.txtDIAutoMap.Visible = false;
                ucdi.lblMapAutoMapRange.Location = new Point(13, 161);
                ucdi.txtDIAutoMap.Location = new Point(121, 158);
                ucdi.txtDIAutoMap.Size = new Size(160, 21);

                //Complement
                ucdi.chkComplement.Visible = true;
                ucdi.chkComplement.Location = new Point(121, 135);


                //ReportingIndex
                ucdi.lblDMRI.Visible = false;
                ucdi.txtDIMReportingIndex.Visible = false;

                //DataType
                ucdi.lblDMDT.Visible = false;
                ucdi.cmbDIMDataType.Visible = false;

                //BitPos
                ucdi.lblDMBP.Visible = false;
                ucdi.txtDIMBitPos.Visible = false;

                //Used
                ucdi.chkUsed.Visible = false;

                //BtnDone & BtnCancel
                ucdi.btnDIMDone.Location = new Point(120, 160);
                ucdi.btnDIMCancel.Location = new Point(200, 160);

                //GrpDIMap
                ucdi.grpDIMap.Location = new Point(72, 172);//new Point(450, 325);
                ucdi.pbDIMHdr.Width = 350;
                ucdi.grpDIMap.Size = new Size(300, 200);
                #endregion Visible True
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void DNPSlave_OnDoubleClick()
        {
            string strRoutineName = "DIConfiguration: DNPSlave_OnDoubleClick";
            try
            {
                ucdi.lblEV.Visible = true;
                ucdi.CmbEV.Visible = true;
                ucdi.lblEventClass.Visible = true;
                ucdi.cmbEventC.Visible = true;
                ucdi.lblVariation.Visible = true;
                ucdi.CmbVari.Visible = true;

                ucdi.ChkIEC61850MapIndex.Visible = false;//Namrata:16/04/2019
                ucdi.txtDIAutoMap.Text = "1";//Namrata:1/7/2017
                //ucdi.cmbDIMCommandType.Enabled = true;


                #region Visibale false
                //Namrata: 21/10/2019
                ucdi.lblKey.Visible = false;
                ucdi.txtKey.Visible = false;

                //Namrata: 20/08/2019
                #region CellNo
                ucdi.CmbCellNo.Visible = false;
                ucdi.lblCellNo.Visible = false;
                #endregion CellNo

                #region Widget
                ucdi.CmbWidget.Visible = false;
                ucdi.lblWidget.Visible = false;
                #endregion Widget
                //61850reporting
                ucdi.lbl61850reporting.Visible = false;
                ucdi.CmbReportingIndex.Visible = false;

                //UnitID
                ucdi.lblUnitID.Visible = false;
                ucdi.txtUnitID.Visible = false;

                //Used
                ucdi.chkUsed.Visible = false;
                #endregion Visibale false

                #region Visibale true
                //DIMNo
                ucdi.lblDNM.Location = new Point(12, 30);
                ucdi.txtDIMNo.Location = new Point(122, 30);
                ucdi.txtDIMNo.Size = new Size(220, 20);

                //DIMReportingIndex
                ucdi.lblDMRI.Visible = true;
                ucdi.txtDIMReportingIndex.Visible = true;
                ucdi.lblDMRI.Location = new Point(12, 55);
                ucdi.txtDIMReportingIndex.Location = new Point(121, 55);
                ucdi.txtDIMReportingIndex.Size = new Size(220, 20);

                //DIMDataType
                ucdi.lblDMDT.Visible = false; ucdi.cmbDIMDataType.Visible = false;

                ucdi.lblDMCT.Location = new Point(12, 81);
                ucdi.cmbDIMCommandType.Location = new Point(121, 81);
                ucdi.cmbDIMCommandType.Size = new Size(220, 20);

                //CommandType
                ucdi.lblDMCT.Visible = true;
                ucdi.cmbDIMCommandType.Visible = true;
                ucdi.lblDMBP.Visible = true; ucdi.txtDIMBitPos.Visible = true;
                ucdi.lblDMBP.Location = new Point(12, 108);
                ucdi.txtDIMBitPos.Location = new Point(121, 108);
                ucdi.txtDIMBitPos.Size = new Size(220, 20);

                ucdi.lblEV.Visible = true;
                ucdi.CmbEV.Visible = true;
                ucdi.lblEV.Location = new Point(12, 134);
                ucdi.CmbEV.Location = new Point(121, 134);
                ucdi.CmbEV.Size = new Size(220, 20);

                ucdi.lblEventClass.Visible = true;
                ucdi.cmbEventC.Visible = true;
                ucdi.lblEventClass.Location = new Point(12, 161);
                ucdi.cmbEventC.Location = new Point(121, 161);
                ucdi.cmbEventC.Size = new Size(220, 20);

                ucdi.lblVariation.Visible = true;
                ucdi.CmbVari.Visible = true;
                ucdi.lblVariation.Location = new Point(12, 187);
                ucdi.CmbVari.Location = new Point(121, 187);
                ucdi.CmbVari.Size = new Size(220, 20);

                //BitPos
                ucdi.lblDMBP.Visible = true; ucdi.txtDIMBitPos.Visible = true;
                ucdi.lblMapdesc.Location = new Point(12, 212);
                ucdi.txtMapDescription.Location = new Point(121, 212);
                ucdi.txtMapDescription.Size = new Size(220, 212);
                //MapDescription
                ucdi.lblMapdesc.Visible = true; ucdi.txtMapDescription.Visible = true;

                //Automap
                ucdi.lblMapAutoMapRange.Visible = false;
                ucdi.txtDIAutoMap.Visible = false;

                //BtnDone & BtnCancel
                ucdi.btnDIMDone.Location = new Point(120, 235);
                ucdi.btnDIMCancel.Location = new Point(200, 235);

                //grpDIMap
                ucdi.grpDIMap.Location = new Point(345, 310);//new Point(450, 325);
                ucdi.pbDIMHdr.Width = 350;
                ucdi.grpDIMap.Size = new Size(350, 270);


                #endregion Visibale true
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void DNPSlave_OnLoad()
        {
            string strRoutineName = "DIConfiguration: DNPSlave_OnLoad";
            try
            {
                ucdi.lblEV.Visible = true;
                ucdi.CmbEV.Visible = true;
                ucdi.lblEventClass.Visible = true;
                ucdi.cmbEventC.Visible = true;
                ucdi.lblVariation.Visible = true;
                ucdi.CmbVari.Visible = true;

                ucdi.ChkIEC61850MapIndex.Visible = false;//Namrata:16/04/2019
                ucdi.txtDIAutoMap.Text = "1";//Namrata:1/7/2017
                ucdi.cmbDIMCommandType.Enabled = true;


                #region Visibale false
                //Namrata: 21/10/2019
                ucdi.lblKey.Visible = false;
                ucdi.txtKey.Visible = false;

                //Namrata: 20/08/2019
                #region CellNo
                ucdi.CmbCellNo.Visible = false;
                ucdi.lblCellNo.Visible = false;
                #endregion CellNo

                #region Widget
                ucdi.CmbWidget.Visible = false;
                ucdi.lblWidget.Visible = false;
                #endregion Widget
                //61850reporting
                ucdi.lbl61850reporting.Visible = false;
                ucdi.CmbReportingIndex.Visible = false;

                //UnitID
                ucdi.lblUnitID.Visible = false;
                ucdi.txtUnitID.Visible = false;

                //Used
                ucdi.chkUsed.Visible = false;
                #endregion Visibale false

                #region Visibale true
                //DIMNo
                ucdi.lblDNM.Location = new Point(12, 30);
                ucdi.txtDIMNo.Location = new Point(122, 30);
                ucdi.txtDIMNo.Size = new Size(220, 20);

                //DIMReportingIndex
                ucdi.lblDMRI.Visible = true;
                ucdi.txtDIMReportingIndex.Visible = true;
                ucdi.lblDMRI.Location = new Point(12, 55);
                ucdi.txtDIMReportingIndex.Location = new Point(121, 55);
                ucdi.txtDIMReportingIndex.Size = new Size(220, 20);

                //DIMDataType
                ucdi.lblDMDT.Visible = false; ucdi.cmbDIMDataType.Visible = false;

                ucdi.lblDMCT.Location = new Point(12, 81);
                ucdi.cmbDIMCommandType.Location = new Point(121, 81);
                ucdi.cmbDIMCommandType.Size = new Size(220, 20);

                //CommandType
                ucdi.lblDMCT.Visible = true;
                ucdi.cmbDIMCommandType.Visible = true;
                ucdi.lblDMBP.Visible = true; ucdi.txtDIMBitPos.Visible = true;
                ucdi.lblDMBP.Location = new Point(12, 108);
                ucdi.txtDIMBitPos.Location = new Point(121, 108);
                ucdi.txtDIMBitPos.Size = new Size(220, 20);




                ucdi.lblEV.Visible = true;
                ucdi.CmbEV.Visible = true;
                ucdi.lblEV.Location = new Point(12, 134);
                ucdi.CmbEV.Location = new Point(121, 134);
                ucdi.CmbEV.Size = new Size(220, 20);


                ucdi.lblEventClass.Visible = true;
                ucdi.cmbEventC.Visible = true;
                ucdi.lblEventClass.Location = new Point(12, 161);
                ucdi.cmbEventC.Location = new Point(121, 161);
                ucdi.cmbEventC.Size = new Size(220, 20);


                ucdi.lblVariation.Visible = true;
                ucdi.CmbVari.Visible = true;
                ucdi.lblVariation.Location = new Point(12, 187);
                ucdi.CmbVari.Location = new Point(121, 187);
                ucdi.CmbVari.Size = new Size(220, 20);



                //BitPos
                ucdi.lblDMBP.Visible = true; ucdi.txtDIMBitPos.Visible = true;
                ucdi.lblMapdesc.Location = new Point(12, 212);
                ucdi.txtMapDescription.Location = new Point(121, 212);
                ucdi.txtMapDescription.Size = new Size(220, 212);

                ////Complement
                //ucdi.chkComplement.Visible = true;
                //ucdi.chkComplement.Location = new Point(13, 215);

                //MapDescription
                ucdi.lblMapdesc.Visible = true; ucdi.txtMapDescription.Visible = true;
                ucdi.lblMapAutoMapRange.Location = new Point(12, 237);
                ucdi.txtDIAutoMap.Location = new Point(121, 237);
                ucdi.txtDIAutoMap.Size = new Size(220, 20);

                //Automap
                ucdi.lblMapAutoMapRange.Visible = true;
                ucdi.txtDIAutoMap.Visible = true;
                //ucdi.lblMapAutoMapRange.Location = new Point(12, 262);
                //ucdi.txtDIAutoMap.Location = new Point(121, 262);
                //ucdi.txtDIAutoMap.Size = new Size(220, 20);

                //BtnDone & BtnCancel
                ucdi.btnDIMDone.Location = new Point(120, 265);
                ucdi.btnDIMCancel.Location = new Point(200, 265);

                //grpDIMap
                ucdi.grpDIMap.Location = new Point(345, 310);//new Point(450, 325);
                ucdi.pbDIMHdr.Width = 350;
                ucdi.grpDIMap.Size = new Size(350, 310);


                #endregion Visibale true
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion Events For All Slaves

        public static void ListToDataTable<DOMap>(IList<DOMap> varlist)
        {

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            if (typeof(DOMap).IsValueType || typeof(DOMap).Equals(typeof(string)))
            {
                DataColumn dc = new DataColumn("Image");
                dt.Columns.Add(dc);
                foreach (DOMap item in varlist)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item;
                    dt.Rows.Add(dr);
                }
            }
            else
            {
                PropertyInfo[] propT = typeof(DOMap).GetProperties(); //find all the public properties of this Type using reflection;
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
            GDSlave.DTDIDOImage = dt;
            //return dt;
        }
    }
}
