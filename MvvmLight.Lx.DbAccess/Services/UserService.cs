using MvvmLight.Lx.DbAccess.Entitys;
using MvvmLight.Lx.DbAccess.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.DbAccess.Services
{
    public interface IUserService
    {
        IEnumerable<UserInfo> QueryUserList();
        UserInfo GetUserInfo(int id);
        void AddUser(UserInfo user);
    }
    public class UserService : IUserService
    {
        public readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void AddUser(UserInfo user)
        {
            _userRepository.AddUser(user);
        }

        public UserInfo GetUserInfo(int id)
        {
           return _userRepository.GetUserById(id);
        }

        public IEnumerable<UserInfo> QueryUserList()
        {
            return _userRepository.GetAllUsers();
        }


    }
}
