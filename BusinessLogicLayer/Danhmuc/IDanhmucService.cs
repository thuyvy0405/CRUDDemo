using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Danhmuc
{
    public interface IDanhmucService
    {
        IEnumerable<DanhMuc> GetAll();
        IEnumerable<DanhMuc> GetListByKeyword(string keyword);
        DanhMuc GetById(int? IDDanhMuc);
        void Insert(DanhMuc danhmuc);
        void Update(DanhMuc danhmuc);
        void Delete(int IDDanhMuc);
        void Save();
        bool DanhMucExists(int id);
        bool CheckNameExists(string Name);

    }
}
