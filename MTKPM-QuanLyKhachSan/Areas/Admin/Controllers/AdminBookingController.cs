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
        RoomTypeDao roomTypeDao;
        BookRoomDao bookRoomDao;
        BookRoomDetailsDao bookRoomDetailsDao;
        CustomerDao customerDao;

        public AdminBookingController(DatabaseContext context)
        {
            roomDao = new RoomDao(context);
            roomTypeDao = new RoomTypeDao(context);
            bookRoomDao = new BookRoomDao(context);
            bookRoomDetailsDao = new BookRoomDetailsDao(context);
            customerDao = new CustomerDao(context);
        }

        public IActionResult Index()
        {
            /*HttpContext.Session.SetInt32("EmployeeId", 1);
            HttpContext.Session.SetString("EmployeeName", "Đào Công Tuấn");
            HttpContext.Session.SetInt32("HotelId", 1);*/

            return View();
        }

        public IActionResult GetBooking()
        {
            var rooms = roomDao.GetRooms(HttpContext.Session.GetInt32("HotelId"));
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
            return PartialView("Booking", new BookingAdminVM());
        }

        [HttpPost]
        public IActionResult Booking(BookingAdminVM bookingAdminVM)
        {
            ExecutionOutcome executionOutcome;
            string error;
            bool status = bookingAdminVM.Validation(out error);

            if (status)
            {
                // tạo khách hàng nếu chưa tồn tại
                int customerId = customerDao.GetCustomerIdByPhoneOrCIC(bookingAdminVM.Phone, bookingAdminVM.CIC);
                if (customerId > 0)
                {
                    Customer newCustomer = new Customer
                    {
                        Phone = bookingAdminVM.Phone,
                        CIC = bookingAdminVM.CIC,
                        Name = bookingAdminVM.Name
                    };

                    customerDao.CreateCustomer(newCustomer);

                    customerId = newCustomer.CustomerId;
                }

                // Tạo phiếu đặt phòng
                BookRoom newBookRoom = new BookRoom
                {
                    CustomerId = customerId,
                    EmployeeId = null,
                    Note = bookingAdminVM.Note,
                };

                // tạo phiếu chi tiết đặt phòng
                foreach (var room in bookingAdminVM.Rooms)
                {
                    // kiểm tra phòng trống
                    if (roomDao.IsRoomAvailable(room.RoomId)) 
                    {
                        // tạo phòng
                        BookRoomDetails newBookRoomDetails = new BookRoomDetails
                        {
                            BookRoomId = newBookRoom.BookRoomId,
                            RoomId = room.RoomId,
                            CheckIn = bookingAdminVM.ConvertDateTime(bookingAdminVM.CheckIn),
                            CheckOut = bookingAdminVM.ConvertDateTime(bookingAdminVM.CheckOut),
                            Note = newBookRoom.Note
                        };
                    }
                }
            }

            executionOutcome = new ExecutionOutcome()
            {
                Result = status,
                Mess = string.IsNullOrEmpty(error) ? "Đặt phòng thành công." : error
            };

            return Json(executionOutcome);
        }

        // chi tiết đặt phòng
        [HttpGet]
        public IActionResult BookingDetails(int bookRoomDetailsId)
        {
            BookRoomDetails bookRoomDetails = bookRoomDetailsDao.GetBookRoomDetailsById(bookRoomDetailsId);

            BookingAdminVM bookingDetailsVM = new BookingAdminVM()
            {
                BookRoomId = bookRoomDetails.BookRoomId,
                Name = bookRoomDetails.BookRoom.Customer.Name,
                Phone = bookRoomDetails.BookRoom.Customer.Phone,
                CheckIn = bookRoomDetails.CheckIn.ToString(),
                CheckOut = bookRoomDetails.CheckOut.ToString(),
                Note = bookRoomDetails.BookRoom.Note,
                CIC = bookRoomDetails.BookRoom.Customer.CIC,
                Rooms = roomDao.GetRooms(HttpContext.Session.GetInt32("HotelId"))
            };

            return PartialView("BookingDetails", bookingDetailsVM);
        }

        // sửa đặt phòng
        [HttpPost]
        public IActionResult EditBooking(BookingAdminVM bookingAdminVM)
        {
            ExecutionOutcome executionOutcome;
            string error;

            if (bookingAdminVM.Validation(out error) == true)
            {
                executionOutcome = new ViewModels.ExecutionOutcome()
                {
                    Result = true,
                    Mess = "Chỉnh sửa đặt phòng thành công.",
                };
            }
            else
            {
                executionOutcome = new ViewModels.ExecutionOutcome()
                {
                    Result = false,
                    Mess = error,
                };
            }

            return Json(executionOutcome);
        }

        // Chọn phòng đặt
        public IActionResult ChooseRoom()
        {
            ViewBag.rooms = roomDao.GetEmptyRooms(HttpContext.Session.GetInt32("HotelId"));
            ViewBag.roomTypes = roomTypeDao.GetRoomTypes(HttpContext.Session.GetInt32("HotelId"));

            return PartialView();
        }
    }
}
