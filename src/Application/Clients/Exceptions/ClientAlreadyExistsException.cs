namespace Application.Clients.Exceptions;

public class ClientAlreadyExistsException : Exception
{
    public ClientAlreadyExistsException() : base("Client already exists")
    {
    }
}