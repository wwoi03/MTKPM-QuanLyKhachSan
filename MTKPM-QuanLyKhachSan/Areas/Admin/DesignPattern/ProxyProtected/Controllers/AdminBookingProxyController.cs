using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Areas.Admin.Controllers;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.ProxyProtected.Services;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Singleton;
using MTKPM_QuanLyKhachSan.Common;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;
using MTKPM_QuanLyKhachSan.ViewModels;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.ProxyProtected.Controllers
{
    [Area("Admin")]
    public class AdminBookingProxyController : Controller, IBooking
    {
        private EmployeeDao employeeDao;
        private Employee employee;
        private EmployeePermissionDao employeePermissionDao;
        private List<EmployeePermission> employeePermissions;
        private IBooking proxy;

        public AdminBookingProxyController()
        {
            employeeDao = new EmployeeDao();
            employeePermissionDao = new EmployeePermissionDao();

            proxy = new AdminBookingController();
        }

        [HttpGet]
        public IActionResult Booking()
        {
            InitData();

            foreach (var permission in employeePermissions)
            {
                if (RentCheckOutType.RentCheckOutAll.ToString().Equals(permission.Permission.PermissionId))
                {
                    return proxy.Booking();
                }
            }

            return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        [HttpPost]
        public IActionResult Booking(BookingAdminVM bookingAdminVM)
        {
            InitData();

            foreach (var permission in employeePermissions)
            {
                if (RentCheckOutType.RentCheckOutAll.ToString().Equals(permission.Permission.PermissionId))
                {
                    return proxy.Booking(bookingAdminVM);
                }
            }

            return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        [HttpGet]
        public IActionResult BookingDetails(int bookRoomDetailsId)
        {
            InitData();

            foreach (var permission in employeePermissions)
            {
                if (RentCheckOutType.RentCheckOutAll.ToString().Equals(permission.Permission.PermissionId))
                {
                    return proxy.BookingDetails(bookRoomDetailsId);
                }
            }

            return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        public IActionResult ChooseRoom()
        {
            InitData();

            foreach (var permission in employeePermissions)
            {
                if (RentCheckOutType.RentCheckOutAll.ToString().Equals(permission.Permission.PermissionId))
                {
                    return proxy.ChooseRoom();
                }
            }

            return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        [HttpPost]
        public IActionResult EditBooking(BookingAdminVM bookingAdminVM)
        {
            InitData();

            foreach (var permission in employeePermissions)
            {
                if (RentCheckOutType.RentCheckOutAll.ToString().Equals(permission.Permission.PermissionId))
                {
                    return proxy.EditBooking(bookingAdminVM);
                }
            }

            return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }
        [HttpGet]
        public IActionResult GetBooking()
        {
            employee = employeeDao.GetEmployeeById(HttpContext.Session.GetInt32("EmployeeId"));
            employeePermissions = employeePermissionDao.GetPermissionByEmployee(employee.EmployeeId);
            InitData();

            foreach (var permission in employeePermissions)
            {
                if (RentCheckOutType.RentCheckOutAll.ToString().Equals(permission.Permission.PermissionId))
                {
                    return proxy.GetBooking();
                }
            }

            return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        public IActionResult Index()
        {
            employee = employeeDao.GetEmployeeById(HttpContext.Session.GetInt32("EmployeeId"));
            employeePermissions = employeePermissionDao.GetPermissionByEmployee(employee.EmployeeId);

            foreach (var permission in employeePermissions)
            {
                if (RentCheckOutType.RentCheckOutAll.ToString().Equals(permission.Permission.PermissionId))
                {
                    return proxy.Index();
                }
            }

            return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        public void InitData()
        {
            employee = employeeDao.GetEmployeeById(HttpContext.Session.GetInt32("EmployeeId"));
            employeePermissions = employeePermissionDao.GetPermissionByEmployee(employee.EmployeeId);
        }
    }
}
