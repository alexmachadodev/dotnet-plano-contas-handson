namespace PlanoContaHandsOn.Application.Shared;

public record ResultadoPaginado<T>(int Pagina, int TamanhoPagina, long TotalRegistros, IReadOnlyCollection<T> Itens);