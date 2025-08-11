namespace PlanoContaHandsOn.UnitTests.Domain.ValueObjects;

public class CodigoTests
{
    [Theory]
    [InlineData("1.1000")]
    [InlineData("1.ABC")]
    [InlineData("1.0")]
    [InlineData("")]
    public void Criar_ComStringInvalida_DeveLancarExcecao(string valorInvalido)
    {
        // Arrange
        Action acao = () => Codigo.Criar(valorInvalido);

        // Assert
        acao.Should().Throw<DomainException>();
    }

    [Fact]
    public void ValidarHierarquia_ComPrefixoIncorreto_DeveLancarExcecao()
    {
        // Arrange
        var codigoFilho = Codigo.Criar("2.1");
        var codigoPai = Codigo.Criar("1");

        // Act
        var acao = () => codigoFilho.ValidarHierarquia(codigoPai);

        // Assert
        acao.Should().Throw<DomainException>();
    }
}