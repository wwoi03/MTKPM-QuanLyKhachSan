using System.ComponentModel.DataAnnotations;

namespace MTKPM_QuanLyKhachSan.ViewModels
{
	public class CustomerVM
	{
		public int CustomerId { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập Username của bạn.")]
		[StringLength(30, ErrorMessage = "Chiều dài vượt quá 30 ký tự")]
		[Display(Name = "Username")]
		public string Username { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [StringLength(20, ErrorMessage = "Chiều dài vượt quá 20 ký tự.")]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Tên của bạn.")]
        [StringLength(20, ErrorMessage = "Chiều dài vượt quá 20 ký tự")]
        [Display(Name = "Họ và tên")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập CCCD của bạn.")]
        [StringLength(13, ErrorMessage = "Căn cước công dân vượt quá 13 ký tự")]
        [Display(Name = "Căn Cước Công Dân")]
        public string CIC { get; set; } // căn cước công dân
        [Required(ErrorMessage = "Vui lòng nhập SDT của bạn.")]
        [StringLength(12, ErrorMessage = "Số Điện thoại vượt quá 12 ký tự")]
        [Display(Name = "Số Điện Thoại")]
        public string? Phone { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Email của bạn.")]
        [StringLength(30, ErrorMessage = "Số Điện thoại vượt quá 30 ký tự")]
        [Display(Name = "Email")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Email của bạn.")]
        public string? Address { get; set; }
        public string VerifyPassword { get; set; }
    }
}
