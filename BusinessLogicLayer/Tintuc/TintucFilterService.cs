using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces.Tintuc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Tintuc
{
    public class TintucFilterService : ITintucFilterService
    {
        private readonly ITintucFilterRepo _context;


        // hàm khởi tạo có ham số là DBContext
        public TintucFilterService(ITintucFilterRepo context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TinTuc>> GetListByDate(DateTime starDate, DateTime endDate, string? keyword, bool? isApproveed)

        {
            return await _context.GetListByDate(starDate, endDate, keyword, isApproveed);
        }

        public async Task<IEnumerable<TinTuc>> GetListByKeyWord(string keyword)
        {
            return await _context.GetListByKeyWord(keyword);
        }

        public async Task<IEnumerable<TinTuc>> GetListUnapproved()
        {
            return await _context.GetListUnapproved();
        }
    }
}
