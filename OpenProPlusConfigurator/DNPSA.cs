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
    public class DNPSA
    {
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        enum DNP3SA
        {
            DNPSA
        };
        public TreeNode UserConfigSlaveTreeNode;
        private DNP3SA ainType = DNP3SA.DNPSA;
        private bool aggressiveM = true;
        private bool authErrEvent = true;
        private int replyTOut = -1;
        //public string replyTOut ;
        public string hmac ="";
        private int maxAuthErrCount = -1;
        private int sSIVTime = -1;
        private int sSIVCount = -1;
        private string authErrEventClass;
        private int slaveNum = -1; private bool isNodeComment = false;
        public static string[] arrAttributes = new string[] { "AggressiveM", "ReplyTOut", "SSIVTime", "SSIVCount", "MaxAuthErrCount", "AuthErrEvent", "AuthErrEventClass", "HMACAlgo" };
        private SlaveTypes slaveType = SlaveTypes.UNKNOWN;
        private int slaveNo = -1;
        private string[] arrHMACAlgo; private string dType = "";
        private string[] arrAEEC; private string AType = "";
        ucGroupUser User = new ucGroupUser();

        List<DNP3User> UserList = new List<DNP3User>();
        public DNPSA(string aiName, List<KeyValuePair<string, string>> aiData, TreeNode tn, SlaveTypes sType, int sNo)
        {
            string strRoutineName = "DNPSA";
            try
            {
                User.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                User.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                User.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                User.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                User.lvUserListItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvUserList_ItemCheck);
                User.lvUserListDoubleClick += new System.EventHandler(this.lvUserList_DoubleClick);
                User.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                User.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                User.btnNextClick += new System.EventHandler(this.btnNext_Click);
                User.btnLastClick += new System.EventHandler(this.btnLast_Click);
                addListHeaders();
                SetSupportedHMCAlgo();
                SetAuthErrorEvent();
                slaveType = sType;
                slaveNo = sNo;
                try
                {
                   // ainType = (DNP3SA)Enum.Parse(typeof(DNP3SA), aiName);
                }
                catch (System.ArgumentException)
                {
                }
                if (aiData != null && aiData.Count > 0) //Parse n store values...
                {
                    foreach (KeyValuePair<string, string> aikp in aiData)
                    {
                        try
                        {
                            if (this.GetType().GetProperty(aikp.Key) != null) //Ajay: 03/07/2018
                            {
                                this.GetType().GetProperty(aikp.Key).SetValue(this, aikp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                        }
                    }
                }
                if (tn != null) tn.Nodes.Clear();
                UserConfigSlaveTreeNode = tn;
                if (tn != null) tn.Text = "UserConf " + "User" ;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public DNPSA(XmlNode aiNode, SlaveTypes sType, int sNo, bool imported,TreeNode tn)
        {
            string strRoutineName = "DNPSA";
            try
            {
                User.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                User.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                User.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                User.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                User.lvUserListItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvUserList_ItemCheck);
                User.lvUserListDoubleClick += new System.EventHandler(this.lvUserList_DoubleClick);
                User.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                User.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                User.btnNextClick += new System.EventHandler(this.btnNext_Click);
                User.btnLastClick += new System.EventHandler(this.btnLast_Click); addListHeaders();
                SetSupportedHMCAlgo(); SetAuthErrorEvent();
                slaveType = sType;
                slaveNo = sNo;
                if (aiNode.Attributes != null)
                {
                    try
                    {
                        ainType = (DNP3SA)Enum.Parse(typeof(DNP3SA), aiNode.Name);
                    }
                    catch (System.ArgumentException)
                    {
                    }
                    foreach (XmlAttribute item in aiNode.Attributes)
                    {
                        try
                        {
                            if (this.GetType().GetProperty(item.Name) != null) //Ajay: 03/07/2018
                            {
                                this.GetType().GetProperty(item.Name).SetValue(this, item.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                        }
                    }
                    if (tn != null) tn.Nodes.Clear();
                    UserConfigSlaveTreeNode = tn;
                    if (tn != null) tn.Text = "UserConf " + "User";
                    parseIECGNode(aiNode, tn);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string rnName = "SMSlave";
        public void parseIECGNode(XmlNode iecgNode, TreeNode tn)
        {
            string strRoutineName = "DNPSA:parseIECGNode";
            try
            {
                rnName = iecgNode.Name;
                tn.Nodes.Clear();
                UserConfigSlaveTreeNode = tn;
               
                    foreach (XmlNode node in iecgNode)
                    //foreach (XmlNode node in iecgNode.ChildNodes[0].ChildNodes)
                    {
                        TreeNode tmp = null;
                        XmlElement root = node.ParentNode as XmlElement;
                        if (node.NodeType == XmlNodeType.Comment) continue;
                        UserList.Add(new DNP3User(node, tmp));
                    }
                
                refreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void updateAttributes(List<KeyValuePair<string, string>> aiData)
        {
            string strRoutineName = "DNPSA:updateAttributes";
            try
            {
                if (aiData != null && aiData.Count > 0)
                {
                    foreach (KeyValuePair<string, string> aikp in aiData)
                    {
                        try
                        {
                            if (this.GetType().GetProperty(aikp.Key) != null) //Ajay: 03/07/2018
                            {
                                this.GetType().GetProperty(aikp.Key).SetValue(this, aikp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            string strRoutineName = "DNPSA:btnDone_Click";
            try
            {
                List<KeyValuePair<string, string>> mbsData = Utils.getKeyValueAttributes(User.grpUser);
                if (mode == Mode.ADD)
                {
                    TreeNode tmp = null;
                    UserList.Add(new DNP3User("User", mbsData, tmp, Int32.Parse(SlaveNum)));
                }
                else if (mode == Mode.EDIT)
                {
                    UserList[editIndex].updateAttributes(mbsData);
                }
                refreshList();
                if (sender != null && e != null)
                {
                    User.grpUser.Visible = false;
                    mode = Mode.NONE;
                    editIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void refreshList()
        {
            string strRoutineName = "DNPSA:refreshList";
            try
            {
                int cnt = 0;
                User.lvUserList.Items.Clear();
                foreach (DNP3User sl in UserList)
                {
                    string[] row = new string[3];
                    if (sl.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = sl.No;
                        row[1] = sl.Name;
                        row[2] = sl.Key;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    User.lvUserList.Items.Add(lvItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadValues()
        {
            string strRoutineName = "DNPSA:loadValues";
            try
            {
                DNP3User User1 = UserList.ElementAt(editIndex);
                if (User1 != null)
                {
                    User.txtUserNo.Text = User1.No;
                    User.txtUserName.Text = User1.Name;
                    User.txtKey.Text = User1.Key;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            string strRoutineName = "SMSSlave:btnCancel_Click";
            try
            {
                User.grpUser.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                User.grpUser.Visible = false;
                Utils.resetValues(User.grpUser);
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
                if (User.lvUserList.Items.Count <= 0) return;
                if (UserList.ElementAt(0).IsNodeComment) return;
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
                if (UserList.ElementAt(editIndex - 1).IsNodeComment) return;
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
                if (editIndex + 1 >= User.lvUserList.Items.Count) return;
                if (UserList.ElementAt(editIndex + 1).IsNodeComment) return;
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
                if (User.lvUserList.Items.Count <= 0) return;
                if (UserList.ElementAt(UserList.Count - 1).IsNodeComment) return;
                editIndex = UserList.Count - 1;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void addListHeaders()
        {
            User.lvUserList.Columns.Add("User No.", 150, HorizontalAlignment.Left);
            User.lvUserList.Columns.Add("User Name", 160, HorizontalAlignment.Left);
            User.lvUserList.Columns.Add("Key", 250, HorizontalAlignment.Left);
            User.lvUserList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (UserList.Count >= Globals.MaxDNPSA)
            {
                MessageBox.Show("Maximum " + Globals.MaxDNPSA + " DNPSA are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            mode = Mode.ADD;
            editIndex = -1;
            Utils.resetValues(User.grpUser);
            Utils.showNavigation(User.grpUser, false);
            loadDefaults();
            User.txtUserNo.Focus();
            User.grpUser.Visible = true;
        }
        private void loadDefaults()
        {
            string strRoutineName = "DNPSA:loadDefaults";
            try
            {
                User.txtUserNo.Text = "1";
                User.txtUserName.Text = "User1";
                User.txtKey.Text = "49 c8 7d 5d 90 21 7a af ec 80 74 eb 71 52 fd b5";
                //UFSMS.txtMobileNo.Text = "+91";
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlaveGroup: btnDelete_Click";
            try
            {
                if (User.lvUserList.Items.Count == 0)
                {
                    MessageBox.Show("Please add atleast one DNPSA ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (User.lvUserList.CheckedItems.Count == 1)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        int iIndex = User.lvUserList.CheckedItems[0].Index;
                        if (result == DialogResult.Yes)
                        {
                            //UserConfigSlaveTreeNode.Nodes.Remove(UserList.ElementAt(iIndex).getTreeNode());
                            UserList.RemoveAt(iIndex);
                            User.lvUserList.Items[iIndex].Remove();
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
        private void lvUserList_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "DNP3Encryption:lvUserList_DoubleClick";
            try
            {

                if (User.lvUserList.SelectedItems.Count <= 0) return;
                ListViewItem lvi = User.lvUserList.SelectedItems[0];
                Utils.UncheckOthers(User.lvUserList, lvi.Index);
                if (UserList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                User.grpUser.Visible = true;
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(User.grpUser, true);
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvUserList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string strRoutineName = "DNP3Encryption:lvUserList_ItemCheck";
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    User.lvUserList.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
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
        public XmlNode exportXMLnode()
        {
            XmlDocument xmlDoc = new XmlDocument();
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
            XmlNode rootNode = null;
            if (isNodeComment)
            {
                rootNode = xmlDoc.CreateComment("DNPSA");
                xmlDoc.AppendChild(rootNode);
                return rootNode;
            }
            //rootNode = xmlDoc.CreateElement("DNPSA");
            //xmlDoc.AppendChild(rootNode);
            //foreach (string attr in arrAttributes)
            //{
            //    XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
            //    attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
            //    rootNode.Attributes.Append(attrName);
            //}
            rootNode = xmlDoc.CreateElement("USERConf");//rootNode = xmlDoc.CreateElement(rnName);
            xmlDoc.AppendChild(rootNode);
            foreach (DNP3User ai in UserList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(ai.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }
            //XmlNode UserConfNode = xmlDoc.CreateElement("USERConf");// Create a new child node
            //xmlDoc.AppendChild(UserConfNode); //add the child node to the root node
            //foreach (DNP3User user in UserList)
            //{
            //    XmlNode importNode = rootNode.OwnerDocument.ImportNode(user.exportXMLnode(), true);
            //    xmlDoc.AppendChild(importNode);
            //}

            return rootNode;
        }

        public TreeNode getTreeNode()
        {
            return UserConfigSlaveTreeNode;
        }
        
        public string SlaveNum
        {
            get { return slaveNum.ToString(); }
            set { slaveNum = Int32.Parse(value); Globals.SlaveNo = Int32.Parse(value); }
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("User_"))
            {
                return User;
            }
            return null;
        }
        public string getSlaveID
        {
            get { return "DNP3Slave_" + SlaveNum; }
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public string ReplyTOut
        {
            get
            {
                try
                {
                  
                    replyTOut = Int32.Parse(User.txtART.Text);
                }
                catch (System.FormatException)
                {
                }
                return replyTOut.ToString();
            }
            set { replyTOut = Int32.Parse(value); User.txtART.Text = value; }
            //get { replyTOut = Int32.Parse(User.txtART.Text); return replyTOut.ToString(); }
            //set { replyTOut = Int32.Parse(value); User.txtART.Text = value; }


            //get { return replyTOut.ToString(); }
            //    set
            //    {
            //        replyTOut = Int32.Parse(value);
            //    }
            //get { replyTOut = Int32.Parse(User.txtART.Text); return replyTOut.ToString(); }
            //set { replyTOut = Int32.Parse(value); User.txtART.Text = value; }
        }
        public string SSIVTime
        {
            get { sSIVTime = Int32.Parse(User.txtKIT.Text); return sSIVTime.ToString(); }
            set { sSIVTime = Int32.Parse(value); User.txtKIT.Text = value; }
        }
        public string SSIVCount
        {
            get { sSIVCount = Int32.Parse(User.txtKIC.Text); return sSIVCount.ToString(); }
            set { sSIVCount = Int32.Parse(value); User.txtKIC.Text = value; }
        }
        public string MaxAuthErrCount
        {
            get { maxAuthErrCount = Int32.Parse(User.txtAEC.Text); return maxAuthErrCount.ToString(); }
            set { maxAuthErrCount = Int32.Parse(value); User.txtAEC.Text = value; }
        }
        public string AuthErrEventClass
        {
            get { return authErrEventClass = User.txtAEEVC.Text; }
            set { authErrEventClass = User.txtAEEVC.Text = value; }
        }
        public string AggressiveM
        {
            get { aggressiveM = User.chkAggMode.Checked; return (aggressiveM == true ? "YES" : "NO"); }
            set
            {
                aggressiveM = (value.ToLower() == "yes") ? true : false;
                if (aggressiveM == true) User.chkAggMode.Checked = true;
                else User.chkAggMode.Checked = false;
            }
        }
        public string AuthErrEvent
        {
            get { authErrEvent = User.ChkAEE.Checked; return (authErrEvent == true ? "YES" : "NO"); }
            set
            {
                authErrEvent = (value.ToLower() == "yes") ? true : false;
                if (authErrEvent == true) User.ChkAEE.Checked = true;
                else User.ChkAEE.Checked = false;
            }
        }
        public string HMACAlgo
        {
            get { return hmac = User.txtHMAC.Text; }
            set { hmac = User.txtHMAC.Text = value; }
        }
        private void SetSupportedHMCAlgo()
        {
            string strRoutineName = "AO: SetSupportedResponseTypes";
            try
            {
                arrHMACAlgo = Utils.getOpenProPlusHandle().getDataTypeValues("HMAC_Algorithm").ToArray();
                //Namrata: 24/04/2018
                if (arrHMACAlgo == null)
                {

                }
                else
                {
                    if (arrHMACAlgo.Length > 0) dType = arrHMACAlgo[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static List<string> getHMACAlgo(SlaveTypes masterType)
        {
            if (masterType == SlaveTypes.DNP3SLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("HMAC_Algorithm");
            else
                return new List<string>();
        }

        private void SetAuthErrorEvent()
        {
            string strRoutineName = "AO: SetAuthErrorEvent";
            try
            {
                arrAEEC = Utils.getOpenProPlusHandle().getDataTypeValues("AuthErrorEventClass").ToArray();
                //Namrata: 24/04/2018
                if (arrAEEC == null){ }
                else
                {
                    if (arrAEEC.Length > 0) AType = arrAEEC[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static List<string> getAuthErrorEventClass(SlaveTypes sType)
        {
            if (sType == SlaveTypes.DNP3SLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("AuthErrorEventClass");
            else
                return new List<string>();
        }
    }
}
