namespace MTKPM_QuanLyKhachSan.Common.Config
{
    public class MyService : IService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<MyService> _logger;

        public MyService(IHttpContextAccessor httpContextAccessor, ILogger<MyService> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public int? GetHotelId()
        {
            return _httpContextAccessor.HttpContext.Session.GetInt32("HotelId");
        }

        public int? GetEmployeeId()
        {
            return _httpContextAccessor.HttpContext.Session.GetInt32("EmployeeId");
        }

        public string? GetEmployeeName()
        {
            return _httpContextAccessor.HttpContext.Session.GetString("EmployeeName");
        }
    }
}
