using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using BusinessLogicLayer.Danhmuc;
using Microsoft.AspNetCore.Authorization;

namespace CRUDTable.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DanhMucsController : Controller
    {
        private readonly IDanhmucService _context;

        public DanhMucsController(IDanhmucService context)
        {
            _context = context;
        }

        // GET: DanhMucs
        public async Task<IActionResult> Index(string keyword)
        {
            if(keyword != null)
            {
                return View(_context.GetListByKeyword(keyword));
            }
            return View(_context.GetAll());
        }

        // GET: DanhMucs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMuc = _context.GetById(id);
            if (danhMuc == null)
            {
                return NotFound();
            }

            return View(danhMuc);
        }

        // GET: DanhMucs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DanhMucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IddanhMuc,TenDanhMuc,TrangThai")] DanhMuc danhMuc)
        {
            ViewData["msgtitleExits"] = "";
            if (ModelState.IsValid)
            {
                if (_context.CheckNameExists(danhMuc.TenDanhMuc))
                {
                    _context.Insert(danhMuc);
                    _context.Save();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewData["msgtitleExits"] = "Tên danh mục đã tồn tại !";
                }
            }
            return View(danhMuc);
        }

        // GET: DanhMucs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMuc = _context.GetById(id);
            if (danhMuc == null)
            {
                return NotFound();
            }
            return View(danhMuc);
        }

        // POST: DanhMucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IddanhMuc,TenDanhMuc,TrangThai")] DanhMuc danhMuc)
        {
            if (id != danhMuc.IddanhMuc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(danhMuc);
                    _context.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DanhMucExists(danhMuc.IddanhMuc))
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
            return View(danhMuc);
        }

        // GET: DanhMucs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMuc = _context.GetById(id);
            if (danhMuc == null)
            {
                return NotFound();
            }

            return View(danhMuc);
        }

        // POST: DanhMucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var danhMuc = _context.GetById(id);
            if (danhMuc != null)
            {
                _context.Delete(danhMuc.IddanhMuc);
            }

            _context.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool DanhMucExists(int id)
        {
            return _context.DanhMucExists(id);
        }
    }
}
