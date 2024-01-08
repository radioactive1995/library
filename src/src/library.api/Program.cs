using library.application;
using library.domain;
using library.infrastructure;
using library.presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.AddPresentationLayer()
    .AddInfrastructureLayer()
    .AddApplicationLayer()
    .AddDomainLayer();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.RegisterEndpoints();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
