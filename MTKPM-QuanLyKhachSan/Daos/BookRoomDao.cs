using System.Collections.Generic;
using System.Linq;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class BookRoomDao
    {
        private readonly DatabaseContext _context;

        public BookRoomDao(DatabaseContext context)
        {
            _context = context;
        }

        public List<BookRoom> GetBookingHistory(int customerId)
        {
            return _context.BookRooms
                           .Where(br => br.CustomerId == customerId)
                           .ToList();
        }
    }
}
