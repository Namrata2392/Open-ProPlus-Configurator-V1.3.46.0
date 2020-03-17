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

    public class DNP3Encryption
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
        List<Encryption> EncryptionList = new List<Encryption>();
        ucGroupEncryption ucENC = new ucGroupEncryption();

        public DNP3Encryption(SlaveTypes slType, int sNo)
        {
            string strRoutineName = "DNP3Encryption";
            try
            {
                sType = slType;
                slaveno = sNo;
                ucENC.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                ucENC.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                ucENC.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucENC.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucENC.lvEncryptionListItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvEncryptionList_ItemCheck);
                ucENC.lvEncryptionListDoubleClick += new System.EventHandler(this.lvEncryptionList_DoubleClick);
                addListHeaders();
                //fillOptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DNP3Encryption(string mbsName, List<KeyValuePair<string, string>> mbsData, TreeNodeCollection tn)
        {
            ucENC.btnAddClick += new System.EventHandler(this.btnAdd_Click);
            ucENC.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
            ucENC.btnDoneClick += new System.EventHandler(this.btnDone_Click);
            ucENC.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
            ucENC.lvEncryptionListItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvEncryptionList_ItemCheck);
            ucENC.lvEncryptionListDoubleClick += new System.EventHandler(this.lvEncryptionList_DoubleClick);
            addListHeaders();
            string strRoutineName = "DNP3Encryption";
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
        public DNP3Encryption(XmlNode sNode, TreeNode tn)
        {
            ucENC.btnAddClick += new System.EventHandler(this.btnAdd_Click);
            ucENC.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
            ucENC.btnDoneClick += new System.EventHandler(this.btnDone_Click);
            ucENC.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
            ucENC.lvEncryptionListItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvEncryptionList_ItemCheck);
            ucENC.lvEncryptionListDoubleClick += new System.EventHandler(this.lvEncryptionList_DoubleClick);
            addListHeaders();
            string strRoutineName = "DNP3Encryption";
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
            string strRoutineName = "DNP3Encryption:addListHeaders";
            try
            {
                ucENC.lvEncryptionList.Columns.Add("Cipher Suite", 100, HorizontalAlignment.Left);
                ucENC.lvEncryptionList.Columns.Add("Certificate", 150, HorizontalAlignment.Left);
                ucENC.lvEncryptionList.Columns.Add("Private Key", 120, HorizontalAlignment.Left);
                ucENC.lvEncryptionList.Columns.Add("CA", 170, HorizontalAlignment.Left);
                ucENC.lvEncryptionList.Columns.Add("DH Parameter", 170, HorizontalAlignment.Left);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void refreshList()
        {
            string strRoutineName = "DNP3Encryption:refreshList";
            try
            {
                int cnt = 0;
                //Utils.GraphicalDisplaySlaveList1.Clear();
                ucENC.lvEncryptionList.Items.Clear();
                foreach (Encryption sl in EncryptionList)
                {
                    string[] row = new string[5];
                    if (sl.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = sl.CipherSuite;
                        row[1] = sl.Certificate;
                        row[2] = sl.PrivateKey;
                        row[3] = sl.CA;
                        row[4] = sl.DHParameter;
                       
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucENC.lvEncryptionList.Items.Add(lvItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            string strRoutineName = "DNP3Encryption:btnDone_Click";
            try
            {
                List<KeyValuePair<string, string>> mbsData = Utils.getKeyValueAttributes(ucENC.grpEncryption);
                if (mode == Mode.ADD)
                {
                    TreeNode tmp = null;
                    EncryptionList.Add(new Encryption("Encryption", mbsData, null, SlaveTypes.DNP3SLAVE, slaveno));
                    //DNPObjectVarList.Add(new DNP3ObjectVariation("DNP3ObjectVariation", mbsData, tmp, Int32.Parse(SlaveNum)));
                }
                else if (mode == Mode.EDIT)
                {
                    EncryptionList[editIndex].updateAttributes(mbsData);
                    //DNPObjectVarList[editIndex].updateAttributes(mbsData);
                }
                refreshList();
                if (sender != null && e != null)
                {
                    ucENC.grpEncryption.Visible = false;
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
            string strRoutineName = "DNP3Encryption:btnCancel_Click";
            try
            {
                ucENC.grpEncryption.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                ucENC.grpEncryption.Visible = false;
                Utils.resetValues(ucENC.grpEncryption);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string strRoutineName = "DNP3Encryption: btnAdd_Click";
            try
            {
                //{
                if (EncryptionList.Count >= Globals.MaxEncryption)
                {
                    MessageBox.Show("Maximum " + Globals.MaxEncryption + " Encryption are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                mode = Mode.ADD;
                editIndex = -1;
                Utils.resetValues(ucENC.grpEncryption);
                Utils.showNavigation(ucENC.grpEncryption, false);
                loadDefaults();
                ucENC.grpEncryption.Visible = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadDefaults()
        {
            string strRoutineName = "DNP3Encryption:loadDefaults";
            try
            {
                ucENC.txtCS.Text = "Abc";
                ucENC.txtCA.Text = "Abc";
                ucENC.txtCer.Text = "Abc";
                ucENC.txtDHP.Text = "Abc";
                ucENC.txtPK.Text = "49 c8 7d 5d 90 21 7a af ec 80 74 eb 71 52 fd b5";
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strRoutineName = "DNP3Encryption: btnDelete_Click";
            try
            {
                if (ucENC.lvEncryptionList.Items.Count == 0)
                {
                    MessageBox.Show("Please add atleast one Encryption ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (ucENC.lvEncryptionList.CheckedItems.Count == 1)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete Encryption?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        int iIndex = ucENC.lvEncryptionList.CheckedItems[0].Index;
                        if (result == DialogResult.Yes)
                        {
                            //DNP3SlaveTreeNode.Nodes.Remove(DNPObjectVarList.ElementAt(iIndex).getTreeNode());
                            EncryptionList.RemoveAt(iIndex);
                            ucENC.lvEncryptionList.Items[iIndex].Remove();
                            //Globals.TotalSlavesCount -= 1; //Ajay: 25/08/2018
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
        private void lvEncryptionList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string strRoutineName = "DNP3Encryption:lvEncryptionList_ItemCheck";
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    ucENC.lvEncryptionList.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
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
            string strRoutineName = "DNP3Encryption:loadValues";
            try
            {
                Encryption Encry = EncryptionList.ElementAt(editIndex);
                if(Encry!=null)
                {
                    ucENC.txtCS.Text = Encry.CipherSuite;
                    ucENC.txtCA.Text = Encry.CA;
                    ucENC.txtCer.Text = Encry.Certificate;
                    ucENC.txtDHP.Text = Encry.DHParameter;
                    ucENC.txtPK.Text = Encry.PrivateKey;
                }
                //DNP3ObjectVariation ObjectVariation = DNPObjectVarList.ElementAt(editIndex);
                //if (ObjectVariation != null)
                //{
                //    ucENC.txtDestAdd.Text = ObjectVariation.SlaveNum;
                //    ucENC.txtC1ME.Text = "1";
                //    ucENC.txtC1MD.Text = "500";
                //    ucENC.txtC2ME.Text = "1";
                //    ucENC.txtC2MD.Text = "500";
                //    ucENC.txtC3ME.Text = "1";
                //    ucENC.txtC1MD.Text = "500";
                //    ucENC.txtMR.Text = "3";
                //    ucENC.txtRD.Text = "2000";
                //    ucENC.txtORD.Text = "3000";


                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvEncryptionList_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "DNP3Encryption:lvEncryptionList_DoubleClick";
            try
            {

                if (ucENC.lvEncryptionList.SelectedItems.Count <= 0) return;
                ListViewItem lvi = ucENC.lvEncryptionList.SelectedItems[0];
                Utils.UncheckOthers(ucENC.lvEncryptionList, lvi.Index);
                if (EncryptionList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ucENC.grpEncryption.Visible = true;
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(ucENC.grpEncryption, true);
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
            rootNode = xmlDoc.CreateElement("Encryption");
            xmlDoc.AppendChild(rootNode);
            foreach (Encryption ai in EncryptionList)
            {
                XmlAttribute attr1 = xmlDoc.CreateAttribute("CipherSuite");
                attr1.Value = ai.CipherSuite.ToString();
                rootNode.Attributes.Append(attr1);

                XmlAttribute attr2 = xmlDoc.CreateAttribute("Certificate");
                attr2.Value = ai.Certificate.ToString();
                rootNode.Attributes.Append(attr2);

                XmlAttribute attrPrivateKey = xmlDoc.CreateAttribute("PrivateKey");
                attrPrivateKey.Value = ai.PrivateKey.ToString();
                rootNode.Attributes.Append(attrPrivateKey);

                XmlAttribute attrCA = xmlDoc.CreateAttribute("CA");
                attrCA.Value = ai.CA.ToString();
                rootNode.Attributes.Append(attrCA);

                XmlAttribute att2DHParameter = xmlDoc.CreateAttribute("DHParameter");
                att2DHParameter.Value = ai.DHParameter.ToString();
                rootNode.Attributes.Append(att2DHParameter);
            }
            return rootNode;
        }
        private string rnName = "";
        public void parseOVNode(XmlNode aicNode, bool imported)
        {
            string strRoutineName = "DNP3Encryption: parseOVNode";
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
                    if (node.ChildNodes[2].Attributes.Count > 0)
                    {
                        if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                                                                           //URList.Add(new UnsolResponse(node.ChildNodes[0], SlaveTypes.DNP3SLAVE, slaveno, false));
                        EncryptionList.Add(new Encryption(node.ChildNodes[2], SlaveTypes.DNP3SLAVE, slaveno, false));
                    }
                    //if (node.Name == "Encryption")
                    //{
                    //    if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                    //    EncryptionList.Add(new Encryption(node, SlaveTypes.DNP3SLAVE, slaveno, false));
                    //}
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
            foreach (Encryption URNode in EncryptionList)
            {
                if (URNode.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("Encryption_"))
            {
                return ucENC;
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
