using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HandyOrgWindow= HandyControl.Controls.Window;
using MvvmLight.Lx.Admin.IServices;
using MvvmLight.Lx.Controls.Mvvm;
using MvvmLight.Lx.Core.Messengers;
using MvvmLight.Lx.Devices.PLC.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;
using MvvmLight.Lx.Controls.Shell.Views;
using HandyControl.Controls;
using MvvmLight.Lx.Core.Devices.Infos;

namespace MvvmLight.Lx.Eap.ViewModels
{
    public class MainWindowViewModel: BaseViewModel
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private readonly IPlcRepository<PlcInfo> _plcRepository;
        public MainWindowViewModel(IUnityContainer container):base(container)
        {
         //   _plcRepository=container.Resolve<IPlcRepository<PlcInfo>>();
            Name = "123";

            //   WeakReferenceMessenger.Default.Register<LoginArgs>(this, HandleLoginMessage);
            //   Method();

            OpenRfidConnCommand = new RelayCommand(ExecuteOpenRfid);
            OpenMsgCommand = new RelayCommand(ExecuteMsg);
        }

        public void ExecuteMsg()
        {
            MessageBox.Show("123");
        }

        private void ExecuteOpenRfid()
        {
            RfidWindow window = new RfidWindow();
            window.Show();
        }

        public ICommand OpenRfidConnCommand {  get; set; }
        public ICommand OpenMsgCommand { get; set; }
        private void HandleLoginMessage(object recipient, LoginArgs message)
        {
            
        }

        public void Method()
        {
           // PlcInfo model = _plcRepository.Context.Readable<PlcInfo>().Where(x=>x.Address="192.168.0.1");
        }

    }
}
