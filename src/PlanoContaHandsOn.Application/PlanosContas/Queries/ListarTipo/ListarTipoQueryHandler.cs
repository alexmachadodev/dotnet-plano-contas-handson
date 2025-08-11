namespace PlanoContaHandsOn.Application.PlanosContas.Queries.ListarTipo;

public class ListarTipoQueryHandler : IRequestHandler<ListarTipoQuery, IReadOnlyCollection<TipoResponse>>
{
    public Task<IReadOnlyCollection<TipoResponse>> Handle(ListarTipoQuery request, CancellationToken cancellationToken)
    {
        var tipos = Enum.GetValues<Tipo>()
            .Select(t => new TipoResponse((int)t, t.ToString()))
            .ToList();

        return Task.FromResult<IReadOnlyCollection<TipoResponse>>(tipos);
    }
}