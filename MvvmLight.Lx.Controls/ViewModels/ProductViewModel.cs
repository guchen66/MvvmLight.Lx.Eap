using MvvmLight.Lx.Controls.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace MvvmLight.Lx.Controls.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        private string _name;

        public string TxtName
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

 
        public ProductViewModel(IUnityContainer container) : base(container)
        {
            TxtName = "ProductView界面";
        }
    }
}
