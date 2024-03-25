namespace APBD3.Exceptions;

public class OverfillException: System.Exception
{
    public OverfillException()
    {
    }

    public OverfillException(string message) : base(message)
    {
    }

    public OverfillException(string message, Exception innerException) : base(message, innerException)
    {
    }
    
}