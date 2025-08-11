namespace PlanoContaHandsOn.UnitTests.Domain.Entities;

public class PlanoContaTests
{
    [Fact]
    public void Construtor_QuandoTipoFilhaDiferenteDoPai_DeveLancarExcecao()
    {
        // Arrange
        var nome = "Salário";
        var codigo = Codigo.Criar("1.1");
        var tipoFilha = Tipo.Despesa;
        var tipoPai = Tipo.Receita;
        var idPai = Guid.NewGuid();

        // Act
        Action acao = () => new PlanoConta(nome, codigo, tipoFilha, aceitaLancamento: true, idPai, tipoPai);

        // Assert
        acao.Should().Throw<DomainException>()
            .WithMessage($"O tipo da conta filha ({tipoFilha}) deve ser igual ao tipo da conta pai ({tipoPai}).");
    }

    [Fact]
    public void Construtor_QuandoDadosValidos_DeveCriarContaCorretamente()
    {
        // Arrange
        var tipoPai = Tipo.Receita;

        // Act
        var conta = new PlanoConta("Salário", Codigo.Criar("1.1"), Tipo.Receita, true, Guid.NewGuid(), tipoPai);

        // Assert
        conta.Id.Should().NotBe(Guid.Empty);
        conta.Nome.Should().Be("Salário");
    }
}
