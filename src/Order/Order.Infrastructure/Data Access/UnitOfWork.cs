using Microsoft.Extensions.Logging;
using Order.Application.Interfaces;
using Order.Application.Interfaces.Base;

namespace Order.Infrastructure.Data_Access
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ILogger<UnitOfWork> _logger;
        private Dictionary<string, IRepository> _registeredRepositories;

        public UnitOfWork(ILogger<UnitOfWork> logger)
        {
            _logger = logger;

            _registeredRepositories = new();
        }

        public void Register(IRepository repository)
        {
            _registeredRepositories.Add(repository.GetType().Name, repository);
        }
        public async Task CommitTransaction()
        {
            var reposSaveChangesTasks = _registeredRepositories
                .Select(rep => rep.Value.Submit())
                .ToList();

            await Task.WhenAll(reposSaveChangesTasks);
        }
    }
}

