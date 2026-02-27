namespace Application.Common.Interfaces;

public interface IUnitOfWork
{
    public Task<int> CommitAsync(CancellationToken cancellationToken);
}