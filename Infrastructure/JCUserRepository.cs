using System.Text;
using Infrastructure.Extentions;
using Infrastructure.Models;
using JCClientCore;


namespace Infrastructure;

public class JCUserRepository : IUserRepository
{
	private readonly IJCClient _jcClient;
	
	public JCUserRepository(IJCClient jcClient)
	{
		_jcClient = jcClient;
		_jcClient.TrySelectApplet(InfrastructureConstants.AppletAID);
	}

	public void Connect()
	{
		_jcClient.TrySelectApplet(InfrastructureConstants.AppletAID);
	}
	
	public string GetFirstName()
	{
		var apduParameters = UserApduParamsFactory.GetFirstName();
		var firstNameAsBytes = _jcClient.TryGetCardData(apduParameters).TrimEnd();
		var firstname = Encoding.ASCII.GetString(firstNameAsBytes);
		return firstname;
	}

	public string GetLastName()
	{
		var apduParameters = UserApduParamsFactory.GetLastName();
		var lastNameAsBytes = _jcClient.TryGetCardData(apduParameters).TrimEnd();
		var lastName = Encoding.ASCII.GetString(lastNameAsBytes);
		return lastName;
	}

	public string GetEmail()
	{
		var apduParameters = UserApduParamsFactory.GetEmail();
		var emailAsBytes = _jcClient.TryGetCardData(apduParameters).TrimEnd();
		var email = Encoding.ASCII.GetString(emailAsBytes);
		return email;
	}

	public string GetPhone()
	{
		var apduParameters = UserApduParamsFactory.GetPhone();
		var phoneAsBytes = _jcClient.TryGetCardData(apduParameters).TrimEnd();
		var phone = Encoding.ASCII.GetString(phoneAsBytes);
		return phone;
	}

	public byte[] GetPhoto()
	{
		var apduParameters = UserApduParamsFactory.GetPhoto();
		var photo = _jcClient.TryGetCardData(apduParameters).TrimEnd();
		return photo;
	}
}