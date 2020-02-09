namespace OpenStreetMap.AddressParser.Extensions
{
	public static class ArgsExtensions
	{
		public static string TryGet(this string[] args, int index)
		{
			if (args.Length == 0) return null;

			return args.Length >= index + 1 ? args[index] : null;
		}
	}
}