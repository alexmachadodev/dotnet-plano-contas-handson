namespace PlanoContaHandsOn.Application.PlanosContas.Services;

public interface IGeradorCodigoPlanoContaService
{
    Codigo Gerar(Codigo? codigoPai, IReadOnlyCollection<Codigo> codigosFilhos);
}