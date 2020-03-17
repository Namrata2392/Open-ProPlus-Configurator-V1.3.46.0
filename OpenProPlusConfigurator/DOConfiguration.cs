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
    public class DOConfiguration
    {
        #region Declaration
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        List<DOMap> slaveDOMapList;
        int intRange; List<string> IEC61850MapCheckedList = null; int intMapCheckItems = 0;
        private RCBConfiguration RCBNode = null;
        private string rnName = "";
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        private Mode mapMode = Mode.NONE;
        private int mapEditIndex = -1;
        private bool isNodeComment = false;
        private string comment = "";
        private string currentSlave = "";
        Dictionary<string, List<DOMap>> slavesDOMapList = new Dictionary<string, List<DOMap>>();
        private MasterTypes masterType = MasterTypes.UNKNOWN;
        private int masterNo = -1;
        private int IEDNo = -1;
        List<DO> doList = new List<DO>();
        ucDOlist ucdo = new ucDOlist();
        private const int COL_CMD_TYPE_WIDTH = 130;
        private const int COL_SELECT_TYPE_WIDTH = 80;
        //Namrata: 11/09/2017
        //Fill RessponseType in All Configuration . 
        public DataGridView dataGridViewDataSet = new DataGridView();
        public DataTable dtdataset = new DataTable();
        DataRow datasetRow;
        private string Response = "";
        private string ied = "";
        private int SelectedIndex;
        List<string> MergeList = null;
        List<string> ObjectRef = null;
        List<string> FC = null;
        List<string> ObjectRefForMap = null;//Namrata:12/04/2019
        List<string> NodeForMap = null;//Namrata:12/04/2019
        List<string> MergeListMap = null;//Namrata:25/03/2019
        List<string> IEC61850CheckedList = null;
        int intCheckItems = 0;
        #endregion Declaration
        public DOConfiguration(MasterTypes mType, int mNo, int iNo)
        {
            ucdo.splitContainer2.Panel2Collapsed = true;
            masterType = mType;
            masterNo = mNo;
            IEDNo = iNo;
            ucdo.btnAddClick += new System.EventHandler(this.btnAdd_Click);
            ucdo.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
            ucdo.btnDoneClick += new System.EventHandler(this.btnDone_Click);
            ucdo.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
            ucdo.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
            ucdo.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
            ucdo.btnNextClick += new System.EventHandler(this.btnNext_Click);
            ucdo.btnLastClick += new System.EventHandler(this.btnLast_Click);
            ucdo.btnDOMDeleteClick += new System.EventHandler(this.btnDOMDelete_Click);
            ucdo.btnDOMDoneClick += new System.EventHandler(this.btnDOMDone_Click);
            ucdo.btnDOMCancelClick += new System.EventHandler(this.btnDOMCancel_Click);

            ucdo.LinkDeleteConfigueClick += new System.EventHandler(this.LinkDeleteConfigue_Click);
            ucdo.linkDoMapClick += new System.EventHandler(this.linkDoMap_Click);
            ucdo.lvDOlistItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDOlist_ItemSelectionChanged);
            //Namrata : 27/07/2017
            ucdo.lvDOMapItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDOMap_ItemSelectionChanged);
            ucdo.lvDOMapDoubleClick += new System.EventHandler(this.lvDOMap_DoubleClick);

            ucdo.cmbIEDName.SelectedIndexChanged += new System.EventHandler(this.cmbIEDName_SelectedIndexChanged);
            ucdo.cmbIEC61850ResponseType.SelectedIndexChanged += new System.EventHandler(this.cmb61850DIResponseType_SelectedIndexChanged);
            ucdo.cmbIEC61850Index.SelectedIndexChanged += new System.EventHandler(this.cmb61850DIIndex_SelectedIndexChanged);
            ucdo.cmbFCSelectedIndexChanged += new System.EventHandler(this.cmbFC_SelectedIndexChanged); //Ajay: 17/01/2019
            ucdo.CmbCellNoSelectedIndexChanged += new System.EventHandler(this.CmbCellNo_SelectedIndexChanged); //Ajay: 17/01/2019
            #region Enable/Disable Events on Load
            if (mType == MasterTypes.Virtual)//Disable add/edit/delete/dblclick n remove checkboxes...
            {
                Virtual_MasterEventsOnLoad();
            }
            else
            {
                ucdo.lvDOlistDoubleClick += new System.EventHandler(this.lvDOlist_DoubleClick);
            }
            if (mType == MasterTypes.IEC61850Client)
            {
                IEC61850Client_EventsOnLoad();
            }
            if (mType == MasterTypes.IEC103)
            {
                ADR_IEC103_MODBUS_MasterEventsOnLoad();
            }
            if (mType == MasterTypes.SPORT)
            {
                IEC101_SPORT_MasterEventsOnLoad();
            }
            if (mType == MasterTypes.MODBUS)
            {
                ADR_IEC103_MODBUS_MasterEventsOnLoad();
            }
            if (mType == MasterTypes.ADR)
            {
                ADR_IEC103_MODBUS_MasterEventsOnLoad();
            }
            if (mType == MasterTypes.IEC101)
            {
                IEC101_SPORT_MasterEventsOnLoad();
            }
            if (mType == MasterTypes.IEC104)
            {
                //Ajay: 18/07/2018 New method created for IEC104
                IEC104_MasterEventsOnLoad();
            }
            //Ajay: 31/07/2018
            if (mType == MasterTypes.LoadProfile)
            {
                LoadProfile_OnLoad();
            }
            #endregion Enable/Disable Events on Load

            addListHeaders();
            fillOptions();
        }
        //Namrata: 16/10/2019
        Graphics graphics;
        private void FocusPictureBox(string id, bool IsFocused)
        {
            for (int i = 1; i <= 60; i++)
            {
                PictureBox pb = (PictureBox)ucdo.TblLayoutCSLD.Controls[i.ToString()];
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
            string strRoutineName = "DOConfiguration:CmbCellNo_SelectedIndexChanged";
            try
            {
                if (ucdo.CmbCellNo.Items.Count > 1)
                {
                    if ((ucdo.CmbCellNo.SelectedIndex != -1))
                    {
                        var rowColl = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable();

                        if (rowColl.Where(x => x["CellNo"].ToString() == ucdo.CmbCellNo.SelectedItem.ToString()).Any())
                        {
                            string Widget = rowColl.Where(x => x["CellNo"].ToString() == ucdo.CmbCellNo.SelectedItem.ToString()).Select(x => x["Widget"].ToString()).FirstOrDefault();
                            //string Widget = (from r in rowColl
                            //                 where r.Field<string>("CellNo") == ucdo.CmbCellNo.SelectedItem.ToString()
                            //                 select r.Field<string>("Widget")).First<string>();
                            ucdo.CmbWidget.Enabled = false;
                            ucdo.CmbWidget.SelectedIndex = ucdo.CmbWidget.FindStringExact(Widget);
                            ucdo.CmbWidget.SelectedItem = Widget;
                            //Namrata: 15/10/2019
                            FocusPictureBox(ucdo.CmbCellNo.Text, true);
                        }
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
            string strRoutineName = "DOConfiguration:CmbCellNo_SelectedIndexChanged";
            try
            {
                if (ucdo.CmbCellNo.Items.Count > 1)
                {
                    if ((ucdo.CmbCellNo.SelectedIndex != -1))
                    {
                        var rowColl = GDSlave.DtDIDOWidgets.AsEnumerable();
                        string Widget = (from r in rowColl
                                         where r.Field<string>("CellNo") == ucdo.CmbCellNo.SelectedItem.ToString()
                                         select r.Field<string>("Widget")).First<string>();
                        ucdo.CmbWidget.Enabled = false;
                        ucdo.CmbWidget.SelectedIndex = ucdo.CmbWidget.FindStringExact(Widget);
                        ucdo.CmbWidget.SelectedItem = Widget;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Ajay: 07/12/2018
            if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
            {
                if (AllowMasterOptionsOnClick(masterType)) { return; }
                else { }
            }

            if (doList.Count >= getMaxDOs())
            {
                MessageBox.Show("Maximum " + getMaxDOs() + " DO's are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            mode = Mode.ADD;
            editIndex = -1;
            #region Enable/Disable Events
            if (masterType == MasterTypes.IEC61850Client)
            {
                IEC61850Client_EventsOnLoad();
                FetchComboboxData();
            }
            else if ((masterType == MasterTypes.ADR) || (masterType == MasterTypes.IEC103) || (masterType == MasterTypes.MODBUS))
            {
                ADR_IEC103_MODBUS_MasterEventsOnLoad();
            }
            else if (masterType == MasterTypes.Virtual)
            {
                Virtual_MasterEventsOnLoad();
            }
            //Ajay: 18/07/2018 Separate condition handled for IEC104
            else if (masterType == MasterTypes.IEC101 || (masterType == MasterTypes.SPORT))
            {
                IEC101_SPORT_MasterEventsOnLoad();
            }
            else if (masterType == MasterTypes.IEC104)
            {
                IEC104_MasterEventsOnLoad();
            }
            //Ajay: 31/07/2018
            else if (masterType == MasterTypes.LoadProfile)
            {
                LoadProfile_OnLoad();
            }
            #endregion Enable/Disable Events
            Utils.resetValues(ucdo.grpDO);
            Utils.showNavigation(ucdo.grpDO, false);
            fillOptions(); //Ajay: 12/11/2018
            loadDefaults();
            ucdo.grpDO.Visible = true;
            ucdo.cmbResponseType.Focus();
            //Namrata: 04/04/2018
            if (masterType == MasterTypes.IEC61850Client)
            {
                if (ucdo.cmbIEDName.SelectedIndex != -1)
                {
                    ucdo.ChkIEC61850Index.Text = "";//Namrata:25/03/2019
                    if (IEC61850CheckedList != null)
                    {
                        ucdo.ChkIEC61850Index.CheckBoxItems.ForEach(x =>
                        {
                            x.Checked = false; x.CheckState = CheckState.Unchecked;
                        });
                    }
                    ucdo.cmbIEDName.SelectedIndex = ucdo.cmbIEDName.FindStringExact(Utils.Iec61850IEDname);
                }
                else
                {
                    ucdo.cmbIEDName.Visible = false;
                    MessageBox.Show("ICD File Missing !!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
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
            for (int i = ucdo.lvDOlist.Items.Count - 1; i >= 0; i--)
            {
                if (ucdo.lvDOlist.Items[i].Checked)
                {
                    if (Utils.IsExistDOinPLC(doList.ElementAt(i).DONo))
                    {
                        DialogResult ask = MessageBox.Show("DO " + doList.ElementAt(i).DONo + " is referred in ParameterLoadConfiguration and all the references will also be deleted." + Environment.NewLine + "Do you want to continue?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (ask == DialogResult.No)
                        {
                            continue;
                        }
                        Utils.DeleteDOFromPLC(doList.ElementAt(i).DONo);
                    }
                    deleteDOFromMaps(doList.ElementAt(i).DONo);
                    doList.RemoveAt(i);
                    ucdo.lvDOlist.Items[i].Remove();
                }
            }
            refreshList();
            refreshCurrentMapList(); //Refresh map listview...
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            int EnableDI = 0;
            int txtno = int.Parse(ucdo.txtAutoMap.Text);//Namrata:30/6/2017
            //if (!Validate()) return;
            List<KeyValuePair<string, string>> doData = Utils.getKeyValueAttributes(ucdo.grpDO);

            #region Fill Address to Datatable for RCBConfiguration 
            if (masterType == MasterTypes.IEC61850Client)//Namrata: 27/09/2017
            {
                IEC61850CheckedList = ucdo.ChkIEC61850Index.CheckBoxItems.Where(x => x.Checked == true).Select(x => x.Text).ToList();//Namrata:25/03/2019
                Response = ucdo.cmbIEC61850ResponseType.Text;
                ied = IEDNo.ToString(); DataColumn dcAddressColumn;
                if (!dtdataset.Columns.Contains("Address"))//Namrata: 15/03/2018
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
                if (Utils.dsRCBDO.Tables.Contains(dtdataset.TableName))
                {
                    Utils.dsRCBDO.Tables[dtdataset.TableName].Clear();
                }
                else
                {
                    Utils.dsRCBDO.Tables.Add(dtdataset.TableName);
                    Utils.dsRCBDO.Tables[dtdataset.TableName].Columns.Add("ObjectReferrence");
                    Utils.dsRCBDO.Tables[dtdataset.TableName].Columns.Add("Node");
                }
                for (int i = 0; i < dtdataset.Rows.Count; i++)
                {
                    row112 = Utils.dsRCBDO.Tables[dtdataset.TableName].NewRow();
                    Utils.dsRCBDO.Tables[dtdataset.TableName].NewRow();
                    for (int j = 0; j < dtdataset.Columns.Count; j++)
                    {
                        Index112 = dtdataset.Rows[i][j].ToString();
                        row112[j] = Index112.ToString();
                    }
                    Utils.dsRCBDO.Tables[dtdataset.TableName].Rows.Add(row112);
                }
                Utils.dsRCBData = Utils.dsRCBDO;
                Utils.dsRCBData.Merge(Utils.dsRCBAI, false, MissingSchemaAction.Add);
                Utils.dsRCBData.Merge(Utils.dsRCBAO, false, MissingSchemaAction.Add);
                Utils.dsRCBData.Merge(Utils.dsRCBDI, false, MissingSchemaAction.Add);
                Utils.dsRCBData.Merge(Utils.dsRCBEN, false, MissingSchemaAction.Add);
            }
            #endregion Fill Address to Datatable for RCBConfiguration 

            #region ADD
            if (mode == Mode.ADD)
            {
                int intDONO = Convert.ToInt32(doData[14].Value);
                int intAutoMapRange = 0;//Ajay: 31/07/2018
                int intReportingIndex = 0; //Ajay: 31/07/2018
                int DONo = 0, ReportingIndex = 0;
                string Description = "";
                if (IEC61850CheckedList != null)
                {
                    intCheckItems = IEC61850CheckedList.Count();
                }
                #region LoadProfile
                if (masterType == MasterTypes.LoadProfile) { intAutoMapRange = 1; }
                else { intAutoMapRange = Convert.ToInt32(doData[10].Value); } //AutoMapRange
                if (masterType != MasterTypes.LoadProfile)//Ajay: 18/07/2018 
                { intReportingIndex = Convert.ToInt32(doData[16].Value); }// DOIndex 
                #endregion LoadProfile

                #region getMaxDOs
                if (intAutoMapRange > getMaxDOs())//Ajay: 23/11/2017
                {
                    MessageBox.Show("Maximum " + getMaxDOs() + " DO's are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion getMaxDOs

                else
                {
                    #region IEC61850Client
                    if (masterType == MasterTypes.IEC61850Client)
                    {
                        int iListCount = 0;
                        for (int i = intDONO; i <= intDONO + intCheckItems - 1; i++)
                        {
                            Description = "";
                            DONo = Globals.DONo;
                            DONo += 1;
                            ReportingIndex = intReportingIndex++;

                            #region EnableDI
                            EnableDI = Convert.ToInt32(ucdo.txtEnableDI.Text);
                            if (EnableDI == 0)
                            {
                                EnableDI = Convert.ToInt32(ucdo.txtEnableDI.Text);
                            }
                            else if (!Utils.EnableDI.Where(x => x.DINo == EnableDI.ToString()).Select(x => x.DINo).Any()) //.ToList().Contains(EnableDI.ToString()))
                            {
                                MessageBox.Show("DI entry does not exist !!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            #endregion EnableDI

                            Description = ucdo.txtDescription.Text;
                            DO NewDO = new DO("DO", doData, null, masterType, masterNo, IEDNo);
                            NewDO.DONo = DONo.ToString();
                            NewDO.Index = ReportingIndex.ToString();
                            NewDO.IEDName = ucdo.cmbIEDName.Text.ToString();
                            NewDO.IEC61850ResponseType = ucdo.cmbIEC61850ResponseType.Text.ToString();
                            NewDO.IEC61850Index = IEC61850CheckedList[iListCount].ToString();
                            NewDO.EnableDI = EnableDI.ToString();
                            NewDO.CModel = ucdo.cmbCModel.Text; //Ajay: 04/08/2018
                            //string FindFC = MergeList.Where(x => x.Contains(IEC61850CheckedList[iListCount].ToString() + ",")).Select(x => x).FirstOrDefault();
                            //string[] GetFC = FindFC.Split(',');
                            //ucdo.cmbFC.SelectedIndex = ucdo.cmbFC.FindStringExact(GetFC[1].ToString());
                            NewDO.FC = ucdo.cmbFC.Text.ToString();
                            doList.Add(NewDO);
                            iListCount++;
                        }
                    }
                    #endregion IEC61850Client

                    #region Other Masters
                    else
                    {
                        for (int i = intDONO; i <= intDONO + intAutoMapRange - 1; i++)
                        {
                            DONo = Globals.DONo;
                            DONo += 1;
                            ReportingIndex = intReportingIndex++;
                            EnableDI = Convert.ToInt32(ucdo.txtEnableDI.Text);
                            if (EnableDI == 0)
                            {
                                EnableDI = Convert.ToInt32(ucdo.txtEnableDI.Text);
                            }
                            else if (!Utils.EnableDI.Where(x => x.DINo == EnableDI.ToString()).Select(x => x.DINo).Any()) //.ToList().Contains(EnableDI.ToString()))
                            {
                                MessageBox.Show("DI entry does not exist !!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (masterType == MasterTypes.ADR)
                            {
                                Description = ucdo.txtDescription.Text;
                            }
                            else if (masterType == MasterTypes.IEC101)
                            {
                                Description = ucdo.txtDescription.Text;
                            }
                            else if (masterType == MasterTypes.SPORT)
                            {
                                Description = ucdo.txtDescription.Text;
                            }
                            else if (masterType == MasterTypes.IEC103)
                            {
                                Description = ucdo.txtDescription.Text;
                            }
                            else if (masterType == MasterTypes.IEC104)
                            {
                                Description = ucdo.txtDescription.Text;
                            }
                            else if (masterType == MasterTypes.MODBUS)
                            {
                                Description = ucdo.txtDescription.Text;
                            }
                            else if (masterType == MasterTypes.LoadProfile)//Ajay: 31/07/2018
                            {
                                Description = ucdo.txtDescription.Text;
                            }
                            DO NewDO = new DO("DO", doData, null, masterType, masterNo, IEDNo);
                            NewDO.DONo = DONo.ToString();
                            NewDO.Index = ReportingIndex.ToString();
                            NewDO.EnableDI = EnableDI.ToString();
                            NewDO.CModel = ucdo.cmbCModel.Text; //Ajay: 04/08/2018
                            doList.Add(NewDO);
                        }
                    }
                    #endregion Other Masters
                }
            }
            #endregion ADD

            #region EDIT
            else if (mode == Mode.EDIT)
            {
                if (ucdo.ChkIEC61850Index.CheckBoxItems.Count > 0)
                {
                    if (ucdo.ChkIEC61850Index.CheckBoxItems.Where(x => x.Checked == true).Count() > 1)
                    {
                        MessageBox.Show("Please select only 1 index.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        doList[editIndex].updateAttributes(doData);
                    }
                }
                else
                {
                    doList[editIndex].updateAttributes(doData);
                }





                //doList[editIndex].updateAttributes(doData);
                EnableDI = Convert.ToInt32(ucdo.txtEnableDI.Text);

                if (EnableDI == 0)
                {
                    EnableDI = Convert.ToInt32(ucdo.txtEnableDI.Text);
                }
                else if (!Utils.EnableDI.Where(x => x.DINo == EnableDI.ToString()).Select(x => x.DINo).Any()) //.ToList().Contains(EnableDI.ToString()))
                {
                    MessageBox.Show("DI entry does not exist !!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            #endregion EDIT

            refreshList();
            if (masterType == MasterTypes.IEC61850Client)//Namrata: 15/03/2018
            {
                RCBNode = new RCBConfiguration(MasterTypes.IEC61850Client, Convert.ToInt32(Utils.MasterNumForIEC61850Client), Convert.ToInt32(Utils.UnitIDForIEC61850Client));
                RCBNode.FillRCBList();
            }
            if (sender != null && e != null)//Namrata: 27/7/2017
            {
                ucdo.grpDO.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ucdo.grpDO.Visible = false;
            mode = Mode.NONE;
            editIndex = -1;
            Utils.resetValues(ucdo.grpDO);
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucdo btnFirst_Click clicked in class!!!");
            if (ucdo.lvDOlist.Items.Count <= 0) return;
            if (doList.ElementAt(0).IsNodeComment) return;
            editIndex = 0;
            loadValues();
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            //Namrata:27/7/2017
            btnDone_Click(null, null);
            if (editIndex - 1 < 0) return;
            if (doList.ElementAt(editIndex - 1).IsNodeComment) return;
            editIndex--;
            loadValues();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            //Namrata:27/7/2017
            btnDone_Click(null, null);
            if (editIndex + 1 >= ucdo.lvDOlist.Items.Count) return;
            if (doList.ElementAt(editIndex + 1).IsNodeComment) return;
            editIndex++;
            loadValues();
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            if (ucdo.lvDOlist.Items.Count <= 0) return;
            if (doList.ElementAt(doList.Count - 1).IsNodeComment) return;
            editIndex = doList.Count - 1;
            loadValues();
        }
        private void LinkDeleteConfigue_Click(object sender, EventArgs e)
        {
            // Ajay: 07/12/2018
            if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
            {
                if (AllowMasterOptionsOnClick(masterType)) { return; }
                else { }
            }

            foreach (ListViewItem listItem in ucdo.lvDOlist.Items)
            {
                listItem.Checked = true;
            }
            DialogResult result = MessageBox.Show("Do You Want To Delete All Records ? ", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.No)
            {
                foreach (ListViewItem listItem in ucdo.lvDOlist.Items)
                {
                    listItem.Checked = false;
                }
                return;
            }
            for (int i = ucdo.lvDOlist.Items.Count - 1; i >= 0; i--)
            {
                Console.WriteLine("*** removing indices: {0}", i);
                if (Utils.IsExistDOinPLC(doList.ElementAt(i).DONo))
                {
                    DialogResult ask = MessageBox.Show("DO " + doList.ElementAt(i).DONo + " is referred in ParameterLoadConfiguration and all the references will also be deleted." + Environment.NewLine + "Do you want to continue?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (ask == DialogResult.No)
                    {
                        continue;
                    }
                    Utils.DeleteDOFromPLC(doList.ElementAt(i).DONo);
                }
                deleteDOFromMaps(doList.ElementAt(i).DONo);
                doList.RemoveAt(i);
                ucdo.lvDOlist.Items.Clear();
            }
            refreshList();
            refreshCurrentMapList();  //Refresh map listview...
        }
        private void linkDoMap_Click(object sender, EventArgs e)
        {
            // Ajay: 07/12/2018
            if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
            {
                if (AllowSlaveOptionsOnClick(Utils.getSlaveTypes(currentSlave))) { return; }
                else { }
            }

            List<DOMap> slaveDOMapList;
            if (!slavesDOMapList.TryGetValue(currentSlave, out slaveDOMapList))
            {
                MessageBox.Show("Error deleting DO map!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (ListViewItem listItem in ucdo.lvDOMap.Items)
            {
                listItem.Checked = true;
            }
            DialogResult result = MessageBox.Show("Do You Want To Delete All Records ? ", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.No)
            {
                foreach (ListViewItem listItem in ucdo.lvDOMap.Items)
                {
                    listItem.Checked = false;
                }
                return;
            }
            for (int i = ucdo.lvDOMap.Items.Count - 1; i >= 0; i--)
            {
                if (GDSlave.DsExcelData.Tables.Count > 0)
                {
                    //Namrata: 04/02/2019
                    var results = from row in GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable()
                                  where row.Field<string>("CellNo") == slaveDOMapList[i].CellNo && row.Field<string>("Widget") == slaveDOMapList[i].Widget && row.Field<string>("Type") == "DO"
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
                slaveDOMapList.RemoveAt(i);
                ucdo.lvDOMap.Items.Clear();
            }
            refreshMapList(slaveDOMapList);
        }
        private void lvDOlist_DoubleClick1(object sender, EventArgs e)
        {
            if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)// Ajay: 07/12/2018
            {
                if (AllowSlaveOptionsOnClick(Utils.getSlaveTypes(currentSlave))) { return; }
                else { }
            }

            if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)// Ajay: 07/12/2018
            {
                if (AllowMasterOptionsOnClick(masterType)) { return; }
                else { }
            }
            ucdo.txtAutoMap.Text = "0";//Namrata: 10/09/2017
            int ListIndex = ucdo.lvDOlist.FocusedItem.Index;//Namrata: 07/03/2018
            ListViewItem lvi = ucdo.lvDOlist.Items[ListIndex];
            Utils.UncheckOthers(ucdo.lvDOlist, lvi.Index);
            if (doList.ElementAt(lvi.Index).IsNodeComment)
            {
                MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #region Enable/Disable Events
            if ((masterType == MasterTypes.ADR) || (masterType == MasterTypes.IEC103) || (masterType == MasterTypes.MODBUS))
            {
                //Ajay: 18/07/2018
                ADR_IEC103_Modbus_OnDoubleClick();
            }
            else if ((masterType == MasterTypes.IEC101) || (masterType == MasterTypes.SPORT))
            {
                IEC101_SPORT_OnDoubleClick();
            }
            else if (masterType == MasterTypes.IEC104)
            {
                IEC104_OnDoubleClick();
            }
            if (masterType == MasterTypes.IEC61850Client)
            {
                IEC61850_OnDoubleClick(); FetchComboboxData();
                ucdo.cmbIEDName.SelectedIndex = ucdo.cmbIEDName.FindStringExact(Utils.Iec61850IEDname); //Namrata: 04/04/2018
            }
            //Ajay: 31/07/2018
            if (masterType == MasterTypes.LoadProfile)
            {
                LoadProfile_OnDoubleClick();
            }
            #endregion Enable/Disable Events

            ucdo.grpDO.Visible = true;
            mode = Mode.EDIT;
            editIndex = lvi.Index;

            //Namrata:25/03/2019
            ucdo.ChkIEC61850Index.Text = "";
            if (IEC61850CheckedList != null)
            {
                ucdo.ChkIEC61850Index.CheckBoxItems.ForEach(x =>
                {
                    x.Checked = false; x.CheckState = CheckState.Unchecked;
                });
            }
            if (ucdo.ChkIEC61850Index.CheckBoxItems.Count > 0)
            {
                ucdo.ChkIEC61850Index.Text = doList[lvi.Index].IEC61850Index.ToString();
            }
            Utils.showNavigation(ucdo.grpDO, true);
            loadValues();
            if (ucdo.ChkIEC61850Index.CheckBoxItems.Count > 0)
            {
                //Namrata:26/03/2019
                ucdo.ChkIEC61850Index.CheckBoxItems.Where(x => x.Text == doList[lvi.Index].IEC61850Index.ToString()).Take(1).ToList().ForEach(x =>
                {
                    x.Checked = true; x.CheckState = CheckState.Checked;
                });
            }
            if (ucdo.ChkIEC61850Index.CheckBoxItems.Where(x => x.Checked == true).Count() > 1)
            {
                MessageBox.Show("Please select only 1 index.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ucdo.cmbResponseType.Focus();
        }
        private void lvDOlist_DoubleClick(object sender, EventArgs e)
        {
            if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)// Ajay: 07/12/2018
            {
                if (AllowMasterOptionsOnClick(masterType)) { return; }
                else { }
            }
            ucdo.txtAutoMap.Text = "0";//Namrata: 10/09/2017
            int ListIndex = ucdo.lvDOlist.FocusedItem.Index;//Namrata: 07/03/2018
            ListViewItem lvi = ucdo.lvDOlist.Items[ListIndex];
            Utils.UncheckOthers(ucdo.lvDOlist, lvi.Index);
            if (doList.ElementAt(lvi.Index).IsNodeComment)
            {
                MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #region Enable/Disable Events
            if ((masterType == MasterTypes.ADR) || (masterType == MasterTypes.IEC103) || (masterType == MasterTypes.MODBUS))
            {
                //Ajay: 18/07/2018
                ADR_IEC103_Modbus_OnDoubleClick();
            }
            else if ((masterType == MasterTypes.IEC101) || (masterType == MasterTypes.SPORT))
            {
                IEC101_SPORT_OnDoubleClick();
            }
            else if (masterType == MasterTypes.IEC104)
            {
                IEC104_OnDoubleClick();
            }
            if (masterType == MasterTypes.IEC61850Client)
            {
                IEC61850_OnDoubleClick(); FetchComboboxData();
                ucdo.cmbIEDName.SelectedIndex = ucdo.cmbIEDName.FindStringExact(Utils.Iec61850IEDname); //Namrata: 04/04/2018
            }
            //Ajay: 31/07/2018
            if (masterType == MasterTypes.LoadProfile)
            {
                LoadProfile_OnDoubleClick();
            }
            #endregion Enable/Disable Events
            ucdo.grpDO.Visible = true;
            mode = Mode.EDIT;
            editIndex = lvi.Index;
            //Namrata:12/04/2019
            ucdo.ChkIEC61850Index.Text = "";
            if ((IEC61850CheckedList != null) || (IEC61850CheckedList == null))
            {
                ucdo.ChkIEC61850Index.CheckBoxItems.ForEach(x =>
                {
                    x.Checked = false; x.CheckState = CheckState.Unchecked;
                });
            }
            if (ucdo.ChkIEC61850Index.CheckBoxItems.Count > 0)
            {
                ucdo.ChkIEC61850Index.Text = doList[lvi.Index].IEC61850Index.ToString();
            }
            loadValues();
            if (ucdo.ChkIEC61850Index.CheckBoxItems.Count > 0)
            {
                //Namrata:26/03/2019
                ucdo.ChkIEC61850Index.CheckBoxItems.Where(x => x.Text == doList[lvi.Index].IEC61850Index.ToString()).Take(1).ToList().ForEach(x =>
                {
                    x.Checked = true; x.CheckState = CheckState.Checked;
                });
            }
            ucdo.cmbResponseType.Focus();
        }
        private void lvDOlist_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string strRoutineName = "lvDOlist_ItemSelectionChanged";
            try
            {
                if (e.IsSelected)
                {
                    Color GreenColour = Color.FromArgb(34, 217, 0);
                    string diIndex = e.Item.Text;
                    Console.WriteLine("*** selected DI: {0}", diIndex);
                    //Namrata: 27/7/2017
                    ucdo.lvDOMapItemSelectionChanged -= new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDOMap_ItemSelectionChanged);
                    ucdo.lvDOMap.SelectedItems.Clear();   //Remove selection from DIMap...
                    ucdo.lvDOMap.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window); //Namrata: 07/04/2018
                    ucdo.lvDOMap.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour)); //Namrata: 07/04/2018
                    Utils.highlightListviewItem(diIndex, ucdo.lvDOMap);
                    //Namrata: 27/7/2017
                    ucdo.lvDOMap.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);//Namrata: 07/04/2018
                    ucdo.lvDOlist.SelectedItems.Clear();
                    ucdo.lvDOlist.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                    ucdo.lvDOlist.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                    ucdo.lvDOlist.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);
                    ucdo.lvDOMapItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDOMap_ItemSelectionChanged);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Namrata: 27/7/2017
        private void lvDOMap_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string strRoutineName = "lvDOMap_ItemSelectionChanged";
            try
            {
                if (e.IsSelected)
                {
                    Color GreenColour = Color.FromArgb(34, 217, 0);
                    string diIndex = e.Item.Text;
                    Console.WriteLine("*** selected DI: {0}", diIndex);
                    //Namrata: 27/7/2017
                    ucdo.lvDOlistItemSelectionChanged -= new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDOlist_ItemSelectionChanged);
                    ucdo.lvDOlist.SelectedItems.Clear();   //Remove selection from DIMap...
                    ucdo.lvDOlist.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window); //Namrata: 07/04/2018
                    ucdo.lvDOlist.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour)); //Namrata: 07/04/2018
                    Utils.highlightListviewItem(diIndex, ucdo.lvDOlist);
                    //Namrata:lvAIlist 27/7/2017
                    ucdo.lvDOlist.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);//Namrata: 07/04/2018
                    ucdo.lvDOMap.SelectedItems.Clear();
                    ucdo.lvDOMap.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                    ucdo.lvDOMap.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                    ucdo.lvDOMap.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);
                    ucdo.lvDOlistItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDOlist_ItemSelectionChanged);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadDefaults()
        {
            ucdo.txtAutoMap.Text = "1";
            ucdo.txtDONo.Text = (Globals.DONo + 1).ToString();
            ucdo.txtIndex.Text = (Globals.DOIndex + 1).ToString();
            ucdo.txtIndex.Text = "1";
            ucdo.txtSubIndex.Text = "0";
            ucdo.txtEnableDI.Text = "0";

            ////Namrata: 24/05/2017
            ucdo.txtPulseDuration.Text = "100";
            if (masterType == MasterTypes.ADR)
            {
                if (ucdo.lvDOlist.Items.Count - 1 >= 0)
                {
                    ucdo.txtIndex.Text = Convert.ToString(Convert.ToInt32(doList[doList.Count - 1].Index) + 1);
                }
                ucdo.cmbResponseType.SelectedIndex = ucdo.cmbResponseType.FindStringExact("ADR_DO_AI");
                ucdo.txtDescription.Text = "ADR_DO";// + (Globals.DONo + 1).ToString();
            }
            else if (masterType == MasterTypes.IEC101)
            {
                if (ucdo.lvDOlist.Items.Count - 1 >= 0)
                {
                    ucdo.txtIndex.Text = Convert.ToString(Convert.ToInt32(doList[doList.Count - 1].Index) + 1);
                }
                ucdo.cmbResponseType.SelectedIndex = ucdo.cmbResponseType.FindStringExact("DoubleCommand");
                ucdo.txtDescription.Text = "IEC101_DO";// + (Globals.DONo + 1).ToString();
            }
            //Ajay:06/07/2018
            else if (masterType == MasterTypes.IEC104)
            {
                if (ucdo.lvDOlist.Items.Count - 1 >= 0)
                {
                    ucdo.txtIndex.Text = Convert.ToString(Convert.ToInt32(doList[doList.Count - 1].Index) + 1);
                }
                ucdo.cmbResponseType.SelectedIndex = ucdo.cmbResponseType.FindStringExact("DoubleCommand");
                ucdo.txtDescription.Text = "IEC104_DO";// + (Globals.DONo + 1).ToString();
            }
            //Ajay:06/07/2018
            else if (masterType == MasterTypes.SPORT)
            {
                if (ucdo.lvDOlist.Items.Count - 1 >= 0)
                {
                    ucdo.txtIndex.Text = Convert.ToString(Convert.ToInt32(doList[doList.Count - 1].Index) + 1);
                }
                //Ajay: 12/11/2018
                if (ucdo.cmbResponseType != null && ucdo.cmbResponseType.Items.Count > 0)
                {
                    ucdo.cmbResponseType.SelectedIndex = 0; // ucdo.cmbResponseType.FindStringExact("DoubleCommand");
                }
                ucdo.txtDescription.Text = "SPORT_DO";// + (Globals.DONo + 1).ToString();
            }
            else if (masterType == MasterTypes.IEC103)
            {
                if (ucdo.lvDOlist.Items.Count - 1 >= 0)
                {
                    ucdo.txtIndex.Text = Convert.ToString(Convert.ToInt32(doList[doList.Count - 1].Index) + 1);
                }
                ucdo.cmbResponseType.SelectedIndex = ucdo.cmbResponseType.FindStringExact("GeneralCommand");
                ucdo.txtDescription.Text = "IEC103_DO";// + (Globals.DONo + 1).ToString();
            }

            else if (masterType == MasterTypes.MODBUS)
            {
                if (ucdo.lvDOlist.Items.Count - 1 >= 0)
                {
                    ucdo.txtIndex.Text = Convert.ToString(Convert.ToInt32(doList[doList.Count - 1].Index) + 1);
                }
                ucdo.cmbResponseType.SelectedIndex = ucdo.cmbResponseType.FindStringExact("WriteSingleCoil");
                ucdo.txtDescription.Text = "MODBUS_DO";// + (Globals.DONo + 1).ToString();
            }
            else if (masterType == MasterTypes.IEC61850Client)
            {
                if (ucdo.lvDOlist.Items.Count - 1 >= 0)
                {
                    ucdo.txtIndex.Text = Convert.ToString(Convert.ToInt32(doList[doList.Count - 1].Index) + 1);
                }
                ucdo.cmbResponseType.SelectedIndex = ucdo.cmbResponseType.FindStringExact("WriteSingleCoil");
                ucdo.txtDescription.Text = "IEC61850Client_DO";// + (Globals.DONo + 1).ToString();
            }
            //Ajay: 31/07/2018
            else if (masterType == MasterTypes.LoadProfile)
            {
                if (ucdo.lvDOlist.Items.Count - 1 >= 0)
                {
                    ucdo.txtIndex.Text = Convert.ToString(Convert.ToInt32(doList[doList.Count - 1].Index) + 1);
                }
                ucdo.txtDescription.Text = "LoadProfile_DO";
            }
        }
        private void loadValues()
        {
            DO doe = doList.ElementAt(editIndex);
            if (doe != null)
            {
                ucdo.txtDONo.Text = doe.DONo;
                ucdo.cmbResponseType.SelectedIndex = ucdo.cmbResponseType.FindStringExact(doe.ResponseType);
                ucdo.txtIndex.Text = doe.Index;
                ucdo.txtSubIndex.Text = doe.SubIndex;
                ucdo.cmbControlType.SelectedIndex = ucdo.cmbControlType.FindStringExact(doe.ControlType);
                ucdo.txtPulseDuration.Text = doe.PulseDurationMS;
                ucdo.txtDescription.Text = doe.Description;
                ucdo.txtEnableDI.Text = doe.EnableDI;
                ucdo.cmbIEDName.SelectedIndex = ucdo.cmbIEDName.FindStringExact(Utils.Iec61850IEDname);//Namrata: 16/03/2018
                ucdo.cmbFC.SelectedIndex = ucdo.cmbFC.FindStringExact(doe.FC); //Ajay: 17/01/2019
                if (doe.Select.ToLower() == "enable") ucdo.ChkEnableDisable.Checked = true;
                else ucdo.ChkEnableDisable.Checked = false;
                if (doe.Event.ToLower() == "yes")//Ajay: 18/07/2018
                { ucdo.ChkEvent.Checked = true; }
                else { ucdo.ChkEvent.Checked = false; }
                ucdo.cmbCModel.SelectedIndex = ucdo.cmbCModel.FindStringExact(doe.CModel);//Ajay: 04/08/2018
                ucdo.ChkIEC61850Index.SelectedIndex = ucdo.ChkIEC61850Index.FindStringExact(doe.IEC61850Index);//Namrata:27/03/2019
            }
        }
        private bool Validate()
        {
            bool status = true;
            //Check empty field's
            if (Utils.IsEmptyFields(ucdo.grpDO))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return status;
        }
        private void refreshList()
        {
            int cnt = 0;
            ucdo.lvDOlist.Items.Clear();
            Utils.DolistforDescription.Clear();


            if (ucdo.lvDOlist.InvokeRequired)
            {
                ucdo.lvDOlist.Invoke(new MethodInvoker(delegate
                {
                    #region MasterTypes.ADR,MasterTypes.IEC103,MasterTypes.MODBUS, MasterTypes.Virtual
                    if ((masterType == MasterTypes.ADR) || (masterType == MasterTypes.IEC103) || (masterType == MasterTypes.MODBUS) || (masterType == MasterTypes.Virtual))
                    {
                        foreach (DO don in doList)
                        {
                            string[] row = new string[8];
                            if (don.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = don.DONo;
                                row[1] = don.ResponseType;
                                row[2] = don.Index;
                                row[3] = don.SubIndex;
                                row[4] = don.ControlType;
                                row[5] = don.PulseDurationMS;
                                row[6] = don.EnableDI;
                                row[7] = don.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucdo.lvDOlist.Items.Add(lvItem);
                        }
                    }
                    #endregion MasterTypes.ADR,MasterTypes.IEC103,MasterTypes.MODBUS, MasterTypes.Virtual

                    #region MasterTypes.IEC101,MasterTypes.SPORT
                    //Ajay: 18/07/2018 Separate condition handled for IEC104
                    if ((masterType == MasterTypes.IEC101) || (masterType == MasterTypes.SPORT))
                    {
                        foreach (DO don in doList)
                        {
                            string[] row = new string[9];
                            if (don.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = don.DONo;
                                row[1] = don.ResponseType;
                                row[2] = don.Index;
                                row[3] = don.SubIndex;
                                row[4] = don.ControlType;
                                row[5] = don.PulseDurationMS;
                                row[6] = don.EnableDI;
                                row[7] = don.Select;
                                row[8] = don.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucdo.lvDOlist.Items.Add(lvItem);
                            Utils.DolistDONo.AddRange(doList);
                        }
                    }
                    #endregion MasterTypes.IEC101,MasterTypes.SPORT

                    #region MasterTypes.IEC104
                    //Ajay: 18/07/2018 
                    if (masterType == MasterTypes.IEC104)
                    {
                        foreach (DO don in doList)
                        {
                            string[] row = new string[9];
                            //string[] row = new string[10]; //Event Added
                            if (don.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = don.DONo;
                                row[1] = don.ResponseType;
                                row[2] = don.Index;
                                row[3] = don.SubIndex;
                                row[4] = don.ControlType;
                                row[5] = don.PulseDurationMS;
                                row[6] = don.EnableDI;
                                row[7] = don.Select;
                                //Ajay: 12/11/2018 Event Removed
                                //row[8] = don.Event; //Ajay: 18/07/2018 Event Added 
                                row[8] = don.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucdo.lvDOlist.Items.Add(lvItem);
                        }
                    }
                    #endregion MasterTypes.IEC104

                    #region MasterTypes.IEC61850Client
                    if (masterType == MasterTypes.IEC61850Client)
                    {
                        foreach (DO don in doList)
                        {
                            string[] row = new string[11]; //[10]
                            if (don.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = don.DONo;
                                row[1] = don.IEDName;
                                row[2] = don.IEC61850ResponseType;
                                row[3] = don.IEC61850Index;
                                row[4] = don.CModel; //Ajay: 04/08/2018 CModel added
                                row[5] = don.FC;
                                row[6] = don.SubIndex;
                                row[7] = don.ControlType;
                                row[8] = don.PulseDurationMS;
                                row[9] = don.EnableDI;
                                row[10] = don.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucdo.lvDOlist.Items.Add(lvItem);
                        }
                    }
                    #endregion MasterTypes.IEC61850Client

                    #region MasterTypes.LoadProfile
                    //Ajay: 31/07/2018
                    if (masterType == MasterTypes.LoadProfile)
                    {
                        foreach (DO don in doList)
                        {
                            string[] row = new string[2];
                            if (don.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = don.DONo;
                                row[1] = don.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucdo.lvDOlist.Items.Add(lvItem);
                        }
                    }
                    #endregion MasterTypes.LoadProfile
                }));
            }
            else
            {
                #region MasterTypes.ADR,MasterTypes.IEC103,MasterTypes.MODBUS, MasterTypes.Virtual
                if ((masterType == MasterTypes.ADR) || (masterType == MasterTypes.IEC103) || (masterType == MasterTypes.MODBUS) || (masterType == MasterTypes.Virtual))
                {
                    foreach (DO don in doList)
                    {
                        string[] row = new string[8];
                        if (don.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = don.DONo;
                            row[1] = don.ResponseType;
                            row[2] = don.Index;
                            row[3] = don.SubIndex;
                            row[4] = don.ControlType;
                            row[5] = don.PulseDurationMS;
                            row[6] = don.EnableDI;
                            row[7] = don.Description;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucdo.lvDOlist.Items.Add(lvItem);
                    }
                }
                #endregion MasterTypes.ADR,MasterTypes.IEC103,MasterTypes.MODBUS, MasterTypes.Virtual

                #region MasterTypes.IEC101,MasterTypes.SPORT
                //Ajay: 18/07/2018 Separate condition handled for IEC104
                if ((masterType == MasterTypes.IEC101) || (masterType == MasterTypes.SPORT))
                {
                    foreach (DO don in doList)
                    {
                        string[] row = new string[9];
                        if (don.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = don.DONo;
                            row[1] = don.ResponseType;
                            row[2] = don.Index;
                            row[3] = don.SubIndex;
                            row[4] = don.ControlType;
                            row[5] = don.PulseDurationMS;
                            row[6] = don.EnableDI;
                            row[7] = don.Select;
                            row[8] = don.Description;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucdo.lvDOlist.Items.Add(lvItem);
                        Utils.DolistDONo.AddRange(doList);
                    }
                }
                #endregion MasterTypes.IEC101,MasterTypes.SPORT

                #region MasterTypes.IEC104
                //Ajay: 18/07/2018 
                if (masterType == MasterTypes.IEC104)
                {
                    foreach (DO don in doList)
                    {
                        string[] row = new string[9];
                        //string[] row = new string[10]; //Event Added
                        if (don.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = don.DONo;
                            row[1] = don.ResponseType;
                            row[2] = don.Index;
                            row[3] = don.SubIndex;
                            row[4] = don.ControlType;
                            row[5] = don.PulseDurationMS;
                            row[6] = don.EnableDI;
                            row[7] = don.Select;
                            //Ajay: 12/11/2018 Event Removed
                            //row[8] = don.Event; //Ajay: 18/07/2018 Event Added 
                            row[8] = don.Description;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucdo.lvDOlist.Items.Add(lvItem);
                    }
                }
                #endregion MasterTypes.IEC104

                #region MasterTypes.IEC61850Client
                if (masterType == MasterTypes.IEC61850Client)
                {
                    foreach (DO don in doList)
                    {
                        string[] row = new string[11]; //[10]
                        if (don.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = don.DONo;
                            row[1] = don.IEDName;
                            row[2] = don.IEC61850ResponseType;
                            row[3] = don.IEC61850Index;
                            row[4] = don.CModel; //Ajay: 04/08/2018 CModel added
                            row[5] = don.FC;
                            row[6] = don.SubIndex;
                            row[7] = don.ControlType;
                            row[8] = don.PulseDurationMS;
                            row[9] = don.EnableDI;
                            row[10] = don.Description;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucdo.lvDOlist.Items.Add(lvItem);
                    }
                }
                #endregion MasterTypes.IEC61850Client

                #region MasterTypes.LoadProfile
                //Ajay: 31/07/2018
                if (masterType == MasterTypes.LoadProfile)
                {
                    foreach (DO don in doList)
                    {
                        string[] row = new string[2];
                        if (don.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = don.DONo;
                            row[1] = don.Description;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucdo.lvDOlist.Items.Add(lvItem);
                    }
                }
                #endregion MasterTypes.LoadProfile
            }
            ucdo.lblDORecords.Text = doList.Count.ToString();
            Utils.DolistRegenerateIndex.AddRange(doList);
            Utils.DolistforDescription.AddRange(doList);
        }
        private int getMaxDOs()
        {
            if (masterType == MasterTypes.IEC103) return Globals.MaxIEC103DO;
            else if (masterType == MasterTypes.MODBUS) return Globals.MaxMODBUSDO;
            //Namrata:13/7/2017
            else if (masterType == MasterTypes.ADR) return Globals.MaxADRDO;
            else if (masterType == MasterTypes.IEC101) return Globals.MaxIEC101DO;
            else if (masterType == MasterTypes.IEC61850Client) return Globals.MaxIEC101DO1;
            else if (masterType == MasterTypes.IEC104) return Globals.MaxIEC104MasterDO;
            else if (masterType == MasterTypes.SPORT) return Globals.MaxSPORTMasterDO;
            else if (masterType == MasterTypes.LoadProfile) return Globals.MaxLoadProfileDO; //Ajay: 31/07/2018
            else return 0;
        }
        //Ajay: 07/12/2018
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
        //Ajay: 07/12/2018
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
        }
        public void FetchCheckboxData()
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
                ucdo.ChkIEC61850Index.Items.Clear();
                foreach (var kv in ObjectRef)
                {
                    ucdo.ChkIEC61850Index.Items.Add(kv.ToString());
                }
            }
        }
        private void FetchComboboxData()
        {
            //Namrata: 13/03/2018
            ucdo.cmbIEDName.DataSource = null;
            List<string> tblNameList = Utils.dsIED.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
            string tblName = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
            //Namrata: 26/04/2018
            if (tblName != null)
            {
                ucdo.cmbIEDName.DataSource = Utils.dsIED.Tables[tblName];
                ucdo.cmbIEDName.DisplayMember = "IEDName";
                ucdo.cmbIEC61850ResponseType.DataSource = Utils.dsResponseType.Tables[tblName];//Namrata: 21/03/2018
                ucdo.cmbIEC61850ResponseType.DisplayMember = "Address";
                FetchCheckboxData();//Namrata:27/03/2019
            }
            else { }
        }

        //-------------------------------------AI Map Logic----------------------------------------------------------------
        //Namrata:12/04/2019
        private void GetIndexForIEC61850Server()
        {
            string strRoutineName = "DOConfiguration: GetIndexForIEC61850Server";
            try
            {
                Utils.CurrentSlaveFinal = currentSlave;
                ucdo.ChkIEC61850MapIndex.DataSource = null;
                List<string> List = new List<string>();
                if (Utils.DSDOSlave.Tables.Count > 0)
                {
                    List = (from r in Utils.DSDOSlave.Tables[Utils.CurrentSlaveFinal].AsEnumerable() select r.Field<string>("ObjectReferrence")).ToList();
                    ObjectRefForMap = ((DataTable)Utils.DSDOSlave.Tables[Utils.CurrentSlaveFinal]).AsEnumerable().Select(x => x[0].ToString()).ToList();
                    NodeForMap = ((DataTable)Utils.DSDOSlave.Tables[Utils.CurrentSlaveFinal]).AsEnumerable().Select(x => x[1].ToString()).ToList();
                    var MergeNodeObjeRefList = ObjectRefForMap.Zip(NodeForMap, (leftTooth, rightTooth) => leftTooth + "," + rightTooth).ToList();//Merge List
                    MergeListMap = MergeNodeObjeRefList;
                    ucdo.ChkIEC61850MapIndex.Items.Clear();
                    foreach (var kv in ObjectRefForMap)
                    {
                        ucdo.ChkIEC61850MapIndex.Items.Add(kv.ToString());
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CreateNewSlave(string slaveNum, string slaveID, XmlNode domNode)
        {
            List<DOMap> sdomList = new List<DOMap>();
            slavesDOMapList.Add(slaveID, sdomList);
            if (domNode != null)
            {
                foreach (XmlNode node in domNode)
                {
                    if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                    sdomList.Add(new DOMap(node, Utils.getSlaveTypes(slaveID)));
                }
            }
            AddMap2SlaveButton(Int32.Parse(slaveNum), slaveID);
            //Namrata: 24/02/2018
            //If Description attribute not exist in XML 
            sdomList.AsEnumerable().ToList().ForEach(item =>
            {
                string strDONo = item.DONo;
                if (string.IsNullOrEmpty(item.Description)) //Ajay: 05/01/2018 
                {
                    item.Description = Utils.DolistforDescription.AsEnumerable().Where(x => x.DONo == strDONo).Select(x => x.Description).FirstOrDefault();
                }
            });
            GDSlave.CurrentSlave = "GraphicalDisplaySlave_" + slaveNum + "_" + GDSlave.XLSFileName; //GraphicalDisplaySlave_1_5x8 SingleBUS_1.txt
           
            currentSlave = slaveID; refreshMapList(sdomList);
            
        }
        private void DeleteSlave(string slaveID)
        {
            slavesDOMapList.Remove(slaveID);
            RadioButton rb = null;
            foreach (Control ctrl in ucdo.flpMap2Slave.Controls)
            {
                if (ctrl.Tag.ToString() == slaveID)
                {
                    rb = (RadioButton)ctrl;
                    break;
                }
            }
            if (rb != null) ucdo.flpMap2Slave.Controls.Remove(rb);
        }
        //Namrata: 30/05/2019
        private void CheckSMSSlaveStatusChanges()
        {
            string strRoutineName = "DOConfiguration:CheckSMSSlaveStatusChanges";
            try
            {

                //Check for slave addition...
                foreach (SMSSlave slvSMS in Utils.getOpenProPlusHandle().getSlaveConfiguration().getSMSSlaveGroup().getSMSSlaves())
                {
                    string slaveID = "SMSSlave_" + slvSMS.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<DOMap>> sn in slavesDOMapList)
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
                foreach (KeyValuePair<string, List<DOMap>> sain in slavesDOMapList)//Loop thru slaves...
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
                    if (ucdo.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucdo.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucdo.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucdo.lvDOMap.Items.Clear();
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
            string strRoutineName = "DOConfiguration:CheckMQTTSlaveStatusChanges";
            try
            {

                //Check for slave addition...
                foreach (MQTTSlave slvMQTT in Utils.getOpenProPlusHandle().getSlaveConfiguration().getMQTTSlaveGroup().getMQTTSlaves())
                {
                    string slaveID = "MQTTSlave_" + slvMQTT.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<DOMap>> sn in slavesDOMapList)
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
                foreach (KeyValuePair<string, List<DOMap>> sain in slavesDOMapList)//Loop thru slaves...
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
                    if (ucdo.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucdo.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucdo.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucdo.lvDOMap.Items.Clear();
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
            //Check for slave addition...
            foreach (IEC104Slave slv104 in Utils.getOpenProPlusHandle().getSlaveConfiguration().getIEC104Group().getIEC104Slaves())//Loop thru slaves...
            {
                string slaveID = "IEC104_" + slv104.SlaveNum;
                bool slaveAdded = true;
                foreach (KeyValuePair<string, List<DOMap>> sn in slavesDOMapList)
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
            foreach (KeyValuePair<string, List<DOMap>> sdon in slavesDOMapList)//Loop thru slaves...
            {
                string slaveID = sdon.Key;
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
                if (ucdo.flpMap2Slave.Controls.Count > 0)
                {
                    ((RadioButton)ucdo.flpMap2Slave.Controls[0]).Checked = true;
                    currentSlave = ((RadioButton)ucdo.flpMap2Slave.Controls[0]).Tag.ToString();
                    refreshCurrentMapList();
                }
                else
                {
                    ucdo.lvDOMap.Items.Clear();
                    currentSlave = "";
                }
            }
            fillMapOptions(Utils.getSlaveTypes(currentSlave));
        }
        //Namrata:13/7/2017
        private void CheckIEC101SlaveStatusChanges()
        {
            //Check for slave addition...
            foreach (IEC101Slave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().getIEC101SlaveGroup().getIEC101Slaves())
            {
                string slaveID = "IEC101Slave_" + slvMB.SlaveNum;
                bool slaveAdded = true;
                foreach (KeyValuePair<string, List<DOMap>> sn in slavesDOMapList)
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
            foreach (KeyValuePair<string, List<DOMap>> sain in slavesDOMapList)//Loop thru slaves...
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
                if (ucdo.flpMap2Slave.Controls.Count > 0)
                {
                    ((RadioButton)ucdo.flpMap2Slave.Controls[0]).Checked = true;
                    currentSlave = ((RadioButton)ucdo.flpMap2Slave.Controls[0]).Tag.ToString();
                    refreshCurrentMapList();
                }
                else
                {
                    ucdo.lvDOMap.Items.Clear();
                    currentSlave = "";
                }
            }
            fillMapOptions(Utils.getSlaveTypes(currentSlave));
        }
        private void CheckMODBUSSlaveStatusChanges()
        {
            //Check for slave addition...
            foreach (MODBUSSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().getMODBUSSlaveGroup().getMODBUSSlaves())//Loop thru slaves...
            {
                string slaveID = "MODBUSSlave_" + slvMB.SlaveNum;
                bool slaveAdded = true;
                foreach (KeyValuePair<string, List<DOMap>> sn in slavesDOMapList)
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
            foreach (KeyValuePair<string, List<DOMap>> sdon in slavesDOMapList)//Loop thru slaves...
            {
                string slaveID = sdon.Key;
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
                if (ucdo.flpMap2Slave.Controls.Count > 0)
                {
                    ((RadioButton)ucdo.flpMap2Slave.Controls[0]).Checked = true;
                    currentSlave = ((RadioButton)ucdo.flpMap2Slave.Controls[0]).Tag.ToString();
                    refreshCurrentMapList();
                }
                else
                {
                    ucdo.lvDOMap.Items.Clear();
                    currentSlave = "";
                }
            }
            fillMapOptions(Utils.getSlaveTypes(currentSlave));
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
                    foreach (KeyValuePair<string, List<DOMap>> sdon in slavesDOMapList)//Loop thru slaves...
                    {
                        if (sdon.Key == slaveID)
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
                foreach (KeyValuePair<string, List<DOMap>> sdon1 in slavesDOMapList)//Loop thru slaves...
                {
                    string slaveID = sdon1.Key;
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
                    if (ucdo.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucdo.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucdo.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucdo.lvDOMap.Items.Clear();
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

        private void CheckSPORTSlaveStatusChanges()
        {
            string strRoutineName = "CheckSPORTSlaveStatusChanges";
            try
            {
                Console.WriteLine("*** CheckSPORTSlaveStatusChanges");
                //Check for slave addition...
                foreach (SPORTSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().getSPORTSlaveGroup().getSPORTSlaves())
                {
                    string slaveID = "SPORTSlave_" + slvMB.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<DOMap>> sn in slavesDOMapList)
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
                foreach (KeyValuePair<string, List<DOMap>> sain in slavesDOMapList)//Loop thru slaves...
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
                    if (ucdo.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucdo.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucdo.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucdo.lvDOMap.Items.Clear();
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
                    foreach (KeyValuePair<string, List<DOMap>> sdon in slavesDOMapList)//Loop thru slaves...
                    {
                        if (sdon.Key == slaveID)
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
                foreach (KeyValuePair<string, List<DOMap>> sdon in slavesDOMapList)//Loop thru slaves...
                {
                    string slaveID = sdon.Key;
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
                    if (ucdo.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucdo.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucdo.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucdo.lvDOMap.Items.Clear();
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
        private void CheckIEC61850SlaveStatusChanges()
        {
            //Check for slave addition...
            foreach (IEC61850ServerSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().get61850SlaveGroup().getMODBUSSlaves())//Loop thru slaves...
            {
                string slaveID = "IEC61850Server_" + slvMB.SlaveNum; //61850ServerSlaveGroup_
                bool slaveAdded = true;
                foreach (KeyValuePair<string, List<DOMap>> sn in slavesDOMapList)
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
            foreach (KeyValuePair<string, List<DOMap>> sdon in slavesDOMapList)//Loop thru slaves...
            {
                string slaveID = sdon.Key;
                bool slaveDeleted = true;
                if (Utils.getSlaveTypes(slaveID) != SlaveTypes.IEC61850Server) continue;
                foreach (IEC61850ServerSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().get61850SlaveGroup().getMODBUSSlaves())//Loop thru slaves...
                {
                    slaveID = "IEC61850Server_" + slvMB.SlaveNum; //61850ServerSlaveGroup_
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
                if (ucdo.flpMap2Slave.Controls.Count > 0)
                {
                    ((RadioButton)ucdo.flpMap2Slave.Controls[0]).Checked = true;
                    currentSlave = ((RadioButton)ucdo.flpMap2Slave.Controls[0]).Tag.ToString();
                    refreshCurrentMapList();
                }
                else
                {
                    ucdo.lvDOMap.Items.Clear();
                    currentSlave = "";
                }
            }
            fillMapOptions(Utils.getSlaveTypes(currentSlave));
        }
        private void deleteDOFromMaps(string doNo)
        {
            foreach (KeyValuePair<string, List<DOMap>> sdon in slavesDOMapList)//Loop thru slaves...
            {
                List<DOMap> delEntry = sdon.Value;
                foreach (DOMap domn in delEntry)
                {
                    if (domn.DONo == doNo)
                    {
                        delEntry.Remove(domn);
                        break;
                    }
                }
            }
        }
        private void AddMap2SlaveButton(int slaveNum, string slaveID)
        {
            RadioButton rb = new RadioButton();
            rb.Name = slaveID;
            rb.Tag = slaveID;//Ex. 'IEC104_1'/'MODBUSSlave_1'
            if (Utils.getSlaveTypes(slaveID) == SlaveTypes.IEC104)
                rb.Text = "IEC104 " + slaveNum;
            else if (Utils.getSlaveTypes(slaveID) == SlaveTypes.MODBUSSLAVE)
                rb.Text = "MODBUS " + slaveNum;
            else if (Utils.getSlaveTypes(slaveID) == SlaveTypes.IEC61850Server)
                rb.Text = "IEC61850 " + slaveNum;
            //Ajay: 05/07/2018
            else if (Utils.getSlaveTypes(slaveID) == SlaveTypes.SPORTSLAVE)
                rb.Text = "SPORT " + slaveNum;
            //Namrata:7/7/17
            //Add IEC101 Button On Mapping Side
            else if (Utils.getSlaveTypes(slaveID) == SlaveTypes.IEC101SLAVE)
                rb.Text = "IEC101 " + slaveNum;
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

            ucdo.flpMap2Slave.Controls.Add(rb);
            rb.Checked = true;
        }
        private string getDescription(ListView lstview, string ainno)
        {
            int iColIndex = ucdo.lvDOlist.Columns["Description"].Index;
            var query = lstview.Items
                    .Cast<ListViewItem>()
                    .Where(item => item.SubItems[0].Text == ainno).Select(s => s.SubItems[iColIndex].Text).Single();
            return query.ToString();
        }
        private void rbGrpMap2Slave_Click(object sender, EventArgs e)
        {
            ucdo.grpDOMap.Visible = false; //Ajay: 07/12/2018
            bool rbChanged = false;
            RadioButton currRB = ((RadioButton)sender);
            if (currentSlave != currRB.Tag.ToString())
            {
                currentSlave = currRB.Tag.ToString();
                rbChanged = true;
                refreshCurrentMapList();
                if (ucdo.lvDOlist.SelectedItems.Count > 0)
                {
                    ucdo.lvDOMap.SelectedItems.Clear();  //Remove selection from DOMap...
                    Utils.highlightListviewItem(ucdo.lvDOlist.SelectedItems[0].Text, ucdo.lvDOMap);
                }
            }
            ShowHideSlaveColumns();
            // Ajay: 07/12/2018
            if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
            {
                if (AllowSlaveOptionsOnClick(Utils.getSlaveTypes(currentSlave))) { return; }
                else { }
            }
            if (rbChanged && ucdo.lvDOlist.CheckedItems.Count <= 0) return;//We supported map listing only
            DO mapDO = null;
            if (ucdo.lvDOlist.CheckedItems.Count != 1)
            {
                MessageBox.Show("Select single DO element to map!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            for (int i = 0; i < ucdo.lvDOlist.Items.Count; i++)
            {
                if (ucdo.lvDOlist.Items[i].Checked)
                {
                    mapDO = doList.ElementAt(i);
                    ucdo.lvDOlist.Items[i].Checked = false;//Now we can uncheck in listview...
                    break;
                }
            }
            if (mapDO == null) return;
            bool alreadyMapped = false; //Search if already mapped for current slave...
            List<DOMap> slaveDOMapList;
            if (!slavesDOMapList.TryGetValue(currentSlave, out slaveDOMapList))
            {
                MessageBox.Show("Slave entry doesnot exist!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else { }
            if (!alreadyMapped)
            {
                mapMode = Mode.ADD;
                mapEditIndex = -1;
                Utils.resetValues(ucdo.grpDOMap);
                ucdo.txtDOMNo.Text = mapDO.DONo;
                string str = getDescription(ucdo.lvDOlist, mapDO.DONo);
                ucdo.txtMapDescription.Text = str;
                loadMapDefaults();
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.GRAPHICALDISPLAYSLAVE)
                {
                    ucdo.button1.Visible = true;
                    ucdo.splitContainer2.Panel2Collapsed = false;
                    //ucai.TblLayoutCSLD.Visible = true;
                }
                else
                {
                    ucdo.button1.Visible = false;
                    ucdo.splitContainer2.Panel2Collapsed = true;
                    ucdo.TblLayoutCSLD.Visible = false;
                }
                #region IEC61850Server
                //Namrata: 4/11/2017
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server)
                {
                    Utils.CurrentSlaveFinal = currentSlave;//Namrata: 04/11/2017
                    GetIndexForIEC61850Server();//Namrata:12/04/2019
                    ucdo.ChkIEC61850MapIndex.Text = "";//Namrata:25/03/2019
                    if (IEC61850MapCheckedList != null)
                    {
                        ucdo.ChkIEC61850MapIndex.CheckBoxItems.ForEach(x =>
                        {
                            x.Checked = false; x.CheckState = CheckState.Unchecked;
                        });
                    }
                    IEC61850Server_OnLoad();
                    ucdo.lblEV.Visible = false;
                    ucdo.CmbEV.Visible = false;
                    ucdo.lblEventClass.Visible = false;
                    ucdo.cmbEventC.Visible = false;
                    ucdo.lblVariation.Visible = false;
                    ucdo.CmbVari.Visible = false;
                }
                #endregion IEC61850Server

                #region MODBUSSLAVE,IEC101SLAVE,SPORTSLAVE,IEC104
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE)
                {
                    MODBUSSlave_OnLoad();
                    ucdo.lblEV.Visible = false;
                    ucdo.CmbEV.Visible = false;
                    ucdo.lblEventClass.Visible = false;
                    ucdo.cmbEventC.Visible = false;
                    ucdo.lblVariation.Visible = false;
                    ucdo.CmbVari.Visible = false;
                    //Namrata: 19/04/2018
                    ucdo.chkDOMSelect.Enabled = false;
                    ucdo.cmbDOMCommandType.Enabled = true;
                    ucdo.txtDOAutoMap.Text = "1"; //Namrata:1/7/2017
                    ucdo.ChkIEC61850MapIndex.Visible = false;
                }
                if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC101SLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC104))
                {
                    IEC101Slave_IEC104Slave_OnLoad();
                    ucdo.chkDOMSelect.Enabled = true;
                    ucdo.cmbDOMCommandType.Enabled = false;
                    ucdo.txtDOAutoMap.Text = "1"; //Namrata:1/7/2017
                    ucdo.ChkIEC61850MapIndex.Visible = false;
                    ucdo.lblEV.Visible = false;
                    ucdo.CmbEV.Visible = false;
                    ucdo.lblEventClass.Visible = false;
                    ucdo.cmbEventC.Visible = false;
                    ucdo.lblVariation.Visible = false;
                    ucdo.CmbVari.Visible = false;
                }
                if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.SPORTSLAVE))
                {
                    SportSlave_OnLoad();
                    ucdo.txtDOAutoMap.Text = "1"; //Namrata:01/07/2017
                    ucdo.lblEV.Visible = false;
                    ucdo.CmbEV.Visible = false;
                    ucdo.lblEventClass.Visible = false;
                    ucdo.cmbEventC.Visible = false;
                    ucdo.lblVariation.Visible = false;
                    ucdo.CmbVari.Visible = false;
                }
                #endregion MODBUSSLAVE,IEC101SLAVE,SPORTSLAVE,IEC104

                #region GraphicalDisplay
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
                        if (ucdo.TblLayoutCSLD.Controls.Count != 0)
                        {
                            ucdo.TblLayoutCSLD.Controls.Clear();
                        }
                        ucdo.TblLayoutCSLD.Visible = false;
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
                            string Widget1 = distinctValues.Rows[iWidget]["Widget"].ToString();
                            PictureBox pb = new PictureBox();
                            ImageConverter converter = new ImageConverter();
                            pb.Margin = new Padding(0, 0, 0, 0);
                            pb.Size = new Size(70, 70);
                            string ImagePath = Globals.Widgets_path + Widget1 + ".png";
                            pb.Image = Image.FromFile(ImagePath);
                            pb.SizeMode = PictureBoxSizeMode.StretchImage;
                            pb.Name = iCellNo.ToString();
                            ucdo.TblLayoutCSLD.Controls.Add(pb);
                            ucdo.TblLayoutCSLD.SetColumn(pb, iColumn);
                            ucdo.TblLayoutCSLD.SetColumn(pb, iRow);
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

                        ucdo.TblLayoutCSLD.Visible = true;
                        ucdo.TblLayoutCSLD.Size = new Size(350, 560);
                        #endregion DisplayImage
                        ucdo.txtUnitID.Text = "";
                        GetDataForGDisplaySlave();
                        //var rowColl = GDSlave.DtDIDOWidgets.AsEnumerable();
                        var rowColl = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable();
                        if (ucdo.CmbCellNo.SelectedItem != null)
                        {
                            string Widget = (from r in rowColl
                                             where r.Field<string>("CellNo") == ucdo.CmbCellNo.SelectedItem.ToString()
                                             select r.Field<string>("Widget")).First<string>();
                            ucdo.CmbWidget.Enabled = false;
                            ucdo.CmbWidget.SelectedIndex = ucdo.CmbWidget.FindStringExact(Widget);
                            ucdo.CmbWidget.SelectedItem = Widget;
                        }
                        DisplaySlave_OnLoad(); ucdo.splitContainer2.SplitterDistance = 600;
                        //Namrata: 26/11/2019
                        UpdateWidgetsList(slaveDOMapList);
                        refreshMapList(slaveDOMapList);
                        ucdo.splitContainer2.SplitterDistance = ucdo.splitContainer2.Width - 490 + 35;
                        ucdo.lblEV.Visible = false;
                        ucdo.CmbEV.Visible = false;
                        ucdo.lblEventClass.Visible = false;
                        ucdo.cmbEventC.Visible = false;
                        ucdo.lblVariation.Visible = false;
                        ucdo.CmbVari.Visible = false;
                    }
                }
                #endregion GraphicalDisplay

                #region MQTTSlave
                if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.MQTTSLAVE))
                {
                    MQTTSlaveEvents();
                    ucdo.lblEV.Visible = false;
                    ucdo.CmbEV.Visible = false;
                    ucdo.lblEventClass.Visible = false;
                    ucdo.cmbEventC.Visible = false;
                    ucdo.lblVariation.Visible = false;
                    ucdo.CmbVari.Visible = false;
                    ucdo.chkDOMSelect.Enabled = true;
                    ucdo.cmbDOMCommandType.Enabled = false;
                    ucdo.txtKey.Text = getMQTTSlaveKey(ucdo.lvDOlist, mapDO.DONo);
                }
                #endregion MQTTSlave

                #region SMSSlave
                if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.SMSSLAVE))
                {

                    SMSSLaveMap_OnLoadEvents();
                    ucdo.lblEV.Visible = false;
                    ucdo.CmbEV.Visible = false;
                    ucdo.lblEventClass.Visible = false;
                    ucdo.cmbEventC.Visible = false;
                    ucdo.lblVariation.Visible = false;
                    ucdo.CmbVari.Visible = false;
                    ucdo.chkDOMSelect.Enabled = true;
                    ucdo.cmbDOMCommandType.Enabled = false;
                }
                if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE))
                {
                    DNPSlave_OnLoad(); ucdo.txtDOAutoMap.Text = "1"; //Namrata:01/07/2017
                }
                #endregion SMSSlave
                ucdo.grpDOMap.Visible = true;
                ucdo.txtDOMReportingIndex.Focus();
            }
        }
        //Namrata: 26/11/2019
        private void UpdateWidgetsList(List<DOMap> list)
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
        private string getMQTTSlaveKey(ListView lstview, string donno)
        {
            int iColIndex = ucdo.lvDOlist.Columns["Description"].Index;
            var query = lstview.Items
                    .Cast<ListViewItem>()
                    .Where(item => item.SubItems[0].Text == donno).Select(s => s.SubItems[iColIndex].Text).Single();
            return query.ToString();
        }
        private void btnDOMDelete_Click(object sender, EventArgs e)
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

            List<DOMap> slaveDOMapList;
            if (!slavesDOMapList.TryGetValue(currentSlave, out slaveDOMapList))
            {
                MessageBox.Show("Error deleting DO map!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            for (int i = ucdo.lvDOMap.Items.Count - 1; i >= 0; i--)
            {
                if (ucdo.lvDOMap.Items[i].Checked)
                {
                    if (GDSlave.DsExcelData.Tables.Count > 0)
                    {
                        bool IsDuplicates = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable()
                        .Count(x => x.Field<string>("CellNo") == slaveDOMapList[i].CellNo && x.Field<string>("Widget") == slaveDOMapList[i].Widget && x.Field<string>("Type") == "DO") > 1;

                        if (IsDuplicates)
                        {
                            GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == slaveDOMapList[i].CellNo && x["Widget"].ToString() == slaveDOMapList[i].Widget && x["DBIndex"].ToString() == slaveDOMapList[i].DONo).ToList().ForEach(Rw => Rw.Delete());
                            GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                        }
                        else
                        {
                            var results = from row in GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable()
                                          where row.Field<string>("CellNo") == slaveDOMapList[i].CellNo && row.Field<string>("Widget") == slaveDOMapList[i].Widget && row.Field<string>("DBIndex") == slaveDOMapList[i].DONo
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
                    }
                    //GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == slaveDOMapList[i].CellNo && x["Widget"].ToString() == slaveDOMapList[i].Widget && x["DBindex"].ToString() == slaveDOMapList[i].DONo).ToList().ForEach(Rw => Rw.Delete());
                    //GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                    slaveDOMapList.RemoveAt(i);
                    ucdo.lvDOMap.Items[i].Remove();
                }
            }

            refreshMapList(slaveDOMapList);
        }
        private void btnDOMDone_Click(object sender, EventArgs e)
        {
            //if (!ValidateMap()) return;
            if (!slavesDOMapList.TryGetValue(currentSlave, out slaveDOMapList))
            {
                MessageBox.Show("Error adding DO map for slave!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<KeyValuePair<string, string>> domData = Utils.getKeyValueAttributes(ucdo.grpDOMap);

            #region Declaration
            string Description = "";
            int DONo = 0, ReportingIndex = 0;
            #endregion Declaration

            #region KeyValuePair
            int intDONO = Convert.ToInt32(domData[15].Value);
            if (domData[11].Value != "")
            {
                intRange = Convert.ToInt32(domData[11].Value);
            }
            int intReportingIndex = Convert.ToInt32(domData[17].Value);
            #endregion KeyValuePair

            #region ListviewItem
            //Namrata: 08/07/2017
            ListViewItem item = ucdo.lvDOlist.Items.Cast<ListViewItem>().FirstOrDefault(x => x.Text == intDONO.ToString());//Find Index Of ListView
            string ind = ucdo.lvDOlist.Items.IndexOf(item).ToString();
            ListViewItem ExistDOMap = ucdo.lvDOMap.FindItemWithText(ucdo.txtDONo.Text);
            #endregion ListviewItem

            #region Add
            if (mapMode == Mode.ADD)
            {
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server)
                {
                    IEC61850MapCheckedList = ucdo.ChkIEC61850MapIndex.CheckBoxItems.Where(x => x.Checked == true).Select(x => x.Text).ToList();//Namrata:25/03/2019
                    if (IEC61850MapCheckedList != null)
                    {
                        intMapCheckItems = IEC61850MapCheckedList.Count();
                    }
                    #region IEC61850Server
                    int iListCount = 0;
                    if (doList.Count >= 0)
                    {
                        if ((intMapCheckItems + Convert.ToInt16(ind)) > ucdo.lvDOlist.Items.Count)
                        {
                            MessageBox.Show("Slave Entry Does Not Exist", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            for (int i = intDONO; i <= intDONO + intMapCheckItems - 1; i++)
                            {
                                if (doList.Select(x => x.DONo).ToList().Contains(i.ToString()))
                                {
                                    DONo = i;
                                    ReportingIndex = intReportingIndex++;
                                    Description = getDescription(ucdo.lvDOlist, DONo.ToString());//Namrata:27/06/2019
                                    DOMap NewDOMap = new DOMap("DO", domData, Utils.getSlaveTypes(currentSlave));
                                    NewDOMap.DONo = DONo.ToString();
                                    NewDOMap.Description = Description;
                                    NewDOMap.IEC61850ReportingIndex = IEC61850MapCheckedList[iListCount].ToString();
                                    slaveDOMapList.Add(NewDOMap);
                                    iListCount++;
                                }
                            }
                        }
                    }
                    #endregion IEC61850Server
                }
                else
                {
                    if (doList.Count >= 0)
                    {
                        if ((intRange + Convert.ToInt16(ind)) > ucdo.lvDOlist.Items.Count)
                        {
                            MessageBox.Show("Slave Entry Does Not Exist", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            GDSlave.IsDOExist = slaveDOMapList.Select(y => y.DONo).ToList().Contains(domData[11].Value.ToString());
                            for (int i = intDONO; i <= intDONO + intRange - 1; i++)//Ajay: 21/11/2017
                            {
                                if (doList.Select(x => x.DONo).ToList().Contains(i.ToString()))//Ajay: 21/11/2017
                                {
                                    DONo = i;
                                    ReportingIndex = intReportingIndex++;
                                    Description = domData[10].Value.ToString();//Namrata:27/06/2019
                                    DOMap NewDOMap = new DOMap("DO", domData, Utils.getSlaveTypes(currentSlave));
                                    NewDOMap.DONo = DONo.ToString();
                                    NewDOMap.ReportingIndex = ReportingIndex.ToString();
                                    NewDOMap.Description = Description.ToString();
                                    slaveDOMapList.Add(NewDOMap);
                                }
                                else
                                {
                                    intRange++;
                                }
                            }
                        }
                    }
                }
            }
            #endregion Add

            #region Edit
            else if (mapMode == Mode.EDIT)
            {
                if (ucdo.ChkIEC61850MapIndex.CheckBoxItems.Count > 0)
                {
                    if (ucdo.ChkIEC61850MapIndex.CheckBoxItems.Where(x => x.Checked == true).Count() > 1)
                    {
                        MessageBox.Show("Please select only 1 ReportingIndex.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        slaveDOMapList[mapEditIndex].updateAttributes(domData);
                    }
                }
                else
                {
                    slaveDOMapList[mapEditIndex].updateAttributes(domData);
                }
            }
            #endregion Edit
            refreshMapList(slaveDOMapList);
            ucdo.grpDOMap.Visible = false;
            mapMode = Mode.NONE;
            mapEditIndex = -1;
        }
        private void btnDOMCancel_Click(object sender, EventArgs e)
        {
            ucdo.grpDOMap.Visible = false;
            mapMode = Mode.NONE;
            mapEditIndex = -1;
            Utils.resetValues(ucdo.grpDOMap);
        }
        private void lvDOMap_DoubleClick(object sender, EventArgs e)
        {
            // Ajay: 07/12/2018
            if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
            {
                if (AllowSlaveOptionsOnClick(Utils.getSlaveTypes(currentSlave))) { return; }
                else { }
            }
            List<DOMap> slaveDOMapList;
            if (!slavesDOMapList.TryGetValue(currentSlave, out slaveDOMapList))
            {
                MessageBox.Show("Error editing DO map for slave!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int ListIndex = ucdo.lvDOMap.FocusedItem.Index;//Namrata: 07/03/2018
            ListViewItem lvi = ucdo.lvDOMap.Items[ListIndex];
            Utils.UncheckOthers(ucdo.lvDOMap, lvi.Index);
            if (slaveDOMapList.ElementAt(lvi.Index).IsNodeComment)
            {
                MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #region Enable/Disable Events
            if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server)
            {
                GetIndexForIEC61850Server();
                IEC61850Server_OnDoubleClick();
                ucdo.lblEV.Visible = false;
                ucdo.CmbEV.Visible = false;
                ucdo.lblEventClass.Visible = false;
                ucdo.cmbEventC.Visible = false;
                ucdo.lblVariation.Visible = false;
                ucdo.CmbVari.Visible = false;
            }
            if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE)
            {
                MODBUSSlave_OnDoubleClick();
                ucdo.lblEV.Visible = false;
                ucdo.CmbEV.Visible = false;
                ucdo.lblEventClass.Visible = false;
                ucdo.cmbEventC.Visible = false;
                ucdo.lblVariation.Visible = false;
                ucdo.CmbVari.Visible = false;
            }
            if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC101SLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC104))
            {
                IEC101IEC104Slave_OnDoubleClick();
                ucdo.lblEV.Visible = false;
                ucdo.CmbEV.Visible = false;
                ucdo.lblEventClass.Visible = false;
                ucdo.cmbEventC.Visible = false;
                ucdo.lblVariation.Visible = false;
                ucdo.CmbVari.Visible = false;
            }
            if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.SPORTSLAVE))
            {
                SportSlave_OnDoubleClick();
                ucdo.lblEV.Visible = false;
                ucdo.CmbEV.Visible = false;
                ucdo.lblEventClass.Visible = false;
                ucdo.cmbEventC.Visible = false;
                ucdo.lblVariation.Visible = false;
                ucdo.CmbVari.Visible = false;
            }
            if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.GRAPHICALDISPLAYSLAVE))
            {
                GetDataForGDisplaySlave();
                DisplaySlave_OnLoad();
                ucdo.lblEV.Visible = false;
                ucdo.CmbEV.Visible = false;
                ucdo.lblEventClass.Visible = false;
                ucdo.cmbEventC.Visible = false;
                ucdo.lblVariation.Visible = false;
                ucdo.CmbVari.Visible = false;
            }
            if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.SMSSLAVE)
            {
                SMSSLaveMap_OnDoubleClickEvents();
                ucdo.lblEV.Visible = false;
                ucdo.CmbEV.Visible = false;
                ucdo.lblEventClass.Visible = false;
                ucdo.cmbEventC.Visible = false;
                ucdo.lblVariation.Visible = false;
                ucdo.CmbVari.Visible = false;
            }
            if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.MQTTSLAVE))
            {
                //ucdo.lblUnit.Visible = true;
                //ucdo.txtUnit.Visible = true;
                ucdo.lblKey.Visible = true;
                ucdo.txtKey.Visible = true;
                MQTTSlave_OnDoubleClick();
                ucdo.lblEV.Visible = false;
                ucdo.CmbEV.Visible = false;
                ucdo.lblEventClass.Visible = false;
                ucdo.cmbEventC.Visible = false;
                ucdo.lblVariation.Visible = false;
                ucdo.CmbVari.Visible = false;
                ucdo.chkDOMSelect.Enabled = true;
                ucdo.cmbDOMCommandType.Enabled = false;
            }
            if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE)
            {
                DNPSlave_OnDoubleClick();
            }
            #endregion Enable/Disable Events
            ucdo.grpDOMap.Visible = true;
            mapMode = Mode.EDIT;
            mapEditIndex = lvi.Index;
            //Namrata:16/04/2019
            ucdo.ChkIEC61850MapIndex.Text = "";
            if ((IEC61850MapCheckedList != null) || (IEC61850MapCheckedList == null))
            {
                ucdo.ChkIEC61850MapIndex.CheckBoxItems.ForEach(x =>
                {
                    x.Checked = false; x.CheckState = CheckState.Unchecked;
                });
            }
            if (ucdo.ChkIEC61850MapIndex.CheckBoxItems.Count > 0)
            {
                ucdo.ChkIEC61850MapIndex.Text = slaveDOMapList[lvi.Index].IEC61850ReportingIndex.ToString();
            }
            loadMapValues();
            //Namrata:16/04/2019
            if (ucdo.ChkIEC61850MapIndex.CheckBoxItems.Count > 0)
            {
                ucdo.ChkIEC61850MapIndex.CheckBoxItems.Where(x => x.Text == slaveDOMapList[lvi.Index].IEC61850ReportingIndex.ToString()).Take(1).ToList().ForEach(x =>
                {
                    x.Checked = true; x.CheckState = CheckState.Checked;
                });
            }
            ucdo.txtDOMReportingIndex.Focus();
        }
        private void loadMapDefaults()
        {
            ucdo.txtDOMReportingIndex.Text = (Globals.DOReportingIndex + 1).ToString();
            ucdo.txtDOMBitPos.Text = "0";
            ucdo.txtUnitID.Text = "1";
        }
        private void loadMapValues()
        {
            List<DOMap> slaveDOMapList;
            if (!slavesDOMapList.TryGetValue(currentSlave, out slaveDOMapList))
            {
                MessageBox.Show("Error loading DO map data for slave!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DOMap domn = slaveDOMapList.ElementAt(mapEditIndex);
            if (domn != null)
            {
                ucdo.txtDOMNo.Text = domn.DONo;
                ucdo.txtDOMReportingIndex.Text = domn.ReportingIndex;
                ucdo.txtKey.Text = domn.Key;
                ucdo.cmbDOMDataType.SelectedIndex = ucdo.cmbDOMDataType.FindStringExact(domn.DataType);
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE)
                {
                    ucdo.cmbDOMCommandType.SelectedIndex = ucdo.cmbDOMCommandType.FindStringExact(domn.CommandType);
                    ucdo.cmbDOMCommandType.Enabled = true;
                    //Namrata:21/7/2017
                    ucdo.chkDOMSelect.Enabled = false;
                }
                else
                {
                    ucdo.cmbDOMCommandType.Enabled = false;
                    ucdo.chkDOMSelect.Enabled = true;   //Namrata:21/7/2017
                }
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE)
                {
                    ucdo.cmbDOMCommandType.Enabled = true; ucdo.chkDOMSelect.Enabled = true;
                    ucdo.cmbEventC.SelectedIndex = ucdo.cmbEventC.FindStringExact(domn.EventClass);//Namrata:27/03/2019
                    ucdo.CmbVari.SelectedIndex = ucdo.CmbVari.FindStringExact(domn.Variation);//Namrata:27/03/2019
                    ucdo.CmbEV.SelectedIndex = ucdo.CmbEV.FindStringExact(domn.EventVariation);//Namrata:27/03/2019
                }
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.SPORTSLAVE)
                {
                    ucdo.txtUnitID.Text = domn.UnitID;
                    ucdo.txtUnitID.Visible = true;
                    ucdo.chkUsed.Visible = true;
                }
                else
                {
                    ucdo.txtUnitID.Enabled = false;
                    ucdo.txtUnitID.Visible = false;
                    ucdo.chkUsed.Enabled = false;
                    ucdo.chkUsed.Visible = false;

                }
                //Ajay: 12/07/2018 check-unchecked Used checkbox according to mapped Used value
                if (domn.Used.ToLower() == "yes")
                { ucdo.chkUsed.Checked = true; }
                else { ucdo.chkUsed.Checked = false; }
                ucdo.ChkIEC61850Index.SelectedIndex = ucdo.ChkIEC61850Index.FindStringExact(domn.IEC61850ReportingIndex);//Namrata:16/04/2019
                ucdo.txtDOMBitPos.Text = domn.BitPos;
                //Namrata: 18/11/2017
                ucdo.txtMapDescription.Text = domn.Description;
                if (domn.Select.ToLower() == "enable") ucdo.chkDOMSelect.Checked = true;
                else ucdo.chkDOMSelect.Checked = false;
                //Namrata: 20/08/2019
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.GRAPHICALDISPLAYSLAVE)
                {
                    ucdo.CmbCellNo.SelectedIndex = ucdo.CmbCellNo.FindStringExact(domn.CellNo);
                    ucdo.CmbWidget.SelectedIndex = ucdo.CmbWidget.FindStringExact(domn.Widget);
                    ucdo.txtUnitID.Enabled = false;
                    ucdo.txtUnitID.Visible = false;
                    if (GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domn.CellNo && (x["Type"].ToString() == "DO" && ((x["DBIndex"].ToString() == domn.DONo) || string.IsNullOrEmpty(x["DBIndex"].ToString())))).Any())
                    {
                        DataRow datarow;
                        datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domn.CellNo && x["Type"].ToString() == "DO" && ((x["DBIndex"].ToString() == domn.DONo) || string.IsNullOrEmpty(x["DBIndex"].ToString()))).ToList().First();
                        //datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == aimp.CellNo && x["Type"].ToString() == "AI").ToList().First();
                        datarow["CellNo"] = domn.CellNo;
                        datarow["Widget"] = domn.Widget;
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
                    ucdo.CmbCellNo.Visible = false;
                    ucdo.CmbWidget.Visible = false;
                }
            }
        }
        private bool ValidateMap()
        {
            bool status = true;

            //Check empty field's
            if (Utils.IsEmptyFields(ucdo.grpDOMap))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return status;
        }
        private void refreshMapList(List<DOMap> tmpList)
        {
            string strRoutineName = "DOConfiguration:refreshMapList";
            try
            {
                int cnt = 0;
                ucdo.lvDOMap.Items.Clear();
                ucdo.lblDOMRecords.Text = "0";
                if (tmpList == null) return;


                #region SlaveTypes.MODBUSSLAVE
                if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE))
                {
                    foreach (DOMap domp in tmpList)
                    {
                        string[] row = new string[15];
                        if (domp.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = domp.DONo;
                            row[2] = domp.ReportingIndex;
                            row[3] = domp.DataType;
                            row[4] = domp.CommandType;
                            row[5] = domp.BitPos;
                            row[6] = domp.Select;
                            row[8] = "";
                            row[9] = "";
                            row[10] = "";
                            row[11] = "";
                            row[12] = "";
                            row[13] = "";
                            row[14] = domp.Description;
                        }

                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucdo.lvDOMap.Items.Add(lvItem);
                    }
                }
                #endregion SlaveTypes.MODBUSSLAVE

                #region SlaveTypes.SPORTSLAVE
                else if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.SPORTSLAVE))
                {
                    foreach (DOMap domp in tmpList)
                    {
                        string[] row = new string[15];
                        if (domp.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = domp.DONo;
                            row[1] = domp.UnitID;
                            row[2] = domp.ReportingIndex;
                            row[3] = domp.DataType;
                            row[5] = domp.BitPos;
                            row[6] = domp.Select;
                            row[7] = domp.Used;
                            row[8] = "";
                            row[9] = "";
                            row[10] = "";
                            row[11] = "";
                            row[12] = "";
                            row[13] = "";
                            row[14] = domp.Description;
                        }

                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucdo.lvDOMap.Items.Add(lvItem);
                    }
                }
                #endregion SlaveTypes.SPORTSLAVE

                #region SlaveTypes.IEC101SLAVE,IEC104
                else if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC101SLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC104))
                {
                    foreach (DOMap domp in tmpList)
                    {
                        string[] row = new string[15];
                        if (domp.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = domp.DONo;
                            row[2] = domp.ReportingIndex;
                            row[3] = domp.DataType;
                            row[4] = domp.CommandType;
                            row[5] = domp.BitPos;
                            row[6] = domp.Select;
                            row[8] = "";
                            row[9] = "";
                            row[10] = "";
                            row[11] = "";
                            row[12] = "";
                            row[13] = "";
                            row[14] = domp.Description;
                        }

                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucdo.lvDOMap.Items.Add(lvItem);
                    }
                }
                #endregion SlaveTypes.IEC101SLAVE,IEC104

                #region SlaveTypes.IEC61850Server
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server)
                {
                    foreach (DOMap domp in tmpList)
                    {
                        string[] row = new string[15];
                        if (domp.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = domp.DONo;
                            row[1] = "";
                            row[2] = domp.IEC61850ReportingIndex;
                            row[3] = domp.DataType;
                            row[4] = domp.CommandType;
                            row[5] = domp.BitPos;
                            row[6] = domp.Select;
                            row[7] = "";
                            row[8] = "";
                            row[9] = "";
                            row[10] = "";
                            row[11] = "";
                            row[12] = "";
                            row[13] = "";
                            row[14] = domp.Description;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucdo.lvDOMap.Items.Add(lvItem);
                    }
                }
                #endregion SlaveTypes.IEC61850Server

                #region Graphical Display
                if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.GRAPHICALDISPLAYSLAVE))
                {

                    foreach (DOMap domp in tmpList)
                    {
                        string[] row = new string[15];
                        if (domp.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = domp.DONo;
                            row[1] = "";
                            row[2] = "";
                            row[3] = "";
                            row[4] = "";
                            row[5] = "";
                            row[6] = "";
                            row[7] = "";
                            row[8] = domp.CellNo;
                            row[9] = domp.Widget;
                            row[10] = "";
                            row[11] = "";
                            row[12] = "";
                            row[13] = "";
                            row[14] = domp.Description;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucdo.lvDOMap.Items.Add(lvItem);



                        #region Update Dataset Tables as per Slave

                        string strConfiguration = domp.CellNo + "," + domp.Widget + "," + "DO" + "," + domp.Description + "," + domp.DONo + "," + domp.UnitID;

                        if (GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && (x["Type"].ToString() == "DO" && ((x["DBIndex"].ToString() == domp.DONo) || string.IsNullOrEmpty(x["DBIndex"].ToString())))).Any())
                        {
                            DataRow datarow;
                            datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && x["Type"].ToString() == "DO" && ((x["DBIndex"].ToString() == domp.DONo) || string.IsNullOrEmpty(x["DBIndex"].ToString()))).ToList().First();
                            //datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && x["Type"].ToString() == "DO" ).ToList().First();
                            datarow["CellNo"] = domp.CellNo;
                            datarow["Widget"] = domp.Widget;
                            datarow["Type"] = "DO";
                            datarow["Name"] = domp.Description;
                            datarow["DBIndex"] = domp.DONo;
                            //Namrata: 04/12/2019
                            datarow["Unit"] = "";
                            //datarow["Unit"] = domp.UnitID;
                            datarow["Configuration"] = strConfiguration;
                            GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                        }
                        else if (GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && string.IsNullOrEmpty(x["Type"].ToString())).ToList().Any())
                        {
                            DataRow datarow;
                            datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && string.IsNullOrEmpty(x["DBIndex"].ToString()) && ((x["DBIndex"].ToString() == domp.DONo) || string.IsNullOrEmpty(x["DBIndex"].ToString()))).ToList().First();
                            //datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && x["Type"].ToString() == "DO" && ((x["DBIndex"].ToString() == domp.DONo) || string.IsNullOrEmpty(x["DBIndex"].ToString()))).ToList().First();
                            datarow["CellNo"] = domp.CellNo;
                            datarow["Widget"] = domp.Widget;
                            datarow["Type"] = "DO";
                            datarow["Name"] = domp.Description;
                            datarow["DBIndex"] = domp.DONo;
                            //Namrata: 04/12/2019
                            datarow["Unit"] = "";
                            //datarow["Unit"] = domp.UnitID;
                            datarow["Configuration"] = strConfiguration;
                            GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                        }
                        else
                        {
                            DataRow datarow;
                            datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].NewRow();
                            datarow["CellNo"] = domp.CellNo;
                            datarow["Widget"] = domp.Widget;
                            datarow["Type"] = "DO";
                            datarow["Name"] = domp.Description;
                            datarow["DBIndex"] = domp.DONo;
                            //Namrata: 04/12/2019
                            datarow["Unit"] = "";
                            //datarow["Unit"] = domp.UnitID;
                            datarow["Configuration"] = strConfiguration;
                            GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].Rows.Add(datarow);
                            GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                        }
                        DataColumn myDC = new DataColumn(GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].Columns[0].ColumnName, System.Type.GetType("System.Int32"));
                        GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                        DataView dv = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].DefaultView;
                        dv.Sort = "CellNo ASC";
                        GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                        DataTable ordered = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable()
                                            .OrderBy(r => int.Parse(r.Field<String>("CellNo")))
                                            .CopyToDataTable();
                        ordered.TableName = GDSlave.CurrentSlave;
                        GDSlave.DsExcelData.Tables.Remove(GDSlave.CurrentSlave);
                        GDSlave.DsExcelData.Tables.Add(ordered);
                    }
                    #endregion Update Dataset Tables as per Slave

                    //Namrata: 16/09/2019
                    //    #region Update Dataset Tables as per Slave

                    //    string strConfiguration = domp.CellNo + "," + domp.Widget + "," + "DO" + "," + domp.Description + "," + domp.DONo + "," + domp.UnitID;

                    //    if (GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && (x["Type"].ToString() == "DO" && ((x["DBIndex"].ToString() == domp.DONo) || string.IsNullOrEmpty(x["DBIndex"].ToString())))).Any())
                    //    {
                    //        DataRow datarow;
                    //        datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && x["Type"].ToString() == "DO" && ((x["DBIndex"].ToString() == domp.DONo) || string.IsNullOrEmpty(x["DBIndex"].ToString()))).ToList().First();
                    //        //datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && x["Type"].ToString() == "DO" ).ToList().First();
                    //        datarow["CellNo"] = domp.CellNo;
                    //        datarow["Widget"] = domp.Widget;
                    //        datarow["Type"] = "DO";
                    //        datarow["Name"] = domp.Description;
                    //        datarow["DBIndex"] = domp.DONo;
                    //        datarow["Unit"] = domp.UnitID;
                    //        datarow["Configuration"] = strConfiguration;
                    //        GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                    //    }
                    //    else if (GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && string.IsNullOrEmpty(x["Type"].ToString())).ToList().Any())
                    //    {
                    //        DataRow datarow;
                    //            datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && string.IsNullOrEmpty(x["Type"].ToString())).ToList().First();
                    //        //datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && x["Type"].ToString() == "DO" && ((x["DBIndex"].ToString() == domp.DONo) || string.IsNullOrEmpty(x["DBIndex"].ToString()))).ToList().First();
                    //        datarow["CellNo"] = domp.CellNo;
                    //        datarow["Widget"] = domp.Widget;
                    //        datarow["Type"] = "DO";
                    //        datarow["Name"] = domp.Description;
                    //        datarow["DBIndex"] = domp.DONo;
                    //        datarow["Unit"] = domp.UnitID;
                    //        datarow["Configuration"] = strConfiguration;
                    //        GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                    //    }
                    //    else
                    //    {
                    //            DataRow datarow;
                    //            datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].NewRow();
                    //            datarow["CellNo"] = domp.CellNo;
                    //            datarow["Widget"] = domp.Widget;
                    //            datarow["Type"] = "DO";
                    //            datarow["Name"] = domp.Description;
                    //            datarow["DBIndex"] = domp.DONo;
                    //            datarow["Unit"] = domp.UnitID;
                    //            datarow["Configuration"] = strConfiguration;
                    //            GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].Rows.Add(datarow);
                    //            GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                    //    }
                    //    DataColumn myDC = new DataColumn(GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].Columns[0].ColumnName, System.Type.GetType("System.Int32"));
                    //    GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                    //    DataView dv = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].DefaultView;
                    //    dv.Sort = "CellNo ASC";
                    //    GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                    //    DataTable ordered = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable()
                    //                        .OrderBy(r => int.Parse(r.Field<String>("CellNo")))
                    //                        .CopyToDataTable();
                    //    ordered.TableName = GDSlave.CurrentSlave;
                    //    GDSlave.DsExcelData.Tables.Remove(GDSlave.CurrentSlave);
                    //    GDSlave.DsExcelData.Tables.Add(ordered);
                    //}
                    //    #endregion Update Dataset Tables as per Slave
                }

                #endregion Graphical Display

                #region SlaveTypes.MQTTSLAVE
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MQTTSLAVE)
                {
                    foreach (DOMap domp in tmpList)
                    {
                        string[] row = new string[15];
                        if (domp.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = domp.DONo;
                            row[2] = domp.ReportingIndex;
                            row[3] = domp.DataType;
                            row[4] = "";
                            row[5] = domp.BitPos;
                            row[6] = "";
                            row[8] = "";
                            row[9] = "";
                            row[10] = domp.Key;
                            row[11] = "";
                            row[12] = "";
                            row[13] = "";
                            row[14] = domp.Description;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucdo.lvDOMap.Items.Add(lvItem);
                    }
                }
                #endregion SlaveTypes.IEC61850Server

                #region SlaveTypes.SMSSLAVE
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.SMSSLAVE)
                {
                    foreach (DOMap domp in tmpList)
                    {
                        string[] row = new string[15];
                        if (domp.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = domp.DONo;
                            row[2] = domp.ReportingIndex;
                            row[3] = domp.DataType;
                            row[4] = "";
                            row[5] = domp.BitPos;
                            row[6] = domp.Select;
                            row[8] = "";
                            row[9] = "";
                            row[10] = "";
                            row[11] = "";
                            row[12] = "";
                            row[13] = "";
                            row[14] = domp.Description;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucdo.lvDOMap.Items.Add(lvItem);
                    }
                }
                #endregion SlaveTypes.IEC61850Server
                #region SlaveTypes.IEC61850Server
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server)
                {
                    foreach (DOMap domp in tmpList)
                    {
                        string[] row = new string[15];
                        if (domp.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = domp.DONo;
                            row[1] = "";
                            row[2] = domp.IEC61850ReportingIndex;
                            row[3] = domp.DataType;
                            row[4] = domp.CommandType;
                            row[5] = domp.BitPos;
                            row[6] = domp.Select;
                            row[7] = "";
                            row[8] = "";
                            row[9] = "";
                            row[10] = "";
                            row[11] = "";
                            row[12] = "";
                            row[13] = "";
                            row[14] = domp.Description;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucdo.lvDOMap.Items.Add(lvItem);
                    }
                }
                #endregion SlaveTypes.IEC61850Server


                #region SlaveTypes.DNP
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE)
                {
                    foreach (DOMap domp in tmpList)
                    {
                        string[] row = new string[15];
                        if (domp.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = domp.DONo;
                            row[2] = domp.ReportingIndex;
                            row[3] = "";
                            row[4] = domp.CommandType;
                            row[5] = domp.BitPos;
                            row[6] = domp.Select;
                            row[8] = "";
                            row[9] = "";
                            row[10] = "";
                            row[11] = domp.EventClass;
                            row[12] = domp.Variation;
                            row[13] = domp.EventVariation;
                            row[14] = domp.Description;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucdo.lvDOMap.Items.Add(lvItem);
                    }
                }
                #endregion SlaveTypes.DNP
                ucdo.lblDOMRecords.Text = tmpList.Count.ToString();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void refreshMapList1(List<DOMap> tmpList)
        {
            string strRoutineName = "DOConfiguration:refreshMapList";
            try
            {
                int cnt = 0;
                ucdo.lvDOMap.Items.Clear();
                ucdo.lblDOMRecords.Text = "0";
                if (tmpList == null) return;

                if (ucdo.lvDOMap.InvokeRequired)
                {
                    ucdo.lvDOMap.Invoke(new MethodInvoker(delegate
                    {
                        #region SlaveTypes.MODBUSSLAVE
                        if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE))
                        {
                            foreach (DOMap domp in tmpList)
                            {
                                string[] row = new string[15];
                                if (domp.IsNodeComment)
                                {
                                    row[0] = "Comment...";
                                }
                                else
                                {
                                    row[0] = domp.DONo;
                                    row[2] = domp.ReportingIndex;
                                    row[3] = domp.DataType;
                                    row[4] = domp.CommandType;
                                    row[5] = domp.BitPos;
                                    row[6] = domp.Select;
                                    row[8] = "";
                                    row[9] = "";
                                    row[10] = "";
                                    row[11] = "";
                                    row[12] = "";
                                    row[13] = "";
                                    row[14] = domp.Description;
                                }

                                ListViewItem lvItem = new ListViewItem(row);
                                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                                ucdo.lvDOMap.Items.Add(lvItem);
                            }
                        }
                        #endregion SlaveTypes.MODBUSSLAVE

                        #region SlaveTypes.SPORTSLAVE
                        else if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.SPORTSLAVE))
                        {
                            foreach (DOMap domp in tmpList)
                            {
                                string[] row = new string[15];
                                if (domp.IsNodeComment)
                                {
                                    row[0] = "Comment...";
                                }
                                else
                                {
                                    row[0] = domp.DONo;
                                    row[1] = domp.UnitID;
                                    row[2] = domp.ReportingIndex;
                                    row[3] = domp.DataType;
                                    row[5] = domp.BitPos;
                                    row[6] = domp.Select;
                                    row[7] = domp.Used;
                                    row[8] = "";
                                    row[9] = "";
                                    row[10] = "";
                                    row[11] = "";
                                    row[12] = "";
                                    row[13] = "";
                                    row[14] = domp.Description;
                                }

                                ListViewItem lvItem = new ListViewItem(row);
                                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                                ucdo.lvDOMap.Items.Add(lvItem);
                            }
                        }
                        #endregion SlaveTypes.SPORTSLAVE

                        #region SlaveTypes.IEC101SLAVE,IEC104
                        else if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC101SLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC104))
                        {
                            foreach (DOMap domp in tmpList)
                            {
                                string[] row = new string[15];
                                if (domp.IsNodeComment)
                                {
                                    row[0] = "Comment...";
                                }
                                else
                                {
                                    row[0] = domp.DONo;
                                    row[2] = domp.ReportingIndex;
                                    row[3] = domp.DataType;
                                    row[4] = domp.CommandType;
                                    row[5] = domp.BitPos;
                                    row[6] = domp.Select;
                                    row[8] = "";
                                    row[9] = "";
                                    row[10] = "";
                                    row[11] = "";
                                    row[12] = "";
                                    row[13] = "";
                                    row[14] = domp.Description;
                                }

                                ListViewItem lvItem = new ListViewItem(row);
                                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                                ucdo.lvDOMap.Items.Add(lvItem);
                            }
                        }
                        #endregion SlaveTypes.IEC101SLAVE,IEC104

                        #region SlaveTypes.IEC61850Server
                        if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server)
                        {
                            foreach (DOMap domp in tmpList)
                            {
                                string[] row = new string[15];
                                if (domp.IsNodeComment)
                                {
                                    row[0] = "Comment...";
                                }
                                else
                                {
                                    row[0] = domp.DONo;
                                    row[1] = "";
                                    row[2] = domp.IEC61850ReportingIndex;
                                    row[3] = domp.DataType;
                                    row[4] = domp.CommandType;
                                    row[5] = domp.BitPos;
                                    row[6] = domp.Select;
                                    row[7] = "";
                                    row[8] = "";
                                    row[9] = "";
                                    row[10] = "";
                                    row[11] = "";
                                    row[12] = "";
                                    row[13] = "";
                                    row[14] = domp.Description;
                                }
                                ListViewItem lvItem = new ListViewItem(row);
                                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                                ucdo.lvDOMap.Items.Add(lvItem);
                            }
                        }
                        #endregion SlaveTypes.IEC61850Server

                        #region Graphical Display
                        if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.GRAPHICALDISPLAYSLAVE))
                        {

                            foreach (DOMap domp in tmpList)
                            {
                                string[] row = new string[15];
                                if (domp.IsNodeComment)
                                {
                                    row[0] = "Comment...";
                                }
                                else
                                {
                                    row[0] = domp.DONo;
                                    row[1] = "";
                                    row[2] = "";
                                    row[3] = "";
                                    row[4] = "";
                                    row[5] = "";
                                    row[6] = "";
                                    row[7] = "";
                                    row[8] = domp.CellNo;
                                    row[9] = domp.Widget;
                                    row[10] = "";
                                    row[11] = "";
                                    row[12] = "";
                                    row[13] = "";
                                    row[14] = domp.Description;
                                }
                                ListViewItem lvItem = new ListViewItem(row);
                                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                                ucdo.lvDOMap.Items.Add(lvItem);



                                #region Update Dataset Tables as per Slave

                                string strConfiguration = domp.CellNo + "," + domp.Widget + "," + "DO" + "," + domp.Description + "," + domp.DONo + "," + domp.UnitID;

                                if (GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && (x["Type"].ToString() == "DO" && ((x["DBIndex"].ToString() == domp.DONo) || string.IsNullOrEmpty(x["DBIndex"].ToString())))).Any())
                                {
                                    DataRow datarow;
                                    datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && x["Type"].ToString() == "DO" && ((x["DBIndex"].ToString() == domp.DONo) || string.IsNullOrEmpty(x["DBIndex"].ToString()))).ToList().First();
                                    //datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && x["Type"].ToString() == "DO" ).ToList().First();
                                    datarow["CellNo"] = domp.CellNo;
                                    datarow["Widget"] = domp.Widget;
                                    datarow["Type"] = "DO";
                                    datarow["Name"] = domp.Description;
                                    datarow["DBIndex"] = domp.DONo;
                                    //Namrata: 04/12/2019
                                    datarow["Unit"] = "";
                                    //datarow["Unit"] = domp.UnitID;
                                    datarow["Configuration"] = strConfiguration;
                                    GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                                }
                                else if (GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && string.IsNullOrEmpty(x["Type"].ToString())).ToList().Any())
                                {
                                    DataRow datarow;
                                    datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && string.IsNullOrEmpty(x["DBIndex"].ToString()) && ((x["DBIndex"].ToString() == domp.DONo) || string.IsNullOrEmpty(x["DBIndex"].ToString()))).ToList().First();
                                    //datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && x["Type"].ToString() == "DO" && ((x["DBIndex"].ToString() == domp.DONo) || string.IsNullOrEmpty(x["DBIndex"].ToString()))).ToList().First();
                                    datarow["CellNo"] = domp.CellNo;
                                    datarow["Widget"] = domp.Widget;
                                    datarow["Type"] = "DO";
                                    datarow["Name"] = domp.Description;
                                    datarow["DBIndex"] = domp.DONo;
                                    //Namrata: 04/12/2019
                                    datarow["Unit"] = "";
                                    //datarow["Unit"] = domp.UnitID;
                                    datarow["Configuration"] = strConfiguration;
                                    GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                                }
                                else
                                {
                                    DataRow datarow;
                                    datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].NewRow();
                                    datarow["CellNo"] = domp.CellNo;
                                    datarow["Widget"] = domp.Widget;
                                    datarow["Type"] = "DO";
                                    datarow["Name"] = domp.Description;
                                    datarow["DBIndex"] = domp.DONo;
                                    //Namrata: 04/12/2019
                                    datarow["Unit"] = "";
                                    //datarow["Unit"] = domp.UnitID;
                                    datarow["Configuration"] = strConfiguration;
                                    GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].Rows.Add(datarow);
                                    GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                                }
                                DataColumn myDC = new DataColumn(GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].Columns[0].ColumnName, System.Type.GetType("System.Int32"));
                                GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                                DataView dv = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].DefaultView;
                                dv.Sort = "CellNo ASC";
                                GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                                DataTable ordered = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable()
                                                    .OrderBy(r => int.Parse(r.Field<String>("CellNo")))
                                                    .CopyToDataTable();
                                ordered.TableName = GDSlave.CurrentSlave;
                                GDSlave.DsExcelData.Tables.Remove(GDSlave.CurrentSlave);
                                GDSlave.DsExcelData.Tables.Add(ordered);
                            }
                            #endregion Update Dataset Tables as per Slave

                            //Namrata: 16/09/2019
                            //    #region Update Dataset Tables as per Slave

                            //    string strConfiguration = domp.CellNo + "," + domp.Widget + "," + "DO" + "," + domp.Description + "," + domp.DONo + "," + domp.UnitID;

                            //    if (GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && (x["Type"].ToString() == "DO" && ((x["DBIndex"].ToString() == domp.DONo) || string.IsNullOrEmpty(x["DBIndex"].ToString())))).Any())
                            //    {
                            //        DataRow datarow;
                            //        datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && x["Type"].ToString() == "DO" && ((x["DBIndex"].ToString() == domp.DONo) || string.IsNullOrEmpty(x["DBIndex"].ToString()))).ToList().First();
                            //        //datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && x["Type"].ToString() == "DO" ).ToList().First();
                            //        datarow["CellNo"] = domp.CellNo;
                            //        datarow["Widget"] = domp.Widget;
                            //        datarow["Type"] = "DO";
                            //        datarow["Name"] = domp.Description;
                            //        datarow["DBIndex"] = domp.DONo;
                            //        datarow["Unit"] = domp.UnitID;
                            //        datarow["Configuration"] = strConfiguration;
                            //        GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                            //    }
                            //    else if (GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && string.IsNullOrEmpty(x["Type"].ToString())).ToList().Any())
                            //    {
                            //        DataRow datarow;
                            //            datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && string.IsNullOrEmpty(x["Type"].ToString())).ToList().First();
                            //        //datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && x["Type"].ToString() == "DO" && ((x["DBIndex"].ToString() == domp.DONo) || string.IsNullOrEmpty(x["DBIndex"].ToString()))).ToList().First();
                            //        datarow["CellNo"] = domp.CellNo;
                            //        datarow["Widget"] = domp.Widget;
                            //        datarow["Type"] = "DO";
                            //        datarow["Name"] = domp.Description;
                            //        datarow["DBIndex"] = domp.DONo;
                            //        datarow["Unit"] = domp.UnitID;
                            //        datarow["Configuration"] = strConfiguration;
                            //        GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                            //    }
                            //    else
                            //    {
                            //            DataRow datarow;
                            //            datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].NewRow();
                            //            datarow["CellNo"] = domp.CellNo;
                            //            datarow["Widget"] = domp.Widget;
                            //            datarow["Type"] = "DO";
                            //            datarow["Name"] = domp.Description;
                            //            datarow["DBIndex"] = domp.DONo;
                            //            datarow["Unit"] = domp.UnitID;
                            //            datarow["Configuration"] = strConfiguration;
                            //            GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].Rows.Add(datarow);
                            //            GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                            //    }
                            //    DataColumn myDC = new DataColumn(GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].Columns[0].ColumnName, System.Type.GetType("System.Int32"));
                            //    GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                            //    DataView dv = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].DefaultView;
                            //    dv.Sort = "CellNo ASC";
                            //    GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                            //    DataTable ordered = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable()
                            //                        .OrderBy(r => int.Parse(r.Field<String>("CellNo")))
                            //                        .CopyToDataTable();
                            //    ordered.TableName = GDSlave.CurrentSlave;
                            //    GDSlave.DsExcelData.Tables.Remove(GDSlave.CurrentSlave);
                            //    GDSlave.DsExcelData.Tables.Add(ordered);
                            //}
                            //    #endregion Update Dataset Tables as per Slave
                        }

                        #endregion Graphical Display

                        #region SlaveTypes.MQTTSLAVE
                        if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MQTTSLAVE)
                        {
                            foreach (DOMap domp in tmpList)
                            {
                                string[] row = new string[15];
                                if (domp.IsNodeComment)
                                {
                                    row[0] = "Comment...";
                                }
                                else
                                {
                                    row[0] = domp.DONo;
                                    row[2] = domp.ReportingIndex;
                                    row[3] = domp.DataType;
                                    row[4] = "";
                                    row[5] = domp.BitPos;
                                    row[6] = "";
                                    row[8] = "";
                                    row[9] = "";
                                    row[10] = domp.Key;
                                    row[11] = "";
                                    row[12] = "";
                                    row[13] = "";
                                    row[14] = domp.Description;
                                }
                                ListViewItem lvItem = new ListViewItem(row);
                                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                                ucdo.lvDOMap.Items.Add(lvItem);
                            }
                        }
                        #endregion SlaveTypes.IEC61850Server

                        #region SlaveTypes.SMSSLAVE
                        if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.SMSSLAVE)
                        {
                            foreach (DOMap domp in tmpList)
                            {
                                string[] row = new string[15];
                                if (domp.IsNodeComment)
                                {
                                    row[0] = "Comment...";
                                }
                                else
                                {
                                    row[0] = domp.DONo;
                                    row[2] = domp.ReportingIndex;
                                    row[3] = domp.DataType;
                                    row[4] = "";
                                    row[5] = domp.BitPos;
                                    row[6] = domp.Select;
                                    row[8] = "";
                                    row[9] = "";
                                    row[10] = "";
                                    row[11] = "";
                                    row[12] = "";
                                    row[13] = "";
                                    row[14] = domp.Description;
                                }
                                ListViewItem lvItem = new ListViewItem(row);
                                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                                ucdo.lvDOMap.Items.Add(lvItem);
                            }
                        }
                        #endregion SlaveTypes.IEC61850Server
                        #region SlaveTypes.IEC61850Server
                        if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server)
                        {
                            foreach (DOMap domp in tmpList)
                            {
                                string[] row = new string[15];
                                if (domp.IsNodeComment)
                                {
                                    row[0] = "Comment...";
                                }
                                else
                                {
                                    row[0] = domp.DONo;
                                    row[1] = "";
                                    row[2] = domp.IEC61850ReportingIndex;
                                    row[3] = domp.DataType;
                                    row[4] = domp.CommandType;
                                    row[5] = domp.BitPos;
                                    row[6] = domp.Select;
                                    row[7] = "";
                                    row[8] = "";
                                    row[9] = "";
                                    row[10] = "";
                                    row[11] = "";
                                    row[12] = "";
                                    row[13] = "";
                                    row[14] = domp.Description;
                                }
                                ListViewItem lvItem = new ListViewItem(row);
                                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                                ucdo.lvDOMap.Items.Add(lvItem);
                            }
                        }
                        #endregion SlaveTypes.IEC61850Server

                        #region SlaveTypes.DNP
                        if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE)
                        {
                            foreach (DOMap domp in tmpList)
                            {
                                string[] row = new string[15];
                                if (domp.IsNodeComment)
                                {
                                    row[0] = "Comment...";
                                }
                                else
                                {
                                    row[0] = domp.DONo;
                                    row[2] = domp.ReportingIndex;
                                    row[3] = "";
                                    row[4] = domp.CommandType;
                                    row[5] = domp.BitPos;
                                    row[6] = domp.Select;
                                    row[8] = "";
                                    row[9] = "";
                                    row[10] = "";
                                    row[11] = domp.EventClass;
                                    row[12] = domp.Variation;
                                    row[13] = domp.EventVariation;
                                    row[14] = domp.Description;
                                }
                                ListViewItem lvItem = new ListViewItem(row);
                                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                                ucdo.lvDOMap.Items.Add(lvItem);
                            }
                        }
                        #endregion SlaveTypes.DNP
                    }));
                }
                else
                {
                    #region SlaveTypes.MODBUSSLAVE
                    if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE))
                    {
                        foreach (DOMap domp in tmpList)
                        {
                            string[] row = new string[15];
                            if (domp.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = domp.DONo;
                                row[2] = domp.ReportingIndex;
                                row[3] = domp.DataType;
                                row[4] = domp.CommandType;
                                row[5] = domp.BitPos;
                                row[6] = domp.Select;
                                row[8] = "";
                                row[9] = "";
                                row[10] = "";
                                row[11] = "";
                                row[12] = "";
                                row[13] = "";
                                row[14] = domp.Description;
                            }

                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucdo.lvDOMap.Items.Add(lvItem);
                        }
                    }
                    #endregion SlaveTypes.MODBUSSLAVE

                    #region SlaveTypes.SPORTSLAVE
                    else if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.SPORTSLAVE))
                    {
                        foreach (DOMap domp in tmpList)
                        {
                            string[] row = new string[15];
                            if (domp.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = domp.DONo;
                                row[1] = domp.UnitID;
                                row[2] = domp.ReportingIndex;
                                row[3] = domp.DataType;
                                row[5] = domp.BitPos;
                                row[6] = domp.Select;
                                row[7] = domp.Used;
                                row[8] = "";
                                row[9] = "";
                                row[10] = "";
                                row[11] = "";
                                row[12] = "";
                                row[13] = "";
                                row[14] = domp.Description;
                            }

                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucdo.lvDOMap.Items.Add(lvItem);
                        }
                    }
                    #endregion SlaveTypes.SPORTSLAVE

                    #region SlaveTypes.IEC101SLAVE,IEC104
                    else if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC101SLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC104))
                    {
                        foreach (DOMap domp in tmpList)
                        {
                            string[] row = new string[15];
                            if (domp.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = domp.DONo;
                                row[2] = domp.ReportingIndex;
                                row[3] = domp.DataType;
                                row[4] = domp.CommandType;
                                row[5] = domp.BitPos;
                                row[6] = domp.Select;
                                row[8] = "";
                                row[9] = "";
                                row[10] = "";
                                row[11] = "";
                                row[12] = "";
                                row[13] = "";
                                row[14] = domp.Description;
                            }

                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucdo.lvDOMap.Items.Add(lvItem);
                        }
                    }
                    #endregion SlaveTypes.IEC101SLAVE,IEC104

                    #region SlaveTypes.IEC61850Server
                    if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server)
                    {
                        foreach (DOMap domp in tmpList)
                        {
                            string[] row = new string[15];
                            if (domp.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = domp.DONo;
                                row[1] = "";
                                row[2] = domp.IEC61850ReportingIndex;
                                row[3] = domp.DataType;
                                row[4] = domp.CommandType;
                                row[5] = domp.BitPos;
                                row[6] = domp.Select;
                                row[7] = "";
                                row[8] = "";
                                row[9] = "";
                                row[10] = "";
                                row[11] = "";
                                row[12] = "";
                                row[13] = "";
                                row[14] = domp.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucdo.lvDOMap.Items.Add(lvItem);
                        }
                    }
                    #endregion SlaveTypes.IEC61850Server

                    #region Graphical Display
                    if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.GRAPHICALDISPLAYSLAVE))
                    {

                        foreach (DOMap domp in tmpList)
                        {
                            string[] row = new string[15];
                            if (domp.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = domp.DONo;
                                row[1] = "";
                                row[2] = "";
                                row[3] = "";
                                row[4] = "";
                                row[5] = "";
                                row[6] = "";
                                row[7] = "";
                                row[8] = domp.CellNo;
                                row[9] = domp.Widget;
                                row[10] = "";
                                row[11] = "";
                                row[12] = "";
                                row[13] = "";
                                row[14] = domp.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucdo.lvDOMap.Items.Add(lvItem);



                            #region Update Dataset Tables as per Slave

                            string strConfiguration = domp.CellNo + "," + domp.Widget + "," + "DO" + "," + domp.Description + "," + domp.DONo + "," + domp.UnitID;

                            if (GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && (x["Type"].ToString() == "DO" && ((x["DBIndex"].ToString() == domp.DONo) || string.IsNullOrEmpty(x["DBIndex"].ToString())))).Any())
                            {
                                DataRow datarow;
                                datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && x["Type"].ToString() == "DO" && ((x["DBIndex"].ToString() == domp.DONo) || string.IsNullOrEmpty(x["DBIndex"].ToString()))).ToList().First();
                                //datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && x["Type"].ToString() == "DO" ).ToList().First();
                                datarow["CellNo"] = domp.CellNo;
                                datarow["Widget"] = domp.Widget;
                                datarow["Type"] = "DO";
                                datarow["Name"] = domp.Description;
                                datarow["DBIndex"] = domp.DONo;
                                //Namrata: 04/12/2019
                                datarow["Unit"] = "";
                                //datarow["Unit"] = domp.UnitID;
                                datarow["Configuration"] = strConfiguration;
                                GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                            }
                            else if (GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && string.IsNullOrEmpty(x["Type"].ToString())).ToList().Any())
                            {
                                DataRow datarow;
                                datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && string.IsNullOrEmpty(x["DBIndex"].ToString()) && ((x["DBIndex"].ToString() == domp.DONo) || string.IsNullOrEmpty(x["DBIndex"].ToString()))).ToList().First();
                                //datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && x["Type"].ToString() == "DO" && ((x["DBIndex"].ToString() == domp.DONo) || string.IsNullOrEmpty(x["DBIndex"].ToString()))).ToList().First();
                                datarow["CellNo"] = domp.CellNo;
                                datarow["Widget"] = domp.Widget;
                                datarow["Type"] = "DO";
                                datarow["Name"] = domp.Description;
                                datarow["DBIndex"] = domp.DONo;
                                //Namrata: 04/12/2019
                                datarow["Unit"] = "";
                                //datarow["Unit"] = domp.UnitID;
                                datarow["Configuration"] = strConfiguration;
                                GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                            }
                            else
                            {
                                DataRow datarow;
                                datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].NewRow();
                                datarow["CellNo"] = domp.CellNo;
                                datarow["Widget"] = domp.Widget;
                                datarow["Type"] = "DO";
                                datarow["Name"] = domp.Description;
                                datarow["DBIndex"] = domp.DONo;
                                //Namrata: 04/12/2019
                                datarow["Unit"] = "";
                                //datarow["Unit"] = domp.UnitID;
                                datarow["Configuration"] = strConfiguration;
                                GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].Rows.Add(datarow);
                                GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                            }
                            DataColumn myDC = new DataColumn(GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].Columns[0].ColumnName, System.Type.GetType("System.Int32"));
                            GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                            DataView dv = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].DefaultView;
                            dv.Sort = "CellNo ASC";
                            GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                            DataTable ordered = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable()
                                                .OrderBy(r => int.Parse(r.Field<String>("CellNo")))
                                                .CopyToDataTable();
                            ordered.TableName = GDSlave.CurrentSlave;
                            GDSlave.DsExcelData.Tables.Remove(GDSlave.CurrentSlave);
                            GDSlave.DsExcelData.Tables.Add(ordered);
                        }
                        #endregion Update Dataset Tables as per Slave

                        //Namrata: 16/09/2019
                        //    #region Update Dataset Tables as per Slave

                        //    string strConfiguration = domp.CellNo + "," + domp.Widget + "," + "DO" + "," + domp.Description + "," + domp.DONo + "," + domp.UnitID;

                        //    if (GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && (x["Type"].ToString() == "DO" && ((x["DBIndex"].ToString() == domp.DONo) || string.IsNullOrEmpty(x["DBIndex"].ToString())))).Any())
                        //    {
                        //        DataRow datarow;
                        //        datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && x["Type"].ToString() == "DO" && ((x["DBIndex"].ToString() == domp.DONo) || string.IsNullOrEmpty(x["DBIndex"].ToString()))).ToList().First();
                        //        //datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && x["Type"].ToString() == "DO" ).ToList().First();
                        //        datarow["CellNo"] = domp.CellNo;
                        //        datarow["Widget"] = domp.Widget;
                        //        datarow["Type"] = "DO";
                        //        datarow["Name"] = domp.Description;
                        //        datarow["DBIndex"] = domp.DONo;
                        //        datarow["Unit"] = domp.UnitID;
                        //        datarow["Configuration"] = strConfiguration;
                        //        GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                        //    }
                        //    else if (GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && string.IsNullOrEmpty(x["Type"].ToString())).ToList().Any())
                        //    {
                        //        DataRow datarow;
                        //            datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && string.IsNullOrEmpty(x["Type"].ToString())).ToList().First();
                        //        //datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable().Where(x => x["CellNo"].ToString() == domp.CellNo && x["Type"].ToString() == "DO" && ((x["DBIndex"].ToString() == domp.DONo) || string.IsNullOrEmpty(x["DBIndex"].ToString()))).ToList().First();
                        //        datarow["CellNo"] = domp.CellNo;
                        //        datarow["Widget"] = domp.Widget;
                        //        datarow["Type"] = "DO";
                        //        datarow["Name"] = domp.Description;
                        //        datarow["DBIndex"] = domp.DONo;
                        //        datarow["Unit"] = domp.UnitID;
                        //        datarow["Configuration"] = strConfiguration;
                        //        GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                        //    }
                        //    else
                        //    {
                        //            DataRow datarow;
                        //            datarow = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].NewRow();
                        //            datarow["CellNo"] = domp.CellNo;
                        //            datarow["Widget"] = domp.Widget;
                        //            datarow["Type"] = "DO";
                        //            datarow["Name"] = domp.Description;
                        //            datarow["DBIndex"] = domp.DONo;
                        //            datarow["Unit"] = domp.UnitID;
                        //            datarow["Configuration"] = strConfiguration;
                        //            GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].Rows.Add(datarow);
                        //            GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                        //    }
                        //    DataColumn myDC = new DataColumn(GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].Columns[0].ColumnName, System.Type.GetType("System.Int32"));
                        //    GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                        //    DataView dv = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].DefaultView;
                        //    dv.Sort = "CellNo ASC";
                        //    GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AcceptChanges();
                        //    DataTable ordered = GDSlave.DsExcelData.Tables[GDSlave.CurrentSlave].AsEnumerable()
                        //                        .OrderBy(r => int.Parse(r.Field<String>("CellNo")))
                        //                        .CopyToDataTable();
                        //    ordered.TableName = GDSlave.CurrentSlave;
                        //    GDSlave.DsExcelData.Tables.Remove(GDSlave.CurrentSlave);
                        //    GDSlave.DsExcelData.Tables.Add(ordered);
                        //}
                        //    #endregion Update Dataset Tables as per Slave
                    }

                    #endregion Graphical Display

                    #region SlaveTypes.MQTTSLAVE
                    if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MQTTSLAVE)
                    {
                        foreach (DOMap domp in tmpList)
                        {
                            string[] row = new string[15];
                            if (domp.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = domp.DONo;
                                row[2] = domp.ReportingIndex;
                                row[3] = domp.DataType;
                                row[4] = "";
                                row[5] = domp.BitPos;
                                row[6] = "";
                                row[8] = "";
                                row[9] = "";
                                row[10] = domp.Key;
                                row[11] = "";
                                row[12] = "";
                                row[13] = "";
                                row[14] = domp.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucdo.lvDOMap.Items.Add(lvItem);
                        }
                    }
                    #endregion SlaveTypes.IEC61850Server

                    #region SlaveTypes.SMSSLAVE
                    if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.SMSSLAVE)
                    {
                        foreach (DOMap domp in tmpList)
                        {
                            string[] row = new string[15];
                            if (domp.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = domp.DONo;
                                row[2] = domp.ReportingIndex;
                                row[3] = domp.DataType;
                                row[4] = "";
                                row[5] = domp.BitPos;
                                row[6] = domp.Select;
                                row[8] = "";
                                row[9] = "";
                                row[10] = "";
                                row[11] = "";
                                row[12] = "";
                                row[13] = "";
                                row[14] = domp.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucdo.lvDOMap.Items.Add(lvItem);
                        }
                    }
                    #endregion SlaveTypes.IEC61850Server
                    #region SlaveTypes.IEC61850Server
                    if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server)
                    {
                        foreach (DOMap domp in tmpList)
                        {
                            string[] row = new string[15];
                            if (domp.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = domp.DONo;
                                row[1] = "";
                                row[2] = domp.IEC61850ReportingIndex;
                                row[3] = domp.DataType;
                                row[4] = domp.CommandType;
                                row[5] = domp.BitPos;
                                row[6] = domp.Select;
                                row[7] = "";
                                row[8] = "";
                                row[9] = "";
                                row[10] = "";
                                row[11] = "";
                                row[12] = "";
                                row[13] = "";
                                row[14] = domp.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucdo.lvDOMap.Items.Add(lvItem);
                        }
                    }
                    #endregion SlaveTypes.IEC61850Server

                    #region SlaveTypes.DNP
                    if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE)
                    {
                        foreach (DOMap domp in tmpList)
                        {
                            string[] row = new string[15];
                            if (domp.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = domp.DONo;
                                row[2] = domp.ReportingIndex;
                                row[3] = "";
                                row[4] = domp.CommandType;
                                row[5] = domp.BitPos;
                                row[6] = domp.Select;
                                row[8] = "";
                                row[9] = "";
                                row[10] = "";
                                row[11] = domp.EventClass;
                                row[12] = domp.Variation;
                                row[13] = domp.EventVariation;
                                row[14] = domp.Description;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucdo.lvDOMap.Items.Add(lvItem);
                        }
                    }
                    #endregion SlaveTypes.DNP
                }
                ucdo.lblDOMRecords.Text = tmpList.Count.ToString();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void refreshCurrentMapList()
        {
            fillMapOptions(Utils.getSlaveTypes(currentSlave));
            List<DOMap> sdomList;
            if (!slavesDOMapList.TryGetValue(currentSlave, out sdomList))
            {
                refreshMapList(null);
            }
            else
            {
                refreshMapList(sdomList);
            }
        }
        private void cmbIEDName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "DO : cmbIEDName_SelectedIndexChanged";
            try
            {
                //Namrata: 04/04/2018
                if (ucdo.cmbIEDName.Focused == false)
                {

                }
                else
                {
                    Utils.Iec61850IEDname = ucdo.cmbIEDName.Text;
                    List<DataTable> dtList = Utils.dsResponseType.Tables.OfType<DataTable>().Where(tbl => tbl.TableName.StartsWith(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname)).ToList();
                    if (dtList.Count == 0)
                    {
                        ucdo.cmbIEC61850ResponseType.DataSource = null;
                        ucdo.cmbIEC61850Index.DataSource = null;
                        ucdo.cmbIEC61850ResponseType.Enabled = false;
                        ucdo.cmbIEC61850Index.Enabled = false;
                        //Ajay: 17/01/2019
                        ucdo.cmbFC.DataSource = null;
                    }
                    else
                    {
                        ucdo.cmbIEC61850ResponseType.Enabled = true;
                        ucdo.cmbIEC61850Index.Enabled = true;
                        ucdo.cmbIEC61850ResponseType.DataSource = Utils.dsResponseType.Tables[Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname];//[Utils.strFrmOpenproplusTreeNode + "/" + "Undefined" + "/" + Utils.Iec61850IEDname];
                        ucdo.cmbIEC61850ResponseType.DisplayMember = "Address";
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
            string strRoutineName = "DO : cmb61850DIIndex_SelectedIndexChanged";
            try
            {
                if (ucdo.cmbIEC61850Index.Items.Count > 0)
                {
                    if (ucdo.cmbIEC61850Index.SelectedIndex != -1)
                    {
                        ucdo.cmbFC.SelectedIndex = ucdo.cmbFC.Items.IndexOf(((DataRowView)ucdo.cmbIEC61850Index.SelectedItem).Row[2].ToString());
                        Utils.IEC61850Index = (((DataRowView)ucdo.cmbIEC61850Index.SelectedItem).Row[0].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        DataSet dsdummy = null; //Ajay: 17/01/2019
        private void cmb61850DIResponseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "DOConfiguration:cmb61850ResponseType_SelectedIndexChanged";
            try
            {
                if (ucdo.cmbIEC61850ResponseType.Items.Count > 1)
                {
                    if ((ucdo.cmbIEC61850ResponseType.SelectedIndex != -1))
                    {
                        Utils.Iec61850IEDname = ucdo.cmbIEDName.Text;//Namrata: 04/04/2018
                        List<DataTable> dtList = Utils.DsAllConfigurationData.Tables.OfType<DataTable>().Where(tbl => tbl.TableName.StartsWith(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname)).ToList();
                        dsdummy = new DataSet();
                        dtList.ForEach(tbl => { DataTable dt = tbl.Copy(); dsdummy.Tables.Add(dt); });
                        ucdo.cmbFC.DataSource = dsdummy.Tables[ucdo.cmbIEC61850ResponseType.SelectedIndex].AsEnumerable().Select(x => x.Field<string>("FC")).Distinct().ToList();//Ajay: 17/01/2019
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ChkIEC61850Index_SelectedIndexChanged(object sender, EventArgs e)//Namrata:22/03/2019
        {
            string strRoutineName = "DOConfiguration:cmb61850Index_SelectedIndexChanged";
            try
            {
                if (ucdo.ChkIEC61850Index.Items.Count > 0)
                {
                    if (ucdo.ChkIEC61850Index.SelectedIndex != -1)
                    {
                        string a = ucdo.ChkIEC61850Index.Text;
                        //string FindObjRef = MergeList.Where(x => x.Contains(a.ToString() + ",")).Select(x => x).FirstOrDefault();
                        //string[] GetFC = FindObjRef.Split(',');
                        //ucdo.cmbFC.SelectedIndex = ucdo.cmbFC.FindStringExact(GetFC[1].ToString());
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
            string strRoutineName = "DOConfiguration:cmbFC_SelectedIndexChanged";
            try
            {
                string FC = ucdo.cmbFC.Text;
                DataTable DT = FilteredIndexDT(FC);
                if (DT != null)
                {
                    List<string> FC1 = DT.AsEnumerable().Select(x => x[0].ToString()).ToList();//Namrata:25/03/2019
                    ucdo.ChkIEC61850Index.Items.Clear();
                    ucdo.ChkIEC61850Index.Text = "";//Namrata:25/03/2019
                    foreach (var kv in FC1)
                    {
                        ucdo.ChkIEC61850Index.Items.Add(kv.ToString());
                    }
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
            string strRoutineName = "DOConfiguration:FilteredIndexDT";
            try
            {
                DataTable DT = new DataTable();
                DT.Columns.Add("ObjectReferrence");
                DT.Columns.Add("Node");
                DT.Columns.Add("FC");
                DataRow[] drRwArry = dsdummy.Tables[ucdo.cmbIEC61850ResponseType.SelectedIndex].AsEnumerable().Where(x => x.Field<string>("FC") == FC).ToArray();
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
        private void fillOptions()
        {
            string strRoutineName = "DOConfiguration:fillOptions";
            try
            {
                ucdo.cmbControlType.Items.Clear();
                //Namrata: 28/09/2017
                //For RCB Configuration 
                dataGridViewDataSet.DataSource = null;
                dtdataset.Clear();
                dataGridViewDataSet.Rows.Clear();
                dataGridViewDataSet.Visible = false;
                dtdataset.Rows.Clear(); dtdataset.Columns.Clear();

                if (!dtdataset.Columns.Contains("Address")) //Ajay: 12/11/2018 Condition checked to handle exception
                { DataColumn dcAddressColumn = dtdataset.Columns.Add("Address", typeof(string)); }
                if (!dtdataset.Columns.Contains("IED")) //Ajay: 12/11/2018 Condition checked to handle exception
                { dtdataset.Columns.Add("IED", typeof(string)); }

                //Namrata: 31/10/2017
                ucdo.cmbIEDName.DataSource = Utils.dsIED.Tables[Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname];// Utils.DtIEDName;
                ucdo.cmbIEDName.DisplayMember = "IEDName";
                //Namrata: 04/04/2018
                if (Utils.Iec61850IEDname != "")
                {
                    ucdo.cmbIEDName.Text = Utils.Iec61850IEDname;
                }

                //Fill ResponseType For IEC61850Client
                //Namrata: 31/10/2017
                ucdo.cmbIEC61850ResponseType.DataSource = Utils.dsResponseType.Tables[Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname];// Utils.DtAddress;
                ucdo.cmbIEC61850ResponseType.DisplayMember = "Address";

                //Fill Response Type...
                if (masterType == MasterTypes.IEC61850Client)
                {
                    ucdo.cmbResponseType.Items.Clear();
                }
                else
                {
                    ucdo.cmbResponseType.Items.Clear();
                    if (masterType != MasterTypes.LoadProfile) //Ajay: 31/07/2018
                    {
                        foreach (String rt in DO.getResponseTypes(masterType))
                        {
                            ucdo.cmbResponseType.Items.Add(rt.ToString());
                        }
                        ucdo.cmbResponseType.SelectedIndex = 0;
                    }
                }
                //Namrata: 25/01/2019
                //Fill Control Type...
                if (masterType == MasterTypes.IEC61850Client)
                {
                    ucdo.cmbControlType.Items.Clear();
                    //Namrata:28/01/2019
                    ucdo.cmbCModel.Items.Clear();
                }
                else { }

                if (masterType != MasterTypes.LoadProfile) //Ajay: 31/07/2018
                {
                    foreach (String ct in DO.getControlTypes(masterType))
                    {
                        ucdo.cmbControlType.Items.Add(ct.ToString());
                    }
                    ucdo.cmbControlType.SelectedIndex = 0;
                }
                //}

                //Ajay: 04/08/2018
                foreach (String ct in DO.GetControlModels(masterType))
                {
                    ucdo.cmbCModel.Items.Add(ct.ToString());
                }
                if (ucdo.cmbCModel != null && ucdo.cmbCModel.Items.Count > 0) { ucdo.cmbCModel.SelectedIndex = 0; }
            }

            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void fillMapOptions(SlaveTypes sType)
        {
            /***** Fill Map details related combobox ******/
            try
            {
                //Fill Data Type...
                ucdo.cmbDOMDataType.Items.Clear();
                foreach (String dt in DOMap.getDataTypes(sType))
                {
                    ucdo.cmbDOMDataType.Items.Add(dt.ToString());
                }
                if (ucdo.cmbDOMDataType.Items.Count > 0) ucdo.cmbDOMDataType.SelectedIndex = 0;
                #region Fill Variations
                ucdo.CmbVari.Items.Clear();
                foreach (String dt in AIMap.getVariartions(sType))
                {
                    ucdo.CmbVari.Items.Add(dt.ToString());
                }
                if (ucdo.CmbVari.Items.Count > 0) ucdo.CmbVari.SelectedIndex = 0;
                #endregion Fill Variations

                #region Fill EVariations
                ucdo.CmbEV.Items.Clear();
                foreach (String dt in AIMap.getEventsVariations(sType))
                {
                    ucdo.CmbEV.Items.Add(dt.ToString());
                }
                if (ucdo.CmbEV.Items.Count > 0) ucdo.CmbEV.SelectedIndex = 0;
                #endregion Fill EVariations

                #region Fill EClass
                ucdo.cmbEventC.Items.Clear();
                foreach (String dt in AIMap.getEventsClasses(sType))
                {
                    ucdo.cmbEventC.Items.Add(dt.ToString());
                }
                if (ucdo.cmbEventC.Items.Count > 0) ucdo.cmbEventC.SelectedIndex = 0;
                #endregion Fill EClass
            }
            catch (System.NullReferenceException)
            {
                Utils.WriteLine(VerboseLevel.ERROR, "DO Map DataType does not exist for Slave Type: {0}", sType.ToString());
            }

            try
            {
                //Fill Command Type...
                ucdo.cmbDOMCommandType.Items.Clear();
                foreach (String ct in DOMap.getCommandTypes(sType))
                {
                    ucdo.cmbDOMCommandType.Items.Add(ct.ToString());
                }
                if (ucdo.cmbDOMCommandType.Items.Count > 0) ucdo.cmbDOMCommandType.SelectedIndex = 0;
                #region Fill Variations
                ucdo.CmbVari.Items.Clear();
                foreach (String dt in DOMap.getVariartions(sType))
                {
                    ucdo.CmbVari.Items.Add(dt.ToString());
                }
                if (ucdo.CmbVari.Items.Count > 0) ucdo.CmbVari.SelectedIndex = 0;
                #endregion Fill Variations

                #region Fill EVariations
                ucdo.CmbEV.Items.Clear();
                foreach (String dt in DOMap.getEventsVariations(sType))
                {
                    ucdo.CmbEV.Items.Add(dt.ToString());
                }
                if (ucdo.CmbEV.Items.Count > 0) ucdo.CmbEV.SelectedIndex = 0;
                #endregion Fill EVariations

                #region Fill EClass
                ucdo.cmbEventC.Items.Clear();
                foreach (String dt in DOMap.getEventsClasses(sType))
                {
                    ucdo.cmbEventC.Items.Add(dt.ToString());
                }
                if (ucdo.cmbEventC.Items.Count > 0) ucdo.cmbEventC.SelectedIndex = 0;
                #endregion Fill EClass
            }
            catch (System.NullReferenceException)
            {
                Utils.WriteLine(VerboseLevel.ERROR, "DO Map CommandType does not exist for Slave Type: {0}", sType.ToString());
            }
        }
        private void addListHeaders()
        {
            #region Master
            if (masterType == MasterTypes.MODBUS)
            {
                ucdo.lvDOlist.Columns.Add("DO No.", 70, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Sub Index", 70, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Control Type", 110, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Pulse Duration(ms)", -2, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Enable DI", "Enable DI", 60, HorizontalAlignment.Left, String.Empty);
                ucdo.lvDOlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                //ucdo.lvDOlist.Columns.Add("Description", 100, HorizontalAlignment.Left);
            }
            else if (masterType == MasterTypes.ADR)
            {
                ucdo.lvDOlist.Columns.Add("DO No.", 70, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Sub Index", 70, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Control Type", 110, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Pulse Duration(ms)", -2, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Enable DI", "Enable DI", 60, HorizontalAlignment.Left, String.Empty);
                ucdo.lvDOlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                //ucdo.lvDOlist.Columns.Add("Description", 100, HorizontalAlignment.Left);
            }
            else if (masterType == MasterTypes.IEC101)
            {
                ucdo.lvDOlist.Columns.Add("DO No.", 70, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Sub Index", 70, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Control Type", 110, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Pulse Duration(ms)", -2, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Enable DI", "Enable DI", 60, HorizontalAlignment.Left, String.Empty);
                ucdo.lvDOlist.Columns.Add("Select", "Select", 60, HorizontalAlignment.Left, String.Empty);
                ucdo.lvDOlist.Columns.Add("Description", "Description", 100, HorizontalAlignment.Left, String.Empty);
            }
            else if (masterType == MasterTypes.SPORT)
            {
                ucdo.lvDOlist.Columns.Add("DO No.", 70, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Sub Index", 70, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Control Type", 110, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Pulse Duration(ms)", -2, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Enable DI", "Enable DI", 60, HorizontalAlignment.Left, String.Empty);
                ucdo.lvDOlist.Columns.Add("Select", "Select", 60, HorizontalAlignment.Left, String.Empty);
                ucdo.lvDOlist.Columns.Add("Description", "Description", 100, HorizontalAlignment.Left, String.Empty);
            }
            else if (masterType == MasterTypes.IEC104)
            {
                ucdo.lvDOlist.Columns.Add("DO No.", 70, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Sub Index", 70, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Control Type", 110, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Pulse Duration(ms)", -2, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Enable DI", "Enable DI", 60, HorizontalAlignment.Left, String.Empty);
                ucdo.lvDOlist.Columns.Add("Select", "Select", 60, HorizontalAlignment.Left, String.Empty);
                //Ajay: 12/11/2018 Event Removed
                //ucdo.lvDOlist.Columns.Add("Event", "Event", 60, HorizontalAlignment.Left, String.Empty); //Ajay: 18/07/2018 
                ucdo.lvDOlist.Columns.Add("Description", "Description", 100, HorizontalAlignment.Left, String.Empty);
            }
            else if (masterType == MasterTypes.IEC103)
            {
                ucdo.lvDOlist.Columns.Add("DO No.", 70, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Sub Index", 70, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Control Type", 110, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Pulse Duration(ms)", -2, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Enable DI", "Enable DI", 60, HorizontalAlignment.Left, String.Empty);
                ucdo.lvDOlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                //ucdo.lvDOlist.Columns.Add("Description", 100, HorizontalAlignment.Left);
            }
            else if (masterType == MasterTypes.Virtual)
            {
                ucdo.lvDOlist.Columns.Add("DO No.", 70, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Sub Index", 70, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Control Type", 110, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Pulse Duration(ms)", -2, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Enable DI", "Enable DI", 60, HorizontalAlignment.Left, String.Empty);
                ucdo.lvDOlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                //ucdo.lvDOlist.Columns.Add("Description", 100, HorizontalAlignment.Left);
            }
            else if (masterType == MasterTypes.IEC61850Client)
            {
                ucdo.lvDOlist.Columns.Add("DO No.", 70, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("IED Name", 110, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Response Type", 210, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Index", 310, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Control Model", 90, HorizontalAlignment.Left); //Ajay: 04/08/2018s Control Model added
                ucdo.lvDOlist.Columns.Add("FC", 50, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Sub Index", 70, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Control Type", 90, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Pulse Duration(ms)", -2, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Enable DI", "Enable DI", 60, HorizontalAlignment.Left, String.Empty);
                ucdo.lvDOlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                //ucdo.lvDOlist.Columns.Add("Description", 100, HorizontalAlignment.Left);
                //Namrata: 15/9/2017
                //Hide IED Name
                ucdo.lvDOlist.Columns[1].Width = 0;
            }
            //Ajay: 31/07/2018
            else if (masterType == MasterTypes.LoadProfile)
            {
                ucdo.lvDOlist.Columns.Add("DO No.", 70, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
            }
            #endregion Master

            #region Slave
            //Add DO map headers...
            ucdo.lvDOMap.Columns.Add("DO No.", 70, HorizontalAlignment.Left);
            ucdo.lvDOMap.Columns.Add("Unit ID", 0, HorizontalAlignment.Left);
            ucdo.lvDOMap.Columns.Add("Reporting Index", 130, HorizontalAlignment.Left);
            ucdo.lvDOMap.Columns.Add("Data Type", 130, HorizontalAlignment.Left);
            ucdo.lvDOMap.Columns.Add("Command Type", 0, HorizontalAlignment.Left);
            ucdo.lvDOMap.Columns.Add("Bit Position", 80, HorizontalAlignment.Left);
            ucdo.lvDOMap.Columns.Add("Select", 150, HorizontalAlignment.Left);
            ucdo.lvDOMap.Columns.Add("Used", 0, HorizontalAlignment.Left);
            //Namrata: 16/08/2019
            ucdo.lvDOMap.Columns.Add("CellNo", 0, HorizontalAlignment.Left);
            ucdo.lvDOMap.Columns.Add("Widget", 0, HorizontalAlignment.Left);
            ucdo.lvDOMap.Columns.Add("Key", "Key", 90, HorizontalAlignment.Left, null);
            //Namrata: 12/11/2019
            ucdo.lvDOMap.Columns.Add("Event Class", 0, HorizontalAlignment.Left);
            ucdo.lvDOMap.Columns.Add("Variation", 0, HorizontalAlignment.Left);
            ucdo.lvDOMap.Columns.Add("Event Variation", 0, HorizontalAlignment.Left);
            ucdo.lvDOMap.Columns.Add("Description", 150, HorizontalAlignment.Left);
            #endregion Slave
        }
        private void ShowHideSlaveColumns()
        {
            string strRoutineName = "AIConfiguration:ShowHideSlaveColumns";
            try
            {
                switch (Utils.getSlaveTypes(currentSlave))
                {
                    case SlaveTypes.IEC104:
                        ucdo.lvDOMap.Columns[1].Text = "Unit ID";
                        Utils.getColumnHeader(ucdo.lvDOMap, "DO No.").Width = 100;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Unit ID").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Reporting Index").Width = 150;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Data Type").Width = 150;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Command Type").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Bit Position").Width = 150;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Select").Width = 100;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Used").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "CellNo").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Widget").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Key").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Description").Width = 150;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Event Class").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Variation").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Event Variation").Width = 0;
                        break;

                    case SlaveTypes.MODBUSSLAVE:
                        ucdo.lvDOMap.Columns[1].Text = "Unit ID";
                        Utils.getColumnHeader(ucdo.lvDOMap, "DO No.").Width = 100;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Unit ID").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Reporting Index").Width = 150;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Data Type").Width = 150;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Command Type").Width = 100;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Bit Position").Width = 150;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Select").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Used").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "CellNo").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Widget").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Key").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Description").Width = 150;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Event Class").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Variation").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Event Variation").Width = 0;
                        break;

                    case SlaveTypes.IEC101SLAVE:
                        ucdo.lvDOMap.Columns[1].Text = "Unit ID";
                        Utils.getColumnHeader(ucdo.lvDOMap, "DO No.").Width = 100;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Unit ID").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Reporting Index").Width = 150;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Data Type").Width = 150;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Command Type").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Bit Position").Width = 150;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Select").Width = 100;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Used").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "CellNo").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Widget").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Key").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Description").Width = 150;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Event Class").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Variation").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Event Variation").Width = 0;
                        break;

                    case SlaveTypes.IEC61850Server:
                        ucdo.lvDOMap.Columns[1].Text = "Unit ID";
                        Utils.getColumnHeader(ucdo.lvDOMap, "DO No.").Width = 100;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Unit ID").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Reporting Index").Width = 150;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Data Type").Width = 150;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Command Type").Width = 100;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Bit Position").Width = 150;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Select").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Used").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "CellNo").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Widget").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Key").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Description").Width = 150;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Event Class").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Variation").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Event Variation").Width = 0;
                        break;

                    case SlaveTypes.SPORTSLAVE:
                        ucdo.lvDOMap.Columns[1].Text = "Unit ID";
                        Utils.getColumnHeader(ucdo.lvDOMap, "DO No.").Width = 100;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Unit ID").Width = 100;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Reporting Index").Width = 150;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Data Type").Width = 150;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Command Type").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Bit Position").Width = 150;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Select").Width = 100;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Used").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "CellNo").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Widget").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Key").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Description").Width = 150;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Event Class").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Variation").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Event Variation").Width = 0;
                        break;

                    //Namrata: 16/08/2019
                    case SlaveTypes.GRAPHICALDISPLAYSLAVE:
                        ucdo.lvDOMap.Columns[1].Text = "Unit";
                        Utils.getColumnHeader(ucdo.lvDOMap, "DO No.").Width = 100;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Unit").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Reporting Index").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Data Type").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Command Type").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Bit Position").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Select").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Used").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "CellNo").Width = 100;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Widget").Width = 150;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Key").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Description").Width = 150;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Event Class").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Variation").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Event Variation").Width = 0;
                        break;

                    case SlaveTypes.MQTTSLAVE:
                        ucdo.lvDOMap.Columns[1].Text = "Unit ID";
                        Utils.getColumnHeader(ucdo.lvDOMap, "DO No.").Width = 100;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Unit ID").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Reporting Index").Width = 150;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Data Type").Width = 150;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Command Type").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Bit Position").Width = 150;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Select").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Used").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "CellNo").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Widget").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Key").Width = 100;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Description").Width = 150;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Event Class").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Variation").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Event Variation").Width = 0;
                        break;

                    case SlaveTypes.SMSSLAVE:
                        ucdo.lvDOMap.Columns[1].Text = "Unit ID";
                        Utils.getColumnHeader(ucdo.lvDOMap, "DO No.").Width = 100;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Unit ID").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Reporting Index").Width = 150;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Data Type").Width = 150;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Command Type").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Bit Position").Width = 150;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Select").Width = 100;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Used").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "CellNo").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Widget").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Key").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Description").Width = 150;
                        break;
                    case SlaveTypes.DNP3SLAVE:
                        ucdo.lvDOMap.Columns[1].Text = "Unit ID";
                        Utils.getColumnHeader(ucdo.lvDOMap, "DO No.").Width = 100;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Unit ID").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Reporting Index").Width = 150;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Data Type").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Command Type").Width = 100;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Bit Position").Width = 150;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Select").Width = 90;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Used").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "CellNo").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Widget").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Key").Width = 0;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Description").Width = 150;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Event Class").Width = 90;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Variation").Width = 90;
                        Utils.getColumnHeader(ucdo.lvDOMap, "Event Variation").Width = 90;
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
            rootNode = xmlDoc.CreateElement("DOConfiguration");
            xmlDoc.AppendChild(rootNode);
            foreach (DO don in doList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(don.exportXMLnode(), true);
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
            rootNode = xmlDoc.CreateElement("DOMap");
            xmlDoc.AppendChild(rootNode);

            List<DOMap> slaveDOMapList;
            if (!slavesDOMapList.TryGetValue(slaveID, out slaveDOMapList))
            {
                Console.WriteLine("##### Slave entries for {0} does not exists", slaveID);
                return rootNode;
            }
            foreach (DOMap domn in slaveDOMapList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(domn.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }
            return rootNode;
        }
        public string exportINI(string slaveNum, string slaveID, string element, ref int ctr)
        {
            string iniData = "";
            List<DOMap> slaveDOMapList;
            if (!slavesDOMapList.TryGetValue(slaveID, out slaveDOMapList))
            {
                Console.WriteLine("DO INI: ##### Slave entries for {0} does not exists", slaveID);
                return iniData;
            }
            //IMP: If "DoubleCommand", create only single IOA in .INI file...
            Dictionary<string, string> riList = new Dictionary<string, string>();
            foreach (DOMap domn in slaveDOMapList)
            {
                int ri;
                try
                {
                    ri = Int32.Parse(domn.ReportingIndex);
                }
                catch (System.FormatException)
                {
                    ri = 0;
                }

                if (domn.DataType == "DoubleCommand" && IsRIinINI(riList, domn.ReportingIndex)) continue;
                if (!riList.ContainsKey(domn.ReportingIndex)) //Ajay: 10/01/2019
                {
                    iniData += "DO_" + ctr++ + "=" + Utils.GenerateIndex("DO", Utils.GetDataTypeIndex(domn.DataType), ri).ToString() + Environment.NewLine;
                    riList.Add(domn.ReportingIndex, domn.DataType);
                }
                //Ajay: 10/01/2019
                else
                {
                    MessageBox.Show("Duplicate Reporting Index (" + domn.ReportingIndex + ") found of DO No " + domn.DONo, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                //iniData += "DO_" + ctr++ + "=" + Utils.GenerateIndex("DO", Utils.GetDataTypeIndex(domn.DataType), ri).ToString() + Environment.NewLine;
            }
            return iniData;
        }
        private bool IsRIinINI(Dictionary<string, string> ril, string ri)
        {
            string tmp;
            if (!ril.TryGetValue(ri, out tmp)) return false;
            else if (tmp != "DoubleCommand")
            {//Got ri, check if DoubleCommand
                return false;
            }
            else return true;
        }
        public void regenerateDOSequence()
        {
            foreach (DO don in doList)
            {
                int oDONo = Int32.Parse(don.DONo);
                //Namrata: 30/10/2017
                //int nDONo = oDONo;
                int nDONo = Globals.DONo++;
                don.DONo = nDONo.ToString();

                //Now change in map...
                foreach (KeyValuePair<string, List<DOMap>> maps in slavesDOMapList)
                {
                    //Ajay: 10/01/2019 Commented
                    //List<DOMap> sdomList = maps.Value;
                    //foreach (DOMap dom in sdomList)
                    //{
                    //    if (dom.DONo == oDONo.ToString() && !dom.IsReindexed)
                    //    {
                    //        //Ajay: 30/07/2018 if same DO mapped again it should take same DO no on reindex. 
                    //        //dom.DONo = nDONo.ToString();
                    //        //dom.IsReindexed = true;
                    //        sdomList.Where(x => x.DONo == oDONo.ToString()).ToList().ForEach(x => { x.DONo = nDONo.ToString(); x.IsReindexed = true; });
                    //        break;
                    //    }
                    //}

                    //Ajay: 10/01/2019 Reindexing issues reported by Aditya K. mail dtd. 27-12-2018
                    maps.Value.OfType<DOMap>().Where(x => x.DONo == oDONo.ToString() && !x.IsReindexed).ToList().ForEach(x => { x.DONo = nDONo.ToString(); x.IsReindexed = true; });
                }

                //Now change in Parameter Load nodes...
                Utils.getOpenProPlusHandle().getParameterLoadConfiguration().ChangeDOSequence(oDONo, nDONo);
            }
            //Reset reindexing status, for next use...
            foreach (KeyValuePair<string, List<DOMap>> maps in slavesDOMapList)
            {
                List<DOMap> sdomList = maps.Value;
                foreach (DOMap dom in sdomList)
                {
                    dom.IsReindexed = false;
                }
            }
            refreshList();
            refreshCurrentMapList();
        }
        public int GetReportingIndex(string slaveNum, string slaveID, int value)
        {
            int ret = 0;
            List<DOMap> slaveDOMapList;
            if (!slavesDOMapList.TryGetValue(slaveID, out slaveDOMapList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                return ret;
            }
            foreach (DOMap dom in slaveDOMapList)
            {
                if (dom.DONo == value.ToString()) return Int32.Parse(dom.ReportingIndex);
            }
            return ret;
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("DO_"))
            {
                //If a IEC104 slave added/deleted, reflect in UI as well as objects.
                CheckIEC104SlaveStatusChanges();
                //If a MODBUS slave added/deleted, reflect in UI as well as objects.
                CheckMODBUSSlaveStatusChanges();
                CheckIEC61850SlaveStatusChanges();
                //Namrata:13/7/2017
                CheckIEC101SlaveStatusChanges();
                //Ajay:09/07/2018
                CheckSPORTSlaveStatusChanges();
                CheckMQTTSlaveStatusChanges();
                CheckSMSSlaveStatusChanges();
                ShowHideSlaveColumns();
                //ShowHideSlaveColumnsSPORT();
                //ShowHideMODBUSSlaveColumns();
                //ShowHideIEC61850ServerSlaveColumns();
                CheckGDisplaySlaveStatusChanges();
                CheckDNPSlaveStatusChanges();
                //Namrata:21/7/2017
                //ShowHideMODBUSSlaveColumns();
                return ucdo;
            }
            return null;
        }
        public void parseDOCNode(XmlNode docNode, bool imported)
        {
            if (docNode == null)
            {
                rnName = "DOConfiguration";
                return;
            }
            //First set root node name...
            rnName = docNode.Name;
            if (docNode.NodeType == XmlNodeType.Comment)
            {
                isNodeComment = true;
                comment = docNode.Value;
            }
            foreach (XmlNode node in docNode)
            {
                if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                doList.Add(new DO(node, masterType, masterNo, IEDNo, imported));
            }
            for (int i = 0; i < doList.Count; i++)
            {
                if (doList[i].EnableDI == "")
                {
                    doList[i].EnableDI = "0";
                    ucdo.txtEnableDI.Text = "0";
                }
            }

            refreshList();
        }
        public void parseDOMNode(string slaveNum, string slaveID, XmlNode domNode)
        {
            //Task thDetails = new Task(() => CreateNewSlave(slaveNum, slaveID, domNode));
            //thDetails.Start();
            CreateNewSlave(slaveNum, slaveID, domNode);
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public int getCount()
        {
            int ctr = 0;
            foreach (DO doNode in doList)
            {
                if (doNode.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }
        public List<DO> getDOs()
        {
            return doList;
        }
        private void GetDataForGDisplaySlave()
        {
            string strRoutineName = "DOConfiguration:GetDataForGDisplaySlave";
            try
            {
                Utils.CurrentSlaveFinal = currentSlave;
                ucdo.CmbCellNo.DataSource = null;
                if (GDSlave.DsExcelData.Tables.Count > 0)
                {
                    GDSlave.ListImageDIDO();
                    ListToDataTable(GDSlave.ListDIDO);

                    List<string> tblNameList = GDSlave.DsExcelData.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                    string CurrentSlave = tblNameList.Where(x => x.Contains(Utils.CurrentSlaveFinal)).Select(x => x).FirstOrDefault();
                    GDSlave.CurrentSlave = CurrentSlave;
                    var CellNo = Utils.GetListFromDT_StringCol(GDSlave.DsExcelData.Tables[CurrentSlave], "CellNo", false);

                    //Namrata: 24/09/2019
                    List<string> DistinctDataSetCellNo = GDSlave.DsExcelData.Tables[CurrentSlave].Rows.OfType<DataRow>()
                                                   .Where(x => x.Field<string>("Widget").Contains("Blank") || x.Field<string>("Widget").Contains("SW_")).Select(x => x.Field<string>("CellNo")).Distinct().ToList();
                    List<string> DistinctDataSetWidgets = GDSlave.DsExcelData.Tables[CurrentSlave].Rows.OfType<DataRow>()
                                                    .Where(x => x.Field<string>("Widget").Contains("Blank") || x.Field<string>("Widget").Contains("SW_")).Select(x => x.Field<string>("Widget")).Distinct().ToList();
                    if (DistinctDataSetCellNo.Count > 0)
                    {
                        ucdo.CmbCellNo.DataSource = null;
                        ucdo.CmbCellNo.Items.Clear();
                        ucdo.CmbCellNo.DataSource = DistinctDataSetCellNo;
                        ucdo.CmbCellNo.SelectedIndex = 0;
                    }
                    if (DistinctDataSetWidgets.Count > 0)
                    {
                        ucdo.CmbWidget.DataSource = null;
                        ucdo.CmbWidget.Items.Clear();
                        ucdo.CmbWidget.DataSource = DistinctDataSetWidgets;
                        ucdo.CmbWidget.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Namrata:27/7/2017
        public int getDOMapCount()
        {
            int ctr = 0;
            fillMapOptions(Utils.getSlaveTypes(currentSlave));
            List<DOMap> sdomList;
            if (!slavesDOMapList.TryGetValue(currentSlave, out sdomList))
            {
                refreshMapList(null);
            }
            else
            {
                refreshMapList(sdomList);
            }
            if (sdomList == null)
            {
                return 0;
            }
            else
            {
                foreach (DOMap asaa in sdomList)
                {
                    if (asaa.IsNodeComment) continue;
                    ctr++;
                }
            }

            return ctr;
        }
        public List<DOMap> getSlaveDOMaps(string slaveID)
        {
            List<DOMap> slaveDOMapList;
            slavesDOMapList.TryGetValue(slaveID, out slaveDOMapList);
            return slaveDOMapList;
        }

        #region Events For All masters
        //Ajay: 31/07/2018 
        public void LoadProfile_OnLoad()
        {
            foreach (Control c in ucdo.grpDO.Controls)
            {
                if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                {
                    if (c.Name.Contains("lblHdrText")) { }
                    else { c.Visible = false; }
                }
                else { }
            }

            ucdo.lblDONo.Visible = ucdo.txtDONo.Visible = true;
            ucdo.lblDONo.Location = new Point(15, 35);
            ucdo.txtDONo.Location = new Point(102, 30);

            ucdo.lblDescription.Visible = ucdo.txtDescription.Visible = true;
            ucdo.lblDescription.Location = new Point(15, 60);
            ucdo.txtDescription.Location = new Point(102, 55);

            ucdo.btnDone.Visible = ucdo.btnCancel.Visible = true;
            ucdo.btnDone.Location = new Point(102, 90);
            ucdo.btnCancel.Location = new Point(190, 90);

            ucdo.grpDO.Height = 130;
            ucdo.grpDO.Width = 280;
            ucdo.grpDO.Location = new Point(172, 52);
        }
        //Ajay: 31/07/2018
        public void LoadProfile_OnDoubleClick()
        {
            string strRoutineName = "LoadProfileOnDoubleClick";
            try
            {
                foreach (Control c in ucdo.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                }

                ucdo.lblDONo.Visible = ucdo.txtDONo.Visible = true;
                ucdo.lblDONo.Location = new Point(15, 35);
                ucdo.txtDONo.Location = new Point(102, 30);

                ucdo.lblDescription.Visible = ucdo.txtDescription.Visible = true;
                ucdo.lblDescription.Location = new Point(15, 60);
                ucdo.txtDescription.Location = new Point(102, 55);

                ucdo.btnDone.Visible = ucdo.btnCancel.Visible = true;
                ucdo.btnDone.Location = new Point(102, 90);
                ucdo.btnCancel.Location = new Point(190, 90);

                ucdo.btnFirst.Visible = ucdo.btnPrev.Visible = ucdo.btnNext.Visible = ucdo.btnLast.Visible = true;
                ucdo.btnFirst.Location = new Point(10, 125);
                ucdo.btnPrev.Location = new Point(80, 125);
                ucdo.btnNext.Location = new Point(155, 125);
                ucdo.btnLast.Location = new Point(230, 125);

                ucdo.grpDO.Height = 155;
                ucdo.grpDO.Width = 280;
                ucdo.grpDO.Location = new Point(172, 52);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Virtual_MasterEventsOnLoad()
        {
            string strRoutineName = "DOConfiguration:Virtual_MasterEventsOnLoad";
            try
            {
                ucdo.ChkEvent.Visible = false; //Ajay: 18/07/2018
                ucdo.cmbCModel.Visible = false; //Ajay: 04/08/2018
                ucdo.lblCModel.Visible = false; //Ajay: 04/08/2018

                ucdo.btnAdd.Enabled = false;
                ucdo.btnDelete.Enabled = false;
                //Namrata: 12/09/2017
                ucdo.lblIEDName.Visible = false;
                ucdo.lblIEC61850ResponseType.Visible = false;
                ucdo.lblIEC61850Index.Visible = false;
                ucdo.cmbIEDName.Visible = false;
                ucdo.cmbIEC61850Index.Visible = false;
                ucdo.cmbIEC61850ResponseType.Visible = false;
                //Namrata: 12/09/2017
                ucdo.lblIEDName.Visible = false;
                ucdo.lblIEC61850ResponseType.Visible = false;
                ucdo.lblIEC61850Index.Visible = false;
                ucdo.cmbIEDName.Visible = false;
                ucdo.cmbIEC61850Index.Visible = false;
                ucdo.cmbIEC61850ResponseType.Visible = false;
                ucdo.lblAutoMap.Location = new Point(11, 211);
                ucdo.txtAutoMap.Location = new Point(121, 213);
                ucdo.btnDone.Location = new Point(102, 279);
                ucdo.btnCancel.Location = new Point(192, 279);
                ucdo.btnFirst.Location = new Point(10, 312);
                ucdo.btnPrev.Location = new Point(81, 312);
                ucdo.btnNext.Location = new Point(160, 312);
                ucdo.btnLast.Location = new Point(231, 312);
                ucdo.grpDO.Location = new Point(172, 52);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void ADR_IEC103_MODBUS_MasterEventsOnLoad()
        {
            string strRoutineName = "DOConfiguration:ADR_IEC103_MODBUS_MasterEventsOnLoad";
            try
            {
                foreach (Control c in ucdo.grpDO.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucdo.lblDONo.Visible = ucdo.txtDONo.Visible = true;
                ucdo.txtDONo.Enabled = false;
                ucdo.lblDONo.Location = new Point(15, 35);
                ucdo.txtDONo.Location = new Point(135, 30);

                ucdo.lblResponseType.Visible = ucdo.cmbResponseType.Visible = true;
                ucdo.lblResponseType.Location = new Point(15, 60);
                ucdo.cmbResponseType.Location = new Point(135, 55);

                ucdo.lblIndex.Visible = ucdo.txtIndex.Visible = true;
                ucdo.lblIndex.Location = new Point(15, 85);
                ucdo.txtIndex.Location = new Point(135, 80);

                ucdo.lblSubIndex.Visible = ucdo.txtSubIndex.Visible = true;
                ucdo.lblSubIndex.Location = new Point(15, 110);
                ucdo.txtSubIndex.Location = new Point(135, 105);

                ucdo.lblControlType.Visible = ucdo.cmbControlType.Visible = true;
                ucdo.lblControlType.Location = new Point(15, 135);
                ucdo.cmbControlType.Location = new Point(135, 130);

                ucdo.lblPulseDuration.Visible = ucdo.txtPulseDuration.Visible = true;
                ucdo.lblPulseDuration.Location = new Point(15, 160);
                ucdo.txtPulseDuration.Location = new Point(135, 155);

                ucdo.lblDescription.Visible = ucdo.txtDescription.Visible = true;
                ucdo.lblDescription.Location = new Point(15, 185);
                ucdo.txtDescription.Location = new Point(135, 180);

                ucdo.lblEnableDI.Visible = ucdo.txtEnableDI.Visible = true;
                ucdo.lblEnableDI.Location = new Point(15, 210);
                ucdo.txtEnableDI.Location = new Point(135, 205);

                ucdo.lblAutoMap.Visible = ucdo.txtAutoMap.Visible = true;
                ucdo.lblAutoMap.Location = new Point(15, 235);
                ucdo.txtAutoMap.Location = new Point(135, 230);

                ucdo.btnDone.Visible = ucdo.btnCancel.Visible = true;
                ucdo.btnDone.Location = new Point(75, 260);
                ucdo.btnCancel.Location = new Point(175, 260);

                ucdo.grpDO.Height = 300;
                ucdo.grpDO.Width = 320;
                ucdo.grpDO.Location = new Point(172, 52);

            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void IEC101_SPORT_MasterEventsOnLoad()
        {
            string strRoutineName = "DOConfiguration:IEC101_SPORT_MasterEventsOnLoad";
            try
            {
                foreach (Control c in ucdo.grpDO.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucdo.lblDONo.Visible = ucdo.txtDONo.Visible = true;
                ucdo.txtDONo.Enabled = false;
                ucdo.lblDONo.Location = new Point(15, 35);
                ucdo.txtDONo.Location = new Point(135, 30);

                ucdo.lblResponseType.Visible = ucdo.cmbResponseType.Visible = true;
                ucdo.lblResponseType.Location = new Point(15, 60);
                ucdo.cmbResponseType.Location = new Point(135, 55);

                ucdo.lblIndex.Visible = ucdo.txtIndex.Visible = true;
                ucdo.lblIndex.Location = new Point(15, 85);
                ucdo.txtIndex.Location = new Point(135, 80);

                ucdo.lblSubIndex.Visible = ucdo.txtSubIndex.Visible = true;
                ucdo.lblSubIndex.Location = new Point(15, 110);
                ucdo.txtSubIndex.Location = new Point(135, 105);

                ucdo.lblControlType.Visible = ucdo.cmbControlType.Visible = true;
                ucdo.lblControlType.Location = new Point(15, 135);
                ucdo.cmbControlType.Location = new Point(135, 130);

                ucdo.lblPulseDuration.Visible = ucdo.txtPulseDuration.Visible = true;
                ucdo.lblPulseDuration.Location = new Point(15, 160);
                ucdo.txtPulseDuration.Location = new Point(135, 155);

                ucdo.lblDescription.Visible = ucdo.txtDescription.Visible = true;
                ucdo.lblDescription.Location = new Point(15, 185);
                ucdo.txtDescription.Location = new Point(135, 180);

                ucdo.lblEnableDI.Visible = ucdo.txtEnableDI.Visible = true;
                ucdo.lblEnableDI.Location = new Point(15, 210);
                ucdo.txtEnableDI.Location = new Point(135, 205);

                ucdo.lblAutoMap.Visible = ucdo.txtAutoMap.Visible = true;
                ucdo.lblAutoMap.Location = new Point(15, 235);
                ucdo.txtAutoMap.Location = new Point(135, 230);

                ucdo.ChkEnableDisable.Visible = true;
                ucdo.ChkEnableDisable.Checked = false;
                ucdo.ChkEnableDisable.Location = new Point(15, 260);

                ucdo.btnDone.Visible = ucdo.btnCancel.Visible = true;
                ucdo.btnDone.Location = new Point(75, 290);
                ucdo.btnCancel.Location = new Point(175, 290);

                ucdo.grpDO.Height = 330;
                ucdo.grpDO.Width = 320;
                ucdo.grpDO.Location = new Point(172, 52);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        //Ajay: 18/07/2018 
        public void IEC104_MasterEventsOnLoad()
        {
            string strRoutineName = "DOConfiguration:IEC104_MasterEventsOnLoad";
            try
            {
                foreach (Control c in ucdo.grpDO.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucdo.lblDONo.Visible = ucdo.txtDONo.Visible = true;
                ucdo.txtDONo.Enabled = false;
                ucdo.lblDONo.Location = new Point(15, 35);
                ucdo.txtDONo.Location = new Point(135, 30);

                ucdo.lblResponseType.Visible = ucdo.cmbResponseType.Visible = true;
                ucdo.lblResponseType.Location = new Point(15, 60);
                ucdo.cmbResponseType.Location = new Point(135, 55);

                ucdo.lblIndex.Visible = ucdo.txtIndex.Visible = true;
                ucdo.lblIndex.Location = new Point(15, 85);
                ucdo.txtIndex.Location = new Point(135, 80);

                ucdo.lblSubIndex.Visible = ucdo.txtSubIndex.Visible = true;
                ucdo.lblSubIndex.Location = new Point(15, 110);
                ucdo.txtSubIndex.Location = new Point(135, 105);

                ucdo.lblControlType.Visible = ucdo.cmbControlType.Visible = true;
                ucdo.lblControlType.Location = new Point(15, 135);
                ucdo.cmbControlType.Location = new Point(135, 130);

                ucdo.lblPulseDuration.Visible = ucdo.txtPulseDuration.Visible = true;
                ucdo.lblPulseDuration.Location = new Point(15, 160);
                ucdo.txtPulseDuration.Location = new Point(135, 155);

                ucdo.lblDescription.Visible = ucdo.txtDescription.Visible = true;
                ucdo.lblDescription.Location = new Point(15, 185);
                ucdo.txtDescription.Location = new Point(135, 180);

                ucdo.lblEnableDI.Visible = ucdo.txtEnableDI.Visible = true;
                ucdo.lblEnableDI.Location = new Point(15, 210);
                ucdo.txtEnableDI.Location = new Point(135, 205);

                ucdo.lblAutoMap.Visible = ucdo.txtAutoMap.Visible = true;
                ucdo.lblAutoMap.Location = new Point(15, 235);
                ucdo.txtAutoMap.Location = new Point(135, 230);

                ucdo.ChkEnableDisable.Visible = true;
                ucdo.ChkEnableDisable.Checked = false;
                ucdo.ChkEnableDisable.Location = new Point(15, 260);

                //ucdo.ChkEvent.Visible = true;
                //ucdo.ChkEvent.Checked = false;
                //ucdo.ChkEvent.Location = new Point(102, 260);

                ucdo.btnDone.Visible = ucdo.btnCancel.Visible = true;
                ucdo.btnDone.Location = new Point(75, 290);
                ucdo.btnCancel.Location = new Point(175, 290);

                ucdo.grpDO.Height = 330;
                ucdo.grpDO.Width = 320;
                ucdo.grpDO.Location = new Point(172, 52);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 18/07/2018
        public void IEC61850Client_EventsOnLoad()
        {
            string strRoutineName = "DOConfiguration:IEC61850Client_EventsOnLoad";
            try
            {
                foreach (Control c in ucdo.grpDO.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucdo.lblDONo.Visible = ucdo.txtDONo.Visible = true;
                ucdo.txtDONo.Enabled = false;
                ucdo.lblDONo.Location = new Point(15, 35);
                ucdo.txtDONo.Location = new Point(135, 30);
                ucdo.txtDONo.Size = new Size(300, 21);

                ucdo.lblIEDName.Visible = ucdo.cmbIEDName.Visible = true;
                ucdo.lblIEDName.Location = new Point(15, 60);
                ucdo.cmbIEDName.Location = new Point(135, 55);
                ucdo.cmbIEDName.Size = new Size(300, 21);

                ucdo.lblIEC61850ResponseType.Visible = ucdo.cmbIEC61850ResponseType.Visible = true;
                ucdo.lblIEC61850ResponseType.Location = new Point(15, 85);
                ucdo.cmbIEC61850ResponseType.Location = new Point(135, 80);
                ucdo.cmbIEC61850ResponseType.Size = new Size(300, 21);

                ucdo.lblFC.Visible = ucdo.cmbFC.Visible = true;
                //ucdo.cmbFC.Enabled = false;
                ucdo.lblFC.Location = new Point(15, 110);
                ucdo.cmbFC.Location = new Point(135, 105);
                ucdo.cmbFC.Size = new Size(300, 21);

                ucdo.lblIEC61850Index.Visible = ucdo.cmbIEC61850Index.Visible = false;
                ucdo.lblIEC61850Index.Location = new Point(15, 135);
                ucdo.cmbIEC61850Index.Location = new Point(135, 130);
                ucdo.cmbIEC61850Index.Size = new Size(300, 21);

                //Namrata:27/03/2019
                ucdo.lblIEC61850Index.Visible = ucdo.ChkIEC61850Index.Visible = true;
                ucdo.lblIEC61850Index.Location = new Point(15, 135);
                ucdo.ChkIEC61850Index.Location = new Point(135, 130);
                ucdo.ChkIEC61850Index.Size = new Size(300, 21);

                ucdo.lblSubIndex.Visible = ucdo.txtSubIndex.Visible = true;
                ucdo.lblSubIndex.Location = new Point(15, 160);
                ucdo.txtSubIndex.Location = new Point(135, 155);
                ucdo.txtSubIndex.Size = new Size(300, 21);

                ucdo.lblControlType.Visible = ucdo.cmbControlType.Visible = true;
                ucdo.lblControlType.Location = new Point(15, 185);
                ucdo.cmbControlType.Location = new Point(135, 180);
                ucdo.cmbControlType.Size = new Size(300, 21);

                ucdo.lblPulseDuration.Visible = ucdo.txtPulseDuration.Visible = true;
                ucdo.lblPulseDuration.Location = new Point(15, 210);
                ucdo.txtPulseDuration.Location = new Point(135, 205);
                ucdo.txtPulseDuration.Size = new Size(300, 21);

                ucdo.lblDescription.Visible = ucdo.txtDescription.Visible = true;
                ucdo.lblDescription.Location = new Point(15, 235);
                ucdo.txtDescription.Location = new Point(135, 230);
                ucdo.txtDescription.Size = new Size(300, 21);

                ucdo.lblEnableDI.Visible = ucdo.txtEnableDI.Visible = true;
                ucdo.lblEnableDI.Location = new Point(15, 260);
                ucdo.txtEnableDI.Location = new Point(135, 255);
                ucdo.txtEnableDI.Size = new Size(300, 21);

                ucdo.lblAutoMap.Visible = ucdo.txtAutoMap.Visible = false;
                ucdo.lblAutoMap.Location = new Point(15, 285);
                ucdo.txtAutoMap.Location = new Point(135, 280);
                ucdo.txtAutoMap.Size = new Size(300, 21);

                ucdo.lblCModel.Visible = ucdo.cmbCModel.Visible = true;
                ucdo.lblCModel.Location = new Point(15, 285);
                ucdo.cmbCModel.Location = new Point(135, 280);
                ucdo.cmbCModel.Size = new Size(300, 21);

                ucdo.btnDone.Visible = ucdo.btnCancel.Visible = true;
                ucdo.btnDone.Location = new Point(165, 310);
                ucdo.btnCancel.Location = new Point(250, 310);

                ucdo.grpDO.Height = 350;
                ucdo.grpDO.Width = 455;
                ucdo.grpDO.Location = new Point(172, 52);
                ucdo.pbHdr.Width = 510;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void IEC61850_OnDoubleClick()
        {
            string strRoutineName = "IEC61850_OnDoubleClick";
            try
            {
                foreach (Control c in ucdo.grpDO.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucdo.lblDONo.Visible = ucdo.txtDONo.Visible = true;
                ucdo.txtDONo.Enabled = false;
                ucdo.lblDONo.Location = new Point(15, 35);
                ucdo.txtDONo.Location = new Point(135, 30);
                ucdo.txtDONo.Size = new Size(300, 21);

                ucdo.lblIEDName.Visible = ucdo.cmbIEDName.Visible = true;
                ucdo.lblIEDName.Location = new Point(15, 60);
                ucdo.cmbIEDName.Location = new Point(135, 55);
                ucdo.cmbIEDName.Size = new Size(300, 21);

                ucdo.lblIEC61850ResponseType.Visible = ucdo.cmbIEC61850ResponseType.Visible = true;
                ucdo.lblIEC61850ResponseType.Location = new Point(15, 85);
                ucdo.cmbIEC61850ResponseType.Location = new Point(135, 80);
                ucdo.cmbIEC61850ResponseType.Size = new Size(300, 21);

                ucdo.lblFC.Visible = ucdo.cmbFC.Visible = true;
                //ucdo.cmbFC.Enabled = false;
                ucdo.lblFC.Location = new Point(15, 110);
                ucdo.cmbFC.Location = new Point(135, 105);
                ucdo.cmbFC.Size = new Size(300, 21);

                ucdo.lblIEC61850Index.Visible = ucdo.cmbIEC61850Index.Visible = false;
                ucdo.lblIEC61850Index.Location = new Point(15, 135);
                ucdo.cmbIEC61850Index.Location = new Point(135, 130);
                ucdo.cmbIEC61850Index.Size = new Size(300, 21);

                ucdo.lblIEC61850Index.Visible = ucdo.ChkIEC61850Index.Visible = true;
                ucdo.lblIEC61850Index.Location = new Point(15, 135);
                ucdo.ChkIEC61850Index.Location = new Point(135, 130);
                ucdo.ChkIEC61850Index.Size = new Size(300, 21);

                ucdo.lblSubIndex.Visible = ucdo.txtSubIndex.Visible = true;
                ucdo.lblSubIndex.Location = new Point(15, 160);
                ucdo.txtSubIndex.Location = new Point(135, 155);
                ucdo.txtSubIndex.Size = new Size(300, 21);

                ucdo.lblControlType.Visible = ucdo.cmbControlType.Visible = true;
                ucdo.lblControlType.Location = new Point(15, 185);
                ucdo.cmbControlType.Location = new Point(135, 180);
                ucdo.cmbControlType.Size = new Size(300, 21);

                ucdo.lblPulseDuration.Visible = ucdo.txtPulseDuration.Visible = true;
                ucdo.lblPulseDuration.Location = new Point(15, 210);
                ucdo.txtPulseDuration.Location = new Point(135, 205);
                ucdo.txtPulseDuration.Size = new Size(300, 21);

                ucdo.lblDescription.Visible = ucdo.txtDescription.Visible = true;
                ucdo.lblDescription.Location = new Point(15, 235);
                ucdo.txtDescription.Location = new Point(135, 230);
                ucdo.txtDescription.Size = new Size(300, 21);

                ucdo.lblEnableDI.Visible = ucdo.txtEnableDI.Visible = true;
                ucdo.lblEnableDI.Location = new Point(15, 260);
                ucdo.txtEnableDI.Location = new Point(135, 255);
                ucdo.txtEnableDI.Size = new Size(300, 21);

                ucdo.lblCModel.Visible = ucdo.cmbCModel.Visible = true;
                ucdo.lblCModel.Location = new Point(15, 285);
                ucdo.cmbCModel.Location = new Point(135, 280);
                ucdo.cmbCModel.Size = new Size(300, 21);

                ucdo.btnDone.Visible = ucdo.btnCancel.Visible = true;
                ucdo.btnDone.Location = new Point(165, 310);
                ucdo.btnCancel.Location = new Point(265, 310);

                ucdo.btnFirst.Visible = ucdo.btnPrev.Visible = ucdo.btnNext.Visible = ucdo.btnLast.Visible = true;

                ucdo.btnFirst.Location = new Point(110, 345);
                ucdo.btnPrev.Location = new Point(180, 345);
                ucdo.btnNext.Location = new Point(250, 345);
                ucdo.btnLast.Location = new Point(320, 345);

                ucdo.grpDO.Height = 375;
                ucdo.grpDO.Width = 455;
                ucdo.grpDO.Location = new Point(172, 52);
                ucdo.pbHdr.Width = 510;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void IEC104_OnDoubleClick()
        {
            string strRoutineName = "IEC104_OnDoubleClick";
            try
            {
                foreach (Control c in ucdo.grpDO.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucdo.lblDONo.Visible = ucdo.txtDONo.Visible = true;
                ucdo.txtDONo.Enabled = false;
                ucdo.lblDONo.Location = new Point(15, 35);
                ucdo.txtDONo.Location = new Point(135, 30);

                ucdo.lblResponseType.Visible = ucdo.cmbResponseType.Visible = true;
                ucdo.lblResponseType.Location = new Point(15, 60);
                ucdo.cmbResponseType.Location = new Point(135, 55);

                ucdo.lblIndex.Visible = ucdo.txtIndex.Visible = true;
                ucdo.lblIndex.Location = new Point(15, 85);
                ucdo.txtIndex.Location = new Point(135, 80);

                ucdo.lblSubIndex.Visible = ucdo.txtSubIndex.Visible = true;
                ucdo.lblSubIndex.Location = new Point(15, 110);
                ucdo.txtSubIndex.Location = new Point(135, 105);

                ucdo.lblControlType.Visible = ucdo.cmbControlType.Visible = true;
                ucdo.lblControlType.Location = new Point(15, 135);
                ucdo.cmbControlType.Location = new Point(135, 130);

                ucdo.lblPulseDuration.Visible = ucdo.txtPulseDuration.Visible = true;
                ucdo.lblPulseDuration.Location = new Point(15, 160);
                ucdo.txtPulseDuration.Location = new Point(135, 155);

                ucdo.lblDescription.Visible = ucdo.txtDescription.Visible = true;
                ucdo.lblDescription.Location = new Point(15, 185);
                ucdo.txtDescription.Location = new Point(135, 180);

                ucdo.lblEnableDI.Visible = ucdo.txtEnableDI.Visible = true;
                ucdo.lblEnableDI.Location = new Point(15, 210);
                ucdo.txtEnableDI.Location = new Point(135, 205);

                //ucdo.lblAutoMap.Visible = ucdo.txtAutoMap.Visible = true;
                //ucdo.lblAutoMap.Location = new Point(15, 235);
                //ucdo.txtAutoMap.Location = new Point(135, 230);

                ucdo.ChkEnableDisable.Visible = true;
                ucdo.ChkEnableDisable.Checked = false;
                ucdo.ChkEnableDisable.Location = new Point(15, 235);

                ucdo.btnDone.Visible = ucdo.btnCancel.Visible = true;
                ucdo.btnDone.Location = new Point(75, 265);
                ucdo.btnCancel.Location = new Point(175, 265);

                ucdo.btnFirst.Visible = ucdo.btnPrev.Visible = ucdo.btnNext.Visible = ucdo.btnLast.Visible = true;
                ucdo.btnFirst.Location = new Point(13, 300);
                ucdo.btnPrev.Location = new Point(88, 300);
                ucdo.btnNext.Location = new Point(163, 300);
                ucdo.btnLast.Location = new Point(238, 300);

                ucdo.grpDO.Height = 335;
                ucdo.grpDO.Width = 320;
                ucdo.grpDO.Location = new Point(172, 52);

            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void IEC101_SPORT_OnDoubleClick()
        {
            string strRoutineName = "IEC101_SPORT_OnDoubleClick";
            try
            {
                foreach (Control c in ucdo.grpDO.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucdo.lblDONo.Visible = ucdo.txtDONo.Visible = true;
                ucdo.txtDONo.Enabled = false;
                ucdo.lblDONo.Location = new Point(15, 35);
                ucdo.txtDONo.Location = new Point(135, 30);

                ucdo.lblResponseType.Visible = ucdo.cmbResponseType.Visible = true;
                ucdo.lblResponseType.Location = new Point(15, 60);
                ucdo.cmbResponseType.Location = new Point(135, 55);

                ucdo.lblIndex.Visible = ucdo.txtIndex.Visible = true;
                ucdo.lblIndex.Location = new Point(15, 85);
                ucdo.txtIndex.Location = new Point(135, 80);

                ucdo.lblSubIndex.Visible = ucdo.txtSubIndex.Visible = true;
                ucdo.lblSubIndex.Location = new Point(15, 110);
                ucdo.txtSubIndex.Location = new Point(135, 105);

                ucdo.lblControlType.Visible = ucdo.cmbControlType.Visible = true;
                ucdo.lblControlType.Location = new Point(15, 135);
                ucdo.cmbControlType.Location = new Point(135, 130);

                ucdo.lblPulseDuration.Visible = ucdo.txtPulseDuration.Visible = true;
                ucdo.lblPulseDuration.Location = new Point(15, 160);
                ucdo.txtPulseDuration.Location = new Point(135, 155);

                ucdo.lblDescription.Visible = ucdo.txtDescription.Visible = true;
                ucdo.lblDescription.Location = new Point(15, 185);
                ucdo.txtDescription.Location = new Point(135, 180);

                ucdo.lblEnableDI.Visible = ucdo.txtEnableDI.Visible = true;
                ucdo.lblEnableDI.Location = new Point(15, 210);
                ucdo.txtEnableDI.Location = new Point(135, 205);

                //ucdo.lblAutoMap.Visible = ucdo.txtAutoMap.Visible = true;
                //ucdo.lblAutoMap.Location = new Point(15, 235);
                //ucdo.txtAutoMap.Location = new Point(135, 230);

                ucdo.ChkEnableDisable.Visible = true;
                ucdo.ChkEnableDisable.Checked = false;
                ucdo.ChkEnableDisable.Location = new Point(15, 235);

                ucdo.btnDone.Visible = ucdo.btnCancel.Visible = true;
                ucdo.btnDone.Location = new Point(75, 265);
                ucdo.btnCancel.Location = new Point(175, 265);

                ucdo.btnFirst.Visible = ucdo.btnPrev.Visible = ucdo.btnNext.Visible = ucdo.btnLast.Visible = true;
                ucdo.btnFirst.Location = new Point(13, 300);
                ucdo.btnPrev.Location = new Point(88, 300);
                ucdo.btnNext.Location = new Point(163, 300);
                ucdo.btnLast.Location = new Point(238, 300);

                ucdo.grpDO.Height = 335;
                ucdo.grpDO.Width = 320;
                ucdo.grpDO.Location = new Point(172, 52);

            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ADR_IEC103_Modbus_OnDoubleClick()
        {
            string strRoutineName = "ADR_IEC103_Modbus_OnDoubleClick";
            try
            {
                foreach (Control c in ucdo.grpDO.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucdo.lblDONo.Visible = ucdo.txtDONo.Visible = true;
                ucdo.txtDONo.Enabled = false;
                ucdo.lblDONo.Location = new Point(15, 35);
                ucdo.txtDONo.Location = new Point(135, 30);

                ucdo.lblResponseType.Visible = ucdo.cmbResponseType.Visible = true;
                ucdo.lblResponseType.Location = new Point(15, 60);
                ucdo.cmbResponseType.Location = new Point(135, 55);

                ucdo.lblIndex.Visible = ucdo.txtIndex.Visible = true;
                ucdo.lblIndex.Location = new Point(15, 85);
                ucdo.txtIndex.Location = new Point(135, 80);

                ucdo.lblSubIndex.Visible = ucdo.txtSubIndex.Visible = true;
                ucdo.lblSubIndex.Location = new Point(15, 110);
                ucdo.txtSubIndex.Location = new Point(135, 105);

                ucdo.lblControlType.Visible = ucdo.cmbControlType.Visible = true;
                ucdo.lblControlType.Location = new Point(15, 135);
                ucdo.cmbControlType.Location = new Point(135, 130);

                ucdo.lblPulseDuration.Visible = ucdo.txtPulseDuration.Visible = true;
                ucdo.lblPulseDuration.Location = new Point(15, 160);
                ucdo.txtPulseDuration.Location = new Point(135, 155);

                ucdo.lblDescription.Visible = ucdo.txtDescription.Visible = true;
                ucdo.lblDescription.Location = new Point(15, 185);
                ucdo.txtDescription.Location = new Point(135, 180);

                ucdo.lblEnableDI.Visible = ucdo.txtEnableDI.Visible = true;
                ucdo.lblEnableDI.Location = new Point(15, 210);
                ucdo.txtEnableDI.Location = new Point(135, 205);

                ucdo.btnDone.Visible = ucdo.btnCancel.Visible = true;
                ucdo.btnDone.Location = new Point(75, 235);
                ucdo.btnCancel.Location = new Point(175, 235);

                ucdo.btnFirst.Visible = ucdo.btnPrev.Visible = ucdo.btnNext.Visible = ucdo.btnLast.Visible = true;
                ucdo.btnFirst.Location = new Point(13, 270);
                ucdo.btnPrev.Location = new Point(88, 270);
                ucdo.btnNext.Location = new Point(163, 270);
                ucdo.btnLast.Location = new Point(238, 270);

                ucdo.grpDO.Height = 305;
                ucdo.grpDO.Width = 320;
                ucdo.grpDO.Location = new Point(172, 52);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion Events For All masters

        #region Events For All Slaves
        public void MQTTSlave_OnDoubleClick()
        {
            #region CellNo
            ucdo.CmbCellNo.Visible = false;
            ucdo.lblCellNo.Visible = false;
            #endregion CellNo

            #region Widget
            ucdo.CmbWidget.Visible = false;
            ucdo.lblWidget.Visible = false;
            #endregion Widget
            ucdo.lblUnitID.Visible = false;
            ucdo.txtUnitID.Visible = false;
            ucdo.chkUsed.Visible = false;

            ucdo.pbDOMHdr.Size = new Size(360, 22);
            ucdo.grpDOMap.Size = new Size(360, 230);
            ucdo.grpDOMap.Location = new Point(243, 307);

            ucdo.lblDNM.Location = new Point(12, 33);
            ucdo.txtDOMNo.Location = new Point(112, 30);
            ucdo.txtDOMNo.Size = new Size(220, 20);


            ucdo.lblDMRI.Location = new Point(12, 58);
            ucdo.txtDOMReportingIndex.Location = new Point(112, 55);
            ucdo.lblDMRI.Visible = true;
            ucdo.txtDOMReportingIndex.Visible = true;
            ucdo.txtDOMReportingIndex.Size = new Size(220, 20);


            ucdo.lblDMDT.Location = new Point(12, 83);
            ucdo.cmbDOMDataType.Location = new Point(112, 80);
            ucdo.lblDMDT.Visible = true;
            ucdo.cmbDOMDataType.Visible = true;
            ucdo.cmbDOMDataType.Size = new Size(220, 20);

            ucdo.lbl61850reportingindex.Visible = false;
            ucdo.CmbReportingIndex.Visible = false;

            ucdo.lblDMCT.Visible = false;
            ucdo.cmbDOMCommandType.Visible = false;

            ucdo.lblDMBP.Location = new Point(12, 109);
            ucdo.txtDOMBitPos.Location = new Point(112, 106);
            ucdo.txtDOMBitPos.Size = new Size(220, 20);

            ucdo.lblKey.Visible = true;
            ucdo.lblKey.Location = new Point(12, 135);
            ucdo.txtKey.Location = new Point(112, 132);
            ucdo.txtKey.Size = new Size(220, 20);
            ucdo.txtKey.Visible = true;

            //description
            ucdo.lblmapdesc.Visible = true;
            ucdo.lblmapdesc.Location = new Point(12, 157);
            ucdo.txtMapDescription.Location = new Point(112, 158);
            ucdo.txtMapDescription.Size = new Size(220, 20);
            ucdo.txtMapDescription.Visible = true;
            ucdo.ChkIEC61850MapIndex.Visible = false;

            ucdo.lblAutoMapMapping.Visible = false;
            ucdo.txtDOAutoMap.Visible = false;
            ucdo.lblDMBP.Visible = true; ucdo.txtDOMBitPos.Visible = true;
            ucdo.chkDOMSelect.Location = new Point(112, 190);
            ucdo.chkUsed.Visible = false;
            ucdo.chkDOMSelect.Visible = false;

            ucdo.btnDOMDone.Location = new Point(112, 190);
            ucdo.btnDOMCancel.Location = new Point(200, 190);

        }
        public void SMSSLaveMap_OnLoadEvents()
        {
            #region CellNo
            ucdo.CmbCellNo.Visible = false;
            ucdo.lblCellNo.Visible = false;
            #endregion CellNo

            #region Widget
            ucdo.CmbWidget.Visible = false;
            ucdo.lblWidget.Visible = false;
            #endregion Widget
            #region Visible False
            //UnitID
            ucdo.lblUnitID.Visible = false;
            ucdo.txtUnitID.Visible = false;
            //chkUsed
            ucdo.chkUsed.Visible = false;

            //61850reportingindex
            ucdo.lbl61850reportingindex.Visible = false;
            ucdo.CmbReportingIndex.Visible = false;

            //CommandType
            ucdo.lblDMCT.Visible = false;
            ucdo.cmbDOMCommandType.Visible = false;

            #endregion Visible False

            #region Visible True
            //DOMNo
            ucdo.lblDNM.Location = new Point(12, 33);
            ucdo.txtDOMNo.Location = new Point(112, 30);
            ucdo.txtDOMNo.Size = new Size(220, 20);

            //DataType
            ucdo.lblDMDT.Location = new Point(12, 83);
            ucdo.cmbDOMDataType.Location = new Point(112, 80);
            ucdo.lblDMDT.Visible = true;
            ucdo.cmbDOMDataType.Visible = true;
            ucdo.cmbDOMDataType.Size = new Size(220, 20);

            //ReportingIndex
            ucdo.lblDMRI.Location = new Point(12, 58);
            ucdo.txtDOMReportingIndex.Location = new Point(112, 55);
            ucdo.lblDMRI.Visible = true;
            ucdo.txtDOMReportingIndex.Visible = true;
            ucdo.txtDOMReportingIndex.Size = new Size(220, 20);

            //BitPos
            ucdo.lblDMBP.Location = new Point(12, 109);
            ucdo.txtDOMBitPos.Location = new Point(112, 106);
            ucdo.txtDOMBitPos.Size = new Size(220, 20);
            ucdo.lblDMBP.Visible = true;
            ucdo.txtDOMBitPos.Visible = true;

            ucdo.lblKey.Visible = false;
            ucdo.txtKey.Visible = false;

            ucdo.lblmapdesc.Visible = true;
            ucdo.lblmapdesc.Location = new Point(12, 136);
            ucdo.txtMapDescription.Location = new Point(112, 132);
            ucdo.txtMapDescription.Size = new Size(220, 20);
            ucdo.txtMapDescription.Visible = true;


            ucdo.lblAutoMapMapping.Visible = true;
            ucdo.txtDOAutoMap.Visible = true;
            ucdo.lblAutoMapMapping.Location = new Point(12, 157);
            ucdo.txtDOAutoMap.Location = new Point(112, 158);
            ucdo.txtDOAutoMap.Size = new Size(220, 20);
            ucdo.txtDOAutoMap.Text = "1";
            //Select
            ucdo.chkDOMSelect.Visible = true;
            ucdo.chkDOMSelect.Location = new Point(112, 185);
            //BtnDone & BtnCancel
            ucdo.btnDOMDone.Location = new Point(112, 210);
            ucdo.btnDOMCancel.Location = new Point(200, 210);

            //grpDOMap
            ucdo.pbDOMHdr.Size = new Size(360, 22);
            ucdo.grpDOMap.Size = new Size(360, 250);
            ucdo.grpDOMap.Location = new Point(243, 307);
            #endregion Visible True
            ucdo.ChkIEC61850MapIndex.Visible = false;
        }

        public void SMSSLaveMap_OnDoubleClickEvents()
        {
            #region CellNo
            ucdo.CmbCellNo.Visible = false;
            ucdo.lblCellNo.Visible = false;
            #endregion CellNo

            #region Widget
            ucdo.CmbWidget.Visible = false;
            ucdo.lblWidget.Visible = false;
            #endregion Widget
            #region Visible False
            //UnitID
            ucdo.lblUnitID.Visible = false;
            ucdo.txtUnitID.Visible = false;
            //chkUsed
            ucdo.chkUsed.Visible = false;

            //61850reportingindex
            ucdo.lbl61850reportingindex.Visible = false;
            ucdo.CmbReportingIndex.Visible = false;

            //CommandType
            ucdo.lblDMCT.Visible = false;
            ucdo.cmbDOMCommandType.Visible = false;

            #endregion Visible False

            #region Visible True

            //DOMNo
            ucdo.lblDNM.Location = new Point(12, 33);
            ucdo.txtDOMNo.Location = new Point(112, 30);
            ucdo.txtDOMNo.Size = new Size(220, 20);

            //DataType
            ucdo.lblDMDT.Location = new Point(12, 83);
            ucdo.cmbDOMDataType.Location = new Point(112, 80);
            ucdo.lblDMDT.Visible = true;
            ucdo.cmbDOMDataType.Visible = true;
            ucdo.cmbDOMDataType.Size = new Size(220, 20);

            //ReportingIndex
            ucdo.lblDMRI.Location = new Point(12, 58);
            ucdo.txtDOMReportingIndex.Location = new Point(112, 55);
            ucdo.lblDMRI.Visible = true;
            ucdo.txtDOMReportingIndex.Visible = true;
            ucdo.txtDOMReportingIndex.Size = new Size(220, 20);

            //BitPos
            ucdo.lblDMBP.Location = new Point(12, 109);
            ucdo.txtDOMBitPos.Location = new Point(112, 106);
            ucdo.txtDOMBitPos.Size = new Size(220, 20);


            ucdo.lblKey.Visible = false;
            ucdo.txtKey.Visible = false;

            ucdo.lblmapdesc.Visible = true;
            ucdo.lblmapdesc.Location = new Point(12, 135);
            ucdo.txtMapDescription.Location = new Point(112, 132);
            ucdo.txtMapDescription.Size = new Size(220, 20);
            ucdo.txtMapDescription.Visible = true;


            ucdo.lblAutoMapMapping.Visible = false;
            ucdo.txtDOAutoMap.Visible = false;


            //Select
            ucdo.chkDOMSelect.Visible = true;
            ucdo.chkDOMSelect.Location = new Point(112, 160);

            //BtnDone & BtnCancel
            ucdo.btnDOMDone.Location = new Point(112, 190);
            ucdo.btnDOMCancel.Location = new Point(200, 190);

            //grpDOMap
            ucdo.pbDOMHdr.Size = new Size(360, 22);
            ucdo.grpDOMap.Size = new Size(360, 240);
            ucdo.grpDOMap.Location = new Point(243, 307);
            #endregion Visible True
            ucdo.ChkIEC61850MapIndex.Visible = false; ucdo.lblDMBP.Visible = true;
            ucdo.txtDOMBitPos.Visible = true;
        }
        public void MQTTSlaveEvents()
        {
            #region Visible False

            #region CellNo
            ucdo.CmbCellNo.Visible = false;
            ucdo.lblCellNo.Visible = false;
            #endregion CellNo

            #region Widget
            ucdo.CmbWidget.Visible = false;
            ucdo.lblWidget.Visible = false;
            #endregion Widget
            //UnitID
            ucdo.lblUnitID.Visible = false;
            ucdo.txtUnitID.Visible = false;
            //chkUsed
            ucdo.chkUsed.Visible = false;

            //61850reportingindex
            ucdo.lbl61850reportingindex.Visible = false;
            ucdo.CmbReportingIndex.Visible = false;

            //CommandType
            ucdo.lblDMCT.Visible = false;
            ucdo.cmbDOMCommandType.Visible = false;

            #endregion Visible False

            #region Visible True

            //DOMNo
            ucdo.lblDNM.Location = new Point(12, 33);
            ucdo.txtDOMNo.Location = new Point(112, 30);
            ucdo.txtDOMNo.Size = new Size(220, 20);

            //DataType
            ucdo.lblDMDT.Location = new Point(12, 83);
            ucdo.cmbDOMDataType.Location = new Point(112, 80);
            ucdo.lblDMDT.Visible = true;
            ucdo.cmbDOMDataType.Visible = true;
            ucdo.cmbDOMDataType.Size = new Size(220, 20);

            //ReportingIndex
            ucdo.lblDMRI.Location = new Point(12, 58);
            ucdo.txtDOMReportingIndex.Location = new Point(112, 55);
            ucdo.lblDMRI.Visible = true;
            ucdo.txtDOMReportingIndex.Visible = true;
            ucdo.txtDOMReportingIndex.Size = new Size(220, 20);

            //BitPos
            ucdo.lblDMBP.Visible = true; ucdo.txtDOMBitPos.Visible = true;
            ucdo.lblDMBP.Location = new Point(12, 109);
            ucdo.txtDOMBitPos.Location = new Point(112, 106);
            ucdo.txtDOMBitPos.Size = new Size(220, 20);


            ucdo.lblKey.Visible = true;
            ucdo.lblKey.Location = new Point(12, 135);
            ucdo.txtKey.Location = new Point(112, 132);
            ucdo.txtKey.Size = new Size(220, 20);
            ucdo.txtKey.Visible = true;

            //description

            ucdo.lblmapdesc.Visible = true;
            ucdo.lblmapdesc.Location = new Point(12, 157);
            ucdo.txtMapDescription.Location = new Point(112, 158);
            ucdo.txtMapDescription.Size = new Size(220, 20);
            ucdo.txtMapDescription.Visible = true;

            //Automap
            ucdo.lblAutoMapMapping.Location = new Point(12, 182);
            ucdo.txtDOAutoMap.Location = new Point(112, 183); ucdo.txtDOAutoMap.Text = "1";
            ucdo.txtDOAutoMap.Size = new Size(220, 20);
            ucdo.lblAutoMapMapping.Visible = true;
            ucdo.txtDOAutoMap.Visible = true;

            //Select
            ucdo.chkDOMSelect.Visible = false;
            ucdo.chkDOMSelect.Location = new Point(112, 210);

            //BtnDone & BtnCancel
            ucdo.btnDOMDone.Location = new Point(112, 215);
            ucdo.btnDOMCancel.Location = new Point(200, 215);

            //grpDOMap
            ucdo.pbDOMHdr.Size = new Size(360, 22);
            ucdo.grpDOMap.Size = new Size(360, 250);
            ucdo.grpDOMap.Location = new Point(243, 307);
            #endregion Visible True
            ucdo.ChkIEC61850MapIndex.Visible = false;
        }
        public void IEC61850Server_OnLoad()
        {
            #region Visible False
            ucdo.lblKey.Visible = false;
            ucdo.txtKey.Visible = false;

            //Namrata: 20/08/2019
            #region CellNo
            ucdo.CmbCellNo.Visible = false;
            ucdo.lblCellNo.Visible = false;
            #endregion CellNo

            #region Widget
            ucdo.CmbWidget.Visible = false;
            ucdo.lblWidget.Visible = false;
            #endregion Widget
            //ReportingIndex
            ucdo.txtDOMReportingIndex.Visible = false;

            //Select
            ucdo.chkDOMSelect.Visible = false;

            //UnitID
            ucdo.lblUnitID.Visible = false;
            ucdo.txtUnitID.Visible = false;
            #endregion Visible False

            #region Visible True

            //DOMNo
            ucdo.lblDNM.Location = new Point(12, 30);
            ucdo.txtDOMNo.Location = new Point(122, 30);
            ucdo.txtDOMNo.Size = new Size(300, 21);
            ucdo.lblDMRI.Visible = false;

            //ReportingIndex
            ucdo.lbl61850reportingindex.Visible = true;
            ucdo.CmbReportingIndex.Visible = false;
            ucdo.lbl61850reportingindex.Location = new Point(12, 55);
            ucdo.ChkIEC61850MapIndex.Visible = true;//Namrata:16/04/2019
            ucdo.ChkIEC61850MapIndex.Location = new Point(122, 55);
            ucdo.ChkIEC61850MapIndex.Size = new Size(300, 21);

            //DataType
            ucdo.lblDMDT.Location = new Point(12, 82);
            ucdo.cmbDOMDataType.Location = new Point(122, 82);
            ucdo.cmbDOMDataType.Size = new Size(300, 21);

            //CommandType
            ucdo.lblDMCT.Visible = false; ucdo.cmbDOMCommandType.Visible = false;
            ucdo.lblDMCT.Location = new Point(12, 108);
            ucdo.cmbDOMCommandType.Location = new Point(122, 108);
            ucdo.cmbDOMCommandType.Size = new Size(300, 21);

            //BitPos
            ucdo.lblDMBP.Location = new Point(12, 108);
            ucdo.txtDOMBitPos.Location = new Point(122, 108);
            ucdo.txtDOMBitPos.Size = new Size(300, 21);


            //Description
            ucdo.lblmapdesc.Location = new Point(12, 135);
            ucdo.txtMapDescription.Location = new Point(122, 135);
            ucdo.txtMapDescription.Size = new Size(300, 21);

            //AutoMap
            ucdo.lblAutoMapMapping.Visible = false;
            ucdo.txtDOAutoMap.Visible = false;

            //Select
            ucdo.chkDOMSelect.Enabled = false;//Namrata: 26/03/2018
            ucdo.chkDOMSelect.Location = new Point(12, 188);

            //BtnDone & BtnCancel
            ucdo.btnDOMDone.Location = new Point(150, 165);
            ucdo.btnDOMCancel.Location = new Point(250, 165);

            //grpDOMap
            ucdo.pbDOMHdr.Size = new Size(450, 22);
            ucdo.grpDOMap.Size = new Size(450, 200);
            ucdo.grpDOMap.Location = new Point(450, 365);

            ucdo.txtDOAutoMap.Text = ""; //Namrata:1/7/2017
            #endregion Visible True
        }
        public void IEC61850Server_OnDoubleClick()
        {
            string strRoutineName = "DOConfiguration: IEC61850Server_OnDoubleClick";
            try
            {
                ucdo.lblKey.Visible = false;
                ucdo.txtKey.Visible = false;
                //Namrata: 20/08/2019
                #region CellNo
                ucdo.CmbCellNo.Visible = false;
                ucdo.lblCellNo.Visible = false;
                #endregion CellNo

                #region Widget
                ucdo.CmbWidget.Visible = false;
                ucdo.lblWidget.Visible = false;
                #endregion Widget
                ucdo.ChkIEC61850MapIndex.Visible = true;//Namrata:16/04/2019
                ucdo.cmbDOMCommandType.Enabled = false;
                ucdo.lblAutoMapMapping.Visible = false;
                ucdo.txtDOAutoMap.Visible = false;

                //DONo
                ucdo.lblDNM.Location = new Point(12, 30);
                ucdo.txtDOMNo.Location = new Point(122, 30);
                ucdo.txtDOMNo.Size = new Size(300, 21);

                //ReportingIndex
                ucdo.lblDMRI.Visible = false;
                ucdo.txtDOMReportingIndex.Visible = false;

                //IEC61850ReportingIndex
                ucdo.lbl61850reportingindex.Visible = true;
                ucdo.CmbReportingIndex.Visible = false;
                ucdo.lbl61850reportingindex.Location = new Point(12, 55);
                ucdo.ChkIEC61850MapIndex.Location = new Point(122, 55);
                ucdo.ChkIEC61850MapIndex.Size = new Size(300, 21);

                //DataType
                ucdo.lblDMDT.Location = new Point(12, 82);
                ucdo.cmbDOMDataType.Location = new Point(122, 82);
                ucdo.cmbDOMDataType.Size = new Size(300, 21);

                //CommandType
                ucdo.lblDMCT.Visible = false;
                ucdo.cmbDOMCommandType.Visible = false;
                ucdo.cmbDOMCommandType.Location = new Point(122, 108);
                ucdo.cmbDOMCommandType.Size = new Size(300, 21);

                //BitPos
                ucdo.lblDMBP.Location = new Point(12, 108);
                ucdo.txtDOMBitPos.Location = new Point(122, 108);
                ucdo.txtDOMBitPos.Size = new Size(300, 21);

                //Map Description
                ucdo.lblmapdesc.Location = new Point(12, 135);
                ucdo.txtMapDescription.Location = new Point(122, 135);
                ucdo.txtMapDescription.Size = new Size(300, 21);

                //Select
                ucdo.chkDOMSelect.Location = new Point(15, 165);

                //BtnDone & BtnCancel
                ucdo.btnDOMDone.Location = new Point(150, 165);
                ucdo.btnDOMCancel.Location = new Point(240, 165);

                //GrpDO
                ucdo.pbDOMHdr.Size = new Size(450, 22);
                ucdo.grpDOMap.Size = new Size(450, 200);
                ucdo.grpDOMap.Location = new Point(450, 365);
                ucdo.txtDOAutoMap.Text = "";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void MODBUSSlave_OnLoad()
        {
            #region Visible False
            ucdo.lblKey.Visible = false;
            ucdo.txtKey.Visible = false;
            //Namrata: 20/08/2019
            #region CellNo
            ucdo.CmbCellNo.Visible = false;
            ucdo.lblCellNo.Visible = false;
            #endregion CellNo

            #region Widget
            ucdo.CmbWidget.Visible = false;
            ucdo.lblWidget.Visible = false;
            #endregion Widget
            //UnitID
            ucdo.lblUnitID.Visible = false;
            ucdo.txtUnitID.Visible = false;

            //Used
            ucdo.chkUsed.Visible = false;

            //61850reportingindex
            ucdo.lbl61850reportingindex.Visible = false;
            ucdo.CmbReportingIndex.Visible = false;
            ucdo.ChkIEC61850MapIndex.Visible = false;//Namrata:16/04/2019

            //Select
            ucdo.chkDOMSelect.Location = new Point(112, 215);
            ucdo.chkDOMSelect.Visible = false;

            //Used
            ucdo.chkUsed.Location = new Point(224, 215);
            ucdo.chkUsed.Visible = false;
            #endregion Visible False

            #region Visible True

            //DOMNo
            ucdo.lblDNM.Location = new Point(12, 33);
            ucdo.txtDOMNo.Location = new Point(112, 30);
            ucdo.txtDOMNo.Size = new Size(220, 20);

            //ReportingIndex
            ucdo.lblDMRI.Location = new Point(12, 58);
            ucdo.txtDOMReportingIndex.Location = new Point(112, 55);
            ucdo.lblDMRI.Visible = true;
            ucdo.txtDOMReportingIndex.Visible = true;
            ucdo.txtDOMReportingIndex.Size = new Size(220, 20);

            //DataType
            ucdo.lblDMDT.Location = new Point(12, 83);
            ucdo.cmbDOMDataType.Location = new Point(112, 80);
            ucdo.lblDMDT.Visible = true;
            ucdo.cmbDOMDataType.Visible = true;
            ucdo.cmbDOMDataType.Size = new Size(220, 20);

            //CommandType
            ucdo.lblDMCT.Location = new Point(12, 109);
            ucdo.cmbDOMCommandType.Location = new Point(112, 106);
            ucdo.cmbDOMCommandType.Size = new Size(220, 20);
            ucdo.lblDMCT.Visible = true;
            ucdo.cmbDOMCommandType.Visible = true;

            //BitPos
            ucdo.lblDMBP.Visible = true; ucdo.txtDOMBitPos.Visible = true;
            ucdo.lblDMBP.Location = new Point(12, 135);
            ucdo.txtDOMBitPos.Location = new Point(112, 132);
            ucdo.txtDOMBitPos.Size = new Size(220, 20);

            //Description
            ucdo.lblmapdesc.Visible = true; ucdo.txtMapDescription.Visible = true;
            ucdo.lblmapdesc.Location = new Point(12, 161);
            ucdo.txtMapDescription.Location = new Point(112, 158);
            ucdo.txtMapDescription.Size = new Size(220, 20);

            //AutoMap
            ucdo.lblAutoMapMapping.Visible = true;
            ucdo.txtDOAutoMap.Visible = true;
            ucdo.lblAutoMapMapping.Location = new Point(12, 186);
            ucdo.txtDOAutoMap.Location = new Point(112, 183);
            ucdo.txtDOAutoMap.Size = new Size(220, 20);

            //btnDone & BtnCancel
            ucdo.btnDOMDone.Location = new Point(112, 215);
            ucdo.btnDOMCancel.Location = new Point(200, 215);

            //grpDOMap
            ucdo.pbDOMHdr.Size = new Size(350, 22);
            ucdo.grpDOMap.Size = new Size(350, 260);
            ucdo.grpDOMap.Location = new Point(249, 307);

            #endregion Visible True

        }
        public void MODBUSSlave_OnDoubleClick()
        {
            string strRoutineName = "DOConfiguration: MODBUSSlave_OnDoubleClick";
            try
            {
                ucdo.lblKey.Visible = false;
                ucdo.txtKey.Visible = false;
                //Namrata: 20/08/2019
                #region CellNo
                ucdo.CmbCellNo.Visible = false;
                ucdo.lblCellNo.Visible = false;
                #endregion CellNo

                #region Widget
                ucdo.CmbWidget.Visible = false;
                ucdo.lblWidget.Visible = false;
                #endregion Widget
                ucdo.lblUnitID.Visible = false;
                ucdo.txtUnitID.Visible = false;
                ucdo.chkUsed.Visible = false;

                ucdo.pbDOMHdr.Size = new Size(350, 22);
                ucdo.grpDOMap.Size = new Size(350, 226);
                ucdo.grpDOMap.Location = new Point(249, 307);

                ucdo.lblDNM.Location = new Point(12, 33);
                ucdo.txtDOMNo.Location = new Point(112, 30);
                ucdo.txtDOMNo.Size = new Size(220, 20);


                ucdo.lblDMRI.Location = new Point(12, 58);
                ucdo.txtDOMReportingIndex.Location = new Point(112, 55);
                ucdo.lblDMRI.Visible = true;
                ucdo.txtDOMReportingIndex.Visible = true;
                ucdo.txtDOMReportingIndex.Size = new Size(220, 20);
                ucdo.ChkIEC61850MapIndex.Visible = false;//Namrata:16/04/2019

                ucdo.lblDMDT.Location = new Point(12, 83);
                ucdo.cmbDOMDataType.Location = new Point(112, 80);
                ucdo.lblDMDT.Visible = true;
                ucdo.cmbDOMDataType.Visible = true;
                ucdo.cmbDOMDataType.Size = new Size(220, 20);

                ucdo.lbl61850reportingindex.Visible = false;
                ucdo.CmbReportingIndex.Visible = false;

                ucdo.lblDMCT.Visible = true; ucdo.cmbDOMCommandType.Visible = true;
                ucdo.lblDMCT.Location = new Point(12, 109);
                ucdo.cmbDOMCommandType.Location = new Point(112, 106);
                ucdo.cmbDOMCommandType.Size = new Size(220, 20);

                ucdo.lblDMBP.Visible = true; ucdo.txtDOMBitPos.Visible = true;
                ucdo.lblDMBP.Location = new Point(12, 135);
                ucdo.txtDOMBitPos.Location = new Point(112, 132);
                ucdo.txtDOMBitPos.Size = new Size(220, 20);

                ucdo.lblmapdesc.Visible = true; ucdo.txtMapDescription.Visible = true;
                ucdo.lblmapdesc.Location = new Point(12, 161);
                ucdo.txtMapDescription.Location = new Point(112, 158);
                ucdo.txtMapDescription.Size = new Size(220, 20);

                ucdo.lblAutoMapMapping.Visible = false;
                ucdo.txtDOAutoMap.Visible = false;
                ucdo.lblAutoMapMapping.Location = new Point(12, 186);
                ucdo.txtDOAutoMap.Location = new Point(112, 183);
                ucdo.txtDOAutoMap.Size = new Size(220, 20);

                ucdo.chkDOMSelect.Location = new Point(112, 215);
                ucdo.chkUsed.Location = new Point(224, 215);
                ucdo.chkUsed.Visible = false;
                ucdo.chkDOMSelect.Visible = false;

                ucdo.btnDOMDone.Location = new Point(112, 187);
                ucdo.btnDOMCancel.Location = new Point(200, 187);

                ucdo.txtDOAutoMap.Text = "0";
                ucdo.chkDOMSelect.Enabled = false;//Namrata: 19/04/2018
                ucdo.cmbDOMCommandType.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void IEC101IEC104Slave_OnDoubleClick()
        {
            string strRoutineName = "DOConfiguration: IEC101IEC104Slave_OnDoubleClick";
            try
            {
                ucdo.lblKey.Visible = false;
                ucdo.txtKey.Visible = false;
                //Namrata: 20/08/2019
                #region CellNo
                ucdo.CmbCellNo.Visible = false;
                ucdo.lblCellNo.Visible = false;
                #endregion CellNo

                #region Widget
                ucdo.CmbWidget.Visible = false;
                ucdo.lblWidget.Visible = false;
                #endregion Widget
                ucdo.lblUnitID.Visible = false;
                ucdo.txtUnitID.Visible = false;
                ucdo.chkUsed.Visible = false;

                ucdo.pbDOMHdr.Size = new Size(350, 22);
                ucdo.grpDOMap.Size = new Size(350, 230);
                ucdo.grpDOMap.Location = new Point(249, 307);

                ucdo.lblDNM.Location = new Point(12, 33);
                ucdo.txtDOMNo.Location = new Point(112, 30);
                ucdo.txtDOMNo.Size = new Size(220, 20);


                ucdo.lblDMRI.Location = new Point(12, 58);
                ucdo.txtDOMReportingIndex.Location = new Point(112, 55);
                ucdo.lblDMRI.Visible = true; ucdo.txtDOMReportingIndex.Visible = true;
                ucdo.txtDOMReportingIndex.Size = new Size(220, 20);


                ucdo.lblDMDT.Location = new Point(12, 83);
                ucdo.cmbDOMDataType.Location = new Point(112, 80);
                ucdo.lblDMDT.Visible = true;
                ucdo.cmbDOMDataType.Visible = true;
                ucdo.cmbDOMDataType.Size = new Size(220, 20);

                ucdo.lbl61850reportingindex.Visible = false;
                ucdo.CmbReportingIndex.Visible = false;
                ucdo.ChkIEC61850MapIndex.Visible = false;//Namrata:16/04/2019

                ucdo.lblDMCT.Visible = false;
                ucdo.cmbDOMCommandType.Visible = false;

                ucdo.lblDMBP.Visible = true; ucdo.txtDOMBitPos.Visible = true;
                ucdo.lblDMBP.Location = new Point(12, 109);
                ucdo.txtDOMBitPos.Location = new Point(112, 106);
                ucdo.txtDOMBitPos.Size = new Size(220, 20);

                ucdo.lblmapdesc.Visible = true; ucdo.txtMapDescription.Visible = true;
                ucdo.lblmapdesc.Location = new Point(12, 135);
                ucdo.txtMapDescription.Location = new Point(112, 132);
                ucdo.txtMapDescription.Size = new Size(220, 20);

                ucdo.lblAutoMapMapping.Location = new Point(12, 161);
                ucdo.txtDOAutoMap.Location = new Point(112, 158);
                ucdo.txtDOAutoMap.Size = new Size(220, 20);
                ucdo.lblAutoMapMapping.Visible = false;
                ucdo.txtDOAutoMap.Visible = false;

                ucdo.chkDOMSelect.Location = new Point(112, 160);

                ucdo.txtDOAutoMap.Text = "0";
                ucdo.chkDOMSelect.Visible = true;
                ucdo.cmbDOMCommandType.Enabled = false;


                //ucdo.chkUsed.Location = new Point(224, 200);
                ucdo.chkUsed.Visible = false;
                ucdo.chkDOMSelect.Visible = true;

                ucdo.btnDOMDone.Location = new Point(112, 185);
                ucdo.btnDOMCancel.Location = new Point(200, 185);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void IEC101Slave_IEC104Slave_OnLoad()
        {
            #region Visible False
            ucdo.lblKey.Visible = false;
            ucdo.txtKey.Visible = false;
            //Namrata: 20/08/2019
            #region CellNo
            ucdo.CmbCellNo.Visible = false;
            ucdo.lblCellNo.Visible = false;
            #endregion CellNo

            #region Widget
            ucdo.CmbWidget.Visible = false;
            ucdo.lblWidget.Visible = false;
            #endregion Widget
            //UnitID
            ucdo.lblUnitID.Visible = false;
            ucdo.txtUnitID.Visible = false;
            //chkUsed
            ucdo.chkUsed.Visible = false;

            //61850reportingindex
            ucdo.lbl61850reportingindex.Visible = false;
            ucdo.CmbReportingIndex.Visible = false;
            ucdo.ChkIEC61850MapIndex.Visible = false;//Namrata:16/04/2019
            //CommandType
            ucdo.lblDMCT.Visible = false;
            ucdo.cmbDOMCommandType.Visible = false;

            #endregion Visible False

            #region Visible True

            //DOMNo
            ucdo.lblDNM.Location = new Point(12, 33);
            ucdo.txtDOMNo.Location = new Point(112, 30);
            ucdo.txtDOMNo.Size = new Size(220, 20);

            //DataType
            ucdo.lblDMDT.Location = new Point(12, 83);
            ucdo.cmbDOMDataType.Location = new Point(112, 80);
            ucdo.lblDMDT.Visible = true;
            ucdo.cmbDOMDataType.Visible = true;
            ucdo.cmbDOMDataType.Size = new Size(220, 20);

            //ReportingIndex
            ucdo.lblDMRI.Location = new Point(12, 58);
            ucdo.txtDOMReportingIndex.Location = new Point(112, 55);
            ucdo.lblDMRI.Visible = true; ucdo.txtDOMReportingIndex.Visible = true;
            ucdo.txtDOMReportingIndex.Size = new Size(220, 20);

            //BitPos
            ucdo.lblDMBP.Visible = true; ucdo.txtDOMBitPos.Visible = true;
            ucdo.lblDMBP.Location = new Point(12, 109);
            ucdo.txtDOMBitPos.Location = new Point(112, 106);
            ucdo.txtDOMBitPos.Size = new Size(220, 20);

            //Select
            ucdo.chkDOMSelect.Visible = true;
            ucdo.chkDOMSelect.Location = new Point(112, 185);

            //Description
            ucdo.lblmapdesc.Visible = true; ucdo.txtMapDescription.Visible = true;
            ucdo.lblmapdesc.Location = new Point(12, 135);
            ucdo.txtMapDescription.Location = new Point(112, 132);
            ucdo.txtMapDescription.Size = new Size(220, 20);

            //Automap
            ucdo.lblAutoMapMapping.Location = new Point(12, 161);
            ucdo.txtDOAutoMap.Location = new Point(112, 158);
            ucdo.txtDOAutoMap.Size = new Size(220, 20);
            ucdo.lblAutoMapMapping.Visible = true;
            ucdo.txtDOAutoMap.Visible = true;

            //BtnDone & BtnCancel
            ucdo.btnDOMDone.Location = new Point(112, 210);
            ucdo.btnDOMCancel.Location = new Point(200, 210);

            //grpDOMap
            ucdo.pbDOMHdr.Size = new Size(350, 22);
            ucdo.grpDOMap.Size = new Size(350, 245);
            ucdo.grpDOMap.Location = new Point(249, 307);
            #endregion Visible True
        }
        private void SportSlave_OnDoubleClick()
        {
            string strRoutineName = "DOConfiguration: SportSlave_OnDoubleClick";
            try
            {
                ucdo.lblKey.Visible = false;
                ucdo.txtKey.Visible = false;
                //Namrata: 20/08/2019
                #region CellNo
                ucdo.CmbCellNo.Visible = false;
                ucdo.lblCellNo.Visible = false;
                #endregion CellNo

                #region Widget
                ucdo.CmbWidget.Visible = false;
                ucdo.lblWidget.Visible = false;
                #endregion Widget
                ucdo.txtDOAutoMap.Text = "0";
                ucdo.pbDOMHdr.Size = new Size(350, 22);
                ucdo.grpDOMap.Size = new Size(350, 250);
                ucdo.grpDOMap.Location = new Point(249, 307);

                ucdo.lblDNM.Location = new Point(12, 33);
                ucdo.txtDOMNo.Location = new Point(112, 30);
                ucdo.txtDOMNo.Size = new Size(220, 20);

                ucdo.lblUnitID.Visible = true;
                ucdo.txtUnitID.Visible = true;
                ucdo.lblUnitID.Location = new Point(12, 58);
                ucdo.txtUnitID.Location = new Point(112, 55);
                ucdo.lblUnitID.Visible = true;
                ucdo.txtUnitID.Visible = true;
                ucdo.txtUnitID.Size = new Size(220, 20);


                ucdo.lblDMRI.Location = new Point(12, 83);
                ucdo.txtDOMReportingIndex.Location = new Point(112, 80);
                ucdo.lblDMRI.Visible = true; ucdo.txtDOMReportingIndex.Visible = true;
                ucdo.txtDOMReportingIndex.Size = new Size(220, 20);

                ucdo.lbl61850reportingindex.Visible = false;
                ucdo.CmbReportingIndex.Visible = false;
                ucdo.ChkIEC61850MapIndex.Visible = false;//Namrata:16/04/2019

                ucdo.lblDMDT.Visible = true; ucdo.cmbDOMDataType.Visible = true;
                ucdo.lblDMDT.Location = new Point(12, 109);
                ucdo.cmbDOMDataType.Location = new Point(112, 106);
                ucdo.cmbDOMDataType.Size = new Size(220, 20);

                ucdo.lblDMCT.Visible = false;
                ucdo.cmbDOMCommandType.Visible = false;

                ucdo.lblDMBP.Visible = true; ucdo.txtDOMBitPos.Visible = true;
                ucdo.lblDMBP.Location = new Point(12, 135);
                ucdo.txtDOMBitPos.Location = new Point(112, 132);
                ucdo.txtDOMBitPos.Size = new Size(220, 20);

                ucdo.lblmapdesc.Visible = true; ucdo.txtMapDescription.Visible = true;
                ucdo.lblmapdesc.Location = new Point(12, 161);
                ucdo.txtMapDescription.Location = new Point(112, 158);
                ucdo.txtMapDescription.Size = new Size(220, 20);

                ucdo.lblAutoMapMapping.Visible = false;
                ucdo.txtDOAutoMap.Visible = false;
                ucdo.lblAutoMapMapping.Location = new Point(12, 186);
                ucdo.txtDOAutoMap.Location = new Point(112, 183);
                ucdo.txtDOAutoMap.Size = new Size(220, 20);

                ucdo.chkDOMSelect.Location = new Point(112, 183);
                ucdo.chkDOMSelect.Visible = true;
                ucdo.chkUsed.Location = new Point(224, 183);
                ucdo.chkUsed.Visible = true;

                ucdo.btnDOMDone.Location = new Point(112, 205);
                ucdo.btnDOMCancel.Location = new Point(200, 205);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Namrata: 20/08/2019
        public void DisplaySlave_OnLoad()
        {
            #region Visible False
            ucdo.lblKey.Visible = false;
            ucdo.txtKey.Visible = false;
            //Used
            ucdo.chkUsed.Visible = false;

            //61850reportingindex
            ucdo.lbl61850reportingindex.Visible = false;
            ucdo.CmbReportingIndex.Visible = false;
            ucdo.ChkIEC61850MapIndex.Visible = false;//Namrata:16/04/2019

            //Select
            ucdo.chkDOMSelect.Location = new Point(112, 215);
            ucdo.chkDOMSelect.Visible = false;

            //Used
            ucdo.chkUsed.Location = new Point(224, 215);
            ucdo.chkUsed.Visible = false;
            #endregion Visible False

            #region Visible True

            //DOMNo
            ucdo.lblDNM.Visible = true;
            ucdo.txtDOMNo.Visible = true;
            ucdo.lblDNM.Location = new Point(12, 33);
            ucdo.txtDOMNo.Location = new Point(112, 30);
            ucdo.txtDOMNo.Size = new Size(160, 21); ;

            //ReportingIndex
            ucdo.lblDMRI.Visible = false;
            ucdo.txtDOMReportingIndex.Visible = false;

            //IEC61850 ReportingIndex
            ucdo.lbl61850reportingindex.Visible = false;
            ucdo.ChkIEC61850MapIndex.Visible = false;

            //DataType
            ucdo.lblDMDT.Visible = false;
            ucdo.cmbDOMDataType.Visible = false;

            //CommandType
            ucdo.lblDMCT.Visible = false;
            ucdo.cmbDOMCommandType.Visible = false;

            //BitPos
            ucdo.lblDMBP.Visible = false;
            ucdo.txtDOMBitPos.Visible = false;

            #region CellNo
            ucdo.lblCellNo.Visible = true;
            ucdo.CmbCellNo.Visible = true;
            ucdo.lblCellNo.Location = new Point(12, 58);
            ucdo.CmbCellNo.Location = new Point(112, 55);
            ucdo.CmbCellNo.Size = new Size(160, 21);
            #endregion CellNo

            #region Widgets
            ucdo.lblWidget.Visible = true;
            ucdo.CmbWidget.Visible = true;
            ucdo.lblWidget.Location = new Point(12, 83);
            ucdo.CmbWidget.Location = new Point(112, 80);
            ucdo.CmbWidget.Size = new Size(160, 21);
            #endregion Widgets

            //UnitID
            //ucdo.lblUnitID.Text = "Unit";
            //Namrata: 04/12/2019
            ucdo.lblUnitID.Text = "";
            ucdo.lblUnitID.Visible = false;
            ucdo.txtUnitID.Visible = false;
            ucdo.lblUnitID.Enabled = false;
            ucdo.txtUnitID.Enabled = false;
            //ucdo.lblUnitID.Location = new Point(12, 109);
            //ucdo.txtUnitID.Location = new Point(112, 106);
            ucdo.txtUnitID.Size = new Size(160, 21);
            ucdo.txtUnitID.Text = "";
            //ucdo.txtUnitID.Text = "Unity";

            //Description
            ucdo.lblmapdesc.Visible = true;
            ucdo.txtMapDescription.Visible = true;
            ucdo.lblmapdesc.Location = new Point(12, 109);
            ucdo.txtMapDescription.Location = new Point(112, 106);
            ucdo.txtMapDescription.Size = new Size(161, 21);

            //AutoMap
            ucdo.lblAutoMapMapping.Visible = false;
            ucdo.txtDOAutoMap.Visible = false;
            ucdo.txtDOAutoMap.Text = "1";
            ucdo.lblAutoMapMapping.Location = new Point(12, 161);
            ucdo.txtDOAutoMap.Location = new Point(112, 158);
            ucdo.txtDOAutoMap.Size = new Size(161, 21);

            //btnDone & BtnCancel
            ucdo.btnDOMDone.Location = new Point(112, 135);
            ucdo.btnDOMCancel.Location = new Point(200, 135);

            //grpDOMap
            ucdo.pbDOMHdr.Size = new Size(350, 22);
            ucdo.grpDOMap.Size = new Size(290, 180);
            ucdo.grpDOMap.Location = new Point(250, 200);

            #endregion Visible True

        }
        private void SportSlave_OnLoad()
        {
            string strRoutineName = "DOConfiguration: SportSlave_OnLoad";
            try
            {
                ucdo.lblKey.Visible = false;
                ucdo.txtKey.Visible = false;
                #region Visible False
                //CommandType
                ucdo.lblDMCT.Visible = false;
                ucdo.cmbDOMCommandType.Visible = false;

                //61850reportingindex
                ucdo.lbl61850reportingindex.Visible = false;
                ucdo.CmbReportingIndex.Visible = false;
                ucdo.ChkIEC61850MapIndex.Visible = false;//Namrata:16/04/2019
                #endregion Visible False

                #region Visible True
                //DOMNo
                ucdo.lblDNM.Location = new Point(12, 33);
                ucdo.txtDOMNo.Location = new Point(112, 30);
                ucdo.txtDOMNo.Size = new Size(220, 20);

                //UnitID
                ucdo.lblUnitID.Visible = true;
                ucdo.txtUnitID.Visible = true;
                ucdo.lblUnitID.Location = new Point(12, 58);
                ucdo.txtUnitID.Location = new Point(112, 55);
                ucdo.txtUnitID.Size = new Size(220, 20);

                //ReportingIndex
                ucdo.lblDMRI.Location = new Point(12, 83);
                ucdo.txtDOMReportingIndex.Location = new Point(112, 80);
                ucdo.lblDMRI.Visible = true;
                ucdo.txtDOMReportingIndex.Visible = true;
                ucdo.txtDOMReportingIndex.Size = new Size(220, 20);

                //DataType
                ucdo.lblDMDT.Visible = true; ucdo.cmbDOMDataType.Visible = true;
                ucdo.lblDMDT.Location = new Point(12, 109);
                ucdo.cmbDOMDataType.Location = new Point(112, 106);
                ucdo.cmbDOMDataType.Size = new Size(220, 20);

                //BitPos
                ucdo.lblDMBP.Visible = true; ucdo.txtDOMBitPos.Visible = true;
                ucdo.lblDMBP.Location = new Point(12, 135);
                ucdo.txtDOMBitPos.Location = new Point(112, 132);
                ucdo.txtDOMBitPos.Size = new Size(220, 20);

                //Description
                ucdo.lblmapdesc.Visible = true; ucdo.txtMapDescription.Visible = true;
                ucdo.lblmapdesc.Location = new Point(12, 161);
                ucdo.txtMapDescription.Location = new Point(112, 158);
                ucdo.txtMapDescription.Size = new Size(220, 20);

                //Automap
                ucdo.lblAutoMapMapping.Visible = true;
                ucdo.txtDOAutoMap.Visible = true;
                ucdo.lblAutoMapMapping.Location = new Point(12, 186);
                ucdo.txtDOAutoMap.Location = new Point(112, 183);
                ucdo.txtDOAutoMap.Size = new Size(220, 20);

                //Select
                ucdo.chkDOMSelect.Location = new Point(112, 213);
                ucdo.chkDOMSelect.Visible = true; ucdo.chkDOMSelect.Enabled = true;
                ucdo.chkUsed.Enabled = true;

                //Used
                ucdo.chkUsed.Location = new Point(224, 213);
                ucdo.chkUsed.Visible = true;

                //btnDone & BtnCancel
                ucdo.btnDOMDone.Location = new Point(112, 233);
                ucdo.btnDOMCancel.Location = new Point(200, 233);

                //GrpDOMap
                ucdo.pbDOMHdr.Size = new Size(350, 22);
                ucdo.grpDOMap.Size = new Size(350, 268);
                ucdo.grpDOMap.Location = new Point(249, 307);
                #endregion Visible True
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DNPSlave_OnLoad()
        {
            #region Visible False
            ucdo.lblKey.Visible = false;
            ucdo.txtKey.Visible = false;
            //Namrata: 20/08/2019
            #region CellNo
            ucdo.CmbCellNo.Visible = false;
            ucdo.lblCellNo.Visible = false;
            #endregion CellNo

            #region Widget
            ucdo.CmbWidget.Visible = false;
            ucdo.lblWidget.Visible = false;
            #endregion Widget
            //UnitID
            ucdo.lblUnitID.Visible = false;
            ucdo.txtUnitID.Visible = false;

            //Used
            ucdo.chkUsed.Visible = false;

            //61850reportingindex
            ucdo.lbl61850reportingindex.Visible = false;
            ucdo.CmbReportingIndex.Visible = false;
            ucdo.ChkIEC61850MapIndex.Visible = false;//Namrata:16/04/2019



            //Used
            ucdo.chkUsed.Location = new Point(224, 215);
            ucdo.chkUsed.Visible = false;
            #endregion Visible False

            #region Visible True

            //DOMNo
            ucdo.lblDNM.Location = new Point(12, 33);
            ucdo.txtDOMNo.Location = new Point(112, 30);
            ucdo.txtDOMNo.Size = new Size(220, 20);

            //ReportingIndex
            ucdo.lblDMRI.Location = new Point(12, 58);
            ucdo.txtDOMReportingIndex.Location = new Point(112, 55);
            ucdo.lblDMRI.Visible = true;
            ucdo.txtDOMReportingIndex.Visible = true;
            ucdo.txtDOMReportingIndex.Size = new Size(220, 20);

            //DataType
            ucdo.lblDMDT.Visible = false;
            ucdo.cmbDOMDataType.Visible = false;

            //CommandType
            ucdo.lblDMCT.Visible = true;
            ucdo.cmbDOMCommandType.Visible = true;
            ucdo.lblDMCT.Location = new Point(12, 83);
            ucdo.cmbDOMCommandType.Location = new Point(112, 80);
            ucdo.cmbDOMCommandType.Size = new Size(220, 20);



            ucdo.lblEV.Visible = true;
            ucdo.CmbEV.Visible = true;
            ucdo.lblEV.Location = new Point(12, 109);
            ucdo.CmbEV.Location = new Point(112, 106);
            ucdo.CmbEV.Size = new Size(220, 20);


            ucdo.lblEventClass.Visible = true;
            ucdo.cmbEventC.Visible = true;
            ucdo.lblEventClass.Location = new Point(12, 137);
            ucdo.cmbEventC.Location = new Point(112, 134);
            ucdo.cmbEventC.Size = new Size(220, 20);



            ucdo.lblVariation.Visible = true;
            ucdo.CmbVari.Visible = true;
            ucdo.lblVariation.Location = new Point(12, 162);
            ucdo.CmbVari.Location = new Point(112, 159);
            ucdo.CmbVari.Size = new Size(220, 20);





            //BitPos
            ucdo.lblDMBP.Visible = true; ucdo.txtDOMBitPos.Visible = true;
            ucdo.lblDMBP.Location = new Point(12, 187);
            ucdo.txtDOMBitPos.Location = new Point(112, 184);
            ucdo.txtDOMBitPos.Size = new Size(220, 20);

            //Description
            ucdo.lblmapdesc.Visible = true; ucdo.txtMapDescription.Visible = true;
            ucdo.lblmapdesc.Location = new Point(12, 212);
            ucdo.txtMapDescription.Location = new Point(112, 209);
            ucdo.txtMapDescription.Size = new Size(220, 20);

            //AutoMap
            ucdo.lblAutoMapMapping.Visible = true;
            ucdo.txtDOAutoMap.Visible = true;
            ucdo.lblAutoMapMapping.Location = new Point(12, 237);
            ucdo.txtDOAutoMap.Location = new Point(112, 234);
            ucdo.txtDOAutoMap.Size = new Size(220, 20);


            //Select
            ucdo.chkDOMSelect.Location = new Point(112, 260);
            ucdo.chkDOMSelect.Visible = true;

            //btnDone & BtnCancel
            ucdo.btnDOMDone.Location = new Point(112, 285);
            ucdo.btnDOMCancel.Location = new Point(200, 285);

            //grpDOMap
            ucdo.pbDOMHdr.Size = new Size(350, 22);
            ucdo.grpDOMap.Size = new Size(350, 320);
            ucdo.grpDOMap.Location = new Point(249, 307);

            #endregion Visible True

        }
        public void DNPSlave_OnDoubleClick()
        {
            #region Visible False
            ucdo.lblKey.Visible = false;
            ucdo.txtKey.Visible = false;
            //Namrata: 20/08/2019
            #region CellNo
            ucdo.CmbCellNo.Visible = false;
            ucdo.lblCellNo.Visible = false;
            #endregion CellNo

            #region Widget
            ucdo.CmbWidget.Visible = false;
            ucdo.lblWidget.Visible = false;
            #endregion Widget
            //UnitID
            ucdo.lblUnitID.Visible = false;
            ucdo.txtUnitID.Visible = false;

            //Used
            ucdo.chkUsed.Visible = false;

            //61850reportingindex
            ucdo.lbl61850reportingindex.Visible = false;
            ucdo.CmbReportingIndex.Visible = false;
            ucdo.ChkIEC61850MapIndex.Visible = false;//Namrata:16/04/2019

            //Select
            ucdo.chkDOMSelect.Location = new Point(112, 215);
            ucdo.chkDOMSelect.Visible = false;

            //Used
            ucdo.chkUsed.Location = new Point(224, 215);
            ucdo.chkUsed.Visible = false;
            #endregion Visible False

            #region Visible True

            //DOMNo
            ucdo.lblDNM.Location = new Point(12, 33);
            ucdo.txtDOMNo.Location = new Point(112, 30);
            ucdo.txtDOMNo.Size = new Size(220, 20);

            //ReportingIndex
            ucdo.lblDMRI.Location = new Point(12, 58);
            ucdo.txtDOMReportingIndex.Location = new Point(112, 55);
            ucdo.lblDMRI.Visible = true;
            ucdo.txtDOMReportingIndex.Visible = true;
            ucdo.txtDOMReportingIndex.Size = new Size(220, 20);

            //DataType
            ucdo.lblDMDT.Visible = false;
            ucdo.cmbDOMDataType.Visible = false;

            //CommandType
            ucdo.lblDMCT.Visible = true;
            ucdo.cmbDOMCommandType.Visible = true;
            ucdo.lblDMCT.Location = new Point(12, 83);
            ucdo.cmbDOMCommandType.Location = new Point(112, 80);
            ucdo.cmbDOMCommandType.Size = new Size(220, 20);

            ucdo.lblEV.Visible = true;
            ucdo.CmbEV.Visible = true;
            ucdo.lblEV.Location = new Point(12, 109);
            ucdo.CmbEV.Location = new Point(112, 106);
            ucdo.CmbEV.Size = new Size(220, 20);

            ucdo.lblEventClass.Visible = true;
            ucdo.cmbEventC.Visible = true;
            ucdo.lblEventClass.Location = new Point(12, 137);
            ucdo.cmbEventC.Location = new Point(112, 134);
            ucdo.cmbEventC.Size = new Size(220, 20);

            ucdo.lblVariation.Visible = true;
            ucdo.CmbVari.Visible = true;
            ucdo.lblVariation.Location = new Point(12, 162);
            ucdo.CmbVari.Location = new Point(112, 159);
            ucdo.CmbVari.Size = new Size(220, 20);

            //BitPos
            ucdo.lblDMBP.Visible = true; ucdo.txtDOMBitPos.Visible = true;
            ucdo.lblDMBP.Location = new Point(12, 187);
            ucdo.txtDOMBitPos.Location = new Point(112, 184);
            ucdo.txtDOMBitPos.Size = new Size(220, 20);

            //Description
            ucdo.lblmapdesc.Visible = true; ucdo.txtMapDescription.Visible = true;
            ucdo.lblmapdesc.Location = new Point(12, 212);
            ucdo.txtMapDescription.Location = new Point(112, 209);
            ucdo.txtMapDescription.Size = new Size(220, 20);

            //Select
            ucdo.chkDOMSelect.Location = new Point(112, 240);
            ucdo.chkDOMSelect.Visible = true;

            //AutoMap
            ucdo.lblAutoMapMapping.Visible = false;
            ucdo.txtDOAutoMap.Visible = false;
            ucdo.lblAutoMapMapping.Location = new Point(12, 237);
            ucdo.txtDOAutoMap.Location = new Point(112, 234);
            ucdo.txtDOAutoMap.Size = new Size(220, 20);

            //btnDone & BtnCancel
            ucdo.btnDOMDone.Location = new Point(112, 265);
            ucdo.btnDOMCancel.Location = new Point(200, 265);

            //grpDOMap
            ucdo.pbDOMHdr.Size = new Size(350, 22);
            ucdo.grpDOMap.Size = new Size(350, 300);
            ucdo.grpDOMap.Location = new Point(249, 307);

            #endregion Visible True

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
