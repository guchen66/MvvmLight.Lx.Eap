using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MvvmLight.Lx.Admin.IServices;
using Wesky.Net.OpenTools.Iot.Scanner;
using Wesky.Net.OpenTools.Iot.Scanner.Models;

namespace MvvmLight.Lx.Admin.Services
{
    public class ScanCodeService : IScanCodeService
    {
        public bool ReadCode()
        {
            ICodeReader reader = new CodeReader();
            ReaderClientInfo clientInfo = new ReaderClientInfo(); // 扫码器客户端实例
            clientInfo.Ip = IPAddress.Parse("192.168.250.110"); // 扫码器IP
            clientInfo.Port = 18204; // 扫码器端口号
            clientInfo.Count = 3; // 没扫到码重试次数
            clientInfo.SendTimeOut = 3000; // 请求超时 毫秒
            clientInfo.ReceiveTimeOut = 3000; // 接收扫码内容超时 毫秒
            clientInfo.Brand = "SR"; // 扫码器品牌
            clientInfo.Command = "CMD"; // 扫码器触发指令，指令可通过各个扫码器厂家提供的配置软件，配置为通用的
            clientInfo.ReaderNo = 1; // 扫码器编号，可自定义，例如有10个，就可以配置1-10号
            clientInfo.CloseCommand = ""; // 停止触发指令，如果没有则默认空字符串即可​
            ReaderResultInfo res = reader.ReaderConnection(ref clientInfo); // 通信连接，连接扫码器服务端，参数返回客户端实例 以及 标准返回值类型ReaderResultInfo​
            if (!res.IsSucceed)
            {
                Console.WriteLine($"与扫码器建立通信连接失败:{res.Message}");
                return false;
            }
            res = reader.ReaderRead(ref clientInfo); // 传入扫码器客户端实例，进行扫码。并参数内返回最新的扫码器客户端实例
            if (!res.IsSucceed)
            {
                Console.WriteLine($"扫码异常:{res.Message}");
                return false;
            }
            else
            {
                Console.WriteLine($"扫到码:{res.Value}  扫码耗时:{res.ElapsedMilliseconds}");
            }
            return true;
        }
    }
}
