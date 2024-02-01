using MTKPM_QuanLyKhachSan.Models;
using System.ComponentModel.DataAnnotations;

namespace MTKPM_QuanLyKhachSan.ViewModels
{
    public class BookingDetailsVM
    {
        public int BookRoomId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên của bạn.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập CCCD.")]
        public string CIC { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập ngày nhận phòng.")]
        public string CheckIn { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày trả phòng.")]
        public string CheckOut { get; set; }

        public string Note { get; set; }

        public List<Room> Rooms { get; set; } 
    }
}
