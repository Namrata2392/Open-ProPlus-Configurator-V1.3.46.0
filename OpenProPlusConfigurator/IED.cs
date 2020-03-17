using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
using System.Threading;
namespace OpenProPlusConfigurator
{
    /**
    * \brief     <b>IED</b> is a class to store data for all data points and there corresponding mapping.
    * \details   This class stores all data points and there corresponding mapping info. The mapping info
    * per slave per data point is stored. It also stores parameters related to IED like Clock Sync Interval,
     * Refresh Interval, GiTime, etc. It also exports the XML node related to this object.
    * 
    */
    public class IED
    {
        #region Declarations
        enum iedType
        {
            IED
        };
        //Task TsAI = new Task();
        //, TsDI, TsDO;
        private bool isNodeComment = false;
        private string comment = "";
        private iedType iType = iedType.IED;
        public int unitID = -1;
        private int asduAddr = -1;
        private string device = "";
        private string remoteIP = "";
        private int tcpPort = 0;
        private int retries = 3;
        private int timeoutms = 100;
        private string descr = "";

        private bool dr = false;
        private MasterTypes masterType = MasterTypes.UNKNOWN;
        private int masterNo = -1;
        private string linkaddresssize = "";
        private AIMap aimap = null;// = new AIConfiguration();
        private AIConfiguration aicNode = null;// = new AIConfiguration();
        private AOConfiguration aocNode = null;// = new AIConfiguration();
        private DIConfiguration dicNode = null;// = new DIConfiguration();
        private DOConfiguration docNode = null;// = new DOConfiguration();
        private ENConfiguration encNode = null;// = new ENConfiguration();
        private RCBConfiguration RcbNode = null;// = new RCBConfiguration();
        private LPFileConfiguration LPFileNode = null;// = new LPFileConfiguration();
        private SlaveMapping smNode = null;// = new SlaveMapping(iec104Grp, ref dicNode);


        private string asSize = "";
        private string ioaSz = "";
        private string cSize = "";

        //Namrata:22/05/2018
        private string t0 = "";
        private string t1 = "";
        private string t2 = "";
        private string t3 = "";
        private string w = "";
        private string k = "";

        //Ajay: 31/07/2018
        private string tapratio = "";
        private string txoffsetcurrent = "";

        //Ajay: 17/01/2019
        private string timestmpType = "";

        ucIED ucied = new ucIED();
        ucMaster61850Server UcMaster61850Server = new ucMaster61850Server();
        private TreeNode IEDTreeNode;

        private string[] arrAttributes;// = { "UnitID", "ASDUAddr", "Device", "Retries", "TimeOutMS", "Description", "DR" };
        private string[] arrASDUSizes;
        private string[] arrIOASizes;
        private string[] arrCOTSizes;
        private string[] arrLinkAddressSizes;
        //Namrata:30/01/2019
        private string[] arrSPORTType;
        private string icdFilePath = "";
        //Ajay:09/07/2018
        private string redundantIP = "";

        //Namrata:31/01/2019
        private string sportType = "";
        private string lastAI = "";
        private string lastDI = "";
        private string lastDO = "";
        private string timestamptype = "";
        private string timestampaccuracy = "";
        private string windowtime = "";
        private string debouncetime = "";
        private string pulsewidthtimeout = "";

        #endregion Declarations

        #region Supported Attributes
        private void SetSupportedAttributes()
        {
            string strRoutineName = "SetSupportedAttributes";
            try
            {
                if (masterType == MasterTypes.IEC103)
                    arrAttributes = new string[] { "UnitID", "ASDUAddr", "Device", "Retries", "TimeOutMS", "Description", "DR" };
                else if (masterType == MasterTypes.IEC101)
                    arrAttributes = new string[] { "UnitID", "ASDUAddr", "Device", "Retries", "TimeOutMS", "LinkAddressSize", "ASDUSize", "IOASize", "COTSize", "Description", };
                else if (masterType == MasterTypes.MODBUS)
                    arrAttributes = new string[] { "UnitID", "Device", "RemoteIP", "TCPPort", "Retries", "TimeOutMS", "Description" };
                else if (masterType == MasterTypes.Virtual)
                    arrAttributes = new string[] { "UnitID", "Device", "Description" };
                //Namarta:11/7/2017
                else if (masterType == MasterTypes.ADR)
                    arrAttributes = new string[] { "UnitID", "Device", "Description", "Retries", "TimeOutMS" };
                else if (masterType == MasterTypes.IEC61850Client)
                    arrAttributes = new string[] { "UnitID", "Device", "RemoteIP", "TCPPort", "Retries", "TimeOutMS", "Description", "SCLName", "TimestampType" }; //Ajay: 17/01/2019
                //Namrata:17/05/2018
                else if (masterType == MasterTypes.IEC104)
                    arrAttributes = new string[] { "UnitID", "RemoteIP", "RedundantIP", "TCPPort", "ASDUAddr", "ASDUSize", "IOASize", "COTSize", "Retries", "TimeOutMS", "T0", "T1", "T2", "T3", "W", "K", "Device", "Description" }; //Ajay: 19/09/2018 TcpPort addded
                else if (masterType == MasterTypes.SPORT)
                    arrAttributes = new string[] { "UnitID", "Device", "Retries", "TimeOutMS", "SportType", "IOASize", "LastAI", "LastDI", "LastDO", "TimestampType", "TimestampAccuracy", "WindowTime", "DebounceTime", "PulseWidthTimeout", "Description" };
                //Ajay: 08/09/2018
                else if (masterType == MasterTypes.LoadProfile)
                    arrAttributes = new string[] { "UnitID", "Device", "DefaultTapRatio", "TXOffsetCurrent", "Description" };
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetSupportedLinkAddressSizes()
        {
            string strRoutineName = "SetSupportedLinkAddressSizes";
            try
            {
                arrLinkAddressSizes = Utils.getOpenProPlusHandle().getDataTypeValues("TwoByte").ToArray();
                if (arrLinkAddressSizes.Length > 0) asSize = arrLinkAddressSizes[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetSupportedSPORTType()
        {
            string strRoutineName = "SetSupportedSPORTType";
            try
            {
                arrSPORTType = Utils.getOpenProPlusHandle().getDataTypeValues("mSPORT_Type").ToArray();
                if (arrSPORTType != null && arrSPORTType.Length > 0) asSize = arrSPORTType[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static List<string> getSPORTTypes()
        {
            return Utils.getOpenProPlusHandle().getDataTypeValues("mSPORT_Type");
        }
        public static List<string> getLinkAddresssizes()
        {
            return Utils.getOpenProPlusHandle().getDataTypeValues("TwoByte");
        }
        //Namrata:7/7/2017
        private void SetSupportedASDUSizes()
        {
            string strRoutineName = "SetSupportedASDUSizes";
            try
            {
                arrASDUSizes = Utils.getOpenProPlusHandle().getDataTypeValues("TwoByte").ToArray();
                if (arrASDUSizes.Length > 0) asSize = arrASDUSizes[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetSupportedIOASizes()
        {
            string strRoutineName = "SetSupportedIOASizes";
            try
            {
                arrIOASizes = Utils.getOpenProPlusHandle().getDataTypeValues("ThreeByte").ToArray();
                if (arrIOASizes.Length > 0) ioaSz = arrIOASizes[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetSupportedCOTSizes()
        {
            string strRoutineName = "SetSupportedCOTSizes";
            try
            {
                arrCOTSizes = Utils.getOpenProPlusHandle().getDataTypeValues("TwoByte").ToArray();
                if (arrCOTSizes.Length > 0) cSize = arrCOTSizes[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static List<string> getASDUsizes()
        {
            return Utils.getOpenProPlusHandle().getDataTypeValues("TwoByte");
        }
        public static List<string> getIOAsizes()
        {
            return Utils.getOpenProPlusHandle().getDataTypeValues("ThreeByte");
        }
        public static List<string> getCOTsizes()
        {
            return Utils.getOpenProPlusHandle().getDataTypeValues("TwoByte");
        }
        #endregion Supported Attributes

        #region Virtual Master..
        public IED(int unit, string devicename, TreeNode tn, MasterTypes mType, int mNo)
        {
            string strRoutineName = "IED";
            try
            {
                masterType = mType;
                masterNo = mNo;
                SetSupportedAttributes();//IMP: Call only after master types are set...
                SetSupportedASDUSizes();
                SetSupportedIOASizes();
                SetSupportedCOTSizes();
                SetSupportedLinkAddressSizes();
                SetSupportedSPORTType();
                if (tn != null) IEDTreeNode = tn;
                LPFileNode = new LPFileConfiguration(mType, mNo, unit); //Ajay: 10/09/2018
                aicNode = new AIConfiguration(mType, mNo, unit);
                aocNode = new AOConfiguration(mType, mNo, unit);
                dicNode = new DIConfiguration(mType, mNo, unit);
                docNode = new DOConfiguration(mType, mNo, unit);
                encNode = new ENConfiguration(mType, mNo, unit);
                smNode = new SlaveMapping(aicNode, aocNode,/*ref */dicNode, docNode, encNode);
                addListHeaders();
                UnitID = unit.ToString();
                Device = devicename;
                if (tn != null) tn.Text = "IED " + this.Description;
                //Ajay: 10/09/2018
                if (masterType == MasterTypes.LoadProfile)
                {
                    if (tn != null) tn.Nodes.Add("LPFILE", "LPFILE", "LPFILE", "LPFILE");
                    LPFileNode.parseLPFileCNode(null, false);
                }
                if (tn != null) tn.Nodes.Add("AI", "AI", "AI", "AI");
                aicNode.parseAICNode(null, false);
                if (tn != null) tn.Nodes.Add("AO", "AO", "AO", "AO");
                aocNode.parseAOCNode(null, false);
                if (tn != null) tn.Nodes.Add("DI", "DI", "DI", "DI");
                dicNode.parseDICNode(null, false);
                if (tn != null) tn.Nodes.Add("DO", "DO", "DO", "DO");
                docNode.parseDOCNode(null, false);
                if (tn != null) tn.Nodes.Add("EN", "EN", "EN", "EN");
                encNode.parseENCNode(null, false);
                refreshList();
                ucied.lblIED.Text = "IED Details (Unit: " + unitID.ToString() + ")";

            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion Virtual Master..

        #region IEC61850Client Master...
        public IED(string iedName, string devicename, List<KeyValuePair<string, string>> iedData, TreeNode tn, MasterTypes mType, int mNo)
        {
            string strRoutineName = "IED";
            try
            {
                masterType = mType;
                masterNo = mNo;
                SetSupportedAttributes();//IMP: Call only after master types are set...
                SetSupportedASDUSizes();
                SetSupportedIOASizes();
                SetSupportedCOTSizes();
                SetSupportedLinkAddressSizes();
                //SetSupportedSPORTType();
                if (tn != null) IEDTreeNode = tn;
                addListHeaders();
                Device = devicename;
                try
                {
                    iType = (iedType)Enum.Parse(typeof(iedType), iedName);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", iedName);
                }

                if (iedData != null && iedData.Count > 0)  //Parse n store values...
                {
                    foreach (KeyValuePair<string, string> iedkp in iedData)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", iedkp.Key, iedkp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(iedkp.Key) != null) //Ajay: 03/07/2018
                            {
                                this.GetType().GetProperty(iedkp.Key).SetValue(this, iedkp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", iedkp.Key, iedkp.Value);
                        }
                    }
                    Utils.Write(VerboseLevel.DEBUG, "\n");
                }
                if (tn != null) tn.Text = "IED " + this.Description;
                RcbNode = new RCBConfiguration(mType, mNo, Int32.Parse(this.UnitID));
                LPFileNode = new LPFileConfiguration(mType, mNo, Int32.Parse(this.UnitID)); //Ajay: 10/09/2018
                aicNode = new AIConfiguration(mType, mNo, Int32.Parse(this.UnitID));
                aocNode = new AOConfiguration(mType, mNo, Int32.Parse(this.UnitID));
                dicNode = new DIConfiguration(mType, mNo, Int32.Parse(this.UnitID));
                docNode = new DOConfiguration(mType, mNo, Int32.Parse(this.UnitID));
                encNode = new ENConfiguration(mType, mNo, Int32.Parse(this.UnitID));
                smNode = new SlaveMapping(aicNode, null, /*ref */dicNode, docNode, encNode);
                if (tn != null) tn.Nodes.Add("RCB", "RCB", "RCB", "RCB");
                RcbNode.parseAICNode(null, false);
                if (tn != null) tn.Nodes.Add("AI", "AI", "AI", "AI");
                aicNode.parseAICNode(null, false);
                if (tn != null) tn.Nodes.Add("AO", "AO", "AO", "AO");
                aocNode.parseAOCNode(null, false);
                if (tn != null) tn.Nodes.Add("DI", "DI", "DI", "DI");
                dicNode.parseDICNode(null, false);
                if (tn != null) tn.Nodes.Add("DO", "DO", "DO", "DO");
                docNode.parseDOCNode(null, false);
                if (tn != null) tn.Nodes.Add("EN", "EN", "EN", "EN");
                encNode.parseENCNode(null, false);
                refreshList();
                ucied.lblIED.Text = "IED Details (Unit: " + unitID.ToString() + ")";
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion IEC61850Client Master...

        public IED(string iedName, List<KeyValuePair<string, string>> iedData, TreeNode tn, MasterTypes mType, int mNo)
        {
            string strRoutineName = "IED";
            try
            {
                masterType = mType;
                masterNo = mNo;
                SetSupportedAttributes();//IMP: Call only after master types are set...
                SetSupportedASDUSizes();
                SetSupportedIOASizes();
                SetSupportedCOTSizes();
                SetSupportedLinkAddressSizes();
                SetSupportedSPORTType();
                if (tn != null) IEDTreeNode = tn;
                addListHeaders();
                try
                {
                    iType = (iedType)Enum.Parse(typeof(iedType), iedName);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", iedName);
                }
                if (iedData != null && iedData.Count > 0)
                {
                    foreach (KeyValuePair<string, string> iedkp in iedData)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", iedkp.Key, iedkp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(iedkp.Key) != null) //Ajay: 03/07/2018
                            {
                                this.GetType().GetProperty(iedkp.Key).SetValue(this, iedkp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", iedkp.Key, iedkp.Value);
                        }
                    }
                    Utils.Write(VerboseLevel.DEBUG, "\n");
                }
                if (tn != null) tn.Text = "IED " + this.Description;

                aicNode = new AIConfiguration(mType, mNo, Int32.Parse(this.UnitID));
                aocNode = new AOConfiguration(mType, mNo, Int32.Parse(this.UnitID));
                dicNode = new DIConfiguration(mType, mNo, Int32.Parse(this.UnitID));
                docNode = new DOConfiguration(mType, mNo, Int32.Parse(this.UnitID));
                encNode = new ENConfiguration(mType, mNo, Int32.Parse(this.UnitID));
                //Ajay: 10/09/2018
                if (masterType == MasterTypes.LoadProfile)
                {
                    LPFileNode = new LPFileConfiguration(mType, mNo, Int32.Parse(this.UnitID));
                }
                smNode = new SlaveMapping(aicNode, aocNode, dicNode, docNode, encNode);
                if (masterType == MasterTypes.LoadProfile)
                {
                    if (tn != null) tn.Nodes.Add("LPFILE", "LPFILE", "LPFILE", "LPFILE");
                    LPFileNode.parseLPFileCNode(null, false);
                }
                if (tn != null) tn.Nodes.Add("AI", "AI", "AI", "AI");
                aicNode.parseAICNode(null, false);
                if (tn != null) tn.Nodes.Add("AO", "AO", "AO", "AO");
                aocNode.parseAOCNode(null, false);
                if (tn != null) tn.Nodes.Add("DI", "DI", "DI", "DI");
                dicNode.parseDICNode(null, false);
                if (tn != null) tn.Nodes.Add("DO", "DO", "DO", "DO");
                docNode.parseDOCNode(null, false);
                if (tn != null) tn.Nodes.Add("EN", "EN", "EN", "EN");
                encNode.parseENCNode(null, false);
                refreshList();
                ucied.lblIED.Text = "IED Details (Unit: " + unitID.ToString() + ")";
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      
       
        public IED(XmlNode iNode, TreeNode tn, MasterTypes mType, int mNo, bool imported)
        {
            string strRoutineName = "IED";
            try
            {
                masterType = mType;
                masterNo = mNo;
                SetSupportedAttributes();
                if (tn != null) IEDTreeNode = tn;
                addListHeaders();
                Utils.WriteLine(VerboseLevel.DEBUG, "iNode name: '{0}'", iNode.Name);
                if (iNode.Attributes != null)
                {
                    try
                    {
                        iType = (iedType)Enum.Parse(typeof(iedType), iNode.Name);
                    }
                    catch (System.ArgumentException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", iNode.Name);
                    }
                    foreach (XmlAttribute item in iNode.Attributes)
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
                else if (iNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = iNode.Value;
                }
                if (imported)//Generate a new unique id...
                {
                    if (masterType == MasterTypes.ADR)//ADR
                    {
                    }
                    else if (masterType == MasterTypes.IEC101)//IEC101
                    {
                        //Namrata: 20/11/2017
                        if (Utils.IEC101MasteriedList.Select(x => x.unitID).ToList().Contains(this.unitID))
                        {
                            this.UnitID = (Globals.getIEC101IEDNo(masterNo) + 1).ToString();
                            //this.Description = "IEC101_" + (Globals.MasterNo + 1).ToString();
                        }
                        else
                        {
                            this.UnitID = (Globals.getIEC101IEDNo(masterNo)).ToString();
                        }
                    }
                    else if (masterType == MasterTypes.IEC103)
                    {//IEC103
                        //this.UnitID = (Globals.getIEC103IEDNo(masterNo) + 1).ToString();
                        //Namrata: 25/11/2017
                        if (Utils.IEC103MasteriedList.Select(x => x.unitID).ToList().Contains(this.unitID))
                        {
                            this.UnitID = (Globals.getIEC103IEDNo(masterNo) + 1).ToString();

                        }
                        else
                        {
                            this.UnitID = (Globals.getIEC103IEDNo(masterNo)).ToString();
                        }
                    }
                    //Namrata:22/05/2018
                    else if (masterType == MasterTypes.IEC104)
                    {
                        if (Utils.IEC103MasteriedList.Select(x => x.unitID).ToList().Contains(this.unitID))
                        {
                            this.UnitID = (Globals.getIEC104IEDNo(masterNo) + 1).ToString();

                        }
                        else
                        {
                            this.UnitID = (Globals.getIEC104IEDNo(masterNo)).ToString();
                        }
                    }
                    else if (masterType == MasterTypes.MODBUS)//MODBUS
                    {
                        //Namrata: 25/11/2017
                        if (Utils.MODBUSMasteriedList.Select(x => x.unitID).ToList().Contains(this.unitID))
                        {
                            this.UnitID = (Globals.getMODBUSIEDNo(masterNo) + 1).ToString();
                        }
                        else
                        {
                            this.UnitID = (Globals.getMODBUSIEDNo(masterNo)).ToString();
                        }
                    }
                    else if (masterType == MasterTypes.IEC61850Client)//MODBUS
                    {
                        //Namrata: 25/11/2017
                        if (Utils.IEC61850MasteriedList.Select(x => x.unitID).ToList().Contains(this.unitID))
                        {
                            this.UnitID = (Globals.get61850IEDNo(masterNo) + 1).ToString();
                        }
                        else
                        {
                            this.UnitID = (Globals.get61850IEDNo(masterNo)).ToString();
                        }
                    }
                    //Ajay: 31/07/2018
                    else if (masterType == MasterTypes.LoadProfile)
                    {
                        if (Utils.LoadProfileMasteriedList.Select(x => x.unitID).ToList().Contains(this.unitID))
                        {
                            this.UnitID = (Globals.getLoadProfileIEDNo(masterNo) + 1).ToString();
                        }
                        else
                        {
                            this.UnitID = (Globals.getLoadProfileIEDNo(masterNo)).ToString();
                        }
                    }
                    //Namrata:29/01/2019
                    else if (masterType == MasterTypes.SPORT)
                    {
                        if (Utils.SPORTMasteriedList.Select(x => x.unitID).ToList().Contains(this.unitID))
                        {
                            this.UnitID = (Globals.getSPORTIEDNo(masterNo) + 1).ToString();

                        }
                        else
                        {
                            this.UnitID = (Globals.getSPORTIEDNo(masterNo)).ToString();
                        }
                    }
                }
                if (tn != null) tn.Text = "IED " + this.Description;
                LPFileNode = new LPFileConfiguration(mType, mNo, Int32.Parse(this.UnitID)); //Ajay: 10/09/2018
                RcbNode = new RCBConfiguration(mType, mNo, Int32.Parse(this.UnitID));
                aicNode = new AIConfiguration(mType, mNo, Int32.Parse(this.UnitID));
                aocNode = new AOConfiguration(mType, mNo, Int32.Parse(this.UnitID));
                dicNode = new DIConfiguration(mType, mNo, Int32.Parse(this.UnitID));
                docNode = new DOConfiguration(mType, mNo, Int32.Parse(this.UnitID));
                encNode = new ENConfiguration(mType, mNo, Int32.Parse(this.UnitID));
                smNode = new SlaveMapping(aicNode, aocNode, /*ref */dicNode, docNode, encNode);
                foreach (XmlNode node in iNode)
                {
                    //Namrata: 14/9/2017
                    //For IEC61850
                    if (node.Name == "ControlBlock")
                    {
                        TreeNode tmp = tn.Nodes.Add("RCB", "RCB", "RCB", "RCB");
                        //Namrata: 27/11/2019
                        //Task thCB = new Task(() => RcbNode.parseAICNode(node, imported));
                        //thCB.Name = "thCB";
                        //thCB.Priority = ThreadPriority.Highest;
                        //thCB.Start();
                        RcbNode.parseAICNode(node, imported);
                    }
                    //Ajay: 10/09/2018
                    else if (node.Name == "LPFILE")
                    {
                        TreeNode tmp = tn.Nodes.Add("LPFILE", "LPFILE", "LPFILE", "LPFILE");
                        //Namrata: 27/11/2019
                        //Task thLPFile = new Task(() => LPFileNode.parseLPFileCNode(node, imported));
                        //thLPFile.Name = "thLPFile";
                        //thLPFile.Priority = ThreadPriority.Highest;
                        //thLPFile.Start();
                        LPFileNode.parseLPFileCNode(node, imported);
                    }
                    else if (node.Name == "AIConfiguration")
                    {
                        Utils.MasterType = mType.ToString();
                        TreeNode tmp = tn.Nodes.Add("AI", "AI", "AI", "AI");
                        //Namrata: 27/11/2019
                        //Task thDetails = new Task(() => aicNode.parseAICNode(node, imported));
                        //thDetails.Start();
                        aicNode.parseAICNode(node, imported);
                    }
                    else if (node.Name == "AOConfiguration")
                    {
                        TreeNode tmp = tn.Nodes.Add("AO", "AO", "AO", "AO");
                        aocNode.parseAOCNode(node, imported);
                    }
                    else if (node.Name == "DIConfiguration")
                    {
                        TreeNode tmp = tn.Nodes.Add("DI", "DI", "DI", "DI");
                        //Task thDI = new Task(() => dicNode.parseDICNode(node, imported));
                        //thDI.Start();
                        dicNode.parseDICNode(node, imported);
                    }
                    else if (node.Name == "DOConfiguration")
                    {
                        TreeNode tmp = tn.Nodes.Add("DO", "DO", "DO", "DO");
                        //Namrata: 27/11/2019
                      //Task thDO = new Task(() => docNode.parseDOCNode(node, imported));
                        //thDO.Start();
                        docNode.parseDOCNode(node, imported);
                    }
                    else if (node.Name == "ENConfiguration")
                    {
                        TreeNode tmp = tn.Nodes.Add("EN", "EN", "EN", "EN");
                        //Namrata: 27/11/2019
                        //Task thEN = new Task(() => encNode.parseENCNode(node, imported));
                        //thEN.Start();
                        encNode.parseENCNode(node, imported);
                    }
                    else if (node.Name == "SlaveMapping")
                    {
                        //Namrata: 27/11/2019
                        //Task thDetails = new Task(() => smNode.parseSMNode(node));
                        //thDetails.Start();
                        smNode.parseSMNode(node);
                    }
                }
                //Task.WaitAll(taskArray);

                refreshList();
                ucied.lblIED.Text = "IED Details (Unit: " + unitID.ToString() + ")";
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Specially used by Virtual Master during loadXML, use to fill info...
        public void parseIEDNode(XmlNode iNode)
        {
            string strRoutineName = "parseIEDNode";
            try
            {
                if (aicNode == null) aicNode = new AIConfiguration(masterType, masterNo, Int32.Parse(this.UnitID));
                if (aocNode == null) aocNode = new AOConfiguration(masterType, masterNo, Int32.Parse(this.UnitID));
                if (dicNode == null) dicNode = new DIConfiguration(masterType, masterNo, Int32.Parse(this.UnitID));
                if (docNode == null) docNode = new DOConfiguration(masterType, masterNo, Int32.Parse(this.UnitID));
                if (encNode == null) encNode = new ENConfiguration(masterType, masterNo, Int32.Parse(this.UnitID));
                if (smNode == null) smNode = new SlaveMapping(aicNode, aocNode, dicNode, docNode, encNode);
                Utils.WriteLine(VerboseLevel.DEBUG, "iNode name: '{0}'", iNode.Name);
                if (iNode.Attributes != null)
                {
                    try
                    {
                        iType = (iedType)Enum.Parse(typeof(iedType), iNode.Name);
                    }
                    catch (System.ArgumentException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", iNode.Name);
                    }
                    foreach (XmlAttribute item in iNode.Attributes)
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
                else if (iNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = iNode.Value;
                }
                if (IEDTreeNode != null) IEDTreeNode.Text = "IED " + this.Description;
                foreach (XmlNode node in iNode)
                {
                    if (node.Name == "AIConfiguration")
                    {
                        aicNode.parseAICNode(node, false);
                    }
                    else if (node.Name == "AOConfiguration")
                    {
                        aocNode.parseAOCNode(node, false);
                    }
                    else if (node.Name == "DIConfiguration")
                    {Task thDI = new Task(() => dicNode.parseDICNode(node, false));
                        thDI.Start();
                        //dicNode.parseDICNode(node, false);
                    }
                    else if (node.Name == "DOConfiguration")
                    {
                        docNode.parseDOCNode(node, false);
                    }
                    else if (node.Name == "ENConfiguration")
                    {
                        encNode.parseENCNode(node, false);
                    }
                    else if (node.Name == "SlaveMapping")
                    {
                        smNode.parseSMNode(node);
                    }
                }
                refreshList();
                ucied.lblIED.Text = "IED Details (Unit: " + unitID.ToString() + ")";
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void updateAttributes(List<KeyValuePair<string, string>> iedData)
        {
            string strRoutineName = "updateAttributes";
            try
            {
                if (iedData != null && iedData.Count > 0)
                {
                    foreach (KeyValuePair<string, string> iedkp in iedData)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", iedkp.Key, iedkp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(iedkp.Key) != null) //Ajay: 03/07/2018
                            {
                                this.GetType().GetProperty(iedkp.Key).SetValue(this, iedkp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", iedkp.Key, iedkp.Value);
                        }
                    }
                    Utils.Write(VerboseLevel.DEBUG, "\n");
                }

                ucied.lblIED.Text = "IED Details (Unit: " + unitID.ToString() + ")";
                if (IEDTreeNode != null) IEDTreeNode.Text = "IED " + this.Description;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void changeCLASequence(int oCLANo, int nCLANo)
        {
            if (dicNode != null) dicNode.changeCLASequence(oCLANo, nCLANo);
        }
        public void changeProfileSequence(int oProfileNo, int nProfileNo)
        {
            if (dicNode != null) dicNode.changeProfileSequence(oProfileNo, nProfileNo);
        }
        public void changeMDSequence(int oMDNo, int nMDNo)
        {
            if (aicNode != null) aicNode.changeMDSequence(oMDNo, nMDNo);
            if (dicNode != null) dicNode.changeMDSequence(oMDNo, nMDNo);
        }
        public void changeDPSequence(int oDPNo, int nDPNo)
        {
            if (aicNode != null) aicNode.changeDPSequence(oDPNo, nDPNo);
        }
        public void changeDDSequence(int oDDNo, int nDDNo)
        {
            if (dicNode != null) dicNode.changeDDSequence(oDDNo, nDDNo);
        }
        public void regenerateAISequence()
        {
            if (aicNode != null) aicNode.regenerateAISequence();
        }
        public void regenerateAOSequence()
        {
            if (aocNode != null) aocNode.regenerateAOSequence();
        }
        public void regenerateDISequence()
        {
            if (dicNode != null) dicNode.regenerateDISequence();
        }
        public void regenerateDOSequence()
        {
            if (docNode != null) docNode.regenerateDOSequence();
        }
        public void regenerateENSequence()
        {
            if (encNode != null) encNode.regenerateENSequence();
        }
        private void addListHeaders()
        {
            string strRoutineName = "addListHeaders";
            try
            {
                ucied.lvIEDDetails.Columns.Add("No.", 50, HorizontalAlignment.Left);
                ucied.lvIEDDetails.Columns.Add("Type", 62, HorizontalAlignment.Left);
                ucied.lvIEDDetails.Columns.Add("Count", 60, HorizontalAlignment.Left);
                ucied.lvIEDDetails.Columns.Add("Map", 70, HorizontalAlignment.Left);
                ucied.lvIEDDetails.Columns.Add("MapCount", 70, HorizontalAlignment.Left);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void refreshList()
        {
            string strRoutineName = "refreshList";
            try
            {
                int rowCnt = 0;
                ucied.lvIEDDetails.Items.Clear();
                string[] row1 = { "1", "AI", aicNode.getCount().ToString(), "AI Map", aicNode.getAIMapCount().ToString() };
                ListViewItem lvItem1 = new ListViewItem(row1);
                if (rowCnt++ % 2 == 0) lvItem1.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                ucied.lvIEDDetails.Items.Add(lvItem1);
                //Namrata: 21/11/2017
                string[] row5 = { "2", "AO", aocNode.getCount().ToString(), "AO Map", aocNode.getENMapCount().ToString() };
                ListViewItem lvItem5 = new ListViewItem(row5);
                if (rowCnt++ % 2 == 0) lvItem5.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                ucied.lvIEDDetails.Items.Add(lvItem5);
                string[] row2 = { "3", "DI", dicNode.getCount().ToString(), "DI Map", dicNode.getDIMapCount().ToString() };
                ListViewItem lvItem2 = new ListViewItem(row2);
                if (rowCnt++ % 2 == 0) lvItem2.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                ucied.lvIEDDetails.Items.Add(lvItem2);
                string[] row3 = { "4", "DO", docNode.getCount().ToString(), "DO Map", docNode.getDOMapCount().ToString() };
                ListViewItem lvItem3 = new ListViewItem(row3);
                if (rowCnt++ % 2 == 0) lvItem3.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                ucied.lvIEDDetails.Items.Add(lvItem3);
                string[] row4 = { "5", "EN", encNode.getCount().ToString(), "EN Map", encNode.getENMapCount().ToString() };
                ListViewItem lvItem4 = new ListViewItem(row4);
                if (rowCnt++ % 2 == 0) lvItem4.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                ucied.lvIEDDetails.Items.Add(lvItem4);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("IED_")) { refreshList(); return ucied; }
            kpArr.RemoveAt(0);
            if (kpArr.ElementAt(0).Contains("RCB_"))
            {
                return RcbNode.getView(kpArr);
            }
            //Ajay: 10/09/2018
            if (kpArr.ElementAt(0).Contains("LPFILE_"))
            {
                return LPFileNode.getView(kpArr);
            }
            if (kpArr.ElementAt(0).Contains("AI_"))
            {
                return aicNode.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("AO_"))
            {
                return aocNode.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("DI_"))
            {
                return dicNode.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("DO_"))
            {
                return docNode.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("EN_"))
            {
                return encNode.getView(kpArr);
            }
            else
            {
                Utils.WriteLine(VerboseLevel.DEBUG, "***** IED: View for element: {0} not supported!!!", kpArr.ElementAt(0));
            }

            return null;
        }
        public TreeNode getTreeNode()
        {
            return IEDTreeNode;
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
            rootNode = xmlDoc.CreateElement(iType.ToString());
            xmlDoc.AppendChild(rootNode);
            foreach (string attr in arrAttributes)
            {
                XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                rootNode.Attributes.Append(attrName);
            }
            XmlNode importrcbICNode = rootNode.OwnerDocument.ImportNode(RcbNode.exportXMLnode(), true);
            rootNode.AppendChild(importrcbICNode);
            XmlNode importAICNode = rootNode.OwnerDocument.ImportNode(aicNode.exportXMLnode(), true);
            rootNode.AppendChild(importAICNode);
            XmlNode importAOCNode = rootNode.OwnerDocument.ImportNode(aocNode.exportXMLnode(), true);
            rootNode.AppendChild(importAOCNode);
            XmlNode importDICNode = rootNode.OwnerDocument.ImportNode(dicNode.exportXMLnode(), true);
            rootNode.AppendChild(importDICNode);
            XmlNode importDOCNode = rootNode.OwnerDocument.ImportNode(docNode.exportXMLnode(), true);
            rootNode.AppendChild(importDOCNode);
            XmlNode importENCNode = rootNode.OwnerDocument.ImportNode(encNode.exportXMLnode(), true);
            rootNode.AppendChild(importENCNode);
            XmlNode importSMNode = rootNode.OwnerDocument.ImportNode(smNode.exportXMLnode(), true);
            rootNode.AppendChild(importSMNode);
            return rootNode;
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
            rootNode = xmlDoc.CreateElement(iType.ToString());
            xmlDoc.AppendChild(rootNode);
            foreach (string attr in arrAttributes)
            {
                XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                rootNode.Attributes.Append(attrName);
            }
            //Ajay: 10/09/2018
            if (masterType == MasterTypes.LoadProfile)
            {
                XmlNode importLPFileCNode = rootNode.OwnerDocument.ImportNode(LPFileNode.exportXMLnode(), true);
                rootNode.AppendChild(importLPFileCNode);
            }

            XmlNode importAICNode = rootNode.OwnerDocument.ImportNode(aicNode.exportXMLnode(), true);
            rootNode.AppendChild(importAICNode);
            XmlNode importAOCNode = rootNode.OwnerDocument.ImportNode(aocNode.exportXMLnode(), true);
            rootNode.AppendChild(importAOCNode);
            XmlNode importDICNode = rootNode.OwnerDocument.ImportNode(dicNode.exportXMLnode(), true);
            rootNode.AppendChild(importDICNode);
            XmlNode importDOCNode = rootNode.OwnerDocument.ImportNode(docNode.exportXMLnode(), true);
            rootNode.AppendChild(importDOCNode);
            XmlNode importENCNode = rootNode.OwnerDocument.ImportNode(encNode.exportXMLnode(), true);
            rootNode.AppendChild(importENCNode);
            XmlNode importSMNode = rootNode.OwnerDocument.ImportNode(smNode.exportXMLnode(), true);
            rootNode.AppendChild(importSMNode);
            return rootNode;
        }
        public XmlNode exportXMLMODBUSnode()
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

            rootNode = xmlDoc.CreateElement(iType.ToString());
            xmlDoc.AppendChild(rootNode);

            if (arrAttributes != null) //Ajay: 31/08/2018
            {
                foreach (string attr in arrAttributes)
                {
                    XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                    attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                    rootNode.Attributes.Append(attrName);
                }
            }

            //XmlNode importrcbICNode = rootNode.OwnerDocument.ImportNode(RcbNode.exportXMLnode(), true);
            //rootNode.AppendChild(importrcbICNode);

            XmlNode importAICNode = rootNode.OwnerDocument.ImportNode(aicNode.exportXMLnode(), true);
            rootNode.AppendChild(importAICNode);

            XmlNode importAOCNode = rootNode.OwnerDocument.ImportNode(aocNode.exportXMLnode(), true);
            rootNode.AppendChild(importAOCNode);

            XmlNode importDICNode = rootNode.OwnerDocument.ImportNode(dicNode.exportXMLnode(), true);
            rootNode.AppendChild(importDICNode);

            XmlNode importDOCNode = rootNode.OwnerDocument.ImportNode(docNode.exportXMLnode(), true);
            rootNode.AppendChild(importDOCNode);

            XmlNode importENCNode = rootNode.OwnerDocument.ImportNode(encNode.exportXMLnode(), true);
            rootNode.AppendChild(importENCNode);

            XmlNode importSMNode = rootNode.OwnerDocument.ImportNode(smNode.exportXMLnode(), true);
            rootNode.AppendChild(importSMNode);
            return rootNode;
        }
        public XmlNode exportIEDnode()
        {
            XmlDocument xmlDoc = new XmlDocument();
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
            XmlNode rootNode = null;
            XmlNode rootIEDNode = null;
            rootNode = xmlDoc.CreateElement("IEDexport");
            xmlDoc.AppendChild(rootNode);
            string rattr = "MasterType";
            XmlAttribute rattrName = xmlDoc.CreateAttribute(rattr);
            rattrName.Value = masterType.ToString();
            rootNode.Attributes.Append(rattrName);
            rootIEDNode = xmlDoc.CreateElement(iType.ToString());
            rootNode.AppendChild(rootIEDNode);
            foreach (string attr in arrAttributes)
            {
                XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                rootIEDNode.Attributes.Append(attrName);
            }
            XmlNode importAICNode = rootIEDNode.OwnerDocument.ImportNode(aicNode.exportXMLnode(), true);
            rootIEDNode.AppendChild(importAICNode);
            XmlNode importAOCNode = rootIEDNode.OwnerDocument.ImportNode(aocNode.exportXMLnode(), true);
            rootIEDNode.AppendChild(importAOCNode);
            XmlNode importDICNode = rootIEDNode.OwnerDocument.ImportNode(dicNode.exportXMLnode(), true);
            rootIEDNode.AppendChild(importDICNode);
            XmlNode importDOCNode = rootIEDNode.OwnerDocument.ImportNode(docNode.exportXMLnode(), true);
            rootIEDNode.AppendChild(importDOCNode);
            XmlNode importENCNode = rootIEDNode.OwnerDocument.ImportNode(encNode.exportXMLnode(), true);
            rootIEDNode.AppendChild(importENCNode);
            return rootNode;
        }
        public string exportIED()
        {
            XmlNode xmlNode = exportIEDnode();
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
            xmlTextWriter.Formatting = Formatting.Indented;
            xmlTextWriter.Indentation = 2; //default is 1.
            xmlNode.WriteTo(xmlTextWriter);
            xmlTextWriter.Flush();
            return stringWriter.ToString();
        }
        public string exportINI(string slaveNum, string slaveID, string element, ref int ctr)
        {
            string iniData = "";

            if (element == "AI")
            {
                iniData = aicNode.exportINI(slaveNum, slaveID, element, ref ctr);
                Console.WriteLine("***** AI ctr = {0}", ctr);
            }
            //Ajay: 10/01/2019
            if (element == "AO")
            {
                iniData = aocNode.exportINI(slaveNum, slaveID, element, ref ctr);
                Console.WriteLine("***** AO ctr = {0}", ctr);
            }
            else if (element == "DI")
            {
                iniData = dicNode.exportINI(slaveNum, slaveID, element, ref ctr);
            }
            else if (element == "DO")
            {
                iniData = docNode.exportINI(slaveNum, slaveID, element, ref ctr);
            }
            else if (element == "EN")
            {
                iniData = encNode.exportINI(slaveNum, slaveID, element, ref ctr);
                Console.WriteLine("***** EN ctr = {0}", ctr);
            }
            else if (element == "DeadBandAI")
            {
                //Ajay: 10/01/2019
                iniData = aicNode.exportINI(slaveNum, slaveID, element, ref ctr);
                //iniData = aicNode.exportINI_DeadBandAI(slaveNum, slaveID, element, ref ctr);
            }
            else if (element == "DeadBandEN")
            {
                //Ajay: 10/01/2019
                //iniData = encNode.exportINI(slaveNum, slaveID, element, ref ctr);
                iniData = encNode.exportINI_DeadBandEN(slaveNum, slaveID, element, ref ctr);
            }

            return iniData;
        }
        //Namrata: 24/11/2017
        public AOConfiguration getAOConfiguration()
        {
            return aocNode;
        }
        public AIConfiguration getAIConfiguration()
        {
            return aicNode;
        }
        public DIConfiguration getDIConfiguration()
        {
            return dicNode;
        }
        public DOConfiguration getDOConfiguration()
        {
            return docNode;
        }
        public ENConfiguration getENConfiguration()
        {
            return encNode;
        }
        public string getIEDID
        {
            get { return "IED_" + masterType.ToString() + "_" + masterNo + "_" + UnitID; }
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public string UnitID
        {
            get
            {
                int i = 0;
                Int32.TryParse(ucied.txtUnitID.Text, out i);
                unitID = i;
                return unitID.ToString();
            }
            set
            {
                unitID = Int32.Parse(value); ucied.txtUnitID.Text = value;
                if (masterType == MasterTypes.IEC103)//IEC103
                    Globals.setIEC103IEDNo(masterNo, Int32.Parse(value));
                //Namrata:3/6/2017
                else if (masterType == MasterTypes.IEC101)//IEC101
                    Globals.setIEC101IEDNo(masterNo, Int32.Parse(value));

                else if (masterType == MasterTypes.MODBUS)//MODBUS
                    Globals.setMODBUSIEDNo(masterNo, Int32.Parse(value));

                else if (masterType == MasterTypes.IEC61850Client)//IEC61850Client
                    Globals.set61850IEDNo(masterNo, Int32.Parse(value));
                //Namrata:22/05/2018
                else if (masterType == MasterTypes.IEC104)
                    Globals.setIEC104IEDNo(masterNo, Int32.Parse(value));
                //Ajay: 29/08/2018
                else if (masterType == MasterTypes.LoadProfile)
                    Globals.setLoadProfileIEDNo(masterNo, Int32.Parse(value));
                //Namrata:29/01/2019
                else if (masterType == MasterTypes.SPORT)
                    Globals.setSPORTIEDNo(masterNo, Int32.Parse(value));

            }
        }
        public string ASDUAddr
        {
            get
            {
                try
                {
                    asduAddr = Int32.Parse(ucied.txtASDUaddress.Text);
                }
                catch (System.FormatException)
                {
                    asduAddr = -1;
                    ucied.txtASDUaddress.Text = asduAddr.ToString();
                }

                return asduAddr.ToString();
            }
            set
            {
                asduAddr = Int32.Parse(value); ucied.txtASDUaddress.Text = value;
                if (masterType == MasterTypes.IEC103)//IEC103
                    Globals.setIEC103ASDUAddress(masterNo, Int32.Parse(value));
                //Namrata:7/7/2017
                if (masterType == MasterTypes.IEC101)//IEC101
                    Globals.setIEC101ASDUAddress(masterNo, Int32.Parse(value));
                //Namarta:3/6/2017
                //else if (masterType == MasterTypes.ADR)//ADR
                //    Globals.setADRASDUAddress(masterNo, Int32.Parse(value));
            }
        }
        public string Device
        {
            get { return device = ucied.txtDevice.Text; }
            set { device = ucied.txtDevice.Text = value; }
        }
        public string RemoteIP
        {
            get { remoteIP = ucied.txtRemoteIP.Text; if (String.IsNullOrWhiteSpace(remoteIP)) remoteIP = "0.0.0.0"; return remoteIP; }
            set { remoteIP = ucied.txtRemoteIP.Text = value; }
        }
        //Ajay:06/07/2018
        public string RedundantIP
        {
            get { return redundantIP; }
            set { redundantIP = value; }
        }
        public string TCPPort
        {
            get
            {
                try
                {
                    tcpPort = Int32.Parse(ucied.txtTCPPort.Text);
                }
                catch (System.FormatException)
                {
                    tcpPort = 0;
                }
                return tcpPort.ToString();
            }
            set
            {
                try
                {
                    tcpPort = Int32.Parse(value);
                }
                catch (System.FormatException)
                {
                    tcpPort = 0;
                }
                ucied.txtTCPPort.Text = tcpPort.ToString();
            }
        }
        public string Retries
        {
            get
            {
                try
                {
                    retries = Int32.Parse(ucied.txtRetries.Text);
                }
                catch (System.FormatException)
                {
                }
                return retries.ToString();
            }
            set { retries = Int32.Parse(value); ucied.txtRetries.Text = value; }
        }
        public string TimeOutMS
        {
            get
            {
                try
                {
                    timeoutms = Int32.Parse(ucied.txtTimeOut.Text);
                }
                catch (System.FormatException)
                {
                }
                return timeoutms.ToString();
            }
            set { timeoutms = Int32.Parse(value); ucied.txtTimeOut.Text = value; }
        }
        public string Description
        {
            get { return descr = ucied.txtDescription.Text; }
            set { descr = ucied.txtDescription.Text = value; }
        }
        public string DR
        {
            get { dr = ucied.chkDR.Checked; return (dr == true ? "ENABLE" : "DISABLE"); }
            set
            {
                dr = (value.ToLower() == "enable") ? true : false;
                if (dr == true) ucied.chkDR.Checked = true;
                else ucied.chkDR.Checked = false;
            }
        }
        public string LinkAddressSize
        {
            get { return linkaddresssize; }
            set { linkaddresssize = value; }
        }
        public string ASDUSize
        {
            get { return asSize; }
            set { asSize = value; }
        }
        public string IOASize
        {
            get
            {
                return ioaSz;
            }
            set
            {
                ioaSz = value;
            }
        }
        public string COTSize
        {
            get { return cSize; }
            set { cSize = value; }
        }
        public string SCLName
        {
            get
            {
                return icdFilePath;
            }
            set
            {
                icdFilePath = value;
            }
        }
        public string T0
        {
            get { return t0; }
            set { t0 = value; }
        }
        public string T1
        {
            get { return t1; }
            set { t1 = value; }
        }
        public string T2
        {
            get { return t2; }
            set { t2 = value; }
        }
        public string T3
        {
            get { return t3; }
            set { t3 = value; }
        }
        public string W
        {
            get { return w; }
            set { w = value; }
        }
        public string K
        {
            get { return k; }
            set { k = value; }
        }
        //Ajay: 31/07/2018
        public string DefaultTapRatio
        {
            get { return tapratio; }
            set { tapratio = value; }
        }
        //Ajay: 31/07/2018
        public string TXOffsetCurrent
        {
            get { return txoffsetcurrent; }
            set { txoffsetcurrent = value; }
        }
        //Ajay: 17/01/2019
        public string TimestampType
        {
            get { return timestmpType; }
            set { timestmpType = value; }
        }
        //Namrata:31/01/2019
        public string SportType
        {
            get
            {
                return sportType;
            }
            set
            {
                sportType = value;
            }
        }
        public string LastAI
        {
            get { return lastAI; }
            set { lastAI = value; }
        }
        public string LastDI
        {
            get { return lastDI; }
            set { lastDI = value; }
        }
        public string LastDO
        {
            get { return lastDO; }
            set { lastDO = value; }
        }
        public string TimestampAccuracy
        {
            get { return timestampaccuracy; }
            set { timestampaccuracy = value; }
        }
        public string WindowTime
        {
            get { return windowtime; }
            set { windowtime = value; }
        }
        public string DebounceTime
        {
            get { return debouncetime; }
            set { debouncetime = value; }
        }
        public string PulseWidthTimeout
        {
            get { return pulsewidthtimeout; }
            set { pulsewidthtimeout = value; }
        }
    }
}
    

