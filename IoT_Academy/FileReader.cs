using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;

namespace IoT_Academy
{
    class FileReader
    {
        public List<Element> ReadElementsFromJsonFile(string filePath)
        {
            var jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Element>>(jsonString);
        }

        public List<Element> ReadElementsFromCsvFile(string filePath)
        {
            var elements = new List<Element>();

            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var element = new Element
                    {
                        Latitude = double.Parse(values[0], CultureInfo.InvariantCulture),
                        Longitude = double.Parse(values[1], CultureInfo.InvariantCulture),
                        GpsTime = DateTime.Parse(values[2]),
                        Speed = int.Parse(values[3]),
                        Angle = int.Parse(values[4]),
                        Altitude = int.Parse(values[5]),
                        Satellites = int.Parse(values[6])
                    };

                    elements.Add(element);
                }
            }

            return elements;
        }
    }
}