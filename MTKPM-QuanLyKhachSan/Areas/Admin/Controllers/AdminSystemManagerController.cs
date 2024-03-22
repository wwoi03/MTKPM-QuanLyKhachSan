using Microsoft.AspNetCore.Mvc;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class AdminSystemManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
