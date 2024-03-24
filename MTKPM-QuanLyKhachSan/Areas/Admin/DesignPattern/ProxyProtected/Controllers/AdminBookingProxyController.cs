using Microsoft.AspNetCore.Mvc;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.ProxyProtected.Controllers
{
    public class AdminBookingProxyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
