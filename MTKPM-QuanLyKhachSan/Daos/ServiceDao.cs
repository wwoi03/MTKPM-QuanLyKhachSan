using Microsoft.EntityFrameworkCore;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class ServiceDao
    {
        private readonly DatabaseContext _context;

        public ServiceDao(DatabaseContext context)
        {
            _context = context;
        }

        // Lấy danh sách Menu
        public List<Service> GetServices()
        {
            return _context.Services.ToList();
        }

        // Tìm kiếm Menu
        public List<Service> SearchServices(string serviceName)
        {
            if (string.IsNullOrEmpty(serviceName))
                return _context.Services.ToList();
            else
                return _context.Services.Where(i => i.Name.Contains(serviceName)).ToList();
        }
        // Thêm Menu mới
        public async Task AddServiceAsync(Service service)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();
        }

        // Cập nhật Menu
        public async Task UpdateServiceAsync(Service service)
        {
            _context.Update(service);
            await _context.SaveChangesAsync();
        }

        // Xóa Menu
        public async Task DeleteServiceAsync(Service service)
        {
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
        }

        // Lấy Menu theo ID
        public async Task<Service> GetServiceByIdAsync(int id)
        {
            return await _context.Services.FindAsync(id);
        }
    }
}

