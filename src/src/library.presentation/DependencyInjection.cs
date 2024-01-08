using library.presentation.Endpoints.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace library.presentation;
public static class DependencyInjection
{
    public static WebApplicationBuilder AddPresentationLayer(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Library API", Version = "v1" });

            // Define the OAuth2.0 scheme that's in use (i.e. Implicit, Password, etc)
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

        return builder;
    }

    public static void RegisterEndpoints(this WebApplication app)
    {
        Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => typeof(IModule).IsAssignableFrom(type) is true && type.IsInterface is false && type.IsAbstract is false)
            .Select(type => (IModule)Activator.CreateInstance(type)!).ToList()
            .ForEach(module => module.RegisterEndpoints(app));
    }
}











