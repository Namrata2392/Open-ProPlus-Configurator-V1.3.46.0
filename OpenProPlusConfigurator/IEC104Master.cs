using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace OpenProPlusConfigurator
{
   public class IEC104Master
    {
        #region Declarations
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        enum masterType
        {
            IEC104
        };

        ucAIlist ucai = new ucAIlist();
        private bool isNodeComment = false;
        private string comment = "";
        private masterType mType = masterType.IEC104;
        private int masterNum = -1;
        private int portNum = -1;
        private bool run = true;
        private int debug = -1;
        private int giTime = 300;
        private int clockSyncInterval = -1;
        private int refreshInterval = 120;
        private string appFirmwareVersion;
        private string desc = "";
        private int SelectedIndex;
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        public static int UnitID;
        private string asSize = "";
        private string ioaSz = "";
        private string cSize = "";
        private string linkAddressSize = "";
        private int tcpPort; //Ajay: 19/09/2018
        List<IED> iedList = new List<IED>();
        ucMasterIEC104 uciec104 = new ucMasterIEC104();
        private TreeNode IEC104TreeNode;
        private OpenFileDialog ofdXMLFile = new OpenFileDialog();
        private string[] arrAttributes = { "MasterNum", "PortNum", "Run", "DEBUG", "GiTime", "ClockSyncInterval", "RefreshInterval", "AppFirmwareVersion", "Description" };
        #endregion Declarations

        public IEC104Master(string m101Name, List<KeyValuePair<string, string>> m101Data, TreeNode tn)
        {
            string strRoutineName = "IEC104Master";
            try
            {
                uciec104.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                //uciec104.lvIEDListItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvIEDList_ItemCheck);
                //uciec104.BtnDeleteAllClick += new System.EventHandler(this.BtnDeleteAll_Click);
                uciec104.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                uciec104.btnExportIEDClick += new System.EventHandler(this.btnExportIED_Click);
                uciec104.btnImportIEDClick += new System.EventHandler(this.btnImportIED_Click);
                uciec104.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                uciec104.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                uciec104.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                uciec104.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                uciec104.btnNextClick += new System.EventHandler(this.btnNext_Click);
                uciec104.btnLastClick += new System.EventHandler(this.btnLast_Click);
                uciec104.lvIEDListDoubleClick += new System.EventHandler(this.lvIEDList_DoubleClick);
                addListHeaders();
                fillOptions();
                try
                {
                    mType = (masterType)Enum.Parse(typeof(masterType), m101Name);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", m101Name);
                }
                if (m101Data != null && m101Data.Count > 0)
                {
                    foreach (KeyValuePair<string, string> m101kp in m101Data)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", m101kp.Key, m101kp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(m101kp.Key) != null) //Ajay: 03/07/2018
                            {
                                this.GetType().GetProperty(m101kp.Key).SetValue(this, m101kp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", m101kp.Key, m101kp.Value);
                        }
                    }
                    Utils.Write(VerboseLevel.DEBUG, "\n");
                }
                if (tn != null) tn.Nodes.Clear();
                IEC104TreeNode = tn;
                if (tn != null) tn.Text = "IEC104 " + this.Description;
                refreshList();
                uciec104.lblIED.Text = "IED List (Master No: " + this.MasterNum + ")";
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public IEC104Master(XmlNode mNode, TreeNode tn)
        {
            string strRoutineName = "IEC104Master";
            try
            {
                uciec104.btnAddClick += new System.EventHandler(this.btnAdd_Click);
               // uciec104.lvIEDListItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvIEDList_ItemCheck);
                uciec104.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                uciec104.btnExportIEDClick += new System.EventHandler(this.btnExportIED_Click);
                uciec104.btnImportIEDClick += new System.EventHandler(this.btnImportIED_Click);
                uciec104.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                uciec104.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                uciec104.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                uciec104.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                uciec104.btnNextClick += new System.EventHandler(this.btnNext_Click);
                uciec104.btnLastClick += new System.EventHandler(this.btnLast_Click);
                uciec104.lvIEDListDoubleClick += new System.EventHandler(this.lvIEDList_DoubleClick);
                addListHeaders();
                fillOptions();
                Utils.WriteLine(VerboseLevel.DEBUG, "mNode name: '{0}'", mNode.Name);
                if (mNode.Attributes != null)
                {
                    try
                    {
                        mType = (masterType)Enum.Parse(typeof(masterType), mNode.Name);
                    }
                    catch (System.ArgumentException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", mNode.Name);
                    }
                    foreach (XmlAttribute item in mNode.Attributes)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", item.Name, item.Value);
                        try
                        {
                            if (this.GetType().GetProperty(item.Name) != null) //Ajay: 03/07/2018
                            {
                                this.GetType().GetProperty(item.Name).SetValue(this, item.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", item.Name, item.Value);
                        }
                    }
                    Utils.Write(VerboseLevel.DEBUG, "\n");
                }
                else if (mNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = mNode.Value;
                }
                if (tn != null) tn.Nodes.Clear();
                IEC104TreeNode = tn;
                tn.Text = "IEC104 " + this.Description;
                foreach (XmlNode node in mNode)
                {
                    if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                    TreeNode tmp = tn.Nodes.Add("IED_" + Utils.GenerateShortUniqueKey(), "IED", "IED", "IED");
                    iedList.Add(new IED(node, tmp, MasterTypes.IEC104, Int32.Parse(MasterNum), false));
                }
                refreshList();
                uciec104.lblIED.Text = "IED List (Master No: " + this.MasterNum + ")";
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvIEDList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string strRoutineName = "lvIEDList_ItemCheck";
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    uciec104.lvIEDList.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
                    {
                        item.Checked = false;
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void updateAttributes(List<KeyValuePair<string, string>> m104Data)
        {
            string strRoutineName = "updateAttributes";
            try
            {
                if (m104Data != null && m104Data.Count > 0)
                {
                    foreach (KeyValuePair<string, string> m104kp in m104Data)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", m104kp.Key, m104kp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(m104kp.Key) != null) //Ajay: 03/07/2018
                            {
                                this.GetType().GetProperty(m104kp.Key).SetValue(this, m104kp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", m104kp.Key, m104kp.Value);
                        }
                    }
                    Utils.Write(VerboseLevel.DEBUG, "\n");
                }
                uciec104.lblIED.Text = "IED List (Master No: " + this.MasterNum + ")";
                if (IEC104TreeNode != null) IEC104TreeNode.Text = "IEC104 " + this.Description;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnAdd_Click";
            try
            {
                if (iedList.Count >= Globals.MaxIEC104IED)
                {
                    MessageBox.Show("Maximum " + Globals.MaxIEC104IED + " IED's in IEC104 Masters are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                mode = Mode.ADD;
                editIndex = -1;
                Utils.resetValues(uciec104.grpIED);
                Utils.showNavigation(uciec104.grpIED, false);
                loadDefaults();
                uciec104.txtUnitID.Text = (Globals.getIEC104IEDNo(Int32.Parse(MasterNum)) + 1).ToString();
                uciec104.txtASDUaddress.Text = (Globals.getIEC104ASDUAddress(Int32.Parse(MasterNum)) + 1).ToString();
                uciec104.grpIED.Visible = true;
                uciec104.txtUnitID.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnDelete_Click";
            try
            {
                if (uciec104.lvIEDList.Items.Count == 0)
                {
                    MessageBox.Show("Please add atleast one IED ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (uciec104.lvIEDList.CheckedItems.Count == 1)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete IED " + uciec104.lvIEDList.CheckedItems[0].Text + " ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        int iIndex = uciec104.lvIEDList.CheckedItems[0].Index;
                        if (result == DialogResult.Yes)
                        {
                            IEC104TreeNode.Nodes.Remove(iedList.ElementAt(iIndex).getTreeNode());
                            Utils.RemoveDI4IED(MasterTypes.IEC104, Int32.Parse(MasterNum), Int32.Parse(iedList[iIndex].UnitID));
                            iedList.RemoveAt(iIndex);
                            uciec104.lvIEDList.Items[iIndex].Remove();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select atleast one IED ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    refreshList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnExportIED_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnExportIED_Click";
            try
            {
                if (uciec104.lvIEDList.CheckedItems.Count != 1)
                {
                    MessageBox.Show("Select single IED for export!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ListViewItem lvi = uciec104.lvIEDList.CheckedItems[0];
                IED expObj = null;
                //Now get the IED object...
                foreach (IED iedObj in iedList)
                {
                    if (iedObj.UnitID == lvi.Text)
                    {
                        expObj = iedObj;
                        break;
                    }
                }
                Utils.SaveIEDFile(expObj.exportIED());
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnFirst_Click";
            try
            {
                Console.WriteLine("*** uciec btnFirst_Click clicked in class!!!");
                if (uciec104.lvIEDList.Items.Count <= 0) return;
                if (iedList.ElementAt(0).IsNodeComment) return;
                editIndex = 0;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnPrev_Click";
            try
            {
                //Namrata:27/7/2017
                btnDone_Click(null, null);
                Console.WriteLine("*** uciec btnPrev_Click clicked in class!!!");
                if (editIndex - 1 < 0) return;
                if (iedList.ElementAt(editIndex - 1).IsNodeComment) return;
                editIndex--;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnNext_Click";
            try
            {
                //Namrata:27/7/2017
                btnDone_Click(null, null);
                if (editIndex + 1 >= uciec104.lvIEDList.Items.Count) return;
                if (iedList.ElementAt(editIndex + 1).IsNodeComment) return;
                editIndex++;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnLast_Click";
            try
            {
                Console.WriteLine("*** uciec btnLast_Click clicked in class!!!");
                if (uciec104.lvIEDList.Items.Count <= 0) return;
                if (iedList.ElementAt(iedList.Count - 1).IsNodeComment) return;
                editIndex = iedList.Count - 1;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnImportIED_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnImportIED_Click";
            try
            {
                if (ofdXMLFile.ShowDialog() == DialogResult.OK)
                {
                    if (!Utils.IsXMLWellFormed(ofdXMLFile.FileName))
                    {
                        MessageBox.Show("Selected file is not a valid XML!!!.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ofdXMLFile.FileName);
                    XmlNodeList nodeList = xmlDoc.SelectNodes("IEDexport");
                    if (nodeList.Count <= 0)
                    {
                        MessageBox.Show("Selected file is not an IED exported node.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    XmlNode rootNode = nodeList.Item(0);
                    Utils.WriteLine(VerboseLevel.DEBUG, "*** Exported IED Node name: {0}", rootNode.Name);
                    if (rootNode.Attributes != null)
                    {
                        foreach (XmlAttribute item in rootNode.Attributes)
                        {
                            Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", item.Name, item.Value);
                            if (item.Name == "MasterType")
                            {
                                if (item.Value != mType.ToString())
                                {
                                    MessageBox.Show("Invalid Master Type (" + item.Value + ") to import!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                        }
                    }
                    foreach (XmlNode node in rootNode)
                    {
                        Utils.WriteLine(VerboseLevel.BOMBARD, "node value: '{0}' child count {1}", node.Name, node.ChildNodes.Count);
                        if (node.Name == "IED")
                        {
                            if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                            TreeNode tmp = IEC104TreeNode.Nodes.Add("IED_" + Utils.GenerateShortUniqueKey(), "IED", "IED", "IED");
                            iedList.Add(new IED(node, tmp, MasterTypes.IEC104, Int32.Parse(MasterNum), true));
                            Utils.CreateDI4IED(MasterTypes.IEC104, Int32.Parse(MasterNum), Int32.Parse(iedList[iedList.Count - 1].UnitID), iedList[iedList.Count - 1].Device);
                            tmp.Expand();
                        }
                    }
                    refreshList();
                    MessageBox.Show("IED imported successfully!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnDone_Click";
            try
            {
                if (!Validate()) return;
                List<KeyValuePair<string, string>> iedData = Utils.getKeyValueAttributes(uciec104.grpIED);
                if (mode == Mode.ADD)
                {
                    TreeNode tmp = IEC104TreeNode.Nodes.Add("IED_" + Utils.GenerateShortUniqueKey(), "IED", "IED", "IED");
                    iedList.Add(new IED("IED", iedData, tmp, MasterTypes.IEC104, Int32.Parse(MasterNum)));
                    Utils.CreateDI4IED(MasterTypes.IEC104, Int32.Parse(MasterNum), Int32.Parse(iedList[iedList.Count - 1].UnitID), iedList[iedList.Count - 1].Device);
                }
                else if (mode == Mode.EDIT)
                {
                    iedList[editIndex].updateAttributes(iedData);
                    if (uciec104.lvIEDList.SelectedIndices.Count > 0)
                    {
                        SelectedIndex = Convert.ToInt16(uciec104.lvIEDList.FocusedItem.Index);
                        Utils.IEDUnitID = Convert.ToInt16(uciec104.lvIEDList.Items[SelectedIndex].Text);
                    }
                    Utils.UpdateDI4IED(MasterTypes.IEC104, Int32.Parse(MasterNum), Utils.IEDUnitID, Int32.Parse(iedList[SelectedIndex].UnitID), iedList[iedList.Count - 1].Device);
                }
                refreshList();
                //Namrata: 09/08/2017
                if (sender != null && e != null)
                {
                    uciec104.grpIED.Visible = false;
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
            string strRoutineName = "btnCancel_Click";
            try
            {
                uciec104.grpIED.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                Utils.resetValues(uciec104.grpIED);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvIEDList_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "lvIEDList_DoubleClick";
            try
            {
                // Ajay: 23/11/2018
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    if (ProtocolGateway.OppIEC104Group_ReadOnly) { return; }
                    else { }
                }

                if (uciec104.lvIEDList.SelectedItems.Count <= 0) return;
                ListViewItem lvi = uciec104.lvIEDList.SelectedItems[0];
                Utils.UncheckOthers(uciec104.lvIEDList, lvi.Index);
                if (iedList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                uciec104.grpIED.Visible = true;
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(uciec104.grpIED, true);
                loadValues();
                uciec104.txtUnitID.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadDefaults()
        {
            string strRoutineName = "loadDefaults";
            try
            {
                uciec104.txtDevice.Text = "IEC104_" + (Globals.getIEC104IEDNo(Int32.Parse(MasterNum)) + 1).ToString();
                uciec104.txtRetries.Text = "3";
                uciec104.txtTimeOut.Text = "100";
                uciec104.txtDescription.Text = "IEC104_IED_" + (Globals.getIEC104IEDNo(Int32.Parse(MasterNum)) + 1).ToString();
                uciec104.cmbASDU.SelectedIndex = uciec104.cmbASDU.FindStringExact("2");
                //Ajay: 06/07/2018 ---Default value for IOA=3 and COT=2 by Aditya K dtd 06/07/2018---
                //uciec104.CmbIOA.SelectedIndex = uciec104.CmbIOA.FindStringExact("2");
                //uciec104.CmbCOT.SelectedIndex = uciec104.CmbCOT.FindStringExact("1");
                uciec104.CmbIOA.SelectedIndex = uciec104.CmbIOA.FindStringExact("3");
                uciec104.CmbCOT.SelectedIndex = uciec104.CmbCOT.FindStringExact("2"); //Ajay: 06/07/2018
                //Namrata:22/05/2018
                //uciec104.txtT0.Text = "30"; //Ajay: 12/11/2018 
                uciec104.txtT0.Text = "5"; //Ajay: 12/11/2018 Changed by Shilpa T dtd 12/11/2018 by Phone.
                uciec104.txtT1.Text = "15";
                uciec104.txtT2.Text = "10";
                uciec104.txtT3.Text = "20";
                uciec104.txtW.Text =  "8";
                uciec104.txtK.Text =  "12";
                //Ajay:06/07/2018
                uciec104.txtRemoteIP.Text = "0.0.0.0";
                uciec104.txtRdeudantIP.Text = "0.0.0.0";
                //Ajay: 19/09/2018
                uciec104.txtTCPPort.Text = "2404";
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadValues()
        {
            string strRoutineName = "loadValues";
            try
            {
                IED eied = iedList.ElementAt(editIndex);
                if (eied != null)
                {
                    uciec104.txtUnitID.Text = eied.UnitID;
                    uciec104.txtRemoteIP.Text = eied.RemoteIP;
                    uciec104.txtASDUaddress.Text = eied.ASDUAddr;
                    uciec104.cmbASDU.SelectedIndex = uciec104.cmbASDU.FindStringExact(eied.ASDUSize);
                    uciec104.CmbIOA.SelectedIndex = uciec104.CmbIOA.FindStringExact(eied.IOASize);
                    uciec104.CmbCOT.SelectedIndex = uciec104.CmbCOT.FindStringExact(eied.COTSize);
                    uciec104.txtRetries.Text = eied.Retries;
                    uciec104.txtTimeOut.Text = eied.TimeOutMS;
                    uciec104.txtT0.Text = eied.T0;
                    uciec104.txtT1.Text = eied.T1;
                    uciec104.txtT2.Text = eied.T2;
                    uciec104.txtT3.Text = eied.T3;
                    uciec104.txtW.Text = eied.W;
                    uciec104.txtK.Text = eied.K;
                    uciec104.txtDevice.Text = eied.Device;
                    uciec104.txtDescription.Text = eied.Description;
                    //Ajay:06/07/2018
                    uciec104.txtRdeudantIP.Text = eied.RedundantIP;
                    uciec104.txtTCPPort.Text = eied.TCPPort; //Ajay: 19/09/2018
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool IsUnitIDUnique(string unitID)
        {
            for (int i = 0; i < iedList.Count; i++)
            {
                IED ied = iedList.ElementAt(i);
                if (ied.UnitID == unitID && (mode == Mode.ADD || editIndex != i)) return false;
            }
            return true;
        }
        private bool Validate()
        {
            bool status = true;
            //Check empty field's
            if (Utils.IsEmptyFields(uciec104.grpIED))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //IED-UnitID should be unique...
            if (!IsUnitIDUnique(uciec104.txtUnitID.Text))
            {
                MessageBox.Show("IED Unit ID must be unique!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return status;
        }
        private void fillOptions()
        {
            string strRoutineName = "fillOptions";
            try
            {
                //Fill ASDU size...
                uciec104.cmbASDU.Items.Clear();
                foreach (String br in IED.getASDUsizes())
                {
                    uciec104.cmbASDU.Items.Add(br.ToString());
                }
                uciec104.cmbASDU.SelectedIndex = 0;

                //Fill IOA size...
                uciec104.CmbIOA.Items.Clear();
                foreach (String br in IED.getIOAsizes())
                {
                    uciec104.CmbIOA.Items.Add(br.ToString());
                }
                uciec104.CmbIOA.SelectedIndex = 0;

                //Fill COT size...
                uciec104.CmbCOT.Items.Clear();
                foreach (String br in IED.getCOTsizes())
                {
                    uciec104.CmbCOT.Items.Add(br.ToString());
                }
                uciec104.CmbCOT.SelectedIndex = 0;

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void addListHeaders()
        {
            string strRoutineName = "addListHeaders";
            try
            {
                uciec104.lvIEDList.Columns.Add("Unit ID", 80, HorizontalAlignment.Left);
                uciec104.lvIEDList.Columns.Add("Remote IP Address", 130, HorizontalAlignment.Left);
                //Ajay:06/07/2018
                uciec104.lvIEDList.Columns.Add("Redundant IP Address", 130, HorizontalAlignment.Left);
                uciec104.lvIEDList.Columns.Add("TCP Port", 100, HorizontalAlignment.Left); //Ajay: 19/09/2018
                uciec104.lvIEDList.Columns.Add("ASDU Address", 130, HorizontalAlignment.Left);
                uciec104.lvIEDList.Columns.Add("ASDU Size", 100, HorizontalAlignment.Left);
                uciec104.lvIEDList.Columns.Add("IOA Size", 100, HorizontalAlignment.Left);
                uciec104.lvIEDList.Columns.Add("COT Size", 100, HorizontalAlignment.Left);
                uciec104.lvIEDList.Columns.Add("Retires", 60, HorizontalAlignment.Left);
                uciec104.lvIEDList.Columns.Add("Timeout (msec)", 85, HorizontalAlignment.Left);
                uciec104.lvIEDList.Columns.Add("T0(Sec)", 60, HorizontalAlignment.Left);
                uciec104.lvIEDList.Columns.Add("T1(Sec)", 85, HorizontalAlignment.Left);
                uciec104.lvIEDList.Columns.Add("T2(Sec)", 60, HorizontalAlignment.Left);
                uciec104.lvIEDList.Columns.Add("T3(Sec)", 85, HorizontalAlignment.Left);
                uciec104.lvIEDList.Columns.Add("w", 60, HorizontalAlignment.Left);
                uciec104.lvIEDList.Columns.Add("K", 85, HorizontalAlignment.Left);
                uciec104.lvIEDList.Columns.Add("Device", 100, HorizontalAlignment.Left);
                uciec104.lvIEDList.Columns.Add("Description", 150, HorizontalAlignment.Left);
                uciec104.lvIEDList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void refreshList()
        {
            string strRoutineName = "refreshList";
            try
            {
                int cnt = 0;
                uciec104.lvIEDList.Items.Clear();
                //Namrata: 25/11/2017
                Utils.IEC101MasteriedList.Clear();
                foreach (IED ied in iedList)
                {
                    string[] row = new string[18];
                    if (ied.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = ied.UnitID;
                        row[1] = ied.RemoteIP;
                        row[2] = ied.RedundantIP;
                        row[3] = ied.TCPPort;
                        row[4] = ied.ASDUAddr;
                        row[5] = ied.ASDUSize;
                        row[6] = ied.IOASize;
                        row[7] = ied.COTSize;
                        row[8] = ied.Retries;
                        row[9] = ied.TimeOutMS;
                        row[10] = ied.T0;
                        row[11] = ied.T1;
                        row[12] = ied.T2;
                        row[13] = ied.T3;
                        row[14] = ied.W;
                        row[15] = ied.K;
                        row[16] = ied.Device;
                        row[17] = ied.Description;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    uciec104.lvIEDList.Items.Add(lvItem);
                }
                //Namrata: 20/11/2017
                Utils.IEC101MasteriedList.AddRange(iedList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("IEC104_")) return uciec104;
            kpArr.RemoveAt(0);
            if (kpArr.ElementAt(0).Contains("IED_"))
            {
                int idx = -1;
                string[] elems = kpArr.ElementAt(0).Split('_');
                idx = Int32.Parse(elems[elems.Length - 1]);
                if (iedList.Count <= 0) return null;
                return iedList[idx].getView(kpArr);
            }
            else
            {
            }
            return null;
        }
        public TreeNode getTreeNode()
        {
            return IEC104TreeNode;
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
            rootNode = xmlDoc.CreateElement(mType.ToString());
            xmlDoc.AppendChild(rootNode);

            foreach (string attr in arrAttributes)
            {
                XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                rootNode.Attributes.Append(attrName);
            }

            foreach (IED iedn in iedList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(iedn.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }
            return rootNode;
        }
        public string exportINI(string slaveNum, string slaveID, string element, ref int ctr)
        {
            string iniData = "";

            if (element == "IED")
            {
                foreach (IED ied in iedList)
                {
                    iniData += "IED_" + ctr++ + "=" + ied.Description + "," + ied.Device + "," + (ied.DR.ToLower() == "enable" ? "YES" : "NO") + Environment.NewLine;
                }
            }
            else
            {
                foreach (IED ied in iedList)
                {
                    iniData += ied.exportINI(slaveNum, slaveID, element, ref ctr);
                }
            }

            return iniData;
        }
        public List<IED> getIEDs()
        {
            return iedList;
        }
        public List<IED> getIEDsByFilter(string iedID)
        {
            List<IED> iList = new List<IED>();
            if (iedID.ToLower() == "all") return iedList;
            else
                foreach (IED i in iedList)
                {
                    if (i.getIEDID == iedID)
                    {
                        iList.Add(i);
                        break;
                    }
                }
            return iList;
        }
        public string getMasterID
        {
            get { return "IEC104_" + MasterNum; }
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public string MasterNum
        {
            get { masterNum = Int32.Parse(uciec104.txtMasterNo.Text); return masterNum.ToString(); }
            set { masterNum = Int32.Parse(value); uciec104.txtMasterNo.Text = value; Globals.MasterNo = Int32.Parse(value); }
        }
        public string PortNum
        {
            get { portNum = Int32.Parse(uciec104.txtPortNo.Text); return portNum.ToString(); }
            set { portNum = Int32.Parse(value); uciec104.txtPortNo.Text = value; }
        }
        public string Run
        {
            get { run = uciec104.chkRun.Checked; return (run == true ? "YES" : "NO"); }
            set
            {
                run = (value.ToLower() == "yes") ? true : false;
                if (run == true) uciec104.chkRun.Checked = true;
                else uciec104.chkRun.Checked = false;
            }
        }
        public string DEBUG
        {
            get { debug = Int32.Parse(uciec104.txtDebug.Text); return debug.ToString(); }
            set { debug = Int32.Parse(value); uciec104.txtDebug.Text = value; }
        }
        public string GiTime
        {
            get { giTime = Int32.Parse(uciec104.txtGiTime.Text); return giTime.ToString(); }
            set { giTime = Int32.Parse(value); uciec104.txtGiTime.Text = value; }
        }
        public string ClockSyncInterval
        {
            get { clockSyncInterval = Int32.Parse(uciec104.txtClockSyncInterval.Text); return clockSyncInterval.ToString(); }
            set { clockSyncInterval = Int32.Parse(value); uciec104.txtClockSyncInterval.Text = value; }
        }
        public string RefreshInterval
        {
            get
            {
                try
                {
                    refreshInterval = Int32.Parse(uciec104.txtRefreshInterval.Text);
                }
                catch (System.FormatException)
                {
                    refreshInterval = 120;
                    uciec104.txtRefreshInterval.Text = refreshInterval.ToString();
                }
                return refreshInterval.ToString();
            }
            set { refreshInterval = Int32.Parse(value); uciec104.txtRefreshInterval.Text = value; }
        }
        public string AppFirmwareVersion
        {
            get { return appFirmwareVersion = uciec104.txtFirmwareVer.Text; }
            set { appFirmwareVersion = uciec104.txtFirmwareVer.Text = value; }
        }
        public string Description
        {
            get { return desc = uciec104.txtIECDesc.Text; }
            set { desc = uciec104.txtIECDesc.Text = value; }
        }
        public string ASDUSize
        {
            get { return asSize; }
            set { asSize = value; }
        }
        public string IOASize
        {
            get
            {
                return ioaSz;
            }
            set
            {
                ioaSz = value;
            }
        }
        public string COTSize
        {
            get { return cSize; }
            set { cSize = value; }
        }
        //Ajay: 19/09/2018
        public string TcpPort
        {
            get { return tcpPort.ToString(); }
            set
            {
                try
                {
                    tcpPort = Int32.Parse(value);
                }
                catch (System.FormatException)
                {
                    tcpPort = -1;
                }
            }
        }
    }
}
