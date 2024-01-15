using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Controllers
{
    public class PublicRoomController : Controller
    {
        RoomTypeDao roomTypeDao;

        public PublicRoomController(DatabaseContext context)
        {
            roomTypeDao = new RoomTypeDao(context);
        }

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
