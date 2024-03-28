using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Prototype;
using System.ComponentModel.DataAnnotations;

namespace MTKPM_QuanLyKhachSan.Models
{
    public class BookRoomDetails : IPrototype
    {
        [Key]
        public int BookRoomDetailsId { get; set; }
        public int BookRoomId { get; set; }
        public BookRoom BookRoom { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string? Note { get; set; } = "";
        public int Status { get; set; }
        public int? HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public BookRoomDetails()
        {

        }

        public BookRoomDetails(BookRoomDetails bookRoomDetails)
        {
            BookRoomDetailsId = bookRoomDetails.BookRoomDetailsId;
            BookRoomId = bookRoomDetails.BookRoomId;
            RoomId = bookRoomDetails.RoomId;
            CheckIn = bookRoomDetails.CheckIn;
            CheckOut = bookRoomDetails.CheckOut;
            Note = bookRoomDetails.Note;    
            Status = bookRoomDetails.Status;
            HotelId = bookRoomDetails.HotelId;
        }

        public IPrototype Clone()
        {
            return new BookRoomDetails(this);
        }
    }
}
