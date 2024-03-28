using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.ViewModels
{
    public class OrderMenuAdminVM
    {
        public int BookRoomDetailsId { get; set; }
        public List<Order> Orders { get; set; }
    }
}
