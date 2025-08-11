namespace PlanoContaHandsOn.Application.PlanosContas.Commands.ExcluirPlanoConta;

public record ExcluirPlanoContaCommand(Guid Id) : IRequest;

public class ExcluirPlanoContaCommandValidator : AbstractValidator<ExcluirPlanoContaCommand>
{
    public ExcluirPlanoContaCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("O id do plano de conta é obrigatório.");
    }
}