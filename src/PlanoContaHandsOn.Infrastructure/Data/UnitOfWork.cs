namespace PlanoContaHandsOn.Infrastructure.Data;

public class UnitOfWork(AppDbContext context) : IUnitOfWork, IDisposable
{
    private const int NumeroViolacaoChaveUnica = 2627;
    private const int NumeroViolacaoIndiceUnico = 2601;

    private bool _disposed;

    public async Task<int> SalvarAlteracoes(CancellationToken cancellationToken)
    {
        try
        {
            return await context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
            when (ex.InnerException is SqlException { Number: NumeroViolacaoChaveUnica or NumeroViolacaoIndiceUnico })
        {
            throw new InternalServerException("A operação não pôde ser concluída pois os dados foram modificados por outro usuário.");
        }
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed is false && disposing)
            context.Dispose();

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}