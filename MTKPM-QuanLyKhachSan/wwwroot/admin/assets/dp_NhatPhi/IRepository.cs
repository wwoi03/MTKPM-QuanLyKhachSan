namespace MTKPM_QuanLyKhachSan.wwwroot.admin.assets.dp_NhatPhi
{
    public interface IRepository<Service> 
    {
        IEnumerable<Service> GetServices();
        IEnumerable<Service> SearchServices(string searchString);
        Service GetServiceById(int id);
        void AddService(Service entity);
        void UpdateService(Service entity);
        void DeleteService(Service entity);
    }
}
