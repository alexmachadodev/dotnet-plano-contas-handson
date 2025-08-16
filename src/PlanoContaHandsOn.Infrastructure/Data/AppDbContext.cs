namespace PlanoContaHandsOn.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IUnitOfWork
{
    private const int NumeroViolacaoChaveUnica = 2627;
    private const int NumeroViolacaoIndiceUnico = 2601;

    public DbSet<PlanoConta> PlanosContas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public async Task<int> SalvarAlteracoes(CancellationToken cancellationToken = default)
    {
        try
        {
            return await SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            throw new ConflictException();
        }
        catch (DbUpdateException ex)
            when (ex.InnerException is SqlException { Number: NumeroViolacaoChaveUnica or NumeroViolacaoIndiceUnico })
        {
            throw new UniqueConstraintException();
        }
    }
}