using MvvmLight.Lx.Core.Devices.Infos;
using NewLife.Common;
using NewLife.Configuration;
using NewLife.Log;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MvvmLight.Lx.Core.Helpsers
{
    public class IniHelper
    {
        public static IniHelper Current=>new IniHelper();

        private NameValueCollection _values;
        private string[] keys;
        private string DefaultPath { get; set; }
        private InIConfigProvider _configProvider;
        public IniHelper()
        {
           // DefaultPath = Directory.GetCurrentDirectory()+ "Config\\Setting.ini";                 //默认读取PLC的路径
        }

        /// <summary>
        /// 假如默认只有一个PLC
        /// </summary>
        public static PlcInfo DefaultPLC => GetSinglePLC();

        public static PlcInfo GetSinglePLC()
        {
            var _provider = new InIConfigProvider { FileName = "Config/setting.ini" };
            PlcInfo plc = new PlcInfo();
            plc.IP = _provider["PlcInfo:ServerIP"];
            plc.PlcName = _provider["PlcInfo:PlcName"];
            plc.Port = _provider["PlcInfo:ServerPort"];
            return plc;
        }

        /// <summary>
        /// 如果ini文件存在多个PLC
        /// </summary>
        public static PlcInfo[] MultiplePLC => GetMultiplePlcByIniFile();

        public static PlcInfo[] GetMultiplePlcByIniFile()
        {
            var _provider = new InIConfigProvider { FileName = "Config/setting.ini" };
            try
            {
                var plcCount = _provider["PLCCount"].ToInt();
                PlcInfo[] plc = new PlcInfo[plcCount];

                for (int i = 0; i < plcCount; i++)
                {
                    plc[i] = new PlcInfo
                    {
                        IP = _provider[$"PlcInfo{i}:ServerIP"],
                        PlcName = _provider[$"PlcInfo{i}:PlcName"],
                        PlcType = _provider[$"PlcInfo{i}:PlcType"],
                        Port = _provider[$"PlcInfo{i}:ServerPort"],
                    };
                }

                return plc;
            }
            catch (Exception ex)
            {
                MessageBox.Show("setting.ini文件不存在");
                return null;
            }
        }

        /// <summary>
        /// 改为List接收
        /// </summary>
        public static IEnumerable<PlcInfo> MultiplePlcList => GetMultiplePlcListByIniFile();

        public static IEnumerable<PlcInfo> GetMultiplePlcListByIniFile()
        {
            var _provider = new InIConfigProvider { FileName = "Config/setting.ini" };
            try
            {
                var plcCount = _provider["PLCCount"].ToInt();
                List<PlcInfo> plcList = new List<PlcInfo>();

                for (int i = 0; i < plcCount; i++)
                {
                    var plc = new PlcInfo
                    {
                        IP = _provider[$"PlcInfo{i}:ServerIP"],
                        PlcName = _provider[$"PlcInfo{i}:PlcName"],
                        PlcType = _provider[$"PlcInfo{i}:PlcType"],
                        Port = _provider[$"PlcInfo{i}:ServerPort"],
                    };
                    plcList.Add(plc);
                }

                return plcList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("setting.ini文件不存在");
                return null;
            }
        }

        public IniHelper GetPath(string path)
        {
            _configProvider = new InIConfigProvider { FileName = $"{path}" };
            return this;
        }

        public string GetSection(string section)
        {
           // var s1 = _configProvider.GetSection("PlcInfo0").Childs.FirstOrDefault(x => x.Key == "IsAutoConnPlc");
           return _configProvider[section];
          
        }

        // 链式调用：GetKey 方法现在返回 string 类型
        public string GetKey(string key)
        {
            if (_values != null && _values.AllKeys.Contains(key))
            {
                return _values[key]; // 返回找到的键对应的值
            }
            return null; // 如果键不存在，返回null
        }
        public static void Read()
        {
            var _provider = new InIConfigProvider { FileName = "Config/setting.ini" };


            var s1 = _provider["Debug"];
            var s2=_provider.GetSection("Debug").Comment;
            var s3=_provider.GetSection("Sys").Comment;
            var s4=_provider.GetSection("PlcInfo:ServerIP").Comment;
            var s5 = _provider["PlcInfo:ServerPort"];
           

            var prv2 = new InIConfigProvider { FileName = (_provider as FileConfigProvider).FileName };
            var set2 = prv2.Load<ConfigModel>();
            
            var s6=set2.Debug;
            var s7=set2.LogLevel;
            var s8=set2.LogPath;
            var s9=set2.NetworkLog;
            var s10=set2.Debug;
          
        }


       

       
    }

    public class ConfigModel
    {
        public string Debug { get; set; }
        public string LogLevel { get; set; }
        public string LogPath { get; set; }
        public string NetworkLog { get; set; }
        public string LogFileFormat { get; set; }
    }
}
