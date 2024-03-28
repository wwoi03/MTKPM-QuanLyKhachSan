using MTKPM_QuanLyKhachSan.Models;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MTKPM_QuanLyKhachSan.ViewModels
{
    public class BookRoomDetailsAdminVM
    {
        public int BookRoomDetailsId { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string CIC { get; set; }
        public string RoomName { get; set; }
        public DateTime CheckIn { get; set; }
        public string CheckInTime { get; set; }
        public string Note { get; set; } = "";
        public Decimal Price { get; set; }
        public List<Order> Orders { get; set; }

        // chuyển đổi giờ
        public string ConvertDateTimeToDateTimeLocal()
        {
            // Chuyển đổi DateTime sang định dạng chuỗi yyyy-MM-ddTHH:mm:ss
            // Điều này phù hợp với giá trị của thuộc tính datetime-local trong HTML5
            return CheckIn.ToString("yyyy-MM-dd");
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

        // lấy giờ
        public string ViewCheckInTime()
        {
            return CheckIn.Hour + ":" + CheckIn.Minute;
        }

        // Kiểm tra ràng buộc
        public bool IsValid(out string error)
        {
            if (IsValidPhoneNumber(Phone) == false)
            {
                error = "Số điện thoại cần nhập đúng định dạng.";
                return false;
            }
            else if (long.Parse(Phone) < 0)
            {
                error = "Số điện thoại không được âm.";
                return false;
            }
            else if (long.Parse(CIC) < 0)
            {
                error = "Căn cước công dân không được âm.";
                return false;
            }
            else if (IsValidTime(CheckInTime, "HH:mm") == false)
            {
                error = "Giờ nhận phòng cần đúng định dạng giờ phút (00:00 đến 23:59).";
                return false;
            } 
            else if (!string.IsNullOrEmpty(Note) && Note.Length > 500)
            {
                error = "Ghi chú cần nhập bé hơn 500 ký tự.";
                return false;
            }

            ConcatDateTime();

            error = "";
            return true;
        }

        // Kiểm tra số điện thoại
        public bool IsValidPhoneNumber(string phone)
        {
            string pattern = @"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$";

            // Sử dụng Regex.IsMatch để kiểm tra chuỗi đầu vào
            return Regex.IsMatch(phone, pattern);
        }

        // kiểm tra nhập đúng định dạng ngày
        public bool IsValidDate(string dateString, string dateFormat)
        {
            DateTime parsedDate;
            bool isValid = DateTime.TryParseExact(dateString, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);

            return isValid;
        }

        // kiểm tra nhập đúng định dạng giờ 
        public bool IsValidTime(string timeStr, string timeFormat)
        {
            DateTime time;
            bool isValid = DateTime.TryParseExact(timeStr, timeFormat, null, System.Globalization.DateTimeStyles.None, out time);

            return isValid;
        }

        // nối ngày và giờ
        public void ConcatDateTime()
        {
            if (TimeSpan.TryParseExact(CheckInTime, "hh\\:mm", CultureInfo.InvariantCulture, out TimeSpan timeSpan))
            {
                // Nối giờ và phút vào CheckIn
                CheckIn = new DateTime(CheckIn.Year, CheckIn.Month, CheckIn.Day, timeSpan.Hours, timeSpan.Minutes, 0);
            }
        }
    }
}
