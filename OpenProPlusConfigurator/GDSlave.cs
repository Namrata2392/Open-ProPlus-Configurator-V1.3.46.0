using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using System.Net;
using System.Reflection;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;

namespace OpenProPlusConfigurator
{
    public class GDSlave
    {
        public static string GetTreeNode;
        public static PictureBox PBox = new PictureBox();
        public static TreeNode GDSlaveTreeNode;
        public static string GDSlaveNo;
        public static string SlaveFolder;

        //Get Excel file data to Datatable
        public static DataTable DtExcelData = new DataTable();
        public static DataSet DsExcelData = new DataSet();

        public static DataSet DsExportData = new DataSet();

        //Get Excel SheetName
        public static string SheetName;

        //Get Images As per AI,DI,DO
        public static List<string> ListDIDO = new List<string>();
        public static List<string> ListAI = new List<string>();
        public static DataTable DTAIImage = new DataTable();
        public static DataTable DTDIDOImage = new DataTable();

        public static bool GDisplaySlave = false;
        public static string DirName = "";
        public static string SLDName = ""; //SLD file name
        public static string SLDPath = ""; //Full SLD path
        public static string SLDWithoutExt = ""; //SLD file name Without Extension
        public static string SLDData;//SLD Data
        public static string CopyXMLFile;//SLD Data
        public static string SlaveNo;//SLD Data
        public static bool IsDOExist = false;
        public static bool IsSLDImport = false;


        #region Create Dir in AppData
        public static string CurrentSlave = "";
        public static string GDSlaveXlsFiles = "";
        public static string XLSDirFile = "";//GDSlave Directory Path on AppData
        public static string XLSFileName = "";//GDSlave Directory Path on AppData
        public static string CurSlave = "";
        public static string ImportSlaveNo = "";
        public static string FileFullPath = "";//GDSlave Directory Path on AppData
        public static string SubDirName = "";

        public static DataTable DtAIWidgets = new DataTable();
        public static DataTable DtDIDOWidgets = new DataTable();

        #region Store Widgets in AppData
        public static string WidgetsPath = "";
        #endregion Store Widgets in AppData

        #endregion Create Dir in AppData
        public static string CreateDirGDSlave = "";
        public static void ListImageDIDO()
        {
            ListDIDO.Add("SW_OFF_H_R_1");
            ListDIDO.Add("SW_OFF_H_R_2");
            ListDIDO.Add("SW_OFF_H_V_R_3");
            ListDIDO.Add("SW_OFF_V_D_2");
            ListDIDO.Add("SW_OFF_V_L_1");
            ListDIDO.Add("SW_OFF_V_R_1");
            ListDIDO.Add("SW_OFF_V_U_1");
            ListDIDO.Add("SW_ON_H_R_1");
            ListDIDO.Add("SW_ON_H_R_2");
            ListDIDO.Add("SW_ON_V_D_2");
            ListDIDO.Add("SW_ON_V_L_1");
            ListDIDO.Add("SW_ON_V_R_1");
            ListDIDO.Add("SW_V_D_1_OFF");
            ListDIDO.Add("SW_V_D_1_ON");
            ListDIDO.Add("SW_V_D_1_Undefined");

            #region Common Images
           

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
            ListDIDO.Add("Line_SingleT_V_D_1");
            ListDIDO.Add("Line_SingleT_V_U_1");

            ListDIDO.Add("Line_Double_H_D_1");
            ListDIDO.Add("Line_DoubleT_H_2");
            ListDIDO.Add("Line_DoubleT_H_D_1");

            ListDIDO.Add("Line_Triple_H_D_1");
            ListDIDO.Add("Line_TripleT_H_D_1");
            ListDIDO.Add("Line_Triple_H_D_2");
            ListDIDO.Add("Line_Triple_H_D_3");
            

            ListDIDO.Add("Symbol_Arrow_H_R_1");
            ListDIDO.Add("Symbol_Arrow_V_D_1");
            #endregion Common Images
        }
        public static void ListImageAI()
        {
            ListAI.Add("Symbol_CT_H_U_1");
            ListAI.Add("Symbol_CT_V_R_1");
            ListAI.Add("Symbol_GND_H_R_1");
            ListAI.Add("Symbol_GND_V_D_1");
            ListAI.Add("Symbol_PT_H_R_1");
            ListAI.Add("Symbol_PT_V_D_1");
            ListAI.Add("Symbol_Tx_H_R_1");
            ListAI.Add("Symbol_Tx_H_R_2");
            ListAI.Add("Symbol_Tx_V_D_1");
            ListAI.Add("Symbol_Tx_V_D_2");

            #region Common Images
            ListAI.Add("Blank");
            ListAI.Add("Line_Double_H_D_1");
            ListAI.Add("Line_DoubleT_H_2");
            ListAI.Add("Line_DoubleT_H_D_1");
            ListAI.Add("Line_Single_H_R_1");
            ListAI.Add("Line_Single_V_D_1");
            ListAI.Add("Line_SingleL_H_D_1");
            ListAI.Add("Line_SingleL_H_L_1");
            ListAI.Add("Line_SingleL_V_R_1");
            ListAI.Add("Line_SingleL_V_U_1");
            ListAI.Add("Line_SingleT_H_L_1");
            ListAI.Add("Line_SingleT_H_R_1");
            ListAI.Add("Line_SingleT_V_D_1");
            ListAI.Add("Line_SingleT_V_U_1");
            ListAI.Add("Symbol_Arrow_H_R_1");
            ListAI.Add("Symbol_Arrow_V_D_1");
            ListDIDO.Add("Text_AI 2_H_L_1");
            ListDIDO.Add("Text_AI_H_L_1");
            #endregion Common Images
        }
    }
}
