using MTKPM_QuanLyKhachSan.Models;
using System.Globalization;

namespace MTKPM_QuanLyKhachSan.ViewModels
{
    public class RoomRentVM
    {
        public int BookRoomDetailsId { get; set; }
        public int RoomId { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomName { get; set; }
        public string? Note { get; set; } = "";
        public DateTime CheckIn { get; set; }
        public int Tidy { get; set; }
        public decimal TotalPrice { get; set; }
        public int QuantityMenu { get; set; }

        public string CalcTimeStay()
		{
            // Lấy ngày giờ hiện tại
            DateTime now = DateTime.Now;

            // Tính khoảng thời gian
            TimeSpan duration = now.Subtract(CheckIn);

            // Lấy số ngày và giờ
            int days = duration.Days;
            int hours = duration.Hours;

            // Chuyển kết quả về dạng string
            return $"{days} ngày {hours} giờ";
        }

        // format tiền
        public string FormatMoney()
		{
            // Tạo một đối tượng CultureInfo, bạn có thể chọn "vi-VN" cho định dạng tiền tệ Việt Nam
            CultureInfo vietnamCulture = new CultureInfo("vi-VN");

            // Sử dụng phương thức ToString() với chuỗi định dạng "N0" để định dạng số tiền
            // "N0" chỉ định rằng bạn muốn số nguyên với dấu phân cách hàng nghìn
            // Thêm " VND" vào cuối để chỉ định đơn vị tiền tệ
            return TotalPrice.ToString("N0", vietnamCulture) + " ~";
        }
    }
}
