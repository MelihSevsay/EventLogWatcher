using EventLogListener.Contract.Managers;
using EventLogListener.Contract.Mappers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLogListener.Contract.Extentions
{
    public static class DbOperationExtentions
    {

        private static bool DbOperationEnabled { get; set; } = Convert.ToBoolean(ConfigurationManager.AppSettings["DbOperationEnabled"]);

        public static void WriteToDb(IList<object> prop)
        {

            if (DbOperationEnabled)
            {

#if (DEBUG)
{
                DbOperationManager.GetRDPHistory();
}
#endif

                var user = RdpUserMapper.EventLogRecordToRdpUser(prop);
                DbOperationManager.InsertRDPHistory(user);
            }

        }

    }
}
