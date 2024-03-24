using Microsoft.AspNetCore.Mvc;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.ProxyProtected.Service
{
	public interface IAccount
	{
        IActionResult Index();
        //IActionResult Login();
        //IActionResult CreateAccount();
        //IActionResult EditAccount(int employeeId);
        //IActionResult LockAccount(int employeeId);
        //IActionResult UnLockAccount(int employeeId);
    }
}
