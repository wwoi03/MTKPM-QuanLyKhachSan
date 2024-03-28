using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Strategy
{
	public interface StrategyDatabase
	{
		List<Room> RoomStrategy (int roomId);
	}
}
