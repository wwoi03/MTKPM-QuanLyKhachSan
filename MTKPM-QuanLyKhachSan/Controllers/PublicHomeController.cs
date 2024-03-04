using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;


namespace MTKPM_QuanLyKhachSan.Controllers
{
    public class PublicHomeController : Controller
    {
        //Khai báo biến roomtypeDao
		RoomTypeDao roomTypeDao;
        //Tạo controller 
		public PublicHomeController(DatabaseContext context)
		{
			roomTypeDao = new RoomTypeDao(context);
		}
		//Hiển thị 3 loại phòng lên trang chủ
		public IActionResult Index(int roomTypeId)
        {
            //Tiêu đề trang
			ViewBag.PageTitle = "Trang chủ";
            //Hiển thị danh sách loại phòng lên trang chủ
            ViewBag.hotRooms = roomTypeDao.GetRoomTypes();
            //Hiển thị chi tiết loại phòng dựa vào Id loại phòng
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
