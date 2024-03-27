using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class ServiceRepositoryProxy : IRepository<Service>
    {
        private readonly IRepository<Service> _serviceRepository;

        public ServiceRepositoryProxy(IRepository<Service> serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        // Phương thức Thêm Menu Mới
        public void AddService(Service entity)
        {
            // Thực hiện các kiểm tra hoặc thao tác bổ sung nếu cần
            _serviceRepository.AddService(entity);
        }
        // Xóa Menu
        public void DeleteService(Service entity)
        {
            // Thực hiện các kiểm tra hoặc thao tác bổ sung nếu cần
            _serviceRepository.DeleteService(entity);
        }
        // Lấy danh sách Menu
        public IEnumerable<Service> GetServices()
        {
            // Thực hiện các kiểm tra hoặc thao tác bổ sung nếu cần
            return _serviceRepository.GetServices();
        }
        // Lấy Menu theo Id
        public Service GetServiceById(int id)
        {
            // Thực hiện các kiểm tra hoặc thao tác bổ sung nếu cần
            return _serviceRepository.GetServiceById(id);
        }
        // Tìm kiếm Menu
        public IEnumerable<Service> SearchServices(string searchString)
        {
            // Thực hiện các kiểm tra hoặc thao tác bổ sung nếu cần
            return _serviceRepository.SearchServices(searchString);
        }
        // Cập nhật Menu được chỉnh sửa
        public void UpdateService(Service entity)
        {
            // Thực hiện các kiểm tra hoặc thao tác bổ sung nếu cần
            _serviceRepository.UpdateService(entity);
        }
    }
}
