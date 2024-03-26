using Microsoft.EntityFrameworkCore;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Singleton;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class PermissionGroupDao
    {
        DatabaseContext context;

        public PermissionGroupDao()
        {
            this.context = SingletonDatabase.Instance;
        }

        public List<PermissionGroup> GetPermissionGroups()
        {
            return context.PermissionGroups
                .Include(i => i.Permission)
                .ToList();
        }
    }
}
