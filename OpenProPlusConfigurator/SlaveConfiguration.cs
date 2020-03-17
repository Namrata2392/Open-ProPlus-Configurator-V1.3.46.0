using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace OpenProPlusConfigurator
{
    /**
    * \brief     <b>SlaveConfiguration</b> is a class to store different types of slaves.
    * \details   This class stores information about various slave types like IEC104, MODBUS Slave, etc.
    * It also exports the XML node related to this object.
    * 
    */
    public class SlaveConfiguration
    {
        private string rnName = "SlaveConfiguration";
        private IEC104Group iec104Grp = new IEC104Group();
        private MODBUSSlaveGroup mbSlaveGrp = new MODBUSSlaveGroup();
        private IEC101SlaveGroup iec101Grp = new IEC101SlaveGroup();
        private IEC61850ServerSlaveGroup server61850Slave = new IEC61850ServerSlaveGroup();
        //Ajay: 02/07/2018
        private SPORTSlaveGroup sportSlaveGrp = new SPORTSlaveGroup();
        ucSlaveConfiguration ucsc = new ucSlaveConfiguration();

        GraphicalDisplaySlaveGroup GraphicalDisplaySlaveGroup;
        private MQTTSalveGroup MqttSlaveGrp = new MQTTSalveGroup(); //Namrata: 01/04/2019

        DNPSlaveGroup DNPSlave;
        DNP3DNPSA Dnp3dnpsa;
       
        //Namrata: 22/11/2019
        private SMSSlaveGroup sMS = new SMSSlaveGroup();
        private DNPSlaveGroup Dnp = new DNPSlaveGroup();
        //Namrata: 16/12/2019
        private GraphicalDisplaySlaveGroup GDS = new GraphicalDisplaySlaveGroup();

        SMSSlaveGroup SMSGroup;
        public SlaveConfiguration(Dictionary<string, TreeNode> treeDicts)
        {
            string strRoutineName = "SlaveConfiguration";
            try
            {
                foreach (KeyValuePair<string, TreeNode> mckp in treeDicts)
                {
                    if (mckp.Key == "GraphicalDisplaySlaveGroup")
                    {
                        GraphicalDisplaySlaveGroup = new GraphicalDisplaySlaveGroup(treeDicts["GraphicalDisplaySlaveGroup"]);
                    }
                    if (mckp.Key == "SMSSlaveGroup")
                    {
                        SMSGroup = new SMSSlaveGroup(treeDicts["SMSSlaveGroup"]);
                    }
                    //Namrata: 06/11/2019
                    if (mckp.Key == "DNP3SlaveGroup")
                    {
                        DNPSlave = new DNPSlaveGroup(treeDicts["DNP3SlaveGroup"]);
                        //Dnp3dnpsa = new DNP3DNPSA(treeDicts["User"]);
                    }
                    else
                    {
                        Console.WriteLine("***** MasterConfiguration: Node '{0}' not supported!!!", mckp.Key);
                    }
                }
                addListHeaders();
                refreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public SlaveConfiguration()
        {
            string strRoutineName = "SlaveConfiguration";
            try
            {
                addListHeaders();
                refreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addListHeaders()
        {
            string strRoutineName = "addListHeaders";
            try
            {
                ucsc.lvSlaveConfiguration.Columns.Add("No.", 60, HorizontalAlignment.Left);
                ucsc.lvSlaveConfiguration.Columns.Add("Description", 150, HorizontalAlignment.Left);
                ucsc.lvSlaveConfiguration.Columns.Add("Total", 100, HorizontalAlignment.Left);
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
                int iCount = 0; //Ajay: 23/11/2018
                int rowCnt = 0;
                //Slave Configuration...
                ucsc.lvSlaveConfiguration.Items.Clear();
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Full) //Ajay: 23/11/2018
                {
                    iCount = 0;
                    if (iec104Grp != null) { iCount = iec104Grp.getCount(); }
                    string[] row1 = { "1", "IEC104", iCount.ToString() };
                    ListViewItem lvItem1 = new ListViewItem(row1);
                    if (rowCnt++ % 2 == 0) lvItem1.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucsc.lvSlaveConfiguration.Items.Add(lvItem1);

                    iCount = 0;
                    if (mbSlaveGrp != null) { iCount = mbSlaveGrp.getCount(); }
                    string[] row2 = { "2", "MODBUS", iCount.ToString() };
                    ListViewItem lvItem2 = new ListViewItem(row2);
                    if (rowCnt++ % 2 == 0) lvItem2.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucsc.lvSlaveConfiguration.Items.Add(lvItem2);

                    iCount = 0;
                    if (iec101Grp != null) { iCount = iec101Grp.getCount(); }
                    string[] row3 = { "3", "IEC101", iCount.ToString() };
                    ListViewItem lvItem3 = new ListViewItem(row3);
                    if (rowCnt++ % 2 == 0) lvItem3.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucsc.lvSlaveConfiguration.Items.Add(lvItem3);

                    iCount = 0;
                    if (server61850Slave != null) { iCount = server61850Slave.getCount(); }
                    string[] row4 = { "4", "IEC61850 Server", iCount.ToString() };
                    ListViewItem lvItem4 = new ListViewItem(row4);
                    if (rowCnt++ % 2 == 0) lvItem4.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucsc.lvSlaveConfiguration.Items.Add(lvItem4);

                    iCount = 0;
                    if (sportSlaveGrp != null) { iCount = sportSlaveGrp.getCount(); }
                    //Ajay: 02/07/2018
                    string[] row5 = { "5", "SPORT", iCount.ToString() };
                    ListViewItem lvItem5 = new ListViewItem(row5);
                    if (rowCnt++ % 2 == 0) lvItem5.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucsc.lvSlaveConfiguration.Items.Add(lvItem5);

                    iCount = 0;
                    if (GraphicalDisplaySlaveGroup != null) { iCount = GraphicalDisplaySlaveGroup.getCount(); }
                    //Ajay: 02/07/2018
                    string[] row6 = { "6", "GraphicalDisplay", iCount.ToString() };
                    ListViewItem lvItem6 = new ListViewItem(row6);
                    if (rowCnt++ % 2 == 0) lvItem6.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucsc.lvSlaveConfiguration.Items.Add(lvItem6);
                }
                //Ajay: 23/11/2018
                else if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    if (ProtocolGateway.OppIEC104SlaveGroup_Visible)
                    {
                        iCount = 0;
                        if (iec104Grp != null) { iCount = iec104Grp.getCount(); }
                        string[] row1 = { "1", "IEC104", iCount.ToString() };
                        ListViewItem lvItem1 = new ListViewItem(row1);
                        if (rowCnt++ % 2 == 0) lvItem1.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucsc.lvSlaveConfiguration.Items.Add(lvItem1);
                    }
                    else { }
                    if (ProtocolGateway.OppMODBUSSlaveGroup_Visible)
                    {
                        iCount = 0;
                        if (mbSlaveGrp != null) { iCount = mbSlaveGrp.getCount(); }
                        string[] row2 = { "2", "MODBUS", iCount.ToString() };
                        ListViewItem lvItem2 = new ListViewItem(row2);
                        if (rowCnt++ % 2 == 0) lvItem2.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucsc.lvSlaveConfiguration.Items.Add(lvItem2);
                    }
                    else { }
                    if (ProtocolGateway.OppIEC101SlaveGroup_Visible)
                    {
                        iCount = 0;
                        if (iec101Grp != null) { iCount = iec101Grp.getCount(); }
                        string[] row3 = { "3", "IEC101", iCount.ToString() };
                        ListViewItem lvItem3 = new ListViewItem(row3);
                        if (rowCnt++ % 2 == 0) lvItem3.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucsc.lvSlaveConfiguration.Items.Add(lvItem3);
                    }
                    else { }
                    if (ProtocolGateway.OppIEC61850SlaveGroup_Visible)
                    {
                        iCount = 0;
                        if (server61850Slave != null) { iCount = server61850Slave.getCount(); }
                        string[] row4 = { "4", "IEC61850 Server", iCount.ToString() };
                        ListViewItem lvItem4 = new ListViewItem(row4);
                        if (rowCnt++ % 2 == 0) lvItem4.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucsc.lvSlaveConfiguration.Items.Add(lvItem4);
                    }
                    else { }
                    if (ProtocolGateway.OppSPORTSlaveGroup_Visible)
                    {
                        iCount = 0;
                        if (sportSlaveGrp != null) { iCount = sportSlaveGrp.getCount(); }
                        //Ajay: 02/07/2018
                        string[] row5 = { "5", "SPORT", iCount.ToString() };
                        ListViewItem lvItem5 = new ListViewItem(row5);
                        if (rowCnt++ % 2 == 0) lvItem5.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucsc.lvSlaveConfiguration.Items.Add(lvItem5);
                    }
                    else { }
                    //Namrata:02/04/2019
                    if (ProtocolGateway.OppMQTTSlaveGroup_Visible)
                    {
                        iCount = 0;
                        if (MqttSlaveGrp != null) { iCount = MqttSlaveGrp.getCount(); }
                        string[] row6 = { "6", "MQTT", iCount.ToString() };
                        ListViewItem lvItem6 = new ListViewItem(row6);
                        if (rowCnt++ % 2 == 0) lvItem6.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucsc.lvSlaveConfiguration.Items.Add(lvItem6);
                    }
                    else { }

                    //Namrata: 25/05/2019
                    if (ProtocolGateway.OppSMSSlaveGroup_Visible)
                    {
                        iCount = 0;
                        if (SMSGroup != null) { iCount = SMSGroup.getCount(); }
                        string[] row7 = { "7", "SMS", iCount.ToString() };
                        ListViewItem lvItem7 = new ListViewItem(row7);
                        if (rowCnt++ % 2 == 0) lvItem7.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucsc.lvSlaveConfiguration.Items.Add(lvItem7);
                    }
                    else { }
                    //Namrata: 09/08/2019
                    if (ProtocolGateway.OppGraphicalDisplaySlaveGroup_Visible)
                    {
                        iCount = 0;
                        if (GraphicalDisplaySlaveGroup != null) { iCount = GraphicalDisplaySlaveGroup.getCount(); }
                        //Ajay: 02/07/2018
                        string[] row8 = { "8", "GraphicalDisplay", iCount.ToString() };
                        ListViewItem lvItem8 = new ListViewItem(row8);
                        if (rowCnt++ % 2 == 0) lvItem8.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucsc.lvSlaveConfiguration.Items.Add(lvItem8);
                    }
                    else { }
                    if (ProtocolGateway.OppDNP3SlaveGroup_Visible)
                    {
                        iCount = 0;
                        if (DNPSlave != null) { iCount = DNPSlave.getCount(); }
                        //Ajay: 02/07/2018
                        string[] row9 = { "9", "DNP3", iCount.ToString() };
                        ListViewItem lvItem9 = new ListViewItem(row9);
                        if (rowCnt++ % 2 == 0) lvItem9.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucsc.lvSlaveConfiguration.Items.Add(lvItem9);
                    }
                    else { }
                }
                else { }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void parseSCNode(XmlNode sNode, Dictionary<string, TreeNode> treeDicts)
        {
            string strRoutineName = "parseSCNode";
            try
            {
                //First set root node name...
                rnName = sNode.Name;
                //tn.Nodes.Clear();
                foreach (XmlNode node in sNode)
                {
                    //if (node.Name == "IEC104Group") //Ajay: 23/11/2018 Commented
                    if (node.Name == "IEC104Group" && treeDicts.Keys.Contains("IEC104Group"))
                    {
                        iec104Grp.parseIECGNode(node, treeDicts["IEC104Group"]);
                    }
                    //else if (node.Name == "MODBUSSlaveGroup") //Ajay: 23/11/2018 Commented
                    else if (node.Name == "MODBUSSlaveGroup" && treeDicts.Keys.Contains("MODBUSSlaveGroup"))
                    {
                        mbSlaveGrp.parseMBSGNode(node, treeDicts["MODBUSSlaveGroup"]);
                    }
                    //else if (node.Name == "IEC101SlaveGroup") //Ajay: 23/11/2018 Commented
                    else if (node.Name == "IEC101SlaveGroup" && treeDicts.Keys.Contains("IEC101SlaveGroup"))
                    {
                        iec101Grp.parseIECGNode(node, treeDicts["IEC101SlaveGroup"]);
                    }
                    //else if (node.Name == "IEC61850ServerGroup") //Ajay: 23/11/2018 Commented
                    else if (node.Name == "IEC61850ServerGroup" && treeDicts.Keys.Contains("IEC61850ServerGroup"))
                    {
                        server61850Slave.parse61850ServerSlaveGNode(node, treeDicts["IEC61850ServerGroup"]);//IEC61850ServerSlaveGroup
                    }
                    //Ajay: 02/07/2018
                    //else if (node.Name == "SPORTSlaveGroup") //Ajay: 23/11/2018 Commented
                    else if (node.Name == "SPORTSlaveGroup" && treeDicts.Keys.Contains("SPORTSlaveGroup"))
                    {
                        sportSlaveGrp.parseIECGNode(node, treeDicts["SPORTSlaveGroup"]);
                    }
                    //Namrata:01/04/2019
                    else if (node.Name == "MQTTSlaveGroup" && treeDicts.Keys.Contains("MQTTSlaveGroup"))
                    {
                        MqttSlaveGrp.parseIECGNode(node, treeDicts["MQTTSlaveGroup"]);
                    }
                    //Namrata: 25/05/2019
                    else if (node.Name == "SMSSlaveGroup" && treeDicts.Keys.Contains("SMSSlaveGroup"))
                    {
                        sMS.parseIECGNode(node, treeDicts["SMSSlaveGroup"]);
                        SMSGroup.parseIECGNode(node, treeDicts["SMSSlaveGroup"]);
                    }
                    //Namrata: 09/08/2019
                    else if (node.Name == "GraphicalDisplaySlaveGroup" && treeDicts.Keys.Contains("GraphicalDisplaySlaveGroup"))
                    {
                        GDS.parseIECGNode(node, treeDicts["GraphicalDisplaySlaveGroup"]);
                        GraphicalDisplaySlaveGroup.parseIECGNode(node, treeDicts["GraphicalDisplaySlaveGroup"]);
                    }
                    //Namrata: 09/08/2019
                    else if (node.Name == "DNP3SlaveGroup" && treeDicts.Keys.Contains("DNP3SlaveGroup"))
                    {
                        Dnp.parseIECGNode(node, treeDicts["DNP3SlaveGroup"]);
                        DNPSlave.parseIECGNode(node, treeDicts["DNP3SlaveGroup"]);
                    }
                    else
                    {
                        Console.WriteLine("***** SlaveConfiguration: Node '{0}' not supported!!!", node.Name);
                    }
                }
                refreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("SlaveConfiguration_")) { refreshList(); return ucsc; }

            kpArr.RemoveAt(0);

            if (kpArr.ElementAt(0).Contains("IEC104Group_"))
            {
                if (iec104Grp == null) return null;
                return iec104Grp.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("MODBUSSlaveGroup_"))
            {
                if (mbSlaveGrp == null) return null;
                return mbSlaveGrp.getView(kpArr);
            }

            else if (kpArr.ElementAt(0).Contains("IEC101SlaveGroup_"))
            {
                if (iec101Grp == null) return null;
                return iec101Grp.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("IEC61850ServerGroup_"))//IEC61850ServerSlaveGroup
            {
                if (server61850Slave == null) return null;
                return server61850Slave.getView(kpArr);
            }
            //Ajay: 02/07/2018
            else if (kpArr.ElementAt(0).Contains("SPORTSlaveGroup_"))
            {
                if (sportSlaveGrp == null) return null;
                return sportSlaveGrp.getView(kpArr);
            }
            //Namrata:01/04/2019
            else if (kpArr.ElementAt(0).Contains("MQTTSlaveGroup_"))
            {
                if (MqttSlaveGrp == null) return null;
                return MqttSlaveGrp.getView(kpArr);
            }
            //Namrata: 25/05/2019
            else if (kpArr.ElementAt(0).Contains("SMSSlaveGroup_"))
            {
                if (SMSGroup == null) return null;
                return SMSGroup.getView(kpArr);
            }
            //Namrata: 09/08/2019
            else if (kpArr.ElementAt(0).Contains("GraphicalDisplaySlaveGroup_"))
            {
                if (GraphicalDisplaySlaveGroup == null) return null;
                return GraphicalDisplaySlaveGroup.getView(kpArr);
            }
            //Namrata: 06/11/2019
            else if (kpArr.ElementAt(0).Contains("DNP3SlaveGroup_"))
            {
                if (DNPSlave == null) return null;
                return DNPSlave.getView(kpArr);
            }
            return null;
        }
        public XmlNode exportXMLnode()
        {
            XmlDocument xmlDoc = new XmlDocument();
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);

            XmlNode rootNode = xmlDoc.CreateElement(rnName);
            xmlDoc.AppendChild(rootNode);
            //Maintain Sequence Of XML
            if (iec104Grp != null)
            {
                XmlNode importIECGNode = rootNode.OwnerDocument.ImportNode(iec104Grp.exportXMLnode(), true);
                rootNode.AppendChild(importIECGNode);
            }

            if (mbSlaveGrp != null)
            {
                XmlNode importMBSGNode = rootNode.OwnerDocument.ImportNode(mbSlaveGrp.exportXMLnode(), true);
                rootNode.AppendChild(importMBSGNode);
            }
            //Namrata:7/7/2017
            if (iec101Grp != null)
            {
                XmlNode importIECGNode = rootNode.OwnerDocument.ImportNode(iec101Grp.exportXMLnode(), true);
                rootNode.AppendChild(importIECGNode);
            }
            //Namrata:7/7/2017
            if (server61850Slave != null)
            {
                XmlNode importIECGNode = rootNode.OwnerDocument.ImportNode(server61850Slave.exportXMLnode(), true);
                rootNode.AppendChild(importIECGNode);
            }
            //Ajay: 02/07/2018
            if (sportSlaveGrp != null)
            {
                XmlNode importIECGNode = rootNode.OwnerDocument.ImportNode(sportSlaveGrp.exportXMLnode(), true);
                rootNode.AppendChild(importIECGNode);
            }
            //Namrata:01/04/2019
            if (MqttSlaveGrp != null)
            {
                XmlNode importIECGNode = rootNode.OwnerDocument.ImportNode(MqttSlaveGrp.exportXMLnode(), true);
                rootNode.AppendChild(importIECGNode);
            }

            //Namrata:25/05/2019
            if (SMSGroup != null)
            {
                XmlNode importIECGNode = rootNode.OwnerDocument.ImportNode(SMSGroup.exportXMLnode(), true);
                rootNode.AppendChild(importIECGNode);
            }
            //Namrata: 09/08/2019
            if (GraphicalDisplaySlaveGroup != null)
            {
                XmlNode importIECGNode = rootNode.OwnerDocument.ImportNode(GraphicalDisplaySlaveGroup.exportXMLnode(), true);
                rootNode.AppendChild(importIECGNode);
            }
            if (DNPSlave != null)
            {
                XmlNode importIECGNode = rootNode.OwnerDocument.ImportNode(DNPSlave.exportXMLnode(), true);
                rootNode.AppendChild(importIECGNode);
            }
            return rootNode;
        }
        public void regenerateSequence()
        {
            string strRoutineName = "regenerateAISequence";
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public IEC104Group getIEC104Group()
        {
            return iec104Grp;
        }
        public IEC101SlaveGroup getIEC101SlaveGroup()
        {
            return iec101Grp;
        }
        public MODBUSSlaveGroup getMODBUSSlaveGroup()
        {
            return mbSlaveGrp;
        }
        public IEC61850ServerSlaveGroup get61850SlaveGroup()
        {
            return server61850Slave;
        }
        //Ajay: 02/07/2018
        public SPORTSlaveGroup getSPORTSlaveGroup()
        {
            return sportSlaveGrp;
        }

        public GraphicalDisplaySlaveGroup getGraphicalDisplaySlaveGroup()
        {
            //Namrata: 22/11/2019
            if (SMSGroup == null)
            {
                return GDS;
            }
            else
            {
                return GraphicalDisplaySlaveGroup;
            }
            //return SMSGroup;

           // return GraphicalDisplaySlaveGroup;
        }
        //Namrata: 01/04/2019
        public MQTTSalveGroup getMQTTSlaveGroup()
        {
            return MqttSlaveGrp;
        }
        //Namrata: 25/05/2019
        public SMSSlaveGroup getSMSSlaveGroup()
        {
            //Namrata: 22/11/2019
            if(SMSGroup == null)
            {
                return sMS;
            }
            else
            {
                return SMSGroup;
            }
            //return SMSGroup;
        }
        public DNPSlaveGroup getDNPSlaveGroup()
        { //Namrata: 22/11/2019
            if (DNPSlave == null)
            {
                return Dnp;
            }
            else
            {
                return DNPSlave;
            }
            //return DNPSlave;
        }
    }
}
