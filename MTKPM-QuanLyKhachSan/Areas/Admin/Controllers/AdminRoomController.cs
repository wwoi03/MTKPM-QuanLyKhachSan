using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;
using MTKPM_QuanLyKhachSan.ViewModels;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System;
using MTKPM_QuanLyKhachSan.DesignPattern.Singleton;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Strategy;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("Admin/[controller]/[action]")]
	public class AdminRoomController : Controller
	{
		RoomDao roomDao;
		private StrategyDatabase roomStrategy;
		//public AdminRoomController(DatabaseContext context)
		//{
		//	roomDao = new RoomDao(context);
		//}
		public AdminRoomController(DatabaseContext context)
		{
			roomStrategy = new Under5HundredRoom();
			roomDao = new RoomDao(context);
			
		}
		public IActionResult Index()
		{
			ViewBag.Room = roomDao.GetRooms();
			return View();
		}

		public void SetRoomStrategy(StrategyDatabase strategy)
		{
			roomStrategy = strategy;
		}
		public IActionResult LuxuryRooms(int roomId)
		{
			var rooms = roomStrategy.RoomStrategy(roomId);
			ViewBag.Room = rooms;
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
			SingletonDatabase.Instance.Rooms.Add(room);
			SingletonDatabase.Instance.SaveChanges();
			//roomDao.InsertRoom(room);
			return RedirectToAction("Index");
		}
		[HttpPost]
		public IActionResult DeleteRoom (int roomId) 
		{
			SingletonDatabase.Instance.Rooms.Remove(SingletonDatabase.Instance.Rooms.FirstOrDefault(i => i.RoomId == roomId));
			SingletonDatabase.Instance.SaveChanges();
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
			Room room = SingletonDatabase.Instance.Rooms.FirstOrDefault(i => i.RoomId == roomId);
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
			SingletonDatabase.Instance.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpGet]		
		public IActionResult EditRoom (int roomId)
		{
			Room room = SingletonDatabase.Instance.Rooms.FirstOrDefault(i => i.RoomId == roomId);
			RoomVM roomVM = new RoomVM()
			{
				RoomId = room.RoomId,
				Name = room.Name,
				Status = room.Status,
				RoomTypeId = room.RoomTypeId,
				Tidy = room.Tidy,
			};
			return PartialView(roomVM);
		}
		[HttpPost]
		public IActionResult EditRoom (RoomVM roomVM)
		{
			Room room = new Room()
			{
				RoomId = roomVM.RoomId,
				Name = roomVM.Name,
				Status = roomVM.Status,
				RoomTypeId = roomVM.RoomTypeId,
				Tidy = roomVM.Tidy,

			};
			//roomDao.EditRoom(room);
			ExecutionOutcome executionOutcome = new ExecutionOutcome()
			{
				Result = true,
				Mess = "Cập nhật phòng thành công."
			};
			//SingletonDatabase.Instance.Rooms.Update(room);
			//SingletonDatabase.Instance.SaveChanges();
			roomDao.EditRoom(room);
			return Ok(executionOutcome);
		}
	}
}
