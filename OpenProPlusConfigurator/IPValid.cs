using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace OpenProPlusConfigurator
{
    public class IPValid
    {
     static public string ReturnSubnetmask(String ipaddress)
     {
         uint firstOctet = ReturnFirtsOctet(ipaddress);
         if (firstOctet >= 0 && firstOctet <= 127)
             return "255.0.0.0";

         else if (firstOctet >= 128 && firstOctet <= 191)
            return "255.255.0.0";

         else if (firstOctet >= 192 && firstOctet <= 223)
             return "255.255.255.0";

         else return "0.0.0.0";
    }
     static public uint ReturnFirtsOctet(string ipAddress)
     {
         System.Net.IPAddress iPAddress = System.Net.IPAddress.Parse(ipAddress);
         byte[] byteIP = iPAddress.GetAddressBytes();
         uint ipInUint = (uint)byteIP[0]; 
         return ipInUint;
     }

   }
    }

   

