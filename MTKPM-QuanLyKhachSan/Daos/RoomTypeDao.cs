﻿using Microsoft.EntityFrameworkCore;
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
        public List<RoomType> GetRoomTypes()
        {
            return context.RoomTypes.ToList();
        }

        // lấy loại phòng theo Id
        public RoomType GetRoomType(int RoomTypeId)
        {
            RoomType roomType = context.RoomTypes.FirstOrDefault(r => r.RoomTypeId == RoomTypeId);
            return roomType;
        }
    }
}
