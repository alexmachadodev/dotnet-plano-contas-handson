namespace PlanoContaHandsOn.Application.PlanosContas.Commands.ExcluirPlanoConta;

public class ExcluirPlanoContaCommandHandler(IPlanoContaRepository repository) : IRequestHandler<ExcluirPlanoContaCommand>
{
    public async Task Handle(ExcluirPlanoContaCommand request, CancellationToken cancellationToken)
    {
        var planoContaExistente = await repository.ObterPorId(request.Id, cancellationToken);

        if (planoContaExistente is null)
            throw new NotFoundException("A conta que você está tentando excluir não foi encontrada.");

        var possuiFilhos = await repository.PossuiFilhos(request.Id, cancellationToken);

        if (possuiFilhos)
            throw new BadRequestException("Não é possível excluir uma conta que possui contas filhas. Exclua as contas filhas primeiro.");
        
        repository.Remover(planoContaExistente);

        await repository.UnitOfWork.SalvarAlteracoes(cancellationToken);
    }
}