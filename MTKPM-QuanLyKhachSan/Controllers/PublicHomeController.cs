using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;


namespace MTKPM_QuanLyKhachSan.Controllers
{
    public class PublicHomeController : Controller
    {
		RoomTypeDao roomTypeDao;

		public PublicHomeController(DatabaseContext context)
		{
			roomTypeDao = new RoomTypeDao(context);
		}
		
		public IActionResult Index(int roomTypeId)
        {
			ViewBag.PageTitle = "Trang chủ";
            ViewBag.hotRooms = roomTypeDao.GetRoomTypes();
			ViewBag.roomTypeDetails = roomTypeDao.GetRoomTypeById(roomTypeId);
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
