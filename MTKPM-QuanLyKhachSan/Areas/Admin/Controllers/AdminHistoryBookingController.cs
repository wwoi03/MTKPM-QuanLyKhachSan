using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminHistoryBookingController : Controller
    {
        private readonly BookRoomDetailsDao _bookRoomDetailsDao;
        private readonly BookRoomDao _bookRoomDao;

        public AdminHistoryBookingController(BookRoomDetailsDao bookRoomDetailsDao, BookRoomDao bookRoomDao)
        {
            _bookRoomDetailsDao = bookRoomDetailsDao;
            _bookRoomDao = bookRoomDao;
        }
        //Hiển thị danh sách lịch sử đặt phòng
        public IActionResult Index()
        {
            var historyBookings = _bookRoomDetailsDao.GetBookRoomDetails();
            return View(historyBookings);
        }
        //Hiển thị chi tiết lịch sử đặt phòng
        public IActionResult Detail(int id)
        {
            var bookingDetails = _bookRoomDetailsDao.GetBookRoomDetailsById(id);
            return View(bookingDetails);
        }
        //Hiển thị trang chỉnh sửa lịch sử đặt phòng
        public IActionResult Edit(int id)
        {
            var bookingDetails = _bookRoomDetailsDao.GetBookRoomDetailsById(id);
            return View(bookingDetails);
        }
        //Xử lý yêu cầu chỉnh sửa lịch sử đặt phòng
        [HttpPost]
        public IActionResult Edit(BookRoomDetails bookRoomDetails)
        {
            _bookRoomDetailsDao.UpdateBookRoomDetails(bookRoomDetails);
            return RedirectToAction("Index");
        }
        //Xử lý yêu cầu xóa Menu
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _bookRoomDetailsDao.Delete(id);
            return RedirectToAction("Index");
        }

        //Xử lý yêu cầu lưu Menu
        [HttpPost]
        public IActionResult Save(BookRoomDetails bookRoomDetails)
        {
            if (ModelState.IsValid)
            {
                _bookRoomDetailsDao.AddBookRoomDetails(bookRoomDetails);
                return RedirectToAction("Index");
            }
            return View(bookRoomDetails);
        }
        //Xử lý yêu cầu tìm kiếm Menu theo ngày/tháng/năm
        [HttpPost]
        public IActionResult Search(DateTime startDate, DateTime endDate)
        {
            var searchResults = _bookRoomDetailsDao.Search(startDate, endDate);
            return View("Index", searchResults);
        }
    }
}
