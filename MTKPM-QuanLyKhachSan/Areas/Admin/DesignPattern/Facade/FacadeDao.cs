using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Singleton;
using MTKPM_QuanLyKhachSan.Common;
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
        DatabaseContext context = SingletonDatabase.Instance;

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
     

        // đổi phòng************************************************
        public void ChangeRoom(int roomIdOld, int roomIdNew)
        {

            BookRoomDetails bookRoomDetails = bookRoomDetailsDao.GetBookRoomDetailsById(roomIdOld);
            bookRoomDetails.RoomId = roomIdNew;
            bookRoomDetailsDao.UpdateBookRoomDetails(bookRoomDetails);
        }

       
        // Khóa tài khoản
        public void LockAccount(int employeeId)
        {
            Employee employee = employeeDao.GetEmployeeById(employeeId);
            employee.Status = (int)EmployeeStatusType.Lock;
            employeeDao.UpdateEmployee(employee);
        }

        // Mở Khóa tài khoản
        public void UnLockAccount(int employeeId)
        {
            Employee employee = employeeDao.GetEmployeeById(employeeId);
            employee.Status = (int)EmployeeStatusType.UnLock;
            employeeDao.UpdateEmployee(employee);
        }
        // dọn phòng
        public void CleanRoom(int roomId)
        {
            Room room = roomDao.GetRoomById(roomId);
            room.Tidy = 0;
            roomDao.UpdateRoom(room);
        }

        // dọn phòng
        public void RequestCleanRoom(int roomId)
        {
            Room room = roomDao.GetRoomById(roomId);
            room.Tidy = 1;
            roomDao.UpdateRoom(room);
        }

    }
}
































////Bill
//public List<Bill> GetBills(int? hotelId)
//{
//    return billDao.GetBills(hotelId);
//}
////Booking
//public void Booking(BookRoom bookRoom)
//{
//    bookRoomDao.Booking(bookRoom);
//}
//public BookRoom GetBookRoomById(int bookingId)
//{
//    return bookRoomDao.GetBookRoomById(bookingId);
//}
////BookRommDetails
//// lấy danh sách phòng đặt chưa nhận
//public List<BookRoomDetails> GetBookRoomDetails()
//{
//    return bookRoomDetailsDao.GetBookRoomDetails();
//}

//// lấy danh sách phòng đã nhận
//public List<BookRoomDetails> GetBookRoomDetailsReceive(int? hotelId)
//{
//    return bookRoomDetailsDao.GetBookRoomDetailsReceive(hotelId);

//}

//// tạo đặt phòng chi tiết
//public void AddBookRoomDetails(BookRoomDetails bookRoomDetails)
//{
//    bookRoomDetailsDao.AddBookRoomDetails(bookRoomDetails);
//}

//// lấy bookingDetails theo id
//public BookRoomDetails GetBookRoomDetailsById(int bookRoomDetailsId)
//{
//    return bookRoomDetailsDao.GetBookRoomDetailsById(bookRoomDetailsId);
//}
//// cập nhật chi tiết đặt phòng
//public void UpdateBookRoomDetails(BookRoomDetails bookRoomDetails)
//{
//    bookRoomDetailsDao.UpdateBookRoomDetails(bookRoomDetails);
//}
