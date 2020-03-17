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
    public class DNPSlaveGroup
    {
        #region Declaration
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        enum protocolType
        {
            TCP,
            RTU,
            ASCII
        };
        string NetworkActive = "";
        string SerialActive = "";
        private string rnName = "DNP3SlaveGroup";
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        ucGroupDNPSlave ucDNPSlave = new ucGroupDNPSlave();
        private TreeNode DNPSlaveGroupTreeNode;
        public List<DNP3Settings> DNPSlaveList = new List<DNP3Settings>(); //Expose the list to others for slave mapping...
                                                                           //public List<DNP3UnsolResponse> DNPSlaveList = new List<DNP3UnsolResponse>(); //Expose the list to others for slave mapping...
        #endregion Declaration
        public DNPSlaveGroup()
        {
            string strRoutineName = "SMSSlaveGroup";
            try
            {
                ucDNPSlave.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                ucDNPSlave.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                ucDNPSlave.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucDNPSlave.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucDNPSlave.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                ucDNPSlave.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                ucDNPSlave.btnNextClick += new System.EventHandler(this.btnNext_Click);
                ucDNPSlave.btnLastClick += new System.EventHandler(this.btnLast_Click);
                ucDNPSlave.lvDNPSlaveItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvDNPSlave_ItemCheck);
                ucDNPSlave.lvDNPSlaveDoubleClick += new System.EventHandler(this.lvDNPSlave_DoubleClick);
                ucDNPSlave.cmbProtocolTypeSelectedIndexChanged += new System.EventHandler(this.cmbProtocolType_SelectedIndexChanged);
                ucDNPSlave.cmbLocalIPSelectedIndexChanged += new System.EventHandler(this.cmbLocalIP_SelectedIndexChanged);
                ucDNPSlave.cmbPortNoSelectedIndexChanged += new System.EventHandler(this.cmbPortNo_SelectedIndexChanged);
                ucDNPSlave.CmbPortNameSelectedIndexChanged += new System.EventHandler(this.CmbPortName_SelectedIndexChanged);
                addListHeaders();
                fillOptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public DNPSlaveGroup(TreeNode tn)
        {
            string strRoutineName = "DNPSlaveGroup";
            try
            {
                tn.Nodes.Clear();
                DNPSlaveGroupTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...
                //GDSlave.GDSlaveTreeNode = DNPSlaveGroupTreeNode;
                ucDNPSlave.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                ucDNPSlave.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                ucDNPSlave.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucDNPSlave.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucDNPSlave.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                ucDNPSlave.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                ucDNPSlave.btnNextClick += new System.EventHandler(this.btnNext_Click);
                ucDNPSlave.btnLastClick += new System.EventHandler(this.btnLast_Click);
                ucDNPSlave.lvDNPSlaveItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvDNPSlave_ItemCheck);
                ucDNPSlave.lvDNPSlaveDoubleClick += new System.EventHandler(this.lvDNPSlave_DoubleClick);
                ucDNPSlave.cmbProtocolTypeSelectedIndexChanged += new System.EventHandler(this.cmbProtocolType_SelectedIndexChanged);
                ucDNPSlave.cmbLocalIPSelectedIndexChanged += new System.EventHandler(this.cmbLocalIP_SelectedIndexChanged);
                ucDNPSlave.cmbPortNoSelectedIndexChanged += new System.EventHandler(this.cmbPortNo_SelectedIndexChanged);
                ucDNPSlave.CmbPortNameSelectedIndexChanged += new System.EventHandler(this.CmbPortName_SelectedIndexChanged);
                addListHeaders();
                fillOptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbProtocolType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "cmbProtocolType_SelectedIndexChanged";
            try
            {
                fillPortNos();
                FillPortName();
                fillEnable();
                if (ucDNPSlave.cmbProtocolType.GetItemText(ucDNPSlave.cmbProtocolType.SelectedItem).ToLower() == "tcp")
                {
                    //Namrata: 19/09/2017
                    ucDNPSlave.lblPN.Text = "Port No.";//ucmbs.lblPN.Text = "Network No.";
                    ucDNPSlave.txtTCPPort.Enabled = true;
                    ucDNPSlave.txtRemoteIP.Enabled = true;
                    ucDNPSlave.cmbPortNo.Enabled = false;
                    //Namrata: 21/09/2017
                    ucDNPSlave.cmbLocalIP.Enabled = true;
                    ucDNPSlave.CmbPortName.Enabled = true;
                    ucDNPSlave.txtSecRemoteIP.Enabled = true;
                }
                else
                {
                    ucDNPSlave.lblPN.Text = "UART No.";
                    ucDNPSlave.txtTCPPort.Enabled = true;//Namrata:06/03/2019; false;
                    ucDNPSlave.txtRemoteIP.Enabled = false;
                    //Namrata: 21/09/2017
                    ucDNPSlave.cmbLocalIP.Enabled = true;//Namrata:06/03/2019; false;
                    ucDNPSlave.cmbPortNo.Enabled = true;
                    ucDNPSlave.CmbPortName.Enabled = false;
                    ucDNPSlave.txtSecRemoteIP.Enabled = false;
                    //Namrata:06/03/2019
                    ucDNPSlave.txtSecRemoteIP.Text = "0.0.0.0";
                    ucDNPSlave.txtRemoteIP.Text = "0.0.0.0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbPortNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "DNPSlaveGroup: cmbPortNo_SelectedIndexChanged";
            try
            {
                //Namrata: 22/09/2017
                if (ucDNPSlave.cmbProtocolType.GetItemText(ucDNPSlave.cmbProtocolType.SelectedItem).ToLower() == "tcp")
                {
                    NetworkActive = "";
                    ucDNPSlave.cmbEnable.DataSource = Utils.dtNetworkConfig.Tables[0]; //ucDNPSlave.cmbLocalIP.SelectedIndex
                    ucDNPSlave.cmbEnable.DisplayMember = "Enable";
                    ucDNPSlave.cmbEnable.ValueMember = "PortNum";
                    ucDNPSlave.cmbEnable.SelectedIndex = ucDNPSlave.cmbPortNo.SelectedIndex;
                    NetworkActive = ucDNPSlave.cmbEnable.Text;
                    //ucDNPSlave.label2.Text = NetworkActive;
                    //Utils.MODBUSSlaveEnable = ucDNPSlave.label2.Text;
                }
                else if (ucDNPSlave.cmbProtocolType.GetItemText(ucDNPSlave.cmbProtocolType.SelectedItem).ToLower() == "rtu")
                {
                    SerialActive = "";
                    ucDNPSlave.cmbEnable.DataSource = Utils.dtSerialConfig.Tables[0];//ucDNPSlave.cmbLocalIP.SelectedIndex
                    ucDNPSlave.cmbEnable.DisplayMember = "Enable";
                    ucDNPSlave.cmbEnable.ValueMember = "PortNum";
                    ucDNPSlave.cmbEnable.SelectedIndex = ucDNPSlave.cmbPortNo.SelectedIndex;
                    SerialActive = ucDNPSlave.cmbEnable.Text;
                    //ucDNPSlave.label2.Text = SerialActive;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbPortName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "DNPSlaveGroup: CmbPortName_SelectedIndexChanged";
            try
            {
                foreach (NetworkInterface ni in Utils.getOpenProPlusHandle().getNetworkConfiguration().getNetworkInterfaces())
                {
                    if (ucDNPSlave.CmbPortName.Text == ni.PortName)
                    {
                        ucDNPSlave.cmbLocalIP.Text = ni.IP;
                        ucDNPSlave.cmbPortNo.Text = ni.PortNum;
                        if (ni.Enable.ToUpper() == "YES")
                        {
                            ucDNPSlave.PbOff.Visible = false;
                            ucDNPSlave.PbOn.Visible = true;
                            ucDNPSlave.PbOn.BringToFront();
                        }
                        else
                        {
                            ucDNPSlave.PbOn.Visible = false;
                            ucDNPSlave.PbOff.Visible = true;
                            ucDNPSlave.PbOff.BringToFront();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbLocalIP_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "DNPSlaveGroup: cmbLocalIP_SelectedIndexChanged";
            try
            {
                //Namrata: 21/09/2017
                if (ucDNPSlave.cmbProtocolType.GetItemText(ucDNPSlave.cmbProtocolType.SelectedItem).ToLower() == "tcp")
                {
                    ucDNPSlave.cmbPortNo.DataSource = Utils.dtNetworkConfig.Tables[0];
                    ucDNPSlave.cmbPortNo.DisplayMember = "PortNum";
                    ucDNPSlave.cmbPortNo.ValueMember = "IP";
                    ucDNPSlave.cmbPortNo.SelectedIndex = ucDNPSlave.cmbLocalIP.SelectedIndex;
                }
                //Namrata: 21/09/2017
                else
                {
                    ucDNPSlave.cmbPortNo.DataSource = Utils.dtNetworkConfig.Tables[0];
                    ucDNPSlave.cmbPortNo.DisplayMember = "PortNum";
                    ucDNPSlave.cmbPortNo.ValueMember = "IP";
                    ucDNPSlave.cmbPortNo.SelectedIndex = ucDNPSlave.cmbLocalIP.SelectedIndex;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void fillOptions()
        {
            string strRoutineName = "DNPSlaveGroup: fillOptions";
            try
            {
                ucDNPSlave.cmbFS.Items.Clear();
                //Fill Link Address Sizes
                ucDNPSlave.cmbFS.Items.Clear();
                foreach (String br in DNP3Settings.getFragmentsizes())
                {
                    ucDNPSlave.cmbFS.Items.Add(br.ToString());
                }
                ucDNPSlave.cmbFS.SelectedIndex = 0;


                ucDNPSlave.cmbProtocolType.Items.Clear();
                //Fill Protocol Type's...
                ucDNPSlave.cmbProtocolType.Items.Clear();
                foreach (String pt in DNP3Settings.getProtocolTypes())
                {
                    ucDNPSlave.cmbProtocolType.Items.Add(pt.ToString());
                }
                ucDNPSlave.cmbProtocolType.SelectedIndex = 0;

                #region  Fill Debug levels...
                ucDNPSlave.cmbDebug.Items.Clear();
                for (int i = 1; i <= Globals.MAX_DEBUG_LEVEL; i++)
                {
                    ucDNPSlave.cmbDebug.Items.Add(i.ToString());
                }
                ucDNPSlave.cmbDebug.SelectedIndex = 0;
                #endregion  Fill Debug levels...
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string strRoutineName = "DNPSlaveGroup: btnAdd_Click";
            try
            {
                if (DNPSlaveList.Count >= Globals.MaxDNPSlave)
                {
                    MessageBox.Show("Maximum " + Globals.MaxDNPSlave + " DNPSlave3.0 Slaves are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    Utils.resetValues(ucDNPSlave.grpDNPSlave);
                    Utils.showNavigation(ucDNPSlave.grpDNPSlave, false);
                    loadDefaults();
                    ucDNPSlave.txtSlaveNum.Text = (Globals.SlaveNo + 1).ToString();
                    ucDNPSlave.grpDNPSlave.Visible = true;
                    ucDNPSlave.txtSlaveNum.Focus();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strRoutineName = "DNPSlaveGroup: btnDelete_Click";
            try
            {
                if (ucDNPSlave.lvDNPSlave.Items.Count == 0)
                {
                    MessageBox.Show("Please add atleast one slave ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (ucDNPSlave.lvDNPSlave.CheckedItems.Count == 1)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete Slave No." + ucDNPSlave.lvDNPSlave.CheckedItems[0].Text + " ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        int iIndex = ucDNPSlave.lvDNPSlave.CheckedItems[0].Index;
                        if (result == DialogResult.Yes)
                        {
                            DNPSlaveGroupTreeNode.Nodes.Remove(DNPSlaveList.ElementAt(iIndex).getTreeNode());
                            DNPSlaveList.RemoveAt(iIndex);
                            ucDNPSlave.lvDNPSlave.Items[iIndex].Remove();
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
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            string strRoutineName = "DNPSlaveGroup: btnDone_Click";
            try
            { // Ajay: 23/11/2018
                if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
                {
                    if (ProtocolGateway.OppDNP3SlaveGroup_ReadOnly) { return; }
                    else { }
                }
                if (!Validate()) return;
                List<KeyValuePair<string, string>> mbsData = Utils.getKeyValueAttributes(ucDNPSlave.grpDNPSlave);
                if (mode == Mode.ADD)
                {
                    TreeNode tmp = null;
                    tmp = DNPSlaveGroupTreeNode.Nodes.Add("DNP3Slave_" + Utils.GenerateShortUniqueKey(), "DNP3Settings", "DNP3Settings", "DNP3Settings");
                    DNPSlaveList.Add(new DNP3Settings("DNP3Settings", mbsData,tmp,SlaveTypes.DNP3SLAVE, Int32.Parse(mbsData[23].Value)));

                    // DNPSlaveList.Add(new DNP3UnsolResponse("DNP3Slave", mbsData, tmp));
                    Globals.TotalSlavesCount += 1; //Ajay: 25/08/2018
                }
                else if (mode == Mode.EDIT)
                {
                    TreeNode tmp = null;
                    DNPSlaveList[editIndex].updateAttributes(mbsData);
                }
                refreshList();
                if (sender != null && e != null)
                {
                    ucDNPSlave.grpDNPSlave.Visible = false;
                    mode = Mode.NONE;
                    editIndex = -1;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            string strRoutineName = "DNPSlaveGroup: btnCancel_Click";
            try
            {
                //Utils.IntIEC104Modbus = Utils.IntIEC104Modbus - 1;
                ucDNPSlave.grpDNPSlave.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            string strRoutineName = "DNPSlaveGroup: btnFirst_Click";
            try
            {
                if (ucDNPSlave.lvDNPSlave.Items.Count <= 0) return;
                if (DNPSlaveList.ElementAt(0).IsNodeComment) return;
                editIndex = 0;
                loadValues();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            string strRoutineName = "DNPSlaveGroup: btnPrev_Click";
            try
            {
                //Namrata:27/7/2017
                btnDone_Click(null, null);
                if (editIndex - 1 < 0) return;
                if (DNPSlaveList.ElementAt(editIndex - 1).IsNodeComment) return;
                editIndex--;
                loadValues();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            string strRoutineName = "DNPSlaveGroup: btnNext_Click";
            try
            {
                btnDone_Click(null, null);
                if (editIndex + 1 >= ucDNPSlave.lvDNPSlave.Items.Count) return;
                if (DNPSlaveList.ElementAt(editIndex + 1).IsNodeComment) return;
                editIndex++;
                loadValues();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            string strRoutineName = "DNPSlaveGroup: btnLast_Click";
            try
            {
                if (ucDNPSlave.lvDNPSlave.Items.Count <= 0) return;
                if (DNPSlaveList.ElementAt(DNPSlaveList.Count - 1).IsNodeComment) return;
                editIndex = DNPSlaveList.Count - 1;
                loadValues();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void fillEnable()
        {
            string strRoutineName = "fillEnable";
            try
            {
                ucDNPSlave.cmbEnable.DataSource = null;
                ucDNPSlave.cmbEnable.Items.Clear();
                ucDNPSlave.cmbEnable.Items.Add("YES");
                ucDNPSlave.cmbEnable.Items.Add("NO");
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FillPortName()
        { //Namrata: 16/10/2017
            ucDNPSlave.CmbPortName.DataSource = null;
            ucDNPSlave.CmbPortName.Items.Clear();
            if (ucDNPSlave.cmbProtocolType.GetItemText(ucDNPSlave.cmbProtocolType.SelectedItem).ToLower() == "tcp")
            {
                foreach (NetworkInterface ni in Utils.getOpenProPlusHandle().getNetworkConfiguration().getNetworkInterfaces())
                {
                    if (ni.Enable == "YES")
                    {
                        ucDNPSlave.CmbPortName.Items.Add(ni.PortName);
                    }
                }
            }
            else
            {
                foreach (SerialInterface si in Utils.getOpenProPlusHandle().getSerialPortConfiguration().getSerialInterfaces())
                {
                    if (si.Enable == "YES")
                    {
                        ucDNPSlave.CmbPortName.Items.Add(si.PortName);
                    }
                }
            }
            foreach (NetworkInterface ni in Utils.getOpenProPlusHandle().getNetworkConfiguration().getNetworkInterfaces())
            {
                if (ucDNPSlave.CmbPortName.Text == ni.PortName)
                {
                    ucDNPSlave.cmbLocalIP.Text = ni.IP;
                    ucDNPSlave.cmbPortNo.Text = ni.PortNum;
                    if (ni.Enable.ToUpper() == "YES")
                    {
                        ucDNPSlave.PbOff.Visible = false;
                        ucDNPSlave.PbOn.Visible = true;
                        ucDNPSlave.PbOn.BringToFront();
                    }
                    else
                    {
                        ucDNPSlave.PbOn.Visible = false;
                        ucDNPSlave.PbOff.Visible = true;
                        ucDNPSlave.PbOff.BringToFront();
                    }
                }
            }
            if (ucDNPSlave.CmbPortName.Items.Count > 0) ucDNPSlave.CmbPortName.SelectedIndex = 0;
        }
        private void fillPortNos()
        {
            string strRoutineName = "fillPortNos";
            try
            {
                //Namrata: 16/10/2017
                ucDNPSlave.cmbPortNo.DataSource = null;
                ucDNPSlave.cmbPortNo.Items.Clear();
                if (ucDNPSlave.cmbProtocolType.GetItemText(ucDNPSlave.cmbProtocolType.SelectedItem).ToLower() == "tcp")
                {
                    foreach (NetworkInterface ni in Utils.getOpenProPlusHandle().getNetworkConfiguration().getNetworkInterfaces())
                    {
                        if (ni.Enable == "YES")
                        {
                            ucDNPSlave.cmbPortNo.Items.Add(ni.PortNum);
                        }
                    }
                }
                else//assume serial interfaces...
                {
                    foreach (SerialInterface si in Utils.getOpenProPlusHandle().getSerialPortConfiguration().getSerialInterfaces())
                    {
                        if (si.Enable == "YES")
                        {
                            ucDNPSlave.cmbPortNo.Items.Add(si.PortNum);
                        }
                        //ucDNPSlave.cmbPortNo.Items.Add(si.PortNum);
                    }
                }
                if (ucDNPSlave.cmbPortNo.Items.Count > 0) ucDNPSlave.cmbPortNo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvDNPSlave_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string strRoutineName = "DNPSlaveGroup:lvDNPSlave_ItemCheck";
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    ucDNPSlave.lvDNPSlave.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
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
        private void lvDNPSlave_DoubleClick(object sender, EventArgs e)
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

                if (ucDNPSlave.lvDNPSlave.SelectedItems.Count <= 0) return;
                ListViewItem lvi = ucDNPSlave.lvDNPSlave.SelectedItems[0];
                Utils.UncheckOthers(ucDNPSlave.lvDNPSlave, lvi.Index);
                if (DNPSlaveList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ucDNPSlave.grpDNPSlave.Visible = true;
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(ucDNPSlave.grpDNPSlave, true);
                loadValues();
                ucDNPSlave.txtSlaveNum.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadDefaults()
        {
            string strRoutineName = "DNPSlaveGroup:loadDefaults";
            try
            {
                ucDNPSlave.txtTCPPort.Text = "20000";
                ucDNPSlave.txtUnitID.Text = "1";
                ucDNPSlave.txtEvenyQSize.Text = "1001";
                ucDNPSlave.txtRemoteIP.Text = "0.0.0.0";
                ucDNPSlave.txtSecRemoteIP.Text = "0.0.0.0";
                //ucDNPSlave.txtlocalIP.Text = "0.0.0.0";
                ucDNPSlave.txtACTout.Text = "10";
                ucDNPSlave.txtCVP.Text = "10";
                ucDNPSlave.txtSelectTout.Text = "10";
                ucDNPSlave.txtDestAdd.Text = "1";
                ucDNPSlave.txtFirmwareVersion.Text = Globals.FIRMWARE_VERSION; 
                ucDNPSlave.cmbDebug.SelectedIndex = ucDNPSlave.cmbDebug.FindStringExact("3");
                ucDNPSlave.ChkbxUR.Checked = true;
                ucDNPSlave.cmbEnable.SelectedIndex = ucDNPSlave.cmbEnable.FindStringExact("NO");
                //ucDNPSlave.label2.Text = ucDNPSlave.cmbEnable.Text;
                if (ucDNPSlave.cmbLocalIP.Items.Count > 0) ucDNPSlave.cmbLocalIP.SelectedIndex = 0;
                foreach (NetworkInterface ni in Utils.getOpenProPlusHandle().getNetworkConfiguration().getNetworkInterfaces())
                {
                    if (ni.Enable == "YES")
                    {
                        ucDNPSlave.CmbPortName.Text = ni.PortName;
                        ucDNPSlave.cmbPortNo.Text = ni.PortNum;//Namrata:25/02/2019
                    }
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
            if (Utils.IsEmptyFields(ucDNPSlave.grpDNPSlave))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return status;
        }
        private void refreshList()
        {
            string strRoutineName = "DNPSlaveGroup:refreshList";
            try
            {
                int cnt = 0;
                //Utils.DNPSlaveList1.Clear();
                ucDNPSlave.lvDNPSlave.Items.Clear();
                foreach (DNP3Settings sl in DNPSlaveList)
                {
                    string[] row = new string[23];
                    if (sl.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = sl.SlaveNum;
                        row[1] = sl.ProtocolType;
                        row[2] = sl.PortNum;
                        row[3] = sl.TcpPort;
                        row[4] = sl.UnitID;
                        row[5] = sl.EventQSize;
                        row[6] = sl.RemoteIP;
                        row[7] = sl.SecRemoteIP;
                        row[8] = sl.LocalIP;
                        row[9] = sl.AppConfirmTimeout;
                        row[10] = sl.ClockValidatePeriod;
                        row[11] = sl.FragmentSize;
                        row[12] = sl.SelectTOut;
                        row[13] = sl.DestAdd;
                        row[14] = sl.MultiFragment;
                        row[15] = sl.Encryption;
                        row[16] = sl.DNPSA;
                        row[17] = sl.NeedTimeIIN;
                        row[18] = sl.NeedRestartIIN;
                        row[19] = sl.UnsolicitedResponse;
                        row[20] = sl.DEBUG;
                        row[21] = sl.AppFirmwareVersion;
                        row[22] = sl.Run;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucDNPSlave.lvDNPSlave.Items.Add(lvItem);

                    //Get SlaveNo
                    //GDSlave.GDSlaveNo = sl.SlaveNum;
                    //Utils.DNPSlaveList.AddRange(DNPSlaveList);//Namrata:09/04/2019
                    //Utils.DNPSlaveList1.AddRange(DNPSlaveList);//Namrata:09/04/2019
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        int a = 0;
        private void loadValues()
        {
            string strRoutineName = "DNPSlaveGroup:loadValues";
            try
            {
                DNP3Settings GDnpSlave = DNPSlaveList.ElementAt(editIndex);
                if (GDnpSlave != null)
                {
                    ucDNPSlave.txtSlaveNum.Text = GDnpSlave.SlaveNum;
                    //ucDNPSlave.CmbProtocolType.SelectedIndex = ucDNPSlave.CmbProtocolType.FindStringExact(GDnpSlave.ProtocolType);
                    ucDNPSlave.cmbPortNo.SelectedIndex = ucDNPSlave.cmbPortNo.FindStringExact(GDnpSlave.PortNum);
                    //ucDNPSlave.cmbPortNoSelectedIndexChanged += new System.EventHandler(this.cmbPortNo_SelectedIndexChanged);
                    //a = Convert.ToInt32((GDnpSlave.PortNum));
                    a = Convert.ToInt32((GDnpSlave.PortNum));
                    ucDNPSlave.cmbEnable.DataSource = Utils.dtNetworkConfig.Tables[0];//ucDNPSlave.cmbLocalIP.SelectedIndex
                    ucDNPSlave.cmbEnable.DisplayMember = "Enable";
                    ucDNPSlave.cmbEnable.ValueMember = "PortNum";
                    ucDNPSlave.txtTCPPort.Text = GDnpSlave.TcpPort;
                    ucDNPSlave.txtRemoteIP.Text = GDnpSlave.RemoteIP;
                    ucDNPSlave.txtSecRemoteIP.Text = GDnpSlave.SecRemoteIP;
                    ucDNPSlave.txtUnitID.Text = GDnpSlave.UnitID;
                    ucDNPSlave.txtEvenyQSize.Text = GDnpSlave.EventQSize;
                    ucDNPSlave.txtACTout.Text = GDnpSlave.AppConfirmTimeout;
                    ucDNPSlave.txtCVP.Text = GDnpSlave.ClockValidatePeriod;
                    ucDNPSlave.txtSelectTout.Text = GDnpSlave.EventQSize;
                    ucDNPSlave.cmbDebug.SelectedIndex = ucDNPSlave.cmbDebug.FindStringExact(GDnpSlave.DEBUG);
                    ucDNPSlave.cmbFS.SelectedIndex = ucDNPSlave.cmbFS.FindStringExact(GDnpSlave.FragmentSize);
                    ucDNPSlave.txtFirmwareVersion.Text = GDnpSlave.AppFirmwareVersion;
                    if (GDnpSlave.Run.ToLower() == "yes") ucDNPSlave.chkRun.Checked = true;
                    else ucDNPSlave.chkRun.Checked = false;

                    if (GDnpSlave.MultiFragment.ToLower() == "yes") ucDNPSlave.chkbxMFAllow.Checked = true;
                    else ucDNPSlave.chkbxMFAllow.Checked = false;

                    if (GDnpSlave.NeedTimeIIN.ToLower() == "yes") ucDNPSlave.ChkBxNeedTIIN.Checked = true;
                    else ucDNPSlave.ChkBxNeedTIIN.Checked = false;

                    if (GDnpSlave.NeedRestartIIN.ToLower() == "yes") ucDNPSlave.ChkBxNeedRIIN.Checked = true;
                    else ucDNPSlave.ChkBxNeedRIIN.Checked = false;

                    if (GDnpSlave.UnsolicitedResponse.ToLower() == "enable") ucDNPSlave.ChkbxUR.Checked = true;
                    else ucDNPSlave.ChkbxUR.Checked = false;

                    if (GDnpSlave.Encryption.ToLower() == "enable") ucDNPSlave.ChkbxEncryption.Checked = true;
                    else ucDNPSlave.ChkbxEncryption.Checked = false;

                    if (GDnpSlave.DNPSA.ToLower() == "enable") ucDNPSlave.chkBxDNPSA.Checked = true;
                    else ucDNPSlave.chkBxDNPSA.Checked = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void addListHeaders()
        {
            string strRoutineName = "DNPSlaveGroup:addListHeaders";
            try
            {
                ucDNPSlave.lvDNPSlave.Columns.Add("Slave No.", 70, HorizontalAlignment.Left);
                ucDNPSlave.lvDNPSlave.Columns.Add("ProtocolType", 70, HorizontalAlignment.Left);
                ucDNPSlave.lvDNPSlave.Columns.Add("PortNo", 70, HorizontalAlignment.Left);
                ucDNPSlave.lvDNPSlave.Columns.Add("TCPPort", 70, HorizontalAlignment.Left);
                ucDNPSlave.lvDNPSlave.Columns.Add("UnitID", 70, HorizontalAlignment.Left);
                ucDNPSlave.lvDNPSlave.Columns.Add("EventQSize", 70, HorizontalAlignment.Left);
                ucDNPSlave.lvDNPSlave.Columns.Add("RemoteIP", 70, HorizontalAlignment.Left);
                ucDNPSlave.lvDNPSlave.Columns.Add("SecRemoteIP", 70, HorizontalAlignment.Left);
                ucDNPSlave.lvDNPSlave.Columns.Add("LocalIP", 70, HorizontalAlignment.Left);
                ucDNPSlave.lvDNPSlave.Columns.Add("ApplnConfirmTOut", 80, HorizontalAlignment.Left);
                ucDNPSlave.lvDNPSlave.Columns.Add("ClockValidatePeriod", 120, HorizontalAlignment.Left);
                ucDNPSlave.lvDNPSlave.Columns.Add("FragmentSize", 120, HorizontalAlignment.Left);
                ucDNPSlave.lvDNPSlave.Columns.Add("SelectTOut", 120, HorizontalAlignment.Left);
                ucDNPSlave.lvDNPSlave.Columns.Add("DestAddress", 120, HorizontalAlignment.Left);
                ucDNPSlave.lvDNPSlave.Columns.Add("MultiFragmentAllow", 120, HorizontalAlignment.Left);
                ucDNPSlave.lvDNPSlave.Columns.Add("Encryption", 120, HorizontalAlignment.Left);
                ucDNPSlave.lvDNPSlave.Columns.Add("DNPSA", 120, HorizontalAlignment.Left);
                ucDNPSlave.lvDNPSlave.Columns.Add("NeedTimeIIN", 120, HorizontalAlignment.Left);
                ucDNPSlave.lvDNPSlave.Columns.Add("NeedRestartIIN", 120, HorizontalAlignment.Left);
                ucDNPSlave.lvDNPSlave.Columns.Add("UnsolicitedResponse", 120, HorizontalAlignment.Left);
                ucDNPSlave.lvDNPSlave.Columns.Add("Debug Level", 100, HorizontalAlignment.Left);
                ucDNPSlave.lvDNPSlave.Columns.Add("Firmware Version", 100, HorizontalAlignment.Left);//Namrata: 24/05/2019
                ucDNPSlave.lvDNPSlave.Columns.Add("Run", 70, HorizontalAlignment.Left);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        public Control getView(List<string> kpArr)
        {
            ucDNPSlave.cmbLocalIP.Items.Clear();
            ucDNPSlave.CmbPortName.Items.Clear();
            foreach (NetworkInterface ni in Utils.getOpenProPlusHandle().getNetworkConfiguration().getNetworkInterfaces())
            {
                if (ni.Enable == "YES")
                {
                    //Namrata:20/1/2018
                    ucDNPSlave.CmbPortName.Items.Add(ni.PortName);
                    if (ni.VirtualIP != "0.0.0.0")
                    {
                        ucDNPSlave.cmbLocalIP.Items.Add(ni.VirtualIP);
                    }
                    else
                    {
                        ucDNPSlave.cmbLocalIP.Items.Add(ni.IP);
                    }
                }
                //ucDNPSlave.cmbLocalIP.Items.Add(ni.IP);
                //ucDNPSlave.CmbPortName.Items.Add(ni.PortName);
            }
            if (ucDNPSlave.cmbLocalIP.Items.Count > 0) ucDNPSlave.cmbLocalIP.SelectedIndex = 0;
            //Fill Port No.
            fillPortNos();
            //Namrata: 14/11/2019
            FillPortName();
            fillEnable();


            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("DNP3SlaveGroup_"))return ucDNPSlave;
            kpArr.RemoveAt(0);

            if (kpArr.ElementAt(0).Contains("DNP3Slave_"))
            {
                int idx = -1;
                string[] elems = kpArr.ElementAt(0).Split('_');
                Utils.WriteLine(VerboseLevel.DEBUG, "$$$$ elem0: {0} elem1: {1}", elems[0], elems[elems.Length - 1]);
                idx = Int32.Parse(elems[elems.Length - 1]);
                if (DNPSlaveList.Count <= 0) return null;
                return DNPSlaveList[idx].getView(kpArr);
            }
            else
            {
                Utils.WriteLine(VerboseLevel.WARNING, "***** IEC103Master: View for element: {0} not supported!!!", kpArr.ElementAt(0));
            }
            return null;
        }
        public void parseIECGNode(XmlNode iecgNode, TreeNode tn)
        {
            string strRoutineName = "DNPSlaveGroup:parseIECGNode";
            try
            {
                rnName = iecgNode.Name;
                tn.Nodes.Clear();
                DNPSlaveGroupTreeNode = tn;
                foreach (XmlNode node in iecgNode)
                {
                    XmlElement root = node.ParentNode as XmlElement;
                    if (node.NodeType == XmlNodeType.Comment) continue;
                    //Namrata:25/02/2019
                    if (node.Attributes[1].Name == "ProtocolType")
                    {
                        if (node.Attributes[1].Value == "TCP")
                        {
                            if (node.Attributes[2].Name == "PortNum")
                            {
                                if (node.Attributes[2].Value == "-1")
                                {
                                    foreach (NetworkInterface ni in Utils.getOpenProPlusHandle().getNetworkConfiguration().getNetworkInterfaces())
                                    {
                                        if (ni.Enable == "YES")
                                        {
                                            node.Attributes[2].Value = ni.PortNum;
                                            ucDNPSlave.cmbPortNo.Text = ni.PortNum;//Namrata:25/02/2019
                                        }
                                    }
                                }
                                else { }
                            }
                        }
                    }
                    TreeNode tmp = tn.Nodes.Add("DNP3Slave_" + Utils.GenerateShortUniqueKey(), "DNP3Settings", "DNP3Settings", "DNP3Settings");
                    DNPSlaveList.Add(new DNP3Settings(node, tmp,SlaveTypes.DNP3SLAVE,Int32.Parse(node.Attributes[0].Value),false));

                 
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
            foreach (DNP3Settings sn in DNPSlaveList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(sn.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }
            return rootNode;
        }
        public int getCount()
        {
            int ctr = 0;
            foreach (DNP3Settings SMSNode in DNPSlaveList)
            {
                if (SMSNode.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }
        public List<DNP3Settings> getDNPSlaves()
        {
            return DNPSlaveList;
        }
    }
}
