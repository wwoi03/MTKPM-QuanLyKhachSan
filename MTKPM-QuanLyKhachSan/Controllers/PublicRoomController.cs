using Microsoft.AspNetCore.Mvc;

namespace MTKPM_QuanLyKhachSan.Controllers
{
    public class PublicRoomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RoomDetails()
        {
            return View();
        }

        public IActionResult Booking()
        {
            return View();
        }
    }
}
