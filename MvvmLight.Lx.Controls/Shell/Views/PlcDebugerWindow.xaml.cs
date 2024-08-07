using MvvmLight.Lx.Controls.Shell.ViewModels;
using MvvmLight.Lx.Controls.ViewModels;
using MvvmLight.Lx.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MvvmLight.Lx.Controls.Shell.Views
{
    /// <summary>
    /// PlcDebugerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PlcDebugerWindow : Window
    {
        public PlcDebugerWindow()
        {
            InitializeComponent();
            this.DataContext = ViewModelServiceLocator.Resolve<PlcDebugerWindowViewModel>();
        }
    }
}
