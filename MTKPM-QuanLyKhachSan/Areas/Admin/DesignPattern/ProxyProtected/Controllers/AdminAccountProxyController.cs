using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MTKPM_QuanLyKhachSan.Areas.Admin.Controllers;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.ProxyProtected.Services;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Singleton;
using MTKPM_QuanLyKhachSan.Common;
using MTKPM_QuanLyKhachSan.Common.Config;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;
using MTKPM_QuanLyKhachSan.ViewModels;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.ProxyProtected.Controllers
{
    [Area("Admin")]
    public class AdminAccountProxyController : Controller, IAccountEmployee
    {
        private DatabaseContext context;
        private EmployeePermissionDao employeePermissionDao;
        private IAccountEmployee proxy;
        private IService myService;

        public AdminAccountProxyController(IService myService)
        {
            this.context = SingletonDatabase.Instance;
            this.myService = myService;
            this.employeePermissionDao = new EmployeePermissionDao(context);
            this.proxy = new AdminAccountController(context, myService);
        }

        public IActionResult Index()
        {
            int? employeeId = myService.GetEmployeeId();
            var checkPermission = employeePermissionDao.CheckPermission(employeeId, AccountType.ViewAccount.ToString());

            if (checkPermission)
                return proxy.Index();
            else
                return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        [HttpGet]
        public IActionResult CreateAccount()
        {
            int? employeeId = myService.GetEmployeeId();
            var checkPermission = employeePermissionDao.CheckPermission(employeeId, AccountType.CreateAccount.ToString());

            if (checkPermission)
                return proxy.CreateAccount();
            else
                return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        [HttpPost]
        public IActionResult CreateAccount(EmployeeVM employeeVM)
        {
            int? employeeId = myService.GetEmployeeId();
            var checkPermission = employeePermissionDao.CheckPermission(employeeId, AccountType.CreateAccount.ToString());

            if (checkPermission)
                return proxy.CreateAccount(employeeVM);
            else
                return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        [HttpGet]
        public IActionResult EditAccount(int employeeId)
        {
            int? employeeIdCheck = myService.GetEmployeeId();
            var checkPermission = employeePermissionDao.CheckPermission(employeeIdCheck, AccountType.EditAccount.ToString());

            if (checkPermission)
                return proxy.EditAccount(employeeId);
            else
                return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        [HttpPost]
        public IActionResult EditAccount(EmployeeVM employeeVM)
        {
            int? employeeId = myService.GetEmployeeId();
            var checkPermission = employeePermissionDao.CheckPermission(employeeId, AccountType.EditAccount.ToString());

            if (checkPermission)
                return proxy.EditAccount(employeeVM);
            else
                return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        [HttpPost]
        public IActionResult LockAccount(int employeeId)
        {
            int? employeeIdCheck = myService.GetEmployeeId();
            var checkPermission = employeePermissionDao.CheckPermission(employeeIdCheck, AccountType.EditAccount.ToString());

            if (checkPermission)
                return proxy.LockAccount(employeeId);
            else
                return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }

        [HttpPost]
        public IActionResult UnLockAccount(int employeeId)
        {
            int? employeeIdCheck = myService.GetEmployeeId();
            var checkPermission = employeePermissionDao.CheckPermission(employeeIdCheck, AccountType.EditAccount.ToString());

            if (checkPermission)
                return proxy.UnLockAccount(employeeId);
            else
                return RedirectToAction("Index", "Error", new { mess = "Bạn không có quyền truy cập." });
        }
    }
}
