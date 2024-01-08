using library.application.Common.Interfaces.Authentication;
using library.application.Common.Interfaces.Persistance;
using library.application.Common.Interfaces.Persistance.Books;
using library.application.Common.Interfaces.Persistance.Users;
using library.infrastructure.Persistance.Common;
using library.infrastructure.Persistance.Context;
using library.infrastructure.Persistance.Context.Interceptors;
using library.infrastructure.Persistance.Repositories;
using library.infrastructure.Persistance.Repositories.Books;
using library.infrastructure.Persistance.Repositories.Users;
using library.infrastructure.Providers.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace library.infrastructure;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddInfrastructureLayer(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IBookCommandRepository, BookCommandRepository>();
        builder.Services.AddScoped<IBookQueryRepository, BookQueryRepository>();
        builder.Services.AddScoped<IUserCommandRepository, UserCommandRepository>();
        builder.Services.AddScoped<IUserQueryRepository, UserQueryRepository>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddSingleton<IPasswordProvider, PasswordProvider>();
        builder.Services.AddSingleton<IJwtTokenProvider, JwtTokenProvider>();
        builder.Services.Configure<DatabaseConfiguration>(builder.Configuration.GetSection(nameof(DatabaseConfiguration)));
        builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection(nameof(JwtConfiguration)));
        builder.Services.AddDbContext<LibraryDbContext>((sp, options) =>
        {
            options.UseSqlServer(builder.Configuration.GetSection(nameof(DatabaseConfiguration)).Get<DatabaseConfiguration>()?.ConnectionString
                ?? throw new ArgumentNullException(nameof(DatabaseConfiguration.ConnectionString)))
            .AddInterceptors(new EventDomainInterceptor(sp.GetRequiredService<IPublisher>()));
        });

        var jwtConfiguration = new JwtConfiguration()
        {
            Audience = string.Empty,
            Secret = string.Empty,
            ExpiryMinutes = default,
            Issuer = string.Empty,
        };

        builder.Configuration.Bind(key: nameof(JwtConfiguration), jwtConfiguration);

        builder.Services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidAudience = jwtConfiguration.Audience,

                ValidateIssuer = true,
                ValidIssuer = jwtConfiguration.Issuer,

                ValidateLifetime = true,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Secret)),
            });

        builder.Services.AddAuthorization();

        return builder;
    }
}
