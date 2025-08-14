var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Database");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IPlanoContaRepository, PlanoContaRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IGeradorCodigoPlanoContaService, GeradorCodigoPlanoContaService>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CriarPlanoContaCommand).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(CriarPlanoContaCommand).Assembly);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddSwaggerDocumentation();

builder.Services.AddTelemetry(builder.Configuration);

builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapPrometheusScrapingEndpoint();

app.UseSwaggerDocumentation();

app.MapPlanoContaEndpoints();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();