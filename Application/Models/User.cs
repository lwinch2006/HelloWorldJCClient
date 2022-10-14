using System.Drawing;

namespace Application.Models;

public class User
{
	public string? FirstName { get; init; }
	public string? Lastname { get; init; }
	public string? Email { get; init; }
	public string? Phone { get; init; }
	public byte[] Photo { get; init; } = Array.Empty<byte>();
}