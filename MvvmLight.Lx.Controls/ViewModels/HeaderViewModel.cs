using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using MvvmLight.Lx.Admin.IServices;
using MvvmLight.Lx.Controls.Mvvm;
using MvvmLight.Lx.Controls.Shell.Views;
using MvvmLight.Lx.DbAccess.Entitys;
using MvvmLight.Lx.DbAccess.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;

namespace MvvmLight.Lx.Controls.ViewModels
{
    public class HeaderViewModel : BaseViewModel
    {
        private readonly IHeaderService _headerService;
        private readonly IWinDialogService _winDialogService;
        public HeaderViewModel(IUnityContainer container) : base(container)
        {
            _headerService = container.Resolve<IHeaderService>();
            _winDialogService = container.Resolve<IWinDialogService>();
            Initialized();
        }

        public void Initialized()
        {
            CreateWindowFromHeader = _headerService.QueryHeaderList().ToList();
            OpenManualTestCommand = new RelayCommand<string>(ExecuteOpenTest);
        }

        /// <summary>
        /// 通过不同的参数，打开不同的测试Dialog
        /// </summary>
        /// <param name="navigatPath"></param>
        private void ExecuteOpenTest(string navigatPath)
        {
            WindowFactory.Current.CreateWindowToOpen(navigatPath);
            
        }

        private IEnumerable<HeaderInfo> _createWindowFromHeader;

        public IEnumerable<HeaderInfo> CreateWindowFromHeader
        {
            get => _createWindowFromHeader ?? (_createWindowFromHeader = new List<HeaderInfo>());
            set => SetProperty(ref _createWindowFromHeader, value);
        }

        public ICommand OpenManualTestCommand {  get; set; }

    }
}
