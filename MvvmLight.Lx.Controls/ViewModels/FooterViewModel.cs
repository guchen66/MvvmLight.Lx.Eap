using MiniExcelLibs;
using MvvmLight.Lx.Admin.IServices;
using MvvmLight.Lx.Controls.Mvvm;
using MvvmLight.Lx.Core.Devices.Clients;
using MvvmLight.Lx.Core.Devices.Infos;
using MvvmLight.Lx.Core.Globals;
using MvvmLight.Lx.Core.Helpsers;
using Mysqlx.Session;
using MySqlX.XDevAPI;
using MySqlX.XDevAPI.Common;
using NewLife.Configuration;
using NewLife.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Xml.Linq;
using Unity;
using HandyOrgMessageBox = HandyControl.Controls.MessageBox;

namespace MvvmLight.Lx.Controls.ViewModels
{
    /// <summary>
    /// 由于FooterView写的所有设备连接
    /// 那么可以将FooterViewModel当作ConnectionViewModel去处理
    /// </summary>
    public class FooterViewModel : BaseViewModel
    {
        public readonly IPlcService _plcService;
        public IScanCodeService _scanCodeService;

        private ManualResetEvent plcConnEvent = new ManualResetEvent(false);
        private ManualResetEvent ccdConnEvent = new ManualResetEvent(false);
        private ManualResetEvent rfidConnEvent = new ManualResetEvent(false);

        private ObservableCollection<PlcInfo> _obPLC;

        public ObservableCollection<PlcInfo> ObPLC
        {
            get => _obPLC ?? (_obPLC = new ObservableCollection<PlcInfo>());
            set => SetProperty(ref _obPLC, value);
        }

        private ObservableCollection<CCDInfo> _obCCD;

        public ObservableCollection<CCDInfo> ObCCD
        {
            get => _obCCD ?? (_obCCD = new ObservableCollection<CCDInfo>());
            set => SetProperty(ref _obCCD, value);
        }

        public FooterViewModel(IUnityContainer container) : base(container)
        {
            IsVisible = true;
            _plcService = container.Resolve<IPlcService>();
            _scanCodeService = container.Resolve<IScanCodeService>();


            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(500);
            _dispatcherTimer.Tick += OnTick;
            _dispatcherTimer.Start();
            //将ini文件的PLC信息填充进来，如果使用数据库也可以使用数据库填充
            foreach (var plc in IniHelper.MultiplePLC)
            {
                ObPLC.Add(plc);
            }

            ObCCD.Add(new CCDInfo() { IP="127.0.0.1",Port="502"});

            //连接PLC
            SysPlc.PlcCount=PlcClient.Current.PlcCount = IniHelper.Current.GetPath("Config/setting.ini").GetSection("PlcCount").ToInt();
            PlcClient.Current.DefaultPlc = ObPLC.Where(it => it.IP == IPHelper.LocalIP4FromPLC).First(); //如果一个PLC走这条线，用于前期测试，正式发布可以不写DefaultPlc，会自动跟据PlcCount的多少连接
            PlcClient.Current.FindRuningPlc = ObPLC.ToArray(); //如果多个PLC走这条线

            //初始化硬件设备，包含PLC、CCD、RFID、扫码枪、运动控制卡
            InitHardware();

        }

        bool _isAutoConnPlc=true;
        public PlcClient _plcClient=new PlcClient();
        private void InitHardware()
        {

            var tasks = new Task[]
            {
                //后台线程长连接，不取消令牌
                Task.Factory.StartNew(() => InitConnPLC(),CancellationToken.None,TaskCreationOptions.LongRunning,TaskScheduler.Default),
                Task.Factory.StartNew(() => InitConnRFId(),CancellationToken.None,TaskCreationOptions.LongRunning,TaskScheduler.Default),
             //   Task.Factory.StartNew(() => InitConnCCd(),CancellationToken.None,TaskCreationOptions.LongRunning,TaskScheduler.Default),
             //   Task.Factory.StartNew(() => InitScanCode(),CancellationToken.None,TaskCreationOptions.LongRunning,TaskScheduler.Default),
               // Task.Factory.StartNew(() => InitReadData(),CancellationToken.None,TaskCreationOptions.LongRunning,TaskScheduler.Default),
            };
        }

        /// <summary>
        /// 初始化PLC，为了应对不同公司的风格
        /// PLC读取Ini文件
        /// </summary>
        public void InitConnPLC()
        {
            for (int i = 0; i < ObPLC.Count; i++)
            {
                var n = i;
                do
                {
                    if (ObPLC[n].IsConn == false)
                    {
                        PlcInfo.IsConn =SysPlc.IsConn= _plcService.DeviceStartRuning(PlcClient.Current.DefaultPlc).IsSuccess;
                        PlcInfo.PlcName = PlcInfo.IsConn ? "已连接PLC" : "未连接PLC";
                    }

                } while (ObPLC[n].IsConn == false && !plcConnEvent.WaitOne(1500));
            }
        }

        /// <summary>
        /// 初始化RFId，为了应对不同公司的风格
        /// RFId读取json文件
        /// </summary>
        private void InitConnRFId()
        {
            var provider = new JsonConfigProvider()
            {
                FileName = "AppConfig.json"
            };
            RfidInfo rfidInfo = new RfidInfo();
            rfidInfo.IP = provider.GetSection($"RFId:Ip").Value;
            rfidInfo.Port = provider.GetSection($"RFId:Port").Value;
            rfidInfo.RfidType = provider.GetSection($"RFId:RFIdType").Value;
            RfidClient.DefaultRfId = rfidInfo;
           
            do
            {
                if (RfidInfo.IsConn==false)
                {
                    SysRFId.IsConn = RfidInfo.IsConn = RfidClient.Current.ConnectionRFId.IsSuccess;
                    RfidInfo.RfidName = SysRFId.IsConn ? "已连接RFId" : "未连接RFId";
                }

            } while (RfidInfo.IsConn == false && !rfidConnEvent.WaitOne(1500));
         
        }

        /// <summary>
        /// 初始化CCD，为了应对不同公司的风格
        /// CCD读取xml文件
        /// </summary>
        private void InitConnCCd()
        {
            string DefaultFilePath = $"Config/CcdConfig.xml";
            XmlHelper xmlHelper = new XmlHelper(DefaultFilePath);
            CcdClient.Current.CcdCount = xmlHelper.CcdCount();
            CcdClient.Current.FindRunCcd=ObCCD.ToArray();
            var ip= xmlHelper.GetSection("1").GetKey("ip");
            var port = xmlHelper.GetSection("1").GetKey("port");
            while (true)
            {
              //  Task.Delay(100);
                var result = CcdClient.Current.ConnectionCCD;
                SysCcd.IsConn =CCDInfo.IsConn = result.IsSuccess;
/*                Dispatcher.CurrentDispatcher.Invoke(
                      new Action(() =>
                      {
                          SysCcd.IsConn = CCDInfo.IsConn = result.IsSuccess;
                          CCDInfo.CCDName = SysCcd.IsConn ? "已连接CCD" : "未连接CCD";
                      })
                  );*/
            }
        }

        /// <summary>
        /// 初始化扫码枪，为了应对不同公司的风格
        /// 扫码枪读取Excel文件
        /// </summary>
        private void InitScanCode()
        {
            string path = $"Config/ScanCodeConfig.xlsx";           
            var scanCodes = MiniExcel.Query<ScanCodeInfo>(path);
            var ip= scanCodes.Select(x=>x.IP).FirstOrDefault();                          //这里要注意，xslx文件的Header一定要和实体类属性大小写相同
            var port= scanCodes.Select(x=>x.Port).FirstOrDefault();
            while (true)
            {
              //  Task.Delay(100);
                var result = ScanCodeClient.Current.ConnectionScanCode;
                SysScanCode.IsConn=ScanCodeInfo.IsConn  = result.IsSuccess;
            }
        }

        /// <summary>
        /// 初始化运动控制卡，为了应对不同公司的风格
        /// 运动控制卡读取数据库文件
        /// </summary>
        private void InitControlCard()
        {
            while (true) 
            {
                Task.Delay(100);
                var result = ControlCardClient.Current.ConnectionControlCard;
                SysControlCard.IsConn=ControlCardInfo.IsConn  = result.IsSuccess;
            }
         
        }


        private void InitReadData()
        {
            if (SysPlc.IsConn)
            {
                do
                {
                    if (RfidInfo.IsConn == false)
                    {
                        SysRFId.IsConn = RfidInfo.IsConn = RfidClient.Current.ConnectionRFId.IsSuccess;
                        RfidInfo.RfidName = SysRFId.IsConn ? "已连接RFId" : "未连接RFId";
                    }

                } while (RfidInfo.IsConn == false && !rfidConnEvent.WaitOne(1500));
            }

        }
        private void OnTick(object sender, EventArgs e)
        {
            if (SysPlc.IsConn)
            {
                if (PlcInfo.IsConn == true)
                {
                    SysPlc.CT = _plcService.ReadInputRegisters(0,5).Message;
                }
            }

          /*  PlcInfo.PlcName = SysPlc.IsConn ? "已连接PLC" : "未连接PLC";
            RfidInfo.RfidName = SysRFId.IsConn ? "已连接RFId" : "未连接RFId";
            CCDInfo.CCDName = SysCcd.IsConn ? "已连接CCD" : "未连接CCD";
            ScanCodeInfo.ScanCodeName = SysScanCode.IsConn ? "已连接扫码枪" : "未连接扫码枪";
            ControlCardInfo.CardName = SysControlCard.IsConn ? "已连接控制卡" : "未连接控制卡";*/
        }

        private PlcInfo _plcInfo;

        public PlcInfo PlcInfo
        {
            get => _plcInfo??(_plcInfo=new PlcInfo());
            set => SetProperty(ref _plcInfo, value);
        }

        private CCDInfo _ccdInfo;

        public CCDInfo CCDInfo
        {
            get => _ccdInfo ?? (_ccdInfo = new CCDInfo());
            set => SetProperty(ref _ccdInfo, value);
        }


        private RfidInfo _rfidInfo;

        public RfidInfo RfidInfo
        {
            get => _rfidInfo ?? (_rfidInfo = new RfidInfo());
            set => SetProperty(ref _rfidInfo, value);
        }

        private ScanCodeInfo _scanCodeInfo;

        public ScanCodeInfo ScanCodeInfo
        {
            get => _scanCodeInfo ?? (_scanCodeInfo = new ScanCodeInfo());
            set => SetProperty(ref _scanCodeInfo, value);
        }


        private ControlCardInfo _controlCardInfo;

        public ControlCardInfo ControlCardInfo
        {
            get => _controlCardInfo ?? (_controlCardInfo = new ControlCardInfo());
            set => SetProperty(ref _controlCardInfo, value);
        }

        private bool _isVisible;

        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }

        private readonly DispatcherTimer _dispatcherTimer;
        public bool IsBusy {  get; set; }
       
    }
}
