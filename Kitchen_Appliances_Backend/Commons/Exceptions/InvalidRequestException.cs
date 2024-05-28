
namespace Kitchen_Appliances_Backend.Commons.Exceptions;

public class InvalidRequestException : Exception
{
    public InvalidRequestException()
    { }
    public InvalidRequestException(string message) : base(message) { }
    public InvalidRequestException(string message, Exception ex) : base(message, ex) { }
}
