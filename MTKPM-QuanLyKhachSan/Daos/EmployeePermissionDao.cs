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

        // Cấp quyền tài khoản
        public void AddEmployeePermission(EmployeePermission employeePermission)
        {
            context.EmployeePermissions.Add(employeePermission);
        }
    }
}
