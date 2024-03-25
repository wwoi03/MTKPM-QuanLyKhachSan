using Microsoft.EntityFrameworkCore;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Singleton;
using MTKPM_QuanLyKhachSan.Common;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class EmployeeDao
    {
        DatabaseContext context;

        public EmployeeDao()
        {
            context = SingletonDatabase.Instance;
        }

        // tìm nhân viên theo id
        public Employee GetEmployeeById(int employeeId)
		{
            return context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
		}

        // lấy danh sách tài khoản
        public List<Employee> GetEmployees(int hotelId)
        {
            return context.Employees.Where(i => i.HotelId == hotelId).ToList();
        }

        // tạo tài khoản 
        public void CreateAccount(Employee employee)
		{
            context.Employees.Add(employee);
            context.SaveChanges();
        }

      
        //Update Employee
        public void UpdateEmployee(Employee employee)
        {
            context.Employees.Update(employee);
            context.SaveChanges();
        }
    }
}
