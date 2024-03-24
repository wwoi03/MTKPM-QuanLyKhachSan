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

        // lấy danh sách dịch vụ
        public List<Service> GetServices()
        {
            return _context.Services.ToList();
        }

        // tìm kiếm dịch vụ
        public List<Service> SearchServices(string serviceName)
        {
            if (string.IsNullOrEmpty(serviceName))
                return _context.Services.ToList();
            else
                return _context.Services.Where(i => i.Name.Contains(serviceName)).ToList();
        }
        // Thêm dịch vụ mới
        public async Task AddServiceAsync(Service service)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();
        }

        // Cập nhật dịch vụ
        public async Task UpdateServiceAsync(Service service)
        {
            _context.Update(service);
            await _context.SaveChangesAsync();
        }

        // Xóa dịch vụ
        public async Task DeleteServiceAsync(Service service)
        {
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
        }

        // Lấy dịch vụ theo ID
        public async Task<Service> GetServiceByIdAsync(int id)
        {
            return await _context.Services.FindAsync(id);
        }
    }
}

