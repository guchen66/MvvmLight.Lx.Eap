using Modbus.Device;
using MvvmLight.Lx.Core.Devices.Infos;
using MvvmLight.Lx.Core.Globals;
using NewLife.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.Core.Devices.Clients
{
    public class ControlCardClient
    {
        public ControlCardBackResult ControlCardBackResult { get; set; }

        public ControlCardBackResult ConnectionControlCard => CreateConnectionControlCard(ControlCardCount);

        private static readonly Lazy<ControlCardClient> _clientExtension = new Lazy<ControlCardClient>(() => new ControlCardClient());

        public static ControlCardClient Current => _clientExtension.Value;

        public int ControlCardCount;

        private ControlCardBackResult CreateConnectionControlCard(int scancodeCount)
        {
            ControlCardBackResult.IsSuccess = true;
            return ControlCardBackResult;
        }

        public ControlCardClient()
        {
            ControlCardBackResult = new ControlCardBackResult();
        }
    }
}
