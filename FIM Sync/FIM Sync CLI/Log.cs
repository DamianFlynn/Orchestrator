using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using FIM_Sync_LIB;
using System.Diagnostics.Eventing;
using System.Diagnostics;

namespace FIM_Sync_CLI
{
    public class ETWLog
    {
        private static Guid providerId = new Guid("{A11a0820-c24b-428c-83e2-26b41091702e}");
        private EventProviderTraceListener ETWListener;
        private TraceSource source;


        public void TraceSource newLog(string logname)
        {
            ETWListener = new EventProviderTraceListener(providerId.ToString());
            source = new TraceSource(logname, SourceLevels.All);
            source.Listeners.Add(ETWListener);
        }


        public void WarnFormat(string LogEntry)
        {
            source.TraceData(
                TraceEventType.Warning | TraceEventType.Start, 2,
                new object[] { LogEntry });
                //new object[] { "abc", "def", true, 123 });
        }

        public void DebugFormat(string LogEntry)
        {
            // Uncommen below if you want more fancy logging ...
            source.TraceEvent(
                TraceEventType.Warning, 12, "Provider guid: {0}",
                new object[] { providerId });
            source.TraceInformation(
               "string {0}, bool {1}, int {2}, ushort {3}",
               new object[] { LogEntry, false, 123, (UInt32)5 });
        }
    }
}


