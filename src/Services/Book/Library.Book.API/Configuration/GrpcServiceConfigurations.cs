using Library.Book.API.Grpc;

namespace Library.Book.API.Configuration;

public static class GrpcServiceConfigurations
{
    public static IServiceCollection AddCustomGrpc(this IServiceCollection services)
    {
        services
             .AddGrpc(options =>
             {
                 options.EnableDetailedErrors = true;
             });

        return services;
    }

    public static IEndpointRouteBuilder MapGrpcServices(this IEndpointRouteBuilder endpoins)
    {
        endpoins.MapGrpcService<BookService>();

        return endpoins;
    }
}
