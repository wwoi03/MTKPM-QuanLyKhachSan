using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Singleton;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Facade
{
    public class FacadeDao
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
        private RoomStatus roomStatusDao;
        private RoomTypeDao roomTypeDao;
        private RoomTypeImageDao roomTypeImageDao;
        private ServiceDao serviceDao;

        public FacadeDao()
        {
            billDao = new BillDao();
            bookRoomDao = new BookRoomDao();
            bookRoomDetailsDao = new BookRoomDetailsDao();
            customerDao = new CustomerDao();
            employeeDao = new EmployeeDao();
            employeePermissionDao = new EmployeePermissionDao();
            orderDao = new OrderDao();
            permissionGroupDao = new PermissionGroupDao();
            roleDao = new RoleDao();
            roomDao = new RoomDao();
            roomStatusDao = new RoomStatus();
            roomTypeDao = new RoomTypeDao();
            roomTypeImageDao = new RoomTypeImageDao();
            serviceDao = new ServiceDao();
        }
    }
}
