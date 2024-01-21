namespace MTKPM_QuanLyKhachSan.ViewModels
{
    public class BookingVM
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime CheckIn { get; set; }
        public string CheckOut { get; set; }
        public int NumAdult { get; set; }
        public int NumChildren { get; set; }
        public int RoomTypeId { get; set; }
        public string Note { get; set; }
    }
}
