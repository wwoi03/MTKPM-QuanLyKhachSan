using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Singleton;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;
using MTKPM_QuanLyKhachSan.ViewModels;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Facede
{
    public class AccountFacede
    {
        private EmployeeDao employeeDao;
        private EmployeePermissionDao employeePermissionDao;
        private PermissionGroupDao permissionGroupDao;
        private RoleDao roleDao;
        DatabaseContext context;

        public AccountFacede()
        {
            context = SingletonDatabase.Instance;

            employeeDao = new EmployeeDao(context);
            employeePermissionDao = new EmployeePermissionDao(context);
            permissionGroupDao = new PermissionGroupDao(context);
            roleDao = new RoleDao(context);
        }

        // tạo tài khoản phụ
        public ExecutionOutcome CreateAccount(EmployeeVM employeeVM)
        {
            string error;
            bool status = employeeVM.Validation(out error);

            if (status)
            {
                try
                {
                    // tạo tài khoản
                    Employee employee = new Employee()
                    {
                        Name = employeeVM.Name,
                        HotelId = 1,
                        Username = employeeVM.Username,
                        Password = employeeVM.Password,
                        Status = 0
                    };
                    employeeDao.CreateAccount(employee);

                    // duyệt danh sách quyền
                    foreach (var item in employeeVM.Permissions)
                    {
                        EmployeePermission employeePermission = new EmployeePermission()
                        {
                            EmployeeId = employee.EmployeeId,
                            PermissionId = item.ToString(),
                        };

                        employeePermissionDao.AddEmployeePermission(employeePermission);
                    }

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    status = false;
                    error = "Hệ thống lỗi. Vui lòng thử lại sau!";
                }
            }

            ExecutionOutcome executionOutcome = new ExecutionOutcome()
            {
                Result = status,
                Mess = string.IsNullOrEmpty(error) ? "Tạo tài khoản thành công." : error
            };

            return executionOutcome;
        }

        // khóa tài khoản
        public ExecutionOutcome LockAccount(int employeeId)
        {
            ExecutionOutcome executionOutcome;
            string error = null;
            bool status = true;

            try
            {
                employeeDao.LockAccount(employeeId);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                status = false;
                error = "Hệ thống lỗi. Vui lòng thử lại sau!";
            }

            executionOutcome = new ExecutionOutcome()
            {
                Result = status,
                Mess = string.IsNullOrEmpty(error) ? "Khóa tài khoản thành công." : error
            };

            return executionOutcome;
        }

        // mở khóa tài khoản
        public ExecutionOutcome UnLockAccount(int employeeId)
        {
            ExecutionOutcome executionOutcome;
            string error = null;
            bool status = true;

            try
            {
                employeeDao.UnLockAccount(employeeId);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                status = false;
                error = "Hệ thống lỗi. Vui lòng thử lại sau!";
            }

            executionOutcome = new ExecutionOutcome()
            {
                Result = status,
                Mess = string.IsNullOrEmpty(error) ? "Mở khóa tài khoản thành công." : error
            };

            return executionOutcome;
        }
    }
}
