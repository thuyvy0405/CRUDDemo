using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Interfaces.Danhmuctintuc
{
    public class DanhmucTintucRepo : IDanhmucTinTucRepo
    {
        private readonly ApplicationDbContext _context;
        // hàm khởi tạo
        public DanhmucTintucRepo()
        {
            _context = new ApplicationDbContext();
        }
        // hàm khởi tạo có ham số là DBContext
        public DanhmucTintucRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Delete(int ID)
        {
            //select idtintuc
            DanhMucTinTuc danhmuctintuc = _context.DanhMucTinTucs.Find(ID);

            //Kiểm tra tin tức khác null thì xóa
            if (danhmuctintuc != null)
            {
                _context.DanhMucTinTucs.Remove(danhmuctintuc);
            }
        }

        public IEnumerable<DanhMucTinTuc> GetAll()
        {
            return _context.DanhMucTinTucs.ToList();
        }

        public DanhMucTinTuc GetById(int? ID)
        {
            return _context.DanhMucTinTucs.Find(ID);
        }

        public void Insert(DanhMucTinTuc danhmuc)
        {
            _context.DanhMucTinTucs.Add(danhmuc);
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(DanhMucTinTuc danhmuc)
        {
            _context.Entry(danhmuc).State = EntityState.Modified;
        }
        public async Task RemoveRangeByTinTucId(int IDTinTuc)
        {
            var danhMucTinTucsByID = await _context.DanhMucTinTucs
                  .Where(x => x.IdtinTuc == IDTinTuc)
                  .ToListAsync();
            _context.DanhMucTinTucs.RemoveRange(danhMucTinTucsByID);
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

        public bool DanhMucTinTucExists(int id)
        {
            return _context.DanhMucTinTucs.Any(e => e.IddanhMuc == id);
        }

        public List<DanhMuc> GetLisyByTintucId(int? IDTinTuc)
        {
            return _context.DanhMucTinTucs
                  .Where(x => x.IdtinTuc == IDTinTuc)
                  .Select(x => x.DanhMuc) // Assuming there is a navigation property named DanhMuc
                  .ToList();
        }

        public void InsertRange(List<DanhMucTinTuc> danhmuctintucs)
        {
            _context.AddRange(danhmuctintucs);
        }
    }
}
