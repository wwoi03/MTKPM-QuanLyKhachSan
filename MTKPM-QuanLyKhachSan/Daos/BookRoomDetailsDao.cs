using Microsoft.EntityFrameworkCore;
using MTKPM_QuanLyKhachSan.Common;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class BookRoomDetailsDao : IBookRoomDetailsDao
    {
        private readonly DatabaseContext _context;

        public BookRoomDetailsDao(DatabaseContext context)
        {
            _context = context;
        }

        // Lấy danh sách phòng đặt chưa nhận
        public List<BookRoomDetails> GetBookRoomDetails()
        {
            return _context.BookRoomDetails
                .Include(i => i.BookRoom.Customer)
                .Include(i => i.Room)
                .ToList();
        }

        // Lấy danh sách phòng đã nhận
        public List<BookRoomDetails> GetBookRoomDetailsReceive(int? hotelId)
        {
            return _context.BookRoomDetails
                .Include(i => i.BookRoom)
                .Include(i => i.BookRoom.Customer)
                .Include(i  => i.Room)
                .Include(i  => i.Room.RoomType)
                .Join(_context.Rooms, 
                    brd => brd.RoomId, // Khóa ngoại từ BookRoomDetails
                    room => room.RoomId, // Khóa chính từ Room
                    (brd, room) => new { brd, room }) // Kết quả kết hợp)
                .Where(i => i.room.Status == (int)RoomStatusType.RoomOccupied && i.brd.HotelId == hotelId)
                .Select(i => i.brd)
                .ToList();
        }

        // Tạo đặt phòng chi tiết
        public void AddBookRoomDetails(BookRoomDetails bookRoomDetails)
        {
            _context.BookRoomDetails.Add(bookRoomDetails);
            _context.SaveChanges();
        }

        // Lấy bookingDetails theo id
        public BookRoomDetails GetBookRoomDetailsById(int bookRoomDetailsId)
        {
            return _context.BookRoomDetails
                .Where(i => i.BookRoomDetailsId == bookRoomDetailsId)
                .Include(i => i.BookRoom.Customer)
                .Include(i => i.Room)
                .Include(i => i.BookRoom)
                .FirstOrDefault();
        }

        // Đổi phòng
        public void ChangeRoom(int roomIdOld, int roomIdNew)
        {
            BookRoomDetails bookRoomDetails = GetBookRoomDetailsById(roomIdOld);
            bookRoomDetails.RoomId = roomIdNew;
            _context.Update(bookRoomDetails);
            _context.SaveChanges();
        }

        // Cập nhật chi tiết đặt phòng
        public void UpdateBookRoomDetails(BookRoomDetails bookRoomDetails)
        {
            _context.Update(bookRoomDetails);
            _context.SaveChanges();
        }

        // Tìm kiếm lịch sử đặt phòng theo ngày/tháng/năm
        public List<BookRoomDetails> Search(DateTime startDate, DateTime endDate)
        {
            return _context.BookRoomDetails
                .Include(i => i.BookRoom.Customer)
                .Include(i => i.Room)
                .Where(i => i.CheckIn >= startDate && i.CheckOut <= endDate)
                .ToList();
        }

        // Xóa chi tiết lịch sử đặt phòng
        public void DeleteBookRoomDetails(BookRoomDetails bookRoomDetails)
        {
            _context.Remove(bookRoomDetails);
            _context.SaveChanges();
        }
    }
    // Khai báo interface IBookRoomDetailsDao
    public interface IBookRoomDetailsDao
    {
        List<BookRoomDetails> GetBookRoomDetails();
        List<BookRoomDetails> GetBookRoomDetailsReceive(int? hotelId);
        void AddBookRoomDetails(BookRoomDetails bookRoomDetails);
        BookRoomDetails GetBookRoomDetailsById(int bookRoomDetailsId);
        void ChangeRoom(int roomIdOld, int roomIdNew);
        void UpdateBookRoomDetails(BookRoomDetails bookRoomDetails);
        List<BookRoomDetails> Search(DateTime startDate, DateTime endDate);
        void DeleteBookRoomDetails(BookRoomDetails bookRoomDetails);
    }
}
