using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using HslCommunication.Profinet.Omron;
using HslCommunication.Profinet.Siemens;
using Modbus.Device;
using MvvmLight.Lx.Core.Devices.Infos;
using MvvmLight.Lx.Core.Globals;
using NewLife.Configuration;

namespace MvvmLight.Lx.Core.Devices.Clients
{
    /// <summary>
    /// 使用HSL-模拟测试
    /// </summary>
    public class RfidClient
    {
        private SiemensS7Net Siemens;
        private OmronFinsNet OmronFins;
        public static RfidInfo DefaultRfId { get; set; }
        public RFIdBackResult RFIdBackResult { get; set; }

        public RFIdBackResult ConnectionRFId => CreateConnectionRFId(DefaultRfId);

        private static readonly Lazy<RfidClient> _clientExtension = new Lazy<RfidClient>(() => new RfidClient());

        public static RfidClient Current => _clientExtension.Value;

        public int RFIdCount;

        private RFIdBackResult CreateConnectionRFId(RfidInfo rfidInfo)
        {
            switch (rfidInfo.RfidType)
            {
                case "西门子":
                    RFIdBackResult= ConnectionSiement(rfidInfo);
                    break;
                case "欧姆龙":
                    RFIdBackResult=ConnectionOmronFins(rfidInfo);
                    break;
                default:
                    break;
            }
            return RFIdBackResult;
        }


        public RFIdBackResult ConnectionSiement(RfidInfo rfidInfo)
        {
            Siemens.IpAddress = rfidInfo.IP;
            Siemens.Port = rfidInfo.Port.ToInt();
            //   Siemens.SetPersistentConnection();      V11版本弃用
            var result = Siemens.ConnectServer();

            RFIdBackResult.IsSuccess = result.IsSuccess;
            return RFIdBackResult;
        }

        public RFIdBackResult ConnectionOmronFins(RfidInfo rfidInfo)
        {
            return RFIdBackResult;
        }
        public RfidClient()
        {
            RFIdBackResult = new RFIdBackResult();
            Siemens = new SiemensS7Net(SiemensPLCS.S1500)
            {
                ConnectTimeOut = 5000,
                ReceiveTimeOut = 5000
            };

            OmronFins = new OmronFinsNet();
        }
    }
}
