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
    public class UFSMS
    {
        enum slaveType
        {
            SMSSlave
        };
        enum SMSUser
        {
            SMSUser
        };
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        enum UFSMSType
        {
            UFSMS
        };
        private int eventQSize;
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        private slaveType sType = slaveType.SMSSlave;
        private int sNo = -1;
        private SMSUser UfsmsType1 = SMSUser.SMSUser;
        private UFSMSType UfsmsType = UFSMSType.UFSMS;
        private bool isNodeComment = false;
        private string comment = "";
        private int slaveNum = -1;
        private string modem = "";
        private bool run = true;
        private bool enableEncryption = true;

        private string Mobileno = "";
        private bool GrantforControl = true;
        private string[] arrAttributes;
        List<SMSUser> SmsuserList = new List<SMSUser>();

        private void SetSupportedAttributes()
        {
            string strRoutineName = "UFSMS: SetSupportedAttributes";
            try
            {
                if (sType == slaveType.SMSSlave)
                    arrAttributes = new string[] { "MobileNo", "GrantForControl"};
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public bool ToBoolean(string value)
        {
            switch (value.ToUpper())
            {
                case "YES":
                    return true;
                case "NO":
                    return false;
                default:
                    throw new InvalidCastException("You can't cast that value to a bool!");
            }
        }

        public UFSMS(string UfsmsName, List<KeyValuePair<string, string>> s101Data, TreeNode tn)
        {
            string strRoutineName = "UFSMS";
            try
            {
                SetSupportedAttributes();
                try
                {
                    UfsmsType = (UFSMSType)Enum.Parse(typeof(UFSMSType), UfsmsName);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", UfsmsName);
                }
                //Parse n store values...
                if (s101Data != null && s101Data.Count > 0)
                {
                    foreach (KeyValuePair<string, string> s104kp in s101Data)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", s104kp.Key, s104kp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(s104kp.Key) != null) //Ajay: 03/07/2018
                            {
                                this.GetType().GetProperty(s104kp.Key).SetValue(this, s104kp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value:{1}", s104kp.Key, s104kp.Value);
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
        int iChild = 0;
        public UFSMS(XmlNode sNode, TreeNode tn)
        {
            string strRoutineName = "UFSMS";
            try
            {
                SetSupportedAttributes();
                if (sNode.Attributes != null)
                {
                    //First set the root element value...
                    try
                    {
                        UfsmsType = (UFSMSType)Enum.Parse(typeof(UFSMSType), sNode.Name);
                    }
                    catch (System.ArgumentException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", sNode.Name);
                    }
                    if (sNode.HasChildNodes)
                    {
                     
                        //for (int iChild = 0; i < sNode.ChildNodes.Count; iChild++)
                        //{
                            if (sNode.ChildNodes[iChild].Name == "SMSUser")
                            {
                                foreach (XmlAttribute xmlattribute in sNode.ChildNodes[iChild].Attributes)
                                {
                                    try
                                    {
                                        if (this.GetType().GetProperty(xmlattribute.Name) != null) //Ajay: 03/07/2018
                                        {
                                            this.GetType().GetProperty(xmlattribute.Name).SetValue(this, xmlattribute.Value);
                                        }
                                    }
                                    catch (System.NullReferenceException)
                                    {
                                    }
                                }
                            iChild++;
                            }
                        }
                    //}
                    
                    Utils.Write(VerboseLevel.DEBUG, "\n");
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
            //Not Used: uciec.lblMain.Text += slaveNum;
        }
        public void updateAttributes(List<KeyValuePair<string, string>> s101Data)
        {
            string strRoutineName = "UFSMS: updateAttributes";
            try
            {
                if (s101Data != null && s101Data.Count > 0)
                {
                    foreach (KeyValuePair<string, string> s101kp in s101Data)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", s101kp.Key, s101kp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(s101kp.Key) != null) //Ajay: 03/07/2018
                            {
                                this.GetType().GetProperty(s101kp.Key).SetValue(this, s101kp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value:{1}", s101kp.Key, s101kp.Value);
                        }
                    }
                    Utils.Write(VerboseLevel.DEBUG, "\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //Not Used: uciec.lblMain.Text += slaveNum;
        }
        public Control getView(List<string> kpArr)
        {
            return null;
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
            rootNode = xmlDoc.CreateElement(UfsmsType1.ToString());
            xmlDoc.AppendChild(rootNode);
            foreach (string attr in arrAttributes)
            {
                XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                rootNode.Attributes.Append(attrName);
            }
            return rootNode;
        }
        
        public string getSlaveID
        {
            get { return "SMSSlave_" + SlaveNum; }
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public string SlaveNum
        {
            get { return slaveNum.ToString(); }
            set { slaveNum = Int32.Parse(value); Globals.SlaveNo = Int32.Parse(value); }
        }
        public string MobileNo
        {
            get { return Mobileno; }
            set { Mobileno = value; }
        }
        public string GrantForControl
        {
            get { return (GrantforControl == true ? "YES" : "NO"); }
            set { GrantforControl = (value.ToLower() == "yes") ? true : false; }
        }
    }
}
