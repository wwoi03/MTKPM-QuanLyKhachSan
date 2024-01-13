using Microsoft.AspNetCore.Mvc;

namespace MTKPM_QuanLyKhachSan.Controllers
{
    public class PublicCustomerController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }

        public IActionResult Information()
        {
            return View();
        }

        public IActionResult HistoryBooking()
        {
            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }
    }
}
