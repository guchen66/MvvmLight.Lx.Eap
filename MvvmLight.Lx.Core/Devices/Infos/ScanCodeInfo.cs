using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.Core.Devices.Infos
{
    public class ScanCodeInfo : HardwareBase
    {
        public ScanCodeInfo()
        {
           
        }

        private string _scanCodeName;

        public string ScanCodeName
        {
            get => _scanCodeName;
            set => SetProperty(ref _scanCodeName, value);
        }

    }
}
