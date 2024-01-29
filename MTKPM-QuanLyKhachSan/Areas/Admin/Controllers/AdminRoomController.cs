using Microsoft.AspNetCore.Mvc;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminRoomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
