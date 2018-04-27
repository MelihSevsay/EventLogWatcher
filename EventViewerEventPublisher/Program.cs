using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventViewerEventPublisher
{
    class Program
    {
        static void Main(string[] args)
        {


            //detects key pressed on console app
            ConsoleKeyInfo key = Console.ReadKey();
            int count = 0;
            //Keep a live till 'Enter' is presses in the console.
            while (key.Key.Equals(ConsoleKey.P))
            {

                //EventLog myNewLog = new EventLog("Application", ".", "testEventLogEvent");                
                EventLog myNewLog = new EventLog("Application", "MELIH-SEVSAY", "TerminalServices-LocalSessionManager");
                
                myNewLog.EnableRaisingEvents = true;
                myNewLog.WriteEntry("Test message : 21", EventLogEntryType.Information, 21);
                //myNewLog.WriteEntry("Test message : 22", EventLogEntryType.Information,22);
                //myNewLog.WriteEntry("Test message : 23", EventLogEntryType.Information,23);

                Console.WriteLine("key(P) is pressed : new Envent published "+ count++);
                
                key = Console.ReadKey();
            }

            Console.WriteLine("Publisher Ended");


        }

    }
}
