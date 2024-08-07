using MvvmLight.Lx.Admin.IServices;
using MvvmLight.Lx.Admin.Services;
using MvvmLight.Lx.Controls.ViewModels;
using MvvmLight.Lx.Controls.Views;
using MvvmLight.Lx.Eap.ViewModels;
using MvvmLight.Lx.Eap.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace MvvmLight.Lx.Eap.Modules
{
    /// <summary>
    /// 这种写法对有参构造器可能有问题
    /// </summary>
    public class ViewToViewModelContailerModule : IUnityContainerModule
    {
        public void ConfigureServices(IUnityContainer container)
        {
            container.RegisterType<MainWindow>();
            container.RegisterType<LoginWindow>();
            container.RegisterType<HeaderView>();
            container.RegisterType<HomeView>();
            container.RegisterType<RightTableView>();
            container.RegisterType<LoginWindowViewModel>();
            container.RegisterType<MainWindowViewModel>();
            container.RegisterType<HomeViewModel>();
            container.RegisterType<RightTableViewModel>();
        }
    }
}
