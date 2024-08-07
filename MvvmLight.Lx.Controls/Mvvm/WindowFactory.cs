using MvvmLight.Lx.Controls.Shell.Views;
using MvvmLight.Lx.Controls.Views;
using MvvmLight.Lx.Core.Devices.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MvvmLight.Lx.Controls.Mvvm
{
    public class WindowFactory
    {
        private static readonly Lazy<WindowFactory> _clientExtension = new Lazy<WindowFactory>(() => new WindowFactory());

        public static WindowFactory Current => _clientExtension.Value;

        private readonly Dictionary<string, Func<Window>> eventHandlers = new Dictionary<string, Func<Window>>();

        public void CreateWindowToOpen(string navigatPath)
        {
            Type type = Type.GetType(navigatPath);
            if (type == null) { return; }
            // 检查是否已经存在对应的委托
            if (eventHandlers.TryGetValue(navigatPath, out var handler))
            {
                // 如果委托存在，直接使用委托创建窗口
                handler();
            }

            // 如果委托不存在，创建新窗口并存储委托
            Window window = Activator.CreateInstance(type) as Window;
            // 存储委托到字典，这里使用了 GetOrAdd 方法确保线程安全
            eventHandlers[navigatPath] = () => window;

            // 检查窗口是否已经打开
            if (window != null && window.Visibility != Visibility.Visible)
            {
                window.ShowDialog();
            }

        }
    }
}


