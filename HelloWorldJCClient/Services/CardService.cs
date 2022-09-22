using HelloWorldJCClient.Models;
using HelloWorldJCClient.Utils;

namespace HelloWorldJCClient.Services;

public class CardService
{
	private ISCardMonitor? _monitor;

	public static string? GetFirstReaderName()
	{
		using var cardContext = CardUtils.GetCardContext();
		return cardContext.GetReaders()?.FirstOrDefault();
	}

	public void StartCardMonitor(Action cardInsertedEventHandler, Action cardRemovedEventHandler)
	{
		var readerName = GetFirstReaderName();

		if (readerName == null)
		{
			throw new OperationException("Card reader not found");
		}

		StopCardMonitor();

		var monitorFactory = MonitorFactory.Instance;
		_monitor = monitorFactory.Create(SCardScope.System);
		_monitor.StatusChanged += (sender, args) =>
		{
			switch (args.NewState)
			{
				case SCRState.Empty:
					cardRemovedEventHandler();
					break;
				
				case SCRState.Present:
					cardInsertedEventHandler();
					break;
			}
		};

		_monitor.Start(readerName);
	}

	public void StopCardMonitor()
	{
		if (_monitor == null)
		{
			return;
		}

		_monitor.Cancel();
		_monitor.Dispose();
	}

	public void SelectApplet(string aid, string delimiter)
	{
		var (smCardContext, isoReader) = CardUtils.GetReader();

		var aidBytes = Convert.FromHexString(aid.Replace(delimiter, string.Empty));

		using (smCardContext)
		using (isoReader)
		{
			var apdu = new CommandApdu(IsoCase.Case3Short, isoReader.ActiveProtocol)
			{
				CLA = 0x00,
				INS = 0xA4,
				P1 = 0x04,
				P2 = 0x00,
				Data = aidBytes
			};

			var response = isoReader.Transmit(apdu);

			if (!CardUtils.IsResponseSuccess(response))
			{
				throw new OperationException(
					$"Invalid response 0x{Convert.ToHexString(new[] { response.SW1, response.SW2 })}");
			}
		}
	}
}