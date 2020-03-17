using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Data;
namespace OpenProPlusConfigurator
{
    public class ENConfiguration
    {
        #region Declaration
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        private int SelectedIndex;
        DataSet dsdummy = null; //Ajay: 17/01/2019
        int ENNo = 0;
        int INdex = 0;
        string Description = "";
        private string Response = "";
        private RCBConfiguration RCBNode = null;
        List<string> MergeList = null;
        List<string> ObjectRef = null;
        List<string> FC = null;
        List<string> IEC61850CheckedList = null;
        int intCheckItems = 0;
        private string rnName = "";
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        private Mode mapMode = Mode.NONE;
        private int mapEditIndex = -1;
        private bool isNodeComment = false;
        private string comment = "";
        private string currentSlave = "";
        Dictionary<string, List<ENMap>> slavesENMapList = new Dictionary<string, List<ENMap>>();
        private MasterTypes masterType = MasterTypes.UNKNOWN;
        private int masterNo = -1;
        private int IEDNo = -1;
        List<EN> enList = new List<EN>();
        ucENlist ucen = new ucENlist();
        private const int COL_CMD_TYPE_WIDTH = 130;
        //Namrata: 11/09/2017
        //Fill RessponseType in All Configuration . 
        public DataGridView dataGridViewDataSet = new DataGridView();
        public DataTable dtdataset = new DataTable();
        DataRow datasetRow;
        #endregion Declaration
        public ENConfiguration(MasterTypes mType, int mNo, int iNo)
        {
            masterType = mType;
            masterNo = mNo;
            IEDNo = iNo;

            #region Btn Events
            ucen.btnAddClick += new System.EventHandler(this.btnAdd_Click);
            ucen.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
            ucen.btnVerifyClick += new EventHandler(this.btnVerify_Click); //Ajay: 31/07/2018
            ucen.btnDoneClick += new System.EventHandler(this.btnDone_Click);
            ucen.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
            ucen.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
            ucen.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
            ucen.btnNextClick += new System.EventHandler(this.btnNext_Click);
            ucen.btnLastClick += new System.EventHandler(this.btnLast_Click);
            ucen.btnENMDeleteClick += new System.EventHandler(this.btnENMDelete_Click);
            ucen.btnENMDoneClick += new System.EventHandler(this.btnENMDone_Click);
            ucen.btnENMCancelClick += new System.EventHandler(this.btnENMCancel_Click);
            #endregion Btn Events

            #region Listview Events
            ucen.lvENMapDoubleClick += new System.EventHandler(this.lvENMap_DoubleClick);
            ucen.lvENlistDoubleClick += new System.EventHandler(this.lvENlist_DoubleClick);
            ucen.lvENlistItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvENlist_ItemSelectionChanged);
            ucen.lvENMapItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvENMap_ItemSelectionChanged);
            #endregion Listview Events

            #region Combobox Events
            ucen.cmbIEDName.SelectedIndexChanged += new System.EventHandler(this.cmbIEDName_SelectedIndexChanged);
            ucen.cmb61850ResponseType.SelectedIndexChanged += new System.EventHandler(this.cmb61850ResponseType_SelectedIndexChanged);
            ucen.cmb61850Index.SelectedIndexChanged += new System.EventHandler(this.cmb61850Index_SelectedIndexChanged);
            ucen.ChkIEC61850Index.SelectedIndexChanged += new System.EventHandler(this.ChkIEC61850Index_SelectedIndexChanged);//Namrata:22/03/2019
            ucen.cmbAI1TextChanged += new System.EventHandler(this.cmbAI1_TextChanged);//Ajay: 10/10/2018
            ucen.cmbEN1TextChanged += new System.EventHandler(this.cmbEN1_TextChanged);//Ajay: 10/10/2018
            ucen.cmbFCSelectedIndexChanged += new System.EventHandler(this.cmbFC_SelectedIndexChanged); //Ajay: 17/01/2019
            #endregion Combobox Events

            #region Link Button
            ucen.lnkbtnDeleteAll.Click += new System.EventHandler(this.LinkDeleteConfigue_Click);
            ucen.lnkENMapClick += new System.EventHandler(this.lnkENMap_Click);
            #endregion Link Button

            #region All Masters Events

            if (mType == MasterTypes.Virtual)//Disable add/edit/delete/dblclick n remove checkboxes...
            {
                ucen.btnAdd.Enabled = false;
                ucen.btnDelete.Enabled = false;
            }
            else
            {
                ucen.lvENlistDoubleClick += new System.EventHandler(this.lvENlist_DoubleClick);
            }
            if (mType == MasterTypes.ADR)
            {
                MasterIEC103_IEC104_ADR_Modbus_OnLoad();
            }
            if (mType == MasterTypes.SPORT)
            {
                SPORTMaster_IEC101Master_OnLoad();
            }
            if (mType == MasterTypes.IEC101)
            {
                SPORTMaster_IEC101Master_OnLoad();
            }
            if (mType == MasterTypes.IEC104)
            {
                MasterIEC103_IEC104_ADR_Modbus_OnLoad();
            }
            if (mType == MasterTypes.IEC103)
            {
                MasterIEC103_IEC104_ADR_Modbus_OnLoad();
            }
            if (mType == MasterTypes.MODBUS)
            {
                MasterIEC103_IEC104_ADR_Modbus_OnLoad();
            }
            if (mType == MasterTypes.IEC61850Client)
            {
                IEC61850Client_OnLoad();
            }
            //Ajay: 31/07/2018
            if (mType == MasterTypes.LoadProfile)
            {
                LoadProfile_OnLoad();
            }
            #endregion All Masters Events

            addListHeaders();
            fillOptions();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Ajay: 07/12/2018
            if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
            {
                if (AllowMasterOptionsOnClick(masterType)) { return; }
                else { }
            }

            if (enList.Count >= getMaxENs())
            {
                MessageBox.Show("Maximum " + getMaxENs() + " EN's are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            mode = Mode.ADD;
            editIndex = -1;
            #region All Masters Events
            if ((masterType == MasterTypes.ADR) || (masterType == MasterTypes.IEC103) || (masterType == MasterTypes.MODBUS))
            {
                MasterIEC103_IEC104_ADR_Modbus_OnLoad();
            }
            //Ajay: 27/07/2018
            if (masterType == MasterTypes.SPORT)
            {
                SPORTMaster_IEC101Master_OnLoad();
            }
            //Ajay: 12/11/2018
            if (masterType == MasterTypes.IEC101)
            {
                SPORTMaster_IEC101Master_OnLoad();
            }
            //Ajay: 27/07/2018
            if (masterType == MasterTypes.IEC104)
            {
                MasterIEC103_IEC104_ADR_Modbus_OnLoad(); //Ajay: 02/09/2018
            }
            if (masterType == MasterTypes.IEC61850Client)
            {
                IEC61850Client_OnLoad();
                FetchComboboxData();
            }
            //Ajay: 31/07/2018
            if (masterType == MasterTypes.LoadProfile)
            {
                LoadProfile_OnLoad(); //Ajay: 31/07/2018
            }
            #endregion All Masters Events

            Utils.resetValues(ucen.grpEN);
            Utils.showNavigation(ucen.grpEN, false);
            fillOptions(); //Ajay: 22/09/2018
            loadDefaults();
            ucen.grpEN.Visible = true;
            ucen.cmbResponseType.Focus();
            //Namrata: 04/04/2018
            if (masterType == MasterTypes.IEC61850Client)
            {
                if (ucen.cmbIEDName.SelectedIndex != -1)
                {
                    ucen.ChkIEC61850Index.Text = "";//Namrata:25/03/2019
                    if (IEC61850CheckedList != null)
                    {
                        ucen.ChkIEC61850Index.CheckBoxItems.ForEach(x =>
                        {
                            x.Checked = false; x.CheckState = CheckState.Unchecked;
                        });
                    }
                    ucen.cmbIEDName.SelectedIndex = ucen.cmbIEDName.FindStringExact(Utils.Iec61850IEDname);

                }
                else
                {
                    ucen.cmbIEDName.Visible = false;
                    MessageBox.Show("ICD File Missing !!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucen btnDelete_Click clicked in class!!!");
            Console.WriteLine("*** enList count: {0} lv count: {1}", enList.Count, ucen.lvENlist.Items.Count);
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

            for (int i = ucen.lvENlist.Items.Count - 1; i >= 0; i--)
            {
                if (ucen.lvENlist.Items[i].Checked)
                {
                    Console.WriteLine("*** removing indices: {0}", i);
                    deleteENFromMaps(enList.ElementAt(i).ENNo);
                    enList.RemoveAt(i);
                    ucen.lvENlist.Items[i].Remove();
                }
            }
            Console.WriteLine("*** enList count: {0} lv count: {1}", enList.Count, ucen.lvENlist.Items.Count);
            refreshList();
            //Refresh map listview...
            refreshCurrentMapList();
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            #region LoadProfile
            if (masterType == MasterTypes.LoadProfile)//Ajay: 31/07/2018
            {
                string[] str = new string[2];
                if (!IsVerified(ucen.cmbAI1, "AI1", out str))
                {
                    string dlgMsg = str[0];
                    if (!string.IsNullOrEmpty(str[1])) { dlgMsg += "=" + str[1]; }
                    dlgMsg += " is not valid." + Environment.NewLine + "Do you want to continue?";
                    DialogResult rslt = MessageBox.Show(dlgMsg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rslt.ToString().ToLower() == "no") { return; }
                }
                if (!IsVerified(ucen.cmbEN1, "EN1", out str))
                {
                    string dlgMsg = str[0];
                    if (!string.IsNullOrEmpty(str[1])) { dlgMsg += "=" + str[1]; }
                    dlgMsg += " is not valid." + Environment.NewLine + "Do you want to continue?";
                    DialogResult rslt = MessageBox.Show(dlgMsg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rslt.ToString().ToLower() == "no") { return; }
                }
            }
            #endregion LoadProfile
            //if (!Validate()) return;
            List<KeyValuePair<string, string>> enData = Utils.getKeyValueAttributes(ucen.grpEN);

            #region Fill Address to Datatable for RCBConfiguration
            if (masterType == MasterTypes.IEC61850Client)//Namrata: 27/09/2017
            {
                IEC61850CheckedList = ucen.ChkIEC61850Index.CheckBoxItems.Where(x => x.Checked == true).Select(x => x.Text).ToList();//Namrata:25/03/2019
                Response = ucen.cmb61850ResponseType.Text;
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
                if (Utils.dsRCBEN.Tables.Contains(dtdataset.TableName))
                {
                    Utils.dsRCBEN.Tables[dtdataset.TableName].Clear();
                }
                else
                {
                    Utils.dsRCBEN.Tables.Add(dtdataset.TableName);
                    Utils.dsRCBEN.Tables[dtdataset.TableName].Columns.Add("ObjectReferrence");
                    Utils.dsRCBEN.Tables[dtdataset.TableName].Columns.Add("Node");
                }
                for (int i = 0; i < dtdataset.Rows.Count; i++)
                {
                    row112 = Utils.dsRCBEN.Tables[dtdataset.TableName].NewRow();
                    Utils.dsRCBEN.Tables[dtdataset.TableName].NewRow();
                    for (int j = 0; j < dtdataset.Columns.Count; j++)
                    {
                        Index112 = dtdataset.Rows[i][j].ToString();
                        row112[j] = Index112.ToString();
                    }
                    Utils.dsRCBEN.Tables[dtdataset.TableName].Rows.Add(row112);
                }
                Utils.dsRCBData = Utils.dsRCBEN;
                Utils.dsRCBData.Merge(Utils.dsRCBAI, false, MissingSchemaAction.Add);
                Utils.dsRCBData.Merge(Utils.dsRCBAO, false, MissingSchemaAction.Add);
                Utils.dsRCBData.Merge(Utils.dsRCBDI, false, MissingSchemaAction.Add);
                Utils.dsRCBData.Merge(Utils.dsRCBDO, false, MissingSchemaAction.Add);
            }
            #endregion Fill Address to Datatable for RCBConfiguration 

            #region ADD
            if (mode == Mode.ADD)
            {
                int intStart = Convert.ToInt32(enData[16].Value);//ENONo
                int intRange = 0;//Ajay: 31/07/2018
                int intIndex = 0;//Ajay: 31/07/2018
                if (IEC61850CheckedList != null)
                {
                    intCheckItems = IEC61850CheckedList.Count();
                }
                #region LoadProfile
                if (masterType == MasterTypes.LoadProfile) { intRange = 1; }
                else { intRange = Convert.ToInt32(enData[10].Value); } //AutoMapRange
                if (masterType != MasterTypes.LoadProfile)
                { intIndex = Convert.ToInt32(enData[18].Value); }//ENIndex 
                #endregion LoadProfile

                #region getMaxENs
                if (intRange > getMaxENs())//Ajay: 23/11/2017
                {
                    MessageBox.Show("Maximum " + getMaxENs() + " EN's are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion getMaxENs

                else
                {
                    #region IEC61850Client
                    if (masterType == MasterTypes.IEC61850Client)
                    {
                        int iListCount = 0;
                        for (int i = intStart; i <= intStart + intCheckItems - 1; i++)
                        {
                            ENNo = Globals.ENNo;
                            ENNo += 1;
                            INdex = intIndex++;

                            Description = ucen.txtDescription.Text;
                            EN NewEI = new EN("EN", enData, null, masterType, masterNo, IEDNo);
                            NewEI.ENNo = ENNo.ToString();

                            NewEI.Index = INdex.ToString();
                            NewEI.Description = Description;
                            NewEI.IEDName = ucen.cmbIEDName.Text.ToString();
                            NewEI.IEC61850Index = IEC61850CheckedList[iListCount].ToString();
                            NewEI.IEC61850ResponseType = ucen.cmb61850ResponseType.Text.ToString();
                            string FindFC = MergeList.Where(x => x.Contains(IEC61850CheckedList[iListCount].ToString() + ",")).Select(x => x).FirstOrDefault();
                            string[] GetFC = FindFC.Split(',');
                            ucen.cmbFC.SelectedIndex = ucen.cmbFC.FindStringExact(GetFC[1].ToString());
                            NewEI.FC = ucen.cmbFC.Text.ToString();
                            enList.Add(NewEI);
                            iListCount++;
                        }
                    }
                    #endregion IEC61850Client

                    #region OtherMasters
                    else
                    {
                        for (int i = intStart; i <= intStart + intRange - 1; i++)//Ajay: 21/11/2017
                        {
                            ENNo = Globals.ENNo;
                            ENNo += 1;
                            INdex = intIndex++;
                            if (masterType == MasterTypes.ADR)
                            {
                                Description = ucen.txtDescription.Text;
                            }
                            else if (masterType == MasterTypes.SPORT)
                            {
                                Description = ucen.txtDescription.Text;
                            }
                            else if (masterType == MasterTypes.IEC101)
                            {
                                Description = ucen.txtDescription.Text;
                            }
                            else if (masterType == MasterTypes.IEC104)
                            {
                                Description = ucen.txtDescription.Text;
                            }
                            else if (masterType == MasterTypes.IEC103)
                            {
                                Description = ucen.txtDescription.Text;
                            }
                            else if (masterType == MasterTypes.MODBUS)
                            {
                                Description = ucen.txtDescription.Text;
                            }

                            else if (masterType == MasterTypes.LoadProfile)//Ajay: 31/07/2018
                            {
                                Description = ucen.txtDescription.Text;
                            }
                            EN NewEI = new EN("EN", enData, null, masterType, masterNo, IEDNo);
                            NewEI.ENNo = ENNo.ToString();
                            NewEI.Index = INdex.ToString();//Ajay: 11/06/2018
                            if ((ucen.cmbDataType.Text == "UnsignedInt32_LSB_MSB")
                                || (ucen.cmbDataType.Text == "SignedInt32_LSB_MSB") || (ucen.cmbDataType.Text == "UnsignedInt32_MSB_LSB")
                                || (ucen.cmbDataType.Text == "SignedInt32_MSB_LSB") || (ucen.cmbDataType.Text == "Float_LSB_MSB")
                                || (ucen.cmbDataType.Text == "Float_MSB_LSB") || (ucen.cmbDataType.Text == "UnsignedLong32_Bit_MSWord_LSWord")
                                || (ucen.cmbDataType.Text == "UnsignedLong32_Bit_LSWord_MSWord") || (ucen.cmbDataType.Text == "SignedLong32_Bit_MSWord_LSWord")
                                || (ucen.cmbDataType.Text == "SignedLong32_Bit_LSWord_MSWord") || (ucen.cmbDataType.Text == "Float_MSWord_LSWord")
                                || (ucen.cmbDataType.Text == "Float_LSWord_MSWord") || (ucen.cmbDataType.Text == "UnsignedInt24_LSB_MSB")
                                || (ucen.cmbDataType.Text == "SignedInt24_LSB_MSB") || (ucen.cmbDataType.Text == "UnsignedInt24_MSB_LSB")
                                || (ucen.cmbDataType.Text == "SignedInt24_MSB_LSB"))
                            {
                                intIndex++;
                            }

                            NewEI.Description = Description;
                            enList.Add(NewEI);
                        }
                    }

                    #endregion OtherMasters
                }
            }
            #endregion ADD

            #region EDIT
            else if (mode == Mode.EDIT)
            {
                if (ucen.ChkIEC61850Index.CheckBoxItems.Count > 0)
                {
                    if (ucen.ChkIEC61850Index.CheckBoxItems.Where(x => x.Checked == true).Count() > 1)
                    {
                        MessageBox.Show("Please select only 1 index.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        enList[editIndex].updateAttributes(enData);
                    }
                }
                else
                {
                    enList[editIndex].updateAttributes(enData);
                }
                //enList[editIndex].updateAttributes(enData);
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
                ucen.grpEN.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
            }
        }
        private void btnVerify_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnVerify_Click";
            try
            {
                string[] str = new string[2];
                if (!IsVerified(ucen.cmbAI1, "AI1", out str) || !IsVerified(ucen.cmbEN1, "EN1", out str))
                {
                    string dlgMsg = str[0];
                    if (!string.IsNullOrEmpty(str[1])) { dlgMsg += "=" + str[1]; }
                    dlgMsg += " is not valid.";
                    MessageBox.Show(dlgMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("AI1 & EN1 are valid.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex) { MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucen btnCancel_Click clicked in class!!!");
            ucen.grpEN.Visible = false;
            mode = Mode.NONE;
            editIndex = -1;
            Utils.resetValues(ucen.grpEN);
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucen btnFirst_Click clicked in class!!!");
            if (ucen.lvENlist.Items.Count <= 0) return;
            if (enList.ElementAt(0).IsNodeComment) return;
            editIndex = 0;
            loadValues();
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            //Namrata: 27/7/2017
            btnDone_Click(null, null);
            Console.WriteLine("*** ucen btnPrev_Click clicked in class!!!");
            if (editIndex - 1 < 0) return;
            if (enList.ElementAt(editIndex - 1).IsNodeComment) return;
            editIndex--;
            loadValues();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            //Namrata: 27/7/2017
            btnDone_Click(null, null);
            Console.WriteLine("*** ucen btnNext_Click clicked in class!!!");
            if (editIndex + 1 >= ucen.lvENlist.Items.Count) return;
            if (enList.ElementAt(editIndex + 1).IsNodeComment) return;
            editIndex++;
            loadValues();
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucen btnLast_Click clicked in class!!!");
            if (ucen.lvENlist.Items.Count <= 0) return;
            if (enList.ElementAt(enList.Count - 1).IsNodeComment) return;
            editIndex = enList.Count - 1;
            loadValues();
        }
        private void cmbIEDName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "DI : cmbIEDName_SelectedIndexChanged";
            try
            {
                //Namrata: 04/04/2018
                if (ucen.cmbIEDName.Focused == false)
                {

                }
                else
                {
                    Utils.Iec61850IEDname = ucen.cmbIEDName.Text;
                    List<DataTable> dtList = Utils.dsResponseType.Tables.OfType<DataTable>().Where(tbl => tbl.TableName.StartsWith(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname)).ToList();
                    if (dtList.Count == 0)
                    {
                        ucen.cmb61850ResponseType.DataSource = null;
                        ucen.cmb61850Index.DataSource = null;
                        ucen.cmb61850ResponseType.Enabled = false;
                        ucen.cmb61850Index.Enabled = false;
                        //Ajay: 17/01/2019
                        //ucen.txtFC.Text = "";
                        ucen.cmbFC.DataSource = null;
                    }
                    else
                    {
                        ucen.cmb61850ResponseType.Enabled = true;
                        ucen.cmb61850Index.Enabled = true;
                        ucen.cmb61850ResponseType.DataSource = Utils.dsResponseType.Tables[Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname];//[Utils.strFrmOpenproplusTreeNode + "/" + "Undefined" + "/" + Utils.Iec61850IEDname];
                        ucen.cmb61850ResponseType.DisplayMember = "Address";
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
            //string strRoutineName = "DI: cmb61850ResponseType_SelectedIndexChanged";
            //try
            //{
            //    if (ucen.cmb61850ResponseType.Items.Count > 1)
            //    {
            //        if ((ucen.cmb61850ResponseType.SelectedIndex != -1))
            //        {
            //            //Namrata: 04/04/2018
            //            Utils.Iec61850IEDname = ucen.cmbIEDName.Text;
            //            //Utils.Iec61850IEDname = ucai.cmbIEDName.Items.OfType<DataRowView>().Select(x => x.Row[0].ToString()).FirstOrDefault().ToString();
            //            List<DataTable> dtList = Utils.DsAllConfigurationData.Tables.OfType<DataTable>().Where(tbl => tbl.TableName.StartsWith(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname)).ToList();
            //            dsdummy = new DataSet();
            //            dtList.ForEach(tbl => { DataTable dt = tbl.Copy(); dsdummy.Tables.Add(dt); });
            //            //Ajay: 17/01/2019 Commented
            //            //ucen.cmb61850Index.DataSource = dsdummy.Tables[ucen.cmb61850ResponseType.SelectedIndex];
            //            //ucen.cmb61850Index.DisplayMember = "ObjectReferrence";
            //            //ucen.cmb61850Index.ValueMember = "Node";
            //            //Ajay: 17/01/2019
            //            ucen.cmbFC.DataSource = dsdummy.Tables[ucen.cmb61850ResponseType.SelectedIndex].AsEnumerable().Select(x => x.Field<string>("FC")).Distinct().ToList();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            string strRoutineName = "cmb61850ResponseType_SelectedIndexChanged";
            try
            {
                if (ucen.cmb61850ResponseType.Items.Count > 1)
                {
                    if ((ucen.cmb61850ResponseType.SelectedIndex != -1))
                    {
                        Utils.Iec61850IEDname = ucen.cmbIEDName.Text;//Namrata: 04/04/2018
                        List<DataTable> dtList = Utils.DsAllConfigurationData.Tables.OfType<DataTable>().Where(tbl => tbl.TableName.StartsWith(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname)).ToList();
                        dsdummy = new DataSet();
                        dtList.ForEach(tbl => { DataTable dt = tbl.Copy(); dsdummy.Tables.Add(dt); });
                        ucen.cmbFC.DataSource = dsdummy.Tables[ucen.cmb61850ResponseType.SelectedIndex].AsEnumerable().Select(x => x.Field<string>("FC")).Distinct().ToList();//Ajay: 17/01/2019
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
            //string strRoutineName = "DI : cmb61850DIIndex_SelectedIndexChanged";
            //try
            //{
            //    if (ucen.cmb61850Index.Items.Count > 0)
            //    {
            //        if (ucen.cmb61850Index.SelectedIndex != -1)
            //        {
            //            //Ajay: 17/01/2019
            //            //ucen.txtFC.Text = ((DataRowView)ucen.cmb61850Index.SelectedItem).Row[2].ToString();

            //            //ucen.cmbFC.SelectedIndex = ucen.cmbFC.FindStringExact(((DataRowView)ucen.cmb61850Index.SelectedItem).Row[2].ToString());
            //            ucen.cmbFC.SelectedIndex = ucen.cmbFC.Items.IndexOf(((DataRowView)ucen.cmb61850Index.SelectedItem).Row[2].ToString());
            //            cmbFC_SelectedIndexChanged(ucen.cmbFC, null);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            string strRoutineName = "DI : cmbFC_SelectedIndexChanged";
            try
            {
                string FC = ucen.cmbFC.Text;
                DataTable DT = FilteredIndexDT(FC);
                if (DT != null)
                {
                    ucen.cmb61850Index.DataSource = DT;
                    ucen.cmb61850Index.DisplayMember = "ObjectReferrence";
                    ucen.cmb61850Index.ValueMember = "Node";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Namrata:22/03/2019
        private void ChkIEC61850Index_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "EN:cmb61850Index_SelectedIndexChanged";
            try
            {
                if (ucen.ChkIEC61850Index.Items.Count > 0)
                {
                    if (ucen.ChkIEC61850Index.SelectedIndex != -1)
                    {
                        string a = ucen.ChkIEC61850Index.Text;
                        string FindObjRef = MergeList.Where(x => x.Contains(a.ToString() + ",")).Select(x => x).FirstOrDefault();
                        string[] GetFC = FindObjRef.Split(',');
                        ucen.cmbFC.SelectedIndex = ucen.cmbFC.FindStringExact(GetFC[1].ToString());
                        Utils.IEC61850Index = GetFC[0].ToString();
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
            string strRoutineName = "ENConfiguration:cmbFC_SelectedIndexChanged";
            try
            {

                string FC = ucen.cmbFC.Text;
                DataTable DT = FilteredIndexDT(FC);
                if (DT != null)
                {
                    List<string> FC1 = DT.AsEnumerable().Select(x => x[0].ToString()).ToList();//Namrata:25/03/2019
                    ucen.ChkIEC61850Index.Items.Clear();
                    ucen.ChkIEC61850Index.Text = "";//Namrata:25/03/2019
                    foreach (var kv in FC1)
                    {
                        ucen.ChkIEC61850Index.Items.Add(kv.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lnkENMap_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucen btnENMDelete_Click clicked in class!!!");
            // Ajay: 07/12/2018
            if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
            {
                if (AllowSlaveOptionsOnClick(Utils.getSlaveTypes(currentSlave))) { return; }
                else { }
            }

            foreach (ListViewItem listItem in ucen.lvENMap.Items)
            {
                listItem.Checked = true;
            }
            DialogResult result = MessageBox.Show("Do You Want To Delete All Records ? ", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            //DialogResult result = MessageBox.Show(Globals.PROMPT_DELETE_ENTRY, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.No)
            {
                foreach (ListViewItem listItem in ucen.lvENMap.Items)
                {
                    listItem.Checked = false;
                }
                return;
            }
            List<ENMap> slaveENMapList;
            if (!slavesENMapList.TryGetValue(currentSlave, out slaveENMapList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                MessageBox.Show("Error deleting EN map!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            for (int i = ucen.lvENMap.Items.Count - 1; i >= 0; i--)
            {

                Console.WriteLine("*** removing indices: {0}", i);
                slaveENMapList.RemoveAt(i);
                ucen.lvENMap.Items.Clear();
            }
            Console.WriteLine("*** slaveENMapList count: {0} lv count: {1}", slaveENMapList.Count, ucen.lvENMap.Items.Count);
            refreshMapList(slaveENMapList);
        }
        private void lvENlist_DoubleClick(object sender, EventArgs e)
        {
            if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)// Ajay: 07/12/2018
            {
                if (AllowMasterOptionsOnClick(masterType)) { return; }
                else { }
            }
            ucen.txtAutoMapNumber.Text = "0";//Namrata: 10/09/2017
            int ListIndex = ucen.lvENlist.FocusedItem.Index;//Namrata: 07/03/2018
            ListViewItem lvi = ucen.lvENlist.Items[ListIndex];

            Utils.UncheckOthers(ucen.lvENlist, lvi.Index);
            if (enList.ElementAt(lvi.Index).IsNodeComment)
            {
                MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #region All Masters
            if ((masterType == MasterTypes.IEC103) || (masterType == MasterTypes.IEC104) || (masterType == MasterTypes.ADR) || (masterType == MasterTypes.MODBUS))
            {
                IEC103_IEC104_ADR_Modbus_OnDoubleClick();
            }
            if ((masterType == MasterTypes.SPORT))//Ajay: 27/07/2018
            {
                SPORT_OnDoubleClick();//Ajay: 27/07/2018
            }
            if ((masterType == MasterTypes.IEC101))//Ajay: 12/11/2018
            {
                IEC101_OnDoubleClick(); //Ajay: 27/07/2018
            }
            if (masterType == MasterTypes.LoadProfile)//Ajay: 31/07/2018
            {
                LoadProfile_OnDoubleClick();
            }
            if (masterType == MasterTypes.IEC61850Client)
            {
                IEC61850_OnDoubleClick(); FetchComboboxData();
                ucen.cmbIEDName.SelectedIndex = ucen.cmbIEDName.FindStringExact(Utils.Iec61850IEDname);//Namrata: 04/04/2018
            }
            #endregion All Masters
            ucen.grpEN.Visible = true;
            mode = Mode.EDIT;
            editIndex = lvi.Index;

            #region IEC61850Client Master
            ucen.ChkIEC61850Index.Text = "";//Namrata:25/03/2019
            if (IEC61850CheckedList != null)
            {
                ucen.ChkIEC61850Index.CheckBoxItems.ForEach(x =>
                {
                    x.Checked = false; x.CheckState = CheckState.Unchecked;
                });
            }
            if (ucen.ChkIEC61850Index.CheckBoxItems.Count > 0)
            {
                ucen.ChkIEC61850Index.Text = enList[lvi.Index].IEC61850Index.ToString();
            }
            #endregion IEC61850Client Master
            Utils.showNavigation(ucen.grpEN, true);
            loadValues();
            if (ucen.ChkIEC61850Index.CheckBoxItems.Count > 0)
            {
                //Namrata:26/03/2019
                ucen.ChkIEC61850Index.CheckBoxItems.Where(x => x.Text == enList[lvi.Index].IEC61850Index.ToString()).Take(1).ToList().ForEach(x =>
                {
                    x.Checked = true; x.CheckState = CheckState.Checked;
                });
            }
            ucen.cmbResponseType.Focus();
        }
        private void lvENlist_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string strRoutineName = "lvENlist_ItemSelectionChanged";
            try
            {
                if (e.IsSelected)
                {
                    Color GreenColour = Color.FromArgb(34, 217, 0);
                    string diIndex = e.Item.Text;
                    Console.WriteLine("*** selected DI: {0}", diIndex);
                    //Namrata: 27/7/2017
                    ucen.lvENMapItemSelectionChanged -= new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvENMap_ItemSelectionChanged);
                    ucen.lvENMap.SelectedItems.Clear();   //Remove selection from DIMap...
                    ucen.lvENMap.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window); //Namrata: 07/04/2018
                    ucen.lvENMap.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour)); //Namrata: 07/04/2018
                    Utils.highlightListviewItem(diIndex, ucen.lvENMap);
                    //Namrata: 27/7/2017
                    ucen.lvENMap.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);//Namrata: 07/04/2018
                    ucen.lvENlist.SelectedItems.Clear();
                    ucen.lvENlist.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                    ucen.lvENlist.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                    ucen.lvENlist.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);
                    ucen.lvENMapItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvENMap_ItemSelectionChanged);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvENMap_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
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
                    ucen.lvENlistItemSelectionChanged -= new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvENlist_ItemSelectionChanged);
                    ucen.lvENlist.SelectedItems.Clear();   //Remove selection from DIMap...
                    ucen.lvENlist.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window); //Namrata: 07/04/2018
                    ucen.lvENlist.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour)); //Namrata: 07/04/2018
                    Utils.highlightListviewItem(diIndex, ucen.lvENlist);
                    //Namrata:lvAIlist 27/7/2017
                    ucen.lvENlist.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);//Namrata: 07/04/2018
                    ucen.lvENMap.SelectedItems.Clear();
                    ucen.lvENMap.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                    ucen.lvENMap.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                    ucen.lvENMap.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);
                    ucen.lvENlistItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvENlist_ItemSelectionChanged);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        //Ajay: 17/01/2019
        private DataTable FilteredIndexDT(string FC)
        {
            string strRoutineName = "AI : FilteredIndexDT";
            try
            {
                DataTable DT = new DataTable();
                DT.Columns.Add("ObjectReferrence");
                DT.Columns.Add("Node");
                DT.Columns.Add("FC");
                DataRow[] drRwArry = dsdummy.Tables[ucen.cmb61850ResponseType.SelectedIndex].AsEnumerable().Where(x => x.Field<string>("FC") == FC).ToArray();
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
            int iColIndex = ucen.lvENlist.Columns["Description"].Index;
            var query = lstview.Items
                    .Cast<ListViewItem>()
                    .Where(item => item.SubItems[0].Text == ainno).Select(s => s.SubItems[iColIndex].Text).Single();
            return query.ToString();
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
                ucen.ChkIEC61850Index.Items.Clear();
                foreach (var kv in ObjectRef)
                {
                    ucen.ChkIEC61850Index.Items.Add(kv.ToString());
                }
            }
        }
        private void FetchComboboxData()
        {
            //Namrata: 13/03/2018
            ucen.cmbIEDName.DataSource = null;
            List<string> tblNameList = Utils.dsIED.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
            string tblName = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
            //Namrata: 26/04/2018
            if (tblName != null)
            {
                ucen.cmbIEDName.DataSource = Utils.dsIED.Tables[tblName];
                ucen.cmbIEDName.DisplayMember = "IEDName";
                ucen.cmb61850ResponseType.DataSource = Utils.dsResponseType.Tables[tblName];//Namrata: 21/03/2018
                ucen.cmb61850ResponseType.DisplayMember = "Address";
                FetchCheckboxData();//Namrata:27/03/2019
            }
            else
            {
                //MessageBox.Show("ICD File Missing !!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
            }
        }
        private void LinkDeleteConfigue_Click(object sender, EventArgs e)
        {
            // Ajay: 07/12/2018
            if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
            {
                if (AllowMasterOptionsOnClick(masterType)) { return; }
                else { }
            }

            foreach (ListViewItem listItem in ucen.lvENlist.Items)
            {
                listItem.Checked = true;
            }
            DialogResult result = MessageBox.Show("Do You Want To Delete All Records ? ", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            //DialogResult result = MessageBox.Show(Globals.PROMPT_DELETE_ENTRY, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.No)
            {
                foreach (ListViewItem listItem in ucen.lvENlist.Items)
                {
                    listItem.Checked = false;
                }
                return;
            }

            for (int i = ucen.lvENlist.Items.Count - 1; i >= 0; i--)
            {
                Console.WriteLine("*** removing indices: {0}", i);
                deleteENFromMaps(enList.ElementAt(i).ENNo);
                enList.RemoveAt(i);
                ucen.lvENlist.Items.Clear();
            }
            Console.WriteLine("*** enList count: {0} lv count: {1}", enList.Count, ucen.lvENlist.Items.Count);
            refreshList();
            //Refresh map listview...
            refreshCurrentMapList();
        }
       
        private void loadDefaults()
        {
            ucen.txtAutoMapNumber.Text = "1";
            ucen.txtENNo.Text = (Globals.ENNo + 1).ToString();
            ucen.txtSubIndex.Text = "1";
            ucen.txtMultiplier.Text = "1";
            ucen.txtConstant.Text = "0";
            ucen.txtIndex.Text = "1";
            if (masterType == MasterTypes.ADR)
            {
                if (ucen.lvENlist.Items.Count - 1 >= 0)
                {
                    ucen.txtIndex.Text = Convert.ToString(Convert.ToInt32(enList[enList.Count - 1].Index) + 1);
                }
                ucen.cmbResponseType.SelectedIndex = ucen.cmbResponseType.FindStringExact("ADR_EN");
                ucen.txtDescription.Text = "ADR_EN";
            }
            else if (masterType == MasterTypes.IEC101)
            {
                if (ucen.lvENlist.Items.Count - 1 >= 0)
                {
                    ucen.txtIndex.Text = Convert.ToString(Convert.ToInt32(enList[enList.Count - 1].Index) + 1);
                }
                ucen.cmbResponseType.SelectedIndex = ucen.cmbResponseType.FindStringExact("IntegratedTotals");
                ucen.txtDescription.Text = "IEC101_EN";
            }
            else if (masterType == MasterTypes.SPORT)
            {
                if (ucen.lvENlist.Items.Count - 1 >= 0)
                {
                    ucen.txtIndex.Text = Convert.ToString(Convert.ToInt32(enList[enList.Count - 1].Index) + 1);
                }
                //Ajay: 12/11/2018
                if (ucen.cmbResponseType != null && ucen.cmbResponseType.Items.Count > 0)
                {
                    ucen.cmbResponseType.SelectedIndex = 0; // ucen.cmbResponseType.FindStringExact("IntegratedTotals");
                }
                ucen.txtDescription.Text = "SPORT_EN";
            }
            else if (masterType == MasterTypes.IEC104)
            {
                if (ucen.lvENlist.Items.Count - 1 >= 0)
                {
                    ucen.txtIndex.Text = Convert.ToString(Convert.ToInt32(enList[enList.Count - 1].Index) + 1);
                }
                ucen.cmbResponseType.SelectedIndex = ucen.cmbResponseType.FindStringExact("IntegratedTotals");
                ucen.txtDescription.Text = "IEC104_EN";
            }
            else if (masterType == MasterTypes.IEC103)
            {
                if (ucen.lvENlist.Items.Count - 1 >= 0)
                {
                    ucen.txtIndex.Text = Convert.ToString(Convert.ToInt32(enList[enList.Count - 1].Index) + 1);
                }
                ucen.cmbResponseType.SelectedIndex = ucen.cmbResponseType.FindStringExact("Measurand_III");
                ucen.txtDescription.Text = "IEC103_EN";
            }
            else if (masterType == MasterTypes.MODBUS)
            {
                if (ucen.lvENlist.Items.Count - 1 >= 0)
                {
                    //Ajay: 11/06/2018
                    switch (enList[enList.Count - 1].DataType)
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
                            ucen.txtIndex.Text = Convert.ToString(Convert.ToInt32(enList[enList.Count - 1].Index) + 2);
                            break;
                        default:
                            ucen.txtIndex.Text = Convert.ToString(Convert.ToInt32(enList[enList.Count - 1].Index) + 1);
                            break;
                    }
                }
                ucen.cmbResponseType.SelectedIndex = ucen.cmbResponseType.FindStringExact("ReadInputRegister");
                ucen.txtDescription.Text = "MODBUS_EN";
            }
            else if (masterType == MasterTypes.IEC61850Client)
            {
                if (ucen.lvENlist.Items.Count - 1 >= 0)
                {
                    ucen.txtIndex.Text = Convert.ToString(Convert.ToInt32(enList[enList.Count - 1].Index) + 1);
                }
                ucen.cmbResponseType.SelectedIndex = ucen.cmbResponseType.FindStringExact("ReadInputRegister");
                ucen.txtDescription.Text = "IEC61850_EN";
            }
            //Ajay: 31/07/2018
            else if (masterType == MasterTypes.LoadProfile)
            {
                if (ucen.lvENlist.Items.Count - 1 >= 0)
                {
                    ucen.txtIndex.Text = Convert.ToString(Convert.ToInt32(enList[enList.Count - 1].Index) + 1);
                }
                if (ucen.cmbName.Items.Count > 0) { ucen.cmbName.SelectedIndex = 0; }
                if (ucen.cmbAI1.Items.Count > 0) { ucen.cmbAI1.SelectedIndex = 0; }
                if (ucen.cmbEN1.Items.Count > 0) { ucen.cmbEN1.SelectedIndex = 0; }
                ucen.txtDescription.Text = "LoadProfile_EN";
                ucen.chkbxLogEnable.Checked = false;
            }
        }
        private void loadValues()
        {
            EN en = enList.ElementAt(editIndex);
            if (en != null)
            {
                ucen.txtENNo.Text = en.ENNo;
                ucen.cmbResponseType.SelectedIndex = ucen.cmbResponseType.FindStringExact(en.ResponseType);
                ucen.txtIndex.Text = en.Index;
                ucen.txtSubIndex.Text = en.SubIndex;
                ucen.cmbDataType.SelectedIndex = ucen.cmbDataType.FindStringExact(en.DataType);
                ucen.txtMultiplier.Text = en.Multiplier;
                ucen.txtConstant.Text = en.Constant;
                ucen.txtDescription.Text = en.Description;
                ucen.cmbIEDName.SelectedIndex = ucen.cmbIEDName.FindStringExact(Utils.Iec61850IEDname);
                ucen.cmb61850ResponseType.SelectedIndex = ucen.cmb61850ResponseType.FindStringExact(en.IEC61850ResponseType);
                ucen.cmb61850Index.SelectedIndex = ucen.cmb61850Index.FindStringExact(en.IEC61850Index);
                ucen.cmbFC.SelectedIndex = ucen.cmbFC.FindStringExact(en.FC); //Ajay: 17/01/2019
                if (en.EventEnable.ToLower() == "yes")//Ajay: 27/07/2018
                { ucen.chkbxEventEnable.Checked = true; }
                else { ucen.chkbxEventEnable.Checked = false; }
                ucen.cmbName.SelectedIndex = ucen.cmbName.FindStringExact(en.Name);//Ajay: 31/07/2018
                ucen.cmbAI1.SelectedIndex = ucen.cmbAI1.FindStringExact(en.AI1);
                ucen.cmbEN1.SelectedIndex = ucen.cmbEN1.FindStringExact(en.EN1);
                if (en.LogEnable.ToLower() == "yes")
                { ucen.chkbxLogEnable.Checked = true; }
                else { ucen.chkbxLogEnable.Checked = false; }
                ucen.ChkIEC61850Index.SelectedIndex = ucen.ChkIEC61850Index.FindStringExact(en.IEC61850Index);//Namrata:27/03/2019
            }
        }
        private bool Validate()
        {
            bool status = true;
            //Check empty field's
            if (Utils.IsEmptyFields(ucen.grpEN))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return status;
        }
        private void refreshList()
        {
            int cnt = 0;
            ucen.lvENlist.Items.Clear();
            Utils.ENlistforDescription.Clear();

            if((masterType == MasterTypes.ADR)|| (masterType == MasterTypes.IEC103) ||(masterType == MasterTypes.MODBUS)|| (masterType == MasterTypes.Virtual))
            {
                foreach (EN en in enList)
                {
                    string[] row = new string[9];
                    if (en.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = en.ENNo;
                        row[1] = en.ResponseType;
                        row[2] = en.Index;
                        row[3] = en.SubIndex;
                        row[4] = en.DataType;
                        row[5] = en.Multiplier;
                        row[6] = en.Constant;
                        row[7] = en.EventEnable; //Ajay: 02/09/2018
                        row[8] = en.Description;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucen.lvENlist.Items.Add(lvItem);
                }
            }
            //Ajay: 27/07/2018
            //if ((masterType == MasterTypes.SPORT) || (masterType == MasterTypes.IEC104) || (masterType == MasterTypes.IEC101))
            if ((masterType == MasterTypes.SPORT))
            {
                foreach (EN en in enList)
                {
                    string[] row = new string[9];
                    if (en.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = en.ENNo;
                        row[1] = en.ResponseType;
                        row[2] = en.Index;
                        row[3] = en.SubIndex;
                        row[4] = en.DataType;
                        row[5] = en.Multiplier;
                        row[6] = en.Constant;
                        row[7] = en.EventEnable; //Ajay: 02/09/2018
                        row[8] = en.Description;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucen.lvENlist.Items.Add(lvItem);
                }
            }
            //Ajay: 12/11/2018
            if ((masterType == MasterTypes.IEC101))
            {
                foreach (EN en in enList)
                {
                    string[] row = new string[9];
                    if (en.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = en.ENNo;
                        row[1] = en.ResponseType;
                        row[2] = en.Index;
                        row[3] = en.SubIndex;
                        row[4] = en.DataType;
                        row[5] = en.Multiplier;
                        row[6] = en.Constant;
                        row[7] = en.EventEnable; //Ajay: 02/09/2018
                        row[8] = en.Description;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucen.lvENlist.Items.Add(lvItem);
                }
            }
            //Ajay: 27/07/2018
            if (masterType == MasterTypes.IEC104)
            {
                foreach (EN en in enList)
                {
                    string[] row = new string[8];
                    if (en.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = en.ENNo;
                        row[1] = en.ResponseType;
                        row[2] = en.Index;
                        row[3] = en.SubIndex;
                        row[4] = en.Multiplier;
                        row[5] = en.Constant;
                        row[6] = en.EventEnable; //Ajay: 27/07/2018 Ebent added
                        row[7] = en.Description;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucen.lvENlist.Items.Add(lvItem);
                }
            }
            //Ajay: 31/07/2018
            if (masterType == MasterTypes.LoadProfile)
            {
                foreach (EN en in enList)
                {
                    string[] row = new string[6];
                    if (en.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = en.ENNo;
                        row[1] = en.Name;
                        row[2] = en.AI1;
                        row[3] = en.EN1;
                        row[4] = en.EventEnable;
                        //row[5] = en.LogEnable; //Ajay: 19/09/2018 commented
                        row[5] = en.Description;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucen.lvENlist.Items.Add(lvItem);
                }
            }
            if (masterType == MasterTypes.IEC61850Client)
            {
                foreach (EN en in enList)
                {
                    string[] row = new string[10];
                    if (en.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = en.ENNo;
                        row[1] = en.IEDName;
                        row[2] = en.IEC61850ResponseType;
                        row[3] = en.IEC61850Index;
                        row[4] = en.FC;
                        row[5] = en.SubIndex;
                        row[6] = en.Multiplier;
                        row[7] = en.Constant;
                        row[8] = en.EventEnable;
                        row[9] = en.Description;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucen.lvENlist.Items.Add(lvItem);
                }
            }
          
            ucen.lblENRecords.Text = enList.Count.ToString();
            Utils.EnlistRegenerateIndex.AddRange(enList);
            Utils.ENlistforDescription.AddRange(enList);
        }
        private int getMaxENs()
        {
            if (masterType == MasterTypes.IEC103) return Globals.MaxIEC103EN;
            else if (masterType == MasterTypes.MODBUS) return Globals.MaxMODBUSEN;
            //Namarta:13/7/2107
            else if (masterType == MasterTypes.IEC101) return Globals.MaxIEC101EN;
            else if (masterType == MasterTypes.ADR) return Globals.MaxADREN;
            else if (masterType == MasterTypes.IEC61850Client) return Globals.Max61850EN;
            else if (masterType == MasterTypes.IEC104) return Globals.MaxIEc104MasterEN;
            else if (masterType == MasterTypes.SPORT) return Globals.MaxSPORTMasterEN;
            else if (masterType == MasterTypes.LoadProfile) return Globals.MaxLoadProfileEN; //Ajay: 31/07/2018
            else return 0;
        }
        private bool IsVerified(ComboBox cmb, string param, out string[] str)
        {
            str = new string[2];
            string strRoutineName = "IsVerified()";
            try
            {
                int iParam = 0;
                int.TryParse(cmb.Text, out iParam);
                if (param.Contains("AI"))
                {
                    if (!Utils.GetAllAIList().Contains(iParam))
                    {
                        str[0] = param; str[1] = iParam.ToString(); return false;
                    }
                }
                else if (param.Contains("EN"))
                {
                    if (!Utils.GetAllENList().Contains(iParam))
                    {
                        str[0] = param; str[1] = iParam.ToString(); return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
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
        /* ============================================= Below this, EN Map logic... ============================================= */
        private void CreateNewSlave(string slaveNum, string slaveID, XmlNode enmNode)
        {
            List<ENMap> senmList = new List<ENMap>();
            slavesENMapList.Add(slaveID, senmList);
            if (enmNode != null)
            {
                foreach (XmlNode node in enmNode)
                {
                    //Console.WriteLine("***** node type: {0}", node.NodeType);
                    if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                    senmList.Add(new ENMap(node, Utils.getSlaveTypes(slaveID)));
                }
            }
            AddMap2SlaveButton(Int32.Parse(slaveNum), slaveID);
            //Namrata: 24/02/2018
            //If Description attribute not exist in XML 
            senmList.AsEnumerable().ToList().ForEach(item =>
            {
                string strDONo = item.ENNo;
                if (string.IsNullOrEmpty(item.Description)) //Ajay: 05/01/2018 
                {
                    item.Description = Utils.ENlistforDescription.AsEnumerable().Where(x => x.ENNo == strDONo).Select(x => x.Description).FirstOrDefault();
                }
            });
            refreshMapList(senmList);
            currentSlave = slaveID;
        }
        private void DeleteSlave(string slaveID)
        {
            slavesENMapList.Remove(slaveID);
            RadioButton rb = null;
            foreach (Control ctrl in ucen.flpMap2Slave.Controls)
            {
                if (ctrl.Tag.ToString() == slaveID)
                {
                    rb = (RadioButton)ctrl;
                    break;
                }
            }
            if (rb != null) ucen.flpMap2Slave.Controls.Remove(rb);
        }
        //Namrata:13/7/2017
        private void CheckIEC101SlaveStatusChanges()
        {
            foreach (IEC101Slave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().getIEC101SlaveGroup().getIEC101Slaves())
            {
                string slaveID = "IEC101Slave_" + slvMB.SlaveNum;
                bool slaveAdded = true;
                foreach (KeyValuePair<string, List<ENMap>> sn in slavesENMapList)
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
            foreach (KeyValuePair<string, List<ENMap>> sain in slavesENMapList)//Loop thru slaves...
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
                if (ucen.flpMap2Slave.Controls.Count > 0)
                {
                    ((RadioButton)ucen.flpMap2Slave.Controls[0]).Checked = true;
                    currentSlave = ((RadioButton)ucen.flpMap2Slave.Controls[0]).Tag.ToString();
                    refreshCurrentMapList();
                }
                else
                {
                    ucen.lvENMap.Items.Clear();
                    currentSlave = "";
                }
            }

            fillMapOptions(Utils.getSlaveTypes(currentSlave));
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
                    foreach (KeyValuePair<string, List<ENMap>> sn in slavesENMapList)
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
                foreach (KeyValuePair<string, List<ENMap>> sn in slavesENMapList)
                {
                    string slaveID = sn.Key;
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
                    if (ucen.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucen.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucen.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucen.lvENMap.Items.Clear();
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
            string strRoutineName = "DIConfiguration:CheckDNPSlaveStatusChanges";
            try
            {
                //Check for slave addition...
                foreach (DNP3Settings dnp3 in Utils.getOpenProPlusHandle().getSlaveConfiguration().getDNPSlaveGroup().getDNPSlaves())//Loop thru slaves...
                {
                    string slaveID = "DNP3Slave_" + dnp3.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<ENMap>> sn in slavesENMapList)
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
                foreach (KeyValuePair<string, List<ENMap>> sn1 in slavesENMapList)
                {
                    string slaveID = sn1.Key;
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
                    if (ucen.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucen.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucen.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucen.lvENMap.Items.Clear();
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
            string strRoutineName = "ENConfiguration:CheckSMSSlaveStatusChanges";
            try
            {

                //Check for slave addition...
                foreach (SMSSlave slvSMS in Utils.getOpenProPlusHandle().getSlaveConfiguration().getSMSSlaveGroup().getSMSSlaves())
                {
                    string slaveID = "SMSSlave_" + slvSMS.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<ENMap>> sn in slavesENMapList)
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
                foreach (KeyValuePair<string, List<ENMap>> sain in slavesENMapList)//Loop thru slaves...
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
                    if (ucen.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucen.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucen.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucen.lvENMap.Items.Clear();
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
            string strRoutineName = "ENConfiguration:CheckMQTTSlaveStatusChanges";
            try
            {

                //Check for slave addition...
                foreach (MQTTSlave slvMQTT in Utils.getOpenProPlusHandle().getSlaveConfiguration().getMQTTSlaveGroup().getMQTTSlaves())
                {
                    string slaveID = "MQTTSlave_" + slvMQTT.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<ENMap>> sn in slavesENMapList)
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
                foreach (KeyValuePair<string, List<ENMap>> sain in slavesENMapList)//Loop thru slaves...
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
                    if (ucen.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucen.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucen.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucen.lvENMap.Items.Clear();
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
            foreach (IEC104Slave slv104 in Utils.getOpenProPlusHandle().getSlaveConfiguration().getIEC104Group().getIEC104Slaves())//Loop thru slaves...
            {
                string slaveID = "IEC104_" + slv104.SlaveNum;
                bool slaveAdded = true;
                foreach (KeyValuePair<string, List<ENMap>> sn in slavesENMapList)
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
            foreach (KeyValuePair<string, List<ENMap>> senn in slavesENMapList)//Loop thru slaves...
            {
                string slaveID = senn.Key;
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
                if (ucen.flpMap2Slave.Controls.Count > 0)
                {
                    ((RadioButton)ucen.flpMap2Slave.Controls[0]).Checked = true;
                    currentSlave = ((RadioButton)ucen.flpMap2Slave.Controls[0]).Tag.ToString();
                    refreshCurrentMapList();
                }
                else
                {
                    ucen.lvENMap.Items.Clear();
                    currentSlave = "";
                }
            }

            fillMapOptions(Utils.getSlaveTypes(currentSlave));
        }
        private void CheckMODBUSSlaveStatusChanges()
        {
            foreach (MODBUSSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().getMODBUSSlaveGroup().getMODBUSSlaves())//Loop thru slaves...
            {
                string slaveID = "MODBUSSlave_" + slvMB.SlaveNum;
                bool slaveAdded = true;
                foreach (KeyValuePair<string, List<ENMap>> sn in slavesENMapList)
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
            foreach (KeyValuePair<string, List<ENMap>> senn in slavesENMapList)//Loop thru slaves...
            {
                string slaveID = senn.Key;
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
                if (ucen.flpMap2Slave.Controls.Count > 0)
                {
                    ((RadioButton)ucen.flpMap2Slave.Controls[0]).Checked = true;
                    currentSlave = ((RadioButton)ucen.flpMap2Slave.Controls[0]).Tag.ToString();
                    refreshCurrentMapList();
                }
                else
                {
                    ucen.lvENMap.Items.Clear();
                    currentSlave = "";
                }
            }

            fillMapOptions(Utils.getSlaveTypes(currentSlave));
        }
        private void CheckIEC61850SlaveStatusChanges()
        {
            foreach (IEC61850ServerSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().get61850SlaveGroup().getMODBUSSlaves())//Loop thru slaves...
            {
                string slaveID = "IEC61850Server_" + slvMB.SlaveNum; //61850ServerSlaveGroup_
                bool slaveAdded = true;
                foreach (KeyValuePair<string, List<ENMap>> sn in slavesENMapList)
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
            foreach (KeyValuePair<string, List<ENMap>> senn in slavesENMapList)//Loop thru slaves...
            {
                string slaveID = senn.Key;
                bool slaveDeleted = true;
                if (Utils.getSlaveTypes(slaveID) != SlaveTypes.IEC61850Server) continue;
                foreach (IEC61850ServerSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().get61850SlaveGroup().getMODBUSSlaves())//Loop thru slaves...
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
                if (ucen.flpMap2Slave.Controls.Count > 0)
                {
                    ((RadioButton)ucen.flpMap2Slave.Controls[0]).Checked = true;
                    currentSlave = ((RadioButton)ucen.flpMap2Slave.Controls[0]).Tag.ToString();
                    refreshCurrentMapList();
                }
                else
                {
                    ucen.lvENMap.Items.Clear();
                    currentSlave = "";
                }
            }

            fillMapOptions(Utils.getSlaveTypes(currentSlave));
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
                    foreach (KeyValuePair<string, List<ENMap>> sn in slavesENMapList)
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
                foreach (KeyValuePair<string, List<ENMap>> sain in slavesENMapList)//Loop thru slaves...
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
                    if (ucen.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucen.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucen.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucen.lvENMap.Items.Clear();
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
        private void deleteENFromMaps(string enNo)
        {
            foreach (KeyValuePair<string, List<ENMap>> senn in slavesENMapList)//Loop thru slaves...
            {
                List<ENMap> delEntry = senn.Value;
                foreach (ENMap enmn in delEntry)
                {
                    if (enmn.ENNo == enNo)
                    {
                        delEntry.Remove(enmn);
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
            //--------------------------//
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
            //rb.Padding = new Padding(0, 0, 0, 0);
            rb.TextAlign = ContentAlignment.TopCenter;
            rb.BackColor = ColorTranslator.FromHtml("#f2f2f2");
            rb.Appearance = Appearance.Button;
            rb.AutoSize = true;
            rb.Image = Properties.Resources.SlaveRadioButton;
            rb.Click += rbGrpMap2Slave_Click;

            ucen.flpMap2Slave.Controls.Add(rb);
            rb.Checked = true;
        }
        private string getMQTTSlaveKey(ListView lstview, string donno)
        {
            int iColIndex = ucen.lvENlist.Columns["Description"].Index;
            var query = lstview.Items
                    .Cast<ListViewItem>()
                    .Where(item => item.SubItems[0].Text == donno).Select(s => s.SubItems[iColIndex].Text).Single();
            return query.ToString();
        }
        private void rbGrpMap2Slave_Click(object sender, EventArgs e)
        {
            ucen.grpENMap.Visible = false; //Ajay: 07/12/2018
            bool rbChanged = false;
            RadioButton currRB = ((RadioButton)sender);
            if (currentSlave != currRB.Tag.ToString())
            {
                currentSlave = currRB.Tag.ToString();
                rbChanged = true;
                refreshCurrentMapList();
                if (ucen.lvENlist.SelectedItems.Count > 0)
                {
                    //If possible highlight the map for new slave selected...
                    //Remove selection from ENMap...
                    ucen.lvENMap.SelectedItems.Clear();
                    Utils.highlightListviewItem(ucen.lvENlist.SelectedItems[0].Text, ucen.lvENMap);
                }
            }

            ShowHideSlaveColumns();
            ShowHideSlaveColumnsSPORT();
            ShowHideSlaveColumnsMQTT();
            ShowHideSlaveColumnsDNP();
            // Ajay: 07/12/2018
            if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
            {
                if (AllowSlaveOptionsOnClick(Utils.getSlaveTypes(currentSlave))) { return; }
                else { }
            }

            if (rbChanged && ucen.lvENlist.CheckedItems.Count <= 0) return;//We supported map listing only

            EN mapEN = null;

            if (ucen.lvENlist.CheckedItems.Count != 1)
            {
                MessageBox.Show("Select single EN element to map!!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            for (int i = 0; i < ucen.lvENlist.Items.Count; i++)
            {
                if (ucen.lvENlist.Items[i].Checked)
                {
                    Console.WriteLine("*** Mapping index: {0}", i);
                    mapEN = enList.ElementAt(i);
                    ucen.lvENlist.Items[i].Checked = false;//Now we can uncheck in listview...
                    break;
                }
            }
            if (mapEN == null) return;

            //Search if already mapped for current slave...
            bool alreadyMapped = false;
            List<ENMap> slaveENMapList;
            if (!slavesENMapList.TryGetValue(currentSlave, out slaveENMapList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                MessageBox.Show("Slave entry doesnot exist!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
                //Do it from elsewhere...
                //slaveENMapList = new List<ENMap>();
                //slavesENMapList.Add(currentSlave, slaveENMapList);
            }
            else
            {
                Console.WriteLine("##### Slave entries exists");
            }
            if (!alreadyMapped)
            {
                mapMode = Mode.ADD;
                mapEditIndex = -1;
                Utils.resetValues(ucen.grpENMap);
                ucen.txtMapENNo.Text = mapEN.ENNo;
                ucen.txtMapDescription.Text = getDescription(ucen.lvENlist, mapEN.ENNo.ToString());
                //Namrata:1/7/2017
                ucen.txtMapAutoMap.Text = "1";
                loadMapDefaults();
                if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC101SLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC104))
                {
                    //Ajay: 24/09/2018 Modified. Mail by Aditya K dtd. 24/09/2018
                    foreach (Control c in ucen.grpENMap.Controls)
                    {
                        if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                        {
                            if (c.Name.Contains("lblMapHdrText")) { }
                            else { c.Visible = false; }
                        }
                        else { }
                    }

                    ucen.lblEV.Visible = false;
                    ucen.CmbEV.Visible = false;
                    ucen.lblEventClass.Visible = false;
                    ucen.cmbEventC.Visible = false;
                    ucen.lblVariation.Visible = false;
                    ucen.CmbVari.Visible = false;

                    ucen.lblMapENNo.Visible = true;
                    ucen.txtMapENNo.Visible = true;
                    ucen.lblMapENNo.Location = new Point(13, 33);
                    ucen.txtMapENNo.Location = new Point(115, 30);

                    ucen.lblMapReportingIndex.Visible = true;
                    ucen.txtMapReportingIndex.Visible = true;
                    ucen.lblMapReportingIndex.Location = new Point(13, 58);
                    ucen.txtMapReportingIndex.Location = new Point(115, 55);

                    ucen.lblMapDataType.Visible = true;
                    ucen.cmbMapDataType.Visible = true;
                    ucen.lblMapDataType.Location = new Point(13, 83);
                    ucen.cmbMapDataType.Location = new Point(115, 80);

                    ucen.lblMapCmdType.Visible = true;
                    ucen.cmbMapCommandType.Visible = true;
                    ucen.lblMapCmdType.Location = new Point(13, 109);
                    ucen.cmbMapCommandType.Location = new Point(115, 106);

                    ucen.lblMapDeadband.Visible = true;
                    ucen.txtMapDeadBand.Visible = true;
                    ucen.lblMapDeadband.Location = new Point(13, 135);
                    ucen.txtMapDeadBand.Location = new Point(115, 132);

                    ucen.lblMapMultiplier.Visible = true;
                    ucen.txtMapMultiplier.Visible = true;
                    ucen.lblMapMultiplier.Location = new Point(13, 161);
                    ucen.txtMapMultiplier.Location = new Point(115, 158);

                    ucen.lblMapConstant.Visible = true;
                    ucen.txtMapConstant.Visible = true;
                    ucen.lblMapConstant.Location = new Point(13, 187);
                    ucen.txtMapConstant.Location = new Point(115, 184);

                    ucen.lblMapDesc.Visible = true;
                    ucen.txtMapDescription.Visible = true;
                    ucen.lblMapDesc.Location = new Point(13, 213);
                    ucen.txtMapDescription.Location = new Point(115, 210);

                    ucen.lblMapAutoMap.Visible = true;
                    ucen.txtMapAutoMap.Visible = true;
                    ucen.lblMapAutoMap.Location = new Point(13, 239);
                    ucen.txtMapAutoMap.Location = new Point(115, 236);
                    ucen.btnENMDone.Visible = true;
                    ucen.btnENMCancel.Visible = true;
                    ucen.btnENMDone.Location = new Point(117, 269);
                    ucen.btnENMCancel.Location = new Point(203, 269);

                    ucen.grpENMap.Size = new Size(301, 309);
                }
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MQTTSLAVE)
                {
                    ucen.txtKey.Text = getMQTTSlaveKey(ucen.lvENlist, mapEN.ENNo);
                    //Ajay: 24/09/2018 Modified. Mail by Aditya K dtd. 24/09/2018
                    foreach (Control c in ucen.grpENMap.Controls)
                    {
                        if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                        {
                            if (c.Name.Contains("lblMapHdrText")) { }
                            else { c.Visible = false; }
                        }
                        else { }
                    }


                    ucen.lblEV.Visible = false;
                    ucen.CmbEV.Visible = false;
                    ucen.lblEventClass.Visible = false;
                    ucen.cmbEventC.Visible = false;
                    ucen.lblVariation.Visible = false;
                    ucen.CmbVari.Visible = false;

                    ucen.lblMapENNo.Visible = true;
                    ucen.txtMapENNo.Visible = true;
                    ucen.lblMapENNo.Location = new Point(13, 33);
                    ucen.txtMapENNo.Location = new Point(115, 30);

                    ucen.lblMapReportingIndex.Visible = true;
                    ucen.txtMapReportingIndex.Visible = true;
                    ucen.lblMapReportingIndex.Location = new Point(13, 58);
                    ucen.txtMapReportingIndex.Location = new Point(115, 55);

                    ucen.lblMapDataType.Visible = true;
                    ucen.cmbMapDataType.Visible = true;
                    ucen.lblMapDataType.Location = new Point(13, 83);
                    ucen.cmbMapDataType.Location = new Point(115, 80);

                    ucen.lblMapCmdType.Visible = true;
                    ucen.cmbMapCommandType.Visible = true;
                    ucen.lblMapCmdType.Location = new Point(13, 109);
                    ucen.cmbMapCommandType.Location = new Point(115, 106);

                    ucen.lblMapDeadband.Visible = true;
                    ucen.txtMapDeadBand.Visible = true;
                    ucen.lblMapDeadband.Location = new Point(13, 135);
                    ucen.txtMapDeadBand.Location = new Point(115, 132);

                    ucen.lblMapMultiplier.Visible = true;
                    ucen.txtMapMultiplier.Visible = true;
                    ucen.lblMapMultiplier.Location = new Point(13, 161);
                    ucen.txtMapMultiplier.Location = new Point(115, 158);

                    ucen.lblMapConstant.Visible = true;
                    ucen.txtMapConstant.Visible = true;
                    ucen.lblMapConstant.Location = new Point(13, 187);
                    ucen.txtMapConstant.Location = new Point(115, 184);

                    ucen.lblUnit.Visible = true;
                    ucen.txtUnit.Visible = true;
                    ucen.lblUnit.Location = new Point(13, 213);
                    ucen.txtUnit.Location = new Point(115, 210);
                    ucen.lblUnit.Visible = true;
                    ucen.txtUnit.Visible = true;
                    ucen.lblKey.Visible = true;
                    ucen.txtKey.Visible = true;
                    ucen.lblKey.Location = new Point(13, 239);
                    ucen.txtKey.Location = new Point(115, 236);




                    ucen.lblMapDesc.Visible = true;
                    ucen.txtMapDescription.Visible = true;
                    ucen.lblMapDesc.Location = new Point(13, 259);
                    ucen.txtMapDescription.Location = new Point(115, 262);

                    ucen.lblMapAutoMap.Visible = true;
                    ucen.txtMapAutoMap.Visible = true;
                    ucen.lblMapAutoMap.Location = new Point(13, 285);
                    ucen.txtMapAutoMap.Location = new Point(115, 288);

                    ucen.btnENMDone.Visible = true;
                    ucen.btnENMCancel.Visible = true;
                    ucen.btnENMDone.Location = new Point(117, 320);
                    ucen.btnENMCancel.Location = new Point(203, 320);
                    ucen.grpENMap.Location = new Point(750, 270);
                    ucen.grpENMap.Size = new Size(301, 352);
                }
                if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE))
                {

                    ucen.lblEV.Visible = false;
                    ucen.CmbEV.Visible = false;
                    ucen.lblEventClass.Visible = false;
                    ucen.cmbEventC.Visible = false;
                    ucen.lblVariation.Visible = false;
                    ucen.CmbVari.Visible = false;

                    //Ajay: 07/09/2018
                    ucen.chkbxEvent.Visible = false;

                    ucen.lblMapUnitID.Visible = false;
                    ucen.txtMapUnitID.Visible = false;
                    ucen.chkUsed.Visible = false;
                    //ucen.grpENMap.Size = new Size(301, 307);
                    //ucen.grpENMap.Location = new Point(583, 310);
                    //ucen.lblAutoMapM.Visible = true;
                    //ucen.txtENAutoMapNumber.Visible = true;
                    //ucen.btnENMDone.Location = new Point(116, 267);
                    //ucen.btnENMCancel.Location = new Point(203, 267);

                    ucen.lblMapENNo.Location = new Point(13, 33);
                    ucen.txtMapENNo.Location = new Point(115, 30);

                    ucen.lblMapReportingIndex.Location = new Point(13, 58);
                    ucen.txtMapReportingIndex.Location = new Point(115, 55);
                    

                    ucen.lblMapDataType.Location = new Point(13, 83);
                    ucen.cmbMapDataType.Location = new Point(115, 80);

                    ucen.lblMapCmdType.Location = new Point(13, 109);
                    ucen.cmbMapCommandType.Location = new Point(115, 106);
                    ucen.lblMapCmdType.Visible = true;
                    ucen.cmbMapCommandType.Visible = true;


                    ucen.lblMapDeadband.Visible = true;
                    ucen.txtMapDeadBand.Visible = true;

                    ucen.lblMapMultiplier.Location = new Point(13, 135);
                    ucen.txtMapMultiplier.Location = new Point(115, 132);

                    ucen.lblMapConstant.Location = new Point(13, 160);
                    ucen.txtMapConstant.Location = new Point(115, 157);

                    ucen.lblMapDesc.Location = new Point(13, 185);
                    ucen.txtMapDescription.Location = new Point(115, 182);

                    ucen.lblMapAutoMap.Location = new Point(13, 210);
                    ucen.txtMapAutoMap.Location = new Point(115, 207);

                    ucen.chkUsed.Visible = false;


                    ucen.btnENMDone.Location = new Point(117, 240);
                    ucen.btnENMCancel.Location = new Point(203, 240);

                    ucen.grpENMap.Size = new Size(301, 280);
                }
                if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.SPORTSLAVE))
                {
                    ucen.lblEV.Visible = false;
                    ucen.CmbEV.Visible = false;
                    ucen.lblEventClass.Visible = false;
                    ucen.cmbEventC.Visible = false;
                    ucen.lblVariation.Visible = false;
                    ucen.CmbVari.Visible = false;

                    //Ajay: 07/09/2018
                    ucen.chkbxEvent.Visible = true;
                    ucen.chkbxEvent.Location = new Point(15, 257);

                    ucen.lblMapENNo.Location = new Point(13, 33);
                    ucen.txtMapENNo.Location = new Point(115, 30);

                    ucen.lblMapUnitID.Location = new Point(13, 58);
                    ucen.txtMapUnitID.Location = new Point(115, 55);
                    ucen.lblMapUnitID.Visible = true;
                    ucen.txtMapUnitID.Visible = true;

                    ucen.lblMapReportingIndex.Location = new Point(13, 83);
                    ucen.txtMapReportingIndex.Location = new Point(115, 80);

                    ucen.lblMapDataType.Location = new Point(13, 109);
                    ucen.cmbMapDataType.Location = new Point(115, 106);

                    ucen.lblMapCmdType.Visible = false;
                    ucen.cmbMapCommandType.Visible = false;

                    ucen.lblMapDeadband.Location = new Point(13, 135);
                    ucen.txtMapDeadBand.Location = new Point(115, 132);

                    ucen.lblMapMultiplier.Location = new Point(13, 160);
                    ucen.txtMapMultiplier.Location = new Point(115, 157);

                    ucen.lblMapConstant.Location = new Point(13, 185);
                    ucen.txtMapConstant.Location = new Point(115, 182);

                    ucen.lblMapDesc.Location = new Point(13, 210);
                    ucen.txtMapDescription.Location = new Point(115, 207);

                    ucen.lblMapAutoMap.Location = new Point(13, 235);
                    ucen.txtMapAutoMap.Location = new Point(115, 232);

                    ucen.chkUsed.Visible = true;
                    ucen.chkUsed.Location = new Point(115, 257);

                    ucen.btnENMDone.Location = new Point(117, 285);
                    ucen.btnENMCancel.Location = new Point(203, 285);

                    ucen.grpENMap.Size = new Size(301, 328);


                }
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE) ucen.cmbMapCommandType.Enabled = true;
                else ucen.cmbMapCommandType.Enabled = false;
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.SMSSLAVE)
                {
                    //Ajay: 24/09/2018 Modified. Mail by Aditya K dtd. 24/09/2018
                    foreach (Control c in ucen.grpENMap.Controls)
                    {
                        if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                        {
                            if (c.Name.Contains("lblMapHdrText")) { }
                            else { c.Visible = false; }
                        }
                        else { }
                    }
                    ucen.lblEV.Visible = false;
                    ucen.CmbEV.Visible = false;
                    ucen.lblEventClass.Visible = false;
                    ucen.cmbEventC.Visible = false;
                    ucen.lblVariation.Visible = false;
                    ucen.CmbVari.Visible = false;

                    ucen.lblUnit.Visible = false;
                    ucen.txtUnit.Visible = false;
                    ucen.lblKey.Visible = false;
                    ucen.txtKey.Visible = false;
                    ucen.lblMapENNo.Visible = true;
                    ucen.txtMapENNo.Visible = true;
                    ucen.lblMapENNo.Location = new Point(13, 33);
                    ucen.txtMapENNo.Location = new Point(115, 30);

                    ucen.lblMapReportingIndex.Visible = true;
                    ucen.txtMapReportingIndex.Visible = true;
                    ucen.lblMapReportingIndex.Location = new Point(13, 58);
                    ucen.txtMapReportingIndex.Location = new Point(115, 55);

                    ucen.lblMapDataType.Visible = true;
                    ucen.cmbMapDataType.Visible = true;
                    ucen.lblMapDataType.Location = new Point(13, 83);
                    ucen.cmbMapDataType.Location = new Point(115, 80);

                    ucen.lblMapCmdType.Visible = false;
                    ucen.cmbMapCommandType.Visible = false;


                    ucen.lblMapDeadband.Visible = true;
                    ucen.txtMapDeadBand.Visible = true;
                    ucen.lblMapDeadband.Location = new Point(13, 109);
                    ucen.txtMapDeadBand.Location = new Point(115, 106);

                    ucen.lblMapMultiplier.Visible = true;
                    ucen.txtMapMultiplier.Visible = true;
                    ucen.lblMapMultiplier.Location = new Point(13, 135);
                    ucen.txtMapMultiplier.Location = new Point(115, 132);

                    ucen.lblMapConstant.Visible = true;
                    ucen.txtMapConstant.Visible = true;
                    ucen.lblMapConstant.Location = new Point(13, 161);
                    ucen.txtMapConstant.Location = new Point(115, 158);


                    ucen.lblUnit.Visible = true;
                    ucen.txtUnit.Visible = true;
                    ucen.lblUnit.Location = new Point(13, 187);
                    ucen.txtUnit.Location = new Point(115, 184);

                    ucen.lblMapDesc.Visible = true;
                    ucen.txtMapDescription.Visible = true;
                    ucen.lblMapDesc.Location = new Point(13, 213);
                    ucen.txtMapDescription.Location = new Point(115, 210);

                    ucen.lblMapAutoMap.Visible = true;
                    ucen.txtMapAutoMap.Visible = true;
                    ucen.lblMapAutoMap.Location = new Point(13, 239);
                    ucen.txtMapAutoMap.Location = new Point(115, 236);

                    ucen.chkbxEvent.Visible = true;
                    ucen.chkbxEvent.Location = new Point(115, 262);

                    ucen.btnENMDone.Visible = true;
                    ucen.btnENMCancel.Visible = true;
                    ucen.btnENMDone.Location = new Point(117, 290);
                    ucen.btnENMCancel.Location = new Point(203, 290);

                    ucen.grpENMap.Size = new Size(301, 325);
                    ucen.grpENMap.Location = new Point(245, 304);
                }

                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE)
                {
                    //Ajay: 24/09/2018 Modified. Mail by Aditya K dtd. 24/09/2018
                    foreach (Control c in ucen.grpENMap.Controls)
                    {
                        if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                        {
                            if (c.Name.Contains("lblMapHdrText")) { }
                            else { c.Visible = false; }
                        }
                        else { }
                    }

                   

                    ucen.lblMapENNo.Visible = true;
                    ucen.txtMapENNo.Visible = true;
                    ucen.lblMapENNo.Location = new Point(13, 33);
                    ucen.txtMapENNo.Location = new Point(115, 30);

                    ucen.lblMapReportingIndex.Visible = true;
                    ucen.txtMapReportingIndex.Visible = true;
                    ucen.lblMapReportingIndex.Location = new Point(13, 58);
                    ucen.txtMapReportingIndex.Location = new Point(115, 55);

                    ucen.lblMapDataType.Visible = false;
                    ucen.cmbMapDataType.Visible = false;

                    ucen.lblMapCmdType.Visible = true;
                    ucen.cmbMapCommandType.Enabled = true;
                    ucen.cmbMapCommandType.Visible = true;
                    ucen.lblMapCmdType.Location = new Point(13, 83);
                    ucen.cmbMapCommandType.Location = new Point(115, 80);

                    ucen.lblMapDeadband.Visible = true;
                    ucen.txtMapDeadBand.Visible = true;
                    ucen.lblMapDeadband.Location = new Point(13, 109);
                    ucen.txtMapDeadBand.Location = new Point(115, 106);

                    ucen.lblMapMultiplier.Visible = true;
                    ucen.txtMapMultiplier.Visible = true;
                    ucen.lblMapMultiplier.Location = new Point(13, 135);
                    ucen.txtMapMultiplier.Location = new Point(115, 132);

                    ucen.lblMapConstant.Visible = true;
                    ucen.txtMapConstant.Visible = true;
                    ucen.lblMapConstant.Location = new Point(13, 161);
                    ucen.txtMapConstant.Location = new Point(115, 158);

                    ucen.lblEV.Visible = true;
                    ucen.CmbEV.Visible = true;
                    ucen.lblEV.Location = new Point(13, 187);
                    ucen.CmbEV.Location = new Point(115, 184);

                    ucen.lblEventClass.Visible = true;
                    ucen.cmbEventC.Visible = true;
                    ucen.lblEventClass.Location = new Point(13, 212);
                    ucen.cmbEventC.Location = new Point(115, 209);

                    ucen.lblVariation.Visible = true;
                    ucen.CmbVari.Visible = true;
                    ucen.lblVariation.Location = new Point(13, 237);
                    ucen.CmbVari.Location = new Point(115, 234);



                    ucen.lblMapDesc.Visible = true;
                    ucen.txtMapDescription.Visible = true;
                    ucen.lblMapDesc.Location = new Point(13, 262);
                    ucen.txtMapDescription.Location = new Point(115, 259);

                    ucen.lblMapAutoMap.Visible = true;
                    ucen.txtMapAutoMap.Visible = true;
                    ucen.lblMapAutoMap.Location = new Point(13, 287);
                    ucen.txtMapAutoMap.Location = new Point(115, 284);
                    ucen.btnENMDone.Visible = true;
                    ucen.btnENMCancel.Visible = true;
                    ucen.btnENMDone.Location = new Point(117, 320);
                    ucen.btnENMCancel.Location = new Point(203, 320);

                    ucen.grpENMap.Size = new Size(301, 350);
                    ucen.grpENMap.Location = new Point(350,265);
                }
                //Namrata:28/7/ 2017
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE) ucen.cmbMapDataType.SelectedIndex = ucen.cmbMapDataType.FindStringExact("IntegratedTotals");
                ucen.grpENMap.Visible = true;
                ucen.txtMapReportingIndex.Focus();
            }
        }
        private void btnENMDelete_Click(object sender, EventArgs e)
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
            List<ENMap> slaveENMapList;
            if (!slavesENMapList.TryGetValue(currentSlave, out slaveENMapList))
            {
                MessageBox.Show("Error deleting EN map!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            for (int i = ucen.lvENMap.Items.Count - 1; i >= 0; i--)
            {
                if (ucen.lvENMap.Items[i].Checked)
                {
                    Console.WriteLine("*** removing indices: {0}", i);
                    slaveENMapList.RemoveAt(i);
                    ucen.lvENMap.Items[i].Remove();
                }
            }
            refreshMapList(slaveENMapList);
        }
        List<ENMap> slaveENMapList;
        private void btnENMDone_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnAIMDone_Click";
            try
            {
                List<KeyValuePair<string, string>> enmData = Utils.getKeyValueAttributes(ucen.grpENMap);
                int intStart = Convert.ToInt32(enmData[15].Value); // AINo
                int intRange = Convert.ToInt32(enmData[9].Value); //AutoMapRange
                int intAIIndex = Convert.ToInt32(enmData[16].Value); // AIReportingIndex

                //Namrata:24/7/2017
                //For Modbus Slave
                int intAIIndex1 = Convert.ToInt32(enmData[16].Value); // For MODBUSSlave AI Index Incremented by 1
                int intDupAIIndex = 0;
                int AINumber = 0, AIINdex = 0;
                int ReportingIndex = 0;
                string AIDescription = "";

                //Namrata:8/7/2017
                ListViewItem item = ucen.lvENlist.Items.Cast<ListViewItem>().FirstOrDefault(x => x.Text == intStart.ToString()); //Find Index Of ListView
                string ind = ucen.lvENlist.Items.IndexOf(item).ToString(); //Find Index Of ListView

                //Namrata:31/7/2017
                ListViewItem ExistAIMap = ucen.lvENMap.FindItemWithText(ucen.txtMapENNo.Text); //Eliminate Duplicate Record From lvAIMAp List

                //if (!ValidateMap()) return;
                Console.WriteLine("*** ucai btnAIMDone_Click clicked in class!!!");

                if (!slavesENMapList.TryGetValue(currentSlave, out slaveENMapList))
                {
                    Console.WriteLine("##### Slave entries does not exists");
                    MessageBox.Show("Error adding AI map for slave!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (mapMode == Mode.ADD)
                {
                    if (enList.Count >= 0)
                    {

                        //Namrata: 31 / 7 / 2017
                        //if (ExistAIMap != null)
                        //{
                        //    MessageBox.Show("Map Entry Already Exist In " + currentSlave.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //    return;
                        //}
                        //else
                        //{
                        if ((intRange + Convert.ToInt16(ind)) > ucen.lvENlist.Items.Count)
                        {
                            MessageBox.Show("Slave Entry Does Not Exist", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            for (int i = intStart; i <= intStart + intRange - 1; i++)
                            {
                                if (enList.Select(x => x.ENNo).ToList().Contains(i.ToString()))
                                {

                                    AINumber = i;
                                    AIINdex = intAIIndex++;
                                    ucen.txtMapDescription.Text = getDescription(ucen.lvENlist, AINumber.ToString());
                                    AIDescription = ucen.txtMapDescription.Text;

                                    ENMap NewENMap = new ENMap("EN", enmData, Utils.getSlaveTypes(currentSlave));

                                    NewENMap.ENNo = AINumber.ToString();
                                    NewENMap.Description = AIDescription.ToString();
                                    NewENMap.ReportingIndex = (AIINdex).ToString();

                                    slaveENMapList.Add(NewENMap);
                                }
                                else
                                {
                                    intRange++;
                                }
                            }
                        }
                        //}
                    }
                }
                else if (mapMode == Mode.EDIT)
                {
                    slaveENMapList[mapEditIndex].updateAttributes(enmData);
                }
                refreshMapList(slaveENMapList);
                ucen.grpENMap.Visible = false;
                mapMode = Mode.NONE;
                mapEditIndex = -1;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnENMCancel_Click(object sender, EventArgs e)
        {
            ucen.grpENMap.Visible = false;
            mapMode = Mode.NONE;
            mapEditIndex = -1;
            Utils.resetValues(ucen.grpENMap);
        }
        private void lvENMap_DoubleClick(object sender, EventArgs e)
        {
            // Ajay: 07/12/2018
            if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
            {
                if (AllowSlaveOptionsOnClick(Utils.getSlaveTypes(currentSlave))) { return; }
                else { }
            }

            List<ENMap> slaveENMapList;
            if (!slavesENMapList.TryGetValue(currentSlave, out slaveENMapList))
            {
                MessageBox.Show("Error editing EN map for slave!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //if (ucen.lvENMap.SelectedItems.Count <= 0) return;
            //ListViewItem lvi = ucen.lvENMap.SelectedItems[0];

            //Namrata: 07/03/2018
            int ListIndex = ucen.lvENMap.FocusedItem.Index;
            ListViewItem lvi = ucen.lvENMap.Items[ListIndex];

            Utils.UncheckOthers(ucen.lvENMap, lvi.Index);
            if (slaveENMapList.ElementAt(lvi.Index).IsNodeComment)
            {
                MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ucen.txtMapAutoMap.Text = "0";
            if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MQTTSLAVE)
            {

                //Ajay: 24/09/2018 Modified. Mail by Aditya K dtd. 24/09/2018
                foreach (Control c in ucen.grpENMap.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblMapHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucen.lblMapENNo.Visible = true;
                ucen.txtMapENNo.Visible = true;
                ucen.lblMapENNo.Location = new Point(13, 33);
                ucen.txtMapENNo.Location = new Point(115, 30);

                ucen.lblMapReportingIndex.Visible = true;
                ucen.txtMapReportingIndex.Visible = true;
                ucen.lblMapReportingIndex.Location = new Point(13, 58);
                ucen.txtMapReportingIndex.Location = new Point(115, 55);

                ucen.lblMapDataType.Visible = true;
                ucen.cmbMapDataType.Visible = true;
                ucen.lblMapDataType.Location = new Point(13, 83);
                ucen.cmbMapDataType.Location = new Point(115, 80);

                ucen.lblMapCmdType.Visible = true;
                ucen.cmbMapCommandType.Visible = true;
                ucen.lblMapCmdType.Location = new Point(13, 109);
                ucen.cmbMapCommandType.Location = new Point(115, 106);

                ucen.lblMapDeadband.Visible = true;
                ucen.txtMapDeadBand.Visible = true;
                ucen.lblMapDeadband.Location = new Point(13, 135);
                ucen.txtMapDeadBand.Location = new Point(115, 132);

                ucen.lblMapMultiplier.Visible = true;
                ucen.txtMapMultiplier.Visible = true;
                ucen.lblMapMultiplier.Location = new Point(13, 161);
                ucen.txtMapMultiplier.Location = new Point(115, 158);

                ucen.lblMapConstant.Visible = true;
                ucen.txtMapConstant.Visible = true;
                ucen.lblMapConstant.Location = new Point(13, 187);
                ucen.txtMapConstant.Location = new Point(115, 184);

                ucen.lblUnit.Visible = true;
                ucen.txtUnit.Visible = true;
                ucen.lblUnit.Location = new Point(13, 213);
                ucen.txtUnit.Location = new Point(115, 210);


                ucen.lblKey.Visible = false;
                ucen.txtKey.Visible = false;

                ucen.lblMapDesc.Visible = true;
                ucen.txtMapDescription.Visible = true;
                ucen.lblMapDesc.Location = new Point(13, 239);
                ucen.txtMapDescription.Location = new Point(115, 236);

                ucen.lblMapAutoMap.Visible = false;
                ucen.txtMapAutoMap.Visible = false;
                ucen.lblMapAutoMap.Location = new Point(13, 259);
                ucen.txtMapAutoMap.Location = new Point(115, 262);



                ucen.chkbxEvent.Visible = true;
                ucen.chkbxEvent.Location = new Point(115, 288);

                ucen.btnENMDone.Visible = true;
                ucen.btnENMCancel.Visible = true;
                ucen.btnENMDone.Location = new Point(117, 292);
                ucen.btnENMCancel.Location = new Point(203, 292);

                ucen.grpENMap.Size = new Size(301, 325);
            }
            if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.SMSSLAVE)
            {

                //Ajay: 24/09/2018 Modified. Mail by Aditya K dtd. 24/09/2018
                foreach (Control c in ucen.grpENMap.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblMapHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucen.lblMapENNo.Visible = true;
                ucen.txtMapENNo.Visible = true;
                ucen.lblMapENNo.Location = new Point(13, 33);
                ucen.txtMapENNo.Location = new Point(115, 30);

                ucen.lblMapReportingIndex.Visible = true;
                ucen.txtMapReportingIndex.Visible = true;
                ucen.lblMapReportingIndex.Location = new Point(13, 58);
                ucen.txtMapReportingIndex.Location = new Point(115, 55);

                ucen.lblMapDataType.Visible = true;
                ucen.cmbMapDataType.Visible = true;
                ucen.lblMapDataType.Location = new Point(13, 83);
                ucen.cmbMapDataType.Location = new Point(115, 80);

                ucen.lblMapCmdType.Visible = true;
                ucen.cmbMapCommandType.Visible = true;
                ucen.lblMapCmdType.Location = new Point(13, 109);
                ucen.cmbMapCommandType.Location = new Point(115, 106);

                ucen.lblMapDeadband.Visible = true;
                ucen.txtMapDeadBand.Visible = true;
                ucen.lblMapDeadband.Location = new Point(13, 135);
                ucen.txtMapDeadBand.Location = new Point(115, 132);

                ucen.lblMapMultiplier.Visible = true;
                ucen.txtMapMultiplier.Visible = true;
                ucen.lblMapMultiplier.Location = new Point(13, 161);
                ucen.txtMapMultiplier.Location = new Point(115, 158);

                ucen.lblMapConstant.Visible = true;
                ucen.txtMapConstant.Visible = true;
                ucen.lblMapConstant.Location = new Point(13, 187);
                ucen.txtMapConstant.Location = new Point(115, 184);

                ucen.lblUnit.Visible = true;
                ucen.txtUnit.Visible = true;
                ucen.lblUnit.Location = new Point(13, 213);
                ucen.txtUnit.Location = new Point(115, 210);
                ucen.lblUnit.Visible = true;
                ucen.txtUnit.Visible = true;
                ucen.lblKey.Visible = true;
                ucen.txtKey.Visible = true;
                ucen.lblKey.Location = new Point(13, 239);
                ucen.txtKey.Location = new Point(115, 236);




                ucen.lblMapDesc.Visible = true;
                ucen.txtMapDescription.Visible = true;
                ucen.lblMapDesc.Location = new Point(13, 259);
                ucen.txtMapDescription.Location = new Point(115, 262);

                ucen.lblMapAutoMap.Visible = false;
                ucen.txtMapAutoMap.Visible = false;
                ucen.lblMapAutoMap.Location = new Point(13, 285);
                ucen.txtMapAutoMap.Location = new Point(115, 288);

                ucen.btnENMDone.Visible = true;
                ucen.btnENMCancel.Visible = true;
                ucen.btnENMDone.Location = new Point(117, 292);
                ucen.btnENMCancel.Location = new Point(203, 292);

                ucen.grpENMap.Size = new Size(301, 325);
            }
            if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC101SLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC104) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE))
            {
                //Ajay: 24/09/2018 Commented All and Modified. 

                //ucen.lblMapUnitID.Visible = false;
                //ucen.txtMapUnitID.Visible = false;
                //ucen.chkUsed.Visible = false;
                ////ucen.grpENMap.Size = new Size(301, 307);
                ////ucen.grpENMap.Location = new Point(583, 310);
                ////ucen.lblAutoMapM.Visible = true;
                ////ucen.txtENAutoMapNumber.Visible = true;
                ////ucen.btnENMDone.Location = new Point(116, 267);
                ////ucen.btnENMCancel.Location = new Point(203, 267);

                //ucen.lblMapENNo.Location = new Point(13, 33);
                //ucen.txtMapENNo.Location = new Point(115, 30);

                //ucen.lblMapReportingIndex.Location = new Point(13, 58);
                //ucen.txtMapReportingIndex.Location = new Point(115, 55);


                //ucen.lblMapDataType.Location = new Point(13, 83);
                //ucen.cmbMapDataType.Location = new Point(115, 80);

                //ucen.lblMapCmdType.Location = new Point(13, 109);
                //ucen.cmbMapCommandType.Location = new Point(115, 106);
                //ucen.lblMapCmdType.Visible = true;
                //ucen.cmbMapCommandType.Visible = true;


                //ucen.lblMapDeadband.Visible = true;
                //ucen.txtMapDeadBand.Visible = true;

                //ucen.lblMapMultiplier.Location = new Point(13, 135);
                //ucen.txtMapMultiplier.Location = new Point(115, 132);

                //ucen.lblMapConstant.Location = new Point(13, 160);
                //ucen.txtMapConstant.Location = new Point(115, 157);

                //ucen.lblMapDesc.Location = new Point(13, 185);
                //ucen.txtMapDescription.Location = new Point(115, 182);

                //ucen.lblMapAutoMap.Location = new Point(13, 210);
                //ucen.txtMapAutoMap.Location = new Point(115, 207);
                //ucen.lblMapAutoMap.Visible = false;
                //ucen.txtMapAutoMap.Visible = false;
                //ucen.chkUsed.Visible = false;


                //ucen.btnENMDone.Location = new Point(117, 210);
                //ucen.btnENMCancel.Location = new Point(203, 210);

                //ucen.grpENMap.Size = new Size(301, 255);

                //Ajay: 24/09/2018 Modified. Mail by Aditya K dtd. 24/09/2018
                foreach (Control c in ucen.grpENMap.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblMapHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucen.lblMapENNo.Visible = true;
                ucen.txtMapENNo.Visible = true;
                ucen.lblMapENNo.Location = new Point(13, 33);
                ucen.txtMapENNo.Location = new Point(115, 30);

                ucen.lblMapReportingIndex.Visible = true;
                ucen.txtMapReportingIndex.Visible = true;
                ucen.lblMapReportingIndex.Location = new Point(13, 58);
                ucen.txtMapReportingIndex.Location = new Point(115, 55);

                ucen.lblMapDataType.Visible = true;
                ucen.cmbMapDataType.Visible = true;
                ucen.lblMapDataType.Location = new Point(13, 83);
                ucen.cmbMapDataType.Location = new Point(115, 80);

                ucen.lblMapCmdType.Visible = true;
                ucen.cmbMapCommandType.Visible = true;
                ucen.lblMapCmdType.Location = new Point(13, 109);
                ucen.cmbMapCommandType.Location = new Point(115, 106);

                ucen.lblMapDeadband.Visible = true;
                ucen.txtMapDeadBand.Visible = true;
                ucen.lblMapDeadband.Location = new Point(13, 135);
                ucen.txtMapDeadBand.Location = new Point(115, 132);

                ucen.lblMapMultiplier.Visible = true;
                ucen.txtMapMultiplier.Visible = true;
                ucen.lblMapMultiplier.Location = new Point(13, 161);
                ucen.txtMapMultiplier.Location = new Point(115, 158);

                ucen.lblMapConstant.Visible = true;
                ucen.txtMapConstant.Visible = true;
                ucen.lblMapConstant.Location = new Point(13, 187);
                ucen.txtMapConstant.Location = new Point(115, 184);

                ucen.lblMapDesc.Visible = true;
                ucen.txtMapDescription.Visible = true;
                ucen.lblMapDesc.Location = new Point(13, 213);
                ucen.txtMapDescription.Location = new Point(115, 210);

                //ucen.lblMapAutoMap.Visible = true;
                //ucen.txtMapAutoMap.Visible = true;
                //ucen.lblMapAutoMap.Location = new Point(13, 239);
                //ucen.txtMapAutoMap.Location = new Point(115, 236);
             
                ucen.btnENMDone.Visible = true;
                ucen.btnENMCancel.Visible = true;
                ucen.btnENMDone.Location = new Point(117, 243);
                ucen.btnENMCancel.Location = new Point(203, 243);

                ucen.grpENMap.Size = new Size(301, 283);
            }
            if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.SPORTSLAVE))
            {
                ucen.lblMapENNo.Location = new Point(13, 33);
                ucen.txtMapENNo.Location = new Point(115, 30);

                ucen.lblMapUnitID.Location = new Point(13, 58);
                ucen.txtMapUnitID.Location = new Point(115, 55);
                ucen.lblMapUnitID.Visible = true;
                ucen.txtMapUnitID.Visible = true;

                ucen.lblMapReportingIndex.Location = new Point(13, 83);
                ucen.txtMapReportingIndex.Location = new Point(115, 80);

                ucen.lblMapDataType.Location = new Point(13, 109);
                ucen.cmbMapDataType.Location = new Point(115, 106);

                ucen.lblMapCmdType.Visible = false;
                ucen.cmbMapCommandType.Visible = false;

                ucen.lblMapDeadband.Location = new Point(13, 135);
                ucen.txtMapDeadBand.Location = new Point(115, 132);

                ucen.lblMapMultiplier.Location = new Point(13, 160);
                ucen.txtMapMultiplier.Location = new Point(115, 157);

                ucen.lblMapConstant.Location = new Point(13, 185);
                ucen.txtMapConstant.Location = new Point(115, 182);

                ucen.lblMapDesc.Location = new Point(13, 210);
                ucen.txtMapDescription.Location = new Point(115, 207);

                ucen.lblMapAutoMap.Location = new Point(13, 235);
                ucen.txtMapAutoMap.Location = new Point(115, 232);
                ucen.lblMapAutoMap.Visible = false;
                ucen.txtMapAutoMap.Visible = false;

                ucen.chkUsed.Visible = ucen.chkUsed.Enabled = true;
                ucen.chkUsed.Location = new Point(115, 236);

                ucen.chkbxEvent.Visible = ucen.chkbxEvent.Enabled = true;
                ucen.chkbxEvent.Location = new Point(15, 236);

                ucen.btnENMDone.Location = new Point(117, 260);
                ucen.btnENMCancel.Location = new Point(203, 260);

                ucen.grpENMap.Size = new Size(301, 300);


            }
            ucen.grpENMap.Visible = true;
            mapMode = Mode.EDIT;
            mapEditIndex = lvi.Index;
            loadMapValues();
            ucen.txtMapReportingIndex.Focus();
        }
        private void loadMapDefaults()
        {
            ucen.txtMapReportingIndex.Text = (Globals.ENReportingIndex + 1).ToString();
            ucen.txtMapDeadBand.Text = "1";
            ucen.txtMapMultiplier.Text = "1";
            ucen.txtMapConstant.Text = "0";
            ucen.txtMapUnitID.Text = "1";
        }
        private void loadMapValues()
        {
            List<ENMap> slaveENMapList;
            if (!slavesENMapList.TryGetValue(currentSlave, out slaveENMapList))
            {
                MessageBox.Show("Error loading EN map data for slave!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ENMap enmn = slaveENMapList.ElementAt(mapEditIndex);
            if (enmn != null)
            {
                ucen.txtMapENNo.Text = enmn.ENNo;
                ucen.txtMapReportingIndex.Text = enmn.ReportingIndex;
                ucen.cmbMapDataType.SelectedIndex = ucen.cmbMapDataType.FindStringExact(enmn.DataType);
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE)
                {
                    ucen.cmbMapCommandType.SelectedIndex = ucen.cmbMapCommandType.FindStringExact(enmn.CommandType);
                    ucen.cmbMapCommandType.Enabled = true;
                }
                else
                {
                    ucen.cmbMapCommandType.Enabled = false;
                }
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.SPORTSLAVE)
                {
                    ucen.txtMapUnitID.Text = enmn.UnitID;
                    ucen.txtMapUnitID.Visible = true;
                    ucen.chkUsed.Visible = true;
                }
                else
                {
                    ucen.txtMapUnitID.Enabled = false;
                    ucen.txtMapUnitID.Visible = false;
                    ucen.chkUsed.Enabled = false;
                    ucen.chkUsed.Visible = false;

                }
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE)
                {
                    ucen.cmbMapCommandType.Enabled = true; ucen.cmbMapCommandType.Enabled = true;
                    ucen.cmbEventC.SelectedIndex = ucen.cmbEventC.FindStringExact(enmn.EventClass);//Namrata:27/03/2019
                    ucen.CmbVari.SelectedIndex = ucen.CmbVari.FindStringExact(enmn.Variation);//Namrata:27/03/2019
                    ucen.CmbEV.SelectedIndex = ucen.CmbEV.FindStringExact(enmn.EventVariation);//Namrata:27/03/2019
                }
                //Ajay: 12/07/2018 check-unchecked Used checkbox according to mapped Used value
                if (enmn.Used.ToLower() == "yes")
                { ucen.chkUsed.Checked = true; }
                else { ucen.chkUsed.Checked = false; }

                //Namrata: 18/11/2017
                ucen.txtMapDescription.Text = enmn.Description;
                ucen.txtMapDeadBand.Text = enmn.Deadband;
                ucen.txtMapMultiplier.Text = enmn.Multiplier;
                ucen.txtMapConstant.Text = enmn.Constant;
                //Ajay: 05/09/2018
                if (enmn.Event.ToLower() == "yes")
                { ucen.chkbxEvent.Checked = true; }
                else { ucen.chkbxEvent.Checked = false; }
            }
        }
        private bool ValidateMap()
        {
            bool status = true;
            //Check empty field's
            if (Utils.IsEmptyFields(ucen.grpENMap))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return status;
        }
        private void refreshMapList(List<ENMap> tmpList)
        {
            int cnt = 0;
            ucen.lvENMap.Items.Clear();
            ucen.lblENMRecords.Text = "0";
            if (tmpList == null) return;

            //ucen.lvENMap.Columns.Add("EN No.", "EN No.", 60, HorizontalAlignment.Left, 1);
            //ucen.lvENMap.Columns.Add("Unit ID", 0, HorizontalAlignment.Left);
            //ucen.lvENMap.Columns.Add("Reporting Index", "Reporting Index", 130, HorizontalAlignment.Left, 1);
            //ucen.lvENMap.Columns.Add("Data Type", "Data Type", 130, HorizontalAlignment.Left, 0);
            //ucen.lvENMap.Columns.Add("Command Type", "Command Type", 0, HorizontalAlignment.Left, 0);
            //ucen.lvENMap.Columns.Add("Deadband", "Deadband", 70, HorizontalAlignment.Left, 0);
            //ucen.lvENMap.Columns.Add("Multiplier", "Multiplier", 70, HorizontalAlignment.Left, 1);
            //ucen.lvENMap.Columns.Add("Constant", "Constant", -2, HorizontalAlignment.Left, 1);
            //ucen.lvENMap.Columns.Add("Used", 0, HorizontalAlignment.Left);
            //ucen.lvENMap.Columns.Add("Event", 0, HorizontalAlignment.Left); //Ajay: 05/09/2018
            //ucen.lvENMap.Columns.Add("Unit", "Unit", 90, HorizontalAlignment.Left, null);
            //ucen.lvENMap.Columns.Add("Key", "Key", 90, HorizontalAlignment.Left, null);
            ////Namrata: 12/11/2019
            //ucen.lvENMap.Columns.Add("Event Class", 0, HorizontalAlignment.Left);
            //ucen.lvENMap.Columns.Add("Variation", 0, HorizontalAlignment.Left);
            //ucen.lvENMap.Columns.Add("Event Variation", 0, HorizontalAlignment.Left);
            //ucen.lvENMap.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, 0);
            else if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.MQTTSLAVE))
            {
                foreach (ENMap enmp in tmpList)
                {
                    string[] row = new string[16];
                    if (enmp.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = enmp.ENNo;
                        row[1] = "";
                        row[2] = enmp.ReportingIndex;
                        row[3] = enmp.DataType;
                        row[4] = "";
                        row[5] = enmp.Deadband;
                        row[6] = enmp.Multiplier;
                        row[7] = enmp.Constant;
                        row[8] = "";
                        row[9] = ""; //Ajay: 07/09/2018
                        row[10] = enmp.Unit;
                        row[11] = enmp.Key;
                        row[12] = "";
                        row[13] = "";
                        row[14] = "";
                        row[15] = enmp.Description;
                    }

                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucen.lvENMap.Items.Add(lvItem);
                }
            }
            if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.SMSSLAVE))
            {
                foreach (ENMap enmp in tmpList)
                {
                    string[] row = new string[16];
                    if (enmp.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = enmp.ENNo;
                        row[1] = "";
                        row[2] = enmp.ReportingIndex;
                        row[3] = enmp.DataType;
                        row[4] = "";
                        row[5] = enmp.Deadband;
                        row[6] = enmp.Multiplier;
                        row[7] = enmp.Constant;
                        row[8] = "";
                        row[9] = enmp.Event; //Ajay: 07/09/2018
                        row[10] = enmp.Unit;
                        row[11] = "";
                        row[12] = "";
                        row[13] = "";
                        row[14] = "";
                        row[15] = enmp.Description;
                    }

                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucen.lvENMap.Items.Add(lvItem);
                }
            }
            else if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE))
            {
                foreach (ENMap enmp in tmpList)
                {
                    string[] row = new string[16];
                    if (enmp.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = enmp.ENNo;
                        row[1] = "";
                        row[2] = enmp.ReportingIndex;
                        row[3] = "";
                        row[4] = enmp.CommandType;
                        row[5] = enmp.Deadband;
                        row[6] = enmp.Multiplier;
                        row[7] = enmp.Constant;
                        row[8] = "";
                        row[9] = ""; //Ajay: 07/09/2018
                        row[10] = "";
                        row[11] = "";
                        row[12] = enmp.EventClass;
                        row[13] = enmp.Variation;
                        row[14] = enmp.EventVariation;
                        row[15] = enmp.Description;
                    }

                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucen.lvENMap.Items.Add(lvItem);
                }
            }
            else
            {
                foreach (ENMap enmp in tmpList)
                {
                    string[] row = new string[16];
                    if (enmp.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = enmp.ENNo;
                        row[1] = enmp.UnitID;
                        row[2] = enmp.ReportingIndex;
                        row[3] = enmp.DataType;
                        row[4] = enmp.CommandType;
                        row[5] = enmp.Deadband;
                        row[6] = enmp.Multiplier;
                        row[7] = enmp.Constant;
                        row[8] = enmp.Used;
                        row[10] = enmp.Unit;
                        row[11] = "";
                        row[12] = "";
                        row[13] = "";
                        row[14] = "";
                        row[15] = enmp.Description;
                    }

                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucen.lvENMap.Items.Add(lvItem);
                }
            }
            ucen.lblENMRecords.Text = tmpList.Count.ToString();
        }
        private void refreshCurrentMapList()
        {
            fillMapOptions(Utils.getSlaveTypes(currentSlave));
            List<ENMap> senmList;
            if (!slavesENMapList.TryGetValue(currentSlave, out senmList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                refreshMapList(null);
            }
            else
            {
                refreshMapList(senmList);
            }
        }
        /* ============================================= Above this, EN Map logic... ============================================= */

        private void fillOptions()
        {
            if (!dtdataset.Columns.Contains("Address")) //Ajay: 22/09/2018 Condition checked to handle exception
            { DataColumn dcAddressColumn = dtdataset.Columns.Add("Address", typeof(string)); }
            if (!dtdataset.Columns.Contains("IED")) //Ajay: 22/09/2018 Condition checked to handle exception
            { dtdataset.Columns.Add("IED", typeof(string)); }

            //Fill IED Name
            //ucen.cmbIEDName.Items.Clear(); //Ajay: 01/11/2018 Items collection cannot be modified when the DataSource property is set.
            //Namrata: 31/10/2017
            ucen.cmbIEDName.DataSource = Utils.dsIED.Tables[Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname];//Utils.DtIEDName;
            ucen.cmbIEDName.DisplayMember = "IEDName";
            if (Utils.Iec61850IEDname != "")
            {
                ucen.cmbIEDName.Text = Utils.Iec61850IEDname;
            }
            //Fill ResponseType For IEC61850Client
            //ucen.cmb61850ResponseType.Items.Clear(); //Ajay: 01/11/2018 Items collection cannot be modified when the DataSource property is set.
            ucen.cmb61850ResponseType.DataSource = Utils.dsResponseType.Tables[Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname];//Utils.DtAddress;
            ucen.cmb61850ResponseType.DisplayMember = "Address";

            //Fill Response Type...
            if (masterType == MasterTypes.IEC61850Client)
            {
                ucen.cmbResponseType.Items.Clear();
            }
            else
            {
                ucen.cmbResponseType.Items.Clear();
                if (masterType != MasterTypes.LoadProfile) //Ajay: 31/07/2018
                {
                    foreach (String rt in EN.getResponseTypes(masterType))
                    {
                        ucen.cmbResponseType.Items.Add(rt.ToString());
                    }
                    ucen.cmbResponseType.SelectedIndex = 0;
                }
            }

            //Fill Data Type...
            if ((masterType == MasterTypes.IEC61850Client))
            {
                ucen.cmbDataType.Items.Clear();
            }
            else
            {
                if (masterType != MasterTypes.LoadProfile) //Ajay: 31/07/2018
                {
                    ucen.cmbDataType.Items.Clear();
                    foreach (String dt in EN.getDataTypes(masterType))
                    {
                        ucen.cmbDataType.Items.Add(dt.ToString());
                    }
                    if (ucen.cmbDataType != null && ucen.cmbDataType.Items.Count > 0)
                    {
                        ucen.cmbDataType.SelectedIndex = 0;
                    }
                    
                }
            }
            //Ajay: 31/07/2018
            if (masterType != MasterTypes.LoadProfile)
            {
                ucen.cmbAI1.Items.Clear();
                ucen.cmbEN1.Items.Clear();
                ucen.cmbName.Items.Clear();
            }
            else
            {
                //ucen.cmbAI1.Items.Clear();
                //ucen.cmbEN1.Items.Clear();
                //ucen.cmbName.Items.Clear();
                ucen.cmbAI1.DataSource = Utils.GetAllAIList();
                ucen.cmbEN1.DataSource = Utils.GetAllENList();
                
            ucen.cmbName.DataSource = Utils.getOpenProPlusHandle().getDataTypeValues("LoadProfile_EN_Name");
            }
        }
        private void fillMapOptions(SlaveTypes sType)
        {
            /***** Fill Map details related combobox ******/

            try
            {
                //Fill Data Type...
                ucen.cmbMapDataType.Items.Clear();
                foreach (String dt in ENMap.getDataTypes(sType))
                {
                    ucen.cmbMapDataType.Items.Add(dt.ToString());
                }
                if (ucen.cmbMapDataType.Items.Count > 0) ucen.cmbMapDataType.SelectedIndex = 0;

                #region Fill Variations
                ucen.CmbVari.Items.Clear();
                foreach (String dt in ENMap.getVariartions(sType))
                {
                    ucen.CmbVari.Items.Add(dt.ToString());
                }
                if (ucen.CmbVari.Items.Count > 0) ucen.CmbVari.SelectedIndex = 0;
                #endregion Fill Variations

                #region Fill EVariations
                ucen.CmbEV.Items.Clear();
                foreach (String dt in ENMap.getEventsVariations(sType))
                {
                    ucen.CmbEV.Items.Add(dt.ToString());
                }
                if (ucen.CmbEV.Items.Count > 0) ucen.CmbEV.SelectedIndex = 0;
                #endregion Fill EVariations

                #region Fill EClass
                ucen.cmbEventC.Items.Clear();
                foreach (String dt in ENMap.getEventsClasses(sType))
                {
                    ucen.cmbEventC.Items.Add(dt.ToString());
                }
                if (ucen.cmbEventC.Items.Count > 0) ucen.cmbEventC.SelectedIndex = 0;
                #endregion Fill EClass
            }
            catch (System.NullReferenceException)
            {
                Utils.WriteLine(VerboseLevel.ERROR, "EN Map DataType does not exist for Slave Type: {0}", sType.ToString());
            }

            try
            {
                //Fill Command Type...
                ucen.cmbMapCommandType.Items.Clear();
                foreach (String ct in ENMap.getCommandTypes(sType))
                {
                    ucen.cmbMapCommandType.Items.Add(ct.ToString());
                }
                if (ucen.cmbMapCommandType.Items.Count > 0) ucen.cmbMapCommandType.SelectedIndex = 0;
            }
            catch (System.NullReferenceException)
            {
                Utils.WriteLine(VerboseLevel.ERROR, "EN Map CommandType does not exist for Slave Type: {0}", sType.ToString());
            }
        }
        private void addListHeaders()
        {
            if(masterType == MasterTypes.ADR)
            {
                ucen.lvENlist.Columns.Add("EN No.", "EN No.", 60, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Response Type", "Response Type", 220, HorizontalAlignment.Left, 0);
                ucen.lvENlist.Columns.Add("Index", "Index", 60, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Sub Index", "Sub Index", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Data Type", "Data Type", 200, HorizontalAlignment.Left, 0);
                ucen.lvENlist.Columns.Add("Multiplier", "Multiplier", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Constant", "Constant", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("EventEnable", "EventEnable", 80, HorizontalAlignment.Left, 1); //Ajay: 02/09/2018
                ucen.lvENlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, 0);
            }
            if (masterType == MasterTypes.IEC101)
            {
                ucen.lvENlist.Columns.Add("EN No.", "EN No.", 60, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Response Type", "Response Type", 220, HorizontalAlignment.Left, 0);
                ucen.lvENlist.Columns.Add("Index", "Index", 60, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Sub Index", "Sub Index", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Data Type", "Data Type", 200, HorizontalAlignment.Left, 0);
                ucen.lvENlist.Columns.Add("Multiplier", "Multiplier", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Constant", "Constant", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("EventEnable", "EventEnable", 80, HorizontalAlignment.Left, 1); //Ajay: 02/09/2018
                ucen.lvENlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, 0);
            }
            if (masterType == MasterTypes.IEC104)
            {
                ucen.lvENlist.Columns.Add("EN No.", "EN No.", 60, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Response Type", "Response Type", 220, HorizontalAlignment.Left, 0);
                ucen.lvENlist.Columns.Add("Index", "Index", 60, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Sub Index", "Sub Index", 70, HorizontalAlignment.Left, 1);
                //ucen.lvENlist.Columns.Add("Data Type", "Data Type", 200, HorizontalAlignment.Left, 0);
                ucen.lvENlist.Columns.Add("Multiplier", "Multiplier", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Constant", "Constant", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("EventEnable", "EventEnable", 80, HorizontalAlignment.Left, 1); //Ajay: 02/09/2018
                ucen.lvENlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, 0);
            }
            if (masterType == MasterTypes.SPORT)
            {
                ucen.lvENlist.Columns.Add("EN No.", "EN No.", 60, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Response Type", "Response Type", 220, HorizontalAlignment.Left, 0);
                ucen.lvENlist.Columns.Add("Index", "Index", 60, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Sub Index", "Sub Index", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Data Type", "Data Type", 200, HorizontalAlignment.Left, 0);
                ucen.lvENlist.Columns.Add("Multiplier", "Multiplier", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Constant", "Constant", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("EventEnable", "EventEnable", 80, HorizontalAlignment.Left, 1); //Ajay: 02/09/2018
                ucen.lvENlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, 0);
            }
            if (masterType == MasterTypes.IEC103)
            {
                ucen.lvENlist.Columns.Add("EN No.", "EN No.", 60, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Response Type", "Response Type", 220, HorizontalAlignment.Left, 0);
                ucen.lvENlist.Columns.Add("Index", "Index", 60, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Sub Index", "Sub Index", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Data Type", "Data Type", 200, HorizontalAlignment.Left, 0);
                ucen.lvENlist.Columns.Add("Multiplier", "Multiplier", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Constant", "Constant", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("EventEnable", "EventEnable", 80, HorizontalAlignment.Left, 1); //Ajay: 02/09/2018
                ucen.lvENlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, 0);
            }
            if (masterType == MasterTypes.MODBUS)
            {
                ucen.lvENlist.Columns.Add("EN No.", "EN No.", 60, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Response Type", "Response Type", 220, HorizontalAlignment.Left, 0);
                ucen.lvENlist.Columns.Add("Index", "Index", 60, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Sub Index", "Sub Index", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Data Type", "Data Type", 200, HorizontalAlignment.Left, 0);
                ucen.lvENlist.Columns.Add("Multiplier", "Multiplier", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Constant", "Constant", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("EventEnable", "EventEnable", 80, HorizontalAlignment.Left, 1); //Ajay: 02/09/2018
                ucen.lvENlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, 0);
            }
            if (masterType == MasterTypes.Virtual)
            {
                ucen.lvENlist.Columns.Add("EN No.", "EN No.", 60, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Response Type", "Response Type", 220, HorizontalAlignment.Left, 0);
                ucen.lvENlist.Columns.Add("Index", "Index", 60, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Sub Index", "Sub Index", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Data Type", "Data Type", 200, HorizontalAlignment.Left, 0);
                ucen.lvENlist.Columns.Add("Multiplier", "Multiplier", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Constant", "Constant", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, 0);
            }
            if (masterType == MasterTypes.IEC61850Client)
            {
                ucen.lvENlist.Columns.Add("EN No.", "EN No.", 60, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("IEDName", 50, HorizontalAlignment.Left);
                ucen.lvENlist.Columns.Add("Response Type", "Response Type", 220, HorizontalAlignment.Left, 0);
                ucen.lvENlist.Columns.Add("Index", "Index", 250, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("FC", "FC", 40, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Sub Index", "Sub Index", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Multiplier", "Multiplier", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Constant", "Constant", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("EventEnable", "EventEnable", 80, HorizontalAlignment.Left, 1); //Ajay: 02/09/2018
                ucen.lvENlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, 0);
                //Namrata: 15/9/2017
                //Hide IED Name
                ucen.lvENlist.Columns[1].Width = 0;
            }
            //Ajay: 31/07/2018
            if (masterType == MasterTypes.LoadProfile)
            {
                ucen.lvENlist.Columns.Add("EN No.", 70, HorizontalAlignment.Left);
                ucen.lvENlist.Columns.Add("Name", 130, HorizontalAlignment.Left);
                ucen.lvENlist.Columns.Add("AI1", 60, HorizontalAlignment.Left);
                ucen.lvENlist.Columns.Add("EN1", 60, HorizontalAlignment.Left);
                ucen.lvENlist.Columns.Add("EventEnable", "EventEnable", 80, HorizontalAlignment.Left, 1); //Ajay: 02/09/2018
                //ucen.lvENlist.Columns.Add("Log Enable", 80, HorizontalAlignment.Left); //Ajay: 19/09/2018 commented
                ucen.lvENlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
            }
            
            //Add EN map headers..
            ucen.lvENMap.Columns.Add("EN No.", "EN No.", 60, HorizontalAlignment.Left, 1);
            ucen.lvENMap.Columns.Add("Unit ID", 0, HorizontalAlignment.Left);
            ucen.lvENMap.Columns.Add("Reporting Index", "Reporting Index", 130, HorizontalAlignment.Left, 1);
            ucen.lvENMap.Columns.Add("Data Type", "Data Type", 130, HorizontalAlignment.Left, 0);
            ucen.lvENMap.Columns.Add("Command Type", "Command Type", 0, HorizontalAlignment.Left, 0);
            ucen.lvENMap.Columns.Add("Deadband", "Deadband", 70, HorizontalAlignment.Left, 0);
            ucen.lvENMap.Columns.Add("Multiplier", "Multiplier", 70, HorizontalAlignment.Left, 1);
            ucen.lvENMap.Columns.Add("Constant", "Constant", -2, HorizontalAlignment.Left, 1);
            ucen.lvENMap.Columns.Add("Used", 0, HorizontalAlignment.Left);
            ucen.lvENMap.Columns.Add("Event", 0, HorizontalAlignment.Left); //Ajay: 05/09/2018
            ucen.lvENMap.Columns.Add("Unit", "Unit", 90, HorizontalAlignment.Left, null);
            ucen.lvENMap.Columns.Add("Key", "Key", 90, HorizontalAlignment.Left, null);
            //Namrata: 12/11/2019
            ucen.lvENMap.Columns.Add("Event Class", 0, HorizontalAlignment.Left);
            ucen.lvENMap.Columns.Add("Variation", 0, HorizontalAlignment.Left);
            ucen.lvENMap.Columns.Add("Event Variation", 0, HorizontalAlignment.Left);
            ucen.lvENMap.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, 0);
        }
        //Ajay: 27/07/2018
        public void SPORT_OnDoubleClick()
        {
            string strRoutineName = "SPORT_OnDoubleClick";
            try
            {
                foreach (Control c in ucen.grpEN.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucen.lblENNo.Visible = ucen.txtENNo.Visible = true;
                ucen.txtENNo.Enabled = false;
                ucen.lblENNo.Location = new Point(15, 35);
                ucen.txtENNo.Location = new Point(102, 30);

                ucen.lblResponseType.Visible = ucen.cmbResponseType.Visible = true;
                ucen.lblResponseType.Location = new Point(15, 60);
                ucen.cmbResponseType.Location = new Point(102, 55);

                ucen.lblIndex.Visible = ucen.txtIndex.Visible = true;
                ucen.lblIndex.Location = new Point(15, 85);
                ucen.txtIndex.Location = new Point(102, 80);

                ucen.lblSubIndex.Visible = ucen.txtSubIndex.Visible = true;
                ucen.lblSubIndex.Location = new Point(15, 110);
                ucen.txtSubIndex.Location = new Point(102, 105);

                ucen.lblDataType.Visible = ucen.cmbDataType.Visible = true;
                ucen.lblDataType.Location = new Point(15, 135);
                ucen.cmbDataType.Location = new Point(102, 130);

                ucen.lblMultiplier.Visible = ucen.txtMultiplier.Visible = true;
                ucen.lblMultiplier.Location = new Point(15, 160);
                ucen.txtMultiplier.Location = new Point(102, 155);

                ucen.lblConstant.Visible = ucen.txtConstant.Visible = true;
                ucen.lblConstant.Location = new Point(15, 185);
                ucen.txtConstant.Location = new Point(102, 180);

                ucen.lblDesc.Visible = ucen.txtDescription.Visible = true;
                ucen.lblDesc.Location = new Point(15, 210);
                ucen.txtDescription.Location = new Point(102, 205);

                //ucen.lblAutoMap.Visible = ucen.txtAutoMapNumber.Visible = true;
                //ucen.lblAutoMap.Location = new Point(15, 450); //Set the location out of box to hide
                //ucen.txtAutoMapNumber.Location = new Point(102, 450); //Set the location out of box to hide

                ucen.chkbxEventEnable.Visible = true;
                ucen.chkbxEventEnable.Checked = false;
                ucen.chkbxEventEnable.Location = new Point(15, 240);

                ucen.btnDone.Visible = ucen.btnCancel.Visible = true;
                ucen.btnDone.Location = new Point(70, 275);
                ucen.btnCancel.Location = new Point(170, 275);

                ucen.btnFirst.Visible = ucen.btnPrev.Visible = ucen.btnNext.Visible = ucen.btnLast.Visible = true;
                ucen.btnFirst.Location = new Point(4, 310);
                ucen.btnPrev.Location = new Point(75, 310);
                ucen.btnNext.Location = new Point(155, 310);
                ucen.btnLast.Location = new Point(230, 310);

                ucen.grpEN.Height = 345;
                ucen.grpEN.Width = 290;
                ucen.grpEN.Location = new Point(172, 52);

                //ucen.chkbxEventEnable.Visible = false;  //Ajay: 27/07/2018

                //ucen.lblDataType.Visible = false;
                //ucen.cmbDataType.Visible = false;
                //ucen.grpEN.Location = new Point(172, 52);
                //ucen.cmbIEDName.Visible = false;
                //ucen.lblIEDName.Visible = false;
                //ucen.lbl61850Index.Visible = false;
                //ucen.cmb61850Index.Visible = false;
                //ucen.lbl61850ResponseType.Visible = false;
                //ucen.cmb61850ResponseType.Visible = false;
                //ucen.lblFC.Visible = false;
                //ucen.txtFC.Visible = false;

                //ucen.lblENNo.Location = new Point(14, 30);
                //ucen.lblResponseType.Location = new Point(14, 55);
                //ucen.lblIndex.Location = new Point(14, 81);
                //ucen.lblSubIndex.Location = new Point(14, 106);
                //ucen.lblMultiplier.Location = new Point(14, 131);
                //ucen.lblConstant.Location = new Point(14, 157);
                //ucen.lblDesc.Location = new Point(14, 182);
                //ucen.lblAutoMap.Location = new Point(14, 207);
                //ucen.txtAutoMapNumber.Enabled = false;
                //ucen.lblAutoMap.Enabled = false;
                //ucen.txtAutoMapNumber.Visible = false;
                //ucen.lblAutoMap.Visible = false;
                //ucen.txtENNo.Location = new Point(120, 27);
                //ucen.txtENNo.Size = new Size(174, 20);
                //ucen.cmbResponseType.Location = new Point(120, 52);
                //ucen.cmbResponseType.Size = new Size(174, 20);
                //ucen.txtIndex.Location = new Point(120, 78);
                //ucen.txtIndex.Size = new Size(174, 20);
                //ucen.txtSubIndex.Location = new Point(120, 103);
                //ucen.txtSubIndex.Size = new Size(174, 20);
                //ucen.txtMultiplier.Location = new Point(120, 128);
                //ucen.txtMultiplier.Size = new Size(174, 20);
                //ucen.txtConstant.Location = new Point(120, 154);
                //ucen.txtConstant.Size = new Size(174, 20);
                //ucen.txtDescription.Location = new Point(120, 179);
                //ucen.txtDescription.Size = new Size(174, 20);
                //ucen.btnDone.Location = new Point(121, 210);
                //ucen.btnCancel.Location = new Point(218, 210);
                //ucen.btnFirst.Location = new Point(20, 245);
                //ucen.btnNext.Location = new Point(89, 245);
                //ucen.btnPrev.Location = new Point(163, 245);
                //ucen.btnLast.Location = new Point(237, 245);
                //ucen.grpEN.Size = new Size(314, 270);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 12/11/2018
        public void IEC101_OnDoubleClick()
        {
            string strRoutineName = "IEC101_OnDoubleClick";
            try
            {
                foreach (Control c in ucen.grpEN.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucen.lblENNo.Visible = ucen.txtENNo.Visible = true;
                ucen.txtENNo.Enabled = false;
                ucen.lblENNo.Location = new Point(15, 35);
                ucen.txtENNo.Location = new Point(102, 30);

                ucen.lblResponseType.Visible = ucen.cmbResponseType.Visible = true;
                ucen.lblResponseType.Location = new Point(15, 60);
                ucen.cmbResponseType.Location = new Point(102, 55);

                ucen.lblIndex.Visible = ucen.txtIndex.Visible = true;
                ucen.lblIndex.Location = new Point(15, 85);
                ucen.txtIndex.Location = new Point(102, 80);

                ucen.lblSubIndex.Visible = ucen.txtSubIndex.Visible = true;
                ucen.lblSubIndex.Location = new Point(15, 110);
                ucen.txtSubIndex.Location = new Point(102, 105);

                ucen.lblDataType.Visible = ucen.cmbDataType.Visible = true;
                ucen.lblDataType.Location = new Point(15, 135);
                ucen.cmbDataType.Location = new Point(102, 130);

                ucen.lblMultiplier.Visible = ucen.txtMultiplier.Visible = true;
                ucen.lblMultiplier.Location = new Point(15, 160);
                ucen.txtMultiplier.Location = new Point(102, 155);

                ucen.lblConstant.Visible = ucen.txtConstant.Visible = true;
                ucen.lblConstant.Location = new Point(15, 185);
                ucen.txtConstant.Location = new Point(102, 180);

                ucen.lblDesc.Visible = ucen.txtDescription.Visible = true;
                ucen.lblDesc.Location = new Point(15, 210);
                ucen.txtDescription.Location = new Point(102, 205);

                //ucen.lblAutoMap.Visible = ucen.txtAutoMapNumber.Visible = true;
                //ucen.lblAutoMap.Location = new Point(15, 450); //Set the location out of box to hide
                //ucen.txtAutoMapNumber.Location = new Point(102, 450); //Set the location out of box to hide

                ucen.chkbxEventEnable.Visible = true;
                ucen.chkbxEventEnable.Checked = false;
                ucen.chkbxEventEnable.Location = new Point(15, 235);

                ucen.btnDone.Visible = ucen.btnCancel.Visible = true;
                ucen.btnDone.Location = new Point(70, 265);
                ucen.btnCancel.Location = new Point(170, 265);

                ucen.btnFirst.Visible = ucen.btnPrev.Visible = ucen.btnNext.Visible = ucen.btnLast.Visible = true;
                ucen.btnFirst.Location = new Point(4, 300);
                ucen.btnPrev.Location = new Point(75, 300);
                ucen.btnNext.Location = new Point(155, 300);
                ucen.btnLast.Location = new Point(230, 300);

                ucen.grpEN.Height = 335;
                ucen.grpEN.Width = 290;
                ucen.grpEN.Location = new Point(172, 52);

                //ucen.chkbxEventEnable.Visible = false;  //Ajay: 27/07/2018

                //ucen.lblDataType.Visible = false;
                //ucen.cmbDataType.Visible = false;
                //ucen.grpEN.Location = new Point(172, 52);
                //ucen.cmbIEDName.Visible = false;
                //ucen.lblIEDName.Visible = false;
                //ucen.lbl61850Index.Visible = false;
                //ucen.cmb61850Index.Visible = false;
                //ucen.lbl61850ResponseType.Visible = false;
                //ucen.cmb61850ResponseType.Visible = false;
                //ucen.lblFC.Visible = false;
                //ucen.txtFC.Visible = false;

                //ucen.lblENNo.Location = new Point(14, 30);
                //ucen.lblResponseType.Location = new Point(14, 55);
                //ucen.lblIndex.Location = new Point(14, 81);
                //ucen.lblSubIndex.Location = new Point(14, 106);
                //ucen.lblMultiplier.Location = new Point(14, 131);
                //ucen.lblConstant.Location = new Point(14, 157);
                //ucen.lblDesc.Location = new Point(14, 182);
                //ucen.lblAutoMap.Location = new Point(14, 207);
                //ucen.txtAutoMapNumber.Enabled = false;
                //ucen.lblAutoMap.Enabled = false;
                //ucen.txtAutoMapNumber.Visible = false;
                //ucen.lblAutoMap.Visible = false;
                //ucen.txtENNo.Location = new Point(120, 27);
                //ucen.txtENNo.Size = new Size(174, 20);
                //ucen.cmbResponseType.Location = new Point(120, 52);
                //ucen.cmbResponseType.Size = new Size(174, 20);
                //ucen.txtIndex.Location = new Point(120, 78);
                //ucen.txtIndex.Size = new Size(174, 20);
                //ucen.txtSubIndex.Location = new Point(120, 103);
                //ucen.txtSubIndex.Size = new Size(174, 20);
                //ucen.txtMultiplier.Location = new Point(120, 128);
                //ucen.txtMultiplier.Size = new Size(174, 20);
                //ucen.txtConstant.Location = new Point(120, 154);
                //ucen.txtConstant.Size = new Size(174, 20);
                //ucen.txtDescription.Location = new Point(120, 179);
                //ucen.txtDescription.Size = new Size(174, 20);
                //ucen.btnDone.Location = new Point(121, 210);
                //ucen.btnCancel.Location = new Point(218, 210);
                //ucen.btnFirst.Location = new Point(20, 245);
                //ucen.btnNext.Location = new Point(89, 245);
                //ucen.btnPrev.Location = new Point(163, 245);
                //ucen.btnLast.Location = new Point(237, 245);
                //ucen.grpEN.Size = new Size(314, 270);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 03/09/2018
        public void IEC103_IEC104_ADR_Modbus_OnDoubleClick()
        {
            string strRoutineName = "EnabledisableEventsOnDoubleClick";
            try
            {
                foreach (Control c in ucen.grpEN.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucen.lblENNo.Visible = ucen.txtENNo.Visible = true;
                ucen.txtENNo.Enabled = false;
                ucen.lblENNo.Location = new Point(15, 35);
                ucen.txtENNo.Location = new Point(102, 30);

                ucen.lblResponseType.Visible = ucen.cmbResponseType.Visible = true;
                ucen.lblResponseType.Location = new Point(15, 60);
                ucen.cmbResponseType.Location = new Point(102, 55);

                ucen.lblIndex.Visible = ucen.txtIndex.Visible = true;
                ucen.lblIndex.Location = new Point(15, 85);
                ucen.txtIndex.Location = new Point(102, 80);

                ucen.lblSubIndex.Visible = ucen.txtSubIndex.Visible = true;
                ucen.lblSubIndex.Location = new Point(15, 110);
                ucen.txtSubIndex.Location = new Point(102, 105);

                ucen.lblDataType.Visible = ucen.cmbDataType.Visible = true;
                ucen.lblDataType.Location = new Point(15, 135);
                ucen.cmbDataType.Location = new Point(102, 130);

                ucen.lblMultiplier.Visible = ucen.txtMultiplier.Visible = true;
                ucen.lblMultiplier.Location = new Point(15, 160);
                ucen.txtMultiplier.Location = new Point(102, 155);

                ucen.lblConstant.Visible = ucen.txtConstant.Visible = true;
                ucen.lblConstant.Location = new Point(15, 185);
                ucen.txtConstant.Location = new Point(102, 180);

                ucen.lblDesc.Visible = ucen.txtDescription.Visible = true;
                ucen.lblDesc.Location = new Point(15, 210);
                ucen.txtDescription.Location = new Point(102, 205);

                ucen.lblAutoMap.Visible = ucen.txtAutoMapNumber.Visible = true;
                ucen.lblAutoMap.Location = new Point(15, 450); //Set the location out of box to hide
                ucen.txtAutoMapNumber.Location = new Point(102, 450); //Set the location out of box to hide

                ucen.chkbxEventEnable.Visible = true;
                ucen.chkbxEventEnable.Checked = false;
                ucen.chkbxEventEnable.Location = new Point(15, 235);

                ucen.btnDone.Visible = ucen.btnCancel.Visible = true;
                ucen.btnDone.Location = new Point(70, 260);
                ucen.btnCancel.Location = new Point(170, 260);

                ucen.btnFirst.Visible = ucen.btnPrev.Visible = ucen.btnNext.Visible = ucen.btnLast.Visible = true;
                ucen.btnFirst.Location = new Point(5, 295);
                ucen.btnPrev.Location = new Point(75, 295);
                ucen.btnNext.Location = new Point(155, 295);
                ucen.btnLast.Location = new Point(230, 295);

                ucen.grpEN.Height = 330;
                ucen.grpEN.Width = 290;
                ucen.grpEN.Location = new Point(172, 52);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 27/07/2018
        public void IEC104_OnDoubleClick()
        {
            ucen.chkbxEventEnable.Visible = true;  //Ajay: 27/07/2018
            ucen.chkbxEventEnable.Location = new Point(15, 210);  //Ajay: 27/07/2018

            ucen.lblDataType.Visible = false;
            ucen.cmbDataType.Visible = false;
            ucen.grpEN.Location = new Point(172, 52);
            ucen.cmbIEDName.Visible = false;
            ucen.lblIEDName.Visible = false;
            ucen.lbl61850Index.Visible = false;
            ucen.cmb61850Index.Visible = false;
            ucen.lbl61850ResponseType.Visible = false;
            ucen.cmb61850ResponseType.Visible = false;
            ucen.lblFC.Visible = false;
            //ucen.txtFC.Visible = false;

            ucen.lblENNo.Location = new Point(14, 30);
            ucen.lblResponseType.Location = new Point(14, 55);
            ucen.lblIndex.Location = new Point(14, 81);
            ucen.lblSubIndex.Location = new Point(14, 106);
            ucen.lblMultiplier.Location = new Point(14, 131);
            ucen.lblConstant.Location = new Point(14, 157);
            ucen.lblDesc.Location = new Point(14, 182);
            ucen.lblAutoMap.Location = new Point(14, 207);
            ucen.txtAutoMapNumber.Enabled = false;
            ucen.lblAutoMap.Enabled = false;
            ucen.txtAutoMapNumber.Visible = false;
            ucen.lblAutoMap.Visible = false;
            ucen.txtENNo.Location = new Point(120, 27);
            ucen.txtENNo.Size = new Size(174, 20);
            ucen.cmbResponseType.Location = new Point(120, 52);
            ucen.cmbResponseType.Size = new Size(174, 20);
            ucen.txtIndex.Location = new Point(120, 78);
            ucen.txtIndex.Size = new Size(174, 20);
            ucen.txtSubIndex.Location = new Point(120, 103);
            ucen.txtSubIndex.Size = new Size(174, 20);
            ucen.txtMultiplier.Location = new Point(120, 128);
            ucen.txtMultiplier.Size = new Size(174, 20);
            ucen.txtConstant.Location = new Point(120, 154);
            ucen.txtConstant.Size = new Size(174, 20);
            ucen.txtDescription.Location = new Point(120, 179);
            ucen.txtDescription.Size = new Size(174, 20);
            ucen.btnDone.Location = new Point(121, 210);
            ucen.btnCancel.Location = new Point(218, 210);
            ucen.btnFirst.Location = new Point(20, 245);
            ucen.btnNext.Location = new Point(89, 245);
            ucen.btnPrev.Location = new Point(163, 245);
            ucen.btnLast.Location = new Point(237, 245);
            ucen.grpEN.Size = new Size(314, 270);
        }
        //Ajay: 31/07/2018
        public void LoadProfile_OnDoubleClick()
        {
            string strRoutineName = "LoadProfileOnDoubleClick";
            try
            {
                foreach (Control c in ucen.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucen.lblENNo.Visible = ucen.txtENNo.Visible = true;
                ucen.lblENNo.Location = new Point(15, 35);
                ucen.txtENNo.Location = new Point(102, 30);

                ucen.lblName.Visible = ucen.cmbName.Visible = true;
                ucen.lblName.Location = new Point(15, 60);
                ucen.cmbName.Location = new Point(102, 55);

                ucen.lblAI1.Visible = ucen.cmbAI1.Visible = true;
                ucen.lblAI1.Location = new Point(15, 85);
                ucen.cmbAI1.Location = new Point(102, 80);

                ucen.lblEN1.Visible = ucen.cmbEN1.Visible = true;
                ucen.lblEN1.Location = new Point(15, 110);
                ucen.cmbEN1.Location = new Point(102, 105);

                ucen.lblDesc.Visible = ucen.txtDescription.Visible = true;
                ucen.lblDesc.Location = new Point(15, 135);
                ucen.txtDescription.Location = new Point(102, 130);

                ucen.chkbxEventEnable.Visible = true;
                ucen.chkbxEventEnable.Checked = false;
                ucen.chkbxEventEnable.Location = new Point(15, 165);

                //Ajay: 19/09/2018 LogEnable remove
                //ucen.chkbxLogEnable.Visible = true;
                //ucen.chkbxLogEnable.Checked = false;
                //ucen.chkbxLogEnable.Location = new Point(120, 165);
                ucen.chkbxLogEnable.Visible = false;

                ucen.btnDone.Visible = ucen.btnCancel.Visible = ucen.btnVerify.Visible = true;
                ucen.btnVerify.Location = new Point(15, 195);
                ucen.btnDone.Location = new Point(110, 195);
                ucen.btnCancel.Location = new Point(200, 195);

                ucen.btnFirst.Visible = ucen.btnPrev.Visible = ucen.btnNext.Visible = ucen.btnLast.Visible = true;
                ucen.btnFirst.Location = new Point(10, 235);
                ucen.btnPrev.Location = new Point(80, 235);
                ucen.btnNext.Location = new Point(160, 235);
                ucen.btnLast.Location = new Point(230, 235);

                ucen.grpEN.Height = 265;
                ucen.grpEN.Location = new Point(172, 52);
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
                foreach (Control c in ucen.grpEN.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucen.lblENNo.Visible = ucen.txtENNo.Visible = true;
                ucen.txtENNo.Enabled = false;
                ucen.lblENNo.Location = new Point(15, 35);
                ucen.txtENNo.Location = new Point(102, 30);
                ucen.txtENNo.Size = new Size(300, 21);

                ucen.lblIEDName.Visible = ucen.cmbIEDName.Visible = true;
                ucen.lblIEDName.Location = new Point(15, 60);
                ucen.cmbIEDName.Location = new Point(102, 55);
                ucen.cmbIEDName.Size = new Size(300, 21);

                ucen.lbl61850ResponseType.Visible = ucen.cmb61850ResponseType.Visible = true;
                ucen.lbl61850ResponseType.Location = new Point(15, 85);
                ucen.cmb61850ResponseType.Location = new Point(102, 80);
                ucen.cmb61850ResponseType.Size = new Size(300, 21);

                ucen.lblFC.Visible = ucen.cmbFC.Visible = true;
                //ucen.txtFC.Enabled = false;
                ucen.lblFC.Location = new Point(15, 110);
                ucen.cmbFC.Location = new Point(102, 105);
                ucen.cmbFC.Size = new Size(300, 21);

                ucen.lbl61850Index.Visible = ucen.cmb61850Index.Visible = false;
                ucen.lbl61850Index.Location = new Point(15, 135);
                ucen.cmb61850Index.Location = new Point(102, 130);
                ucen.cmb61850Index.Size = new Size(300, 21);

                //Namrata: 27/03/2019
                ucen.lbl61850Index.Visible = ucen.ChkIEC61850Index.Visible = true;
                ucen.lbl61850Index.Location = new Point(15, 135);
                ucen.ChkIEC61850Index.Location = new Point(102, 130);
                ucen.ChkIEC61850Index.Size = new Size(300, 21);

                ucen.lblSubIndex.Visible = ucen.txtSubIndex.Visible = true;
                ucen.lblSubIndex.Location = new Point(15, 160);
                ucen.txtSubIndex.Location = new Point(102, 155);
                ucen.txtSubIndex.Size = new Size(300, 21);

                ucen.lblMultiplier.Visible = ucen.txtMultiplier.Visible = true;
                ucen.lblMultiplier.Location = new Point(15, 185);
                ucen.txtMultiplier.Location = new Point(102, 180);
                ucen.txtMultiplier.Size = new Size(300, 21);

                ucen.lblConstant.Visible = ucen.txtConstant.Visible = true;
                ucen.lblConstant.Location = new Point(15, 210);
                ucen.txtConstant.Location = new Point(102, 205);
                ucen.txtConstant.Size = new Size(300, 21);

                ucen.lblDesc.Visible = ucen.txtDescription.Visible = true;
                ucen.lblDesc.Location = new Point(15, 235);
                ucen.txtDescription.Location = new Point(102, 230);
                ucen.txtDescription.Size = new Size(300, 21);

                ucen.lblAutoMap.Visible = ucen.txtAutoMapNumber.Visible = true;
                ucen.lblAutoMap.Location = new Point(15, 450); //Set the location out of box to hide
                ucen.txtAutoMapNumber.Location = new Point(102, 450); //Set the location out of box to hide
                ucen.txtAutoMapNumber.Size = new Size(300, 21);

                ucen.chkbxEventEnable.Visible = true;
                ucen.chkbxEventEnable.Checked = false;
                ucen.chkbxEventEnable.Location = new Point(15, 260);

                ucen.btnDone.Visible = ucen.btnCancel.Visible = true;
                ucen.btnDone.Location = new Point(135, 270);
                ucen.btnCancel.Location = new Point(235, 270);

                ucen.btnFirst.Visible = ucen.btnPrev.Visible = ucen.btnNext.Visible = ucen.btnLast.Visible = true;
                ucen.btnFirst.Location = new Point(80, 303);
                ucen.btnPrev.Location = new Point(150, 303);
                ucen.btnNext.Location = new Point(220, 303);
                ucen.btnLast.Location = new Point(290, 303);

                ucen.grpEN.Height = 335;
                ucen.grpEN.Width = 420;
                ucen.grpEN.Location = new Point(172, 52);
                ucen.pbHdr.Width = 510;
                //ucen.chkbxEventEnable.Visible = false;  //Ajay: 27/07/2018

                //ucen.grpEN.Location = new Point(172, 52);
                //ucen.lblResponseType.Visible = false;
                //ucen.lblIndex.Visible = false;
                //ucen.lblDataType.Visible = false;
                //ucen.cmbResponseType.Visible = false;
                //ucen.cmbResponseType.Visible = false;
                //ucen.txtIndex.Visible = false;
                //ucen.txtIndex.Visible = false;
                //ucen.cmbDataType.Visible = false;
                //ucen.cmbDataType.Visible = false;
                //ucen.cmbIEDName.Visible = true;
                //ucen.lblIEDName.Visible = true;
                //ucen.lbl61850Index.Visible = true;
                //ucen.cmb61850Index.Visible = true;
                //ucen.lbl61850ResponseType.Visible = true;
                //ucen.cmb61850ResponseType.Visible = true;
                //ucen.lblFC.Visible = true;
                //ucen.txtFC.Visible = true;

                //ucen.lblENNo.Location = new Point(14, 30);
                //ucen.lblIEDName.Location = new Point(14, 55);
                //ucen.lbl61850ResponseType.Location = new Point(14, 81);
                //ucen.lbl61850Index.Location = new Point(14, 106);
                //ucen.lblFC.Location = new Point(14, 131);
                //ucen.lblSubIndex.Location = new Point(14, 157);
                //ucen.lblMultiplier.Location = new Point(14, 182);
                //ucen.lblConstant.Location = new Point(14, 207);
                //ucen.lblDesc.Location = new Point(14, 232);
                //ucen.lblAutoMap.Visible = false;
                //ucen.txtENNo.Location = new Point(120, 27);
                //ucen.cmbIEDName.Location = new Point(120, 52);
                //ucen.cmb61850ResponseType.Location = new Point(120, 78);
                //ucen.cmb61850Index.Location = new Point(120, 103);
                //ucen.txtFC.Location = new Point(120, 128);
                //ucen.txtSubIndex.Location = new Point(120, 154);
                //ucen.txtMultiplier.Location = new Point(120, 179);
                //ucen.txtConstant.Location = new Point(120, 204);
                //ucen.txtDescription.Location = new Point(120, 229);
                //ucen.txtAutoMapNumber.Visible = false;

                //ucen.txtENNo.Size = new Size(300, 20);
                //ucen.txtSubIndex.Size = new Size(300, 20);
                //ucen.cmbIEDName.Size = new Size(300, 20);
                //ucen.cmb61850ResponseType.Size = new Size(300, 20);
                //ucen.cmb61850Index.Size = new Size(300, 20);
                //ucen.txtFC.Size = new Size(300, 20);
                //ucen.txtMultiplier.Size = new Size(300, 20);
                //ucen.txtConstant.Size = new Size(300, 20);
                //ucen.txtDescription.Size = new Size(300, 20);
                //ucen.txtAutoMapNumber.Size = new Size(300, 20);

                //ucen.btnDone.Location = new Point(170, 258);
                //ucen.btnCancel.Location = new Point(260, 258);
                //ucen.btnFirst.Location = new Point(90, 287);
                //ucen.btnPrev.Location = new Point(180, 287);
                //ucen.btnNext.Location = new Point(270, 287);
                //ucen.btnLast.Location = new Point(360, 287);


                ////ucen.btnDone.Location = new Point(170, 282);
                ////ucen.btnCancel.Location = new Point(260, 282);
                //ucen.grpEN.Size = new Size(440, 315);
                //ucen.pbHdr.Width = 440;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void IEC61850Client_OnLoad()
        {
            string strRoutineName = "IEC61850_OnLoad";
            try
            {
                foreach (Control c in ucen.grpEN.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucen.lblENNo.Visible = ucen.txtENNo.Visible = true;
                ucen.txtENNo.Enabled = false;
                ucen.lblENNo.Location = new Point(15, 35);
                ucen.txtENNo.Location = new Point(102, 30);
                ucen.txtENNo.Size = new Size(300, 21);

                ucen.lblIEDName.Visible = ucen.cmbIEDName.Visible = true;
                ucen.lblIEDName.Location = new Point(15, 60);
                ucen.cmbIEDName.Location = new Point(102, 55);
                ucen.cmbIEDName.Size = new Size(300, 21);

                ucen.lbl61850ResponseType.Visible = ucen.cmb61850ResponseType.Visible = true;
                ucen.lbl61850ResponseType.Location = new Point(15, 85);
                ucen.cmb61850ResponseType.Location = new Point(102, 80);
                ucen.cmb61850ResponseType.Size = new Size(300, 21);

                ucen.lblFC.Visible = ucen.cmbFC.Visible = true;
                //ucen.cmbFC.Enabled = false;
                ucen.lblFC.Location = new Point(15, 110);
                ucen.cmbFC.Location = new Point(102, 105);
                ucen.cmbFC.Size = new Size(300, 21);

                ucen.lbl61850Index.Visible = ucen.cmb61850Index.Visible = false;
                ucen.lbl61850Index.Location = new Point(15, 135);
                ucen.cmb61850Index.Location = new Point(102, 130);
                ucen.cmb61850Index.Size = new Size(300, 21);

                //Namrata:27/03/2019
                ucen.lbl61850Index.Visible = ucen.ChkIEC61850Index.Visible = true;
                ucen.lbl61850Index.Location = new Point(15, 135);
                ucen.ChkIEC61850Index.Location = new Point(102, 130);
                ucen.ChkIEC61850Index.Size = new Size(300, 21);

                ucen.lblSubIndex.Visible = ucen.txtSubIndex.Visible = true;
                ucen.lblSubIndex.Location = new Point(15, 160);
                ucen.txtSubIndex.Location = new Point(102, 155);
                ucen.txtSubIndex.Size = new Size(300, 21);

                ucen.lblMultiplier.Visible = ucen.txtMultiplier.Visible = true;
                ucen.lblMultiplier.Location = new Point(15, 185);
                ucen.txtMultiplier.Location = new Point(102, 180);
                ucen.txtMultiplier.Size = new Size(300, 21);

                ucen.lblConstant.Visible = ucen.txtConstant.Visible = true;
                ucen.lblConstant.Location = new Point(15, 210);
                ucen.txtConstant.Location = new Point(102, 205);
                ucen.txtConstant.Size = new Size(300, 21);

                ucen.lblDesc.Visible = ucen.txtDescription.Visible = true;
                ucen.lblDesc.Location = new Point(15, 235);
                ucen.txtDescription.Location = new Point(102, 230);
                ucen.txtDescription.Size = new Size(300, 21);

                ucen.lblAutoMap.Visible = ucen.txtAutoMapNumber.Visible = false;
                ucen.lblAutoMap.Location = new Point(15, 260);
                ucen.txtAutoMapNumber.Location = new Point(102, 255);
                ucen.txtAutoMapNumber.Size = new Size(300, 21);

                ucen.chkbxEventEnable.Visible = true;
                ucen.chkbxEventEnable.Checked = false;
                ucen.chkbxEventEnable.Location = new Point(19, 262);

                ucen.btnDone.Visible = ucen.btnCancel.Visible = true;
                ucen.btnDone.Location = new Point(135, 270);
                ucen.btnCancel.Location = new Point(235, 270);

                ucen.grpEN.Height = 310;
                ucen.grpEN.Width = 420;
                ucen.grpEN.Location = new Point(172, 52);
                ucen.pbHdr.Width = 510;
                //ucen.chkbxEventEnable.Visible = false; //Ajay: 27/07/2018 

                //ucen.grpEN.Location = new Point(172, 52);
                //ucen.lblResponseType.Visible = false;
                //ucen.lblIndex.Visible = false;
                //ucen.lblDataType.Visible = false;
                //ucen.cmbResponseType.Visible = false;
                //ucen.cmbResponseType.Visible = false;
                //ucen.txtIndex.Visible = false;
                //ucen.txtIndex.Visible = false;
                //ucen.cmbDataType.Visible = false;
                //ucen.cmbDataType.Visible = false;
                //ucen.cmbIEDName.Visible = true;
                //ucen.lblIEDName.Visible = true;
                //ucen.lbl61850Index.Visible = true;
                //ucen.cmb61850Index.Visible = true;
                //ucen.lbl61850ResponseType.Visible = true;
                //ucen.cmb61850ResponseType.Visible = true;
                //ucen.lblfc.Visible = true;
                //ucen.txtFC.Visible = true;

                //ucen.lblENNo.Location = new Point(14, 30);
                //ucen.lblIEDName.Location = new Point(14, 55);
                //ucen.lbl61850ResponseType.Location = new Point(14, 81);
                //ucen.lbl61850Index.Location = new Point(14, 106);
                //ucen.lblfc.Location = new Point(14, 131);
                //ucen.lblSubIndex.Location = new Point(14, 157);
                //ucen.lblMultiplier.Location = new Point(14, 182);
                //ucen.lblConstant.Location = new Point(14, 207);
                //ucen.lblDesc.Location = new Point(14, 232);
                //ucen.lblAutoMap.Location = new Point(14, 257);


                //ucen.txtENNo.Location = new Point(120, 27);
                //ucen.cmbIEDName.Location = new Point(120, 52);
                //ucen.cmb61850ResponseType.Location = new Point(120, 78);
                //ucen.cmb61850Index.Location = new Point(120, 103);
                //ucen.txtFC.Location = new Point(120, 128);
                //ucen.txtSubIndex.Location = new Point(120, 154);
                //ucen.txtMultiplier.Location = new Point(120, 179);
                //ucen.txtConstant.Location = new Point(120, 204);
                //ucen.txtDescription.Location = new Point(120, 229);
                //ucen.txtAutoMapNumber.Location = new Point(120, 254);

                ////ucen.txtSubIndex.Location = new Point(120, 52);
                ////ucen.cmbIEDName.Location = new Point(120, 78);
                ////ucen.cmb61850ResponseType.Location = new Point(120, 103);
                ////ucen.cmb61850Index.Location = new Point(120, 128);
                ////ucen.txtFC.Location = new Point(120, 154);
                ////ucen.txtMultiplier.Location = new Point(120, 179);
                ////ucen.txtConstant.Location = new Point(120, 204);
                ////ucen.txtDescription.Location = new Point(120, 229);
                ////ucen.txtAutoMapNumber.Location = new Point(120, 254);

                //ucen.lblAutoMap.Visible = true;
                //ucen.txtAutoMapNumber.Visible = true;
                //ucen.lblAutoMap.Enabled = true;
                //ucen.txtAutoMapNumber.Enabled = true;
                //ucen.txtENNo.Size = new Size(300,20);
                //ucen.txtSubIndex.Size = new Size(300, 20);
                //ucen.cmbIEDName.Size = new Size(300, 20);
                //ucen.cmb61850ResponseType.Size = new Size(300, 20);
                //ucen.cmb61850Index.Size = new Size(300, 20);
                //ucen.txtFC.Size = new Size(300, 20);
                //ucen.txtMultiplier.Size = new Size(300, 20);
                //ucen.txtConstant.Size = new Size(300, 20);
                //ucen.txtDescription.Size = new Size(300, 20);
                //ucen.txtAutoMapNumber.Size = new Size(300, 20);
                //ucen.btnDone.Location = new Point(170, 282);
                //ucen.btnCancel.Location = new Point(260, 282);
                //ucen.grpEN.Size = new Size(440, 320);
                //ucen.pbHdr.Width = 440;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 12/11/2018
        public void IEC101_OnLoad()
        {
            string strRoutineName = "IEC101_OnLoad";
            try
            {
                foreach (Control c in ucen.grpEN.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucen.lblENNo.Visible = ucen.txtENNo.Visible = true;
                ucen.txtENNo.Enabled = false;
                ucen.lblENNo.Location = new Point(15, 35);
                ucen.txtENNo.Location = new Point(102, 30);

                ucen.lblResponseType.Visible = ucen.cmbResponseType.Visible = true;
                ucen.lblResponseType.Location = new Point(15, 60);
                ucen.cmbResponseType.Location = new Point(102, 55);

                ucen.lblIndex.Visible = ucen.txtIndex.Visible = true;
                ucen.lblIndex.Location = new Point(15, 85);
                ucen.txtIndex.Location = new Point(102, 80);

                ucen.lblSubIndex.Visible = ucen.txtSubIndex.Visible = true;
                ucen.lblSubIndex.Location = new Point(15, 110);
                ucen.txtSubIndex.Location = new Point(102, 105);

                ucen.lblDataType.Visible = ucen.cmbDataType.Visible = true;
                ucen.lblDataType.Location = new Point(15, 135);
                ucen.cmbDataType.Location = new Point(102, 130);

                ucen.lblMultiplier.Visible = ucen.txtMultiplier.Visible = true;
                ucen.lblMultiplier.Location = new Point(15, 160);
                ucen.txtMultiplier.Location = new Point(102, 155);

                ucen.lblConstant.Visible = ucen.txtConstant.Visible = true;
                ucen.lblConstant.Location = new Point(15, 185);
                ucen.txtConstant.Location = new Point(102, 180);

                ucen.lblDesc.Visible = ucen.txtDescription.Visible = true;
                ucen.lblDesc.Location = new Point(15, 210);
                ucen.txtDescription.Location = new Point(102, 205);

                ucen.lblAutoMap.Visible = ucen.txtAutoMapNumber.Visible = true;
                ucen.lblAutoMap.Location = new Point(15, 235);
                ucen.txtAutoMapNumber.Location = new Point(102, 230);

                ucen.chkbxEventEnable.Visible = true;
                ucen.chkbxEventEnable.Checked = false;
                ucen.chkbxEventEnable.Location = new Point(15, 260);

                ucen.btnDone.Visible = ucen.btnCancel.Visible = true;
                ucen.btnDone.Location = new Point(65, 290);
                ucen.btnCancel.Location = new Point(165, 290);

                ucen.grpEN.Height = 330;
                ucen.grpEN.Width = 290;
                ucen.grpEN.Location = new Point(172, 52);

                //ucen.chkbxEventEnable.Visible = false; //Ajay: 27/07/2018 

                //ucen.lblDataType.Visible = false;
                //ucen.cmbDataType.Visible = false;
                //ucen.grpEN.Location = new Point(172, 52);
                //ucen.cmbIEDName.Visible = false;
                //ucen.lblIEDName.Visible = false;
                //ucen.lbl61850Index.Visible = false;
                //ucen.cmb61850Index.Visible = false;
                //ucen.lbl61850ResponseType.Visible = false;
                //ucen.cmb61850ResponseType.Visible = false;
                //ucen.lblfc.Visible = false;
                //ucen.txtFC.Visible = false;

                //ucen.lblENNo.Location = new Point(14, 30);
                //ucen.lblResponseType.Location = new Point(14, 55);
                //ucen.lblIndex.Location = new Point(14, 81);
                //ucen.lblSubIndex.Location = new Point(14, 106);
                //ucen.lblMultiplier.Location = new Point(14, 131);
                //ucen.lblConstant.Location = new Point(14, 157);
                //ucen.lblDesc.Location = new Point(14, 182);
                //ucen.lblAutoMap.Location = new Point(14, 207);
                //ucen.txtAutoMapNumber.Enabled = true;
                //ucen.lblAutoMap.Enabled = true;

                //ucen.txtENNo.Location = new Point(120, 27);
                //ucen.txtENNo.Size = new Size(174, 20);
                //ucen.cmbResponseType.Location = new Point(120, 52);
                //ucen.cmbResponseType.Size = new Size(174, 20);
                //ucen.txtIndex.Location = new Point(120, 78);
                //ucen.txtIndex.Size = new Size(174, 20);
                //ucen.txtSubIndex.Location = new Point(120, 103);
                //ucen.txtSubIndex.Size = new Size(174, 20);
                //ucen.txtMultiplier.Location = new Point(120, 128);
                //ucen.txtMultiplier.Size = new Size(174, 20);
                //ucen.txtConstant.Location = new Point(120, 154);
                //ucen.txtConstant.Size = new Size(174, 20);
                //ucen.txtDescription.Location = new Point(120, 179);
                //ucen.txtDescription.Size = new Size(174, 20);
                //ucen.txtAutoMapNumber.Location = new Point(120, 204);
                //ucen.txtAutoMapNumber.Size = new Size(174, 20);

                //ucen.btnDone.Location = new Point(120, 230);
                //ucen.btnCancel.Location = new Point(217, 230);
                //ucen.grpEN.Size = new Size(313, 270);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void SPORTMaster_IEC101Master_OnLoad()
        {
            //Ajay: 02/09/2018
            string strRoutineName = "IEC101_SPORT_OnLoad";
            try
            {
                foreach (Control c in ucen.grpEN.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucen.lblENNo.Visible = ucen.txtENNo.Visible = true;
                ucen.txtENNo.Enabled = false;
                ucen.lblENNo.Location = new Point(15, 35);
                ucen.txtENNo.Location = new Point(102, 30);

                ucen.lblResponseType.Visible = ucen.cmbResponseType.Visible = true;
                ucen.lblResponseType.Location = new Point(15, 60);
                ucen.cmbResponseType.Location = new Point(102, 55);

                ucen.lblIndex.Visible = ucen.txtIndex.Visible = true;
                ucen.lblIndex.Location = new Point(15, 85);
                ucen.txtIndex.Location = new Point(102, 80);

                ucen.lblSubIndex.Visible = ucen.txtSubIndex.Visible = true;
                ucen.lblSubIndex.Location = new Point(15, 110);
                ucen.txtSubIndex.Location = new Point(102, 105);

                ucen.lblDataType.Visible = ucen.cmbDataType.Visible = true;
                ucen.lblDataType.Location = new Point(15, 135);
                ucen.cmbDataType.Location = new Point(102, 130);

                ucen.lblMultiplier.Visible = ucen.txtMultiplier.Visible = true;
                ucen.lblMultiplier.Location = new Point(15, 160);
                ucen.txtMultiplier.Location = new Point(102, 155);

                ucen.lblConstant.Visible = ucen.txtConstant.Visible = true;
                ucen.lblConstant.Location = new Point(15, 185);
                ucen.txtConstant.Location = new Point(102, 180);

                ucen.lblDesc.Visible = ucen.txtDescription.Visible = true;
                ucen.lblDesc.Location = new Point(15, 210);
                ucen.txtDescription.Location = new Point(102, 205);

                ucen.lblAutoMap.Visible = ucen.txtAutoMapNumber.Visible = true;
                ucen.lblAutoMap.Location = new Point(15, 235);
                ucen.txtAutoMapNumber.Location = new Point(102, 230);

                ucen.chkbxEventEnable.Visible = true;
                ucen.chkbxEventEnable.Checked = false;
                ucen.chkbxEventEnable.Location = new Point(15, 260);

                ucen.btnDone.Visible = ucen.btnCancel.Visible = true;
                ucen.btnDone.Location = new Point(65, 295);
                ucen.btnCancel.Location = new Point(165, 295);

                ucen.grpEN.Height = 335;
                ucen.grpEN.Width = 290;
                ucen.grpEN.Location = new Point(172, 52);

                //ucen.chkbxEventEnable.Visible = false; //Ajay: 27/07/2018 

                //ucen.lblDataType.Visible = false;
                //ucen.cmbDataType.Visible = false;
                //ucen.grpEN.Location = new Point(172, 52);
                //ucen.cmbIEDName.Visible = false;
                //ucen.lblIEDName.Visible = false;
                //ucen.lbl61850Index.Visible = false;
                //ucen.cmb61850Index.Visible = false;
                //ucen.lbl61850ResponseType.Visible = false;
                //ucen.cmb61850ResponseType.Visible = false;
                //ucen.lblfc.Visible = false;
                //ucen.txtFC.Visible = false;

                //ucen.lblENNo.Location = new Point(14, 30);
                //ucen.lblResponseType.Location = new Point(14, 55);
                //ucen.lblIndex.Location = new Point(14, 81);
                //ucen.lblSubIndex.Location = new Point(14, 106);
                //ucen.lblMultiplier.Location = new Point(14, 131);
                //ucen.lblConstant.Location = new Point(14, 157);
                //ucen.lblDesc.Location = new Point(14, 182);
                //ucen.lblAutoMap.Location = new Point(14, 207);
                //ucen.txtAutoMapNumber.Enabled = true;
                //ucen.lblAutoMap.Enabled = true;

                //ucen.txtENNo.Location = new Point(120, 27);
                //ucen.txtENNo.Size = new Size(174, 20);
                //ucen.cmbResponseType.Location = new Point(120, 52);
                //ucen.cmbResponseType.Size = new Size(174, 20);
                //ucen.txtIndex.Location = new Point(120, 78);
                //ucen.txtIndex.Size = new Size(174, 20);
                //ucen.txtSubIndex.Location = new Point(120, 103);
                //ucen.txtSubIndex.Size = new Size(174, 20);
                //ucen.txtMultiplier.Location = new Point(120, 128);
                //ucen.txtMultiplier.Size = new Size(174, 20);
                //ucen.txtConstant.Location = new Point(120, 154);
                //ucen.txtConstant.Size = new Size(174, 20);
                //ucen.txtDescription.Location = new Point(120, 179);
                //ucen.txtDescription.Size = new Size(174, 20);
                //ucen.txtAutoMapNumber.Location = new Point(120, 204);
                //ucen.txtAutoMapNumber.Size = new Size(174, 20);

                //ucen.btnDone.Location = new Point(120, 230);
                //ucen.btnCancel.Location = new Point(217, 230);
                //ucen.grpEN.Size = new Size(313, 270);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 27/07/2018
        public void EnableEventsIEC104OnLoad()
        {
            ucen.chkbxEventEnable.Visible = true; //Ajay: 27/07/2018
            ucen.chkbxEventEnable.Location = new Point(15, 260); //Ajay: 27/07/2018

            ucen.grpEN.Location = new Point(172,52);
            ucen.cmbIEDName.Visible = false;
            ucen.lblIEDName.Visible = false;
            ucen.lbl61850Index.Visible = false;
            ucen.cmb61850Index.Visible = false;
            ucen.lbl61850ResponseType.Visible = false;
            ucen.cmb61850ResponseType.Visible = false;
            ucen.lblFC.Visible = false;
            //ucen.txtFC.Visible = false;

            ucen.lblENNo.Location = new Point(14, 30);
            ucen.lblResponseType.Location = new Point(14, 55);
            ucen.lblIndex.Location = new Point(14, 81);
            ucen.lblSubIndex.Location = new Point(14, 106);
            ucen.lblDataType.Location = new Point(14, 131);
            ucen.lblMultiplier.Location = new Point(14, 157);
            ucen.lblConstant.Location = new Point(14, 182);
            ucen.lblDesc.Location = new Point(14, 207);
            ucen.lblAutoMap.Location = new Point(14, 232);

            ucen.txtENNo.Location = new Point(120, 27);
            ucen.txtENNo.Size = new Size(174, 20);
            ucen.cmbResponseType.Location = new Point(120, 52);
            ucen.cmbResponseType.Size = new Size(174, 20);
            ucen.txtIndex.Location = new Point(120, 78);
            ucen.txtIndex.Size = new Size(174, 20);
            ucen.txtSubIndex.Location = new Point(120, 103);
            ucen.txtSubIndex.Size = new Size(174, 20);
            ucen.cmbDataType.Location = new Point(120, 128);
            ucen.cmbDataType.Size = new Size(174, 20);
            ucen.txtMultiplier.Location = new Point(120, 154);
            ucen.txtMultiplier.Size = new Size(174, 20);
            ucen.txtConstant.Location = new Point(120, 179);
            ucen.txtConstant.Size = new Size(174, 20);
            ucen.txtDescription.Location = new Point(120, 204);
            ucen.txtDescription.Size = new Size(174, 20);
            ucen.txtAutoMapNumber.Visible = true;
            ucen.lblAutoMap.Visible = true;
            ucen.txtAutoMapNumber.Enabled = true;
            ucen.lblAutoMap.Enabled = true;
            ucen.txtAutoMapNumber.Location = new Point(120, 229);
            ucen.txtAutoMapNumber.Size = new Size(174, 20);
            ucen.btnDone.Location = new Point(120, 260);
            ucen.btnCancel.Location = new Point(217, 260);
            ucen.grpEN.Size = new Size(313, 300);
        }
        public void MasterIEC103_IEC104_ADR_Modbus_OnLoad()
        {
            //Ajay: 30/08/2018
            string strRoutineName = "IEC103_IEC104_ADR_Modbus_OnLoad";
            try
            {
                foreach (Control c in ucen.grpEN.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucen.lblENNo.Visible = ucen.txtENNo.Visible = true;
                ucen.txtENNo.Enabled = false;
                ucen.lblENNo.Location = new Point(15, 35);
                ucen.txtENNo.Location = new Point(102, 30);

                ucen.lblResponseType.Visible = ucen.cmbResponseType.Visible = true;
                ucen.lblResponseType.Location = new Point(15, 60);
                ucen.cmbResponseType.Location = new Point(102, 55);

                ucen.lblIndex.Visible = ucen.txtIndex.Visible = true;
                ucen.lblIndex.Location = new Point(15, 85);
                ucen.txtIndex.Location = new Point(102, 80);

                ucen.lblSubIndex.Visible = ucen.txtSubIndex.Visible = true;
                ucen.lblSubIndex.Location = new Point(15, 110);
                ucen.txtSubIndex.Location = new Point(102, 105);

                ucen.lblDataType.Visible = ucen.cmbDataType.Visible = true;
                ucen.lblDataType.Location = new Point(15, 135);
                ucen.cmbDataType.Location = new Point(102, 130);

                ucen.lblMultiplier.Visible = ucen.txtMultiplier.Visible = true;
                ucen.lblMultiplier.Location = new Point(15, 160);
                ucen.txtMultiplier.Location = new Point(102, 155);

                ucen.lblConstant.Visible = ucen.txtConstant.Visible = true;
                ucen.lblConstant.Location = new Point(15, 185);
                ucen.txtConstant.Location = new Point(102, 180);

                ucen.lblDesc.Visible = ucen.txtDescription.Visible = true;
                ucen.lblDesc.Location = new Point(15, 210);
                ucen.txtDescription.Location = new Point(102, 205);

                ucen.lblAutoMap.Visible = ucen.txtAutoMapNumber.Visible = true;
                ucen.lblAutoMap.Location = new Point(15, 235);
                ucen.txtAutoMapNumber.Location = new Point(102, 230);

                ucen.chkbxEventEnable.Visible = true;
                ucen.chkbxEventEnable.Checked = false;
                ucen.chkbxEventEnable.Location = new Point(15, 260);

                ucen.btnDone.Visible = ucen.btnCancel.Visible = true;
                ucen.btnDone.Location = new Point(70, 290);
                ucen.btnCancel.Location = new Point(170, 290);

                ucen.grpEN.Height = 330;
                ucen.grpEN.Width = 290;
                ucen.grpEN.Location = new Point(172, 52);

                //ucen.ChkEvent.Visible = false; //Ajay: 27/07/2018

                //ucen.grpEN.Location = new Point(172,52);
                //ucen.cmbIEDName.Visible = false;
                //ucen.lblIEDName.Visible = false;
                //ucen.LblIndex61850.Visible = false;
                //ucen.cmb61850Index.Visible = false;
                //ucen.LblRespType.Visible = false;
                //ucen.cmb61850ResponseType.Visible = false;
                //ucen.lblfc.Visible = false;
                //ucen.txtFC.Visible = false;

                //ucen.lblENNo.Location = new Point(14, 30);
                //ucen.lblRT.Location = new Point(14, 55);
                //ucen.lblIdx.Location = new Point(14, 81);
                //ucen.lblSIdx.Location = new Point(14, 106);
                //ucen.lblDT.Location = new Point(14, 131);
                //ucen.lblM.Location = new Point(14, 157);
                //ucen.lblC.Location = new Point(14, 182);
                //ucen.lblDesc.Location = new Point(14, 207);
                //ucen.lblAutomapNumber.Location = new Point(14, 232);

                //ucen.txtENNo.Location = new Point(120, 27);
                //ucen.txtENNo.Size = new Size(174, 20);
                //ucen.cmbResponseType.Location = new Point(120, 52);
                //ucen.cmbResponseType.Size = new Size(174, 20);
                //ucen.txtIndex.Location = new Point(120, 78);
                //ucen.txtIndex.Size = new Size(174, 20);
                //ucen.txtSubIndex.Location = new Point(120, 103);
                //ucen.txtSubIndex.Size = new Size(174, 20);
                //ucen.cmbDataType.Location = new Point(120, 128);
                //ucen.cmbDataType.Size = new Size(174, 20);
                //ucen.txtMultiplier.Location = new Point(120, 154);
                //ucen.txtMultiplier.Size = new Size(174, 20);
                //ucen.txtConstant.Location = new Point(120, 179);
                //ucen.txtConstant.Size = new Size(174, 20);
                //ucen.txtDescription.Location = new Point(120, 204);
                //ucen.txtDescription.Size = new Size(174, 20);
                //ucen.txtAutoMapNumber.Visible = true;
                //ucen.lblAutomapNumber.Visible = true;
                //ucen.txtAutoMapNumber.Enabled = true;
                //ucen.lblAutomapNumber.Enabled = true;
                //ucen.txtAutoMapNumber.Location = new Point(120, 229);
                //ucen.txtAutoMapNumber.Size = new Size(174, 20);
                //ucen.btnDone.Location = new Point(120, 260);
                //ucen.btnCancel.Location = new Point(217, 260);
                //ucen.grpEN.Size = new Size(313, 300);
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
                foreach (Control c in ucen.grpEN.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                ucen.lblENNo.Visible = ucen.txtENNo.Visible = true;
                ucen.lblENNo.Location = new Point(15, 35);
                ucen.txtENNo.Location = new Point(102, 30);

                ucen.lblName.Visible = ucen.cmbName.Visible = true;
                ucen.lblName.Location = new Point(15, 60);
                ucen.cmbName.Location = new Point(102, 55);

                ucen.lblAI1.Visible = ucen.cmbAI1.Visible = true;
                ucen.lblAI1.Location = new Point(15, 85);
                ucen.cmbAI1.Location = new Point(102, 80);

                ucen.lblEN1.Visible = ucen.cmbEN1.Visible = true;
                ucen.lblEN1.Location = new Point(15, 110);
                ucen.cmbEN1.Location = new Point(102, 105);

                ucen.lblDesc.Visible = ucen.txtDescription.Visible = true;
                ucen.lblDesc.Location = new Point(15, 135);
                ucen.txtDescription.Location = new Point(102, 130);

                ucen.chkbxEventEnable.Visible = true;
                ucen.chkbxEventEnable.Checked = false;
                ucen.chkbxEventEnable.Location = new Point(15, 165);

                //Ajay: 19/09/2018 LogEnable remove
                //ucen.chkbxLogEnable.Visible = true;
                //ucen.chkbxLogEnable.Checked = false;
                //ucen.chkbxLogEnable.Location = new Point(120, 165);
                ucen.chkbxLogEnable.Visible = false;

                ucen.btnDone.Visible = ucen.btnCancel.Visible = ucen.btnVerify.Visible = true;
                ucen.btnVerify.Location = new Point(15, 195);
                ucen.btnDone.Location = new Point(110, 195);
                ucen.btnCancel.Location = new Point(200, 195);

                ucen.grpEN.Height = 235;
                ucen.grpEN.Width = 290;
                ucen.grpEN.Location = new Point(172, 52);

                //ucen.lvENlistDoubleClick += new System.EventHandler(this.lvENlist_DoubleClick);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ShowHideSlaveColumnsSPORT()
        {
            string strRoutineName = "ShowHideSlaveColumnsSPORT";
            try
            {
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.SPORTSLAVE) Utils.getColumnHeader(ucen.lvENMap, "Unit ID").Width = COL_CMD_TYPE_WIDTH;
                else Utils.getColumnHeader(ucen.lvENMap, "Unit ID").Width = 0; //Hide...
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.SPORTSLAVE) Utils.getColumnHeader(ucen.lvENMap, "Used").Width = COL_CMD_TYPE_WIDTH;
                else Utils.getColumnHeader(ucen.lvENMap, "Used").Width = 0; //Hide...
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ShowHideSlaveColumns()
        {
            if (ucen.lvENMap.InvokeRequired)
            {
                ucen.lvENMap.Invoke(new MethodInvoker(delegate
                {
                    if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE) || Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE) Utils.getColumnHeader(ucen.lvENMap, "Command Type").Width = COL_CMD_TYPE_WIDTH;
                    else Utils.getColumnHeader(ucen.lvENMap, "Command Type").Width = 0;//Hide...
                    //Ajay: 07 / 09 / 2018
                    switch (Utils.getSlaveTypes(currentSlave))
                    {
                        case SlaveTypes.IEC101SLAVE:
                            Utils.getColumnHeader(ucen.lvENMap, "Event").Width = 80;
                            break;
                        case SlaveTypes.IEC104:
                        case SlaveTypes.SPORTSLAVE:
                            Utils.getColumnHeader(ucen.lvENMap, "Event").Width = 80;
                            break;
                        case SlaveTypes.MQTTSLAVE:
                            break;
                        default:
                            Utils.getColumnHeader(ucen.lvENMap, "Event").Width = 0;
                            break;
                    }
                }));
            }
            else
            {
                if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE) || Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE) Utils.getColumnHeader(ucen.lvENMap, "Command Type").Width = COL_CMD_TYPE_WIDTH;
                else Utils.getColumnHeader(ucen.lvENMap, "Command Type").Width = 0;//Hide...
                //Ajay: 07 / 09 / 2018
                switch (Utils.getSlaveTypes(currentSlave))
                {
                    case SlaveTypes.IEC101SLAVE:
                        Utils.getColumnHeader(ucen.lvENMap, "Event").Width = 80;
                        break;
                    case SlaveTypes.IEC104:
                    case SlaveTypes.SPORTSLAVE:
                        Utils.getColumnHeader(ucen.lvENMap, "Event").Width = 80;
                        break;
                    case SlaveTypes.MQTTSLAVE:
                        break;
                    default:
                        Utils.getColumnHeader(ucen.lvENMap, "Event").Width = 0;
                        break;
                }
            }
            //if (ucen.lvENMap.InvokeRequired)

            //{

            //    ucen.lvENMap.Invoke(new MethodInvoker(delegate
            //    {
            //        if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE) || Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE) Utils.getColumnHeader(ucen.lvENMap, "Command Type").Width = COL_CMD_TYPE_WIDTH;
            //        else Utils.getColumnHeader(ucen.lvENMap, "Command Type").Width = 0;//Hide...
            //        Ajay: 07 / 09 / 2018
            //        switch (Utils.getSlaveTypes(currentSlave))
            //        {
            //            case SlaveTypes.IEC101SLAVE:
            //                Utils.getColumnHeader(ucen.lvENMap, "Event").Width = 80;
            //                break;
            //            case SlaveTypes.IEC104:
            //            case SlaveTypes.SPORTSLAVE:
            //                Utils.getColumnHeader(ucen.lvENMap, "Event").Width = 80;
            //                break;
            //            case SlaveTypes.MQTTSLAVE:
            //                break;
            //            default:
            //                Utils.getColumnHeader(ucen.lvENMap, "Event").Width = 0;
            //                break;
            //        }
            //    }));

            //}



            //if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE) || Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE) Utils.getColumnHeader(ucen.lvENMap, "Command Type").Width = COL_CMD_TYPE_WIDTH;
            //else Utils.getColumnHeader(ucen.lvENMap, "Command Type").Width = 0;//Hide...
            ////Ajay: 07/09/2018
            //switch (Utils.getSlaveTypes(currentSlave))
            //{
            //    case SlaveTypes.IEC101SLAVE:
            //    case SlaveTypes.IEC104:
            //    case SlaveTypes.SPORTSLAVE:
            //        Utils.getColumnHeader(ucen.lvENMap, "Event").Width = 80;
            //        break;
            //    //case SlaveTypes.MQTTSLAVE:
            //    //    break;
            //    default:
            //        Utils.getColumnHeader(ucen.lvENMap, "Event").Width = 0;
            //        break;
            //}
        }
        private void ShowHideSlaveColumnsDNP()
        {
            string strRoutineName = "ENConfiguration:ShowHideSlaveColumnsDNP";
            try
            {
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE) Utils.getColumnHeader(ucen.lvENMap, "Event Class").Width = COL_CMD_TYPE_WIDTH;
                else Utils.getColumnHeader(ucen.lvENMap, "Event Class").Width = 0; //Hide...
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE) Utils.getColumnHeader(ucen.lvENMap, "Variation").Width = COL_CMD_TYPE_WIDTH;
                else Utils.getColumnHeader(ucen.lvENMap, "Variation").Width = 0; //Hide...
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE) Utils.getColumnHeader(ucen.lvENMap, "Event Variation").Width = COL_CMD_TYPE_WIDTH;
                else Utils.getColumnHeader(ucen.lvENMap, "Event Variation").Width = 0; //Hide...
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.DNP3SLAVE) Utils.getColumnHeader(ucen.lvENMap, "Data Type").Width = 0;
                else Utils.getColumnHeader(ucen.lvENMap, "Data Type").Width = COL_CMD_TYPE_WIDTH; //Hide...
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ShowHideSlaveColumnsMQTT()
        {
            string strRoutineName = "ENConfiguration:ShowHideSlaveColumnsMQTT";
            try
            {
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MQTTSLAVE) Utils.getColumnHeader(ucen.lvENMap, "Unit").Width = COL_CMD_TYPE_WIDTH;
                else Utils.getColumnHeader(ucen.lvENMap, "Unit").Width = 0; //Hide...
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MQTTSLAVE) Utils.getColumnHeader(ucen.lvENMap, "Key").Width = COL_CMD_TYPE_WIDTH;
                else Utils.getColumnHeader(ucen.lvENMap, "Key").Width = 0; //Hide...
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.SMSSLAVE) Utils.getColumnHeader(ucen.lvENMap, "Unit").Width = COL_CMD_TYPE_WIDTH;

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

            rootNode = xmlDoc.CreateElement("ENConfiguration");
            xmlDoc.AppendChild(rootNode);

            foreach (EN en in enList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(en.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }

            return rootNode;
        }
        public string exportXML()
        {
            XmlNode xmlNode = exportXMLnode();
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);

            xmlTextWriter.Formatting = Formatting.Indented;
            xmlTextWriter.Indentation = 2; //default is 1.
            xmlNode.WriteTo(xmlTextWriter);
            xmlTextWriter.Flush();

            return stringWriter.ToString();
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

            rootNode = xmlDoc.CreateElement("ENMap");
            xmlDoc.AppendChild(rootNode);

            List<ENMap> slaveENMapList;
            if (!slavesENMapList.TryGetValue(slaveID, out slaveENMapList))
            {
                Console.WriteLine("##### Slave entries for {0} does not exists", slaveID);
                return rootNode;
            }
            foreach (ENMap enmn in slaveENMapList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(enmn.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }

            return rootNode;
        }
        public string exportMapXML(String slaveID)
        {
            XmlNode xmlNode = exportMapXMLnode(slaveID);
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);

            xmlTextWriter.Formatting = Formatting.Indented;
            xmlTextWriter.Indentation = 2; //default is 1.
            xmlNode.WriteTo(xmlTextWriter);
            xmlTextWriter.Flush();

            return stringWriter.ToString();
        }
        public string exportINI(string slaveNum, string slaveID, string element, ref int ctr)
        {
            string iniData = "";

            List<ENMap> slaveENMapList;
            if (!slavesENMapList.TryGetValue(slaveID, out slaveENMapList))
            {
                Console.WriteLine("EN INI: ##### Slave entries for {0} does not exists", slaveID);
                return iniData;
            }
            if (element == "DeadBandEN")
            {
                foreach (ENMap enmn in slaveENMapList)
                {
                    iniData += "DeadBand_" + ctr++ + "=" + Utils.GetDataTypeShortNotation(enmn.DataType) + "," + enmn.ReportingIndex + "," + enmn.Deadband + Environment.NewLine;
                }
            }
            else if (element == "EN")
            {
                foreach (ENMap enmn in slaveENMapList)
                {
                    int ri;
                    try
                    {
                        ri = Int32.Parse(enmn.ReportingIndex);
                    }
                    catch (System.FormatException)
                    {
                        ri = 0;
                    }
                    if (slaveENMapList.Where(x => x.ReportingIndex == ri.ToString()).Select(x => x).Count() > 1) //Ajay: 10/01/2019
                    {
                        MessageBox.Show("Duplicate Reporting Index (" + enmn.ReportingIndex + ") found of EN No " + enmn.ENNo, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        iniData += "AI_" + ctr++ + "=" + Utils.GenerateIndex("AI", Utils.GetDataTypeIndex(enmn.DataType), ri).ToString() + Environment.NewLine;//EN data returned as AI...
                    }
                }

            }

            return iniData;
        }
        //Ajay: 10/01/2019
        public string exportINI_DeadBandEN(string slaveNum, string slaveID, string element, ref int ctr)
        {
            string iniData = "";

            List<ENMap> slaveENMapList;
            if (!slavesENMapList.TryGetValue(slaveID, out slaveENMapList))
            {
                Console.WriteLine("EN INI: ##### Slave entries for {0} does not exists", slaveID);
                return iniData;
            }

            if (element == "DeadBandEN")
            {
                foreach (ENMap enmn in slaveENMapList)
                {
                    iniData += "DeadBand_" + ctr++ + "=" + Utils.GetDataTypeShortNotation(enmn.DataType) + "," + enmn.ReportingIndex + "," + enmn.Deadband + Environment.NewLine;
                }
            }
            else if (element == "EN")
            {
                foreach (ENMap enmn in slaveENMapList)
                {
                    int ri;
                    try
                    {
                        ri = Int32.Parse(enmn.ReportingIndex);
                    }
                    catch (System.FormatException)
                    {
                        ri = 0;
                    }
                    iniData += "AI_" + ctr++ + "=" + Utils.GenerateIndex("AI", Utils.GetDataTypeIndex(enmn.DataType), ri).ToString() + Environment.NewLine;//EN data returned as AI...
                }
            }

            return iniData;
        }
        public void regenerateENSequence()
        {
            foreach (EN enn in enList)
            {
                int oENNo = Int32.Parse(enn.ENNo);
                //Namrata: 30/10/2017
                //int nENNo = oENNo;
                int nENNo = Globals.ENNo++; //Ajay: 22/09/2018
                enn.ENNo = nENNo.ToString();

                //Now change in map...
                foreach (KeyValuePair<string, List<ENMap>> maps in slavesENMapList)
                {
                    //Ajay: 10/01/2019 Commented
                    //List<ENMap> senmList = maps.Value;
                    //foreach (ENMap enm in senmList)
                    //{
                    //    if (enm.ENNo == oENNo.ToString() && !enm.IsReindexed)
                    //    {
                    //        //Ajay: 30/07/2018 if same EN mapped again it should take same EN no on reindex. 
                    //        //enm.ENNo = nENNo.ToString();
                    //        //enm.IsReindexed = true;
                    //        senmList.Where(x => x.ENNo == oENNo.ToString()).ToList().ForEach(x => { x.ENNo = nENNo.ToString(); x.IsReindexed = true; });
                    //        break;
                    //    }
                    //}

                    //Ajay: 10/01/2019 Reindexing issues reported by Aditya K. mail dtd. 27-12-2018
                    maps.Value.OfType<ENMap>().Where(x => x.ENNo == oENNo.ToString() && !x.IsReindexed).ToList().ForEach(x => { x.ENNo = nENNo.ToString(); x.IsReindexed = true; });
                }

                //Now change in Parameter Load nodes...
                Utils.getOpenProPlusHandle().getParameterLoadConfiguration().ChangeENSequence(oENNo, nENNo);
            }
            //Reset reindexing status, for next use...
            foreach (KeyValuePair<string, List<ENMap>> maps in slavesENMapList)
            {
                List<ENMap> senmList = maps.Value;
                foreach (ENMap enm in senmList)
                {
                    enm.IsReindexed = false;
                }
            }
            refreshList();
            refreshCurrentMapList();
        }
        public int GetReportingIndex(string slaveNum, string slaveID, int value)
        {
            int ret = 0;

            List<ENMap> slaveENMapList;
            if (!slavesENMapList.TryGetValue(slaveID, out slaveENMapList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                return ret;
            }

            foreach (ENMap enm in slaveENMapList)
            {
                if (enm.ENNo == value.ToString()) return Int32.Parse(enm.ReportingIndex);
            }

            return ret;
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("EN_"))
            {
                //If a IEC104 slave added/deleted, reflect in UI as well as objects.
                CheckIEC104SlaveStatusChanges();
                //If a MODBUS slave added/deleted, reflect in UI as well as objects.
                CheckMODBUSSlaveStatusChanges();
                CheckIEC61850SlaveStatusChanges();
                //Namarta:13/7/2017
                CheckIEC101SlaveStatusChanges();
                CheckSPORTSlaveStatusChanges();
                CheckMQTTSlaveStatusChanges();
                CheckSMSSlaveStatusChanges();
                CheckGDisplaySlaveStatusChanges();
                CheckDNPSlaveStatusChanges();
                ShowHideSlaveColumns();
                ShowHideSlaveColumnsSPORT();
                ShowHideSlaveColumnsMQTT();
                ShowHideSlaveColumnsDNP();
                return ucen;
            }

            return null;
        }
        public void parseENCNode(XmlNode encNode, bool imported)
        {
            if (encNode == null)
            {
                rnName = "ENConfiguration";
                return;
            }

            //First set root node name...
            rnName = encNode.Name;

            if (encNode.NodeType == XmlNodeType.Comment)
            {
                isNodeComment = true;
                comment = encNode.Value;
            }

            foreach (XmlNode node in encNode)
            {
                //Console.WriteLine("***** node type: {0}", node.NodeType);
                if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                enList.Add(new EN(node, masterType, masterNo, IEDNo, imported));
            }
            refreshList();
        }
        public void parseENMNode(string slaveNum, string slaveID, XmlNode enmNode)
        {
            Task thDetails = new Task(() => CreateNewSlave(slaveNum, slaveID, enmNode));
            thDetails.Start();
            //CreateNewSlave(slaveNum, slaveID, enmNode);
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public int getCount()
        {
            int ctr = 0;
            foreach (EN enNode in enList)
            {
                if (enNode.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }
        public List<EN> getENs()
        {
            return enList;
        }
        //Namrata:27/7/2017
        public int getENMapCount()
        {
            int ctr = 0;
            fillMapOptions(Utils.getSlaveTypes(currentSlave));
            List<ENMap> senmList;
            if (!slavesENMapList.TryGetValue(currentSlave, out senmList))
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
                foreach (ENMap asaa in senmList)
                {
                    if (asaa.IsNodeComment) continue;
                    ctr++;
                }
            }
            return ctr;
        }
        public List<ENMap> getSlaveENMaps(string slaveID)
        {
            List<ENMap> slaveENMapList;
            slavesENMapList.TryGetValue(slaveID, out slaveENMapList);
            return slaveENMapList;
        }
        public class ListViewItemComparer : IComparer
        {
            private int col;
            private SortOrder order;
            private int dataType = 0;
            public ListViewItemComparer()
            {
                col = 0;
                order = SortOrder.Ascending;
            }
            public ListViewItemComparer(int column, SortOrder order, int ColType)
            {
                col = column;
                this.order = order;
                dataType = ColType;
            }
            public int Compare(object x, object y)
            {
                int returnVal = -1;
                if (dataType == 0)
                {
                    //string
                    returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
                }
                else if (dataType == 1)
                {
                    //int
                    string ix = "0", iy = "0";
                    if (((ListViewItem)x).SubItems[col].Text == "") ix = "0";
                    else ix = ((ListViewItem)x).SubItems[col].Text;
                    if (((ListViewItem)y).SubItems[col].Text == "") iy = "0";
                    else iy = ((ListViewItem)y).SubItems[col].Text;
                    returnVal = CompareInt(ix, iy);
                }
                // Determine whether the sort order is descending.
                // Invert the value returned by String.Compare.
                if (order == SortOrder.Descending) returnVal *= -1;
                return returnVal;
            }
            private int CompareInt(string x, string y)
            {
                int ix, iy;
                try
                {
                    ix = Int32.Parse(x);
                }
                catch (System.FormatException)
                {
                    ix = 0;
                }
                try
                {
                    iy = Int32.Parse(y);
                }
                catch (System.FormatException)
                {
                    iy = 0;
                }
                return ix - iy;
            }
        }
        //Ajay: 10/10/2018
        private void cmbAI1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cmb = (ComboBox)sender;
                int i = 0;
                Int32.TryParse(cmb.Text, out i);
                if (i > 0)
                {
                   ucen.cmbEN1.Text = "";
                   ucen.cmbEN1.Enabled = false;
                }
                else
                {
                    cmb.Text = "";
                    cmb.Enabled = true;
                    ucen.cmbEN1.Text = "";
                    ucen.cmbEN1.Enabled = true;
                }
            }
            catch { }
        }
        //Ajay: 10/10/2018
        private void cmbEN1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cmb = (ComboBox)sender;
                int i = 0;
                Int32.TryParse(cmb.Text, out i);
                if (i > 0)
                {
                    ucen.cmbAI1.Text = "";
                    ucen.cmbAI1.Enabled = false;
                }
                else
                {
                    cmb.Text = "";
                    cmb.Enabled = true;
                    ucen.cmbAI1.Text = "";
                    ucen.cmbAI1.Enabled = true;
                }
            }
            catch { }
        }
    }
}


