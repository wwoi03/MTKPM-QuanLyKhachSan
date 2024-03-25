using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.ViewModels;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.ProxyProtected.Services
{
    public interface IAccount : IProxy
    {
        IActionResult Index();
        IActionResult CreateAccount();
        IActionResult CreateAccount(EmployeeVM employeeVM);
    }
}
