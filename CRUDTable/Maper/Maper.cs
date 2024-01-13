using AutoMapper;
using CRUDTable.Models;
using Microsoft.AspNetCore.Identity;

namespace CRUDTable.Maper
{
    public class Maper : Profile
    {
        public Maper() {
            CreateMap<IdentityUser, UserRoleModel>();
            CreateMap<UserRoleModel, IdentityUser>();

        }
    }
}
