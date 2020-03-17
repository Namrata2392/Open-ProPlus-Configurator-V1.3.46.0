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
    public class IEC104MasterGroup
    {
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        //Ajay:09/07/2018
        private string rnName = "IEC104MasterGroup";
        //private string rnName = "IEC104Group";
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        public List<IEC104Master> m101List = new List<IEC104Master>();
        ucIEC104MasterGroup ucm104 = new ucIEC104MasterGroup();
        ucAIlist ucai = new ucAIlist();
        private TreeNode IEC104GroupTreeNode;

        public IEC104MasterGroup(TreeNode tn)
        {
            tn.Nodes.Clear();
            IEC104GroupTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...
            ucm104.btnAddClick += new System.EventHandler(this.btnAdd_Click);
            ucm104.lvIEC104MasterItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvIEC104Master_ItemCheck);
            ucm104.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
            ucm104.btnDoneClick += new System.EventHandler(this.btnDone_Click);
            ucm104.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
            ucm104.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
            ucm104.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
            ucm104.btnNextClick += new System.EventHandler(this.btnNext_Click);
            ucm104.btnLastClick += new System.EventHandler(this.btnLast_Click);
            ucm104.lvIEC104MasterDoubleClick += new System.EventHandler(this.lvIEC101Master_DoubleClick);
            addListHeaders();
            fillOptions();
        }
        private void lvIEC104Master_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    ucm104.lvIEC104Master.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
                    {
                        item.Checked = false;
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnAdd_Click";
            try
            {
                if (m101List.Count >= Globals.MaxIEC104Master)
                {
                    MessageBox.Show("Maximum " + Globals.MaxIEC104Master + " IEC104 Masters are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                mode = Mode.ADD;
                editIndex = -1;
                Utils.resetValues(ucm104.grpIEC104);
                Utils.showNavigation(ucm104.grpIEC104, false);
                loadDefaults();
                ucm104.txtMasterNo.Text = (Globals.MasterNo + 1).ToString();
                ucm104.grpIEC104.Visible = true;
                ucm104.cmbPortNo.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ucm104.lvIEC104Master.Items.Count == 0)
            {
                MessageBox.Show("Please add atleast one master ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (ucm104.lvIEC104Master.CheckedItems.Count == 1)
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete Master " + ucm104.lvIEC104Master.CheckedItems[0].Text + " ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    int iIndex = ucm104.lvIEC104Master.CheckedItems[0].Index;
                    if (result == DialogResult.Yes)
                    {
                        IEC104GroupTreeNode.Nodes.Remove(m101List.ElementAt(iIndex).getTreeNode());
                        m101List.RemoveAt(iIndex);
                        ucm104.lvIEC104Master.Items[iIndex].Remove();
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
        private void btnDone_Click(object sender, EventArgs e)
        {
            if (!Validate()) return;
            List<KeyValuePair<string, string>> m101Data = Utils.getKeyValueAttributes(ucm104.grpIEC104);
            if (mode == Mode.ADD)
            {
                TreeNode tmp = IEC104GroupTreeNode.Nodes.Add("IEC104_" + Utils.GenerateShortUniqueKey(), "IEC104", "IEC104Master", "IEC104Master");
                m101List.Add(new IEC104Master("IEC104", m101Data, tmp));
            }
            else if (mode == Mode.EDIT)
            {
                m101List[editIndex].updateAttributes(m101Data);
            }
            refreshList();
            //Namrata: 09/08/2017
            if (sender != null && e != null)
            {
                ucm104.grpIEC104.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucm103 btnCancel_Click clicked in class!!!");
            ucm104.grpIEC104.Visible = false;
            mode = Mode.NONE;
            editIndex = -1;
            Utils.resetValues(ucm104.grpIEC104);
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (ucm104.lvIEC104Master.Items.Count <= 0) return;
            if (m101List.ElementAt(0).IsNodeComment) return;
            editIndex = 0;
            loadValues();
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            btnDone_Click(null, null);
            if (editIndex - 1 < 0) return;
            if (m101List.ElementAt(editIndex - 1).IsNodeComment) return;
            editIndex--;
            loadValues();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            btnDone_Click(null, null);
            if (editIndex + 1 >= ucm104.lvIEC104Master.Items.Count) return;
            if (m101List.ElementAt(editIndex + 1).IsNodeComment) return;
            editIndex++;
            loadValues();
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            if (ucm104.lvIEC104Master.Items.Count <= 0) return;
            if (m101List.ElementAt(m101List.Count - 1).IsNodeComment) return;
            editIndex = m101List.Count - 1;
            loadValues();
        }
        private void lvIEC101Master_DoubleClick(object sender, EventArgs e)
        {
            // Ajay: 23/11/2018
            if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
            {
                if (ProtocolGateway.OppIEC104Group_ReadOnly) { return; }
                else { }
            }

            if (ucm104.lvIEC104Master.SelectedItems.Count <= 0) return;
            ListViewItem lvi = ucm104.lvIEC104Master.SelectedItems[0];
            Utils.UncheckOthers(ucm104.lvIEC104Master, lvi.Index);
            if (m101List.ElementAt(lvi.Index).IsNodeComment)
            {
                MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ucm104.grpIEC104.Visible = true;
            mode = Mode.EDIT;
            editIndex = lvi.Index;
            Utils.showNavigation(ucm104.grpIEC104, true);
            loadValues();
            ucm104.cmbPortNo.Focus();
        }
        private void loadDefaults()
        {
            ucm104.cmbDebug.SelectedIndex = ucm104.cmbDebug.FindStringExact("3");
            ucm104.txtGiTime.Text = "600";
            ucm104.txtClockSyncInterval.Text = "300";
            ucm104.txtRefreshInterval.Text = "120";
            ucm104.txtFirmwareVersion.Text = Globals.FIRMWARE_VERSION;
            ucm104.txtDescription.Text = "IEC104_" + (Globals.MasterNo + 1).ToString();
        }
        private void loadValues()
        {
            IEC104Master m101 = m101List.ElementAt(editIndex);
            if (m101 != null)
            {
                ucm104.txtMasterNo.Text = m101.MasterNum;
                ucm104.cmbPortNo.SelectedIndex = ucm104.cmbPortNo.FindStringExact(m101.PortNum);
                ucm104.cmbDebug.SelectedIndex = ucm104.cmbDebug.FindStringExact(m101.DEBUG);
                ucm104.txtGiTime.Text = m101.GiTime;
                ucm104.txtClockSyncInterval.Text = m101.ClockSyncInterval;
                ucm104.txtRefreshInterval.Text = m101.RefreshInterval;
                ucm104.txtFirmwareVersion.Text = m101.AppFirmwareVersion;
                if (m101.Run.ToLower() == "yes") ucm104.chkRun.Checked = true;
                else ucm104.chkRun.Checked = false;
                ucm104.txtDescription.Text = m101.Description;
            }
        }
        private bool Validate()
        {
            bool status = true;
            //Check empty field's
            if (Utils.IsEmptyFields(ucm104.grpIEC104))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return status;
        }
        private void fillOptions()
        {
            //Fill Debug levels...
            ucm104.cmbDebug.Items.Clear();
            for (int i = 1; i <= Globals.MAX_DEBUG_LEVEL; i++)
            {
                ucm104.cmbDebug.Items.Add(i.ToString());
            }
            ucm104.cmbDebug.SelectedIndex = 0;
        }
        private void addListHeaders()
        {
            ucm104.lvIEC104Master.Columns.Add("Master No.", 80, HorizontalAlignment.Left);
            ucm104.lvIEC104Master.Columns.Add("Port No.", 80, HorizontalAlignment.Left);
            ucm104.lvIEC104Master.Columns.Add("GI Time (sec)", 80, HorizontalAlignment.Left);
            ucm104.lvIEC104Master.Columns.Add("Clock Sync Interval (sec)", 150, HorizontalAlignment.Left);
            ucm104.lvIEC104Master.Columns.Add("Refresh Interval (sec)", 130, HorizontalAlignment.Left);
            ucm104.lvIEC104Master.Columns.Add("Firmware Version", 140, HorizontalAlignment.Left);
            ucm104.lvIEC104Master.Columns.Add("Debug Level", 110, HorizontalAlignment.Left);
            ucm104.lvIEC104Master.Columns.Add("Description", 150, HorizontalAlignment.Left);
            ucm104.lvIEC104Master.Columns.Add("Run", 80, HorizontalAlignment.Left);
            ucm104.lvIEC104Master.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
        private void refreshList()
        {
            int cnt = 0;
            ucm104.lvIEC104Master.Items.Clear();
            foreach (IEC104Master mt in m101List)
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
                ucm104.lvIEC104Master.Items.Add(lvItem);
            }
        }
        public Control getView(List<string> kpArr)
        {
            //if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("IEC104Group_"))
            //Ajay:09/07/2018
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("IEC104MasterGroup_"))
            {
                //Fill serial ports...
                ucm104.cmbPortNo.Items.Clear();
                foreach (SerialInterface si in Utils.getOpenProPlusHandle().getSerialPortConfiguration().getSerialInterfaces())
                {
                    ucm104.cmbPortNo.Items.Add(si.PortNum);
                }
                if (ucm104.cmbPortNo.Items.Count > 0) ucm104.cmbPortNo.SelectedIndex = 0;
                return ucm104;
            }
            kpArr.RemoveAt(0);
            if (kpArr.ElementAt(0).Contains("IEC104_"))
            {
                int idx = -1;
                string[] elems = kpArr.ElementAt(0).Split('_');
                idx = Int32.Parse(elems[elems.Length - 1]);
                if (m101List.Count <= 0) return null;
                return m101List[idx].getView(kpArr);
            }
            else
            {
               
            }
            return null;
        }
        public XmlNode exportXMLnode()
        {
            XmlDocument xmlDoc = new XmlDocument();
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
            XmlNode rootNode = xmlDoc.CreateElement(rnName);
            xmlDoc.AppendChild(rootNode);
            foreach (IEC104Master mn in m101List)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(mn.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }
            return rootNode;
        }
        public string exportINI(string slaveNum, string slaveID, string element, ref int ctr)
        {
            string iniData = "";
            foreach (IEC104Master iec101 in m101List)
            {
                iniData += iec101.exportINI(slaveNum, slaveID, element, ref ctr);
            }
            return iniData;
        }
        public void regenerateAISequence()
        {
            foreach (IEC104Master i101 in m101List)
            {
                foreach (IED ied in i101.getIEDs())
                {
                    ied.regenerateAISequence();
                }
            }
        }
        public void regenerateAOSequence()
        {
            foreach (IEC104Master i101 in m101List)
            {
                foreach (IED ied in i101.getIEDs())
                {
                    ied.regenerateAOSequence();
                }
            }
        }
        public void regenerateDISequence()
        {
            foreach (IEC104Master i101 in m101List)
            {
                foreach (IED ied in i101.getIEDs())
                {
                    ied.regenerateDISequence();
                }
            }
        }
        public void regenerateDOSequence()
        {
            foreach (IEC104Master i101 in m101List)
            {
                foreach (IED ied in i101.getIEDs())
                {
                    ied.regenerateDOSequence();
                }
            }
        }
        public void regenerateENSequence()
        {
            foreach (IEC104Master i101 in m101List)
            {
                foreach (IED ied in i101.getIEDs())
                {
                    ied.regenerateENSequence();
                }
            }
        }
        public void parseIECGNode(XmlNode iecgNode, TreeNode tn)
        {
            rnName = iecgNode.Name;
            tn.Nodes.Clear();
            IEC104GroupTreeNode = tn;
            foreach (XmlNode node in iecgNode)
            {
                if (node.NodeType == XmlNodeType.Comment) continue;
                TreeNode tmp = tn.Nodes.Add("IEC104_" + Utils.GenerateShortUniqueKey(), "IEC104", "IEC104Master", "IEC104Master");
                m101List.Add(new IEC104Master(node, tmp));
            }
            refreshList();
        }
        public int getCount()
        {
            int ctr = 0;
            foreach (IEC104Master iecNode in m101List)
            {
                if (iecNode.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }
        public List<IEC104Master> getIEC104Masters()
        {
            return m101List;
        }
        public List<IEC104Master> getIEC101MastersByFilter(string masterID)
        {
            List<IEC104Master> mList = new List<IEC104Master>();
            if (masterID.ToLower() == "all") return m101List;
            else
                foreach (IEC104Master iec in m101List)
                {
                    if (iec.getMasterID == masterID)
                    {
                        mList.Add(iec);
                        break;
                    }
                }
            return mList;
        }
        //Namrata:22/6/2017
        public List<IED> getIEC101IEDsByFilter(string masterID)
        {
            List<IED> iLst = new List<IED>();
            if (masterID.ToLower() == "all")
            {
                foreach (IEC104Master iec in m101List)
                {
                    foreach (IED ied in iec.getIEDs())
                    {
                        iLst.Add(ied);
                    }
                }
            }
            else
            {
                foreach (IEC104Master iec in m101List)
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
        //Namrata:19/6/2017
        public List<IED> getIEC101IEDsByFilter(string masterID, string iedID)
        {
            List<IED> iLst = new List<IED>();

            foreach (IEC104Master iec in m101List)
            {
                if (iec.getMasterID == masterID)
                {
                    if (iedID.ToLower() == "all")
                    {
                        return iec.getIEDs();
                    }
                    else
                    {
                        foreach (IED ied in iec.getIEDs())
                        {
                            if (ied.getIEDID == iedID)
                            {
                                iLst.Add(ied);
                                break;
                            }
                        }
                    }
                    break;
                }
            }
            return iLst;
        }
    }
}
