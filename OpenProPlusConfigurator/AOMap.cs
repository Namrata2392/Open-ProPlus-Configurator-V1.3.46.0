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
    * \brief     <b>AIMap</b> is a class to store info about AI mapping's for various slaves.
    * \details   This class stores info related to AI mapping's for various slaves. It retrieves/stores various 
    * mapping parameters like DataType, Deadband, etc. depending on the slave type this object belongs to. 
    * It also exports the XML node related to this object.
    * 
    */
    public class AOMap
    {
        #region Declaration
        enum aomType
        {
            AO
        };
        private string key = "";
        private string unit = "";
        private string description = "";
        private bool isNodeComment = false;
        private string comment = "";
        private bool isReindexed = false;
        private aomType aimnType = aomType.AO;
        private int aiNum = -1;
        private int reportingIdx = -1;
        private string dType = "";
        private string cType = "";
        private double dBand = 1;
        private double mult = 1;
        private double cnst = 0;
        private SlaveTypes slaveType = SlaveTypes.UNKNOWN;
        private string[] arrAttributes;// = { "AINo", "ReportingIndex", "DataType", "Deadband", "Multiplier", "Constant" };
        private string[] arrDataTypes;
        private string[] arrCommandTypes;
        //Ajay:06/07/2018
        private string unitID = "";
        private bool used = true;
        #endregion Declaration
        private void SetSupportedAttributes()
        {
            string strRoutineName = "AOMap: SetSupportedAttributes";
            try
            {
                if (slaveType == SlaveTypes.IEC104)
                    arrAttributes = new string[] { "AONo", "ReportingIndex", "DataType", "Deadband", "Multiplier", "Constant", "Description" };
                else if (slaveType == SlaveTypes.MODBUSSLAVE)
                    arrAttributes = new string[] { "AONo", "ReportingIndex", "DataType", "CommandType", "Deadband", "Multiplier", "Constant", "Description" };
                else if (slaveType == SlaveTypes.IEC101SLAVE)
                    arrAttributes = new string[] { "AONo", "ReportingIndex", "DataType", "Deadband", "Multiplier", "Constant", "Description" };
                else if (slaveType == SlaveTypes.IEC61850Server)
                    arrAttributes = new string[] { "AONo", "ReportingIndex", "DataType", "Deadband", "Multiplier", "Constant", "Description" };
                //Ajay: 13/07/2018
                else if (slaveType == SlaveTypes.SPORTSLAVE)
                    arrAttributes = new string[] { "AONo", "UnitID", "ReportingIndex", "DataType", "Deadband", "Multiplier", "Constant", "Used", "Description" };
                //Namrata:02/04/2019
                else if (slaveType == SlaveTypes.MQTTSLAVE)
                    arrAttributes = new string[] { "AONo", "ReportingIndex", "Key", "DataType", "Deadband", "Unit", "Multiplier", "Constant", "Description" };
                else if (slaveType == SlaveTypes.SMSSLAVE)
                    arrAttributes = new string[] { "AONo", "ReportingIndex", "DataType", "Deadband", "Multiplier", "Constant", "Description" };
                //Namrata: 12/11/2019
                else if (slaveType == SlaveTypes.DNP3SLAVE)
                    arrAttributes = new string[] { "AONo", "ReportingIndex","CommandType","Deadband", "Multiplier", "Constant", "Description" };
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetSupportedDataTypes()
        {
            string strRoutineName = "AOMap: SetSupportedDataTypes";
            try
            {
                if (slaveType == SlaveTypes.IEC104)
                    arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC104_AI_DataType").ToArray();
                else if (slaveType == SlaveTypes.MODBUSSLAVE)
                    arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("MODBUSSlave_AO_DataType").ToArray();
                //Namrata: 24/11/2017
                else if (slaveType == SlaveTypes.IEC101SLAVE)
                    arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC101Slave_AO_DataType").ToArray();
                else if (slaveType == SlaveTypes.IEC61850Server)
                    arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("MODBUSSlave_AO_DataType").ToArray();
                //Ajay:06/07/2018
                else if (slaveType == SlaveTypes.SPORTSLAVE)
                    arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("SportSlave_AO_DataType").ToArray();
                //Namrata:02/04/2019
                else if (slaveType == SlaveTypes.MQTTSLAVE)
                    arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("MQTTSlave_AO_DataType").ToArray();
                //Namrata: 30/05/2019
                else if (slaveType == SlaveTypes.SMSSLAVE)
                    arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("SMSSlave_AO_DataType").ToArray();
                if (arrDataTypes == null) { }
                else
                { if (arrDataTypes.Length > 0) dType = arrDataTypes[0]; }
                //    if (arrDataTypes.Length > 0) dType = arrDataTypes[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetSupportedCommandTypes()
        {
            string strRoutineName = "AOMap: SetSupportedCommandTypes";
            try
            {
                //Only MODBUSlave supports this...
                if (slaveType == SlaveTypes.MODBUSSLAVE)
                {
                    arrCommandTypes = Utils.getOpenProPlusHandle().getDataTypeValues("MODBUSSlave_AO_CommandType").ToArray();
                }
                { 
                if (slaveType == SlaveTypes.DNP3SLAVE)
                    arrCommandTypes = Utils.getOpenProPlusHandle().getDataTypeValues("DNP_AO_CommandType").ToArray();
            }
                if (arrCommandTypes != null && arrCommandTypes.Length > 0) cType = arrCommandTypes[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static List<string> getDataTypes(SlaveTypes slaveType)
        {
            if (slaveType == SlaveTypes.IEC104)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC104_AI_DataType");
            else if (slaveType == SlaveTypes.MODBUSSLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("MODBUSSlave_AO_DataType");
            //Namrata: 24/11/2017
            else if (slaveType == SlaveTypes.IEC101SLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC101Slave_AO_DataType");
            else if (slaveType == SlaveTypes.IEC61850Server)
                return Utils.getOpenProPlusHandle().getDataTypeValues("MODBUSSlave_AO_DataType");
            //Ajay:06/07/2018
            else if (slaveType == SlaveTypes.SPORTSLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("SportSlave_AO_DataType");
            //Namrata:02/04/2019
            else if (slaveType == SlaveTypes.MQTTSLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("MQTTSlave_AO_DataType");
            //Namrata: 30/05/2019
            else if (slaveType == SlaveTypes.SMSSLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("SMSSlave_AO_DataType");
            else
                return new List<string>();
        }
        public static List<string> getCommandTypes(SlaveTypes slaveType)
        {
            //Only MODBUSlave supports this...
            if (slaveType == SlaveTypes.MODBUSSLAVE)
            { return Utils.getOpenProPlusHandle().getDataTypeValues("MODBUSSlave_AO_CommandType"); }
            if (slaveType == SlaveTypes.DNP3SLAVE)
            { return Utils.getOpenProPlusHandle().getDataTypeValues("DNP_AO_CommandType"); }
            else
                return new List<string>();
        }
        public AOMap(string aimName, List<KeyValuePair<string, string>> aimData, SlaveTypes sType)
        {
            string strRoutineName = "AOMap";
            try
            {
                slaveType = sType;
                SetSupportedAttributes();//IMP: Call only after slave types are set...
                SetSupportedDataTypes();
                SetSupportedCommandTypes();
                //First set the root element value...
                try
                {
                    aimnType = (aomType)Enum.Parse(typeof(aomType), aimName);
                }
                catch (System.ArgumentException) { }
              
                //Parse n store values...
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
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", aimkp.Key, aimkp.Value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public AOMap(XmlNode aimNode, SlaveTypes sType)
        {
            string strRoutineName = "AOMap";
            try
            {
                slaveType = sType;
                SetSupportedAttributes(); //IMP: Call only after slave types are set...
                SetSupportedDataTypes();
                SetSupportedCommandTypes();
                
                if (aimNode.Attributes != null)
                {
                    //First set the root element value...
                    try
                    {
                        aimnType = (aomType)Enum.Parse(typeof(aomType), aimNode.Name);
                    }
                    catch (System.ArgumentException) { }
                    
                    foreach (XmlAttribute item in aimNode.Attributes)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", item.Name, item.Value);
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
            string strRoutineName = "AOMap:updateAttributes";
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
            if (arrAttributes != null && arrAttributes.Count() > 0)//Ajay: 12/07/2018
            {
                foreach (string attr in arrAttributes)
                {
                    XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                    attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                    rootNode.Attributes.Append(attrName);
                }
            }
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
        public string AONo
        {
            get { return aiNum.ToString(); }
            set { aiNum = Int32.Parse(value); }
        }
        public string ReportingIndex
        {
            get { return reportingIdx.ToString(); }
            set { reportingIdx = Int32.Parse(value); Globals.AOReportingIndex = Int32.Parse(value); }
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
    }
}
