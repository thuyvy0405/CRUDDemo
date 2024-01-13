using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Tintuc
{
    public interface ITintucFilterService
    {
        Task<IEnumerable<TinTuc>> GetListByKeyWord(string keyword);
        Task<IEnumerable<TinTuc>> GetListByDate(DateTime starDate, DateTime endDate, string? keyword, bool? isApproveed);
        Task<IEnumerable<TinTuc>> GetListUnapproved();
    }
}
