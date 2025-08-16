namespace PlanoContaHandsOn.Application.PlanosContas.Commands.CriarPlanoConta;

public class CriarPlanoContaCommandHandler(IPlanoContaRepository repository) : IRequestHandler<CriarPlanoContaCommand, Guid>
{
    public async Task<Guid> Handle(CriarPlanoContaCommand request, CancellationToken cancellationToken)
    {
        var codigoEnviado = Codigo.Criar(request.Codigo);

        PlanoConta? planoContaPai = null;

        if (request.IdPai.HasValue)
        {
            planoContaPai = await repository.ObterPorId(request.IdPai.Value, cancellationToken);

            if (planoContaPai is null)
                throw new NotFoundException("A conta pai especificada não existe.");

            if (planoContaPai.AceitaLancamento)
                throw new BadRequestException("Plano de contas que aceita lançamento não pode ter contas filhas.");
        }

        codigoEnviado.ValidarHierarquia(planoContaPai?.Codigo);

        var codigosFilhos = await repository.ObterCodigosFilhos(request.IdPai, cancellationToken);

        if (codigosFilhos.Contains(codigoEnviado))
            throw new BadRequestException($"O código '{codigoEnviado.Value}' já está em uso.");

        var novoPlanoConta = new PlanoConta(request.Nome, codigoEnviado,
            request.Tipo, request.AceitaLancamento, request.IdPai, planoContaPai?.Tipo);
        
        await repository.Adicionar(novoPlanoConta, cancellationToken);

        await repository.UnitOfWork.SalvarAlteracoes(cancellationToken);

        return novoPlanoConta.Id;
    }
}