namespace Application.Clients.Queries.Models;

// Used to avoid queries dependency from Domain/Core Aggregate Root
public class ClientReadModel
{
    public Guid Id { get; init; } 
    public string Cnpj { get; init; } = string.Empty; 
    public string Name { get; init; } = string.Empty; 
    public string Status { get; init; } = string.Empty;
}