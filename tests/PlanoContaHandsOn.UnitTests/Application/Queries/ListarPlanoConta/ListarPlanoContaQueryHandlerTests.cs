namespace PlanoContaHandsOn.UnitTests.Application.Queries.ListarPlanoConta;

public class ListarPlanoContaQueryHandlerTests
{
    private readonly IPlanoContaRepository _planoContaRepository;
    private readonly ListarPlanoContaQueryHandler _handler;

    public ListarPlanoContaQueryHandlerTests()
    {
        _planoContaRepository = Substitute.For<IPlanoContaRepository>();
        _handler = new ListarPlanoContaQueryHandler(_planoContaRepository);
    }

    [Fact]
    public async Task Handle_QuandoChamado_DeveRetornarResultadoPaginadoCorretamente()
    {
        // Arrange
        var query = new ListarPlanoContaQuery(1, 10, "Receita");

        var contasMock = new List<PlanoConta>
        {
            new("Receitas de Vendas", Codigo.Criar("1.1"), Tipo.Receita, true, null, null)
        };

        var totalRegistrosMock = 1;

        _planoContaRepository.ListarPaginado(query.Pagina, query.TamanhoPagina, query.Filtro, Arg.Any<CancellationToken>())
            .Returns((contasMock, totalRegistrosMock));

        // Act
        var resultado = await _handler.Handle(query, CancellationToken.None);

        // Assert
        resultado.Should().NotBeNull();
        resultado.Pagina.Should().Be(query.Pagina);
        resultado.TamanhoPagina.Should().Be(query.TamanhoPagina);
        resultado.TotalRegistros.Should().Be(totalRegistrosMock);
        resultado.Itens.Should().HaveCount(1);

        var primeiroItem = resultado.Itens.First();
        primeiroItem.Nome.Should().Be("Receitas de Vendas");
        primeiroItem.Codigo.Should().Be("1.1");
        primeiroItem.Tipo.Should().Be(nameof(Tipo.Receita));
    }
}