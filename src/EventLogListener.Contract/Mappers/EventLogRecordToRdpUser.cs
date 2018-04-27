using EventLogListener.Contract.Type.Enums;
using EventLogListener.Contract.Type.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLogListener.Contract.Mappers
{
    public class RdpUserMapper
    {

        public static RDPUser EventLogRecordToRdpUser(IList<object> prop)
        {

            //xPathRefs[0] = "Event/System/TimeCreated/@SystemTime";
            //xPathRefs[1] = "Event/System/Computer";
            //xPathRefs[2] = "Event/System/EventRecordID";
            //xPathRefs[3] = "Event/EventData/Data[@Name=\"TargetUserName\"]";
            //xPathRefs[4] = "Event/EventData/Data[@Name=\"TargetDomainName\"]";
            //xPathRefs[5] = "Event/UserData/EventXML/User";
            //xPathRefs[6] = "Event/UserData/EventXML/Address";
            
            RDPType enumResult = RDPType.Default;
            if (prop[7]!=null)  
                Enum.TryParse<RDPType>(prop[7].ToString(), out enumResult);

            return new RDPUser {

                EventId = Convert.ToInt32(prop[2]),
                RDP_DATE = Convert.ToDateTime(prop[0]),
                RDP_USER = prop[5] !=null ? prop[5].ToString() : prop[1].ToString(),
                RDP_IP = prop[6]!=null ? prop[6].ToString() : "localhost",
                RDP_TYPE = prop[7] != null ? enumResult.ToString() : RDPType.Default.ToString()

            };

        }


    }
}
