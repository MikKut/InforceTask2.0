using PhotoGallery.Api.Host.Data;
using System.Data.Entity;

namespace PhotoGallery.Api.Host.Services;

public abstract class BaseDataService<T>
    where T : ApplicationDbContext
{
    private readonly IDbContextWrapper<T> _dbContextWrapper;
    private readonly ILogger<BaseDataService<T>> _logger;

    protected BaseDataService(
        IDbContextWrapper<T> dbContextWrapper,
        ILogger<BaseDataService<T>> logger)
    {
        _dbContextWrapper = dbContextWrapper;
        _logger = logger;
    }

    protected Task ExecuteSafeAsync(Func<Task> action, CancellationToken cancellationToken = default)
    {
        return ExecuteSafeAsync(token => action(), cancellationToken);
    }

    protected Task<TResult> ExecuteSafeAsync<TResult>(Func<Task<TResult>> action, CancellationToken cancellationToken = default)
    {
        return ExecuteSafeAsync(token => action(), cancellationToken);
    }

    private async Task ExecuteSafeAsync(Func<CancellationToken, Task> action, CancellationToken cancellationToken = default)
    {
        await using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = await _dbContextWrapper.BeginTransactionAsync(cancellationToken);

        try
        {
            await action(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogError(ex, $"transaction is rollbacked");
            throw;
        }
    }

    private async Task<TResult> ExecuteSafeAsync<TResult>(Func<CancellationToken, Task<TResult>> action, CancellationToken cancellationToken = default)
    {
        await using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = await _dbContextWrapper.BeginTransactionAsync(cancellationToken);

        try
        {
            TResult? result = await action(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return result;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogError(ex, $"transaction is rollbacked");
            throw;
        }
    }
}