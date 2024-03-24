using Microsoft.EntityFrameworkCore;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class BillDao
    {
        DatabaseContext context;

        public BillDao(DatabaseContext context)
        {
            this.context = context;
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
