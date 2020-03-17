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
    /**
    * \brief     <b>DD</b> is a class to store info about derived data input
    * \details   This class stores info related to derived data input. It retrieves/stores 
    * various parameters like DINo, Operation, etc. It also exports the XML node 
    * related to this object.
    * 
    */
    public class DD
    {
        #region Declaration
        private bool isNodeComment = false;
        private string comment = "";
        private bool isReindexedDINo1 = false;
        private bool isReindexedDINo2 = false;
        //Ajay: 17/11/2018 isReindexedDINo3 to isReindexedDINo10 added
        private bool isReindexedDINo3 = false;
        private bool isReindexedDINo4 = false;
        private bool isReindexedDINo5 = false;
        private bool isReindexedDINo6 = false;
        private bool isReindexedDINo7 = false;
        private bool isReindexedDINo8 = false;
        private bool isReindexedDINo9 = false;
        private bool isReindexedDINo10 = false;

        private string rnName = "";
        private int ddIndex = -1;
        private int diNo1 = 0;
        private int diNo2 = 0;
        //Ajay: 17/11/2018 DINo3 to DINo10 added
        private int diNo3 = 0;
        private int diNo4 = 0;
        private int diNo5 = 0;
        private int diNo6 = 0;
        private int diNo7 = 0;
        private int diNo8 = 0;
        private int diNo9 = 0;
        private int diNo10 = 0;  //Ajay: 17/11/2018
        private string opr = "";
        private int delayms = 5;
        //Ajay: 17/11/2018 DINo3 to DINo10 added
        //private string[] arrAttributes = { "DDIndex", "DINo1", "DINo2", "Operation", "DelayMS" };
        private string[] arrAttributes = { "DDIndex", "DINo1", "DINo2", "DINo3", "DINo4", "DINo5", "DINo6", "DINo7", "DINo8", "DINo9", "DINo10", "Operation", "DelayMS" };
        private string[] arrOperations;
        #endregion Declaration
        private void SetSupportedOperations()
        {
            string strRoutineName = "SetSupportedOperations";
            try
            {
                arrOperations = Utils.getOpenProPlusHandle().getDataTypeValues("Operation_Logical").ToArray();
                if (arrOperations.Length > 0) opr = arrOperations[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static List<string> getOperations()
        {
            return Utils.getOpenProPlusHandle().getDataTypeValues("Operation_Logical");
        }
        public DD(string ddName, List<KeyValuePair<string, string>> ddData)
        {
            string strRoutineName = "DD";
            try
            {
                SetSupportedOperations();
                rnName = ddName;
                //Parse n store values...
                if (ddData != null && ddData.Count > 0)
                {
                    foreach (KeyValuePair<string, string> ddkp in ddData)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", ddkp.Key, ddkp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(ddkp.Key) != null) //Ajay: 03/07/2018
                            {
                                this.GetType().GetProperty(ddkp.Key).SetValue(this, ddkp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", ddkp.Key, ddkp.Value);
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
        public DD(XmlNode dNode)
        {
            string strRoutineName = "DD";
            try
            {
                SetSupportedOperations();
                //Parse n store values...
                Utils.WriteLine(VerboseLevel.DEBUG, "dNode name: '{0}' {1}", dNode.Name, dNode.Value);
                rnName = dNode.Name;
                if (dNode.Attributes != null)
                {
                    foreach (XmlAttribute item in dNode.Attributes)
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
                    Utils.Write(VerboseLevel.DEBUG, "\n");
                }
                else if (dNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = dNode.Value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void updateAttributes(List<KeyValuePair<string, string>> ddData)
        {
            string strRoutineName = "updateAttributes";
            try
            {
                if (ddData != null && ddData.Count > 0)
                {
                    foreach (KeyValuePair<string, string> ddkp in ddData)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", ddkp.Key, ddkp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(ddkp.Key) != null) //Ajay: 03/07/2018
                            {
                                this.GetType().GetProperty(ddkp.Key).SetValue(this, ddkp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", ddkp.Key, ddkp.Value);
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
            rootNode = xmlDoc.CreateElement(rnName);
            xmlDoc.AppendChild(rootNode);
            foreach (string attr in arrAttributes)
            {
                XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                rootNode.Attributes.Append(attrName);
            }
            return rootNode;
        }
        public bool IsReindexedDINo1
        {
            get { return isReindexedDINo1; }
            set { isReindexedDINo1 = value; }
        }
        public bool IsReindexedDINo2
        {
            get { return isReindexedDINo2; }
            set { isReindexedDINo2 = value; }
        }
        //Ajay: 17/11/2018
        public bool IsReindexedDINo3
        {
            get { return isReindexedDINo3; }
            set { isReindexedDINo3 = value; }
        }
        //Ajay: 17/11/2018
        public bool IsReindexedDINo4
        {
            get { return isReindexedDINo4; }
            set { isReindexedDINo4 = value; }
        }
        //Ajay: 17/11/2018
        public bool IsReindexedDINo5
        {
            get { return isReindexedDINo5; }
            set { isReindexedDINo5 = value; }
        }
        //Ajay: 17/11/2018
        public bool IsReindexedDINo6
        {
            get { return isReindexedDINo6; }
            set { isReindexedDINo6 = value; }
        }
        //Ajay: 17/11/2018
        public bool IsReindexedDINo7
        {
            get { return isReindexedDINo7; }
            set { isReindexedDINo7 = value; }
        }
        //Ajay: 17/11/2018
        public bool IsReindexedDINo8
        {
            get { return isReindexedDINo8; }
            set { isReindexedDINo8 = value; }
        }
        //Ajay: 17/11/2018
        public bool IsReindexedDINo9
        {
            get { return isReindexedDINo9; }
            set { isReindexedDINo9 = value; }
        }
        //Ajay: 17/11/2018
        public bool IsReindexedDINo10
        {
            get { return isReindexedDINo10; }
            set { isReindexedDINo10 = value; }
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public string DDIndex
        {
            get { return ddIndex.ToString(); }
            set { ddIndex = Int32.Parse(value); Globals.DDNo = Int32.Parse(value); }
        }
        public string DINo1
        {
            get { return diNo1.ToString(); }
            set { diNo1 = Int32.Parse(value); }
        }
        public string DINo2
        {
            get { return diNo2.ToString(); }
            set { diNo2 = Int32.Parse(value); }
        }
        //Ajay: 17/11/2018
        public string DINo3
        {
            get { return diNo3.ToString(); }
            set
            {
                int i = 0;
                Int32.TryParse(value, out i);
                diNo3 = i;
            }
        }
        //Ajay: 17/11/2018
        public string DINo4
        {
            get { return diNo4.ToString(); }
            set
            {
                int i = 0;
                Int32.TryParse(value, out i);
                diNo4 = i;
            }
        }
        //Ajay: 17/11/2018
        public string DINo5
        {
            get { return diNo5.ToString(); }
            set
            {
                int i = 0;
                Int32.TryParse(value, out i);
                diNo5 = i;
            }
        }
        //Ajay: 17/11/2018
        public string DINo6
        {
            get { return diNo6.ToString(); }
            set
            {
                int i = 0;
                Int32.TryParse(value, out i);
                diNo6 = i;
            }
        }
        //Ajay: 17/11/2018
        public string DINo7
        {
            get { return diNo7.ToString(); }
            set
            {
                int i = 0;
                Int32.TryParse(value, out i);
                diNo7 = i;
            }
        }
        //Ajay: 17/11/2018
        public string DINo8
        {
            get { return diNo8.ToString(); }
            set
            {
                int i = 0;
                Int32.TryParse(value, out i);
                diNo8 = i;
            }
        }
        //Ajay: 17/11/2018
        public string DINo9
        {
            get { return diNo9.ToString(); }
            set
            {
                int i = 0;
                Int32.TryParse(value, out i);
                diNo9 = i;
            }
        }
        //Ajay: 17/11/2018
        public string DINo10
        {
            get { return diNo10.ToString(); }
            set
            {
                int i = 0;
                Int32.TryParse(value, out i);
                diNo10 = i;
            }
        }
        public string Operation
        {
            get { return opr; }
            set
            {
                opr = value;
            }
        }
        public string DelayMS
        {
            get { return delayms.ToString(); }
            set { delayms = Int32.Parse(value); }
        }
    }
}
