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
                start = DateTime.Parse(booking.CheckIn.ToString()).ToString("yyyy-MM-dd"),
                end = DateTime.Parse(booking.CheckOut.ToString()).ToString("yyyy-MM-dd"),
                title = booking.Customer.Name,
                color = "#2BA5F0",
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

        public IActionResult Booking()
        {
            return PartialView();
        }

        public IActionResult BookingDetails(int bookingId)
        {
            BookRoom bookRoom = bookRoomDao.GetBookRoomById(bookingId);

            BookingDetailsVM bookingDetailsVM = new BookingDetailsVM()
            {
                BookRoomId = bookRoom.BookRoomId,
                Name = bookRoom.Customer.Name,
                Phone = bookRoom.Customer.Phone,
                CheckIn = bookRoom.CheckIn.ToString(),
                CheckOut = bookRoom.CheckOut.ToString(),
                Note = bookRoom.Note,
                CIC = "12345678910",
            };

            return PartialView();
        }
    }
}
