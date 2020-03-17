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
    public class SPORTGroup
    {
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        private string rnName = "SPORTGroup";
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        public List<SPORTMaster> m101List = new List<SPORTMaster>();
        ucSPORTGroup ucmSPORT = new ucSPORTGroup();
        private TreeNode SPORTGroupTreeNode;

        public SPORTGroup(TreeNode tn)
        {
            tn.Nodes.Clear();
            SPORTGroupTreeNode = tn;
            ucmSPORT.btnAddClick += new System.EventHandler(this.btnAdd_Click);
            ucmSPORT.lvSPORTMasterItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvSPORTMaster_ItemCheck);
            ucmSPORT.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
            ucmSPORT.btnDoneClick += new System.EventHandler(this.btnDone_Click);
            ucmSPORT.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
            ucmSPORT.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
            ucmSPORT.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
            ucmSPORT.btnNextClick += new System.EventHandler(this.btnNext_Click);
            ucmSPORT.btnLastClick += new System.EventHandler(this.btnLast_Click);
            ucmSPORT.lvSPORTMasterDoubleClick += new System.EventHandler(this.lvSPORTMaster_DoubleClick);
            addListHeaders();
            fillOptions();
        }
        private void lvSPORTMaster_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    ucmSPORT.lvSPORTMaster.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
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
            if (m101List.Count >= Globals.MaxSPORTMaster)
            {
                MessageBox.Show("Maximum " + Globals.MaxSPORTMaster + " SPORT Masters are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            mode = Mode.ADD;
            editIndex = -1;
            Utils.resetValues(ucmSPORT.grpSPORT);
            Utils.showNavigation(ucmSPORT.grpSPORT, false);
            //Namrata: 3/7/2017
            loadDefaults();
            ucmSPORT.txtMasterNo.Text = (Globals.MasterNo + 1).ToString();
            ucmSPORT.grpSPORT.Visible = true;
            ucmSPORT.cmbPortNo.Focus();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ucmSPORT.lvSPORTMaster.Items.Count == 0)
            {
                MessageBox.Show("Please add atleast one master ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (ucmSPORT.lvSPORTMaster.CheckedItems.Count == 1)
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete Master " + ucmSPORT.lvSPORTMaster.CheckedItems[0].Text + " ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    int iIndex = ucmSPORT.lvSPORTMaster.CheckedItems[0].Index;
                    if (result == DialogResult.Yes)
                    {
                        SPORTGroupTreeNode.Nodes.Remove(m101List.ElementAt(iIndex).getTreeNode());
                        m101List.RemoveAt(iIndex);
                        ucmSPORT.lvSPORTMaster.Items[iIndex].Remove();
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
            List<KeyValuePair<string, string>> m101Data = Utils.getKeyValueAttributes(ucmSPORT.grpSPORT);
            if (mode == Mode.ADD)
            {
                TreeNode tmp = SPORTGroupTreeNode.Nodes.Add("SPORT_" + Utils.GenerateShortUniqueKey(), "SPORT", "SPORTMaster", "SPORTMaster");
                m101List.Add(new SPORTMaster("SPORT", m101Data, tmp));
            }
            else if (mode == Mode.EDIT)
            {
                m101List[editIndex].updateAttributes(m101Data);
            }
            refreshList();
            if (sender != null && e != null)
            {
                ucmSPORT.grpSPORT.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ucmSPORT.grpSPORT.Visible = false;
            mode = Mode.NONE;
            editIndex = -1;
            Utils.resetValues(ucmSPORT.grpSPORT);
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (ucmSPORT.lvSPORTMaster.Items.Count <= 0) return;
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
            if (editIndex + 1 >= ucmSPORT.lvSPORTMaster.Items.Count) return;
            if (m101List.ElementAt(editIndex + 1).IsNodeComment) return;
            editIndex++;
            loadValues();
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            if (ucmSPORT.lvSPORTMaster.Items.Count <= 0) return;
            if (m101List.ElementAt(m101List.Count - 1).IsNodeComment) return;
            editIndex = m101List.Count - 1;
            loadValues();
        }

        private void lvSPORTMaster_DoubleClick(object sender, EventArgs e)
        {
            // Ajay: 23/11/2018
            if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
            {
                if (ProtocolGateway.OppSPORTGroup_ReadOnly) { return; }
                else { }
            }

            if (ucmSPORT.lvSPORTMaster.SelectedItems.Count <= 0) return;
            ListViewItem lvi = ucmSPORT.lvSPORTMaster.SelectedItems[0];
            Utils.UncheckOthers(ucmSPORT.lvSPORTMaster, lvi.Index);
            if (m101List.ElementAt(lvi.Index).IsNodeComment)
            {
                MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ucmSPORT.grpSPORT.Visible = true;
            mode = Mode.EDIT;
            editIndex = lvi.Index;
            Utils.showNavigation(ucmSPORT.grpSPORT, true);
            loadValues();
            ucmSPORT.cmbPortNo.Focus();
        }
        private void loadDefaults()
        {
            ucmSPORT.cmbDebug.SelectedIndex = ucmSPORT.cmbDebug.FindStringExact("3");
            ucmSPORT.txtGiTime.Text = "600";
            ucmSPORT.txtClockSyncInterval.Text = "300";
            ucmSPORT.txtRefreshInterval.Text = "120";
            ucmSPORT.txtFirmwareVersion.Text = Globals.FIRMWARE_VERSION;
            ucmSPORT.txtDescription.Text = "SPORT_" + (Globals.MasterNo + 1).ToString();
        }
        private void loadValues()
        {
            SPORTMaster m101 = m101List.ElementAt(editIndex);
            if (m101 != null)
            {
                ucmSPORT.txtMasterNo.Text = m101.MasterNum;
                ucmSPORT.cmbPortNo.SelectedIndex = ucmSPORT.cmbPortNo.FindStringExact(m101.PortNum);
                ucmSPORT.cmbDebug.SelectedIndex = ucmSPORT.cmbDebug.FindStringExact(m101.DEBUG);
                ucmSPORT.txtGiTime.Text = m101.GiTime;
                ucmSPORT.txtClockSyncInterval.Text = m101.ClockSyncInterval;
                ucmSPORT.txtRefreshInterval.Text = m101.RefreshInterval;
                ucmSPORT.txtFirmwareVersion.Text = m101.AppFirmwareVersion;
                if (m101.Run.ToLower() == "yes") ucmSPORT.chkRun.Checked = true;
                else ucmSPORT.chkRun.Checked = false;
                ucmSPORT.txtDescription.Text = m101.Description;
            }
        }
        private bool Validate()
        {
            bool status = true;
            //Check empty field's
            if (Utils.IsEmptyFields(ucmSPORT.grpSPORT))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return status;
        }
        private void fillOptions()
        {
            //Fill Debug levels...
            ucmSPORT.cmbDebug.Items.Clear();
            for (int i = 1; i <= Globals.MAX_DEBUG_LEVEL; i++)
            {
                ucmSPORT.cmbDebug.Items.Add(i.ToString());
            }
            ucmSPORT.cmbDebug.SelectedIndex = 0;
        }
        private void addListHeaders()
        {
            ucmSPORT.lvSPORTMaster.Columns.Add("Master No.", 80, HorizontalAlignment.Left);
            ucmSPORT.lvSPORTMaster.Columns.Add("Port No.", 80, HorizontalAlignment.Left);
            ucmSPORT.lvSPORTMaster.Columns.Add("GI Time (sec)", 80, HorizontalAlignment.Left);
            ucmSPORT.lvSPORTMaster.Columns.Add("Clock Sync Interval (sec)", 150, HorizontalAlignment.Left);
            ucmSPORT.lvSPORTMaster.Columns.Add("Refresh Interval (sec)", 130, HorizontalAlignment.Left);
            ucmSPORT.lvSPORTMaster.Columns.Add("Firmware Version", 140, HorizontalAlignment.Left);
            ucmSPORT.lvSPORTMaster.Columns.Add("Debug Level", 110, HorizontalAlignment.Left);
            ucmSPORT.lvSPORTMaster.Columns.Add("Description", 150, HorizontalAlignment.Left);
            ucmSPORT.lvSPORTMaster.Columns.Add("Run", 80, HorizontalAlignment.Left);
            ucmSPORT.lvSPORTMaster.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
        private void refreshList()
        {
            int cnt = 0;
            ucmSPORT.lvSPORTMaster.Items.Clear();
            foreach (SPORTMaster mt in m101List)
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
                ucmSPORT.lvSPORTMaster.Items.Add(lvItem);
            }
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("SPORTGroup_"))
            {
                //Fill serial ports...
                ucmSPORT.cmbPortNo.Items.Clear();
                foreach (SerialInterface si in Utils.getOpenProPlusHandle().getSerialPortConfiguration().getSerialInterfaces())
                {
                    ucmSPORT.cmbPortNo.Items.Add(si.PortNum);
                }
                if (ucmSPORT.cmbPortNo.Items.Count > 0) ucmSPORT.cmbPortNo.SelectedIndex = 0;
                return ucmSPORT;
            }
            kpArr.RemoveAt(0);
            if (kpArr.ElementAt(0).Contains("SPORT_"))
            {
                int idx = -1;
                string[] elems = kpArr.ElementAt(0).Split('_');
                idx = Int32.Parse(elems[elems.Length - 1]);
                if (m101List.Count <= 0) return null;
                return m101List[idx].getView(kpArr);
            }
            else
            {
                Console.WriteLine("***** View for element: {0} not supported!!!", kpArr.ElementAt(0));
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
            foreach (SPORTMaster mn in m101List)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(mn.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }
            return rootNode;
        }

        public string exportINI(string slaveNum, string slaveID, string element, ref int ctr)
        {
            string iniData = "";
            foreach (SPORTMaster iec101 in m101List)
            {
                iniData += iec101.exportINI(slaveNum, slaveID, element, ref ctr);
            }
            return iniData;
        }
        public void regenerateAISequence()
        {
            foreach (SPORTMaster i101 in m101List)
            {
                foreach (IED ied in i101.getIEDs())
                {
                    ied.regenerateAISequence();
                }
            }
        }
        public void regenerateAOSequence()
        {
            foreach (SPORTMaster i101 in m101List)
            {
                foreach (IED ied in i101.getIEDs())
                {
                    ied.regenerateAOSequence();
                }
            }
        }
        public void regenerateDISequence()
        {
            foreach (SPORTMaster i101 in m101List)
            {
                foreach (IED ied in i101.getIEDs())
                {
                    ied.regenerateDISequence();
                }
            }
        }
        public void regenerateDOSequence()
        {
            foreach (SPORTMaster i101 in m101List)
            {
                foreach (IED ied in i101.getIEDs())
                {
                    ied.regenerateDOSequence();
                }
            }
        }
        public void regenerateENSequence()
        {
            foreach (SPORTMaster i101 in m101List)
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
            SPORTGroupTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...
            foreach (XmlNode node in iecgNode)
            {
                if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                TreeNode tmp = tn.Nodes.Add("SPORT_" + Utils.GenerateShortUniqueKey(), "SPORT", "SPORTMaster", "SPORTMaster");
                m101List.Add(new SPORTMaster(node, tmp));
            }
            refreshList();
        }
        public int getCount()
        {
            int ctr = 0;
            foreach (SPORTMaster iecNode in m101List)
            {
                if (iecNode.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }
        public List<SPORTMaster> getSPORTMasters()
        {
            return m101List;
        }
        public List<SPORTMaster> getSPORTMastersByFilter(string masterID)
        {
            List<SPORTMaster> mList = new List<SPORTMaster>();
            if (masterID.ToLower() == "all") return m101List;
            else
                foreach (SPORTMaster iec in m101List)
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
        public List<IED> getSPORTIEDsByFilter(string masterID)
        {
            List<IED> iLst = new List<IED>();
            if (masterID.ToLower() == "all")
            {
                foreach (SPORTMaster iec in m101List)
                {
                    foreach (IED ied in iec.getIEDs())
                    {
                        iLst.Add(ied);
                    }
                }
            }
            else
            {
                foreach (SPORTMaster iec in m101List)
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
        public List<IED> getSPORTIEDsByFilter(string masterID, string iedID)
        {
            List<IED> iLst = new List<IED>();

            foreach (SPORTMaster iec in m101List)
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
