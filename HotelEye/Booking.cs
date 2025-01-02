using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HotelEye
{
    public class Booking
    {
        public string HotelId { get; set; }
        [JsonPropertyName("arrival")]
        public DateOnly ArrivalDate { get; set; }
        [JsonPropertyName("departure")]
        public DateOnly DepartureDate { get; set; }
        public string RoomType { get; set; }
        public string RoomRate { get; set; }
    }
}
