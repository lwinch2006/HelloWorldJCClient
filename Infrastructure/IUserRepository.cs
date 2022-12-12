namespace Infrastructure;

public interface IUserRepository
{
	void Connect();
	string GetFirstName();
	string GetLastName();
	string GetEmail();
	string GetPhone();
	byte[] GetPhoto();
}