using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Controllers
{
    public class PublicHomeController : Controller
    {
        EmployeeDao employeeDao;
        CustomerDao customerDao;
        RoomDao roomDao;

        public PublicHomeController(DatabaseContext context)
        {
            employeeDao = new EmployeeDao(context);
            customerDao = new CustomerDao(context);
            roomDao = new RoomDao(context);
        }
        public IActionResult Index()
        {
            ViewBag.PageTitle = "Trang chủ";
            ViewBag.employees = employeeDao.GetQuantityOfEmployee();
            ViewBag.customers = customerDao.GetQuantityOfCustomer();
            ViewBag.rooms = roomDao.GetQuantityOfRoom();
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Service()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
