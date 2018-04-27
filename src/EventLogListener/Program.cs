using EventLogListener.Contract.Extentions;
using EventLogListener.Contract;
using System.Diagnostics.Eventing.Reader;

namespace EventLogListener
{
    class Program
    {

        static void Main(string[] args)
        {

            //Read already logged events
            //EventLogReaderExtention.ReadEventLogData();

#if (DEBUG)
            {
                #region EventLog Query For Test, EventPublisher creates events for this query.

                string queryString = "*[System/EventID=0 or System/EventID=21 or System/EventID=23 or System/EventID=24 or System/EventID=25]";//"<QueryList><QueryId=\"0\"Path=\"Application\"><SelectPath=\"Application\">*[System[(EventID=21orEventID=22orEventID=23)]]</Select></Query></QueryList>";
                
                EventLogQuery eventLogQuery = new EventLogQuery("Application", PathType.LogName, queryString);                

                Listener.Subscribe(eventLogQuery);

                #endregion
            }
#else
            {
            #region

                string queryString =
                "<QueryList>" +
                "  <Query Id='0' Path='Microsoft-Windows-TerminalServices-LocalSessionManager/Operational'>" +
                "    <Select Path='Microsoft-Windows-TerminalServices-LocalSessionManager/Operational'>*[System[(EventID=21 or EventID=23 or EventID=24 or EventID=25)]]</Select>" +
                "  </Query>" +
                "</QueryList>";

            EventLogQuery eventLogQuery = new EventLogQuery("System", PathType.LogName, queryString);


            Listener.Subscribe(eventLogQuery);

            #endregion
            }
#endif

        }


    }
}
