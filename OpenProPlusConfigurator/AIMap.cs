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
    public class AIMap
    {
        #region Declaration
        enum aimType
        {
            AI
        };
      
        private string key = "";
        private string unit = "";
        private bool isNodeComment = false;
        private string comment = "";
        private bool isReindexed = false;
        private aimType aimnType = aimType.AI;
        private int aiNum = -1;
        private int reportingIdx = -1;
        private string dType = "";
        private string description = "";
        private string cType = "";
        private double dBand = 1;
        private double mult = 1;
        private double cnst = 0;
        public SlaveTypes slaveType = SlaveTypes.UNKNOWN;
        private string[] arrAttributes;
        private string[] arrDataTypes;
        private string[] arrCommandTypes;
        //Namrata: 02/11/2017
        private string iec61850reportingindex = "";
        //Ajay:06/07/2018
        private string unitID = "";
        private bool used = true;
        //Ajay: 05/09/2018
        private bool sEvent = false;

        private string cellNo = "";
        private string widget = "";

        private string eClass = "";
        private string variation = "";
        private string evari = "";
        private string[] arrvariation;
        private string[] arrEVariation;
        private string[] arrEClass;
        private string Vari = "";
        private string EVar = "";
        private string ECls = "";

        #endregion Declaration
        private void SetSupportedAttributes()
        {
            string strRoutineName = "AIMap:SetSupportedAttributes";
            try
            {
                if (slaveType == SlaveTypes.IEC104)
                    arrAttributes = new string[] { "AINo", "ReportingIndex", "DataType", "Deadband", "Multiplier", "Constant", "Event", "Description" }; //Ajay: 05/09/2018 Event added
                else if (slaveType == SlaveTypes.MODBUSSLAVE)
                    arrAttributes = new string[] { "AINo", "ReportingIndex", "DataType", "CommandType", "Deadband", "Multiplier", "Constant", "Description" };
                //Namrata: 07/07/2017
                else if (slaveType == SlaveTypes.IEC101SLAVE)
                    arrAttributes = new string[] { "AINo", "ReportingIndex", "DataType", "Deadband", "Multiplier", "Constant","Event", "Description" }; //Ajay: 05/09/2018 Event added
                else if (slaveType == SlaveTypes.IEC61850Server)
                    arrAttributes = new string[] { "AINo" };
                //Ajay: 06/07/2018
                else if (slaveType == SlaveTypes.SPORTSLAVE)
                    arrAttributes = new string[] { "AINo", "UnitID", "ReportingIndex", "DataType", "Deadband", "Multiplier", "Constant", "Used", "Event", "Description" };  //Ajay: 05/09/2018 Event added
                //Namrata:02/04/2019                                                                                                                                                          //Ajay: 06/07/2018
                else if (slaveType == SlaveTypes.MQTTSLAVE)
                    arrAttributes = new string[] { "AINo", "ReportingIndex", "Key", "DataType", "Unit", "Deadband", "Multiplier", "Constant", "Description" };
                //Namrata: 30/05/2019                                                                                                                                                        //Ajay: 06/07/2018
                else if (slaveType == SlaveTypes.SMSSLAVE)
                    arrAttributes = new string[] { "AINo", "ReportingIndex", "DataType", "Unit", "Deadband", "Multiplier", "Constant", "Description" };
                else if (slaveType == SlaveTypes.GRAPHICALDISPLAYSLAVE)
                    arrAttributes = new string[] { "AINo"};
                //Namrata: 12/11/2019
                else if (slaveType == SlaveTypes.DNP3SLAVE)
                    arrAttributes = new string[] { "AINo","ReportingIndex","CommandType","EventClass", "Variation", "EventVariation", "Deadband", "Multiplier", "Constant", "Description" };
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetSupportedEventVariation()
        {
            string strRoutineName = "AIMap:SetSupportedEventVariation";
            try
            {
                arrEVariation = Utils.getOpenProPlusHandle().getDataTypeValues("EventsVariations").ToArray();
                if (arrEVariation.Length > 0) EVar = arrEVariation[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetSupportedEventClass()
        {
            string strRoutineName = "AIMap:SetSupportedEventClass";
            try
            {
                arrEClass = Utils.getOpenProPlusHandle().getDataTypeValues("EventsClasses").ToArray();
                if (arrEClass.Length > 0) ECls = arrEClass[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetSupportedVariation()
        {
            string strRoutineName = "AIMap:SetSupportedVariation";
            try
            {
                arrvariation = Utils.getOpenProPlusHandle().getDataTypeValues("Variations").ToArray();
                if (arrvariation.Length > 0) Vari = arrvariation[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void SetSupportedDataTypes()
        {
            string strRoutineName = "AIMap:SetSupportedDataTypes";
            try
            {
                if (slaveType == SlaveTypes.IEC104)
                    arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC104_AI_DataType").ToArray();
                else if (slaveType == SlaveTypes.MODBUSSLAVE)
                    arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("MODBUSSlave_AI_DataType").ToArray();
                //Namrata: 07/07/2017
                else if (slaveType == SlaveTypes.IEC101SLAVE)
                    arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC101Slave_AI_DataType").ToArray();
                else if (slaveType == SlaveTypes.IEC61850Server)
                    arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850Slave_AI_DataType").ToArray();
                //Ajay: 06/07/2018
                else if (slaveType == SlaveTypes.SPORTSLAVE)
                    arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("SportSlave_AI_DataType").ToArray();
                //Namrata:02/04/2019
                else if (slaveType == SlaveTypes.MQTTSLAVE)
                    arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("MQTTSlave_AI_DataType").ToArray();
                //if (arrDataTypes.Length > 0) dType = arrDataTypes[0];
                //Namrata: 30/05/2019
                else if (slaveType == SlaveTypes.SMSSLAVE)
                    arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("SMSSlave_AI_DataType").ToArray();
               // if (arrDataTypes.Length > 0) dType = arrDataTypes[0];
                if (arrDataTypes == null){ }
                else
                { if (arrDataTypes.Length > 0) dType = arrDataTypes[0]; }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetSupportedCommandTypes()
        {
            string strRoutineName = "AIMap:SetSupportedCommandTypes";
            try
            {
                if (slaveType == SlaveTypes.MODBUSSLAVE)
                    arrCommandTypes = Utils.getOpenProPlusHandle().getDataTypeValues("MODBUSSlave_AI_CommandType").ToArray();
                if (slaveType == SlaveTypes.IEC61850Server)
                    arrCommandTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850Slave_AI_CommandType").ToArray();
                if (slaveType == SlaveTypes.DNP3SLAVE)
                    arrCommandTypes = Utils.getOpenProPlusHandle().getDataTypeValues("DNP_AI_CommandType").ToArray();
                if (arrCommandTypes != null && arrCommandTypes.Length > 0) cType = arrCommandTypes[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public static List<string> getVariartions(SlaveTypes slaveType)
        {
            if (slaveType == SlaveTypes.DNP3SLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("Variations");
            else
                return new List<string>();
        }
        public static List<string> getEventsVariations(SlaveTypes slaveType)
        {
            if (slaveType == SlaveTypes.DNP3SLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("EventsVariations");
            else
                return new List<string>();
        }
        public static List<string> getEventsClasses(SlaveTypes slaveType)
        {
            if (slaveType == SlaveTypes.DNP3SLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("EventsClasses");
            else
                return new List<string>();
        }


        public static List<string> getDataTypes(SlaveTypes slaveType)
        {
            if (slaveType == SlaveTypes.IEC104)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC104_AI_DataType");
            else if (slaveType == SlaveTypes.MODBUSSLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("MODBUSSlave_AI_DataType");
            //Namrata: 07/07/2017
            else if (slaveType == SlaveTypes.IEC101SLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC101Slave_AI_DataType");
            else if (slaveType == SlaveTypes.IEC61850Server)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850Slave_AI_DataType");
            //Ajay: 06/07/2018
            else if (slaveType == SlaveTypes.SPORTSLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("SportSlave_AI_DataType");
            //Namrata:02/04/2019
            else if (slaveType == SlaveTypes.MQTTSLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("MQTTSlave_AI_DataType");
            //Namrata: 30/05/2019
            else if (slaveType == SlaveTypes.SMSSLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("SMSSlave_AI_DataType");
            else
                return new List<string>();
        }
        public static List<string> getCommandTypes(SlaveTypes slaveType)
        {
            //Only MODBUSlave supports this...
            if (slaveType == SlaveTypes.MODBUSSLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("MODBUSSlave_AI_CommandType");
            if (slaveType == SlaveTypes.IEC61850Server)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850Slave_AI_CommandType");
            if (slaveType == SlaveTypes.DNP3SLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("DNP_AI_CommandType");
            else
                return new List<string>();
        }
        public AIMap(string aimName, List<KeyValuePair<string, string>> aimData, SlaveTypes sType)
        {
            string strRoutineName = "AIMap";
            try
            {
                slaveType = sType;
                SetSupportedAttributes();//IMP: Call only after slave types are set...
                SetSupportedDataTypes();
                SetSupportedCommandTypes();
                SetSupportedEventClass();
                SetSupportedEventVariation();
                SetSupportedVariation();
                try
                {
                    aimnType = (aimType)Enum.Parse(typeof(aimType), aimName);
                }
                catch (System.ArgumentException) { }
                if (aimData != null && aimData.Count > 0) //Parse n store values...
                {
                    foreach (KeyValuePair<string, string> aimkp in aimData)
                    {
                        try
                        {
                            if (this.GetType().GetProperty(aimkp.Key) != null) //Ajay: 03/07/2018
                            {
                                this.GetType().GetProperty(aimkp.Key).SetValue(this, aimkp.Value);
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
        public AIMap(XmlNode aimNode, SlaveTypes sType)
        {
            string strRoutineName = "AIMap";
            try
            {
                slaveType = sType;
                SetSupportedAttributes();//IMP: Call only after slave types are set...
                SetSupportedDataTypes();
                SetSupportedCommandTypes();
                SetSupportedEventClass();
                SetSupportedEventVariation();
                SetSupportedVariation();
                if (aimNode.Attributes != null)
                {
                    //First set the root element value...
                    try
                    {
                        aimnType = (aimType)Enum.Parse(typeof(aimType), aimNode.Name);
                    }
                    catch (System.ArgumentException) { }
                    //Namrata: 07/11/2017
                    if ((slaveType == SlaveTypes.DNP3SLAVE) || (slaveType == SlaveTypes.IEC101SLAVE) || (slaveType == SlaveTypes.IEC104) || (slaveType == SlaveTypes.MODBUSSLAVE) || (slaveType == SlaveTypes.UNKNOWN) || (slaveType == SlaveTypes.SPORTSLAVE)||(slaveType == SlaveTypes.SMSSLAVE)|| (slaveType == SlaveTypes.MQTTSLAVE))
                    {
                        foreach (XmlAttribute item in aimNode.Attributes)
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
                    if(slaveType == SlaveTypes.GRAPHICALDISPLAYSLAVE)
                    {
                        if (aimNode.Name == "AI")
                        {
                            foreach (XmlAttribute xmlattribute in aimNode.Attributes)
                            {
                                try
                                {
                                    if (xmlattribute.Name == "Unit")
                                    {
                                        unitID = aimNode.Attributes[3].Value;
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

                    //Namrata: 07/11/2017
                    #region If slaveType Type is IEC61850Server
                    if (slaveType == SlaveTypes.IEC61850Server)
                    {
                        if (aimNode.Name == "AI")
                        {
                            foreach (XmlAttribute xmlattribute in aimNode.Attributes)
                            {
                                try
                                {
                                    if (xmlattribute.Name == "ReportingIndex")
                                    {
                                        iec61850reportingindex = aimNode.Attributes[1].Value;
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
                    #endregion If slaveType Type is IEC61850Server
                }
                else if (aimNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = aimNode.Value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void updateAttributes(List<KeyValuePair<string, string>> aimData)
        {
            string strRoutineName = "AIMap:updateAttributes";
            try
            {
                if (aimData != null && aimData.Count > 0)
                {
                    foreach (KeyValuePair<string, string> aimkp in aimData)
                    {
                        try
                        {
                            if (this.GetType().GetProperty(aimkp.Key) != null) //Ajay: 03/07/2018
                            {
                                this.GetType().GetProperty(aimkp.Key).SetValue(this, aimkp.Value);
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
            rootNode = xmlDoc.CreateElement(aimnType.ToString());
            xmlDoc.AppendChild(rootNode);

            #region IEC61850Server
            if (slaveType == SlaveTypes.IEC61850Server)
            {
                foreach (string attr in arrAttributes)
                {
                    XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                    attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                    rootNode.Attributes.Append(attrName);
                }
                XmlAttribute attr2 = xmlDoc.CreateAttribute("ReportingIndex");
                attr2.Value = iec61850reportingindex.ToString();
                rootNode.Attributes.Append(attr2);

                XmlAttribute attrDataType = xmlDoc.CreateAttribute("DataType");
                attrDataType.Value = DataType.ToString();
                rootNode.Attributes.Append(attrDataType);

                XmlAttribute attrCommandType = xmlDoc.CreateAttribute("CommandType");
                attrCommandType.Value = CommandType.ToString();
                rootNode.Attributes.Append(attrCommandType);

                XmlAttribute attrDeadband = xmlDoc.CreateAttribute("Deadband");
                attrDeadband.Value = Deadband.ToString();
                rootNode.Attributes.Append(attrDeadband);

                XmlAttribute attrMultiplier = xmlDoc.CreateAttribute("Multiplier");
                attrMultiplier.Value = Multiplier.ToString();
                rootNode.Attributes.Append(attrMultiplier);

                XmlAttribute attrConstant = xmlDoc.CreateAttribute("Constant");
                attrConstant.Value = Multiplier.ToString();
                rootNode.Attributes.Append(attrConstant);

                XmlAttribute attrDescription = xmlDoc.CreateAttribute("Description");
                attrDescription.Value = Description.ToString();
                rootNode.Attributes.Append(attrDescription);
            }
            #endregion IEC61850Server

            #region GDisplaySlave
            if (slaveType == SlaveTypes.GRAPHICALDISPLAYSLAVE)
            {
                foreach (string attr in arrAttributes)
                {
                    XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                    attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                    rootNode.Attributes.Append(attrName);
                }
                XmlAttribute attrCellNo = xmlDoc.CreateAttribute("CellNo");
                attrCellNo.Value = CellNo.ToString();
                rootNode.Attributes.Append(attrCellNo);

                XmlAttribute attrWidget = xmlDoc.CreateAttribute("Widget");
                attrWidget.Value = Widget.ToString();
                rootNode.Attributes.Append(attrWidget);

                XmlAttribute attrUnit = xmlDoc.CreateAttribute("Unit");
                attrUnit.Value = UnitID.ToString();
                rootNode.Attributes.Append(attrUnit);

                XmlAttribute attrDescription = xmlDoc.CreateAttribute("Description");
                attrDescription.Value = Description.ToString();
                rootNode.Attributes.Append(attrDescription);

            }
            #endregion GDisplaySlave

            #region OtherSlave
            if ((slaveType == SlaveTypes.DNP3SLAVE) || (slaveType == SlaveTypes.IEC101SLAVE) || (slaveType == SlaveTypes.IEC104) || (slaveType == SlaveTypes.MODBUSSLAVE) || (slaveType == SlaveTypes.UNKNOWN) || (slaveType == SlaveTypes.SPORTSLAVE)|| (slaveType == SlaveTypes.MQTTSLAVE) || (slaveType == SlaveTypes.SMSSLAVE))
            {
                foreach (string attr in arrAttributes)
                {
                    XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                    attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                    rootNode.Attributes.Append(attrName);
                }
            }
            #endregion OtherSlave

            return rootNode;
        }
        public Control getView(List<string> kpArr)
        {
            return null;
        }
        public bool IsReindexed
        {
            get { return isReindexed; }
            set { isReindexed = value; }
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public string AINo
        {
            get { return aiNum.ToString(); }
            set { aiNum = Int32.Parse(value); }
        }
        public string ReportingIndex
        {
            get { return reportingIdx.ToString(); }
            set { reportingIdx = Int32.Parse(value); Globals.AIReportingIndex = Int32.Parse(value); }
        }
        public string DataType
        {
            get { return dType; }
            set
            {
                dType = value;
            }
        }
        public string CommandType
        {
            get { return cType; }
            set
            {
                cType = value;
            }
        }
        public string Deadband
        {
            get { return dBand.ToString(); }
            set
            {
                try
                {
                    dBand = Double.Parse(value);
                }
                catch (System.FormatException)
                {
                    dBand = 1;
                }
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
        public string Description
        {
            get { return description; }
            set { description = value; }
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
        public string IEC61850ReportingIndex
        {
            get { return iec61850reportingindex; }
            set
            {
                iec61850reportingindex = value;
            }
        }
        //Ajay:06/07/2018
        public string UnitID
        {
            get { return unitID; }
            set
            {
                unitID = value;
            }
        }
        public string Used
        {
            get { return (used == true ? "YES" : "NO"); }
            set
            {
                used = (value.ToLower() == "yes") ? true : false;
            }
        }
        //Ajay: 05/09/2018
        public string Event
        {
            get { return (sEvent == true ? "YES" : "NO"); }
            set
            {
                sEvent = (value.ToLower() == "yes") ? true : false;
            }
        }
        public string Unit
        {
            get { return unit; }
            set
            {
                unit = value;
            }
        }
        public string Key
        {
            get { return key; }
            set
            {
                key = value;
            }
        }
       
        public string EventClass
        {
            get { return eClass; }
            set
            {
                eClass = value;
            }
        }
        public string Variation
        {
            get { return variation; }
            set
            {
                variation = value;
            }
        }
        public string EventVariation
        {
            get { return evari; }
            set
            {
                evari = value;
            }
        }


        #region Graphical Display
        public string CellNo
        {
            get { return cellNo; }
            set { cellNo = value; }
        }
        public string Widget
        {
            get { return widget; }
            set { widget = value; }
        }
        #endregion GraphicalDisplay
    }

 }

