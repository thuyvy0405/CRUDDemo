using DataAccessLayer.Interfaces.Quyen;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Quyen
{
    public class QuyenService : IQuyenService
    {
        private readonly IQuyenRepo _repo;
        public QuyenService(IQuyenRepo repo) { 
            _repo = repo;
        }  
        public IEnumerable<IdentityRole> GetAll()
        {
            return _repo.GetAll();
        }
    }
}
