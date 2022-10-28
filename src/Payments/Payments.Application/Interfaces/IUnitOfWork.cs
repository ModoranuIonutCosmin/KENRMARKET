using Payments.Application.Interfaces.Base;

namespace Payments.Application.Interfaces;

public interface IUnitOfWork
{
    void Register(IRepository repository);

    Task CommitTransaction();
}