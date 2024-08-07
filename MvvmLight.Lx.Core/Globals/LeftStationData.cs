using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.Core.Globals
{
    public class LeftStationData:ObservableObject
    {
		private string _ct;

		public string CT
		{
			get => _ct;
			set => SetProperty(ref _ct, value);
		}

		private DateTime _runTime;

		public DateTime RunTime
		{
			get => _runTime;
			set => SetProperty(ref _runTime, value);
		}

        private DateTime _faultTime;

        public DateTime FaultTime
        {
            get => _faultTime;
            set => SetProperty(ref _faultTime, value);
        }
    }

    public class RightStationData : ObservableObject
    {
        private string _ct;

        public string CT
        {
            get => _ct;
            set => SetProperty(ref _ct, value);
        }

        private DateTime _runTime;

        public DateTime RunTime
        {
            get => _runTime;
            set => SetProperty(ref _runTime, value);
        }

        private DateTime _faultTime;

        public DateTime FaultTime
        {
            get => _faultTime;
            set => SetProperty(ref _faultTime, value);
        }

    }
}
