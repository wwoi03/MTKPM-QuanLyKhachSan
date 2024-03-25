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

        public ProxyBookingController()
        {
            adminBooking = new AdminBookingController();
            employeePermissionDao = new EmployeePermissionDao();
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
            //if (TempData["Alert"] != null)
            //{
            //    HttpContext.Session.SetString("Alert", TempData["Alert"].ToString());
            //}
            if (employee != null)
            {
                return RedirectToAction("Index", "AdminBooking");
            }
            else
            {
                //HttpContext.Session.SetString("Alert", TempData["Alert"].ToString());
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
            //HttpContext.Session.SetString("AlertGetBooking", "Bạn không có quyền xem đặt phòng");
            //ViewBag.AlertMessage = HttpContext.Session.GetString("AlertGetBooking");
            //return adminBooking.GetBooking();
            return RedirectToAction("Index", "AdminBooking");
        }

        [HttpGet]
        public IActionResult Booking()
        {
            foreach (var item in listPermission)
            {
                if (item.PermissionId == PermissionBookingType.CreateBooking.ToString())
                    return RedirectToAction("Booking", "AdminBooking");
            }
            HttpContext.Session.SetString("AlertBooking", "Bạn không có quyền đặt phòng");
            return RedirectToAction("Booking", "AdminBooking");
        }



        [HttpGet]
        public IActionResult BookingDetails(int Id)
        {
            foreach (var item in listPermission)
            {
                if (item.PermissionId == PermissionBookingType.DetailsBooking.ToString())
                    return RedirectToAction("BookingDetails", "AdminBooking", new { bookRoomDetailsId = Id});
            }
            HttpContext.Session.SetString("AlertBookingDetails", "Bạn không có quyền xem chi tiết đặt phòng");
            return RedirectToAction("BookingDetails", "AdminBooking", new { bookRoomDetailsId = Id });
        }

        public IActionResult EditBooking(BookingAdminVM bookingAdminVM)
        {
            foreach (var item in listPermission)
            {
                if (item.PermissionId == PermissionBookingType.EditBooking.ToString())
                    return adminBooking.EditBooking(bookingAdminVM);
            }
            //HttpContext.Session.SetString("AlertEditBooking", "Bạn không có quyền sửa đặt phòng");
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
