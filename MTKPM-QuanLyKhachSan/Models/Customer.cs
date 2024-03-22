using System.ComponentModel.DataAnnotations;

namespace MTKPM_QuanLyKhachSan.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string CIC { get; set; } // căn cước công dân
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }
}
