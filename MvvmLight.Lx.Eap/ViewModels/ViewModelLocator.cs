using MvvmLight.Lx.Controls.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Unity;
using Unity.Extension;

namespace MvvmLight.Lx.Eap.ViewModels
{
    public class ViewModelLocationProvider
    {
        public Control Build(object data)
        {
            if (data is null)
                return null;

            //获取data的限定名，View替换ViewModel
            var name = data.GetType().FullName.Replace("ViewModel", "View");
            var type = Type.GetType(name);

            if (type != null)
            {
                var control = (Control)Activator.CreateInstance(type);   //实例化 -> 转成WPF UI基类
                control.DataContext = data;
                return control;
            }

            return new Control();
        }

        public static void SetDefaultViewToViewModel(Action<IUnityContainer> action)
        {
            action.Invoke(Container);
        }

        public static void SetDefaultViewToViewModel<TView>(TView view, Action<TView, IUnityContainer> func) where TView : Window
        {
            // 确保 view 不是 null
            if (view != null)
            {
                func(view, Container);
            }
        }

        public static void SetMvvmContact<TView,TViewModel>(TView view, TViewModel viewModel) where TView : Window
        {
            // 确保 view 不是 null
            if (view is Window win)
            {
                win.DataContext = viewModel;
            }
        }

        //   public Type ViewType => GetClassType();
        static IUnityContainer Container;
       /* public ViewModelLocationProvider(IUnityContainer unityContainer)
        {
            Container=unityContainer;
        }*/
       /// <summary>
       /// 注意配置和约定
       /// </summary>
       /// <param name="viewType"></param>
        public void Get(Window viewType)
        {
            var viewName = viewType.GetType().FullName.Replace("Window", "WindowViewModel");
            var viewModel = Type.GetType(viewName);
            var viewAssemblyName= viewType.GetType().Assembly.FullName;
            var viewModelName = $"{viewName},{viewAssemblyName}";

          /*  if (viewModelType != null)
            {
                var viewModel = Container.Resolve(viewModelType) as BaseViewModel; // 假设你的ViewModel继承自ViewModelBase
                viewType.DataContext = viewModel;
            }*/

        }
        /// <summary>
        /// 只获取类名
        /// </summary>
        public IEnumerable<Type> GetClassType()
        {
            // 获取当前应用程序的目录，假设Models文件夹与应用程序在同一目录下
            string modelsFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Views");

            // 获取Views文件夹中的所有类
            var clazz = GetClassNamesInFolderByLinq();
            return clazz;
        }

        public IEnumerable<Type> GetClassNamesInFolderByLinq()
        {
            // 加载调用方程序集
            Assembly callingAssembly = Assembly.GetCallingAssembly();
            // 获取所有类型
            Type[] allTypes = callingAssembly.GetTypes();

            // 使用 LINQ 查询筛选出位于 Views 文件夹下的类型，并筛选出Model结尾的，获取类名
            var classNames = allTypes
                .Where(type => type.Namespace != null && type.Namespace.Contains("Views") && type.Name.EndsWith("Window"));
            return classNames;
        }
    }
    public class ViewModelLocator
    {
        public readonly IUnityContainer Container;

        public ViewModelLocator()
        {
          
            this.Container = ContainerLocator.Current;
            
        }
        //ContainerBuilder

        public void GetWindow(Window win)
        {
            win.DataContext = this;
        }
        public MainWindowViewModel MainWindowViewModel=>Container.Resolve<MainWindowViewModel>();

    }

    public class ContainerLocator
    {
        public static IUnityContainerExtensionConfigurator _container;
        public static UnityContainer containerExtension;
        public static IUnityContainer Current => containerExtension;
        private static Lazy<IUnityContainer> _lazyContainer;

        public ContainerLocator()
        {
           // UnityContainerExtensions.Resolve<IUnityContainer>();
        }

        public static void SetContainerExtension(Func<IUnityContainer> factory) =>
           _lazyContainer = new Lazy<IUnityContainer>(factory);
    }
}
