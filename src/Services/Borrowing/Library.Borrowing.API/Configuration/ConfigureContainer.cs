using Autofac;
using Library.Borrowing.Application.IntegrationEvents.EventHandling;

namespace Library.Borrowing.API.Configuration;

public static class ConfigureContainer
{
    public static void ContainerBuilderEvents(ContainerBuilder arg2)
    {
        arg2.RegisterType<BookBorrowingAcceptedIntegrationEventHandler>();
    }
}

