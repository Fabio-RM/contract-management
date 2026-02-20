namespace Api.Core.Exceptions;

public class ClientInactiveException : InvalidOperationException
{
    public ClientInactiveException() : base("Client is not active")
    {
    }
}