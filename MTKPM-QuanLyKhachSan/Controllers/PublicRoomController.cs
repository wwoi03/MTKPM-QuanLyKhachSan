using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;
using MTKPM_QuanLyKhachSan.ViewModels;

namespace MTKPM_QuanLyKhachSan.Controllers
{
    public class PublicRoomController : Controller
    {
        RoomTypeDao roomTypeDao;

        public PublicRoomController(DatabaseContext context)
        {
            roomTypeDao = new RoomTypeDao(context);
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
            return View();
        }
    }
}
