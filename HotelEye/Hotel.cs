using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HotelEye
{
    public class Hotel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<RoomType> RoomTypes { get; set; }
        public List<Room> Rooms { get; set; }

        public class RoomType
        {
            public string Code { get; set; }
            public string Description { get; set; }
        }

        public class Room
        {
            public string RoomType { get; set; }
            public string RoomId { get; set; }
        }
    }
}
