//Namrata:01/04/2019
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

namespace OpenProPlusConfigurator
{
   public class MQTTSlave
    {
        #region Declaration
        enum slaveType
        {
            MQTTSlave
        };
        enum ProtocolType
        {
            RTU,
            ASCII,
            TCP
        };
        private int debug;
        private string appFirmwareVersion;
        private bool isNodeComment = false;
        private string comment = "";
        private slaveType sType = slaveType.MQTTSlave;
        private int slaveNum = -1;
        private string topic;
        private string brokeraddress;
        private string username;
        private string password;
        private string certificate;
        private string tlslevel;
        private string output = "";
        private string broker;
        private bool run = true;
        private string portnum;//Namrata:23/04/2019
        private string qos = "";//Namrata:23/04/2019
        private string scantime;
        private string[] arrAttributes = { "SlaveNum", "UserName", "Password", "Topic", "Broker","BrokerAddress","ScanTime", "Certificate","TLSLevel","Output","PortNum","QoS", "DEBUG", "AppFirmwareVersion","Run" };
        #endregion Declaration

        public static List<string> getOutputTypes()
        {
            return Utils.getOpenProPlusHandle().getDataTypeValues("MQTTSlave_OutputType");
        }
        public static List<string> getQoS()
        {
            return Utils.getOpenProPlusHandle().getDataTypeValues("MQTTSlave_QoS");
        }
        public static List<string> getBroker()
        {
            return Utils.getOpenProPlusHandle().getDataTypeValues("MQTTSlave_Broker");
        }
        public MQTTSlave(string mbsName, List<KeyValuePair<string, string>> mbsData, TreeNode tn)
        {
            string strRoutineName = "MQTTSlave";
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public MQTTSlave(XmlNode sNode, TreeNode tn)
        {
            string strRoutineName = "MQTTSlave";
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
        public void updateAttributes(List<KeyValuePair<string, string>> mbsData)
        {
            string strRoutineName = "updateAttributes";
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
            rootNode = xmlDoc.CreateElement(sType.ToString());
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
            get { return "MQTTSlave_" + SlaveNum; }
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

        public string Topic
        {
            get {return topic; }
            set {topic = value; }
        }
        public string BrokerAddress
        {
            get { return brokeraddress; }
            set { brokeraddress = value; }
        }
        public string UserName
        {
            get { return username; }
            set { username = value; }
        }
        public string Password
        {
            get {return password; }
            set {password = value;}
        }
        public string Certificate
        {
            get { return certificate; }
            set { certificate = value; }
        }
        public string TLSLevel
        {
            get { return tlslevel; }
            set { tlslevel = value;}
        }
        public string Output
        {
            get { return output; }
            set { output = value; }
        }
        public string QoS
        {
            get { return qos; }
            set { qos = value; }
        }
        public string Run
        {
            get { return (run == true ? "YES" : "NO"); }
            set { run = (value.ToLower() == "yes") ? true : false; }
        }
        public string Broker
        {
            get { return broker; }
            set { broker = value; }
        }
        public string ScanTime
        {
            get { return scantime; }
            set { scantime = value; }
        }
        public string PortNum
        {
            get { return portnum; }
            set { portnum = value; }
        }
        public string DEBUG
        {
            get { return debug.ToString(); }
            set { debug = Int32.Parse(value); }
        }
        public string AppFirmwareVersion
        {
            get { return appFirmwareVersion; }
            set { appFirmwareVersion = value; }
        }
    }
}
