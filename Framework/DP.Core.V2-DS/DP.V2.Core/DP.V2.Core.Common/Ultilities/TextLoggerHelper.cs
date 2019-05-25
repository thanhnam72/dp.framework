using System;
using System.IO;
using System.Net;

namespace DP.V2.Core.Common.Ultilities
{
    /// <summary>
    /// For log error and information in file and in database.
    /// </summary>
    public static class TextLoggerHelper
    {
        #region " [ Properties ] "

        /// <summary>
        /// The string log file path.
        /// </summary>
        private static string strLogFilePath = string.Empty;

        /// <summary>
        /// The sw
        /// </summary>
        private static StreamWriter sw;

        /// <summary>
        /// Setting LogFile path. If the logfile path is null then it will update error info into LogFile.txt under
        /// application directory.
        /// </summary>
        /// <value>
        /// The log file path.
        /// </value>
        public static string LogFilePath
        {
            set
            {
                strLogFilePath = value;
                string pathWithoutFilename = Path.GetDirectoryName(strLogFilePath);
                if (!Directory.Exists(pathWithoutFilename))
                {
                    Directory.CreateDirectory(pathWithoutFilename);
                }
                if (!File.Exists(strLogFilePath))
                {
                    File.Create(strLogFilePath);
                }
            }
            get
            {
                return strLogFilePath;
            }
        }

        #endregion

        #region " [ Private function ] "

        /// <summary>
        /// Check the log file in applcation directory. If it is not available, creae it
        /// </summary>
        /// <returns>
        /// Log file path
        /// </returns>
        private static string GetLogFilePath(bool infoLog = true)
        {
            try
            {
                // get the base directory
                string _baseDir = AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.RelativeSearchPath;

                _baseDir = Path.Combine(_baseDir, "Logs");
                // search the file below the current directory
                if (!Directory.Exists(_baseDir))
                {
                    Directory.CreateDirectory(_baseDir);
                }
                string _retFilePath = "";
                if (infoLog)
                {
                    _retFilePath = Path.Combine(_baseDir, "InforLog.txt");
                }
                else
                {
                    _retFilePath = Path.Combine(_baseDir, "ErrorLog.txt");
                }

                // if exists, return the path
                if (File.Exists(_retFilePath))
                    return _retFilePath;
                //create a text file
                else
                {
                    if (!CheckDirectory(_retFilePath))
                        return string.Empty;

                    FileStream fs = new FileStream(_retFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    fs.Close();
                }

                return _retFilePath;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Create a directory if not exists
        /// </summary>
        /// <param name="strLogPath">The string log path.</param>
        /// <returns></returns>
        private static bool CheckDirectory(string strLogPath)
        {
            try
            {
                int _nFindSlashPos = strLogPath.Trim().LastIndexOf("\\", StringComparison.Ordinal);
                string _strDirectoryname = strLogPath.Trim().Substring(0, _nFindSlashPos);

                if (!Directory.Exists(_strDirectoryname))
                    Directory.CreateDirectory(_strDirectoryname);

                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }

        /// <summary>
        /// Writes the error log.
        /// </summary>
        /// <param name="strPathName">Name of the string path.</param>
        /// <param name="objException">The object exception.</param>
        /// <param name="additionalinfo">The additionalinfo.</param>
        /// <param name="isException">Exception or not.</param>
        /// <returns></returns>
        private static bool WriteErrorLog(string strPathName, Exception objException, string additionalinfo, bool isException)
        {
            bool _bReturn;

            try
            {
                IPAddress[] _ips = Dns.GetHostAddresses(Dns.GetHostName());

                sw = new StreamWriter(strPathName, true);
                if (isException)
                {
                    sw.WriteLine("Source		: " + objException.Source.Trim());
                    sw.WriteLine("Method		: " + objException.TargetSite.Name);
                    sw.WriteLine("Date		: " + DateTime.Now.ToShortDateString());
                    sw.WriteLine("Time		: " + DateTime.Now.ToLongTimeString());
                    sw.WriteLine("Error		: " + objException.Message.Trim());
                    sw.WriteLine("Stack Trace	: " + objException.StackTrace.Trim());
                    sw.WriteLine("^^-------------------------------------------------------------------^^");
                }
                else
                {
                    sw.WriteLine("Date		: " + DateTime.Now.ToShortDateString());
                    sw.WriteLine("Time		: " + DateTime.Now.ToLongTimeString());
                    sw.WriteLine("Computer	: " + Dns.GetHostName());
                    sw.WriteLine("Additional Info		: " + additionalinfo.Trim());
                    sw.WriteLine("^^-------------------------------------------------------------------^^");
                }
                sw.Flush();
                sw.Close();
                _bReturn = true;
            }
            catch (Exception)
            {
                _bReturn = false;
            }
            return _bReturn;
        }

        #endregion

        #region " [ Publish ] "

        /// <summary>
        /// This Is created in order to maintain Additional infomation in the seperate file path and for clear visibility
        /// </summary>
        /// <param name="logDescription">The log description.</param>
        /// <returns></returns>
        public static bool OutputLog(string logDescription)
        {
            string _strAddlogPathName;
            if (LogFilePath.Equals(string.Empty))
            {
                //Get Default log file path "LogFile.txt"
                _strAddlogPathName = GetLogFilePath(true);
            }
            else
            {
                //If the log file path is not empty but the file is not available it will create it
                if (!File.Exists(LogFilePath))
                {
                    if (!CheckDirectory(LogFilePath))
                        return false;

                    FileStream fs = new FileStream(LogFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    fs.Close();
                }
                _strAddlogPathName = LogFilePath;
            }

            Exception ex = new Exception();
            bool bReturn = WriteErrorLog(_strAddlogPathName, ex, logDescription, false);
            return bReturn;
        }

        /// <summary>
        /// This Is created in order to maintain Additional infomation in the seperate file path and for clear visibility
        /// </summary>
        /// <param name="AdditionInforDescription">The log description. [User define]</param>
        /// <param name="objException"> Exception object </param>
        /// <returns></returns>
        public static bool OutputLog(string AdditionInforDescription, Exception objException)
        {
            string _strAddlogPathName;
            if (LogFilePath.Equals(string.Empty))
            {
                //Get Default log file path "LogFile.txt"
                _strAddlogPathName = GetLogFilePath(false);
            }
            else
            {
                //If the log file path is not empty but the file is not available it will create it
                if (!File.Exists(LogFilePath))
                {
                    if (!CheckDirectory(LogFilePath))
                        return false;

                    FileStream fs = new FileStream(LogFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    fs.Close();
                }
                _strAddlogPathName = LogFilePath;
            }

            bool bReturn = WriteErrorLog(_strAddlogPathName, objException, AdditionInforDescription, false);
            return bReturn;
        }

        #endregion

    }
}
