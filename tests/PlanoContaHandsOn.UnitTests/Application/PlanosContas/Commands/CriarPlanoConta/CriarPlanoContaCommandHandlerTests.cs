namespace PlanoContaHandsOn.UnitTests.Application.PlanosContas.Commands.CriarPlanoConta;

public class CriarPlanoContaCommandHandlerTests
{
    private readonly IPlanoContaRepository _planoContaRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly CriarPlanoContaCommandHandler _handler;

    public CriarPlanoContaCommandHandlerTests()
    {
        _planoContaRepository = Substitute.For<IPlanoContaRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new CriarPlanoContaCommandHandler(_planoContaRepository, _unitOfWork);
    }

    [Fact]
    public async Task Handle_ComDadosValidos_DeveCriarContaComCodigoCorreto()
    {
        // Arrange
        var idPai = Guid.NewGuid();
        var comando = new CriarPlanoContaCommand("Serviços de TI", "1.99", true, Tipo.Receita, idPai);

        var contaPai = new PlanoConta("Receitas", Codigo.Criar("1"), Tipo.Receita, false, null, null);

        _planoContaRepository.ObterPorId(idPai, Arg.Any<CancellationToken>())
            .Returns(contaPai);

        var codigosFilhosExistentes = new List<Codigo> { Codigo.Criar("1.1"), Codigo.Criar("1.2") };

        _planoContaRepository.ObterCodigosFilhos(idPai, Arg.Any<CancellationToken>())
            .Returns(codigosFilhosExistentes);

        PlanoConta? contaAdicionada = null;
        await _planoContaRepository.Adicionar(Arg.Do<PlanoConta>(c => contaAdicionada = c), Arg.Any<CancellationToken>());

        // Act
        var novaContaId = await _handler.Handle(comando, CancellationToken.None);

        // Assert
        await _unitOfWork.Received(1).SalvarAlteracoes(Arg.Any<CancellationToken>());
        contaAdicionada.Should().NotBeNull();
        contaAdicionada?.Codigo.Value.Should().Be(comando.Codigo);
    }

    [Fact]
    public async Task Handle_QuandoPaiAceitaLancamento_DeveLancarBadRequestException()
    {
        // Arrange
        var idPai = Guid.NewGuid();
        var comando = new CriarPlanoContaCommand("Filha Ilegal", "1.1.1", true, Tipo.Receita, idPai);

        var contaPai = new PlanoConta(
            "Vendas à Vista",
            Codigo.Criar("1.1"),
            Tipo.Receita,
            aceitaLancamento: true,
            null,
            null);

        _planoContaRepository.ObterPorId(idPai, Arg.Any<CancellationToken>())
            .Returns(contaPai);

        // Act
        Func<Task> acao = async () => await _handler.Handle(comando, CancellationToken.None);

        // Assert
        
        await acao.Should().ThrowAsync<BadRequestException>()
            .WithMessage("Plano de contas que aceita lançamento não pode ter contas filhas.");

        // Garantimos que a transação não foi comitada
        await _unitOfWork.DidNotReceive().SalvarAlteracoes(Arg.Any<CancellationToken>());
    }
}