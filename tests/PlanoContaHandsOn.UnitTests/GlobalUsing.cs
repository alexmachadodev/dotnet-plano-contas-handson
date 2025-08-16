global using FluentAssertions;
global using NSubstitute;

global using PlanoContaHandsOn.Domain.Exceptions;
global using PlanoContaHandsOn.Domain.ValueObjects;
global using PlanoContaHandsOn.Domain.Entities;
global using PlanoContaHandsOn.Domain.Enums;
global using PlanoContaHandsOn.Domain.Repositories;

global using PlanoContaHandsOn.Application.PlanosContas.Commands.CriarPlanoConta;
global using PlanoContaHandsOn.Application.Exceptions;
global using PlanoContaHandsOn.Application.PlanosContas.Commands.ExcluirPlanoConta;
global using PlanoContaHandsOn.Application.PlanosContas.Queries.ObterProximoCodigo;
global using PlanoContaHandsOn.Application.PlanosContas.Services;
global using PlanoContaHandsOn.Application.PlanosContas.Queries.ListarPlanoConta;