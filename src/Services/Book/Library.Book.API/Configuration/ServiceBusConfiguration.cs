using Library.Book.Application.IntegrationEvents.EventHandling;
using Library.Book.Application.IntegrationEvents.Events;
using Library.Domain.Core.Bus;

namespace Library.Book.API.Configuration;

/// <summary>
/// Class to configure service bus
/// </summary>
public static class ServiceBusConfiguration
{
    /// <summary>
    /// Congiure service bus
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder ConfigureEventBus(this IApplicationBuilder app)
    {
        var eventBus = app.ApplicationServices.GetRequiredService<IApplicationEventBus>();
        eventBus.Subscribe<BookBorrowingAcceptedIntegrationEvent, BookBorrowingAcceptedIntegrationEventHandler>();

        return app;
    }
}
