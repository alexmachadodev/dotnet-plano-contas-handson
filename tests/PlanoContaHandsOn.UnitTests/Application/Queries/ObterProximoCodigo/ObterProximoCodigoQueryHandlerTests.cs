namespace PlanoContaHandsOn.UnitTests.Application.Queries.ObterProximoCodigo;

public class ObterProximoCodigoQueryHandlerTests
{
    private readonly IPlanoContaRepository _planoContaRepository;
    private readonly ObterProximoCodigoQueryHandler _handler;

    public ObterProximoCodigoQueryHandlerTests()
    {
        _planoContaRepository = Substitute.For<IPlanoContaRepository>();
        var geradorCodigoPlanoConta = new GeradorCodigoPlanoContaService();
        _handler = new ObterProximoCodigoQueryHandler(_planoContaRepository, geradorCodigoPlanoConta);
    }

    [Fact]
    public async Task Handle_QuandoPaiExiste_DeveRetornarProximoCodigoGeradoCorretamente()
    {
        // Arrange
        var idPai = Guid.NewGuid();
        var query = new ObterProximoCodigoQuery(idPai);

        var contaPai = new PlanoConta("Receitas", Codigo.Criar("1"), Tipo.Receita, false, null, null);
        _planoContaRepository.ObterPorId(idPai, Arg.Any<CancellationToken>()).Returns(contaPai);

        var codigosFilhos = new List<Codigo> { Codigo.Criar("1.1"), Codigo.Criar("1.2") };
        _planoContaRepository.ObterCodigosFilhos(idPai, Arg.Any<CancellationToken>()).Returns(codigosFilhos);

        // Act
        var codigoSugerido = await _handler.Handle(query, CancellationToken.None);

        // Assert
        codigoSugerido.ProximoCodigo.Should().Be("1.3");
    }
}