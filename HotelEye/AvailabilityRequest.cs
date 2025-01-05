using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HotelEye
{
    public partial class AvailabilityRequest
    {
        public string HotelId { get; set; }
        public DateOnly ArrivalDate { get; set; }
        public DateOnly DepartureDate { get; set; }
        public string RoomTypeCode { get; set; }

        public AvailabilityRequest(string userInput)
        {
            Regex regexSingleDate = RegexSingleDate();
            Regex regexDateRange = RegexDateRange();

            Match resultSingleDate = regexSingleDate.Match(userInput);
            Match resultDateRange = regexDateRange.Match(userInput);

            if (resultSingleDate.Success)
            {
                HotelId = resultSingleDate.Groups[1].Value.Trim();
                ArrivalDate = DateOnly.ParseExact(resultSingleDate.Groups[2].Value.Trim(), "yyyyMMdd");
                DepartureDate = ArrivalDate.AddDays(1);
                RoomTypeCode = resultSingleDate.Groups[3].Value.Trim();
            }
            else if (resultDateRange.Success)
            {
                HotelId = resultDateRange.Groups[1].Value.Trim();
                ArrivalDate = DateOnly.ParseExact(resultDateRange.Groups[2].Value.Trim(), "yyyyMMdd");
                DepartureDate = DateOnly.ParseExact(resultDateRange.Groups[3].Value.Trim(), "yyyyMMdd");
                RoomTypeCode = resultDateRange.Groups[4].Value.Trim();
            } else
            {
                throw new ArgumentException("Invalid input format.");
            }
        }

        [GeneratedRegex(@"Availability\(([^,]+),(\s*\d{8}\s*),([^)]+)\)")]
        private static partial Regex RegexSingleDate();

        [GeneratedRegex(@"Availability\(([^,]+),(\s*\d{8}\s*)-(\s*\d{8}\s*),([^\)]+)\)")]
        private static partial Regex RegexDateRange();
    }
}
