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

        // lấy danh sách phòng đặt
        public List<BookRoom> GetBookRooms()
        {
            return context.BookRooms.Include(i => i.Customer).Include(i => i.Room).ToList();
        } 
    }
}
