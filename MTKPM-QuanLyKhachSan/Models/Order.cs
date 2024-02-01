using System.ComponentModel.DataAnnotations;

namespace MTKPM_QuanLyKhachSan.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int BookRoomDetailsId { get; set; }
        public BookRoomDetails BookRoomDetails { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
