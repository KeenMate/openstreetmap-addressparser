namespace OpenStreetMap.AddressParser.Models
{
	public class CompleteAddress
	{
		public string Latitude { get; set; }
		public string Longitude { get; set; }

		public string State { get; set; }
		public string City { get; set; }

		public string Street { get; set; }
		public string HouseNumber { get; set; }
		public string PostCode { get; set; }
	}
}