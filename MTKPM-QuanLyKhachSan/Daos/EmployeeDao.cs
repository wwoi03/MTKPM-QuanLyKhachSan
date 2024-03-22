using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class EmployeeDao
    {
        DatabaseContext context;

        public EmployeeDao(DatabaseContext context)
        {
            this.context = context;
        }

        // tạo tài khoản 
        public void CreateAccount(Employee employee)
		{
            context.Employees.Add(employee);
            context.SaveChanges();
		}
    }
}
