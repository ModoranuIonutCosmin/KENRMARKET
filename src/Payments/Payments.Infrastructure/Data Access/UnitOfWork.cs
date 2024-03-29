﻿using Microsoft.Extensions.Logging;
using Payments.Application.Interfaces;
using Payments.Application.Interfaces.Base;

namespace Payments.Infrastructure.Data_Access;

public class UnitOfWork : IUnitOfWork
{
    private readonly ILogger<UnitOfWork>             _logger;
    private readonly Dictionary<string, IRepository> _registeredRepositories;

    public UnitOfWork(ILogger<UnitOfWork> logger)
    {
        _logger = logger;

        _registeredRepositories = new Dictionary<string, IRepository>();
    }

    public void Register(IRepository repository)
    {
        _registeredRepositories[repository.GetType().Name] = repository;
    }

    public async Task CommitTransaction()
    {
        var reposSaveChangesTasks = _registeredRepositories
                                    .Select(rep => rep.Value.Submit())
                                    .ToList();

        await Task.WhenAll(reposSaveChangesTasks);
    }
}