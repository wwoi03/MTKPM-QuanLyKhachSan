using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Singleton;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class RoomTypeImageDao
    {
        DatabaseContext context;

        public RoomTypeImageDao()
        {
            this.context = SingletonDatabase.Instance;
        }
    }
}
