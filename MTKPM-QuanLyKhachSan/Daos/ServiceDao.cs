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
        public void AddService(Service service)
        {
            _context.Services.Add(service);
            _context.SaveChanges();
        }

        // Cập nhật Menu
        public void UpdateService(Service service)
        {
            _context.Update(service);
            _context.SaveChanges();
        }

        // Xóa Menu
        public void DeleteService(Service service)
        {
            // Kiểm tra xem ServiceId đã được thiết lập chưa
            if (service.ServiceId != 0)
            {
                _context.Services.Remove(service);
                _context.SaveChanges();
            }
            else
            {
                // Xử lý khi ServiceId chưa được thiết lập
                throw new ArgumentException("ServiceId has not been set.");
            }
        }

        // Lấy Menu theo ID
        public Service GetServiceById(int id)
        {
            return _context.Services.Find(id);
        }
    }
}

