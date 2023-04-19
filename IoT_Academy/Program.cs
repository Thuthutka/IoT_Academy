using System;
using System.Collections.Generic;
using System.Linq;
using Geolocation;

namespace IoT_Academy
{

    class Program
    {
        static void Main(string[] args)
        {
            var fileReader = new FileReader();
            var elements = new List<Element>();

            // Read elements from the JSON file
            var jsonElements = fileReader.ReadElementsFromJsonFile("data.json");
            elements.AddRange(jsonElements);

            // Read elements from the CSV file
            var csvElements = fileReader.ReadElementsFromCsvFile("data.csv");
            elements.AddRange(csvElements);

            DrawSatellitesHistogram(elements);
            DrawSpeedHistogram(elements);
            CalculateSections(elements);
        }

        static void DrawSatellitesHistogram(List<Element> elements)
        {
            var satelliteCounts = elements.GroupBy(e => e.Satellites)
                                          .ToDictionary(g => g.Key, g => g.Count());

            Console.WriteLine("Satellites Histogram:");
            foreach (var pair in satelliteCounts.OrderBy(x => x.Key))
            {
                Console.Write($"{pair.Key}:".PadRight(4));

                for (int i = 0; i < pair.Value / 100; i++)
                {
                    Console.Write("*");
                }

                Console.WriteLine($" ({pair.Value})");
            }
        }

        static void DrawSpeedHistogram(List<Element> elements)
        {
            var speedCounts = elements.GroupBy(e => e.Speed / 10 * 10)
                                       .ToDictionary(g => $"{g.Key}-{g.Key + 9}", g => g.Count());

            Console.WriteLine("Speed Histogram:");
            foreach (var pair in speedCounts.OrderBy(x => x.Key))
            {
                Console.Write($"{pair.Key}:".PadRight(6));

                for (int i = 0; i < pair.Value / 100; i++)
                {
                    Console.Write("*");
                }

                Console.WriteLine($" ({pair.Value})");
            }
        }

        static void CalculateSections(List<Element> elements)
        {
            Element startElement = null;
            Element endElement = null;
            List<Section> sections = new List<Section>();

            for (int i = 0; i < elements.Count - 1; i++)
            {
                Element currentElement = elements[i];
                Element nextElement = elements[i + 1];

                double distance = GeoCalculator.GetDistance(currentElement.Latitude, currentElement.Longitude, nextElement.Latitude, nextElement.Longitude);

                if (distance >= 100000)
                {
                    endElement = nextElement;
                    TimeSpan time = endElement.GpsTime - startElement.GpsTime;
                    sections.Add(new Section(startElement, endElement, time));
                    startElement = null;
                    endElement = null;
                }
                else if (startElement == null)
                {
                    startElement = currentElement;
                }
            }

            if (sections.Count > 0)
            {
                Console.WriteLine($"Found {sections.Count} sections of at least 100km long:");
                foreach (var section in sections)
                {
                    double sectionDistance = GeoCalculator.GetDistance(section.StartElement.Latitude, section.StartElement.Longitude, section.EndElement.Latitude, section.EndElement.Longitude);
                    double sectionTime = section.Time.TotalMinutes;
                    double averageSpeed = (sectionDistance / sectionTime) * 60;

                    Console.WriteLine($"Section starts at ({section.StartElement.Latitude}, {section.StartElement.Longitude}) and ends at ({section.EndElement.Latitude}, {section.EndElement.Longitude})");
                    Console.WriteLine($"Start time: {section.StartElement.GpsTime}, End time: {section.EndElement.GpsTime}");
                    Console.WriteLine($"Section distance: {sectionDistance:N2} km, Section time: {sectionTime:N2} minutes, Average speed: {averageSpeed:N2} km/h");
                }
            }
            else
            {
                Console.WriteLine("No sections of at least 100km found.");
            }
        }
    }
}
