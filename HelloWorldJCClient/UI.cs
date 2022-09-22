using HelloWorldJCClient.Models;
using HelloWorldJCClient.Services;

namespace HelloWorldJCClient;

public class UI
{
	private readonly CardService _cardService;
	private readonly UserService _userService;

	public UI()
	{
		_cardService = new CardService();
		_userService = new UserService();
	}

	public void Run()
	{
		PrintWelcome();
		_cardService.StartCardMonitor(PrintGetUserData, PrintWelcome);
		WaitPressAnyKeyAndExit();
	}
	
	private void PrintWelcome()
	{
		Console.Clear();
		Console.WriteLine("╔═════════════════════════════════════════════╗");
		Console.WriteLine("║                                             ║");
		Console.WriteLine("║       Personal Identity Verification        ║");
		Console.WriteLine("║                                             ║");
		Console.WriteLine("║          Reader: ACS ACR 38U-CCID           ║");
		Console.WriteLine("║                                             ║");
		Console.WriteLine("║           (Press any key to exit)           ║");
		Console.WriteLine("║                                             ║");
		Console.WriteLine("║                                             ║");
		Console.WriteLine("╚═════════════════════════════════════════════╝");
		Console.WriteLine();
	}

	private void PrintGetUserData()
	{
		try
		{
			var user = _userService.GetUser();
		
			Console.Clear();
			PrintWelcome();
			Console.WriteLine("Firstname: {0}", user.FirstName);
			Console.WriteLine("Lastname: {0}", user.Lastname);
			Console.WriteLine("Email: {0}", user.Email);
			Console.WriteLine("Phone: {0}", user.Phone);
		}
		catch (OperationException ex)
		{
			Console.WriteLine(ex.Message);
		}
	}

	private void WaitPressAnyKeyAndExit()
	{
		Console.ReadKey();
		_cardService.StopCardMonitor();
		Console.WriteLine("Exited");
	}
}