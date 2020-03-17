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
using System.Reflection;
using System.Text.RegularExpressions;
namespace OpenProPlusConfigurator
{
    public class SMSSlaveGroup
    {
        #region Declaration
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        private string rnName = "SMSSlaveGroup";
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        ucGroupSMSSlave ucSMS = new ucGroupSMSSlave();
        private TreeNode SMSlaveGroupTreeNode;
        public List<SMSSlave> SMSSlaveList = new List<SMSSlave>();//Expose the list to others for slave mapping...
        #endregion Declaration
        public SMSSlaveGroup(TreeNode tn)
        {
            string strRoutineName = "SMSSlaveGroup:SMSSlaveGroup";
            try
            {
                tn.Nodes.Clear();
                SMSlaveGroupTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...
                Utils.SMSSlaveTreeNode = SMSlaveGroupTreeNode;
                ucSMS.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                ucSMS.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                ucSMS.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucSMS.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucSMS.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                ucSMS.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                ucSMS.btnNextClick += new System.EventHandler(this.btnNext_Click);
                ucSMS.btnLastClick += new System.EventHandler(this.btnLast_Click);
                ucSMS.lvSMSSlaveItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvSMSSlave_ItemCheck);
                ucSMS.lvSMSSlaveDoubleClick += new System.EventHandler(this.lvSMSSlave_DoubleClick);
                ucSMS.chkUserFriendlySMSCheckedChanged += new System.EventHandler(this.chkUserFriendlySMS_CheckedChanged);
                ucSMS.chkUserFriendlySMSCheckStateChanged += new System.EventHandler(this.chkUserFriendlySMS_CheckStateChanged);
                addListHeaders();
                fillOptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public SMSSlaveGroup()
        {
            string strRoutineName = "SMSSlaveGroup";
            try
            {
                ucSMS.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                ucSMS.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                ucSMS.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucSMS.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucSMS.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                ucSMS.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                ucSMS.btnNextClick += new System.EventHandler(this.btnNext_Click);
                ucSMS.btnLastClick += new System.EventHandler(this.btnLast_Click);
                ucSMS.lvSMSSlaveItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvSMSSlave_ItemCheck);
                ucSMS.lvSMSSlaveDoubleClick += new System.EventHandler(this.lvSMSSlave_DoubleClick);
                ucSMS.chkUserFriendlySMSCheckedChanged += new System.EventHandler(this.chkUserFriendlySMS_CheckedChanged);
                ucSMS.chkUserFriendlySMSCheckStateChanged += new System.EventHandler(this.chkUserFriendlySMS_CheckStateChanged);
                addListHeaders();
                fillOptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void chkUserFriendlySMS_CheckedChanged(object sender, EventArgs e)
        {
            if(ucSMS.chkUserFriendlySMS.Checked==true)
            {
                Utils.IsValidate = true;
            }
            else
            {
                Utils.IsValidate = false;
            }
           
        }
        private void chkUserFriendlySMS_CheckStateChanged(object sender, EventArgs e)
        {
            //if (ucSMS.chkUserFriendlySMS.CheckState == CheckState.Checked)
            //{
            //    Utils.IsValidate = true;
            //}
            //else
            //{
            //    Utils.IsValidate = false;
            //}

        }
        private void lvSMSSlave_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "lvSMSSlave_DoubleClick";
            try
            {
                // Ajay: 23/11/2018
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    if (ProtocolGateway.OppSPORTSlaveGroup_ReadOnly) { return; }
                    else { }
                }

                if (ucSMS.lvSMSSlave.SelectedItems.Count <= 0) return;
                ListViewItem lvi = ucSMS.lvSMSSlave.SelectedItems[0];
                Utils.UncheckOthers(ucSMS.lvSMSSlave, lvi.Index);
                if (SMSSlaveList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ucSMS.grpSMSSlave.Visible = true;
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(ucSMS.grpSMSSlave, true);
                loadValues();
                ucSMS.txtSlaveNum.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void lvSMSSlave_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string strRoutineName = "SMSSlaveGroup:lvSMSSlave_ItemCheck";
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    ucSMS.lvSMSSlave.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
                    {
                        item.Checked = false;
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void fillOptions()
        {
            string strRoutineName = "SMSSlaveGroup: fillOptions";
            try
            {
                #region  Fill Modem ...
                ucSMS.CmbModem.Items.Clear();
                foreach (String st in SMSSlave.getModem())
                {
                    ucSMS.CmbModem.Items.Add(st.ToString());
                }
                if (ucSMS.CmbModem.Items.Count > 0) { ucSMS.CmbModem.SelectedIndex = 0; }
                #endregion  Fill Modem ...

                #region  Fill Debug levels...
                ucSMS.cmbDebug.Items.Clear();
                for (int i = 1; i <= Globals.MAX_DEBUG_LEVEL; i++)
                {
                    ucSMS.cmbDebug.Items.Add(i.ToString());
                }
                ucSMS.cmbDebug.SelectedIndex = 0;
                #endregion  Fill Debug levels...
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string strRoutineName = "SMSSlaveGroup:btnAdd_Click";
            try
            {
                if (SMSSlaveList.Count >= Globals.MaxSMSSlave)
                {
                    MessageBox.Show("Maximum " + Globals.MaxSMSSlave + " SMS Slaves are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //Ajay: 25/08/2018 
                bool IsContinue = false;
                if (Globals.TotalSlavesCount < Globals.MaxTotalNoOfSlaves)
                {
                    IsContinue = true;
                }
                else
                {
                    DialogResult rslt = MessageBox.Show("Maximum " + Globals.MaxTotalNoOfSlaves + " Slaves are recommended to be added." + Environment.NewLine + "Do you still want to continue?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rslt.ToString().ToLower() == "no")
                    { return; }
                    else { IsContinue = true; }
                }
                if (IsContinue)
                {
                    mode = Mode.ADD;
                    editIndex = -1;
                    Utils.resetValues(ucSMS.grpSMSSlave);
                    Utils.showNavigation(ucSMS.grpSMSSlave, false);
                    loadDefaults();
                    ucSMS.txtSlaveNum.Text = (Globals.SlaveNo + 1).ToString();
                    ucSMS.grpSMSSlave.Visible = true;
                    ucSMS.txtSlaveNum.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strRoutineName = "SMSSlaveGroup:btnDelete_Click";
            try
            {
                Utils.WriteLine(VerboseLevel.DEBUG, "*** SMSSlaveList count: {0} lv count: {1}", SMSSlaveList.Count, ucSMS.lvSMSSlave.Items.Count);
                if (ucSMS.lvSMSSlave.Items.Count == 0)
                {
                    MessageBox.Show("Please add atleast one slave ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (ucSMS.lvSMSSlave.CheckedItems.Count == 1)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete Slave No." + ucSMS.lvSMSSlave.CheckedItems[0].Text + " ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        int iIndex = ucSMS.lvSMSSlave.CheckedItems[0].Index;
                        if (result == DialogResult.Yes)
                        {
                            SMSlaveGroupTreeNode.Nodes.Remove(SMSSlaveList.ElementAt(iIndex).getTreeNode());
                            SMSSlaveList.RemoveAt(iIndex);
                            ucSMS.lvSMSSlave.Items[iIndex].Remove();
                            Globals.TotalSlavesCount -= 1; //Ajay: 25/08/2018
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select atleast one Slave ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    refreshList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnExportINI_Click(object sender, EventArgs e)
        {
            //FIXME: Check if the 'xmlFile' exists, meaning the filename exist...
        }
        //private void btnDone_Click(object sender, EventArgs e)
        //{
        //    string strRoutineName = "SMSSlaveGroup:btnDone_Click";
        //    try
        //    {
        //        //TreeNode tmp=null;
        //        if (!Validate()) return;
        //        List<KeyValuePair<string, string>> mbsData = Utils.getKeyValueAttributes(ucSMS.grpSMSSlave);
        //        if (!Utils.IsValidate)
        //        {
        //            if (!CheckTextBox(ucSMS.txtRemoteMobileNo)) return;
        //        }
        //        if (mode == Mode.ADD)
        //        {
        //            if(mbsData[2].Value =="")
        //            {
        //                MessageBox.Show("Please select Port No.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                return;
        //            }
        //            TreeNode tmp = null;
        //            if(Utils.IsValidate)
        //            {
        //                tmp = SMSlaveGroupTreeNode.Nodes.Add("SMSSlave_" + Utils.GenerateShortUniqueKey(), "SMSSlave", "SMSSlave", "SMSSlave");
        //                SMSSlaveList.Add(new SMSSlave("SMSSlave", mbsData, tmp));
        //            }
        //            else
        //            {
        //                SMSSlaveList.Add(new SMSSlave("SMSSlave", mbsData, tmp));
        //            }
        //            Globals.TotalSlavesCount += 1; //Ajay: 25/08/2018
        //        }
        //        else if (mode == Mode.EDIT)
        //        {
        //            TreeNode tmp = null;
        //            if (Utils.IsValidate)
        //            {
        //                if(SMSSlaveList[editIndex].SMSSlaveTreeNode == null)
        //                {
        //                    tmp = SMSlaveGroupTreeNode.Nodes.Add("SMSSlave_" + Utils.GenerateShortUniqueKey(), "SMSSlave", "SMSSlave", "SMSSlave");
        //                    SMSSlave NewAIMap = new SMSSlave("SMSSlave", mbsData, tmp);
        //                    SMSSlaveList[editIndex].SMSSlaveTreeNode = Utils.SMSSlaveTreeNode;
        //                    SMSSlaveList[editIndex].updateAttributes(mbsData);
        //                }
        //                else
        //                {
        //                    SMSSlaveList[editIndex].updateAttributes(mbsData);
        //                }

        //            }
        //            else if (Utils.IsValidate == false)
        //            {
        //                if (SMSSlaveList[editIndex].SMSSlaveTreeNode != null)
        //                {
        //                    SMSlaveGroupTreeNode.Nodes.Remove(SMSSlaveList[editIndex].SMSSlaveTreeNode);
        //                    SMSSlaveList[editIndex].SMSSlaveTreeNode = null;
        //                }
        //                else
        //                {
        //                    SMSSlaveList[editIndex].updateAttributes(mbsData);
        //                }
        //            }
        //            else { SMSSlaveList[editIndex].updateAttributes(mbsData); }
        //        }
        //        refreshList();
        //        if (sender != null && e != null)
        //        {
        //            ucSMS.grpSMSSlave.Visible = false;
        //            mode = Mode.NONE;
        //            editIndex = -1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        private void btnDone_Click(object sender, EventArgs e)
        {
            string strRoutineName = "SMSSlaveGroup:btnDone_Click";
            try
            {
                if (!Validate()) return;
                List<KeyValuePair<string, string>> mbsData = Utils.getKeyValueAttributes(ucSMS.grpSMSSlave);
                if (!Utils.IsValidate)
                {
                    if (!CheckTextBox(ucSMS.txtRemoteMobileNo)) return;
                }
                if (mode == Mode.ADD)
                {
                    if (mbsData[2].Value == "")
                    {
                        MessageBox.Show("Please select Port No.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    TreeNode tmp = null;
                    if (Utils.IsValidate)
                    {
                        tmp = SMSlaveGroupTreeNode.Nodes.Add("SMSSlave_" + Utils.GenerateShortUniqueKey(), "SMSSlave", "SMSSlave", "SMSSlave");
                        SMSSlaveList.Add(new SMSSlave("SMSSlave", mbsData, tmp));
                    }
                    else
                    {
                        SMSSlaveList.Add(new SMSSlave("SMSSlave", mbsData, tmp));
                    }
                    Globals.TotalSlavesCount += 1; //Ajay: 25/08/2018
                }
                else if (mode == Mode.EDIT)
                {
                    TreeNode tmp = null;
                    if (Utils.IsValidate)
                    {
                        if (SMSSlaveList[editIndex].SMSSlaveTreeNode == null)
                        {
                            tmp = SMSlaveGroupTreeNode.Nodes.Add("SMSSlave_" + Utils.GenerateShortUniqueKey(), "SMSSlave", "SMSSlave", "SMSSlave");
                            SMSSlave NewAIMap = new SMSSlave("SMSSlave", mbsData, tmp);
                            SMSSlaveList[editIndex].SMSSlaveTreeNode = Utils.SMSSlaveTreeNode;
                            SMSSlaveList[editIndex].updateAttributes(mbsData);
                        }
                        else
                        {
                            SMSSlaveList[editIndex].updateAttributes(mbsData);
                        }

                    }
                    else if (Utils.IsValidate == false)
                    {
                        if (SMSSlaveList[editIndex].SMSSlaveTreeNode != null)
                        {
                            SMSlaveGroupTreeNode.Nodes.Remove(SMSSlaveList[editIndex].SMSSlaveTreeNode);
                            SMSSlaveList[editIndex].SMSSlaveTreeNode = null;
                            SMSSlaveList[editIndex].updateAttributes(mbsData);
                        }
                        else
                        {
                            SMSSlaveList[editIndex].updateAttributes(mbsData);
                        }
                    }
                    else { SMSSlaveList[editIndex].updateAttributes(mbsData); }
                }
                refreshList();
                if (sender != null && e != null)
                {
                    ucSMS.grpSMSSlave.Visible = false;
                    mode = Mode.NONE;
                    editIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            string strRoutineName = "SMSSlaveGroup:btnCancel_Click";
            try
            {
                Utils.IntIEC104Modbus = Utils.IntIEC104Modbus - 1;
                ucSMS.grpSMSSlave.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            string strRoutineName = "SMSSlaveGroup:btnFirst_Click";
            try
            {
                if (ucSMS.lvSMSSlave.Items.Count <= 0) return;
                if (SMSSlaveList.ElementAt(0).IsNodeComment) return;
                editIndex = 0;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            string strRoutineName = "SMSSlaveGroup:btnPrev_Click";
            try
            {
                //Namrata:27/7/2017
                btnDone_Click(null, null);
                Console.WriteLine("*** ucSMS btnPrev_Click clicked in class!!!");
                if (editIndex - 1 < 0) return;
                if (SMSSlaveList.ElementAt(editIndex - 1).IsNodeComment) return;
                editIndex--;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            string strRoutineName = "SMSSlaveGroup:btnNext_Click";
            try
            {
                //Namrata:27/7/2017
                btnDone_Click(null, null);
                Console.WriteLine("*** ucSMS btnNext_Click clicked in class!!!");
                if (editIndex + 1 >= ucSMS.lvSMSSlave.Items.Count) return;
                if (SMSSlaveList.ElementAt(editIndex + 1).IsNodeComment) return;
                editIndex++;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            string strRoutineName = "SMSSlaveGroup:btnLast_Click";
            try
            {
                Console.WriteLine("*** ucSMS btnLast_Click clicked in class!!!");
                if (ucSMS.lvSMSSlave.Items.Count <= 0) return;
                if (SMSSlaveList.ElementAt(SMSSlaveList.Count - 1).IsNodeComment) return;
                editIndex = SMSSlaveList.Count - 1;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvMODBUSSlave_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "SMSSlaveGroup:lvMODBUSSlave_DoubleClick";
            try
            {
                // Ajay: 23/11/2018
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    if (ProtocolGateway.OppSMSSlaveGroup_ReadOnly) { return; }
                    else { }
                }

                if (ucSMS.lvSMSSlave.SelectedItems.Count <= 0) return;
                ListViewItem lvi = ucSMS.lvSMSSlave.SelectedItems[0];
                Utils.UncheckOthers(ucSMS.lvSMSSlave, lvi.Index);
                if (SMSSlaveList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ucSMS.grpSMSSlave.Visible = true;
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(ucSMS.grpSMSSlave, true);
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void fillPortNos()
        {
            string strRoutineName = "fillPortNos";
            try
            {
                //Namrata: 16/10/2017
                ucSMS.cmbPortNo.DataSource = null;
                ucSMS.cmbPortNo.Items.Clear();
                foreach (SerialInterface si in Utils.getOpenProPlusHandle().getSerialPortConfiguration().getSerialInterfaces())
                {
                    if (si.Enable == "YES")
                    {
                        DataTable DTSerialConfig = Utils.dtSerialConfig.Tables[0];
                        var PortNumTable = (from n in DTSerialConfig.AsEnumerable()
                                            where n.Field<string>("Enable").Contains("YES")
                                            select n).CopyToDataTable();

                        ucSMS.cmbPortNo.DataSource = PortNumTable;
                        ucSMS.cmbPortNo.DisplayMember = "PortNum";
                        ucSMS.cmbPortNo.ValueMember = "Enable";
                        ucSMS.cmbPortNo.SelectedIndex = ucSMS.cmbPortNo.SelectedIndex;
                    }
                }
                //foreach (SerialInterface si in Utils.getOpenProPlusHandle().getSerialPortConfiguration().getSerialInterfaces())
                //{
                //    ucSMS.cmbPortNo.Items.Add(si.PortNum);
                //}
                if (ucSMS.cmbPortNo.Items.Count > 0) ucSMS.cmbPortNo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadDefaults()
        {
            string strRoutineName = "SMSSlaveGroup:loadDefaults";
            try
            {
                ucSMS.CmbModem.SelectedIndex = ucSMS.CmbModem.FindStringExact("Quectel");
                ucSMS.txtTolerancePeriodInMin.Text = "0";
                ucSMS.txtRemoteMobileNo.Text = "+";
                ucSMS.txtFirmwareVersion.Text = Globals.FIRMWARE_VERSION; //Namrata: 24/05/2019
                ucSMS.cmbDebug.SelectedIndex = ucSMS.cmbDebug.FindStringExact("3"); //Namrata: 24/05/2019

                ucSMS.txtUnitID.Text = "1";
                ucSMS.chkUserFriendlySMS.Checked = true;
                ucSMS.txtEventQSize.Text = "1000";
                //Namrata:25/05/2019
                foreach (SerialInterface si in Utils.getOpenProPlusHandle().getSerialPortConfiguration().getSerialInterfaces())
                {
                    if (si.Enable == "YES")
                    {
                        DataTable DTSerialConfig = Utils.dtSerialConfig.Tables[0];
                        var PortNumTable = (from n in DTSerialConfig.AsEnumerable()
                                            where n.Field<string>("Enable").Contains("YES")
                                            select n).CopyToDataTable();

                        ucSMS.cmbPortNo.DataSource = PortNumTable;
                        ucSMS.cmbPortNo.DisplayMember = "PortNum";
                        ucSMS.cmbPortNo.ValueMember = "Enable";
                        ucSMS.cmbPortNo.SelectedIndex = ucSMS.cmbPortNo.SelectedIndex;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadValues()
        {
            string strRoutineName = "SMSSlaveGroup:loadValues";
            try
            {
                SMSSlave SMS = SMSSlaveList.ElementAt(editIndex);
                if (SMS != null)
                {
                    ucSMS.txtSlaveNum.Text = SMS.SlaveNum;
                    ucSMS.txtUnitID.Text = SMS.UnitID;
                    ucSMS.CmbModem.SelectedIndex = ucSMS.CmbModem.FindStringExact(SMS.Modem);
                    ucSMS.txtTolerancePeriodInMin.Text = SMS.TolerancePeriodInMin;
                    ucSMS.txtRemoteMobileNo.Text = SMS.RemoteMobileNo;
                    ucSMS.cmbPortNo.Text = SMS.PortNum;
                    ucSMS.txtEventQSize.Text = SMS.EventQSize;
                    DataRowView oDataRowView = ucSMS.cmbPortNo.SelectedItem as DataRowView;
                    string sValue = string.Empty;
                    if (oDataRowView != null)
                    {
                        sValue = oDataRowView.Row["PortNum"] as string;
                    }
                    if (ucSMS.cmbPortNo.Items.OfType<DataRowView>().Where(x => x.Row.ItemArray[0].ToString() == sValue).Any())
                    {
                        ucSMS.cmbPortNo.SelectedIndex = ucSMS.cmbPortNo.Items.IndexOf(ucSMS.cmbPortNo.Items.OfType<DataRowView>().Where(x => x.Row.ItemArray[0].ToString() == sValue).Select(x => x).SingleOrDefault());
                    }
                    else
                    {
                        ucSMS.cmbPortNo.SelectedIndex = 0;
                    }

                    ucSMS.txtFirmwareVersion.Text = SMS.AppFirmwareVersion;
                    ucSMS.cmbDebug.SelectedIndex = ucSMS.cmbDebug.FindStringExact(SMS.DEBUG);

                    foreach (NetworkInterface ni in Utils.getOpenProPlusHandle().getNetworkConfiguration().getNetworkInterfaces())
                    {
                        if (ni.Enable == "YES")
                        {
                            if (ucSMS.cmbPortNo.Items.OfType<DataRowView>().Where(x => x.Row.ItemArray[0].ToString() == ni.PortNum).Any())
                            {
                                ucSMS.cmbPortNo.SelectedIndex = ucSMS.cmbPortNo.Items.IndexOf(ucSMS.cmbPortNo.Items.OfType<DataRowView>().Where(x => x.Row.ItemArray[0].ToString() == ni.PortNum).Select(x => x).SingleOrDefault());
                            }
                        }
                    }
                    if (SMS.Run.ToLower() == "yes") ucSMS.chkRun.Checked = true;
                    else ucSMS.chkRun.Checked = false;
                    if (SMS.UserFriendlySMS.ToLower() == "yes") ucSMS.chkUserFriendlySMS.Checked = true;
                    else ucSMS.chkUserFriendlySMS.Checked = false;
                    if (SMS.EnableEncryption.ToLower() == "yes") ucSMS.chkEnableEncryption.Checked = true;
                    else ucSMS.chkEnableEncryption.Checked = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool Validate()
        {
            bool status = true;

            //Check empty field's
            if (Utils.IsEmptyFields(ucSMS.grpSMSSlave))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return status;
        }
        private void addListHeaders()
        {
            string strRoutineName = "SMSSlaveGroup:addListHeaders";
            try
            {
                ucSMS.lvSMSSlave.Columns.Add("Slave No.", 70, HorizontalAlignment.Left);
                ucSMS.lvSMSSlave.Columns.Add("UnitID", 70, HorizontalAlignment.Left);
                ucSMS.lvSMSSlave.Columns.Add("Port No.", 80, HorizontalAlignment.Left);
                ucSMS.lvSMSSlave.Columns.Add("EventQSize", 120, HorizontalAlignment.Left);
                ucSMS.lvSMSSlave.Columns.Add("Modem", 100, HorizontalAlignment.Left);
                ucSMS.lvSMSSlave.Columns.Add("TolerancePeriod(Min)", 140, HorizontalAlignment.Left);
                ucSMS.lvSMSSlave.Columns.Add("Remote Mobile No.", 120, HorizontalAlignment.Left);
                ucSMS.lvSMSSlave.Columns.Add("Enable Encryption", 100, HorizontalAlignment.Left);
                ucSMS.lvSMSSlave.Columns.Add("User Friendly SMS", 100, HorizontalAlignment.Left);
                ucSMS.lvSMSSlave.Columns.Add("Firmware Version", 100, HorizontalAlignment.Left);//Namrata: 24/05/2019
                ucSMS.lvSMSSlave.Columns.Add("Debug Level", 100, HorizontalAlignment.Left);//Namrata: 24/05/2019
                ucSMS.lvSMSSlave.Columns.Add("Run", 70, HorizontalAlignment.Left);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void refreshList()
        {
            string strRoutineName = "SMSSlaveGroup:refreshList";
            try
            {
                int cnt = 0;
                ucSMS.lvSMSSlave.Items.Clear();
                foreach (SMSSlave sl in SMSSlaveList)
                {
                    string[] row = new string[12];
                    if (sl.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = sl.SlaveNum;
                        row[1] = sl.UnitID;
                        row[2] = sl.PortNum;
                        row[3] = sl.EventQSize;
                        row[4] = sl.Modem;
                        row[5] = sl.TolerancePeriodInMin;
                        row[6] = sl.RemoteMobileNo;
                        row[7] = sl.EnableEncryption;
                        row[8] = sl.UserFriendlySMS;
                        row[9] = sl.AppFirmwareVersion;
                        row[10] = sl.DEBUG;
                        row[11] = sl.Run;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucSMS.lvSMSSlave.Items.Add(lvItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool CheckTextBox(TextBox tb)
        {
            bool isValid = true;
            Regex reg = new Regex("^\\+[0-9]+$"); ;
            if (reg.IsMatch(tb.Text.Trim()) == false || tb.Text.Length > 14)
            {
                //MessageBox.Show(tb.Name+ "Invalid Mobile Number!!",Application.ProductName,MessageBoxButtons.OK,MessageBoxIcon.Warning);
                MessageBox.Show("Please enter valid Mobile Number" + tb.Name + "!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb.Focus();
                return false;
            }
            return isValid;
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("SMSSlaveGroup_"))
            {
                fillPortNos();
                return ucSMS;
            }
            kpArr.RemoveAt(0);

            if (kpArr.ElementAt(0).Contains("SMSSlave_"))
            {
                int idx = -1;
                string[] elems = kpArr.ElementAt(0).Split('_');
                //Namrata: 10/06/2019
                int iIndex = 0;
                if(SMSSlaveList.Where(x => x.SlaveNum == Utils.SMSSlaveNo).Select(x => x).Any())
                {
                    iIndex = SMSSlaveList.IndexOf(SMSSlaveList.Where(x => x.SlaveNum == Utils.SMSSlaveNo).Select(x => x).FirstOrDefault());
                }
                //idx = Int32.Parse(elems[elems.Length - 1]);
                if (SMSSlaveList.Count <= 0) return null;
                return SMSSlaveList[iIndex].getView(kpArr);
            }
            else
            {
                Utils.WriteLine(VerboseLevel.WARNING, "***** View for element: {0} not supported!!!", kpArr.ElementAt(0));
            }
            return null;
        }
        public void parseIECGNode(XmlNode iecgNode, TreeNode tn)
        {
            string strRoutineName = "SMSSlaveGroup:parseIECGNode";
            try
            {
                rnName = iecgNode.Name;
                tn.Nodes.Clear();
                SMSlaveGroupTreeNode = tn;
                foreach (XmlNode node in iecgNode)
                {
                    XmlElement root = node.ParentNode as XmlElement;
                    if (node.NodeType == XmlNodeType.Comment) continue;
                    TreeNode tmp = tn.Nodes.Add("SMSSlave_" + Utils.GenerateShortUniqueKey(), "SMSSlave", "SMSSlaveGroup", "SMSSlaveGroup");
                    SMSSlaveList.Add(new SMSSlave(node, tmp));
                }
                refreshList();
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
            foreach (SMSSlave sn in SMSSlaveList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(sn.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }
            return rootNode;
        }
        public int getCount()
        {
            int ctr = 0;
            foreach (SMSSlave SMSNode in SMSSlaveList)
            {
                if (SMSNode.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }
        public List<SMSSlave> getSMSSlaves()
        {
            return SMSSlaveList;
        }
        public List<SMSSlave> getSMSSlavesByFilter(string slaveID)
        {
            List<SMSSlave> SMSList = new List<SMSSlave>();
            if (slaveID.ToLower() == "all") return SMSSlaveList;
            else
                foreach (SMSSlave iec in SMSSlaveList)
                {
                    if (iec.getSlaveID == slaveID)
                    {
                        SMSList.Add(iec);
                        break;
                    }
                }

            return SMSList;
        }
    }
}
