using HotelEye.Model;

namespace HotelEye.UnitTests
{
    [TestFixture]
    public class BookingManagerTests
    {
        private BookingManager _bookingManager;

        [SetUp]
        public void Setup()
        {
            List<Hotel> hotels = new List<Hotel>
            {
                new()
                {
                    Id = "H1",
                    Name = "Hotel California",
                    RoomTypes = new List<Hotel.RoomType>()
                    {
                        new ()
                        {
                            Code = "SGL",
                            Description = "Single room"
                        },
                        new ()
                        {
                            Code = "DBL",
                            Description = "Double room"
                        }                    
                    },
                    Rooms = new List<Hotel.Room>()
                    {
                        new()
                        {
                            RoomTypeCode = "SGL",
                            RoomId = "101"
                        },
                        new()
                        {
                            RoomTypeCode = "SGL",
                            RoomId = "102"
                        },
                        new()
                        {
                            RoomTypeCode = "DBL",
                            RoomId = "201"
                        },
                        new ()
                        {
                            RoomTypeCode = "DBL",
                            RoomId = "202"
                        }
                    }
                }
            };

            List<Booking> bookings = new List<Booking>()
            {
                new()
                {
                    HotelId = "H1",
                    ArrivalDate = new DateOnly(2025, 1, 1),
                    DepartureDate = new DateOnly(2025, 1, 3),
                    RoomTypeCode = "SGL",
                    RoomRate = "Prepaid"
                },
                new()
                {
                    HotelId = "H1",
                    ArrivalDate = new DateOnly(2025, 1, 1),
                    DepartureDate = new DateOnly(2025, 1, 5),
                    RoomTypeCode = "SGL",
                    RoomRate = "Standard"
                },
                new()
                {
                    HotelId = "H1",
                    ArrivalDate = new DateOnly(2025, 1, 1),
                    DepartureDate = new DateOnly(2025, 1, 3),
                    RoomTypeCode = "DBL",
                    RoomRate = "Standard"
                }
            };

            _bookingManager = new BookingManager(hotels, bookings);
        }

        [TestCase("H1", "2025-01-01", "2025-01-02", "SGL", 0)]
        [TestCase("H1", "2025-01-03", "2025-01-06", "SGL", 1)]
        [TestCase("H1", "2025-01-06", "2025-01-08", "SGL", 2)]
        [TestCase("H1", "2025-01-02", "2025-01-05", "DBL", 1)]
        [TestCase("H1", "2025-01-03", "2025-01-10", "DBL", 2)]
        public void Return_Available_Rooms_Number(string hotelId, string arrivalDateStr, string departureDateStr, string roomTypeCode, int expected)
        {
            var arrivalDate = DateOnly.Parse(arrivalDateStr);
            var departureDate = DateOnly.Parse(departureDateStr);

            int availableRooms = _bookingManager.CheckAvailability(hotelId, arrivalDate, departureDate, roomTypeCode);

            Assert.That(availableRooms, Is.EqualTo(expected));
        }
    }
}