using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Tintuc
{
    public interface ITintucServices
    {
        Task<IEnumerable<TinTuc>> GetAll();
        Task<TinTuc> GetById(int? IDTinTuc);
        Task<TinTuc> GetByURL (string? URL);
        bool Insert(TinTuc TinTuc);
        Task<bool> Update(TinTuc TinTuc);
        void Delete(int IDTinTuc);
        void Save();
        bool TinTucExists(int id);
        void RemoveImageById(int id);
        void UpdateStatus(int id);
        bool CheckName(string name);

    }
}
