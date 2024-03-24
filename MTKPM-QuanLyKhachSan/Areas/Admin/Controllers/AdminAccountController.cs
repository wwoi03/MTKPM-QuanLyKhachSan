using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;
using MTKPM_QuanLyKhachSan.ViewModels;
using Newtonsoft.Json;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]

    public class AdminAccountController : Controller
    {
        RoleDao roleDao;
        PermissionGroupDao permissionGroupDao;
        EmployeeDao employeeDao;
        EmployeePermissionDao employeePermissionDao;

        public AdminAccountController(DatabaseContext context)
        {
            roleDao = new RoleDao(context);
            permissionGroupDao = new PermissionGroupDao(context);
            employeeDao = new EmployeeDao(context);
            employeePermissionDao = new EmployeePermissionDao(context);

        }

        public IActionResult Index()
        {
            ViewBag.employees = employeeDao.GetEmployees(1);

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        // Tạo tài khoản phụ
        [HttpGet]
        public IActionResult CreateAccount()
        {
            ViewBag.roles = roleDao.GetRoles(1);
            ViewBag.permissionGroups = permissionGroupDao.GetPermissionGroups();

            EmployeeVM employeeVM = new EmployeeVM();

            return PartialView(employeeVM);
        }

        [HttpPost]
        public IActionResult CreateAccount(EmployeeVM employeeVM)
        {
            string error;
            bool status = employeeVM.Validation(out error);

            if (status)
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
            }

            ExecutionOutcome executionOutcome = new ExecutionOutcome()
            {
                Result = status,
                Mess = string.IsNullOrEmpty(error) ? "Tạo tài khoản thành công." : error
            };

            return Json(executionOutcome);
        }

        // Tạo tài khoản phụ
        [HttpGet]
        public IActionResult EditAccount(int employeeId)
        {
            ViewBag.roles = roleDao.GetRoles(1);
            ViewBag.permissionGroups = permissionGroupDao.GetPermissionGroups();

            EmployeeVM employeeVM = new EmployeeVM();

            return PartialView(employeeVM);
        }

        [HttpPost]
        public IActionResult EditAccount(EmployeeVM employeeVM)
        {
            string error;
            bool status = employeeVM.Validation(out error);

            if (status)
            {
                // duyệt danh sách quyền
                foreach (var item in employeeVM.Permissions)
                {
                    EmployeePermission employeePermission = new EmployeePermission()
                    {
                        //EmployeeId = employee.EmployeeId,
                        PermissionId = item.ToString(),
                    };

                    employeePermissionDao.AddEmployeePermission(employeePermission);
                }
            }

            ExecutionOutcome executionOutcome = new ExecutionOutcome()
            {
                Result = status,
                Mess = string.IsNullOrEmpty(error) ? "Tạo tài khoản thành công." : error
            };

            return Json(executionOutcome);
        }

        [HttpPost]
        public IActionResult LockAccount(int employeeId)
		{
            employeeDao.LockAccount(employeeId);

            ExecutionOutcome executionOutcome = new ExecutionOutcome()
            {
                Result = true,
                Mess = "Khóa tài khoản thành công."
            };

            return Json(executionOutcome);
		}

        [HttpPost]
        public IActionResult UnLockAccount(int employeeId)
        {
            employeeDao.UnLockAccount(employeeId);

            ExecutionOutcome executionOutcome = new ExecutionOutcome()
            {
                Result = true,
                Mess = "Mở khóa tài khoản thành công."
            };

            return Json(executionOutcome);
        }

        /*[HttpPost]
        public IActionResult SetPermission(EmployeeVM employeeVM, string permission)
        {
            if (employeeVM.Permissions.Contains(permission))
                employeeVM.Permissions.Remove(permission);
            else 
                employeeVM.Permissions.Add(permission);

            employeeVM.Permissions.Add("EditRoom");


            TempData["EmployeeVM"] = JsonConvert.SerializeObject(employeeVM);

            return RedirectToAction("CreateAccount", "AdminAccount", new { area = "Admin"});
        }*/

        /*string employeeVMJson = TempData["EmployeeVM"] as string;

            if (!string.IsNullOrEmpty(employeeVMJson))
            {
                employeeVM = JsonConvert.DeserializeObject<EmployeeVM>(employeeVMJson);
            }*/
    }
}
