using EventLogListener.Contract.Environments;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLogListener.Contract.Extentions
{
    public class StreamWriterExtention
    {


        private static string LogFilePath { get; set; } = ConfigurationManager.AppSettings["LogFilePath"];
        private static bool WriteToFileEnabled { get; set; } = Convert.ToBoolean(ConfigurationManager.AppSettings["WriteToFileEnabled"]);
        

        public static void WriteToFile(IList<object> logEventProps, string filePath = null)
        {

            if (WriteToFileEnabled)
            {
                filePath = filePath ?? LogFilePath;

                if (!string.IsNullOrWhiteSpace(filePath))
                {
                    using (StreamWriter writer = new StreamWriter(filePath, true))
                    {

                        writer.WriteLine("Time: {0}", logEventProps[0]);
                        writer.WriteLine("Computer: {0}", logEventProps[1]);
                        writer.WriteLine("EventId: {0}", logEventProps[2]);
                        writer.WriteLine("TargetUserName: {0}", logEventProps[3]);
                        writer.WriteLine("TargetDomainName: {0}", logEventProps[4]);
                        writer.WriteLine("User: {0}", logEventProps[5]);
                        writer.WriteLine("IP: {0}", logEventProps[6]);
                        writer.WriteLine("EventType: {0}", logEventProps[7]);

                        writer.WriteLine("---------------------------------------");

                        writer.Flush();

                    }

                }

            }

        }

        public static void WriteToFile(string message, string filePath = null, bool forceToWrite = false)
        {

            if (WriteToFileEnabled || forceToWrite)
            {
                filePath = filePath ?? LogFilePath;

                if (!string.IsNullOrWhiteSpace(filePath))
                {
                    using (StreamWriter writer = new StreamWriter(filePath, true))
                    {

                        writer.WriteLine("---------------------------------------");

                        
                        if (forceToWrite)                        
                            writer.WriteLine($"{DateTime.Now.ToString(Globals.BeautifulDateTimeFormat)} => " + message);
                        else
                            writer.WriteLine(message);
                        

                        writer.WriteLine("---------------------------------------");

                        writer.Flush();

                    }
                }

            }

        }


    }
}
