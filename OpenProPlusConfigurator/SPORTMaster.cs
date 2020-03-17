using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;
namespace OpenProPlusConfigurator
{
    public class SPORTMaster
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
            SPORT
        };
        private bool isNodeComment = false;
        private string comment = "";
        private masterType mType = masterType.SPORT;
        private int masterNum = -1;
        private int portNum = -1;
        private bool run = true;
        private int debug = -1;
        private int giTime = 300;
        private int clockSyncInterval = -1;
        private int refreshInterval = 120;
        private string appFirmwareVersion;
        private string desc = "";
        //Namrata:15/6/2017
        private int SelectedIndex;
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        public static int UnitID;
        //Namarta:7/7/2017
        private string asSize = "";
        private string ioaSz = "";
        private string cSize = "";
        private string linkAddressSize = "";
        List<IED> iedList = new List<IED>();
        ucMasterSPORT ucSPORT = new ucMasterSPORT();
        private TreeNode SPORTMasterTreeNode;
        private OpenFileDialog ofdXMLFile = new OpenFileDialog();
        private string[] arrAttributes = { "MasterNum", "PortNum", "Run", "DEBUG", "GiTime", "ClockSyncInterval", "RefreshInterval", "AppFirmwareVersion", "Description" };
        #endregion Declarations

        public SPORTMaster(string m101Name, List<KeyValuePair<string, string>> m101Data, TreeNode tn)
        {
            string strRoutineName = "SPORTMaster";
            try
            {
                ucSPORT.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                ucSPORT.lvIEDListItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvIEDList_ItemCheck);
                ucSPORT.BtnDeleteAllClick += new System.EventHandler(this.BtnDeleteAll_Click);
                ucSPORT.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                ucSPORT.btnExportIEDClick += new System.EventHandler(this.btnExportIED_Click);
                ucSPORT.btnImportIEDClick += new System.EventHandler(this.btnImportIED_Click);
                ucSPORT.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucSPORT.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucSPORT.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                ucSPORT.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                ucSPORT.btnNextClick += new System.EventHandler(this.btnNext_Click);
                ucSPORT.btnLastClick += new System.EventHandler(this.btnLast_Click);
                //ucSPORT.txtLastAITextChanged += new System.EventHandler(this.txtLastAI_TextChanged);
                //ucSPORT.txtLastDITextChanged += new System.EventHandler(this.txtLastDI_TextChanged);
                //ucSPORT.txtLastDOTextChanged += new System.EventHandler(this.txtLastDO_TextChanged);
                ucSPORT.lvIEDListDoubleClick += new System.EventHandler(this.lvIEDList_DoubleClick);
                addListHeaders();
                fillOptions();
                try
                {
                    mType = (masterType)Enum.Parse(typeof(masterType), m101Name);
                }
                catch (System.ArgumentException)
                {
                }
                if (m101Data != null && m101Data.Count > 0)
                {
                    foreach (KeyValuePair<string, string> m101kp in m101Data)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", m101kp.Key, m101kp.Value);
                        try
                        {
                            this.GetType().GetProperty(m101kp.Key).SetValue(this, m101kp.Value);
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", m101kp.Key, m101kp.Value);
                        }
                    }
                    Utils.Write(VerboseLevel.DEBUG, "\n");
                }
                if (tn != null) tn.Nodes.Clear();
                SPORTMasterTreeNode = tn;
                if (tn != null) tn.Text = "SPORT " + this.Description;
                refreshList();
                ucSPORT.lblIED.Text = "IED List (Master No: " + this.MasterNum + ")";
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimitLastAIValidataion()
        {
            string strRoutineName = "SPORTMaster";
            try
            {
                if (ucSPORT.CmbIOA.SelectedItem.ToString() == "1")
                {
                    Regex regex = new Regex(@"^(\b(1?[0-9]{1,2}|2[0-4][0-9]|25[0-5])\b)$");
                    if (regex.IsMatch(ucSPORT.txtLastAI.Text))
                    {
                    }
                    else
                    {
                        MessageBox.Show("Valid ranges are between 0-255.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (ucSPORT.CmbIOA.SelectedItem.ToString() == "2")
                {
                    Regex regex = new Regex(@"^(\b(1?[0-9]|[1-8][0-9]|9[0-9]|[1-8][0-9]{2}|9[0-8][0-9]|99[0-9]|[1-8][0-9]{3}|9[0-8][0-9]{2}|99[0-8][0-9]|999[0-9]|[12][0-9]{4}|3[0-4][0-9]{3}|35[0-4][0-9]{2}|355[0-5][0-9]|3556[0-5])\b)$");
                    if (regex.IsMatch(ucSPORT.txtLastAI.Text))
                    {
                    }
                    else
                    {
                        MessageBox.Show("Valid ranges are between 0-65535.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Namrata:31/01/2019
        private void txtLastDI_TextChanged(object sender, EventArgs e)
        {
            if (ucSPORT.CmbIOA.SelectedItem.ToString() == "1")
            {
                Regex regex = new Regex(@"^(\b(1?[0-9]{1,2}|2[0-4][0-9]|25[0-5])\b)$");
                if (regex.IsMatch(ucSPORT.txtLastDI.Text))
                {
                }
                else
                {
                    MessageBox.Show("Valid ranges are between 0-65535.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (ucSPORT.CmbIOA.SelectedItem.ToString() == "2")
            {
                Regex regex = new Regex(@"^(\b(1?[0-9]|[1-8][0-9]|9[0-9]|[1-8][0-9]{2}|9[0-8][0-9]|99[0-9]|[1-8][0-9]{3}|9[0-8][0-9]{2}|99[0-8][0-9]|999[0-9]|[12][0-9]{4}|3[0-4][0-9]{3}|35[0-4][0-9]{2}|355[0-5][0-9]|3556[0-5])\b)$");
                if (regex.IsMatch(ucSPORT.txtLastDI.Text))
                {
                }
                else
                {
                    MessageBox.Show("Valid ranges are between 0-255.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }
        //Namrata:31/01/2019
        private void txtLastDO_TextChanged(object sender, EventArgs e)
        {
            if (ucSPORT.CmbIOA.SelectedItem.ToString() == "1")
            {
                Regex regex = new Regex(@"^(\b(1?[0-9]{1,2}|2[0-4][0-9]|25[0-5])\b)$");
                if (regex.IsMatch(ucSPORT.txtLastDO.Text))
                {
                }
                else
                {
                    MessageBox.Show("Valid ranges are between 0-255.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (ucSPORT.CmbIOA.SelectedItem.ToString() == "2")
            {
                Regex regex = new Regex(@"^(\b(1?[0-9]|[1-8][0-9]|9[0-9]|[1-8][0-9]{2}|9[0-8][0-9]|99[0-9]|[1-8][0-9]{3}|9[0-8][0-9]{2}|99[0-8][0-9]|999[0-9]|[12][0-9]{4}|3[0-4][0-9]{3}|35[0-4][0-9]{2}|355[0-5][0-9]|3556[0-5])\b)$");
                if (regex.IsMatch(ucSPORT.txtLastDO.Text))
                {
                }
                else
                {
                    MessageBox.Show("Valid ranges are between 0-65535.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }
        //Namrata:31/01/2019
        private void txtLastAI_TextChanged(object sender, EventArgs e)
        {
            if(ucSPORT.CmbIOA.SelectedItem.ToString() == "1")
            {
                Regex regex = new Regex(@"^(\b(1?[0-9]{1,2}|2[0-4][0-9]|25[0-5])\b)$");
                if (regex.IsMatch(ucSPORT.txtLastAI.Text))
                { 
                }
                else
                {
                    MessageBox.Show("Valid ranges are between 0-255.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (ucSPORT.CmbIOA.SelectedItem.ToString() == "2")
            {
                Regex regex = new Regex(@"^(\b(1?[0-9]|[1-8][0-9]|9[0-9]|[1-8][0-9]{2}|9[0-8][0-9]|99[0-9]|[1-8][0-9]{3}|9[0-8][0-9]{2}|99[0-8][0-9]|999[0-9]|[12][0-9]{4}|3[0-4][0-9]{3}|35[0-4][0-9]{2}|355[0-5][0-9]|3556[0-5])\b)$");
                if (regex.IsMatch(ucSPORT.txtLastAI.Text))
                {
                }
                else
                {
                    MessageBox.Show("Valid ranges are between 0-65535.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }
        private void BtnDeleteAll_Click(object sender, EventArgs e)
        {
            string strRoutineName = "BtnDeleteAll_Click";
            try
            {
                ucSPORT.lvIEDListItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.lvIEDList_ItemCheck);
                foreach (ListViewItem listItem in ucSPORT.lvIEDList.Items)
                {
                    listItem.Checked = true;
                }
                DialogResult result = MessageBox.Show("Do You Want To Delete All Records ? ", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                {
                    foreach (ListViewItem listItem in ucSPORT.lvIEDList.Items)
                    {
                        listItem.Checked = false;
                    }
                    return;
                }
                for (int i = ucSPORT.lvIEDList.Items.Count - 1; i >= 0; i--)
                {
                    SPORTMasterTreeNode.Nodes.Remove(iedList.ElementAt(i).getTreeNode());
                    Utils.RemoveDI4IED(MasterTypes.SPORT, Int32.Parse(MasterNum), Int32.Parse(iedList[i].UnitID));
                    iedList.RemoveAt(i);
                    ucSPORT.lvIEDList.Items[i].Remove();
                }
                refreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public SPORTMaster(XmlNode mNode, TreeNode tn)
        {
            string strRoutineName = "SPORTMaster";
            try
            {
                ucSPORT.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                ucSPORT.lvIEDListItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvIEDList_ItemCheck);
                ucSPORT.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                ucSPORT.btnExportIEDClick += new System.EventHandler(this.btnExportIED_Click);
                ucSPORT.btnImportIEDClick += new System.EventHandler(this.btnImportIED_Click);
                ucSPORT.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucSPORT.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucSPORT.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                ucSPORT.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                ucSPORT.btnNextClick += new System.EventHandler(this.btnNext_Click);
                ucSPORT.btnLastClick += new System.EventHandler(this.btnLast_Click);
                ucSPORT.lvIEDListDoubleClick += new System.EventHandler(this.lvIEDList_DoubleClick);
                //Namrata:01/02/2019
                //ucSPORT.txtLastAITextChanged += new System.EventHandler(this.txtLastAI_TextChanged);
                //ucSPORT.txtLastDITextChanged += new System.EventHandler(this.txtLastDI_TextChanged);
                //ucSPORT.txtLastDOTextChanged += new System.EventHandler(this.txtLastDO_TextChanged);
                addListHeaders();
                fillOptions();
                if (mNode.Attributes != null)
                {
                    try
                    {
                        mType = (masterType)Enum.Parse(typeof(masterType), mNode.Name);
                    }
                    catch (System.ArgumentException)
                    {
                    }
                    foreach (XmlAttribute item in mNode.Attributes)
                    {
                        try
                        {
                            if (this.GetType().GetProperty(item.Name) != null) //Ajay: 03/07/2018
                            {
                                this.GetType().GetProperty(item.Name).SetValue(this, item.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        { 
                        }
                    }
                }
                else if (mNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = mNode.Value;
                }
                if (tn != null) tn.Nodes.Clear();
                SPORTMasterTreeNode = tn;
                tn.Text = "SPORT " + this.Description;
                foreach (XmlNode node in mNode)
                {
                    if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                    TreeNode tmp = tn.Nodes.Add("IED_" + Utils.GenerateShortUniqueKey(), "IED", "IED", "IED");
                    iedList.Add(new IED(node, tmp, MasterTypes.SPORT, Int32.Parse(MasterNum), false));
                }
                refreshList();
                ucSPORT.lblIED.Text = "IED List (Master No: " + this.MasterNum + ")";
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
                    ucSPORT.lvIEDList.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
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
        public void updateAttributes(List<KeyValuePair<string, string>> m101Data)
        {
            string strRoutineName = "updateAttributes";
            try
            {
                if (m101Data != null && m101Data.Count > 0)
                {
                    foreach (KeyValuePair<string, string> m101kp in m101Data)
                    {
                        try
                        {
                            this.GetType().GetProperty(m101kp.Key).SetValue(this, m101kp.Value);
                        }
                        catch (System.NullReferenceException)
                        {
                        }
                    }
                }
                ucSPORT.lblIED.Text = "IED List (Master No: " + this.MasterNum + ")";
                if (SPORTMasterTreeNode != null) SPORTMasterTreeNode.Text = "SPORT " + this.Description;
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
                //Namrata:31/01/2019
                //ucSPORT.txtLastAITextChanged -= new System.EventHandler(this.txtLastAI_TextChanged);
                //ucSPORT.txtLastDITextChanged -= new System.EventHandler(this.txtLastDI_TextChanged);
                //ucSPORT.txtLastDOTextChanged -= new System.EventHandler(this.txtLastDO_TextChanged);

                if (iedList.Count >= Globals.MaxSPORTIED)
                {
                    MessageBox.Show("Maximum " + Globals.MaxSPORTIED + " IED's in SPORT Masters are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                mode = Mode.ADD;
                editIndex = -1;
                Utils.resetValues(ucSPORT.grpIED);
                Utils.showNavigation(ucSPORT.grpIED, false);
                loadDefaults();
                ucSPORT.txtUnitID.Text = (Globals.getSPORTIEDNo(Int32.Parse(MasterNum)) + 1).ToString();
                ucSPORT.grpIED.Visible = true;
                ucSPORT.txtUnitID.Focus();
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
                if (ucSPORT.lvIEDList.Items.Count == 0)
                {
                    MessageBox.Show("Please add atleast one IED ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (ucSPORT.lvIEDList.CheckedItems.Count == 1)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete IED " + ucSPORT.lvIEDList.CheckedItems[0].Text + " ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        int iIndex = ucSPORT.lvIEDList.CheckedItems[0].Index;
                        if (result == DialogResult.Yes)
                        {
                            SPORTMasterTreeNode.Nodes.Remove(iedList.ElementAt(iIndex).getTreeNode());
                            Utils.RemoveDI4IED(MasterTypes.SPORT, Int32.Parse(MasterNum), Int32.Parse(iedList[iIndex].UnitID));
                            iedList.RemoveAt(iIndex);
                            ucSPORT.lvIEDList.Items[iIndex].Remove();
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
                if (ucSPORT.lvIEDList.CheckedItems.Count != 1)
                {
                    MessageBox.Show("Select single IED for export!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ListViewItem lvi = ucSPORT.lvIEDList.CheckedItems[0];
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
                if (ucSPORT.lvIEDList.Items.Count <= 0) return;
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
                btnDone_Click(null, null);
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
                btnDone_Click(null, null);
                if (editIndex + 1 >= ucSPORT.lvIEDList.Items.Count) return;
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
                if (ucSPORT.lvIEDList.Items.Count <= 0) return;
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
                    if (rootNode.Attributes != null)
                    {
                        foreach (XmlAttribute item in rootNode.Attributes)
                        {
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
                            TreeNode tmp = SPORTMasterTreeNode.Nodes.Add("IED_" + Utils.GenerateShortUniqueKey(), "IED", "IED", "IED");
                            iedList.Add(new IED(node, tmp, MasterTypes.SPORT, Int32.Parse(MasterNum), true));
                            Utils.CreateDI4IED(MasterTypes.SPORT, Int32.Parse(MasterNum), Int32.Parse(iedList[iedList.Count - 1].UnitID), iedList[iedList.Count - 1].Device);
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
                List<KeyValuePair<string, string>> iedData = Utils.getKeyValueAttributes(ucSPORT.grpIED);
                
                #region Validation for LastAI,LastDI,LastDO
                //Namrata:01/02/2019
                int txtLastAI = Int32.Parse(ucSPORT.txtLastAI.Text);
                int txtLastDI = Int32.Parse(ucSPORT.txtLastDI.Text);
                int txtLastDO = Int32.Parse(ucSPORT.txtLastDO.Text);
                if (ucSPORT.CmbIOA.SelectedItem.ToString() == "1")
                {
                    Regex RegexForIOA1 = new Regex(@"^(\b(1?[0-9]{1,2}|2[0-4][0-9]|25[0-5])\b)$");
                   
                    if (RegexForIOA1.IsMatch(txtLastAI.ToString())){ }
                    else
                    {
                        MessageBox.Show("Valid ranges for LastAI are between 0-255.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (RegexForIOA1.IsMatch(txtLastDI.ToString())) { }
                    else
                    {
                        MessageBox.Show("Valid ranges for LastDI are between 0-255.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (RegexForIOA1.IsMatch(txtLastDO.ToString())) { }
                    else
                    {
                        MessageBox.Show("Valid ranges for LastDO are between 0-255.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
               else if (ucSPORT.CmbIOA.SelectedItem.ToString() == "2")
                {
                    Regex RegexForIOA2 = new Regex(@"^(\b(1?[0-9]|[1-8][0-9]|9[0-9]|[1-8][0-9]{2}|9[0-8][0-9]|99[0-9]|[1-8][0-9]{3}|9[0-8][0-9]{2}|99[0-8][0-9]|999[0-9]|[12][0-9]{4}|3[0-4][0-9]{3}|35[0-4][0-9]{2}|355[0-5][0-9]|3556[0-5])\b)$");
                    if (RegexForIOA2.IsMatch(txtLastAI.ToString()))
                    {
                    }
                    else
                    {
                        MessageBox.Show("Valid ranges for LastAI are between 0-65535.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (RegexForIOA2.IsMatch(txtLastDI.ToString())) { }
                    else
                    {
                        MessageBox.Show("Valid ranges for LastDI are between 0-65535.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (RegexForIOA2.IsMatch(txtLastDO.ToString())) { }
                    else
                    {
                        MessageBox.Show("Valid ranges for LastDO are between 0-65535.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                #endregion Validation for LastAI,LastDI,LastDO

                #region Validation for TxtTimeStampAccuracy
                //Namrata:04/02/2019
                if (Convert.ToInt32(ucSPORT.txtTimestampAccuracy.Text) >= 0) 
                {
                    if(Convert.ToInt32(ucSPORT.txtTimestampAccuracy.Text) <= 65535)
                    {

                    }
                    else
                    {
                        MessageBox.Show("Valid ranges for TimeStampAccuracy are between 0-65535(mSec). ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                }
                else
                {
                    MessageBox.Show("Valid ranges for TimeStampAccuracy are between 0-65535(mSec). ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion Validation for TxtTimeStampAccuracy

                #region Validation for txtWindowTime
                if (Convert.ToInt32(ucSPORT.txtWindowTime.Text) >= 10) 
                {
                    if(Convert.ToInt32(ucSPORT.txtWindowTime.Text) <= 2500)
                    {

                    }
                    else
                    {
                        MessageBox.Show("Valid ranges for WindowTime are between 10-2500(mSec). ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Valid ranges for WindowTime are between 10-2500(mSec). ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion Validation for TxtTimeStampAccuracy

                #region Validation for txtDebounceTime
                if (Convert.ToInt32(ucSPORT.txtDebounceTime.Text) >= 1) 
                {
                    if(Convert.ToInt32(ucSPORT.txtDebounceTime.Text) <= 255)
                    {

                    }
                    else
                    {
                        MessageBox.Show("Valid ranges for DebounceTime are between 1-255(Sec). ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Valid ranges for DebounceTime are between 1-255(Sec). ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion Validation for txtDebounceTime

                #region Validation for txtPulseWidthTimeout
                string text = ucSPORT.txtPulsewidthTimeout.Text.ToString();
                int MintxtPulsewidthTimeout = 10, MaxtxtPulsewidthTimeout = 2550;
                int TxtPulsewidthTimeoutInt = int.Parse(text);

                if (TxtPulsewidthTimeoutInt > MaxtxtPulsewidthTimeout || TxtPulsewidthTimeoutInt < MintxtPulsewidthTimeout)
                {
                    MessageBox.Show("Number can be only in range " + MintxtPulsewidthTimeout.ToString() + " and " + MaxtxtPulsewidthTimeout.ToString(),Application.ProductName,MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    
                }
                else
                {

                }
            







                //if (num >= 10)// (Convert.ToInt32(ucSPORT.txtPulsewidthTimeout.Text) >= 10)
                //{
                //    if (Convert.ToInt32(ucSPORT.txtPulsewidthTimeout.Text) <= 2550)
                //    {
                //        MessageBox.Show("Valid ranges for PulseWidthTimeout are between 10-2550(mSec). ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        return;
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("Valid ranges for PulseWidthTimeout are between 10-2550(mSec). ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
                #endregion Validation for txtPulseWidthTimeout

                if (mode == Mode.ADD)
                {
                    TreeNode tmp = SPORTMasterTreeNode.Nodes.Add("IED_" + Utils.GenerateShortUniqueKey(), "IED", "IED", "IED");
                    iedList.Add(new IED("IED", iedData, tmp, MasterTypes.SPORT, Int32.Parse(MasterNum)));
                    Utils.CreateDI4IED(MasterTypes.SPORT, Int32.Parse(MasterNum), Int32.Parse(iedList[iedList.Count - 1].UnitID), iedList[iedList.Count - 1].Device);
                }
                else if (mode == Mode.EDIT)
                {
                    iedList[editIndex].updateAttributes(iedData);
                    if (ucSPORT.lvIEDList.SelectedIndices.Count > 0)
                    {
                        SelectedIndex = Convert.ToInt16(ucSPORT.lvIEDList.FocusedItem.Index);
                        Utils.IEDUnitID = Convert.ToInt16(ucSPORT.lvIEDList.Items[SelectedIndex].Text);
                    }
                    Utils.UpdateDI4IED(MasterTypes.IEC101, Int32.Parse(MasterNum), Utils.IEDUnitID, Int32.Parse(iedList[SelectedIndex].UnitID), iedList[iedList.Count - 1].Device);
                }
                refreshList();
                //Namrata: 09/08/2017
                if (sender != null && e != null)
                {
                    ucSPORT.grpIED.Visible = false;
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
                ucSPORT.grpIED.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                Utils.resetValues(ucSPORT.grpIED);
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
                    if (ProtocolGateway.OppSPORTGroup_ReadOnly) { return; }
                    else { }
                }

                if (ucSPORT.lvIEDList.SelectedItems.Count <= 0) return;
                ListViewItem lvi = ucSPORT.lvIEDList.SelectedItems[0];
                Utils.UncheckOthers(ucSPORT.lvIEDList, lvi.Index);
                if (iedList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ucSPORT.grpIED.Visible = true;
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(ucSPORT.grpIED, true);
                loadValues();
                ucSPORT.txtUnitID.Focus();
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
                //Namrata:01/02/2019
                //if(mode==Mode.ADD)
                //{
                //    ucSPORT.txtLastAITextChanged -= new System.EventHandler(this.txtLastAI_TextChanged);
                //    ucSPORT.txtLastDITextChanged -= new System.EventHandler(this.txtLastDI_TextChanged);
                //    ucSPORT.txtLastDOTextChanged -= new System.EventHandler(this.txtLastDO_TextChanged);

                //}
               

                ucSPORT.txtDevice.Text = "SPORT_" + (Globals.getSPORTIEDNo(Int32.Parse(MasterNum)) + 1).ToString();
                ucSPORT.txtRetries.Text = "3";
                ucSPORT.txtTimeOut.Text = "100";
                ucSPORT.txtDescription.Text = "SPORT_IED_" + (Globals.getSPORTIEDNo(Int32.Parse(MasterNum)) + 1).ToString();
                ucSPORT.CmbIOA.SelectedIndex = ucSPORT.CmbIOA.FindStringExact("1");
                ucSPORT.CmbSportType.SelectedIndex = ucSPORT.CmbSportType.FindStringExact("1080");
                //Namrata:30/01/2019
                ucSPORT.txtLastAI.Text = "0";
                ucSPORT.txtLastDI.Text = "0";
                ucSPORT.txtLastDO.Text = "0";
                ucSPORT.txtTimestampType.Text = "0";
                ucSPORT.txtTimestampAccuracy.Text = "0";
                ucSPORT.txtWindowTime.Text = "10";
                ucSPORT.txtDebounceTime.Text = "1";
                ucSPORT.txtPulsewidthTimeout.Text = "10";


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
                    ucSPORT.txtUnitID.Text = eied.UnitID;
                    //ucSPORT.txtASDUaddress.Text = eied.ASDUAddr;
                    ucSPORT.txtDevice.Text = eied.Device;
                    ucSPORT.txtRetries.Text = eied.Retries;
                    ucSPORT.txtTimeOut.Text = eied.TimeOutMS;
                    ucSPORT.txtDescription.Text = eied.Description;
                    //Namrata:31/01/2019
                    ucSPORT.CmbSportType.SelectedIndex = ucSPORT.CmbSportType.FindStringExact(eied.SportType);
                    ucSPORT.CmbIOA.SelectedIndex = ucSPORT.CmbIOA.FindStringExact(eied.IOASize);
                    ucSPORT.txtLastAI.Text = eied.LastAI;
                    ucSPORT.txtLastDI.Text = eied.LastDI;
                    ucSPORT.txtLastDO.Text = eied.LastDO;
                    ucSPORT.txtTimestampType.Text = eied.TimestampType;
                    ucSPORT.txtTimestampAccuracy.Text = eied.TimestampAccuracy;
                    ucSPORT.txtWindowTime.Text = eied.WindowTime;
                    ucSPORT.txtDebounceTime.Text = eied.DebounceTime;
                    ucSPORT.txtPulsewidthTimeout.Text = eied.PulseWidthTimeout;
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
            if (Utils.IsEmptyFields(ucSPORT.grpIED))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //IED-UnitID should be unique...
            if (!IsUnitIDUnique(ucSPORT.txtUnitID.Text))
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
                //Fill IOA size...
                ucSPORT.CmbIOA.Items.Clear();
                foreach (String br in IED.getIOAsizes())
                {
                    ucSPORT.CmbIOA.Items.Add(br.ToString());
                }
                if (ucSPORT.CmbIOA.Items.Count > 0) { ucSPORT.CmbIOA.SelectedIndex = 0; }

                //Fill LinkAddress size...
                ucSPORT.CmbSportType.Items.Clear();
                foreach (String br in IED.getSPORTTypes())
                {
                    //ucSPORT.CmbSportType.DataSource = Utils.getOpenProPlusHandle().getDataTypeValues("OperateOn");
                    //Utils.getOpenProPlusHandle().getDataTypeValues("OperateOn");
                    ucSPORT.CmbSportType.Items.Add(br.ToString());
                }
                if (ucSPORT.CmbSportType.Items.Count > 0) { ucSPORT.CmbSportType.SelectedIndex = 0; }
                //Namrata:30/01/2019
                //ucSPORT.CmbLastAI.DataSource = Utils.GetAINoForSPORTIED(MasterTypes.SPORT, MasterNum.ToString());
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
                ucSPORT.lvIEDList.Columns.Add("Unit ID", 80, HorizontalAlignment.Left);
                ucSPORT.lvIEDList.Columns.Add("Device", 100, HorizontalAlignment.Left);
                ucSPORT.lvIEDList.Columns.Add("Retires", 60, HorizontalAlignment.Left);
                ucSPORT.lvIEDList.Columns.Add("Timeout (msec)", 85, HorizontalAlignment.Left);
                ucSPORT.lvIEDList.Columns.Add("SportType", 100, HorizontalAlignment.Left);
                ucSPORT.lvIEDList.Columns.Add("IOA Size", 100, HorizontalAlignment.Left);
                ucSPORT.lvIEDList.Columns.Add("LastAI", 100, HorizontalAlignment.Left);
                ucSPORT.lvIEDList.Columns.Add("LastDI", 100, HorizontalAlignment.Left);
                ucSPORT.lvIEDList.Columns.Add("LastDO", 100, HorizontalAlignment.Left);
                ucSPORT.lvIEDList.Columns.Add("TimestampType", 100, HorizontalAlignment.Left);
                ucSPORT.lvIEDList.Columns.Add("TimestampAccuracy", 100, HorizontalAlignment.Left);
                ucSPORT.lvIEDList.Columns.Add("WindowTime", 100, HorizontalAlignment.Left);
                ucSPORT.lvIEDList.Columns.Add("DebounceTime", 100, HorizontalAlignment.Left);
                ucSPORT.lvIEDList.Columns.Add("PulsewidthTimeout", 100, HorizontalAlignment.Left);
                ucSPORT.lvIEDList.Columns.Add("Description", 100, HorizontalAlignment.Left);
                ucSPORT.lvIEDList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
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
                ucSPORT.lvIEDList.Items.Clear();
                //Namrata: 25/11/2017
                Utils.IEC101MasteriedList.Clear();
                foreach (IED ied in iedList)
                {
                    string[] row = new string[15];
                    if (ied.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = ied.UnitID;
                        row[1] = ied.Device;
                        row[2] = ied.Retries;
                        row[3] = ied.TimeOutMS;
                        row[4] = ied.SportType;
                        row[5] = ied.IOASize;
                        row[6] = ied.LastAI;
                        row[7] = ied.LastDI;
                        row[8] = ied.LastDO;
                        row[9] = ied.TimestampType;
                        row[10] = ied.TimestampAccuracy;
                        row[11] = ied.WindowTime;
                        row[12] = ied.DebounceTime;
                        row[13] = ied.PulseWidthTimeout;
                        row[14] = ied.Description;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucSPORT.lvIEDList.Items.Add(lvItem);
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
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("SPORT_")) return ucSPORT;
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
            return SPORTMasterTreeNode;
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
            get { return "IEC101_" + MasterNum; }
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public string MasterNum
        {
            get { masterNum = Int32.Parse(ucSPORT.txtMasterNo.Text); return masterNum.ToString(); }
            set { masterNum = Int32.Parse(value); ucSPORT.txtMasterNo.Text = value; Globals.MasterNo = Int32.Parse(value); }
        }
        public string PortNum
        {
            get { portNum = Int32.Parse(ucSPORT.txtPortNo.Text); return portNum.ToString(); }
            set { portNum = Int32.Parse(value); ucSPORT.txtPortNo.Text = value; }
        }
        public string Run
        {
            get { run = ucSPORT.chkRun.Checked; return (run == true ? "YES" : "NO"); }
            set
            {
                run = (value.ToLower() == "yes") ? true : false;
                if (run == true) ucSPORT.chkRun.Checked = true;
                else ucSPORT.chkRun.Checked = false;
            }
        }
        public string DEBUG
        {
            get { debug = Int32.Parse(ucSPORT.txtDebug.Text); return debug.ToString(); }
            set { debug = Int32.Parse(value); ucSPORT.txtDebug.Text = value; }
        }
        public string GiTime
        {
            get { giTime = Int32.Parse(ucSPORT.txtGiTime.Text); return giTime.ToString(); }
            set { giTime = Int32.Parse(value); ucSPORT.txtGiTime.Text = value; }
        }
        public string ClockSyncInterval
        {
            get { clockSyncInterval = Int32.Parse(ucSPORT.txtClockSyncInterval.Text); return clockSyncInterval.ToString(); }
            set { clockSyncInterval = Int32.Parse(value); ucSPORT.txtClockSyncInterval.Text = value; }
        }
        public string RefreshInterval
        {
            get
            {
                try
                {
                    refreshInterval = Int32.Parse(ucSPORT.txtRefreshInterval.Text);
                }
                catch (System.FormatException)
                {
                    refreshInterval = 120;
                    ucSPORT.txtRefreshInterval.Text = refreshInterval.ToString();
                }
                return refreshInterval.ToString();
            }
            set { refreshInterval = Int32.Parse(value); ucSPORT.txtRefreshInterval.Text = value; }
        }
        public string AppFirmwareVersion
        {
            get { return appFirmwareVersion = ucSPORT.txtFirmwareVer.Text; }
            set { appFirmwareVersion = ucSPORT.txtFirmwareVer.Text = value; }
        }
        public string Description
        {
            get { return desc = ucSPORT.txtIECDesc.Text; }
            set { desc = ucSPORT.txtIECDesc.Text = value; }
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
    }
}
