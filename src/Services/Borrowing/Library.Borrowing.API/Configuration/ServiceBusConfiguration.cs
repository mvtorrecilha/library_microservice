using Library.Adapter.EventBus.Application;

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
        eventBus.StartConsumer();

        return app;
    }
}
