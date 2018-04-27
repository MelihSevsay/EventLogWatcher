using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLogListener.Contract.Type.Enums
{
    public enum RDPType : byte
    {

        Default = 1,
        Logon = 21,
        ShellStart = 22,
        Logoff = 23,
        Disconnected = 24,
        Reconnection = 25

    }
}
