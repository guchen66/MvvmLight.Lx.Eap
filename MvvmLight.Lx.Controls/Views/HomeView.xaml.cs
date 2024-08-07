using MvvmLight.Lx.Admin.IServices;
using MvvmLight.Lx.Controls.ViewModels;
using MvvmLight.Lx.Core.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Unity;
using Unity.Extension;
using Unity.Resolution;

namespace MvvmLight.Lx.Controls.Views
{
    /// <summary>
    /// HomeView.xaml 的交互逻辑
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();

            this.DataContext = ViewModelServiceLocator.DefaultContainer.Resolve<HomeViewModel>();
          //  UnityContainer unityContainer = new UnityContainer();
          //  UnityContainerExtensions.Resolve<HomeViewModel>(unityContainer);
        }
    }
}
