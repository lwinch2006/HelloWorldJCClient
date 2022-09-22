namespace HelloWorldJCClient.Utils;

public static class ByteUtils
{
	public static byte[] TrimEnd(this byte[] array)
	{
		var lastIndex = Array.FindLastIndex(array, b => b != 0);

		Array.Resize(ref array, lastIndex + 1);

		return array;
	}
}