//***********************************************************************************************************
//* Version     Updated     Action      Description
//* -------     -------     ------      ---------------------------------------------------------------------
//* 2.1         04/03/2015  Update      Updated
//* 2.2         11/06/2015  Update      Updated Error function
//* 2.3         03/08/2016  Update      Updated Switch LogJob to Boolean value
//*                                     Updated Switch LogTypeDebug to Boolean value

using System;
using System.IO;

namespace Log_Win
{
    class Log
    {
        private String strLogFolder = "";                   // Store the value of the log directory
        private Boolean boolLogJob;                         // Stores a Boolean Value *Updated 03/08/2016 Version 2.3
        private Boolean boolLogTypeDebug;                   // Stores a Boolean Value *Updated 03/08/2016 Version 2.3
        private String strLogFileExtention = "";
        private StreamWriter logfile = null;                // Object of output
        private string strLogName = "";                     // Store output file name

        public String logfileName = "";
        public String StrErrorMessage ="";                  // Return error message

        AutoForumReader.GetAppSettings appSettings = new AutoForumReader.GetAppSettings();

        /* *********************************************************************************************** 
         *  Main Process      
         */
        public void LogInit() 
        {
            StrErrorMessage = ReadConfigurationFile();      // Get configuration parameters
            if (StrErrorMessage.Length == 0)
                StrErrorMessage = Open();                   // Open SteamWriter object

        }   // end of method

#region Info
        /* *********************************************************************************************** 
         *  Write Informational message to log file
         *      
         *  input param:    message         text to be written to the log file
         *  return:         error message
         */

        public String Info(String message)
        {
            try
            {
                if (boolLogTypeDebug) // Update for modification 03/08/2016 Version 2.3
                {
                    logfile.WriteLine("INFO  " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + message);
                    logfile.Flush();
                }
                return "";
            }
            catch (Exception Ex)
            {
                throw new Exception("Log02 " + Ex.Message.ToString());
            }   // end of try/catch

        }   // end of method
#endregion

#region Error
        /* ********************************************************************************************** 
         *  Write Error message to log file
         *      
         *  input param:    message         text to be written to the log file
         *  return:         error message
         */

        public String Error(String message)
        {
            try
            {
                logfile.WriteLine("ERROR " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + message);   // added datetime to error  v2.2
//              logfile.WriteLine("ERROR " + message);                                                          // remove  v2.2
                logfile.Flush(); 
                return "";
            }
            catch (Exception Ex)
            {
                throw new Exception("Log03 " + Ex.Message.ToString());
            }   // end of try/catch

        }   // end of method
#endregion

#region RunTime
        /* ********************************************************************************************** 
         *  Write Run message to log file
         *      
         *  input param:    message         text to be written to the log file
         *  return:         error message
         */

        public String RunTime(String message)
        {
            try
            {
                logfile.WriteLine("RUN   " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + message);
                logfile.Flush();
                return "";
            }
            catch (Exception Ex)
            {
                throw new Exception("Log01 " + Ex.Message.ToString());
            }   // end of try/catch

        }   // end of method
#endregion 

#region DataList
        /* *********************************************************************************************** 
         *  Write Informational message to log file
         *      
         *  input param:    message         text to be written to the log file
         *  return:         none
         */

        public void DataList(String message)
        {
            try
            {
                if (boolLogTypeDebug) // Update for modification 03/08/2016 Version 2.3
                {
                    logfile.WriteLine(message);
                    logfile.Flush();
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("Log03 " + Ex.Message.ToString());
            }   // end of try/catch

        }   // end of method
#endregion

#region Close
        /* *********************************************************************************************** 
         *  Close the log file
         *      
         *  input param:    none
         *  return:         error message
         */

        public String Close()
        {
            try
            {
                logfile.WriteLine("RUN   " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + "Log is Closed");
                logfile.Flush();
                logfile.Close();
                return "";
            }
            catch (Exception Ex)
            {
                throw new Exception("Log04 " + Ex.Message.ToString());
            }   // end of try/catch

        }   // end of method
#endregion

#region Open
        /* *********************************************************************************************** 
         *  Opens and format log file name
         *      
         *  input param:    none
         *  return:         error message
         */

        private String Open()
        {
            try
            {
                string appdata_folder = strLogFolder;
                String ts;

                if (boolLogJob) // Update for modification 03/08/2016 Version 2.3
                    ts = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                else
                    ts = DateTime.Now.ToString("yyyyMMdd");

                logfileName = String.Format(@"{0}\{1}_{2}.{3}", appdata_folder, strLogName, ts, strLogFileExtention);
                logfile = File.AppendText(logfileName);
                return "";
            }
            catch (Exception Ex)
            {
                return ("Log05 " + Ex.Message.ToString());
            }   // end of try/catch
        
        }   // end of method
#endregion
        
#region Configuration File
        /* *********************************************************************************************** 
         *  Read the Configuration File
         *      
         *  input param:    none
         *  return:         error message
         */

        private String ReadConfigurationFile()
        {
            try
            {
                strLogFolder = appSettings.ServerLogLocation;
                strLogName = appSettings.ServerLogName;
                //Begin code update for modification 03/08/2016 Version 2.3
                boolLogJob = Boolean.Parse(appSettings.ServerLogJobSW);

                boolLogTypeDebug = Boolean.Parse(appSettings.ServerLogDebugTypeSW);
                //End code update for modification date 03/08/2016 Version 2.3
                strLogFileExtention = appSettings.ServerLogType;

                return "";
            }
            catch (Exception Ex)
            {
                return ("Log06 " + Ex.Message.ToString()); 
            }   // end of try/catch

        }   // end of method
#endregion
 
    }   // end of class
}   // end of namespace