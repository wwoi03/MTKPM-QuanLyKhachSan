using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Controllers
{
    public class PublicHomeController : Controller
    {
        //Khai báo các biến 
        EmployeeDao employeeDao;
        CustomerDao customerDao;
        RoomDao roomDao;

        //Tạo controller
        public PublicHomeController(DatabaseContext context)
        {
            employeeDao = new EmployeeDao(context);
            customerDao = new CustomerDao(context);
            roomDao = new RoomDao(context);
        }
        public IActionResult Index()
        {
            //Tiêu đề trang
            ViewBag.PageTitle = "Trang chủ";
			//Hiển thị số lượng nhân viên lên trang chủ
			ViewBag.employees = employeeDao.GetQuantityOfEmployee();
			//Hiển thị số lượng khách hàng lên trang chủ
			ViewBag.customers = customerDao.GetQuantityOfCustomer();
			//Hiển thị số lượng phòng lên trang chủ
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
