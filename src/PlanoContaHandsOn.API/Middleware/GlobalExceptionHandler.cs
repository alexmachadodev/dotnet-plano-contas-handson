namespace PlanoContaHandsOn.API.Middleware;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = CriarProblemDetails(context, exception);

        context.Response.StatusCode = problemDetails.Status.GetValueOrDefault(StatusCodes.Status500InternalServerError);

        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private ProblemDetails CriarProblemDetails(HttpContext context, Exception exception)
    {
        var problemDetails = exception switch
        {
            InternalServerException intenalServerErrorr => new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Erro interno",
                Detail = intenalServerErrorr.Message
            },
            ValidationException validationException => new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Erro de validação",
                Detail = "Um ou mais erros de validação ocorreram.",
                Extensions =
                    { ["errors"] = validationException.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }) }
            },
            BadRequestException badRequestException => new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Erro de validação",
                Detail = badRequestException.Message
            },
            NotFoundException notFoundException => new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Não encontrado",
                Detail = notFoundException.Message
            },
            DomainException domainException => new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Erro de validação de negócio",
                Detail = domainException.Message
            },
            ConflictException conflictException => new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Dados modificados por outro usuario",
                Detail = conflictException.Message
            },
            UniqueConstraintException uniqueConstraintException => new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Dados já cadastrados",
                Detail = uniqueConstraintException.Message
            },
            _ => new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Erro interno do servidor",
                Detail = "Ocorreu um erro inesperado. Por favor, contate o suporte e forneça o ID de rastreamento."
            }
        };

        if (problemDetails.Status == StatusCodes.Status500InternalServerError)
        {
            logger.LogError(exception, "Erro Interno do Servidor: {Message}. RequestId: {RequestId}",
                exception.Message, context.TraceIdentifier);
        }

        problemDetails.Extensions["requestId"] = context.TraceIdentifier;
        problemDetails.Extensions["traceId"] = System.Diagnostics.Activity.Current?.Id;
        problemDetails.Instance = $"{context.Request.Method} {context.Request.Path}";

        return problemDetails;
    }

}