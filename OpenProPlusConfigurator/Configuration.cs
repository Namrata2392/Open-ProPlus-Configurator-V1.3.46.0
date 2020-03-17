using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.Collections.ObjectModel;
using System.Data;

using System.Linq;
namespace OpenProPlusConfigurator
{
    /// <summary>
    /// Configuration class used for parsing and saving SCL document.
    /// </summary>
    public class Configuration : XmlDocument, IConfigure
    {
        #region Fields
        /// <summary>
        /// The global identifier for mapped items.
        /// </summary>
        private int gid = 0;
        /// <summary>
        /// The internal grid view identifier.
        /// </summary>
        private int iid = 0;
        /// <summary>
        /// Controls whether wrong items shall be skipped.
        /// </summary>
        private bool skipWrongItems = false;
        #endregion

        #region IConfigure interface
        /// <summary>
        /// Indicates the SCL file has entries which cannot be parsed.
        /// </summary>
        public bool CorruptedScl { private set; get; }
        /// <summary>
        /// Indicates a wrong entry is found.
        /// </summary>
        public bool WrongEntry { internal set; get; }
        /// <summary>
        /// The mapping index number assigned to single data item entry.
        /// </summary>
        public bool MappingChanged { private set; get; }
        /// <summary>
        /// Collected types instance.
        /// </summary>
        private Dictionary<string, string> types = new Dictionary<string, string>();
        /// <summary>
        /// The collected types dictionary.
        /// </summary>
        public IDictionary<string, string> ITypes
        {
            get
            {
                return this.types;
            }
        }
        /// <summary>
        /// IED dictionary instance.
        /// </summary>
        private Dictionary<string, XPathNodeIterator> ieds = new Dictionary<string, XPathNodeIterator>();
        /// <summary>
        /// IED dictionary.
        /// </summary>
        public IDictionary<string, XPathNodeIterator> IIeds
        {
            get
            {
                return this.ieds;
            }
        }
        /// <summary>
        /// Additional tags dictionary instance.
        /// </summary>
        private Dictionary<string, PrivateTag> additionalTags = new Dictionary<string, PrivateTag>();
        /// <summary>
        /// Additional tags dictionary.
        /// </summary>
        public IDictionary<string, PrivateTag> IAdditionalTags
        {
            get
            {
                return this.additionalTags;
            }
        }
        /// <summary>
        /// Grid items collection instance.
        /// </summary>
        private List<GridItem> gridItems = new List<GridItem>();
        /// <summary>
        /// Grid items collection.
        /// </summary>
        public ICollection<GridItem> IGridItems
        {
            get
            {
                return this.gridItems as ICollection<GridItem>;
            }
        }
        /// <summary>
        /// DA types dictionary instance.
        /// </summary>
        private Dictionary<string, Dictionary<string, Bda>> daTypes = new Dictionary<string, Dictionary<string, Bda>>();
        /// <summary>
        /// DA types dictionary.
        /// </summary>
        public IDictionary<string, Dictionary<string, Bda>> IDATypes
        {
            get
            {
                return this.daTypes;
            }
        }
        /// <summary>
        /// Logical node types dictionary instance.
        /// </summary>
        private Dictionary<string, LNType> lNodeType = new Dictionary<string, LNType>();
        /// <summary>
        /// Logical node types dictionary.
        /// </summary>
        public IDictionary<string, LNType> ILNodeTypes
        {
            get
            {
                return this.lNodeType;
            }
        }
        /// <summary>
        /// DO type dictionary instance.
        /// </summary>
        private Dictionary<string, Dictionary<string, DataItem>> doType = new Dictionary<string, Dictionary<string, DataItem>>();
        /// <summary>
        /// DO type dictionary.
        /// </summary>
        public IDictionary<string, Dictionary<string, DataItem>> IDOTypes
        {
            get
            {
                return this.doType;
            }
        }
        /// <summary>
        /// Logical node dictionary instance.
        /// </summary>
        private Dictionary<string, LN> logicalNodes = new Dictionary<string, LN>();
        /// <summary>
        /// Logical node dictionary.
        /// </summary>
        public IDictionary<string, LN> ILogicalNodes
        {
            get
            {
                return this.logicalNodes;
            }
        }
        /// <summary>
        /// Predefined data sets dictionary instance.
        /// </summary>
        private Dictionary<string, List<Fcda>> dataSets = new Dictionary<string, List<Fcda>>();
        /// <summary>
        /// Predefined data sets dictionary.
        /// </summary>
        public IDictionary<string, List<Fcda>> IDataSets
        {
            get
            {
                return this.dataSets;
            }
        }
        /// <summary>
        /// Dynamic data sets dictionary.
        /// </summary>
        private Dictionary<string, List<Fcda>> dDataSets = new Dictionary<string, List<Fcda>>();
        /// <summary>
        /// Dynamic data sets dictionary.
        /// </summary>
        public IDictionary<string, List<Fcda>> ICustomDataSets
        {
            get
            {
                return this.dDataSets;
            }
        }
        /// <summary>
        /// Used dynamic data sets dictionary instance.
        /// </summary>
        private Dictionary<string, string> dDataSetsInUse = new Dictionary<string, string>();
        /// <summary>
        /// Used dynamic data sets dictionary.
        /// </summary>
        public IDictionary<string, string> ICustomDataSetsInUse
        {
            get
            {
                return this.dDataSetsInUse;
            }

        }
        /// <summary>
        /// Temporary data sets dictionary instance.
        /// </summary>
        private Dictionary<string, List<Fcda>> tempDataSets = new Dictionary<string, List<Fcda>>();
        /// <summary>
        /// Temporary data sets dictionary.
        /// </summary>
        public IDictionary<string, List<Fcda>> ITempDataSets
        {
            get
            {
                return this.tempDataSets;
            }
        }
        /// <summary>
        /// Reports dictionary instance.
        /// </summary>
        private Dictionary<string, Report> reports = new Dictionary<string, Report>();
        /// <summary>
        /// Reports dictionary.
        /// </summary>
        public IDictionary<string, Report> IReports
        {
            get
            {
                return this.reports;
            }
        }
        /// <summary>
        /// Reports collection instance.
        /// </summary>
        private List<Report> localRcb = new List<Report>();
        /// <summary>
        /// Reports collection.
        /// </summary>
        public ICollection<Report> ILocalRcbs
        {
            get
            {
                return this.localRcb as ICollection<Report>;
            }
        }
        /// <summary>
        /// Used reports collection instance.
        /// </summary>
        private List<Report> usedRcb = new List<Report>();
        /// <summary>
        /// Used reports collection.
        /// </summary>
        public ICollection<Report> IUsedRcbs
        {
            get
            {
                return this.usedRcb as ICollection<Report>;
            }
        }
        /// <summary>
        /// Dictionary of mapped IEDs collection instance.
        /// </summary>
        private Dictionary<string, List<PrivateTag>> iedMappings = new Dictionary<string, List<PrivateTag>>();
        /// <summary>
        /// Dictionary of mapped IEDs.
        /// </summary>
        public IDictionary<string, List<PrivateTag>> IIedMappings
        {
            get
            {
                return this.iedMappings;
            }
        }
        /// <summary>
        /// Ignored objects collection instance.
        /// </summary>
        private HashSet<string> ignoredObjects = new HashSet<string>
        {
            { "Test" },
            { "Check" },
            { "T" },
            { "q" },
            { "t" },
            { "ctlNum" },
            { "orCat" },
            { "orIdent" }
        };
        /// <summary>
        /// Ignored objects collection.
        /// </summary>
        public ICollection<string> IIgnoredObjects
        {
            get
            {
                return this.ignoredObjects;
            }
        }
        /// <summary>
        /// Supported types dictionary instance.
        /// </summary>
        private HashSet<string> supportedTypes = new HashSet<string> {
            { "BOOLEAN" },
            { "INT8" },
            { "INT16" },
            { "INT24" },
            { "INT32" },
            { "INT128" },
            { "INT8U" },
            { "INT16U" },
            { "INT24U" },
            { "INT32U" },
            { "FLOAT32" },
            { "FLOAT64" },
            { "Enum" },
            { "Dbpos" }
        };
        /// <summary>
        /// Supported types dictionary.
        /// </summary>
        public ICollection<string> ISupportedTypes
        {
            get
            {
                return this.supportedTypes;
            }
        }
        /// <summary>
        /// Supported functional constraints collection instance.
        /// </summary>
        private HashSet<string> supportedFCs = new HashSet<string> {
            { "ST" },
            { "MX" },
            { "CO" },
            { "CF" }
        };
        /// <summary>
        /// Supported functional constraints collection.
        /// </summary>
        public ICollection<string> ISupportedFCs
        {
            get
            {
                return this.supportedFCs;
            }
        }
        /// <summary>
        /// Column headers dictionary instance.
        /// </summary>
        private Dictionary<string, int> columnHeaders = new Dictionary<string, int> {
            { "Id", 0 },
            { "Mapping Type", 1 },
            { "Index", 2 },
            { "Refresh Type", 3 },
            { "Type", 4 },
            { "Object Reference", 5 },
            { "FC", 6 },
            { "Description", 7 }
        };
        /// <summary>
        /// Column headers dictionary.
        /// </summary>
        public IDictionary<string, int> IColumnHeaders
        {
            get
            {
                return this.columnHeaders;
            }
        }
        /// <summary>
        /// Supported functional constrains dictionary instance.
        /// </summary>
        private Dictionary<string, string> usedFCs = new Dictionary<string, string>();
        /// <summary>
        /// Supported functional constrains dictionary.
        /// </summary>
        public IDictionary<string, string> IUsedFCs
        {
            get
            {
                return this.usedFCs;
            }
        }
        /// <summary>
        /// Name of the edited file.
        /// </summary>
        public string IOpenedFileName { set; get; }
        /// <summary>
        /// Opened file name full path.
        /// </summary>
        public string IFullFileNamePath { set; get; }
        #endregion

        #region IEC-61850 edition evaluation
        /// <summary>
        /// Defines IEC61850 standard editions.
        /// </summary>
        public enum IEC61850Edition
        {
            /// <summary>
            /// Edition is not specified.
            /// </summary>
            Unknown,
            /// <summary>
            /// Edition 1 (2003).
            /// </summary>
            Edition1,
            /// <summary>
            /// Edition 2 (2007).
            /// </summary>
            Edition2
        };

        /// <summary>
        /// Evaluates the edition version for the current SCL document. It is assumed the required schema version is 2007 and revision is B.
        /// </summary>
        /// <param name="sclRootNode">The SCL root node.</param>
        public IEC61850Edition EvaluateEdition(XPathNavigator sclRootNode)
        {
            XmlNamespaceManager manager = new XmlNamespaceManager(sclRootNode.NameTable);
            manager.AddNamespace("scl", "http://www.iec.ch/61850/2003/SCL");

            XPathNavigator scl = sclRootNode.SelectSingleNode("/scl:SCL", manager);
            string sclVersion = scl.GetAttribute("version", "");
            string sclRevision = scl.GetAttribute("revision", "");
            string version = sclVersion + sclRevision;
            if (string.Compare(version, "2007B") >= 0)
            {
                Utils.Edition = "Ed2"; ;
                return IEC61850Edition.Edition2;

            }
            else
            {
                Utils.Edition = "Ed1"; ;
                return IEC61850Edition.Edition1;
            }
        }
        #endregion

        #region Constructors and initializers
        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            this.skipWrongItems = false;
            this.CorruptedScl = false;
            this.WrongEntry = false;
            this.MappingChanged = false;
        }
        #endregion



        /// <summary>
        /// Adds item to the grid.
        /// </summary>
        /// <param name="iid">The data item identifier.</param>
        /// <param name="isChecked">true if item is mapped</param>
        /// <param name="type">The data item type.</param>
        /// <param name="reference">The full reference to specified item.</param>
        /// <param name="fc">The functional constraint.</param>
        /// <param name="desc">The user description of item.</param>
        /// <param name="ln">Item's logical node owner.</param>
        /// <param name="iedName">Item's IED owner.</param>
        /// <param name="model">The control model if any.</param>
        DataRow DrOnReq;
        private void addToGrid(int iid, bool isChecked, string type, string reference, string fc, string desc, string ln, string iedName, string model)
        {
            try
            {
                this.IGridItems.Add(new GridItem(this.gid, iid, isChecked, type, reference, fc, desc, ln, iedName, model));
                DrOnReq = Utils.OnReqData.NewRow();
                DrOnReq["Onrequest"] = reference;
                DrOnReq["Node"] = "On Request";
                DrOnReq["FC"] = fc.ToString();
                Utils.OnReqData.Rows.Add(DrOnReq);
                int a = Utils.OnReqData.Rows.Count;
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("Duplicated grid item: " + reference);
            }
            this.gid++;
        }

        /// <summary>
        /// Selects mapping items for the grid view.Utils.OnReqData
        /// </summary>
        private void chooseMappingItems()
        {
            foreach (var ln in this.ILogicalNodes)
            {
                //Namrata:20/04/2019
                Utils.OnReqData.Clear();
                Utils.OnReqData.Rows.Clear();
                Utils.OnReqData.Columns.Clear();
                Utils.OnReqData.TableName = null;
                DataColumn dccolumn1;
                if (!Utils.OnReqData.Columns.Contains("OnRequest"))//Namrata: 15/03/2018
                { dccolumn1 = Utils.OnReqData.Columns.Add("OnRequest", typeof(string)); }
                if (!Utils.OnReqData.Columns.Contains("Node"))//Namrata: 15/03/2018
                { Utils.OnReqData.Columns.Add("Node", typeof(string)); }
                if (!Utils.OnReqData.Columns.Contains("FC"))//Namrata: 15/03/2018
                { Utils.OnReqData.Columns.Add("FC", typeof(string)); }
                string iedName = ln.Value.IedName;
                LNType lnt;
                if (!this.ILNodeTypes.ContainsKey(ln.Value.LNType))
                {
                    continue;
                }
                lnt = this.ILNodeTypes[ln.Value.LNType];
                foreach (var doLNodeType in lnt.DataObjects)
                {
                    string doType = doLNodeType.Value;
                    Dictionary<string, DataItem> daDict;
                    if (!this.IDOTypes.ContainsKey(doType))
                    {
                        continue;
                    }
                    daDict = this.IDOTypes[doType];
                    foreach (var da in daDict)
                    {
                        if (da.Value.BType == "Quality" || da.Value.BType == "Timestamp")
                        {
                            continue;
                        }
                        if (da.Value.Sdo)
                        {
                            string sdoName = da.Value.Name;
                            string sdoType = da.Value.Type;
                            string model = "";
                            foreach (var t in daDict)
                            {
                                if (t.Key.Contains("ctlModel"))
                                {
                                    model = t.Value.CtlModel;
                                    break;
                                }
                            }
                            this.chooseMappingItems(true, sdoType, ref sdoName, iedName +
                                                    ln.Value.LDName + "/" +
                                                    ln.Value.LNPrefix +
                                                    ln.Value.LNClass +
                                                    ln.Value.LNInstance + "." +
                                                    doLNodeType.Key + ".", da.Value.FC, ln.Value.LNType, ln.Value.IedName, da.Value.Count, model);
                        }
                        else
                        {
                            DataItem tmp = da.Value;
                            string model = "";
                            foreach (var t in daDict)
                            {
                                if (t.Key.Contains("ctlModel"))
                                {
                                    model = t.Value.CtlModel;
                                    break;
                                }
                            }
                            if (tmp.BType == "Struct")
                            {
                                string structType = tmp.Type;
                                string structName = tmp.Name;
                                this.chooseMappingItems(false, structType, ref structName, iedName +
                                                        ln.Value.LDName + "/" +
                                                        ln.Value.LNPrefix +
                                                        ln.Value.LNClass +
                                                        ln.Value.LNInstance + "." +
                                                        doLNodeType.Key + ".", da.Value.FC, ln.Value.LNType, ln.Value.IedName, da.Value.Count,
                                                        model);
                            }
                            else
                            {
                                if (this.ISupportedTypes.Contains(da.Value.BType) && this.ISupportedFCs.Contains(da.Value.FC))
                                {
                                    if (string.IsNullOrEmpty(da.Value.Count))
                                    {
                                        if (model != "")
                                        {
                                            this.addToGrid(da.Value.ID, false, da.Value.BType, iedName +
                                                           ln.Value.LDName + "/" + ln.Value.LNPrefix + ln.Value.LNClass +
                                                           ln.Value.LNInstance + "." + doLNodeType.Key + "." +
                                                           da.Value.Name, da.Value.FC, "", ln.Key, ln.Value.IedName, model);
                                        }
                                        else
                                        {
                                            this.addToGrid(da.Value.ID, false, da.Value.BType, iedName + ln.Value.LDName + "/" + ln.Value.LNPrefix + ln.Value.LNClass +
                                                           ln.Value.LNInstance + "." + doLNodeType.Key + "." +
                                                           da.Value.Name, da.Value.FC, "", ln.Key, ln.Value.IedName, da.Value.CtlModel);
                                        }
                                    }
                                    else
                                    {
                                        Int32 i = Convert.ToInt32(da.Value.Count, CultureInfo.CurrentCulture);
                                        if (i == 0)
                                        {
                                            if (model != "")
                                            {
                                                this.addToGrid(da.Value.ID, false, da.Value.BType, iedName +
                                                               ln.Value.LDName + "/" + ln.Value.LNPrefix + ln.Value.LNClass +
                                                               ln.Value.LNInstance + "." + doLNodeType.Key + "." +
                                                               da.Value.Name, da.Value.FC, "", ln.Key, ln.Value.IedName, model);
                                            }
                                            else
                                            {
                                                this.addToGrid(da.Value.ID, false, da.Value.BType, iedName + ln.Value.LDName + "/" + ln.Value.LNPrefix + ln.Value.LNClass +
                                                               ln.Value.LNInstance + "." + doLNodeType.Key + "." +
                                                               da.Value.Name, da.Value.FC, "", ln.Key, ln.Value.IedName, da.Value.CtlModel);
                                            }
                                        }
                                        else
                                        {
                                            for (int j = 0; j < i; j++)
                                            {
                                                if (model != "")
                                                {
                                                    this.addToGrid(da.Value.ID, false, da.Value.BType, iedName +
                                                                   ln.Value.LDName + "/" + ln.Value.LNPrefix + ln.Value.LNClass +
                                                                   ln.Value.LNInstance + "." + doLNodeType.Key + "." + da.Value.Name + "[" +
                                                                   j.ToString(CultureInfo.InvariantCulture) + "]", da.Value.FC, "", ln.Key, ln.Value.IedName, model);
                                                }
                                                else
                                                {
                                                    this.addToGrid(da.Value.ID, false, da.Value.BType, iedName +
                                                                   ln.Value.LDName + "/" + ln.Value.LNPrefix + ln.Value.LNClass +
                                                                   ln.Value.LNInstance + "." + doLNodeType.Key + "." + da.Value.Name + "[" +
                                                                   j.ToString(CultureInfo.InvariantCulture) + "]", da.Value.FC, "", ln.Key, ln.Value.IedName,
                                                                   da.Value.CtlModel);
                                                }
                                            }
                                        }
                                    }
                                    if (!this.ITypes.ContainsKey(da.Value.BType))
                                    {
                                        this.ITypes.Add(da.Value.BType, da.Value.BType);
                                    }
                                    if (!this.IUsedFCs.ContainsKey(da.Value.FC))
                                    {
                                        this.IUsedFCs.Add(da.Value.FC, "");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Explores type and selects items for the grid view.
        /// </summary>
        /// <param name="isSdo">Is the SDO context indication.</param>
        /// <param name="type">The node type.</param>
        /// <param name="name">The node name.</param>
        /// <param name="prefix">The item prefix.</param>
        /// <param name="fc">The current functional constraint.</param>
        /// <param name="lnName">The current logical node name.</param>
        /// <param name="IedName">The current IED name.</param>
        /// <param name="count">The items count in case of array.</param>
        /// <param name="model">The control model value.</param>
        private void chooseMappingItems(bool isSdo, string type, ref string name, string prefix, string fc, string lnName, string IedName, string count, string model)
        {
            if (isSdo)
            {
                if (this.IDOTypes.ContainsKey(type))
                {
                    Dictionary<string, DataItem> daDictTmp = this.IDOTypes[type];
                    foreach (var v in daDictTmp)
                    {
                        if (this.ISupportedTypes.Contains(v.Value.BType))
                        {
                            continue;
                        }
                        if (v.Value.Sdo)
                        {
                            string sdoName = name + "." + v.Value.Name;
                            type = v.Value.Type;
                            model = v.Value.CtlModel;
                            this.chooseMappingItems(true, type, ref sdoName, prefix, v.Value.FC, lnName, IedName, count, model);
                        }
                        else
                        {
                            DataItem tmp = v.Value;
                            if (tmp.BType == "Struct")
                            {
                                string structName = name + "." + tmp.Name;
                                type = tmp.Type;
                                model = tmp.CtlModel;
                                this.chooseMappingItems(false, type, ref structName, prefix, tmp.FC, lnName, IedName, count, model);
                            }
                            else
                            {
                                if (this.ISupportedTypes.Contains(tmp.BType) && this.ISupportedFCs.Contains(v.Value.FC) && !this.IIgnoredObjects.Contains(tmp.Name))
                                {
                                    if (string.IsNullOrEmpty(count))
                                    {
                                        model = v.Value.CtlModel;
                                        this.addToGrid(v.Value.ID, false, tmp.BType, prefix + name + "." + tmp.Name, v.Value.FC, "", lnName, IedName, model);
                                    }
                                    else
                                    {
                                        Int32 i = Convert.ToInt32(count, CultureInfo.CurrentCulture);
                                        for (int j = 0; j < i; j++)
                                        {
                                            string[] split = name.Split('.');
                                            string tmp2 = split[0] + ".[" + j.ToString(CultureInfo.InvariantCulture) + "]";
                                            for (int z = 1; z < split.Length; z++)
                                            {
                                                tmp2 += ".";
                                                tmp2 += split[z];
                                            }
                                            model = v.Value.CtlModel;
                                            this.addToGrid(v.Value.ID, false, tmp.BType, prefix + tmp2 + "." + tmp.Name, v.Value.FC, "", lnName, IedName, model);
                                        }
                                    }
                                    if (!this.ITypes.ContainsKey(tmp.BType))
                                    {
                                        this.ITypes.Add(tmp.BType, tmp.BType);
                                    }
                                    if (!this.IUsedFCs.ContainsKey(tmp.FC))
                                    {
                                        this.IUsedFCs.Add(tmp.FC, "");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (this.IDATypes.ContainsKey(type))
                {
                    var tmp = this.IDATypes[type];
                    foreach (var v in tmp.Values)
                    {
                        if (v.BType == "Struct")
                        {
                            string structName = name + "." + v.Name;
                            type = v.Type;
                            this.chooseMappingItems(false, type, ref structName, prefix, fc, lnName, IedName, count, model);
                        }
                        else
                        {
                            if (this.ISupportedTypes.Contains(v.BType) && this.ISupportedFCs.Contains(fc) && !this.IIgnoredObjects.Contains(v.Name))
                            {
                                if (string.IsNullOrEmpty(count))
                                {
                                    this.addToGrid(v.ID, false, v.BType, prefix + name + "." + v.Name, fc, "", lnName, IedName, model);
                                }
                                else
                                {
                                    Int32 i = Convert.ToInt32(count, CultureInfo.CurrentCulture);
                                    for (int j = 0; j < i; j++)
                                    {
                                        string[] split = name.Split('.');
                                        string tmp2 = split[0] + ".[" + j.ToString(CultureInfo.InvariantCulture) + "]";
                                        for (int z = 1; z < split.Length; z++)
                                        {
                                            tmp2 += ".";
                                            tmp2 += split[z];
                                        }
                                        this.addToGrid(v.ID, false, v.BType, prefix + tmp2 + "." + v.Name, fc, "", lnName, IedName, model);
                                    }
                                }
                                if (!this.ITypes.ContainsKey(v.BType))
                                {
                                    this.ITypes.Add(v.BType, v.BType);
                                }
                                if (!this.IUsedFCs.ContainsKey(fc))
                                {
                                    this.IUsedFCs.Add(fc, "");
                                }
                            }
                        }
                    }
                }
            }
            return;
        }

        /// <summary>
        /// Parses the SCL document.
        /// </summary>
        /// 
        string Manufacture = "";
        IEC61850ServerSlaveGroup iec = new IEC61850ServerSlaveGroup();
        ucGroup61850ServerSlave ucmbs = new ucGroup61850ServerSlave();
        public void ParseScl()
        {
            string limitsIedName = "";
            int cSize = 0;
            int iSize = 0;
            int aSize = 0;
            int pSize = 0;

            this.parseDATypes();
            this.parseLNodeTypes();
            this.parseDOTypes();
            ParserRemoteIP();
            ParserIEDName();
            XPathNavigator navigator = LastChild.CreateNavigator();
            XPathNodeIterator iedIterator = navigator.SelectDescendants("IED", navigator.NamespaceURI, true);
            XPathNodeIterator privateLimitsIterator = navigator.SelectDescendants("Limits", "http://www.ashidaelectronics.com/", false);

            string ieDName = "";
            while (iedIterator.MoveNext())
            {
                if (iedIterator.Current != null)
                {
                    XPathNodeIterator tmp = iedIterator.Clone();
                    XPathNodeIterator tmp2 = iedIterator.Clone();
                    if (tmp.Current.Name == "IED")
                    {
                        tmp.Current.MoveToFirstAttribute();
                        do
                        {

                            if (tmp.Current.Name == "name")
                            {
                                if (tmp.Current.Value == "")
                                {
                                    MessageBox.Show("IED node name value is empty.\nPlease check the SCL file",
                                                    "SCL parsing error",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                                    this.CorruptedScl = true;
                                    break;
                                }
                                ieDName = tmp.Current.Value;
                                try
                                {
                                    this.IIeds.Add(ieDName, tmp2);
                                    this.IIedMappings.Add(ieDName + ".Analog", new List<PrivateTag>());
                                    this.IIedMappings.Add(ieDName + ".Parameter", new List<PrivateTag>());
                                    this.IIedMappings.Add(ieDName + ".IedDin", new List<PrivateTag>());
                                    this.IIedMappings.Add(ieDName + ".Control", new List<PrivateTag>());
                                }
                                catch (ArgumentException)
                                {
                                    throw new ArgumentException("Duplicated IED: " + ieDName);
                                }
                                //Namrata: 24/10/2017

                                //tmp.Current.MoveToParent();
                            }
                            //Namrata: 24/10/2017
                            if (tmp.Current.Name == "manufacturer")
                            {
                                Manufacture = tmp.Current.Value;
                                Utils.Manufacture = Manufacture;
                            }
                        } while (tmp.Current.MoveToNextAttribute());
                    }
                }
            }
            while (privateLimitsIterator.MoveNext())
            {
                XPathNodeIterator temp = privateLimitsIterator.Clone();
                temp.Current.MoveToFirstAttribute();
                cSize = 0;
                iSize = 0;
                aSize = 0;
                pSize = 0;
                do
                {
                    if (temp.Current.Name == "iedName")
                    {
                        limitsIedName = temp.Current.Value;
                        Utils.IEDName = ieDName;//Namrata:20/04/2019
                    }
                    if (temp.Current.Name == "controls")
                    {
                        cSize = Convert.ToInt32(temp.Current.Value, CultureInfo.InvariantCulture);
                    }
                    if (temp.Current.Name == "analogs")
                    {
                        aSize = Convert.ToInt32(temp.Current.Value, CultureInfo.InvariantCulture);
                    }
                    if (temp.Current.Name == "iedDins")
                    {
                        iSize = Convert.ToInt32(temp.Current.Value, CultureInfo.InvariantCulture);
                    }
                    if (temp.Current.Name == "parameters")
                    {
                        pSize = Convert.ToInt32(temp.Current.Value, CultureInfo.InvariantCulture);
                    }

                } while (temp.Current.MoveToNextAttribute());

                List<PrivateTag> analogTags = new List<PrivateTag>(new PrivateTag[aSize]);
                List<PrivateTag> parameterTags = new List<PrivateTag>(new PrivateTag[pSize]);
                List<PrivateTag> ieddinTags = new List<PrivateTag>(new PrivateTag[iSize]);
                List<PrivateTag> controlTags = new List<PrivateTag>(new PrivateTag[cSize]);

                this.IIedMappings[limitsIedName + ".Analog"] = analogTags;
                this.IIedMappings[limitsIedName + ".Parameter"] = parameterTags;
                this.IIedMappings[limitsIedName + ".IedDin"] = ieddinTags;
                this.IIedMappings[limitsIedName + ".Control"] = controlTags;
            }

            iedIterator = navigator.SelectDescendants("IED", navigator.NamespaceURI, true);
            while (iedIterator.MoveNext())
            {
                if (iedIterator.Current != null)
                {
                    XPathNodeIterator tmp = iedIterator.Clone();
                    if (tmp.Current.Name == "IED")
                    {
                        tmp.Current.MoveToFirstAttribute();
                        do
                        {
                            if (tmp.Current.Name == "name")
                            {
                                ieDName = tmp.Current.Value;
                                tmp.Current.MoveToParent();
                            }
                            //Namrata: 24/10/2017
                            if (tmp.Current.Name == "manufacturer")
                            {
                                Manufacture = tmp.Current.Value;
                                Utils.Manufacture = Manufacture; //Namrata: 02/11/2017
                                tmp.Current.MoveToParent();
                            }
                        } while (tmp.Current.MoveToNextAttribute());
                    }
                    XPathNodeIterator LdeviceIterator = tmp.Current.SelectDescendants("LDevice", navigator.NamespaceURI, true);
                    while (LdeviceIterator.MoveNext())
                    {
                        XPathNodeIterator lDeviceTmp = LdeviceIterator.Clone();
                        string logicalDeviceName = "";
                        if (lDeviceTmp.Current != null)
                        {
                            if (lDeviceTmp.Current.Name == "LDevice")
                            {
                                lDeviceTmp.Current.MoveToFirstAttribute();
                                do
                                {
                                    if (lDeviceTmp.Current.Name == "inst")
                                    {
                                        logicalDeviceName = lDeviceTmp.Current.Value;
                                    }
                                } while (lDeviceTmp.Current.MoveToNextAttribute());

                                if (!string.IsNullOrEmpty(logicalDeviceName))
                                {
                                    lDeviceTmp.Current.MoveToParent();
                                }
                                else
                                {
                                    MessageBox.Show("Logical device node name value is empty.\n Please check the SCL file",
                                                    "SCL parsing error",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                                    this.CorruptedScl = true;
                                    break;
                                }
                            }
                        }
                        this.parseLN(logicalDeviceName, ieDName, LdeviceIterator);
                        //Namrata:24/10/2017
                        Utils.LogicalDeviceName = logicalDeviceName;
                    }
                }
            }
            if (this.CorruptedScl == true)
            {
                this.IIeds.Clear();
                this.IIedMappings.Clear();
                this.ILogicalNodes.Clear();
                return;
            }
            this.chooseMappingItems();
            this.parsePrivateSection();
        }
        private void ParserIEDName()
        {
            XPathNavigator navigator = LastChild.CreateNavigator();
            XPathNodeIterator LNodeType = navigator.SelectDescendants("Communication", navigator.NamespaceURI, false);
            while (LNodeType.MoveNext())
            {
                XPathNodeIterator tmp = LNodeType.Clone();
                tmp.Current.MoveToFirstAttribute();
                XPathNodeIterator tmp2 = LNodeType.Clone();
                tmp2 = tmp2.Current.SelectDescendants("ConnectedAP", navigator.NamespaceURI, false);
                while (tmp2.MoveNext())
                {
                    XPathNodeIterator tmp3 = tmp2.Clone();
                    tmp3.Current.MoveToFirstAttribute();
                    string IEDName = "";
                    if (tmp3.CurrentPosition == 1)
                    {
                        if (tmp3.Current.Name == "iedName")
                        {
                            IEDName = tmp3.Current.Value;
                            Utils.FinalIEDName = IEDName;
                        }
                    }
                    else { }
                }
            }
        }
        /// <summary>
        /// Builds dictionary with existing private tags from eSCL, allows reediting functionality.
        /// </summary>
        /// <returns></returns>
        private void parsePrivateSection()
        {
            string address = "";
            string dataset = "";
            string iedName = "";
            string trgOpt = "";
            string iPrd = "";
            string confRev = "";
            string bufTime = "";
            string custom = "";
            string fc = "";
            string desc = "";
            string type = "";
            string rType = "";
            string index = "";
            int lastCOid = -1;
            int tmpIndex = 0;
            int iter = 0;
            string findRep = "";
            bool skipRCBItem = false;
            bool skipDIItem = false;

            XPathNavigator navigator = LastChild.CreateNavigator();
            XPathNodeIterator privateRCBIterator = navigator.SelectDescendants("RCB", "http://www.ashidaelectronics.com/", false);
            while (privateRCBIterator.MoveNext())
            {
                XPathNodeIterator tmp = privateRCBIterator.Clone();
                tmp.Current.MoveToFirstAttribute();
                address = "";
                dataset = "";
                iedName = "";
                trgOpt = "0";
                iPrd = "0";
                confRev = "";
                bufTime = "0";
                custom = "";

                do
                {
                    if (tmp.Current.Name == "address")
                    {
                        address = tmp.Current.Value;
                    }
                    if (tmp.Current.Name == "iedName")
                    {
                        iedName = tmp.Current.Value;
                    }
                    if (tmp.Current.Name == "dataset")
                    {
                        dataset = tmp.Current.Value;
                    }
                    if (tmp.Current.Name == "triggerOptions")
                    {
                        trgOpt = tmp.Current.Value;
                    }
                    if (tmp.Current.Name == "integrityPeriod")
                    {
                        iPrd = tmp.Current.Value;
                    }
                    if (tmp.Current.Name == "configRevision")
                    {
                        confRev = tmp.Current.Value;
                    }
                    if (tmp.Current.Name == "bufferTime")
                    {
                        bufTime = tmp.Current.Value;
                    }
                    if (tmp.Current.Name == "custom")
                    {
                        custom = tmp.Current.Value;
                    }
                } while (tmp.Current.MoveToNextAttribute());

                skipRCBItem = this.verifyRcb(privateRCBIterator, address, iedName, dataset, trgOpt, confRev, custom);
                if (skipRCBItem != true)
                {
                    string[] temp = address.Split('.', '/');
                    string[] dsNameTmp = dataset.Split('.', '/');
                    string ld = temp[0].Substring(iedName.Length);
                    string repFromAddres = FindReport(address);
                    Report report = this.IReports[repFromAddres];
                    report.Address = address;
                    report.IedName = iedName;
                    report.LDName = ld;
                    report.LNName = temp[1];
                    report.ReportName = temp[2];
                    if (dsNameTmp.Length > 1)
                    {
                        report.DSName = dsNameTmp[2];
                    }
                    else
                    {
                        report.DSName = dsNameTmp[0];
                    }
                    if (report.DSName.StartsWith("@", StringComparison.CurrentCulture))
                    {
                        report.DSAddressDots = report.DSName + "." + iedName + "." + ld + "." + report.LNName;
                        foreach (var key in this.ICustomDataSets.Keys)
                        {
                            if (key.Contains(report.DSName + ".") && key.Contains(report.IedName))
                            {
                                report.DSAddressDots = key;
                            }
                        }
                        report.DSAddress = report.DSName;
                    }
                    else
                    {
                        report.DSAddress = iedName + ld + "/" + temp[1] + "." + report.DSName;
                        report.DSAddressDots = report.DSName + "." + iedName + "." + ld + "." + report.LNName;
                    }
                    if (dsNameTmp.Length == 1)
                    {
                        this.IReports[repFromAddres].DSName = dsNameTmp[0];
                        if (this.ICustomDataSets != null)
                        {
                            if (this.ICustomDataSets.Count > 0)
                            {
                                bool toAdd = false;
                                foreach (var key in this.ICustomDataSets.Keys)
                                {
                                    if (!key.Contains(report.DSName))
                                    {
                                        toAdd = true;
                                    }
                                }
                                if (toAdd)
                                {
                                    this.ICustomDataSets.Add(report.DSAddressDots, null);
                                }
                            }
                            else
                            {
                                this.ICustomDataSets.Add(report.DSAddressDots, null);
                            }
                        }
                    }
                    else
                    {
                        if (this.ICustomDataSets != null)
                        {
                            if (!this.IDataSets.ContainsKey(report.DSAddressDots) && (!this.ICustomDataSets.ContainsKey(report.DSAddressDots)))
                            {
                                this.ICustomDataSets.Add(report.DSAddressDots, null);
                            }
                        }
                    }
                    report.IntgPeriod = iPrd;
                    report.RptId = address.Replace('.', '$');
                    report.ConfRev = confRev;
                    report.BufTime = bufTime;
                    report.TrgOptNum = trgOpt;
                    report.Custom = custom;
                    if (!this.IUsedRcbs.Contains(report))
                    {
                        this.IUsedRcbs.Add(report);
                    }
                    skipRCBItem = false;
                }
            }

            bool shownInvalidMappingMessageBox = false;

            XPathNodeIterator privateDiIterator = navigator.SelectDescendants("DI", "http://www.ashidaelectronics.com/", false);
            while (privateDiIterator.MoveNext())
            {
                XPathNodeIterator tmp = privateDiIterator.Clone();
                tmp.Current.MoveToFirstAttribute();
                address = "";
                fc = "";
                iedName = "";
                desc = "";
                type = "";
                rType = "";
                index = "";
                do
                {
                    if (tmp.Current.Name == "address")
                    {
                        address = tmp.Current.Value;
                    }
                    if (tmp.Current.Name == "fc")
                    {
                        fc = tmp.Current.Value;
                    }
                    if (tmp.Current.Name == "iedName")
                    {
                        iedName = tmp.Current.Value;
                    }
                    if (tmp.Current.Name == "mType")
                    {
                        type = tmp.Current.Value;
                    }
                    if (tmp.Current.Name == "description")
                    {
                        desc = tmp.Current.Value;
                    }
                    if (tmp.Current.Name == "rType")
                    {
                        rType = tmp.Current.Value;
                    }
                    if (tmp.Current.Name == "index")
                    {
                        index = tmp.Current.Value;
                    }
                } while (tmp.Current.MoveToNextAttribute());

                skipDIItem = this.verifyDataItem(privateDiIterator, address, fc, iedName, type, rType, index);
                findRep = this.FindReport(rType);
                if (!skipDIItem && (rType.Equals("On Request") || this.IUsedRcbs.Contains(this.IReports[findRep])))
                {
                    if (!IsMappingValid(findRep, address, fc, rType))
                    {
                        if (!shownInvalidMappingMessageBox)
                        {
                            // message box will be shown only once
                            //MessageBox.Show("Invalid mapping entries found, they will be removed.", MainWindow.TITLE_COMMON, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            shownInvalidMappingMessageBox = true;
                            this.MappingChanged = true;
                        }
                        continue;
                    }

                    bool foundInGridItems = false;
                    foreach (GridItem item in this.IGridItems)
                    {
                        if ((item.IID != lastCOid
                             && item.ObjectReference == address
                             && item.FC == fc)
                            || item.IID == lastCOid
                            && item.ObjectReference == address
                            && (!item.FC.Equals("CO")))
                        {
                            item.IsChecked = true;
                            item.MappingType = type;
                            item.Description = desc;
                            item.RefreshType = rType;
                            item.Index = index;
                            lastCOid = item.IID;
                            foundInGridItems = true;
                            break;
                        }
                    }
                    if (foundInGridItems == false)
                    {
                        if (this.skipWrongItems == true)
                        {
                            this.MappingChanged = true;
                        }
                        else
                        {
                            DialogResult drs = MessageBox.Show("Invalid data item:\n\n" + privateDiIterator.Current.OuterXml.ToString() + "\n\nDo you want to skip it?",
                                                               "Private section parsing error",
                                                               MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            if (drs == DialogResult.Yes)
                            {
                                DialogResult dialRes = MessageBox.Show("Do you want to skip ALL invalid items?",
                                                                       "Private section parsing error",
                                                                       MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                                if (dialRes == DialogResult.Yes)
                                {
                                    this.skipWrongItems = true;
                                }
                                else
                                {
                                    this.skipWrongItems = false;
                                }
                                this.MappingChanged = true;
                            }
                            if (drs == DialogResult.No)
                            {
                                this.skipWrongItems = false;
                                this.WrongEntry = true;
                                this.ICustomDataSets.Clear();
                                this.ICustomDataSetsInUse.Clear();
                                this.IUsedRcbs.Clear();
                                this.IAdditionalTags.Clear();
                                this.IIedMappings.Clear();
                                this.IReports.Clear();
                                this.IGridItems.Clear();
                                this.ILocalRcbs.Clear();
                                this.IDataSets.Clear();
                                this.ILogicalNodes.Clear();
                                this.ILNodeTypes.Clear();
                                this.IDOTypes.Clear();
                                this.IDATypes.Clear();
                                throw new ArgumentException("Data Item entry has at least one invalid or missing attribute.");
                            }
                        }
                        continue;
                    }
                    tmpIndex = Convert.ToInt32(index, CultureInfo.InvariantCulture);
                    if (lastCOid != -1
                        && !this.IAdditionalTags.ContainsKey(address + fc + lastCOid))
                    {
                        this.IAdditionalTags.Add(address + fc + lastCOid, new PrivateTag(lastCOid, type, index, address, fc, desc, iedName, rType));
                        if (tmpIndex > this.IIedMappings[iedName + "." + type].Count)
                        {
                            this.IIedMappings[iedName + "." + type].Add(this.IAdditionalTags[address + fc + lastCOid]);
                        }
                        else
                        {
                            this.IIedMappings[iedName + "." + type][tmpIndex - 1] = this.IAdditionalTags[address + fc + lastCOid];
                        }
                    }
                }
                skipDIItem = false;
            }
            foreach (List<PrivateTag> ied in this.IIedMappings.Values)
            {
                iter = ied.Count;
                while ((iter > 0) && (ied[iter - 1] == null))
                {
                    ied.RemoveAt(iter - 1);
                    iter--;
                }
            }
        }

        /// <summary>
        /// Check if mapping for report request type is valid.
        /// </summary>
        /// <param name="reportName"></param>
        /// <param name="address"></param>
        /// <param name="fc"></param>
        /// <param name="requestType"></param>
        /// <returns></returns>
        private bool IsMappingValid(string reportName, string address, string fc, string requestType)
        {
            if (this.IReports.ContainsKey(reportName) && this.IUsedRcbs.Contains(this.IReports[reportName]) && !requestType.Equals("On Request"))
            {
                var ds = this.IDataSets.FirstOrDefault(p => p.Key == this.IReports[reportName].DSAddressDots);
                if (ds.Value != null)
                {
                    if (ds.Value.Count(p => p.FC == fc && (p.Address == address || address.Contains(p.Address + "."))) == 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Verifies the selected report is used in configuration - check there is a data item assigned to it.
        /// </summary>
        /// <param name="rpt">Report that should be checked.</param>
        /// <param name="tags">Mapped data item dictionary.</param>
        /// <returns>
        ///   <c>true</c> if the report used; otherwise, <c>false</c>.
        /// </returns>


        /// <summary>
        /// Validates the RCB entry.
        /// </summary>
        /// <param name="rcbi">XML iterator of selected RCB entry.</param>
        /// <param name="address">RCB reference address.</param>
        /// <param name="iedName">IED name.</param>
        /// <param name="dataset">Dataset reference.</param>
        /// <param name="trgOpt">Triggering options.</param>
        /// <param name="confRev">Configuration revision.</param>
        /// <param name="custom">Indicates if the data set is custom and shall be created when necessary.</param>
        /// <returns>
        /// Returns boolean value: true if entry is wrong and false if not
        /// </returns>
        /// <exception cref="System.ArgumentException">Report Control Block has at least one invalid or missing attribute.</exception>
        private bool verifyRcb(XPathNodeIterator rcbi, string address, string iedName, string dataset, string trgOpt, string confRev, string custom)
        {
            bool skipRCBItem = false;
            bool duplicateDynDs = false;
            string[] splittedDataset;
            string dsDotted = "";
            string dynamicDs = "";

            if (!dataset.Contains("@"))
            {
                splittedDataset = dataset.Split('/', '.');
                dsDotted = splittedDataset[2] + "." + iedName + "." + splittedDataset[0].Replace(iedName, "") + "." + splittedDataset[1];
            }
            else
            {
                dynamicDs = dataset;
                if (!this.ICustomDataSetsInUse.ContainsKey(dynamicDs) && custom == "true")
                {
                    this.ICustomDataSetsInUse.Add(dynamicDs, dynamicDs);
                }
                else
                {
                    duplicateDynDs = true;
                }
            }
            if (string.IsNullOrEmpty(address)
                || string.IsNullOrEmpty(iedName)
                || string.IsNullOrEmpty(dataset)
                || string.IsNullOrEmpty(trgOpt)
                || string.IsNullOrEmpty(confRev)
                || (!this.IDataSets.ContainsKey(dsDotted) && !custom.Equals("true"))
                || (!string.IsNullOrEmpty(dynamicDs) && !custom.Equals("true"))
                || duplicateDynDs
                || string.IsNullOrEmpty(FindReport(address))
                )
            {
                if (this.skipWrongItems == true)
                {
                    skipRCBItem = true;
                    this.MappingChanged = true;
                }
                else
                {
                    DialogResult ds = MessageBox.Show("Invalid report control block:\n\n" + rcbi.Current.OuterXml.ToString() + "\n\nDo you want to skip this RCB with assigned data items?",
                                                      "Private section parsing error",
                                                      MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    if (ds == DialogResult.Yes)
                    {
                        DialogResult dialRes = MessageBox.Show("Do you want to skip ALL invalid entries?",
                                                               "Private section parsing error",
                                                               MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        if (dialRes == DialogResult.Yes)
                        {
                            skipRCBItem = true;
                            this.skipWrongItems = true;
                        }
                        else
                        {
                            this.skipWrongItems = false;
                            skipRCBItem = true;
                        }
                        this.MappingChanged = true;
                    }
                    if (ds == DialogResult.No)
                    {
                        this.WrongEntry = true;
                        this.ICustomDataSets.Clear();
                        this.ICustomDataSetsInUse.Clear();
                        this.IUsedRcbs.Clear();
                        this.IAdditionalTags.Clear();
                        this.IIedMappings.Clear();
                        this.IReports.Clear();
                        this.IGridItems.Clear();
                        this.ILocalRcbs.Clear();
                        this.IDataSets.Clear();
                        this.ILogicalNodes.Clear();
                        this.ILNodeTypes.Clear();
                        this.IDOTypes.Clear();
                        this.IDATypes.Clear();
                        throw new ArgumentException("Report Control Block has at least one invalid or missing attribute.");
                    }
                }
            }
            return skipRCBItem;
        }

        /// <summary>
        /// Validates data item (includes attributes and syntax).
        /// </summary>
        /// <param name="iterator">XML iterator of the selected data item.</param>
        /// <param name="address">The address.</param>
        /// <param name="fc">The functional constraint.</param>
        /// <param name="iedName">The IED name.</param>
        /// <param name="type">The IEC type.</param>
        /// <param name="rType">Refresh type.</param>
        /// <param name="index">The SCADA index.</param>
        /// <returns>
        /// Returns boolean value: true if entry is wrong and false if not
        /// </returns>
        /// <exception cref="System.ArgumentException">Data Item entry has at least one invalid or missing attribute.</exception>
        private bool verifyDataItem(XPathNodeIterator iterator, string address, string fc, string iedName, string type, string rType, string index)
        {
            bool skipDIItem = false;

            if (string.IsNullOrEmpty(address)
                || string.IsNullOrEmpty(fc)
                || string.IsNullOrEmpty(iedName)
                || string.IsNullOrEmpty(type)
                || string.IsNullOrEmpty(rType)
                || string.IsNullOrEmpty(index)
                || (string.IsNullOrEmpty(FindReport(rType)) && !rType.Equals("On Request")))
            {
                if (this.skipWrongItems == true)
                {
                    skipDIItem = true;
                    this.MappingChanged = true;
                }
                else
                {
                    DialogResult drs = MessageBox.Show("Invalid data item:\n\n" + iterator.Current.OuterXml.ToString() + "\n\nDo you want to skip this it?",
                                                       "Private section parsing error",
                                                       MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    if (drs == DialogResult.Yes)
                    {
                        DialogResult dialRes = MessageBox.Show("Do you want to skip ALL invalid entries?",
                                                               "Private section parsing error",
                                                               MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        if (dialRes == DialogResult.Yes)
                        {
                            this.skipWrongItems = true;
                            skipDIItem = true;
                        }
                        else
                        {
                            this.skipWrongItems = false;
                            skipDIItem = true;
                        }
                        this.MappingChanged = true;
                    }
                    if (drs == DialogResult.No)
                    {
                        this.skipWrongItems = false;
                        this.WrongEntry = true;
                        this.ICustomDataSets.Clear();
                        this.ICustomDataSetsInUse.Clear();
                        this.IUsedRcbs.Clear();
                        this.IAdditionalTags.Clear();
                        this.IIedMappings.Clear();
                        this.IReports.Clear();
                        this.IGridItems.Clear();
                        this.ILocalRcbs.Clear();
                        this.IDataSets.Clear();
                        this.ILogicalNodes.Clear();
                        this.ILNodeTypes.Clear();
                        this.IDOTypes.Clear();
                        this.IDATypes.Clear();
                        throw new ArgumentException("Data Item entry has at least one invalid or missing attribute.");
                    }
                }
            }
            return skipDIItem;
        }

        /// <summary>
        /// Parses logical nodes in the logical device.
        /// </summary>
        /// <param name="ldName">The logical device name.</param>
        /// <param name="IEDname">The IED name</param>
        /// <param name="ld">XPath iterator for the logical device.</param>
        private void parseLN(string ldName, string IEDname, XPathNodeIterator ld)
        {
            XPathNavigator navigator = LastChild.CreateNavigator();
            XPathNodeIterator ln;

            ln = ld.Current.SelectDescendants("LN0", navigator.NamespaceURI, false);
            this.getLN(ldName, IEDname, ln);
            ln = ld.Current.SelectDescendants("LN", navigator.NamespaceURI, false);
            this.getLN(ldName, IEDname, ln);
        }

        /// <summary>
        /// Parses selected logical nodes.
        /// </summary>
        /// <param name="ldName">The logical device name.</param>
        /// <param name="iedName">The IED name.</param>
        /// <param name="ln">XPath iterator with logical nodes.</param>
        private void getLN(string ldName, string iedName, XPathNodeIterator ln)
        {
            string lnClass = "";
            string lnName = "";
            while (ln.MoveNext())
            {
                string instLn = "";
                string prefix = "";
                string lnInst = "";
                string lnType = "";
                lnName = ln.Current.Name;
                if (ln.Current != null)
                {
                    XPathNavigator backup = ln.Current.Clone();
                    XPathNavigator backup2 = ln.Current.Clone();
                    backup.MoveToParent();
                    backup.MoveToFirstAttribute();
                    do
                    {
                        if (backup.Name == "inst")
                        {
                            instLn = backup.Value;
                        }
                    } while (backup.MoveToNextAttribute());
                    if (ldName != instLn)
                    {
                        continue;
                    }
                    if (ln.Current != null)
                    {
                        XPathNavigator nextBackup = ln.Current.Clone();
                        nextBackup.MoveToFirstAttribute();
                        do
                        {
                            if (nextBackup.Name == "prefix")
                            {
                                prefix = nextBackup.Value;
                            }
                            if (nextBackup.Name == "lnClass")
                            {
                                lnClass = nextBackup.Value;
                            }
                            if (nextBackup.Name == "inst")
                            {
                                lnInst = nextBackup.Value;
                            }
                            if (nextBackup.Name == "lnType")
                            {
                                lnType = nextBackup.Value;
                            }
                        } while (nextBackup.MoveToNextAttribute());
                        string lnCheck = iedName + ldName + prefix + lnClass + lnInst;
                        if (lnInst == "" && lnName == "LN")
                        {
                            MessageBox.Show("Logical node 'inst' value is empty.\nPlease check the SCL file",
                                            "SCL parsing error",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            this.CorruptedScl = true;
                            break;
                        }
                        try
                        {
                            this.ILogicalNodes.Add(lnCheck, new LN(lnType, lnClass, prefix, lnInst, iedName, ldName));
                        }
                        catch (ArgumentException)
                        {
                            throw new ArgumentException("Duplicated logical node: " + ldName);
                        }

                        XPathNodeIterator doiIterator = backup2.SelectDescendants("DOI", backup2.NamespaceURI, false);
                        while (doiIterator.MoveNext())
                        {
                            XPathNavigator doiBack = doiIterator.Current.Clone();
                            doiBack.MoveToFirstAttribute();
                            string doiName = "";
                            do
                            {
                                if (doiBack.Name == "name")
                                {
                                    doiName = doiBack.Value;
                                }
                            } while (doiBack.MoveToNextAttribute());
                            XPathNodeIterator dai = doiIterator.Current.SelectDescendants("DAI", backup2.NamespaceURI, false);
                            while (dai.MoveNext())
                            {
                                XPathNavigator back = dai.Current.Clone();
                                back.MoveToFirstAttribute();
                                do
                                {
                                    if (back.Name == "name" && back.Value == "ctlModel")
                                    {
                                        string model = "";
                                        XPathNodeIterator ctl = dai.Clone();
                                        ctl = ctl.Current.SelectDescendants("Val", backup2.NamespaceURI, false);
                                        while (ctl.MoveNext())
                                        {
                                            model = ctl.Current.Value;
                                        }
                                        if (model != "")
                                        {
                                            if (this.ILNodeTypes.ContainsKey(lnType))
                                            {
                                                LNType l = this.ILNodeTypes[lnType];
                                                if (l.DataObjects.ContainsKey(doiName))
                                                {
                                                    string t = l.DataObjects[doiName];
                                                    if (this.IDOTypes.ContainsKey(t))
                                                    {
                                                        Dictionary<string, DataItem> dic = this.IDOTypes[t];
                                                        foreach (var v in dic)
                                                        {
                                                            if (v.Key.Contains("ctlModel"))
                                                            {
                                                                v.Value.CtlModel = model;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                } while (back.MoveToNextAttribute());
                            }
                        }
                    }
                }
                XPathNodeIterator copy = ln.Clone();
                this.parseDataSets(copy, iedName, ldName, prefix + lnClass + lnInst);
                this.parseReports(copy, iedName, ldName, prefix + lnClass + lnInst);
            }
        }
        /// <summary>
        /// Parses data attribute types.
        /// </summary>
        private void parseDATypes()
        {
            XPathNavigator navigator = LastChild.CreateNavigator();
            XPathNodeIterator DATypes = navigator.SelectDescendants("DAType", navigator.NamespaceURI, false);
            string name = "";
            string bType = "";
            string type = "";
            string model = "";

            while (DATypes.MoveNext())
            {
                XPathNodeIterator tmp = DATypes.Clone();
                tmp.Current.MoveToFirstAttribute();
                string daTypeID = "";
                do
                {
                    if (tmp.Current.Name == "id")
                    {
                        daTypeID = tmp.Current.Value;
                    }
                } while (tmp.Current.MoveToNextAttribute());
                XPathNodeIterator tmp2 = DATypes.Clone();
                tmp2 = tmp2.Current.SelectDescendants("BDA", navigator.NamespaceURI, false);
                var dict = new Dictionary<string, Bda>();
                while (tmp2.MoveNext())
                {
                    XPathNodeIterator tmp3 = tmp2.Clone();
                    tmp3.Current.MoveToFirstAttribute();
                    name = "";
                    bType = "";
                    type = "";
                    model = "";
                    do
                    {
                        if (tmp3.Current.Name == "name")
                        {
                            name = tmp3.Current.Value;
                        }
                        if (tmp3.Current.Name == "bType")
                        {
                            bType = tmp3.Current.Value;
                        }
                        if (tmp3.Current.Name == "type")
                        {
                            type = tmp3.Current.Value;
                        }
                    } while (tmp3.Current.MoveToNextAttribute());

                    if (name == "ctlModel")
                    {
                        XPathNodeIterator ctl = tmp3.Clone();
                        ctl.Current.MoveToParent();
                        ctl = ctl.Current.SelectDescendants("Val", navigator.NamespaceURI, false);
                        while (ctl.MoveNext())
                        {
                            model = ctl.Current.Value;
                        }
                    }
                    if (name.Contains("ctlVal"))
                    {
                        try
                        {
                            // Separate item for command ON
                            dict.Add(name + "." + this.iid, new Bda(this.iid, name, bType, type));
                            this.iid++;
                            // Separate item for command OFF
                            dict.Add(name + "." + this.iid, new Bda(this.iid, name, bType, type));
                        }
                        catch (ArgumentException)
                        {
                            throw new ArgumentException("Duplicated BDA entry: " + name);
                        }
                    }
                    else
                    {
                        try
                        {
                            dict.Add(name + "." + this.iid, new Bda(this.iid, name, bType, type));
                        }
                        catch (ArgumentException)
                        {
                            throw new ArgumentException("Duplicated BDA entry: " + name);
                        }
                        this.iid++;
                    }
                }
                this.IDATypes.Add(daTypeID, dict);
            }
        }

        /// <summary>
        /// Parses logical node types in the SCL.
        /// </summary>
        public void parseLNodeTypes()
        {
            XPathNavigator navigator = LastChild.CreateNavigator();
            XPathNodeIterator LNodeType = navigator.SelectDescendants("LNodeType", navigator.NamespaceURI, false);

            while (LNodeType.MoveNext())
            {
                XPathNodeIterator tmp = LNodeType.Clone();
                tmp.Current.MoveToFirstAttribute();
                string lnTypeId = "";
                do
                {
                    if (tmp.Current.Name == "id")
                    {
                        lnTypeId = tmp.Current.Value;
                    }
                } while (tmp.Current.MoveToNextAttribute());

                LNType ln = new LNType();
                XPathNodeIterator tmp2 = LNodeType.Clone();
                tmp2 = tmp2.Current.SelectDescendants("DO", navigator.NamespaceURI, false);
                while (tmp2.MoveNext())
                {
                    XPathNodeIterator tmp3 = tmp2.Clone();
                    tmp3.Current.MoveToFirstAttribute();
                    string name = "";
                    string type = "";
                    do
                    {
                        if (tmp3.Current.Name == "name")
                        {
                            name = tmp3.Current.Value;
                        }
                        if (tmp3.Current.Name == "type")
                        {
                            type = tmp3.Current.Value;
                        }
                    } while (tmp3.Current.MoveToNextAttribute());
                    try
                    {
                        ln.AddDO(name, type);
                    }
                    catch (ArgumentException)
                    {
                        throw new ArgumentException("Duplicated DO entry: " + name);
                    }
                }
                try
                {
                    this.ILNodeTypes.Add(lnTypeId, ln);
                }
                catch (ArgumentException)
                {
                    throw new ArgumentException("Duplicated LNodeType entry: " + lnTypeId);
                }
            }
        }

        /// <summary>
        /// Parses data object types in the SCL.
        /// </summary>
        public void parseDOTypes()
        {
            string name = "";
            string type = "";
            string FC = "";
            string bType = "";
            string count = "";
            string model = "";
            XPathNavigator navigator = LastChild.CreateNavigator();
            XPathNodeIterator DOType = navigator.SelectDescendants("DOType", navigator.NamespaceURI, false);

            while (DOType.MoveNext())
            {
                Dictionary<string, DataItem> daDict = new Dictionary<string, DataItem>();

                XPathNodeIterator tmp = DOType.Clone();
                tmp.Current.MoveToFirstAttribute();
                string doTypeId = "";
                do
                {
                    if (tmp.Current.Name == "id")
                    {
                        doTypeId = tmp.Current.Value;
                    }
                } while (tmp.Current.MoveToNextAttribute());

                XPathNodeIterator tmp2 = DOType.Clone();
                tmp2 = tmp2.Current.SelectDescendants("DA", navigator.NamespaceURI, false);
                if (tmp2.Count > 0)
                {
                    while (tmp2.MoveNext())
                    {
                        XPathNodeIterator tmp3 = tmp2.Clone();
                        tmp3.Current.MoveToFirstAttribute();
                        name = "";
                        type = "";
                        FC = "";
                        bType = "";
                        count = "";
                        model = "";
                        do
                        {
                            if (tmp3.Current.Name == "name")
                            {
                                name = tmp3.Current.Value;
                            }
                            if (tmp3.Current.Name == "fc")
                            {
                                FC = tmp3.Current.Value;
                            }
                            if (tmp3.Current.Name == "bType")
                            {
                                bType = tmp3.Current.Value;
                            }
                            if (tmp3.Current.Name == "type")
                            {
                                type = tmp3.Current.Value;
                            }
                            if (tmp3.Current.Name == "count")
                            {
                                count = tmp3.Current.Value;
                            }
                        } while (tmp3.Current.MoveToNextAttribute());

                        if (name == "ctlModel")
                        {
                            XPathNodeIterator ctl = tmp3.Clone();
                            ctl.Current.MoveToParent();
                            ctl = ctl.Current.SelectDescendants("Val", navigator.NamespaceURI, false);
                            while (ctl.MoveNext())
                            {
                                model = ctl.Current.Value;
                            }
                        }
                        if (!name.Equals("SBOw") &&
                            !name.Equals("Cancel"))
                        {
                            DataItem da = new DataItem(this.iid, false, name, FC, bType, type, count, model);
                            try
                            {
                                daDict.Add(name + "." + this.iid, da);
                            }
                            catch (ArgumentException)
                            {
                                throw new ArgumentException("Duplicated DA entry: " + name);
                            }
                            this.iid++;
                        }
                    }
                }

                XPathNodeIterator tmp4 = DOType.Clone();
                tmp4 = tmp4.Current.SelectDescendants("SDO", navigator.NamespaceURI, false);
                if (tmp4.Count > 0)
                {
                    while (tmp4.MoveNext())
                    {
                        XPathNodeIterator tmp3 = tmp4.Clone();
                        tmp3.Current.MoveToFirstAttribute();
                        name = "";
                        type = "";
                        count = "";
                        do
                        {
                            if (tmp3.Current.Name == "name")
                            {
                                name = tmp3.Current.Value;
                            }
                            if (tmp3.Current.Name == "type")
                            {
                                type = tmp3.Current.Value;
                            }
                            if (tmp3.Current.Name == "count")
                            {
                                count = tmp3.Current.Value;
                            }
                        } while (tmp3.Current.MoveToNextAttribute());
                        if (name == "ctlModel")
                        {
                            XPathNodeIterator ctl = tmp3.Clone();
                            ctl.Current.MoveToParent();
                            ctl = ctl.Current.SelectDescendants("Val", navigator.NamespaceURI, false);
                            while (ctl.MoveNext())
                            {
                                model = ctl.Current.Value;
                            }
                        }
                        try
                        {
                            DataItem da = new DataItem(this.iid, true, name, "", "", type, count, model);
                            daDict.Add(name + "." + this.iid, da);
                            this.iid++;
                        }
                        catch (ArgumentException)
                        {
                            throw new ArgumentException("Duplicated data object: " + name);
                        }
                    }
                }
                try
                {
                    this.IDOTypes.Add(doTypeId, daDict);
                }
                catch (ArgumentException)
                {
                    throw new ArgumentException("Duplicated DOType: " + doTypeId);
                }
            }
        }

        //Namrata:14/03/2018 
        private void ParserRemoteIP()
        {
            XPathNavigator navigator = LastChild.CreateNavigator();
            XPathNodeIterator LNodeType = navigator.SelectDescendants("Communication", navigator.NamespaceURI, false);
            while (LNodeType.MoveNext())
            {
                XPathNodeIterator tmp = LNodeType.Clone();
                tmp.Current.MoveToFirstAttribute();
                XPathNodeIterator tmp2 = LNodeType.Clone();
                tmp2 = tmp2.Current.SelectDescendants("P", navigator.NamespaceURI, false);
                while (tmp2.MoveNext())
                {
                    XPathNodeIterator tmp3 = tmp2.Clone();
                    tmp3.Current.MoveToFirstAttribute();
                    string RemoteIP = "";
                    if (tmp3.Current.Value == "IP")
                    {
                        if (tmp2.Current.Name == "P")
                        {
                            RemoteIP = tmp2.Current.Value;
                            Utils.RemoteIPAddress = RemoteIP;
                        }
                    }
                    else
                    {
                    }
                }
            }
        }

        /// <summary>
        /// Parses data sets from the SCL document.
        /// </summary>
        /// <param name="lni">The XPath iterator at the logical node instance.</param>
        /// <param name="iedName">The IED name.</param>
        /// <param name="ldName">The logical device name.</param>
        /// <param name="lnName">The logical node name.</param>
        private void parseDataSets(XPathNodeIterator lni, string iedName, string ldName, string lnName)
        {
            string ldInst = "";
            string prefix = "";
            string lnClass = "";
            string lnInst = "";
            string doName = "";
            string daName = "";
            string fc = "";

            XPathNodeIterator tmp = lni.Current.SelectDescendants("DataSet", lni.Current.NamespaceURI, false);
            while (tmp.MoveNext())
            {
                XPathNodeIterator DsName = tmp.Clone();
                DsName.Current.MoveToFirstAttribute();
                string name = "";
                do
                {
                    if (DsName.Current.Name == "name")
                    {
                        name = DsName.Current.Value;
                        break;
                    }
                } while (DsName.Current.MoveToNextAttribute());

                XPathNodeIterator fcdaIterator = tmp.Current.SelectDescendants("FCDA", lni.Current.NamespaceURI, false);
                List<Fcda> list = new List<Fcda>();
                while (fcdaIterator.MoveNext())
                {
                    XPathNodeIterator tmp2 = fcdaIterator.Clone();
                    ldInst = "";
                    prefix = "";
                    lnClass = "";
                    lnInst = "";
                    doName = "";
                    daName = "";
                    fc = "";
                    tmp2.Current.MoveToFirstAttribute();
                    do
                    {
                        if (tmp2.Current.Name == "ldInst")
                        {
                            ldInst = tmp2.Current.Value;
                        }
                        if (tmp2.Current.Name == "prefix")
                        {
                            prefix = tmp2.Current.Value;
                        }
                        if (tmp2.Current.Name == "lnClass")
                        {
                            lnClass = tmp2.Current.Value;
                        }
                        if (tmp2.Current.Name == "lnInst")
                        {
                            lnInst = tmp2.Current.Value;
                        }
                        if (tmp2.Current.Name == "doName")
                        {
                            doName = tmp2.Current.Value;
                        }
                        if (tmp2.Current.Name == "daName")
                        {
                            daName = tmp2.Current.Value;
                        }
                        if (tmp2.Current.Name == "fc")
                        {
                            fc = tmp2.Current.Value;
                        }
                    } while (tmp2.Current.MoveToNextAttribute());
                    try
                    {
                        list.Add(new Fcda(iedName, name, ldInst, prefix, lnClass, lnInst, doName, daName, fc));
                    }
                    catch (ArgumentException)
                    {
                        throw new ArgumentException("Duplicated FCDA: " + name);
                    }
                }
                try
                {
                    this.IDataSets.Add(name + "." + iedName + "." + ldName + "." + lnName, list);
                }
                catch (ArgumentException)
                {
                    throw new ArgumentException("Duplicated DatSet entry: " + name);
                }
            }
        }

        /// <summary>
        /// Parses ReportControl section and creates related RCB instances.
        /// </summary>
        /// <param name="lni">XPath iterator at the specified logical node.</param>
        /// <param name="iedName">The IED name.</param>
        /// <param name="ldName">The logical device name.</param>
        /// <param name="lnName">The logical node name.</param>
        private void parseReports(XPathNodeIterator lni, string iedName, string ldName, string lnName)
        {
            if (lni.Current != null)
            {
                string name = "";
                string dSet = "";
                string iPd = "";
                string rId = "";
                string cRev = "";
                string buff = "";
                string bTime = "";
                string custom = "";
                string trigOpt = "";
                string reportName = "";
                string datasetAddress = "";
                string seqNum = "";
                string timeStamp = "";
                string dataSet = "";
                string dataRef = "";
                string configRef = "";
                string segmentation = "";
                string dchg = "";
                string qchg = "";
                string dupd = "";
                string period = "";
                string gi = "";
                string tempVar = "";
                string tempVarName = "";
                string tempVarId = "";

                XPathNodeIterator rIterator = lni.Current.SelectDescendants("ReportControl", lni.Current.NamespaceURI, false);
                while (rIterator.MoveNext())
                {
                    XPathNodeIterator rIteratorOptF = rIterator.Clone();
                    XPathNodeIterator rIteratorTrigOpt = rIterator.Clone();
                    XPathNodeIterator tmp = rIterator.Clone();
                    name = "";
                    dSet = "";
                    iPd = "";
                    rId = "";
                    cRev = "";
                    buff = "";
                    bTime = "";
                    custom = "";
                    trigOpt = "";

                    tmp.Current.MoveToFirstAttribute();
                    do
                    {
                        if (tmp.Current.Name.Equals("name")) { name = tmp.Current.Value; }
                        if (tmp.Current.Name.Equals("datSet")) { dSet = tmp.Current.Value; }
                        if (tmp.Current.Name.Equals("intgPd")) { iPd = tmp.Current.Value; }
                        if (tmp.Current.Name.Equals("rptID")) { rId = tmp.Current.Value; }
                        if (tmp.Current.Name.Equals("confRev")) { cRev = tmp.Current.Value; }
                        if (tmp.Current.Name.Equals("buffered")) { buff = tmp.Current.Value; }
                        if (tmp.Current.Name.Equals("bufTime")) { bTime = tmp.Current.Value; }
                        if (tmp.Current.Name.Equals("custom")) { custom = tmp.Current.Value; }
                        if (tmp.Current.Name.Equals("triggerOptions")) { trigOpt = tmp.Current.Value; }
                    } while (tmp.Current.MoveToNextAttribute());

                    XPathNodeIterator optFieldsIterator = rIteratorOptF.Current.SelectDescendants("OptFields", lni.Current.NamespaceURI, false);
                    XPathNodeIterator tmp1;
                    XPathNodeIterator tmp2;
                    XmlNamespaceManager manager = new XmlNamespaceManager(lni.Current.NameTable);
                    manager.AddNamespace("scl", lni.Current.NamespaceURI);
                    int max = Math.Min(rIteratorTrigOpt.Current.SelectSingleNode("scl:RptEnabled/@max", manager)?.ValueAsInt ?? 0, 99);  // max shall be between 0-99
                    bool indexed = true;
                    var root = rIteratorTrigOpt.Current.Clone();
                    root.MoveToRoot();
                    if (EvaluateEdition(root) == IEC61850Edition.Edition2)
                    {
                        indexed = rIteratorTrigOpt.Current.SelectSingleNode("@indexed", manager)?.ValueAsBoolean ?? true;
                        if (max <= 0)
                        {
                            max = 1;
                        }
                    }
                    while (optFieldsIterator.MoveNext())
                    {
                        tmp1 = optFieldsIterator.Clone();
                        tmp1.Current.MoveToFirstAttribute();
                        do
                        {
                            if (tmp1.Current.Name.Equals("seqNum"))
                            {
                                seqNum = tmp1.Current.Value;
                            }
                            if (tmp1.Current.Name.Equals("timeStamp"))
                            {
                                timeStamp = tmp1.Current.Value;
                            }
                            if (tmp1.Current.Name.Equals("dataSet"))
                            {
                                dataSet = tmp1.Current.Value;
                            }
                            if (tmp1.Current.Name.Equals("dataRef"))
                            {
                                dataRef = tmp1.Current.Value;
                            }
                            if (tmp1.Current.Name.Equals("configRef"))
                            {
                                configRef = tmp1.Current.Value;
                            }
                            if (tmp1.Current.Name.Equals("segmentation"))
                            {
                                segmentation = tmp1.Current.Value;
                            }
                        } while (tmp1.Current.MoveToNextAttribute());
                    }
                    XPathNodeIterator TrgOpsIterator = rIteratorTrigOpt.Current.SelectDescendants("TrgOps", lni.Current.NamespaceURI, false);
                    while (TrgOpsIterator.MoveNext())
                    {
                        tmp2 = TrgOpsIterator.Clone();
                        tmp2.Current.MoveToFirstAttribute();
                        do
                        {
                            if (tmp2.Current.Name.Equals("dchg"))
                            {
                                dchg = tmp2.Current.Value;
                            }
                            if (tmp2.Current.Name.Equals("qchg"))
                            {
                                qchg = tmp2.Current.Value;
                            }
                            if (tmp2.Current.Name.Equals("dupd"))
                            {
                                dupd = tmp2.Current.Value;
                            }
                            if (tmp2.Current.Name.Equals("period"))
                            {
                                period = tmp2.Current.Value;
                            }
                            if (tmp2.Current.Name.Equals("gi"))
                            {
                                gi = tmp2.Current.Value;
                            }
                        } while (tmp2.Current.MoveToNextAttribute());
                    }
                    reportName = iedName + ldName + "/" + lnName + "." + name;
                    if (dSet == "")
                    {
                        custom = "true";
                        datasetAddress = "";
                    }
                    else
                    {
                        datasetAddress = iedName + ldName + "/" + lnName + "." + dSet;
                    }
                    if (indexed && (max > 0))
                    {
                        for (int i = 1; i <= max; i++)
                        {
                            try
                            {
                                if (i < 10)
                                {
                                    tempVar = reportName + "0" + i.ToString();
                                    tempVarName = name + "0" + i.ToString();
                                    tempVarId = rId + "0" + i.ToString();
                                }
                                else
                                {
                                    tempVar = reportName + i.ToString();
                                    tempVarName = name + i.ToString();
                                    tempVarId = rId + i.ToString();
                                }
                                Report report = new Report(tempVar, iedName, ldName, lnName, tempVarName, dSet, datasetAddress, iPd, tempVarId, cRev, buff, bTime, trigOpt, custom);
                                if (!string.IsNullOrEmpty(dchg))
                                {
                                    report.TrgOps["dchg"] = dchg;
                                }
                                if (!string.IsNullOrEmpty(qchg))
                                {
                                    report.TrgOps["qchg"] = qchg;
                                }
                                if (!string.IsNullOrEmpty(dupd))
                                {
                                    report.TrgOps["dupd"] = dupd;
                                }
                                if (!string.IsNullOrEmpty(period))
                                {
                                    report.TrgOps["period"] = period;
                                }
                                if (!string.IsNullOrEmpty(gi))
                                {
                                    report.TrgOps["gi"] = gi;
                                }
                                if (!string.IsNullOrEmpty(seqNum))
                                {
                                    report.OptFields["seqNum"] = seqNum;
                                }
                                if (!string.IsNullOrEmpty(timeStamp))
                                {
                                    report.OptFields["timeStamp"] = timeStamp;
                                }
                                if (!string.IsNullOrEmpty(dataSet))
                                {
                                    report.OptFields["dataSet"] = dataSet;
                                }
                                if (!string.IsNullOrEmpty(dataRef))
                                {
                                    report.OptFields["dataRef"] = dataRef;
                                }
                                if (!string.IsNullOrEmpty(configRef))
                                {
                                    report.OptFields["configRef"] = configRef;
                                }
                                if (!string.IsNullOrEmpty(segmentation))
                                {
                                    report.OptFields["segmentation"] = segmentation;
                                }
                                if (dSet != "")
                                {
                                    string check = dSet + "." + iedName + "." + ldName + "." + lnName;
                                    if (!this.IDataSets.ContainsKey(check))
                                    {
                                        MessageBox.Show("Invalid dataset defined for " + reportName + ".\nPlease check the SCL file",
                                                        "SCL parsing error",
                                                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                                    }
                                    else
                                    {
                                        this.IReports.Add(name + "0" + i.ToString() + "." + iedName + "." + ldName + "." + lnName, report);
                                    }
                                }
                                else
                                {
                                    this.IReports.Add(name + "0" + i.ToString() + "." + iedName + "." + ldName + "." + lnName, report);
                                }
                            }
                            catch (ArgumentException)
                            {
                                throw new ArgumentException("Duplicated report control block: " + name);
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            Report report = new Report(reportName, iedName, ldName, lnName, name, dSet, datasetAddress, iPd, rId, cRev, buff, bTime, trigOpt, custom);
                            if (!string.IsNullOrEmpty(dchg))
                            {
                                report.TrgOps["dchg"] = dchg;
                            }
                            if (!string.IsNullOrEmpty(qchg))
                            {
                                report.TrgOps["qchg"] = qchg;
                            }
                            if (!string.IsNullOrEmpty(dupd))
                            {
                                report.TrgOps["dupd"] = dupd;
                            }
                            if (!string.IsNullOrEmpty(period))
                            {
                                report.TrgOps["period"] = period;
                            }
                            if (!string.IsNullOrEmpty(gi))
                            {
                                report.TrgOps["gi"] = gi;
                            }
                            string check = dSet + "." + iedName + "." + ldName + "." + lnName;
                            if (dSet == "")
                            {
                                custom = "true";
                                report.DSAddress = "";
                                report.Custom = "true";
                                report.DSAddressDots = "";
                            }
                            else
                            {
                                datasetAddress = iedName + ldName + "/" + lnName + "." + dSet;
                                report.DSAddress = datasetAddress;
                            }
                            if (!this.IDataSets.ContainsKey(check) && report.Custom != "true")
                            {
                                MessageBox.Show("Invalid dataset defined for " + reportName + ".\nPlease check the SCL file",
                                                "SCL parsing error",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            }
                            else
                            {
                                this.IReports.Add(name + "." + iedName + "." + ldName + "." + lnName, report);
                            }
                        }
                        catch (ArgumentException)
                        {
                            throw new ArgumentException("Duplicated report control block: " + name);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Find matching report using address.
        /// </summary>
        /// <param name="address">Report address.</param>
        /// <returns>The report name.</returns>
        public string FindReport(string address)
        {
            string ret = "";
            foreach (var rep in this.IReports)
            {
                if (rep.Value.Address.Equals(address))
                {
                    ret = rep.Key;
                    break;
                }
            }
            return ret;
        }
    }
}
