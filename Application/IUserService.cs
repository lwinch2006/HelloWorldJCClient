using Application.Models;

namespace Application;

public interface IUserService
{
	string GetFirstName();
	string GetLastName();
	string GetEmail();
	string GetPhone();
	byte[] GetPhoto();
	User GetUser();
}