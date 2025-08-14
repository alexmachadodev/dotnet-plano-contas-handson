namespace PlanoContaHandsOn.Domain.Interfaces;

public interface IRepository<T> : IDisposable
{
    IUnitOfWork UnitOfWork { get; }
}