using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.Devices.PLC.Repositorys
{

    public interface IPlcRepository<TEntity> where TEntity : class,new()
    {
        IDirtPlcClient Context { get; }
    }

    public class PlcRepository<TEntity>: IPlcRepository<TEntity> where TEntity : class,new()
    {
        public PlcRepository(IDirtPlcClient client=null)
        {
            client=Context;
        }

        public IDirtPlcClient Context => PlcDirted.Client;
    }

    public class PlcDirted 
    {
        public static DirtPlcClient Client = GetClient();
        public static DirtPlcClient GetClient()
        {
            DirtPlcClient client= new DirtPlcClient();
            if (client == null)
            {

            }
            return client;
        }
    }
}
