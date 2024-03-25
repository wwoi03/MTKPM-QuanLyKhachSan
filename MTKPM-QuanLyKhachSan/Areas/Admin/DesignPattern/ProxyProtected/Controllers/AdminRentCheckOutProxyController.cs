using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Areas.Admin.Controllers;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.ProxyProtected.Service;
using MTKPM_QuanLyKhachSan.Common;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.ProxyProtected.ProxyControllers
{
    [Area("Admin")]
    public class AdminRentCheckOutProxyController : Controller,IRentCheckOut
    {
        private IRentCheckOut adminRentCheckOut;
        private Employee employee;
        EmployeePermissionDao employeePermissionDao;
        List<EmployeePermission> listPermission;
        RoomDao roomDao;
        public AdminRentCheckOutProxyController(DatabaseContext context)
        {
            adminRentCheckOut = new AdminRentCheckOutController(context);
            roomDao = new RoomDao(context);
            //GiaBo
            employee = new Employee();
            employee.EmployeeId = 1;
            //Giabo
            //listPermission = new List<EmployeePermission>();
            employeePermissionDao = new EmployeePermissionDao(context);
            //listPermission = new List<EmployeePermission>();
            if (employee != null)
                listPermission = employeePermissionDao.GetPermissionByEmployee(employee.EmployeeId);
        }


        public IActionResult Index()
        {
            foreach (var item in listPermission)
            {
                if (item.PermissionId == PermissionRentCheckOutType.ViewRent.ToString())
                    return adminRentCheckOut.Index();
            }
            HttpContext.Session.SetString("Alert", "Bạn không có quyền xem danh sách thuê-trả phòng");
            return RedirectToAction("Index", "AdminBookingProxy");
        }

        public IActionResult ChangeRoom(int bookRoomDetailsId)
        {
            foreach (var item in listPermission)
            {
                if (item.PermissionId == PermissionRentCheckOutType.EditRent.ToString())
                    return adminRentCheckOut.ChangeRoom(bookRoomDetailsId);
            }
            HttpContext.Session.SetString("AlertChangeRoom", "Bạn không có quyền chỉnh sửa thông tin phòng thuê");
            return adminRentCheckOut.ChangeRoom(bookRoomDetailsId);

        }
        [HttpPost]
        public IActionResult CleanRoom(int roomId)
        {
            foreach (var item in listPermission)
            {
                if (item.PermissionId == PermissionRentCheckOutType.EditRent.ToString())
                    return adminRentCheckOut.CleanRoom(roomId);
            }
            HttpContext.Session.SetString("AlertCleanRoom", "Bạn không có quyền chỉnh sửa thông tin phòng thuê");
            return adminRentCheckOut.RoomClean();
        }
      

        [HttpGet]
        public IActionResult EditBookRoomDetails(int bookRoomDetailsId)
        {
            foreach (var item in listPermission)
            {
                if (item.PermissionId == PermissionRentCheckOutType.EditRent.ToString())
                    return adminRentCheckOut.EditBookRoomDetails(bookRoomDetailsId);
            }
            HttpContext.Session.SetString("AlertEditBookRoomDetails", "Bạn không có quyền chỉnh sửa thông tin phòng thuê");
            return adminRentCheckOut.EditBookRoomDetails(bookRoomDetailsId);
        }

        [HttpPost]
        public IActionResult OrderMenu(int bookRoomDetailsId, List<Order> orders)
        {
            foreach (var item in listPermission)
            {
                if (item.PermissionId == PermissionRentCheckOutType.CreateRent.ToString())
                    return adminRentCheckOut.OrderMenu(bookRoomDetailsId, orders);
            }
            HttpContext.Session.SetString("AlertOrderMenu", "Bạn không có quyền gọi món");
            return adminRentCheckOut.OrderMenu(bookRoomDetailsId, orders);

        }
        [HttpPost]
        public IActionResult RequestCleanRoom(int roomId)
        {
            Room room = roomDao.GetRoomById(roomId);
            foreach (var item in listPermission)
            {
                if (item.PermissionId == PermissionRentCheckOutType.EditRent.ToString())
                    return adminRentCheckOut.RequestCleanRoom(roomId);
            }
            HttpContext.Session.SetString("AlertRequestCleanRoom", "Bạn không có quyền chỉnh sửa thông tin phòng thuê");
            if (room.Status == 1)
            {
                return adminRentCheckOut.RoomRent();
            }
            else
            {
                return adminRentCheckOut.RoomWait();
            }
        }

        public IActionResult RoomClean()
        {
            return adminRentCheckOut.RoomClean();
        }

        public IActionResult RoomWait()
        {
            return adminRentCheckOut.RoomWait();
        }

        public IActionResult RoomRent()
        {
            return adminRentCheckOut.RoomRent();
        }

        public IActionResult RoomHistory()
        {
            return adminRentCheckOut.RoomHistory();
        }
    }
}
