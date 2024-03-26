using Microsoft.EntityFrameworkCore;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class ServiceDao : IRepository<Service>
    {
        private readonly DatabaseContext _context;

        // Tạo hàm ServiceDao khai báo biến _context
        public ServiceDao(DatabaseContext context)
        {
            _context = context;
        }
        // Tạo hàm ServiceDao() rỗng
        public ServiceDao()
        {
        }
        // Lấy danh sách Menu
        public IEnumerable<Service> GetServices()
        {
            return _context.Services.ToList();
        }
        // Tìm kiếm Menu theo tên Menu
        public IEnumerable<Service> SearchServices(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
                return _context.Services.ToList();
            else
                return _context.Services.Where(i => i.Name.Contains(searchString)).ToList();
        }
        // Lấy Menu theo Id
        public Service GetServiceById(int id)
        {
            return _context.Services.Find(id);
        }
        // Thêm Menu mới
        public void AddService(Service service)
        {
            _context.Services.Add(service);
            _context.SaveChanges();
        }
        // Cập nhật Menu được chỉnh sửa
        public void UpdateService(Service service)
        {
            _context.Entry(service).State = EntityState.Modified;
            _context.SaveChanges();
        }
        // Xóa Menu
        public void DeleteService(Service service)
        {
            _context.Services.Remove(service);
            _context.SaveChanges();
        }

    }
    //Khai báo interface IRepository
    public interface IRepository<Service>
    {
        void AddService(Service entity);
        void DeleteService(Service entity);
        IEnumerable<Service> GetServices();
        Service GetServiceById(int id);
        IEnumerable<Service> SearchServices(string searchString);
        void UpdateService(Service entity);
    }
}

