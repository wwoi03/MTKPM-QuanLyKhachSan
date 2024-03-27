namespace MTKPM_QuanLyKhachSan.Common
{
    // Trạng thái phòng
    public enum RoomStatusType : int
    {
        RoomAvailable = 0,   // Phòng trống
        RoomOccupied = 1,    // Phòng đang được thuê
        RoomPending = 2      // Phòng đang chờ nhận
    }

    public enum BookRoomDetailsType : int
    {
        NotReceived = 0,  // chưa nhận
        Received = 1,    // đã nhận
        Cancel = 2,    // bị hủy
        Pay = 3,      // đã thanh toán
    }

    // Trạng thái dọn phòng
    public enum RoomTidyType : int
    {
        Cleaned = 0, // đã dọn phòng
        NotCleaned = 1, // chưa dọn phòng
    }

    // trạng thái tài khoản
    public enum EmployeeStatusType : int
    {
        UnLock = 0, // mở khóa
        Lock = 1, // khóa
    }

    public enum AccountType
    {
        ViewAccount,
        CreateAccount,
        EditAccount,
        DeleteAccount,
    }

    public enum RentCheckOutType
    {
        RentCheckOutAll
    }
}
