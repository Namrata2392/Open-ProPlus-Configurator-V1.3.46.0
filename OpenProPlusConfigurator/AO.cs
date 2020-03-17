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
    #region Declaration
    public class AO
    {
        enum aoType
        {
            AO
        };
        ucMasterIEC103 uciec = new ucMasterIEC103();
        private bool isNodeComment = false;
        private string comment = "";
        private aoType ainType = aoType.AO;
        private int aoNum = -1;
        private string rType = "";
        private int idx = -1;
        private int sidx = -1;
        private string dType = "";
        private double mult = 1;
        private double cnst = 0;
        private string aiDesc = "";
        private MasterTypes masterType = MasterTypes.UNKNOWN;
        private int masterNo = -1;
        public int IEDNo = -1;
        private string[] arrAttributes;
        private string[] arrResponseTypes;
        private string[] arrDataTypes;
        //Namrata: 21/04/2018
        private string Iec61850Index = "";
        private string Iec61850ResponseType = "";
        private string Iec61850Iedname = "";
        private string fc = "";
        private bool events = true; //Ajay: 26/07/2018
        #endregion Declaration
        private void SetSupportedAttributes()
        {
            string strRoutineName = "AO:SetSupportedAttributes";
            try
            {
                if (masterType == MasterTypes.IEC103)
                    arrAttributes = new string[] { "AONo", "ResponseType", "Index", "SubIndex", "DataType", "Multiplier", "Constant", "Description" };
                else if (masterType == MasterTypes.MODBUS)
                    arrAttributes = new string[] { "AONo", "ResponseType", "Index", "SubIndex", "DataType", "Multiplier", "Constant", "Description" };
                else if (masterType == MasterTypes.Virtual)
                    arrAttributes = new string[] { "AONo", "ResponseType", "Index", "DataType", "Multiplier", "Constant", "Description" };
                //Namarta:3/6/2017
                else if (masterType == MasterTypes.ADR)
                    arrAttributes = new string[] { "AONo", "ResponseType", "Index", "SubIndex", "DataType", "Multiplier", "Constant", "Description" };
                //Namrata:7/7/2017
                else if (masterType == MasterTypes.IEC101)
                    arrAttributes = new string[] { "AONo", "ResponseType", "Index", "SubIndex", "DataType", "Multiplier", "Constant", "Description" };
                else if (masterType == MasterTypes.IEC61850Client)
                    arrAttributes = new string[] { "AONo" };
                //Namrata:22/05/2018
                else if (masterType == MasterTypes.IEC104)
                arrAttributes = new string[] { "AONo", "ResponseType", "Index", "SubIndex", "DataType", "Multiplier", "Constant", "Description" }; 
                //Namrata:23/05/2018
                else if (masterType == MasterTypes.SPORT)
                arrAttributes = new string[] { "AONo", "ResponseType", "Index", "SubIndex", "DataType", "Multiplier", "Constant", "Description" };
                //Ajay: 31/07/2018
                else if (masterType == MasterTypes.LoadProfile)
                    arrAttributes = new string[] { "AONo", "Description" };
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void SetSupportedResponseTypes()
        {
            string strRoutineName = "AO: SetSupportedResponseTypes";
            try
            {
                if (masterType == MasterTypes.IEC103)
                    arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC103_AO_ResponseType").ToArray();
                else if (masterType == MasterTypes.MODBUS)
                    arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("MODBUS_AO_ResponseType").ToArray();
                else if (masterType == MasterTypes.Virtual)
                    arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("Virtual_AO_ResponseType").ToArray();
                //Namrata:7/7/2017
                else if (masterType == MasterTypes.IEC101)
                    arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_AO_ResponseType").ToArray();
                else if (masterType == MasterTypes.ADR)
                    arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("ADR_AO_ResponseType").ToArray();
                //Namrata:22/05/2018
                else if (masterType == MasterTypes.IEC104)
                    arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_AO_ResponseType").ToArray();
                //Namrata:23/05/2018
                else if (masterType == MasterTypes.SPORT)
                    arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("SportMaster_AO_ResponseType").ToArray();
               
                //Namrata: 24/04/2018
                if (arrResponseTypes == null)
                {

                }
                else
                {
                    if (arrResponseTypes.Length > 0) dType = arrResponseTypes[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetSupportedDataTypes()
        {
            string strRoutineName = "AO: SetSupportedDataTypes";
            try
            {
                if (masterType == MasterTypes.IEC103)
                    arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC103_AO_DataType").ToArray();
                else if (masterType == MasterTypes.ADR)
                    arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("ADR_AO_DataType").ToArray();
                else if (masterType == MasterTypes.MODBUS)
                    arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("MODBUS_AO_DataType").ToArray();
                else if (masterType == MasterTypes.Virtual)
                    arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("Virtual_AO_DataType").ToArray();
                //Namrata:7/7/2017
                else if (masterType == MasterTypes.IEC101)
                    arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_AO_DataType").ToArray();
                //Namrata:22/05/2018
                else if (masterType == MasterTypes.IEC104)
                    arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC104_AO_DataType").ToArray();
                //Namrata: 22/01/2018
                if (arrDataTypes == null)
                {

                }
                else
                {
                    if (arrDataTypes.Length > 0) dType = arrDataTypes[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static List<string> getResponseTypes(MasterTypes masterType)
        {
            if (masterType == MasterTypes.IEC103)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC103_AO_ResponseType");
            else if (masterType == MasterTypes.ADR)
                return Utils.getOpenProPlusHandle().getDataTypeValues("ADR_AO_ResponseType");
            else if (masterType == MasterTypes.MODBUS)
                return Utils.getOpenProPlusHandle().getDataTypeValues("MODBUS_AO_ResponseType");
            else if (masterType == MasterTypes.Virtual)
                return Utils.getOpenProPlusHandle().getDataTypeValues("Virtual_AO_ResponseType");
            //Namrata:7/7/2017
            else if (masterType == MasterTypes.IEC101)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_AO_ResponseType");
            //Namrata:22/05/2018
            else if (masterType == MasterTypes.IEC104)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_AO_ResponseType");
            //Namrata:23/05/2018
            else if (masterType == MasterTypes.SPORT)
                return Utils.getOpenProPlusHandle().getDataTypeValues("SportMaster_AO_ResponseType");
            else
                return new List<string>();
        }
        public static List<string> getDataTypes(MasterTypes masterType)
        {
            if (masterType == MasterTypes.IEC103)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC103_AO_DataType");
            else if (masterType == MasterTypes.ADR)
                return Utils.getOpenProPlusHandle().getDataTypeValues("ADR_AO_DataType");
            else if (masterType == MasterTypes.MODBUS)
                return Utils.getOpenProPlusHandle().getDataTypeValues("MODBUS_AO_DataType");
            else if (masterType == MasterTypes.Virtual)
                return Utils.getOpenProPlusHandle().getDataTypeValues("Virtual_AO_DataType");
            //Namrata:7/7/2017
            else if (masterType == MasterTypes.IEC101)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_AO_DataType");
            //Namrata:22/05/2018
            else if (masterType == MasterTypes.IEC104)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC104_AO_DataType");
            //Ajay: 12/11/2018
            else if (masterType == MasterTypes.SPORT)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_AO_DataType");
            else
                return new List<string>();
        }
        public AO(string aiName, List<KeyValuePair<string, string>> aiData, TreeNode tn, MasterTypes mType, int mNo, int iNo)
        {
            string strRoutineName = "AO";
            try
            {
                masterType = mType;
                masterNo = mNo;
                IEDNo = iNo;
                SetSupportedAttributes();
                SetSupportedResponseTypes();
                SetSupportedDataTypes();
                try
                {
                    ainType = (aoType)Enum.Parse(typeof(aoType), aiName);
                }
                catch (System.ArgumentException) { }
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
                        catch (System.NullReferenceException) { }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public AO(XmlNode aiNode, MasterTypes mType, int mNo, int iNo, bool imported)
        {
            string strRoutineName = "AO";
            try
            {
                masterType = mType;
                masterNo = mNo;
                IEDNo = iNo;
                SetSupportedAttributes();
                SetSupportedResponseTypes();
                SetSupportedDataTypes();
                if (aiNode.Attributes != null)
                {
                    try
                    {
                        ainType = (aoType)Enum.Parse(typeof(aoType), aiNode.Name);
                    }
                    catch (System.ArgumentException) { }
                    
                    if (masterType == MasterTypes.ADR || masterType == MasterTypes.IEC101 || masterType == MasterTypes.IEC103 || masterType == MasterTypes.MODBUS || masterType == MasterTypes.Virtual || masterType == MasterTypes.IEC104 || masterType == MasterTypes.LoadProfile) //Ajay: 31/07/2018 LoadProfile added
                    {
                        foreach (XmlAttribute item in aiNode.Attributes)
                        {
                            try
                            {
                                if (this.GetType().GetProperty(item.Name) != null) //Ajay: 03/07/2018
                                {
                                    this.GetType().GetProperty(item.Name).SetValue(this, item.Value);
                                }
                            }
                            catch (System.NullReferenceException) { }
                        }
                    }
                    //Namrata: 21/04/2018
                    #region If master Type is IEC61850Client
                    if (masterType == MasterTypes.IEC61850Client)
                    {
                        if (aiNode.Name == "AO")
                        {
                            foreach (XmlAttribute xmlattribute in aiNode.Attributes)
                            {
                                Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", xmlattribute.Name, xmlattribute.Value);
                                try
                                {
                                    if (xmlattribute.Name == "ResponseType")
                                    {
                                        Iec61850ResponseType = aiNode.Attributes[1].Value;
                                    }
                                    else if (xmlattribute.Name == "Index")
                                    {
                                        Iec61850Index = aiNode.Attributes[2].Value;
                                    }
                                    else if (xmlattribute.Name == "FC")
                                    {
                                        fc = aiNode.Attributes[3].Value;
                                    }
                                    else
                                    {
                                        if (this.GetType().GetProperty(xmlattribute.Name) != null) //Ajay: 03/07/2018
                                        {
                                            this.GetType().GetProperty(xmlattribute.Name).SetValue(this, xmlattribute.Value);
                                        }
                                    }
                                }
                                catch (System.NullReferenceException) { }
                            }
                        }
                    }
                    #endregion If master Type is IEC61850Client
                }
                else if (aiNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = aiNode.Value;
                }
                if (imported)//Generate a new unique id...
                {
                    this.AONo = (Globals.AONo + 1).ToString();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void updateAttributes(List<KeyValuePair<string, string>> aiData)
        {
            string strRoutineName = "AO: updateAttributes";
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
                        catch (System.NullReferenceException) { }
                    }
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
            rootNode = xmlDoc.CreateElement(ainType.ToString());

            #region MasterType IEC61850
            if (masterType == MasterTypes.IEC61850Client)
            {
                foreach (string attr in arrAttributes)
                {
                    XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                    attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                    rootNode.Attributes.Append(attrName);
                }
                XmlAttribute attr1 = xmlDoc.CreateAttribute("ResponseType");
                attr1.Value = Iec61850ResponseType.ToString();
                rootNode.Attributes.Append(attr1);

                XmlAttribute attr2 = xmlDoc.CreateAttribute("Index");
                attr2.Value = Iec61850Index.ToString();
                rootNode.Attributes.Append(attr2);

                XmlAttribute attFC = xmlDoc.CreateAttribute("FC");
                attFC.Value = FC.ToString();
                rootNode.Attributes.Append(attFC);

                XmlAttribute attr3 = xmlDoc.CreateAttribute("SubIndex");
                attr3.Value = SubIndex.ToString();
                rootNode.Attributes.Append(attr3);

                XmlAttribute attr5 = xmlDoc.CreateAttribute("Multiplier");
                attr5.Value = Multiplier.ToString();
                rootNode.Attributes.Append(attr5);

                XmlAttribute attr6 = xmlDoc.CreateAttribute("Constant");
                attr6.Value = Constant.ToString();
                rootNode.Attributes.Append(attr6);

                XmlAttribute attr7 = xmlDoc.CreateAttribute("Description");
                attr7.Value = Description.ToString();
                rootNode.Attributes.Append(attr7);
            }
            #endregion MasterType IEC61850

            #region MasterType IEC103,ADR,IEC101,MODBUS,Virtual,LoadProfile
            if ((masterType == MasterTypes.IEC103) || (masterType == MasterTypes.IEC104) || (masterType == MasterTypes.ADR) || (masterType == MasterTypes.IEC101) || (masterType == MasterTypes.MODBUS) || (masterType == MasterTypes.Virtual) || (masterType == MasterTypes.SPORT) || (masterType == MasterTypes.LoadProfile)) //Ajay: 31/07/2018 LoadProfile added
            {
                foreach (string attr in arrAttributes)
                {
                    XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                    attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                    rootNode.Attributes.Append(attrName);
                }
            }
            #endregion MasterType IEC103,ADR,IEC101,MODBUS,Virtual

            return rootNode;
        }
        public XmlNode exportXMLnode1()
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
            rootNode = xmlDoc.CreateElement(ainType.ToString());
            xmlDoc.AppendChild(rootNode);
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
        public string AONo
        {
            get { return aoNum.ToString(); }
            set { aoNum = Int32.Parse(value); Globals.AONo = Int32.Parse(value); }
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
                Globals.AOIndex = i;
            }
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
        }
        public string DataType
        {
            get { return dType; }
            set
            {
                dType = value;
            }
        }
        public string Multiplier
        {
            get { return mult.ToString(); }
            set
            {
                try
                {
                    mult = Double.Parse(value);
                }
                catch (System.FormatException)
                {
                    mult = 1;
                }
            }
        }
        public string Constant
        {
            get { return cnst.ToString(); }
            set
            {
                try
                {
                    cnst = Double.Parse(value);
                }
                catch (System.FormatException)
                {
                    cnst = 0;
                }
            }
        }
        public string Description
        {
            get { return aiDesc; }
            set { aiDesc = value; }
        }
        public string IEDName
        {
            get { return Iec61850Iedname; }
            set
            {
                Iec61850Iedname = value;
            }
        }
        public string IEC61850ResponseType
        {
            get { return Iec61850ResponseType; }
            set
            {
                Iec61850ResponseType = value;
            }
        }
        public string IEC61850Index
        {
            get { return Iec61850Index; }
            set
            {
                Iec61850Index = value;
            }
        }
        public string FC
        {
            get { return fc; }
            set { fc = value; }
        }
        //Ajay: 26/07/2018
        public string Event
        {
            get { return (events == true ? "YES" : "NO"); }
            set
            {
                events = (value.ToLower() == "yes") ? true : false;
            }
        }
    }
}
