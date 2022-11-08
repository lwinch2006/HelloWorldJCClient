namespace BusinessLogic.Models;

public class OperationException : Exception
{
	public OperationException()
		: base()
	{
		
	}

	public OperationException(string message)
		: base(message)
	{
		
	}

	public OperationException(string message, Exception exception)
		: base(message, exception)
	{
		
	}
}