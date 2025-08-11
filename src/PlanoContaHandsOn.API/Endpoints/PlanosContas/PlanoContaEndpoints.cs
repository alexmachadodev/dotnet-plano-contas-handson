namespace PlanoContaHandsOn.API.Endpoints.PlanosContas;

public static class PlanoContaEndpoints
{
    public static void MapPlanoContaEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/planos-contas")
            .WithTags("Plano de Contas")
            .WithOpenApi();

        group.MapPost("/", async (CriarPlanoContaCommand command, ISender sender) =>
            {
                var novaContaId = await sender.Send(command);

                return Results.Created($"/api/planos-contas/{novaContaId}", new { id = novaContaId });
            })
            .WithName("CriarPlanoConta")
            .WithSummary("Cria um novo plano de contas.")
            .WithDescription("Este endpoint cria um novo plano de conta, validando as regras de negócio como hierarquia, tipo e unicidade de código.")
            .Produces<object>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);

        group.MapGet("/proximo-codigo", async (Guid? idPai, ISender sender) =>
            {
                var query = new ObterProximoCodigoQuery(idPai);

                var resultado = await sender.Send(query);

                return Results.Ok(resultado);
            })
            .WithName("ObterProximoCodigo")
            .WithSummary("Sugere o próximo código de plano de conta disponível")
            .WithDescription("Com base em um `idPai` opcional, sugere o próximo código sequencial vago. Se o `idPai` não for fornecido, sugere um código de nível raiz.")
            .Produces<ProximoCodigoResponse>()
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);


        group.MapGet("/", async ([AsParameters] ListarPlanoContaQuery query, ISender sender) =>
            {
                var resultado = await sender.Send(query);

                return Results.Ok(resultado);
            })
            .WithName("ListarPlanosContas")
            .WithSummary("Lista os planos de contas de forma paginada.")
            .WithDescription("Retorna uma lista paginada, permitindo a filtragem pelo início do nome.")
            .Produces<ResultadoPaginado<PlanoContaResponse>>()
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);


        group.MapDelete("/{id:guid}", async (Guid id, ISender sender) =>
            {
                var comando = new ExcluirPlanoContaCommand(id);

                await sender.Send(comando);

                return Results.NoContent();
            })
            .WithName("ExcluirPlanoConta")
            .WithSummary("Exclui um plano de conta existente")
            .WithDescription("Exclui um plano de conta desde que ela não possua contas filhas. Retorna 204 em caso de sucesso.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound);

        group.MapGet("/tipos", async (ISender sender) =>
            {
                var resultado = await sender.Send(new ListarTipoQuery());

                return Results.Ok(resultado);
            })
            .WithName("ListarTiposDeConta")
            .WithSummary("Retorna a lista de tipos de plano de conta disponíveis (Receita, Despesa, etc.).")
            .Produces<IReadOnlyCollection<TipoResponse>>();
    }
}