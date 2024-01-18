using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class CustomerDao
    {
        DatabaseContext context;

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
