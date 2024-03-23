using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Areas.Admin.Controllers;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.ProxyProtected.Service;
using MTKPM_QuanLyKhachSan.Common;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;
using MTKPM_QuanLyKhachSan.ViewModels;
using Newtonsoft.Json;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.ProxyProtected.ProxyControllers
{
    [Area("Admin")]
    public class ProxyBookingController : Controller, IBooking
    {
        private IBooking adminBooking;
        private Employee employee;
        EmployeePermissionDao employeePermissionDao;
        List<EmployeePermission> listPermission;

        public ProxyBookingController(DatabaseContext context)
        {
            adminBooking = new AdminBookingController(context);
            employeePermissionDao = new EmployeePermissionDao(context);
            //Gia bo
            employee = new Employee();
            employee.EmployeeId = 1;
            //Chuyen doi employee -> string =json
            //string json = JsonConvert.SerializeObject(employee);
            //HttpContext.Session.SetString("CurrentEmployee", json);
            //HttpContext.Session.SetInt32("1", 1);

            ////Nen vao session
            //json = HttpContext.Session.GetString("CurrentEmployee");


            //if (HttpContext.Session.GetString("CurrentEmployee") != null)
            //{
            //    // Deserialize chuỗi JSON thành đối tượng
            //    employee = JsonConvert.DeserializeObject<Employee>(json);
            //}
            if (employee != null)
                listPermission = employeePermissionDao.GetPermissionByEmployee(employee.EmployeeId);
        }
        public IActionResult Index()
        {
            if (employee != null)
            {
                if (TempData["Alert"] != null)
                {
                    HttpContext.Session.SetString("Alert", TempData["Alert"].ToString());
                }
                return RedirectToAction("Index", "AdminBooking");
            }
            else
            {
                HttpContext.Session.SetString("Alert", "Bạn không có quyền truy cập vào trang quản lý");
                return RedirectToAction("Index", "PublicHome");
            }
        }

      
        public IActionResult GetBooking()
        {
            foreach (var item in listPermission)
            {
                if (item.PermissionId == PermissionBookingType.ViewBooking.ToString())
                    return adminBooking.GetBooking();
            }
            TempData["Alert"] = "Bạn không có quyền xem đặt phòng";
            return RedirectToAction("Index", "ProxyBooking");
        }

        [HttpGet]
        public IActionResult Booking()
        {
            foreach (var item in listPermission)
            {
                if (item.PermissionId == PermissionBookingType.CreateBooking.ToString())
                    return RedirectToAction("Booking", "AdminBooking");
            }
            TempData["Alert"] = "Bạn không có quyền đặt phòng";
            return RedirectToAction("Index", "ProxyBooking");
        }
      

            
        [HttpGet]
        public IActionResult BookingDetails(int Id)
        {
            foreach (var item in listPermission)
            {
                if (item.PermissionId == PermissionBookingType.DetailsBooking.ToString())
                    return RedirectToAction("BookingDetails", "AdminBooking", new { bookRoomDetailsId = Id});
            }
            TempData["Alert"] = "Bạn không có quyền xem chi tiết đặt phòng";
            return RedirectToAction("Index", "ProxyBooking");
        }

        public IActionResult EditBooking(BookingAdminVM bookingAdminVM)
        {
            foreach (var item in listPermission)
            {
                if (item.PermissionId == PermissionBookingType.EditBooking.ToString())
                    return adminBooking.EditBooking(bookingAdminVM);
            }
            TempData["Alert"] = "Bạn không có quyền sửa đặt phòng";
            return RedirectToAction("Index", "ProxyBooking");
        }

        public IActionResult ChooseRoom()
        {
            return RedirectToAction("ChooseRoom", "AdminBooking");
        }





        //public IActionResult Alert()
        //{
        //    string value= "shkuhukshiukshuks";
        //    if (TempData.ContainsKey("Alert"))
        //    {
        //        // TempData contains the key
        //        value = TempData["Alert"].ToString();
        //        return Json(value);
        //    }
        //    else
        //    {
        //        return Json(value);
        //    }
        //}
    }
}
