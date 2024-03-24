using System.ComponentModel.DataAnnotations;


namespace MTKPM_QuanLyKhachSan.Models
{
    public class PermissionGroup
    {
        [Key]
        public string PermissionId { get; set; }
        public Permission Permission { get; set; }
        [Key]
        public string RoleId { get; set; }
        public Role Role { get; set; }

        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }
}
