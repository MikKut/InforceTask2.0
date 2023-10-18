using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace PhotoGallery.Api.Host.Data
{
    public interface IDbContextWrapper<TContext>
        where TContext : DbContext
    {
        TContext DbContext { get; }

        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
    }
}
