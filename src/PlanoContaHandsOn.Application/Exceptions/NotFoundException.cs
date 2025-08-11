namespace PlanoContaHandsOn.Application.Exceptions;

public class NotFoundException(string message) : Exception(message)
{
    public NotFoundException(string name, object key) : this($"Entidade \"{name}\" ({key}) não foi encontrado.")
    {
    }
}