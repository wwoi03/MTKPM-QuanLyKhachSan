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
        BookRoomDao bookRoomDao;
        BookRoomDetailsDao bookRoomDetailsDao;

        public AdminRentCheckOutController(DatabaseContext context)
        {
            roomTypeDao = new RoomTypeDao(context);
            roomDao = new RoomDao(context);
            bookRoomDao = new BookRoomDao(context);
            bookRoomDetailsDao = new BookRoomDetailsDao(context);
        }

        public IActionResult Index()
        {
            return View();
        }

        // danh sách phòng chờ
        public IActionResult RoomWait()
        {
            ViewBag.roomTypes = roomTypeDao.GetRoomTypes();
            ViewBag.roomWaits = roomDao.GetEmptyRooms();

            return PartialView();
        }

        // danh sách phòng đang thuê
        public IActionResult RoomRent()
        {
            ViewBag.roomRents = bookRoomDetailsDao.GetBookRoomDetailsReceive();
            ViewBag.roomTypes = roomTypeDao.GetRoomTypes();

            return PartialView();
        }

        // danh sách phòng cần dọn
        public IActionResult RoomClean()
        {
            ViewBag.roomCleans = roomDao.GetCleanRooms();
            ViewBag.roomTypes = roomTypeDao.GetRoomTypes();

            return PartialView();
        }

        // dọn phòng
        [HttpPost]
        public IActionResult CleanRoom(int roomId)
        {
            roomDao.CleanRoom(roomId);
            return Json(true);
        }

        // yêu cầu dọn phòng
        [HttpPost]
        public IActionResult RequestCleanRoom(int roomId)
        {
            roomDao.RequestCleanRoom(roomId);
            return Json(true);
        }
    }
}
