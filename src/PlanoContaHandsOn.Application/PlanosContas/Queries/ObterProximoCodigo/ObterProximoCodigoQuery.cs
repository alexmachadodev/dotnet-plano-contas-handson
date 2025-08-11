namespace PlanoContaHandsOn.Application.PlanosContas.Queries.ObterProximoCodigo;

public record ObterProximoCodigoQuery(Guid? IdPai) : IRequest<ProximoCodigoResponse>;