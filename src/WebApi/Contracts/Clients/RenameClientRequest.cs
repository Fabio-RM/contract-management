using System.ComponentModel.DataAnnotations;

namespace WebApi.Contracts.Clients;

public sealed record RenameClientRequest
{
    [Required]
    public string Name { get; init; }
}