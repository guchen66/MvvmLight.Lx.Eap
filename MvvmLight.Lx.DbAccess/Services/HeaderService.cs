using MvvmLight.Lx.DbAccess.Entitys;
using MvvmLight.Lx.DbAccess.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.DbAccess.Services
{
      public interface IHeaderService
    {
        IEnumerable<HeaderInfo> QueryHeaderList();

    }
    public class HeaderService : IHeaderService
    {
        public readonly IHeaderRepository _repository;
        public HeaderService(IHeaderRepository repository)
        {
           _repository = repository;
        }

        public IEnumerable<HeaderInfo> QueryHeaderList()
        {
            return _repository.GetAllHeaders();
        }


    }
}
