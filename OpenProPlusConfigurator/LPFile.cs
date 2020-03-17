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
    * \brief     <b>DI</b> is a class to store info about Digital Input's
    * \details   This class stores info related to Digital Input's. It retrieves/stores various parameters like ResponseType, etc. 
    * depending on the master type this object belongs to. It also exports the XML node related to this object.
    * 
    */
    public class LPFile
    {
        #region Declarations
        enum LPFileType
        {
            PARAMETER
        };
        private bool isNodeComment = false;
        private string comment = "";
        private LPFileType LpFilenType = LPFileType.PARAMETER;
        private int LPFileNum = -1;
        private string rType = "";
        private int idx = 0;
        private int sidx = 0;
        private string LPFileDesc = "";
        private MasterTypes masterType = MasterTypes.UNKNOWN;
        private int masterNo = -1;
        private int IEDNo = -1;
        private string[] arrAttributes;
        private string[] arrResponseTypes;
        private string[] arrAttributesEvents;
        public List<DI> MODBUSList = new List<DI>();
        ucDIlist ucdi = new ucDIlist();

        private string[] arrDataTypes;
        private string dType = "";

        private int aiNo = 0;
        private int enNo = 0;
        //private string paramCode = ""; //Ajay: 10/10/2018 Commented
        private string name = ""; //Ajay: 10/10/2018

        #endregion Declarations
        private void SetSupportedAttributes()
        {
            string strRoutineName = "SetSupportedAttributes";
            try
            {
                if (masterType == MasterTypes.LoadProfile)
                    //Ajay: 10/10/2018 Commented
                    //arrAttributes = new string[] { "AINo", "ENNo", "PARAMCODE" };
                    arrAttributes = new string[] { "AINo", "ENNo", "Name" }; //Ajay: 10/10/2018
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 10/10/2018 Commented
        //public static List<string> getParamCode(MasterTypes masterType)
        //{
        //    if (masterType == MasterTypes.LoadProfile)
        //        return Utils.getOpenProPlusHandle().getDataTypeValues("LoadProfile_LPFile_ParamCode");
        //    else
        //        return new List<string>();
        //}
        public LPFile(string LPFileName, List<KeyValuePair<string, string>> LpFileData, TreeNode tn, MasterTypes mType, int mNo, int iNo)
        {
            string strRoutineName = "DI";
            try
            {
                masterType = mType;
                masterNo = mNo;
                IEDNo = iNo;
                SetSupportedAttributes();
                try
                {
                    LpFilenType = (LPFileType)Enum.Parse(typeof(LPFileType), LPFileName);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", LPFileName);
                }
                if (LpFileData != null && LpFileData.Count > 0)
                {
                    foreach (KeyValuePair<string, string> lpfilekp in LpFileData)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", lpfilekp.Key, lpfilekp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(lpfilekp.Key) != null)
                            {
                                this.GetType().GetProperty(lpfilekp.Key).SetValue(this, lpfilekp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "LPFile: Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", lpfilekp.Key, lpfilekp.Value);
                        }
                    }
                    Utils.Write(VerboseLevel.DEBUG, "\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public LPFile(XmlNode LPFileNode, MasterTypes mType, int mNo, int iNo, bool imported)
        {
            string StrRoutineName = "LPFile";
            try
            {
                masterType = mType;
                masterNo = mNo;
                IEDNo = iNo;
                SetSupportedAttributes();
                if (LPFileNode.Attributes != null)
                {
                    try
                    {
                        LpFilenType = (LPFileType)Enum.Parse(typeof(LPFileType), LPFileNode.Name);
                    }
                    catch (System.ArgumentException)
                    {
                    }
                    if (masterType == MasterTypes.LoadProfile) 
                    {
                        foreach (XmlAttribute item in LPFileNode.Attributes)
                        {
                            try
                            {
                                if (this.GetType().GetProperty(item.Name) != null)
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
                else if (LPFileNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = LPFileNode.Value;
                }
                if (imported)
                {
                    //Ajay: 10/10/2018 Commented
                    //this.LPFileNo = (Globals.LPFileNo + 1).ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(StrRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void updateAttributes(List<KeyValuePair<string, string>> diData)
        {
            if (diData != null && diData.Count > 0)
            {
                foreach (KeyValuePair<string, string> dikp in diData)
                {
                    try
                    {
                        if (this.GetType().GetProperty(dikp.Key) != null)
                        {
                            this.GetType().GetProperty(dikp.Key).SetValue(this, dikp.Value);
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                    }
                }
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
            rootNode = xmlDoc.CreateElement(LpFilenType.ToString());
            xmlDoc.AppendChild(rootNode);

            //Create New Element in XML
            foreach (string attr in arrAttributes)
            {
                XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                rootNode.Attributes.Append(attrName);
            }
            return rootNode;
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public string LPFileNo
        {
            get { return LPFileNum.ToString(); }
            set { LPFileNum = Int32.Parse(value); Globals.DINo = Int32.Parse(value); }
        }
        public string ResponseType
        {
            get { return rType; }
            set
            {
                rType = value;
            }
        }
        public string Index
        {
            get { return idx.ToString(); }
            //Ajay: 31/07/2018
            set
            {
                int i = 0;
                Int32.TryParse(value, out i);
                idx = i;
                Globals.DIIndex = i;
            }
            //set { idx = Int32.Parse(value); Globals.DIIndex = Int32.Parse(value); } //Ajay: 31/07/2018
        }
        public string SubIndex
        {
            get { return sidx.ToString(); }
            //Ajay: 31/07/2018
            set
            {
                int i = 0;
                Int32.TryParse(value, out i);
                sidx = i;
            }
            //set { sidx = Int32.Parse(value); }  //Ajay: 31/07/2018
        }
        //Ajay: 10/10/2018 Commented
        //public string PARAMCODE
        //{
        //    get { return paramCode; }
        //    set { paramCode = value; }
        //}

        //Ajay: 10/10/2018 
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string AINo
        {
            get { return aiNo.ToString(); }
            set
            {
                int i = 0;
                Int32.TryParse(value, out i);
                aiNo = i;
            }
        }
        public string ENNo
        {
            get { return enNo.ToString(); }
            set
            {
                int i = 0;
                Int32.TryParse(value, out i);
                enNo = i;
            }
        }

    }
}
