using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.ViewModels;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.ProxyProtected.Services
{
    public interface IAccountEmployee
    {
        IActionResult Index();
        IActionResult CreateAccount();
        IActionResult CreateAccount(EmployeeVM employeeVM);
        IActionResult EditAccount(int employeeId);
        IActionResult EditAccount(EmployeeVM employeeVM);
        IActionResult LockAccount(int employeeId);
        IActionResult UnLockAccount(int employeeId);
    }
}
