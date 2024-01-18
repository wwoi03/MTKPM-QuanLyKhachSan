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
		[Display(Name = "Mật khẩu")]
		public string Password { get; set; }
		public string Name { get; set; }
		public string CIC { get; set; } // căn cước công dân
		public string? Phone { get; set; }
		public string? Email { get; set; }
		public string? Address { get; set; }
	}
}
