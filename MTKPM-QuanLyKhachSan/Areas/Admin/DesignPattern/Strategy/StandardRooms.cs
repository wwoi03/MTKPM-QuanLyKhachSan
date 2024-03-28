using MTKPM_QuanLyKhachSan.DesignPattern.Singleton;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Strategy
{
	public class StandardRooms : StrategyDatabase
	{
		public List<Room> RoomStrategy(int roomId)
		{
			var room = roomId == 0 ?
			SingletonDatabase.Instance.Rooms.Where(item => item.RoomTypeId == 3).OrderByDescending(item => item.RoomId).ToList() :
			SingletonDatabase.Instance.Rooms.Where(item => item.RoomTypeId == 3 && item.RoomTypeId == roomId).OrderByDescending(item => item.RoomId).ToList();
			return room;
		}
	}
}
