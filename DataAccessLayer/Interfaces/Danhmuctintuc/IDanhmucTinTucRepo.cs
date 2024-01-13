using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces.Danhmuctintuc
{
    public interface IDanhmucTinTucRepo
    {
        IEnumerable<DanhMucTinTuc> GetAll();
        DanhMucTinTuc GetById(int? IDDanhMucTinTuc);
        List<DanhMuc> GetLisyByTintucId(int? IDTinTuc);
        Task RemoveRangeByTinTucId(int IDTinTuc);
        void Insert(DanhMucTinTuc danhmuctintuc);
        void InsertRange(List<DanhMucTinTuc> danhmuctintucs);
        void Update(DanhMucTinTuc danhmuctintuc);
        void Delete(int IDDanhMucTinTuc);
        Task Save();
        bool DanhMucTinTucExists(int id);
    }
}
