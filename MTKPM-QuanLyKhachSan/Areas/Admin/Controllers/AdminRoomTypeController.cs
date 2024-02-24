using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminRoomTypeController : Controller
    {
        RoomTypeDao roomTypeDao;
		public AdminRoomTypeController(DatabaseContext context)
		{
			roomTypeDao = new RoomTypeDao(context);
		}
		public IActionResult Index()
        {
			ViewBag.RoomType = roomTypeDao.GetRoomTypes();
			return View();
		}
        public IActionResult AddRoomType()
        {
            return View();
        }
    }
}
