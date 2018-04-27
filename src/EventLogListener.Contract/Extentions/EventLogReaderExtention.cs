using EventLogListener.Contract.Managers;
using EventLogListener.Contract.Mappers;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLogListener.Contract.Extentions
{
    public static class EventLogReaderExtention
    {


        public static void EventLogEventRead(object obj, EventRecordWrittenEventArgs arg)
        {
            
            if (arg.EventRecord != null)
            {
                //////
                // This section creates a list of XPath reference strings to select
                // the properties that we want to display
                // In this example, we will extract the User, TimeCreated, EventID and EventRecordID
                //////
                // Array of strings containing XPath references
                String[] xPathRefs = new String[8];
                xPathRefs[0] = "Event/System/TimeCreated/@SystemTime";
                xPathRefs[1] = "Event/System/Computer";
                xPathRefs[2] = "Event/System/EventRecordID";
                xPathRefs[3] = "Event/EventData/Data[@Name=\"TargetUserName\"]";
                xPathRefs[4] = "Event/EventData/Data[@Name=\"TargetDomainName\"]";
                xPathRefs[5] = "Event/UserData/EventXML/User";
                xPathRefs[6] = "Event/UserData/EventXML/Address";
                xPathRefs[7] = "Event/System/EventID";
                

                IEnumerable<String> xPathEnum = xPathRefs;

                // Create the property selection context using the XPath reference
                EventLogPropertySelector logPropertyContext = new EventLogPropertySelector(xPathEnum);

                IList<object> logEventProps = ((EventLogRecord)arg.EventRecord).GetPropertyValues(logPropertyContext);
                StreamWriterExtention.WriteToFile(logEventProps);
                DbOperationExtentions.WriteToDb(logEventProps);

#if (DEBUG)
                {

                    Console.WriteLine("U1 Time: {0}", logEventProps[0]);
                    Console.WriteLine("Computer: {0}", logEventProps[1]);
                    Console.WriteLine("EventRecordId: {0}", logEventProps[2]);
                    Console.WriteLine("TargetUserName: {0}", logEventProps[3]);
                    Console.WriteLine("TargetDomainName: {0}", logEventProps[4]);
                    Console.WriteLine("User: {0}", logEventProps[5]);
                    Console.WriteLine("IP: {0}", logEventProps[6]);
                    Console.WriteLine("EventType: {0}", logEventProps[7]);

                    Console.WriteLine("---------------------------------------");

                    Console.WriteLine("Description: {0}", arg.EventRecord.FormatDescription());

                }
#endif

            }
            else
            {
#if (DEBUG)
                {
                    Console.WriteLine("The event instance was null.");
                }
#endif

            }
        }

        public static void ReadEventLogData()
        {

            string queryString =
                "<QueryList>" +
                "  <Query Id='0' Path='Microsoft-Windows-TerminalServices-LocalSessionManager/Operational'>" +
                "    <Select Path='Microsoft-Windows-TerminalServices-LocalSessionManager/Operational'>*[System[(EventID=21 or EventID=23 or EventID=24 or EventID=25)]]</Select>" +
                "  </Query>" +
                "</QueryList>";
            
            try
            {

                EventLogQuery eventsQuery = new EventLogQuery("System", PathType.LogName, queryString);
                EventLogReader logReader = new EventLogReader(eventsQuery);

                //create an array of strings for xpath enum.
                String[] xPathRefs = new String[4];
                xPathRefs[0] = "Event/UserData/EventXML/User";
                xPathRefs[1] = "Event/UserData/EventXML/Address";
                xPathRefs[2] = "Event/System/EventRecordID";
                xPathRefs[3] = "Event/System/EventID";

                //Sample to read different way to map.
                //xPathRefs[2] = "Event/EventData/Data[@Name='MainPathBootTime']";
                //xPathRefs[3] = "Event/EventData/Data[@Name='BootPostBootTime']";

                IEnumerable<String> xPathEnum = xPathRefs;
                EventLogPropertySelector logPropertyContext = new EventLogPropertySelector(xPathEnum);

                for (EventRecord eventDetail = logReader.ReadEvent(); eventDetail != null; eventDetail = logReader.ReadEvent())
                {

                    IList<object> logEventProps;
                    logEventProps = ((EventLogRecord)eventDetail).GetPropertyValues(logPropertyContext);

                    string user = logEventProps[0].ToString();
                    string ip = logEventProps[1] !=null ? logEventProps[1].ToString() : "";
                    int eventRecordID = logEventProps[2]!=null ? Convert.ToInt32(logEventProps[2]) : 0;
                    int eventTypeId = logEventProps[3]!=null ? Convert.ToInt32(logEventProps[3]) : 0;

                }
                
            }
            catch (EventLogNotFoundException e)
            {


#if (DEBUG)
                {
                    Console.WriteLine("Error while reading the event logs");
                }
#endif

            }

        }
        



    }
}
