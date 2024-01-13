using BusinessLogicLayer.Danhmuc;
using BusinessLogicLayer.Danhmuctintuc;
using BusinessLogicLayer.Tintuc;
using Microsoft.AspNetCore.Mvc;

namespace CRUDTable.Controllers
{
    public class HomeController : Controller
    {
        private ITintucServices _ITintucServices;
        private ITintucFilterService _ITintucFilterService;
        private IDanhmucService _IDanhmucService;
        private IDanhmuctintucService _IDanhmucTinTucService;
        private readonly IWebHostEnvironment _environment;


        public HomeController(IWebHostEnvironment environment, ITintucServices tintucServices, IDanhmucService danhmucService, IDanhmuctintucService danhmucTintucService, ITintucFilterService ITintucFilterService)
        {
            _environment = environment;
            _ITintucServices = tintucServices;
            _IDanhmucService = danhmucService;
            _IDanhmucTinTucService = danhmucTintucService;
            _ITintucFilterService = ITintucFilterService;

        }

        [HttpGet("/{tintucID}")]
       public async Task<IActionResult> ReadNews(string tintucID)
       {
           var news = await _ITintucServices.GetByURL(tintucID);

           if (news == null)
           {
               return NotFound();
           }

           return View(news);
       }

    }
}
