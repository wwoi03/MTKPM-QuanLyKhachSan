using Microsoft.AspNetCore.Mvc;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
