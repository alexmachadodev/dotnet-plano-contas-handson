namespace PlanoContaHandsOn.Application.PlanosContas.Commands.CriarPlanoConta;

public record CriarPlanoContaCommand(string Nome, string Codigo, bool AceitaLancamento, Tipo Tipo, Guid? IdPai) : IRequest<Guid>;

public class CriarPlanoContaCommandValidator : AbstractValidator<CriarPlanoContaCommand>
{
    public CriarPlanoContaCommandValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome do plano de conta é obrigatório.")
            .MaximumLength(100).WithMessage("O nome do plano de conta não pode exceder 100 caracteres.");

        RuleFor(x => x.Codigo)
            .NotEmpty().WithMessage("O código do plano de conta é obrigatório.");

        RuleFor(x => x.Tipo)
            .IsInEnum().WithMessage("O tipo especificado não é válido.");
    }
}