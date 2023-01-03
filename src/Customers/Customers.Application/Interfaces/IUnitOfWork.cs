using Customers.Application.Interfaces.Base;

namespace Customers.Application.Interfaces;

public interface IUnitOfWork
{
    void Register(IRepository repository);

    Task CommitTransaction();
}