using Library.Adapter.EventBus.Extensions;
using Library.Adapter.ResponseFormatter;
using Library.Web.Aggregator.Extensions;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddNotifier()
    .AddGrpcServices(builder.Configuration)
    .AddEventBus(builder.Configuration)
    .AddMediatR(typeof(Program))
    .AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
