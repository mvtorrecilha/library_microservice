using Autofac;
using Autofac.Extensions.DependencyInjection;
using Library.Adapter.EventBus.Extensions;
using Library.Borrowing.API.Configuration;
using Library.Borrowing.Application;
using Library.Borrowing.Infrastructure.Data;
using Library.Borrowing.Infrastructure.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(ConfigureContainer.ContainerBuilderEvents);

builder.Services
    .AddSqlData(builder.Configuration)
    .AddSqlServices()
    .AddMediatR(typeof(Program))
    .AddEventBus(builder.Configuration)
    .AddApplication()
    
    .AddControllers();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Library.Borrowing API", Version = "v1" });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<BorrowContext>();
    context.Database.Migrate();
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

app.ConfigureEventBus();

app.Run();