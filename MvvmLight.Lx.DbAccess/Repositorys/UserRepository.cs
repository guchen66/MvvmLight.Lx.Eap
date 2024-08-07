using MvvmLight.Lx.DbAccess.Entitys;
using MvvmLight.Lx.DbAccess.TestDemo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.DbAccess.Repositorys
{
    public interface IUserRepository
    {
        // 增加用户
        void AddUser(UserInfo user);
        Task AddUserAsync(UserInfo user);

        // 删除用户
        void RemoveUser(UserInfo user);
        Task RemoveUserAsync(UserInfo user);

        // 更新用户
        void UpdateUser(UserInfo user);
        Task UpdateUserAsync(UserInfo user);

        // 根据Id查找用户
        UserInfo GetUserById(int id);
        Task<UserInfo> GetUserByIdAsync(int id);

        // 获取所有用户
        IEnumerable<UserInfo> GetAllUsers();
        Task<IEnumerable<UserInfo>> GetAllUsersAsync();
    }
    public class UserRepository : IUserRepository
    {
        private readonly MvvmLightContext _context;
        public UserRepository(MvvmLightContext context)
        {
            _context = context;
        }
        public void AddUser(UserInfo user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public async Task AddUserAsync(UserInfo user)
        {
             _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public void RemoveUser(UserInfo user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public async Task RemoveUserAsync(UserInfo user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public void UpdateUser(UserInfo user)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateUserAsync(UserInfo user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public UserInfo GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        public async Task<UserInfo> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public IEnumerable<UserInfo> GetAllUsers()
        {
           
            return _context.Users.ToList();
        }

        public async Task<IEnumerable<UserInfo>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
