namespace Api.Core.Exceptions;

public class ClientActiveException : InvalidOperationException
{
    public ClientActiveException() : base("Client is already active")
    {
    }
}