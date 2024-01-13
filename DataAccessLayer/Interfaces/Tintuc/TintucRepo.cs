
using Azure.Messaging;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Interfaces.Tintuc
{
    public class TintucRepo : ITintucRepo
    {
        private readonly ApplicationDbContext _context;

        // hàm khởi tạo
        public TintucRepo()
        {
            _context = new ApplicationDbContext();
        }
       // hàm khởi tạo có ham số là DBContext
        public TintucRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Delete(int IDTinTuc)
        {
            //select idtintuc
            TinTuc tintuc = _context.TinTucs.Find(IDTinTuc);

            //Kiểm tra tin tức khác null thì xóa
            if (tintuc != null)
            {
                _context.TinTucs.Remove(tintuc);
            }
        }

        public async Task<IEnumerable<TinTuc>> GetAll()
        {
            return  _context.TinTucs.ToList();
        }

        public async Task<TinTuc> GetById(int? IDTinTuc)
        {
            return _context.TinTucs.Include(x => x.DanhMucTinTucs).ThenInclude(x => x.DanhMuc).FirstOrDefault(x => x.IdtinTuc.Equals(IDTinTuc));
        }
        
        public bool Insert(TinTuc tintuc)
        {

            if (CheckName(tintuc.TieuDe.ToString()) == true)
            {
               _context.TinTucs.Add(tintuc);
                return true;
            }
            return false;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task<bool> Update(TinTuc tintuc)
        {
            _context.Update(tintuc);
            //_context.Entry(tintuc).State = EntityState.Modified;
            return true;
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

        public bool TinTucExists(int id)
        {
            return _context.TinTucs.Any(e => e.IdtinTuc == id);
        }

        public async Task<TinTuc> GetByURL(string? URL)
        {
            return _context.TinTucs.FirstOrDefault(x => x.Url.Contains(URL));
        }

        public void RemoveImageById(int id)
        {
            var tintuc = _context.TinTucs.FirstOrDefault(x => x.IdtinTuc.Equals(id));
            if(tintuc != null)
            {
                tintuc.HinhAnh = null;
                _context.Update(tintuc);
                _context.SaveChanges();
            }

        }

        public void UpdateStatus(int id)
        {
            var tintuc = _context.TinTucs.Find(id);
            if(tintuc != null)
            {
                //Cập nhật lại trạng thái
                tintuc.TrangThai = !tintuc.TrangThai;
                _context.Update(tintuc);
                _context.SaveChanges();
            }
        }

        public bool CheckName(string name)
        {
            if(name == null)
            {
                return false;
            }
            else
            {
                    var tieude = _context.TinTucs.FirstOrDefault(t => t.TieuDe == name);
                    if (tieude != null)
                    {
                        return (false);
                    }              
            }
            return true;
        }
    }
}
