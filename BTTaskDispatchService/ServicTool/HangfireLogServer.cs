using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTTaskDispatchService.ServicTool
{
    public class HangfireLogServer
    {
        public static void ExceptionLog(Exception e,string msg)
        {
            string logPath = BTServiceFactory.GetSectionValyue("LogFile");

            var sb = new StringBuilder();
            sb.Append("# Begin # ");
            sb.AppendFormat("Time:[{0}]\r\n", DateTime.Now.ToString("HH:mm:ss"));
            sb.AppendFormat("Message:{0}:{1}\r\n", e.Message, e.StackTrace + ((e.InnerException != null) ? e.InnerException.Message : ""));
            sb.Append($"# {msg} #\r\n");
            sb.Append("# Over #\r\n");
            WriteLog(logPath + @"/error/", string.Format("{0}.log", DateTime.Now.ToString("yyyy-M-d")), sb.ToString());
        }
        public static void SqlExceptionLog(Exception e)
        {
            string logPath = BTServiceFactory.GetSectionValyue("LogFile");
            var sb = new StringBuilder();
            sb.Append("# Begin # ");
            sb.AppendFormat("Time:[{0}]\r\n", DateTime.Now.ToString("HH:mm:ss"));
            sb.AppendFormat("Message:{0}:{1}\r\n", e.Message, e.StackTrace + ((e.InnerException != null) ? e.InnerException.Message : ""));
            sb.Append("# Over #\r\n");
            WriteLog(logPath + @"/sql/", string.Format("{0}.log", DateTime.Now.ToString("yyyy-M-d")), sb.ToString());
        }

        public static void WriteLog(string contentText, string logTypeName = "SysLogs")
        {
            string logPath = BTServiceFactory.GetSectionValyue("LogFile");
            var sb = new StringBuilder();
            sb.AppendFormat("Time:[{0}]\r\n", DateTime.Now.ToString("HH:mm:ss"));
            sb.AppendFormat("Message:{0}\r\n", contentText);
            string folder = string.IsNullOrWhiteSpace(logTypeName) ? "SysLogs" : logTypeName;
            WriteLog(logPath + @"/" + folder + @"/", string.Format("{0}.log", DateTime.Now.ToString("yyyy-M-d")), sb.ToString());
        }

        public static void WriteLog(string dirName, string fileName, string contentText)
        {
            if (!Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
            }
            string logName = dirName + fileName;

            FileStream logFile = null;
            StreamWriter streamWriter = null;

            try
            {
                logFile = new FileStream(logName, FileMode.OpenOrCreate);
                logFile.Seek(0, SeekOrigin.End);
                streamWriter = new StreamWriter(logFile, Encoding.GetEncoding("gb2312"));
                streamWriter.WriteLine(contentText);
            }
            catch (Exception)
            {

            }
            finally
            {
                if (streamWriter != null) streamWriter.Close();
                if (logFile != null) logFile.Close();
            }
        }
    }
}
