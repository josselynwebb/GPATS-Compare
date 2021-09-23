// SVN Information
// $Author:: wileyj           $: Author of last commit
//   $Date:: 2020-07-06 16:01#$: Date of last commit
//    $Rev:: 27851            $: Revision of last commit

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;  // Add System.ServiceProcess framework reference from systemserviceprocess.dll
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace NAM
{

    /// <summary>
    /// <c>EventViewer</c> class
    /// Contains dll imports and methods relating to the Windows Event Log Viewer
    /// </summary>
    class EventViewer
    {
        private const EventLogEntryType LOG_ERROR = EventLogEntryType.Error;
        private const EventLogEntryType LOG_WARN  = EventLogEntryType.Warning;
        private const EventLogEntryType LOG_INFO  = EventLogEntryType.Information;
        
        public List<EventLogEntry> ReadLog(string logName, string logSource, int numEntries=1)
        {
            List<EventLogEntry> logEntries = new List<EventLogEntry>();

            if (EventLog.SourceExists(logSource))
            {
                using (EventLog log = new EventLog(logName, ".", logSource))
                {

                    foreach (EventLogEntry entry in log.Entries)
                    {
                        if (entry.Source == logSource)
                        {
                            logEntries.Add(entry);
                        }
                    }
                }
            }

            return logEntries;
        }

        public bool WriteLog(string logName, string logSource, string message, EventLogEntryType logType=LOG_INFO)
        {
            if (!EventLog.SourceExists(logSource))
            {
                EventLog.CreateEventSource(logSource, logName);
            }

            using (EventLog log = new EventLog(logName, ".", logSource))
            {
                try
                {
                    log.WriteEntry(message, logType);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return true;
        }

        public EventLogEntry FindEntry(List<EventLogEntry> eventLogList, string phrase, EventLogEntryType type = LOG_INFO)
        {
            foreach (EventLogEntry entry in eventLogList)
            {
                if (entry.EntryType == type && entry.Message.Contains(phrase))
                {
                    return entry;
                }
            }

            return null;
        }
    }
}