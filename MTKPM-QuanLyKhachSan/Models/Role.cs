using System.ComponentModel.DataAnnotations;

namespace MTKPM_QuanLyKhachSan.Models
{
    public class Role
    {
        [Key]
        public string RoleId { get; set; }
        public string Name { get; set; }

        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }
}
