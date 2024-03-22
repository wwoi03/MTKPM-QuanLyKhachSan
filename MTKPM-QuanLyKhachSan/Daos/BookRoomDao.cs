using Microsoft.EntityFrameworkCore;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class BookRoomDao
    {
        DatabaseContext context;

        public BookRoomDao(DatabaseContext context)
        {
            this.context = context;
        }

        // tạo đặt phòng
        public void Booking(BookRoom bookRoom)
		{
            context.BookRooms.Add(bookRoom);
            context.SaveChanges();
		}

        // lấy booking theo id
        public BookRoom GetBookRoomById(int bookingId)
        {
            return context.BookRooms.Where(i => i.BookRoomId == bookingId).Include(i => i.Customer).FirstOrDefault();
        }

      

    }
}
