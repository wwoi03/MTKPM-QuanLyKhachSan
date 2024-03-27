using Microsoft.EntityFrameworkCore;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class BookRoomDao : IBookRoomDao
    {
        private readonly DatabaseContext _context;
        public BookRoomDao(DatabaseContext context)
        {
            _context = context;
        }

        // Tạo đặt phòng
        public void Booking(BookRoom bookRoom)
		{
            _context.BookRooms.Add(bookRoom);
            _context.SaveChanges();
		}

        // Lấy danh sách đặt phòng theo Id
        public BookRoom GetBookRoomById(int bookingId)
        {
            return _context.BookRooms.Where(i => i.BookRoomId == bookingId).Include(i => i.Customer).FirstOrDefault();
        }

        // Cập nhật lịch sử đặt phòng
        public void UpdateBookRoom(BookRoom bookRoom)
        {
            _context.Update(bookRoom);
            _context.SaveChanges();
        }

        // Xóa lịch sử đặt phòng
        public void DeleteBookRoom(BookRoom bookRoom)
        {
            _context.Remove(bookRoom);
            _context.SaveChanges();
        }
    }
    //Khai báo interface IBookRoomDao
    public interface IBookRoomDao
    {
        void Booking(BookRoom bookRoom);
        BookRoom GetBookRoomById(int bookingId);
        void UpdateBookRoom(BookRoom bookRoom);
        void DeleteBookRoom(BookRoom bookRoom);
    }
}
