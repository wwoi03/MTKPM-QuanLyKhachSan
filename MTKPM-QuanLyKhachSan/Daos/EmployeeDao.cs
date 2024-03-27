using Microsoft.EntityFrameworkCore;
using MTKPM_QuanLyKhachSan.Common;
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

        // tìm nhân viên theo id
        public Employee GetEmployeeById(int? employeeId)
		{
            return context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
		}

        // lấy danh sách tài khoản
        public List<Employee> GetEmployees(int? hotelId)
        {
            return context.Employees.Where(i => i.HotelId == hotelId).ToList();
        }

        // tạo tài khoản 
        public void CreateAccount(Employee employee)
		{
            context.Employees.Add(employee);
        }

        // Khóa tài khoản
        public void LockAccount(int employeeId)
		{
            Employee employee = GetEmployeeById(employeeId);
            employee.Status = (int)EmployeeStatusType.Lock;

            context.Employees.Update(employee);
		}

        // Mở Khóa tài khoản
        public void UnLockAccount(int employeeId)
        {
            Employee employee = GetEmployeeById(employeeId);
            employee.Status = (int)EmployeeStatusType.UnLock;

            context.Employees.Update(employee);
        }

        // đăng nhập
        public Employee Login(string username, string password)
        {
            return context.Employees.FirstOrDefault(i => i.Username.ToLower().Equals(username.ToLower()) && i.Password.Equals(password));
        }
    }
}
