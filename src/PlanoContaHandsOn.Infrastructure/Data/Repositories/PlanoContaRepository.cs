namespace PlanoContaHandsOn.Infrastructure.Data.Repositories;

public class PlanoContaRepository(AppDbContext context) : IPlanoContaRepository
{
    public IUnitOfWork UnitOfWork => context;

    public async Task<bool> PossuiFilhos(Guid planoContaPaiId, CancellationToken cancellationToken = default)
        => await context.PlanosContas.AnyAsync(c => c.IdPai == planoContaPaiId, cancellationToken);

    public async Task<PlanoConta?> ObterPorId(Guid id, CancellationToken cancellationToken)
        => await context.PlanosContas.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyCollection<Codigo>> ObterCodigosFilhos(Guid? idPai, CancellationToken cancellationToken)
        => await context.PlanosContas
            .AsNoTracking()
            .Where(c => c.IdPai == idPai)
            .Select(c => c.Codigo)
            .ToListAsync(cancellationToken);

    //public async Task<PlanoConta?> ObterPaiPorFilho(Guid filhoId, CancellationToken cancellationToken)
    //{
    //    var filho = await context.PlanosContas
    //        .AsNoTracking()
    //        .FirstOrDefaultAsync(c => c.Id == filhoId, cancellationToken);
        
    //    if (filho?.IdPai == null)
    //    {
    //        return null;
    //    }
        
    //    return await context.PlanosContas.AsNoTracking()
    //        .FirstOrDefaultAsync(c => c.Id == filho.IdPai, cancellationToken);
    //}

    public async Task Adicionar(PlanoConta planoConta, CancellationToken cancellationToken)
        => await context.PlanosContas.AddAsync(planoConta, cancellationToken);

    public void Remover(PlanoConta planoConta) => context.Remove(planoConta);

    public async Task<(IReadOnlyCollection<PlanoConta> Itens, long TotalRegistros)> ListarPaginado(int pagina, int tamanhoPagina,
        string? filtroNome, CancellationToken cancellationToken)
    {
        var query = context.PlanosContas.AsNoTracking();

        if (string.IsNullOrWhiteSpace(filtroNome) is false)
        {
            query = query.Where(c => c.Nome.StartsWith(filtroNome));
        }

        var totalRegistros = await query.LongCountAsync(cancellationToken);

        var itens = await query
            .OrderBy(c => c.Codigo)
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToListAsync(cancellationToken);

        return (itens, totalRegistros);
    }

    public async Task<IReadOnlyCollection<PlanoConta>> ListarPorAceitaLancamento(bool aceitaLancamento, CancellationToken cancellationToken)
        => await context.PlanosContas
            .AsNoTracking()
            .Where(c => c.AceitaLancamento == aceitaLancamento)
            .OrderBy(c => c.Codigo)
            .ToListAsync(cancellationToken);

    public void Dispose() => context.Dispose();

    
}