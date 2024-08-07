using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.Core.Globals
{
    public static class SysPlc
    {
        public static bool IsSwitchFrom {  get; set; }           //PLC是否直连工控机还是走交换机
        public static bool IsConn { get; set; } 
        public static string PlcState { get; set; }
        public static string PlcName { get; set; }
        public static string PlcType { get; set; }
        public static int PlcCount {  get; set; }
        public static string Heart { get; set; }                //PLC的心跳，实时检测PLC是否与上位机连接
        public static string CT { get;set; }

    }

    public static class SysCcd
    {
        public static bool IsSwitchFrom { get; set; }           
        public static bool IsConn { get; set; }
        public static string CcdState { get; set; }
        public static string CcdName { get; set; }
        public static string CcdType { get; set; }
        public static int CcdCount { get; set; }
        public static string Heart { get; set; }                
    }

    public static class SysRFId
    {
        public static bool IsSwitchFrom { get; set; }
        public static bool IsConn { get; set; }
        public static string RFIdState { get; set; }
        public static string RFIdName { get; set; }
        public static string RFIdType { get; set; }
        public static int RFIdCount { get; set; }
        public static string Heart { get; set; }
    }

    public static class SysScanCode
    {
        public static bool IsSwitchFrom { get; set; }
        public static bool IsConn { get; set; }
        public static string ScanCodeState { get; set; }
        public static string ScanCodeName { get; set; }
        public static string ScanCodeType { get; set; }
        public static int ScanCodeCount { get; set; }
        public static string Heart { get; set; }
    }


    public static class SysControlCard
    {
        public static bool IsSwitchFrom { get; set; }
        public static bool IsConn { get; set; }
        public static string ControlCardState { get; set; }
        public static string ControlCardName { get; set; }
        public static string ControlCardType { get; set; }
        public static int ControlCardCount { get; set; }
        public static string Heart { get; set; }
    }
}
