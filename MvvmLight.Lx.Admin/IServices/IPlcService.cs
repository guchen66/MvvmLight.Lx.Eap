using MvvmLight.Lx.Core.Devices;
using MvvmLight.Lx.Core.Devices.Clients;
using MvvmLight.Lx.Core.Devices.Infos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.Admin.IServices
{
    public interface IPlcService
    {
        PlcBackResult DeviceStartRuning(PlcInfo plcInfo);
        //读取01 输出线圈
     //   Task<PlcBackResult> ReadCoilsAsync(ushort start, ushort end);
        PlcBackResult ReadInputRegisters(ushort start, ushort end);
        PlcBackResult ReadCoilsBoolArray(ushort start, ushort end);

        //读取02 输入线圈
        /*   Task<ApiResponse> ReadInputsAsync(IDeviceData deviceInfo, ushort start, ushort end);

           //读取01 输出寄存器
           Task<ApiResponse> ReadHoldingRegistersAsync(IDeviceData deviceInfo, ushort start, ushort end);

           //读取01 输入寄存器
           Task<ApiResponse> ReadInputRegistersAsync(IDeviceData deviceInfo, ushort start, ushort end);

           //写入01 单个线圈
           Task WriteCoilsAsync(IDeviceData deviceInfo, bool value);*/

        bool ReadHeart(ushort start, ushort end);
    }
}
