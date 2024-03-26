using Microsoft.EntityFrameworkCore;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Singleton;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class EmployeePermissionDao
    {
        DatabaseContext context;

        public EmployeePermissionDao()
        {
            this.context = SingletonDatabase.Instance;
        }

        // Cấp quyền tài khoản
        public void AddEmployeePermission(EmployeePermission employeePermission)
        {
            context.EmployeePermissions.Add(employeePermission);
        }

        // lấy danh sách quyền của nhân viên
        public List<EmployeePermission> GetPermissionByEmployee(int? employeeId)
        {
            List<EmployeePermission> listEmployeePermission = context.EmployeePermissions
                .Where(i => i.EmployeeId == employeeId)
                .Include(i => i.Permission).ToList();
            return listEmployeePermission;
        }
    }
}
