using MvvmLight.Lx.Admin.IServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Unity;

namespace MvvmLight.Lx.Admin.Services
{
    public class WinDialogService : IWinDialogService
    {
        private readonly IUnityContainer _container;

        private readonly ICollection _collection;

        public ICollection FullName {  get { return _collection; } }

        public WinDialogService(IUnityContainer container)
        {
            _container = container;
        }

        public void ShowDialog(string winName)
        {
            ShowDialog(winName,null);
        }

        public void ShowDialog(string winName,Action<MessageBoxResult> result)
        {
            ShowDialogInit(winName, value => { var s= MessageBoxButton.YesNo; });
        }

        private void ShowDialogInit(string winName, Action<MessageBoxResult> result)
        {
            Window window = CreateWindow(winName);
            window.Title =winName;
            window.Show();
        }

        private Window CreateWindow(string winName)
        {
            if (string.IsNullOrWhiteSpace(winName))
            {
                return _container.Resolve<Window>();
            }
            return _container.Resolve<Window>(winName);
        }
    }
}
