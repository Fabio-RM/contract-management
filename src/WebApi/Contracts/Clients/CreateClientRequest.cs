using System.ComponentModel.DataAnnotations;

namespace WebApi.Contracts.Clients;

public sealed record CreateClientRequest
{
    [Required]
    public string Cnpj { get; init; }
    
    [Required]
    public string Name { get; init; }
}