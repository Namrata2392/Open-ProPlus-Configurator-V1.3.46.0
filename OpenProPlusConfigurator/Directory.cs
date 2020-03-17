using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using OpenProPlusConfigurator.Properties;
using System.Globalization;
using System.Collections;
using System.Drawing;
using System.Security.AccessControl;

namespace OpenProPlusConfigurator
{
    public class Directory
    {
        public static void CreateDirectoryForIEC61850C(string SafeFileName, string Filename)
        {
            string strRoutineName = "Directory: CreateDirectoryForIEC61850C";
            try
            {
                ICDFilesData.IEC61850CICDFileLoc = System.IO.Path.GetTempPath() + "protocol" + @"\" + "IEC61850Client";
                if (!System.IO.Directory.Exists(ICDFilesData.IEC61850CICDFileLoc))
                {
                    System.IO.Directory.CreateDirectory(ICDFilesData.IEC61850CICDFileLoc);
                    if (!FileExists(ICDFilesData.IEC61850CICDFileLoc + @"\" + SafeFileName))
                    {
                        File.Copy(Filename, ICDFilesData.IEC61850CICDFileLoc + @"\" + SafeFileName);
                    }
                    else { }
                }
                else
                {
                    if (!FileExists(ICDFilesData.IEC61850CICDFileLoc + @"\" + SafeFileName))
                    {
                        File.Copy(Filename, ICDFilesData.IEC61850CICDFileLoc + @"\" + SafeFileName);
                    }
                    else { }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void CreateDirForIEC61850Server(string SafeFileName, string Filename)
        {
            string strRoutineName = "Directory: CreateDirForIEC61850Server";
            try
            {
                //GDSlave.GDSlaveXlsFiles = System.IO.Path.GetTempPath() + "protocol" + @"\" + "GUI" + @"\" + GDSlave.SlaveFolder;
                ICDFilesData.IEC61850SICDFileLoc = System.IO.Path.GetTempPath() + "protocol" + @"\" + "IEC61850Server" + @"\" + Utils.Iec61850SSlaveFolder;// Utils.Iec61850SSlaveNo 
                if (!System.IO.Directory.Exists(ICDFilesData.IEC61850SICDFileLoc))
                {
                    System.IO.Directory.CreateDirectory(ICDFilesData.IEC61850SICDFileLoc);
                    if (!FileExists(ICDFilesData.IEC61850SICDFileLoc + @"\" + SafeFileName))
                    {
                        File.Copy(Filename, ICDFilesData.IEC61850SICDFileLoc + @"\" + SafeFileName);
                    }
                    else { }
                }
                else
                {
                    if (!FileExists(ICDFilesData.IEC61850SICDFileLoc + @"\" + SafeFileName))
                    {
                        File.Copy(Filename, ICDFilesData.IEC61850SICDFileLoc + @"\" + SafeFileName);
                    }
                    else { }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static bool FileExists(string fileName)
        {
            string strRoutineName = "Directory: FileExists";
            try
            {
                return GetFileSearchPaths(fileName).Any(File.Exists);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }
        //Check if File Exist in Directory
        public static IEnumerable<string> GetFileSearchPaths(string fileName)
        {
            yield return fileName;
            yield return Path.Combine(System.IO.Directory.GetParent(Path.GetDirectoryName(fileName)).FullName, Path.GetFileName(fileName));
        }
        public static void DeleteDirectory(string DirectoryName, string Filename)
        {
            string strRoutineName = "Directory: DeleteDirectory";
            try
            {
                if (File.Exists(Path.Combine(DirectoryName, Filename)))
                {
                    File.Delete(Path.Combine(DirectoryName, Filename));// If file found, delete it    
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void CreateDirectoryForGDisplay(string FileName, string FilePath)
        {
            string strRoutineName = "Directory: CreateDirectoryForGraphicalDisplay";
            try
            {
                GDSlave.GDSlaveXlsFiles = System.IO.Path.GetTempPath() + "protocol" + @"\" + "GUI" + @"\" + GDSlave.SlaveFolder;
                if (!System.IO.Directory.Exists(GDSlave.GDSlaveXlsFiles))
                { System.IO.Directory.CreateDirectory(GDSlave.GDSlaveXlsFiles); }
                else { }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Create Diretory for Graphical Display
        public static void CreateDirectoryForGraphicalDisplay(string FileName, string FilePath)
        {
            string strRoutineName = "Directory: CreateDirectoryForGraphicalDisplay";
            try
            {
                GDSlave.GDSlaveXlsFiles = System.IO.Path.GetTempPath() + "protocol" + @"\" + "GUI" + @"\"+ GDSlave.SlaveFolder;
                //GDSlave.GDSlaveXlsFiles = System.IO.Path.GetTempPath() + "Protocol" + @"\" + "SLD";
                if (!System.IO.Directory.Exists(GDSlave.GDSlaveXlsFiles))
                {
                    System.IO.Directory.CreateDirectory(GDSlave.GDSlaveXlsFiles);
                    if (!FileExists(GDSlave.GDSlaveXlsFiles + @"\" + FileName))
                    {
                        File.Copy(FilePath, GDSlave.GDSlaveXlsFiles + @"\" + FileName);
                    }
                    else { }
                }
                else
                {
                    if (!FileExists(GDSlave.GDSlaveXlsFiles + @"\" + FileName))
                    {
                        File.Copy(FilePath, GDSlave.GDSlaveXlsFiles + @"\" + FileName);
                    }
                    else { }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Create Directory To Save Widgets
        public static void CreateDirectoryForWidgets(string FileName, string FilePath)
        {
            string strRoutineName = "Directory: CreateDirectoryForWidgets";
            try
            {
                string FName = Path.GetFileNameWithoutExtension(FileName);
                GDSlave.WidgetsPath = System.IO.Path.GetTempPath() + "protocol" + @"\" + "GUI" + @"\"+ GDSlave.SlaveFolder+@"\"+ "widget";
                if (!System.IO.Directory.Exists(GDSlave.WidgetsPath))
                {
                    System.IO.Directory.CreateDirectory(GDSlave.WidgetsPath);
                    if (!FileExists(GDSlave.WidgetsPath + @"\" + FileName))
                    {
                        File.Copy(FilePath, GDSlave.WidgetsPath + @"\" + FileName);
                    }
                    else { }
                }
                else
                {
                    if (!FileExists(GDSlave.WidgetsPath + @"\" + FileName))
                    {
                        File.Copy(FilePath, GDSlave.WidgetsPath + @"\" + FileName);
                    }
                    else { }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void CreateDirForWidgets(string FileName, string FilePath)
        {
            string strRoutineName = "Directory: CreateDirForWidgets";
            try
            {
                string FName = Path.GetFileNameWithoutExtension(FileName);
                GDSlave.WidgetsPath = System.IO.Path.GetTempPath() + "protocol" + @"\" + "GUI" + @"\" + GDSlave.SlaveFolder + @"\" + "widget";
                if (!System.IO.Directory.Exists(GDSlave.WidgetsPath))
                {
                    System.IO.Directory.CreateDirectory(GDSlave.WidgetsPath);
                    if (!FileExists(GDSlave.WidgetsPath + @"\" + FileName))
                    {
                        //File.Copy(FilePath, GDSlave.WidgetsPath + @"\" + FileName);
                    }
                    else { }
                }
                else
                {
                    if (!FileExists(GDSlave.WidgetsPath + @"\" + FileName))
                    {
                        File.Copy(FilePath, GDSlave.WidgetsPath + @"\" + FileName);
                    }
                    else { }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        internal static DirectorySecurity GetAccessControl(string v)
        {
            throw new NotImplementedException();
        }
    }
}
