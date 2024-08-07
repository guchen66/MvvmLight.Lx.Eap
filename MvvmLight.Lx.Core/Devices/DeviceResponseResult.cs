using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.Core.Devices
{
   public class DeviceResponseResult
    {
        public string Message { get; set; }

        public bool Status { get; set; }

        public object Result { get; set; }

        public DeviceResponseResult(string message, bool status = false)
        {
            Message = message;
            Status = status;
        }

        public DeviceResponseResult(bool status, object result)
        {
            Status = status;
            Result = result;
        }
    }

    public class DeviceResponseResult<T>
    {
        public string Message { get; set; }

        public bool Status { get; set; }

        public T Result { get; set; }

        public DeviceResponseResult(string message, bool status = false)
        {
            Message = message;
            Status = status;
        }

        public DeviceResponseResult(bool status, T result)
        {
            Status = status;
            Result = result;
        }
    }
}
