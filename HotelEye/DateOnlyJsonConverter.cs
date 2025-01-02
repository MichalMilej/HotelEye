﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HotelEye
{
    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private readonly string _format = "yyyyMMdd";

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String && DateOnly.TryParseExact(reader.GetString(), _format, out var date))
            {
                return date;
            }
            throw new JsonException($"Unable to convert \"{reader.GetString()}\" to {typeof(DateOnly)}.");
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
