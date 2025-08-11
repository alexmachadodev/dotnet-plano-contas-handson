namespace PlanoContaHandsOn.Application.PlanosContas.Services;

public class GeradorCodigoPlanoContaService : IGeradorCodigoPlanoContaService
{
    public Codigo Gerar(Codigo? codigoPai, IReadOnlyCollection<Codigo> codigosFilhos)
    {
        if (codigoPai is null)
            return Codigo.Criar(ObterProximoCodigoRaiz(codigosFilhos));

        if (codigosFilhos.Any() is false)
            return Codigo.Criar($"{codigoPai.Value}.1");

        var maiorSufixo = codigosFilhos
            .Select(c => int.Parse(c.Value.Split('.').Last()))
            .Max();

        return Codigo.Criar($"{codigoPai.Value}.{maiorSufixo + 1}");
    }

    private static string ObterProximoCodigoRaiz(IReadOnlyCollection<Codigo> codigosFilhos)
    {
        if (codigosFilhos.Any() is false)
            return "1";

        var maiorCodigoRaiz = codigosFilhos.Select(c => int.Parse(c.Value)).Max();
        
        return (maiorCodigoRaiz + 1).ToString(); 
    }
}