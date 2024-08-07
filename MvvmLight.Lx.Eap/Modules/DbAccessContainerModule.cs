using MvvmLight.Lx.Admin.IServices;
using MvvmLight.Lx.Admin.Services;
using MvvmLight.Lx.Core.Devices.Infos;
using MvvmLight.Lx.DbAccess.Repositorys;
using MvvmLight.Lx.DbAccess.Services;
using MvvmLight.Lx.Devices.PLC.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace MvvmLight.Lx.Eap.Modules
{
    public class DbAccessContainerModule : IUnityContainerModule
    {
        public void ConfigureServices(IUnityContainer container)
        {
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IHeaderRepository, HeaderRepository>();
            container.RegisterType<IHeaderService, HeaderService>();
           // container.RegisterType<IPlcRepository<PlcInfo>, PlcRepository<PlcInfo>>();
        }
    }
}
