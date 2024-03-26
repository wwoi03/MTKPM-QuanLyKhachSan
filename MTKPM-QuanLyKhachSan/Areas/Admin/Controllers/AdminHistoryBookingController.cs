using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using iTextSharp.text;
using iTextSharp.text.pdf;

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
        public IActionResult Edit(BookRoomDetails bookRoomDetails, BookRoom bookRoom)
        {
            _bookRoomDetailsDao.UpdateBookRoomDetails(bookRoomDetails);
            _bookRoomDao.UpdateBookRoom(bookRoom);
            return RedirectToAction("Index");
        }
        //Xử lý yêu cầu xóa lịch sử đặt phòng
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _bookRoomDetailsDao.Delete(id);
            return RedirectToAction("Index");
        }

        //Xử lý yêu cầu lưu lịch sử đặt phòng
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
        //Xử lý yêu cầu tìm kiếm lịch sử đặt phòng theo ngày/tháng/năm
        [HttpPost]
        public IActionResult Search(DateTime startDate, DateTime endDate)
        {
            var searchResults = _bookRoomDetailsDao.Search(startDate, endDate);
            return View("Index", searchResults);
        }
        // Xử lý yêu cầu in hóa đơn
        [HttpPost]
        public IActionResult PrintInvoice(int id)
        {
            // Lấy chi tiết đặt phòng từ id
            var bookingDetails = _bookRoomDetailsDao.GetBookRoomDetailsById(id);

            // Tạo document PDF
            Document doc = new Document();
            MemoryStream ms = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, ms);
            doc.Open();

            // Thêm nội dung vào PDF
            doc.Add(new Paragraph("Hóa đơn"));
            doc.Add(new Paragraph("Mã đặt phòng: " + bookingDetails.BookRoomId));
            doc.Add(new Paragraph("Mã khách hàng: " + bookingDetails.BookRoom.CustomerId));
            doc.Add(new Paragraph("Tên khách hàng: " + bookingDetails.BookRoom.Customer.Name));
            doc.Add(new Paragraph("Mã nhân viên: " + bookingDetails.BookRoom.EmployeeId));
            doc.Add(new Paragraph("Mã phòng: " + bookingDetails.RoomId));
            doc.Add(new Paragraph("Phòng: " + bookingDetails.Room.Name));
            doc.Add(new Paragraph("Người lớn: " + bookingDetails.BookRoom.NumAdult));
            doc.Add(new Paragraph("Trẻ em: " + bookingDetails.BookRoom.NumChildren));
            doc.Add(new Paragraph("Ghi chú: " + bookingDetails.BookRoom.Note));
            doc.Add(new Paragraph("Thời gian nhận phòng: " + bookingDetails.CheckIn.ToString("dd/MM/yyyy HH:mm:ss")));
            doc.Add(new Paragraph("Thời gian trả phòng: " + bookingDetails.CheckOut.ToString("dd/MM/yyyy HH:mm:ss")));
            doc.Add(new Paragraph("Trạng thái: " + bookingDetails.Status));

            doc.Close();

            // Chuẩn bị file để tải về
            byte[] fileBytes = ms.ToArray();
            ms.Close();

            // Tên file hóa đơn
            string fileName = "HoaDon_" + bookingDetails.BookRoomId + ".pdf";

            // Trả về file để tải về
            return File(fileBytes, "application/pdf", fileName);
        }
    }
}
