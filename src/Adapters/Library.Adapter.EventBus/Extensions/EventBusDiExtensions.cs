using Library.Adapter.EventBus.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Library.Adapter.EventBus.Extensions;

public static class EventBusDiExtensions
{
    public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddRabbitMQ(configuration);
    }

    private static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration) =>
        services
            .AddSingleton<IApplicationRabbitMQPersistentConnection, ApplicationRabbitMQPersistentConnection>()
            .AddSingleton<IApplicationEventBus, ApplicationEventBusRabbitMQ>()
            .AddSingleton<IConnectionFactory>((serviceProvider) =>
            {
                return new ConnectionFactory()
                {
                    HostName = configuration["EventBusConnection"],
                    DispatchConsumersAsync = true
                };
            });

}
