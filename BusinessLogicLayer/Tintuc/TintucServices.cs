using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Interfaces.Tintuc;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;

namespace BusinessLogicLayer.Tintuc
{
    public class TintucServices : ITintucServices
    {
        private readonly ITintucRepo _context;

        public TintucServices(ITintucRepo context)
        {
            _context = context;
        }
        public void Delete(int IDTinTuc)
        {
            _context.Delete(IDTinTuc);  
        }


        public async Task<IEnumerable<TinTuc>> GetAll()
        {
            return await _context.GetAll();
        }


        public async Task<TinTuc> GetById(int? IDTinTuc)
        {
            return await _context.GetById(IDTinTuc);
        }
        
        public bool Insert(TinTuc tintuc)
        {
            if(tintuc.Url == null)
            {
                tintuc.Url = CreateURL(tintuc.TieuDe);
            }
            tintuc.NgayTao = DateTime.Now;
            tintuc.NgayUpdate = DateTime.Now;
           return _context.Insert(tintuc);
        }

        string CreateURL(string tentitntuc)
        {
            // Remove accents from Vietnamese characters
            string normalized = tentitntuc.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            foreach (char c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }
            string url = sb.ToString().ToLowerInvariant();
            // Replace spaces with dashes
            url = Regex.Replace(url, @"\s+", "-");

            // Append a unique identifier (e.g., a random number) at the end of the URL
            Random rnd = new Random();
            int uniqueId = rnd.Next(1000000, 9999999);
            url = $"{url}-{uniqueId}";
            return "https://localhost:7276/"+url;
        }

        public void Save()
        {
            _context.Save();
        }

        public Task<bool> Update(TinTuc tintuc)
        {
            if (tintuc.Url == null)
            {
                tintuc.Url = CreateURL(tintuc.TieuDe);
            }
         //   tintuc.NgayTao = tintuc.NgayTao;
            tintuc.NgayUpdate = DateTime.Now;
            return _context.Update(tintuc);
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
        }
        */
        public bool TinTucExists(int id)
        {
            return _context.TinTucExists(id);
        }

        public async Task<TinTuc> GetByURL(string? URL)
        {
            return await _context.GetByURL(URL);

        }

        public void RemoveImageById(int id)
        {
            _context.RemoveImageById(id);
        }

        public void UpdateStatus(int id)
        {
            _context.UpdateStatus(id);
        }
        public bool CheckName(string name)
        {
           return _context.CheckName(name);
        }
    }
}
