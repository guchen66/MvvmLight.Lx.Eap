using MvvmLight.Lx.Admin.IServices;
using MvvmLight.Lx.Admin.Services;
using MvvmLight.Lx.Controls.ViewModels;
using MvvmLight.Lx.Controls.Views;
using MvvmLight.Lx.DbAccess;
using MvvmLight.Lx.DbAccess.TestDemo;
using MvvmLight.Lx.Eap.ViewModels;
using MvvmLight.Lx.Eap.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Extension;
using Unity.Lifetime;

namespace MvvmLight.Lx.Eap.Components
{
    /// <summary>
    /// 容器依赖组件
    /// </summary>
    public interface IContainerComponent
    {
        void Load(IUnityContainer container);
    }

    public class LocalServerComponent : IContainerComponent
    {
        public void Load(IUnityContainer container)
        {
            container.RegisterType<ILoginService, LoginService>();
            container.RegisterType<MainWindow>();
            container.RegisterType<LoginWindow>();
            container.RegisterType<HeaderView>();
            container.RegisterType<HomeView>();
            container.RegisterType<LoginWindowViewModel>();
            container.RegisterType<MainWindowViewModel>();
            container.RegisterType<HomeViewModel>();

            // 配置DbContext
        //    container.RegisterType<MvvmLightContext>(new ContainerControlledLifetimeManager());
          

        }
    }
}
