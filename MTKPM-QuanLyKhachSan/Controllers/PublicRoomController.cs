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
        BookRoomDetailsDao bookRoomDetailsDao;

        public PublicRoomController(DatabaseContext context)
        {
            roomTypeDao = new RoomTypeDao(context);
            roomDao = new RoomDao(context);
            bookRoomDao = new BookRoomDao(context);
            bookRoomDetailsDao = new BookRoomDetailsDao(context);
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
                ViewBag.rooms = roomTypeDao.GetRoomTypes(HttpContext.Session.GetInt32("HotelId"));
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
            ViewBag.roomTypes = roomTypeDao.GetRoomTypes(HttpContext.Session.GetInt32("HotelId"));

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
            ViewBag.roomTypes = roomTypeDao.GetRoomTypes(HttpContext.Session.GetInt32("HotelId"));

            ViewModels.ExecutionOutcome executeOperation;

            if (roomDao.RoomStatus(bookingVM.RoomId) != 0)
            {
                executeOperation = new ViewModels.ExecutionOutcome()
                {
                    Result = false,
                    Mess = "Phòng đã có người đặt. Vui lòng chọn phòng khác.",
                };
            } 
            else if (int.Parse(bookingVM.Phone) <= 0 || bookingVM.Phone.Length > 10)
            {
                executeOperation = new ViewModels.ExecutionOutcome()
                {
                    Result = false,
                    Mess = "Vui lòng nhập đúng định dạng số điện thoại.",
                };
            }
            else if (bookingVM.CheckDate() == false)
            {
                executeOperation = new ViewModels.ExecutionOutcome()
                {
                    Result = false,
                    Mess = "Ngày đi phải nhỏ hơn ngày tới.",
                };
            }
            else
            {
                BookRoom bookRoom = new BookRoom()
                {
                    CustomerId = (int)HttpContext.Session.GetInt32("CustomerId"),
                    Note = bookingVM.Note,
                    NumAdult = bookingVM.NumAdult,
                    NumChildren = bookingVM.NumChildren,
                };

                bookRoomDao.Booking(bookRoom);

                BookRoomDetails bookRoomDetails = new BookRoomDetails()
                {
                    BookRoomId = bookRoom.BookRoomId,
                    RoomId = bookingVM.RoomId,
                    CheckIn = bookingVM.ConvertDateTime(bookingVM.CheckIn),
                    CheckOut = bookingVM.ConvertDateTime(bookingVM.CheckOut),
                };

                bookRoomDetailsDao.AddBookRoomDetails(bookRoomDetails);

                executeOperation = new ViewModels.ExecutionOutcome()
                {
                    Result = true,
                    Mess = "Đã đặt phòng thành công.",
                };
            }

            return Json(executeOperation);
        }

        [HttpPost]
        public IActionResult RoomPartialView(int roomTypeId)
        {
            ViewBag.rooms = roomDao.GetEmptyRoomByType(roomTypeId);
            return PartialView();
        }
    } 
}
