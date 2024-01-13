using AutoMapper;
using BusinessLogicLayer.Quyen;
using BusinessLogicLayer.Taikhoan;
using CRUDTable.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRUDTable.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AccountController : Controller
    {
        private readonly ITaikhoanService _taikhoanService;
        private readonly IQuyenService _quyenservice;
        private readonly IMapper _maper;    

        public AccountController(ITaikhoanService taikhoanService, IQuyenService quyenservice, IMapper maper)
        {
            _taikhoanService = taikhoanService;
            _quyenservice = quyenservice;
            _maper = maper;
        }
        private void GetAllRoles()
        {
            ViewBag.Quyen = _quyenservice.GetAll();
        }
        public IActionResult Index()
        {
            var models = _taikhoanService.GetAll();
            return View(models);
        }

        public IActionResult Create()
        {
            GetAllRoles();
            return View();
        }

        [HttpPost]
        public IActionResult Create(IdentityUser user, string roleName, string pass)
        {
            if(user != null && roleName != null)
            {
                _taikhoanService.Insert(user, roleName, pass);
                return RedirectToAction(nameof(Index));
            }
            GetAllRoles();
            ViewData["msg"] = "Vui lòng điền đầy đủ thông tin";
            return View(user);
        }
        public IActionResult Edit(string? email)
        {
            if(email == null)
            {
                return NotFound();
            }

            //Ánh xạ
            UserRoleModel model =_maper.Map<UserRoleModel>(_taikhoanService.GetByEmail(email));
            model.RoleName = _taikhoanService.GetRolesByEmail(email);
            if (model == null)
            {
                return NotFound();
            }
            GetAllRoles();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserRoleModel user)
        {
            GetAllRoles();
            if (user != null)
            {

                int result = await _taikhoanService.Update(_maper.Map<IdentityUser>(user), user.RoleName[0], user.NewPassWork, user.OldPassWord);
                if(result == 0)
                {
                    ViewData["msg"] = "Mật khẩu cũ không khớp !";
                    return View(user);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["msg"] = "Vui lòng điền đầy đủ thông tin";
            return View(user);
        }

        public IActionResult Details(string? email)
        {
            if (email == null)
            {
                return NotFound();
            }
            var model = _taikhoanService.GetByEmail(email);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
        public IActionResult Delete(string email)
        {
            if (email == null)
            {
                return NotFound();
            }
            var model = _taikhoanService.GetByEmail(email);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteConfirm(string email)
        {
            if(email == null)
            {
                return NotFound();
            }
            _taikhoanService.Delete(email);
            return RedirectToAction(nameof(Index));
        }
    }
}
