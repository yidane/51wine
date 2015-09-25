using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace TicketStatusService
{
    public class LogManager
    {
        private readonly static object _Lock = new object();
        private readonly static List<TaskToolLog> LogList = new List<TaskToolLog>();

        public static void WriteLog(string logName, string logContent)
        {
            TaskToolLog oTaskToolLog = null;
            if (LogList.Any(log => string.Equals(log.LogName, logName)))
            {
                oTaskToolLog = LogList.Find(log => string.Equals(log.LogName, logName));
            }
            else
            {
                lock (_Lock)
                {
                    var logInfo = log4net.LogManager.GetLogger(logName);
                    oTaskToolLog = new TaskToolLog(logName, logInfo);
                    LogList.Add(oTaskToolLog);
                }
            }

            if (oTaskToolLog != null)
                oTaskToolLog.LogInfo.Info(logContent);
        }
    }

    public class TaskToolLog
    {
        public TaskToolLog(string logName, ILog logInfo)
        {
            this.LogName = logName;
            this.LogInfo = logInfo;
        }
        public string LogName { get; set; }
        public ILog LogInfo { get; set; }
    }
}
