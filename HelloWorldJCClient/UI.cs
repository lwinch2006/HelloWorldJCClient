namespace HelloWorldJCClient;

public static class UI
{
	public static void PrintWelcome()
	{
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
	}
	
	public static void PrintSmartCardReaders()
	{
		Console.WriteLine("Currently connected readers");
		foreach (var readerName in Service.GetCardReaders())
		{
			Console.WriteLine(readerName);
		}
	}

	public static void PrintEmptyLines(int lineCount)
	{
		for (var i = 0; i < lineCount; i++)
		{
			Console.WriteLine();
		}
	}

	public static void DoWork()
	{
		Service.StartCardMonitor(PrintGetResponse);
	}
	
	public static void PrintGetResponse()
	{
		Console.WriteLine(Service.GetResponse());
	}

	public static void WaitPressAnyKeyAndExit()
	{
		Console.ReadKey();
		Service.StopCardMonitor();
		Console.WriteLine("Exited");
	}
}