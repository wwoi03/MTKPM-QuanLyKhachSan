using MTKPM_QuanLyKhachSan.Models;
using MTKPM_QuanLyKhachSan.ViewModels;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class PermissionDao
    {
        DatabaseContext context;

        public PermissionDao(DatabaseContext context)
        {
            this.context = context;
        }
        //LayDanhSachPermission
        public List <Permission> GetPermissionByEmployee(int employeeId)
        {
            List<EmployeePermission> listEmployeePermission = context.EmployeePermissions.Where(i => i.EmployeeId == employeeId).ToList();

            List<Permission> listPermission = new List<Permission>();
            foreach(var item in listEmployeePermission)
            {
                Permission permission = context.Permissions.Where(i => i.PermissionId == item.PermissionId).FirstOrDefault();
                listPermission.Add(permission);
            }
            return listPermission;
        }
       

    }
}
