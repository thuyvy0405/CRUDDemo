using DataAccessLayer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces.Taikhoan
{
    public class TaikhoanRepo : ITaikhoanRepo
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public TaikhoanRepo(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        public IEnumerable<IdentityUser> GetAll()
        {
            return _userManager.Users.ToList();
        }

        public IEnumerable<IdentityUser> GetListByKeyWord(string keyword)
        {
            return _userManager.Users
                .Where(u => u.UserName.Contains(keyword) || u.Email.Contains(keyword))
                .ToList();
        }

        public IdentityUser GetByEmail(string? email)
        {
            return _userManager.FindByEmailAsync(email).Result;
        }

        public void Insert(IdentityUser user, string nameRole, string password)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _userManager.CreateAsync(user, password).Wait();

            if (!string.IsNullOrEmpty(nameRole))
            {
                EnsureRoleExists(nameRole);
                _userManager.AddToRoleAsync(user, nameRole).Wait();
            }
        }

        public async Task<int> Update(IdentityUser user, string roleName, string password, string currentpass)
        {
            if (user == null)
            {
                return 2; // User is null, return an appropriate error code
            }
            // Detach the entity from the DbContext to avoid tracking issues
           // _context.Entry(user).State = EntityState.Detached;

            // Kiểm tra user có tồn tại trong CSDL
            var existingUser = await _userManager.FindByIdAsync(user.Id);

            if (existingUser == null)
            {
                return 2; // user không tìm thấy
            }

            if (!string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(currentpass))
            {
                var result = await _userManager.ChangePasswordAsync(existingUser, currentpass, password);

                if (!result.Succeeded)
                {
                    return 0; // Không thể thay đổi mật khauar
                }
            }
            existingUser.UserName = user.UserName; // update username
            existingUser.Email = user.Email; // Cập nhật email
            // Check cập nhật
            var updateResult = await _userManager.UpdateAsync(existingUser);

            if (!updateResult.Succeeded)
            {
                // Cập nhật k thành công
                return 0;
            }

            if (!string.IsNullOrEmpty(roleName))
            {
                await EnsureRoleExists(roleName);

                var existingRoles = await _userManager.GetRolesAsync(existingUser);

                // RXóa các role đã tồn tại
                await _userManager.RemoveFromRolesAsync(existingUser, existingRoles);
                //Tạo role mới cho user
                await _userManager.AddToRoleAsync(existingUser, roleName);
            }

            return 1; // Update successful
        }


        public void Delete(string email)
        {
            var user = _userManager.FindByEmailAsync(email).Result;
            if (user != null)
            {
                _userManager.DeleteAsync(user).Wait();
            }
        }

        public void Save()
        {
            // UserManager sẽ tự quản lý việc lưu thông tin người dùng, do đó, không cần phải gọi SaveChanges() ở đây.
        }
        private async Task EnsureRoleExists(string roleName)
        {
            if (await _roleManager.RoleExistsAsync(roleName) == false)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        public List<string> GetRolesByEmail(string userEmail)
        {
            var user = _userManager.FindByEmailAsync(userEmail).Result;

            if (user != null)
            {
                var userRoles = _userManager.GetRolesAsync(user).Result.ToList();
                return userRoles;
            }

            return new List<string>();
        }

    }
}
