using MediatR;

namespace Application.Common.Interfaces;

public interface ICommand<TResponse> : IRequest<TResponse>
{
}