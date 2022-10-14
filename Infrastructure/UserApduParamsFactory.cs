using JCClientCore.Models;

namespace Infrastructure;

public static class UserApduParamsFactory
{
	public static ApduParameters GetFirstName()
	{
		return new ApduParameters
		{
			IsoCase = 1,
			CLA = 0x80,
			INS = 0x02,
			P1 = 0x00,
			P2 = 0x00,
			Le = 0x19
		};
	}

	public static ApduParameters GetLastName()
	{
		return new ApduParameters
		{
			IsoCase = 1,
			CLA = 0x80,
			INS = 0x03,
			P1 = 0x00,
			P2 = 0x00,
			Le = 0x19
		};
	}

	public static ApduParameters GetEmail()
	{
		return new ApduParameters
		{
			IsoCase = 1,
			CLA = 0x80,
			INS = 0x04,
			P1 = 0x00,
			P2 = 0x00,
			Le = 0x32
		};
	}

	public static ApduParameters GetPhone()
	{
		return new ApduParameters
		{
			IsoCase = 1,
			CLA = 0x80,
			INS = 0x05,
			P1 = 0x00,
			P2 = 0x00,
			Le = 0x19
		};
	}

	public static ApduParameters GetPhoto()
	{
		return new ApduParameters
		{
			IsoCase = 1,
			CLA = 0x80,
			INS = 0x06,
			P1 = 0x00,
			P2 = 0x00,
			Le = 0x00
		};
	}
}