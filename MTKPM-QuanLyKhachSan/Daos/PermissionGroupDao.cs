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

        public List<PermissionGroup> GetPermissionGroups(int hotelId)
        {
            return context.PermissionGroups
                .Include(i => i.Permission)
                .Where(x => x.HotelId == hotelId || x.HotelId == null || x.HotelId == 0)
                .ToList();
        }
    }
}
