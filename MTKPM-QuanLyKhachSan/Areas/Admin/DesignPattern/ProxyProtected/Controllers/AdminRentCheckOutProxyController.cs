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
    [Route("Admin/[controller]/[action]")]
    public class AdminRentCheckOutProxyController : Controller, IRentCheckOut
    {
        private DatabaseContext context;
        private EmployeeDao employeeDao;
        private Employee employee;
        private EmployeePermissionDao employeePermissionDao;
        private List<EmployeePermission> employeePermissions;
        private IRentCheckOut proxy;

        public AdminRentCheckOutProxyController()
        {
            employeeDao = new EmployeeDao();
            employeePermissionDao = new EmployeePermissionDao();

            proxy = new AdminRentCheckOutController();
        }

        public IActionResult ChangeRoom(int bookRoomDetailsId)
        {
            InitData();
            foreach (var permission in employeePermissions)
            {
                if (RentCheckOutType.RentCheckOutAll.ToString().Equals(permission.Permission.PermissionId))
                {
                    return proxy.ChangeRoom(bookRoomDetailsId);
                }
            }

            return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }
        [HttpPost]
        public IActionResult ChangeRoom(int roomIdOld, int roomIdNew, bool isCleanRoom = false)
        {
            InitData();
            foreach (var permission in employeePermissions)
            {
                if (RentCheckOutType.RentCheckOutAll.ToString().Equals(permission.Permission.PermissionId))
                {
                    return proxy.ChangeRoom(roomIdOld, roomIdNew, isCleanRoom);
                }
            }

            return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }
        [HttpPost]
        public IActionResult CleanRoom(int roomId)
        {
            InitData();
            foreach (var permission in employeePermissions)
            {
                if (RentCheckOutType.RentCheckOutAll.ToString().Equals(permission.Permission.PermissionId))
                {
                    return proxy.CleanRoom(roomId);
                }
            }

            return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        public IActionResult EditBookRoomDetails(int bookRoomDetailsId)
        {
            InitData();
            foreach (var permission in employeePermissions)
            {
                if (RentCheckOutType.RentCheckOutAll.ToString().Equals(permission.Permission.PermissionId))
                {
                    return proxy.EditBookRoomDetails(bookRoomDetailsId);
                }
            }

            return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }
        [HttpPost]
        public IActionResult EditBookRoomDetails(BookRoomDetailsAdminVM bookRoomDetailsAdminVM)
        {
            InitData();
            foreach (var permission in employeePermissions)
            {
                if (RentCheckOutType.RentCheckOutAll.ToString().Equals(permission.Permission.PermissionId))
                {
                    return proxy.EditBookRoomDetails(bookRoomDetailsAdminVM);
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

        public IActionResult OrderMenu(int bookRoomDetailsId)
        {
            InitData();
            foreach (var permission in employeePermissions)
            {
                if (RentCheckOutType.RentCheckOutAll.ToString().Equals(permission.Permission.PermissionId))
                {
                    return proxy.OrderMenu(bookRoomDetailsId);
                }
            }

            return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        [HttpPost]
        public IActionResult OrderMenu(int bookRoomDetailsId, List<Order> orders)
        {
            InitData();
            foreach (var permission in employeePermissions)
            {
                if (RentCheckOutType.RentCheckOutAll.ToString().Equals(permission.Permission.PermissionId))
                {
                    return proxy.OrderMenu(bookRoomDetailsId, orders);
                }
            }

            return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }
        [HttpPost]
        public IActionResult RequestCleanRoom(int roomId)
        {
            InitData();
            foreach (var permission in employeePermissions)
            {
                if (RentCheckOutType.RentCheckOutAll.ToString().Equals(permission.Permission.PermissionId))
                {
                    return proxy.RequestCleanRoom(roomId);
                }
            }

            return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        public IActionResult RoomClean()
        {
            InitData();
            foreach (var permission in employeePermissions)
            {
                if (RentCheckOutType.RentCheckOutAll.ToString().Equals(permission.Permission.PermissionId))
                {
                    return proxy.RoomClean();
                }
            }

            return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        public IActionResult RoomHistory()
        {
            InitData();
            foreach (var permission in employeePermissions)
            {
                if (RentCheckOutType.RentCheckOutAll.ToString().Equals(permission.Permission.PermissionId))
                {
                    return proxy.RoomHistory();
                }
            }

            return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        public IActionResult RoomRent()
        {
            InitData();
            foreach (var permission in employeePermissions)
            {
                if (RentCheckOutType.RentCheckOutAll.ToString().Equals(permission.Permission.PermissionId))
                {
                    return proxy.RoomRent();
                }
            }

            return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        public IActionResult RoomWait()
        {
            InitData();

            foreach (var permission in employeePermissions)
            {
                if (RentCheckOutType.RentCheckOutAll.ToString().Equals(permission.Permission.PermissionId))
                {
                    return proxy.RoomWait();
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
