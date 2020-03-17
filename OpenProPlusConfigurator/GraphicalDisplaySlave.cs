using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections;
using System.Drawing.Imaging;
using System.IO.Compression;
using OpenProPlusConfigurator.Properties;
using System.Globalization;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Security.AccessControl;
namespace OpenProPlusConfigurator
{
    public class GraphicalDisplaySlave
    {
        ImageList ilTvItems = new ImageList();
        #region Declaration
        enum slaveType
        {
            GraphicalDisplaySlave
        };
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        private enum Operations
        {
            NONE,
            IMPORT,
            CREATE
        }
        private Mode mode = Mode.NONE;
        private Operations operation = Operations.NONE;
        private int editIndex = -1;
        private int eventQSize;
        public TreeNode GraphicalDisplaySlaveTreeNode;
        private bool isNodeComment = false;
        private string comment = "";
        private slaveType sType = slaveType.GraphicalDisplaySlave;

        private int slaveNum = -1;
        private string type = "";
        private string gridRows = "";
        private string gridColumns = "";
        private int debug;
        private string appFirmwareVersion;
        private bool run = true;
        List<SLDSettings> SLDSettingsList = new List<SLDSettings>();
        ucGroupSLD UcSLD = new ucGroupSLD();
        OpenFileDialog openFileDialog1 = new OpenFileDialog();

        public DataGridView DgvData = new DataGridView();
        public DataTable StoreData = new DataTable();
        DataSet DsData = new DataSet();
        DataTable Dt = new DataTable();
        DataTable DtExportData = new DataTable();
        frmOpenProPlus frm = new frmOpenProPlus();
        public static string[] arrAttributes = new string[] { "SlaveNum", "Type", "GridRows", "GridColumns", "EventQSize", "DEBUG", "AppFirmwareVersion", "Run" };
        OpenProPlus_Config opcHandle = null;
        #endregion Declaration
        public GraphicalDisplaySlave(string mbsName, List<KeyValuePair<string, string>> mbsData, TreeNode tn)
        {
            string strRoutineName = "GraphicalDisplaySlave";
            PictureBoxAllowDropEvents();
            UcSLD.btnAddClick += new System.EventHandler(this.btnAdd_Click);
            UcSLD.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
            UcSLD.btnDoneClick += new System.EventHandler(this.btnDone_Click);
            UcSLD.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
            UcSLD.lvSLDListItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvSLDList_ItemCheck);
            UcSLD.lvSLDListDoubleClick += new System.EventHandler(this.lvSLDList_DoubleClick);
            UcSLD.PCDFileClick += new System.EventHandler(this.PCDFile_Click);
            UcSLD.btnImportSLDClick += new System.EventHandler(this.btnImportSLD_Click);
            UcSLD.BtnCreateSLDClick += new System.EventHandler(this.BtnCreateSLD_Click);
            UcSLD.Load += new System.EventHandler(this.ucGroupSLD_Load);
            UcSLD.TvSLDItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TvSLD_ItemDrag);
            UcSLD.TvSLDDragDrop += new System.Windows.Forms.DragEventHandler(this.TvSLD_DragDrop);
            UcSLD.TvSLDDragEnter += new System.Windows.Forms.DragEventHandler(this.TvSLD_DragEnter);
            UcSLD.btnSaveSLDClick += new System.EventHandler(this.btnSaveSLD_Click);
            #region Picturebox
            //UcSLD.PbSource.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PbSource_MouseDown);
            UcSLD.Pb1.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb1_DragDrop);
            UcSLD.Pb1.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb1_DragEnter);
            UcSLD.Pb2.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb2_DragDrop);
            UcSLD.Pb2.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb2_DragEnter);
            UcSLD.Pb3.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb3_DragDrop);
            UcSLD.Pb3.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb3_DragEnter);
            UcSLD.Pb4.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb4_DragDrop);
            UcSLD.Pb4.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb4_DragEnter);
            UcSLD.Pb5.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb5_DragDrop);
            UcSLD.Pb5.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb5_DragEnter);
            UcSLD.Pb6.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb6_DragDrop);
            UcSLD.Pb6.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb6_DragEnter);
            UcSLD.Pb7.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb7_DragDrop);
            UcSLD.Pb7.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb7_DragEnter);
            UcSLD.Pb8.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb8_DragDrop);
            UcSLD.Pb8.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb8_DragEnter);
            UcSLD.Pb9.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb9_DragDrop);
            UcSLD.Pb9.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb9_DragEnter);
            UcSLD.Pb10.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb10_DragDrop);
            UcSLD.Pb10.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb10_DragEnter);

            UcSLD.Pb11.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb11_DragDrop);
            UcSLD.Pb11.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb11_DragEnter);
            UcSLD.Pb12.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb12_DragDrop);
            UcSLD.Pb12.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb12_DragEnter);
            UcSLD.Pb13.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb13_DragDrop);
            UcSLD.Pb13.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb13_DragEnter);
            UcSLD.Pb14.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb14_DragDrop);
            UcSLD.Pb14.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb14_DragEnter);
            UcSLD.Pb15.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb15_DragDrop);
            UcSLD.Pb15.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb15_DragEnter);
            UcSLD.Pb16.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb16_DragDrop);
            UcSLD.Pb16.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb16_DragEnter);
            UcSLD.Pb17.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb17_DragDrop);
            UcSLD.Pb17.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb17_DragEnter);
            UcSLD.Pb18.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb18_DragDrop);
            UcSLD.Pb18.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb18_DragEnter);
            UcSLD.Pb19.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb19_DragDrop);
            UcSLD.Pb19.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb19_DragEnter);
            UcSLD.Pb20.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb20_DragDrop);
            UcSLD.Pb20.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb20_DragEnter);

            UcSLD.Pb21.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb21_DragDrop);
            UcSLD.Pb21.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb21_DragEnter);
            UcSLD.Pb22.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb22_DragDrop);
            UcSLD.Pb22.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb22_DragEnter);
            UcSLD.Pb23.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb23_DragDrop);
            UcSLD.Pb23.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb23_DragEnter);
            UcSLD.Pb24.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb24_DragDrop);
            UcSLD.Pb24.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb24_DragEnter);
            UcSLD.Pb25.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb25_DragDrop);
            UcSLD.Pb25.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb25_DragEnter);
            UcSLD.Pb26.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb26_DragDrop);
            UcSLD.Pb26.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb26_DragEnter);
            UcSLD.Pb27.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb27_DragDrop);
            UcSLD.Pb27.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb27_DragEnter);
            UcSLD.Pb28.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb28_DragDrop);
            UcSLD.Pb28.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb28_DragEnter);
            UcSLD.Pb29.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb29_DragDrop);
            UcSLD.Pb29.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb29_DragEnter);
            UcSLD.Pb30.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb30_DragDrop);
            UcSLD.Pb30.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb30_DragEnter);

            UcSLD.Pb31.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb31_DragDrop);
            UcSLD.Pb31.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb31_DragEnter);
            UcSLD.Pb32.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb32_DragDrop);
            UcSLD.Pb32.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb32_DragEnter);
            UcSLD.Pb33.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb33_DragDrop);
            UcSLD.Pb33.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb33_DragEnter);
            UcSLD.Pb34.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb34_DragDrop);
            UcSLD.Pb34.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb34_DragEnter);
            UcSLD.Pb35.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb35_DragDrop);
            UcSLD.Pb35.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb35_DragEnter);
            UcSLD.Pb36.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb36_DragDrop);
            UcSLD.Pb36.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb36_DragEnter);
            UcSLD.Pb37.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb37_DragDrop);
            UcSLD.Pb37.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb37_DragEnter);
            UcSLD.Pb38.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb38_DragDrop);
            UcSLD.Pb38.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb38_DragEnter);
            UcSLD.Pb39.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb39_DragDrop);
            UcSLD.Pb39.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb39_DragEnter);
            UcSLD.Pb40.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb40_DragDrop);
            UcSLD.Pb40.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb40_DragEnter);


            UcSLD.Pb41.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb41_DragDrop);
            UcSLD.Pb41.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb41_DragEnter);
            UcSLD.Pb42.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb42_DragDrop);
            UcSLD.Pb42.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb42_DragEnter);
            UcSLD.Pb43.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb43_DragDrop);
            UcSLD.Pb43.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb43_DragEnter);
            UcSLD.Pb44.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb44_DragDrop);
            UcSLD.Pb44.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb44_DragEnter);
            UcSLD.Pb45.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb45_DragDrop);
            UcSLD.Pb45.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb45_DragEnter);
            UcSLD.Pb46.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb46_DragDrop);
            UcSLD.Pb46.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb46_DragEnter);
            UcSLD.Pb47.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb47_DragDrop);
            UcSLD.Pb47.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb47_DragEnter);
            UcSLD.Pb48.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb48_DragDrop);
            UcSLD.Pb48.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb48_DragEnter);
            UcSLD.Pb49.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb49_DragDrop);
            UcSLD.Pb49.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb49_DragEnter);
            UcSLD.Pb50.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb50_DragDrop);
            UcSLD.Pb50.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb50_DragEnter);

            UcSLD.Pb51.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb51_DragDrop);
            UcSLD.Pb51.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb51_DragEnter);
            UcSLD.Pb52.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb52_DragDrop);
            UcSLD.Pb52.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb52_DragEnter);
            UcSLD.Pb53.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb53_DragDrop);
            UcSLD.Pb53.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb53_DragEnter);
            UcSLD.Pb54.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb54_DragDrop);
            UcSLD.Pb54.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb54_DragEnter);
            UcSLD.Pb55.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb55_DragDrop);
            UcSLD.Pb55.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb55_DragEnter);
            UcSLD.Pb56.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb56_DragDrop);
            UcSLD.Pb56.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb56_DragEnter);
            UcSLD.Pb57.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb57_DragDrop);
            UcSLD.Pb57.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb57_DragEnter);
            UcSLD.Pb58.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb58_DragDrop);
            UcSLD.Pb58.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb58_DragEnter);
            UcSLD.Pb59.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb59_DragDrop);
            UcSLD.Pb59.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb59_DragEnter);
            UcSLD.Pb60.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb60_DragDrop);
            UcSLD.Pb60.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb60_DragEnter);
            #endregion PictureBox DragDrop
            addListHeaders();
            try
            {
                try
                {
                    sType = (slaveType)Enum.Parse(typeof(slaveType), mbsName);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", mbsName);
                }
                if (mbsData != null && mbsData.Count > 0)
                {
                    foreach (KeyValuePair<string, string> mbskp in mbsData)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", mbskp.Key, mbskp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(mbskp.Key) != null) //Ajay: 03/07/2018
                            {
                                this.GetType().GetProperty(mbskp.Key).SetValue(this, mbskp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value:{1}", mbskp.Key, mbskp.Value);
                        }
                    }
                    Utils.Write(VerboseLevel.DEBUG, "\n");
                    if (tn != null) tn.Nodes.Clear();
                    GraphicalDisplaySlaveTreeNode = tn;
                    if (tn != null) tn.Text = "GDisplaySlave " + "GraphicalDisplaySlave_" + this.SlaveNum;
                    GDSlave.SlaveNo = this.SlaveNum;
                    GDSlave.GDSlaveTreeNode = tn;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public GraphicalDisplaySlave(XmlNode sNode, TreeNode tn)
        {
            PictureBoxAllowDropEvents();
            //UcSLD.ChkSHGridLines.CheckedChanged += new System.EventHandler(this.ChkSHGridLines_CheckedChanged);
            UcSLD.btnAddClick += new System.EventHandler(this.btnAdd_Click);
            UcSLD.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
            UcSLD.btnDoneClick += new System.EventHandler(this.btnDone_Click);
            UcSLD.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
            UcSLD.lvSLDListItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvSLDList_ItemCheck);
            UcSLD.lvSLDListDoubleClick += new System.EventHandler(this.lvSLDList_DoubleClick);
            UcSLD.PCDFileClick += new System.EventHandler(this.PCDFile_Click);
            UcSLD.btnImportSLDClick += new System.EventHandler(this.btnImportSLD_Click);
            UcSLD.BtnCreateSLDClick += new System.EventHandler(this.BtnCreateSLD_Click);
            UcSLD.Load += new System.EventHandler(this.ucGroupSLD_Load);
            UcSLD.TvSLDItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TvSLD_ItemDrag);
            UcSLD.TvSLDDragDrop += new System.Windows.Forms.DragEventHandler(this.TvSLD_DragDrop);
            UcSLD.TvSLDDragEnter += new System.Windows.Forms.DragEventHandler(this.TvSLD_DragEnter);
            UcSLD.btnSaveSLDClick += new System.EventHandler(this.btnSaveSLD_Click);
            #region Picturebox
            //UcSLD.PbSource.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PbSource_MouseDown);
            UcSLD.Pb1.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb1_DragDrop);
            UcSLD.Pb1.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb1_DragEnter);
            UcSLD.Pb2.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb2_DragDrop);
            UcSLD.Pb2.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb2_DragEnter);
            UcSLD.Pb3.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb3_DragDrop);
            UcSLD.Pb3.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb3_DragEnter);
            UcSLD.Pb4.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb4_DragDrop);
            UcSLD.Pb4.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb4_DragEnter);
            UcSLD.Pb5.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb5_DragDrop);
            UcSLD.Pb5.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb5_DragEnter);
            UcSLD.Pb6.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb6_DragDrop);
            UcSLD.Pb6.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb6_DragEnter);
            UcSLD.Pb7.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb7_DragDrop);
            UcSLD.Pb7.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb7_DragEnter);
            UcSLD.Pb8.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb8_DragDrop);
            UcSLD.Pb8.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb8_DragEnter);
            UcSLD.Pb9.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb9_DragDrop);
            UcSLD.Pb9.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb9_DragEnter);
            UcSLD.Pb10.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb10_DragDrop);
            UcSLD.Pb10.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb10_DragEnter);

            UcSLD.Pb11.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb11_DragDrop);
            UcSLD.Pb11.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb11_DragEnter);
            UcSLD.Pb12.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb12_DragDrop);
            UcSLD.Pb12.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb12_DragEnter);
            UcSLD.Pb13.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb13_DragDrop);
            UcSLD.Pb13.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb13_DragEnter);
            UcSLD.Pb14.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb14_DragDrop);
            UcSLD.Pb14.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb14_DragEnter);
            UcSLD.Pb15.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb15_DragDrop);
            UcSLD.Pb15.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb15_DragEnter);
            UcSLD.Pb16.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb16_DragDrop);
            UcSLD.Pb16.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb16_DragEnter);
            UcSLD.Pb17.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb17_DragDrop);
            UcSLD.Pb17.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb17_DragEnter);
            UcSLD.Pb18.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb18_DragDrop);
            UcSLD.Pb18.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb18_DragEnter);
            UcSLD.Pb19.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb19_DragDrop);
            UcSLD.Pb19.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb19_DragEnter);
            UcSLD.Pb20.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb20_DragDrop);
            UcSLD.Pb20.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb20_DragEnter);

            UcSLD.Pb21.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb21_DragDrop);
            UcSLD.Pb21.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb21_DragEnter);
            UcSLD.Pb22.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb22_DragDrop);
            UcSLD.Pb22.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb22_DragEnter);
            UcSLD.Pb23.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb23_DragDrop);
            UcSLD.Pb23.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb23_DragEnter);
            UcSLD.Pb24.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb24_DragDrop);
            UcSLD.Pb24.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb24_DragEnter);
            UcSLD.Pb25.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb25_DragDrop);
            UcSLD.Pb25.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb25_DragEnter);
            UcSLD.Pb26.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb26_DragDrop);
            UcSLD.Pb26.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb26_DragEnter);
            UcSLD.Pb27.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb27_DragDrop);
            UcSLD.Pb27.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb27_DragEnter);
            UcSLD.Pb28.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb28_DragDrop);
            UcSLD.Pb28.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb28_DragEnter);
            UcSLD.Pb29.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb29_DragDrop);
            UcSLD.Pb29.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb29_DragEnter);
            UcSLD.Pb30.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb30_DragDrop);
            UcSLD.Pb30.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb30_DragEnter);

            UcSLD.Pb31.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb31_DragDrop);
            UcSLD.Pb31.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb31_DragEnter);
            UcSLD.Pb32.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb32_DragDrop);
            UcSLD.Pb32.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb32_DragEnter);
            UcSLD.Pb33.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb33_DragDrop);
            UcSLD.Pb33.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb33_DragEnter);
            UcSLD.Pb34.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb34_DragDrop);
            UcSLD.Pb34.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb34_DragEnter);
            UcSLD.Pb35.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb35_DragDrop);
            UcSLD.Pb35.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb35_DragEnter);
            UcSLD.Pb36.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb36_DragDrop);
            UcSLD.Pb36.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb36_DragEnter);
            UcSLD.Pb37.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb37_DragDrop);
            UcSLD.Pb37.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb37_DragEnter);
            UcSLD.Pb38.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb38_DragDrop);
            UcSLD.Pb38.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb38_DragEnter);
            UcSLD.Pb39.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb39_DragDrop);
            UcSLD.Pb39.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb39_DragEnter);
            UcSLD.Pb40.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb40_DragDrop);
            UcSLD.Pb40.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb40_DragEnter);
            UcSLD.Pb41.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb41_DragDrop);
            UcSLD.Pb41.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb41_DragEnter);
            UcSLD.Pb42.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb42_DragDrop);
            UcSLD.Pb42.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb42_DragEnter);
            UcSLD.Pb43.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb43_DragDrop);
            UcSLD.Pb43.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb43_DragEnter);
            UcSLD.Pb44.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb44_DragDrop);
            UcSLD.Pb44.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb44_DragEnter);
            UcSLD.Pb45.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb45_DragDrop);
            UcSLD.Pb45.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb45_DragEnter);
            UcSLD.Pb46.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb46_DragDrop);
            UcSLD.Pb46.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb46_DragEnter);
            UcSLD.Pb47.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb47_DragDrop);
            UcSLD.Pb47.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb47_DragEnter);
            UcSLD.Pb48.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb48_DragDrop);
            UcSLD.Pb48.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb48_DragEnter);
            UcSLD.Pb49.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb49_DragDrop);
            UcSLD.Pb49.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb49_DragEnter);
            UcSLD.Pb50.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb50_DragDrop);
            UcSLD.Pb50.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb50_DragEnter);

            UcSLD.Pb51.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb51_DragDrop);
            UcSLD.Pb51.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb51_DragEnter);
            UcSLD.Pb52.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb52_DragDrop);
            UcSLD.Pb52.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb52_DragEnter);
            UcSLD.Pb53.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb53_DragDrop);
            UcSLD.Pb53.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb53_DragEnter);
            UcSLD.Pb54.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb54_DragDrop);
            UcSLD.Pb54.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb54_DragEnter);
            UcSLD.Pb55.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb55_DragDrop);
            UcSLD.Pb55.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb55_DragEnter);
            UcSLD.Pb56.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb56_DragDrop);
            UcSLD.Pb56.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb56_DragEnter);
            UcSLD.Pb57.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb57_DragDrop);
            UcSLD.Pb57.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb57_DragEnter);
            UcSLD.Pb58.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb58_DragDrop);
            UcSLD.Pb58.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb58_DragEnter);
            UcSLD.Pb59.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb59_DragDrop);
            UcSLD.Pb59.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb59_DragEnter);
            UcSLD.Pb60.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pb60_DragDrop);
            UcSLD.Pb60.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pb60_DragEnter);

            #endregion PictureBox DragDrop
            addListHeaders();
            string strRoutineName = "GraphicalDisplaySlave";
            try
            {
                if (sNode.Attributes != null)
                {
                    try
                    {
                        sType = (slaveType)Enum.Parse(typeof(slaveType), sNode.Name);
                    }
                    catch (System.ArgumentException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", sNode.Name);
                    }

                    foreach (XmlAttribute item in sNode.Attributes)
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
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value:{1}", item.Name, item.Value);
                        }
                    }
                    Utils.Write(VerboseLevel.DEBUG, "\n");
                    if (tn != null) tn.Nodes.Clear();
                    GraphicalDisplaySlaveTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...   
                    if (tn != null) tn.Text = "GDisplaySlave " + "GraphicalDisplaySlave_" + this.SlaveNum;
                    parseIECGNode(sNode, tn);
                }
                else if (sNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = sNode.Value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        List<string> WidgetImages = new List<string>(); List<string> filesFound = new List<string>();
        public string[] GetFilesFrom(string searchFolder, string[] filters, bool isRecursive)
        {
            //List<string> filesFound = new List<string>();
            var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (var filter in filters)
            {
                filesFound.AddRange(System.IO.Directory.GetFiles(searchFolder, String.Format("*.{0}", filter), searchOption));
                WidgetImages.AddRange(System.IO.Directory.GetFiles(searchFolder, String.Format("*.{0}", filter), searchOption));
            }
            return filesFound.ToArray();
        }
        private void btnSaveSLD_Click(object sender, EventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:btnSaveSLD_Click";
            try
            {
                GDSlave.IsSLDImport = false;
                string TableName = "GraphicalDisplaySlave_" + GDSlave.GDSlaveNo + "_" + "diagram.txt"; //GraphicalDisplaySlave_1_5x8 SingleBUS_1.txt
                GDSlave.SlaveFolder = "GraphicalDisplaySlave_" + GDSlave.GDSlaveNo;
                string FilePath = System.IO.Path.GetTempPath() + "protocol" + @"\" + "GUI" + @"\" + GDSlave.SlaveFolder;
                string FileName = "diagram.txt";
                UcSLD.txtTemplateSLDFile.Text = FileName;
                UcSLD.txtTemplateSLDFile.Focus();
                UcSLD.txtTemplateSLDFile.Enabled = false;
                UcSLD.PCDFile.Visible = false;
                GDSlave.XLSFileName = FileName;//5x8 SingleBUS_1.txt
                GDSlave.FileFullPath = FilePath; //C:\Users\namrata\Desktop\5x8 SingleBUS_1.txt
                //UcSLD.lblFilePath.Text  = FilePath;
                //Create Directory to store .xls files
                Directory.CreateDirectoryForGDisplay(FileName, FilePath);
                SaveData(FilePath, TableName, ",");
                Directory.CreateDirForWidgets(FileName, FilePath);
                //mode = Mode.ADD;
                //editIndex = -1;
                ////Utils.resetValues(UcSLD.grpSLDSetting);
                ////Utils.showNavigation(UcSLD.grpSLDSetting, false);
                UcSLD.grpSLDSetting.Visible = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public DataSet SaveData(string File, string TableName, string delimiter)
        {
            string DestDir = System.IO.Path.GetTempPath() + "protocol" + @"\" + "GUI" + @"\" + GDSlave.SlaveFolder;
            if (GDSlave.DsExcelData.Tables.Contains(TableName))
            {
                GDSlave.DsExcelData.Tables.Remove(TableName);
                GDSlave.DsExcelData.Tables.Add(TableName);
            }
            else { GDSlave.DsExcelData.Tables.Add(TableName); }
            #region Add Columns
            if (!GDSlave.DsExcelData.Tables[TableName].Columns.Contains("CellNo"))
            {
                GDSlave.DsExcelData.Tables[TableName].Columns.Add("CellNo", typeof(object));
            }
            if (!GDSlave.DsExcelData.Tables[TableName].Columns.Contains("Widget"))
            {
                GDSlave.DsExcelData.Tables[TableName].Columns.Add("Widget", typeof(string));
            }
            if (!GDSlave.DsExcelData.Tables[TableName].Columns.Contains("Type"))
            {
                GDSlave.DsExcelData.Tables[TableName].Columns.Add("Type", typeof(string));
            }
            if (!GDSlave.DsExcelData.Tables[TableName].Columns.Contains("Name"))
            {
                GDSlave.DsExcelData.Tables[TableName].Columns.Add("Name", typeof(string));
            }
            if (!GDSlave.DsExcelData.Tables[TableName].Columns.Contains("DBIndex"))
            {
                GDSlave.DsExcelData.Tables[TableName].Columns.Add("DBIndex", typeof(string));
            }
            if (!GDSlave.DsExcelData.Tables[TableName].Columns.Contains("Unit"))
            {
                GDSlave.DsExcelData.Tables[TableName].Columns.Add("Unit", typeof(string));
            }
            if (!GDSlave.DsExcelData.Tables[TableName].Columns.Contains("Configuration"))
            {
                GDSlave.DsExcelData.Tables[TableName].Columns.Add("Configuration", typeof(string));
            }
            #endregion Add Columns

            int iFileRow = 0; int iColumn = 0; int PBname = 1; int iRow = 0; int iCellNo = 1;
            ImageConverter converter = new ImageConverter();
            UcSLD.TblLayoutCSLD.Controls.OfType<PictureBox>().ToList().ForEach(x =>
            {
                Control c = UcSLD.TblLayoutCSLD.GetControlFromPosition(iColumn, iRow);
                DataRow Dr = GDSlave.DsExcelData.Tables[TableName].NewRow();
                Dr["CellNo"] = iCellNo.ToString();
                if (c.Tag != null) { Dr["Widget"] = c.Tag.ToString(); }
                else { Dr["Widget"] = "Blank"; }
                Dr["Type"] = "";
                Dr["Name"] = "";
                Dr["DBIndex"] = "";
                Dr["Unit"] = "";
                Dr["Configuration"] = Dr[0] + "," + Dr[1] + "," + Dr[2] + "," + Dr[3] + "," + Dr[4] + ",";
                GDSlave.DsExcelData.Tables[TableName].Rows.Add(Dr);
                if (iCellNo > 60) { }
                else { iCellNo++; }
                PBname++;
                if (iFileRow <= 60 - 1)
                { iFileRow++; }
                else { }
                if (iColumn == 5)
                {
                    iColumn = 0; iRow++;
                }
                else
                {
                    iColumn++;
                }
            });

            CreateTextFile(GDSlave.DsExcelData.Tables[TableName], DestDir);
            return GDSlave.DsExcelData;
        }
        public DataSet SaveData1(string File, string TableName, string delimiter)
        {
            string DestDir = System.IO.Path.GetTempPath() + "protocol" + @"\" + "GUI" + @"\" + GDSlave.SlaveFolder;
            if (GDSlave.DsExcelData.Tables.Contains(TableName))
            {
                GDSlave.DsExcelData.Tables.Remove(TableName);
                GDSlave.DsExcelData.Tables.Add(TableName);
            }
            else { GDSlave.DsExcelData.Tables.Add(TableName); }
            #region Add Columns
            if (!GDSlave.DsExcelData.Tables[TableName].Columns.Contains("CellNo"))
            {
                GDSlave.DsExcelData.Tables[TableName].Columns.Add("CellNo", typeof(object));
            }
            if (!GDSlave.DsExcelData.Tables[TableName].Columns.Contains("Widget"))
            {
                GDSlave.DsExcelData.Tables[TableName].Columns.Add("Widget", typeof(string));
            }
            if (!GDSlave.DsExcelData.Tables[TableName].Columns.Contains("Type"))
            {
                GDSlave.DsExcelData.Tables[TableName].Columns.Add("Type", typeof(string));
            }
            if (!GDSlave.DsExcelData.Tables[TableName].Columns.Contains("Name"))
            {
                GDSlave.DsExcelData.Tables[TableName].Columns.Add("Name", typeof(string));
            }
            if (!GDSlave.DsExcelData.Tables[TableName].Columns.Contains("DBIndex"))
            {
                GDSlave.DsExcelData.Tables[TableName].Columns.Add("DBIndex", typeof(string));
            }
            if (!GDSlave.DsExcelData.Tables[TableName].Columns.Contains("Unit"))
            {
                GDSlave.DsExcelData.Tables[TableName].Columns.Add("Unit", typeof(string));
            }
            if (!GDSlave.DsExcelData.Tables[TableName].Columns.Contains("Configuration"))
            {
                GDSlave.DsExcelData.Tables[TableName].Columns.Add("Configuration", typeof(string));
            }
            #endregion Add Columns

            int iFileRow = 0; int iColumn = 0; int PBname = 1; int iRow = 0; int iCellNo = 1;
            ImageConverter converter = new ImageConverter();
            //pb.Margin = new Padding(0, 0, 0, 0);
            UcSLD.TblLayoutCSLD.Controls.OfType<PictureBox>().ToList().ForEach(x =>
            {
                Control c = UcSLD.TblLayoutCSLD.GetControlFromPosition(iColumn, iRow);
                DataRow Dr = GDSlave.DsExcelData.Tables[TableName].NewRow();
                Dr["CellNo"] = iCellNo.ToString();
                if (c.Tag != null) { Dr["Widget"] = c.Tag.ToString(); }
                else { Dr["Widget"] = "Blank"; }
                Dr["Type"] = "";
                Dr["Name"] = "";
                Dr["DBIndex"] = "";
                Dr["Unit"] = "";
                Dr["Configuration"] = Dr[0] + "," + Dr[1] + "," + Dr[2] + "," + Dr[3] + "," + Dr[4] + ",";
                GDSlave.DsExcelData.Tables[TableName].Rows.Add(Dr);
                if (iCellNo > 40) { }
                else { iCellNo++; }
                PBname++;
                if (iFileRow <= 40 - 1)
                { iFileRow++; }
                else { }
                if (iColumn == 4)
                {
                    iColumn = 0; iRow++;
                }
                else
                {
                    iColumn++;
                }


            });

            CreateTextFile(GDSlave.DsExcelData.Tables[TableName], DestDir);
            return GDSlave.DsExcelData;
        }
        private void CreateTextFile(DataTable DDT, string DestinationTxtDirectory)
        {
            string strRoutineName = "frmOpenProPlus:CreateTxtFile";
            try
            {
                string TxtFileName = Path.Combine(DestinationTxtDirectory, "diagram.txt");
                File.Create(TxtFileName).Dispose();
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
                            File.AppendAllText(TxtFileName, Environment.NewLine + CsvText);
                            iFirstLine++;
                        }
                        else
                        {
                            File.AppendAllText(TxtFileName, CsvText);
                            iFirstLine++;
                        }
                    }
                    System.Threading.Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #region CommonNodes
        KeyValuePair<string, string>[] CommonNodes = new KeyValuePair<string, string>[]
                    {
                        new KeyValuePair<string, string>("Blank","Blank"),
                        new KeyValuePair<string, string>("Lines", "Lines"),
                        new KeyValuePair<string, string>("Switches", "Switches"),
                        new KeyValuePair<string, string>("Symbols", "Symbols"),
                        new KeyValuePair<string, string>("Text", "Text"),
                    };


        #region Line Sub Nodes
        KeyValuePair<string, string>[] LineSubNodes = new KeyValuePair<string, string>[]
                   {
                        new KeyValuePair<string, string>("Line_Singles", "Line_Singles"),
                        new KeyValuePair<string, string>("Line_Doubles", "Line_Doubles"),
                        new KeyValuePair<string, string>("Line_Triples", "Line_Triples"),
                   };

        KeyValuePair<string, string>[] LineNodes = new KeyValuePair<string, string>[]
                    {
                        #region Single Lines
                          new KeyValuePair<string, string>("Line_SingleT_V_U_1", "Line_SingleT_V_U_1"),
                         new KeyValuePair<string, string>("Line_SingleL_V_U_1", "Line_SingleL_V_U_1"),
                        new KeyValuePair<string, string>("Line_SingleT_H_L_1", "Line_SingleT_H_L_1"),
                          new KeyValuePair<string, string>("Line_SingleT_V_D_1", "Line_SingleT_V_D_1"),


                         new KeyValuePair<string, string>("Line_SingleL_H_D_1", "Line_SingleL_H_D_1"),

                        new KeyValuePair<string, string>("Line_SingleT_H_R_2", "Line_SingleT_H_R_2"),
                        new KeyValuePair<string, string>("Line_SingleL_H_L_1", "Line_SingleL_H_L_1"),
                         new KeyValuePair<string, string>("Line_Single_H_R_1", "Line_Single_H_R_1"),
                           new KeyValuePair<string, string>("Line_SingleT_H_R_1", "Line_SingleT_H_R_1"),
                         new KeyValuePair<string, string>("Line_Single_H_R_2", "Line_Single_H_R_2"),
                       new KeyValuePair<string, string>("Line_SingleL_V_R_1", "Line_SingleL_V_R_1"),


                        new KeyValuePair<string, string>("Line_Single_V_D_1", "Line_Single_V_D_1"),
                       
                        
                        //new KeyValuePair<string, string>("Line_SingleL_V_R_1", "Line_SingleL_V_R_1"),
                        //new KeyValuePair<string, string>("Line_SingleL_V_U_1", "Line_SingleL_V_U_1"),
                        
                        //new KeyValuePair<string, string>("Line_SingleT_V_D_1", "Line_SingleT_V_D_1"),
                       
                        //new KeyValuePair<string, string>("Line_SingleT_V_D_1", "Line_SingleT_V_D_1"),
                        
                      
                        #endregion Single Lines

                        #region Double Lines
                         new KeyValuePair<string, string>("Line_DoubleT_H_D_1", "Line_DoubleT_H_D_1"),
                        new KeyValuePair<string, string>("Line_Double_H_D_1", "Line_Double_H_D_1"),
                        new KeyValuePair<string, string>("Line_DoubleT_H_2", "Line_DoubleT_H_2"),
                       
                        #endregion Double Lines

                        #region Triple Lines
                         new KeyValuePair<string, string>("Line_Triple_H_D_3", "Line_Triple_H_D_3"),
                        new KeyValuePair<string, string>("Line_Triple_H_D_1", "Line_Triple_H_D_1"),
                        new KeyValuePair<string, string>("Line_Triple_H_D_2", "Line_Triple_H_D_2"),
                        new KeyValuePair<string, string>("Line_TripleT_H_D_1", "Line_TripleT_H_D_1"),
                        
                       
                        #endregion Triple Lines
                    };
        #endregion Line Sub Nodes

        KeyValuePair<string, string>[] SwitchNodes = new KeyValuePair<string, string>[]
                  {
                        new KeyValuePair<string, string>("SW_H_D_1_OFF", "SW_H_D_1_OFF"),
                        new KeyValuePair<string, string>("SW_H_D_1_ON",  "SW_H_D_1_ON"),
                        new KeyValuePair<string, string>("SW_H_D_1_Undefined", "SW_H_D_1_Undefined"),

                        new KeyValuePair<string, string>("SW_H_R_2_OFF", "SW_H_R_2_OFF"),
                        new KeyValuePair<string, string>("SW_H_R_2_ON", "SW_H_R_2_ON"),
                        new KeyValuePair<string, string>("SW_H_R_2_Undefined", "SW_H_R_2_Undefined"),

                        new KeyValuePair<string, string>("SW_V_D_1_OFF", "SW_V_D_1_OFF"),
                        new KeyValuePair<string, string>("SW_V_D_1_ON", "SW_V_D_1_ON"),
                        new KeyValuePair<string, string>("SW_V_D_1_Undefined", "SW_V_D_1_Undefined"),

                        new KeyValuePair<string, string>("SW_V_U_1_OFF", "SW_V_U_1_OFF"),
                        new KeyValuePair<string, string>("SW_V_U_1_ON", "SW_V_U_1_ON"),
                        new KeyValuePair<string, string>("SW_V_U_1_Undefined", "SW_V_U_1_Undefined"),

                        //new KeyValuePair<string, string>("SW_H_V_R_3_OFF", "SW_H_V_R_3_OFF"),
                        //new KeyValuePair<string, string>("SW_V_D_2_OFF", "SW_V_D_2_OFF"),
                        //new KeyValuePair<string, string>("SW_V_L_1_OFF", "SW_V_L_1_OFF"),
                        //new KeyValuePair<string, string>("SW_V_L_1_ON", "SW_V_L_1_ON"),
                        //new KeyValuePair<string, string>("SW_V_R_1_OFF", "SW_V_R_1_OFF"),
                        //new KeyValuePair<string, string>("SW_V_R_1_ON", "SW_V_R_1_ON"),
                  };

        KeyValuePair<string, string>[] SwitchHD1Nodes = new KeyValuePair<string, string>[]
                 {
                    new KeyValuePair<string, string>("SW_H_D_1_OFF", "SW_H_D_1_OFF"),
                        new KeyValuePair<string, string>("SW_H_D_1_ON",  "SW_H_D_1_ON"),
                        new KeyValuePair<string, string>("SW_H_D_1_Undefined", "SW_H_D_1_Undefined"),
                 };


        KeyValuePair<string, string>[] SwitchHR2Nodes = new KeyValuePair<string, string>[]
                 {
                        new KeyValuePair<string, string>("SW_H_R_2_OFF", "SW_H_D_1_OFF"),
                        new KeyValuePair<string, string>("SW_H_R_2_ON",  "SW_H_D_1_ON"),
                        new KeyValuePair<string, string>("SW_H_R_2_Undefined", "SW_H_D_1_Undefined"),
                 };


        KeyValuePair<string, string>[] SwitchVD1Nodes = new KeyValuePair<string, string>[]
                 {
                    new KeyValuePair<string, string>("SW_V_D_1_OFF", "SW_H_D_1_OFF"),
                    new KeyValuePair<string, string>("SW_V_D_1_ON",  "SW_H_D_1_ON"),
                    new KeyValuePair<string, string>("SW_V_D_1_Undefined", "SW_H_D_1_Undefined"),
                 };


        KeyValuePair<string, string>[] SwitchVU1Nodes = new KeyValuePair<string, string>[]
                 {
                    new KeyValuePair<string, string>("SW_V_U_1_OFF", "SW_H_D_1_OFF"),
                    new KeyValuePair<string, string>("SW_V_U_1_ON",  "SW_H_D_1_ON"),
                    new KeyValuePair<string, string>("SW_V_U_1_Undefined", "SW_H_D_1_Undefined"),
                 };

        KeyValuePair<string, string>[] SymbolNodes = new KeyValuePair<string, string>[]
                {
                        new KeyValuePair<string, string>("Symbol_Arrow_H_L_1", "Symbol_Arrow_H_L_1"),
                        new KeyValuePair<string, string>("Symbol_Arrow_H_R_1", "Symbol_Arrow_H_R_1"),
                        new KeyValuePair<string, string>("Symbol_Arrow_V_D_1",  "Symbol_Arrow_V_D_1"),

                        new KeyValuePair<string, string>("Symbol_GND_H_L_1", "Symbol_GND_H_L_1"),
                        new KeyValuePair<string, string>("Symbol_GND_H_R_1", "Symbol_GND_H_R_1"),
                        new KeyValuePair<string, string>("Symbol_GND_V_R_1", "Symbol_GND_V_R_1"),

                        new KeyValuePair<string, string>("Symbol_Tx_V_D_1", "Symbol_Tx_V_D_1"),
                        new KeyValuePair<string, string>("Symbol_Tx_V_D_2", "Symbol_Tx_V_D_2"),

                        new KeyValuePair<string, string>("Symbol2_CT_V_D_1", "Symbol2_CT_V_D_1"),

                        new KeyValuePair<string, string>("Symbol2_Tx_V_D_1", "Symbol2_Tx_V_D_1"),
                        new KeyValuePair<string, string>("Symbol2_Tx_V_D_2", "Symbol2_Tx_V_D_2"),

                        //new KeyValuePair<string, string>("Symbol_CT_V_R_1", "Symbol_CT_V_R_1"),
                        //new KeyValuePair<string, string>("Symbol_PT_H_R_1", "Symbol_PT_H_R_1"),
                        //new KeyValuePair<string, string>("Symbol_PT_V_D_1", "Symbol_PT_V_D_1"),
                };
        KeyValuePair<string, string>[] TextNodes = new KeyValuePair<string, string>[]
             {
                        new KeyValuePair<string, string>("Text_AI_2_H_L_1", "Text_AI_2_H_L_1"),
                        new KeyValuePair<string, string>("Text_AI_3_H_L_1", "Text_AI_3_H_L_1"),
                        new KeyValuePair<string, string>("Text_AI_4_H_L_1", "Text_AI_4_H_L_1"),
                        new KeyValuePair<string, string>("Text_AI_5_H_L_1", "Text_AI_5_H_L_1"),
                        new KeyValuePair<string, string>("Text_AI_H_L_1",  "Text_AI_H_L_1"),
             };


        #endregion CommonNodes


        private void AddTreeNodes()
        {
            string strRoutineName = "GraphicalDisplaySlave: AddTreeNodes";
            try
            {
                //Namrata:20/11/2019
                UcSLD.TvSLD.Nodes.Clear();
                string searchFolder = Globals.Widgets_path;
                var filters = new string[] { "jpg", "png", "bmp" };
                var files = GetFilesFrom(searchFolder, filters, false);
                ilTvItems.ImageSize = new Size(13, 13);
                ilTvItems.ColorDepth = ColorDepth.Depth32Bit;
                UcSLD.TvSLD.ImageList = ilTvItems;
                TreeNode ParentNode = UcSLD.TvSLD.Nodes.Add("Widget");
                TreeNode ChildeNode; ParentNode.Nodes.Clear();
                for (int i = 0; i < filesFound.Count(); i++)
                {
                    string WidgetName = Path.GetFileNameWithoutExtension(filesFound[i].ToString());
                    string filename = Path.Combine(searchFolder, WidgetName + ".png");
                    ilTvItems.Images.Add(WidgetName, Image.FromFile(@"" + filename));
                    ChildeNode = ParentNode.Nodes.Add(WidgetName, WidgetName, WidgetName, WidgetName);
                }
                ParentNode.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void TvSLD_ItemDrag(object sender, ItemDragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: TvSLD_ItemDrag";
            try
            {
                TreeNode aNode = (TreeNode)e.Item;
                GDSlave.GetTreeNode = aNode.Text.ToString();
                int ImgLstIndex = ilTvItems.Images.IndexOfKey(GDSlave.GetTreeNode);

                if (e.Button == MouseButtons.Left)
                {
                    UcSLD.TvSLD.DoDragDrop(ilTvItems.Images[ImgLstIndex], DragDropEffects.Copy);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void TvSLD_DragDrop(object sender, DragEventArgs e)
        {
        }
        private void TvSLD_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        #region PictureBox DragEnter
        private void Pb1_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb1_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb2_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb2_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb3_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb3_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb4_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb4_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb5_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb5_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb6_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb6_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb7_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb7_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb8_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb8_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb9_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb9_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb10_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb10_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb11_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb11_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb12_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb12_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb13_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb13_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb14_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb14_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb15_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb15_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb16_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb16_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb17_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb17_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb18_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb18_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb19_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb19_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb20_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb20_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb21_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb21_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb22_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb22_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb23_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb23_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb24_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb24_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb25_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb25_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb26_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb26_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb27_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb27_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb28_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb28_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb29_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb29_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb30_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb30_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb31_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb31_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb32_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb32_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb33_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb33_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb34_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb34_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb35_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb35_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb37_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb37_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb38_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb38_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb36_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb36_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb39_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb39_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb40_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb40_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb41_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb41_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb42_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb42_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb43_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb43_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb44_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb44_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Pb45_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb45_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb46_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb46_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb47_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb47_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb48_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb48_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb49_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb49_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb50_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb50_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Pb51_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb51_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb52_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb51_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb53_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb53_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb54_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb54_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb55_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb55_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb56_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb56_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb57_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb57_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb58_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb58_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Pb59_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb59_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb60_DragEnter(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb60_DragEnter";
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.None; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion PictureBox DragEnter

        #region PictureBox DragDrop
        private void Pb1_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb1_DragDrop";
            try
            {
                //string searchFolder = Globals.Widgets_path;

                //Image image = Image.FromFile(path);
                //object o = Properties.Resources.ResourceManager.GetObject(GDSlave.GetTreeNode);
                //if (o is Image)
                //{
                //    UcSLD.Pb1.Image = o as Image;
                //}
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb1.Image = Image.FromFile(ImagePath);
                UcSLD.Pb1.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb1.BorderStyle = BorderStyle.None;
                UcSLD.Pb1.Size = new Size(70, 70);//new Size(50, 50);
                UcSLD.Pb1.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb2_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb2_DragDrop";
            try
            {
                //object o = Properties.Resources.ResourceManager.GetObject(GDSlave.GetTreeNode);
                //if (o is Image)
                //{
                //    UcSLD.Pb2.Image = o as Image;
                //}
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb2.Image = Image.FromFile(ImagePath);
                UcSLD.Pb2.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb2.BorderStyle = BorderStyle.None;
                UcSLD.Pb2.Size = new Size(70, 70); //new Size(50, 50);
                UcSLD.Pb2.Tag = GDSlave.GetTreeNode;
                //PictureboxTags(UcSLD.Pb2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb3_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb3_DragDrop";
            try
            {
                //object o = Properties.Resources.ResourceManager.GetObject(GDSlave.GetTreeNode);
                //if (o is Image)
                //{
                //    UcSLD.Pb3.Image = o as Image;
                //}
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb3.Image = Image.FromFile(ImagePath);
                UcSLD.Pb3.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb3.BorderStyle = BorderStyle.None;
                UcSLD.Pb3.Size = new Size(70, 70);//new Size(70, 70);
                UcSLD.Pb3.Tag = GDSlave.GetTreeNode;
                //UcSLD.Pb3.Tag = GDSlave.ImageName;
                // PictureboxTags(UcSLD.Pb3);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb4_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb4_DragDrop";
            try
            {
                //object o = Properties.Resources.ResourceManager.GetObject(GDSlave.GetTreeNode);
                //if (o is Image)
                //{
                //    UcSLD.Pb4.Image = o as Image;
                //}
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb4.Image = Image.FromFile(ImagePath);
                UcSLD.Pb4.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb4.BorderStyle = BorderStyle.None;
                UcSLD.Pb4.Size = new Size(70, 70);
                UcSLD.Pb4.Tag = GDSlave.GetTreeNode;
                //UcSLD.Pb4.Tag = GDSlave.ImageName;
                //PictureboxTags(UcSLD.Pb4);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb5_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb5_DragDrop";
            try
            {
                //object o = Properties.Resources.ResourceManager.GetObject(GDSlave.GetTreeNode);
                //if (o is Image)
                //{
                //    UcSLD.Pb5.Image = o as Image;
                //}
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb5.Image = Image.FromFile(ImagePath);
                UcSLD.Pb5.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb5.BorderStyle = BorderStyle.None;
                UcSLD.Pb5.Size = new Size(70, 70);
                UcSLD.Pb5.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb6_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb6_DragDrop";
            try
            {
                //object o = Properties.Resources.ResourceManager.GetObject(GDSlave.GetTreeNode);
                //if (o is Image)
                //{
                //    UcSLD.Pb6.Image = o as Image;
                //}
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb6.Image = Image.FromFile(ImagePath);
                UcSLD.Pb6.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb6.BorderStyle = BorderStyle.None;
                UcSLD.Pb6.Size = new Size(70, 70);
                UcSLD.Pb6.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb7_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb7_DragDrop";
            try
            {
                //object o = Properties.Resources.ResourceManager.GetObject(GDSlave.GetTreeNode);
                //if (o is Image)
                //{
                //    UcSLD.Pb7.Image = o as Image;
                //}
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb7.Image = Image.FromFile(ImagePath);
                UcSLD.Pb7.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb7.BorderStyle = BorderStyle.None;
                UcSLD.Pb7.Size = new Size(70, 70);
                UcSLD.Pb7.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb8_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb8_DragDrop";
            try
            {
                //object o = Properties.Resources.ResourceManager.GetObject(GDSlave.GetTreeNode);
                //if (o is Image)
                //{
                //    UcSLD.Pb8.Image = o as Image;
                //}
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb8.Image = Image.FromFile(ImagePath);
                UcSLD.Pb8.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb8.BorderStyle = BorderStyle.None;
                UcSLD.Pb8.Size = new Size(70, 70);
                UcSLD.Pb8.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb9_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb9_DragDrop";
            try
            {
                //object o = Properties.Resources.ResourceManager.GetObject(GDSlave.GetTreeNode);
                //if (o is Image)
                //{
                //    UcSLD.Pb9.Image = o as Image;
                //}
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb9.Image = Image.FromFile(ImagePath);
                UcSLD.Pb9.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb9.BorderStyle = BorderStyle.None;
                UcSLD.Pb9.Size = new Size(70, 70);
                UcSLD.Pb9.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb10_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb10_DragDrop";
            try
            {
                //object o = Properties.Resources.ResourceManager.GetObject(GDSlave.GetTreeNode);
                //if (o is Image)
                //{
                //    UcSLD.Pb10.Image = o as Image;
                //}
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb10.Image = Image.FromFile(ImagePath);
                UcSLD.Pb10.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb10.BorderStyle = BorderStyle.None;
                UcSLD.Pb10.Size = new Size(70, 70);
                UcSLD.Pb10.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb11_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb11_DragDrop";
            try
            {
                //object o = Properties.Resources.ResourceManager.GetObject(GDSlave.GetTreeNode);
                //if (o is Image)
                //{
                //    UcSLD.Pb11.Image = o as Image;
                //}
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb11.Image = Image.FromFile(ImagePath);
                UcSLD.Pb11.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb11.BorderStyle = BorderStyle.None;
                UcSLD.Pb11.Size = new Size(70, 70);
                UcSLD.Pb11.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb12_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb12_DragDrop";
            try
            {
                //object o = Properties.Resources.ResourceManager.GetObject(GDSlave.GetTreeNode);
                //if (o is Image)
                //{
                //    UcSLD.Pb12.Image = o as Image;
                //}
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb12.Image = Image.FromFile(ImagePath);
                UcSLD.Pb12.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb12.BorderStyle = BorderStyle.None;
                UcSLD.Pb12.Size = new Size(70, 70);
                UcSLD.Pb12.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb13_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb13_DragDrop";
            try
            {
                //object o = Properties.Resources.ResourceManager.GetObject(GDSlave.GetTreeNode);
                //if (o is Image)
                //{
                //    UcSLD.Pb13.Image = o as Image;
                //}
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb13.Image = Image.FromFile(ImagePath);
                UcSLD.Pb13.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb13.BorderStyle = BorderStyle.None;
                UcSLD.Pb13.Size = new Size(70, 70);
                UcSLD.Pb13.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb14_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb14_DragDrop";
            try
            {
                //object o = Properties.Resources.ResourceManager.GetObject(GDSlave.GetTreeNode);
                //if (o is Image)
                //{
                //    UcSLD.Pb14.Image = o as Image;
                //}
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb14.Image = Image.FromFile(ImagePath);
                UcSLD.Pb14.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb14.BorderStyle = BorderStyle.None;
                UcSLD.Pb14.Size = new Size(70, 70);
                UcSLD.Pb14.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb15_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb15_DragDrop";
            try
            {
                //object o = Properties.Resources.ResourceManager.GetObject(GDSlave.GetTreeNode);
                //if (o is Image)
                //{
                //    UcSLD.Pb15.Image = o as Image;
                //}
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb15.Image = Image.FromFile(ImagePath);
                UcSLD.Pb15.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb15.BorderStyle = BorderStyle.None;
                UcSLD.Pb15.Size = new Size(70, 70);
                UcSLD.Pb15.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb16_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb16_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb16.Image = Image.FromFile(ImagePath);
                UcSLD.Pb16.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb16.BorderStyle = BorderStyle.None;
                UcSLD.Pb16.Size = new Size(70, 70);
                UcSLD.Pb16.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb17_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb17_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb17.Image = Image.FromFile(ImagePath);
                UcSLD.Pb17.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb17.BorderStyle = BorderStyle.None;
                UcSLD.Pb17.Size = new Size(70, 70);
                UcSLD.Pb17.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb18_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb18_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb18.Image = Image.FromFile(ImagePath);
                UcSLD.Pb18.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb18.BorderStyle = BorderStyle.None;
                UcSLD.Pb18.Size = new Size(70, 70);
                UcSLD.Pb18.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb19_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb19_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb19.Image = Image.FromFile(ImagePath);
                UcSLD.Pb19.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb19.BorderStyle = BorderStyle.None;
                UcSLD.Pb19.Size = new Size(70, 70);
                UcSLD.Pb19.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb20_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb20_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb20.Image = Image.FromFile(ImagePath);
                UcSLD.Pb20.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb20.BorderStyle = BorderStyle.None;
                UcSLD.Pb20.Size = new Size(70, 70);
                UcSLD.Pb20.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb21_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb21_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb21.Image = Image.FromFile(ImagePath);
                UcSLD.Pb21.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb21.BorderStyle = BorderStyle.None;
                UcSLD.Pb21.Size = new Size(70, 70);
                UcSLD.Pb21.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb22_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb22_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb22.Image = Image.FromFile(ImagePath);
                UcSLD.Pb22.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb22.BorderStyle = BorderStyle.None;
                UcSLD.Pb22.Size = new Size(70, 70);
                UcSLD.Pb22.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb23_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb23_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb23.Image = Image.FromFile(ImagePath);
                UcSLD.Pb23.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb23.BorderStyle = BorderStyle.None;
                UcSLD.Pb23.Size = new Size(70, 70);
                UcSLD.Pb23.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb24_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb24_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb24.Image = Image.FromFile(ImagePath);
                UcSLD.Pb24.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb24.BorderStyle = BorderStyle.None;
                UcSLD.Pb24.Size = new Size(70, 70);
                UcSLD.Pb24.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb25_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb25_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb25.Image = Image.FromFile(ImagePath);
                UcSLD.Pb25.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb25.BorderStyle = BorderStyle.None;
                UcSLD.Pb25.Size = new Size(70, 70);
                UcSLD.Pb25.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb26_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb26_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb26.Image = Image.FromFile(ImagePath);
                UcSLD.Pb26.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb26.BorderStyle = BorderStyle.None;
                UcSLD.Pb26.Size = new Size(70, 70);
                UcSLD.Pb26.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb27_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb27_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb27.Image = Image.FromFile(ImagePath);
                UcSLD.Pb27.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb27.BorderStyle = BorderStyle.None;
                UcSLD.Pb27.Size = new Size(70, 70);
                UcSLD.Pb27.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb28_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb28_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb28.Image = Image.FromFile(ImagePath);
                UcSLD.Pb28.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb28.BorderStyle = BorderStyle.None;
                UcSLD.Pb28.Size = new Size(70, 70);
                UcSLD.Pb28.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb29_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb29_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb29.Image = Image.FromFile(ImagePath);
                UcSLD.Pb29.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb29.BorderStyle = BorderStyle.None;
                UcSLD.Pb29.Size = new Size(70, 70);
                UcSLD.Pb29.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb30_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb30_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb30.Image = Image.FromFile(ImagePath);
                UcSLD.Pb30.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb30.BorderStyle = BorderStyle.None;
                UcSLD.Pb30.Size = new Size(70, 70);
                UcSLD.Pb30.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb31_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb31_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb31.Image = Image.FromFile(ImagePath);
                UcSLD.Pb31.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb31.BorderStyle = BorderStyle.None;
                UcSLD.Pb31.Size = new Size(70, 70);
                UcSLD.Pb31.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb32_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb32_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb32.Image = Image.FromFile(ImagePath);
                UcSLD.Pb32.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb32.BorderStyle = BorderStyle.None;
                UcSLD.Pb32.Size = new Size(70, 70);
                UcSLD.Pb32.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb33_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb33_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb33.Image = Image.FromFile(ImagePath);
                UcSLD.Pb33.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb33.BorderStyle = BorderStyle.None;
                UcSLD.Pb33.Size = new Size(70, 70);
                UcSLD.Pb33.Tag = GDSlave.GetTreeNode; //sUcSLD.Pb33.Image = (Bitmap)e.Data.GetData(DataFormats.Bitmap, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb34_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb34_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb34.Image = Image.FromFile(ImagePath);
                UcSLD.Pb34.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb34.BorderStyle = BorderStyle.None;
                UcSLD.Pb34.Size = new Size(70, 70);
                UcSLD.Pb34.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb35_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb35_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb35.Image = Image.FromFile(ImagePath);
                UcSLD.Pb35.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb35.BorderStyle = BorderStyle.None;
                UcSLD.Pb35.Size = new Size(70, 70);
                UcSLD.Pb35.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb36_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb36_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb36.Image = Image.FromFile(ImagePath);
                UcSLD.Pb36.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb36.BorderStyle = BorderStyle.None;
                UcSLD.Pb36.Size = new Size(70, 70);
                UcSLD.Pb36.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb37_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb37_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb37.Image = Image.FromFile(ImagePath);
                UcSLD.Pb37.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb37.BorderStyle = BorderStyle.None;
                UcSLD.Pb37.Size = new Size(70, 70);
                UcSLD.Pb37.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb38_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb38_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb38.Image = Image.FromFile(ImagePath);
                UcSLD.Pb38.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb38.BorderStyle = BorderStyle.None;
                UcSLD.Pb38.Size = new Size(70, 70);
                UcSLD.Pb38.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb39_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb39_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb39.Image = Image.FromFile(ImagePath);
                UcSLD.Pb39.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb39.BorderStyle = BorderStyle.None;
                UcSLD.Pb39.Size = new Size(70, 70);
                UcSLD.Pb39.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pb40_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: Pb40_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb40.Image = Image.FromFile(ImagePath);
                UcSLD.Pb40.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb40.BorderStyle = BorderStyle.None;
                UcSLD.Pb40.Size = new Size(70, 70);
                UcSLD.Pb40.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb41_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb41_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb41.Image = Image.FromFile(ImagePath);
                UcSLD.Pb41.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb41.BorderStyle = BorderStyle.None;
                UcSLD.Pb41.Size = new Size(70, 70);
                UcSLD.Pb41.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb42_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb42_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb42.Image = Image.FromFile(ImagePath);
                UcSLD.Pb42.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb42.BorderStyle = BorderStyle.None;
                UcSLD.Pb42.Size = new Size(70, 70);
                UcSLD.Pb42.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Pb43_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb43_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb43.Image = Image.FromFile(ImagePath);
                UcSLD.Pb43.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb43.BorderStyle = BorderStyle.None;
                UcSLD.Pb43.Size = new Size(70, 70);
                UcSLD.Pb43.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb44_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb44_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb44.Image = Image.FromFile(ImagePath);
                UcSLD.Pb44.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb44.BorderStyle = BorderStyle.None;
                UcSLD.Pb44.Size = new Size(70, 70);
                UcSLD.Pb44.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Pb45_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb45_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb45.Image = Image.FromFile(ImagePath);
                UcSLD.Pb45.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb45.BorderStyle = BorderStyle.None;
                UcSLD.Pb45.Size = new Size(70, 70);
                UcSLD.Pb45.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb46_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb46_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb46.Image = Image.FromFile(ImagePath);
                UcSLD.Pb46.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb46.BorderStyle = BorderStyle.None;
                UcSLD.Pb46.Size = new Size(70, 70);
                UcSLD.Pb46.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb47_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb47_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb47.Image = Image.FromFile(ImagePath);
                UcSLD.Pb47.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb47.BorderStyle = BorderStyle.None;
                UcSLD.Pb47.Size = new Size(70, 70);
                UcSLD.Pb47.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb48_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb48_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb48.Image = Image.FromFile(ImagePath);
                UcSLD.Pb48.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb48.BorderStyle = BorderStyle.None;
                UcSLD.Pb48.Size = new Size(70, 70);
                UcSLD.Pb48.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb49_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb49_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb49.Image = Image.FromFile(ImagePath);
                UcSLD.Pb49.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb49.BorderStyle = BorderStyle.None;
                UcSLD.Pb49.Size = new Size(70, 70);
                UcSLD.Pb49.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Pb50_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb50_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb50.Image = Image.FromFile(ImagePath);
                UcSLD.Pb50.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb50.BorderStyle = BorderStyle.None;
                UcSLD.Pb50.Size = new Size(70, 70);
                UcSLD.Pb50.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb51_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb51_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb51.Image = Image.FromFile(ImagePath);
                UcSLD.Pb51.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb51.BorderStyle = BorderStyle.None;
                UcSLD.Pb51.Size = new Size(70, 70);
                UcSLD.Pb51.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb52_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb52_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb52.Image = Image.FromFile(ImagePath);
                UcSLD.Pb52.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb52.BorderStyle = BorderStyle.None;
                UcSLD.Pb52.Size = new Size(70, 70);
                UcSLD.Pb52.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb53_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb53_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb53.Image = Image.FromFile(ImagePath);
                UcSLD.Pb53.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb53.BorderStyle = BorderStyle.None;
                UcSLD.Pb53.Size = new Size(70, 70);
                UcSLD.Pb53.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb54_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:P54_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb54.Image = Image.FromFile(ImagePath);
                UcSLD.Pb54.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb54.BorderStyle = BorderStyle.None;
                UcSLD.Pb54.Size = new Size(70, 70);
                UcSLD.Pb54.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb55_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb55_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb55.Image = Image.FromFile(ImagePath);
                UcSLD.Pb55.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb55.BorderStyle = BorderStyle.None;
                UcSLD.Pb55.Size = new Size(70, 70);
                UcSLD.Pb55.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb56_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb56_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb56.Image = Image.FromFile(ImagePath);
                UcSLD.Pb56.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb56.BorderStyle = BorderStyle.None;
                UcSLD.Pb56.Size = new Size(70, 70);
                UcSLD.Pb56.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb57_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb57_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb57.Image = Image.FromFile(ImagePath);
                UcSLD.Pb57.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb57.BorderStyle = BorderStyle.None;
                UcSLD.Pb57.Size = new Size(70, 70);
                UcSLD.Pb57.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb58_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb58_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb58.Image = Image.FromFile(ImagePath);
                UcSLD.Pb58.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb58.BorderStyle = BorderStyle.None;
                UcSLD.Pb58.Size = new Size(70, 70);
                UcSLD.Pb58.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb59_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb59_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb59.Image = Image.FromFile(ImagePath);
                UcSLD.Pb59.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb59.BorderStyle = BorderStyle.None;
                UcSLD.Pb59.Size = new Size(70, 70);
                UcSLD.Pb59.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pb60_DragDrop(object sender, DragEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:Pb60_DragDrop";
            try
            {
                string ImagePath = Globals.Widgets_path + GDSlave.GetTreeNode + ".png";
                UcSLD.Pb60.Image = Image.FromFile(ImagePath);
                UcSLD.Pb60.SizeMode = PictureBoxSizeMode.StretchImage;
                UcSLD.Pb60.BorderStyle = BorderStyle.None;
                UcSLD.Pb60.Size = new Size(70, 70);
                UcSLD.Pb60.Tag = GDSlave.GetTreeNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion PictureBox DragDrop
        private void btnImportSLD_Click(object sender, EventArgs e)
        {
            try
            {
                GDSlave.IsSLDImport = true;
                UcSLD.SCDisplayImage.Show();
                UcSLD.SCCreateSLD.Hide();
                UcSLD.SCMain.Panel2Collapsed = true;
                //UcSLD.splitContainer4.Panel2Collapsed = false;
            }
            catch (Exception Ex)
            {

            }
        }

        private void BtnCreateSLD_Click(object sender, EventArgs e)
        {
            //UcSLD.SCCreateSLD.SplitterDistance = 256;//240;
            if (SLDSettingsList.Count >= 1)
            {
                MessageBox.Show("Maximum " + Globals.MaxSLD + " SLDSetting's are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                mode = Mode.ADD;
                editIndex = -1;
                Utils.resetValues(UcSLD.grpSLDSetting);
                Utils.showNavigation(UcSLD.grpSLDSetting, false);
                //string TableName = "GraphicalDisplaySlave_" + GDSlave.GDSlaveNo + "_" + "diagram.txt"; //GraphicalDisplaySlave_1_5x8 SingleBUS_1.txt
                //if (GDSlave.DsExcelData.Tables.Contains(TableName))
                //{

                //}
                //else { GDSlave.DsExcelData.Tables.Add(TableName); }
                UcSLD.grpSLDSetting.Hide();
                //UcSLD.SCDisplayImage.Hide();
                //UcSLD.SCMain.Panel2Collapsed = false;
                ////UcSLD.splitContainer4.Panel2Collapsed = true;
                //UcSLD.SCCreateSLD.Show();
                AddTreeNodes();
                UcSLD.SCMain.Panel2Collapsed = false;
                UcSLD.splitContainer1.Show();
                UcSLD.splitContainer1.Panel1Collapsed = true;
                UcSLD.SCCreateSLD.Hide();
                UcSLD.SCDisplayImage.Hide();
                UcSLD.TblLayoutCSLD.Show();
                //UcSLD.SCDisplayData.Show();
                UcSLD.tableLayoutPanel1.Hide();
                UcSLD.BtnSaveSLD.Visible = true;
                UcSLD.BtnSaveSLD.Location = new Point(200, 3);
                UcSLD.btnDelete.Location = new Point(286, 3);
                UcSLD.panel3.VerticalScroll.Visible = true;
                UcSLD.SCMain.SplitterDistance = 400;
                UcSLD.splitContainer1.SplitterDistance = 320;

                UcSLD.SCMain.SplitterDistance = 440;
                UcSLD.splitContainer1.SplitterDistance = 300;

                UcSLD.SCMain.SplitterDistance = 400;//;412;
                UcSLD.SCCreateSLD.SplitterDistance = 256;//240;
                UcSLD.SCCreateSLD.Show();
            }
        }
        private void addListHeaders()
        {
            UcSLD.lvSLDList.Columns.Add("Template SLD File", 200, HorizontalAlignment.Left);
            UcSLD.lvSLDList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
        }
        public void updateAttributes(List<KeyValuePair<string, string>> mbsData)
        {
            string strRoutineName = "GraphicalDisplaySlave: updateAttributes";
            try
            {
                if (mbsData != null && mbsData.Count > 0)
                {
                    foreach (KeyValuePair<string, string> mbskp in mbsData)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", mbskp.Key, mbskp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(mbskp.Key) != null) //Ajay: 03/07/2018
                            {
                                this.GetType().GetProperty(mbskp.Key).SetValue(this, mbskp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value:{1}", mbskp.Key, mbskp.Value);
                        }
                    }
                    Utils.Write(VerboseLevel.DEBUG, "\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChkSHGridLines_CheckedChanged(object sender, EventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:ChkSHGridLines_CheckedChanged";
            try
            {
                //if (UcSLD.ChkSHGridLines.Checked == true)
                //{
                //    //UcSLD.TblLayoutCSLD.Size = new Size(256, 409);
                //    UcSLD.TblLayoutCSLD.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                //}
                //else
                //{
                //    //UcSLD.TblLayoutCSLD.Size = new Size(250, 400);
                //    UcSLD.TblLayoutCSLD.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ChkGridLines_CheckedChanged(object sender, EventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:ChkGridLines_CheckedChanged";
            try
            {
                //if (UcSLD.ChkGridLines.Checked == true)
                //{
                //    UcSLD.TblLayoutImagePanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                //}
                //else
                //{
                //    UcSLD.TblLayoutImagePanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        string CurrentDTTable;
        private void PbSource_MouseDown(object sender, MouseEventArgs e)
        {
            // Start the drag if it's the right mouse button.
            if (e.Button == MouseButtons.Left)
            {
                //UcSLD.PbSource.DoDragDrop(UcSLD.PbSource.Image, DragDropEffects.Copy);
            }
        }
        private void PbTarget_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                // See if this is a copy and the data includes an image.
                if (e.Data.GetDataPresent(DataFormats.Bitmap) &&
                    (e.AllowedEffect & DragDropEffects.Copy) != 0)
                {
                    //UcSLD.PbTarget.Width = 50;
                    //UcSLD.PbTarget.Height = 50;
                    //UcSLD.PbTarget.SizeMode = PictureBoxSizeMode.AutoSize;
                    //UcSLD.PbTarget.Dock = DockStyle.Fill;
                    // Allow this.
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    // Don't allow any other drop.
                    e.Effect = DragDropEffects.None;
                }
            }
            catch (Exception Ex)
            {

            }
        }
        private void PbTarget_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                //UcSLD.PbTarget.Width = 50;
                //UcSLD.PbTarget.Height = 50;
                //UcSLD.PbTarget.SizeMode = PictureBoxSizeMode.AutoSize;
                //UcSLD.PbTarget.Dock = DockStyle.Fill;
                //UcSLD.PbTarget.Image = (Bitmap)e.Data.GetData(DataFormats.Bitmap, true);
            }
            catch (Exception Ex)
            {

            }
        }
        private void PictureBoxAllowDropEvents()
        {
            #region PictureBox Events
            UcSLD.Pb1.AllowDrop = true;
            UcSLD.Pb2.AllowDrop = true;
            UcSLD.Pb3.AllowDrop = true;
            UcSLD.Pb4.AllowDrop = true;
            UcSLD.Pb5.AllowDrop = true;
            UcSLD.Pb6.AllowDrop = true;
            UcSLD.Pb7.AllowDrop = true;
            UcSLD.Pb8.AllowDrop = true;
            UcSLD.Pb9.AllowDrop = true;
            UcSLD.Pb10.AllowDrop = true;
            UcSLD.Pb11.AllowDrop = true;
            UcSLD.Pb12.AllowDrop = true;
            UcSLD.Pb13.AllowDrop = true;
            UcSLD.Pb14.AllowDrop = true;
            UcSLD.Pb15.AllowDrop = true;
            UcSLD.Pb16.AllowDrop = true;
            UcSLD.Pb17.AllowDrop = true;
            UcSLD.Pb18.AllowDrop = true;
            UcSLD.Pb19.AllowDrop = true;
            UcSLD.Pb20.AllowDrop = true;
            UcSLD.Pb21.AllowDrop = true;
            UcSLD.Pb22.AllowDrop = true;
            UcSLD.Pb23.AllowDrop = true;
            UcSLD.Pb24.AllowDrop = true;
            UcSLD.Pb25.AllowDrop = true;
            UcSLD.Pb26.AllowDrop = true;
            UcSLD.Pb27.AllowDrop = true;
            UcSLD.Pb28.AllowDrop = true;
            UcSLD.Pb29.AllowDrop = true;
            UcSLD.Pb30.AllowDrop = true;
            UcSLD.Pb31.AllowDrop = true;
            UcSLD.Pb32.AllowDrop = true;
            UcSLD.Pb33.AllowDrop = true;
            UcSLD.Pb34.AllowDrop = true;
            UcSLD.Pb35.AllowDrop = true;
            UcSLD.Pb36.AllowDrop = true;
            UcSLD.Pb37.AllowDrop = true;
            UcSLD.Pb38.AllowDrop = true;
            UcSLD.Pb39.AllowDrop = true;
            UcSLD.Pb40.AllowDrop = true;

            UcSLD.Pb41.AllowDrop = true;
            UcSLD.Pb42.AllowDrop = true;
            UcSLD.Pb43.AllowDrop = true;
            UcSLD.Pb44.AllowDrop = true;
            UcSLD.Pb45.AllowDrop = true;
            UcSLD.Pb46.AllowDrop = true;
            UcSLD.Pb47.AllowDrop = true;
            UcSLD.Pb48.AllowDrop = true;
            UcSLD.Pb49.AllowDrop = true;
            UcSLD.Pb50.AllowDrop = true;
            UcSLD.Pb51.AllowDrop = true;
            UcSLD.Pb52.AllowDrop = true;
            UcSLD.Pb53.AllowDrop = true;
            UcSLD.Pb54.AllowDrop = true;
            UcSLD.Pb55.AllowDrop = true;
            UcSLD.Pb56.AllowDrop = true;
            UcSLD.Pb57.AllowDrop = true;
            UcSLD.Pb58.AllowDrop = true;
            UcSLD.Pb59.AllowDrop = true;
            UcSLD.Pb60.AllowDrop = true;
            #endregion PictureBox Events
        }
        private void ucGroupSLD_Load(object sender, EventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:ucGroupSLD_Load";
            try
            {
                UcSLD.panel3.VerticalScroll.Visible = true;
                UcSLD.TblLayoutCSLD.Size = new Size(420, 700);
                UcSLD.btnDelete.Location = new Point(200, 3);
                if (operation == Operations.NONE)
                {
                    UcSLD.SCDisplayImage.Hide();
                    UcSLD.SCCreateSLD.Hide();
                    UcSLD.SCMain.Panel2Collapsed = true;
                    //UcSLD.splitContainer4.Panel2Collapsed = true;
                }
                UcSLD.TblLayoutCSLD.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

                if (GDSlave.DsExcelData.Tables.Count > 0)
                {
                    List<string> tblNameList = GDSlave.DsExcelData.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                    CurrentDTTable = tblNameList.Where(x => x.Contains(GDSlave.CurSlave)).Select(x => x).FirstOrDefault();
                }
                if (GDSlave.DsExcelData.Tables.Contains(CurrentDTTable))
                {
                    AddTreeNodes();
                    UcSLD.SCMain.Panel2Collapsed = false;
                    UcSLD.splitContainer1.Show();
                    UcSLD.splitContainer1.Panel1Collapsed = true;
                    UcSLD.SCCreateSLD.Hide();
                    UcSLD.SCDisplayImage.Hide();
                    UcSLD.TblLayoutCSLD.Show();
                    //UcSLD.SCDisplayData.Show();
                    UcSLD.tableLayoutPanel1.Hide();
                    UcSLD.BtnSaveSLD.Visible = true;
                    UcSLD.BtnSaveSLD.Location = new Point(200, 3);
                    UcSLD.btnDelete.Location = new Point(286, 3);
                    UcSLD.panel3.VerticalScroll.Visible = true;
                    UcSLD.SCMain.SplitterDistance = 400;
                    UcSLD.splitContainer1.SplitterDistance = 320;

                    UcSLD.SCMain.SplitterDistance = 440;
                    UcSLD.splitContainer1.SplitterDistance = 300;

                    UcSLD.SCMain.SplitterDistance = 400;//;412;
                    UcSLD.SCCreateSLD.SplitterDistance = 256;//240;
                    UcSLD.SCCreateSLD.Show();
                    //UcSLD.tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                    //UcSLD.SCMain.Panel2Collapsed = false;
                    //UcSLD.splitContainer1.Show();
                    //UcSLD.splitContainer1.Panel2Collapsed = true;
                    //UcSLD.SCDisplayImage.Show();
                    //UcSLD.tableLayoutPanel1.Hide();
                    //UcSLD.label3.Hide();
                    //UcSLD.tableLayoutPanel1.Show();
                    //UcSLD.splitContainer1.Panel2Collapsed = true;
                }
                else
                {
                    //UcSLD.ChkGridLines.Hide();
                    //UcSLD.TblLayoutImagePanel1.Hide();
                    //UcSLD.lblSLD.Hide();
                    //UcSLD.lblFileName.Hide();
                    //UcSLD.lblFilePath.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PCDFile_Click(object sender, EventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave: PCDFile_Click";
            try
            {
                string filePath = string.Empty;
                string fileExt = string.Empty;
                openFileDialog1.AddExtension = true;
                openFileDialog1.InitialDirectory = Application.ExecutablePath;
                openFileDialog1.FileName = "";
                openFileDialog1.DefaultExt = ".txt";
                openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) //if there is a file choosen by the user
                {
                    filePath = openFileDialog1.FileName; //C:\Users\namrata\Desktop\5x8 SingleBUS_1.txt
                    fileExt = Path.GetExtension(filePath); //.txt
                    string FileName = Path.GetFileName(filePath); //5x8 SingleBUS_1.txt
                    string TableName = "GraphicalDisplaySlave_" + GDSlave.GDSlaveNo + "_" + Path.GetFileName(FileName); //GraphicalDisplaySlave_1_5x8 SingleBUS_1.txt
                    GDSlave.SlaveFolder = "GraphicalDisplaySlave_" + GDSlave.GDSlaveNo;
                    ConvertTextFileToDS(filePath, TableName, ",");
                    UcSLD.txtTemplateSLDFile.Text = Path.GetFileName(filePath);
                    UcSLD.txtTemplateSLDFile.Focus();
                    GDSlave.XLSFileName = Path.GetFileName(filePath);//5x8 SingleBUS_1.txt
                    GDSlave.FileFullPath = filePath; //C:\Users\namrata\Desktop\5x8 SingleBUS_1.txt


                    //Namrata: 20/11/2019
                    //AddTreeNodes();
                    //UcSLD.SCMain.Panel2Collapsed = false;
                    //UcSLD.splitContainer1.Show();
                    //UcSLD.splitContainer1.Panel1Collapsed = true;
                    //UcSLD.SCCreateSLD.Hide();
                    //UcSLD.SCDisplayImage.Hide();
                    //UcSLD.TblLayoutCSLD.Show();
                    ////UcSLD.SCDisplayData.Show();
                    //UcSLD.tableLayoutPanel1.Hide();
                    //UcSLD.BtnSaveSLD.Visible = true;
                    //UcSLD.BtnSaveSLD.Location = new Point(200, 3);
                    //UcSLD.btnDelete.Location = new Point(286, 3);
                    //UcSLD.panel3.VerticalScroll.Visible = true;
                    //UcSLD.SCMain.SplitterDistance = 400;
                    //UcSLD.splitContainer1.SplitterDistance = 320;

                    //UcSLD.SCMain.SplitterDistance = 440;
                    //UcSLD.splitContainer1.SplitterDistance = 300;

                    //UcSLD.SCMain.SplitterDistance = 400;//;412;
                    //UcSLD.SCCreateSLD.SplitterDistance = 256;//240;
                    //UcSLD.SCCreateSLD.Show();

                    //Create Directory to store .xls files
                    Directory.CreateDirectoryForGraphicalDisplay(FileName, filePath);
                    Directory.CreateDirectoryForWidgets(FileName, filePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static DataSet ConvertTextFileToDS(string File, string TableName, string delimiter)
        {
            StreamReader s = new StreamReader(File);
            //GDSlave.DsExcelData.Tables.Add(TableName);

            //Namrata: 22/11/2019
            if(GDSlave.DsExcelData.Tables.Contains(TableName))
            {
                GDSlave.DsExcelData.Tables.Remove(TableName);
                GDSlave.DsExcelData.Tables.Add(TableName);
            }
            else
            {
                GDSlave.DsExcelData.Tables.Add(TableName);
            }

            #region Add Columns
            if (!GDSlave.DsExcelData.Tables[TableName].Columns.Contains("CellNo"))
            {
                GDSlave.DsExcelData.Tables[TableName].Columns.Add("CellNo", typeof(object));
            }
            if (!GDSlave.DsExcelData.Tables[TableName].Columns.Contains("Widget"))
            {
                GDSlave.DsExcelData.Tables[TableName].Columns.Add("Widget", typeof(string));
            }
            if (!GDSlave.DsExcelData.Tables[TableName].Columns.Contains("Type"))
            {
                GDSlave.DsExcelData.Tables[TableName].Columns.Add("Type", typeof(string));
            }
            if (!GDSlave.DsExcelData.Tables[TableName].Columns.Contains("Name"))
            {
                GDSlave.DsExcelData.Tables[TableName].Columns.Add("Name", typeof(string));
            }
            if (!GDSlave.DsExcelData.Tables[TableName].Columns.Contains("DBIndex"))
            {
                GDSlave.DsExcelData.Tables[TableName].Columns.Add("DBIndex", typeof(string));
            }
            if (!GDSlave.DsExcelData.Tables[TableName].Columns.Contains("Unit"))
            {
                GDSlave.DsExcelData.Tables[TableName].Columns.Add("Unit", typeof(string));
            }
            if (!GDSlave.DsExcelData.Tables[TableName].Columns.Contains("Configuration"))
            {
                GDSlave.DsExcelData.Tables[TableName].Columns.Add("Configuration", typeof(string));
            }
            #endregion Add Columns
            string AllData = s.ReadToEnd(); //Read the rest of the data in the file.
            string[] rows = AllData.Split("\r\n".ToCharArray());
            rows = rows.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            foreach (string str in rows)
            {
                string[] tokens = str.Split(',');
                DataRow drow = GDSlave.DsExcelData.Tables[TableName].NewRow();
                drow["CellNo"] = tokens[0].ToString();
                drow["Widget"] = tokens[1].ToString();
                drow["Type"] = tokens[2].ToString();
                drow["Name"] = tokens[3].ToString();
                drow["DBIndex"] = tokens[4].ToString();
                drow["Unit"] = tokens[5].ToString();
                drow["Configuration"] = drow[0] + "," + drow[1] + "," + drow[2] + "," + drow[3] + "," + drow[4] + "," + drow[5];
                GDSlave.DsExcelData.Tables[TableName].Rows.Add(drow);
            }
            return GDSlave.DsExcelData;
        }
        public static DataSet ConvertTextFileToDS1(string File, string TableName, string delimiter)
        {
            DataSet Ds = new DataSet();
            StreamReader s = new StreamReader(File);
            Ds.Tables.Add(TableName);

            #region Add Columns
            if (!Ds.Tables[TableName].Columns.Contains("CellNo"))
            {
                Ds.Tables[TableName].Columns.Add("CellNo", typeof(object));
            }
            if (!Ds.Tables[TableName].Columns.Contains("Widget"))
            {
                Ds.Tables[TableName].Columns.Add("Widget", typeof(string));
            }
            if (!Ds.Tables[TableName].Columns.Contains("Type"))
            {
                Ds.Tables[TableName].Columns.Add("Type", typeof(string));
            }
            if (!Ds.Tables[TableName].Columns.Contains("Name"))
            {
                Ds.Tables[TableName].Columns.Add("Name", typeof(string));
            }
            if (!Ds.Tables[TableName].Columns.Contains("DBIndex"))
            {
                Ds.Tables[TableName].Columns.Add("DBIndex", typeof(string));
            }
            if (!Ds.Tables[TableName].Columns.Contains("Unit"))
            {
                Ds.Tables[TableName].Columns.Add("Unit", typeof(string));
            }
            if (!Ds.Tables[TableName].Columns.Contains("Configuration"))
            {
                Ds.Tables[TableName].Columns.Add("Configuration", typeof(string));
            }
            #endregion Add Columns
            string AllData = s.ReadToEnd(); //Read the rest of the data in the file.
            string[] rows = AllData.Split("\r\n".ToCharArray());
            rows = rows.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            foreach (string str in rows)
            {
                string[] tokens = str.Split(',');
                DataRow drow = Ds.Tables[TableName].NewRow();
                drow["CellNo"] = tokens[0].ToString();
                drow["Widget"] = tokens[1].ToString();
                drow["Type"] = tokens[2].ToString();
                drow["Name"] = tokens[3].ToString();
                drow["DBIndex"] = tokens[4].ToString();
                drow["Unit"] = tokens[5].ToString();
                drow["Configuration"] = drow[0] + "," + drow[1] + "," + drow[2] + "," + drow[3] + "," + drow[4] + "," + drow[5];
                Ds.Tables[TableName].Rows.Add(drow);
            }
            GDSlave.DsExcelData = Ds;
            return Ds;
        }
        private void lvSLDList_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:lvSLDList_DoubleClick";
            try
            {
                if (UcSLD.lvSLDList.SelectedItems.Count <= 0) return;
                ListViewItem lvi = UcSLD.lvSLDList.SelectedItems[0];
                Utils.UncheckOthers(UcSLD.lvSLDList, lvi.Index);
                if (SLDSettingsList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                UcSLD.grpSLDSetting.Visible = true;
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(UcSLD.grpSLDSetting, true);
                loadValues();
                UcSLD.txtSlaveNum.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvSLDList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:lvSLDList_ItemCheck";
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    UcSLD.lvSLDList.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
                    {
                        item.Checked = false;
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadValues()
        {
            string strRoutineName = "GraphicalDisplaySlave:loadValues";
            try
            {
                SLDSettings SLD = SLDSettingsList.ElementAt(editIndex);
                if (SLD != null)
                {
                    UcSLD.txtTemplateSLDFile.Text = SLD.TemplateSLDFile;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:btnAdd_Click";
            try
            {
                if (SLDSettingsList.Count >= Globals.MaxSLD)
                {
                    MessageBox.Show("Maximum " + Globals.MaxSLD + " SLDSetting's are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                mode = Mode.ADD;
                editIndex = -1;
                UcSLD.txtTemplateSLDFile.Enabled = true;
                UcSLD.PCDFile.Visible = true;
                Utils.resetValues(UcSLD.grpSLDSetting);
                Utils.showNavigation(UcSLD.grpSLDSetting, false);
                //loadDefaults();
                //AddTreeNodes();
                UcSLD.grpSLDSetting.Visible = true;
                //Namrata:14/10/2019

                GDSlave.IsSLDImport = true;
                UcSLD.BtnSaveSLD.Visible = false;
                UcSLD.btnDelete.Location = new Point(200, 3);

                UcSLD.SCMain.Panel2Collapsed = true;
                UcSLD.splitContainer1.Show();
                //UcSLD.splitContainer1.Panel1Collapsed = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadDefaults()
        {
            string strRoutineName = "GraphicalDisplaySlave:loadDefaults";
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void ClearDataOnBtnCancel_Click()
        {
            string FileName = Path.GetFileName(GDSlave.XLSDirFile);
            Directory.DeleteDirectory(GDSlave.GDSlaveXlsFiles, GDSlave.XLSFileName);

            #region Clear Excel Data
            List<string> DsIEDList = GDSlave.DsExcelData.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
            DsIEDList.Select(x => x).Where(x => x.Contains("GraphicalDisplaySlave_" + GDSlave.GDSlaveNo)).ToList().ForEach(tbl =>
            {
                if (GDSlave.DsExcelData.Tables.Contains(tbl))
                {
                    GDSlave.DsExcelData.Tables.Remove(tbl);
                }
            });
            #endregion Clear Excel Data
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:btnCancel_Click";
            try
            {
                if (mode == Mode.ADD)
                {
                    ClearDataOnBtnCancel_Click();
                }
                UcSLD.grpSLDSetting.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                UcSLD.grpSLDSetting.Visible = false;
                Utils.resetValues(UcSLD.grpSLDSetting);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:btnDelUFSMS.lvUFSMSListete_Click";
            try
            {
                if (UcSLD.lvSLDList.Items.Count == 0)
                {
                    MessageBox.Show("Please add atleast one SLD ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (UcSLD.lvSLDList.CheckedItems.Count == 1)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete " + UcSLD.lvSLDList.CheckedItems[0].Text + " ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        int iIndex = UcSLD.lvSLDList.CheckedItems[0].Index;
                        if (result == DialogResult.Yes)
                        {
                            SLDSettingsList.RemoveAt(iIndex);
                            UcSLD.lvSLDList.Items[iIndex].Remove();
                            Globals.TotalSlavesCount -= 1; //Ajay: 25/08/2018

                            #region Clear Excel Data
                            List<string> DsIEDList = GDSlave.DsExcelData.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                            DsIEDList.Select(x => x).Where(x => x.Contains("GraphicalDisplaySlave_" + GDSlave.GDSlaveNo)).ToList().ForEach(tbl =>
                            {
                                if (GDSlave.DsExcelData.Tables.Contains(tbl))
                                {
                                    GDSlave.DsExcelData.Tables.Remove(tbl);
                                }
                            });

                            UcSLD.tableLayoutPanel1.Controls.Clear();
                            UcSLD.tableLayoutPanel1.Hide();
                            //UcSLD.lblFileName.Hide();
                            //UcSLD.lblFilePath.Hide();
                            //UcSLD.lblSLD.Hide();
                            #endregion Clear Excel Data
                        }
                        else
                        {
                            return;
                        }
                        if (GDSlave.DsExcelData.Tables.Count > 0)
                        {
                            GDSlave.GDisplaySlave = true;
                        }
                        else
                        {
                            GDSlave.GDisplaySlave = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select atleast one Slave ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    refreshList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void refreshList()
        {
            string strRoutineName = "GraphicalDisplaySlave:refreshList";
            try
            {
                int cnt = 0;
                UcSLD.lvSLDList.Items.Clear();
                foreach (SLDSettings sld in SLDSettingsList)
                {
                    string[] row = new string[1];
                    if (sld.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = sld.TemplateSLDFile;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    UcSLD.lvSLDList.Items.Add(lvItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool Validate()
        {
            string strRoutineName = "GraphicalDisplaySlave: Validate";

            bool status = true;
            if (UcSLD.txtTemplateSLDFile.Text == "")
            {
                MessageBox.Show("Please import .csv file!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return status;
        }

        public static List<string> ListDIDO = new List<string>();
        private void btnDone_Click(object sender, EventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:btnDone_Click";
            try
            {
                List<KeyValuePair<string, string>> mbsData = Utils.getKeyValueAttributes(UcSLD.grpSLDSetting);
                if (!Validate()) return;
                if (mode == Mode.ADD)
                {
                    TreeNode tmp = null;
                    SLDSettingsList.Add(new SLDSettings("SLDSettings", mbsData, tmp, Int32.Parse(SlaveNum)));
                }
                else if (mode == Mode.EDIT)
                {
                    SLDSettingsList[editIndex].updateAttributes(mbsData);
                }
                refreshList();
                if (sender != null && e != null)
                {
                    UcSLD.grpSLDSetting.Visible = false;
                    mode = Mode.NONE;
                    editIndex = -1;
                }
                if (GDSlave.DsExcelData.Tables.Count > 0)
                {
                    if (GDSlave.IsSLDImport)
                    {
                        #region IsSLDImport
                        //UcSLD.SCMain.Panel2Collapsed = false;
                        //UcSLD.splitContainer1.Show();
                        //UcSLD.splitContainer1.Panel2Collapsed = true;
                        //UcSLD.SCDisplayImage.Show();
                        ////UcSLD.SCDisplayData.Show();
                        //UcSLD.tableLayoutPanel1.Hide();
                        GDSlave.GDisplaySlave = true;
                        UcSLD.TblLayoutCSLD.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                        UcSLD.TblLayoutCSLD.Size = new System.Drawing.Size(420, 700);
                        List<string> tblNameList = GDSlave.DsExcelData.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                        string CurrentSlave = tblNameList.Where(x => x.Contains(GDSlave.CurSlave)).Select(x => x).FirstOrDefault();
                        List<DataRow> ListCurrentSlave = GDSlave.DsExcelData.Tables[CurrentSlave].AsEnumerable().ToList();

                        DataView view = new DataView(GDSlave.DsExcelData.Tables[CurrentSlave]);
                        DataTable distinctValues = new DataTable();
                        distinctValues = view.ToTable(true, "CellNo", "Widget");
                        //Namrata: 16/09/2019
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "Images|*.png;*.bmp;*.jpg";
                        System.Drawing.Imaging.ImageFormat format = ImageFormat.Png;
                        int iFileRow = 0; int iColumn = 0; int PBname = 1; int iRow = 0;
                        ImageConverter converter = new ImageConverter();
                        //pb.Margin = new Padding(0, 0, 0, 0);
                        UcSLD.TblLayoutCSLD.Controls.OfType<PictureBox>().ToList().ForEach(x =>
                        {
                            string Widget = distinctValues.Rows[iFileRow]["Widget"].ToString();
                            x.Name = "Pb" + PBname;
                            x.Size = new Size(70, 70);
                            x.Tag = Widget;
                            //UcSLD.Pb1.Tag = GDSlave.GetTreeNode;
                            object o = Properties.Resources.ResourceManager.GetObject(Widget);
                            string ImagePath1 = Globals.Widgets_path + Widget + ".png";
                            x.Image = Image.FromFile(ImagePath1);
                            x.SizeMode = PictureBoxSizeMode.StretchImage;
                            UcSLD.TblLayoutCSLD.SetColumn(x, iColumn);
                            UcSLD.TblLayoutCSLD.SetRow(x, iRow);
                            #region Save Widgets On DirectoryPath
                            //Namrata: 16/09/2019
                            Bitmap bitmap = new Bitmap(x.Image);
                            Graphics gr = Graphics.FromImage(bitmap);
                            gr.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
                           
                            #endregion Save Widgets On DirectoryPath
                            PBname++;
                            if (iFileRow < distinctValues.Rows.Count - 1)
                            { iFileRow++; }
                            else { }
                            if (iColumn == 5)
                            {
                                iColumn = 0; iRow++;
                            }
                            else
                            {
                                iColumn++;
                            }
                        });


                        var filters = new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp", "svg" };
                        var files = GetFilesFrom1(Globals.Widgets_path, filters, false);
                        PictureBox pbW = new PictureBox();
                        ImageConverter converterW = new ImageConverter();
                        for (int i = 0; i < files.Count(); i++)
                        {
                            string WidgetPath = files[i];
                            pbW.Image = Image.FromFile(WidgetPath);
                            Bitmap bitmap = new Bitmap(pbW.Image);
                            Graphics gr = Graphics.FromImage(bitmap);
                            gr.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);

                            string WidgetPaths = files[i];
                            string WName = WidgetPaths.Split('\\').Last();
                            string ImagePathW = GDSlave.WidgetsPath + @"\" + WName;
                            bitmap.Save(ImagePathW, ImageFormat.Png);
                        }

                        SaveWidgetsInDirectory();
                        //UcSLD.SCMain.Panel2Collapsed = false;
                        //UcSLD.splitContainer1.Show();
                        //UcSLD.splitContainer1.Panel2Collapsed = true;
                        //UcSLD.SCDisplayImage.Show();
                        ////UcSLD.SCDisplayData.Show();
                        //UcSLD.tableLayoutPanel1.Hide();
                        //Namrata: 20/11/2019
                        AddTreeNodes();
                        UcSLD.SCMain.Panel2Collapsed = false;
                        UcSLD.splitContainer1.Show();
                        UcSLD.splitContainer1.Panel1Collapsed = true;
                        UcSLD.SCCreateSLD.Hide();
                        UcSLD.SCDisplayImage.Hide();
                        UcSLD.TblLayoutCSLD.Show();
                        //UcSLD.SCDisplayData.Show();
                        UcSLD.tableLayoutPanel1.Hide();
                        UcSLD.BtnSaveSLD.Visible = true;
                        UcSLD.BtnSaveSLD.Location = new Point(200, 3);
                        UcSLD.btnDelete.Location = new Point(286, 3);
                        UcSLD.panel3.VerticalScroll.Visible = true;
                        UcSLD.SCMain.SplitterDistance = 400;
                        UcSLD.splitContainer1.SplitterDistance = 320;

                        UcSLD.SCMain.SplitterDistance = 440;
                        UcSLD.splitContainer1.SplitterDistance = 300;

                        UcSLD.SCMain.SplitterDistance = 400;//;412;
                        UcSLD.SCCreateSLD.SplitterDistance = 256;//240;
                        UcSLD.SCCreateSLD.Show();
                        //UcSLD.tableLayoutPanel1.Show();
                        //UcSLD.SCMain.SplitterDistance = 630;
                        //UcSLD.splitContainer1.SplitterDistance = 400;
                        //UcSLD.splitContainer1.Panel2Collapsed = true;
                        //UcSLD.panel1.Show();
                        //UcSLD.label3.Show(); UcSLD.RTxtFilePath.Show();
                        UcSLD.RTxtFilePath.Text = GDSlave.FileFullPath;
                    }
                    #endregion IsSLDImport
                    else
                    {
                        GDSlave.GDisplaySlave = true;
                        UcSLD.tableLayoutPanel1.Hide();
                        List<string> tblNameList = GDSlave.DsExcelData.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                        string CurrentSlave = tblNameList.Where(x => x.Contains(GDSlave.CurSlave)).Select(x => x).FirstOrDefault();
                        List<DataRow> ListCurrentSlave = GDSlave.DsExcelData.Tables[CurrentSlave].AsEnumerable().ToList();

                        DataView view = new DataView(GDSlave.DsExcelData.Tables[CurrentSlave]);
                        DataTable distinctValues = new DataTable();
                        distinctValues = view.ToTable(true, "CellNo", "Widget");
                        //Namrata: 16/09/2019
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "Images|*.png;*.bmp;*.jpg";
                        System.Drawing.Imaging.ImageFormat format = ImageFormat.Png;
                        int iWidget = 0; int iColumn = 0;
                        for (int iRow = 0; iRow < distinctValues.Rows.Count; iRow++)
                        {
                            string Widget = distinctValues.Rows[iWidget]["Widget"].ToString();
                            PictureBox pb = new PictureBox();
                            ImageConverter converter = new ImageConverter();
                            pb.Margin = new Padding(0, 0, 0, 0);
                            pb.Size = new Size(70, 70);
                            string ImagePath12 = Globals.Widgets_path + Widget + ".png";
                            pb.Image = Image.FromFile(ImagePath12);
                            pb.SizeMode = PictureBoxSizeMode.StretchImage;
                            pb.Dock = DockStyle.Fill;
                            #region Save Widgets On DirectoryPath




                            #endregion Save Widgets On DirectoryPath
                            if (iWidget < distinctValues.Rows.Count - 1)
                            { iWidget++; }
                            else { }
                            if (iColumn == 5)
                            {
                                iColumn = 0;
                            }
                        }



                        var filters = new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp", "svg" };
                        var files = GetFilesFrom1(Globals.Widgets_path, filters, false);
                        PictureBox pbW = new PictureBox();
                        ImageConverter converterW = new ImageConverter();
                        for (int i = 0; i < files.Count(); i++)
                        {
                            string WidgetPath = files[i];
                            pbW.Image = Image.FromFile(WidgetPath);
                            Bitmap bitmap = new Bitmap(pbW.Image);
                            Graphics gr = Graphics.FromImage(bitmap);
                            gr.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);

                            string WidgetPaths = files[i];
                            string WName = WidgetPaths.Split('\\').Last();
                            string ImagePathW = GDSlave.WidgetsPath + @"\" + WName;
                            bitmap.Save(ImagePathW, ImageFormat.Png);
                        }
                        SaveWidgetsInDirectory();
                        UcSLD.TblLayoutCSLD.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                        //UcSLD.TblLayoutCSLD.Size = new System.Drawing.Size(420, 700);
                    }
                }
                else
                {
                    GDSlave.GDisplaySlave = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //private void btnDone_Click(object sender, EventArgs e)
        //{
        //    string strRoutineName = "GraphicalDisplaySlave:btnDone_Click";
        //    try
        //    {
        //        List<KeyValuePair<string, string>> mbsData = Utils.getKeyValueAttributes(UcSLD.grpSLDSetting);
        //        if (!Validate()) return;
        //        if (mode == Mode.ADD)
        //        {
        //            TreeNode tmp = null;
        //            SLDSettingsList.Add(new SLDSettings("SLDSettings", mbsData, tmp, Int32.Parse(SlaveNum)));
        //        }
        //        else if (mode == Mode.EDIT)
        //        {
        //            SLDSettingsList[editIndex].updateAttributes(mbsData);
        //        }
        //        refreshList();
        //        if (sender != null && e != null)
        //        {
        //            UcSLD.grpSLDSetting.Visible = false;
        //            mode = Mode.NONE;
        //            editIndex = -1;
        //        }
        //        if (GDSlave.DsExcelData.Tables.Count > 0)
        //        {
        //            if (GDSlave.IsSLDImport)
        //            {

        //                #region IsSLDImport
        //                UcSLD.SCMain.Panel2Collapsed = false;
        //                UcSLD.splitContainer1.Show();
        //                UcSLD.splitContainer1.Panel2Collapsed = true;
        //                UcSLD.SCDisplayImage.Show();
        //                //UcSLD.SCDisplayData.Show();
        //                UcSLD.tableLayoutPanel1.Hide();
        //                GDSlave.GDisplaySlave = true;
        //                UcSLD.tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        //                UcSLD.tableLayoutPanel1.Size = new System.Drawing.Size(420, 700);
        //                List<string> tblNameList = GDSlave.DsExcelData.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
        //                string CurrentSlave = tblNameList.Where(x => x.Contains(GDSlave.CurSlave)).Select(x => x).FirstOrDefault();
        //                List<DataRow> ListCurrentSlave = GDSlave.DsExcelData.Tables[CurrentSlave].AsEnumerable().ToList();

        //                DataView view = new DataView(GDSlave.DsExcelData.Tables[CurrentSlave]);
        //                DataTable distinctValues = new DataTable();
        //                distinctValues = view.ToTable(true, "CellNo", "Widget");
        //                //Namrata: 16/09/2019
        //                SaveFileDialog sfd = new SaveFileDialog();
        //                sfd.Filter = "Images|*.png;*.bmp;*.jpg";
        //                System.Drawing.Imaging.ImageFormat format = ImageFormat.Png;
        //                int iFileRow = 0; int iColumn = 0; int PBname = 1; int iRow = 0;
        //                ImageConverter converter = new ImageConverter();
        //                //pb.Margin = new Padding(0, 0, 0, 0);
        //                UcSLD.tableLayoutPanel1.Controls.OfType<PictureBox>().ToList().ForEach(x =>
        //                {
        //                    string Widget = distinctValues.Rows[iFileRow]["Widget"].ToString();
        //                    x.Name = "PicBox" + PBname;
        //                    x.Size = new Size(70, 70);
        //                    object o = Properties.Resources.ResourceManager.GetObject(Widget);
        //                    string ImagePath1 = Globals.Widgets_path + Widget + ".png";
        //                    x.Image = Image.FromFile(ImagePath1);
        //                    x.SizeMode = PictureBoxSizeMode.StretchImage;
        //                    UcSLD.tableLayoutPanel1.SetColumn(x, iColumn);
        //                    UcSLD.tableLayoutPanel1.SetRow(x, iRow);
        //                    #region Save Widgets On DirectoryPath
        //                    //Namrata: 16/09/2019
        //                    Bitmap bitmap = new Bitmap(x.Image);
        //                    Graphics gr = Graphics.FromImage(bitmap);
        //                    gr.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
        //                    //string FileExt = ".png";
        //                    //switch (FileExt)
        //                    //{
        //                    //    case ".jpg":
        //                    //        format = ImageFormat.Jpeg;
        //                    //        break;
        //                    //    case ".bmp":
        //                    //        format = ImageFormat.Bmp;
        //                    //        break;
        //                    //    case ".png":
        //                    //        format = ImageFormat.Png;
        //                    //        break;
        //                    //}
        //                    //string ImagePath = GDSlave.WidgetsPath + @"\" + Widget + ".png";
        //                    //bitmap.Save(ImagePath, ImageFormat.Png);

        //                    #endregion Save Widgets On DirectoryPath
        //                    PBname++;
        //                    if (iFileRow < distinctValues.Rows.Count - 1)
        //                    { iFileRow++; }
        //                    else { }
        //                    if (iColumn == 5)
        //                    {
        //                        iColumn = 0; iRow++;
        //                    }
        //                    else
        //                    {
        //                        iColumn++;
        //                    }
        //                });


        //                var filters = new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp", "svg" };
        //                var files = GetFilesFrom1(Globals.Widgets_path, filters, false);
        //                PictureBox pbW = new PictureBox();
        //                ImageConverter converterW = new ImageConverter();
        //                for (int i = 0; i < files.Count(); i++)
        //                {
        //                    string WidgetPath = files[i];
        //                    pbW.Image = Image.FromFile(WidgetPath);
        //                    Bitmap bitmap = new Bitmap(pbW.Image);
        //                    Graphics gr = Graphics.FromImage(bitmap);
        //                    gr.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);

        //                    string WidgetPaths = files[i];
        //                    string WName = WidgetPaths.Split('\\').Last();
        //                    string ImagePathW = GDSlave.WidgetsPath + @"\" + WName;
        //                    bitmap.Save(ImagePathW, ImageFormat.Png);
        //                }

        //                SaveWidgetsInDirectory();
        //                //UcSLD.SCMain.Panel2Collapsed = false;
        //                //UcSLD.splitContainer1.Show();
        //                //UcSLD.splitContainer1.Panel2Collapsed = true;
        //                //UcSLD.SCDisplayImage.Show();
        //                ////UcSLD.SCDisplayData.Show();
        //                //UcSLD.tableLayoutPanel1.Hide();

        //                UcSLD.tableLayoutPanel1.Show();
        //                UcSLD.SCMain.SplitterDistance = 630;
        //                UcSLD.splitContainer1.SplitterDistance = 400;
        //                UcSLD.splitContainer1.Panel2Collapsed = true;
        //                UcSLD.panel1.Show();
        //                UcSLD.label3.Show(); UcSLD.RTxtFilePath.Show();
        //                UcSLD.RTxtFilePath.Text = GDSlave.FileFullPath;
        //            }
        //            #endregion IsSLDImport
        //            else
        //            {
        //                GDSlave.GDisplaySlave = true;
        //                UcSLD.tableLayoutPanel1.Hide();
        //                List<string> tblNameList = GDSlave.DsExcelData.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
        //                string CurrentSlave = tblNameList.Where(x => x.Contains(GDSlave.CurSlave)).Select(x => x).FirstOrDefault();
        //                List<DataRow> ListCurrentSlave = GDSlave.DsExcelData.Tables[CurrentSlave].AsEnumerable().ToList();

        //                DataView view = new DataView(GDSlave.DsExcelData.Tables[CurrentSlave]);
        //                DataTable distinctValues = new DataTable();
        //                distinctValues = view.ToTable(true, "CellNo", "Widget");
        //                //Namrata: 16/09/2019
        //                SaveFileDialog sfd = new SaveFileDialog();
        //                sfd.Filter = "Images|*.png;*.bmp;*.jpg";
        //                System.Drawing.Imaging.ImageFormat format = ImageFormat.Png;
        //                int iWidget = 0; int iColumn = 0;
        //                for (int iRow = 0; iRow < distinctValues.Rows.Count; iRow++)
        //                {
        //                    string Widget = distinctValues.Rows[iWidget]["Widget"].ToString();
        //                    PictureBox pb = new PictureBox();
        //                    ImageConverter converter = new ImageConverter();
        //                    pb.Margin = new Padding(0, 0, 0, 0);
        //                    pb.Size = new Size(70, 70);
        //                    string ImagePath12 = Globals.Widgets_path + Widget + ".png";
        //                    pb.Image = Image.FromFile(ImagePath12);
        //                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
        //                    pb.Dock = DockStyle.Fill;
        //                    #region Save Widgets On DirectoryPath




        //                    #endregion Save Widgets On DirectoryPath
        //                    if (iWidget < distinctValues.Rows.Count - 1)
        //                    { iWidget++; }
        //                    else { }
        //                    if (iColumn == 5)
        //                    {
        //                        iColumn = 0;
        //                    }
        //                }



        //                var filters = new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp", "svg" };
        //                var files = GetFilesFrom1(Globals.Widgets_path, filters, false);
        //                PictureBox pbW = new PictureBox();
        //                ImageConverter converterW = new ImageConverter();
        //                for (int i = 0; i < files.Count(); i++)
        //                {
        //                    string WidgetPath = files[i];
        //                    pbW.Image = Image.FromFile(WidgetPath);
        //                    Bitmap bitmap = new Bitmap(pbW.Image);
        //                    Graphics gr = Graphics.FromImage(bitmap);
        //                    gr.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);

        //                    string WidgetPaths = files[i];
        //                    string WName = WidgetPaths.Split('\\').Last();
        //                    string ImagePathW = GDSlave.WidgetsPath + @"\" + WName;
        //                    bitmap.Save(ImagePathW, ImageFormat.Png);
        //                }
        //                SaveWidgetsInDirectory();
        //                UcSLD.TblLayoutCSLD.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        //                //UcSLD.TblLayoutCSLD.Size = new System.Drawing.Size(420, 700);
        //            }
        //        }
        //        else
        //        {
        //            GDSlave.GDisplaySlave = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        public static String[] GetFilesFrom1(String searchFolder, String[] filters, bool isRecursive)
        {
            List<String> filesFound = new List<String>();
            var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (var filter in filters)
            {
                filesFound.AddRange(System.IO.Directory.GetFiles(searchFolder, String.Format("*.{0}", filter), searchOption));
            }
            return filesFound.ToArray();
        }
        private void btnDone_Click11(object sender, EventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:btnDone_Click";
            try
            {
                #region ADD WIDGETS
                ListDIDO.Add("Blank");
                ListDIDO.Add("Line_Single_H_R_1");
                ListDIDO.Add("Line_Single_H_R_2");
                ListDIDO.Add("Line_Single_V_D_1");
                ListDIDO.Add("Line_SingleL_H_D_1");
                ListDIDO.Add("Line_SingleL_H_L_1");
                ListDIDO.Add("Line_SingleL_V_R_1");
                ListDIDO.Add("Line_SingleL_V_U_1");
                ListDIDO.Add("Line_SingleT_H_L_1");
                ListDIDO.Add("Line_SingleT_H_R_1");
                ListDIDO.Add("Line_SingleT_H_R_2");
                ListDIDO.Add("Line_SingleT_V_D_1");
                ListDIDO.Add("Line_SingleT_V_U_1");
                ListDIDO.Add("Line_Double_H_D_1");
                ListDIDO.Add("Line_DoubleT_H_2");
                ListDIDO.Add("Line_DoubleT_H_D_1");
                ListDIDO.Add("Line_Triple_H_D_1");
                ListDIDO.Add("Line_TripleT_H_D_1");
                ListDIDO.Add("Line_Triple_H_D_2");
                ListDIDO.Add("Line_Triple_H_D_3");
                ListDIDO.Add("SW_H_D_1_OFF");
                ListDIDO.Add("SW_H_D_1_ON");
                ListDIDO.Add("SW_H_D_1_Undefined");
                ListDIDO.Add("SW_H_R_2_OFF");
                ListDIDO.Add("SW_H_R_2_ON");
                ListDIDO.Add("SW_H_R_2_Undefined");
                ListDIDO.Add("SW_V_D_1_OFF");
                ListDIDO.Add("SW_V_D_1_ON");
                ListDIDO.Add("SW_V_D_1_Undefined");
                ListDIDO.Add("SW_V_U_1_OFF");
                ListDIDO.Add("SW_V_U_1_ON");
                ListDIDO.Add("SW_V_U_1_Undefined");
                ListDIDO.Add("Symbol_Arrow_H_L_1");
                ListDIDO.Add("Symbol_Arrow_H_R_1");
                ListDIDO.Add("Symbol_Arrow_V_D_1");
                ListDIDO.Add("Symbol_GND_H_L_1");
                ListDIDO.Add("Symbol_GND_H_R_1");
                ListDIDO.Add("Symbol_GND_V_R_1");
                ListDIDO.Add("Symbol_Tx_V_D_1");
                ListDIDO.Add("Symbol_Tx_V_D_2");
                ListDIDO.Add("Symbol2_CT_V_D_1");
                ListDIDO.Add("Symbol2_Tx_V_D_1");
                ListDIDO.Add("Symbol2_Tx_V_D_2");
                ListDIDO.Add("Text_AI_2_H_L_1");
                ListDIDO.Add("Text_AI_3_H_L_1");
                ListDIDO.Add("Text_AI_4_H_L_1");
                ListDIDO.Add("Text_AI_5_H_L_1");
                ListDIDO.Add("Text_AI_H_L_1");
                #endregion ADD WIDGETS
                List<KeyValuePair<string, string>> mbsData = Utils.getKeyValueAttributes(UcSLD.grpSLDSetting);
                if (!Validate()) return;
                if (mode == Mode.ADD)
                {
                    TreeNode tmp = null;
                    SLDSettingsList.Add(new SLDSettings("SLDSettings", mbsData, tmp, Int32.Parse(SlaveNum)));
                }
                else if (mode == Mode.EDIT)
                {
                    SLDSettingsList[editIndex].updateAttributes(mbsData);
                }
                refreshList();
                if (sender != null && e != null)
                {
                    UcSLD.grpSLDSetting.Visible = false;
                    mode = Mode.NONE;
                    editIndex = -1;
                }
                if (GDSlave.DsExcelData.Tables.Count > 0)
                {
                    if (GDSlave.IsSLDImport)
                    {
                        #region IsSLDImport
                        UcSLD.SCMain.Panel2Collapsed = false;
                        UcSLD.splitContainer1.Show();
                        UcSLD.splitContainer1.Panel2Collapsed = true;
                        UcSLD.SCDisplayImage.Show();
                        //UcSLD.SCDisplayData.Show();
                        UcSLD.tableLayoutPanel1.Hide();
                        GDSlave.GDisplaySlave = true;
                        UcSLD.tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                        UcSLD.tableLayoutPanel1.Size = new System.Drawing.Size(420, 700);
                        List<string> tblNameList = GDSlave.DsExcelData.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                        string CurrentSlave = tblNameList.Where(x => x.Contains(GDSlave.CurSlave)).Select(x => x).FirstOrDefault();
                        List<DataRow> ListCurrentSlave = GDSlave.DsExcelData.Tables[CurrentSlave].AsEnumerable().ToList();

                        DataView view = new DataView(GDSlave.DsExcelData.Tables[CurrentSlave]);
                        DataTable distinctValues = new DataTable();
                        distinctValues = view.ToTable(true, "CellNo", "Widget");
                        //Namrata: 16/09/2019
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "Images|*.png;*.bmp;*.jpg";
                        System.Drawing.Imaging.ImageFormat format = ImageFormat.Png;
                        int iFileRow = 0; int iColumn = 0; int PBname = 1; int iRow = 0;
                        ImageConverter converter = new ImageConverter();
                        //pb.Margin = new Padding(0, 0, 0, 0);
                        UcSLD.tableLayoutPanel1.Controls.OfType<PictureBox>().ToList().ForEach(x =>
                        {
                            string Widget = distinctValues.Rows[iFileRow]["Widget"].ToString();
                            x.Name = "PicBox" + PBname;
                            x.Size = new Size(70, 70);
                            object o = Properties.Resources.ResourceManager.GetObject(Widget);
                            //if (o is Image)
                            //{
                            //    x.Image = o as Image;

                            //}
                            //x.SizeMode = PictureBoxSizeMode.AutoSize;
                            //x.Dock = DockStyle.Fill;
                            string ImagePath1 = Globals.Widgets_path + Widget + ".png";
                            x.Image = Image.FromFile(ImagePath1);
                            x.SizeMode = PictureBoxSizeMode.StretchImage;
                            UcSLD.tableLayoutPanel1.SetColumn(x, iColumn);
                            UcSLD.tableLayoutPanel1.SetRow(x, iRow);
                            #region Save Widgets On DirectoryPath
                            //Namrata: 16/09/2019
                            Bitmap bitmap = new Bitmap(x.Image);
                            Graphics gr = Graphics.FromImage(bitmap);
                            gr.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
                            string FileExt = ".png";
                            switch (FileExt)
                            {
                                case ".jpg":
                                    format = ImageFormat.Jpeg;
                                    break;
                                case ".bmp":
                                    format = ImageFormat.Bmp;
                                    break;
                                case ".png":
                                    format = ImageFormat.Png;
                                    break;
                            }
                            string ImagePath = GDSlave.WidgetsPath + @"\" + Widget + ".png";
                            bitmap.Save(ImagePath, ImageFormat.Png);

                            #endregion Save Widgets On DirectoryPath
                            PBname++;
                            if (iFileRow < distinctValues.Rows.Count - 1)
                            { iFileRow++; }
                            else { }
                            if (iColumn == 5)
                            {
                                iColumn = 0; iRow++;
                            }
                            else
                            {
                                iColumn++;
                            }
                        });
                        SaveWidgetsInDirectory();
                        //UcSLD.SCMain.Panel2Collapsed = false;
                        //UcSLD.splitContainer1.Show();
                        //UcSLD.splitContainer1.Panel2Collapsed = true;
                        //UcSLD.SCDisplayImage.Show();
                        ////UcSLD.SCDisplayData.Show();
                        //UcSLD.tableLayoutPanel1.Hide();

                        UcSLD.tableLayoutPanel1.Show();
                        UcSLD.SCMain.SplitterDistance = 721;
                        UcSLD.splitContainer1.SplitterDistance = 330;
                        UcSLD.splitContainer1.Panel2Collapsed = true;
                        UcSLD.panel1.Show();
                        UcSLD.label3.Show(); UcSLD.RTxtFilePath.Show();
                        UcSLD.RTxtFilePath.Text = GDSlave.FileFullPath;
                    }
                    #endregion IsSLDImport
                    else
                    {
                        GDSlave.GDisplaySlave = true;
                        UcSLD.tableLayoutPanel1.Hide();
                        List<string> tblNameList = GDSlave.DsExcelData.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                        string CurrentSlave = tblNameList.Where(x => x.Contains(GDSlave.CurSlave)).Select(x => x).FirstOrDefault();
                        List<DataRow> ListCurrentSlave = GDSlave.DsExcelData.Tables[CurrentSlave].AsEnumerable().ToList();

                        DataView view = new DataView(GDSlave.DsExcelData.Tables[CurrentSlave]);
                        DataTable distinctValues = new DataTable();
                        distinctValues = view.ToTable(true, "CellNo", "Widget");
                        //Namrata: 16/09/2019
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "Images|*.png;*.bmp;*.jpg";
                        System.Drawing.Imaging.ImageFormat format = ImageFormat.Png;
                        int iWidget = 0; int iColumn = 0;
                        for (int iRow = 0; iRow < distinctValues.Rows.Count; iRow++)
                        {
                            string Widget = distinctValues.Rows[iWidget]["Widget"].ToString();
                            PictureBox pb = new PictureBox();
                            ImageConverter converter = new ImageConverter();
                            pb.Margin = new Padding(0, 0, 0, 0);
                            pb.Size = new Size(70, 70);
                            //pb.Size =new Size(70, 70);
                            //object o = Properties.Resources.ResourceManager.GetObject(Widget);
                            //if (o is Image)
                            //{
                            //    pb.Image = o as Image;
                            //}
                            //pb.SizeMode = PictureBoxSizeMode.AutoSize;
                            string ImagePath12 = Globals.Widgets_path + Widget + ".png";
                            pb.Image = Image.FromFile(ImagePath12);
                            pb.SizeMode = PictureBoxSizeMode.StretchImage;
                            pb.Dock = DockStyle.Fill;
                            #region Save Widgets On DirectoryPath

                            #region Save Widgtes Switch
                            object SwHDUndefine = Properties.Resources.ResourceManager.GetObject("SW_H_D_1_Undefined");
                            object SwHDON = Properties.Resources.ResourceManager.GetObject("SW_H_D_1_ON");
                            object SwHDOFF = Properties.Resources.ResourceManager.GetObject("SW_H_D_1_OFF");

                            object SwHRUndefine = Properties.Resources.ResourceManager.GetObject("SW_H_R_2_Undefined");
                            object SwHRON = Properties.Resources.ResourceManager.GetObject("SW_H_R_2_ON");
                            object SwHROFF = Properties.Resources.ResourceManager.GetObject("SW_H_R_2_OFF");

                            object SwVDUndefine = Properties.Resources.ResourceManager.GetObject("SW_V_D_1_Undefined");
                            object SwVDON = Properties.Resources.ResourceManager.GetObject("SW_V_D_1_ON");
                            object SwVDOFF = Properties.Resources.ResourceManager.GetObject("SW_V_D_1_OFF");

                            object SwVUUndefine = Properties.Resources.ResourceManager.GetObject("SW_V_U_1_Undefined");
                            object SwVDUN = Properties.Resources.ResourceManager.GetObject("SW_V_U_1_ON");
                            object SwVDUFF = Properties.Resources.ResourceManager.GetObject("SW_V_U_1_OFF");

                            Bitmap BmpImg = new Bitmap(pb.Image);
                            string ImagePath;

                            #region SwitchHD1Nodes
                            foreach (KeyValuePair<string, string> mn in SwitchHD1Nodes)
                            {
                                switch (mn.Key)
                                {
                                    case "SW_H_D_1_OFF":
                                        pb.Image = SwHDON as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_H_D_1_ON" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);

                                        pb.Image = SwHDUndefine as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_H_D_1_Undefined" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);
                                        break;
                                    case "SW_H_D_1_ON":
                                        pb.Image = SwHDOFF as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_H_D_1_OFF" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);

                                        pb.Image = SwHDUndefine as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_H_D_1_Undefined" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);
                                        break;
                                    case "SW_H_D_1_Undefined":
                                        pb.Image = SwHDON as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_H_D_1_ON" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);

                                        pb.Image = SwHDOFF as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_H_D_1_OFF" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            #endregion SwitchHD1Nodes

                            #region SwitchHR2Nodes
                            foreach (KeyValuePair<string, string> mn in SwitchHR2Nodes)
                            {
                                switch (mn.Key)
                                {
                                    case "SW_H_R_2_OFF":
                                        pb.Image = SwHDON as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_H_R_2_ON" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);

                                        pb.Image = SwHDUndefine as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_H_R_2_Undefined" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);
                                        break;
                                    case "SW_H_R_2_ON":
                                        pb.Image = SwHDOFF as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_H_R_2_OFF" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);

                                        pb.Image = SwHDUndefine as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_H_R_2_Undefined" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);
                                        break;
                                    case "SW_H_R_2_Undefined":
                                        pb.Image = SwHDON as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_H_R_2_ON" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);

                                        pb.Image = SwHDOFF as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_H_R_2_OFF" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            #endregion SwitchHD1Nodes

                            #region SwitchVD1Nodes
                            foreach (KeyValuePair<string, string> mn in SwitchVD1Nodes)
                            {
                                switch (mn.Key)
                                {
                                    case "SW_V_D_1_OFF":
                                        pb.Image = SwHDON as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_V_D_1_ON" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);

                                        pb.Image = SwHDUndefine as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_V_D_1_Undefined" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);
                                        break;
                                    case "SW_V_D_1_ON":
                                        pb.Image = SwHDOFF as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_V_D_1_OFF" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);

                                        pb.Image = SwHDUndefine as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_V_D_1_Undefined" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);
                                        break;
                                    case "SW_V_D_1_Undefined":
                                        pb.Image = SwHDON as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_V_D_1_ON" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);

                                        pb.Image = SwHDOFF as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_V_D_1_OFF" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            #endregion SwitchVD1Nodes

                            #region SwitchHD1Nodes
                            foreach (KeyValuePair<string, string> mn in SwitchVU1Nodes)
                            {
                                switch (mn.Key)
                                {
                                    case "SW_V_U_1_OFF":
                                        pb.Image = SwHDON as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_V_U_1_ON" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);

                                        pb.Image = SwHDUndefine as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_V_U_1_Undefined" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);
                                        break;
                                    case "SW_V_U_1_ON":
                                        pb.Image = SwHDOFF as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_V_U_1_OFF" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);

                                        pb.Image = SwHDUndefine as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_V_U_1_Undefined" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);
                                        break;
                                    case "SW_V_U_1_Undefined":
                                        pb.Image = SwHDON as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_V_U_1_ON" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);

                                        pb.Image = SwHDOFF as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_V_U_1_OFF" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            #endregion SwitchHD1Nodes
                            #endregion Save Widgtes Switch


                            Bitmap bitmap = new Bitmap(pb.Image);
                            Graphics gr = Graphics.FromImage(bitmap);
                            gr.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
                            string ImagePath1 = GDSlave.WidgetsPath + @"\" + Widget + ".png";
                            bitmap.Save(ImagePath1, ImageFormat.Png);

                            #endregion Save Widgets On DirectoryPath
                            if (iWidget < distinctValues.Rows.Count - 1)
                            { iWidget++; }
                            else { }
                            if (iColumn == 5)
                            {
                                iColumn = 0;
                            }
                        }

                        SaveWidgetsInDirectory();
                        UcSLD.TblLayoutCSLD.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                        //UcSLD.TblLayoutCSLD.Size = new System.Drawing.Size(420, 700);
                    }
                }
                else
                {
                    GDSlave.GDisplaySlave = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDone_Click1(object sender, EventArgs e)
        {
            string strRoutineName = "GraphicalDisplaySlave:btnDone_Click";
            try
            {
                #region ADD WIDGETS
                ListDIDO.Add("Blank");
                ListDIDO.Add("Line_Single_H_R_1");
                ListDIDO.Add("Line_Single_H_R_2");
                ListDIDO.Add("Line_Single_V_D_1");
                ListDIDO.Add("Line_SingleL_H_D_1");
                ListDIDO.Add("Line_SingleL_H_L_1");
                ListDIDO.Add("Line_SingleL_V_R_1");
                ListDIDO.Add("Line_SingleL_V_U_1");
                ListDIDO.Add("Line_SingleT_H_L_1");
                ListDIDO.Add("Line_SingleT_H_R_1");
                ListDIDO.Add("Line_SingleT_H_R_2");
                ListDIDO.Add("Line_SingleT_V_D_1");
                ListDIDO.Add("Line_SingleT_V_U_1");
                ListDIDO.Add("Line_Double_H_D_1");
                ListDIDO.Add("Line_DoubleT_H_2");
                ListDIDO.Add("Line_DoubleT_H_D_1");
                ListDIDO.Add("Line_Triple_H_D_1");
                ListDIDO.Add("Line_TripleT_H_D_1");
                ListDIDO.Add("Line_Triple_H_D_2");
                ListDIDO.Add("Line_Triple_H_D_3");
                ListDIDO.Add("SW_H_D_1_OFF");
                ListDIDO.Add("SW_H_D_1_ON");
                ListDIDO.Add("SW_H_D_1_Undefined");
                ListDIDO.Add("SW_H_R_2_OFF");
                ListDIDO.Add("SW_H_R_2_ON");
                ListDIDO.Add("SW_H_R_2_Undefined");
                ListDIDO.Add("SW_V_D_1_OFF");
                ListDIDO.Add("SW_V_D_1_ON");
                ListDIDO.Add("SW_V_D_1_Undefined");
                ListDIDO.Add("SW_V_U_1_OFF");
                ListDIDO.Add("SW_V_U_1_ON");
                ListDIDO.Add("SW_V_U_1_Undefined");
                ListDIDO.Add("Symbol_Arrow_H_L_1");
                ListDIDO.Add("Symbol_Arrow_H_R_1");
                ListDIDO.Add("Symbol_Arrow_V_D_1");
                ListDIDO.Add("Symbol_GND_H_L_1");
                ListDIDO.Add("Symbol_GND_H_R_1");
                ListDIDO.Add("Symbol_GND_V_R_1");
                ListDIDO.Add("Symbol_Tx_V_D_1");
                ListDIDO.Add("Symbol_Tx_V_D_2");
                ListDIDO.Add("Symbol2_CT_V_D_1");
                ListDIDO.Add("Symbol2_Tx_V_D_1");
                ListDIDO.Add("Symbol2_Tx_V_D_2");
                ListDIDO.Add("Text_AI_2_H_L_1");
                ListDIDO.Add("Text_AI_3_H_L_1");
                ListDIDO.Add("Text_AI_4_H_L_1");
                ListDIDO.Add("Text_AI_5_H_L_1");
                ListDIDO.Add("Text_AI_H_L_1");
                #endregion ADD WIDGETS
                List<KeyValuePair<string, string>> mbsData = Utils.getKeyValueAttributes(UcSLD.grpSLDSetting);
                if (!Validate()) return;
                if (mode == Mode.ADD)
                {
                    TreeNode tmp = null;
                    SLDSettingsList.Add(new SLDSettings("SLDSettings", mbsData, tmp, Int32.Parse(SlaveNum)));
                }
                else if (mode == Mode.EDIT)
                {
                    SLDSettingsList[editIndex].updateAttributes(mbsData);
                }
                refreshList();
                if (sender != null && e != null)
                {
                    UcSLD.grpSLDSetting.Visible = false;
                    mode = Mode.NONE;
                    editIndex = -1;
                }
                if (GDSlave.DsExcelData.Tables.Count > 0)
                {
                    if (GDSlave.IsSLDImport)
                    {
                        #region IsSLDImport
                        UcSLD.SCMain.Panel2Collapsed = false;
                        UcSLD.splitContainer1.Show();
                        UcSLD.splitContainer1.Panel2Collapsed = true;
                        UcSLD.SCDisplayImage.Show();
                        //UcSLD.SCDisplayData.Show();
                        UcSLD.tableLayoutPanel1.Hide();
                        GDSlave.GDisplaySlave = true;
                        UcSLD.tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                        UcSLD.tableLayoutPanel1.Size = new System.Drawing.Size(420, 700);
                        List<string> tblNameList = GDSlave.DsExcelData.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                        string CurrentSlave = tblNameList.Where(x => x.Contains(GDSlave.CurSlave)).Select(x => x).FirstOrDefault();
                        List<DataRow> ListCurrentSlave = GDSlave.DsExcelData.Tables[CurrentSlave].AsEnumerable().ToList();

                        DataView view = new DataView(GDSlave.DsExcelData.Tables[CurrentSlave]);
                        DataTable distinctValues = new DataTable();
                        distinctValues = view.ToTable(true, "CellNo", "Widget");
                        //Namrata: 16/09/2019
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "Images|*.png;*.bmp;*.jpg";
                        System.Drawing.Imaging.ImageFormat format = ImageFormat.Png;
                        int iFileRow = 0; int iColumn = 0; int PBname = 1; int iRow = 0;
                        ImageConverter converter = new ImageConverter();
                        //pb.Margin = new Padding(0, 0, 0, 0);
                        UcSLD.tableLayoutPanel1.Controls.OfType<PictureBox>().ToList().ForEach(x =>
                        {
                            string Widget = distinctValues.Rows[iFileRow]["Widget"].ToString();
                            x.Name = "PicBox" + PBname;
                            x.Size = new Size(70, 70);
                            object o = Properties.Resources.ResourceManager.GetObject(Widget);
                            if (o is Image)
                            {
                                x.Image = o as Image;

                            }
                            x.SizeMode = PictureBoxSizeMode.AutoSize;
                            x.Dock = DockStyle.Fill;
                            UcSLD.tableLayoutPanel1.SetColumn(x, iColumn);
                            UcSLD.tableLayoutPanel1.SetRow(x, iRow);
                            #region Save Widgets On DirectoryPath
                            //Namrata: 16/09/2019
                            Bitmap bitmap = new Bitmap(x.Image);
                            Graphics gr = Graphics.FromImage(bitmap);
                            gr.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
                            string FileExt = ".png";
                            switch (FileExt)
                            {
                                case ".jpg":
                                    format = ImageFormat.Jpeg;
                                    break;
                                case ".bmp":
                                    format = ImageFormat.Bmp;
                                    break;
                                case ".png":
                                    format = ImageFormat.Png;
                                    break;
                            }
                            string ImagePath = GDSlave.WidgetsPath + @"\" + Widget + ".png";
                            bitmap.Save(ImagePath, ImageFormat.Png);

                            #endregion Save Widgets On DirectoryPath
                            PBname++;
                            if (iFileRow < distinctValues.Rows.Count - 1)
                            { iFileRow++; }
                            else { }
                            if (iColumn == 5)
                            {
                                iColumn = 0; iRow++;
                            }
                            else
                            {
                                iColumn++;
                            }
                        });
                        SaveWidgetsInDirectory();
                        //UcSLD.SCMain.Panel2Collapsed = false;
                        //UcSLD.splitContainer1.Show();
                        //UcSLD.splitContainer1.Panel2Collapsed = true;
                        //UcSLD.SCDisplayImage.Show();
                        ////UcSLD.SCDisplayData.Show();
                        //UcSLD.tableLayoutPanel1.Hide();

                        UcSLD.tableLayoutPanel1.Show();
                        UcSLD.SCMain.SplitterDistance = 721;
                        UcSLD.splitContainer1.SplitterDistance = 330;
                        UcSLD.splitContainer1.Panel2Collapsed = true;
                        UcSLD.panel1.Show();
                        UcSLD.label3.Show(); UcSLD.RTxtFilePath.Show();
                        UcSLD.RTxtFilePath.Text = GDSlave.FileFullPath;
                    }
                    #endregion IsSLDImport
                    else
                    {
                        GDSlave.GDisplaySlave = true;
                        UcSLD.tableLayoutPanel1.Hide();
                        List<string> tblNameList = GDSlave.DsExcelData.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                        string CurrentSlave = tblNameList.Where(x => x.Contains(GDSlave.CurSlave)).Select(x => x).FirstOrDefault();
                        List<DataRow> ListCurrentSlave = GDSlave.DsExcelData.Tables[CurrentSlave].AsEnumerable().ToList();

                        DataView view = new DataView(GDSlave.DsExcelData.Tables[CurrentSlave]);
                        DataTable distinctValues = new DataTable();
                        distinctValues = view.ToTable(true, "CellNo", "Widget");
                        //Namrata: 16/09/2019
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "Images|*.png;*.bmp;*.jpg";
                        System.Drawing.Imaging.ImageFormat format = ImageFormat.Png;
                        int iWidget = 0; int iColumn = 0;
                        for (int iRow = 0; iRow < distinctValues.Rows.Count; iRow++)
                        {
                            string Widget = distinctValues.Rows[iWidget]["Widget"].ToString();
                            PictureBox pb = new PictureBox();
                            ImageConverter converter = new ImageConverter();
                            pb.Margin = new Padding(0, 0, 0, 0);
                            pb.Size = new Size(70, 70);
                            //pb.Size =new Size(70, 70);
                            object o = Properties.Resources.ResourceManager.GetObject(Widget);
                            if (o is Image)
                            {
                                pb.Image = o as Image;
                            }
                            pb.SizeMode = PictureBoxSizeMode.AutoSize;
                            pb.Dock = DockStyle.Fill;
                            #region Save Widgets On DirectoryPath

                            #region Save Widgtes Switch
                            object SwHDUndefine = Properties.Resources.ResourceManager.GetObject("SW_H_D_1_Undefined");
                            object SwHDON = Properties.Resources.ResourceManager.GetObject("SW_H_D_1_ON");
                            object SwHDOFF = Properties.Resources.ResourceManager.GetObject("SW_H_D_1_OFF");

                            object SwHRUndefine = Properties.Resources.ResourceManager.GetObject("SW_H_R_2_Undefined");
                            object SwHRON = Properties.Resources.ResourceManager.GetObject("SW_H_R_2_ON");
                            object SwHROFF = Properties.Resources.ResourceManager.GetObject("SW_H_R_2_OFF");

                            object SwVDUndefine = Properties.Resources.ResourceManager.GetObject("SW_V_D_1_Undefined");
                            object SwVDON = Properties.Resources.ResourceManager.GetObject("SW_V_D_1_ON");
                            object SwVDOFF = Properties.Resources.ResourceManager.GetObject("SW_V_D_1_OFF");

                            object SwVUUndefine = Properties.Resources.ResourceManager.GetObject("SW_V_U_1_Undefined");
                            object SwVDUN = Properties.Resources.ResourceManager.GetObject("SW_V_U_1_ON");
                            object SwVDUFF = Properties.Resources.ResourceManager.GetObject("SW_V_U_1_OFF");

                            Bitmap BmpImg = new Bitmap(pb.Image);
                            string ImagePath;

                            #region SwitchHD1Nodes
                            foreach (KeyValuePair<string, string> mn in SwitchHD1Nodes)
                            {
                                switch (mn.Key)
                                {
                                    case "SW_H_D_1_OFF":
                                        pb.Image = SwHDON as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_H_D_1_ON" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);

                                        pb.Image = SwHDUndefine as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_H_D_1_Undefined" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);
                                        break;
                                    case "SW_H_D_1_ON":
                                        pb.Image = SwHDOFF as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_H_D_1_OFF" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);

                                        pb.Image = SwHDUndefine as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_H_D_1_Undefined" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);
                                        break;
                                    case "SW_H_D_1_Undefined":
                                        pb.Image = SwHDON as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_H_D_1_ON" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);

                                        pb.Image = SwHDOFF as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_H_D_1_OFF" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            #endregion SwitchHD1Nodes

                            #region SwitchHR2Nodes
                            foreach (KeyValuePair<string, string> mn in SwitchHR2Nodes)
                            {
                                switch (mn.Key)
                                {
                                    case "SW_H_R_2_OFF":
                                        pb.Image = SwHDON as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_H_R_2_ON" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);

                                        pb.Image = SwHDUndefine as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_H_R_2_Undefined" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);
                                        break;
                                    case "SW_H_R_2_ON":
                                        pb.Image = SwHDOFF as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_H_R_2_OFF" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);

                                        pb.Image = SwHDUndefine as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_H_R_2_Undefined" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);
                                        break;
                                    case "SW_H_R_2_Undefined":
                                        pb.Image = SwHDON as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_H_R_2_ON" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);

                                        pb.Image = SwHDOFF as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_H_R_2_OFF" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            #endregion SwitchHD1Nodes

                            #region SwitchVD1Nodes
                            foreach (KeyValuePair<string, string> mn in SwitchVD1Nodes)
                            {
                                switch (mn.Key)
                                {
                                    case "SW_V_D_1_OFF":
                                        pb.Image = SwHDON as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_V_D_1_ON" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);

                                        pb.Image = SwHDUndefine as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_V_D_1_Undefined" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);
                                        break;
                                    case "SW_V_D_1_ON":
                                        pb.Image = SwHDOFF as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_V_D_1_OFF" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);

                                        pb.Image = SwHDUndefine as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_V_D_1_Undefined" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);
                                        break;
                                    case "SW_V_D_1_Undefined":
                                        pb.Image = SwHDON as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_V_D_1_ON" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);

                                        pb.Image = SwHDOFF as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_V_D_1_OFF" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            #endregion SwitchVD1Nodes

                            #region SwitchHD1Nodes
                            foreach (KeyValuePair<string, string> mn in SwitchVU1Nodes)
                            {
                                switch (mn.Key)
                                {
                                    case "SW_V_U_1_OFF":
                                        pb.Image = SwHDON as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_V_U_1_ON" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);

                                        pb.Image = SwHDUndefine as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_V_U_1_Undefined" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);
                                        break;
                                    case "SW_V_U_1_ON":
                                        pb.Image = SwHDOFF as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_V_U_1_OFF" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);

                                        pb.Image = SwHDUndefine as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_V_U_1_Undefined" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);
                                        break;
                                    case "SW_V_U_1_Undefined":
                                        pb.Image = SwHDON as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_V_U_1_ON" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);

                                        pb.Image = SwHDOFF as Image;
                                        ImagePath = GDSlave.WidgetsPath + @"\" + "SW_V_U_1_OFF" + ".png";
                                        BmpImg.Save(ImagePath, ImageFormat.Png);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            #endregion SwitchHD1Nodes
                            #endregion Save Widgtes Switch


                            Bitmap bitmap = new Bitmap(pb.Image);
                            Graphics gr = Graphics.FromImage(bitmap);
                            gr.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
                            string ImagePath1 = GDSlave.WidgetsPath + @"\" + Widget + ".png";
                            bitmap.Save(ImagePath1, ImageFormat.Png);

                            #endregion Save Widgets On DirectoryPath
                            if (iWidget < distinctValues.Rows.Count - 1)
                            { iWidget++; }
                            else { }
                            if (iColumn == 5)
                            {
                                iColumn = 0;
                            }
                        }

                        SaveWidgetsInDirectory();
                        UcSLD.TblLayoutCSLD.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                        //UcSLD.TblLayoutCSLD.Size = new System.Drawing.Size(420, 700);
                    }
                }
                else
                {
                    GDSlave.GDisplaySlave = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
     
        public void SaveWidgetsInDirectory()
        {
            string strRoutineName = "GraphicalDisplaySlave:SaveWidgetsInDirectory";
            try
            {
                string FolderPathToZip = GDSlave.WidgetsPath;
                string ZipFileName = GDSlave.WidgetsPath + ".zip";
                try
                {
                    if (System.IO.Directory.Exists(System.IO.Path.GetTempPath() + "protocol" + @"\" + "GUI" + @"\" + GDSlave.SlaveFolder)) { }
                    else
                    {
                        System.IO.Directory.CreateDirectory(System.IO.Path.GetTempPath() + "protocol" + @"\" + "GUI" + @"\" + GDSlave.SlaveFolder);
                    }
                    #region Delete ZipFile if Exist in Directory
                    //if (File.Exists(ZipFileName))
                    //{
                    //    File.Delete(ZipFileName);
                    //}
                    //else
                    //{
                    //    ZipFile.CreateFromDirectory(FolderPathToZip, ZipFileName);
                    //}
                    #endregion Delete ZipFile if Exist in Directory

                    #region Delete Widget Folder From Directory
                    //if (System.IO.Directory.Exists(GDSlave.WidgetsPath))
                    //{
                    //    System.IO.Directory.Delete(GDSlave.WidgetsPath, true);
                    //}
                    #endregion Delete Widget Folder From Directory

                }
                catch (Exception)
                {
                    MessageBox.Show("Some Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public DataTable RemoveDuplicateRows(DataTable dTable, string colName)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();
            foreach (DataRow drow in dTable.Rows)
            {
                if (hTable.Contains(drow[colName]))
                    duplicateList.Add(drow);
                else
                    hTable.Add(drow[colName], string.Empty);
            }
            foreach (DataRow dRow in duplicateList)
                dTable.Rows.Remove(dRow);
            return dTable;
        }
        private string rnName = "SMSlave";

        string SlaveNodeName = "";
        string SlaveNumber = "";
        string strTxtFilePath = "";
        public void parseIECGNode(XmlNode iecgNode, TreeNode tn)
        {
            string strRoutineName = "GraphicalDisplaySlave:parseIECGNode";
            try
            {
                rnName = iecgNode.Name;
                tn.Nodes.Clear();
                GraphicalDisplaySlaveTreeNode = tn;
                foreach (XmlNode node in iecgNode.ChildNodes[0].ChildNodes)
                {
                    TreeNode tmp = null;
                    XmlElement root = node.ParentNode as XmlElement;
                    if (node.NodeType == XmlNodeType.Comment) continue;
                    SLDSettingsList.Add(new SLDSettings(node, tmp));

                    #region Importing GDisplay Data
                    SlaveNodeName = "GDisplaySlave GraphicalDisplaySlave_" + iecgNode.Attributes[0].Value;//"IEC61850 " + mbsgNode.Attributes[9].Value.ToString();
                    SlaveNumber = iecgNode.Attributes[0].Value;
                    strTxtFilePath = SLDSettingsList[SLDSettingsList.Count - 1].TemplateSLDFile.ToString();
                    GDSlave.SubDirName = "GraphicalDisplaySlave_" + iecgNode.Attributes[0].Value;
                    GDSlave.SlaveFolder = "GraphicalDisplaySlave_" + iecgNode.Attributes[0].Value;

                    if (!string.IsNullOrEmpty(strTxtFilePath))
                    {
                        //string TxtFilePath = Utils.XMLFolderPath + "\\" + "protocol" + "\\" + "GUI" + "\\"+GDSlave.SubDirName +"\\"+ strTxtFilePath;
                        string TxtFilePath = Utils.XMLFolderPath + "\\" + "GUI" + "\\" + GDSlave.SubDirName + "\\" + strTxtFilePath;
                        if (File.Exists(TxtFilePath))
                        {
                            ImportCSVData(TxtFilePath);
                        }
                        //else if (!File.Exists(ICDFilesData.ICDDirFile + @"\" + strTxtFilePath))
                        //{
                        //}
                    }
                    else
                    {
                    }
                    #endregion Importing GDisplay Data
                }
                refreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void ImportCSVData(string Path)
        {
            string strRoutineName = "GraphicalDisplaySlave: ImportCSVData";
            try
            {
                UcSLD.SCMain.Panel2Collapsed = false;
                UcSLD.splitContainer1.Show();
                UcSLD.splitContainer1.Panel1Collapsed = true;
                UcSLD.SCCreateSLD.Hide();
                UcSLD.SCDisplayImage.Hide();
                UcSLD.TblLayoutCSLD.Show();
                //UcSLD.SCDisplayData.Show();
                UcSLD.tableLayoutPanel1.Hide();
                UcSLD.BtnSaveSLD.Visible = true;
                UcSLD.BtnSaveSLD.Location = new Point(200, 3);
                UcSLD.btnDelete.Location = new Point(286, 3);
                UcSLD.panel3.VerticalScroll.Visible = true;
                UcSLD.SCMain.SplitterDistance = 400;
                UcSLD.splitContainer1.SplitterDistance = 320;

                UcSLD.SCMain.SplitterDistance = 440;
                UcSLD.splitContainer1.SplitterDistance = 300;

                UcSLD.SCMain.SplitterDistance = 400;//;412;
                UcSLD.SCCreateSLD.SplitterDistance = 256;//240;
                UcSLD.SCCreateSLD.Show();
                int iFileRow = 0; int iColumn = 0; int PBname = 1; int iRow = 0;
                string filePath = string.Empty;
                string fileExt = string.Empty;
                openFileDialog1.AddExtension = true;
                openFileDialog1.InitialDirectory = Application.ExecutablePath;
                openFileDialog1.FileName = Path;
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;

                #region Get UnitID,TreeNode,ICDFilePath
                Utils.strFrmOpenproplusTreeNode = SlaveNodeName;//TreeNode
                //UcSLD.lblFilePath.Text = Path;
                UcSLD.txtTemplateSLDFile.Text = openFileDialog1.SafeFileName;
                #endregion Get UnitID,TreeNode,ICDFilePath

                string TableName = "GraphicalDisplaySlave_" + SlaveNumber + "_" + strTxtFilePath;
                ConvertTextFileToDS(Path, TableName, ",");
                UcSLD.txtTemplateSLDFile.Focus();
                GDSlave.XLSFileName = strTxtFilePath;
                GDSlave.FileFullPath = Path;

                UcSLD.tableLayoutPanel1.Hide();
                if (GDSlave.DsExcelData.Tables.Count > 0)
                {
                    GDSlave.GDisplaySlave = true;
                    UcSLD.TblLayoutCSLD.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                    UcSLD.TblLayoutCSLD.Size = new System.Drawing.Size(420, 700);
                    List<string> tblNameList = GDSlave.DsExcelData.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                    string CurrentSlave = tblNameList.Where(x => x.Contains(TableName)).Select(x => x).FirstOrDefault();
                    List<DataRow> ListCurrentSlave = GDSlave.DsExcelData.Tables[CurrentSlave].AsEnumerable().ToList();

                    DataView view = new DataView(GDSlave.DsExcelData.Tables[CurrentSlave]);
                    //List<string> tblNameList = GDSlave.DsExcelData.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                    //string CurrentSlave = tblNameList.Where(x => x.Contains(GDSlave.CurSlave)).Select(x => x).FirstOrDefault();
                    //List<DataRow> ListCurrentSlave = GDSlave.DsExcelData.Tables[GDSlave.CurSlave].AsEnumerable().ToList();

                    //DataView view = new DataView(GDSlave.DsExcelData.Tables[GDSlave.CurSlave]);
                    DataTable distinctValues = new DataTable();
                    distinctValues = view.ToTable(true, "CellNo", "Widget");
                    //Namrata: 16/09/2019
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "Images|*.png;*.bmp;*.jpg";
                    System.Drawing.Imaging.ImageFormat format = ImageFormat.Png;
                    GDSlave.WidgetsPath =  System.IO.Path.GetTempPath() + @"\" + "protocol" + @"\" + Utils.strFrmOpenproplusTreeNode + @"\" + "widget";
                         ImageConverter converter = new ImageConverter();
                    //pb.Margin = new Padding(0, 0, 0, 0);
                    UcSLD.TblLayoutCSLD.Controls.OfType<PictureBox>().ToList().ForEach(x =>
                    {
                        string Widget = distinctValues.Rows[iFileRow]["Widget"].ToString();
                        x.Name = "Pb" + PBname;
                        x.Size = new Size(70, 70);
                        x.Tag = Widget;
                        //UcSLD.Pb1.Tag = GDSlave.GetTreeNode;
                        object o = Properties.Resources.ResourceManager.GetObject(Widget);
                        string ImagePath1 = Globals.Widgets_path + Widget + ".png";
                        x.Image = Image.FromFile(ImagePath1);
                        x.SizeMode = PictureBoxSizeMode.StretchImage;
                        UcSLD.TblLayoutCSLD.SetColumn(x, iColumn);
                        UcSLD.TblLayoutCSLD.SetRow(x, iRow);
                        #region Save Widgets On DirectoryPath
                        //Namrata: 16/09/2019
                        Bitmap bitmap = new Bitmap(x.Image);
                        Graphics gr = Graphics.FromImage(bitmap);
                        gr.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);

                        #endregion Save Widgets On DirectoryPath
                        PBname++;
                        if (iFileRow < distinctValues.Rows.Count - 1)
                        { iFileRow++; }
                        else { }
                        if (iColumn == 5)
                        {
                            iColumn = 0; iRow++;
                        }
                        else
                        {
                            iColumn++;
                        }
                    });


                    //var filters = new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp", "svg" };
                    //var files = GetFilesFrom1(Globals.Widgets_path, filters, false);
                    //PictureBox pbW = new PictureBox();
                    //ImageConverter converterW = new ImageConverter();
                    //for (int i = 0; i < files.Count(); i++)
                    //{
                    //    string WidgetPath = files[i];
                    //    pbW.Image = Image.FromFile(WidgetPath);
                    //    Bitmap bitmap = new Bitmap(pbW.Image);
                    //    Graphics gr = Graphics.FromImage(bitmap);
                    //    gr.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);

                    //    string WidgetPaths = files[i];
                    //    string WName = WidgetPaths.Split('\\').Last();
                    //    string ImagePathW = GDSlave.WidgetsPath + @"\" + WName;
                    //    bitmap.Save(ImagePathW, ImageFormat.Png);
                     
                    //    bitmap.Dispose();//To Do…. isprobaj
                    //    gr.Dispose();//To Do…. isprobaj
                    //}

                    //SaveWidgetsInDirectory();
                   
                    UcSLD.SCMain.Panel2Collapsed = false;
                    UcSLD.splitContainer1.Show();
                    UcSLD.splitContainer1.Panel1Collapsed = true;
                    UcSLD.SCCreateSLD.Show();
                    UcSLD.SCDisplayImage.Hide();
                    UcSLD.TblLayoutCSLD.Show();
                    //UcSLD.SCDisplayData.Show();
                    UcSLD.tableLayoutPanel1.Hide();
                    UcSLD.BtnSaveSLD.Visible = true;
                    UcSLD.BtnSaveSLD.Location = new Point(200, 3);
                    UcSLD.btnDelete.Location = new Point(286, 3);
                    UcSLD.panel3.VerticalScroll.Visible = true;
                    UcSLD.SCMain.SplitterDistance = 400;
                    UcSLD.splitContainer1.SplitterDistance = 320;

                    UcSLD.SCMain.SplitterDistance = 440;
                    UcSLD.splitContainer1.SplitterDistance = 300;

                    UcSLD.SCMain.SplitterDistance = 400;//;412;
                    UcSLD.SCCreateSLD.SplitterDistance = 256;//240;
                    UcSLD.SCCreateSLD.Show();
                    //UcSLD.tableLayoutPanel1.Show();
                    //UcSLD.SCMain.SplitterDistance = 630;
                    //UcSLD.splitContainer1.SplitterDistance = 400;
                    //UcSLD.splitContainer1.Panel2Collapsed = true;
                    //UcSLD.panel1.Show();
                    //UcSLD.label3.Show(); UcSLD.RTxtFilePath.Show();
                   // UcSLD.RTxtFilePath.Text = GDSlave.FileFullPath;


                    //SaveWidgetsInDirectory();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      
        public void ImportCSVDataOld(string Path)
        {
            string strRoutineName = "GraphicalDisplaySlave: ImportCSVData";
            try
            {
                int iFileRow = 0; int iColumn = 0; int PBname = 1; int iRow = 0;
                string filePath = string.Empty;
                string fileExt = string.Empty;
                openFileDialog1.AddExtension = true;
                openFileDialog1.InitialDirectory = Application.ExecutablePath;
                openFileDialog1.FileName = Path;
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;

                #region Get UnitID,TreeNode,ICDFilePath
                Utils.strFrmOpenproplusTreeNode = SlaveNodeName;//TreeNode
                //UcSLD.lblFilePath.Text = Path;
                UcSLD.txtTemplateSLDFile.Text = openFileDialog1.SafeFileName;
                #endregion Get UnitID,TreeNode,ICDFilePath

                string TableName = "GraphicalDisplaySlave_" + SlaveNumber + "_" + strTxtFilePath;
                ConvertTextFileToDS(Path, TableName, ",");
                UcSLD.txtTemplateSLDFile.Focus();
                GDSlave.XLSFileName = strTxtFilePath;
                GDSlave.FileFullPath = Path;

                Directory.CreateDirectoryForGraphicalDisplay(GDSlave.XLSFileName, GDSlave.FileFullPath);
                Directory.CreateDirectoryForWidgets(GDSlave.XLSFileName, GDSlave.FileFullPath);
                UcSLD.tableLayoutPanel1.Hide();
                if (GDSlave.DsExcelData.Tables.Count > 0)
                {
                    GDSlave.GDisplaySlave = true;
                    //UcSLD.tableLayoutPanel1.Show();
                    //UcSLD.ChkGridLines.Hide();
                    UcSLD.tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
                    UcSLD.tableLayoutPanel1.Size = new System.Drawing.Size(420, 700);
                    //UcSLD.lblSLD.Show();
                    //UcSLD.lblFileName.Show();
                    // UcSLD.lblFilePath.Show();
                    List<string> tblNameList = GDSlave.DsExcelData.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                    string CurrentSlave = tblNameList.Where(x => x.Contains(TableName)).Select(x => x).FirstOrDefault();
                    List<DataRow> ListCurrentSlave = GDSlave.DsExcelData.Tables[CurrentSlave].AsEnumerable().ToList();

                    DataView view = new DataView(GDSlave.DsExcelData.Tables[CurrentSlave]);
                    DataTable distinctValues = new DataTable();
                    distinctValues = view.ToTable(true, "CellNo", "Widget");

                    //Namrata: 16/09/2019
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "Images|*.png;*.bmp;*.jpg";
                    System.Drawing.Imaging.ImageFormat format = ImageFormat.Png;
                    UcSLD.tableLayoutPanel1.Controls.OfType<PictureBox>().ToList().ForEach(x =>
                    {
                        string Widget = distinctValues.Rows[iFileRow]["Widget"].ToString();
                        x.Name = "PicBox" + PBname;
                        x.Size = new Size(70, 70);
                        string ImagePath = Globals.Widgets_path + Widget + ".png";
                        x.Image = Image.FromFile(ImagePath);
                        x.SizeMode = PictureBoxSizeMode.StretchImage;
                        x.Dock = DockStyle.Fill;
                        //object o = Properties.Resources.ResourceManager.GetObject(Widget);
                        //if (o is Image)
                        //{
                        //    x.Image = o as Image;

                        //}
                        //x.SizeMode = PictureBoxSizeMode.AutoSize;
                        //x.Dock = DockStyle.Fill;
                        UcSLD.tableLayoutPanel1.SetColumn(x, iColumn);
                        UcSLD.tableLayoutPanel1.SetRow(x, iRow);
                        #region Save Widgets On DirectoryPath
                        //Namrata: 16/09/2019
                        Bitmap bitmap = new Bitmap(x.Image);
                        Graphics gr = Graphics.FromImage(bitmap);
                        gr.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
                        string FileExt = ".png";
                        switch (FileExt)
                        {
                            case ".jpg":
                                format = ImageFormat.Jpeg;
                                break;
                            case ".bmp":
                                format = ImageFormat.Bmp;
                                break;
                            case ".png":
                                format = ImageFormat.Png;
                                break;
                        }
                        string ImagePath89 = GDSlave.WidgetsPath + @"\" + Widget + ".png";
                        bitmap.Save(ImagePath89, ImageFormat.Png);

                        #endregion Save Widgets On DirectoryPath
                        PBname++;
                        if (iFileRow < distinctValues.Rows.Count - 1)
                        { iFileRow++; }
                        else { }
                        if (iColumn == 5)
                        {
                            iColumn = 0; iRow++;
                        }
                        else
                        {
                            iColumn++;
                        }
                    });

                    SaveWidgetsInDirectory();
                    UcSLD.SCDisplayImage.Panel2Collapsed = true;
                    UcSLD.tableLayoutPanel1.Show();
                    UcSLD.SCMain.SplitterDistance = 400;
                    //UcSLD.tableLayoutPanel1.Show();
                    //UcSLD.SCMain.SplitterDistance = 721;
                    //UcSLD.splitContainer1.SplitterDistance = 330;
                    //UcSLD.splitContainer1.Panel2Collapsed = true;
                    //UcSLD.panel1.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void ImportCSVData1(string Path)
        {
            string strRoutineName = "GraphicalDisplaySlave: ImportCSVData";
            try
            {
                int iFileRow = 0; int iColumn = 0; int PBname = 1; int iRow = 0;
                string filePath = string.Empty;
                string fileExt = string.Empty;
                openFileDialog1.AddExtension = true;
                openFileDialog1.InitialDirectory = Application.ExecutablePath;
                openFileDialog1.FileName = Path;
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;

                #region Get UnitID,TreeNode,ICDFilePath
                Utils.strFrmOpenproplusTreeNode = SlaveNodeName;//TreeNode
                //UcSLD.lblFilePath.Text = Path;
                UcSLD.txtTemplateSLDFile.Text = openFileDialog1.SafeFileName;
                #endregion Get UnitID,TreeNode,ICDFilePath

                string TableName = "GraphicalDisplaySlave_" + SlaveNumber + "_" + strTxtFilePath;
                ConvertTextFileToDS(Path, TableName, ",");
                UcSLD.txtTemplateSLDFile.Focus();
                GDSlave.XLSFileName = strTxtFilePath;
                GDSlave.FileFullPath = Path;

                Directory.CreateDirectoryForGraphicalDisplay(GDSlave.XLSFileName, GDSlave.FileFullPath);
                Directory.CreateDirectoryForWidgets(GDSlave.XLSFileName, GDSlave.FileFullPath);
                if (GDSlave.DsExcelData.Tables.Count > 0)
                {
                    GDSlave.GDisplaySlave = true;
                    UcSLD.tableLayoutPanel1.Show();
                    //UcSLD.ChkGridLines.Hide();
                    UcSLD.tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
                    UcSLD.tableLayoutPanel1.Size = new System.Drawing.Size(420, 700);
                    //UcSLD.lblSLD.Show();
                    //UcSLD.lblFileName.Show();
                    // UcSLD.lblFilePath.Show();
                    List<string> tblNameList = GDSlave.DsExcelData.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                    string CurrentSlave = tblNameList.Where(x => x.Contains(TableName)).Select(x => x).FirstOrDefault();
                    List<DataRow> ListCurrentSlave = GDSlave.DsExcelData.Tables[CurrentSlave].AsEnumerable().ToList();

                    DataView view = new DataView(GDSlave.DsExcelData.Tables[CurrentSlave]);
                    DataTable distinctValues = new DataTable();
                    distinctValues = view.ToTable(true, "CellNo", "Widget");

                    //Namrata: 16/09/2019
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "Images|*.png;*.bmp;*.jpg";
                    System.Drawing.Imaging.ImageFormat format = ImageFormat.Png;
                    UcSLD.tableLayoutPanel1.Controls.OfType<PictureBox>().ToList().ForEach(x =>
                    {
                        string Widget = distinctValues.Rows[iFileRow]["Widget"].ToString();
                        x.Name = "PicBox" + PBname;
                        x.Size = new Size(70, 70);
                        object o = Properties.Resources.ResourceManager.GetObject(Widget);
                        if (o is Image)
                        {
                            x.Image = o as Image;

                        }
                        x.SizeMode = PictureBoxSizeMode.AutoSize;
                        x.Dock = DockStyle.Fill;
                        UcSLD.tableLayoutPanel1.SetColumn(x, iColumn);
                        UcSLD.tableLayoutPanel1.SetRow(x, iRow);
                        #region Save Widgets On DirectoryPath
                        //Namrata: 16/09/2019
                        Bitmap bitmap = new Bitmap(x.Image);
                        Graphics gr = Graphics.FromImage(bitmap);
                        gr.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
                        string FileExt = ".png";
                        switch (FileExt)
                        {
                            case ".jpg":
                                format = ImageFormat.Jpeg;
                                break;
                            case ".bmp":
                                format = ImageFormat.Bmp;
                                break;
                            case ".png":
                                format = ImageFormat.Png;
                                break;
                        }
                        string ImagePath = GDSlave.WidgetsPath + @"\" + Widget + ".png";
                        bitmap.Save(ImagePath, ImageFormat.Png);

                        #endregion Save Widgets On DirectoryPath
                        PBname++;
                        if (iFileRow < distinctValues.Rows.Count - 1)
                        { iFileRow++; }
                        else { }
                        if (iColumn == 5)
                        {
                            iColumn = 0; iRow++;
                        }
                        else
                        {
                            iColumn++;
                        }
                    });

                    SaveWidgetsInDirectory();
                    //UcSLD.tableLayoutPanel1.Show();
                    //UcSLD.SCMain.SplitterDistance = 721;
                    //UcSLD.splitContainer1.SplitterDistance = 330;
                    //UcSLD.splitContainer1.Panel2Collapsed = true;
                    //UcSLD.panel1.Show();
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
            rootNode = xmlDoc.CreateElement(sType.ToString());
            xmlDoc.AppendChild(rootNode);
            foreach (string attr in arrAttributes)
            {
                XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                rootNode.Attributes.Append(attrName);
            }
            XmlNode SLDNode = xmlDoc.CreateElement("SLD");// Create a new child node
            rootNode.AppendChild(SLDNode); //add the child node to the root node
            foreach (SLDSettings smsuser in SLDSettingsList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(smsuser.exportXMLnode(), true);
                SLDNode.AppendChild(importNode);
            }
            return rootNode;
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("GraphicalDisplaySlave_"))
            {
                return UcSLD;
            }
            return null;
        }
        public TreeNode getTreeNode()
        {
            return GraphicalDisplaySlaveTreeNode;
        }
        public string getSlaveID
        {
            get { return "GraphicalDisplaySlave" + SlaveNum; }
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public string SlaveNum
        {

            get { slaveNum = Int32.Parse(UcSLD.txtSlaveNum.Text); return slaveNum.ToString(); }
            set { slaveNum = Int32.Parse(value); UcSLD.txtSlaveNum.Text = value; Globals.SlaveNo = Int32.Parse(value); }
        }
        public string Type
        {
            get { return type = UcSLD.txtType.Text; }
            set { type = UcSLD.txtType.Text = value; }

        }
        public string GridRows
        {
            get { return gridRows = UcSLD.txtGR.Text; }
            set { gridRows = UcSLD.txtGR.Text = value; }

        }
        public string GridColumns
        {
            get { return gridColumns = UcSLD.txtGridColumns.Text; }
            set { gridColumns = UcSLD.txtGridColumns.Text = value; }

        }
        public string EventQSize
        {
            get { return eventQSize.ToString(); }
            set { eventQSize = Int32.Parse(value); }
        }
        public string AppFirmwareVersion
        {
            get { return appFirmwareVersion = UcSLD.txtFirmwareVersion.Text; }
            set { appFirmwareVersion = UcSLD.txtFirmwareVersion.Text = value; }
        }
        public string Run
        {
            get { run = UcSLD.chkRun.Checked; return (run == true ? "YES" : "NO"); }
            set
            {
                run = (value.ToLower() == "yes") ? true : false;
                if (run == true) UcSLD.chkRun.Checked = true;
                else UcSLD.chkRun.Checked = false;
            }
        }
        public string DEBUG
        {
            get { debug = Int32.Parse(UcSLD.txtDebug.Text); return debug.ToString(); }
            set { debug = Int32.Parse(value); UcSLD.txtDebug.Text = value; }
        }
        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
    }
}
