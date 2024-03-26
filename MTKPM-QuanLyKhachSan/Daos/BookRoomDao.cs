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
        // Tạo đặt phòng
        public void Booking(BookRoom bookRoom)
		{
            context.BookRooms.Add(bookRoom);
            context.SaveChanges();
		}

        // Lấy danh sách đặt phòng theo Id
        public BookRoom GetBookRoomById(int bookingId)
        {
            return context.BookRooms.Where(i => i.BookRoomId == bookingId).Include(i => i.Customer).FirstOrDefault();
        }
        // Cập nhật lịch sử đặt phòng
        public void UpdateBookRoom(BookRoom bookRoom)
        {
            context.Update(bookRoom);
            context.SaveChanges();
        }
    }
}
