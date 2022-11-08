using BusinessLogic;
using Infrastructure;
using JCClientCore;
using Microsoft.AspNetCore.Mvc;
using MvcWebApp.Models.UserDetails;

namespace MvcWebApp.Controllers;

public class UserDetailsController : Controller
{
	private readonly IUserService _userService;
	
	public UserDetailsController()
	{
		_userService = new UserService(new JCUserRepository(new JCClient()));
	}

	public IActionResult Index()
	{
		var user = _userService.GetUserWithoutPhoto();

		var vm = new UserDetailsViewModel
		{
			FirstName = user.FirstName,
			Lastname = user.Lastname,
			Email = user.Email,
			Phone = user.Phone
		};

		return View(vm);
	}

	public IActionResult GetUserPhoto()
	{
		var photoAsBytes = _userService.GetPhoto();
		return File(photoAsBytes, "image/webp");
	}
}