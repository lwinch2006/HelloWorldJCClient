using System.Drawing;

namespace BusinessLogic.Models;

public class User
{
	public string? FirstName { get; init; }
	public string? Lastname { get; init; }
	public string? Email { get; init; }
	public string? Phone { get; init; }
	public byte[]? Photo { get; init; }
}