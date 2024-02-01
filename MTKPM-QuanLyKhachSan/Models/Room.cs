using System.ComponentModel.DataAnnotations;

namespace MTKPM_QuanLyKhachSan.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public int RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }
        public int Tidy { get; set; }
    }
}
