using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MvvmLight.Lx.Admin.IServices;
using MvvmLight.Lx.Controls.Mvvm;
using MvvmLight.Lx.Core.Messengers;
using MvvmLight.Lx.Eap.Views;
using Unity;
using HandyOrgWindow=HandyControl.Controls.Window;
using Window = System.Windows.Window;
using HandyOrgMessageBox = HandyControl.Controls.MessageBox;
using System.IO;
using MvvmLight.Lx.DbAccess.TestDemo;
using MvvmLight.Lx.DbAccess.Services;
using MvvmLight.Lx.DbAccess;
using MvvmLight.Lx.DbAccess.Entitys;
using System.Runtime.Remoting.Contexts;
using System.Data.Entity;
using System.Xml.Linq;
using HandyControl.Controls;
using MvvmLight.Lx.Core.Dtos;
namespace MvvmLight.Lx.Eap.ViewModels
{
    public class LoginWindowViewModel : BaseViewModel
    {
        private LoginDto _loginDto;

        public LoginDto LoginDto
        {
            get => _loginDto??(_loginDto=new LoginDto());
            set => SetProperty(ref _loginDto, value);
        }


        private UserInfo _users;

        public UserInfo Users
        {
            get => _users ?? (_users = new UserInfo());
            set => SetProperty(ref _users, value);
        }

        public readonly ILoginService _loginService;
        public readonly IUserService _db;
        public LoginWindowViewModel(IUnityContainer container) : base(container)
        {
            _loginService=container.Resolve<ILoginService>();
            _db=container.Resolve<IUserService>();
            LoginCommand = new RelayCommand<LoginDto>(ExecuteLogin);
            CancelCommand = new RelayCommand<Window>(ExecuteCancel);
           
            InitializedData();
        }

        private void InitializedData()
        {
           var user1= _db.GetUserInfo(4);
           
            var result=  _db.QueryUserList();
            LoginDto.Username = "Admin";
            LoginDto.Password = "123456";
        }

        private void ExecuteCancel(Window win)
        {
            Application.Current.Shutdown();
        }

        private void ExecuteLogin(LoginDto loginDto)
        {
            var activeWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            activeWindow.DialogResult = true;
            HandyOrgMessageBox.Success("登录成功");

         /*   if (IsCanSignIn(loginDto.Username, loginDto.Password)) // 直接调用方法
            {
                var activeWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
                activeWindow.DialogResult = true;
                HandyOrgMessageBox.Success("登录成功");
            }
            else
            {
                HandyOrgMessageBox.Warning("登录失败");
            }*/
        }

      //  public bool IsLoginSucess => IsCanSignIn(UserName,Password);

        public ICommand LoginCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand SavePwdCommand { get; set; }
        public ICommand InitializingCommand { get; set; }
        
        private bool IsCanSignIn(string username, string password) =>!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);
    }
}
