using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.ViewModels
{
	public class RoomVM
	{
		public int RoomId { get; set; }
		public string Name { get; set; }
		public int Status { get; set; }
		public int RoomTypeId { get; set; }
		public RoomType RoomType { get; set; }
		public int Tidy { get; set; }
	}
}
