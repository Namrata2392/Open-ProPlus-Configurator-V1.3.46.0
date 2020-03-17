﻿using System;
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
    * \brief     <b>DIMap</b> is a class to store info about DI mapping's for various slaves.
    * \details   This class stores info related to DI mapping's for various slaves. It retrieves/stores various 
    * mapping parameters like DataType, BitPos, etc. depending on the slave type this object belongs to. 
    * It also exports the XML node related to this object.
    * 
    */
    public class DIMap
    {
        enum dimType
        {
            DI
        };
        #region Declaration
        //Namrata: 02/11/2017
        private string eClass = "";
        private string variation = "";
        private string evari = "";
        private string key = "";
        private string iec61850reportingindex = "";
        private string description = "";
        private bool isNodeComment = false;
        private string comment = "";
        private bool isReindexed = false;
        private dimType dimnType = dimType.DI;
        private int diNum = -1;
        private int reportingIdx = -1;
        private string dType = "";
        private string cType = "";
        private int bitPos = -1;
        private bool complement = true;
        private SlaveTypes slaveType = SlaveTypes.UNKNOWN;
        private string[] arrAttributes;
        private string[] arrDataTypes;
        private string[] arrCommandTypes;
        //Ajay:06/07/2018
        private string unitID = "";
        private bool used = true;
        private string cellNo = "";
        private string widget = "";

        private string[] arrvariation;
        private string[] arrEVariation;
        private string[] arrEClass;
        private string Vari = "";
        private string EVar = "";
        private string ECls = "";
        #endregion Declaration
        private void SetSupportedAttributes()
        {
            if (slaveType == SlaveTypes.IEC104)
                arrAttributes = new string[] { "DINo", "ReportingIndex", "DataType", "BitPos", "Complement", "Description" };
            else if (slaveType == SlaveTypes.MODBUSSLAVE)
                arrAttributes = new string[] { "DINo", "ReportingIndex", "DataType", "CommandType", "BitPos", "Complement", "Description" };
            //Namrata:7/7/2017
            if (slaveType == SlaveTypes.IEC101SLAVE)
                arrAttributes = new string[] { "DINo", "ReportingIndex", "DataType", "BitPos", "Complement", "Description" };
            else if (slaveType == SlaveTypes.IEC61850Server)
                arrAttributes = new string[] { "DINo" };
            //Ajay: 06/07/2018
            else if (slaveType == SlaveTypes.SPORTSLAVE)
                arrAttributes = new string[] { "DINo", "UnitID", "ReportingIndex", "DataType", "BitPos", "Complement", "Used", "Description" };
            //Ajay: 06/07/2018
            else if (slaveType == SlaveTypes.MQTTSLAVE)
                arrAttributes = new string[] { "DINo", "ReportingIndex", "Key", "DataType", "BitPos", "Complement", "Description" };
            //Namrata: 30/05/2019
            if (slaveType == SlaveTypes.SMSSLAVE)
                arrAttributes = new string[] { "DINo", "ReportingIndex", "DataType", "BitPos", "Complement", "Description" };
            else if (slaveType == SlaveTypes.GRAPHICALDISPLAYSLAVE)
                arrAttributes = new string[] { "DINo" };
            //Namrata: 12/11/2019
            else if (slaveType == SlaveTypes.DNP3SLAVE)
                arrAttributes = new string[] { "DINo", "ReportingIndex", "CommandType", "EventClass", "Variation", "EventVariation", "BitPos", "Description" };
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


        private void SetSupportedDataTypes()
        {
            if (slaveType == SlaveTypes.IEC104)
                arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC104_DI_DataType").ToArray();
            else if (slaveType == SlaveTypes.MODBUSSLAVE)
                arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("MODBUSSlave_DI_DataType").ToArray();
            else if (slaveType == SlaveTypes.IEC61850Server)
                arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850Slave_DI_DataType").ToArray();
            //Namrata:7/7/2017
            else if (slaveType == SlaveTypes.IEC101SLAVE)
                arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC101Slave_DI_DataType").ToArray();
            else if (slaveType == SlaveTypes.SPORTSLAVE)
                arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("SportSlave_DI_DataType").ToArray();
            //Namrata:02/04/2019
            else if (slaveType == SlaveTypes.MQTTSLAVE)
                arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("MQTTSlave_DI_DataType").ToArray();
            //Namrata: 30/05/2019
            else if (slaveType == SlaveTypes.SMSSLAVE)
                arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("SMSSlave_DI_DataType").ToArray();
            if (arrDataTypes == null){ }
                else
                { if (arrDataTypes.Length > 0) dType = arrDataTypes[0]; }
        }
        private void SetSupportedCommandTypes()
        {
            //Only MODBUSlave supports this...
            if (slaveType == SlaveTypes.MODBUSSLAVE)
                arrCommandTypes = Utils.getOpenProPlusHandle().getDataTypeValues("MODBUSSlave_DI_CommandType").ToArray();
            if (slaveType == SlaveTypes.IEC61850Server)
                arrCommandTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850Slave_DI_CommandType").ToArray();
            if (slaveType == SlaveTypes.DNP3SLAVE)
                arrCommandTypes = Utils.getOpenProPlusHandle().getDataTypeValues("DNP_DI_CommandType").ToArray();
            if (arrCommandTypes != null && arrCommandTypes.Length > 0) cType = arrCommandTypes[0];
        }
        public static List<string> getDataTypes(SlaveTypes slaveType)
        {
            if (slaveType == SlaveTypes.IEC104)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC104_DI_DataType");
            else if (slaveType == SlaveTypes.MODBUSSLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("MODBUSSlave_DI_DataType");
            else if (slaveType == SlaveTypes.IEC61850Server)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850Slave_DI_DataType");
            //Namrata:7/7/2017
            else if (slaveType == SlaveTypes.IEC101SLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC101Slave_DI_DataType");
          
            else if (slaveType == SlaveTypes.SPORTSLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("SportSlave_DI_DataType");
            //Namrata:02/04/2019
            else if (slaveType == SlaveTypes.MQTTSLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("MQTTSlave_DI_DataType");
            //Namrata: 30/05/2019
            else if (slaveType == SlaveTypes.SMSSLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("SMSSlave_DI_DataType");
            else
                return new List<string>();
        }
        public static List<string> getCommandTypes(SlaveTypes slaveType)
        {
            //Only MODBUSlave supports this...
            if (slaveType == SlaveTypes.MODBUSSLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("MODBUSSlave_DI_CommandType");
            if (slaveType == SlaveTypes.IEC61850Server)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850Slave_DI_CommandType");
            if (slaveType == SlaveTypes.DNP3SLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("DNP_DI_CommandType");
            else
                return new List<string>();
        }
        public DIMap(string dimName, List<KeyValuePair<string, string>> dimData, SlaveTypes sType)
        {
            slaveType = sType;
            SetSupportedAttributes();//IMP: Call only after slave types are set...
            SetSupportedDataTypes();
            SetSupportedCommandTypes();
            SetSupportedEventClass();
            SetSupportedEventVariation();
            SetSupportedVariation();
            //First set the root element value...
            try
            {
                dimnType = (dimType)Enum.Parse(typeof(dimType), dimName);
            }
            catch (System.ArgumentException)
            {
                Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", dimName);
            }
            //Parse n store values...
            if (dimData != null && dimData.Count > 0)
            {
                foreach (KeyValuePair<string, string> dimkp in dimData)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", dimkp.Key, dimkp.Value);
                    try
                    {
                        if (this.GetType().GetProperty(dimkp.Key) != null) //Ajay: 02/07/2018
                        {
                            this.GetType().GetProperty(dimkp.Key).SetValue(this, dimkp.Value);
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "DIMap: Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", dimkp.Key, dimkp.Value);
                    }
                }
                Utils.Write(VerboseLevel.DEBUG, "\n");
            }
        }
        public DIMap(XmlNode dimNode, SlaveTypes sType)
        {
            slaveType = sType;
            SetSupportedAttributes();//IMP: Call only after slave types are set...
            SetSupportedDataTypes();
            SetSupportedCommandTypes();
            SetSupportedEventClass();
            SetSupportedEventVariation();
            SetSupportedVariation();
            //Parse n store values...
            Utils.WriteLine(VerboseLevel.DEBUG, "dimNode name: '{0}'", dimNode.Name);
            if (dimNode.Attributes != null)
            {
                //First set the root element value...
                try
                {
                    dimnType = (dimType)Enum.Parse(typeof(dimType), dimNode.Name);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", dimNode.Name);
                }
                if ((slaveType == SlaveTypes.DNP3SLAVE) || (slaveType == SlaveTypes.GRAPHICALDISPLAYSLAVE) || (slaveType == SlaveTypes.IEC101SLAVE) || (slaveType == SlaveTypes.IEC104) || (slaveType == SlaveTypes.MODBUSSLAVE) || (slaveType == SlaveTypes.SPORTSLAVE) ||(slaveType == SlaveTypes.MQTTSLAVE)|| (slaveType == SlaveTypes.SMSSLAVE)) //Ajay: 10/08/2018 SPORTSLAVE added to condition. SPORT mapping not showing by Aditya K mail dtd. 09/08/2018
                {
                    foreach (XmlAttribute item in dimNode.Attributes)
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
                            Utils.WriteLine(VerboseLevel.WARNING, "DIMap: Field doesn't exist. XML and class fields mismatch!!! Key: {0} value: {1}", item.Name, item.Value);
                        }
                    }
                }
                //Namrata: 07/11/2017
                // If slaveType Type is IEC61850Server
                if (slaveType == SlaveTypes.IEC61850Server)
                {
                    if (dimNode.Name == "DI")
                    {
                        foreach (XmlAttribute xmlattribute in dimNode.Attributes)
                        {
                            Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", xmlattribute.Name, xmlattribute.Value);
                            try
                            {

                                if (xmlattribute.Name == "ReportingIndex")
                                {
                                    iec61850reportingindex = dimNode.Attributes[1].Value;
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
            else if (dimNode.NodeType == XmlNodeType.Comment)
            {
                isNodeComment = true;
                comment = dimNode.Value;
            }
        }
        public void updateAttributes(List<KeyValuePair<string, string>> dimData)
        {
            if (dimData != null && dimData.Count > 0)
            {
                foreach (KeyValuePair<string, string> dimkp in dimData)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", dimkp.Key, dimkp.Value);
                    try
                    {
                        if (this.GetType().GetProperty(dimkp.Key) != null) //Ajay: 03/07/2018
                        {
                            this.GetType().GetProperty(dimkp.Key).SetValue(this, dimkp.Value);
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "DIMap: Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {0}", dimkp.Key, dimkp.Value);
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
            rootNode = xmlDoc.CreateElement(dimnType.ToString());
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

                XmlAttribute attrDeadband = xmlDoc.CreateAttribute("BitPos");
                attrDeadband.Value = BitPos.ToString();
                rootNode.Attributes.Append(attrDeadband);

                XmlAttribute attrMultiplier = xmlDoc.CreateAttribute("Complement");
                attrMultiplier.Value = Complement.ToString();
                rootNode.Attributes.Append(attrMultiplier);

                XmlAttribute attrConstant = xmlDoc.CreateAttribute("Description");
                attrConstant.Value = Description.ToString();
                rootNode.Attributes.Append(attrConstant);
            }
            #endregion IEC61850Server

            #region SPORTSLAVE,IEC101SLAVE,IEC104,MODBUSSLAVE,UNKNOWN
            if ((slaveType == SlaveTypes.DNP3SLAVE) || (slaveType == SlaveTypes.SPORTSLAVE) || (slaveType == SlaveTypes.IEC101SLAVE) || (slaveType == SlaveTypes.IEC104) || (slaveType == SlaveTypes.MODBUSSLAVE) || (slaveType == SlaveTypes.UNKNOWN)||(slaveType == SlaveTypes.MQTTSLAVE) || (slaveType == SlaveTypes.SMSSLAVE))
            {
                foreach (string attr in arrAttributes)
                {
                    XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                    attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                    rootNode.Attributes.Append(attrName);
                }
            }
            #endregion SPORTSLAVE,IEC101SLAVE,IEC104,MODBUSSLAVE,UNKNOWN

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

                //XmlAttribute attrUnit = xmlDoc.CreateAttribute("Unit");
                //attrUnit.Value = UnitID.ToString();
                //rootNode.Attributes.Append(attrUnit);

                XmlAttribute attrComplement = xmlDoc.CreateAttribute("Complement");
                attrComplement.Value = Complement.ToString();
                rootNode.Attributes.Append(attrComplement);

                XmlAttribute attrDescription = xmlDoc.CreateAttribute("Description");
                attrDescription.Value = Description.ToString();
                rootNode.Attributes.Append(attrDescription);
            }
            #endregion GDisplaySlave
            return rootNode;
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
        public string DINo
        {
            get { return diNum.ToString(); }
            set { diNum = Int32.Parse(value); }
        }
        public string ReportingIndex
        {
            get { return reportingIdx.ToString(); }
            set { reportingIdx = Int32.Parse(value); Globals.DIReportingIndex = Int32.Parse(value); }
        }
        public string DataType
        {
            get { return dType; }
            set
            {
                dType = value;
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
        public string CommandType
        {
            get { return cType; }
            set
            {
                cType = value;
            }
        }
        public string BitPos
        {
            get { return bitPos.ToString(); }
            set { bitPos = Int32.Parse(value); }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string Complement
        {
            get { return (complement == true ? "YES" : "NO"); }
            set
            {
                complement = (value.ToLower() == "yes") ? true : false;
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
        public string Key
        {
            get { return key; }
            set
            {
                key = value;
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
