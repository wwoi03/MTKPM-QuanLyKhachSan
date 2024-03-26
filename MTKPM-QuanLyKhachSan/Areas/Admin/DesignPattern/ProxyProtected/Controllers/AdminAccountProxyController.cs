using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MTKPM_QuanLyKhachSan.Areas.Admin.Controllers;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.ProxyProtected.Services;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Singleton;
using MTKPM_QuanLyKhachSan.Common;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;
using MTKPM_QuanLyKhachSan.ViewModels;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.ProxyProtected.Controllers
{
    [Area("Admin")]
    public class AdminAccountProxyController : Controller, IAccountEmployee
    {
        private DatabaseContext context;
        private EmployeeDao employeeDao;
        private Employee employee;
        private EmployeePermissionDao employeePermissionDao;
        private List<EmployeePermission> employeePermissions;
        private IAccountEmployee proxy;

        public AdminAccountProxyController(IHttpContextAccessor httpContextAccessor)
        {

            employeeDao = new EmployeeDao();
            employeePermissionDao = new EmployeePermissionDao();

            employee = employeeDao.GetEmployeeById(httpContextAccessor.HttpContext?.Session.GetInt32("EmployeeId"));
            employeePermissions = employeePermissionDao.GetPermissionByEmployee(employee.EmployeeId);

            proxy = new AdminAccountController();
        }

        public IActionResult Index()
        {

            foreach (var permission in employeePermissions)
            {
                if (AccountType.ViewAccount.ToString().Equals(permission.Permission.PermissionId))
                {
                    return proxy.Index();
                }
            }

            return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        [HttpGet]
        public IActionResult CreateAccount()
        {
            foreach (var permission in employeePermissions)
            {
                if (AccountType.CreateAccount.ToString().Equals(permission.Permission.PermissionId))
                {
                    return proxy.CreateAccount();
                }
            }

            return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        [HttpPost]
        public IActionResult CreateAccount(EmployeeVM employeeVM)
        {
            foreach (var permission in employeePermissions)
            {
                if (AccountType.CreateAccount.ToString().Equals(permission.Permission.PermissionId))
                {
                    return proxy.CreateAccount(employeeVM);
                }
            }

            return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        [HttpGet]
        public IActionResult EditAccount(int employeeId)
        {

            foreach (var permission in employeePermissions)
            {
                if (AccountType.EditAccount.ToString().Equals(permission.Permission.PermissionId))
                {
                    return proxy.EditAccount(employeeId);
                }
            }

            return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        [HttpPost]
        public IActionResult EditAccount(EmployeeVM employeeVM)
        {
            foreach (var permission in employeePermissions)
            {
                if (AccountType.EditAccount.ToString().Equals(permission.Permission.PermissionId))
                {
                    return proxy.EditAccount(employeeVM);
                }
            }

            return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        [HttpPost]
        public IActionResult LockAccount(int employeeId)
        {
            foreach (var permission in employeePermissions)
            {
                if (AccountType.EditAccount.ToString().Equals(permission.Permission.PermissionId))
                {
                    return proxy.LockAccount(employeeId);
                }
            }

            return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        [HttpPost]
        public IActionResult UnLockAccount(int employeeId)
        {
            foreach (var permission in employeePermissions)
            {
                if (AccountType.EditAccount.ToString().Equals(permission.Permission.PermissionId))
                {
                    return proxy.UnLockAccount(employeeId);
                }
            }

            return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }
    }
}
