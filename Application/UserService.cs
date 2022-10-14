using System.Drawing;
using Application.Models;
using Infrastructure;

namespace Application;

public class UserService : IUserService
{
	private readonly IUserRepository _userRepository; 
	public UserService(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}
	
	public string GetFirstName()
	{
		return _userRepository.GetFirstName();
	}

	public string GetLastName()
	{
		return _userRepository.GetLastName();
	}

	public string GetEmail()
	{
		return _userRepository.GetEmail();
	}

	public string GetPhone()
	{
		return _userRepository.GetPhone();
	}

	public byte[] GetPhoto()
	{
		return _userRepository.GetPhoto();
	}

	public User GetUser()
	{
		var user = new User
		{
			FirstName = GetFirstName(),
			Lastname = GetLastName(),
			Email = GetEmail(),
			Phone = GetPhone(),
			Photo = GetPhoto()
		};

		return user;
	}
}