using System.ComponentModel.DataAnnotations;

namespace MTKPM_QuanLyKhachSan.Models
{
    public class Hotel
    {
        [Key]
        public int HotelId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
