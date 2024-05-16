using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class RoleDao
    {
        DatabaseContext context;

        public RoleDao(DatabaseContext context)
        {
            this.context = context;
        }

        // lấy danh sách quyền hạn theo khách sạn
        public List<Role> GetRoles(int? hotelId)
		{
            return context.Roles
                .Where(r => r.HotelId == hotelId || r.HotelId == null || r.HotelId == 0)
                .ToList();
		}
    }
}
