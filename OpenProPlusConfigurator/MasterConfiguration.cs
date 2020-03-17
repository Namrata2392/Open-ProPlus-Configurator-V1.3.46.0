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
    * \brief     <b>MasterConfiguration</b> is a class to store different types of masters.
    * \details   This class stores information about various master types like IEC103, MODBUS, etc.
    * It also exports the XML node related to this object.
    * 
    */
    public class MasterConfiguration
    {
        #region Declaration
        private string rnName = "MasterConfiguration";
        IEC103Group iec103Grp;
        IEC101Group iec101Grp;
        MODBUSGroup mbGrp;
        IEC61850ServerGroup server61850;
        VirtualGroup vGrp;
        ADRGroup adrgroup;
        PLUGroup plugroup;
        RCBConfiguration RCB;
        IEC104MasterGroup iec104Grp;
        SPORTGroup Sport;
        LoadProfileGroup LoadProfileGrp; //Ajay: 31/07/2018
        ucMasterConfiguration ucmc = new ucMasterConfiguration();
        #endregion Declaration
        public MasterConfiguration(Dictionary<string, TreeNode> treeDicts)
        {
            string strRoutineName = "MasterConfiguration";
            try
            {
                foreach (KeyValuePair<string, TreeNode> mckp in treeDicts)
                {
                    if (mckp.Key == "ADRGroup")
                    {
                        adrgroup = new ADRGroup(treeDicts["ADRGroup"]);
                    }
                    //Namrata:6/7/2017
                    if (mckp.Key == "IEC101Group")
                    {
                        iec101Grp = new IEC101Group(treeDicts["IEC101Group"]);
                    }
                    if (mckp.Key == "IEC103Group")
                    {
                        iec103Grp = new IEC103Group(treeDicts["IEC103Group"]);
                    }
                    else if (mckp.Key == "MODBUSGroup")
                    {
                        mbGrp = new MODBUSGroup(treeDicts["MODBUSGroup"]);
                    }
                    else if (mckp.Key == "IEC61850ClientGroup")//61850Group
                    {
                        server61850 = new IEC61850ServerGroup(treeDicts["IEC61850ClientGroup"]); //61850Group
                    }
                    //Namrata:17/05/2018
                    if (mckp.Key == "IEC104MasterGroup")
                    {
                        iec104Grp = new IEC104MasterGroup(treeDicts["IEC104MasterGroup"]);
                    }
                    else if (mckp.Key == "SPORTGroup")//SPORTGroup
                    {
                        Sport = new SPORTGroup(treeDicts["SPORTGroup"]); //SPORTGroup
                    }
                    //Namrata:25/10/2017
                    else if (mckp.Key == "PLUGroup")//61850Group
                    {
                        plugroup = new PLUGroup(treeDicts["PLUGroup"]); //61850Group
                    }
                    else if (mckp.Key == "VirtualGroup")
                    {
                        vGrp = new VirtualGroup(treeDicts["VirtualGroup"]);
                    }
                    //Ajay: 31/07/2018
                    else if (mckp.Key == "LoadProfileGroup")
                    {
                        LoadProfileGrp = new LoadProfileGroup(treeDicts["LoadProfileGroup"]);
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
        private void addListHeaders()
        {
            string strRoutineName = "txtTcpPortKeyPress";
            try
            {
                ucmc.lvMasterConfiguration.Columns.Add("No.", 60, HorizontalAlignment.Left);
                ucmc.lvMasterConfiguration.Columns.Add("Description", 150, HorizontalAlignment.Left);
                ucmc.lvMasterConfiguration.Columns.Add("Total", 150, HorizontalAlignment.Left);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void refreshList()
        {
            string strRoutineName = "txtTcpPortKeyPress";
            try
            {
                int iCount = 0; //Ajay: 23/11/2018

                int rowCnt = 0;
                ucmc.lvMasterConfiguration.Items.Clear();
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Full) //Ajay: 23/11/2018
                {
                    iCount = 0;
                    if (adrgroup != null) { iCount = adrgroup.getCount(); }
                    string[] row1 = { "1", "ADR", iCount.ToString() };
                    ListViewItem lvItem1 = new ListViewItem(row1);
                    if (rowCnt++ % 2 == 0) lvItem1.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucmc.lvMasterConfiguration.Items.Add(lvItem1);

                    iCount = 0;
                    if (iec101Grp != null) { iCount = iec101Grp.getCount(); }
                    string[] row2 = { "2", "IEC101", iCount.ToString() };
                    ListViewItem lvItem2 = new ListViewItem(row2);
                    if (rowCnt++ % 2 == 0) lvItem2.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucmc.lvMasterConfiguration.Items.Add(lvItem2);

                    iCount = 0;
                    if (iec103Grp != null) { iCount = iec103Grp.getCount(); }
                    string[] row3 = { "3", "IEC103", iCount.ToString() };
                    ListViewItem lvItem3 = new ListViewItem(row3);
                    if (rowCnt++ % 2 == 0) lvItem3.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucmc.lvMasterConfiguration.Items.Add(lvItem3);

                    iCount = 0;
                    if (mbGrp != null) { iCount = mbGrp.getCount(); }
                    string[] row4 = { "4", "MODBUS", iCount.ToString() };
                    ListViewItem lvItem4 = new ListViewItem(row4);
                    if (rowCnt++ % 2 == 0) lvItem4.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucmc.lvMasterConfiguration.Items.Add(lvItem4);

                    iCount = 0;
                    if (server61850 != null) { iCount = server61850.getCount(); }
                    string[] row5 = { "5", "IEC61850", iCount.ToString() };
                    ListViewItem lvItem5 = new ListViewItem(row5);
                    if (rowCnt++ % 2 == 0) lvItem5.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucmc.lvMasterConfiguration.Items.Add(lvItem5);

                    iCount = 0;
                    if (iec104Grp != null) { iCount = iec104Grp.getCount(); }
                    string[] row6 = { "6", "IEC104", iCount.ToString() };
                    ListViewItem lvItem6 = new ListViewItem(row6);
                    if (rowCnt++ % 2 == 0) lvItem6.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucmc.lvMasterConfiguration.Items.Add(lvItem6);

                    iCount = 0;
                    if (Sport != null) { iCount = Sport.getCount(); }
                    string[] row7 = { "7", "SPORT", iCount.ToString() };
                    ListViewItem lvItem7 = new ListViewItem(row7);
                    if (rowCnt++ % 2 == 0) lvItem7.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucmc.lvMasterConfiguration.Items.Add(lvItem7);

                    iCount = 0;
                    if (vGrp != null) { iCount = vGrp.getCount(); }
                    string[] row8 = { "8", "Virtual", iCount.ToString() };
                    ListViewItem lvItem8 = new ListViewItem(row8);
                    if (rowCnt++ % 2 == 0) lvItem8.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucmc.lvMasterConfiguration.Items.Add(lvItem8);

                    iCount = 0;
                    if (LoadProfileGrp != null) { iCount = LoadProfileGrp.getCount(); }
                    //Ajay: 31/07/2018
                    string[] row9 = { "9", "LoadProfile", iCount.ToString() };
                    ListViewItem lvItem9 = new ListViewItem(row9);
                    if (rowCnt++ % 2 == 0) lvItem9.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucmc.lvMasterConfiguration.Items.Add(lvItem9);
                }
                //Ajay: 23/11/2018
                else if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    if (ProtocolGateway.OppADRGroup_Visible)
                    {
                        iCount = 0;
                        if (adrgroup != null) { iCount = adrgroup.getCount(); }
                        string[] row1 = { "1", "ADR", iCount.ToString() };
                        ListViewItem lvItem1 = new ListViewItem(row1);
                        if (rowCnt++ % 2 == 0) lvItem1.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucmc.lvMasterConfiguration.Items.Add(lvItem1);
                    }
                    else { }
                    if (ProtocolGateway.OppIEC101Group_Visible)
                    {
                        iCount = 0;
                        if (iec101Grp != null) { iCount = iec101Grp.getCount(); }
                        string[] row2 = { "2", "IEC101", iCount.ToString() };
                        ListViewItem lvItem2 = new ListViewItem(row2);
                        if (rowCnt++ % 2 == 0) lvItem2.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucmc.lvMasterConfiguration.Items.Add(lvItem2);
                    }
                    else { }
                    if (ProtocolGateway.OppIEC103Group_Visible)
                    {
                        iCount = 0;
                        if (iec103Grp != null) { iCount = iec103Grp.getCount(); }
                        string[] row3 = { "3", "IEC103", iCount.ToString() };
                        ListViewItem lvItem3 = new ListViewItem(row3);
                        if (rowCnt++ % 2 == 0) lvItem3.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucmc.lvMasterConfiguration.Items.Add(lvItem3);
                    }
                    else { }
                    if (ProtocolGateway.OppMODBUSGroup_Visible)
                    {
                        iCount = 0;
                        if (mbGrp != null) { iCount = mbGrp.getCount(); }
                        string[] row4 = { "4", "MODBUS", iCount.ToString() };
                        ListViewItem lvItem4 = new ListViewItem(row4);
                        if (rowCnt++ % 2 == 0) lvItem4.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucmc.lvMasterConfiguration.Items.Add(lvItem4);
                    }
                    else { }
                    if (ProtocolGateway.OppIEC61850Group_Visible)
                    {
                        iCount = 0;
                        if (server61850 != null) { iCount = server61850.getCount(); }
                        string[] row5 = { "5", "IEC61850", iCount.ToString() };
                        ListViewItem lvItem5 = new ListViewItem(row5);
                        if (rowCnt++ % 2 == 0) lvItem5.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucmc.lvMasterConfiguration.Items.Add(lvItem5);
                    }
                    else { }
                    if (ProtocolGateway.OppIEC104Group_Visible)
                    {
                        iCount = 0;
                        if (iec104Grp != null) { iCount = iec104Grp.getCount(); }
                        string[] row6 = { "6", "IEC104", iCount.ToString() };
                        ListViewItem lvItem6 = new ListViewItem(row6);
                        if (rowCnt++ % 2 == 0) lvItem6.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucmc.lvMasterConfiguration.Items.Add(lvItem6);
                    }
                    else { }
                    if (ProtocolGateway.OppSPORTGroup_Visible)
                    {
                        iCount = 0;
                        if (Sport != null) { iCount = Sport.getCount(); }
                        string[] row7 = { "7", "SPORT", iCount.ToString() };
                        ListViewItem lvItem7 = new ListViewItem(row7);
                        if (rowCnt++ % 2 == 0) lvItem7.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucmc.lvMasterConfiguration.Items.Add(lvItem7);
                    }
                    else { }
                    if (ProtocolGateway.OppVirtualGroup_Visible)
                    {
                        iCount = 0;
                        if (vGrp != null) { iCount = vGrp.getCount(); }
                        string[] row8 = { "8", "Virtual", iCount.ToString() };
                        ListViewItem lvItem8 = new ListViewItem(row8);
                        if (rowCnt++ % 2 == 0) lvItem8.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucmc.lvMasterConfiguration.Items.Add(lvItem8);
                    }
                    else { }
                    if (ProtocolGateway.OppLoadProfileGroup_Visible)
                    {
                        iCount = 0;
                        if (LoadProfileGrp != null) { iCount = LoadProfileGrp.getCount(); }
                        //Ajay: 31/07/2018
                        string[] row9 = { "9", "LoadProfile", iCount.ToString() };
                        ListViewItem lvItem9 = new ListViewItem(row9);
                        if (rowCnt++ % 2 == 0) lvItem9.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucmc.lvMasterConfiguration.Items.Add(lvItem9);
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
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("MasterConfiguration_")) { refreshList(); return ucmc; }

            kpArr.RemoveAt(0);

            //Namrata: 10/05/2017
            if (kpArr.ElementAt(0).Contains("ADRGroup_"))
            {
                if (adrgroup == null) return null;
                return adrgroup.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("IEC101Group_"))
            {
                if (iec101Grp == null) return null;
                return iec101Grp.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("IEC103Group_"))
            {
                if (iec103Grp == null) return null;
                return iec103Grp.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("MODBUSGroup_"))
            {
                if (mbGrp == null) return null;
                return mbGrp.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("IEC61850ClientGroup_"))// 61850Group_
            {
                if (server61850 == null) return null;
                return server61850.getView(kpArr);
            }
            //Namrata:17/05/2018
            else if (kpArr.ElementAt(0).Contains("IEC104MasterGroup_"))
            {
                if (iec104Grp == null) return null;
                return iec104Grp.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("SPORTGroup_"))// 61850Group_
            {
                if (Sport == null) return null;
                return Sport.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("PLUGroup_"))// 61850Group_
            {
                if (plugroup == null) return null;
                return plugroup.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("VirtualGroup_"))
            {
                if (vGrp == null) return null;
                return vGrp.getView(kpArr);
            }
            //Ajay: 31/07/2018
            else if (kpArr.ElementAt(0).Contains("LoadProfileGroup_"))
            {
                if (LoadProfileGrp == null) return null;
                return LoadProfileGrp.getView(kpArr);
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
            //Namrata:31/5/2017
            if (adrgroup != null)
            {
                XmlNode importADRMasterNode = rootNode.OwnerDocument.ImportNode(adrgroup.exportXMLnode(), true);
                rootNode.AppendChild(importADRMasterNode);
            }
            if (iec101Grp != null)
            {
                XmlNode import101Node = rootNode.OwnerDocument.ImportNode(iec101Grp.exportXMLnode(), true);
                rootNode.AppendChild(import101Node);
            }
            if (iec103Grp != null)
            {
                XmlNode importIECGNode = rootNode.OwnerDocument.ImportNode(iec103Grp.exportXMLnode(), true);
                rootNode.AppendChild(importIECGNode);
            }
            
            if (mbGrp != null)
            {
                XmlNode importMBGNode = rootNode.OwnerDocument.ImportNode(mbGrp.exportXMLnode(), true);
                rootNode.AppendChild(importMBGNode);
            }

            if (server61850 != null)
            {
                XmlNode importMBGNode = rootNode.OwnerDocument.ImportNode(server61850.exportXMLnode(), true);
                rootNode.AppendChild(importMBGNode);
            }
            //Namrata:17/05/2018
            if (iec104Grp != null)
            {
                XmlNode importIEC104GNode = rootNode.OwnerDocument.ImportNode(iec104Grp.exportXMLnode(), true);
                rootNode.AppendChild(importIEC104GNode);
            }
            if (Sport != null)
            {
                XmlNode importMBGNode = rootNode.OwnerDocument.ImportNode(Sport.exportXMLnode(), true);
                rootNode.AppendChild(importMBGNode);
            }
            if (vGrp != null)
            {
                XmlNode importVGNode = rootNode.OwnerDocument.ImportNode(vGrp.exportXMLnode(), true);
                rootNode.AppendChild(importVGNode);
            }
            //Namrata: 25/10/2017
            if (plugroup != null)
            {
                XmlNode importMBGNode = rootNode.OwnerDocument.ImportNode(plugroup.exportXMLnode(), true);
                rootNode.AppendChild(importMBGNode);
            }
            //Ajay: 31/07/2018
            if (LoadProfileGrp != null)
            {
                XmlNode importLoadProfileGrpNode = rootNode.OwnerDocument.ImportNode(LoadProfileGrp.exportXMLnode(), true);
                rootNode.AppendChild(importLoadProfileGrpNode);
            }
            return rootNode;
        }
        public string exportINI(string slaveNum, string slaveID)
        {
            //string[] arrINIelements = { "AI", "EN","AO", "DI", "DO", "IED", "DeadBandAI", "DeadBandEN" };//Don't change order...
            //Namrata:28/01/2019
            string[] arrINIelements = { "AI", "EN","AO", "DI", "DO", "IED", "DeadBandAI", "DeadBandEN" };//Don't change order...
            
            //Ajay //string[] arrINIelements = { "AI", "AO", "DI", "DO", "EN", "IED", "DeadBandAI", "DeadBandEN" };//Don't change order... //Ajay: 10/01/2019
            string finalINIdata = "";
            string storeINIdata = "";
            int ctr = 1;
            for (int i = 0; i < arrINIelements.Length; i++)
            {
                string iniData = "";
                if (arrINIelements[i] != "EN" && arrINIelements[i] != "DeadBandEN") ctr = 1;//Since EN treated as AI...
                //Namrata:31/5/2017
                if (adrgroup != null)
                {
                    string tmp = adrgroup.exportINI(slaveNum, slaveID, arrINIelements[i], ref ctr);
                    iniData += tmp;
                }
                if (iec101Grp != null)
                {
                    string tmp = iec101Grp.exportINI(slaveNum, slaveID, arrINIelements[i], ref ctr);
                    iniData += tmp;
                }
                if (iec103Grp != null)
                {
                    string tmp = iec103Grp.exportINI(slaveNum, slaveID, arrINIelements[i], ref ctr);
                    iniData += tmp;
                }
                if (mbGrp != null)
                {
                    string tmp = mbGrp.exportINI(slaveNum, slaveID, arrINIelements[i], ref ctr);
                    iniData += tmp;
                }
                if (server61850 != null)
                {
                    string tmp = server61850.exportINI(slaveNum, slaveID, arrINIelements[i], ref ctr);
                    iniData += tmp;
                }
                //Namrata:17/05/2018
                if (iec104Grp != null)
                {
                    string tmp = iec104Grp.exportINI(slaveNum, slaveID, arrINIelements[i], ref ctr);
                    iniData += tmp;
                }
                if (Sport != null)
                {
                    string tmp = Sport.exportINI(slaveNum, slaveID, arrINIelements[i], ref ctr);
                    iniData += tmp;
                }
                if (vGrp != null)
                {
                    string tmp = vGrp.exportINI(slaveNum, slaveID, arrINIelements[i], ref ctr);
                    iniData += tmp;
                }
                //Ajay: 31/07/2018
                if (LoadProfileGrp != null)
                {
                    string tmp = LoadProfileGrp.exportINI(slaveNum, slaveID, arrINIelements[i], ref ctr);
                    iniData += tmp;
                 }
                    if (arrINIelements[i] == "AI" || arrINIelements[i] == "DeadBandAI")
                {
                    storeINIdata += iniData;
                }
                else
                {
                    string postFix = "";
                    if (arrINIelements[i] == "EN") postFix = "AI";
                    else postFix = arrINIelements[i];

                    if (arrINIelements[i] != "DeadBandEN") finalINIdata += "Num" + postFix + "=" + (ctr - 1).ToString() + Environment.NewLine;
                    if (storeINIdata.Length > 0) finalINIdata += storeINIdata;
                    storeINIdata = "";
                    finalINIdata += iniData;
                }
                if (arrINIelements[i] == "IED")
                {
                    //Hardcoded entry as asked by Naina...
                    finalINIdata += "IEDClockSyncIntervalSec=10" + Environment.NewLine;
                }
            }

            return finalINIdata;
        }
        public void regenerateAOSequence()
        {
            string strRoutineName = "regenerateAOSequence";
            try
            {
                //Reset AI no.
                Globals.resetUniqueNos(ResetUniqueNos.AO);
                Globals.AONo++;
               
                iec101Grp.regenerateAOSequence(); //Handle IEC101 masters...
                iec103Grp.regenerateAOSequence(); //Handle IEC103 masters...
              
                mbGrp.regenerateAOSequence(); //Handle MODBUS masters...
                                              //Namrata:17/05/2018
                iec104Grp.regenerateAOSequence(); //Handle IEC104 masters...
                Sport.regenerateAOSequence();
                vGrp.regenerateAOSequence(); //Handle Virtual masters...
                LoadProfileGrp.regenerateAOSequence(); //Ajay: 31/07/2018
                //Namrata:31/5/2017
                //Handle ADR masters...
                //adrgroup.regenerateAOSequence();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void regenerateAISequence()
        {
            string strRoutineName = "regenerateAISequence";
            try
            {
                //Reset AI no.
                Globals.resetUniqueNos(ResetUniqueNos.AI);
                Globals.AINo++;
               
                iec101Grp.regenerateAISequence(); //Handle IEC101 masters...
                iec103Grp.regenerateAISequence(); //Handle IEC103 masters...
               
                mbGrp.regenerateAISequence(); //Handle MODBUS masters...
                server61850.regenerateAISequence();  //Handle Virtual masters...
                                                     //Namrata:17/05/2018
                iec104Grp.regenerateAISequence(); //Handle IEC104 masters...
                Sport.regenerateAISequence();
                vGrp.regenerateAISequence();
                LoadProfileGrp.regenerateAISequence(); //Ajay: 31/07/2018
                //Namrata:31/5/2017
                //Handle ADR masters...
                adrgroup.regenerateAISequence();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void regenerateDISequence()
        {
            string strRoutineName = "regenerateDISequence";
            try
            {
                //Reset DI no.
                Globals.resetUniqueNos(ResetUniqueNos.DI);
                Globals.DINo++;//Start from 1...
                //Namrata:31/5/2017
                adrgroup.regenerateDISequence(); //Handle ADR masters...
                iec101Grp.regenerateDISequence(); //Handle IEC101 masters...
                iec103Grp.regenerateDISequence();//Handle IEC103 masters...
              
                mbGrp.regenerateDISequence(); //Handle MODBUS masters...
                server61850.regenerateDISequence();//Handle IEC61850 masters...
                                                   //Namrata:17/05/2018
                iec104Grp.regenerateDISequence(); //Handle IEC104 masters...
                Sport.regenerateDISequence();
                vGrp.regenerateDISequence(); //Handle Virtual masters...
                LoadProfileGrp.regenerateDISequence(); //Ajay: 31/07/2018
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void regenerateDOSequence()
        {
            string strRoutineName = "regenerateDOSequence";
            try
            {
                //Reset DO no.
                Globals.resetUniqueNos(ResetUniqueNos.DO);
                Globals.DONo++;//Start from 1...
                //Namrata:31/5/2017
                adrgroup.regenerateDOSequence();//Handle ADR masters...
                iec101Grp.regenerateDOSequence(); //Handle IEC101 masters...
                iec103Grp.regenerateDOSequence(); //Handle IEC103 masters...
                mbGrp.regenerateDOSequence(); //Handle MODBUS masters...
                server61850.regenerateDOSequence();
                //Namrata:17/05/2018
                iec104Grp.regenerateDOSequence(); //Handle IEC104 masters...
                Sport.regenerateDOSequence();
                vGrp.regenerateDOSequence(); //Handle Virtual masters...
                LoadProfileGrp.regenerateDOSequence(); //Ajay: 31/07/2018

            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void regenerateENSequence()
        {
            string strRoutineName = "regenerateENSequence";
            try
            {
                Globals.resetUniqueNos(ResetUniqueNos.EN);
                Globals.ENNo++;
                adrgroup.regenerateENSequence();            
                iec101Grp.regenerateENSequence();
                iec103Grp.regenerateENSequence();   
                mbGrp.regenerateENSequence();     
                server61850.regenerateENSequence(); 
                iec104Grp.regenerateENSequence();    
                Sport.regenerateENSequence();
                vGrp.regenerateENSequence();      
                LoadProfileGrp.regenerateENSequence(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void parseMCNode(XmlNode mNode, Dictionary<string, TreeNode> treeDicts)
        {
            
            string strRoutineName = "parseMCNode";
            try
            {
                //First set root node name...
                rnName = mNode.Name;
                //tn.Nodes.Clear();

                foreach (XmlNode node in mNode)
                {
                    //Namrata:31/5/2017
                    if (node.Name == "ADRGroup" && treeDicts.Keys.Contains("ADRGroup"))
                    {
                        //Task thDetails = new Task(() => adrgroup.parseIECGNode(node, treeDicts["ADRGroup"]));
                        //thDetails.Start();
                        adrgroup.parseIECGNode(node, treeDicts["ADRGroup"]);
                    }
                    else if (node.Name == "IEC101Group" && treeDicts.Keys.Contains("IEC101Group"))
                    {
                        //Task thDetails = new Task(() => iec101Grp.parseIECGNode(node, treeDicts["IEC101Group"]));
                        //thDetails.Start();
                        iec101Grp.parseIECGNode(node, treeDicts["IEC101Group"]);
                    }
                    else if (node.Name == "IEC103Group" && treeDicts.Keys.Contains("IEC103Group"))
                    {
                        //Task thDetails = new Task(() => iec103Grp.parseIECGNode(node, treeDicts["IEC103Group"]));
                        //thDetails.Start();
                        iec103Grp.parseIECGNode(node, treeDicts["IEC103Group"]);
                    }
                    else if (node.Name == "MODBUSGroup" && treeDicts.Keys.Contains("MODBUSGroup"))
                    {
                        //Task thDetails = new Task(() => mbGrp.parseMBGNode(node, treeDicts["MODBUSGroup"]));
                        //thDetails.Start();
                        mbGrp.parseMBGNode(node, treeDicts["MODBUSGroup"]);
                    }
                    else if (node.Name == "IEC61850ClientGroup" && treeDicts.Keys.Contains("IEC61850ClientGroup"))
                    {
                        //Task thDetails = new Task(() => server61850.parseMBGNode(node, treeDicts["IEC61850ClientGroup"]));
                        //thDetails.Start();
                        server61850.parseMBGNode(node, treeDicts["IEC61850ClientGroup"]); //61850Group
                    }
                    //Namrata:17/05/2018
                    //else if (node.Name == "IEC104MasterGroup") //Ajay: 23/11/2018 Commented
                    else if (node.Name == "IEC104MasterGroup" && treeDicts.Keys.Contains("IEC104MasterGroup"))
                    {
                        //Task thDetails = new Task(() => iec104Grp.parseIECGNode(node, treeDicts["IEC104MasterGroup"]));
                        //thDetails.Start();
                        iec104Grp.parseIECGNode(node, treeDicts["IEC104MasterGroup"]);
                    }
                    //else if (node.Name == "SPORTGroup") //Ajay: 23/11/2018 Commented
                    else if (node.Name == "SPORTGroup" && treeDicts.Keys.Contains("SPORTGroup"))
                    {
                        //Task thDetails = new Task(() => Sport.parseIECGNode(node, treeDicts["SPORTGroup"]));
                        //thDetails.Start();
                        Sport.parseIECGNode(node, treeDicts["SPORTGroup"]); //SPORTGroup
                    }
                    //else if (node.Name == "VirtualGroup") //Ajay: 23/11/2018 Commented
                    else if (node.Name == "VirtualGroup" && treeDicts.Keys.Contains("VirtualGroup"))
                    {
                        //Task thDetails = new Task(() => vGrp.parseVGNode(node));
                        //thDetails.Start();
                        vGrp.parseVGNode(node/*, treeDicts["VirtualGroup"] */);//TreeNode not needed as already sent...
                    }
                    //Ajay: 31/07/2018
                    //else if (node.Name == "LoadProfileGroup") //Ajay: 23/11/2018 Commented
                    else if (node.Name == "LoadProfileGroup" && treeDicts.Keys.Contains("LoadProfileGroup"))
                    {
                        //Task thDetails = new Task(() => LoadProfileGrp.parseMBGNode(node, treeDicts["LoadProfileGroup"]));
                        //thDetails.Start();
                        LoadProfileGrp.parseMBGNode(node, treeDicts["LoadProfileGroup"]);//TreeNode not needed as already sent...
                    }
                    else
                    {
                    }
                }
                refreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public IEC103Group getIEC103Group()
        {
            return iec103Grp;
        }
        public IEC101Group getIEC101Group()
        {
            return iec101Grp;
        }
        public SPORTGroup getSPORTGroup()
        {
            return Sport;
        }
        //Namrata:17/05/2018
        public IEC104MasterGroup getIEC104Group()
        {
            return iec104Grp;
        }
        //Namarta:31/5/2017
        public ADRGroup getADRMasterGroup()
        {
            return adrgroup;
        }
        public MODBUSGroup getMODBUSGroup()
        {
            return mbGrp;
        }
        public VirtualGroup getVirtualGroup()
        {
            return vGrp;
        }
        //Ajay: 31/07/2018
        public LoadProfileGroup getLoadProfileGroup()
        {
            return LoadProfileGrp;
        }
        public IEC61850ServerGroup getIEC61850ClientGroup()
        {
            return server61850;
        }
    }
}
