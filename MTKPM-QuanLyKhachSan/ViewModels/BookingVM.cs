using System.ComponentModel.DataAnnotations;

namespace MTKPM_QuanLyKhachSan.ViewModels
{
    public class BookingVM
    {
        [Required(ErrorMessage = "Vui lòng nhập tên của bạn.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        public string Phone { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập ngày nhận phòng.")]
        public string CheckIn { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày trả phòng.")]
        public string CheckOut { get; set; }

        public int NumAdult { get; set; }

        public int NumChildren { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập lòng chọn phòng.")]
        public int RoomId { get; set; }

        public string Note { get; set; }

        public DateTime ConvertDateTime(string dateTimeStr)
        {
            string format = "dd.MM.yyyy HH:mm";

            // Sử dụng ParseExact
            try
            {
                DateTime result = DateTime.ParseExact(dateTimeStr, format, System.Globalization.CultureInfo.InvariantCulture);
                return result;
            }
            catch (FormatException)
            {
                Console.WriteLine("Chuỗi không hợp lệ");
            }

            return DateTime.MinValue;
        }

        // kiểm tra ngày trả bé hơn ngày nhận
        public bool CheckDate()
        {
            if (ConvertDateTime(CheckIn) > ConvertDateTime(CheckOut))
                return false;
            return true;
        }
    }
}
