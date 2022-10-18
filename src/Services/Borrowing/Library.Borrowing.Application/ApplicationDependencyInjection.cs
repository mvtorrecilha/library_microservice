using Library.Adapter.EventBus;
using Library.Borrowing.Application.IntegrationEvents.EventHandling;
using Library.Borrowing.Application.IntegrationEvents.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Borrowing.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

        services.AddScoped<IEventHandler<BookBorrowingAcceptedIntegrationEvent>, BookBorrowingAcceptedIntegrationEventHandler>();
        return services;
    }

}
