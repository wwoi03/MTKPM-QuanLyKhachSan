using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using MTKPM_QuanLyKhachSan.Areas.Admin.Controllers;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.ProxyProtected.Service;
using MTKPM_QuanLyKhachSan.Common;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.ProxyProtected.ProxyControllers
{
    [Area("Admin")]
    public class AdminAccountProxyController : Controller, IAccount
	{
        private IAccount adminAccount;
        private Employee employee;
        EmployeePermissionDao employeePermissionDao;
        List<EmployeePermission> listPermission;


        public AdminAccountProxyController(DatabaseContext context)
		{
            adminAccount = new AdminAccountController(context);
            employeePermissionDao = new EmployeePermissionDao(context);
            employee = new Employee();
            employee.EmployeeId = 1;
            listPermission = new List<EmployeePermission>();
            if (employee != null)
                listPermission = employeePermissionDao.GetPermissionByEmployee(employee.EmployeeId);
        }

        public IActionResult Index()
        {
            foreach (var item in listPermission)
            {
                if (item.PermissionId == "555")
                    return adminAccount.Index();
            }
            HttpContext.Session.SetString("Alert", "Bạn không có quyền xem danh sách tài khoản");
            return RedirectToAction("Index", "AdminBookingProxy");
        }

	}
}
