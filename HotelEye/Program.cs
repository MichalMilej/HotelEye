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

            BookingManager bookingManager = new BookingManager(hotels, bookings);

            while (true)
            {
                string userInput = Console.ReadLine() ?? "";
                if (string.IsNullOrEmpty(userInput))
                {
                    break;
                }
                Regex regexOneDate = new Regex(@"Availability\(([^,]+),(\s*\d{8}\s*),([^)]+)\)");
                Regex regexDateRange = new Regex(@"Availability\(([^,]+),(\s*\d{8}\s*)-(\s*\d{8}\s*),([^\)]+)\)");

                Match resultSingleDate = regexOneDate.Match(userInput);
                Match resultDateRange = regexDateRange.Match(userInput);

                string inputHotelId;
                DateOnly inputArrivalDate;
                DateOnly inputDepartureDate;
                string inputRoomTypeCode;

                if (resultSingleDate.Success)
                {
                    inputHotelId = resultSingleDate.Groups[1].Value.Trim();
                    inputArrivalDate = DateOnly.ParseExact(resultSingleDate.Groups[2].Value.Trim(), "yyyyMMdd");
                    inputDepartureDate = inputArrivalDate.AddDays(1);
                    inputRoomTypeCode = resultSingleDate.Groups[3].Value.Trim();
                }
                else if (resultDateRange.Success)
                {
                    inputHotelId = resultDateRange.Groups[1].Value.Trim();
                    inputArrivalDate = DateOnly.ParseExact(resultDateRange.Groups[2].Value.Trim(), "yyyyMMdd");
                    inputDepartureDate = DateOnly.ParseExact(resultDateRange.Groups[3].Value.Trim(), "yyyyMMdd");
                    inputRoomTypeCode = resultDateRange.Groups[4].Value.Trim();
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }

                try
                {
                    int availableRooms = bookingManager.CheckAvailability(inputHotelId, inputArrivalDate, inputDepartureDate,  inputRoomTypeCode);
                    Console.WriteLine(availableRooms);
                }
                catch (KeyNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
