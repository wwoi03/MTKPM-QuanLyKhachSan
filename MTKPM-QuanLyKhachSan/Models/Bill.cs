using System.ComponentModel.DataAnnotations;

namespace MTKPM_QuanLyKhachSan.Models
{
    public class Bill
    {
        [Key]
        public int BillId { get; set; }
        public int BookRoomId { get; set; }
        public BookRoom BookRoom { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public double ServicePrice { get; set; }
        public double RoomPrice { get; set; }
        public double TotalPrice { get; set; }
        public DateTime DatePayment { get; set; }
    }
}
