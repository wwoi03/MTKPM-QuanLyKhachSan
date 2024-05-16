using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Prototype;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Singleton;
using MTKPM_QuanLyKhachSan.Common;
using MTKPM_QuanLyKhachSan.Common.Config;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;
using MTKPM_QuanLyKhachSan.ViewModels;
using Newtonsoft.Json;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Facede
{
    public class BookingFacede
    {
        public BookRoomDao bookRoomDao;
        public BookRoomDetailsDao bookRoomDetailsDao;
        public RoomDao roomDao;
        public RoomTypeDao roomTypeDao;
        public CustomerDao customerDao;
        public DatabaseContext context;
        public IService myService;

        public BookingFacede(DatabaseContext context, IService myService)
        {
            this.context = context;
            this.myService = myService;

            bookRoomDao = new BookRoomDao(context);
            roomDao = new RoomDao(context);
            customerDao = new CustomerDao(context);
            bookRoomDetailsDao = new BookRoomDetailsDao(context);
            roomTypeDao = new RoomTypeDao(context);
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
                    if (customerId == 0)
                    {
                        Customer newCustomer = new Customer
                        {
                            Phone = bookingAdminVM.Phone,
                            CIC = bookingAdminVM.CIC,
                            Name = bookingAdminVM.Name,
                            HotelId = myService.GetHotelId()
                        };

                        customerDao.CreateCustomer(newCustomer);
                        context.SaveChanges();

                        customerId = newCustomer.CustomerId;
                    }

                    // Tạo phiếu đặt phòng
                    BookRoom newBookRoom = new BookRoom
                    {
                        CustomerId = customerId,
                        EmployeeId = myService.GetEmployeeId(),
                        Note = string.IsNullOrEmpty(bookingAdminVM.Note) ? "" : bookingAdminVM.Note,
                        HotelId = myService.GetHotelId(),
                    };

                    bookRoomDao.Booking(newBookRoom);
                    context.SaveChanges();

                    IPrototype bookRoomDetailsIP = new BookRoomDetails
                    {
                        BookRoomId = newBookRoom.BookRoomId,
                        RoomId = 0,
                        CheckIn = bookingAdminVM.ConvertDateTime(bookingAdminVM.CheckIn),
                        CheckOut = bookingAdminVM.ConvertDateTime(bookingAdminVM.CheckOut),
                        Note = string.IsNullOrEmpty(newBookRoom.Note) ? "" : newBookRoom.Note,
                        HotelId = myService.GetHotelId(),
                        Status = (int)BookRoomDetailsType.NotReceived
                    };

                    // tạo phiếu chi tiết đặt phòng
                    foreach (var roomId in bookingAdminVM.RoomIds)
                    {
                        // kiểm tra phòng trống
                        if (roomDao.IsRoomAvailable(roomId))
                        {
                            BookRoomDetails newBookRoomDetails = (BookRoomDetails)bookRoomDetailsIP.Clone();
                            newBookRoomDetails.RoomId = roomId;

                            bookRoomDetailsDao.AddBookRoomDetails(newBookRoomDetails);
                            roomDao.UpdateRoomPending(roomId);
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

        // lấy dữ liệu và chuyển về RoomTitleVM
        public List<RoomTitleVM> GetRoomTitleVMJsons()
        {
            var rooms = roomDao.GetRooms(myService.GetEmployeeId());

            // Chuyển đổi từ Room sang RoomTitleVM
            List<RoomTitleVM> roomTitleVMs = rooms.Select(room => new RoomTitleVM
            {
                id = (room.RoomId).ToString(),
                title = room.Name,
            }).ToList();

            return roomTitleVMs;
        }

        // lấy dữ liệu và chuyển về BookingEventVM
        public List<BookingEventVM> GetBookingEventVMs()
        {
            var bookings = bookRoomDetailsDao.GetBookRoomDetails();

            List<BookingEventVM> bookingEventVMs = bookings.Select(booking => new BookingEventVM
            {
                id = (booking.BookRoomDetailsId).ToString(),
                resourceId = (booking.RoomId).ToString(),
                start = DateTime.Parse(booking.CheckIn.ToString()).ToString("yyyy-MM-dd"),
                end = DateTime.Parse(booking.CheckOut.ToString()).ToString("yyyy-MM-dd"),
                title = booking.BookRoom.Customer.Name,
                color = (BookRoomDetailsType)booking.Status switch
                {
                    BookRoomDetailsType.Pay => "#FFE0B6",
                    BookRoomDetailsType.NotReceived => "#2BA5F0", 
                    BookRoomDetailsType.Received => "#00A69A", 

                },
            }).ToList();

            return bookingEventVMs;
        }

        public BookingAdminVM BookingDetails(int bookRoomDetailsId)
        {
            BookRoomDetails bookRoomDetails = bookRoomDetailsDao.GetBookRoomDetailsById(bookRoomDetailsId);

            BookingAdminVM bookingDetailsVM = new BookingAdminVM()
            {
                BookRoomId = bookRoomDetails.BookRoomId,
                Name = bookRoomDetails.BookRoom.Customer.Name,
                Phone = bookRoomDetails.BookRoom.Customer.Phone,
                CheckIn = bookRoomDetails.CheckIn.ToString(),
                CheckOut = bookRoomDetails.CheckOut.ToString(),
                Note = bookRoomDetails.BookRoom.Note,
                CIC = bookRoomDetails.BookRoom.Customer.CIC,
                Rooms = roomDao.GetRooms(myService.GetHotelId())
            };

            return bookingDetailsVM;
        }
    }
}
