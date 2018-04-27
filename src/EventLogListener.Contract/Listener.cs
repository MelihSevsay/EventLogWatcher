using EventLogListener.Contract.Extentions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLogListener.Contract
{
    public static class Listener
    {
        

        public static void Subscribe(EventLogQuery eventLogQuery = null)
        {
            
            StreamWriterExtention.WriteToFile("Subscribe "+DateTime.Now.ToString("hh:mm:ss"));

            EventLogWatcher watcher = null;
            try
            {

                if (eventLogQuery == null)
                {
                    //EventLogQuery subscriptionQuery = new EventLogQuery("Security", PathType.LogName, "*[System/EventID=4624]"); "*[System/EventID=0 or System/EventID=21 or System/EventID=23 or System/EventID=24 or System/EventID=25]");
                    string queryString = "*[System/EventID=0 or System/EventID=21 or System/EventID=23 or System/EventID=23 or System/EventID=24 or System/EventID=25]";//"<QueryList><QueryId=\"0\"Path=\"Application\"><SelectPath=\"Application\">*[System[(EventID=21 or EventID=22 or EventID=23)]]</Select></Query></QueryList>";
                    eventLogQuery = new EventLogQuery("Application", PathType.LogName, queryString);

                }

                watcher = new EventLogWatcher(eventLogQuery);

                // Make the watcher listen to the EventRecordWritten
                // events.  When this event happens, the callback method
                // (EventLogEventRead) is called.
                watcher.EventRecordWritten += new EventHandler<EventRecordWrittenEventArgs>(EventLogReaderExtention.EventLogEventRead);

                // Activate the subscription
                watcher.Enabled = true;


                Console.ReadLine();
                //for (int i = 0; i < 5; i++)
                //{
                //    // Wait for events to occur. 
                //    System.Threading.Thread.Sleep(100000);
                //}
            }
            catch (EventLogReadingException e)
            {
                Console.WriteLine("Error reading the log: {0}", e.Message);
            }
            finally
            {
                // Stop listening to events
                watcher.Enabled = false;

                if (watcher != null)
                {
                    watcher.Dispose();
                }
            }

            StreamWriterExtention.WriteToFile("Close " + DateTime.Now.ToString("hh:mm:ss"));
            Console.WriteLine("NoEvent triggered");

            //WindowsService de hata VERIYOR.
            //TaskSchedular da hata vermiyor.
            //Console.ReadKey();
        }


    }
}
