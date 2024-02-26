using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;
using MTKPM_QuanLyKhachSan.ViewModels;
using System;

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
			ViewBag.RoomType = roomTypeDao.GetRoomTypes1();
			return View();
		}
        [HttpGet]
        public IActionResult AddRoomType()
        {
            return View();
        }
		[HttpPost]
		public IActionResult AddRoomType(RoomTypeVM roomTypeVM)
		{
			ModelState.Remove("RoomTypeId");
			ModelState.Remove("NumView");
			RoomType roomType = new RoomType()
			{
				RoomTypeId = roomTypeVM.RoomTypeId,
				Name = roomTypeVM.Name,
				Price = roomTypeVM.Price,
				Description = roomTypeVM.Description,
				NumBed = roomTypeVM.NumBed,
				NumAdult = roomTypeVM.NumAdult,
				NumChildren = roomTypeVM.NumChildren,
				NumView = roomTypeVM.NumView,
			};

			roomTypeDao.InsertRoomType(roomType);
			return RedirectToAction("Index");
		}
    }
}
