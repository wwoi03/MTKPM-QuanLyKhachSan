using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.ViewModels;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.ProxyProtected.Services
{
    public interface IBooking : IProxy
    {
        IActionResult Index();

        [HttpGet]
        IActionResult Booking();

        [HttpPost]
        IActionResult Booking(BookingAdminVM bookingAdminVM);

        [HttpGet]
        IActionResult BookingDetails(int bookRoomDetailsId);

        [HttpPost]
        IActionResult EditBooking(BookingAdminVM bookingAdminVM);
    }
}
