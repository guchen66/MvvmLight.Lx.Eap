using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.Core.Devices.Infos
{
    public class CCDInfo : HardwareBase
    {
        public CCDInfo()
        {
             
        }

        private string _ccdName;

        public string CCDName
        {
            get => _ccdName;
            set => SetProperty(ref _ccdName, value);
        }
    }
}
