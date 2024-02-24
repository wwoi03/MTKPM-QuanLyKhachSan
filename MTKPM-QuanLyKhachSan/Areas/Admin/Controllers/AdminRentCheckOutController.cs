﻿using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class AdminRentCheckOutController : Controller
    {
        RoomTypeDao roomTypeDao;
        RoomDao roomDao;
        BookRoomDao bookRoomDao;
        BookRoomDetailsDao bookRoomDetailsDao;
        BillDao billDao;

        public AdminRentCheckOutController(DatabaseContext context)
        {
            roomTypeDao = new RoomTypeDao(context);
            roomDao = new RoomDao(context);
            bookRoomDao = new BookRoomDao(context);
            bookRoomDetailsDao = new BookRoomDetailsDao(context);
            billDao = new BillDao(context);
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

        // lịch sử phòng
        public IActionResult RoomHistory()
        {
            ViewBag.roomHistory = billDao.GetBills();
            ViewBag.roomTypes = roomTypeDao.GetRoomTypes();

            return PartialView();
        }

        // dọn phòng
        [HttpPost]
        public IActionResult CleanRoom(int roomId)
        {
            roomDao.CleanRoom(roomId);
            return RedirectToAction("RoomClean", "AdminRentCheckOut", new { area = "Admin" });
        }

        // yêu cầu dọn phòng
        [HttpPost]
        public IActionResult RequestCleanRoom(int roomId)
        {
            roomDao.RequestCleanRoom(roomId);

            Room room = roomDao.GetRoomById(roomId);

            if (room.Status == 1) 
                return RedirectToAction("RoomRent", "AdminRentCheckOut", new { area = "Admin" });
            else
                return RedirectToAction("RoomWait", "AdminRentCheckOut", new { area = "Admin" });
        }

        // đổi phòng
        [HttpGet]
        public IActionResult ChangeRoom(int roomId)
        {
            ViewBag.roomChange = roomDao.GetRoomById(roomId);
            ViewBag.roomWaits = roomDao.GetEmptyRooms();
            ViewBag.roomTypes = roomTypeDao.GetRoomTypes();

            return PartialView();
        }

        // đổi phòng
        [HttpPost]
        public IActionResult ChangeRoom(int roomIdOld, int roomIdNew, bool isCleanRoom = false)
        {
            bookRoomDetailsDao.ChangeRoom(roomIdOld, roomIdNew);

            if (isCleanRoom)
            {
                roomDao.CleanRoom(roomIdNew);
            }

            return RedirectToAction("RoomRent", "AdminRentCheckOut", new { area = "Admin" });
        }
    }
}
