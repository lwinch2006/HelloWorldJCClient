using System.Drawing;
using BusinessLogic.Models;
using Infrastructure;

namespace BusinessLogic;

public class UserService : IUserService
{
	private readonly IUserRepository _userRepository; 
	public UserService(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}
	
	public string? GetFirstName()
	{
		return _userRepository.GetFirstName();
	}

	public string? GetLastName()
	{
		return _userRepository.GetLastName();
	}

	public string? GetEmail()
	{
		return _userRepository.GetEmail();
	}

	public string? GetPhone()
	{
		return _userRepository.GetPhone();
	}

	public byte[]? GetPhoto()
	{
		return _userRepository.GetPhoto();
	}

	public User? GetUser()
	{
		_userRepository.Connect();
		
		var email = GetEmail();

		if (string.IsNullOrWhiteSpace(email))
		{
			return null;
		}
		
		var user = new User
		{
			FirstName = GetFirstName(),
			Lastname = GetLastName(),
			Email = email,
			Phone = GetPhone(),
			Photo = GetPhoto()
		};

		return user;
	}

	public User GetUserWithoutPhoto()
	{
		var user = new User
		{
			FirstName = GetFirstName(),
			Lastname = GetLastName(),
			Email = GetEmail(),
			Phone = GetPhone()
		};

		return user;
	}
}