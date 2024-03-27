using MTKPM_QuanLyKhachSan.Common;
using MTKPM_QuanLyKhachSan.Common.Config;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;
using MTKPM_QuanLyKhachSan.ViewModels;

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

        // danh sách phòng chờ
        public List<RoomWaitVM> RoomWait()
        {
            var roomWaits = RoomDao.GetRoomWaits(myService.GetHotelId());

            List<RoomWaitVM> roomWaitVMs = roomWaits.Select(roomWait => new RoomWaitVM
            {
                RoomId = roomWait.RoomId,
                RoomTypeId = roomWait.RoomTypeId,
                RoomName = roomWait.Name,
                Status = roomWait.Status,
                Tidy = roomWait.Tidy,
            }).ToList();

            return roomWaitVMs;
        }

        // danh sách phòng đang thuê
        public List<RoomRentVM> RoomRent()
        {
            var roomRents = BookRoomDetailsDao.GetBookRoomDetailsReceive(myService.GetHotelId());

            List<RoomRentVM> roomRentVMs = roomRents.Select(roomRent => new RoomRentVM
            {
                CustomerName = roomRent.BookRoom.Customer.Name,
                BookRoomDetailsId = roomRent.BookRoomDetailsId,
                RoomId = roomRent.RoomId,
                RoomTypeId = roomRent.Room.RoomTypeId,
                RoomName = roomRent.Room.Name,
                Tidy = roomRent.Room.Tidy,
                Note = roomRent.Note,
                CheckIn = roomRent.CheckIn,
                TotalPrice = BillDao.CalcTotalPriceRoom(roomRent.BookRoomDetailsId),
                QuantityMenu = OrderDao.CalcOrderQuantity(roomRent.BookRoomDetailsId),
            }).ToList();

            return roomRentVMs;
        }

        // đổi phòng
        public ExecutionOutcome ChangeRoom(int bookRoomDetailsIdint, int roomIdOld, int roomIdNew, bool isCleanRoom = false)
        {
            var roomOld = RoomDao.GetRoomById(roomIdOld);
            var roomNew = RoomDao.GetRoomById(roomIdNew);
            var status = false;

            if (roomOld != null && roomNew != null)
            {
                // cập nhật phòng mới
                BookRoomDetailsDao.ChangeRoom(bookRoomDetailsIdint, roomIdNew);

                // cập nhật trạng thái phòng mới
                RoomDao.UpdateStatus(roomIdNew, (int)RoomStatusType.RoomOccupied);

                // cập nhật trạng thái phòng cũ
                RoomDao.UpdateStatus(roomIdOld, (int)RoomStatusType.RoomAvailable);

                // dọn phòng cũ
                RoomDao.RequestCleanRoom(roomIdOld);

                // dọn phòng mới
                if (isCleanRoom)
                {
                    RoomDao.RequestCleanRoom(roomIdNew);
                }

                context.SaveChanges();
                status = true;
            }

            ExecutionOutcome executionOutcome = new ExecutionOutcome
            {
                Result = status,
                Mess = status == false ? "Hệ thống lỗi. Vui lòng thử lại sau" : ""
            };

            return executionOutcome;
        }

        // thêm menu
        public OrderMenuAdminVM OrderMenu(int bookRoomDetailsId)
        {
            var services = ServiceDao.GetServices();
            
            OrderMenuAdminVM orderMenuAdminVM = new OrderMenuAdminVM
            {
                BookRoomDetailsId = bookRoomDetailsId,
                Orders = services.Select(service => new Order
                {
                    ServiceId = service.ServiceId,
                }).ToList(),
            };

            return orderMenuAdminVM;
        }

        // thêm menu
        public ExecutionOutcome OrderMenu(int bookRoomDetailsId, List<Order> orders)
        {
            foreach (var order in orders)
            {
                var service = ServiceDao.GetServiceById(order.ServiceId);

                Order newOrder = new Order
                {
                    ServiceId = service.ServiceId,
                    Quantity = order.Quantity,
                    Price = service.Price,
                    OrderDate = DateTime.Now,
                    BookRoomDetailsId = bookRoomDetailsId,
                };

                OrderDao.CreateOrder(newOrder);

                context.SaveChanges();
            }

            ExecutionOutcome executionOutcome = new ExecutionOutcome
            {
                Mess = "Thêm menu thành công.",
                Result = true
            };

            return executionOutcome;
        }

        // chỉnh sửa phòng
        public BookRoomDetailsAdminVM GetBookRoomDetails(int bookRoomDetailsId)
        {
            var bookRoomDetails = BookRoomDetailsDao.GetBookRoomDetailsById(bookRoomDetailsId);
            var orders = OrderDao.GetOrderByBookRoomDetailsId(bookRoomDetailsId);

            BookRoomDetailsAdminVM bookRoomDetailsAdminVM = new BookRoomDetailsAdminVM
            {
                CustomerName = bookRoomDetails.BookRoom.Customer.Name,
                Phone = bookRoomDetails.BookRoom.Customer.Phone,
                CIC = bookRoomDetails.BookRoom.Customer.CIC,
                BookRoomDetailsId = bookRoomDetails.BookRoomDetailsId,
                RoomName = bookRoomDetails.Room.Name,
                CheckIn = bookRoomDetails.CheckIn,
                Note = bookRoomDetails.Note,
                Orders = orders,
            };

            return bookRoomDetailsAdminVM;
        }

        // chỉnh sửa phòng
        public CheckOutVM CheckOut(int bookRoomDetailsId)
        {
            var bookRoomDetails = BookRoomDetailsDao.GetBookRoomDetailsById(bookRoomDetailsId);

            if ((BookRoomDetailsType)bookRoomDetails.Status == BookRoomDetailsType.Received)
            {
                var orders = OrderDao.GetOrderByBookRoomDetailsId(bookRoomDetailsId);

                CheckOutVM checkOutVM = new CheckOutVM
                {
                    BookRoomDetailsId = bookRoomDetailsId,
                    CustomerId = bookRoomDetails.BookRoom.Customer.CustomerId,
                    RoomId = bookRoomDetails.RoomId,
                    RoomName = bookRoomDetails.Room.Name,
                    CheckIn = bookRoomDetails.CheckIn,
                    CheckOut = DateTime.Now,
                    TotalPriceRoom = BillDao.CalcPriceRoom(bookRoomDetailsId),
                    TotalPriceService = BillDao.CalcPriceService(bookRoomDetailsId),
                    Name = bookRoomDetails.BookRoom.Customer.Name,
                    Orders = orders,
                };

                return checkOutVM;
            }

            throw new Exception("Lỗi hệ thống");
        }

        public ExecutionOutcome CheckOut(CheckOutVM checkOutVM)
        {
            string error = null;
            bool status = true;

            try
            {
                // tạo bill
                Bill bill = new Bill()
                {
                    BookRoomDetailsId = checkOutVM.BookRoomDetailsId,
                    ServicePrice = BillDao.CalcPriceService(checkOutVM.BookRoomDetailsId),
                    RoomPrice = BillDao.CalcPriceRoom(checkOutVM.BookRoomDetailsId),
                    TotalPrice = BillDao.CalcTotalPriceRoom(checkOutVM.BookRoomDetailsId),
                    DatePayment = DateTime.Now,
                    CustomerId = checkOutVM.CustomerId,
                    HotelId = myService.GetHotelId(),
                };
                BillDao.CreateBill(bill);

                // cập nhật ngày trả phòng
                BookRoomDetailsDao.UpdateCheckOut(checkOutVM.BookRoomDetailsId, DateTime.Now);

                // cập nhật trạng thái chi tiết đặt phòng
                BookRoomDetailsDao.UpdateStatus(checkOutVM.BookRoomDetailsId, (int)BookRoomDetailsType.Pay);

                // cập nhật trạng thái phòng
                RoomDao.UpdateStatus(checkOutVM.RoomId, (int)RoomStatusType.RoomAvailable);

                // cập nhật trạng thái dọn phòng
                RoomDao.UpdateTidy(checkOutVM.RoomId, (int)RoomTidyType.NotCleaned);

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                error = "Hệ thống lỗi. Vui lòng thử lại sau.";
                status = false;
            }
            

            return new ExecutionOutcome
            {
                Mess = string.IsNullOrEmpty(error) ? "Trả phòng thành công." : error,
                Result = status,
            };
        }

        // chỉnh sửa phòng
        public ExecutionOutcome EditBookRoomDetails(BookRoomDetailsAdminVM bookRoomDetailsAdminVM)
        {
            string error = null;
            bool status = bookRoomDetailsAdminVM.IsValid(out error);

            if (status)
            {
                // cập nhật lại chi tiết đặt phòng
                BookRoomDetails bookRoomDetails = BookRoomDetailsDao.GetBookRoomDetailsById(bookRoomDetailsAdminVM.BookRoomDetailsId);
                bookRoomDetails.CheckIn = bookRoomDetailsAdminVM.CheckIn;
                bookRoomDetails.Note = bookRoomDetailsAdminVM.Note;
                BookRoomDetailsDao.UpdateBookRoomDetails(bookRoomDetails);

                // cập nhật lại thông tin khách hàng
                Customer customer = bookRoomDetails.BookRoom.Customer;
                customer.CIC = bookRoomDetailsAdminVM.CIC;
                customer.Phone = bookRoomDetailsAdminVM.Phone;
                customer.Name = bookRoomDetailsAdminVM.CustomerName;
                CustomerDao.UpdateCustomer(customer);

                context.SaveChanges();
            }

            return new ExecutionOutcome
            {
                Mess = string.IsNullOrEmpty(error) ? "Chỉnh sửa thành công." : error,
                Result = status,
            };
        }

        // dọn phòng
        public ExecutionOutcome CleanRoom(int roomId)
        {
            var room = RoomDao.GetRoomById(roomId);
            var status = false;

            if (room != null)
            {
                RoomDao.CleanRoom(roomId);
                context.SaveChanges();
                status = true;
            }

            ExecutionOutcome executionOutcome = new ExecutionOutcome
            {
                Result = status,
                Mess = status == false ? "Phòng không tồn tại" : ""
            };

            return executionOutcome;
        }

        // yêu cầu dọn phòng
        public ExecutionOutcome RequestCleanRoom(int roomId)
        {
            var room = RoomDao.GetRoomById(roomId);
            var status = false;

            if (room != null)
            {
                RoomDao.RequestCleanRoom(roomId);
                context.SaveChanges();
                status = true;
            }

            ExecutionOutcome executionOutcome = new ExecutionOutcome
            {
                Result = status,
                Mess = status == false ? "Phòng không tồn tại" : ""
            };

            return executionOutcome;
        }

        // nhận phòng
        public ExecutionOutcome CheckIn(int roomId)
        {
            var room = RoomDao.GetRoomById(roomId);
            var status = false;

            if (room != null)
            {
                BookRoomDetailsDao.UpdateCheckInByRoomId(roomId);
                RoomDao.CheckIn(roomId);
                context.SaveChanges();
                status = true;
            }

            ExecutionOutcome executionOutcome = new ExecutionOutcome
            {
                Result = status,
                Mess = status == false ? "Phòng không tồn tại" : "Nhận phòng thành công"
            };

            return executionOutcome;
        }

        // nhận phòng
        public ExecutionOutcome CancelBooking(int roomId)
        {
            var room = RoomDao.GetRoomById(roomId);
            var status = false;

            if (room != null)
            {
                BookRoomDetailsDao.CancelBookRoomDetailsByRoomId(roomId);
                RoomDao.CancelBooking(roomId);
                context.SaveChanges();
                status = true;
            }

            ExecutionOutcome executionOutcome = new ExecutionOutcome
            {
                Result = status,
                Mess = status == false ? "Phòng không tồn tại" : "Hủy đặt phòng thành công"
            };

            return executionOutcome;
        }
    }
}
