using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using LogManagement.Models;
using Newtonsoft.Json;

namespace LogManagement.Core
{
    public class FileLogManager : ILogManager
    {
        public string ExecutingAssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);//or replace u path like "D:"
        public void LogApplicationCalls(EventEntry eventLogEntry)
        {
            eventLogEntry.MachineName = Dns.GetHostName();
            WriteLog(eventLogEntry, "log.txt");
        }

        public void LogExceptions(ExceptionEntry exception)
        {
            exception.MachineName = Dns.GetHostName();
            exception.IpAddress = GetIpAddress();
            exception.DateTime = DateTime.Now;
            WriteException(exception, "exception.txt");
            
        }

        private void WriteLog(EventEntry eventEntry, string fileType)
        {
            try
            {
                eventEntry.IsDebug = IsDebug();
                using (var txtWriter = File.AppendText(ExecutingAssemblyPath + "\\" + fileType))
                {
                    txtWriter.WriteLine("==================== {0} ====================\n", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    var result = JsonConvert.SerializeObject(eventEntry);
                    result = result.Replace("{", "").Replace("}", "").Replace(",", " =|= \n");
                    txtWriter.WriteLine("{0}", result);
                    txtWriter.WriteLine("=============================================================\n\n");
                }
            }
            catch (Exception ex)
            {
                WriteException(new ExceptionEntry { Exception =CreateExceptionString( ex) }, "exception.txt");
            }
        }
        private void WriteException(ExceptionEntry exceptionEntry, string fileType)
        {
            try
            {
                exceptionEntry.IsDebug = IsDebug();
                using (var txtWriter = File.AppendText(ExecutingAssemblyPath + "\\" + fileType))
                {
                    txtWriter.WriteLine("==================== {0} ====================\n", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    var result = JsonConvert.SerializeObject(exceptionEntry);
                    result = result.Replace("{", "").Replace("}", "").Replace(",", " =|= \n");
                    txtWriter.WriteLine("{0}", result);
                    txtWriter.WriteLine("=============================================================\n\n");
                }
            }
            catch (Exception ex)
            {
                WriteException(new ExceptionEntry { Exception = ex.ToString() }, "exception.txt");
            }
        }

        private static string GetIpAddress()
        {
            try
            {
                var externalIP = "";
                externalIP = new WebClient().DownloadString("http://checkip.dyndns.org/");
                externalIP = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}"))
                                               .Matches(externalIP)[0].ToString();
                return externalIP;
            }
            catch (Exception ex) { return "127.0.0.1"; }
        }
         
        public static bool IsDebug()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var attributes = assembly.GetCustomAttributes(typeof(DebuggableAttribute), true);
            if (attributes == null || attributes.Length == 0)
                return true;

            var d = (DebuggableAttribute)attributes[0];
            return d.IsJITTrackingEnabled;
        }

        public   string CreateExceptionString(Exception e)
        {
            var sb = new StringBuilder();
            CreateExceptionString(sb, e, String.Empty);
            return sb.ToString();
        }

        private static void CreateExceptionString(StringBuilder sb, Exception e, string indent)
        {
            switch (indent)
            {
                case null:
                    indent = String.Empty;
                    break;
                default:
                    if (indent.Length > 0)
                    {
                        sb.AppendFormat("{0}Inner ", indent);
                    }
                    break;
            }

            sb.AppendFormat("Exception Found:\n{0}Type: {1}", indent, e.GetType().FullName);
            sb.AppendFormat("\n{0}Message: {1}", indent, e.Message);
            sb.AppendFormat("\n{0}Source: {1}", indent, e.Source);
            sb.AppendFormat("\n{0}Stacktrace: {1}", indent, e.StackTrace);

            if (e.InnerException != null)
            {
                sb.Append("\n");
                CreateExceptionString(sb, e.InnerException, indent + "  ");
            }
        }
    }
}
