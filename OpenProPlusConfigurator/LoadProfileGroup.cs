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
    * \brief     <b>LoadProfileGroup</b> is a class to store all the Load Profiles
    * \details   This class stores info related to all Load Profiles. It allows
    * user to add multiple Load Profiles. It also exports the XML node related to this object.
    * 
    */
    public class LoadProfileGroup
    {
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        private string rnName = "LoadProfileGroup";
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        List<LoadProfileMaster> lpList = new List<LoadProfileMaster>();
        ucGroupLoadProfile uclp = new ucGroupLoadProfile();
        private TreeNode LoadProfileGroupTreeNode;
        public LoadProfileGroup(TreeNode tn)
        {
            tn.Nodes.Clear();
            LoadProfileGroupTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...
            uclp.btnAddClick += new System.EventHandler(this.btnAdd_Click);
            uclp.lvLoadProfilemasterItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvLoadProfilemaster_ItemCheck);
            uclp.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
            uclp.btnDoneClick += new System.EventHandler(this.btnDone_Click);
            uclp.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
            uclp.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
            uclp.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
            uclp.btnNextClick += new System.EventHandler(this.btnNext_Click);
            uclp.btnLastClick += new System.EventHandler(this.btnLast_Click);
            uclp.lvLoadProfilemasterDoubleClick += new System.EventHandler(this.lvLoadProfilemaster_DoubleClick);
            addListHeaders();
            fillOptions();
        }
        private void lvLoadProfilemaster_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    uclp.lvLoadProfilemaster.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
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
            if (lpList.Count >= Globals.MaxLoadProfileMaster)
            {
                MessageBox.Show("Maximum " + Globals.MaxLoadProfileMaster + " Load Profile Master is supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Console.WriteLine("*** uclp btnAdd_Click clicked in class!!!");
            mode = Mode.ADD;
            editIndex = -1;
            Utils.resetValues(uclp.grpLoadProfile);
            Utils.showNavigation(uclp.grpLoadProfile, false);
            loadDefaults();
            //Ajay: 03/01/2019  LoadProfile MasterNum attribute value to fix it to '1'. Modification by Rohini G by mail dtd. 03/01/2019
            //uclp.txtMasterNo.Text = (Globals.MasterNo + 1).ToString();
            uclp.txtMasterNo.Text = "1"; 
            uclp.grpLoadProfile.Visible = true;
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Utils.OPPCLoadProfileList.Clear();
                if (uclp.lvLoadProfilemaster.Items.Count == 0)
                {
                    MessageBox.Show("Please add atleast one Master ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (uclp.lvLoadProfilemaster.CheckedItems.Count == 1)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete Master " + uclp.lvLoadProfilemaster.CheckedItems[0].Text + " ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        int iIndex = uclp.lvLoadProfilemaster.CheckedItems[0].Index;
                        Utils.WriteLine(VerboseLevel.DEBUG, "*** removing indices: {0}", iIndex);
                        if (result == DialogResult.Yes)
                        {
                            LoadProfileGroupTreeNode.Nodes.Remove(lpList.ElementAt(iIndex).getTreeNode());
                            lpList.RemoveAt(iIndex);
                            uclp.lvLoadProfilemaster.Items[iIndex].Remove();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select atleast one Master ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    refreshList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            Utils.OPPCLoadProfileList.Clear();
            if (!Validate()) return;
            Console.WriteLine("*** uclp btnDone_Click clicked in class!!!");
            List<KeyValuePair<string, string>> lpData = Utils.getKeyValueAttributes(uclp.grpLoadProfile);
            if (mode == Mode.ADD)
            {
                TreeNode tmp = LoadProfileGroupTreeNode.Nodes.Add("LoadProfile_" + Utils.GenerateShortUniqueKey(), "LoadProfile", "LoadProfileMaster", "LoadProfileMaster");
                lpList.Add(new LoadProfileMaster("LoadProfile", lpData, tmp));
            }
            else if (mode == Mode.EDIT)
            {
                lpList[editIndex].updateAttributes(lpData);
            }
            refreshList();
            uclp.grpLoadProfile.Visible = false;
            mode = Mode.NONE;
            editIndex = -1;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** uclp btnCancel_Click clicked in class!!!");
            uclp.grpLoadProfile.Visible = false;
            mode = Mode.NONE;
            editIndex = -1;
            Utils.resetValues(uclp.grpLoadProfile);
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** uclp btnFirst_Click clicked in class!!!");
            if (uclp.lvLoadProfilemaster.Items.Count <= 0) return;
            if (lpList.ElementAt(0).IsNodeComment) return;
            editIndex = 0;
            loadValues();
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** uclp btnPrev_Click clicked in class!!!");
            if (editIndex - 1 < 0) return;
            if (lpList.ElementAt(editIndex - 1).IsNodeComment) return;
            editIndex--;
            loadValues();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** uclp btnNext_Click clicked in class!!!");
            if (editIndex + 1 >= uclp.lvLoadProfilemaster.Items.Count) return;
            if (lpList.ElementAt(editIndex + 1).IsNodeComment) return;
            editIndex++;
            loadValues();
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** uclp btnLast_Click clicked in class!!!");
            if (uclp.lvLoadProfilemaster.Items.Count <= 0) return;
            if (lpList.ElementAt(lpList.Count - 1).IsNodeComment) return;
            editIndex = lpList.Count - 1;
            loadValues();
        }
        private void lvLoadProfilemaster_DoubleClick(object sender, EventArgs e)
        {
            Console.WriteLine("*** uclp lvLoadProfilemaster_DoubleClick clicked in class!!!");

            // Ajay: 23/11/2018
            if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
            {
                if (ProtocolGateway.OppLoadProfileGroup_ReadOnly) { return; }
                else { }
            }

            if (uclp.lvLoadProfilemaster.SelectedItems.Count <= 0) return;

            ListViewItem lvi = uclp.lvLoadProfilemaster.SelectedItems[0];
            Utils.UncheckOthers(uclp.lvLoadProfilemaster, lvi.Index);
            if (lpList.ElementAt(lvi.Index).IsNodeComment)
            {
                MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            uclp.grpLoadProfile.Visible = true;
            mode = Mode.EDIT;
            editIndex = lvi.Index;
            Utils.showNavigation(uclp.grpLoadProfile, true);
            loadValues();
        }
        private void loadDefaults()
        {
            uclp.txtTimeIntrvl.Text = "15"; //Ajay: 22/09/2018 
            if (uclp.cmbDebug.Items.Count > 0) { uclp.cmbDebug.SelectedIndex = uclp.cmbDebug.FindStringExact("3"); }
            //if (uclp.cmbTimeIntrvl.Items.Count > 0) { uclp.cmbTimeIntrvl.SelectedIndex = uclp.cmbTimeIntrvl.FindStringExact("15"); } //Ajay: 22/09/2018 commented
            //uclp.txtTimeInrvl.Text = "15";
            //uclp.txtDescription.Text = "LoadProfile_" + (Globals.MasterNo + 1).ToString();
        }
        private void loadValues()
        {
            LoadProfileMaster lpm = lpList.ElementAt(editIndex);
            if (lpm != null)
            {
                uclp.txtMasterNo.Text = lpm.MasterNum;
                uclp.txtTimeIntrvl.Text = lpm.TimeInterval; //Ajay: 22/09/2018 
                //uclp.txtTimeInrvl.Text = lpm.TimeInterval;
                //uclp.cmbTimeIntrvl.SelectedIndex = uclp.cmbTimeIntrvl.FindStringExact(lpm.TimeInterval); //Ajay: 22/09/2018 commented
                uclp.cmbDebug.SelectedIndex = uclp.cmbDebug.FindStringExact(lpm.DEBUG);
                if (lpm.Run.ToLower() == "yes") uclp.chkbxRun.Checked = true;
                else uclp.chkbxRun.Checked = false;
                //uclp.txtDescription.Text = lpm.Description;
            }
        }
        private bool Validate()
        {
            bool status = true;
            //Check empty field's
            if (Utils.IsEmptyFields(uclp.grpLoadProfile) || Utils.IsEmptyDropDown(uclp.grpLoadProfile)) //Ajay: 29/08/2018  Utils.IsEmptyDropDown added
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return status;
        }
        private void fillOptions()
        {
            //Fill Debug levels...
            uclp.cmbDebug.Items.Clear();
            for (int i = 1; i <= Globals.MAX_DEBUG_LEVEL; i++)
            {
                uclp.cmbDebug.Items.Add(i.ToString());
            }
            uclp.cmbDebug.SelectedIndex = 0;
            //Ajay: 22/09/2018 commented
            //Fill Time Interval
            //uclp.cmbTimeIntrvl.Items.Clear();
            //uclp.cmbTimeIntrvl.DataSource = Utils.getOpenProPlusHandle().getDataTypeValues("LoadProfile_TimeInterval");
            //if (uclp.cmbTimeIntrvl.Items.Count > 0) { uclp.cmbTimeIntrvl.SelectedIndex = 0; }
        }
        private void addListHeaders()
        {
            uclp.lvLoadProfilemaster.Columns.Add("Master No.", 80, HorizontalAlignment.Left);
            uclp.lvLoadProfilemaster.Columns.Add("Time Interval(min)", 110, HorizontalAlignment.Left);//Namrata:27/03/2019
            uclp.lvLoadProfilemaster.Columns.Add("Debug Level", 90, HorizontalAlignment.Left);
            uclp.lvLoadProfilemaster.Columns.Add("Run", 70, HorizontalAlignment.Left);
            //uclp.lvLoadProfilemaster.Columns.Add("Description", 170, HorizontalAlignment.Left);
            uclp.lvLoadProfilemaster.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
        }
        private void refreshList()
        {
            Utils.OPPCLoadProfileList.Clear();
            int cnt = 0;
            uclp.lvLoadProfilemaster.Items.Clear();
            foreach (LoadProfileMaster mlp in lpList)
            {
                string[] row = new string[4];
                if (mlp.IsNodeComment)
                {
                    row[0] = "Comment...";
                }
                else
                {
                    row[0] = mlp.MasterNum;
                    row[1] = mlp.TimeInterval;
                    row[2] = mlp.DEBUG;
                    row[3] = mlp.Run;
                }
                ListViewItem lvItem = new ListViewItem(row);
                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                uclp.lvLoadProfilemaster.Items.Add(lvItem);
            }
            Utils.OPPCLoadProfileList.AddRange(lpList);
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("LoadProfileGroup_"))
            {
                return uclp;
            }
            kpArr.RemoveAt(0);
            if (kpArr.ElementAt(0).Contains("LoadProfile_"))
            {
                int idx = -1;
                string[] elems = kpArr.ElementAt(0).Split('_');
                Console.WriteLine("$$$$ elem0: {0} elem1: {1}", elems[0], elems[elems.Length - 1]);
                idx = Int32.Parse(elems[elems.Length - 1]);
                if (lpList.Count <= 0) return null;
                return lpList[idx].getView(kpArr);
            }
            else
            {
                Console.WriteLine("***** View for element: {0} not supported!!!", kpArr.ElementAt(0));
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
            foreach (LoadProfileMaster mn in lpList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(mn.exportXMLnode(), true);
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
            foreach (LoadProfileMaster lpm in lpList)
            {
                iniData += lpm.exportINI(slaveNum, slaveID, element, ref ctr);
            }
            return iniData;
        }

        public void regenerateSequence()
        {
            int oMasterNo = -1;
            int nMasterNo = -1;
            //Reset MODBUS master no.
            Globals.resetUniqueNos(ResetUniqueNos.LOADPROFILE);
            Globals.MasterNo++;//Start from 1...
            foreach (LoadProfileMaster lpm in lpList)
            {
                oMasterNo = Int32.Parse(lpm.MasterNum);
                nMasterNo = Globals.MasterNo++;
                lpm.MasterNum = nMasterNo.ToString();
            }
            Utils.getOpenProPlusHandle().getMasterConfiguration().getLoadProfileGroup().refreshList();
        }

        public void regenerateAISequence()
        {
            foreach (LoadProfileMaster lpm in lpList)
            {
                foreach (IED ied in lpm.getIEDs())
                {
                    ied.regenerateAISequence();
                }
            }
        }
        public void regenerateAOSequence()
        {
            foreach (LoadProfileMaster lpm in lpList)
            {
                foreach (IED ied in lpm.getIEDs())
                {
                    ied.regenerateAOSequence();
                }
            }
        }
        public void regenerateDISequence()
        {
            foreach (LoadProfileMaster lpm in lpList)
            {
                foreach (IED ied in lpm.getIEDs())
                {
                    ied.regenerateDISequence();
                }
            }
        }

        public void regenerateDOSequence()
        {
            foreach (LoadProfileMaster lpm in lpList)
            {
                foreach (IED ied in lpm.getIEDs())
                {
                    ied.regenerateDOSequence();
                }
            }
        }

        public void regenerateENSequence()
        {
            foreach (LoadProfileMaster lpm in lpList)
            {
                foreach (IED ied in lpm.getIEDs())
                {
                    ied.regenerateENSequence();
                }
            }
        }

        public void parseMBGNode(XmlNode mbgNode, TreeNode tn)
        {
            //First set root node name...
            rnName = mbgNode.Name;
            tn.Nodes.Clear();
            LoadProfileGroupTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...
            foreach (XmlNode node in mbgNode)
            {
                //Console.WriteLine("***** node type: {0}", node.NodeType);
                if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                TreeNode tmp = tn.Nodes.Add("LoadProfile_" + Utils.GenerateShortUniqueKey(), "LoadProfile", "LoadProfileMaster", "LoadProfileMaster");
                lpList.Add(new LoadProfileMaster(node, tmp));
            }
            refreshList();
        }
        public int getCount()
        {
            int ctr = 0;
            foreach (LoadProfileMaster lpNode in lpList)
            {
                if (lpNode.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }
        public List<LoadProfileMaster> getLoadProfileMasters()
        {
            return lpList;
        }
        public List<LoadProfileMaster> getLoadProfileMastersByFilter(string masterID)
        {
            List<LoadProfileMaster> mList = new List<LoadProfileMaster>();
            if (masterID.ToLower() == "all") return lpList;
            else
                foreach (LoadProfileMaster m in lpList)
                {
                    if (m.getMasterID == masterID)
                    {
                        mList.Add(m);
                        break;
                    }
                }

            return mList;
        }
        public List<IED> getLoadProfileIEDsByFilter(string masterID)
        {
            List<IED> iLst = new List<IED>();

            if (masterID.ToLower() == "all")
            {
                foreach (LoadProfileMaster m in lpList)
                {
                    foreach (IED ied in m.getIEDs())
                    {
                        iLst.Add(ied);
                    }
                }
            }
            else
            {
                foreach (LoadProfileMaster m in lpList)
                {
                    if (m.getMasterID == masterID)
                    {
                        foreach (IED ied in m.getIEDs())
                        {
                            iLst.Add(ied);
                        }
                        break;
                    }
                }
            }

            return iLst;
        }
    }
}
