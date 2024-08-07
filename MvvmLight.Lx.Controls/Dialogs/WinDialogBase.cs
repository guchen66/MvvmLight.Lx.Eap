using MvvmLight.Lx.Controls.Prisms;
using Mysqlx;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MvvmLight.Lx.Controls.Dialogs
{
    public class WinDialogBase
    {
        public IRegionManager _regionManager;
        public WinDialogBase(IRegionManager regionManager)
        {

            _regionManager = regionManager;

        }
        public void Usage()
        {
            _regionManager.RequestNavigate("", args =>
            {
                Dispatcher.CurrentDispatcher.Invoke(new Action(() => { }));
            });
        }
    }

    public interface IRegionManager
    {
        void RequestNavigate(string regionName,  Action<NavigationResult> navigationCallback);
    }

    public class RegionManager : IRegionManager
    {
        public void RequestNavigate(string regionName,  Action<NavigationResult> navigationCallback)
        {
            throw new NotImplementedException();
        }
    }
}
