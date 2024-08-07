using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Modbus.Device;
using MvvmLight.Lx.Core.Devices.Infos;
using MvvmLight.Lx.Core.Globals;
using NewLife.Configuration;

namespace MvvmLight.Lx.Core.Devices.Clients
{
    public class ScanCodeClient 
    {
        public ScanCodeBackResult ScanCodeBackResult { get; set; }

        public ScanCodeBackResult ConnectionScanCode => CreateConnectionScanCode(ScanCodeCount);

        private static readonly Lazy<ScanCodeClient> _clientExtension = new Lazy<ScanCodeClient>(() => new ScanCodeClient());

        public static ScanCodeClient Current => _clientExtension.Value;

        public int ScanCodeCount;

        private ScanCodeBackResult CreateConnectionScanCode(int scancodeCount)
        {
            ScanCodeBackResult.IsSuccess = true;
            return ScanCodeBackResult;
        }

        public ScanCodeClient()
        {
            ScanCodeBackResult = new ScanCodeBackResult();
        }
    }
}
