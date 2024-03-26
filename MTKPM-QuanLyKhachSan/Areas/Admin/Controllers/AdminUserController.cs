using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Singleton;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;
using MTKPM_QuanLyKhachSan.ViewModels;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminUserController : Controller
    {
        EmployeeDao employeeDao;

        public AdminUserController()
        {
            DatabaseContext context = SingletonDatabase.Instance;

            employeeDao = new EmployeeDao(context);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginVM());
        }

        [HttpPost]
        public IActionResult Login(LoginVM loginVM)
        {
            if (HttpContext.Session.GetString("EmployeeId") == null)
            {
                var employee = employeeDao.Login(loginVM.Username, loginVM.Password);

                if (employee != null)
                {
                    HttpContext.Session.SetInt32("EmployeeId", employee.EmployeeId);
                    HttpContext.Session.SetString("EmployeeName", employee.Name);
                    HttpContext.Session.SetInt32("HotelId", employee.HotelId);

                    return RedirectToAction("Index", "AdminBookingProxy");
                }

                return View();
            }

            return View();
        }
    }
}
