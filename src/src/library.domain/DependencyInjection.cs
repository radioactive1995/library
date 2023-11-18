using Microsoft.AspNetCore.Builder;

namespace library.domain;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddDomainLayer(this WebApplicationBuilder builder)
    {

        return builder;
    }
}