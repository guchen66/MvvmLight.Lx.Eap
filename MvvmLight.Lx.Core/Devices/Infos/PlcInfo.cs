using CommunityToolkit.Mvvm.ComponentModel;
using MvvmLight.Lx.Core.Globals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.Core.Devices.Infos
{
    /// <summary>
    /// PLC的基础信息
    /// 如果View以实例去绑定，那么构造器进行静态的赋值
    /// </summary>
    public class PlcInfo: HardwareBase
    {
        public PlcInfo()
        {
           
        }

        private string _plcType;

        public string PlcType
        {
            get => _plcType;
            set => SetProperty(ref _plcType, value);
        }

        private string _plcName;

		public string PlcName
		{
			get => _plcName;
			set => SetProperty(ref _plcName, value);
		}

 

     
    }
}
