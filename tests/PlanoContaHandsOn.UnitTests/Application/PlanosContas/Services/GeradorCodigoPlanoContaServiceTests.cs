namespace PlanoContaHandsOn.UnitTests.Application.PlanosContas.Services;

public class GeradorCodigoPlanoContaServiceTests
{
    private readonly GeradorCodigoPlanoContaService _gerador = new();

    [Theory]
    [MemberData(nameof(DadosParaGeracaoDeCodigoRaiz))]
    public void Gerar_ParaContaRaiz_DeveRetornarCodigoCorreto(IReadOnlyCollection<Codigo> codigosFilhos, string codigoEsperado)
    {
        // Act
        var proximoCodigo = _gerador.Gerar(codigoPai: null, codigosFilhos);

        // Assert
        proximoCodigo.Value.Should().Be(codigoEsperado);
    }

    public static IEnumerable<object[]> DadosParaGeracaoDeCodigoRaiz()
    {
        yield return [new List<Codigo>(), "1"];

        yield return [new List<Codigo> { Codigo.Criar("1"), Codigo.Criar("3"), Codigo.Criar("2") }, "4"];

        yield return [new List<Codigo> { Codigo.Criar("9") }, "10"];
    }

    [Theory]
    [MemberData(nameof(DadosParaGeracaoDeCodigoFilho))]
    public void Gerar_ParaContaFilha_DeveRetornarCodigoHierarquicoCorreto(Codigo codigoPai, IReadOnlyCollection<Codigo> codigosFilhos, string codigoEsperado)
    {
        var proximoCodigo = _gerador.Gerar(codigoPai, codigosFilhos);
        proximoCodigo.Value.Should().Be(codigoEsperado);
    }

    public static IEnumerable<object[]> DadosParaGeracaoDeCodigoFilho()
    {
        yield return [Codigo.Criar("1"), new List<Codigo>(), "1.1"];
        yield return [Codigo.Criar("2"), new List<Codigo> { Codigo.Criar("2.1"), Codigo.Criar("2.2") }, "2.3"];
        yield return [Codigo.Criar("1.1"), new List<Codigo>(), "1.1.1"];
        yield return [Codigo.Criar("2.1"), new List<Codigo> { Codigo.Criar("2.1.1"), Codigo.Criar("2.1.3") }, "2.1.4"];
        yield return [Codigo.Criar("3.1.1"), new List<Codigo>(), "3.1.1.1"];
        yield return [Codigo.Criar("3.1.1"), new List<Codigo> { Codigo.Criar("3.1.1.1") }, "3.1.1.2"];
        yield return [Codigo.Criar("1"), new List<Codigo> { Codigo.Criar("1.9"), Codigo.Criar("1.10") }, "1.11"];
    }

    [Theory]
    [MemberData(nameof(DadosParaNivelCheio))]
    public void Gerar_QuandoNivelEstaCheio_DeveLancarExcecao(Codigo codigoPai, IReadOnlyCollection<Codigo> codigosFilhos)
    {
        Action acao = () => _gerador.Gerar(codigoPai, codigosFilhos);

        acao.Should().Throw<DomainException>();
    }

    public static IEnumerable<object[]> DadosParaNivelCheio()
    {
        yield return [null, new List<Codigo> { Codigo.Criar("999") }];
        yield return [Codigo.Criar("1.1"), new List<Codigo> { Codigo.Criar("1.1.999") }];
    }
}