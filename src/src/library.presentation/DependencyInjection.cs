using library.presentation.Endpoints.Base;
using Microsoft.AspNetCore.Builder;
using System.Reflection;

namespace library.presentation;
public static class DependencyInjection
{
    public static WebApplicationBuilder AddPresentationLayer(this WebApplicationBuilder builder)
    {
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











