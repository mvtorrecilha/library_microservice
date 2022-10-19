using Autofac;
using Library.Book.Application.IntegrationEvents.EventHandling;

namespace Library.Book.API.Configuration;

public static class ConfigureContainer
{
    public static void ContainerBuilderEvents(ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterType<BookBorrowingAcceptedIntegrationEventHandler>();
    }
}
