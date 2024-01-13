using Microsoft.AspNetCore.Identity;

namespace CRUDTable.Models
{
    public class UserRoleModel : IdentityUser
    {
        public string OldPassWord { get; set; }
        public string NewPassWork { get; set; }
        public List<string> RoleName { get; set; }
    }
}
