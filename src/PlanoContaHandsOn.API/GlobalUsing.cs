global using Microsoft.OpenApi.Models;
global using Microsoft.AspNetCore.Diagnostics;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Diagnostics.HealthChecks;
global using HealthChecks.UI.Client;

global using FluentValidation;
global using MediatR;

global using PlanoContaHandsOn.API.Endpoints.PlanosContas;
global using PlanoContaHandsOn.API.Middleware;
global using PlanoContaHandsOn.API.Configuration;

global using PlanoContaHandsOn.Application.PlanosContas.Commands.CriarPlanoConta;
global using PlanoContaHandsOn.Application.PlanosContas.Commands.ExcluirPlanoConta;
global using PlanoContaHandsOn.Application.PlanosContas.Queries.ListarPlanoConta;
global using PlanoContaHandsOn.Application.PlanosContas.Queries.ObterProximoCodigo;

global using PlanoContaHandsOn.Domain.Interfaces;
global using PlanoContaHandsOn.Domain.Exceptions;

global using PlanoContaHandsOn.Application.PlanosContas.Services;
global using PlanoContaHandsOn.Application.Exceptions;
global using PlanoContaHandsOn.Application.PlanosContas.Queries.ListarTipo;
global using PlanoContaHandsOn.Application.Shared;

global using PlanoContaHandsOn.Infrastructure.Data;
global using PlanoContaHandsOn.Infrastructure.Data.Repositories;