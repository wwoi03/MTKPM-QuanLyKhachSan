using Microsoft.AspNetCore.Mvc;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
