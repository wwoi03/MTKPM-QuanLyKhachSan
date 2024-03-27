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
    [Route("Admin/[controller]/[action]")]
    public class AdminRentCheckOutProxyController : Controller, IRentCheckOut
    {
        private DatabaseContext context;
        private EmployeePermissionDao employeePermissionDao;
        private IRentCheckOut proxy;
        private IService myService;

        public AdminRentCheckOutProxyController(IService myService)
        {
            context = SingletonDatabase.Instance;
            this.myService = myService;
            employeePermissionDao = new EmployeePermissionDao(context);
            proxy = new AdminRentCheckOutController(context, myService);
        }

        public IActionResult ChangeRoom(int bookRoomDetailsId)
        {
            int? employeeId = myService.GetEmployeeId();
            var checkPermission = employeePermissionDao.CheckPermission(employeeId, RentCheckOutType.RentCheckOutAll.ToString());

            if (checkPermission)
                return proxy.ChangeRoom(bookRoomDetailsId);
            else
                return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        [HttpPost]
        public IActionResult ChangeRoom(int roomIdOld, int roomIdNew, bool isCleanRoom = false)
        {
            int? employeeId = myService.GetEmployeeId();
            var checkPermission = employeePermissionDao.CheckPermission(employeeId, RentCheckOutType.RentCheckOutAll.ToString());

            if (checkPermission)
                return proxy.ChangeRoom(roomIdOld, roomIdNew, isCleanRoom);
            else
                return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        [HttpPost]
        public IActionResult CleanRoom(int roomId)
        {
            int? employeeId = myService.GetEmployeeId();
            var checkPermission = employeePermissionDao.CheckPermission(employeeId, RentCheckOutType.RentCheckOutAll.ToString());

            if (checkPermission)
                return proxy.CleanRoom(roomId);
            else
                return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        public IActionResult EditBookRoomDetails(int bookRoomDetailsId)
        {
            int? employeeId = myService.GetEmployeeId();
            var checkPermission = employeePermissionDao.CheckPermission(employeeId, RentCheckOutType.RentCheckOutAll.ToString());

            if (checkPermission)
                return proxy.EditBookRoomDetails(bookRoomDetailsId);
            else
                return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        [HttpPost]
        public IActionResult EditBookRoomDetails(BookRoomDetailsAdminVM bookRoomDetailsAdminVM)
        {
            int? employeeId = myService.GetEmployeeId();
            var checkPermission = employeePermissionDao.CheckPermission(employeeId, RentCheckOutType.RentCheckOutAll.ToString());

            if (checkPermission)
                return proxy.EditBookRoomDetails(bookRoomDetailsAdminVM);
            else
                return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        public IActionResult Index()
        {
            int? employeeId = myService.GetEmployeeId();
            var checkPermission = employeePermissionDao.CheckPermission(employeeId, RentCheckOutType.RentCheckOutAll.ToString());

            if (checkPermission)
                return proxy.Index();
            else
                return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        public IActionResult OrderMenu(int bookRoomDetailsId)
        {
            int? employeeId = myService.GetEmployeeId();
            var checkPermission = employeePermissionDao.CheckPermission(employeeId, RentCheckOutType.RentCheckOutAll.ToString());

            if (checkPermission)
                return proxy.OrderMenu(bookRoomDetailsId);
            else
                return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        [HttpPost]
        public IActionResult OrderMenu(int bookRoomDetailsId, List<Order> orders)
        {
            int? employeeId = myService.GetEmployeeId();
            var checkPermission = employeePermissionDao.CheckPermission(employeeId, RentCheckOutType.RentCheckOutAll.ToString());

            if (checkPermission)
                return proxy.OrderMenu(bookRoomDetailsId, orders);
            else
                return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        [HttpPost]
        public IActionResult RequestCleanRoom(int roomId)
        {
            int? employeeId = myService.GetEmployeeId();
            var checkPermission = employeePermissionDao.CheckPermission(employeeId, RentCheckOutType.RentCheckOutAll.ToString());

            if (checkPermission)
                return proxy.RequestCleanRoom(roomId);
            else
                return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        public IActionResult RoomClean()
        {
            int? employeeId = myService.GetEmployeeId();
            var checkPermission = employeePermissionDao.CheckPermission(employeeId, RentCheckOutType.RentCheckOutAll.ToString());

            if (checkPermission)
                return proxy.RoomClean();
            else
                return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        public IActionResult RoomHistory()
        {
            int? employeeId = myService.GetEmployeeId();
            var checkPermission = employeePermissionDao.CheckPermission(employeeId, RentCheckOutType.RentCheckOutAll.ToString());

            if (checkPermission)
                return proxy.RoomHistory();
            else
                return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        public IActionResult RoomRent()
        {
            int? employeeId = myService.GetEmployeeId();
            var checkPermission = employeePermissionDao.CheckPermission(employeeId, RentCheckOutType.RentCheckOutAll.ToString());

            if (checkPermission)
                return proxy.RoomRent();
            else
                return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        public IActionResult RoomWait()
        {
            int? employeeId = myService.GetEmployeeId();
            var checkPermission = employeePermissionDao.CheckPermission(employeeId, RentCheckOutType.RentCheckOutAll.ToString());

            if (checkPermission)
                return proxy.RoomWait();
            else
                return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }
    }
}
