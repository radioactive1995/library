using FluentValidation;
using library.application.Common.Pipelines;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace library.application;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddApplicationLayer(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));
        return builder;
    }
}