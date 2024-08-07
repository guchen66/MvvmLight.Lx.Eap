using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmLight.Lx.Admin.IServices;
using MvvmLight.Lx.Controls.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;

namespace MvvmLight.Lx.Controls.ViewModels
{
    public class HomeViewModel : ObservableObject
    {
        private string _name;

        public string TxtName
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public IWinDialogService _winService;
        public HomeViewModel(IWinDialogService winService)
        {
            _winService = winService;
            TxtName = "Home界面";
            OpenWinCommand = new RelayCommand(ExecuteOpen);
        }

        private void ExecuteOpen()
        {
            _winService.ShowDialog("RfidWindow");
        }

        public ICommand OpenWinCommand {  get; set; }
    }
}
