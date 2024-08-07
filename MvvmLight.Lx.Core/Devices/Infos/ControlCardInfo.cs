using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.Core.Devices.Infos
{
   public class ControlCardInfo : HardwareBase
    {
        public ControlCardInfo()
        {
           
        }

        private string _cardName;

        public string CardName
        {
            get => _cardName;
            set => SetProperty(ref _cardName, value);
        }
    }
}
