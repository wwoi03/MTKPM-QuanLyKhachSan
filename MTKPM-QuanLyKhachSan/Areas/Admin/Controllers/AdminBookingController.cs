using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Facede;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.ProxyProtected.Services;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Singleton;
using MTKPM_QuanLyKhachSan.Common.Config;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;
using MTKPM_QuanLyKhachSan.ViewModels;
using Newtonsoft.Json;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminBookingController : Controller, IBooking
    {
        private BookingFacede bookingFacede;
        private readonly IService _myService;

        public AdminBookingController(DatabaseContext context, IService myService)
        {
            _myService = myService;
            bookingFacede = new BookingFacede(context, myService);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetBooking()
        {
            var roomTitleVMJsons = bookingFacede.GetRoomTitleVMJsons();
            var bookingEventVMs = bookingFacede.GetBookingEventVMs();

            // Chuyển đổi danh sách RoomTitleVM sang chuỗi JSON
            string roomTitleVMJsonsString = JsonConvert.SerializeObject(roomTitleVMJsons, Formatting.Indented);
            // Chuyển đổi danh sách BookingEventVM sang chuỗi JSON
            string bookingEventVMsString = JsonConvert.SerializeObject(bookingEventVMs, Formatting.Indented);

            // Tạo một đối tượng anonymous chứa dữ liệu JSON
            var jsonData = new
            {
                resources = roomTitleVMJsonsString,
                events = bookingEventVMsString
            };

            return Json(jsonData);
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
            ExecutionOutcome executionOutcome = bookingFacede.Booking(bookingAdminVM);

            return Json(executionOutcome);
        }

        // chi tiết đặt phòng
        [HttpGet]
        public IActionResult BookingDetails(int bookRoomDetailsId)
        {
            BookingAdminVM bookingDetailsVM = bookingFacede.BookingDetails(bookRoomDetailsId);

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
            ViewBag.rooms = bookingFacede.roomDao.GetEmptyRooms(_myService.GetHotelId());
            ViewBag.roomTypes = bookingFacede.roomTypeDao.GetRoomTypes(_myService.GetHotelId());

            return PartialView();
        }
    }
}
