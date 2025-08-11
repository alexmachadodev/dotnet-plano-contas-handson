namespace PlanoContaHandsOn.Application.PlanosContas.Queries.ListarPlanoConta;

public record PlanoContaResponse(Guid Id, string Codigo, string Nome, bool AceitaLancamento, string Tipo);