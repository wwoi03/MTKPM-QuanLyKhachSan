using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Singleton;
using MTKPM_QuanLyKhachSan.Common.Config;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;
using MTKPM_QuanLyKhachSan.ViewModels;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminUserController : Controller
    {
        EmployeeDao employeeDao;
        IService myService;

        public AdminUserController(IService myService)
        {
            this.myService = myService;
            employeeDao = new EmployeeDao(SingletonDatabase.Instance);
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (myService.GetEmployeeId() != null)
            {
                return RedirectToAction("Index", "AdminHome");
            }

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

                    return RedirectToAction("Index", "AdminHome");
                }

                return View();
            }

            return View();
        }
    }
}
