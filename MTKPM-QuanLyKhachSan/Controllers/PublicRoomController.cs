using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Daos;

namespace MTKPM_QuanLyKhachSan.Controllers
{
    public class PublicRoomController : Controller
    {
        RoomTypeDao roomTypeDao = new RoomTypeDao();

        public IActionResult Index()
        {
            ViewBag.PageTitle = "Phòng";
            ViewBag.rooms = roomTypeDao.GetRoomTypes();

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
