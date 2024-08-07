using MvvmLight.Lx.Admin.IServices;
using MvvmLight.Lx.Core.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MvvmLight.Lx.Admin.Services
{
    public class LoginService : ILoginService
    {
        [SimpleHandlerAttribute]
        public void Login(string username, string password)
        {
            MessageBox.Show("登录成功");
        }

        public void Login(Window win)
        {
            win.DialogResult = true;
        }
    }
}
