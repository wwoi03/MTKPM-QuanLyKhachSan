using System.Collections.Generic;
using System.Linq;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class BookRoomDao
    {
        //Khai báo biến _context
        private readonly DatabaseContext _context;

        //Tạo hàm BookRoomDao để nhận tham chiếu biến context
        public BookRoomDao(DatabaseContext context)
        {
            _context = context;
        }
        //Chức năng lấy lịch sử đặt phòng
        public List<BookRoom> GetBookingHistory(int customerId)
        {
            return _context.BookRooms
                           .Where(br => br.CustomerId == customerId)
                           .ToList();
        }
    }
}
