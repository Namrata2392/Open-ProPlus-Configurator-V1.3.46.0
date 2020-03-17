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
    public class IEC101Group
    {
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        private string rnName = "IEC101Group";
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        public List<IEC101Master> m101List = new List<IEC101Master>();
        ucIEC101Group ucm101 = new ucIEC101Group();
        ucAIlist ucai = new ucAIlist();
        private TreeNode IEC101GroupTreeNode;

        public IEC101Group(TreeNode tn)
        {
            string strRoutineName = "IEC101Group";
            try
            {
                tn.Nodes.Clear();
                IEC101GroupTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...
                ucm101.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                ucm101.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                ucm101.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucm101.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucm101.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                ucm101.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                ucm101.btnNextClick += new System.EventHandler(this.btnNext_Click);
                ucm101.btnLastClick += new System.EventHandler(this.btnLast_Click);

                ucm101.lvIEC101MasterDoubleClick += new System.EventHandler(this.lvIEC101Master_DoubleClick);
                ucm101.lvIEC101MasterItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvIEC101Master_ItemCheck);
                addListHeaders();
                fillOptions();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvIEC101Master_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string strRoutineName = "IEC101Group:lvIEC101Master_ItemCheck";
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    ucm101.lvIEC101Master.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
                    {
                        item.Checked = false;
                    });
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string strRoutineName = "IEC101Group:btnAdd_Click";
            try
            {
                if (m101List.Count >= Globals.MaxIEC101Master)
                {
                    MessageBox.Show("Maximum " + Globals.MaxIEC101Master + " IEC101 Masters are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                mode = Mode.ADD;
                editIndex = -1;
                Utils.resetValues(ucm101.grpIEC101);
                Utils.showNavigation(ucm101.grpIEC101, false);
                //Namrata: 03/07/2017
                loadDefaults();
                ucm101.txtMasterNo.Text = (Globals.MasterNo + 1).ToString();
                ucm101.grpIEC101.Visible = true;
                ucm101.cmbPortNo.Focus();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strRoutineName = "IEC101Group:btnDelete_Click";
            try
            {
                if (ucm101.lvIEC101Master.Items.Count == 0)
                {
                    MessageBox.Show("Please add atleast one master ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (ucm101.lvIEC101Master.CheckedItems.Count == 1)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete Master " + ucm101.lvIEC101Master.CheckedItems[0].Text + " ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        int iIndex = ucm101.lvIEC101Master.CheckedItems[0].Index;
                        if (result == DialogResult.Yes)
                        {
                            IEC101GroupTreeNode.Nodes.Remove(m101List.ElementAt(iIndex).getTreeNode());
                            m101List.RemoveAt(iIndex);
                            ucm101.lvIEC101Master.Items[iIndex].Remove();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select atleast one Master ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    refreshList();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            string strRoutineName = "IEC101Group:btnDone_Click";
            try
            {
                if (!Validate()) return;
                List<KeyValuePair<string, string>> m101Data = Utils.getKeyValueAttributes(ucm101.grpIEC101);
                if (mode == Mode.ADD)
                {
                    TreeNode tmp = IEC101GroupTreeNode.Nodes.Add("IEC101_" + Utils.GenerateShortUniqueKey(), "IEC101", "IEC101Master", "IEC101Master");
                    m101List.Add(new IEC101Master("IEC101", m101Data, tmp));
                }
                else if (mode == Mode.EDIT)
                {
                    m101List[editIndex].updateAttributes(m101Data);
                }
                refreshList();
                //Namrata: 09/08/2017
                if (sender != null && e != null)
                {
                    ucm101.grpIEC101.Visible = false;
                    mode = Mode.NONE;
                    editIndex = -1;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            string strRoutineName = "IEC101Group:btnCancel_Click";
            try
            {
                ucm101.grpIEC101.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                Utils.resetValues(ucm101.grpIEC101);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            string strRoutineName = "IEC101Group:btnFirst_Click";
            try
            {
                if (ucm101.lvIEC101Master.Items.Count <= 0) return;
                if (m101List.ElementAt(0).IsNodeComment) return;
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
            string strRoutineName = "IEC101Group:btnPrev_Click";
            try
            {
                //Namrata: 27/07/2017
                btnDone_Click(null, null);
                if (editIndex - 1 < 0) return;
                if (m101List.ElementAt(editIndex - 1).IsNodeComment) return;
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
            string strRoutineName = "IEC101Group:btnNext_Click";
            try
            {
                //Namrata:27/07/2017
                btnDone_Click(null, null);
                if (editIndex + 1 >= ucm101.lvIEC101Master.Items.Count) return;
                if (m101List.ElementAt(editIndex + 1).IsNodeComment) return;
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
            string strRoutineName = "IEC101Group:btnLast_Click";
            try
            {
                if (ucm101.lvIEC101Master.Items.Count <= 0) return;
                if (m101List.ElementAt(m101List.Count - 1).IsNodeComment) return;
                editIndex = m101List.Count - 1;
                loadValues();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvIEC101Master_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "IEC101Group:lvIEC101Master_DoubleClick";
            try
            {
                // Ajay: 23/11/2018
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    if (ProtocolGateway.OppIEC101Group_ReadOnly) { return; }
                    else { }
                }
                if (ucm101.lvIEC101Master.SelectedItems.Count <= 0) return;
                ListViewItem lvi = ucm101.lvIEC101Master.SelectedItems[0];
                Utils.UncheckOthers(ucm101.lvIEC101Master, lvi.Index);
                if (m101List.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ucm101.grpIEC101.Visible = true;
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(ucm101.grpIEC101, true);
                loadValues();
                ucm101.cmbPortNo.Focus();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadDefaults()
        {
            string strRoutineName = "IEC101Group:loadDefaults";
            try
            {
                ucm101.cmbDebug.SelectedIndex = ucm101.cmbDebug.FindStringExact("3");
                ucm101.txtGiTime.Text = "600";
                ucm101.txtClockSyncInterval.Text = "300";
                ucm101.txtRefreshInterval.Text = "120";
                ucm101.txtFirmwareVersion.Text = Globals.FIRMWARE_VERSION;
                ucm101.txtDescription.Text = "IEC101_" + (Globals.MasterNo + 1).ToString();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadValues()
        {
            string strRoutineName = "IEC101Group:loadValues";
            try
            {
                IEC101Master m101 = m101List.ElementAt(editIndex);
                if (m101 != null)
                {
                    ucm101.txtMasterNo.Text = m101.MasterNum;
                    ucm101.cmbPortNo.SelectedIndex = ucm101.cmbPortNo.FindStringExact(m101.PortNum);
                    ucm101.cmbDebug.SelectedIndex = ucm101.cmbDebug.FindStringExact(m101.DEBUG);
                    ucm101.txtGiTime.Text = m101.GiTime;
                    ucm101.txtClockSyncInterval.Text = m101.ClockSyncInterval;
                    ucm101.txtRefreshInterval.Text = m101.RefreshInterval;
                    ucm101.txtFirmwareVersion.Text = m101.AppFirmwareVersion;
                    if (m101.Run.ToLower() == "yes") ucm101.chkRun.Checked = true;
                    else ucm101.chkRun.Checked = false;
                    ucm101.txtDescription.Text = m101.Description;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private bool Validate()
        {
            string strRoutineName = "IEC101Group:Validate";
            try
            {
                bool status = true;
                if (Utils.IsEmptyFields(ucm101.grpIEC101))//Check empty field's
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
        private void fillOptions()
        {
            string strRoutineName = "IEC101Group:fillOptions";
            try
            {
                //Fill Debug levels...
                ucm101.cmbDebug.Items.Clear();
                for (int i = 1; i <= Globals.MAX_DEBUG_LEVEL; i++)
                {
                    ucm101.cmbDebug.Items.Add(i.ToString());
                }
                ucm101.cmbDebug.SelectedIndex = 0;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void addListHeaders()
        {
            string strRoutineName = "IEC101Group:addListHeaders";
            try
            {
                ucm101.lvIEC101Master.Columns.Add("Master No.", 80, HorizontalAlignment.Left);
                ucm101.lvIEC101Master.Columns.Add("Port No.", 80, HorizontalAlignment.Left);
                ucm101.lvIEC101Master.Columns.Add("GI Time (sec)", 80, HorizontalAlignment.Left);
                ucm101.lvIEC101Master.Columns.Add("Clock Sync Interval (sec)", 150, HorizontalAlignment.Left);
                ucm101.lvIEC101Master.Columns.Add("Refresh Interval (sec)", 130, HorizontalAlignment.Left);
                ucm101.lvIEC101Master.Columns.Add("Firmware Version", 140, HorizontalAlignment.Left);
                ucm101.lvIEC101Master.Columns.Add("Debug Level", 110, HorizontalAlignment.Left);
                ucm101.lvIEC101Master.Columns.Add("Description", 150, HorizontalAlignment.Left);
                ucm101.lvIEC101Master.Columns.Add("Run", 80, HorizontalAlignment.Left);
                ucm101.lvIEC101Master.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void refreshList()
        {
            string strRoutineName = "IEC101Group:refreshList";
            try
            {
                int cnt = 0;
                ucm101.lvIEC101Master.Items.Clear();

                foreach (IEC101Master mt in m101List)
                {
                    string[] row = new string[9];
                    if (mt.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = mt.MasterNum;
                        row[1] = mt.PortNum;
                        row[2] = mt.GiTime;
                        row[3] = mt.ClockSyncInterval;
                        row[4] = mt.RefreshInterval;
                        row[5] = mt.AppFirmwareVersion;
                        row[6] = mt.DEBUG;
                        row[7] = mt.Description;
                        row[8] = mt.Run;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucm101.lvIEC101Master.Items.Add(lvItem);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public Control getView(List<string> kpArr)
        {
            string strRoutineName = "IEC101Group:getView";
            try
            {
                if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("IEC101Group_"))
                {
                    //Fill serial ports...
                    ucm101.cmbPortNo.Items.Clear();
                    foreach (SerialInterface si in Utils.getOpenProPlusHandle().getSerialPortConfiguration().getSerialInterfaces())
                    {
                        ucm101.cmbPortNo.Items.Add(si.PortNum);
                    }
                    if (ucm101.cmbPortNo.Items.Count > 0) ucm101.cmbPortNo.SelectedIndex = 0;
                    return ucm101;
                }
                kpArr.RemoveAt(0);
                if (kpArr.ElementAt(0).Contains("IEC101_"))
                {
                    int idx = -1;
                    string[] elems = kpArr.ElementAt(0).Split('_');
                    idx = Int32.Parse(elems[elems.Length - 1]);
                    if (m101List.Count <= 0) return null;
                    return m101List[idx].getView(kpArr);
                }
                else { }

                return null;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

        }
        public XmlNode exportXMLnode()
        {
            string strRoutineName = "IEC101Group:exportXMLnode";
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                StringWriter stringWriter = new StringWriter();
                XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
                XmlNode rootNode = xmlDoc.CreateElement(rnName);
                xmlDoc.AppendChild(rootNode);
                foreach (IEC101Master mn in m101List)
                {
                    XmlNode importNode = rootNode.OwnerDocument.ImportNode(mn.exportXMLnode(), true);
                    rootNode.AppendChild(importNode);
                }
                return rootNode;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

        }
        public string exportINI(string slaveNum, string slaveID, string element, ref int ctr)
        {
            string strRoutineName = "IEC101Group:exportINI";
            try
            {
                string iniData = "";
                foreach (IEC101Master iec101 in m101List)
                {
                    iniData += iec101.exportINI(slaveNum, slaveID, element, ref ctr);
                }
                return iniData;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public void regenerateAISequence()
        {
            string strRoutineName = "IEC101Group:regenerateAISequence";
            try
            {
                foreach (IEC101Master i101 in m101List)
                {
                    foreach (IED ied in i101.getIEDs())
                    {
                        ied.regenerateAISequence();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void regenerateAOSequence()
        {
            string strRoutineName = "IEC101Group:regenerateAOSequence";
            try
            {
                foreach (IEC101Master i101 in m101List)
                {
                    foreach (IED ied in i101.getIEDs())
                    {
                        ied.regenerateAOSequence();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void regenerateDISequence()
        {
            string strRoutineName = "IEC101Group:regenerateDISequence";
            try
            {
                foreach (IEC101Master i101 in m101List)
                {
                    foreach (IED ied in i101.getIEDs())
                    {
                        ied.regenerateDISequence();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void regenerateDOSequence()
        {
            string strRoutineName = "IEC101Group:regenerateDOSequence";
            try
            {
                foreach (IEC101Master i101 in m101List)
                {
                    foreach (IED ied in i101.getIEDs())
                    {
                        ied.regenerateDOSequence();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void regenerateENSequence()
        {
            string strRoutineName = "IEC101Group:regenerateENSequence";
            try
            {
                foreach (IEC101Master i101 in m101List)
                {
                    foreach (IED ied in i101.getIEDs())
                    {
                        ied.regenerateENSequence();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void parseIECGNode(XmlNode iecgNode, TreeNode tn)
        {
            string strRoutineName = "IEC101Group:parseIECGNode";
            try
            {
                rnName = iecgNode.Name;
                tn.Nodes.Clear();
                IEC101GroupTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...
                foreach (XmlNode node in iecgNode)
                {
                    if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                    TreeNode tmp = tn.Nodes.Add("IEC101_" + Utils.GenerateShortUniqueKey(), "IEC101", "IEC101Master", "IEC101Master");
                    m101List.Add(new IEC101Master(node, tmp));
                }
                refreshList();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public int getCount()
        {
            string strRoutineName = "IEC101Group";
            try
            {
                int ctr = 0;
                foreach (IEC101Master iecNode in m101List)
                {
                    if (iecNode.IsNodeComment) continue;
                    ctr++;
                }
                return ctr;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }

        }
        public List<IEC101Master> getIEC101Masters()
        {
            string strRoutineName = "IEC101Group:getIEC101Masters";
            try
            {
                return m101List;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public List<IEC101Master> getIEC101MastersByFilter(string masterID)
        {
            string strRoutineName = "IEC101Group:getIEC101MastersByFilter";
            try
            {
                List<IEC101Master> mList = new List<IEC101Master>();
                if (masterID.ToLower() == "all") return m101List;
                else
                    foreach (IEC101Master iec in m101List)
                    {
                        if (iec.getMasterID == masterID)
                        {
                            mList.Add(iec);
                            break;
                        }
                    }
                return mList;

            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        //Namrata:22/6/2017
        public List<IED> getIEC101IEDsByFilter(string masterID)
        {
            string strRoutineName = "IEC101Group:getIEC101IEDsByFilter";
            try
            {
                List<IED> iLst = new List<IED>();
                if (masterID.ToLower() == "all")
                {
                    foreach (IEC101Master iec in m101List)
                    {
                        foreach (IED ied in iec.getIEDs())
                        {
                            iLst.Add(ied);
                        }
                    }
                }
                else
                {
                    foreach (IEC101Master iec in m101List)
                    {
                        if (iec.getMasterID == masterID)
                        {
                            foreach (IED ied in iec.getIEDs())
                            {
                                iLst.Add(ied);
                            }
                            break;
                        }
                    }
                }
                return iLst;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

        }

    }
}
