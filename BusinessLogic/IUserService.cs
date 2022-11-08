using BusinessLogic.Models;

namespace BusinessLogic;

public interface IUserService
{
	string? GetFirstName();
	string? GetLastName();
	string? GetEmail();
	string? GetPhone();
	byte[]? GetPhoto();
	User? GetUser();
	User? GetUserWithoutPhoto();
}