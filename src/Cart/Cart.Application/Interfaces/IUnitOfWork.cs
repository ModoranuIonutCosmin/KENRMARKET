using Cart.Application.Interfaces.Base;

namespace Cart.Application.Interfaces;

public interface IUnitOfWork
{
    void Register(IRepository repository);

    Task CommitTransaction();
}