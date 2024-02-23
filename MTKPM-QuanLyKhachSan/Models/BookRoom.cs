using System.ComponentModel.DataAnnotations;

namespace MTKPM_QuanLyKhachSan.Models
{
    public class BookRoom
    {
        [Key]
        public int BookRoomId { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId {  get; set; }
        public int NumAdult { get; set; }
        public int NumChildren { get; set; }
        public string Note {  get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string IsPayment { get; set; }
    }
}
