using System.ComponentModel.DataAnnotations;

namespace MTKPM_QuanLyKhachSan.Models
{
    public class Bill
    {
        [Key]
        public int BillId { get; set; }
        public int BookRoomDetailsId { get; set; }
        public BookRoomDetails BookRoomDetails { get; set; }
        public string Payer { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public decimal ServicePrice { get; set; }
        public decimal RoomPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime DatePayment { get; set; }
    }
}
