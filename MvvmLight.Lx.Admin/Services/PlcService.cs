using MvvmLight.Lx.Admin.IServices;
using MvvmLight.Lx.Core.Devices;
using MvvmLight.Lx.Core.Devices.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modbus.Device;
using MvvmLight.Lx.Core.Devices.Infos;
namespace MvvmLight.Lx.Admin.Services
{
    public class PlcService : IPlcService
    {
        public readonly PlcClient _client;
        public readonly IModbusMaster _modbusMaster;
        public PlcService(PlcClient client)
        {
            _client = client;
           // _modbusMaster = modbusMaster;
        }

        public PlcBackResult DeviceStartRuning(PlcInfo plcInfo)
        {
            PlcBackResult plcBackResult = new PlcBackResult()
            {
                IsSuccess = true,
            };
            if (string.IsNullOrWhiteSpace(plcInfo.IP))
            {
                plcBackResult.IsSuccess = false;
                plcBackResult.Message = "PLC地址不存在";
                return plcBackResult;
            }

            try
            {
                _client.DefaultPlc = plcInfo;
                return _client.ConnectionSiglePLC(); ;
            }
            catch (Exception ex)
            {

                plcBackResult.IsSuccess = false;
                plcBackResult.Message = "当前PLC的IP不存在";
                return plcBackResult;
            }
         
        }

     /*   public async Task<PlcBackResult> ReadCoilsAsync(ushort start, ushort end)
        {
            PlcBackResult plcBackResult = new PlcBackResult()
            {
                IsSuccess = true,
            };

            if (_client == null)
            {
                plcBackResult.IsSuccess= false;
                plcBackResult.Message = "客户端未连接";
                return plcBackResult;
            }
            else
            {
                if (_client.GetSingleClient()==null)
                {
                    plcBackResult.IsSuccess = false;
                    plcBackResult.Message = "Modbus未连接";
                    return plcBackResult;
                }
                else
                {
                    var boolResult = await _client.GetSingleClient().ReadCoilsAsync(1, start, end);
                    plcBackResult.IsSuccess = true;                 
                    string result = string.Join("", boolResult.Select(b => b.ToString()));
                    plcBackResult.Message = result;
                    return plcBackResult;
                }
                
            }
        }*/
        public PlcBackResult ReadInputRegisters(ushort start, ushort end)
        {
            PlcBackResult plcBackResult = new PlcBackResult()
            {
                IsSuccess = true,
            };
          //  var s1 = _client.SingleClient;
            var boolResult = _modbusMaster.ReadInputRegisters(1, start, end);
            string result = string.Join("", boolResult.Select(b => b.ToString()));
            plcBackResult.Message = result;
            return plcBackResult;
        }
        public PlcBackResult ReadCoilsBoolArray(ushort start, ushort end)
        {
            PlcBackResult plcBackResult = new PlcBackResult()
            {
                IsSuccess = true,
            };

            var boolResult =PlcClient.CurrentMaster.ReadCoils(1,start,end);
            string result = string.Join("", boolResult.Select(b => b.ToString()));
            plcBackResult.Message = result;
            return plcBackResult;
        }

        public bool ReadHeart(ushort start, ushort end)
        {
            var boolArray= PlcClient.CurrentMaster.ReadCoils(1, start, end);
            return boolArray[0];
        }
    }
}
