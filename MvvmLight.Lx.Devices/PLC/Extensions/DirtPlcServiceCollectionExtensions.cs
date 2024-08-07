using MvvmLight.Lx.Devices.PLC.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.Devices.PLC.Extensions
{
    public static class DirtPlcServiceCollectionExtensions
    {
        internal static List<IocPlcConfig> configs = new List<IocPlcConfig>();
    }
}
