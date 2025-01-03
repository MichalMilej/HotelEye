using System.Text.Json;
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

            BookingManager bookingManager = new BookingManager(hotels, bookings);

            DateOnly arrivalDate = DateOnly.ParseExact("20240903", "yyyyMMdd");
            DateOnly departureDate = DateOnly.ParseExact("20240907", "yyyyMMdd");

            try
            {
                int availableRooms = bookingManager.CheckAvailability("H1", arrivalDate, departureDate, "DBL");
                Console.WriteLine(availableRooms);
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
