using MvvmLight.Lx.Core.Devices.Infos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.Devices.PLC
{
    public interface IReadClient
    {
        PlcInfo Where(Func<PlcInfo, object> value);
    }

    public class ReadClient : IReadClient
    {
        public PlcInfo Where(Func<PlcInfo, object> value)
        {
            PlcInfo plcInfo = new PlcInfo();
            plcInfo.Id = 2;
            plcInfo.PlcName = "西门子2";
            plcInfo.IP = "192.168.0.1111";
            return plcInfo;
        }
    }
}
