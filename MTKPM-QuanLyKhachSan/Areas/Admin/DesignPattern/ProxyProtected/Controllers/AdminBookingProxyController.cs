using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Areas.Admin.Controllers;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.ProxyProtected.Services;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Singleton;
using MTKPM_QuanLyKhachSan.Common;
using MTKPM_QuanLyKhachSan.Common.Config;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;
using MTKPM_QuanLyKhachSan.ViewModels;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.ProxyProtected.Controllers
{
    [Area("Admin")]
    public class AdminBookingProxyController : Controller, IBooking
    {
        private DatabaseContext context;
        private EmployeePermissionDao employeePermissionDao;
        private IBooking proxy;
        private IService myService;

        public AdminBookingProxyController(DatabaseContext context, IService myService)
        {
            //this.context = SingletonDatabase.Instance;
            this.myService = myService;

            employeePermissionDao = new EmployeePermissionDao(context);
            proxy = new AdminBookingController(context, myService);
        }

        [HttpGet]
        public IActionResult Booking()
        {
            int? employeeId = myService.GetEmployeeId();
            var checkPermission = employeePermissionDao.CheckPermission(employeeId, RentCheckOutType.RentCheckOutAll.ToString());

            if (checkPermission)
                return proxy.Booking();
            else
                return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        [HttpPost]
        public IActionResult Booking(BookingAdminVM bookingAdminVM)
        {
            int? employeeId = myService.GetEmployeeId();
            var checkPermission = employeePermissionDao.CheckPermission(employeeId, RentCheckOutType.RentCheckOutAll.ToString());

            if (checkPermission)
                return proxy.Booking(bookingAdminVM);
            else
                return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        [HttpGet]
        public IActionResult BookingDetails(int bookRoomDetailsId)
        {
            int? employeeId = myService.GetEmployeeId();
            var checkPermission = employeePermissionDao.CheckPermission(employeeId, RentCheckOutType.RentCheckOutAll.ToString());

            if (checkPermission)
                return proxy.BookingDetails(bookRoomDetailsId);
            else
                return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        [HttpGet]
        public IActionResult ChooseRoom()
        {
            int? employeeId = myService.GetEmployeeId();
            var checkPermission = employeePermissionDao.CheckPermission(employeeId, RentCheckOutType.RentCheckOutAll.ToString());

            if (checkPermission)
                return proxy.ChooseRoom();
            else
                return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        [HttpPost]
        public IActionResult EditBooking(BookingAdminVM bookingAdminVM)
        {
            int? employeeId = myService.GetEmployeeId();
            var checkPermission = employeePermissionDao.CheckPermission(employeeId, RentCheckOutType.RentCheckOutAll.ToString());

            if (checkPermission)
                return proxy.EditBooking(bookingAdminVM);
            else
                return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        [HttpGet]
        public IActionResult GetBooking()
        {
            int? employeeId = myService.GetEmployeeId();
            var checkPermission = employeePermissionDao.CheckPermission(employeeId, RentCheckOutType.RentCheckOutAll.ToString());

            if (checkPermission)
                return proxy.GetBooking();
            else
                return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        [HttpGet]
        public IActionResult Index()
        {
            int? employeeId = myService.GetEmployeeId();
            var checkPermission = employeePermissionDao.CheckPermission(employeeId, RentCheckOutType.RentCheckOutAll.ToString());

            if (checkPermission)
                return proxy.Index();
            else
                return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }
    }
}
