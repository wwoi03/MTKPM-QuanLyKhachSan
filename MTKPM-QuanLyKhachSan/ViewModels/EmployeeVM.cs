using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.ViewModels
{
    public class EmployeeVM
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public int Status { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public int? HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public List<string> Permissions { get; set; } = new List<string>();
        public List<Permission> PermissionsEmployee { get; set; }

        // validation
        public bool Validation(out string error)
        {
            if (string.IsNullOrEmpty(Name))
            {
                error = "Vui lòng nhập tên người dùng.";
                return false;
            } 
            else if (string.IsNullOrEmpty(Username))
            {
                error = "Vui lòng nhập tên đăng nhập.";
                return false;
            }
            else if (string.IsNullOrEmpty(Password))
            {
                error = "Vui lòng nhập mật khẩu.";
                return false;
            }
            else if (string.IsNullOrEmpty(PasswordConfirm))
            {
                error = "Vui lòng xác nhận mật khẩu.";
                return false;
            }
            else if (!Password.Equals(PasswordConfirm))
            {
                error = "Mật khẩu xác nhận không chính xác.";
                return false;
            }
            else
            {
                error = "";
                return true;
            }
        }
    }
}
