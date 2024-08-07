using MvvmLight.Lx.Admin.IServices;
using MvvmLight.Lx.Admin.Services;
using MvvmLight.Lx.DbAccess.Repositorys;
using MvvmLight.Lx.DbAccess.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace MvvmLight.Lx.Eap.Modules
{
   public class DialogContailerModule : IUnityContainerModule
    {
        public void ConfigureServices(IUnityContainer container)
        {
            container.RegisterType<IWinDialogService, WinDialogService>();
            container.RegisterType<ILoginService, LoginService>();
        }
    }
}
