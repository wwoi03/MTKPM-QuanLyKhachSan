using System.ComponentModel.DataAnnotations;

namespace MTKPM_QuanLyKhachSan.Models
{
    public class Permission
    {
        [Key]
        public string PermissionId { get; set; }
        public string Name { get; set; }
    }
}
