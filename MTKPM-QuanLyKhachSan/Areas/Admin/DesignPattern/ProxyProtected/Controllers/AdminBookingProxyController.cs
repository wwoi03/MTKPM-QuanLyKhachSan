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
    public class AdminBookingProxyController : Controller, IBooking
    {
        private IBooking adminBooking;
        private Employee employee;
        EmployeePermissionDao employeePermissionDao;
        List<EmployeePermission> listPermission;

        public AdminBookingProxyController(DatabaseContext context)
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
            listPermission = new List<EmployeePermission>();
            if (employee != null)
                listPermission = employeePermissionDao.GetPermissionByEmployee(employee.EmployeeId);
        }
        public IActionResult Index()
        {
            //HttpContext.Session.SetInt32("EmployeeId", 1);
            //HttpContext.Session.SetString("EmployeeName", "Đào Công Tuấn");
            //HttpContext.Session.SetInt32("HotelId", 1);
            if (employee != null)
            {
                if (HttpContext.Session.GetString("Alert") != null)
                {
                    ViewBag.AlertMessage = HttpContext.Session.GetString("Alert");
                    HttpContext.Session.Remove("Alert");
                }
                return adminBooking.Index();
            }
            else
            {
                //return RedirectToAction("Index", "PublicHome");
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
            HttpContext.Session.SetString("Alert", "Bạn không có quyền xem đặt phòng");
           
            return adminBooking.Index();
        }

        [HttpGet]
        public IActionResult Booking()
        {
            foreach (var item in listPermission)
            {
                if (item.PermissionId == PermissionBookingType.CreateBooking.ToString())
                    return adminBooking.Booking();
            }
            HttpContext.Session.SetString("AlertBooking", "Bạn không có quyền đặt phòng");
            return adminBooking.Booking();
        }
       


        [HttpGet]
        public IActionResult BookingDetails(int Id)
        {
            foreach (var item in listPermission)
            {
                if (item.PermissionId == PermissionBookingType.DetailsBooking.ToString())
                    return adminBooking.BookingDetails(Id);
            }
            HttpContext.Session.SetString("AlertBookingDetails", "Bạn không có quyền xem chi tiết đặt phòng");
            return adminBooking.BookingDetails(Id);
        }

        public IActionResult EditBooking(BookingAdminVM bookingAdminVM)
        {
            foreach (var item in listPermission)
            {
                if (item.PermissionId == PermissionBookingType.EditBooking.ToString())
                    return adminBooking.EditBooking(bookingAdminVM);
            }
            //HttpContext.Session.SetString("AlertEditBooking", "Bạn không có quyền sửa đặt phòng");
            return adminBooking.Index();
        }

        public IActionResult ChooseRoom()
        {
            return adminBooking.ChooseRoom();
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
