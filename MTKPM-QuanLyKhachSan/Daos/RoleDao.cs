using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Singleton;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class RoleDao
    {
        DatabaseContext context;

        public RoleDao()
        {
            this.context = SingletonDatabase.Instance;
        }

        // lấy danh sách quyền hạn theo khách sạn
        public List<Role> GetRoles(int hotelId)
		{
            return context.Roles
                .Where(r => r.HotelId == hotelId || r.HotelId == null || r.HotelId == 0)
                .ToList();
		}
    }
}
