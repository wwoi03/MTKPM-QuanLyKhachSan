using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class EmployeeDao
    {
        //Khai báo biến context
        DatabaseContext context;

        //Tạo hàm EmployeeDao để tham chiếu biến context
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
