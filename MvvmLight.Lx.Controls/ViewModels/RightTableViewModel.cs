using MvvmLight.Lx.Admin.IServices;
using MvvmLight.Lx.Controls.Mvvm;
using MvvmLight.Lx.Core.Globals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace MvvmLight.Lx.Controls.ViewModels
{
    public class RightTableViewModel : BaseViewModel
    {
        private LeftStationData _leftStation;

        public LeftStationData LeftStation
        {
            get => _leftStation??(_leftStation=new LeftStationData());
            set => SetProperty(ref _leftStation, value);
        }

        private RightStationData _rightStation;

        public RightStationData RightStation
        {
            get => _rightStation??(_rightStation=new RightStationData());
            set => SetProperty(ref _rightStation, value);
        }

        private readonly IPlcService _plcService;
        public RightTableViewModel(IUnityContainer container) : base(container)
        {
            _plcService = container.Resolve<IPlcService>();
            Initialized();
        }

        private void Initialized()
        {
            LeftStation .CT= SysPlc.CT;
        }
    }
}
