namespace JCClientCore.Models;

public class ApduParameters
{
	public int IsoCase { get; init; }
	public byte CLA { get; init; }
	public byte INS { get; init; }
	public byte P1 { get; init; }
	public byte P2 { get; init; }
	public int Le { get; init; }
	public byte[]? Data { get; init; }
}