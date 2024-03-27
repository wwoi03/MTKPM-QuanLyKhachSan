using MTKPM_QuanLyKhachSan.Models;
using System.ComponentModel.DataAnnotations;

namespace MTKPM_QuanLyKhachSan.ViewModels
{
    public class BookingAdminVM
    {
        public int BookRoomId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên của bạn.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập CCCD.")]
        public string CIC { get; set; } = null!;


        [Required(ErrorMessage = "Vui lòng nhập ngày nhận phòng.")]
        public string CheckIn { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày trả phòng.")]
        public string CheckOut { get; set; }

        public string Note { get; set; } = "";

        public List<Room> Rooms { get; set; } = new List<Room>();
        public List<int> RoomIds { get; set; } = new List<int>();

        public DateTime ConvertDateTime(string dateTimeStr)
        {
            string format = "yyyy-MM-ddTHH:mm";
            DateTime dateTime = DateTime.ParseExact(dateTimeStr, format, null);
            return dateTime;
        }

        // Hàm kiểm tra xem một chuỗi có đúng với định dạng datetime-local không
        public static bool IsDateTimeLocalValid(string dateTimeLocalString)
        {
            // Định dạng của chuỗi đầu vào từ input type="datetime-local"
            string format = "yyyy-MM-ddTHH:mm";

            // Kiểm tra chuỗi có đúng với định dạng datetime-local không
            return DateTime.TryParseExact(dateTimeLocalString, format, null, System.Globalization.DateTimeStyles.None, out _);
        }

        // kiểm tra ngày trả bé hơn ngày nhận
        public bool CheckDate()
        {
            if (ConvertDateTime(CheckIn) > ConvertDateTime(CheckOut))
                return false;
            return true;
        }

        // Kiểm tra ràng buộc
        public bool Validation(out string error)
        {
            if (int.Parse(Phone) <= 0 || Phone.Length > 10)
            {
                error = "Vui lòng nhập đúng định dạng số điện thoại.";
                return false;
            }
            else if (string.IsNullOrEmpty(CheckIn))
            {
                error = "Vui lòng nhập ngày nhận phòng";
                return false;
            }
            else if (string.IsNullOrEmpty(CheckOut))
            {
                error = "Vui lòng nhập ngày trả phòng";
                return false;
            }
            else if (!IsDateTimeLocalValid(CheckIn))
            {
                error = "Ngày nhận phòng không đúng định dạng";
                return false;
            }
            else if (!IsDateTimeLocalValid(CheckOut))
            {
                error = "Ngày nhận phòng không đúng định dạng";
                return false;
            }
            else if (CheckDate() == false)
            {
                error = "Ngày đi phải nhỏ hơn ngày tới.";
                return false;
            }
            else
            {
                error = "";
                return true;
            }
        }

        // chuyển đổi sang model
        public BookRoom ConvertModel()
        {
            BookRoom bookRoom = new BookRoom()
            {
                //CustomerId = this.Bo
            };

            return bookRoom;
        }
    }
}
