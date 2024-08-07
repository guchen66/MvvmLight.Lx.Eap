using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MvvmLight.Lx.Admin.IServices
{
    public interface ILoginService
    {
         void Login(string username, string password);
         void Login(Window win);
    }
}
