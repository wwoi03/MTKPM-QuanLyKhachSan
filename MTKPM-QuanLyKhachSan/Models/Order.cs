using System.ComponentModel.DataAnnotations;

namespace MTKPM_QuanLyKhachSan.Models
{
    public class Order
    {
        [Key]
        public int BookRoomId { get; set; }
        public BookRoom BookRoom { get; set; }
        [Key]
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public int Quantity { get; set; }
    }
}
