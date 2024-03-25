namespace APBD3.Exceptions;

public class HazardException: System.Exception
{
    public HazardException()
    {
    }

    public HazardException(string message) : base(message)
    {
    }

    public HazardException(string message, Exception innerException) : base(message, innerException)
    {
    }
}