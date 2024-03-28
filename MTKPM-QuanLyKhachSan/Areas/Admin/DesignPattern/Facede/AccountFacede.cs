using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Singleton;
using MTKPM_QuanLyKhachSan.Common.Config;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;
using MTKPM_QuanLyKhachSan.ViewModels;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Facede
{
    public class AccountFacede
    {
        public EmployeeDao EmployeeDao { get; set; }
        public EmployeePermissionDao EmployeePermissionDao { get; set; }
        public PermissionGroupDao PermissionGroupDao { get; set; }
        public RoleDao RoleDao { get; set; }
        private DatabaseContext context;
        private IService myService;

        public AccountFacede(DatabaseContext context, IService myService)
        {
            this.context = context;
            this.myService = myService;

            EmployeeDao = new EmployeeDao(context);
            EmployeePermissionDao = new EmployeePermissionDao(context);
            PermissionGroupDao = new PermissionGroupDao(context);
            RoleDao = new RoleDao(context);
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
                        HotelId = (int)myService.GetHotelId(),
                        Username = employeeVM.Username,
                        Password = employeeVM.Password,
                        Status = 0
                    };
                    EmployeeDao.CreateAccount(employee);
                    context.SaveChanges();

                    // duyệt danh sách quyền
                    foreach (var item in employeeVM.Permissions)
                    {
                        EmployeePermission employeePermission = new EmployeePermission()
                        {
                            EmployeeId = employee.EmployeeId,
                            PermissionId = item.ToString(),
                        };

                        EmployeePermissionDao.AddEmployeePermission(employeePermission);
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
                EmployeeDao.LockAccount(employeeId);
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
                EmployeeDao.UnLockAccount(employeeId);
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

        public EmployeeVM GetAccount(int employeeId)
        {
            var employee = EmployeeDao.GetEmployeeById(employeeId);

            EmployeeVM employeeVM = new EmployeeVM()
            {
                EmployeeId = employee.EmployeeId,
                Name = employee.Name,
                Username = employee.Username,
                PermissionsEmployee = EmployeePermissionDao.GetPermissionByEmployee(employee.EmployeeId).Select(i => i.Permission).ToList(),
                HotelId = myService.GetHotelId()
            };

            return employeeVM;
        }

        public ExecutionOutcome EditAccount(EmployeeVM employeeVM)
        {
            string error;
            bool status = employeeVM.Validation(out error);

            if (status)
            {
                try
                {
                    // duyệt danh sách quyền
                    foreach (var item in employeeVM.Permissions)
                    {
                        EmployeePermission employeePermission = new EmployeePermission()
                        {
                            //EmployeeId = employee.EmployeeId,
                            PermissionId = item.ToString(),
                        };

                        EmployeePermissionDao.AddEmployeePermission(employeePermission);
                    }
                }
                catch (Exception ex)
                {
                    error = "Hệ thống lỗi. Vui lòng thử lại sau.";
                    status = false;
                }
            }

            ExecutionOutcome executionOutcome = new ExecutionOutcome()
            {
                Result = status,
                Mess = string.IsNullOrEmpty(error) ? "Tạo tài khoản thành công." : error
            };

            return executionOutcome;
        }
    }
}