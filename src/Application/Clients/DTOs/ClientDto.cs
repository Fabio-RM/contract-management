namespace Application.Clients.DTOs;

public record ClientDto(Guid Id, string Cnpj, string Name, string Status);