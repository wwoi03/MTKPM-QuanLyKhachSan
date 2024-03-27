using Microsoft.EntityFrameworkCore;
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
        public Customer GetCustomerByUserName(string username)
        {
            return context.Customers.Where(item => item.Username.Equals(username)).FirstOrDefault();
        }

        // tạo khách hàng
        public void CreateCustomer(Customer customer)
        {
            context.Customers.Add(customer);
        }

        // cập nhật thông tin khách hàng
        public void UpdateCustomer(Customer customer)
        {
            context.Customers.Update(customer);
        }

        // kiểm tra khách hàng đã tồn tại với SĐT hoặc CCCD
        public int GetCustomerIdByPhoneOrCIC(string phone, string cic)
        {
            Customer? customer = context.Customers.FirstOrDefault(i => i.Phone.Equals(phone) || i.CIC.Equals(cic));

            if (customer == null)
                return 0;

            return customer.CustomerId;
        }
    }
}