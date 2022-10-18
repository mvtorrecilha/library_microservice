using Autofac;
using Library.Infra.EventBus.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Library.Infra.EventBus.Extensions;

public static class EventBusDiExtensions
{
    public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddRabbitMQ(configuration);
    }

    private static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSingleton<IApplicationRabbitMQPersistentConnection, ApplicationRabbitMQPersistentConnection>()
            .AddSingleton<IApplicationEventBus, ApplicationEventBusRabbitMQ>()
            .AddSingleton<IConnectionFactory>((serviceProvider) =>
            {
                return new ConnectionFactory()
                {
                    HostName = configuration["RabbitMQConnection:EventBusConnection"],
                    UserName = configuration["RabbitMQConnection:UserName"],
                    Password = configuration["RabbitMQConnection:Password"],
                    DispatchConsumersAsync = true
                };
            });

        services.AddSingleton<IApplicationEventBus, ApplicationEventBusRabbitMQ>(sp =>
        {
            var rabbitMQPersistentConnection = sp.GetRequiredService<IApplicationRabbitMQPersistentConnection>();
            var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
            var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
            var logger = sp.GetRequiredService<ILogger<ApplicationEventBusRabbitMQ>>();
            int retryCount = Int32.Parse(configuration["RabbitMQConnection:RetryCount"]);


            return new ApplicationEventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, retryCount);
        });

        return services;
    }
}
