using Library.Adapter.ResponseFormatter;
using Library.Web.Aggregator.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddNotifier()
    .AddGrpcServices(builder.Configuration)
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
