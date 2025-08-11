namespace PlanoContaHandsOn.API.Configuration;

public static class SwaggerConfigExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API de Plano de Contas",
                Version = "v1",
                Description = "API para gerenciamento de um plano de contas hierárquico.",
                Contact = new OpenApiContact
                {
                    Name = "Alex Machado",
                    Email = "alexmachadodev@gmail.com"
                }
            });
        });

        return services;
    }

    public static WebApplication UseSwaggerDocumentation(this WebApplication app)
    {
        if (app.Environment.IsDevelopment() is false) 
            return app;

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Plano de Contas API v1");
            options.RoutePrefix = "swagger";
        });

        return app;
    }
}