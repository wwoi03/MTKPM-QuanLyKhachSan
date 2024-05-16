using Microsoft.EntityFrameworkCore;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class BillDao
    {
        DatabaseContext context;

        public BillDao(DatabaseContext context)
        {
            this.context = context;
        }

        // lấy danh sách hóa đơn
        public List<Bill> GetBills(int? hotelId)
        {
            return context.Bills
                .Where(i => i.HotelId == hotelId)
                .Include(i => i.BookRoomDetails)
                .Include(i => i.BookRoomDetails.Room)
                .OrderBy(i => i.DatePayment)
                .ToList();
        }

        // tiền phòng
        public decimal CalcPriceRoom(int bookRoomDetailsId)
        {
            var bookRoomDetails = context.BookRoomDetails
                .Include(i => i.Room)
                .Include(i => i.Room.RoomType)
                .FirstOrDefault(i => i.BookRoomDetailsId == bookRoomDetailsId);

            int days = (DateTime.Now - bookRoomDetails.CheckIn).Days;

            decimal priceRoom = bookRoomDetails.Room.RoomType.Price * (days == 0 ? 1 : days);

            return priceRoom;
        }

        public decimal CalcPriceService(int bookRoomDetailsId)
        {
            decimal priceOrder = context.Orders
                .Where(i => i.BookRoomDetailsId == bookRoomDetailsId)
                .Sum(i => i.Quantity * i.Price);

            return priceOrder;
        }

        // tổng tiền phòng
        public decimal CalcTotalPriceRoom(int bookRoomDetailsId)
        {
            decimal totalPrice = CalcPriceService(bookRoomDetailsId) + CalcPriceRoom(bookRoomDetailsId);

            return totalPrice;
        }

        // Tạo bill
        public void CreateBill(Bill bill)
        {
            context.Bills.Add(bill);
        }
    }
}
