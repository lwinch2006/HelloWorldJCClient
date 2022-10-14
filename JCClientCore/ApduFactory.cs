using JCClientCore.Models;
using PCSC;
using PCSC.Iso7816;

namespace JCClientCore;

public static class ApduFactory
{
	public static CommandApdu ToSelectAppletApdu(byte[] aid, SCardProtocol activeProtocol)
	{
		return new CommandApdu(IsoCase.Case3Short, activeProtocol)
		{
			CLA = 0x00,
			INS = 0xA4,
			P1 = 0x04,
			P2 = 0x00,
			Data = aid
		};
	}
	
	public static CommandApdu From(ApduParameters apduParameters, SCardProtocol activeProtocol)
	{
		return new CommandApdu((IsoCase)apduParameters.IsoCase, activeProtocol)
		{
			CLA = apduParameters.CLA,
			INS = apduParameters.INS,
			P1 = apduParameters.P1,
			P2 = apduParameters.P2,
			Le = apduParameters.Le
		};
	}
}