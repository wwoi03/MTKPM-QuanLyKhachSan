using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.ViewModels
{
	public class RoomTypeVM
	{
		public int RoomTypeId { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public string? Description { get; set; }
		public int? NumBed { get; set; }
		public int? NumAdult { get; set; }
		public int? NumChildren { get; set; }
		public int? NumView { get; set; }
		public string? Image { get; set; }

		public IFormFile ImageFile { get; set; }
	}
}
