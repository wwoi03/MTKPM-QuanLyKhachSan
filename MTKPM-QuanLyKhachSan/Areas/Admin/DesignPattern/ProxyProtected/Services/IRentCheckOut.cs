using Microsoft.AspNetCore.Mvc;
using MTKPM_QuanLyKhachSan.Models;
using MTKPM_QuanLyKhachSan.ViewModels;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.ProxyProtected.Services
{
    public interface IRentCheckOut
    {
        IActionResult Index();
        IActionResult RoomWait();
        IActionResult RoomRent();
        IActionResult RoomClean();
        IActionResult RoomHistory();
        IActionResult CleanRoom(int roomId);
        IActionResult RequestCleanRoom(int roomId);
        IActionResult ChangeRoom(int bookRoomDetailsId);
        IActionResult ChangeRoom(int bookRoomDetailsId, int roomIdOld, int roomIdNew, bool isCleanRoom = false);
        IActionResult OrderMenu(int bookRoomDetailsId);
        IActionResult OrderMenu(int bookRoomDetailsId, List<Order> orders);
        IActionResult EditBookRoomDetails(int bookRoomDetailsId);
        IActionResult EditBookRoomDetails(BookRoomDetailsAdminVM bookRoomDetailsAdminVM);
        IActionResult CheckIn(int roomId);
        IActionResult CheckOut(int bookRoomDetailsId);
        IActionResult CheckOut(CheckOutVM checkOutVM);
        IActionResult CancelBooking(int roomId);
    }
}
