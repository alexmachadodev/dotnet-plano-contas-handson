namespace PlanoContaHandsOn.Domain.ValueObjects;

public sealed record Codigo
{
    public string Value { get; }
    public const int LimiteMaximoPorNivel = 999;

    private Codigo(string value) => Value = value;

    public static Codigo Criar(string codigoStr)
    {
        if (string.IsNullOrWhiteSpace(codigoStr))
            throw new DomainException("O código não pode ser vazio.");

        var segmentos = codigoStr.Split('.');

        foreach (var segmento in segmentos)
        {
            if (int.TryParse(segmento, out var valorSegmento) is false)
                throw new DomainException("Cada segmento do código da conta deve ser um número.");

            if (valorSegmento is > LimiteMaximoPorNivel or < 1)
                throw new DomainException($"Cada segmento do código da conta deve estar entre 1 e {LimiteMaximoPorNivel}.");
        }

        return new Codigo(codigoStr);
    }

    public void ValidarHierarquia(Codigo? codigoPai)
    {
        if (codigoPai is not null && Value.StartsWith(codigoPai.Value + ".") is false)
            throw new DomainException($"O código '{Value}' não corresponde à hierarquia da conta pai '{codigoPai.Value}'.");
    }
}