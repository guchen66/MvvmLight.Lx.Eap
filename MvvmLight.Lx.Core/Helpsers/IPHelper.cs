using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace MvvmLight.Lx.Core.Helpsers
{
    public class IPHelper
    {
        /// <summary>
        /// 查询所有以太网IP4
        /// </summary>
        public static IEnumerable<IPAddress> GetLocalIP4()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                Console.WriteLine("No Network Available");
            }

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            var ipaddress = host
                .AddressList
                .Where(ip => ip.AddressFamily == AddressFamily.InterNetwork);

            return ipaddress;
        }

        /// <summary>
        /// 启用这个方法必须将PLC与工控机直连，且网口重命名为PLC
        /// </summary>
        public static string QueryableLocalIP4ByIPName()
        {
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            string interfaceName = "PLC";
            foreach (NetworkInterface ni in networkInterfaces)
            {
                // 检查网络接口的名称或描述是否与提供的名称匹配
                if (ni.Name.Equals(interfaceName, StringComparison.OrdinalIgnoreCase) ||
                    (ni.Description != null && ni.Description.Equals(interfaceName, StringComparison.OrdinalIgnoreCase)))
                {
                    IPInterfaceProperties ipProperties = ni.GetIPProperties();
                    foreach (UnicastIPAddressInformation ip in ipProperties.UnicastAddresses)
                    {
                       return ip.Address.ToString();
                    }
                   
                }
            }
            return IPAddress.Loopback.ToString(); // 假设只查找第一个匹配的接口,未找到命名为PLC的网口，程序返回回环地址127.0.0.1
        }

        public static string LocalIP4FromPLC => QueryableLocalIP4ByIPName();
        public static IEnumerable<IPAddress> QueryableLocalIP4 => GetLocalIP4();

    }
}
