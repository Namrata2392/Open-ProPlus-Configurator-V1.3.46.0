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
using System.Text.RegularExpressions;

namespace OpenProPlusConfigurator
{
    public class GraphicalDisplaySlaveGroup
    {
        #region Declaration
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        private string rnName = "GraphicalDisplaySlaveGroup";
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        ucGroupGraphicalDisplaySlave ucGDSlave = new ucGroupGraphicalDisplaySlave();
        private TreeNode GraphicalDisplaySlaveGroupTreeNode;
        public List<GraphicalDisplaySlave> GraphicalDisplaySlaveList = new List<GraphicalDisplaySlave>(); //Expose the list to others for slave mapping...
        #endregion Declaration
        public GraphicalDisplaySlaveGroup()
        {
            string strRoutineName = "GraphicalDisplaySlaveGroup";
            try
            {
                ucGDSlave.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                ucGDSlave.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                ucGDSlave.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucGDSlave.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucGDSlave.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                ucGDSlave.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                ucGDSlave.btnNextClick += new System.EventHandler(this.btnNext_Click);
                ucGDSlave.btnLastClick += new System.EventHandler(this.btnLast_Click);
                ucGDSlave.lvGraphicalDisplayItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvGraphicalDisplay_ItemCheck);
                ucGDSlave.lvGraphicalDisplayDoubleClick += new System.EventHandler(this.lvGraphicalDisplay_DoubleClick);
                addListHeaders();
                fillOptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public GraphicalDisplaySlaveGroup(TreeNode tn)
        {
            string strRoutineName = "GraphicalDisplaySlaveGroup:GraphicalDisplaySlaveGroup";
            try
            {
                tn.Nodes.Clear();
                GraphicalDisplaySlaveGroupTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...
                GDSlave.GDSlaveTreeNode = GraphicalDisplaySlaveGroupTreeNode;
                ucGDSlave.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                ucGDSlave.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                ucGDSlave.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucGDSlave.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucGDSlave.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                ucGDSlave.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                ucGDSlave.btnNextClick += new System.EventHandler(this.btnNext_Click);
                ucGDSlave.btnLastClick += new System.EventHandler(this.btnLast_Click);
                ucGDSlave.lvGraphicalDisplayItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvGraphicalDisplay_ItemCheck);
                ucGDSlave.lvGraphicalDisplayDoubleClick += new System.EventHandler(this.lvGraphicalDisplay_DoubleClick);
                addListHeaders();
                fillOptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void fillOptions()
        {
            string strRoutineName = "SMSSlaveGroup: fillOptions";
            try
            {
                #region  Fill Debug levels...
                ucGDSlave.cmbDebug.Items.Clear();
                for (int i = 1; i <= Globals.MAX_DEBUG_LEVEL; i++)
                {
                    ucGDSlave.cmbDebug.Items.Add(i.ToString());
                }
                ucGDSlave.cmbDebug.SelectedIndex = 0;
                #endregion  Fill Debug levels...
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlaveGroup: btnAdd_Click";
            try
            {
                if (GraphicalDisplaySlaveList.Count >= Globals.MaxGraphicalDisplaySlave)
                {
                    MessageBox.Show("Maximum " + Globals.MaxGraphicalDisplaySlave + " Graphical Display Slaves are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //Ajay: 25/08/2018 
                bool IsContinue = false;
                if (Globals.TotalSlavesCount < Globals.MaxTotalNoOfSlaves)
                {
                    IsContinue = true;
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
                    Utils.resetValues(ucGDSlave.grpGraphicalDisplay);
                    Utils.showNavigation(ucGDSlave.grpGraphicalDisplay, false);
                    loadDefaults();
                    ucGDSlave.txtSlaveNum.Text = (Globals.SlaveNo + 1).ToString();
                    ucGDSlave.grpGraphicalDisplay.Visible = true;
                    ucGDSlave.txtSlaveNum.Focus();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlaveGroup: btnDelete_Click";
            try
            {
                if (ucGDSlave.lvGraphicalDisplay.Items.Count == 0)
                {
                    MessageBox.Show("Please add atleast one slave ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (ucGDSlave.lvGraphicalDisplay.CheckedItems.Count == 1)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete Slave No." + ucGDSlave.lvGraphicalDisplay.CheckedItems[0].Text + " ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        int iIndex = ucGDSlave.lvGraphicalDisplay.CheckedItems[0].Index;
                        if (result == DialogResult.Yes)
                        {
                            GraphicalDisplaySlaveGroupTreeNode.Nodes.Remove(GraphicalDisplaySlaveList.ElementAt(iIndex).getTreeNode());
                            GraphicalDisplaySlaveList.RemoveAt(iIndex);
                            ucGDSlave.lvGraphicalDisplay.Items[iIndex].Remove();
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
            string strRoutineName = "GraphicalDisplaySlaveGroup: btnDone_Click";
            try
            {
                // Ajay: 23/11/2018
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    if (ProtocolGateway.OppGraphicalDisplaySlaveGroup_ReadOnly) { return; }
                    else { }
                }

                if (!Validate()) return;
                List<KeyValuePair<string, string>> mbsData = Utils.getKeyValueAttributes(ucGDSlave.grpGraphicalDisplay);

                #region Only 1 Run YES allow
                //if (Utils.GraphicalDisplaySlaveList1.Count>0)
                //{
                //    List<GraphicalDisplaySlave> ListGD = Utils.GraphicalDisplaySlaveList1.Distinct().ToList();
                //    bool exists = ListGD.Where(w => w.Run =="YES").Any();
                //    if(exists==true)
                //    {
                //        if (mbsData[1].Value == "YES")
                //        {
                //            MessageBox.Show("Only 1 Run YES allow!! ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //            return;
                //        }

                //    }
                //}
                #endregion Only 1 Run YES allow
                if (mode == Mode.ADD)
                {
                    TreeNode tmp = null;
                    tmp = GraphicalDisplaySlaveGroupTreeNode.Nodes.Add("GraphicalDisplaySlave_" + Utils.GenerateShortUniqueKey(),"GraphicalDisplaySlave", "GDisplaySlave", "GDisplaySlave");
                    //tmp = GraphicalDisplaySlaveGroupTreeNode.Nodes.Add("GraphicalDisplaySlave_" + Utils.GenerateShortUniqueKey(), "GraphicalDisplaySlave", "GraphicalDisplaySlave", "GraphicalDisplaySlave");
                    GraphicalDisplaySlaveList.Add(new GraphicalDisplaySlave("GraphicalDisplaySlave", mbsData, tmp));
                    Globals.TotalSlavesCount += 1; //Ajay: 25/08/2018
                }
                else if (mode == Mode.EDIT)
                {
                    TreeNode tmp = null;
                    GraphicalDisplaySlaveList[editIndex].updateAttributes(mbsData);
                }
                refreshList();
                if (sender != null && e != null)
                {
                    ucGDSlave.grpGraphicalDisplay.Visible = false;
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
            string strRoutineName = "GraphicalDisplaySlaveGroup: btnCancel_Click";
            try
            {
                //Utils.IntIEC104Modbus = Utils.IntIEC104Modbus - 1;
                ucGDSlave.grpGraphicalDisplay.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlaveGroup: btnFirst_Click";
            try
            {
                if (ucGDSlave.lvGraphicalDisplay.Items.Count <= 0) return;
                if (GraphicalDisplaySlaveList.ElementAt(0).IsNodeComment) return;
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
            string strRoutineName = "GraphicalDisplaySlaveGroup: btnPrev_Click";
            try
            {
                //Namrata:27/7/2017
                btnDone_Click(null, null);
                if (editIndex - 1 < 0) return;
                if (GraphicalDisplaySlaveList.ElementAt(editIndex - 1).IsNodeComment) return;
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
            string strRoutineName = "GraphicalDisplaySlaveGroup: btnNext_Click";
            try
            {
                btnDone_Click(null, null);
                if (editIndex + 1 >= ucGDSlave.lvGraphicalDisplay.Items.Count) return;
                if (GraphicalDisplaySlaveList.ElementAt(editIndex + 1).IsNodeComment) return;
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
            string strRoutineName = "GraphicalDisplaySlaveGroup: btnLast_Click";
            try
            {
                if (ucGDSlave.lvGraphicalDisplay.Items.Count <= 0) return;
                if (GraphicalDisplaySlaveList.ElementAt(GraphicalDisplaySlaveList.Count - 1).IsNodeComment) return;
                editIndex = GraphicalDisplaySlaveList.Count - 1;
                loadValues();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvGraphicalDisplay_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlaveGroup:lvGraphicalDisplay_ItemCheck";
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    ucGDSlave.lvGraphicalDisplay.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
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
        private void lvGraphicalDisplay_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "lvSMSSlave_DoubleClick";
            try
            {
                // Ajay: 23/11/2018
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    if (ProtocolGateway.OppSPORTSlaveGroup_ReadOnly) { return; }
                    else { }
                }

                if (ucGDSlave.lvGraphicalDisplay.SelectedItems.Count <= 0) return;
                ListViewItem lvi = ucGDSlave.lvGraphicalDisplay.SelectedItems[0];
                Utils.UncheckOthers(ucGDSlave.lvGraphicalDisplay, lvi.Index);
                if (GraphicalDisplaySlaveList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ucGDSlave.grpGraphicalDisplay.Visible = true;
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(ucGDSlave.grpGraphicalDisplay, true);
                loadValues();
                ucGDSlave.txtSlaveNum.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadDefaults()
        {
            string strRoutineName = "GraphicalDisplaySlaveGroup:loadDefaults";
            try
            {
                ucGDSlave.txtType.Text = "5Inch";
                ucGDSlave.txtGridRows.Text = "10";
                ucGDSlave.txtGridColumns.Text = "6";
                ucGDSlave.txtFirmwareVersion.Text = Globals.FIRMWARE_VERSION; //Namrata: 24/05/2019
                ucGDSlave.cmbDebug.SelectedIndex = ucGDSlave.cmbDebug.FindStringExact("3"); //Namrata: 24/05/2019
                //Namrata: 25/11/2019
                ucGDSlave.txtEQS.Text = "500";
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
            if (Utils.IsEmptyFields(ucGDSlave.grpGraphicalDisplay))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return status;
        }
        private void refreshList()
        {
            string strRoutineName = "SMSSlaveGroup:refreshList";
            try
            {
                int cnt = 0;
                Utils.GraphicalDisplaySlaveList1.Clear();
                ucGDSlave.lvGraphicalDisplay.Items.Clear();

                if (ucGDSlave.lvGraphicalDisplay.InvokeRequired)
                {
                    ucGDSlave.lvGraphicalDisplay.Invoke(new MethodInvoker(delegate
                    {
                        foreach (GraphicalDisplaySlave sl in GraphicalDisplaySlaveList)
                        {
                            string[] row = new string[12];
                            if (sl.IsNodeComment)
                            {
                                row[0] = "Comment...";
                            }
                            else
                            {
                                row[0] = sl.SlaveNum;
                                row[1] = sl.Type;
                                row[2] = sl.GridRows;
                                row[3] = sl.GridColumns;
                                row[4] = sl.EventQSize;
                                row[5] = sl.DEBUG;
                                row[6] = sl.AppFirmwareVersion;
                                row[7] = sl.Run;
                            }
                            ListViewItem lvItem = new ListViewItem(row);
                            if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            ucGDSlave.lvGraphicalDisplay.Items.Add(lvItem);
                            //Get SlaveNo
                            GDSlave.GDSlaveNo = sl.SlaveNum;
                            Utils.GraphicalDisplaySlaveList.AddRange(GraphicalDisplaySlaveList);//Namrata:09/04/2019
                            Utils.GraphicalDisplaySlaveList1.AddRange(GraphicalDisplaySlaveList);//Namrata:09/04/2019
                        }
                    }));
                }
                else
                {
                    foreach (GraphicalDisplaySlave sl in GraphicalDisplaySlaveList)
                    {
                        string[] row = new string[12];
                        if (sl.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = sl.SlaveNum;
                            row[1] = sl.Type;
                            row[2] = sl.GridRows;
                            row[3] = sl.GridColumns;
                            row[4] = sl.EventQSize;
                            row[5] = sl.DEBUG;
                            row[6] = sl.AppFirmwareVersion;
                            row[7] = sl.Run;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucGDSlave.lvGraphicalDisplay.Items.Add(lvItem);
                        //Get SlaveNo
                        GDSlave.GDSlaveNo = sl.SlaveNum;
                        Utils.GraphicalDisplaySlaveList.AddRange(GraphicalDisplaySlaveList);//Namrata:09/04/2019
                        Utils.GraphicalDisplaySlaveList1.AddRange(GraphicalDisplaySlaveList);//Namrata:09/04/2019
                    }
                }




                //    foreach (GraphicalDisplaySlave sl in GraphicalDisplaySlaveList)
                //{
                //    string[] row = new string[12];
                //    if (sl.IsNodeComment)
                //    {
                //        row[0] = "Comment...";
                //    }
                //    else
                //    {
                //        row[0] = sl.SlaveNum;
                //        row[1] = sl.Type;
                //        row[2] = sl.GridRows;
                //        row[3] = sl.GridColumns;
                //        row[4] = sl.EventQSize;
                //        row[5] = sl.DEBUG;
                //        row[6] = sl.AppFirmwareVersion;
                //        row[7] = sl.Run;
                //    }
                //    ListViewItem lvItem = new ListViewItem(row);
                //    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                //    ucGDSlave.lvGraphicalDisplay.Items.Add(lvItem);
                //    //Get SlaveNo
                //    GDSlave.GDSlaveNo = sl.SlaveNum;
                //    Utils.GraphicalDisplaySlaveList.AddRange(GraphicalDisplaySlaveList);//Namrata:09/04/2019
                //    Utils.GraphicalDisplaySlaveList1.AddRange(GraphicalDisplaySlaveList);//Namrata:09/04/2019
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadValues()
        {
            string strRoutineName = "SMSSlaveGroup:loadValues";
            try
            {
                GraphicalDisplaySlave GraphicalDisplaySlave = GraphicalDisplaySlaveList.ElementAt(editIndex);
                if (GraphicalDisplaySlave != null)
                {
                    ucGDSlave.txtSlaveNum.Text = GraphicalDisplaySlave.SlaveNum;
                    ucGDSlave.txtType.Text = GraphicalDisplaySlave.Type;
                    ucGDSlave.txtGridRows.Text = GraphicalDisplaySlave.GridRows;
                    ucGDSlave.txtGridColumns.Text = GraphicalDisplaySlave.GridColumns;
                    ucGDSlave.cmbDebug.SelectedIndex = ucGDSlave.cmbDebug.FindStringExact(GraphicalDisplaySlave.DEBUG);
                    ucGDSlave.txtFirmwareVersion.Text = GraphicalDisplaySlave.AppFirmwareVersion;
                    if (GraphicalDisplaySlave.Run.ToLower() == "yes") ucGDSlave.chkRun.Checked = true;
                    else ucGDSlave.chkRun.Checked = false;
                    //Namrata: 25/11/2019
                    ucGDSlave.txtEQS.Text = GraphicalDisplaySlave.EventQSize;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void addListHeaders()
        {
            string strRoutineName = "SMSSlaveGroup:addListHeaders";
            try
            {
                ucGDSlave.lvGraphicalDisplay.Columns.Add("Slave No.", 70, HorizontalAlignment.Left);
                ucGDSlave.lvGraphicalDisplay.Columns.Add("Type", 70, HorizontalAlignment.Left);
                ucGDSlave.lvGraphicalDisplay.Columns.Add("Grid Rows", 80, HorizontalAlignment.Left);
                ucGDSlave.lvGraphicalDisplay.Columns.Add("Grid Columns", 120, HorizontalAlignment.Left);
                ucGDSlave.lvGraphicalDisplay.Columns.Add("Event Queue Size", 120, HorizontalAlignment.Left);
                ucGDSlave.lvGraphicalDisplay.Columns.Add("Debug Level", 100, HorizontalAlignment.Left);
                ucGDSlave.lvGraphicalDisplay.Columns.Add("Firmware Version", 100, HorizontalAlignment.Left);//Namrata: 24/05/2019
                ucGDSlave.lvGraphicalDisplay.Columns.Add("Run", 70, HorizontalAlignment.Left);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("GraphicalDisplaySlaveGroup_"))
            {
                return ucGDSlave;
            }
            kpArr.RemoveAt(0);

            if (kpArr.ElementAt(0).Contains("GraphicalDisplaySlave_"))
            {
                int idx = -1;
                string[] elems = kpArr.ElementAt(0).Split('_');
                int iIndex = 0;
                if (GraphicalDisplaySlaveList.Where(x => x.SlaveNum == GDSlave.GDSlaveNo).Select(x => x).Any())
                {
                    iIndex = GraphicalDisplaySlaveList.IndexOf(GraphicalDisplaySlaveList.Where(x => x.SlaveNum == GDSlave.GDSlaveNo).Select(x => x).FirstOrDefault());
                }
                //idx = Int32.Parse(elems[elems.Length - 1]);
                if (GraphicalDisplaySlaveList.Count <= 0) return null;
                return GraphicalDisplaySlaveList[iIndex].getView(kpArr);
            }
            else
            {
                Utils.WriteLine(VerboseLevel.WARNING, "***** View for element: {0} not supported!!!", kpArr.ElementAt(0));
            }
            return null;
        }
        public void parseIECGNode(XmlNode iecgNode, TreeNode tn)
        {
            string strRoutineName = "SMSSlaveGroup:parseIECGNode";
            try
            {
                rnName = iecgNode.Name;
                tn.Nodes.Clear();
                GraphicalDisplaySlaveGroupTreeNode = tn;
                foreach (XmlNode node in iecgNode)
                {
                    XmlElement root = node.ParentNode as XmlElement;
                    if (node.NodeType == XmlNodeType.Comment) continue;
                    TreeNode tmp = tn.Nodes.Add("GraphicalDisplaySlave_" + Utils.GenerateShortUniqueKey(), "GraphicalDisplaySlave", "GraphicalDisplaySlaveGroup", "GraphicalDisplaySlaveGroup");
                    GraphicalDisplaySlaveList.Add(new GraphicalDisplaySlave(node, tmp));
                }
                //Namrata: 25/11/2019
                //If "EventQSize" not exist in XML File
                for (int i = 0; i < GraphicalDisplaySlaveList.Count; i++)
                {
                    if ((GraphicalDisplaySlaveList[i].EventQSize == "")|| (GraphicalDisplaySlaveList[i].EventQSize == "0"))
                    {
                        GraphicalDisplaySlaveList[i].EventQSize = "500";
                        ucGDSlave.txtEQS.Text = "500";
                    } 
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
            foreach (GraphicalDisplaySlave sn in GraphicalDisplaySlaveList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(sn.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }
            return rootNode;
        }
        public int getCount()
        {
            int ctr = 0;
            foreach (GraphicalDisplaySlave SMSNode in GraphicalDisplaySlaveList)
            {
                if (SMSNode.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }
        public List<GraphicalDisplaySlave> getGDisplaySlaves()
        {
            return GraphicalDisplaySlaveList;
        }

    }
}
