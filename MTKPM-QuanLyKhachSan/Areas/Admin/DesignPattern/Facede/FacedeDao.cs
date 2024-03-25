using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Singleton;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;
using MTKPM_QuanLyKhachSan.ViewModels;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Facede
{
    public class FacedeDao
    {
        private BillDao billDao;
        private BookRoomDao bookRoomDao;
        private BookRoomDetailsDao bookRoomDetailsDao;
        private CustomerDao customerDao;
        private EmployeeDao employeeDao;
        private EmployeePermissionDao employeePermissionDao;
        private OrderDao orderDao;
        private PermissionGroupDao permissionGroupDao;
        private RoleDao roleDao;
        private RoomDao roomDao;
        private RoomTypeDao roomTypeDao;
        private RoomTypeImageDao roomTypeImageDao;
        private ServiceDao serviceDao;
        DatabaseContext context;

        public FacedeDao()
        {
            context = SingletonDatabase.Instance;

            billDao = new BillDao(context);
            bookRoomDao = new BookRoomDao(context);
            bookRoomDetailsDao = new BookRoomDetailsDao(context);
            customerDao = new CustomerDao(context);
            employeeDao = new EmployeeDao(context);
            employeePermissionDao = new EmployeePermissionDao(context);
            orderDao = new OrderDao(context);
            permissionGroupDao = new PermissionGroupDao(context);
            roleDao = new RoleDao(context);
            roomDao = new RoomDao(context);
            roomTypeDao = new RoomTypeDao(context);
            roomTypeImageDao = new RoomTypeImageDao(context);
            serviceDao = new ServiceDao(context);
        }

        public bool Booking(BookingAdminVM bookingAdminVM)
        {

            return false;
        }
    }
}
