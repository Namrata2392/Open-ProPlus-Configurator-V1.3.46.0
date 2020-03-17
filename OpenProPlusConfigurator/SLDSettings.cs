using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace OpenProPlusConfigurator
{
    public class SLDSettings
    {
        enum SLDSettingsType
        {
            SLDSettings
        };
        private SLDSettingsType SLDType = SLDSettingsType.SLDSettings;
        private bool isNodeComment = false;
        private string comment = "";
        private string templateSLDFile = "";
      
        public static string[] arrAttributes = {"TemplateSLDFile"};

        public SLDSettings(string smsUserName, List<KeyValuePair<string, string>> SLDSettingsData, TreeNode tn, int mNo)
        {
            string strRoutineName = "SLDSettings";
            try
            {
                try
                {
                    SLDType = (SLDSettingsType)Enum.Parse(typeof(SLDSettingsType), smsUserName);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", smsUserName);
                }
                if (SLDSettingsData != null && SLDSettingsData.Count > 0)
                {
                    foreach (KeyValuePair<string, string> lpfilekp in SLDSettingsData)
                    {
                        try
                        {
                            if (this.GetType().GetProperty(lpfilekp.Key) != null)
                            {
                                this.GetType().GetProperty(lpfilekp.Key).SetValue(this, lpfilekp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "LPFile: Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", lpfilekp.Key, lpfilekp.Value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public SLDSettings(XmlNode sNode, TreeNode tn)
        {
            string strRoutineName = "SLDSettings";
            try
            {
                if (sNode.Attributes != null)
                {
                    try
                    {
                        SLDType = (SLDSettingsType)Enum.Parse(typeof(SLDSettingsType), sNode.Name);
                    }
                    catch (System.ArgumentException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", sNode.Name);
                    }
                    foreach (XmlAttribute item in sNode.Attributes)
                    {
                        try
                        {
                            if (this.GetType().GetProperty(item.Name) != null) //Ajay: 03/07/2018
                            {
                                this.GetType().GetProperty(item.Name).SetValue(this, item.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value:{1}", item.Name, item.Value);
                        }
                    }
                }
                else if (sNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = sNode.Value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void updateAttributes(List<KeyValuePair<string, string>> sSportData)
        {
            string strRoutineName = "SLDSettings:updateAttributes";
            try
            {
                if (sSportData != null && sSportData.Count > 0)
                {
                    foreach (KeyValuePair<string, string> sSportkp in sSportData)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", sSportkp.Key, sSportkp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(sSportkp.Key) != null)
                            {
                                this.GetType().GetProperty(sSportkp.Key).SetValue(this, sSportkp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value:{1}", sSportkp.Key, sSportkp.Value);
                        }
                    }
                    Utils.Write(VerboseLevel.DEBUG, "\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //Not Used: uciec.lblMain.Text += slaveNum;
        }
        public Control getView(List<string> kpArr)
        {
            return null;
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
            rootNode = xmlDoc.CreateElement(SLDType.ToString());
            xmlDoc.AppendChild(rootNode);
            foreach (string attr in arrAttributes)
            {
                XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                rootNode.Attributes.Append(attrName);
            }
            return rootNode;
        }
        public string TemplateSLDFile
        {
            get { return templateSLDFile; }
            set { templateSLDFile = value; }
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
    }
}
