using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.ProxyProtected.Service;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;
using MTKPM_QuanLyKhachSan.ViewModels;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class AdminRentCheckOutController : Controller,IRentCheckOut
    {
        RoomTypeDao roomTypeDao;
        RoomDao roomDao;
        BookRoomDao bookRoomDao;
        BookRoomDetailsDao bookRoomDetailsDao;
        BillDao billDao;
        OrderDao orderDao;
        ServiceDao serviceDao;
        CustomerDao customerDao;


        public AdminRentCheckOutController(DatabaseContext context)
        {
            roomTypeDao = new RoomTypeDao(context);
            roomDao = new RoomDao(context);
            bookRoomDao = new BookRoomDao(context);
            bookRoomDetailsDao = new BookRoomDetailsDao(context);
            billDao = new BillDao(context);
            orderDao = new OrderDao(context);
            serviceDao = new ServiceDao(context);
            customerDao = new CustomerDao(context);
        }

        public IActionResult Index()
        {
            HttpContext.Session.SetInt32("EmployeeId", 1);
            HttpContext.Session.SetString("EmployeeName", "Đào Công Tuấn");
            HttpContext.Session.SetInt32("HotelId", 1);
            //thong bao
            if (HttpContext.Session.GetString("Alert") != null)
                ViewBag.AlertMessage = HttpContext.Session.GetString("Alert");

            return View();
        }

        // danh sách phòng chờ
        public IActionResult RoomWait()
        {
            //thong bao
            if (HttpContext.Session.GetString("AlertRequestCleanRoom") != null)
            {
                ViewBag.AlertMessage = HttpContext.Session.GetString("AlertRequestCleanRoom");
                HttpContext.Session.Remove("AlertRequestCleanRoom");
            }

            var roomWaits = roomDao.GetEmptyRooms(HttpContext.Session.GetInt32("HotelId"));

            ViewBag.roomTypes = roomTypeDao.GetRoomTypes(HttpContext.Session.GetInt32("HotelId"));
            ViewBag.roomWaits = roomWaits.Select(roomWait => new RoomWaitVM
            {
                RoomId = roomWait.RoomId,
                RoomTypeId = roomWait.RoomTypeId,
                RoomName = roomWait.Name,
                Status = roomWait.Status,
                Tidy = roomWait.Tidy,
            });

            return PartialView();
        }

        // danh sách phòng đang thuê
        public IActionResult RoomRent()
        {
            //thong bao
            if (HttpContext.Session.GetString("AlertRequestCleanRoom") != null)
            {
                ViewBag.AlertMessage = HttpContext.Session.GetString("AlertRequestCleanRoom");
                HttpContext.Session.Remove("AlertRequestCleanRoom");
            }

            ViewBag.roomTypes = roomTypeDao.GetRoomTypes(HttpContext.Session.GetInt32("HotelId"));

            var roomRents = bookRoomDetailsDao.GetBookRoomDetailsReceive(HttpContext.Session.GetInt32("HotelId"));

            ViewBag.roomRents = roomRents.Select(roomRent => new RoomRentVM
			{
                CustomerName = roomRent.BookRoom.Customer.Name,
                BookRoomDetailsId = roomRent.BookRoomDetailsId,
                RoomId = roomRent.RoomId,
                RoomTypeId = roomRent.Room.RoomTypeId,
                RoomName = roomRent.Room.Name,
                Tidy = roomRent.Room.Tidy,
                Note = roomRent.Note,
                CheckIn = roomRent.CheckIn,
                TotalPrice = orderDao.CalcOrderPrice(roomRent.BookRoomDetailsId) + roomRent.Room.RoomType.Price,
                QuantityMenu = orderDao.CalcOrderQuantity(roomRent.BookRoomDetailsId),
            }).ToList();

            return PartialView();
        }

        // danh sách phòng cần dọn
        public IActionResult RoomClean()
        {
            //thong bao
            if (HttpContext.Session.GetString("AlertCleanRoom") != null)
            {
                ViewBag.AlertMessage = HttpContext.Session.GetString("AlertCleanRoom");
                HttpContext.Session.Remove("AlertCleanRoom");
            }

            ViewBag.roomCleans = roomDao.GetCleanRooms(HttpContext.Session.GetInt32("HotelId"));
            ViewBag.roomTypes = roomTypeDao.GetRoomTypes(HttpContext.Session.GetInt32("HotelId"));

            return PartialView();
        }

        // lịch sử phòng
        public IActionResult RoomHistory()
        {
            ViewBag.roomHistory = billDao.GetBills(HttpContext.Session.GetInt32("HotelId"));
            ViewBag.roomTypes = roomTypeDao.GetRoomTypes(HttpContext.Session.GetInt32("HotelId"));

            return PartialView();
        }

        // dọn phòng
        [HttpPost]
        public IActionResult CleanRoom(int roomId)
        {
            roomDao.CleanRoom(roomId);
            return RedirectToAction("RoomClean", "AdminRentCheckOut", new { area = "Admin" });
        }

        // yêu cầu dọn phòng
        [HttpPost]
        public IActionResult RequestCleanRoom(int roomId)
        {
            roomDao.RequestCleanRoom(roomId);

            Room room = roomDao.GetRoomById(roomId);

            if (room.Status == 1) 
                return RedirectToAction("RoomRent", "AdminRentCheckOut", new { area = "Admin" });
            else
                return RedirectToAction("RoomWait", "AdminRentCheckOut", new { area = "Admin" });
        }

        // đổi phòng
        [HttpGet]
        public IActionResult ChangeRoom(int bookRoomDetailsId)
        {
            ViewBag.roomChange = bookRoomDetailsDao.GetBookRoomDetailsById(bookRoomDetailsId);
            ViewBag.roomWaits = roomDao.GetEmptyRooms(HttpContext.Session.GetInt32("HotelId"));
            ViewBag.roomTypes = roomTypeDao.GetRoomTypes(HttpContext.Session.GetInt32("HotelId"));
            //Thong bao 
            if (HttpContext.Session.GetString("AlertChangeRoom") != null)
                ViewBag.AlertMessage = HttpContext.Session.GetString("AlertChangeRoom");
            return PartialView();
        }

        // đổi phòng
        [HttpPost]
        public IActionResult ChangeRoom(int roomIdOld, int roomIdNew, bool isCleanRoom = false)
        {
            bookRoomDetailsDao.ChangeRoom(roomIdOld, roomIdNew);

            if (isCleanRoom)
            {
                roomDao.CleanRoom(roomIdNew);
            }

            return RedirectToAction("RoomRent", "AdminRentCheckOut", new { area = "Admin" });
        }

        // thêm menu
        [HttpGet]
        public IActionResult OrderMenu(int bookRoomDetailsId)
		{
            ViewBag.bookRoomDetails = bookRoomDetailsDao.GetBookRoomDetailsById(bookRoomDetailsId);
            ViewBag.services = serviceDao.GetServices();
            var services = serviceDao.GetServices();

            OrderMenuAdminVM orderMenuAdminVM = new OrderMenuAdminVM
            {
                BookRoomDetailsId = bookRoomDetailsId,
                Orders = services.Select(service => new Order
                {
                    ServiceId = service.ServiceId,
                }).ToList(),
            };

            return PartialView(orderMenuAdminVM);
		}

        // thêm menu
        [HttpPost]
        public IActionResult OrderMenu(int bookRoomDetailsId, List<Order> orders)
        {
            //Thong bao 
            if (HttpContext.Session.GetString("AlertOrderMenu") != null)
                ViewBag.AlertMessage = HttpContext.Session.GetString("AlertOrderMenu");
            return PartialView();
        }

        // chỉnh sửa phòng
        [HttpGet]
        public IActionResult EditBookRoomDetails(int bookRoomDetailsId)
        {
            //Thong bao 
            if (HttpContext.Session.GetString("AlertEditBookRoomDetails") != null)
                ViewBag.AlertMessage = HttpContext.Session.GetString("AlertEditBookRoomDetails");

            var bookRoomDetails = bookRoomDetailsDao.GetBookRoomDetailsById(bookRoomDetailsId);

            BookRoomDetailsAdminVM bookRoomDetailsAdminVM = new BookRoomDetailsAdminVM
            {
                CustomerName = bookRoomDetails.BookRoom.Customer.Name,
                Phone = bookRoomDetails.BookRoom.Customer.Phone,
                CIC = bookRoomDetails.BookRoom.Customer.CIC,
                BookRoomDetailsId = bookRoomDetails.BookRoomDetailsId,
                RoomName = bookRoomDetails.Room.Name,
                CheckIn = bookRoomDetails.CheckIn,
                Note = bookRoomDetails.Note,
                Orders = null,
            };

            return PartialView(bookRoomDetailsAdminVM);
        }

        // chỉnh sửa phòng
        [HttpPost]
        public IActionResult EditBookRoomDetails(BookRoomDetailsAdminVM bookRoomDetailsAdminVM)
        {
            string error = "";
            bool result = bookRoomDetailsAdminVM.IsValid(out error);

            if (result)
            {
                // cập nhật lại chi tiết đặt phòng
                BookRoomDetails bookRoomDetails = bookRoomDetailsDao.GetBookRoomDetailsById(bookRoomDetailsAdminVM.BookRoomDetailsId);
                bookRoomDetails.CheckIn = bookRoomDetailsAdminVM.CheckIn;
                bookRoomDetails.Note = bookRoomDetailsAdminVM.Note;
                bookRoomDetailsDao.UpdateBookRoomDetails(bookRoomDetails);

                // cập nhật lại thông tin khách hàng
                Customer customer = bookRoomDetails.BookRoom.Customer;
                customer.CIC = bookRoomDetailsAdminVM.CIC;
                customer.Phone = bookRoomDetailsAdminVM.Phone;
                customer.Name = bookRoomDetailsAdminVM.CustomerName;
                customerDao.UpdateCustomer(customer);

                return Json(new ExecutionOutcome
                {
                    Mess = "Chỉnh sửa thành công.",
                    Result = true,
                });
            } 
            else
            {
                return Json(new ExecutionOutcome
                {
                    Mess = error,
                    Result = false,
                });
            }
        }
    }
}
