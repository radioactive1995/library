using library.application;
using library.domain;
using library.infrastructure;
using library.presentation;
using library.presentation.Core;
using library.presentation.Endpoints.Books;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddPresentationLayer()
    .AddInfrastructureLayer()
    .AddApplicationLayer()
    .AddDomainLayer();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.RegisterEndpoints();

app.UseAuthorization();

app.MapControllers();

app.Run();
