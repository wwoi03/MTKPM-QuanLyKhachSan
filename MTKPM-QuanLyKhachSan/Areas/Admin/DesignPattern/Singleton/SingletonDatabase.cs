using Microsoft.EntityFrameworkCore;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Singleton
{
    public sealed class SingletonDatabase 
    {
        // Singleton instance của DatabaseContext
        public static DatabaseContext Instance { get; } = CreateInstance();

        // Phương thức private để tạo instance
        private static DatabaseContext CreateInstance()
        {
            // Cấu hình DbContextOptions
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=HotelManager;Trusted_Connection=True;TrustServerCertificate=True");

            // Trả về instance của DatabaseContext
            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
