using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using PhotoGallery.Api.Host.Data;
using PhotoGallery.Api.Host.Services;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PhotoGallery.Api.UnitTests.Services
{
    public class BaseDataServiceTest
    {
        private readonly Mock<ILogger<BaseDataService<ApplicationDbContext>>> _logger;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly TestDataService _dataService;
        private readonly Mock<IDbContextTransaction> _dbContextTransactionMock;

        public BaseDataServiceTest()
        {
            _dbContextTransactionMock = new Mock<IDbContextTransaction>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            _dataService = new TestDataService(_dbContextWrapper.Object, _logger.Object);
        }

        [Fact]
        public async Task ExecuteSafeAsync_ExecutesActionAndCommitsTransaction()
        {
            // Arrange
            var executed = false;
            var cancellationToken = CancellationToken.None;

            async Task CheckAction()
            {
                executed = true;
                await Task.CompletedTask;
            }

            _dbContextWrapper
                .Setup(s => s.BeginTransactionAsync(cancellationToken))
                .ReturnsAsync(_dbContextTransactionMock.Object);

            // Act
            await _dataService.ExecuteSafeAsync(CheckAction, cancellationToken);

            // Assert
            Assert.True(executed);
            _dbContextTransactionMock.Verify(t => t.CommitAsync(cancellationToken), Times.Once);
            _dbContextWrapper.Verify(s => s.BeginTransactionAsync(cancellationToken), Times.Once);
            _dbContextTransactionMock.Verify(s => s.RollbackAsync(cancellationToken), Times.Never);
        }

        [Fact]
        public async Task ExecuteSafeAsync_CatchesExceptionAndRollsBackTransaction()
        {
            // Arrange
            var exceptionMessage = "Test exception";
            var cancellationToken = CancellationToken.None;

            async Task CheckAction()
            {
                throw new Exception(exceptionMessage);
            }

            _dbContextWrapper
                .Setup(s => s.BeginTransactionAsync(cancellationToken))
                .ReturnsAsync(_dbContextTransactionMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _dataService.ExecuteSafeAsync(CheckAction, cancellationToken));

            _dbContextTransactionMock.Verify(t => t.RollbackAsync(cancellationToken), Times.Once);
            _dbContextWrapper.Verify(s => s.BeginTransactionAsync(cancellationToken), Times.Once);
            _dbContextTransactionMock.Verify(s => s.CommitAsync(cancellationToken), Times.Never);
        }

        private class TestDataService : BaseDataService<ApplicationDbContext>
        {
            public TestDataService(
                IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
                ILogger<BaseDataService<ApplicationDbContext>> logger)
                : base(dbContextWrapper, logger)
            {
            }

            public new Task ExecuteSafeAsync(Func<Task> action, CancellationToken cancellationToken = default)
            {
                return base.ExecuteSafeAsync(action, cancellationToken);
            }

            public new Task<TResult> ExecuteSafeAsync<TResult>(Func<Task<TResult>> action, CancellationToken cancellationToken = default)
            {
                return base.ExecuteSafeAsync(action, cancellationToken);
            }
        }
    }
}