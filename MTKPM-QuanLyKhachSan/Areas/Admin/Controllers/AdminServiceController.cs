using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminServiceController : Controller
    {
        private readonly IRepository<Service> _serviceRepository;

        public AdminServiceController(IRepository<Service> serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        // Hiển thị danh sách Menu
        public IActionResult Index(string searchString)
        {
            var services = string.IsNullOrEmpty(searchString) ? _serviceRepository.GetServices() : _serviceRepository.SearchServices(searchString);
            return View(services);
        }

        // Hiển thị trang thêm Menu
        public IActionResult Create()
        {
            return View();
        }

        // Xử lý yêu cầu thêm Menu 
        [HttpPost]
        public IActionResult Create(Service service)
        {
            _serviceRepository.AddService(service);
            return RedirectToAction("Index");
        }

        // Hiển thị trang chỉnh sửa Menu
        public IActionResult Edit(int id)
        {
            var service = _serviceRepository.GetServiceById(id);
            return View(service);
        }

        // Xử lý yêu cầu chỉnh sửa Menu 
        [HttpPost]
        public IActionResult Edit(Service service)
        {
            _serviceRepository.UpdateService(service);
            return RedirectToAction("Index");
        }

        // Xử lý xem chi tiết Menu
        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = _serviceRepository.GetServiceById(id.Value);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // Hiển thị trang xóa Menu
        public IActionResult Delete(int id)
        {
            var service = _serviceRepository.GetServiceById(id);
            return View(service);
        }

        // Xử lý yêu cầu xóa Menu 
        [HttpPost]
        public IActionResult Delete(Service service)
        {
            _serviceRepository.DeleteService(service);
            return RedirectToAction("Index");
        }
    }
}

