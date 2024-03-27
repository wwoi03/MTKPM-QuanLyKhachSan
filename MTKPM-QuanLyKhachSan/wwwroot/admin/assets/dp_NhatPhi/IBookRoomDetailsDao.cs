namespace MTKPM_QuanLyKhachSan.wwwroot.admin.assets.dp_NhatPhi
{
    public interface IBookRoomDetailsDao<BookRoomDetails> where BookRoomDetails : class
    {
        List<BookRoomDetails> GetBookRoomDetails();
        List<BookRoomDetails> GetBookRoomDetailsReceive(int? hotelId);
        void AddBookRoomDetails(BookRoomDetails bookRoomDetails);
        BookRoomDetails GetBookRoomDetailsById(int bookRoomDetailsId);
        void ChangeRoom(int roomIdOld, int roomIdNew);
        void UpdateBookRoomDetails(BookRoomDetails bookRoomDetails);
        List<BookRoomDetails> Search(DateTime startDate, DateTime endDate);
        void DeleteBookRoomDetails(BookRoomDetails bookRoomDetails);
    }
}
