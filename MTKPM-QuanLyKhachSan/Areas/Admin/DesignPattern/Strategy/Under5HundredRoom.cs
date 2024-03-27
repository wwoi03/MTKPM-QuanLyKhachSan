using MTKPM_QuanLyKhachSan.DesignPattern.Singleton;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Strategy
{
	public class Under5HundredRoom : StrategyDatabase
	{
		public List<Room> RoomStrategy(int roomId)
		{
			var room = roomId == 0 ?  
			SingletonDatabase.Instance.Rooms.Where(item => item.RoomTypeId == 1).OrderByDescending(item => item.RoomId).ToList():
			SingletonDatabase.Instance.Rooms.Where(item => item.RoomTypeId == 1 && item.RoomTypeId == roomId).OrderByDescending(item => item.RoomId).ToList();
			return room;
		}
	}
}
