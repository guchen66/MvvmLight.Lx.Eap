using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.Entity;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Xml.Linq;
using HandyControl.Controls;
using MvvmLight.Lx.Admin.IServices;
using MvvmLight.Lx.Admin.Services;
using MvvmLight.Lx.Controls.Mvvm;
using MvvmLight.Lx.Core.Devices.Infos;
using MvvmLight.Lx.Core.Extensions;
using MvvmLight.Lx.DbAccess;
using MvvmLight.Lx.DbAccess.Repositorys;
using MvvmLight.Lx.DbAccess.Services;
using MvvmLight.Lx.DbAccess.TestDemo;
using MvvmLight.Lx.Devices.PLC.Ioc;
using MvvmLight.Lx.Devices.PLC.Repositorys;
using MvvmLight.Lx.Eap.Components;
using MvvmLight.Lx.Eap.Modules;
using MvvmLight.Lx.Eap.ViewModels;
using MvvmLight.Lx.Eap.Views;
using MySql.Data.MySqlClient;
using Unity;
using Unity.Injection;
using Unity.Interception;
using Unity.Lifetime;
using Unity.Resolution;

namespace MvvmLight.Lx.Eap
{
    public class Bootstrapper
    {
        private IUnityContainer Container { get; set; }

        public Bootstrapper()
        {          
            Container = ConfigureService();
        }

        private IUnityContainer ConfigureService()
        {
            MyStartup.AddMvvmLightContext();
            var container = new UnityContainer();
            ViewModelServiceLocator.Initialize(container);          //自定义服务定位器    
            container.AddMvvmLightContext();                        //初始化注册数据库
          //  container.RegisterComponent<LocalServerComponent>();
            container.RegisterModules();                            //自定义Module    
            return container;
        }

        public void Run()
        {      
            var app = new Application();
            app.Startup += App_Startup;
            var login = new LoginWindow();                                         //在使用IUnityContainer时候，必须new 否则莫名其妙报错,不报错，直接退出
            var view = Container.Resolve<LoginWindow>();                           //必须解析否则应用对象正在关闭
            var loginWindowViewModel = Container.Resolve<LoginWindowViewModel>();
            view.DataContext = loginWindowViewModel;
            var result = view.ShowDialog();
            if (result == false)
            {
                // Application.Current.Shutdown();
            }
            else
            {
               // ProgramHelper.Usage();
                var mainWindow = Container.Resolve<MainWindow>();
                var mainViewModel = Container.Resolve<MainWindowViewModel>();
                mainWindow.DataContext = mainViewModel;
                mainWindow.Show();
                app.Run(mainWindow);
               
            }
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            if (sender is Application app)
            {
                //添加HandyControl组件库
                app.Resources.Clear();
                app.Resources.MergedDictionaries.Add(HandOrgResource);           
                
                //初始化资源，图像，样式，字体
                //执行首次运行检查
                //配置日志记录
                //注册事件处理器，键盘快捷键，系统事件
                //配置权限和安全设置
                //初始化插件系统
                //检查更新，提醒用户
            }
        }    

        public static ResourceDictionary HandOrgResource => LoadTheme();

        public static ResourceDictionary LoadTheme()
        {
           
            // 加载资源字典
            var resourceDictionary = new ResourceDictionary();
            resourceDictionary.Source = new Uri(
                "pack://application:,,,/HandOrgResource.xaml",
                UriKind.RelativeOrAbsolute
            );
            //  resourceDictionary.Source = new Uri("/MvvmLight.Lx.Eap;component/ApplicationResource.xaml", UriKind.RelativeOrAbsolute);
            return resourceDictionary;
        }

        public static void LoadStyle()
        {
            // 创建一个新的Style对象
            Style globalStyle = new Style(typeof(Control));

            // 设置Style的TargetType属性
            globalStyle.TargetType = typeof(Control);

            // 创建FontFamily对象，并设置楷体字体
            FontFamily 楷体 = new FontFamily("楷体");

            // 设置Style的Setter属性，设置FontFamily为楷体
            globalStyle.Setters.Add(new Setter(Control.FontFamilyProperty, 楷体));

            // 可以添加其他样式设置，例如设置字体大小
            // globalStyle.Setters.Add(new Setter(Control.FontSizeProperty, 12.0));

            // 将Style添加到应用程序的资源中
          //  this.Resources.Add("GlobalStyle", globalStyle);
        }
    }
}
