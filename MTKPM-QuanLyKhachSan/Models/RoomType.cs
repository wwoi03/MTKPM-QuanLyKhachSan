using System.ComponentModel.DataAnnotations;
using HtmlAgilityPack;

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
        public int? NumView { get; set; }

        public string ShortDesc()
        {
            // Sử dụng HtmlAgilityPack để phân tích HTML
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(Description);

            // Lấy nội dung của thẻ p đầu tiên
            var firstParagraphContent = doc.DocumentNode.SelectSingleNode("//p")?.InnerText;

            return firstParagraphContent;
        }
    }
}
