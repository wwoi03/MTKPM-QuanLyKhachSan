using Microsoft.EntityFrameworkCore;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class BookRoomDetailsDao
    {
        DatabaseContext context;

        public BookRoomDetailsDao(DatabaseContext context)
        {
            this.context = context;
        }

        // lấy danh sách phòng đặt
        public List<BookRoomDetails> GetBookRoomDetails()
        {
            return context.BookRoomDetails
                .Include(i => i.BookRoom.Customer)
                .Include(i => i.Room)
                .ToList();
        }

        // tạo đặt phòng chi tiết
        public void AddBookRoomDetails(BookRoomDetails bookRoomDetails)
        {
            context.BookRoomDetails.Add(bookRoomDetails);
            context.SaveChanges();
        }

        // lấy bookingDetails theo id
        public BookRoomDetails GetBookRoomDetailsById(int bookRoomDetailsId)
        {
            return context.BookRoomDetails
                .Where(i => i.BookRoomDetailsId == bookRoomDetailsId)
                .Include(i => i.BookRoom.Customer)
                .Include(i => i.Room)
                .FirstOrDefault();
        }
    }
}
