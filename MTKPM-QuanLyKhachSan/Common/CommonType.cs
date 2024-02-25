namespace MTKPM_QuanLyKhachSan.Common
{
    // Trạng thái phòng
    public enum RoomStatusType : int
    {
        RoomAvailable = 0,   // Phòng trống
        RoomOccupied = 1,    // Phòng đang được thuê
        RoomPending = 2      // Phòng đang chờ nhận
    }

    // Trạng thái dọn phòng
    public enum RoomTidyType : int
    {
        Cleaned = 0, // đã dọn phòng
        NotCleaned = 1, // chưa dọn phòng
    }
}
