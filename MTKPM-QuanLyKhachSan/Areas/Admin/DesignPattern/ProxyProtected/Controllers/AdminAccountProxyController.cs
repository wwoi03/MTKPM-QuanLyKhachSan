using Microsoft.AspNetCore.Mvc;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.ProxyProtected.Controllers
{
    public class AdminAccountProxyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
