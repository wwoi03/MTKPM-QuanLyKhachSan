using Microsoft.EntityFrameworkCore;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class PermissionGroupDao
    {
        DatabaseContext context;

        public PermissionGroupDao(DatabaseContext context)
        {
            this.context = context;
        }

        public List<PermissionGroup> GetPermissionGroups(int? hotelId)
        {
            return context.PermissionGroups
                .Include(i => i.Permission)
                .Include(i => i.Role)
                .Where(i => i.HotelId == hotelId || i.HotelId == null)
                .ToList();
        }
    }
}
