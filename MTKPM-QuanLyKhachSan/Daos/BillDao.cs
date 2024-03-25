using Microsoft.EntityFrameworkCore;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Singleton;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class BillDao
    {
        DatabaseContext context;

        public BillDao()
        {
            context = SingletonDatabase.Instance;
        }

        // lấy danh sách hóa đơn
        public List<Bill> GetBills(int? hotelId)
        {
            return context.Bills
                .Where(i => i.HotelId == hotelId)
                .Include(i => i.BookRoomDetails)
                .Include(i => i.BookRoomDetails.Room)
                .OrderBy(i => i.DatePayment)
                .ToList();
        }
    }
}
