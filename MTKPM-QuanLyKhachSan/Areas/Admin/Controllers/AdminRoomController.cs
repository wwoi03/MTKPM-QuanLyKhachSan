using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;
using MTKPM_QuanLyKhachSan.ViewModels;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("Admin/[controller]/[action]")]
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
		[HttpGet]
		public IActionResult AddRoom()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddRoom(RoomVM roomVM)
        {
			ModelState.Remove("RoomId");
            Room room = new Room()
            {
                RoomId = roomVM.RoomId,
                Name = roomVM.Name,
                Status = roomVM.Status,
                RoomTypeId = roomVM.RoomTypeId,
                Tidy = roomVM.Tidy,
            };
            roomDao.InsertRoom(room);
            return RedirectToAction("Index");
		}
		[HttpGet]
		public IActionResult Delete(int roomId)
		{
			Room room = roomDao.GetRoomById(roomId);
			return Json(room);
		}

		[HttpPost]
		public IActionResult DeleteById(int roomId)
		{
			roomDao.DeleteMenu(roomId);
			return RedirectToAction("Index");
		}
	}
}
