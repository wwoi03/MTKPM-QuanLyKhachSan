using Microsoft.EntityFrameworkCore;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class RoomTypeDao
    {
        DatabaseContext context = new DatabaseContext();

        // Lấy danh sách loại phòng
        public List<RoomType> GetRoomTypes()
        {
            return context.RoomTypes.ToList();
        }
    }
}
