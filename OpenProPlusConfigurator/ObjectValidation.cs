
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
    public class ObjectValidation
    {
        enum OV
        {
            ObjectVariation
        };
        private OV ainType = OV.ObjectVariation;
        private int bi = -1;
        private int bie = -1;
        private int di = -1;
        private int die = -1;
        private int bos = -1;
        private int bc = -1;
        private int bce = -1;
        private int fc = -1;
        private int fce = -1;
        private int ai = -1;
        private int aie = -1;
        private int aos = -1;
       private int slaveNum = -1; private bool isNodeComment = false;
        public static string[] arrAttributes = new string[] { "BI", "BIE", "DI", "DIE", "BOS", "BC", "BCE", "FC", "FCE", "AI", "AIE", "AOS" };
        private SlaveTypes slaveType = SlaveTypes.UNKNOWN;
        private int slaveNo = -1;
        public ObjectValidation(string aiName, List<KeyValuePair<string, string>> aiData, TreeNode tn, SlaveTypes sType, int sNo)
        {
            string strRoutineName = "UnsolResponse";
            try
            {
                slaveType = sType;
                slaveNo = sNo;
                try
                {
                    ainType = (OV)Enum.Parse(typeof(OV), aiName);
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
        public ObjectValidation(XmlNode aiNode, SlaveTypes sType, int sNo, bool imported)
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
                        ainType = (OV)Enum.Parse(typeof(OV), aiNode.Name);
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
                rootNode = xmlDoc.CreateComment("ObjectVariation");
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
        public string BI
        {
            get { return bi.ToString(); }
            set
            {
                bi = Int32.Parse(value);
            }
        }
        public string BIE
        {
            get { return bie.ToString(); }
            set
            {
                bie = Int32.Parse(value);
            }
        }
        public string DI    
        {
            get { return di.ToString(); }
            set
            {
                di = Int32.Parse(value);
            }
        }
        public string DIE
        {
            get { return die.ToString(); }
            set
            {
                die = Int32.Parse(value);
            }
        }
        public string BOS
        {
            get { return bos.ToString(); }
            set
            {
                bos = Int32.Parse(value);
            }
        }
        public string BC
        {
            get { return bc.ToString(); }
            set
            {
                bc = Int32.Parse(value);
            }
        }
        public string BCE
        {
            get { return bce.ToString(); }
            set
            {
                bce = Int32.Parse(value);
            }
        }
        public string FC
        {
            get { return fc.ToString(); }
            set
            {
                fc = Int32.Parse(value);
            }
        }
        public string FCE
        {
            get { return fce.ToString(); }
            set
            {
                fce = Int32.Parse(value);
            }
        }
        public string AI
        {
            get { return ai.ToString(); }
            set
            {
                ai = Int32.Parse(value);
            }
        }
        public string AIE
        {
            get { return aie.ToString(); }
            set
            {
                aie = Int32.Parse(value);
            }
        }
        public string AOS
        {
            get { return aos.ToString(); }
            set
            {
                aos = Int32.Parse(value);
            }
        }

    }
}
