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
using Humanizer.Localisation.TimeToClockNotation;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("Admin/[controller]/[action]")]
	public class AdminRoomTypeController : Controller
	{
		RoomTypeDao roomTypeDao;
		private readonly IWebHostEnvironment _hostEnvironment;
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
			IFormFile imageFile = roomTypeVM.ImageFile;
			ModelState.Remove("Image");
			ModelState.Remove("ImageFile");
			ModelState.Remove("RoomTypeId");
			ModelState.Remove("NumView");
			if (ModelState.IsValid)
			{
				if (imageFile != null && imageFile.Length > 0)
				{
					// Đảm bảo đường dẫn thư mục image
					var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Content/HinhAnhSP");

					// Tạo tên tệp ảnh duy nhất bằng cách sử dụng Guid và đuôi tệp ảnh ban đầu
					var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

					// Kết hợp đường dẫn đến thư mục image và tên tệp ảnh để có đường dẫn đầy đủ
					var filePath = Path.Combine(imagePath, fileName);

					// Lưu tệp ảnh vào thư mục image
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						imageFile.CopyTo(stream);
					}

					// Lưu đường dẫn tệp ảnh vào thuộc tính Image của sản phẩm
					roomTypeVM.Image = fileName;
				}
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
			return View();
		}
	}
}
