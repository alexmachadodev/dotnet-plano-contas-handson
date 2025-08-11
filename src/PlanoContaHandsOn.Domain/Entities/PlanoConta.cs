namespace PlanoContaHandsOn.Domain.Entities;

public class PlanoConta
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public Codigo Codigo { get; private set; }
    public bool AceitaLancamento { get; private set; }
    public Tipo Tipo { get; private set; }
    public Guid? IdPai { get; private set; }

    public PlanoConta(string nome, Codigo codigo, Tipo tipo, bool aceitaLancamento, Guid? idPai, Tipo? tipoPai)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("O nome do plano de conta não pode ser vazio.");

        if(codigo is null)
            throw new DomainException("O código do plano de conta não pode ser vazio.");

        if (idPai.HasValue && tipo != tipoPai)
            throw new DomainException($"O tipo da conta filha ({tipo}) deve ser igual ao tipo da conta pai ({tipoPai}).");

        Id = Guid.NewGuid();
        Nome = nome;
        Codigo = codigo;
        Tipo = tipo;
        AceitaLancamento = aceitaLancamento;
        IdPai = idPai;
    }

    private PlanoConta() { }
}
