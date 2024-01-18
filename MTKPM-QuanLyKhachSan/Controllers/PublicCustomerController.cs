using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;
using MTKPM_QuanLyKhachSan.ViewModels;

namespace MTKPM_QuanLyKhachSan.Controllers
{
    public class PublicCustomerController : Controller
    {
        CustomerDao customerDao;

        public PublicCustomerController(DatabaseContext context)
        {
            customerDao = new CustomerDao(context);
        }

        //Đăng nhập
        [HttpGet]
		public IActionResult Login()
        {
			if (HttpContext.Session.GetString("Email") == null)
			{
				return View();
			}
			else
			{
				return RedirectToAction("Index", "PublicHome");
			}
		}
		[HttpPost]
		public IActionResult Login(CustomerVM customerVM)
		{
			ModelState.Remove("Name");
            ModelState.Remove("CustomerId");
            ModelState.Remove("CIC");
            ModelState.Remove("Phone");
            ModelState.Remove("Email");
            ModelState.Remove("Address");
            ModelState.Remove("VerifyPassword");

			if (ModelState.IsValid)
			{
				Customer customer = customerDao.GetCustomerByUserName(customerVM.Username);

				if (customer != null && customer.Password.Equals(customerVM.Password))
				{
					HttpContext.Session.SetString("Username", customer.Username);
					HttpContext.Session.SetString("Name", customer.Name);
					HttpContext.Session.SetInt32("CustomerId", customer.CustomerId);
					return RedirectToAction("Index", "PublicHome");
				}

				ViewBag.messageError = "Tên đăng nhập hoặc mật khẩu không chính xác!";
			}
			return View();
		}

		public IActionResult Register()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }

        public IActionResult Information()
        {
            return View();
        }

        public IActionResult HistoryBooking()
        {
            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }
    }
}
