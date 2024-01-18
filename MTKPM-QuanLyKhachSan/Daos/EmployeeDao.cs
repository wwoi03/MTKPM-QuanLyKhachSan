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

        //Lấy số lượng nhân viên
        public int GetQuantityOfEmployee()
        {
            int count = context.Employees.Count();
            return count;
        }
    }
}
