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

        public void CreateCustomer(Customer customer)
        {
            context.Customers.Add(customer);
            context.SaveChanges();
        }

        // cập nhật thông tin khách hàng
        public void UpdateCustomer(Customer customer)
        {
            context.Customers.Update(customer);
            context.SaveChanges();
        }
    }
}