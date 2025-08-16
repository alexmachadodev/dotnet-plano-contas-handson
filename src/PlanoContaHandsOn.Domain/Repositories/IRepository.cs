namespace PlanoContaHandsOn.Domain.Repositories;

public interface IRepository<T> : IDisposable
{
    IUnitOfWork UnitOfWork { get; }
}