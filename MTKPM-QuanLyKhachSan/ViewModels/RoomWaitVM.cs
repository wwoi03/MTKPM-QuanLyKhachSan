using MTKPM_QuanLyKhachSan.Common;

namespace MTKPM_QuanLyKhachSan.ViewModels
{
	public class RoomWaitVM
	{
		public int RoomId { get; set; }
		public int RoomTypeId { get; set; }
		public string RoomName { get; set; } = "";
		public int Status { get; set; }
		public int Tidy { get; set; }

		public string ViewStatus()
		{
			switch ((RoomStatusType)Status)
			{
				case RoomStatusType.RoomAvailable:
					return "Chưa sử dụng";
				case RoomStatusType.RoomPending:
					return "Đang chờ nhận";
				default:
					return "Phòng hư";
			}
		}

		public string ViewTidy()
		{
			switch ((RoomTidyType)Tidy)
			{
				case RoomTidyType.Cleaned:
					return "Đã dọn phòng";
				case RoomTidyType.NotCleaned:
					return "Chưa dọn phòng";
				default:
					return "Phòng bẩn như lol";
			}
		}
	}
}
