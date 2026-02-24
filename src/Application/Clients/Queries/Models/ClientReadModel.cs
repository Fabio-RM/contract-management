namespace Application.Clients.Queries.Models;

// Used to avoid queries dependency from Domain/Core Aggregate Root
public class ClientReadModel
{
    public Guid Id { get; set; } 
    public string Cnpj { get; set; } = string.Empty; 
    public string Name { get; set; } = string.Empty; 
    public string Status { get; set; } = string.Empty;
}