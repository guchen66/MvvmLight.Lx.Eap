
using MvvmLight.Lx.Core.Devices.Infos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.Devices.PLC
{
    public interface IDirtPlcClient
    {
        IReadClient Readable<T>();
        PlcInfo Where(Func<PlcInfo, object> value);
        //  void Where(Expression<Func<bool>> expression);
    }
}
