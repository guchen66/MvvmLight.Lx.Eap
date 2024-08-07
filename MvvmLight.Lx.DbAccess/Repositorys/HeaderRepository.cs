using MvvmLight.Lx.DbAccess.Entitys;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.DbAccess.Repositorys
{
    public interface IHeaderRepository
    {

        // 获取所有用户
        IEnumerable<HeaderInfo> GetAllHeaders();
    }
    public class HeaderRepository : IHeaderRepository
    {
        private readonly MvvmLightContext _context;
        public HeaderRepository(MvvmLightContext context)
        {
            _context = context;
        }

        public IEnumerable<HeaderInfo> GetAllHeaders()
        {
            return _context.Headers.ToList();
        }
    }
}
