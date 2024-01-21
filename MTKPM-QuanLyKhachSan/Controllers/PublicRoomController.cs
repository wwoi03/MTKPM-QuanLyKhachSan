using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;
using MTKPM_QuanLyKhachSan.ViewModels;

namespace MTKPM_QuanLyKhachSan.Controllers
{
    public class PublicRoomController : Controller
    {
        RoomTypeDao roomTypeDao;
        RoomDao roomDao;
        BookRoomDao bookRoomDao;

        public PublicRoomController(DatabaseContext context)
        {
            roomTypeDao = new RoomTypeDao(context);
            roomDao = new RoomDao(context);
            bookRoomDao = new BookRoomDao(context);
        }

        public IActionResult Index(SearchRoomTypeVM searchRoomTypeVM)
        {
            ViewBag.PageTitle = "Phòng";

            if (searchRoomTypeVM.NumAdult > 0 && searchRoomTypeVM.NumChildren > 0)
            {
                ViewBag.rooms = roomTypeDao.SearchRoomType
                (
                    searchRoomTypeVM.CheckIn,
                    searchRoomTypeVM.CheckOut,
                    searchRoomTypeVM.NumAdult,
                    searchRoomTypeVM.NumChildren
                );
            }
            else
            {
                ViewBag.rooms = roomTypeDao.GetRoomTypes();
            }

            return View();
        }

        public IActionResult RoomDetails(int roomTypeId)
        {
            ViewBag.PageTitle = "Chi tiết phòng";
            ViewBag.roomTypeDetails = roomTypeDao.GetRoomTypeById(roomTypeId);
            return View();
        }

        public IActionResult Booking()
        {
            ViewBag.roomTypes = roomTypeDao.GetRoomTypes();

			if (HttpContext.Session.GetInt32("CustomerId") == null)
			{
				return RedirectToAction("Login", "PublicCustomer");
			}
			else
			{
				return View();
			}
        }

        [HttpPost]
        public IActionResult Booking(BookingVM bookingVM)
        {
            ViewBag.roomTypes = roomTypeDao.GetRoomTypes();

            BookRoom bookRoom = new BookRoom()
            {
                CheckIn = bookingVM.ConvertDateTime(bookingVM.CheckIn),
                CheckOut = bookingVM.ConvertDateTime(bookingVM.CheckOut),
                CustomerId = (int)HttpContext.Session.GetInt32("CustomerId"),
                RoomId = bookingVM.RoomId,
                Note = bookingVM.Note,
                IsPayment = false,
            };

            bookRoomDao.Booking(bookRoom);

            return Json("Bạn đã đặt phòng thành công.");
        }

        [HttpPost]
        public IActionResult RoomPartialView(int roomTypeId)
        {
            ViewBag.rooms = roomDao.GetEmptyRoomByType(roomTypeId);
            return PartialView();
        }
    } 
}
