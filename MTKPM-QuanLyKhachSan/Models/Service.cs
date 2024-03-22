using System.ComponentModel.DataAnnotations;

namespace MTKPM_QuanLyKhachSan.Models
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }
}
