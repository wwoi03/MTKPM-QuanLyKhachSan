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
    [Route("Admin/[controller]/[action]")]
    public class ProxyBookingController : Controller, IBooking
    {
        private AdminBookingController adminBooking;
        private Employee employee;
        PermissionDao permissionDao;
        

        public ProxyBookingController(DatabaseContext context)
        {
            adminBooking = new AdminBookingController(context);
            permissionDao = new PermissionDao(context);
            //Gia bo
            employee = new Employee();
            employee.EmployeeId = 1;
            //Chuyen doi employee -> string =json
            string json = JsonConvert.SerializeObject(employee);
            //HttpContext.Session.SetString("CurrentEmployee", json);
            //HttpContext.Session.SetInt32("1", 1);

            ////Nen vao session
            //json = HttpContext.Session.GetString("CurrentEmployee");

          
            //if (HttpContext.Session.GetString("CurrentEmployee") != null)
            //{
            //    // Deserialize chuỗi JSON thành đối tượng
            //    employee = JsonConvert.DeserializeObject<Employee>(json);
            //}
            
        }
        public IActionResult Index()
        {
            foreach(var permission in permissionDao.GetPermissionByEmployee(employee.EmployeeId))
            {
                //PermissionBookingType.DetailsRoom.ToString()
                if (permission.PermissionId == "1")
                {
                    return RedirectToAction("Index", "AdminSystemManager");
                }
            }
            return RedirectToAction("Index", "AdminRentCheckOut");
        }
        public IActionResult Booking()
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        public IActionResult BookingDetails(int Id)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public IActionResult BookingDetails()
        {
            throw new NotImplementedException();
        }
        public IActionResult ChooseRoom()
        {
            throw new NotImplementedException();
        }

        public IActionResult EditBooking(BookingAdminVM bookingAdminVM)
        {
            throw new NotImplementedException();
        }

        public IActionResult GetBooking()
        {
            throw new NotImplementedException();
        }


    }
}
