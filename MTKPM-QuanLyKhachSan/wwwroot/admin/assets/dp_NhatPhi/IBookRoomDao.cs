namespace MTKPM_QuanLyKhachSan.wwwroot.admin.assets.dp_NhatPhi
{
    public interface IBookRoomDao<BookRoom> where BookRoom : class
    {
        void Booking(BookRoom bookRoom);
        BookRoom GetBookRoomById(int bookingId);
        void UpdateBookRoom(BookRoom bookRoom);
        void DeleteBookRoom(BookRoom bookRoom);
    }
}
