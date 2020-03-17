//-------------------------------------------------------------------------------------------------------------------
//Created By: Namrata
//Date: 08/04/2019
//For IEC61850Client & IEC61850Server 
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


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

namespace OpenProPlusConfigurator
{
    public class ICDFilesData
    {
        public static string ICDDirMFile = "";//IEC61850Client Directory Path on AppData
        public static string ICDDirSFile = "";//IEC61850Server Directory Path on AppData
        public static string DirectoryName = "";
        public static string XmlName = "";//Xml file name

        public static string CreateDirIEC61850C = "";
        public static string CreateDirIEC61850S = "";

        public static string XmlPath = "";//Full Xml path
        public static string XmlWithoutExt = "";//Xml file name Without Extension
        public static string XMLData;//Xml Data
        //Namrata:08/04/2019
        public static string CopyXMLFile = ""; //Copy xml file in APPData
        public static string IEC61850SICDFileLoc = "";//Create directory for IEC61850Server

        public static string XMLFileIEC61850Client = "";//Create directory for IEC61850Client
        public static string IEC61850Xmlname = "";//Xml file name for IEC61850
        public static bool IEC61850Client = false;// Set bool for 61850Client
        public static string IEC61850CICDFileLoc = "";//IEC61850Client ICD file location

        public static bool IEC61850Server = false;// Set bool for 61850Server

        public static string SlaveNum = "";//For Import IEC61850Server
        public static string IEC61850SNode;
        public static string ICDPath = "";
    }
}
