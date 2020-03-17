//Created By: Namrata Chaudhari
//Date: 06/11/2019
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
using System.ComponentModel;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections;
using System.Drawing.Imaging;
using System.IO.Compression;
using OpenProPlusConfigurator.Properties;
using System.Globalization;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Security.AccessControl;

namespace OpenProPlusConfigurator
{
    public class DNP3UnsolResponse
    {
        enum slaveType
        {
            DNP3Slave
        };
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        public TreeNode DNP3SlaveTreeNode;
        private bool isNodeComment = false;
        private string comment = ""; private int slaveno = -1;
        private SlaveTypes sType = SlaveTypes.DNP3SLAVE;
        public static string[] arrAttributes = new string[] { "DestAdd", "C1MaxEvents", "C1MaxDelay", "C2MaxEvents", "C2MaxDelay", "C3MaxEvents", "C3MaxDelay", "MaxRetry", "RetryDelay", "OfflineRetryDelay", "AllowNull" };
        List<UnsolResponse> URList = new List<UnsolResponse>();
        ucGroupUnsolicited ucUR = new ucGroupUnsolicited();

        public DNP3UnsolResponse(SlaveTypes slType, int sNo)
        {
            string strRoutineName = "DNP3UnsolResponse";
            try
            {
                sType = slType;
                slaveno = sNo;
                ucUR.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                ucUR.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                ucUR.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucUR.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucUR.lvUnsolicitedListItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvUnsolicitedList_ItemCheck);
                ucUR.lvUnsolicitedListDoubleClick += new System.EventHandler(this.lvUnsolicitedList_DoubleClick);
                addListHeaders();
                //fillOptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DNP3UnsolResponse(string mbsName, List<KeyValuePair<string, string>> mbsData, TreeNodeCollection tn)
        {
            ucUR.btnAddClick += new System.EventHandler(this.btnAdd_Click);
            ucUR.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
            ucUR.btnDoneClick += new System.EventHandler(this.btnDone_Click);
            ucUR.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
            ucUR.lvUnsolicitedListItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvUnsolicitedList_ItemCheck);
            ucUR.lvUnsolicitedListDoubleClick += new System.EventHandler(this.lvUnsolicitedList_DoubleClick);
            addListHeaders();
            string strRoutineName = "DNP3UnsolResponse";
            try
            {
                //this.fillOptions();
                try
                {
                    //sType = (slaveType)Enum.Parse(typeof(slaveType), mbsName);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", mbsName);
                }
                if (mbsData != null && mbsData.Count > 0)
                {
                    foreach (KeyValuePair<string, string> mbskp in mbsData)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", mbskp.Key, mbskp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(mbskp.Key) != null) //Ajay: 03/07/2018
                            {
                                this.GetType().GetProperty(mbskp.Key).SetValue(this, mbskp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value:{1}", mbskp.Key, mbskp.Value);
                        }
                    }

                    // if (tn != null) tn.noClear();


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public DNP3UnsolResponse(XmlNode sNode, TreeNode tn)
        {
            ucUR.btnAddClick += new System.EventHandler(this.btnAdd_Click);
            ucUR.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
            ucUR.btnDoneClick += new System.EventHandler(this.btnDone_Click);
            ucUR.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
            ucUR.lvUnsolicitedListItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvUnsolicitedList_ItemCheck);
            ucUR.lvUnsolicitedListDoubleClick += new System.EventHandler(this.lvUnsolicitedList_DoubleClick);
            addListHeaders();
            string strRoutineName = "DNP3UnsolResponse";
            try
            {
                if (sNode.Attributes != null)
                {
                    try
                    {
                        //sType = (slaveType)Enum.Parse(typeof(slaveType), sNode.Name);
                    }
                    catch (System.ArgumentException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", sNode.Name);
                    }

                    foreach (XmlAttribute item in sNode.Attributes)
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
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value:{1}", item.Name, item.Value);
                        }
                    }
                    Utils.Write(VerboseLevel.DEBUG, "\n");
                    if (tn != null) tn.Nodes.Clear();
                    DNP3SlaveTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...   
                    //if (tn != null) tn.Text = "UnsolicitedResponse " + "DNP3Slave_" + this.SlaveNum;
                    //parseIECGNode(sNode, tn);
                }
                else if (sNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = sNode.Value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addListHeaders()
        {
            string strRoutineName = "DNP3UnsolResponse:addListHeaders";
            try
            {
                ucUR.lvUnsolicitedList.Columns.Add("Destination Address", 150, HorizontalAlignment.Left);
                ucUR.lvUnsolicitedList.Columns.Add("C1 Max Events", 70, HorizontalAlignment.Left);
                ucUR.lvUnsolicitedList.Columns.Add("C1 Max Delay", 80, HorizontalAlignment.Left);
                ucUR.lvUnsolicitedList.Columns.Add("C2 Max Events", 120, HorizontalAlignment.Left);
                ucUR.lvUnsolicitedList.Columns.Add("C2 Max Delay", 120, HorizontalAlignment.Left);
                ucUR.lvUnsolicitedList.Columns.Add("C3 Max Events", 120, HorizontalAlignment.Left);
                ucUR.lvUnsolicitedList.Columns.Add("C3 Max Delay", 120, HorizontalAlignment.Left);
                ucUR.lvUnsolicitedList.Columns.Add("Max Retry", 100, HorizontalAlignment.Left);
                ucUR.lvUnsolicitedList.Columns.Add("Retry Delay", 100, HorizontalAlignment.Left);//Namrata: 24/05/2019
                ucUR.lvUnsolicitedList.Columns.Add("Offline Retry Delay", 170, HorizontalAlignment.Left);
                ucUR.lvUnsolicitedList.Columns.Add("Allow Null", 120, HorizontalAlignment.Left);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void refreshList()
        {
            string strRoutineName = "DNP3UnsolResponse:refreshList";
            try
            {
                int cnt = 0;
                //Utils.GraphicalDisplaySlaveList1.Clear();
                ucUR.lvUnsolicitedList.Items.Clear();
                foreach (UnsolResponse sl in URList)
                {
                    string[] row = new string[11];
                    if (sl.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = sl.DestAdd;
                        row[1] = sl.C1MaxEvents;
                        row[2] = sl.C1MaxDelay;
                        row[3] = sl.C2MaxEvents;
                        row[4] = sl.C2MaxDelay;
                        row[5] = sl.C3MaxEvents;
                        row[6] = sl.C3MaxDelay;
                        row[7] = sl.MaxRetry;
                        row[8] = sl.RetryDelay;
                        row[9] = sl.OfflineRetryDelay;
                        row[10] = sl.AllowNull;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucUR.lvUnsolicitedList.Items.Add(lvItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            string strRoutineName = "DNP3UnsolResponse:btnDone_Click";
            try
            {
                List<KeyValuePair<string, string>> mbsData = Utils.getKeyValueAttributes(ucUR.grpUnsolicited);
                if (mode == Mode.ADD)
                {
                    TreeNode tmp = null;
                    URList.Add(new UnsolResponse("UnSolicitedResponse", mbsData, null, SlaveTypes.DNP3SLAVE, slaveno));
                    //DNPObjectVarList.Add(new DNP3ObjectVariation("DNP3ObjectVariation", mbsData, tmp, Int32.Parse(SlaveNum)));
                }
                else if (mode == Mode.EDIT)
                {
                    URList[editIndex].updateAttributes(mbsData);
                    //DNPObjectVarList[editIndex].updateAttributes(mbsData);
                }
                refreshList();
                if (sender != null && e != null)
                {
                    ucUR.grpUnsolicited.Visible = false;
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
            string strRoutineName = "DNP3UnsolResponse:btnCancel_Click";
            try
            {
                ucUR.grpUnsolicited.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                ucUR.grpUnsolicited.Visible = false;
                Utils.resetValues(ucUR.grpUnsolicited);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string strRoutineName = "DNP3UnsolResponse: btnAdd_Click";
            try
            {
                //{
                if (URList.Count >= Globals.MaxUnsolResponse)
                {
                    MessageBox.Show("Maximum " + Globals.MaxUnsolResponse + " Unsolicited Response are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                mode = Mode.ADD;
                editIndex = -1;
                Utils.resetValues(ucUR.grpUnsolicited);
                Utils.showNavigation(ucUR.grpUnsolicited, false);
                loadDefaults();
                ucUR.grpUnsolicited.Visible = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadDefaults()
        {
            string strRoutineName = "DNP3UnsolResponse:loadDefaults";
            try
            {
                ucUR.txtDestAdd.Text = "1";
                ucUR.txtC1ME.Text = "1";
                ucUR.txtC1MD.Text = "500";
                ucUR.txtC2ME.Text = "1";
                ucUR.txtC2MD.Text = "500";
                ucUR.txtC3ME.Text = "1";
                ucUR.txtC3MD.Text = "500";
                ucUR.txtMR.Text = "3";
                ucUR.txtRD.Text = "2000";
                ucUR.txtORD.Text = "3000";
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strRoutineName = "DNP3UnsolResponse: btnDelete_Click";
            try
            {
                if (ucUR.lvUnsolicitedList.Items.Count == 0)
                {
                    MessageBox.Show("Please add atleast one Unsolicited Response ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (ucUR.lvUnsolicitedList.CheckedItems.Count == 1)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete Unsolicited Response?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        int iIndex = ucUR.lvUnsolicitedList.CheckedItems[0].Index;
                        if (result == DialogResult.Yes)
                        {
                            //DNP3SlaveTreeNode.Nodes.Remove(DNPObjectVarList.ElementAt(iIndex).getTreeNode());
                            URList.RemoveAt(iIndex);
                            ucUR.lvUnsolicitedList.Items[iIndex].Remove();
                            //Globals.TotalSlavesCount -= 1; //Ajay: 25/08/2018
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select atleast Unsolicited Response ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    refreshList();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvUnsolicitedList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string strRoutineName = "DNP3UnsolResponse:lvUnsolicitedList_ItemCheck";
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    ucUR.lvUnsolicitedList.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
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
        private void loadValues()
        {
            string strRoutineName = "DNP3UnsolResponse:loadValues";
            try
            {
                UnsolResponse UR = URList.ElementAt(editIndex);
                if (UR != null)
                {
                    ucUR.txtDestAdd.Text = UR.DestAdd;
                    ucUR.txtC1ME.Text = UR.C1MaxEvents;
                    ucUR.txtC1MD.Text = UR.C1MaxDelay;
                    ucUR.txtC2ME.Text = UR.C2MaxEvents;
                    ucUR.txtC2MD.Text = UR.C2MaxDelay;
                    ucUR.txtC3ME.Text = UR.C3MaxEvents;
                    ucUR.txtC3MD.Text = UR.C3MaxEvents;
                    ucUR.txtMR.Text = UR.MaxRetry;
                    ucUR.txtRD.Text = UR.RetryDelay;
                    ucUR.txtORD.Text = UR.OfflineRetryDelay;

                    if (UR.AllowNull.ToLower() == "enable") ucUR.checkBox3.Checked = true;
                    else ucUR.checkBox3.Checked = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvUnsolicitedList_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "DNP3UnsolResponse:lvUnsolicitedList_DoubleClick";
            try
            {

                if (ucUR.lvUnsolicitedList.SelectedItems.Count <= 0) return;
                ListViewItem lvi = ucUR.lvUnsolicitedList.SelectedItems[0];
                Utils.UncheckOthers(ucUR.lvUnsolicitedList, lvi.Index);
                if (URList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ucUR.grpUnsolicited.Visible = true;
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(ucUR.grpUnsolicited, true);
                loadValues();
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
            XmlNode rootNode = null;
            if (isNodeComment)
            {
                rootNode = xmlDoc.CreateComment(comment);
                xmlDoc.AppendChild(rootNode);
                return rootNode;
            }
            rootNode = xmlDoc.CreateElement("UnSolicitedResponse");//rootNode = xmlDoc.CreateElement(rnName);
            xmlDoc.AppendChild(rootNode);

            #region URList
            foreach (UnsolResponse ai in URList)
            {
                XmlAttribute attr1 = xmlDoc.CreateAttribute("DestAdd");
                attr1.Value = ai.DestAdd.ToString();
                rootNode.Attributes.Append(attr1);

                XmlAttribute attr2 = xmlDoc.CreateAttribute("C1MaxEvents");
                attr2.Value = ai.C1MaxEvents.ToString();
                rootNode.Attributes.Append(attr2);

                XmlAttribute attrC1MaxDelay = xmlDoc.CreateAttribute("C1MaxDelay");
                attrC1MaxDelay.Value = ai.C1MaxDelay.ToString();
                rootNode.Attributes.Append(attrC1MaxDelay);

                XmlAttribute attrC2MaxEvents = xmlDoc.CreateAttribute("C2MaxEvents");
                attrC2MaxEvents.Value = ai.C2MaxEvents.ToString();
                rootNode.Attributes.Append(attrC2MaxEvents);

                XmlAttribute attrC2MaxDelay = xmlDoc.CreateAttribute("C2MaxDelay");
                attrC2MaxDelay.Value = ai.C2MaxDelay.ToString();
                rootNode.Attributes.Append(attrC2MaxDelay);

                XmlAttribute attrC3MaxEvents = xmlDoc.CreateAttribute("C3MaxEvents");
                attrC3MaxEvents.Value = ai.C3MaxEvents.ToString();
                rootNode.Attributes.Append(attrC3MaxEvents);

                XmlAttribute attrC3MaxDelay = xmlDoc.CreateAttribute("C3MaxDelay");
                attrC3MaxDelay.Value = ai.C3MaxDelay.ToString();
                rootNode.Attributes.Append(attrC3MaxDelay);

                XmlAttribute attrMaxRetry = xmlDoc.CreateAttribute("MaxRetry");
                attrMaxRetry.Value = ai.MaxRetry.ToString();
                rootNode.Attributes.Append(attrMaxRetry);

                XmlAttribute attrRetryDelay = xmlDoc.CreateAttribute("RetryDelay");
                attrRetryDelay.Value = ai.RetryDelay.ToString();
                rootNode.Attributes.Append(attrRetryDelay);

                XmlAttribute attrOfflineRetryDelay = xmlDoc.CreateAttribute("OfflineRetryDelay");
                attrOfflineRetryDelay.Value = ai.OfflineRetryDelay.ToString();
                rootNode.Attributes.Append(attrOfflineRetryDelay);

                XmlAttribute attrAllowNull = xmlDoc.CreateAttribute("AllowNull");
                attrAllowNull.Value = ai.AllowNull.ToString();
                rootNode.Attributes.Append(attrAllowNull);
            }
            #endregion URList
            return rootNode;
        }
        private string rnName = "";
        public void parseURNode(XmlNode aicNode, bool imported)
        {
            string strRoutineName = "DNP3UnsolResponse: parseURNode";
            try
            {
                if (aicNode == null)
                {
                    rnName = "UnsolicitedResponse";
                    return;
                }
                //First set root node name...
                rnName = aicNode.Name;
                if (aicNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = aicNode.Value;
                }
                foreach (XmlNode node in aicNode)
                {
                    if(node.ChildNodes[0].Attributes.Count > 0)
                    {
                        if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                        //URList.Add(new UnsolResponse(node.ChildNodes[0], SlaveTypes.DNP3SLAVE, slaveno, false));
                        URList.Add(new UnsolResponse(node.ChildNodes[0], SlaveTypes.DNP3SLAVE, slaveno, false));
                    }
                    //if (node.Name == "UnSolicitedResponse")
                    ////if (node.ChildNodes[0].Name == "UnSolicitedResponse")
                    //{
                    //    if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                    //    //URList.Add(new UnsolResponse(node.ChildNodes[0], SlaveTypes.DNP3SLAVE, slaveno, false));
                    //    URList.Add(new UnsolResponse(node, SlaveTypes.DNP3SLAVE, slaveno, false));
                    //}
                }
                refreshList();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("UnsolicitedResponse_"))
            {
                return ucUR;
            }
            return null;
        }
        public int getCount()
        {
            int ctr = 0;
            foreach (UnsolResponse URNode in URList)
            {
                if (URNode.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }
        public TreeNode getTreeNode()
        {
            return DNP3SlaveTreeNode;
        }
        //public string getSlaveID
        //{
        //   // get { return "DNP3Slave_" + SlaveNum; }
        //}
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }

    }

}

