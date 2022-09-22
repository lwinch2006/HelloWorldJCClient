namespace HelloWorldJCClient;

public static class CardUtils
{
	public static ISCardContext GetCardContext()
	{
		var contextFactory = ContextFactory.Instance;
		var cardContext = contextFactory.Establish(SCardScope.System);
		return cardContext;
	}
	
	public static (ISCardContext, IsoReader) GetIsoReaderInstance()
	{
		var cardContext = GetCardContext();
		var readerNames = cardContext.GetReaders();
		var isoReader = new IsoReader(cardContext, readerNames[0], SCardShareMode.Shared, SCardProtocol.Any, false);
		return (cardContext, isoReader);
	}
	
	public static bool IsCardResponseOk(Response response)
	{
		return response.SW1 == 0x90 && response.SW2 == 0x00;
	}
}