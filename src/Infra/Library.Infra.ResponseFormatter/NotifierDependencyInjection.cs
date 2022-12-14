using Library.Infra.ResponseFormatter.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Infra.ResponseFormatter;

public static class NotifierDependencyInjection
{
    public static IServiceCollection AddNotifier(this IServiceCollection services)
    {
        services
             .AddScoped(typeof(INotifier), typeof(Notifier))
             .AddScoped(typeof(IResponseFormatterResult), typeof(ResponseFormatterResult));

        return services;
    }
}
