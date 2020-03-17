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
    * \brief     <b>LoadProfileMaster</b> is a class to store all the IED's & LoadProfile master parameters.
    * \details   This class stores info for all IED's & LoadProfile master parameters. It allows
    * user to add multiple IED's. It also exports the XML node related to this object.
    * 
    */
    public class LoadProfileMaster
    {
        #region Declarations
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        enum masterType
        {
            LoadProfile
        };

        enum ProtocolType
        {
            RTU,
            ASCII,
            TCP
        };

        private bool isNodeComment = false;
        private string comment = "";
        private masterType mType = masterType.LoadProfile;
        private int masterNum = -1;
        private string device = "";
        private double dftTapRatio = 0;
        private double txOffsetCurrent = 0;
        private string desc = "";
        private int SelectedIndex;
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        private int debug = -1;
        private bool run = true;

        List<IED> iedList = new List<IED>();
        ucMasterLoadProfile uclp = new ucMasterLoadProfile();
        private TreeNode LoadProfileTreeNode;
        private OpenFileDialog ofdXMLFile = new OpenFileDialog();

        private string[] arrAttributes = { "MasterNum", "TimeInterval", "DEBUG", "Run" }; 
        #endregion Declarations 
        public static List<string> getProtocolTypes()
        {
            List<string> lpt = new List<string>();
            foreach (ProtocolType ptv in Enum.GetValues(typeof(ProtocolType)))
            {
                lpt.Add(ptv.ToString());
            }
            return lpt;
        }

        public LoadProfileMaster(string lpName, List<KeyValuePair<string, string>> lpData, TreeNode tn)
        {
            uclp.btnAddClick += new System.EventHandler(this.btnAdd_Click);
            uclp.lvIEDListItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvIEDList_ItemCheck);
            uclp.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
            uclp.btnExportIEDClick += new System.EventHandler(this.btnExportIED_Click);
            uclp.btnImportIEDClick += new System.EventHandler(this.btnImportIED_Click);
            uclp.btnDoneClick += new System.EventHandler(this.btnDone_Click);
            uclp.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
            uclp.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
            uclp.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
            uclp.btnNextClick += new System.EventHandler(this.btnNext_Click);
            uclp.btnLastClick += new System.EventHandler(this.btnLast_Click);
            uclp.lvIEDListDoubleClick += new System.EventHandler(this.lvIEDList_DoubleClick);
            addListHeaders();
            this.fillOptions();

            //First set the root element value...
            try
            {
                mType = (masterType)Enum.Parse(typeof(masterType), lpName);
            }
            catch (System.ArgumentException)
            {
                Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", lpName);
            }

            //Parse n store values...
            if (lpData != null && lpData.Count > 0)
            {
                foreach (KeyValuePair<string, string> mbkp in lpData)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", mbkp.Key, mbkp.Value);
                    try
                    {
                        if (this.GetType().GetProperty(mbkp.Key) != null)
                        {
                            this.GetType().GetProperty(mbkp.Key).SetValue(this, mbkp.Value);
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", mbkp.Key, mbkp.Value);
                    }
                }
                Utils.Write(VerboseLevel.DEBUG, "\n");
            }

            if (tn != null) tn.Nodes.Clear();
            LoadProfileTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...
            refreshList();
            uclp.lblIED.Text = "IED List (Master No: " + this.masterNum + ")";
        }

        public LoadProfileMaster(XmlNode mNode, TreeNode tn)
        {
            uclp.btnAddClick += new System.EventHandler(this.btnAdd_Click);
            uclp.lvIEDListItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvIEDList_ItemCheck);
            uclp.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
            uclp.btnExportIEDClick += new System.EventHandler(this.btnExportIED_Click);
            uclp.btnImportIEDClick += new System.EventHandler(this.btnImportIED_Click);
            uclp.btnDoneClick += new System.EventHandler(this.btnDone_Click);
            uclp.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
            uclp.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
            uclp.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
            uclp.btnNextClick += new System.EventHandler(this.btnNext_Click);
            uclp.btnLastClick += new System.EventHandler(this.btnLast_Click);
            uclp.lvIEDListDoubleClick += new System.EventHandler(this.lvIEDList_DoubleClick);
            addListHeaders();
            this.fillOptions();

            //Parse n store values...
            Utils.WriteLine(VerboseLevel.DEBUG, "mNode name: '{0}'", mNode.Name);
            if (mNode.Attributes != null)
            {
                //First set the root element value...
                try
                {
                    mType = (masterType)Enum.Parse(typeof(masterType), mNode.Name);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", mNode.Name);
                }

                foreach (XmlAttribute item in mNode.Attributes)
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
                        Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", item.Name, item.Value);
                    }
                }
                Utils.Write(VerboseLevel.DEBUG, "\n");
            }
            else if (mNode.NodeType == XmlNodeType.Comment)
            {
                isNodeComment = true;
                comment = mNode.Value;
            }

            if (tn != null) tn.Nodes.Clear();
            LoadProfileTreeNode = tn; //Save local copy so we can use it to manually add nodes in above constructor...

            //Now since we have parsed, we can name the treenode...
            //if (tn != null) tn.Text = "Load Profile " + this.Description;

            foreach (XmlNode node in mNode)
            {
                if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                TreeNode tmp = tn.Nodes.Add("IED_" + Utils.GenerateShortUniqueKey(), "IED", "IED", "IED");
                iedList.Add(new IED(node, tmp, MasterTypes.LoadProfile, Int32.Parse(MasterNum), false));
            }
            refreshList();
            uclp.lblIED.Text = "IED List (Master No: " + this.MasterNum + ")";
        }

        public void updateAttributes(List<KeyValuePair<string, string>> mbData)
        {
            if (mbData != null && mbData.Count > 0)
            {
                foreach (KeyValuePair<string, string> mbkp in mbData)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", mbkp.Key, mbkp.Value);
                    try
                    {
                        if (this.GetType().GetProperty(mbkp.Key) != null) //Ajay: 03/07/2018
                        {
                            this.GetType().GetProperty(mbkp.Key).SetValue(this, mbkp.Value);
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", mbkp.Key, mbkp.Value);
                    }
                }
                Utils.Write(VerboseLevel.DEBUG, "\n");
            }

            uclp.lblIED.Text = "IED List (Master No: " + this.MasterNum + ")";
            if (LoadProfileTreeNode != null) LoadProfileTreeNode.Text = "Load Profile " + this.Description;
        }
        private void lvIEDList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    uclp.lvIEDList.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
                    {
                        item.Checked = false;
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (iedList.Count >= Globals.MaxLoadProfileIED)
            {
                MessageBox.Show("Maximum " + Globals.MaxLoadProfileIED + " IED's in Load Profile Masters are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            mode = Mode.ADD;
            editIndex = -1;
            Utils.resetValues(uclp.grpIED);
            Utils.showNavigation(uclp.grpIED, false);
            loadDefaults();
            uclp.grpIED.Visible = true;
            uclp.txtUnitID.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (uclp.lvIEDList.Items.Count == 0)
                {
                    MessageBox.Show("Please add atleast one IED ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (uclp.lvIEDList.CheckedItems.Count == 1)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete IED " + uclp.lvIEDList.CheckedItems[0].Text + " ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        int iIndex = uclp.lvIEDList.CheckedItems[0].Index;
                        if (result == DialogResult.Yes)
                        {
                            LoadProfileTreeNode.Nodes.Remove(iedList.ElementAt(iIndex).getTreeNode());
                            Utils.RemoveDI4IED(MasterTypes.LoadProfile, Int32.Parse(MasterNum), Int32.Parse(iedList[iIndex].UnitID));
                            iedList.RemoveAt(iIndex);
                            uclp.lvIEDList.Items[iIndex].Remove();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select atleast one IED ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    refreshList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnExportIED_Click(object sender, EventArgs e)
        {
            if (uclp.lvIEDList.CheckedItems.Count != 1)
            {
                MessageBox.Show("Select single IED for export!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListViewItem lvi = uclp.lvIEDList.CheckedItems[0];
            IED expObj = null;
            //Now get the IED object...
            foreach (IED iedObj in iedList)
            {
                if (iedObj.UnitID == lvi.Text)
                {
                    expObj = iedObj;
                    break;
                }
            }
            Utils.SaveIEDFile(expObj.exportIED()); //exportIED
        }
       
        private void btnImportIED_Click(object sender, EventArgs e)
        {
            if (ofdXMLFile.ShowDialog() == DialogResult.OK)
            {
                Utils.WriteLine(VerboseLevel.DEBUG, "*** Opening file: {0}", ofdXMLFile.FileName);
                if (!Utils.IsXMLWellFormed(ofdXMLFile.FileName))
                {
                    MessageBox.Show("Selected file is not a valid XML!!!.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ofdXMLFile.FileName);

                XmlNodeList nodeList = xmlDoc.SelectNodes("IEDexport");
                Utils.WriteLine(VerboseLevel.BOMBARD, "nodeList count: {0}", nodeList.Count);

                if (nodeList.Count <= 0)
                {
                    MessageBox.Show("Selected file is not an IED exported node.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                XmlNode rootNode = nodeList.Item(0);
                Utils.WriteLine(VerboseLevel.DEBUG, "*** Exported IED Node name: {0}", rootNode.Name);

                if (rootNode.Attributes != null)
                {
                    foreach (XmlAttribute item in rootNode.Attributes)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", item.Name, item.Value);
                        if (item.Name == "MasterType")
                        {
                            if (item.Value != mType.ToString())
                            {
                                MessageBox.Show("Invalid Master Type (" + item.Value + ") to import!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                }
                foreach (XmlNode node in rootNode)
                {
                    Utils.WriteLine(VerboseLevel.BOMBARD, "node value: '{0}' child count {1}", node.Name, node.ChildNodes.Count);
                    if (node.Name == "IED")
                    {
                        //Utils.WriteLine("***** node type: {0}", node.NodeType);
                        if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                        TreeNode tmp = LoadProfileTreeNode.Nodes.Add("IED_" + Utils.GenerateShortUniqueKey(), "IED", "IED", "IED");
                        iedList.Add(new IED(node, tmp, MasterTypes.LoadProfile, Int32.Parse(MasterNum), true));
                        Utils.CreateDI4IED(MasterTypes.LoadProfile, Int32.Parse(MasterNum), Int32.Parse(iedList[iedList.Count - 1].UnitID), iedList[iedList.Count - 1].Device);
                        tmp.Expand();
                    }
                }
                refreshList();
                MessageBox.Show("IED imported successfully!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }//File selected thru open dialog...
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (!Validate()) return;
            List<KeyValuePair<string, string>> iedData = Utils.getKeyValueAttributes(uclp.grpIED);
            if (mode == Mode.ADD)
            {
                TreeNode tmp = LoadProfileTreeNode.Nodes.Add("IED_" + Utils.GenerateShortUniqueKey(), "IED", "IED", "IED");
                iedList.Add(new IED("IED", iedData, tmp, MasterTypes.LoadProfile, Int32.Parse(MasterNum)));
                //Namrata:01/02/2019
                //IEDHealth entry not allowed in Virtual DI.
                //Requirement from Naina D
                //Utils.CreateDI4IED(MasterTypes.LoadProfile, Int32.Parse(MasterNum), Int32.Parse(iedList[iedList.Count - 1].UnitID), iedList[iedList.Count - 1].Device);
            }
            else if (mode == Mode.EDIT)
            {
                iedList[editIndex].updateAttributes(iedData);
                //Namrata:14/06/2017
                if (uclp.lvIEDList.SelectedIndices.Count > 0)
                {
                    SelectedIndex = Convert.ToInt16(uclp.lvIEDList.FocusedItem.Index);
                    Utils.IEDUnitID = Convert.ToInt16(uclp.lvIEDList.Items[SelectedIndex].Text);
                }
                //Namrata:01/02/2019
                //Utils.UpdateDI4IED(MasterTypes.LoadProfile, Int32.Parse(MasterNum), Utils.IEDUnitID, Int32.Parse(iedList[SelectedIndex].UnitID), iedList[iedList.Count - 1].Device);
            }
            refreshList();
            //Namrata: 09/08/2017
            if (sender != null && e != null)
            {
                uclp.grpIED.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            uclp.grpIED.Visible = false;
            mode = Mode.NONE;
            editIndex = -1;
            Utils.resetValues(uclp.grpIED);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** uclp btnFirst_Click clicked in class!!!");
            if (uclp.lvIEDList.Items.Count <= 0) return;
            if (iedList.ElementAt(0).IsNodeComment) return;
            editIndex = 0;
            loadValues();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            btnDone_Click(null, null);
            Console.WriteLine("*** uclp btnPrev_Click clicked in class!!!");
            if (editIndex - 1 < 0) return;
            if (iedList.ElementAt(editIndex - 1).IsNodeComment) return;
            editIndex--;
            loadValues();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            btnDone_Click(null, null);
            Console.WriteLine("*** uclp btnNext_Click clicked in class!!!");
            if (editIndex + 1 >= uclp.lvIEDList.Items.Count) return;
            if (iedList.ElementAt(editIndex + 1).IsNodeComment) return;
            editIndex++;
            loadValues();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** uclp btnLast_Click clicked in class!!!");
            if (uclp.lvIEDList.Items.Count <= 0) return;
            if (iedList.ElementAt(iedList.Count - 1).IsNodeComment) return;
            editIndex = iedList.Count - 1;
            loadValues();
        }

        private void lvIEDList_DoubleClick(object sender, EventArgs e)
        {
            // Ajay: 23/11/2018
            if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
            {
                if (ProtocolGateway.OppLoadProfileGroup_ReadOnly) { return; }
                else { }
            }

            if (uclp.lvIEDList.SelectedItems.Count <= 0) return;

            ListViewItem lvi = uclp.lvIEDList.SelectedItems[0];
            Utils.UncheckOthers(uclp.lvIEDList, lvi.Index);
            if (iedList.ElementAt(lvi.Index).IsNodeComment)
            {
                MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            uclp.grpIED.Visible = true;
            mode = Mode.EDIT;
            editIndex = lvi.Index;
            Utils.showNavigation(uclp.grpIED, true);
            loadValues();
            uclp.txtUnitID.Focus();
        }

        private void loadDefaults()
        {
            uclp.txtUnitID.Text = (Globals.getLoadProfileIEDNo(Int32.Parse(MasterNum)) + 1).ToString();
            uclp.txtTapRatio.Text = "4.8";
            uclp.txtTxOffsetCurrent.Text = "0.75";
            uclp.txtDevice.Text = "LoadProfile"; // _" + (Globals.getLoadProfileIEDNo(Int32.Parse(MasterNum)) + 1).ToString();
            uclp.txtDescription.Text = "LoadProfile_IED_" + (Globals.getLoadProfileIEDNo(Int32.Parse(MasterNum)) + 1).ToString();
        }

        private void loadValues()
        {
            IED eied = iedList.ElementAt(editIndex);
            if (eied != null)
            {
                uclp.txtUnitID.Text = eied.UnitID;
                uclp.txtDevice.Text = eied.Device;
                uclp.txtTapRatio.Text = eied.DefaultTapRatio;
                uclp.txtTxOffsetCurrent.Text = eied.TXOffsetCurrent;
                uclp.txtDescription.Text = eied.Description;
            }
        }
        private bool Validate()
        {
            bool status = true;
            
            //Check empty field's
            if (Utils.IsEmptyFields(uclp.grpIED) || Utils.IsEmptyDropDown(uclp.grpIED)) //Ajay: 29/08/2018  Utils.IsEmptyDropDown added
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //IED UnitID should be unique...
            if (!IsUnitIDUnique(uclp.txtUnitID.Text))
            {
                MessageBox.Show("IED Unit ID must be unique!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return status;
        }

        private void fillOptions()
        {
            try
            {
                //uclp.cmbUnitID.Items.Clear();
                //uclp.cmbUnitID.DataSource = Utils.getOpenProPlusHandle().getDataTypeValues("LoadProfile_UnitID");
                //if (uclp.cmbUnitID.Items.Count > 0) { uclp.cmbUnitID.SelectedIndex = 0; }
            }
            catch { }
        }

        private void addListHeaders()
        {
            uclp.lvIEDList.Columns.Add("Unit ID", 60, HorizontalAlignment.Left);
            uclp.lvIEDList.Columns.Add("Device", 90, HorizontalAlignment.Left);
            uclp.lvIEDList.Columns.Add("Default Tap Ratio", 120, HorizontalAlignment.Left);
            uclp.lvIEDList.Columns.Add("TX Offset Current", 120, HorizontalAlignment.Left);
            uclp.lvIEDList.Columns.Add("Description", -2, HorizontalAlignment.Left);
            uclp.lvIEDList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
        }

        private void refreshList()
        {
            int cnt = 0;
            uclp.lvIEDList.Items.Clear();
            Utils.LoadProfileMasteriedList.Clear();
            foreach (IED ied in iedList)
            {
                string[] row = new string[5];
                if (ied.IsNodeComment)
                {
                    row[0] = "Comment...";
                }
                else
                {
                    row[0] = ied.UnitID;
                    row[1] = ied.Device;
                    row[2] = ied.DefaultTapRatio;
                    row[3] = ied.TXOffsetCurrent;
                    row[4] = ied.Description;
                }

                ListViewItem lvItem = new ListViewItem(row);
                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                uclp.lvIEDList.Items.Add(lvItem);
            }
            Utils.LoadProfileMasteriedList.AddRange(iedList);
        }
        private bool IsUnitIDUnique(string unitID)
        {
            for (int i = 0; i < iedList.Count; i++)
            {
                IED ied = iedList.ElementAt(i);
                if (ied.UnitID == unitID && (mode == Mode.ADD || editIndex != i)) return false;
            }

            return true;
        }

        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("LoadProfile_")) return uclp;

            kpArr.RemoveAt(0);
            Utils.WriteLine(VerboseLevel.DEBUG, "$$$$$ elem: {0}", kpArr.ElementAt(0));
            if (kpArr.ElementAt(0).Contains("IED_"))
            {
                int idx = -1;
                string[] elems = kpArr.ElementAt(0).Split('_');
                Utils.WriteLine(VerboseLevel.DEBUG, "$$$$ After split elem0: {0} elem1: {1}", elems[0], elems[elems.Length - 1]);
                idx = Int32.Parse(elems[elems.Length - 1]);
                if (iedList.Count <= 0) return null;
                return iedList[idx].getView(kpArr);
            }
            else
            {
                Utils.WriteLine(VerboseLevel.WARNING, "***** LoadProfileMaster: View for element: {0} not supported!!!", kpArr.ElementAt(0));
            }

            return null;
        }

        public TreeNode getTreeNode()
        {
            return LoadProfileTreeNode;
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
            rootNode = xmlDoc.CreateElement(mType.ToString());
            xmlDoc.AppendChild(rootNode);
            foreach (string attr in arrAttributes)
            {
                XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                rootNode.Attributes.Append(attrName);
            }
            foreach (IED iedn in iedList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(iedn.exportXMLnode(), true);
                //XmlNode importNode = rootNode.OwnerDocument.ImportNode(iedn.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }
            return rootNode;
        }

        public string exportXML()
        {
            XmlNode xmlNode = exportXMLnode();
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);

            xmlTextWriter.Formatting = Formatting.Indented;
            xmlTextWriter.Indentation = 2; //default is 1.
            xmlNode.WriteTo(xmlTextWriter);
            xmlTextWriter.Flush();

            return stringWriter.ToString();
        }

        public string exportINI(string slaveNum, string slaveID, string element, ref int ctr)
        {
            string iniData = "";

            if (element == "IED")
            {
                foreach (IED ied in iedList)
                {
                    iniData += "IED_" + ctr++ + "=" + ied.Description + "," + ied.Device + "," + (ied.DR.ToLower() == "enable" ? "YES" : "NO") + Environment.NewLine;
                }
            }
            else
            {
                foreach (IED ied in iedList)
                {
                    iniData += ied.exportINI(slaveNum, slaveID, element, ref ctr);
                }
            }

            return iniData;
        }

        public List<IED> getIEDs()
        {
            return iedList;
        }

        public List<IED> getIEDsByFilter(string iedID)
        {
            List<IED> iList = new List<IED>();
            if (iedID.ToLower() == "all") return iedList;
            else
                foreach (IED i in iedList)
                {
                    if (i.getIEDID == iedID)
                    {
                        iList.Add(i);
                        break;
                    }
                }

            return iList;
        }
        public string getMasterID
        {
            get { return "LoadProfile_" + MasterNum; }
        }

        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
       

        public string MasterNum
        {
            get { masterNum = Int32.Parse(uclp.txtMasterNo.Text); return masterNum.ToString(); }
            set { masterNum = Int32.Parse(value); uclp.txtMasterNo.Text = value; Globals.MasterNo = Int32.Parse(value); }
        }

        //public string PortNum
        //{
        //    get { portNum = Int32.Parse(uclp.txtPortNo.Text); return portNum.ToString(); }
        //    set { portNum = Int32.Parse(value); uclp.txtPortNo.Text = value; }
        //}

        //public string Run
        //{
        //    get { run = uclp.chkRun.Checked; return (run == true ? "YES" : "NO"); }
        //    set
        //    {
        //        run = (value.ToLower() == "yes") ? true : false;
        //        if (run == true) uclp.chkRun.Checked = true;
        //        else uclp.chkRun.Checked = false;
        //    }
        //}
        public string Run
        {
            get { run = uclp.chkbxRun.Checked; return (run == true ? "YES" : "NO"); }
            set
            {
                run = (value.ToLower() == "yes") ? true : false;
                if (run == true) uclp.chkbxRun.Checked = true;
                else uclp.chkbxRun.Checked = false;
            }
        }
        public string DEBUG
        {
            get { debug = Int32.Parse(uclp.txtDebug.Text); return debug.ToString(); }
            set { debug = Int32.Parse(value); uclp.txtDebug.Text = value; }
        }
        public string Description
        {
            get { return desc = uclp.txtDescription.Text; }
            set { desc = uclp.txtDescription.Text = value; }
        }
        public string TimeInterval
        {
            get { return desc = uclp.txtTimeInrvl.Text; }
            set { desc = uclp.txtTimeInrvl.Text = value; }
        }
        public string Device
        {
            get { return device = uclp.txtDevice.Text; }
            set { device = uclp.txtDevice.Text = value; }
        }
        public string DefaultTapRatio
        {
            get
            {
                double i = 0;
                Double.TryParse(uclp.txtTapRatio.Text, out i);
                dftTapRatio = i;
                return dftTapRatio.ToString();
            }
            set { desc = uclp.txtTapRatio.Text = value; }
        }
        public string TXOffsetCurrent
        {
            get
            {
                double i = 0;
                Double.TryParse(uclp.txtTxOffsetCurrent.Text, out i);
                txOffsetCurrent = i;
                return txOffsetCurrent.ToString();
            }
            set { desc = uclp.txtTxOffsetCurrent.Text = value; }
        }
    }
}
