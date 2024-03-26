using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Singleton;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;
using MTKPM_QuanLyKhachSan.ViewModels;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Facede
{
    public class BookingFacede
    {
        private BookRoomDao bookRoomDao;
        private RoomDao roomDao;
        private CustomerDao customerDao;
        DatabaseContext context;

        public BookingFacede()
        {
            context = SingletonDatabase.Instance;

            bookRoomDao = new BookRoomDao();
            roomDao = new RoomDao();
            customerDao = new CustomerDao();
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
    }
}
