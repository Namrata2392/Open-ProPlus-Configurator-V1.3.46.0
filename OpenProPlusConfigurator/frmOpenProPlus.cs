//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Windows.Forms;
//using System.IO;
//using System.Collections;
//using System.Xml.Schema;
//using System.Xml;
//using System.IO.Compression;
//using System.Threading;
//using System.Net;
//using System.Diagnostics;
//using Microsoft.Win32;
//using System.Drawing.Drawing2D;
//using System.Reflection;
//using Renci.SshNet;
//using Renci.SshNet.Sftp;
//using System.Collections.Specialized;
//using System.Text;
//using System.Data.OleDb;
//using Excel = Microsoft.Office.Interop.Excel;
//using System.Windows.Forms;
//using ICSharpCode.SharpZipLib.Tar;
//using ICSharpCode.SharpZipLib.GZip;

//namespace OpenProPlusConfigurator
//{
//    public partial class frmOpenProPlus : Form
//    {
//        #region Declarations
//        OpenProPlus_Config opcHandle = null;
//        frmProgess fp = null; //Namrata: 12/01/2018
//        frmOverview fo = null;
//        private bool showExitMessage = false;
//        private string xmlFile = null;
//        private bool iec104EventHandled = false;
//        ucGroupIEC104 ucs104 = null;

//        private bool SMSEventHandled = false;
//        ucGroupSMSSlave ucSMS = null;
//        //Namrata:6/7/2017
//        ucGroupIEC101Slave ucs101 = null;
//        private bool iec101EventHandled = false;
//        public const int COLS_B4_MULTISLAVE = 13;
//        public const int TOTAL_MAP_PARAMS = 7;
//        public const int FILTER_PANEL_HEIGHT = 70;
//        private int sortColumn = -1;
//        ucRCBList ucRCB = null;
//        //Namrata: 06/09/2017
//        //View Recent File
//        private MruList MyMruList;
//        ucAIlist ucsai = null;
//        ucAOList ucsao = null;
//        ucDIlist ucsdi = null;
//        ucDOlist ucsdo = null;
//        ucENlist ucsen = null;
//        ucLPFilelist ucslpfile = null;
//        private bool IsXmlValid = false;
//        ucRCBList uc = null; //Namrata: 12/01/2018
//        List<RCB> aiList = new List<RCB>();
//        ucAIlist ucai = null;//Namrata: 12/01/2018
//        private AIConfiguration aicNodeforiec61850Client = null;
//        private AOConfiguration aocNodeforiec61850Client = null;
//        private DIConfiguration dicNodeforiec61850Client = null;
//        private DOConfiguration docNodeforiec61850Client = null;
//        private ENConfiguration encNodeforiec61850Client = null;
//        private RCBConfiguration RCBNodeforiec61850Client = null;
//        private ucDRConfig ucsDRConfig = null; //Ajay: 29/12/2018
//        private DRConfiguration drConfiguration = null; //Ajay: 29/12/2018
//        private bool VersionMatch = false; //Ajay: 11/01/2019
//        DataTable table1 = new DataTable("IEC61850");
//        string FolderNamewithoutexstension = "";
//        OpenFileDialog fd = new OpenFileDialog();
//        #endregion Declarations
//        public frmOpenProPlus()
//        {
//            InitializeComponent();
//        }
//        private void UpdateXMLFile(string FileName)
//        {
//            string strRoutineName = "frmOpenProPlus: UpdateXMLFile";
//            try
//            {
//                if (!string.IsNullOrEmpty(FileName))
//                {
//                    xmlFile = FileName;
//                    toolStripStatusLabel1.Visible = true;
//                    tspFileName.Text = FileName;
//                }
//                else
//                {
//                    toolStripStatusLabel1.Visible = true;
//                    xmlFile = tspFileName.Text = string.Empty;
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        //Ajay: 23/11/2018
//        private void ReadProtocolGatewayXML()
//        {
//            string strRoutineName = "frmOpenProPlus: ReadProtocolGatewayXML";
//            try
//            {
//                if (File.Exists(ProtocolGateway.ProtocolGatewayConfigurationFile))
//                {
//                    XmlDocument doc = new XmlDocument();
//                    doc.Load(ProtocolGateway.ProtocolGatewayConfigurationFile);
//                    if (doc != null)
//                    {
//                        XmlElement root = doc.DocumentElement;
//                        XmlNodeList xnl = root.ChildNodes;
//                        XmlAttribute xaVisible = null;
//                        XmlAttribute xaReadOnly = null;
//                        XmlNodeList xnlMainNode = null;

//                        xnlMainNode = root.SelectNodes("//Details");
//                        if (xnlMainNode.Count > 0)
//                        {
//                            xaVisible = xnlMainNode[0].Attributes["Visible"];
//                            if (xaVisible != null) ProtocolGateway.OppDetails_Visible = Convert.ToBoolean(xaVisible.Value);
//                            xaReadOnly = xnlMainNode[0].Attributes["ReadOnly"];
//                            if (xaReadOnly != null) ProtocolGateway.OppDetails_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
//                        }
//                        xnlMainNode = root.SelectNodes("//NetWorkConfiguration");
//                        if (xnlMainNode.Count > 0)
//                        {
//                            xaVisible = xnlMainNode[0].Attributes["Visible"];
//                            if (xaVisible != null) ProtocolGateway.OppNetWorkConfiguration_Visible = Convert.ToBoolean(xaVisible.Value);
//                            xaReadOnly = xnlMainNode[0].Attributes["ReadOnly"];
//                            if (xaReadOnly != null) ProtocolGateway.OppNetWorkConfiguration_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
//                        }
//                        xnlMainNode = root.SelectNodes("//SerialPortConfiguration");
//                        if (xnlMainNode.Count > 0)
//                        {
//                            xaVisible = xnlMainNode[0].Attributes["Visible"];
//                            if (xaVisible != null) ProtocolGateway.OppSerialPortConfiguration_Visible = Convert.ToBoolean(xaVisible.Value);
//                            xaReadOnly = xnlMainNode[0].Attributes["ReadOnly"];
//                            if (xaReadOnly != null) ProtocolGateway.OppSerialPortConfiguration_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
//                        }
//                        xnlMainNode = root.SelectNodes("//SystemConfiguration");
//                        if (xnlMainNode.Count > 0)
//                        {
//                            xaVisible = xnlMainNode[0].Attributes["Visible"];
//                            if (xaVisible != null) ProtocolGateway.OppSystemConfiguration_Visible = Convert.ToBoolean(xaVisible.Value);
//                            xaReadOnly = xnlMainNode[0].Attributes["ReadOnly"];
//                            if (xaReadOnly != null) ProtocolGateway.OppSystemConfiguration_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
//                        }
//                        xnlMainNode = root.SelectNodes("//SlaveConfiguration");
//                        if (xnlMainNode.Count > 0)
//                        {
//                            xaVisible = xnlMainNode[0].Attributes["Visible"];
//                            if (xaVisible != null) ProtocolGateway.OppSlaveConfiguration_Visible = Convert.ToBoolean(xaVisible.Value);
//                            xaReadOnly = xnlMainNode[0].Attributes["ReadOnly"];
//                            if (xaReadOnly != null) ProtocolGateway.OppSlaveConfiguration_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);

//                            xnlMainNode[0].ChildNodes.OfType<XmlNode>().ToList().ForEach(xn =>
//                            {
//                                if (xn.Name == "IEC104SlaveGroup")
//                                {
//                                    xaVisible = xn.Attributes["Visible"];
//                                    if (xaVisible != null) ProtocolGateway.OppIEC104SlaveGroup_Visible = Convert.ToBoolean(xaVisible.Value);
//                                    xaReadOnly = xn.Attributes["ReadOnly"];
//                                    if (xaReadOnly != null) ProtocolGateway.OppIEC104SlaveGroup_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
//                                }
//                                else if (xn.Name == "MODBUSSlaveGroup")
//                                {
//                                    xaVisible = xn.Attributes["Visible"];
//                                    if (xaVisible != null) ProtocolGateway.OppMODBUSSlaveGroup_Visible = Convert.ToBoolean(xaVisible.Value);
//                                    xaReadOnly = xn.Attributes["ReadOnly"];
//                                    if (xaReadOnly != null) ProtocolGateway.OppMODBUSSlaveGroup_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
//                                }
//                                else if (xn.Name == "IEC101SlaveGroup")
//                                {
//                                    xaVisible = xn.Attributes["Visible"];
//                                    if (xaVisible != null) ProtocolGateway.OppIEC101SlaveGroup_Visible = Convert.ToBoolean(xaVisible.Value);
//                                    xaReadOnly = xn.Attributes["ReadOnly"];
//                                    if (xaReadOnly != null) ProtocolGateway.OppIEC101SlaveGroup_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
//                                }
//                                else if (xn.Name == "IEC61850ServerGroup")
//                                {
//                                    xaVisible = xn.Attributes["Visible"];
//                                    if (xaVisible != null) ProtocolGateway.OppIEC61850SlaveGroup_Visible = Convert.ToBoolean(xaVisible.Value);
//                                    xaReadOnly = xn.Attributes["ReadOnly"];
//                                    if (xaReadOnly != null) ProtocolGateway.OppIEC61850SlaveGroup_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
//                                }
//                                else if (xn.Name == "SPORTSlaveGroup")
//                                {
//                                    xaVisible = xn.Attributes["Visible"];
//                                    if (xaVisible != null) ProtocolGateway.OppSPORTSlaveGroup_Visible = Convert.ToBoolean(xaVisible.Value);
//                                    xaReadOnly = xn.Attributes["ReadOnly"];
//                                    if (xaReadOnly != null) ProtocolGateway.OppSPORTSlaveGroup_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
//                                }
//                                //Namrata:02/04/2019
//                                else if (xn.Name == "MQTTSlaveGroup")
//                                {
//                                    xaVisible = xn.Attributes["Visible"];
//                                    if (xaVisible != null) ProtocolGateway.OppMQTTSlaveGroup_Visible = Convert.ToBoolean(xaVisible.Value);
//                                    xaReadOnly = xn.Attributes["ReadOnly"];
//                                    if (xaReadOnly != null) ProtocolGateway.OppMQTTSlaveGroup_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
//                                }
//                                //Namrata:25/05/2019
//                                else if (xn.Name == "SMSSlaveGroup")
//                                {
//                                    xaVisible = xn.Attributes["Visible"];
//                                    if (xaVisible != null) ProtocolGateway.OppSMSSlaveGroup_Visible = Convert.ToBoolean(xaVisible.Value);
//                                    xaReadOnly = xn.Attributes["ReadOnly"];
//                                    if (xaReadOnly != null) ProtocolGateway.OppSMSSlaveGroup_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
//                                }
//                                else if (xn.Name == "GraphicalDisplaySlaveGroup")
//                                {
//                                    xaVisible = xn.Attributes["Visible"];
//                                    if (xaVisible != null) ProtocolGateway.OppGraphicalDisplaySlaveGroup_Visible = Convert.ToBoolean(xaVisible.Value);
//                                    xaReadOnly = xn.Attributes["ReadOnly"];
//                                    if (xaReadOnly != null) ProtocolGateway.OppGraphicalDisplaySlaveGroup_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
//                                }
//                                else if (xn.Name == "DNP3SlaveGroup")
//                                {
//                                    xaVisible = xn.Attributes["Visible"];
//                                    if (xaVisible != null) ProtocolGateway.OppDNP3SlaveGroup_Visible = Convert.ToBoolean(xaVisible.Value);
//                                    xaReadOnly = xn.Attributes["ReadOnly"];
//                                    if (xaReadOnly != null) ProtocolGateway.OppDNP3SlaveGroup_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
//                                }
//                            });
//                        }
//                        else { }
//                        xnlMainNode = root.SelectNodes("//MasterConfiguration");
//                        if (xnlMainNode.Count > 0)
//                        {
//                            xaVisible = xnlMainNode[0].Attributes["Visible"];
//                            if (xaVisible != null) ProtocolGateway.OppMasterConfiguration_Visible = Convert.ToBoolean(xaVisible.Value);
//                            xaReadOnly = xnlMainNode[0].Attributes["ReadOnly"];
//                            ProtocolGateway.OppMasterConfiguration_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);

//                            xnlMainNode[0].ChildNodes.OfType<XmlNode>().ToList().ForEach(xn =>
//                            {
//                                if (xn.Name == "ADRGroup")
//                                {
//                                    xaVisible = xn.Attributes["Visible"];
//                                    if (xaVisible != null) ProtocolGateway.OppADRGroup_Visible = Convert.ToBoolean(xaVisible.Value);
//                                    xaReadOnly = xn.Attributes["ReadOnly"];
//                                    if (xaReadOnly != null) ProtocolGateway.OppADRGroup_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
//                                }
//                                else if (xn.Name == "IEC101Group")
//                                {
//                                    xaVisible = xn.Attributes["Visible"];
//                                    if (xaVisible != null) ProtocolGateway.OppIEC101Group_Visible = Convert.ToBoolean(xaVisible.Value);
//                                    xaReadOnly = xn.Attributes["ReadOnly"];
//                                    if (xaReadOnly != null) ProtocolGateway.OppIEC101Group_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
//                                }
//                                else if (xn.Name == "IEC103Group")
//                                {
//                                    xaVisible = xn.Attributes["Visible"];
//                                    if (xaVisible != null) ProtocolGateway.OppIEC103Group_Visible = Convert.ToBoolean(xaVisible.Value);
//                                    xaReadOnly = xn.Attributes["ReadOnly"];
//                                    if (xaReadOnly != null) ProtocolGateway.OppIEC103Group_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
//                                }
//                                else if (xn.Name == "MODBUSGroup")
//                                {
//                                    xaVisible = xn.Attributes["Visible"];
//                                    if (xaVisible != null) ProtocolGateway.OppMODBUSGroup_Visible = Convert.ToBoolean(xaVisible.Value);
//                                    xaReadOnly = xn.Attributes["ReadOnly"];
//                                    if (xaReadOnly != null) ProtocolGateway.OppMODBUSGroup_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
//                                }
//                                else if (xn.Name == "IEC61850ClientGroup")
//                                {
//                                    xaVisible = xn.Attributes["Visible"];
//                                    if (xaVisible != null) ProtocolGateway.OppIEC61850Group_Visible = Convert.ToBoolean(xaVisible.Value);
//                                    xaReadOnly = xn.Attributes["ReadOnly"];
//                                    if (xaReadOnly != null) ProtocolGateway.OppIEC61850Group_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
//                                }
//                                else if (xn.Name == "IEC104Group")
//                                {
//                                    xaVisible = xn.Attributes["Visible"];
//                                    if (xaVisible != null) ProtocolGateway.OppIEC104Group_Visible = Convert.ToBoolean(xaVisible.Value);
//                                    xaReadOnly = xn.Attributes["ReadOnly"];
//                                    if (xaReadOnly != null) ProtocolGateway.OppIEC104Group_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
//                                }
//                                else if (xn.Name == "SPORTGroup")
//                                {
//                                    xaVisible = xn.Attributes["Visible"];
//                                    if (xaVisible != null) ProtocolGateway.OppSPORTGroup_Visible = Convert.ToBoolean(xaVisible.Value);
//                                    xaReadOnly = xn.Attributes["ReadOnly"];
//                                    if (xaReadOnly != null) ProtocolGateway.OppSPORTGroup_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
//                                }
//                                else if (xn.Name == "VirtualGroup")
//                                {
//                                    xaVisible = xn.Attributes["Visible"];
//                                    if (xaVisible != null) ProtocolGateway.OppVirtualGroup_Visible = Convert.ToBoolean(xaVisible.Value);
//                                    xaReadOnly = xn.Attributes["ReadOnly"];
//                                    if (xaReadOnly != null) ProtocolGateway.OppVirtualGroup_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
//                                }
//                                else if (xn.Name == "LoadProfileGroup")
//                                {
//                                    xaVisible = xn.Attributes["Visible"];
//                                    if (xaVisible != null) ProtocolGateway.OppLoadProfileGroup_Visible = Convert.ToBoolean(xaVisible.Value);
//                                    xaReadOnly = xn.Attributes["ReadOnly"];
//                                    if (xaReadOnly != null) ProtocolGateway.OppLoadProfileGroup_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
//                                }

//                            });
//                        }
//                        else { }

//                        xnlMainNode = root.SelectNodes("//ParameterLoadConfiguration");
//                        if (xnlMainNode.Count > 0)
//                        {
//                            xaVisible = xnlMainNode[0].Attributes["Visible"];
//                            if (xaVisible != null) ProtocolGateway.OppParameterLoadConfiguration_Visible = Convert.ToBoolean(xaVisible.Value);
//                            xaReadOnly = xnlMainNode[0].Attributes["ReadOnly"];
//                            if (xaReadOnly != null) ProtocolGateway.OppParameterLoadConfiguration_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
//                        }
//                    }
//                    else { }
//                }
//                else { }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void frmParser_Load(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: frmParser_Load";
//            try
//            {
//                //Namrata: 25/11/2019
//                recentFilesToolStripMenuItem.Visible = false;
//                toolStripSeparator6.Visible = false;
//                ssParser.Hide();
//                //Ajay: 23/11/2018
//                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
//                {
//                    //Ajay: 09/01/2019
//                    openToolStripMenuItem.Enabled = false;
//                    newToolStripMenuItem.Enabled = false;
//                    recentFilesToolStripMenuItem.Enabled = false;
//                    tsbNew.Enabled = false;
//                    tsbOpen.Enabled = false;
//                    tsbtnFTPConfig.Visible = false;

//                    if (string.IsNullOrEmpty(ProtocolGateway.OppIpAddress) || string.IsNullOrEmpty(ProtocolGateway.User) || string.IsNullOrEmpty(ProtocolGateway.Password))
//                    {
//                        tsbtnFileDownload.Enabled = false;
//                        tsbtnFileUpload.Enabled = false;
//                    }
//                    if (!string.IsNullOrEmpty(ProtocolGateway.ProtocolGatewayConfigurationFile))
//                    {
//                        ReadProtocolGatewayXML();
//                        //Ajay: 14/12/2018
//                        Globals.ZONE_RESOURCES_PATH = Path.GetDirectoryName(ProtocolGateway.ProtocolGatewayConfigurationFile) + @"\" + "resources" + @"\";
//                        Globals.RESOURCES_PATH = Path.GetDirectoryName(ProtocolGateway.ProtocolGatewayConfigurationFile) + @"\" + "resources" + @"\";
//                    }
//                }
//                //Ajay: 07/12/2018
//                else if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Full)
//                {
//                    tsbtnFTPConfig.Visible = true;
//                    if (Properties.Settings.Default["OppIPAddress"] != null)
//                    {
//                        ProtocolGateway.OppIpAddress = Properties.Settings.Default["OppIPAddress"].ToString();
//                    }
//                    if (Properties.Settings.Default["FTPUser"] != null)
//                    {
//                        ProtocolGateway.User = Properties.Settings.Default["FTPUser"].ToString();
//                    }
//                    if (Properties.Settings.Default["FTPPassword"] != null)
//                    {
//                        ProtocolGateway.Password = Properties.Settings.Default["FTPPassword"].ToString();
//                    }
//                    if (Properties.Settings.Default["FTProtocol"] != null)
//                    {
//                        ProtocolGateway.Protocol = Properties.Settings.Default["FTProtocol"].ToString();
//                    }
//                }

//                //Namrata: 18/04/2018
//                //Delete Folder from AppData
//                ICDFilesData.ICDDirMFile = System.IO.Path.GetTempPath() + "protocol";
//                if (System.IO.Directory.Exists(ICDFilesData.ICDDirMFile))
//                {
//                    string DirectoryNameDelete = System.IO.Path.GetTempPath() + "protocol"; /*System.IO.Path.GetTempPath() + @"\" + "IEC61850_Client";*/
//                    FileDirectoryOperations fileDirectoryOperations = new FileDirectoryOperations();
//                    FileDirectoryOperations.DeleteDirectory(DirectoryNameDelete);
//                }
//                //Namrata: 23/04/2018
//                //Delete Folder from ProgramData
//                string DPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName;
//                if (DPath != "")
//                {
//                    if (ofdXMLFile.FileName != "")
//                    {
//                        string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(ofdXMLFile.FileName) + "\\" + "Protocol" + "\\" + "IEC61850Client";/* Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(ofdXMLFile.FileName) + "\\" + "IEC61850_Client" + "\\" + "ProtocolConfiguration";*/
//                        string ImportedDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(Utils.DirectoryPath);// 
//                        if (System.IO.Directory.Exists(path))
//                        {
//                            FileDirectoryOperations fileDirectoryOperation = new FileDirectoryOperations();
//                            FileDirectoryOperations.DeleteDirectory(ImportedDirectoryPath);
//                        }
//                    }
//                    else
//                    {
//                        FileDirectoryOperations fileDirectoryOperation = new FileDirectoryOperations();
//                        FileDirectoryOperations.DeleteDirectory(DPath);
//                    }
//                }
//                fp = new frmProgess();
//                uc = new ucRCBList();
//                ucai = new ucAIlist();
//                toolStripStatusLabel1.Visible = false;
//                lvValidationMessages.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
//                lvValidationMessages.Scrollable = true;
//                Utils.SetFormIcon(this);
//                //Namrata: 31/08/2017
//                //Check if Zone file exists...
//                if (!File.Exists(Globals.ZONE_RESOURCES_PATH + Globals.TIME_ZONE_LIST))
//                {
//                    MessageBox.Show("Zone file (" + Globals.ZONE_RESOURCES_PATH + Globals.TIME_ZONE_LIST + ") is missing. Exiting application!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    this.Close();
//                    return;
//                }
//                #region XSD Validations
//                //Check if XSD file exists...
//                if (!File.Exists(Globals.RESOURCES_PATH + Globals.XSD_FILENAME))
//                {
//                    MessageBox.Show("Schema file (" + Globals.RESOURCES_PATH + Globals.XSD_FILENAME + ") is missing. Exiting application!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    this.Close();
//                    return;
//                }
//                if (!File.Exists(Globals.RESOURCES_PATH + Globals.XSD_DATATYPE_FILENAME))
//                {
//                    MessageBox.Show("Schema file (" + Globals.RESOURCES_PATH + Globals.XSD_DATATYPE_FILENAME + ") is missing. Exiting application!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    this.Close();
//                    return;
//                }

//                if (!Utils.IsXMLWellFormed(Globals.RESOURCES_PATH + Globals.XSD_FILENAME))
//                {
//                    MessageBox.Show("Schema file (" + Globals.RESOURCES_PATH + Globals.XSD_FILENAME + ") is not a valid XML. Exiting application!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    this.Close();
//                    return;
//                }
//                if (!Utils.IsXMLWellFormed(Globals.RESOURCES_PATH + Globals.XSD_DATATYPE_FILENAME))
//                {
//                    MessageBox.Show("Schema file (" + Globals.RESOURCES_PATH + Globals.XSD_DATATYPE_FILENAME + ") is not a valid XML. Exiting application!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    this.Close();
//                    return;
//                }
//                #endregion XSD Validations
//                showExitMessage = true;
//                lvValidationMessages.Columns.Add("Validation Messages...", 1000, HorizontalAlignment.Left);
//                ResetConfiguratorState(true);
//                HandleMapViewChange();
//                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Full) //Ajay: 09/01/2019
//                {
//                    //Namrata: 28/12/2017
//                    MyMruList = new MruList(Application.ProductName, this.recentFilesToolStripMenuItem, 10, this.myOwnRecentFilesGotCleared_handler);
//                    MyMruList.FileSelected += MyMruList_FileSelected;
//                }
//                //Namrata: 19/12/2017
//                //Ajay: 23/11/2018
//                //TreeNode searchNode = tvItems.Nodes[0].Nodes[5];
//                TreeNode searchNode = tvItems.Nodes[0].Nodes["MasterConfiguration"];
//                if (searchNode != null)
//                    searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });

//                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
//                {
//                    //EditNwConfigurationInXMLFile(); //Ajay: 08/01/2018

//                     openXMLFile();

//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void myOwnRecentFilesGotCleared_handler(object obj, EventArgs evt)
//        {
//        }
//        private void MyMruList_FileSelected(string file_name)
//        {
//            string strRoutineName = "frmOpenProPlus: MyMruList_FileSelected";
//            try
//            {
//                //Ajay: 11/01/2019
//                if (!CheckVersions(file_name))
//                {
//                    VersionMatch = false;
//                    DialogResult rslt = MessageBox.Show("Do you still want to continue with mismatched versions of OpenPro+Configurator, xml and xsd files?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
//                    if (rslt == DialogResult.No)
//                    {
//                        return;
//                    }
//                    else { }
//                }
//                else { VersionMatch = true; }

//                OpenFile(file_name);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void OpenFile(string file_name)
//        {
//            string strRoutineName = "frmOpenProPlus: OpenFile";
//            try
//            {
//                lvValidationMessages.Items.Clear(); //Ajay: 06/10/2018
//                RichTextBox RichtextBox = new RichTextBox();
//                // Load the file.
//                RichtextBox.Clear();
//                if (file_name.ToLower().EndsWith(".rtf"))
//                {
//                    RichtextBox.LoadFile(file_name);
//                }
//                else
//                {
//                    #region ClearDatasets
//                    Utils.DsRCB.Clear();
//                    Utils.DsRCBData.Clear();
//                    Utils.dsResponseType.Clear();
//                    Utils.DsResponseType.Clear();
//                    Utils.dsIED.Clear();
//                    Utils.dsIEDName.Clear();
//                    Utils.DsAllConfigurationData.Clear();
//                    Utils.DsAllConfigureData.Clear();
//                    Utils.DsRCBDataset.Clear();
//                    Utils.DsRCBDS.Clear();
//                    Utils.DtRCBdata.Clear();
//                    #endregion ClearDatasets

//                    RichtextBox.Text = File.ReadAllText(file_name);
//                    Utils.XMLFolderPath = Path.GetDirectoryName(file_name);
//                    int result = 0;
//                    string errMsg = "XML file is valid...";
//                    ResetConfiguratorState(false);
//                    showLoading();

//                    xmlFile = "";//reset old filename...
//                    ListViewItem lvi;
//                    lvi = new ListViewItem("Validating file: " + file_name);
//                    //Namrata: 27/7/2017
//                    toolStripStatusLabel1.Visible = true;  //Display File Name in Toolstrip 
//                    tspFileName.Text = file_name;
//                    lvValidationMessages.Items.Add(lvi);
//                    lvi = new ListViewItem("");
//                    lvValidationMessages.Items.Add(lvi);
//                    result = opcHandle.loadXML(file_name, tvItems, out IsXmlValid);
//                    if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
//                    else { pnlValidationMessages.Visible = true; pnlValidationMessages.BringToFront(); }
//                    if (result == -1) errMsg = "File doesnot exist!!!";
//                    else if (result == -2) errMsg = "File is not a well-formed XML!!!";
//                    else if (result == -3) errMsg = "XSD file is not valid!!!";
//                    else if (result == -4) errMsg = "File is not a valid XML, as per the schema!!!";
//                    lvi = new ListViewItem("");
//                    lvValidationMessages.Items.Add(lvi);
//                    lvi = new ListViewItem(errMsg);
//                    lvValidationMessages.Items.Add(lvi);
//                    lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
//                    if (result < 0)
//                    {
//                        hideLoading();
//                        MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        return;
//                    }
//                    //Ajay: 10/01/2019
//                    //xmlFile = ofdXMLFile.FileName;//Assign only after loading file...
//                    if (!string.IsNullOrEmpty(ofdXMLFile.FileName) && File.Exists(ofdXMLFile.FileName)) //Ajay: 10/01/2019
//                    {
//                        xmlFile = ofdXMLFile.FileName;//Assign only after loading file...
//                    }
//                    //Ajay: 10/01/2019
//                    else
//                    {
//                        if (!string.IsNullOrEmpty(file_name) && File.Exists(file_name))
//                        {
//                            xmlFile = file_name;
//                        }
//                    }

//                    tvItems.SelectedNode = tvItems.Nodes[0];
//                    //Namrata: 05/01/2018
//                    #region Treeview Collapse Common Node
//                    //Ajay: 29/11/2018
//                    //TreeNode searchNode = tvItems.Nodes[0].Nodes[5];
//                    TreeNode searchNode = tvItems.Nodes[0].Nodes["MasterConfiguration"];
//                    if (searchNode != null)
//                        searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
//                    #endregion Treeview Collapse Common Node
//                    tvItems.Nodes[0].EnsureVisible();
//                    hideLoading();
//                }
//                MyMruList.AddFile(file_name); // Add the file to the MRU list.
//            }
//            catch (Exception ex)
//            {
//                MyMruList.RemoveFile(file_name);// Remove the file from the MRU list.
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void ValidationCallBack(object sender, oppcValidationMessages e)
//        {
//            string strRoutineName = "frmOpenProPlus: ValidationCallBack";
//            try
//            {
//                if (e.Severity == XmlSeverityType.Warning)
//                {
//                    ListViewItem lvi = new ListViewItem("Warning: Matching schema not found.  No validation occurred." + e.Message);
//                    lvValidationMessages.Items.Add(lvi);
//                }
//                else
//                {
//                    ListViewItem lvi = new ListViewItem("Validation error Line No. " + e.LineNo + ": " + e.Message);
//                    lvValidationMessages.Items.Add(lvi);
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        public OpenProPlus_Config getOpenProPlusHandle()
//        {
//            return opcHandle;
//        }
//        private void handleAbout()
//        {
//            string strRoutineName = "frmOpenProPlus: handleAbout";
//            try
//            {
//                frmAbout fa = new frmAbout();
//                fa.ShowDialog();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: aboutToolStripMenuItem_Click";
//            try
//            {
//                handleAbout();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void tsbAbout_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: tsbAbout_Click";
//            try
//            {
//                handleAbout();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void toolbarToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: toolbarToolStripMenuItem_Click";
//            try
//            {
//                if (toolbarToolStripMenuItem.Checked) tsParser.Visible = true;
//                else tsParser.Visible = false;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
//        {

//        }
//        private void tvItems_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: tvItems_NodeMouseClick";
//            try
//            {
//                #region IEC61850Client
//                if (Utils.IEC61850ClientMList.Count > 0 && e.Node.FullPath.Contains("IEC61850 Group") && (e.Node.Level == 3))
//                {
//                    if (e.Node.Index <= Utils.IEC61850ClientMList.Count - 1)
//                    {

//                        Utils.MasterNumForIEC61850Client = Utils.IEC61850ClientMList[e.Node.Index].MasterNum.ToString();
//                    }
//                }
//                if (Utils.IEC61850ClientMList.Count > 0 && e.Node.FullPath.Contains("IEC61850 Group") && (e.Node.Level == 4))
//                {
//                    if (e.Node.Parent.Index <= Utils.IEC61850ClientMList.Count - 1)
//                    {
//                        Utils.MasterNumForIEC61850Client = Utils.IEC61850ClientMList[e.Node.Parent.Index].MasterNum.ToString();
//                    }
//                }
//                else if (Utils.IEC61850ClientMList.Count > 0 && e.Node.FullPath.Contains("IEC61850 Group") && (e.Node.Level == 5))
//                {
//                    if (e.Node.Parent.Index <= Utils.IEC61850ClientMList.Count - 1)
//                    {
//                        Utils.MasterNumForIEC61850Client = Utils.IEC61850ClientMList[e.Node.Parent.Parent.Index].MasterNum.ToString();
//                    }
//                }
//                if (Utils.IEC61850MasteriedListGetIEDNo.Count > 0 && e.Node.FullPath.Contains("IEC61850 Group") && (e.Node.Level == 4))
//                {
//                    if (e.Node.Parent.Index <= Utils.IEC61850MasteriedListGetIEDNo.Count - 1)
//                    {
//                        Utils.UnitIDForIEC61850Client = Utils.IEC61850MasteriedListGetIEDNo[e.Node.Parent.Index].UnitID.ToString();
//                    }
//                }
//                else if (Utils.IEC61850MasteriedListGetIEDNo.Count > 0 && e.Node.FullPath.Contains("IEC61850 Group") && (e.Node.Level == 5))
//                {
//                    Utils.UnitIDForIEC61850Client = Utils.IEC61850MasteriedListGetIEDNo[e.Node.Parent.Index].UnitID.ToString();
//                }
//                #endregion IEC61850Client


//                //Namrata: 26/10/2019
//                if (e.Node.Parent == null)
//                {

//                }
//                else
//                {
//                    if (e.Node.Parent.Text == "IEC61850 Group")
//                    {
//                        Utils.strFrmOpenproplusTreeNode = e.Node.Text;
//                        Utils.strFrmOpenproplusIEdname = "";
//                    }
//                }

//                if (e.Node.Parent != null)
//                {
//                    if (e.Node.Parent.Parent != null)
//                    {
//                        if (e.Node.Parent.Parent.Text != null)
//                        {
//                            if (e.Node.Parent.Parent.Text == "IEC61850 Group")
//                            {
//                                if (e.Button == MouseButtons.Right)
//                                {
//                                    if (e.Node.Text.Contains("IED "))
//                                    {
//                                        Utils.SCLFileName = Utils.IEC61850MasteriedListGetIEDNo[e.Node.Index].SCLName.ToString();
//                                        ContextMenuStrip cm = opcHandle.GetToolstripMenu();
//                                        if (cm != null)
//                                        {
//                                            Point pt = new Point(e.X, e.Y);
//                                            cm.Show(tvItems, pt);
//                                        }
//                                    }
//                                }
//                            }
//                        }
//                        else
//                        {
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void tvItems_NodeMouseClick1(object sender, TreeNodeMouseClickEventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: tvItems_NodeMouseClick";
//            try
//            {
//                #region IEC61850Client
//                if (Utils.IEC61850ClientMList.Count > 0 && e.Node.FullPath.Contains("IEC61850 Group") && (e.Node.Level == 3))
//                {
//                    if (e.Node.Index <= Utils.IEC61850ClientMList.Count - 1)
//                    {

//                        Utils.MasterNumForIEC61850Client = Utils.IEC61850ClientMList[e.Node.Index].MasterNum.ToString();
//                    }
//                }
//                if (Utils.IEC61850ClientMList.Count > 0 && e.Node.FullPath.Contains("IEC61850 Group") && (e.Node.Level == 4))
//                {
//                    if (e.Node.Parent.Index <= Utils.IEC61850ClientMList.Count - 1)
//                    {
//                        Utils.MasterNumForIEC61850Client = Utils.IEC61850ClientMList[e.Node.Parent.Index].MasterNum.ToString();
//                    }
//                }
//                else if (Utils.IEC61850ClientMList.Count > 0 && e.Node.FullPath.Contains("IEC61850 Group") && (e.Node.Level == 5))
//                {
//                    if (e.Node.Parent.Index <= Utils.IEC61850ClientMList.Count - 1)
//                    {
//                        Utils.MasterNumForIEC61850Client = Utils.IEC61850ClientMList[e.Node.Parent.Parent.Index].MasterNum.ToString();
//                    }
//                }
//                if (Utils.IEC61850MasteriedListGetIEDNo.Count > 0 && e.Node.FullPath.Contains("IEC61850 Group") && (e.Node.Level == 4))
//                {
//                    if (e.Node.Parent.Index <= Utils.IEC61850MasteriedListGetIEDNo.Count - 1)
//                    {
//                        Utils.UnitIDForIEC61850Client = Utils.IEC61850MasteriedListGetIEDNo[e.Node.Parent.Index].UnitID.ToString();
//                    }
//                }
//                else if (Utils.IEC61850MasteriedListGetIEDNo.Count > 0 && e.Node.FullPath.Contains("IEC61850 Group") && (e.Node.Level == 5))
//                {
//                    Utils.UnitIDForIEC61850Client = Utils.IEC61850MasteriedListGetIEDNo[e.Node.Parent.Index].UnitID.ToString();
//                }
//                #endregion IEC61850Client


//                //Namrata: 26/10/2019
//                if (e.Node.Parent == null)
//                {

//                }



//                //if (e.Node.Parent.Parent == null)
//                //{

//                //}
//                else if (e.Node.Parent != null)
//                {
//                    if (e.Node.Parent.Text == "IEC61850 Group")
//                    {
//                        Utils.strFrmOpenproplusTreeNode = e.Node.Text;
//                        Utils.strFrmOpenproplusIEdname = "";
//                    }
//                }
//                //Namrata: 15/7/2017 
//                if (e.Button == MouseButtons.Right)
//                {
//                    if (e.Node.Text == "Master Configuration")
//                    {
//                        ContextMenuStrip cm = opcHandle.getContextMenu();
//                        if (cm != null)
//                        {
//                            Point pt = new Point(e.X, e.Y);
//                            cm.Show(tvItems, pt);
//                        }
//                    }
//                }
//                //if (e.Node.Parent.Parent == null)
//                //{

//                //}
//                else if (e.Node.Parent.Parent.Text != null)
//                {
//                    if (e.Node.Parent.Parent.Text == "IEC61850 Group")
//                    {
//                        if (e.Button == MouseButtons.Right)
//                        {
//                            if (e.Node.Text.Contains("IED "))
//                            {
//                                Utils.SCLFileName = Utils.IEC61850MasteriedListGetIEDNo[e.Node.Index].SCLName.ToString();
//                                ContextMenuStrip cm = opcHandle.GetToolstripMenu();
//                                if (cm != null)
//                                {
//                                    Point pt = new Point(e.X, e.Y);
//                                    cm.Show(tvItems, pt);
//                                }
//                            }
//                        }
//                    }
//                }
//                else
//                {
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void tvItems_AfterSelect(object sender, TreeViewEventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: tvItems_AfterSelect";
//            try
//            {
//                try
//                {
//                    try
//                    {

//                    }
//                    catch (System.NullReferenceException)
//                    {

//                    }
//                    this.scParser.Panel2.Controls.Clear();//Remove all previous controls...
//                                                          //Namrata:10/06/2019
//                    if (e.Node.Parent != null)
//                    {
//                        if (e.Node.Parent.Text == "SMS Group")
//                        {
//                            string tblName = e.Node.Text.ToString();
//                            if (tblName != null)
//                            {
//                                string[] tokens = tblName.Split('_');
//                                Utils.SMSSlaveNo = tokens[1];  //tokens[1];
//                            }
//                        }
//                    }
//                    //Namrata:10/06/2019
//                    if (e.Node.Parent != null)
//                    {
//                        if (e.Node.Parent.Text == "GraphicalDisplay Group")
//                        {
//                            string tblName = e.Node.Text.ToString();
//                            string CurrentSlave = e.Node.Text.ToString();
//                            if (tblName != null)
//                            {
//                                string[] tokens = tblName.Split('_');
//                                GDSlave.GDSlaveNo = tokens[1];  //tokens[1];
//                            }
//                            if (CurrentSlave != null)
//                            {
//                                string[] tokens = tblName.Split(' ');
//                                GDSlave.CurSlave = tokens[1];  //tokens[1];
//                            }
//                        }
//                    }
//                    Control ucrp = opcHandle.getView(Utils.getKeyPathArray(e.Node));

//                    #region LPFILE
//                    //Ajay: 10/09/2018
//                    if (e.Node.Text == "LPFILE")
//                    {
//                        if (ucrp is ucLPFilelist)
//                        {
//                            ucslpfile = (ucLPFilelist)ucrp;
//                            ucslpfile.ucLPFilelist_Load(null, null);
//                            if (e.Node.Parent.Parent.Parent.Text == "IEC61850 Group")
//                            {
//                                Utils.strFrmOpenproplusTreeNode = e.Node.Parent.Parent.Text;
//                                //Namrata: 04/04/2018
//                                List<string> tblNameList = Utils.dsIED.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
//                                string tblName = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
//                                if (tblName != null)
//                                {
//                                    string[] tokens = tblName.Split('_');
//                                    Utils.Iec61850IEDname = tokens[3];
//                                }
//                                aicNodeforiec61850Client = new AIConfiguration(MasterTypes.IEC61850Client, Convert.ToInt32(Utils.MasterNumForIEC61850Client), Convert.ToInt32(Utils.UnitIDForIEC61850Client));
//                            }
//                        }
//                    }
//                    #endregion LPFILE

//                    #region AI
//                    //Namrata: 26/10/2017
//                    if (e.Node.Text == "AI")
//                    {
//                        if (ucrp is ucAIlist)
//                        {
//                            ucsai = (ucAIlist)ucrp;
//                            ucsai.ucAIlist_Load(null, null);
//                            if (e.Node.Parent.Parent.Parent.Text == "IEC61850 Group")
//                            {
//                                Utils.strFrmOpenproplusTreeNode = e.Node.Parent.Parent.Text;
//                                //Namrata: 04/04/2018
//                                List<string> tblNameList = Utils.dsIED.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
//                                string tblName = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
//                                if (tblName != null)
//                                {
//                                    string[] tokens = tblName.Split('_');
//                                    Utils.Iec61850IEDname = tokens[3];
//                                }

//                                aicNodeforiec61850Client = new AIConfiguration(MasterTypes.IEC61850Client, Convert.ToInt32(Utils.MasterNumForIEC61850Client), Convert.ToInt32(Utils.UnitIDForIEC61850Client));
//                            }
//                        }
//                    }
//                    #endregion AI

//                    #region AO
//                    //Namrata: 26/10/2017
//                    if (e.Node.Text == "AO")
//                    {
//                        if (ucrp is ucAOList)
//                        {
//                            ucsao = (ucAOList)ucrp;
//                            //ucsao.ucAIlist_Load(null, null);
//                            if (e.Node.Parent.Parent.Parent.Text == "IEC61850 Group")
//                            {
//                                Utils.strFrmOpenproplusTreeNode = e.Node.Parent.Parent.Text;
//                                //Namrata: 04/04/2018
//                                List<string> tblNameList = Utils.dsIED.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
//                                string tblName = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
//                                if (tblName != null)
//                                {
//                                    string[] tokens = tblName.Split('_');
//                                    Utils.Iec61850IEDname = tokens[3];
//                                }
//                                aocNodeforiec61850Client = new AOConfiguration(MasterTypes.IEC61850Client, Convert.ToInt32(Utils.MasterNumForIEC61850Client), Convert.ToInt32(Utils.UnitIDForIEC61850Client));
//                            }
//                        }
//                    }
//                    #endregion AO

//                    #region DI
//                    //Namrata: 26/10/2017
//                    if (e.Node.Text == "DI")
//                    {
//                        if (ucrp is ucDIlist)
//                        {
//                            ucsdi = (ucDIlist)ucrp;
//                            ucsdi.ucDIlist_Load(null, null);
//                            if (e.Node.Parent.Parent.Parent.Text == "IEC61850 Group")
//                            {
//                                Utils.strFrmOpenproplusTreeNode = e.Node.Parent.Parent.Text;
//                                //Namrata: 04/04/2018
//                                List<string> tblNameList = Utils.dsIED.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
//                                string tblName = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
//                                if (tblName != null)
//                                {
//                                    string[] tokens = tblName.Split('_');
//                                    Utils.Iec61850IEDname = tokens[3];
//                                }
//                                dicNodeforiec61850Client = new DIConfiguration(MasterTypes.IEC61850Client, Convert.ToInt32(Utils.MasterNumForIEC61850Client), Convert.ToInt32(Utils.UnitIDForIEC61850Client));
//                            }
//                        }
//                    }
//                    #endregion DI

//                    #region DO
//                    //Namrata: 26/10/2017
//                    if (e.Node.Text == "DO")
//                    {
//                        if (ucrp is ucDOlist)
//                        {
//                            ucsdo = (ucDOlist)ucrp;
//                            if (e.Node.Parent.Parent.Parent.Text == "IEC61850 Group")
//                            {
//                                Utils.strFrmOpenproplusTreeNode = e.Node.Parent.Parent.Text;
//                                //Namrata: 04/04/2018
//                                List<string> tblNameList = Utils.dsIED.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
//                                string tblName = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
//                                if (tblName != null)
//                                {
//                                    string[] tokens = tblName.Split('_');
//                                    Utils.Iec61850IEDname = tokens[3];
//                                }
//                                docNodeforiec61850Client = new DOConfiguration(MasterTypes.IEC61850Client, Convert.ToInt32(Utils.MasterNumForIEC61850Client), Convert.ToInt32(Utils.UnitIDForIEC61850Client));
//                            }
//                        }
//                    }
//                    #endregion DO

//                    #region EN
//                    //Namrata: 26/10/2017
//                    if (e.Node.Text == "EN")
//                    {
//                        if (ucrp is ucENlist)
//                        {
//                            ucsen = (ucENlist)ucrp;
//                            if (e.Node.Parent.Parent.Parent.Text == "IEC61850 Group")
//                            {
//                                Utils.strFrmOpenproplusTreeNode = e.Node.Parent.Parent.Text;
//                                //Namrata: 04/04/2018
//                                List<string> tblNameList = Utils.dsIED.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
//                                string tblName = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
//                                if (tblName != null)
//                                {
//                                    string[] tokens = tblName.Split('_');
//                                    Utils.Iec61850IEDname = tokens[3];
//                                }
//                                encNodeforiec61850Client = new ENConfiguration(MasterTypes.IEC61850Client, Convert.ToInt32(Utils.MasterNumForIEC61850Client), Convert.ToInt32(Utils.UnitIDForIEC61850Client));
//                            }
//                        }
//                    }

//                    #endregion EN

//                    #region ucGroupIEC104
//                    if (ucrp is ucGroupIEC104)
//                    {
//                        iec104EventHandled = false; //Ajay: 10/01/2018
//                        if (!iec104EventHandled)
//                        {
//                            ucs104 = (ucGroupIEC104)ucrp;
//                            Console.WriteLine("***### boom handle event for 104 slave INI export");
//                            ((ucGroupIEC104)ucrp).btnExportINIClick += new System.EventHandler(this.btnExportINI_Click);
//                            iec104EventHandled = true;
//                        }
//                    }
//                    #endregion ucGroupIEC104

//                    //Namrata: 22/11/2019
//                    #region ucGroupIEC104

//        //private bool SMSEventHandled = false;
//        //ucGroupSMSSlave ucSMS = null;
//                    if (ucrp is ucGroupSMSSlave)
//                    {
//                        SMSEventHandled = false; //Ajay: 10/01/2018
//                        if (!iec104EventHandled)
//                        {
//                            ucSMS = (ucGroupSMSSlave)ucrp;
//                            Console.WriteLine("***### boom handle event for 104 slave INI export");
//                            // ((ucGroupIEC104)ucrp).btnExportINIClick += new System.EventHandler(this.btnExportINI_Click);
//                            SMSEventHandled = true;
//                        }
//                    }
//                    #endregion ucGroupIEC104




//                    #region ucGroupIEC101Slave
//                    //Namrata:3/7/2017
//                    else if (ucrp is ucGroupIEC101Slave)
//                    {
//                        //iec101EventHandled = false; //Ajay: 10/01/2018
//                        if (!iec101EventHandled)
//                        {
//                            ucs101 = (ucGroupIEC101Slave)ucrp;
//                            Console.WriteLine("***### boom handle event for 101 slave INI export");
//                            ((ucGroupIEC101Slave)ucrp).btnexportIEC101INIClick += new System.EventHandler(this.btnExportINIIEC101_Click);
//                            iec101EventHandled = true;
//                        }
//                    }
//                    #endregion ucGroupIEC101Slave

//                    #region RCB
//                    //Namrata: 26/09/2017
//                    if (e.Node.Text == "RCB")
//                    {
//                        if (ucrp is ucRCBList)
//                        {
//                            //Namrata: 21/03/2018
//                            ucRCB = (ucRCBList)ucrp;
//                            ucRCB.ucRCBList_Load(null, null);
//                            if (e.Node.Parent.Parent.Parent.Text == "IEC61850 Group")
//                            {
//                                Utils.strFrmOpenproplusTreeNode = e.Node.Parent.Parent.Text;
//                                RCBNodeforiec61850Client = new RCBConfiguration(MasterTypes.IEC61850Client, Convert.ToInt32(Utils.MasterNumForIEC61850Client), Convert.ToInt32(Utils.UnitIDForIEC61850Client));
//                                RCBNodeforiec61850Client.FillRCBList();
//                            }
//                        }
//                    }
//                    #endregion RCB
//                    this.scParser.Panel2.Controls.Add(ucrp);
//                }
//                catch (System.NullReferenceException)
//                {
//                    Console.WriteLine("*** NullReferenceException handled...");
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void btnExportINIIEC101_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: btnExportINIIEC101_Click";
//            try
//            {
//                // Namrata: Commented on 28/01/2019//if (ucs101.INIExported) return; //Ajay: 10/01/2019
//                if (ucs101 == null) return;
//                if (ucs101.lvIEC101Slave.CheckedItems.Count != 1)
//                {
//                    MessageBox.Show("Select single slave for INI export!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    return;
//                }
//                if (xmlFile == null || xmlFile == "")
//                {
//                    MessageBox.Show("Save file before INI export!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    return;
//                }
//                ListViewItem lvi1 = ucs101.lvIEC101Slave.CheckedItems[0];
//                saveINIFile(lvi1.Text, "IEC101Slave_" + lvi1.Text); //saveINIFile(lvi1.Text, "IEC101_" + lvi1.Text);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }

//        }
//        private void btnExportINI_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: btnExportINI_Click";
//            try
//            {
//                if (ucs104.INIExported) return; //Ajay: 10/01/2019
//                if (ucs104 == null) return;
//                if (ucs104.lvIEC104Slave.CheckedItems.Count != 1)
//                {
//                    MessageBox.Show("Select Single Slave For INI Export!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    return;
//                }
//                if (xmlFile == null || xmlFile == "")
//                {
//                    MessageBox.Show("Save File Before INI Export!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    return;
//                }
//                ListViewItem lvi = ucs104.lvIEC104Slave.CheckedItems[0];
//                saveINIFile(lvi.Text, "IEC104_" + lvi.Text);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void saveINIFile(string slaveNum, string slaveID)
//        {
//            string strRoutineName = "frmOpenProPlus: saveINIFile";
//            try
//            {
//                Console.WriteLine("*** INI file for: {0} {1}", slaveNum, slaveID);
//                sfdXMLFile.Filter = "INI Files|*.ini";
//                if (sfdXMLFile.ShowDialog() == DialogResult.OK)
//                {
//                    Console.WriteLine("*** Saving to file: {0}", sfdXMLFile.FileName);

//                    writeINIFile(sfdXMLFile.FileName, slaveNum, slaveID);
//                    //Ajay: 21/11/2017 Show message box with file path  "\"" + sfdXMLFile.FileName + "\"
//                    MessageBox.Show("\"" + sfdXMLFile.FileName + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void writeINIFile(string fName, string slaveNum, string slaveID)
//        {
//            string strRoutineName = "frmOpenProPlus: writeINIFile";
//            try
//            {
//                File.WriteAllText(fName, opcHandle.getINIData(xmlFile, slaveNum, slaveID));
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void saveXMLFile()
//        {
//            string strRoutineName = "frmOpenProPlus: saveXMLFile";
//            try
//            {
//                if (xmlFile == null || xmlFile == "")
//                {
//                    sfdXMLFile.Filter = "XML Files|*.xml";
//                    sfdXMLFile.Title = "Save XML File";
//                    if (sfdXMLFile.ShowDialog() == DialogResult.OK)
//                    {

//                        xmlFile = sfdXMLFile.FileName;
//                        {
//                            Utils.XMLNameWOExt = Path.GetFileNameWithoutExtension(xmlFile);
//                           string dir= Path.GetDirectoryName(xmlFile);
//                            Utils.DirNameSave = dir + @"\" + Utils.XMLNameWOExt;
//                            //Namrata: 21/03/2018
//                            ICDFilesData.DirectoryName = Path.GetDirectoryName(xmlFile); //Get DirectoryName
//                            ICDFilesData.XmlName = Path.GetFileName(xmlFile); //Get XML Name
//                            ICDFilesData.XmlPath = xmlFile;//XML with full path
//                            ICDFilesData.XmlWithoutExt = Path.GetFileNameWithoutExtension(ICDFilesData.XmlPath);

//                            #region SLDFiles
//                            //if (GDSlave.GDisplaySlave)
//                            //{
//                            //    //Namrata: 16/08/2019
//                            GDSlave.DirName = Path.GetDirectoryName(xmlFile); //Get DirectoryName
//                            GDSlave.SLDName = Path.GetFileName(xmlFile); //Get XML Name
//                            GDSlave.SLDPath = xmlFile;//XML with full path
//                            GDSlave.SLDWithoutExt = Path.GetFileNameWithoutExtension(GDSlave.SLDPath);
//                            //}
//                            #endregion SLDFiles
//                            writeXMLFile(xmlFile);
//                            UpdateXMLFile(xmlFile);
//                            if ((Utils.IEC61850ClientMList.Count > 0) || (Utils.IEC61850ServerSList.Count > 0))//Namrata:08/04/2019
//                            {
//                                MessageBox.Show("\"" + ICDFilesData.IEC61850Xmlname + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

//                            }
//                            else
//                            {
//                                MessageBox.Show("\"" + xmlFile + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
//                            }
//                        }
//                    }
//                }
//                else
//                {
//                    Utils.XMLNameWOExt = Path.GetFileNameWithoutExtension(Utils.DirNameSave);
//                    #region ICDFiles
//                    //Namrata: 21/03/2018
//                    ICDFilesData.DirectoryName = Path.GetDirectoryName(Utils.DirNameSave); //Get DirectoryName
//                    ICDFilesData.XmlName = Path.GetFileName(Utils.DirNameSave); //Get XML Name
//                    ICDFilesData.XmlPath = xmlFile;// Utils.DirNameSave;//XML with full path
//                    ICDFilesData.XmlWithoutExt = Path.GetFileNameWithoutExtension(Utils.DirNameSave);
//                    #endregion ICDFiles

//                    GDSlave.DirName = Path.GetDirectoryName(Utils.DirNameSave); //Get DirectoryName
//                    GDSlave.SLDName = Path.GetFileName(Utils.DirNameSave); //Get XML Name
//                    GDSlave.SLDPath = Utils.DirNameSave;//XML with full path
//                    GDSlave.SLDWithoutExt = Path.GetFileNameWithoutExtension(GDSlave.SLDPath);

//                    //xmlFile = System.IO.Path.GetTempPath() + @"\" + "openproplus_config.xml";

//                    //Utils.XMLNameWOExt = Path.GetFileNameWithoutExtension(xmlFile);
//                    //#region ICDFiles
//                    ////Namrata: 21/03/2018
//                    //ICDFilesData.DirectoryName = Path.GetDirectoryName(xmlFile); //Get DirectoryName
//                    //ICDFilesData.XmlName = Path.GetFileName(xmlFile); //Get XML Name
//                    //ICDFilesData.XmlPath = xmlFile;//XML with full path
//                    //ICDFilesData.XmlWithoutExt = Path.GetFileNameWithoutExtension(ICDFilesData.XmlPath);
//                    //#endregion ICDFiles

//                    //GDSlave.DirName = Path.GetDirectoryName(xmlFile); //Get DirectoryName
//                    //GDSlave.SLDName = Path.GetFileName(xmlFile); //Get XML Name
//                    //GDSlave.SLDPath = xmlFile;//XML with full path
//                    //GDSlave.SLDWithoutExt = Path.GetFileNameWithoutExtension(GDSlave.SLDPath);

//                    //Namarta: 21/11/2019
//                    xmlFile = ICDFilesData.DirectoryName + @"\" + ICDFilesData.XmlName + ".xml";
//                    ICDFilesData.XmlPath = Utils.DirNameSave + ".xml";// Utils.DirNameSave;//XML with full path
//                    writeXMLFile(xmlFile);
//                    xmlFile = Utils.DirNameSave + ".xml";
//                    UpdateXMLFile(xmlFile);
//                    ICDFilesData.IEC61850Xmlname = Path.GetDirectoryName(xmlFile) + @"\"+ Utils.XMLNameWOExt;
//                    if ((Utils.IEC61850ClientMList.Count > 0) || (Utils.IEC61850ServerSList.Count > 0))//Namrata:08/04/2019
//                    {
//                        MessageBox.Show("\"" + ICDFilesData.IEC61850Xmlname + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

//                    }
//                    else
//                    {
//                        MessageBox.Show("\"" + Utils.DirNameSave + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        static void CopyDirectory(DirectoryInfo source, DirectoryInfo destination)
//        {
//            string strRoutineName = "frmOpenProPlus: CopyDirectory";
//            try
//            {
//                if (!destination.Exists)
//                {
//                    destination.Create();
//                }
//                FileInfo[] files = source.GetFiles();
//                foreach (FileInfo file in files)
//                {
//                    file.CopyTo(Path.Combine(destination.FullName, file.Name));
//                }
//                // Process subdirectories.
//                DirectoryInfo[] dirs = source.GetDirectories();
//                foreach (DirectoryInfo dir in dirs)
//                {
//                    string destinationDir = Path.Combine(destination.FullName, dir.Name);  //Get destination directory.
//                    CopyDirectory(dir, new DirectoryInfo(destinationDir)); //Call CopyDirectory() recursively.
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        static IEnumerable<string> GetFileSearchPaths(string fileName)
//        {
//            yield return fileName;
//            yield return Path.Combine(System.IO.Directory.GetParent(Path.GetDirectoryName(fileName)).FullName, Path.GetFileName(fileName));
//        }
//        static bool FileExists(string fileName)
//        {
//            return GetFileSearchPaths(fileName).Any(File.Exists);
//        }
//        string XMLFolder = "";
//        private void saveAsXMLFile()
//        {
//            string strRoutineName = "frmOpenProPlus: saveAsXMLFile";
//            try
//            {
//                string strFilePath = "";
//                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
//                {
//                    if (!string.IsNullOrEmpty(ProtocolGateway.ProtocolGatewayConfigurationFile))
//                    {
//                        //sfdXMLFile.InitialDirectory = Path.GetDirectoryName(ProtocolGateway.ProtocolGatewayConfigurationFile);
//                        //if (!string.IsNullOrEmpty(xmlFile)) { sfdXMLFile.FileName = Path.GetFileName(xmlFile); }
//                        frmInput frminput = new frmInput();
//                        if (frminput.ShowDialog() == DialogResult.OK)
//                        {
//                            if (!string.IsNullOrEmpty(frminput.txtbxInput.Text.Trim()))
//                            {
//                                strFilePath = Path.Combine(Path.GetDirectoryName(ProtocolGateway.ProtocolGatewayConfigurationFile), frminput.txtbxInput.Text.Trim());
//                                frminput.Close();
//                            }
//                        }
//                        else { return; }
//                    }
//                }
//                else if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Full)
//                {
//                    sfdXMLFile.Filter = "XML Files|*.xml";
//                    sfdXMLFile.Title = "Save As XML";
//                    if (!string.IsNullOrEmpty(xmlFile)) { sfdXMLFile.FileName = xmlFile; Utils.XMLOldFileName = xmlFile; }
//                    if (sfdXMLFile.ShowDialog() == DialogResult.OK)
//                    {
//                        //Namrata: 22/10/2019
//                        //string XMLFileName = "OpenProPlus_Config.xml";
//                        //string FilePath = Path.GetDirectoryName(sfdXMLFile.FileName);
//                        //strFilePath = Path.Combine(FilePath, XMLFileName);
//                        //XMLFolder = Path.GetFileNameWithoutExtension(sfdXMLFile.FileName);
//                        strFilePath = sfdXMLFile.FileName.Trim();
//                    }
//                }
//                if (!string.IsNullOrEmpty(strFilePath))
//                {
//                    #region XMLName Without Extension

//                    Utils.XMLNameWOExt = Path.GetFileNameWithoutExtension(strFilePath);
//                    #endregion XMLName Without Extension

//                    #region IEC61850
//                    //Namrata: 27/04/2018
//                    ICDFilesData.DirectoryName = Path.GetDirectoryName(strFilePath); //Get DirectoryName
//                    ICDFilesData.XmlName = Path.GetFileName(strFilePath); // Get XML Name
//                    ICDFilesData.XmlPath = strFilePath; // XML with full path
//                    ICDFilesData.XmlWithoutExt = Path.GetFileNameWithoutExtension(ICDFilesData.XmlPath);
//                    #endregion IEC61850

//                    #region SLDFiles
//                    //if (GDSlave.GDisplaySlave)
//                    //{
//                    //Namrata: 16/08/2019
//                    GDSlave.DirName = Path.GetDirectoryName(strFilePath); //Get DirectoryName
//                    GDSlave.SLDName = Path.GetFileName(strFilePath); //Get XML Name
//                    GDSlave.SLDPath = strFilePath;//XML with full path
//                    GDSlave.SLDWithoutExt = Path.GetFileNameWithoutExtension(GDSlave.SLDPath);
//                    //}
//                    #endregion SLDFiles
//                    Utils.DirNameSave = ICDFilesData.DirectoryName + @"\" + Utils.XMLNameWOExt;
//                    writeXMLFile(strFilePath);
//                    UpdateXMLFile(strFilePath);
//                    Utils.XMLUpdatedFileName = strFilePath;
//                    //Namrata: 29/01/2018
//                    MyMruList = new MruList(Application.ProductName, this.recentFilesToolStripMenuItem, 10, strFilePath, this.myOwnRecentFilesGotCleared_handler);
//                    MyMruList.FileSelected += MyMruList_FileSelected;
//                    if (Utils.IEC61850ClientMList.Count > 0)
//                    {
//                        MessageBox.Show("\"" + ICDFilesData.IEC61850Xmlname + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
//                    }
//                    else
//                    {
//                        MessageBox.Show("\"" + strFilePath + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
//                        //MessageBox.Show("\"" + Path.GetFileName(strFilePath) + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
//        {
//            System.IO.Directory.CreateDirectory(GDSlave.SubDirName);

//            // Copy each file into the new directory.
//            foreach (FileInfo fi in source.GetFiles())
//            {
//                fi.CopyTo(Path.Combine(GDSlave.SubDirName, fi.Name), true);
//            }

//            DirectoryInfo[] dirs = source.GetDirectories();
//            foreach (DirectoryInfo dir in dirs)
//            {
//                var diTarget = new DirectoryInfo(GDSlave.SubDirName);
//                DirectoryInfo CopyDiagramFolder = diTarget;
//                string destinationDir = Path.Combine(CopyDiagramFolder.FullName, dir.Name);
//                CopyDirectory(dir, new DirectoryInfo(destinationDir));
//            }
//        }
//        public void Compressfile()
//        {
//            string fileName = "openproplus_config.xml";
//            string sourcePath = @"C:\Users\swatin\Desktop\890";
//            DirectoryInfo di = new DirectoryInfo(sourcePath);
//            foreach (FileInfo fi in di.GetFiles())
//            {
//                //for specific file 
//                if (fi.ToString() == fileName)
//                {
//                    Compress(fi);
//                }
//            }
//        }

//        public static void Compress(FileInfo fi)
//        {
//            // Get the stream of the source file.
//            using (FileStream inFile = fi.OpenRead())
//            {
//                // Prevent compressing hidden and 
//                // already compressed files.
//                if ((File.GetAttributes(fi.FullName)
//                    & FileAttributes.Hidden)
//                    != FileAttributes.Hidden & fi.Extension != ".gz")
//                {
//                    // Create the compressed file.
//                    using (FileStream outFile = File.Create(fi.FullName + ".gz"))
//                    {
//                        using (GZipStream Compress = new GZipStream(outFile, CompressionMode.Compress))
//                        {
//                            // Copy the source file into 
//                            // the compression stream.
//                            inFile.CopyTo(Compress);

//                            Console.WriteLine("Compressed {0} from {1} to {2} bytes.",
//                                fi.Name, fi.Length.ToString(), outFile.Length.ToString());
//                        }
//                    }
//                }
//            }
//        }


//        private void SaveXMLFiles()
//        {
//            string strRoutineName = "frmOpenProPlus: SaveXMLFiles";
//            try
//            {
//                XmlDocument Xmldoc = new XmlDocument();
//                Xmldoc.Load(Utils.CopyXML);

//                string XMLFileName = "openproplus_config.xml";//updatedXMLFile
//                                                              //string FolderPath = Path.GetDirectoryName(Utils.CopyXML);
//                string FolderPath = Path.GetDirectoryName(Utils.DirNameSave);
//                string XMLName = Path.GetFileNameWithoutExtension(Utils.DirNameSave);

//                string DestDir = Path.Combine(FolderPath, XMLName);

//                if (!System.IO.Directory.Exists(DestDir))
//                {
//                    System.IO.Directory.CreateDirectory(DestDir);
//                    if (System.IO.Directory.Exists(DestDir))
//                    {

//                        File.Move(Utils.CopyXML, DestDir + @"\" + "openproplus_config.xml");
//                    }
//                    else { }
//                }
//                else
//                {
//                    string[] fileEntries = System.IO.Directory.GetFiles(DestDir, "*.xml");
//                    string XmlfileName = string.Empty;
//                    string XmlLocation = string.Empty;

//                    foreach (string s in fileEntries)
//                    {
//                        XmlfileName = System.IO.Path.GetFileName(s);
//                        XmlLocation = System.IO.Path.Combine(DestDir, XmlfileName);
//                        if (File.Exists(XmlLocation))
//                        {
//                            System.IO.File.Delete(XmlLocation);
//                        }
//                        System.IO.File.Move(Utils.CopyXML, DestDir + @"\" + "openproplus_config.xml");
//                    }
//                }
//                string ZipFileName = DestDir + ".zip";
//                if (File.Exists(ZipFileName))
//                {
//                    File.Delete(ZipFileName);
//                    ZipFile.CreateFromDirectory(DestDir, ZipFileName, CompressionLevel.NoCompression, false);

//                }
//                else
//                {
//                    ZipFile.CreateFromDirectory(DestDir, ZipFileName, CompressionLevel.NoCompression, false);
//                }
//                #region Create TarFile
//                //string DirName = Path.Combine(FolderPath, XMLName);
//                //string TarFile = DirName + ".tar.gz";
//                //Utils.XMLNamExt1 = FolderPath;
//                ////string TarFile = DirName + ".tar.gz";
//                ////Namrata:18/10/2019
//                //if (File.Exists(TarFile))
//                //{
//                //    File.Delete(TarFile);
//                //    CreateTarGZ1(TarFile, "openproplus_config.xml");
//                //    //CreateTarGZ(TarFile, DestDir);
//                //}
//                //else
//                //{
//                //    CreateTarGZ1(TarFile, "openproplus_config.xml");
//                //}
//                #endregion Create TarFile

//                #region Remove Folder 
//                if (System.IO.Directory.Exists(DestDir))
//                {
//                    System.IO.Directory.Delete(DestDir, true);
//                }
//                #endregion Remove Folder 
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void CreateTarGZ1(string tgzFilename, string fileName)
//        {
//            using (var outStream = File.Create(tgzFilename))
//            using (var gzoStream = new GZipOutputStream(outStream))
//            using (var tarArchive = TarArchive.CreateOutputTarArchive(gzoStream))
//            {
//                // Add files
//                string[] filenames = System.IO.Directory.GetFiles(@"C:\Users\swatin\Desktop\123");
//                foreach (string filename in filenames)
//                {
//                    TarEntry tarEntry = TarEntry.CreateEntryFromFile(filename);
//                    tarArchive.WriteEntry(tarEntry, true);
//                }
//                //tarArchive.RootPath = Path.GetDirectoryName(@"C:\Users\swatin\Desktop\123");

//                //var tarEntry = TarEntry.CreateEntryFromFile("openproplus_config.xml");
//                //tarEntry.Name = Path.GetFileName(fileName);

//                //tarArchive.WriteEntry(tarEntry, true);
//            }
//        }




//        private void CreateTar(string outputTarFilename, string sourceDirectory)
//        {
//            using (FileStream fs = new FileStream(outputTarFilename, FileMode.Create, FileAccess.Write, FileShare.None))
//            using (Stream gzipStream = new GZipOutputStream(fs))
//            using (TarArchive tarArchive = TarArchive.CreateOutputTarArchive(gzipStream))
//            {
//                AddDirectoryFilesToTar(tarArchive, sourceDirectory, true);
//            }
//        }
//        private void AddDirectoryFilesToTar(TarArchive tarArchive, string sourceDirectory, bool recurse)
//        {
//            //// Recursively add sub-folders
//            if (recurse)
//            {
//                string[] directories = System.IO.Directory.GetDirectories(sourceDirectory);
//                foreach (string directory in directories)
//                {
//                    TarEntry tarEntry = TarEntry.CreateTarEntry(directory);
//                    //tarArchive.WriteEntry(tarEntry, false);
//                    //tarArchive.WriteEntry
//                    AddDirectoryFilesToTar(tarArchive, directory, recurse);
//                }
//            }
//            // Add files
//            string[] filenames = System.IO.Directory.GetFiles(sourceDirectory);
//            foreach (string filename in filenames)
//            {
//                TarEntry tarEntry = TarEntry.CreateEntryFromFile(filename);
//                tarArchive.WriteEntry(tarEntry, true);
//            }
//        }
//        private static void addDirectory(TarArchive tarArchive, string directory)
//        {
//            TarEntry tarEntry = TarEntry.CreateEntryFromFile(directory);
//            tarArchive.WriteEntry(tarEntry, false);

//            //string[] filenames = System.IO.Directory.GetFiles(directory);
//            //foreach (string filename in filenames)
//            //{
//            //    addFile(tarArchive, filename);
//            //}

//            string[] directories = System.IO.Directory.GetDirectories(directory);
//            foreach (string dir in directories)
//                addDirectory(tarArchive, dir);
//        }

//        //Namarta:11/04/2019
//        private void writeXMLFile(string wFile)
//        {
//            string strRoutineName = "frmOpenProPlus: writeXMLFile";
//            try
//            {
//                File.WriteAllText(wFile, opcHandle.getXMLData());
//                #region IEC61850Client And IEC61850Server
//                ICDFilesData.XMLData = wFile;//Namrata:09/04/2019
//                GDSlave.SLDData = wFile;

//                //if ((ICDFilesData.IEC61850Client == true) || (ICDFilesData.IEC61850Server == false) || (GDSlave.GDisplaySlave == true))
//                //{
//                //    SaveXMLSLDClientServer();
//                //}
//                if ((ICDFilesData.IEC61850Client == true) || (ICDFilesData.IEC61850Server == true) || (GDSlave.GDisplaySlave == true))
//                {
//                    SaveXMLSLDClientServer();
//                }
//                else
//                {
//                    Utils.CopyXML = wFile;
//                    SaveXMLFiles();
//                }
//                #endregion IEC61850Client And IEC61850Server
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }


//        private void SaveSLDFiles()
//        {
//            string strRoutineName = "frmOpenProPlus: SaveSLDFiles";
//            try
//            {
//                XmlDocument Xmldoc = new XmlDocument();
//                Utils.GDisplayXMLFile = GDSlave.SLDData;
//                Xmldoc.Load(Utils.GDisplayXMLFile);


//                #region Declarations
//                string XMLFileName = Path.GetFileNameWithoutExtension(Utils.GDisplayXMLFile);//updatedXMLFile
//                string FullXmlPath = GDSlave.DirName + @"\" + GDSlave.SLDWithoutExt;//XMLPath
//                string FileNameWithExtension = Path.GetFileName(Utils.GDisplayXMLFile);

//                //For Moving XMLFile
//                string MoveXMLFiles = GDSlave.DirName + @"\" + GDSlave.SLDWithoutExt;
//                GDSlave.XLSDirFile = System.IO.Path.GetTempPath() + "protocol" + @"\" + "GUI";
//                GDSlave.CopyXMLFile = System.IO.Path.GetTempPath() + "protocol";
//                #endregion Declarations


//                #region Save Updated Txt Files In AppData
//                if (GDSlave.DsExcelData.Tables.Count > 0)
//                {
//                    foreach (DataTable Dt in GDSlave.DsExcelData.Tables)
//                    {
//                        string TableName = Dt.TableName;
//                        string[] tokens = TableName.Split('_');
//                        GDSlave.XLSFileName = TableName.Substring(24);
//                        string FolderName = tokens[0] + "_" + tokens[1];//GDSlave.XLSFileName = tokens[2];
//                        string TxtFilePath = System.IO.Path.GetTempPath() + "protocol" + @"\" + "GUI" + @"\" + FolderName + @"\" + GDSlave.XLSFileName;
//                        if (System.IO.File.Exists(TxtFilePath))
//                        {
//                            File.Delete(TxtFilePath);
//                            ExportDataSetToCsvFile(Dt, TxtFilePath);
//                        }
//                        else { ExportDataSetToCsvFile(Dt, TxtFilePath); }
//                    }
//                }
//                #endregion Save Updated Txt Files In AppData

//                #region Update GDSlave.CreateDirGDSlave Directory Name
//                if (GDSlave.GDisplaySlave)
//                {
//                    if (GDSlave.SLDWithoutExt != "")
//                    { GDSlave.CreateDirGDSlave = GDSlave.DirName + @"\" + GDSlave.SLDWithoutExt + @"\" + "protocol" + @"\" + "GUI"; }
//                    else
//                    { GDSlave.CreateDirGDSlave = GDSlave.DirName + @"\" + "protocol" + @"\" + "GUI"; }
//                }
//                #endregion Update GDSlave.CreateDirGDSlave Directory Name

//                if (Utils.GraphicalDisplaySlaveList.Count > 0)
//                {
//                    #region Get XMLFile From AppData
//                    string fileName = string.Empty;
//                    string destFile = string.Empty;
//                    bool IsXMLExist;
//                    string[] XMLfileNames = System.IO.Directory.GetFiles(GDSlave.CopyXMLFile, "*.xml", SearchOption.TopDirectoryOnly);

//                    if (XMLfileNames.Length != 0)
//                    {
//                        IsXMLExist = true;
//                        foreach (string fileName1 in XMLfileNames)
//                        {
//                            File.Delete(XMLfileNames[0]);
//                        }
//                    }
//                    else { IsXMLExist = false; }
//                    #endregion Get XMLFile From AppData

//                    if (Utils.XMLFilePath != "")
//                    {
//                        FolderNamewithoutexstension = Path.GetDirectoryName(Utils.XMLFilePath) + @"\" + Utils.ExtractedFileName;
//                    }

//                    #region Check If Directory Exist in AppData 
//                    //GDSlave.XLSDirFile = C:\Users\namrata\AppData\Local\Temp\protocol\SLD
//                    if (!System.IO.Directory.Exists(GDSlave.XLSDirFile))
//                    {
//                        System.IO.Directory.CreateDirectory(GDSlave.XLSDirFile);
//                        if (System.IO.Directory.Exists(GDSlave.XLSDirFile))
//                        {
//                            #region Move XML File From User Selected Location To AppData
//                            string[] files = System.IO.Directory.GetFiles(GDSlave.XLSDirFile);//Get Files From Directory
//                            foreach (string s in files)
//                            {
//                                fileName = System.IO.Path.GetFileName(s);
//                                destFile = System.IO.Path.Combine(GDSlave.XLSDirFile, fileName);
//                                File.Move(GDSlave.SLDPath, GDSlave.CopyXMLFile + @"\" + "openproplus_config.xml");
//                                //File.Move(GDSlave.SLDPath, GDSlave.CopyXMLFile + @"\" + GDSlave.SLDWithoutExt);
//                            }
//                            #endregion Move XML File From User Selected Location To AppData
//                        }
//                        else { }
//                    }
//                    else
//                    {
//                        #region Move XML File From User Selected Location To AppData
//                        if (!FileExists(GDSlave.CopyXMLFile + @"\" + GDSlave.SLDName))
//                        {
//                            //Namrata: 22/10/2019
//                            //Fixed XMLFile Name
//                            File.Move(GDSlave.SLDPath, GDSlave.CopyXMLFile + @"\" + "openproplus_config.xml");
//                            //File.Move(GDSlave.SLDPath, GDSlave.CopyXMLFile + @"\" + GDSlave.SLDName);
//                        }
//                        #endregion Move XML File From User Selected Location To AppData
//                    }
//                    #endregion Check If Directory Exist in AppData

//                    #region Create Directory For At User Location
//                    if (!System.IO.Directory.Exists(FullXmlPath))
//                    {
//                        System.IO.Directory.CreateDirectory(FullXmlPath);
//                        string[] fileEntries1 = System.IO.Directory.GetFiles(GDSlave.CopyXMLFile, "*.xml");
//                        string XmlfileName = string.Empty;
//                        string XmlLocation = string.Empty;

//                        foreach (string s in fileEntries1)
//                        {
//                            XmlfileName = System.IO.Path.GetFileName(s);
//                            XmlLocation = System.IO.Path.Combine(MoveXMLFiles, XmlfileName);
//                            if (File.Exists(XmlLocation))
//                            {
//                                System.IO.File.Delete(XmlLocation);
//                            }
//                            System.IO.File.Copy(s, XmlLocation, true);
//                        }

//                        if (!System.IO.Directory.Exists(GDSlave.CreateDirGDSlave))
//                        {
//                            System.IO.Directory.CreateDirectory(GDSlave.CreateDirGDSlave);
//                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(GDSlave.XLSDirFile);
//                            foreach (string subdirectory in subdirectoryEntries)
//                            {
//                                string[] tokens = subdirectory.Split('\\');
//                                GDSlave.SubDirName = GDSlave.CreateDirGDSlave + "\\" + tokens[8];

//                                var diSource = new DirectoryInfo(GDSlave.XLSDirFile + "\\" + tokens[8]);
//                                var diTarget = new DirectoryInfo(GDSlave.CreateDirGDSlave);
//                                CopyAll(diSource, diTarget);

//                            }
//                        }
//                    }
//                    #endregion Create Directory For At User Location
//                    else
//                    {
//                        #region Move XMLFile From AppData To UserLocation
//                        string[] fileEntries11 = System.IO.Directory.GetFiles(GDSlave.CopyXMLFile, "*.xml");
//                        string XmlfileName1 = string.Empty;
//                        string XmlLocation1 = string.Empty;
//                        foreach (string s in fileEntries11)
//                        {
//                            XmlfileName1 = System.IO.Path.GetFileName(s);
//                            XmlLocation1 = System.IO.Path.Combine(MoveXMLFiles, XmlfileName1);
//                            if (File.Exists(XmlLocation1))
//                            {
//                                System.IO.File.Delete(XmlLocation1);
//                            }
//                            System.IO.File.Copy(s, XmlLocation1, true);
//                        }
//                        #endregion Move XMLFile From AppData To UserLocation

//                        if (!System.IO.Directory.Exists(GDSlave.CreateDirGDSlave))
//                        {
//                            System.IO.Directory.CreateDirectory(GDSlave.CreateDirGDSlave);

//                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
//                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(GDSlave.XLSDirFile);
//                            foreach (string subdirectory in subdirectoryEntries)
//                            {
//                                string[] tokens = subdirectory.Split('\\');
//                                GDSlave.SubDirName = GDSlave.CreateDirGDSlave + "\\" + tokens[8];
//                                var diSource = new DirectoryInfo(GDSlave.XLSDirFile + "\\" + tokens[8]);
//                                var diTarget = new DirectoryInfo(GDSlave.CreateDirGDSlave);
//                                CopyAll(diSource, diTarget);
//                            }
//                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
//                        }
//                        else
//                        {
//                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
//                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(GDSlave.XLSDirFile);
//                            foreach (string subdirectory in subdirectoryEntries)
//                            {
//                                string[] tokens = subdirectory.Split('\\');
//                                GDSlave.SubDirName = GDSlave.CreateDirGDSlave + "\\" + tokens[8];
//                                if (System.IO.Directory.Exists(GDSlave.SubDirName))
//                                {
//                                    System.IO.Directory.Delete(GDSlave.SubDirName, true);
//                                    var diSource = new DirectoryInfo(GDSlave.XLSDirFile + "\\" + tokens[8]);
//                                    var diTarget = new DirectoryInfo(GDSlave.CreateDirGDSlave);
//                                    CopyAll(diSource, diTarget);
//                                }
//                                else
//                                {
//                                    var diSource1 = new DirectoryInfo(GDSlave.XLSDirFile + "\\" + tokens[8]);
//                                    var diTarget1 = new DirectoryInfo(GDSlave.CreateDirGDSlave);
//                                    CopyAll(diSource1, diTarget1);
//                                }
//                            }
//                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
//                        }
//                    }

//                    string ZipFileName = FullXmlPath + ".zip";
//                    if (File.Exists(ZipFileName))
//                    {
//                        File.Delete(ZipFileName);
//                        ZipFile.CreateFromDirectory(FullXmlPath, ZipFileName, CompressionLevel.NoCompression, false);

//                    }
//                    else
//                    {
//                        ZipFile.CreateFromDirectory(FullXmlPath, ZipFileName, CompressionLevel.NoCompression, false);
//                    }

//                    if (System.IO.Directory.Exists(FullXmlPath))
//                    {
//                        System.IO.Directory.Delete(FullXmlPath, true);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void SaveXMLSLDClientServer()
//        {
//            string strRoutineName = "frmOpenProPlus: SaveXMLSLDClientServer";
//            try
//            {
//                XmlDocument Xmldoc = new XmlDocument();
//                Utils.XMLFilecopy = ICDFilesData.XMLData;
//                Xmldoc.Load(Utils.XMLFilecopy);

//                #region Declarations
//                string updatedXMLFile = Path.GetFileNameWithoutExtension(Utils.XMLFilecopy);//ABC      
//                string XMLPath = ICDFilesData.DirectoryName + @"\" + ICDFilesData.XmlWithoutExt;
//                string GetFilewithextension = Path.GetFileName(Utils.XMLFilecopy);
//                string MoveXMLFiles = ICDFilesData.DirectoryName + @"\" + ICDFilesData.XmlWithoutExt; //For Moving XMLFile
//                string FullXmlPath = ICDFilesData.DirectoryName + @"\" + ICDFilesData.XmlWithoutExt;
//                //IEC61850Client Directory
//                ICDFilesData.ICDDirMFile = System.IO.Path.GetTempPath() + "protocol" + @"\" + "IEC61850Client";
//                //IEC61850Server Directory
//                ICDFilesData.ICDDirSFile = System.IO.Path.GetTempPath() + "protocol" + @"\" + "IEC61850Server";
//                //Namrata:08/04/2019 
//                ICDFilesData.CopyXMLFile = System.IO.Path.GetTempPath() + "protocol";
//                GDSlave.XLSDirFile = System.IO.Path.GetTempPath() + "protocol" + @"\" + "GUI";
//                #endregion Declarations

//                #region Create IEC61850Client,IEC61850Server,SLD Directory Path
//                if ((ICDFilesData.IEC61850Client == true) || (ICDFilesData.IEC61850Server == true) || (GDSlave.GDisplaySlave == true))
//                {
//                    if (ICDFilesData.XmlWithoutExt != "")
//                    {
//                        ICDFilesData.CreateDirIEC61850C = ICDFilesData.DirectoryName + @"\" + ICDFilesData.XmlWithoutExt + @"\" + "protocol" + @"\" + "IEC61850Client";//Directory Path
//                        ICDFilesData.CreateDirIEC61850S = ICDFilesData.DirectoryName + @"\" + ICDFilesData.XmlWithoutExt + @"\" + "protocol" + @"\" + "IEC61850Server";//Directory Path
//                        //GDSlave.CreateDirGDSlave = GDSlave.DirName + @"\" + GDSlave.SLDWithoutExt + @"\" + "protocol" + @"\" + "GUI";
//                    }
//                    else
//                    {
//                        ICDFilesData.CreateDirIEC61850C = ICDFilesData.DirectoryName + @"\" + "protocol" + @"\" + "IEC61850Client";//Create DirectoryName
//                        ICDFilesData.CreateDirIEC61850S = ICDFilesData.DirectoryName + @"\" + "protocol" + @"\" + "IEC61850Server";//Create DirectoryName

//                        //GDSlave.CreateDirGDSlave = GDSlave.DirName + @"\" + "protocol" + @"\" + "GUI";
//                    }
//                }
//                #endregion Create IEC61850Client,IEC61850Server,SLD Directory Path

//                if (GDSlave.GDisplaySlave)
//                {
//                    if (GDSlave.SLDWithoutExt != "")
//                    { GDSlave.CreateDirGDSlave = GDSlave.DirName + @"\" + GDSlave.SLDWithoutExt + @"\" + "protocol" + @"\" + "GUI"; }
//                    else
//                    { GDSlave.CreateDirGDSlave = GDSlave.DirName + @"\" + "protocol" + @"\" + "GUI"; }
//                }

//                #region Get XMLFile From AppData
//                string fileName = string.Empty;
//                string destFile = string.Empty;
//                bool IsXMLExist;
//                //string[] XMLfileNames = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml", SearchOption.TopDirectoryOnly); //Utils.XMLFilecopy
//                ////string[] XMLfileNames = System.IO.Directory.GetFiles(Utils.XMLFilecopy, "*.xml", SearchOption.TopDirectoryOnly); //Utils.XMLFilecopy
//                //if (XMLfileNames.Length != 0)
//                //{
//                //    IsXMLExist = true;
//                //    foreach (string fileName1 in XMLfileNames)
//                //    {
//                //        File.Delete(XMLfileNames[0]);
//                //    }
//                //}
//                //else { IsXMLExist = false; }
//                #endregion Get XMLFile From AppData

//                if (Utils.XMLFilePath != "")
//                {
//                    FolderNamewithoutexstension = Path.GetDirectoryName(Utils.XMLFilePath) + @"\" + Utils.ExtractedFileName;
//                }

//                #region IEC61850Server Slave
//                if (Utils.IEC61850ServerSList.Count > 0)
//                {
//                    #region Check If Directory Exist in AppData 
//                    //GDSlave.XLSDirFile = C:\Users\namrata\AppData\Local\Temp\protocol\SLD
//                    if (!System.IO.Directory.Exists(ICDFilesData.ICDDirSFile))
//                    {
//                        System.IO.Directory.CreateDirectory(ICDFilesData.ICDDirSFile);
//                        if (System.IO.Directory.Exists(ICDFilesData.ICDDirSFile))
//                        {
//                            #region Move XML File From User Selected Location To AppData
//                            string[] files = System.IO.Directory.GetFiles(ICDFilesData.ICDDirSFile);//Get Files From Directory
//                            foreach (string s in files)
//                            {
//                                fileName = System.IO.Path.GetFileName(s);
//                                destFile = System.IO.Path.Combine(ICDFilesData.ICDDirSFile, fileName);
//                                File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
//                            }
//                            #endregion Move XML File From User Selected Location To AppData
//                        }
//                        else { }
//                    }
//                    else
//                    {
//                        #region Move XML File From User Selected Location To AppData
//                        if (!FileExists(ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml"))
//                        {
//                            //Namrata: 22/10/2019
//                            //Fixed XMLFile Name
//                            File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
//                        }
//                        #endregion Move XML File From User Selected Location To AppData
//                    }
//                    #endregion Check If Directory Exist in AppData

//                    #region Create Directory For At User Location
//                    if (!System.IO.Directory.Exists(FullXmlPath))
//                    {
//                        System.IO.Directory.CreateDirectory(FullXmlPath);
//                        string[] fileEntries1 = System.IO.Directory.GetFiles(XMLPath, "*.xml");
//                        string XmlfileName = string.Empty;
//                        string XmlLocation = string.Empty;

//                        foreach (string s in fileEntries1)
//                        {
//                            XmlfileName = System.IO.Path.GetFileName(s);
//                            XmlLocation = System.IO.Path.Combine(MoveXMLFiles, XmlfileName);
//                            if (File.Exists(XmlLocation))
//                            {
//                                System.IO.File.Delete(XmlLocation);
//                            }
//                            System.IO.File.Copy(s, XmlLocation, true);
//                        }
//                        if (!System.IO.Directory.Exists(ICDFilesData.CreateDirIEC61850S))
//                        {
//                            System.IO.Directory.CreateDirectory(ICDFilesData.CreateDirIEC61850S);
//                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(ICDFilesData.ICDDirSFile);
//                            foreach (string subdirectory in subdirectoryEntries)
//                            {
//                                string[] tokens = subdirectory.Split('\\');
//                                GDSlave.SubDirName = ICDFilesData.CreateDirIEC61850S + "\\" + tokens[8];
//                                var diSource = new DirectoryInfo(ICDFilesData.ICDDirSFile + "\\" + tokens[8]);
//                                var diTarget = new DirectoryInfo(ICDFilesData.CreateDirIEC61850S);
//                                CopyAll(diSource, diTarget);
//                            }
//                        }
//                    }
//                    #endregion Create Directory For At User Location
//                    else
//                    {
//                        #region Move XMLFile From AppData To UserLocation
//                        string[] fileEntries11 = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml");
//                        string XmlfileName1 = string.Empty;
//                        string XmlLocation1 = string.Empty;
//                        foreach (string s in fileEntries11)
//                        {
//                            XmlfileName1 = System.IO.Path.GetFileName(s);
//                            XmlLocation1 = System.IO.Path.Combine(MoveXMLFiles, XmlfileName1);
//                            if (File.Exists(XmlLocation1))
//                            {
//                                System.IO.File.Delete(XmlLocation1);
//                            }
//                            System.IO.File.Copy(s, XmlLocation1, true);
//                        }
//                        #endregion Move XMLFile From AppData To UserLocation

//                        if (!System.IO.Directory.Exists(ICDFilesData.CreateDirIEC61850S))
//                        {
//                            System.IO.Directory.CreateDirectory(ICDFilesData.CreateDirIEC61850S);

//                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
//                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(ICDFilesData.ICDDirSFile);
//                            foreach (string subdirectory in subdirectoryEntries)
//                            {
//                                string[] tokens = subdirectory.Split('\\');
//                                GDSlave.SubDirName = ICDFilesData.CreateDirIEC61850S + "\\" + tokens[8];
//                                var diSource = new DirectoryInfo(ICDFilesData.ICDDirSFile + "\\" + tokens[8]);
//                                var diTarget = new DirectoryInfo(ICDFilesData.CreateDirIEC61850S);
//                                CopyAll(diSource, diTarget);
//                            }
//                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
//                        }
//                        else
//                        {
//                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
//                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(ICDFilesData.ICDDirSFile);
//                            foreach (string subdirectory in subdirectoryEntries)
//                            {
//                                string[] tokens = subdirectory.Split('\\');
//                                GDSlave.SubDirName = ICDFilesData.CreateDirIEC61850S + "\\" + tokens[8];
//                                if (System.IO.Directory.Exists(GDSlave.SubDirName))
//                                {
//                                    System.IO.Directory.Delete(GDSlave.SubDirName, true);
//                                    var diSource = new DirectoryInfo(ICDFilesData.ICDDirSFile + "\\" + tokens[8]);
//                                    var diTarget = new DirectoryInfo(ICDFilesData.CreateDirIEC61850S);
//                                    CopyAll(diSource, diTarget);
//                                }
//                                else
//                                {
//                                    var diSource1 = new DirectoryInfo(ICDFilesData.ICDDirSFile + "\\" + tokens[8]);
//                                    var diTarget1 = new DirectoryInfo(ICDFilesData.CreateDirIEC61850S);
//                                    CopyAll(diSource1, diTarget1);
//                                }
//                            }
//                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
//                        }
//                    }

//                }
//                #endregion IEC61850Server Slave

//                #region IEC61850Client Master
//                if (Utils.IEC61850ClientMList.Count > 0)
//                {
//                    #region Check If Directory Exist in AppData 
//                    //GDSlave.XLSDirFile = C:\Users\namrata\AppData\Local\Temp\protocol\SLD
//                    if (!System.IO.Directory.Exists(ICDFilesData.ICDDirMFile))
//                    {
//                        System.IO.Directory.CreateDirectory(ICDFilesData.ICDDirMFile);
//                        if (System.IO.Directory.Exists(ICDFilesData.ICDDirMFile))
//                        {
//                            #region Move XML File From User Selected Location To AppData
//                            string[] files = System.IO.Directory.GetFiles(ICDFilesData.ICDDirMFile);//Get Files From Directory
//                            foreach (string s in files)
//                            {
//                                fileName = System.IO.Path.GetFileName(s);
//                                destFile = System.IO.Path.Combine(ICDFilesData.ICDDirMFile, fileName);
//                                File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
//                            }
//                            #endregion Move XML File From User Selected Location To AppData
//                        }
//                        else { }
//                    }
//                    else
//                    {
//                        #region Move XML File From User Selected Location To AppData
//                        if (!FileExists(ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml"))
//                        {
//                            //Namrata: 22/10/2019
//                            //Fixed XMLFile Name
//                            File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
//                        }
//                        #endregion Move XML File From User Selected Location To AppData
//                    }
//                    #endregion Check If Directory Exist in AppData

//                    #region Create Directory For At User Location
//                    if (!System.IO.Directory.Exists(FullXmlPath))
//                    {
//                        System.IO.Directory.CreateDirectory(FullXmlPath);
//                        string[] fileEntries1 = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml");
//                        string XmlfileName = string.Empty;
//                        string XmlLocation = string.Empty;

//                        foreach (string s in fileEntries1)
//                        {
//                            XmlfileName = System.IO.Path.GetFileName(s);
//                            XmlLocation = System.IO.Path.Combine(MoveXMLFiles, XmlfileName);
//                            if (File.Exists(XmlLocation))
//                            {
//                                System.IO.File.Delete(XmlLocation);
//                            }
//                            System.IO.File.Copy(s, XmlLocation, true);
//                        }
//                        if (!System.IO.Directory.Exists(ICDFilesData.CreateDirIEC61850C))
//                        {
//                            System.IO.Directory.CreateDirectory(ICDFilesData.CreateDirIEC61850C);
//                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(ICDFilesData.ICDDirMFile);
//                            foreach (string subdirectory in subdirectoryEntries)
//                            {
//                                string[] tokens = subdirectory.Split('\\');
//                                GDSlave.SubDirName = ICDFilesData.CreateDirIEC61850C + "\\" + tokens[8];
//                                var diSource = new DirectoryInfo(ICDFilesData.ICDDirMFile + "\\" + tokens[8]);
//                                var diTarget = new DirectoryInfo(ICDFilesData.CreateDirIEC61850C);
//                                CopyAll(diSource, diTarget);
//                            }
//                        }
//                    }
//                    #endregion Create Directory For At User Location
//                    else
//                    {
//                        #region Move XMLFile From AppData To UserLocation
//                        string[] fileEntries11 = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml");
//                        string XmlfileName1 = string.Empty;
//                        string XmlLocation1 = string.Empty;
//                        foreach (string s in fileEntries11)
//                        {
//                            XmlfileName1 = System.IO.Path.GetFileName(s);
//                            XmlLocation1 = System.IO.Path.Combine(MoveXMLFiles, XmlfileName1);
//                            if (File.Exists(XmlLocation1))
//                            {
//                                System.IO.File.Delete(XmlLocation1);
//                            }
//                            System.IO.File.Copy(s, XmlLocation1, true);
//                        }
//                        #endregion Move XMLFile From AppData To UserLocation

//                        if (!System.IO.Directory.Exists(ICDFilesData.CreateDirIEC61850C))
//                        {
//                            System.IO.Directory.CreateDirectory(ICDFilesData.CreateDirIEC61850C);

//                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
//                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(ICDFilesData.ICDDirMFile);
//                            foreach (string subdirectory in subdirectoryEntries)
//                            {
//                                string[] tokens = subdirectory.Split('\\');
//                                GDSlave.SubDirName = ICDFilesData.CreateDirIEC61850S + "\\" + tokens[8];
//                                var diSource = new DirectoryInfo(ICDFilesData.ICDDirMFile + "\\" + tokens[8]);
//                                var diTarget = new DirectoryInfo(ICDFilesData.CreateDirIEC61850C);
//                                CopyAll(diSource, diTarget);
//                            }
//                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
//                        }
//                        else
//                        {
//                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
//                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(ICDFilesData.ICDDirMFile);
//                            foreach (string subdirectory in subdirectoryEntries)
//                            {
//                                string[] tokens = subdirectory.Split('\\');
//                                GDSlave.SubDirName = ICDFilesData.CreateDirIEC61850C + "\\" + tokens[8];
//                                if (System.IO.Directory.Exists(GDSlave.SubDirName))
//                                {
//                                    System.IO.Directory.Delete(GDSlave.SubDirName, true);
//                                    var diSource = new DirectoryInfo(ICDFilesData.ICDDirMFile + "\\" + tokens[8]);
//                                    var diTarget = new DirectoryInfo(ICDFilesData.CreateDirIEC61850C);
//                                    CopyAll(diSource, diTarget);
//                                }
//                                else
//                                {
//                                    var diSource1 = new DirectoryInfo(ICDFilesData.ICDDirMFile + "\\" + tokens[8]);
//                                    var diTarget1 = new DirectoryInfo(ICDFilesData.CreateDirIEC61850C);
//                                    CopyAll(diSource1, diTarget1);
//                                }
//                            }
//                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
//                        }
//                    }
//                }
//                #endregion IEC61850Client Master

//                #region GraphicalDisplay Slave
//                if (Utils.GraphicalDisplaySlaveList.Count > 0)
//                {
//                    #region Save Updated Txt Files In AppData
//                    if (GDSlave.DsExcelData.Tables.Count > 0)
//                    {
//                        foreach (DataTable Dt in GDSlave.DsExcelData.Tables)
//                        {
//                            string TableName = Dt.TableName;
//                            string[] tokens = TableName.Split('_');
//                            GDSlave.XLSFileName = TableName.Substring(24);
//                            string FolderName = tokens[0] + "_" + tokens[1];
//                            string TxtFilePath = System.IO.Path.GetTempPath() + "protocol" + @"\" + "GUI" + @"\" + FolderName + @"\" + GDSlave.XLSFileName;
//                            if (System.IO.File.Exists(TxtFilePath))
//                            {
//                                File.Delete(TxtFilePath);
//                                ExportDataSetToCsvFile(Dt, TxtFilePath);
//                            }
//                            else
//                            {
//                                ExportDataSetToCsvFile(Dt, TxtFilePath);
//                            }
//                        }
//                    }
//                    #endregion Save Updated Txt Files In AppData

//                    #region Check If Directory Exist in AppData 
//                    //GDSlave.XLSDirFile = C:\Users\namrata\AppData\Local\Temp\protocol\SLD
//                    if (!System.IO.Directory.Exists(GDSlave.XLSDirFile))
//                    {
//                        System.IO.Directory.CreateDirectory(GDSlave.XLSDirFile);
//                        if (System.IO.Directory.Exists(GDSlave.XLSDirFile))
//                        {
//                            #region Move XML File From User Selected Location To AppData
//                            string[] files = System.IO.Directory.GetFiles(GDSlave.XLSDirFile);//Get Files From Directory
//                            foreach (string s in files)
//                            {
//                                fileName = System.IO.Path.GetFileName(s);
//                                destFile = System.IO.Path.Combine(GDSlave.XLSDirFile, fileName);
//                                File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
//                            }
//                            #endregion Move XML File From User Selected Location To AppData
//                        }
//                        else { }
//                    }
//                    else
//                    {
//                        //Namrata:20/11/2019
//                        string[] XMLfileNames = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml", SearchOption.TopDirectoryOnly); //Utils.XMLFilecopy
//                        //string[] XMLfileNames = System.IO.Directory.GetFiles(Utils.XMLFilecopy, "*.xml", SearchOption.TopDirectoryOnly); //Utils.XMLFilecopy
//                        if (XMLfileNames.Length != 0)
//                        {
//                            IsXMLExist = true;
//                            foreach (string fileName1 in XMLfileNames)
//                            {
//                                File.Delete(XMLfileNames[0]);
//                            }
//                        }
//                        else { IsXMLExist = false; }
//                        #region Move XML File From User Selected Location To AppData
//                        if (!FileExists(ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml"))
//                        {
//                            //Namrata: 22/10/2019
//                            //Fixed XMLFile Name
//                            File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
//                        }
//                        else
//                        {
//                            File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
//                        }
//                        #endregion Move XML File From User Selected Location To AppData
//                    }
//                    #endregion Check If Directory Exist in AppData

//                    #region Create Directory For At User Location
//                    if (!System.IO.Directory.Exists(FullXmlPath))
//                    {
//                        System.IO.Directory.CreateDirectory(FullXmlPath);
//                        string[] fileEntries1 = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml");
//                        string XmlfileName = string.Empty;
//                        string XmlLocation = string.Empty;

//                        foreach (string s in fileEntries1)
//                        {
//                            XmlfileName = System.IO.Path.GetFileName(s);
//                            XmlLocation = System.IO.Path.Combine(MoveXMLFiles, XmlfileName);
//                            if (File.Exists(XmlLocation))
//                            {
//                                System.IO.File.Delete(XmlLocation);
//                            }
//                            System.IO.File.Copy(s, XmlLocation, true);
//                        }
//                        if (!System.IO.Directory.Exists(GDSlave.CreateDirGDSlave))
//                        {
//                            System.IO.Directory.CreateDirectory(GDSlave.CreateDirGDSlave);
//                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(GDSlave.XLSDirFile);
//                            foreach (string subdirectory in subdirectoryEntries)
//                            {
//                                string[] tokens = subdirectory.Split('\\');
//                                GDSlave.SubDirName = GDSlave.CreateDirGDSlave + "\\" + tokens[8];
//                                var diSource = new DirectoryInfo(GDSlave.XLSDirFile + "\\" + tokens[8]);
//                                var diTarget = new DirectoryInfo(GDSlave.CreateDirGDSlave);
//                                CopyAll(diSource, diTarget);
//                            }
//                        }
//                    }
//                    #endregion Create Directory For At User Location
//                    else
//                    {
//                        #region Move XMLFile From AppData To UserLocation
//                        string[] fileEntries11 = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml");
//                        string XmlfileName1 = string.Empty;
//                        string XmlLocation1 = string.Empty;
//                        foreach (string s in fileEntries11)
//                        {
//                            XmlfileName1 = System.IO.Path.GetFileName(s);
//                            XmlLocation1 = System.IO.Path.Combine(MoveXMLFiles, XmlfileName1);
//                            if (File.Exists(XmlLocation1))
//                            {
//                                System.IO.File.Delete(XmlLocation1);
//                            }
//                            System.IO.File.Copy(s, XmlLocation1, true);
//                        }
//                        #endregion Move XMLFile From AppData To UserLocation

//                        if (!System.IO.Directory.Exists(GDSlave.CreateDirGDSlave))
//                        {
//                            System.IO.Directory.CreateDirectory(GDSlave.CreateDirGDSlave);

//                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
//                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(GDSlave.XLSDirFile);
//                            foreach (string subdirectory in subdirectoryEntries)
//                            {
//                                string[] tokens = subdirectory.Split('\\');
//                                GDSlave.SubDirName = GDSlave.CreateDirGDSlave + "\\" + tokens[8];
//                                var diSource = new DirectoryInfo(GDSlave.XLSDirFile + "\\" + tokens[8]);
//                                var diTarget = new DirectoryInfo(GDSlave.CreateDirGDSlave);
//                                CopyAll(diSource, diTarget);
//                            }
//                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
//                        }
//                        else
//                        {
//                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
//                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(GDSlave.XLSDirFile);
//                            foreach (string subdirectory in subdirectoryEntries)
//                            {
//                                string[] tokens = subdirectory.Split('\\');
//                                GDSlave.SubDirName = GDSlave.CreateDirGDSlave + "\\" + tokens[8];
//                                if (System.IO.Directory.Exists(GDSlave.SubDirName))
//                                {
//                                    System.IO.Directory.Delete(GDSlave.SubDirName, true);
//                                    var diSource = new DirectoryInfo(GDSlave.XLSDirFile + "\\" + tokens[8]);
//                                    var diTarget = new DirectoryInfo(GDSlave.CreateDirGDSlave);
//                                    CopyAll(diSource, diTarget);
//                                }
//                                else
//                                {
//                                    var diSource1 = new DirectoryInfo(GDSlave.XLSDirFile + "\\" + tokens[8]);
//                                    var diTarget1 = new DirectoryInfo(GDSlave.CreateDirGDSlave);
//                                    CopyAll(diSource1, diTarget1);
//                                }
//                            }
//                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
//                        }
//                    }

//                }
//                #endregion GraphicalDisplay Slave

//                #region Create .Zip File
//                string ZipFileName = FullXmlPath + ".zip";
//                if (File.Exists(ZipFileName))
//                {
//                    File.Delete(ZipFileName);
//                    ZipFile.CreateFromDirectory(FullXmlPath, ZipFileName, CompressionLevel.NoCompression, false);
//                }
//                else
//                {
//                    ZipFile.CreateFromDirectory(FullXmlPath, ZipFileName, CompressionLevel.NoCompression, false);
//                }
//                #endregion Create .Zip File

//                #region Delete Folder From UserLocation
//                if (System.IO.Directory.Exists(FullXmlPath))
//                {
//                    System.IO.Directory.Delete(FullXmlPath, true);
//                }
//                #endregion Delete Folder From UserLocation
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }



//        private bool CheckVersions(string xmlFilePath)
//        {
//            string strRoutineName = "frmOpenProPlus: CheckVersions";
//            try
//            {
//                string[] openproconfigVers = Utils.GetVersionsFromOPPConfigXSD();
//                string[] commonconfigVers = Utils.GetVersionsFromCommonXSD();
//                string[] xmlfileVers = Utils.GetVersionsFromXML(xmlFilePath);
//                if (openproconfigVers != null && commonconfigVers != null && xmlfileVers != null)
//                {
//                    if (openproconfigVers.Count() > 0 && commonconfigVers.Count() > 0 && xmlfileVers.Count() > 0)
//                    {
//                        if (!string.IsNullOrEmpty(openproconfigVers[0]) && !string.IsNullOrEmpty(commonconfigVers[0]))
//                        {
//                            if (!string.IsNullOrEmpty(xmlfileVers[0]))
//                            {
//                                if (xmlfileVers[0] != "OppConfiguratorVer=" + Utils.AssemblyVersion)
//                                {
//                                    MessageBox.Show("'" + Path.GetFileName(xmlFilePath) + "' file was created using OpenPro+Configurator version " + Utils.GetVersionSubstring(xmlfileVers[0]) + ", and the current OpenPro+Configurator version is " + Utils.AssemblyVersion, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
//                                    return false;
//                                }
//                            }
//                            if (openproconfigVers[0] != commonconfigVers[0])
//                            {
//                                MessageBox.Show("OpenPro+Configurator version (" + Utils.AssemblyVersion + ") is mismatched in openproplus_config.xsd and common_config.xsd", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
//                                return false;
//                            }
//                            if (openproconfigVers[0] != "OppConfiguratorVer=" + Utils.AssemblyVersion)
//                            {
//                                MessageBox.Show("OpenPro+Configurator version (" + Utils.AssemblyVersion + ") is mismatched in openproplus_config.xsd", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
//                                return false;
//                            }
//                            if (commonconfigVers[0] != "OppConfiguratorVer=" + Utils.AssemblyVersion)
//                            {
//                                MessageBox.Show("OpenPro+Configurator version (" + Utils.AssemblyVersion + ") is mismatched in common_config.xsd", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
//                                return false;
//                            }
//                        }
//                    }
//                    if (openproconfigVers.Count() > 1 && commonconfigVers.Count() > 1 && xmlfileVers.Count() > 1)
//                    {
//                        if (!string.IsNullOrEmpty(openproconfigVers[1]) && !string.IsNullOrEmpty(commonconfigVers[1]))
//                        {
//                            if (!string.IsNullOrEmpty(xmlfileVers[1]))
//                            {
//                                if (xmlfileVers[1] != openproconfigVers[1])
//                                {
//                                    MessageBox.Show("'" + Path.GetFileName(xmlFilePath) + "' file was created using openproplus_config.xsd version " + Utils.GetVersionSubstring(xmlfileVers[1]) + ", and the current openproplus_config.xsd version is " + Utils.GetVersionSubstring(openproconfigVers[1]), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
//                                    return false;
//                                }
//                            }
//                            if (openproconfigVers[1] != commonconfigVers[1])
//                            {
//                                MessageBox.Show("openproplus_config.xsd version (" + Utils.GetVersionSubstring(openproconfigVers[1]) + ") is mismatched in common_config.xsd", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
//                                return false;
//                            }
//                        }
//                    }
//                    if (openproconfigVers.Count() > 2 && commonconfigVers.Count() > 2 && xmlfileVers.Count() > 2)
//                    {
//                        if (!string.IsNullOrEmpty(openproconfigVers[2]) && !string.IsNullOrEmpty(commonconfigVers[2]))
//                        {
//                            if (!string.IsNullOrEmpty(xmlfileVers[2]))
//                            {
//                                if (xmlfileVers[2] != commonconfigVers[2])
//                                {
//                                    MessageBox.Show("'" + Path.GetFileName(xmlFilePath) + "' file was created using common_config.xsd version " + Utils.GetVersionSubstring(xmlfileVers[2]) + ", and the current common_config.xsd version is " + Utils.GetVersionSubstring(commonconfigVers[2]), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
//                                    return false;
//                                }
//                            }
//                            if (openproconfigVers[2] != commonconfigVers[2])
//                            {
//                                MessageBox.Show("common_config.xsd version (" + Utils.GetVersionSubstring(commonconfigVers[2]) + ") is mismatched in openproplus_config.xsd", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
//                                return false;
//                            }
//                        }
//                    }
//                }
//                return true;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return false;
//            }
//        }


//        string GetProtocolFolder = "";
//        private void openXMLFile()
//        {
//            string strRoutineName = "frmOpenProPlus: openXMLFile";
//            try
//            {
//                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Full)
//                {
//                    #region AppMode=Full

//                    ofdXMLFile.Filter = "XML Files| *.xml|Zip Files|*.zip;*.rar"; //"XML Files|*.xml";
//                    ofdXMLFile.FilterIndex = 1;
//                    ofdXMLFile.RestoreDirectory = true;
//                    ofdXMLFile.Title = "Browse ZIP File";//"Browse XML File";
//                    if (ofdXMLFile.ShowDialog() == DialogResult.OK)
//                    {
//                        string openedFile = ofdXMLFile.FileName; //Namrata: 18/11/2017
//                        string DirectoryExtention = Path.GetExtension(ofdXMLFile.FileName);

//                        // #region Namrata
//                        //Namrata: 23/10/2019
//                        if (DirectoryExtention == ".zip")
//                        {
//                            string extractPath = System.IO.Path.GetTempPath() + "protocol";//Path.GetDirectoryName(openedFile);
//                            string ZIPName = Path.GetDirectoryName(openedFile);
//                            string ZipFolderName = Path.GetFileNameWithoutExtension(openedFile);
//                            string GetXMLPath = Path.Combine(ZIPName, ZipFolderName);

//                            Utils.DirNameSave = ZIPName+ @"\" + ZipFolderName;
//                            if (System.IO.Directory.Exists(Utils.DirNameSave))
//                            {
//                                System.IO.Directory.Delete(Utils.DirNameSave, true);
//                            }
//                                if (!System.IO.Directory.Exists(extractPath))
//                            {
//                                System.IO.Directory.CreateDirectory(extractPath);
//                                ZipFile.ExtractToDirectory(openedFile, GetXMLPath);
//                            }
//                            else
//                            {
//                                System.IO.Directory.Delete(extractPath, true);
//                                System.IO.Directory.CreateDirectory(extractPath);
//                                ZipFile.ExtractToDirectory(openedFile, GetXMLPath);
//                            }
//                            string[] fileEntries1 = System.IO.Directory.GetFiles(GetXMLPath, "*.xml");

//                            string fileName1 = string.Empty;
//                            string destFile1 = string.Empty;
//                            foreach (string s in fileEntries1)
//                            {
//                                fileName1 = System.IO.Path.GetFileName(s);
//                                destFile1 = System.IO.Path.Combine(extractPath, fileName1);
//                                System.IO.File.Move(s, destFile1);
//                                ofdXMLFile.FileName = destFile1;
//                            }
//                            string Protocol = "protocol";
//                            string GetProtocol = Path.Combine(GetXMLPath, Protocol);
//                            if (!System.IO.Directory.Exists(GetProtocol))
//                            {
//                                if (!CheckVersions(GetXMLPath))
//                                {
//                                    VersionMatch = false;
//                                    DialogResult rslt = MessageBox.Show("Do you still want to continue with mismatched versions of OpenPro+Configurator, xml and xsd files?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
//                                    if (rslt == DialogResult.No)
//                                    {
//                                        return;
//                                    }
//                                    else { }
//                                }
//                                else
//                                {
//                                    VersionMatch = true; //Ajay: 11/01/2019
//                                }
//                                //Namrata:26/04/2018
//                                Utils.XMLFolderPath = Path.GetDirectoryName(GetProtocol);

//                                #region IEC61850
//                                //Namrata: 27/04/2018
//                                ICDFilesData.DirectoryName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
//                                ICDFilesData.XmlName = Path.GetFileName(ofdXMLFile.FileName); // Get XML Name
//                                ICDFilesData.XmlPath = ofdXMLFile.FileName; // XML with full path
//                                ICDFilesData.XmlWithoutExt = "";

//                                #region ClearDatasets
//                                Utils.dsIED.Clear();
//                                Utils.dsIED.Tables.Clear();
//                                Utils.DsRCB.Clear();
//                                Utils.DsRCBData.Clear();
//                                Utils.dsResponseType.Clear();
//                                Utils.DsResponseType.Clear();
//                                Utils.dsIED.Clear();
//                                Utils.dsIEDName.Clear();
//                                Utils.DsAllConfigurationData.Clear();
//                                Utils.DsAllConfigureData.Clear();
//                                Utils.DsRCBDataset.Clear();
//                                Utils.DsRCBDS.Clear();
//                                Utils.DtRCBdata.Clear();
//                                #endregion ClearDatasets
//                                #endregion IEC61850

//                                #region GDisplay
//                                //Namrata: 27/04/2018
//                                GDSlave.DirName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
//                                GDSlave.SLDName = Path.GetFileName(ofdXMLFile.FileName); //Get XML Name
//                                GDSlave.SLDPath = ofdXMLFile.FileName;//XML with full path
//                                GDSlave.SLDWithoutExt = "";
//                                #region ClearDatasets
//                                GDSlave.DsExcelData.Tables.Clear();
//                                GDSlave.DsExcelData.Clear();
//                                GDSlave.DtExcelData.Clear();
//                                GDSlave.DsExportData.Tables.Clear();
//                                GDSlave.DTAIImage.Clear();
//                                GDSlave.DTDIDOImage.Clear();
//                                #endregion ClearDatasets
//                                #endregion GDisplay
//                                this.MyMruList.AddFile(ofdXMLFile.FileName); //Now give it to the MRUManager
//                                int result = 0;
//                                string errMsg = "XML file is valid...";
//                                ResetConfiguratorState(false);
//                                showLoading();
//                                xmlFile = "";//reset old filename...
//                                pnlValidationMessages.Visible = true;
//                                ListViewItem lvi;
//                                lvi = new ListViewItem("Validating file: " + ofdXMLFile.FileName);
//                                //Namrata: 27/7/2017
//                                toolStripStatusLabel1.Visible = true;  //Display File Name in Toolstrip 
//                                tspFileName.Text = openedFile;
//                                lvValidationMessages.Items.Add(lvi);
//                                lvi = new ListViewItem("");
//                                lvValidationMessages.Items.Add(lvi);
//                                result = opcHandle.loadXML(ofdXMLFile.FileName, tvItems, out IsXmlValid);
//                                if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
//                                else
//                                {
//                                    pnlValidationMessages.Visible = true;
//                                    pnlValidationMessages.BringToFront();
//                                }
//                                if (result == -1) errMsg = "File doesnot exist!!!";
//                                else if (result == -2) errMsg = "File is not a well-formed XML!!!";
//                                else if (result == -3) errMsg = "XSD file is not valid!!!";
//                                else if (result == -4) errMsg = "File is not a valid XML, as per the schema!!!";
//                                else if (result == -5) errMsg = "MODBUS Slave Port is Not Valid as per the schema";
//                                lvi = new ListViewItem("");
//                                lvValidationMessages.Items.Add(lvi);
//                                lvi = new ListViewItem(errMsg);
//                                lvValidationMessages.Items.Add(lvi);
//                                lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
//                                if (result < 0)
//                                {
//                                    hideLoading();
//                                    MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                                    return;
//                                }
//                                xmlFile = ofdXMLFile.FileName; //Assign only after loading file...
//                                                               //Namrata: 19/12/2017
//                                                               //Ajay: 29/11/2018
//                                TreeNode searchNode = tvItems.Nodes[0].Nodes["MasterConfiguration"];
//                                if (searchNode != null)
//                                    searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
//                                tvItems.SelectedNode = tvItems.Nodes[0];
//                                tvItems.Nodes[0].EnsureVisible();
//                                hideLoading();
//                            }
//                            else
//                            {
//                                string[] subdirectoryEntries = System.IO.Directory.GetDirectories(GetProtocol);
//                                foreach (string subdirectory in subdirectoryEntries)
//                                {
//                                    //Namrata: 01/11/2019
//                                    if (subdirectory.Contains("protocol"))
//                                    {
//                                        int index = subdirectory.IndexOf("protocol");
//                                        GetProtocolFolder = subdirectory.Substring(subdirectory.IndexOf("protocol") + 9);
//                                    }

//                                    GDSlave.SubDirName = extractPath + "\\" + GetProtocolFolder;
//                                    string CopyDir = extractPath + "\\" + GetProtocolFolder;

//                                    if (System.IO.Directory.Exists(GDSlave.SubDirName))
//                                    {
//                                        System.IO.Directory.Delete(GDSlave.SubDirName, true);
//                                        var diSource = new DirectoryInfo(GetProtocol + "\\" + GetProtocolFolder);
//                                        var diTarget = new DirectoryInfo(CopyDir);
//                                        CopyAll(diSource, diTarget);
//                                    }
//                                    else
//                                    {
//                                        var diSource1 = new DirectoryInfo(GetProtocol + "\\" + GetProtocolFolder);
//                                        var diTarget1 = new DirectoryInfo(CopyDir);
//                                        CopyAll(diSource1, diTarget1);
//                                    }
//                                }

//                                if (System.IO.Directory.Exists(GetXMLPath))
//                                {
//                                    System.IO.Directory.Delete(GetXMLPath, true);
//                                }
//                                //#endregion Namrata
//                                //Ajay: 11/01/2019
//                                if (!CheckVersions(ofdXMLFile.FileName))
//                                {
//                                    VersionMatch = false;
//                                    DialogResult rslt = MessageBox.Show("Do you still want to continue with mismatched versions of OpenPro+Configurator, xml and xsd files?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
//                                    if (rslt == DialogResult.No)
//                                    {
//                                        return;
//                                    }
//                                    else { }
//                                }
//                                else
//                                {
//                                    VersionMatch = true; //Ajay: 11/01/2019
//                                }
//                                //Namrata:26/04/2018
//                                Utils.XMLFolderPath = Path.GetDirectoryName(ofdXMLFile.FileName);

//                                #region IEC61850
//                                //Namrata: 27/04/2018
//                                ICDFilesData.DirectoryName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
//                                ICDFilesData.XmlName = Path.GetFileName(ofdXMLFile.FileName); // Get XML Name
//                                ICDFilesData.XmlPath = ofdXMLFile.FileName; // XML with full path
//                                ICDFilesData.XmlWithoutExt = "";// Path.GetFileNameWithoutExtension(Utils.XMLFileFullPath);
//                                                                //Namrata: 29/03/2018
//                                #region ClearDatasets
//                                Utils.dsIED.Clear();
//                                Utils.dsIED.Tables.Clear();
//                                Utils.DsRCB.Clear();
//                                Utils.DsRCBData.Clear();
//                                Utils.dsResponseType.Clear();
//                                Utils.DsResponseType.Clear();
//                                Utils.dsIED.Clear();
//                                Utils.dsIEDName.Clear();
//                                Utils.DsAllConfigurationData.Clear();
//                                Utils.DsAllConfigureData.Clear();
//                                Utils.DsRCBDataset.Clear();
//                                Utils.DsRCBDS.Clear();
//                                Utils.DtRCBdata.Clear();
//                                #endregion ClearDatasets
//                                #endregion IEC61850

//                                #region GDisplay
//                                //Namrata: 27/04/2018
//                                GDSlave.DirName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
//                                GDSlave.SLDName = Path.GetFileName(ofdXMLFile.FileName); //Get XML Name
//                                GDSlave.SLDPath = ofdXMLFile.FileName;//XML with full path
//                                GDSlave.SLDWithoutExt = "";
//                                #region ClearDatasets
//                                GDSlave.DsExcelData.Tables.Clear();
//                                GDSlave.DsExcelData.Clear();
//                                GDSlave.DtExcelData.Clear();
//                                GDSlave.DsExportData.Tables.Clear();
//                                GDSlave.DTAIImage.Clear();
//                                GDSlave.DTDIDOImage.Clear();
//                                #endregion ClearDatasets
//                                #endregion GDisplay
//                                this.MyMruList.AddFile(openedFile); //Now give it to the MRUManager
//                                int result = 0;
//                                string errMsg = "XML file is valid...";
//                                ResetConfiguratorState(false);
//                                showLoading();
//                                xmlFile = "";//reset old filename...
//                                pnlValidationMessages.Visible = true;
//                                ListViewItem lvi;
//                                lvi = new ListViewItem("Validating file: " + ofdXMLFile.FileName);
//                                //Namrata: 27/7/2017
//                                toolStripStatusLabel1.Visible = true;  //Display File Name in Toolstrip 
//                                tspFileName.Text = openedFile +@"\"+ ICDFilesData.XmlName;//ofdXMLFile.FileName;
//                                lvValidationMessages.Items.Add(lvi);
//                                lvi = new ListViewItem("");
//                                lvValidationMessages.Items.Add(lvi);
//                                result = opcHandle.loadXML(ofdXMLFile.FileName, tvItems, out IsXmlValid);
//                                if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
//                                else
//                                {
//                                    pnlValidationMessages.Visible = true;
//                                    pnlValidationMessages.BringToFront();
//                                }
//                                if (result == -1) errMsg = "File doesnot exist!!!";
//                                else if (result == -2) errMsg = "File is not a well-formed XML!!!";
//                                else if (result == -3) errMsg = "XSD file is not valid!!!";
//                                else if (result == -4) errMsg = "File is not a valid XML, as per the schema!!!";
//                                else if (result == -5) errMsg = "MODBUS Slave Port is Not Valid as per the schema";
//                                lvi = new ListViewItem("");
//                                lvValidationMessages.Items.Add(lvi);
//                                lvi = new ListViewItem(errMsg);
//                                lvValidationMessages.Items.Add(lvi);
//                                lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
//                                if (result < 0)
//                                {
//                                    hideLoading();
//                                    MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                                    return;
//                                }
//                                xmlFile = ofdXMLFile.FileName; //Assign only after loading file...
//                                                               //Namrata: 19/12/2017
//                                                               //Ajay: 29/11/2018
//                                TreeNode searchNode = tvItems.Nodes[0].Nodes["MasterConfiguration"];
//                                if (searchNode != null)
//                                    searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
//                                tvItems.SelectedNode = tvItems.Nodes[0];
//                                tvItems.Nodes[0].EnsureVisible();
//                                hideLoading();
//                            }
//                        }
//                        else
//                        {
//                            //#endregion Namrata
//                            //Ajay: 11/01/2019
//                            if (!CheckVersions(ofdXMLFile.FileName))
//                            {
//                                VersionMatch = false;
//                                DialogResult rslt = MessageBox.Show("Do you still want to continue with mismatched versions of OpenPro+Configurator, xml and xsd files?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
//                                if (rslt == DialogResult.No)
//                                {
//                                    return;
//                                }
//                                else { }
//                            }
//                            else
//                            {
//                                VersionMatch = true; //Ajay: 11/01/2019
//                            }
//                            //Namrata:26/04/2018
//                            Utils.XMLFolderPath = Path.GetDirectoryName(ofdXMLFile.FileName);

//                            #region IEC61850
//                            //Namrata: 27/04/2018
//                            ICDFilesData.DirectoryName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
//                            ICDFilesData.XmlName = Path.GetFileName(ofdXMLFile.FileName); // Get XML Name
//                            ICDFilesData.XmlPath = ofdXMLFile.FileName; // XML with full path
//                            ICDFilesData.XmlWithoutExt = "";// Path.GetFileNameWithoutExtension(Utils.XMLFileFullPath);
//                                                            //Namrata: 29/03/2018
//                            #region ClearDatasets
//                            Utils.dsIED.Clear();
//                            Utils.dsIED.Tables.Clear();
//                            Utils.DsRCB.Clear();
//                            Utils.DsRCBData.Clear();
//                            Utils.dsResponseType.Clear();
//                            Utils.DsResponseType.Clear();
//                            Utils.dsIED.Clear();
//                            Utils.dsIEDName.Clear();
//                            Utils.DsAllConfigurationData.Clear();
//                            Utils.DsAllConfigureData.Clear();
//                            Utils.DsRCBDataset.Clear();
//                            Utils.DsRCBDS.Clear();
//                            Utils.DtRCBdata.Clear();
//                            #endregion ClearDatasets
//                            #endregion IEC61850

//                            #region GDisplay
//                            //Namrata: 27/04/2018
//                            GDSlave.DirName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
//                            GDSlave.SLDName = Path.GetFileName(ofdXMLFile.FileName); //Get XML Name
//                            GDSlave.SLDPath = ofdXMLFile.FileName;//XML with full path
//                            GDSlave.SLDWithoutExt = "";
//                            #region ClearDatasets
//                            GDSlave.DsExcelData.Tables.Clear();
//                            GDSlave.DsExcelData.Clear();
//                            GDSlave.DtExcelData.Clear();
//                            GDSlave.DsExportData.Tables.Clear();
//                            GDSlave.DTAIImage.Clear();
//                            GDSlave.DTDIDOImage.Clear();
//                            #endregion ClearDatasets
//                            #endregion GDisplay
//                            this.MyMruList.AddFile(openedFile); //Now give it to the MRUManager
//                            int result = 0;
//                            string errMsg = "XML file is valid...";
//                            ResetConfiguratorState(false);
//                            showLoading();
//                            xmlFile = "";//reset old filename...
//                            pnlValidationMessages.Visible = true;
//                            ListViewItem lvi;
//                            lvi = new ListViewItem("Validating file: " + ofdXMLFile.FileName);
//                            //Namrata: 27/7/2017
//                            toolStripStatusLabel1.Visible = true;  //Display File Name in Toolstrip 
//                            tspFileName.Text = ofdXMLFile.FileName;
//                            lvValidationMessages.Items.Add(lvi);
//                            lvi = new ListViewItem("");
//                            lvValidationMessages.Items.Add(lvi);
//                            result = opcHandle.loadXML(ofdXMLFile.FileName, tvItems, out IsXmlValid);
//                            if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
//                            else
//                            {
//                                pnlValidationMessages.Visible = true;
//                                pnlValidationMessages.BringToFront();
//                            }
//                            if (result == -1) errMsg = "File doesnot exist!!!";
//                            else if (result == -2) errMsg = "File is not a well-formed XML!!!";
//                            else if (result == -3) errMsg = "XSD file is not valid!!!";
//                            else if (result == -4) errMsg = "File is not a valid XML, as per the schema!!!";
//                            else if (result == -5) errMsg = "MODBUS Slave Port is Not Valid as per the schema";
//                            lvi = new ListViewItem("");
//                            lvValidationMessages.Items.Add(lvi);
//                            lvi = new ListViewItem(errMsg);
//                            lvValidationMessages.Items.Add(lvi);
//                            lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
//                            if (result < 0)
//                            {
//                                hideLoading();
//                                MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                                return;
//                            }
//                            xmlFile = ofdXMLFile.FileName; //Assign only after loading file...
//                                                           //Namrata: 19/12/2017
//                                                           //Ajay: 29/11/2018
//                            TreeNode searchNode = tvItems.Nodes[0].Nodes["MasterConfiguration"];
//                            if (searchNode != null)
//                                searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
//                            tvItems.SelectedNode = tvItems.Nodes[0];
//                            tvItems.Nodes[0].EnsureVisible();
//                            hideLoading();
//                        }
//                    }
//                    else { }

//                    #endregion
//                }
//                //Ajay: 29/11/2018
//                else if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
//                {
//                    #region AppMode=Restricted

//                    string openedFile = ProtocolGateway.OppConfigurationFile;
//                    Utils.XMLFolderPath = Path.GetDirectoryName(ProtocolGateway.OppConfigurationFile);
//                    string DirectoryExtention = Path.GetExtension(openedFile);

//                     #region Namrata
//                    //Namrata: 23/10/2019
//                    if (DirectoryExtention == ".zip")
//                    {
//                        string extractPath = System.IO.Path.GetTempPath() + "protocol";//Path.GetDirectoryName(openedFile);
//                        string ZIPName = Path.GetDirectoryName(openedFile);
//                        string ZipFolderName = Path.GetFileNameWithoutExtension(openedFile);
//                        string GetXMLPath = Path.Combine(ZIPName, ZipFolderName);

//                        Utils.DirNameSave = ZIPName + @"\" + ZipFolderName;
//                        if (System.IO.Directory.Exists(Utils.DirNameSave))
//                        {
//                            System.IO.Directory.Delete(Utils.DirNameSave, true);
//                        }
//                        if (!System.IO.Directory.Exists(extractPath))
//                        {
//                            System.IO.Directory.CreateDirectory(extractPath);
//                            ZipFile.ExtractToDirectory(openedFile, GetXMLPath);
//                        }
//                        else
//                        {
//                            System.IO.Directory.Delete(extractPath, true);
//                            System.IO.Directory.CreateDirectory(extractPath);
//                            ZipFile.ExtractToDirectory(openedFile, GetXMLPath);
//                        }
//                        string[] fileEntries1 = System.IO.Directory.GetFiles(GetXMLPath, "*.xml");

//                        string fileName1 = string.Empty;
//                        string destFile1 = string.Empty;
//                        foreach (string s in fileEntries1)
//                        {
//                            fileName1 = System.IO.Path.GetFileName(s);
//                            destFile1 = System.IO.Path.Combine(extractPath, fileName1);
//                            System.IO.File.Move(s, destFile1);
//                            ofdXMLFile.FileName = destFile1;
//                        }
//                        string Protocol = "protocol";
//                        string GetProtocol = Path.Combine(GetXMLPath, Protocol);
//                        if (!System.IO.Directory.Exists(GetProtocol))
//                        {
//                            if (!CheckVersions(GetXMLPath))
//                            {
//                                VersionMatch = false;
//                                DialogResult rslt = MessageBox.Show("Do you still want to continue with mismatched versions of OpenPro+Configurator, xml and xsd files?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
//                                if (rslt == DialogResult.No)
//                                {
//                                    return;
//                                }
//                                else { }
//                            }
//                            else
//                            {
//                                VersionMatch = true; //Ajay: 11/01/2019
//                            }
//                            //Namrata:26/04/2018
//                            Utils.XMLFolderPath = Path.GetDirectoryName(GetProtocol);

//                            #region IEC61850
//                            //Namrata: 27/04/2018
//                            ICDFilesData.DirectoryName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
//                            ICDFilesData.XmlName = Path.GetFileName(ofdXMLFile.FileName); // Get XML Name
//                            ICDFilesData.XmlPath = ofdXMLFile.FileName; // XML with full path
//                            ICDFilesData.XmlWithoutExt = "";

//                            #region ClearDatasets
//                            Utils.dsIED.Clear();
//                            Utils.dsIED.Tables.Clear();
//                            Utils.DsRCB.Clear();
//                            Utils.DsRCBData.Clear();
//                            Utils.dsResponseType.Clear();
//                            Utils.DsResponseType.Clear();
//                            Utils.dsIED.Clear();
//                            Utils.dsIEDName.Clear();
//                            Utils.DsAllConfigurationData.Clear();
//                            Utils.DsAllConfigureData.Clear();
//                            Utils.DsRCBDataset.Clear();
//                            Utils.DsRCBDS.Clear();
//                            Utils.DtRCBdata.Clear();
//                            #endregion ClearDatasets
//                            #endregion IEC61850

//                            #region GDisplay
//                            //Namrata: 27/04/2018
//                            GDSlave.DirName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
//                            GDSlave.SLDName = Path.GetFileName(ofdXMLFile.FileName); //Get XML Name
//                            GDSlave.SLDPath = ofdXMLFile.FileName;//XML with full path
//                            GDSlave.SLDWithoutExt = "";
//                            #region ClearDatasets
//                            GDSlave.DsExcelData.Tables.Clear();
//                            GDSlave.DsExcelData.Clear();
//                            GDSlave.DtExcelData.Clear();
//                            GDSlave.DsExportData.Tables.Clear();
//                            GDSlave.DTAIImage.Clear();
//                            GDSlave.DTDIDOImage.Clear();
//                            #endregion ClearDatasets
//                            #endregion GDisplay
//                            this.MyMruList.AddFile(ofdXMLFile.FileName); //Now give it to the MRUManager
//                            int result = 0;
//                            string errMsg = "XML file is valid...";
//                            ResetConfiguratorState(false);
//                            showLoading();
//                            xmlFile = "";//reset old filename...
//                            pnlValidationMessages.Visible = true;
//                            ListViewItem lvi;
//                            lvi = new ListViewItem("Validating file: " + ofdXMLFile.FileName);
//                            //Namrata: 27/7/2017
//                            toolStripStatusLabel1.Visible = true;  //Display File Name in Toolstrip 
//                            tspFileName.Text = openedFile;
//                            lvValidationMessages.Items.Add(lvi);
//                            lvi = new ListViewItem("");
//                            lvValidationMessages.Items.Add(lvi);
//                            result = opcHandle.loadXML(ofdXMLFile.FileName, tvItems, out IsXmlValid);
//                            if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
//                            else
//                            {
//                                pnlValidationMessages.Visible = true;
//                                pnlValidationMessages.BringToFront();
//                            }
//                            if (result == -1) errMsg = "File doesnot exist!!!";
//                            else if (result == -2) errMsg = "File is not a well-formed XML!!!";
//                            else if (result == -3) errMsg = "XSD file is not valid!!!";
//                            else if (result == -4) errMsg = "File is not a valid XML, as per the schema!!!";
//                            else if (result == -5) errMsg = "MODBUS Slave Port is Not Valid as per the schema";
//                            lvi = new ListViewItem("");
//                            lvValidationMessages.Items.Add(lvi);
//                            lvi = new ListViewItem(errMsg);
//                            lvValidationMessages.Items.Add(lvi);
//                            lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
//                            if (result < 0)
//                            {
//                                hideLoading();
//                                MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                                return;
//                            }
//                            xmlFile = ofdXMLFile.FileName; //Assign only after loading file...
//                                                           //Namrata: 19/12/2017
//                                                           //Ajay: 29/11/2018
//                            TreeNode searchNode = tvItems.Nodes[0].Nodes["MasterConfiguration"];
//                            if (searchNode != null)
//                                searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
//                            tvItems.SelectedNode = tvItems.Nodes[0];
//                            tvItems.Nodes[0].EnsureVisible();
//                            hideLoading();
//                        }
//                        else
//                        {
//                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(GetProtocol);
//                            foreach (string subdirectory in subdirectoryEntries)
//                            {
//                                //Namrata: 01/11/2019
//                                if (subdirectory.Contains("protocol"))
//                                {
//                                    int index = subdirectory.IndexOf("protocol");
//                                    GetProtocolFolder = subdirectory.Substring(subdirectory.IndexOf("protocol") + 9);
//                                }

//                                GDSlave.SubDirName = extractPath + "\\" + GetProtocolFolder;
//                                string CopyDir = extractPath + "\\" + GetProtocolFolder;

//                                if (System.IO.Directory.Exists(GDSlave.SubDirName))
//                                {
//                                    System.IO.Directory.Delete(GDSlave.SubDirName, true);
//                                    var diSource = new DirectoryInfo(GetProtocol + "\\" + GetProtocolFolder);
//                                    var diTarget = new DirectoryInfo(CopyDir);
//                                    CopyAll(diSource, diTarget);
//                                }
//                                else
//                                {
//                                    var diSource1 = new DirectoryInfo(GetProtocol + "\\" + GetProtocolFolder);
//                                    var diTarget1 = new DirectoryInfo(CopyDir);
//                                    CopyAll(diSource1, diTarget1);
//                                }
//                            }

//                            if (System.IO.Directory.Exists(GetXMLPath))
//                            {
//                                System.IO.Directory.Delete(GetXMLPath, true);
//                            }
//                            //#endregion Namrata
//                            //Ajay: 11/01/2019
//                            if (!CheckVersions(ofdXMLFile.FileName))
//                            {
//                                VersionMatch = false;
//                                DialogResult rslt = MessageBox.Show("Do you still want to continue with mismatched versions of OpenPro+Configurator, xml and xsd files?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
//                                if (rslt == DialogResult.No)
//                                {
//                                    return;
//                                }
//                                else { }
//                            }
//                            else
//                            {
//                                VersionMatch = true; //Ajay: 11/01/2019
//                            }
//                            //Namrata:26/04/2018
//                            Utils.XMLFolderPath = Path.GetDirectoryName(ofdXMLFile.FileName);

//                            #region IEC61850
//                            //Namrata: 27/04/2018
//                            ICDFilesData.DirectoryName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
//                            ICDFilesData.XmlName = Path.GetFileName(ofdXMLFile.FileName); // Get XML Name
//                            ICDFilesData.XmlPath = ofdXMLFile.FileName; // XML with full path
//                            ICDFilesData.XmlWithoutExt = "";// Path.GetFileNameWithoutExtension(Utils.XMLFileFullPath);
//                                                            //Namrata: 29/03/2018
//                            #region ClearDatasets
//                            Utils.dsIED.Clear();
//                            Utils.dsIED.Tables.Clear();
//                            Utils.DsRCB.Clear();
//                            Utils.DsRCBData.Clear();
//                            Utils.dsResponseType.Clear();
//                            Utils.DsResponseType.Clear();
//                            Utils.dsIED.Clear();
//                            Utils.dsIEDName.Clear();
//                            Utils.DsAllConfigurationData.Clear();
//                            Utils.DsAllConfigureData.Clear();
//                            Utils.DsRCBDataset.Clear();
//                            Utils.DsRCBDS.Clear();
//                            Utils.DtRCBdata.Clear();
//                            #endregion ClearDatasets
//                            #endregion IEC61850

//                            #region GDisplay
//                            //Namrata: 27/04/2018
//                            GDSlave.DirName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
//                            GDSlave.SLDName = Path.GetFileName(ofdXMLFile.FileName); //Get XML Name
//                            GDSlave.SLDPath = ofdXMLFile.FileName;//XML with full path
//                            GDSlave.SLDWithoutExt = "";
//                            #region ClearDatasets
//                            GDSlave.DsExcelData.Tables.Clear();
//                            GDSlave.DsExcelData.Clear();
//                            GDSlave.DtExcelData.Clear();
//                            GDSlave.DsExportData.Tables.Clear();
//                            GDSlave.DTAIImage.Clear();
//                            GDSlave.DTDIDOImage.Clear();
//                            #endregion ClearDatasets
//                            #endregion GDisplay
//                            //this.MyMruList.AddFile(openedFile); //Now give it to the MRUManager
//                            int result = 0;
//                            string errMsg = "XML file is valid...";
//                            ResetConfiguratorState(false);
//                            showLoading();
//                            xmlFile = "";//reset old filename...
//                            pnlValidationMessages.Visible = true;
//                            ListViewItem lvi;
//                            lvi = new ListViewItem("Validating file: " + ofdXMLFile.FileName);
//                            //Namrata: 27/7/2017
//                            toolStripStatusLabel1.Visible = true;  //Display File Name in Toolstrip 
//                            tspFileName.Text = openedFile + @"\" + ICDFilesData.XmlName;//ofdXMLFile.FileName;
//                            lvValidationMessages.Items.Add(lvi);
//                            lvi = new ListViewItem("");
//                            lvValidationMessages.Items.Add(lvi);
//                            result = opcHandle.loadXML(ofdXMLFile.FileName, tvItems, out IsXmlValid);
//                            if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
//                            else
//                            {
//                                pnlValidationMessages.Visible = true;
//                                pnlValidationMessages.BringToFront();
//                            }
//                            if (result == -1) errMsg = "File doesnot exist!!!";
//                            else if (result == -2) errMsg = "File is not a well-formed XML!!!";
//                            else if (result == -3) errMsg = "XSD file is not valid!!!";
//                            else if (result == -4) errMsg = "File is not a valid XML, as per the schema!!!";
//                            else if (result == -5) errMsg = "MODBUS Slave Port is Not Valid as per the schema";
//                            lvi = new ListViewItem("");
//                            lvValidationMessages.Items.Add(lvi);
//                            lvi = new ListViewItem(errMsg);
//                            lvValidationMessages.Items.Add(lvi);
//                            lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
//                            if (result < 0)
//                            {
//                                hideLoading();
//                                MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                                return;
//                            }
//                            xmlFile = ofdXMLFile.FileName; //Assign only after loading file...
//                                                           //Namrata: 19/12/2017
//                                                           //Ajay: 29/11/2018
//                            TreeNode searchNode = tvItems.Nodes[0].Nodes["MasterConfiguration"];
//                            if (searchNode != null)
//                                searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
//                            tvItems.SelectedNode = tvItems.Nodes[0];
//                            tvItems.Nodes[0].EnsureVisible();
//                            hideLoading();
//                        }
//                    }
//                    #endregion

//                    else
//                    {

//                        #region IEC61850
//                        ICDFilesData.DirectoryName = Path.GetDirectoryName(ProtocolGateway.OppConfigurationFile); //Get DirectoryName
//                        ICDFilesData.XmlName = Path.GetFileName(ProtocolGateway.OppConfigurationFile); // Get XML Name
//                        ICDFilesData.XmlPath = ProtocolGateway.OppConfigurationFile; // XML with full path
//                        ICDFilesData.XmlWithoutExt = "";// Path.GetFileNameWithoutExtension(Utils.XMLFileFullPath);
//                        #region ClearDatasets
//                        Utils.dsIED.Clear();
//                        Utils.dsIED.Tables.Clear();
//                        Utils.DsRCB.Clear();
//                        Utils.DsRCBData.Clear();
//                        Utils.dsResponseType.Clear();
//                        Utils.DsResponseType.Clear();
//                        Utils.dsIED.Clear();
//                        Utils.dsIEDName.Clear();
//                        Utils.DsAllConfigurationData.Clear();
//                        Utils.DsAllConfigureData.Clear();
//                        Utils.DsRCBDataset.Clear();
//                        Utils.DsRCBDS.Clear();
//                        Utils.DtRCBdata.Clear();
//                        #endregion ClearDatasets
//                        #endregion IEC61850

//                        #region GDisplay
//                        //Namrata: 27/04/2018
//                        GDSlave.DirName = Path.GetDirectoryName(ProtocolGateway.OppConfigurationFile); //Get DirectoryName
//                        GDSlave.SLDName = Path.GetFileName(ProtocolGateway.OppConfigurationFile); //Get XML Name
//                        GDSlave.SLDPath = ProtocolGateway.OppConfigurationFile;//XML with full path
//                        GDSlave.SLDWithoutExt = "";
//                        #region ClearDatasets
//                        GDSlave.DsExcelData.Tables.Clear();
//                        GDSlave.DtExcelData.Clear();
//                        GDSlave.DsExportData.Tables.Clear();
//                        GDSlave.DTAIImage.Clear();
//                        GDSlave.DTDIDOImage.Clear();
//                        #endregion ClearDatasets
//                        #endregion GDisplay
//                        int result = 0;
//                        string errMsg = "XML file is valid...";
//                        ResetConfiguratorState(false);
//                        showLoading();
//                        xmlFile = "";//reset old filename...
//                        pnlValidationMessages.Visible = true;
//                        ListViewItem lvi;
//                        lvi = new ListViewItem("Validating file: " + ProtocolGateway.OppConfigurationFile);
//                        toolStripStatusLabel1.Visible = true;  //Display File Name in Toolstrip 
//                        tspFileName.Text = ProtocolGateway.OppConfigurationFile;
//                        lvValidationMessages.Items.Add(lvi);
//                        lvi = new ListViewItem("");
//                        lvValidationMessages.Items.Add(lvi);
//                        result = opcHandle.loadXML(ProtocolGateway.OppConfigurationFile, tvItems, out IsXmlValid);
//                        if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
//                        else
//                        {
//                            pnlValidationMessages.Visible = true;
//                            pnlValidationMessages.BringToFront();
//                        }
//                        if (result == -1) errMsg = "File does not exist!";
//                        else if (result == -2) errMsg = "File is not a well-formed XML!";
//                        else if (result == -3) errMsg = "XSD file is not valid!";
//                        else if (result == -4) errMsg = "File is not a valid XML, as per the schema!";
//                        else if (result == -5) errMsg = "MODBUS Slave Port is Not Valid as per the schema!";
//                        lvi = new ListViewItem("");
//                        lvValidationMessages.Items.Add(lvi);
//                        lvi = new ListViewItem(errMsg);
//                        lvValidationMessages.Items.Add(lvi);
//                        lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
//                        if (result < 0)
//                        {
//                            hideLoading();
//                            MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                            return;
//                        }
//                        xmlFile = ProtocolGateway.OppConfigurationFile;//Assign only after loading file...
//                                                                       //Ajay: 29/11/2018
//                                                                       //TreeNode searchNode = tvItems.Nodes[0].Nodes[5];
//                        TreeNode searchNode = tvItems.Nodes[0].Nodes["MasterConfiguration"];
//                        if (searchNode != null)
//                            searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
//                        tvItems.SelectedNode = tvItems.Nodes[0];
//                        tvItems.Nodes[0].EnsureVisible();
//                        hideLoading();
//                    }
//                    #endregion
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void openXMLFile1()
//        {
//            string strRoutineName = "frmOpenProPlus: openXMLFile";
//            try
//            {
//                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Full)
//                {
//                    ofdXMLFile.Filter = "XML Files| *.xml|Zip Files|*.zip;*.rar"; //"XML Files|*.xml";
//                    ofdXMLFile.FilterIndex = 1;
//                    ofdXMLFile.RestoreDirectory = true;
//                    ofdXMLFile.Title = "Browse ZIP File";//"Browse XML File";
//                    if (ofdXMLFile.ShowDialog() == DialogResult.OK)
//                    {
//                        string openedFile = ofdXMLFile.FileName; //Namrata: 18/11/2017
//                        string DirectoryExtention = Path.GetExtension(ofdXMLFile.FileName);

//                        // #region Namrata
//                        //Namrata: 23/10/2019
//                        if (DirectoryExtention == ".zip")
//                        {
//                            string extractPath = System.IO.Path.GetTempPath() + "protocol";//Path.GetDirectoryName(openedFile);
//                            string ZIPName = Path.GetDirectoryName(openedFile);
//                            string ZipFolderName = Path.GetFileNameWithoutExtension(openedFile);
//                            string GetXMLPath = Path.Combine(ZIPName, ZipFolderName);
//                            if (!System.IO.Directory.Exists(extractPath))
//                            {
//                                System.IO.Directory.CreateDirectory(extractPath);
//                                ZipFile.ExtractToDirectory(openedFile, GetXMLPath);
//                            }
//                            else
//                            {
//                                System.IO.Directory.Delete(extractPath,true);
//                                System.IO.Directory.CreateDirectory(extractPath);
//                                ZipFile.ExtractToDirectory(openedFile, GetXMLPath);
//                            }
//                            string[] fileEntries1 = System.IO.Directory.GetFiles(GetXMLPath, "*.xml");

//                            string fileName1 = string.Empty;
//                            string destFile1 = string.Empty;
//                            foreach (string s in fileEntries1)
//                            {
//                                fileName1 = System.IO.Path.GetFileName(s);
//                                destFile1 = System.IO.Path.Combine(extractPath, fileName1);
//                                System.IO.File.Move(s, destFile1);
//                                ofdXMLFile.FileName = destFile1;
//                            }
//                            string Protocol = "protocol";
//                            string GetProtocol = Path.Combine(GetXMLPath, Protocol);
//                            if (!System.IO.Directory.Exists(GetProtocol))
//                            {
//                                if (!CheckVersions(ofdXMLFile.FileName))
//                                {
//                                    VersionMatch = false;
//                                    DialogResult rslt = MessageBox.Show("Do you still want to continue with mismatched versions of OpenPro+Configurator, xml and xsd files?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
//                                    if (rslt == DialogResult.No)
//                                    {
//                                        return;
//                                    }
//                                    else { }
//                                }
//                                else
//                                {
//                                    VersionMatch = true; //Ajay: 11/01/2019
//                                }
//                                //Namrata:26/04/2018
//                                Utils.XMLFolderPath = Path.GetDirectoryName(ofdXMLFile.FileName);

//                                #region IEC61850
//                                //Namrata: 27/04/2018
//                                ICDFilesData.DirectoryName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
//                                ICDFilesData.XmlName = Path.GetFileName(ofdXMLFile.FileName); // Get XML Name
//                                ICDFilesData.XmlPath = ofdXMLFile.FileName; // XML with full path
//                                ICDFilesData.XmlWithoutExt = "";

//                                #region ClearDatasets
//                                Utils.dsIED.Clear();
//                                Utils.dsIED.Tables.Clear();
//                                Utils.DsRCB.Clear();
//                                Utils.DsRCBData.Clear();
//                                Utils.dsResponseType.Clear();
//                                Utils.DsResponseType.Clear();
//                                Utils.dsIED.Clear();
//                                Utils.dsIEDName.Clear();
//                                Utils.DsAllConfigurationData.Clear();
//                                Utils.DsAllConfigureData.Clear();
//                                Utils.DsRCBDataset.Clear();
//                                Utils.DsRCBDS.Clear();
//                                Utils.DtRCBdata.Clear();
//                                #endregion ClearDatasets
//                                #endregion IEC61850

//                                #region GDisplay
//                                //Namrata: 27/04/2018
//                                GDSlave.DirName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
//                                GDSlave.SLDName = Path.GetFileName(ofdXMLFile.FileName); //Get XML Name
//                                GDSlave.SLDPath = ofdXMLFile.FileName;//XML with full path
//                                GDSlave.SLDWithoutExt = "";
//                                #region ClearDatasets
//                                GDSlave.DsExcelData.Tables.Clear();
//                                GDSlave.DsExcelData.Clear();
//                                GDSlave.DtExcelData.Clear();
//                                GDSlave.DsExportData.Tables.Clear();
//                                GDSlave.DTAIImage.Clear();
//                                GDSlave.DTDIDOImage.Clear();
//                                #endregion ClearDatasets
//                                #endregion GDisplay
//                                this.MyMruList.AddFile(openedFile); //Now give it to the MRUManager
//                                int result = 0;
//                                string errMsg = "XML file is valid...";
//                                ResetConfiguratorState(false);
//                                showLoading();
//                                xmlFile = "";//reset old filename...
//                                pnlValidationMessages.Visible = true;
//                                ListViewItem lvi;
//                                lvi = new ListViewItem("Validating file: " + ofdXMLFile.FileName);
//                                //Namrata: 27/7/2017
//                                toolStripStatusLabel1.Visible = true;  //Display File Name in Toolstrip 
//                                tspFileName.Text = ofdXMLFile.FileName;
//                                lvValidationMessages.Items.Add(lvi);
//                                lvi = new ListViewItem("");
//                                lvValidationMessages.Items.Add(lvi);
//                                result = opcHandle.loadXML(ofdXMLFile.FileName, tvItems, out IsXmlValid);
//                                if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
//                                else
//                                {
//                                    pnlValidationMessages.Visible = true;
//                                    pnlValidationMessages.BringToFront();
//                                }
//                                if (result == -1) errMsg = "File doesnot exist!!!";
//                                else if (result == -2) errMsg = "File is not a well-formed XML!!!";
//                                else if (result == -3) errMsg = "XSD file is not valid!!!";
//                                else if (result == -4) errMsg = "File is not a valid XML, as per the schema!!!";
//                                else if (result == -5) errMsg = "MODBUS Slave Port is Not Valid as per the schema";
//                                lvi = new ListViewItem("");
//                                lvValidationMessages.Items.Add(lvi);
//                                lvi = new ListViewItem(errMsg);
//                                lvValidationMessages.Items.Add(lvi);
//                                lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
//                                if (result < 0)
//                                {
//                                    hideLoading();
//                                    MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                                    return;
//                                }
//                                xmlFile = ofdXMLFile.FileName; //Assign only after loading file...
//                                                               //Namrata: 19/12/2017
//                                                               //Ajay: 29/11/2018
//                                TreeNode searchNode = tvItems.Nodes[0].Nodes["MasterConfiguration"];
//                                if (searchNode != null)
//                                    searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
//                                tvItems.SelectedNode = tvItems.Nodes[0];
//                                tvItems.Nodes[0].EnsureVisible();
//                                hideLoading();
//                            }
//                            else
//                            {
//                                string[] subdirectoryEntries = System.IO.Directory.GetDirectories(GetProtocol);
//                                foreach (string subdirectory in subdirectoryEntries)
//                                {
//                                    //Namrata: 01/11/2019
//                                    if (subdirectory.Contains("protocol"))
//                                    {
//                                        int index = subdirectory.IndexOf("protocol");
//                                        GetProtocolFolder = subdirectory.Substring(subdirectory.IndexOf("protocol") + 9);
//                                    }

//                                    GDSlave.SubDirName = extractPath + "\\" + GetProtocolFolder;
//                                    string CopyDir = extractPath + "\\" + GetProtocolFolder;

//                                    if (System.IO.Directory.Exists(GDSlave.SubDirName))
//                                    {
//                                        System.IO.Directory.Delete(GDSlave.SubDirName, true);
//                                        var diSource = new DirectoryInfo(GetProtocol + "\\" + GetProtocolFolder);
//                                        var diTarget = new DirectoryInfo(CopyDir);
//                                        CopyAll(diSource, diTarget);
//                                    }
//                                    else
//                                    {
//                                        var diSource1 = new DirectoryInfo(GetProtocol + "\\" + GetProtocolFolder);
//                                        var diTarget1 = new DirectoryInfo(CopyDir);
//                                        CopyAll(diSource1, diTarget1);
//                                    }
//                                }

//                                if (System.IO.Directory.Exists(GetXMLPath))
//                                {
//                                    System.IO.Directory.Delete(GetXMLPath, true);
//                                }
//                                //#endregion Namrata
//                                //Ajay: 11/01/2019
//                                if (!CheckVersions(ofdXMLFile.FileName))
//                                {
//                                    VersionMatch = false;
//                                    DialogResult rslt = MessageBox.Show("Do you still want to continue with mismatched versions of OpenPro+Configurator, xml and xsd files?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
//                                    if (rslt == DialogResult.No)
//                                    {
//                                        return;
//                                    }
//                                    else { }
//                                }
//                                else
//                                {
//                                    VersionMatch = true; //Ajay: 11/01/2019
//                                }
//                                //Namrata:26/04/2018
//                                Utils.XMLFolderPath = Path.GetDirectoryName(ofdXMLFile.FileName);

//                                #region IEC61850
//                                //Namrata: 27/04/2018
//                                ICDFilesData.DirectoryName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
//                                ICDFilesData.XmlName = Path.GetFileName(ofdXMLFile.FileName); // Get XML Name
//                                ICDFilesData.XmlPath = ofdXMLFile.FileName; // XML with full path
//                                ICDFilesData.XmlWithoutExt = "";// Path.GetFileNameWithoutExtension(Utils.XMLFileFullPath);
//                                                                //Namrata: 29/03/2018
//                                #region ClearDatasets
//                                Utils.dsIED.Clear();
//                                Utils.dsIED.Tables.Clear();
//                                Utils.DsRCB.Clear();
//                                Utils.DsRCBData.Clear();
//                                Utils.dsResponseType.Clear();
//                                Utils.DsResponseType.Clear();
//                                Utils.dsIED.Clear();
//                                Utils.dsIEDName.Clear();
//                                Utils.DsAllConfigurationData.Clear();
//                                Utils.DsAllConfigureData.Clear();
//                                Utils.DsRCBDataset.Clear();
//                                Utils.DsRCBDS.Clear();
//                                Utils.DtRCBdata.Clear();
//                                #endregion ClearDatasets
//                                #endregion IEC61850

//                                #region GDisplay
//                                //Namrata: 27/04/2018
//                                GDSlave.DirName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
//                                GDSlave.SLDName = Path.GetFileName(ofdXMLFile.FileName); //Get XML Name
//                                GDSlave.SLDPath = ofdXMLFile.FileName;//XML with full path
//                                GDSlave.SLDWithoutExt = "";
//                                #region ClearDatasets
//                                GDSlave.DsExcelData.Tables.Clear();
//                                GDSlave.DsExcelData.Clear();
//                                GDSlave.DtExcelData.Clear();
//                                GDSlave.DsExportData.Tables.Clear();
//                                GDSlave.DTAIImage.Clear();
//                                GDSlave.DTDIDOImage.Clear();
//                                #endregion ClearDatasets
//                                #endregion GDisplay
//                                this.MyMruList.AddFile(openedFile); //Now give it to the MRUManager
//                                int result = 0;
//                                string errMsg = "XML file is valid...";
//                                ResetConfiguratorState(false);
//                                showLoading();
//                                xmlFile = "";//reset old filename...
//                                pnlValidationMessages.Visible = true;
//                                ListViewItem lvi;
//                                lvi = new ListViewItem("Validating file: " + ofdXMLFile.FileName);
//                                //Namrata: 27/7/2017
//                                toolStripStatusLabel1.Visible = true;  //Display File Name in Toolstrip 
//                                tspFileName.Text = ofdXMLFile.FileName;
//                                lvValidationMessages.Items.Add(lvi);
//                                lvi = new ListViewItem("");
//                                lvValidationMessages.Items.Add(lvi);
//                                result = opcHandle.loadXML(ofdXMLFile.FileName, tvItems, out IsXmlValid);
//                                if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
//                                else
//                                {
//                                    pnlValidationMessages.Visible = true;
//                                    pnlValidationMessages.BringToFront();
//                                }
//                                if (result == -1) errMsg = "File doesnot exist!!!";
//                                else if (result == -2) errMsg = "File is not a well-formed XML!!!";
//                                else if (result == -3) errMsg = "XSD file is not valid!!!";
//                                else if (result == -4) errMsg = "File is not a valid XML, as per the schema!!!";
//                                else if (result == -5) errMsg = "MODBUS Slave Port is Not Valid as per the schema";
//                                lvi = new ListViewItem("");
//                                lvValidationMessages.Items.Add(lvi);
//                                lvi = new ListViewItem(errMsg);
//                                lvValidationMessages.Items.Add(lvi);
//                                lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
//                                if (result < 0)
//                                {
//                                    hideLoading();
//                                    MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                                    return;
//                                }
//                                xmlFile = ofdXMLFile.FileName; //Assign only after loading file...
//                                                               //Namrata: 19/12/2017
//                                                               //Ajay: 29/11/2018
//                                TreeNode searchNode = tvItems.Nodes[0].Nodes["MasterConfiguration"];
//                                if (searchNode != null)
//                                    searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
//                                tvItems.SelectedNode = tvItems.Nodes[0];
//                                tvItems.Nodes[0].EnsureVisible();
//                                hideLoading();
//                            }
//                        }
//                        else
//                        {
//                            //#endregion Namrata
//                            //Ajay: 11/01/2019
//                            if (!CheckVersions(ofdXMLFile.FileName))
//                            {
//                                VersionMatch = false;
//                                DialogResult rslt = MessageBox.Show("Do you still want to continue with mismatched versions of OpenPro+Configurator, xml and xsd files?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
//                                if (rslt == DialogResult.No)
//                                {
//                                    return;
//                                }
//                                else { }
//                            }
//                            else
//                            {
//                                VersionMatch = true; //Ajay: 11/01/2019
//                            }
//                            //Namrata:26/04/2018
//                            Utils.XMLFolderPath = Path.GetDirectoryName(ofdXMLFile.FileName);

//                            #region IEC61850
//                            //Namrata: 27/04/2018
//                            ICDFilesData.DirectoryName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
//                            ICDFilesData.XmlName = Path.GetFileName(ofdXMLFile.FileName); // Get XML Name
//                            ICDFilesData.XmlPath = ofdXMLFile.FileName; // XML with full path
//                            ICDFilesData.XmlWithoutExt = "";// Path.GetFileNameWithoutExtension(Utils.XMLFileFullPath);
//                                                            //Namrata: 29/03/2018
//                            #region ClearDatasets
//                            Utils.dsIED.Clear();
//                            Utils.dsIED.Tables.Clear();
//                            Utils.DsRCB.Clear();
//                            Utils.DsRCBData.Clear();
//                            Utils.dsResponseType.Clear();
//                            Utils.DsResponseType.Clear();
//                            Utils.dsIED.Clear();
//                            Utils.dsIEDName.Clear();
//                            Utils.DsAllConfigurationData.Clear();
//                            Utils.DsAllConfigureData.Clear();
//                            Utils.DsRCBDataset.Clear();
//                            Utils.DsRCBDS.Clear();
//                            Utils.DtRCBdata.Clear();
//                            #endregion ClearDatasets
//                            #endregion IEC61850

//                            #region GDisplay
//                            //Namrata: 27/04/2018
//                            GDSlave.DirName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
//                            GDSlave.SLDName = Path.GetFileName(ofdXMLFile.FileName); //Get XML Name
//                            GDSlave.SLDPath = ofdXMLFile.FileName;//XML with full path
//                            GDSlave.SLDWithoutExt = "";
//                            #region ClearDatasets
//                            GDSlave.DsExcelData.Tables.Clear();
//                            GDSlave.DsExcelData.Clear();
//                            GDSlave.DtExcelData.Clear();
//                            GDSlave.DsExportData.Tables.Clear();
//                            GDSlave.DTAIImage.Clear();
//                            GDSlave.DTDIDOImage.Clear();
//                            #endregion ClearDatasets
//                            #endregion GDisplay
//                            this.MyMruList.AddFile(openedFile); //Now give it to the MRUManager
//                            int result = 0;
//                            string errMsg = "XML file is valid...";
//                            ResetConfiguratorState(false);
//                            showLoading();
//                            xmlFile = "";//reset old filename...
//                            pnlValidationMessages.Visible = true;
//                            ListViewItem lvi;
//                            lvi = new ListViewItem("Validating file: " + ofdXMLFile.FileName);
//                            //Namrata: 27/7/2017
//                            toolStripStatusLabel1.Visible = true;  //Display File Name in Toolstrip 
//                            tspFileName.Text = ofdXMLFile.FileName;
//                            lvValidationMessages.Items.Add(lvi);
//                            lvi = new ListViewItem("");
//                            lvValidationMessages.Items.Add(lvi);
//                            result = opcHandle.loadXML(ofdXMLFile.FileName, tvItems, out IsXmlValid);
//                            if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
//                            else
//                            {
//                                pnlValidationMessages.Visible = true;
//                                pnlValidationMessages.BringToFront();
//                            }
//                            if (result == -1) errMsg = "File doesnot exist!!!";
//                            else if (result == -2) errMsg = "File is not a well-formed XML!!!";
//                            else if (result == -3) errMsg = "XSD file is not valid!!!";
//                            else if (result == -4) errMsg = "File is not a valid XML, as per the schema!!!";
//                            else if (result == -5) errMsg = "MODBUS Slave Port is Not Valid as per the schema";
//                            lvi = new ListViewItem("");
//                            lvValidationMessages.Items.Add(lvi);
//                            lvi = new ListViewItem(errMsg);
//                            lvValidationMessages.Items.Add(lvi);
//                            lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
//                            if (result < 0)
//                            {
//                                hideLoading();
//                                MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                                return;
//                            }
//                            xmlFile = ofdXMLFile.FileName; //Assign only after loading file...
//                                                           //Namrata: 19/12/2017
//                                                           //Ajay: 29/11/2018
//                            TreeNode searchNode = tvItems.Nodes[0].Nodes["MasterConfiguration"];
//                            if (searchNode != null)
//                                searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
//                            tvItems.SelectedNode = tvItems.Nodes[0];
//                            tvItems.Nodes[0].EnsureVisible();
//                            hideLoading();
//                        }
//                    }
//                    else { }
//                }
//                //Ajay: 29/11/2018
//                else if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
//                {
//                    string openedFile = ProtocolGateway.OppConfigurationFile;
//                    Utils.XMLFolderPath = Path.GetDirectoryName(ProtocolGateway.OppConfigurationFile);

//                    #region IEC61850
//                    ICDFilesData.DirectoryName = Path.GetDirectoryName(ProtocolGateway.OppConfigurationFile); //Get DirectoryName
//                    ICDFilesData.XmlName = Path.GetFileName(ProtocolGateway.OppConfigurationFile); // Get XML Name
//                    ICDFilesData.XmlPath = ProtocolGateway.OppConfigurationFile; // XML with full path
//                    ICDFilesData.XmlWithoutExt = "";// Path.GetFileNameWithoutExtension(Utils.XMLFileFullPath);
//                    #region ClearDatasets
//                    Utils.dsIED.Clear();
//                    Utils.dsIED.Tables.Clear();
//                    Utils.DsRCB.Clear();
//                    Utils.DsRCBData.Clear();
//                    Utils.dsResponseType.Clear();
//                    Utils.DsResponseType.Clear();
//                    Utils.dsIED.Clear();
//                    Utils.dsIEDName.Clear();
//                    Utils.DsAllConfigurationData.Clear();
//                    Utils.DsAllConfigureData.Clear();
//                    Utils.DsRCBDataset.Clear();
//                    Utils.DsRCBDS.Clear();
//                    Utils.DtRCBdata.Clear();
//                    #endregion ClearDatasets
//                    #endregion IEC61850

//                    #region GDisplay
//                    //Namrata: 27/04/2018
//                    GDSlave.DirName = Path.GetDirectoryName(ProtocolGateway.OppConfigurationFile); //Get DirectoryName
//                    GDSlave.SLDName = Path.GetFileName(ProtocolGateway.OppConfigurationFile); //Get XML Name
//                    GDSlave.SLDPath = ProtocolGateway.OppConfigurationFile;//XML with full path
//                    GDSlave.SLDWithoutExt = "";
//                    #region ClearDatasets
//                    GDSlave.DsExcelData.Tables.Clear();
//                    GDSlave.DtExcelData.Clear();
//                    GDSlave.DsExportData.Tables.Clear();
//                    GDSlave.DTAIImage.Clear();
//                    GDSlave.DTDIDOImage.Clear();
//                    #endregion ClearDatasets
//                    #endregion GDisplay
//                    int result = 0;
//                    string errMsg = "XML file is valid...";
//                    ResetConfiguratorState(false);
//                    showLoading();
//                    xmlFile = "";//reset old filename...
//                    pnlValidationMessages.Visible = true;
//                    ListViewItem lvi;
//                    lvi = new ListViewItem("Validating file: " + ProtocolGateway.OppConfigurationFile);
//                    toolStripStatusLabel1.Visible = true;  //Display File Name in Toolstrip 
//                    tspFileName.Text = ProtocolGateway.OppConfigurationFile;
//                    lvValidationMessages.Items.Add(lvi);
//                    lvi = new ListViewItem("");
//                    lvValidationMessages.Items.Add(lvi);
//                    result = opcHandle.loadXML(ProtocolGateway.OppConfigurationFile, tvItems, out IsXmlValid);
//                    if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
//                    else
//                    {
//                        pnlValidationMessages.Visible = true;
//                        pnlValidationMessages.BringToFront();
//                    }
//                    if (result == -1) errMsg = "File does not exist!";
//                    else if (result == -2) errMsg = "File is not a well-formed XML!";
//                    else if (result == -3) errMsg = "XSD file is not valid!";
//                    else if (result == -4) errMsg = "File is not a valid XML, as per the schema!";
//                    else if (result == -5) errMsg = "MODBUS Slave Port is Not Valid as per the schema!";
//                    lvi = new ListViewItem("");
//                    lvValidationMessages.Items.Add(lvi);
//                    lvi = new ListViewItem(errMsg);
//                    lvValidationMessages.Items.Add(lvi);
//                    lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
//                    if (result < 0)
//                    {
//                        hideLoading();
//                        MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        return;
//                    }
//                    xmlFile = ProtocolGateway.OppConfigurationFile;//Assign only after loading file...
//                    //Ajay: 29/11/2018
//                    //TreeNode searchNode = tvItems.Nodes[0].Nodes[5];
//                    TreeNode searchNode = tvItems.Nodes[0].Nodes["MasterConfiguration"];
//                    if (searchNode != null)
//                        searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
//                    tvItems.SelectedNode = tvItems.Nodes[0];
//                    tvItems.Nodes[0].EnsureVisible();
//                    hideLoading();
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void showLoading()
//        {
//            string strRoutineName = "frmOpenProPlus: showLoading";
//            try
//            {
//                // Configure a BackgroundWorker to perform your long running operation.
//                BackgroundWorker bgw = new BackgroundWorker();
//                bgw.DoWork += new DoWorkEventHandler(bg_DoWork);
//                bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bg_RunWorkerCompleted);
//                Control.CheckForIllegalCrossThreadCalls = false;
//                bgw.RunWorkerAsync();
//                System.Threading.Thread.Sleep(1000);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void bg_DoWork(object sender, DoWorkEventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: bg_DoWork";
//            try
//            {
//                // Display the loading form.
//                Console.WriteLine("*** showLoading form created...");
//                fp.TopMost = true;
//                fp.ShowDialog();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
//        {
//            Console.WriteLine("*** bg_RunWorkerCompleted called!!!");
//        }
//        private void hideLoading()
//        {
//            string strRoutineName = "frmOpenProPlus: hideLoading";
//            try
//            {
//                if (fp != null)
//                {
//                    fp.Hide();
//                }
//                else
//                {
//                    Console.WriteLine("*** hideLoading called w/o showLoading...");
//                }
//                this.TopMost = true;
//                this.TopMost = false;//Force it on top n then disable...
//                this.TopLevel = true;
//                Control.CheckForIllegalCrossThreadCalls = true;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void handleSaveAs()
//        {
//            string strRoutineName = "frmOpenProPlus: handleSaveAs";
//            try
//            {
//                saveAsXMLFile();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: saveAsToolStripMenuItem_Click";
//            try
//            {
//                ssParser.Show();
//                handleSaveAs();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void handleSave()
//        {
//            string strRoutineName = "frmOpenproPlus: handleSave";
//            try
//            {
//                DialogResult result = MessageBox.Show("Do you want to save changes?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
//                if (result == DialogResult.Yes)
//                {
//                    saveXMLFile();
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenproPlus: saveToolStripMenuItem_Click";
//            try
//            {
//                ssParser.Show();
//                handleSave();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void tsbSave_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: tsbSave_Click";
//            try
//            {
//                handleSave();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void frmParser_FormClosing(object sender, FormClosingEventArgs e)
//        {
//            string strRoutineName = "frmOpenproPlus: frmParser_FormClosing";
//            try
//            {
//                if (!showExitMessage) return;

//                DialogResult result = MessageBox.Show("Do you want to save any changes?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
//                if (result == DialogResult.Yes)
//                {
//                    saveXMLFile();

//                    //Namrata: 14/04/2018
//                    //Delete Folder from ProgramData
//                    if (Utils.DirectoryPath != "")
//                    {
//                        string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(ofdXMLFile.FileName) + "\\" + "IEC61850Client" + "\\" + "ProtocolConfiguration";
//                        string ImportedDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(Utils.DirectoryPath);// 
//                        if (System.IO.Directory.Exists(path))
//                        {
//                            FileDirectoryOperations fileDirectoryOperation = new FileDirectoryOperations();
//                            FileDirectoryOperations.DeleteDirectory(ImportedDirectoryPath);
//                        }
//                    }
//                    //Namrata: 18/04/2018
//                    //Delete Folder from AppData
//                    ICDFilesData.ICDDirMFile = System.IO.Path.GetTempPath() + "Protocol";// Application.StartupPath + @"\" + "IEC61850_Client" + @"\" + "ProtocolConfiguration";
//                    if (System.IO.Directory.Exists(ICDFilesData.ICDDirMFile))
//                    {
//                        FileDirectoryOperations fileDirectoryOperations = new FileDirectoryOperations();
//                        FileDirectoryOperations.DeleteDirectory(ICDFilesData.ICDDirMFile);
//                    }
//                }
//                else if (result == DialogResult.No)
//                {
//                    //Namrata: 28/04/2018
//                    Utils.AIMapReindex.Clear(); //to clear all  data on new 

//                    //Namrata: 14/04/2018
//                    if (Utils.DirectoryPath != "")
//                    {
//                        string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(ofdXMLFile.FileName) + "\\" + "IEC61850_Client" + "\\" + "ProtocolConfiguration";
//                        string ImportedDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(Utils.DirectoryPath);// 
//                        if (System.IO.Directory.Exists(path))
//                        {
//                            FileDirectoryOperations fileDirectoryOperation = new FileDirectoryOperations();
//                            FileDirectoryOperations.DeleteDirectory(ImportedDirectoryPath);
//                        }
//                    }
//                    //Namrata: 18/04/2018
//                    //Delete Folder from AppData
//                    ICDFilesData.ICDDirMFile = System.IO.Path.GetTempPath() + @"\" + "Protocol";
//                    ///*ICDFilesData.ICDDirFile = System.IO.Path.GetTempPath() + "IEC6185*/0Client" + @"\" + "ProtocolConfiguration";// Application.StartupPath + @"\" + "IEC61850_Client" + @"\" + "ProtocolConfiguration";
//                    if (System.IO.Directory.Exists(ICDFilesData.ICDDirMFile))
//                    {
//                        FileDirectoryOperations fileDirectoryOperations = new FileDirectoryOperations();
//                        FileDirectoryOperations.DeleteDirectory(ICDFilesData.ICDDirMFile);
//                    }
//                }
//                else
//                {
//                    e.Cancel = true;
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenproPlus: exitToolStripMenuItem_Click";
//            try
//            {
//                handleNew();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void HandleMapViewChange()
//        {
//            string strRoutineName = "frmOpenproPlus: HandleMapViewChange";
//            try
//            {
//                //IMP: scParser and grpMapping should be added first in forms (Designer.cs). Ex.
//                //Else docking won't work properly
//                if (tsbMappingView.Checked)
//                {
//                    scParser.Dock = DockStyle.None;
//                    scParser.Visible = false;
//                    grpMappingView.Dock = DockStyle.Fill;
//                    grpMappingView.Visible = true;
//                }
//                else
//                {
//                    grpMappingView.Dock = DockStyle.None;
//                    grpMappingView.Visible = false;
//                    scParser.Dock = DockStyle.Fill;
//                    scParser.Visible = true;
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void tsbMappingView_CheckedChanged(object sender, EventArgs e)
//        {
//            Console.WriteLine("***** Mapping view changed...");
//        }
//        private void tsbMappingView_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: tsbMappingView_Click";
//            try
//            {
//                handleShowOverview();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void ResetConfiguratorState(bool isNew)
//        {
//            string strRoutineName = "frmOpenProPlus: ResetConfiguratorState";
//            try
//            {
//                opcHandle = null;
//                tvItems.Nodes.Clear();
//                opcHandle = new OpenProPlus_Config();
//                opcHandle.ShowValidationMessages += this.ValidationCallBack;
//                Utils.setOpenProPlusHandle(getOpenProPlusHandle());
//                if (fo != null) fo.setOpenProPlusHandle(getOpenProPlusHandle());
//                Globals.resetUniqueNos(ResetUniqueNos.ALL);
//                //Patch for SerialConfiguration, call in context: opcHandle.loadSchema(Globals.RESOURCES_PATH + Globals.XSD_DATATYPE_FILENAME);
//                opcHandle.loadDefaultItems(tvItems);
//                tvItems.SelectedNode = tvItems.Nodes[0];
//                //IMP: Create all default entries ONLY after resetting unique nos and creating treenodes...
//                if (isNew) Utils.CreateDefaultEntries();
//                int a = Utils.DummyDI.Count();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        //Ajay: 21/11/2017
//        private void EnDs_Save_SaveAs_Exit(bool IsEnabled)
//        {
//            string strRoutineName = "frmOpenProPlus: tsbMappingView_Click";
//            try
//            {
//                saveToolStripMenuItem.Enabled = saveAsToolStripMenuItem.Enabled = exitToolStripMenuItem.Enabled = IsEnabled;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void handleOpen()
//        {
//            string strRoutineName = "frmOpenProPlus: handleOpen";
//            VersionMatch = false;
//            try
//            {
//                ssParser.Show();
//                toolStripStatusLabel1.Visible = true;
//                //Ajay: 21/11/2017 All edited
//                if (opcHandle.IsFileOpen)
//                {
//                    string strMsg = "Do you want to save any changes ?";
//                    if (!string.IsNullOrEmpty(xmlFile))
//                    {
//                        strMsg = strMsg.TrimEnd('?');
//                        strMsg += " to \"" + xmlFile + "\" ?";
//                    }
//                    if (xmlFile != null)
//                    {
//                        DialogResult result = MessageBox.Show(strMsg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
//                        if (result == DialogResult.Yes)
//                        {
//                            saveXMLFile();
//                        }
//                        else
//                        {
//                            ofdXMLFile.FileName = "";
//                            //Namrata: 28/04/2018
//                            Utils.AIMapReindex.Clear(); //to clear all  data on new 
//                        }
//                    }
//                }
//                openXMLFile();
//                if (VersionMatch) //Ajay: 11/01/2019
//                {
//                    opcHandle.IsFileOpen = true;
//                    Utils.IsOpen = true;
//                    EnDs_Save_SaveAs_Exit(true);
//                    saveToolStripMenuItem.Enabled = true;
//                    newToolStripMenuItem.Enabled = true;
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void openToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: openToolStripMenuItem_Click";
//            try
//            {
//                ssParser.Show();
//                handleOpen();
//            }
//            catch (Exception Ex)
//            {
//                MessageBox.Show(strRoutineName + ":" + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void tsbOpen_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenproPlus: tsbOpen_Click";
//            try
//            {
//                handleOpen();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void handleNew()
//        {
//            string strRoutineName = "frmOpenproPlus: handleNew";
//            try
//            {
//                //Ajay: 21/11/2017 All edited
//                if (opcHandle.IsFileOpen)
//                {
//                    string strMsg = "Do you want to save any changes ?";
//                    if (!string.IsNullOrEmpty(xmlFile))
//                    {
//                        strMsg = strMsg.TrimEnd('?');
//                        strMsg += " to \"" + xmlFile + "\" ?";
//                    }
//                    if (xmlFile != null)
//                    {
//                        DialogResult result = MessageBox.Show(strMsg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
//                        if (result == DialogResult.Yes)
//                        {
//                            saveXMLFile();
//                            #region IEC61850
//                            //Namrata: 18/04/2018
//                            //Delete Folder from AppData
//                            ICDFilesData.ICDDirMFile = System.IO.Path.GetTempPath() + "Protocol";
//                            if (System.IO.Directory.Exists(ICDFilesData.ICDDirMFile))
//                            {
//                                FileDirectoryOperations fileDirectoryOperations = new FileDirectoryOperations();
//                                FileDirectoryOperations.DeleteDirectory(ICDFilesData.ICDDirMFile);
//                            }
//                            if (Utils.DirectoryPath != "")
//                            {
//                                string FileNameWithoutExtesion = Path.GetFileNameWithoutExtension(Utils.DirectoryPath);
//                                string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(FileNameWithoutExtesion) + "\\" + "IEC61850_Client" + "\\" + "ProtocolConfiguration";
//                                string ImportedDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(Utils.DirectoryPath);// 
//                                if (System.IO.Directory.Exists(path))
//                                {
//                                    FileDirectoryOperations fileDirectoryOperation = new FileDirectoryOperations();
//                                    FileDirectoryOperations.DeleteDirectory(ImportedDirectoryPath);
//                                }
//                            }
//                            #endregion IEC61850

//                            #region Graphical Display
//                            //Namrata: 18/04/2018
//                            //Delete Folder from AppData
//                            GDSlave.XLSDirFile = System.IO.Path.GetTempPath() + "Protocol";
//                            if (System.IO.Directory.Exists(GDSlave.XLSDirFile))
//                            {
//                                FileDirectoryOperations fileDirectoryOperations = new FileDirectoryOperations();
//                                FileDirectoryOperations.DeleteDirectory(GDSlave.XLSDirFile);
//                            }
//                            if (Utils.DirectoryPath != "")
//                            {
//                                string FileNameWithoutExtesion = Path.GetFileNameWithoutExtension(Utils.DirectoryPath);
//                                string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(FileNameWithoutExtesion) + "\\" + "GUI";
//                                string ImportedDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(Utils.DirectoryPath);// 
//                                if (System.IO.Directory.Exists(path))
//                                {
//                                    FileDirectoryOperations fileDirectoryOperation = new FileDirectoryOperations();
//                                    FileDirectoryOperations.DeleteDirectory(ImportedDirectoryPath);
//                                }
//                            }
//                            #endregion Graphical Display

//                            xmlFile = null;
//                        }
//                        else
//                        {
//                            #region IEC61850
//                            //Namrata: 18/04/2018
//                            //Delete Folder from AppData
//                            ICDFilesData.ICDDirMFile = System.IO.Path.GetTempPath() + "protocol";
//                            if (System.IO.Directory.Exists(ICDFilesData.ICDDirMFile))
//                            {
//                                string DirectoryNameDelete = System.IO.Path.GetTempPath() + @"\" + "IEC61850Client";
//                                FileDirectoryOperations fileDirectoryOperations = new FileDirectoryOperations();
//                                FileDirectoryOperations.DeleteDirectory(DirectoryNameDelete);
//                            }
//                            if (Utils.DirectoryPath != "")
//                            {
//                                string FileNameWithoutExtesion = Path.GetFileNameWithoutExtension(Utils.DirectoryPath);
//                                string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(FileNameWithoutExtesion) + "\\" + "IEC61850Client" + "\\" + "Protocol";
//                                string ImportedDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(Utils.DirectoryPath);// 
//                                if (System.IO.Directory.Exists(path))
//                                {
//                                    FileDirectoryOperations fileDirectoryOperation = new FileDirectoryOperations();
//                                    FileDirectoryOperations.DeleteDirectory(ImportedDirectoryPath);
//                                }
//                            }
//                            #endregion IEC61850

//                            #region GDisplay
//                            #region ClearDatasets
//                            GDSlave.GDisplaySlave = false;
//                            GDSlave.DsExcelData.Tables.Clear();
//                            GDSlave.DsExcelData.Clear();
//                            GDSlave.DtExcelData.Clear();
//                            GDSlave.DsExportData.Tables.Clear();
//                            GDSlave.DTAIImage.Clear();
//                            GDSlave.DTDIDOImage.Clear();
//                            #endregion ClearDatasets
//                            //Namrata: 18/04/2018
//                            //Delete Folder from AppData
//                            GDSlave.XLSDirFile = System.IO.Path.GetTempPath() + "Protocol";
//                            if (System.IO.Directory.Exists(GDSlave.XLSDirFile))
//                            {
//                                string DirectoryNameDelete = System.IO.Path.GetTempPath() + @"\" + "GUI";
//                                FileDirectoryOperations fileDirectoryOperations = new FileDirectoryOperations();
//                                FileDirectoryOperations.DeleteDirectory(DirectoryNameDelete);
//                            }
//                            if (Utils.DirectoryPath != "")
//                            {
//                                string FileNameWithoutExtesion = Path.GetFileNameWithoutExtension(Utils.DirectoryPath);
//                                string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(FileNameWithoutExtesion) + "\\" + "GUI" + "\\" + "Protocol";
//                                string ImportedDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(Utils.DirectoryPath);// 
//                                if (System.IO.Directory.Exists(path))
//                                {
//                                    FileDirectoryOperations fileDirectoryOperation = new FileDirectoryOperations();
//                                    FileDirectoryOperations.DeleteDirectory(ImportedDirectoryPath);
//                                }
//                            }
//                            #endregion GDisplay
//                            xmlFile = null;
//                            //ResetConfiguratorState(true);
//                            //opcHandle.IsFileOpen = true;
//                        }
//                    }
//                    else
//                    {
//                        #region ClearDatasets
//                        GDSlave.DsExcelData.Tables.Clear();
//                        GDSlave.DsExcelData.Clear();
//                        GDSlave.DtExcelData.Clear();
//                        GDSlave.DsExportData.Tables.Clear();
//                        GDSlave.DTAIImage.Clear();
//                        GDSlave.DTDIDOImage.Clear();
//                        #endregion ClearDatasets
//                        //Namrata: 18/04/2018
//                        //Delete Folder from AppData
//                        ICDFilesData.ICDDirMFile = System.IO.Path.GetTempPath() + "protocol";//Namrata:11/04/2019
//                        if (System.IO.Directory.Exists(ICDFilesData.ICDDirMFile))
//                        {
//                            FileDirectoryOperations fileDirectoryOperations = new FileDirectoryOperations();
//                            FileDirectoryOperations.DeleteDirectory(ICDFilesData.ICDDirMFile);
//                        }
//                        if (Utils.DirectoryPath != "")
//                        {
//                            string FileNameWithoutExtesion = Path.GetFileNameWithoutExtension(Utils.DirectoryPath);
//                            string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(FileNameWithoutExtesion) + "\\" + "IEC61850Client" + "\\" + "ProtocolConfiguration";
//                            string ImportedDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(Utils.DirectoryPath);// 
//                            if (System.IO.Directory.Exists(path))
//                            {
//                                FileDirectoryOperations fileDirectoryOperation = new FileDirectoryOperations();
//                                FileDirectoryOperations.DeleteDirectory(ImportedDirectoryPath);
//                            }
//                        }


//                        #region GDisplay
//                        //Namrata: 18/04/2018
//                        //Delete Folder from AppData
//                        GDSlave.XLSDirFile = System.IO.Path.GetTempPath() + "Protocol";
//                        if (System.IO.Directory.Exists(GDSlave.XLSDirFile))
//                        {
//                            FileDirectoryOperations fileDirectoryOperations = new FileDirectoryOperations();
//                            FileDirectoryOperations.DeleteDirectory(GDSlave.XLSDirFile);
//                        }
//                        if (Utils.DirectoryPath != "")
//                        {
//                            string FileNameWithoutExtesion = Path.GetFileNameWithoutExtension(Utils.DirectoryPath);
//                            string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(FileNameWithoutExtesion) + "\\" + "IEC61850Client" + "\\" + "ProtocolConfiguration";
//                            string ImportedDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(Utils.DirectoryPath);// 
//                            if (System.IO.Directory.Exists(path))
//                            {
//                                FileDirectoryOperations fileDirectoryOperation = new FileDirectoryOperations();
//                                FileDirectoryOperations.DeleteDirectory(ImportedDirectoryPath);
//                            }
//                        }
//                        #endregion GDisplay
//                    }
//                }


//                ResetConfiguratorState(true);
//                opcHandle.IsFileOpen = true;
//                EnDs_Save_SaveAs_Exit(true);
//                toolStripStatusLabel1.Visible = false;
//                tspFileName.Text = "";
//                #region //Ajay: 21/11/2017  Original code commented
//                //DialogResult result = MessageBox.Show("Do you want to save any changes?", "New XML file...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
//                //if (result == DialogResult.Yes)
//                //{
//                //    saveXMLFile();
//                //    xmlFile = null;
//                //    ResetConfiguratorState(true);
//                //}
//                //else if (result == DialogResult.No)
//                //{
//                //    xmlFile = null;
//                //    ResetConfiguratorState(true);
//                //}
//                //else//cancel
//                //{
//                //}
//                #endregion
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void newToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenproPlus: newToolStripMenuItem_Click";
//            try
//            {
//                handleNew();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void tsbNew_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: tsbNew_Click";
//            try
//            {
//                //Namrata: 28/04/2018
//                Utils.AIMapReindex.Clear(); //to clear all  data on new 
//                handleNew();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void grpMappingView_Resize(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: grpMappingView_Resize";
//            try
//            {
//                lvMappingView.Width = grpMappingView.Width - 10;
//                lvMappingView.Height = grpMappingView.Height - FILTER_PANEL_HEIGHT - 10;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void lvMappingView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: lvMappingView_DrawColumnHeader";
//            try
//            {
//                //IMP: No 'Console.Write' statements here...
//                //Console.WriteLine("*** boom boom boom boom bash");
//                e.DrawDefault = true;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void lvMappingView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: lvMappingView_DrawSubItem";
//            try
//            {
//                //IMP: No 'Console.Write' statements here...
//                if (e.ColumnIndex >= COLS_B4_MULTISLAVE && e.SubItem.Text == "yes")
//                {
//                    e.DrawDefault = false;
//                    e.DrawBackground();
//                    Rectangle tPos = new Rectangle(e.SubItem.Bounds.X + ((e.SubItem.Bounds.Width - e.SubItem.Bounds.Height) / 2), e.SubItem.Bounds.Y, e.SubItem.Bounds.Height, e.SubItem.Bounds.Height);
//                    e.Graphics.DrawImage((Image)Properties.Resources.ResourceManager.GetObject("greenindicator"), tPos);
//                }
//                else if (e.SubItem.Text == "transparentimg")
//                {
//                    e.DrawDefault = false;
//                    e.DrawBackground();
//                    Rectangle tPos = new Rectangle(e.SubItem.Bounds.X + ((e.SubItem.Bounds.Width - e.SubItem.Bounds.Height) / 2), e.SubItem.Bounds.Y, e.SubItem.Bounds.Height, e.SubItem.Bounds.Height);
//                    e.Graphics.DrawImage((Image)Properties.Resources.ResourceManager.GetObject("transparent"), tPos);
//                }
//                else
//                {
//                    e.DrawDefault = true;
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }

//        }
//        private void lvMappingView_ColumnClick(object sender, ColumnClickEventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: lvMappingView_ColumnClick";
//            try
//            {
//                if (e.Column != sortColumn)  // Determine whether the column is the same as the last column clicked.
//                {
//                    sortColumn = e.Column;
//                    lvMappingView.Sorting = SortOrder.Ascending;
//                }
//                else
//                {
//                    if (lvMappingView.Sorting == SortOrder.Ascending) // Determine what the last sort order was and change it.
//                        lvMappingView.Sorting = SortOrder.Descending;
//                    else
//                        lvMappingView.Sorting = SortOrder.Ascending;
//                }
//                lvMappingView.Sort();  // Call the sort method to manually sort.
//                lvMappingView.ListViewItemSorter = new ListViewItemComparer(e.Column, lvMappingView.Sorting, lvMappingView.Columns[e.Column].Tag);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }

//        }
//        // Implements the manual sorting of items by column.
//        class ListViewItemComparer : IComparer
//        {
//            private int col;
//            private SortOrder order;
//            private int dataType = 0; //0->string, 1->int
//            public ListViewItemComparer()
//            {
//                col = 0;
//                order = SortOrder.Ascending;
//            }
//            public ListViewItemComparer(int column, SortOrder order, object objType)
//            {
//                col = column;
//                this.order = order;
//                if (objType != null && objType.ToString() == "int") dataType = 1;
//            }
//            public int Compare(object x, object y)
//            {
//                int returnVal = -1;
//                if (dataType == 0)
//                {//string
//                    returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text,
//                                        ((ListViewItem)y).SubItems[col].Text);
//                }
//                else if (dataType == 1)
//                {//int
//                    string ix = "0", iy = "0";
//                    if (((ListViewItem)x).SubItems[col].Text == "") ix = "0";
//                    else ix = ((ListViewItem)x).SubItems[col].Text;

//                    if (((ListViewItem)y).SubItems[col].Text == "") iy = "0";
//                    else iy = ((ListViewItem)y).SubItems[col].Text;

//                    returnVal = CompareInt(ix, iy);
//                }
//                // Determine whether the sort order is descending.
//                // Invert the value returned by String.Compare.
//                if (order == SortOrder.Descending) returnVal *= -1;
//                return returnVal;
//            }
//            private int CompareInt(string x, string y)
//            {
//                int ix, iy;
//                try
//                {
//                    ix = Int32.Parse(x);
//                }
//                catch (System.FormatException)
//                {
//                    ix = 0;
//                }
//                try
//                {
//                    iy = Int32.Parse(y);
//                }
//                catch (System.FormatException)
//                {
//                    iy = 0;
//                }
//                return ix - iy;
//            }
//        }
//        private void handleShowOverview()
//        {
//            string strRoutineName = "frmOpenProPlus: handleShowOverview";
//            try
//            {
//                if (fo == null)
//                {
//                    fo = new frmOverview(getOpenProPlusHandle());
//                    fo.FormClosed += fo_FormClosed;
//                    fo.Show();
//                }
//                else
//                {
//                    fo.TopMost = true;
//                    fo.TopMost = false;
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void showOverviewToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: showOverviewToolStripMenuItem_Click";
//            try
//            {
//                handleShowOverview();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void fo_FormClosed(object sender, FormClosedEventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: fo_FormClosed";
//            try
//            {
//                fo = null;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void frmParser_Resize(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: frmParser_Resize";
//            try
//            {
//                pnlValidationMessages.Left = (this.Width - pnlValidationMessages.Width) / 2;
//                pnlValidationMessages.Top = (this.Height - (this.mnuParser.Height + this.tsParser.Height + this.ssParser.Height + pnlValidationMessages.Height));

//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void btnClose_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: btnClose_Click";
//            try
//            {
//                pnlValidationMessages.Visible = false;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void scParser_Panel2_ControlAdded(object sender, ControlEventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: scParser_Panel2_ControlAdded";
//            try
//            {
//                e.Control.Width = scParser.Panel2.Width; //Resize the control in right-side panel, got using getView()
//                e.Control.Height = scParser.Panel2.Height;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void scParser_Panel2_Resize(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: scParser_Panel2_Resize";
//            try
//            {
//                if (scParser.Panel2.Controls.Count <= 0) return;
//                Control ctrl = scParser.Panel2.Controls[0];
//                if (ctrl != null)
//                {
//                    ctrl.Width = scParser.Panel2.Width;
//                    ctrl.Height = scParser.Panel2.Height;
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void tsbClose_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: tsbClose_Click";
//            try
//            {
//                this.Close();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void tsbReindexData_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: tsbReindexData_Click";
//            try
//            {
//                opcHandle.regenerateSequence();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void handleHelp()
//        {
//            string strRoutineName = "frmOpenProPlus: handleHelp";
//            try
//            {
//                Help.ShowHelp(this, Globals.RESOURCES_PATH + "OpenPro+ Help.chm");
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void tsbHelp_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: tsbHelp_Click";
//            try
//            {
//                handleHelp();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void shelpToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: shelpToolStripMenuItem_Click";
//            try
//            {
//                handleHelp();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void openProUIToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: openProUIToolStripMenuItem_Click";
//            try
//            {
//                frmOpenPro_UI FrmOpenPro_UIs = new frmOpenPro_UI();
//                FrmOpenPro_UIs.Show();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void ssParser_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
//        {

//        }
//        private void manualToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: manualToolStripMenuItem_Click";
//            try
//            {
//                Help.ShowHelp(this, Globals.RESOURCES_PATH + "OpenProPlus Configurator - User Manual.pdf");
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void recentFilesToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            //MyMruList.FileSelected += MyMruList_FileSelected;
//        }
//        private void tspImportICD_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: tspImportICD_Click";
//            try
//            {
//                frmOpenPro_UI FrmOpenPro_UIs = new frmOpenPro_UI();
//                FrmOpenPro_UIs.Show();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void tspManual_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: tspManual_Click";
//            try
//            {
//                Help.ShowHelp(this, Globals.RESOURCES_PATH + "OpenProPlus Configurator - User Manual.pdf");
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        private void toolStripButtonExpandTV_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: toolStripButtonExpandTV_Click";
//            try
//            {
//                tvItems.ExpandAll();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        //Ajay: 04/12/2018
//        #region File Upload and Download

//        //Ajay: 04/12/2018
//        private void tsbtnFileDownload_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: tsbtnFileDownload_Click";
//            try
//            {
//                if (string.IsNullOrEmpty(ProtocolGateway.OppIpAddress) || string.IsNullOrEmpty(ProtocolGateway.User) || string.IsNullOrEmpty(ProtocolGateway.Password))
//                {
//                    if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Full)
//                    {
//                        frmFTP frmFtp = new frmFTP();
//                        frmFtp.ShowDialog();
//                    }
//                    else
//                    {
//                        MessageBox.Show("Could not connect to server due to improper connection info!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        return;
//                    }
//                }
//                this.Cursor = Cursors.WaitCursor;
//                if (ProtocolGateway.Protocol.ToUpper() == "FTP")
//                {
//                    if (!string.IsNullOrEmpty(ProtocolGateway.OppIpAddress) && !string.IsNullOrEmpty(ProtocolGateway.User) && !string.IsNullOrEmpty(ProtocolGateway.Password))
//                    {
//                        DownloadFileUsingFTP("ftp://" + ProtocolGateway.OppIpAddress, "openproplus_config.xml");
//                    }
//                    else
//                    {
//                        MessageBox.Show("Could not connect to server due to improper connection info!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        return;
//                    }
//                }
//                else if (ProtocolGateway.Protocol.ToUpper() == "SFTP")
//                {
//                    if (!string.IsNullOrEmpty(ProtocolGateway.OppIpAddress) && !string.IsNullOrEmpty(ProtocolGateway.User) && !string.IsNullOrEmpty(ProtocolGateway.Password))
//                    {
//                        DownloadFileUsingSFTP("mnt/app/database", "openproplus_config.xml");
//                    }
//                    else
//                    {
//                        MessageBox.Show("Could not connect to server due to improper connection info!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        return;
//                    }
//                }
//                //Ajay: 08/01/2019
//                else if (ProtocolGateway.Protocol.ToUpper() == "HTTP")
//                {
//                    if (!string.IsNullOrEmpty(ProtocolGateway.OppIpAddress) && !string.IsNullOrEmpty(ProtocolGateway.User) && !string.IsNullOrEmpty(ProtocolGateway.Password))
//                    {
//                        DownloadFileUsingFTP("ftp://" + ProtocolGateway.OppIpAddress, "openproplus_config.xml");
//                        //DownloadFileUsingHTTP("http://" + ProtocolGateway.OppIpAddress);
//                    }
//                    else
//                    {
//                        MessageBox.Show("Could not connect to server due to improper connection info!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        return;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(this.Name + ": " + MethodBase.GetCurrentMethod().Name + ": " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//            finally
//            { this.Cursor = Cursors.Default; }
//        }
//        //Ajay: 04/12/2018
//        private void tsbtnFileUpload_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                if (string.IsNullOrEmpty(ProtocolGateway.OppIpAddress) || string.IsNullOrEmpty(ProtocolGateway.User) || string.IsNullOrEmpty(ProtocolGateway.Password))
//                {
//                    if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Full)
//                    {
//                        frmFTP frmFtp = new frmFTP();
//                        frmFtp.ShowDialog();
//                    }
//                    else
//                    {
//                        MessageBox.Show("Could not connect to server due to improper connection info!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        return;
//                    }
//                }
//                this.Cursor = Cursors.WaitCursor;
//                if (ProtocolGateway.Protocol.ToUpper() == "FTP")
//                {
//                    if (!string.IsNullOrEmpty(ProtocolGateway.OppIpAddress) && !string.IsNullOrEmpty(ProtocolGateway.User) && !string.IsNullOrEmpty(ProtocolGateway.Password))
//                    {
//                        UploadFileUsingFTP("ftp://" + ProtocolGateway.OppIpAddress, "openproplus_config.xml");
//                    }
//                    else
//                    {
//                        MessageBox.Show("Could not connect to server due to improper connection info!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        return;
//                    }
//                }
//                else if (ProtocolGateway.Protocol.ToUpper() == "SFTP")
//                {
//                    if (!string.IsNullOrEmpty(ProtocolGateway.OppIpAddress) && !string.IsNullOrEmpty(ProtocolGateway.User) && !string.IsNullOrEmpty(ProtocolGateway.Password))
//                    {
//                        UploadFileUsingSFTP("mnt/app/database", "openproplus_config.xml");
//                    }
//                    else
//                    {
//                        MessageBox.Show("Could not connect to server due to improper connection info!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        return;
//                    }
//                }
//                else if (ProtocolGateway.Protocol.ToUpper() == "HTTP")
//                {
//                    if (!string.IsNullOrEmpty(ProtocolGateway.OppIpAddress) && !string.IsNullOrEmpty(ProtocolGateway.User) && !string.IsNullOrEmpty(ProtocolGateway.Password))
//                    {
//                        UploadFileUsingHTTP("ftp://" + ProtocolGateway.OppIpAddress);
//                    }
//                    else
//                    {
//                        MessageBox.Show("Could not connect to server due to improper connection info!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        return;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(this.Name + ": " + MethodBase.GetCurrentMethod().Name + ": " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//            finally
//            { this.Cursor = Cursors.Default; }
//        }

//        #region FTP
//        //Ajay: 04/12/2018
//        private FtpWebRequest CreateFTPWebRequest(string ftpDirectoryPath, bool keepAlive = false)
//        {
//            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(ftpDirectoryPath));
//            //Set proxy to null. Under current configuration if this option is not set then the proxy that is used will get an html response from the web content gateway (firewall monitoring system)
//            request.Proxy = null;
//            request.UsePassive = true;
//            request.UseBinary = true;
//            request.KeepAlive = keepAlive;
//            request.Credentials = new NetworkCredential(ProtocolGateway.User, ProtocolGateway.Password);
//            return request;
//        }
//        //Ajay: 04/12/2018
//        private void DownloadFileUsingFTP(string ftpFilePath, string ftpFileName)
//        {
//            try
//            {
//                int bytesRead = 0;
//                byte[] buffer = new byte[2048];
//                Stream reader = null;
//                SaveFileDialog svfiledlg = new SaveFileDialog();
//                string strFilePath = "";
//                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
//                {
//                    if (!string.IsNullOrEmpty(xmlFile) && File.Exists(xmlFile))
//                    {
//                        strFilePath = xmlFile;
//                        //Path.Combine(Path.GetDirectoryName(ProtocolGateway.OppConfigurationFile), ftpFileName);
//                    }
//                }
//                else if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Full)
//                {
//                    svfiledlg.Title = "Save OpenProPlus Configuration File";
//                    svfiledlg.InitialDirectory = Path.GetFullPath(Environment.SpecialFolder.MyDocuments.ToString());
//                    svfiledlg.DefaultExt = "xml";
//                    svfiledlg.FileName = ftpFileName; //"openproplus_config.xml";
//                    svfiledlg.Filter = "xml files (*.xml)|*.xml";
//                    svfiledlg.CheckFileExists = false;
//                    svfiledlg.CheckPathExists = true;
//                    svfiledlg.SupportMultiDottedExtensions = false;
//                    if (svfiledlg.ShowDialog() == DialogResult.OK)
//                    {
//                        strFilePath = svfiledlg.FileName;
//                    }
//                    else { return; }
//                }
//                if (!string.IsNullOrEmpty(strFilePath))
//                {
//                    FtpWebRequest request = CreateFTPWebRequest(ftpFilePath + "//" + ftpFileName, true);
//                    request.Method = WebRequestMethods.Ftp.DownloadFile;
//                    try
//                    {
//                        reader = request.GetResponse().GetResponseStream();
//                    }
//                    catch (WebException ex)
//                    {
//                        MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        //MessageBox.Show(((FtpWebResponse)ex.Response).StatusDescription, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        return;
//                    }
//                    catch (InvalidOperationException ex)
//                    {
//                        MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        return;
//                    }
//                    if (reader != null)
//                    {
//                        string destinationFileName = "";
//                        frmInput frminput = new frmInput();
//                        frminput.defaultFileName = GetDefaultFileName(Path.GetDirectoryName(strFilePath));
//                        if (frminput.ShowDialog() == DialogResult.OK)
//                        {
//                            if (!string.IsNullOrEmpty(frminput.txtbxInput.Text.Trim()))
//                            {
//                                destinationFileName = Path.Combine(Path.GetDirectoryName(ProtocolGateway.ProtocolGatewayConfigurationFile), frminput.txtbxInput.Text.Trim());
//                                frminput.Close();
//                            }
//                        }
//                        else { return; }

//                        FileStream fs = new FileStream(Path.Combine(Path.GetDirectoryName(strFilePath), destinationFileName), FileMode.Create);
//                        while (true)
//                        {
//                            bytesRead = reader.Read(buffer, 0, buffer.Length);
//                            if (bytesRead == 0)
//                                break;
//                            fs.Write(buffer, 0, bytesRead);
//                        }
//                        fs.Close();
//                        MessageBox.Show("'" + Path.GetFileName(destinationFileName) + "' file downloaded successfully!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    }
//                    else
//                    {
//                        MessageBox.Show("File is empty!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    }
//                }
//            }
//            catch (Exception ex) { MessageBox.Show("File downloading failed! " + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error); }
//        }
//        //Ajay: 04/12/2018
//        private void UploadFileUsingFTP(string ftpFilePath, string ftpFileName)
//        {
//            FtpWebRequest ftpRequest = null;
//            Stream ftpStream = null;
//            FileStream localFileStream = null;
//            int bufferSize = 2048;
//            byte[] buffer = new byte[2048];
//            string strFilePath = "";
//            OpenFileDialog opnfiledlg = new OpenFileDialog();
//            try
//            {
//                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
//                {
//                    if (!string.IsNullOrEmpty(xmlFile) && File.Exists(xmlFile))
//                    {
//                        strFilePath = xmlFile;
//                    }
//                }
//                else if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Full)
//                {
//                    opnfiledlg.Title = "Select OpenProPlus Configuration File";
//                    //opnfiledlg.InitialDirectory = Path.GetDirectoryName(ProtocolGateway.OppConfigurationFile);
//                    opnfiledlg.DefaultExt = "xml";
//                    opnfiledlg.Filter = "xml files (*.xml)|*.xml";
//                    opnfiledlg.CheckFileExists = true;
//                    opnfiledlg.CheckPathExists = true;
//                    opnfiledlg.SupportMultiDottedExtensions = false;
//                    if (opnfiledlg.ShowDialog() == DialogResult.OK)
//                    {
//                        strFilePath = opnfiledlg.FileName;
//                    }
//                    else { return; }
//                }
//                DialogResult result = MessageBox.Show("Are you sure, you want to upload '" + strFilePath + "' to '" + ProtocolGateway.OppIpAddress + "'?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
//                if (result == DialogResult.Yes)
//                {
//                    ftpRequest = CreateFTPWebRequest(ftpFilePath + "//" + ftpFileName, false);
//                    ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
//                    try
//                    {
//                        ftpStream = ftpRequest.GetRequestStream();
//                    }
//                    catch (WebException e)
//                    {
//                        String status = ((FtpWebResponse)e.Response).StatusDescription;
//                        MessageBox.Show(status, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        return;
//                    }
//                    if (ftpStream != null)
//                    {
//                        localFileStream = new FileStream(strFilePath, FileMode.Open);
//                        byte[] byteBuffer = new byte[bufferSize];
//                        int bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
//                        while (bytesSent != 0)
//                        {
//                            ftpStream.Write(byteBuffer, 0, bytesSent);
//                            bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
//                        }
//                        localFileStream.Close();
//                        MessageBox.Show("'" + strFilePath + "' Uploaded successfully!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    }
//                    else { }
//                }
//                else { return; }
//            }
//            catch (Exception ex) { MessageBox.Show("'" + strFilePath + "' uploading failed!" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error); }
//            finally
//            {
//                if (localFileStream != null) localFileStream.Close();
//                if (ftpStream != null) ftpStream.Close();
//                ftpRequest = null;
//            }
//        }

//        #endregion

//        #region SFTP
//        //Ajay: 14/12/2018
//        private void DownloadFileUsingSFTP(string sftpFileDirectory, string sftpFileName)
//        {
//            try
//            {
//                SaveFileDialog svfiledlg = new SaveFileDialog();
//                string strFilePath = "";
//                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
//                {
//                    strFilePath = Path.GetDirectoryName(ProtocolGateway.OppConfigurationFile);
//                }
//                else if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Full)
//                {
//                    svfiledlg.Title = "Save OpenProPlus Configuration File";
//                    svfiledlg.InitialDirectory = Path.GetFullPath(Environment.SpecialFolder.MyDocuments.ToString());
//                    svfiledlg.DefaultExt = "xml";
//                    svfiledlg.FileName = "openproplus_config.xml";
//                    svfiledlg.Filter = "xml files (*.xml)|*.xml";
//                    svfiledlg.CheckFileExists = false;
//                    svfiledlg.CheckPathExists = true;
//                    svfiledlg.SupportMultiDottedExtensions = false;
//                    if (svfiledlg.ShowDialog() == DialogResult.OK)
//                    {
//                        strFilePath = svfiledlg.FileName;
//                    }
//                    else { return; }
//                }
//                if (!string.IsNullOrEmpty(strFilePath))
//                {
//                    try
//                    {
//                        using (SftpClient sftpClient = new SftpClient(ProtocolGateway.OppIpAddress, 22, ProtocolGateway.User, ProtocolGateway.Password))
//                        {
//                            sftpClient.KeepAliveInterval = new TimeSpan(0, 1, 0);
//                            try
//                            {
//                                sftpClient.Connect();
//                            }
//                            catch (Renci.SshNet.Common.SshConnectionException ex)
//                            {
//                                MessageBox.Show("Can not establish the connection with " + ProtocolGateway.OppIpAddress + Environment.NewLine + ex.Message + Environment.NewLine + ex.DisconnectReason, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
//                                return;
//                            }
//                            sftpClient.ChangeDirectory("/");
//                            SftpFile sftpFile = sftpClient.ListDirectory(sftpFileDirectory).ToList().Where(x => x.Name == sftpFileName).SingleOrDefault();
//                            using (Stream stream = File.OpenWrite(strFilePath))
//                            {
//                                if (sftpClient.IsConnected)
//                                {
//                                    try
//                                    {
//                                        sftpClient.DownloadFile(sftpFile.FullName, stream);
//                                    }
//                                    catch (Renci.SshNet.Common.SftpPathNotFoundException ex)
//                                    {
//                                        MessageBox.Show("File downloading failed!" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                                        return;
//                                    }
//                                    catch (Renci.SshNet.Common.SftpPermissionDeniedException ex)
//                                    {
//                                        MessageBox.Show("File downloading failed!" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                                        return;
//                                    }
//                                    catch (Renci.SshNet.Common.SshConnectionException ex)
//                                    {
//                                        MessageBox.Show("File downloading failed!" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                                        return;
//                                    }
//                                    catch (Renci.SshNet.Common.SshException ex)
//                                    {
//                                        MessageBox.Show("File downloading failed!" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                                        return;
//                                    }
//                                    MessageBox.Show("File downloaded successfully to '" + strFilePath + "'", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
//                                }
//                            }
//                        }
//                    }
//                    catch (Renci.SshNet.Common.SshException ex)
//                    {
//                        MessageBox.Show("File downloading failed!" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    }
//                }
//            }
//            catch (Exception ex) { MessageBox.Show("File downloading failed! " + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error); }
//        }
//        //Ajay: 14/12/2018
//        private void UploadFileUsingSFTP(string sftpFileDirectory, string sftpFileName)
//        {
//            try
//            {
//                SftpClient sftpClient = null;
//                OpenFileDialog opnfiledlg = new OpenFileDialog();
//                string strFilePath = "";
//                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
//                {
//                    strFilePath = Path.GetDirectoryName(ProtocolGateway.OppConfigurationFile);
//                }
//                else if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Full)
//                {
//                    opnfiledlg.Title = "Select OpenProPlus Configuration File";
//                    opnfiledlg.InitialDirectory = Path.GetFullPath(Environment.SpecialFolder.MyDocuments.ToString());
//                    opnfiledlg.DefaultExt = "xml";
//                    opnfiledlg.Filter = "xml files (*.xml)|*.xml";
//                    opnfiledlg.CheckFileExists = true;
//                    opnfiledlg.CheckPathExists = true;
//                    opnfiledlg.SupportMultiDottedExtensions = false;
//                    if (opnfiledlg.ShowDialog() == DialogResult.OK)
//                    {
//                        strFilePath = opnfiledlg.FileName;
//                    }
//                    else { return; }
//                }
//                if (!string.IsNullOrEmpty(strFilePath))
//                {
//                    DialogResult result = MessageBox.Show("Are you sure, you want to upload '" + strFilePath + "' to '" + ProtocolGateway.OppIpAddress + "'?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
//                    if (result == DialogResult.Yes)
//                    {
//                        try
//                        {
//                            using (sftpClient = new SftpClient(ProtocolGateway.OppIpAddress, 22, ProtocolGateway.User, ProtocolGateway.Password))
//                            {
//                                sftpClient.KeepAliveInterval = new TimeSpan(0, 1, 0);
//                                try
//                                {
//                                    sftpClient.Connect();
//                                }
//                                catch (Renci.SshNet.Common.SshConnectionException ex)
//                                {
//                                    MessageBox.Show("Can not establish the connection with " + ProtocolGateway.OppIpAddress + Environment.NewLine + ex.Message + Environment.NewLine + ex.DisconnectReason, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
//                                }
//                                sftpClient.ChangeDirectory("/");
//                                SftpFile sftpFile = sftpClient.ListDirectory(sftpFileDirectory).ToList().Where(x => x.Name == sftpFileName).SingleOrDefault();
//                                using (Stream stream = File.OpenRead(strFilePath))
//                                {
//                                    if (sftpClient.IsConnected)
//                                    {
//                                        try
//                                        {
//                                            if (sftpFile != null)
//                                            {
//                                                sftpClient.UploadFile(stream, sftpFile.FullName);
//                                            }
//                                            else
//                                            {
//                                                MessageBox.Show("Invalid file!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                                            }
//                                        }
//                                        catch (Renci.SshNet.Common.SftpPathNotFoundException ex)
//                                        {
//                                            MessageBox.Show("File uploading failed!" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                                            return;
//                                        }
//                                        catch (Renci.SshNet.Common.SftpPermissionDeniedException ex)
//                                        {
//                                            MessageBox.Show("File uploading failed!" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                                            return;
//                                        }
//                                        catch (Renci.SshNet.Common.SshConnectionException ex)
//                                        {
//                                            MessageBox.Show("File uploading failed!" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                                            return;
//                                        }
//                                        catch (Renci.SshNet.Common.SshException ex)
//                                        {
//                                            MessageBox.Show("File uploading failed!" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                                            return;
//                                        }
//                                        MessageBox.Show("File uploaded successfully to '" + strFilePath + "'", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
//                                    }
//                                }
//                            }
//                        }
//                        catch (Renci.SshNet.Common.SshException ex)
//                        {
//                            MessageBox.Show("File uploading failed!" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        }
//                    }
//                }
//            }
//            catch (Exception ex) { MessageBox.Show("File uploading failed! " + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error); }
//        }

//        #endregion

//        #region HTTP
//        //Ajay: 08/01/2019
//        private void UploadFileUsingHTTP(string OppWebAddr)
//        {
//            string strFilePath = "";
//            OpenFileDialog opnfiledlg = new OpenFileDialog();
//            WebClient wbclient = new WebClient();
//            WebClient client = new WebClient();
//            try
//            {
//                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
//                {
//                    if (!string.IsNullOrEmpty(xmlFile) && File.Exists(xmlFile))
//                    {
//                        strFilePath = xmlFile;
//                    }
//                    else
//                    {
//                        MessageBox.Show("File is empty!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
//                        return;
//                    }
//                }
//                else if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Full)
//                {
//                    opnfiledlg.Title = "Select OpenProPlus Configuration File";
//                    //opnfiledlg.InitialDirectory = Path.GetDirectoryName(ProtocolGateway.OppConfigurationFile);
//                    opnfiledlg.DefaultExt = "xml";
//                    opnfiledlg.Filter = "xml files (*.xml)|*.xml";
//                    opnfiledlg.CheckFileExists = true;
//                    opnfiledlg.CheckPathExists = true;
//                    opnfiledlg.SupportMultiDottedExtensions = false;
//                    if (opnfiledlg.ShowDialog() == DialogResult.OK)
//                    {
//                        strFilePath = opnfiledlg.FileName;
//                    }
//                    else { return; }
//                }
//                //strFilePath = @"C:\Users\ajayp\Downloads\openproplus_config.xml";
//                if (!string.IsNullOrEmpty(strFilePath))
//                {
//                    DialogResult result = MessageBox.Show("Are you sure, you want to upload '" + Path.GetFileName(strFilePath) + "' to '" + ProtocolGateway.OppIpAddress + "'?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
//                    if (result == DialogResult.Yes)
//                    {
//                        wbclient.Credentials = new NetworkCredential(ProtocolGateway.User, ProtocolGateway.Password);
//                        wbclient.UploadFile(OppWebAddr + "/rtv2opp.xml", strFilePath);
//                        //Upload file
//                        client = new WebClient();
//                        NameValueCollection login = new NameValueCollection();
//                        login.Add("username", "admin");
//                        login.Add("password", "admin");
//                        login.Add("mode", "upload");
//                        try
//                        {
//                            string strResponse = Encoding.UTF8.GetString(client.UploadValues("http://" + ProtocolGateway.OppIpAddress + "//rtv2upload.php", login));
//                            if (!string.IsNullOrEmpty(strResponse))
//                            {
//                                frmHTMLWebResponse frmRes = new frmHTMLWebResponse();
//                                frmRes.Text = "File upload status";
//                                frmRes.wbBrowser.DocumentText = "'" + Path.GetFileName(strFilePath) + "' " + strResponse;
//                                frmRes.ShowDialog();
//                            }
//                            //if (strResponse.Contains("successfully"))
//                            //{
//                            //    MessageBox.Show("'" + Path.GetFileName(strFilePath) + "' " + strResponse, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
//                            //}
//                            //else if (strResponse.Contains("failed") || strResponse.Contains("error"))
//                            //{
//                            //    MessageBox.Show("'" + Path.GetFileName(strFilePath) + "' " + strResponse, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
//                            //}
//                        }
//                        catch (Exception ex)
//                        {
//                            MessageBox.Show("File uploading failed!" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
//                            return;
//                        }

//                    }
//                    else { return; }
//                }
//                else
//                {
//                    MessageBox.Show("File is empty!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
//                    return;
//                }
//            }
//            catch (Exception ex) { MessageBox.Show("File uploading failed!" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error); }
//            finally
//            {
//                if (wbclient != null) { wbclient.Dispose(); wbclient = null; }
//                if (client != null) { client.Dispose(); client = null; }
//            }
//        }
//        //Ajay: 08/01/2019
//        //Ajay: 14/12/2018
//        private void DownloadFileUsingHTTP(string OppWebAddr)
//        {
//            try
//            {
//                SaveFileDialog svfiledlg = new SaveFileDialog();
//                string strFilePath = "";
//                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
//                {
//                    strFilePath = Path.GetDirectoryName(ProtocolGateway.OppConfigurationFile);
//                }
//                else if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Full)
//                {
//                    svfiledlg.Title = "Save OpenProPlus Configuration File";
//                    svfiledlg.InitialDirectory = Path.GetFullPath(Environment.SpecialFolder.MyDocuments.ToString());
//                    svfiledlg.DefaultExt = "xml";
//                    svfiledlg.FileName = "openproplus_config.xml";
//                    svfiledlg.Filter = "xml files (*.xml)|*.xml";
//                    svfiledlg.CheckFileExists = false;
//                    svfiledlg.CheckPathExists = true;
//                    svfiledlg.SupportMultiDottedExtensions = false;
//                    if (svfiledlg.ShowDialog() == DialogResult.OK)
//                    {
//                        strFilePath = Path.GetDirectoryName(svfiledlg.FileName);
//                    }
//                    else { return; }
//                }
//                if (!string.IsNullOrEmpty(strFilePath))
//                {
//                    WebClient client = new WebClient();
//                    try
//                    {
//                        //Ajay: 08/01/2018
//                        NameValueCollection loginData = new NameValueCollection();
//                        loginData.Add("username", "admin");
//                        loginData.Add("password", "admin");
//                        loginData.Add("mode", "download");
//                        try
//                        {
//                            string response = Encoding.ASCII.GetString(client.UploadValues(OppWebAddr + "//rtv2upload.php", "post", loginData));
//                        }
//                        catch (Exception ex)
//                        {
//                            MessageBox.Show("File downloading failed!" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
//                            return;
//                        }
//                        string destinationFileName = "";
//                        frmInput frminput = new frmInput();
//                        frminput.defaultFileName = GetDefaultFileName(strFilePath);
//                        if (frminput.ShowDialog() == DialogResult.OK)
//                        {
//                            if (!string.IsNullOrEmpty(frminput.txtbxInput.Text.Trim()))
//                            {
//                                destinationFileName = Path.Combine(Path.GetDirectoryName(ProtocolGateway.ProtocolGatewayConfigurationFile), frminput.txtbxInput.Text.Trim());
//                                frminput.Close();
//                            }
//                        }
//                        else { return; }
//                        client.DownloadFile(new Uri(OppWebAddr + ":/mnt/app/database/openproplus_config.xml"), "d:\\Test.xml");
//                    }
//                    catch (Renci.SshNet.Common.SshException ex)
//                    {
//                        MessageBox.Show("File downloading failed!" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
//                    }
//                    finally
//                    {
//                        if (client != null) { client.Dispose(); client = null; }
//                    }
//                }
//            }
//            catch (Exception ex) { MessageBox.Show("File downloading failed! " + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1); }
//        }
//        #endregion

//        #endregion
//        //Ajay: 09/01/2018
//        private string GetDefaultFileName(string Path)
//        {
//            string FileName = "";
//            int iCount = System.IO.Directory.GetFiles(Path).Where(x => System.IO.Path.GetExtension(x) == ".xml").Count();
//            if (iCount > 0)
//            {
//                FileName = (iCount).ToString().PadLeft(3, '0') + ".xml";
//                //Swati: 31/08/2017
//                int i = iCount;
//                while (File.Exists(Path + @"\" + FileName))
//                {
//                    FileName = (i + 1).ToString().PadLeft(3, '0') + ".set";
//                    i++;
//                }
//            }
//            else
//            { FileName = "000.xml"; }
//            return FileName;
//        }
//        //Ajay: 07/12/2018
//        private void tsbtnFTPConfig_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: tsbtnFTPConfig_Click";
//            try
//            {
//                frmFTP ftp = new frmFTP();
//                ftp.ShowDialog();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        //Ajay: 29/12/2018
//        private void dRConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: dRConfigurationToolStripMenuItem_Click";
//            try
//            {
//                this.scParser.Panel2.Controls.Clear();//Remove all previous controls...
//                //DRConfiguration drConfiguration = new DRConfiguration();
//                if (ucsDRConfig == null) ucsDRConfig = new ucDRConfig(this, opcHandle);
//                if (drConfiguration == null) drConfiguration = new DRConfiguration(ucsDRConfig); //Ajay: 29/12/2018

//                this.scParser.Panel2.Controls.Add(ucsDRConfig);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        //Ajay: 08/01/2019
//        private void openProDevicesToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            string strRoutineName = "frmOpenProPlus: openProDevicesToolStripMenuItem_Click";
//            try
//            {
//                frmBonjour frm = new frmBonjour();
//                frm.ShowDialog();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private void mnuParser_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
//        {

//        }
//        private void toolStripButton1_Click(object sender, EventArgs e)
//        {
//            FrmSLD fr = new FrmSLD();
//            fr.ShowDialog();
//        }

//        public void ExportDataSetToCsvFile(DataTable DDT, string DestinationCsvDirectory)
//        {
//            string strRoutineName = "frmOpenProPlus:ExportDataSetToCsvFile";
//            try
//            {
//                int iFirstLine = 0;
//                {
//                    string CsvText = string.Empty;
//                    foreach (DataRow DDR in DDT.Rows)
//                    {
//                        CsvText = string.Empty;
//                        CsvText = DDR[6].ToString();
//                        //CsvText = DDR["Configuration"].ToString();
//                        //outputFile.WriteLine(CsvText.ToString());
//                        if (iFirstLine > 0)
//                        {
//                            File.AppendAllText(DestinationCsvDirectory, Environment.NewLine + CsvText);
//                            iFirstLine++;
//                        }
//                        else
//                        {
//                            File.AppendAllText(DestinationCsvDirectory, CsvText);
//                            iFirstLine++;
//                        }
//                    }
//                    System.Threading.Thread.Sleep(1000);
//                    //}
//                }
//                //}
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private void toolStripButton2_Click(object sender, EventArgs e)
//        {

//            FrmSLD c = new FrmSLD();
//            c.ShowDialog();

//        }
//    }
//}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Xml.Schema;
using System.Xml;
using System.IO.Compression;
using System.Threading;
using System.Net;
using System.Diagnostics;
using Microsoft.Win32;
using System.Drawing.Drawing2D;
using System.Reflection;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System.Collections.Specialized;
using System.Text;
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Tar;
using ICSharpCode.SharpZipLib.GZip;

namespace OpenProPlusConfigurator
{
    public partial class frmOpenProPlus : Form
    {
        #region Declarations
        OpenProPlus_Config opcHandle = null;
        frmProgess fp = null; //Namrata: 12/01/2018
        frmOverview fo = null;
        private bool showExitMessage = false;
        private string xmlFile = null;
        private bool iec104EventHandled = false;
        ucGroupIEC104 ucs104 = null;

        private bool SMSEventHandled = false;
        ucGroupSMSSlave ucSMS = null;
        //Namrata:6/7/2017
        ucGroupIEC101Slave ucs101 = null;
        private bool iec101EventHandled = false;
        public const int COLS_B4_MULTISLAVE = 13;
        public const int TOTAL_MAP_PARAMS = 7;
        public const int FILTER_PANEL_HEIGHT = 70;
        private int sortColumn = -1;
        ucRCBList ucRCB = null;
        //Namrata: 06/09/2017
        //View Recent File
        private MruList MyMruList;
        ucAIlist ucsai = null;
        ucAOList ucsao = null;
        ucDIlist ucsdi = null;
        ucDOlist ucsdo = null;
        ucENlist ucsen = null;
        ucLPFilelist ucslpfile = null;
        private bool IsXmlValid = false;
        ucRCBList uc = null; //Namrata: 12/01/2018
        List<RCB> aiList = new List<RCB>();
        ucAIlist ucai = null;//Namrata: 12/01/2018
        private AIConfiguration aicNodeforiec61850Client = null;
        private AOConfiguration aocNodeforiec61850Client = null;
        private DIConfiguration dicNodeforiec61850Client = null;
        private DOConfiguration docNodeforiec61850Client = null;
        private ENConfiguration encNodeforiec61850Client = null;
        private RCBConfiguration RCBNodeforiec61850Client = null;
        private ucDRConfig ucsDRConfig = null; //Ajay: 29/12/2018
        private DRConfiguration drConfiguration = null; //Ajay: 29/12/2018
        private bool VersionMatch = false; //Ajay: 11/01/2019
        DataTable table1 = new DataTable("IEC61850");
        string FolderNamewithoutexstension = "";
        OpenFileDialog fd = new OpenFileDialog();
        #endregion Declarations
        public frmOpenProPlus()
        {
            InitializeComponent();
        }
        private void UpdateXMLFile(string FileName)
        {
            string strRoutineName = "frmOpenProPlus: UpdateXMLFile";
            try
            {
                if (!string.IsNullOrEmpty(FileName))
                {
                    xmlFile = FileName;
                    toolStripStatusLabel1.Visible = true;
                    tspFileName.Text = FileName;
                }
                else
                {
                    toolStripStatusLabel1.Visible = true;
                    xmlFile = tspFileName.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 23/11/2018
        private void ReadProtocolGatewayXML()
        {
            string strRoutineName = "frmOpenProPlus: ReadProtocolGatewayXML";
            try
            {
                if (File.Exists(ProtocolGateway.ProtocolGatewayConfigurationFile))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(ProtocolGateway.ProtocolGatewayConfigurationFile);
                    if (doc != null)
                    {
                        XmlElement root = doc.DocumentElement;
                        XmlNodeList xnl = root.ChildNodes;
                        XmlAttribute xaVisible = null;
                        XmlAttribute xaReadOnly = null;
                        XmlNodeList xnlMainNode = null;

                        xnlMainNode = root.SelectNodes("//Details");
                        if (xnlMainNode.Count > 0)
                        {
                            xaVisible = xnlMainNode[0].Attributes["Visible"];
                            if (xaVisible != null) ProtocolGateway.OppDetails_Visible = Convert.ToBoolean(xaVisible.Value);
                            xaReadOnly = xnlMainNode[0].Attributes["ReadOnly"];
                            if (xaReadOnly != null) ProtocolGateway.OppDetails_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
                        }
                        xnlMainNode = root.SelectNodes("//NetWorkConfiguration");
                        if (xnlMainNode.Count > 0)
                        {
                            xaVisible = xnlMainNode[0].Attributes["Visible"];
                            if (xaVisible != null) ProtocolGateway.OppNetWorkConfiguration_Visible = Convert.ToBoolean(xaVisible.Value);
                            xaReadOnly = xnlMainNode[0].Attributes["ReadOnly"];
                            if (xaReadOnly != null) ProtocolGateway.OppNetWorkConfiguration_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
                        }
                        xnlMainNode = root.SelectNodes("//SerialPortConfiguration");
                        if (xnlMainNode.Count > 0)
                        {
                            xaVisible = xnlMainNode[0].Attributes["Visible"];
                            if (xaVisible != null) ProtocolGateway.OppSerialPortConfiguration_Visible = Convert.ToBoolean(xaVisible.Value);
                            xaReadOnly = xnlMainNode[0].Attributes["ReadOnly"];
                            if (xaReadOnly != null) ProtocolGateway.OppSerialPortConfiguration_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
                        }
                        xnlMainNode = root.SelectNodes("//SystemConfiguration");
                        if (xnlMainNode.Count > 0)
                        {
                            xaVisible = xnlMainNode[0].Attributes["Visible"];
                            if (xaVisible != null) ProtocolGateway.OppSystemConfiguration_Visible = Convert.ToBoolean(xaVisible.Value);
                            xaReadOnly = xnlMainNode[0].Attributes["ReadOnly"];
                            if (xaReadOnly != null) ProtocolGateway.OppSystemConfiguration_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
                        }
                        xnlMainNode = root.SelectNodes("//SlaveConfiguration");
                        if (xnlMainNode.Count > 0)
                        {
                            xaVisible = xnlMainNode[0].Attributes["Visible"];
                            if (xaVisible != null) ProtocolGateway.OppSlaveConfiguration_Visible = Convert.ToBoolean(xaVisible.Value);
                            xaReadOnly = xnlMainNode[0].Attributes["ReadOnly"];
                            if (xaReadOnly != null) ProtocolGateway.OppSlaveConfiguration_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);

                            xnlMainNode[0].ChildNodes.OfType<XmlNode>().ToList().ForEach(xn =>
                            {
                                if (xn.Name == "IEC104SlaveGroup")
                                {
                                    xaVisible = xn.Attributes["Visible"];
                                    if (xaVisible != null) ProtocolGateway.OppIEC104SlaveGroup_Visible = Convert.ToBoolean(xaVisible.Value);
                                    xaReadOnly = xn.Attributes["ReadOnly"];
                                    if (xaReadOnly != null) ProtocolGateway.OppIEC104SlaveGroup_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
                                }
                                else if (xn.Name == "MODBUSSlaveGroup")
                                {
                                    xaVisible = xn.Attributes["Visible"];
                                    if (xaVisible != null) ProtocolGateway.OppMODBUSSlaveGroup_Visible = Convert.ToBoolean(xaVisible.Value);
                                    xaReadOnly = xn.Attributes["ReadOnly"];
                                    if (xaReadOnly != null) ProtocolGateway.OppMODBUSSlaveGroup_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
                                }
                                else if (xn.Name == "IEC101SlaveGroup")
                                {
                                    xaVisible = xn.Attributes["Visible"];
                                    if (xaVisible != null) ProtocolGateway.OppIEC101SlaveGroup_Visible = Convert.ToBoolean(xaVisible.Value);
                                    xaReadOnly = xn.Attributes["ReadOnly"];
                                    if (xaReadOnly != null) ProtocolGateway.OppIEC101SlaveGroup_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
                                }
                                else if (xn.Name == "IEC61850ServerGroup")
                                {
                                    xaVisible = xn.Attributes["Visible"];
                                    if (xaVisible != null) ProtocolGateway.OppIEC61850SlaveGroup_Visible = Convert.ToBoolean(xaVisible.Value);
                                    xaReadOnly = xn.Attributes["ReadOnly"];
                                    if (xaReadOnly != null) ProtocolGateway.OppIEC61850SlaveGroup_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
                                }
                                else if (xn.Name == "SPORTSlaveGroup")
                                {
                                    xaVisible = xn.Attributes["Visible"];
                                    if (xaVisible != null) ProtocolGateway.OppSPORTSlaveGroup_Visible = Convert.ToBoolean(xaVisible.Value);
                                    xaReadOnly = xn.Attributes["ReadOnly"];
                                    if (xaReadOnly != null) ProtocolGateway.OppSPORTSlaveGroup_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
                                }
                                //Namrata:02/04/2019
                                else if (xn.Name == "MQTTSlaveGroup")
                                {
                                    xaVisible = xn.Attributes["Visible"];
                                    if (xaVisible != null) ProtocolGateway.OppMQTTSlaveGroup_Visible = Convert.ToBoolean(xaVisible.Value);
                                    xaReadOnly = xn.Attributes["ReadOnly"];
                                    if (xaReadOnly != null) ProtocolGateway.OppMQTTSlaveGroup_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
                                }
                                //Namrata:25/05/2019
                                else if (xn.Name == "SMSSlaveGroup")
                                {
                                    xaVisible = xn.Attributes["Visible"];
                                    if (xaVisible != null) ProtocolGateway.OppSMSSlaveGroup_Visible = Convert.ToBoolean(xaVisible.Value);
                                    xaReadOnly = xn.Attributes["ReadOnly"];
                                    if (xaReadOnly != null) ProtocolGateway.OppSMSSlaveGroup_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
                                }
                                else if (xn.Name == "GraphicalDisplaySlaveGroup")
                                {
                                    xaVisible = xn.Attributes["Visible"];
                                    if (xaVisible != null) ProtocolGateway.OppGraphicalDisplaySlaveGroup_Visible = Convert.ToBoolean(xaVisible.Value);
                                    xaReadOnly = xn.Attributes["ReadOnly"];
                                    if (xaReadOnly != null) ProtocolGateway.OppGraphicalDisplaySlaveGroup_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
                                }
                                else if (xn.Name == "DNP3SlaveGroup")
                                {
                                    xaVisible = xn.Attributes["Visible"];
                                    if (xaVisible != null) ProtocolGateway.OppDNP3SlaveGroup_Visible = Convert.ToBoolean(xaVisible.Value);
                                    xaReadOnly = xn.Attributes["ReadOnly"];
                                    if (xaReadOnly != null) ProtocolGateway.OppDNP3SlaveGroup_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
                                }
                            });
                        }
                        else { }
                        xnlMainNode = root.SelectNodes("//MasterConfiguration");
                        if (xnlMainNode.Count > 0)
                        {
                            xaVisible = xnlMainNode[0].Attributes["Visible"];
                            if (xaVisible != null) ProtocolGateway.OppMasterConfiguration_Visible = Convert.ToBoolean(xaVisible.Value);
                            xaReadOnly = xnlMainNode[0].Attributes["ReadOnly"];
                            ProtocolGateway.OppMasterConfiguration_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);

                            xnlMainNode[0].ChildNodes.OfType<XmlNode>().ToList().ForEach(xn =>
                            {
                                if (xn.Name == "ADRGroup")
                                {
                                    xaVisible = xn.Attributes["Visible"];
                                    if (xaVisible != null) ProtocolGateway.OppADRGroup_Visible = Convert.ToBoolean(xaVisible.Value);
                                    xaReadOnly = xn.Attributes["ReadOnly"];
                                    if (xaReadOnly != null) ProtocolGateway.OppADRGroup_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
                                }
                                else if (xn.Name == "IEC101Group")
                                {
                                    xaVisible = xn.Attributes["Visible"];
                                    if (xaVisible != null) ProtocolGateway.OppIEC101Group_Visible = Convert.ToBoolean(xaVisible.Value);
                                    xaReadOnly = xn.Attributes["ReadOnly"];
                                    if (xaReadOnly != null) ProtocolGateway.OppIEC101Group_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
                                }
                                else if (xn.Name == "IEC103Group")
                                {
                                    xaVisible = xn.Attributes["Visible"];
                                    if (xaVisible != null) ProtocolGateway.OppIEC103Group_Visible = Convert.ToBoolean(xaVisible.Value);
                                    xaReadOnly = xn.Attributes["ReadOnly"];
                                    if (xaReadOnly != null) ProtocolGateway.OppIEC103Group_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
                                }
                                else if (xn.Name == "MODBUSGroup")
                                {
                                    xaVisible = xn.Attributes["Visible"];
                                    if (xaVisible != null) ProtocolGateway.OppMODBUSGroup_Visible = Convert.ToBoolean(xaVisible.Value);
                                    xaReadOnly = xn.Attributes["ReadOnly"];
                                    if (xaReadOnly != null) ProtocolGateway.OppMODBUSGroup_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
                                }
                                else if (xn.Name == "IEC61850ClientGroup")
                                {
                                    xaVisible = xn.Attributes["Visible"];
                                    if (xaVisible != null) ProtocolGateway.OppIEC61850Group_Visible = Convert.ToBoolean(xaVisible.Value);
                                    xaReadOnly = xn.Attributes["ReadOnly"];
                                    if (xaReadOnly != null) ProtocolGateway.OppIEC61850Group_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
                                }
                                else if (xn.Name == "IEC104Group")
                                {
                                    xaVisible = xn.Attributes["Visible"];
                                    if (xaVisible != null) ProtocolGateway.OppIEC104Group_Visible = Convert.ToBoolean(xaVisible.Value);
                                    xaReadOnly = xn.Attributes["ReadOnly"];
                                    if (xaReadOnly != null) ProtocolGateway.OppIEC104Group_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
                                }
                                else if (xn.Name == "SPORTGroup")
                                {
                                    xaVisible = xn.Attributes["Visible"];
                                    if (xaVisible != null) ProtocolGateway.OppSPORTGroup_Visible = Convert.ToBoolean(xaVisible.Value);
                                    xaReadOnly = xn.Attributes["ReadOnly"];
                                    if (xaReadOnly != null) ProtocolGateway.OppSPORTGroup_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
                                }
                                else if (xn.Name == "VirtualGroup")
                                {
                                    xaVisible = xn.Attributes["Visible"];
                                    if (xaVisible != null) ProtocolGateway.OppVirtualGroup_Visible = Convert.ToBoolean(xaVisible.Value);
                                    xaReadOnly = xn.Attributes["ReadOnly"];
                                    if (xaReadOnly != null) ProtocolGateway.OppVirtualGroup_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
                                }
                                else if (xn.Name == "LoadProfileGroup")
                                {
                                    xaVisible = xn.Attributes["Visible"];
                                    if (xaVisible != null) ProtocolGateway.OppLoadProfileGroup_Visible = Convert.ToBoolean(xaVisible.Value);
                                    xaReadOnly = xn.Attributes["ReadOnly"];
                                    if (xaReadOnly != null) ProtocolGateway.OppLoadProfileGroup_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
                                }

                            });
                        }
                        else { }

                        xnlMainNode = root.SelectNodes("//ParameterLoadConfiguration");
                        if (xnlMainNode.Count > 0)
                        {
                            xaVisible = xnlMainNode[0].Attributes["Visible"];
                            if (xaVisible != null) ProtocolGateway.OppParameterLoadConfiguration_Visible = Convert.ToBoolean(xaVisible.Value);
                            xaReadOnly = xnlMainNode[0].Attributes["ReadOnly"];
                            if (xaReadOnly != null) ProtocolGateway.OppParameterLoadConfiguration_ReadOnly = Convert.ToBoolean(xaReadOnly.Value);
                        }
                    }
                    else { }
                }
                else { }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void frmParser_Load(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: frmParser_Load";
            try
            {

                pnlValidationMessages.SendToBack();
                
                ssParser.Hide();
                //Ajay: 23/11/2018
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    //Ajay: 09/01/2019
                    openToolStripMenuItem.Enabled = false;
                    newToolStripMenuItem.Enabled = false;
                    recentFilesToolStripMenuItem.Enabled = false;
                    tsbNew.Enabled = false;
                    tsbOpen.Enabled = false;
                    tsbtnFTPConfig.Visible = false;

                    if (string.IsNullOrEmpty(ProtocolGateway.OppIpAddress) || string.IsNullOrEmpty(ProtocolGateway.User) || string.IsNullOrEmpty(ProtocolGateway.Password))
                    {
                        tsbtnFileDownload.Enabled = false;
                        tsbtnFileUpload.Enabled = false;
                    }
                    if (!string.IsNullOrEmpty(ProtocolGateway.ProtocolGatewayConfigurationFile))
                    {
                        ReadProtocolGatewayXML();
                        //Ajay: 14/12/2018
                        Globals.ZONE_RESOURCES_PATH = Path.GetDirectoryName(ProtocolGateway.ProtocolGatewayConfigurationFile) + @"\" + "resources" + @"\";
                        Globals.RESOURCES_PATH = Path.GetDirectoryName(ProtocolGateway.ProtocolGatewayConfigurationFile) + @"\" + "resources" + @"\";
                    }
                }
                //Ajay: 07/12/2018
                else if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Full)
                {
                    tsbtnFTPConfig.Visible = true;
                    if (Properties.Settings.Default["OppIPAddress"] != null)
                    {
                        ProtocolGateway.OppIpAddress = Properties.Settings.Default["OppIPAddress"].ToString();
                    }
                    if (Properties.Settings.Default["FTPUser"] != null)
                    {
                        ProtocolGateway.User = Properties.Settings.Default["FTPUser"].ToString();
                    }
                    if (Properties.Settings.Default["FTPPassword"] != null)
                    {
                        ProtocolGateway.Password = Properties.Settings.Default["FTPPassword"].ToString();
                    }
                    if (Properties.Settings.Default["FTProtocol"] != null)
                    {
                        ProtocolGateway.Protocol = Properties.Settings.Default["FTProtocol"].ToString();
                    }
                }

                //Namrata: 18/04/2018
                //Delete Folder from AppData
                ICDFilesData.ICDDirMFile = System.IO.Path.GetTempPath() + "protocol";
                if (System.IO.Directory.Exists(ICDFilesData.ICDDirMFile))
                {
                    string DirectoryNameDelete = System.IO.Path.GetTempPath() + "protocol"; /*System.IO.Path.GetTempPath() + @"\" + "IEC61850_Client";*/
                    FileDirectoryOperations fileDirectoryOperations = new FileDirectoryOperations();
                    FileDirectoryOperations.DeleteDirectory(DirectoryNameDelete);
                }
                //Namrata: 23/04/2018
                //Delete Folder from ProgramData
                string DPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName;
                if (DPath != "")
                {
                    if (ofdXMLFile.FileName != "")
                    {
                        string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(ofdXMLFile.FileName) + "\\" + "Protocol" + "\\" + "IEC61850Client";/* Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(ofdXMLFile.FileName) + "\\" + "IEC61850_Client" + "\\" + "ProtocolConfiguration";*/
                        string ImportedDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(Utils.DirectoryPath);// 
                        if (System.IO.Directory.Exists(path))
                        {
                            FileDirectoryOperations fileDirectoryOperation = new FileDirectoryOperations();
                            FileDirectoryOperations.DeleteDirectory(ImportedDirectoryPath);
                        }
                    }
                    else
                    {
                        FileDirectoryOperations fileDirectoryOperation = new FileDirectoryOperations();
                        FileDirectoryOperations.DeleteDirectory(DPath);
                    }
                }
                fp = new frmProgess();
                uc = new ucRCBList();
                ucai = new ucAIlist();
                toolStripStatusLabel1.Visible = false;
                lvValidationMessages.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
                lvValidationMessages.Scrollable = true;
                Utils.SetFormIcon(this);
                //Namrata: 31/08/2017
                //Check if Zone file exists...
                if (!File.Exists(Globals.ZONE_RESOURCES_PATH + Globals.TIME_ZONE_LIST))
                {
                    MessageBox.Show("Zone file (" + Globals.ZONE_RESOURCES_PATH + Globals.TIME_ZONE_LIST + ") is missing. Exiting application!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
                #region XSD Validations
                //Check if XSD file exists...
                if (!File.Exists(Globals.RESOURCES_PATH + Globals.XSD_FILENAME))
                {
                    MessageBox.Show("Schema file (" + Globals.RESOURCES_PATH + Globals.XSD_FILENAME + ") is missing. Exiting application!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
                if (!File.Exists(Globals.RESOURCES_PATH + Globals.XSD_DATATYPE_FILENAME))
                {
                    MessageBox.Show("Schema file (" + Globals.RESOURCES_PATH + Globals.XSD_DATATYPE_FILENAME + ") is missing. Exiting application!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                if (!Utils.IsXMLWellFormed(Globals.RESOURCES_PATH + Globals.XSD_FILENAME))
                {
                    MessageBox.Show("Schema file (" + Globals.RESOURCES_PATH + Globals.XSD_FILENAME + ") is not a valid XML. Exiting application!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
                if (!Utils.IsXMLWellFormed(Globals.RESOURCES_PATH + Globals.XSD_DATATYPE_FILENAME))
                {
                    MessageBox.Show("Schema file (" + Globals.RESOURCES_PATH + Globals.XSD_DATATYPE_FILENAME + ") is not a valid XML. Exiting application!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
                #endregion XSD Validations
                showExitMessage = true;
                lvValidationMessages.Columns.Add("Validation Messages...", 1000, HorizontalAlignment.Left);
                ResetConfiguratorState(true);
                HandleMapViewChange();
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Full) //Ajay: 09/01/2019
                {
                    //Namrata: 28/12/2017
                    MyMruList = new MruList(Application.ProductName, this.recentFilesToolStripMenuItem, 10, this.myOwnRecentFilesGotCleared_handler);
                    MyMruList.FileSelected += MyMruList_FileSelected;
                }
                //Namrata: 19/12/2017
                //Ajay: 23/11/2018
                //TreeNode searchNode = tvItems.Nodes[0].Nodes[5];
                TreeNode searchNode = tvItems.Nodes[0].Nodes["MasterConfiguration"];
                if (searchNode != null)
                    searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });

                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    //EditNwConfigurationInXMLFile(); //Ajay: 08/01/2018

                    openXMLFile();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void myOwnRecentFilesGotCleared_handler(object obj, EventArgs evt)
        {
        }
        private void MyMruList_FileSelected(string file_name)
        {
            string strRoutineName = "frmOpenProPlus: MyMruList_FileSelected";
            try
            {
                //Namrata: 25/11/2019
                string extractPath = System.IO.Path.GetTempPath() + "protocol";//Path.GetDirectoryName(openedFile);
                string ZIPName = Path.GetDirectoryName(file_name);
                string ZipFolderName = Path.GetFileNameWithoutExtension(file_name);
                string GetXMLPath = Path.Combine(ZIPName, ZipFolderName);

                Utils.DirNameSave = ZIPName + @"\" + ZipFolderName;
                if (System.IO.Directory.Exists(Utils.DirNameSave))
                {
                    System.IO.Directory.Delete(Utils.DirNameSave, true);
                }
                if (!System.IO.Directory.Exists(extractPath))
                {
                    System.IO.Directory.CreateDirectory(extractPath);
                    ZipFile.ExtractToDirectory(file_name, GetXMLPath);
                }
                else
                {
                    System.IO.Directory.Delete(extractPath, true);
                    System.IO.Directory.CreateDirectory(extractPath);
                    ZipFile.ExtractToDirectory(file_name, GetXMLPath);
                }
                string[] fileEntries1 = System.IO.Directory.GetFiles(GetXMLPath, "*.xml");

                string fileName1 = string.Empty;
                string destFile1 = string.Empty;
                foreach (string s in fileEntries1)
                {
                    fileName1 = System.IO.Path.GetFileName(s);
                    destFile1 = System.IO.Path.Combine(extractPath, fileName1);
                    System.IO.File.Move(s, destFile1);
                    ofdXMLFile.FileName = destFile1;
                }
                string Protocol = "protocol";
                string GetProtocol = Path.Combine(GetXMLPath, Protocol);
                if (!System.IO.Directory.Exists(GetProtocol))
                {
                    if (!CheckVersions(ofdXMLFile.FileName))
                    {
                        VersionMatch = false;
                        DialogResult rslt = MessageBox.Show("Do you still want to continue with mismatched versions of OpenPro+Configurator, xml and xsd files?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (rslt == DialogResult.No)
                        {
                            return;
                        }
                        else { }
                    }
                    else
                    {
                        VersionMatch = true; //Ajay: 11/01/2019
                    }
                }
                //Ajay: 11/01/2019
                //    if (!CheckVersions(file_name))
                //{
                //    VersionMatch = false;
                //    DialogResult rslt = MessageBox.Show("Do you still want to continue with mismatched versions of OpenPro+Configurator, xml and xsd files?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                //    if (rslt == DialogResult.No)
                //    {
                //        return;
                //    }
                //    else { }
                //}
                //else { VersionMatch = true; }

                OpenFile(file_name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void OpenFile(string file_name)
        {
            string strRoutineName = "frmOpenProPlus: OpenFile";
            try
            {
                lvValidationMessages.Items.Clear();
                RichTextBox RichtextBox = new RichTextBox();
                RichtextBox.Clear();
                string DirectoryExtention = Path.GetExtension(file_name);
                if (DirectoryExtention == ".zip")
                {
                    string extractPath = System.IO.Path.GetTempPath() + "protocol"; //C:\Users\swatin\AppData\Local\Temp\protocol
                    string ZIPName = Path.GetDirectoryName(file_name);
                    string ZipFolderName = Path.GetFileNameWithoutExtension(file_name);
                    string GetXMLPath = Path.Combine(ZIPName, ZipFolderName);

                    Utils.DirNameSave = ZIPName + @"\" + ZipFolderName;
                    if (System.IO.Directory.Exists(Utils.DirNameSave))
                    {
                        System.IO.Directory.Delete(Utils.DirNameSave, true);
                    }
                    if (!System.IO.Directory.Exists(extractPath))
                    {
                        System.IO.Directory.CreateDirectory(extractPath);
                        ZipFile.ExtractToDirectory(file_name, GetXMLPath);
                    }
                    else
                    {
                        System.IO.Directory.Delete(extractPath, true);
                        System.IO.Directory.CreateDirectory(extractPath);
                        ZipFile.ExtractToDirectory(file_name, GetXMLPath);
                    }
                    string[] fileEntries1 = System.IO.Directory.GetFiles(GetXMLPath, "*.xml");

                    string fileName1 = string.Empty;
                    string destFile1 = string.Empty;
                    foreach (string s in fileEntries1)
                    {
                        fileName1 = System.IO.Path.GetFileName(s);
                        destFile1 = System.IO.Path.Combine(extractPath, fileName1);
                        System.IO.File.Move(s, destFile1);
                        ofdXMLFile.FileName = destFile1;
                    }
                    string Protocol = "protocol";
                    string GetProtocol = Path.Combine(GetXMLPath, Protocol);
                    if (!System.IO.Directory.Exists(GetProtocol))
                    {
                        if (!CheckVersions(GetXMLPath))
                        {
                            VersionMatch = false;
                            DialogResult rslt = MessageBox.Show("Do you still want to continue with mismatched versions of OpenPro+Configurator, xml and xsd files?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            if (rslt == DialogResult.No)
                            {
                                return;
                            }
                            else { }
                        }
                        else
                        {
                            VersionMatch = true; //Ajay: 11/01/2019
                        }
                        //Namrata:26/04/2018
                        Utils.XMLFolderPath = Path.GetDirectoryName(GetProtocol);

                        #region IEC61850
                        //Namrata: 27/04/2018
                        ICDFilesData.DirectoryName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
                        ICDFilesData.XmlName = Path.GetFileName(ofdXMLFile.FileName); // Get XML Name
                        ICDFilesData.XmlPath = ofdXMLFile.FileName; // XML with full path
                        ICDFilesData.XmlWithoutExt = "";

                        #region ClearDatasets
                        Utils.dsIED.Clear();
                        Utils.dsIED.Tables.Clear();
                        Utils.DsRCB.Clear();
                        Utils.DsRCBData.Clear();
                        Utils.dsResponseType.Clear();
                        Utils.DsResponseType.Clear();
                        Utils.dsIED.Clear();
                        Utils.dsIEDName.Clear();
                        Utils.DsAllConfigurationData.Clear();
                        Utils.DsAllConfigureData.Clear();
                        Utils.DsRCBDataset.Clear();
                        Utils.DsRCBDS.Clear();
                        Utils.DtRCBdata.Clear();
                        #endregion ClearDatasets
                        #endregion IEC61850

                        #region GDisplay
                        //Namrata: 27/04/2018
                        GDSlave.DirName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
                        GDSlave.SLDName = Path.GetFileName(ofdXMLFile.FileName); //Get XML Name
                        GDSlave.SLDPath = ofdXMLFile.FileName;//XML with full path
                        GDSlave.SLDWithoutExt = "";
                        #region ClearDatasets
                        GDSlave.DsExcelData.Tables.Clear();
                        GDSlave.DsExcelData.Clear();
                        GDSlave.DtExcelData.Clear();
                        GDSlave.DsExportData.Tables.Clear();
                        GDSlave.DTAIImage.Clear();
                        GDSlave.DTDIDOImage.Clear();
                        #endregion ClearDatasets
                        #endregion GDisplay
                        this.MyMruList.AddFile(file_name); //Now give it to the MRUManager
                        int result = 0;
                        string errMsg = "XML file is valid...";
                        ResetConfiguratorState(false);
                        showLoading();
                        xmlFile = "";//reset old filename...
                        pnlValidationMessages.Visible = true;
                        ListViewItem lvi;
                        lvi = new ListViewItem("Validating file: " + ofdXMLFile.FileName);
                        //Namrata: 27/7/2017
                        toolStripStatusLabel1.Visible = true;  //Display File Name in Toolstrip 
                        tspFileName.Text = file_name;
                        lvValidationMessages.Items.Add(lvi);
                        lvi = new ListViewItem("");
                        lvValidationMessages.Items.Add(lvi);
                        result = opcHandle.loadXML(ofdXMLFile.FileName, tvItems, out IsXmlValid);
                        if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
                        else
                        {
                            pnlValidationMessages.Visible = true;
                            pnlValidationMessages.BringToFront();
                        }
                        if (result == -1) errMsg = "File doesnot exist!!!";
                        else if (result == -2) errMsg = "File is not a well-formed XML!!!";
                        else if (result == -3) errMsg = "XSD file is not valid!!!";
                        else if (result == -4) errMsg = "File is not a valid XML, as per the schema!!!";
                        else if (result == -5) errMsg = "MODBUS Slave Port is Not Valid as per the schema";
                        lvi = new ListViewItem("");
                        lvValidationMessages.Items.Add(lvi);
                        lvi = new ListViewItem(errMsg);
                        lvValidationMessages.Items.Add(lvi);
                        lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
                        if (result < 0)
                        {
                            hideLoading();
                            MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        xmlFile = ofdXMLFile.FileName; //Assign only after loading file...
                                                       //Namrata: 19/12/2017
                                                       //Ajay: 29/11/2018
                        TreeNode searchNode = tvItems.Nodes[0].Nodes["MasterConfiguration"];
                        if (searchNode != null)
                            searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
                        tvItems.SelectedNode = tvItems.Nodes[0];
                        tvItems.Nodes[0].EnsureVisible();
                        hideLoading();
                    }
                    else
                    {
                        string[] subdirectoryEntries = System.IO.Directory.GetDirectories(GetProtocol);
                        foreach (string subdirectory in subdirectoryEntries)
                        {
                            //Namrata: 01/11/2019
                            if (subdirectory.Contains("protocol"))
                            {
                                int index = subdirectory.IndexOf("protocol");
                                GetProtocolFolder = subdirectory.Substring(subdirectory.IndexOf("protocol") + 9);
                            }

                            GDSlave.SubDirName = extractPath + "\\" + GetProtocolFolder;
                            string CopyDir = extractPath + "\\" + GetProtocolFolder;

                            if (System.IO.Directory.Exists(GDSlave.SubDirName))
                            {
                                System.IO.Directory.Delete(GDSlave.SubDirName, true);
                                var diSource = new DirectoryInfo(GetProtocol + "\\" + GetProtocolFolder);
                                var diTarget = new DirectoryInfo(CopyDir);
                                CopyAll(diSource, diTarget);
                            }
                            else
                            {
                                var diSource1 = new DirectoryInfo(GetProtocol + "\\" + GetProtocolFolder);
                                var diTarget1 = new DirectoryInfo(CopyDir);
                                CopyAll(diSource1, diTarget1);
                            }
                        }

                        if (System.IO.Directory.Exists(GetXMLPath))
                        {
                            System.IO.Directory.Delete(GetXMLPath, true);
                        }
                        //#endregion Namrata
                        //Ajay: 11/01/2019
                        if (!CheckVersions(ofdXMLFile.FileName))
                        {
                            VersionMatch = false;
                            DialogResult rslt = MessageBox.Show("Do you still want to continue with mismatched versions of OpenPro+Configurator, xml and xsd files?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            if (rslt == DialogResult.No)
                            {
                                return;
                            }
                            else { }
                        }
                        else
                        {
                            VersionMatch = true; //Ajay: 11/01/2019
                        }
                        //Namrata:26/04/2018
                        Utils.XMLFolderPath = Path.GetDirectoryName(ofdXMLFile.FileName);

                        #region IEC61850
                        //Namrata: 27/04/2018
                        ICDFilesData.DirectoryName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
                        ICDFilesData.XmlName = Path.GetFileName(ofdXMLFile.FileName); // Get XML Name
                        ICDFilesData.XmlPath = ofdXMLFile.FileName; // XML with full path
                        ICDFilesData.XmlWithoutExt = "";// Path.GetFileNameWithoutExtension(Utils.XMLFileFullPath);
                                                        //Namrata: 29/03/2018
                        #region ClearDatasets
                        Utils.dsIED.Clear();
                        Utils.dsIED.Tables.Clear();
                        Utils.DsRCB.Clear();
                        Utils.DsRCBData.Clear();
                        Utils.dsResponseType.Clear();
                        Utils.DsResponseType.Clear();
                        Utils.dsIED.Clear();
                        Utils.dsIEDName.Clear();
                        Utils.DsAllConfigurationData.Clear();
                        Utils.DsAllConfigureData.Clear();
                        Utils.DsRCBDataset.Clear();
                        Utils.DsRCBDS.Clear();
                        Utils.DtRCBdata.Clear();
                        #endregion ClearDatasets
                        #endregion IEC61850

                        #region GDisplay
                        //Namrata: 27/04/2018
                        GDSlave.DirName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
                        GDSlave.SLDName = Path.GetFileName(ofdXMLFile.FileName); //Get XML Name
                        GDSlave.SLDPath = ofdXMLFile.FileName;//XML with full path
                        GDSlave.SLDWithoutExt = "";
                        #region ClearDatasets
                        GDSlave.DsExcelData.Tables.Clear();
                        GDSlave.DsExcelData.Clear();
                        GDSlave.DtExcelData.Clear();
                        GDSlave.DsExportData.Tables.Clear();
                        GDSlave.DTAIImage.Clear();
                        GDSlave.DTDIDOImage.Clear();
                        #endregion ClearDatasets
                        #endregion GDisplay
                        this.MyMruList.AddFile(file_name); //Now give it to the MRUManager
                        int result = 0;
                        string errMsg = "XML file is valid...";
                        ResetConfiguratorState(false);
                        showLoading();
                        xmlFile = "";//reset old filename...
                        pnlValidationMessages.Visible = true;
                        ListViewItem lvi;
                        lvi = new ListViewItem("Validating file: " + ofdXMLFile.FileName);
                        //Namrata: 27/7/2017
                        toolStripStatusLabel1.Visible = true;  //Display File Name in Toolstrip 
                        //tspFileName.Text = openedFile + @"\" + ICDFilesData.XmlName;//ofdXMLFile.FileName;
                        lvValidationMessages.Items.Add(lvi);
                        lvi = new ListViewItem("");
                        lvValidationMessages.Items.Add(lvi);
                        result = opcHandle.loadXML(ofdXMLFile.FileName, tvItems, out IsXmlValid);
                        if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
                        else
                        {
                            pnlValidationMessages.Visible = true;
                            pnlValidationMessages.BringToFront();
                        }
                        if (result == -1) errMsg = "File doesnot exist!!!";
                        else if (result == -2) errMsg = "File is not a well-formed XML!!!";
                        else if (result == -3) errMsg = "XSD file is not valid!!!";
                        else if (result == -4) errMsg = "File is not a valid XML, as per the schema!!!";
                        else if (result == -5) errMsg = "MODBUS Slave Port is Not Valid as per the schema";
                        lvi = new ListViewItem("");
                        lvValidationMessages.Items.Add(lvi);
                        lvi = new ListViewItem(errMsg);
                        lvValidationMessages.Items.Add(lvi);
                        lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
                        if (result < 0)
                        {
                            hideLoading();
                            MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        xmlFile = ofdXMLFile.FileName; //Assign only after loading file...
                                                       //Namrata: 19/12/2017
                                                       //Ajay: 29/11/2018
                        TreeNode searchNode = tvItems.Nodes[0].Nodes["MasterConfiguration"];
                        if (searchNode != null)
                            searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
                        tvItems.SelectedNode = tvItems.Nodes[0];
                        tvItems.Nodes[0].EnsureVisible();
                        hideLoading();
                    }
                }
                else
                {
                    if (file_name.ToLower().EndsWith(".rtf"))
                    {
                        RichtextBox.LoadFile(file_name);
                    }
                    else
                    {
                        #region ClearDatasets
                        Utils.DsRCB.Clear();
                        Utils.DsRCBData.Clear();
                        Utils.dsResponseType.Clear();
                        Utils.DsResponseType.Clear();
                        Utils.dsIED.Clear();
                        Utils.dsIEDName.Clear();
                        Utils.DsAllConfigurationData.Clear();
                        Utils.DsAllConfigureData.Clear();
                        Utils.DsRCBDataset.Clear();
                        Utils.DsRCBDS.Clear();
                        Utils.DtRCBdata.Clear();
                        #endregion ClearDatasets

                        RichtextBox.Text = File.ReadAllText(file_name);
                        Utils.XMLFolderPath = Path.GetDirectoryName(file_name);
                        int result = 0;
                        string errMsg = "XML file is valid...";
                        ResetConfiguratorState(false);
                        showLoading();

                        xmlFile = "";//reset old filename...
                        ListViewItem lvi;
                        lvi = new ListViewItem("Validating file: " + file_name);
                        //Namrata: 27/7/2017
                        toolStripStatusLabel1.Visible = true;  //Display File Name in Toolstrip 
                        tspFileName.Text = file_name;
                        lvValidationMessages.Items.Add(lvi);
                        lvi = new ListViewItem("");
                        lvValidationMessages.Items.Add(lvi);
                        result = opcHandle.loadXML(file_name, tvItems, out IsXmlValid);
                        if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
                        else { pnlValidationMessages.Visible = true; pnlValidationMessages.BringToFront(); }
                        if (result == -1) errMsg = "File doesnot exist!!!";
                        else if (result == -2) errMsg = "File is not a well-formed XML!!!";
                        else if (result == -3) errMsg = "XSD file is not valid!!!";
                        else if (result == -4) errMsg = "File is not a valid XML, as per the schema!!!";
                        lvi = new ListViewItem("");
                        lvValidationMessages.Items.Add(lvi);
                        lvi = new ListViewItem(errMsg);
                        lvValidationMessages.Items.Add(lvi);
                        lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
                        if (result < 0)
                        {
                            hideLoading();
                            MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        //Ajay: 10/01/2019
                        //xmlFile = ofdXMLFile.FileName;//Assign only after loading file...
                        if (!string.IsNullOrEmpty(ofdXMLFile.FileName) && File.Exists(ofdXMLFile.FileName)) //Ajay: 10/01/2019
                        {
                            xmlFile = ofdXMLFile.FileName;//Assign only after loading file...
                        }
                        //Ajay: 10/01/2019
                        else
                        {
                            if (!string.IsNullOrEmpty(file_name) && File.Exists(file_name))
                            {
                                xmlFile = file_name;
                            }
                        }

                        tvItems.SelectedNode = tvItems.Nodes[0];
                        //Namrata: 05/01/2018
                        #region Treeview Collapse Common Node
                        //Ajay: 29/11/2018
                        //TreeNode searchNode = tvItems.Nodes[0].Nodes[5];
                        TreeNode searchNode = tvItems.Nodes[0].Nodes["MasterConfiguration"];
                        if (searchNode != null)
                            searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
                        #endregion Treeview Collapse Common Node
                        tvItems.Nodes[0].EnsureVisible();
                        hideLoading();
                    }
                }
                MyMruList.AddFile(file_name); // Add the file to the MRU list.
            }
            catch (Exception ex)
            {
                MyMruList.RemoveFile(file_name);// Remove the file from the MRU list.
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ValidationCallBack(object sender, oppcValidationMessages e)
        {
            string strRoutineName = "frmOpenProPlus: ValidationCallBack";
            try
            {
                if (e.Severity == XmlSeverityType.Warning)
                {
                    ListViewItem lvi = new ListViewItem("Warning: Matching schema not found.  No validation occurred." + e.Message);
                    lvValidationMessages.Items.Add(lvi);
                }
                else
                {
                    ListViewItem lvi = new ListViewItem("Validation error Line No. " + e.LineNo + ": " + e.Message);
                    lvValidationMessages.Items.Add(lvi);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public OpenProPlus_Config getOpenProPlusHandle()
        {
            return opcHandle;
        }
        private void handleAbout()
        {
            string strRoutineName = "frmOpenProPlus: handleAbout";
            try
            {
                frmAbout fa = new frmAbout();
                fa.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: aboutToolStripMenuItem_Click";
            try
            {
                handleAbout();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void tsbAbout_Click(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: tsbAbout_Click";
            try
            {
                handleAbout();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void toolbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: toolbarToolStripMenuItem_Click";
            try
            {
                if (toolbarToolStripMenuItem.Checked) tsParser.Visible = true;
                else tsParser.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void tvItems_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: tvItems_NodeMouseClick";
            try
            {
                #region IEC61850Client
                if (Utils.IEC61850ClientMList.Count > 0 && e.Node.FullPath.Contains("IEC61850 Group") && (e.Node.Level == 3))
                {
                    if (e.Node.Index <= Utils.IEC61850ClientMList.Count - 1)
                    {

                        Utils.MasterNumForIEC61850Client = Utils.IEC61850ClientMList[e.Node.Index].MasterNum.ToString();
                    }
                }
                if (Utils.IEC61850ClientMList.Count > 0 && e.Node.FullPath.Contains("IEC61850 Group") && (e.Node.Level == 4))
                {
                    if (e.Node.Parent.Index <= Utils.IEC61850ClientMList.Count - 1)
                    {
                        Utils.MasterNumForIEC61850Client = Utils.IEC61850ClientMList[e.Node.Parent.Index].MasterNum.ToString();
                    }
                }
                else if (Utils.IEC61850ClientMList.Count > 0 && e.Node.FullPath.Contains("IEC61850 Group") && (e.Node.Level == 5))
                {
                    if (e.Node.Parent.Index <= Utils.IEC61850ClientMList.Count - 1)
                    {
                        Utils.MasterNumForIEC61850Client = Utils.IEC61850ClientMList[e.Node.Parent.Parent.Index].MasterNum.ToString();
                    }
                }
                if (Utils.IEC61850MasteriedListGetIEDNo.Count > 0 && e.Node.FullPath.Contains("IEC61850 Group") && (e.Node.Level == 4))
                {
                    if (e.Node.Parent.Index <= Utils.IEC61850MasteriedListGetIEDNo.Count - 1)
                    {
                        Utils.UnitIDForIEC61850Client = Utils.IEC61850MasteriedListGetIEDNo[e.Node.Parent.Index].UnitID.ToString();
                    }
                }
                else if (Utils.IEC61850MasteriedListGetIEDNo.Count > 0 && e.Node.FullPath.Contains("IEC61850 Group") && (e.Node.Level == 5))
                {
                    if (e.Node.Parent.Index <= Utils.IEC61850MasteriedListGetIEDNo.Count - 1)
                    {
                        Utils.UnitIDForIEC61850Client = Utils.IEC61850MasteriedListGetIEDNo[e.Node.Parent.Index].UnitID.ToString();
                    }
                    //Utils.UnitIDForIEC61850Client = Utils.IEC61850MasteriedListGetIEDNo[e.Node.Parent.Index].UnitID.ToString();
                    Utils.MasterNumForIEC61850Client = Utils.IEC61850ClientMList[e.Node.Parent.Parent.Index].MasterNum.ToString();
                }
                #endregion IEC61850Client


                //Namrata: 26/10/2019
                if (e.Node.Parent == null)
                {

                }
                else
                {
                    if (e.Node.Parent.Text == "IEC61850 Group")
                    {
                        Utils.strFrmOpenproplusTreeNode = e.Node.Text;
                        Utils.strFrmOpenproplusIEdname = "";
                    }
                }

                if (e.Node.Parent != null)
                {
                    if (e.Node.Parent.Parent != null)
                    {
                        if (e.Node.Parent.Parent.Text != null)
                        {
                            if (e.Node.Parent.Parent.Text == "IEC61850 Group")
                            {
                                if (e.Button == MouseButtons.Right)
                                {
                                    if (e.Node.Text.Contains("IED "))
                                    {
                                        Utils.SCLFileName = Utils.IEC61850MasteriedListGetIEDNo[e.Node.Index].SCLName.ToString();
                                        ContextMenuStrip cm = opcHandle.GetToolstripMenu();
                                        if (cm != null)
                                        {
                                            Point pt = new Point(e.X, e.Y);
                                            cm.Show(tvItems, pt);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void tvItems_NodeMouseClick1(object sender, TreeNodeMouseClickEventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: tvItems_NodeMouseClick";
            try
            {
                #region IEC61850Client
                if (Utils.IEC61850ClientMList.Count > 0 && e.Node.FullPath.Contains("IEC61850 Group") && (e.Node.Level == 3))
                {
                    if (e.Node.Index <= Utils.IEC61850ClientMList.Count - 1)
                    {

                        Utils.MasterNumForIEC61850Client = Utils.IEC61850ClientMList[e.Node.Index].MasterNum.ToString();
                    }
                }
                if (Utils.IEC61850ClientMList.Count > 0 && e.Node.FullPath.Contains("IEC61850 Group") && (e.Node.Level == 4))
                {
                    if (e.Node.Parent.Index <= Utils.IEC61850ClientMList.Count - 1)
                    {
                        Utils.MasterNumForIEC61850Client = Utils.IEC61850ClientMList[e.Node.Parent.Index].MasterNum.ToString();
                    }
                }
                else if (Utils.IEC61850ClientMList.Count > 0 && e.Node.FullPath.Contains("IEC61850 Group") && (e.Node.Level == 5))
                {
                    if (e.Node.Parent.Index <= Utils.IEC61850ClientMList.Count - 1)
                    {
                        Utils.MasterNumForIEC61850Client = Utils.IEC61850ClientMList[e.Node.Parent.Parent.Index].MasterNum.ToString();
                    }
                }
                if (Utils.IEC61850MasteriedListGetIEDNo.Count > 0 && e.Node.FullPath.Contains("IEC61850 Group") && (e.Node.Level == 4))
                {
                    if (e.Node.Parent.Index <= Utils.IEC61850MasteriedListGetIEDNo.Count - 1)
                    {
                        Utils.UnitIDForIEC61850Client = Utils.IEC61850MasteriedListGetIEDNo[e.Node.Parent.Index].UnitID.ToString();
                    }
                }
                else if (Utils.IEC61850MasteriedListGetIEDNo.Count > 0 && e.Node.FullPath.Contains("IEC61850 Group") && (e.Node.Level == 5))
                {
                    Utils.UnitIDForIEC61850Client = Utils.IEC61850MasteriedListGetIEDNo[e.Node.Parent.Index].UnitID.ToString();
                }
                #endregion IEC61850Client


                //Namrata: 26/10/2019
                if (e.Node.Parent == null)
                {

                }



                //if (e.Node.Parent.Parent == null)
                //{

                //}
                else if (e.Node.Parent != null)
                {
                    if (e.Node.Parent.Text == "IEC61850 Group")
                    {
                        Utils.strFrmOpenproplusTreeNode = e.Node.Text;
                        Utils.strFrmOpenproplusIEdname = "";
                    }
                }
                //Namrata: 15/7/2017 
                if (e.Button == MouseButtons.Right)
                {
                    if (e.Node.Text == "Master Configuration")
                    {
                        ContextMenuStrip cm = opcHandle.getContextMenu();
                        if (cm != null)
                        {
                            Point pt = new Point(e.X, e.Y);
                            cm.Show(tvItems, pt);
                        }
                    }
                }
                //if (e.Node.Parent.Parent == null)
                //{

                //}
                else if (e.Node.Parent.Parent.Text != null)
                {
                    if (e.Node.Parent.Parent.Text == "IEC61850 Group")
                    {
                        if (e.Button == MouseButtons.Right)
                        {
                            if (e.Node.Text.Contains("IED "))
                            {
                                Utils.SCLFileName = Utils.IEC61850MasteriedListGetIEDNo[e.Node.Index].SCLName.ToString();
                                ContextMenuStrip cm = opcHandle.GetToolstripMenu();
                                if (cm != null)
                                {
                                    Point pt = new Point(e.X, e.Y);
                                    cm.Show(tvItems, pt);
                                }
                            }
                        }
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void tvItems_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: tvItems_AfterSelect";
            try
            {
                try
                {
                    try
                    {

                    }
                    catch (System.NullReferenceException)
                    {

                    }
                    this.scParser.Panel2.Controls.Clear();//Remove all previous controls...
                                                          //Namrata:10/06/2019
                    if (e.Node.Parent != null)
                    {
                        if (e.Node.Parent.Text == "SMS Group")
                        {
                            string tblName = e.Node.Text.ToString();
                            if (tblName != null)
                            {
                                string[] tokens = tblName.Split('_');
                                Utils.SMSSlaveNo = tokens[1];  //tokens[1];
                            }
                        }
                    }
                    //Namrata:10/06/2019
                    if (e.Node.Parent != null)
                    {
                        if (e.Node.Parent.Text == "GraphicalDisplay Group")
                        {
                            string tblName = e.Node.Text.ToString();
                            string CurrentSlave = e.Node.Text.ToString();
                            if (tblName != null)
                            {
                                string[] tokens = tblName.Split('_');
                                GDSlave.GDSlaveNo = tokens[1];  //tokens[1];
                            }
                            if (CurrentSlave != null)
                            {
                                string[] tokens = tblName.Split(' ');
                                GDSlave.CurSlave = tokens[1];  //tokens[1];
                            }
                        }
                    }
                    Control ucrp = opcHandle.getView(Utils.getKeyPathArray(e.Node));

                    #region LPFILE
                    //Ajay: 10/09/2018
                    if (e.Node.Text == "LPFILE")
                    {
                        if (ucrp is ucLPFilelist)
                        {
                            ucslpfile = (ucLPFilelist)ucrp;
                            ucslpfile.ucLPFilelist_Load(null, null);
                            if (e.Node.Parent.Parent.Parent.Text == "IEC61850 Group")
                            {
                                Utils.strFrmOpenproplusTreeNode = e.Node.Parent.Parent.Text;
                                //Namrata: 04/04/2018
                                List<string> tblNameList = Utils.dsIED.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                                string tblName = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
                                if (tblName != null)
                                {
                                    string[] tokens = tblName.Split('_');
                                    Utils.Iec61850IEDname = tokens[3];
                                }
                                aicNodeforiec61850Client = new AIConfiguration(MasterTypes.IEC61850Client, Convert.ToInt32(Utils.MasterNumForIEC61850Client), Convert.ToInt32(Utils.UnitIDForIEC61850Client));
                            }
                        }
                    }
                    #endregion LPFILE

                    #region AI
                    //Namrata: 26/10/2017
                    if (e.Node.Text == "AI")
                    {
                        if (ucrp is ucAIlist)
                        {
                            ucsai = (ucAIlist)ucrp;
                            ucsai.ucAIlist_Load(null, null);
                            if (e.Node.Parent.Parent.Parent.Text == "IEC61850 Group")
                            {
                                Utils.strFrmOpenproplusTreeNode = e.Node.Parent.Parent.Text;
                                //Namrata: 04/04/2018
                                List<string> tblNameList = Utils.dsIED.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                                string tblName = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
                                if (tblName != null)
                                {
                                    string[] tokens = tblName.Split('_');
                                    Utils.Iec61850IEDname = tokens[3];
                                }

                                aicNodeforiec61850Client = new AIConfiguration(MasterTypes.IEC61850Client, Convert.ToInt32(Utils.MasterNumForIEC61850Client), Convert.ToInt32(Utils.UnitIDForIEC61850Client));
                            }
                        }
                    }
                    #endregion AI

                    #region AO
                    //Namrata: 26/10/2017
                    if (e.Node.Text == "AO")
                    {
                        if (ucrp is ucAOList)
                        {
                            ucsao = (ucAOList)ucrp;
                            //ucsao.ucAIlist_Load(null, null);
                            if (e.Node.Parent.Parent.Parent.Text == "IEC61850 Group")
                            {
                                Utils.strFrmOpenproplusTreeNode = e.Node.Parent.Parent.Text;
                                //Namrata: 04/04/2018
                                List<string> tblNameList = Utils.dsIED.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                                string tblName = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
                                if (tblName != null)
                                {
                                    string[] tokens = tblName.Split('_');
                                    Utils.Iec61850IEDname = tokens[3];
                                }
                                aocNodeforiec61850Client = new AOConfiguration(MasterTypes.IEC61850Client, Convert.ToInt32(Utils.MasterNumForIEC61850Client), Convert.ToInt32(Utils.UnitIDForIEC61850Client));
                            }
                        }
                    }
                    #endregion AO

                    #region DI
                    //Namrata: 26/10/2017
                    if (e.Node.Text == "DI")
                    {
                        if (ucrp is ucDIlist)
                        {
                            ucsdi = (ucDIlist)ucrp;
                            ucsdi.ucDIlist_Load(null, null);
                            if (e.Node.Parent.Parent.Parent.Text == "IEC61850 Group")
                            {
                                Utils.strFrmOpenproplusTreeNode = e.Node.Parent.Parent.Text;
                                //Namrata: 04/04/2018
                                List<string> tblNameList = Utils.dsIED.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                                string tblName = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
                                if (tblName != null)
                                {
                                    string[] tokens = tblName.Split('_');
                                    Utils.Iec61850IEDname = tokens[3];
                                }
                                dicNodeforiec61850Client = new DIConfiguration(MasterTypes.IEC61850Client, Convert.ToInt32(Utils.MasterNumForIEC61850Client), Convert.ToInt32(Utils.UnitIDForIEC61850Client));
                            }
                            else if(e.Node.Parent.Parent.Parent.Text=="ADR Group")
                            {
                                //dicNodeforiec61850Client = new DIConfiguration(MasterTypes.ADR, Convert.ToInt32(Utils.MasterNumForIEC61850Client), Convert.ToInt32(Utils.UnitIDForIEC61850Client));
                            }
                        }
                    }
                    #endregion DI

                    #region DO
                    //Namrata: 26/10/2017
                    if (e.Node.Text == "DO")
                    {
                        if (ucrp is ucDOlist)
                        {
                            ucsdo = (ucDOlist)ucrp;
                            if (e.Node.Parent.Parent.Parent.Text == "IEC61850 Group")
                            {
                                Utils.strFrmOpenproplusTreeNode = e.Node.Parent.Parent.Text;
                                //Namrata: 04/04/2018
                                List<string> tblNameList = Utils.dsIED.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                                string tblName = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
                                if (tblName != null)
                                {
                                    string[] tokens = tblName.Split('_');
                                    Utils.Iec61850IEDname = tokens[3];
                                }
                                docNodeforiec61850Client = new DOConfiguration(MasterTypes.IEC61850Client, Convert.ToInt32(Utils.MasterNumForIEC61850Client), Convert.ToInt32(Utils.UnitIDForIEC61850Client));
                            }
                        }
                    }
                    #endregion DO

                    #region EN
                    //Namrata: 26/10/2017
                    if (e.Node.Text == "EN")
                    {
                        if (ucrp is ucENlist)
                        {
                            ucsen = (ucENlist)ucrp;
                            if (e.Node.Parent.Parent.Parent.Text == "IEC61850 Group")
                            {
                                Utils.strFrmOpenproplusTreeNode = e.Node.Parent.Parent.Text;
                                //Namrata: 04/04/2018
                                List<string> tblNameList = Utils.dsIED.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                                string tblName = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
                                if (tblName != null)
                                {
                                    string[] tokens = tblName.Split('_');
                                    Utils.Iec61850IEDname = tokens[3];
                                }
                                encNodeforiec61850Client = new ENConfiguration(MasterTypes.IEC61850Client, Convert.ToInt32(Utils.MasterNumForIEC61850Client), Convert.ToInt32(Utils.UnitIDForIEC61850Client));
                            }
                        }
                    }

                    #endregion EN

                    #region ucGroupIEC104
                    if (ucrp is ucGroupIEC104)
                    {
                        iec104EventHandled = false; //Ajay: 10/01/2018
                        if (!iec104EventHandled)
                        {
                            ucs104 = (ucGroupIEC104)ucrp;
                            Console.WriteLine("***### boom handle event for 104 slave INI export");
                            ((ucGroupIEC104)ucrp).btnExportINIClick += new System.EventHandler(this.btnExportINI_Click);
                            iec104EventHandled = true;
                        }
                    }
                    #endregion ucGroupIEC104

                    //Namrata: 22/11/2019
                    #region ucGroupIEC104

                    //private bool SMSEventHandled = false;
                    //ucGroupSMSSlave ucSMS = null;
                    if (ucrp is ucGroupSMSSlave)
                    {
                        SMSEventHandled = false; //Ajay: 10/01/2018
                        if (!iec104EventHandled)
                        {
                            ucSMS = (ucGroupSMSSlave)ucrp;
                            Console.WriteLine("***### boom handle event for 104 slave INI export");
                            // ((ucGroupIEC104)ucrp).btnExportINIClick += new System.EventHandler(this.btnExportINI_Click);
                            SMSEventHandled = true;
                        }
                    }
                    #endregion ucGroupIEC104




                    #region ucGroupIEC101Slave
                    //Namrata:3/7/2017
                    else if (ucrp is ucGroupIEC101Slave)
                    {
                        //iec101EventHandled = false; //Ajay: 10/01/2018
                        if (!iec101EventHandled)
                        {
                            ucs101 = (ucGroupIEC101Slave)ucrp;
                            Console.WriteLine("***### boom handle event for 101 slave INI export");
                            ((ucGroupIEC101Slave)ucrp).btnexportIEC101INIClick += new System.EventHandler(this.btnExportINIIEC101_Click);
                            iec101EventHandled = true;
                        }
                    }
                    #endregion ucGroupIEC101Slave

                    #region RCB
                    //Namrata: 26/09/2017
                    if (e.Node.Text == "RCB")
                    {
                        if (ucrp is ucRCBList)
                        {
                            //Namrata: 21/03/2018
                            ucRCB = (ucRCBList)ucrp;
                            ucRCB.ucRCBList_Load(null, null);
                            if (e.Node.Parent.Parent.Parent.Text == "IEC61850 Group")
                            {
                                Utils.strFrmOpenproplusTreeNode = e.Node.Parent.Parent.Text;
                                RCBNodeforiec61850Client = new RCBConfiguration(MasterTypes.IEC61850Client, Convert.ToInt32(Utils.MasterNumForIEC61850Client), Convert.ToInt32(Utils.UnitIDForIEC61850Client));
                                RCBNodeforiec61850Client.FillRCBList();
                            }
                        }
                    }
                    #endregion RCB
                    this.scParser.Panel2.Controls.Add(ucrp);
                }
                catch (System.NullReferenceException)
                {
                    Console.WriteLine("*** NullReferenceException handled...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnExportINIIEC101_Click(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: btnExportINIIEC101_Click";
            try
            {
                // Namrata: Commented on 28/01/2019//if (ucs101.INIExported) return; //Ajay: 10/01/2019
                if (ucs101 == null) return;
                if (ucs101.lvIEC101Slave.CheckedItems.Count != 1)
                {
                    MessageBox.Show("Select single slave for INI export!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (xmlFile == null || xmlFile == "")
                {
                    MessageBox.Show("Save file before INI export!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ListViewItem lvi1 = ucs101.lvIEC101Slave.CheckedItems[0];
                saveINIFile(lvi1.Text, "IEC101Slave_" + lvi1.Text); //saveINIFile(lvi1.Text, "IEC101_" + lvi1.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void btnExportINI_Click(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: btnExportINI_Click";
            try
            {
                if (ucs104.INIExported) return; //Ajay: 10/01/2019
                if (ucs104 == null) return;
                if (ucs104.lvIEC104Slave.CheckedItems.Count != 1)
                {
                    MessageBox.Show("Select Single Slave For INI Export!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (xmlFile == null || xmlFile == "")
                {
                    MessageBox.Show("Save File Before INI Export!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ListViewItem lvi = ucs104.lvIEC104Slave.CheckedItems[0];
                saveINIFile(lvi.Text, "IEC104_" + lvi.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void saveINIFile(string slaveNum, string slaveID)
        {
            string strRoutineName = "frmOpenProPlus: saveINIFile";
            try
            {
                Console.WriteLine("*** INI file for: {0} {1}", slaveNum, slaveID);
                sfdXMLFile.Filter = "INI Files|*.ini";
                if (sfdXMLFile.ShowDialog() == DialogResult.OK)
                {
                    Console.WriteLine("*** Saving to file: {0}", sfdXMLFile.FileName);

                    writeINIFile(sfdXMLFile.FileName, slaveNum, slaveID);
                    //Ajay: 21/11/2017 Show message box with file path  "\"" + sfdXMLFile.FileName + "\"
                    MessageBox.Show("\"" + sfdXMLFile.FileName + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void writeINIFile(string fName, string slaveNum, string slaveID)
        {
            string strRoutineName = "frmOpenProPlus: writeINIFile";
            try
            {
                File.WriteAllText(fName, opcHandle.getINIData(xmlFile, slaveNum, slaveID));
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void saveXMLFile()
        {
            string strRoutineName = "frmOpenProPlus: saveXMLFile";
            try
            {
                if (xmlFile == null || xmlFile == "")
                {
                    sfdXMLFile.Filter = "XML Files|*.xml";
                    sfdXMLFile.Title = "Save XML File";
                    if (sfdXMLFile.ShowDialog() == DialogResult.OK)
                    {

                        xmlFile = sfdXMLFile.FileName;
                        {
                            Utils.XMLNameWOExt = Path.GetFileNameWithoutExtension(xmlFile);
                            string dir = Path.GetDirectoryName(xmlFile);
                            Utils.DirNameSave = dir + @"\" + Utils.XMLNameWOExt;
                            //Namrata: 21/03/2018
                            ICDFilesData.DirectoryName = Path.GetDirectoryName(xmlFile); //Get DirectoryName
                            ICDFilesData.XmlName = Path.GetFileName(xmlFile); //Get XML Name
                            ICDFilesData.XmlPath = xmlFile;//XML with full path
                            ICDFilesData.XmlWithoutExt = Path.GetFileNameWithoutExtension(ICDFilesData.XmlPath);

                            #region SLDFiles
                            //if (GDSlave.GDisplaySlave)
                            //{
                            //    //Namrata: 16/08/2019
                            GDSlave.DirName = Path.GetDirectoryName(xmlFile); //Get DirectoryName
                            GDSlave.SLDName = Path.GetFileName(xmlFile); //Get XML Name
                            GDSlave.SLDPath = xmlFile;//XML with full path
                            GDSlave.SLDWithoutExt = Path.GetFileNameWithoutExtension(GDSlave.SLDPath);
                            //}
                            #endregion SLDFiles
                            writeXMLFile(xmlFile);
                            UpdateXMLFile(xmlFile);
                            ICDFilesData.IEC61850Xmlname = Path.GetDirectoryName(xmlFile) + @"\" + Utils.XMLNameWOExt + ".zip";
                            if ((Utils.IEC61850ClientMList.Count > 0) || (Utils.IEC61850ServerSList.Count > 0))//Namrata:08/04/2019
                            {
                                MessageBox.Show("\"" + ICDFilesData.IEC61850Xmlname + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            else
                            {
                                xmlFile= Path.GetDirectoryName(xmlFile) + @"\" + Utils.XMLNameWOExt + ".zip";
                                MessageBox.Show("\"" + xmlFile + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                else
                {
                    //Utils.XMLNameWOExt = Path.GetFileNameWithoutExtension(xmlFile);
                    //string dir = Path.GetDirectoryName(xmlFile);
                    //Utils.DirNameSave = dir + @"\" + Utils.XMLNameWOExt;


                    Utils.XMLNameWOExt = Path.GetFileNameWithoutExtension(Utils.DirNameSave);
                    #region ICDFiles
                    //Namrata: 21/03/2018
                    ICDFilesData.DirectoryName = Path.GetDirectoryName(Utils.DirNameSave); //Get DirectoryName
                    ICDFilesData.XmlName = Path.GetFileName(Utils.DirNameSave); //Get XML Name
                    ICDFilesData.XmlPath = xmlFile;// Utils.DirNameSave;//XML with full path
                    ICDFilesData.XmlWithoutExt = Path.GetFileNameWithoutExtension(Utils.DirNameSave);
                    #endregion ICDFiles

                    GDSlave.DirName = Path.GetDirectoryName(Utils.DirNameSave); //Get DirectoryName
                    GDSlave.SLDName = Path.GetFileName(Utils.DirNameSave); //Get XML Name
                    GDSlave.SLDPath = Utils.DirNameSave;//XML with full path
                    GDSlave.SLDWithoutExt = Path.GetFileNameWithoutExtension(GDSlave.SLDPath);

                    //xmlFile = System.IO.Path.GetTempPath() + @"\" + "openproplus_config.xml";

                    //Utils.XMLNameWOExt = Path.GetFileNameWithoutExtension(xmlFile);
                    //#region ICDFiles
                    ////Namrata: 21/03/2018
                    //ICDFilesData.DirectoryName = Path.GetDirectoryName(xmlFile); //Get DirectoryName
                    //ICDFilesData.XmlName = Path.GetFileName(xmlFile); //Get XML Name
                    //ICDFilesData.XmlPath = xmlFile;//XML with full path
                    //ICDFilesData.XmlWithoutExt = Path.GetFileNameWithoutExtension(ICDFilesData.XmlPath);
                    //#endregion ICDFiles

                    //GDSlave.DirName = Path.GetDirectoryName(xmlFile); //Get DirectoryName
                    //GDSlave.SLDName = Path.GetFileName(xmlFile); //Get XML Name
                    //GDSlave.SLDPath = xmlFile;//XML with full path
                    //GDSlave.SLDWithoutExt = Path.GetFileNameWithoutExtension(GDSlave.SLDPath);

                    //Namarta: 21/11/2019
                    xmlFile = ICDFilesData.DirectoryName + @"\" + ICDFilesData.XmlName + ".xml";
                    ICDFilesData.XmlPath = Utils.DirNameSave + ".xml";// Utils.DirNameSave;//XML with full path
                    writeXMLFile(xmlFile);
                    xmlFile = Utils.DirNameSave + ".xml";
                    UpdateXMLFile(xmlFile);
                    ICDFilesData.IEC61850Xmlname = Path.GetDirectoryName(xmlFile) + @"\" + Utils.XMLNameWOExt+".zip";
                    if ((Utils.IEC61850ClientMList.Count > 0) || (Utils.IEC61850ServerSList.Count > 0))//Namrata:08/04/2019
                    {
                        MessageBox.Show("\"" + ICDFilesData.IEC61850Xmlname + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("\"" + Utils.DirNameSave + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void saveXMLFile1()
        {
            string strRoutineName = "frmOpenProPlus: saveXMLFile";
            try
            {
                if (xmlFile == null || xmlFile == "")
                {
                    sfdXMLFile.Filter = "XML Files|*.xml";
                    sfdXMLFile.Title = "Save XML File";
                    if (sfdXMLFile.ShowDialog() == DialogResult.OK)
                    {

                        xmlFile = sfdXMLFile.FileName;
                        {
                            Utils.XMLNameWOExt = Path.GetFileNameWithoutExtension(xmlFile);
                            string dir = Path.GetDirectoryName(xmlFile);
                            Utils.DirNameSave = dir + @"\" + Utils.XMLNameWOExt;
                            //Namrata: 21/03/2018
                            ICDFilesData.DirectoryName = Path.GetDirectoryName(xmlFile); //Get DirectoryName
                            ICDFilesData.XmlName = Path.GetFileName(xmlFile); //Get XML Name
                            ICDFilesData.XmlPath = xmlFile;//XML with full path
                            ICDFilesData.XmlWithoutExt = Path.GetFileNameWithoutExtension(ICDFilesData.XmlPath);

                            #region SLDFiles
                            //if (GDSlave.GDisplaySlave)
                            //{
                            //    //Namrata: 16/08/2019
                            GDSlave.DirName = Path.GetDirectoryName(xmlFile); //Get DirectoryName
                            GDSlave.SLDName = Path.GetFileName(xmlFile); //Get XML Name
                            GDSlave.SLDPath = xmlFile;//XML with full path
                            GDSlave.SLDWithoutExt = Path.GetFileNameWithoutExtension(GDSlave.SLDPath);
                            //}
                            #endregion SLDFiles
                            writeXMLFile(xmlFile);
                            UpdateXMLFile(xmlFile);
                            if ((Utils.IEC61850ClientMList.Count > 0) || (Utils.IEC61850ServerSList.Count > 0))//Namrata:08/04/2019
                            {
                                MessageBox.Show("\"" + ICDFilesData.IEC61850Xmlname + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            else
                            {
                                MessageBox.Show("\"" + xmlFile + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                else
                {
                    Utils.XMLNameWOExt = Path.GetFileNameWithoutExtension(Utils.DirNameSave);
                    #region ICDFiles
                    //Namrata: 21/03/2018
                    ICDFilesData.DirectoryName = Path.GetDirectoryName(Utils.DirNameSave); //Get DirectoryName
                    ICDFilesData.XmlName = Path.GetFileName(Utils.DirNameSave); //Get XML Name
                    ICDFilesData.XmlPath = xmlFile;// Utils.DirNameSave;//XML with full path
                    ICDFilesData.XmlWithoutExt = Path.GetFileNameWithoutExtension(Utils.DirNameSave);
                    #endregion ICDFiles

                    GDSlave.DirName = Path.GetDirectoryName(Utils.DirNameSave); //Get DirectoryName
                    GDSlave.SLDName = Path.GetFileName(Utils.DirNameSave); //Get XML Name
                    GDSlave.SLDPath = Utils.DirNameSave;//XML with full path
                    GDSlave.SLDWithoutExt = Path.GetFileNameWithoutExtension(GDSlave.SLDPath);

                    //xmlFile = System.IO.Path.GetTempPath() + @"\" + "openproplus_config.xml";

                    //Utils.XMLNameWOExt = Path.GetFileNameWithoutExtension(xmlFile);
                    //#region ICDFiles
                    ////Namrata: 21/03/2018
                    //ICDFilesData.DirectoryName = Path.GetDirectoryName(xmlFile); //Get DirectoryName
                    //ICDFilesData.XmlName = Path.GetFileName(xmlFile); //Get XML Name
                    //ICDFilesData.XmlPath = xmlFile;//XML with full path
                    //ICDFilesData.XmlWithoutExt = Path.GetFileNameWithoutExtension(ICDFilesData.XmlPath);
                    //#endregion ICDFiles

                    //GDSlave.DirName = Path.GetDirectoryName(xmlFile); //Get DirectoryName
                    //GDSlave.SLDName = Path.GetFileName(xmlFile); //Get XML Name
                    //GDSlave.SLDPath = xmlFile;//XML with full path
                    //GDSlave.SLDWithoutExt = Path.GetFileNameWithoutExtension(GDSlave.SLDPath);

                    //Namarta: 21/11/2019
                    xmlFile = ICDFilesData.DirectoryName + @"\" + ICDFilesData.XmlName + ".xml";
                    ICDFilesData.XmlPath = Utils.DirNameSave + ".xml";// Utils.DirNameSave;//XML with full path
                    writeXMLFile(xmlFile);
                    xmlFile = Utils.DirNameSave + ".xml";
                    UpdateXMLFile(xmlFile);
                    ICDFilesData.IEC61850Xmlname = Path.GetDirectoryName(xmlFile) + @"\" + Utils.XMLNameWOExt;
                    if ((Utils.IEC61850ClientMList.Count > 0) || (Utils.IEC61850ServerSList.Count > 0))//Namrata:08/04/2019
                    {
                        MessageBox.Show("\"" + ICDFilesData.IEC61850Xmlname + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("\"" + Utils.DirNameSave + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        static void CopyDirectory(DirectoryInfo source, DirectoryInfo destination)
        {
            string strRoutineName = "frmOpenProPlus: CopyDirectory";
            try
            {
                if (!destination.Exists)
                {
                    destination.Create();
                }
                FileInfo[] files = source.GetFiles();
                foreach (FileInfo file in files)
                {
                    file.CopyTo(Path.Combine(destination.FullName, file.Name));
                }
                // Process subdirectories.
                DirectoryInfo[] dirs = source.GetDirectories();
                foreach (DirectoryInfo dir in dirs)
                {
                    string destinationDir = Path.Combine(destination.FullName, dir.Name);  //Get destination directory.
                    CopyDirectory(dir, new DirectoryInfo(destinationDir)); //Call CopyDirectory() recursively.
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        static IEnumerable<string> GetFileSearchPaths(string fileName)
        {
            yield return fileName;
            yield return Path.Combine(System.IO.Directory.GetParent(Path.GetDirectoryName(fileName)).FullName, Path.GetFileName(fileName));
        }
        static bool FileExists(string fileName)
        {
            return GetFileSearchPaths(fileName).Any(File.Exists);
        }
        string XMLFolder = "";
        private void saveAsXMLFile()
        {
            string strRoutineName = "frmOpenProPlus: saveAsXMLFile";
            try
            {
                string strFilePath = "";
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    if (!string.IsNullOrEmpty(ProtocolGateway.ProtocolGatewayConfigurationFile))
                    {
                        //sfdXMLFile.InitialDirectory = Path.GetDirectoryName(ProtocolGateway.ProtocolGatewayConfigurationFile);
                        //if (!string.IsNullOrEmpty(xmlFile)) { sfdXMLFile.FileName = Path.GetFileName(xmlFile); }
                        frmInput frminput = new frmInput();
                        if (frminput.ShowDialog() == DialogResult.OK)
                        {
                            if (!string.IsNullOrEmpty(frminput.txtbxInput.Text.Trim()))
                            {
                                strFilePath = Path.Combine(Path.GetDirectoryName(ProtocolGateway.ProtocolGatewayConfigurationFile), frminput.txtbxInput.Text.Trim());
                                frminput.Close();
                            }
                        }
                        else { return; }
                    }
                }
                else if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Full)
                {
                    sfdXMLFile.Filter = "XML Files|*.xml";
                    sfdXMLFile.Title = "Save As XML";
                    if (!string.IsNullOrEmpty(xmlFile)) { sfdXMLFile.FileName = xmlFile; Utils.XMLOldFileName = xmlFile; }
                    if (sfdXMLFile.ShowDialog() == DialogResult.OK)
                    {
                        //Namrata: 22/10/2019
                        //string XMLFileName = "OpenProPlus_Config.xml";
                        //string FilePath = Path.GetDirectoryName(sfdXMLFile.FileName);
                        //strFilePath = Path.Combine(FilePath, XMLFileName);
                        //XMLFolder = Path.GetFileNameWithoutExtension(sfdXMLFile.FileName);
                        strFilePath = sfdXMLFile.FileName.Trim();
                    }
                }
                if (!string.IsNullOrEmpty(strFilePath))
                {
                    #region XMLName Without Extension

                    Utils.XMLNameWOExt = Path.GetFileNameWithoutExtension(strFilePath);
                    #endregion XMLName Without Extension

                    #region IEC61850
                    //Namrata: 27/04/2018
                    ICDFilesData.DirectoryName = Path.GetDirectoryName(strFilePath); //Get DirectoryName
                    ICDFilesData.XmlName = Path.GetFileName(strFilePath); // Get XML Name
                    ICDFilesData.XmlPath = strFilePath; // XML with full path
                    ICDFilesData.XmlWithoutExt = Path.GetFileNameWithoutExtension(ICDFilesData.XmlPath);
                    #endregion IEC61850

                    #region SLDFiles
                    //if (GDSlave.GDisplaySlave)
                    //{
                    //Namrata: 16/08/2019
                    GDSlave.DirName = Path.GetDirectoryName(strFilePath); //Get DirectoryName
                    GDSlave.SLDName = Path.GetFileName(strFilePath); //Get XML Name
                    GDSlave.SLDPath = strFilePath;//XML with full path
                    GDSlave.SLDWithoutExt = Path.GetFileNameWithoutExtension(GDSlave.SLDPath);
                    //}
                    #endregion SLDFiles
                    Utils.DirNameSave = ICDFilesData.DirectoryName + @"\" + Utils.XMLNameWOExt;
                    writeXMLFile(strFilePath);
                    UpdateXMLFile(strFilePath);
                    Utils.XMLUpdatedFileName = strFilePath;
                    ICDFilesData.IEC61850Xmlname = Utils.DirNameSave + ".zip";
                    //Namrata: 29/01/2018
                    MyMruList = new MruList(Application.ProductName, this.recentFilesToolStripMenuItem, 10, strFilePath, this.myOwnRecentFilesGotCleared_handler);
                    MyMruList.FileSelected += MyMruList_FileSelected;
                    if (Utils.IEC61850ClientMList.Count > 0)
                    {
                        MessageBox.Show("\"" + ICDFilesData.IEC61850Xmlname + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                    else
                    {
                        MessageBox.Show("\"" + strFilePath + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        //MessageBox.Show("\"" + Path.GetFileName(strFilePath) + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            System.IO.Directory.CreateDirectory(GDSlave.SubDirName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(GDSlave.SubDirName, fi.Name), true);
            }

            DirectoryInfo[] dirs = source.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                var diTarget = new DirectoryInfo(GDSlave.SubDirName);
                DirectoryInfo CopyDiagramFolder = diTarget;
                string destinationDir = Path.Combine(CopyDiagramFolder.FullName, dir.Name);
                CopyDirectory(dir, new DirectoryInfo(destinationDir));
            }
        }
        public void Compressfile()
        {
            string fileName = "openproplus_config.xml";
            string sourcePath = @"C:\Users\swatin\Desktop\890";
            DirectoryInfo di = new DirectoryInfo(sourcePath);
            foreach (FileInfo fi in di.GetFiles())
            {
                //for specific file 
                if (fi.ToString() == fileName)
                {
                    Compress(fi);
                }
            }
        }

        public static void Compress(FileInfo fi)
        {
            // Get the stream of the source file.
            using (FileStream inFile = fi.OpenRead())
            {
                // Prevent compressing hidden and 
                // already compressed files.
                if ((File.GetAttributes(fi.FullName)
                    & FileAttributes.Hidden)
                    != FileAttributes.Hidden & fi.Extension != ".gz")
                {
                    // Create the compressed file.
                    using (FileStream outFile = File.Create(fi.FullName + ".gz"))
                    {
                        using (GZipStream Compress = new GZipStream(outFile, CompressionMode.Compress))
                        {
                            // Copy the source file into 
                            // the compression stream.
                            inFile.CopyTo(Compress);

                            Console.WriteLine("Compressed {0} from {1} to {2} bytes.",
                                fi.Name, fi.Length.ToString(), outFile.Length.ToString());
                        }
                    }
                }
            }
        }


        private void SaveXMLFiles()
        {
            string strRoutineName = "frmOpenProPlus: SaveXMLFiles";
            try
            {
                XmlDocument Xmldoc = new XmlDocument();
                Xmldoc.Load(Utils.CopyXML);

                string XMLFileName = "openproplus_config.xml";//updatedXMLFile
                                                              //string FolderPath = Path.GetDirectoryName(Utils.CopyXML);
                string FolderPath = Path.GetDirectoryName(Utils.DirNameSave);
                string XMLName = Path.GetFileNameWithoutExtension(Utils.DirNameSave);

                string DestDir = Path.Combine(FolderPath, XMLName);

                if (!System.IO.Directory.Exists(DestDir))
                {
                    System.IO.Directory.CreateDirectory(DestDir);
                    if (System.IO.Directory.Exists(DestDir))
                    {

                        File.Move(Utils.CopyXML, DestDir + @"\" + "openproplus_config.xml");
                    }
                    else { }
                }
                else
                {
                    string[] fileEntries = System.IO.Directory.GetFiles(DestDir, "*.xml");
                    string XmlfileName = string.Empty;
                    string XmlLocation = string.Empty;

                    foreach (string s in fileEntries)
                    {
                        XmlfileName = System.IO.Path.GetFileName(s);
                        XmlLocation = System.IO.Path.Combine(DestDir, XmlfileName);
                        if (File.Exists(XmlLocation))
                        {
                            System.IO.File.Delete(XmlLocation);
                        }
                        System.IO.File.Move(Utils.CopyXML, DestDir + @"\" + "openproplus_config.xml");
                    }
                }
                string ZipFileName = DestDir + ".zip";
                if (File.Exists(ZipFileName))
                {
                    File.Delete(ZipFileName);
                    ZipFile.CreateFromDirectory(DestDir, ZipFileName, CompressionLevel.NoCompression, false);

                }
                else
                {
                    ZipFile.CreateFromDirectory(DestDir, ZipFileName, CompressionLevel.NoCompression, false);
                }
                #region Create TarFile
                //string DirName = Path.Combine(FolderPath, XMLName);
                //string TarFile = DirName + ".tar.gz";
                //Utils.XMLNamExt1 = FolderPath;
                ////string TarFile = DirName + ".tar.gz";
                ////Namrata:18/10/2019
                //if (File.Exists(TarFile))
                //{
                //    File.Delete(TarFile);
                //    CreateTarGZ1(TarFile, "openproplus_config.xml");
                //    //CreateTarGZ(TarFile, DestDir);
                //}
                //else
                //{
                //    CreateTarGZ1(TarFile, "openproplus_config.xml");
                //}
                #endregion Create TarFile

                #region Remove Folder 
                if (System.IO.Directory.Exists(DestDir))
                {
                    System.IO.Directory.Delete(DestDir, true);
                }
                #endregion Remove Folder 
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CreateTarGZ1(string tgzFilename, string fileName)
        {
            using (var outStream = File.Create(tgzFilename))
            using (var gzoStream = new GZipOutputStream(outStream))
            using (var tarArchive = TarArchive.CreateOutputTarArchive(gzoStream))
            {
                // Add files
                string[] filenames = System.IO.Directory.GetFiles(@"C:\Users\swatin\Desktop\123");
                foreach (string filename in filenames)
                {
                    TarEntry tarEntry = TarEntry.CreateEntryFromFile(filename);
                    tarArchive.WriteEntry(tarEntry, true);
                }
                //tarArchive.RootPath = Path.GetDirectoryName(@"C:\Users\swatin\Desktop\123");

                //var tarEntry = TarEntry.CreateEntryFromFile("openproplus_config.xml");
                //tarEntry.Name = Path.GetFileName(fileName);

                //tarArchive.WriteEntry(tarEntry, true);
            }
        }




        private void CreateTar(string outputTarFilename, string sourceDirectory)
        {
            using (FileStream fs = new FileStream(outputTarFilename, FileMode.Create, FileAccess.Write, FileShare.None))
            using (Stream gzipStream = new GZipOutputStream(fs))
            using (TarArchive tarArchive = TarArchive.CreateOutputTarArchive(gzipStream))
            {
                AddDirectoryFilesToTar(tarArchive, sourceDirectory, true);
            }
        }
        private void AddDirectoryFilesToTar(TarArchive tarArchive, string sourceDirectory, bool recurse)
        {
            //// Recursively add sub-folders
            if (recurse)
            {
                string[] directories = System.IO.Directory.GetDirectories(sourceDirectory);
                foreach (string directory in directories)
                {
                    TarEntry tarEntry = TarEntry.CreateTarEntry(directory);
                    //tarArchive.WriteEntry(tarEntry, false);
                    //tarArchive.WriteEntry
                    AddDirectoryFilesToTar(tarArchive, directory, recurse);
                }
            }
            // Add files
            string[] filenames = System.IO.Directory.GetFiles(sourceDirectory);
            foreach (string filename in filenames)
            {
                TarEntry tarEntry = TarEntry.CreateEntryFromFile(filename);
                tarArchive.WriteEntry(tarEntry, true);
            }
        }
        private static void addDirectory(TarArchive tarArchive, string directory)
        {
            TarEntry tarEntry = TarEntry.CreateEntryFromFile(directory);
            tarArchive.WriteEntry(tarEntry, false);

            //string[] filenames = System.IO.Directory.GetFiles(directory);
            //foreach (string filename in filenames)
            //{
            //    addFile(tarArchive, filename);
            //}

            string[] directories = System.IO.Directory.GetDirectories(directory);
            foreach (string dir in directories)
                addDirectory(tarArchive, dir);
        }

        //Namarta:11/04/2019
        private void writeXMLFile(string wFile)
        {
            string strRoutineName = "frmOpenProPlus: writeXMLFile";
            try
            {
                File.WriteAllText(wFile, opcHandle.getXMLData());
                #region IEC61850Client And IEC61850Server
                ICDFilesData.XMLData = wFile;//Namrata:09/04/2019
                GDSlave.SLDData = wFile;
                if ((ICDFilesData.IEC61850Client == true) || (ICDFilesData.IEC61850Server == true) || (GDSlave.GDisplaySlave == true))
                {
                    SaveXMLSLDClientServer();
                }
                else
                {
                    Utils.CopyXML = wFile;
                    SaveXMLFiles();
                }
                #endregion IEC61850Client And IEC61850Server
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void SaveSLDFiles()
        {
            string strRoutineName = "frmOpenProPlus: SaveSLDFiles";
            try
            {
                XmlDocument Xmldoc = new XmlDocument();
                Utils.GDisplayXMLFile = GDSlave.SLDData;
                Xmldoc.Load(Utils.GDisplayXMLFile);


                #region Declarations
                string XMLFileName = Path.GetFileNameWithoutExtension(Utils.GDisplayXMLFile);//updatedXMLFile
                string FullXmlPath = GDSlave.DirName + @"\" + GDSlave.SLDWithoutExt;//XMLPath
                string FileNameWithExtension = Path.GetFileName(Utils.GDisplayXMLFile);

                //For Moving XMLFile
                string MoveXMLFiles = GDSlave.DirName + @"\" + GDSlave.SLDWithoutExt;
                GDSlave.XLSDirFile = System.IO.Path.GetTempPath() + "protocol" + @"\" + "GUI";
                GDSlave.CopyXMLFile = System.IO.Path.GetTempPath() + "protocol";
                #endregion Declarations


                #region Save Updated Txt Files In AppData
                if (GDSlave.DsExcelData.Tables.Count > 0)
                {
                    foreach (DataTable Dt in GDSlave.DsExcelData.Tables)
                    {
                        string TableName = Dt.TableName;
                        string[] tokens = TableName.Split('_');
                        GDSlave.XLSFileName = TableName.Substring(24);
                        string FolderName = tokens[0] + "_" + tokens[1];//GDSlave.XLSFileName = tokens[2];
                        string TxtFilePath = System.IO.Path.GetTempPath() + "protocol" + @"\" + "GUI" + @"\" + FolderName + @"\" + GDSlave.XLSFileName;
                        if (System.IO.File.Exists(TxtFilePath))
                        {
                            File.Delete(TxtFilePath);
                            ExportDataSetToCsvFile(Dt, TxtFilePath);
                        }
                        else { ExportDataSetToCsvFile(Dt, TxtFilePath); }
                    }
                }
                #endregion Save Updated Txt Files In AppData

                #region Update GDSlave.CreateDirGDSlave Directory Name
                if (GDSlave.GDisplaySlave)
                {
                    if (GDSlave.SLDWithoutExt != "")
                    { GDSlave.CreateDirGDSlave = GDSlave.DirName + @"\" + GDSlave.SLDWithoutExt + @"\" + "protocol" + @"\" + "GUI"; }
                    else
                    { GDSlave.CreateDirGDSlave = GDSlave.DirName + @"\" + "protocol" + @"\" + "GUI"; }
                }
                #endregion Update GDSlave.CreateDirGDSlave Directory Name

                if (Utils.GraphicalDisplaySlaveList.Count > 0)
                {
                    #region Get XMLFile From AppData
                    string fileName = string.Empty;
                    string destFile = string.Empty;
                    bool IsXMLExist;
                    string[] XMLfileNames = System.IO.Directory.GetFiles(GDSlave.CopyXMLFile, "*.xml", SearchOption.TopDirectoryOnly);

                    if (XMLfileNames.Length != 0)
                    {
                        IsXMLExist = true;
                        foreach (string fileName1 in XMLfileNames)
                        {
                            File.Delete(XMLfileNames[0]);
                        }
                    }
                    else { IsXMLExist = false; }
                    #endregion Get XMLFile From AppData

                    if (Utils.XMLFilePath != "")
                    {
                        FolderNamewithoutexstension = Path.GetDirectoryName(Utils.XMLFilePath) + @"\" + Utils.ExtractedFileName;
                    }

                    #region Check If Directory Exist in AppData 
                    //GDSlave.XLSDirFile = C:\Users\namrata\AppData\Local\Temp\protocol\SLD
                    if (!System.IO.Directory.Exists(GDSlave.XLSDirFile))
                    {
                        System.IO.Directory.CreateDirectory(GDSlave.XLSDirFile);
                        if (System.IO.Directory.Exists(GDSlave.XLSDirFile))
                        {
                            #region Move XML File From User Selected Location To AppData
                            string[] files = System.IO.Directory.GetFiles(GDSlave.XLSDirFile);//Get Files From Directory
                            foreach (string s in files)
                            {
                                fileName = System.IO.Path.GetFileName(s);
                                destFile = System.IO.Path.Combine(GDSlave.XLSDirFile, fileName);
                                File.Move(GDSlave.SLDPath, GDSlave.CopyXMLFile + @"\" + "openproplus_config.xml");
                                //File.Move(GDSlave.SLDPath, GDSlave.CopyXMLFile + @"\" + GDSlave.SLDWithoutExt);
                            }
                            #endregion Move XML File From User Selected Location To AppData
                        }
                        else { }
                    }
                    else
                    {
                        #region Move XML File From User Selected Location To AppData
                        if (!FileExists(GDSlave.CopyXMLFile + @"\" + GDSlave.SLDName))
                        {
                            //Namrata: 22/10/2019
                            //Fixed XMLFile Name
                            File.Move(GDSlave.SLDPath, GDSlave.CopyXMLFile + @"\" + "openproplus_config.xml");
                            //File.Move(GDSlave.SLDPath, GDSlave.CopyXMLFile + @"\" + GDSlave.SLDName);
                        }
                        #endregion Move XML File From User Selected Location To AppData
                    }
                    #endregion Check If Directory Exist in AppData

                    #region Create Directory For At User Location
                    if (!System.IO.Directory.Exists(FullXmlPath))
                    {
                        System.IO.Directory.CreateDirectory(FullXmlPath);
                        string[] fileEntries1 = System.IO.Directory.GetFiles(GDSlave.CopyXMLFile, "*.xml");
                        string XmlfileName = string.Empty;
                        string XmlLocation = string.Empty;

                        foreach (string s in fileEntries1)
                        {
                            XmlfileName = System.IO.Path.GetFileName(s);
                            XmlLocation = System.IO.Path.Combine(MoveXMLFiles, XmlfileName);
                            if (File.Exists(XmlLocation))
                            {
                                System.IO.File.Delete(XmlLocation);
                            }
                            System.IO.File.Copy(s, XmlLocation, true);
                        }

                        if (!System.IO.Directory.Exists(GDSlave.CreateDirGDSlave))
                        {
                            System.IO.Directory.CreateDirectory(GDSlave.CreateDirGDSlave);
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(GDSlave.XLSDirFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = GDSlave.CreateDirGDSlave + "\\" + tokens[8];

                                var diSource = new DirectoryInfo(GDSlave.XLSDirFile + "\\" + tokens[8]);
                                var diTarget = new DirectoryInfo(GDSlave.CreateDirGDSlave);
                                CopyAll(diSource, diTarget);

                            }
                        }
                    }
                    #endregion Create Directory For At User Location
                    else
                    {
                        #region Move XMLFile From AppData To UserLocation
                        string[] fileEntries11 = System.IO.Directory.GetFiles(GDSlave.CopyXMLFile, "*.xml");
                        string XmlfileName1 = string.Empty;
                        string XmlLocation1 = string.Empty;
                        foreach (string s in fileEntries11)
                        {
                            XmlfileName1 = System.IO.Path.GetFileName(s);
                            XmlLocation1 = System.IO.Path.Combine(MoveXMLFiles, XmlfileName1);
                            if (File.Exists(XmlLocation1))
                            {
                                System.IO.File.Delete(XmlLocation1);
                            }
                            System.IO.File.Copy(s, XmlLocation1, true);
                        }
                        #endregion Move XMLFile From AppData To UserLocation

                        if (!System.IO.Directory.Exists(GDSlave.CreateDirGDSlave))
                        {
                            System.IO.Directory.CreateDirectory(GDSlave.CreateDirGDSlave);

                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(GDSlave.XLSDirFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = GDSlave.CreateDirGDSlave + "\\" + tokens[8];
                                var diSource = new DirectoryInfo(GDSlave.XLSDirFile + "\\" + tokens[8]);
                                var diTarget = new DirectoryInfo(GDSlave.CreateDirGDSlave);
                                CopyAll(diSource, diTarget);
                            }
                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                        }
                        else
                        {
                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(GDSlave.XLSDirFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = GDSlave.CreateDirGDSlave + "\\" + tokens[8];
                                if (System.IO.Directory.Exists(GDSlave.SubDirName))
                                {
                                    System.IO.Directory.Delete(GDSlave.SubDirName, true);
                                    var diSource = new DirectoryInfo(GDSlave.XLSDirFile + "\\" + tokens[8]);
                                    var diTarget = new DirectoryInfo(GDSlave.CreateDirGDSlave);
                                    CopyAll(diSource, diTarget);
                                }
                                else
                                {
                                    var diSource1 = new DirectoryInfo(GDSlave.XLSDirFile + "\\" + tokens[8]);
                                    var diTarget1 = new DirectoryInfo(GDSlave.CreateDirGDSlave);
                                    CopyAll(diSource1, diTarget1);
                                }
                            }
                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                        }
                    }

                    string ZipFileName = FullXmlPath + ".zip";
                    if (File.Exists(ZipFileName))
                    {
                        File.Delete(ZipFileName);
                        ZipFile.CreateFromDirectory(FullXmlPath, ZipFileName, CompressionLevel.NoCompression, false);

                    }
                    else
                    {
                        ZipFile.CreateFromDirectory(FullXmlPath, ZipFileName, CompressionLevel.NoCompression, false);
                    }

                    if (System.IO.Directory.Exists(FullXmlPath))
                    {
                        System.IO.Directory.Delete(FullXmlPath, true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SaveXMLSLDClientServerOld()
        {
            string strRoutineName = "frmOpenProPlus: SaveXMLSLDClientServer";
            try
            {
                XmlDocument Xmldoc = new XmlDocument();
                Utils.XMLFilecopy = ICDFilesData.XMLData;
                Xmldoc.Load(Utils.XMLFilecopy);

                #region Declarations
                string updatedXMLFile = Path.GetFileNameWithoutExtension(Utils.XMLFilecopy);//ABC      
                string XMLPath = ICDFilesData.DirectoryName + @"\" + ICDFilesData.XmlWithoutExt;
                string GetFilewithextension = Path.GetFileName(Utils.XMLFilecopy);
                string MoveXMLFiles = ICDFilesData.DirectoryName + @"\" + ICDFilesData.XmlWithoutExt; //For Moving XMLFile
                string FullXmlPath = ICDFilesData.DirectoryName + @"\" + ICDFilesData.XmlWithoutExt;

                //IEC61850Client Directory
                ICDFilesData.ICDDirMFile = System.IO.Path.GetTempPath() + "protocol" + @"\" + "IEC61850Client";

                //IEC61850Server Directory
                ICDFilesData.ICDDirSFile = System.IO.Path.GetTempPath() + "protocol" + @"\" + "IEC61850Server";

                //SLD Directory
                GDSlave.XLSDirFile = System.IO.Path.GetTempPath() + "protocol" + @"\" + "GUI";

                //Namrata:08/04/2019 
                ICDFilesData.CopyXMLFile = System.IO.Path.GetTempPath() + "protocol";

                #endregion Declarations

                #region Create IEC61850Client,IEC61850Server,SLD Directory Path
                if ((ICDFilesData.IEC61850Client == true) || (ICDFilesData.IEC61850Server == true) || (GDSlave.GDisplaySlave == true))
                {
                    if (ICDFilesData.XmlWithoutExt != "")
                    {
                        ICDFilesData.CreateDirIEC61850C = ICDFilesData.DirectoryName + @"\" + ICDFilesData.XmlWithoutExt + @"\" + "protocol";// + @"\" + "IEC61850Client";
                        ICDFilesData.CreateDirIEC61850S = ICDFilesData.DirectoryName + @"\" + ICDFilesData.XmlWithoutExt + @"\" + "protocol" + @"\" + "IEC61850Server";
                    }
                    else
                    {
                        ICDFilesData.CreateDirIEC61850C = ICDFilesData.DirectoryName + @"\" + "protocol";// + @"\" + "IEC61850Client";
                        ICDFilesData.CreateDirIEC61850S = ICDFilesData.DirectoryName + @"\" + "protocol" + @"\" + "IEC61850Server";
                    }
                }
                if (GDSlave.GDisplaySlave)
                {
                    if (GDSlave.SLDWithoutExt != "")
                    { GDSlave.CreateDirGDSlave = GDSlave.DirName + @"\" + GDSlave.SLDWithoutExt + @"\" + "protocol" + @"\" + "GUI"; }
                    else
                    { GDSlave.CreateDirGDSlave = GDSlave.DirName + @"\" + "protocol" + @"\" + "GUI"; }
                }
                #endregion Create IEC61850Client,IEC61850Server,SLD Directory Path

                #region Get XMLFile From AppData
                string fileName = string.Empty; string destFile = string.Empty; bool IsXMLExist;

                #endregion Get XMLFile From AppData
                if (Utils.XMLFilePath != "")
                {
                    FolderNamewithoutexstension = Path.GetDirectoryName(Utils.XMLFilePath) + @"\" + Utils.ExtractedFileName;
                }
                #region IEC61850Server Slave
                if (Utils.IEC61850ServerSList.Count > 0)
                {
                    #region Check If Directory Exist in AppData 
                    //GDSlave.XLSDirFile = C:\Users\namrata\AppData\Local\Temp\protocol\SLD
                    if (!System.IO.Directory.Exists(ICDFilesData.ICDDirSFile))
                    {
                        System.IO.Directory.CreateDirectory(ICDFilesData.ICDDirSFile);
                        if (System.IO.Directory.Exists(ICDFilesData.ICDDirSFile))
                        {
                            #region Move XML File From User Selected Location To AppData
                            string[] files = System.IO.Directory.GetFiles(ICDFilesData.ICDDirSFile);//Get Files From Directory
                            foreach (string s in files)
                            {
                                fileName = System.IO.Path.GetFileName(s);
                                destFile = System.IO.Path.Combine(ICDFilesData.ICDDirSFile, fileName);
                                File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                            }
                            #endregion Move XML File From User Selected Location To AppData
                        }
                        else { }
                    }
                    else
                    {
                        #region Move XML File From User Selected Location To AppData
                        if (!FileExists(ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml"))
                        {
                            //Namrata: 22/10/2019
                            //Fixed XMLFile Name
                            File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                        }
                        #endregion Move XML File From User Selected Location To AppData
                    }
                    #endregion Check If Directory Exist in AppData

                    #region Create Directory For At User Location
                    if (!System.IO.Directory.Exists(FullXmlPath))
                    {
                        System.IO.Directory.CreateDirectory(FullXmlPath);
                        string[] fileEntries1 = System.IO.Directory.GetFiles(XMLPath, "*.xml");
                        string XmlfileName = string.Empty;
                        string XmlLocation = string.Empty;

                        foreach (string s in fileEntries1)
                        {
                            XmlfileName = System.IO.Path.GetFileName(s);
                            XmlLocation = System.IO.Path.Combine(MoveXMLFiles, XmlfileName);
                            if (File.Exists(XmlLocation))
                            {
                                System.IO.File.Delete(XmlLocation);
                            }
                            System.IO.File.Copy(s, XmlLocation, true);
                        }
                        if (!System.IO.Directory.Exists(ICDFilesData.CreateDirIEC61850S))
                        {
                            System.IO.Directory.CreateDirectory(ICDFilesData.CreateDirIEC61850S);
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(ICDFilesData.ICDDirSFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = ICDFilesData.CreateDirIEC61850S + "\\" + tokens[8];
                                var diSource = new DirectoryInfo(ICDFilesData.ICDDirSFile + "\\" + tokens[8]);
                                var diTarget = new DirectoryInfo(ICDFilesData.CreateDirIEC61850S);
                                CopyAll(diSource, diTarget);
                            }
                        }
                    }
                    #endregion Create Directory For At User Location
                    else
                    {
                        #region Move XMLFile From AppData To UserLocation
                        string[] fileEntries11 = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml");
                        string XmlfileName1 = string.Empty;
                        string XmlLocation1 = string.Empty;
                        foreach (string s in fileEntries11)
                        {
                            XmlfileName1 = System.IO.Path.GetFileName(s);
                            XmlLocation1 = System.IO.Path.Combine(MoveXMLFiles, XmlfileName1);
                            if (File.Exists(XmlLocation1))
                            {
                                System.IO.File.Delete(XmlLocation1);
                            }
                            System.IO.File.Copy(s, XmlLocation1, true);
                        }
                        #endregion Move XMLFile From AppData To UserLocation

                        if (!System.IO.Directory.Exists(ICDFilesData.CreateDirIEC61850S))
                        {
                            System.IO.Directory.CreateDirectory(ICDFilesData.CreateDirIEC61850S);

                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(ICDFilesData.ICDDirSFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = ICDFilesData.CreateDirIEC61850S + "\\" + tokens[8];
                                var diSource = new DirectoryInfo(ICDFilesData.ICDDirSFile + "\\" + tokens[8]);
                                var diTarget = new DirectoryInfo(ICDFilesData.CreateDirIEC61850S);
                                CopyAll(diSource, diTarget);
                            }
                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                        }
                        else
                        {
                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(ICDFilesData.ICDDirSFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = ICDFilesData.CreateDirIEC61850S + "\\" + tokens[8];
                                if (System.IO.Directory.Exists(GDSlave.SubDirName))
                                {
                                    System.IO.Directory.Delete(GDSlave.SubDirName, true);
                                    var diSource = new DirectoryInfo(ICDFilesData.ICDDirSFile + "\\" + tokens[8]);
                                    var diTarget = new DirectoryInfo(ICDFilesData.CreateDirIEC61850S);
                                    CopyAll(diSource, diTarget);
                                }
                                else
                                {
                                    var diSource1 = new DirectoryInfo(ICDFilesData.ICDDirSFile + "\\" + tokens[8]);
                                    var diTarget1 = new DirectoryInfo(ICDFilesData.CreateDirIEC61850S);
                                    CopyAll(diSource1, diTarget1);
                                }
                            }
                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                        }
                    }

                }
                #endregion IEC61850Server Slave

                #region 
                if (Utils.IEC61850ClientMList.Count > 0)
                {
                    #region Check If Directory Exist in AppData 
                    if (!System.IO.Directory.Exists(ICDFilesData.ICDDirMFile))
                    {
                        System.IO.Directory.CreateDirectory(ICDFilesData.ICDDirMFile);
                        if (System.IO.Directory.Exists(ICDFilesData.ICDDirMFile))
                        {
                            #region Move XML File From User Selected Location To AppData
                            string[] files = System.IO.Directory.GetFiles(ICDFilesData.ICDDirMFile);//Get Files From Directory
                            foreach (string s in files)
                            {
                                fileName = System.IO.Path.GetFileName(s);
                                destFile = System.IO.Path.Combine(ICDFilesData.ICDDirMFile, fileName);
                                File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                            }
                            #endregion Move XML File From User Selected Location To AppData
                        }
                        else { }
                    }
                    else
                    {
                        //Namrata:20/11/2019
                        string[] XMLfileNames = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml", SearchOption.TopDirectoryOnly); //Utils.XMLFilecopy
                        //string[] XMLfileNames = System.IO.Directory.GetFiles(Utils.XMLFilecopy, "*.xml", SearchOption.TopDirectoryOnly); //Utils.XMLFilecopy
                        if (XMLfileNames.Length != 0)
                        {
                            IsXMLExist = true;
                            foreach (string fileName1 in XMLfileNames)
                            {
                                File.Delete(XMLfileNames[0]);
                            }
                        }
                        else { IsXMLExist = false; }
                        #region Move XML File From User Selected Location To AppData
                        if (!FileExists(ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml"))
                        {
                            //Namrata: 22/10/2019
                            //Fixed XMLFile Name
                            File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                        }
                        else
                        {
                            File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                        }
                        #endregion Move XML File From User Selected Location To AppData
                    }
                    #endregion Check If Directory Exist in AppData

                    #region Create Directory For At User Location
                    if (!System.IO.Directory.Exists(FullXmlPath))
                    {
                        System.IO.Directory.CreateDirectory(FullXmlPath);
                        string[] fileEntries1 = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml");
                        string XmlfileName = string.Empty;
                        string XmlLocation = string.Empty;

                        foreach (string s in fileEntries1)
                        {
                            XmlfileName = System.IO.Path.GetFileName(s);
                            XmlLocation = System.IO.Path.Combine(MoveXMLFiles, XmlfileName);
                            if (File.Exists(XmlLocation))
                            {
                                System.IO.File.Delete(XmlLocation);
                            }
                            System.IO.File.Copy(s, XmlLocation, true);
                        }
                        if (!System.IO.Directory.Exists(ICDFilesData.CreateDirIEC61850C))
                        {
                            string temppath = System.IO.Path.GetTempPath() + "protocol";
                            System.IO.Directory.CreateDirectory(ICDFilesData.CreateDirIEC61850C);
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(temppath);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = ICDFilesData.CreateDirIEC61850C + "\\" + tokens[7];
                                var diSource = new DirectoryInfo(temppath + "\\" + tokens[7]);
                                var diTarget = new DirectoryInfo(ICDFilesData.CreateDirIEC61850C);
                                CopyAll(diSource, diTarget);
                            }
                        }
                    }
                    #endregion Create Directory For At User Location
                    else
                    {
                        #region Move XMLFile From AppData To UserLocation
                        string[] fileEntries11 = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml");
                        string XmlfileName1 = string.Empty;
                        string XmlLocation1 = string.Empty;
                        foreach (string s in fileEntries11)
                        {
                            XmlfileName1 = System.IO.Path.GetFileName(s);
                            XmlLocation1 = System.IO.Path.Combine(MoveXMLFiles, XmlfileName1);
                            if (File.Exists(XmlLocation1))
                            {
                                System.IO.File.Delete(XmlLocation1);
                            }
                            System.IO.File.Copy(s, XmlLocation1, true);
                        }
                        #endregion Move XMLFile From AppData To UserLocation

                        if (!System.IO.Directory.Exists(ICDFilesData.CreateDirIEC61850C))
                        {
                            System.IO.Directory.CreateDirectory(ICDFilesData.CreateDirIEC61850C);

                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(ICDFilesData.ICDDirMFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = ICDFilesData.CreateDirIEC61850C + "\\" + tokens[8];
                                var diSource = new DirectoryInfo(ICDFilesData.ICDDirMFile + "\\" + tokens[8]);
                                var diTarget = new DirectoryInfo(ICDFilesData.CreateDirIEC61850C);
                                CopyAll(diSource, diTarget);
                            }
                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                        }
                        else
                        {
                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(ICDFilesData.ICDDirMFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = ICDFilesData.CreateDirIEC61850C + "\\" + tokens[8];
                                if (System.IO.Directory.Exists(GDSlave.SubDirName))
                                {
                                    System.IO.Directory.Delete(GDSlave.SubDirName, true);
                                    var diSource = new DirectoryInfo(ICDFilesData.ICDDirMFile + "\\" + tokens[8]);
                                    var diTarget = new DirectoryInfo(ICDFilesData.CreateDirIEC61850C);
                                    CopyAll(diSource, diTarget);
                                }
                                else
                                {
                                    var diSource1 = new DirectoryInfo(ICDFilesData.ICDDirMFile + "\\" + tokens[8]);
                                    var diTarget1 = new DirectoryInfo(ICDFilesData.CreateDirIEC61850C);
                                    CopyAll(diSource1, diTarget1);
                                }
                            }
                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                        }
                    }
                }
                #endregion 

                #region GraphicalDisplay Slave
                if (Utils.GraphicalDisplaySlaveList.Count > 0)
                {
                    #region Save Updated Txt Files In AppData
                    if (GDSlave.DsExcelData.Tables.Count > 0)
                    {
                        foreach (DataTable Dt in GDSlave.DsExcelData.Tables)
                        {
                            string TableName = Dt.TableName;
                            string[] tokens = TableName.Split('_');
                            GDSlave.XLSFileName = TableName.Substring(24);
                            string FolderName = tokens[0] + "_" + tokens[1];
                            string TxtFilePath = System.IO.Path.GetTempPath() + "protocol" + @"\" + "GUI" + @"\" + FolderName + @"\" + GDSlave.XLSFileName;
                            if (System.IO.File.Exists(TxtFilePath))
                            {
                                File.Delete(TxtFilePath);
                                ExportDataSetToCsvFile(Dt, TxtFilePath);
                            }
                            else
                            {
                                ExportDataSetToCsvFile(Dt, TxtFilePath);
                            }
                        }
                    }
                    #endregion Save Updated Txt Files In AppData

                    #region Check If Directory Exist in AppData 
                    //GDSlave.XLSDirFile = C:\Users\namrata\AppData\Local\Temp\protocol\SLD
                    if (!System.IO.Directory.Exists(GDSlave.XLSDirFile))
                    {
                        System.IO.Directory.CreateDirectory(GDSlave.XLSDirFile);
                        if (System.IO.Directory.Exists(GDSlave.XLSDirFile))
                        {
                            #region Move XML File From User Selected Location To AppData
                            string[] files = System.IO.Directory.GetFiles(GDSlave.XLSDirFile);//Get Files From Directory
                            foreach (string s in files)
                            {
                                fileName = System.IO.Path.GetFileName(s);
                                destFile = System.IO.Path.Combine(GDSlave.XLSDirFile, fileName);
                                File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                            }
                            #endregion Move XML File From User Selected Location To AppData
                        }
                        else { }
                    }
                    else
                    {
                        //Namrata:20/11/2019
                        string[] XMLfileNames = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml", SearchOption.TopDirectoryOnly); //Utils.XMLFilecopy
                        //string[] XMLfileNames = System.IO.Directory.GetFiles(Utils.XMLFilecopy, "*.xml", SearchOption.TopDirectoryOnly); //Utils.XMLFilecopy
                        if (XMLfileNames.Length != 0)
                        {
                            IsXMLExist = true;
                            foreach (string fileName1 in XMLfileNames)
                            {
                                File.Delete(XMLfileNames[0]);
                            }
                        }
                        else { IsXMLExist = false; }
                        #region Move XML File From User Selected Location To AppData
                        if (!FileExists(ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml"))
                        {
                            //Namrata: 22/10/2019
                            //Fixed XMLFile Name
                            File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                        }
                        else
                        {
                            File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                        }
                        #endregion Move XML File From User Selected Location To AppData
                    }
                    #endregion Check If Directory Exist in AppData

                    #region Create Directory For At User Location
                    if (!System.IO.Directory.Exists(FullXmlPath))
                    {
                        System.IO.Directory.CreateDirectory(FullXmlPath);
                        string[] fileEntries1 = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml");
                        string XmlfileName = string.Empty;
                        string XmlLocation = string.Empty;

                        foreach (string s in fileEntries1)
                        {
                            XmlfileName = System.IO.Path.GetFileName(s);
                            XmlLocation = System.IO.Path.Combine(MoveXMLFiles, XmlfileName);
                            if (File.Exists(XmlLocation))
                            {
                                System.IO.File.Delete(XmlLocation);
                            }
                            System.IO.File.Copy(s, XmlLocation, true);
                        }
                        if (!System.IO.Directory.Exists(GDSlave.CreateDirGDSlave))
                        {
                            System.IO.Directory.CreateDirectory(GDSlave.CreateDirGDSlave);
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(GDSlave.XLSDirFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = GDSlave.CreateDirGDSlave + "\\" + tokens[8];
                                var diSource = new DirectoryInfo(GDSlave.XLSDirFile + "\\" + tokens[8]);
                                var diTarget = new DirectoryInfo(GDSlave.CreateDirGDSlave);
                                CopyAll(diSource, diTarget);
                            }
                        }
                    }
                    #endregion Create Directory For At User Location
                    else
                    {
                        #region Move XMLFile From AppData To UserLocation
                        string[] fileEntries11 = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml");
                        string XmlfileName1 = string.Empty;
                        string XmlLocation1 = string.Empty;
                        foreach (string s in fileEntries11)
                        {
                            XmlfileName1 = System.IO.Path.GetFileName(s);
                            XmlLocation1 = System.IO.Path.Combine(MoveXMLFiles, XmlfileName1);
                            if (File.Exists(XmlLocation1))
                            {
                                System.IO.File.Delete(XmlLocation1);
                            }
                            System.IO.File.Copy(s, XmlLocation1, true);
                        }
                        #endregion Move XMLFile From AppData To UserLocation

                        if (!System.IO.Directory.Exists(GDSlave.CreateDirGDSlave))
                        {
                            System.IO.Directory.CreateDirectory(GDSlave.CreateDirGDSlave);

                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(GDSlave.XLSDirFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = GDSlave.CreateDirGDSlave + "\\" + tokens[8];
                                var diSource = new DirectoryInfo(GDSlave.XLSDirFile + "\\" + tokens[8]);
                                var diTarget = new DirectoryInfo(GDSlave.CreateDirGDSlave);
                                CopyAll(diSource, diTarget);
                            }
                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                        }
                        else
                        {
                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(GDSlave.XLSDirFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = GDSlave.CreateDirGDSlave + "\\" + tokens[8];
                                if (System.IO.Directory.Exists(GDSlave.SubDirName))
                                {
                                    System.IO.Directory.Delete(GDSlave.SubDirName, true);
                                    var diSource = new DirectoryInfo(GDSlave.XLSDirFile + "\\" + tokens[8]);
                                    var diTarget = new DirectoryInfo(GDSlave.CreateDirGDSlave);
                                    CopyAll(diSource, diTarget);
                                }
                                else
                                {
                                    var diSource1 = new DirectoryInfo(GDSlave.XLSDirFile + "\\" + tokens[8]);
                                    var diTarget1 = new DirectoryInfo(GDSlave.CreateDirGDSlave);
                                    CopyAll(diSource1, diTarget1);
                                }
                            }
                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                        }
                    }

                }
                #endregion GraphicalDisplay Slave

                #region Create .Zip File
                string ZipFileName = FullXmlPath + ".zip";
                if (File.Exists(ZipFileName))
                {
                    File.Delete(ZipFileName);
                    ZipFile.CreateFromDirectory(FullXmlPath, ZipFileName, CompressionLevel.NoCompression, false);
                }
                else
                {
                    ZipFile.CreateFromDirectory(FullXmlPath, ZipFileName, CompressionLevel.NoCompression, false);
                }
                #endregion Create .Zip File

                #region Delete Folder From UserLocation
                if (System.IO.Directory.Exists(FullXmlPath))
                {
                    System.IO.Directory.Delete(FullXmlPath, true);
                }
                #endregion Delete Folder From UserLocation
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SaveXMLSLDClientServer11()
        {
            string strRoutineName = "frmOpenProPlus: SaveXMLSLDClientServer";
            try
            {
                XmlDocument Xmldoc = new XmlDocument();
                Utils.XMLFilecopy = ICDFilesData.XMLData;
                Xmldoc.Load(Utils.XMLFilecopy);

                #region Declarations
                string updatedXMLFile = Path.GetFileNameWithoutExtension(Utils.XMLFilecopy);//ABC      
                string XMLPath = ICDFilesData.DirectoryName + @"\" + ICDFilesData.XmlWithoutExt;
                string GetFilewithextension = Path.GetFileName(Utils.XMLFilecopy);
                string MoveXMLFiles = ICDFilesData.DirectoryName + @"\" + ICDFilesData.XmlWithoutExt; //For Moving XMLFile
                string FullXmlPath = ICDFilesData.DirectoryName + @"\" + ICDFilesData.XmlWithoutExt;
                //IEC61850Client Directory
                ICDFilesData.ICDDirMFile = System.IO.Path.GetTempPath() + "protocol" + @"\" + "IEC61850Client";
                //IEC61850Server Directory
                ICDFilesData.ICDDirSFile = System.IO.Path.GetTempPath() + "protocol" + @"\" + "IEC61850Server";
                //Namrata:08/04/2019 
                ICDFilesData.CopyXMLFile = System.IO.Path.GetTempPath() + "protocol";
                GDSlave.XLSDirFile = System.IO.Path.GetTempPath() + "protocol" + @"\" + "GUI";
                #endregion Declarations

                #region Create IEC61850Client,IEC61850Server,SLD Directory Path
                if ((ICDFilesData.IEC61850Client == true) || (ICDFilesData.IEC61850Server == true) || (GDSlave.GDisplaySlave == true))
                {
                    if (ICDFilesData.XmlWithoutExt != "")
                    {
                        ICDFilesData.CreateDirIEC61850C = ICDFilesData.DirectoryName + @"\" + ICDFilesData.XmlWithoutExt + @"\" + "protocol" + @"\" + "IEC61850Client";//Directory Path
                        ICDFilesData.CreateDirIEC61850S = ICDFilesData.DirectoryName + @"\" + ICDFilesData.XmlWithoutExt + @"\" + "protocol" + @"\" + "IEC61850Server";//Directory Path
                        //GDSlave.CreateDirGDSlave = GDSlave.DirName + @"\" + GDSlave.SLDWithoutExt + @"\" + "protocol" + @"\" + "GUI";
                    }
                    else
                    {
                        ICDFilesData.CreateDirIEC61850C = ICDFilesData.DirectoryName + @"\" + "protocol" + @"\" + "IEC61850Client";//Create DirectoryName
                        ICDFilesData.CreateDirIEC61850S = ICDFilesData.DirectoryName + @"\" + "protocol" + @"\" + "IEC61850Server";//Create DirectoryName

                        //GDSlave.CreateDirGDSlave = GDSlave.DirName + @"\" + "protocol" + @"\" + "GUI";
                    }
                }
                #endregion Create IEC61850Client,IEC61850Server,SLD Directory Path

                if (GDSlave.GDisplaySlave)
                {
                    if (GDSlave.SLDWithoutExt != "")
                    { GDSlave.CreateDirGDSlave = GDSlave.DirName + @"\" + GDSlave.SLDWithoutExt + @"\" + "protocol" + @"\" + "GUI"; }
                    else
                    { GDSlave.CreateDirGDSlave = GDSlave.DirName + @"\" + "protocol" + @"\" + "GUI"; }
                }

                #region Get XMLFile From AppData
                string fileName = string.Empty;
                string destFile = string.Empty;
                bool IsXMLExist;
                //string[] XMLfileNames = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml", SearchOption.TopDirectoryOnly); //Utils.XMLFilecopy
                ////string[] XMLfileNames = System.IO.Directory.GetFiles(Utils.XMLFilecopy, "*.xml", SearchOption.TopDirectoryOnly); //Utils.XMLFilecopy
                //if (XMLfileNames.Length != 0)
                //{
                //    IsXMLExist = true;
                //    foreach (string fileName1 in XMLfileNames)
                //    {
                //        File.Delete(XMLfileNames[0]);
                //    }
                //}
                //else { IsXMLExist = false; }
                #endregion Get XMLFile From AppData

                if (Utils.XMLFilePath != "")
                {
                    FolderNamewithoutexstension = Path.GetDirectoryName(Utils.XMLFilePath) + @"\" + Utils.ExtractedFileName;
                }

                #region IEC61850Server Slave
                if (Utils.IEC61850ServerSList.Count > 0)
                {
                    #region Check If Directory Exist in AppData 
                    //GDSlave.XLSDirFile = C:\Users\namrata\AppData\Local\Temp\protocol\SLD
                    if (!System.IO.Directory.Exists(ICDFilesData.ICDDirSFile))
                    {
                        System.IO.Directory.CreateDirectory(ICDFilesData.ICDDirSFile);
                        if (System.IO.Directory.Exists(ICDFilesData.ICDDirSFile))
                        {
                            #region Move XML File From User Selected Location To AppData
                            string[] files = System.IO.Directory.GetFiles(ICDFilesData.ICDDirSFile);//Get Files From Directory
                            foreach (string s in files)
                            {
                                fileName = System.IO.Path.GetFileName(s);
                                destFile = System.IO.Path.Combine(ICDFilesData.ICDDirSFile, fileName);
                                File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                            }
                            #endregion Move XML File From User Selected Location To AppData
                        }
                        else { }
                    }
                    else
                    {
                        #region Move XML File From User Selected Location To AppData
                        if (!FileExists(ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml"))
                        {
                            //Namrata: 22/10/2019
                            //Fixed XMLFile Name
                            File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                        }
                        #endregion Move XML File From User Selected Location To AppData
                    }
                    #endregion Check If Directory Exist in AppData

                    #region Create Directory For At User Location
                    if (!System.IO.Directory.Exists(FullXmlPath))
                    {
                        System.IO.Directory.CreateDirectory(FullXmlPath);
                        string[] fileEntries1 = System.IO.Directory.GetFiles(XMLPath, "*.xml");
                        string XmlfileName = string.Empty;
                        string XmlLocation = string.Empty;

                        foreach (string s in fileEntries1)
                        {
                            XmlfileName = System.IO.Path.GetFileName(s);
                            XmlLocation = System.IO.Path.Combine(MoveXMLFiles, XmlfileName);
                            if (File.Exists(XmlLocation))
                            {
                                System.IO.File.Delete(XmlLocation);
                            }
                            System.IO.File.Copy(s, XmlLocation, true);
                        }
                        if (!System.IO.Directory.Exists(ICDFilesData.CreateDirIEC61850S))
                        {
                            System.IO.Directory.CreateDirectory(ICDFilesData.CreateDirIEC61850S);
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(ICDFilesData.ICDDirSFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = ICDFilesData.CreateDirIEC61850S + "\\" + tokens[8];
                                var diSource = new DirectoryInfo(ICDFilesData.ICDDirSFile + "\\" + tokens[8]);
                                var diTarget = new DirectoryInfo(ICDFilesData.CreateDirIEC61850S);
                                CopyAll(diSource, diTarget);
                            }
                        }
                    }
                    #endregion Create Directory For At User Location
                    else
                    {
                        #region Move XMLFile From AppData To UserLocation
                        string[] fileEntries11 = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml");
                        string XmlfileName1 = string.Empty;
                        string XmlLocation1 = string.Empty;
                        foreach (string s in fileEntries11)
                        {
                            XmlfileName1 = System.IO.Path.GetFileName(s);
                            XmlLocation1 = System.IO.Path.Combine(MoveXMLFiles, XmlfileName1);
                            if (File.Exists(XmlLocation1))
                            {
                                System.IO.File.Delete(XmlLocation1);
                            }
                            System.IO.File.Copy(s, XmlLocation1, true);
                        }
                        #endregion Move XMLFile From AppData To UserLocation

                        if (!System.IO.Directory.Exists(ICDFilesData.CreateDirIEC61850S))
                        {
                            System.IO.Directory.CreateDirectory(ICDFilesData.CreateDirIEC61850S);

                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(ICDFilesData.ICDDirSFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = ICDFilesData.CreateDirIEC61850S + "\\" + tokens[8];
                                var diSource = new DirectoryInfo(ICDFilesData.ICDDirSFile + "\\" + tokens[8]);
                                var diTarget = new DirectoryInfo(ICDFilesData.CreateDirIEC61850S);
                                CopyAll(diSource, diTarget);
                            }
                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                        }
                        else
                        {
                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(ICDFilesData.ICDDirSFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = ICDFilesData.CreateDirIEC61850S + "\\" + tokens[8];
                                if (System.IO.Directory.Exists(GDSlave.SubDirName))
                                {
                                    System.IO.Directory.Delete(GDSlave.SubDirName, true);
                                    var diSource = new DirectoryInfo(ICDFilesData.ICDDirSFile + "\\" + tokens[8]);
                                    var diTarget = new DirectoryInfo(ICDFilesData.CreateDirIEC61850S);
                                    CopyAll(diSource, diTarget);
                                }
                                else
                                {
                                    var diSource1 = new DirectoryInfo(ICDFilesData.ICDDirSFile + "\\" + tokens[8]);
                                    var diTarget1 = new DirectoryInfo(ICDFilesData.CreateDirIEC61850S);
                                    CopyAll(diSource1, diTarget1);
                                }
                            }
                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                        }
                    }

                }
                #endregion IEC61850Server Slave

                #region IEC61850Client Master
                if (Utils.IEC61850ClientMList.Count > 0)
                {
                    #region Check If Directory Exist in AppData 
                    //GDSlave.XLSDirFile = C:\Users\namrata\AppData\Local\Temp\protocol\SLD
                    if (!System.IO.Directory.Exists(ICDFilesData.ICDDirMFile))
                    {
                        System.IO.Directory.CreateDirectory(ICDFilesData.ICDDirMFile);
                        if (System.IO.Directory.Exists(ICDFilesData.ICDDirMFile))
                        {
                            #region Move XML File From User Selected Location To AppData
                            string[] files = System.IO.Directory.GetFiles(ICDFilesData.ICDDirMFile);//Get Files From Directory
                            foreach (string s in files)
                            {
                                fileName = System.IO.Path.GetFileName(s);
                                destFile = System.IO.Path.Combine(ICDFilesData.ICDDirMFile, fileName);
                                File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                            }
                            #endregion Move XML File From User Selected Location To AppData
                        }
                        else { }
                    }
                    else
                    {
                        #region Move XML File From User Selected Location To AppData
                        if (!FileExists(ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml"))
                        {
                            //Namrata: 22/10/2019
                            //Fixed XMLFile Name
                            File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                        }
                        else
                        {
                                if (File.Exists(ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml"))
                                {
                                    System.IO.File.Delete(ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                                }
                                File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                                //System.IO.File.Copy(s, XmlLocation1, true);
                            

                            //File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                        }
                        #endregion Move XML File From User Selected Location To AppData
                    }
                    #endregion Check If Directory Exist in AppData

                    #region Create Directory For At User Location
                    if (!System.IO.Directory.Exists(FullXmlPath))
                    {
                        System.IO.Directory.CreateDirectory(FullXmlPath);
                        string[] fileEntries1 = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml");
                        string XmlfileName = string.Empty;
                        string XmlLocation = string.Empty;

                        foreach (string s in fileEntries1)
                        {
                            XmlfileName = System.IO.Path.GetFileName(s);
                            XmlLocation = System.IO.Path.Combine(MoveXMLFiles, XmlfileName);
                            if (File.Exists(XmlLocation))
                            {
                                System.IO.File.Delete(XmlLocation);
                            }
                            System.IO.File.Copy(s, XmlLocation, true);
                        }
                        if (!System.IO.Directory.Exists(ICDFilesData.CreateDirIEC61850C))
                        {
                            System.IO.Directory.CreateDirectory(ICDFilesData.CreateDirIEC61850C);
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(ICDFilesData.CopyXMLFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = ICDFilesData.CreateDirIEC61850C;// + "\\" + tokens[8];
                                var diSource = new DirectoryInfo(ICDFilesData.ICDDirMFile);// + "\\" + tokens[8]);
                                var diTarget = new DirectoryInfo(ICDFilesData.CreateDirIEC61850C);
                                CopyAll(diSource, diTarget);
                            }
                        }
                    }
                    #endregion Create Directory For At User Location
                    else
                    {
                        #region Move XMLFile From AppData To UserLocation
                        string[] fileEntries11 = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml");
                        string XmlfileName1 = string.Empty;
                        string XmlLocation1 = string.Empty;
                        foreach (string s in fileEntries11)
                        {
                            XmlfileName1 = System.IO.Path.GetFileName(s);
                            XmlLocation1 = System.IO.Path.Combine(MoveXMLFiles, XmlfileName1);
                            if (File.Exists(XmlLocation1))
                            {
                                System.IO.File.Delete(XmlLocation1);
                            }
                            System.IO.File.Copy(s, XmlLocation1, true);
                        }
                        #endregion Move XMLFile From AppData To UserLocation

                        if (!System.IO.Directory.Exists(ICDFilesData.CreateDirIEC61850C))
                        {
                            System.IO.Directory.CreateDirectory(ICDFilesData.CreateDirIEC61850C);

                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(ICDFilesData.ICDDirMFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = ICDFilesData.CreateDirIEC61850S + "\\" + tokens[8];
                                var diSource = new DirectoryInfo(ICDFilesData.ICDDirMFile + "\\" + tokens[8]);
                                var diTarget = new DirectoryInfo(ICDFilesData.CreateDirIEC61850C);
                                CopyAll(diSource, diTarget);
                            }
                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                        }
                        else
                        {
                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(ICDFilesData.ICDDirMFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = ICDFilesData.CreateDirIEC61850C + "\\" + tokens[8];
                                if (System.IO.Directory.Exists(GDSlave.SubDirName))
                                {
                                    System.IO.Directory.Delete(GDSlave.SubDirName, true);
                                    var diSource = new DirectoryInfo(ICDFilesData.ICDDirMFile + "\\" + tokens[8]);
                                    var diTarget = new DirectoryInfo(ICDFilesData.CreateDirIEC61850C);
                                    CopyAll(diSource, diTarget);
                                }
                                else
                                {
                                    var diSource1 = new DirectoryInfo(ICDFilesData.ICDDirMFile + "\\" + tokens[8]);
                                    var diTarget1 = new DirectoryInfo(ICDFilesData.CreateDirIEC61850C);
                                    CopyAll(diSource1, diTarget1);
                                }
                            }
                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                        }
                    }
                }
                #endregion IEC61850Client Master

                #region GraphicalDisplay Slave
                if (Utils.GraphicalDisplaySlaveList.Count > 0)
                {
                    #region Save Updated Txt Files In AppData
                    if (GDSlave.DsExcelData.Tables.Count > 0)
                    {
                        foreach (DataTable Dt in GDSlave.DsExcelData.Tables)
                        {
                            string TableName = Dt.TableName;
                            string[] tokens = TableName.Split('_');
                            GDSlave.XLSFileName = TableName.Substring(24);
                            string FolderName = tokens[0] + "_" + tokens[1];
                            string TxtFilePath = System.IO.Path.GetTempPath() + "protocol" + @"\" + "GUI" + @"\" + FolderName + @"\" + GDSlave.XLSFileName;
                            if (System.IO.File.Exists(TxtFilePath))
                            {
                                File.Delete(TxtFilePath);
                                ExportDataSetToCsvFile(Dt, TxtFilePath);
                            }
                            else
                            {
                                ExportDataSetToCsvFile(Dt, TxtFilePath);
                            }
                        }
                    }
                    #endregion Save Updated Txt Files In AppData

                    #region Check If Directory Exist in AppData 
                    //GDSlave.XLSDirFile = C:\Users\namrata\AppData\Local\Temp\protocol\SLD
                    if (!System.IO.Directory.Exists(GDSlave.XLSDirFile))
                    {
                        System.IO.Directory.CreateDirectory(GDSlave.XLSDirFile);
                        if (System.IO.Directory.Exists(GDSlave.XLSDirFile))
                        {
                            #region Move XML File From User Selected Location To AppData
                            string[] files = System.IO.Directory.GetFiles(GDSlave.XLSDirFile);//Get Files From Directory
                            foreach (string s in files)
                            {
                                fileName = System.IO.Path.GetFileName(s);
                                destFile = System.IO.Path.Combine(GDSlave.XLSDirFile, fileName);
                                File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                            }
                            #endregion Move XML File From User Selected Location To AppData
                        }
                        else { }
                    }
                    else
                    {
                        //Namrata:20/11/2019
                        string[] XMLfileNames = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml", SearchOption.TopDirectoryOnly); //Utils.XMLFilecopy
                        //string[] XMLfileNames = System.IO.Directory.GetFiles(Utils.XMLFilecopy, "*.xml", SearchOption.TopDirectoryOnly); //Utils.XMLFilecopy
                        if (XMLfileNames.Length != 0)
                        {
                            IsXMLExist = true;
                            foreach (string fileName1 in XMLfileNames)
                            {
                                File.Delete(XMLfileNames[0]);
                            }
                        }
                        else { IsXMLExist = false; }
                        #region Move XML File From User Selected Location To AppData
                        if (!FileExists(ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml"))
                        {
                            //Namrata: 22/10/2019
                            //Fixed XMLFile Name
                            File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                        }
                        else
                        {
                            File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                        }
                        #endregion Move XML File From User Selected Location To AppData
                    }
                    #endregion Check If Directory Exist in AppData

                    #region Create Directory For At User Location
                    if (!System.IO.Directory.Exists(FullXmlPath))
                    {
                        System.IO.Directory.CreateDirectory(FullXmlPath);
                        string[] fileEntries1 = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml");
                        string XmlfileName = string.Empty;
                        string XmlLocation = string.Empty;

                        foreach (string s in fileEntries1)
                        {
                            XmlfileName = System.IO.Path.GetFileName(s);
                            XmlLocation = System.IO.Path.Combine(MoveXMLFiles, XmlfileName);
                            if (File.Exists(XmlLocation))
                            {
                                System.IO.File.Delete(XmlLocation);
                            }
                            System.IO.File.Copy(s, XmlLocation, true);
                        }
                        if (!System.IO.Directory.Exists(GDSlave.CreateDirGDSlave))
                        {
                            System.IO.Directory.CreateDirectory(GDSlave.CreateDirGDSlave);
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(GDSlave.XLSDirFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = GDSlave.CreateDirGDSlave + "\\" + tokens[8];
                                var diSource = new DirectoryInfo(GDSlave.XLSDirFile + "\\" + tokens[8]);
                                var diTarget = new DirectoryInfo(GDSlave.CreateDirGDSlave);
                                CopyAll(diSource, diTarget);
                            }
                        }
                    }
                    #endregion Create Directory For At User Location
                    else
                    {
                        #region Move XMLFile From AppData To UserLocation
                        string[] fileEntries11 = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml");
                        string XmlfileName1 = string.Empty;
                        string XmlLocation1 = string.Empty;
                        foreach (string s in fileEntries11)
                        {
                            XmlfileName1 = System.IO.Path.GetFileName(s);
                            XmlLocation1 = System.IO.Path.Combine(MoveXMLFiles, XmlfileName1);
                            if (File.Exists(XmlLocation1))
                            {
                                System.IO.File.Delete(XmlLocation1);
                            }
                            System.IO.File.Copy(s, XmlLocation1, true);
                        }
                        #endregion Move XMLFile From AppData To UserLocation

                        if (!System.IO.Directory.Exists(GDSlave.CreateDirGDSlave))
                        {
                            System.IO.Directory.CreateDirectory(GDSlave.CreateDirGDSlave);

                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(GDSlave.XLSDirFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = GDSlave.CreateDirGDSlave + "\\" + tokens[8];
                                var diSource = new DirectoryInfo(GDSlave.XLSDirFile + "\\" + tokens[8]);
                                var diTarget = new DirectoryInfo(GDSlave.CreateDirGDSlave);
                                CopyAll(diSource, diTarget);
                            }
                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                        }
                        else
                        {
                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(GDSlave.XLSDirFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = GDSlave.CreateDirGDSlave + "\\" + tokens[8];
                                if (System.IO.Directory.Exists(GDSlave.SubDirName))
                                {
                                    System.IO.Directory.Delete(GDSlave.SubDirName, true);
                                    var diSource = new DirectoryInfo(GDSlave.XLSDirFile + "\\" + tokens[8]);
                                    var diTarget = new DirectoryInfo(GDSlave.CreateDirGDSlave);
                                    CopyAll(diSource, diTarget);
                                }
                                else
                                {
                                    var diSource1 = new DirectoryInfo(GDSlave.XLSDirFile + "\\" + tokens[8]);
                                    var diTarget1 = new DirectoryInfo(GDSlave.CreateDirGDSlave);
                                    CopyAll(diSource1, diTarget1);
                                }
                            }
                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                        }
                    }

                }
                #endregion GraphicalDisplay Slave

                #region Create .Zip File
                string ZipFileName = FullXmlPath + ".zip";
                if (File.Exists(ZipFileName))
                {
                    File.Delete(ZipFileName);
                    ZipFile.CreateFromDirectory(FullXmlPath, ZipFileName, CompressionLevel.NoCompression, false);
                }
                else
                {
                    ZipFile.CreateFromDirectory(FullXmlPath, ZipFileName, CompressionLevel.NoCompression, false);
                }
                #endregion Create .Zip File

                #region Delete Folder From UserLocation
                if (System.IO.Directory.Exists(FullXmlPath))
                {
                    //Namrata: 27/11/2019
                    DeleteDirectory(FullXmlPath);
                    //System.IO.Directory.Delete(FullXmlPath, true);
                }
                #endregion Delete Folder From UserLocation
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SaveXMLSLDClientServer1()
        {
            string strRoutineName = "frmOpenProPlus: SaveXMLSLDClientServer";
            try
            {
                XmlDocument Xmldoc = new XmlDocument();
                Utils.XMLFilecopy = ICDFilesData.XMLData;
                Xmldoc.Load(Utils.XMLFilecopy);

                #region Declarations
                string updatedXMLFile = Path.GetFileNameWithoutExtension(Utils.XMLFilecopy);//ABC      
                string XMLPath = ICDFilesData.DirectoryName + @"\" + ICDFilesData.XmlWithoutExt;
                string GetFilewithextension = Path.GetFileName(Utils.XMLFilecopy);
                string MoveXMLFiles = ICDFilesData.DirectoryName + @"\" + ICDFilesData.XmlWithoutExt; //For Moving XMLFile
                string FullXmlPath = ICDFilesData.DirectoryName + @"\" + ICDFilesData.XmlWithoutExt;
                //IEC61850Client Directory
                ICDFilesData.ICDDirMFile = System.IO.Path.GetTempPath() + "protocol" + @"\" + "IEC61850Client";
                //IEC61850Server Directory
                ICDFilesData.ICDDirSFile = System.IO.Path.GetTempPath() + "protocol" + @"\" + "IEC61850Server";
                //Namrata:08/04/2019 
                ICDFilesData.CopyXMLFile = System.IO.Path.GetTempPath() + "protocol";
                GDSlave.XLSDirFile = System.IO.Path.GetTempPath() + "protocol" + @"\" + "GUI";
                #endregion Declarations

                #region Create IEC61850Client,IEC61850Server,SLD Directory Path
                if ((ICDFilesData.IEC61850Client == true) || (ICDFilesData.IEC61850Server == true) || (GDSlave.GDisplaySlave == true))
                {
                    if (ICDFilesData.XmlWithoutExt != "")
                    {
                        ICDFilesData.CreateDirIEC61850C = ICDFilesData.DirectoryName + @"\" + ICDFilesData.XmlWithoutExt + @"\" + "protocol" + @"\" + "IEC61850Client";//Directory Path
                        ICDFilesData.CreateDirIEC61850S = ICDFilesData.DirectoryName + @"\" + ICDFilesData.XmlWithoutExt + @"\" + "protocol" + @"\" + "IEC61850Server";//Directory Path
                        //GDSlave.CreateDirGDSlave = GDSlave.DirName + @"\" + GDSlave.SLDWithoutExt + @"\" + "protocol" + @"\" + "GUI";
                    }
                    else
                    {
                        ICDFilesData.CreateDirIEC61850C = ICDFilesData.DirectoryName + @"\" + "protocol" + @"\" + "IEC61850Client";//Create DirectoryName
                        ICDFilesData.CreateDirIEC61850S = ICDFilesData.DirectoryName + @"\" + "protocol" + @"\" + "IEC61850Server";//Create DirectoryName

                        //GDSlave.CreateDirGDSlave = GDSlave.DirName + @"\" + "protocol" + @"\" + "GUI";
                    }
                }
                #endregion Create IEC61850Client,IEC61850Server,SLD Directory Path

                if (GDSlave.GDisplaySlave)
                {
                    if (GDSlave.SLDWithoutExt != "")
                    { GDSlave.CreateDirGDSlave = GDSlave.DirName + @"\" + GDSlave.SLDWithoutExt + @"\" + "protocol" + @"\" + "GUI"; }
                    else
                    { GDSlave.CreateDirGDSlave = GDSlave.DirName + @"\" + "protocol" + @"\" + "GUI"; }
                }

                #region Get XMLFile From AppData
                string fileName = string.Empty;
                string destFile = string.Empty;
                bool IsXMLExist;
                //string[] XMLfileNames = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml", SearchOption.TopDirectoryOnly); //Utils.XMLFilecopy
                ////string[] XMLfileNames = System.IO.Directory.GetFiles(Utils.XMLFilecopy, "*.xml", SearchOption.TopDirectoryOnly); //Utils.XMLFilecopy
                //if (XMLfileNames.Length != 0)
                //{
                //    IsXMLExist = true;
                //    foreach (string fileName1 in XMLfileNames)
                //    {
                //        File.Delete(XMLfileNames[0]);
                //    }
                //}
                //else { IsXMLExist = false; }
                #endregion Get XMLFile From AppData

                if (Utils.XMLFilePath != "")
                {
                    FolderNamewithoutexstension = Path.GetDirectoryName(Utils.XMLFilePath) + @"\" + Utils.ExtractedFileName;
                }

                #region IEC61850Server Slave
                if (Utils.IEC61850ServerSList.Count > 0)
                {
                    #region Check If Directory Exist in AppData 
                    //GDSlave.XLSDirFile = C:\Users\namrata\AppData\Local\Temp\protocol\SLD
                    if (!System.IO.Directory.Exists(ICDFilesData.ICDDirSFile))
                    {
                        System.IO.Directory.CreateDirectory(ICDFilesData.ICDDirSFile);
                        if (System.IO.Directory.Exists(ICDFilesData.ICDDirSFile))
                        {
                            #region Move XML File From User Selected Location To AppData
                            string[] files = System.IO.Directory.GetFiles(ICDFilesData.ICDDirSFile);//Get Files From Directory
                            foreach (string s in files)
                            {
                                fileName = System.IO.Path.GetFileName(s);
                                destFile = System.IO.Path.Combine(ICDFilesData.ICDDirSFile, fileName);
                                File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                            }
                            #endregion Move XML File From User Selected Location To AppData
                        }
                        else { }
                    }
                    else
                    {
                        #region Move XML File From User Selected Location To AppData
                        if (!FileExists(ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml"))
                        {
                            //Namrata: 22/10/2019
                            //Fixed XMLFile Name
                            File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                        }
                        #endregion Move XML File From User Selected Location To AppData
                    }
                    #endregion Check If Directory Exist in AppData

                    #region Create Directory For At User Location
                    if (!System.IO.Directory.Exists(FullXmlPath))
                    {
                        System.IO.Directory.CreateDirectory(FullXmlPath);
                        string[] fileEntries1 = System.IO.Directory.GetFiles(XMLPath, "*.xml");
                        string XmlfileName = string.Empty;
                        string XmlLocation = string.Empty;

                        foreach (string s in fileEntries1)
                        {
                            XmlfileName = System.IO.Path.GetFileName(s);
                            XmlLocation = System.IO.Path.Combine(MoveXMLFiles, XmlfileName);
                            if (File.Exists(XmlLocation))
                            {
                                System.IO.File.Delete(XmlLocation);
                            }
                            System.IO.File.Copy(s, XmlLocation, true);
                        }
                        if (!System.IO.Directory.Exists(ICDFilesData.CreateDirIEC61850S))
                        {
                            System.IO.Directory.CreateDirectory(ICDFilesData.CreateDirIEC61850S);
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(ICDFilesData.ICDDirSFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = ICDFilesData.CreateDirIEC61850S + "\\" + tokens[8];
                                var diSource = new DirectoryInfo(ICDFilesData.ICDDirSFile + "\\" + tokens[8]);
                                var diTarget = new DirectoryInfo(ICDFilesData.CreateDirIEC61850S);
                                CopyAll(diSource, diTarget);
                            }
                        }
                    }
                    #endregion Create Directory For At User Location
                    else
                    {
                        #region Move XMLFile From AppData To UserLocation
                        string[] fileEntries11 = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml");
                        string XmlfileName1 = string.Empty;
                        string XmlLocation1 = string.Empty;
                        foreach (string s in fileEntries11)
                        {
                            XmlfileName1 = System.IO.Path.GetFileName(s);
                            XmlLocation1 = System.IO.Path.Combine(MoveXMLFiles, XmlfileName1);
                            if (File.Exists(XmlLocation1))
                            {
                                System.IO.File.Delete(XmlLocation1);
                            }
                            System.IO.File.Copy(s, XmlLocation1, true);
                        }
                        #endregion Move XMLFile From AppData To UserLocation

                        if (!System.IO.Directory.Exists(ICDFilesData.CreateDirIEC61850S))
                        {
                            System.IO.Directory.CreateDirectory(ICDFilesData.CreateDirIEC61850S);

                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(ICDFilesData.ICDDirSFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = ICDFilesData.CreateDirIEC61850S + "\\" + tokens[8];
                                var diSource = new DirectoryInfo(ICDFilesData.ICDDirSFile + "\\" + tokens[8]);
                                var diTarget = new DirectoryInfo(ICDFilesData.CreateDirIEC61850S);
                                CopyAll(diSource, diTarget);
                            }
                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                        }
                        else
                        {
                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(ICDFilesData.ICDDirSFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = ICDFilesData.CreateDirIEC61850S + "\\" + tokens[8];
                                if (System.IO.Directory.Exists(GDSlave.SubDirName))
                                {
                                    System.IO.Directory.Delete(GDSlave.SubDirName, true);
                                    var diSource = new DirectoryInfo(ICDFilesData.ICDDirSFile + "\\" + tokens[8]);
                                    var diTarget = new DirectoryInfo(ICDFilesData.CreateDirIEC61850S);
                                    CopyAll(diSource, diTarget);
                                }
                                else
                                {
                                    var diSource1 = new DirectoryInfo(ICDFilesData.ICDDirSFile + "\\" + tokens[8]);
                                    var diTarget1 = new DirectoryInfo(ICDFilesData.CreateDirIEC61850S);
                                    CopyAll(diSource1, diTarget1);
                                }
                            }
                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                        }
                    }

                }
                #endregion IEC61850Server Slave

                #region IEC61850Client Master
                if (Utils.IEC61850ClientMList.Count > 0)
                {
                    #region Check If Directory Exist in AppData 
                    //GDSlave.XLSDirFile = C:\Users\namrata\AppData\Local\Temp\protocol\SLD
                    if (!System.IO.Directory.Exists(ICDFilesData.ICDDirMFile))
                    {
                        System.IO.Directory.CreateDirectory(ICDFilesData.ICDDirMFile);
                        if (System.IO.Directory.Exists(ICDFilesData.ICDDirMFile))
                        {
                            #region Move XML File From User Selected Location To AppData
                            string[] files = System.IO.Directory.GetFiles(ICDFilesData.ICDDirMFile);//Get Files From Directory
                            foreach (string s in files)
                            {
                                fileName = System.IO.Path.GetFileName(s);
                                destFile = System.IO.Path.Combine(ICDFilesData.ICDDirMFile, fileName);
                                File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                            }
                            #endregion Move XML File From User Selected Location To AppData
                        }
                        else { }
                    }
                    else
                    {
                        #region Move XML File From User Selected Location To AppData
                        if (!FileExists(ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml"))
                        {
                            //Namrata: 22/10/2019
                            //Fixed XMLFile Name
                            File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                        }
                        else
                        {
                            File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                        }
                        #endregion Move XML File From User Selected Location To AppData
                    }
                    #endregion Check If Directory Exist in AppData

                    #region Create Directory For At User Location
                    if (!System.IO.Directory.Exists(FullXmlPath))
                    {
                        System.IO.Directory.CreateDirectory(FullXmlPath);
                        string[] fileEntries1 = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml");
                        string XmlfileName = string.Empty;
                        string XmlLocation = string.Empty;

                        foreach (string s in fileEntries1)
                        {
                            XmlfileName = System.IO.Path.GetFileName(s);
                            XmlLocation = System.IO.Path.Combine(MoveXMLFiles, XmlfileName);
                            if (File.Exists(XmlLocation))
                            {
                                System.IO.File.Delete(XmlLocation);
                            }
                            System.IO.File.Copy(s, XmlLocation, true);
                        }
                        if (!System.IO.Directory.Exists(ICDFilesData.CreateDirIEC61850C))
                        {
                            System.IO.Directory.CreateDirectory(ICDFilesData.CreateDirIEC61850C);
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(ICDFilesData.CopyXMLFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = ICDFilesData.CreateDirIEC61850C;// + "\\" + tokens[8];
                                var diSource = new DirectoryInfo(ICDFilesData.ICDDirMFile);// + "\\" + tokens[8]);
                                var diTarget = new DirectoryInfo(ICDFilesData.CreateDirIEC61850C);
                                CopyAll(diSource, diTarget);
                            }
                        }
                    }
                    #endregion Create Directory For At User Location
                    else
                    {
                        #region Move XMLFile From AppData To UserLocation
                        string[] fileEntries11 = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml");
                        string XmlfileName1 = string.Empty;
                        string XmlLocation1 = string.Empty;
                        foreach (string s in fileEntries11)
                        {
                            XmlfileName1 = System.IO.Path.GetFileName(s);
                            XmlLocation1 = System.IO.Path.Combine(MoveXMLFiles, XmlfileName1);
                            if (File.Exists(XmlLocation1))
                            {
                                System.IO.File.Delete(XmlLocation1);
                            }
                            System.IO.File.Copy(s, XmlLocation1, true);
                        }
                        #endregion Move XMLFile From AppData To UserLocation

                        if (!System.IO.Directory.Exists(ICDFilesData.CreateDirIEC61850C))
                        {
                            System.IO.Directory.CreateDirectory(ICDFilesData.CreateDirIEC61850C);

                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(ICDFilesData.ICDDirMFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = ICDFilesData.CreateDirIEC61850S + "\\" + tokens[8];
                                var diSource = new DirectoryInfo(ICDFilesData.ICDDirMFile + "\\" + tokens[8]);
                                var diTarget = new DirectoryInfo(ICDFilesData.CreateDirIEC61850C);
                                CopyAll(diSource, diTarget);
                            }
                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                        }
                        else
                        {
                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(ICDFilesData.ICDDirMFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = ICDFilesData.CreateDirIEC61850C + "\\" + tokens[8];
                                if (System.IO.Directory.Exists(GDSlave.SubDirName))
                                {
                                    System.IO.Directory.Delete(GDSlave.SubDirName, true);
                                    var diSource = new DirectoryInfo(ICDFilesData.ICDDirMFile + "\\" + tokens[8]);
                                    var diTarget = new DirectoryInfo(ICDFilesData.CreateDirIEC61850C);
                                    CopyAll(diSource, diTarget);
                                }
                                else
                                {
                                    var diSource1 = new DirectoryInfo(ICDFilesData.ICDDirMFile + "\\" + tokens[8]);
                                    var diTarget1 = new DirectoryInfo(ICDFilesData.CreateDirIEC61850C);
                                    CopyAll(diSource1, diTarget1);
                                }
                            }
                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                        }
                    }
                }
                #endregion IEC61850Client Master

                #region GraphicalDisplay Slave
                if (Utils.GraphicalDisplaySlaveList.Count > 0)
                {
                    #region Save Updated Txt Files In AppData
                    if (GDSlave.DsExcelData.Tables.Count > 0)
                    {
                        foreach (DataTable Dt in GDSlave.DsExcelData.Tables)
                        {
                            string TableName = Dt.TableName;
                            string[] tokens = TableName.Split('_');
                            GDSlave.XLSFileName = TableName.Substring(24);
                            string FolderName = tokens[0] + "_" + tokens[1];
                            string TxtFilePath = System.IO.Path.GetTempPath() + "protocol" + @"\" + "GUI" + @"\" + FolderName + @"\" + GDSlave.XLSFileName;
                            if (System.IO.File.Exists(TxtFilePath))
                            {
                                File.Delete(TxtFilePath);
                                ExportDataSetToCsvFile(Dt, TxtFilePath);
                            }
                            else
                            {
                                ExportDataSetToCsvFile(Dt, TxtFilePath);
                            }
                        }
                    }
                    #endregion Save Updated Txt Files In AppData

                    #region Check If Directory Exist in AppData 
                    //GDSlave.XLSDirFile = C:\Users\namrata\AppData\Local\Temp\protocol\SLD
                    if (!System.IO.Directory.Exists(GDSlave.XLSDirFile))
                    {
                        System.IO.Directory.CreateDirectory(GDSlave.XLSDirFile);
                        if (System.IO.Directory.Exists(GDSlave.XLSDirFile))
                        {
                            #region Move XML File From User Selected Location To AppData
                            string[] files = System.IO.Directory.GetFiles(GDSlave.XLSDirFile);//Get Files From Directory
                            foreach (string s in files)
                            {
                                fileName = System.IO.Path.GetFileName(s);
                                destFile = System.IO.Path.Combine(GDSlave.XLSDirFile, fileName);
                                File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                            }
                            #endregion Move XML File From User Selected Location To AppData
                        }
                        else { }
                    }
                    else
                    {
                        //Namrata:20/11/2019
                        string[] XMLfileNames = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml", SearchOption.TopDirectoryOnly); //Utils.XMLFilecopy
                        //string[] XMLfileNames = System.IO.Directory.GetFiles(Utils.XMLFilecopy, "*.xml", SearchOption.TopDirectoryOnly); //Utils.XMLFilecopy
                        if (XMLfileNames.Length != 0)
                        {
                            IsXMLExist = true;
                            foreach (string fileName1 in XMLfileNames)
                            {
                                File.Delete(XMLfileNames[0]);
                            }
                        }
                        else { IsXMLExist = false; }
                        #region Move XML File From User Selected Location To AppData
                        if (!FileExists(ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml"))
                        {
                            //Namrata: 22/10/2019
                            //Fixed XMLFile Name
                            File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                        }
                        else
                        {
                            File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                        }
                        #endregion Move XML File From User Selected Location To AppData
                    }
                    #endregion Check If Directory Exist in AppData

                    #region Create Directory For At User Location
                    if (!System.IO.Directory.Exists(FullXmlPath))
                    {
                        System.IO.Directory.CreateDirectory(FullXmlPath);
                        string[] fileEntries1 = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml");
                        string XmlfileName = string.Empty;
                        string XmlLocation = string.Empty;

                        foreach (string s in fileEntries1)
                        {
                            XmlfileName = System.IO.Path.GetFileName(s);
                            XmlLocation = System.IO.Path.Combine(MoveXMLFiles, XmlfileName);
                            if (File.Exists(XmlLocation))
                            {
                                System.IO.File.Delete(XmlLocation);
                            }
                            System.IO.File.Copy(s, XmlLocation, true);
                        }
                        if (!System.IO.Directory.Exists(GDSlave.CreateDirGDSlave))
                        {
                            System.IO.Directory.CreateDirectory(GDSlave.CreateDirGDSlave);
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(GDSlave.XLSDirFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = GDSlave.CreateDirGDSlave + "\\" + tokens[8];
                                var diSource = new DirectoryInfo(GDSlave.XLSDirFile + "\\" + tokens[8]);
                                var diTarget = new DirectoryInfo(GDSlave.CreateDirGDSlave);
                                CopyAll(diSource, diTarget);
                            }
                        }
                    }
                    #endregion Create Directory For At User Location
                    else
                    {
                        #region Move XMLFile From AppData To UserLocation
                        string[] fileEntries11 = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml");
                        string XmlfileName1 = string.Empty;
                        string XmlLocation1 = string.Empty;
                        foreach (string s in fileEntries11)
                        {
                            XmlfileName1 = System.IO.Path.GetFileName(s);
                            XmlLocation1 = System.IO.Path.Combine(MoveXMLFiles, XmlfileName1);
                            if (File.Exists(XmlLocation1))
                            {
                                System.IO.File.Delete(XmlLocation1);
                            }
                            System.IO.File.Copy(s, XmlLocation1, true);
                        }
                        #endregion Move XMLFile From AppData To UserLocation

                        if (!System.IO.Directory.Exists(GDSlave.CreateDirGDSlave))
                        {
                            System.IO.Directory.CreateDirectory(GDSlave.CreateDirGDSlave);

                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(GDSlave.XLSDirFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = GDSlave.CreateDirGDSlave + "\\" + tokens[8];
                                var diSource = new DirectoryInfo(GDSlave.XLSDirFile + "\\" + tokens[8]);
                                var diTarget = new DirectoryInfo(GDSlave.CreateDirGDSlave);
                                CopyAll(diSource, diTarget);
                            }
                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                        }
                        else
                        {
                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(GDSlave.XLSDirFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = GDSlave.CreateDirGDSlave + "\\" + tokens[8];
                                if (System.IO.Directory.Exists(GDSlave.SubDirName))
                                {
                                    System.IO.Directory.Delete(GDSlave.SubDirName, true);
                                    var diSource = new DirectoryInfo(GDSlave.XLSDirFile + "\\" + tokens[8]);
                                    var diTarget = new DirectoryInfo(GDSlave.CreateDirGDSlave);
                                    CopyAll(diSource, diTarget);
                                }
                                else
                                {
                                    var diSource1 = new DirectoryInfo(GDSlave.XLSDirFile + "\\" + tokens[8]);
                                    var diTarget1 = new DirectoryInfo(GDSlave.CreateDirGDSlave);
                                    CopyAll(diSource1, diTarget1);
                                }
                            }
                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                        }
                    }

                }
                #endregion GraphicalDisplay Slave

                #region Create .Zip File
                string ZipFileName = FullXmlPath + ".zip";
                if (File.Exists(ZipFileName))
                {
                    File.Delete(ZipFileName);
                    ZipFile.CreateFromDirectory(FullXmlPath, ZipFileName, CompressionLevel.NoCompression, false);
                }
                else
                {
                    ZipFile.CreateFromDirectory(FullXmlPath, ZipFileName, CompressionLevel.NoCompression, false);
                }
                #endregion Create .Zip File

                #region Delete Folder From UserLocation
                if (System.IO.Directory.Exists(FullXmlPath))
                {
                    //Namrata: 27/11/2019
                    DeleteDirectory(FullXmlPath);
                    //System.IO.Directory.Delete(FullXmlPath, true);
                }
                #endregion Delete Folder From UserLocation
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SaveXMLSLDClientServer()
        {
            string strRoutineName = "frmOpenProPlus: SaveXMLSLDClientServer";
            try
            {
                XmlDocument Xmldoc = new XmlDocument();
                Utils.XMLFilecopy = ICDFilesData.XMLData;
                Xmldoc.Load(Utils.XMLFilecopy);

                #region Declarations
                string updatedXMLFile = Path.GetFileNameWithoutExtension(Utils.XMLFilecopy);//ABC      
                string XMLPath = ICDFilesData.DirectoryName + @"\" + ICDFilesData.XmlWithoutExt;
                string GetFilewithextension = Path.GetFileName(Utils.XMLFilecopy);
                string MoveXMLFiles = ICDFilesData.DirectoryName + @"\" + ICDFilesData.XmlWithoutExt; //For Moving XMLFile
                string FullXmlPath = ICDFilesData.DirectoryName + @"\" + ICDFilesData.XmlWithoutExt;

                //IEC61850Client Directory
                ICDFilesData.ICDDirMFile = System.IO.Path.GetTempPath() + "protocol" + @"\" + "IEC61850Client";

                //IEC61850Server Directory
                ICDFilesData.ICDDirSFile = System.IO.Path.GetTempPath() + "protocol" + @"\" + "IEC61850Server";

                //SLD Directory
                GDSlave.XLSDirFile = System.IO.Path.GetTempPath() + "protocol" + @"\" + "GUI";

                //Namrata:08/04/2019 
                ICDFilesData.CopyXMLFile = System.IO.Path.GetTempPath() + "protocol";

                #endregion Declarations

                #region Create IEC61850Client,IEC61850Server,SLD Directory Path
                if ((ICDFilesData.IEC61850Client == true) || (ICDFilesData.IEC61850Server == true) || (GDSlave.GDisplaySlave == true))
                {
                    if (ICDFilesData.XmlWithoutExt != "")
                    {
                        ICDFilesData.CreateDirIEC61850C = ICDFilesData.DirectoryName + @"\" + ICDFilesData.XmlWithoutExt + @"\" + "protocol";// + @"\" + "IEC61850Client";
                        ICDFilesData.CreateDirIEC61850S = ICDFilesData.DirectoryName + @"\" + ICDFilesData.XmlWithoutExt + @"\" + "protocol" + @"\" + "IEC61850Server";
                    }
                    else
                    {
                        ICDFilesData.CreateDirIEC61850C = ICDFilesData.DirectoryName + @"\" + "protocol";// + @"\" + "IEC61850Client";
                        ICDFilesData.CreateDirIEC61850S = ICDFilesData.DirectoryName + @"\" + "protocol" + @"\" + "IEC61850Server";
                    }
                }
                if (GDSlave.GDisplaySlave)
                {
                    if (GDSlave.SLDWithoutExt != "")
                    { GDSlave.CreateDirGDSlave = GDSlave.DirName + @"\" + GDSlave.SLDWithoutExt + @"\" + "protocol" + @"\" + "GUI"; }
                    else
                    { GDSlave.CreateDirGDSlave = GDSlave.DirName + @"\" + "protocol" + @"\" + "GUI"; }
                }
                #endregion Create IEC61850Client,IEC61850Server,SLD Directory Path

                #region Get XMLFile From AppData
                string fileName = string.Empty; string destFile = string.Empty; bool IsXMLExist;

                #endregion Get XMLFile From AppData
                if (Utils.XMLFilePath != "")
                {
                    FolderNamewithoutexstension = Path.GetDirectoryName(Utils.XMLFilePath) + @"\" + Utils.ExtractedFileName;
                }

                #region move XML file from User Location to Appdata
                string[] XMLfileNames = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml", SearchOption.TopDirectoryOnly); //Utils.XMLFilecopy
                if (XMLfileNames.Length != 0)
                {
                    IsXMLExist = true;
                    foreach (string fileName1 in XMLfileNames)
                    {
                        File.Delete(XMLfileNames[0]);
                        File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                    }
                }
                else
                {
                    File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                }
                #endregion move XML file from User Location to Appdata
                
                if (Utils.IEC61850ClientMList.Count > 0)
                {
                    #region Check If Directory Exist in AppData 
                    if (!System.IO.Directory.Exists(ICDFilesData.ICDDirMFile))
                    {
                        System.IO.Directory.CreateDirectory(ICDFilesData.ICDDirMFile);
                        if (System.IO.Directory.Exists(ICDFilesData.ICDDirMFile))
                        {
                            #region Move XML File From User Selected Location To AppData
                            string[] files = System.IO.Directory.GetFiles(ICDFilesData.ICDDirMFile);//Get Files From Directory
                            foreach (string s in files)
                            {
                                fileName = System.IO.Path.GetFileName(s);
                                destFile = System.IO.Path.Combine(ICDFilesData.ICDDirMFile, fileName);
                                File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                            }
                            #endregion Move XML File From User Selected Location To AppData
                        }
                        else { }
                    }
                    else
                    {
                    }
                    #endregion Check If Directory Exist in AppData

                    #region Create Directory For At User Location
                    if (!System.IO.Directory.Exists(FullXmlPath))
                    {
                        System.IO.Directory.CreateDirectory(FullXmlPath);
                        string[] fileEntries1 = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml");
                        string XmlfileName = string.Empty;
                        string XmlLocation = string.Empty;

                        foreach (string s in fileEntries1)
                        {
                            XmlfileName = System.IO.Path.GetFileName(s);
                            XmlLocation = System.IO.Path.Combine(MoveXMLFiles, XmlfileName);
                            if (File.Exists(XmlLocation))
                            {
                                System.IO.File.Delete(XmlLocation);
                            }
                            System.IO.File.Copy(s, XmlLocation, true);
                        }
                        if (!System.IO.Directory.Exists(ICDFilesData.CreateDirIEC61850C))
                        {
                            string temppath = System.IO.Path.GetTempPath() + "protocol";
                            System.IO.Directory.CreateDirectory(ICDFilesData.CreateDirIEC61850C);
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(temppath);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = ICDFilesData.CreateDirIEC61850C + "\\" + tokens[7];
                                var diSource = new DirectoryInfo(temppath + "\\" + tokens[7]);
                                var diTarget = new DirectoryInfo(ICDFilesData.CreateDirIEC61850C);
                                CopyAll(diSource, diTarget);
                            }
                        }
                    }
                    #endregion Create Directory For At User Location
                    else
                    {
                        #region Move XMLFile From AppData To UserLocation
                        string[] fileEntries11 = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml");
                        string XmlfileName1 = string.Empty;
                        string XmlLocation1 = string.Empty;
                        foreach (string s in fileEntries11)
                        {
                            XmlfileName1 = System.IO.Path.GetFileName(s);
                            XmlLocation1 = System.IO.Path.Combine(MoveXMLFiles, XmlfileName1);
                            if (File.Exists(XmlLocation1))
                            {
                                System.IO.File.Delete(XmlLocation1);
                            }
                            System.IO.File.Copy(s, XmlLocation1, true);
                        }
                        #endregion Move XMLFile From AppData To UserLocation

                        if (!System.IO.Directory.Exists(ICDFilesData.CreateDirIEC61850C))
                        {
                            System.IO.Directory.CreateDirectory(ICDFilesData.CreateDirIEC61850C);

                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(ICDFilesData.ICDDirMFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = ICDFilesData.CreateDirIEC61850C + "\\" + tokens[8];
                                var diSource = new DirectoryInfo(ICDFilesData.ICDDirMFile + "\\" + tokens[8]);
                                var diTarget = new DirectoryInfo(ICDFilesData.CreateDirIEC61850C);
                                CopyAll(diSource, diTarget);
                            }
                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                        }
                        else
                        {
                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(ICDFilesData.ICDDirMFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = ICDFilesData.CreateDirIEC61850C + "\\" + tokens[8];
                                if (System.IO.Directory.Exists(GDSlave.SubDirName))
                                {
                                    System.IO.Directory.Delete(GDSlave.SubDirName, true);
                                    var diSource = new DirectoryInfo(ICDFilesData.ICDDirMFile + "\\" + tokens[8]);
                                    var diTarget = new DirectoryInfo(ICDFilesData.CreateDirIEC61850C);
                                    CopyAll(diSource, diTarget);
                                }
                                else
                                {
                                    var diSource1 = new DirectoryInfo(ICDFilesData.ICDDirMFile + "\\" + tokens[8]);
                                    var diTarget1 = new DirectoryInfo(ICDFilesData.CreateDirIEC61850C);
                                    CopyAll(diSource1, diTarget1);
                                }
                            }
                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                        }
                    }
                }

                #region IEC61850Server Slave
                if (Utils.IEC61850ServerSList.Count > 0)
                {
                    #region Check If Directory Exist in AppData 
                    //GDSlave.XLSDirFile = C:\Users\namrata\AppData\Local\Temp\protocol\SLD
                    if (!System.IO.Directory.Exists(ICDFilesData.ICDDirSFile))
                    {
                        System.IO.Directory.CreateDirectory(ICDFilesData.ICDDirSFile);
                        if (System.IO.Directory.Exists(ICDFilesData.ICDDirSFile))
                        {
                            #region Move XML File From User Selected Location To AppData
                            //string[] files = System.IO.Directory.GetFiles(ICDFilesData.ICDDirSFile);//Get Files From Directory
                            //foreach (string s in files)
                            //{
                            //    fileName = System.IO.Path.GetFileName(s);
                            //    destFile = System.IO.Path.Combine(ICDFilesData.ICDDirSFile, fileName);
                            //    File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                            //}
                            #endregion Move XML File From User Selected Location To AppData
                        }
                        else { }
                    }
                    else
                    {
                        #region Move XML File From User Selected Location To AppData
                        //if (!FileExists(ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml"))
                        //{
                        //    //Namrata: 22/10/2019
                        //    //Fixed XMLFile Name
                        //    File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                        //}
                        #endregion Move XML File From User Selected Location To AppData
                    }
                    #endregion Check If Directory Exist in AppData

                    #region Create Directory For At User Location
                    if (!System.IO.Directory.Exists(FullXmlPath))
                    {
                        System.IO.Directory.CreateDirectory(FullXmlPath);
                        string[] fileEntries1 = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml");
                        string XmlfileName = string.Empty;
                        string XmlLocation = string.Empty;

                        foreach (string s in fileEntries1)
                        {
                            XmlfileName = System.IO.Path.GetFileName(s);
                            XmlLocation = System.IO.Path.Combine(MoveXMLFiles, XmlfileName);
                            if (File.Exists(XmlLocation))
                            {
                                System.IO.File.Delete(XmlLocation);
                            }
                            System.IO.File.Copy(s, XmlLocation, true);
                        }
                        if (!System.IO.Directory.Exists(ICDFilesData.CreateDirIEC61850S))
                        {
                            System.IO.Directory.CreateDirectory(ICDFilesData.CreateDirIEC61850S);
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(ICDFilesData.ICDDirSFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = ICDFilesData.CreateDirIEC61850S + "\\" + tokens[8];
                                var diSource = new DirectoryInfo(ICDFilesData.ICDDirSFile + "\\" + tokens[8]);
                                var diTarget = new DirectoryInfo(ICDFilesData.CreateDirIEC61850S);
                                CopyAll(diSource, diTarget);
                            }
                        }
                    }
                    #endregion Create Directory For At User Location
                    else
                    {
                        #region Move XMLFile From AppData To UserLocation
                        string[] fileEntries11 = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml");
                        string XmlfileName1 = string.Empty;
                        string XmlLocation1 = string.Empty;
                        foreach (string s in fileEntries11)
                        {
                            XmlfileName1 = System.IO.Path.GetFileName(s);
                            XmlLocation1 = System.IO.Path.Combine(MoveXMLFiles, XmlfileName1);
                            if (File.Exists(XmlLocation1))
                            {
                                System.IO.File.Delete(XmlLocation1);
                            }
                            System.IO.File.Copy(s, XmlLocation1, true);
                        }
                        #endregion Move XMLFile From AppData To UserLocation

                        if (!System.IO.Directory.Exists(ICDFilesData.CreateDirIEC61850S))
                        {
                            System.IO.Directory.CreateDirectory(ICDFilesData.CreateDirIEC61850S);

                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(ICDFilesData.ICDDirSFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = ICDFilesData.CreateDirIEC61850S + "\\" + tokens[8];
                                var diSource = new DirectoryInfo(ICDFilesData.ICDDirSFile + "\\" + tokens[8]);
                                var diTarget = new DirectoryInfo(ICDFilesData.CreateDirIEC61850S);
                                CopyAll(diSource, diTarget);
                            }
                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                        }
                        else
                        {
                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(ICDFilesData.ICDDirSFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = ICDFilesData.CreateDirIEC61850S + "\\" + tokens[8];
                                if (System.IO.Directory.Exists(GDSlave.SubDirName))
                                {
                                    System.IO.Directory.Delete(GDSlave.SubDirName, true);
                                    var diSource = new DirectoryInfo(ICDFilesData.ICDDirSFile + "\\" + tokens[8]);
                                    var diTarget = new DirectoryInfo(ICDFilesData.CreateDirIEC61850S);
                                    CopyAll(diSource, diTarget);
                                }
                                else
                                {
                                    var diSource1 = new DirectoryInfo(ICDFilesData.ICDDirSFile + "\\" + tokens[8]);
                                    var diTarget1 = new DirectoryInfo(ICDFilesData.CreateDirIEC61850S);
                                    CopyAll(diSource1, diTarget1);
                                }
                            }
                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                        }
                    }

                }
                #endregion IEC61850Server Slave



                #region GraphicalDisplay Slave
                if (Utils.GraphicalDisplaySlaveList.Count > 0)
                {
                    #region Save Updated Txt Files In AppData
                    if (GDSlave.DsExcelData.Tables.Count > 0)
                    {
                        foreach (DataTable Dt in GDSlave.DsExcelData.Tables)
                        {
                            string TableName = Dt.TableName;
                            string[] tokens = TableName.Split('_');
                            GDSlave.XLSFileName = TableName.Substring(24);
                            string FolderName = tokens[0] + "_" + tokens[1];
                            string TxtFilePath = System.IO.Path.GetTempPath() + "protocol" + @"\" + "GUI" + @"\" + FolderName + @"\" + GDSlave.XLSFileName;
                            if (System.IO.File.Exists(TxtFilePath))
                            {
                                File.Delete(TxtFilePath);
                                ExportDataSetToCsvFile(Dt, TxtFilePath);
                            }
                            else
                            {
                                ExportDataSetToCsvFile(Dt, TxtFilePath);
                            }
                        }
                    }
                    #endregion Save Updated Txt Files In AppData

                    #region Check If Directory Exist in AppData 
                    //GDSlave.XLSDirFile = C:\Users\namrata\AppData\Local\Temp\protocol\SLD
                    if (!System.IO.Directory.Exists(GDSlave.XLSDirFile))
                    {
                        System.IO.Directory.CreateDirectory(GDSlave.XLSDirFile);
                        if (System.IO.Directory.Exists(GDSlave.XLSDirFile))
                        {
                            #region Move XML File From User Selected Location To AppData
                            string[] files = System.IO.Directory.GetFiles(GDSlave.XLSDirFile);//Get Files From Directory
                            foreach (string s in files)
                            {
                                fileName = System.IO.Path.GetFileName(s);
                                destFile = System.IO.Path.Combine(GDSlave.XLSDirFile, fileName);
                                File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                            }
                            #endregion Move XML File From User Selected Location To AppData
                        }
                        else { }
                    }
                    else
                    {
                        ////Namrata: 20 / 11 / 2019
                        //string[] XMLfileNames = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml", SearchOption.TopDirectoryOnly); //Utils.XMLFilecopy
                        //string[] XMLfileNames = System.IO.Directory.GetFiles(Utils.XMLFilecopy, "*.xml", SearchOption.TopDirectoryOnly); //Utils.XMLFilecopy
                        //if (XMLfileNames.Length != 0)
                        //{
                        //    IsXMLExist = true;
                        //    foreach (string fileName1 in XMLfileNames)
                        //    {
                        //        File.Delete(XMLfileNames[0]);
                        //    }
                        //}
                        //else { IsXMLExist = false; }
                        //#region Move XML File From User Selected Location To AppData
                        //if (!FileExists(ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml"))
                        //{
                        //    Namrata: 22 / 10 / 2019
                        //    Fixed XMLFile Name
                        //    File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                        //}
                        //else
                        //{
                        //    File.Move(ICDFilesData.XmlPath, ICDFilesData.CopyXMLFile + @"\" + "openproplus_config.xml");
                        //}
                        //#endregion Move XML File From User Selected Location To AppData
                    }
                    #endregion Check If Directory Exist in AppData

                    #region Create Directory For At User Location
                    if (!System.IO.Directory.Exists(FullXmlPath))
                    {
                        System.IO.Directory.CreateDirectory(FullXmlPath);
                        string[] fileEntries1 = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml");
                        string XmlfileName = string.Empty;
                        string XmlLocation = string.Empty;

                        foreach (string s in fileEntries1)
                        {
                            XmlfileName = System.IO.Path.GetFileName(s);
                            XmlLocation = System.IO.Path.Combine(MoveXMLFiles, XmlfileName);
                            if (File.Exists(XmlLocation))
                            {
                                System.IO.File.Delete(XmlLocation);
                            }
                            System.IO.File.Copy(s, XmlLocation, true);
                        }
                        if (!System.IO.Directory.Exists(GDSlave.CreateDirGDSlave))
                        {
                            System.IO.Directory.CreateDirectory(GDSlave.CreateDirGDSlave);
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(GDSlave.XLSDirFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = GDSlave.CreateDirGDSlave + "\\" + tokens[8];
                                var diSource = new DirectoryInfo(GDSlave.XLSDirFile + "\\" + tokens[8]);
                                var diTarget = new DirectoryInfo(GDSlave.CreateDirGDSlave);
                                CopyAll(diSource, diTarget);
                            }
                        }
                    }
                    #endregion Create Directory For At User Location
                    else
                    {
                        #region Move XMLFile From AppData To UserLocation
                        string[] fileEntries11 = System.IO.Directory.GetFiles(ICDFilesData.CopyXMLFile, "*.xml");
                        string XmlfileName1 = string.Empty;
                        string XmlLocation1 = string.Empty;
                        foreach (string s in fileEntries11)
                        {
                            XmlfileName1 = System.IO.Path.GetFileName(s);
                            XmlLocation1 = System.IO.Path.Combine(MoveXMLFiles, XmlfileName1);
                            if (File.Exists(XmlLocation1))
                            {
                                System.IO.File.Delete(XmlLocation1);
                            }
                            System.IO.File.Copy(s, XmlLocation1, true);
                        }
                        #endregion Move XMLFile From AppData To UserLocation

                        if (!System.IO.Directory.Exists(GDSlave.CreateDirGDSlave))
                        {
                            System.IO.Directory.CreateDirectory(GDSlave.CreateDirGDSlave);

                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(GDSlave.XLSDirFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = GDSlave.CreateDirGDSlave + "\\" + tokens[8];
                                var diSource = new DirectoryInfo(GDSlave.XLSDirFile + "\\" + tokens[8]);
                                var diTarget = new DirectoryInfo(GDSlave.CreateDirGDSlave);
                                CopyAll(diSource, diTarget);
                            }
                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                        }
                        else
                        {
                            #region Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(GDSlave.XLSDirFile);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                string[] tokens = subdirectory.Split('\\');
                                GDSlave.SubDirName = GDSlave.CreateDirGDSlave + "\\" + tokens[8];
                                if (System.IO.Directory.Exists(GDSlave.SubDirName))
                                {
                                    System.IO.Directory.Delete(GDSlave.SubDirName, true);
                                    var diSource = new DirectoryInfo(GDSlave.XLSDirFile + "\\" + tokens[8]);
                                    var diTarget = new DirectoryInfo(GDSlave.CreateDirGDSlave);
                                    CopyAll(diSource, diTarget);
                                }
                                else
                                {
                                    var diSource1 = new DirectoryInfo(GDSlave.XLSDirFile + "\\" + tokens[8]);
                                    var diTarget1 = new DirectoryInfo(GDSlave.CreateDirGDSlave);
                                    CopyAll(diSource1, diTarget1);
                                }
                            }
                            #endregion Move All Directories(Folders) Exist in "C:\Users\namrata\AppData\Local\Temp\protocol\SLD"
                        }
                    }

                }
                #endregion GraphicalDisplay Slave

                #region Create .Zip File
                string ZipFileName = FullXmlPath + ".zip";
                if (File.Exists(ZipFileName))
                {
                    File.Delete(ZipFileName);
                    ZipFile.CreateFromDirectory(FullXmlPath, ZipFileName, CompressionLevel.NoCompression, false);
                }
                else
                {
                    ZipFile.CreateFromDirectory(FullXmlPath, ZipFileName, CompressionLevel.NoCompression, false);
                }
                #endregion Create .Zip File

                #region Delete Folder From UserLocation
                if (System.IO.Directory.Exists(FullXmlPath))
                {
                    DeleteFilesAndFoldersRecursively(FullXmlPath);
                    // System.IO.Directory.Delete(FullXmlPath, true);
                }
                #endregion Delete Folder From UserLocation
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void DeleteFilesAndFoldersRecursively(string target_dir)
        {
            foreach (string file in System.IO.Directory.GetFiles(target_dir))
            {
                File.Delete(file);
            }

            foreach (string subDir in System.IO.Directory.GetDirectories(target_dir))
            {
                DeleteFilesAndFoldersRecursively(subDir);
            }

            Thread.Sleep(1); // This makes the difference between whether it works or not. Sleep(0) is not enough.
            System.IO.Directory.Delete(target_dir);
        }
        public static void DeleteDirectory(string target_dir)
        {
            foreach (string file in System.IO.Directory.GetFiles(target_dir))
            {
                File.Delete(file);
            }
            foreach (string subDir in System.IO.Directory.GetDirectories(target_dir))
            {
                DeleteDirectory(subDir);
            }
            Thread.Sleep(1); // This makes the difference between whether it works or not. Sleep(0) is not enough.
            System.IO.Directory.Delete(target_dir);
        }
        private bool CheckVersions(string xmlFilePath)
        {
            string strRoutineName = "frmOpenProPlus: CheckVersions";
            try
            {
                string[] openproconfigVers = Utils.GetVersionsFromOPPConfigXSD();
                string[] commonconfigVers = Utils.GetVersionsFromCommonXSD();
                string[] xmlfileVers = Utils.GetVersionsFromXML(xmlFilePath);
                if (openproconfigVers != null && commonconfigVers != null && xmlfileVers != null)
                {
                    if (openproconfigVers.Count() > 0 && commonconfigVers.Count() > 0 && xmlfileVers.Count() > 0)
                    {
                        if (!string.IsNullOrEmpty(openproconfigVers[0]) && !string.IsNullOrEmpty(commonconfigVers[0]))
                        {
                            if (!string.IsNullOrEmpty(xmlfileVers[0]))
                            {
                                if (xmlfileVers[0] != "OppConfiguratorVer=" + Utils.AssemblyVersion)
                                {
                                    MessageBox.Show("'" + Path.GetFileName(xmlFilePath) + "' file was created using OpenPro+Configurator version " + Utils.GetVersionSubstring(xmlfileVers[0]) + ", and the current OpenPro+Configurator version is " + Utils.AssemblyVersion, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                    return false;
                                }
                            }
                            if (openproconfigVers[0] != commonconfigVers[0])
                            {
                                MessageBox.Show("OpenPro+Configurator version (" + Utils.AssemblyVersion + ") is mismatched in openproplus_config.xsd and common_config.xsd", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                return false;
                            }
                            if (openproconfigVers[0] != "OppConfiguratorVer=" + Utils.AssemblyVersion)
                            {
                                MessageBox.Show("OpenPro+Configurator version (" + Utils.AssemblyVersion + ") is mismatched in openproplus_config.xsd", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                return false;
                            }
                            if (commonconfigVers[0] != "OppConfiguratorVer=" + Utils.AssemblyVersion)
                            {
                                MessageBox.Show("OpenPro+Configurator version (" + Utils.AssemblyVersion + ") is mismatched in common_config.xsd", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                return false;
                            }
                        }
                    }
                    if (openproconfigVers.Count() > 1 && commonconfigVers.Count() > 1 && xmlfileVers.Count() > 1)
                    {
                        if (!string.IsNullOrEmpty(openproconfigVers[1]) && !string.IsNullOrEmpty(commonconfigVers[1]))
                        {
                            if (!string.IsNullOrEmpty(xmlfileVers[1]))
                            {
                                if (xmlfileVers[1] != openproconfigVers[1])
                                {
                                    MessageBox.Show("'" + Path.GetFileName(xmlFilePath) + "' file was created using openproplus_config.xsd version " + Utils.GetVersionSubstring(xmlfileVers[1]) + ", and the current openproplus_config.xsd version is " + Utils.GetVersionSubstring(openproconfigVers[1]), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                    return false;
                                }
                            }
                            if (openproconfigVers[1] != commonconfigVers[1])
                            {
                                MessageBox.Show("openproplus_config.xsd version (" + Utils.GetVersionSubstring(openproconfigVers[1]) + ") is mismatched in common_config.xsd", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                return false;
                            }
                        }
                    }
                    if (openproconfigVers.Count() > 2 && commonconfigVers.Count() > 2 && xmlfileVers.Count() > 2)
                    {
                        if (!string.IsNullOrEmpty(openproconfigVers[2]) && !string.IsNullOrEmpty(commonconfigVers[2]))
                        {
                            if (!string.IsNullOrEmpty(xmlfileVers[2]))
                            {
                                if (xmlfileVers[2] != commonconfigVers[2])
                                {
                                    MessageBox.Show("'" + Path.GetFileName(xmlFilePath) + "' file was created using common_config.xsd version " + Utils.GetVersionSubstring(xmlfileVers[2]) + ", and the current common_config.xsd version is " + Utils.GetVersionSubstring(commonconfigVers[2]), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                    return false;
                                }
                            }
                            if (openproconfigVers[2] != commonconfigVers[2])
                            {
                                MessageBox.Show("common_config.xsd version (" + Utils.GetVersionSubstring(commonconfigVers[2]) + ") is mismatched in openproplus_config.xsd", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        string GetProtocolFolder = "";
        private void openXMLFile()
        {
            string strRoutineName = "frmOpenProPlus: openXMLFile";
            try
            {
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Full)
                {
                    #region AppMode=Full

                    ofdXMLFile.Filter = "XML Files| *.xml|Zip Files|*.zip;*.rar"; //"XML Files|*.xml";
                    ofdXMLFile.FilterIndex = 1;
                    ofdXMLFile.RestoreDirectory = true;
                    ofdXMLFile.Title = "Browse ZIP File";//"Browse XML File";
                    if (ofdXMLFile.ShowDialog() == DialogResult.OK)
                    {
                        string openedFile = ofdXMLFile.FileName; //Namrata: 18/11/2017
                        string DirectoryExtention = Path.GetExtension(ofdXMLFile.FileName);
                        toolStripStatusLabel1.Visible = true; tspFileName.Visible = true;
                        tspFileName.Text = openedFile;
                        #region 
                        //Namrata: 23/10/2019
                        if (DirectoryExtention == ".zip")
                        {
                            string extractPath = System.IO.Path.GetTempPath() + "protocol";//Path.GetDirectoryName(openedFile);
                            string ZIPName = Path.GetDirectoryName(openedFile);
                            string ZipFolderName = Path.GetFileNameWithoutExtension(openedFile);
                            string GetXMLPath = Path.Combine(ZIPName, ZipFolderName);

                            Utils.DirNameSave = ZIPName + @"\" + ZipFolderName;
                            if (System.IO.Directory.Exists(Utils.DirNameSave))
                            {
                                //Namrata: 27/11/2019
                                DeleteDirectory(Utils.DirNameSave);
                                //System.IO.Directory.Delete(Utils.DirNameSave, true);
                            }
                            if (!System.IO.Directory.Exists(extractPath))
                            {
                                System.IO.Directory.CreateDirectory(extractPath);
                                ZipFile.ExtractToDirectory(openedFile, GetXMLPath);
                            }
                            else
                            { 
                                //Namrata: 27/11/2019
                                DeleteDirectory(extractPath);
                                //System.IO.Directory.Delete(extractPath, true);
                                System.IO.Directory.CreateDirectory(extractPath);
                                ZipFile.ExtractToDirectory(openedFile, GetXMLPath);
                            }
                            string[] fileEntries1 = System.IO.Directory.GetFiles(GetXMLPath, "*.xml");

                            string fileName1 = string.Empty;
                            string destFile1 = string.Empty;
                            foreach (string s in fileEntries1)
                            {
                                fileName1 = System.IO.Path.GetFileName(s);
                                destFile1 = System.IO.Path.Combine(extractPath, fileName1);
                                System.IO.File.Move(s, destFile1);
                                ofdXMLFile.FileName = destFile1;
                            }
                            string Protocol = "protocol";
                            string GetProtocol = Path.Combine(GetXMLPath, Protocol);
                            if (!System.IO.Directory.Exists(GetProtocol))
                            {
                                if (!CheckVersions(GetXMLPath))
                                {
                                    VersionMatch = false;
                                    DialogResult rslt = MessageBox.Show("Do you still want to continue with mismatched versions of OpenPro+Configurator, xml and xsd files?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                                    if (rslt == DialogResult.No)
                                    {
                                        return;
                                    }
                                    else { }
                                }
                                else
                                {
                                    VersionMatch = true; //Ajay: 11/01/2019
                                }
                                //Namrata:26/04/2018
                                Utils.XMLFolderPath = Path.GetDirectoryName(GetProtocol);

                                #region IEC61850
                                //Namrata: 27/04/2018
                                ICDFilesData.DirectoryName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
                                ICDFilesData.XmlName = Path.GetFileName(ofdXMLFile.FileName); // Get XML Name
                                ICDFilesData.XmlPath = ofdXMLFile.FileName; // XML with full path
                                ICDFilesData.XmlWithoutExt = "";

                                #region ClearDatasets
                                Utils.dsIED.Clear();
                                Utils.dsIED.Tables.Clear();
                                Utils.DsRCB.Clear();
                                Utils.DsRCBData.Clear();
                                Utils.dsResponseType.Clear();
                                Utils.DsResponseType.Clear();
                                Utils.dsIED.Clear();
                                Utils.dsIEDName.Clear();
                                Utils.DsAllConfigurationData.Clear();
                                Utils.DsAllConfigureData.Clear();
                                Utils.DsRCBDataset.Clear();
                                Utils.DsRCBDS.Clear();
                                Utils.DtRCBdata.Clear();
                                #endregion ClearDatasets
                                #endregion IEC61850

                                #region GDisplay
                                //Namrata: 27/04/2018
                                GDSlave.DirName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
                                GDSlave.SLDName = Path.GetFileName(ofdXMLFile.FileName); //Get XML Name
                                GDSlave.SLDPath = ofdXMLFile.FileName;//XML with full path
                                GDSlave.SLDWithoutExt = "";
                                #region ClearDatasets
                                GDSlave.DsExcelData.Tables.Clear();
                                GDSlave.DsExcelData.Clear();
                                GDSlave.DtExcelData.Clear();
                                GDSlave.DsExportData.Tables.Clear();
                                GDSlave.DTAIImage.Clear();
                                GDSlave.DTDIDOImage.Clear();
                                #endregion ClearDatasets
                                #endregion GDisplay
                                this.MyMruList.AddFile(openedFile); //Now give it to the MRUManager
                                int result = 0;
                                string errMsg = "XML file is valid...";
                                ResetConfiguratorState(false);
                                showLoading();
                                xmlFile = "";//reset old filename...
                                pnlValidationMessages.Visible = false;
                                ListViewItem lvi;
                                lvi = new ListViewItem("Validating file: " + ofdXMLFile.FileName);
                                //Namrata: 27/7/2017
                                toolStripStatusLabel1.Visible = true;  //Display File Name in Toolstrip 
                                tspFileName.Text = openedFile;
                                lvValidationMessages.Items.Add(lvi);
                                lvi = new ListViewItem("");
                                lvValidationMessages.Items.Add(lvi);
                                result = opcHandle.loadXML(ofdXMLFile.FileName, tvItems, out IsXmlValid);
                                if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
                                else
                                {
                                    pnlValidationMessages.Visible = true;
                                    pnlValidationMessages.BringToFront();
                                    //Namrata: 13/12/2019
                                    panel1.BringToFront();
                                    lvValidationMessages.BringToFront();
                                }

                                
                                if (result == -1) errMsg = "File doesnot exist!!!";
                                else if (result == -2) errMsg = "File is not a well-formed XML!!!";
                                else if (result == -3) errMsg = "XSD file is not valid!!!";
                                else if (result == -4) errMsg = "File is not a valid XML, as per the schema!!!";
                                else if (result == -5) errMsg = "MODBUS Slave Port is Not Valid as per the schema";
                                lvi = new ListViewItem("");
                                lvValidationMessages.Items.Add(lvi);
                                lvi = new ListViewItem(errMsg);
                                lvValidationMessages.Items.Add(lvi);
                                lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
                                if (result < 0)
                                {
                                    hideLoading();
                                    MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                xmlFile = ofdXMLFile.FileName; //Assign only after loading file...
                                                               //Namrata: 19/12/2017
                                                               //Ajay: 29/11/2018
                                TreeNode searchNode = tvItems.Nodes[0].Nodes["MasterConfiguration"];
                                if (searchNode != null)
                                    searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
                                tvItems.SelectedNode = tvItems.Nodes[0];
                                tvItems.Nodes[0].EnsureVisible();
                                hideLoading();
                            }
                            else
                            {
                                string[] subdirectoryEntries = System.IO.Directory.GetDirectories(GetProtocol);
                                foreach (string subdirectory in subdirectoryEntries)
                                {
                                    //Namrata: 01/11/2019
                                    if (subdirectory.Contains("protocol"))
                                    {
                                        int index = subdirectory.IndexOf("protocol");
                                        GetProtocolFolder = subdirectory.Substring(subdirectory.IndexOf("protocol") + 9);
                                    }

                                    GDSlave.SubDirName = extractPath + "\\" + GetProtocolFolder;
                                    string CopyDir = extractPath + "\\" + GetProtocolFolder;

                                    if (System.IO.Directory.Exists(GDSlave.SubDirName))
                                    {
                                        System.IO.Directory.Delete(GDSlave.SubDirName, true);
                                        var diSource = new DirectoryInfo(GetProtocol + "\\" + GetProtocolFolder);
                                        var diTarget = new DirectoryInfo(CopyDir);
                                        CopyAll(diSource, diTarget);
                                    }
                                    else
                                    {
                                        var diSource1 = new DirectoryInfo(GetProtocol + "\\" + GetProtocolFolder);
                                        var diTarget1 = new DirectoryInfo(CopyDir);
                                        CopyAll(diSource1, diTarget1);
                                    }
                                }

                                if (System.IO.Directory.Exists(GetXMLPath))
                                {
                                    //Namrata: 27/11/2019
                                    DeleteDirectory(GetXMLPath);
                                    //System.IO.Directory.Delete(GetXMLPath, true);
                                }
                                //#endregion Namrata
                                //Ajay: 11/01/2019
                                if (!CheckVersions(ofdXMLFile.FileName))
                                {
                                    VersionMatch = false;
                                    DialogResult rslt = MessageBox.Show("Do you still want to continue with mismatched versions of OpenPro+Configurator, xml and xsd files?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                                    if (rslt == DialogResult.No)
                                    {
                                        return;
                                    }
                                    else { }
                                }
                                else
                                {
                                    VersionMatch = true; //Ajay: 11/01/2019
                                }
                                //Namrata:26/04/2018
                                Utils.XMLFolderPath = Path.GetDirectoryName(ofdXMLFile.FileName);

                                #region IEC61850
                                //Namrata: 27/04/2018
                                ICDFilesData.DirectoryName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
                                ICDFilesData.XmlName = Path.GetFileName(ofdXMLFile.FileName); // Get XML Name
                                ICDFilesData.XmlPath = ofdXMLFile.FileName; // XML with full path
                                ICDFilesData.XmlWithoutExt = "";// Path.GetFileNameWithoutExtension(Utils.XMLFileFullPath);
                                                                //Namrata: 29/03/2018
                                #region ClearDatasets
                                Utils.dsIED.Clear();
                                Utils.dsIED.Tables.Clear();
                                Utils.DsRCB.Clear();
                                Utils.DsRCBData.Clear();
                                Utils.dsResponseType.Clear();
                                Utils.DsResponseType.Clear();
                                Utils.dsIED.Clear();
                                Utils.dsIEDName.Clear();
                                Utils.DsAllConfigurationData.Clear();
                                Utils.DsAllConfigureData.Clear();
                                Utils.DsRCBDataset.Clear();
                                Utils.DsRCBDS.Clear();
                                Utils.DtRCBdata.Clear();
                                #endregion ClearDatasets
                                #endregion IEC61850

                                #region GDisplay
                                //Namrata: 27/04/2018
                                GDSlave.DirName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
                                GDSlave.SLDName = Path.GetFileName(ofdXMLFile.FileName); //Get XML Name
                                GDSlave.SLDPath = ofdXMLFile.FileName;//XML with full path
                                GDSlave.SLDWithoutExt = "";
                                #region ClearDatasets
                                GDSlave.DsExcelData.Tables.Clear();
                                GDSlave.DsExcelData.Clear();
                                GDSlave.DtExcelData.Clear();
                                GDSlave.DsExportData.Tables.Clear();
                                GDSlave.DTAIImage.Clear();
                                GDSlave.DTDIDOImage.Clear();
                                #endregion ClearDatasets
                                #endregion GDisplay
                                this.MyMruList.AddFile(openedFile); //Now give it to the MRUManager
                                int result = 0;
                                string errMsg = "XML file is valid...";
                                ResetConfiguratorState(false);
                                showLoading();
                                xmlFile = "";//reset old filename...
                                pnlValidationMessages.Visible = false;
                                ListViewItem lvi;
                                lvi = new ListViewItem("Validating file: " + ofdXMLFile.FileName);
                                //Namrata: 27/7/2017
                                toolStripStatusLabel1.Visible = true;  //Display File Name in Toolstrip 
                                tspFileName.Text = openedFile + @"\" + ICDFilesData.XmlName;//ofdXMLFile.FileName;
                                lvValidationMessages.Items.Add(lvi);
                                lvi = new ListViewItem("");
                                lvValidationMessages.Items.Add(lvi);
                                result = opcHandle.loadXML(ofdXMLFile.FileName, tvItems, out IsXmlValid);
                                if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
                                else
                                {
                                    pnlValidationMessages.Visible = true;
                                    pnlValidationMessages.BringToFront();
                                }
                                if (result == -1) errMsg = "File doesnot exist!!!";
                                else if (result == -2) errMsg = "File is not a well-formed XML!!!";
                                else if (result == -3) errMsg = "XSD file is not valid!!!";
                                else if (result == -4) errMsg = "File is not a valid XML, as per the schema!!!";
                                else if (result == -5) errMsg = "MODBUS Slave Port is Not Valid as per the schema";
                                lvi = new ListViewItem("");
                                lvValidationMessages.Items.Add(lvi);
                                lvi = new ListViewItem(errMsg);
                                lvValidationMessages.Items.Add(lvi);
                                lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
                                if (result < 0)
                                {
                                    hideLoading();
                                    MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                xmlFile = ofdXMLFile.FileName; //Assign only after loading file...
                                                               //Namrata: 19/12/2017
                                                               //Ajay: 29/11/2018
                                TreeNode searchNode = tvItems.Nodes[0].Nodes["MasterConfiguration"];
                                if (searchNode != null)
                                    searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
                                tvItems.SelectedNode = tvItems.Nodes[0];
                                tvItems.Nodes[0].EnsureVisible();
                                hideLoading();
                            }
                        }

                        #endregion
                        else
                        {
                            //#endregion Namrata
                            //Ajay: 11/01/2019
                            if (!CheckVersions(ofdXMLFile.FileName))
                            {
                                VersionMatch = false;
                                DialogResult rslt = MessageBox.Show("Do you still want to continue with mismatched versions of OpenPro+Configurator, xml and xsd files?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                                if (rslt == DialogResult.No)
                                {
                                    return;
                                }
                                else { }
                            }
                            else
                            {
                                VersionMatch = true; //Ajay: 11/01/2019
                            }
                            //Namrata:26/04/2018
                            Utils.XMLFolderPath = Path.GetDirectoryName(ofdXMLFile.FileName);

                            #region IEC61850
                            //Namrata: 27/04/2018
                            ICDFilesData.DirectoryName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
                            ICDFilesData.XmlName = Path.GetFileName(ofdXMLFile.FileName); // Get XML Name
                            ICDFilesData.XmlPath = ofdXMLFile.FileName; // XML with full path
                            ICDFilesData.XmlWithoutExt = "";// Path.GetFileNameWithoutExtension(Utils.XMLFileFullPath);
                                                            //Namrata: 29/03/2018
                            #region ClearDatasets
                            Utils.dsIED.Clear();
                            Utils.dsIED.Tables.Clear();
                            Utils.DsRCB.Clear();
                            Utils.DsRCBData.Clear();
                            Utils.dsResponseType.Clear();
                            Utils.DsResponseType.Clear();
                            Utils.dsIED.Clear();
                            Utils.dsIEDName.Clear();
                            Utils.DsAllConfigurationData.Clear();
                            Utils.DsAllConfigureData.Clear();
                            Utils.DsRCBDataset.Clear();
                            Utils.DsRCBDS.Clear();
                            Utils.DtRCBdata.Clear();
                            #endregion ClearDatasets
                            #endregion IEC61850

                            #region GDisplay
                            //Namrata: 27/04/2018
                            GDSlave.DirName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
                            GDSlave.SLDName = Path.GetFileName(ofdXMLFile.FileName); //Get XML Name
                            GDSlave.SLDPath = ofdXMLFile.FileName;//XML with full path
                            GDSlave.SLDWithoutExt = "";
                            #region ClearDatasets
                            GDSlave.DsExcelData.Tables.Clear();
                            GDSlave.DsExcelData.Clear();
                            GDSlave.DtExcelData.Clear();
                            GDSlave.DsExportData.Tables.Clear();
                            GDSlave.DTAIImage.Clear();
                            GDSlave.DTDIDOImage.Clear();
                            #endregion ClearDatasets
                            #endregion GDisplay
                            this.MyMruList.AddFile(openedFile); //Now give it to the MRUManager
                            int result = 0;
                            string errMsg = "XML file is valid...";
                            ResetConfiguratorState(false);
                            showLoading();
                            xmlFile = "";//reset old filename...
                            pnlValidationMessages.Visible = false;
                            ListViewItem lvi;
                            lvi = new ListViewItem("Validating file: " + ofdXMLFile.FileName);
                            //Namrata: 27/7/2017
                            //toolStripStatusLabel1.Visible = true;  //Display File Name in Toolstrip 
                            tspFileName.Text = ofdXMLFile.FileName;
                            lvValidationMessages.Items.Add(lvi);
                            lvi = new ListViewItem("");
                            lvValidationMessages.Items.Add(lvi);
                            result = opcHandle.loadXML(ofdXMLFile.FileName, tvItems, out IsXmlValid);
                            if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
                            else
                            {
                                pnlValidationMessages.Visible = true;
                                pnlValidationMessages.BringToFront();
                            }
                            if (result == -1) errMsg = "File doesnot exist!!!";
                            else if (result == -2) errMsg = "File is not a well-formed XML!!!";
                            else if (result == -3) errMsg = "XSD file is not valid!!!";
                            else if (result == -4) errMsg = "File is not a valid XML, as per the schema!!!";
                            else if (result == -5) errMsg = "MODBUS Slave Port is Not Valid as per the schema";
                            lvi = new ListViewItem("");
                            lvValidationMessages.Items.Add(lvi);
                            lvi = new ListViewItem(errMsg);
                            lvValidationMessages.Items.Add(lvi);
                            lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
                            if (result < 0)
                            {
                                hideLoading();
                                MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            xmlFile = ofdXMLFile.FileName; //Assign only after loading file...
                                                           //Namrata: 19/12/2017
                                                           //Ajay: 29/11/2018
                            TreeNode searchNode = tvItems.Nodes[0].Nodes["MasterConfiguration"];
                            if (searchNode != null)
                                searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
                            tvItems.SelectedNode = tvItems.Nodes[0];
                            tvItems.Nodes[0].EnsureVisible();
                            hideLoading();
                        }
                    }
                    else { }

                    #endregion
                }
                //Ajay: 29/11/2018
                else if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    #region AppMode=Restricted

                    string openedFile = ProtocolGateway.OppConfigurationFile;
                    Utils.XMLFolderPath = Path.GetDirectoryName(ProtocolGateway.OppConfigurationFile);
                    string DirectoryExtention = Path.GetExtension(openedFile);
                    //Namrata: 09/12/2019
                    Utils.ZipDirName = Path.GetFileName(openedFile);

                    #region Namrata
                    //Namrata: 23/10/2019
                    if (DirectoryExtention == ".zip")
                    {
                        string extractPath = System.IO.Path.GetTempPath() + "protocol";//Path.GetDirectoryName(openedFile);
                        string ZIPName = Path.GetDirectoryName(openedFile);
                        string ZipFolderName = Path.GetFileNameWithoutExtension(openedFile);
                        string GetXMLPath = Path.Combine(ZIPName, ZipFolderName);

                        Utils.DirNameSave = ZIPName + @"\" + ZipFolderName;
                        if (System.IO.Directory.Exists(Utils.DirNameSave))
                        {
                            System.IO.Directory.Delete(Utils.DirNameSave, true);
                        }
                        if (!System.IO.Directory.Exists(extractPath))
                        {
                            System.IO.Directory.CreateDirectory(extractPath);
                            ZipFile.ExtractToDirectory(openedFile, GetXMLPath);
                        }
                        else
                        {
                            System.IO.Directory.Delete(extractPath, true);
                            System.IO.Directory.CreateDirectory(extractPath);
                            ZipFile.ExtractToDirectory(openedFile, GetXMLPath);
                        }
                        string[] fileEntries1 = System.IO.Directory.GetFiles(GetXMLPath, "*.xml");

                        string fileName1 = string.Empty;
                        string destFile1 = string.Empty;
                        foreach (string s in fileEntries1)
                        {
                            fileName1 = System.IO.Path.GetFileName(s);
                            destFile1 = System.IO.Path.Combine(extractPath, fileName1);
                            System.IO.File.Move(s, destFile1);
                            ofdXMLFile.FileName = destFile1;
                        }
                        string Protocol = "protocol";
                        string GetProtocol = Path.Combine(GetXMLPath, Protocol);
                        if (!System.IO.Directory.Exists(GetProtocol))
                        {
                            if (!CheckVersions(GetXMLPath))
                            {
                                VersionMatch = false;
                                DialogResult rslt = MessageBox.Show("Do you still want to continue with mismatched versions of OpenPro+Configurator, xml and xsd files?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                                if (rslt == DialogResult.No)
                                {
                                    return;
                                }
                                else { }
                            }
                            else
                            {
                                VersionMatch = true; //Ajay: 11/01/2019
                            }
                            //Namrata:26/04/2018
                            Utils.XMLFolderPath = Path.GetDirectoryName(GetProtocol);

                            #region IEC61850
                            //Namrata: 27/04/2018
                            ICDFilesData.DirectoryName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
                            ICDFilesData.XmlName = Path.GetFileName(ofdXMLFile.FileName); // Get XML Name
                            ICDFilesData.XmlPath = ofdXMLFile.FileName; // XML with full path
                            ICDFilesData.XmlWithoutExt = "";

                            #region ClearDatasets
                            Utils.dsIED.Clear();
                            Utils.dsIED.Tables.Clear();
                            Utils.DsRCB.Clear();
                            Utils.DsRCBData.Clear();
                            Utils.dsResponseType.Clear();
                            Utils.DsResponseType.Clear();
                            Utils.dsIED.Clear();
                            Utils.dsIEDName.Clear();
                            Utils.DsAllConfigurationData.Clear();
                            Utils.DsAllConfigureData.Clear();
                            Utils.DsRCBDataset.Clear();
                            Utils.DsRCBDS.Clear();
                            Utils.DtRCBdata.Clear();
                            #endregion ClearDatasets
                            #endregion IEC61850

                            #region GDisplay
                            //Namrata: 27/04/2018
                            GDSlave.DirName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
                            GDSlave.SLDName = Path.GetFileName(ofdXMLFile.FileName); //Get XML Name
                            GDSlave.SLDPath = ofdXMLFile.FileName;//XML with full path
                            GDSlave.SLDWithoutExt = "";
                            #region ClearDatasets
                            GDSlave.DsExcelData.Tables.Clear();
                            GDSlave.DsExcelData.Clear();
                            GDSlave.DtExcelData.Clear();
                            GDSlave.DsExportData.Tables.Clear();
                            GDSlave.DTAIImage.Clear();
                            GDSlave.DTDIDOImage.Clear();
                            #endregion ClearDatasets
                            #endregion GDisplay
                            this.MyMruList.AddFile(ofdXMLFile.FileName); //Now give it to the MRUManager
                            int result = 0;
                            string errMsg = "XML file is valid...";
                            ResetConfiguratorState(false);
                            showLoading();
                            xmlFile = "";//reset old filename...
                            //pnlValidationMessages.Visible = false;
                            ListViewItem lvi;
                            lvi = new ListViewItem("Validating file: " + ofdXMLFile.FileName);
                            //Namrata: 27/7/2017
                            toolStripStatusLabel1.Visible = false;  //Display File Name in Toolstrip 
                            tspFileName.Text = openedFile;
                            lvValidationMessages.Items.Add(lvi);
                            lvi = new ListViewItem("");
                            lvValidationMessages.Items.Add(lvi);
                            result = opcHandle.loadXML(ofdXMLFile.FileName, tvItems, out IsXmlValid);
                            if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
                            else
                            {
                                pnlValidationMessages.Visible = true;
                                pnlValidationMessages.BringToFront();
                            }
                            if (result == -1) errMsg = "File doesnot exist!!!";
                            else if (result == -2) errMsg = "File is not a well-formed XML!!!";
                            else if (result == -3) errMsg = "XSD file is not valid!!!";
                            else if (result == -4) errMsg = "File is not a valid XML, as per the schema!!!";
                            else if (result == -5) errMsg = "MODBUS Slave Port is Not Valid as per the schema";
                            lvi = new ListViewItem("");
                            lvValidationMessages.Items.Add(lvi);
                            lvi = new ListViewItem(errMsg);
                            lvValidationMessages.Items.Add(lvi);
                            lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
                            if (result < 0)
                            {
                                hideLoading();
                                MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            xmlFile = ofdXMLFile.FileName; //Assign only after loading file...
                                                           //Namrata: 19/12/2017
                                                           //Ajay: 29/11/2018
                            TreeNode searchNode = tvItems.Nodes[0].Nodes["MasterConfiguration"];
                            if (searchNode != null)
                                searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
                            tvItems.SelectedNode = tvItems.Nodes[0];
                            tvItems.Nodes[0].EnsureVisible();
                            hideLoading();
                        }
                        else
                        {
                            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(GetProtocol);
                            foreach (string subdirectory in subdirectoryEntries)
                            {
                                //Namrata: 01/11/2019
                                if (subdirectory.Contains("protocol"))
                                {
                                    int index = subdirectory.IndexOf("protocol");
                                    GetProtocolFolder = subdirectory.Substring(subdirectory.IndexOf("protocol") + 9);
                                }

                                GDSlave.SubDirName = extractPath + "\\" + GetProtocolFolder;
                                string CopyDir = extractPath + "\\" + GetProtocolFolder;

                                if (System.IO.Directory.Exists(GDSlave.SubDirName))
                                {
                                    System.IO.Directory.Delete(GDSlave.SubDirName, true);
                                    var diSource = new DirectoryInfo(GetProtocol + "\\" + GetProtocolFolder);
                                    var diTarget = new DirectoryInfo(CopyDir);
                                    CopyAll(diSource, diTarget);
                                }
                                else
                                {
                                    var diSource1 = new DirectoryInfo(GetProtocol + "\\" + GetProtocolFolder);
                                    var diTarget1 = new DirectoryInfo(CopyDir);
                                    CopyAll(diSource1, diTarget1);
                                }
                            }

                            if (System.IO.Directory.Exists(GetXMLPath))
                            {
                                System.IO.Directory.Delete(GetXMLPath, true);
                            }
                            //#endregion Namrata
                            //Ajay: 11/01/2019
                            if (!CheckVersions(ofdXMLFile.FileName))
                            {
                                VersionMatch = false;
                                DialogResult rslt = MessageBox.Show("Do you still want to continue with mismatched versions of OpenPro+Configurator, xml and xsd files?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                                if (rslt == DialogResult.No)
                                {
                                    return;
                                }
                                else { }
                            }
                            else
                            {
                                VersionMatch = true; //Ajay: 11/01/2019
                            }
                            //Namrata:26/04/2018
                            Utils.XMLFolderPath = Path.GetDirectoryName(ofdXMLFile.FileName);

                            #region IEC61850
                            //Namrata: 27/04/2018
                            ICDFilesData.DirectoryName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
                            ICDFilesData.XmlName = Path.GetFileName(ofdXMLFile.FileName); // Get XML Name
                            ICDFilesData.XmlPath = ofdXMLFile.FileName; // XML with full path
                            ICDFilesData.XmlWithoutExt = "";// Path.GetFileNameWithoutExtension(Utils.XMLFileFullPath);
                                                            //Namrata: 29/03/2018
                            #region ClearDatasets
                            Utils.dsIED.Clear();
                            Utils.dsIED.Tables.Clear();
                            Utils.DsRCB.Clear();
                            Utils.DsRCBData.Clear();
                            Utils.dsResponseType.Clear();
                            Utils.DsResponseType.Clear();
                            Utils.dsIED.Clear();
                            Utils.dsIEDName.Clear();
                            Utils.DsAllConfigurationData.Clear();
                            Utils.DsAllConfigureData.Clear();
                            Utils.DsRCBDataset.Clear();
                            Utils.DsRCBDS.Clear();
                            Utils.DtRCBdata.Clear();
                            #endregion ClearDatasets
                            #endregion IEC61850

                            #region GDisplay
                            //Namrata: 27/04/2018
                            GDSlave.DirName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
                            GDSlave.SLDName = Path.GetFileName(ofdXMLFile.FileName); //Get XML Name
                            GDSlave.SLDPath = ofdXMLFile.FileName;//XML with full path
                            GDSlave.SLDWithoutExt = "";
                            #region ClearDatasets
                            GDSlave.DsExcelData.Tables.Clear();
                            GDSlave.DsExcelData.Clear();
                            GDSlave.DtExcelData.Clear();
                            GDSlave.DsExportData.Tables.Clear();
                            GDSlave.DTAIImage.Clear();
                            GDSlave.DTDIDOImage.Clear();
                            #endregion ClearDatasets
                            #endregion GDisplay
                            //this.MyMruList.AddFile(openedFile); //Now give it to the MRUManager
                            int result = 0;
                            string errMsg = "XML file is valid...";
                            ResetConfiguratorState(false);
                            showLoading();
                            xmlFile = "";//reset old filename...
                            pnlValidationMessages.Visible = true;
                            ListViewItem lvi;
                            lvi = new ListViewItem("Validating file: " + ofdXMLFile.FileName);
                            //Namrata: 27/7/2017
                            //toolStripStatusLabel1.Visible = false;  //Display File Name in Toolstrip 
                            tspFileName.Text = openedFile + @"\" + ICDFilesData.XmlName;//ofdXMLFile.FileName;
                            lvValidationMessages.Items.Add(lvi);
                            lvi = new ListViewItem("");
                            lvValidationMessages.Items.Add(lvi);
                            result = opcHandle.loadXML(ofdXMLFile.FileName, tvItems, out IsXmlValid);
                            if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
                            else
                            {
                                pnlValidationMessages.Visible = true;
                                pnlValidationMessages.BringToFront();
                            }
                            if (result == -1) errMsg = "File doesnot exist!!!";
                            else if (result == -2) errMsg = "File is not a well-formed XML!!!";
                            else if (result == -3) errMsg = "XSD file is not valid!!!";
                            else if (result == -4) errMsg = "File is not a valid XML, as per the schema!!!";
                            else if (result == -5) errMsg = "MODBUS Slave Port is Not Valid as per the schema";
                            lvi = new ListViewItem("");
                            lvValidationMessages.Items.Add(lvi);
                            lvi = new ListViewItem(errMsg);
                            lvValidationMessages.Items.Add(lvi);
                            lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
                            if (result < 0)
                            {
                                hideLoading();
                                MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            xmlFile = ofdXMLFile.FileName; //Assign only after loading file...
                                                           //Namrata: 19/12/2017
                                                           //Ajay: 29/11/2018
                            TreeNode searchNode = tvItems.Nodes[0].Nodes["MasterConfiguration"];
                            if (searchNode != null)
                                searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
                            tvItems.SelectedNode = tvItems.Nodes[0];
                            tvItems.Nodes[0].EnsureVisible();
                            hideLoading();
                        }
                    }
                    #endregion

                    else
                    {

                        #region IEC61850
                        ICDFilesData.DirectoryName = Path.GetDirectoryName(ProtocolGateway.OppConfigurationFile); //Get DirectoryName
                        ICDFilesData.XmlName = Path.GetFileName(ProtocolGateway.OppConfigurationFile); // Get XML Name
                        ICDFilesData.XmlPath = ProtocolGateway.OppConfigurationFile; // XML with full path
                        ICDFilesData.XmlWithoutExt = "";// Path.GetFileNameWithoutExtension(Utils.XMLFileFullPath);
                        #region ClearDatasets
                        Utils.dsIED.Clear();
                        Utils.dsIED.Tables.Clear();
                        Utils.DsRCB.Clear();
                        Utils.DsRCBData.Clear();
                        Utils.dsResponseType.Clear();
                        Utils.DsResponseType.Clear();
                        Utils.dsIED.Clear();
                        Utils.dsIEDName.Clear();
                        Utils.DsAllConfigurationData.Clear();
                        Utils.DsAllConfigureData.Clear();
                        Utils.DsRCBDataset.Clear();
                        Utils.DsRCBDS.Clear();
                        Utils.DtRCBdata.Clear();
                        #endregion ClearDatasets
                        #endregion IEC61850

                        #region GDisplay
                        //Namrata: 27/04/2018
                        GDSlave.DirName = Path.GetDirectoryName(ProtocolGateway.OppConfigurationFile); //Get DirectoryName
                        GDSlave.SLDName = Path.GetFileName(ProtocolGateway.OppConfigurationFile); //Get XML Name
                        GDSlave.SLDPath = ProtocolGateway.OppConfigurationFile;//XML with full path
                        GDSlave.SLDWithoutExt = "";
                        #region ClearDatasets
                        GDSlave.DsExcelData.Tables.Clear();
                        GDSlave.DtExcelData.Clear();
                        GDSlave.DsExportData.Tables.Clear();
                        GDSlave.DTAIImage.Clear();
                        GDSlave.DTDIDOImage.Clear();
                        #endregion ClearDatasets
                        #endregion GDisplay
                        int result = 0;
                        string errMsg = "XML file is valid...";
                        ResetConfiguratorState(false);
                        showLoading();
                        xmlFile = "";//reset old filename...
                        pnlValidationMessages.Visible = true;
                        ListViewItem lvi;
                        lvi = new ListViewItem("Validating file: " + ProtocolGateway.OppConfigurationFile);
                        toolStripStatusLabel1.Visible = true;  //Display File Name in Toolstrip 
                        tspFileName.Text = ProtocolGateway.OppConfigurationFile;
                        lvValidationMessages.Items.Add(lvi);
                        lvi = new ListViewItem("");
                        lvValidationMessages.Items.Add(lvi);
                        result = opcHandle.loadXML(ProtocolGateway.OppConfigurationFile, tvItems, out IsXmlValid);
                        if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
                        else
                        {
                            pnlValidationMessages.Visible = true;
                            pnlValidationMessages.BringToFront();
                        }
                        if (result == -1) errMsg = "File does not exist!";
                        else if (result == -2) errMsg = "File is not a well-formed XML!";
                        else if (result == -3) errMsg = "XSD file is not valid!";
                        else if (result == -4) errMsg = "File is not a valid XML, as per the schema!";
                        else if (result == -5) errMsg = "MODBUS Slave Port is Not Valid as per the schema!";
                        lvi = new ListViewItem("");
                        lvValidationMessages.Items.Add(lvi);
                        lvi = new ListViewItem(errMsg);
                        lvValidationMessages.Items.Add(lvi);
                        lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
                        if (result < 0)
                        {
                            hideLoading();
                            MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        xmlFile = ProtocolGateway.OppConfigurationFile;//Assign only after loading file...
                                                                       //Ajay: 29/11/2018
                                                                       //TreeNode searchNode = tvItems.Nodes[0].Nodes[5];
                        TreeNode searchNode = tvItems.Nodes[0].Nodes["MasterConfiguration"];
                        if (searchNode != null)
                            searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
                        tvItems.SelectedNode = tvItems.Nodes[0];
                        tvItems.Nodes[0].EnsureVisible();
                        hideLoading();
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void openXMLFile1()
        {
            string strRoutineName = "frmOpenProPlus: openXMLFile";
            try
            {
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Full)
                {
                    ofdXMLFile.Filter = "XML Files| *.xml|Zip Files|*.zip;*.rar"; //"XML Files|*.xml";
                    ofdXMLFile.FilterIndex = 1;
                    ofdXMLFile.RestoreDirectory = true;
                    ofdXMLFile.Title = "Browse ZIP File";//"Browse XML File";
                    if (ofdXMLFile.ShowDialog() == DialogResult.OK)
                    {
                        string openedFile = ofdXMLFile.FileName; //Namrata: 18/11/2017
                        string DirectoryExtention = Path.GetExtension(ofdXMLFile.FileName);

                        // #region Namrata
                        //Namrata: 23/10/2019
                        if (DirectoryExtention == ".zip")
                        {
                            string extractPath = System.IO.Path.GetTempPath() + "protocol";//Path.GetDirectoryName(openedFile);
                            string ZIPName = Path.GetDirectoryName(openedFile);
                            string ZipFolderName = Path.GetFileNameWithoutExtension(openedFile);
                            string GetXMLPath = Path.Combine(ZIPName, ZipFolderName);
                            if (!System.IO.Directory.Exists(extractPath))
                            {
                                System.IO.Directory.CreateDirectory(extractPath);
                                ZipFile.ExtractToDirectory(openedFile, GetXMLPath);
                            }
                            else
                            {
                                System.IO.Directory.Delete(extractPath, true);
                                System.IO.Directory.CreateDirectory(extractPath);
                                ZipFile.ExtractToDirectory(openedFile, GetXMLPath);
                            }
                            string[] fileEntries1 = System.IO.Directory.GetFiles(GetXMLPath, "*.xml");

                            string fileName1 = string.Empty;
                            string destFile1 = string.Empty;
                            foreach (string s in fileEntries1)
                            {
                                fileName1 = System.IO.Path.GetFileName(s);
                                destFile1 = System.IO.Path.Combine(extractPath, fileName1);
                                System.IO.File.Move(s, destFile1);
                                ofdXMLFile.FileName = destFile1;
                            }
                            string Protocol = "protocol";
                            string GetProtocol = Path.Combine(GetXMLPath, Protocol);
                            if (!System.IO.Directory.Exists(GetProtocol))
                            {
                                if (!CheckVersions(ofdXMLFile.FileName))
                                {
                                    VersionMatch = false;
                                    DialogResult rslt = MessageBox.Show("Do you still want to continue with mismatched versions of OpenPro+Configurator, xml and xsd files?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                                    if (rslt == DialogResult.No)
                                    {
                                        return;
                                    }
                                    else { }
                                }
                                else
                                {
                                    VersionMatch = true; //Ajay: 11/01/2019
                                }
                                //Namrata:26/04/2018
                                Utils.XMLFolderPath = Path.GetDirectoryName(ofdXMLFile.FileName);

                                #region IEC61850
                                //Namrata: 27/04/2018
                                ICDFilesData.DirectoryName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
                                ICDFilesData.XmlName = Path.GetFileName(ofdXMLFile.FileName); // Get XML Name
                                ICDFilesData.XmlPath = ofdXMLFile.FileName; // XML with full path
                                ICDFilesData.XmlWithoutExt = "";

                                #region ClearDatasets
                                Utils.dsIED.Clear();
                                Utils.dsIED.Tables.Clear();
                                Utils.DsRCB.Clear();
                                Utils.DsRCBData.Clear();
                                Utils.dsResponseType.Clear();
                                Utils.DsResponseType.Clear();
                                Utils.dsIED.Clear();
                                Utils.dsIEDName.Clear();
                                Utils.DsAllConfigurationData.Clear();
                                Utils.DsAllConfigureData.Clear();
                                Utils.DsRCBDataset.Clear();
                                Utils.DsRCBDS.Clear();
                                Utils.DtRCBdata.Clear();
                                #endregion ClearDatasets
                                #endregion IEC61850

                                #region GDisplay
                                //Namrata: 27/04/2018
                                GDSlave.DirName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
                                GDSlave.SLDName = Path.GetFileName(ofdXMLFile.FileName); //Get XML Name
                                GDSlave.SLDPath = ofdXMLFile.FileName;//XML with full path
                                GDSlave.SLDWithoutExt = "";
                                #region ClearDatasets
                                GDSlave.DsExcelData.Tables.Clear();
                                GDSlave.DsExcelData.Clear();
                                GDSlave.DtExcelData.Clear();
                                GDSlave.DsExportData.Tables.Clear();
                                GDSlave.DTAIImage.Clear();
                                GDSlave.DTDIDOImage.Clear();
                                #endregion ClearDatasets
                                #endregion GDisplay
                                this.MyMruList.AddFile(openedFile); //Now give it to the MRUManager
                                int result = 0;
                                string errMsg = "XML file is valid...";
                                ResetConfiguratorState(false);
                                showLoading();
                                xmlFile = "";//reset old filename...
                                pnlValidationMessages.Visible = true;
                                ListViewItem lvi;
                                lvi = new ListViewItem("Validating file: " + ofdXMLFile.FileName);
                                //Namrata: 27/7/2017
                                toolStripStatusLabel1.Visible = true;  //Display File Name in Toolstrip 
                                tspFileName.Text = ofdXMLFile.FileName;
                                lvValidationMessages.Items.Add(lvi);
                                lvi = new ListViewItem("");
                                lvValidationMessages.Items.Add(lvi);
                                result = opcHandle.loadXML(ofdXMLFile.FileName, tvItems, out IsXmlValid);
                                if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
                                else
                                {
                                    pnlValidationMessages.Visible = true;
                                    pnlValidationMessages.BringToFront();
                                }
                                if (result == -1) errMsg = "File doesnot exist!!!";
                                else if (result == -2) errMsg = "File is not a well-formed XML!!!";
                                else if (result == -3) errMsg = "XSD file is not valid!!!";
                                else if (result == -4) errMsg = "File is not a valid XML, as per the schema!!!";
                                else if (result == -5) errMsg = "MODBUS Slave Port is Not Valid as per the schema";
                                lvi = new ListViewItem("");
                                lvValidationMessages.Items.Add(lvi);
                                lvi = new ListViewItem(errMsg);
                                lvValidationMessages.Items.Add(lvi);
                                lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
                                if (result < 0)
                                {
                                    hideLoading();
                                    MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                xmlFile = ofdXMLFile.FileName; //Assign only after loading file...
                                                               //Namrata: 19/12/2017
                                                               //Ajay: 29/11/2018
                                TreeNode searchNode = tvItems.Nodes[0].Nodes["MasterConfiguration"];
                                if (searchNode != null)
                                    searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
                                tvItems.SelectedNode = tvItems.Nodes[0];
                                tvItems.Nodes[0].EnsureVisible();
                                hideLoading();
                            }
                            else
                            {
                                string[] subdirectoryEntries = System.IO.Directory.GetDirectories(GetProtocol);
                                foreach (string subdirectory in subdirectoryEntries)
                                {
                                    //Namrata: 01/11/2019
                                    if (subdirectory.Contains("protocol"))
                                    {
                                        int index = subdirectory.IndexOf("protocol");
                                        GetProtocolFolder = subdirectory.Substring(subdirectory.IndexOf("protocol") + 9);
                                    }

                                    GDSlave.SubDirName = extractPath + "\\" + GetProtocolFolder;
                                    string CopyDir = extractPath + "\\" + GetProtocolFolder;

                                    if (System.IO.Directory.Exists(GDSlave.SubDirName))
                                    {
                                        System.IO.Directory.Delete(GDSlave.SubDirName, true);
                                        var diSource = new DirectoryInfo(GetProtocol + "\\" + GetProtocolFolder);
                                        var diTarget = new DirectoryInfo(CopyDir);
                                        CopyAll(diSource, diTarget);
                                    }
                                    else
                                    {
                                        var diSource1 = new DirectoryInfo(GetProtocol + "\\" + GetProtocolFolder);
                                        var diTarget1 = new DirectoryInfo(CopyDir);
                                        CopyAll(diSource1, diTarget1);
                                    }
                                }

                                if (System.IO.Directory.Exists(GetXMLPath))
                                {
                                    System.IO.Directory.Delete(GetXMLPath, true);
                                }
                                //#endregion Namrata
                                //Ajay: 11/01/2019
                                if (!CheckVersions(ofdXMLFile.FileName))
                                {
                                    VersionMatch = false;
                                    DialogResult rslt = MessageBox.Show("Do you still want to continue with mismatched versions of OpenPro+Configurator, xml and xsd files?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                                    if (rslt == DialogResult.No)
                                    {
                                        return;
                                    }
                                    else { }
                                }
                                else
                                {
                                    VersionMatch = true; //Ajay: 11/01/2019
                                }
                                //Namrata:26/04/2018
                                Utils.XMLFolderPath = Path.GetDirectoryName(ofdXMLFile.FileName);

                                #region IEC61850
                                //Namrata: 27/04/2018
                                ICDFilesData.DirectoryName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
                                ICDFilesData.XmlName = Path.GetFileName(ofdXMLFile.FileName); // Get XML Name
                                ICDFilesData.XmlPath = ofdXMLFile.FileName; // XML with full path
                                ICDFilesData.XmlWithoutExt = "";// Path.GetFileNameWithoutExtension(Utils.XMLFileFullPath);
                                                                //Namrata: 29/03/2018
                                #region ClearDatasets
                                Utils.dsIED.Clear();
                                Utils.dsIED.Tables.Clear();
                                Utils.DsRCB.Clear();
                                Utils.DsRCBData.Clear();
                                Utils.dsResponseType.Clear();
                                Utils.DsResponseType.Clear();
                                Utils.dsIED.Clear();
                                Utils.dsIEDName.Clear();
                                Utils.DsAllConfigurationData.Clear();
                                Utils.DsAllConfigureData.Clear();
                                Utils.DsRCBDataset.Clear();
                                Utils.DsRCBDS.Clear();
                                Utils.DtRCBdata.Clear();
                                #endregion ClearDatasets
                                #endregion IEC61850

                                #region GDisplay
                                //Namrata: 27/04/2018
                                GDSlave.DirName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
                                GDSlave.SLDName = Path.GetFileName(ofdXMLFile.FileName); //Get XML Name
                                GDSlave.SLDPath = ofdXMLFile.FileName;//XML with full path
                                GDSlave.SLDWithoutExt = "";
                                #region ClearDatasets
                                GDSlave.DsExcelData.Tables.Clear();
                                GDSlave.DsExcelData.Clear();
                                GDSlave.DtExcelData.Clear();
                                GDSlave.DsExportData.Tables.Clear();
                                GDSlave.DTAIImage.Clear();
                                GDSlave.DTDIDOImage.Clear();
                                #endregion ClearDatasets
                                #endregion GDisplay
                                this.MyMruList.AddFile(openedFile); //Now give it to the MRUManager
                                int result = 0;
                                string errMsg = "XML file is valid...";
                                ResetConfiguratorState(false);
                                showLoading();
                                xmlFile = "";//reset old filename...
                                pnlValidationMessages.Visible = true;
                                ListViewItem lvi;
                                lvi = new ListViewItem("Validating file: " + ofdXMLFile.FileName);
                                //Namrata: 27/7/2017
                                toolStripStatusLabel1.Visible = true;  //Display File Name in Toolstrip 
                                tspFileName.Text = ofdXMLFile.FileName;
                                lvValidationMessages.Items.Add(lvi);
                                lvi = new ListViewItem("");
                                lvValidationMessages.Items.Add(lvi);
                                result = opcHandle.loadXML(ofdXMLFile.FileName, tvItems, out IsXmlValid);
                                if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
                                else
                                {
                                    pnlValidationMessages.Visible = true;
                                    pnlValidationMessages.BringToFront();
                                }
                                if (result == -1) errMsg = "File doesnot exist!!!";
                                else if (result == -2) errMsg = "File is not a well-formed XML!!!";
                                else if (result == -3) errMsg = "XSD file is not valid!!!";
                                else if (result == -4) errMsg = "File is not a valid XML, as per the schema!!!";
                                else if (result == -5) errMsg = "MODBUS Slave Port is Not Valid as per the schema";
                                lvi = new ListViewItem("");
                                lvValidationMessages.Items.Add(lvi);
                                lvi = new ListViewItem(errMsg);
                                lvValidationMessages.Items.Add(lvi);
                                lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
                                if (result < 0)
                                {
                                    hideLoading();
                                    MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                xmlFile = ofdXMLFile.FileName; //Assign only after loading file...
                                                               //Namrata: 19/12/2017
                                                               //Ajay: 29/11/2018
                                TreeNode searchNode = tvItems.Nodes[0].Nodes["MasterConfiguration"];
                                if (searchNode != null)
                                    searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
                                tvItems.SelectedNode = tvItems.Nodes[0];
                                tvItems.Nodes[0].EnsureVisible();
                                hideLoading();
                            }
                        }
                        else
                        {
                            //#endregion Namrata
                            //Ajay: 11/01/2019
                            if (!CheckVersions(ofdXMLFile.FileName))
                            {
                                VersionMatch = false;
                                DialogResult rslt = MessageBox.Show("Do you still want to continue with mismatched versions of OpenPro+Configurator, xml and xsd files?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                                if (rslt == DialogResult.No)
                                {
                                    return;
                                }
                                else { }
                            }
                            else
                            {
                                VersionMatch = true; //Ajay: 11/01/2019
                            }
                            //Namrata:26/04/2018
                            Utils.XMLFolderPath = Path.GetDirectoryName(ofdXMLFile.FileName);

                            #region IEC61850
                            //Namrata: 27/04/2018
                            ICDFilesData.DirectoryName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
                            ICDFilesData.XmlName = Path.GetFileName(ofdXMLFile.FileName); // Get XML Name
                            ICDFilesData.XmlPath = ofdXMLFile.FileName; // XML with full path
                            ICDFilesData.XmlWithoutExt = "";// Path.GetFileNameWithoutExtension(Utils.XMLFileFullPath);
                                                            //Namrata: 29/03/2018
                            #region ClearDatasets
                            Utils.dsIED.Clear();
                            Utils.dsIED.Tables.Clear();
                            Utils.DsRCB.Clear();
                            Utils.DsRCBData.Clear();
                            Utils.dsResponseType.Clear();
                            Utils.DsResponseType.Clear();
                            Utils.dsIED.Clear();
                            Utils.dsIEDName.Clear();
                            Utils.DsAllConfigurationData.Clear();
                            Utils.DsAllConfigureData.Clear();
                            Utils.DsRCBDataset.Clear();
                            Utils.DsRCBDS.Clear();
                            Utils.DtRCBdata.Clear();
                            #endregion ClearDatasets
                            #endregion IEC61850

                            #region GDisplay
                            //Namrata: 27/04/2018
                            GDSlave.DirName = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
                            GDSlave.SLDName = Path.GetFileName(ofdXMLFile.FileName); //Get XML Name
                            GDSlave.SLDPath = ofdXMLFile.FileName;//XML with full path
                            GDSlave.SLDWithoutExt = "";
                            #region ClearDatasets
                            GDSlave.DsExcelData.Tables.Clear();
                            GDSlave.DsExcelData.Clear();
                            GDSlave.DtExcelData.Clear();
                            GDSlave.DsExportData.Tables.Clear();
                            GDSlave.DTAIImage.Clear();
                            GDSlave.DTDIDOImage.Clear();
                            #endregion ClearDatasets
                            #endregion GDisplay
                            this.MyMruList.AddFile(openedFile); //Now give it to the MRUManager
                            int result = 0;
                            string errMsg = "XML file is valid...";
                            ResetConfiguratorState(false);
                            showLoading();
                            xmlFile = "";//reset old filename...
                            pnlValidationMessages.Visible = true;
                            ListViewItem lvi;
                            lvi = new ListViewItem("Validating file: " + ofdXMLFile.FileName);
                            //Namrata: 27/7/2017
                            toolStripStatusLabel1.Visible = true;  //Display File Name in Toolstrip 
                            tspFileName.Text = ofdXMLFile.FileName;
                            lvValidationMessages.Items.Add(lvi);
                            lvi = new ListViewItem("");
                            lvValidationMessages.Items.Add(lvi);
                            result = opcHandle.loadXML(ofdXMLFile.FileName, tvItems, out IsXmlValid);
                            if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
                            else
                            {
                                pnlValidationMessages.Visible = true;
                                pnlValidationMessages.BringToFront();
                            }
                            if (result == -1) errMsg = "File doesnot exist!!!";
                            else if (result == -2) errMsg = "File is not a well-formed XML!!!";
                            else if (result == -3) errMsg = "XSD file is not valid!!!";
                            else if (result == -4) errMsg = "File is not a valid XML, as per the schema!!!";
                            else if (result == -5) errMsg = "MODBUS Slave Port is Not Valid as per the schema";
                            lvi = new ListViewItem("");
                            lvValidationMessages.Items.Add(lvi);
                            lvi = new ListViewItem(errMsg);
                            lvValidationMessages.Items.Add(lvi);
                            lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
                            if (result < 0)
                            {
                                hideLoading();
                                MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            xmlFile = ofdXMLFile.FileName; //Assign only after loading file...
                                                           //Namrata: 19/12/2017
                                                           //Ajay: 29/11/2018
                            TreeNode searchNode = tvItems.Nodes[0].Nodes["MasterConfiguration"];
                            if (searchNode != null)
                                searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
                            tvItems.SelectedNode = tvItems.Nodes[0];
                            tvItems.Nodes[0].EnsureVisible();
                            hideLoading();
                        }
                    }
                    else { }
                }
                //Ajay: 29/11/2018
                else if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    string openedFile = ProtocolGateway.OppConfigurationFile;
                    Utils.XMLFolderPath = Path.GetDirectoryName(ProtocolGateway.OppConfigurationFile);

                    #region IEC61850
                    ICDFilesData.DirectoryName = Path.GetDirectoryName(ProtocolGateway.OppConfigurationFile); //Get DirectoryName
                    ICDFilesData.XmlName = Path.GetFileName(ProtocolGateway.OppConfigurationFile); // Get XML Name
                    ICDFilesData.XmlPath = ProtocolGateway.OppConfigurationFile; // XML with full path
                    ICDFilesData.XmlWithoutExt = "";// Path.GetFileNameWithoutExtension(Utils.XMLFileFullPath);
                    #region ClearDatasets
                    Utils.dsIED.Clear();
                    Utils.dsIED.Tables.Clear();
                    Utils.DsRCB.Clear();
                    Utils.DsRCBData.Clear();
                    Utils.dsResponseType.Clear();
                    Utils.DsResponseType.Clear();
                    Utils.dsIED.Clear();
                    Utils.dsIEDName.Clear();
                    Utils.DsAllConfigurationData.Clear();
                    Utils.DsAllConfigureData.Clear();
                    Utils.DsRCBDataset.Clear();
                    Utils.DsRCBDS.Clear();
                    Utils.DtRCBdata.Clear();
                    #endregion ClearDatasets
                    #endregion IEC61850

                    #region GDisplay
                    //Namrata: 27/04/2018
                    GDSlave.DirName = Path.GetDirectoryName(ProtocolGateway.OppConfigurationFile); //Get DirectoryName
                    GDSlave.SLDName = Path.GetFileName(ProtocolGateway.OppConfigurationFile); //Get XML Name
                    GDSlave.SLDPath = ProtocolGateway.OppConfigurationFile;//XML with full path
                    GDSlave.SLDWithoutExt = "";
                    #region ClearDatasets
                    GDSlave.DsExcelData.Tables.Clear();
                    GDSlave.DtExcelData.Clear();
                    GDSlave.DsExportData.Tables.Clear();
                    GDSlave.DTAIImage.Clear();
                    GDSlave.DTDIDOImage.Clear();
                    #endregion ClearDatasets
                    #endregion GDisplay
                    int result = 0;
                    string errMsg = "XML file is valid...";
                    ResetConfiguratorState(false);
                    showLoading();
                    xmlFile = "";//reset old filename...
                    pnlValidationMessages.Visible = true;
                    ListViewItem lvi;
                    lvi = new ListViewItem("Validating file: " + ProtocolGateway.OppConfigurationFile);
                    toolStripStatusLabel1.Visible = true;  //Display File Name in Toolstrip 
                    tspFileName.Text = ProtocolGateway.OppConfigurationFile;
                    lvValidationMessages.Items.Add(lvi);
                    lvi = new ListViewItem("");
                    lvValidationMessages.Items.Add(lvi);
                    result = opcHandle.loadXML(ProtocolGateway.OppConfigurationFile, tvItems, out IsXmlValid);
                    if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
                    else
                    {
                        pnlValidationMessages.Visible = true;
                        pnlValidationMessages.BringToFront();
                    }
                    if (result == -1) errMsg = "File does not exist!";
                    else if (result == -2) errMsg = "File is not a well-formed XML!";
                    else if (result == -3) errMsg = "XSD file is not valid!";
                    else if (result == -4) errMsg = "File is not a valid XML, as per the schema!";
                    else if (result == -5) errMsg = "MODBUS Slave Port is Not Valid as per the schema!";
                    lvi = new ListViewItem("");
                    lvValidationMessages.Items.Add(lvi);
                    lvi = new ListViewItem(errMsg);
                    lvValidationMessages.Items.Add(lvi);
                    lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
                    if (result < 0)
                    {
                        hideLoading();
                        MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    xmlFile = ProtocolGateway.OppConfigurationFile;//Assign only after loading file...
                    //Ajay: 29/11/2018
                    //TreeNode searchNode = tvItems.Nodes[0].Nodes[5];
                    TreeNode searchNode = tvItems.Nodes[0].Nodes["MasterConfiguration"];
                    if (searchNode != null)
                        searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
                    tvItems.SelectedNode = tvItems.Nodes[0];
                    tvItems.Nodes[0].EnsureVisible();
                    hideLoading();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void showLoading()
        {
            string strRoutineName = "frmOpenProPlus: showLoading";
            try
            {
                // Configure a BackgroundWorker to perform your long running operation.
                BackgroundWorker bgw = new BackgroundWorker();
                bgw.DoWork += new DoWorkEventHandler(bg_DoWork);
                bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bg_RunWorkerCompleted);
                Control.CheckForIllegalCrossThreadCalls = false;
                bgw.RunWorkerAsync();
                System.Threading.Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void bg_DoWork(object sender, DoWorkEventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: bg_DoWork";
            try
            {
                // Display the loading form.
                Console.WriteLine("*** showLoading form created...");
                fp.TopMost = true;
                fp.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("*** bg_RunWorkerCompleted called!!!");
        }
        private void hideLoading()
        {
            string strRoutineName = "frmOpenProPlus: hideLoading";
            try
            {
                if (fp != null)
                {
                    fp.Hide();
                }
                else
                {
                    Console.WriteLine("*** hideLoading called w/o showLoading...");
                }
                this.TopMost = true;
                this.TopMost = false;//Force it on top n then disable...
                this.TopLevel = true;
                Control.CheckForIllegalCrossThreadCalls = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void handleSaveAs()
        {
            string strRoutineName = "frmOpenProPlus: handleSaveAs";
            try
            {
                saveAsXMLFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: saveAsToolStripMenuItem_Click";
            try
            {
                ssParser.Show();
                handleSaveAs();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void handleSave()
        {
            string strRoutineName = "frmOpenproPlus: handleSave";
            try
            {
                DialogResult result = MessageBox.Show("Do you want to save changes?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    saveXMLFile();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenproPlus: saveToolStripMenuItem_Click";
            try
            {
                ssParser.Show();
                handleSave();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void tsbSave_Click(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: tsbSave_Click";
            try
            {
                handleSave();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void frmParser_FormClosing(object sender, FormClosingEventArgs e)
        {
            string strRoutineName = "frmOpenproPlus: frmParser_FormClosing";
            try
            {
                if (!showExitMessage) return;

                DialogResult result = MessageBox.Show("Do you want to save any changes?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    saveXMLFile();

                    //Namrata: 14/04/2018
                    //Delete Folder from ProgramData
                    if (Utils.DirectoryPath != "")
                    {
                        string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(ofdXMLFile.FileName) + "\\" + "IEC61850Client" + "\\" + "ProtocolConfiguration";
                        string ImportedDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(Utils.DirectoryPath);// 
                        if (System.IO.Directory.Exists(path))
                        {
                            FileDirectoryOperations fileDirectoryOperation = new FileDirectoryOperations();
                            FileDirectoryOperations.DeleteDirectory(ImportedDirectoryPath);
                        }
                    }
                    //Namrata: 18/04/2018
                    //Delete Folder from AppData
                    ICDFilesData.ICDDirMFile = System.IO.Path.GetTempPath() + "Protocol";// Application.StartupPath + @"\" + "IEC61850_Client" + @"\" + "ProtocolConfiguration";
                    if (System.IO.Directory.Exists(ICDFilesData.ICDDirMFile))
                    {
                        FileDirectoryOperations fileDirectoryOperations = new FileDirectoryOperations();
                        FileDirectoryOperations.DeleteDirectory(ICDFilesData.ICDDirMFile);
                    }
                }
                else if (result == DialogResult.No)
                {
                    //Namrata: 28/04/2018
                    Utils.AIMapReindex.Clear(); //to clear all  data on new 

                    //Namrata: 14/04/2018
                    if (Utils.DirectoryPath != "")
                    {
                        string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(ofdXMLFile.FileName) + "\\" + "IEC61850_Client" + "\\" + "ProtocolConfiguration";
                        string ImportedDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(Utils.DirectoryPath);// 
                        if (System.IO.Directory.Exists(path))
                        {
                            FileDirectoryOperations fileDirectoryOperation = new FileDirectoryOperations();
                            FileDirectoryOperations.DeleteDirectory(ImportedDirectoryPath);
                        }
                    }
                    //Namrata: 18/04/2018
                    //Delete Folder from AppData
                    ICDFilesData.ICDDirMFile = System.IO.Path.GetTempPath() + @"\" + "Protocol";
                    ///*ICDFilesData.ICDDirFile = System.IO.Path.GetTempPath() + "IEC6185*/0Client" + @"\" + "ProtocolConfiguration";// Application.StartupPath + @"\" + "IEC61850_Client" + @"\" + "ProtocolConfiguration";
                    if (System.IO.Directory.Exists(ICDFilesData.ICDDirMFile))
                    {
                        FileDirectoryOperations fileDirectoryOperations = new FileDirectoryOperations();
                        FileDirectoryOperations.DeleteDirectory(ICDFilesData.ICDDirMFile);
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenproPlus: exitToolStripMenuItem_Click";
            try
            {
                handleNew();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void HandleMapViewChange()
        {
            string strRoutineName = "frmOpenproPlus: HandleMapViewChange";
            try
            {
                //IMP: scParser and grpMapping should be added first in forms (Designer.cs). Ex.
                //Else docking won't work properly
                if (tsbMappingView.Checked)
                {
                    scParser.Dock = DockStyle.None;
                    scParser.Visible = false;
                    grpMappingView.Dock = DockStyle.Fill;
                    grpMappingView.Visible = true;
                }
                else
                {
                    grpMappingView.Dock = DockStyle.None;
                    grpMappingView.Visible = false;
                    scParser.Dock = DockStyle.Fill;
                    scParser.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void tsbMappingView_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("***** Mapping view changed...");
        }
        private void tsbMappingView_Click(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: tsbMappingView_Click";
            try
            {
                handleShowOverview();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ResetConfiguratorState(bool isNew)
        {
            string strRoutineName = "frmOpenProPlus: ResetConfiguratorState";
            try
            {
                opcHandle = null;
                tvItems.Nodes.Clear();
                opcHandle = new OpenProPlus_Config();
                opcHandle.ShowValidationMessages += this.ValidationCallBack;
                Utils.setOpenProPlusHandle(getOpenProPlusHandle());
                if (fo != null) fo.setOpenProPlusHandle(getOpenProPlusHandle());
                Globals.resetUniqueNos(ResetUniqueNos.ALL);
                //Patch for SerialConfiguration, call in context: opcHandle.loadSchema(Globals.RESOURCES_PATH + Globals.XSD_DATATYPE_FILENAME);
                opcHandle.loadDefaultItems(tvItems);
                tvItems.SelectedNode = tvItems.Nodes[0];
                //IMP: Create all default entries ONLY after resetting unique nos and creating treenodes...
                if (isNew) Utils.CreateDefaultEntries();
                int a = Utils.DummyDI.Count();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 21/11/2017
        private void EnDs_Save_SaveAs_Exit(bool IsEnabled)
        {
            string strRoutineName = "frmOpenProPlus: tsbMappingView_Click";
            try
            {
                saveToolStripMenuItem.Enabled = saveAsToolStripMenuItem.Enabled = exitToolStripMenuItem.Enabled = IsEnabled;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void handleOpen()
        {
            string strRoutineName = "frmOpenProPlus: handleOpen";
            VersionMatch = false;
            try
            {
                ssParser.Show();
                
                //Ajay: 21/11/2017 All edited
                if (opcHandle.IsFileOpen)
                {
                    string strMsg = "Do you want to save any changes ?";
                    if (!string.IsNullOrEmpty(xmlFile))
                    {
                        strMsg = strMsg.TrimEnd('?');
                        strMsg += " to \"" + xmlFile + "\" ?";
                    }
                    if (xmlFile != null)
                    {
                        DialogResult result = MessageBox.Show(strMsg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                        if (result == DialogResult.Yes)
                        {
                            saveXMLFile();
                        }
                        else
                        {
                            ofdXMLFile.FileName = "";
                            //Namrata: 28/04/2018
                            Utils.AIMapReindex.Clear(); //to clear all  data on new 
                        }
                    }
                }
                toolStripStatusLabel1.Visible = false;
                //tspFileName.Text = xmlFile;
                pnlValidationMessages.Visible = false;
                openXMLFile();
                if (VersionMatch) //Ajay: 11/01/2019
                {
                    opcHandle.IsFileOpen = true;
                    Utils.IsOpen = true;
                    EnDs_Save_SaveAs_Exit(true);
                    saveToolStripMenuItem.Enabled = true;
                    newToolStripMenuItem.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: openToolStripMenuItem_Click";
            try
            {
                ssParser.Show();
                handleOpen();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void tsbOpen_Click(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenproPlus: tsbOpen_Click";
            try
            {
                handleOpen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void handleNew()
        {
            string strRoutineName = "frmOpenproPlus: handleNew";
            try
            {
                //Ajay: 21/11/2017 All edited
                if (opcHandle.IsFileOpen)
                {
                    string strMsg = "Do you want to save any changes ?";
                    if (!string.IsNullOrEmpty(xmlFile))
                    {
                        strMsg = strMsg.TrimEnd('?');
                        strMsg += " to \"" + xmlFile + "\" ?";
                    }
                    if (xmlFile != null)
                    {
                        DialogResult result = MessageBox.Show(strMsg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                        if (result == DialogResult.Yes)
                        {
                            saveXMLFile();
                            #region IEC61850
                            //Namrata: 18/04/2018
                            //Delete Folder from AppData
                            ICDFilesData.ICDDirMFile = System.IO.Path.GetTempPath() + "Protocol";
                            if (System.IO.Directory.Exists(ICDFilesData.ICDDirMFile))
                            {
                                FileDirectoryOperations fileDirectoryOperations = new FileDirectoryOperations();
                                FileDirectoryOperations.DeleteDirectory(ICDFilesData.ICDDirMFile);
                            }
                            if (Utils.DirectoryPath != "")
                            {
                                string FileNameWithoutExtesion = Path.GetFileNameWithoutExtension(Utils.DirectoryPath);
                                string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(FileNameWithoutExtesion) + "\\" + "IEC61850_Client" + "\\" + "ProtocolConfiguration";
                                string ImportedDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(Utils.DirectoryPath);// 
                                if (System.IO.Directory.Exists(path))
                                {
                                    FileDirectoryOperations fileDirectoryOperation = new FileDirectoryOperations();
                                    FileDirectoryOperations.DeleteDirectory(ImportedDirectoryPath);
                                }
                            }
                            #endregion IEC61850

                            #region Graphical Display
                            //Namrata: 18/04/2018
                            //Delete Folder from AppData
                            GDSlave.XLSDirFile = System.IO.Path.GetTempPath() + "Protocol";
                            if (System.IO.Directory.Exists(GDSlave.XLSDirFile))
                            {
                                FileDirectoryOperations fileDirectoryOperations = new FileDirectoryOperations();
                                FileDirectoryOperations.DeleteDirectory(GDSlave.XLSDirFile);
                            }
                            if (Utils.DirectoryPath != "")
                            {
                                string FileNameWithoutExtesion = Path.GetFileNameWithoutExtension(Utils.DirectoryPath);
                                string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(FileNameWithoutExtesion) + "\\" + "GUI";
                                string ImportedDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(Utils.DirectoryPath);// 
                                if (System.IO.Directory.Exists(path))
                                {
                                    FileDirectoryOperations fileDirectoryOperation = new FileDirectoryOperations();
                                    FileDirectoryOperations.DeleteDirectory(ImportedDirectoryPath);
                                }
                            }
                            #endregion Graphical Display

                            xmlFile = null;
                        }
                        else
                        {
                            #region IEC61850
                            //Namrata: 18/04/2018
                            //Delete Folder from AppData
                            ICDFilesData.ICDDirMFile = System.IO.Path.GetTempPath() + "protocol";
                            if (System.IO.Directory.Exists(ICDFilesData.ICDDirMFile))
                            {
                                string DirectoryNameDelete = System.IO.Path.GetTempPath() + @"\" + "IEC61850Client";
                                FileDirectoryOperations fileDirectoryOperations = new FileDirectoryOperations();
                                FileDirectoryOperations.DeleteDirectory(DirectoryNameDelete);
                            }
                            if (Utils.DirectoryPath != "")
                            {
                                string FileNameWithoutExtesion = Path.GetFileNameWithoutExtension(Utils.DirectoryPath);
                                string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(FileNameWithoutExtesion) + "\\" + "IEC61850Client" + "\\" + "Protocol";
                                string ImportedDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(Utils.DirectoryPath);// 
                                if (System.IO.Directory.Exists(path))
                                {
                                    FileDirectoryOperations fileDirectoryOperation = new FileDirectoryOperations();
                                    FileDirectoryOperations.DeleteDirectory(ImportedDirectoryPath);
                                }
                            }
                            #endregion IEC61850

                            #region GDisplay
                            #region ClearDatasets
                            GDSlave.GDisplaySlave = false;
                            GDSlave.DsExcelData.Tables.Clear();
                            GDSlave.DsExcelData.Clear();
                            GDSlave.DtExcelData.Clear();
                            GDSlave.DsExportData.Tables.Clear();
                            GDSlave.DTAIImage.Clear();
                            GDSlave.DTDIDOImage.Clear();
                            #endregion ClearDatasets
                            //Namrata: 18/04/2018
                            //Delete Folder from AppData
                            GDSlave.XLSDirFile = System.IO.Path.GetTempPath() + "Protocol";
                            if (System.IO.Directory.Exists(GDSlave.XLSDirFile))
                            {
                                string DirectoryNameDelete = System.IO.Path.GetTempPath() + @"\" + "GUI";
                                FileDirectoryOperations fileDirectoryOperations = new FileDirectoryOperations();
                                FileDirectoryOperations.DeleteDirectory(DirectoryNameDelete);
                            }
                            if (Utils.DirectoryPath != "")
                            {
                                string FileNameWithoutExtesion = Path.GetFileNameWithoutExtension(Utils.DirectoryPath);
                                string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(FileNameWithoutExtesion) + "\\" + "GUI" + "\\" + "Protocol";
                                string ImportedDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(Utils.DirectoryPath);// 
                                if (System.IO.Directory.Exists(path))
                                {
                                    FileDirectoryOperations fileDirectoryOperation = new FileDirectoryOperations();
                                    FileDirectoryOperations.DeleteDirectory(ImportedDirectoryPath);
                                }
                            }
                            #endregion GDisplay
                            xmlFile = null;
                            //ResetConfiguratorState(true);
                            //opcHandle.IsFileOpen = true;
                        }
                    }
                    else
                    {
                        #region ClearDatasets
                        GDSlave.DsExcelData.Tables.Clear();
                        GDSlave.DsExcelData.Clear();
                        GDSlave.DtExcelData.Clear();
                        GDSlave.DsExportData.Tables.Clear();
                        GDSlave.DTAIImage.Clear();
                        GDSlave.DTDIDOImage.Clear();
                        #endregion ClearDatasets
                        //Namrata: 18/04/2018
                        //Delete Folder from AppData
                        ICDFilesData.ICDDirMFile = System.IO.Path.GetTempPath() + "protocol";//Namrata:11/04/2019
                        if (System.IO.Directory.Exists(ICDFilesData.ICDDirMFile))
                        {
                            FileDirectoryOperations fileDirectoryOperations = new FileDirectoryOperations();
                            FileDirectoryOperations.DeleteDirectory(ICDFilesData.ICDDirMFile);
                        }
                        if (Utils.DirectoryPath != "")
                        {
                            string FileNameWithoutExtesion = Path.GetFileNameWithoutExtension(Utils.DirectoryPath);
                            string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(FileNameWithoutExtesion) + "\\" + "IEC61850Client" + "\\" + "ProtocolConfiguration";
                            string ImportedDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(Utils.DirectoryPath);// 
                            if (System.IO.Directory.Exists(path))
                            {
                                FileDirectoryOperations fileDirectoryOperation = new FileDirectoryOperations();
                                FileDirectoryOperations.DeleteDirectory(ImportedDirectoryPath);
                            }
                        }


                        #region GDisplay
                        //Namrata: 18/04/2018
                        //Delete Folder from AppData
                        GDSlave.XLSDirFile = System.IO.Path.GetTempPath() + "Protocol";
                        if (System.IO.Directory.Exists(GDSlave.XLSDirFile))
                        {
                            FileDirectoryOperations fileDirectoryOperations = new FileDirectoryOperations();
                            FileDirectoryOperations.DeleteDirectory(GDSlave.XLSDirFile);
                        }
                        if (Utils.DirectoryPath != "")
                        {
                            string FileNameWithoutExtesion = Path.GetFileNameWithoutExtension(Utils.DirectoryPath);
                            string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(FileNameWithoutExtesion) + "\\" + "IEC61850Client" + "\\" + "ProtocolConfiguration";
                            string ImportedDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(Utils.DirectoryPath);// 
                            if (System.IO.Directory.Exists(path))
                            {
                                FileDirectoryOperations fileDirectoryOperation = new FileDirectoryOperations();
                                FileDirectoryOperations.DeleteDirectory(ImportedDirectoryPath);
                            }
                        }
                        #endregion GDisplay
                    }
                }


                ResetConfiguratorState(true);
                opcHandle.IsFileOpen = true;
                EnDs_Save_SaveAs_Exit(true);
                toolStripStatusLabel1.Visible = false;
                tspFileName.Text = "";
                #region //Ajay: 21/11/2017  Original code commented
                //DialogResult result = MessageBox.Show("Do you want to save any changes?", "New XML file...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                //if (result == DialogResult.Yes)
                //{
                //    saveXMLFile();
                //    xmlFile = null;
                //    ResetConfiguratorState(true);
                //}
                //else if (result == DialogResult.No)
                //{
                //    xmlFile = null;
                //    ResetConfiguratorState(true);
                //}
                //else//cancel
                //{
                //}
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenproPlus: newToolStripMenuItem_Click";
            try
            {
                handleNew();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void tsbNew_Click(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: tsbNew_Click";
            try
            {
                //Namrata: 28/04/2018
                Utils.AIMapReindex.Clear(); //to clear all  data on new 
                handleNew();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void grpMappingView_Resize(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: grpMappingView_Resize";
            try
            {
                lvMappingView.Width = grpMappingView.Width - 10;
                lvMappingView.Height = grpMappingView.Height - FILTER_PANEL_HEIGHT - 10;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvMappingView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: lvMappingView_DrawColumnHeader";
            try
            {
                //IMP: No 'Console.Write' statements here...
                //Console.WriteLine("*** boom boom boom boom bash");
                e.DrawDefault = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvMappingView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: lvMappingView_DrawSubItem";
            try
            {
                //IMP: No 'Console.Write' statements here...
                if (e.ColumnIndex >= COLS_B4_MULTISLAVE && e.SubItem.Text == "yes")
                {
                    e.DrawDefault = false;
                    e.DrawBackground();
                    Rectangle tPos = new Rectangle(e.SubItem.Bounds.X + ((e.SubItem.Bounds.Width - e.SubItem.Bounds.Height) / 2), e.SubItem.Bounds.Y, e.SubItem.Bounds.Height, e.SubItem.Bounds.Height);
                    e.Graphics.DrawImage((Image)Properties.Resources.ResourceManager.GetObject("greenindicator"), tPos);
                }
                else if (e.SubItem.Text == "transparentimg")
                {
                    e.DrawDefault = false;
                    e.DrawBackground();
                    Rectangle tPos = new Rectangle(e.SubItem.Bounds.X + ((e.SubItem.Bounds.Width - e.SubItem.Bounds.Height) / 2), e.SubItem.Bounds.Y, e.SubItem.Bounds.Height, e.SubItem.Bounds.Height);
                    e.Graphics.DrawImage((Image)Properties.Resources.ResourceManager.GetObject("transparent"), tPos);
                }
                else
                {
                    e.DrawDefault = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void lvMappingView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: lvMappingView_ColumnClick";
            try
            {
                if (e.Column != sortColumn)  // Determine whether the column is the same as the last column clicked.
                {
                    sortColumn = e.Column;
                    lvMappingView.Sorting = SortOrder.Ascending;
                }
                else
                {
                    if (lvMappingView.Sorting == SortOrder.Ascending) // Determine what the last sort order was and change it.
                        lvMappingView.Sorting = SortOrder.Descending;
                    else
                        lvMappingView.Sorting = SortOrder.Ascending;
                }
                lvMappingView.Sort();  // Call the sort method to manually sort.
                lvMappingView.ListViewItemSorter = new ListViewItemComparer(e.Column, lvMappingView.Sorting, lvMappingView.Columns[e.Column].Tag);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        // Implements the manual sorting of items by column.
        class ListViewItemComparer : IComparer
        {
            private int col;
            private SortOrder order;
            private int dataType = 0; //0->string, 1->int
            public ListViewItemComparer()
            {
                col = 0;
                order = SortOrder.Ascending;
            }
            public ListViewItemComparer(int column, SortOrder order, object objType)
            {
                col = column;
                this.order = order;
                if (objType != null && objType.ToString() == "int") dataType = 1;
            }
            public int Compare(object x, object y)
            {
                int returnVal = -1;
                if (dataType == 0)
                {//string
                    returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text,
                                        ((ListViewItem)y).SubItems[col].Text);
                }
                else if (dataType == 1)
                {//int
                    string ix = "0", iy = "0";
                    if (((ListViewItem)x).SubItems[col].Text == "") ix = "0";
                    else ix = ((ListViewItem)x).SubItems[col].Text;

                    if (((ListViewItem)y).SubItems[col].Text == "") iy = "0";
                    else iy = ((ListViewItem)y).SubItems[col].Text;

                    returnVal = CompareInt(ix, iy);
                }
                // Determine whether the sort order is descending.
                // Invert the value returned by String.Compare.
                if (order == SortOrder.Descending) returnVal *= -1;
                return returnVal;
            }
            private int CompareInt(string x, string y)
            {
                int ix, iy;
                try
                {
                    ix = Int32.Parse(x);
                }
                catch (System.FormatException)
                {
                    ix = 0;
                }
                try
                {
                    iy = Int32.Parse(y);
                }
                catch (System.FormatException)
                {
                    iy = 0;
                }
                return ix - iy;
            }
        }
        private void handleShowOverview()
        {
            string strRoutineName = "frmOpenProPlus: handleShowOverview";
            try
            {
                if (fo == null)
                {
                    fo = new frmOverview(getOpenProPlusHandle());
                    fo.FormClosed += fo_FormClosed;
                    fo.Show();
                }
                else
                {
                    fo.TopMost = true;
                    fo.TopMost = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void showOverviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: showOverviewToolStripMenuItem_Click";
            try
            {
                handleShowOverview();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void fo_FormClosed(object sender, FormClosedEventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: fo_FormClosed";
            try
            {
                fo = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void frmParser_Resize(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: frmParser_Resize";
            try
            {
                pnlValidationMessages.Left = (this.Width - pnlValidationMessages.Width) / 2;
                pnlValidationMessages.Top = (this.Height - (this.mnuParser.Height + this.tsParser.Height + this.ssParser.Height + pnlValidationMessages.Height));

            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: btnClose_Click";
            try
            {
                pnlValidationMessages.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void scParser_Panel2_ControlAdded(object sender, ControlEventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: scParser_Panel2_ControlAdded";
            try
            {
                e.Control.Width = scParser.Panel2.Width; //Resize the control in right-side panel, got using getView()
                e.Control.Height = scParser.Panel2.Height;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void scParser_Panel2_Resize(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: scParser_Panel2_Resize";
            try
            {
                if (scParser.Panel2.Controls.Count <= 0) return;
                Control ctrl = scParser.Panel2.Controls[0];
                if (ctrl != null)
                {
                    ctrl.Width = scParser.Panel2.Width;
                    ctrl.Height = scParser.Panel2.Height;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void tsbClose_Click(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: tsbClose_Click";
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void tsbReindexData_Click(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: tsbReindexData_Click";
            try
            {
                opcHandle.regenerateSequence();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void handleHelp()
        {
            string strRoutineName = "frmOpenProPlus: handleHelp";
            try
            {
                Help.ShowHelp(this, Globals.RESOURCES_PATH + "OpenPro+ Help.chm");
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void tsbHelp_Click(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: tsbHelp_Click";
            try
            {
                handleHelp();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void shelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: shelpToolStripMenuItem_Click";
            try
            {
                handleHelp();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void openProUIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: openProUIToolStripMenuItem_Click";
            try
            {
                frmOpenPro_UI FrmOpenPro_UIs = new frmOpenPro_UI();
                FrmOpenPro_UIs.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ssParser_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void manualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: manualToolStripMenuItem_Click";
            try
            {
                Help.ShowHelp(this, Globals.RESOURCES_PATH + "OpenProPlus Configurator - User Manual.pdf");
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void recentFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MyMruList.FileSelected += MyMruList_FileSelected;
        }
        private void tspImportICD_Click(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: tspImportICD_Click";
            try
            {
                frmOpenPro_UI FrmOpenPro_UIs = new frmOpenPro_UI();
                FrmOpenPro_UIs.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void tspManual_Click(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: tspManual_Click";
            try
            {
                Help.ShowHelp(this, Globals.RESOURCES_PATH + "OpenProPlus Configurator - User Manual.pdf");
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void toolStripButtonExpandTV_Click(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: toolStripButtonExpandTV_Click";
            try
            {
                tvItems.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 04/12/2018
        #region File Upload and Download

        //Ajay: 04/12/2018
        private void tsbtnFileDownload_Click(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: tsbtnFileDownload_Click";
            try
            {
                if (string.IsNullOrEmpty(ProtocolGateway.OppIpAddress) || string.IsNullOrEmpty(ProtocolGateway.User) || string.IsNullOrEmpty(ProtocolGateway.Password))
                {
                    if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Full)
                    {
                        frmFTP frmFtp = new frmFTP();
                        frmFtp.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Could not connect to server due to improper connection info!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                this.Cursor = Cursors.WaitCursor;
                if (ProtocolGateway.Protocol.ToUpper() == "FTP")
                {
                    if (!string.IsNullOrEmpty(ProtocolGateway.OppIpAddress) && !string.IsNullOrEmpty(ProtocolGateway.User) && !string.IsNullOrEmpty(ProtocolGateway.Password))
                    {
                        DownloadFileUsingFTP("ftp://" + ProtocolGateway.OppIpAddress, "openproplus_config.xml");
                    }
                    else
                    {
                        MessageBox.Show("Could not connect to server due to improper connection info!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (ProtocolGateway.Protocol.ToUpper() == "SFTP")
                {
                    if (!string.IsNullOrEmpty(ProtocolGateway.OppIpAddress) && !string.IsNullOrEmpty(ProtocolGateway.User) && !string.IsNullOrEmpty(ProtocolGateway.Password))
                    {
                        DownloadFileUsingSFTP("mnt/app/database", "openproplus_config.xml");
                    }
                    else
                    {
                        MessageBox.Show("Could not connect to server due to improper connection info!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                //Ajay: 08/01/2019
                else if (ProtocolGateway.Protocol.ToUpper() == "HTTP")
                {
                    if (!string.IsNullOrEmpty(ProtocolGateway.OppIpAddress) && !string.IsNullOrEmpty(ProtocolGateway.User) && !string.IsNullOrEmpty(ProtocolGateway.Password))
                    {
                        DownloadFileUsingFTP("ftp://" + ProtocolGateway.OppIpAddress, "openproplus_config.xml");
                        //DownloadFileUsingHTTP("http://" + ProtocolGateway.OppIpAddress);
                    }
                    else
                    {
                        MessageBox.Show("Could not connect to server due to improper connection info!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.Name + ": " + MethodBase.GetCurrentMethod().Name + ": " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            { this.Cursor = Cursors.Default; }
        }
        //Ajay: 04/12/2018
        private void tsbtnFileUpload_Click(object sender, EventArgs e)
        {
            try
            {
                ProtocolGateway.OppIpAddress = "";
                ProtocolGateway.User = "";
                ProtocolGateway.Password = "";
                if (string.IsNullOrEmpty(ProtocolGateway.OppIpAddress) || string.IsNullOrEmpty(ProtocolGateway.User) || string.IsNullOrEmpty(ProtocolGateway.Password))
                {
                    if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Full)
                    {
                        frmFTP frmFtp = new frmFTP();
                        frmFtp.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Could not connect to server due to improper connection info!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                this.Cursor = Cursors.WaitCursor;
                if (ProtocolGateway.Protocol.ToUpper() == "FTP")
                {
                    if (!string.IsNullOrEmpty(ProtocolGateway.OppIpAddress) && !string.IsNullOrEmpty(ProtocolGateway.User) && !string.IsNullOrEmpty(ProtocolGateway.Password))
                    {
                        UploadFileUsingFTP("ftp://" + ProtocolGateway.OppIpAddress, "openproplus_config.xml");
                    }
                    else
                    {
                        MessageBox.Show("Could not connect to server due to improper connection info!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (ProtocolGateway.Protocol.ToUpper() == "SFTP")
                {
                    if (!string.IsNullOrEmpty(ProtocolGateway.OppIpAddress) && !string.IsNullOrEmpty(ProtocolGateway.User) && !string.IsNullOrEmpty(ProtocolGateway.Password))
                    {
                        UploadFileUsingSFTP("mnt/app/database", "openproplus_config.xml");
                    }
                    else
                    {
                        MessageBox.Show("Could not connect to server due to improper connection info!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (ProtocolGateway.Protocol.ToUpper() == "HTTP")
                {
                    if (!string.IsNullOrEmpty(ProtocolGateway.OppIpAddress) && !string.IsNullOrEmpty(ProtocolGateway.User) && !string.IsNullOrEmpty(ProtocolGateway.Password))
                    {
                        UploadFileUsingHTTP("ftp://" + ProtocolGateway.OppIpAddress);
                    }
                    else
                    {
                        MessageBox.Show("Could not connect to server due to improper connection info!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.Name + ": " + MethodBase.GetCurrentMethod().Name + ": " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            { this.Cursor = Cursors.Default; }
        }

        #region FTP
        //Ajay: 04/12/2018
        private FtpWebRequest CreateFTPWebRequest(string ftpDirectoryPath, bool keepAlive = false)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(ftpDirectoryPath));
            //Set proxy to null. Under current configuration if this option is not set then the proxy that is used will get an html response from the web content gateway (firewall monitoring system)
            request.Proxy = null;
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = keepAlive;
            request.Credentials = new NetworkCredential(ProtocolGateway.User, ProtocolGateway.Password);
            return request;
        }
        //Ajay: 04/12/2018
        private void DownloadFileUsingFTP(string ftpFilePath, string ftpFileName)
        {
            try
            {
                int bytesRead = 0;
                byte[] buffer = new byte[2048];
                Stream reader = null;
                SaveFileDialog svfiledlg = new SaveFileDialog();
                string strFilePath = "";
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    if (!string.IsNullOrEmpty(xmlFile) && File.Exists(xmlFile))
                    {
                        strFilePath = xmlFile;
                        //Path.Combine(Path.GetDirectoryName(ProtocolGateway.OppConfigurationFile), ftpFileName);
                    }
                }
                else if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Full)
                {
                    svfiledlg.Title = "Save OpenProPlus Configuration File";
                    svfiledlg.InitialDirectory = Path.GetFullPath(Environment.SpecialFolder.MyDocuments.ToString());
                    svfiledlg.DefaultExt = "xml";
                    svfiledlg.FileName = ftpFileName; //"openproplus_config.xml";
                    svfiledlg.Filter = "xml files (*.xml)|*.xml";
                    svfiledlg.CheckFileExists = false;
                    svfiledlg.CheckPathExists = true;
                    svfiledlg.SupportMultiDottedExtensions = false;
                    if (svfiledlg.ShowDialog() == DialogResult.OK)
                    {
                        strFilePath = svfiledlg.FileName;
                    }
                    else { return; }
                }
                if (!string.IsNullOrEmpty(strFilePath))
                {
                    FtpWebRequest request = CreateFTPWebRequest(ftpFilePath + "//" + ftpFileName, true);
                    request.Method = WebRequestMethods.Ftp.DownloadFile;
                    try
                    {
                        reader = request.GetResponse().GetResponseStream();
                    }
                    catch (WebException ex)
                    {
                        MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //MessageBox.Show(((FtpWebResponse)ex.Response).StatusDescription, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (reader != null)
                    {
                        string destinationFileName = "";
                        frmInput frminput = new frmInput();
                        frminput.defaultFileName = GetDefaultFileName(Path.GetDirectoryName(strFilePath));
                        if (frminput.ShowDialog() == DialogResult.OK)
                        {
                            if (!string.IsNullOrEmpty(frminput.txtbxInput.Text.Trim()))
                            {
                                destinationFileName = Path.Combine(Path.GetDirectoryName(ProtocolGateway.ProtocolGatewayConfigurationFile), frminput.txtbxInput.Text.Trim());
                                frminput.Close();
                            }
                        }
                        else { return; }

                        FileStream fs = new FileStream(Path.Combine(Path.GetDirectoryName(strFilePath), destinationFileName), FileMode.Create);
                        while (true)
                        {
                            bytesRead = reader.Read(buffer, 0, buffer.Length);
                            if (bytesRead == 0)
                                break;
                            fs.Write(buffer, 0, bytesRead);
                        }
                        fs.Close();
                        MessageBox.Show("'" + Path.GetFileName(destinationFileName) + "' file downloaded successfully!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("File is empty!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("File downloading failed! " + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        //Ajay: 04/12/2018
        private void UploadFileUsingFTP(string ftpFilePath, string ftpFileName)
        {
            FtpWebRequest ftpRequest = null;
            Stream ftpStream = null;
            FileStream localFileStream = null;
            int bufferSize = 2048;
            byte[] buffer = new byte[2048];
            string strFilePath = "";
            OpenFileDialog opnfiledlg = new OpenFileDialog();
            try
            {
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    if (!string.IsNullOrEmpty(xmlFile) && File.Exists(xmlFile))
                    {
                        strFilePath = xmlFile;
                    }
                }
                else if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Full)
                {
                    opnfiledlg.Title = "Select OpenProPlus Configuration File";
                    //opnfiledlg.InitialDirectory = Path.GetDirectoryName(ProtocolGateway.OppConfigurationFile);
                    opnfiledlg.DefaultExt = "xml";
                    opnfiledlg.Filter = "xml files (*.xml)|*.xml";
                    opnfiledlg.CheckFileExists = true;
                    opnfiledlg.CheckPathExists = true;
                    opnfiledlg.SupportMultiDottedExtensions = false;
                    if (opnfiledlg.ShowDialog() == DialogResult.OK)
                    {
                        strFilePath = opnfiledlg.FileName;
                    }
                    else { return; }
                }
                DialogResult result = MessageBox.Show("Are you sure, you want to upload '" + strFilePath + "' to '" + ProtocolGateway.OppIpAddress + "'?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {


                    //ftpRequest = CreateFTPWebRequest(ftpFilePath + "//" + ftpFileName, false);
                    string sss = ftpFilePath + "/" + "uploads.php?"  + ftpFileName;
                    ftpRequest = CreateFTPWebRequest(ftpFilePath + "/"+"uploads.php" + ftpFileName, false);
                    ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                    try
                    {
                        ftpStream = ftpRequest.GetRequestStream();
                    }
                    catch (WebException e)
                    {
                        String status = ((FtpWebResponse)e.Response).StatusDescription;
                        MessageBox.Show(status, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (ftpStream != null)
                    {
                        localFileStream = new FileStream(strFilePath, FileMode.Open);
                        byte[] byteBuffer = new byte[bufferSize];
                        int bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
                        while (bytesSent != 0)
                        {
                            ftpStream.Write(byteBuffer, 0, bytesSent);
                            bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
                        }
                        localFileStream.Close();
                        MessageBox.Show("'" + strFilePath + "' Uploaded successfully!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else { MessageBox.Show("File Empty"); }
                }
                else { return; }
            }
            catch (Exception ex) { MessageBox.Show("'" + strFilePath + "' uploading failed!" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally
            {
                if (localFileStream != null) localFileStream.Close();
                if (ftpStream != null) ftpStream.Close();
                ftpRequest = null;
            }
        }

        #endregion

        #region SFTP
        //Ajay: 14/12/2018
        private void DownloadFileUsingSFTP(string sftpFileDirectory, string sftpFileName)
        {
            try
            {
                SaveFileDialog svfiledlg = new SaveFileDialog();
                string strFilePath = "";
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    strFilePath = Path.GetDirectoryName(ProtocolGateway.OppConfigurationFile);
                }
                else if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Full)
                {
                    svfiledlg.Title = "Save OpenProPlus Configuration File";
                    svfiledlg.InitialDirectory = Path.GetFullPath(Environment.SpecialFolder.MyDocuments.ToString());
                    svfiledlg.DefaultExt = "xml";
                    svfiledlg.FileName = "openproplus_config.xml";
                    svfiledlg.Filter = "xml files (*.xml)|*.xml";
                    svfiledlg.CheckFileExists = false;
                    svfiledlg.CheckPathExists = true;
                    svfiledlg.SupportMultiDottedExtensions = false;
                    if (svfiledlg.ShowDialog() == DialogResult.OK)
                    {
                        strFilePath = svfiledlg.FileName;
                    }
                    else { return; }
                }
                if (!string.IsNullOrEmpty(strFilePath))
                {
                    try
                    {
                        using (SftpClient sftpClient = new SftpClient(ProtocolGateway.OppIpAddress, 22, ProtocolGateway.User, ProtocolGateway.Password))
                        {
                            sftpClient.KeepAliveInterval = new TimeSpan(0, 1, 0);
                            try
                            {
                                sftpClient.Connect();
                            }
                            catch (Renci.SshNet.Common.SshConnectionException ex)
                            {
                                MessageBox.Show("Can not establish the connection with " + ProtocolGateway.OppIpAddress + Environment.NewLine + ex.Message + Environment.NewLine + ex.DisconnectReason, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            sftpClient.ChangeDirectory("/");
                            SftpFile sftpFile = sftpClient.ListDirectory(sftpFileDirectory).ToList().Where(x => x.Name == sftpFileName).SingleOrDefault();
                            using (Stream stream = File.OpenWrite(strFilePath))
                            {
                                if (sftpClient.IsConnected)
                                {
                                    try
                                    {
                                        sftpClient.DownloadFile(sftpFile.FullName, stream);
                                    }
                                    catch (Renci.SshNet.Common.SftpPathNotFoundException ex)
                                    {
                                        MessageBox.Show("File downloading failed!" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                    catch (Renci.SshNet.Common.SftpPermissionDeniedException ex)
                                    {
                                        MessageBox.Show("File downloading failed!" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                    catch (Renci.SshNet.Common.SshConnectionException ex)
                                    {
                                        MessageBox.Show("File downloading failed!" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                    catch (Renci.SshNet.Common.SshException ex)
                                    {
                                        MessageBox.Show("File downloading failed!" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                    MessageBox.Show("File downloaded successfully to '" + strFilePath + "'", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                    catch (Renci.SshNet.Common.SshException ex)
                    {
                        MessageBox.Show("File downloading failed!" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("File downloading failed! " + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        //Ajay: 14/12/2018
        private void UploadFileUsingSFTP(string sftpFileDirectory, string sftpFileName)
        {
            try
            {
                SftpClient sftpClient = null;
                OpenFileDialog opnfiledlg = new OpenFileDialog();
                string strFilePath = "";
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    strFilePath = Path.GetDirectoryName(ProtocolGateway.OppConfigurationFile);
                }
                else if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Full)
                {
                    opnfiledlg.Title = "Select OpenProPlus Configuration File";
                    opnfiledlg.InitialDirectory = Path.GetFullPath(Environment.SpecialFolder.MyDocuments.ToString());
                    opnfiledlg.DefaultExt = "xml";
                    opnfiledlg.Filter = "xml files (*.xml)|*.xml";
                    opnfiledlg.CheckFileExists = true;
                    opnfiledlg.CheckPathExists = true;
                    opnfiledlg.SupportMultiDottedExtensions = false;
                    if (opnfiledlg.ShowDialog() == DialogResult.OK)
                    {
                        strFilePath = opnfiledlg.FileName;
                    }
                    else { return; }
                }
                if (!string.IsNullOrEmpty(strFilePath))
                {
                    DialogResult result = MessageBox.Show("Are you sure, you want to upload '" + strFilePath + "' to '" + ProtocolGateway.OppIpAddress + "'?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            using (sftpClient = new SftpClient(ProtocolGateway.OppIpAddress, 22, ProtocolGateway.User, ProtocolGateway.Password))
                            {
                                sftpClient.KeepAliveInterval = new TimeSpan(0, 1, 0);
                                try
                                {
                                    sftpClient.Connect();
                                }
                                catch (Renci.SshNet.Common.SshConnectionException ex)
                                {
                                    MessageBox.Show("Can not establish the connection with " + ProtocolGateway.OppIpAddress + Environment.NewLine + ex.Message + Environment.NewLine + ex.DisconnectReason, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                sftpClient.ChangeDirectory("/");
                                SftpFile sftpFile = sftpClient.ListDirectory(sftpFileDirectory).ToList().Where(x => x.Name == sftpFileName).SingleOrDefault();
                                using (Stream stream = File.OpenRead(strFilePath))
                                {
                                    if (sftpClient.IsConnected)
                                    {
                                        try
                                        {
                                            if (sftpFile != null)
                                            {
                                                sftpClient.UploadFile(stream, sftpFile.FullName);
                                            }
                                            else
                                            {
                                                MessageBox.Show("Invalid file!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                        catch (Renci.SshNet.Common.SftpPathNotFoundException ex)
                                        {
                                            MessageBox.Show("File uploading failed!" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        }
                                        catch (Renci.SshNet.Common.SftpPermissionDeniedException ex)
                                        {
                                            MessageBox.Show("File uploading failed!" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        }
                                        catch (Renci.SshNet.Common.SshConnectionException ex)
                                        {
                                            MessageBox.Show("File uploading failed!" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        }
                                        catch (Renci.SshNet.Common.SshException ex)
                                        {
                                            MessageBox.Show("File uploading failed!" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        }
                                        MessageBox.Show("File uploaded successfully to '" + strFilePath + "'", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }
                        }
                        catch (Renci.SshNet.Common.SshException ex)
                        {
                            MessageBox.Show("File uploading failed!" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("File uploading failed! " + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        #endregion

        #region HTTP
        //Ajay: 08/01/2019
        private void UploadFileUsingHTTP(string OppWebAddr)
        {
            string strFilePath = "";
            OpenFileDialog opnfiledlg = new OpenFileDialog();
            WebClient wbclient = new WebClient();
            WebClient client = new WebClient();
            try
            {
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    if (!string.IsNullOrEmpty(xmlFile) && File.Exists(xmlFile))
                    {
                        strFilePath = xmlFile;
                    }
                    else
                    {
                        MessageBox.Show("File is empty!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        return;
                    }
                }
                else if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Full)
                {
                    opnfiledlg.Title = "Select OpenProPlus Configuration File";
                    //opnfiledlg.InitialDirectory = Path.GetDirectoryName(ProtocolGateway.OppConfigurationFile);
                    opnfiledlg.DefaultExt = "xml";
                    opnfiledlg.Filter = "xml files (*.xml)|*.xml";
                    opnfiledlg.CheckFileExists = true;
                    opnfiledlg.CheckPathExists = true;
                    opnfiledlg.SupportMultiDottedExtensions = false;
                    if (opnfiledlg.ShowDialog() == DialogResult.OK)
                    {
                        strFilePath = opnfiledlg.FileName;
                    }
                    else { return; }
                }
                //strFilePath = @"C:\Users\ajayp\Downloads\openproplus_config.xml";
                if (!string.IsNullOrEmpty(strFilePath))
                {
                    DialogResult result = MessageBox.Show("Are you sure, you want to upload '" + Path.GetFileName(strFilePath) + "' to '" + ProtocolGateway.OppIpAddress + "'?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (result == DialogResult.Yes)
                    {
                        wbclient.Credentials = new NetworkCredential(ProtocolGateway.User, ProtocolGateway.Password);
                       // wbclient.UploadFile(OppWebAddr + "/rtv2opp.xml", strFilePath);
                        //Upload file
                        client = new WebClient();
                        NameValueCollection login = new NameValueCollection();
                        login.Add("username", "admin");
                        login.Add("password", "admin");
                        login.Add("mode", "upload");
                        try
                        {
                            string filename = Path.GetFileName(strFilePath);
                           string aa = "http://" + ProtocolGateway.OppIpAddress + "/uploads.php?"  + filename;
                            //string strResponse = Encoding.UTF8.GetString(client.UploadValues("http://" + ProtocolGateway.OppIpAddress + "/uploads.php?name="+filename, login));
                            string strResponse = Encoding.UTF8.GetString(client.UploadValues("http://" + ProtocolGateway.OppIpAddress + "//rtv2upload.php", login));
                            if (!string.IsNullOrEmpty(strResponse))
                            {
                                frmHTMLWebResponse frmRes = new frmHTMLWebResponse();
                                frmRes.Text = "File upload status";
                                frmRes.wbBrowser.DocumentText = "'" + Path.GetFileName(strFilePath) + "' " + strResponse;
                                frmRes.ShowDialog();
                            }
                            //if (strResponse.Contains("successfully"))
                            //{
                            //    MessageBox.Show("'" + Path.GetFileName(strFilePath) + "' " + strResponse, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            //}
                            //else if (strResponse.Contains("failed") || strResponse.Contains("error"))
                            //{
                            //    MessageBox.Show("'" + Path.GetFileName(strFilePath) + "' " + strResponse, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            //}
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("File uploading failed!" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            return;
                        }

                    }
                    else { return; }
                }
                else
                {
                    MessageBox.Show("File is empty!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            catch (Exception ex) { MessageBox.Show("File uploading failed!" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally
            {
                if (wbclient != null) { wbclient.Dispose(); wbclient = null; }
                if (client != null) { client.Dispose(); client = null; }
            }
        }
        //Ajay: 08/01/2019
        //Ajay: 14/12/2018
        private void DownloadFileUsingHTTP(string OppWebAddr)
        {
            try
            {
                SaveFileDialog svfiledlg = new SaveFileDialog();
                string strFilePath = "";
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    strFilePath = Path.GetDirectoryName(ProtocolGateway.OppConfigurationFile);
                }
                else if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Full)
                {
                    svfiledlg.Title = "Save OpenProPlus Configuration File";
                    svfiledlg.InitialDirectory = Path.GetFullPath(Environment.SpecialFolder.MyDocuments.ToString());
                    svfiledlg.DefaultExt = "xml";
                    svfiledlg.FileName = "openproplus_config.xml";
                    svfiledlg.Filter = "xml files (*.xml)|*.xml";
                    svfiledlg.CheckFileExists = false;
                    svfiledlg.CheckPathExists = true;
                    svfiledlg.SupportMultiDottedExtensions = false;
                    if (svfiledlg.ShowDialog() == DialogResult.OK)
                    {
                        strFilePath = Path.GetDirectoryName(svfiledlg.FileName);
                    }
                    else { return; }
                }
                if (!string.IsNullOrEmpty(strFilePath))
                {
                    WebClient client = new WebClient();
                    try
                    {
                        //Ajay: 08/01/2018
                        NameValueCollection loginData = new NameValueCollection();
                        loginData.Add("username", "admin");
                        loginData.Add("password", "admin");
                        loginData.Add("mode", "download");
                        try
                        {
                            string response = Encoding.ASCII.GetString(client.UploadValues(OppWebAddr + "//rtv2upload.php", "post", loginData));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("File downloading failed!" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            return;
                        }
                        string destinationFileName = "";
                        frmInput frminput = new frmInput();
                        frminput.defaultFileName = GetDefaultFileName(strFilePath);
                        if (frminput.ShowDialog() == DialogResult.OK)
                        {
                            if (!string.IsNullOrEmpty(frminput.txtbxInput.Text.Trim()))
                            {
                                destinationFileName = Path.Combine(Path.GetDirectoryName(ProtocolGateway.ProtocolGatewayConfigurationFile), frminput.txtbxInput.Text.Trim());
                                frminput.Close();
                            }
                        }
                        else { return; }
                        client.DownloadFile(new Uri(OppWebAddr + ":/mnt/app/database/openproplus_config.xml"), "d:\\Test.xml");
                    }
                    catch (Renci.SshNet.Common.SshException ex)
                    {
                        MessageBox.Show("File downloading failed!" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                    finally
                    {
                        if (client != null) { client.Dispose(); client = null; }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("File downloading failed! " + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1); }
        }
        #endregion

        #endregion
        //Ajay: 09/01/2018
        private string GetDefaultFileName(string Path)
        {
            string FileName = "";
            int iCount = System.IO.Directory.GetFiles(Path).Where(x => System.IO.Path.GetExtension(x) == ".xml").Count();
            if (iCount > 0)
            {
                FileName = (iCount).ToString().PadLeft(3, '0') + ".xml";
                //Swati: 31/08/2017
                int i = iCount;
                while (File.Exists(Path + @"\" + FileName))
                {
                    FileName = (i + 1).ToString().PadLeft(3, '0') + ".set";
                    i++;
                }
            }
            else
            { FileName = "000.xml"; }
            return FileName;
        }


        //Ajay: 07/12/2018
        private void tsbtnFTPConfig_Click(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: tsbtnFTPConfig_Click";
            try
            {
                frmFTP ftp = new frmFTP();
                ftp.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 29/12/2018
        private void dRConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: dRConfigurationToolStripMenuItem_Click";
            try
            {
                this.scParser.Panel2.Controls.Clear();//Remove all previous controls...
                //DRConfiguration drConfiguration = new DRConfiguration();
                if (ucsDRConfig == null) ucsDRConfig = new ucDRConfig(this, opcHandle);
                if (drConfiguration == null) drConfiguration = new DRConfiguration(ucsDRConfig); //Ajay: 29/12/2018

                this.scParser.Panel2.Controls.Add(ucsDRConfig);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ajay: 08/01/2019
        private void openProDevicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: openProDevicesToolStripMenuItem_Click";
            try
            {
                frmBonjour frm = new frmBonjour();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuParser_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FrmSLD fr = new FrmSLD();
            fr.ShowDialog();
        }

        public void ExportDataSetToCsvFile(DataTable DDT, string DestinationCsvDirectory)
        {
            string strRoutineName = "frmOpenProPlus:ExportDataSetToCsvFile";
            try
            {
                int iFirstLine = 0;
                {
                    string CsvText = string.Empty;
                    foreach (DataRow DDR in DDT.Rows)
                    {
                        CsvText = string.Empty;
                        CsvText = DDR[6].ToString();
                        //CsvText = DDR["Configuration"].ToString();
                        //outputFile.WriteLine(CsvText.ToString());
                        if (iFirstLine > 0)
                        {
                            File.AppendAllText(DestinationCsvDirectory, Environment.NewLine + CsvText);
                            iFirstLine++;
                        }
                        else
                        {
                            File.AppendAllText(DestinationCsvDirectory, CsvText);
                            iFirstLine++;
                        }
                    }
                    System.Threading.Thread.Sleep(1000);
                    //}
                }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

            FrmSLD c = new FrmSLD();
            c.ShowDialog();

        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            string strRoutineName = "frmOpenProPlus: tsbReindexData_Click";
            try
            {
                opcHandle.regenerateSlaveSequence();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
