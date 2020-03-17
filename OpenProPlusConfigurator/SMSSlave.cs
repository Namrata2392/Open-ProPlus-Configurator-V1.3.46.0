//Namrata:24/05/2019
//Created by Namrata Chaudhari
//---------------------------------------------------------------------------------------------------------------------------
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
    public class SMSSlave
    {
        #region Declaration
        enum slaveType
        {
            SMSSlave
        };
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        public TreeNode SMSSlaveTreeNode;
        private bool isNodeComment = false;
        private string comment = "";
        private slaveType sType = slaveType.SMSSlave;
        private int slaveNum = -1;
        private string modem = "";
        private bool run = true;
        private bool enableEncryption = true;
        private bool userFriendlySMS = true;
        private int portNum = -1;
        private string remoteMobileNo = "";
        private string tolerancePeriodInMin;
        List<SMSUser> UFSMSList = new List<SMSUser>();
        //List<UFSMS> UFSMSList = new List<UFSMS>();
        ucUFSMS UFSMS = new ucUFSMS();
        //Namrata:24/05/2019
        private int debug; private string eventQSize;
        private string appFirmwareVersion; public string unitid = "";
        public static string[] arrAttributes = new string[] { "SlaveNum", "UnitID", "PortNum", "Modem", "EventQSize", "TolerancePeriodInMin", "RemoteMobileNo", "EnableEncryption", "UserFriendlySMS", "DEBUG", "AppFirmwareVersion", "Run" };
        #endregion Declaration

        public static List<string> getModem()
        {
            return Utils.getOpenProPlusHandle().getDataTypeValues("SMSSlave_Modem");
        }
        public SMSSlave(string mbsName, List<KeyValuePair<string, string>> mbsData, TreeNode tn)
        {
            string strRoutineName = "SMSSlave";
            UFSMS.btnAddClick += new System.EventHandler(this.btnAdd_Click);
            UFSMS.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
            UFSMS.btnDoneClick += new System.EventHandler(this.btnDone_Click);
            UFSMS.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
            UFSMS.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
            UFSMS.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
            UFSMS.btnNextClick += new System.EventHandler(this.btnNext_Click);
            UFSMS.btnLastClick += new System.EventHandler(this.btnLast_Click);
            UFSMS.lvUFSMSListItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvUFSMSList_ItemCheck);
            UFSMS.lvUFSMSListDoubleClick += new System.EventHandler(this.lvUFSMSList_DoubleClick);
            addListHeaders();
            try
            {
                try
                {
                    sType = (slaveType)Enum.Parse(typeof(slaveType), mbsName);
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
                    Utils.Write(VerboseLevel.DEBUG, "\n");
                    if (tn != null) tn.Nodes.Clear();
                    SMSSlaveTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...   
                    if (tn != null) tn.Text = "SMSSlave " + "SMSSlave_" + this.SlaveNum;
                    Utils.SMSSlaveTreeNode = tn;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public SMSSlave(XmlNode sNode, TreeNode tn)
        {
            UFSMS.btnAddClick += new System.EventHandler(this.btnAdd_Click);
            UFSMS.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
            UFSMS.btnDoneClick += new System.EventHandler(this.btnDone_Click);
            UFSMS.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
            UFSMS.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
            UFSMS.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
            UFSMS.btnNextClick += new System.EventHandler(this.btnNext_Click);
            UFSMS.btnLastClick += new System.EventHandler(this.btnLast_Click);
            UFSMS.lvUFSMSListItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvUFSMSList_ItemCheck);
            UFSMS.lvUFSMSListDoubleClick += new System.EventHandler(this.lvUFSMSList_DoubleClick);
            addListHeaders();
            string strRoutineName = "SMSSlave";
            try
            {
                if (sNode.Attributes != null)
                {
                    try
                    {
                        sType = (slaveType)Enum.Parse(typeof(slaveType), sNode.Name);
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
                    SMSSlaveTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...   
                    if (tn != null) tn.Text = "SMSSlave " + "SMSSlave_" + this.SlaveNum;
                    parseIECGNode(sNode, tn);
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
            UFSMS.lvUFSMSList.Columns.Add("Mobile No.", 150, HorizontalAlignment.Left);
            UFSMS.lvUFSMSList.Columns.Add("Grant For Control", 160, HorizontalAlignment.Left);
            UFSMS.lvUFSMSList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
        }
        public void updateAttributes(List<KeyValuePair<string, string>> mbsData)
        {
            string strRoutineName = "SMSSlave: updateAttributes";
            try
            {
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
                    Utils.Write(VerboseLevel.DEBUG, "\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadValues()
        {
            string strRoutineName = "SMSSlave:loadValues";
            try
            {
                SMSUser SMS = UFSMSList.ElementAt(editIndex);
                //UFSMS SMS = UFSMSList.ElementAt(editIndex);
                if (SMS != null)
                {
                    UFSMS.txtMobileNo.Text = SMS.MobileNo;
                    if (SMS.GrantForControl.ToLower() == "yes") UFSMS.chkGrantForControl.Checked = true;
                    else UFSMS.chkGrantForControl.Checked = false;
                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (UFSMSList.Count >= Globals.MaxSMSUser)
            {
                MessageBox.Show("Maximum " + Globals.MaxSMSUser + " SMSUser's are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            mode = Mode.ADD;
            editIndex = -1;
            Utils.resetValues(UFSMS.grpUFSMS);
            Utils.showNavigation(UFSMS.grpUFSMS, false);
            loadDefaults();
            UFSMS.txtMobileNo.Focus();
            UFSMS.grpUFSMS.Visible = true;
        }
        private void loadDefaults()
        {
            string strRoutineName = "SMSSlave:loadDefaults";
            try
            {
                UFSMS.txtMobileNo.Text = "+";
                //UFSMS.txtMobileNo.Text = "+91";
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            string strRoutineName = "SMSSlaveGroup:btnCancel_Click";
            try
            {
                UFSMS.grpUFSMS.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                UFSMS.grpUFSMS.Visible = false;
                Utils.resetValues(UFSMS.grpUFSMS);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strRoutineName = "SMSSlave:btnDelUFSMS.lvUFSMSListete_Click";
            try
            {
                Utils.WriteLine(VerboseLevel.DEBUG, "*** UFSMSList count: {0} lv count: {1}", UFSMSList.Count, UFSMS.lvUFSMSList.Items.Count);
                if (UFSMS.lvUFSMSList.Items.Count == 0)
                {
                    MessageBox.Show("Please add atleast one SMSUser ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (UFSMS.lvUFSMSList.CheckedItems.Count == 1)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete Slave No." + UFSMS.lvUFSMSList.CheckedItems[0].Text + " ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        int iIndex = UFSMS.lvUFSMSList.CheckedItems[0].Index;
                        if (result == DialogResult.Yes)
                        {
                            UFSMSList.RemoveAt(iIndex);
                            UFSMS.lvUFSMSList.Items[iIndex].Remove();
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
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void refreshList()
        {
            string strRoutineName = "SMSSlave:refreshList";
            try
            {
                int cnt = 0;
                UFSMS.lvUFSMSList.Items.Clear();
                foreach (SMSUser sl in UFSMSList)
                { 
                    string[] row = new string[2];
                    if (sl.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = sl.MobileNo;
                        row[1] = sl.GrantForControl;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    UFSMS.lvUFSMSList.Items.Add(lvItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            string strRoutineName = "SMSSlave:btnDone_Click";
            try
            {
                List<KeyValuePair<string, string>> mbsData = Utils.getKeyValueAttributes(UFSMS.grpUFSMS);
                if (mode == Mode.ADD)
                {
                    TreeNode tmp = null;
                    UFSMSList.Add(new SMSUser("SMSUser", mbsData, tmp, Int32.Parse(SlaveNum)));
                }
                else if (mode == Mode.EDIT)
                {
                    UFSMSList[editIndex].updateAttributes(mbsData);
                }
                refreshList();
                if (sender != null && e != null)
                {
                    UFSMS.grpUFSMS.Visible = false;
                    mode = Mode.NONE;
                    editIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            string strRoutineName = "SMSSlave:btnFirst_Click";
            try
            {
                if (UFSMS.lvUFSMSList.Items.Count <= 0) return;
                if (UFSMSList.ElementAt(0).IsNodeComment) return;
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
            string strRoutineName = "SMSSlave:btnPrev_Click";
            try
            {
                //Namrata:27/7/2017
                btnDone_Click(null, null);
                Console.WriteLine("*** ucSMS btnPrev_Click clicked in class!!!");
                if (editIndex - 1 < 0) return;
                if (UFSMSList.ElementAt(editIndex - 1).IsNodeComment) return;
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
            string strRoutineName = "SMSSlave:btnNext_Click";
            try
            {
                //Namrata:27/7/2017
                btnDone_Click(null, null);
                Console.WriteLine("*** ucSMS btnNext_Click clicked in class!!!");
                if (editIndex + 1 >= UFSMS.lvUFSMSList.Items.Count) return;
                if (UFSMSList.ElementAt(editIndex + 1).IsNodeComment) return;
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
            string strRoutineName = "SMSSlave:btnLast_Click";
            try
            {
                Console.WriteLine("*** ucSMS btnLast_Click clicked in class!!!");
                if (UFSMS.lvUFSMSList.Items.Count <= 0) return;
                if (UFSMSList.ElementAt(UFSMSList.Count - 1).IsNodeComment) return;
                editIndex = UFSMSList.Count - 1;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvUFSMSList_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "lvUFSMSList_DoubleClick";
            try
            {

                if (UFSMS.lvUFSMSList.SelectedItems.Count <= 0) return;
                ListViewItem lvi = UFSMS.lvUFSMSList.SelectedItems[0];
                Utils.UncheckOthers(UFSMS.lvUFSMSList, lvi.Index);
                if (UFSMSList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                UFSMS.grpUFSMS.Visible = true;
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(UFSMS.grpUFSMS, true);
                loadValues();
                UFSMS.txtSlaveNum.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvUFSMSList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string strRoutineName = "lvUFSMSList_ItemCheck";
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    UFSMS.lvUFSMSList.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
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
        private string rnName = "SMSlave";
        public void parseIECGNode(XmlNode iecgNode, TreeNode tn)
        {
            string strRoutineName = "SMSSlave:parseIECGNode";
            try
            {
                rnName = iecgNode.Name;
                tn.Nodes.Clear();
                SMSSlaveTreeNode = tn;
                foreach (XmlNode node in iecgNode.ChildNodes[0].ChildNodes)
                {
                    TreeNode tmp = null;
                    XmlElement root = node.ParentNode as XmlElement;
                    if (node.NodeType == XmlNodeType.Comment) continue;
                    UFSMSList.Add(new SMSUser(node, tmp));
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
            XmlNode rootNode = null;
            if (isNodeComment)
            {
                rootNode = xmlDoc.CreateComment(comment);
                xmlDoc.AppendChild(rootNode);
                return rootNode;
            }
            rootNode = xmlDoc.CreateElement(sType.ToString());
            xmlDoc.AppendChild(rootNode);
            foreach (string attr in arrAttributes)
            {
                XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                rootNode.Attributes.Append(attrName);
            }
            if (rootNode.Attributes[8].Value == "YES")
            {
                XmlNode UFSMSNode = xmlDoc.CreateElement("UFSMS");// Create a new child node
                rootNode.AppendChild(UFSMSNode); //add the child node to the root node
                foreach (SMSUser smsuser in UFSMSList)
                {
                    XmlNode importNode = rootNode.OwnerDocument.ImportNode(smsuser.exportXMLnode(), true);
                    UFSMSNode.AppendChild(importNode);
                }
            }
            else { }

            return rootNode;
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("SMSSlave_"))
            {
                return UFSMS;
            }
            return null;
        }
        public TreeNode getTreeNode()
        {
            return SMSSlaveTreeNode;
        }
        public string getSlaveID
        {
            get { return "SMSSlave" + SlaveNum; }
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public string SlaveNum
        {

            get { slaveNum = Int32.Parse(UFSMS.txtSlaveNum.Text); return slaveNum.ToString(); }
            set { slaveNum = Int32.Parse(value); UFSMS.txtSlaveNum.Text = value; Globals.SlaveNo = Int32.Parse(value); }
        }

        public string SlaveNum1
        {
            get
            {
                int i = 0;
                Int32.TryParse(UFSMS.txtSlaveNum.Text, out i);
                slaveNum = i;
                return slaveNum.ToString();
            }
            set
            {
                slaveNum = Int32.Parse(value); UFSMS.txtSlaveNum.Text = value;
            }
        }
        public string Modem
        {
            get { return modem = UFSMS.txtModem.Text; }
            set { modem = UFSMS.txtModem.Text = value; }
        }
        public string TolerancePeriodInMin
        {
            get { return tolerancePeriodInMin = UFSMS.txtTolerancePeriodInMin.Text; }
            set { tolerancePeriodInMin = UFSMS.txtTolerancePeriodInMin.Text = value; }

        }
        public string RemoteMobileNo
        {
            get { return remoteMobileNo = UFSMS.txtRemoteMobileNo.Text; }
            set { remoteMobileNo = UFSMS.txtRemoteMobileNo.Text = value; }
        }
        public string Run
        {
            get { run = UFSMS.chkRun.Checked; return (run == true ? "YES" : "NO"); }
            set
            {
                run = (value.ToLower() == "yes") ? true : false;
                if (run == true) UFSMS.chkRun.Checked = true;
                else UFSMS.chkRun.Checked = false;
            }
        }
        public string EnableEncryption
        {
            get { enableEncryption = UFSMS.chkEnableEncryption.Checked; return (enableEncryption == true ? "YES" : "NO"); }
            set
            {
                enableEncryption = (value.ToLower() == "yes") ? true : false;
                if (enableEncryption == true) UFSMS.chkEnableEncryption.Checked = true;
                else UFSMS.chkEnableEncryption.Checked = false;
            }
        }
        public string UserFriendlySMS
        {
            get { return (userFriendlySMS == true ? "YES" : "NO"); }
            set
            {
                userFriendlySMS = (value.ToLower() == "yes") ? true : false;
            }
            //get { userFriendlySMS = UFSMS.chkUserFriendlySMS.Checked; return (userFriendlySMS == true ? "YES" : "NO"); }
            //set
            //{
            //    userFriendlySMS = (value.ToLower() == "yes") ? true : false;
            //    if (userFriendlySMS == true) UFSMS.chkUserFriendlySMS.Checked = true;
            //    else UFSMS.chkUserFriendlySMS.Checked = false;
            //}
        }
        public string PortNum
        {
            get { portNum = Int32.Parse(UFSMS.txtPortNo.Text); return portNum.ToString(); }
            set { portNum = Int32.Parse(value); UFSMS.txtPortNo.Text = value; }
        }
        public string DEBUG
        {
            get { debug = Int32.Parse(UFSMS.txtDebug.Text); return debug.ToString(); }
            set { debug = Int32.Parse(value); UFSMS.txtDebug.Text = value; }
        }
        public string EventQSize
        {
            get { return eventQSize = UFSMS.txtEventQSize.Text; }
            set { eventQSize = UFSMS.txtEventQSize.Text = value; }
        }
        public string AppFirmwareVersion
        {
            get { return appFirmwareVersion = UFSMS.txtFirmwareVersion.Text; }
            set { appFirmwareVersion = UFSMS.txtFirmwareVersion.Text = value; }
        }
        public string UnitID
        {
            get { return unitid = UFSMS.txtUnitID.Text; }
            set { unitid = UFSMS.txtUnitID.Text = value; }
        }
    }
}
