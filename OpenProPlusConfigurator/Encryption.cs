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
    public class Encryption
    {
        enum ENC
        {
            Encryption
        };
        private ENC ainType = ENC.Encryption;
        private string cipherSuite;
        private string certificate;
        private string privateKey;
        private string ca; private string dHParameter;
        private int slaveNum = -1; private bool isNodeComment = false;
        public static string[] arrAttributes = new string[] { "CipherSuite", "Certificate", "PrivateKey", "CA", "DHParameter"};
        private SlaveTypes slaveType = SlaveTypes.UNKNOWN;
        private int slaveNo = -1;
        public Encryption(string aiName, List<KeyValuePair<string, string>> aiData, TreeNode tn, SlaveTypes sType, int sNo)
        {
            string strRoutineName = "Encryption";
            try
            {
                slaveType = sType;
                slaveNo = sNo;
                try
                {
                    ainType = (ENC)Enum.Parse(typeof(ENC), aiName);
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
        public Encryption(XmlNode aiNode, SlaveTypes sType, int sNo, bool imported)
        {
            string strRoutineName = "Encryption";
            try
            {
                slaveType = sType;
                slaveNo = sNo;
                if (aiNode.Attributes != null)
                {
                    try
                    {
                        ainType = (ENC)Enum.Parse(typeof(ENC), aiNode.Name);
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
            string strRoutineName = "Encryption:updateAttributes";
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
                rootNode = xmlDoc.CreateComment("Encryption");
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
        public string CipherSuite
        {
            get { return cipherSuite; }
            set
            {
                cipherSuite = value;
            }
        }
        public string CA
        {
            get { return ca; }
            set
            {
                ca = value;
            }
        }
        public string Certificate
        {
            get { return certificate; }
            set
            {
                certificate = value;
            }
        }
        public string PrivateKey
        {
            get { return privateKey; }
            set
            {
                privateKey = value;
            }
        }
        public string DHParameter
        {
            get { return dHParameter; }
            set
            {
                dHParameter = value;
            }
        }
       
    }
}
