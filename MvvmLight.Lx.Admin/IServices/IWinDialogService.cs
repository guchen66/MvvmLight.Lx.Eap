using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MvvmLight.Lx.Admin.IServices
{
    public interface IWinDialogService
    {
        ICollection FullName { get; }

        void ShowDialog(string winName);
    }
}
