using BusinessLogicLayer.Danhmuctintuc;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces.Danhmuctintuc;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Danhmuctintuc
{
    public class DanhmucTintucService : IDanhmuctintucService
    {
        private readonly IDanhmucTinTucRepo _context;
        
        // hàm khởi tạo có ham số là DBContext
        public DanhmucTintucService(IDanhmucTinTucRepo context)
        {
            _context = context;
        }
        public void Delete(int ID)
        {
            //select idtintuc
           _context.Delete(ID);
        }

        public IEnumerable<DanhMucTinTuc> GetAll()
        {
            return _context.GetAll();
        }

        public DanhMucTinTuc GetById(int? ID)
        {
            return _context.GetById(ID);
        }

        public void Insert(DanhMucTinTuc danhmuc)
        {
            _context.Insert(danhmuc);
        }
        public async Task Save()
        {
            await _context.Save();
        }

        public void Update(DanhMucTinTuc danhmuc)
        {
            _context.Update (danhmuc);
        }
        public async Task RemoveRangeByTinTucId(int IDTinTuc)
        {
           await _context.RemoveRangeByTinTucId ( IDTinTuc );
        }

        private bool disposed = false;

       /* protected virtual void Dispose(bool disposing)
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
        }*/

        public bool DanhMucTinTucExists(int id)
        {
            return _context.DanhMucTinTucExists(id);
        }

        public List<DanhMuc> GetLisyByTintucId(int? IDTinTuc)
        {
            return _context.GetLisyByTintucId ( IDTinTuc );
        }

        public void InsertRange(List<DanhMucTinTuc> danhmuctintucs)
        {
            _context.InsertRange(danhmuctintucs);
        }
    }
}
