using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Facede;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.ProxyProtected.Services;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Singleton;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;
using MTKPM_QuanLyKhachSan.ViewModels;
using Newtonsoft.Json;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]

    public class AdminAccountController : Controller, IAccountEmployee
    {
        RoleDao roleDao;
        PermissionGroupDao permissionGroupDao;
        EmployeeDao employeeDao;
        EmployeePermissionDao employeePermissionDao;
        AccountFacede accountFacede;

        public AdminAccountController(DatabaseContext context)
        {
            roleDao = new RoleDao(context);
            permissionGroupDao = new PermissionGroupDao(context);
            employeeDao = new EmployeeDao(context);
            employeePermissionDao = new EmployeePermissionDao(context);

            accountFacede = new AccountFacede(context);
        }

        public IActionResult Index()
        {
            ViewBag.employees = employeeDao.GetEmployees(1);
           
            return View();
        }

        public IActionResult Login()
        {
            HttpContext.Session.SetInt32("EmployeeId", 1);
            HttpContext.Session.SetString("EmployeeName", "Đào Công Tuấn");
            HttpContext.Session.SetInt32("HotelId", 1);

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
            ExecutionOutcome executionOutcome = accountFacede.CreateAccount(employeeVM);

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

        // khóa tài khoản
        [HttpPost]
        public IActionResult LockAccount(int employeeId)
		{
            ExecutionOutcome executionOutcome = accountFacede.LockAccount(employeeId);

            return Json(executionOutcome);
		}

        // mở khóa tài khoản
        [HttpPost]
        public IActionResult UnLockAccount(int employeeId)
        {
            ExecutionOutcome executionOutcome = accountFacede.UnLockAccount(employeeId);
            
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
