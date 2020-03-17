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
    public class UnsolResponse
    {
        enum UR
        {
            UnSolicitedResponse
        };
        private UR ainType = UR.UnSolicitedResponse;
        private int DA = -1;
        private int C1ME = -1;
        private int C1MD = -1;
        private int C2ME = -1;
        private int C2MD = -1;
        private int C3ME = -1;
        private int C3MD = -1;
        private int MR = -1;
        private int RD = -1;
        private int ORD = -1;
        private bool ANull = true; private int slaveNum = -1; private bool isNodeComment = false;
        public static string[] arrAttributes = new string[] { "DestAdd", "C1MaxEvents", "C1MaxDelay", "C2MaxEvents", "C2MaxDelay", "C3MaxEvents", "C3MaxDelay", "MaxRetry", "RetryDelay", "OfflineRetryDelay", "AllowNull" };
        private SlaveTypes slaveType = SlaveTypes.UNKNOWN;
        private int slaveNo = -1;
        public UnsolResponse(string aiName, List<KeyValuePair<string, string>> aiData, TreeNode tn, SlaveTypes sType, int sNo)
        {
            string strRoutineName = "UnsolResponse";
            try
            {
                slaveType = sType;
                slaveNo = sNo;
                
                try
                {
                   ainType = (UR)Enum.Parse(typeof(UR), aiName);
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public UnsolResponse(XmlNode aiNode, SlaveTypes sType, int sNo, bool imported)
        {
            string strRoutineName = "UnsolResponse";
            try
            {
                slaveType = sType;
                slaveNo = sNo;
                if (aiNode.Attributes != null)
                {
                    try
                    {
                        ainType = (UR)Enum.Parse(typeof(UR), aiNode.Name);
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
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void updateAttributes(List<KeyValuePair<string, string>> aiData)
        {
            string strRoutineName = "AI:updateAttributes";
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
        public XmlNode exportXMLnode()
        {
           



            XmlDocument xmlDoc = new XmlDocument();
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
            XmlNode rootNode = null;
            if (isNodeComment)
            {
                rootNode = xmlDoc.CreateComment("UnSolicitedResponse");
                xmlDoc.AppendChild(rootNode);
                return rootNode;
            }
           

            return rootNode;
        }
        public string SlaveNum
        {
            get { return slaveNum.ToString(); }
            set { slaveNum = Int32.Parse(value); Globals.SlaveNo = Int32.Parse(value); }
        }
        
        public string getSlaveID
        {
            get { return "DNP3Slave_" + SlaveNum; }
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public string DestAdd
        {
            get { return DA.ToString(); }
            set
            {
                DA = Int32.Parse(value);
            }
        }
        public string C1MaxEvents
        {
            get { return C1ME.ToString(); }
            set
            {
                C1ME = Int32.Parse(value);
            }
        }
        public string C1MaxDelay
        {
            get { return C1MD.ToString(); }
            set
            {
                C1MD = Int32.Parse(value);
            }
        }
        public string C2MaxEvents
        {
            get { return C2ME.ToString(); }
            set
            {
                C2ME = Int32.Parse(value);
            }
        }
        public string C2MaxDelay
        {
            get { return C3MD.ToString(); }
            set
            {
                C3MD = Int32.Parse(value);
            }
        }
        public string C3MaxEvents
        {
            get { return C3ME.ToString(); }
            set
            {
                C3ME = Int32.Parse(value);
            }
        }
        public string C3MaxDelay
        {
            get { return C3MD.ToString(); }
            set
            {
                C3MD = Int32.Parse(value);
            }
        }
        public string MaxRetry
        {
            get { return MR.ToString(); }
            set
            {
                MR = Int32.Parse(value);
            }
        }
        public string RetryDelay
        {
            get { return RD.ToString(); }
            set
            {
                RD = Int32.Parse(value);
            }
        }
        public string OfflineRetryDelay
        {
            get { return ORD.ToString(); }
            set
            {
                ORD = Int32.Parse(value);
            }
        }
        public string AllowNull
        {
            get { return (ANull == true ? "ENABLE" : "DISABLE"); }
            set { ANull = (value.ToLower() == "enable") ? true : false; }
        }
    }
}
