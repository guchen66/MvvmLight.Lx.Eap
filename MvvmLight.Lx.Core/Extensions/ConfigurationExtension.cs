using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.Core.Extensions
{
    /// <summary>
    /// 读取默认config，要求WPF默认的App.Config
    /// 具有<Man>
    ///	<add key = "老大" value="曹操" />
    ///	<add key = "老二" value="典韦" />
    ///	<add key = "老三" value="郭嘉" />
    /// </Man>
    /// <section name="Man" type="System.Configuration.DictionarySectionHandler"/>
    /// </summary>
    public class ConfigurationExtension
    {
        public void ReadDefaultConfig()
        {
            IDictionary dict = (IDictionary)ConfigurationManager.GetSection("Man");
        }
    }
}
