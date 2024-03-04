using System.ComponentModel.DataAnnotations;

namespace MTKPM_QuanLyKhachSan.Models
{
    public class BookRoom
    {
        [Key]
        public int BookRoomId { get; set; }
        public int CustomerId { get; set; }
		public Customer Customer { get; set; }
		public int? EmployeeId {  get; set; }
		public Employee Employee { get; set; }
		public int? NumAdult { get; set; }
        public int? NumChildren { get; set; }
        public string? Note {  get; set; }
        

	}
}
