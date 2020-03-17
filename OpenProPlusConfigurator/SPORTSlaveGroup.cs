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
    public class SPORTSlaveGroup
    {
        #region Decaration
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        private string rnName = "SPORTSlaveGroup";
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        private List<SPORTSlave> sSportList = new List<SPORTSlave>();  //Expose the list to others for slave mapping thru func()...
        ucGroupSPORTSlave ucsSPORT = new ucGroupSPORTSlave();
        private TreeNode SPORTGroupTreeNode;
        #endregion Decaration
        public SPORTSlaveGroup()
        {
            string strRoutineName = "SPORTSlaveGroup";
            try
            {
                ucsSPORT.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                ucsSPORT.lvSPORTSlaveItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvSPORTSlave_ItemCheck);
                ucsSPORT.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                ucsSPORT.btnexportSPORTINIClick += new System.EventHandler(this.btnexportSPORTINI_Click);
                ucsSPORT.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucsSPORT.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucsSPORT.lvSPORTSlaveDoubleClick += new System.EventHandler(this.lvSPORTSlave_DoubleClick);
                ucsSPORT.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                ucsSPORT.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                ucsSPORT.btnNextClick += new System.EventHandler(this.btnNext_Click);
                ucsSPORT.btnLastClick += new System.EventHandler(this.btnLast_Click);
                addListHeaders();
                fillOptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //private SlaveTypes slave = SlaveTypes.UNKNOWN;
        private void lvSPORTSlave_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string strRoutineName = "SPORTSlave_ItemCheck";
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    ucsSPORT.lvSPORTSlave.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
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
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnAdd_Click";
            try
            {
                if (sSportList.Count >= Globals.MaxSPORTSlave)
                {
                    MessageBox.Show("Maximum " + Globals.MaxSPORTSlave + " SPORT Slaves are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //Ajay: 25/08/2018 
                bool IsContinue = false;
                if (Globals.TotalSlavesCount < Globals.MaxTotalNoOfSlaves)
                { IsContinue = true;
                }
                else
                {
                    DialogResult rslt = MessageBox.Show("Maximum " + Globals.MaxTotalNoOfSlaves + " Slaves are recommended to be added." + Environment.NewLine + "Do you still want to continue?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rslt.ToString().ToLower() == "no")
                    { return; }
                    else { IsContinue = true; }
                }
                if (IsContinue)
                {
                    mode = Mode.ADD;
                    editIndex = -1;
                    Utils.resetValues(ucsSPORT.grpSPORT);
                    Utils.showNavigation(ucsSPORT.grpSPORT, false);
                    loadDefaults();
                    ucsSPORT.txtSlaveNum.Text = (Globals.SlaveNo + 1).ToString();
                    ucsSPORT.grpSPORT.Visible = true;
                    ucsSPORT.txtSlaveNum.Focus();
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
                ucsSPORT.grpSPORT.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                Utils.resetValues(ucsSPORT.grpSPORT);
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
                ucsSPORT.cmbPortNo.SelectedIndex = ucsSPORT.cmbPortNo.FindStringExact("1");
                ucsSPORT.txtASDUaddress.Text = "1";
                //Ajay: 12/07/2018 ---Default IOA=1 by Aditya K---
                //ucsSPORT.cmbIOASize.SelectedIndex = ucsSPORT.cmbIOASize.FindStringExact("2");
                ucsSPORT.cmbIOASize.SelectedIndex = ucsSPORT.cmbIOASize.FindStringExact("1"); //Ajay: 12/07/2018
                ucsSPORT.txtEventQSize.Text = "1000";
                ucsSPORT.txtFirmwareVersion.Text = Globals.FIRMWARE_VERSION;
                ucsSPORT.cmbDebug.SelectedIndex = ucsSPORT.cmbDebug.FindStringExact("3");
                ucsSPORT.chkRun.Checked = true;
                //Ajay: 27/08/2018
                //ucsSPORT.txtSPORTType.Text = "SPORT";
                ucsSPORT.cmbSportType.SelectedIndex = 0;
                ucsSPORT.chkbxEvent.Checked = false; //Ajay: 31/08/2018
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvSPORTSlave_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "lvSPORTSlave_DoubleClick";
            try
            {
                // Ajay: 23/11/2018
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    if (ProtocolGateway.OppSPORTSlaveGroup_ReadOnly) { return; }
                    else { }
                }

                if (ucsSPORT.lvSPORTSlave.SelectedItems.Count <= 0) return;
                ListViewItem lvi = ucsSPORT.lvSPORTSlave.SelectedItems[0];
                Utils.UncheckOthers(ucsSPORT.lvSPORTSlave, lvi.Index);
                if (sSportList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ucsSPORT.grpSPORT.Visible = true;
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(ucsSPORT.grpSPORT, true);
                loadValues();
                ucsSPORT.txtSlaveNum.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnexportSPORTINI_Click(object sender, EventArgs e)
        {

        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnDelete_Click";
            try
            {
                Utils.WriteLine(VerboseLevel.DEBUG, "*** mbsList count: {0} lv count: {1}", sSportList.Count, ucsSPORT.lvSPORTSlave.Items.Count);
                if (ucsSPORT.lvSPORTSlave.Items.Count == 0)
                {
                    MessageBox.Show("Please add atleast one slave ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (ucsSPORT.lvSPORTSlave.CheckedItems.Count == 1)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete Slave No." + ucsSPORT.lvSPORTSlave.CheckedItems[0].Text + " ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        int iIndex = ucsSPORT.lvSPORTSlave.CheckedItems[0].Index;
                        Utils.WriteLine(VerboseLevel.DEBUG, "*** removing indices: {0}", iIndex);
                        if (result == DialogResult.Yes)
                        {
                            sSportList.RemoveAt(iIndex);
                            ucsSPORT.lvSPORTSlave.Items[iIndex].Remove();
                            Globals.TotalSlavesCount -= 1; //Ajay: 25/08/2018
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select atleast one Slave ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    Utils.WriteLine(VerboseLevel.DEBUG, "*** mbsList count: {0} lv count: {1}", sSportList.Count, ucsSPORT.lvSPORTSlave.Items.Count);
                    refreshList();
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

                List<KeyValuePair<string, string>> sSportData = Utils.getKeyValueAttributes(ucsSPORT.grpSPORT);
                if (mode == Mode.ADD)
                {
                    TreeNode tmp = null;
                    sSportList.Add(new SPORTSlave("SPORTSlave", sSportData, tmp));
                    Globals.TotalSlavesCount += 1; //Ajay: 25/08/2018
                }
                else if (mode == Mode.EDIT)
                {
                    sSportList[editIndex].updateAttributes(sSportData);
                }
                refreshList();
                //Namrata: 09/08/2017
                if (sender != null && e != null)
                {
                    ucsSPORT.grpSPORT.Visible = false;
                    mode = Mode.NONE;
                    editIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool Validate()
        {
            bool status = true;
            //Check empty field's
            if (Utils.IsEmptyFields(ucsSPORT.grpSPORT))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return status;
        }
        private void fillOptions()
        {
            string strRoutineName = "fillOptions";
            try
            {
                ucsSPORT.cmbPortNo.Items.Clear();
                foreach (String br in SPORTSlave.getPortNo())
                {
                    ucsSPORT.cmbPortNo.Items.Add(br.ToString());
                }
                ucsSPORT.cmbPortNo.SelectedIndex = 0;

                //Fill IOA size...
                ucsSPORT.cmbIOASize.Items.Clear();
                foreach (String ioa in SPORTSlave.getIOAsizes())
                {
                    ucsSPORT.cmbIOASize.Items.Add(ioa.ToString());
                }
                ucsSPORT.cmbIOASize.SelectedIndex = 0;

                //Fill Debug levels...
                ucsSPORT.cmbDebug.Items.Clear();
                for (int i = 1; i <= Globals.MAX_DEBUG_LEVEL; i++)
                {
                    ucsSPORT.cmbDebug.Items.Add(i.ToString());
                }
                ucsSPORT.cmbDebug.SelectedIndex = 0;
                //Ajay: 27/08/2018  Fill Sport Type...
                ucsSPORT.cmbSportType.Items.Clear();
                foreach (String st in SPORTSlave.getSportTypes())
                {
                    ucsSPORT.cmbSportType.Items.Add(st.ToString());
                }
                if (ucsSPORT.cmbSportType.Items.Count > 0) { ucsSPORT.cmbSportType.SelectedIndex = 0; }
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
                SPORTSlave sSport = sSportList.ElementAt(editIndex);
                if (sSport != null)
                {
                    ucsSPORT.txtSlaveNum.Text = sSport.SlaveNum;
                    ucsSPORT.cmbPortNo.SelectedIndex = ucsSPORT.cmbPortNo.FindStringExact(sSport.PortNum);
                    //Ajay: 27/08/2018
                    //ucsSPORT.txtSPORTType.Text = sSport.SportType;
                    ucsSPORT.cmbSportType.SelectedIndex = ucsSPORT.cmbSportType.FindStringExact(sSport.SportType);
                    ucsSPORT.txtASDUaddress.Text = sSport.ASDUAddress;
                    ucsSPORT.cmbIOASize.SelectedIndex = ucsSPORT.cmbIOASize.FindStringExact(sSport.IOASize);
                    ucsSPORT.txtEventQSize.Text = sSport.EventQSize;
                    ucsSPORT.txtFirmwareVersion.Text = sSport.AppFirmwareVersion;
                    ucsSPORT.cmbDebug.SelectedIndex = ucsSPORT.cmbDebug.FindStringExact(sSport.DEBUG);
                    //Ajay: 31/08/2018
                    if (sSport.Event.ToLower() == "yes")
                    { ucsSPORT.chkbxEvent.Checked = true; }
                    else { ucsSPORT.chkbxEvent.Checked = false; }
                    //Ajay: 31/08/2018
                    if (sSport.Run.ToLower() == "yes")
                    { ucsSPORT.chkRun.Checked = true; }
                    else { ucsSPORT.chkRun.Checked = false; }
                }
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
                ucsSPORT.lvSPORTSlave.Columns.Add("Slave No.", 70, HorizontalAlignment.Left);
                ucsSPORT.lvSPORTSlave.Columns.Add("Port No.", 90, HorizontalAlignment.Left);
                ucsSPORT.lvSPORTSlave.Columns.Add("ASDU Address", 100, HorizontalAlignment.Left);
                ucsSPORT.lvSPORTSlave.Columns.Add("Sport Type", 100, HorizontalAlignment.Left);
                ucsSPORT.lvSPORTSlave.Columns.Add("IOA Size", 90, HorizontalAlignment.Left);
                ucsSPORT.lvSPORTSlave.Columns.Add("Event Queue Size", 100, HorizontalAlignment.Left);
                //ucsSPORT.lvSPORTSlave.Columns.Add("Event", 80, HorizontalAlignment.Left); //Ajay: 31/08/2018  //Ajay: 22/09/2018 Event removed
                ucsSPORT.lvSPORTSlave.Columns.Add("Firmware Version", 100, HorizontalAlignment.Left);
                ucsSPORT.lvSPORTSlave.Columns.Add("Debug Level", 90, HorizontalAlignment.Left);
                ucsSPORT.lvSPORTSlave.Columns.Add("Run", -2, HorizontalAlignment.Left);
                //ucsSPORT.lvSPORTSlave.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
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
                ucsSPORT.lvSPORTSlave.Items.Clear();
                //addListHeaders();
                foreach (SPORTSlave sl in sSportList)
                {
                    string[] row = new string[9];
                    if (sl.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = sl.SlaveNum;
                        row[1] = sl.PortNum;
                        row[2] = sl.ASDUAddress;
                        row[3] = sl.SportType;
                        row[4] = sl.IOASize;
                        row[5] = sl.EventQSize;
                        //row[6] = sl.Event;  //Ajay: 31/08/2018 //Ajay: 22/09/2018 Event removed
                        row[6] = sl.AppFirmwareVersion;
                        row[7] = sl.DEBUG;
                        row[8] = sl.Run;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucsSPORT.lvSPORTSlave.Items.Add(lvItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("SPORTSlaveGroup_"))
            {
                //Fill Port No.
                //fillPortNos();
                return ucsSPORT;
            }

            kpArr.RemoveAt(0);
            if (kpArr.ElementAt(0).Contains("SPORTSlave_"))
            {
                int idx = -1;
                string[] elems = kpArr.ElementAt(0).Split('_');
                Utils.WriteLine(VerboseLevel.DEBUG, "$$$$ elem0: {0} elem1: {1}", elems[0], elems[elems.Length - 1]);
                idx = Int32.Parse(elems[elems.Length - 1]);
                if (sSportList.Count <= 0) return null;
                return sSportList[idx].getView(kpArr);
            }
            else
            {
                Utils.WriteLine(VerboseLevel.WARNING, "***** View for element: {0} not supported!!!", kpArr.ElementAt(0));
            }

            return null;
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnFirst_Click";
            try
            {
                Console.WriteLine("*** ucsSPORTSlave btnFirst_Click clicked in class!!!");
                if (ucsSPORT.lvSPORTSlave.Items.Count <= 0) return;
                if (sSportList.ElementAt(0).IsNodeComment) return;
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
                Console.WriteLine("*** ucs104 btnPrev_Click clicked in class!!!");
                if (editIndex - 1 < 0) return;
                if (sSportList.ElementAt(editIndex - 1).IsNodeComment) return;
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
                Console.WriteLine("*** ucs104 btnNext_Click clicked in class!!!");
                if (editIndex + 1 >= ucsSPORT.lvSPORTSlave.Items.Count) return;
                if (sSportList.ElementAt(editIndex + 1).IsNodeComment) return;
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
                Console.WriteLine("*** ucs104 btnLast_Click clicked in class!!!");
                if (ucsSPORT.lvSPORTSlave.Items.Count <= 0) return;
                if (sSportList.ElementAt(sSportList.Count - 1).IsNodeComment) return;
                editIndex = sSportList.Count - 1;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void parseIECGNode(XmlNode iecgNode, TreeNode tn)
        {
            string strRoutineName = "parseIECGNode";
            try
            {
                rnName = iecgNode.Name;
                tn.Nodes.Clear();
                SPORTGroupTreeNode = tn;
                foreach (XmlNode node in iecgNode)
                {
                    TreeNode tmp = null;
                    if (node.NodeType == XmlNodeType.Comment) continue;
                    sSportList.Add(new SPORTSlave(node, tmp));
                }
                refreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public XmlNode exportXMLnode()
        {
            XmlDocument xmlDoc = new XmlDocument();
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
            XmlNode rootNode = xmlDoc.CreateElement(rnName);
            xmlDoc.AppendChild(rootNode);
            foreach (SPORTSlave sn in sSportList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(sn.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }
            return rootNode;
        }
        public int getCount()
        {
            int ctr = 0;
            foreach (SPORTSlave sSPORTNode in sSportList)
            {
                if (sSPORTNode.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }
        public List<SPORTSlave> getSPORTSlaves()
        {
            return sSportList;
        }
        public List<SPORTSlave> getSPORTSlavesByFilter(string slaveID)
        {
            List<SPORTSlave> sportList = new List<SPORTSlave>();
            if (slaveID.ToLower() == "all") return sSportList;
            else
                foreach (SPORTSlave iec in sSportList)
                {
                    if (iec.getSlaveID == slaveID)
                    {
                        sportList.Add(iec);
                        break;
                    }
                }

            return sportList;
        }
    }
}
