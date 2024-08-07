using MvvmLight.Lx.Admin.IServices;
using MvvmLight.Lx.Admin.Services;
using MvvmLight.Lx.Eap.ViewModels;
using MvvmLight.Lx.Eap.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using Unity;
using HandyOrgMessageBox = HandyControl.Controls.MessageBox;
namespace MvvmLight.Lx.Eap
{
    public class Program 
    {
        private static Mutex mutex;
        [STAThread]
        public static void Main(string[] args)
        {
            mutex = new Mutex(true, "MvvmLight.Lx.Eap");
            if (mutex.WaitOne(0, false))
            {
                var bootstrapper = new Bootstrapper();
                bootstrapper.Run();
            }
            else
            {
                HandyOrgMessageBox.Error("已经有一个软件运行中，请勿重复开启！", "提示");
                Application.Current.Shutdown();
            }

        }

    }

    public abstract class AppBase : Application
    {
        public IUnityContainer _Container;
        protected virtual void Initialize()
        {
            ContainerLocator.SetContainerExtension(CreateContainerExtension);
            _Container = ContainerLocator.Current;
        }
        protected abstract IUnityContainer CreateContainerExtension();
    }


    public class ProgramHelper
    {
        public static void Usage()
        {
            // 创建一个新的线程池实例
            ThreadPool.QueueUserWorkItem(Worker);

            // 等待所有任务完成
            ThreadPool.QueueUserWorkItem(Wait);

            Console.WriteLine("结束");
        }

        private static void Worker(object state)
        {
            while (!Thread.CurrentThread.IsBackground)
            {
                Console.WriteLine("正在执行任务");

                // 等待一段时间
                Thread.Sleep(1000);
            }
        }

        private static void Wait(object state)
        {
            while (!Thread.CurrentThread.IsBackground)
            {
                Console.WriteLine("等待任务完成");

                // 等待一段时间
                Thread.Sleep(1000);
            }
        }
    }
 }
