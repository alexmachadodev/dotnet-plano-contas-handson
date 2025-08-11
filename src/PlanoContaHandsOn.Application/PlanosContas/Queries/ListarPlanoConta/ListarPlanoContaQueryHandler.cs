namespace PlanoContaHandsOn.Application.PlanosContas.Queries.ListarPlanoConta;

public class ListarPlanoContaQueryHandler(IPlanoContaRepository repository) : IRequestHandler<ListarPlanoContaQuery, ResultadoPaginado<PlanoContaResponse>>
{
    public async Task<ResultadoPaginado<PlanoContaResponse>> Handle(ListarPlanoContaQuery request, CancellationToken cancellationToken)
    {
        var (itens, totalRegistros) =
            await repository.ListarPaginado(request.Pagina, request.TamanhoPagina, request.Filtro, cancellationToken);

        var itensDto = itens.Select(conta => new PlanoContaResponse(conta.Id, conta.Codigo.Value, conta.Nome,
            conta.AceitaLancamento, conta.Tipo.ToString())).ToList();

        return new ResultadoPaginado<PlanoContaResponse>(request.Pagina, request.TamanhoPagina, totalRegistros, itensDto);
    }
}