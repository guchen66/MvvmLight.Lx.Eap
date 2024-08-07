using MvvmLight.Lx.Core.Devices.Infos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.Devices.PLC
{
    public class DirtPlcProvider : IDirtPlcClient
    {
        public DirtPlcClient Client { get;  set; }

        public IReadClient Readable<T>()
        {
            return Client.Readable<T>();
        }

        public PlcInfo Where(Func<PlcInfo, object> value)
        {
            PlcInfo plcInfo = new PlcInfo();
            plcInfo.Id = 1;
            plcInfo.PlcName = "西门子";
            plcInfo.IP = "192.168.0.1";
            return plcInfo;
        }
    }
}
