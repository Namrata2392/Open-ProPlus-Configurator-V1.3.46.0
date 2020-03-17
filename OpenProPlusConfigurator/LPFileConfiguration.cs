//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml;
//using System.IO;
//using System.Windows.Forms;
//using System.Drawing;
//using System.Data;

//namespace OpenProPlusConfigurator
//{
//    /**
//    * \brief     <b>LPFileConfiguration</b> is a class to store info about all LPFiles and there corresponding mapping infos.
//    * \details   This class stores info related to all LPFiles and there corresponding mapping's for various slaves. It allows
//    * user to add multiple LPFiles. The user can map this LPFiles to various slaves created in the system, along with the mapping parameters
//    * for those slave types. It also exports the XML node related to this object.
//    * 
//    */
//    public class LPFileConfiguration
//    {
//        #region Declaration
//        private enum Mode
//        {
//            NONE,
//            ADD,
//            EDIT
//        }
//        private string rnName = "";
//        private Mode mode = Mode.NONE;
//        private int editIndex = -1;
//        private bool isNodeComment = false;
//        private string comment = "";
//        private MasterTypes masterType = MasterTypes.UNKNOWN;
//        private int masterNo = -1;
//        private int IEDNo = -1;
//        private const int COL_CMD_TYPE_WIDTH = 130;
//        //Fill RessponseType in All Configuration . 
//        public DataGridView dataGridViewDataSet = new DataGridView();
//        public DataTable dtdataset = new DataTable();
//        List<LPFile> LPFileList = new List<LPFile>();
//        ucLPFilelist uclpfile = new ucLPFilelist();
//        DataSet DsRCB = new DataSet();
//        #endregion Declaration
//        public LPFileConfiguration(MasterTypes mType, int mNo, int iNo)
//        {
//            string strRoutineName = "LPFileConfiguration";
//            try
//            {
//                masterType = mType;
//                masterNo = mNo;
//                IEDNo = iNo;
//                uclpfile.ucLPFilelistLoad += new System.EventHandler(this.ucLPFilelist_Load);
//                uclpfile.btnAddClick += new System.EventHandler(this.btnAdd_Click);
//                uclpfile.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
//                uclpfile.btnVerifyClick += new EventHandler(this.btnVerify_Click); //Ajay: 31/07/2018
//                uclpfile.btnDoneClick += new System.EventHandler(this.btnDone_Click);
//                uclpfile.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
//                uclpfile.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
//                uclpfile.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
//                uclpfile.btnNextClick += new System.EventHandler(this.btnNext_Click);
//                uclpfile.btnLastClick += new System.EventHandler(this.btnLast_Click);
//                uclpfile.linkLPFileClick += new System.EventHandler(this.linkLPFile_Click);
//                uclpfile.lvLPFilelistItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvLPFilelist_ItemSelectionChanged);
//                //Ajay: 10/10/2018
//                uclpfile.cmbAINoTextChanged += new System.EventHandler(this.cmbAINo_TextChanged);
//                uclpfile.cmbENNoTextChanged += new System.EventHandler(this.cmbENNo_TextChanged);

//                if (mType == MasterTypes.LoadProfile)
//                {
//                    LoadProfile_OnLoad();
//                }
//                addListHeaders();
//                fillOptions();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        //Ajay: 07/12/2018
//        private bool AllowMasterOptionsOnClick(MasterTypes mstrType)
//        {
//            try
//            {
//                switch (mstrType)
//                {
//                    case MasterTypes.ADR:
//                        if (ProtocolGateway.OppADRGroup_ReadOnly) { return true; }
//                        else { return false; }
//                    case MasterTypes.IEC101:
//                        if (ProtocolGateway.OppIEC101Group_ReadOnly) { return true; }
//                        else { return false; }
//                    case MasterTypes.IEC103:
//                        if (ProtocolGateway.OppIEC103Group_ReadOnly) { return true; }
//                        else { return false; }
//                    case MasterTypes.MODBUS:
//                        if (ProtocolGateway.OppMODBUSGroup_ReadOnly) { return true; }
//                        else { return false; }
//                    case MasterTypes.IEC61850Client:
//                        if (ProtocolGateway.OppIEC61850Group_ReadOnly) { return true; }
//                        else { return false; }
//                    case MasterTypes.IEC104:
//                        if (ProtocolGateway.OppIEC104Group_ReadOnly) { return true; }
//                        else { return false; }
//                    case MasterTypes.SPORT:
//                        if (ProtocolGateway.OppSPORTGroup_ReadOnly) { return true; }
//                        else { return false; }
//                    case MasterTypes.Virtual:
//                        if (ProtocolGateway.OppVirtualGroup_ReadOnly) { return true; }
//                        else { return false; }
//                    case MasterTypes.LoadProfile:
//                        if (ProtocolGateway.OppLoadProfileGroup_ReadOnly) { return true; }
//                        else { return false; }
//                    default:
//                        return false;
//                }
//            }
//            catch { return false; }
//        }
//        //Ajay: 07/12/2018
//        private bool AllowSlaveOptionsOnClick(SlaveTypes slvType)
//        {
//            try
//            {
//                switch (slvType)
//                {
//                    case SlaveTypes.IEC101SLAVE:
//                        if (ProtocolGateway.OppIEC101SlaveGroup_ReadOnly) { return true; }
//                        else { return false; }
//                    case SlaveTypes.IEC104:
//                        if (ProtocolGateway.OppIEC104SlaveGroup_ReadOnly) { return true; }
//                        else { return false; }
//                    case SlaveTypes.IEC61850Server:
//                        if (ProtocolGateway.OppIEC61850SlaveGroup_ReadOnly) { return true; }
//                        else { return false; }
//                    case SlaveTypes.MODBUSSLAVE:
//                        if (ProtocolGateway.OppMODBUSSlaveGroup_ReadOnly) { return true; }
//                        else { return false; }
//                    case SlaveTypes.SPORTSLAVE:
//                        if (ProtocolGateway.OppSPORTSlaveGroup_ReadOnly) { return true; }
//                        else { return false; }
//                    default:
//                        return false;
//                }
//            }
//            catch { return false; }
//        }
//        private int SelectedIndex;
//        private void lvLPFilelist_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            Color GreenColour = Color.FromArgb(82, 208, 23);
//            if (uclpfile.lvLPFilelist.SelectedIndices.Count > 0)
//            {
//                SelectedIndex = Convert.ToInt32(uclpfile.lvLPFilelist.SelectedItems[0].Text);
//                uclpfile.lvLPFilelist.SelectedItems.Clear();
//                uclpfile.lvLPFilelist.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
//                uclpfile.lvLPFilelist.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
//                uclpfile.lvLPFilelist.Items.Cast<ListViewItem>().Where(x => x.Text == SelectedIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);
//            }
//        }
//        private void ucLPFilelist_Load(object sender, EventArgs e)
//        {
//            string strRoutineName = "uclpfilelist_Load";
//            try
//            {
//                foreach (var L in Utils.VirtualPLU)
//                {
//                    if (L.Run == "YES")
//                    {
//                        uclpfile.btnAdd.Enabled = true;
//                        uclpfile.btnDelete.Enabled = true;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void btnAdd_Click(object sender, EventArgs e)
//        {
//            string strRoutinename = "btnAdd_Click";
//            try
//            {
//                // Ajay: 07/12/2018
//                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
//                {
//                    if (AllowMasterOptionsOnClick(masterType)) { return; }
//                    else { }
//                }

//                if (LPFileList.Count >= getMaxLPFiles())
//                {
//                    MessageBox.Show("Maximum " + getMaxLPFiles() + " LPFiles are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    return;
//                }
//                mode = Mode.ADD;
//                editIndex = -1;
//                if (masterType == MasterTypes.LoadProfile)
//                {
//                    LoadProfile_OnLoad();
//                }
//                Utils.resetValues(uclpfile.grpLPFile);
//                Utils.showNavigation(uclpfile.grpLPFile, false);
//                fillOptions(); //Ajay: 24/09/2018
//                loadDefaults();
//                uclpfile.grpLPFile.Visible = true;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutinename + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void linkLPFile_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "linkDO_Click";
//            try
//            {
//                // Ajay: 07/12/2018
//                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
//                {
//                    if (AllowMasterOptionsOnClick(masterType)) { return; }
//                    else { }
//                }

//                foreach (ListViewItem listItem in uclpfile.lvLPFilelist.Items)
//                {
//                    listItem.Checked = true;
//                }
//                DialogResult result = MessageBox.Show("Do you want to delete all records ? ", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
//                if (result == DialogResult.No)
//                {
//                    foreach (ListViewItem listItem in uclpfile.lvLPFilelist.Items)
//                    {
//                        listItem.Checked = false;
//                    }
//                    return;
//                }
//                for (int i = uclpfile.lvLPFilelist.Items.Count - 1; i >= 0; i--)
//                {
//                    Console.WriteLine("*** removing indices: {0}", i);
//                    if (Utils.IsExistDIinPLC(LPFileList.ElementAt(i).LPFileNo))
//                    {
//                        DialogResult ask = MessageBox.Show("DI " + LPFileList.ElementAt(i).LPFileNo + " is referred in ParameterLoadConfiguration and all the references will also be deleted." + Environment.NewLine + "Do you want to continue?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
//                        if (ask == DialogResult.No)
//                        {
//                            continue;
//                        }
//                        Utils.DeleteDIFromPLC(LPFileList.ElementAt(i).LPFileNo);
//                    }
//                    LPFileList.RemoveAt(i);
//                    uclpfile.lvLPFilelist.Items.Clear();
//                }
//                Console.WriteLine("*** diList count: {0} lv count: {1}", LPFileList.Count, uclpfile.lvLPFilelist.Items.Count);
//                refreshList();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void btnDelete_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "btnDelete_Click";
//            try
//            {
//                // Ajay: 07/12/2018
//                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
//                {
//                    if (AllowMasterOptionsOnClick(masterType)) { return; }
//                    else { }
//                }

//                var LitsItemsChecked = new List<KeyValuePair<int, int>>();
//                DialogResult result = MessageBox.Show(Globals.PROMPT_DELETE_ENTRY, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
//                if (result == DialogResult.No)
//                {
//                    return;
//                }
//                for (int i = uclpfile.lvLPFilelist.Items.Count - 1; i >= 0; i--)
//                {
//                    if (uclpfile.lvLPFilelist.Items[i].Checked)
//                    {
//                        if (Utils.IsExistDIinPLC(LPFileList.ElementAt(i).LPFileNo))
//                        {
//                            DialogResult ask = MessageBox.Show("DI " + LPFileList.ElementAt(i).LPFileNo + " is referred in ParameterLoadConfiguration and all the references will also be deleted." + Environment.NewLine + "Do you want to continue?", "Delete DI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
//                            if (ask == DialogResult.No)
//                            {
//                                continue;
//                            }
//                            Utils.DeleteDIFromPLC(LPFileList.ElementAt(i).LPFileNo);
//                        }
//                        LPFileList.RemoveAt(i);
//                        uclpfile.lvLPFilelist.Items[i].Remove();
//                    }
//                }
//                refreshList();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void btnDone_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "btnDone_Click";
//            try
//            {
//                if (masterType == MasterTypes.LoadProfile)
//                {
//                    string[] str = new string[2];
//                    if (!IsVerified(uclpfile.cmbAINo, "AINo", out str))
//                    {
//                        string dlgMsg = str[0];
//                        if (!string.IsNullOrEmpty(str[1])) { dlgMsg += "=" + str[1]; }
//                        dlgMsg += " is not valid." + Environment.NewLine + "Do you want to continue?";
//                        DialogResult rslt = MessageBox.Show(dlgMsg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
//                        if (rslt.ToString().ToLower() == "no") { return; }
//                    }
//                    if (!IsVerified(uclpfile.cmbENNo, "ENNo", out str))
//                    {
//                        string dlgMsg = str[0];
//                        if (!string.IsNullOrEmpty(str[1])) { dlgMsg += "=" + str[1]; }
//                        dlgMsg += " is not valid." + Environment.NewLine + "Do you want to continue?";
//                        DialogResult rslt = MessageBox.Show(dlgMsg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
//                        if (rslt.ToString().ToLower() == "no") { return; }
//                    }
//                }

//                Utils.diList1.Clear();
//                Console.WriteLine("*** ucai btnDone_Click clicked in class!!!");
//                List<KeyValuePair<string, string>> LPFileData = Utils.getKeyValueAttributes(uclpfile.grpLPFile);
//                if (mode == Mode.ADD)
//                {
//                    //[#] changes as per LPFileData
//                    //Ajay: 10/10/2018 Commented
//                    //int intStart = Convert.ToInt32(LPFileData[3].Value); // LPFileNo

//                    //Ajay: 10/10/2018 Commented
//                    //Ajay: 31/07/2018
//                    //int intRange = 0;
//                    //if (masterType == MasterTypes.LoadProfile) { intRange = 1; }
//                    //else { intRange = Convert.ToInt32(LPFileData[12].Value); } //AutoMapRange

//                    //Ajay: 10/10/2018 Commented
//                    //Ajay: 31/07/2018
//                    //int intDIIndex = 0;
//                    //if (masterType != MasterTypes.LoadProfile) { intDIIndex = Convert.ToInt32(LPFileData[17].Value); }

//                    //int DINumber1 = 0, DIINdex1 = 0; //Ajay: 10/10/2018 Commented
//                    //if (intRange > getMaxLPFiles())  //Ajay: 10/10/2018 Commented
//                    if ((LPFileList.Count + 1) > getMaxLPFiles())
//                    {
//                        MessageBox.Show("Maximum " + getMaxLPFiles() + " LPFiles are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                        return;
//                    }
//                    else
//                    {
//                        //for (int i = intStart; i <= intStart + intRange - 1; i++)
//                        //{
//                            //DINumber1 = Globals.LPFileNo; //Ajay: 10/10/2018 Commented
//                            //DINumber1 += 1; //Ajay: 10/10/2018 Commented
//                            //DIINdex1 = intDIIndex++; //Ajay: 10/10/2018 Commented
//                            LPFile NewLPFile = new LPFile("PARAMETER", LPFileData, null, masterType, masterNo, IEDNo);
//                            //NewLPFile.LPFileNo = DINumber1.ToString(); //Ajay: 10/10/2018 Commented
//                            LPFileList.Add(NewLPFile);
//                            //Globals.LPFileNo++; //Ajay: 10/10/2018 Commented
//                        //}
//                    }
//                    Utils.LPFileList1.AddRange(LPFileList);
//                }
//                else if (mode == Mode.EDIT)
//                {
//                    LPFileList[editIndex].updateAttributes(LPFileData);
//                }
//                refreshList();
//                if (sender != null && e != null)
//                {
//                    uclpfile.grpLPFile.Visible = false;
//                    mode = Mode.NONE;
//                    editIndex = -1;
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void btnVerify_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "btnVerify_Click";
//            try
//            {
//                string[] str = new string[2];
//                if (!IsVerified(uclpfile.cmbAINo, "AINo", out str) || !IsVerified(uclpfile.cmbENNo, "ENNo", out str))
//                {
//                    string dlgMsg = str[0];
//                    if (!string.IsNullOrEmpty(str[1])) { dlgMsg += "=" + str[1]; }
//                    dlgMsg += " is not valid.";
//                    MessageBox.Show(dlgMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                }
//                else
//                {
//                    MessageBox.Show("AINo & ENNo are valid.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
//                }
//            }
//            catch (Exception ex) { MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error); }
//        }
//        private bool IsVerified(ComboBox cmb, string param, out string[] str)
//        {
//            str = new string[2];
//            string strRoutineName = "IsVerified()";
//            try
//            {
//                int iParam = 0;
//                int.TryParse(cmb.Text, out iParam);
//                if (param.Contains("AI"))
//                {
//                    if (!Utils.GetAllAIList().Contains(iParam))
//                    {
//                        str[0] = param; str[1] = iParam.ToString(); return false;
//                    }
//                }
//                else if (param.Contains("EN"))
//                {
//                    if (!Utils.GetAllENList().Contains(iParam))
//                    {
//                        str[0] = param; str[1] = iParam.ToString(); return false;
//                    }
//                }
//                return true;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return false;
//            }
//        }
//        private void btnCancel_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "btnCancel_Click";
//            try
//            {
//                Console.WriteLine("*** uclpfile btnCancel_Click clicked in class!!!");
//                uclpfile.grpLPFile.Visible = false;
//                mode = Mode.NONE;
//                editIndex = -1;
//                Utils.resetValues(uclpfile.grpLPFile);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void btnFirst_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "btnFirst_Click";
//            try
//            {
//                Console.WriteLine("*** uclpfile btnFirst_Click clicked in class!!!");
//                if (uclpfile.lvLPFilelist.Items.Count <= 0) return;
//                if (LPFileList.ElementAt(0).IsNodeComment) return;
//                editIndex = 0;
//                loadValues();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void btnPrev_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "btnPrev_Click";
//            try
//            {
//                //Namrata: 27/7/2017
//                btnDone_Click(null, null);
//                Console.WriteLine("*** uclpfile btnPrev_Click clicked in class!!!");
//                if (editIndex - 1 < 0) return;
//                if (LPFileList.ElementAt(editIndex - 1).IsNodeComment) return;
//                editIndex--;
//                loadValues();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void btnNext_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "btnNext_Click";
//            try
//            {
//                //Namrata: 27/7/2017
//                btnDone_Click(null, null);
//                Console.WriteLine("*** uclpfile btnNext_Click clicked in class!!!");
//                if (editIndex + 1 >= uclpfile.lvLPFilelist.Items.Count) return;
//                if (LPFileList.ElementAt(editIndex + 1).IsNodeComment) return;
//                editIndex++;
//                loadValues();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void btnLast_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "btnLast_Click";
//            try
//            {
//                Console.WriteLine("*** uclpfile btnLast_Click clicked in class!!!");
//                if (uclpfile.lvLPFilelist.Items.Count <= 0) return;
//                if (LPFileList.ElementAt(LPFileList.Count - 1).IsNodeComment) return;
//                editIndex = LPFileList.Count - 1;
//                loadValues();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void lvLPFilelist_DoubleClick(object sender, EventArgs e)
//        {
//            string strRoutineName = "lvLPFilelist_DoubleClick";
//            try
//            {
//                // Ajay: 07/12/2018
//                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
//                {
//                    if (AllowMasterOptionsOnClick(masterType)) { return; }
//                    else { }
//                }

//                int ListIndex = uclpfile.lvLPFilelist.FocusedItem.Index;
//                ListViewItem lvi = uclpfile.lvLPFilelist.Items[ListIndex];//ucai.lvAIlist.SelectedItems[0];
//                Utils.UncheckOthers(uclpfile.lvLPFilelist, lvi.Index);
//                if (LPFileList.ElementAt(lvi.Index).IsNodeComment)
//                {
//                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    return;
//                }

//                if (masterType == MasterTypes.LoadProfile)
//                {
//                    LoadProfile_OnDoubleClick();
//                }
//                uclpfile.grpLPFile.Visible = true;
//                mode = Mode.EDIT;
//                editIndex = lvi.Index;
//                Utils.showNavigation(uclpfile.grpLPFile, true);
//                loadValues();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void lvLPFilelist_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
//        {
//            string strRoutineName = "lvLPFilelist_ItemSelectionChanged";
//            try
//            {
//                if (e.IsSelected)
//                {
//                    Color GreenColour = Color.FromArgb(34, 217, 0);
//                    string diIndex = e.Item.Text;
//                    Console.WriteLine("*** selected DI: {0}", diIndex);
//                    uclpfile.lvLPFilelist.SelectedItems.Clear();
//                    uclpfile.lvLPFilelist.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
//                    uclpfile.lvLPFilelist.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
//                    uclpfile.lvLPFilelist.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);

//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }     
//        private void loadDefaults()
//        {
//            string strRoutineName = "loadDefaults";
//            try
//            {
//                //Ajay: 10/10/2018 Commented
//                //uclpfile.txtLPFileNo.Text = (Globals.LPFileNo + 1).ToString();

//                if (uclpfile.cmbAINo.Items.Count > 0) { uclpfile.cmbAINo.SelectedIndex = 0; }
//                if (uclpfile.cmbENNo.Items.Count > 0) { uclpfile.cmbENNo.SelectedIndex = 0; }
//                //Ajay: 10/10/2018 Commented
//                //if (uclpfile.cmbParamCode.Items.Count > 0) { uclpfile.cmbParamCode.SelectedIndex = 0; }
//                //Ajay: 26/12/2018
//                //uclpfile.txtName.Text = ""; //Ajay: 10/10/2018
//                if (uclpfile.cmbName.Items.Count > 0) { uclpfile.cmbName.SelectedIndex = 0; } //Ajay: 26/12/2018
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void loadValues()
//        {
//            string strRoutineName = "loadValues";
//            try
//            {
//                LPFile lp = LPFileList.ElementAt(editIndex);
//                if (lp != null)
//                {
//                    //uclpfile.txtLPFileNo.Text = lp.LPFileNo; //Ajay: 10/10/2018 Commented
//                    uclpfile.cmbAINo.SelectedIndex = uclpfile.cmbAINo.FindStringExact(lp.AINo);
//                    uclpfile.cmbENNo.SelectedIndex = uclpfile.cmbENNo.FindStringExact(lp.ENNo);
//                    //Ajay: 10/10/2018 Commented
//                    //uclpfile.cmbParamCode.SelectedIndex = uclpfile.cmbParamCode.FindStringExact(di.PARAMCODE);
//                    //Ajay: 26/12/2018    
//                    //uclpfile.txtName.Text = lp.Name; //Ajay: 10/10/2018
//                    uclpfile.cmbName.SelectedIndex = uclpfile.cmbName.FindStringExact(lp.Name); //Ajay: 26/12/2018  
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private bool Validate()
//        {
//            bool status = true;
//            //Check empty field's
//            if (Utils.IsEmptyFields(uclpfile.grpLPFile))
//            {
//                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return false;
//            }
//            return status;
//        }
//        private int getMaxLPFiles()
//        {
//            if (masterType == MasterTypes.LoadProfile) return Globals.MaxLoadProfileLPFile;
//            else return 0;
//        }
//        public void refreshList()
//        {
//            string strRoutineName = "LPFileConfiguration: refreshList";
//            try
//            {
//                int cnt = 0;
//                uclpfile.lvLPFilelist.Items.Clear();
//                Utils.LPFilelistforDescription.Clear();

//                if (masterType == MasterTypes.LoadProfile)
//                {
//                    foreach (LPFile lp in LPFileList)
//                    {
//                        string[] row = new string[3];
//                        if (lp.IsNodeComment)
//                        {
//                            row[0] = "Comment...";
//                        }
//                        else
//                        {
//                            //row[0] = lp.LPFileNo;
//                            row[0] = lp.AINo;
//                            row[1] = lp.ENNo;
//                            //row[2] = lp.PARAMCODE; //Ajay: 10/10/2018 Commented
//                            row[2] = lp.Name;
//                        }
//                        ListViewItem lvItem = new ListViewItem(row);
//                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
//                        uclpfile.lvLPFilelist.Items.Add(lvItem);
//                    }
//                }

//                uclpfile.lblDIRecords.Text = LPFileList.Count.ToString();
//                Utils.LPFilelistRegenerateIndex.AddRange(LPFileList); // For DI Reindex
//                //Utils.DummyDI.AddRange(LPFileList); //For VirtualDI AutomaticEnteries
//                //Utils.EnableDI.AddRange(LPFileList); // Validate DI exist in DOConfiguration
//                Utils.LPFilelistforDescription.AddRange(LPFileList); //For Description not present in XML 
//                //Utils.DiListForVirualDIImport.AddRange(LPFileList); 

//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void fillOptions()
//        {
//            string strRoutineName = "LPFile : fillOptions";
//            try
//            {
//                if (masterType == MasterTypes.LoadProfile)
//                {
//                    //Ajay: 24/09/2018 Commented CLEAR()
//                    //uclpfile.cmbAINo.Items.Clear();
//                    //uclpfile.cmbENNo.Items.Clear();
//                    //uclpfile.cmbParamCode.Items.Clear();
//                    //uclpfile.cmbAINo.DataSource = Utils.GetAllDIList();

//                    //Ajay: 10/10/2018 GetAllAIList() GetAllENList()
//                    //Ajay: 26/12/2018
//                    //uclpfile.cmbAINo.DataSource = Utils.GetAllAIList_LoadProfile(); //Ajay: 24/09/2018 issue by Aditya K mail dtd. 24/09/2018
//                    uclpfile.cmbAINo.DataSource = Utils.GetAllAIList();
//                    //Ajay: 26/12/2018
//                    //uclpfile.cmbENNo.DataSource = Utils.GetAllENList_LoadProfile();
//                    uclpfile.cmbENNo.DataSource = Utils.GetAllENList();

//                    //Ajay: 26/12/2018
//                    List<string> List = new List<string>();
//                    List.AddRange(Utils.getOpenProPlusHandle().getDataTypeValues("LoadProfile_AI_Name"));
//                    List.AddRange(Utils.getOpenProPlusHandle().getDataTypeValues("LoadProfile_EN_Name"));
//                    uclpfile.cmbName.DataSource = List.Distinct().ToList();
//                    if (uclpfile.cmbName.Items.Count > 0) { uclpfile.cmbName.SelectedIndex = 0; }

//                    //Ajay: 10/10/2018 Commented
//                    //uclpfile.cmbParamCode.DataSource = Utils.getOpenProPlusHandle().getDataTypeValues("LoadProfile_LPFile_ParamCode");
//                    if (uclpfile.cmbAINo.Items.Count > 0) { uclpfile.cmbAINo.SelectedIndex = 0; }
//                    if (uclpfile.cmbENNo.Items.Count > 0) { uclpfile.cmbENNo.SelectedIndex = 0; }
//                    //if (uclpfile.cmbParamCode.Items.Count > 0) { uclpfile.cmbParamCode.SelectedIndex = 0; } //Ajay: 10/10/2018 Commented
//                }
//                else { }
//            }

//            catch (Exception Ex)
//            {
//                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//            //uclpfile.CmbDataType.SelectedIndex = 0;
//        }
//        private void addListHeaders()
//        {
//            if (masterType == MasterTypes.LoadProfile)
//            {
//                //uclpfile.lvLPFilelist.Columns.Add("LPFile No.", 70, HorizontalAlignment.Left);
//                uclpfile.lvLPFilelist.Columns.Add("AINo", 80, HorizontalAlignment.Left);
//                uclpfile.lvLPFilelist.Columns.Add("ENNo", 80, HorizontalAlignment.Left);
//                //uclpfile.lvLPFilelist.Columns.Add("PARAMCODE", 150, HorizontalAlignment.Left); //Ajay: 10/10/2018 Commented
//                uclpfile.lvLPFilelist.Columns.Add("Name", -2, HorizontalAlignment.Left); //Ajay: 10/10/2018
//            }
//            else { }
//        }
//        public XmlNode exportXMLnode()
//        {
//            XmlDocument xmlDoc = new XmlDocument();
//            StringWriter stringWriter = new StringWriter();
//            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
//            XmlNode rootNode = null;
//            if (isNodeComment)
//            {
//                rootNode = xmlDoc.CreateComment(comment);
//                xmlDoc.AppendChild(rootNode);
//                return rootNode;
//            }

//            rootNode = xmlDoc.CreateElement("LPFILE");//rootNode = xmlDoc.CreateElement(rnName);
//            xmlDoc.AppendChild(rootNode);

//            foreach (LPFile di in Utils.LPFileList1.Distinct()) //LPFileList)
//            {
//                XmlNode importNode = rootNode.OwnerDocument.ImportNode(di.exportXMLnode(), true);
//                rootNode.AppendChild(importNode);
//            }
//            return rootNode;
//        } 
//        public void parseLPFileCNode(XmlNode lpfilecNode, bool imported)
//        {
//            if (lpfilecNode == null)
//            {
//                rnName = "LPFileConfiguration";
//                return;
//            }
//            //First set root node name...
//            rnName = lpfilecNode.Name;
//            if (lpfilecNode.NodeType == XmlNodeType.Comment)
//            {
//                isNodeComment = true;
//                comment = lpfilecNode.Value;
//            }
//            foreach (XmlNode node in lpfilecNode)
//            {
//                //Console.WriteLine("***** node type: {0}", node.NodeType);
//                if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
//                LPFileList.Add(new LPFile(node, masterType, masterNo, IEDNo, imported));
//            }
//            refreshList();
//        }
//        public void LoadProfile_OnLoad()
//        {
//            string strRoutineName = "LoadProfileOnLoad";
//            try
//            {
//                foreach (Control c in uclpfile.grpLPFile.Controls)
//                {
//                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
//                    {
//                        if (c.Name.Contains("lblHdrText")) { }
//                        else { c.Visible = false; }
//                    }
//                    else { }

//                    //Ajay: 10/10/2018 Commented
//                    //uclpfile.lblLPFileNo.Visible = uclpfile.txtLPFileNo.Visible = true;
//                    //uclpfile.lblLPFileNo.Location = new Point(15, 35);
//                    //uclpfile.txtLPFileNo.Location = new Point(102, 30);

//                    uclpfile.lblAINo.Visible = uclpfile.cmbAINo.Visible = true;
//                    uclpfile.lblAINo.Location = new Point(15, 35);
//                    uclpfile.cmbAINo.Location = new Point(102, 30);

//                    uclpfile.lblENNo.Visible = uclpfile.cmbENNo.Visible = true;
//                    uclpfile.lblENNo.Location = new Point(15, 60);
//                    uclpfile.cmbENNo.Location = new Point(102, 55);

//                    //Ajay: 10/10/2018 Commented
//                    //uclpfile.lblParamCode.Visible = uclpfile.cmbParamCode.Visible = true;
//                    //uclpfile.lblParamCode.Location = new Point(15, 110);
//                    //uclpfile.cmbParamCode.Location = new Point(102, 105); 

//                    //Ajay: 10/10/2018
//                    uclpfile.lblName.Visible = uclpfile.cmbName.Visible = true;
//                    uclpfile.lblName.Location = new Point(15, 85);
//                    uclpfile.cmbName.Location = new Point(102, 80);

//                    uclpfile.btnDone.Visible = uclpfile.btnCancel.Visible = uclpfile.btnVerify.Visible = true;
//                    uclpfile.btnVerify.Location = new Point(15, 110);
//                    uclpfile.btnDone.Location = new Point(130, 110);
//                    uclpfile.btnCancel.Location = new Point(240, 110);

//                    uclpfile.grpLPFile.Height = 150;
//                    uclpfile.grpLPFile.Location = new Point(172, 52);

//                    uclpfile.lvLPFilelistDoubleClick += new System.EventHandler(this.lvLPFilelist_DoubleClick);
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        public void LoadProfile_OnDoubleClick()
//        {
//            string strRoutineName = "LoadProfileOnDoubleClick";
//            try
//            {
//                foreach (Control c in uclpfile.Controls)
//                {
//                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox)
//                    {
//                        if (c.Name.Contains("lblHdrText")) { }
//                        else { c.Visible = false; }
//                    }
//                    else { }
//                }

//                //Ajay: 10/10/2018 Commented
//                //uclpfile.lblLPFileNo.Visible = uclpfile.txtLPFileNo.Visible = true;
//                //uclpfile.lblLPFileNo.Location = new Point(15, 35);
//                //uclpfile.txtLPFileNo.Location = new Point(102, 30);

//                uclpfile.lblAINo.Visible = uclpfile.cmbAINo.Visible = true;
//                uclpfile.lblAINo.Location = new Point(15, 35);
//                uclpfile.cmbAINo.Location = new Point(102, 30);

//                uclpfile.lblENNo.Visible = uclpfile.cmbENNo.Visible = true;
//                uclpfile.lblENNo.Location = new Point(15, 60);
//                uclpfile.cmbENNo.Location = new Point(102, 55);

//                //Ajay: 10/10/2018 Commented
//                //uclpfile.lblParamCode.Visible = uclpfile.cmbParamCode.Visible = true;
//                //uclpfile.lblParamCode.Location = new Point(15, 110);
//                //uclpfile.cmbParamCode.Location = new Point(102, 105);

//                //Ajay: 10/10/2018 Commented
//                uclpfile.lblName.Visible = uclpfile.cmbName.Visible = true;
//                uclpfile.lblName.Location = new Point(15, 85);
//                uclpfile.cmbName.Location = new Point(102, 80);

//                uclpfile.btnDone.Visible = uclpfile.btnCancel.Visible = uclpfile.btnVerify.Visible = true;
//                uclpfile.btnVerify.Location = new Point(15, 110);
//                uclpfile.btnDone.Location = new Point(130, 110);
//                uclpfile.btnCancel.Location = new Point(240, 110);

//                uclpfile.btnFirst.Visible = uclpfile.btnPrev.Visible = uclpfile.btnNext.Visible = uclpfile.btnLast.Visible = true;
//                uclpfile.btnFirst.Location = new Point(13, 145);
//                uclpfile.btnPrev.Location = new Point(88, 145);
//                uclpfile.btnNext.Location = new Point(163, 145);
//                uclpfile.btnLast.Location = new Point(238, 145);

//                uclpfile.grpLPFile.Height = 175;
//                uclpfile.grpLPFile.Location = new Point(172, 52);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        public Control getView(List<string> kpArr)
//        {
//            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("LPFILE_"))
//            {
//                return uclpfile;
//            }
//            return null;
//        }

//        //Ajay: 10/10/2018
//        private void cmbAINo_TextChanged(object sender, EventArgs e)
//        {
//            try
//            {
//                ComboBox cmb = (ComboBox)sender;
//                int i = 0;
//                Int32.TryParse(cmb.Text, out i);
//                if (i > 0)
//                {
//                    uclpfile.cmbENNo.Text = "";
//                    uclpfile.cmbENNo.Enabled = false;
//                    //uclpfile.txtName.Text = Utils.GetNameForLPFILE("AI", i.ToString()); //Ajay: 26/12/2018 Commented
//                }
//                else
//                {
//                    cmb.Text = "";
//                    cmb.Enabled = true;
//                    uclpfile.cmbENNo.Text = "";
//                    uclpfile.cmbENNo.Enabled = true;
//                    //uclpfile.txtName.Text = ""; //Ajay: 26/12/2018 Commented
//                }
//            }
//            catch { }
//        }
//        //Ajay: 10/10/2018
//        private void cmbENNo_TextChanged(object sender, EventArgs e)
//        {
//            try
//            {
//                ComboBox cmb = (ComboBox)sender;
//                int i = 0;
//                Int32.TryParse(cmb.Text, out i);
//                if (i > 0)
//                {
//                    uclpfile.cmbAINo.Text = "";
//                    uclpfile.cmbAINo.Enabled = false;
//                    //uclpfile.txtName.Text = Utils.GetNameForLPFILE("EN", i.ToString()); //Ajay: 26/12/2018 Commented
//                }
//                else
//                {
//                    cmb.Text = "";
//                    cmb.Enabled = true;
//                    uclpfile.cmbAINo.Text = "";
//                    uclpfile.cmbAINo.Enabled = true;
//                    //uclpfile.txtName.Text = ""; //Ajay: 26/12/2018 Commented
//                }
//            }
//            catch { }
//        }
//    }
//}
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
    /**
    * \brief     <b>LPFileConfiguration</b> is a class to store info about all LPFiles and there corresponding mapping infos.
    * \details   This class stores info related to all LPFiles and there corresponding mapping's for various slaves. It allows
    * user to add multiple LPFiles. The user can map this LPFiles to various slaves created in the system, along with the mapping parameters
    * for those slave types. It also exports the XML node related to this object.
    * 
    */
    public class LPFileConfiguration
    {
        #region Declaration
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        private string rnName = "";
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        private bool isNodeComment = false;
        private string comment = "";
        private MasterTypes masterType = MasterTypes.UNKNOWN;
        private int masterNo = -1;
        private int IEDNo = -1;
        private const int COL_CMD_TYPE_WIDTH = 130;
        //Fill RessponseType in All Configuration . 
        public DataGridView dataGridViewDataSet = new DataGridView();
        public DataTable dtdataset = new DataTable();
        List<LPFile> LPFileList = new List<LPFile>();
        ucLPFilelist uclpfile = new ucLPFilelist();
        DataSet DsRCB = new DataSet();
        #endregion Declaration
        public LPFileConfiguration(MasterTypes mType, int mNo, int iNo)
        {
            string strRoutineName = "LPFileConfiguration";
            try
            {
                masterType = mType;
                masterNo = mNo;
                IEDNo = iNo;
                uclpfile.ucLPFilelistLoad += new System.EventHandler(this.ucLPFilelist_Load);
                uclpfile.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                uclpfile.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                uclpfile.btnVerifyClick += new EventHandler(this.btnVerify_Click); //Ajay: 31/07/2018
                uclpfile.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                uclpfile.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                uclpfile.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                uclpfile.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                uclpfile.btnNextClick += new System.EventHandler(this.btnNext_Click);
                uclpfile.btnLastClick += new System.EventHandler(this.btnLast_Click);
                uclpfile.linkLPFileClick += new System.EventHandler(this.linkLPFile_Click);
                uclpfile.lvLPFilelistItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvLPFilelist_ItemSelectionChanged);
                //Ajay: 10/10/2018
                uclpfile.cmbAINoTextChanged += new System.EventHandler(this.cmbAINo_TextChanged);
                uclpfile.cmbENNoTextChanged += new System.EventHandler(this.cmbENNo_TextChanged);

                if (mType == MasterTypes.LoadProfile)
                {
                    LoadProfile_OnLoad();
                }
                addListHeaders();
                fillOptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private int SelectedIndex;
        private void lvLPFilelist_SelectedIndexChanged(object sender, EventArgs e)
        {
            Color GreenColour = Color.FromArgb(82, 208, 23);
            if (uclpfile.lvLPFilelist.SelectedIndices.Count > 0)
            {
                SelectedIndex = Convert.ToInt32(uclpfile.lvLPFilelist.SelectedItems[0].Text);
                uclpfile.lvLPFilelist.SelectedItems.Clear();
                uclpfile.lvLPFilelist.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                uclpfile.lvLPFilelist.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                uclpfile.lvLPFilelist.Items.Cast<ListViewItem>().Where(x => x.Text == SelectedIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);
            }
        }
        private void ucLPFilelist_Load(object sender, EventArgs e)
        {
            string strRoutineName = "uclpfilelist_Load";
            try
            {
                foreach (var L in Utils.VirtualPLU)
                {
                    if (L.Run == "YES")
                    {
                        uclpfile.btnAdd.Enabled = true;
                        uclpfile.btnDelete.Enabled = true;
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
            string strRoutinename = "btnAdd_Click";
            try
            {
                // Ajay: 07/12/2018
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    if (AllowMasterOptionsOnClick(masterType)) { return; }
                    else { }
                }

                if (LPFileList.Count >= getMaxLPFiles())
                {
                    MessageBox.Show("Maximum " + getMaxLPFiles() + " LPFiles are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                mode = Mode.ADD;
                editIndex = -1;
                if (masterType == MasterTypes.LoadProfile)
                {
                    LoadProfile_OnLoad();
                }
                Utils.resetValues(uclpfile.grpLPFile);
                Utils.showNavigation(uclpfile.grpLPFile, false);
                fillOptions(); //Ajay: 24/09/2018
                loadDefaults();
                uclpfile.grpLPFile.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutinename + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void linkLPFile_Click(object sender, EventArgs e)
        {
            string strRoutineName = "linkDO_Click";
            try
            {
                // Ajay: 07/12/2018
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    if (AllowMasterOptionsOnClick(masterType)) { return; }
                    else { }
                }

                foreach (ListViewItem listItem in uclpfile.lvLPFilelist.Items)
                {
                    listItem.Checked = true;
                }
                DialogResult result = MessageBox.Show("Do you want to delete all records ? ", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                {
                    foreach (ListViewItem listItem in uclpfile.lvLPFilelist.Items)
                    {
                        listItem.Checked = false;
                    }
                    return;
                }
                for (int i = uclpfile.lvLPFilelist.Items.Count - 1; i >= 0; i--)
                {
                    Console.WriteLine("*** removing indices: {0}", i);
                    if (Utils.IsExistDIinPLC(LPFileList.ElementAt(i).LPFileNo))
                    {
                        DialogResult ask = MessageBox.Show("DI " + LPFileList.ElementAt(i).LPFileNo + " is referred in ParameterLoadConfiguration and all the references will also be deleted." + Environment.NewLine + "Do you want to continue?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (ask == DialogResult.No)
                        {
                            continue;
                        }
                        Utils.DeleteDIFromPLC(LPFileList.ElementAt(i).LPFileNo);
                    }
                    LPFileList.RemoveAt(i);
                    uclpfile.lvLPFilelist.Items.Clear();
                }
                Console.WriteLine("*** diList count: {0} lv count: {1}", LPFileList.Count, uclpfile.lvLPFilelist.Items.Count);
                refreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnDelete_Click";
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
                for (int i = uclpfile.lvLPFilelist.Items.Count - 1; i >= 0; i--)
                {
                    if (uclpfile.lvLPFilelist.Items[i].Checked)
                    {
                        if (Utils.IsExistDIinPLC(LPFileList.ElementAt(i).LPFileNo))
                        {
                            DialogResult ask = MessageBox.Show("DI " + LPFileList.ElementAt(i).LPFileNo + " is referred in ParameterLoadConfiguration and all the references will also be deleted." + Environment.NewLine + "Do you want to continue?", "Delete DI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (ask == DialogResult.No)
                            {
                                continue;
                            }
                            Utils.DeleteDIFromPLC(LPFileList.ElementAt(i).LPFileNo);
                        }
                        LPFileList.RemoveAt(i);
                        uclpfile.lvLPFilelist.Items[i].Remove();
                    }
                }
                refreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnDone_Click";
            try
            {
                #region LoadProfile
                if (masterType == MasterTypes.LoadProfile)
                {
                    string[] str = new string[2];
                    if (!IsVerified(uclpfile.cmbAINo, "AINo", out str))
                    {
                        string dlgMsg = str[0];
                        if (!string.IsNullOrEmpty(str[1])) { dlgMsg += "=" + str[1]; }
                        dlgMsg += " is not valid." + Environment.NewLine + "Do you want to continue?";
                        DialogResult rslt = MessageBox.Show(dlgMsg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (rslt.ToString().ToLower() == "no") { return; }
                    }
                    if (!IsVerified(uclpfile.cmbENNo, "ENNo", out str))
                    {
                        string dlgMsg = str[0];
                        if (!string.IsNullOrEmpty(str[1])) { dlgMsg += "=" + str[1]; }
                        dlgMsg += " is not valid." + Environment.NewLine + "Do you want to continue?";
                        DialogResult rslt = MessageBox.Show(dlgMsg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (rslt.ToString().ToLower() == "no") { return; }
                    }
                }
                #endregion LoadProfile
                Utils.diList1.Clear();
                List<KeyValuePair<string, string>> LPFileData = Utils.getKeyValueAttributes(uclpfile.grpLPFile);
                if (mode == Mode.ADD)
                {
                    if ((LPFileList.Count + 1) > getMaxLPFiles())
                    {
                        MessageBox.Show("Maximum " + getMaxLPFiles() + " LPFiles are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        LPFile NewLPFile = new LPFile("PARAMETER", LPFileData, null, masterType, masterNo, IEDNo);
                        //Namrata:28/03/2019
                        if (LPFileList.Count > 0)
                        {
                            string AINo = NewLPFile.AINo; string ENNo = NewLPFile.ENNo;
                            bool AIDuplicate = LPFileList.Any(cus => cus.AINo == AINo);
                            bool ENDuplicate = LPFileList.Any(lpfile => lpfile.ENNo == ENNo);
                            if (AIDuplicate)
                            {
                                MessageBox.Show("AINo " + AINo + " " + "already exist.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            //Namrata: 03/06/2019
                            //if (ENDuplicate)
                            //{
                            //    MessageBox.Show("ENNo " + ENNo + " " + "already exist.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //    return;
                            //}
                        }
                        LPFileList.Add(NewLPFile);
                    }
                    //Utils.LPFileList1.AddRange(LPFileList);//Namrata:Commented 28/03/2019
                }
                else if (mode == Mode.EDIT)
                {
                    LPFileList[editIndex].updateAttributes(LPFileData);
                }
                refreshList();
                if (sender != null && e != null)
                {
                    uclpfile.grpLPFile.Visible = false;
                    mode = Mode.NONE;
                    editIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnVerify_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnVerify_Click";
            try
            {
                string[] str = new string[2];
                if (!IsVerified(uclpfile.cmbAINo, "AINo", out str) || !IsVerified(uclpfile.cmbENNo, "ENNo", out str))
                {
                    string dlgMsg = str[0];
                    if (!string.IsNullOrEmpty(str[1])) { dlgMsg += "=" + str[1]; }
                    dlgMsg += " is not valid.";
                    MessageBox.Show(dlgMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("AINo & ENNo are valid.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex) { MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error); }
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
        private void btnCancel_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnCancel_Click";
            try
            {
                Console.WriteLine("*** uclpfile btnCancel_Click clicked in class!!!");
                uclpfile.grpLPFile.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                Utils.resetValues(uclpfile.grpLPFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnFirst_Click";
            try
            {
                Console.WriteLine("*** uclpfile btnFirst_Click clicked in class!!!");
                if (uclpfile.lvLPFilelist.Items.Count <= 0) return;
                if (LPFileList.ElementAt(0).IsNodeComment) return;
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
            string strRoutineName = "btnPrev_Click";
            try
            {
                //Namrata: 27/7/2017
                btnDone_Click(null, null);
                Console.WriteLine("*** uclpfile btnPrev_Click clicked in class!!!");
                if (editIndex - 1 < 0) return;
                if (LPFileList.ElementAt(editIndex - 1).IsNodeComment) return;
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
            string strRoutineName = "btnNext_Click";
            try
            {
                //Namrata: 27/7/2017
                btnDone_Click(null, null);
                Console.WriteLine("*** uclpfile btnNext_Click clicked in class!!!");
                if (editIndex + 1 >= uclpfile.lvLPFilelist.Items.Count) return;
                if (LPFileList.ElementAt(editIndex + 1).IsNodeComment) return;
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
            string strRoutineName = "btnLast_Click";
            try
            {
                Console.WriteLine("*** uclpfile btnLast_Click clicked in class!!!");
                if (uclpfile.lvLPFilelist.Items.Count <= 0) return;
                if (LPFileList.ElementAt(LPFileList.Count - 1).IsNodeComment) return;
                editIndex = LPFileList.Count - 1;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvLPFilelist_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "lvLPFilelist_DoubleClick";
            try
            {
                // Ajay: 07/12/2018
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    if (AllowMasterOptionsOnClick(masterType)) { return; }
                    else { }
                }

                int ListIndex = uclpfile.lvLPFilelist.FocusedItem.Index;
                ListViewItem lvi = uclpfile.lvLPFilelist.Items[ListIndex];//ucai.lvAIlist.SelectedItems[0];
                Utils.UncheckOthers(uclpfile.lvLPFilelist, lvi.Index);
                if (LPFileList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (masterType == MasterTypes.LoadProfile)
                {
                    LoadProfile_OnDoubleClick();
                }
                uclpfile.grpLPFile.Visible = true;
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(uclpfile.grpLPFile, true);
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvLPFilelist_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string strRoutineName = "lvLPFilelist_ItemSelectionChanged";
            try
            {
                if (e.IsSelected)
                {
                    Color GreenColour = Color.FromArgb(34, 217, 0);
                    string diIndex = e.Item.Text;
                    Console.WriteLine("*** selected DI: {0}", diIndex);
                    uclpfile.lvLPFilelist.SelectedItems.Clear();
                    uclpfile.lvLPFilelist.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                    uclpfile.lvLPFilelist.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                    uclpfile.lvLPFilelist.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadDefaults()
        {
            string strRoutineName = "loadDefaults";
            try
            {
                //Ajay: 10/10/2018 Commented
                //uclpfile.txtLPFileNo.Text = (Globals.LPFileNo + 1).ToString();

                if (uclpfile.cmbAINo.Items.Count > 0) { uclpfile.cmbAINo.SelectedIndex = 0; }
                if (uclpfile.cmbENNo.Items.Count > 0) { uclpfile.cmbENNo.SelectedIndex = 0; }
                //Ajay: 10/10/2018 Commented
                //if (uclpfile.cmbParamCode.Items.Count > 0) { uclpfile.cmbParamCode.SelectedIndex = 0; }
                //Ajay: 26/12/2018
                //uclpfile.txtName.Text = ""; //Ajay: 10/10/2018
                if (uclpfile.cmbName.Items.Count > 0) { uclpfile.cmbName.SelectedIndex = 0; } //Ajay: 26/12/2018
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadValues()
        {
            string strRoutineName = "loadValues";
            try
            {
                LPFile lp = LPFileList.ElementAt(editIndex);
                if (lp != null)
                {
                    //uclpfile.txtLPFileNo.Text = lp.LPFileNo; //Ajay: 10/10/2018 Commented
                    uclpfile.cmbAINo.SelectedIndex = uclpfile.cmbAINo.FindStringExact(lp.AINo);
                    uclpfile.cmbENNo.SelectedIndex = uclpfile.cmbENNo.FindStringExact(lp.ENNo);
                    //Ajay: 10/10/2018 Commented
                    //uclpfile.cmbParamCode.SelectedIndex = uclpfile.cmbParamCode.FindStringExact(di.PARAMCODE);
                    //Ajay: 26/12/2018    
                    //uclpfile.txtName.Text = lp.Name; //Ajay: 10/10/2018
                    uclpfile.cmbName.SelectedIndex = uclpfile.cmbName.FindStringExact(lp.Name); //Ajay: 26/12/2018  
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
            if (Utils.IsEmptyFields(uclpfile.grpLPFile))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return status;
        }
        private int getMaxLPFiles()
        {
            if (masterType == MasterTypes.LoadProfile) return Globals.MaxLoadProfileLPFile;
            else return 0;
        }
        public void refreshList()
        {
            string strRoutineName = "LPFileConfiguration: refreshList";
            try
            {
                int cnt = 0;
                uclpfile.lvLPFilelist.Items.Clear();
                Utils.LPFilelistforDescription.Clear();

                if (masterType == MasterTypes.LoadProfile)
                {
                    foreach (LPFile lp in LPFileList)
                    {
                        string[] row = new string[3];
                        if (lp.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            //row[0] = lp.LPFileNo;
                            row[0] = lp.AINo;
                            row[1] = lp.ENNo;
                            //row[2] = lp.PARAMCODE; //Ajay: 10/10/2018 Commented
                            row[2] = lp.Name;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        uclpfile.lvLPFilelist.Items.Add(lvItem);
                    }
                }

                uclpfile.lblDIRecords.Text = LPFileList.Count.ToString();
                Utils.LPFilelistRegenerateIndex.AddRange(LPFileList); // For DI Reindex
                //Utils.DummyDI.AddRange(LPFileList); //For VirtualDI AutomaticEnteries
                //Utils.EnableDI.AddRange(LPFileList); // Validate DI exist in DOConfiguration
                Utils.LPFilelistforDescription.AddRange(LPFileList); //For Description not present in XML 
                Utils.LPFileList1.AddRange(LPFileList);
                //Utils.DiListForVirualDIImport.AddRange(LPFileList); 

            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void fillOptions()
        {
            string strRoutineName = "LPFile : fillOptions";
            try
            {
                if (masterType == MasterTypes.LoadProfile)
                {
                    uclpfile.cmbAINo.DataSource = Utils.GetAllAIList();
                    uclpfile.cmbENNo.DataSource = Utils.GetAllENList();
                    //Ajay: 26/12/2018
                    List<string> List = new List<string>();
                    List.AddRange(Utils.getOpenProPlusHandle().getDataTypeValues("LoadProfile_AI_Name"));
                    List.AddRange(Utils.getOpenProPlusHandle().getDataTypeValues("LoadProfile_EN_Name"));
                    uclpfile.cmbName.DataSource = List.Distinct().ToList();
                    if (uclpfile.cmbName.Items.Count > 0) { uclpfile.cmbName.SelectedIndex = 0; }
                    if (uclpfile.cmbAINo.Items.Count > 0) { uclpfile.cmbAINo.SelectedIndex = 0; }
                    if (uclpfile.cmbENNo.Items.Count > 0) { uclpfile.cmbENNo.SelectedIndex = 0; }
                }
                else { }
            }

            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //uclpfile.CmbDataType.SelectedIndex = 0;
        }
        private void addListHeaders()
        {
            if (masterType == MasterTypes.LoadProfile)
            {
                uclpfile.lvLPFilelist.Columns.Add("AINo", 80, HorizontalAlignment.Left);
                uclpfile.lvLPFilelist.Columns.Add("ENNo", 80, HorizontalAlignment.Left);
                uclpfile.lvLPFilelist.Columns.Add("Name", -2, HorizontalAlignment.Left); //Ajay: 10/10/2018
            }
            else { }
        }
        public XmlNode exportXMLnode()
        {
            refreshList();//Namrata:28/03/2019
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
            rootNode = xmlDoc.CreateElement("LPFILE");
            xmlDoc.AppendChild(rootNode);
            List<long> longs = new List<long> { 1, 2, 3, 4, 3, 2, 5 };
            var distinctList = longs.Distinct().ToList();

            //Namrata: 11/07/2019
            //Avoid duplicae entries during save XML.
            List<LPFile> LPList = Utils.LPFileList1
                                         .GroupBy(p => new { p.AINo, p.ENNo })
                                         .Select(g => g.First())
                                         .ToList();
            //var LPList = Utils.LPFileList1.Distinct().ToList(); //GroupBy(i => i.AINo).Select(g => g.First()).ToList();//Namrata:28/03/2019
            foreach (LPFile di in LPList) //LPFileList)//foreach (LPFile di in Utils.LPFileList1.Distinct()) //LPFileList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(di.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }
            return rootNode;
        }
        public void parseLPFileCNode(XmlNode lpfilecNode, bool imported)
        {
            if (lpfilecNode == null)
            {
                rnName = "LPFileConfiguration";
                return;
            }
            //First set root node name...
            rnName = lpfilecNode.Name;
            if (lpfilecNode.NodeType == XmlNodeType.Comment)
            {
                isNodeComment = true;
                comment = lpfilecNode.Value;
            }
            foreach (XmlNode node in lpfilecNode)
            {
                //Console.WriteLine("***** node type: {0}", node.NodeType);
                if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                LPFileList.Add(new LPFile(node, masterType, masterNo, IEDNo, imported));
            }
            refreshList();
        }
        public void LoadProfile_OnLoad()
        {
            string strRoutineName = "LoadProfileOnLoad";
            try
            {
                foreach (Control c in uclpfile.grpLPFile.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox || c is Button)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }

                    //Ajay: 10/10/2018 Commented
                    //uclpfile.lblLPFileNo.Visible = uclpfile.txtLPFileNo.Visible = true;
                    //uclpfile.lblLPFileNo.Location = new Point(15, 35);
                    //uclpfile.txtLPFileNo.Location = new Point(102, 30);

                    uclpfile.lblAINo.Visible = uclpfile.cmbAINo.Visible = true;
                    uclpfile.lblAINo.Location = new Point(15, 35);
                    uclpfile.cmbAINo.Location = new Point(102, 30);

                    uclpfile.lblENNo.Visible = uclpfile.cmbENNo.Visible = true;
                    uclpfile.lblENNo.Location = new Point(15, 60);
                    uclpfile.cmbENNo.Location = new Point(102, 55);

                    //Ajay: 10/10/2018 Commented
                    //uclpfile.lblParamCode.Visible = uclpfile.cmbParamCode.Visible = true;
                    //uclpfile.lblParamCode.Location = new Point(15, 110);
                    //uclpfile.cmbParamCode.Location = new Point(102, 105); 

                    //Ajay: 10/10/2018
                    uclpfile.lblName.Visible = uclpfile.cmbName.Visible = true;
                    uclpfile.lblName.Location = new Point(15, 85);
                    uclpfile.cmbName.Location = new Point(102, 80);

                    uclpfile.btnDone.Visible = uclpfile.btnCancel.Visible = uclpfile.btnVerify.Visible = true;
                    uclpfile.btnVerify.Location = new Point(15, 110);
                    uclpfile.btnDone.Location = new Point(130, 110);
                    uclpfile.btnCancel.Location = new Point(240, 110);

                    uclpfile.grpLPFile.Height = 150;
                    uclpfile.grpLPFile.Location = new Point(172, 52);

                    uclpfile.lvLPFilelistDoubleClick += new System.EventHandler(this.lvLPFilelist_DoubleClick);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void LoadProfile_OnDoubleClick()
        {
            string strRoutineName = "LoadProfileOnDoubleClick";
            try
            {
                foreach (Control c in uclpfile.Controls)
                {
                    if (c is Label || c is TextBox || c is ComboBox || c is CheckBox)
                    {
                        if (c.Name.Contains("lblHdrText")) { }
                        else { c.Visible = false; }
                    }
                    else { }
                }

                //Ajay: 10/10/2018 Commented
                //uclpfile.lblLPFileNo.Visible = uclpfile.txtLPFileNo.Visible = true;
                //uclpfile.lblLPFileNo.Location = new Point(15, 35);
                //uclpfile.txtLPFileNo.Location = new Point(102, 30);

                uclpfile.lblAINo.Visible = uclpfile.cmbAINo.Visible = true;
                uclpfile.lblAINo.Location = new Point(15, 35);
                uclpfile.cmbAINo.Location = new Point(102, 30);

                uclpfile.lblENNo.Visible = uclpfile.cmbENNo.Visible = true;
                uclpfile.lblENNo.Location = new Point(15, 60);
                uclpfile.cmbENNo.Location = new Point(102, 55);

                //Ajay: 10/10/2018 Commented
                //uclpfile.lblParamCode.Visible = uclpfile.cmbParamCode.Visible = true;
                //uclpfile.lblParamCode.Location = new Point(15, 110);
                //uclpfile.cmbParamCode.Location = new Point(102, 105);

                //Ajay: 10/10/2018 Commented
                uclpfile.lblName.Visible = uclpfile.cmbName.Visible = true;
                uclpfile.lblName.Location = new Point(15, 85);
                uclpfile.cmbName.Location = new Point(102, 80);

                uclpfile.btnDone.Visible = uclpfile.btnCancel.Visible = uclpfile.btnVerify.Visible = true;
                uclpfile.btnVerify.Location = new Point(15, 110);
                uclpfile.btnDone.Location = new Point(130, 110);
                uclpfile.btnCancel.Location = new Point(240, 110);

                uclpfile.btnFirst.Visible = uclpfile.btnPrev.Visible = uclpfile.btnNext.Visible = uclpfile.btnLast.Visible = true;
                uclpfile.btnFirst.Location = new Point(13, 145);
                uclpfile.btnPrev.Location = new Point(88, 145);
                uclpfile.btnNext.Location = new Point(163, 145);
                uclpfile.btnLast.Location = new Point(238, 145);

                uclpfile.grpLPFile.Height = 175;
                uclpfile.grpLPFile.Location = new Point(172, 52);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("LPFILE_"))
            {
                return uclpfile;
            }
            return null;
        }

        //Ajay: 10/10/2018
        private void cmbAINo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cmb = (ComboBox)sender;
                int i = 0;
                Int32.TryParse(cmb.Text, out i);
                if (i > 0)
                {
                    uclpfile.cmbENNo.Text = "";
                    uclpfile.cmbENNo.Enabled = false;
                    //uclpfile.txtName.Text = Utils.GetNameForLPFILE("AI", i.ToString()); //Ajay: 26/12/2018 Commented
                }
                else
                {
                    cmb.Text = "";
                    cmb.Enabled = true;
                    uclpfile.cmbENNo.Text = "";
                    uclpfile.cmbENNo.Enabled = true;
                    //uclpfile.txtName.Text = ""; //Ajay: 26/12/2018 Commented
                }
            }
            catch { }
        }
        //Ajay: 10/10/2018
        private void cmbENNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cmb = (ComboBox)sender;
                int i = 0;
                Int32.TryParse(cmb.Text, out i);
                if (i > 0)
                {
                    uclpfile.cmbAINo.Text = "";
                    uclpfile.cmbAINo.Enabled = false;
                    //uclpfile.txtName.Text = Utils.GetNameForLPFILE("EN", i.ToString()); //Ajay: 26/12/2018 Commented
                }
                else
                {
                    cmb.Text = "";
                    cmb.Enabled = true;
                    uclpfile.cmbAINo.Text = "";
                    uclpfile.cmbAINo.Enabled = true;
                    //uclpfile.txtName.Text = ""; //Ajay: 26/12/2018 Commented
                }
            }
            catch { }
        }
    }
}
