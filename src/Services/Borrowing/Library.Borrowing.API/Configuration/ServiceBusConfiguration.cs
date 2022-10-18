using Library.Adapter.EventBus.Application;
using Library.Borrowing.Application.IntegrationEvents.EventHandling;
using Library.Borrowing.Application.IntegrationEvents.Events;

namespace Library.Borrowing.API.Configuration;

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
