using System.ComponentModel.DataAnnotations;

namespace MTKPM_QuanLyKhachSan.Models
{
    public class RoomTypeImage
    {
        [Key]
        public int RoomTypeImageId { get; set; }
        public int RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }
        public string Image { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }
}
