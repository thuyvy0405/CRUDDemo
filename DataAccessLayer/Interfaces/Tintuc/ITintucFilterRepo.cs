using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces.Tintuc
{
    public interface ITintucFilterRepo
    {
        Task<IEnumerable<TinTuc>> GetListByKeyWord(string keyword);
        Task<IEnumerable<TinTuc>> GetListByDate(DateTime starDate, DateTime endDate, string? keyword, bool? isApproveed);
        Task<IEnumerable<TinTuc>> GetListUnapproved();
    }
}
