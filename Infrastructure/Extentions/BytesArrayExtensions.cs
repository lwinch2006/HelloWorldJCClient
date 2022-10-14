namespace Infrastructure.Extentions;

public static class BytesArrayExtensions
{
	public static byte[] TrimEnd(this byte[] array)
	{
		var lastIndex = Array.FindLastIndex(array, b => b != 0);
		Array.Resize(ref array, lastIndex + 1);
		return array;
	}
}