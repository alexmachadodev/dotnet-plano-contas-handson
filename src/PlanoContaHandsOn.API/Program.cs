var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Database");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString, sqlServerOptions =>
    {
        sqlServerOptions.UseHierarchyId();
    }));

builder.Services.AddScoped<IPlanoContaRepository, PlanoContaRepository>();
builder.Services.AddTransient<IGeradorCodigoPlanoContaService, GeradorCodigoPlanoContaService>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CriarPlanoContaCommand).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(CriarPlanoContaCommand).Assembly);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

app.UseSwaggerDocumentation();

app.MapPlanoContaEndpoints();

app.UseExceptionHandler(options => { });

app.Run();