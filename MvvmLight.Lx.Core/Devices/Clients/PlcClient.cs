using MvvmLight.Lx.Core.Devices.Infos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Modbus.Device;
using MvvmLight.Lx.Core.Helpsers;
using NewLife.Configuration;
using MvvmLight.Lx.Core.Globals;
using System.Net.Http;
using System.Threading;
namespace MvvmLight.Lx.Core.Devices.Clients
{
    /// <summary>
    /// 和PLC建立长连接，PLC客户端使用全局单例
    /// </summary>
    public class PlcClient
    {
        /// <summary>
        /// 此实例默认只有一个PLC
        /// </summary>
        /// <returns></returns>
      /*  public IModbusMaster GetSingleClient()
        {
            lock (_lockObject)
            {
                if (CurrentMaster != null)
                {
                    var _provider = new InIConfigProvider { FileName = "Config/setting.ini" };
                    var Ip = _provider["PlcInfo:ServerIp"];
                    var Port = _provider["PlcInfo:ServerPort"];
                    TcpClient tcpClient = new TcpClient();
                    var result = tcpClient.BeginConnect(Ip, int.Parse(Port), null, null);

                    var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(2));          //设置超时时间为2秒
                   // tcpClient.Connect(Ip, int.Parse(Port));

                    SysPlc.IsConn = tcpClient.Connected;
                    SysPlc.PlcState = SysPlc.IsConn ? "PLC已连接" : "PLC未连接";

                    if (tcpClient.Connected)
                    {
                        CurrentMaster = Modbus.Device.ModbusIpMaster.CreateIp(tcpClient);
                    }
                }
            }
            return CurrentMaster;
        }*/

        /// <summary>
        /// 当存在多个PLC实例时
        /// </summary>
        /// <param name="plcId"></param>
        /// <returns></returns>
        public IModbusMaster GetClient(int plcId)
        {
            if (_modbusMasters.ContainsKey(plcId))
            {
                return _modbusMasters[plcId];
            }

            lock (_lockObject)
            {
                if (!_modbusMasters.ContainsKey(plcId))
                {
                    var plcInfo = FindRuningPlc.FirstOrDefault(p => p.Id == plcId);
                    if (plcInfo != null)
                    {
                        TcpClient tcpClient = new TcpClientWithTimeout(plcInfo.IP, int.Parse(plcInfo.Port), 1000).Connect();
                     //   TcpClient tcpClient = new TcpClient();
                        tcpClient.Connect(plcInfo.IP, int.Parse(plcInfo.Port)); // 假设Port也从配置中获取
                        if (tcpClient.Connected)
                        {
                            var modbusMaster = Modbus.Device.ModbusIpMaster.CreateIp(tcpClient);
                            _modbusMasters[plcId] = modbusMaster;
                            return modbusMaster;
                        }
                    }
                }
            }
            return null;
        }

        public PlcBackResult CreateConnectionPLC(int PlcCount)
        {
            // Task plcTask = Task.Factory.StartNew(ExecutePlcTask, PlcCount);

            Task plcTask =new  Task(ExecutePlcTask);                        //读取PLC的数据
            plcTask.Start();
          //  PlcResult = ExecutePlcTask(PlcCount);
            return PlcResult;
        }

        private void ExecutePlcTask()
        {
            if (PlcCount == 1)
            {
                PlcResult = ConnectionSiglePLC();
            }
            else
            {
                PlcResult = ConnectionMultipePLC();
            }
        }

        /// <summary>
        /// 一个PLC
        /// </summary>
        public PlcBackResult ConnectionSiglePLC()
        {
            try
            {
                TcpClient tcpClient = new TcpClientWithTimeout(DefaultPlc.IP, int.Parse(DefaultPlc.Port), 1000).Connect();      //超时时间1秒
                if (!tcpClient.Connected)
                {
                    PlcResult.Message = "连接失败.";
                    PlcResult.IsSuccess = false;
                    return PlcResult;
                }

                // 创建Modbus连接
                PlcResult.CurrentMaster = Modbus.Device.ModbusIpMaster.CreateIp(tcpClient);
                PlcResult.IsSuccess = true;
                PlcResult.Message = "连接成功.";
            }
            catch (Exception ex)
            {
                PlcResult.Message = $"连接失败.{ex.Message}";
                PlcResult.IsSuccess = false;
            }
            return PlcResult;
        }

        /// <summary>
        /// 多个PLC
        /// </summary>
        public PlcBackResult ConnectionMultipePLC()
        {          
            try
            {
                for (int i = 0; i < PlcCount; i++)
                {
                    TcpClient tcpClient = new TcpClient();
                    var result = tcpClient.BeginConnect(FindRuningPlc[i].IP, int.Parse(FindRuningPlc[i].Port), null, null);
                    FindRuningPlc[i].IsConn= result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(2));          //设置超时时间为2秒

                    if (!tcpClient.Connected)
                    {
                        PlcResult.Message = "连接失败.";
                        PlcResult.IsSuccess = false;
                        return PlcResult;
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
                PlcResult.Message = $"连接失败.{ex.Message}";
                PlcResult.IsSuccess = false;
            }
            return PlcResult;
        }

        public int PlcCount { get; set; }           //PLC的个数

        public int SavleId {  get; set; }           //PLC从站ID

        public bool IsConnected { get; set; }       //判断PLC是否连接成功

        public PlcInfo DefaultPlc { get; set; }

        public PlcInfo[] FindRuningPlc { get; set; } 

        public PlcBackResult PlcResult {  get; set; }   //PLC的返回结果

        public static IModbusMaster CurrentMaster;            //1个PLC使用它连接，使用静态全局公用一个单例

        private static readonly Lazy<PlcClient> _clientExtension = new Lazy<PlcClient>(() => new PlcClient());

        public static PlcClient Current => _clientExtension.Value;

        private readonly object _lockObject = new object();

        private readonly Dictionary<int, IModbusMaster> _modbusMasters = new Dictionary<int, IModbusMaster>();

        public readonly Dictionary<int, Func<IModbusMaster>> ModbusMasters = new Dictionary<int, Func<IModbusMaster>>();

        public IEnumerable<PlcInfo> PlcList { get; set; }

    //    public IModbusMaster SingleClient=> GetSingleClient();        //单个PLC

        public IModbusMaster CreateClient=> GetClient(SavleId);         //多个PLC

        public PlcBackResult ConnectionPLC => CreateConnectionPLC(PlcCount);    //创建PLC的连接

        public PlcClient()
        {
            PlcResult = new PlcBackResult();
        }
    }

    public class TcpClientWithTimeout
    {
        protected string _hostname;
        protected int _port;
        protected int _timeout_milliseconds;
        protected TcpClient connection;
        protected bool connected;
        protected Exception exception;

        public TcpClientWithTimeout(string hostname, int port, int timeout_milliseconds)
        {
            _hostname = hostname;
            _port = port;
            _timeout_milliseconds = timeout_milliseconds;
        }
        public TcpClient Connect()
        {
            // kick off the thread that tries to connect
            connected = false;
            exception = null;
            Thread thread = new Thread(new ThreadStart(BeginConnect));
            thread.IsBackground = true; // So that a failed connection attempt 
                                        // wont prevent the process from terminating while it does the long timeout
            thread.Start();

            // wait for either the timeout or the thread to finish
            thread.Join(_timeout_milliseconds);

            if (connected == true)
            {
                // it succeeded, so return the connection
                thread.Abort();
                return connection;
            }
            if (exception != null)
            {
                // it crashed, so return the exception to the caller
                thread.Abort();
                throw exception;
            }
            else
            {
                // if it gets here, it timed out, so abort the thread and throw an exception
                thread.Abort();
                string message = string.Format("TcpClient connection to {0}:{1} timed out",
                  _hostname, _port);
                throw new TimeoutException(message);
            }
        }
        protected void BeginConnect()
        {
            try
            {
                connection = new TcpClient(_hostname, _port);
                // record that it succeeded, for the main thread to return to the caller
                connected = true;
            }
            catch (Exception ex)
            {
                // record the exception for the main thread to re-throw back to the calling code
                exception = ex;
            }
        }
    }
}
