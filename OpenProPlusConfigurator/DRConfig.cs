using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenProPlusConfigurator
{
    public class DRConfig
    {
        public enum DRType
        {
            Tag,
            Channel,
            None
        }

        private string _fun;
        private string _inf;
        private string _tagName;
        private string _acc;
        private string _channelName;
        private string _unit;
        private string _vori;
        private string _phase;
        private string _pors;
        private string _min;
        private string _max;

        public string fun
        {
            get { return _fun; }
            set { _fun = value; }
        }
        public string inf
        {
            get { return _inf; }
            set { _inf = value; }
        }
        public string tagName
        {
            get { return _tagName; }
            set { _tagName = value; }
        }
        public string acc
        {
            get { return _acc; }
            set { _acc = value; }
        }
        public string channelName
        {
            get { return _channelName; }
            set { _channelName = value; }
        }
        public string unit
        {
            get { return _unit; }
            set { _unit = value; }
        }
        public string vori
        {
            get { return _vori; }
            set { _vori = value; }
        }
        public string phase
        {
            get { return _phase; }
            set { _phase = value; }
        }
        public string pors
        {
            get { return _pors; }
            set { _pors = value; }
        }
        public string min
        {
            get { return _min; }
            set { _min = value; }
        }
        public string max
        {
            get { return _max; }
            set { _max = value; }
        }

        public void updateAttributes(List<KeyValuePair<string, string>> drData)
        {
            string strRoutineName = "updateAttributes";
            try
            {
                if (drData != null && drData.Count > 0)
                {
                    foreach (KeyValuePair<string, string> aikp in drData)
                    {
                        try
                        {
                            if (this.GetType().GetProperty(aikp.Key) != null) 
                            {
                                this.GetType().GetProperty(aikp.Key).SetValue(this, aikp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
