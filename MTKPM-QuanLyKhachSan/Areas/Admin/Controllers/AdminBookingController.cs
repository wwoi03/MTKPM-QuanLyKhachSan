using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;
using MTKPM_QuanLyKhachSan.ViewModels;
using Newtonsoft.Json;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminBookingController : Controller
    {
        RoomDao roomDao;
        BookRoomDao bookRoomDao;

        public AdminBookingController(DatabaseContext context)
        {
            roomDao = new RoomDao(context);
            bookRoomDao = new BookRoomDao(context);
        }

        public IActionResult Index()
        {
            

            return View();
        }

        public IActionResult GetBooking()
        {
            var rooms = roomDao.GetRooms();
            var bookings = bookRoomDao.GetBookRooms();

            // Chuyển đổi từ Room sang RoomTitleVM
            List<RoomTitleVM> roomTitleVMs = rooms.Select(room => new RoomTitleVM
            {
                id = (room.RoomId).ToString(),
                title = room.Name,
            }).ToList();

            List<BookingEventVM> bookingEventVMs = bookings.Select(booking => new BookingEventVM
            {
                id = (booking.BookRoomId).ToString(),
                resourceId = (booking.RoomId).ToString(),
                start = (booking.CheckIn).ToString(),
                end = (booking.CheckOut).ToString(),
                title = booking.Customer.Name,
            }).ToList();

            // Chuyển đổi danh sách RoomTitleVM sang chuỗi JSON
            string roomTitleVMJsons = JsonConvert.SerializeObject(roomTitleVMs, Formatting.Indented);
            string bookingEventVMsJsons = JsonConvert.SerializeObject(bookingEventVMs, Formatting.Indented);

            return Json(new
            {
                resources = roomTitleVMJsons,
                events = bookingEventVMsJsons,
            });
        }
    }
}
