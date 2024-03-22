using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminAccountController : Controller
    {
        RoleDao roleDao;
        PermissionGroupDao permissionGroupDao;

        public AdminAccountController(DatabaseContext context)
		{
            roleDao  = new RoleDao(context);
            permissionGroupDao = new PermissionGroupDao(context);

        }
        public IActionResult Index()
        {
            ViewBag.roles = roleDao.GetRoles(1);
            ViewBag.permissionGroups = permissionGroupDao.GetPermissionGroups(1);

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
