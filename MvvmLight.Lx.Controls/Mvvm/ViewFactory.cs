using MvvmLight.Lx.Controls.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.Controls.Mvvm
{
    public static class ViewFactory
    {
        public static HomeView Create()
        {
            return new HomeView();
        }
    }
}
