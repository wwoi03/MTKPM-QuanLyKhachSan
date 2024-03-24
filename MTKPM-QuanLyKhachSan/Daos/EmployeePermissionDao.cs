using Microsoft.EntityFrameworkCore;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class EmployeePermissionDao
    {
        DatabaseContext context;

        public EmployeePermissionDao(DatabaseContext context)
        {
            this.context = context;
        }
        //LayDanhSachPermission
        public List<EmployeePermission> GetPermissionByEmployee(int employeeId)
        {
            List<EmployeePermission> listEmployeePermission = context.EmployeePermissions.Where(i => i.EmployeeId == employeeId).Include(i => i.Permission).ToList();
            return listEmployeePermission;
        }
    }
}
