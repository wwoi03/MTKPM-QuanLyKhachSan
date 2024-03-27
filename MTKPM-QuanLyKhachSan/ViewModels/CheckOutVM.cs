using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.ViewModels
{
    public class CheckOutVM
    {
        public int BookRoomDetailsId { get; set; }
        public int CustomerId { get; set; }
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public string Name { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public decimal TotalPriceRoom { get; set; }
        public decimal TotalPriceService { get; set; }
        public string? Note { get; set; }
        public List<Order> Orders { get; set; }

        // chuyển đổi giờ
        public string ViewCheckInDate(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }

        // lấy giờ
        public string ViewCheckInTime(DateTime dateTime)
        {
            return dateTime.Hour + ":" + dateTime.Minute;
        }

        // lấy ngày
        public int DayStay()
        {
            return (CheckOut - CheckIn).Days;
        }

        // lấy số lượng order
        public int ViewQuantityOrder()
        {
            return Orders.Sum(o => o.Quantity);
        }

        // Tính tổng tiền menu
        public decimal CalcPriceMenu()
        {
            return Orders.Sum(o => o.Price * o.Quantity);
        }

        public string FormatCurrency(decimal price)
        {
            // Sử dụng phương thức ToString("C") để định dạng tiền tệ
            string formatCurrency = string.Format("{0:N0} đ", price);
            return formatCurrency;
        }
    }
}
