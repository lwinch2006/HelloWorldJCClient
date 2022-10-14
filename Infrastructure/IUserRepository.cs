namespace Infrastructure;

public interface IUserRepository
{
	string GetFirstName();
	string GetLastName();
	string GetEmail();
	string GetPhone();
	byte[] GetPhoto();
}