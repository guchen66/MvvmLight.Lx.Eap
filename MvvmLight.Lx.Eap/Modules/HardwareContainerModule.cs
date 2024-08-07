using Modbus.Device;
using MvvmLight.Lx.Admin.IServices;
using MvvmLight.Lx.Admin.Services;
using MvvmLight.Lx.Core.Devices.Clients;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;

namespace MvvmLight.Lx.Eap.Modules
{
    public class HardwareContainerModule : IUnityContainerModule
    {
        public void ConfigureServices(IUnityContainer container)
        {
            container.RegisterType<IPlcService, PlcService>();
            container.RegisterType<IScanCodeService, ScanCodeService>();
            container.RegisterType<PlcClient>();

            container.RegisterType<IModbusMaster, ModbusIpMaster>();
        }
    }
}
