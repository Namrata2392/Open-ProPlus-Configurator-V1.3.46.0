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
    public class SPORTSlave
    {
        #region Declarations
        enum SPORTType
        {
           SPORTSlave,
        };
        private bool isNodeComment = false;
        private string comment = "";
        private SPORTType sType = SPORTType.SPORTSlave;
        private int slaveNum = -1;
        private string portNum = "";
        private int asduAddress = -1;
        private string ioaSz = "";
        private string sportType = "";
        private int eventQSize;
        private int debug;
        private string appFirmwareVersion;
        private bool run = true;
        //Ajay: 31/08/2018 Event added
        private string[] arrAttributes = { "SlaveNum", "PortNum","ASDUAddress","SportType","IOASize","EventQSize",
                                            /*"Event",*/ "DEBUG","AppFirmwareVersion","Run" };   //Ajay: 22/09/2018 Event removed
        private string[] arrASDUSizes;
        private string[] arrIOASizes;
        private string[] arrPortNum;
        private bool _event = false; //Ajay: 31/08/2018
        #endregion Declarations

        //private void SetSupportedASDUSizes()
        //{
        //    string strRoutineName = "SetSupportedASDUSizes";
        //    try
        //    {
        //        arrASDUSizes = Utils.getOpenProPlusHandle().getDataTypeValues("TwoByte").ToArray();
        //    if (arrASDUSizes.Length > 0) asSize = arrASDUSizes[0];
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        private void SetSupportedIOASizes()
        {
            string strRoutineName = "SetSupportedIOASizes";
            try
            {
                arrIOASizes = Utils.getOpenProPlusHandle().getDataTypeValues("ThreeByte").ToArray();
                if (arrIOASizes.Length > 0) ioaSz = arrIOASizes[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}
        private void SetSupportedPortNumber()
        {
            string strRoutineName = "SetSupportedPortNumber";
            try
            {
                arrPortNum = Utils.getOpenProPlusHandle().getDataTypeValues("UartNum").ToArray();
            if (arrPortNum.Length > 0) portNum = arrPortNum[0];
        }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}
        public static List<string> getASDUsizes()
        {
            return Utils.getOpenProPlusHandle().getDataTypeValues("TwoByte");
        }
        public static List<string> getIOAsizes()
        {
            return Utils.getOpenProPlusHandle().getDataTypeValues("ThreeByte");
        }
        //Ajay: 27/08/2018
        public static List<string> getSportTypes()
        {
            return Utils.getOpenProPlusHandle().getDataTypeValues("SportSlave_Type");
        }
        public static List<string> getPortNo()
        {
            return Utils.getOpenProPlusHandle().getDataTypeValues("UartNum");
        }
        public SPORTSlave(string sSportName , List<KeyValuePair<string,string>>sSportData,TreeNode tn)
        {
            string strRoutineName = "SPORTSlave";
            try
            {
                //SetSupportedASDUSizes();
                SetSupportedIOASizes();
                SetSupportedPortNumber();
            try
            {
                sType = (SPORTType)Enum.Parse(typeof(SPORTType), sSportName);
            }
            catch(System.ArgumentException)
            {
                Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!",sSportName);
            }
            //Parse n store values...
            if (sSportData != null && sSportData.Count > 0)
            {
                foreach (KeyValuePair<string, string> sSportkp in sSportData)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", sSportkp.Key, sSportkp.Value);
                    try
                    {
                        if (this.GetType().GetProperty(sSportkp.Key) != null)
                        {
                            this.GetType().GetProperty(sSportkp.Key).SetValue(this, sSportkp.Value);
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value:{1}", sSportkp.Key, sSportkp.Value);
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
        public SPORTSlave(XmlNode sNode, TreeNode tn)
        {
            string strRoutineName = "SPORTSlave";
            try
            {
                //SetSupportedASDUSizes();
                SetSupportedIOASizes();
                SetSupportedPortNumber();
                //IMP: Use tn when we want to further add child nodes to 'IEC104'. Check if it's null...
                //Parse n store values...
                Utils.WriteLine(VerboseLevel.DEBUG, "sNode name: '{0}'", sNode.Name);
                if (sNode.Attributes != null)
                {
                    //First set the root element value...
                    try
                    {
                        sType = (SPORTType)Enum.Parse(typeof(SPORTType), sNode.Name);
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
    //Not Used: uciec.lblMain.Text += slaveNum;
}
        public void updateAttributes(List<KeyValuePair<string, string>> sSportData)
        {
            string strRoutineName = "updateAttributes";
            try
            {
                if (sSportData != null && sSportData.Count > 0)
            {
                foreach (KeyValuePair<string, string> sSportkp in sSportData)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", sSportkp.Key, sSportkp.Value);
                    try
                    {
                        if (this.GetType().GetProperty(sSportkp.Key) != null)
                        {
                            this.GetType().GetProperty(sSportkp.Key).SetValue(this, sSportkp.Value);
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value:{1}", sSportkp.Key, sSportkp.Value);
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
        public string exportINI()
        {
            return this.SlaveNum;
        }
        public string getSlaveID
        {
            get { return "SPORTSlave_" + SlaveNum; }
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
        public string PortNum
        {
            get { return portNum; }
            set
            {
                portNum = value;
            }
        }
        public string ASDUAddress
        {
            get { return asduAddress.ToString(); }
            set { asduAddress = Int32.Parse(value); }
        }
        public string SportType
        {
            get { return sportType; }
            set { sportType = value; }
        }
        public string IOASize
        {
            get
            {
                return ioaSz;
            }
            set
            {
                ioaSz = value;
            }
        }
        public string EventQSize
        {
            get { return eventQSize.ToString(); }
            set { eventQSize = Int32.Parse(value); }
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
        public string Run
        {
            get { return (run == true ? "YES" : "NO"); }
            set
            {
                run = (value.ToLower() == "yes") ? true : false;
            }
        }
        //Ajay: 31/08/2018
        public string Event
        {
            get { return (_event == true ? "YES" : "NO"); }
            set
            {
                _event = (value.ToLower() == "yes") ? true : false;
            }
        }


    }
}
