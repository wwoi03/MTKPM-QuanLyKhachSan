using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.ProxyProtected.Service
{
    public interface IRentCheckOut
    {
        IActionResult Index();
        IActionResult CleanRoom(int roomId);
        IActionResult RequestCleanRoom(int roomId);
        IActionResult ChangeRoom(int bookRoomDetailsId);
        IActionResult OrderMenu(int bookRoomDetailsId, List<Order> orders);
        IActionResult EditBookRoomDetails(int bookRoomDetailsId);

    }
}
