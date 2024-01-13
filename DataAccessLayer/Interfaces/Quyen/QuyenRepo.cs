using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces.Quyen
{
    public class QuyenRepo : IQuyenRepo
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public QuyenRepo(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IEnumerable<IdentityRole> GetAll()
        {
            return _roleManager.Roles;
        }

        

    }
}
