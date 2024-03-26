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
            roomTypeDao = new RoomTypeDao();
            roomTypeImageDao = new RoomTypeImageDao();
            serviceDao = new ServiceDao();
        }

        // đặt phòng
        public ExecutionOutcome Booking(BookingAdminVM bookingAdminVM)
        {
            ExecutionOutcome executionOutcome;
            string error;
            bool status = bookingAdminVM.Validation(out error);

            if (status)
            {
                try
                {
                    // tạo khách hàng nếu chưa tồn tại
                    int customerId = customerDao.GetCustomerIdByPhoneOrCIC(bookingAdminVM.Phone, bookingAdminVM.CIC);
                    if (customerId > 0)
                    {
                        Customer newCustomer = new Customer
                        {
                            Phone = bookingAdminVM.Phone,
                            CIC = bookingAdminVM.CIC,
                            Name = bookingAdminVM.Name
                        };

                        customerDao.CreateCustomer(newCustomer);

                        customerId = newCustomer.CustomerId;
                    }

                    // Tạo phiếu đặt phòng
                    BookRoom newBookRoom = new BookRoom
                    {
                        CustomerId = customerId,
                        EmployeeId = null,
                        Note = bookingAdminVM.Note,
                        HotelId = 1
                    };

                    bookRoomDao.Booking(newBookRoom);

                    // tạo phiếu chi tiết đặt phòng
                    foreach (var room in bookingAdminVM.Rooms)
                    {
                        // kiểm tra phòng trống
                        if (roomDao.IsRoomAvailable(room.RoomId))
                        {
                            // tạo phòng
                            BookRoomDetails newBookRoomDetails = new BookRoomDetails
                            {
                                BookRoomId = newBookRoom.BookRoomId,
                                RoomId = room.RoomId,
                                CheckIn = bookingAdminVM.ConvertDateTime(bookingAdminVM.CheckIn),
                                CheckOut = bookingAdminVM.ConvertDateTime(bookingAdminVM.CheckOut),
                                Note = newBookRoom.Note
                            };
                        }
                    }

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    status = false;
                    error = "Hệ thống lỗi. Vui lòng thử lại sau!";
                }
            }

            executionOutcome = new ExecutionOutcome()
            {
                Result = status,
                Mess = string.IsNullOrEmpty(error) ? "Đặt phòng thành công." : error
            };
            

            return executionOutcome;
        }

        // tạo tài khoản phụ
        public ExecutionOutcome CreateAccount(EmployeeVM employeeVM)
        {
            string error;
            bool status = employeeVM.Validation(out error);

            if (status)
            {
                try
                {
                    // tạo tài khoản
                    Employee employee = new Employee()
                    {
                        Name = employeeVM.Name,
                        HotelId = 1,
                        Username = employeeVM.Username,
                        Password = employeeVM.Password,
                        Status = 0
                    };
                    employeeDao.CreateAccount(employee);

                    // duyệt danh sách quyền
                    foreach (var item in employeeVM.Permissions)
                    {
                        EmployeePermission employeePermission = new EmployeePermission()
                        {
                            EmployeeId = employee.EmployeeId,
                            PermissionId = item.ToString(),
                        };

                        employeePermissionDao.AddEmployeePermission(employeePermission);
                    }

                    context.SaveChanges();
                }
                catch(Exception ex)
                {
                    status = false;
                    error = "Hệ thống lỗi. Vui lòng thử lại sau!";
                }
            }

            ExecutionOutcome executionOutcome = new ExecutionOutcome()
            {
                Result = status,
                Mess = string.IsNullOrEmpty(error) ? "Tạo tài khoản thành công." : error
            };

            return executionOutcome;
        }

        // khóa tài khoản
        public ExecutionOutcome LockAccount(int employeeId)
        {
            ExecutionOutcome executionOutcome;
            string error = null;
            bool status = true;

            try
            {
                employeeDao.LockAccount(employeeId);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                status = false;
                error = "Hệ thống lỗi. Vui lòng thử lại sau!";
            }

            executionOutcome = new ExecutionOutcome()
            {
                Result = status,
                Mess = string.IsNullOrEmpty(error) ? "Khóa tài khoản thành công." : error
            };

            return executionOutcome;
        }

        // mở khóa tài khoản
        public ExecutionOutcome UnLockAccount(int employeeId)
        {
            ExecutionOutcome executionOutcome;
            string error = null;
            bool status = true;

            try
            {
                employeeDao.UnLockAccount(employeeId);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                status = false;
                error = "Hệ thống lỗi. Vui lòng thử lại sau!";
            }

            executionOutcome = new ExecutionOutcome()
            {
                Result = status,
                Mess = string.IsNullOrEmpty(error) ? "Mở khóa tài khoản thành công." : error
            };

            return executionOutcome;
        }
    }
}
