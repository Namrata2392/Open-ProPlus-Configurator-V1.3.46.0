using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using Bonjour;
using System.Reflection;
using System.Net.NetworkInformation;

namespace OpenProPlusConfigurator
{
    public partial class frmBonjour : Form
    {
        string mac = "";
        string macHost = "";
        string[] row;
        DataTable dtMain = new DataTable();
        string strRoutine = "";


        public frmBonjour()
        {
            InitializeComponent();
            dtMain.Columns.Add("Host Name");
            dtMain.Columns.Add("MAC Address");
            dtMain.Columns.Add("IP");
            //dtMain.Columns.Add("IP");
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            strRoutine = this.Name + "; " + MethodBase.GetCurrentMethod().Name + "; ";
            try
            {
                ReadService();
            }
            catch (Exception ex)
            {
                csLog.LogError(strRoutine + ex.Message);
            }
           
        }

        public void DisplayDnsAddresses()
        {
            strRoutine = this.Name + "; " + MethodBase.GetCurrentMethod().Name + "; ";
            try
            {
                System.Net.NetworkInformation.NetworkInterface[] adapters = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
                foreach (System.Net.NetworkInformation.NetworkInterface adapter in adapters)
                {

                    IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
                    IPAddressCollection dnsServers = adapterProperties.DnsAddresses;

                    if (dnsServers.Count > 0)
                    {
                        Console.WriteLine(adapter.Description);
                        foreach (System.Net.IPAddress dns in dnsServers)
                        {
                            Console.WriteLine("  DNS Servers ............................. : {0}",
                                dns.ToString());
                            //textBox1.Text = textBox1.Text + Environment.NewLine +
                            //    dns.ToString();
                        }
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                csLog.LogError(strRoutine + ex.Message);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            strRoutine = this.Name + "; " + MethodBase.GetCurrentMethod().Name + "; ";
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ReadService();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                csLog.LogError(strRoutine + ex.Message);
            }
        }

        void ReadService()
        {
            strRoutine = this.Name + "; " + MethodBase.GetCurrentMethod().Name + "; ";
            try
            {
                dataGridView1.DataSource = null;
                //Sharvari:15/03/2019
                dataGridView1.Rows.Clear();
                dataGridView1.ColumnCount = 4;

                dataGridView1.Columns[0].Name = "MAC";
                dataGridView1.Columns[1].Name = "MACHost";

                dataGridView1.Columns[2].Name = "Host Name";
                dataGridView1.Columns[3].Name = "IP";
                DNSSDService service = new DNSSDService();
                DNSSDEventManager eventManager = new DNSSDEventManager();
                //**********ServiceFound************//
                eventManager.ServiceFound += new _IDNSSDEvents_ServiceFoundEventHandler(eventManager_ServiceFound);
                //   DNSSDService browse = service.Browse(0, 0, "_axis-video._tcp", null, eventManager);
                //DNSSDService browser = service.Browse(0, 0, "_http._tcp", null, eventManager);
                DNSSDService browser1 = service.Browse(0, 0, "_Workstation._tcp", "local.", eventManager);
                DNSSDRecord records = new DNSSDRecord();
                //Application.EnableVisualStyles();
                //Application.SetCompatibleTextRenderingDefault(false);
                //Application.Run(new frmBonjour());
            }
            catch (Exception ex)
            {
                csLog.LogError(strRoutine + ex.Message);
            }
        }

        void eventManager_ServiceFound(DNSSDService browser1, DNSSDFlags flags, uint ifIndex, string serviceName, string regtype, string domain)
        {
            strRoutine = this.Name + "; " + MethodBase.GetCurrentMethod().Name + "; ";
            try
            {
                Console.WriteLine("browser: " + browser1 + "\nDNSSDFlags " + flags + "\nifIndex " + ifIndex + "\nserviceName: " + serviceName + "\nregtype: " + regtype + "\ndomain: " + domain);
                mac = serviceName;
                String St = serviceName;

                int pFrom = St.IndexOf("[") + "[".Length;
                int pTo = St.LastIndexOf("]");

                int pFrom1 = St.IndexOf("") + "".Length;
                int pTo1 = St.LastIndexOf("[");

                String result = St.Substring(pFrom, pTo - pFrom);
                String result1 = St.Substring(pFrom1, pTo1 - pFrom1);
                mac = result;
                macHost = result1;
                row = new string[] { mac, macHost, "-", "-" };
                dataGridView1.Rows.Add(row);

                DNSSDEventManager eventManager = new DNSSDEventManager();
                //**********ServiceResolved************//
                eventManager.ServiceResolved += new _IDNSSDEvents_ServiceResolvedEventHandler(EventManager_ServiceResolved);

                //eventManager.AddressFound += new _IDNSSDEvents_AddressFoundEventHandler(eventManager_AddressFound);
                browser1.Resolve(flags, ifIndex, serviceName, regtype, domain, eventManager);
                DNSSDAddressFamily family = new DNSSDAddressFamily();
                browser1.GetAddrInfo(flags, ifIndex, family, "", eventManager);
            }
            catch (Exception ex)
            {
                csLog.LogError(strRoutine + ex.Message);
            }
            //var ipAddress = IPMacMapper.FindIPFromMacAddress("mac-address");
        }

        private void EventManager_ServiceResolved(DNSSDService service, DNSSDFlags flags, uint ifIndex, string fullname, string hostname, ushort port, TXTRecord record)
        {
            try
            {
                strRoutine = this.Name + "; " + MethodBase.GetCurrentMethod().Name + "; ";
                //throw new NotImplementedException();
                Console.WriteLine("-------------------eventManager_ServiceResolved---------------------");
                Console.WriteLine("DNSSDService " + service + "\nDNSSDFlags " + flags + "\nifindex " + ifIndex + "\nfullname " + fullname + "hostname " + hostname + "\nport " + port + "\nrecord " + record);
                //var str = System.Text.Encoding.Default.GetString(record.GetValueForKey("macaddress"));
                //Console.WriteLine("mac " + str);
                Console.WriteLine("----------------------------------------");
                DNSSDEventManager eventManager = new DNSSDEventManager();
                //**********AddressFound************//
                eventManager.AddressFound += new _IDNSSDEvents_AddressFoundEventHandler(eventManager_AddressFound);
                DNSSDAddressFamily family = new DNSSDAddressFamily();
                service.GetAddrInfo(flags, ifIndex, family, hostname, eventManager);
            }
            catch (Exception ex)
            {
                csLog.LogError(strRoutine + ex.Message);
            }

        }

        private void eventManager_AddressFound(DNSSDService service, DNSSDFlags flags, uint ifIndex, string hostname, DNSSDAddressFamily addressFamily, string address, uint ttl)
        {
            try
            {
                strRoutine = this.Name + "; " + MethodBase.GetCurrentMethod().Name + "; ";
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("FOUND ADDRESS");
                Console.WriteLine("----------------------------------------");
                int pFrom1 = hostname.IndexOf("") + "".Length;
                int pTo1 = hostname.LastIndexOf(".l");
                String result1 = hostname.Substring(pFrom1, pTo1 - pFrom1);
                row = new string[] { "-", hostname, result1, address };
                dataGridView1.Rows.Add(row);
            }
            catch (Exception ex)
            {
                csLog.LogError(strRoutine + ex.Message);
            }
        }

        private void ProcessGridview(int row, int col1, int col2)
        { }

        private void button1_Click(object sender, EventArgs e)
        {
            strRoutine = this.Name + "; " + MethodBase.GetCurrentMethod().Name + "; ";
            try
            {
                dtMain.Clear();//Sharvari:15/03/2019
                List<string> CommanClm = new List<string>(); /* ... */
                dataGridView1.Rows.OfType<DataGridViewRow>().ToList().ForEach(Rw =>
                {
                    string strIP = "";
                    List<string> IPList = new List<string>();
                    string strHost = Rw.Cells["MACHost"].FormattedValue.ToString();
                    string strMAC = Rw.Cells["MAC"].FormattedValue.ToString();
                    dataGridView1.Rows.OfType<DataGridViewRow>().
                    Where(r => r.Cells["Host Name"].FormattedValue.ToString() != "-").ToList().
                    ForEach(r =>
                    {
                        if (r.Cells["Host Name"].FormattedValue.ToString().TrimEnd() == strHost.TrimEnd())
                        {
                            strIP = r.Cells["IP"].FormattedValue.ToString();
                            IPList.Add(strIP);
                            //dtMain.Rows.Add(strHost, strMAC, string.Join(", ",IPList));
                            dtMain.Rows.Add(strHost, strMAC, strIP);
                        }
                    });

                });
                dgvOpenProBrowser.DataSource = dtMain;
            }
            catch (Exception ex)
            {
                csLog.LogError(strRoutine + ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            strRoutine = this.Name + "; " + MethodBase.GetCurrentMethod().Name + "; ";
            try
            {
                dataGridView1.AllowUserToAddRows = false;
                button1_Click(sender, e);
                timer1.Enabled = false;
            }
            catch (Exception ex)
            {
                csLog.LogError(strRoutine + ex.Message);
            }
        }


        private void dgvOpenProBrowser_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            strRoutine = this.Name + "; " + MethodBase.GetCurrentMethod().Name + "; ";
            try
            {
                if (e.ColumnIndex == 2)
                {
                    System.Diagnostics.Process.Start("http://" + dgvOpenProBrowser[e.ColumnIndex, e.RowIndex].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                csLog.LogError(strRoutine + ex.Message);
            }
        }

        private void dgvOpenProBrowser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Btn_Refresh_Click(object sender, EventArgs e)
        {
            //Sharvari:15/03/2019
            dtMain.Clear();
            dataGridView1.DataSource = null;
            dgvOpenProBrowser.DataSource = null;
            DNSSDService service = new DNSSDService();
            ReadService();
            timer1.Enabled = true;
        }
    }
}
