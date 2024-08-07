using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.Devices.PLC
{
    public enum PlcActionType
    {
        Insert = 0,
        Update = 1,
        Delete = 2,
        Query = 3,
        UnKnown = -1
    }
}
