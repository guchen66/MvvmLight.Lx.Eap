using MvvmLight.Lx.Devices.PLC.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.Devices.PLC.Ioc
{
    public class PlcIocServices
    {
        public static void AddDirtPlc(IocPlcConfig iocPlcConfig)
        {
            DirtPlcServiceCollectionExtensions.configs.Add(iocPlcConfig);
        }
    }
}
