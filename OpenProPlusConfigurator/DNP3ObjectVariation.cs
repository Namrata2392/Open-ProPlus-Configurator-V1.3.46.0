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
   
    public class DNP3ObjectVariation
    {
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
        List<ObjectValidation> OVList = new List<ObjectValidation>();
        ucGroupObjectValidation ucOV = new ucGroupObjectValidation();

        public DNP3ObjectVariation(SlaveTypes slType, int sNo)
        {
            string strRoutineName = "DNP3ObjectVariation";
            try
            {
                sType = slType;
                slaveno = sNo;
                ucOV.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                ucOV.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                ucOV.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucOV.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucOV.lvObjectValidItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvObjectValid_ItemCheck);
                ucOV.lvObjectValidDoubleClick += new System.EventHandler(this.lvObjectValid_DoubleClick);
                addListHeaders();
                //fillOptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DNP3ObjectVariation(string mbsName, List<KeyValuePair<string, string>> mbsData, TreeNodeCollection tn)
        {
            ucOV.btnAddClick += new System.EventHandler(this.btnAdd_Click);
            ucOV.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
            ucOV.btnDoneClick += new System.EventHandler(this.btnDone_Click);
            ucOV.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
            ucOV.lvObjectValidItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvObjectValid_ItemCheck);
            ucOV.lvObjectValidDoubleClick += new System.EventHandler(this.lvObjectValid_DoubleClick);
            addListHeaders();
            string strRoutineName = "DNP3ObjectVariation";
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
        public DNP3ObjectVariation(XmlNode sNode, TreeNode tn)
        {
            ucOV.btnAddClick += new System.EventHandler(this.btnAdd_Click);
            ucOV.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
            ucOV.btnDoneClick += new System.EventHandler(this.btnDone_Click);
            ucOV.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
            ucOV.lvObjectValidItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvObjectValid_ItemCheck);
            ucOV.lvObjectValidDoubleClick += new System.EventHandler(this.lvObjectValid_DoubleClick);
            addListHeaders();
            string strRoutineName = "DNP3ObjectVariation";
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
            string strRoutineName = "DNP3ObjectVariation:addListHeaders";
            try
            {
                ucOV.lvObjectValid.Columns.Add("Binary Input", 100, HorizontalAlignment.Left);
                ucOV.lvObjectValid.Columns.Add("Binary Input Event", 150, HorizontalAlignment.Left);
                ucOV.lvObjectValid.Columns.Add("Double Input", 120, HorizontalAlignment.Left);
                ucOV.lvObjectValid.Columns.Add("Double Input Event", 170, HorizontalAlignment.Left);
                ucOV.lvObjectValid.Columns.Add("Binary Output Status", 170, HorizontalAlignment.Left);
                ucOV.lvObjectValid.Columns.Add("Binary Counter", 120, HorizontalAlignment.Left);
                ucOV.lvObjectValid.Columns.Add("Binary Counter Events", 120, HorizontalAlignment.Left);
                ucOV.lvObjectValid.Columns.Add("Frozen Counter", 100, HorizontalAlignment.Left);
                ucOV.lvObjectValid.Columns.Add("Frozen Counter Events", 170, HorizontalAlignment.Left);
                ucOV.lvObjectValid.Columns.Add("Analog Input", 150, HorizontalAlignment.Left);
                ucOV.lvObjectValid.Columns.Add("Analog Input Events", 180, HorizontalAlignment.Left);
                ucOV.lvObjectValid.Columns.Add("Analog Output Status", 180, HorizontalAlignment.Left);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void refreshList()
        {
            string strRoutineName = "DNP3ObjectVariation:refreshList";
            try
            {
                int cnt = 0;
                //Utils.GraphicalDisplaySlaveList1.Clear();
                ucOV.lvObjectValid.Items.Clear();
                foreach (ObjectValidation sl in OVList)
                {
                    string[] row = new string[12];
                    if (sl.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = sl.BI;
                        row[1] = sl.BIE;
                        row[2] = sl.DI;
                        row[3] = sl.DIE;
                        row[4] = sl.BOS;
                        row[5] = sl.BC;
                        row[6] = sl.BCE;
                        row[7] = sl.FC;
                        row[8] = sl.FCE;
                        row[9] = sl.AI;
                        row[10] = sl.AIE;
                        row[11] = sl.AOS;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucOV.lvObjectValid.Items.Add(lvItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            string strRoutineName = "DNP3ObjectVariation:btnDone_Click";
            try
            {
                List<KeyValuePair<string, string>> mbsData = Utils.getKeyValueAttributes(ucOV.grpObjectValid);
                if (mode == Mode.ADD)
                {
                    TreeNode tmp = null;
                    OVList.Add(new ObjectValidation("ObjectVariation", mbsData, null, SlaveTypes.DNP3SLAVE, slaveno));
                    //DNPObjectVarList.Add(new DNP3ObjectVariation("DNP3ObjectVariation", mbsData, tmp, Int32.Parse(SlaveNum)));
                }
                else if (mode == Mode.EDIT)
                {
                    OVList[editIndex].updateAttributes(mbsData);
                    //DNPObjectVarList[editIndex].updateAttributes(mbsData);
                }
                refreshList();
                if (sender != null && e != null)
                {
                    ucOV.grpObjectValid.Visible = false;
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
            string strRoutineName = "DNP3ObjectVariation:btnCancel_Click";
            try
            {
                ucOV.grpObjectValid.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                ucOV.grpObjectValid.Visible = false;
                Utils.resetValues(ucOV.grpObjectValid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string strRoutineName = "DNP3ObjectVariation: btnAdd_Click";
            try
            {
                //{
                if (OVList.Count >= Globals.MaxObjectVariation)
                {
                    MessageBox.Show("Maximum " + Globals.MaxObjectVariation + " Object Variation are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                mode = Mode.ADD;
                editIndex = -1;
                Utils.resetValues(ucOV.grpObjectValid);
                Utils.showNavigation(ucOV.grpObjectValid, false);
                loadDefaults();
                ucOV.grpObjectValid.Visible = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadDefaults()
        {
            string strRoutineName = "DNP3ObjectVariation:loadDefaults";
            try
            {
                ucOV.txtBI.Text = "1";
                ucOV.txtBIE.Text = "2";
                ucOV.txtDI.Text = "1";
                ucOV.txtDIE.Text = "2";
                ucOV.txtBOS.Text = "2";
                ucOV.txtBC.Text = "5";
                ucOV.txtBCE.Text = "1";
                ucOV.txtFC.Text = "9";
                ucOV.txtFCE.Text = "1";
                ucOV.txtAI.Text = "3";
                ucOV.txtAIE.Text = "3";
                ucOV.txtAOS.Text = "2";
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strRoutineName = "DNP3ObjectVariation: btnDelete_Click";
            try
            {
                if (ucOV.lvObjectValid.Items.Count == 0)
                {
                    MessageBox.Show("Please add atleast one Object Variation ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (ucOV.lvObjectValid.CheckedItems.Count == 1)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete Object Variation?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        int iIndex = ucOV.lvObjectValid.CheckedItems[0].Index;
                        if (result == DialogResult.Yes)
                        {
                            //DNP3SlaveTreeNode.Nodes.Remove(DNPObjectVarList.ElementAt(iIndex).getTreeNode());
                            OVList.RemoveAt(iIndex);
                            ucOV.lvObjectValid.Items[iIndex].Remove();
                            //Globals.TotalSlavesCount -= 1; //Ajay: 25/08/2018
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select atleast one Object Variation ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    refreshList();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvObjectValid_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string strRoutineName = "DNP3ObjectVariation:lvObjectValid_ItemCheck";
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    ucOV.lvObjectValid.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
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
            string strRoutineName = "DNP3ObjectVariation:loadValues";
            try
            {
                ObjectValidation ov = OVList.ElementAt(editIndex);
                if (ov != null)
                {
                    ucOV.txtBI.Text = ov.BI; 
                    ucOV.txtBIE.Text = ov.BIE;
                    ucOV.txtDI.Text = ov.DI;
                    ucOV.txtDIE.Text = ov.DIE;
                    ucOV.txtBOS.Text = ov.BOS;
                    ucOV.txtBC.Text = ov.BC;
                    ucOV.txtBCE.Text = ov.BCE;
                    ucOV.txtFC.Text = ov.FC;
                    ucOV.txtFCE.Text = ov.FCE;
                    ucOV.txtAI.Text = ov.AI;
                    ucOV.txtAIE.Text = ov.AIE;
                    ucOV.txtAOS.Text = ov.AOS;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvObjectValid_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "DNP3ObjectVariation:lvObjectValid_DoubleClick";
            try
            {

                if (ucOV.lvObjectValid.SelectedItems.Count <= 0) return;
                ListViewItem lvi = ucOV.lvObjectValid.SelectedItems[0];
                Utils.UncheckOthers(ucOV.lvObjectValid, lvi.Index);
                if (OVList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ucOV.grpObjectValid.Visible = true;
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(ucOV.grpObjectValid, true);
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
            rootNode = xmlDoc.CreateElement("ObjectVariation");
            xmlDoc.AppendChild(rootNode);
            foreach (ObjectValidation ai in OVList)
            {
                XmlAttribute attr1 = xmlDoc.CreateAttribute("BI");
                attr1.Value = ai.BI.ToString();
                rootNode.Attributes.Append(attr1);

                XmlAttribute attr2 = xmlDoc.CreateAttribute("BIE");
                attr2.Value = ai.BIE.ToString();
                rootNode.Attributes.Append(attr2);

                XmlAttribute attrDI = xmlDoc.CreateAttribute("DI");
                attrDI.Value = ai.DI.ToString();
                rootNode.Attributes.Append(attrDI);

                XmlAttribute attrDIE = xmlDoc.CreateAttribute("DIE");
                attrDIE.Value = ai.DIE.ToString();
                rootNode.Attributes.Append(attrDIE);

                XmlAttribute att2BOS = xmlDoc.CreateAttribute("BOS");
                att2BOS.Value = ai.BOS.ToString();
                rootNode.Attributes.Append(att2BOS);

                XmlAttribute attrBC = xmlDoc.CreateAttribute("BC");
                attrBC.Value = ai.BC.ToString();
                rootNode.Attributes.Append(attrBC);

                XmlAttribute attrBCE = xmlDoc.CreateAttribute("BCE");
                attrBCE.Value = ai.BCE.ToString();
                rootNode.Attributes.Append(attrBCE);

                XmlAttribute attrFC = xmlDoc.CreateAttribute("FC");
                attrFC.Value = ai.FC.ToString();
                rootNode.Attributes.Append(attrFC);

                XmlAttribute attrFCE = xmlDoc.CreateAttribute("FCE");
                attrFCE.Value = ai.FCE.ToString();
                rootNode.Attributes.Append(attrFCE);

                XmlAttribute att2AI = xmlDoc.CreateAttribute("AI");
                att2AI.Value = ai.AI.ToString();
                rootNode.Attributes.Append(att2AI);

                XmlAttribute attrAIE = xmlDoc.CreateAttribute("AIE");
                attrAIE.Value = ai.AIE.ToString();
                rootNode.Attributes.Append(attrAIE);

                XmlAttribute attrAOS = xmlDoc.CreateAttribute("AOS");
                attrAOS.Value = ai.AOS.ToString();
                rootNode.Attributes.Append(attrAOS);

            }
            return rootNode;
        }
        private string rnName = "";
        public void parseOVNode(XmlNode aicNode, bool imported)
        {
            string strRoutineName = "DNP3ObjectVariation: parseOVNode";
            try
            {
                if (aicNode == null)
                {
                    rnName = "ObjectVariation";
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
                    if (node.ChildNodes[1].Attributes.Count > 0)
                    {
                        if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                        //URList.Add(new UnsolResponse(node.ChildNodes[0], SlaveTypes.DNP3SLAVE, slaveno, false));
                        OVList.Add(new ObjectValidation(node.ChildNodes[1], SlaveTypes.DNP3SLAVE, slaveno, false));
                    }
                    //if (node.Name == "ObjectVariation")
                    //{
                    //    if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                    //    OVList.Add(new ObjectValidation(node, SlaveTypes.DNP3SLAVE, slaveno, false));
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
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("ObjectVariation_"))
            {
                return ucOV;
            }
            return null;
        }
        public int getCount()
        {
            int ctr = 0;
            foreach (ObjectValidation URNode in OVList)
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
