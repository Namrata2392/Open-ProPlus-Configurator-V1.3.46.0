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
    public class AI
    {
        #region Declaration
        enum aiType
        {
            AI
        };
        ucMasterIEC103 uciec = new ucMasterIEC103();
        private bool isNodeComment = false;
        private string comment = "";
        private aiType ainType = aiType.AI;
        private int aiNum = -1;
        private string rType = "";
        private int idx = -1;
        private int sidx = 0;
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
        //Namrata: 03/09/2017
        private string Iec61850Index = "";
        private string Iec61850ResponseType = "";
        private string Iec61850Iedname = "";
        private string fc = ""; //Namrata:10/04/2018
        //Ajay: 13/07/2018
        private bool eventenable = true;
        //Ajay: 31/07/2018
        private string name = "";
        private int ai1 = 0;
        private int ai2 = 0;
        private int ai3 = 0;
        private bool logenable = false;

        //Namrata:29/01/2019
        private double deadband = 1;
        private double highlimit = 1;
        private double lowlimit = 1;
        private int dono = 0;
        #endregion Declaration
        private void SetSupportedAttributes()
        {
            string strRoutineName = "AI:SetSupportedAttributes";
            try
            {
                if (masterType == MasterTypes.IEC103)
                    arrAttributes = new string[] { "AINo", "ResponseType", "Index", "SubIndex", "DataType", "Multiplier", "Constant", "EventEnable", "Description" }; //Ajay: 30/08/2018 EventEnable added
                else if (masterType == MasterTypes.MODBUS)
                    arrAttributes = new string[] { "AINo", "ResponseType", "Index", "SubIndex", "DataType", "Multiplier", "Constant", "EventEnable", "Description" }; //Ajay: 30/08/2018 EventEnable added
                else if (masterType == MasterTypes.Virtual)
                    arrAttributes = new string[] { "AINo", "ResponseType", "Index", "DataType", "Multiplier", "Constant", "Description" }; 
                else if (masterType == MasterTypes.ADR)//Namrata:3/6/2017
                    arrAttributes = new string[] { "AINo", "ResponseType", "Index", "SubIndex", "DataType", "Multiplier", "Constant", "EventEnable", "Description" }; //Ajay: 30/08/2018 EventEnable added
                else if (masterType == MasterTypes.IEC101) //Namrata:7/7/2017) //Ajay: 12/11/2018 DataType Added
                    arrAttributes = new string[] { "AINo", "ResponseType", "Index", "SubIndex", "DataType", "Multiplier", "Constant", "EventEnable", "Description" };////arrAttributes = new string[] { "AINo", "ResponseType", "Index", "SubIndex", "Multiplier", "Constant", "EventEnable", "Description" };
                else if (masterType == MasterTypes.IEC61850Client) //Namrata :23/8/2017 
                    arrAttributes = new string[] { "AINo"}; //Ajay: 30/08/2018 EventEnable added //Ajay: 01/11/2018 EventEnable Removed from aaray
                else if (masterType == MasterTypes.IEC104)//Ajay: 13/07/2018 Event Added
                    arrAttributes = new string[] { "AINo", "ResponseType", "Index", "SubIndex", "Multiplier", "Constant", "EventEnable", "Description" }; //Ajay: 30/08/2018 EventEnable added
                else if (masterType == MasterTypes.SPORT) //Namrata:23/05/2018
                    arrAttributes = new string[] { "AINo", "ResponseType", "Index", "SubIndex", "Deadband", "Multiplier", "Constant", "EventEnable","HighLimit","LowLimit", "DONo", "Description" }; //Ajay:30/08/2018 EventEnable added
                else if (masterType == MasterTypes.LoadProfile)//Ajay: 19/09/2018 LogEnable removed//Ajay: 31/07/2018
                    arrAttributes = new string[] { "AINo", "Name", "AI1", "AI2", "AI3", "EventEnable", "Description" }; //Ajay:30/08/2018 EventEnable added
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetSupportedResponseTypes()
        {
            string strRoutineName = "AI:SetSupportedResponseTypes";
            try
            {
                if (masterType == MasterTypes.IEC103)
                    arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC103_AI_ResponseType").ToArray();
                else if (masterType == MasterTypes.MODBUS)
                    arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("MODBUS_AI_ResponseType").ToArray();
                else if (masterType == MasterTypes.Virtual)
                    arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("Virtual_AI_ResponseType").ToArray();
                else if (masterType == MasterTypes.IEC101)//Namrata:7/7/2017
                    arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_AI_ResponseType").ToArray();
                else if (masterType == MasterTypes.ADR)
                    arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("ADR_AI_ResponseType").ToArray();
                else if (masterType == MasterTypes.IEC104)//Namrata:22/05/2018
                    arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_AI_ResponseType").ToArray();
                else if (masterType == MasterTypes.SPORT)//Namrata:22/05/2018
                    arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("SportMaster_AI_ResponseType").ToArray();
                if (arrResponseTypes == null)//Namrata:24/04/2018
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
            string strRoutineName = "AI:SetSupportedDataTypes";
            try
            {
                if (masterType == MasterTypes.IEC103)
                    arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC103_AI_DataType").ToArray();
                else if (masterType == MasterTypes.ADR)
                    arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("ADR_AI_DataType").ToArray();
                else if (masterType == MasterTypes.MODBUS)
                    arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("MODBUS_AI_DataType").ToArray();
                else if (masterType == MasterTypes.Virtual)
                    arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("Virtual_AI_DataType").ToArray();
                else if (masterType == MasterTypes.IEC101) //Namrata:7/7/2017
                    arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_AI_DataType").ToArray();
                else if (masterType == MasterTypes.IEC104) //Namrata:22/05/2018
                    arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC104_AI_DataType").ToArray();
                if (arrDataTypes == null) //Namrata: 22/01/2018
                { }
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
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC103_AI_ResponseType");
            else if (masterType == MasterTypes.ADR)
                return Utils.getOpenProPlusHandle().getDataTypeValues("ADR_AI_ResponseType");
            else if (masterType == MasterTypes.MODBUS)
                return Utils.getOpenProPlusHandle().getDataTypeValues("MODBUS_AI_ResponseType");
            else if (masterType == MasterTypes.Virtual)
                return Utils.getOpenProPlusHandle().getDataTypeValues("Virtual_AI_ResponseType");
            else if (masterType == MasterTypes.IEC101) //Namrata:7/7/2017
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_AI_ResponseType");
            else if (masterType == MasterTypes.IEC104) //Namrata:22/05/2018
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_AI_ResponseType");
            else if (masterType == MasterTypes.SPORT) //Namrata:23/05/2018
                return Utils.getOpenProPlusHandle().getDataTypeValues("SportMaster_AI_ResponseType");
            else
                return new List<string>();
        }
        public static List<string> getDataTypes(MasterTypes masterType)
        {
            if (masterType == MasterTypes.IEC103)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC103_AI_DataType");
            else if (masterType == MasterTypes.ADR)
                return Utils.getOpenProPlusHandle().getDataTypeValues("ADR_AI_DataType");
            else if (masterType == MasterTypes.MODBUS)
                return Utils.getOpenProPlusHandle().getDataTypeValues("MODBUS_AI_DataType");
            else if (masterType == MasterTypes.Virtual)
                return Utils.getOpenProPlusHandle().getDataTypeValues("Virtual_AI_DataType");
            else if (masterType == MasterTypes.IEC101) //Namrata:7/7/2017
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_AI_DataType");
            else if (masterType == MasterTypes.IEC104) //Namrata:22/05/2018
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC104_AI_DataType");
            else
                return new List<string>();
        }
        public AI(string aiName, List<KeyValuePair<string, string>> aiData, TreeNode tn, MasterTypes mType, int mNo, int iNo)
        {
            string strRoutineName = "AI";
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
                    ainType = (aiType)Enum.Parse(typeof(aiType), aiName);
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
        public AI(XmlNode aiNode, MasterTypes mType, int mNo, int iNo, bool imported)
        {
            string strRoutineName = "AI";
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
                        ainType = (aiType)Enum.Parse(typeof(aiType), aiNode.Name);
                    }
                    catch (System.ArgumentException)
                    {
                    }
                    if (masterType == MasterTypes.ADR || masterType == MasterTypes.IEC101 || masterType == MasterTypes.IEC103 || masterType == MasterTypes.MODBUS || masterType == MasterTypes.Virtual || masterType == MasterTypes.IEC104 || masterType == MasterTypes.SPORT || masterType == MasterTypes.LoadProfile) //Ajay: 31/07/2018 LoadProfile Added
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
                            catch (System.NullReferenceException)
                            {
                            }
                        }
                    }
                    // If master Type is IEC61850Client
                    if (masterType == MasterTypes.IEC61850Client) //Namrata: 17/7/2017
                    {
                        if (aiNode.Name == "AI")
                        {
                            foreach (XmlAttribute xmlattribute in aiNode.Attributes)
                            {
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
                                catch (System.NullReferenceException)
                                {
                                }
                            }
                        }
                    }
                }
                else if (aiNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = aiNode.Value;
                }
                if (imported)//Generate a new unique id...
                {
                    this.AINo = (Globals.AINo + 1).ToString();
                    if (masterType == MasterTypes.IEC103)
                    {
                    }
                    else if (masterType == MasterTypes.ADR)
                    {
                        //Commented as asked by Amol, 29/12/2k16: this.Index = (Globals.AIIndex + 1).ToString();
                    }
                    else if (masterType == MasterTypes.MODBUS)
                    {
                        //Commented as asked by Amol, 29 / 12 / 2k16: this.Index = (Globals.AIIndex + 1).ToString();
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
                rootNode = xmlDoc.CreateComment(comment);
                xmlDoc.AppendChild(rootNode);
                return rootNode;
            }
            rootNode = xmlDoc.CreateElement(ainType.ToString());

            //Ajay: 01/11/2018 Commented - added attributes directly in arrAttributes Array
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

                //Ajay: 01/11/2018 EventEnable added
                XmlAttribute attr7 = xmlDoc.CreateAttribute("EventEnable"); 
                attr7.Value = EventEnable.ToString();
                rootNode.Attributes.Append(attr7);

                XmlAttribute attr8 = xmlDoc.CreateAttribute("Description");
                attr8.Value = Description.ToString();
                rootNode.Attributes.Append(attr8);
            }
            #endregion MasterType IEC61850

            #region MasterType IEC103,ADR,IEC101,MODBUS,Virtual, LoadProfile
            if ((masterType == MasterTypes.IEC103) || (masterType == MasterTypes.ADR) || (masterType == MasterTypes.IEC104) || 
                (masterType == MasterTypes.IEC101) || (masterType == MasterTypes.MODBUS) || (masterType == MasterTypes.Virtual) || 
                (masterType == MasterTypes.SPORT) || (masterType == MasterTypes.LoadProfile)) //Ajay: 31/07/2018 LoadProfile Added
            {
                foreach (string attr in arrAttributes)
                {
                    XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                    attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                    rootNode.Attributes.Append(attrName);
                }
            }
            #endregion 

            return rootNode;
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public string AINo
        {
            get { return aiNum.ToString(); }
            set { aiNum = Int32.Parse(value); Globals.AINo = Int32.Parse(value); }
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
                Globals.AIIndex = i;
            }
            //set { idx = Int32.Parse(value); Globals.AIIndex = Int32.Parse(value); } //Ajay: 31/07/2018
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
            //set { sidx = Int32.Parse(value); } //Ajay: 31/07/2018
        }
        public string DataType
        {
            get { return dType; }
            set
            {
                dType = value;
            }
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
        public string FC
        {
            get { return fc; }
            set { fc = value; }
        }

        //Ajay: 13/07/2018
        public string EventEnable
        {
            get { return (eventenable == true ? "YES" : "NO"); }
            set
            {
                eventenable = (value.ToLower() == "yes") ? true : false;
            }
        }
        //Ajay: 31/07/2018
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        //Ajay: 31/07/2018
        public string AI1
        {
            get { return ai1.ToString(); }
            set
            {
                int i = 0;
                Int32.TryParse(value, out i);
                ai1 = i;
            }
        }
        //Ajay: 31/07/2018
        public string AI2
        {
            get { return ai2.ToString(); }
            set
            {
                int i = 0;
                Int32.TryParse(value, out i);
                ai2 = i;
            }
        }
        //Ajay: 31/07/2018
        public string AI3
        {
            get { return ai3.ToString(); }
            set
            {
                int i = 0;
                Int32.TryParse(value, out i);
                ai3 = i;
            }
        }
        //Ajay: 31/07/2018
        public string LogEnable
        {
            get { return (logenable == true ? "YES" : "NO"); }
            set
            {
                logenable = (value.ToLower() == "yes") ? true : false;
            }
        }

        //Namrata:29/01/2019
        public string Deadband
        {
            get { return deadband.ToString(); }
            set
            {
                try
                {
                    deadband = Double.Parse(value);
                }
                catch (System.FormatException)
                {
                    deadband = 1;
                }
            }
        }
        public string HighLimit
        {
            get { return highlimit.ToString(); }
            set
            {
                try
                {
                    highlimit = Double.Parse(value);
                }
                catch (System.FormatException)
                {
                    highlimit = 1;
                }
            }
        }
        public string LowLimit
        {
            get { return lowlimit.ToString(); }
            set
            {
                try
                {
                    lowlimit = Double.Parse(value);
                }
                catch (System.FormatException)
                {
                    lowlimit = 1;
                }
            }
        }
        public string DONo
        {
            get { return dono.ToString(); }
            set
            {
                int i = 0;
                Int32.TryParse(value, out i);
                dono = i;
            }
        }
    }
}
