using System.Text.Json;
using System.Text.RegularExpressions;
using HotelEye.HotelEye;
using HotelEye.Model;

namespace HotelEye
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string hotelsJsonFilePath = "../../../TestData/hotels.json";
            string bookingsJsonFilePath = "../../../TestData/bookings.json";

            JsonFileReader jsonFileReader = new JsonFileReader(new DateOnlyJsonConverter());

            List<Hotel> hotels = jsonFileReader.ReadFromFile<Hotel>(hotelsJsonFilePath);
            List<Booking> bookings = jsonFileReader.ReadFromFile<Booking>(bookingsJsonFilePath);

            if (hotels.Count == 0 || bookings.Count == 0)
            {
                Console.WriteLine("Failed to read data from files.");
                return;
            }

            BookingManager bookingManager = new(hotels, bookings);

            while (true)
            {
                string userInput = Console.ReadLine() ?? "";
                if (string.IsNullOrEmpty(userInput))
                {
                    break;
                }

                try
                {
                    AvailabilityRequest availabilityRequest = new(userInput);
                    int availableRooms = bookingManager.CheckAvailability(availabilityRequest.HotelId,
                        availabilityRequest.ArrivalDate,
                        availabilityRequest.DepartureDate,
                        availabilityRequest.RoomTypeCode);

                    Console.WriteLine(availableRooms);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
