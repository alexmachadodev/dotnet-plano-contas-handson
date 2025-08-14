namespace PlanoContaHandsOn.Domain.Interfaces;

public interface IPlanoContaRepository : IRepository<PlanoConta>
{
    Task Adicionar(PlanoConta planoConta, CancellationToken cancellationToken = default);
    void Remover(PlanoConta planoConta);
    Task<bool> PossuiFilhos(Guid planoContaPaiId, CancellationToken cancellationToken = default);
    Task<PlanoConta?> ObterPorId(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Codigo>> ObterCodigosFilhos(Guid? idPai, CancellationToken cancellationToken = default);
    Task<(IReadOnlyCollection<PlanoConta> Itens, long TotalRegistros)> ListarPaginado(int pagina, int tamanhoPagina,
        string? filtroNome, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<PlanoConta>> ListarPorAceitaLancamento(bool aceitaLancamento, CancellationToken cancellationToken = default);
}