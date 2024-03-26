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
        private readonly ServiceDao _serviceDao;
        public AdminServiceController(ServiceDao serviceDao)
        {
            _serviceDao = serviceDao;
        }
        // Hiển thị danh sách Menu
        public IActionResult Index(string searchString)
        {
            var services = string.IsNullOrEmpty(searchString) ? _serviceDao.GetServices() : _serviceDao.SearchServices(searchString);
            return View(services);
        }
        // Hiển thị trang thêm Menu
        public IActionResult Create()
        {
            return View();
        }

        // Xử lý yêu cầu thêm Menu (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Price")] Service service)
        {
            if (ModelState.IsValid)
            {
                await _serviceDao.AddServiceAsync(service);
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        // Hiển thị trang chỉnh sửa Menu
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _serviceDao.GetServiceByIdAsync(id.Value);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }
        // Xử lý yêu cầu chỉnh sửa Menu (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceId, Name, Price, ImageFile")] Service service)
        {
            if (id != service.ServiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _serviceDao.UpdateServiceAsync(service);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        // Xử lý xem chi tiết Menu
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _serviceDao.GetServiceByIdAsync(id.Value);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }
        // Xử lý yêu cầu xóa Menu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var service = await _serviceDao.GetServiceByIdAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            await _serviceDao.DeleteServiceAsync(service);
            return RedirectToAction(nameof(Index));
        }
    }
}

