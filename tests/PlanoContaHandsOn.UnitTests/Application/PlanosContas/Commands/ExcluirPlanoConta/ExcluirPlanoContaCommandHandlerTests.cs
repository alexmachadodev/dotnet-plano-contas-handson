namespace PlanoContaHandsOn.UnitTests.Application.PlanosContas.Commands.ExcluirPlanoConta;

public class ExcluirPlanoContaCommandHandlerTests
{
    private readonly IPlanoContaRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ExcluirPlanoContaCommandHandler _handler;

    public ExcluirPlanoContaCommandHandlerTests()
    {
        _repository = Substitute.For<IPlanoContaRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new ExcluirPlanoContaCommandHandler(_repository, _unitOfWork);
    }

    [Fact]
    public async Task Handle_QuandoContaPossuiFilhos_DeveLancarBadRequestException()
    {
        // Arrange
        var idContaParaExcluir = Guid.NewGuid();
        var comando = new ExcluirPlanoContaCommand(idContaParaExcluir);

        var contaExistente = new PlanoConta("Receitas", Codigo.Criar("1"), Tipo.Receita, false, null, null);

        _repository.ObterPorId(idContaParaExcluir, Arg.Any<CancellationToken>())
            .Returns(contaExistente);

        _repository.PossuiFilhos(idContaParaExcluir, Arg.Any<CancellationToken>())
            .Returns(true);

        // Act
        var acao = async () => await _handler.Handle(comando, CancellationToken.None);

        // Assert
        await acao.Should().ThrowAsync<BadRequestException>()
            .WithMessage("Não é possível excluir uma conta que possui contas filhas. Exclua as contas filhas primeiro.");

        _repository.DidNotReceive().Remover(Arg.Any<PlanoConta>());
        await _unitOfWork.DidNotReceive().SalvarAlteracoes(Arg.Any<CancellationToken>());
    }
}