using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Singleton;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Strategy
{
	public class DoubleRooms : StrategyDatabase
	{
		public List<Room> RoomStrategy(int roomId)
		{
			var room = roomId == 0 ?
			SingletonDatabase.Instance.Rooms.Where(item => item.RoomTypeId == 2).OrderByDescending(item => item.RoomId).ToList() :
			SingletonDatabase.Instance.Rooms.Where(item => item.RoomTypeId == 2 && item.RoomTypeId == roomId).OrderByDescending(item => item.RoomId).ToList();
			return room;
		}
	}
}
