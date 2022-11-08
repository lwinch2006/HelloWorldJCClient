using JCClientCore.Models;
using PCSC;
using PCSC.Exceptions;
using PCSC.Iso7816;
using PCSC.Monitoring;
using Exception = System.Exception;

namespace JCClientCore;

public class JCClient : IJCClient
{
	private ISCardMonitor? _monitor;

	public bool IsResponseSuccess(Response cardResponse)
	{
		return cardResponse.StatusWord == 0x9000;
	}

	public bool IsGetResponseAnswer(Response cardResponse)
	{
		return cardResponse.SW1 == 0x61 && cardResponse.SW2 > 0x0;
	}

	public void StartCardMonitor(Action cardInsertedEventHandler, Action cardRemovedEventHandler)
	{
		var readerName = GetCardContext().GetReaders()?.FirstOrDefault();

		if (readerName == null)
		{
			throw new CardOperationException("Card reader not found");
		}

		StopCardMonitor();

		_monitor = MonitorFactory.Instance.Create(SCardScope.System);
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

	public void SelectApplet(string aid, string delimiter = ":")
	{
		using var cardContext = GetCardContext();
		using var cardReader = GetReader(cardContext);

		var aidBytes = Convert.FromHexString(aid.Replace(delimiter, string.Empty));
		var apdu = ApduFactory.ToSelectAppletApdu(aidBytes, cardReader.ActiveProtocol);

		var response = cardReader.Transmit(apdu);
		if (!IsResponseSuccess(response))
		{
			throw new CardOperationException(
				$"Invalid response 0x{Convert.ToHexString(new[] { response.SW1, response.SW2 })}");
		}
	}

	public bool TrySelectApplet(string aid, string delimiter = ":")
	{
		try
		{
			SelectApplet(aid, delimiter);
			return true;
		}
		catch (PCSCException)
		{
			// Logging goes here...
			return false;
		}
	}

	public byte[] GetCardData(ApduParameters apduParameters)
	{
		using var cardContext = GetCardContext();
		using var cardReader = GetReader(cardContext);

		var apdu = ApduFactory.From(apduParameters, cardReader.ActiveProtocol);
		
		var response = cardReader.Transmit(apdu);
		if (!IsResponseSuccess(response) && !IsGetResponseAnswer(response))
		{
			throw new CardOperationException(
				$"Invalid response 0x{Convert.ToHexString(new[] { response.SW1, response.SW2 })}");
		}
	
		var data = response.GetData() ?? Array.Empty<byte>();
		return data;
	}

	public byte[] TryGetCardData(ApduParameters apduParameters)
	{
		var responseBytes = Array.Empty<byte>();
		
		try
		{
			responseBytes = GetCardData(apduParameters);
		}
		catch (PCSCException)
		{
			// Logging goes here...
		}

		return responseBytes;
	}

	private ISCardContext GetCardContext()
	{
		var contextFactory = ContextFactory.Instance;
		var cardContext = contextFactory.Establish(SCardScope.System);
		return cardContext;
	}
	
	private IsoReader GetReader(ISCardContext cardContext)
	{
		var readerNames = cardContext.GetReaders();
		var isoReader = new IsoReader(cardContext, readerNames[0], SCardShareMode.Shared, SCardProtocol.Any, false);
		return isoReader;
	}
}