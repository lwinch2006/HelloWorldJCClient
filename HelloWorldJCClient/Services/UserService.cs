using HelloWorldJCClient.Models;

namespace HelloWorldJCClient.Services;

public class UserService
{
	private readonly CardService _cardService;
	private readonly InstructionService _instructionService;

	public UserService()
	{
		_cardService = new CardService();
		_instructionService = new InstructionService();
	}

	public User GetUser()
	{
		_cardService.SelectApplet("D2:76:00:01:24:10:00:01", ":");
		
		var firstname = _instructionService.GetFirstname();
		var lastname = _instructionService.GetLastname();
		var email = _instructionService.GetEmail();
		var phone = _instructionService.GetPhone();
		
		var user = new User
		{
			FirstName = firstname,
			Lastname = lastname,
			Email = email,
			Phone = phone
		};

		return user;
	}
}