using EventLogListener.Contract.Type.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLogListener.Contract.Type.Models
{
    public class RDPUser
    {

        public long EventId { get; set; }
        public string RDP_IP { get; set; }
        public string RDP_USER { get; set; }
        public DateTime RDP_DATE { get; set; }
        public string RDP_TYPE { get; set; }

    }
}
