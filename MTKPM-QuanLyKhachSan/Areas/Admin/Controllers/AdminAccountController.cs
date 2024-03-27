using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Facede;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.ProxyProtected.Services;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Singleton;
using MTKPM_QuanLyKhachSan.Common.Config;
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
        private AccountFacede accountFacede;
        private readonly IService myService;

        public AdminAccountController(DatabaseContext context, IService myService)
        {
            this.myService = myService;
            accountFacede = new AccountFacede(context, myService);
        }

        public IActionResult Index()
        {
            ViewBag.employees = accountFacede.EmployeeDao.GetEmployees(myService.GetHotelId());
           
            return View();
        }

        // Tạo tài khoản phụ
        [HttpGet]
        public IActionResult CreateAccount()
        {
            ViewBag.roles = accountFacede.RoleDao.GetRoles(myService.GetHotelId());
            ViewBag.permissionGroups = accountFacede.PermissionGroupDao.GetPermissionGroups();
            
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
            ViewBag.roles = accountFacede.RoleDao.GetRoles(myService.GetHotelId());
            ViewBag.permissionGroups = accountFacede.PermissionGroupDao.GetPermissionGroups();

            return PartialView(new EmployeeVM());
        }

        [HttpPost]
        public IActionResult EditAccount(EmployeeVM EditAccount)
        {
            ExecutionOutcome executionOutcome = accountFacede.EditAccount(EditAccount);

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
