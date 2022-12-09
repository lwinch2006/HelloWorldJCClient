namespace BlazorComponents.Models;

public class User
{
	public string? FirstName { get; set; }
	public bool FirstNameChanged { get; private set; }
	
	public string? LastName { get; set; }
	public bool LastNameChanged { get; private set; }
	
	public string? Email { get; set; }
	public bool EmailChanged { get; private set; }
	
	public string? Phone { get; set; }
	public bool PhoneChanged { get; private set; }
	
	public byte[]? Photo { get; set; }
	public bool PhotoChanged { get; private set; }
}