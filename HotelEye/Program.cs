using System.Text.Json;

namespace HotelEye
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string hotelsJsonFilePath = "../../../TestData/hotels.json";
            string bookingsJsonFilePath = "../../../TestData/bookings.json";

            try
            {
                string hotelsJsonString = File.ReadAllText(hotelsJsonFilePath);
                string bookingsJsonString = File.ReadAllText(bookingsJsonFilePath);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new DateOnlyJsonConverter() }
                };

                List<Hotel> hotels = JsonSerializer.Deserialize<List<Hotel>>(hotelsJsonString, options);
                List<Booking> bookings = JsonSerializer.Deserialize<List<Booking>>(bookingsJsonString, options);

            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (JsonException e)
            {
                Console.WriteLine(e.Message);
            }     
        }
    }
}
