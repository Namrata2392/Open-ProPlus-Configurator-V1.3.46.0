using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace OpenProPlusConfigurator
{

    class Program
    {
        public static bool boolModifyXML = false;
        public static bool Debug = false;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        //Ajay: 23/11/2018 Commented
        //static void Main()
        //{
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.Run(new frmOpenProPlus());
        //}

        //Ajay: 23/11/2018
        static void Main(String[] args)
        {
            //ProtocolGateway.ProtocolGatewayConfigExist = false; //Ajay: 04/01/2019

            //D:\ADR245BM00_215__3H_V4.03\ProtocolGateway\SDDD.zip D:\ADR245BM00_215__3H_V4.03\ProtocolGateway\ProtocolGateway.config 10.0.2.213 sa pass@123 0.0.0.0 255.0.0.0 0.0.0.0 True True  "Fail Over" eth0

            //"D:\ADR245BM00_215__3H_V4.03\ProtocolGateway\0011.xml" "D:\ADR245BM00_215__3H_V4.03\ProtocolGateway\ProtocolGateway.config" 0.0.0.0 openproftp openftp#123
            if (args.Length > 0)
            {
                ProtocolGateway.AppMode = ProtocolGateway.AppModes.Restricted;

                if (Process.GetProcessesByName("OpenProPlusConfigurator").Count() > 1)
                {
                    MessageBox.Show("OpenProPlus Configurator is already running.", "OpenProPlus Configurator", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Environment.Exit(0);
                    return;
                }
                else { }
                //Namrata: 13/12/2019
                if(args.Length>5)
                {
                    boolModifyXML = true;
                }
                else
                {
                    boolModifyXML = false;
                }

                ProtocolGateway.OppConfigurationFile = args[0];
                if (args.Length > 1) { ProtocolGateway.ProtocolGatewayConfigurationFile = args[1]; }
                if (args.Length > 2) { ProtocolGateway.OppIpAddress = args[2]; }
                if (args.Length > 3) { ProtocolGateway.User = args[3]; }
                if (args.Length > 4) { ProtocolGateway.Password = args[4]; }
                //Namrata: 21/11/2019
                if (args.Length > 5) { ProtocolGateway.NwIP = args[5]; }
                if (args.Length > 7) { ProtocolGateway.NwSubnetMask = args[6]; }
                if (args.Length > 6) { ProtocolGateway.NwGateway = args[7]; }
                if (args.Length > 8) { ProtocolGateway.NwEthernetMode = args[8]; }
                if (args.Length > 9) { ProtocolGateway.NwEth0Check = args[9]; }
                if (args.Length > 10) { ProtocolGateway.NwEth1Check = args[10]; }
                if (args.Length > 11) { ProtocolGateway.NwMode = args[11]; }
                if (args.Length > 12) { ProtocolGateway.NwprimaryDevice = args[12]; }

                //Ajay: 08/01/2019
                //if (args.Length > 5) { ProtocolGateway.NwIP = args[5]; }
                //if (args.Length > 6) { ProtocolGateway.NwGateway = args[6]; }
                //if (args.Length > 7) { ProtocolGateway.NwSubnetMask = args[7]; }
                //if (args.Length > 8) { ProtocolGateway.NwEthernetMode = args[8]; }

                ProtocolGateway.Protocol = "HTTP"; //default set to "HTTP" Protocol when calling from RTV2
                ProtocolGatewayConfigSetVisible(false);
                ProtocolGatewayConfigSetReadOnly(true);
                if (Debug)
                {
                    MessageBox.Show("OppConfigurationFile=" + ProtocolGateway.OppConfigurationFile + "; ProtocolGatewayConfigurationFile=" + ProtocolGateway.ProtocolGatewayConfigurationFile + "; OppIpAddress=" + ProtocolGateway.OppIpAddress + "; FTPUser=" + ProtocolGateway.User);
                    //Ajay: 08/01/2019
                    //"NwIP=" + ProtocolGateway.NwIP + "; NwGateway=" + ProtocolGateway.NwGateway + "; NwSubnetMask=" + ProtocolGateway.NwSubnetMask + "; NwEthernetMode=" + ProtocolGateway.NwEthernetMode);
                }
            }
            else
            {
                //if (!string.IsNullOrEmpty(Globals.RESOURCES_PATH))
                //{
                //    if (File.Exists(Path.Combine(Globals.RESOURCES_PATH, "ProtocolGateway.config")))
                //    {
                //        ProtocolGateway.ProtocolGatewayConfigExist = true;
                //    }
                //}
                ProtocolGateway.AppMode = ProtocolGateway.AppModes.Full;
                //if (ProtocolGateway.ProtocolGatewayConfigExist) { ProtocolGateway.AppMode = ProtocolGateway.AppModes.Restricted; }
                //else { ProtocolGateway.AppMode = ProtocolGateway.AppModes.Full; }
                ProtocolGateway.OppConfigurationFile = "";
                ProtocolGateway.ProtocolGatewayConfigurationFile = "";
                //if(ProtocolGateway.ProtocolGatewayConfigExist) { ProtocolGateway.ProtocolGatewayConfigurationFile = Path.Combine(Globals.RESOURCES_PATH, "ProtocolGateway.config"); }
                ProtocolGateway.OppIpAddress = "";
                ProtocolGateway.User = "";
                ProtocolGateway.Password = "";
                ProtocolGateway.Protocol = "HTTP";
                ProtocolGatewayConfigSetVisible(true);
                ProtocolGatewayConfigSetReadOnly(false);
            }
            //ProtocolGateway.Protocol = "HTTP";
            //ProtocolGateway.AppMode = ProtocolGateway.AppModes.Restricted;
            //ProtocolGateway.OppIpAddress = "10.0.2.213";
            //ProtocolGateway.User = "openproftp";
            //ProtocolGateway.Password = "openftp#123";
            //ProtocolGateway.OppConfigurationFile = @"C:\Users\ajayp\Downloads\openproplus_config.xml";
            //ProtocolGateway.ProtocolGatewayConfigurationFile = @"D:\SWTrainee\SCADA\OpenProPlus Configurator\Open ProPlus Configurator_27.12.2018 -  V1.2.14 - DR Config Hidden\Open ProPlus Configurator\OpenProPlusConfigurator\bin\Debug\resources\ProtocolGateway.config";

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmOpenProPlus());
        }

        private static void ProtocolGatewayConfigSetVisible(bool val)
        {
            try
            {
                ProtocolGateway.OppDetails_Visible = val;
                ProtocolGateway.OppNetWorkConfiguration_Visible = val;
                ProtocolGateway.OppSerialPortConfiguration_Visible = val;
                ProtocolGateway.OppSystemConfiguration_Visible = val;
                ProtocolGateway.OppSlaveConfiguration_Visible = val;
                ProtocolGateway.OppIEC104SlaveGroup_Visible = val;
                ProtocolGateway.OppMODBUSSlaveGroup_Visible = val;
                ProtocolGateway.OppIEC101SlaveGroup_Visible = val;
                ProtocolGateway.OppIEC61850SlaveGroup_Visible = val;
                ProtocolGateway.OppSPORTSlaveGroup_Visible = val;
                ProtocolGateway.OppMasterConfiguration_Visible = val;
                ProtocolGateway.OppADRGroup_Visible = val;
                ProtocolGateway.OppIEC101Group_Visible = val;
                ProtocolGateway.OppIEC103Group_Visible = val;
                ProtocolGateway.OppMODBUSGroup_Visible = val;
                ProtocolGateway.OppIEC61850Group_Visible = val;
                ProtocolGateway.OppIEC104Group_Visible = val;
                ProtocolGateway.OppSPORTGroup_Visible = val;
                ProtocolGateway.OppVirtualGroup_Visible = val;
                ProtocolGateway.OppLoadProfileGroup_Visible = val;
                ProtocolGateway.OppMQTTSlaveGroup_Visible = val;//Namrata:02/04/2019
                ProtocolGateway.OppSMSSlaveGroup_Visible = val;//Namrata:02/04/2019
                ProtocolGateway.OppParameterLoadConfiguration_Visible = val;
                ProtocolGateway.OppGraphicalDisplaySlaveGroup_Visible = val;
                ProtocolGateway.OppDNP3SlaveGroup_Visible = val;
            }
            catch { }
        }
        private static void ProtocolGatewayConfigSetReadOnly(bool val)
        {
            try
            {
                ProtocolGateway.OppDetails_ReadOnly = val;
                ProtocolGateway.OppSMSSlaveGroup_ReadOnly = val;
                ProtocolGateway.OppMQTTSlaveGroup_ReadOnly = val;
                ProtocolGateway.OppNetWorkConfiguration_ReadOnly = val;
                ProtocolGateway.OppSerialPortConfiguration_ReadOnly = val;
                ProtocolGateway.OppSystemConfiguration_ReadOnly = val;
                ProtocolGateway.OppSlaveConfiguration_ReadOnly = val;
                ProtocolGateway.OppDNP3SlaveGroup_ReadOnly = val;
                ProtocolGateway.OppIEC104SlaveGroup_ReadOnly = val;
                ProtocolGateway.OppMODBUSSlaveGroup_ReadOnly = val;
                ProtocolGateway.OppIEC101SlaveGroup_ReadOnly = val;
                ProtocolGateway.OppIEC61850SlaveGroup_ReadOnly = val;
                ProtocolGateway.OppSPORTSlaveGroup_ReadOnly = val;
                ProtocolGateway.OppGraphicalDisplaySlaveGroup_ReadOnly = val;
                ProtocolGateway.OppMasterConfiguration_ReadOnly = val;
                ProtocolGateway.OppADRGroup_ReadOnly = val;
                ProtocolGateway.OppIEC101Group_ReadOnly = val;
                ProtocolGateway.OppIEC103Group_ReadOnly = val;
                ProtocolGateway.OppMODBUSGroup_ReadOnly = val;
                ProtocolGateway.OppIEC61850Group_ReadOnly = val;
                ProtocolGateway.OppIEC104Group_ReadOnly = val;
                ProtocolGateway.OppSPORTGroup_ReadOnly = val;
                ProtocolGateway.OppVirtualGroup_ReadOnly = val;
                ProtocolGateway.OppLoadProfileGroup_ReadOnly = val;
                ProtocolGateway.OppParameterLoadConfiguration_ReadOnly = val;
                ProtocolGateway.OppDNP3SlaveGroup_ReadOnly = val;
            }
            catch { }
        }
    }
}
