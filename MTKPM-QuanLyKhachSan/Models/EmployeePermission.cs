using System.ComponentModel.DataAnnotations;

namespace MTKPM_QuanLyKhachSan.Models
{
    public class EmployeePermission
    {
        [Key]
        public string PermissionId { get; set; }
        public Permission Permission { get; set; }
        [Key]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
