using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;
using MTKPM_QuanLyKhachSan.ViewModels;
using Newtonsoft.Json;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminBookingController : Controller
    {
        RoomDao roomDao;

        public AdminBookingController(DatabaseContext context)
        {
            roomDao = new RoomDao(context);
        }

        public IActionResult Index()
        {
            var rooms = roomDao.GetRooms();

            // Chuyển đổi từ Room sang RoomTitleVM
            List<RoomTitleVM> roomTitleVMs = rooms.Select(room => new RoomTitleVM
            {
                id = room.RoomId,
                title = room.Name,
            }).ToList();

            // Chuyển đổi danh sách RoomTitleVM sang chuỗi JSON
            string json = JsonConvert.SerializeObject(roomTitleVMs, Formatting.Indented);

            return View();
        }
    }
}
