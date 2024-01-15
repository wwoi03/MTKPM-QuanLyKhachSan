using System.ComponentModel.DataAnnotations;

namespace MTKPM_QuanLyKhachSan.Models
{
    public class RoomType
    {
        [Key]
        public int RoomTypeId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int? NumBed { get; set; }
        public int? NumAdult { get; set; }
        public int? NumChildren { get; set; }
    }
}
