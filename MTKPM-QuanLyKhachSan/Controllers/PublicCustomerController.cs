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
        //Đăng ký
        [HttpGet]

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(CustomerVM customerVM)
        {
            ModelState.Remove("CustomerId");
            ModelState.Remove("CIC");
            ModelState.Remove("Phone");
            ModelState.Remove("Email");
            ModelState.Remove("Address");
            if (ModelState.IsValid)
            {
                Customer customerRegister = customerDao.GetCustomerByUserName(customerVM.Username);
                if (customerRegister == null)
                {
                    if (customerVM.Password.Equals(customerVM.VerifyPassword))
                    {
                        Customer customer = new Customer()
                        {
                            Username = customerVM.Username,
                            CIC = customerVM.CIC,
                            Phone = customerVM.Phone,
                            Email = customerVM.Email,
                            Address = customerVM.Address,
                            Name = customerVM.Name,
                            Password = customerVM.Password
                        };

                        customerDao.CreateCustomer(customer);

                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ViewBag.messageError = "Mật khẩu xác nhận không khớp";
                    }
                }
                else
                {
                    ViewBag.messageError = "Tài khoản đã tồn tại";
                }
            }
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