using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmLight.Lx.Core.Devices.Infos;
namespace MvvmLight.Lx.Devices.PLC
{
    public class DirtPlcClient:IDirtPlcClient
    {
        private DirtPlcProvider _Context = null;

        private DirtPlcProvider Context => GetContext();

        private DirtPlcProvider GetContext()
        {
            DirtPlcProvider provider = new DirtPlcProvider();
          //  provider = Synchronization();
            if (provider.Client == null)
            {
                provider.Client = this;
            }
            return provider;
        }

        private ConnectionPlcConfig _CurrentConnectionConfig;

        internal bool IsSingle { get; set; }

        public IReadClient Readable<T>()
        {
            return Context.Readable<T>();
        }

        public PlcInfo Where(Func<PlcInfo, object> value)
        {
            PlcInfo plcInfo = new PlcInfo();
            plcInfo.Id = 2;
            plcInfo.PlcName = "西门子2";
            plcInfo.IP = "192.168.0.1";
            return plcInfo;
        }

        /*  public PlcActionType PlcActionType
          {
              get=>Context
          }

          public DirtPlcProvider Context => GetContext();

          private DirtPlcProvider GetContext()
          {
              DirtPlcProvider dirtPlcProvider = null;
              dirtPlcProvider = Synchronization();
              if (dirtPlcProvider.Root == null)
              {
                  dirtPlcProvider.Root = this;
              }

              return dirtPlcProvider;
          }*/

    }
}
