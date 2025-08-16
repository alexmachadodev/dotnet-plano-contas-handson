namespace PlanoContaHandsOn.Domain.Repositories;

public interface IUnitOfWork
{
    Task<int> SalvarAlteracoes(CancellationToken cancellationToken = default);
}