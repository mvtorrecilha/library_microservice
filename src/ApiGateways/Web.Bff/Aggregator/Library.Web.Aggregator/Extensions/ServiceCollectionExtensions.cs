using GrpcBook;
using GrpcStudent;
using Library.Web.Aggregator.Services;
using Library.Web.Aggregator.Services.Interfaces;

namespace Library.Web.Aggregator.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGrpcServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddGrpcServicesDI()
            .AddGrpcClientDI(configuration);
    }

    public static IServiceCollection AddGrpcServicesDI(this IServiceCollection services)
    {
        return services
        .AddScoped<IBorrowingService, BorrowingService>();
    }

    public static IServiceCollection AddGrpcClientDI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpcClient<StudentGrpc.StudentGrpcClient>((services, options) =>
        {
            options.Address = new Uri(configuration["urls:student"]);
        });

        services.AddGrpcClient<BookGrpc.BookGrpcClient>((services, options) =>
        {
            options.Address = new Uri(configuration["urls:book"]);
        });

        return services;
    }
}
