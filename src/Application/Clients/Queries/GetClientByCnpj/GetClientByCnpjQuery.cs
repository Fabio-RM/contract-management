using Application.Clients.DTOs;
using MediatR;

namespace Application.Clients.Queries.GetClientByCnpj;

public record GetClientByCnpjQuery(string cnpj): IRequest<ClientDto>
{
    public string ClientCnpj { get; } = cnpj;
}