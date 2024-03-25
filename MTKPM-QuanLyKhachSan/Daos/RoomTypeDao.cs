using Microsoft.EntityFrameworkCore;
using MTKPM_QuanLyKhachSan.Models;

namespace MTKPM_QuanLyKhachSan.Daos
{
    public class RoomTypeDao
    {
        DatabaseContext context;

        public RoomTypeDao(DatabaseContext context)
        {
            this.context = context;
        }

        // Lấy danh sách loại phòng
        public List<RoomType> GetRoomTypes(int? hotelId)
        {
            return context.RoomTypes
                .Where(i => i.HotelId == hotelId)
                .ToList();
        }

        // lấy loại phòng theo Id
        public RoomType GetRoomTypeById(int RoomTypeId)
        {
            RoomType roomType = context.RoomTypes.FirstOrDefault(r => r.RoomTypeId == RoomTypeId);
            return roomType;
        }

        // tìm kiếm loại phòng
        public List<RoomType> SearchRoomType(DateTime checkIn, DateTime checkOut, int numAdult, int numChildren)
        {
            List<RoomType> roomTypes;
            if (numAdult > numChildren)
            {
                roomTypes = context.RoomTypes.Where(i => i.NumAdult <= numAdult).ToList();
            }
            else
            {
                roomTypes = context.RoomTypes.Where(i => i.NumAdult <= numAdult || i.NumChildren <= numChildren).ToList();
            }
            return roomTypes;
        }
		public List<RoomType> GetRoomTypes1()
		{
			return context.RoomTypes.OrderByDescending(p => p.RoomTypeId).ToList();
		}
		public void InsertRoomType(RoomType newRoomType)
		{
			context.RoomTypes.Add(newRoomType);
			context.SaveChanges();
		}
	}
}
