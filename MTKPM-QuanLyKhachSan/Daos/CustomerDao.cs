using Microsoft.EntityFrameworkCore;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class CustomerDao
    {
        //Khai báo biến context
        DatabaseContext context;

        //Tạo hàm CustomerDao để nhận tham chiếu biến context
        public CustomerDao(DatabaseContext context)
        {
            this.context = context;
        }

        //Lấy danh sách khách hàng bằng tên đăng nhập
        public Customer GetCustomerByUserName(string username)
        {
            return context.Customers.Where(item => item.Username.Equals(username)).FirstOrDefault();
        }
        //Thêm khách hàng mới
        public void CreateCustomer(Customer customer)
        {
            context.Customers.Add(customer);
            context.SaveChanges();
        }
        //Lấy danh sách khách hàng bằng Id
		public Customer GetCustomerbyId(int? customerId)
		{
			return context.Customers.Where(item => item.CustomerId.Equals(customerId)).FirstOrDefault();
		}
        //Chỉnh sửa thông tin 
		public void EditInformation(Customer newCustomer)
		{
			context.Customers.Update(newCustomer);
			context.SaveChanges();
		}
	}
}