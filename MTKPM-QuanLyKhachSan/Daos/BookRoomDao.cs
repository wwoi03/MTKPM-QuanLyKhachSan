using Microsoft.EntityFrameworkCore;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Singleton;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class BookRoomDao
    {
        DatabaseContext context;

        public BookRoomDao()
        {
            this.context = SingletonDatabase.Instance;
        }

        // tạo đặt phòng
        public void Booking(BookRoom bookRoom)
		{
            context.BookRooms.Add(bookRoom);
		}

        // lấy booking theo id
        public BookRoom GetBookRoomById(int bookingId)
        {
            return context.BookRooms.Where(i => i.BookRoomId == bookingId).Include(i => i.Customer).FirstOrDefault();
        }

        //
    }
}
