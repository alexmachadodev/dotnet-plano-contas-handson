namespace PlanoContaHandsOn.Application.PlanosContas.Queries.ObterProximoCodigo;

public class ObterProximoCodigoQueryHandler(IPlanoContaRepository repository, IGeradorCodigoPlanoContaService geradorCodigo) 
    : IRequestHandler<ObterProximoCodigoQuery, ProximoCodigoResponse>
{
    public async Task<ProximoCodigoResponse> Handle(ObterProximoCodigoQuery request, CancellationToken cancellationToken)
    {
        //try
        //{
            PlanoConta? planoContaPai = null;

            if (request.IdPai.HasValue)
            {
                planoContaPai = await repository.ObterPorId(request.IdPai.Value, cancellationToken);

                if (planoContaPai is null)
                    throw new NotFoundException("A conta pai especificada para a sugestão de código não existe.");
            }

            var codigosFilhos = await repository.ObterCodigosFilhos(request.IdPai, cancellationToken);

            var proximoCodigo = geradorCodigo.Gerar(planoContaPai?.Codigo, codigosFilhos);

            return new ProximoCodigoResponse(proximoCodigo.Value);
        //}
        //catch (ApplicationException e)
        //{
        //    var avo = await repository.ObterPaiPorFilho(request.IdPai.Value, cancellationToken);
        //    var codigosTios = await repository.ObterCodigosFilhos(avo?.Id, cancellationToken);
        //    var proximoTio = geradorCodigo.Gerar(avo?.Codigo, codigosTios);

        //    return proximoTio.Value;
        //}
    }
}