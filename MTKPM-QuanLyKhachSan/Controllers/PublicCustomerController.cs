using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;
using MTKPM_QuanLyKhachSan.ViewModels;

namespace MTKPM_QuanLyKhachSan.Controllers
{
    public class PublicCustomerController : Controller
    {
        //Khai báo các biến
        CustomerDao customerDao;
        BookRoomDao bookRoomDao;
        //Tạo controller
        public PublicCustomerController(DatabaseContext context)
        {
            customerDao = new CustomerDao(context);
            bookRoomDao = new BookRoomDao(context);
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
            HttpContext.Session.Remove("Username");
            HttpContext.Session.Remove("Name");
            HttpContext.Session.Remove("Password");
            HttpContext.Session.Remove("CIC");
            HttpContext.Session.Remove("Phone");
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("Address");
            HttpContext.Session.Remove("CustomerId");
            return RedirectToAction("Login", "PublicCustomer");
        }

        public IActionResult Information()
        {
			ViewBag.PageHeader = "Thông Tin Khách Hàng";
			if (HttpContext.Session.GetInt32("CustomerId") == null)
			{
				return RedirectToAction("Login", "PublicCustomer");
			}
			ViewBag.Information = customerDao.GetCustomerbyId(HttpContext.Session.GetInt32("CustomerId"));
			return View(); ;
        }

        public IActionResult EditInformation()
        {       
			ViewData["PageTitle"] = "Edit Information";
			if (HttpContext.Session.GetInt32("CustomerId") == null)
			{
				return RedirectToAction("Login", "PublicCustomer");
			}

			Customer customer = customerDao.GetCustomerbyId(HttpContext.Session.GetInt32("CustomerId"));
			CustomerVM customerVM = new CustomerVM()
			{
                CustomerId = customer.CustomerId,
				Username = customer.Username,
                Password = customer.Password,
                Name = customer.Name,
                CIC = customer.CIC,
                Phone = customer.Phone,
                Email   = customer.Email,
                Address = customer.Address,

			};

			if (customerVM == null)
				return NotFound();
			return View(customerVM);
		}
        [HttpPost]
        public IActionResult EditInformation(CustomerVM customerVM)
        {
			ModelState.Remove("Password");
			ModelState.Remove("CustomerId");
			ModelState.Remove("Username");
			ModelState.Remove("VerifyPassword");
            if (!ModelState.IsValid)
            {
                return View(customerVM);
            }
            Customer newCustomer = new Customer()
            {
				CustomerId = (int)HttpContext.Session.GetInt32("CustomerId"),
				Username = customerVM.Username,
                Password = customerVM.Password,
                Name = customerVM.Name,
                CIC = customerVM.CIC,
                Phone = customerVM.Phone,
                Email = customerVM.Email,
                Address = customerVM.Address,
            };
            HttpContext.Session.SetString("Name", newCustomer.Name);

			customerDao.EditInformation(newCustomer);
			return RedirectToAction("Information");
		}

        //Hàm thực hiện chức năng 'Xem lịch sử đặt phòng'
        public IActionResult HistoryBooking()
        {
            // Kiểm tra xem khách hàng đã đăng nhập chưa
            if (HttpContext.Session.GetInt32("CustomerId") == null)
            {
                return RedirectToAction("Login", "PublicCustomer");
            }
            // Lấy Id của khách hàng từ session
            int customerId = HttpContext.Session.GetInt32("CustomerId").Value;
            // Lấy lịch sử đặt phòng của khách hàng từ cơ sở dữ liệu
            var bookingHistory = bookRoomDao.GetBookingHistory(customerId);
            // Khởi tạo view model và truyền dữ liệu
            var viewModel = new BookingHistory
            {
                BookingList = bookingHistory
            };
            // Trả về view với dữ liệu
            return View(viewModel);
        }

        public IActionResult ChangePassword()
        {
			ViewData["PageTitle"] = "Edit Information";
			if (HttpContext.Session.GetInt32("CustomerId") == null)
			{
				return RedirectToAction("Login", "PublicCustomer");
			}

			Customer customer = customerDao.GetCustomerbyId(HttpContext.Session.GetInt32("CustomerId"));
			CustomerVM customerVM = new CustomerVM()
			{
				CustomerId = customer.CustomerId,
				Username = customer.Username,
				Password = customer.Password,
				Name = customer.Name,
				CIC = customer.CIC,
				Phone = customer.Phone,
				Email = customer.Email,
				Address = customer.Address,
			};

			if (customerVM == null)
				return NotFound();
			return View(customerVM);
		}
		[HttpPost]
        public IActionResult ChangePassword(CustomerVM customerVM)
        {
			ModelState.Remove("CustomerId");
			ModelState.Remove("Username");
			ModelState.Remove("Name");
			ModelState.Remove("CIC");
			ModelState.Remove("Phone");
			ModelState.Remove("Email");
			ModelState.Remove("Address");
			
			if (customerVM.Password.Equals(customerVM.VerifyPassword))
			{	
				Customer newCustomer = new Customer()
                {
                    CustomerId = (int)HttpContext.Session.GetInt32("CustomerId"),
                    Username = customerVM.Username,
                    Password = customerVM.Password,
                    Name = customerVM.Name,
                    CIC = customerVM.CIC,
                    Phone = customerVM.Phone,
                    Email = customerVM.Email,
                    Address = customerVM.Address,
                };
				customerDao.EditInformation(newCustomer);
				return RedirectToAction("Information");
			}
			else
			{
				ViewBag.messageError = "Mật khẩu xác nhận không khớp";
			}
            return View(customerVM);

		}
	}
}