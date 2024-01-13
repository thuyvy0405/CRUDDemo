using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces.Danhmuc;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Danhmuc
{
    public class DanhmucService:IDanhmucService
    {
        private readonly IDanhmucRepo _context;
        // hàm khởi tạo
        public DanhmucService(IDanhmucRepo context)
        {
            _context = context;
        }
        public void Delete(int IDDanhMuc)
        {
           _context.Delete(IDDanhMuc);  
        }

        public IEnumerable<DanhMuc> GetAll()
        {
            return _context.GetAll();
        }

        public DanhMuc GetById(int? IDDanhMuc)
        {
            return _context.GetById(IDDanhMuc);
        }

        public void Insert(DanhMuc danhmuc)
        {
            _context.Insert(danhmuc);
        }

        public void Save()
        {
            _context.Save();
        }

        public void Update(DanhMuc danhmuc)
        {
            _context.Update(danhmuc);
        }

        private bool disposed = false;
        /*
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
        }*/

        public bool DanhMucExists(int id)
        {
            return _context.DanhMucExists(id);
        }

        public IEnumerable<DanhMuc> GetListByKeyword(string keyword)
        {
            return _context.GetListByKeyword(keyword);
        }

        public bool CheckNameExists(string Name)
        {
            return _context.CheckNameExists(Name);
        }
    }
}
