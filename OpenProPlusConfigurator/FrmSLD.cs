using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenProPlusConfigurator
{
    public partial class FrmSLD : Form
    {
        public FrmSLD()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        private void FrmSLD_Load(object sender, EventArgs e)
        {
            
        }
        public DataTable RemoveDuplicateRows(DataTable dTable, string colName)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();
            foreach (DataRow drow in dTable.Rows)
            {
                if (hTable.Contains(drow[colName]))
                    duplicateList.Add(drow);
                else
                    hTable.Add(drow[colName], string.Empty);
            }
            foreach (DataRow dRow in duplicateList)
                dTable.Rows.Remove(dRow);
            return dTable;
        }
        private void Btn_Save_Click(object sender, EventArgs e)
        {
        }

        private void ChkGridLines_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void Pb18_Click(object sender, EventArgs e)
        {

        }
    }
}