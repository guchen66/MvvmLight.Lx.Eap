using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MvvmLight.Lx.Core.Devices.Infos
{
    public abstract class HardwareBase: ObservableObject
    {
        // 假设所有硬件设备都有一个ID和连接状态
        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _ip;
        public string IP
        {
            get => _ip;
            set => SetProperty(ref _ip, value);
        }

        private string _port;

        public string Port
        {
            get => _port;
            set => SetProperty(ref _port, value);
        }

        private bool _isConn;
        public bool IsConn
        {
            get => _isConn;
            set 
            { 
                SetProperty(ref _isConn, value); 
             /*   if (value)
                {
                    BackColor = "Lime";
                }else 
                {
                    BackColor= "Red";
                }*/
            }
        }

        private string _backColor;

        public string BackColor
        {
            get => _backColor;
            set => SetProperty(ref _backColor, value);
        }

        // 如果有通用的方法或属性，可以在这里定义
        // 例如，一个连接或断开连接的方法
        //  public abstract void Connect();
        //  public abstract void Disconnect();

        // 构造函数，可以在子类中调用
        protected HardwareBase()
        {
            // 初始化代码
        }
    }
}
