using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace DataAccessLayer.Interfaces.Danhmuc
{
    public class DanhmucRepo:IDanhmucRepo
    {
        private readonly ApplicationDbContext _context;
        // hàm khởi tạo
        public DanhmucRepo()
        {
            _context = new ApplicationDbContext();
        }
        // hàm khởi tạo có ham số là DBContext
        public DanhmucRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Delete(int IDDanhMuc)
        {
            //select idtintuc
            DanhMuc danhmuc = _context.DanhMucs.Find(IDDanhMuc);

            //Kiểm tra tin tức khác null thì xóa
            if (danhmuc != null)
            {
                _context.DanhMucs.Remove(danhmuc);
            }
        }

        public IEnumerable<DanhMuc> GetAll()
        {
            return _context.DanhMucs.ToList();
        }

        public DanhMuc GetById(int? IDDanhMuc)
        {
            return _context.DanhMucs.Find(IDDanhMuc);
        }

        public void Insert(DanhMuc danhmuc)
        {
            _context.DanhMucs.Add(danhmuc);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(DanhMuc danhmuc)
        {
            _context.Entry(danhmuc).State = EntityState.Modified;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // Giải phóng tài nguyên quản lý
                    _context.Dispose();
                }
            }
            // Đánh dấu rằng tài nguyên đã được giải phóng.
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            // Ngăn chặn Garbage Collector gọi phương thức Finalize cho đối tượng này.
            GC.SuppressFinalize(this);
        }

        public bool DanhMucExists(int id)
        {
            return _context.DanhMucs.Any(e => e.IddanhMuc == id);
        }

        public IEnumerable<DanhMuc> GetListByKeyword(string keyword)
        {
            return _context.DanhMucs.Where(x => x.TenDanhMuc.Contains(keyword));
        }

        public bool CheckNameExists(string Name)
        {
            var danhmuc = _context.DanhMucs.FirstOrDefault(x => x.TenDanhMuc.Equals(Name));
            return danhmuc == null ? true : false;// Neu chua ton tai thi true
        }
    }
}
