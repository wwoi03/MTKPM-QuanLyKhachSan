using System.ComponentModel.DataAnnotations;

namespace MTKPM_QuanLyKhachSan.Models
{
    public class RoomImage
    {
        [Key]
        public int RoomImageId { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public string Image { get; set; }
    }
}
