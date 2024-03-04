using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class CustomerDao
    {
        //Khai báo biến context
        DatabaseContext context;

        //Tạo hàm CustomerDao để tham chiếu biến context
        public CustomerDao(DatabaseContext context)
        {
            this.context = context;
        }

        //Lấy số lượng khách hàng
        public int GetQuantityOfCustomer()
        {
            int count = context.Customers.Count();
            return count;
        }

    }
}
