using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class RoomDao
    {
		//Khai báo biến context
		DatabaseContext context;

		//Tạo hàm RoomDao để tham chiếu biến context
		public RoomDao(DatabaseContext context)
		{
			this.context = context;
		}

		//Lấy số lượng phòng
		public int GetQuantityOfRoom()
		{
			int count = context.Rooms.Count();
			return count;
		}
	}
}
