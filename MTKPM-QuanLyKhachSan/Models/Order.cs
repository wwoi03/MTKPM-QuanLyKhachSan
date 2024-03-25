using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Prototype.OrderPrototype;
using System.ComponentModel.DataAnnotations;

namespace MTKPM_QuanLyKhachSan.Models
{
    public class Order : IPrototype
    {
        [Key]
        public int OrderId { get; set; }
        public int BookRoomDetailsId { get; set; }
        public BookRoomDetails BookRoomDetails { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }

        public Order()
        {

        }

        private Order(Order order)
        {
            OrderId = order.OrderId;
            BookRoomDetailsId = order.BookRoomDetailsId;
            BookRoomDetails = order.BookRoomDetails;
            ServiceId = order.ServiceId;
            Quantity = order.Quantity;
            Price = order.Price;
            OrderDate = order.OrderDate;
        }

        public IPrototype Clone()
        {
            return new Order(this);
        }
    }
}
