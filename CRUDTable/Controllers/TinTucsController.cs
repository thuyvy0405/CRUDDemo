using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Entities;
using BusinessLogicLayer.Danhmuc;
using BusinessLogicLayer.Danhmuctintuc;
using BusinessLogicLayer.Tintuc;
using Microsoft.AspNetCore.Authorization;
using CRUDTable.Models;
namespace CRUDTable.Controllers
{
    [Authorize(Roles="Admin")]
    public class TinTucsController : Controller
    {
        private ITintucServices _ITintucServices;
        private ITintucFilterService _ITintucFilterService;
        private IDanhmucService _IDanhmucService;
        private IDanhmuctintucService _IDanhmucTinTucService;
        private readonly IWebHostEnvironment _environment;


        public TinTucsController(IWebHostEnvironment environment, ITintucServices tintucServices, IDanhmucService danhmucService, IDanhmuctintucService danhmucTintucService, ITintucFilterService ITintucFilterService)
        {
            _environment = environment;
            _ITintucServices = tintucServices;
            _IDanhmucService = danhmucService;
            _IDanhmucTinTucService = danhmucTintucService;
            _ITintucFilterService = ITintucFilterService;

        }


        // GET: TinTucs
        public async Task<IActionResult> Index(string? keyword, DateTime startDate, DateTime endDate, bool? isApproved)
        {
            if(keyword == null && startDate != DateTime.Now && endDate != DateTime.Now && isApproved == null)
            {
                var model = await _ITintucServices.GetAll();
                return View(model);
            }    

            var modelfilter = await _ITintucFilterService.GetListByDate(startDate, endDate, keyword != null ? keyword : "", isApproved);
            return View(modelfilter);
        }

        // GET: TinTucs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinTuc = await _ITintucServices.GetById(id);
            if (tinTuc == null)
            {
                return NotFound();
            }

            return View(tinTuc);
        }

        // GET: TinTucs/Create
        public IActionResult Create()
        {
            ViewBag.Danhmuc =  _IDanhmucService.GetAll();
            return View();
        }


       

        // POST: TinTucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdtinTuc,TieuDe,Url,TomTat,NoiDung,HinhAnh,NgayTao,NgayUpdate,LuotXem,TrangThai")] TinTuc tinTuc, List<int>? IDDanhMuc, IFormFile? fileanh)
        {
            ViewData["msg"] = "";

            if (ModelState.IsValid && IDDanhMuc.Count > 0)
            {
                if (!_ITintucServices.Insert(tinTuc))
                {
                    ViewData["msgtitleExits"] = "Tên tiêu đề đã tồn tại !";
                    ViewBag.Danhmuc = _IDanhmucService.GetAll();
                    return View(tinTuc);
                }
                tinTuc.HinhAnh = await UploadImage(fileanh, null);

                _ITintucServices.Save();
                foreach (var item in IDDanhMuc)
                {
                    DanhMucTinTuc dmtt = new DanhMucTinTuc();
                    dmtt.IddanhMuc = item;
                    dmtt.IdtinTuc = tinTuc.IdtinTuc;

                    _IDanhmucTinTucService.Insert(dmtt);
                    _IDanhmucTinTucService.Save();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["msg"] = "Vui lòng chọn 1 hoặc nhiều danh mục";
            ViewBag.Danhmuc = _IDanhmucService.GetAll();
            return View(tinTuc);
        }

        // GET: TinTucs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinTuc = await _ITintucServices.GetById(id);
            if (tinTuc == null)
            {
                return NotFound();
            }
            ViewBag.Danhmuc = _IDanhmucService.GetAll();
            return View(tinTuc);
        }

        // POST: TinTucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdtinTuc,TieuDe,Url,TomTat,NoiDung,HinhAnh,NgayTao,NgayUpdate,LuotXem,TrangThai")] TinTuc tinTuc, List<int> IDDanhMuc, IFormFile? fileanh)
        {
            ViewData["msg"] = "";
            var oldimagename = tinTuc.HinhAnh;
            if (id != tinTuc.IdtinTuc)
            {
                return NotFound();
            }
            var checkTintuc = await _ITintucServices.GetById(tinTuc.IdtinTuc);
           
            //Kiem tra tieu de
            if(checkTintuc.TieuDe != tinTuc.TieuDe)
            {
                if(!_ITintucServices.CheckName(tinTuc.TieuDe))
                {
                    ViewData["msgtitleExits"] = "Tên tiêu đề đã tồn tại !";
                    ViewBag.Danhmuc = _IDanhmucService.GetAll();
                    return View(checkTintuc);
                }    
            }


            if (ModelState.IsValid && IDDanhMuc.Count > 0)
            {
                try
                {
                    
                    var uniqueImage = await UploadImage(fileanh, oldimagename);
                    tinTuc.HinhAnh = uniqueImage != null ? uniqueImage : oldimagename;
                    _ITintucServices.Update(tinTuc);
                    _ITintucServices.Save();

                    await _IDanhmucTinTucService.RemoveRangeByTinTucId(tinTuc.IdtinTuc);
                    await _IDanhmucTinTucService.Save();
                    foreach (var item in IDDanhMuc)
                    {
                        DanhMucTinTuc dmtt = new DanhMucTinTuc
                        {
                            IddanhMuc = item,
                            IdtinTuc = tinTuc.IdtinTuc,
                        };

                        _IDanhmucTinTucService.Insert(dmtt);
                    }
                    await _IDanhmucTinTucService.Save();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TinTucExists(tinTuc.IdtinTuc))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["msg"] = "Vui lòng chọn 1 hoặc nhiều danh mục";
            ViewBag.Danhmuc = _IDanhmucService.GetAll();
            return View(tinTuc);
        }

        // GET: TinTucs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinTuc = await _ITintucServices.GetById(id);
            if (tinTuc == null)
            {
                return NotFound();
            }

            return View(tinTuc);
        }

        // POST: TinTucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tinTuc = await _ITintucServices.GetById(id);
            if (tinTuc != null)
            {
                _ITintucServices.Delete(id);
            }

            _ITintucServices.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool TinTucExists(int id)
        {
            return _ITintucServices.TinTucExists(id);
        }
        [HttpPost]
        public void RemoveOldImage([FromBody] RemoveImageRequest request)
        {
            // Kiểm tra xem tintuc có hình ảnh chưa ??
            if (!string.IsNullOrEmpty(request.ImageName))
            {
                //Đường dẫn hình ảnh cũ
                var oldImagePath = Path.Combine(_environment.WebRootPath, "images", "tintuc", request.ImageName);
                //Kiểm tra nếu tồn tại thì xóa
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                    _ITintucServices.RemoveImageById(request.Id);
                }
            }
        }
        [HttpPost]
        public void UpdateStatus([FromBody] TinTucRequest request)
        {
            // Kiểm tra xem tintuc có hình ảnh chưa ??
            _ITintucServices.UpdateStatus(request.Id);
        }
        public async Task<string> UploadImage(IFormFile fileanh, string? OldImageName)
        {
            string uniqueFileName = null;
            if (fileanh != null)
            {
                // Kiểm tra xem tintuc có hình ảnh chưa ??
                if (!string.IsNullOrEmpty(OldImageName))
                {
                    //Đường dẫn hình ảnh cũ
                    var oldImagePath = Path.Combine(_environment.WebRootPath, "images", "tintuc", OldImageName);
                    //Kiểm tra nếu tồn tại thì xóa
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                var uploadsDirectory = Path.Combine(_environment.WebRootPath, "images", "tintuc");
                if (!Directory.Exists(uploadsDirectory))
                {
                    Directory.CreateDirectory(uploadsDirectory);
                }

                // Generate a unique filename for the uploaded file
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(fileanh.FileName);
                var filePath = Path.Combine(uploadsDirectory, uniqueFileName);
                // Save the file to the server
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await fileanh.CopyToAsync(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
