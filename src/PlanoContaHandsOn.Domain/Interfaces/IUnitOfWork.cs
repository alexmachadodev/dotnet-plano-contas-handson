namespace PlanoContaHandsOn.Domain.Interfaces;

public interface IUnitOfWork
{
    Task<int> SalvarAlteracoes(CancellationToken cancellationToken = default);
}