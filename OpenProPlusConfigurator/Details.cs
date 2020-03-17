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
    * \brief     <b>Details</b> is a class to store info about device details
    * \details   This class stores info related to openpro+ device. It retrieves/stores 
    * various parameters like XML version, Hardware version, etc. It also exports the XML node 
    * related to this object.
    * 
    */
    public class Details
    {
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        private Mode mode = Mode.NONE;
        private string rnName = "Details";
        private string xmlVersion = null;
        private string deviceType = "";
        private string hwVersion = null;
        private string swVersion = null;
        private string deviceDescription = null;
        //Ajay: 11/01/2019 
        private string oppconfiguratorversion = "";
        private string openproplusconfigversion = "";
        private string commonconfigversion = "";

        ucDetails ucDetails = new ucDetails();
        //Ajay: 11/01/2019  "OppConfiguratorVer", "OppXsdVer", "CommonXsdVer" added
        //private string[] arrAttributes1 = { "XML Version", "Device Type", "Hw Version", "Sw Version", "Device Description" };
        //private string[] arrAttributes = { "XMLVersion", "DeviceType", "HwVersion", "SwVer", "DeviceDescription" };
        private string[] arrAttributes = { "XMLVersion", "DeviceType", "HwVersion", "SwVer", "DeviceDescription", "OppConfiguratorVer", "OppXsdVer", "CommonXsdVer" };
        public Details()
        {
            string strRoutineName = "Details";
            try
            {
                ucDetails.btnEditClick += new System.EventHandler(this.btnEdit_Click);
                ucDetails.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucDetails.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucDetails.lvDetailsDoubleClick += new System.EventHandler(this.lvDetails_DoubleClick);
                ucDetails.ucDetailsLoad += new System.EventHandler(this.ucDetails_Load);
                //Namrata: 17/06/2019
                FillVersions();
                addListHeaders();
                loadDefaults();
                refreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Namrata: 17/06/2019
        public void FillVersions()
        {
            string[] GetOPPConfigXSDVersion = Utils.GetVersionsFromOPPConfigXSD();
            if (GetOPPConfigXSDVersion != null && GetOPPConfigXSDVersion.Count() > 1)
            {
                if (GetOPPConfigXSDVersion[1] != null)
                {
                    openproplusconfigversion = Utils.GetVersionSubstring(GetOPPConfigXSDVersion[1]);
                }
            }
            string[] GetOPPCommonXSDVersion = Utils.GetVersionsFromCommonXSD();
            if (GetOPPCommonXSDVersion != null && GetOPPCommonXSDVersion.Count() > 2)
            {
                if (GetOPPCommonXSDVersion[2] != null)
                {
                    commonconfigversion = Utils.GetVersionSubstring(GetOPPCommonXSDVersion[2]);
                }
            }
        }
        private void ucDetails_Load(object sender, EventArgs e)
        {
            //ucDetails.lvDetails.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }
        private void lvDetails_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "lvDetails_DoubleClick";
            try
            {
                Console.WriteLine("*** ucDetails btnEdit_Click clicked in class!!!");
                // Ajay: 23/11/2018
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    if(ProtocolGateway.OppDetails_ReadOnly) { return; }
                    else {  }
                }

                mode = Mode.EDIT;
                ucDetails.grpDetails.Visible = true;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnEdit_Click";
            try
            {
                Console.WriteLine("*** ucDetails btnEdit_Click clicked in class!!!");
                mode = Mode.EDIT;
                ucDetails.grpDetails.Visible = true;
                loadValues();
            }
             catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnDone_Click";
            try
            {
                if (!Validate()) return;
                List<KeyValuePair<string, string>> dets = Utils.getKeyValueAttributes(ucDetails.grpDetails);
                //Namrata: 17/06/2019
                if (ucDetails.txtDeviceType.MaxLength > 253)
                {
                    MessageBox.Show("DeviceType MaxLength 253 Characters", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (mode == Mode.EDIT)
                {
                    updateAttributes(dets);
                }
                refreshList();
                ucDetails.grpDetails.Visible = false;
                mode = Mode.NONE;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnCancel_Click";
            try
            {
                Console.WriteLine("*** ucDetails btnCancel_Click clicked in class!!!");
                ucDetails.grpDetails.Visible = false;
                mode = Mode.NONE;
                Utils.resetValues(ucDetails.grpDetails);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Namrata: 21/11/2017
        private void loadDefaults()
        {
            string strRoutineName = "loadDefaults";
            try
            {
                XMLVersion = Globals.XML_VERSION;
                DeviceType = Globals.DEVICE_TYPE;
                HwVersion = Globals.HW_VERSION;
                SwVer = Globals.SW_VERSION;
                ucDetails.txtDescription.Text = "OpenPro+";
                DeviceDescription = ucDetails.txtDescription.Text;
                //Ajay: 11/01/2019
                OppConfiguratorVer = Utils.AssemblyVersion;//ucDetails.txtbxOppConfiguratorVer.Text;
                //Namrata: 17/06/2019
                OppXsdVer = openproplusconfigversion; //ucDetails.txtbxOppConfigXsdVer.Text;
                CommonXsdVer = commonconfigversion;  //ucDetails.txtbxCommonXsdVer.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadValues()
        {
            string strRoutineName = "loadValues";
            try
            {
                ucDetails.txtXMLVersion.Text = XMLVersion;
                ucDetails.txtDeviceType.Text = DeviceType;
                ucDetails.txtHwVersion.Text = HwVersion;
                ucDetails.txtSwVersion.Text = SwVer;
                ucDetails.txtDescription.Text = DeviceDescription;
                //Ajay: 11/01/2019
                ucDetails.txtbxOppConfiguratorVer.Text = OppConfiguratorVer;
                ucDetails.txtbxOppConfigXsdVer.Text = OppXsdVer;
                ucDetails.txtbxCommonXsdVer.Text = CommonXsdVer;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool Validate()
        {
            string strRoutineName = "Validate";
            //try
            //{
                bool status = true;
                //Check empty field's
                if (Utils.IsEmptyFields(ucDetails.grpDetails))
                {
                    MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                return status;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        public void refreshList()
        {
            string strRoutineName = "refreshList";
            try
            {
                int cnt = 0;
                ucDetails.lvDetails.Items.Clear();
                //Namrata: 18/1/2017
                for (int i = 0; i < arrAttributes.Count(); i++)
                {
                    string[] row = new string[2];
                    row[0] = arrAttributes[i];
                    row[1] = (string)this.GetType().GetProperty(arrAttributes[i]).GetValue(this);
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucDetails.lvDetails.Items.Add(lvItem);
                }
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
                ucDetails.lvDetails.Columns.Add("Name", 200, HorizontalAlignment.Left);
                ucDetails.lvDetails.Columns.Add("Value", 300, HorizontalAlignment.Left);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void updateAttributes(List<KeyValuePair<string, string>> detData)
        {
            string strRoutineName = "updateAttributes";
            try
            {
                if (detData != null && detData.Count > 0)
                {
                    foreach (KeyValuePair<string, string> detkp in detData)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", detkp.Key, detkp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(detkp.Key) != null) //Ajay: 03/07/2018
                            {
                                this.GetType().GetProperty(detkp.Key).SetValue(this, detkp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", detkp.Key, detkp.Value);
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

        public XmlNode exportXMLnode()
        {
            XmlDocument xmlDoc = new XmlDocument();
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);

            XmlNode rootNode = xmlDoc.CreateElement(rnName);
            xmlDoc.AppendChild(rootNode);

            foreach (string attr in arrAttributes)
            {
                XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                if(attr == "OppConfiguratorVer")
                {
                    attrName.Value = Utils.AssemblyVersion.ToString();
                }
                else if(attr == "OppXsdVer")
                {
                    string[] Vers = Utils.GetVersionsFromOPPConfigXSD();
                    if(Vers != null && Vers.Count() > 1)
                    {
                        if(Vers[1] != null)
                        {
                            attrName.Value = Utils.GetVersionSubstring(Vers[1]);
                        }
                    }
                }
                else if(attr == "CommonXsdVer")
                {
                    string[] Vers = Utils.GetVersionsFromCommonXSD();
                    if (Vers != null && Vers.Count() > 2)
                    {
                        if (Vers[2] != null)
                        {
                            attrName.Value = Utils.GetVersionSubstring(Vers[2]);
                        }
                    }
                }
                else
                {
                    attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                }
                rootNode.Attributes.Append(attrName);
            }
            return rootNode;
        }
        public void parseDetailsNode(XmlNode detNode)
        {
            string strRoutineName = "parseDetailsNode";
            try
            {
                rnName = detNode.Name;
                if (detNode.Attributes != null)
                {
                    foreach (XmlAttribute item in detNode.Attributes)
                    {
                        try
                        {
                            //if (item.Name == "DeviceDescription" || item.Name == "OppConfiguratorVer" || item.Name == "OppXsdVer" || item.Name == "CommonXsdVer")
                            //{
                            //    DeviceDescription = detNode.Attributes[4].Value;
                            //    ucDetails.lvDetails.Items[4].SubItems[1].Text = DeviceDescription;
                            //    //ucDetails.txtDescription.Text = DeviceDescription;

                            //    //Ajay: 11/01/2019
                            //    OppConfiguratorVer = detNode.Attributes[4].Value;
                            //    ucDetails.lvDetails.Items[4].SubItems[1].Text = OppConfiguratorVer;

                            //    OppXsdVer = detNode.Attributes[4].Value;
                            //    ucDetails.lvDetails.Items[4].SubItems[1].Text = OppXsdVer;

                            //    CommonXsdVer = detNode.Attributes[4].Value;
                            //    ucDetails.lvDetails.Items[4].SubItems[1].Text = CommonXsdVer;
                            //}
                            //else
                            {
                                if (this.GetType().GetProperty(item.Name) != null) //Ajay: 03/07/2018
                                {
                                    this.GetType().GetProperty(item.Name).SetValue(this, item.Value);
                                }
                            }
                            refreshList();
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field {0} doesn't exist. XML and class fields mismatch!!!", item.Name);
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

        public Control getView(List<string> kpArr)
        {
            FillVersions();
            return ucDetails;
        }

        public string XMLVersion
        {
            get { return xmlVersion; }
            set { xmlVersion = value; }
        }

        public string DeviceType
        {
            get { return deviceType; }
            set { deviceType = value; }
        }

        public string HwVersion
        {
            get { return hwVersion; }
            set { hwVersion = value; }
        }

        public string SwVer
        {
            get { return swVersion; }
            set { swVersion = value; }
        }

        public string DeviceDescription
        {
            get { return deviceDescription; }
            set { deviceDescription = value; }
        }

        public Size ClientSize { get; private set; }

        //Ajay: 11/01/2019
        public string OppConfiguratorVer
        {
            get { return oppconfiguratorversion; }
            set { oppconfiguratorversion = value; }
        }
        //Ajay: 11/01/2019
        public string OppXsdVer
        {
            get { return openproplusconfigversion; }
            set { openproplusconfigversion = value; }
        }
        //Ajay: 11/01/2019
        public string CommonXsdVer
        {
            get { return commonconfigversion; }
            set { commonconfigversion = value; }
        }
    }
}
