using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Areas.Admin.Controllers;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.ProxyProtected.Services;
using MTKPM_QuanLyKhachSan.Models;
using MTKPM_QuanLyKhachSan.ViewModels;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.ProxyProtected.Controllers
{
    public class AdminBookingProxyController : Controller, IBooking
    {
        private IProxy proxy;
        private Employee employee;

        public AdminBookingProxyController()
        {
            //proxy = new AdminBookingController();
        }

        public IActionResult Booking()
        {
            throw new NotImplementedException();
        }

        public IActionResult Booking(BookingAdminVM bookingAdminVM)
        {
            throw new NotImplementedException();
        }

        public IActionResult BookingDetails(int bookRoomDetailsId)
        {
            throw new NotImplementedException();
        }

        public IActionResult EditBooking(BookingAdminVM bookingAdminVM)
        {
            throw new NotImplementedException();
        }

        public IActionResult Index()
        {
            throw new NotImplementedException();
        }
    }
}
