using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using MvvmLight.Lx.Admin.IServices;
using MvvmLight.Lx.Controls.Mvvm;
using MvvmLight.Lx.Core.Devices;
using MvvmLight.Lx.Core.Devices.Clients;
using MvvmLight.Lx.Core.Devices.Infos;
using MvvmLight.Lx.Core.Globals;
using MvvmLight.Lx.Core.Helpsers;
using MySqlX.XDevAPI;
using MySqlX.XDevAPI.Common;
using Unity;
using HandyOrgMessageBox = HandyControl.Controls.MessageBox;

namespace MvvmLight.Lx.Controls.Shell.ViewModels
{
    public class PlcDebugerWindowViewModel : BaseViewModel
    {
        private string _ct;

        public string CT
        {
            get => _ct;
            set => SetProperty(ref _ct, value);
        }

        private PlcClient _client;

        public PlcClient Client
        {
            get => _client ?? (_client = new PlcClient());
            set => SetProperty(ref _client, value);
        }
        private PlcInfo _selectItemPLC;

        public PlcInfo SelectItemPLC
        {
            get => _selectItemPLC ?? (_selectItemPLC = new PlcInfo());
            set => SetProperty(ref _selectItemPLC, value);
        }

        private ObservableCollection<PlcInfo> _obPLC;

        public ObservableCollection<PlcInfo> ObPLC
        {
            get => _obPLC ?? (_obPLC = new ObservableCollection<PlcInfo>());
            set => SetProperty(ref _obPLC, value);
        }

        public readonly IPlcService _plcService;

        public PlcDebugerWindowViewModel(IUnityContainer container): base(container)
        {
            _plcService = container.Resolve<IPlcService>();
            ConnectionPlcCommand = new RelayCommand<object>(ExecuteConnSinglePLC);
        //    _isAutoConnPlc = Convert.ToBoolean(IniHelper.Current.GetPath("Config/setting.ini").GetSection("IsAutoConnPlc")); //是否自动连接PLC ，读取指定文件中的key，获取value,
         //   ObPLC = GetAllPlc();
         //   GetData();
            
        //    InitHardWare();
        }

        private void InitHardWare()
        {
            var tasks = new Task[]
            {
                //后台线程长连接，不取消令牌
                Task.Factory.StartNew(() => InitConnPLC(),CancellationToken.None,TaskCreationOptions.LongRunning,TaskScheduler.Default),
                Task.Factory.StartNew(() => InitConnRFId(),CancellationToken.None,TaskCreationOptions.LongRunning,TaskScheduler.Default),
                Task.Factory.StartNew(() => 
                {
                   /* Client = new PlcClient()
                    {
                        PlcCount = SysPlc.PlcCount =IniHelper.Current.GetPath("Config/setting.ini").GetSection("PlcCount").ToInt(),
                        DefaultPlc = ObPLC.Where(it => it.IP == IPHelper.LocalIP4FromPLC).First(), //如果一个PLC走这条线，用于前期测试，正式发布可以不写DefaultPlc，会自动跟据PlcCount的多少连接
                        PlcArray = ObPLC.ToArray(), //如果多个PLC走这条线
                    };
                   
                    var result = Client.ConnectionPLC;
                    if (result.IsSuccess)
                    {
                        Dispatcher.CurrentDispatcher.Invoke(
                            new Action(() =>
                            {
                                SysPlc.IsCon = SelectItemPLC.IsConn = result.IsSuccess;
                            })
                        );
                    }*/
                },CancellationToken.None,TaskCreationOptions.LongRunning,TaskScheduler.Default),
            };
            Task.Factory.ContinueWhenAll(tasks,completedTasks =>
            {
                // 当所有任务完成时的逻辑
                foreach (var task in completedTasks)
                {
                    if (task.IsFaulted)
                    {
                        // 处理任务异常
                       Dispatcher.CurrentDispatcher.Invoke(new Action(() =>
                       {
                           HandyOrgMessageBox.Info("Task。PLC连接失败");
                       }));
                    }
                    // 其他所有任务完成时的逻辑
                }
            });
        }

        private void InitConnRFId()
        {
            
        }

        private void InitConnCCd()
        {
            
        }

        private void GetData()
        {
         
            Task task = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    var result = _plcService.ReadCoilsBoolArray(0, 5);
                    CT = result.Message;
                }
            });
        }

        public void InitConnPLC()
        {
          /*  Client = new PlcClient()
            {
                PlcCount = SysPlc.PlcCount =IniHelper.Current.GetPath("Config/setting.ini").GetSection("PlcCount").ToInt(),
                DefaultPlc = ObPLC.Where(it => it.IP == IPHelper.LocalIP4FromPLC).First(), //如果一个PLC走这条线，用于前期测试，正式发布可以不写DefaultPlc，会自动跟据PlcCount的多少连接
                PlcArray = ObPLC.ToArray(), //如果多个PLC走这条线
            };*/
            var Client1=PlcClient.Current;

            Client1.PlcCount = SysPlc.PlcCount = IniHelper.Current.GetPath("Config/setting.ini").GetSection("PlcCount").ToInt();
            Client1.DefaultPlc = ObPLC.Where(it => it.IP == IPHelper.LocalIP4FromPLC).First(); //如果一个PLC走这条线，用于前期测试，正式发布可以不写DefaultPlc，会自动跟据PlcCount的多少连接
            Client1.FindRuningPlc = ObPLC.ToArray(); //如果多个PLC走这条线
            var result = Client.ConnectionPLC;
            if (result.IsSuccess)
            {
                Dispatcher.CurrentDispatcher.Invoke(
                    new Action(() =>
                    {
                        SysPlc.IsConn = SelectItemPLC.IsConn = result.IsSuccess;
                    })
                );
            }
        }

        private ObservableCollection<PlcInfo> GetAllPlc()
        {
            ObservableCollection<PlcInfo> plcInfos = new ObservableCollection<PlcInfo>();
            foreach (var plc in IniHelper.MultiplePLC)
            {
                plcInfos.Add(plc);
            }
            return plcInfos;
        }

        private bool _isConn;

        public bool IsConn
        {
            get => _isConn;
            set => SetProperty(ref _isConn, value);
        }

        private bool _isAutoConnPlc = true;

        /// <summary>
        /// 单个连接
        /// </summary>
        public void ExecuteConnSinglePLC(object obj)
        {
            if (_isAutoConnPlc)
            {
                var plc = obj as PlcInfo;
                if (plc != null && plc.IsConn == false)
                {
                    do
                    {
                        if (!SysPlc.IsSwitchFrom)
                        {
                            SelectItemPLC = ObPLC
                                .Where(it => it.IP == IPHelper.LocalIP4FromPLC)
                                .FirstOrDefault();
                            PlcBackResult plcBackResult = _plcService.DeviceStartRuning(
                                SelectItemPLC
                            );
                            SelectItemPLC.IsConn = plcBackResult.IsSuccess;
                            if (!plcBackResult.IsSuccess)
                                HandyOrgMessageBox.Error(plcBackResult.Message);
                        }
                    } while (false);
                }
            }
        }

        public ICommand ConnectionPlcCommand { get; set; }
    }
}
