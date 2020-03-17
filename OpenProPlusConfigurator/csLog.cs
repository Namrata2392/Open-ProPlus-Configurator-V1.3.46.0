using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

class csLog
{
    public static string PCName = System.Environment.MachineName ;
    public static string UserName = System.Environment.UserName;
    public static string AppName = Application.ProductName;
    public static string ExeName = Path.GetFileName(Application.ExecutablePath);
    public static string AppPath = Path.GetTempPath();// Application.StartupPath;
    public static string ProcessName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;  //Naina: 28/01/2013

    public static string LogFileName;
    public static string LogErrorFileName;
    private static DateTime LastLogTime = new DateTime();
    private static DateTime LastErrorLogTime = new DateTime();
    private static object m_LogSync = new object();

    //Naina: 25/01/2013
    public static string strLogFilePrefix = "LogHMI";
    public static string strErrorFilePrefix = "ErrorHMI";   

    #region LogData
    public static void LogData(string strData)
    {
        StreamWriter sw = null;
        bool boolNewFile = false;
        if ((LastLogTime != DateTime.MinValue) && (LastLogTime.TimeOfDay > DateTime.Now.TimeOfDay)) boolNewFile = true;
        //Naina: 25/01/2013
        //LogFileName = AppPath + "\\Log_" + DateTime.Now.DayOfWeek + ".log";       
        LogFileName = AppPath + "\\" + strLogFilePrefix + "_" + DateTime.Now.DayOfWeek + ".log";

        //Mutex mut = new Mutex();
        //mut.WaitOne();
        try
        {
            if (boolNewFile) sw = new StreamWriter(LogFileName); //Open File
            else
            {
                lock (m_LogSync)
                {
                    using (sw = File.AppendText(LogFileName))
                    {
                        if (strData != "") strData = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") + "; " + PCName + "; " + UserName + "; " + ExeName + "; " + strData;
                        sw.WriteLine(strData);
                        sw.Flush();
                    }
                }
            }
        }
        catch (IOException IO_Exc)
        {
            //MessageBox.Show(IO_Exc.Message, "LogData");   //Naina: 17/03/2011
            Debug.Print("LogData: " + IO_Exc.Message);
            LogError(strData);
            LogError(IO_Exc);   //Naina: 09/06/2011
        }
        catch (Exception Exc)
        {
            //csMisc.DisplayWarning(Exc.Message, "LogData");
            Debug.Print("LogData: " + Exc.Message);
            LogError(strData);
            LogError(Exc);
        }
        finally     //Close SW
        {
            if (sw != null)
            {
                // sw.Flush();
                sw.Close();
                sw.Dispose();
            }
            GC.Collect();
            LastLogTime = DateTime.Now;
        }
        //mut.ReleaseMutex();
    }
    #endregion

    #region LogError
    //Para[0] can be exception or string
    //public static void LogError(params object[] para)
    //{
    //    StreamWriter sw;
    //    bool boolNewFile = false;
    //    if (LastErrorLogTime != DateTime.MinValue)
    //        if (LastErrorLogTime.TimeOfDay > DateTime.Now.TimeOfDay) boolNewFile = true;

    //    //Naina: 25/01/2013
    //    //LogErrorFileName = AppPath + "\\Error_" + DateTime.Now.DayOfWeek + ".log";
    //    LogErrorFileName = AppPath + "\\" + strErrorFilePrefix + "_" + DateTime.Now.DayOfWeek + ".log";

    //    try
    //    {
    //        //Open File
    //        if (boolNewFile) sw = new StreamWriter(LogErrorFileName);
    //        else sw = File.AppendText(LogErrorFileName);

    //        try
    //        {
    //            //Naina: 22/03/2012
    //            //string strError = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff") + "; " + PCName + "; " + AppName + "-> ";
    //            string strError = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") + "; " + PCName + "; " + ExeName + "; ";

    //            if (para[0] is Exception)
    //            {
    //                //Log Error
    //                Exception Exc = (Exception)para[0];
    //                strError += Exc.Message.ToString();
    //                sw.WriteLine(strError);
    //                //Display Error
    //                if (para.Count() > 1)
    //                    if ((bool)para[1] == true)
    //                    {
    //                        MessageBox.Show("Whoops! Please contact the developers with the following"
    //                              + " information:\n\n" + Exc.Message + Exc.StackTrace,
    //                              "Fatal Error", System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Stop);
    //                    }
    //            }

    //            if (para[0] is string)
    //            {
    //                //Log Error
    //                strError += para[0].ToString();
    //                sw.WriteLine(strError);
    //                //Display Error
    //                if (para.Count() > 0)
    //                    if ((bool)para[1] == true)
    //                    { MessageBox.Show(strError); }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            MessageBox.Show("csLog: LogError: " + ex.Message);
    //        }
    //        finally
    //        {
    //            sw.Close();
    //            sw.Dispose();
    //            LastErrorLogTime = DateTime.Now; //Naina: 20/11/2011
    //            GC.Collect();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show("csLog: LogError: " + ex.Message);
    //    } 
    //}
    public static void LogError(params object[] para)
    {
        StreamWriter sw;
        bool boolNewFile = false;
        if (LastErrorLogTime != DateTime.MinValue)
            if (LastErrorLogTime.TimeOfDay > DateTime.Now.TimeOfDay) boolNewFile = true;

        //Naina: 25/01/2013
        //LogErrorFileName = AppPath + "\\Error_" + DateTime.Now.DayOfWeek + ".log";
        LogErrorFileName = AppPath + "\\" + strErrorFilePrefix + "_" + DateTime.Now.DayOfWeek + ".log";

        try
        {
            //Open File
            if (boolNewFile) sw = new StreamWriter(LogErrorFileName);
            else sw = File.AppendText(LogErrorFileName);

            try
            {
                //Naina: 22/03/2012
                //string strError = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff") + "; " + PCName + "; " + AppName + "-> ";
                string strError = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") + "; " + PCName + "; " + ExeName + "; ";

                if (para[0] is Exception)
                {
                    //Log Error
                    Exception Exc = (Exception)para[0];
                    strError += Exc.Message.ToString();
                    sw.WriteLine(strError);
                    //Display Error
                    if (para.Count() > 1)
                        if ((bool)para[1] == true)
                        {
                            MessageBox.Show("Whoops! Please contact the developers with the following"
                                  + " information:\n\n" + Exc.Message + Exc.StackTrace,
                                  "Fatal Error", System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                }

                if (para[0] is string)
                {
                    //Log Error
                    strError += para[0].ToString();
                    sw.WriteLine(strError);
                    //Display Error
                    if (para.Count() > 1)
                        if ((bool)para[1] == true)
                        { MessageBox.Show("t1 Para1:" + para[1].ToString()  + strError); }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("csLog: LogError1: " + para[0].ToString() + "; " + ex.Message);
            }
            finally
            {
                sw.Close();
                sw.Dispose();
                LastErrorLogTime = DateTime.Now; //Naina: 20/11/2011
                GC.Collect();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("csLog: LogError2: " + para[0].ToString() + "; " + ex.Message);
        }
    }

    public static void LogError1(params object[] para)
    {
        StreamWriter sw;
        bool boolNewFile = false;
        if (LastErrorLogTime != DateTime.MinValue)
            if (LastErrorLogTime.TimeOfDay > DateTime.Now.TimeOfDay) boolNewFile = true;

        //Naina: 25/01/2013
        //LogErrorFileName = AppPath + "\\Error_" + DateTime.Now.DayOfWeek + ".log";
        LogErrorFileName = AppPath + "\\" + strErrorFilePrefix + "_" + DateTime.Now.DayOfWeek + ".log";

        try
        {
            //Open File
            if (boolNewFile) sw = new StreamWriter(LogErrorFileName);
            else sw = File.AppendText(LogErrorFileName);

            try
            {
                //Naina: 22/03/2012
                //string strError = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff") + "; " + PCName + "; " + AppName + "-> ";
                string strError = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") + "; " + PCName + "; " + ExeName + "; ";

                if (para[0] is Exception)
                {
                    //Log Error
                    Exception Exc = (Exception)para[0];
                    strError += Exc.Message.ToString();
                    sw.WriteLine(strError);
                    //Display Error
                    if (para.Count() > 1)
                        if ((bool)para[1] == true)
                        {
                            MessageBox.Show("Whoops! Please contact the developers with the following"
                                  + " information:\n\n" + Exc.Message + Exc.StackTrace,
                                  "Fatal Error", System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                }

                if (para[0] is string)
                {
                    //Log Error
                    strError += para[0].ToString();
                    sw.WriteLine(strError);
                    //Display Error
                    if (para.Count() > 1)
                        if ((bool)para[1] == true)
                        { MessageBox.Show("t1 Para1:" + para[1].ToString() + strError); }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("csLog: LogError1: " + para[0].ToString() + "; " + ex.Message);
            }
            finally
            {
                sw.Close();
                sw.Dispose();
                LastErrorLogTime = DateTime.Now; //Naina: 20/11/2011
                GC.Collect();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("csLog: LogError2: " + para[0].ToString() + "; " + ex.Message);
        }
    }
    #endregion
}

class csMessageShow
{
    public static string PCName = System.Environment.MachineName;
    public static string UserName = System.Environment.UserName;
    public static string AppName =  Application.ProductName;
    public static string ExeName = Path.GetFileName(Application.ExecutablePath);
    public static string AppPath = Application.StartupPath;
    public static string ProcessName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;  //Naina: 28/01/2013

    public static string LogFileName = "";
    public static string LogErrorFileName = "";
    //private static DateTime LastLogTime = new DateTime();
    //private static DateTime LastErrorLogTime = new DateTime();
    public static void DisplayWarning(string strwarning, string strCaption, bool logmessage,MessageBoxIcon MessageType,MessageBoxButtons Messagebuttons)
    {
        switch (MessageType)
        {
            case MessageBoxIcon.Information:
                break;
            case MessageBoxIcon.Error:
                break;
            case MessageBoxIcon.Question:
                break;
            case MessageBoxIcon.Warning:
                break;
        }
    }
}