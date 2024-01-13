using System.ComponentModel.DataAnnotations;

namespace MTKPM_QuanLyKhachSan.Models
{
    public class EmployeeRole
    {
        [Key]
        public int RoleId { get; set; }
        public Role Role { get; set; }
        [Key]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
