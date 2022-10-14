namespace JCClientCore.Models;

public class CardOperationException : Exception
{
	public CardOperationException()
	{ }

	public CardOperationException(string message)
		: base(message)
	{ }

	public CardOperationException(string message, Exception exception)
		: base(message, exception)
	{ }
}