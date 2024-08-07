using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MvvmLight.Lx.Core.Extensions
{
    public static class WinExtension
    {
        public static void Show(this Window win, string winName)
        {
            Type type = Type.GetType(winName);

            if (type != null)
            {
                var window = Activator.CreateInstance(type) as Window;
                window.Show();
            }
        }
    }

    public interface IWinResult { }
}
