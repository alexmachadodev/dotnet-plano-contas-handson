namespace PlanoContaHandsOn.Domain.Exceptions;

public class UniqueConstraintException(string message) : Exception(message)
{
    public UniqueConstraintException() : this("Um ou mais valores informados já estão em uso. Verifique os dados e tente novamente.")
    {
    }
}