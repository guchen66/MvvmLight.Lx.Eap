using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MvvmLight.Lx.Core.Devices.Infos
{
    public class RfidInfo : HardwareBase
    {
        public RfidInfo()
        {
           
        }


        private string _rfidType;

        public string RfidType
        {
            get => _rfidType;
            set => SetProperty(ref _rfidType, value);
        }

        private string _rfidName;

        public string RfidName
        {
            get => _rfidName;
            set => SetProperty(ref _rfidName, value);
        }

    }
}
