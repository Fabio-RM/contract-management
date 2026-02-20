namespace Api.Application.Exceptions;

public class ClientNotFoundException : Exception
{
    public ClientNotFoundException() : base("Client not found")
    {
    }
}