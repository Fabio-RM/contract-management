namespace Api.Application.Dtos;

public record ClientDto(Guid Id, string Name, string Cnpj, bool IsActive);