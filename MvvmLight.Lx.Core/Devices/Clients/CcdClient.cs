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
    public class CcdClient
    {
        public int CcdCount { get; set; }

        public CCDBackResult CCDResult { get; set; }   //CCD的返回结果

        public CCDInfo[] FindRunCcd { get; set; }

        private static readonly Lazy<CcdClient> _clientExtension = new Lazy<CcdClient>(() => new CcdClient());

        public static CcdClient Current => _clientExtension.Value;

        public CCDBackResult ConnectionCCD => CreateConnectionCCD(CcdCount);    //创建CCD的连接
        public readonly Dictionary<int, Func<IModbusMaster>> ModbusMasters = new Dictionary<int, Func<IModbusMaster>>();
        public CcdClient()
        {
            CCDResult=new CCDBackResult();
        }

        /// <summary>
        /// PLC当时是用来测试的，
        /// 这个直接当作多个相机模拟
        /// </summary>
        /// <param name="CcdCount"></param>
        /// <returns></returns>
        public CCDBackResult CreateConnectionCCD(int CcdCount)
        {
           /* try
            {
                for (int i = 0; i < CcdCount; i++)
                {
                    TcpClient tcpClient = new TcpClient();
                    var result = tcpClient.BeginConnect(FindRunCcd[i].IP, int.Parse(FindRunCcd[i].Port), null, null);
                    FindRunCcd[i].IsConn = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(2));          //设置超时时间为2秒

                    if (!tcpClient.Connected)
                    {
                        CCDResult.Message = "连接失败.";
                        CCDResult.IsSuccess = false;
                        return CCDResult;
                    }

                    ModbusMasters[i] = (() =>
                    {
                        var modbusResult = ModbusIpMaster.CreateIp(tcpClient);
                        return modbusResult;
                    });
                }
            }
            catch (Exception ex)
            {
                CCDResult.Message = $"连接失败.{ex.Message}";
                CCDResult.IsSuccess = false;
            }*/
            return CCDResult;
        }
    }
}
