using System.ComponentModel.DataAnnotations;

namespace MTKPM_QuanLyKhachSan.Models
{
    public class BookRoomDetails
    {
        [Key]
        public int BookRoomDetailsId { get; set; }
        public int BookRoomId { get; set; }
        public BookRoom BookRoom { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string? Note { get; set; }
        public int Status { get; set; }
    }
}
