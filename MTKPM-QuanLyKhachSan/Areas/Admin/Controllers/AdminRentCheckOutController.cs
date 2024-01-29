using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminRentCheckOutController : Controller
    {
        RoomTypeDao roomTypeDao;
        RoomDao roomDao;

        public AdminRentCheckOutController(DatabaseContext context)
        {
            roomTypeDao = new RoomTypeDao(context);
            roomDao = new RoomDao(context);
        }

        public IActionResult Index()
        {
            ViewBag.roomTypes = roomTypeDao.GetRoomTypes();
            ViewBag.rooms = roomDao.GetRooms();

            return View();
        }
    }
}
