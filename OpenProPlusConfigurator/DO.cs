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
    public class DO
    {
        #region Declaration
        enum doType
        {
            DO
        };
        private bool isNodeComment = false;
        private string comment = "";
        private doType donType = doType.DO;
        private int doNum = -1;
        private string rType = "";
        private int idx = -1;
        private int sidx = -1;
        private string cType = "";
        private int pulseDurationmSec = 100;
        private string doDesc = "";
        private MasterTypes masterType = MasterTypes.UNKNOWN;
        private int masterNo = -1;
        private string[] arrResponseTypes;
        private int IEDNo = -1;
        private string[] arrAttributes;
        private string[] arrControlTypes;
        //Namrata: 03/09/2017
        private string Iec61850DOIndex = "";
        private bool selectRequired = false;
        private string Iec61850DOResponseType = "";
        private string Iec61850DOIedname = "";
        private string enableDI1 = "";
        private string fc = "";
        private bool events = true; //Ajay: 18/07/2018
        private string cntrlModel = ""; //Ajay: 04/08/2018
        private string[] arrControlModels;
        ucDOlist UcDo = new ucDOlist();
        #endregion Declaration
        public static List<string> GetControlModels(MasterTypes masterType)
        {
            if(masterType == MasterTypes.IEC61850Client)
            {
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850_ControlModels");
            }
            else { return new List<string>(); }
        }
        private void SetSupportedAttributes()
        {
            if (masterType == MasterTypes.IEC61850Client)
                arrAttributes = new string[] { "DONo" };
                //arrAttributes = new string[] { "DONo", "CModel" }; //Ajay: 04/08/2018
            else if (masterType == MasterTypes.IEC103 || masterType == MasterTypes.MODBUS || masterType == MasterTypes.Virtual || masterType == MasterTypes.ADR)
                arrAttributes = new string[] { "DONo", "ResponseType", "Index", "SubIndex", "ControlType", "PulseDurationMS", "EnableDI", "Description" };
            //Namrata:07/05/2018
            else if (masterType == MasterTypes.IEC101)
                arrAttributes = new string[] { "DONo", "ResponseType", "Index", "SubIndex", "ControlType", "PulseDurationMS", "EnableDI","Select", "Description" };
            //Namrata:22/05/2018
            else if (masterType == MasterTypes.IEC104)
                //Ajay: 18/07/2018 Event Added
                //Ajay: 12/11/2018 Event Removed
                //arrAttributes = new string[] { "DONo", "ResponseType", "Index", "SubIndex", "ControlType", "PulseDurationMS", "EnableDI", "Select", "Event", "Description" };
                arrAttributes = new string[] { "DONo", "ResponseType", "Index", "SubIndex", "ControlType", "PulseDurationMS", "EnableDI", "Select", "Description" };
                //Namrata:23/05/2018
            else if (masterType == MasterTypes.SPORT)
                arrAttributes = new string[] { "DONo", "ResponseType", "Index", "SubIndex", "ControlType", "PulseDurationMS", "EnableDI", "Select", "Description" };
            //Ajay: 31/07/2018
            else if (masterType == MasterTypes.LoadProfile)
                arrAttributes = new string[] { "DONo", "Description" };
        }
        private void SetSupportedResponseTypes()
        {
            if (masterType == MasterTypes.IEC103)
                arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC103_DO_ResponseType").ToArray();
            else if (masterType == MasterTypes.ADR)
                arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("ADR_DO_ResponseType").ToArray();
            else if (masterType == MasterTypes.MODBUS)
                arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("MODBUS_DO_ResponseType").ToArray();
            else if (masterType == MasterTypes.Virtual)
                arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("Virtual_DO_ResponseType").ToArray();
            //Namrata:7/7/2017
            else if (masterType == MasterTypes.IEC101)
                arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_DO_ResponseType").ToArray();
            //Namrata:22/05/2018
            else if (masterType == MasterTypes.IEC104)
                arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_DO_ResponseType").ToArray();
            //Namrata:23/05/2018
            else if (masterType == MasterTypes.SPORT)
                arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("SportMaster_DO_ResponseType").ToArray();
            //Ajay: 31/07/2018
            //else if (masterType == MasterTypes.LoadProfile)
            //    arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("LoadProfile_DO_ResponseType").ToArray();
            //if (arrResponseTypes.Length > 0) rType = arrResponseTypes[0];
            //Namrata: 24/04/2018
            if (arrResponseTypes == null)
            {

            }
            else
            {
                if (arrResponseTypes.Length > 0) rType = arrResponseTypes[0];
            }
        }
        private void SetSupportedControlTypes()
        {
            if (masterType == MasterTypes.IEC103)
                arrControlTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC103_DO_ControlType").ToArray();
            else if (masterType == MasterTypes.ADR)
                arrControlTypes = Utils.getOpenProPlusHandle().getDataTypeValues("ADR_DO_ControlType").ToArray();
            else if (masterType == MasterTypes.MODBUS)
                arrControlTypes = Utils.getOpenProPlusHandle().getDataTypeValues("MODBUS_DO_ControlType").ToArray();
            else if (masterType == MasterTypes.Virtual)
                arrControlTypes = Utils.getOpenProPlusHandle().getDataTypeValues("Virtual_DO_ControlType").ToArray();
            //Namrata:07/7/2017
            else if (masterType == MasterTypes.IEC101)
                arrControlTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_DO_ControlType").ToArray();
            else if (masterType == MasterTypes.IEC61850Client)
                arrControlTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850_DO_ControlType").ToArray();
            //Namrata:22/05/2018
            else if (masterType == MasterTypes.IEC104)
                arrControlTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_DO_ControlType").ToArray();
            //Namrata:23/05/2018
            else if (masterType == MasterTypes.SPORT)
                //Ajay: 02/07/2018
                //arrControlTypes = Utils.getOpenProPlusHandle().getDataTypeValues("SPORTMaster_DO_ControlType").ToArray();
                arrControlTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_DO_ControlType").ToArray();
            //Ajay: 31/07/2018
            //else if (masterType == MasterTypes.LoadProfile)
            //arrControlTypes = Utils.getOpenProPlusHandle().getDataTypeValues("LoadProfile_DO_ControlType").ToArray();
            //Ajay: 31/07/2018
            if (masterType != MasterTypes.LoadProfile)
            { if (arrControlTypes.Length > 0) cType = arrControlTypes[0]; }
        }
        public static List<string> getResponseTypes(MasterTypes masterType)
        {
            if (masterType == MasterTypes.IEC103)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC103_DO_ResponseType");
            if (masterType == MasterTypes.ADR)
                return Utils.getOpenProPlusHandle().getDataTypeValues("ADR_DO_ResponseType");
            else if (masterType == MasterTypes.MODBUS)
                return Utils.getOpenProPlusHandle().getDataTypeValues("MODBUS_DO_ResponseType");
            else if (masterType == MasterTypes.Virtual)
                return Utils.getOpenProPlusHandle().getDataTypeValues("Virtual_DO_ResponseType");
            //Namrata:07/7/2017
            else if (masterType == MasterTypes.IEC101)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_DO_ResponseType");
            //Namrata:22/05/2018
            else if (masterType == MasterTypes.IEC104)
                //return Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_DI_ResponseType"); 
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_DO_ResponseType");
            //Namrata:23/05/2018
            else if (masterType == MasterTypes.SPORT)
                return Utils.getOpenProPlusHandle().getDataTypeValues("SportMaster_DO_ResponseType");
            //Ajay: 31/07/2018
            //else if (masterType == MasterTypes.LoadProfile)
            //return Utils.getOpenProPlusHandle().getDataTypeValues("LoadProfile_DO_ResponseType");
            return new List<string>();
        }
        public static List<string> getControlTypes(MasterTypes masterType)
        {
            if (masterType == MasterTypes.IEC103)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC103_DO_ControlType");
            if (masterType == MasterTypes.ADR)
                return Utils.getOpenProPlusHandle().getDataTypeValues("ADR_DO_ControlType");
            else if (masterType == MasterTypes.MODBUS)
                return Utils.getOpenProPlusHandle().getDataTypeValues("MODBUS_DO_ControlType");
            else if (masterType == MasterTypes.Virtual)
                return Utils.getOpenProPlusHandle().getDataTypeValues("Virtual_DO_ControlType");
            //Namrata:7/7/2017
            else if (masterType == MasterTypes.IEC101)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_DO_ControlType");
            //Namrata:22/05/2018
            else if (masterType == MasterTypes.IEC104)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_DO_ControlType");
            else if (masterType == MasterTypes.IEC61850Client)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850_DO_ControlType");
            //Namrata:23/05/2018
            else if (masterType == MasterTypes.SPORT)
                //Ajay: 02/07/2018
                //return Utils.getOpenProPlusHandle().getDataTypeValues("SPORTMaster_DO_ControlType");
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_DO_ControlType");
            //Ajay: 31/07/2018
            //else if (masterType == MasterTypes.LoadProfile)
            //return Utils.getOpenProPlusHandle().getDataTypeValues("LoadProfile_DO_ControlType");
            else
                return new List<string>();
        }
        public DO(string doName, List<KeyValuePair<string, string>> doData, TreeNode tn, MasterTypes mType, int mNo, int iNo)
        {
            masterType = mType;
            masterNo = mNo;
            IEDNo = iNo;
            SetSupportedAttributes();
            SetSupportedResponseTypes();
            SetSupportedControlTypes();
            try
            {
                donType = (doType)Enum.Parse(typeof(doType), doName);
            }
            catch (System.ArgumentException)
            {
            }
            if (doData != null && doData.Count > 0)
            {
                foreach (KeyValuePair<string, string> dokp in doData)
                {
                    try
                    {
                        if (this.GetType().GetProperty(dokp.Key) != null) //Ajay: 02/07/2018
                        {
                            this.GetType().GetProperty(dokp.Key).SetValue(this, dokp.Value);
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                    }
                }
            }
        }
        public DO(XmlNode doNode, MasterTypes mType, int mNo, int iNo, bool imported)
        {
            masterType = mType;
            masterNo = mNo;
            IEDNo = iNo;
            SetSupportedAttributes();
            SetSupportedResponseTypes();
            SetSupportedControlTypes();
            if (doNode.Attributes != null)
            {
                try
                {
                    donType = (doType)Enum.Parse(typeof(doType), doNode.Name);
                }
                catch (System.ArgumentException)
                { 
                }
                if (masterType == MasterTypes.ADR || masterType == MasterTypes.IEC103 || masterType == MasterTypes.MODBUS || masterType == MasterTypes.Virtual || masterType == MasterTypes.IEC104 || masterType == MasterTypes.SPORT || masterType == MasterTypes.LoadProfile) //Ajay: 31/07/2018 LoadProfile added
                {
                    foreach (XmlAttribute item in doNode.Attributes)
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
                //Namrata: 08/05/2018
                if(masterType == MasterTypes.IEC101)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(Utils.XMLFileName);
                    XmlAttribute attrName = xmlDoc.CreateAttribute("Select");
                    attrName.Value = "ENABLE";
                    bool IsExist = false;

                    arrAttributes.OfType<string>().ToList().ForEach(item =>
                    {
                        if (!doNode.Attributes.OfType<XmlNode>().ToList().Select(x => x.Name).ToList().Contains(item))
                        {
                            IsExist = true;
                        }
                    });
                    if (IsExist)
                    {
                        doNode.Attributes.SetNamedItem(attrName);
                    }
                    foreach (XmlAttribute item in doNode.Attributes)
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
                }
                //Namrata: 17/7/2017
                if (masterType == MasterTypes.IEC61850Client)
                {
                    if (doNode.Name == "DO")
                    {
                        foreach (XmlAttribute xmlattribute in doNode.Attributes)
                        {
                            Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", xmlattribute.Name, xmlattribute.Value);
                            try
                            {

                                if (xmlattribute.Name == "ResponseType")
                                {
                                    //Ajay: 04/08/2018 Seq changed due to CModel added
                                    //Iec61850DOResponseType = doNode.Attributes[1].Value;
                                    Iec61850DOResponseType = doNode.Attributes["ResponseType"].Value;
                                }
                                else if (xmlattribute.Name == "Index")
                                {
                                    //Ajay: 04/08/2018 Seq changed due to CModel added
                                    //Iec61850DOIndex = doNode.Attributes[2].Value;
                                    Iec61850DOIndex = doNode.Attributes["Index"].Value;
                                }
                                else if (xmlattribute.Name == "FC")
                                {
                                    //Ajay: 04/08/2018 Seq changed due to CModel added
                                    //fc = doNode.Attributes[3].Value;
                                    fc = doNode.Attributes["FC"].Value;
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
                                Utils.WriteLine(VerboseLevel.WARNING, "DI: Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", xmlattribute.Name, xmlattribute.Value);
                            }

                        }
                    }
                }
                Utils.Write(VerboseLevel.DEBUG, "\n");
            }
            else if (doNode.NodeType == XmlNodeType.Comment)
            {
                isNodeComment = true;
                comment = doNode.Value;
            }
            if (imported)//Generate a new unique id...
            {
                this.DONo = (Globals.DONo + 1).ToString();
                //Commented as asked by Amol, 29/12/2k16: this.Index = (Globals.DOIndex + 1).ToString();
            }
        }
        public void updateAttributes(List<KeyValuePair<string, string>> doData)
        {
            if (doData != null && doData.Count > 0)
            {
                foreach (KeyValuePair<string, string> dokp in doData)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", dokp.Key, dokp.Value);
                    try
                    {
                        if (this.GetType().GetProperty(dokp.Key) != null) //Ajay: 03/07/2018
                        { 
                            this.GetType().GetProperty(dokp.Key).SetValue(this, dokp.Value);
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", dokp.Key, dokp.Value);
                    }
                }
                Utils.Write(VerboseLevel.DEBUG, "\n");
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
            rootNode = xmlDoc.CreateElement(donType.ToString());
            xmlDoc.AppendChild(rootNode);

            if (masterType == MasterTypes.IEC61850Client)
            {
                foreach (string attr in arrAttributes)
                {
                    XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                    attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                    rootNode.Attributes.Append(attrName);
                }

                XmlAttribute attr1 = xmlDoc.CreateAttribute("ResponseType");
                attr1.Value = Iec61850DOResponseType.ToString();
                rootNode.Attributes.Append(attr1);

                XmlAttribute attr2 = xmlDoc.CreateAttribute("Index");
                attr2.Value = Iec61850DOIndex.ToString();
                rootNode.Attributes.Append(attr2);

                //Ajay: 04/08/2018
                XmlAttribute attrCmodel = xmlDoc.CreateAttribute("CModel");
                attrCmodel.Value = CModel;
                rootNode.Attributes.Append(attrCmodel);

                XmlAttribute attrfc = xmlDoc.CreateAttribute("FC");
                attrfc.Value = FC.ToString();
                rootNode.Attributes.Append(attrfc);

                XmlAttribute attr3 = xmlDoc.CreateAttribute("SubIndex");
                attr3.Value = SubIndex.ToString();
                rootNode.Attributes.Append(attr3);

                XmlAttribute attr4 = xmlDoc.CreateAttribute("ControlType");
                attr4.Value = ControlType.ToString();
                rootNode.Attributes.Append(attr4);

                XmlAttribute attr7 = xmlDoc.CreateAttribute("PulseDurationMS");
                attr7.Value = PulseDurationMS.ToString();
                rootNode.Attributes.Append(attr7);

                XmlAttribute attr9 = xmlDoc.CreateAttribute("EnableDI");
                attr9.Value = EnableDI.ToString();
                rootNode.Attributes.Append(attr9);

                XmlAttribute attr8 = xmlDoc.CreateAttribute("Description");
                attr8.Value = Description.ToString();
                rootNode.Attributes.Append(attr8);
            }
            else
            {
                foreach (string attr in arrAttributes)
                {
                    XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                    attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                    if(attrName.Name == "EnableDI")
                    {
                        if(attrName.Value == "")
                        {
                            attrName.Value = "0";
                        }
                    }
                    rootNode.Attributes.Append(attrName);
                }
            }
            return rootNode;
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public string DONo
        {
            get { return doNum.ToString(); }
            set { doNum = Int32.Parse(value); Globals.DONo = Int32.Parse(value); }
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
                Globals.DOIndex = i;
            }
            //set { idx = Int32.Parse(value); Globals.DOIndex = Int32.Parse(value); } //Ajay: 31/07/2018
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
        public string ControlType
        {
            get { return cType; }
            set
            {
                cType = value;
            }
        }
        public string PulseDurationMS
        {
            get { return pulseDurationmSec.ToString(); }
            set { pulseDurationmSec = Int32.Parse(value); }
        }
        public string Description
        {
            get { return doDesc; }
            set { doDesc = value; }
        }
        public string FC
        {
            get { return fc; }
            set { fc = value; }
        }
        public string IEDName
        {
            get { return Iec61850DOIedname; }
            set
            {
                Iec61850DOIedname = value;
            }
        }
        public string IEC61850ResponseType
        {
            get { return Iec61850DOResponseType; }
            set
            {
                Iec61850DOResponseType = value;
            }
        }
        public string IEC61850Index
        {
            get { return Iec61850DOIndex; }
            set
            {
                Iec61850DOIndex = value;
            }
        }
        public string EnableDI
        {
            get { return enableDI1; }
            set
            {
                enableDI1 = value;
            }
        }
        public string Select
        {
            get { return (selectRequired == true ? "ENABLE" : "DISABLE"); }
            set { selectRequired = (value.ToLower() == "enable") ? true : false; }
        }
        //Ajay: 18/07/2018
        public string Event
        {
            get { return (events == true ? "YES" : "NO"); }
            set
            {
                events = (value.ToLower() == "yes") ? true : false;
            }
        }
        //Ajay: 04/08/2018
        public string CModel
        {
            get { return cntrlModel; }
            set
            {
                cntrlModel = value;
            }
        }
    }
}
