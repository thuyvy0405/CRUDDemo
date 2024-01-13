using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Taikhoan
{
    public interface ITaikhoanService
    {
        IEnumerable<IdentityUser> GetAll();
        IEnumerable<IdentityUser> GetListByKeyWord(string keyword);
        IdentityUser GetByEmail(string? email);
        void Insert(IdentityUser user, string nameRole, string newpass);
        Task<int> Update(IdentityUser user, string nameRole, string newpass, string oldpass);
        void Delete(string email);
        public List<string> GetRolesByEmail(string userEmail);

    }
}
