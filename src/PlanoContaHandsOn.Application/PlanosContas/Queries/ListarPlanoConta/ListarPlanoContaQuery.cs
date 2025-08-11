namespace PlanoContaHandsOn.Application.PlanosContas.Queries.ListarPlanoConta;

public record ListarPlanoContaQuery(int Pagina = 1, int TamanhoPagina = 25, string? Filtro = null) : IRequest<ResultadoPaginado<PlanoContaResponse>>;