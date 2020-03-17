using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;

namespace OpenProPlusConfigurator
{
   public class DNP3Settings
    {
        enum protocolType
        {
            TCP,
            RTU,
            ASCII
        };
       
        enum DNPType
        {
            DNP3Settings
        };
        ucGroupUnsolicited ucUR = new ucGroupUnsolicited();
        ucGroupDNPSlave ucDNP = new ucGroupDNPSlave();
        ucCommonDNPSlave ucDNPSettings = new ucCommonDNPSlave();
        private int slaveNum = -1;
        private protocolType pt = protocolType.TCP;
        private int portNum = -1;
        private int tcpPort=-1;
        private string remoteIP = "";
        private int unitID = -1;
        private int eventQSize;
        private string appFirmwareVersion;
        private int debug;
        private string localIP = "";
        private bool run = true;
        private string secremoteIP = "";
        private string portName;
        private int applCTout = -1;
        private int cvp = -1;
        private string fs;
        private int sTout = -1;
        private int DestAddress = -1;
        private bool MFAllow = true;
        private bool encyption = true;
        private bool Dnpsa = true;
        private bool TimeIIN = true;
        private bool RestartIIN = true;
        private bool UnsolResp = true;
        private DNPType DType = DNPType.DNP3Settings;
        private TreeNode DNP3TreeNode;
        private SlaveTypes slaveType = SlaveTypes.UNKNOWN;
        private DNP3UnsolResponse UnsolResNode = null;
        private DNP3ObjectVariation ObjectVarNode = null;
        private DNP3Encryption EncryptionVarNode = null;
        private DNP3DNPSA DNPSAVarNode = null;
        private DNP3User DNPSAUserNode = null; TreeNode parent = new TreeNode();
        private string[] arrAttributes;
        private void SetSupportedAttributes()
        {
            if (slaveType == SlaveTypes.DNP3SLAVE)
                arrAttributes = new string[] { "SlaveNum", "ProtocolType", "PortNum", "TcpPort", "LocalIP","RemoteIP", "SecRemoteIP", "UnitID", "EventQSize", "FragmentSize", "MultiFragment", "AppConfirmTimeout", "NeedTimeIIN", "NeedRestartIIN", "ClockValidatePeriod", "SelectTOut", "DestAdd", "UnsolicitedResponse", "Encryption", "DNPSA","DEBUG", "AppFirmwareVersion", "Run" }; //Ajay: 04/08/2018 FC added
        }
        public DNP3Settings(string iedName, List<KeyValuePair<string, string>> iedData, TreeNode tn, SlaveTypes sType, int sNo)
        {
            string strRoutineName = "DNP3Settings";
            try
            {
                slaveType = sType;
                slaveNum = sNo;
                SetSupportedAttributes();
                if (tn != null) DNP3TreeNode = tn;
                addListHeaders();
                fillOptions();
                try
                {
                    DType = (DNPType)Enum.Parse(typeof(DNPType), iedName);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", iedName);
                }
                if (iedData != null && iedData.Count > 0)
                {
                    foreach (KeyValuePair<string, string> iedkp in iedData)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", iedkp.Key, iedkp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(iedkp.Key) != null) //Ajay: 03/07/2018
                            {
                                this.GetType().GetProperty(iedkp.Key).SetValue(this, iedkp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", iedkp.Key, iedkp.Value);
                        }
                    }
                    Utils.Write(VerboseLevel.DEBUG, "\n");
                }
                 if (tn != null) tn.Text = "DNP3Settings " + "DNP3Settings_"+slaveNum;

                UnsolResNode = new DNP3UnsolResponse(sType, slaveNum);
                ObjectVarNode = new DNP3ObjectVariation(sType, slaveNum);
                EncryptionVarNode = new DNP3Encryption(sType, slaveNum);
                DNPSAVarNode = new DNP3DNPSA(sType, slaveNum);
                DNPSAVarNode = new DNP3DNPSA(tn, sType, slaveNum);
                //DNPSAUserNode = new DNP3User();

                if (tn != null) tn.Nodes.Add("UnsolicitedResponse", "Unsolicited Response", "UnsolicitedResponse", "UnsolicitedResponse");
                UnsolResNode.parseURNode(null, false);

                if (tn != null) tn.Nodes.Add("ObjectVariation", "Object Variation", "ObjectVariation", "ObjectVariation");
                ObjectVarNode.parseOVNode(null, false);

                if (tn != null) tn.Nodes.Add("Encryption", "Encryption", "Encryption", "Encryption");
                EncryptionVarNode.parseOVNode(null, false);

                if (tn != null) tn.Nodes.Add("DNPSA", "DNPSA", "DNPSA", "DNPSA");
                //tn.Nodes[3].Nodes.Add("USERCONFUser", "USERCONF User", "USERCONFUser", "USERCONFUser");
                DNPSAVarNode.parseOVNode(null,tn);

                refreshList();
                //ucied.lblIED.Text = "IED Details (Unit: " + unitID.ToString() + ")";
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public DNP3Settings(XmlNode iNode, TreeNode tn, SlaveTypes sType, int sNo, bool imported)
        {

            string strRoutineName = "DNP3Settings";
            try
            {
                slaveType = sType;
                slaveNum = sNo;
                SetSupportedAttributes();
                if (tn != null) DNP3TreeNode = tn;
                addListHeaders(); fillOptions();
                try
                {
                    DType = (DNPType)Enum.Parse(typeof(DNPType), iNode.Name);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", iNode.Name);
                }
                foreach (XmlAttribute item in iNode.Attributes)
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
                        Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", item.Name, item.Value);
                    }
                }
                // if (tn != null) tn.Text = "DNPSettings " + this.Description;
                if (tn != null) tn.Text = "DNP3Settings " + "DNP3Settings_" + slaveNum;
                UnsolResNode = new DNP3UnsolResponse(sType, slaveNum);
                ObjectVarNode = new DNP3ObjectVariation(sType, slaveNum);
                EncryptionVarNode = new DNP3Encryption(sType, slaveNum);
                DNPSAVarNode = new DNP3DNPSA(sType, slaveNum);

                if (tn != null) tn.Nodes.Add("UnsolicitedResponse", "Unsolicited Response", "UnsolicitedResponse", "UnsolicitedResponse");
                //UnsolResNode.parseURNode(iNode.ChildNodes[0], false);
                 UnsolResNode.parseURNode(iNode, false);
                if (tn != null) tn.Nodes.Add("ObjectVariation", "Object Variation", "ObjectVariation", "ObjectVariation");
                //ObjectVarNode.parseOVNode(iNode.ChildNodes[0], false);
                ObjectVarNode.parseOVNode(iNode, false);
                if (tn != null) tn.Nodes.Add("Encryption", "Encryption", "Encryption", "Encryption");
                //EncryptionVarNode.parseOVNode(iNode.ChildNodes[0], false);
                EncryptionVarNode.parseOVNode(iNode, false);
                if (tn != null) tn.Nodes.Add("DNPSA", "DNPSA", "DNPSA", "DNPSA");
                //tn.Nodes[3].Nodes.Add("USERCONFUser", "USERCONF User", "USERCONFUser", "USERCONFUser");
                DNPSAVarNode.parseOVNode(iNode, tn);
                //DNPSAVarNode.parseOVNode(iNode, tn);
                refreshList();
                //ucied.lblIED.Text = "IED Details (Unit: " + unitID.ToString() + ")";
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void updateAttributes(List<KeyValuePair<string, string>> mbsData)
        {
            string strRoutineName = "DNPSlave: updateAttributes";
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
        private void fillOptions()
        {
            //Fill Protocol Type...
            ucDNPSettings.cmbProtocolType.Items.Clear();
            foreach (protocolType ptv in Enum.GetValues(typeof(protocolType)))
            {
                ucDNPSettings.cmbProtocolType.Items.Add(ptv.ToString());
            }
            ucDNPSettings.cmbProtocolType.SelectedIndex = 0;
        }
        public static List<string> getProtocolTypes()
        {
            List<string> lpt = new List<string>();
            foreach (protocolType ptv in Enum.GetValues(typeof(protocolType)))
            {
                lpt.Add(ptv.ToString());
            }
            return lpt;
        }
        public static List<string> getFragmentsizes()
        {
            return Utils.getOpenProPlusHandle().getDataTypeValues("AppFragmentSize");
        }
        public TreeNode getTreeNode()
        {
            return DNP3TreeNode;
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

            rootNode = xmlDoc.CreateElement("DNP3Slave");
            xmlDoc.AppendChild(rootNode);

            if (arrAttributes != null) //Ajay: 31/08/2018
            {
                foreach (string attr in arrAttributes)
                {
                    XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                    attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                    rootNode.Attributes.Append(attrName);
                }
            }
            XmlNode SettingsNode = xmlDoc.CreateElement("DNP3Settings");// Create a new child node
            rootNode.AppendChild(SettingsNode); //add the child node to the root node

            XmlNode importUnsolRespNode = rootNode.OwnerDocument.ImportNode(UnsolResNode.exportXMLnode(), true);
            SettingsNode.AppendChild(importUnsolRespNode);

            XmlNode importObjectValidNode = rootNode.OwnerDocument.ImportNode(ObjectVarNode.exportXMLnode(), true);
            SettingsNode.AppendChild(importObjectValidNode);

            XmlNode importEncryptionNode = rootNode.OwnerDocument.ImportNode(EncryptionVarNode.exportXMLnode(), true);
            SettingsNode.AppendChild(importEncryptionNode);

            XmlNode importDNPSACNode = rootNode.OwnerDocument.ImportNode(DNPSAVarNode.exportXMLnode(), true);
            SettingsNode.AppendChild(importDNPSACNode);

            return rootNode;
        }
      
        private void addListHeaders()
        {
            string strRoutineName = "addListHeaders";
            try
            {
                ucDNPSettings.lvSettingsDetails.Columns.Add("No.", 50, HorizontalAlignment.Left);
                ucDNPSettings.lvSettingsDetails.Columns.Add("Type", 190, HorizontalAlignment.Left);
                ucDNPSettings.lvSettingsDetails.Columns.Add("Count", 60, HorizontalAlignment.Left);
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
                int rowCnt = 0;
                ucDNPSettings.lvSettingsDetails.Items.Clear();
                string[] row1 = { "1", "Unsolicited Response" , UnsolResNode.getCount().ToString()};
                ListViewItem lvItem1 = new ListViewItem(row1);
                if (rowCnt++ % 2 == 0) lvItem1.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                ucDNPSettings.lvSettingsDetails.Items.Add(lvItem1);
                //Namrata: 21/11/2017
                string[] row5 = { "2", "Object Variation",ObjectVarNode.getCount().ToString()};
                ListViewItem lvItem5 = new ListViewItem(row5);
                if (rowCnt++ % 2 == 0) lvItem5.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                ucDNPSettings.lvSettingsDetails.Items.Add(lvItem5);
                string[] row2 = { "3", "Encryption", EncryptionVarNode.getCount().ToString()};
                ListViewItem lvItem2 = new ListViewItem(row2);
                if (rowCnt++ % 2 == 0) lvItem2.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                ucDNPSettings.lvSettingsDetails.Items.Add(lvItem2);
                string[] row3 = { "4", "DNPSA" , DNPSAVarNode.getCount().ToString()};
                ListViewItem lvItem3 = new ListViewItem(row3);
                if (rowCnt++ % 2 == 0) lvItem3.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                ucDNPSettings.lvSettingsDetails.Items.Add(lvItem3);
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("DNP3Slave_")) { refreshList(); return ucDNPSettings; }
            kpArr.RemoveAt(0);


            if (kpArr.ElementAt(0).Contains("UnsolicitedResponse_"))
            {
                return UnsolResNode.getView(kpArr);
            }

            if (kpArr.ElementAt(0).Contains("ObjectVariation_"))
            {
                return ObjectVarNode.getView(kpArr);
            }

            if (kpArr.ElementAt(0).Contains("Encryption_"))
            {
                return EncryptionVarNode.getView(kpArr);
            }

            if (kpArr.ElementAt(0).Contains("DNPSA_"))
            {
                return DNPSAVarNode.getView(kpArr);
            }

            //if(kpArr.ElementAt(0).Contains("USERCONFUser_"))
            //{

            //}
            else
            {
                Utils.WriteLine(VerboseLevel.DEBUG, "***** IED: View for element: {0} not supported!!!", kpArr.ElementAt(0));
            }

            return null;
        }
        private bool isNodeComment = false;
        private string comment = "";
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
       
        public string getSlaveID
        {
            get { return "DNP3Slave_" + SlaveNum; }
        }
        public string SlaveNum
        {
            get { slaveNum = Int32.Parse(ucDNPSettings.txtSlaveNum.Text); return slaveNum.ToString(); }
            set { slaveNum = Int32.Parse(value); ucDNPSettings.txtSlaveNum.Text = value; Globals.SlaveNo = Int32.Parse(value); }
        }
        public string ProtocolType
        {
            get { pt = (protocolType)Enum.Parse(typeof(protocolType), ucDNPSettings.cmbProtocolType.GetItemText(ucDNPSettings.cmbProtocolType.SelectedItem)); return pt.ToString(); }
            set
            {
                try
                {
                    pt = (protocolType)Enum.Parse(typeof(protocolType), value);
                    ucDNPSettings.cmbProtocolType.SelectedIndex = ucDNPSettings.cmbProtocolType.FindStringExact(value);
                   
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", value);
                }
            }
        }
        public string PortNum
        {
            get { portNum = Int32.Parse(ucDNPSettings.txtPortNo.Text); return portNum.ToString(); }
            set { portNum = Int32.Parse(value); ucDNPSettings.txtPortNo.Text = value; }
        }
        public string TcpPort
        {
            get { tcpPort = Int32.Parse(ucDNPSettings.txtTCPPort.Text); return tcpPort.ToString(); }
            set { tcpPort = Int32.Parse(value); ucDNPSettings.txtTCPPort.Text = value; }
        }
        public string RemoteIP
        {
            get { return remoteIP = ucDNPSettings.txtRemoteIP.Text; }
            set { remoteIP = ucDNPSettings.txtRemoteIP.Text = value; }

            //get { if (String.IsNullOrWhiteSpace(remoteIP)) remoteIP = "0.0.0.0"; return remoteIP; }
            //set { remoteIP = value; }
        }
        public string UnitID
        {
            get { unitID = Int32.Parse(ucDNPSettings.txtUnitID.Text); return unitID.ToString(); }
            set { unitID = Int32.Parse(value); ucDNPSettings.txtUnitID.Text = value; }
        }
        public string EventQSize
        {
            get { eventQSize = Int32.Parse(ucDNPSettings.txtEvenyQSize.Text); return eventQSize.ToString(); }
            set { eventQSize = Int32.Parse(value); ucDNPSettings.txtEvenyQSize.Text = value; }
        }
        public string AppFirmwareVersion
        {
            get { return appFirmwareVersion = ucDNPSettings.txtFirmwareVersion.Text; }
            set { appFirmwareVersion = ucDNPSettings.txtFirmwareVersion.Text = value; }
        }
       
        public string DEBUG
        {
            get { debug = Int32.Parse(ucDNPSettings.txtDebug.Text); return debug.ToString(); }
            set { debug = Int32.Parse(value); ucDNPSettings.txtDebug.Text = value; }
          
        }
       
        //Namrata:10/05/2017
        public string LocalIP
        {
            //get { return remoteIP = ucDNPSettings.txtRemoteIP.Text; }
            //set { remoteIP = ucDNPSettings.txtRemoteIP.Text = value; }
            get { if (String.IsNullOrWhiteSpace(localIP)) localIP = "0.0.0.0"; return localIP; }
            set { localIP = value; }
        }
        public string Run
        {
            get { run = ucDNPSettings.chkRun.Checked; return (run == true ? "YES" : "NO"); }
            set
            {
                run = (value.ToLower() == "yes") ? true : false;
                if (run == true) ucDNPSettings.chkRun.Checked = true;
                else ucDNPSettings.chkRun.Checked = false;
            }
        }
        public string MultiFragment
        {
            get { MFAllow = ucDNPSettings.chkbxMFAllow.Checked; return (MFAllow == true ? "YES" : "NO"); }
            set
            {
                MFAllow = (value.ToLower() == "yes") ? true : false;
                if (MFAllow == true) ucDNPSettings.chkbxMFAllow.Checked = true;
                else ucDNPSettings.chkbxMFAllow.Checked = false;
            }
        }
        public string NeedTimeIIN
        {
            get { TimeIIN = ucDNPSettings.ChkBxNeedTIIN.Checked; return (TimeIIN == true ? "YES" : "NO"); }
            set
            {
                TimeIIN = (value.ToLower() == "yes") ? true : false;
                if (TimeIIN == true) ucDNPSettings.ChkBxNeedTIIN.Checked = true;
                else ucDNPSettings.ChkBxNeedTIIN.Checked = false;
            }
        }
        public string NeedRestartIIN
        {
            get { RestartIIN = ucDNPSettings.ChkBxNeedRIIN.Checked; return (RestartIIN == true ? "YES" : "NO"); }
            set
            {
                RestartIIN = (value.ToLower() == "yes") ? true : false;
                if (RestartIIN == true) ucDNPSettings.ChkBxNeedRIIN.Checked = true;
                else ucDNPSettings.ChkBxNeedRIIN.Checked = false;
            }
        }
        public string UnsolicitedResponse
        {
            get { UnsolResp = ucDNPSettings.ChkbxUR.Checked; return (UnsolResp == true ? "ENABLE" : "DISABLE"); }
            set
            {
                UnsolResp = (value.ToLower() == "enable") ? true : false;
                if (UnsolResp == true) ucDNPSettings.ChkbxUR.Checked = true;
                else ucDNPSettings.ChkbxUR.Checked = false;
            }
        }
        public string Encryption
        {
            get { encyption = ucDNPSettings.ChkbxEncryption.Checked; return (encyption == true ? "ENABLE" : "DISABLE"); }
            set
            {
                encyption = (value.ToLower() == "enable") ? true : false;
                if (encyption == true) ucDNPSettings.ChkbxEncryption.Checked = true;
                else ucDNPSettings.ChkbxEncryption.Checked = false;
            }
        }
        public string DNPSA
        {
            get { Dnpsa = ucDNPSettings.chkBxDNPSA.Checked; return (Dnpsa == true ? "ENABLE" : "DISABLE"); }
            set
            {
                Dnpsa = (value.ToLower() == "enable") ? true : false;
                if (Dnpsa == true) ucDNPSettings.chkBxDNPSA.Checked = true;
                else ucDNPSettings.chkBxDNPSA.Checked = false;
            }
        }
        public string SecRemoteIP
        {
            get { return secremoteIP = ucDNPSettings.txtSecRemoteIP.Text; }
            set { secremoteIP = ucDNPSettings.txtSecRemoteIP.Text = value; }
            //get { if (String.IsNullOrWhiteSpace(secremoteIP)) secremoteIP = "0.0.0.0"; return secremoteIP; }
            //set { secremoteIP = value; }
        }
        public string PortName
        {
            get { return portName; }
            set { portName = value; }
        }
        public string AppConfirmTimeout
        {
            get { applCTout = Int32.Parse(ucDNPSettings.txtACTout.Text); return applCTout.ToString(); }
            set { applCTout = Int32.Parse(value); ucDNPSettings.txtACTout.Text = value; }
        }
        public string ClockValidatePeriod
        {
            get { cvp = Int32.Parse(ucDNPSettings.txtCVP.Text); return cvp.ToString(); }
            set { cvp = Int32.Parse(value); ucDNPSettings.txtCVP.Text = value; }
        }
        public string FragmentSize
        {
            get { return fs = ucDNPSettings.txtFS.Text; }
            set { fs = ucDNPSettings.txtFS.Text = value; }
        }
        public string SelectTOut
        {
            get { sTout = Int32.Parse(ucDNPSettings.txtSelectTout.Text); return sTout.ToString(); }
            set { sTout = Int32.Parse(value); ucDNPSettings.txtSelectTout.Text = value; }
        }
        public string DestAdd
        {
            get { DestAddress = Int32.Parse(ucDNPSettings.txtDestAdd.Text); return DestAddress.ToString(); }
            set { DestAddress = Int32.Parse(value); ucDNPSettings.txtDestAdd.Text = value; }
        }

    }
}
