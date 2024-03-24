using Microsoft.AspNetCore.Mvc;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminSystemManagerController : Controller
    {
        public AdminSystemManagerController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
