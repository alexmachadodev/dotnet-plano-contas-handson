namespace PlanoContaHandsOn.Application.PlanosContas.Queries.ListarPlanoContaPai;

public class ListarPlanoContaPaiQueryHandler(IPlanoContaRepository repository) : IRequestHandler<ListarPlanoContaPaiQuery, IReadOnlyCollection<PlanoContaPaiResponse>>
{
    public async Task<IReadOnlyCollection<PlanoContaPaiResponse>> Handle(ListarPlanoContaPaiQuery request, CancellationToken cancellationToken)
    {
        var planosContas = await repository.ListarPorAceitaLancamento(false, cancellationToken);

        return planosContas
            .Select(plano => new PlanoContaPaiResponse(plano.Id, plano.Codigo.Value, plano.Nome))
            .ToList();
    }
}