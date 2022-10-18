using Autofac.Extensions.DependencyInjection;
using Library.Infra.EventBus.Extensions;
using Library.Infra.ResponseFormatter;
using Library.Web.Aggregator.Extensions;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services
    .AddNotifier()
    .AddGrpcServices(builder.Configuration)
    .AddEventBus(builder.Configuration)
    .AddMediatR(typeof(Program))
    .AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
