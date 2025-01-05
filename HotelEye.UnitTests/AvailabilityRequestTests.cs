namespace HotelEye.UnitTests
{
    public class AvailabilityRequestTests
    {
        [TestCase("Availability(H1, 20250101, SGL)", "H1", "2025-01-01", "2025-01-02", "SGL")]
        [TestCase("Availability(H2,20250105,DBL)", "H2", "2025-01-05", "2025-01-06", "DBL")]
        [TestCase("Availability(H3, 20250101-20250106, TPL)", "H3", "2025-01-01", "2025-01-06", "TPL")]
        [TestCase("Availability(H1, 20190601-20190701, SGL)", "H1", "2019-06-01", "2019-07-01", "SGL")]
        public void Create_Availability_Request(string userInput, string expHotelId, string expArrivalDateStr, string expDepartureDateStr,
            string expRoomTypeCode)
        {
            DateOnly expArrivalDate = DateOnly.Parse(expArrivalDateStr);
            DateOnly expDepartureDate = DateOnly.Parse(expDepartureDateStr);

            AvailabilityRequest availabilityRequest = new(userInput);

            Assert.Multiple(() =>
            {
                Assert.That(expHotelId, Is.EqualTo(availabilityRequest.HotelId));
                Assert.That(expArrivalDate, Is.EqualTo(availabilityRequest.ArrivalDate));
                Assert.That(expDepartureDate, Is.EqualTo(availabilityRequest.DepartureDate));
                Assert.That(expRoomTypeCode, Is.EqualTo(availabilityRequest.RoomTypeCode));
            });
        }

        [TestCase("Availability")]
        [TestCase("Availability(H1, SGL)")]
        [TestCase("Availability(H1, text, SGL)")]
        [TestCase("Availability(H1, 0001000102142, SGL)")]
        [TestCase("Availability(H1, 20250101-2025, SGL)")]
        public void Throw_Argument_Exception_When_Invalid_Input(string userInput)
        {
            Assert.Throws<ArgumentException>(() => new AvailabilityRequest(userInput));
        }
    }
}
