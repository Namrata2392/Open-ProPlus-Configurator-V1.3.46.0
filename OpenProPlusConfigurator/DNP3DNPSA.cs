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

    public class DNP3DNPSA
    {
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        public TreeNode DNP3SlaveTreeNode; TreeNode DNP3SlaveTreeNode1 = new TreeNode();
           
        private bool isNodeComment = false;
        private string comment = ""; private int slaveno = -1;
        private SlaveTypes sType = SlaveTypes.DNP3SLAVE;
        List<DNPSA> DnpsaList = new List<DNPSA>();
        ucGroupDNPSA ucDNPSA = new ucGroupDNPSA();

        public DNP3DNPSA(TreeNode tn, SlaveTypes slType, int sNo)
        {
            string strRoutineName = "DNP3DNPSA:DNP3DNPSA";
            try
            {
                tn.Nodes.Clear();
                DNP3SlaveTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...
                sType = slType;
                slaveno = sNo;
                ucDNPSA.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                ucDNPSA.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                ucDNPSA.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucDNPSA.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucDNPSA.lvDNPSAListItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvDNPSAList_ItemCheck);
                ucDNPSA.lvDNPSAListDoubleClick += new System.EventHandler(this.lvDNPSAList_DoubleClick);
                addListHeaders();
                fillOptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DNP3DNPSA(SlaveTypes slType, int sNo)
        {
            string strRoutineName = "DNP3DNPSA";
            try
            {
                DNP3SlaveTreeNode1.Text = "DNPSA";
                DNP3SlaveTreeNode = DNP3SlaveTreeNode1;
                sType = slType;
                slaveno = sNo;
                ucDNPSA.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                ucDNPSA.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                ucDNPSA.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucDNPSA.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucDNPSA.lvDNPSAListItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvDNPSAList_ItemCheck);
                ucDNPSA.lvDNPSAListDoubleClick += new System.EventHandler(this.lvDNPSAList_DoubleClick);
                addListHeaders();
                fillOptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public DNP3DNPSA(string mbsName, List<KeyValuePair<string, string>> mbsData, TreeNodeCollection tn)
        {
            ucDNPSA.btnAddClick += new System.EventHandler(this.btnAdd_Click);
            ucDNPSA.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
            ucDNPSA.btnDoneClick += new System.EventHandler(this.btnDone_Click);
            ucDNPSA.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
            ucDNPSA.lvDNPSAListItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvDNPSAList_ItemCheck);
            ucDNPSA.lvDNPSAListDoubleClick += new System.EventHandler(this.lvDNPSAList_DoubleClick);
            addListHeaders();
            string strRoutineName = "DNP3DNPSA";
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
        public DNP3DNPSA(XmlNode sNode, TreeNode tn)
        {
            ucDNPSA.btnAddClick += new System.EventHandler(this.btnAdd_Click);
            ucDNPSA.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
            ucDNPSA.btnDoneClick += new System.EventHandler(this.btnDone_Click);
            ucDNPSA.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
            ucDNPSA.lvDNPSAListItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvDNPSAList_ItemCheck);
            ucDNPSA.lvDNPSAListDoubleClick += new System.EventHandler(this.lvDNPSAList_DoubleClick);
            string strRoutineName = "DNP3DNPSA";
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

        private void fillOptions()
        {
            string strRoutineName = "DNP3DNPSA:fillOptions";
            try
            {
                ucDNPSA.cmbHMACAlgo.Items.Clear();
                foreach (String ct in DNPSA.getHMACAlgo(SlaveTypes.DNP3SLAVE))
                {
                    ucDNPSA.cmbHMACAlgo.Items.Add(ct.ToString());
                }
                if (ucDNPSA.cmbHMACAlgo != null && ucDNPSA.cmbHMACAlgo.Items.Count > 0) { ucDNPSA.cmbHMACAlgo.SelectedIndex = 0; }


                ucDNPSA.CmbAEC.Items.Clear();
                foreach (String ct in DNPSA.getAuthErrorEventClass(SlaveTypes.DNP3SLAVE))
                {
                    ucDNPSA.CmbAEC.Items.Add(ct.ToString());
                }
                if (ucDNPSA.CmbAEC != null && ucDNPSA.CmbAEC.Items.Count > 0) { ucDNPSA.CmbAEC.SelectedIndex = 0; }
            }

            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void addListHeaders()
        {
            string strRoutineName = "DNP3DNPSA:addListHeaders";
            try
            {
                ucDNPSA.lvDNPSAList.Columns.Add("Aggressive Mode", 100, HorizontalAlignment.Left);
                ucDNPSA.lvDNPSAList.Columns.Add("Reply TOut", 150, HorizontalAlignment.Left);
                ucDNPSA.lvDNPSAList.Columns.Add("SSIVTime", 120, HorizontalAlignment.Left);
                ucDNPSA.lvDNPSAList.Columns.Add("SSIVCount", 170, HorizontalAlignment.Left);
                ucDNPSA.lvDNPSAList.Columns.Add("Max Auth Error Count", 230, HorizontalAlignment.Left);
                ucDNPSA.lvDNPSAList.Columns.Add("Auth Error Event", 230, HorizontalAlignment.Left);
                ucDNPSA.lvDNPSAList.Columns.Add("Auth Error Event Class", 230, HorizontalAlignment.Left);
                ucDNPSA.lvDNPSAList.Columns.Add("HMAC Algorithm", 230, HorizontalAlignment.Left);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void refreshList()
        {
            string strRoutineName = "DNP3DNPSA:refreshList";
            try
            {
                int cnt = 0;
                //Utils.GraphicalDisplaySlaveList1.Clear();
                ucDNPSA.lvDNPSAList.Items.Clear();
                foreach (DNPSA sl in DnpsaList)
                {
                    string[] row = new string[8];
                    if (sl.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = sl.AggressiveM;
                        row[1] = sl.ReplyTOut;
                        row[2] = sl.SSIVTime;
                        row[3] = sl.SSIVCount;
                        row[4] = sl.MaxAuthErrCount;
                        row[5] = sl.AuthErrEvent;
                        row[6] = sl.AuthErrEventClass;
                        row[7] = sl.HMACAlgo;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucDNPSA.lvDNPSAList.Items.Add(lvItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            string strRoutineName = "DNP3DNPSA:btnDone_Click";
            try
            {
                List<KeyValuePair<string, string>> mbsData = Utils.getKeyValueAttributes(ucDNPSA.grpDNPSA);
                if (mode == Mode.ADD)
                {
                    TreeNode tmp = null;
                   tmp = DNP3SlaveTreeNode.Nodes[3].Nodes.Add("User_" + Utils.GenerateShortUniqueKey(),"User", "UserConf", "UserConf");
                    
                    DnpsaList.Add(new DNPSA("User", mbsData,tmp, SlaveTypes.DNP3SLAVE,slaveno));
                    
                }
                else if (mode == Mode.EDIT)
                {
                    DnpsaList[editIndex].updateAttributes(mbsData);
                    //DNPObjectVarList[editIndex].updateAttributes(mbsData);
                }
                refreshList();
                if (sender != null && e != null)
                {
                    ucDNPSA.grpDNPSA.Visible = false;
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
            string strRoutineName = "DNP3DNPSA:btnCancel_Click";
            try
            {
                ucDNPSA.grpDNPSA.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                ucDNPSA.grpDNPSA.Visible = false;
                Utils.resetValues(ucDNPSA.grpDNPSA);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string strRoutineName = "DNP3DNPSA: btnAdd_Click";
            try
            {
                //{
                if (DnpsaList.Count >= Globals.MaxDNPSA)
                {
                    MessageBox.Show("Maximum " + Globals.MaxDNPSA + " DNPSA are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                mode = Mode.ADD;
                editIndex = -1;
                Utils.resetValues(ucDNPSA.grpDNPSA);
                Utils.showNavigation(ucDNPSA.grpDNPSA, false);
                loadDefaults();
                ucDNPSA.grpDNPSA.Visible = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadDefaults()
        {
            string strRoutineName = "DNP3DNPSA:loadDefaults";
            try
            {
                ucDNPSA.txtART.Text = "2000";
                ucDNPSA.txtKIT.Text = "3600";
                ucDNPSA.txtKIC.Text = "1000";
                ucDNPSA.txtAEC.Text = "2";
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strRoutineName = "DNP3DNPSA: btnDelete_Click";
            try
            {
                if (ucDNPSA.lvDNPSAList.Items.Count == 0)
                {
                    MessageBox.Show("Please add atleast one ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (ucDNPSA.lvDNPSAList.CheckedItems.Count == 1)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        int iIndex = ucDNPSA.lvDNPSAList.CheckedItems[0].Index;
                        if (result == DialogResult.Yes)
                        {
                            DNP3SlaveTreeNode.Nodes.Remove(DnpsaList.ElementAt(iIndex).getTreeNode());
                            DnpsaList.RemoveAt(iIndex);
                            ucDNPSA.lvDNPSAList.Items[iIndex].Remove();
                            //Globals.TotalSlavesCount -= 1; //Ajay: 25/08/2018
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select atleast one ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    refreshList();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
  
        private void lvDNPSAList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string strRoutineName = "DNP3DNPSA:lvDNPSAList_ItemCheck";
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    ucDNPSA.lvDNPSAList.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
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
            string strRoutineName = "DNP3DNPSA:loadValues";
            try
            {
                DNPSA domn = DnpsaList.ElementAt(editIndex);
                if (domn != null)
                {
                    ucDNPSA.txtART.Text = domn.ReplyTOut;
                    ucDNPSA.txtKIT.Text = domn.SSIVTime;
                    ucDNPSA.txtKIC.Text = domn.SSIVCount;
                    ucDNPSA.txtAEC.Text = domn.MaxAuthErrCount;
                    ucDNPSA.CmbAEC.SelectedIndex = ucDNPSA.CmbAEC.FindStringExact(domn.AuthErrEventClass);
                    ucDNPSA.cmbHMACAlgo.SelectedIndex = ucDNPSA.cmbHMACAlgo.FindStringExact(domn.HMACAlgo);
                    if (domn.AggressiveM.ToLower() == "yes")
                    { ucDNPSA.chkAggMode.Checked = true; }
                    else { ucDNPSA.chkAggMode.Checked = true; }

                    if (domn.AuthErrEvent.ToLower() == "yes")
                    { ucDNPSA.ChkAEE.Checked = true; }
                    else { ucDNPSA.ChkAEE.Checked = false; }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvDNPSAList_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "DNP3DNPSA:lvDNPSAList_DoubleClick";
            try
            {

                if (ucDNPSA.lvDNPSAList.SelectedItems.Count <= 0) return;
                ListViewItem lvi = ucDNPSA.lvDNPSAList.SelectedItems[0];
                Utils.UncheckOthers(ucDNPSA.lvDNPSAList, lvi.Index);
                if (DnpsaList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ucDNPSA.grpDNPSA.Visible = true;
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(ucDNPSA.grpDNPSA, true);
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
            rootNode = xmlDoc.CreateElement("DNPSA");
            xmlDoc.AppendChild(rootNode);

            foreach (DNPSA ai in DnpsaList)
            {
                XmlAttribute attr1 = xmlDoc.CreateAttribute("AggressiveM");
                attr1.Value = ai.AggressiveM.ToString();
                rootNode.Attributes.Append(attr1);

                XmlAttribute attr2 = xmlDoc.CreateAttribute("ReplyTOut");
                attr2.Value = ai.ReplyTOut.ToString();
                rootNode.Attributes.Append(attr2);

                XmlAttribute attrPrivateKey = xmlDoc.CreateAttribute("SSIVTime");
                attrPrivateKey.Value = ai.SSIVTime.ToString();
                rootNode.Attributes.Append(attrPrivateKey);

                XmlAttribute attrCA = xmlDoc.CreateAttribute("SSIVCount");
                attrCA.Value = ai.SSIVCount.ToString();
                rootNode.Attributes.Append(attrCA);

                XmlAttribute att2DHParameter = xmlDoc.CreateAttribute("MaxAuthErrCount");
                att2DHParameter.Value = ai.MaxAuthErrCount.ToString();
                rootNode.Attributes.Append(att2DHParameter);

                XmlAttribute attrAuthErrEvent = xmlDoc.CreateAttribute("AuthErrEvent");
                attrAuthErrEvent.Value = ai.AuthErrEvent.ToString();
                rootNode.Attributes.Append(attrAuthErrEvent);

                XmlAttribute att2AuthErrEventClass = xmlDoc.CreateAttribute("AuthErrEventClass");
                att2AuthErrEventClass.Value = ai.AuthErrEventClass.ToString();
                rootNode.Attributes.Append(att2AuthErrEventClass);

                XmlAttribute att2HMACAlgo = xmlDoc.CreateAttribute("HMACAlgo");
                att2HMACAlgo.Value = ai.HMACAlgo.ToString();
                rootNode.Attributes.Append(att2HMACAlgo);

                foreach (DNPSA dnpsa in DnpsaList)
                {
                    XmlNode importNode = rootNode.OwnerDocument.ImportNode(dnpsa.exportXMLnode(), true);
                    rootNode.AppendChild(importNode);
                }

            }



            //foreach (DNPSA dnpsa in DnpsaList)
            //{
            //    XmlNode importNode = rootNode.OwnerDocument.ImportNode(dnpsa.exportXMLnode(), true);
            //    rootNode.AppendChild(importNode);
            //}

            //XmlNode SLDNode = xmlDoc.CreateElement("SLD");// Create a new child node
            //rootNode.AppendChild(SLDNode); //add the child node to the root node
            //foreach (SLDSettings smsuser in SLDSettingsList)
            //{
            //    XmlNode importNode = rootNode.OwnerDocument.ImportNode(smsuser.exportXMLnode(), true);
            //    SLDNode.AppendChild(importNode);
            //}
            return rootNode;
        }
        private string rnName = "";

        public void parseOVNode1(XmlNode aicNode, bool imported)
        {
            string strRoutineName = "DNP3DNPSA: parseOVNode";
            try
            {
                if (aicNode == null)
                {
                    rnName = "DNPSA";
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
                    if (node.Name == "DNPSA")
                    {
                        //if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                        //TreeNode tmp = tn.Nodes.Add("DNP3Slave_" + Utils.GenerateShortUniqueKey(), "DNP3Settings", "DNP3Slave", "DNP3Slave");
                        ////TreeNode tmp = aicNode.ChildNodes[3].n.Add("User_" + Utils.GenerateShortUniqueKey(), "User", "User", "User");
                        //DnpsaList.Add(new DNPSA(node, SlaveTypes.DNP3SLAVE, slaveno, false,tmp));
                    }
                }
                refreshList();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void parseOVNode(XmlNode aicNode, TreeNode tn)
        {
            string strRoutineName = "DNP3DNPSA: parseOVNode";
            try
            {
                if (aicNode == null)
                {
                    rnName = "DNPSA";
                    return;
                }
                //First set root node name...
                rnName = aicNode.Name;
                //tn.Nodes.Clear();
                DNP3SlaveTreeNode = tn;
                if (aicNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = aicNode.Value;
                }
                foreach (XmlNode node in aicNode)
                {
                    if (node.Name == "DNPSA")
                    {
                        if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                        TreeNode tmp = tn.Nodes[3].Nodes.Add("User_" + Utils.GenerateShortUniqueKey(), "User", "UserConf", "UserConf");
                        DnpsaList.Add(new DNPSA(node, SlaveTypes.DNP3SLAVE, slaveno, false, tmp));
                        
                    }
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
            int ctr = 0;
            foreach (DNPSA URNode in DnpsaList)
            {
                if (URNode.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }
       

        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("DNPSA_"))
            {
                return ucDNPSA;
            }
            kpArr.RemoveAt(0);

            if (kpArr.ElementAt(0).Contains("User_"))
            {
                int idx = -1;
                string[] elems = kpArr.ElementAt(0).Split('_');
                int iIndex = 0;
              
                //idx = Int32.Parse(elems[elems.Length - 1]);
                if (DnpsaList.Count <= 0) return null;
                return DnpsaList[iIndex].getView(kpArr);
            }
            else
            {
                Utils.WriteLine(VerboseLevel.WARNING, "***** View for element: {0} not supported!!!", kpArr.ElementAt(0));
            }
            return null;
        }



        public Control getView1(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("DNPSA_"))
            {
                return ucDNPSA;
            }
            return null;
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

