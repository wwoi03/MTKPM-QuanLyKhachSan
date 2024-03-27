using MTKPM_QuanLyKhachSan.Common.Config;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Facede
{
    public class RentCheckOutFacede
    {
        public RoomTypeDao RoomTypeDao { get; set; }
        public RoomDao RoomDao { get; set; }
        public BookRoomDao BookRoomDao { get; set; }
        public BookRoomDetailsDao BookRoomDetailsDao { get; set; }
        public BillDao BillDao { get; set; }
        public OrderDao OrderDao { get; set; }
        public ServiceDao ServiceDao { get; set; }
        public CustomerDao CustomerDao { get; set; }

        private DatabaseContext context;
        private IService myService;

        public RentCheckOutFacede(DatabaseContext context, IService myService)
        {
            this.context = context;
            this.myService = myService;

            RoomTypeDao = new RoomTypeDao(context);
            RoomDao = new RoomDao(context);
            BookRoomDao = new BookRoomDao(context);
            BookRoomDetailsDao = new BookRoomDetailsDao(context);
            BillDao = new BillDao(context);
            OrderDao = new OrderDao(context);
            ServiceDao = new ServiceDao(context);
            CustomerDao = new CustomerDao(context);
        }
    }
}
