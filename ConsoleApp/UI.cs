using System.Diagnostics;
using BusinessLogic;
using Infrastructure;
using JCClientCore;
using JCClientCore.Models;


namespace ConsoleApp;

public class UI
{
	private readonly IJCClient _jcClient;

	public UI()
	{
		_jcClient = new JCClient();
	}

	public void Run()
	{
		PrintWelcome();
		_jcClient.StartCardMonitor(PrintGetUserData, PrintWelcome);
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
			var stopWatch = new Stopwatch();
			stopWatch.Start();
			
			var userService = new UserService(new JCUserRepository(_jcClient));
			var user = userService.GetUserWithoutPhoto();
		
			Console.Clear();
			PrintWelcome();
			Console.WriteLine("Firstname: {0}", user.FirstName);
			Console.WriteLine("Lastname: {0}", user.Lastname);
			Console.WriteLine("Email: {0}", user.Email);
			Console.WriteLine("Phone: {0}", user.Phone);
			Console.WriteLine("Photo: {0}", user.Photo.Length > 0 ? "yes" : "no");
			
			stopWatch.Stop();
			var elapsed = stopWatch.Elapsed;
			Console.WriteLine();
			Console.WriteLine("Time: {0} seconds", elapsed.TotalSeconds);
			
			if (user.Photo.Length > 0)
			{
				File.WriteAllBytes("photo.webp", user.Photo);
			}
		}
		catch (CardOperationException ex)
		{
			Console.WriteLine(ex.Message);
		}
	}

	private void WaitPressAnyKeyAndExit()
	{
		Console.ReadKey();
		_jcClient.StopCardMonitor();
		Console.WriteLine("Exited");
	}
}