using Microsoft.EntityFrameworkCore;
using MTKPM_QuanLyKhachSan.Models;
using System.Linq;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class RoomDao
    {
        DatabaseContext context;

        public RoomDao(DatabaseContext context)
        {
            this.context = context;
        }

        // lấy danh sách phòng trống theo loại phòng
        public List<Room> GetEmptyRoomByType(int roomTypeId)
        {
            return context.Rooms.Where(i => i.RoomTypeId == roomTypeId && i.Status == 0).Include(i => i.RoomType).ToList();
        }

        // kiểm tra trạng thái phòng
        public int RoomStatus(int roomId)
        {
            return context.Rooms.FirstOrDefault(i => i.RoomId == roomId).Status;
        }

        // lấy danh sách phòng
        public List<Room> GetRooms()
        {
            return context.Rooms.OrderByDescending(i => Convert.ToInt32(i.Name)).ToList();
        }

        // lấy danh sách phòng trống
        public List<Room> GetEmptyRooms()
        {
            return context.Rooms.Where(i => i.Status == 0).Include(i => i.RoomType).ToList();
        }
    }
}
