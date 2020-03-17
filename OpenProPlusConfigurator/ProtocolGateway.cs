using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OpenProPlusConfigurator
{
    public static class ProtocolGateway
    {

        #region Declaration
        private static string oppConfigFile;
        private static string ProtocolGatewayConfigFile;
        private static string oppIpAddress;
        private static string user;
        private static string password;
        private static string protocol;
        private static string oppConfigurationZippedFile;
        //Namrata: 21/11/2019
        private static string nwIP;
        private static string nwGateway;
        private static string nwSubnetmask;
        private static string nwEthernetmode;
        private static string nwEth0Check;
        private static string nwEth1Check;
        private static string nwmode;
        private static string nwPrimaryDevice;



        //Ajay: 08/01/2019
        //private static string nwIP;
        //private static string nwGateway;
        //private static string nwSubnetmask;
        //private static string nwEthernetmode;

        private static bool oppDetails_Visible = false;
        private static bool oppDetails_ReadOnly = false;

        private static bool oppNetWorkConfig_Visible = false;
        private static bool oppNetWorkConfig_ReadOnly = false;

        private static bool oppSerialPortConfig_Visible = false;
        private static bool oppSerialPortConfig_ReadOnly = false;

        private static bool oppSystemConfig_Visible = false;
        private static bool oppSystemConfig_ReadOnly = false;

        private static bool oppSlaveConfig_Visible = false;
        private static bool oppSlaveConfig_ReadOnly = false;

        private static bool oppIEC104SlaveGroup_Visible = false;
        private static bool oppIEC104SlaveGroup_ReadOnly = false;

        private static bool oppMODBUSSlaveGroup_Visible = false;
        private static bool oppMODBUSSlaveGroup_ReadOnly = false;

        private static bool oppDNP3SlaveGroup_Visible = false;
        private static bool oppDNP3SlaveGroup_ReadOnly = false;

        private static bool oppIEC101SlaveGroup_Visible = false;
        private static bool oppIEC101SlaveGroup_ReadOnly = false;

        private static bool oppIEC61850SlaveGroup_Visible = false;
        private static bool oppIEC61850SlaveGroup_ReadOnly = false;

        private static bool oppSPORTSlaveGroup_Visible = false;
        private static bool oppSPORTSlaveGroup_ReadOnly = false;

        private static bool oppMasterConfig_Visible = false;
        private static bool oppMasterConfig_ReadOnly = false;

        private static bool oppADRGroup_Visible = false;
        private static bool oppADRGroup_ReadOnly = false;

        private static bool oppIEC101Group_Visible = false;
        private static bool oppIEC101Group_ReadOnly = false;

        private static bool oppIEC103Group_Visible = false;
        private static bool oppIEC103Group_ReadOnly = false;

        private static bool oppMODBUSGroup_Visible = false;
        private static bool oppMODBUSGroup_ReadOnly = false;

        private static bool oppIEC61850Group_Visible = false;
        private static bool oppIEC61850Group_ReadOnly = false;

        private static bool oppIEC104Group_Visible = false;
        private static bool oppIEC104Group_ReadOnly = false;

        private static bool oppSPORTGroup_Visible = false;
        private static bool oppSPORTGroup_ReadOnly = false;

        private static bool oppVirtualGroup_Visible = false;
        private static bool oppVirtualGroup_ReadOnly = false;

        private static bool oppLoadProfileGroup_Visible = false;
        private static bool oppLoadProfileGroup_ReadOnly = false;

        private static bool oppParameterLoadConfig_Visible = false;
        private static bool oppParameterLoadConfig_ReadOnly = false;

        //Namrata: 09/08/2019
        private static bool oppGraphicalDisplaySlaveGroup_Visible = false;
        private static bool oppGraphicalDisplaySlaveGroup_ReadOnly = false;

        //Namrata: 02/04/2019
        private static bool oppMQTTSlaveGroup_Visible = false;
        private static bool oppMQTTSlaveGroup_ReadOnly = false;

        //Namrata: 25/05/2019
        private static bool oppSMSSlaveGroup_Visible = false;
        private static bool oppSMSSlaveGroup_ReadOnly = false;
        #endregion Declaration
        public enum AppModes
        {
            Full,
            Restricted,
            NONE
        }

        public static AppModes AppMode = AppModes.NONE;
        //public static bool ProtocolGatewayConfigExist = false; //Ajay: 04/01/2019

        public static string OppConfigurationFile
        {
            get { return oppConfigFile; }
            set { oppConfigFile = value; }
        }
        public static string ProtocolGatewayConfigurationFile
        {
            get { return ProtocolGatewayConfigFile; }
            set { ProtocolGatewayConfigFile = value; }
        }
        public static string OppIpAddress
        {
            get { return oppIpAddress; }
            set { oppIpAddress = value; }
        }
        public static string User
        {
            get { return user; }
            set { user = value; }
        }
        public static string Password
        {
            get { return password; }
            set { password = value; }
        }
        public static string Protocol
        {
            get { return protocol; }
            set { protocol = value; }
        }

        //Namrata: 21/11/2019
        public static string OppConfigurationZippedFile
        {
            get { return oppConfigurationZippedFile; }
            set { oppConfigurationZippedFile = value; }
        }
        public static bool OppDetails_Visible
        {
            get { return oppDetails_Visible; }
            set { oppDetails_Visible = value; }
        }
        public static bool OppDetails_ReadOnly
        {
            get { return oppDetails_ReadOnly; }
            set { oppDetails_ReadOnly = value; }
        }
        public static bool OppNetWorkConfiguration_Visible
        {
            get { return oppNetWorkConfig_Visible; }
            set { oppNetWorkConfig_Visible = value; }
        }
        public static bool OppNetWorkConfiguration_ReadOnly
        {
            get { return oppNetWorkConfig_ReadOnly; }
            set { oppNetWorkConfig_ReadOnly = value; }
        }
        public static bool OppSerialPortConfiguration_Visible
        {
            get { return oppSerialPortConfig_Visible; }
            set { oppSerialPortConfig_Visible = value; }
        }
        public static bool OppSerialPortConfiguration_ReadOnly
        {
            get { return oppSerialPortConfig_ReadOnly; }
            set { oppSerialPortConfig_ReadOnly = value; }
        }
        public static bool OppSystemConfiguration_Visible
        {
            get { return oppSystemConfig_Visible; }
            set { oppSystemConfig_Visible = value; }
        }
        public static bool OppSystemConfiguration_ReadOnly
        {
            get { return oppSystemConfig_ReadOnly; }
            set { oppSystemConfig_ReadOnly = value; }
        }
        public static bool OppSlaveConfiguration_Visible
        {
            get { return oppSlaveConfig_Visible; }
            set { oppSlaveConfig_Visible = value; }
        }
        public static bool OppSlaveConfiguration_ReadOnly
        {
            get { return oppSlaveConfig_ReadOnly; }
            set { oppSlaveConfig_ReadOnly = value; }
        }

        public static bool OppDNP3SlaveGroup_Visible
        {
            get { return oppDNP3SlaveGroup_Visible; }
            set { oppDNP3SlaveGroup_Visible = value; }
        }
        public static bool OppDNP3SlaveGroup_ReadOnly
        {
            get { return oppDNP3SlaveGroup_ReadOnly; }
            set { oppDNP3SlaveGroup_ReadOnly = value; }
        }
        public static bool OppIEC104SlaveGroup_Visible
        {
            get { return oppIEC104SlaveGroup_Visible; }
            set { oppIEC104SlaveGroup_Visible = value; }
        }
        public static bool OppIEC104SlaveGroup_ReadOnly
        {
            get { return oppIEC104SlaveGroup_ReadOnly; }
            set { oppIEC104SlaveGroup_ReadOnly = value; }
        }
        public static bool OppMODBUSSlaveGroup_Visible
        {
            get { return oppMODBUSSlaveGroup_Visible; }
            set { oppMODBUSSlaveGroup_Visible = value; }
        }
        public static bool OppMODBUSSlaveGroup_ReadOnly
        {
            get { return oppMODBUSSlaveGroup_ReadOnly; }
            set { oppMODBUSSlaveGroup_ReadOnly = value; }
        }
        public static bool OppIEC101SlaveGroup_Visible
        {
            get { return oppIEC101SlaveGroup_Visible; }
            set { oppIEC101SlaveGroup_Visible = value; }
        }
        public static bool OppIEC101SlaveGroup_ReadOnly
        {
            get { return oppIEC101SlaveGroup_ReadOnly; }
            set { oppIEC101SlaveGroup_ReadOnly = value; }
        }
        public static bool OppIEC61850SlaveGroup_Visible
        {
            get { return oppIEC61850SlaveGroup_Visible; }
            set { oppIEC61850SlaveGroup_Visible = value; }
        }
        public static bool OppIEC61850SlaveGroup_ReadOnly
        {
            get { return oppIEC61850SlaveGroup_ReadOnly; }
            set { oppIEC61850SlaveGroup_ReadOnly = value; }
        }
        public static bool OppSPORTSlaveGroup_Visible
        {
            get { return oppSPORTSlaveGroup_Visible; }
            set { oppSPORTSlaveGroup_Visible = value; }
        }
        public static bool OppSPORTSlaveGroup_ReadOnly
        {
            get { return oppSPORTSlaveGroup_ReadOnly; }
            set { oppSPORTSlaveGroup_ReadOnly = value; }
        }

        //Namrata: 09/08/2019
        public static bool OppGraphicalDisplaySlaveGroup_Visible
        {
            get { return oppGraphicalDisplaySlaveGroup_Visible; }
            set { oppGraphicalDisplaySlaveGroup_Visible = value; }
        }
        public static bool OppGraphicalDisplaySlaveGroup_ReadOnly
        {
            get { return oppGraphicalDisplaySlaveGroup_ReadOnly; }
            set { oppGraphicalDisplaySlaveGroup_ReadOnly = value; }
        }
        public static bool OppMasterConfiguration_Visible
        {
            get { return oppMasterConfig_Visible; }
            set { oppMasterConfig_Visible = value; }
        }
        public static bool OppMasterConfiguration_ReadOnly
        {
            get { return oppMasterConfig_ReadOnly; }
            set { oppMasterConfig_ReadOnly = value; }
        }
        public static bool OppADRGroup_Visible
        {
            get { return oppADRGroup_Visible; }
            set { oppADRGroup_Visible = value; }
        }
        public static bool OppADRGroup_ReadOnly
        {
            get { return oppADRGroup_ReadOnly; }
            set { oppADRGroup_ReadOnly = value; }
        }
        public static bool OppIEC101Group_Visible
        {
            get { return oppIEC101Group_Visible; }
            set { oppIEC101Group_Visible = value; }
        }
        public static bool OppIEC101Group_ReadOnly
        {
            get { return oppIEC101Group_ReadOnly; }
            set { oppIEC101Group_ReadOnly = value; }
        }
        public static bool OppIEC103Group_Visible
        {
            get { return oppIEC103Group_Visible; }
            set { oppIEC103Group_Visible = value; }
        }
        public static bool OppIEC103Group_ReadOnly
        {
            get { return oppIEC103Group_ReadOnly; }
            set { oppIEC103Group_ReadOnly = value; }
        }
        public static bool OppMODBUSGroup_Visible
        {
            get { return oppMODBUSGroup_Visible; }
            set { oppMODBUSGroup_Visible = value; }
        }
        public static bool OppMODBUSGroup_ReadOnly
        {
            get { return oppMODBUSGroup_ReadOnly; }
            set { oppMODBUSGroup_ReadOnly = value; }
        }
        public static bool OppIEC61850Group_Visible
        {
            get { return oppIEC61850Group_Visible; }
            set { oppIEC61850Group_Visible = value; }
        }
        public static bool OppIEC61850Group_ReadOnly
        {
            get { return oppIEC61850Group_ReadOnly; }
            set { oppIEC61850Group_ReadOnly = value; }
        }
        public static bool OppIEC104Group_Visible
        {
            get { return oppIEC104Group_Visible; }
            set { oppIEC104Group_Visible = value; }
        }
        public static bool OppIEC104Group_ReadOnly
        {
            get { return oppIEC104Group_ReadOnly; }
            set { oppIEC104Group_ReadOnly = value; }
        }
        public static bool OppSPORTGroup_Visible
        {
            get { return oppSPORTGroup_Visible; }
            set { oppSPORTGroup_Visible = value; }
        }
        public static bool OppSPORTGroup_ReadOnly
        {
            get { return oppSPORTGroup_ReadOnly; }
            set { oppSPORTGroup_ReadOnly = value; }
        }
        public static bool OppVirtualGroup_Visible
        {
            get { return oppVirtualGroup_Visible; }
            set { oppVirtualGroup_Visible = value; }
        }
        public static bool OppVirtualGroup_ReadOnly
        {
            get { return oppVirtualGroup_ReadOnly; }
            set { oppVirtualGroup_ReadOnly = value; }
        }
        public static bool OppLoadProfileGroup_Visible
        {
            get { return oppLoadProfileGroup_Visible; }
            set { oppLoadProfileGroup_Visible = value; }
        }
        public static bool OppLoadProfileGroup_ReadOnly
        {
            get { return oppLoadProfileGroup_ReadOnly; }
            set { oppLoadProfileGroup_ReadOnly = value; }
        }
        public static bool OppParameterLoadConfiguration_Visible
        {
            get { return oppParameterLoadConfig_Visible; }
            set { oppParameterLoadConfig_Visible = value; }
        }
        public static bool OppParameterLoadConfiguration_ReadOnly
        {
            get { return oppParameterLoadConfig_ReadOnly; }
            set { oppParameterLoadConfig_ReadOnly = value; }
        }

        public static bool OppMQTTSlaveGroup_Visible
        {
            get { return oppMQTTSlaveGroup_Visible; }
            set { oppMQTTSlaveGroup_Visible = value; }
        }
        public static bool OppSMSSlaveGroup_Visible
        {
            get { return oppSMSSlaveGroup_Visible; }
            set { oppSMSSlaveGroup_Visible = value; }
        }
        public static bool OppMQTTSlaveGroup_ReadOnly
        {
            get { return oppMQTTSlaveGroup_ReadOnly; }
            set { oppMQTTSlaveGroup_ReadOnly = value; }
        }

        public static bool OppSMSSlaveGroup_ReadOnly
        {
            get { return oppSMSSlaveGroup_ReadOnly; }
            set { oppSMSSlaveGroup_ReadOnly = value; }
        }
        //Ajay: 08/01/2019
        //public static string NwIP
        //{
        //    get { return nwIP; }
        //    set { nwIP = value; }
        //}
        //Ajay: 08/01/2019
        //public static string NwGateway
        //{
        //    get { return nwGateway; }
        //    set { nwGateway = value; }
        //}
        //Ajay: 08/01/2019
        //public static string NwSubnetMask
        //{
        //    get { return nwSubnetmask; }
        //    set { nwSubnetmask = value; }
        //}
        //Ajay: 08/01/2019
        //public static string NwEthernetMode
        //{
        //    get { return nwEthernetmode; }
        //    set { nwEthernetmode = value; }
        //}


        //Namrata:21/11/2019
        public static string NwIP
        {
            get { return nwIP; }
            set { nwIP = value; }
        }
        public static string NwGateway
        {
            get { return nwGateway; }
            set { nwGateway = value; }
        }
        public static string NwSubnetMask
        {
            get { return nwSubnetmask; }
            set { nwSubnetmask = value; }
        }
        public static string NwEthernetMode
        {
            get { return nwEthernetmode; }
            set { nwEthernetmode = value; }
        }
        public static string NwEth0Check
        {
            get { return nwEth0Check; }
            set { nwEth0Check = value; }
        }
        public static string NwEth1Check
        {
            get { return nwEth1Check; }
            set { nwEth1Check = value; }
        }
        public static string NwMode
        {
            get { return nwmode; }
            set { nwmode = value; }
        }
        public static string NwprimaryDevice
        {
            get { return nwPrimaryDevice; }
            set { nwPrimaryDevice = value; }
        }
        //private static string nwEth0Check;
        //private static string nwEth1Check;
        //private static string nwmode;
        //private static string nwPrimaryDevice;

    }
}
