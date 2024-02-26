using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminRoomController : Controller
    {
        RoomDao roomDao;
		public AdminRoomController(DatabaseContext context)
		{
			roomDao = new RoomDao(context);
		}
		public IActionResult Index()
        {
            ViewBag.Room = roomDao.GetRoom();
            return View();  
        }
    }
}
