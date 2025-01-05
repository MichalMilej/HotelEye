namespace HotelEye
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.Json;

    namespace HotelEye
    {
        public class JsonFileReader
        {
            private readonly JsonSerializerOptions _options;

            public JsonFileReader(DateOnlyJsonConverter dateOnlyJsonConverter)
            {
                _options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { dateOnlyJsonConverter }
                };
            }

            public List<T> ReadFromFile<T>(string filePath)
            {
                try
                {
                    string jsonString = File.ReadAllText(filePath);
                    return JsonSerializer.Deserialize<List<T>>(jsonString, _options);
                }
                catch (DirectoryNotFoundException e)
                {
                    Console.WriteLine($"Directory not found: {e.Message}");
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine($"File not found: {e.Message}");
                }
                catch (JsonException e)
                {
                    Console.WriteLine($"JSON error: {e.Message}");
                }
                return [];
            }
        }
    }
}
