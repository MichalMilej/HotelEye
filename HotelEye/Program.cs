using HotelEye.HotelEye;
using HotelEye.Model;

namespace HotelEye
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 4)
            {
                Console.WriteLine("Invalid number of program parameters.");
                return;
            }

            int hotelsPathArgIndex = args[0].Equals("--hotels") ? 1 : 3;
            int bookingsPathArgIndex = args[0].Equals("--bookings") ? 1 : 3;

            JsonFileReader jsonFileReader = new(new DateOnlyJsonConverter());

            List<Hotel> hotels = jsonFileReader.ReadFromFile<Hotel>(args[hotelsPathArgIndex]);
            List<Booking> bookings = jsonFileReader.ReadFromFile<Booking>(args[bookingsPathArgIndex]);

            if (!(hotels.Count != 0 && hotels.All(h => !string.IsNullOrEmpty(h.Id)
                && bookings.Count != 0 && bookings.All(b => !string.IsNullOrEmpty(b.HotelId)))))
            {
                Console.WriteLine("Failed to read data from files.");
                return;
            }

            BookingManager bookingManager = new(hotels, bookings);

            while (true)
            {
                Console.WriteLine("Enter command (ex. \"Availability(H1, 20250101-20250105, SGL)\") or press enter to exit: ");
                string userInput = Console.ReadLine() ?? "";
                if (string.IsNullOrEmpty(userInput))
                {
                    return;
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
