using Microsoft.EntityFrameworkCore.Storage;

namespace Api.Core.Repositories;

public interface IUnitOfWork : IAsyncDisposable
{
  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
  Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}
