using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using MvvmLight.Lx.Admin.Services;
using MvvmLight.Lx.Admin.IServices;
using MvvmLight.Lx.Core.Extensions;
namespace MvvmLight.Lx.Controls.Mvvm
{
    public class BaseViewModel: ObservableObject
    {
        IUnityContainer _container;
        public BaseViewModel(IUnityContainer container)
        {
            _container = container;

         //   _loginService = ServiceLocator.Resolve<ILoginService>();

          //  _loginService =UnityContainerExtensions.Resolve<ILoginService>(container);
        }
    }
}
