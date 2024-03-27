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
			ViewBag.Room = roomDao.GetRooms();
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
		[HttpPost]
		public IActionResult DeleteRoom (int roomId) 
		{
			roomDao.DeleteRoom(roomId);
			ExecutionOutcome executionOutcome = new ExecutionOutcome()
			{
				Result = true,
				Mess = "Xóa phòng thành công."
			};

			return Json(executionOutcome);
		}
		[HttpGet]
		public IActionResult DetailsRoom(int roomId)
		{
			Room room = roomDao.GetRoomById(roomId);
			RoomVM roomVM = new RoomVM()
			{
				RoomId= room.RoomId,
				Name = room.Name,
				Status = room.Status,
				RoomTypeId = room.RoomTypeId,
				Tidy = room.Tidy,
			};
			return PartialView(roomVM);
			
		}
		[HttpPost]
		public IActionResult DeltailsRoom (RoomVM roomVM)
		{
			roomDao.DetailRoom(roomVM);
			return RedirectToAction("Index");
		}
	}
}
