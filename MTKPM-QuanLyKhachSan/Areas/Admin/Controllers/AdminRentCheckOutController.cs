using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Facede;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.ProxyProtected.Services;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Singleton;
using MTKPM_QuanLyKhachSan.Common;
using MTKPM_QuanLyKhachSan.Common.Config;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;
using MTKPM_QuanLyKhachSan.ViewModels;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class AdminRentCheckOutController : Controller, IRentCheckOut
    {
        private RentCheckOutFacede rentCheckOutFacede;
        private readonly IService myService;

        public AdminRentCheckOutController(DatabaseContext context, IService myService)
        {
            this.myService = myService;
            rentCheckOutFacede = new RentCheckOutFacede(context, myService);
        }

        public IActionResult Index()
        {
            return View();
        }

        // danh sách phòng chờ
        public IActionResult RoomWait()
        {
            ViewBag.roomTypes = rentCheckOutFacede.RoomTypeDao.GetRoomTypes(myService.GetHotelId());
            ViewBag.roomWaits = rentCheckOutFacede.RoomWait();

            return PartialView();
        }

        // danh sách phòng đang thuê
        public IActionResult RoomRent()
        {
            ViewBag.roomTypes = rentCheckOutFacede.RoomTypeDao.GetRoomTypes(myService.GetHotelId());
            ViewBag.roomRents = rentCheckOutFacede.RoomRent();

            return PartialView();
        }

        // danh sách phòng cần dọn
        public IActionResult RoomClean()
        {
            ViewBag.roomCleans = rentCheckOutFacede.RoomDao.GetCleanRooms(myService.GetHotelId());
            ViewBag.roomTypes = rentCheckOutFacede.RoomTypeDao.GetRoomTypes(myService.GetHotelId());

            return PartialView();
        }

        // lịch sử phòng
        public IActionResult RoomHistory()
        {
            ViewBag.roomHistory = rentCheckOutFacede.BillDao.GetBills(myService.GetHotelId());
            ViewBag.roomTypes = rentCheckOutFacede.RoomTypeDao.GetRoomTypes(myService.GetHotelId());

            return PartialView();
        }

        // dọn phòng
        [HttpPost]
        public IActionResult CleanRoom(int roomId)
        {
            ExecutionOutcome executionOutcome = rentCheckOutFacede.CleanRoom(roomId);

            if (executionOutcome.Result)
                return RedirectToAction("RoomClean", "AdminRentCheckOutProxy", new { area = "Admin" });

            return PartialView(executionOutcome);
        }

        // yêu cầu dọn phòng
        [HttpPost]
        public IActionResult RequestCleanRoom(int roomId)
        {
            ExecutionOutcome executionOutcome = rentCheckOutFacede.RequestCleanRoom(roomId);

            if (executionOutcome.Result)
            {
                Room room = rentCheckOutFacede.RoomDao.GetRoomById(roomId);

                if ((RoomStatusType)room.Status == RoomStatusType.RoomOccupied)
                    return RedirectToAction("RoomRent", "AdminRentCheckOutProxy", new { area = "Admin" });
                else
                    return RedirectToAction("RoomWait", "AdminRentCheckOutProxy", new { area = "Admin" });
            }
                
            return PartialView(executionOutcome); 
        }

        // đổi phòng
        [HttpGet]
        public IActionResult ChangeRoom(int bookRoomDetailsId)
        {
            ViewBag.roomChange = rentCheckOutFacede.BookRoomDetailsDao.GetBookRoomDetailsById(bookRoomDetailsId);
            ViewBag.roomWaits = rentCheckOutFacede.RoomDao.GetEmptyRooms(myService.GetHotelId());
            ViewBag.roomTypes = rentCheckOutFacede.RoomTypeDao.GetRoomTypes(myService.GetHotelId());

            return PartialView();
        }

        // đổi phòng
        [HttpPost]
        public IActionResult ChangeRoom(int bookRoomDetailsIdint, int roomIdOld, int roomIdNew, bool isCleanRoom = false)
        {
            ExecutionOutcome executionOutcome = rentCheckOutFacede.ChangeRoom(bookRoomDetailsIdint, roomIdOld, roomIdNew, isCleanRoom);

            if (executionOutcome.Result)
                return RedirectToAction("RoomRent", "AdminRentCheckOutProxy", new { area = "Admin" });
                
            return PartialView(executionOutcome);
        }

        // thêm menu
        [HttpGet]
        public IActionResult OrderMenu(int bookRoomDetailsId)
		{
            ViewBag.bookRoomDetails = rentCheckOutFacede.BookRoomDetailsDao.GetBookRoomDetailsById(bookRoomDetailsId);
            ViewBag.services = rentCheckOutFacede.ServiceDao.GetServices();

            OrderMenuAdminVM orderMenuAdminVM = rentCheckOutFacede.OrderMenu(bookRoomDetailsId);

            return PartialView(orderMenuAdminVM);
		}

        // thêm menu
        [HttpPost]
        public IActionResult OrderMenu(int bookRoomDetailsId, List<Order> orders)
        {
            ExecutionOutcome executionOutcome = rentCheckOutFacede.OrderMenu(bookRoomDetailsId, orders);

            return PartialView(executionOutcome);
        }

        // chỉnh sửa phòng
        [HttpGet]
        public IActionResult EditBookRoomDetails(int bookRoomDetailsId)
        {
            BookRoomDetailsAdminVM bookRoomDetailsAdminVM = rentCheckOutFacede.GetBookRoomDetails(bookRoomDetailsId);

            return PartialView(bookRoomDetailsAdminVM);
        }

        // chỉnh sửa phòng
        [HttpPost]
        public IActionResult EditBookRoomDetails(BookRoomDetailsAdminVM bookRoomDetailsAdminVM)
        {
            ExecutionOutcome executionOutcome = rentCheckOutFacede.EditBookRoomDetails(bookRoomDetailsAdminVM);

            return Json(executionOutcome);
        }

        [HttpPost]
        public IActionResult CheckIn(int roomId)
        {
            ExecutionOutcome executionOutcome = rentCheckOutFacede.CheckIn(roomId);

            return Json(executionOutcome);
        }

        public IActionResult CheckOut(int bookRoomDetailsId)
        {
            CheckOutVM checkOutVM = rentCheckOutFacede.CheckOut(bookRoomDetailsId);

            return PartialView(checkOutVM);
        }

        [HttpPost]
        public IActionResult CheckOut(CheckOutVM checkOutVM)
        {
            ExecutionOutcome executionOutcome = rentCheckOutFacede.CheckOut(checkOutVM);

            return Json(executionOutcome);
        }

        [HttpPost]
        public IActionResult CancelBooking(int roomId)
        {
            ExecutionOutcome executionOutcome = rentCheckOutFacede.CancelBooking(roomId);

            return Json(executionOutcome);
        }
    }
}
