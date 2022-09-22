using HelloWorldJCClient.Models;
using HelloWorldJCClient.Utils;

namespace HelloWorldJCClient.Services;

public class InstructionService
{
	private readonly CardService _cardService;

	public InstructionService()
	{
		_cardService = new CardService();
	}

	public string GetFirstname()
	{
		var apduParameters = new ApduParameters
		{
			IsoCase = 1,
			CLA = 0x80,
			INS = 0x02,
			P1 = 0x00,
			P2 = 0x00,
			Le = 0x19
		};
		
		var firstNameAsBytes =  GetCardData(apduParameters).TrimEnd();
		var firstname = Encoding.ASCII.GetString(firstNameAsBytes);
		return firstname;
	}

	public string GetLastname()
	{
		var apduParameters = new ApduParameters
		{
			IsoCase = 1,
			CLA = 0x80,
			INS = 0x03,
			P1 = 0x00,
			P2 = 0x00,
			Le = 0x19
		};
		
		var response = Encoding.ASCII.GetString(GetCardData(apduParameters).TrimEnd());
		return response;
	}

	public string GetEmail()
	{
		var apduParameters = new ApduParameters
		{
			IsoCase = 1,
			CLA = 0x80,
			INS = 0x04,
			P1 = 0x00,
			P2 = 0x00,
			Le = 0x32
		};
		
		var response = Encoding.ASCII.GetString(GetCardData(apduParameters).TrimEnd());
		return response;
	}
	
	public string GetPhone()
	{
		var apduParameters = new ApduParameters
		{
			IsoCase = 1,
			CLA = 0x80,
			INS = 0x05,
			P1 = 0x00,
			P2 = 0x00,
			Le = 0x19
		};
		
		var response = Encoding.ASCII.GetString(GetCardData(apduParameters).TrimEnd());
		return response;
	}

	public string GetResponse()
	{
		var apduParameters = new ApduParameters
		{
			IsoCase = 1,
			CLA = 0x80,
			INS = 0x01,
			P1 = 0x00,
			P2 = 0x00,
			Le = 0x0D
		};
		
		var response = Encoding.ASCII.GetString(GetCardData(apduParameters).TrimEnd());
		return response;
	}

	private static byte[] GetCardData(ApduParameters apduParameters)
	{
		var (smCardContext, isoReader) = CardUtils.GetReader();
		
		using (smCardContext)
		using (isoReader)
		{
			var apdu = new CommandApdu((IsoCase)apduParameters.IsoCase, isoReader.ActiveProtocol)
			{
				CLA = apduParameters.CLA,
				INS = apduParameters.INS,
				P1 = apduParameters.P1,
				P2 = apduParameters.P2,
				Le = apduParameters.Le
			};
			
			var response = isoReader.Transmit(apdu);

			if (!CardUtils.IsResponseSuccess(response))
			{
				throw new OperationException(
					$"Invalid response 0x{Convert.ToHexString(new[] { response.SW1, response.SW2 })}");
			}
		
			var data = response.GetData();
			return data;
		}
	}
}