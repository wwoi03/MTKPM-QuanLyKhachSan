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
        BookRoomDetailsDao bookRoomDetailsDao;

        public AdminBookingController(DatabaseContext context)
        {
            roomDao = new RoomDao(context);
            bookRoomDao = new BookRoomDao(context);
            bookRoomDetailsDao = new BookRoomDetailsDao(context);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetBooking()
        {
            var rooms = roomDao.GetRooms();
            var bookings = bookRoomDetailsDao.GetBookRoomDetails();

            // Chuyển đổi từ Room sang RoomTitleVM
            List<RoomTitleVM> roomTitleVMs = rooms.Select(room => new RoomTitleVM
            {
                id = (room.RoomId).ToString(),
                title = room.Name,
            }).ToList();

            List<BookingEventVM> bookingEventVMs = bookings.Select(booking => new BookingEventVM
            {
                id = (booking.BookRoomDetailsId).ToString(),
                resourceId = (booking.RoomId).ToString(),
                start = DateTime.Parse(booking.CheckIn.ToString()).ToString("yyyy-MM-dd"),
                end = DateTime.Parse(booking.CheckOut.ToString()).ToString("yyyy-MM-dd"),
                title = booking.BookRoom.Customer.Name,
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

        // Đặt phòng
        [HttpGet]
        public IActionResult Booking()
        {
            return PartialView();
        }

        // chi tiết đặt phòng
        [HttpGet]
        public IActionResult BookingDetails(int bookRoomDetailsId)
        {
            BookRoomDetails bookRoomDetails = bookRoomDetailsDao.GetBookRoomDetailsById(bookRoomDetailsId);

            BookingDetailsVM bookingDetailsVM = new BookingDetailsVM()
            {
                BookRoomId = bookRoomDetails.BookRoomId,
                Name = bookRoomDetails.BookRoom.Customer.Name,
                Phone = bookRoomDetails.BookRoom.Customer.Phone,
                CheckIn = bookRoomDetails.CheckIn.ToString(),
                CheckOut = bookRoomDetails.CheckOut.ToString(),
                Note = bookRoomDetails.Note,
                CIC = bookRoomDetails.BookRoom.Customer.CIC,
            };

            return PartialView("BookingDetails", bookingDetailsVM);
        }

        // sửa đặt phòng
        [HttpPost]
        public IActionResult EditBooking(BookingDetailsVM bookingDetailsVM)
        {
            ExecuteOperation executeOperation;

            if (int.Parse(bookingDetailsVM.Phone) <= 0 || bookingDetailsVM.Phone.Length > 10)
            {
                executeOperation = new ExecuteOperation()
                {
                    Result = false,
                    Mess = "Vui lòng nhập đúng định dạng số điện thoại.",
                };
            }
            else if (bookingDetailsVM.CheckDate() == false)
            {
                executeOperation = new ExecuteOperation()
                {
                    Result = false,
                    Mess = "Ngày đi phải nhỏ hơn ngày tới.",
                };
            }
            else
            {
                executeOperation = new ExecuteOperation()
                {
                    Result = true,
                    Mess = "Chỉnh sửa đặt phòng thành công.",
                };
            }

            return Json(executeOperation);
        }
    }
}
