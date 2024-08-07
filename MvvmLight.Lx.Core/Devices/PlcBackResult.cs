using Modbus.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.Core.Devices
{
    public class PlcBackResult
    {

        public bool IsSuccess { get; set; } = true;

        public string Message { get; set; }

        public IModbusMaster CurrentMaster {  get; set; }
    }

    public class CCDBackResult
    {
        public bool IsSuccess { get; set; } = true;

        public string Message;
    }

    public class RFIdBackResult
    {
        public bool IsSuccess { get; set; } = true;

        public string Message;
    }

    public class ScanCodeBackResult
    {
        public bool IsSuccess { get; set; } = true;

        public string Message;
    }

    public class ControlCardBackResult
    {
        public bool IsSuccess { get; set; } = true;

        public string Message;
    }
}
