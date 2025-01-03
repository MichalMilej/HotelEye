using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelEye.Model;

namespace HotelEye
{
    public class BookingManager(List<Hotel> hotels, List<Booking> bookings)
    {
        public List<Hotel> Hotels { get; set; } = hotels;
        public List<Booking> Bookings { get; set; } = bookings;

        public int CheckAvailability(string hotelId, DateOnly arrivalDate, DateOnly departureDate, string roomTypeCode)
        {
            var hotel = Hotels.Find(h => h.Id == hotelId)
                ?? throw new KeyNotFoundException($"Hotel with id {hotelId} not found.");

            int matchingRooms = hotel.Rooms.Count(r => r.RoomTypeCode == roomTypeCode);
            int availableRooms = matchingRooms;

            var hotelBookings = Bookings.FindAll(b => b.HotelId == hotelId);
            foreach (Booking booking in hotelBookings)
            {
                if (booking.RoomTypeCode != roomTypeCode)
                {
                    continue;
                }
                if (!((arrivalDate < booking.ArrivalDate && departureDate <= booking.ArrivalDate) 
                    || (arrivalDate >= booking.DepartureDate)))
                {
                    availableRooms--;
                }
            }

            return availableRooms;
        }
    }
}
