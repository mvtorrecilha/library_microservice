using Autofac;
using Library.Borrowing.Application.IntegrationEvents.EventHandling;

namespace Library.Borrowing.API.Configuration;

public static class ConfigureContainer
{
    public static void ContainerBuilderEvents(ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterType<BookReturnedAcceptedIntegrationEventHandler>();
    }
}

