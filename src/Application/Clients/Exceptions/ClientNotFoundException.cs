namespace Application.Clients.Exceptions;

public class ClientNotFoundException : Exception
{
    public ClientNotFoundException() : base("Client not found")
    {
    }
}