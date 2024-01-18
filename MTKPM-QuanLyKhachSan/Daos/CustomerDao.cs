using Microsoft.EntityFrameworkCore;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class CustomerDao
    {
		DatabaseContext context;
		public CustomerDao(DatabaseContext context) {
            this.context = context;
        }
        public Customer GetCustomerByUserName(string username) 
        {
            return context.Customers.Where(item => item.Username.Equals(username)).FirstOrDefault();
        }
    }
}
