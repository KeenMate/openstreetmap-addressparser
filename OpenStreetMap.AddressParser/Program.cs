using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using CsvHelper;
using CsvHelper.Configuration;
using OpenStreetMap.AddressParser.Extensions;
using OpenStreetMap.AddressParser.Models;
using OpenStreetMap.AddressParser.Models.Xml2CSharp;

namespace OpenStreetMap.AddressParser
{
	class Program
	{
		static void Main(string[] args)
		{
			var inputFile = args.TryGet(0) ?? "map.osm";
			var outputFile = args.TryGet(1) ?? "addresses.csv";

			var osm = ReadOsmFile(inputFile);
			// filter out only nodes with address tags
			var addresses = osm.Node.Where(x => x.Tag.Any(t => t.Key == "addr:city")).ToList();
			var completeAddresses = ConvertToWholeAddresses(addresses);
			WriteToCsv(outputFile, completeAddresses);
			//WriteToJson(outputFile, completeAddresses);
		}

		private static void WriteToCsv(string filename, List<CompleteAddress> completeAddresses)
		{
			CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture);
			csvConfig.Delimiter = ";";
			csvConfig.Encoding = Encoding.UTF8;

			using (var writer = new StreamWriter(filename))
			using (var csv = new CsvWriter(writer, csvConfig))
			{
				csv.WriteRecords(completeAddresses);
			}
		}

		private static void WriteToJson(string filename, List<CompleteAddress> completeAddresses)
		{
			var options = new JsonSerializerOptions
			{
				WriteIndented = true,
			};
			var jsonString = JsonSerializer.Serialize(completeAddresses, options);
			File.WriteAllText(filename, jsonString);
		}

		private static List<CompleteAddress> ConvertToWholeAddresses(List<Node> addresses)
		{
			return addresses.Select(ParseSingleAddress).ToList();
		}

		private static CompleteAddress ParseSingleAddress(Node address)
		{
			var newAddress = new CompleteAddress();
			newAddress.City = address.Tag.Find(t => t.Key == "addr:city")?.Value;
			newAddress.HouseNumber = address.Tag.Find(t => t.Key == "addr:housenumber")?.Value;
			newAddress.PostCode = address.Tag.Find(t => t.Key == "addr:postcode")?.Value;
			newAddress.State = address.Tag.Find(t => t.Key == "addr:state")?.Value;
			newAddress.Street = address.Tag.Find(t => t.Key == "addr:street")?.Value;

			newAddress.Latitude = address.Latitude;
			newAddress.Longitude = address.Longitude;

			return newAddress;
		}

		public static Osm ReadOsmFile(string filename)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(Osm));

			using Stream reader = new FileStream(filename, FileMode.Open);
			return (Osm)serializer.Deserialize(reader);
		}

	}
}
