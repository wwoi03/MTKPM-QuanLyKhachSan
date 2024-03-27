using System.ComponentModel.DataAnnotations;

namespace MTKPM_QuanLyKhachSan.Models
{
    public class BookRoom
    {
        [Key]
        public int BookRoomId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int? NumAdult { get; set; } = 0;
        public int? NumChildren { get; set; } = 0;
        public string? Note { get; set; } = "";
        public int? HotelId { get; set; }
        public Hotel Hotel { get; set; }

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
    }
}
