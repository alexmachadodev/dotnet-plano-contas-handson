namespace PlanoContaHandsOn.Domain.Exceptions;

public class ConflictException(string message) : Exception(message)
{
    public ConflictException() : this("A operação não pôde ser concluída pois os dados foram modificados por outro usuário. Tente novamente.")
    {
    }
}