using Order.Application.Interfaces.Base;

namespace Order.Application.Interfaces
{
    public interface IUnitOfWork
    {

        void Register(IRepository repository);

        Task CommitTransaction();
    }
}
