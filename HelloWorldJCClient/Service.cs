using PCSC.Monitoring;

namespace HelloWorldJCClient;

public static class Service
{
	private static ISCardMonitor? _monitor;	
	
	public static IEnumerable<string> GetCardReaders()
	{
		using var cardContext = CardUtils.GetCardContext();
		
		return cardContext.GetReaders();
	}

	public static bool SelectApplet()
	{
		var (smCardContext, isoReader) = CardUtils.GetIsoReaderInstance();
		
		using (smCardContext)
		using (isoReader)
		{
			var apdu = new CommandApdu(IsoCase.Case3Short, isoReader.ActiveProtocol)
			{
				CLA = 0x00,
				INS = 0xA4,
				P1 = 0x04,
				P2 = 0x00,
				Data = new byte[] { 0xD2, 0x76, 0x00, 0x01, 0x24, 0x10, 0x00, 0x01 }
			};
	
			var response = isoReader.Transmit(apdu);
			
			return CardUtils.IsCardResponseOk(response);
		}
	}
	
	public static string GetResponse()
	{
		var (smCardContext, isoReader) = CardUtils.GetIsoReaderInstance();
		
		using (smCardContext)
		using (isoReader)
		{
			var apdu = new CommandApdu(IsoCase.Case2Short, isoReader.ActiveProtocol)
			{
				CLA = 0x80,
				INS = 0x01,
				P1 = 0x00,
				P2 = 0x00,
				Le = 0x0D,
			};

			if (!SelectApplet())
			{
				return "Select applet failure";
			}
			
			var response = isoReader.Transmit(apdu);

			if (!CardUtils.IsCardResponseOk(response))
			{
				return $"Invalid response 0x{Convert.ToHexString(new[] { response.SW1, response.SW2 })}";
			}
		
			var data = response.GetData();
			return Encoding.ASCII.GetString(data);
		}
	}

	public static void StartCardMonitor(Action clientFunction)
	{
		StopCardMonitor();

		var monitorFactory = MonitorFactory.Instance;
		_monitor = monitorFactory.Create(SCardScope.System);
		_monitor.StatusChanged += (sender, args) =>
		{
			if (args.NewState == SCRState.Present)
			{
				clientFunction();
			}
		};

		_monitor.Start(GetCardReaders().First());
	}

	public static void StopCardMonitor()
	{
		if (_monitor == null)
		{
			return;
		}

		_monitor.Cancel();
		_monitor.Dispose();
	}
}