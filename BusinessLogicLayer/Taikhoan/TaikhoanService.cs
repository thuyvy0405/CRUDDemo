using DataAccessLayer.Interfaces.Taikhoan;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Taikhoan
{
    public class TaikhoanService : ITaikhoanService
    {
        private readonly ITaikhoanRepo _repo;
        public TaikhoanService(ITaikhoanRepo repo)
        {
            _repo = repo;
        }
        public void Delete(string email)
        {
            _repo.Delete(email);
        }

        public IEnumerable<IdentityUser> GetAll()
        {
            return _repo.GetAll();
        }

        public IdentityUser GetByEmail(string? email)
        {
            return _repo.GetByEmail(email);
        }

        public IEnumerable<IdentityUser> GetListByKeyWord(string keyword)
        {
            return GetListByKeyWord(keyword);
        }

      
        public void Insert(IdentityUser user, string nameRole, string pass)
        {
            _repo.Insert(user, nameRole, pass);   
        }

        public async Task<int> Update(IdentityUser user, string nameRole, string pass, string oldpass)
        {
            return await _repo.Update(user, nameRole, pass, oldpass);
        }
        public List<string> GetRolesByEmail(string userEmail)
        {
            return _repo.GetRolesByEmail(userEmail);
        }
    }
}
