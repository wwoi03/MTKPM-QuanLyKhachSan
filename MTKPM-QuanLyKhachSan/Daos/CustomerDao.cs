using Microsoft.EntityFrameworkCore;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Singleton;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class CustomerDao
    {
        DatabaseContext context;
        public CustomerDao()
        {
            context = SingletonDatabase.Instance;
        }
        public Customer GetCustomerByUserName(string username)
        {
            return context.Customers.Where(item => item.Username.Equals(username)).FirstOrDefault();
        }

        // tạo khách hàng
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

        // kiểm tra khách hàng đã tồn tại với SĐT hoặc CCCD
        public int GetCustomerIdByPhoneOrCIC(string phone, string cic)
        {
            return context.Customers.FirstOrDefault(i => i.Phone.Equals(phone) || i.CIC.Equals(cic)).CustomerId;
        }
    }
}