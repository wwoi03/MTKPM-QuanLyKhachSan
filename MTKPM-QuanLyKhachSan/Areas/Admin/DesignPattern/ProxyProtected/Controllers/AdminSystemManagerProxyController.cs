using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Areas.Admin.Controllers;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.ProxyProtected.Controllers
{
    [Area("Admin")]
    public class AdminSystemManagerProxyController : Controller
    {
        AdminSystemManagerController adminSystemManagerController;

        public AdminSystemManagerProxyController()
        {
            adminSystemManagerController = new AdminSystemManagerController();
        }

        public IActionResult Index()
        {
            return adminSystemManagerController.Index();
        }
    }
}
