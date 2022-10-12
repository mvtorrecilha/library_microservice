using Library.Book.API.Configuration;
using Library.Book.Application;
using Library.Book.Infrastructure.Data;
using Library.Book.Infrastructure.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCustomGrpc()
    .AddSqlData(builder.Configuration)
    .AddSqlServices()
    .AddApplication()
    .AddMediatR(typeof(Program))
    .AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Library.Book API", Version = "v1" });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var logger = scope.ServiceProvider.GetService<ILogger<BookContextSeed>>();
    var context = services.GetRequiredService<BookContext>();
    context.Database.Migrate();

    new BookContextSeed()
             .SeedAsync(context, logger)
             .Wait();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGrpcServices();

app.Run();
