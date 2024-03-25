using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Singleton;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class ServiceDao
    {
        DatabaseContext context;

        public ServiceDao()
        {
            context = SingletonDatabase.Instance;
        }

        // lấy danh sách dịch vụ
        public List<Service> GetServices()
		{
            return context.Services.ToList();
		}

        // tìm kiếm dịch vụ
        public List<Service> SearchServices(string serviceName)
        {
            if (string.IsNullOrEmpty(serviceName))
                return context.Services.ToList();
            else
                return context.Services.Where(i => i.Name.Contains(serviceName)).ToList();
        }
    }
}
