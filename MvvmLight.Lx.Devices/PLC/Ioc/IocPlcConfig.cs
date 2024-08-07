using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.Devices.PLC.Ioc
{
    public class IocPlcConfig
    {
        public dynamic ConfigId { get; set; }

        public IocPlcType PlcType { get; set; }             //PLC的类型

        public string ConnectionPlc {  get; set; }          //连接Plc的对象

        public bool IsAutoConnection {  get; set; }          //是否自动连接
    }
}
