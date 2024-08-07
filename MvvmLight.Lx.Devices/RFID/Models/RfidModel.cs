using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.Devices.RFID.Models
{
    public class RfidModel : ObservableObject
    {
        private string _ip;

        public string IP
        {
            get => _ip;
            set => SetProperty(ref _ip, value);
        }

        private string _address;

        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        private string _port;

        public string Port
        {
            get => _port;
            set => SetProperty(ref _port, value);
        }

        public RfidModel()
        {
            this.IP = "127.0.0.1";
            this.Port = "8970";
        }
    }
}
