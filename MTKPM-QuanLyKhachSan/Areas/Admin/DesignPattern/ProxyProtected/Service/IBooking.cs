using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.ViewModels;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.ProxyProtected.Service
{
    public interface IBooking
    {
        IActionResult Index();
        IActionResult GetBooking();
        IActionResult Booking();
        IActionResult BookingDetails(int Id);
        IActionResult EditBooking(BookingAdminVM bookingAdminVM);
        IActionResult ChooseRoom();
    }
}
