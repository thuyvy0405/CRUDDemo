using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces.Tintuc
{
    public class TintucFilterRepo : ITintucFilterRepo
    {
        private readonly ApplicationDbContext _context;

        // hàm khởi tạo
        public TintucFilterRepo()
        {
            _context = new ApplicationDbContext();
        }
        // hàm khởi tạo có ham số là DBContext
        public TintucFilterRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TinTuc>> GetListByDate(DateTime starDate, DateTime endDate, string? keyword, bool? isApproveed)
        {
           var data = _context.TinTucs.Where(x =>
               x.NgayTao <= starDate &&
               x.NgayUpdate >= endDate ||
               x.TieuDe.Contains(keyword) ||
               x.NoiDung.Contains(keyword) ||
               x.TomTat.Contains(keyword)
           ).AsQueryable();
           return  isApproveed != null ?  data.Where(x => x.TrangThai.Equals(isApproveed)).ToList() :  data.ToList();
        }

        public async Task<IEnumerable<TinTuc>> GetListByKeyWord(string keyword)
        {
            return await _context.TinTucs.Where(x =>
               x.TieuDe.Contains(keyword) ||
               x.NoiDung.Contains(keyword) ||
               x.TomTat.Contains(keyword)
           ).ToListAsync();
        }

        public async Task<IEnumerable<TinTuc>> GetListUnapproved()
        {
            return await _context.TinTucs.Where(x =>
               x.TrangThai == false
           ).ToListAsync();
        }
    }
}
